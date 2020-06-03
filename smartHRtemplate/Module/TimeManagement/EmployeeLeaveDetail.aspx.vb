Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Microsoft.Reporting.WebForms
Imports System.IO

Public Class EmployeeLeaveDetail
    Inherits System.Web.UI.Page
    Dim ApplyLeave As New clsApplyLeave
    Dim ApproveLeave As New clsApproveLeave
    Dim AuthenCode As String = "TIMLEAVE"
    Dim olddata(3) As String
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
    Dim emailFile As String = ConfigurationManager.AppSettings("EmailURL")
    'Dim PayrollFile As String = ""
    'Dim isEligible As String = "Yes"

    Private Sub LoadChart(ByVal empid As String, ByVal leavetype As String)
        Try
            gridLeaveChart.DataSource = Process.SearchDataP2("Emp_Leave_Chart_Type", empid, leavetype)
            gridLeaveChart.AllowSorting = False
            gridLeaveChart.AllowPaging = False
            gridLeaveChart.DataBind()
        Catch ex As Exception
            lblstatus.Text = ex.Message
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    Private Function ValidatePeriod(ByVal sDate As Date, ByVal LeaveYear As String) As Boolean
        Try
            'select PeriodStart, PeriodEnd   from Leave_Period where name = ''
            Dim validate As Boolean = False
            Dim strPeriod As New DataSet
            strPeriod = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "select PeriodStart, PeriodEnd   from Leave_Period where name = '" & LeaveYear & "'")
            Dim startPeriod As Date = CDate(strPeriod.Tables(0).Rows(0).Item("PeriodStart"))
            Dim endPeriod As Date = CDate(strPeriod.Tables(0).Rows(0).Item("PeriodEnd"))

            If (startPeriod <= sDate) And (sDate <= endPeriod) Then
                validate = True
            Else
                validate = False
            End If
            Return validate
        Catch ex As Exception
            Return False
        End Try
    End Function

    
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            '
            If Not Me.IsPostBack Then
                radApproval.Items.Clear()
                radApproval.Items.Add("Pending")
                radApproval.Items.Add("Approved")
                radApproval.Items.Add("Cancelled")
                radApproval.Items.Add("Rejected")

                'Company_Structure_get_parent
                If Request.QueryString("id") IsNot Nothing Then
                    Dim Approver1Status As String = ""
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Employee_Leavelist_get", Request.QueryString("id"))
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    lblLoanRefNo.Text = strUser.Tables(0).Rows(0).Item("refno").ToString
                    lblEmpID.Text = strUser.Tables(0).Rows(0).Item("empid").ToString
                    lblEmpName.Text = strUser.Tables(0).Rows(0).Item("EmployeeName").ToString
                    lblDays.Text = strUser.Tables(0).Rows(0).Item("NoofDays").ToString
                    lblLocation.Text = strUser.Tables(0).Rows(0).Item("Location").ToString
                    lblreason.Text = strUser.Tables(0).Rows(0).Item("Reason").ToString
                    radStartDate.SelectedDate = CDate(strUser.Tables(0).Rows(0).Item("From"))
                    radEndDate.SelectedDate = CDate(strUser.Tables(0).Rows(0).Item("To"))
                    lblLeaveType.Text = strUser.Tables(0).Rows(0).Item("LeaveType").ToString
                    lblMgrComment.Text = strUser.Tables(0).Rows(0).Item("SupervisorComment").ToString
                    lblSupervisor.Text = strUser.Tables(0).Rows(0).Item("ApproverName1").ToString
                    lblLeave.Text = strUser.Tables(0).Rows(0).Item("LeaveName").ToString
                    lblMgrID.Text = strUser.Tables(0).Rows(0).Item("Approver1").ToString
                    If IsDBNull(strUser.Tables(0).Rows(0).Item("PayDate")) = False Then
                        lblPayDate.Text = CDate(strUser.Tables(0).Rows(0).Item("PayDate")).ToLongDateString
                    End If
                    lbllength.Text = strUser.Tables(0).Rows(0).Item("leavelength").ToString
                    txtComment.Text = strUser.Tables(0).Rows(0).Item("hrcomment").ToString
                    lblSupervisorApproval.Text = strUser.Tables(0).Rows(0).Item("Status").ToString
                    lblstatustemp.Text = strUser.Tables(0).Rows(0).Item("Status").ToString
                    lblstatustemp2.Text = strUser.Tables(0).Rows(0).Item("Status2").ToString
                    Process.AssignRadComboValue(radApproval, strUser.Tables(0).Rows(0).Item("Status2").ToString)
                    lblGradeLevel.Text = strUser.Tables(0).Rows(0).Item("gradelevel").ToString
                    lblcreatedby.Text = strUser.Tables(0).Rows(0).Item("addedby").ToString
                    If IsDBNull(strUser.Tables(0).Rows(0).Item("addedon")) = False Then
                        lblcreatedon.Text = strUser.Tables(0).Rows(0).Item("addedon").ToString
                    End If
                    Session("emp_emailaddr") = strUser.Tables(0).Rows(0).Item("EmpMail").ToString

                    If lblSupervisorApproval.Text = "Pending" Then
                        radApproval.Enabled = False
                    Else
                        radApproval.Enabled = True
                        btnStatus.Visible = True
                    End If
                    lblfilename.Text = strUser.Tables(0).Rows(0).Item("filename").ToString
                    If lblfilename.Text = "" Then
                        lblfilelabel.Visible = False
                        lblfilename.Visible = False
                        lnkDownloadAttach.Visible = False
                    Else
                        lblfilelabel.Visible = True
                        lblfilename.Visible = True
                        lnkDownloadAttach.Visible = True
                    End If
                    LoadChart(lblEmpID.Text, lblLeaveType.Text)
                    If (CDate(lblPayDate.Text) < CDate(lblcreatedon.Text)) Then
                        payline.Visible = False
                    End If
                End If

            End If
        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub

    

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Response.Redirect("~/Module/TimeManagement/EmployeeLeaves.aspx")
        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnStatus_Click(sender As Object, e As EventArgs) Handles btnStatus.Click
        Try
            Dim LevelApproval As Integer = 0
            Dim OldValue As String = ""
            Dim NewValue As String = ""

            Dim j As Integer = 0

            If Request.QueryString("id") IsNot Nothing Then
                Dim strUser As New DataSet
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Employee_Leavelist_Update_HR_Status", txtid.Text, radApproval.SelectedItem.Text, txtComment.Text, Session("UserEmpID"), Session("LoginID"), radStartDate.SelectedDate.Value, radEndDate.SelectedDate.Value, CInt(lblDays.Text))
                'SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Employee_Leavelist_Update_HR_Status", txtid.Text, radApproval.SelectedItem.Text, txtComment.Text, Session("UserEmpID"), Session("LoginID"), CDate(radStartDate.SelectedDate.Value), CDate(radEndDate.SelectedDate.Value), CInt(lblDays.Text))
                Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Updated Leave " & lblLoanRefNo.Text, "Leave Approval")
                lblstatus.Text = "Approval Status Updated"

                Dim strGrade As New DataSet
                Dim approvername As String = ""
                strGrade = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "select name, email from dbo.Employees_All where empid = '" & Session("UserEmpID") & "'")
                If strGrade.Tables(0).Rows.Count > 0 Then
                    approver2_emailaddr = strGrade.Tables(0).Rows(0).Item("email").ToString
                    approvername = strGrade.Tables(0).Rows(0).Item("name").ToString
                End If

                'get final status
                Dim strFinalStatus As New DataSet
                Dim finalstatus As String = ""
                Dim approver1name As String = ""
                Dim approver2name As String = ""
                Dim status_approver1name As String = ""
                Dim status_approver2name As String = ""
                strFinalStatus = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Employee_Leavelist_get", lblLoanRefNo.Text)
                If strFinalStatus.Tables(0).Rows.Count > 0 Then
                    finalstatus = strFinalStatus.Tables(0).Rows(0).Item("FinalStatus").ToString
                    status_approver1name = strFinalStatus.Tables(0).Rows(0).Item("status").ToString
                    status_approver2name = strFinalStatus.Tables(0).Rows(0).Item("status2").ToString
                    approver1name = strFinalStatus.Tables(0).Rows(0).Item("Approver1Name").ToString
                    approver2name = strFinalStatus.Tables(0).Rows(0).Item("Approver2Name").ToString
                End If

                'Process.Leave_HR_Level2_Approval(Process.GetMailList("hr"), lblLoanRefNo.Text, lblEmpName.Text, lblLeaveType.Text, radStartDate.SelectedDate, radEndDate.SelectedDate, lblreason.Text, lblSupervisorApproval.Text, approvername)
                Session("rptAttachment") = ""
                If status_approver1name.ToLower = "approved" Then
                    LodaDataTable(txtid.Text)
                    Process.Leave_Notification_Final(lblLoanRefNo.Text, lblEmpName.Text, lblLeaveType.Text, radStartDate.SelectedDate, radEndDate.SelectedDate, lblreason.Text, status_approver1name, approver1name, status_approver2name, approver2name, lblEmpID.Text, lblMgrID.Text, Session("rptAttachment"), Process.ApplicationURL & "/" & Process.GetMailLink(AuthenCode, 1), Process.ApplicationURL & "/" & Process.GetMailLink(AuthenCode, 4))
                End If


            End If


        Catch ex As Exception
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
            lblstatus.Text = ex.Message
        End Try
    End Sub
    Private Sub LodaDataTable(id As String)
        Dim dtEarning As New DataTable
        dtEarning = Process.SearchData("Employee_Leavelist_get", id)
        GenerateLeaveLetter(dtEarning, Process.GetData("general_info_get"), Server.MapPath(emailFile & "LeaveLetter" & id & ".PDF"))
    End Sub
    Private Sub GenerateLeaveLetter(dtearn As DataTable, logos As DataTable, ByVal savePath As String)
        Dim ReportViewer1 As New ReportViewer
        ReportViewer1.ProcessingMode = ProcessingMode.Local
        ReportViewer1.SizeToReportContent = True
        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Module/Reports/TimeManagement/LeaveLetter.rdlc")
        Dim _rsource As New ReportDataSource("Leave", dtearn)
        Dim _rsource2 As New ReportDataSource("logo", logos)
        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.LocalReport.DataSources.Add(_rsource)
        ReportViewer1.LocalReport.DataSources.Add(_rsource2)
        ReportViewer1.LocalReport.Refresh()
        Dim Bytes() As Byte = ReportViewer1.LocalReport.Render("PDF", "", Nothing, Nothing, Nothing, Nothing, Nothing)

        If File.Exists(savePath) Then
            File.Delete(savePath)
        End If

        Using Stream As New FileStream(savePath, FileMode.Create)
            Stream.Write(Bytes, 0, Bytes.Length)
        End Using
        Session("rptAttachment") = savePath
    End Sub

    Private Sub ValidateLeave(ByVal EmpID As String, ByVal LeaveType As String, ByVal DateFrom As Date, ByVal DateTo As Date, ByVal Location As String)
        Try
            NoDays = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "Select dbo.NoOfWorkingDays('" & lblGradeLevel.Text & "','" & Process.DDMONYYYY(DateFrom) & "','" & Process.DDMONYYYY(DateTo) & "','" & Location & "')")    'strGrade.Tables(0).Rows(0).Item("NoOfDays").ToString
            lblDays.Text = NoDays


        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub
    Protected Sub radStartDate_SelectedDateChanged(sender As Object, e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles radStartDate.SelectedDateChanged
        If radStartDate.SelectedDate < Now.Date Then
            lblstatus.Text = "Leave Start Date cannot be in the past!"
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & lblstatus.Text + "')", True)
            radStartDate.SelectedDate = Now.Date
            radStartDate.Focus()
            Exit Sub
        End If

        If lblLeaveType.Text Is Nothing Or radStartDate.SelectedDate Is Nothing Or radEndDate.SelectedDate Is Nothing Then
        Else
            If ValidatePeriod(radStartDate.SelectedDate, radStartDate.SelectedDate.Value.Year) = False Then
                lblstatus.Text = "Selected Leave StartDate is outside the selected leave calendar year!"
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & lblstatus.Text + "')", True)
                radStartDate.Focus()
                Exit Sub
            End If

            ValidateLeave(lblEmpID.Text, lblLeaveType.Text, radStartDate.SelectedDate, radEndDate.SelectedDate, lblLocation.Text)

        End If
    End Sub

    Protected Sub radEndDate_SelectedDateChanged(sender As Object, e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles radEndDate.SelectedDateChanged
        If radStartDate.SelectedDate.Value.Year <> radEndDate.SelectedDate.Value.Year Then
            lblstatus.Text = "Leave Duration must span within same year, if leave splill to next year create a second leave request for the next year!"
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & lblstatus.Text + "')", True)
            radEndDate.Focus()
            Exit Sub
        End If

        If lblLeaveType.Text Is Nothing Or radStartDate.SelectedDate Is Nothing Or radEndDate.SelectedDate Is Nothing Then
        Else
            If ValidatePeriod(radEndDate.SelectedDate, radEndDate.SelectedDate.Value.Year) = False Then
                lblstatus.Text = "Selected Leave EndDate is outside the selected leave calendar year!"
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & lblstatus.Text + "')", True)
                radEndDate.Focus()
                Exit Sub
            End If
            ValidateLeave(lblEmpID.Text, lblLeaveType.Text, radStartDate.SelectedDate, radEndDate.SelectedDate, lblLocation.Text)
        End If
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
    Protected Sub lnkDownloadAttach_Click(sender As Object, e As EventArgs) Handles lnkDownloadAttach.Click
        Try
            lblstatus.Text = ""
            If lblfilename.Text.Trim = "" Then
                lblstatus.Text = "No download"
            Else

                Dim dt As DataTable = Process.SearchData("Employee_Leavelist_get", txtid.Text)
                If dt IsNot Nothing Then
                    downloadFile(CType(dt.Rows(0)("fileimage"), Byte()), dt.Rows(0)("filetype").ToString(), dt.Rows(0)("filename").ToString())
                End If
            End If
        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub
End Class