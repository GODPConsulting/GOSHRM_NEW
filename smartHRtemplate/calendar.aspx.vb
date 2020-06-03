Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Telerik.Web.UI

Public Class calendar
    Inherits System.Web.UI.Page
    Dim education As New clsEducation
    Dim AuthenCode As String = "EDUCARE"
    Dim olddata(4) As String
    Dim lblstatus As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'Process.LoadRadCombo(radEducationLevel, "Education_Level_Get_all", 0)
            If Not Me.IsPostBack Then
                Process.LoadRadComboTextAndValueP1(cboInvitees, "Emp_PersonalDetail_Get_Employees", Session("Access"), "Employee2", "EmpID", True)
                'Company_Structure_get_parent
                If Request.QueryString("id") IsNot Nothing Then
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Calendar_Event_Get", Request.QueryString("id"))
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    Process.LoadTimeToRadCombo(radHourStart, radMinStart, radTimeStart, strUser.Tables(0).Rows(0).Item("EventTime"))
                    txtDesc.Value = strUser.Tables(0).Rows(0).Item("EventDescription").ToString
                    txtname.Value = strUser.Tables(0).Rows(0).Item("EventTitle").ToString
                    radScheduleTime.SelectedDate = CDate(strUser.Tables(0).Rows(0).Item("EventDate"))
                    radEndDate.SelectedDate = CDate(strUser.Tables(0).Rows(0).Item("EventEndDate"))
                    Process.AssignRadComboValue(radStat, strUser.Tables(0).Rows(0).Item("EventStat").ToString)
                    Process.LoadListAndComboxFromDataset(lstInvitees, cboInvitees, "Calendar_Event_Members_Get", "Employee2", "invitees", txtid.Text)
                Else
                    txtid.Text = "0"
                    radScheduleTime.SelectedDate = DateTime.Now
                End If

            End If
        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "danger")
        End Try
    End Sub
    Private Function GetIdentity(ByVal sEmpID As String, ByVal eventDate As Date, ByVal eventtime As String, ByVal eventtitle As String, ByVal eventdesc As String, ByVal enddate As Date, ByVal eventstat As String) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Calendar_Event_Update"
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@empid", SqlDbType.VarChar).Value = sEmpID
            cmd.Parameters.Add("@eventdate", SqlDbType.Date).Value = eventDate
            cmd.Parameters.Add("@eventtime", SqlDbType.VarChar).Value = eventtime
            cmd.Parameters.Add("@eventtitle", SqlDbType.VarChar).Value = eventtitle
            cmd.Parameters.Add("@eventdesc", SqlDbType.VarChar).Value = eventdesc
            cmd.Parameters.Add("@eventenddate", SqlDbType.Date).Value = enddate
            cmd.Parameters.Add("@eventstat", SqlDbType.VarChar).Value = eventstat
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception
            Process.strExp = ex.Message
            Return 0
        End Try
    End Function
    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try

            If (txtname.Value.Trim = "") Then
                lblstatus = "Event Title required!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                txtname.Focus()
                Exit Sub
            End If

            If radScheduleTime.SelectedDate Is Nothing Then
                lblstatus = "Start Date required!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                radScheduleTime.Focus()
                Exit Sub
            End If

            If radEndDate.SelectedDate Is Nothing Then
                lblstatus = "End Date required!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                radEndDate.Focus()
                Exit Sub
            End If

            If txtid.Text = "0" Then
                txtid.Text = GetIdentity(Session("UserEmpID"), radScheduleTime.SelectedDate, Process.ConvertCtrlToTime(radHourStart.SelectedValue, radMinStart.SelectedValue, radTimeStart.SelectedValue), txtname.Value.Trim, txtDesc.Value, radEndDate.SelectedDate, radStat.SelectedValue)

                If txtid.Text = "0" Then
                    lblstatus = Process.strExp
                    Process.loadalert(divalert, msgalert, lblstatus, "danger")
                    Exit Sub
                End If
            Else
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Calendar_Event_Update", txtid.Text, Session("UserEmpID"), radScheduleTime.SelectedDate, Process.ConvertCtrlToTime(radHourStart.SelectedValue, radMinStart.SelectedValue, radTimeStart.SelectedValue), txtname.Value.Trim, txtDesc.Value, radEndDate.SelectedDate, radStat.SelectedValue)
            End If

            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Calendar_Event_Members_Update_Stat", txtid.Text, "N")
            If lstInvitees.Items.Count > 0 Then
                Dim collection As IList(Of RadComboBoxItem) = cboInvitees.CheckedItems
                If (collection.Count <> 0) Then
                    For Each item As RadComboBoxItem In collection
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Calendar_Event_Members_Update", txtid.Text, item.Value)
                    Next
                End If

                'If chkMail.Checked = True Then

                '    Dim maillist As String = ""
                '    Dim maillist1 As String = ""
                '    Dim loops As Integer = 1
                '    Dim colls As IList(Of RadComboBoxItem) = cboInvitees.CheckedItems
                '    If (colls.Count <> 0) Then
                '        For Each item As RadComboBoxItem In colls                          
                '            If loops = 1 Then
                '                maillist = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "Select a.Email from dbo.Employees_All a where EmpID = '" & item.Value & "' ")
                '                maillist1 = maillist
                '            Else
                '                maillist = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "Select a.Email from dbo.Employees_All a where EmpID = '" & item.Value & "' ")
                '                maillist1 = maillist1 & ";" & maillist
                '            End If

                '            loops = loops + 1                            
                '        Next
                '        'Process.Payroll_Notification(maillist, PayrollPeriod, datecreated, netamount)
                '    End If
                'End If
            End If
            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Calendar_Event_Members_delete", txtid.Text)



            lblstatus = "Record saved"
            Process.loadalert(divalert, msgalert, lblstatus, "success")
            txtname.Value = ""
            txtDesc.Value = ""

            'Response.Write("<script language='javascript'> { self.close() }</script>")
        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "danger")
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect("~/home.aspx", True)
        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "danger")
        End Try
    End Sub

    Protected Sub cboInvitees_ItemChecked(sender As Object, e As Telerik.Web.UI.RadComboBoxItemEventArgs) Handles cboInvitees.ItemChecked
        Try
            Process.LoadListBoxFromCombo(lstInvitees, cboInvitees)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

End Class