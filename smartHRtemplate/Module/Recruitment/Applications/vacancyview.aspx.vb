Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Collections.Generic
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO

Public Class vacancyviews
    Inherits System.Web.UI.Page
    Public yaer As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not Me.IsPostBack Then
                'Image logo
                Dim strUsers As New DataSet
                strUsers = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "general_info_get")
                If IsDBNull(strUsers.Tables(0).Rows(0).Item("imgLogo")) Or strUsers.Tables(0).Rows(0).Item("imgLogo").ToString.Trim = "" Then
                    imgProfile.ImageUrl = imgClear.ImageUrl
                Else
                    imgProfile.ImageUrl = "~/Module/Admin/Organisation/CompanyLogo.ashx"
                End If

                'Current Year
                yaer = Convert.ToString(Date.Now.Year)

                Session("PreviousPage") = Request.UrlReferrer
                If Request.QueryString("Code") IsNot Nothing Then
                    Session("JobID") = Request.QueryString("Code")
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Job_Post_get", Request.QueryString("Code"))

                    aspecialisation.InnerText = strUser.Tables(0).Rows(0).Item("specialization").ToString
                    lblID.Text = strUser.Tables(0).Rows(0).Item("code").ToString
                    ajobtitle.InnerText = strUser.Tables(0).Rows(0).Item("Title").ToString
                    ajobtype.InnerText = strUser.Tables(0).Rows(0).Item("Type").ToString
                    ajobdesc.InnerText = strUser.Tables(0).Rows(0).Item("JobDescription").ToString
                    amineducation.InnerText = strUser.Tables(0).Rows(0).Item("EducationLevel").ToString
                    aexplevel.InnerText = strUser.Tables(0).Rows(0).Item("ExperienceLevel").ToString
                    askill.InnerText = strUser.Tables(0).Rows(0).Item("Skills").ToString
                    acompany.InnerText = strUser.Tables(0).Rows(0).Item("company").ToString
                    acountry.InnerText = strUser.Tables(0).Rows(0).Item("Country").ToString
                    alocation.InnerText = strUser.Tables(0).Rows(0).Item("Location").ToString
                    aage.InnerText = strUser.Tables(0).Rows(0).Item("StartAgeRange").ToString & " - " & strUser.Tables(0).Rows(0).Item("EndAgeRange").ToString
                    'txtCurrency.InnerText = strUser.Tables(0).Rows(0).Item("Currency").ToString
                    aexpyears.InnerText = strUser.Tables(0).Rows(0).Item("experience1").ToString & " - " & strUser.Tables(0).Rows(0).Item("experience2").ToString
                    aclosingdate.InnerText = CDate(strUser.Tables(0).Rows(0).Item("closingdate").ToString).ToLongDateString
                    txtHasAptitude.Text = strUser.Tables(0).Rows(0).Item("TestOnline").ToString
                    'amingrade.InnerText = strUser.Tables(0).Rows(0).Item("minacademicgrade").ToString
                    'ahighschoolreq.InnerText = "Minimum Requirement Grade: " & strUser.Tables(0).Rows(0).Item("minentrylevelgrade").ToString

                    'If amingrade.InnerText.ToLower = "n/a" Or amingrade.InnerText.Trim = "" Then
                    '    divgrade.Visible = False

                    'End If

                    'Dim strDiscipline As New DataSet
                    'strDiscipline = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Job_Post_Discipline_Get_All", lblID.Text)
                    'If strDiscipline.Tables(0).Rows.Count > 0 Then
                    '    For i As Integer = 0 To strDiscipline.Tables(0).Rows.Count - 1
                    '        If i = 0 Then
                    '            adiscipline.Value = strDiscipline.Tables(0).Rows(i).Item("items").ToString
                    '        Else
                    '            adiscipline.Value = adiscipline.Value & vbNewLine & strDiscipline.Tables(0).Rows(i).Item("items").ToString
                    '        End If
                    '    Next
                    'End If

                    'If adiscipline.Value.ToLower = "n/a" Or adiscipline.Value.Trim = "" Then
                    '    divdiscipline.Visible = False
                    'End If

                    'Dim strOLSubject As New DataSet
                    'strOLSubject = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Job_Post_OLSubject_Get_All", lblID.Text)
                    'If strOLSubject.Tables(0).Rows.Count > 0 Then
                    '    For i As Integer = 0 To strOLSubject.Tables(0).Rows.Count - 1
                    '        If i = 0 Then
                    '            ahighqualification.Value = strOLSubject.Tables(0).Rows(i).Item("items").ToString
                    '        Else
                    '            ahighqualification.Value = ahighqualification.Value & vbNewLine & strOLSubject.Tables(0).Rows(i).Item("items").ToString
                    '        End If
                    '    Next
                    'End If

                    Dim strOLSubject As New DataSet
                    Dim sss As StringBuilder = New StringBuilder("")
                    strOLSubject = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Job_Post_OLSubject_Get_All", lblID.Text)
                    If strOLSubject.Tables(0).Rows.Count > 0 Then
                        For i As Integer = 0 To strOLSubject.Tables(0).Rows.Count - 1
                            Dim subjects As String = strOLSubject.Tables(0).Rows(i).Item("items").ToString
                            sss.Append("<p> *" + subjects + " </p>")
                        Next
                    Else
                        sss.Append("<p> Minimum 5 O'level Credits in not more than two sittings </p>")
                        'olevelsubjectlabel.InnerText = ""
                    End If
                    Dim ww As String = sss.ToString()
                    olevelsubject.InnerHtml = ww
                    'If ahighqualification.Value.ToLower = "n/a" Or ahighqualification.Value.Trim = "" Then
                    '    divhighschool.Visible = False
                    'End If
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub


    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            ' Response.Redirect("~/Module/Recruitment/Applications/Vacancies", True)

            If Session("ApplcantID") Is Nothing Then
                Response.Redirect("~/Module/Recruitment/Applications/ApplicantLogin.aspx", True)
            Else
                Dim AlreadyApplied As Boolean = CBool(SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Recruit_Applications_Confirm_Applicant", lblID.Text, Session("appid")))

                If AlreadyApplied = True Then
                    Process.loadalert(divalert, msgalert, "You have already applied for this position", "danger")
                    Exit Sub
                End If

                Process.Applicant_Notification(acompany.InnerText, Session("ApplcantID"), ajobtitle.InnerText, Session("ApplicantName"))
                'MultiView1.ActiveViewIndex = 1
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Applications_Update", lblID.Text, Session("appid"))

                Dim strOnlineCount As New DataSet
                strOnlineCount = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "select * from Recruit_Job_Test where StageNo = 1 and online = 'Yes' and JobID = " & lblID.Text)
                Dim onlinecount As Integer = strOnlineCount.Tables(0).Rows.Count

                If txtHasAptitude.Text.ToUpper = "YES" Then ' Check Apitude Test exists
                    If onlinecount > 0 Then 'Check if Stage 1 is online 
                        If MatchApplicant() > 0 Then
                            'Password
                            Dim URL As String = ""

                            URL = Process.ApplicationURL
                            Dim TestURL As String = ConfigurationManager.AppSettings("TestURL")
                            'lblHeader.Text
                            Process.OnlineTest_Notification(Session("ApplcantID"), acompany.InnerText, ajobtitle.InnerText, Session("ApplicantName"), "", URL & TestURL & lblID.Text & "&stage=1")
                        Else
                            Dim strJobInfo As New DataSet
                            strJobInfo = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Job_Post_Get", lblID.Text)
                            If strJobInfo.Tables(0).Rows.Count > 0 Then
                                Dim edulevel As String = strJobInfo.Tables(0).Rows(0).Item("EducationLevel").ToString
                                Dim explevel As String = strJobInfo.Tables(0).Rows(0).Item("specialization").ToString
                                Dim startage As Integer = strJobInfo.Tables(0).Rows(0).Item("StartAgeRange").ToString
                                Dim endage As Integer = strJobInfo.Tables(0).Rows(0).Item("EndAgeRange").ToString
                                Dim expstart As Integer = strJobInfo.Tables(0).Rows(0).Item("experience1").ToString
                                Dim expend As Integer = strJobInfo.Tables(0).Rows(0).Item("experience2").ToString
                                Process.OnlineTest_Unsuccessful_Criteria(Session("ApplcantID"), acompany.InnerText, ajobtitle.InnerText, Session("ApplicantName"), edulevel, explevel, startage, endage, expstart, expend)
                            End If
                        End If
                    Else
                        If MatchApplicant() > 0 Then 'Add to Level 1 Shortlist
                            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Job_Test_Shortlist_Update", lblID.Text, Session("AppID"), 0, 1, 0, Session("ApplcantID"), 0, Date.Now, Date.Now, Date.Now.TimeOfDay, "No")
                        End If
                    End If
                Else

                    'Short List Candiadate if it meets criteria and requires no online test
                    If MatchApplicant() > 0 Then
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Applications_Update_State", Session("ApplcantID"), lblID.Text, "ShortListed", "Yes")
                    End If
                End If
            End If

            'Success Message
            Process.loadalert(divalert, msgalert, "You have successfully applied for this Job", "success")
            testmode.Style.Add("display", "none")
            Button1.Visible = True
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Function MatchApplicant() As Integer
        Dim Datas As New DataTable
        Dim job As String = ""
        Dim gender As String = ""
        job = lblID.Text

        Return SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "recruit_Applicant_Match", job, Session("appid"))
    End Function

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Response.Redirect("~/Module/Recruitment/Applications/myapplications")
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            Response.Redirect("~/Module/Recruitment/Applications/searchpage")
        Catch ex As Exception

        End Try
    End Sub
End Class