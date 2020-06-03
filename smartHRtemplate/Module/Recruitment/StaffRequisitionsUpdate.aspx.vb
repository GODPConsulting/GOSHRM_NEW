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

Public Class StaffRequisitionsUpdate
    Inherits System.Web.UI.Page
    Dim jobpost As New clsJobPost
    Dim olddata(22) As String
    Dim AuthenCode As String = "STAFFREQUISITEFORM"
    Dim AuthenCode2 As String = "STAFFREQUISITE"
    Public req As String
    Public bug As String

    'HR View on Requistion Detail
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
                    Process.LoadRadComboTextAndValueP2(cbocompany, "Company_Structure_Get_ByLevel", "1", Session("Access"), "name", "name", False)
                    cbocompany.Visible = False
                    Process.AssignRadComboValue(cbocompany, Session("organisation"))
                Else
                    Process.LoadRadComboTextAndValueP2(cboCompany, "Company_Structure_Get_ByLevel", "2", Session("Access"), "name", "name", False)
                End If


                cborequisition.Items.Clear()
                cborequisition.Items.Add("New")
                cborequisition.Items.Add("Replacement")
                cborequisition.Items.Add("Restructuring")
                cborequisition.Items.Add("Transfer")
                cborequisition.ToolTip = "A New Position is one that did not previously exist." & vbNewLine & "A Reclassified Position is a position to which there has been a significant change in duties and/or qualifications since it was last posted or classified." & vbNewLine & " A Replacement Position is a vacant position when trying to replace an exiting staff"

                Process.LoadRadComboTextAndValue(cbojobtype, "employment_status_get_all", "name", "name", False)

                Process.LoadRadComboTextAndValueInitiate(cbojobtitle, "Job_Titles_get_all", "--select--", "name", "name")
                Process.LoadRadComboTextAndValueInitiate(cboexperience, "Recruit_Experience_Level_get_all", "Any Experience Level", "name", "name")
                Process.LoadRadComboTextAndValueInitiate(cboeducation, "Education_get_all", "Any Education Level", "name", "name")
                Process.LoadRadComboTextAndValue(cbospecialisation, "Recruit_Specialization_Get_All", "name", "name")
                Process.LoadRadComboTextAndValueInitiate(cbograde, "Job_Grade_get_all", "--Select--", "name", "name")
                txtid.Text = "0"

                If Request.QueryString("id") IsNot Nothing Then
                    txtid.Text = Request.QueryString("id")
                End If

                If Request.QueryString("id1") IsNot Nothing Then
                    txtid.Text = Request.QueryString("id1")
                End If

                'If Request.QueryString("id2") IsNot Nothing Then
                '    txtid.Text = Request.QueryString("id2")
                '    btComplete.Visible = False
                'End If

                If txtid.Text <> "0" Then
                    Dim strUser As New DataSet
                    Process.LoadRadComboTextAndValueP1(cboHiringManager, "Emp_PersonalDetail_Get_Employees", Process.GetCompanyName, "name", "EmpID", False)
                    Process.LoadRadComboTextAndValueP1(cbooffice, "Company_Parent_Breakdown", Process.GetCompanyName, "Companys", "Companys", False)
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Job_Requisition_get", txtid.Text)
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    Process.AssignRadComboValue(cbojobtitle, strUser.Tables(0).Rows(0).Item("Title").ToString)

                    Process.AssignRadComboValue(cbocompany, strUser.Tables(0).Rows(0).Item("CompanyName").ToString)

                    ajobdesc.Value = strUser.Tables(0).Rows(0).Item("jobdesc").ToString
                    Process.AssignRadComboValue(cbojobtype, strUser.Tables(0).Rows(0).Item("Type").ToString)
                    'Process.AssignRadComboValue(cbooffice, strUser.Tables(0).Rows(0).Item("Dept").ToString)
                    cbooffice.SelectedValue = strUser.Tables(0).Rows(0).Item("Dept").ToString()
                    Process.AssignRadComboValue(cborequisition, strUser.Tables(0).Rows(0).Item("RecruitMode").ToString)

                    Process.AssignRadComboValue(cbograde, strUser.Tables(0).Rows(0).Item("jobgrade").ToString)
                    areason.Value = strUser.Tables(0).Rows(0).Item("RecruitModeComment").ToString
                    'Education
                    Process.AssignRadComboValue(cboeducation, strUser.Tables(0).Rows(0).Item("EducationLevel").ToString)
                    'cboExperience
                    Process.AssignRadComboValue(cboexperience, strUser.Tables(0).Rows(0).Item("ExperienceLevel").ToString)                    
                    askills.Value = strUser.Tables(0).Rows(0).Item("Skills").ToString

                    areqposition.Value = strUser.Tables(0).Rows(0).Item("NoOfPositions").ToString
                    Dim hodname As String = strUser.Tables(0).Rows(0).Item("HODApproval").ToString

                    txtLocation.Text = strUser.Tables(0).Rows(0).Item("Location").ToString
                    aminage.Value = strUser.Tables(0).Rows(0).Item("StartAgeRange").ToString
                    amaxage.Value = strUser.Tables(0).Rows(0).Item("EndAgeRange").ToString
                    'Specialisation       
                    Process.AssignRadComboValue(cbospecialisation, strUser.Tables(0).Rows(0).Item("specialization").ToString)

                    aminexpyr.Value = strUser.Tables(0).Rows(0).Item("experience1").ToString
                    amaxexpyr.Value = strUser.Tables(0).Rows(0).Item("experience2").ToString
                    ahod.Value = strUser.Tables(0).Rows(0).Item("hodname").ToString
                    Process.AssignRadComboValue(cboHiringManager, strUser.Tables(0).Rows(0).Item("hiringmanager").ToString)
                    datLastResumption.SelectedDate = strUser.Tables(0).Rows(0).Item("LastestResumption")
                    Process.CheckComboFromText(txtLocation, cbolocation)

                    If IsDate(strUser.Tables(0).Rows(0).Item("AddedOn")) = True Then
                        createdon.InnerText = "Created on " & CDate(strUser.Tables(0).Rows(0).Item("AddedOn")).ToLongDateString & " by " & strUser.Tables(0).Rows(0).Item("addedby")
                    End If
                    If IsDate(strUser.Tables(0).Rows(0).Item("UpdatedOn")) = True Then
                        updatedon.InnerText = "Last modified on " & CDate(strUser.Tables(0).Rows(0).Item("UpdatedOn")).ToLongDateString & " by " & strUser.Tables(0).Rows(0).Item("updatedby")
                    End If

                    Dim strAuto As New DataSet
                    strAuto = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Job_Requisition_Auto", cbooffice.SelectedValue, cbojobtitle.SelectedValue, cbograde.SelectedValue, cbojobtype.SelectedValue, datLastResumption.SelectedDate, Process.FirstDateofYear, Process.LastDay(Date.Now.Year, Date.Now.Month))
                    If strAuto.Tables(0).Rows.Count > 0 Then
                        abudgetposition.Value = strAuto.Tables(0).Rows(0).Item("amountrequired").ToString
                        afilledposition.Value = strAuto.Tables(0).Rows(0).Item("currentpos").ToString
                    End If

                    Process.LoadRadComboTextAndValueInitiateP1(cbolocation, "location_get_country", cbooffice.SelectedValue, "--Select--", "name", "name")
                    Dim txtLoc As New TextBox
                    txtLoc.Visible = False
                    Process.CheckComboFromText(txtLoc, cbolocation)
                    If strUser.Tables(0).Rows(0).Item("finalstatus").ToString = "Approved" Then
                        btnupdate.Attributes.Add("disabled", "disabled")
                        'btComplete.Visible = False
                    End If

                    cbooffice.Enabled = False
                    cbocompany.Enabled = False
                    cbojobtype.Enabled = False
                    cbograde.Enabled = False

                    If strUser.Tables(0).Rows(0).Item("addedby") <> Session("EmpName") Then
                        btComplete.Enabled = False
                    End If

                    If hodname = "Approved" Then
                        btComplete.Enabled = False
                    End If
                Else
                    Process.LoadRadComboTextAndValueP1(cboHiringManager, "Emp_PersonalDetail_Get_Employees", Process.GetCompanyName, "name", "EmpID", False)
                    Process.LoadRadComboTextAndValueP1(cbooffice, "Company_Parent_Breakdown", cbocompany.SelectedValue, "Companys", "Companys", False)
                    txtid.Text = "0"
                    approvallink.Visible = False
                    'btComplete.Enabled = False


                End If
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            Dim lblstatus As New StringBuilder
            If cbocompany.SelectedValue Is Nothing Then
                lblstatus.AppendLine("Company required!")
            End If


            If IsNumeric(areqposition.Value) = False Then
                lblstatus.AppendLine("Number of Position must be numeric only!")
            End If

            If IsNumeric(aminage.Value) = False Then
                lblstatus.AppendLine("Minimum Age must be numeric only!")
            End If

            If IsNumeric(amaxage.Value) = False Then
                lblstatus.AppendLine("Maximum Age must be numeric only!")
            End If

            If IsNumeric(aminexpyr.Value) = False Or IsNumeric(amaxexpyr.Value) = False Then
                lblstatus.AppendLine("Years of Experience must be numeric only, set to 0 if no experience is required!")
            End If

            If CInt(areqposition.Value) < 0 Then
                lblstatus.AppendLine("Set Number of Positions required!")
            End If

            If CInt(amaxage.Value) < CInt(aminage.Value) Then
                lblstatus.AppendLine("Maximum Age cannot be less than Minimum Age!")
            End If

            If CInt(amaxexpyr.Value) < CInt(aminexpyr.Value) Then
                lblstatus.AppendLine("Maximum Years of Experience cannot be less than Minimum Years!")
            End If

            If datLastResumption.SelectedDate Is Nothing Then
                lblstatus.AppendLine("A lastest resumption date is required!")
            End If

            If datLastResumption.SelectedDate <= Date.Now Then
                lblstatus.AppendLine("A lastest resumption date is required!")
            End If

            If lblstatus.Length > 0 Then
                Process.loadalert(divalert, msgalert, lblstatus.ToString, "warning")
                Exit Sub
            End If

            Dim astat As String = ""
            If txtid.Text = "0" Then
                txtid.Text = GetIdentity(cbojobtitle.SelectedValue, cbojobtype.SelectedValue, cborequisition.SelectedItem.Text, areason.Value, areqposition.Value, "", _
                                          txtLocation.Text, aminage.Value, amaxage.Value, cboeducation.SelectedItem.Text, cboexperience.SelectedItem.Text, _
                                          askills.Value, cbospecialisation.SelectedItem.Text, cboHiringManager.SelectedItem.Value, Session("ManagerID"), _
                                           cbooffice.SelectedItem.Value, aminexpyr.Value, amaxexpyr.Value, ajobdesc.Value, datLastResumption.SelectedDate, cbocompany.SelectedValue, cbograde.SelectedValue)
                If txtid.Text = "0" Then
                    Exit Sub
                End If
                btComplete.Enabled = True
                'approvallink.Visible = False
                astat = "Staff Requisition Record saved, click complete to request Department Head approval for requisition"
            Else
                astat = "Staff Requisition Record saved"
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Job_Requisition_update", txtid.Text, cbojobtitle.SelectedValue, cbojobtype.SelectedValue, cborequisition.SelectedItem.Text, areason.Value, areqposition.Value, "", _
                                          txtLocation.Text, aminage.Value, amaxage.Value, cboeducation.SelectedItem.Text, cboexperience.SelectedItem.Text, _
                                          askills.Value, cbospecialisation.SelectedItem.Text, cboHiringManager.SelectedItem.Value, Session("ManagerID"), _
                                           cbooffice.SelectedItem.Value, aminexpyr.Value, amaxexpyr.Value, Session("UserEmpID"), ajobdesc.Value, datLastResumption.SelectedDate, cbocompany.SelectedValue, cbograde.SelectedValue)
            End If


            'Send Mail to Employees with competencies
            Dim strHOD As New DataSet
            Dim strHR As New DataSet
            Dim hodemail As String = ""
            Dim hodname As String = ""
            Dim hiringmgrname As String = ""
            Dim iniemail As String = ""

            Process.loadalert(divalert, msgalert, astat, "success")
            'approvallink.Visible = False
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
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
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
            Return 0
        End Try
    End Function
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            If Request.QueryString("id") IsNot Nothing Then
                Response.Redirect("~/Module/Recruitment/StaffRequisitionForm", True)
            ElseIf Request.QueryString("id2") IsNot Nothing Then
                Response.Redirect("~/Module/Employee/Recruitment/StaffRequisitionApprove", True)
            ElseIf Request.QueryString("emp") IsNot Nothing Then
                Response.Redirect("~/Module/Employee/Recruitment/StaffRequisition", True)
            ElseIf Request.QueryString("id1") IsNot Nothing Then
                Response.Redirect("~/Module/Employee/Recruitment/StaffRequisition.aspx", True)
            Else
                Response.Redirect("~/Module/Recruitment/StaffRequisitionForm", True)
            End If

        Catch ex As Exception

        End Try
    End Sub





    Protected Sub cboLocation_CheckAllCheck(sender As Object, e As Telerik.Web.UI.RadComboBoxCheckAllCheckEventArgs)
        Try
            Process.LoadTextBoxFromCombo(txtLocation, cbolocation)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Private Sub cboLocation_ItemChecked(sender As Object, e As Telerik.Web.UI.RadComboBoxItemEventArgs)
        Try
            Process.LoadTextBoxFromCombo(txtLocation, cbolocation)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


    Protected Sub cborequisition_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cborequisition.SelectedIndexChanged
        Try
            If cborequisition.SelectedItem.Text = "New" Then
                lbrequisition.InnerText = "Please indicate why this position is needed"
            ElseIf cborequisition.SelectedItem.Text = "Replacement" Then
                lbrequisition.InnerText = "Please indicate the person(s) who is leaving"
            Else
                lbrequisition.InnerText = "Comment on requisition"
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub cbooffice_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cbooffice.SelectedIndexChanged
        Try
            'get country to populate location
            Process.LoadRadComboTextAndValueInitiateP1(cbolocation, "location_get_country", cbooffice.SelectedValue, "--Select--", "name", "name")
            'Process.LoadRadComboTextAndValueInitiateP1(cboGrade, "Recruit_Job_Requisition_Budgeted_Grades", cbooffice.SelectedValue, "--Select--", "jobgrade", "jobgrade")
            Session("ManagerID") = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select isnull(head,'') head from Company_Structure a where a.name = '" & cbooffice.SelectedValue & "'")

            Dim strHOD As New DataSet
            strHOD = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_PersonalDetail_get", Session("ManagerID"))
            If strHOD.Tables(0).Rows.Count > 0 Then
                Session("ManagerEmail") = strHOD.Tables(0).Rows(0).Item("workEmail").ToString
                ahod.Value = strHOD.Tables(0).Rows(0).Item("firstName").ToString & " " & strHOD.Tables(0).Rows(0).Item("LastName").ToString
            Else
                ahod.Value = ""
            End If

            If cbojobtype.SelectedValue IsNot Nothing And cbooffice.SelectedValue IsNot Nothing And cbograde.SelectedValue IsNot Nothing And datLastResumption.SelectedDate IsNot Nothing Then
                Dim strAuto As New DataSet
                strAuto = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Job_Requisition_Auto", cbooffice.SelectedValue, cbojobtitle.SelectedValue, cbograde.SelectedValue, cbojobtype.SelectedValue, datLastResumption.SelectedDate, Process.FirstDateofYear, Process.LastDay(Date.Now.Year, Date.Now.Month))
                If strAuto.Tables(0).Rows.Count > 0 Then
                    aminage.Value = strAuto.Tables(0).Rows(0).Item("age_min").ToString
                    amaxage.Value = strAuto.Tables(0).Rows(0).Item("age_max").ToString
                    abudgetposition.Value = strAuto.Tables(0).Rows(0).Item("amountrequired").ToString
                    afilledposition.Value = strAuto.Tables(0).Rows(0).Item("currentpos").ToString
                    Process.AssignRadComboValue(cboeducation, strAuto.Tables(0).Rows(0).Item("min_education").ToString)
                    'Process.CheckComboFromText(txtCompetency, cboCompetency)
                Else
                    ajobdesc.Value = ""
                    askills.Value = ""
                    aminage.Value = "0"
                    amaxage.Value = "0"
                    abudgetposition.Value = "0"
                    afilledposition.Value = "0"
                End If
            Else
                aminage.Value = "0"
                amaxage.Value = "0"
                abudgetposition.Value = "0"
                afilledposition.Value = "0"
                'Process.CheckComboFromText(txtCompetency, cboCompetency)
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub datLastResumption_SelectedDateChanged(sender As Object, e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles datLastResumption.SelectedDateChanged
        Try
            If cbojobtype.SelectedValue IsNot Nothing And cbooffice.SelectedValue IsNot Nothing And cbograde.SelectedValue IsNot Nothing And datLastResumption.SelectedDate IsNot Nothing Then
                Dim strAuto As New DataSet
                strAuto = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Job_Requisition_Auto", cbooffice.SelectedValue, cbojobtitle.SelectedValue, cbograde.SelectedValue, cbojobtype.SelectedValue, datLastResumption.SelectedDate, Process.FirstDateofYear, Process.LastDay(Date.Now.Year, Date.Now.Month))
                If strAuto.Tables(0).Rows.Count > 0 Then
                    aminage.Value = strAuto.Tables(0).Rows(0).Item("age_min").ToString
                    amaxage.Value = strAuto.Tables(0).Rows(0).Item("age_max").ToString
                    abudgetposition.Value = strAuto.Tables(0).Rows(0).Item("amountrequired").ToString
                    afilledposition.Value = strAuto.Tables(0).Rows(0).Item("currentpos").ToString
                    Process.AssignRadComboValue(cboeducation, strAuto.Tables(0).Rows(0).Item("min_education").ToString)
                    'Process.CheckComboFromText(txtCompetency, cboCompetency)
                Else
                    aminage.Value = "0"
                    amaxage.Value = "0"
                    abudgetposition.Value = "0"
                    afilledposition.Value = "0"
                    'Process.CheckComboFromText(txtCompetency, cboCompetency)
                End If

                'abudgetposition.Value = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Recruit_Job_Requisition_Budgeted_No", cbooffice.SelectedValue, cboGrade.SelectedValue, datLastResumption.SelectedDate)
            Else
                aminage.Value = "0"
                amaxage.Value = "0"
                abudgetposition.Value = "0"
                afilledposition.Value = "0"
                'Process.CheckComboFromText(txtCompetency, cboCompetency)
            End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub



    Protected Sub cboGrade_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cbograde.SelectedIndexChanged
        Try
            If cbojobtype.SelectedValue IsNot Nothing And cbooffice.SelectedValue IsNot Nothing And cbograde.SelectedValue IsNot Nothing And datLastResumption.SelectedDate IsNot Nothing Then
                Dim strAuto As New DataSet
                strAuto = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Job_Requisition_Auto", cbooffice.SelectedValue, cbojobtitle.SelectedValue, cbograde.SelectedValue, cbojobtype.SelectedValue, datLastResumption.SelectedDate, Process.FirstDateofYear, Process.LastDay(Date.Now.Year, Date.Now.Month))
                If strAuto.Tables(0).Rows.Count > 0 Then
                    aminage.Value = strAuto.Tables(0).Rows(0).Item("age_min").ToString
                    amaxage.Value = strAuto.Tables(0).Rows(0).Item("age_max").ToString
                    abudgetposition.Value = strAuto.Tables(0).Rows(0).Item("amountrequired").ToString
                    afilledposition.Value = strAuto.Tables(0).Rows(0).Item("currentpos").ToString
                    'Process.CheckComboFromText(txtCompetency, cboCompetency)
                    Process.AssignRadComboValue(cboeducation, strAuto.Tables(0).Rows(0).Item("min_education").ToString)
                Else
                    aminage.Value = "0"
                    amaxage.Value = "0"
                    abudgetposition.Value = "0"
                    afilledposition.Value = "0"
                    'Process.CheckComboFromText(txtCompetency, cboCompetency)
                End If

                'abudgetposition.Value = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Recruit_Job_Requisition_Budgeted_No", cbooffice.SelectedValue, cboGrade.SelectedValue, datLastResumption.SelectedDate)
            Else
                'ajobdesc.Value  = ""
                'askills.Value  = ""
                aminage.Value = "0"
                amaxage.Value = "0"
                abudgetposition.Value = "0"
                afilledposition.Value = "0"
                'Process.CheckComboFromText(txtCompetency, cboCompetency)
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub cbojobtype_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cbojobtype.SelectedIndexChanged
        If cbojobtype.SelectedValue IsNot Nothing And cbooffice.SelectedValue IsNot Nothing And cbograde.SelectedValue IsNot Nothing And datLastResumption.SelectedDate IsNot Nothing Then
            Dim strAuto As New DataSet
            strAuto = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Job_Requisition_Auto", cbooffice.SelectedValue, cbojobtitle.SelectedValue, cbograde.SelectedValue, cbojobtype.SelectedValue, datLastResumption.SelectedDate, Process.FirstDateofYear, Process.LastDay(Date.Now.Year, Date.Now.Month))
            If strAuto.Tables(0).Rows.Count > 0 Then
                aminage.Value = strAuto.Tables(0).Rows(0).Item("age_min").ToString
                amaxage.Value = strAuto.Tables(0).Rows(0).Item("age_max").ToString
                abudgetposition.Value = strAuto.Tables(0).Rows(0).Item("amountrequired").ToString
                afilledposition.Value = strAuto.Tables(0).Rows(0).Item("currentpos").ToString
                'Process.CheckComboFromText(txtCompetency, cboCompetency)
                Process.AssignRadComboValue(cboeducation, strAuto.Tables(0).Rows(0).Item("min_education").ToString)
            Else
                ajobdesc.Value = ""
                askills.Value = ""
                aminage.Value = "0"
                amaxage.Value = "0"
                abudgetposition.Value = "0"
                afilledposition.Value = "0"
                'Process.CheckComboFromText(txtCompetency, cboCompetency)
            End If

            'abudgetposition.Value = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Recruit_Job_Requisition_Budgeted_No", cbooffice.SelectedValue, cboGrade.SelectedValue, datLastResumption.SelectedDate)
        Else
            'ajobdesc.Value  = ""
            'askills.Value  = ""
            aminage.Value = "0"
            amaxage.Value = "0"
            abudgetposition.Value = "0"
            afilledposition.Value = "0"
            'Process.CheckComboFromText(txtCompetency, cboCompetency)
        End If
    End Sub

    Protected Sub lnkApprovalStat_Click(sender As Object, e As EventArgs)
        Try

            Dim url As String = ""

            If Request.QueryString("id") IsNot Nothing Then
                url = "StaffRequisitionsStat?id=" & txtid.Text
            ElseIf Request.QueryString("id1") IsNot Nothing Then
                url = "StaffRequisitionStat?id=" & txtid.Text
            ElseIf Request.QueryString("id2") IsNot Nothing Then
                url = "StaffRequisitionStat?id=" & txtid.Text
            ElseIf Request.QueryString("emp") IsNot Nothing Then
                url = "StaffRequisitionStat?id=" & txtid.Text
            Else
                url = "StaffRequisitionsStat?id=" & txtid.Text
            End If

            Dim s As String = "window.open('" & url + "', 'popup_window', 'width=700,height=700,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes');"
            ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub cboJobTitle_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cbojobtitle.SelectedIndexChanged
        Try

            Dim strDesc As New DataSet
            strDesc = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Job_Titles_get", cbojobtitle.SelectedValue)
            ajobdesc.Value = strDesc.Tables(0).Rows(0).Item("jobdescription").ToString
            Process.LoadTextAreaP1(askills, "Job_Title_Skills_Get_All_2", cbojobtitle.SelectedValue, "skills")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub cboLocation_CheckAllCheck1(sender As Object, e As Telerik.Web.UI.RadComboBoxCheckAllCheckEventArgs) Handles cbolocation.CheckAllCheck
        Process.LoadTextBoxFromCombo(txtLocation, cbolocation)
    End Sub

    Protected Sub cboLocation_ItemChecked1(sender As Object, e As Telerik.Web.UI.RadComboBoxItemEventArgs) Handles cbolocation.ItemChecked
        Process.LoadTextBoxFromCombo(txtLocation, cbolocation)
    End Sub


    Protected Sub cbocompany_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cbocompany.SelectedIndexChanged
        Process.LoadRadComboTextAndValueInitiateP1(cbooffice, "Company_Parent_Breakdown", cbocompany.SelectedValue, "--select--", "Companys", "Companys")
        Process.LoadRadComboTextAndValueP1(cboHiringManager, "Emp_PersonalDetail_Get_Employees", cbocompany.SelectedValue, "name", "EmpID", True)
    End Sub

    Private Sub btComplete_Click(sender As Object, e As System.EventArgs) Handles btComplete.Click
        Try
            Dim lblstatus As String = ""
            Dim msg As String = ""
            Dim msgbuild As New StringBuilder()
            Dim confirmValue As String = Request.Form("confirm_value")

            If txtid.Text <> "0" Then
                Dim strUser As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Job_Requisition_get", txtid.Text)
                afilledposition.Value = strUser.Tables(0).Rows(0).Item("NoOfPositions").ToString
                If confirmValue = "No" Then
                    ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Cancelled" + "')", True)
                Else
                    'Send Mail to Employees with competencies
                    Dim strHOD As New DataSet
                    Dim hodemail As String = ""
                    Dim hodID As String = ""
                    Dim hiringmgrname As String = ""

                    strHOD = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "select empid from dbo.Employees_All where name = '" & ahod.Value & "'")
                    If strHOD.Tables(0).Rows.Count > 0 Then
                        hodID = strHOD.Tables(0).Rows(0).Item("empid").ToString
                    End If
                    'Process.requestedURL = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "menu_get_link", AuthenCode2, 1)
                    Process.requestedURL = "Module/Employee/Recruitment/StaffRequisitionApprove.aspx"
                    Process.Staff_Requisition_Alert_Approver(ahod.Value, cbojobtitle.SelectedValue, cbooffice.SelectedValue, afilledposition.Value, datLastResumption.SelectedDate, "", Session("UserEmpID"), hodID, Process.ApplicationURL & "/" & Process.requestedURL)
                    lblstatus = "Staff Requisition is forwarded for approval"
                    Process.loadalert(divalert, msgalert, lblstatus, "success")
                End If
            Else
                If Convert.ToInt32(areqposition.Value) > Convert.ToInt32(abudgetposition.Value) Then
                    reqbug.InnerText = "Budgeted Position Exceeded"
                    'ClientScript.RegisterClientScriptBlock(Me.[GetType](), "Confirm", Convert.ToString("(confirm('") & "Budgeted Position Exceeded !!!" + "')", True)
                    'Process.loadalert(divalert, msgalert, "Budgeted Position Exceeded", "danger")
                    'Exit Sub
                End If
                If confirmValue = "No" Then
                    ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Cancelled" + "')", True)
                Else
                    'Send Mail to Employees with competencies
                    Dim strHOD As New DataSet
                    Dim hodemail As String = ""
                    Dim hodID As String = ""
                    Dim hiringmgrname As String = ""

                    strHOD = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "select empid from dbo.Employees_All where name = '" & ahod.Value & "'")
                    If strHOD.Tables(0).Rows.Count > 0 Then
                        hodID = strHOD.Tables(0).Rows(0).Item("empid").ToString
                    End If
                    'Process.requestedURL = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "menu_get_link", AuthenCode2, 1)
                    Process.requestedURL = "Module/Employee/Recruitment/StaffRequisitionApprove.aspx"
                    Process.Staff_Requisition_Alert_Approver(ahod.Value, cbojobtitle.SelectedValue, cbooffice.SelectedValue, afilledposition.Value, datLastResumption.SelectedDate, "", Session("UserEmpID"), hodID, Process.ApplicationURL & "/" & Process.requestedURL)
                    lblstatus = "Staff Requisition is forwarded for approval"
                    Process.loadalert(divalert, msgalert, lblstatus, "success")
                End If
            End If
            
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
End Class