Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO


Public Class AppraisalDisagree
    Inherits System.Web.UI.Page
    Dim clsappcycle As New clsAppraisalCycle
    Dim olddata(5) As String
    Dim AuthenCode As String = "TEAMPERF"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                Dim strUser As New DataSet
                strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Appraisal_Summary_Get", Request.QueryString("id").ToString)
                If strUser.Tables(0).Rows.Count > 0 Then
                    lblempid.Text = strUser.Tables(0).Rows(0).Item("empid").ToString
                End If
                amgrcomment.Focus()
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            Dim lblstatus As String = ""
            Process.loadalert(divalert, msgalert, lblstatus, "warning")
            If amgrcomment.Value.Trim = "" Then
                lblstatus = "Please include a comment!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                Exit Sub
            End If
            Process.disagree = 1
            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_Disagree", Request.QueryString("id").ToString)
            Process.Appraisal_Review_Disagree(lblempid.Text, Request.QueryString("cycle").ToString, Request.QueryString("empname").ToString, amgrcomment.Value, Process.GetEmployeeName(Request.QueryString("reviewer").ToString), Request.QueryString("reviewer").ToString, Session("EmpName"), Session("UserEmpID"), Process.ApplicationURL + "/" + Process.GetMailLink(AuthenCode, 4))
            lblstatus = "Disagree review comment sent to First Reviewer!"
            Process.loadalert(divalert, msgalert, lblstatus, "success")

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            Response.Write("<script language='javascript'> { self.close() }</script>")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

End Class