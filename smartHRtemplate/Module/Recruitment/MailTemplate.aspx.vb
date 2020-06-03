Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Microsoft.Reporting.WebForms
Imports System.IO


Public Class MailTemplate
    Inherits System.Web.UI.Page
    Dim emailFile As String = ConfigurationManager.AppSettings("EmailURL")
    Dim recruitid As String
    Private Sub PromotionTable(id As String)
        Dim dtEarning As New DataTable
        dtEarning = Process.SearchData("Recruitment_Promotion_Get", id)
        lblpath.Text = Server.MapPath(emailFile & "Promotion" & id & ".pdf")
        lnkattach.InnerText = "Promotion_" & id & ".pdf"
        GeneratePromotionLetter(dtEarning, Process.GetData("general_info_get"), Server.MapPath(emailFile & "Promotion" & id & ".pdf"))
    End Sub
    Private Sub GeneratePromotionLetter(dtearn As DataTable, logos As DataTable, ByVal savePath As String)
        Dim ReportViewer1 As New ReportViewer
        ReportViewer1.ProcessingMode = ProcessingMode.Local
        ReportViewer1.SizeToReportContent = True
        'ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Module/Reports/Performance/PromotionLetter.rdlc")
        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Module/Reports/Performance/PromotionLetter.rdlc")
        Dim _rsource As New ReportDataSource("promotion", dtearn)
        Dim _rsource2 As New ReportDataSource("logo", logos)
        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.LocalReport.DataSources.Add(_rsource)
        ReportViewer1.LocalReport.DataSources.Add(_rsource2)
        ReportViewer1.LocalReport.Refresh()
        Dim Bytes() As Byte = ReportViewer1.LocalReport.Render("PDF", "", Nothing, Nothing, Nothing, Nothing, Nothing)

        If File.Exists(savePath) Then
            File.Delete(savePath)
        End If

        Using Stream As New FileStream(savePath, FileMode.Create)
            Stream.Write(Bytes, 0, Bytes.Length)
        End Using
        Session("rptAttachment") = savePath
        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruitment_Promotion_Update_Location", Request.QueryString("id"), savePath)
    End Sub
    Private Sub OfferTable(id As String)
        Dim dtEarning As New DataTable
        dtEarning = Process.SearchData("Recruit_Applicant_Get_Profile", id)
        lblpath.Text = Server.MapPath(emailFile & "Offer" & id & ".pdf")
        lnkattach.InnerText = "Offer_" & id & ".pdf"
        GenerateOfferLetter(dtEarning, Process.GetData("general_info_get"), Server.MapPath(emailFile & "Offer" & id & ".pdf"))
    End Sub
    Private Sub GenerateOfferLetter(dtearn As DataTable, logos As DataTable, ByVal savePath As String)
        Dim ReportViewer1 As New ReportViewer
        ReportViewer1.ProcessingMode = ProcessingMode.Local
        ReportViewer1.SizeToReportContent = True
        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Module/Reports/Performance/JobOffer.rdlc")
        Dim _rsource As New ReportDataSource("joboffer", dtearn)
        Dim _rsource2 As New ReportDataSource("logo", logos)
        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.LocalReport.DataSources.Add(_rsource)
        ReportViewer1.LocalReport.DataSources.Add(_rsource2)
        ReportViewer1.LocalReport.Refresh()
        Dim Bytes() As Byte = ReportViewer1.LocalReport.Render("PDF", "", Nothing, Nothing, Nothing, Nothing, Nothing)

        If File.Exists(savePath) Then
            File.Delete(savePath)
        End If

        Using Stream As New FileStream(savePath, FileMode.Create)
            Stream.Write(Bytes, 0, Bytes.Length)
        End Using
        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Applications_Offer_Location", Session("applicantidstr"), savePath)
        Session("rptAttachment") = savePath
    End Sub
    Private Sub ConfirmationTable(id As String)
        Dim dtEarning As New DataTable
        dtEarning = Process.SearchData("Recruit_Confirmation_Get", id)
        lblpath.Text = Server.MapPath(emailFile & "ConfirmationLetter" & id & ".pdf")
        lnkattach.InnerText = "ConfirmationLetter_" & id & ".pdf"
        GenerateConfirmationLetter(dtEarning, Process.GetData("general_info_get"), Server.MapPath(emailFile & "ConfirmationLetter" & id & ".pdf"))
    End Sub
    Private Sub GenerateConfirmationLetter(dtearn As DataTable, logos As DataTable, ByVal savePath As String)
        Dim ReportViewer1 As New ReportViewer
        ReportViewer1.ProcessingMode = ProcessingMode.Local
        ReportViewer1.SizeToReportContent = True
        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Module/Reports/Performance/ConfirmationLetter.rdlc")
        Dim _rsource As New ReportDataSource("confirm", dtearn)
        Dim _rsource2 As New ReportDataSource("logo", logos)
        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.LocalReport.DataSources.Add(_rsource)
        ReportViewer1.LocalReport.DataSources.Add(_rsource2)
        ReportViewer1.LocalReport.Refresh()
        Dim Bytes() As Byte = ReportViewer1.LocalReport.Render("PDF", "", Nothing, Nothing, Nothing, Nothing, Nothing)

        If File.Exists(savePath) Then
            File.Delete(savePath)
        End If

        Using Stream As New FileStream(savePath, FileMode.Create)
            Stream.Write(Bytes, 0, Bytes.Length)
        End Using
        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Confirmation_Update_Location", Request.QueryString("id"), savePath)
        Session("rptAttachment") = savePath
    End Sub
    Private Sub JobOfferTemplete()
        Dim strUser As New DataSet
        strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Applications_Get_Applicant", Request.QueryString("appid"))
        lblid.Text = Request.QueryString("appid").ToString
        Dim candidate As String = strUser.Tables(0).Rows(0).Item("Applicant").ToString
        Dim email As String = strUser.Tables(0).Rows(0).Item("EmailAddress").ToString
        Dim jobtitle As String = strUser.Tables(0).Rows(0).Item("Title").ToString
        Dim company As String = strUser.Tables(0).Rows(0).Item("company").ToString
        lblcompany.Text = strUser.Tables(0).Rows(0).Item("company").ToString
        Session("applicantidstr") = strUser.Tables(0).Rows(0).Item("applicantid").ToString

        OfferTable(Request.QueryString("appid"))
        aemail.Value = email
        asubject.Value = "Offer of Employment from " & company

        Dim template As New StringBuilder()
        template.AppendLine("Dear @CandidateName,")
        template.AppendLine()
        template.AppendLine("We are pleased to offer you the position of @JobTitle with us here at @Company where we hope you will enjoy your role and make a significant contribution to the success of the business.")
        template.AppendLine()
        template.AppendLine("Please do come by our office to pick up your offer letter.")
        template.AppendLine("Your employment if you do accept our offer will commence on ../../..")
        template.AppendLine()
        template.AppendLine("Once again congratulations")
        template.AppendLine()
        template.AppendLine("@Company Team")
        amessage.value = template.ToString.Replace("@CandidateName", candidate).Replace("@JobTitle", jobtitle).Replace("@Company", company)
    End Sub
    Private Sub Confirmation()
        Dim strUser As New DataSet
        strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Confirmation_Get", Request.QueryString("id"))
        recruitid = strUser.Tables(0).Rows(0).Item("recruitid").ToString
        Dim employee As String = strUser.Tables(0).Rows(0).Item("recruitname").ToString
        lblid.Text = Request.QueryString("id").ToString
        Dim email As String = strUser.Tables(0).Rows(0).Item("recruitmail").ToString
        Dim confirmdatedate As String = Request.QueryString("date")
        Dim jobtitle As String = strUser.Tables(0).Rows(0).Item("recruittitle").ToString
        Dim supervisor As String = strUser.Tables(0).Rows(0).Item("SupervisorName").ToString        
        aemail.Value = email
        ConfirmationTable(Request.QueryString("id"))
        asubject.Value = "Your Employment Confirmation"
        lblempid.Text = strUser.Tables(0).Rows(0).Item("empid").ToString
        Dim template As New StringBuilder()
        template.AppendLine("Dear @CandidateName,")
        template.AppendLine()
        template.AppendLine("We are pleased to inform you that you have been confirmed. ")
        template.AppendLine()
        template.AppendLine("Find attached your confirmation letter.")
        template.AppendLine()
        template.AppendLine("Thank you.")
        template.AppendLine()
        template.AppendLine("@Company Team")
        amessage.value = template.ToString.Replace("@CandidateName", employee).Replace("@date", confirmdatedate).Replace("@jobtitle", jobtitle).Replace("@supervisor", supervisor).Replace("@Company", Process.GetCompanyByEmpID(Request.QueryString("empid")))
    End Sub
    Private Sub Promotion()
        PromotionTable(Request.QueryString("id"))

        Dim strUser As New DataSet
        strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruitment_Promotion_Get", Request.QueryString("id"))
        lblid.Text = Request.QueryString("id").ToString
        Dim employee As String = strUser.Tables(0).Rows(0).Item("empName").ToString
        Dim email As String = strUser.Tables(0).Rows(0).Item("empEmail").ToString
        Dim confirmdatedate As String = Process.DDMONYYYY(CDate(strUser.Tables(0).Rows(0).Item("effectivedate")))
        Dim jobtitle As String = strUser.Tables(0).Rows(0).Item("jobtitle").ToString
        Dim grade As String = strUser.Tables(0).Rows(0).Item("jobgrade").ToString
        Dim supervisor As String = strUser.Tables(0).Rows(0).Item("SupervisorName").ToString
        Dim dept As String = strUser.Tables(0).Rows(0).Item("dept").ToString
        lblmgr.Text = strUser.Tables(0).Rows(0).Item("initiatorid").ToString
        lblempid.Text = strUser.Tables(0).Rows(0).Item("empid").ToString
        aemail.Value = email
        asubject.Value = "Promotion Letter "
        Dim template As New StringBuilder()
        template.AppendLine("Dear @CandidateName,")
        template.AppendLine()
        template.AppendLine("We are pleased to inform you that you have been promoted. ")
        template.AppendLine()
        template.AppendLine("Find attached your promotion letter.")
        template.AppendLine()
        template.AppendLine("Thank you")
        template.AppendLine()
        template.AppendLine("@Company Team")
        amessage.value = template.ToString.Replace("@CandidateName", employee).Replace("@date", confirmdatedate).Replace("@jobgrade", grade).Replace("@jobtitle", jobtitle).Replace("@supervisor", supervisor).Replace("@Company", Process.GetCompanyName(dept))
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                Session("rptAttachment") = ""
                If Request.QueryString("template") = "joboffer" Then
                    divjoboffer.Visible = True
                    divpromotion.Visible = False
                    JobOfferTemplete()
                ElseIf Request.QueryString("template") = "confirmation" Then
                    divjoboffer.Visible = False
                    divpromotion.Visible = False
                    Confirmation()
                ElseIf Request.QueryString("template") = "promotion" Then
                    divjoboffer.Visible = True
                    divjoboffer.Visible = False
                    Promotion()
                End If
            End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub btnback_Click(sender As Object, e As EventArgs)
        Try
            If Request.QueryString("template") = "promotion" Then
                Response.Redirect("promotionsupdate?id=" & lblid.Text, True)
            End If
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub btnSend_Click(sender As Object, e As EventArgs)
        Try
            Dim lblstatus As String = ""
            Process.loadalert(divalert, msgalert, lblstatus, "success")

            Dim csvPath As String = ""
            Dim csvPath2 As String = ""
            Dim csvPath3 As String = ""

            If file1.PostedFile IsNot Nothing And file1.PostedFile.ContentLength > 0 Then
                'To create a PostedFile
                csvPath2 = Server.MapPath(Process.FileURL) + Path.GetFileName(file1.PostedFile.FileName)
                If File.Exists(csvPath2) = True Then
                    File.Delete(csvPath2)
                End If
                file1.PostedFile.SaveAs(csvPath2)
            Else
                'Exit Sub
            End If

            If file2.PostedFile IsNot Nothing And file2.PostedFile.ContentLength > 0 Then
                'To create a PostedFile
                csvPath3 = Server.MapPath(Process.FileURL) + Path.GetFileName(file2.PostedFile.FileName)
                If File.Exists(csvPath3) = True Then
                    File.Delete(csvPath3)
                End If
                file2.PostedFile.SaveAs(csvPath3)
            Else
                'Exit Sub
            End If


            'If Not file1.PostedFile Is Nothing And file1.PostedFile.ContentLength < 0 = True Then
            '    'To create a PostedFile
            '    csvPath = Server.MapPath(Process.FileURL) + Path.GetFileName(file1.PostedFile.FileName)
            '    If File.Exists(csvPath) = True Then
            '        File.Delete(csvPath)
            '    End If
            '    file1.PostedFile.SaveAs(csvPath)
            'Else
            '    'Exit Sub
            'End If

            'If Not file2.PostedFile Is Nothing And file2.PostedFile.ContentLength < 0 Then
            '    'To create a PostedFile
            '    csvPath2 = Server.MapPath(Process.FileURL) + Path.GetFileName(file2.PostedFile.FileName)
            '    If File.Exists(csvPath2) = True Then
            '        File.Delete(csvPath2)
            '    End If
            '    file2.PostedFile.SaveAs(csvPath2)
            'Else
            '    'Exit Sub
            'End If

            'If Not file3.PostedFile Is Nothing And file3.PostedFile.ContentLength < 0 Then
            '    'To create a PostedFile
            '    csvPath3 = Server.MapPath(Process.FileURL) + Path.GetFileName(file3.PostedFile.FileName)
            '    If File.Exists(csvPath3) = True Then
            '        File.Delete(csvPath3)
            '    End If
            '    file3.PostedFile.SaveAs(csvPath3)
            'Else
            '    'Exit Sub
            'End If



            Dim company As String = ""
            If Request.QueryString("template") = "joboffer" Then
                company = lblcompany.Text
            End If

            'If lblpath.Text.Trim <> "" Then
            '    csvPath = lblpath.Text & ";" & csvPath
            'End If

            'If csvPath2.Trim <> "" Then
            '    csvPath = lblpath.Text & ";" & csvPath & ";" & csvPath2
            'End If

            'If csvPath3.Trim <> "" Then
            '    csvPath = lblpath.Text & ";" & csvPath & ";" & csvPath2 & ";" & csvPath3
            'End If

            If lblpath.Text.Trim <> "" Then
                csvPath = lblpath.Text
            End If

            If csvPath2.Trim <> "" And lblpath.Text.Trim <> "" Then
                csvPath = lblpath.Text & ";" & csvPath2
            End If

            If csvPath2.Trim <> "" And lblpath.Text.Trim = "" Then
                csvPath = csvPath2
            End If

            If csvPath3.Trim <> "" And lblpath.Text.Trim <> "" And lblpath.Text.Trim <> "" Then
                csvPath = lblpath.Text & ";" & csvPath2 & ";" & csvPath3
            End If

            If csvPath3.Trim <> "" And lblpath.Text.Trim <> "" And lblpath.Text.Trim = "" Then
                csvPath = csvPath2 & ";" & csvPath3
            End If

            If csvPath3.Trim <> "" And lblpath.Text.Trim = "" And lblpath.Text.Trim = "" Then
                csvPath = csvPath3
            End If

            If Process.SendEmail("", company, aemail.Value, asubject.Value, amessage.Value, csvPath, False) = True Then

                'ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Mail successfully sent" + "')", True)
                'txtTemplate.Enabled = False
                lblstatus = "Mail successfully sent"
                Process.loadalert(divalert, msgalert, lblstatus, "success")


                If Request.QueryString("template") = "joboffer" Then
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Applications_Update_State", Session("ApplcantID"), Session("JobID"), "OfferLetterSent", "Yes")
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Applications_Update_Stat", 0, Session("ApplcantID"), Session("JobID"), "Hired")
                ElseIf Request.QueryString("template") = "confirmation" Then
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Confirmation_Get", Request.QueryString("id"))
                    recruitid = strUser.Tables(0).Rows(0).Item("recruitid").ToString

                    Process.MailNotification(lblempid.Text, Process.MailEmployeeConfirm, asubject.Value, amessage.Value, aemail.Value, company, "", recruitid, "", csvPath)
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Confirmation_Update_Letter_Stat", Request.QueryString("id"))
                ElseIf Request.QueryString("template") = "promotion" Then
                    Process.MailNotification(lblempid.Text, Process.MailPromotion, asubject.Value, amessage.Value, aemail.Value, company, "", lblempid.Text, "", csvPath)
                    Process.Staff_Promotion_Complete(lblempid.Text, lblmgr.Text, Session("rptAttachment"), Process.ApplicationURL + "/" + Process.GetMailLink("ADMPROMOTION", 2))
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruitment_Promotion_Update_Letter_Stat", Request.QueryString("id"))
                End If

            Else
                Process.loadalert(divalert, msgalert, Session("exception"), "danger")
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub



    Protected Sub lnkattachment_Click(sender As Object, e As EventArgs)
        Try

            Process.downloadFile(lblpath.Text)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnremove_Click(sender As Object, e As EventArgs) Handles btnremove.Click
        Try
            Dim lblstatus As String = ""

            Dim confirmValue As String = Request.Form("confirm_value1")
            If confirmValue = "No" Then
                lblstatus = "Remove Cancelled"
                Process.loadalert(divalert, msgalert, lblstatus, "info")
            Else
                lblpath.Text = ""
                btnremove.Visible = False
                lnkattach.Visible = False
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class