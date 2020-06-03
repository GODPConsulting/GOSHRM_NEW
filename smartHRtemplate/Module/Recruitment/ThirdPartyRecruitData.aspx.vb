Imports Microsoft.ApplicationBlocks.Data
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Public Class ThirdPartyRecruitData
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "THIRDPARTYREC"
    Dim PersonalDetail As New clsEmpPersonalDetail
    Dim ContactDetail As New clsEmpContactInfo
    Dim EmergencyDetail As New clsEmpEmergency
    Dim olddata(21) As String
    Dim imgByte As Byte() = Nothing

#Region "LoadData"
   
    Private Sub LoadPersonalDetail(ByVal EmpID As String)
        Try
            Dim strPersonal As New DataSet
            strPersonal = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_PersonalDetail_ThirdParty_get", EmpID)
            If strPersonal.Tables(0).Rows.Count > 0 Then
                txtID.Text = strPersonal.Tables(0).Rows(0).Item("id").ToString

                If IsDBNull(strPersonal.Tables(0).Rows(0).Item("imgfile")) Or strPersonal.Tables(0).Rows(0).Item("imgfile").ToString = "" Then
                    imgProfile.ImageUrl = imgClear.ImageUrl
                Else
                    imgProfile.ImageUrl = "ImgThirdHandler.ashx?imgid=" & txtID.Text
                End If



                txtFirstName.Text = strPersonal.Tables(0).Rows(0).Item("FirstName").ToString
                txtMidName.Text = strPersonal.Tables(0).Rows(0).Item("MiddleName").ToString
                txtLastName.Text = strPersonal.Tables(0).Rows(0).Item("LastName").ToString

                Process.AssignRadDropDownValue(radGender, strPersonal.Tables(0).Rows(0).Item("Gender").ToString)
                Process.AssignRadDropDownValue(radMaritalStatus, strPersonal.Tables(0).Rows(0).Item("MaritalStatus").ToString)
                Process.AssignRadDropDownValue(radNationality, strPersonal.Tables(0).Rows(0).Item("Nationality").ToString)
                Process.AssignRadDropDownValue(radCompany, strPersonal.Tables(0).Rows(0).Item("companyname").ToString)


                If IsDBNull(strPersonal.Tables(0).Rows(0).Item("DateOfBirth")) = False Then
                    radDOB.SelectedDate = strPersonal.Tables(0).Rows(0).Item("DateOfBirth")
                End If

                txtBloodGrp.Text = strPersonal.Tables(0).Rows(0).Item("BloodGroup").ToString
                txtStateOrigin.Text = strPersonal.Tables(0).Rows(0).Item("StateOfOrigin").ToString
                txtIDType.Text = strPersonal.Tables(0).Rows(0).Item("IDMethod").ToString
                txtIDNo.Text = strPersonal.Tables(0).Rows(0).Item("IDNo").ToString

                If IsDBNull(strPersonal.Tables(0).Rows(0).Item("IDExpiryDate")) = False And IsDBNull(strPersonal.Tables(0).Rows(0).Item("IDExpiryDate")).ToString.Contains("AM") = False Then
                    radIDExpiry.SelectedDate = strPersonal.Tables(0).Rows(0).Item("IDExpiryDate")
                End If

                txtIDIssuer.Text = strPersonal.Tables(0).Rows(0).Item("IDIssuer").ToString
                Process.AssignRadDropDownValue(radCountryofBirth, strPersonal.Tables(0).Rows(0).Item("CountryOfBirth").ToString)

                txtPlaceOfBirth.Text = strPersonal.Tables(0).Rows(0).Item("Placeofbirth").ToString
                txtHobbies.Text = strPersonal.Tables(0).Rows(0).Item("Hobbies").ToString

                If IsDBNull(strPersonal.Tables(0).Rows(0).Item("DateJoin")) = False Then
                    radDateJoin.SelectedDate = strPersonal.Tables(0).Rows(0).Item("DateJoin")
                End If



                'If IsDBNull(strPersonal.Tables(0).Rows(0).Item("Photo")) = False Then
                '    txtImage.Text = strPersonal.Tables(0).Rows(0).Item("Photo")
                'End If

                'imgProfile.ImageUrl = txtImage.Text
                ''Photo
            End If
        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub
    Private Sub LoadContacts(ByVal EmpID As String)
        Try
            Dim strContacts As New DataSet
            strContacts = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Contact_Info_ThirdParty_get", EmpID)
            If strContacts.Tables(0).Rows.Count > 0 Then
                txtIDContact.Text = strContacts.Tables(0).Rows(0).Item("id").ToString
                txtcAddr1.Text = strContacts.Tables(0).Rows(0).Item("Address1").ToString
                txtAddr2.Text = strContacts.Tables(0).Rows(0).Item("Address2").ToString
                txtPostalAddr.Text = strContacts.Tables(0).Rows(0).Item("PostalAddress").ToString
                txtEmail.Text = strContacts.Tables(0).Rows(0).Item("PersonalEMail").ToString
                txtCity.Text = strContacts.Tables(0).Rows(0).Item("City").ToString
                Process.AssignRadDropDownValue(radResidenceCountry, strContacts.Tables(0).Rows(0).Item("Country").ToString)

                txtHomePhone.Text = strContacts.Tables(0).Rows(0).Item("HomePhone").ToString
                txtMobileNo.Text = strContacts.Tables(0).Rows(0).Item("MobileNo").ToString
                txtWorkPhone.Text = strContacts.Tables(0).Rows(0).Item("WorkPhone").ToString
                txtWorkEmail.Text = strContacts.Tables(0).Rows(0).Item("WorkEmail").ToString


            End If
        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub
    Private Sub LoadEmergencyContact(ByVal EmpID As String)
        Try
            Dim strEmergency As New DataSet
            strEmergency = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Emergency_Contact_ThirdParty_get", EmpID)
            'Emergency Contacts
            If strEmergency.Tables(0).Rows.Count > 0 Then
                txtIDEmergency.Text = strEmergency.Tables(0).Rows(0).Item("id").ToString
                txtEmerName1.Text = strEmergency.Tables(0).Rows(0).Item("Name1").ToString
                txtEmerAddr1.Text = strEmergency.Tables(0).Rows(0).Item("Address1").ToString
                txtEmerNo1.Text = strEmergency.Tables(0).Rows(0).Item("Phone1").ToString

                Process.AssignRadDropDownValue(radEmerRelationship1, strEmergency.Tables(0).Rows(0).Item("Relationship1").ToString)

                txtEmerName2.Text = strEmergency.Tables(0).Rows(0).Item("Name2").ToString
                txtEmerAddr2.Text = strEmergency.Tables(0).Rows(0).Item("Address2").ToString
                txtEmerNo2.Text = strEmergency.Tables(0).Rows(0).Item("Phone2").ToString
                Process.AssignRadDropDownValue(radEmerRelationship2, strEmergency.Tables(0).Rows(0).Item("Relationship2").ToString)

                txtRefAddress1.Text = strEmergency.Tables(0).Rows(0).Item("RefereeAddress1").ToString
                txtRefEmail1.Text = strEmergency.Tables(0).Rows(0).Item("RefereeEmail1").ToString
                txtRefName1.Text = strEmergency.Tables(0).Rows(0).Item("RefereeName1").ToString
                txtRefPhone1.Text = strEmergency.Tables(0).Rows(0).Item("RefereePhone1").ToString
                txtRefPostion1.Text = strEmergency.Tables(0).Rows(0).Item("RefereePostion1").ToString
                txtRefYear1.Text = strEmergency.Tables(0).Rows(0).Item("RefereeYears1").ToString
                Process.AssignRadComboValue(cboRefConfirm1, strEmergency.Tables(0).Rows(0).Item("RefereeConfirm1").ToString)

                txtRefAddress2.Text = strEmergency.Tables(0).Rows(0).Item("RefereeAddress2").ToString
                txtRefEmail2.Text = strEmergency.Tables(0).Rows(0).Item("RefereeEmail2").ToString
                txtRefName2.Text = strEmergency.Tables(0).Rows(0).Item("RefereeName2").ToString
                txtRefPhone2.Text = strEmergency.Tables(0).Rows(0).Item("RefereePhone2").ToString
                txtRefPostion2.Text = strEmergency.Tables(0).Rows(0).Item("RefereePostion2").ToString
                txtRefYear2.Text = strEmergency.Tables(0).Rows(0).Item("RefereeYears2").ToString
                Process.AssignRadComboValue(cboRefConfirm1, strEmergency.Tables(0).Rows(0).Item("RefereeConfirm2").ToString)

            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub LoadLanguages(LoadType As String)
        Try

            GridVwLang.DataSource = Process.SearchData("Emp_Languages_ThirdParty_get_all", txtEmpID.Text.Trim)
            GridVwLang.AllowSorting = True
            GridVwLang.AllowPaging = True
            GridVwLang.DataBind()
        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub
    Private Sub LoadSkills(LoadType As String)
        Try
            'If LoadType = "All" Then
            '    GridVwSkills.DataSource = Process.SearchData("Emp_Skills_ThirdParty_get_all", txtEmpID.Text.Trim)
            'End If
            GridVwSkills.DataSource = Process.SearchData("Emp_Skills_ThirdParty_get_all", txtEmpID.Text.Trim)
            GridVwSkills.AllowSorting = True
            GridVwSkills.AllowPaging = True
            GridVwSkills.DataBind()
        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub
    Private Sub LoadCertification(LoadType As String)
        Try
            'If LoadType = "All" Then
            '    GridVwCertification.DataSource = Process.SearchData("Emp_Certifications_ThirdParty_get_all", txtEmpID.Text.Trim)
            'End If
            GridVwCertification.DataSource = Process.SearchData("Emp_Certifications_ThirdParty_get_all", txtEmpID.Text.Trim)
            GridVwCertification.AllowSorting = True
            GridVwCertification.AllowPaging = True
            GridVwCertification.DataBind()
        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub

    Private Sub LoadEducation(LoadType As String)
        Try
            'If LoadType = "All" Then
            '    GridVwEducation.DataSource = Process.SearchData("Emp_Education_ThirdParty_get_all", txtEmpID.Text.Trim)
            'End If
            GridVwEducation.DataSource = Process.SearchData("Emp_Education_ThirdParty_get_all", txtEmpID.Text.Trim)
            GridVwEducation.AllowSorting = True
            GridVwEducation.AllowPaging = True
            GridVwEducation.DataBind()
        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "SortGrids"

    Protected Sub SortLanguages(ByVal sender As Object, ByVal e As GridViewSortEventArgs) Handles GridVwLang.Sorting
        Try
            Dim sortExpression As String = e.SortExpression
            Dim direction As String = String.Empty
            If SortDirection = SortDirection.Ascending Then
                SortDirection = SortDirection.Descending
                direction = " DESC"
            Else
                SortDirection = SortDirection.Ascending
                direction = " ASC"
            End If
            Dim table As DataTable = Process.SearchData("Emp_languages_thirdparty_get_all", txtEmpID.Text.Trim)
            table.DefaultView.Sort = sortExpression & direction
            GridVwLang.DataSource = table
            GridVwLang.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub SortSkills(ByVal sender As Object, ByVal e As GridViewSortEventArgs) Handles GridVwSkills.Sorting
        Try
            Dim sortExpression As String = e.SortExpression
            Dim direction As String = String.Empty
            If SortDirection = SortDirection.Ascending Then
                SortDirection = SortDirection.Descending
                direction = " DESC"
            Else
                SortDirection = SortDirection.Ascending
                direction = " ASC"
            End If
            Dim table As DataTable = Process.SearchData("Emp_Skills_ThirdParty_get_all", txtEmpID.Text.Trim)
            table.DefaultView.Sort = sortExpression & direction
            GridVwSkills.DataSource = table
            GridVwSkills.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub SortCertifications(ByVal sender As Object, ByVal e As GridViewSortEventArgs) Handles GridVwCertification.Sorting
        Try
            Dim sortExpression As String = e.SortExpression
            Dim direction As String = String.Empty
            If SortDirection = SortDirection.Ascending Then
                SortDirection = SortDirection.Descending
                direction = " DESC"
            Else
                SortDirection = SortDirection.Ascending
                direction = " ASC"
            End If
            Dim table As DataTable = Process.SearchData("Emp_Certifications_ThirdParty_get_all", txtEmpID.Text.Trim)
            table.DefaultView.Sort = sortExpression & direction
            GridVwCertification.DataSource = table
            GridVwCertification.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub SortEducation(ByVal sender As Object, ByVal e As GridViewSortEventArgs) Handles GridVwEducation.Sorting
        Try
            Dim sortExpression As String = e.SortExpression
            Dim direction As String = String.Empty
            If SortDirection = SortDirection.Ascending Then
                SortDirection = SortDirection.Descending
                direction = " DESC"
            Else
                SortDirection = SortDirection.Ascending
                direction = " ASC"
            End If
            Dim table As DataTable = Process.SearchData("Emp_Education_ThirdParty_get_all", txtEmpID.Text.Trim)
            table.DefaultView.Sort = sortExpression & direction
            GridVwEducation.DataSource = table
            GridVwEducation.DataBind()
        Catch ex As Exception

        End Try
    End Sub
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try



            If Not Me.IsPostBack Then
                'TabContainer1.ActiveTabIndex = 0

                Process.LoadRadDropDownTextAndValue(radNationality, "Nationalities_get_all", "name", "name", False)
                Process.LoadRadDropDownTextAndValue(radCountryofBirth, "CountryTable_get", "Country", "Country", False)
                Process.LoadRadDropDownTextAndValue(radResidenceCountry, "CountryTable_get_all", "Country", "Country", False)
                Process.LoadRadDropDownTextAndValue(radEmerRelationship1, "emp_relationship_get_all", "name", "name", False)
                Process.LoadRadDropDownTextAndValue(radEmerRelationship2, "emp_relationship_get_all", "name", "name", False)
                Process.LoadRadDropDownTextAndValue(radCompany, "Recruit_ThirdParty_Company_get_all", "companyname", "companyname", False)

                radGender.Items.Clear()
                radGender.Items.Add("Female")
                radGender.Items.Add("Male")

                radMaritalStatus.Items.Clear()
                radMaritalStatus.Items.Add("Single")
                radMaritalStatus.Items.Add("Married")
                radMaritalStatus.Items.Add("Divorced")
                radMaritalStatus.Items.Add("Widowed")

                cboRefConfirm1.Items.Clear()
                cboRefConfirm1.Items.Add("No")
                cboRefConfirm1.Items.Add("Yes")

                cboRefConfirm2.Items.Clear()
                cboRefConfirm2.Items.Add("No")
                cboRefConfirm2.Items.Add("Yes")

                If Request.QueryString("id") IsNot Nothing Then
                    Dim EmpID As String = Request.QueryString("id")
                    txtEmpID.Text = EmpID
                    txtID.Text = EmpID
                    Session("EmpID") = EmpID

                    'Load Personal
                    LoadPersonalDetail(EmpID)
                    LoadContacts(EmpID)
                    LoadEmergencyContact(EmpID)

                    LoadSkills("All")
                    LoadEducation("All")
                    LoadLanguages("All")
                    LoadCertification("All")

                Else
                    txtID.Text = "0"
                    txtEmpID.Text = "0"
                    LoadSkills("All")
                    LoadEducation("All")
                    LoadLanguages("All")
                    LoadCertification("All")

                End If
            End If
        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub


    Private Sub GridVwSkills_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridVwSkills.PageIndexChanging
        Try
            GridVwSkills.PageIndex = e.NewPageIndex
            GridVwSkills.DataSource = Process.SearchData("Emp_Skills_ThirdParty_get_all", txtEmpID.Text.Trim)
            GridVwSkills.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Public Property SortDirection() As SortDirection
        Get
            If ViewState("SortDirection") Is Nothing Then
                ViewState("SortDirection") = SortDirection.Ascending
            End If
            Return DirectCast(ViewState("SortDirection"), SortDirection)
        End Get
        Set(ByVal value As SortDirection)
            ViewState("SortDirection") = value
        End Set
    End Property


    Protected Sub OnRowDataBoundLang(sender As Object, e As GridViewRowEventArgs) Handles GridVwLang.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GridVwLang, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub
    Protected Sub OnRowDataBoundEdu(sender As Object, e As GridViewRowEventArgs) Handles GridVwEducation.RowDataBound, GridVwEducation.RowDataBound, GridVwEducation.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GridVwEducation, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub
    Protected Sub OnRowDataBoundSkills(sender As Object, e As GridViewRowEventArgs) Handles GridVwSkills.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GridVwSkills, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub
    Protected Sub OnRowDataBoundCert(sender As Object, e As GridViewRowEventArgs) Handles GridVwCertification.RowDataBound, GridVwCertification.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GridVwCertification, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub



    Protected Sub GridVwLang_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridVwLang.PageIndexChanging
        Try
            GridVwLang.PageIndex = e.NewPageIndex
            GridVwLang.DataSource = Process.SearchData("Emp_languages_thirdparty_get_all", txtEmpID.Text.Trim)
            GridVwLang.DataBind()
        Catch ex As Exception
        End Try
    End Sub



    Protected Sub GridVwEducation_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridVwEducation.PageIndexChanging
        Try
            GridVwEducation.PageIndex = e.NewPageIndex
            GridVwEducation.DataSource = Process.SearchData("Emp_Education_ThirdParty_get_all", txtEmpID.Text.Trim)
            GridVwEducation.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub GridVwCertification_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridVwCertification.PageIndexChanging
        Try
            GridVwCertification.PageIndex = e.NewPageIndex
            GridVwCertification.DataSource = Process.SearchData("Emp_Certifications_ThirdParty_get_all", txtEmpID.Text.Trim)
            GridVwCertification.DataBind()
        Catch ex As Exception
        End Try
    End Sub
    Private Function GetEMPIdentity(ByVal empid As String, ByVal FirstName As String, ByVal MiddleName As String, _
                                 ByVal LastName As String, ByVal Gender As String, ByVal MaritalStatus As String, _
                                  ByVal Nationality As String, ByVal DateOfBirth As Date, ByVal BloodGroup As String, _
                                  ByVal StateOfOrigin As String, ByVal IDMethod As String, ByVal IDNo As String, _
                                 ByVal IDExpiryDate As Date, ByVal IDIssuer As String, ByVal CountryOfBirth As String, _
                                  ByVal PlaceOfBirth As String, ByVal Hobbies As String, ByVal DateJoin As Date, _
                                  ByVal Photo As String, ByVal workshift As String, ByVal company As String) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Emp_PersonalDetail_ThirdParty_update"
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@EmpID", SqlDbType.VarChar).Value = "0"
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
            cmd.Parameters.Add("@workshift", SqlDbType.VarChar).Value = workshift
            cmd.Parameters.Add("@company", SqlDbType.VarChar).Value = company
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception
            Process.strExp = ex.Message
            Return 0
        End Try
    End Function

    Protected Sub btnAddPDetail_Click(sender As Object, e As EventArgs) Handles btnAddPDetail.Click
        Try

            Dim ImgPhoto As New Byte

            If txtID.Text <> "0" And txtID.Text.Trim <> "" Then
                If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False Then
                    ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
                    Exit Sub
                End If
            End If


            If txtEmpID.Text.Trim = "" Then
                lblstatus.Text = "Assign Employee ID to Employee!"
                txtEmpID.Focus()
                Exit Sub
            End If

            If (txtFirstName.Text.Trim = "") Then
                lblstatus.Text = "First Name required!"
                txtFirstName.Focus()
                Exit Sub
            End If

            If (txtLastName.Text.Trim = "") Then
                lblstatus.Text = "Last Name required!"
                txtLastName.Focus()
                Exit Sub
            End If

            If radGender.SelectedText.Trim = "" Then
                lblstatus.Text = "Gender required!"
                radGender.Focus()
                Exit Sub
            End If

            If radCompany.SelectedText.Trim = "" Then
                lblstatus.Text = "Company required!"
                radCompany.Focus()
                Exit Sub
            End If

            If radMaritalStatus.SelectedText.Trim = "" Then
                lblstatus.Text = "Marital Status required!"
                radMaritalStatus.Focus()
                Exit Sub
            End If

            If radDOB.SelectedDate Is Nothing Then
                lblstatus.Text = "Date of Birth required!"
                radDOB.Focus()
                Exit Sub
            End If

            If radIDExpiry.SelectedDate Is Nothing Then
                lblstatus.Text = "ID Expiration Date required!"
                radIDExpiry.Focus()
                Exit Sub
            End If

            If radDateJoin.SelectedDate Is Nothing Then
                lblstatus.Text = "Date Employee joined required!"
                radDateJoin.Focus()
                Exit Sub
            End If

            btnAddPDetail.Enabled = False



            'Old Data
            If txtID.Text <> "0" And txtID.Text.Trim <> "" Then
                Dim strPersonal As New DataSet
                strPersonal = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_PersonalDetail_ThirdParty_get_id", txtID.Text)
                olddata(0) = strPersonal.Tables(0).Rows(0).Item("id").ToString
                olddata(1) = strPersonal.Tables(0).Rows(0).Item("EmpID").ToString
                olddata(2) = strPersonal.Tables(0).Rows(0).Item("FirstName").ToString
                olddata(3) = strPersonal.Tables(0).Rows(0).Item("MiddleName").ToString
                olddata(4) = strPersonal.Tables(0).Rows(0).Item("LastName").ToString
                olddata(5) = strPersonal.Tables(0).Rows(0).Item("Gender").ToString
                olddata(6) = strPersonal.Tables(0).Rows(0).Item("MaritalStatus").ToString
                olddata(7) = strPersonal.Tables(0).Rows(0).Item("Nationality").ToString
                olddata(8) = strPersonal.Tables(0).Rows(0).Item("DateOfBirth").ToString
                olddata(9) = strPersonal.Tables(0).Rows(0).Item("BloodGroup").ToString
                olddata(10) = strPersonal.Tables(0).Rows(0).Item("StateOfOrigin").ToString
                olddata(11) = strPersonal.Tables(0).Rows(0).Item("IDMethod").ToString
                olddata(12) = strPersonal.Tables(0).Rows(0).Item("IDNo").ToString
                olddata(13) = strPersonal.Tables(0).Rows(0).Item("IDExpiryDate").ToString
                olddata(14) = strPersonal.Tables(0).Rows(0).Item("IDIssuer").ToString
                olddata(15) = strPersonal.Tables(0).Rows(0).Item("CountryOfBirth").ToString
                olddata(16) = strPersonal.Tables(0).Rows(0).Item("Placeofbirth").ToString
                olddata(17) = strPersonal.Tables(0).Rows(0).Item("Hobbies").ToString
                olddata(18) = strPersonal.Tables(0).Rows(0).Item("DateJoin").ToString
                'olddata(19) = strPersonal.Tables(0).Rows(0).Item("ConfirmationDate").ToString
                olddata(19) = strPersonal.Tables(0).Rows(0).Item("TerminationDate").ToString
            End If

            If txtIDNo.Text Is Nothing Then
                txtIDNo.Text = ""
            End If
            If txtStateOrigin.Text Is Nothing Then
                txtStateOrigin.Text = ""
            End If
            If txtIDIssuer.Text Is Nothing Then
                txtIDIssuer.Text = ""
            End If
            If txtHobbies.Text Is Nothing Then
                txtHobbies.Text = ""
            End If
            If txtPlaceOfBirth.Text Is Nothing Then
                txtPlaceOfBirth.Text = ""
            End If
            If txtMidName.Text Is Nothing Then
                txtMidName.Text = ""
            End If

            PersonalDetail.EmpID = txtEmpID.Text.Trim
            PersonalDetail.FirstName = txtFirstName.Text.Trim
            PersonalDetail.MiddleName = txtMidName.Text.Trim
            PersonalDetail.LastName = txtLastName.Text.Trim
            PersonalDetail.Gender = radGender.SelectedText
            PersonalDetail.MaritalStatus = radMaritalStatus.SelectedText
            PersonalDetail.Nationality = radNationality.SelectedText
            PersonalDetail.DateOfBirth = radDOB.SelectedDate
            PersonalDetail.BloodGroup = txtBloodGrp.Text.Trim
            PersonalDetail.StateOfOrigin = txtStateOrigin.Text.Trim
            PersonalDetail.IdentityMethod = txtIDType.Text.Trim
            PersonalDetail.IDNo = txtIDNo.Text.Trim
            PersonalDetail.IDExpiryDate = radIDExpiry.SelectedDate
            PersonalDetail.IDIssuer = txtIDIssuer.Text.Trim
            PersonalDetail.CountryOfBirth = radCountryofBirth.SelectedText
            PersonalDetail.PlaceOfBirth = txtPlaceOfBirth.Text.Trim
            PersonalDetail.Hobbies = txtHobbies.Text.Trim
            PersonalDetail.DateJoin = radDateJoin.SelectedDate

            'If radConfirmDate.SelectedDate Is Nothing Then
            'Else
            '    PersonalDetail.ConfirmationDate = radConfirmDate.SelectedDate
            'End If

            'PersonalDetail.TerminationDate = radTerminationDate.SelectedDate
            If txtID.Text.Trim = "" Then
                PersonalDetail.ID = 0
            Else
                PersonalDetail.ID = txtID.Text
            End If

            Dim OldValue As String = ""
            Dim NewValue As String = ""

            Dim j As Integer = 0

            If txtID.Text <> "0" And txtID.Text.Trim <> "" Then 'Updates
                For Each a In GetType(clsEmpPersonalDetail).GetProperties()
                    If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If IsNumeric(a.GetValue(PersonalDetail, Nothing)) = True And IsNumeric(olddata(j)) = True Then
                                If CDbl(a.GetValue(PersonalDetail, Nothing)) <> CDbl(olddata(j)) Then
                                    NewValue += a.Name + ": " + a.GetValue(PersonalDetail, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            Else
                                If a.GetValue(PersonalDetail, Nothing).ToString <> olddata(j).ToString Then
                                    NewValue += a.Name + ": " + a.GetValue(PersonalDetail, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            End If
                        End If
                    End If
                    j = j + 1
                Next
            Else
                For Each a In GetType(clsEmpPersonalDetail).GetProperties() 'New Entries
                    If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If a.GetValue(PersonalDetail, Nothing) = Nothing Then
                                NewValue += a.Name + ":" + " " & vbCrLf
                            Else
                                NewValue += a.Name + ": " + a.GetValue(PersonalDetail, Nothing).ToString & vbCrLf
                            End If
                        End If
                    End If
                Next
            End If



            If txtID.Text <> "0" And txtID.Text.Trim <> "" Then
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_PersonalDetail_ThirdParty_update", PersonalDetail.ID, PersonalDetail.EmpID.ToUpper, PersonalDetail.FirstName, _
                                  PersonalDetail.MiddleName, PersonalDetail.LastName, PersonalDetail.Gender, PersonalDetail.MaritalStatus, PersonalDetail.Nationality, _
                                  PersonalDetail.DateOfBirth, PersonalDetail.BloodGroup, PersonalDetail.StateOfOrigin, PersonalDetail.IdentityMethod, PersonalDetail.IDNo, _
                                  PersonalDetail.IDExpiryDate, PersonalDetail.IDIssuer, PersonalDetail.CountryOfBirth, PersonalDetail.PlaceOfBirth, PersonalDetail.Hobbies, _
                                  PersonalDetail.DateJoin, txtImage.Text, "", radCompany.SelectedText)
            Else
                txtID.Text = GetEMPIdentity(PersonalDetail.EmpID.ToUpper, PersonalDetail.FirstName, _
                                  PersonalDetail.MiddleName, PersonalDetail.LastName, PersonalDetail.Gender, PersonalDetail.MaritalStatus, PersonalDetail.Nationality, _
                                  PersonalDetail.DateOfBirth, PersonalDetail.BloodGroup, PersonalDetail.StateOfOrigin, PersonalDetail.IdentityMethod, PersonalDetail.IDNo, _
                                  PersonalDetail.IDExpiryDate, PersonalDetail.IDIssuer, PersonalDetail.CountryOfBirth, PersonalDetail.PlaceOfBirth, PersonalDetail.Hobbies, _
                                  PersonalDetail.DateJoin, txtImage.Text, "", radCompany.SelectedText)

                If txtID.Text = "0" Then
                    lblstatus.Text = Process.strExp
                    Exit Sub
                End If
            End If






            If NewValue.Trim = "" And OldValue.Trim = "" Then
            Else
                If txtID.Text <> "0" And txtID.Text.Trim <> "" Then
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Updated " + PersonalDetail.EmpID, "Employee Personal Detail")
                Else
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Inserted", "Employee Personal Detail")
                End If
            End If

            Session("EmpID") = PersonalDetail.EmpID
            imgProfile.ImageUrl = "ImgThirdHandler.ashx?imgid=" & txtID.Text

            lblstatus.Text = "Personal Detail Record saved"
            'Response.Write("<script language='javascript'> { self.close() }</script>")
        Catch ex As Exception
            lblstatus.Text = ex.Message
        Finally
            btnAddPDetail.Enabled = True
        End Try
    End Sub

    Protected Sub btnDelLang_Click(sender As Object, e As EventArgs) Handles btnDelLang.Click
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Delete") = False Then
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
                Exit Sub
            End If
            Dim confirmValue As String = Request.Form("confirm_value")

            If confirmValue = "Yes" Then
                Dim atLeastOneRowDeleted As Boolean = False
                For Each row As GridViewRow In GridVwLang.Rows
                    Dim cb As CheckBox = row.FindControl("chkEmp0")
                    If cb IsNot Nothing AndAlso cb.Checked Then
                        atLeastOneRowDeleted = True
                        Dim ID As String = Convert.ToString(GridVwLang.DataKeys(row.RowIndex).Value)
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Languages_ThirdParty_delete", ID)
                    End If
                Next
                LoadLanguages("All")
            Else
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Delete has been cancelled" + "')", True)
            End If
        Catch ex As Exception
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    Protected Sub btnAddContact_Click(sender As Object, e As EventArgs) Handles btnAddContact.Click
        Try
            If txtIDContact.Text.Trim <> "" And txtIDContact.Text.Trim <> "0" Then
                If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False Then
                    ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
                    Exit Sub
                End If
            End If
            If (txtcAddr1.Text.Trim = "") Then
                lblstatus.Text = "Address required!"
                txtcAddr1.Focus()
                Exit Sub
            End If

            If (radResidenceCountry.SelectedValue = "") Then
                lblstatus.Text = "Country of residence required!"
                radResidenceCountry.Focus()
                Exit Sub
            End If

            If txtCity.Text.Trim = "" Then
                lblstatus.Text = "Residence City required!"
                txtCity.Focus()
                Exit Sub
            End If

            If txtMobileNo.Text.Trim = "" Then
                lblstatus.Text = "Mobile Number required!"
                txtMobileNo.Focus()
                Exit Sub
            End If

            If txtWorkEmail.Text.Trim = "" Then
                lblstatus.Text = "Work Email required!"
                txtWorkEmail.Focus()
                Exit Sub
            End If

            'Old Data
            If txtIDContact.Text.Trim <> "" And txtIDContact.Text.Trim <> "0" Then
                Dim strContacts As New DataSet
                strContacts = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Contact_Info_ThirdParty_get", txtIDContact.Text)
                olddata(0) = strContacts.Tables(0).Rows(0).Item("id").ToString
                olddata(1) = strContacts.Tables(0).Rows(0).Item("Empid").ToString
                olddata(2) = strContacts.Tables(0).Rows(0).Item("Address1").ToString
                olddata(3) = strContacts.Tables(0).Rows(0).Item("Address2").ToString
                olddata(4) = strContacts.Tables(0).Rows(0).Item("City").ToString
                olddata(5) = strContacts.Tables(0).Rows(0).Item("Country").ToString
                olddata(6) = strContacts.Tables(0).Rows(0).Item("PostalAddress").ToString
                olddata(7) = strContacts.Tables(0).Rows(0).Item("MobileNo").ToString
                olddata(8) = strContacts.Tables(0).Rows(0).Item("HomePhone").ToString
                olddata(9) = strContacts.Tables(0).Rows(0).Item("PersonalEMail").ToString
                olddata(10) = strContacts.Tables(0).Rows(0).Item("WorkEmail").ToString
                olddata(11) = strContacts.Tables(0).Rows(0).Item("WorkPhone").ToString
            End If

            If txtIDContact.Text.Trim = "" Then
                ContactDetail.ID = 0
            Else
                ContactDetail.ID = txtIDContact.Text
            End If

            If txtMobileNo.Text Is Nothing Then
                txtMobileNo.Text = ""
            End If
            If txtHomePhone.Text Is Nothing Then
                txtHomePhone.Text = ""
            End If
            If txtWorkPhone.Text Is Nothing Then
                txtWorkPhone.Text = ""
            End If
            If txtcAddr1.Text Is Nothing Then
                txtcAddr1.Text = ""
            End If
            If txtAddr2.Text Is Nothing Then
                txtAddr2.Text = ""
            End If
            If txtPostalAddr.Text Is Nothing Then
                txtPostalAddr.Text = ""
            End If
            If txtEmail.Text Is Nothing Then
                txtEmail.Text = ""
            End If
            If txtCity.Text Is Nothing Then
                txtCity.Text = ""
            End If

            ContactDetail.EmpID = txtEmpID.Text.Trim
            ContactDetail.Address1 = txtcAddr1.Text
            ContactDetail.Address2 = txtAddr2.Text
            ContactDetail.PostalAddress = txtPostalAddr.Text
            ContactDetail.PersonalEmail = txtEmail.Text
            ContactDetail.City = txtCity.Text
            ContactDetail.Country = radResidenceCountry.SelectedText
            ContactDetail.HomePhone = txtHomePhone.Text
            ContactDetail.MobileNo = txtMobileNo.Text
            ContactDetail.WorkPhone = txtWorkPhone.Text
            ContactDetail.WorkEmail = txtWorkEmail.Text


            Dim OldValue As String = ""
            Dim NewValue As String = ""

            Dim j As Integer = 0

            If txtIDContact.Text.Trim <> "" And txtIDContact.Text.Trim <> "0" Then 'Updates
                For Each a In GetType(clsEmpContactInfo).GetProperties()
                    If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If IsNumeric(a.GetValue(ContactDetail, Nothing)) = True And IsNumeric(olddata(j)) = True Then
                                If CDbl(a.GetValue(ContactDetail, Nothing)) <> CDbl(olddata(j)) Then
                                    NewValue += a.Name + ": " + a.GetValue(ContactDetail, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            Else
                                If a.GetValue(ContactDetail, Nothing).ToString <> olddata(j).ToString Then
                                    NewValue += a.Name + ": " + a.GetValue(ContactDetail, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            End If
                        End If
                    End If
                    j = j + 1
                Next
            Else
                For Each a In GetType(clsEmpContactInfo).GetProperties() 'New Entries
                    If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If a.GetValue(ContactDetail, Nothing) = Nothing Then
                                NewValue += a.Name + ":" + " " & vbCrLf
                            Else
                                NewValue += a.Name + ": " + a.GetValue(ContactDetail, Nothing).ToString & vbCrLf
                            End If
                        End If
                    End If
                Next
            End If

            If txtIDContact.Text.Trim <> "" And txtIDContact.Text.Trim <> "0" Then
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Contact_Info_ThirdParty_update", ContactDetail.ID, ContactDetail.EmpID, ContactDetail.Address1, ContactDetail.Address2, _
            ContactDetail.City, ContactDetail.Country, ContactDetail.PostalAddress, ContactDetail.MobileNo, ContactDetail.HomePhone, ContactDetail.PersonalEmail, ContactDetail.WorkEmail,
            ContactDetail.WorkPhone)
            Else
                txtIDContact.Text = GetContactIdentity(ContactDetail.EmpID, ContactDetail.Address1, ContactDetail.Address2, _
            ContactDetail.City, ContactDetail.Country, ContactDetail.PostalAddress, ContactDetail.MobileNo, ContactDetail.HomePhone, ContactDetail.PersonalEmail, ContactDetail.WorkEmail,
            ContactDetail.WorkPhone)
            End If


            If NewValue.Trim = "" And OldValue.Trim = "" Then
            Else
                If txtIDContact.Text.Trim <> "" And txtIDContact.Text.Trim <> "0" Then
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Updated " + PersonalDetail.EmpID, "Employee Contact Detail")
                Else
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Inserted", "Employee Contact Detail")
                End If

            End If
            lblstatus.Text = "Record saved"
            'Response.Write("<script language='javascript'> { self.close() }</script>")
        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try

    End Sub
    Private Function GetEmergencyIdentity(ByVal empid As String, ByVal name1 As String, ByVal address1 As String, _
                                 ByVal phone1 As String, ByVal relationship1 As String, ByVal name2 As String, _
                                  ByVal address2 As String, ByVal phone2 As String, ByVal relationship2 As String, _
                                  ByVal RefereeName1 As String, ByVal RefereeAddress1 As String, ByVal RefereePhone1 As String, _
                                 ByVal RefereeEmail1 As String, ByVal RefereePostion1 As String, ByVal RefereeYears1 As Integer, _
                                  ByVal RefereeName2 As String, ByVal RefereeAddress2 As String, ByVal RefereePhone2 As String, _
                                  ByVal RefereeEmail2 As String, ByVal RefereePostion2 As String, ByVal RefereeYears2 As Integer) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Emp_Emergency_Contact_ThirdParty_Update"
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
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception
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
            cmd.CommandText = "Emp_Contact_Info_ThirdParty_update"
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
            Return 0
        End Try
    End Function

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            If txtIDEmergency.Text <> "0" Or txtIDEmergency.Text.Trim <> "" Then
                If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False Then
                    ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
                    Exit Sub
                End If
            End If


            If (txtEmerName1.Text.Trim = "") Then
                lblstatus.Text = "Emergency Contact Name required!"
                txtEmerName1.Focus()
                Exit Sub
            End If

            If txtEmerAddr1.Text.Trim = "" Then
                lblstatus.Text = "Emergency Contact Address required!"
                txtEmerAddr1.Focus()
                Exit Sub
            End If

            If txtEmerNo1.Text.Trim = "" Then
                lblstatus.Text = "Emergency Mobile Number required!"
                txtEmerNo1.Focus()
                Exit Sub
            End If

            If radEmerRelationship1.SelectedValue.Trim = "" Then
                lblstatus.Text = "Emergency Contact Relationship required!"
                radEmerRelationship1.Focus()
                Exit Sub
            End If

            If txtEmerName2.Text.Trim IsNot Nothing Then
                If txtEmerNo2.Text.Trim Is Nothing Then
                    lblstatus.Text = "Contact 2 Phone required!"
                    txtEmerNo2.Focus()
                    Exit Sub
                End If

                If txtEmerAddr2.Text.Trim Is Nothing Then
                    lblstatus.Text = "Contact 2 Address required!"
                    txtEmerAddr2.Focus()
                    Exit Sub
                End If

                If radEmerRelationship2.SelectedValue.Trim = "" Then
                    lblstatus.Text = "Emergency Contact 2 Relationship required!"
                    radEmerRelationship2.Focus()
                    Exit Sub
                End If

            End If

            If txtRefName1.Text.Trim IsNot Nothing Then
                If txtRefAddress1.Text.Trim Is Nothing Then
                    lblstatus.Text = "Referee 1 Address required!"
                    txtRefAddress1.Focus()
                    Exit Sub
                End If
                If txtRefEmail1.Text.Trim Is Nothing Or txtRefEmail1.Text.Contains("@") = False Then
                    lblstatus.Text = "Referee 1 Email required!"
                    txtRefEmail1.Focus()
                    Exit Sub
                End If
                If txtRefPhone1.Text.Trim Is Nothing Then
                    lblstatus.Text = "Referee 1 Phone required!"
                    txtRefPhone1.Focus()
                    Exit Sub
                End If
                If txtRefPostion1.Text.Trim Is Nothing Then
                    lblstatus.Text = "Referee 1 Job Position required!"
                    txtRefPostion1.Focus()
                    Exit Sub
                End If
                If txtRefYear1.Text.Trim Is Nothing And IsNumeric(txtRefYear1.Text) = False Then
                    lblstatus.Text = "Referee 1 Years known required!"
                    txtRefYear1.Focus()
                    Exit Sub
                End If
            End If

            If txtRefName2.Text.Trim IsNot Nothing Then
                If txtRefAddress2.Text.Trim Is Nothing Then
                    lblstatus.Text = "Referee 2 Address required!"
                    txtRefAddress2.Focus()
                    Exit Sub
                End If
                If txtRefEmail2.Text.Trim Is Nothing Or txtRefEmail2.Text.Contains("@") = False Then
                    lblstatus.Text = "Referee 2 Email required!"
                    txtRefEmail2.Focus()
                    Exit Sub
                End If
                If txtRefPhone2.Text.Trim Is Nothing Then
                    lblstatus.Text = "Referee 2 Phone required!"
                    txtRefPhone2.Focus()
                    Exit Sub
                End If
                If txtRefPostion2.Text.Trim Is Nothing Then
                    lblstatus.Text = "Referee 2 Job Position required!"
                    txtRefPostion2.Focus()
                    Exit Sub
                End If
                If txtRefYear2.Text.Trim Is Nothing And IsNumeric(txtRefYear1.Text) = False Then
                    lblstatus.Text = "Referee 2 Years known required!"
                    txtRefYear2.Focus()
                    Exit Sub
                End If
            End If

            'Old Data
            If txtIDEmergency.Text.Trim <> "" And txtIDEmergency.Text.Trim <> "0" Then
                Dim strEmergency As New DataSet
                strEmergency = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Emergency_Contact_ThirdParty_get", txtIDEmergency.Text.Trim)
                olddata(0) = strEmergency.Tables(0).Rows(0).Item("id").ToString
                olddata(1) = strEmergency.Tables(0).Rows(0).Item("Empid").ToString
                olddata(2) = strEmergency.Tables(0).Rows(0).Item("Name1").ToString
                olddata(3) = strEmergency.Tables(0).Rows(0).Item("Address1").ToString
                olddata(4) = strEmergency.Tables(0).Rows(0).Item("Phone1").ToString
                olddata(5) = strEmergency.Tables(0).Rows(0).Item("Relationship1").ToString
                olddata(6) = strEmergency.Tables(0).Rows(0).Item("Name2").ToString
                olddata(7) = strEmergency.Tables(0).Rows(0).Item("Address2").ToString
                olddata(8) = strEmergency.Tables(0).Rows(0).Item("Phone2").ToString
                olddata(9) = strEmergency.Tables(0).Rows(0).Item("Relationship2").ToString
            End If

            If txtAddr2.Text Is Nothing Then
                txtAddr2.Text = ""
            End If

            If txtEmerAddr2.Text Is Nothing Then
                txtEmerAddr2.Text = ""
            End If

            If txtEmerName2.Text Is Nothing Then
                txtEmerName2.Text = ""
            End If

            If txtEmerNo2.Text Is Nothing Then
                txtEmerNo2.Text = ""
            End If


            If txtIDContact.Text.Trim = "" Then
                EmergencyDetail.ID = 0
            Else
                EmergencyDetail.ID = txtIDEmergency.Text
            End If

            EmergencyDetail.Name1 = txtEmerName1.Text
            EmergencyDetail.Address1 = txtEmerAddr1.Text
            EmergencyDetail.Phone1 = txtEmerNo1.Text
            EmergencyDetail.RelationShip1 = radEmerRelationship1.SelectedText
            EmergencyDetail.Name2 = txtEmerName2.Text
            EmergencyDetail.Address2 = txtEmerAddr2.Text
            EmergencyDetail.Phone2 = txtEmerNo2.Text
            EmergencyDetail.RelationShip2 = radEmerRelationship2.SelectedText


            Dim OldValue As String = ""
            Dim NewValue As String = ""

            Dim j As Integer = 0

            If txtIDEmergency.Text.Trim <> "" And txtIDEmergency.Text.Trim <> "0" Then 'Updates
                For Each a In GetType(clsEmpEmergency).GetProperties()
                    If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If IsNumeric(a.GetValue(EmergencyDetail, Nothing)) = True And IsNumeric(olddata(j)) = True Then
                                If CDbl(a.GetValue(EmergencyDetail, Nothing)) <> CDbl(olddata(j)) Then
                                    NewValue += a.Name + ": " + a.GetValue(EmergencyDetail, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            Else
                                If a.GetValue(EmergencyDetail, Nothing).ToString <> olddata(j).ToString Then
                                    NewValue += a.Name + ": " + a.GetValue(EmergencyDetail, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            End If
                        End If
                    End If
                    j = j + 1
                Next
            Else
                For Each a In GetType(clsEmpEmergency).GetProperties() 'New Entries
                    If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If a.GetValue(EmergencyDetail, Nothing) = Nothing Then
                                NewValue += a.Name + ":" + " " & vbCrLf
                            Else
                                NewValue += a.Name + ": " + a.GetValue(EmergencyDetail, Nothing).ToString & vbCrLf
                            End If
                        End If
                    End If
                Next
            End If

            If txtIDEmergency.Text.Trim <> "" And txtIDEmergency.Text.Trim <> "0" Then
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Emergency_Contact_ThirdParty_Update", EmergencyDetail.ID, EmergencyDetail.EmpID, EmergencyDetail.Name1, EmergencyDetail.Address1, _
                                      EmergencyDetail.Phone1, EmergencyDetail.RelationShip1, EmergencyDetail.Name2, EmergencyDetail.Address2, _
                                      EmergencyDetail.Phone2, EmergencyDetail.RelationShip2, txtRefName1.Text.Trim, txtRefAddress1.Text.Trim, txtRefPhone1.Text.Trim, txtRefEmail1.Text.Trim, _
                                      txtRefPostion1.Text, txtRefYear1.Text, txtRefName2.Text.Trim, txtRefAddress2.Text.Trim, txtRefPhone2.Text.Trim, txtRefEmail2.Text.Trim, _
                                      txtRefPostion2.Text, txtRefYear2.Text)
            Else
                txtIDEmergency.Text = GetEmergencyIdentity(EmergencyDetail.EmpID, EmergencyDetail.Name1, EmergencyDetail.Address1, _
                                      EmergencyDetail.Phone1, EmergencyDetail.RelationShip1, EmergencyDetail.Name2, EmergencyDetail.Address2, _
                                      EmergencyDetail.Phone2, EmergencyDetail.RelationShip2, txtRefName1.Text.Trim, txtRefAddress1.Text.Trim, txtRefPhone1.Text.Trim, txtRefEmail1.Text.Trim, _
                                      txtRefPostion1.Text, txtRefYear1.Text, txtRefName2.Text.Trim, txtRefAddress2.Text.Trim, txtRefPhone2.Text.Trim, txtRefEmail2.Text.Trim, _
                                      txtRefPostion2.Text, txtRefYear2.Text)
            End If


            If NewValue.Trim = "" And OldValue.Trim = "" Then
            Else
                If txtIDEmergency.Text.Trim <> "" And txtIDEmergency.Text.Trim <> "0" Then
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Updated " + PersonalDetail.EmpID, "Employee Emergency Contacts")
                Else
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Inserted", "Employee Emergency Contacts")
                End If

            End If
            lblstatus.Text = "Contact Detail Record saved"
            'Response.Write("<script language='javascript'> { self.close() }</script>")
        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub



    Protected Sub btnAddLang_Click(sender As Object, e As EventArgs) Handles btnAddLang.Click
        Try
            Session("EmpID") = txtEmpID.Text.Trim
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
                Exit Sub
            End If
            'Response.Write("<script language='javascript'> { popup = window.open(""EmployeeDependant.aspx"" , ""Stone Details"", ""height=500,width=700,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no"");  popup.focus(); }</script>")
            Response.Redirect("~/Module/Recruitment/EmployeeThirdLanguage.aspx", True)
        Catch ex As Exception
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    Protected Sub btnDelSkill_Click(sender As Object, e As EventArgs) Handles btnDelSkill.Click
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Delete") = False Then
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
                Exit Sub
            End If
            Dim confirmValue As String = Request.Form("confirm_value")

            If confirmValue = "Yes" Then
                Dim atLeastOneRowDeleted As Boolean = False
                For Each row As GridViewRow In GridVwSkills.Rows
                    Dim cb As CheckBox = row.FindControl("chkEmp1")
                    If cb IsNot Nothing AndAlso cb.Checked Then
                        atLeastOneRowDeleted = True
                        Dim ID As String = Convert.ToString(GridVwSkills.DataKeys(row.RowIndex).Value)
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Skills_ThirdParty_delete", ID)
                    End If
                Next
                LoadSkills("All")
            Else
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Delete has been cancelled" + "')", True)
            End If
        Catch ex As Exception
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    Protected Sub btnAddSkill_Click(sender As Object, e As EventArgs) Handles btnAddSkill.Click
        Try
            Session("EmpID") = txtEmpID.Text.Trim
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
                Exit Sub
            End If
            'Response.Write("<script language='javascript'> { popup = window.open(""EmployeeDependant.aspx"" , ""Stone Details"", ""height=500,width=700,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no"");  popup.focus(); }</script>")
            Response.Redirect("~/Module/Recruitment/EmployeeThirdSkills.aspx", True)
        Catch ex As Exception
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    Protected Sub btnAddEducation_Click(sender As Object, e As EventArgs) Handles btnAddEducation.Click
        Try
            Session("EmpID") = txtEmpID.Text.Trim
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
                Exit Sub
            End If
            'Response.Write("<script language='javascript'> { popup = window.open(""EmployeeDependant.aspx"" , ""Stone Details"", ""height=500,width=700,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no"");  popup.focus(); }</script>")
            Response.Redirect("~/Module/Recruitment/EmployeeThirdEducation.aspx", True)
        Catch ex As Exception
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    Protected Sub btnDelEducation_Click(sender As Object, e As EventArgs) Handles btnDelEducation.Click
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Delete") = False Then
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
                Exit Sub
            End If
            Dim confirmValue As String = Request.Form("confirm_value")

            If confirmValue = "Yes" Then
                Dim atLeastOneRowDeleted As Boolean = False
                For Each row As GridViewRow In GridVwEducation.Rows
                    Dim cb As CheckBox = row.FindControl("chkEmp2")
                    If cb IsNot Nothing AndAlso cb.Checked Then
                        atLeastOneRowDeleted = True
                        Dim ID As String = Convert.ToString(GridVwEducation.DataKeys(row.RowIndex).Value)
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Education_ThirdParty_delete", ID)
                    End If
                Next
                LoadEducation("All")
            Else
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Delete has been cancelled" + "')", True)
            End If
        Catch ex As Exception
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    Protected Sub btnAddCert_Click(sender As Object, e As EventArgs) Handles btnAddCert.Click
        'EmployeeCertification
        Try
            Session("EmpID") = txtEmpID.Text.Trim
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
                Exit Sub
            End If
            'Response.Write("<script language='javascript'> { popup = window.open(""EmployeeDependant.aspx"" , ""Stone Details"", ""height=500,width=700,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no"");  popup.focus(); }</script>")
            Response.Redirect("~/Module/Recruitment/EmployeeThirdCertification.aspx", True)
        Catch ex As Exception
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    Protected Sub btnDelCert_Click(sender As Object, e As EventArgs) Handles btnDelCert.Click
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Delete") = False Then
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
                Exit Sub
            End If
            Dim confirmValue As String = Request.Form("confirm_value")

            If confirmValue = "Yes" Then
                Dim atLeastOneRowDeleted As Boolean = False
                For Each row As GridViewRow In GridVwCertification.Rows
                    Dim cb As CheckBox = row.FindControl("chkEmp3")
                    If cb IsNot Nothing AndAlso cb.Checked Then
                        atLeastOneRowDeleted = True
                        Dim ID As String = Convert.ToString(GridVwCertification.DataKeys(row.RowIndex).Value)
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Certifications_ThirdParty_delete", ID)
                    End If
                Next
                LoadCertification("All")
            Else
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Delete has been cancelled" + "')", True)
            End If
        Catch ex As Exception
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub



    Private Function GetPhotoIdentity(ByVal empid As String, ByVal photo As Byte(), ByVal imgtype As String) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Emp_PersonalDetail_ThirdParty_update_PhotoImage"
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
    Protected Sub btnImage_Click(sender As Object, e As EventArgs) Handles btnImage.Click
        Try

            'SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_PersonalDetail_ThirdParty_update_photo", txtEmpID.Text, Path)
            If imgUpload.HasFile AndAlso Not imgUpload.PostedFile Is Nothing Then
                Dim img_strm As Stream = imgUpload.PostedFile.InputStream
                'Retrieving the length of the file to upload
                Dim img_len As Integer = imgUpload.PostedFile.ContentLength
                'retrieving the type of the file to upload
                Dim strtype As String = imgUpload.PostedFile.ContentType.ToString()
                Dim strname As String = Path.GetFileName(imgUpload.PostedFile.FileName)
                Dim imgdata As Byte() = New Byte(img_len - 1) {}
                Dim n As Integer = img_strm.Read(imgdata, 0, img_len)

                If txtID.Text = "0" Then
                    txtID.Text = GetPhotoIdentity(txtEmpID.Text.Trim, imgdata, strtype)
                    If txtID.Text = "0" Then
                        lblstatus.Text = Process.strExp
                        Exit Sub
                    Else
                        txtEmpID.Text = txtID.Text
                    End If
                Else
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_PersonalDetail_ThirdParty_update_PhotoImage", txtEmpID.Text.Trim, imgdata, strtype)
                End If
                imgProfile.ImageUrl = "ImgThirdHandler.ashx?imgid=" & txtID.Text

                lblstatus.Text = "Photo uploaded"
            Else
                lblstatus.Text = "No photo selected for upload"
                imgUpload.Focus()
            End If

        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnImage0_Click(sender As Object, e As EventArgs) Handles btnImage0.Click
        Try
            imgProfile.ImageUrl = imgClear.ImageUrl
            If txtID.Text <> "0" Then
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_PersonalDetail_ThirdParty_update_PhotoImage", txtEmpID.Text.Trim, Nothing, "")
            End If

         

        Catch ex As Exception

        End Try
    End Sub
   
   

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Response.Write("<script language='javascript'> { self.close() }</script>")
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            Response.Write("<script language='javascript'> { self.close() }</script>")
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            Response.Write("<script language='javascript'> { self.close() }</script>")
        Catch ex As Exception
        End Try
    End Sub

   
End Class