Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports System.IO


Public Class CandidateProfile
    Inherits System.Web.UI.Page
    Dim emailFile As String = ConfigurationManager.AppSettings("FileURL")
    Private Sub LoadProfile(ByVal id As String)
        Dim strUser As New DataSet
        strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Job_Applicant_Profile", id)
        If strUser.Tables(0).Rows.Count > 0 Then
            txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
            'lblimgtype.Text = strUser.Tables(0).Rows(0).Item("imgtype").ToString

            aaddress.Value = strUser.Tables(0).Rows(0).Item("ResidentAddress").ToString
            acity.Value = strUser.Tables(0).Rows(0).Item("City").ToString
            aemailadd.Value = strUser.Tables(0).Rows(0).Item("EmailAddress").ToString
            arefemail1.Value = strUser.Tables(0).Rows(0).Item("RefereeEmail1").ToString
            arefemail2.Value = strUser.Tables(0).Rows(0).Item("RefereeEmail2").ToString            
            alastname.Value = strUser.Tables(0).Rows(0).Item("LastName").ToString
            aothername.Value = strUser.Tables(0).Rows(0).Item("FirstName").ToString + " " + strUser.Tables(0).Rows(0).Item("MiddleName").ToString
            aphonenumber.Value = strUser.Tables(0).Rows(0).Item("MobileNo").ToString
            arefaddr1.Value = strUser.Tables(0).Rows(0).Item("RefereeAddress1").ToString
            arefaddr2.Value = strUser.Tables(0).Rows(0).Item("RefereeAddress2").ToString
            arefphone1.Value = strUser.Tables(0).Rows(0).Item("RefereePhone1").ToString
            arefphone2.Value = strUser.Tables(0).Rows(0).Item("RefereePhone2").ToString
            arefposition1.Value = strUser.Tables(0).Rows(0).Item("RefereePostion1").ToString
            arefposition2.Value = strUser.Tables(0).Rows(0).Item("RefereePostion2").ToString
            arefname1.Value = strUser.Tables(0).Rows(0).Item("RefereeName1").ToString
            arefname2.Value = strUser.Tables(0).Rows(0).Item("RefereeName2").ToString
            astate.Value = strUser.Tables(0).Rows(0).Item("state").ToString
            aexpyears.Value = strUser.Tables(0).Rows(0).Item("Experience").ToString
            arefyears1.Value = strUser.Tables(0).Rows(0).Item("RefereeYears1").ToString
            arefyears2.Value = strUser.Tables(0).Rows(0).Item("RefereeYears2").ToString

            btcertificate.InnerText = strUser.Tables(0).Rows(0).Item("certname").ToString
            btcoverletter.InnerText = strUser.Tables(0).Rows(0).Item("coverlettername").ToString
            btresume.InnerText = strUser.Tables(0).Rows(0).Item("cvname").ToString
            Process.AssignRadComboValue(cbocountry, strUser.Tables(0).Rows(0).Item("country").ToString)
            Process.AssignRadComboValue(cboEducation, strUser.Tables(0).Rows(0).Item("education").ToString)
            Process.AssignRadComboValue(cboField, strUser.Tables(0).Rows(0).Item("fieldtype").ToString)
            Process.AssignRadComboValue(cbogender, strUser.Tables(0).Rows(0).Item("gender").ToString)
            Process.AssignRadComboValue(cbomarital, strUser.Tables(0).Rows(0).Item("maritalstatus").ToString)
            Process.AssignRadComboValue(cbonationality, strUser.Tables(0).Rows(0).Item("nationality").ToString)
            Process.LoadListAndComboxFromDataset(lstLang, cboLanguage, "Recruit_Applicant_Get_Profile_Language", "value", "value", aemailadd.Value)
            Process.LoadListAndComboxFromDataset(lstskills, cboSkills, "Recruit_Applicant_Get_Profile_Skill", "value", "value", aemailadd.Value)
            Process.AssignRadComboValue(cboDiscipline, strUser.Tables(0).Rows(0).Item("discipline").ToString)
            Process.AssignRadComboValue(cbograde, strUser.Tables(0).Rows(0).Item("academicgrade").ToString)
            datDOB.SelectedDate = CDate(strUser.Tables(0).Rows(0).Item("dob"))
            If txtid.Text <> "0" Then
                'imgprofile.ImageUrl = "~/Module/Recruitment/ImgApplicantHandler.ashx?imgid=" & txtid.Text
                'Dim imgpath As String = Server.MapPath(emailFile + strUser.Tables(0).Rows(0).Item("imgtype").ToString)
                Dim imgpath As String = emailFile + strUser.Tables(0).Rows(0).Item("imgtype").ToString
                imgprofile.ImageUrl = imgpath
            End If
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                If Session("clicked") Is Nothing Then
                    Session("clicked") = "0"
                End If

                Process.LoadRadComboTextAndValue(cboSkills, "Job_Title_Skills_Get_All_Distinct", "skills", "skills", False)
                Process.LoadRadComboTextAndValue(cboField, "Recruit_Specialization_Get_All", "Name", "Name", False)
                Process.LoadRadComboTextAndValue(cbonationality, "Nationalities_get_all", "Name", "Name", True)
                Process.LoadRadComboTextAndValue(cboEducation, "Education_get_all", "Name", "Name", True)
                Process.LoadRadComboTextAndValue(cbocountry, "CountryTable_get", "Country", "Country", True)
                Process.LoadRadComboTextAndValue(cboLanguage, "Languages_get_all", "name", "name", False)
                Process.LoadRadComboTextAndValue(cboDiscipline, "Recruit_Academic_Discipline_Get_All", "name", "name", True)
                Process.LoadRadComboTextAndValue(cbograde, "Recruit_Academic_Grade_Get_All", "name", "name", True)

                cbogender.Items.Clear()
                cbogender.Items.Add("Female")
                cbogender.Items.Add("Male")

                cbomarital.Items.Clear()
                cbomarital.Items.Add("Single")
                cbomarital.Items.Add("Married")
                cbomarital.Items.Add("Divorced")
                cbomarital.Items.Add("Widowed")

                If Request.QueryString("id") IsNot Nothing Then
                    divpwd.Visible = False

                    If Request.QueryString("id") IsNot Nothing Then
                        Session("AppID") = Request.QueryString("id")
                    End If

                    LoadProfile(Request.QueryString("id"))
                    LoadOLResult(txtid.Text)
                Else
                    If txtid.Text = "" Then
                        txtid.Text = 0
                    End If

                    If Session("AppID") IsNot Nothing Then
                        txtid.Text = Session("AppID")
                        divschool.Visible = True
                        divpwd.Visible = False
                    Else
                        divschool.Visible = False
                    End If

                    aexpyears.Value = 0
                    arefyears1.Value = 0
                    arefyears2.Value = 0

                    LoadProfile(txtid.Text)
                    LoadOLResult(txtid.Text)
                End If
            End If

            If btcertificate.InnerText.Trim = "" Then
                btcertificate.Visible = False
            Else
                btcertificate.Visible = True
            End If

            If btcoverletter.InnerText.Trim = "" Then
                btcoverletter.Visible = False
            Else
                btcoverletter.Visible = True
            End If

            If btresume.InnerText.Trim = "" Then
                btresume.Visible = False
            Else
                btresume.Visible = True
            End If

            If Session("clicked") = "1" Then
                btnupdate.Focus()
            End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub




    Private Function GetPassword() As String
        Dim random As New Random()
        Dim num As String = Convert.ToString(random.Next(10, 20000)).PadLeft(6, "0")
        Return num
    End Function
    Private Function GetIdentity(Pwd As String, Skill As String, lang As String, cvfile As Byte(), cvtype As String, _
                                cvname As String, coverfile As Byte(), covertype As String, covername As String, certfile As Byte(), certtype As String, certname As String, photo As Byte(), phototype As String) As String
        Try
            Dim fname As String = ""
            Dim mname As String = ""
            aothername.Value = aothername.Value.Trim
            Dim AllWords() As String = aothername.Value.Split(" "c)
            If AllWords.Count > 0 Then
                If AllWords.Count > 1 Then
                    fname = AllWords(0)
                    mname = AllWords(1)
                Else
                    fname = AllWords(0)
                End If
            End If


            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Recruit_Applicant_Update"
            cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = txtid.Text.Trim
            cmd.Parameters.Add("@EmailAddress", SqlDbType.VarChar).Value = aemailadd.Value.Trim
            cmd.Parameters.Add("@MobileNo", SqlDbType.VarChar).Value = aphonenumber.Value.Trim
            cmd.Parameters.Add("@Gender", SqlDbType.VarChar).Value = cbogender.SelectedItem.Text
            cmd.Parameters.Add("@FieldType", SqlDbType.VarChar).Value = cboField.SelectedValue
            cmd.Parameters.Add("@DOB", SqlDbType.Date).Value = datDOB.SelectedDate
            cmd.Parameters.Add("@Education", SqlDbType.VarChar).Value = cboEducation.SelectedValue
            cmd.Parameters.Add("@Skill", SqlDbType.VarChar).Value = Skill
            cmd.Parameters.Add("@lang", SqlDbType.VarChar).Value = lang
            cmd.Parameters.Add("@Experience", SqlDbType.Int).Value = aexpyears.Value
            cmd.Parameters.Add("@Nationality", SqlDbType.VarChar).Value = cbonationality.SelectedItem.Text
            cmd.Parameters.Add("@FName", SqlDbType.VarChar).Value = fname
            cmd.Parameters.Add("@MName", SqlDbType.VarChar).Value = mname
            cmd.Parameters.Add("@LName", SqlDbType.VarChar).Value = alastname.Value.Trim
            cmd.Parameters.Add("@Marital", SqlDbType.VarChar).Value = cbomarital.SelectedItem.Text
            cmd.Parameters.Add("@Address", SqlDbType.VarChar).Value = aaddress.Value.Trim
            cmd.Parameters.Add("@City", SqlDbType.VarChar).Value = acity.Value
            cmd.Parameters.Add("@State", SqlDbType.VarChar).Value = astate.Value
            cmd.Parameters.Add("@Country", SqlDbType.VarChar).Value = cbocountry.SelectedValue
            cmd.Parameters.Add("@Pwd", SqlDbType.VarChar).Value = Pwd
            cmd.Parameters.Add("@RefereeName1", SqlDbType.VarChar).Value = arefname1.Value.Trim

            cmd.Parameters.Add("@RefereeAddress1", SqlDbType.VarChar).Value = arefaddr1.Value
            cmd.Parameters.Add("@RefereePhone1", SqlDbType.VarChar).Value = arefphone1.Value
            cmd.Parameters.Add("@RefereeEmail1", SqlDbType.VarChar).Value = arefemail1.Value
            cmd.Parameters.Add("@RefereePostion1", SqlDbType.VarChar).Value = arefposition1.Value
            cmd.Parameters.Add("@RefereeYears1", SqlDbType.Int).Value = arefyears1.Value
            cmd.Parameters.Add("@RefereeName2", SqlDbType.VarChar).Value = arefname2.Value.Trim
            cmd.Parameters.Add("@RefereeAddress2", SqlDbType.VarChar).Value = arefaddr2.Value.Trim
            cmd.Parameters.Add("@RefereePhone2", SqlDbType.VarChar).Value = arefphone2.Value
            cmd.Parameters.Add("@RefereeEmail2", SqlDbType.VarChar).Value = arefemail2.Value
            cmd.Parameters.Add("@RefereePostion2", SqlDbType.VarChar).Value = arefposition2.Value
            cmd.Parameters.Add("@RefereeYears2", SqlDbType.Int).Value = arefyears2.Value
            cmd.Parameters.Add("@cvfile", SqlDbType.Image).Value = cvfile
            cmd.Parameters.Add("@cvtype", SqlDbType.VarChar).Value = cvtype
            cmd.Parameters.Add("@cvname", SqlDbType.VarChar).Value = cvname
            cmd.Parameters.Add("@coverfile", SqlDbType.Image).Value = coverfile
            cmd.Parameters.Add("@covertype", SqlDbType.VarChar).Value = covertype
            cmd.Parameters.Add("@covername", SqlDbType.VarChar).Value = covername
            cmd.Parameters.Add("@certfile", SqlDbType.Image).Value = certfile
            cmd.Parameters.Add("@certtype", SqlDbType.VarChar).Value = certtype
            cmd.Parameters.Add("@certname", SqlDbType.VarChar).Value = certname
            cmd.Parameters.Add("@photofile", SqlDbType.Image).Value = photo
            cmd.Parameters.Add("@phototype", SqlDbType.VarChar).Value = phototype
            cmd.Parameters.Add("@academicgrade", SqlDbType.VarChar).Value = cbograde.SelectedValue
            cmd.Parameters.Add("@discipline", SqlDbType.VarChar).Value = cboDiscipline.SelectedValue
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
            Return 0
        End Try
    End Function
    Protected Sub lblcv_Click(sender As Object, e As EventArgs)
        Try
            Dim dt As DataTable = Process.SearchData("Recruit_Job_Applicant_Profile", txtid.Text)
            If dt IsNot Nothing Then
                'downloadFile(CType(dt.Rows(0)("cvfile"), Byte()), dt.Rows(0)("cvtype").ToString(), dt.Rows(0)("cvname").ToString())
            End If
            Dim fileName As String = dt.Rows(0)("cvname").ToString()
            Dim filePath As String = Server.MapPath(emailFile & fileName)
            Response.ContentType = ContentType
            Response.AppendHeader("Content-Disposition", ("attachment; filename=" + Path.GetFileName(filePath)))
            Response.WriteFile(filePath)
            Response.End()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub lblcertificate_Click(sender As Object, e As EventArgs)
        Try
            Dim dt As DataTable = Process.SearchData("Recruit_Job_Applicant_Profile", txtid.Text)
            If dt IsNot Nothing Then
                'downloadFile(CType(dt.Rows(0)("certfile"), Byte()), dt.Rows(0)("certtype").ToString(), dt.Rows(0)("certname").ToString())
            End If
            Dim fileName As String = dt.Rows(0)("certname").ToString()
            Dim filePath As String = Server.MapPath(emailFile & fileName)
            Response.ContentType = ContentType
            Response.AppendHeader("Content-Disposition", ("attachment; filename=" + Path.GetFileName(filePath)))
            Response.WriteFile(filePath)
            Response.End()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub lblcoverletter_Click(sender As Object, e As EventArgs)
        Try
            Dim dt As DataTable = Process.SearchData("Recruit_Job_Applicant_Profile", txtid.Text)
            If dt IsNot Nothing Then
                'downloadFile(CType(dt.Rows(0)("coverletterfile"), Byte()), dt.Rows(0)("coverlettertype").ToString(), dt.Rows(0)("coverlettername").ToString())
            End If
            Dim fileName As String = dt.Rows(0)("coverlettername").ToString()
            Dim filePath As String = Server.MapPath(emailFile & fileName)
            Response.ContentType = ContentType
            Response.AppendHeader("Content-Disposition", ("attachment; filename=" + Path.GetFileName(filePath)))
            Response.WriteFile(filePath)
            Response.End()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
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
    Protected Sub btnSend_Click(sender As Object, e As EventArgs)
        Try
            System.Threading.Thread.Sleep(300)
            Dim lblstatus As String = ""
            Dim cvtype As String = ""
            Dim cvname As String = ""
            Dim cvfile As Byte() = Nothing

            Dim coverlettertype As String = ""
            Dim coverlettername As String = ""
            Dim coverletterfile As Byte() = Nothing

            Dim certtype As String = ""
            Dim certname As String = ""
            Dim certfile As Byte() = Nothing

            Dim phototype As String = ""
            Dim photo As Byte() = Nothing

            If aemailadd.Value.Trim.Contains("@") = False Or aemailadd.Value.Trim = "" Then
                lblstatus = "Enter a valid email address"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                aemailadd.Focus()
                Exit Sub
            End If

            'Check Existence
            If txtid.Text = "0" Then
                Dim exists As Boolean = CBool(SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Recruit_Confirm_Applicant", aemailadd.Value.Trim))
                If exists = True Then
                    lblstatus = "Email Address already exists in our system!"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    aemailadd.Focus()
                    Exit Sub
                End If
            End If


            Dim rootFolder As String = Server.MapPath(Process.FileURL & "Applications/" + txtid.Text.PadLeft(6 - txtid.Text.Length, "0"))
            Dim AppFolder As String = rootFolder + "/" + aemailadd.Value

            If Not fileresume.PostedFile Is Nothing Then
                ''To create a PostedFile
                Dim img_strm As Stream = fileresume.PostedFile.InputStream
                Dim img_len As Integer = fileresume.PostedFile.ContentLength
                cvtype = fileresume.PostedFile.ContentType.ToString()
                cvname = Path.GetFileName(fileresume.PostedFile.FileName)
                cvfile = New Byte(img_len - 1) {}
                If cvname <> "" Then
                    Dim n As Integer = img_strm.Read(cvfile, 0, img_len)
                    fileresume.PostedFile.SaveAs(Server.MapPath(emailFile + "Resume_" & alastname.Value & cvname))
                    cvname = "Resume_" & alastname.Value & cvname
                End If
            Else
                    If btresume.InnerText = "" Then
                    lblstatus = "No Resume available for upload, please select resume"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    fileresume.Focus()
                    Exit Sub
                End If
            End If

            'Save Cover Letter
            If Not filecoverletter.PostedFile Is Nothing Then



                Dim img_strm As Stream = filecoverletter.PostedFile.InputStream
                    Dim img_len As Integer = filecoverletter.PostedFile.ContentLength
                    coverlettertype = filecoverletter.PostedFile.ContentType.ToString()
                    coverlettername = Path.GetFileName(filecoverletter.PostedFile.FileName)
                coverletterfile = New Byte(img_len - 1) {}
                If coverlettername <> "" Then
                    Dim n As Integer = img_strm.Read(coverletterfile, 0, img_len)
                    filecoverletter.PostedFile.SaveAs(Server.MapPath(emailFile + "Coverletter_" & alastname.Value & coverlettername))
                    coverlettername = "Coverletter_" & alastname.Value & coverlettername
                End If

            Else
                    If btcoverletter.InnerText = "" Then
                    lblstatus = "No Cover Letter available for upload, please select Cover Letter"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    filecoverletter.Focus()
                    Exit Sub
                End If

            End If

            'Save Cover Letter
            If Not filecertificate.PostedFile Is Nothing Then
                Dim img_strm As Stream = filecertificate.PostedFile.InputStream
                Dim img_len As Integer = filecertificate.PostedFile.ContentLength
                certtype = filecertificate.PostedFile.ContentType.ToString()
                certname = Path.GetFileName(filecertificate.PostedFile.FileName)
                certfile = New Byte(img_len - 1) {}
                If certname <> "" Then
                    Dim n As Integer = img_strm.Read(certfile, 0, img_len)
                    filecertificate.PostedFile.SaveAs(Server.MapPath(emailFile + "Certificate_" & alastname.Value & certname))
                    certname = "Certificate_" & alastname.Value & certname
                End If

            Else
                    If btcertificate.InnerText = "" Then
                    lblstatus = "No School Certificate available for upload, please select"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    filecertificate.Focus()
                    Exit Sub
                End If

            End If

            If Not imguploads.PostedFile Is Nothing Then
                Dim img_strm As Stream = imguploads.PostedFile.InputStream
                Dim img_len As Integer = imguploads.PostedFile.ContentLength
                'phototype = imguploads.PostedFile.ContentType.ToString()
                phototype = Path.GetFileName(imguploads.PostedFile.FileName)
                photo = New Byte(img_len - 1) {}
                If phototype <> "" Then
                    Dim n As Integer = img_strm.Read(photo, 0, img_len)
                    imguploads.PostedFile.SaveAs(Server.MapPath(emailFile) + "ApplicantPhoto_" & alastname.Value & phototype)
                    phototype = "ApplicantPhoto_" & alastname.Value & phototype
                End If
            End If


            If alastname.Value.Trim = "" Then
                alastname.Focus()
                lblstatus = "Last Name required"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                Exit Sub
            End If

            If aothername.Value.Trim = "" Then
                aothername.Focus()
                lblstatus = "Other Names required"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                Exit Sub
            End If

            If IsNumeric(aexpyears.Value) = False Then
                aexpyears.Focus()
                lblstatus = "Years of Experience is required"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                Exit Sub
            End If

            If IsDate(datDOB.SelectedDate) = False Then
                datDOB.Focus()
                lblstatus = "Date of Birth is required"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                Exit Sub
            End If

            If aaddress.Value.Trim = "" Then
                aaddress.Focus()
                lblstatus = "Residential Address is required"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                Exit Sub
            End If

            If astate.Value.Trim = "" Then
                astate.Focus()
                lblstatus = "State/Province is required"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                Exit Sub
            End If

            Dim mobileno As String = ""
            Dim skills As String = ""
            Dim lang As String = ""
            Dim DOB As Date
            Dim FieldOfWork As String = ""
            Dim Education As String = ""

            If aphonenumber.Value.Trim <> "" Then
                mobileno = aphonenumber.Value.Trim
            End If

            skills = ""
            For i As Integer = 0 To lstskills.Items.Count - 1
                If skills.Trim = "" Then
                    skills = lstskills.Items.Item(i).Text
                Else
                    skills = skills & "; " & lstskills.Items.Item(i).Text
                End If
            Next

            lang = ""
            For i As Integer = 0 To lstLang.Items.Count - 1
                If lang.Trim = "" Then
                    lang = lstLang.Items.Item(i).Text
                Else
                    lang = lang & "; " & lstLang.Items.Item(i).Text
                End If
            Next

            If cboField.SelectedItem.Text <> "" Then
                FieldOfWork = cboField.SelectedItem.Text
            End If

            If arefname1.Value.Trim <> "" Then
                If arefaddr1.Value.Trim = "" Then
                    lblstatus = "Organisation/ Address of Referee 1 is required"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    arefaddr1.Focus()
                    Exit Sub
                End If

                If arefphone1.Value.Trim = "" Then
                    lblstatus = "Contact Number of Referee 1 is required"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    arefphone1.Focus()
                    Exit Sub
                End If


                If arefemail1.Value.Trim <> "" Then
                    If arefemail1.Value.Contains("@") = False Then
                        lblstatus = "Email Address of Referee 1 is required"
                        Process.loadalert(divalert, msgalert, lblstatus, "warning")
                        arefemail1.Focus()
                        Exit Sub
                    End If
                End If

                If arefposition1.Value.Trim = "" Then
                    lblstatus = "Job Status of Referee 1 is required"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    arefposition1.Focus()
                    Exit Sub
                End If

                If arefyears1.Value.Trim = "" Then
                    lblstatus = "Years known to Referee 1 is required"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    arefyears1.Focus()
                    Exit Sub
                End If
            End If

            If arefname2.Value.Trim <> "" Then
                If arefaddr2.Value.Trim = "" Then
                    lblstatus = "Organisation/ Address of Referee 2 is required"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    arefaddr2.Focus()
                    Exit Sub
                End If

                If arefphone2.Value.Trim = "" Then
                    lblstatus = "Contact Number of Referee 2 is required"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    arefphone2.Focus()
                    Exit Sub
                End If

                If arefemail2.Value.Trim <> "" Then
                    If arefemail2.Value.Contains("@") = False Then
                        lblstatus = "Email Address of Referee 2 is required"
                        Process.loadalert(divalert, msgalert, lblstatus, "warning")
                        arefemail2.Focus()
                        Exit Sub
                    End If
                End If

                If arefposition2.Value.Trim = "" Then
                    lblstatus = "Job Status of Referee 2 is required"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    arefposition2.Focus()
                    Exit Sub
                End If

                If arefyears2.Value.Trim = "" Then
                    lblstatus = "Years known to Referee 2 is required"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    arefyears2.Focus()
                    Exit Sub
                End If
            End If

            If apwd.Value.Trim <> aconfirmpwd.Value.Trim Then
                lblstatus = "Password mismatch!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                apwd.Focus()
                Exit Sub
            End If

            Dim Password As String = ""
            If apwd.Value.Trim <> "" Then
                Password = Process.Encrypt(apwd.Value.Trim)
            End If

            If txtid.Text = "0" Then
                txtid.Text = GetIdentity(Password, skills, lang, Nothing, cvtype, cvname, Nothing, coverlettertype, coverlettername, Nothing, certtype, certname, Nothing, phototype)
            Else
                If GetIdentity(Password, skills, lang, Nothing, cvtype, cvname, Nothing, coverlettertype, coverlettername, Nothing, certtype, certname, Nothing, phototype) = "0" Then
                    Exit Sub
                End If
            End If




            If txtid.Text = 0 Then

                Exit Sub
            End If
            Session("ApplcantID") = aemailadd.Value
            lblstatus = "Profile saved!"
            LoadProfile(txtid.Text)
            If Session("AppID") Is Nothing Then
                Session("AppID") = txtid.Text
                Session("ApplicantName") = alastname.Value & " " & aothername.Value
                If divpwd.Visible = True = True Then
                    Process.Applicant_New_Profile(aemailadd.Value.Trim, aothername.Value, aemailadd.Value, apwd.Value)
                End If

                divpwd.Visible = False

                apwd.Value = ""
                aconfirmpwd.Value = ""

                If GridVwHeaderChckbox.Rows.Count < 1 Then
                    divschool.Visible = True
                    lblstatus = "Profile saved,scroll down to input your school leaving results!"
                    Process.loadalert(divalert, msgalert, lblstatus, "info")
                    btnupdate.Focus()
                    Exit Sub

                End If

                lblstatus = "Profile successfully created!"
                Process.loadalert(divalert, msgalert, lblstatus, "success")

            Else
                Session("clicked") = "0"
            End If
        Catch ex As Exception
            btsave.Disabled = False
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Function MatchApplicant() As Integer
        Dim Datas As New DataTable
        Dim job As String = ""
        job = Session("JobID")
        Datas = Process.SearchDataP6("recruit_Applicant_System_Shortlisting", job, cbogender.SelectedItem.Text, "Yes", "Yes", "Yes", aemailadd.Value)

        Dim strUser As New DataSet
        strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "recruit_Applicant_System_Shortlisting", job, cbogender.SelectedItem.Text, "Yes", "Yes", "Yes", aemailadd.Value)
        Return strUser.Tables(0).Rows.Count
    End Function

    Protected Sub cboSkills_ItemChecked(sender As Object, e As Telerik.Web.UI.RadComboBoxItemEventArgs) Handles cboSkills.ItemChecked
        Process.LoadListBoxFromCombo(lstskills, cboSkills)
    End Sub

    Protected Sub cboLanguage_ItemChecked(sender As Object, e As Telerik.Web.UI.RadComboBoxItemEventArgs) Handles cboLanguage.ItemChecked
        Process.LoadListBoxFromCombo(lstLang, cboLanguage)
    End Sub

    Protected Sub btnAddResult_Click(sender As Object, e As EventArgs)
        DrillDown("", txtid.Text)
    End Sub
    Protected Sub DrillDown(id As String, appid As String)
        Try
            Dim url As String = ""
            If id = "" Then
                url = "ExamResultUpdate.aspx?appid=" & appid
            Else
                url = "ExamResultUpdate.aspx?id=" & id & "appid=" & appid
            End If

            Dim s As String = "window.open('" & url + "', 'popup_window', 'width=620,height=400,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no');"
            ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")            
        End Try
    End Sub
    Private Sub LoadOLResult(id As String)
        Try
            'tabSchool.Visible = True
            GridVwHeaderChckbox.DataSource = Process.SearchData("Recruit_Applicant_School_Leaving_Result_Get_All", id)
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub
    Protected Sub btndelete_Click(sender As Object, e As EventArgs)
        Try
            Dim count As Integer = 0
            Dim confirmValue As String = Request.Form("confirm_value")
            If confirmValue = "Yes" Then
                Session("clicked") = "1"
                Dim atLeastOneRowDeleted As Boolean = False
                ' Iterate through the Products.Rows property
                For Each row As GridViewRow In GridVwHeaderChckbox.Rows
                    ' Access the CheckBox
                    Dim cb As CheckBox = row.FindControl("chkEmp")
                    If cb IsNot Nothing AndAlso cb.Checked Then
                        count = count + 1
                        ' Delete row! (Well, not really...)
                        atLeastOneRowDeleted = True
                        ' First, get the ProductID for the selected row
                        Dim ID As String = _
                            Convert.ToString(GridVwHeaderChckbox.DataKeys(row.RowIndex).Value)
                        ' "Delete" the row
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Applicant_School_Leaving_Result_Delete", ID)
                    End If
                Next
                Process.loadalert(divalert, msgalert, count.ToString & " records successfully deleted", "success")
                LoadOLResult(Session("AppID"))
            
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

   

    Protected Sub gridOLSubject_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridVwHeaderChckbox.SelectedIndexChanged
        DrillDown(GridVwHeaderChckbox.SelectedDataKey.Value, txtid.Text)
    End Sub
End Class