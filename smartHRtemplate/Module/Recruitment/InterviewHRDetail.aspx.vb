Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports System.IO


Public Class InterviewHRDetail
    Inherits System.Web.UI.Page
    Private Sub LoadGrid(applicantid As String, jobid As Integer)
        Try
            GridVwHeaderChckbox.DataSource = Process.SearchDataP2("Recruit_Job_Test_Online_Result_Applicant_Get", applicantid, jobid)
            GridVwHeaderChckbox.AllowSorting = True
            GridVwHeaderChckbox.AllowPaging = True
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")            
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then

                'ViewState("PreviousPage") = Request.UrlReferrer
                radRecommendation.Items.Clear()
                radRecommendation.Items.Add("Advise to employ")
                radRecommendation.Items.Add("Disqualify")
                radRecommendation.Items.Add("Hold-on")

                Dim strUser As New DataSet
                If Request.QueryString("id") IsNot Nothing Then
                    Session("intHRDetailID") = Request.QueryString("id")
                End If
                strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Job_Interviewee_Get", Session("intHRDetailID"))
                Session("company") = strUser.Tables(0).Rows(0).Item("company").ToString
                lblID.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                lbljobid.Text = strUser.Tables(0).Rows(0).Item("jobid").ToString
                aname.Value = strUser.Tables(0).Rows(0).Item("CandidateName").ToString

                aemailaddr.Value = strUser.Tables(0).Rows(0).Item("CandidateEmail").ToString
                agender.Value = strUser.Tables(0).Rows(0).Item("Gender").ToString
                aspecialisation.Value = strUser.Tables(0).Rows(0).Item("FieldType").ToString
                If IsDate(strUser.Tables(0).Rows(0).Item("DOB")) Then
                    adob.Value = CDate(strUser.Tables(0).Rows(0).Item("DOB").ToString).ToLongDateString
                End If

                aeducation.Value = strUser.Tables(0).Rows(0).Item("Education").ToString
                aexpyear.Value = strUser.Tables(0).Rows(0).Item("Experience").ToString
                lnkccletter.Attributes.Add("title", strUser.Tables(0).Rows(0).Item("CoverLetter").ToString)
                lnkcert.Attributes.Add("title", strUser.Tables(0).Rows(0).Item("certificate").ToString)
                lnkcv.Attributes.Add("title", strUser.Tables(0).Rows(0).Item("cv").ToString)
                lnkoffletter.Attributes.Add("title", "Offer Letter")

                acomment.Value = strUser.Tables(0).Rows(0).Item("comment").ToString
                lblrecruit.Text = strUser.Tables(0).Rows(0).Item("recruited").ToString
                aoffer.Value = strUser.Tables(0).Rows(0).Item("OfferLetterSent").ToString
                lblApplicantID.Text = strUser.Tables(0).Rows(0).Item("Applicantid").ToString
                lblapplicant.Text = strUser.Tables(0).Rows(0).Item("oapplicantid").ToString
                pagetitle.InnerText = strUser.Tables(0).Rows(0).Item("Title").ToString
                amedicalcoment.Value = strUser.Tables(0).Rows(0).Item("medicalcomment").ToString
                lblcoverletter.Text = strUser.Tables(0).Rows(0).Item("CoverLetter").ToString
                lblresume.Text = strUser.Tables(0).Rows(0).Item("CV").ToString
                lblCertificate.Text = strUser.Tables(0).Rows(0).Item("certificate").ToString

                If lblcoverletter.Text.Trim = "" Then
                    lnkccletter.Disabled = True
                    lnkccletter.InnerText = "No Cover Letter"
                End If

                If lblresume.Text.Trim = "" Then
                    lnkcv.Disabled = True
                    lnkcv.InnerText = "No Resume"
                End If

                If lblCertificate.Text.Trim = "" Then
                    lnkcert.Disabled = True
                    lnkcert.InnerText = "No Certificate"
                End If

                Process.AssignRadDropDownValue(radRecommendation, strUser.Tables(0).Rows(0).Item("recommendation").ToString)
                If strUser.Tables(0).Rows(0).Item("medicalrequest").ToString.ToLower = "no" Then
                    amedicalreq.Value = "Not requested"
                Else
                    amedicalreq.Value = "Requested  " + CDate(strUser.Tables(0).Rows(0).Item("medicalrequestdate")).ToLongDateString
                End If
                If strUser.Tables(0).Rows(0).Item("accountrequest").ToString.ToLower = "no" Then
                    aacctreq.Value = "Not requested"
                Else
                    aacctreq.Value = "Requested  " + CDate(strUser.Tables(0).Rows(0).Item("accountrequestdate")).ToLongDateString
                End If
                arecruited.Value = strUser.Tables(0).Rows(0).Item("recruited").ToString

                If IsDBNull(strUser.Tables(0).Rows(0).Item("filepath").ToString) = False Then
                    Session("hrjoboffer") = strUser.Tables(0).Rows(0).Item("filepath").ToString
                Else
                    Session("hrjoboffer") = ""
                End If

                LoadGrid(aemailaddr.Value, lbljobid.Text)
                If GridVwHeaderChckbox.Rows.Count <= 0 Then
                    divgrid.Visible = False
                End If

                If aoffer.Value.ToLower = "yes" Then
                    lnkoffletter.Visible = True
                Else
                    lnkoffletter.Visible = False
                End If

            End If
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
    Protected Sub downloadCV(ByVal dt As DataTable)
        Dim bytes() As Byte = CType(dt.Rows(0)("cvfile"), Byte())
        Response.Buffer = True
        Response.Charset = ""
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = dt.Rows(0)("cvtype").ToString()
        Response.AddHeader("content-disposition", "attachment;filename=" & dt.Rows(0)("cvname").ToString())
        Response.BinaryWrite(bytes)
        Response.Flush()
        Response.End()
    End Sub
    Protected Sub downloadCoverLetter(ByVal dt As DataTable)
        Dim bytes() As Byte = CType(dt.Rows(0)("coverletterfile"), Byte())
        Response.Buffer = True
        Response.Charset = ""
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = dt.Rows(0)("coverlettertype").ToString()
        Response.AddHeader("content-disposition", "attachment;filename=" & dt.Rows(0)("coverlettername").ToString())
        Response.BinaryWrite(bytes)
        Response.Flush()
        Response.End()
    End Sub
    Protected Sub downloadCert(ByVal dt As DataTable)
        Dim bytes() As Byte = CType(dt.Rows(0)("certfile"), Byte())
        Response.Buffer = True
        Response.Charset = ""
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = dt.Rows(0)("certtype").ToString()
        Response.AddHeader("content-disposition", "attachment;filename=" & dt.Rows(0)("certname").ToString())
        Response.BinaryWrite(bytes)
        Response.Flush()
        Response.End()
    End Sub


    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try

            Dim lblstatus As String = ""
            Process.loadalert(divalert, msgalert, lblstatus, "warning")
            If radRecommendation.SelectedText Is Nothing Then
                lblstatus = "Recommendation about candidate is required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                radRecommendation.Focus()
                Exit Sub
            End If

            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Job_Interviewee_Update", lblID.Text, acomment.Value, radRecommendation.SelectedText, amedicalcoment.Value.Trim, Session("LoginID"))
            lblstatus = "Record saved"
            Process.loadalert(divalert, msgalert, lblstatus, "success")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub lnkResume_Click(sender As Object, e As EventArgs)
        Try
            Dim lblstatus As String = ""
            If lblresume.Text.Trim = "" Then
                lblstatus = "No Resume to download for applicant"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
            Else
                Dim dt As DataTable = Process.SearchData("Recruit_Job_Interviewee_Get", lblID.Text)
                If dt IsNot Nothing Then
                    downloadFile(CType(dt.Rows(0)("cvfile"), Byte()), dt.Rows(0)("cvtype").ToString(), dt.Rows(0)("cvname").ToString())
                End If
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub lnkCoverLetter_Click(sender As Object, e As EventArgs)
        Try
            Dim lblstatus As String = ""
            If lblcoverletter.Text.Trim = "" Then
                lblstatus = "No cover letter to download for applicant"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
            Else
                Dim dt As DataTable = Process.SearchData("Recruit_Job_Interviewee_Get", lblID.Text)
                If dt IsNot Nothing Then
                    'downloadCoverLetter(dt)
                    downloadFile(CType(dt.Rows(0)("coverletterfile"), Byte()), dt.Rows(0)("coverlettertype").ToString(), dt.Rows(0)("coverlettername").ToString())
                End If
            End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub lnkCert_Click(sender As Object, e As EventArgs)
        Try
            Dim lblstatus As String = ""
            If lblCertificate.Text.Trim = "" Then
                lblstatus = "No Certificate to download for applicant"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
            Else
                Dim dt As DataTable = Process.SearchData("Recruit_Job_Interviewee_Get", lblID.Text)
                If dt IsNot Nothing Then
                    downloadFile(CType(dt.Rows(0)("certfile"), Byte()), dt.Rows(0)("certtype").ToString(), dt.Rows(0)("certname").ToString())
                End If
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnRecruit_Click(sender As Object, e As EventArgs)
        Try
            Dim lblstatus As String = ""
            If lblrecruit.Text.ToLower = "yes" Then
                lblstatus = "Candidate has already being recruited"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                Exit Sub
            End If

            If radRecommendation.SelectedText <> "Advise to employ" Then
                lblstatus = "Candidate's recommendation has not be set to Advise to Employ, command has been cancelled"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                Exit Sub
            Else
                '"JobinterviewUpdate.aspx?id=" + code
                Session("JobID") = lbljobid.Text
                Session("ApplcantID") = aemailaddr.Value.Trim
                arecruited.Value = "Processing ..."
                'Dim url As String = "RecruitCandidate.aspx?id=" & lblApplicantID.Text
                'Dim s As String = "window.open('" & url + "', 'popup_window', 'width=1050,height=900,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no');"
                'ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)

                Response.Redirect("~/Module/Recruitment/RecruitCandidate?id=" & lblApplicantID.Text, True)
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnJobOffer_Click(sender As Object, e As EventArgs)
        Try
            Dim lblstatus As String = ""

            If radRecommendation.SelectedText <> "Advise to employ" Then
                lblstatus = "Candidate's recommendation has not be set to Advise to Employ, command has been cancelled"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                Exit Sub
            Else
                Session("JobID") = lbljobid.Text
                Session("ApplcantID") = aemailaddr.Value.Trim
                Response.Redirect("~/Module/Recruitment/MailTemplate?appid=" & lblApplicantID.Text & "&template=joboffer", True)
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnMedicalReq_Click(sender As Object, e As EventArgs)
        Try
            Dim lblstatus As String = ""
            Dim URL As String = Process.ApplicationURL ' "http://" & System.Net.Dns.GetHostByName(Server.MachineName.ToString).AddressList(0).ToString() & "/GOSHRM" 'URL.Replace("localhost", Server.MachineName.ToString)
            Dim BiodataURL As String = ConfigurationManager.AppSettings("BioDataURL")
            If Process.Candidate_Medical_Info_Notification(Session("company"), aemailaddr.Value.Trim, pagetitle.InnerText, aname.Value, URL & BiodataURL & lblApplicantID.Text) = True Then
                lblstatus = "Medical Information request successfully sent to " & aemailaddr.Value
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Applications_Update_Stat", lblApplicantID.Text, "", lbljobid.Text, "Medical Assessment")
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Job_Interview_Interviewee_Addition_Info", lblID.Text, "medical")
                amedicalreq.Value = "Requested  " + DateTime.Now.ToLongDateString
                Process.loadalert(divalert, msgalert, lblstatus, "success")
            Else
                Process.loadalert(divalert, msgalert, Session("exception"), "danger")
            End If


        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")            
        End Try
    End Sub

    Protected Sub lnkBioData_Click(sender As Object, e As EventArgs)
        Try
            Dim url As String = "ApplicantBioDataView.aspx?ApplicantID=" & lblApplicantID.Text
            Dim s As String = "window.open('" & url + "', 'popup_window', 'width=1050,height=900,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no');"
            ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)

            'Response.Redirect("~/Module/Recruitment/ApplicantBioDataView?ApplicantID=" & lblApplicantID.Text, True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnAccountInfo_Click(sender As Object, e As EventArgs)
        Try
            Dim lblstatus As String = ""
            Dim URL As String = Process.ApplicationURL ' "http://" & System.Net.Dns.GetHostByName(Server.MachineName.ToString).AddressList(0).ToString() & "/GOSHRM" 'URL.Replace("localhost", Server.MachineName.ToString)
            Dim BiodataURL As String = ConfigurationManager.AppSettings("AccountDataURL")
            If Process.Candidate_BankAccount_Info_Notification(Session("company"), aemailaddr.Value.Trim, pagetitle.InnerText, aname.Value, URL & BiodataURL & lblApplicantID.Text) = True Then
                lblstatus = "Bank Account Information request successfully sent to " & aemailaddr.Value
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Job_Interview_Interviewee_Addition_Info", lblID.Text, "account")
                aacctreq.Value = "Requested  " + DateTime.Now.ToLongDateString
                Process.loadalert(divalert, msgalert, lblstatus, "success")
            Else
                Process.loadalert(divalert, msgalert, Session("exception"), "danger")
            End If


        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Protected Sub lnkofferletter_Click(sender As Object, e As EventArgs)
        Try
            Dim lblstatus As String = ""
            Process.loadalert(divalert, msgalert, lblstatus, "danger")
            If Session("hrjoboffer") <> "" Then
                Process.downloadFile(Session("hrjoboffer"))
            Else
                lblstatus = "File not found"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
            End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
End Class