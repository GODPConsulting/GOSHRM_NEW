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
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "applicant_lastlogin_update", Request.Form("uid"))
                    Response.Redirect("~/Module/Recruitment/Applications/Vacancies", True)
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
            Response.Redirect("~/Module/Recruitment/Applications/CandidateProfile")
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