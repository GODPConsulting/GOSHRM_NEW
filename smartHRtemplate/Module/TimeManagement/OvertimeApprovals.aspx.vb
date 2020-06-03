Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Microsoft.VisualBasic


Public Class OvertimeApprovals
    Inherits System.Web.UI.Page
    Dim TimeSheet As New clsTimeSheet
    Dim ApproveLeave As New clsApproveLeave
    Dim AuthenCode As String = "ATTENDANCE"
    Dim olddata(11) As String
    Dim emp_emailaddr As String
    Dim approver1_emailaddr As String
    Dim approver2_emailaddr As String
    Dim LeaveBalance As Integer = 0
    Dim NoDays As Integer = 0
    Dim Level1(2) As String
    Dim Level2(2) As String
    Dim Separators() As Char = {":"c}
    Dim EmpID_1 As String = ""
    Dim EmpID_2 As String = ""
    Dim EmpID_1_Name As String = ""
    Dim EmpID_2_Name As String = ""
    Dim isEligible As String = "Yes"



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            '

            If Not Me.IsPostBack Then
                cboApproval.Items.Clear()
                cboApproval.Items.Add("Pending")
                cboApproval.Items.Add("Approved")
                cboApproval.Items.Add("Rejected")
                cboApproval.Items.Add("")
                Process.AssignRadComboValue(cboApproval, "")
                If Request.QueryString("id") IsNot Nothing Then
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Time_Employee_Attendance_Detail_My_Team", Request.QueryString("id"))
                    lblAgreedOvertime.Text = strUser.Tables(0).Rows(0).Item("agreedovertime").ToString
                    Session("EmpID") = strUser.Tables(0).Rows(0).Item("empid").ToString
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    Process.AssignRadComboValue(cboApproval, strUser.Tables(0).Rows(0).Item("hrapprovalstatus").ToString)
                    lblShift.Text = strUser.Tables(0).Rows(0).Item("shifts").ToString
                    lblDuration.Text = strUser.Tables(0).Rows(0).Item("duration").ToString
                    lblStartTime.Text = strUser.Tables(0).Rows(0).Item("checkintime").ToString
                    lblEndTime.Text = strUser.Tables(0).Rows(0).Item("checkouttime").ToString
                    lblHoursLogged.Text = strUser.Tables(0).Rows(0).Item("actualduration").ToString
                    lblStartDate.Text = strUser.Tables(0).Rows(0).Item("checkindate").ToString
                    lblEndDate.Text = strUser.Tables(0).Rows(0).Item("checkoutdate").ToString
                    lblManagerApproval.Text = strUser.Tables(0).Rows(0).Item("mgrapprovalstatus").ToString
                    lblMgrComment.Text = strUser.Tables(0).Rows(0).Item("mgrcomment").ToString
                    lblMgr.Text = strUser.Tables(0).Rows(0).Item("mgrname").ToString
                    If IsDBNull(strUser.Tables(0).Rows(0).Item("mgrapprovaldate")) = False Then
                        lblMgrDate.Text = strUser.Tables(0).Rows(0).Item("mgrapprovaldate").ToString
                    End If

                    txtComment.Text = strUser.Tables(0).Rows(0).Item("hrcomment").ToString
                    If IsDBNull(strUser.Tables(0).Rows(0).Item("hrapprovaldate")) = False Then
                        lblApprovalDate.Text = strUser.Tables(0).Rows(0).Item("hrapprovaldate").ToString
                    End If

                    If cboApproval.SelectedItem.Text = "" Or cboApproval.SelectedItem.Text = "Pending" Then
                        lblApprovalDate.Visible = False
                        lblDate.Visible = False
                    Else
                        lblApprovalDate.Visible = True
                        lblDate.Visible = True

                    End If

                    If CDbl(lblHoursLogged.Text) > CDbl(lblDuration.Text) Then
                        lblOverTime.Text = CDbl(lblHoursLogged.Text) - CDbl(lblDuration.Text)
                        cboApproval.Enabled = True
                        btnAdd.Visible = True
                    Else
                        btnAdd.Visible = False
                        lblOverTime.Text = 0
                        cboApproval.Enabled = False
                        lblApprovalDate.Visible = False
                        lblDate.Visible = False
                    End If
                    lblHeADER.Text = strUser.Tables(0).Rows(0).Item("Name").ToString & " Attendance: " & lblStartDate.Text

                End If

            End If
        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub


    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Time_Employee_Attendance_HRUpdate_Status", txtid.Text, cboApproval.SelectedItem.Text, txtComment.Text, Session("UserEmpID"))

            lblstatus.Text = "Approval Status saved"

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


End Class