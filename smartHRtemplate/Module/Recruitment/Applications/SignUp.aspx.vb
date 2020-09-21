Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports System.IO
Public Class SignUp
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            imgProfile.ImageUrl = "~/Module/Admin/Organisation/CompanyLogo.ashx"
        Catch ex As Exception

        End Try

    End Sub
    Protected Sub Btn_sendClick(sender As Object, e As EventArgs)
        Try
            System.Threading.Thread.Sleep(300)
            Dim lblstatus As String = ""
            If aemailadd.Value.Trim.Contains("@") = False Or aemailadd.Value.Trim = "" Then
                lblstatus = "Enter a valid email address"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                aemailadd.Focus()
                Exit Sub
            End If
            'Check Existence

            Dim exists As Boolean = CBool(SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Recruit_Confirm_Applicant", aemailadd.Value.Trim))
                If exists = True Then
                    lblstatus = "Email Address already exists in our system!"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    aemailadd.Focus()
                    Exit Sub
                End If

            If apwd.Value.Trim <> aconfirmpwd.Value.Trim Then
                lblstatus = "Password mismatch!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                apwd.Focus()
                Exit Sub
            End If
            Dim Pwd As String = ""
            If apwd.Value.Trim <> "" Then
                Pwd = Process.Encrypt(apwd.Value.Trim)
            End If
            Dim Applicants As String = afirstnameadd.Value.Trim + "" + amiddlenameadd.Value.Trim + "" + alastnameadd.Value.Trim
            Dim Star = Guid.NewGuid.ToString()
            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Applicant_SignUp", aemailadd.Value.Trim, Pwd, Star, afirstnameadd.Value.Trim, amiddlenameadd.Value.Trim, alastnameadd.Value.Trim)
            Dim Link = Process.ApplicationURL + "/Module/Recruitment/Applications/verified?id=" + Star
            Process.Verify_Recruit("company", aemailadd.Value.Trim, afirstnameadd.Value.Trim, Link)

            Response.Redirect("~/Module/Recruitment/Applications/Verify?id=" + Star)


        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
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


End Class