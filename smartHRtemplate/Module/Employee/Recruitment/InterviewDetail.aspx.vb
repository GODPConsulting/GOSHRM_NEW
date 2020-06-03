Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports System.IO


Public Class InterviewDetail
    Inherits System.Web.UI.Page
    Dim AuthenCode As String
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
                cborecommendation.Items.Clear()
                cborecommendation.Items.Add("Advise to employ")
                cborecommendation.Items.Add("Disqualify")
                cborecommendation.Items.Add("Hold-on")

                Dim strUser As New DataSet
                strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Job_Interview_Comment_Get", Request.QueryString("id"))

                If strUser.Tables(0).Rows.Count <= 0 Then
                    Process.loadalert(divalert, msgalert, "No record found!", "danger")
                    Exit Sub
                End If
                lblID.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                lbljobid.Text = strUser.Tables(0).Rows(0).Item("jobid").ToString
                acandidate.Value = strUser.Tables(0).Rows(0).Item("CandidateName").ToString
                aemailaddress.Value = strUser.Tables(0).Rows(0).Item("CandidateEmail").ToString
                agender.Value = strUser.Tables(0).Rows(0).Item("Gender").ToString
                aspecialisation.Value = strUser.Tables(0).Rows(0).Item("FieldType").ToString
                If IsDate(strUser.Tables(0).Rows(0).Item("DOB")) = True Then
                    adob.Value = CDate(strUser.Tables(0).Rows(0).Item("DOB")).ToLongDateString
                Else
                    adob.Value = strUser.Tables(0).Rows(0).Item("DOB").ToString
                End If
                aeducation.Value = strUser.Tables(0).Rows(0).Item("Education").ToString                
               
                If IsDBNull(strUser.Tables(0).Rows(0).Item("CoverLetter")) Then
                    divcoverletter.Visible = False
                Else
                    If strUser.Tables(0).Rows(0).Item("CoverLetter").ToString = "" Then
                        divcoverletter.Visible = False
                    Else
                        divcoverletter.Visible = True
                    End If
                End If

                If IsDBNull(strUser.Tables(0).Rows(0).Item("CV")) Then
                    divresume.Visible = False
                Else
                    If strUser.Tables(0).Rows(0).Item("CV").ToString = "" Then
                        divresume.Visible = False
                    Else
                        divresume.Visible = True
                    End If
                End If

                If IsDBNull(strUser.Tables(0).Rows(0).Item("certificate")) Then
                    divcert.Visible = False
                Else
                    If strUser.Tables(0).Rows(0).Item("certificate").ToString = "" Then
                        divcert.Visible = False
                    Else
                        divcert.Visible = True
                    End If
                End If
                acomment.Value = strUser.Tables(0).Rows(0).Item("comment").ToString
                ahrrecommendation.Value = strUser.Tables(0).Rows(0).Item("hr_recommendation").ToString
                Process.AssignRadComboValue(cborecommendation, strUser.Tables(0).Rows(0).Item("interviewer_recommendation").ToString)
                Session("ApplcantName") = strUser.Tables(0).Rows(0).Item("CandidateName").ToString
                Session("Interviewer") = strUser.Tables(0).Rows(0).Item("Employee").ToString
                Session("InterviewerID") = strUser.Tables(0).Rows(0).Item("interviewer").ToString
                Session("ApplcantID") = strUser.Tables(0).Rows(0).Item("ApplicantID").ToString
                lnkattach.InnerText = strUser.Tables(0).Rows(0).Item("evaluationfilename").ToString
                LoadGrid(aemailaddress.Value, lbljobid.Text)
                If GridVwHeaderChckbox.Rows.Count <= 0 Then
                    divperformance.Visible = False
                End If

                If IsDBNull(strUser.Tables(0).Rows(0).Item("evaluationfilename").ToString) = False Then
                    lnkattach.Visible = True
                Else
                    lnkattach.Visible = False
                End If

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            'Response.Write("<script language='javascript'> { self.close() }</script>")
            'If ViewState("PreviousPage") IsNot Nothing Then
            '    Response.Redirect(ViewState("PreviousPage").ToString())
            'Else
            '    Response.Write("<script language='javascript'> { self.close() }</script>")
            'End If
            Response.Redirect("interviews", True)
        Catch ex As Exception
        End Try
    End Sub



    Protected Sub btnSend_Click(sender As Object, e As EventArgs)
        Try
            Dim lblstatus As String = ""
            If acomment.Value.Trim = "" Then
                lblstatus = "Your comment about the candidate is required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                acomment.Focus()
                Exit Sub
            End If

            If cborecommendation.SelectedItem.Text Is Nothing Then
                lblstatus = "Your recommendation about candidate is required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                cborecommendation.Focus()
                Exit Sub
            End If

            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Job_Interview_Comment_Update", lblID.Text, acomment.Value, cborecommendation.SelectedItem.Text, Session("LoginID"))
            lblstatus = "Record saved"
            Process.loadalert(divalert, msgalert, lblstatus, "success")
            'Recruit_Job_Interview_Comment_Update
        Catch ex As Exception

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
    Protected Sub lnkResume_Click(sender As Object, e As EventArgs)
        Try
            Dim dt As DataTable = Process.SearchData("Recruit_Job_Interview_Comment_Get", lblID.Text)
            If dt IsNot Nothing Then
                downloadFile(CType(dt.Rows(0)("cvfile"), Byte()), dt.Rows(0)("cvtype").ToString(), dt.Rows(0)("cvname").ToString())
            End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub lnkCoverLetter_Click(sender As Object, e As EventArgs)

        Try
   
                Dim dt As DataTable = Process.SearchData("Recruit_Job_Interview_Comment_Get", lblID.Text)
                If dt IsNot Nothing Then
                    'downloadCoverLetter(dt)
                    downloadFile(CType(dt.Rows(0)("coverletterfile"), Byte()), dt.Rows(0)("coverlettertype").ToString(), dt.Rows(0)("coverlettername").ToString())
                End If


        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try

    End Sub

    Protected Sub lnkCert_Click(sender As Object, e As EventArgs)
        Try

                Dim dt As DataTable = Process.SearchData("Recruit_Job_Interview_Comment_Get", lblID.Text)
                If dt IsNot Nothing Then
                    downloadFile(CType(dt.Rows(0)("certfile"), Byte()), dt.Rows(0)("certtype").ToString(), dt.Rows(0)("certname").ToString())
                End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub lnkEvaluation_Click(sender As Object, e As EventArgs)

        'Response.Redirect("~/Module/Employee/Recruitment/InterviewEvaluationForm?Id=" & lblID.Text, True)

        Try
            Dim url As String = "InterviewEvaluationForm?id=" & lblID.Text
            Dim s As String = "window.open('" & url + "', 'popup_window', 'width=1050,height=900,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no');"
            ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Function GetFileID(ByVal fileimage As Byte(), ByVal filename As String, ByVal filetype As String) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Recruit_Job_Interview_Comment_Update_File"
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@fileimage", SqlDbType.Image).Value = fileimage
            cmd.Parameters.Add("@filename", SqlDbType.VarChar).Value = filename
            cmd.Parameters.Add("@filetype", SqlDbType.VarChar).Value = filetype
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception
            Process.strExp = ex.Message
            Return "0"
        End Try
    End Function

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) 'Handles btnUpload.Click
        Try
            'If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
            '    Process.loadalert(divalert, msgalert, Process.privilegemsg, "warning")
            '    Exit Sub
            'End If

            If Not file1.PostedFile Is Nothing Then
                Dim strtype As String = ""
                Dim strname As String = ""
                Dim imgdata As Byte() = Nothing
                Dim strsize As Integer = 0

                Dim img_strm As Stream = file1.PostedFile.InputStream
                Dim img_len As Integer = file1.PostedFile.ContentLength
                strtype = file1.PostedFile.ContentType.ToString()
                strname = Path.GetFileName(file1.PostedFile.FileName)
                strsize = file1.PostedFile.ContentLength

                imgdata = New Byte(img_len - 1) {}
                Dim n As Integer = img_strm.Read(imgdata, 0, img_len)

                If lblID.Text = "0" Or lblID.Text = "" Then
                    lblID.Text = GetFileID(imgdata, strname, strtype)
                    If lblID.Text = "0" Then
                        Exit Sub
                    End If
                Else
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Job_Interview_Comment_Update_File", lblID.Text, imgdata, strname, strtype)
                End If
                lnkattach.InnerText = strname
                lnkattach.Visible = True
            End If

            Process.loadalert(divalert, msgalert, "Uploaded Successfully", "success")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Private Sub btComplete_Click(sender As Object, e As System.EventArgs) Handles btComplete.Click
        Try
            Dim lblstatus As String = ""
            Dim msg As String = ""
            Dim msgbuild As New StringBuilder()
            Dim confirmValue As String = Request.Form("confirm_value")
            If confirmValue = "No" Then
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Cancelled" + "')", True)
            Else
                'Send Mail to Employees with competencies
                Dim strHOD As New DataSet
                Dim hodemail As String = ""
                Dim hodname As String = ""
                Dim hiringmgrname As String = ""

                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Job_Interview_Comment_Update", lblID.Text, acomment.Value, cborecommendation.SelectedItem.Text, Session("LoginID"))

                strHOD = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "select name from dbo.Employees_All where supervisorID = '" & Session("UserEmpID") & "'")
                If strHOD.Tables(0).Rows.Count > 0 Then
                    hiringmgrname = strHOD.Tables(0).Rows(0).Item("name").ToString
                End If
                Process.requestedURL = "Module/Recruitment/JobInterviews"
                Process.Interview_Detail_Alert_Approver_complete(hiringmgrname, acandidate.Value, aspecialisation.Value, agender.Value, Session("UserEmpID"), Session("HiringMgrID"), Process.ApplicationURL & "/" & Process.requestedURL)
                lblstatus = "Interview Evaluation is forwarded for approval"
                Process.loadalert(divalert, msgalert, lblstatus, "success")
                Response.Redirect("interviews", True)
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub lnkDownload_Click(sender As Object, e As EventArgs)
        Try
            Dim lblstatus As String = ""
            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Job_Interview_Comment_File_Clear", lblID.Text)
            lblstatus = "Evaluation form removed!"
            lnkattach.InnerText = ""
            lnkattach.Visible = False
            Process.loadalert(divalert, msgalert, lblstatus, "success")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub downloadevaluation_Click(sender As Object, e As EventArgs)
        Try
            Dim dt As DataTable = Process.SearchData("Recruit_Job_Interview_Comment_Get", lblID.Text)
            If dt IsNot Nothing Then
                downloadFile(CType(dt.Rows(0)("evaluationfileimage"), Byte()), dt.Rows(0)("evaluationfiletype").ToString(), dt.Rows(0)("evaluationfilename").ToString())
            End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
End Class