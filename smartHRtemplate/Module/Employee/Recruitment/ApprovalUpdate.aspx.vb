Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO


Public Class ApprovalUpdate
    Inherits System.Web.UI.Page
    Dim NoDays As Integer = 0
    Dim AuthenCode As String = "MGRPROMOTIONS"
    Dim AuthenCode2 As String = "MGRSUCCESSION"


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            '

            If Not Me.IsPostBack Then
                radStatus.Items.Clear()
                radStatus.Items.Add("Pending")
                radStatus.Items.Add("Approved")
                radStatus.Items.Add("Cancelled")
                radStatus.Items.Add("Rejected")

                'Process.AssignRadDropDownValue(radStatus, "Pending")

                'Company_Structure_get_parent
                If Request.QueryString("id") IsNot Nothing Then
                    Dim reason As String = ""
                    Dim status As String = ""

                    Dim strUser As New DataSet
                    If Request.QueryString("type") = "promotion" Then
                        strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruitment_Promotion_Get", Request.QueryString("id"))

                        lblinitiator.Text = strUser.Tables(0).Rows(0).Item("initiatorid").ToString
                        lbljobgrade.Text = strUser.Tables(0).Rows(0).Item("jobgrade").ToString
                        lbljobgradeold.Text = strUser.Tables(0).Rows(0).Item("oldjobgrade").ToString
                        lbljobtitle.Text = strUser.Tables(0).Rows(0).Item("Jobtitle").ToString
                        lbljobtitleold.Text = strUser.Tables(0).Rows(0).Item("oldJobtitle").ToString

                    ElseIf Request.QueryString("type") = "succession" Then
                        strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruitment_Succession_Get", Request.QueryString("id"))

                        lbljobgrade.Text = strUser.Tables(0).Rows(0).Item("plannedjobgrade").ToString
                        lbljobgradeold.Text = strUser.Tables(0).Rows(0).Item("jobgrade").ToString
                        lbljobtitle.Text = strUser.Tables(0).Rows(0).Item("plannedJobtitle").ToString
                        lbljobtitleold.Text = strUser.Tables(0).Rows(0).Item("Jobtitle").ToString
                    End If

                    lblID.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    lblempid.Text = strUser.Tables(0).Rows(0).Item("empid").ToString
                    If strUser.Tables(0).Rows(0).Item("approver1").ToString = Session("UserEmpID") Then
                        status = strUser.Tables(0).Rows(0).Item("approverstatus1").ToString
                        reason = strUser.Tables(0).Rows(0).Item("approvercomment1").ToString
                        lbllinkid.Text = "1"
                        lblapprovernext.Text = strUser.Tables(0).Rows(0).Item("approver2").ToString
                    ElseIf strUser.Tables(0).Rows(0).Item("approver2").ToString = Session("UserEmpID") Then
                        status = strUser.Tables(0).Rows(0).Item("approverstatus2").ToString
                        reason = strUser.Tables(0).Rows(0).Item("approvercomment2").ToString
                        lblapprovernext.Text = strUser.Tables(0).Rows(0).Item("approver3").ToString
                        lbllinkid.Text = "2"
                    ElseIf strUser.Tables(0).Rows(0).Item("approver3").ToString = Session("UserEmpID") Then
                        status = strUser.Tables(0).Rows(0).Item("approverstatus3").ToString
                        reason = strUser.Tables(0).Rows(0).Item("approvercomment3").ToString
                        lbllinkid.Text = "3"

                    End If

                    Process.AssignRadDropDownValue(radStatus, status)
                    If strUser.Tables(0).Rows(0).Item("finalstatus").ToString.ToLower = "approved" Then
                        radStatus.Enabled = False
                    End If

                    acomment.Value = reason
                    'Process.workfapprovalid = lblID.Text
                End If

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try

            If Request.QueryString("type") = "promotion" Then
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruitment_Promotion_Approval_Update", lblID.Text, radStatus.SelectedText, acomment.Value, lbllinkid.Text)
                If radStatus.SelectedText.ToLower = "approved" Then
                    If lblapprovernext.Text.ToLower <> "n/a" Then
                        Process.Staff_Promotion_Approver_Notification(lbljobtitleold.Text, lbljobgradeold.Text, lbljobtitle.Text, lbljobgrade.Text, lblempid.Text, lblinitiator.Text, lblapprovernext.Text, Process.ApplicationURL + "/" + Process.GetMailLink(AuthenCode, 2))
                    End If

                End If
                If lblapprovernext.Text = "n/a" Or lbllinkid.Text = "3" Then
                    Process.Staff_Promotion_Approver_HR_Reply("Complete", radStatus.SelectedText, acomment.Value, lbljobtitleold.Text, lbljobgradeold.Text, lbljobtitle.Text, lbljobgrade.Text, lblempid.Text, lblapprovernext.Text, Process.ApplicationURL + "/" + Process.GetMailLink(AuthenCode, 1))
                Else
                    Process.Staff_Promotion_Approver_HR_Reply(lbllinkid.Text, radStatus.SelectedText, acomment.Value, lbljobtitleold.Text, lbljobgradeold.Text, lbljobtitle.Text, lbljobgrade.Text, lblempid.Text, lblapprovernext.Text, Process.ApplicationURL + "/" + Process.GetMailLink(AuthenCode, 1))
                End If

            ElseIf Request.QueryString("type") = "succession" Then
                'Recruitment_Succession_Approval_Update
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruitment_Succession_Approval_Update", lblID.Text, radStatus.SelectedText, acomment.Value, lbllinkid.Text)
                If radStatus.SelectedText.ToLower = "approved" Then
                    If lblapprovernext.Text.ToLower <> "n/a" Then
                        Process.Staff_Succession_Approver_Notification(lbljobtitleold.Text, lbljobgradeold.Text, lbljobtitle.Text, lbljobgrade.Text, lblempid.Text, lblinitiator.Text, lblapprovernext.Text, Process.ApplicationURL + "/" + Process.GetMailLink(AuthenCode2, 2))
                    End If

                End If
                If lblapprovernext.Text = "n/a" Or lbllinkid.Text = "3" Then
                    Process.Staff_Succession_Approver_HR_Reply("Complete", radStatus.SelectedText, acomment.Value, lbljobtitleold.Text, lbljobgradeold.Text, lbljobtitle.Text, lbljobgrade.Text, lblempid.Text, Session("UserEmpID"), Process.ApplicationURL + "/" + Process.GetMailLink(AuthenCode2, 1))
                Else
                    Process.Staff_Succession_Approver_HR_Reply(lbllinkid.Text, radStatus.SelectedText, acomment.Value, lbljobtitleold.Text, lbljobgradeold.Text, lbljobtitle.Text, lbljobgrade.Text, lblempid.Text, Session("UserEmpID"), Process.ApplicationURL + "/" + Process.GetMailLink(AuthenCode2, 1))
                End If
            End If


            Process.loadalert(divalert, msgalert, "Status successfully updated", "success")
            Response.Write("<script language='javascript'> { self.close() }</script>")

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            Response.Write("<script language='javascript'> { self.close() }</script>")
        Catch ex As Exception

        End Try
    End Sub
End Class