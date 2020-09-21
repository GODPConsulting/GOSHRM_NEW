Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports System.IO
Public Class Verified
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            imgProfile.ImageUrl = "~/Module/Admin/Organisation/CompanyLogo.ashx"
            If Not Me.IsPostBack Then
                If Request.QueryString("id") IsNot Nothing Then
                    Dim Star = Request.QueryString("id")
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Applicant_Verify", Star)
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub lnkOldUser(sender As Object, e As EventArgs)
        Try
            Response.Redirect("~/Module/Recruitment/Applications/ApplicantLogin")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub proceedUser(Sender As Object, e As EventArgs)
        Try

            If Request.QueryString("id") IsNot Nothing Then
                Dim Star = Request.QueryString("id")

                Dim strUser As New DataSet
                strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Applicant_Authen2", Star)
                Dim VerificationStatus As Boolean = strUser.Tables(0).Rows(0).Item("Virified")
                Dim FirstUserCheck1 = strUser.Tables(0).Rows(0).Item("certname").ToString
                Dim FirstUserCheck2 = strUser.Tables(0).Rows(0).Item("coverlettername").ToString
                Dim FirstUserCheck3 = strUser.Tables(0).Rows(0).Item("certname").ToString
                Dim FirstUserCheck4 = strUser.Tables(0).Rows(0).Item("Skill").ToString
                Dim Email = strUser.Tables(0).Rows(0).Item("EmailAddress").ToString
                Dim Pwd = strUser.Tables(0).Rows(0).Item("Pwd").ToString

                Dim lblStatus As String = ""
                Session("ApplcantID") = Email
                Dim ipaddress As String
                ipaddress = Request.ServerVariables("HTTP_X_FORWARDED_FOR")
                If ipaddress = "" Or ipaddress Is Nothing Then
                    ipaddress = Request.ServerVariables("REMOTE_ADDR")
                End If

                Session("IPAddress") = ipaddress
                If Process.ApplicantAuthen(Email, Pwd) = True Then

                    If (VerificationStatus = False) Then
                        lblStatus = "Please Comfirm Your Email"
                        Process.loadalert(divalert, msgalert, lblStatus, "danger")
                    Else
                        If (FirstUserCheck1 = "" And FirstUserCheck2 = "" And FirstUserCheck3 = "" And FirstUserCheck4 = "") Then
                            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "applicant_lastlogin_update", Email)
                            Response.Redirect("~/Module/Recruitment/Applications/CandidateProfile", True)
                        Else

                            Response.Redirect("~/Module/Recruitment/Applications/ApplicantLogin")
                        End If
                    End If
                End If
            Else
                    Response.Redirect("~/Module/Recruitment/Applications/ApplicantLogin")
            End If
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