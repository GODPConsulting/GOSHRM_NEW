Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Telerik.Web.UI
Public Class PasswordRecovery
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            imgProfile.ImageUrl = "~/Module/Admin/Organisation/CompanyLogo.ashx"

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

  

    Protected Sub btnLogin_Click(sender As Object, e As EventArgs)
        Try
            If Request.QueryString("recovery") <> "staff" Then
                MultiView1.ActiveViewIndex = 1
                If Process.Applicant_Recovery(Request.Form("uid")) = True Then
                    fstatus.InnerText = "Login credential has been sent to " & Request.Form("uid")
                Else
                    fstatus.InnerText = Request.Form("uid") & " doesn't exist in the system"
                End If
            Else
                MultiView1.ActiveViewIndex = 1
                If Process.Employee_Recovery(Request.Form("uid")) = True Then
                    fstatus.InnerText = "Login credential has been sent to " & Request.Form("uid")
                Else
                    fstatus.InnerText = Request.Form("uid") & " doesn't exist in the system"
                End If
            End If

            
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

   
End Class