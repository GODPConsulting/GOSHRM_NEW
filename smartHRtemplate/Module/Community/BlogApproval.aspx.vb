Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO


Public Class BlogApproval
    Inherits System.Web.UI.Page
  

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                Dim strdetail As New DataSet
                strdetail = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Blogs_Get", Request.QueryString("id"))
                If strdetail.Tables(0).Rows.Count > 0 Then
                    txtGoalDesc.Text = strdetail.Tables(0).Rows(0).Item("approvalcomment").ToString
                End If

                txtGoalDesc.Focus()
            End If
        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try

            If txtGoalDesc.Text.Trim = "" Then
                lblstatus.Text = "Please include a comment!"
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & lblstatus.Text + "')", True)
                Exit Sub
            End If
            Process.disagree = 1
            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Blogs_Approval_Comment_Add", Request.QueryString("id").ToString, Session("UserEmpID"), txtGoalDesc.Text)

            lblstatus.Text = "Saved!!"
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & lblstatus.Text + "')", True)
        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Response.Write("<script language='javascript'> { self.close() }</script>")
        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub


    'Protected Sub rdoStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rdoStatus.SelectedIndexChanged
    '    Try
    '        txtappstatus.Text = rdoStatus.SelectedItem.Text
    '    Catch ex As Exception

    '    End Try
    'End Sub
End Class