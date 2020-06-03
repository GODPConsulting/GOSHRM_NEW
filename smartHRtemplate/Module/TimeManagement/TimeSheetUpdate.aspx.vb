Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Microsoft.VisualBasic


Public Class TimeSheetUpdate
    Inherits System.Web.UI.Page
    Dim TimeSheet As New clsTimeSheet
    Dim ApproveLeave As New clsApproveLeave
    Dim AuthenCode As String = "TIMESHEET"
    Dim AuthenCode2 As String = "EMPTIMESHEET"
    Dim olddata(11) As String
    Dim emp_emailaddr As String
    Dim approver1_emailaddr As String
    Dim approver2_emailaddr As String
    Dim LeaveBalance As Integer = 0
    Dim NoDays As Integer = 0
    Dim Level1(2) As String
    Dim Level2(2) As String
    Dim Separators() As Char = {":"c}
    Dim EmpID_1 As String = ""
    Dim EmpID_2 As String = ""
    Dim EmpID_1_Name As String = ""
    Dim EmpID_2_Name As String = ""
    Dim isEligible As String = "Yes"
   
  
 
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            '

            If Not Me.IsPostBack Then
                rdoStatus.Items.Clear()           
                rdoStatus.Items.Add("Pending")
                rdoStatus.Items.Add("Approved")
                rdoStatus.Items.Add("Reject")


                rdoHRStatus.Items.Clear()
                rdoHRStatus.Items.Add("Pending")
                rdoHRStatus.Items.Add("Approved")
                rdoHRStatus.Items.Add("Reject")


                Process.LoadRadDropDownTextAndValueP1(radProject, "Time_Projects_Get_My", Session("UserEmpID"), "name", "id", False)

                'Company_Structure_get_parent
                If Request.QueryString("id") IsNot Nothing Then

                    
                    btnAdd.Visible = False

                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Time_Sheet_Get", Request.QueryString("id"))

                    'If Session("UserEmpID") = strUser.Tables(0).Rows(0).Item("Approver").ToString Or Process.IsHRManager(Session("UserEmpID")) Then
                    Process.LoadRadDropDownTextAndValueP1(radProject, "Time_Projects_Get", strUser.Tables(0).Rows(0).Item("projectid").ToString, "name", "id", False)
                    'End If

                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    lblDays.Text = strUser.Tables(0).Rows(0).Item("WorkDuration").ToString
                    lblDays0.Text = "for " & strUser.Tables(0).Rows(0).Item("EmployeeName").ToString


                    radActivity.Enabled = False
                    txtActivity.Text = strUser.Tables(0).Rows(0).Item("Activity")
                    Process.AssignRadDropDownValue(radProject, strUser.Tables(0).Rows(0).Item("Project").ToString)
                    radProject.Enabled = False



                    Process.LoadRadDropDownTextAndValueP1(radActivity, "Time_Projects_Get_Activities", radProject.SelectedItem.Value, "Activity", "Activity")
                    Process.AssignRadDropDownValue(radActivity, strUser.Tables(0).Rows(0).Item("Activity").ToString)

                    Process.LoadRadDropDownTextAndValueP1(radEmployee, "Emp_PersonalDetail_Get_Employees_In_Project", radProject.SelectedItem.Value, "Employee", "EmpID")
                    Process.AssignRadDropDownValue(radEmployee, strUser.Tables(0).Rows(0).Item("Employee").ToString)

                    radStartDate.SelectedDate = strUser.Tables(0).Rows(0).Item("ActivityDate").ToString
                    radEndDate.SelectedDate = strUser.Tables(0).Rows(0).Item("ActivityDate").ToString

                    Process.LoadTimeToRadCombo(radHourStart, radMinStart, radTimeStart, strUser.Tables(0).Rows(0).Item("starttime"))
                    Process.LoadTimeToRadCombo(radHourStart0, radMinStart0, radTimeStart0, strUser.Tables(0).Rows(0).Item("endtime"))

                    Process.RadioListCheck(rdoStatus, strUser.Tables(0).Rows(0).Item("Status").ToString)
                    Process.RadioListCheck(rdoHRStatus, strUser.Tables(0).Rows(0).Item("HRStatus").ToString)

                    txtNote.Text = strUser.Tables(0).Rows(0).Item("Note").ToString
                    txtComment.Text = strUser.Tables(0).Rows(0).Item("Comment").ToString
                    lblHRName.Text = strUser.Tables(0).Rows(0).Item("Name").ToString
                    txtHRComment.Text = strUser.Tables(0).Rows(0).Item("HRComment").ToString
                    lblPMName.Text = strUser.Tables(0).Rows(0).Item("PMManager").ToString
                    lblFinalStatus.Text = strUser.Tables(0).Rows(0).Item("FinalStatus").ToString

                    If Session("UserEmpID") = strUser.Tables(0).Rows(0).Item("Approver").ToString Then
                        btnStatus.Visible = True
                        btnAdd.Visible = False

                        rdoStatus.Enabled = True
                        txtComment.Enabled = True

                    Else
                        btnAdd.Visible = False
                        btnStatus.Visible = False

                        rdoStatus.Enabled = False
                        txtComment.Enabled = False
                    End If

                    If Process.IsHRManager(Session("UserEmpID")) Then
                        btnHR.Visible = True
                        txtHRComment.Enabled = True
                        rdoHRStatus.Enabled = True
                    Else
                        btnHR.Visible = False
                        txtHRComment.Enabled = False
                        rdoHRStatus.Enabled = False
                    End If
                    radEmployee.Enabled = False
                Else
                    'Process.LoadRadDropDownTextAndValueP1(radEmployee, "Emp_PersonalDetail_Get_Employees_In_Project", radProject.SelectedItem.Value, "Employee", "EmpID")
                    'txtEmployee.Text = Session("UserEmpID") & ":" & Session("EmpName")
                    btnStatus.Visible = False
                    btnHR.Visible = False
                    txtHRComment.Enabled = False
                    rdoHRStatus.Enabled = False
                    txtComment.Enabled = False
                    rdoStatus.Enabled = False

                    txtid.Text = "0"
                    radEmployee.Enabled = False
                    lblFinalStatus.Text = "Pending"
                    Process.RadioListCheck(rdoStatus, "Pending")
                    Process.RadioListCheck(rdoHRStatus, "Pending")
                End If

            End If
        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub
    Private Function GetIdentity(ByVal sEmpID As String, ByVal ProjectID As String, ByVal Activity As String, ByVal ActivityDate As Date, ActivityendDate As Date, ByVal starttime As String, ByVal enddate As String, ByVal duration As Integer, ByVal note As String, ByVal PM As String, ByVal user As String) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Time_Sheet_Update"
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@EmpID", SqlDbType.VarChar).Value = sEmpID
            cmd.Parameters.Add("@ProjectID", SqlDbType.Int).Value = ProjectID
            cmd.Parameters.Add("@Activity", SqlDbType.VarChar).Value = Activity
            cmd.Parameters.Add("@ActivityDate", SqlDbType.Date).Value = ActivityDate
            cmd.Parameters.Add("@ActivityEndDate", SqlDbType.Date).Value = ActivityendDate
            cmd.Parameters.Add("@StartTime", SqlDbType.VarChar).Value = starttime
            cmd.Parameters.Add("@EndTime", SqlDbType.VarChar).Value = enddate
            cmd.Parameters.Add("@WorkDuration", SqlDbType.VarChar).Value = duration
            cmd.Parameters.Add("@Note", SqlDbType.VarChar).Value = note
            cmd.Parameters.Add("@PM", SqlDbType.VarChar).Value = PM
            cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = user
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            If Request.QueryString("id") IsNot Nothing Then
                If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False And Process.AuthenAction(Session("role"), AuthenCode2, "Update") = False Then
                    ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
                    Exit Sub
                End If
            Else
                If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False And Process.AuthenAction(Session("role"), AuthenCode2, "Create") = False Then
                    ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
                    Exit Sub
                End If
            End If

            If lblFinalStatus.Text.ToUpper = "APPROVED" Then
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Timesheet has been approved by PM and HR, changes cannot be made!" + "')", True)
                lblstatus.Text = "Timesheet has been approved by PM and HR, changes cannot be made!"
                Exit Sub
            End If

            If Request.QueryString("id") IsNot Nothing Then
                If radEmployee.SelectedItem.Text.Contains(Session("UserEmpID")) Then
                    lblstatus.Text = "Time Sheet has already been submitted for Approval"
                    Exit Sub
                End If
            End If

            lblstatus.Text = "Record saving, please wait ..."

            If (txtActivity.Text Is Nothing) Then
                lblstatus.Text = "Activity required!"
                txtActivity.Focus()
                Exit Sub
            End If

            If (radStartDate.SelectedDate Is Nothing) Then
                lblstatus.Text = "Activity Start Date required!"
                radStartDate.Focus()
                Exit Sub
            End If

            If (radEndDate.SelectedDate Is Nothing) Then
                lblstatus.Text = "Activity End Date required!"
                radEndDate.Focus()
                Exit Sub
            End If



            If CDate(Process.ConvertCtrlToTime(radHourStart0.SelectedValue, radMinStart0.SelectedValue, radTimeStart0.SelectedValue)).TimeOfDay < CDate(Process.ConvertCtrlToTime(radHourStart.SelectedValue, radMinStart.SelectedValue, radTimeStart.SelectedValue)).TimeOfDay Then
                lblstatus.Text = "End Time can not be time ahead of Start Time!"

                Exit Sub
            End If

            If radStartDate.SelectedDate > Now.Date Then
                lblstatus.Text = "Setting future timesheet not allowed!"
                radStartDate.Focus()
                Exit Sub
            End If

            If radStartDate.SelectedDate > radEndDate.SelectedDate Then
                lblstatus.Text = "Start Date cannot be more than End date!"
                radStartDate.Focus()
                Exit Sub
            End If

            If radEndDate.SelectedDate > Now.Date Then
                lblstatus.Text = "Setting future timesheet not allowed!"
                radEndDate.Focus()
                Exit Sub
            End If


            TimeSheet.id = txtid.Text
            TimeSheet.Activity = txtActivity.Text
            TimeSheet.ActivityDate = radStartDate.SelectedDate
            TimeSheet.ActivityEndDate = radEndDate.SelectedDate
            TimeSheet.Duration = lblDays.Text
            TimeSheet.Employee = radEmployee.SelectedItem.Value
            TimeSheet.EndTime = Process.ConvertCtrlToTime(radHourStart0.SelectedValue, radMinStart0.SelectedValue, radTimeStart0.SelectedValue)
            TimeSheet.StartTime = Process.ConvertCtrlToTime(radHourStart.SelectedValue, radMinStart.SelectedValue, radTimeStart.SelectedValue)
            TimeSheet.Note = txtNote.Text
            TimeSheet.Project = radProject.SelectedItem.Text




            If txtid.Text <> "0" And txtid.Text.Trim <> "" Then
                Dim strUser As New DataSet
                strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Time_Sheet_Get", txtid.Text)
                olddata(0) = strUser.Tables(0).Rows(0).Item("id").ToString

                Level1 = strUser.Tables(0).Rows(0).Item("Employee").ToString.Split(Separators, StringSplitOptions.RemoveEmptyEntries)
                olddata(1) = Level1(0).ToString.Trim

                olddata(2) = strUser.Tables(0).Rows(0).Item("Project").ToString

                olddata(3) = strUser.Tables(0).Rows(0).Item("Activity").ToString
                olddata(4) = strUser.Tables(0).Rows(0).Item("ActivityDate").ToString
                olddata(5) = strUser.Tables(0).Rows(0).Item("ActivityEndDate").ToString
                olddata(6) = strUser.Tables(0).Rows(0).Item("StartTime").ToString
                olddata(7) = strUser.Tables(0).Rows(0).Item("EndTime").ToString
                olddata(8) = strUser.Tables(0).Rows(0).Item("WorkDuration").ToString
                olddata(9) = strUser.Tables(0).Rows(0).Item("Note").ToString
                olddata(10) = strUser.Tables(0).Rows(0).Item("Status").ToString
            End If


            Dim OldValue As String = ""
            Dim NewValue As String = ""

            Dim j As Integer = 0


            If txtid.Text <> "0" And txtid.Text.Trim <> "" Then 'Updates
                For Each a In GetType(clsTimeSheet).GetProperties()
                    If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" And a.Name.ToLower <> "status" Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If IsNumeric(a.GetValue(TimeSheet, Nothing)) = True And IsNumeric(olddata(j)) = True Then
                                If CDbl(a.GetValue(TimeSheet, Nothing)) <> CDbl(olddata(j)) Then
                                    NewValue += a.Name + ": " + a.GetValue(TimeSheet, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            Else
                                If a.GetValue(TimeSheet, Nothing).ToString <> olddata(j).ToString Then
                                    NewValue += a.Name + ": " + a.GetValue(TimeSheet, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            End If
                        End If
                    End If
                    j = j + 1
                Next
            Else
                For Each a In GetType(clsTimeSheet).GetProperties() 'New Entries
                    If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" And a.Name.ToLower <> "status" Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If a.GetValue(TimeSheet, Nothing) = Nothing Then
                                NewValue += a.Name + ":" + " " & vbCrLf
                            Else
                                NewValue += a.Name + ": " + a.GetValue(TimeSheet, Nothing).ToString & vbCrLf
                            End If
                        End If
                    End If
                Next
            End If

            btnAdd.Enabled = False

            'Get PM Info
            Dim strPM As New DataSet
            strPM = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Time_Projects_Get", radProject.SelectedItem.Value)
            Dim pmEmail As String = strPM.Tables(0).Rows(0).Item("PMEmail").ToString
            Dim pmName As String = strPM.Tables(0).Rows(0).Item("PMName").ToString
            Dim clientname As String = strPM.Tables(0).Rows(0).Item("clientname").ToString
            Dim projectname As String = strPM.Tables(0).Rows(0).Item("name").ToString
            Dim pmID As String = strPM.Tables(0).Rows(0).Item("ProjectMgrID").ToString

            If txtid.Text <> "0" And txtid.Text.Trim <> "" Then
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Time_Sheet_Update", TimeSheet.id, TimeSheet.Employee, radProject.SelectedItem.Value, TimeSheet.Activity, TimeSheet.ActivityDate, TimeSheet.ActivityEndDate, TimeSheet.StartTime, TimeSheet.EndTime, TimeSheet.Duration, TimeSheet.Note, pmID, Session("LoginID"))
            Else
                txtid.Text = GetIdentity(TimeSheet.Employee, radProject.SelectedItem.Value, TimeSheet.Activity, TimeSheet.ActivityDate, TimeSheet.ActivityEndDate, TimeSheet.StartTime, TimeSheet.EndTime, TimeSheet.Duration, TimeSheet.Note, pmID, Session("LoginID"))
            End If


            Process.PM_TimeSheet_Approval(pmEmail, pmName, projectname, clientname, radEmployee.SelectedItem.Text, radStartDate.SelectedDate, radEndDate.SelectedDate, Process.AMPM_Time(Process.ConvertCtrlToTime(radHourStart.SelectedValue, radMinStart.SelectedValue, radTimeStart.SelectedValue)), Process.AMPM_Time(Process.ConvertCtrlToTime(radHourStart0.SelectedValue, radMinStart0.SelectedValue, radTimeStart0.SelectedValue)), radEmployee.SelectedValue, pmID)

            If NewValue.Trim = "" And OldValue.Trim = "" Then

            Else
                If txtid.Text <> "0" And txtid.Text.Trim <> "" Then
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Updated", "Time Sheet")
                Else
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Inserted", "Time Sheet")
                End If
            End If
            lblstatus.Text = "Record saved and submitted for approval"

        Catch ex As Exception
            lblstatus.Text = ex.Message
        Finally
            btnAdd.Enabled = True
        End Try
    End Sub


    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Response.Redirect("TimeSheet.aspx")
        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub



    Protected Sub radStartDate_SelectedDateChanged(sender As Object, e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles radStartDate.SelectedDateChanged
        Try
            If radEndDate.SelectedDate IsNot Nothing Then
                Dim Date1 As Date = radStartDate.SelectedDate
                Dim Date2 As Date = radEndDate.SelectedDate
                Dim NoDays As Integer = DateDiff(DateInterval.Day, Date1, Date2) + 1
                Dim Duration As Integer = 0
                Duration = NoDays * (CDate(Process.ConvertCtrlToTime(radHourStart0.SelectedValue, radMinStart0.SelectedValue, radTimeStart0.SelectedValue)).TimeOfDay.TotalHours - CDate(Process.ConvertCtrlToTime(radHourStart.SelectedValue, radMinStart.SelectedValue, radTimeStart.SelectedValue)).TimeOfDay.TotalHours)
                If Duration < 0 Then
                    lblDays.Text = 0
                Else
                    lblDays.Text = Duration
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub




    Protected Sub btnStatus_Click(sender As Object, e As EventArgs) Handles btnStatus.Click
        Try


            'If Request.QueryString("id") IsNot Nothing Then
            '    If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False Then
            '        ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
            '        Exit Sub
            '    End If
            'Else
            '    If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
            '        ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
            '        Exit Sub
            '    End If
            'End If

            lblstatus.Text = "Record saving, please wait ..."


            If (radStartDate.SelectedDate Is Nothing) Then
                lblstatus.Text = "Start Date required!"
                radStartDate.Focus()
                Exit Sub
            End If

            If rdoStatus.SelectedItem.Text.Contains("Reject") Then
                lblstatus.Text = "Comment on rejection is required!"
                txtComment.Focus()
                Exit Sub
            End If

            'If txtEmployee.Text.Trim <> "" Then
            '    Level1 = txtEmployee.Text.Split(Separators, StringSplitOptions.RemoveEmptyEntries)
            '    EmpID_1 = Level1(0).ToString.Trim
            'End If
            'Project
            'If txtProject.Text.Trim <> "" Then
            '    Level2 = txtProject.Text.Split(Separators, StringSplitOptions.RemoveEmptyEntries)
            '    EmpID_2 = Level2(0).ToString.Trim
            'End If

            TimeSheet.id = txtid.Text
            TimeSheet.Activity = txtActivity.Text
            TimeSheet.ActivityDate = radStartDate.SelectedDate
            TimeSheet.Duration = lblDays.Text
            TimeSheet.Employee = EmpID_1
            TimeSheet.EndTime = Process.ConvertCtrlToTime(radHourStart0.SelectedValue, radMinStart0.SelectedValue, radTimeStart0.SelectedValue)
            TimeSheet.StartTime = Process.ConvertCtrlToTime(radHourStart.SelectedValue, radMinStart.SelectedValue, radTimeStart.SelectedValue)
            TimeSheet.Note = txtNote.Text
            TimeSheet.Project = EmpID_2


            For i = 0 To rdoStatus.Items.Count - 1
                If rdoStatus.Items(i).Selected Then
                    TimeSheet.Status = rdoStatus.Items(i).Value
                    Exit For
                End If
            Next

            'If Request.QueryString("id") IsNot Nothing Then
            '    Dim strUser As New DataSet
            '    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Time_Sheet_Get", Request.QueryString("id"))
            '    olddata(0) = strUser.Tables(0).Rows(0).Item("id").ToString

            '    Level1 = strUser.Tables(0).Rows(0).Item("Employee").ToString.Split(Separators, StringSplitOptions.RemoveEmptyEntries)
            '    olddata(1) = Level1(0).ToString.Trim

            '    Level2 = strUser.Tables(0).Rows(0).Item("Project").ToString.Split(Separators, StringSplitOptions.RemoveEmptyEntries)
            '    olddata(2) = Level2(0).ToString.Trim

            '    olddata(3) = strUser.Tables(0).Rows(0).Item("Activity").ToString
            '    olddata(4) = strUser.Tables(0).Rows(0).Item("ActivityDate").ToString
            '    olddata(5) = strUser.Tables(0).Rows(0).Item("StartTime").ToString
            '    olddata(6) = strUser.Tables(0).Rows(0).Item("EndTime").ToString
            '    olddata(7) = strUser.Tables(0).Rows(0).Item("WorkDuration").ToString
            '    olddata(8) = strUser.Tables(0).Rows(0).Item("Note").ToString
            '    olddata(9) = strUser.Tables(0).Rows(0).Item("Status").ToString
            'End If

            Dim OldValue As String = ""
            Dim NewValue As String = ""

            Dim j As Integer = 0


            'If Request.QueryString("id") IsNot Nothing Then 'Updates
            '    For Each a In GetType(clsTimeSheet).GetProperties()
            '        If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
            '            If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
            '                If IsNumeric(a.GetValue(TimeSheet, Nothing)) = True And IsNumeric(olddata(j)) = True Then
            '                    If CDbl(a.GetValue(TimeSheet, Nothing)) <> CDbl(olddata(j)) Then
            '                        NewValue += a.Name + ": " + a.GetValue(TimeSheet, Nothing).ToString & vbCrLf
            '                        OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
            '                    End If
            '                Else
            '                    If a.GetValue(TimeSheet, Nothing).ToString <> olddata(j).ToString Then
            '                        NewValue += a.Name + ": " + a.GetValue(TimeSheet, Nothing).ToString & vbCrLf
            '                        OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
            '                    End If
            '                End If
            '            End If
            '        End If
            '        j = j + 1
            '    Next
            'Else
            '    For Each a In GetType(clsTimeSheet).GetProperties() 'New Entries
            '        If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
            '            If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
            '                If a.GetValue(TimeSheet, Nothing) = Nothing Then
            '                    NewValue += a.Name + ":" + " " & vbCrLf
            '                Else
            '                    NewValue += a.Name + ": " + a.GetValue(TimeSheet, Nothing).ToString & vbCrLf
            '                End If
            '            End If
            '        End If
            '    Next
            'End If

            btnStatus.Enabled = False
            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Time_Sheet_Update_Status", TimeSheet.id, TimeSheet.StartTime, TimeSheet.EndTime, TimeSheet.Duration, TimeSheet.Note, TimeSheet.Status, txtComment.Text, Session("UserEmpID"))

            'Get PM Info
            Dim strPM As New DataSet
            strPM = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Time_Projects_Get", radProject.SelectedItem.Value)
            Dim pmEmail As String = strPM.Tables(0).Rows(0).Item("PMEmail").ToString
            Dim pmName As String = strPM.Tables(0).Rows(0).Item("PMName").ToString
            Dim clientname As String = strPM.Tables(0).Rows(0).Item("clientname").ToString
            Dim projectname As String = strPM.Tables(0).Rows(0).Item("name").ToString
            Dim pmID As String = strPM.Tables(0).Rows(0).Item("ProjectMgrID").ToString
            Dim company As String = strPM.Tables(0).Rows(0).Item("CompanyName").ToString


            'Mail HR Managers
            Dim strHR As New DataSet
            strHR = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_PersonalDetail_Get_HR_Staff", company)
            For hrcount As Integer = 0 To strHR.Tables(0).Rows.Count - 1
                Dim hrEmail As String = strHR.Tables(0).Rows(hrcount).Item("Email").ToString
                Process.HR_TimeSheet_Approval(hrEmail, pmName, projectname, clientname, radEmployee.SelectedItem.Text, radStartDate.SelectedDate, Process.AMPM_Time(Process.ConvertCtrlToTime(radHourStart.SelectedValue, radMinStart.SelectedValue, radTimeStart.SelectedValue)), Process.AMPM_Time(Process.ConvertCtrlToTime(radHourStart0.SelectedValue, radMinStart0.SelectedValue, radTimeStart0.SelectedValue)), rdoStatus.SelectedItem.Text, txtComment.Text, radEmployee.SelectedValue, pmID)
            Next


            'If NewValue.Trim = "" And OldValue.Trim = "" Then
            'Else
            '    If Request.QueryString("id") IsNot Nothing Then
            '        Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Updated ID: " & TimeSheet.id, "Time Sheet Submit")
            '    Else
            '        Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Inserted", "Time Sheet Submit")
            '    End If
            'End If

            lblstatus.Text = "Record saved, Approval Status updated"
            'Response.Write("<script language='javascript'> { self.close() }</script>")
        Catch ex As Exception
            lblstatus.Text = ex.Message
        Finally
            btnStatus.Enabled = True
        End Try
    End Sub





    Protected Sub radEmployee_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.DropDownListEventArgs) Handles radEmployee.SelectedIndexChanged
        Try
            'txtEmployee.Text = radEmployee.SelectedText
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub radActivity_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.DropDownListEventArgs) Handles radActivity.SelectedIndexChanged
        Try
            txtActivity.Text = radActivity.SelectedText
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub radProject_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.DropDownListEventArgs) Handles radProject.SelectedIndexChanged
        Try
            'txtProject.Text = radProject.SelectedText
            Process.LoadRadDropDownTextAndValueP1(radEmployee, "Emp_PersonalDetail_Get_Employees_In_Project", radProject.SelectedItem.Value, "Employee", "EmpID")
            Process.LoadRadDropDownTextAndValueP1(radActivity, "Time_Projects_Get_Activities", radProject.SelectedItem.Value, "Activity", "Activity")
            Dim strEmp As New DataSet
            strEmp = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "select employee2  employee from dbo.Employees_All where empid = '" & Session("UserEmpID") & "'")
            If strEmp.Tables(0).Rows.Count > 0 Then
                Process.AssignRadDropDownValue(radEmployee, strEmp.Tables(0).Rows(0).Item("Employee").ToString)
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnHR_Click(sender As Object, e As EventArgs) Handles btnHR.Click
        Try
            If rdoStatus.SelectedItem.Text.Contains("Pending") Then
                lblstatus.Text = "Project Manager yet to update approval status, update is cancelled"
                Exit Sub
            End If

            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Time_Sheet_HR_Update", txtid.Text, Session("UserEmpID"), rdoHRStatus.SelectedItem.Text, txtHRComment.Text.Trim)
            lblstatus.Text = "Record saved, HR Approval Status updated"
        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub

    Protected Sub radEndDate_SelectedDateChanged(sender As Object, e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles radEndDate.SelectedDateChanged
        Try
            If radStartDate.SelectedDate IsNot Nothing Then
                Dim Date1 As Date = radStartDate.SelectedDate
                Dim Date2 As Date = radEndDate.SelectedDate
                Dim NoDays As Integer = DateDiff(DateInterval.Day, Date1, Date2) + 1
                Dim Duration As Integer = 0
                Duration = NoDays * (CDate(Process.ConvertCtrlToTime(radHourStart0.SelectedValue, radMinStart0.SelectedValue, radTimeStart0.SelectedValue)).TimeOfDay.TotalHours - CDate(Process.ConvertCtrlToTime(radHourStart.SelectedValue, radMinStart.SelectedValue, radTimeStart.SelectedValue)).TimeOfDay.TotalHours)
                If Duration < 0 Then
                    lblDays.Text = 0
                Else
                    lblDays.Text = Duration
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub radHourStart_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles radHourStart.SelectedIndexChanged
        Try
            If radStartDate.SelectedDate IsNot Nothing And radEndDate.SelectedDate IsNot Nothing Then
                Dim Date1 As Date = radStartDate.SelectedDate
                Dim Date2 As Date = radEndDate.SelectedDate
                Dim NoDays As Integer = DateDiff(DateInterval.Day, Date1, Date2) + 1
                Dim Duration As Integer = 0
                Duration = NoDays * (CDate(Process.ConvertCtrlToTime(radHourStart0.SelectedValue, radMinStart0.SelectedValue, radTimeStart0.SelectedValue)).TimeOfDay.TotalHours - CDate(Process.ConvertCtrlToTime(radHourStart.SelectedValue, radMinStart.SelectedValue, radTimeStart.SelectedValue)).TimeOfDay.TotalHours)
                If Duration < 0 Then
                    lblDays.Text = 0
                Else
                    lblDays.Text = NoDays * (CDate(Process.ConvertCtrlToTime(radHourStart0.SelectedValue, radMinStart0.SelectedValue, radTimeStart0.SelectedValue)).TimeOfDay.TotalHours - CDate(Process.ConvertCtrlToTime(radHourStart.SelectedValue, radMinStart.SelectedValue, radTimeStart.SelectedValue)).TimeOfDay.TotalHours)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub radMinStart_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles radMinStart.SelectedIndexChanged
        Try
            If radStartDate.SelectedDate IsNot Nothing And radEndDate.SelectedDate IsNot Nothing Then
                Dim Date1 As Date = radStartDate.SelectedDate
                Dim Date2 As Date = radEndDate.SelectedDate
                Dim NoDays As Integer = DateDiff(DateInterval.Day, Date1, Date2) + 1
                Dim Duration As Integer = 0
                Duration = NoDays * (CDate(Process.ConvertCtrlToTime(radHourStart0.SelectedValue, radMinStart0.SelectedValue, radTimeStart0.SelectedValue)).TimeOfDay.TotalHours - CDate(Process.ConvertCtrlToTime(radHourStart.SelectedValue, radMinStart.SelectedValue, radTimeStart.SelectedValue)).TimeOfDay.TotalHours)
                If Duration < 0 Then
                    lblDays.Text = 0
                Else
                    lblDays.Text = Duration
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub radTimeStart_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles radTimeStart.SelectedIndexChanged
        Try
            If radStartDate.SelectedDate IsNot Nothing And radEndDate.SelectedDate IsNot Nothing Then
                Dim Date1 As Date = radStartDate.SelectedDate
                Dim Date2 As Date = radEndDate.SelectedDate
                Dim NoDays As Integer = DateDiff(DateInterval.Day, Date1, Date2) + 1
                Dim Duration As Integer = 0
                Duration = NoDays * (CDate(Process.ConvertCtrlToTime(radHourStart0.SelectedValue, radMinStart0.SelectedValue, radTimeStart0.SelectedValue)).TimeOfDay.TotalHours - CDate(Process.ConvertCtrlToTime(radHourStart.SelectedValue, radMinStart.SelectedValue, radTimeStart.SelectedValue)).TimeOfDay.TotalHours)
                If Duration < 0 Then
                    lblDays.Text = 0
                Else
                    lblDays.Text = Duration
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub radHourStart0_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles radHourStart0.SelectedIndexChanged
        Try
            If radStartDate.SelectedDate IsNot Nothing And radEndDate.SelectedDate IsNot Nothing Then
                Dim Date1 As Date = radStartDate.SelectedDate
                Dim Date2 As Date = radEndDate.SelectedDate
                Dim NoDays As Integer = DateDiff(DateInterval.Day, Date1, Date2) + 1
                Dim Duration As Integer = 0
                Duration = NoDays * (CDate(Process.ConvertCtrlToTime(radHourStart0.SelectedValue, radMinStart0.SelectedValue, radTimeStart0.SelectedValue)).TimeOfDay.TotalHours - CDate(Process.ConvertCtrlToTime(radHourStart.SelectedValue, radMinStart.SelectedValue, radTimeStart.SelectedValue)).TimeOfDay.TotalHours)
                If Duration < 0 Then
                    lblDays.Text = 0
                Else
                    lblDays.Text = Duration
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub radMinStart0_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles radMinStart0.SelectedIndexChanged
        Try
            If radStartDate.SelectedDate IsNot Nothing And radEndDate.SelectedDate IsNot Nothing Then
                Dim Date1 As Date = radStartDate.SelectedDate
                Dim Date2 As Date = radEndDate.SelectedDate
                Dim NoDays As Integer = DateDiff(DateInterval.Day, Date1, Date2) + 1
                Dim Duration As Integer = 0
                Duration = NoDays * (CDate(Process.ConvertCtrlToTime(radHourStart0.SelectedValue, radMinStart0.SelectedValue, radTimeStart0.SelectedValue)).TimeOfDay.TotalHours - CDate(Process.ConvertCtrlToTime(radHourStart.SelectedValue, radMinStart.SelectedValue, radTimeStart.SelectedValue)).TimeOfDay.TotalHours)
                If Duration < 0 Then
                    lblDays.Text = 0
                Else
                    lblDays.Text = Duration
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub radTimeStart0_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles radTimeStart0.SelectedIndexChanged
        Try
            If radStartDate.SelectedDate IsNot Nothing And radEndDate.SelectedDate IsNot Nothing Then
                Dim Date1 As Date = radStartDate.SelectedDate
                Dim Date2 As Date = radEndDate.SelectedDate
                Dim NoDays As Integer = DateDiff(DateInterval.Day, Date1, Date2) + 1
                Dim Duration As Integer = 0
                Duration = NoDays * (CDate(Process.ConvertCtrlToTime(radHourStart0.SelectedValue, radMinStart0.SelectedValue, radTimeStart0.SelectedValue)).TimeOfDay.TotalHours - CDate(Process.ConvertCtrlToTime(radHourStart.SelectedValue, radMinStart.SelectedValue, radTimeStart.SelectedValue)).TimeOfDay.TotalHours)
                If Duration < 0 Then
                    lblDays.Text = 0
                Else
                    lblDays.Text = Duration
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class