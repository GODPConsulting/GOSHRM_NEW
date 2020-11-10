Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Microsoft.VisualBasic


Public Class OvertimeRequest
    Inherits System.Web.UI.Page
    Dim TimeSheet As New clsTimeSheet
    Dim ApproveLeave As New clsApproveLeave
    Dim AuthenCode As String = "EMPATTEND"
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
                If Request.QueryString("id") IsNot Nothing Then
                    Dim strUser As New DataSet
                    Dim oBool As Boolean = False
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Time_Employee_Attendance_Detail_My_Team", Request.QueryString("id"))
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    lblManager.Text = strUser.Tables(0).Rows(0).Item("MgrName").ToString
                    lblHeADER.Text = strUser.Tables(0).Rows(0).Item("DateNames").ToString & ": Overtime Pay Request"
                    txtComment.Text = strUser.Tables(0).Rows(0).Item("empcomment").ToString
                    If CDbl(strUser.Tables(0).Rows(0).Item("actualduration")) > CDbl(strUser.Tables(0).Rows(0).Item("duration")) Then
                        lblAgreedOvertime.Text = CDbl(strUser.Tables(0).Rows(0).Item("actualduration")) - CDbl(strUser.Tables(0).Rows(0).Item("duration"))
                    Else
                        lblAgreedOvertime.Text = "0"
                    End If

                    oBool = strUser.Tables(0).Rows(0).Item("overtimepayrequest")
                    If oBool = True Then
                        txtComment.Enabled = False
                        Process.DisableButton(btnAdd)
                        lblstatus.Text = "Request already initiated!"
                    End If
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

            If txtComment.Text.Trim = "" Then
                lblstatus.Text = "Enter comment, to make approval process easier!"
                txtComment.Focus()
                Exit Sub
            End If

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
            Dim grade As String = strUser.Tables(0).Rows(0).Item("grade").ToString
            Dim company As String = strUser.Tables(0).Rows(0).Item("company").ToString
            Dim Link = Process.ApplicationURL & "/" & "Module/TimeManagement/OvertimeApprovals.aspx?id=" & Request.QueryString("id")

            Dim strCheck As New DataSet
            strCheck = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Payroll_Attendance_Exception_Check", company, grade)
            If strCheck.Tables(0).Rows.Count > 0 Then
                lblstatus.Text = "employees on " & grade & " are not eligible for overtime!"
                Exit Sub
            End If

            
            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Time_Employee_Attendance_Overtime_Request", txtid.Text, txtComment.Text, lblAgreedOvertime.Text)

        

            msgbuild.Clear()
            msgbuild.AppendLine("Employee      : " & empname)
            msgbuild.AppendLine(" ")
            msgbuild.AppendLine("Shift         : " & shifts)
            msgbuild.AppendLine("Duration (Hrs): " & shiftduration.ToString)
            msgbuild.AppendLine(" ")
            msgbuild.AppendLine("CheckIn Date  : " & checkindate)
            msgbuild.AppendLine("CheckIn Time  : " & checkintime)
            msgbuild.AppendLine(" ")
            msgbuild.AppendLine("CheckOut Date : " & checkoutdate)
            msgbuild.AppendLine("CheckOut Time : " & checkouttime)
            msgbuild.AppendLine(" ")
            msgbuild.AppendLine("Work Hours    : " & actualduration.ToString)
            msgbuild.AppendLine("Overtime      : " & (actualduration - shiftduration).ToString)
            msgbuild.AppendLine("Follow this Link:" & Link)

            Process.MailNotification(checkindate, 2, checkindate & " Overtime Approval Request by " & empname, msgbuild.ToString, mgrid, empid, Process.GetEmpIDMailList("hr"), mgrid, "")

            Dim Arrays() As String = Process.GetEmpIDMailList("hr").Split(Separators, StringSplitOptions.RemoveEmptyEntries)
            For i = 0 To Arrays.Count - 1
                Process.MailNotification(checkindate, 2, checkindate & " Overtime Approval Request by " & empname, msgbuild.ToString, mgrid, empid, Process.GetEmpIDMailList("hr"), Arrays(i), "")
            Next

            msgbuild.Clear()
            msgbuild.AppendLine("Your overtime request has been forwarded to " & lblManager.Text & " for approval")
            Process.MailNotification(checkindate, 2, checkindate & " Overtime Request has been forwarded", msgbuild.ToString, empid, Process.AppName, "", empid, "")

            lblstatus.Text = "Overtime Pay Request sent to " & lblManager.Text & " for approval"
            Process.DisableButton(btnAdd)
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