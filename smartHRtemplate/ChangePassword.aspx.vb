Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Telerik.Web.UI
Public Class ChangePassword
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'If Not Me.IsPostBack Then
            '    Session.Clear()
            '    Session.Abandon()
            '    FormsAuthentication.SignOut()
            'End If
            If Request.UrlReferrer.ToString.ToLower.Contains("/changepassword") = False And Request.UrlReferrer.ToString.ToLower.Contains("/default") = False Then
                Session("pp") = Request.UrlReferrer.ToString
            End If


        Catch ex As Exception

        End Try
    End Sub
    'Public Sub Login(object sender, EventArgs e)
    '    Try
    '        If txtNewPassword.Text <> txtNewPassword2.Text Then
    '            lblStatus.Text = "New Password don't match"
    '            txtNewPassword2.Focus()
    '            Exit Sub
    '        End If

    '        Dim strDataSet As New DataSet
    '        strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "users_authen", Session("LoginID"), Process.Encrypt(txtOldPassword.Text))
    '        Dim count As Integer = strDataSet.Tables(0).Rows.Count
    '        If count > 0 Then
    '            Dim id As String = strDataSet.Tables(0).Rows(0).Item("id").ToString
    '            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "users_update_password", id, Process.Encrypt(txtNewPassword.Text))
    '            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Password successfully updated" + "')", True)
    '            Response.Redirect("~/Home.aspx", True)
    '        Else
    '            lblStatus.Text = "Login validation failed, invalid password!"
    '        End If


    '    Catch ex As Exception
    '        Process.loadalert(divalert, msgalert, ex.Message, "danger")
    '    End Try
    'End Sub
    Protected Sub btnLogin_Click(sender As Object, e As EventArgs)
        Try
            Dim lblstatus As String = ""
            If newpwd.Value <> confirmpwd.Value Then
                lblstatus = "New Password don't match"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                confirmpwd.Focus()
                Exit Sub
            End If
            If currentpwd.Value.ToLower.Trim = newpwd.Value.ToLower.Trim Then
                Process.loadalert(divalert, msgalert, "New password should be different from the current password", "danger")
                Exit Sub
            End If

            Dim strDataSet As New DataSet
            strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "users_authen", Session("LoginID"), Process.Encrypt(currentpwd.Value))
            Dim count As Integer = strDataSet.Tables(0).Rows.Count
            If count > 0 Then
                Dim id As String = strDataSet.Tables(0).Rows(0).Item("id").ToString
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "users_update_password", id, Process.Encrypt(newpwd.Value))

                Dim days As Integer = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select cycleDays from Admin_PasswordDate")
                Dim newDAte As Date = Date.Now.AddDays(days)

                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, CommandType.Text, "update users set passwordupdatedate = '" & newDAte & "', passwordupdate = 'true' where userid ='" & Session("LoginID") & "'")

                If Session("pp") Is Nothing Then
                    Response.Redirect("empdashboard", True)
                Else
                    If Session("pp") = "" Then
                        Response.Redirect("empdashboard", True)
                    Else
                        Response.Redirect(Session("pp"), True)
                    End If
                End If
            Else
                lblstatus = "Login validation failed, Invalid password!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
            End If


        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnHome_Click(sender As Object, e As EventArgs)
        Try
            'If Session("pp") Is Nothing Then
            '    Response.Redirect("empdashboard", True)
            'Else
            '    If Session("pp") = "" Then
            '        Response.Redirect("empdashboard", True)
            '    Else
            '        Response.Redirect(Session("pp"), True)
            '    End If
            'End If
            Response.Redirect("empdashboard", True)

        Catch ex As Exception

        End Try
    End Sub
End Class