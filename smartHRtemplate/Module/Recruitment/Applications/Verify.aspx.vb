Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports System.IO
Public Class WebForm1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            imgProfile.ImageUrl = "~/Module/Admin/Organisation/CompanyLogo.ashx"
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
    Protected Sub lnkforgetpwd(sender As Object, e As EventArgs)
        Try
            Response.Redirect("~/Module/Recruitment/Applications/PasswordRecovery")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub Btn_sendClick(sender As Object, e As EventArgs)
        ' If Not Me.IsPostBack Then
        If Request.QueryString("id") IsNot Nothing Then
                Dim Star = Request.QueryString("id")
                Dim Link = Process.ApplicationURL + "/Module/Recruitment/Applications/verified?id=" + Star
            Dim strUser As New DataSet
            Star = Star.ToString

            strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Job_Applicant_Profile_Verify", Star)
            Dim EmailAdress = strUser.Tables(0).Rows(0).Item("EmailAddress").ToString
            Dim FirstName = strUser.Tables(0).Rows(0).Item("FirstName").ToString
                Process.Verify_Recruit("company", EmailAdress, FirstName, Link)
            End If
        'End If

    End Sub
End Class