Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports System.IO
Imports Telerik.Web.UI


Public Class RecruitCandidate
    Inherits System.Web.UI.Page
    Dim education As New clsEducation
    Dim Separators() As Char = {","c}
    Dim section As String = ""
    Private Function GetWorkIdentity(ByVal empid As String, ByVal gradelevel As String, ByVal jobtitle As String, ByVal supervisor As String, _
                                ByVal coach As String, ByVal depttype As String, _
                                 ByVal office As String, ByVal jobtype As String, ByVal joblocation As String, _
                                ByVal jobcountry As String, ByVal startdate As String, ByVal startyear As Integer, ByVal enddate As String, ByVal endyear As Integer) As String
        Try            
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Emp_Work_History_Update"
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@EmpID", SqlDbType.VarChar).Value = empid
            cmd.Parameters.Add("@GradeLevel", SqlDbType.VarChar).Value = gradelevel
            cmd.Parameters.Add("@JobTitle", SqlDbType.VarChar).Value = jobtitle
            cmd.Parameters.Add("@Supervisor", SqlDbType.VarChar).Value = supervisor
            cmd.Parameters.Add("@Supervisor2", SqlDbType.VarChar).Value = cboreviewer2.SelectedValue
            cmd.Parameters.Add("@IndirectSupervisor", SqlDbType.VarChar).Value = coach
            cmd.Parameters.Add("@DeptType", SqlDbType.VarChar).Value = depttype
            cmd.Parameters.Add("@Office", SqlDbType.VarChar).Value = office
            cmd.Parameters.Add("@JobType", SqlDbType.VarChar).Value = jobtype
            cmd.Parameters.Add("@Location", SqlDbType.VarChar).Value = joblocation
            cmd.Parameters.Add("@Country", SqlDbType.VarChar).Value = jobcountry
            cmd.Parameters.Add("@StartDate", SqlDbType.VarChar).Value = startdate
            cmd.Parameters.Add("@StartYear", SqlDbType.Int).Value = startyear
            cmd.Parameters.Add("@EndDate", SqlDbType.VarChar).Value = enddate
            cmd.Parameters.Add("@EndYear", SqlDbType.Int).Value = endyear
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
            Return 0
        End Try
    End Function
    Private Function GetContactIdentity(ByVal empid As String, ByVal address1 As String, ByVal address2 As String, _
                                ByVal City As String, ByVal Country As String, _
                                 ByVal PostalAddress As String, ByVal MobileNo As String, ByVal HomePhone As String, _
                                ByVal PersonalEMail As String, ByVal WorkEmail As String, ByVal WorkPhone As String) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            If cborecruit.SelectedItem.Value.ToUpper = "NO" Then
                cmd.CommandText = "Emp_Contact_Info_update"
            Else
                cmd.CommandText = "Emp_Contact_Info_thirdparty_update"
            End If

            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@EmpID", SqlDbType.VarChar).Value = empid
            cmd.Parameters.Add("@Address1", SqlDbType.VarChar).Value = address1
            cmd.Parameters.Add("@Address2", SqlDbType.VarChar).Value = address2
            cmd.Parameters.Add("@City", SqlDbType.VarChar).Value = City
            cmd.Parameters.Add("@Country", SqlDbType.VarChar).Value = Country
            cmd.Parameters.Add("@PostalAddress", SqlDbType.VarChar).Value = PostalAddress
            cmd.Parameters.Add("@MobileNo", SqlDbType.VarChar).Value = MobileNo
            cmd.Parameters.Add("@HomePhone", SqlDbType.VarChar).Value = HomePhone
            cmd.Parameters.Add("@PersonalEMail", SqlDbType.VarChar).Value = PersonalEMail
            cmd.Parameters.Add("@WorkEmail", SqlDbType.VarChar).Value = WorkEmail
            cmd.Parameters.Add("@WorkPhone", SqlDbType.VarChar).Value = WorkPhone
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
            Return 0
        End Try
    End Function

    Private Function GetEMPIdentity(ByVal empid As String, ByVal FirstName As String, ByVal MiddleName As String, _
                                 ByVal LastName As String, ByVal Gender As String, ByVal MaritalStatus As String, _
                                  ByVal Nationality As String, ByVal DateOfBirth As Date, ByVal BloodGroup As String, _
                                  ByVal StateOfOrigin As String, ByVal IDMethod As String, ByVal IDNo As String, _
                                 ByVal IDExpiryDate As Date, ByVal IDIssuer As String, ByVal CountryOfBirth As String, _
                                  ByVal PlaceOfBirth As String, ByVal Hobbies As String, ByVal DateJoin As Date, _
                                  ByVal Photo As String, ByVal shifts As String, ByVal company As String) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            If cborecruit.SelectedValue.ToUpper = "NO" Then
                cmd.CommandText = "Emp_PersonalDetail_update"
            Else
                cmd.CommandText = "Emp_PersonalDetail_ThirdParty_update"
            End If

            cmd.Parameters.Add("@id", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@EmpID", SqlDbType.VarChar).Value = empid
            cmd.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = FirstName
            cmd.Parameters.Add("@MiddleName", SqlDbType.VarChar).Value = MiddleName
            cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = LastName
            cmd.Parameters.Add("@Gender", SqlDbType.VarChar).Value = Gender
            cmd.Parameters.Add("@MaritalStatus", SqlDbType.VarChar).Value = MaritalStatus
            cmd.Parameters.Add("@Nationality", SqlDbType.VarChar).Value = Nationality
            cmd.Parameters.Add("@DateOfBirth", SqlDbType.Date).Value = DateOfBirth
            cmd.Parameters.Add("@BloodGroup", SqlDbType.VarChar).Value = BloodGroup
            cmd.Parameters.Add("@StateOfOrigin", SqlDbType.VarChar).Value = StateOfOrigin
            cmd.Parameters.Add("@IDMethod", SqlDbType.VarChar).Value = IDMethod
            cmd.Parameters.Add("@IDNo", SqlDbType.VarChar).Value = IDNo
            cmd.Parameters.Add("@IDExpiryDate", SqlDbType.Date).Value = IDExpiryDate
            cmd.Parameters.Add("@IDIssuer", SqlDbType.VarChar).Value = IDIssuer
            cmd.Parameters.Add("@CountryOfBirth", SqlDbType.VarChar).Value = CountryOfBirth
            cmd.Parameters.Add("@PlaceOfBirth", SqlDbType.VarChar).Value = PlaceOfBirth
            cmd.Parameters.Add("@Hobbies", SqlDbType.VarChar).Value = Hobbies
            cmd.Parameters.Add("@DateJoin", SqlDbType.Date).Value = DateJoin
            cmd.Parameters.Add("@Photo", SqlDbType.VarChar).Value = Photo
            cmd.Parameters.Add("@workshift", SqlDbType.VarChar).Value = shifts

            If cborecruit.SelectedValue.ToUpper = "YES" Then
                cmd.Parameters.Add("@company", SqlDbType.VarChar).Value = company
            End If
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
            Return 0
        End Try
    End Function
    Private Function GetEmergencyIdentity(ByVal empid As String, ByVal name1 As String, ByVal address1 As String, _
                                 ByVal phone1 As String, ByVal relationship1 As String, ByVal name2 As String, _
                                  ByVal address2 As String, ByVal phone2 As String, ByVal relationship2 As String, _
                                  ByVal RefereeName1 As String, ByVal RefereeAddress1 As String, ByVal RefereePhone1 As String, _
                                 ByVal RefereeEmail1 As String, ByVal RefereePostion1 As String, ByVal RefereeYears1 As Integer, _
                                  ByVal RefereeName2 As String, ByVal RefereeAddress2 As String, ByVal RefereePhone2 As String, _
                                  ByVal RefereeEmail2 As String, ByVal RefereePostion2 As String, ByVal RefereeYears2 As Integer, ByVal confirm1 As String, ByVal confirm2 As String) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            If cborecruit.SelectedItem.Value.ToUpper = "NO" Then
                cmd.CommandText = "Emp_Emergency_Contact_Update"
            Else
                cmd.CommandText = "Emp_Emergency_Contact_ThirdParty_Update"
            End If

            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@EmpID", SqlDbType.VarChar).Value = empid
            cmd.Parameters.Add("@Name1", SqlDbType.VarChar).Value = name1
            cmd.Parameters.Add("@Address1", SqlDbType.VarChar).Value = address1
            cmd.Parameters.Add("@Phone1", SqlDbType.VarChar).Value = phone1
            cmd.Parameters.Add("@Relationship1", SqlDbType.VarChar).Value = relationship1
            cmd.Parameters.Add("@Name2", SqlDbType.VarChar).Value = name2
            cmd.Parameters.Add("@Address2", SqlDbType.VarChar).Value = address2
            cmd.Parameters.Add("@Phone2", SqlDbType.VarChar).Value = phone2
            cmd.Parameters.Add("@Relationship2", SqlDbType.VarChar).Value = relationship2

            cmd.Parameters.Add("@RefereeName1", SqlDbType.VarChar).Value = RefereeName1
            cmd.Parameters.Add("@RefereeAddress1", SqlDbType.VarChar).Value = RefereeAddress1
            cmd.Parameters.Add("@RefereePhone1", SqlDbType.VarChar).Value = RefereePhone1
            cmd.Parameters.Add("@RefereeEmail1", SqlDbType.VarChar).Value = RefereeEmail1
            cmd.Parameters.Add("@RefereePostion1", SqlDbType.VarChar).Value = RefereePostion1
            cmd.Parameters.Add("@RefereeYears1", SqlDbType.Int).Value = RefereeYears1
            cmd.Parameters.Add("@RefereeName2", SqlDbType.VarChar).Value = RefereeName2
            cmd.Parameters.Add("@RefereeAddress2", SqlDbType.VarChar).Value = RefereeAddress2
            cmd.Parameters.Add("@RefereePhone2", SqlDbType.VarChar).Value = RefereePhone2
            cmd.Parameters.Add("@RefereeEmail2", SqlDbType.VarChar).Value = RefereeEmail2
            cmd.Parameters.Add("@RefereePostion2", SqlDbType.VarChar).Value = RefereePostion2
            cmd.Parameters.Add("@RefereeYears2", SqlDbType.Int).Value = RefereeYears2
            cmd.Parameters.Add("@RefereeConfirm1", SqlDbType.VarChar).Value = confirm1
            cmd.Parameters.Add("@RefereeConfirm2", SqlDbType.VarChar).Value = confirm2
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
            Return 0
        End Try
    End Function
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'SingleLineTextBox, // will render a textbox 
            'MultiLineTextBox, // will render a text area
            'YesOrNo, //will render a checkbox
            'SingleSelect, //will render a dropdownlist
            'MultiSelect //will render a listbo

            If Not Me.IsPostBack Then
                chkLogin.Checked = False
                divlogin.Visible = False
                photo.Visible = False
                chkLogin.Checked = False
                divwork.Visible = False

                lblIDEmer.Text = "0"
                lblIDEmp.Text = "0"
                lblIDContact.Text = "0"
                lblIDJob.Text = "0"


                'Process.AssignRadComboValue(radmcompany, Session("Organisation"))

                cbouserstat.Items.Clear()
                cbouserstat.Items.Add("Enabled")
                cbouserstat.Items.Add("Disabled")

                Process.LoadRadComboTextAndValue(cbouserrole, "roles_get_all", "Role", "Role", True)

                Process.LoadRadComboTextAndValue(cbonationality, "Nationalities_get_all", "name", "name", True)
                Process.LoadRadComboTextAndValue(cbocountrybirth, "CountryTable_get", "Country", "Country", True)
                Process.LoadRadComboTextAndValue(cbocountry, "CountryTable_get", "Country", "Country", True)
                Process.LoadRadComboTextAndValue(cboemerrelationship, "emp_relationship_get_all", "name", "name", True)

                Process.LoadRadComboTextAndValue(cboshift, "Job_Work_Shift_Get_All", "shiftname", "shiftname", True)

                Process.LoadRadComboTextAndValueInitiate(cbojobgrade, "Job_Grade_get_all", "--select--", "Name", "Name")
                Process.LoadRadComboTextAndValueInitiate(cbojobtitle, "Job_Titles_get_all", "--select--", "Name", "Name")

                Process.LoadRadComboTextAndValue(cbojobstatus, "employment_status_get_all", "Name", "Name", True)
                Process.LoadRadComboTextAndValueInitiate(cboworkcountry, "CountryTable_get", "--select--", "Country", "Country")
                Process.LoadRadComboTextAndValueInitiateP1(cbouseraccesslevel, "Users_Access_Level", Session("Level"), "--select--", "Definition", "level")

                cbogender.Items.Clear()
                cbogender.Items.Add("Female")
                cbogender.Items.Add("Male")

                cbomaritalstat.Items.Clear()
                cbomaritalstat.Items.Add("Single")
                cbomaritalstat.Items.Add("Married")
                cbomaritalstat.Items.Add("Divorced")
                cbomaritalstat.Items.Add("Widowed")

                cborefconfirmed1.Items.Clear()
                cborefconfirmed1.Items.Add("No")
                cborefconfirmed1.Items.Add("Yes")

                cborefconfirmed2.Items.Clear()
                cborefconfirmed2.Items.Add("No")
                cborefconfirmed2.Items.Add("Yes")

                Process.AssignRadComboValue(cborefconfirmed1, "No")
                Process.AssignRadComboValue(cborefconfirmed2, "No")

                Dim strUser As New DataSet
                strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Applications_Get_Applicant", Request.QueryString("id"))
                txtFirstName.Text = strUser.Tables(0).Rows(0).Item("firstname").ToString
                alastname.Value = strUser.Tables(0).Rows(0).Item("lastname").ToString
                aothernames.Value = strUser.Tables(0).Rows(0).Item("firstname").ToString + " " + strUser.Tables(0).Rows(0).Item("middlename").ToString
                txtmiddlename.Text = strUser.Tables(0).Rows(0).Item("middlename").ToString
                Process.AssignRadComboValue(cbogender, strUser.Tables(0).Rows(0).Item("gender").ToString)
                Process.AssignRadComboValue(cbomaritalstat, strUser.Tables(0).Rows(0).Item("MaritalStatus").ToString)
                Process.AssignRadComboValue(cbonationality, strUser.Tables(0).Rows(0).Item("nationality").ToString)
                Process.AssignRadComboValue(cbocountrybirth, strUser.Tables(0).Rows(0).Item("country").ToString)
                datDOB.SelectedDate = strUser.Tables(0).Rows(0).Item("dob").ToString
                aaddress.Value = strUser.Tables(0).Rows(0).Item("ResidentAddress").ToString
                Process.AssignRadComboValue(cbocountry, strUser.Tables(0).Rows(0).Item("country").ToString)
                aphonenumber.Value = strUser.Tables(0).Rows(0).Item("MobileNo").ToString
                aemailaddress.Value = strUser.Tables(0).Rows(0).Item("EmailAddress").ToString
                acity.Value = strUser.Tables(0).Rows(0).Item("city").ToString

                arefaddr1.Value = strUser.Tables(0).Rows(0).Item("RefereeAddress1").ToString
                arefemail1.Value = strUser.Tables(0).Rows(0).Item("RefereeEmail1").ToString
                arefname1.Value = strUser.Tables(0).Rows(0).Item("RefereeName1").ToString

                arefphone1.Value = strUser.Tables(0).Rows(0).Item("RefereePhone1").ToString
                arefposition1.Value = strUser.Tables(0).Rows(0).Item("RefereePostion1").ToString
                arefyears1.Value = strUser.Tables(0).Rows(0).Item("RefereeYears1").ToString

                arefaddr2.Value = strUser.Tables(0).Rows(0).Item("RefereeAddress2").ToString
                arefemail2.Value = strUser.Tables(0).Rows(0).Item("RefereeEmail2").ToString
                arefname2.Value = strUser.Tables(0).Rows(0).Item("RefereeName2").ToString
                arefphone2.Value = strUser.Tables(0).Rows(0).Item("RefereePhone2").ToString
                arefposition2.Value = strUser.Tables(0).Rows(0).Item("RefereePostion2").ToString
                arefyears2.Value = strUser.Tables(0).Rows(0).Item("RefereeYears2").ToString

                lstLang.Items.Clear()
                Dim breakdown As String()
                breakdown = strUser.Tables(0).Rows(0).Item("Languages").ToString.Split(Separators, StringSplitOptions.RemoveEmptyEntries)
                For i As Integer = 0 To breakdown.Length - 1
                    lstLang.Items.Add(breakdown(i).Trim)
                Next

                lstSkills.Items.Clear()
                Dim skillbreakdown As String()
                skillbreakdown = strUser.Tables(0).Rows(0).Item("Skill").ToString.Split(Separators, StringSplitOptions.RemoveEmptyEntries)
                For i As Integer = 0 To skillbreakdown.Length - 1
                    lstSkills.Items.Add(skillbreakdown(i).Trim)
                Next

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            Dim lblstatus As String = ""
            If cborecruit.Text.Contains("Select") Then
                lblstatus = "Indicate recruitment type!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                cborecruit.Focus()
                Exit Sub
            End If
            Dim workshifts As String = ""

            If cborecruit.SelectedItem.Value.ToUpper = "NO" Then
                If cboshift.SelectedValue Is Nothing Then
                    lblstatus = "Work Shift required!"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    cboshift.Focus()
                    Exit Sub
                Else
                    workshifts = cboshift.SelectedValue
                End If


                If aworkemail.Value.Trim Is Nothing Or aworkemail.Value.Contains("@") = False Then
                    lblstatus = "A valid Email Address required!"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    aworkemail.Focus()
                    Exit Sub
                End If


                If cbojobgrade.SelectedValue.Contains("--select--") = True Then
                    lblstatus = "Job Grade required!"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    cbojobgrade.Focus()
                    Exit Sub
                End If

                If cbojobtitle.SelectedValue.Contains("--select--") Then
                    lblstatus = "Job Title required!"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    cbojobtitle.Focus()
                    Exit Sub
                End If

                If cboworkcountry.SelectedValue Is Nothing Then
                    lblstatus = "Work Country required!"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    cboworkcountry.Focus()
                    Exit Sub
                End If


                If cboworklocation.SelectedValue.Contains("--select--") Then
                    lblstatus = "Job Location required!"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    cboworklocation.Focus()
                    Exit Sub
                End If



                If cbojoboffice.SelectedValue.Contains("--select--") Then
                    lblstatus = "Job Office required!"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    cbojoboffice.Focus()
                    Exit Sub
                End If

                If cboreportsto.SelectedValue Is Nothing Then
                    lblstatus = "Supervisor required, select 'N/A' if employee reports to no one!"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    cboreportsto.Focus()
                    Exit Sub
                End If

                If cboreviewer1.SelectedValue Is Nothing Then
                    lblstatus = "First reviewer required, select 'N/A' if employee has no reviewer!"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    cboreviewer1.Focus()
                    Exit Sub
                End If

                If cboreviewer2.SelectedValue Is Nothing Then
                    lblstatus = "Second reviewer required, select 'N/A' if employee has no reviewer!"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    cboreviewer2.Focus()
                    Exit Sub
                End If

                If cboshift.SelectedValue Is Nothing Then
                    lblstatus = "Work Shift required!"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    cboshift.Focus()
                    Exit Sub
                End If

            Else
                If cbocompany.SelectedValue Is Nothing Then
                    lblstatus = "Company required!"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    cbocompany.Focus()
                    Exit Sub
                End If
            End If

            If datDateJoined.SelectedDate.HasValue = False Then
                lblstatus = "Resumption date required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                datDateJoined.Focus()
                Exit Sub
            End If


            If arefaddr1.Value.Trim = "" Or arefemail1.Value.Trim = "" Or arefname1.Value.Trim = "" Or arefphone1.Value.Trim = "" Or _
                arefposition1.Value.Trim = "" Or arefyears1.Value.Trim = "" Then
                lblstatus = "Complete Referee 1 requirements!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                arefname1.Focus()
                Exit Sub
            End If


            If arefaddr2.Value.Trim = "" Or arefemail2.Value.Trim = "" Or arefname2.Value.Trim = "" Or arefphone2.Value.Trim = "" Or _
               arefposition2.Value.Trim = "" Or arefyears2.Value.Trim = "" Then
                lblstatus = "Complete Referee 2 requirements!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                arefname2.Focus()
                Exit Sub
            End If

            If aemeraddress.Value.Trim = "" Or aemername.Value.Trim = "" Or aemernumber.Value.Trim = "" Then
                lblstatus = "Complete Emergency Contact requirements!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                aemername.Focus()
                Exit Sub
            End If


            lblstatus = ""
            If aempid.Value.Trim Is Nothing And cborecruit.SelectedItem.Value.ToUpper = "NO" Then
                lblstatus = "Assign Employee ID to new recruit"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                Exit Sub
            Else
                'Personal Info

                Dim company As String = ""
                If cborecruit.SelectedItem.Value.ToUpper = "YES" Then
                    company = cbocompany.SelectedValue
                End If
                section = "P"

                If lblIDEmp.Text = "0" Then
                    lblIDEmp.Text = GetEMPIdentity(aempid.Value.Trim, txtFirstName.Text.Trim, _
                                txtmiddlename.Text.Trim, alastname.Value, cbogender.SelectedValue, cbomaritalstat.SelectedValue, cbonationality.SelectedValue, _
                                datDOB.SelectedDate, "", "", "", "", _
                                Nothing, "", cbocountrybirth.SelectedValue, cbocountrybirth.SelectedValue, "", _
                                datDateJoined.SelectedDate, txtimage.Text, workshifts, company)
                    If lblIDEmp.Text = "0" Then                        
                        Exit Sub
                    End If
                Else
                    If cborecruit.SelectedItem.Value.ToUpper = "NO" Then
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_PersonalDetail_update", lblIDEmp.Text, aempid.Value.Trim, txtFirstName.Text.Trim, _
                             txtmiddlename.Text.Trim, alastname.Value, cbogender.SelectedValue, cbomaritalstat.SelectedValue, cbonationality.SelectedValue, _
                             datDOB.SelectedDate, "", "", "", "", _
                             Nothing, "", cbocountrybirth.SelectedValue, cbocountrybirth.SelectedValue, "", _
                             datDateJoined.SelectedDate, txtimage.Text, workshifts)
                    Else
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_PersonalDetail_Thirdparty_update", lblIDEmp.Text, aempid.Value, txtFirstName.Text.Trim, _
                              txtmiddlename.Text.Trim, alastname.Value, cbogender.SelectedValue, cbomaritalstat.SelectedValue, cbonationality.SelectedValue, _
                              datDOB.SelectedDate, "", "", "", "", _
                              Nothing, "", cbocountrybirth.SelectedValue, cbocountrybirth.SelectedValue, "", _
                              datDateJoined.SelectedDate, txtimage.Text, workshifts, cbocompany.SelectedValue)
                    End If

                End If



                section = "C"
                'Contact
                If cborecruit.SelectedItem.Value.ToUpper = "NO" Then
                    If lblIDContact.Text = "0" Then
                        lblIDContact.Text = GetContactIdentity(aempid.Value.Trim, aaddress.Value.Trim, "", acity.Value, cbocountry.SelectedValue, aaddress.Value.Trim, aphonenumber.Value, aphonenumber.Value, aemailaddress.Value, aworkemail.Value.Trim, aphonenumber.Value)
                        If lblIDContact.Text = "0" Then
                            Exit Sub
                        End If
                    Else
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Contact_Info_update", lblIDContact.Text, aempid.Value.Trim, aaddress.Value.Trim, "", acity.Value, cbocountry.SelectedValue, aaddress.Value.Trim, aphonenumber.Value, aphonenumber.Value, aemailaddress.Value, aworkemail.Value.Trim, aphonenumber.Value)
                    End If
                Else
                    If lblIDContact.Text = "0" Then
                        lblIDContact.Text = GetContactIdentity(aempid.Value.Trim, aaddress.Value.Trim, "", acity.Value, cbocountry.SelectedValue, aaddress.Value.Trim, aphonenumber.Value, aphonenumber.Value, aemailaddress.Value, aworkemail.Value.Trim, aphonenumber.Value)
                        If lblIDContact.Text = "0" Then

                            Exit Sub
                        End If
                    Else
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Contact_Info_ThirdParty_update", lblIDContact.Text, aempid.Value.Trim, aaddress.Value.Trim, "", acity.Value, cbocountry.SelectedValue, aaddress.Value.Trim, aphonenumber.Value, aphonenumber.Value, aemailaddress.Value, aworkemail.Value.Trim, aphonenumber.Value)
                    End If
                End If


                'Emergency
                section = "E"
                If cborecruit.SelectedItem.Value.ToUpper = "NO" Then
                    If lblIDEmer.Text = "0" Then
                        lblIDEmer.Text = GetEmergencyIdentity(aempid.Value.Trim, aemername.Value.Trim, aemeraddress.Value.Trim, _
                                             aemernumber.Value, cboemerrelationship.SelectedValue, "", "", _
                                             "", "", arefname1.Value.Trim, arefaddr1.Value.Trim, arefphone1.Value.Trim, arefemail1.Value.Trim, _
                                             arefposition1.Value, arefyears1.Value, arefname2.Value.Trim, arefaddr2.Value.Trim, arefphone2.Value.Trim, arefemail2.Value.Trim, _
                                             arefposition2.Value, arefyears2.Value, cborefconfirmed1.SelectedValue, cborefconfirmed2.SelectedValue)
                        If lblIDEmer.Text = "0" Then                            
                            Exit Sub
                        End If
                    Else
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Emergency_Contact_Update", lblIDEmer.Text, aempid.Value.Trim, aemername.Value.Trim, aemeraddress.Value.Trim, _
                                            aemernumber.Value, cboemerrelationship.SelectedValue, "", "", _
                                            "", "", arefname1.Value.Trim, arefaddr1.Value.Trim, arefphone1.Value.Trim, arefemail1.Value.Trim, _
                                            arefposition1.Value, arefyears1.Value, arefname2.Value.Trim, arefaddr2.Value.Trim, arefphone2.Value.Trim, arefemail2.Value.Trim, _
                                            arefposition2.Value, arefyears2.Value, cborefconfirmed1.SelectedValue, cborefconfirmed2.SelectedValue)
                    End If
                Else
                    If lblIDEmer.Text = "0" Then
                        lblIDEmer.Text = GetEmergencyIdentity(aempid.Value.Trim, aemername.Value.Trim, aemeraddress.Value.Trim, _
                                             aemernumber.Value, cboemerrelationship.SelectedValue, "", "", _
                                             "", "", arefname1.Value.Trim, arefaddr1.Value.Trim, arefphone1.Value.Trim, arefemail1.Value.Trim, _
                                             arefposition1.Value, arefyears1.Value, arefname2.Value.Trim, arefaddr2.Value.Trim, arefphone2.Value.Trim, arefemail2.Value.Trim, _
                                             arefposition2.Value, arefyears2.Value, cborefconfirmed1.SelectedValue, cborefconfirmed2.SelectedValue)
                        If lblIDEmer.Text = "0" Then

                            Exit Sub
                        End If
                    Else
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Emergency_Contact_ThirdParty_Update", lblIDEmer.Text, aempid.Value.Trim, aemername.Value.Trim, aemeraddress.Value.Trim, _
                                            aemernumber.Value, cboemerrelationship.SelectedValue, "", "", _
                                            "", "", arefname1.Value.Trim, arefaddr1.Value.Trim, arefphone1.Value.Trim, arefemail1.Value.Trim, _
                                            arefposition1.Value, arefyears1.Value, arefname2.Value.Trim, arefaddr2.Value.Trim, arefphone2.Value.Trim, arefemail2.Value.Trim, _
                                            arefposition2.Value, arefyears2.Value, cborefconfirmed1.SelectedValue, cborefconfirmed2.SelectedValue)
                    End If
                End If


                'Qualification
                'Language
                section = "L"
                For i As Integer = 0 To lstLang.Items.Count - 1
                    Dim langid As String = ""

                    If cborecruit.SelectedItem.Value.ToUpper = "NO" Then
                        langid = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select id from Emp_Languages where EmpID = '" & aempid.Value.Trim & "' and [language] = '" & lstLang.Items.Item(i).Text & "'")
                    Else
                        langid = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select id from Emp_Languages_thirdparty where EmpID = '" & aempid.Value.Trim & "' and [language] = '" & lstLang.Items.Item(i).Text & "'")
                    End If
                    If langid Is Nothing Then
                        langid = "0"
                    End If

                    If cborecruit.SelectedItem.Value.ToUpper = "NO" Then
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Languages_update", langid, aempid.Value, lstLang.Items.Item(i).Text, "Good", "Good", "Good")
                    Else
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Languages_ThirdParty_update", langid, aempid.Value, lstLang.Items.Item(i).Text, "Good", "Good", "Good")
                    End If

                Next
                'Skills
                section = "S"
                For i As Integer = 0 To lstSkills.Items.Count - 1
                    Dim skillid As String = ""

                    If cborecruit.SelectedItem.Value.ToUpper = "NO" Then
                        skillid = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select id from Emp_Skills where EmpID = '" & aempid.Value.Trim & "' and Skill = '" & lstSkills.Items.Item(i).Text & "'")
                    Else
                        skillid = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select id from Emp_Skills_thirdparty where EmpID = '" & aempid.Value.Trim & "' and Skill = '" & lstSkills.Items.Item(i).Text & "'")
                    End If

                    If skillid Is Nothing Then
                        skillid = "0"
                    End If

                    If cborecruit.SelectedItem.Value.ToUpper = "NO" Then
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Skills_Update", skillid, aempid.Value, lstSkills.Items.Item(i).Text)
                    Else
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Skills_Thirdparty_Update", skillid, aempid.Value, lstSkills.Items.Item(i).Text)
                    End If

                Next

                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Applications_Update_State", Session("ApplcantID"), Session("JobID"), "recruited", "Yes")
                If cborecruit.SelectedItem.Value.ToUpper = "NO" Then
                    If lblIDJob.Text = "0" Then
                        lblIDJob.Text = GetWorkIdentity(aempid.Value.Trim.ToUpper, cbojobgrade.SelectedValue, cbojobtitle.SelectedValue, cboreportsto.SelectedValue, cboreviewer1.SelectedValue, "", cbojoboffice.SelectedValue, cbojobstatus.SelectedValue, cboworklocation.SelectedValue, cboworkcountry.SelectedValue, MonthName(datDateJoined.SelectedDate.Value.Month, True), datDateJoined.SelectedDate.Value.Year, "Present", 0)
                        If lblIDJob.Text = "0" Then
                            Exit Sub
                        End If
                        Process.Welcome_Recruit(Process.GetCompanyName(cbojoboffice.SelectedValue), aworkemail.Value, aempid.Value, txtFirstName.Text)
                    Else
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Work_History_Update", lblIDJob.Text, aempid.Value.Trim.ToUpper, cbojobgrade.SelectedValue, cbojobtitle.SelectedValue, cboreportsto.SelectedValue, cboreviewer2.SelectedValue, cboreviewer1.SelectedValue, "", cbojoboffice.SelectedValue, cbojobstatus.SelectedValue, cboworklocation.SelectedValue, cboworkcountry.SelectedValue, MonthName(datDateJoined.SelectedDate.Value.Month, True), datDateJoined.SelectedDate.Value.Year, "Present", 0)
                    End If

                    If chkLogin.Checked = True Then

                        If ausername.Value.Trim = "" Then
                            lblstatus = "User Name for Employee"
                            Process.loadalert(divalert, msgalert, lblstatus, "warning")
                            ausername.Focus()
                            Exit Sub
                        End If

                        If apassword.Value.Trim = "" Then
                            lblstatus = "Create Password for Employee"
                            Process.loadalert(divalert, msgalert, lblstatus, "warning")
                            apassword.Focus()
                            Exit Sub
                        End If

                        If cbouserrole.SelectedValue Is Nothing Then
                            lblstatus = "Assign a Login Role to Employee"
                            Process.loadalert(divalert, msgalert, lblstatus, "warning")
                            cbouserrole.Focus()
                            Exit Sub
                        End If

                        If cbouserstat.Text Is Nothing Then
                            lblstatus = "Set Login Status for Employee"
                            Process.loadalert(divalert, msgalert, lblstatus, "warning")
                            cbouserstat.Focus()
                            Exit Sub
                        End If

                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "users_add", ausername.Value.Trim, alastname.Value & " " & txtFirstName.Text.Trim, cbouserrole.SelectedValue, cbouserstat.Text.Trim, aworkemail.Value.Trim, "Yes", Process.Encrypt(apassword.Value), Session("LoginID"), aempid.Value.Trim, cbouseraccesslevel.SelectedValue)

                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "users_access_delete", ausername.Value.Trim)
                        Dim collection As IList(Of RadComboBoxItem) = cbouseraccess.CheckedItems
                        If (collection.Count <> 0) Then
                            For Each item As RadComboBoxItem In collection
                                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "users_access_update", ausername.Value.Trim, item.Value)
                            Next

                        End If

                        If Process.User_Notification(aworkemail.Value.Trim, txtFirstName.Text, ausername.Value, apassword.Value, Process.ApplicationURL & "/Default.aspx") = False Then
                            Process.loadalert(divalert, msgalert, Session("exception"), "danger")
                        End If
                    End If
                Else
                    Process.Welcome_Recruit(cbocompany.SelectedValue, aemailaddress.Value, aempid.Value, txtFirstName.Text)
                End If

                lblstatus = "Candidate successfully recruited"
                Process.loadalert(divalert, msgalert, lblstatus, "success")
            End If


        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try

    End Sub
   
    Protected Sub chkLogin_CheckedChanged(sender As Object, e As EventArgs) Handles chkLogin.CheckedChanged
        Try
            If chkLogin.Checked = True Then
                divlogin.Visible = True
            Else
                divlogin.Visible = False
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try

    End Sub

    Protected Sub Chkphoto_CheckedChanged(sender As Object, e As EventArgs) Handles Chkphoto.CheckedChanged
        Try
            If Chkphoto.Checked = True Then
                photo.Visible = True
            Else
                photo.Visible = False
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try

    End Sub

    Protected Sub cbojobgrade_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cbojobgrade.SelectedIndexChanged
        Try

            Process.LoadRadComboTextAndValueP2(cboreportsto, "Emp_PersonalDetail_get_Superiors", cbojobgrade.SelectedValue, Process.GetCompanyName, "Employee2", "EmpID", True)
            Process.LoadRadComboTextAndValueP2(cboreviewer1, "Emp_PersonalDetail_get_Superiors", cbojobgrade.SelectedValue, Process.GetCompanyName, "Employee2", "EmpID", True)
            Process.LoadRadComboTextAndValueP2(cboreviewer2, "Emp_PersonalDetail_get_Superiors", cbojobgrade.SelectedValue, Process.GetCompanyName, "Employee2", "EmpID", True)

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub cboworkcountry_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboworkcountry.SelectedIndexChanged
        Try
            If cboworkcountry.SelectedValue IsNot Nothing Then
                Process.LoadRadComboTextAndValueInitiateP1(cboworklocation, "location_get_country", cboworkcountry.SelectedValue, "--select--", "name", "name")
            End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


    Private Function GetPhotoIdentity(ByVal empid As String, ByVal photo As Byte(), ByVal imgtype As String) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure

            If cborecruit.SelectedItem.Value.ToUpper = "NO" Then
                cmd.CommandText = "Emp_PersonalDetail_update_PhotoImage"
            Else
                cmd.CommandText = "Emp_PersonalDetail_thirdparty_update_PhotoImage"
                empid = "0"

            End If

            cmd.Parameters.Add("@EmpID", SqlDbType.VarChar).Value = empid
            cmd.Parameters.Add("@Photo", SqlDbType.Image).Value = photo
            cmd.Parameters.Add("@imagetype", SqlDbType.VarChar).Value = imgtype
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception
            Process.strExp = ex.Message
            Return 0
        End Try
    End Function
    Protected Sub btnImage_Click(sender As Object, e As EventArgs) Handles btnupload.Click
        Try
            Dim lblstatus As String = ""
            If cborecruit.SelectedItem.Value Is Nothing Then
                lblstatus = "Indicate recruitment type!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                cborecruit.Focus()
                Exit Sub
            End If

            If cborecruit.SelectedItem.Value.ToUpper = "NO" And aempid.Value.Trim = "" Then
                lblstatus = "Assign an Employee ID before uploading Photo!, check Recruit for ThirdParty if recruitment is for a Client Company"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                aempid.Focus()
                Exit Sub
            End If

            If Not imgUpload.PostedFile Is Nothing Then
                Dim img_strm As Stream = imgUpload.PostedFile.InputStream
                'Retrieving the length of the file to upload
                Dim img_len As Integer = imgUpload.PostedFile.ContentLength
                'retrieving the type of the file to upload
                Dim strtype As String = imgUpload.PostedFile.ContentType.ToString()
                Dim strname As String = Path.GetFileName(imgUpload.PostedFile.FileName)
                Dim imgdata As Byte() = New Byte(img_len - 1) {}
                Dim n As Integer = img_strm.Read(imgdata, 0, img_len)

                If lblIDEmp.Text = "0" Then
                    lblIDEmp.Text = GetPhotoIdentity(aempid.Value.Trim, imgdata, strtype)

                    If lblIDEmp.Text = "0" Then
                        Process.loadalert(divalert, msgalert, "Save Personal Details first before uploading profile picture", "warning")
                        Exit Sub
                    End If
                Else
                    If cborecruit.SelectedItem.Value.ToUpper = "NO" Then
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_PersonalDetail_update_PhotoImage", aempid.Value.Trim, imgdata, strtype)
                    Else
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_PersonalDetail_thirdparty_update_PhotoImage", lblIDEmp.Text, imgdata, strtype)
                        aempid.Value = lblIDEmp.Text
                        aempid.Disabled = True
                    End If

                End If
                If cborecruit.SelectedItem.Value.ToUpper = "NO" Then
                    imgProfile.ImageUrl = "~/Module/Recruitment/ImgHandler.ashx?imgid=" & lblIDEmp.Text
                Else
                    aempid.Value = lblIDEmp.Text
                    imgProfile.ImageUrl = "~/Module/Recruitment/ImgThirdHandler.ashx?imgid=" & lblIDEmp.Text
                End If


                lblstatus = "Photo uploaded"
                Process.loadalert(divalert, msgalert, lblstatus, "success")
            Else
                lblstatus = "No photo selected for upload"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                imgUpload.Focus()
            End If



        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub



    Protected Sub cborecruit_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cborecruit.SelectedIndexChanged
        'Protected Sub cborecruit_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cborecruit.TextChanged
        Try
            If cborecruit.SelectedValue.ToUpper = "YES" Then
                Process.LoadRadComboTextAndValue(cbocompany, "Recruit_ThirdParty_Company_get_all", "companyname", "companyname", False)
                divwork.Visible = False
                divlogin.Visible = False
                chkLogin.Checked = False
                aempid.Disabled = True
                aempid.Value = "0"

            ElseIf cborecruit.SelectedValue.ToUpper = "NO" Then
                Dim ismulti As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select isnull(ismulticompany,'No')  from general_info")
                If ismulti.ToLower = "no" Then
                    Process.LoadRadComboTextAndValueInitiateP2(cbocompany, "Company_Structure_Get_ByLevel", "1", Session("Access"), "--select--", "name", "name")
                Else
                    Process.LoadRadComboTextAndValueInitiateP2(cbocompany, "Company_Structure_Get_ByLevel", "2", Session("Access"), "--select--", "name", "name")
                End If

                divwork.Visible = True
                'divlogin.Visible = True
                aempid.Disabled = False
            Else
                Process.loadalert(divalert, msgalert, "Indicate whether recruitment is for Client Company", "warning")
                cborecruit.Focus()
                Exit Sub
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub cbojoboffice_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs)
        'Try
        '    If cbojobgrade.SelectedValue IsNot Nothing And cbojoboffice.SelectedValue IsNot Nothing Then
        '        Process.LoadRadComboTextAndValueInitiateP2(cboJobSupervisor, "Emp_PersonalDetail_Get_Superiors_2", cbojobgrade.SelectedValue, Process.GetCompanyName, "--Select--", "Employee2", "EmpID")
        '        Process.LoadRadComboTextAndValueInitiateP2(cboreviewer1, "Emp_PersonalDetail_Get_Superiors_2", cbojobgrade.SelectedValue, Process.GetCompanyName, "--Select--", "Employee2", "EmpID")
        '    End If
        'Catch ex As Exception
        '    Response.Write(ex.Message)
        'End Try
    End Sub



    Protected Sub cbocompany_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cbocompany.SelectedIndexChanged
        Process.LoadRadComboTextAndValueP1(cbojoboffice, "Company_Parent_Breakdown", cbocompany.SelectedValue, "Companys", "Companys", False)
    End Sub


    Protected Sub cbouseraccesslevel_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cbouseraccesslevel.SelectedIndexChanged
        Try
            Process.LoadRadComboTextAndValueP1(cbouseraccess, "Company_Structure_Get_Level", cbouseraccesslevel.SelectedValue, "Name", "Name", False)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btngenerate_Click(sender As Object, e As EventArgs)
        Try
            aempid.Value = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select dbo.Generate_EmpID()")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub cbouseraccess_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cbouseraccess.SelectedIndexChanged
        'Try
        '    Process.LoadRadComboTextAndValueP1(cbouseraccess, "Company_Structure_Get_Level", cbouseraccess.SelectedValue, "Name", "Name", False)
        'Catch ex As Exception

        'End Try
    End Sub
End Class