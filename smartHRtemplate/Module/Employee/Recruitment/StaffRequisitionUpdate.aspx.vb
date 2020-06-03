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

Public Class StaffRequisitionUpdate
    Inherits System.Web.UI.Page
    Dim jobpost As New clsJobPost
    Dim olddata(22) As String
    Dim AuthenCode As String = "STAFFREQUISITE"
    Dim AuthenCode2 As String = "APPSTAFFREQUISITE"

   
    Private Sub LoadDynamic()
        Try
        Catch ex As Exception

        End Try
    End Sub
    Private Sub DisableControl()
        Try
            'txtAgeMax.Enabled = False
            'txtAgeMin.Enabled = False
            'txtCompetency.Enabled = False
            'txtJobDesc.Enabled = False
            'txtJobTitle.Enabled = False
            'txtLocation.Enabled = False
            'txtPositions.Enabled = False
            'txtreason.Enabled = False
            'txtYrEnd.Enabled = False
            'txtYrStart.Enabled = False
            'cboCompetency.Enabled = False
            'cboEducation.Enabled = False
            'cboExperience.Enabled = False
            'cboLocation.Enabled = False
            'cboSpecialisation.Enabled = False
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
           

            If Not Me.IsPostBack Then

                Dim ismulti As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select isnull(ismulticompany,'No')  from general_info")
                If ismulti.ToLower = "no" Then
                    Process.LoadRadComboTextAndValueP2(radCompany, "Company_Structure_Get_ByLevel", "1", Session("Access"), "name", "name", False)
                Else
                    Process.LoadRadComboTextAndValueP2(radCompany, "Company_Structure_Get_ByLevel", "2", Session("Access"), "name", "name", False)
                End If
                

                radHiringType.Items.Clear()
                radHiringType.Items.Add("New")
                radHiringType.Items.Add("Replacement")
                radHiringType.Items.Add("Restructuring")
                radHiringType.Items.Add("Transfer")
                radHiringType.ToolTip = "A New Position is one that did not previously exist." & vbNewLine & "A Reclassified Position is a position to which there has been a significant change in duties and/or qualifications since it was last posted or classified." & vbNewLine & " A Replacement Position is a vacant position when trying to replace an exiting staff"


                Process.LoadRadDropDownTextAndValue(radJobType, "employment_status_get_all", "name", "name", False)

                Process.LoadRadComboTextAndValueInitiate(cboJobTitle, "Job_Titles_get_all", "--select--", "name", "name")
                Process.LoadRadComboTextAndValueInitiate(cboExperience, "Recruit_Experience_Level_get_all", "Any Experience Level", "name", "name")
                Process.LoadRadComboTextAndValueInitiate(cboEducation, "Education_get_all", "Any Education Level", "name", "name")




                Process.LoadRadComboTextAndValue(cboSpecialisation, "Recruit_Specialization_Get_All", "name", "name")
                Process.LoadRadComboTextAndValueInitiate(cboGrade, "Job_Grade_get_all", "--Select--", "name", "name")

                If Request.QueryString("id") IsNot Nothing Then
                    Process.LoadRadComboTextAndValueP1(cboHiringManager, "Emp_PersonalDetail_Get_Employees", Process.GetCompanyName, "name", "EmpID", False)
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Job_Requisition_get", Request.QueryString("id"))
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    Process.AssignRadComboValue(cboJobTitle, strUser.Tables(0).Rows(0).Item("Title").ToString)

                    Process.AssignRadComboValue(radCompany, strUser.Tables(0).Rows(0).Item("CompanyName").ToString)
                    Process.LoadRadComboTextAndValueP1(cbooffice, "Company_Parent_Breakdown", radCompany.SelectedValue, "Companys", "Companys", False)
                    txtJobDesc.Text = strUser.Tables(0).Rows(0).Item("jobdesc").ToString

                    Process.AssignRadDropDownValue(radJobType, strUser.Tables(0).Rows(0).Item("Type").ToString)
                    Process.AssignRadComboValue(cbooffice, strUser.Tables(0).Rows(0).Item("Dept").ToString)
                    Process.AssignRadDropDownValue(radHiringType, strUser.Tables(0).Rows(0).Item("RecruitMode").ToString)
                    Process.AssignRadComboValue(cboGrade, strUser.Tables(0).Rows(0).Item("jobgrade").ToString)

                    txtreason.Text = strUser.Tables(0).Rows(0).Item("RecruitModeComment").ToString
                    'Education
                    Process.AssignRadComboValue(cboEducation, strUser.Tables(0).Rows(0).Item("EducationLevel").ToString)

                    'cboExperience
                    Process.AssignRadComboValue(cboExperience, strUser.Tables(0).Rows(0).Item("ExperienceLevel").ToString)
                    'txtSkills.Text = strUser.Tables(0).Rows(0).Item("Skills").ToString
                    txtCompetency.Text = strUser.Tables(0).Rows(0).Item("Skills").ToString
                    txtPositions.Text = strUser.Tables(0).Rows(0).Item("NoOfPositions").ToString


                    txtAgeMin.Text = strUser.Tables(0).Rows(0).Item("StartAgeRange").ToString
                    txtAgeMax.Text = strUser.Tables(0).Rows(0).Item("EndAgeRange").ToString
                    'Specialisation       
                    Process.AssignRadComboValue(cboSpecialisation, strUser.Tables(0).Rows(0).Item("specialization").ToString)

                    txtYrStart.Text = strUser.Tables(0).Rows(0).Item("experience1").ToString
                    txtYrEnd.Text = strUser.Tables(0).Rows(0).Item("experience2").ToString
                    Process.AssignRadComboValue(cboHiringManager, strUser.Tables(0).Rows(0).Item("hiringmanager").ToString)

                    ' Process.CheckComboFromText(txtCompetency, cboCompetency)
                    Process.CheckComboFromText(txtLocation, cboLocation)

                    lblcreatedby.Text = strUser.Tables(0).Rows(0).Item("AddedBy").ToString
                    datLastResumption.SelectedDate = strUser.Tables(0).Rows(0).Item("LastestResumption")

                    lblcreatedon.Text = strUser.Tables(0).Rows(0).Item("AddedOn").ToString
                    lblHOD.Text = strUser.Tables(0).Rows(0).Item("hodname").ToString
                    lblHODID.Text = strUser.Tables(0).Rows(0).Item("hod").ToString
                    lblupdatedon.Text = strUser.Tables(0).Rows(0).Item("UpdatedOn").ToString
                    lblupdatedby.Text = strUser.Tables(0).Rows(0).Item("UpdatedBy").ToString


                    Dim strAuto As New DataSet
                    strAuto = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Job_Requisition_Auto", cbooffice.SelectedValue, cboJobTitle.SelectedValue, cboGrade.SelectedValue, radJobType.SelectedValue, datLastResumption.SelectedDate, Process.FirstDateofYear, Process.LastDay(Date.Now.Year, Date.Now.Month))
                    If strAuto.Tables(0).Rows.Count > 0 Then
                        lblBNo.Text = strAuto.Tables(0).Rows(0).Item("amountrequired").ToString
                        lblCurrentPositions.Text = strAuto.Tables(0).Rows(0).Item("currentpos").ToString
                    End If

                    Process.LoadRadComboTextAndValueInitiateP1(cboLocation, "location_get_country", cbooffice.SelectedValue, "--Select--", "name", "name")
                    Dim txtLoc As New TextBox
                    txtLoc.Visible = False
                    Process.CheckComboFromText(txtLoc, cboLocation)
                    txtLocation.Text = strUser.Tables(0).Rows(0).Item("Location").ToString

                    If strUser.Tables(0).Rows(0).Item("finalstatus").ToString = "Approved" Then
                        Process.DisableButton(btnAdd)
                    End If
                    radCompany.Enabled = False
                    cbooffice.Enabled = False

                Else
                    txtid.Text = "0"
                    lnkApprovalStat.Visible = False
                    btnComplete.Visible = False
                    Process.LoadRadComboTextAndValueP1(cbooffice, "Company_Parent_Breakdown", radCompany.SelectedValue, "Companys", "Companys", False)
                    Process.LoadRadComboTextAndValueP1(cboHiringManager, "Emp_PersonalDetail_Get_Employees", radCompany.SelectedValue, "Employee2", "EmpID", False)
                End If
            End If

        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False And Process.AuthenAction(Session("role"), AuthenCode2, "Update") = False Then
                lblstatus.Text = "You don't have privilege to perform this action"
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
                Exit Sub
            End If

            

            'If radCompany.SelectedText.Contains("Select") Then
            '    lblstatus.Text = "Company required!"
            '    radCompany.Focus()
            '    Exit Sub
            'End If


            If IsNumeric(txtPositions.Text) = False Then
                lblstatus.Text = "Number of Position must be numeric only!"
                txtPositions.Focus()
                Exit Sub
            End If

            If IsNumeric(txtAgeMin.Text) = False Then
                lblstatus.Text = "Minimum Age must be numeric only!"
                txtAgeMin.Focus()
                Exit Sub
            End If

            If IsNumeric(txtAgeMax.Text) = False Then
                lblstatus.Text = "Maximum Age must be numeric only!"
                txtAgeMax.Focus()
                Exit Sub
            End If

            If IsNumeric(txtYrStart.Text) = False Or IsNumeric(txtYrEnd.Text) = False Then
                lblstatus.Text = "Years of Experience must be numeric only, set to 0 if no experience is required!"
                txtYrStart.Focus()
                Exit Sub
            End If

            If CInt(txtPositions.Text) < 0 Then
                lblstatus.Text = "Set Number of Positions required!"
                txtPositions.Focus()
                Exit Sub
            End If

            If CInt(txtAgeMax.Text) < CInt(txtAgeMin.Text) Then
                lblstatus.Text = "Maximum Age cannot be less than Minimum Age!"
                txtAgeMax.Focus()
                Exit Sub
            End If

            If CInt(txtYrEnd.Text) < CInt(txtYrStart.Text) Then
                lblstatus.Text = "Maximum Years of Experience cannot be less than Minimum Years!"
                txtAgeMax.Focus()
                Exit Sub
            End If

            If datLastResumption.SelectedDate Is Nothing Then
                lblstatus.Text = "A lastest resumption date is required!"
                datLastResumption.Focus()
                Exit Sub
            End If

            If datLastResumption.SelectedDate <= Date.Now Then
                lblstatus.Text = "A lastest resumption date is required!"
                datLastResumption.Focus()
                Exit Sub
            End If


            btnAdd.Enabled = False

            If txtid.Text = "0" Then
                txtid.Text = GetIdentity(cboJobTitle.SelectedValue, radJobType.SelectedText, radHiringType.SelectedText, txtreason.Text, txtPositions.Text, "", _
                                          txtLocation.Text, txtAgeMin.Text, txtAgeMax.Text, cboEducation.SelectedItem.Text, cboExperience.SelectedItem.Text, _
                                          txtCompetency.Text, cboSpecialisation.SelectedItem.Text, cboHiringManager.SelectedItem.Value, Session("HODID"), _
                                           cbooffice.SelectedValue, txtYrStart.Text, txtYrEnd.Text, txtJobDesc.Text, datLastResumption.SelectedDate, radCompany.SelectedValue, cboGrade.SelectedValue)
                If txtid.Text = "0" Then
                    lblstatus.Text = Process.strExp
                    Exit Sub
                End If
            Else
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Job_Requisition_update", txtid.Text, cboJobTitle.SelectedValue, radJobType.SelectedText, radHiringType.SelectedText, txtreason.Text, txtPositions.Text, "", _
                                          txtLocation.Text, txtAgeMin.Text, txtAgeMax.Text, cboEducation.SelectedItem.Text, cboExperience.SelectedItem.Text, _
                                          txtCompetency.Text, cboSpecialisation.SelectedItem.Text, cboHiringManager.SelectedItem.Value, Session("HODID"), _
                                           cbooffice.SelectedValue, txtYrStart.Text, txtYrEnd.Text, Session("UserEmpID"), txtJobDesc.Text, datLastResumption.SelectedDate, radCompany.SelectedValue, cboGrade.SelectedValue)
            End If



            'HODApproval



            lblstatus.Text = "Staff Requisition Record saved"
            btnComplete.Visible = True
            If CInt(lblCurrentPositions.Text) + CInt(txtPositions.Text) > CInt(lblBNo.Text) Then
                lblstatus.Text = "Record saved, Note: Total Number required and current staff exceeds budgeted positions!"
            End If
        Catch ex As Exception
            lblstatus.Text = ex.Message
        Finally
            btnAdd.Enabled = True
        End Try
    End Sub
    Private Function GetIdentity(ByVal JobTitle As String, ByVal JobType As String, ByVal recruitmode As String, ByVal recruitcomment As String, _
                                 ByVal NoOfPosition As Integer, ByVal Country As String, ByVal Location As String, _
                                 ByVal MinAge As String, ByVal MaxAge As String, ByVal EducationLevel As String, ByVal ExperienceLevel As String, ByVal Skills As String, _
                                 ByVal strSpecialisation As String, ByVal HiringManager As String, ByVal HOD As String, _
                                 ByVal dept As String, ByVal YrStart As Integer, ByVal YrEnd As Integer, ByVal jobdesc As String, ByVal resumedate As Date, company As String, sjobgrade As String) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Recruit_Job_Requisition_update"
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@Title", SqlDbType.VarChar).Value = JobTitle
            cmd.Parameters.Add("@JobType", SqlDbType.VarChar).Value = JobType
            cmd.Parameters.Add("@RecruitMode", SqlDbType.VarChar).Value = recruitmode
            cmd.Parameters.Add("@RecruitModeComment", SqlDbType.VarChar).Value = recruitcomment
            cmd.Parameters.Add("@NoOfPositions", SqlDbType.Int).Value = NoOfPosition
            cmd.Parameters.Add("@Country", SqlDbType.VarChar).Value = Country
            cmd.Parameters.Add("@Location", SqlDbType.VarChar).Value = Location
            cmd.Parameters.Add("@StartAgeRange", SqlDbType.Int).Value = MinAge
            cmd.Parameters.Add("@EndAgeRange", SqlDbType.Int).Value = MaxAge
            cmd.Parameters.Add("@EducationLevel", SqlDbType.VarChar).Value = EducationLevel
            cmd.Parameters.Add("@ExperienceLevel", SqlDbType.VarChar).Value = ExperienceLevel
            cmd.Parameters.Add("@Skills", SqlDbType.VarChar).Value = Skills
            cmd.Parameters.Add("@Specialisation", SqlDbType.VarChar).Value = strSpecialisation
            cmd.Parameters.Add("@HiringManager", SqlDbType.VarChar).Value = HiringManager
            cmd.Parameters.Add("@HOD", SqlDbType.VarChar).Value = HOD
            cmd.Parameters.Add("@Dept", SqlDbType.VarChar).Value = dept
            cmd.Parameters.Add("@Expr1", SqlDbType.Int).Value = YrStart
            cmd.Parameters.Add("@Expr2", SqlDbType.Int).Value = YrEnd
            cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = Session("UserEmpID")
            cmd.Parameters.Add("@jobdesc", SqlDbType.VarChar).Value = jobdesc
            cmd.Parameters.Add("@LastestResumption", SqlDbType.Date).Value = Process.DDMONYYYY(resumedate)
            cmd.Parameters.Add("@Company", SqlDbType.VarChar).Value = company
            cmd.Parameters.Add("@jobgrade", SqlDbType.VarChar).Value = sjobgrade
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception
            Process.strExp = ex.Message
            Return 0
        End Try
    End Function
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Response.Write("<script language='javascript'> { self.close() }</script>")
        Catch ex As Exception

        End Try
    End Sub







    Protected Sub cboLocation_CheckAllCheck(sender As Object, e As Telerik.Web.UI.RadComboBoxCheckAllCheckEventArgs) Handles cboLocation.CheckAllCheck
        Try
            Process.LoadTextBoxFromCombo(txtLocation, cboLocation)
        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub

    Private Sub cboLocation_ItemChecked(sender As Object, e As Telerik.Web.UI.RadComboBoxItemEventArgs) Handles cboLocation.ItemChecked
        Try
            Process.LoadTextBoxFromCombo(txtLocation, cboLocation)
        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub



    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Process.LoadTextBoxFromCombo(txtLocation, cboLocation)
    End Sub

    Protected Sub radHiringType_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.DropDownListEventArgs) Handles radHiringType.SelectedIndexChanged
        Try
            If radHiringType.SelectedText = "New" Then
                lblrequisition.Text = "Please indicate why this position is needed"
            ElseIf radHiringType.SelectedText = "Replacement" Then
                lblrequisition.Text = "Please indicate the person(s) who is leaving"
            Else
                lblrequisition.Text = "Comment on requisition"
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub radOffice_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cbooffice.SelectedIndexChanged
        Try
            'get country to populate location
            Process.LoadRadComboTextAndValueInitiateP1(cboLocation, "location_get_country", cbooffice.SelectedValue, "--Select--", "name", "name")
            'Process.LoadRadComboTextAndValueInitiateP1(cboGrade, "Recruit_Job_Requisition_Budgeted_Grades", radOffice.SelectedValue, "--Select--", "jobgrade", "jobgrade")
            Session("HODID") = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select isnull(head,'') head from Company_Structure a where a.name = '" & cbooffice.SelectedValue & "'")

            Dim strHOD As New DataSet
            strHOD = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_PersonalDetail_get", Session("HODID"))
            If strHOD.Tables(0).Rows.Count > 0 Then
                Session("ManagerEmail") = strHOD.Tables(0).Rows(0).Item("workEmail").ToString
                lblHOD.Text = strHOD.Tables(0).Rows(0).Item("firstName").ToString & " " & strHOD.Tables(0).Rows(0).Item("LastName").ToString
                lblHODID.Text = strHOD.Tables(0).Rows(0).Item("empid").ToString
            Else
                lblHOD.Text = ""
                lblHODID.Text = ""
            End If

            If radJobType.SelectedValue IsNot Nothing And cbooffice.SelectedValue IsNot Nothing And cboGrade.SelectedValue IsNot Nothing And datLastResumption.SelectedDate IsNot Nothing Then
                Dim strAuto As New DataSet
                strAuto = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Job_Requisition_Auto", cbooffice.SelectedValue, cboJobTitle.SelectedValue, cboGrade.SelectedValue, radJobType.SelectedValue, datLastResumption.SelectedDate, Process.FirstDateofYear, Process.LastDay(Date.Now.Year, Date.Now.Month))
                If strAuto.Tables(0).Rows.Count > 0 Then
                    'txtJobDesc.Text = strAuto.Tables(0).Rows(0).Item("jobdescription").ToString
                    'txtCompetency.Text = strAuto.Tables(0).Rows(0).Item("skills").ToString
                    txtAgeMin.Text = strAuto.Tables(0).Rows(0).Item("age_min").ToString
                    txtAgeMax.Text = strAuto.Tables(0).Rows(0).Item("age_max").ToString
                    lblBNo.Text = strAuto.Tables(0).Rows(0).Item("amountrequired").ToString
                    lblCurrentPositions.Text = strAuto.Tables(0).Rows(0).Item("currentpos").ToString
                    Process.AssignRadComboValue(cboEducation, strAuto.Tables(0).Rows(0).Item("min_education").ToString)
                    'Process.CheckComboFromText(txtCompetency, cboCompetency)
                Else
                    ' txtJobDesc.Text = ""
                    ' txtCompetency.Text = ""
                    txtAgeMin.Text = "0"
                    txtAgeMax.Text = "0"
                    lblBNo.Text = "0"
                    lblCurrentPositions.Text = "0"
                    'Process.CheckComboFromText(txtCompetency, cboCompetency)
                End If

                'lblBNo.Text = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Recruit_Job_Requisition_Budgeted_No", radOffice.SelectedValue, cboGrade.SelectedValue, datLastResumption.SelectedDate)
            Else
                'txtJobDesc.Text = ""
                'txtCompetency.Text = ""
                txtAgeMin.Text = "0"
                txtAgeMax.Text = "0"
                lblBNo.Text = "0"
                lblCurrentPositions.Text = "0"
                'Process.CheckComboFromText(txtCompetency, cboCompetency)
            End If
        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub

    Protected Sub datLastResumption_SelectedDateChanged(sender As Object, e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles datLastResumption.SelectedDateChanged
        Try
            '    If cboJobTitle.SelectedValue IsNot Nothing And radJobType.SelectedValue IsNot Nothing And cbooffice.SelectedValue IsNot Nothing And cboGrade.SelectedValue IsNot Nothing And datLastResumption.SelectedDate IsNot Nothing Then
            '        Dim strAuto As New DataSet
            '        strAuto = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Job_Requisition_Auto", cbooffice.SelectedValue, cboJobTitle.SelectedValue, cboGrade.SelectedValue, radJobType.SelectedValue, datLastResumption.SelectedDate, Process.FirstDateofYear, Process.LastDay(Date.Now.Year, Date.Now.Month))
            '        If strAuto.Tables(0).Rows.Count > 0 Then
            '            ' txtJobDesc.Text = strAuto.Tables(0).Rows(0).Item("jobdescription").ToString
            '            'txtCompetency.Text = strAuto.Tables(0).Rows(0).Item("skills").ToString
            '            txtAgeMin.Text = strAuto.Tables(0).Rows(0).Item("age_min").ToString
            '            txtAgeMax.Text = strAuto.Tables(0).Rows(0).Item("age_max").ToString
            '            lblBNo.Text = strAuto.Tables(0).Rows(0).Item("amountrequired").ToString
            '            lblCurrentPositions.Text = strAuto.Tables(0).Rows(0).Item("currentpos").ToString
            '            Process.AssignRadComboValue(cboEducation, strAuto.Tables(0).Rows(0).Item("min_education").ToString)
            '            'Process.CheckComboFromText(txtCompetency, cboCompetency)
            '        Else
            '            'txtJobDesc.Text = ""
            '            'txtCompetency.Text = ""
            '            txtAgeMin.Text = "0"
            '            txtAgeMax.Text = "0"
            '            lblBNo.Text = "0"
            '            lblCurrentPositions.Text = "0"
            '            'Process.CheckComboFromText(txtCompetency, cboCompetency)
            '        End If

            '        'lblBNo.Text = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Recruit_Job_Requisition_Budgeted_No", radOffice.SelectedValue, cboGrade.SelectedValue, datLastResumption.SelectedDate)
            '    Else
            '        'txtJobDesc.Text = ""
            '        'txtCompetency.Text = ""
            '        txtAgeMin.Text = "0"
            '        txtAgeMax.Text = "0"
            '        lblBNo.Text = "0"
            '        lblCurrentPositions.Text = "0"
            '        'Process.CheckComboFromText(txtCompetency, cboCompetency)
            '    End If

        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub

   

    Protected Sub cboGrade_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboGrade.SelectedIndexChanged
        Try
            If cboJobTitle.SelectedValue IsNot Nothing And radJobType.SelectedValue IsNot Nothing And cbooffice.SelectedValue IsNot Nothing And cboGrade.SelectedValue IsNot Nothing And datLastResumption.SelectedDate IsNot Nothing Then
                Dim strAuto As New DataSet
                strAuto = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Job_Requisition_Auto", cbooffice.SelectedValue, cboJobTitle.SelectedValue, cboGrade.SelectedValue, radJobType.SelectedValue, datLastResumption.SelectedDate, Process.FirstDateofYear, Process.LastDay(Date.Now.Year, Date.Now.Month))
                If strAuto.Tables(0).Rows.Count > 0 Then
                    'txtJobDesc.Text = strAuto.Tables(0).Rows(0).Item("jobdescription").ToString
                    'txtCompetency.Text = strAuto.Tables(0).Rows(0).Item("skills").ToString
                    txtAgeMin.Text = strAuto.Tables(0).Rows(0).Item("age_min").ToString
                    txtAgeMax.Text = strAuto.Tables(0).Rows(0).Item("age_max").ToString
                    lblBNo.Text = strAuto.Tables(0).Rows(0).Item("amountrequired").ToString
                    lblCurrentPositions.Text = strAuto.Tables(0).Rows(0).Item("currentpos").ToString
                    'Process.CheckComboFromText(txtCompetency, cboCompetency)
                    Process.AssignRadComboValue(cboEducation, strAuto.Tables(0).Rows(0).Item("min_education").ToString)
                Else
                    txtJobDesc.Text = ""
                    txtCompetency.Text = ""
                    txtAgeMin.Text = "0"
                    txtAgeMax.Text = "0"
                    lblBNo.Text = "0"
                    lblCurrentPositions.Text = "0"
                    'Process.CheckComboFromText(txtCompetency, cboCompetency)
                End If

                'lblBNo.Text = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Recruit_Job_Requisition_Budgeted_No", radOffice.SelectedValue, cboGrade.SelectedValue, datLastResumption.SelectedDate)
            Else
                'txtJobDesc.Text = ""
                'txtCompetency.Text = ""
                txtAgeMin.Text = "0"
                txtAgeMax.Text = "0"
                lblBNo.Text = "0"
                lblCurrentPositions.Text = "0"
                'Process.CheckComboFromText(txtCompetency, cboCompetency)
            End If
        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub

    Protected Sub radJobType_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.DropDownListEventArgs) Handles radJobType.SelectedIndexChanged
        If cboJobTitle.SelectedValue IsNot Nothing And radJobType.SelectedValue IsNot Nothing And cbooffice.SelectedValue IsNot Nothing And cboGrade.SelectedValue IsNot Nothing And datLastResumption.SelectedDate IsNot Nothing Then
            Dim strAuto As New DataSet
            strAuto = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Job_Requisition_Auto", cbooffice.SelectedValue, cboJobTitle.SelectedValue, cboGrade.SelectedValue, radJobType.SelectedValue, datLastResumption.SelectedDate, Process.FirstDateofYear, Process.LastDay(Date.Now.Year, Date.Now.Month))
            If strAuto.Tables(0).Rows.Count > 0 Then
                'txtJobDesc.Text = strAuto.Tables(0).Rows(0).Item("jobdescription").ToString
                'txtCompetency.Text = strAuto.Tables(0).Rows(0).Item("skills").ToString
                txtAgeMin.Text = strAuto.Tables(0).Rows(0).Item("age_min").ToString
                txtAgeMax.Text = strAuto.Tables(0).Rows(0).Item("age_max").ToString
                lblBNo.Text = strAuto.Tables(0).Rows(0).Item("amountrequired").ToString
                lblCurrentPositions.Text = strAuto.Tables(0).Rows(0).Item("currentpos").ToString
                'Process.CheckComboFromText(txtCompetency, cboCompetency)
                Process.AssignRadComboValue(cboEducation, strAuto.Tables(0).Rows(0).Item("min_education").ToString)
            Else
                'txtJobDesc.Text = ""
                'txtCompetency.Text = ""
                txtAgeMin.Text = "0"
                txtAgeMax.Text = "0"
                lblBNo.Text = "0"
                lblCurrentPositions.Text = "0"
                'Process.CheckComboFromText(txtCompetency, cboCompetency)
            End If

            'lblBNo.Text = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Recruit_Job_Requisition_Budgeted_No", radOffice.SelectedValue, cboGrade.SelectedValue, datLastResumption.SelectedDate)
        Else
            'txtJobDesc.Text = ""
            'txtCompetency.Text = ""
            txtAgeMin.Text = "0"
            txtAgeMax.Text = "0"
            lblBNo.Text = "0"
            lblCurrentPositions.Text = "0"
            'Process.CheckComboFromText(txtCompetency, cboCompetency)
        End If
    End Sub

    Protected Sub lnkApprovalStat_Click(sender As Object, e As EventArgs) Handles lnkApprovalStat.Click
        Try
            Dim url As String = "StaffRequisitionStat.aspx?id=" & txtid.Text
            Dim s As String = "window.open('" & url + "', 'popup_window', 'width=600,height=700,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no');"
            ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)

        Catch ex As Exception
            lblstatus.Text = ex.Message
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & lblstatus.Text + "')", True)
        End Try
    End Sub

    Protected Sub cboJobTitle_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboJobTitle.SelectedIndexChanged
        Try

            Dim strDesc As New DataSet
            strDesc = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Job_Titles_get", cboJobTitle.SelectedValue)
            txtJobDesc.Text = strDesc.Tables(0).Rows(0).Item("jobdescription").ToString
            Process.LoadTextBoxP1(txtCompetency, "Job_Title_Skills_Get_All_2", cboJobTitle.SelectedValue, "skills")
        Catch ex As Exception
        End Try
    End Sub

    

  
    Protected Sub btnComplete_Click(sender As Object, e As EventArgs) Handles btnComplete.Click

        Try
            lblstatus.Text = ""
            Dim msg As String = ""
            Dim msgbuild As New StringBuilder()
            Dim confirmValue As String = Request.Form("confirm_value")
            If confirmValue = "No" Then
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Cancelled" + "')", True)
            Else
                lblstatus.Text = ""
                'Send Mail to Employees with competencies
                Dim strHOD As New DataSet
                Dim hodemail As String = ""
                Dim hodname As String = ""
                Dim hiringmgrname As String = ""

                strHOD = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "select name from dbo.Employees_All where EmpID = '" & cboHiringManager.SelectedItem.Value & "'")
                If strHOD.Tables(0).Rows.Count > 0 Then
                    hiringmgrname = strHOD.Tables(0).Rows(0).Item("name").ToString
                End If
                Process.requestedURL = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "menu_get_link", AuthenCode, 1)
                Process.Staff_Requisition_Alert_Approver(hiringmgrname, cboJobTitle.SelectedValue, cbooffice.SelectedValue, txtPositions.Text, datLastResumption.SelectedDate, "", Session("UserEmpID"), lblHODID.Text, Process.ApplicationURL & Process.requestedURL)
                lblstatus.Text = "Staff Requisition is forwarded for approval"
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & lblstatus.Text + "')", True)

            End If
        Catch ex As Exception
            lblstatus.Text = ex.Message
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try


    End Sub

    Protected Sub radCompany_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles radCompany.SelectedIndexChanged
        Process.LoadRadComboTextAndValueInitiateP1(cbooffice, "Company_Parent_Breakdown", radCompany.SelectedValue, "--select--", "Companys", "Companys")
        Process.LoadRadComboTextAndValueP1(cboHiringManager, "Emp_PersonalDetail_Get_Employees", radCompany.SelectedValue, "Employee2", "EmpID", False)
    End Sub
End Class