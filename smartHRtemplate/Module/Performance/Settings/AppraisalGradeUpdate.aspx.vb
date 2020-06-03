Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO


Public Class AppraisalGradeUpdate
    Inherits System.Web.UI.Page
    Dim clsappcycle As New clsAppraisalCycle
    Dim AuthenCode As String = "APPRAISALGRADE"
    Dim olddata(5) As String
    Dim lblstatus As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                'Company_Structure_get_parent              
                txtid.Text = "0"
                If Request.QueryString("id") IsNot Nothing Then
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Grading_Get", Request.QueryString("id"))
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    txtMin.Value = strUser.Tables(0).Rows(0).Item("MinScore").ToString
                    txtMax.Value = strUser.Tables(0).Rows(0).Item("MaxScore").ToString
                    txtName.Value = strUser.Tables(0).Rows(0).Item("GradeName").ToString
                    txtDesc.Value = strUser.Tables(0).Rows(0).Item("GradeDescription").ToString

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

            If txtName.Value.Trim Is Nothing Then
                lblstatus = "Point Name required!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                txtName.Focus()
                Exit Sub
            End If

            If IsNumeric(txtMin.Value) = False Then
                lblstatus = "Minimum Value required!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                txtMin.Focus()
                Exit Sub
            End If

            If IsNumeric(txtMax.Value) = False Then
                lblstatus = "Maximum Value required!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                txtMax.Focus()
                Exit Sub
            End If

            If CDbl(txtMax.Value) < CDbl(txtMin.Value) Then
                lblstatus = "Maximum Value must be > Minimum Value!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                txtMax.Focus()
                Exit Sub
            End If

            If (txtDesc.Value.Trim Is Nothing) Then
                lblstatus = "Description required!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                txtDesc.Focus()
                Exit Sub
            End If

            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Grading_update", txtid.Text, txtName.Value.Trim, txtDesc.Value.Trim, txtMin.Value, txtMax.Value, Session("LoginID"))

            lblstatus = "Record saved"
            Process.loadalert(divalert, msgalert, lblstatus, "success")
            'Response.Write("<script language='javascript'> { self.close() }</script>")
        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "danger")
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect("~/Module/Performance/Settings/AppraisalGrades", True)
        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "danger")
        End Try
    End Sub
End Class