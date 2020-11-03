Imports Microsoft.ApplicationBlocks.Data
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO


Public Class EmployeeData
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim emailFile As String = ConfigurationManager.AppSettings("FileURL")
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "EMPLIST"
    Dim PersonalDetail As New clsEmpPersonalDetail
    Dim ContactDetail As New clsEmpContactInfo
    Dim EmergencyDetail As New clsEmpEmergency
    Dim olddata(21) As String
    Dim imgByte As Byte() = Nothing

#Region "LoadData"
    Private Sub LoadDependants(LoadType As String)
        Try
            If LoadType = "All" Then
                GridVwHeaderChckbox.DataSource = Process.SearchData("Emp_Dependents_get_all", aempid.Value.Trim)
            End If
            GridVwHeaderChckbox.AllowSorting = True
            GridVwHeaderChckbox.AllowPaging = True
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Sub LoadPersonalDetail(ByVal EmpID As String)
        Try
            Dim strPersonal As New DataSet
            strPersonal = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_PersonalDetail_get", EmpID)
            If strPersonal.Tables(0).Rows.Count > 0 Then
                chkPay.Visible = True
                chkPay.Checked = CBool(strPersonal.Tables(0).Rows(0).Item("suspendpay"))

                txtID.Text = strPersonal.Tables(0).Rows(0).Item("id").ToString

                'imgphoto.Src = "ImgHandler.ashx?imgid=" & txtID.Text
                imgphoto.Src = strPersonal.Tables(0).Rows(0).Item("imgtype").ToString

                afirstname.Value = strPersonal.Tables(0).Rows(0).Item("FirstName").ToString
                amiddlename.Value = strPersonal.Tables(0).Rows(0).Item("MiddleName").ToString
                alastname.Value = strPersonal.Tables(0).Rows(0).Item("LastName").ToString

                pagetitle.InnerText = afirstname.Value & " " & alastname.Value & " Profile"

                Process.AssignHTMLSelectValue(drpgender, strPersonal.Tables(0).Rows(0).Item("Gender").ToString)
                Process.AssignHTMLSelectValue(drpmaritalstatus, strPersonal.Tables(0).Rows(0).Item("MaritalStatus").ToString)
                Process.AssignHTMLSelectValue(drpnationality, strPersonal.Tables(0).Rows(0).Item("Nationality").ToString)


                If IsDBNull(strPersonal.Tables(0).Rows(0).Item("DateOfBirth")) = False Then
                    adateofbirth.SelectedDate = CDate(strPersonal.Tables(0).Rows(0).Item("DateOfBirth"))
                End If

                'txtBloodGrp.Text = strPersonal.Tables(0).Rows(0).Item("BloodGroup").ToString
                astateorigin.Value = strPersonal.Tables(0).Rows(0).Item("StateOfOrigin").ToString
                aidtype.Value = strPersonal.Tables(0).Rows(0).Item("IDMethod").ToString
                aidnumber.Value = strPersonal.Tables(0).Rows(0).Item("IDNo").ToString

                If IsDBNull(strPersonal.Tables(0).Rows(0).Item("confirmationdate")) = False Then
                    adateconfirm.SelectedDate = CDate(strPersonal.Tables(0).Rows(0).Item("confirmationdate")).ToLongDateString
                Else
                    divconfirmdate.Visible = False
                End If

                If IsDBNull(strPersonal.Tables(0).Rows(0).Item("expectedConfirmation")) = False Then
                    adateexpectconfirm.SelectedDate = CDate(strPersonal.Tables(0).Rows(0).Item("expectedConfirmation")).ToLongDateString
                End If


                If IsDBNull(strPersonal.Tables(0).Rows(0).Item("IDExpiryDate")) = False And IsDBNull(strPersonal.Tables(0).Rows(0).Item("IDExpiryDate")).ToString.Contains("AM") = False Then
                    aidexpirydate.SelectedDate = CDate(strPersonal.Tables(0).Rows(0).Item("IDExpiryDate"))
                End If



                aidissuer.Value = strPersonal.Tables(0).Rows(0).Item("IDIssuer").ToString
                Process.AssignHTMLSelectValue(drpcountryofbirth, strPersonal.Tables(0).Rows(0).Item("CountryOfBirth").ToString)

                abirthplace.Value = strPersonal.Tables(0).Rows(0).Item("Placeofbirth").ToString


                If IsDBNull(strPersonal.Tables(0).Rows(0).Item("DateJoin")) = False Then
                    adateresume.SelectedDate = CDate(strPersonal.Tables(0).Rows(0).Item("DateJoin"))
                End If

                If IsDBNull(strPersonal.Tables(0).Rows(0).Item("terminationdate")) = False Then
                    If strPersonal.Tables(0).Rows(0).Item("terminationdate").ToString.Contains("1900") = False Then
                        aterminatedate.SelectedDate = strPersonal.Tables(0).Rows(0).Item("terminationdate")
                    Else
                        divterminatedate.Visible = False
                    End If
                Else
                    divterminatedate.Visible = False
                End If

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, "Basic Information: " & ex.Message, "danger")
        End Try
    End Sub
    Private Sub LoadContacts(ByVal EmpID As String)
        Try
            Dim strContacts As New DataSet
            strContacts = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Contact_Info_get", EmpID)
            If strContacts.Tables(0).Rows.Count > 0 Then
                txtIDContact.Text = strContacts.Tables(0).Rows(0).Item("id").ToString
                aaddress.Value = strContacts.Tables(0).Rows(0).Item("Address1").ToString
                aemailaddress.Value = strContacts.Tables(0).Rows(0).Item("PersonalEMail").ToString
                aaddresscity.Value = strContacts.Tables(0).Rows(0).Item("City").ToString
                Process.AssignHTMLSelectValue(drpaddresscountry, strContacts.Tables(0).Rows(0).Item("Country").ToString)
                aphonenumber.Value = strContacts.Tables(0).Rows(0).Item("MobileNo").ToString
                aworkphonenumber.Value = strContacts.Tables(0).Rows(0).Item("WorkPhone").ToString
                aworkemailaddress.Value = strContacts.Tables(0).Rows(0).Item("WorkEmail").ToString
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, "Contact Information: " & ex.Message, "danger")
        End Try
    End Sub
    Private Sub LoadEmergencyContact(ByVal EmpID As String)
        Try
            Dim strTempEmergency As New DataSet
            strTempEmergency = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Emergency_Contact_Changes_get", EmpID)
            If strTempEmergency.Tables(0).Rows.Count > 0 Then
                tempaemername1.Value = strTempEmergency.Tables(0).Rows(0).Item("Name1").ToString
                tempaemeraddress1.Value = strTempEmergency.Tables(0).Rows(0).Item("Address1").ToString
                tempaemercontactnumber1.Value = strTempEmergency.Tables(0).Rows(0).Item("Phone1").ToString
                Process.AssignHTMLSelectValue(drptemprelation1, strTempEmergency.Tables(0).Rows(0).Item("Relationship1").ToString)

                tempaemername2.Value = strTempEmergency.Tables(0).Rows(0).Item("Name2").ToString
                tempaemeraddress2.Value = strTempEmergency.Tables(0).Rows(0).Item("Address2").ToString
                tempaemercontactnumber2.Value = strTempEmergency.Tables(0).Rows(0).Item("Phone2").ToString
                Process.AssignHTMLSelectValue(drptemprelation2, strTempEmergency.Tables(0).Rows(0).Item("Relationship2").ToString)
                collapse_acc.Visible = True
            Else
                collapse_acc.Visible = False
            End If

            Dim strEmergency As New DataSet
            strEmergency = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Emergency_Contact_get", EmpID)
            'Emergency Contacts
            If strEmergency.Tables(0).Rows.Count > 0 Then
                txtIDEmergency.Text = strEmergency.Tables(0).Rows(0).Item("id").ToString

                aemername1.Value = strEmergency.Tables(0).Rows(0).Item("Name1").ToString
                aemeraddress1.Value = strEmergency.Tables(0).Rows(0).Item("Address1").ToString
                aemercontactnumber1.Value = strEmergency.Tables(0).Rows(0).Item("Phone1").ToString

                Process.AssignHTMLSelectValue(drpemerrelation1, strEmergency.Tables(0).Rows(0).Item("Relationship1").ToString)

                aemername2.Value = strEmergency.Tables(0).Rows(0).Item("Name2").ToString
                aemeraddress2.Value = strEmergency.Tables(0).Rows(0).Item("Address2").ToString
                aemercontactnumber2.Value = strEmergency.Tables(0).Rows(0).Item("Phone2").ToString
                Process.AssignHTMLSelectValue(drprelation2, strEmergency.Tables(0).Rows(0).Item("Relationship2").ToString)

                areforganisation1.Value = strEmergency.Tables(0).Rows(0).Item("RefereeAddress1").ToString
                arefemailaddress1.Value = strEmergency.Tables(0).Rows(0).Item("RefereeEmail1").ToString
                arefname1.Value = strEmergency.Tables(0).Rows(0).Item("RefereeName1").ToString
                arefphonenumber1.Value = strEmergency.Tables(0).Rows(0).Item("RefereePhone1").ToString
                'txtRefPostion1.Text = strEmergency.Tables(0).Rows(0).Item("RefereePostion1").ToString
                arefyrsknown1.Value = strEmergency.Tables(0).Rows(0).Item("RefereeYears1").ToString
                Process.AssignHTMLSelectValue(drprefconfirmed1, strEmergency.Tables(0).Rows(0).Item("RefereeConfirm1").ToString)

                areforganisation2.Value = strEmergency.Tables(0).Rows(0).Item("RefereeAddress2").ToString
                arefemailaddress2.Value = strEmergency.Tables(0).Rows(0).Item("RefereeEmail2").ToString
                arefname2.Value = strEmergency.Tables(0).Rows(0).Item("RefereeName2").ToString
                arefcontactnumber2.Value = strEmergency.Tables(0).Rows(0).Item("RefereePhone2").ToString
                'txtRefPostion2.Text = strEmergency.Tables(0).Rows(0).Item("RefereePostion2").ToString
                arefyears2.Value = strEmergency.Tables(0).Rows(0).Item("RefereeYears2").ToString
                Process.AssignHTMLSelectValue(drprefconfirmed2, strEmergency.Tables(0).Rows(0).Item("RefereeConfirm2").ToString)

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, "Emergency Contact: " & ex.Message, "danger")
        End Try
    End Sub
    Private Sub LoadLanguages(LoadType As String)
        Try

            GridVwLang.DataSource = Process.SearchData("Emp_languages_get_all", aempid.Value.Trim)
            GridVwLang.AllowSorting = True
            GridVwLang.AllowPaging = True
            GridVwLang.DataBind()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Sub LoadSkills(LoadType As String)
        Try
            'If LoadType = "All" Then
            '    GridVwSkills.DataSource = Process.SearchData("Emp_Skills_get_all", aempid.Value.Trim)
            'End If
            GridVwSkills.DataSource = Process.SearchData("Actual_skills_get", aempid.Value.Trim)
            GridVwSkills.AllowSorting = True
            GridVwSkills.AllowPaging = True
            GridVwSkills.DataBind()
        Catch ex As Exception
            Response.Write("S: " & ex.Message)

        End Try
    End Sub
    Private Sub LoadCertification(LoadType As String)
        Try
            'If LoadType = "All" Then
            '    GridVwCertification.DataSource = Process.SearchData("Emp_Certifications_get_all", aempid.Value.Trim)
            'End If
            GridVwCertification.DataSource = Process.SearchData("Emp_Certifications_get_all", aempid.Value.Trim)
            GridVwCertification.AllowSorting = True
            GridVwCertification.AllowPaging = True
            GridVwCertification.DataBind()
        Catch ex As Exception
            Response.Write("Cert: " & ex.Message)
        End Try
    End Sub
    Private Sub LoadWorkHistory(LoadType As String)
        Try
            'If LoadType = "All" Then
            '    GridVwWorkHistory.DataSource = Process.SearchData("Emp_Work_History_get_all", aempid.Value.Trim)
            'End If
            GridVwWorkHistory.DataSource = Process.SearchData("Emp_Work_History_get_all", aempid.Value.Trim)
            GridVwWorkHistory.AllowSorting = True
            GridVwWorkHistory.AllowPaging = True
            GridVwWorkHistory.DataBind()
        Catch ex As Exception
            Response.Write("WH: " & ex.Message)

        End Try
    End Sub
    Private Sub LoadAsset(LoadType As String)
        Try
            'If LoadType = "All" Then
            '    GridVwWorkHistory.DataSource = Process.SearchData("Emp_Work_History_get_all", aempid.Value.Trim)
            'End If
            GridAsset.DataSource = Process.SearchData("Emp_Asset_get_all", aempid.Value.Trim)
            GridAsset.AllowSorting = True
            GridAsset.AllowPaging = True
            GridAsset.DataBind()
        Catch ex As Exception
            Response.Write("WH: " & ex.Message)

        End Try
    End Sub
    Private Sub LoadHobbies(LoadType As String)
        Try
            'If LoadType = "All" Then
            '    GridVwWorkHistory.DataSource = Process.SearchData("Emp_Work_History_get_all", aempid.Value.Trim)
            'End If
            GridHobbies.DataSource = Process.SearchData("Emp_Hobbies_get_all", aempid.Value.Trim)
            GridHobbies.AllowSorting = True
            GridHobbies.AllowPaging = True
            GridHobbies.DataBind()
        Catch ex As Exception
            Response.Write("WH: " & ex.Message)

        End Try
    End Sub
    Private Sub LoadEducation(LoadType As String)
        Try
            'If LoadType = "All" Then
            '    GridVwEducation.DataSource = Process.SearchData("Emp_Education_get_all", aempid.Value.Trim)
            'End If
            GridVwEducation.DataSource = Process.SearchData("Emp_Education_get_all", aempid.Value.Trim)
            GridVwEducation.AllowSorting = True
            GridVwEducation.AllowPaging = True
            GridVwEducation.DataBind()
        Catch ex As Exception

            Response.Write("Edu: " & ex.Message)
        End Try
    End Sub
