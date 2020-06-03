Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports System.IO
Imports System.Net
Imports System.Management
Imports System.Net.NetworkInformation
Imports System.Xml


Public Class License
    Inherits System.Web.UI.Page
    Dim education As New clsEducation
    Dim Separators() As Char = {","c}
   
    Dim apptype As String = ConfigurationManager.AppSettings("apptype")
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then

                If apptype.ToLower = "cloud" Then
                    ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "License feature not available in Cloud Edition" + "')", True)

                    Response.Redirect("~/Home.aspx", True)
                End If
                Dim strUser As New DataSet
                strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "User_Temp_Table_Get")

                If strUser.Tables(0).Rows.Count > 0 Then
                    license.Value = Process.Decrypt(strUser.Tables(0).Rows(0).Item("Keys").ToString)
                    createdon.Value = strUser.Tables(0).Rows(0).Item("CreatedOn").ToString
                    updatedon.Value = strUser.Tables(0).Rows(0).Item("Updated").ToString
                    hostip.Value = strUser.Tables(0).Rows(0).Item("hostip").ToString
                End If
                createdon.Disabled = True
                updatedon.Disabled = True
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            If hostip.Value.Trim = "" Then
                Process.loadalert(divalert, msgalert, "Enter Host Server IP", "warning")
                hostip.Focus()
                Exit Sub
            End If

            If license.Value.Trim = "" Or Len(license.Value.Trim) < 10 Then

                Process.loadalert(divalert, msgalert, "Enter License Key", "warning")
                license.Focus()
                Exit Sub
            End If

            If Process.DnsTest("www.google.com") = True Then
                If Process.RegisterApp("http://godp.com.ng/validate_hr_license{0}", license.Value, Process.getMacAddress(hostip.Value)) = True Then
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "User_Temp_Table_Update", hostip.Value, Process.Encrypt(license.Value), Date.Now.AddYears(50))
                    Process.loadalert(divalert, msgalert, "License successfully saved")
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "User_Temp_Table_Register", Process.Encrypt("yes"))
                Else
                    If Process.strExp.Trim = "" Then
                        Process.loadalert(divalert, msgalert, "Key validation failed", "warning")
                    Else
                        Process.loadalert(divalert, msgalert, Process.strExp, "danger")
                    End If
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "User_Temp_Table_Register", Process.Encrypt("no"))
                    'ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & lblstatus.Text + "')", True)
                End If
            Else
                Process.loadalert(divalert, msgalert, "Key validation failed, please check your internet connection", "warning")

            End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try

    End Sub
    
End Class