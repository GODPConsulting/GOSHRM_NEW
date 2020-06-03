Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO


Public Class AppraisalPointUpdate
    Inherits System.Web.UI.Page
    Dim clsappcycle As New clsAppraisalCycle
    Dim AuthenCode As String = "APPRAISALQUEST"
    Dim olddata(5) As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                'Company_Structure_get_parent              
                txtid.Text = "0"
                If Request.QueryString("id") IsNot Nothing Then
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Points_Get", Request.QueryString("id"))
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    txtPoint.Value = strUser.Tables(0).Rows(0).Item("Point").ToString
                    txtName.Value = strUser.Tables(0).Rows(0).Item("PointName").ToString
                    txtDesc.Value = strUser.Tables(0).Rows(0).Item("PointDescription").ToString

                    lblcreatedby.Value = strUser.Tables(0).Rows(0).Item("AddedBy").ToString
                    lblcreatedon.Value = strUser.Tables(0).Rows(0).Item("AddedOn").ToString
                    lblupdatedby.Value = strUser.Tables(0).Rows(0).Item("UpdatedBy").ToString
                    lblupdatedon.Value = strUser.Tables(0).Rows(0).Item("UpdatedOn").ToString
                End If

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            If txtid.Text <> "0" Then
                If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False Then
                    Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "danger")
                    Exit Sub
                End If
            End If

            If txtPoint.Value.Trim Is Nothing Then
                Process.loadalert(divalert, msgalert, "Point Number required!", "danger")
                txtPoint.Focus()
                Exit Sub
            End If

            If txtName.Value.Trim Is Nothing Then
                Process.loadalert(divalert, msgalert, "Point Name required!", "danger")
                txtName.Focus()
                Exit Sub
            End If

            If (txtDesc.Value.Trim Is Nothing) Then
                Process.loadalert(divalert, msgalert, "Description required!", "danger")
                txtDesc.Focus()
                Exit Sub
            End If

            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Points_update", txtid.Text, txtPoint.Value, txtName.Value.Trim, txtDesc.Value.Trim, Session("LoginID"))

            Process.loadalert(divalert, msgalert, "Record saved", "success")
            'Response.Write("<script language='javascript'> { self.close() }</script>")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect("~/Module/Performance/Settings/AppraisalPoints", True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
End Class