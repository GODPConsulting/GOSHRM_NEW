Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Telerik.Web.UI


Public Class FinCalendarUpdate
    Inherits System.Web.UI.Page
    Dim leaveperiod As New clsLeavePeriod
    Dim AuthenCode As String = "FINCALENDAR"
    Dim olddata(4) As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                drpstartyear.Items.Clear()
                drpendyear.Items.Clear()
                For i As Integer = 2016 To 2050
                    Dim item As New ListItem()
                    item.Text = i
                    item.Value = i
                    drpstartyear.Items.Add(item)
                Next

                For i As Integer = 2016 To 2050
                    Dim item As New ListItem()
                    item.Text = i
                    item.Value = i
                    drpendyear.Items.Add(item)
                Next

                If Request.QueryString("id") IsNot Nothing Then
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Finance_Calendar_get", Request.QueryString("id"))
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    aname.Value = strUser.Tables(0).Rows(0).Item("name").ToString
                    Process.AssignHTMLSelectValue(drpstartmonth, CDate(strUser.Tables(0).Rows(0).Item("PeriodStart")).Month)
                    Process.AssignHTMLSelectValue(drpstartyear, CDate(strUser.Tables(0).Rows(0).Item("PeriodStart")).Year)

                    Process.AssignHTMLSelectValue(drpendmonth, CDate(strUser.Tables(0).Rows(0).Item("PeriodEnd")).Month)
                    Process.AssignHTMLSelectValue(drpendyear, CDate(strUser.Tables(0).Rows(0).Item("PeriodEnd")).Year)
                End If

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.message, "danger")
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False Then
                Process.loadalert(divalert, msgalert, Process.privilegemsg, "warning")
                Exit Sub
            End If
            Dim lblstatus As String = ""
            If (IsNumeric(aname.Value) = False) Then
                lblstatus = "Calendar Name required e.g 2015!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                aname.Focus()
                Exit Sub
            End If

            If drpstartmonth.Value Is Nothing Then
                lblstatus = "Financial start month required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                drpstartmonth.Focus()
                Exit Sub
            End If

            If drpstartyear.Value Is Nothing Then
                lblstatus = "Financial start year required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                drpstartyear.Focus()
                Exit Sub
            End If

            If drpendmonth.Value Is Nothing Then
                lblstatus = "Financial end month required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                drpendmonth.Focus()
                Exit Sub
            End If

            If drpendyear.Value Is Nothing Then
                lblstatus = "Financial end year required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                drpendyear.Focus()
                Exit Sub
            End If

            If txtid.Text.Trim = "" Then
                leaveperiod.id = 0
            Else
                leaveperiod.id = txtid.Text
            End If
            leaveperiod.Name = aname.Value.Trim
            leaveperiod.PeriodStart = Process.FirstDay(drpstartyear.Value, drpstartmonth.Value)
            leaveperiod.PeriodEnd = Process.LastDay(drpendyear.Value, drpendmonth.Value)

            If leaveperiod.PeriodEnd < leaveperiod.PeriodStart Then
                lblstatus = "Start Date cannot be after End Date!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                drpstartmonth.Focus()
                Exit Sub
            End If

            If DateDiff(DateInterval.Month, leaveperiod.PeriodStart, leaveperiod.PeriodEnd) > 12 Then
                lblstatus = "Start Date and End Date cannot be beyond 12 calendar months!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                drpendmonth.Focus()
                Exit Sub
            End If

            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Finance_Calendar_update", leaveperiod.id, leaveperiod.Name, leaveperiod.PeriodStart, leaveperiod.PeriodEnd)

            lblstatus = "Record saved"
            Process.loadalert(divalert, msgalert, lblstatus, "success")
            'Response.Write("<script language='javascript'> { self.close() }</script>")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect("~/Module/Finance/Settings/FinancialCalendar", True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.message, "danger")
        End Try
    End Sub


End Class