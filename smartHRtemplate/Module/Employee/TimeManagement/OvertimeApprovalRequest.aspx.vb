Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Microsoft.VisualBasic


Public Class OvertimeApprovalRequest
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
            strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Time_Employee_Attendance_Detail_My_Team", txtid.Text)
            If strUser.Tables(0).Rows.Count > 0 Then
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

                Process.MailNotification(checkindate, 2, checkindate & " Overtime Approval Request by " & mgrname & " on behalf of " & empname, msgbuild.ToString, empid, mgrid, Process.GetEmpIDMailList("hr"), empid, "")

                Dim Arrays() As String = Process.GetEmpIDMailList("hr").Split(Separators, StringSplitOptions.RemoveEmptyEntries)
                For i = 0 To Arrays.Count - 1
                    Process.MailNotification(checkindate, 2, checkindate & " Overtime Approval Request by " & mgrname & " on behalf of " & empname, msgbuild.ToString, Process.GetEmpIDMailList("hr"), mgrid, empid, Arrays(i), "")
                Next
            End If
           

   

            lblstatus.Text = "Overtime pay request forwarded to HR for further action!"

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


    Protected Sub dateFrom_SelectedDateChanged(sender As Object, e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles dateFrom.SelectedDateChanged
        Try
            Dim finalstatus As String = ""
            Dim stat As String = ""
            Dim strAttend As New DataSet
            strAttend = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Time_Employee_Attendance_Get_All", Request.QueryString("empid"), dateFrom.SelectedDate, dateFrom.SelectedDate)
            'strUser.Tables(0).Rows(0).Item("empid").ToString
            If strAttend.Tables(0).Rows.Count > 0 Then
                txtid.Text = strAttend.Tables(0).Rows(0).Item("id").ToString
                lblShift.Text = strAttend.Tables(0).Rows(0).Item("shifts").ToString
                lblDuration.Text = strAttend.Tables(0).Rows(0).Item("duration").ToString

                If IsDBNull(strAttend.Tables(0).Rows(0).Item("checkintime")) = False Then
                    lblHeADER.Text = strAttend.Tables(0).Rows(0).Item("EmployeeName").ToString & " Overtime Pay Request "
                    lblStartTime.Text = strAttend.Tables(0).Rows(0).Item("checkintime").ToString
                    lblEndTime.Text = strAttend.Tables(0).Rows(0).Item("checkouttime").ToString
                    lblEndDate.Text = strAttend.Tables(0).Rows(0).Item("checkoutdate").ToString
                    lblHoursLogged.Text = strAttend.Tables(0).Rows(0).Item("actualduration").ToString
                    txtagreedOvertime.Text = strAttend.Tables(0).Rows(0).Item("agreedovertime").ToString
                    txtovertimeindex.Text = strAttend.Tables(0).Rows(0).Item("overtimeindex").ToString
                    Process.AssignRadComboValue(cboApproval, strAttend.Tables(0).Rows(0).Item("mgrapprovalstatus").ToString)
                    txtComment.Text = strAttend.Tables(0).Rows(0).Item("mgrcomment").ToString
                    lblApprovalDate.Text = strAttend.Tables(0).Rows(0).Item("mgrapprovaldate").ToString
                    If CDbl(lblHoursLogged.Text) > CDbl(lblDuration.Text) Then
                        lblOverTime.Text = FormatNumber(CDbl(lblHoursLogged.Text) - CDbl(lblDuration.Text), 2)
                        Process.EnableButton(btnAdd)
                    Else
                        lblOverTime.Text = "0"
                        Process.DisableButton(btnAdd)
                    End If
                End If


            End If
        Catch ex As Exception

        End Try
    End Sub
End Class