Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Telerik.Web.UI
Public Class ApplicationLogin
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Session("ApplicantJobID") = Request.QueryString("id")
            Session("stage") = Request.QueryString("stage")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub



    Protected Sub btnLogin_Click(sender As Object, e As EventArgs)
        Try
            Session("ApplcantID") = Request.Form("uid")

            If Process.ApplicantAuthen(Request.Form("uid"), Process.Encrypt(Request.Form("pwd"))) = True Then
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "recruit_applicant_lastlogin_update", Request.Form("uid"), Session("ApplicantJobID"))
                Response.Redirect("~/Module/Recruitment/OnlineTest/TestStartup.aspx", True)

            Else
                Process.loadalert(divalert, msgalert, "Login failed, invalid user or password", "danger")
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


End Class