Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Telerik.Web.UI

Public Class AppraisalPeriodUpdate
    Inherits System.Web.UI.Page
    Dim clsappcycle As New clsAppraisalCycle
    Dim AuthenCode As String = "APPRAISALPERIOD"
    Dim olddata(9) As String
    Dim lblstatus As String = ""
    Private Sub GetDate_Range()
        Try
            Dim strUser As New DataSet
            strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Finance_Calendar_get", cboYear.SelectedValue)
            If strUser.Tables(0).Rows.Count > 0 Then
                lblCalendar.Value = Process.DDMONYYYY(CDate(strUser.Tables(0).Rows(0).Item("PeriodStart"))) & " : " & Process.DDMONYYYY(CDate(strUser.Tables(0).Rows(0).Item("PeriodEnd")))
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                'Company_Structure_get_parent
                cboStatus.Items.Clear()
                cboStatus.Items.Add("Locked")
                cboStatus.Items.Add("Open")
                txtid.Text = "0"

                Dim ismulti As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select isnull(ismulticompany,'No')  from general_info")

                If ismulti.ToLower = "no" Then
                    Process.LoadRadComboTextAndValueP2(cboCompany, "Company_Structure_Get_ByLevel", "1", Session("Access"), "name", "name", False)
                Else
                    Process.LoadRadComboTextAndValueP2(cboCompany, "Company_Structure_Get_ByLevel", "2", Session("Access"), "name", "name", False)
                End If

                If Request.QueryString("id") IsNot Nothing Then
                    Process.LoadRadComboTextAndValue(cboYear, "Finance_Calendar_get_all", "name", "name", False)
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Appraisal_Cycle_Get", Request.QueryString("id"))
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    dateStart.SelectedDate = strUser.Tables(0).Rows(0).Item("StartPeriod").ToString
                    dateEnd.SelectedDate = strUser.Tables(0).Rows(0).Item("EndPeriod").ToString
                    dateDue.SelectedDate = strUser.Tables(0).Rows(0).Item("Due").ToString
                    Process.AssignRadComboValue(cboStatus, strUser.Tables(0).Rows(0).Item("Status").ToString)
                    Process.AssignRadComboValue(cboYear, strUser.Tables(0).Rows(0).Item("ReviewYear").ToString)
                    Process.AssignRadComboValue(cboCompany, strUser.Tables(0).Rows(0).Item("company").ToString)
                    'txtCompetScore.Text = strUser.Tables(0).Rows(0).Item("competencies").ToString
                    'txtGoalScore.Text = strUser.Tables(0).Rows(0).Item("goals").ToString
                    txtQuestScore.Text = strUser.Tables(0).Rows(0).Item("questions").ToString

                    lblcreatedby.Value = strUser.Tables(0).Rows(0).Item("AddedBy").ToString
                    lblcreatedon.Value = strUser.Tables(0).Rows(0).Item("AddedOn").ToString
                    lblupdatedby.Value = strUser.Tables(0).Rows(0).Item("UpdatedBy").ToString
                    lblupdatedon.Value = strUser.Tables(0).Rows(0).Item("UpdatedOn").ToString
                    lblnotification.Value = strUser.Tables(0).Rows(0).Item("NotificationSent").ToString
                    lblForm.Value = strUser.Tables(0).Rows(0).Item("FormGenerated").ToString
                    txtEmpweight.Value = strUser.Tables(0).Rows(0).Item("EmpGradeRatio").ToString
                    txtmgrweight.Value = strUser.Tables(0).Rows(0).Item("MgrGradeRatio").ToString
                    txtmgrweight2.Value = strUser.Tables(0).Rows(0).Item("MgrGradeRatio2").ToString

                    If strUser.Tables(0).Rows(0).Item("MgrGradeRatio2").ToString = "Yes" Then
                        Process.loadalert(divalert, msgalert, "Objectives copied from previous appraisal objective", "danger")
                        Process.DisableButton(btngenerate0)
                    Else

                    End If

                    GetDate_Range()
                    cboYear.Enabled = False
                    cboCompany.Enabled = False
                Else
                    cboYear.Items.Clear()
                    For z As Integer = Date.Now.Year - 1 To 2100
                        Dim itemTemp As New RadComboBoxItem()
                        itemTemp.Text = z.ToString
                        itemTemp.Value = z.ToString
                        cboYear.Items.Add(itemTemp)
                        itemTemp.DataBind()
                    Next

                    'Process.AssignRadComboValue(cboYear, Now.Year.ToString)
                End If

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Function GetIdentity(ByVal ryear As Integer, ByVal startdate As Date, enddate As Date, duedate As Date, mgrweight As Double, empweight As Double, question As Double, status As String, user As String, mgrweight2 As Double, company As String) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Performance_Appraisal_Cycle_Update"
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@year", SqlDbType.Int).Value = ryear
            cmd.Parameters.Add("@startperiod", SqlDbType.Date).Value = startdate
            cmd.Parameters.Add("@endperiod", SqlDbType.Date).Value = enddate
            cmd.Parameters.Add("@due", SqlDbType.Date).Value = duedate
            cmd.Parameters.Add("@mgrratio", SqlDbType.Decimal).Value = mgrweight
            cmd.Parameters.Add("@empratio", SqlDbType.Decimal).Value = empweight
            cmd.Parameters.Add("@quest", SqlDbType.Decimal).Value = question
            cmd.Parameters.Add("@status", SqlDbType.VarChar).Value = status
            cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = user
            cmd.Parameters.Add("@mgrratio2", SqlDbType.Decimal).Value = mgrweight2
            cmd.Parameters.Add("@company", SqlDbType.VarChar).Value = company
            cmd.Parameters.Add("@GradeRatio360", SqlDbType.Decimal).Value = 0
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
            If txtid.Text <> "0" Then
                If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False Then
                    ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
                    Exit Sub
                End If
            End If


            If (dateStart.SelectedDate Is Nothing) Then
                lblstatus = "Start Period required!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                dateStart.Focus()
                Exit Sub
            End If
            If (dateEnd.SelectedDate Is Nothing) Then
                lblstatus = "End Period required!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                dateEnd.Focus()
                Exit Sub
            End If
            If (dateDue.SelectedDate Is Nothing) Then
                lblstatus = "Due Date required!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                dateDue.Focus()
                Exit Sub
            End If

            If dateStart.SelectedDate > dateEnd.SelectedDate Then
                lblstatus = "Appraisal Date Range invalid!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                dateEnd.Focus()
                Exit Sub
            End If

            If dateDue.SelectedDate < dateEnd.SelectedDate Then
                lblstatus = "Date Due cannot be less than Appraisal End Date!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                dateEnd.Focus()
                Exit Sub
            End If

            If Process.ValidateCalendar("Financial", cboYear.SelectedValue, dateStart.SelectedDate).ToUpper = "NO" Then
                lblstatus = "Start Date " & dateStart.SelectedDate.ToString & " does not fall within Financial Calendar of " & cboYear.SelectedValue
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                dateStart.Focus()
                Exit Sub
            End If

            If Process.ValidateCalendar("Financial", cboYear.SelectedValue, dateEnd.SelectedDate).ToUpper = "NO" Then
                lblstatus = "End Date " & dateEnd.SelectedDate.ToString & " does not fall within Financial Calendar of " & cboYear.SelectedValue
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                dateEnd.Focus()
                Exit Sub
            End If

            If dateStart.SelectedDate > dateEnd.SelectedDate Then
                lblstatus = "Start Date cannot be beyond End Date required!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                dateEnd.Focus()
                Exit Sub
            End If

            If dateEnd.SelectedDate > dateDue.SelectedDate Then
                lblstatus = "End Date cannot be beyond Due Date required!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                dateEnd.Focus()
                Exit Sub
            End If

            If IsNumeric(txtmgrweight.Value) = False Then
                lblstatus = "Reviewer 1's Weight required in number!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                txtmgrweight.Focus()
                Exit Sub
            End If

            If IsNumeric(txtmgrweight2.Value) = False Then
                lblstatus = "Reviewer 2's Weight required in number!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                txtmgrweight2.Focus()
                Exit Sub
            End If

            If IsNumeric(txtEmpweight.Value) = False Then
                lblstatus = "Reviewee's Weight required in number!"
                txtEmpweight.Focus()
                Exit Sub
            End If

            If (CDbl(txtEmpweight.Value) + CDbl(txtmgrweight.Value) + CDbl(txtmgrweight2.Value)) <> 100 Then
                lblstatus = "Sum of Reviewer and Reviewee's Weight must equal 100!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                txtEmpweight.Focus()
                Exit Sub
            End If

            'If (CDbl(txtCompetScore.Text) + CDbl(txtGoalScore.Text) + CDbl(txtQuestScore.Text) >= 99.9) And (CDbl(txtCompetScore.Text) + CDbl(txtGoalScore.Text) + CDbl(txtQuestScore.Text) <= 100) Then
            'Else
            '    lblstatus.Text = "Total Appraisal must be 100%!"
            '    txtCompetScore.Focus()
            '    Exit Sub
            'End If


            'Old Data
            If txtid.Text <> "0" Then
                Dim strUser As New DataSet
                strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Appraisal_Cycle_get", txtid.Text)
                olddata(0) = strUser.Tables(0).Rows(0).Item("id").ToString
                olddata(1) = strUser.Tables(0).Rows(0).Item("StartPeriod").ToString
                olddata(2) = strUser.Tables(0).Rows(0).Item("EndPeriod").ToString
                olddata(3) = strUser.Tables(0).Rows(0).Item("Due").ToString
                olddata(4) = strUser.Tables(0).Rows(0).Item("Status").ToString
                olddata(5) = strUser.Tables(0).Rows(0).Item("reviewyear").ToString
                olddata(6) = strUser.Tables(0).Rows(0).Item("MgrGradeRatio").ToString
                olddata(7) = strUser.Tables(0).Rows(0).Item("MgrGradeRatio2").ToString
                olddata(8) = strUser.Tables(0).Rows(0).Item("EmpGradeRatio").ToString
                olddata(9) = strUser.Tables(0).Rows(0).Item("company").ToString
            End If

            If txtid.Text.Trim = "" Then
                clsappcycle.id = 0
            Else
                clsappcycle.id = txtid.Text
            End If
            clsappcycle.StartPeriod = dateStart.SelectedDate
            clsappcycle.EndPeriod = dateEnd.SelectedDate
            clsappcycle.DueDate = dateDue.SelectedDate
            clsappcycle.Status = cboStatus.SelectedItem.Text
            clsappcycle.ReviewYear = cboYear.SelectedValue
            clsappcycle.ReviewerWeight = txtmgrweight.Value
            clsappcycle.RevieweeWeight = txtEmpweight.Value
            clsappcycle.Reviewer2Weight = txtmgrweight2.Value
            clsappcycle.Company = cboCompany.SelectedValue

            'Check Cycle Validity
            Dim IsOk As Boolean = CBool(SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Performance_Appraisal_Cycle_Check", clsappcycle.id, clsappcycle.StartPeriod, clsappcycle.EndPeriod, clsappcycle.Company))

            If IsOk = False Then
                lblstatus = "Entry is invalid, Date period cannot run in between other existing cycles"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                Exit Sub
            End If

            Dim OldValue As String = ""
            Dim NewValue As String = ""

            Dim j As Integer = 0

            If txtid.Text <> "0" Then
                For Each a In GetType(clsAppraisalCycle).GetProperties()
                    If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If IsNumeric(a.GetValue(clsappcycle, Nothing)) = True And IsNumeric(olddata(j)) = True Then
                                If CDbl(a.GetValue(clsappcycle, Nothing)) <> CDbl(olddata(j)) Then
                                    NewValue += a.Name + ": " + a.GetValue(clsappcycle, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            Else
                                If a.GetValue(clsappcycle, Nothing).ToString <> olddata(j).ToString Then
                                    NewValue += a.Name + ": " + a.GetValue(clsappcycle, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            End If
                        End If
                    End If
                    j = j + 1
                Next
            Else
                For Each a In GetType(clsAppraisalCycle).GetProperties() 'New Entries
                    If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If a.GetValue(clsappcycle, Nothing) = Nothing Then
                                NewValue += a.Name + ":" + " " & vbCrLf
                            Else
                                NewValue += a.Name + ": " + a.GetValue(clsappcycle, Nothing).ToString & vbCrLf
                            End If
                        End If
                    End If
                Next
            End If

            If txtid.Text = "0" Or txtid.Text.Trim = "" Then
                txtid.Text = GetIdentity(clsappcycle.ReviewYear, clsappcycle.StartPeriod, clsappcycle.EndPeriod, clsappcycle.DueDate, clsappcycle.ReviewerWeight, clsappcycle.RevieweeWeight, 0, clsappcycle.Status, Session("LoginID"), clsappcycle.Reviewer2Weight, clsappcycle.Company)
                If txtid.Text = "0" Then
                    lblstatus = Process.strExp
                    Process.loadalert(divalert, msgalert, lblstatus, "danger")
                    Exit Sub
                End If
            Else
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_Cycle_update", clsappcycle.id, clsappcycle.ReviewYear, clsappcycle.StartPeriod, clsappcycle.EndPeriod, clsappcycle.DueDate, clsappcycle.ReviewerWeight, clsappcycle.RevieweeWeight, 0, clsappcycle.Status, Session("LoginID"), clsappcycle.Reviewer2Weight, clsappcycle.Company, 0)
            End If

            If NewValue.Trim = "" And OldValue.Trim = "" Then
            Else
                If txtid.Text <> "0" Then
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Updated " & dateStart.SelectedDate & "-" & dateEnd.SelectedDate, "Appraisal Cycle")
                Else
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Inserted", "Appraisal Cycle")
                End If

            End If
            lblstatus = "Record saved"
            Process.loadalert(divalert, msgalert, lblstatus, "success")
            'Response.Write("<script language='javascript'> { self.close() }</script>")
        Catch ex As Exception
            lblstatus = ex.Message
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect("~/Module/Performance/Settings/AppraisalPeriodList.aspx", True)
        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "danger")
        End Try
    End Sub


    Protected Sub btngenerate_Click(sender As Object, e As EventArgs)
        Try
            System.Threading.Thread.Sleep(300)
            If txtid.Text = "0" Then
                lblstatus = "Record must be saved before notifying employees"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                Exit Sub
            End If

            If cboStatus.SelectedItem.Text = "Locked" Then
                lblstatus = "Notification cannot be sent on Locked/Closed Appraisal Cycle"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                Exit Sub
            End If

            Dim strForm As New DataSet
            strForm = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Appraisal_Summary_Get_All", txtid.Text)
            If strForm.Tables(0).Rows.Count = 0 Then
                lblstatus = "Generate Appraisal Form for Employees before sending notification"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                btngenerate0.Focus()
                Exit Sub
            End If

            Dim strGrade As New DataSet
            strGrade = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Job_Grade_get_all")
            Dim jobgrade As String = ""
            If strGrade.Tables(0).Rows.Count > 0 Then
                For j As Integer = 0 To strGrade.Tables(0).Rows.Count - 1
                    jobgrade = strGrade.Tables(0).Rows(j).Item("name").ToString()
                    Dim strDataSet As New DataSet
                    strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "Select a.empid, Name,isnull(email,'aa') email  from dbo.Employees_All a left outer join Performance_Appraisal_Summary b on a.empid = b.empid and b.AppraisalCycleID = " & txtid.Text & " where (b.Completed = 'No' or b.Completed is null) and a.Grade in (select b.JobGrade from Competency_JobGrade b) or a.JobTitle in (select c.JobTitle from Competency_JobTitle c) and a.grade = '" & jobgrade & "' and dbo.My_Company(a.Office) ='" & cboCompany.SelectedValue & "' and a.Terminated = 'No'")
                    Dim maillist As String = ""
                    Dim empidlist As String = ""
                    If strDataSet.Tables(0).Rows.Count > 0 Then
                        For i As Integer = 0 To strDataSet.Tables(0).Rows.Count - 1
                            If i = 0 Then
                                maillist = strDataSet.Tables(0).Rows(i).Item("email").ToString()
                                empidlist = strDataSet.Tables(0).Rows(i).Item("empid").ToString()
                            Else
                                maillist = maillist & ";" & strDataSet.Tables(0).Rows(i).Item("email").ToString()
                                empidlist = empidlist & ";" & strDataSet.Tables(0).Rows(i).Item("empid").ToString()
                            End If
                        Next
                        Process.Appraisal_Objective_Alert(empidlist, maillist, Process.DDMONYYYY(dateStart.SelectedDate), Process.GetMailLink("EMPMYGOAL", 3))
                    End If
                Next
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_Cycle_Update_Notification", txtid.Text)

                lblstatus = "Appraisal Notification sent to Employees of " & cboCompany.SelectedValue
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & lblstatus + "')", True)
            End If


        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "danger")
        End Try
    End Sub

    Protected Sub btngenerate0_Click(sender As Object, e As EventArgs) Handles btngenerate0.Click
        Try
            If txtid.Text = "0" Then
                lblstatus = "Save before copying previous appraisal objective!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                Exit Sub
            End If

            Dim confirmValue As String = Request.Form("confirm_value")
            If confirmValue = "Yes" Then
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_Summary_Copy", txtid.Text)
                lblstatus = "Objectives copied from previous appraisal objective"
                Process.loadalert(divalert, msgalert, lblstatus, "success")
                btngenerate0.Enabled = False
                btngenerate0.BackColor = Color.Gray
            End If
          

            'If cboStatus.SelectedItem.Text = "Locked" Then
            '    lblstatus.Text = "New Form cannot be generated on Locked/Closed Appraisal Cycle"
            '    ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & lblstatus.Text + "')", True)
            '    Exit Sub
            'End If

            'SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_Summary_Create", txtid.Text)
            'SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_Cycle_Update_Form", txtid.Text)
            'lblstatus.Text = Process.DDMONYYYY(dateStart.SelectedDate) & " : " & Process.DDMONYYYY(dateEnd.SelectedDate) & " Appraisal Forms successfully generated"
            
        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "danger")
        End Try
    End Sub

    Protected Sub cboYear_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboYear.SelectedIndexChanged
        Try
            GetDate_Range()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btngenerate1_Click(sender As Object, e As EventArgs)
        Try
            System.Threading.Thread.Sleep(300)

            If txtid.Text = "0" Then
                lblstatus = "Record must be saved before notifying employees"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                Exit Sub
            End If

            If cboStatus.SelectedItem.Text = "Locked" Then
                lblstatus = "Notification cannot be sent on Locked/Closed Appraisal Cycle"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                Exit Sub
            End If

            Dim strForm As New DataSet
            strForm = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Appraisal_Summary_Get_All", txtid.Text)
            If strForm.Tables(0).Rows.Count = 0 Then
                lblstatus = "Generate Appraisal Form for Employees before sending notification"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                btngenerate0.Focus()
                Exit Sub
            End If

            Dim strGrade As New DataSet
            strGrade = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Job_Grade_get_all")
            Dim jobgrade As String = ""
            If strGrade.Tables(0).Rows.Count > 0 Then
                For j As Integer = 0 To strGrade.Tables(0).Rows.Count - 1
                    jobgrade = strGrade.Tables(0).Rows(j).Item("name").ToString()
                    Dim strDataSet As New DataSet
                    strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "Select a.empid, Name,isnull(email,'aa')  from dbo.Employees_All a left outer join Performance_Appraisal_Summary b on a.empid = b.empid and b.AppraisalCycleID = " & txtid.Text & " where a.email is not null and (b.EmpSubmited = 'No' or b.EmpSubmited is null) and a.Grade in (select b.JobGrade from Competency_JobGrade b) or a.JobTitle in (select c.JobTitle from Competency_JobTitle c) and a.grade = '" & jobgrade & "' and dbo.My_Company(a.Office) ='" & cboCompany.SelectedValue & "' and a.Terminated = 'No'")
                    Dim maillist As String = ""
                    Dim empidlist As String = ""
                    If strDataSet.Tables(0).Rows.Count > 0 Then
                        For i As Integer = 0 To strDataSet.Tables(0).Rows.Count - 1
                            If i = 0 Then
                                maillist = strDataSet.Tables(0).Rows(i).Item("email").ToString()
                                empidlist = strDataSet.Tables(0).Rows(i).Item("empid").ToString()
                            Else
                                maillist = maillist & ";" & strDataSet.Tables(0).Rows(i).Item("email").ToString()
                                empidlist = empidlist & ";" & strDataSet.Tables(0).Rows(i).Item("empid").ToString()
                            End If
                        Next
                        Process.Appraisal_Feedback_Alert(empidlist, maillist, Process.DDMONYYYY(dateEnd.SelectedDate), Process.GetMailLink(AuthenCode, 2))
                    End If
                Next
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_Cycle_Update_Notification", txtid.Text)

                lblstatus = "Appraisal Notification sent to Employees of " & cboCompany.SelectedValue
                Process.loadalert(divalert, msgalert, lblstatus, "success")
            End If


        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "danger")
        End Try
    End Sub
End Class