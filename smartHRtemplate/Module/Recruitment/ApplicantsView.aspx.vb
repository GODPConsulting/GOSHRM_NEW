Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports System.IO


Public Class ApplicantsView
    Inherits System.Web.UI.Page
    Dim emailFile As String = ConfigurationManager.AppSettings("FileURL")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                Session("FromApplicantView") = "True"
                ViewState("PreviousPage") = Request.UrlReferrer

                pagetitle.InnerText = "Job ID: " & Session("JobID")
                Dim strUser As New DataSet
                strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Applications_Get_Applicant", Request.QueryString("applicantid"))
                lblID.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                aapplicant.Value = strUser.Tables(0).Rows(0).Item("Applicant").ToString
                aemailaddr.Value = strUser.Tables(0).Rows(0).Item("EmailAddress").ToString
                aphonenumber.Value = strUser.Tables(0).Rows(0).Item("MobileNo").ToString
                agender.Value = strUser.Tables(0).Rows(0).Item("Gender").ToString
                aspecialisation.Value = strUser.Tables(0).Rows(0).Item("FieldType").ToString
                adob.Value = strUser.Tables(0).Rows(0).Item("DOB").ToString
                aeducation.Value = strUser.Tables(0).Rows(0).Item("Education").ToString
                anationality.Value = strUser.Tables(0).Rows(0).Item("Nationality").ToString
                askill.Value = strUser.Tables(0).Rows(0).Item("Skill").ToString
                aexpyear.Value = strUser.Tables(0).Rows(0).Item("Experience").ToString
                btncoverletter.Attributes.Add("title", strUser.Tables(0).Rows(0).Item("coverlettername").ToString)
                btnresume.Attributes.Add("title", strUser.Tables(0).Rows(0).Item("CVname").ToString)
                btncertificate.Attributes.Add("title", strUser.Tables(0).Rows(0).Item("certname").ToString)
                btnofferletter.Attributes.Add("title", strUser.Tables(0).Rows(0).Item("filepath").ToString)
                agrade.Value = strUser.Tables(0).Rows(0).Item("academicgrade").ToString
                aaddress.Value = strUser.Tables(0).Rows(0).Item("ResidentAddress").ToString
                amaritalstat.Value = strUser.Tables(0).Rows(0).Item("MaritalStatus").ToString
                lblrecruited.Text = strUser.Tables(0).Rows(0).Item("recruited").ToString
                lblofferpath.Text = strUser.Tables(0).Rows(0).Item("filepath").ToString

                lblCoverLetter.Text = strUser.Tables(0).Rows(0).Item("coverlettername").ToString
                lblResume.Text = strUser.Tables(0).Rows(0).Item("CVname").ToString
                lblCertificate.Text = strUser.Tables(0).Rows(0).Item("certname").ToString
                lblofferpath.Text = strUser.Tables(0).Rows(0).Item("filepath").ToString

                If lblrecruited.Text.ToLower = "yes" Then
                    btnupdate.Disabled = True
                End If

                If lblofferpath.Text.Trim = "" Then
                    btnofferletter.Visible = False
                End If
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnclose_Click(sender As Object, e As EventArgs)
        Try
            'Response.Write("<script language='javascript'> { self.close() }</script>")
            If ViewState("PreviousPage") IsNot Nothing Then
                Response.Redirect(ViewState("PreviousPage").ToString())
                Session("FromApplicantView") = "True"
            Else
                Response.Write("<script language='javascript'> { self.close() }</script>")
            End If
        Catch ex As Exception
        End Try
    End Sub



    Protected Sub btnSend_Click(sender As Object, e As EventArgs)
        Try
            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Applications_Update_State", aemailaddr.Value, Session("JobID"), "ShortListed", "Yes")

            Process.loadalert(divalert, msgalert, aapplicant.Value & " Successfully shortlisted", "success")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
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
            Dim lblstatus As String = ""
            If lblResume.Text.Trim = "" Then
                lblstatus = "No Resume to download for applicant"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
            Else

                Dim dt As DataTable = Process.SearchData("Recruit_Applications_Get_Applicant", lblID.Text)
                If dt IsNot Nothing Then
                    'downloadCV(dt)
                    'downloadFile(CType(dt.Rows(0)("cvfile"), Byte()), dt.Rows(0)("cvtype").ToString(), dt.Rows(0)("cvname").ToString())
                    Dim fileName As String = dt.Rows(0)("cvname").ToString()
                    Dim filePath As String = Server.MapPath(emailFile & fileName)
                    Response.ContentType = ContentType
                    Response.AppendHeader("Content-Disposition", ("attachment; filename=" + Path.GetFileName(filePath)))
                    Response.WriteFile(filePath)
                    Response.End()
                End If

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub lnkCoverLetter_Click(sender As Object, e As EventArgs)
        Try
            Dim lblstatus As String = ""
            If lblCoverLetter.Text.Trim = "" Then
                lblstatus = "No cover letter to download for applicant"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
            Else

                Dim dt As DataTable = Process.SearchData("Recruit_Applications_Get_Applicant", lblID.Text)
                If dt IsNot Nothing Then
                    'downloadCoverLetter(dt)
                    'downloadFile(CType(dt.Rows(0)("coverletterfile"), Byte()), dt.Rows(0)("coverlettertype").ToString(), dt.Rows(0)("coverlettername").ToString())
                    Dim fileName As String = dt.Rows(0)("coverlettername").ToString()
                    Dim filePath As String = Server.MapPath(emailFile & fileName)
                    Response.ContentType = ContentType
                    Response.AppendHeader("Content-Disposition", ("attachment; filename=" + Path.GetFileName(filePath)))
                    Response.WriteFile(filePath)
                    Response.End()
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
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
            Else
                Dim dt As DataTable = Process.SearchData("Recruit_Applications_Get_Applicant", lblID.Text)
                If dt IsNot Nothing Then
                    'downloadCoverLetter(dt)
                    'downloadFile(CType(dt.Rows(0)("certfile"), Byte()), dt.Rows(0)("certtype").ToString(), dt.Rows(0)("certname").ToString())
                    Dim fileName As String = dt.Rows(0)("certname").ToString()
                    Dim filePath As String = Server.MapPath(emailFile & fileName)
                    Response.ContentType = ContentType
                    Response.AppendHeader("Content-Disposition", ("attachment; filename=" + Path.GetFileName(filePath)))
                    Response.WriteFile(filePath)
                    Response.End()
                End If
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub lnkofferletter_Click(sender As Object, e As EventArgs)
        Try
            Dim lblstatus As String = ""
            'Process.downloadFile(lblofferpath.Text)
            Dim fileName As String = lblofferpath.Text
            Dim filePath As String = Server.MapPath(emailFile & fileName)
            Response.ContentType = ContentType
            Response.AppendHeader("Content-Disposition", ("attachment; filename=" + Path.GetFileName(filePath)))
            Response.WriteFile(filePath)
            Response.End()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
End Class