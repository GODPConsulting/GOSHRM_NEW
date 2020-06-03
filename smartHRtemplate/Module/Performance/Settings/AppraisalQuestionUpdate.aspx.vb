Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO


Public Class AppraisalQuestionUpdate
    Inherits System.Web.UI.Page
    Dim clsappcycle As New clsAppraisalCycle
    Dim AuthenCode As String = "APPRAISALQUEST"
    Dim olddata(5) As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                'Company_Structure_get_parent
                rdoActive.Items.Clear()
                rdoActive.Items.Add("No")
                rdoActive.Items.Add("Yes")

                cboCategory.Items.Clear()
                cboCategory.Items.Add("Growth and Development")
                cboCategory.Items.Add("Achievement/Accomplishment")

                txtid.Text = "0"
                Process.RadioListCheck(rdoActive, "Yes")
                If Request.QueryString("id") IsNot Nothing Then
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Appraisal_Questions_Get", Request.QueryString("id"))
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    txtQuestion.Text = strUser.Tables(0).Rows(0).Item("Questions").ToString
                    Process.RadioListCheck(rdoActive, strUser.Tables(0).Rows(0).Item("Active").ToString)
                    Process.AssignRadComboValue(cboCategory, strUser.Tables(0).Rows(0).Item("Category").ToString)
                    txtactive.Text = strUser.Tables(0).Rows(0).Item("Active").ToString
                    lblcreatedby.Text = strUser.Tables(0).Rows(0).Item("AddedBy").ToString
                    lblcreatedon.Text = strUser.Tables(0).Rows(0).Item("AddedOn").ToString
                    lblupdatedby.Text = strUser.Tables(0).Rows(0).Item("UpdatedBy").ToString
                    lblupdatedon.Text = strUser.Tables(0).Rows(0).Item("UpdatedOn").ToString
                End If

            End If
        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            If txtid.Text <> "0" Then
                If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False Then
                    ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
                    Exit Sub
                End If
            End If
           
            If (txtQuestion.Text.Trim Is Nothing) Then
                lblstatus.Text = "Question required!"
                txtQuestion.Focus()
                Exit Sub
            End If

            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_Questions_update", txtid.Text, txtQuestion.Text, cboCategory.SelectedItem.Text, txtactive.Text, Session("LoginID"))

            lblstatus.Text = "Record saved"
            Response.Write("<script language='javascript'> { self.close() }</script>")
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


    Protected Sub rdoActive_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rdoActive.SelectedIndexChanged
        Try
            txtactive.Text = rdoActive.SelectedItem.Text
        Catch ex As Exception

        End Try
    End Sub
End Class