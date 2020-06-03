Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Microsoft.VisualBasic


Public Class OvertimeApproval
    Inherits System.Web.UI.Page
    Dim TimeSheet As New clsTimeSheet
    Dim ApproveLeave As New clsApproveLeave
    Dim AuthenCode As String = "APPATTENDANCE"
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
                    Session("EmpID") = strUser.Tables(0).Rows(0).Item("empid").ToString
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    Process.AssignRadComboValue(cboApproval, strUser.Tables(0).Rows(0).Item("mgrapprovalstatus").ToString)

                    lblhrstatus.Text = strUser.Tables(0).Rows(0).Item("hrapprovalstatus").ToString
                    If strUser.Tables(0).Rows(0).Item("mgrapprovalstatus").ToString.ToLower <> "approved" Then
                        lblhrstatus.Visible = False
                        lblhrstatuslabel.Visible = False
                    End If
                    lblShift.Text = strUser.Tables(0).Rows(0).Item("shifts").ToString
                    lblDuration.Text = strUser.Tables(0).Rows(0).Item("duration").ToString
                    lblStartTime.Text = strUser.Tables(0).Rows(0).Item("checkintime").ToString
                    lblEndTime.Text = strUser.Tables(0).Rows(0).Item("checkouttime").ToString
                    lblHoursLogged.Text = strUser.Tables(0).Rows(0).Item("actualduration").ToString
                    lblStartDate.Text = strUser.Tables(0).Rows(0).Item("checkindate").ToString
                    lblEndDate.Text = strUser.Tables(0).Rows(0).Item("checkoutdate").ToString
                    txtComment.Text = strUser.Tables(0).Rows(0).Item("mgrcomment").ToString
                    txtagreedOvertime.Text = strUser.Tables(0).Rows(0).Item("agreedovertime").ToString
                    If IsDBNull(strUser.Tables(0).Rows(0).Item("overtimeindex").ToString) = True Or CDbl(strUser.Tables(0).Rows(0).Item("overtimeindex").ToString) = 0 Then
                        Dim strIndex As New DataSet
                        strIndex = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Payroll_Options_Get_Company", Process.GetCompanyByEmpID(Session("EmpID")))
                        If strIndex.Tables(0).Rows.Count > 0 Then
                            If IsDBNull(strIndex.Tables(0).Rows(0).Item("overtimeindex").ToString()) = True Then
                                txtovertimeindex.Text = "1"
                            Else
                                txtovertimeindex.Text = strIndex.Tables(0).Rows(0).Item("overtimeindex").ToString()
                            End If
                        Else
                            txtovertimeindex.Text = "1"
                        End If

                    Else
                        txtovertimeindex.Text = strUser.Tables(0).Rows(0).Item("overtimeindex").ToString()
                    End If

                    

                    If cboApproval.SelectedItem.Text = "" Or cboApproval.SelectedItem.Text.ToLower = "pending" Then
                        lblApprovalDate.Visible = False
                        lblDate.Visible = False
                    Else
                        lblApprovalDate.Visible = True
                        lblDate.Visible = True
                    End If

                    If CDbl(lblHoursLogged.Text) > CDbl(lblDuration.Text) Then
                        lblOverTime.Text = CDbl(lblHoursLogged.Text) - CDbl(lblDuration.Text)
                    Else
                        lblOverTime.Text = 0
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
            Dim msgbuild As New StringBuilder()
            Dim Separators() As Char = {";"c}

            If IsNumeric(txtagreedOvertime.Text) = False Then
                lblstatus.Text = "Invalid overtime figure set!"
                txtagreedOvertime.Focus()
                Exit Sub
            End If

            If IsNumeric(txtovertimeindex.Text) = False Then
                lblstatus.Text = "Invalid overtime index figure set!"
                txtovertimeindex.Focus()
                Exit Sub
            End If

            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Time_Employee_Attendance_Update_Status", txtid.Text, cboApproval.SelectedItem.Text, txtComment.Text, Session("UserEmpID"), txtagreedOvertime.Text, txtovertimeindex.Text)

            Dim strUser As New DataSet
            strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Time_Employee_Attendance_Detail_My_Team", Request.QueryString("id"))
            Dim empid As String = strUser.Tables(0).Rows(0).Item("empid").ToString
            Dim empname As String = strUser.Tables(0).Rows(0).Item("Name").ToString
            Dim mgrid As String = strUser.Tables(0).Rows(0).Item("managerid").ToString
            Dim dateshift As String = strUser.Tables(0).Rows(0).Item("datenames").ToString
            Dim shifts As String = strUser.Tables(0).Rows(0).Item("shifts").ToString
            Dim checkindate As String = strUser.Tables(0).Rows(0).Item("checkindate").ToString
            Dim checkoutdate As String = strUser.Tables(0).Rows(0).Item("checkoutdate").ToString
            Dim checkintime As String = strUser.Tables(0).Rows(0).Item("checkindate").ToString
            Dim checkouttime As String = strUser.Tables(0).Rows(0).Item("checkoutdate").ToString
            Dim shiftduration As Double = strUser.Tables(0).Rows(0).Item("duration")
            Dim actualduration As Double = strUser.Tables(0).Rows(0).Item("actualduration")
            Dim mgrname As String = strUser.Tables(0).Rows(0).Item("MgrName").ToString

            msgbuild.Clear()
            msgbuild.AppendLine("Employee       : " & empname)
            msgbuild.AppendLine(" ")
            msgbuild.AppendLine("Shift          : " & shifts)
            msgbuild.AppendLine("Duration (Hrs) : " & shiftduration.ToString)
            msgbuild.AppendLine(" ")
            msgbuild.AppendLine("CheckIn Date   : " & checkindate)
            msgbuild.AppendLine("CheckIn Time   : " & checkintime)
            msgbuild.AppendLine(" ")
            msgbuild.AppendLine("CheckOut Date  : " & checkoutdate)
            msgbuild.AppendLine("CheckOut Time  : " & checkouttime)
            msgbuild.AppendLine(" ")
            msgbuild.AppendLine("Work Hours     : " & actualduration.ToString)
            msgbuild.AppendLine("System Overtime: " & (actualduration - shiftduration).ToString)
            msgbuild.AppendLine(" ")
            msgbuild.AppendLine("Approval Stat  : " & cboApproval.SelectedItem.Text & " by " & mgrname)
            msgbuild.AppendLine(" ")
            msgbuild.AppendLine("Agreed Overtime (Hr): " & txtagreedOvertime.Text)
            msgbuild.AppendLine("Overtime Index: " & txtovertimeindex.Text)

            Process.MailNotification(checkindate, 2, "Re: " & checkindate & " Overtime Approval Request by " & empname, msgbuild.ToString, empid, mgrid, Process.GetEmpIDMailList("hr"), empid, "")

            Dim Arrays() As String = Process.GetEmpIDMailList("hr").Split(Separators, StringSplitOptions.RemoveEmptyEntries)
            For i = 0 To Arrays.Count - 1
                Process.MailNotification(checkindate, 2, "Re: " & checkindate & " Overtime Approval Request by " & empname, msgbuild.ToString, Process.GetEmpIDMailList("hr"), mgrid, empid, Arrays(i), "")
            Next

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