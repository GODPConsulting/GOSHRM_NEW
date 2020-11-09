Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Telerik.Web.UI


Public Class TrainingSessionsUpdate
    Inherits System.Web.UI.Page
    Dim trainsession As New clsTrainSession
    Dim AuthenCode As String = "TRAINSESSION"
    Dim olddata(14) As String
    Dim oFileAttached1 As Byte
    Dim oFileAttached As Byte()
    Dim EmpID As String = ""
    Dim Level(2) As String
    Dim HREmpID As String = ""
    Dim HRLevel(2) As String
    Dim Separators() As Char = {":"c}
    Dim TrainersList As New StringBuilder()
    Private Sub LoadDept()
        Try
            If Request.QueryString("id") IsNot Nothing Then
                Dim strUser As New DataSet
                strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Training_Sessions_get", Request.QueryString("id"))
                Dim ncom As String = strUser.Tables(0).Rows(0).Item("company").ToString
                Dim grades As String = ""
                Dim counter As Integer = 0
                radDept.DataFieldID = "ID"
                radDept.DataFieldParentID = "ParentID"
                radDept.DataValueField = "Value"
                radDept.DataTextField = "Text"
                radDept.DataSource = Process.GetUnitsData(ncom, grades)
                radDept.DataBind()
            Else
                Dim grades As String = ""
                Dim counter As Integer = 0
                radDept.DataFieldID = "ID"
                radDept.DataFieldParentID = "ParentID"
                radDept.DataValueField = "Value"
                radDept.DataTextField = "Text"
                radDept.DataSource = Process.GetUnitsData(cboCompany.SelectedValue, grades)
                radDept.DataBind()
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                drpCategory.Items.Clear()
                drpCategory.Items.Add("Mandatory")
                drpCategory.Items.Add("Optional")
                drpCategory.Items.Add("Sponsored")

                Dim ismulti As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select isnull(ismulticompany,'No')  from general_info")
                If ismulti.ToLower = "no" Then
                    Process.LoadRadComboTextAndValueP2(cboCompany, "Company_Structure_Get_ByLevel", "1", Session("Access"), "name", "name", False)
                Else
                    Process.LoadRadComboTextAndValueP2(cboCompany, "Company_Structure_Get_ByLevel", "2", Session("Access"), "name", "name", False)
                End If
                Process.AssignRadComboValue(cboCompany, Session("Organisation"))
                radSessionType.Items.Clear()
                radSessionType.Items.Add("External")
                radSessionType.Items.Add("Internal")

                Process.LoadRadComboTextAndValue(drpGrade, "Job_Grade_TreeView", "GradeName", "GradeName", False)
                LoadDept()



                Process.LoadRadComboTextAndValueP2(drpTrainee, "Emp_PersonalDetail_get_all_Specific", "", cboCompany.SelectedValue, "Employee2", "EmpID", False)
                Process.LoadRadComboTextAndValueP2(cbocoordinator, "Emp_PersonalDetail_get_all_Specific", "", cboCompany.SelectedValue, "Employee2", "EmpID", False)
                Process.LoadRadComboTextAndValueP2(cboTrainer, "Emp_PersonalDetail_get_all", "", Process.GetCompanyName, "Employee2", "EmpID", False)
                Process.LoadRadComboTextAndValue(cboCourse, "Courses_get_all", "Course Title", "id", False)

                Process.LoadRadDropDownTextAndValue(radCurrency, "Currency_Load_1", "Currency", "Code", False)
                'Process.LoadRadCombo(radHR, "Emp_PersonalDetail_Get_HR_Staff", 0)
                Dim strDataSet As New DataSet

                If Request.QueryString("id") IsNot Nothing Then
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Training_Sessions_get", Request.QueryString("id"))

                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    txtnote.InnerText = strUser.Tables(0).Rows(0).Item("comment").ToString
                    txtname.Text = strUser.Tables(0).Rows(0).Item("name").ToString
                    Process.AssignRadComboValue(cboCourse, strUser.Tables(0).Rows(0).Item("course").ToString)
                    Process.AssignRadComboValue(cboCompany, strUser.Tables(0).Rows(0).Item("company").ToString)
                    datApplicationAssessment.SelectedDate = strUser.Tables(0).Rows(0).Item("ApplicationAssessment_Date").ToString
                    radScheduleTime.SelectedDate = CDate(strUser.Tables(0).Rows(0).Item("scheduledtime"))
                    radDateDue.SelectedDate = strUser.Tables(0).Rows(0).Item("duedate").ToString
                    radDeliveryMethod.SelectedText = strUser.Tables(0).Rows(0).Item("deliverymethod").ToString
                    txtLocation.Value = strUser.Tables(0).Rows(0).Item("deliverylocation").ToString
                    Process.LoadTimeToRadCombo(radHourStart, radMinStart, radTimeStart, strUser.Tables(0).Rows(0).Item("trainingtime"))

                    Process.AssignRadDropDownValue(drpCategory, strUser.Tables(0).Rows(0).Item("category").ToString)

                    Process.AssignRadDropDownValue(radCurrency, strUser.Tables(0).Rows(0).Item("Currency").ToString)
                    txtCost.Text = strUser.Tables(0).Rows(0).Item("Cost").ToString
                    radSessionType.SelectedText = strUser.Tables(0).Rows(0).Item("TrainingType").ToString
                    'Process.AssignRadComboValue(cbocoordinator, strUser.Tables(0).Rows(0).Item("coordinator").ToString)
                    txtAttendee.Text = strUser.Tables(0).Rows(0).Item("availabletrainness").ToString 'availabletrainness

                    Dim ncoordID As String = strUser.Tables(0).Rows(0).Item("coordinator").ToString
                    'Dim squery As String = "select name from dbo.employees_all a where a.empid = " + ncoordID + ""
                    'Dim newcoord As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, squery)
                    Dim ncom As String = strUser.Tables(0).Rows(0).Item("company").ToString
                    Process.LoadRadComboTextAndValueP2(drpTrainee, "Emp_PersonalDetail_get_all_Specific", "", ncom, "Employee2", "EmpID", False)
                    Process.LoadRadComboTextAndValueP2(cbocoordinator, "Emp_PersonalDetail_get_all_Specific", "", ncom, "Employee2", "EmpID", False)
                    Process.AssignRadComboValue2(cbocoordinator, ncoordID, strUser.Tables(0).Rows(0).Item("coordinator").ToString)

                    If radSessionType.SelectedText.ToLower = "internal" Then
                        'Get Trainees
                        txtTrainer.Visible = False
                        Process.LoadListAndComboxFromDataset(lstTrainer, cboTrainer, "Training_Session_Get_Trainers", "Trainers", "EmpiD", txtid.Text)
                        If lstTrainer.Items.Count < 1 Then
                            lstTrainer.Visible = False
                        Else
                            lstTrainer.Visible = True
                        End If
                    Else
                        txtTrainer.Visible = True
                        cboTrainer.Visible = False
                        lstTrainer.Visible = False

                        Dim strTrainer As New DataSet
                        strTrainer = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Training_Session_Get_Trainers", txtid.Text)
                        If strTrainer.Tables(0).Rows.Count > 0 Then
                            txtTrainer.Value = strTrainer.Tables(0).Rows(0).Item("Trainers").ToString
                        End If
                    End If
                    cboCompany.Enabled = False
                    cboCourse.Enabled = False

                    'ComputeCost()
                    Process.LoadListAndComboxFromDataset(lstTrainee, drpTrainee, "Training_Session_Get_Trainees", "Employee2", "EmpiD", txtid.Text)
                    Dim ll As New RadListBox
                    Process.LoadListAndComboxFromDataset(ll, drpGrade, "Training_Session_JobGrade_Get_All", "jobgrade", "jobgrade", txtid.Text)
                    Process.LoadRadTree(radDept, "Training_Session_Dept_Get_All", "office", "office", txtid.Text)
                Else
                    txtid.Text = "0"
                    txtAttendee.Text = "1"
                    txtCost.Text = "0"
                End If
              
             
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    Private Function GetIdentity(ByVal sname As String, ByVal course As String, ByVal detail As String,
                                 ByVal scheduledtime As Date, ByVal dueddate As Date, ByVal deliverymethod As String,
                                  ByVal deliverylocation As String, ByVal attendancetype As String, ByVal status As String, ByVal hr As String,
                                  ByVal coordinator As String, ByVal sType As String, ByVal currency As String,
                                  ByVal cost As Double, ByVal hod As Boolean, ByVal coach As Boolean, ByVal requireshr As String,
                                  ByVal stime As String, ByVal appassessmentdate As Date) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Training_Sessions_update"
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = sname
            cmd.Parameters.Add("@course", SqlDbType.VarChar).Value = course
            cmd.Parameters.Add("@detail", SqlDbType.VarChar).Value = detail
            cmd.Parameters.Add("@ScheduledTime", SqlDbType.Date).Value = scheduledtime
            cmd.Parameters.Add("@DueDate", SqlDbType.Date).Value = dueddate
            cmd.Parameters.Add("@DeliveryMethod", SqlDbType.VarChar).Value = deliverymethod
            cmd.Parameters.Add("@DeliveryLocation", SqlDbType.VarChar).Value = deliverylocation
            cmd.Parameters.Add("@AttendanceType", SqlDbType.VarChar).Value = attendancetype
            cmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = status
            cmd.Parameters.Add("@HR", SqlDbType.VarChar).Value = hr
            cmd.Parameters.Add("@coordinator", SqlDbType.VarChar).Value = coordinator
            cmd.Parameters.Add("@Type", SqlDbType.VarChar).Value = sType
            cmd.Parameters.Add("@Currency", SqlDbType.VarChar).Value = currency
            cmd.Parameters.Add("@Cost", SqlDbType.Decimal).Value = cost
            cmd.Parameters.Add("@HOD", SqlDbType.Bit).Value = hod
            cmd.Parameters.Add("@Coach", SqlDbType.Bit).Value = coach
            cmd.Parameters.Add("@RequiresHRApproval", SqlDbType.VarChar).Value = requireshr
            cmd.Parameters.Add("@time", SqlDbType.VarChar).Value = stime
            cmd.Parameters.Add("@availabletrainness", SqlDbType.VarChar).Value = txtAttendee.Text
            cmd.Parameters.Add("@company", SqlDbType.VarChar).Value = cboCompany.SelectedValue
            cmd.Parameters.Add("@category", SqlDbType.VarChar).Value = drpCategory.SelectedText
            cmd.Parameters.Add("@comment", SqlDbType.VarChar).Value = txtnote.Value.Trim
            cmd.Parameters.Add("@ApplicationAssessment_Date", SqlDbType.Date).Value = appassessmentdate
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception
            Response.Write(ex.Message)
            Return 0
        End Try
    End Function
    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            If txtid.Text <> "0" Then
                If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False Then
                    ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
                    Exit Sub
                End If
            End If
         
            Dim lblstatus As String = ""
            If (txtname.Text.Trim = "") Then
                lblstatus = "Training Session Name required!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                txtname.Focus()
                Exit Sub
            End If

            If (drpCategory.SelectedText.Trim = "" Or drpCategory.SelectedText.Trim.ToLower = "-- select --") Then
                lblstatus = "Category required!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                drpCategory.Focus()
                Exit Sub
            End If

            If (radScheduleTime.SelectedDate Is Nothing) Then
                lblstatus = "Schedule Date and Time required!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                radScheduleTime.Focus()
                Exit Sub
            End If

            If (radDateDue.SelectedDate Is Nothing) Then
                lblstatus = "Due Date required!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                radDateDue.Focus()
                Exit Sub
            End If

            If radScheduleTime.SelectedDate > radDateDue.SelectedDate Then
                lblstatus = "Due Date cannot be before Schedule Date required!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                radDateDue.Focus()
                Exit Sub
            End If

            If (datApplicationAssessment.SelectedDate Is Nothing) Then
                datApplicationAssessment.SelectedDate = radDateDue.SelectedDate
            End If

            If radScheduleTime.SelectedDate > datApplicationAssessment.SelectedDate Then
                lblstatus = "Application Assessment cannot be before Schedule Date required!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                datApplicationAssessment.Focus()
                Exit Sub
            End If

            If datApplicationAssessment.SelectedDate < radDateDue.SelectedDate Then
                lblstatus = "Application Assessment cannot be before Due Date required!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                datApplicationAssessment.Focus()
                Exit Sub
            End If

            If (radSessionType.SelectedText.Trim = "") Or (radSessionType.SelectedText.Trim = "--Select--") Then
                lblstatus = "Session Type required!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                radSessionType.Focus()
                Exit Sub
            End If

            If (radDeliveryMethod.SelectedText.Trim = "") Or (radDeliveryMethod.SelectedText.Trim = "--Select--") Then
                lblstatus = "Delivery Method required!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                radDeliveryMethod.Focus()
                Exit Sub
            End If

            If txtCost.Text Is Nothing Then
                txtCost.Text = 0
            End If

            If (radCurrency.SelectedText Is Nothing) Or (radCurrency.SelectedText.Trim = "--Select--") Then
                radCurrency.SelectedText = ""
            End If


            If IsNumeric(txtCost.Text) = False Then
                lblstatus = "Cost must be numeric!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                txtCost.Focus()
                Exit Sub
            End If

          
            If lstTrainee.Items.Count > CInt(txtAttendee.Text) Then
                lblstatus = "Available number of training slots is less than number of selected trainees!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                txtAttendee.Focus()
                Exit Sub
            End If

            lblstatus = "Saving record, please wait ...."
            'Old Data
            If txtid.Text <> "0" Then
                Dim strUser As New DataSet
                strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Training_Sessions_get", Request.QueryString("id"))
                olddata(0) = strUser.Tables(0).Rows(0).Item("id").ToString
                olddata(1) = strUser.Tables(0).Rows(0).Item("name").ToString
                olddata(2) = strUser.Tables(0).Rows(0).Item("course").ToString
                olddata(3) = strUser.Tables(0).Rows(0).Item("coordinator").ToString
                olddata(4) = strUser.Tables(0).Rows(0).Item("TrainingType").ToString
                olddata(5) = strUser.Tables(0).Rows(0).Item("scheduledtime").ToString
                olddata(6) = strUser.Tables(0).Rows(0).Item("trainingtime").ToString
                olddata(7) = strUser.Tables(0).Rows(0).Item("duedate").ToString
                olddata(8) = strUser.Tables(0).Rows(0).Item("deliverymethod").ToString
                olddata(9) = strUser.Tables(0).Rows(0).Item("deliverylocation").ToString
                olddata(10) = strUser.Tables(0).Rows(0).Item("attendancetype").ToString
                olddata(11) = strUser.Tables(0).Rows(0).Item("currency").ToString
                olddata(12) = strUser.Tables(0).Rows(0).Item("cost").ToString
                olddata(13) = strUser.Tables(0).Rows(0).Item("ApplicationAssessment_Date").ToString
            End If


            If txtid.Text.Trim = "0" Or txtid.Text.Trim = "" Then
                trainsession.id = 0
            Else
                trainsession.id = txtid.Text
            End If
            trainsession.Name = txtname.Text.Trim
            trainsession.Course = cboCourse.SelectedItem.Text
            'trainsession.Detail = txtDesc.Text
            trainsession.TrainingType = radSessionType.SelectedText
            trainsession.ScheduleDate = radScheduleTime.SelectedDate
            trainsession.DueDate = radDateDue.SelectedDate
            trainsession.DeliveryMethod = radDeliveryMethod.SelectedText
            trainsession.DeliveryLocation = txtLocation.Value
            trainsession.ScheduleTime = Process.ConvertCtrlToTime(radHourStart.SelectedValue, radMinStart.SelectedValue, radTimeStart.SelectedValue)
            trainsession.Currency = radCurrency.SelectedValue
            trainsession.Cost = txtCost.Text
            trainsession.Coordinator = cbocoordinator.SelectedValue
            trainsession.ApplicationAssessmentDate = datApplicationAssessment.SelectedDate
            Dim OldValue As String = ""
            Dim NewValue As String = ""

            Dim j As Integer = 0

            If txtid.Text <> "0" Then 'Updates
                For Each a In GetType(clsTrainSession).GetProperties()
                    If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" And a.Name.ToLower <> "attachement" And a.Name.ToLower <> "attachname" Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If IsNumeric(a.GetValue(trainsession, Nothing)) = True And IsNumeric(olddata(j)) = True Then
                                If CDbl(a.GetValue(trainsession, Nothing)) <> CDbl(olddata(j)) Then
                                    NewValue += a.Name + ": " + a.GetValue(trainsession, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            Else
                                If a.GetValue(trainsession, Nothing).ToString <> olddata(j).ToString Then
                                    NewValue += a.Name + ": " + a.GetValue(trainsession, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            End If
                        End If
                    End If
                    j = j + 1
                Next
            Else
                For Each a In GetType(clsTrainSession).GetProperties() 'New Entries
                    If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" And a.Name.ToLower <> "attachement" And a.Name.ToLower <> "attachname" Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If a.GetValue(trainsession, Nothing) = Nothing Then
                                NewValue += a.Name + ":" + " " & vbCrLf
                            Else
                                NewValue += a.Name + ": " + a.GetValue(trainsession, Nothing).ToString & vbCrLf
                            End If
                        End If
                    End If
                Next
            End If

            If txtid.Text = "0" Then
                txtid.Text = GetIdentity(trainsession.Name, cboCourse.SelectedValue, "N/A", trainsession.ScheduleDate, trainsession.DueDate, trainsession.DeliveryMethod, trainsession.DeliveryLocation, "Assigned", "Approved", HREmpID, trainsession.Coordinator, trainsession.TrainingType, trainsession.Currency, trainsession.Cost, False, False, "No", trainsession.ScheduleTime, trainsession.ApplicationAssessmentDate)
                If txtid.Text = "0" Then
                    lblstatus = Process.strExp
                    Exit Sub
                End If
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Employee_Training_Sessions_Update_Assessment", txtid.Text)
            Else
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Training_Sessions_update", trainsession.id, trainsession.Name, cboCourse.SelectedValue, "N/A", trainsession.ScheduleDate, trainsession.DueDate, trainsession.DeliveryMethod, trainsession.DeliveryLocation, "Assigned", "Approved", HREmpID, trainsession.Coordinator, trainsession.TrainingType, trainsession.Currency, trainsession.Cost, False, False, "No", trainsession.ScheduleTime, txtAttendee.Text, cboCompany.SelectedValue, drpCategory.SelectedText, txtnote.Value.Trim, trainsession.ApplicationAssessmentDate)
            End If


            If radSessionType.SelectedText = "External" Then
                lstTrainer.Items.Clear()
                lstTrainer.Items.Add(txtTrainer.Value)
            End If

            If lstTrainer.Items.Count > 0 Then
                For d As Integer = 0 To lstTrainer.Items.Count - 1
                    'Level = lstTrainer.Items(d).Text.Split(Separators, StringSplitOptions.RemoveEmptyEntries)
                    EmpID = lstTrainer.Items(d).Value  ' Level(0).ToString.Trim
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Training_Sessions_Trainer_Add", EmpID, txtid.Text, lstTrainer.Items(d).Text)

                    ''Mail Trainers
                    'Dim strTrainerStaff As New DataSet
                    'strTrainerStaff = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "select name, email from Employees_All where EmpID = '" & EmpID & "'")
                    'Dim temail As String = strTrainerStaff.Tables(0).Rows(0).Item("Email").ToString
                    'Dim tname As String = strTrainerStaff.Tables(0).Rows(0).Item("name").ToString
                    'Process.Training_Notification_Trainers(temail, tname, txtname.Text, "", radCoordinator.SelectedItem.Text, txtLocation.Text, Process.DDMMYYYY(radScheduleTime.SelectedDate, "/"), Process.AMPM_Time(Process.ConvertCtrlToTime(radHourStart, radMinStart, radTimeStart)), Process.DDMMYYYY(radDateDue.SelectedDate, "/"))
                Next
                'Get List to delete
                Dim strSession As New DataSet
                'Get All employees in session of database
                strSession = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Training_Session_Trainers_Get_By_Session", txtid.Text)
                If strSession.Tables(0).Rows.Count > 0 Then
                    For c As Integer = 0 To strSession.Tables(0).Rows.Count - 1
                        EmpID = strSession.Tables(0).Rows(0).Item("empid").ToString
                        Dim isTrainer As Boolean = False
                        For h As Integer = 0 To lstTrainer.Items.Count - 1
                            If lstTrainer.Items(h).Value.Contains(EmpID) = True Then
                                isTrainer = True
                                Exit For
                            End If
                        Next 'h loop
                        If isTrainer = False Then 'Delete if Employee not in list
                            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Training_Session_Trainers_Delete_By_Session_EmpID", EmpID, txtid.Text)
                        End If
                    Next
                End If
            Else
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Training_Sessions_Trainer_Delete_By_Session", txtid.Text)
            End If

            'Trainees
            Dim oHODBool As Boolean = False
            Dim oCoachBool As Boolean = False
            Dim oHODStatus As String = "Approved"
            Dim oCoachStatus As String = "Approved"

            'Grade upate
            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Training_Session_JobGrade_Delete", txtid.Text)
            Dim collection As IList(Of RadComboBoxItem) = drpGrade.CheckedItems
            If (collection.Count <> 0) Then
                For Each item As RadComboBoxItem In collection
                    Dim listitem As New RadListBoxItem()
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Training_Session_JobGrade_Update", txtid.Text, item.Value)
                Next
            End If

            'dept upate
            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Training_Session_Dept_Delete", txtid.Text)
            For Each node As RadTreeNode In radDept.EmbeddedTree.CheckedNodes
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Training_Session_Dept_Update", txtid.Text, node.Text)

            Next


            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Employee_Training_Sessions_Update_Mark", txtid.Text, "N")
            If lstTrainee.Items.Count > 0 Then
                For d As Integer = 0 To lstTrainee.Items.Count - 1
                    'Level = lstTrainee.Items(d).Text.Split(Separators, StringSplitOptions.RemoveEmptyEntries)
                    EmpID = lstTrainee.Items(d).Value  'Level(0).ToString.Trim
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Employee_Training_Sessions_Add", EmpID, txtid.Text, oHODStatus, oCoachStatus, datApplicationAssessment.SelectedDate)
                Next
            End If
            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Employee_Training_Sessions_delete_Mark", txtid.Text, "N")


            If NewValue.Trim = "" And OldValue.Trim = "" Then
            Else
                If txtid.Text <> "0" Then
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Updated", "Training Session")
                Else
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Inserted", "Training Session")
                End If
            End If
            cboCompany.Enabled = False
            cboCourse.Enabled = False

            lblstatus = "Record saved"
            Process.loadalert(divalert, msgalert, lblstatus, "success")
            'Response.Write("<script language='javascript'> { self.close() }</script>")
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Dim lblstatus As String = ""
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Response.Redirect("~/Module/Trainings/Settings/TrainingSessions")
        Catch ex As Exception
            lblstatus = ex.Message
            'Process.loadalert(divalert, msgalert, lblstatus, "danger")
        End Try
    End Sub


    Protected Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs)
        'Try
        '    If CheckBox3.Checked = True Then
        '        lblHR.Visible = True
        '        txtHR.Visible = True
        '        radHR.Visible = True
        '    Else
        '        lblHR.Visible = False
        '        txtHR.Visible = False
        '        radHR.Visible = False
        '    End If
        'Catch ex As Exception

        'End Try
    End Sub

    Protected Sub radSessionType_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.DropDownListEventArgs) Handles radSessionType.SelectedIndexChanged
        Try
            If radSessionType.SelectedText.ToLower = "internal" Then
                lstTrainer.Visible = True
                cboTrainer.Visible = True
                txtTrainer.Visible = False
            Else
                lstTrainer.Visible = False
                cboTrainer.Visible = False
                txtTrainer.Visible = True
            End If
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub Button1_Click(sender As Object, e As EventArgs)
        'Dim confirmValue As String = Request.Form("click_value")
        'If confirmValue = "Yes" Then
        '    Process.LoadListBoxFromCombo(lstTrainee, drpTrainee)

        'End If
        Process.LoadListBoxFromCombo(lstTrainee, drpTrainee)
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs)
        Try
            Process.LoadListBoxFromCombo(lstTrainer, cboTrainer)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub

    Protected Sub btnMail_Click(sender As Object, e As EventArgs) Handles btnMail.Click
        Try
            Dim mailSent As Boolean = False
            Dim mailSents As Boolean = False
            If cbocoordinator.Text.Count > 0 Then
                    mailSent = Process.Training_Notification_coordinator(txtname.Text, cbocoordinator.SelectedItem.Text, cbocoordinator.SelectedItem.Text, txtLocation.Value, Process.DDMONYYYY(radScheduleTime.SelectedDate) & " : " & Process.DDMONYYYY(radDateDue.SelectedDate), Process.AMPM_Time(Process.ConvertCtrlToTime(radHourStart.SelectedValue, radMinStart.SelectedValue, radTimeStart.SelectedValue)), cbocoordinator.SelectedValue, Process.ApplicationURL + "/" + Process.GetMailLink(AuthenCode, 2))
                    If mailSent = False Then
                        ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & Process.GetEmailAddress(cbocoordinator.SelectedValue) & ": " & Process.strExp + "')", True)
                    End If
            End If
            If lstTrainer.Items.Count > 0 Then
                For d As Integer = 0 To lstTrainer.Items.Count - 1
                    'Level = lstTrainer.Items(d).Text.Split(Separators, StringSplitOptions.RemoveEmptyEntries)
                    mailSent = Process.Training_Notification_Trainers(txtname.Text, txtTrainer.Value, cbocoordinator.SelectedItem.Text, txtLocation.Value, Process.DDMONYYYY(radScheduleTime.SelectedDate) & " : " & Process.DDMONYYYY(radDateDue.SelectedDate), Process.AMPM_Time(Process.ConvertCtrlToTime(radHourStart.SelectedValue, radMinStart.SelectedValue, radTimeStart.SelectedValue)), lstTrainer.Items(d).Value, Process.ApplicationURL + "/" + Process.GetMailLink(AuthenCode, 2))
                    If mailSent = False Then
                        ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & Process.GetEmailAddress(lstTrainer.Items(d).Value) & ": " & Process.strExp + "')", True)
                    End If
                Next
            End If

            'Trainees
            Dim oHODBool As Boolean = False
            Dim oCoachBool As Boolean = False
            Dim oHODStatus As String = "Approved"
            Dim oCoachStatus As String = "Approved"

            If lstTrainee.Items.Count > 0 Then
                For d As Integer = 0 To lstTrainee.Items.Count - 1
                    'Level = lstTrainee.Items(d).Text.Split(Separators, StringSplitOptions.RemoveEmptyEntries)
                    EmpID = lstTrainee.Items(d).Value  'Level(0).ToString.Trim
                    'Mail Trainees
                    Dim strTraineeStaff As New DataSet
                    strTraineeStaff = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "select name, email,supervisorid from Employees_All where EmpID = '" & EmpID & "'")
                    Dim temail As String = strTraineeStaff.Tables(0).Rows(0).Item("Email").ToString
                    Dim tname As String = strTraineeStaff.Tables(0).Rows(0).Item("name").ToString
                    Dim mgr As String = strTraineeStaff.Tables(0).Rows(0).Item("supervisorid").ToString
                    mailSent = Process.Training_Notification_Trainees(txtname.Text, "", cbocoordinator.SelectedItem.Text, txtLocation.Value, radScheduleTime.SelectedDate, Process.AMPM_Time(Process.ConvertCtrlToTime(radHourStart.SelectedValue, radMinStart.SelectedValue, radTimeStart.SelectedValue)), radDateDue.SelectedDate, EmpID, Process.ApplicationURL + "/" + Process.GetMailLink(AuthenCode, 2))
                    If mailSent = False Then
                        ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & temail & ": " & Process.strExp + "')", True)
                    End If

                Next
            End If
            If mailSent = True Then
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Training notification successfully sent" + "')", True)
                lblstatus = "Training notification successfully sent"
                Process.loadalert(divalert, msgalert, lblstatus, "success")
            Else
                lblstatus = Process.strExp
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
            End If


        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    Protected Sub btnMail0_Click(sender As Object, e As EventArgs) Handles btnMail0.Click
        Try
            Dim strCourse As New DataSet
            strCourse = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Courses_get", cboCourse.SelectedValue)
            Dim questCount = strCourse.Tables(0).Rows(0).Item("QuestionsNo").ToString
            If questCount > 0 Then
                Dim confirmValue As String = Request.Form("confirm_value")
                If confirmValue = "Yes" Then
                    If lstTrainee.Items.Count > 0 Then
                        For d As Integer = 0 To lstTrainee.Items.Count - 1
                            'Level = lstTrainee.Items(d).Text.Split(Separators, StringSplitOptions.RemoveEmptyEntries)
                            EmpID = lstTrainee.Items(d).Value  'Level(0).ToString.Trim
                            'Mail Trainees
                            Dim strTraineeStaff As New DataSet
                            strTraineeStaff = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "select name, email from Employees_All where EmpID = '" & EmpID & "'")
                            Dim temail As String = strTraineeStaff.Tables(0).Rows(0).Item("Email").ToString
                            Dim tname As String = strTraineeStaff.Tables(0).Rows(0).Item("name").ToString
                            Process.Training_Learning_Assessment_Notify(temail, tname, txtname.Text, EmpID, "")
                        Next
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Employee_Training_Sessions_Update_Assessment", txtid.Text)
                        lblstatus = "Learning Assessment Notification sent to trainees"
                        Process.loadalert(divalert, msgalert, lblstatus, "danger")
                        Exit Sub
                    End If
                Else
                    Process.loadalert(divalert, msgalert, Convert.ToString("alert('") & "Learning Assessment cancelled" + "')", "danger")
                End If
            Else
                lblstatus = "No Assessment Question have been set for " & cboCourse.SelectedItem.Text
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                Exit Sub
            End If
            lblstatus = ""
        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "danger")
        End Try
    End Sub

    Protected Sub Button4_Click(sender As Object, e As EventArgs) Handles btnAssessmentNotify.Click
        Try
            Dim confirmValue As String = Request.Form("confirm_value1")
            If confirmValue = "Yes" Then
                If lstTrainee.Items.Count > 0 Then
                    For d As Integer = 0 To lstTrainee.Items.Count - 1
                        EmpID = lstTrainee.Items(d).Value  'Level(0).ToString.Trim
                        'Mail Trainees                        
                        Process.Training_Application_Assessment_Notify(txtid.Text, txtname.Text, EmpID, Process.ApplicationURL + "/" + Process.GetMailLink(AuthenCode, 2))

                    Next
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Employee_Training_Sessions_Update_App_Assessment", txtid.Text)
                    lblstatus = "Application Assessment Notification sent to trainees"
                    Process.loadalert(divalert, msgalert, lblstatus, "success")
                    Exit Sub
                End If
            Else
                lblstatus = "Application Assessment cancelled"
                Process.loadalert(divalert, msgalert, lblstatus, "success")
            End If

            lblstatus = ""
        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "danger")
        End Try
    End Sub

 


    Protected Sub Button5_Click(sender As Object, e As EventArgs)
        Try
            Dim grades As String = ""
            Dim counter As Integer = 0
            Dim collection As IList(Of RadComboBoxItem) = drpGrade.CheckedItems
            If (collection.Count <> 0) Then
                For Each item As RadComboBoxItem In collection
                    counter = counter + 1
                    If counter = 1 Then
                        grades = "'" & item.Text & "'"
                    Else
                        grades = "'" & item.Text & "'," + grades
                    End If
                Next
                radDept.DataFieldID = "ID"
                radDept.DataFieldParentID = "ParentID"
                radDept.DataValueField = "Value"
                radDept.DataTextField = "Text"
                radDept.DataSource = Process.GetUnitsData(cboCompany.SelectedValue, grades) 'Process.GetData("Company_Structure_dropdwon")
                radDept.DataBind()
            Else
                radDept.Entries.Clear()
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    Private Sub reloadTraineelist()
        Try
            Dim counter As Integer = 0
            Dim dept As String = "''"
            Dim grades As String = "''"
            For Each node As RadTreeNode In radDept.EmbeddedTree.CheckedNodes
                counter = counter + 1
                ' If node.Nodes.Count = 0 Then
                ' node.Text 
                If counter = 1 Then
                    dept = "'" & node.Text & "'"
                Else
                    dept = "'" & node.Text & "'," + dept
                End If
                'End If
            Next

            counter = 0
            Dim collection As IList(Of RadComboBoxItem) = drpGrade.CheckedItems
            If (collection.Count <> 0) Then
                For Each item As RadComboBoxItem In collection
                    counter = counter + 1
                    If counter = 1 Then
                        grades = "'" & item.Text & "'"
                    Else
                        grades = "'" & item.Text & "'," + grades
                    End If
                Next

            End If



            'If counter > 0 Then
            Dim strDataSet As New DataSet
            drpTrainee.Items.Clear()
            Dim sql As String = "Select a.Employee, a.EmpID from dbo.Employees_All a where a.Office in (@dept) and a.grade in (@grade) and a.Terminated = 'No'"
            sql = sql.Replace("@dept", dept).Replace("@grade", grades)
            strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, sql)
            For i As Integer = 0 To strDataSet.Tables(0).Rows.Count - 1
                'strDataSet.Tables(0).Rows(i).Item(Index).ToString()
                Dim item As New RadComboBoxItem()
                item.Text = strDataSet.Tables(0).Rows(i).Item("Employee").ToString()
                item.Value = strDataSet.Tables(0).Rows(i).Item("EmpID").ToString()
                drpTrainee.Items.Add(item)
                item.DataBind()
            Next
            'End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub Button6_Click()
        Try
            Dim counter As Integer = 0
            Dim dept As String = "''"
            Dim grades As String = "''"
            For Each node As RadTreeNode In radDept.EmbeddedTree.CheckedNodes
                counter = counter + 1
                ' If node.Nodes.Count = 0 Then
                ' node.Text 
                If counter = 1 Then
                    dept = "'" & node.Text & "'"
                Else
                    dept = "'" & node.Text & "'," + dept
                End If
                'End If
            Next

            counter = 0
            Dim collection As IList(Of RadComboBoxItem) = drpGrade.CheckedItems
            If (collection.Count <> 0) Then
                For Each item As RadComboBoxItem In collection
                    counter = counter + 1
                    If counter = 1 Then
                        grades = "'" & item.Text & "'"
                    Else
                        grades = "'" & item.Text & "'," + grades
                    End If
                Next

            End If



            'If counter > 0 Then
            Dim strDataSet As New DataSet
            drpTrainee.Items.Clear()
            Dim sql As String = "Select a.Employee2, a.EmpID from dbo.Employees_All a where a.Office in (@dept) and a.grade in (@grade) and a.Terminated = 'No'"
            sql = sql.Replace("@dept", dept).Replace("@grade", grades)
            strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, sql)
            For i As Integer = 0 To strDataSet.Tables(0).Rows.Count - 1
                'strDataSet.Tables(0).Rows(i).Item(Index).ToString()
                Dim item As New RadComboBoxItem()
                item.Text = strDataSet.Tables(0).Rows(i).Item("Employee2").ToString()
                item.Value = strDataSet.Tables(0).Rows(i).Item("EmpID").ToString()
                drpTrainee.Items.Add(item)
                item.DataBind()
            Next
            'End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs)
        Try
            Dim confirmValue As String = Request.Form("clear_value")
            If confirmValue = "Yes" Then
                lstTrainee.Items.Clear()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub drpGrade_SelectedIndexChanged1(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs)
        Try
            reloadTraineelist()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    Private Sub ComputeCost()
        Try
            Dim strDataSet As New DataSet
            strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Courses_get", cboCourse.SelectedValue)
            If strDataSet.Tables(0).Rows.Count > 0 Then
                If CDbl(strDataSet.Tables(0).Rows(0).Item("cost").ToString()) > 0 Then
                    txtCost.Text = CInt(txtAttendee.Text) * CDbl(strDataSet.Tables(0).Rows(0).Item("cost").ToString())
                    Process.AssignRadDropDownValue(radCurrency, strDataSet.Tables(0).Rows(0).Item("currency").ToString())
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub cboCourse_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboCourse.SelectedIndexChanged
        ComputeCost
    End Sub

    Protected Sub txtAttendee_TextChanged(sender As Object, e As EventArgs) Handles txtAttendee.TextChanged
        ComputeCost
    End Sub

    Protected Sub cboCompany_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboCompany.SelectedIndexChanged
        Try
            LoadDept()
            Process.LoadRadComboTextAndValueP2(cbocoordinator, "Emp_PersonalDetail_get_all_Specific", "", cboCompany.SelectedValue, "Employee2", "EmpID", False)
            Process.LoadRadComboTextAndValueP2(drpTrainee, "Emp_PersonalDetail_get_all_Specific", "", cboCompany.SelectedValue, "Employee2", "EmpID", False)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub drpTrainee_CheckAllCheck(sender As Object, e As Telerik.Web.UI.RadComboBoxCheckAllCheckEventArgs)
        Try
            Process.LoadListBoxFromCombo(lstTrainee, drpTrainee)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub drpTrainee_ItemChecked(sender As Object, e As Telerik.Web.UI.RadComboBoxItemEventArgs) Handles drpTrainee.ItemChecked
        Try
            Process.LoadListBoxFromCombo(lstTrainee, drpTrainee)

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub



    Protected Sub cboTrainer_ItemChecked(sender As Object, e As Telerik.Web.UI.RadComboBoxItemEventArgs) Handles cboTrainer.ItemChecked
        Try
            Process.LoadListBoxFromCombo(lstTrainer, cboTrainer)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Protected Sub drpGrade_ItemChecked(sender As Object, e As Telerik.Web.UI.RadComboBoxItemEventArgs) Handles drpGrade.ItemChecked
        Button6_Click()
    End Sub



    Protected Sub drpTrainee_CheckAllCheck1(sender As Object, e As Telerik.Web.UI.RadComboBoxCheckAllCheckEventArgs) Handles drpTrainee.CheckAllCheck
        Process.LoadListBoxFromCombo(lstTrainee, drpTrainee)
    End Sub

    Protected Sub cboTrainer_CheckAllCheck(sender As Object, e As Telerik.Web.UI.RadComboBoxCheckAllCheckEventArgs) Handles cboTrainer.CheckAllCheck
        Process.LoadListBoxFromCombo(lstTrainer, cboTrainer)
    End Sub
End Class