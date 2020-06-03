Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO


Public Class MenuSetupUpdate
    Inherits System.Web.UI.Page
    Dim workweek As New clsWorkWeek
    Dim AuthenCode As String = "MENUSETUPUPDATE"
    Dim olddata(4) As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
  
                'Work_Week_get_parent
                If Request.QueryString("id") IsNot Nothing Then
                    Dim RID As String = Request.QueryString("id")
                    Dim sDataset As DataSet
                    sDataset = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "SELECT * FROM menu where id = '" + RID + "'")
                    txtid.Text = sDataset.Tables(0).Rows(0).Item("id").ToString
                    oldname.Value = sDataset.Tables(0).Rows(0).Item("SubModule").ToString
                ElseIf Request.QueryString("id1") IsNot Nothing Then
                    Dim RID As String = Request.QueryString("id1")
                    Dim sDataset As DataSet
                    sDataset = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "SELECT * FROM menu where id = '" + RID + "'")
                    txtid.Text = sDataset.Tables(0).Rows(0).Item("id").ToString
                    oldname.Value = sDataset.Tables(0).Rows(0).Item("SubModule1").ToString
                ElseIf Request.QueryString("id2") IsNot Nothing Then
                    Dim RID As String = Request.QueryString("id2")
                    Dim sDataset As DataSet
                    sDataset = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "SELECT * FROM menu where id = '" + RID + "'")
                    txtid.Text = sDataset.Tables(0).Rows(0).Item("id").ToString
                    oldname.Value = sDataset.Tables(0).Rows(0).Item("SubModule2").ToString
                Else
                    txtid.Text = "0"
                    Process.loadalert(divalert, msgalert, "Unauthorized Access", "danger")
                End If

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            'If txtid.Text <> "0" Then
            '    If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False Then
            '        Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "warning")
            '        Exit Sub
            '    End If
            'End If

            Dim lblstatus As String = ""
            If (newname.Value = "" Or newname.Value Is Nothing) Then
                lblstatus = "New Menu name required!"
                Process.loadalert(divalert, msgalert, lblstatus, "info")
                Exit Sub
            End If


            If txtid.Text = "0" Then
                Process.loadalert(divalert, msgalert, "Unauthorized Access", "danger")
                If txtid.Text = "0" Then
                    Exit Sub
                End If
            Else
                If Request.QueryString("id") IsNot Nothing Then
                    Dim i As Integer = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "update menu set submodule = '" & newname.Value.ToString() & "' where submodule = '" & oldname.Value.ToString() & "'")
                ElseIf Request.QueryString("id1") IsNot Nothing Then
                    Dim i As Integer = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "update menu set submodule1 = '" & newname.Value.ToString() & "' where submodule1 = '" & oldname.Value.ToString() & "'")
                ElseIf Request.QueryString("id2") IsNot Nothing Then
                    Dim i As Integer = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "update menu set submodule2 = '" & newname.Value.ToString() & "' where submodule2 = '" & oldname.Value.ToString() & "'")
                Else
                    Process.loadalert(divalert, msgalert, "Unauthorized Access", "danger")
                End If

            End If
            
            lblstatus = "Record saved"
            Process.loadalert(divalert, msgalert, lblstatus, "success")
            Response.Redirect("~/Module/Reports/Finance/MenuSetup", True)
            'Response.Write("<script language='javascript'> { self.close() }</script>")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect("~/Module/Reports/Finance/MenuSetup", True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    'Protected Sub radStructure_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.DropDownListEventArgs) Handles radDay.SelectedIndexChanged
    '    Try
    '        Process.LoadRadComboP1(radStatus, "Work_Week_get_parent", radDay.SelectedText, 0)
    '    Catch ex As Exception
    '        Process.loadalert(divalert, msgalert, ex.Message, "danger")
    '    End Try
    'End Sub
End Class