Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Microsoft.VisualBasic


Public Class EmployeeShiftUpdate
    Inherits System.Web.UI.Page
    Dim TimeSheet As New clsTimeSheet
    Dim ApproveLeave As New clsApproveLeave
    Dim AuthenCode As String = "TEAMSHIFT"
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
                Process.LoadRadDropDownTextAndValue(radShift, "Job_Work_Shift_Get_All", "shiftname", "shiftname", False)
                Process.LoadRadComboTextAndValueInitiateP1(cboEmployee, "Emp_PersonalDetail_Get_ReportingToMe", Session("UserEmpID"), "--Select--", "Employee", "EmpID")
                If Request.QueryString("id") IsNot Nothing Then
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Job_Employee_Shift_Team_Get", Request.QueryString("id"))

                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    Process.AssignRadDropDownValue(radShift, strUser.Tables(0).Rows(0).Item("shiftname").ToString)
                    Process.AssignRadComboValue(cboEmployee, strUser.Tables(0).Rows(0).Item("empid").ToString)
                    radStartDate.SelectedDate = CDate(strUser.Tables(0).Rows(0).Item("datefrom"))
                    radEndDate.SelectedDate = CDate(strUser.Tables(0).Rows(0).Item("dateto"))

                    If strUser.Tables(0).Rows(0).Item("datestr").ToString.ToUpper = "PRESENT" Then
                        chkCurrent.Checked = True
                        lblDateTo.Visible = False
                        radEndDate.Visible = False
                    Else
                        chkCurrent.Checked = False
                        lblDateTo.Visible = True
                        radEndDate.Visible = True
                    End If
                Else
                    txtid.Text = "0"
                    chkCurrent.Checked = True
                    lblDateTo.Visible = False
                    radEndDate.Visible = False
                End If

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Function GetIdentity(ByVal shiftname As String, ByVal empid As String, ByVal datefrom As Date, ByVal datestr As String) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Job_Employee_Shift_Update"
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@shiftname", SqlDbType.VarChar).Value = shiftname
            cmd.Parameters.Add("@empid", SqlDbType.VarChar).Value = empid
            cmd.Parameters.Add("@datefrom", SqlDbType.Date).Value = datefrom
            cmd.Parameters.Add("@datestr", SqlDbType.VarChar).Value = datestr
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception
            Process.strExp = ex.Message
            Return 0
        End Try
    End Function

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            Dim strEndDate As String = ""
            If Request.QueryString("id") IsNot Nothing Then
                If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False And Process.AuthenAction(Session("role"), AuthenCode, "Update") = False Then
                    Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "danger")
                    Exit Sub
                End If
            Else
                If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False And Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                    Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "danger")
                    Exit Sub
                End If
            End If
            Process.loadalert(divalert, msgalert, "Record saving, please wait ...", "success")
            Dim lblstatus As String = ""
            If (radShift.SelectedValue Is Nothing) Then
                lblstatus = "Shift Name required!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                radShift.Focus()
                Exit Sub
            End If

            If (radStartDate.SelectedDate Is Nothing) Then
                lblstatus = "Employee Shift Start Date required!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                radStartDate.Focus()
                Exit Sub
            End If

            If chkCurrent.Checked = False Then
                If (radEndDate.SelectedDate Is Nothing) Then
                    lblstatus = "Employee Shift End Date required!"
                    Process.loadalert(divalert, msgalert, lblstatus, "danger")
                    radEndDate.Focus()
                    Exit Sub
                Else
                    If radStartDate.SelectedDate > radEndDate.SelectedDate Then
                        lblstatus = "Start Date cannot be beyond End Date!"
                        Process.loadalert(divalert, msgalert, lblstatus, "danger")
                        radEndDate.Focus()
                        Exit Sub
                    Else
                        strEndDate = radEndDate.SelectedDate
                    End If

                End If
            Else
                strEndDate = "Present"
            End If

            btnAdd.EnableViewState = False


            If txtid.Text <> "0" And txtid.Text.Trim <> "" Then
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Job_Employee_Shift_Update", txtid.Text, radShift.SelectedValue, cboEmployee.SelectedValue, radStartDate.SelectedDate, strEndDate)

            Else
                txtid.Text = GetIdentity(radShift.SelectedValue, cboEmployee.SelectedValue, radStartDate.SelectedDate, strEndDate)
                If txtid.Text = "0" Then
                    lblstatus = Process.strExp
                    Process.loadalert(divalert, msgalert, lblstatus, "danger")
                    Exit Sub
                End If
            End If


            lblstatus = "Record saved"
            Process.loadalert(divalert, msgalert, lblstatus, "success")

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        Finally
            btnAdd.EnableViewState = True
        End Try
    End Sub


    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect("~/Module/Employee/TimeManagement/EmployeeShift.aspx", True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    
    Protected Sub chkCurrent_CheckedChanged(sender As Object, e As EventArgs) Handles chkCurrent.CheckedChanged
        Try
            If chkCurrent.Checked = True Then
                lblDateTo.Visible = False
                radEndDate.Visible = False
            Else
                lblDateTo.Visible = True
                radEndDate.Visible = True
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class