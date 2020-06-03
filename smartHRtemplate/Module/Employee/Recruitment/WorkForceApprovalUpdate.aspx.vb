Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO


Public Class WorkForceApprovalUpdate
    Inherits System.Web.UI.Page
    Dim NoDays As Integer = 0
    Dim AuthenCode As String = "WFBUDGET"

    
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            '

            If Not Me.IsPostBack Then
                If Session("clicked") Is Nothing Then
                    Session("clicked") = "0"
                End If

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
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_WorkForce_Budget_Get", Request.QueryString("id"))
                    lblID.Text = strUser.Tables(0).Rows(0).Item("id").ToString

                    If strUser.Tables(0).Rows(0).Item("approver1").ToString = Session("UserEmpID") Then
                        status = strUser.Tables(0).Rows(0).Item("approver1status").ToString
                        reason = strUser.Tables(0).Rows(0).Item("approver1comment").ToString
                        lbllinkid.Text = "1"
                    ElseIf strUser.Tables(0).Rows(0).Item("approver2").ToString = Session("UserEmpID") Then
                        status = strUser.Tables(0).Rows(0).Item("approver2status").ToString
                        reason = strUser.Tables(0).Rows(0).Item("approver2comment").ToString
                        lbllinkid.Text = "2"
                    ElseIf strUser.Tables(0).Rows(0).Item("approver3").ToString = Session("UserEmpID") Then
                        status = strUser.Tables(0).Rows(0).Item("approver3status").ToString
                        reason = strUser.Tables(0).Rows(0).Item("approver3comment").ToString
                        lbllinkid.Text = "3"
                    ElseIf strUser.Tables(0).Rows(0).Item("approver4").ToString = Session("UserEmpID") Then
                        status = strUser.Tables(0).Rows(0).Item("approver4status").ToString
                        reason = strUser.Tables(0).Rows(0).Item("approver4comment").ToString
                        lbllinkid.Text = "4"
                    End If

                    Process.AssignRadDropDownValue(radStatus, status)
                    acomment.Value = reason
                    'Process.workfapprovalid = lblID.Text
                End If

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            Dim lblstatus As String = ""
            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_WorkForce_Approval_Update", lblID.Text, radStatus.SelectedText, acomment.Value, lbllinkid.Text, Session("UserEmpID"))
            Dim dept As String = ""
            Dim budgetyear As String = ""
            Dim sForceType As String = ""
            If Session("clicked") = 1 Then
                sForceType = "Budget"
            Else
                sForceType = "Plan"
            End If

            Dim strUser As New DataSet
            strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_WorkForce_Budget_Get", Request.QueryString("id"))
            If strUser.Tables(0).Rows.Count > 0 Then

                Dim approver As String = "n/a"
                If lbllinkid.Text = "1" Then
                    approver = strUser.Tables(0).Rows(0).Item("approver2").ToString
                ElseIf lbllinkid.Text = "2" Then
                    approver = strUser.Tables(0).Rows(0).Item("approver3").ToString
                ElseIf lbllinkid.Text = "3" Then
                    approver = strUser.Tables(0).Rows(0).Item("approver4").ToString
                End If

                dept = strUser.Tables(0).Rows(0).Item("office").ToString
                budgetyear = strUser.Tables(0).Rows(0).Item("budgetyear").ToString
                If radStatus.SelectedText.ToLower = "approved" Then
                    If approver.ToLower <> "n/a" Then                      
                        'Process.Work_Force_To_Approvers(sForceType, budgetyear, dept, approver, Process.ApplicationURL & "/Module/Recruitment/WorkForceBudget.aspx")
                        Process.Work_Force_To_Approvers(sForceType, budgetyear, dept, approver, Process.ApplicationURL & "/" & Process.GetMailLink(AuthenCode, 2))
                    Else
                        Process.Work_Force_Approvals_Complete(sForceType, budgetyear, dept, Process.ApplicationURL & "/" & Process.GetMailLink(AuthenCode, 3))
                    End If
                End If
            End If

            Process.Work_Force_Approvals(sForceType, budgetyear, dept, Session("userempid"), Session("EmpName"), radStatus.SelectedText, acomment.Value, Process.GetMailLink(AuthenCode, 3))
            If lbllinkid.Text = "4" And radStatus.SelectedText.ToLower = "approved" Then
                Process.Work_Force_Approvals_Complete(sForceType, budgetyear, dept, Process.ApplicationURL & "/" & Process.GetMailLink(AuthenCode, 3))
            End If
            lblstatus = "Status successfully updated"
            Process.loadalert(divalert, msgalert, lblstatus, "success")
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