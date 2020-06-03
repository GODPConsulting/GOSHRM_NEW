Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.Web.Script.Serialization

Public Class Recruit
    Inherits System.Web.UI.MasterPage


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                If Session("ApplicantName") IsNot Nothing Then
                    divwelcome.InnerText = "Welcome, " & Session("ApplicantName")
                Else
                    btnLogin.Style.Add("display", "none")
                End If

                
            End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub Signout_Click(sender As Object, e As EventArgs)
        Try

            Session.Clear()
            Session.Abandon()
            FormsAuthentication.SignOut()
            Response.Redirect("~/Module/Recruitment/Applications/Applicantlogin", True)
            'FormsAuthentication.RedirectToLoginPage()
        Catch ex As Exception

        End Try
    End Sub
End Class