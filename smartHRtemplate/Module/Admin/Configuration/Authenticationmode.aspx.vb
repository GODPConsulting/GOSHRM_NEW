Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Telerik.Web.UI

Public Class Authenticationmode
    Inherits System.Web.UI.Page
    Dim mailconfig As New clsMailConfig
    Dim AuthenCode As String = "UserAuthen"
    Dim olddata(14) As String
    Dim oFileAttached1 As Byte
    Dim oFileAttached As Byte()
    Private Sub cboADPostBack()
        If (cboAD.SelectedItem.Value.ToLower() = "false") Then
            ldap.Disabled = True
        Else
            ldap.Disabled = False
        End If
    End Sub
    Private Sub cboDualAuthenPostBack()
        If (cboDualAuthen.SelectedItem.Value.ToLower() = "false") Then
            cboAuthenMode.Enabled = False
        Else
            cboAuthenMode.Enabled = True
        End If
    End Sub
    Private Sub LoadData()
        Dim strUser As New DataSet
        strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "User_LDAP_Setting_Get")
        If strUser.Tables(0).Rows.Count > 0 Then
            ldap.Value = strUser.Tables(0).Rows(0).Item("ldap").ToString
            Process.AssignRadComboValue(cboAD, strUser.Tables(0).Rows(0).Item("useAD").ToString)
            Process.AssignRadComboValue(cboDualAuthen, strUser.Tables(0).Rows(0).Item("dual_auth").ToString)
            Process.AssignRadComboValue(cboAuthenMode, strUser.Tables(0).Rows(0).Item("dual_mode").ToString)
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                LoadData()
                cboADPostBack()
                cboDualAuthenPostBack()

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub



    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "danger")
                Exit Sub
            End If

            If (cboAD.SelectedItem.Value.ToLower() = "true") Then
                If (ldap.Value.Trim = "") Then
                    Process.loadalert(divalert, msgalert, "Host Name / Server required!", "warning")
                    ldap.Focus()
                    Exit Sub
                End If
            End If

            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "User_LDAP_Setting_Update", ldap.Value.Trim, cboAD.SelectedItem.Value, cboDualAuthen.SelectedItem.Value, cboAuthenMode.SelectedItem.Value)

            Process.loadalert(divalert, msgalert, "Record saved", "success")

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub cboAD_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles cboAD.SelectedIndexChanged
        Try
            cboADPostBack()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cboDualAuthen_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles cboDualAuthen.SelectedIndexChanged
        Try
            cboDualAuthenPostBack()
        Catch ex As Exception

        End Try
    End Sub
End Class