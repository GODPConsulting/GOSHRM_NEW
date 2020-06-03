Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Telerik.Web.UI
Public Class DualAuthentication
    Inherits System.Web.UI.Page
    Private Sub CheckApplicationAssements()
        Try
            Process.Training_Application_Assessment_AutoAlert()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "users_refresh")

            Dim strUser As New DataSet

            strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "User_LDAP_Setting_Get")
            If strUser.Tables(0).Rows.Count > 0 Then
                Dim dualMode As String = strUser.Tables(0).Rows(0).Item("dual_mode")
                If dualMode.ToString.ToLower = "email" Then
                    sentto.InnerText = Session("LoginEmail")
                Else
                    sentto.InnerText = Session("UserMobileNo")
                End If
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub



    Protected Sub btnLogin_Click(sender As Object, e As EventArgs)
        Try
            Dim strUser As New DataSet
            Dim loginSuccess As Boolean = False
            Dim changePwd As Boolean = CBool(Request.QueryString("change"))

            If Process.DualAuthen(Request.QueryString("id"), Request.Form("code")) Then
                loginSuccess = True
            Else
                Process.loadalert(divalert, msgalert, "Login failed, invalid user or password", "warning")
            End If

            If (loginSuccess) Then
                CheckApplicationAssements()
                If (changePwd) Then
                    If Request.QueryString("ReturnUrl") IsNot Nothing Then
                        Dim url As String = Request.QueryString("ReturnUrl").ToString
                        Response.Redirect(url, True)
                    Else
                        Response.Redirect("~/empdashboard", True)
                    End If
                Else
                    If Request.QueryString("ReturnUrl") IsNot Nothing Then
                        Dim url As String = Request.QueryString("ReturnUrl").ToString
                        Response.Redirect(url, True)
                    Else
                        Response.Redirect("~/ChangePassword", True)
                    End If
                End If

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
End Class