Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Collections.Generic
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Telerik.Web.UI

Public Class JobPostingUpdate
    Inherits System.Web.UI.Page
    Dim jobpost As New clsJobPost
    Dim olddata(22) As String
    Dim AuthenCode As String = "JOBPOST"


    Private Sub LoadDynamic()
        Try
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                cboOnline.Items.Clear()
                cboOnline.Items.Add("No")
                cboOnline.Items.Add("Yes")

                cboclone.Items.Clear()
                cboclone.Items.Add("No")
                cboclone.Items.Add("Yes")

                Dim ismulti As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select isnull(ismulticompany,'No')  from general_info")
                If ismulti.ToLower = "no" Then
                    Process.LoadRadComboTextAndValueP2(cboOffice, "Company_Structure_Get_ByLevel", "1", Session("Access"), "name", "name", False)
                Else
                    Process.LoadRadComboTextAndValueP2(cboOffice, "Company_Structure_Get_ByLevel", "2", Session("Access"), "name", "name", False)
                End If

                Process.LoadRadComboTextAndValueInitiate(cbojobclone, "Recruit_Job_Requisition_Get_Approved", "--Select--", "title", "id")
                Process.LoadRadComboTextAndValueInitiate(cboJobTitle, "Job_Titles_get_all", "-- Select --", "name", "id")

                Process.LoadRadComboTextAndValue(cbojobtype, "employment_status_get_all", "name", "name", False)
                'Process.LoadRadDropDownTextAndValue(radCountry, "CountryTable_get", "country", "country", False)
                Process.LoadRadComboTextAndValue(cbocurrency, "Currency_Load", "currency", "currency", False)
                Process.LoadRadComboTextAndValueInitiate(cboExperience, "Recruit_Experience_Level_get_all", "Any Experience Level", "name", "name")
                Process.LoadRadComboTextAndValueInitiate(cboEducation, "Education_get_all", "Any Education Level", "name", "name")
                Process.LoadRadComboTextAndValueP1(cboHiringManager, "Emp_PersonalDetail_Get_Employees", Process.GetCompanyName, "name", "EmpID", True)
                Process.LoadRadComboTextAndValue(cboSpecialisation, "Recruit_Specialization_Get_All", "name", "name")
                Process.AssignRadComboValue(cboOnline, "No")
                Process.LoadRadComboTextAndValue(cboDiscipline, "Recruit_Academic_Discipline_Get_All", "name", "name", True)
                Process.LoadRadComboTextAndValue(cboSchoolLeaving, "Recruit_OLevel_Subject_Get_All", "name", "name", True)
                Process.LoadRadComboTextAndValue(cboMinOLGrade, "Recruit_OLevel_Grade_Get_All", "name", "name", True)
                Process.LoadRadComboTextAndValue(cboMinGrade, "Recruit_Academic_Grade_Get_All", "name", "name", True)

                radMail.Items.Clear()
                radMail.Items.Add("No")
                radMail.Items.Add("Yes")

                cboActive.Items.Clear()
                cboActive.Items.Add("Yes")
                cboActive.Items.Add("No")


                divactive.Visible = False

                If Request.QueryString("id") IsNot Nothing Then
                    divclone.Visible = False

                    Dim strUser As New DataSet
                    divactive.Visible = True
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Job_Post_get", Request.QueryString("id"))
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    pagetitle.InnerText = "Job Post " & strUser.Tables(0).Rows(0).Item("Code").ToString
                    Process.AssignRadComboValue(cboJobTitle, strUser.Tables(0).Rows(0).Item("Title").ToString)
                    Process.AssignRadComboValue(cboOffice, strUser.Tables(0).Rows(0).Item("company").ToString)
                    Process.AssignRadComboValue(cbojobtype, strUser.Tables(0).Rows(0).Item("type").ToString)
                    ajobdesc.Value = strUser.Tables(0).Rows(0).Item("JobDescription").ToString
                    anote.Value = strUser.Tables(0).Rows(0).Item("comments").ToString
                    'Education
                    Process.AssignRadComboValue(cboEducation, strUser.Tables(0).Rows(0).Item("EducationLevel").ToString)

                    'cboExperience
                    Process.AssignRadComboValue(cboExperience, strUser.Tables(0).Rows(0).Item("ExperienceLevel").ToString)
                    Process.AssignRadComboValue(cboOnline, strUser.Tables(0).Rows(0).Item("TestOnline").ToString)                    
                    ajobskill.Value = strUser.Tables(0).Rows(0).Item("Skills").ToString
                    aposition.Value = strUser.Tables(0).Rows(0).Item("NoOfPositions").ToString
                    lblcountry.Text = strUser.Tables(0).Rows(0).Item("Country").ToString
                    txtLocation.Text = strUser.Tables(0).Rows(0).Item("Location").ToString
                    aminage.Value = strUser.Tables(0).Rows(0).Item("StartAgeRange").ToString
                    amaxage.Value = strUser.Tables(0).Rows(0).Item("EndAgeRange").ToString
                    Process.AssignRadComboValue(cbocurrency, strUser.Tables(0).Rows(0).Item("Currency").ToString)
                    aminsalary.Value = strUser.Tables(0).Rows(0).Item("StartSalaryRange").ToString
                    amaxsalary.Value = strUser.Tables(0).Rows(0).Item("EndSalaryRange").ToString

                    'Status
                    ajobstatus.Value = strUser.Tables(0).Rows(0).Item("Status").ToString
                    radCloseDate.SelectedDate = CDate(strUser.Tables(0).Rows(0).Item("closingdate").ToString)

                    'Mail
                    Process.AssignRadComboValue(radMail, strUser.Tables(0).Rows(0).Item("sendmail").ToString)
                    Process.AssignRadComboValue(cboActive, strUser.Tables(0).Rows(0).Item("RecruitmentActive").ToString)

                    'Specialisation       
                    Process.AssignRadComboValue(cboSpecialisation, strUser.Tables(0).Rows(0).Item("specialization").ToString)

                    aminexpyr.Value = strUser.Tables(0).Rows(0).Item("experience1").ToString
                    amaxexpyr.Value = strUser.Tables(0).Rows(0).Item("experience2").ToString
                    Process.AssignRadComboValue(cboHiringManager, strUser.Tables(0).Rows(0).Item("hiringmanager").ToString)

                    Process.AssignRadComboValue(cboMinGrade, strUser.Tables(0).Rows(0).Item("minacademicgrade").ToString)
                    Process.AssignRadComboValue(cboMinOLGrade, strUser.Tables(0).Rows(0).Item("minentrylevelgrade").ToString)
                    Process.LoadListAndComboxFromDataset(lstDiscipline, cboDiscipline, "Recruit_Job_Post_Discipline_Get_All", "items", "items", txtid.Text)
                    Process.LoadListAndComboxFromDataset(lstSchoolLeaving, cboSchoolLeaving, "Recruit_Job_Post_OLSubject_Get_All", "items", "items", txtid.Text)
                    Process.LoadRadComboTextAndValueP1(cboLocation, "location_get_country", lblcountry.Text, "name", "name", False)
                    Process.CheckComboFromText(txtLocation, cboLocation)
                Else
                    txtid.Text = "0"
                    ajobstatus.Value = "Open"
                    divclonesource.Visible = False
                End If
            End If




        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub



    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try

            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "warning")
                Exit Sub
            End If

            Dim lblstatus As String = ""
            If cboJobTitle.SelectedItem.Text.Contains("--") = True Then
                lblstatus = "Job Title is required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                cboJobTitle.Focus()
                Exit Sub
            End If

            If (cbocurrency.SelectedValue Is Nothing) Then
                lblstatus = "Currency Value required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                cbocurrency.Focus()
                Exit Sub
            End If


            If IsNumeric(aposition.Value) = False Then
                lblstatus = "Number of Position must be numeric only!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                aposition.Focus()
                Exit Sub
            End If

            If IsNumeric(aminage.Value) = False Then
                lblstatus = "Minimum Age must be numeric only!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                aminage.Focus()
                Exit Sub
            End If

            If IsNumeric(amaxage.Value) = False Then
                lblstatus = "Maximum Age must be numeric only!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                amaxage.Focus()
                Exit Sub
            End If

            'If IsNumeric(aminsalary.Value) = False Then
            '    lblstatus = "Minimum Salary Range must be numeric only!"
            '    Process.loadalert(divalert, msgalert, lblstatus, "warning")
            '    aminsalary.Focus()
            '    Exit Sub
            'End If

            'If IsNumeric(amaxsalary.Value) = False Then
            '    lblstatus = "Maximum Salary Range must be numeric only!"
            '    Process.loadalert(divalert, msgalert, lblstatus, "warning")
            '    amaxsalary.Focus()
            '    Exit Sub
            'End If

            If IsNumeric(aminexpyr.Value) = False Then
                lblstatus = "Years of Experience must be numeric only, set to 0 if no experience is required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                aminexpyr.Focus()
                Exit Sub
            End If

            If IsNumeric(amaxexpyr.Value) = False Then
                lblstatus = "Years of Experience must be numeric only, set to 0 if no experience is required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                amaxexpyr.Focus()
                Exit Sub
            End If

            If CInt(aposition.Value) < 0 Then
                lblstatus = "Set Number of Positions required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                aposition.Focus()
                Exit Sub
            End If

            If CInt(aminage.Value) > CInt(amaxage.Value) Then
                lblstatus = "Maximum Age cannot be less than Minimum Age!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                aminage.Focus()
                Exit Sub
            End If

            If CInt(amaxexpyr.Value) < CInt(aminexpyr.Value) Then
                lblstatus = "Maximum Years of Experience cannot be less than Minimum Years!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                amaxexpyr.Focus()
                Exit Sub
            End If
            If (amaxsalary.Value = "" Or aminsalary.Value = "") Then
                aminsalary.Value = "0"
                amaxsalary.Value = "0"
            End If
            If CInt(amaxsalary.Value) < CInt(aminsalary.Value) Then
                lblstatus = "Maximum Salary cannot be less than Minimum Salary!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                amaxsalary.Focus()
                Exit Sub
            End If

            If txtid.Text <> "0" Then
                Dim strUser As New DataSet
                strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Job_Post_get", txtid.Text)
                olddata(0) = strUser.Tables(0).Rows(0).Item("id").ToString
                olddata(1) = strUser.Tables(0).Rows(0).Item("Title").ToString
                olddata(2) = strUser.Tables(0).Rows(0).Item("Type").ToString
                olddata(3) = strUser.Tables(0).Rows(0).Item("HiringManager").ToString
                olddata(4) = strUser.Tables(0).Rows(0).Item("JobDescription").ToString
                olddata(5) = strUser.Tables(0).Rows(0).Item("EducationLevel").ToString
                olddata(6) = strUser.Tables(0).Rows(0).Item("ExperienceLevel").ToString
                olddata(7) = strUser.Tables(0).Rows(0).Item("Skills").ToString
                olddata(8) = strUser.Tables(0).Rows(0).Item("NoOfPositions").ToString
                olddata(9) = strUser.Tables(0).Rows(0).Item("Country").ToString
                olddata(10) = strUser.Tables(0).Rows(0).Item("Location").ToString
                olddata(11) = strUser.Tables(0).Rows(0).Item("StartAgeRange").ToString
                olddata(12) = strUser.Tables(0).Rows(0).Item("EndAgeRange").ToString
                olddata(13) = strUser.Tables(0).Rows(0).Item("Currency").ToString
                olddata(14) = strUser.Tables(0).Rows(0).Item("StartSalaryRange").ToString
                olddata(15) = strUser.Tables(0).Rows(0).Item("EndSalaryRange").ToString
                olddata(16) = strUser.Tables(0).Rows(0).Item("Status").ToString
                olddata(17) = strUser.Tables(0).Rows(0).Item("closingdate").ToString
                olddata(18) = strUser.Tables(0).Rows(0).Item("sendmail").ToString
                olddata(19) = strUser.Tables(0).Rows(0).Item("specialization").ToString
                olddata(20) = strUser.Tables(0).Rows(0).Item("experience1").ToString
                olddata(21) = strUser.Tables(0).Rows(0).Item("experience2").ToString
            End If


            If txtid.Text.Trim = "" Or txtid.Text = "0" Then
                jobpost.id = 0
            Else
                jobpost.id = txtid.Text
            End If
            jobpost.ClosingDate = radCloseDate.SelectedDate
            jobpost.Skills = ajobskill.Value
            jobpost.Country = lblcountry.Text
            jobpost.Currency = cbocurrency.SelectedValue
            jobpost.EducationLevel = cboeducation.SelectedValue
            jobpost.ExperienceLevel = cboExperience.SelectedValue
            jobpost.HiringManager = cboHiringManager.SelectedValue
            jobpost.JobDescription = ajobdesc.Value
            jobpost.JobTitle = cboJobTitle.SelectedItem.Text
            jobpost.JobType = cbojobtype.SelectedValue
            jobpost.Location = txtLocation.Text
            jobpost.MaxAge = amaxage.Value
            jobpost.MinAge = aminage.Value
            jobpost.MinSalary = aminsalary.Value
            jobpost.MaxSalary = amaxsalary.Value
            jobpost.NoOfPosition = aposition.Value
            jobpost.SendMail = radMail.SelectedItem.Text
            jobpost.Status = ajobstatus.Value
            jobpost.Specialisation = cboSpecialisation.SelectedValue
            jobpost.MinYearsOfExperience = aminexpyr.Value
            jobpost.MaxYearsOfExperience = amaxexpyr.Value



            Dim OldValue As String = ""
            Dim NewValue As String = ""

            Dim j As Integer = 0

            If txtid.Text <> "0" Then 'Updates
                For Each a In GetType(clsJobPost).GetProperties()
                    If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If IsNumeric(a.GetValue(jobpost, Nothing)) = True And IsNumeric(olddata(j)) = True Then
                                If CDbl(a.GetValue(jobpost, Nothing)) <> CDbl(olddata(j)) Then
                                    NewValue += a.Name + ": " + a.GetValue(jobpost, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            Else
                                If a.GetValue(jobpost, Nothing).ToString <> olddata(j).ToString Then
                                    NewValue += a.Name + ": " + a.GetValue(jobpost, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            End If
                        End If
                    End If
                    j = j + 1
                Next
            Else
                For Each a In GetType(clsJobPost).GetProperties() 'New Entries
                    If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If a.GetValue(jobpost, Nothing) = Nothing Then
                                NewValue += a.Name + ":" + " " & vbCrLf
                            Else
                                NewValue += a.Name + ": " + a.GetValue(jobpost, Nothing).ToString & vbCrLf
                            End If
                        End If
                    End If
                Next
            End If

            If txtid.Text = "0" Then
                txtid.Text = GetIdentity(jobpost.JobTitle, jobpost.JobType, jobpost.JobDescription, jobpost.EducationLevel, jobpost.ExperienceLevel, jobpost.Skills, jobpost.NoOfPosition, jobpost.Country, jobpost.Location, jobpost.MinAge, jobpost.MaxAge, jobpost.Currency, jobpost.MinSalary, jobpost.MaxSalary, jobpost.ClosingDate, jobpost.SendMail, jobpost.HiringManager, cboActive.SelectedItem.Text, cboSpecialisation.SelectedItem.Text, aminexpyr.Value, amaxexpyr.Value, cboOnline.SelectedItem.Text)
                If txtid.Text = "0" Then
                    Exit Sub
                End If
            Else
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Job_Post_update", jobpost.id, jobpost.JobTitle, jobpost.JobType, jobpost.JobDescription, jobpost.EducationLevel, jobpost.ExperienceLevel, jobpost.Skills, jobpost.NoOfPosition, jobpost.Country, jobpost.Location, jobpost.MinAge, jobpost.MaxAge, jobpost.Currency, jobpost.MinSalary, jobpost.MaxSalary, Date.Now, jobpost.ClosingDate, jobpost.SendMail, jobpost.HiringManager, cboActive.SelectedItem.Text, cboSpecialisation.SelectedItem.Text, aminexpyr.Value, amaxexpyr.Value, Session("LoginID"), Session("LoginID"), cboOnline.SelectedItem.Text, cboOffice.SelectedValue, anote.Value, cboMinGrade.SelectedValue, cboMinOLGrade.SelectedValue)
            End If

            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Job_Post_Discipline_Delete", txtid.Text)
            For d As Integer = 0 To lstDiscipline.Items.Count - 1
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Job_Post_Discipline_Update", txtid.Text, lstDiscipline.Items(d).Value)
            Next

            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Job_Post_OLSubject_Delete", txtid.Text)
            For d As Integer = 0 To lstSchoolLeaving.Items.Count - 1
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Job_Post_OLSubject_Update", txtid.Text, lstSchoolLeaving.Items(d).Value)
            Next

            If NewValue.Trim = "" And OldValue.Trim = "" Then
            Else
                If txtid.Text <> "0" Then
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Updated", "Job Postings")
                Else
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Inserted", "Job Postings")
                End If
            End If

            'Send Mail to Employees with competencies
            If jobpost.SendMail = "Yes" Then

            End If

            Process.loadalert(divalert, msgalert, "Record saved!", "success")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Function GetIdentity(ByVal JobTitle As String, ByVal JobType As String, ByVal JobDescription As String, ByVal EducationLevel As String, ByVal ExperienceLevel As String, ByVal Skills As String, ByVal NoOfPosition As Integer, ByVal Country As String, ByVal Location As String, ByVal MinAge As String, ByVal MaxAge As String, ByVal Currency As String, ByVal MinSalary As Double, ByVal intMaxSalary As Double, ByVal ClosingDate As Date, ByVal SendMail As String, ByVal HiringManager As String, ByVal rActive As String, ByVal strSpecialisation As String, ByVal YrStart As Integer, ByVal YrEnd As Integer, ByVal isOnline As String) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Recruit_Job_Post_update"
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@Title", SqlDbType.VarChar).Value = JobTitle
            cmd.Parameters.Add("@Type", SqlDbType.VarChar).Value = JobType
            cmd.Parameters.Add("@JobDescription", SqlDbType.VarChar).Value = JobDescription
            cmd.Parameters.Add("@EducationLevel", SqlDbType.VarChar).Value = EducationLevel
            cmd.Parameters.Add("@ExperienceLevel", SqlDbType.VarChar).Value = ExperienceLevel
            cmd.Parameters.Add("@Skills", SqlDbType.VarChar).Value = Skills
            cmd.Parameters.Add("@NoOfPositions", SqlDbType.Int).Value = NoOfPosition
            cmd.Parameters.Add("@country", SqlDbType.VarChar).Value = Country
            cmd.Parameters.Add("@Location", SqlDbType.VarChar).Value = Location
            cmd.Parameters.Add("@StartAgeRange", SqlDbType.Int).Value = MinAge
            cmd.Parameters.Add("@EndAgeRange", SqlDbType.Int).Value = MaxAge
            cmd.Parameters.Add("@Currency", SqlDbType.VarChar).Value = Currency
            cmd.Parameters.Add("@StartSalaryRange", SqlDbType.Int).Value = MinSalary
            cmd.Parameters.Add("@EndSalaryRange", SqlDbType.Int).Value = intMaxSalary
            cmd.Parameters.Add("@DatePosted", SqlDbType.Date).Value = Date.Now
            cmd.Parameters.Add("@ClosingDate", SqlDbType.Date).Value = ClosingDate
            cmd.Parameters.Add("@Mail", SqlDbType.VarChar).Value = SendMail
            cmd.Parameters.Add("@HireManager", SqlDbType.VarChar).Value = HiringManager
            cmd.Parameters.Add("@RecruitActive", SqlDbType.VarChar).Value = rActive
            cmd.Parameters.Add("@Specialisation", SqlDbType.VarChar).Value = strSpecialisation
            cmd.Parameters.Add("@ExpStart", SqlDbType.Int).Value = YrStart
            cmd.Parameters.Add("@ExpEnd", SqlDbType.Int).Value = YrEnd
            cmd.Parameters.Add("@Addedby", SqlDbType.VarChar).Value = Session("LoginID")
            cmd.Parameters.Add("@Updatedby", SqlDbType.VarChar).Value = Session("LoginID")
            cmd.Parameters.Add("@TestOnline", SqlDbType.VarChar).Value = isOnline
            cmd.Parameters.Add("@company", SqlDbType.VarChar).Value = cboOffice.SelectedValue
            cmd.Parameters.Add("@comment", SqlDbType.VarChar).Value = anote.Value
            cmd.Parameters.Add("@minacademicgrade", SqlDbType.VarChar).Value = cboMinGrade.SelectedValue
            cmd.Parameters.Add("@minentrylevelgrade", SqlDbType.VarChar).Value = cboMinOLGrade.SelectedValue
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
            Return 0
        End Try
    End Function
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect("~/Module/Recruitment/jobpostings", True)
        Catch ex As Exception

        End Try

    End Sub
    Protected Sub cboLocation_CheckAllCheck(sender As Object, e As Telerik.Web.UI.RadComboBoxCheckAllCheckEventArgs)
        Try
            Process.LoadTextBoxFromCombo(txtLocation, cboLocation)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Private Sub cboLocation_ItemChecked(sender As Object, e As Telerik.Web.UI.RadComboBoxItemEventArgs)
        Try
            Process.LoadTextBoxFromCombo(txtLocation, cboLocation)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub cboclone_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboclone.SelectedIndexChanged
        Try
            If cboclone.SelectedItem.Text = "Yes" Then
                divclonesource.Visible = True
            Else
                divclonesource.Visible = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub radJobClone_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cbojobclone.SelectedIndexChanged
        Try

            Dim strUser As New DataSet
            strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Job_Requisition_Get", cbojobclone.SelectedValue)
            If strUser.Tables(0).Rows.Count > 0 Then
                Process.AssignRadComboValue(cboJobTitle, strUser.Tables(0).Rows(0).Item("Title").ToString)
                Process.AssignRadComboValue(cbojobtype, strUser.Tables(0).Rows(0).Item("type").ToString)
                Process.AssignRadComboValue(cboHiringManager, strUser.Tables(0).Rows(0).Item("HiringMgrName").ToString)
                Process.AssignRadComboValue(cboSpecialisation, strUser.Tables(0).Rows(0).Item("Specialization").ToString)
                ajobdesc.Value = strUser.Tables(0).Rows(0).Item("jobdesc").ToString
                ajobskill.Value = strUser.Tables(0).Rows(0).Item("skills").ToString
                Process.AssignRadComboValue(cboExperience, strUser.Tables(0).Rows(0).Item("ExperienceLevel").ToString)
                Process.AssignRadComboValue(cboeducation, strUser.Tables(0).Rows(0).Item("EducationLevel").ToString)
                aposition.Value = strUser.Tables(0).Rows(0).Item("NoOfPositions").ToString
                lblcountry.Text = strUser.Tables(0).Rows(0).Item("country").ToString
                txtLocation.Text = strUser.Tables(0).Rows(0).Item("location").ToString
                aminexpyr.Value = strUser.Tables(0).Rows(0).Item("Experience1").ToString
                amaxexpyr.Value = strUser.Tables(0).Rows(0).Item("Experience2").ToString
                aminage.Value = strUser.Tables(0).Rows(0).Item("StartAgeRange").ToString
                amaxage.Value = strUser.Tables(0).Rows(0).Item("EndAgeRange").ToString
            Else
                ajobdesc.Value = ""
                ajobskill.Value = ""
                Process.AssignRadComboValue(cboExperience, "")
                Process.AssignRadComboValue(cboeducation, "")
                aposition.Value = "0"                
                txtLocation.Text = ""
                aminexpyr.Value = "0"
                amaxexpyr.Value = "0"
                aminage.Value = "0"
                amaxage.Value = "0"
            End If
      

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub cboJobTitle_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboJobTitle.SelectedIndexChanged
        Try
            Dim strC As New DataSet
            strC = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Job_Title_Skills_Get_All", cboJobTitle.SelectedValue)
            For i As Integer = 0 To strC.Tables(0).Rows.Count - 1
                If i = 0 Then
                    ajobskill.Value = "* " & strC.Tables(0).Rows(i).Item("skills").ToString
                Else
                    ajobskill.Value = ajobskill.Value & vbNewLine & "* " & strC.Tables(0).Rows(i).Item("skills").ToString
                End If

            Next

            Dim strDesc As New DataSet
            strDesc = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Job_Titles_get", cboJobTitle.SelectedValue)
            If strDesc.Tables(0).Rows.Count > 0 Then
                ajobdesc.Value = strDesc.Tables(0).Rows(0).Item("jobdescription").ToString
            End If

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub radOffice_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboOffice.SelectedIndexChanged
        Try
            Dim strCountry As New DataSet
            strCountry = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Company_Structure_get", cboOffice.SelectedValue)
            lblcountry.Text = strCountry.Tables(0).Rows(0).Item("country").ToString

            Process.LoadRadComboTextAndValueP1(cboLocation, "location_get_country", lblcountry.Text, "name", "name", False)
        Catch ex As Exception
        End Try

    End Sub



    Protected Sub cboLocation_ItemChecked1(sender As Object, e As Telerik.Web.UI.RadComboBoxItemEventArgs) Handles cboLocation.ItemChecked
        Process.LoadTextBoxFromCombo(txtLocation, cboLocation)
    End Sub

    Private Sub cboDiscipline_CheckAllCheck(sender As Object, e As Telerik.Web.UI.RadComboBoxCheckAllCheckEventArgs) Handles cboDiscipline.CheckAllCheck
        Process.LoadListBoxFromCombo(lstDiscipline, cboDiscipline)
    End Sub

    Protected Sub cboDiscipline_ItemChecked(sender As Object, e As Telerik.Web.UI.RadComboBoxItemEventArgs) Handles cboDiscipline.ItemChecked
        'Process.LoadListBoxFromCombo(lstDiscipline, cboDiscipline)
        Try
            lstDiscipline.Items.Clear()
            Dim collection As IList(Of RadComboBoxItem) = cboDiscipline.CheckedItems
            If (collection.Count <> 0) Then
                For Each item As RadComboBoxItem In collection
                    Dim listitem As New RadListBoxItem()
                    listitem.Text = item.Text
                    listitem.Value = item.Value
                    lstDiscipline.Items.Add(listitem)
                    listitem.DataBind()
                Next
            Else
                lstDiscipline.Items.Clear()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cboSchoolLeaving_CheckAllCheck(sender As Object, e As Telerik.Web.UI.RadComboBoxCheckAllCheckEventArgs) Handles cboSchoolLeaving.CheckAllCheck
        Process.LoadListBoxFromCombo(lstSchoolLeaving, cboSchoolLeaving)
    End Sub

    Protected Sub cboSchoolLeaving_ItemChecked(sender As Object, e As Telerik.Web.UI.RadComboBoxItemEventArgs) Handles cboSchoolLeaving.ItemChecked
        'Process.LoadListBoxFromCombo(lstSchoolLeaving, cboSchoolLeaving)
        Try
            lstSchoolLeaving.Items.Clear()
            Dim collection As IList(Of RadComboBoxItem) = cboSchoolLeaving.CheckedItems
            If (collection.Count <> 0) Then
                For Each item As RadComboBoxItem In collection
                    Dim listitem As New RadListBoxItem()
                    listitem.Text = item.Text
                    listitem.Value = item.Value
                    lstSchoolLeaving.Items.Add(listitem)
                    listitem.DataBind()
                Next
            Else
                lstSchoolLeaving.Items.Clear()
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class