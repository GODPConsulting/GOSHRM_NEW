Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO


Public Class ActivitysUpdate
    Inherits System.Web.UI.Page
    Dim client As New clsClient
    Dim AuthenCode As String = "CLIENT"
    Dim olddata(8) As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                ViewState("PreviousPage") = Request.UrlReferrer
                'Company_Structure_get_parent
                If Request.QueryString("PID") IsNot Nothing Then
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Time_Projects_Activities_Get", Request.QueryString("PID"))
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    txtprojid.Text = strUser.Tables(0).Rows(0).Item("ProjectID").ToString
                    txtDetail.Text = strUser.Tables(0).Rows(0).Item("Activity").ToString
                    txtEstimation.Text = strUser.Tables(0).Rows(0).Item("estimatedhr").ToString
                    lblClient.Text = strUser.Tables(0).Rows(0).Item("ClientName").ToString
                    lblProject.Text = strUser.Tables(0).Rows(0).Item("Name").ToString
                Else
                    txtid.Text = 0
                End If

            End If
        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False Then
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
                Exit Sub
            End If

            If (txtDetail.Text.Trim = "") Then
                lblstatus.Text = "Activity required!"
                txtDetail.Focus()
                Exit Sub
            End If

            If IsNumeric(txtEstimation.Text) = False Then
                lblstatus.Text = "Estimated Time in hours to completion is required!"
                txtEstimation.Focus()
                Exit Sub
            End If

            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Time_Projects_Activities_Update", txtid.Text, txtprojid.Text, txtDetail.Text, txtEstimation.Text)

            lblstatus.Text = "Record saved"

            If ViewState("PreviousPage") IsNot Nothing Then
                Response.Redirect(ViewState("PreviousPage").ToString())
            End If

        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            'Response.Write("<script language='javascript'> { self.close() }</script>")
            If ViewState("PreviousPage") IsNot Nothing Then
                Response.Redirect(ViewState("PreviousPage").ToString())
            End If
        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub


End Class