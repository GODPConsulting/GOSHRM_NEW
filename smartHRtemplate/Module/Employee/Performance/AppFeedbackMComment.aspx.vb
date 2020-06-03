Imports Microsoft.ApplicationBlocks.Data

Public Class AppFeedbackMComment
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                If Request.QueryString("id") IsNot Nothing Then
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Appraisal_Summary_Get", Request.QueryString("id").ToString)
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    cboDecisions.Text = strUser.Tables(0).Rows(0).Item("decision").ToString
                    comments.Value = strUser.Tables(0).Rows(0).Item("finalcomment").ToString

                End If
            End If
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            Dim lblstatus As String = ""
            Process.loadalert(divalert, msgalert, lblstatus, "danger")
            If cboDecisions.Text Is Nothing Or cboDecisions.Text = "--Select--" Then
                lblstatus = "Please select a decision"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                Exit Sub
            End If
            If comments.Value Is Nothing Then
                lblstatus = "Please enter comment"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                Exit Sub
            End If
            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_Comment_Update", txtid.Text, cboDecisions.Text, comments.Value)
            Process.loadalert(divalert, msgalert, "Comment successfully updated", "success")


        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect("~/Module/Employee/Performance/DirectReportAppraisalObjectivesForm.aspx", True)
        Catch ex As Exception

        End Try
    End Sub

End Class