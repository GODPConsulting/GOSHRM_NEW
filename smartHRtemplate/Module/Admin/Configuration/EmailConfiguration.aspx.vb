Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Telerik.Web.UI

Public Class EmailConfiguration
    Inherits System.Web.UI.Page
    Dim mailconfig As New clsMailConfig
    Dim AuthenCode As String = "EMAIL"
    Dim olddata(14) As String
    Dim oFileAttached1 As Byte
    Dim oFileAttached As Byte()
    

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Read") = False Then
                Process.loadalert(divalert, msgalert, Process.privilegemsg, "warning")
                Exit Sub
            End If

            If Not Me.IsPostBack Then
                Dim smtpauth As String = ""

                Dim strUser As New DataSet
                strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Email_Configuration_get")
                If strUser.Tables(0).Rows.Count > 0 Then
                    smtphost.Value = strUser.Tables(0).Rows(0).Item("smtphost").ToString
                    smtpport.Value = strUser.Tables(0).Rows(0).Item("smtpport").ToString
                    smtpemail.Value = strUser.Tables(0).Rows(0).Item("senderemail").ToString
                    sendername.Value = strUser.Tables(0).Rows(0).Item("sendername").ToString
                    'smtppwd.Value = Process.Decrypt(strUser.Tables(0).Rows(0).Item("smptpwd").ToString)

                    smtpuser.Value = strUser.Tables(0).Rows(0).Item("smtpuser").ToString

                    smtpauth = strUser.Tables(0).Rows(0).Item("secureconnection").ToString
                    If smtpauth.ToUpper = "TRUE" Then
                        smtpauthentication.Value = "SSL"
                    ElseIf smtpauth.ToUpper = "FALSE" Then
                        smtpauthentication.Value = "TLS"
                    Else
                        smtpauthentication.Value = strUser.Tables(0).Rows(0).Item("secureconnection").ToString
                    End If
                    sendemail.Value = strUser.Tables(0).Rows(0).Item("sendnotification").ToString
                    'Process.AssignRadDropDownValue(radConnectionType, strUser.Tables(0).Rows(0).Item("secureconnection").ToString)
                    'Process.AssignRadDropDownValue(radAuthentication, strUser.Tables(0).Rows(0).Item("usesmtpauthentication").ToString)
                    'Process.AssignRadDropDownValue(radNotification, strUser.Tables(0).Rows(0).Item("sendnotification").ToString)

                    'txtTestMailAddr.Enabled = True
                    'btnTest.Enabled = True
                    'If radAuthentication.SelectedText = "Yes" Then
                    '    lblpassword.Visible = True
                    '    txtpassword.Visible = True
                    '    lbluser.Visible = True
                    '    txtsmtpuser.Visible = True
                    'Else
                    '    lblpassword.Visible = False
                    '    txtpassword.Visible = False
                    '    lbluser.Visible = False
                    '    txtsmtpuser.Visible = False
                    'End If
                    'Else
                    '    Process.AssignRadDropDownValue(radConnectionType, "True")
                    '    Process.AssignRadDropDownValue(radAuthentication, "Yes")
                    '    Process.AssignRadDropDownValue(radNotification, "No")
                    '    If radAuthentication.SelectedText = "Yes" Then
                    '        lblpassword.Visible = True
                    '        txtpassword.Visible = True
                    '        lbluser.Visible = True
                    '        txtsmtpuser.Visible = True
                    '    Else
                    '        lblpassword.Visible = False
                    '        txtpassword.Visible = False
                    '        lbluser.Visible = False
                    '        txtsmtpuser.Visible = False
                    '    End If
                End If
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub



    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False Then
                Process.loadalert(divalert, msgalert, Process.privilegemsg, "warning")
                Exit Sub
            End If

            'If (txtsenderemail.Text.Trim = "") Then
            '    lblstatus.Text = "Sender's Email required!"
            '    txtsenderemail.Focus()
            '    Exit Sub
            'End If

            'txtsenderemail.Text = txtsmtpuser.Text
            Dim lblstatus As String = ""
            If (smtphost.Value.Trim = "") Then
                lblstatus = "SMTP Host required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                smtphost.Focus()
                Exit Sub
            End If

            If (smtppwd.Value.Trim = "") Then
                lblstatus = "SMTP Password required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                smtppwd.Focus()
                Exit Sub
            End If

            'Old Data

            mailconfig.SenderEmail = smtpemail.Value.Trim
            mailconfig.SenderName = sendername.Value.Trim
            mailconfig.SMTPHost = smtphost.Value.Trim
            mailconfig.SMTPPassword = Process.Encrypt(smtppwd.Value)
            mailconfig.SMTPPort = CInt(smtpport.Value)
            mailconfig.SMTPUser = smtpuser.Value.Trim
            mailconfig.ConnectionType = smtpauthentication.Value

            If mailconfig.ConnectionType.ToUpper = "NONE" Then
                mailconfig.UseSMTPAuthentication = "No"
            Else
                mailconfig.UseSMTPAuthentication = "Yes"
                If smtpuser.Value.Trim = "" Then
                    lblstatus = "SMTP User required!"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    smtpuser.Focus()
                    Exit Sub
                End If

                If smtppwd.Value = "" Then
                    lblstatus = "SMTP Password required!"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    smtppwd.Focus()
                    Exit Sub
                End If
            End If

            Dim strUser As New DataSet
            strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Email_Configuration_get")
            Dim ccount As Integer = strUser.Tables(0).Rows.Count
            If strUser.Tables(0).Rows.Count > 0 Then
                olddata(0) = strUser.Tables(0).Rows(0).Item("senderemail").ToString
                olddata(1) = strUser.Tables(0).Rows(0).Item("sendername").ToString
                olddata(2) = strUser.Tables(0).Rows(0).Item("smtphost").ToString
                olddata(3) = strUser.Tables(0).Rows(0).Item("smtpport").ToString
                olddata(4) = strUser.Tables(0).Rows(0).Item("usesmtpauthentication").ToString
                olddata(5) = strUser.Tables(0).Rows(0).Item("smtpuser").ToString
                olddata(6) = Process.Decrypt(strUser.Tables(0).Rows(0).Item("smptpwd").ToString)
                olddata(7) = strUser.Tables(0).Rows(0).Item("secureconnection").ToString
            End If

            Dim NewValue As String = ""
            Dim OldValue As String = ""

            Dim j As Integer = 0

            If olddata(0) IsNot Nothing Then
                For Each a In GetType(clsMailConfig).GetProperties()
                    If a.Name.ToLower <> "id" And a.Name.ToLower.Contains("password") = False Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If IsNumeric(a.GetValue(mailconfig, Nothing)) = True And IsNumeric(olddata(j)) = True Then
                                If CDbl(a.GetValue(mailconfig, Nothing)) <> CDbl(olddata(j)) Then
                                    NewValue += a.Name + ": " + a.GetValue(mailconfig, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            Else
                                If a.GetValue(mailconfig, Nothing).ToString <> olddata(j).ToString Then
                                    NewValue += a.Name + ": " + a.GetValue(mailconfig, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            End If
                        End If
                    End If
                    j = j + 1
                Next
            Else
                For Each a In GetType(clsMailConfig).GetProperties() 'New Entries
                    If a.Name.ToLower <> "id" And a.Name.ToLower.Contains("password") Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If a.GetValue(mailconfig, Nothing) = Nothing Then
                                NewValue += a.Name + ":" + " " & vbCrLf
                            Else
                                NewValue += a.Name + ": " + a.GetValue(mailconfig, Nothing).ToString & vbCrLf
                            End If
                        End If
                    End If
                Next
            End If

            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Email_Configuration_Update", mailconfig.SenderEmail, mailconfig.SenderName, mailconfig.SMTPHost, mailconfig.SMTPPort, mailconfig.UseSMTPAuthentication, mailconfig.SMTPUser, mailconfig.SMTPPassword, mailconfig.ConnectionType, sendemail.Value)

            If NewValue.Trim = "" And OldValue.Trim = "" Then
            Else
                If olddata(0) IsNot Nothing Then
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Updated", "Email Configuration")
                Else
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Inserted", "Email Configuration")
                End If
            End If

            'Response.Redirect("~/Module/Admin/Configuration/EmailConfiguration.aspx", False)
            lblstatus = "Record saved"
            Process.loadalert(divalert, msgalert, lblstatus, "success")

            'btnTest.Enabled = True
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.message, "danger")
        End Try
    End Sub


    Protected Sub btnTest_Click(sender As Object, e As EventArgs)
        Try
            Dim lblstatus As String = ""
            If Process.SendEmail("", "", txttestemail.Value, "Testing Mail on GOSHRM", "Mail received", "", False) = True Then
                lblstatus = "Mail sent successful to " & txttestemail.Value
                Process.loadalert(divalert, msgalert, lblstatus, "success")
            Else
                Process.loadalert(divalert, msgalert, Session("exception"), "warning")
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.message, "danger")
        End Try
    End Sub

    'Protected Sub radAuthentication_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.DropDownListEventArgs) Handles radAuthentication.SelectedIndexChanged
    '    Try
    '        If radAuthentication.SelectedText = "Yes" Then
    '            lblpassword.Visible = True
    '            txtpassword.Visible = True
    '            lbluser.Visible = True
    '            txtsmtpuser.Visible = True
    '        Else
    '            lblpassword.Visible = False
    '            txtpassword.Visible = False
    '            lbluser.Visible = False
    '            txtsmtpuser.Visible = False
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub
End Class