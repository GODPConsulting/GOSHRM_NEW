Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO


Public Class LeaveToApprove
    Inherits System.Web.UI.Page
    Dim ApplyLeave As New clsApplyLeave
    Dim ApproveLeave As New clsApproveLeave
    Dim AuthenCode As String = "APPLEAVES"
    Dim olddata(3) As String
    Dim LeaveBalance As Integer = 0
    Dim NoDays As Integer = 0
    Dim Level1(2) As String
    Dim Level2(2) As String
    Dim Separators() As Char = {":"c}
    Dim EmpID_1 As String = ""
    Dim EmpID_2 As String = ""
    Dim EmpID_1_Name As String = ""
    Dim EmpID_2_Name As String = ""

    Dim iID As String = ""




    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            '
            If Not Me.IsPostBack Then
                'Company_Structure_get_parent
                radApproval.Items.Clear()
                radApproval.Items.Add("Pending")
                radApproval.Items.Add("Approved")
                radApproval.Items.Add("Cancelled")
                radApproval.Items.Add("Rejected")

                Session("PreviousPage") = Request.UrlReferrer.ToString

                If Request.QueryString("id") IsNot Nothing Then
                    Dim Approver1Status As String = ""
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Employee_Leavelist_get", Request.QueryString("id"))
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    lblLeaveRefNo.Text = strUser.Tables(0).Rows(0).Item("refno").ToString
                    lblEmpID.Text = strUser.Tables(0).Rows(0).Item("empid").ToString
                    aemployee.Value = strUser.Tables(0).Rows(0).Item("EmployeeName").ToString

                    aDays.Value = strUser.Tables(0).Rows(0).Item("NoofDays").ToString
                    'Emp_PersonalDetail_get_all                   
                    Session("EmpGrade") = strUser.Tables(0).Rows(0).Item("GradeLevel").ToString
                    'lblManager.Text = strUser.Tables(0).Rows(0).Item("ApproverName1").ToString
                    Session("Mylocation") = strUser.Tables(0).Rows(0).Item("Location").ToString
                    areason.Value = strUser.Tables(0).Rows(0).Item("Reason").ToString
                    astartdate.Value = CDate(strUser.Tables(0).Rows(0).Item("From")).ToLongDateString
                    lblleavedate.Text = strUser.Tables(0).Rows(0).Item("From")
                    aenddate.Value = CDate(strUser.Tables(0).Rows(0).Item("To")).ToLongDateString
                    pagetitle.InnerText = strUser.Tables(0).Rows(0).Item("LeaveType").ToString & " : " & strUser.Tables(0).Rows(0).Item("refno").ToString
                    lblleavetype.Text = strUser.Tables(0).Rows(0).Item("LeaveType").ToString
                    Process.AssignRadComboValue(radApproval, strUser.Tables(0).Rows(0).Item("status").ToString)
                    Approver1Status = strUser.Tables(0).Rows(0).Item("status").ToString
                    ahrstatus.Value = strUser.Tables(0).Rows(0).Item("status2").ToString
                    Session("ManagerID") = strUser.Tables(0).Rows(0).Item("Approver1").ToString
                    acomment.Value = strUser.Tables(0).Rows(0).Item("SupervisorComment").ToString
                    alength.Value = strUser.Tables(0).Rows(0).Item("leavelength").ToString


                    If strUser.Tables(0).Rows(0).Item("filename").ToString.Trim = "" Then
                        sattachment.Visible = False
                    Else
                        sattachment.Visible = True
                    End If

                    Session("approver1_emailaddr") = strUser.Tables(0).Rows(0).Item("ManagerEmail").ToString
                    Session("emp_emailaddr") = strUser.Tables(0).Rows(0).Item("EmpMail").ToString

                    If Session("ManagerID") = Session("UserEmpID") Then
                        radApproval.Enabled = True
                        btnupdate.Disabled = False
                    Else
                        radApproval.Enabled = False
                        btnupdate.Disabled = True
                    End If

                    'If Approver1Status <> "Pending" Then
                    '    radApproval.Enabled = False
                    'End If

                    'If ahrstatus.Value = "Approved" Then
                    '    radApproval.Enabled = False
                    'End If
                End If
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            Dim lblstatus As String = ""

            If lblEmpID.Text = Session("UserEmpID") Then
                lblstatus = "Not allowed to perform self approval!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                Exit Sub
            End If

            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Employee_Leavelist_Update_Status", txtid.Text, radApproval.SelectedItem.Text, acomment.Value)
            If (radApproval.SelectedItem.Text.ToLower() = "approved") Then
                Process.Leave_Approver_Approvals(Process.GetMailList("hr"), lblLeaveRefNo.Text, aemployee.Value, lblleavetype.Text, astartdate.Value, aenddate.Value, areason.Value, radApproval.SelectedItem.Text, Session("EmpName"), acomment.Value, lblEmpID.Text, Session("UserEmpID"), Process.ApplicationURL + "/" + Process.GetMailLink(AuthenCode, 1))
                lblstatus = "Saved and forwarded to HR for further action"
                Process.loadalert(divalert, msgalert, lblstatus, "success")
            Else
                lblstatus = "Saved"
                Process.loadalert(divalert, msgalert, lblstatus, "success")
            End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect(Session("PreviousPage"), True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub downloadFile(ByVal bytefile As Byte(), ByVal filetype As String, ByVal filename As String)
        Dim bytes() As Byte = bytefile
        Response.Buffer = True
        Response.Charset = ""
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = filetype
        Response.AddHeader("content-disposition", "attachment;filename=" & filename)
        Response.BinaryWrite(bytes)
        Response.Flush()
        Response.End()
    End Sub

    Protected Sub lnkDownloadAttach_Click(sender As Object, e As EventArgs)
        Try
            Process.loadalert(divalert, msgalert, "", "warning")
            Dim dt As DataTable = Process.SearchData("Employee_Leavelist_get", txtid.Text)
            If dt IsNot Nothing Then
                downloadFile(CType(dt.Rows(0)("fileimage"), Byte()), dt.Rows(0)("filetype").ToString(), dt.Rows(0)("filename").ToString())
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
End Class