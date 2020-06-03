Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO


Public Class ExitApprovalUpdate
    Inherits System.Web.UI.Page
    Dim exits As New clsExit
    Dim AuthenCode As String = "EMPAPPROVALEXIT"
    Dim olddata(3) As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                cboApproval.Items.Clear()
                cboApproval.Items.Add("Pending")
                cboApproval.Items.Add("Approved")
                cboApproval.Items.Add("Cancelled")
                cboApproval.Items.Add("Rejected")

                If Session("clicked") Is Nothing Then
                    Session("clicked") = "0"
                End If

                'Company_Structure_get_parent
                If Request.QueryString("id") IsNot Nothing Then
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Termination_Get", Request.QueryString("id"))
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    lblemployee.Value = strUser.Tables(0).Rows(0).Item("Employee").ToString
                    lblexitdate.Value = strUser.Tables(0).Rows(0).Item("TerminationDate2").ToString
                    lblexittype.Value = strUser.Tables(0).Rows(0).Item("ExitType").ToString
                    lblhrcomment.Value = strUser.Tables(0).Rows(0).Item("hrcomment").ToString.Replace(vbCrLf, "<br />")
                    lblmyapproval.Text = strUser.Tables(0).Rows(0).Item("SupervisorApproval").ToString
                    lblnoticedate.Value = strUser.Tables(0).Rows(0).Item("NoticeDate2").ToString
                    lblreason.Value = strUser.Tables(0).Rows(0).Item("Reason").ToString.Replace(vbCrLf, "<br />")
                    
                    lbldept.Value = strUser.Tables(0).Rows(0).Item("Office").ToString
                    lblposition.Value = strUser.Tables(0).Rows(0).Item("jobtitle").ToString
                    lblEmpID.Text = strUser.Tables(0).Rows(0).Item("EmpID").ToString
                    lblapprover.Text = strUser.Tables(0).Rows(0).Item("ForwardTo").ToString


                    If strUser.Tables(0).Rows(0).Item("approver2").ToString = Session("UserEmpID") Then
                        Process.AssignRadComboValue(cboApproval, strUser.Tables(0).Rows(0).Item("Approval2").ToString)
                        txtComment.Value = strUser.Tables(0).Rows(0).Item("ApproverComment2").ToString
                        lblapproverid.Text = strUser.Tables(0).Rows(0).Item("approver2").ToString
                        Session("clicked") = 2
                    ElseIf strUser.Tables(0).Rows(0).Item("mgrid").ToString = Session("UserEmpID") Then
                        Process.AssignRadComboValue(cboApproval, strUser.Tables(0).Rows(0).Item("SupervisorApproval").ToString)
                        txtComment.Value = strUser.Tables(0).Rows(0).Item("supervisorcomment").ToString
                        lblapproverid.Text = strUser.Tables(0).Rows(0).Item("mgrid").ToString
                        Session("clicked") = 1
                    End If



                    If lblhrapproval.Value.ToLower = "approved" And cboApproval.SelectedItem.Text.ToLower = "approved" Then
                        cboApproval.Enabled = False
                    End If
                Else
                    txtid.Text = "0"
                End If

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            If Session("clicked") = 1 Then
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Termination_Update_Supervisor", txtid.Text, cboApproval.SelectedItem.Text, txtComment.Value)
            Else
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Termination_Update_Higher_Approver", txtid.Text, cboApproval.SelectedItem.Text, txtComment.Value)
            End If


            Process.Exit_Approval_To_HR(cboApproval.SelectedItem.Text, txtComment.Value, lblEmpID.Text, lblapproverid.Text, Process.ApplicationURL() + "/" + Process.GetMailLink(AuthenCode, 1))
            Process.loadalert(divalert, msgalert, "Approval is saved and forwarded to HR for further action", "success")

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
   
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect("ExitApprovals")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


    
End Class