#End Region

#Region "SortGrids"
    Protected Sub SortDependants(ByVal sender As Object, ByVal e As GridViewSortEventArgs) Handles GridVwHeaderChckbox.Sorting
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
            Dim table As DataTable = Process.SearchData("Emp_Dependents_get", aempid.Value.Trim)
            table.DefaultView.Sort = sortExpression & direction
            GridVwHeaderChckbox.DataSource = table
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception

        End Try
    End Sub


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
            Dim table As DataTable = Process.SearchData("Emp_languages_get_all", aempid.Value.Trim)
            table.DefaultView.Sort = sortExpression & direction
            GridVwLang.DataSource = table
            GridVwLang.DataBind()
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub SortWorkHistory(ByVal sender As Object, ByVal e As GridViewSortEventArgs) Handles GridVwWorkHistory.Sorting
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
            Dim table As DataTable = Process.SearchData("Emp_Work_History_get_all", aempid.Value.Trim)
            table.DefaultView.Sort = sortExpression & direction
            GridVwWorkHistory.DataSource = table
            GridVwWorkHistory.DataBind()
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub SortAsset(ByVal sender As Object, ByVal e As GridViewSortEventArgs) Handles GridAsset.Sorting
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
            Dim table As DataTable = Process.SearchData("Emp_Asset_get_all", aempid.Value.Trim)
            table.DefaultView.Sort = sortExpression & direction
            GridAsset.DataSource = table
            GridAsset.DataBind()
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub SortHobbies(ByVal sender As Object, ByVal e As GridViewSortEventArgs) Handles GridHobbies.Sorting
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
            Dim table As DataTable = Process.SearchData("Emp_Hobbies_get_all", aempid.Value.Trim)
            table.DefaultView.Sort = sortExpression & direction
            GridHobbies.DataSource = table
            GridHobbies.DataBind()
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
            'Dim table As DataTable = Process.SearchData("Emp_Skills_get_all", aempid.Value.Trim)
            Dim table As DataTable = Process.SearchData("Actual_skills_get", aempid.Value.Trim)
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
            Dim table As DataTable = Process.SearchData("Emp_Certifications_get_all", aempid.Value.Trim)
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
            Dim table As DataTable = Process.SearchData("Emp_Education_get_all", aempid.Value.Trim)
            table.DefaultView.Sort = sortExpression & direction
            GridVwEducation.DataSource = table
            GridVwEducation.DataBind()
        Catch ex As Exception

        End Try
    End Sub
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Read") = False Then
                Process.loadalert(divalert, msgalert, Process.privilegemsg, "warning")

                Exit Sub
            End If


            If Not Me.IsPostBack Then
                If Session("clicked") Is Nothing Then
                    Session("clicked") = "0"
                End If

                'TabContainer1.ActiveTabIndex = 0
                cbouserstat.Items.Clear()
                cbouserstat.Items.Add("Enabled")
                cbouserstat.Items.Add("Disabled")

                Process.LoadRadComboTextAndValue(cbouserrole, "roles_get_all", "Role", "Role", True)

                Process.LoadHTMLSelectTextAndValue(drpnationality, "Nationalities_get_all", "name", "name", True)
                Process.LoadHTMLSelectTextAndValue(drpcountryofbirth, "CountryTable_get", "Country", "Country", True)
                Process.LoadHTMLSelectTextAndValue(drpaddresscountry, "CountryTable_get_all", "Country", "Country", True)
                Process.LoadHTMLSelectTextAndValue(drpemerrelation1, "emp_relationship_get_all", "name", "name", True)
                Process.LoadHTMLSelectTextAndValue(drprelation2, "emp_relationship_get_all", "name", "name", True)



                If Request.QueryString("id") IsNot Nothing Then
                    chklogcreate.Checked = False
                    chklogcreate.Visible = False

                    LoginData(False)
                    Dim EmpID As String = Request.QueryString("id")
                    aempid.Value = EmpID
                    Session("EmpID") = EmpID

                    'Load Personal
                    LoadPersonalDetail(EmpID)
                    LoadContacts(EmpID)
                    LoadEmergencyContact(EmpID)
                    LoadDependants("All")
                    LoadSkills("All")
                    LoadEducation("All")
                    LoadLanguages("All")
                    LoadCertification("All")
                    LoadWorkHistory("All")
                    LoadAsset("All")
                    LoadHobbies("All")
                    divlogin.Visible = False
                Else
                    Dim count As Integer = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.StoredProcedure, "Check_EmployeeID_Format")
                    If count <> 0 Then
                        aempid.Value = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select dbo.Generate_EmpID()")
                    End If
                    'aempid.Value = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select dbo.Generate_EmpID()")
                    txtID.Text = "0"
                    If aempid.Value = "" Then
                        aempid.Value = "0"
                    End If
                    LoadDependants("All")
                    LoadSkills("All")
                    LoadEducation("All")
                    LoadLanguages("All")
                    LoadCertification("All")
                    LoadWorkHistory("All")
                    LoadAsset("All")
                    LoadHobbies("All")
                End If

                If Session("clicked") = 1 Then
                    btnPersonal_Click(sender, e)
                ElseIf Session("clicked") = 2 Then
                    btnEmergency_Click(sender, e)
                ElseIf Session("clicked") = 3 Then
                    btnDependants_Click(sender, e)
                ElseIf Session("clicked") = 4 Then
                    btnQualifications_Click(sender, e)
                ElseIf Session("clicked") = 5 Then
                    btnWorkHistory_Click(sender, e)
                ElseIf Session("clicked") = 6 Then
                    btnAsset_click(sender, e)
                ElseIf Session("clicked") = 7 Then
                    btnHobbies_click(sender, e)
                End If
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub


    Private Sub GridVwSkills_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridVwSkills.PageIndexChanging
        Try
            GridVwSkills.PageIndex = e.NewPageIndex
            GridVwSkills.DataSource = Process.SearchData("Emp_Skills_get_all", aempid.Value.Trim)
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





    Protected Sub GridVwHeaderChckbox_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridVwHeaderChckbox.PageIndexChanging
        Try
            GridVwHeaderChckbox.PageIndex = e.NewPageIndex
            GridVwHeaderChckbox.DataSource = Process.SearchData("Emp_Dependents_get", aempid.Value.Trim)
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub GridVwLang_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridVwLang.PageIndexChanging
        Try
            GridVwLang.PageIndex = e.NewPageIndex
            GridVwLang.DataSource = Process.SearchData("Emp_languages_get_all", aempid.Value.Trim)
            GridVwLang.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btnDelDependents_Click(sender As Object, e As EventArgs) Handles btndeletedependants.Click
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Delete") = False Then
                Process.loadalert(divalert, msgalert, Process.privilegemsg, "warning")
                Exit Sub
            End If
            Dim count As Integer = 0
            Dim confirmValue As String = Request.Form("confirm_value")
            If confirmValue = "Yes" Then
                Dim atLeastOneRowDeleted As Boolean = False
                For Each row As GridViewRow In GridVwHeaderChckbox.Rows
                    Dim cb As CheckBox = row.FindControl("chkEmp")
                    If cb IsNot Nothing AndAlso cb.Checked Then
                        count = count + 1
                        atLeastOneRowDeleted = True
                        Dim ID As String = Convert.ToString(GridVwHeaderChckbox.DataKeys(row.RowIndex).Value)
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Dependents_delete", ID)
                    End If
                Next
                Process.loadalert(divalert, msgalert, count.ToString & " records successfully deleted", "success")
                LoadDependants("All")
            
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub GridVwEducation_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridVwEducation.PageIndexChanging
        Try
            GridVwEducation.PageIndex = e.NewPageIndex
            GridVwEducation.DataSource = Process.SearchData("Emp_Education_get_all", aempid.Value.Trim)
            GridVwEducation.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub GridVwCertification_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridVwCertification.PageIndexChanging
        Try
            GridVwCertification.PageIndex = e.NewPageIndex
            GridVwCertification.DataSource = Process.SearchData("Emp_Certifications_get_all", aempid.Value.Trim)
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
                                  ByVal Photo As String, ByVal workshift As String) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Emp_PersonalDetail_update"
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
            cmd.Parameters.Add("@workshift", SqlDbType.VarChar).Value = workshift
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception
            Process.strExp = ex.Message
            Return 0
        End Try
    End Function

    Protected Sub btnAddPDetail_Click(sender As Object, e As EventArgs)
        Try
            Dim lblstatus As String = ""
            Dim ImgPhotoByte As New Byte

            If txtID.Text <> "0" And txtID.Text.Trim <> "" Then
                If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False Then
                    Process.loadalert(divalert, msgalert, Process.privilegemsg, "warning")
                    Exit Sub
                End If
            End If


            If aempid.Value.Trim = "" Then
                lblstatus = "Assign Employee ID to Employee!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                aempid.Focus()
                Exit Sub
            End If

            If (afirstname.Value.Trim = "") Then
                lblstatus = "First Name required!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                afirstname.Focus()
                Exit Sub
            End If

            If (alastname.Value.Trim = "") Then
                lblstatus = "Last Name required!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                alastname.Focus()
                Exit Sub
            End If

            If drpgender.Value.Trim = "" Then
                lblstatus = "Gender required!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                drpgender.Focus()
                Exit Sub
            End If

            If drpmaritalstatus.Value.Trim = "" Then
                lblstatus = "Marital Status required!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                drpmaritalstatus.Focus()
                Exit Sub
            End If

            If adateofbirth.SelectedDate Is Nothing Then
                lblstatus = "Date of Birth required!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                adateofbirth.Focus()
                Exit Sub
            End If

            If adateresume.SelectedDate Is Nothing Then
                lblstatus = "Date Employee joined required!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                adateresume.Focus()
                Exit Sub
            End If



            If chklogcreate.Checked Then

                If auseremail.Value.Contains(".") = False Or auseremail.Value.Contains("@") = False Then
                    lblstatus = "Create valid Email for Employee!"
                    Process.loadalert(divalert, msgalert, lblstatus, "danger")
                    auseremail.Focus()
                    Exit Sub
                End If
                If ausername.Value.Trim = "" Then
                    lblstatus = "Create User Name for Employee!"
                    Process.loadalert(divalert, msgalert, lblstatus, "danger")
                    ausername.Focus()
                    Exit Sub
                End If

                If ausername.Value.Contains(" ") Then
                    lblstatus = "text space not allowed in Username!"
                    Process.loadalert(divalert, msgalert, lblstatus, "danger")
                    ausername.Focus()
                    Exit Sub
                End If

                If apassword.Value.Trim = "" Then
                    lblstatus = "Create Password for Employee!"
                    Process.loadalert(divalert, msgalert, lblstatus, "danger")
                    apassword.Focus()
                    Exit Sub
                End If


                If cbouserrole.SelectedValue.Contains("Select") Then
                    lblstatus = "Assign a Login Role to Employee!"
                    Process.loadalert(divalert, msgalert, lblstatus, "danger")
                    cbouserrole.Focus()
                    Exit Sub
                End If

                If cbouserstat.SelectedItem.Text.Contains("Select") Then
                    lblstatus = "Set Login Status for Employee!"
                    Process.loadalert(divalert, msgalert, lblstatus, "danger")
                    cbouserstat.Focus()
                    Exit Sub
                End If
            End If


            'Old Data
            If txtID.Text <> "0" And txtID.Text.Trim <> "" Then
                Dim strPersonal As New DataSet
                strPersonal = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_PersonalDetail_get_id", txtID.Text)
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
                olddata(19) = strPersonal.Tables(0).Rows(0).Item("ConfirmationDate").ToString
                olddata(19) = strPersonal.Tables(0).Rows(0).Item("TerminationDate").ToString
            End If

            If aidnumber.Value Is Nothing Then
                aidnumber.Value = ""
            End If
            If astateorigin.Value Is Nothing Then
                astateorigin.Value = ""
            End If
            If aidissuer.Value Is Nothing Then
                aidissuer.Value = ""
            End If


            If aidexpirydate.SelectedDate Is Nothing Then
                aidexpirydate.SelectedDate = CDate("31-DEC-2099")
            End If


            If abirthplace.Value Is Nothing Then
                abirthplace.Value = ""
            End If
            If amiddlename.Value Is Nothing Then
                amiddlename.Value = ""
            End If

            PersonalDetail.EmpID = aempid.Value.Trim
            PersonalDetail.FirstName = afirstname.Value.Trim
            PersonalDetail.MiddleName = amiddlename.Value.Trim
            PersonalDetail.LastName = alastname.Value.Trim
            PersonalDetail.Gender = drpgender.Value
            PersonalDetail.MaritalStatus = drpmaritalstatus.Value
            PersonalDetail.Nationality = drpnationality.Value
            PersonalDetail.DateOfBirth = adateofbirth.SelectedDate
            PersonalDetail.BloodGroup = ""
            PersonalDetail.StateOfOrigin = astateorigin.Value.Trim
            PersonalDetail.IdentityMethod = aidtype.Value.Trim
            PersonalDetail.IDNo = aidnumber.Value.Trim
            PersonalDetail.IDExpiryDate = aidexpirydate.SelectedDate
            PersonalDetail.IDIssuer = aidissuer.Value.Trim
            PersonalDetail.CountryOfBirth = drpcountryofbirth.Value
            PersonalDetail.PlaceOfBirth = abirthplace.Value.Trim
            PersonalDetail.Hobbies = ""
            PersonalDetail.DateJoin = adateresume.SelectedDate


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
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_PersonalDetail_update", PersonalDetail.ID, PersonalDetail.EmpID.ToUpper, PersonalDetail.FirstName, _
                                  PersonalDetail.MiddleName, PersonalDetail.LastName, PersonalDetail.Gender, PersonalDetail.MaritalStatus, PersonalDetail.Nationality, _
                                  PersonalDetail.DateOfBirth, PersonalDetail.BloodGroup, PersonalDetail.StateOfOrigin, PersonalDetail.IdentityMethod, PersonalDetail.IDNo, _
                                  PersonalDetail.IDExpiryDate, PersonalDetail.IDIssuer, PersonalDetail.CountryOfBirth, PersonalDetail.PlaceOfBirth, PersonalDetail.Hobbies, _
                                  PersonalDetail.DateJoin, "", "")
            Else
                txtID.Text = GetEMPIdentity(PersonalDetail.EmpID.ToUpper, PersonalDetail.FirstName, _
                                  PersonalDetail.MiddleName, PersonalDetail.LastName, PersonalDetail.Gender, PersonalDetail.MaritalStatus, PersonalDetail.Nationality, _
                                  PersonalDetail.DateOfBirth, PersonalDetail.BloodGroup, PersonalDetail.StateOfOrigin, PersonalDetail.IdentityMethod, PersonalDetail.IDNo, _
                                  PersonalDetail.IDExpiryDate, PersonalDetail.IDIssuer, PersonalDetail.CountryOfBirth, PersonalDetail.PlaceOfBirth, PersonalDetail.Hobbies, _
                                  PersonalDetail.DateJoin, "", "")

                If txtID.Text = "0" Then
                    Exit Sub
                End If
            End If

            btnAddContact_Click()



            If chklogcreate.Checked Then

                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "users_add", ausername.Value.Trim, alastname.Value & " " & afirstname.Value.Trim, cbouserrole.SelectedValue, cbouserstat.SelectedItem.Text, auseremail.Value.Trim, "Yes", Process.Encrypt(apassword.Value), Session("LoginID"), aempid.Value.Trim)

                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Contact_Info_update", 0, aempid.Value, "", "", _
             "", "", "", "", "", "", auseremail.Value.Trim, "")
                LoadContacts(aempid.Value)

                If aempid.Value.Trim <> "" Then
                    If chkhradmin.Checked Then
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "users_hr_managers_update", aempid.Value.Trim)
                    Else
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "users_hr_managers_delete", aempid.Value.Trim)
                    End If
                End If



                If aempid.Value.Trim <> "" Then
                    If chkfinadmin.Checked Then
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "users_finance_managers_update", aempid.Value.Trim)
                    Else
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "users_finance_managers_delete", aempid.Value.Trim)
                    End If
                End If

                If ausername.Value.Trim <> "" Then
                    If chksuperadmin.Checked Then
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "users_system_admin_update", ausername.Value.Trim)
                    Else
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "users_system_admin_delete", ausername.Value.Trim)
                    End If
                End If



                If Process.User_Notification(auseremail.Value, afirstname.Value, ausername.Value, apassword.Value, Process.ApplicationURL & "/default") = False Then
                    Process.loadalert(divalert, msgalert, Session("exception"), "danger")
                    Exit Sub
                End If

                chklogcreate.Checked = False
                LoginData(False)
                chklogcreate.Visible = False
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

            btnImage_Click()

            'imgphoto.Src = "ImgHandler.ashx?imgid=" & txtID.Text
            lblstatus = "Personal Detail Record saved"
            Process.loadalert(divalert, msgalert, lblstatus, "success")
            chkPay.Visible = True
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnDelLang_Click(sender As Object, e As EventArgs) Handles btndeletelanguage.Click
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Delete") = False Then

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
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Languages_delete", ID)
                    End If
                Next
                LoadLanguages("All")
            Else
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Delete has been cancelled" + "')", True)
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Private Sub btnAddContact_Click()
        Try
            Dim lblstatus As String = ""
            'If (aaddress.Value.Trim = "") Then
            '    lblstatus = "Address required!"
            '    aaddress.Focus()
            '    Exit Sub
            'End If

            'If (drpaddresscountry.Value.Trim = "") Then
            '    lblstatus = "Country of residence required!"
            '    drpaddresscountry.Focus()
            '    Exit Sub
            'End If

            'If aaddresscity.Value.Trim = "" Then
            '    lblstatus = "city required!"
            '    aaddresscity.Focus()
            '    Exit Sub
            'End If

            'If aphonenumber.Value.Trim = "" Then
            '    lblstatus = "phone number required!"
            '    aphonenumber.Focus()
            '    Exit Sub
            'End If


            If txtIDContact.Text.Trim = "" Then
                ContactDetail.ID = 0
            Else
                ContactDetail.ID = txtIDContact.Text
            End If

            If aphonenumber.Value Is Nothing Then
                aphonenumber.Value = ""
            End If

            If aworkphonenumber.Value Is Nothing Then
                aworkphonenumber.Value = ""
            End If
            If aaddress.Value Is Nothing Then
                aaddress.Value = ""
            End If

            If aemailaddress.Value Is Nothing Then
                aemailaddress.Value = ""
            End If
            If aaddresscity.Value Is Nothing Then
                aaddresscity.Value = ""
            End If

            ContactDetail.EmpID = aempid.Value.Trim
            ContactDetail.Address1 = aaddress.Value
            ContactDetail.Address2 = ""
            ContactDetail.PostalAddress = aaddress.Value
            ContactDetail.PersonalEmail = aemailaddress.Value
            ContactDetail.City = aaddresscity.Value
            ContactDetail.Country = drpaddresscountry.Value
            ContactDetail.HomePhone = ""
            ContactDetail.MobileNo = aphonenumber.Value
            ContactDetail.WorkPhone = aworkphonenumber.Value
            ContactDetail.WorkEmail = aworkemailaddress.Value


            If txtIDContact.Text.Trim <> "" And txtIDContact.Text.Trim <> "0" Then
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Contact_Info_update", ContactDetail.ID, ContactDetail.EmpID, ContactDetail.Address1, ContactDetail.Address2, _
            ContactDetail.City, ContactDetail.Country, ContactDetail.PostalAddress, ContactDetail.MobileNo, ContactDetail.HomePhone, ContactDetail.PersonalEmail, ContactDetail.WorkEmail,
            ContactDetail.WorkPhone)
            Else
                txtIDContact.Text = GetContactIdentity(ContactDetail.EmpID, ContactDetail.Address1, ContactDetail.Address2, _
            ContactDetail.City, ContactDetail.Country, ContactDetail.PostalAddress, ContactDetail.MobileNo, ContactDetail.HomePhone, ContactDetail.PersonalEmail, ContactDetail.WorkEmail,
            ContactDetail.WorkPhone)
            End If



        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try

    End Sub
    Private Function GetEmergencyIdentity(ByVal empid As String, ByVal name1 As String, ByVal address1 As String, _
                                 ByVal phone1 As String, ByVal relationship1 As String, ByVal name2 As String, _
                                  ByVal address2 As String, ByVal phone2 As String, ByVal relationship2 As String, _
                                  ByVal RefereeName1 As String, ByVal RefereeAddress1 As String, ByVal RefereePhone1 As String, _
                                 ByVal RefereeEmail1 As String, ByVal RefereePostion1 As String, ByVal RefereeYears1 As Integer, _
                                  ByVal RefereeName2 As String, ByVal RefereeAddress2 As String, ByVal RefereePhone2 As String, _
                                  ByVal RefereeEmail2 As String, ByVal RefereePostion2 As String, ByVal RefereeYears2 As Integer, confirm1 As String, confirm2 As String) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Emp_Emergency_Contact_Update"
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
    Private Function GetContactIdentity(ByVal empid As String, ByVal address1 As String, ByVal address2 As String, _
                                ByVal City As String, ByVal Country As String, _
                                 ByVal PostalAddress As String, ByVal MobileNo As String, ByVal HomePhone As String, _
                                ByVal PersonalEMail As String, ByVal WorkEmail As String, ByVal WorkPhone As String) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Emp_Contact_Info_update"
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

    Protected Sub btnSaveEmergency_Click(sender As Object, e As EventArgs)
        Try
            Dim lblstatus As String = ""
            If txtIDEmergency.Text <> "0" Or txtIDEmergency.Text.Trim <> "" Then
                If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False Then
                    Exit Sub
                End If
            End If

            If aempid.Value.Trim = "" Then
                lblstatus = "Employee ID required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                aempid.Focus()
                Exit Sub
            End If

            If (aemername1.Value.Trim = "") Then
                lblstatus = "emergency contact name required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                aemername1.Focus()
                Exit Sub
            End If

            If aemeraddress1.Value.Trim = "" Then
                lblstatus = "emergency contact address required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                aemeraddress1.Focus()
                Exit Sub
            End If

            If aemercontactnumber1.Value.Trim = "" Then
                lblstatus = "emergency contact number required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                aemercontactnumber1.Focus()
                Exit Sub
            End If

            If drpemerrelation1.Value.Trim = "" Then
                lblstatus = "relationship with employee required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                drpemerrelation1.Focus()
                Exit Sub
            End If

            If aemeraddress2.Value.Trim <> "" Then
                If aemercontactnumber2.Value.Trim = "" Then
                    lblstatus = "Emergency Contact 2 phone number required!"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    aemercontactnumber2.Focus()
                    Exit Sub
                End If

                If aemeraddress2.Value.Trim Is Nothing Then
                    lblstatus = "Contact 2 Address required!"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    aemeraddress2.Focus()
                    Exit Sub
                End If

                If drprelation2.Value.Trim = "" Then
                    lblstatus = "contact 2 relationship required!"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    drprelation2.Focus()
                    Exit Sub
                End If

            End If

            If arefname1.Value.Trim IsNot Nothing Then
                If areforganisation1.Value.Trim Is Nothing Then
                    lblstatus = "Referee 1 Address required!"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    areforganisation1.Focus()
                    Exit Sub
                End If

                If arefphonenumber1.Value.Trim Is Nothing Then
                    lblstatus = "Referee 1 phone required!"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    arefphonenumber1.Focus()
                    Exit Sub
                End If

                If arefyrsknown1.Value.Trim Is Nothing And IsNumeric(arefyrsknown1.Value) = False Then
                    lblstatus = "Referee 1 years known required!"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    arefyrsknown1.Focus()
                    Exit Sub
                End If
            End If

            If arefname2.Value.Trim <> "" Then
                If areforganisation2.Value.Trim = "" Then
                    lblstatus = "Referee 2 Address required!"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    areforganisation2.Focus()
                    Exit Sub
                End If

                If arefcontactnumber2.Value.Trim Is Nothing Then
                    lblstatus = "Referee 2 phone required!"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    arefcontactnumber2.Focus()
                    Exit Sub
                End If

                If arefyears2.Value.Trim Is Nothing And IsNumeric(arefyrsknown1.Value) = False Then
                    lblstatus = "Referee 2 years known required!"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    arefyears2.Focus()
                    Exit Sub
                End If
            End If



            If aemeraddress2.Value Is Nothing Then
                aemeraddress2.Value = ""
            End If

            If aemeraddress1.Value Is Nothing Then
                aemeraddress1.Value = ""
            End If

            If aemercontactnumber2.Value Is Nothing Then
                aemercontactnumber2.Value = ""
            End If


            If txtIDEmergency.Text.Trim = "" Then
                EmergencyDetail.ID = 0
            Else
                EmergencyDetail.ID = txtIDEmergency.Text
            End If
            EmergencyDetail.EmpID = aempid.Value
            EmergencyDetail.Name1 = aemername1.Value
            EmergencyDetail.Address1 = aemeraddress1.Value
            EmergencyDetail.Phone1 = aemercontactnumber1.Value
            EmergencyDetail.RelationShip1 = drpemerrelation1.Value
            EmergencyDetail.Name2 = aemeraddress1.Value
            EmergencyDetail.Address2 = aemeraddress2.Value
            EmergencyDetail.Phone2 = aemercontactnumber2.Value
            EmergencyDetail.RelationShip2 = drprelation2.Value

            If arefyrsknown1.Value.Trim = "" Then
                arefyrsknown1.Value = "0"
            End If

            If arefyears2.Value.Trim = "" Then
                arefyears2.Value = "0"
            End If

            Dim OldValue As String = ""
            Dim NewValue As String = ""

            Dim j As Integer = 0


            If txtIDEmergency.Text.Trim <> "" And txtIDEmergency.Text.Trim <> "0" Then
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Emergency_Contact_Update", EmergencyDetail.ID, EmergencyDetail.EmpID, EmergencyDetail.Name1, EmergencyDetail.Address1, _
                                      EmergencyDetail.Phone1, EmergencyDetail.RelationShip1, EmergencyDetail.Name2, EmergencyDetail.Address2, _
                                      EmergencyDetail.Phone2, EmergencyDetail.RelationShip2, arefname1.Value.Trim, areforganisation1.Value.Trim, arefphonenumber1.Value.Trim, arefemailaddress1.Value.Trim, _
                                      "", arefyrsknown1.Value, arefname2.Value.Trim, areforganisation2.Value.Trim, arefcontactnumber2.Value.Trim, arefemailaddress2.Value.Trim, _
                                      "", arefyears2.Value, drprefconfirmed1.Value, drprefconfirmed2.Value)
            Else
                txtIDEmergency.Text = GetEmergencyIdentity(EmergencyDetail.EmpID, EmergencyDetail.Name1, EmergencyDetail.Address1, _
                                      EmergencyDetail.Phone1, EmergencyDetail.RelationShip1, EmergencyDetail.Name2, EmergencyDetail.Address2, _
                                      EmergencyDetail.Phone2, EmergencyDetail.RelationShip2, arefname1.Value.Trim, areforganisation1.Value.Trim, arefphonenumber1.Value.Trim, arefemailaddress1.Value.Trim, _
                                      "", arefyrsknown1.Value, arefname2.Value.Trim, areforganisation2.Value.Trim, arefcontactnumber2.Value.Trim, arefemailaddress2.Value.Trim, _
                                      "", arefyears2.Value, drprefconfirmed1.Value, drprefconfirmed2.Value)

                If txtIDEmergency.Text = "0" Then
                    Exit Sub
                End If
            End If

            lblstatus = "Emergency contact record saved!"
            Process.loadalert(divalert, msgalert, lblstatus, "success")

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
            'lblstatus.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnAddDependents_Click(sender As Object, e As EventArgs)
        Try
            Session("EmpID") = aempid.Value.Trim
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                Process.loadalert(divalert, msgalert, Process.privilegemsg, "warning")

                Exit Sub
            End If
            OpenDependant("")

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try

    End Sub

    Protected Sub btnAddLang_Click(sender As Object, e As EventArgs)
        Try
            Session("EmpID") = aempid.Value.Trim
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                Process.loadalert(divalert, msgalert, Process.privilegemsg, "warning")

                Exit Sub
            End If
            OpenLanguage("")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Protected Sub btnDelSkill_Click(sender As Object, e As EventArgs) Handles btndeleteskills.Click
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Delete") = False Then
                Process.loadalert(divalert, msgalert, Process.privilegemsg, "warning")

                Exit Sub
            End If
            Dim count As Integer = 0
            Dim confirmValue As String = Request.Form("confirm_value")

            If confirmValue = "Yes" Then
                Dim atLeastOneRowDeleted As Boolean = False
                For Each row As GridViewRow In GridVwSkills.Rows
                    Dim cb As CheckBox = row.FindControl("chkEmp1")
                    If cb IsNot Nothing AndAlso cb.Checked Then
                        count = count + 1
                        atLeastOneRowDeleted = True
                        Dim ID As String = Convert.ToString(GridVwSkills.DataKeys(row.RowIndex).Value)
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Skills_delete", ID)
                    End If
                Next
                Process.loadalert(divalert, msgalert, count.ToString & " records successfully deleted", "success")
                LoadSkills("All")

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnAddSkill_Click(sender As Object, e As EventArgs)
        Try
            Session("EmpID") = aempid.Value.Trim
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                Process.loadalert(divalert, msgalert, Process.privilegemsg, "warning")

                Exit Sub
            End If

            OpenSkill("")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnAddEducation_Click(sender As Object, e As EventArgs)
        Try
            Session("EmpID") = aempid.Value.Trim
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                Process.loadalert(divalert, msgalert, Process.privilegemsg, "warning")

                Exit Sub
            End If
            OpenEducation(Session("EmpID"))
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Protected Sub btnDelEducation_Click(sender As Object, e As EventArgs) Handles btndeleteeducation.Click
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Delete") = False Then
                Process.loadalert(divalert, msgalert, Process.privilegemsg, "warning")

                Exit Sub
            End If
            Dim count As Integer = 0
            Dim confirmValue As String = Request.Form("confirm_value")

            If confirmValue = "Yes" Then
                Dim atLeastOneRowDeleted As Boolean = False
                For Each row As GridViewRow In GridVwEducation.Rows
                    Dim cb As CheckBox = row.FindControl("chkEmpEdu")
                    If cb IsNot Nothing AndAlso cb.Checked Then
                        count = count + 1
                        atLeastOneRowDeleted = True
                        Dim ID As String = Convert.ToString(GridVwEducation.DataKeys(row.RowIndex).Value)
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Education_delete", ID)
                    End If
                Next
                Process.loadalert(divalert, msgalert, count.ToString & " records successfully deleted", "success")
                LoadEducation("All")
           
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Protected Sub btnAddCert_Click(sender As Object, e As EventArgs)
        'EmployeeCertification
        Try
            Session("EmpID") = aempid.Value.Trim
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                Process.loadalert(divalert, msgalert, Process.privilegemsg, "warning")

                Exit Sub
            End If

            OpenCertificate(Session("EmpID"))
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnDelCert_Click(sender As Object, e As EventArgs) Handles btndeletecertificate.Click
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Delete") = False Then
                Process.loadalert(divalert, msgalert, Process.privilegemsg, "warning")

                Exit Sub
            End If
            Dim count As Integer = 0
            Dim confirmValue As String = Request.Form("confirm_value")

            If confirmValue = "Yes" Then
                Dim atLeastOneRowDeleted As Boolean = False
                For Each row As GridViewRow In GridVwCertification.Rows
                    Dim cb As CheckBox = row.FindControl("chkEmp3")
                    If cb IsNot Nothing AndAlso cb.Checked Then
                        count = count + 1
                        atLeastOneRowDeleted = True
                        Dim ID As String = Convert.ToString(GridVwCertification.DataKeys(row.RowIndex).Value)
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Certifications_delete", ID)
                    End If
                Next
                Process.loadalert(divalert, msgalert, count.ToString & " records successfully deleted", "success")
                LoadCertification("All")
            
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnAddWork_Click(sender As Object, e As EventArgs)
        Try
            Session("EmpID") = aempid.Value.Trim
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                Process.loadalert(divalert, msgalert, Process.privilegemsg, "warning")

                Exit Sub
            End If
            OpenWorkHistory("")

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub btnAddAsset_Click(sender As Object, e As EventArgs)
        Try
            Session("EmpID") = aempid.Value.Trim
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                Process.loadalert(divalert, msgalert, Process.privilegemsg, "warning")

                Exit Sub
            End If
            OpenAsset("")

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub btnAddHobbies_Click(sender As Object, e As EventArgs)
        Try
            Session("EmpID") = aempid.Value.Trim
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                Process.loadalert(divalert, msgalert, Process.privilegemsg, "warning")

                Exit Sub
            End If
            OpenHobbies("")

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub



    Protected Sub btnDelWork_Click(sender As Object, e As EventArgs) Handles btndeletecareer.Click
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Delete") = False Then

                Exit Sub
            End If
            Dim confirmValue As String = Request.Form("confirm_value")

            If confirmValue = "Yes" Then
                Dim atLeastOneRowDeleted As Boolean = False
                For Each row As GridViewRow In GridVwWorkHistory.Rows
                    Dim cb As CheckBox = row.FindControl("chkEmp4")
                    If cb IsNot Nothing AndAlso cb.Checked Then
                        atLeastOneRowDeleted = True
                        Dim ID As String = Convert.ToString(GridVwWorkHistory.DataKeys(row.RowIndex).Value)
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Work_History_delete", ID)
                    End If
                Next
                LoadWorkHistory("All")
            Else
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Delete has been cancelled" + "')", True)
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
            cmd.CommandText = "Emp_PersonalDetail_update_PhotoImage"
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
    Private Function btnImage_Click() As Boolean
        Try

            'SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_PersonalDetail_update_photo", aempid.Value, Path)
            If Not imgUpload.PostedFile Is Nothing Then
                Dim img_strm As Stream = imgUpload.PostedFile.InputStream
                'Retrieving the length of the file to upload
                Dim img_len As Integer = imgUpload.PostedFile.ContentLength
                'retrieving the type of the file to upload
                Dim strtype As String = imgUpload.PostedFile.ContentType.ToString()
                Dim strname As String = Path.GetFileName(imgUpload.PostedFile.FileName)
                Dim imgdata As Byte() = New Byte(img_len - 1) {}
                Dim n As Integer = img_strm.Read(imgdata, 0, img_len)
                Dim imageName As String = (Server.MapPath(emailFile) + "EmployeePhoto_" & aempid.Value.Trim & ".jpg")
                If File.Exists(imageName) Then
                    File.Delete(imageName)
                End If
                Using Stream As New FileStream(imageName, FileMode.Create)
                    Stream.Write(imgdata, 0, imgdata.Length)
                End Using
                imageName = emailFile + "EmployeePhoto_" & aempid.Value.Trim & ".jpg"

                If txtID.Text <> "0" And imgdata.Length <> 0 Then
                    'txtID.Text = GetPhotoIdentity(aempid.Value.Trim, imgdata, strtype)

                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_PersonalDetail_update_PhotoImage", aempid.Value.Trim, Nothing, imageName)
                End If
                'imgphoto.Src = "ImgHandler.ashx?imgid=" & txtID.Text
                imgphoto.Src = imageName

            End If
            Return True
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
            Return False
        End Try
    End Function

    Protected Sub btnImage0_Click(sender As Object, e As EventArgs)
        Try
            'imgProfile.ImageUrl = imgClear.ImageUrl
            If txtID.Text <> "0" Then
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_PersonalDetail_update_PhotoImage", aempid.Value.Trim, Nothing, "")
            End If

        Catch ex As Exception

        End Try
    End Sub
    Private Sub LoginData(Visibility As Boolean)
        Try
            'UserInfo.Visible = Visibility
            'UserTitle.Visible = Visibility
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub chkLogin_CheckedChanged(sender As Object, e As EventArgs) Handles chklogcreate.CheckedChanged
        If chklogcreate.Checked Then
            divlogin.Visible = True

        Else
            divlogin.Visible = False
        End If
    End Sub






    Protected Sub chkPay_CheckedChanged(sender As Object, e As EventArgs) Handles chkPay.CheckedChanged
        Try
            Dim lblstatus As String = ""
            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_PersonalDetail_UpdatePay", txtID.Text, chkPay.Checked)
            If chkPay.Checked = True Then
                lblstatus = "Employee's pay is now suspended"
                Process.loadalert(divalert, msgalert, lblstatus, "success")
            Else
                lblstatus = "Suspension lefted off Employee Pay"
                Process.loadalert(divalert, msgalert, lblstatus, "success")
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try

    End Sub

    Protected Sub btnRegenerate_Click(sender As Object, e As EventArgs)
        Try

            aempid.Value = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select dbo.Generate_EmpID()")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub DownloadCertificate(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim sid As String = CType(sender, LinkButton).CommandArgument
            Dim dt As DataTable = Process.SearchData("Emp_Certifications_get", sid)
            If dt IsNot Nothing Then
                'downloadFile(CType(dt.Rows(0)("fileimage"), Byte()), dt.Rows(0)("filetype").ToString(), dt.Rows(0)("filename").ToString())
            End If
            Dim fileName As String = dt.Rows(0)("filename").ToString()
            Dim filePath As String = Server.MapPath(emailFile & fileName)
            Response.ContentType = ContentType
            Response.AppendHeader("Content-Disposition", ("attachment; filename=" + Path.GetFileName(filePath)))
            Response.WriteFile(filePath)
            Response.End()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub DownloadEducation(ByVal sender As Object, ByVal e As EventArgs)
        Try
            '  lblstatus.Text = ""
            Dim sid As String = CType(sender, LinkButton).CommandArgument
            Dim dt As DataTable = Process.SearchData("Emp_Education_get", sid)
            If dt IsNot Nothing Then
                'downloadFile(CType(dt.Rows(0)("fileimage"), Byte()), dt.Rows(0)("filetype").ToString(), dt.Rows(0)("filename").ToString())
            End If
            Dim fileName As String = dt.Rows(0)("filename").ToString()
            Dim filePath As String = Server.MapPath(emailFile & fileName)
            Response.ContentType = ContentType
            Response.AppendHeader("Content-Disposition", ("attachment; filename=" + Path.GetFileName(filePath)))
            Response.WriteFile(filePath)
            Response.End()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
            ' lblstatus.Text = ex.Message
        End Try
    End Sub
    Protected Sub downloadFile(ByVal bytefile As Byte(), ByVal filetype As String, ByVal filename As String)
        Dim bytes() As Byte = bytefile
        Response.Buffer = True
        Response.Charset = ""
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = filetype
        Response.AddHeader("content-disposition", "attachment;filename=" & filename)
        Response.BinaryWrite(bytes)
        Response.Flush()
        Response.End()
    End Sub

    Protected Sub btnPersonal_Click(sender As Object, e As EventArgs)
        Try
            MultiView1.ActiveViewIndex = 0
            Session("clicked") = MultiView1.ActiveViewIndex + 1

            btpersonal.Attributes.Remove("class")
            'btpersonalcontact.Attributes.Remove("class")
            btemercontact.Attributes.Remove("class")
            btqualification.Attributes.Remove("class")
            btdependants.Attributes.Remove("class")
            btcareer.Attributes.Remove("class")
            btasset.Attributes.Remove("class")
            bthobbies.Attributes.Remove("class")

            btpersonal.Attributes.Add("class", "btn btn-success")
            'btpersonalcontact.Attributes.Add("class", "btn btn-default")
            btemercontact.Attributes.Add("class", "btn btn-default")
            btqualification.Attributes.Add("class", "btn btn-default")
            btdependants.Attributes.Add("class", "btn btn-default")
            btcareer.Attributes.Add("class", "btn btn-default")
            btasset.Attributes.Add("class", "btn btn-default")
            bthobbies.Attributes.Add("class", "btn btn-default")
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btnContactDetail_Click(sender As Object, e As EventArgs)
        Try
            MultiView1.ActiveViewIndex = 1
            Session("clicked") = MultiView1.ActiveViewIndex + 1

            btpersonal.Attributes.Remove("class")
            'btpersonalcontact.Attributes.Remove("class")
            btemercontact.Attributes.Remove("class")
            btqualification.Attributes.Remove("class")
            btdependants.Attributes.Remove("class")
            btcareer.Attributes.Remove("class")
            btasset.Attributes.Remove("class")
            bthobbies.Attributes.Remove("class")

            btpersonal.Attributes.Add("class", "btn btn-default")
            'btpersonalcontact.Attributes.Add("class", "btn btn-success")
            btemercontact.Attributes.Add("class", "btn btn-default")
            btqualification.Attributes.Add("class", "btn btn-default")
            btdependants.Attributes.Add("class", "btn btn-default")
            btcareer.Attributes.Add("class", "btn btn-default")
            btasset.Attributes.Add("class", "btn btn-default")
            bthobbies.Attributes.Add("class", "btn btn-default")
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnEmergency_Click(sender As Object, e As EventArgs)
        Try
            MultiView1.ActiveViewIndex = 1
            Session("clicked") = MultiView1.ActiveViewIndex + 1

            btpersonal.Attributes.Remove("class")
            '.Attributes.Remove("class")
            btemercontact.Attributes.Remove("class")
            btqualification.Attributes.Remove("class")
            btdependants.Attributes.Remove("class")
            btcareer.Attributes.Remove("class")
            btasset.Attributes.Remove("class")
            bthobbies.Attributes.Remove("class")

            btpersonal.Attributes.Add("class", "btn btn-default")
            'btpersonalcontact.Attributes.Add("class", "btn btn-default")
            btemercontact.Attributes.Add("class", "btn btn-success")
            btqualification.Attributes.Add("class", "btn btn-default")
            btdependants.Attributes.Add("class", "btn btn-default")
            btcareer.Attributes.Add("class", "btn btn-default")
            btasset.Attributes.Add("class", "btn btn-default")
            bthobbies.Attributes.Add("class", "btn btn-default")
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnDependants_Click(sender As Object, e As EventArgs)
        Try
            MultiView1.ActiveViewIndex = 2
            Session("clicked") = MultiView1.ActiveViewIndex + 1

            btpersonal.Attributes.Remove("class")
            'btpersonalcontact.Attributes.Remove("class")
            btemercontact.Attributes.Remove("class")
            btqualification.Attributes.Remove("class")
            btdependants.Attributes.Remove("class")
            btcareer.Attributes.Remove("class")
            btasset.Attributes.Remove("class")
            bthobbies.Attributes.Remove("class")

            btpersonal.Attributes.Add("class", "btn btn-default")
            'btpersonalcontact.Attributes.Add("class", "btn btn-default")
            btemercontact.Attributes.Add("class", "btn btn-default")
            btqualification.Attributes.Add("class", "btn btn-default")
            btdependants.Attributes.Add("class", "btn btn-success")
            btcareer.Attributes.Add("class", "btn btn-default")
            btasset.Attributes.Add("class", "btn btn-default")
            bthobbies.Attributes.Add("class", "btn btn-default")
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnQualifications_Click(sender As Object, e As EventArgs)
        Try
            MultiView1.ActiveViewIndex = 3
            Session("clicked") = MultiView1.ActiveViewIndex + 1

            btpersonal.Attributes.Remove("class")
            'btpersonalcontact.Attributes.Remove("class")
            btemercontact.Attributes.Remove("class")
            btqualification.Attributes.Remove("class")
            btdependants.Attributes.Remove("class")
            btcareer.Attributes.Remove("class")
            btasset.Attributes.Remove("class")
            bthobbies.Attributes.Remove("class")

            btpersonal.Attributes.Add("class", "btn btn-default")
            'btpersonalcontact.Attributes.Add("class", "btn btn-default")
            btemercontact.Attributes.Add("class", "btn btn-default")
            btqualification.Attributes.Add("class", "btn btn-success")
            btdependants.Attributes.Add("class", "btn btn-default")
            btcareer.Attributes.Add("class", "btn btn-default")
            btasset.Attributes.Add("class", "btn btn-default")
            bthobbies.Attributes.Add("class", "btn btn-default")
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnWorkHistory_Click(sender As Object, e As EventArgs)
        Try

            MultiView1.ActiveViewIndex = 4
            Session("clicked") = MultiView1.ActiveViewIndex + 1

            btpersonal.Attributes.Remove("class")
            'btpersonalcontact.Attributes.Remove("class")
            btemercontact.Attributes.Remove("class")
            btqualification.Attributes.Remove("class")
            btdependants.Attributes.Remove("class")
            btcareer.Attributes.Remove("class")
            btasset.Attributes.Remove("class")
            bthobbies.Attributes.Remove("class")

            btpersonal.Attributes.Add("class", "btn btn-default")
            'btpersonalcontact.Attributes.Add("class", "btn btn-default")
            btemercontact.Attributes.Add("class", "btn btn-default")
            btqualification.Attributes.Add("class", "btn btn-default")
            btdependants.Attributes.Add("class", "btn btn-default")
            btcareer.Attributes.Add("class", "btn btn-success")
            btasset.Attributes.Add("class", "btn btn-default")
            bthobbies.Attributes.Add("class", "btn btn-default")
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub btnAsset_click(sender As Object, e As EventArgs)
        Try

            MultiView1.ActiveViewIndex = 5
            Session("clicked") = MultiView1.ActiveViewIndex + 1

            btpersonal.Attributes.Remove("class")
            'btpersonalcontact.Attributes.Remove("class")
            btemercontact.Attributes.Remove("class")
            btqualification.Attributes.Remove("class")
            btdependants.Attributes.Remove("class")
            btcareer.Attributes.Remove("class")
            btasset.Attributes.Remove("class")
            bthobbies.Attributes.Remove("class")

            btpersonal.Attributes.Add("class", "btn btn-default")
            'btpersonalcontact.Attributes.Add("class", "btn btn-default")
            btemercontact.Attributes.Add("class", "btn btn-default")
            btqualification.Attributes.Add("class", "btn btn-default")
            btdependants.Attributes.Add("class", "btn btn-default")
            btcareer.Attributes.Add("class", "btn btn-default")
            btasset.Attributes.Add("class", "btn btn-success")
            bthobbies.Attributes.Add("class", "btn btn-default")
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub btnHobbies_click(sender As Object, e As EventArgs)
        Try

            MultiView1.ActiveViewIndex = 6
            Session("clicked") = MultiView1.ActiveViewIndex + 1

            btpersonal.Attributes.Remove("class")
            'btpersonalcontact.Attributes.Remove("class")
            btemercontact.Attributes.Remove("class")
            btqualification.Attributes.Remove("class")
            btdependants.Attributes.Remove("class")
            btcareer.Attributes.Remove("class")
            btasset.Attributes.Remove("class")
            bthobbies.Attributes.Remove("class")

            btpersonal.Attributes.Add("class", "btn btn-default")
            'btpersonalcontact.Attributes.Add("class", "btn btn-default")
            btemercontact.Attributes.Add("class", "btn btn-default")
            btqualification.Attributes.Add("class", "btn btn-default")
            btdependants.Attributes.Add("class", "btn btn-default")
            btcareer.Attributes.Add("class", "btn btn-default")
            btasset.Attributes.Add("class", "btn btn-default")
            bthobbies.Attributes.Add("class", "btn btn-success")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub OpenDependant(sid As String)
        Try
            Dim url As String = ""
            If sid = "" Then
                url = "EmployeeDependant.aspx?empid=" & aempid.Value
            Else
                url = "EmployeeDependant.aspx?Id1=" & sid
            End If
            Response.Redirect(url, True)
            'Dim s As String = "window.open('" & url + "', 'popup_window', 'width=700,height=500,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no');"
            'ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Sub OpenCertificate(sid As String)
        Try
            Dim url As String = ""
            If sid = "" Then
                url = "EmployeeCertification"
            Else
                url = "EmployeeCertification?Id1=" & sid
            End If
            Response.Redirect(url, True)
            'Dim s As String = "window.open('" & url + "', 'popup_window', 'width=700,height=500,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no');"
            'ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Sub OpenEducation(sid As String)
        Try
            Dim url As String = ""
            If sid = "" Then
                url = "EmployeeEducation"
            Else
                url = "EmployeeEducation?Id1=" & sid
            End If
            Response.Redirect(url, True)
            'Dim s As String = "window.open('" & url + "', 'popup_window', 'width=700,height=500,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no');"
            'ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Sub OpenLanguage(sid As String)
        Try
            Dim url As String = ""
            If sid = "" Then
                url = "EmployeeLanguage"
            Else
                url = "EmployeeLanguage?Id1=" & sid
            End If
            Response.Redirect(url, True)
            'Dim s As String = "window.open('" & url + "', 'popup_window', 'width=700,height=500,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no');"
            'ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Sub OpenSkill(sid As String)
        Try
            Dim url As String = ""
            If sid = "" Then
                url = "EmployeeSkills"
            Else
                url = "EmployeeSkills?Id1=" & sid
            End If
            Response.Redirect(url, True)
            'Dim s As String = "window.open('" & url + "', 'popup_window', 'width=700,height=500,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no');"
            'ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Sub OpenWorkHistory(sid As String)
        Try
            Dim url As String = ""
            If sid = "" Then
                url = "EmployeeWorkHistory"
            Else
                url = "EmployeeWorkHistory?Id1=" & sid
            End If
            Response.Redirect(url, True)
            'Dim s As String = "window.open('" & url + "', 'popup_window', 'width=700,height=500,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no');"
            'ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Sub OpenAsset(sid As String)
        Try
            Dim url As String = ""
            If sid = "" Then
                url = "EmployeeAsset"
            Else
                url = "EmployeeAsset?Id1=" & sid
            End If
            Response.Redirect(url, True)
            'Dim s As String = "window.open('" & url + "', 'popup_window', 'width=700,height=500,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no');"
            'ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Sub OpenHobbies(sid As String)
        Try
            Dim url As String = ""
            If sid = "" Then
                url = "EmployeeHobbies"
            Else
                url = "EmployeeHobbies?Id1=" & sid
            End If
            Response.Redirect(url, True)
            'Dim s As String = "window.open('" & url + "', 'popup_window', 'width=700,height=500,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no');"
            'ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub GridVwHeaderChckbox_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridVwHeaderChckbox.RowCommand
        If (e.CommandName = "AddDependant") Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            OpenDependant(index)
        End If
    End Sub
    Private Sub GridVwCertification_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridVwCertification.RowCommand
        Try
            If (e.CommandName = "AddCertificate") Then
                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                OpenCertificate(index)
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub GridVwEducation_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridVwEducation.RowCommand

        Try
            If (e.CommandName = "AddEducation") Then
                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                OpenEducation(index)
            End If
        Catch ex As Exception

        End Try

    End Sub
    Private Sub GridVwLang_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridVwLang.RowCommand
        Try
            If (e.CommandName = "AddLanguage") Then
                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                OpenLanguage(index)
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub GridVwSkills_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridVwSkills.RowCommand
        Try
            If (e.CommandName = "AddSkill") Then
                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                OpenSkill(index)
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub GridVwWorkHistory_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridVwWorkHistory.RowCommand
        Try
            If (e.CommandName = "AddWorkHistory") Then
                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                OpenWorkHistory(index)
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub GridAsset_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridAsset.RowCommand
        Try
            If (e.CommandName = "AddAsset") Then
                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                OpenAsset(index)
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub GridHobbies_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridHobbies.RowCommand
        Try
            If (e.CommandName = "AddHobbies") Then
                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                OpenHobbies(index)
            End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/Module/Employee/Employees.aspx", True)
    End Sub
    Protected Sub btnCancel1_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/Module/Employee/Employees.aspx", True)
    End Sub
    Protected Sub Approve(sender As Object, e As EventArgs) Handles lnkApprove.Click
        Try
            Dim confirmValue As String = Request.Form("confirmapprove_value")
            If confirmValue = "Yes" Then
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Emergency_Contact_Changes_Approve", Request.QueryString("id"), Session("UserEmpID"))
                Process.loadalert(divalert, msgalert, "Information approved", "success")
                LoadEmergencyContact(Request.QueryString("id"))
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub
    Protected Sub CancelChange(sender As Object, e As EventArgs) Handles lnkCancel.Click
        Try
            Dim confirmValue As String = Request.Form("confirmcancel_value")
            If confirmValue = "Yes" Then
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Emergency_Contact_Changes_Cancel", Request.QueryString("id"), Session("UserEmpID"))
                Process.loadalert(divalert, msgalert, "Information update cancelled", "info")
                LoadEmergencyContact(Request.QueryString("id"))
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub
End Class