Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Telerik.Web.UI
Public Class ApplicantLogin
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            imgProfile.ImageUrl = "~/Module/Admin/Organisation/CompanyLogo.ashx"

            If Request.QueryString("new") IsNot Nothing Then                
                Process.loadalert(divalert, msgalert, "Profile successfully created, login to proceed with application", "success")
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")            
        End Try
    End Sub



    Protected Sub btnLogin_Click(sender As Object, e As EventArgs)
        Try
            Dim lblStatus As String = ""
            Session("ApplcantID") = Request.Form("uid")
            Dim ipaddress As String
            ipaddress = Request.ServerVariables("HTTP_X_FORWARDED_FOR")
            If ipaddress = "" Or ipaddress Is Nothing Then
                ipaddress = Request.ServerVariables("REMOTE_ADDR")
            End If

            Session("IPAddress") = ipaddress
            'Check for Authentication Mode

            If Process.ApplicantAuthen(Request.Form("uid"), Process.Encrypt(Request.Form("pwd"))) = True Then

                If Request.QueryString("id") IsNot Nothing Then
                    Session("ApplicantJobID") = Request.QueryString("id")
                    Session("stage") = Request.QueryString("stage")
                    Response.Redirect("~/Module/Recruitment/OnlineTest/TestStartup", True)
                Else
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Applicant_Get", Request.Form("uid"))
                    Dim VerificationStatus As Boolean = strUser.Tables(0).Rows(0).Item("Virified")
                    Dim FirstUserCheck1 = strUser.Tables(0).Rows(0).Item("certname").ToString
                    Dim FirstUserCheck2 = strUser.Tables(0).Rows(0).Item("coverlettername").ToString
                    Dim FirstUserCheck3 = strUser.Tables(0).Rows(0).Item("certname").ToString
                    Dim FirstUserCheck4 = strUser.Tables(0).Rows(0).Item("Skill").ToString
                    If (VerificationStatus = False) Then
                        lblStatus = "Please Comfirm Your Email"
                        Process.loadalert(divalert, msgalert, lblStatus, "danger")
                    Else
                        If (FirstUserCheck1 = "" And FirstUserCheck2 = "" And FirstUserCheck3 = "" And FirstUserCheck4 = "") Then
                            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "applicant_lastlogin_update", Request.Form("uid"))
                            Response.Redirect("~/Module/Recruitment/Applications/CandidateProfile", True)
                        Else
                            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "applicant_lastlogin_update", Request.Form("uid"))
                            Response.Redirect("~/Module/Recruitment/Applications/Vacancies", True)
                        End If
                    End If
                End If
            Else
                lblStatus = "Login failed, invalid user or password"
                Process.loadalert(divalert, msgalert, lblStatus, "danger")
            End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")            
        End Try
    End Sub

    Protected Sub lnkNewUser(sender As Object, e As EventArgs)
        Try
            Response.Redirect("~/Module/Recruitment/Applications/SignUp")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")  
        End Try
    End Sub
    Protected Sub lnkforgetpwd(sender As Object, e As EventArgs)
        Try
            Response.Redirect("~/Module/Recruitment/Applications/PasswordRecovery")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
End Class