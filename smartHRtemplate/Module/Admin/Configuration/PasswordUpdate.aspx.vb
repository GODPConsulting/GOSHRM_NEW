Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Telerik.Web.UI

Public Class PasswordUpdate
    Inherits System.Web.UI.Page
    Dim mailconfig As New clsMailConfig
    Dim AuthenCode As String = "PASSWORDDATE"
    Dim olddata(14) As String
    Dim oFileAttached1 As Byte
    Dim oFileAttached As Byte()
    Private Sub LoadData()
        Dim strUser As New DataSet
        strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "select * from Admin_PasswordDate")
        If strUser.Tables(0).Rows.Count > 0 Then
            days.Value = strUser.Tables(0).Rows(0).Item("cycleDays").ToString
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                LoadData()
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message)
            'lblstatus.Text = ex.Message
        End Try
    End Sub



    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            'If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False Then
            '    Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "danger")
            '    Exit Sub
            'End If

            'If (radDateDue.SelectedDate Is Nothing) Then
            '    Process.loadalert(divalert, msgalert, "Due Date required!", "danger")
            '    Exit Sub
            'End If

            If (days.Value Is Nothing) Then
                Process.loadalert(divalert, msgalert, "Update cycle days required!", "danger")
                Exit Sub
            End If

            Dim newDAte As Date = Date.Now.AddDays(Convert.ToInt32(days.Value))

            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Admin_Password_Update", newDAte, days.Value)

            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, CommandType.Text, "update users set passwordupdatedate = '" & newDAte & "', passwordupdate = 'false'")

            Process.loadalert(divalert, msgalert, "Record saved", "success")

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


End Class