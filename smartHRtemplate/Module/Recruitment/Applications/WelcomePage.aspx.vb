Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO


Public Class WelcomePage
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                imgProfile.ImageUrl = "~/Module/Admin/Organisation/CompanyLogo.ashx"
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnSignIn_Click(sender As Object, e As EventArgs) Handles btnSignIn.Click
        Try
            Response.Redirect("~/Module/Recruitment/Applications/ApplicantLogin")
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnBrowseJob_Click(sender As Object, e As EventArgs) Handles btnBrowseJob.Click
        Try
            Response.Redirect("~/Module/Recruitment/Applications/Vacancies")
        Catch ex As Exception

        End Try
    End Sub
End Class