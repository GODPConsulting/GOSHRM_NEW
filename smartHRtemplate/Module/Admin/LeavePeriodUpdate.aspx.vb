Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Telerik.Web.UI


Public Class LeavePeriodUpdate
    Inherits System.Web.UI.Page
    Dim leaveperiod As New clsLeavePeriod
    Dim AuthenCode As String = "LEAVEPERIOD"
    Dim olddata(4) As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                'Company_Structure_get_parent
                radYearStart.Items.Clear()
                radYearEnd.Items.Clear()
                For z As Integer = 2015 To 2050
                    Dim itemTemp As New RadComboBoxItem()
                    itemTemp.Text = z.ToString
                    itemTemp.Value = z.ToString
                    radYearStart.Items.Add(itemTemp)
                    itemTemp.DataBind()
                    If (z = Now.Year) Then
                        Exit For
                    End If
                Next

                For z As Integer = 2015 To 2050
                    Dim itemTemp As New RadComboBoxItem()
                    itemTemp.Text = z.ToString
                    itemTemp.Value = z.ToString
                    radYearEnd.Items.Add(itemTemp)
                    itemTemp.DataBind()
                    If (z = Now.Year) Then
                        Exit For
                    End If
                Next

                If Request.QueryString("id") IsNot Nothing Then
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Leave_Period_get", Request.QueryString("id"))
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    aname.Value = strUser.Tables(0).Rows(0).Item("name").ToString
                    Process.AssignRadComboValue(radMonthStart, CDate(strUser.Tables(0).Rows(0).Item("PeriodStart")).Month)
                    Process.AssignRadComboValue(radYearStart, CDate(strUser.Tables(0).Rows(0).Item("PeriodStart")).Year)

                    Process.AssignRadComboValue(radMonthEnd, CDate(strUser.Tables(0).Rows(0).Item("PeriodEnd")).Month)
                    Process.AssignRadComboValue(radYearEnd, CDate(strUser.Tables(0).Rows(0).Item("PeriodEnd")).Year)
                End If

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False Then
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
                Exit Sub
            End If


            Dim lblstatus As String = ""
            If (aname.Value.Trim = "") Then
                lblstatus = "Name required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                aname.Focus()
                Exit Sub
            End If

            If radYearStart.SelectedValue Is Nothing Then
                lblstatus = "Start Year required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                radYearStart.Focus()
                Exit Sub
            End If

            If radMonthStart.SelectedValue Is Nothing Then
                lblstatus = "Start Month required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                radMonthStart.Focus()
                Exit Sub
            End If

            If radYearEnd.SelectedValue Is Nothing Then
                lblstatus = "End Year required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                radYearEnd.Focus()
                Exit Sub
            End If

            If radMonthEnd.SelectedValue Is Nothing Then
                lblstatus = "End Month required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                radMonthEnd.Focus()
                Exit Sub
            End If

            'Old Data
            If txtid.Text.Trim = "" Then
                leaveperiod.id = 0
            Else
                leaveperiod.id = txtid.Text
            End If
            leaveperiod.Name = aname.Value.Trim
            leaveperiod.PeriodStart = Process.FirstDay(radYearStart.SelectedValue, radMonthStart.SelectedValue)
            leaveperiod.PeriodEnd = Process.LastDay(radYearEnd.SelectedValue, radMonthEnd.SelectedValue)

            Dim OldValue As String = ""
            Dim NewValue As String = ""

            Dim j As Integer = 0

            If leaveperiod.PeriodEnd < leaveperiod.PeriodStart Then
                lblstatus = "Start Date cannot be after End Date!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                radMonthEnd.Focus()
                Exit Sub
            End If


            If DateDiff(DateInterval.Month, leaveperiod.PeriodStart, leaveperiod.PeriodEnd) > 12 Then
                lblstatus = "Start Date and End Date cannot be beyond 12 calendar months!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                radMonthEnd.Focus()
                Exit Sub
            End If

            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Leave_Period_update", leaveperiod.id, leaveperiod.Name, leaveperiod.PeriodStart, leaveperiod.PeriodEnd)

            lblstatus = "Record saved"
            Process.loadalert(divalert, msgalert, lblstatus, "success")

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect("~/Module/Admin/leaveperiod", True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


End Class