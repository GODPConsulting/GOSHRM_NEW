Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports System.IO


Public Class CasualLeaves
    Inherits System.Web.UI.Page
    Dim ApplyLeave As New clsApplyLeave
    Dim ApproveLeave As New clsApproveLeave
    Dim AuthenCode As String = "LEAVE"
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

    'Dim isEligible As String = "Yes"

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


    Private Sub ValidateLeave(ByVal EmpID As String, ByVal LeaveType As String, ByVal DateFrom As Date, ByVal DateTo As Date, ByVal Location As String)
        Try
            Dim lblstatus As String = ""
            Process.loadalert(divalert1, msgalert1, "", "danger")
            If LeaveType.Trim = "" Or radStartDate.SelectedDate Is Nothing Or radEndDate.SelectedDate Is Nothing Then
                LeaveBalance = 0
                NoDays = 0
                aDays.Value = 0
            Else
                Dim strGrade As New DataSet
                strGrade = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Employee_Leave_Validate", EmpID, LeaveType, DateFrom, DateTo, Location)
                LeaveBalance = strGrade.Tables(0).Rows(0).Item("Balance").ToString
                NoDays = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "Select dbo.NoOfWorkingDays('" & Session("EmpGrade") & "','" & Process.DDMONYYYY(DateFrom) & "','" & Process.DDMONYYYY(DateTo) & "','" & Location & "')")    'strGrade.Tables(0).Rows(0).Item("NoOfDays").ToString
                aDays.Value = NoDays
            End If

            If Session("LeaveDays") > 0 Then
                If LeaveBalance < 0 Then
                    lblstatus = "You don't have sufficient leave days left!"
                    Process.loadalert(divalert1, msgalert1, lblstatus, "danger")
                End If
            End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Sub LoadGrid(empid As String)
        Try
            GridVwHeaderChckbox.DataSource = Process.SearchData("Employee_LeaveList_Approved_Outstanding", empid)
            GridVwHeaderChckbox.AllowSorting = True
            GridVwHeaderChckbox.AllowPaging = True
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            '
            If Not Me.IsPostBack Then
                If Request.UrlReferrer.ToString.ToLower.Contains("leavedetails") = False Then
                    Session("PreviousPage") = Request.UrlReferrer.ToString
                End If
                If Request.QueryString("id") IsNot Nothing Then

                    Dim Approver1Status As String = ""
                    'btnAdd.Visible = False
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Employee_Leavelist_get", Request.QueryString("id"))
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    lblLoanRefNo.Text = strUser.Tables(0).Rows(0).Item("refno").ToString
                    lblEmpID.Text = strUser.Tables(0).Rows(0).Item("empid").ToString
                    aempname.Value = strUser.Tables(0).Rows(0).Item("EmployeeName").ToString
                    aBalance.Value = strUser.Tables(0).Rows(0).Item("BalanceDays").ToString
                    aDays.Value = strUser.Tables(0).Rows(0).Item("NoofDays").ToString
                    'Emp_PersonalDetail_get_all                   
                    Session("EmpGrade") = strUser.Tables(0).Rows(0).Item("GradeLevel").ToString
                    amgrname.Value = strUser.Tables(0).Rows(0).Item("ApproverName1").ToString
                    Session("Mylocation") = strUser.Tables(0).Rows(0).Item("Location").ToString
                    areason.Value = strUser.Tables(0).Rows(0).Item("Reason").ToString
                    radStartDate.SelectedDate = strUser.Tables(0).Rows(0).Item("From").ToString
                    radEndDate.SelectedDate = strUser.Tables(0).Rows(0).Item("To").ToString
                    lblleavetype.Text = strUser.Tables(0).Rows(0).Item("LeaveName").ToString
                    lnkdownload.InnerText = strUser.Tables(0).Rows(0).Item("filename").ToString
                    amgrstatus.Value = strUser.Tables(0).Rows(0).Item("status").ToString
                    ahrstatus.Value = strUser.Tables(0).Rows(0).Item("status2").ToString
                    Session("ManagerID") = strUser.Tables(0).Rows(0).Item("Approver1").ToString


                    If IsDBNull(strUser.Tables(0).Rows(0).Item("SupervisorComment")) = True Then
                        divmgrcomment.Visible = False
                    Else
                        If strUser.Tables(0).Rows(0).Item("SupervisorComment").ToString = "" Then
                            divmgrcomment.Visible = False
                        Else
                            divmgrcomment.Visible = True
                            amgrcomment.Value = strUser.Tables(0).Rows(0).Item("SupervisorComment").ToString
                        End If
                    End If

                    If IsDBNull(strUser.Tables(0).Rows(0).Item("HRComment")) = True Then
                        divhrcomment.Visible = False
                    Else
                        If strUser.Tables(0).Rows(0).Item("HRComment").ToString = "" Then
                            divhrcomment.Visible = False
                        Else
                            ahrcomment.Value = strUser.Tables(0).Rows(0).Item("HRComment").ToString
                        End If
                    End If

                    If amgrstatus.Value.ToLower = "approved" Then
                        Me.radStartDate.Enabled = False
                        radEndDate.Enabled = False
                    End If
                Else
                    divmgrcomment.Visible = False
                    divhrcomment.Visible = False

                    txtid.Text = "0"
                    lblEmpID.Text = Session("UserEmpID")
                    lblleavetype.Text = "Casual Leave"

                    Dim strBal As New DataSet
                    strBal = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Leave_Chart_Type", lblEmpID.Text, "Annual Leave")
                    If strBal.Tables(0).Rows.Count > 0 Then
                        aBalance.Value = strBal.Tables(0).Rows(0).Item("totalbalance").ToString
                        lblBalance0.Text = strBal.Tables(0).Rows(0).Item("approvednottaken").ToString
                    End If
                    Dim msg As String = ""
                    If CInt(aBalance.Value) <= 0 Then
                        If CInt(lblBalance0.Text) <= 0 Then
                            Dim strCasualSet As New DataSet
                            strCasualSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Leave_Casual_Leave_Get_Setting", Session("EmpGrade"))
                            If strCasualSet.Tables(0).Rows.Count > 0 Then
                                If strBal.Tables(0).Rows(0).Item("LeaveCarriedForward").ToString.ToLower = "no" Then
                                    msg = "Insufficient annual leave days, requested casual leave days will be deducted from next Salary PAY!"
                                    Process.casualcheck = 1
                                    Process.loadalert(divalert, msgalert, msg, "warning")

                                Else
                                    Process.casualcheck = 2
                                    msg = "Insufficient annual leave days, requested casual leave days will be deducted from next year annual leave!"
                                    Process.loadalert(divalert, msgalert, msg, "warning")
                                End If
                            End If

                        Else
                            Process.casualcheck = 0
                            radStartDate.AutoPostBack = False
                            radEndDate.Visible = False
                            GridVwHeaderChckbox.Visible = True
                            LoadGrid(lblEmpID.Text)
                            msg = "Deduction from Available Annual Leaves to substitute for Casual"
                            Process.loadalert(divalert, msgalert, msg, "info")
                        End If
                    End If

                    Dim strGrade As New DataSet
                    strGrade = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_PersonalDetail_get", lblEmpID.Text)
                    aempname.Value = strGrade.Tables(0).Rows(0).Item("fullname").ToString
                    Session("EmpGrade") = strGrade.Tables(0).Rows(0).Item("Grade").ToString
                    Session("Mylocation") = strGrade.Tables(0).Rows(0).Item("Location").ToString
                    Session("EmpStatus") = strGrade.Tables(0).Rows(0).Item("jobstatus").ToString
                    amgrname.Value = strGrade.Tables(0).Rows(0).Item("ManagerName").ToString
                    Session("ManagerID") = strGrade.Tables(0).Rows(0).Item("SupervisorID").ToString
                    Session("approver1_emailaddr") = strGrade.Tables(0).Rows(0).Item("ManagerEmail").ToString
                    Session("emp_emailaddr") = strGrade.Tables(0).Rows(0).Item("MyMail").ToString
                End If
            End If
            If lnkdownload.InnerText.Trim = "" Then
                lnkclr.Visible = False
                lnkdownload.Visible = False
            Else
                lnkclr.Visible = True
                lnkdownload.Visible = True
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Function GetIdentityImage(ByVal loanref As String, ByVal empid As String, ByVal approver1 As String, ByVal sleavetype As String, _
                                 ByVal reason As String, ByVal leavefrom As Date, ByVal leaveto As Date, ByVal leavebal As Integer, _
                                  ByVal leavedays As Integer, ByVal slocation As String, ByVal addedby As String, _
                                  ByVal leavename As String, ByVal paydate As Date, ByVal fileimage As Byte(), ByVal filename As String, ByVal filetype As String, ByVal filesize As String) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Employee_Leavelist_Update_File"
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@EmpID", SqlDbType.VarChar).Value = empid
            cmd.Parameters.Add("@Approver1", SqlDbType.VarChar).Value = approver1
            cmd.Parameters.Add("@LeaveType", SqlDbType.VarChar).Value = sleavetype
            cmd.Parameters.Add("@Reason", SqlDbType.VarChar).Value = reason
            cmd.Parameters.Add("@LeaveFrom", SqlDbType.Date).Value = leavefrom
            cmd.Parameters.Add("@LeaveTo", SqlDbType.Date).Value = leaveto
            cmd.Parameters.Add("@LeaveBalance", SqlDbType.Int).Value = leavebal
            cmd.Parameters.Add("@NoOfDays", SqlDbType.Int).Value = leavedays
            cmd.Parameters.Add("@Location", SqlDbType.VarChar).Value = slocation
            cmd.Parameters.Add("@AddedBy", SqlDbType.VarChar).Value = addedby
            cmd.Parameters.Add("@LeaveName", SqlDbType.VarChar).Value = leavename
            cmd.Parameters.Add("@PayDate", SqlDbType.Date).Value = paydate
            cmd.Parameters.Add("@fileimage", SqlDbType.Image).Value = fileimage
            cmd.Parameters.Add("@filename", SqlDbType.VarChar).Value = filename
            cmd.Parameters.Add("@filetype", SqlDbType.VarChar).Value = filetype
            cmd.Parameters.Add("@filesize", SqlDbType.BigInt).Value = filesize
            cmd.Parameters.Add("@casualcheck", SqlDbType.Int).Value = Process.casualcheck
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception

            Process.loadalert(divalert, msgalert, ex.Message, "danger")
            Return 0
        End Try
    End Function
    Private Function GetIdentity(ByVal loanref As String, ByVal empid As String, ByVal approver1 As String, ByVal sleavetype As String, _
                                 ByVal reason As String, ByVal leavefrom As Date, ByVal leaveto As Date, ByVal leavebal As Integer, _
                                  ByVal leavedays As Integer, ByVal slocation As String, ByVal addedby As String, _
                                  ByVal leavename As String, ByVal paydate As Date) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Employee_Leavelist_Update"
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@EmpID", SqlDbType.VarChar).Value = empid
            cmd.Parameters.Add("@Approver1", SqlDbType.VarChar).Value = approver1
            cmd.Parameters.Add("@LeaveType", SqlDbType.VarChar).Value = sleavetype
            cmd.Parameters.Add("@Reason", SqlDbType.VarChar).Value = reason
            cmd.Parameters.Add("@LeaveFrom", SqlDbType.Date).Value = leavefrom
            cmd.Parameters.Add("@LeaveTo", SqlDbType.Date).Value = leaveto
            cmd.Parameters.Add("@LeaveBalance", SqlDbType.Int).Value = leavebal
            cmd.Parameters.Add("@NoOfDays", SqlDbType.Int).Value = leavedays
            cmd.Parameters.Add("@Location", SqlDbType.VarChar).Value = slocation
            cmd.Parameters.Add("@AddedBy", SqlDbType.VarChar).Value = addedby
            cmd.Parameters.Add("@LeaveName", SqlDbType.VarChar).Value = leavename
            cmd.Parameters.Add("@PayDate", SqlDbType.Date).Value = paydate
            cmd.Parameters.Add("@casualcheck", SqlDbType.Int).Value = Process.casualcheck
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception            
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
            Return 0
        End Try
    End Function
    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            System.Threading.Thread.Sleep(300)
            Dim msgbuild As New StringBuilder()
            Dim Separators() As Char = {";"c}
            Dim lblstatus As String = ""
     
            Process.loadalert(divalert, msgalert, lblstatus, "danger")

            If radEndDate.Visible = True Then
                If (radEndDate.SelectedDate Is Nothing) Then
                    lblstatus = "Leave End Date required!"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    radEndDate.Focus()
                    Exit Sub
                End If

                If radStartDate.SelectedDate > radEndDate.SelectedDate Then
                    lblstatus = "Leave End Date must be beyond Start Date!"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    radEndDate.Focus()
                    Exit Sub
                End If

                Dim startsyear As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select name from Leave_Period where '" & Process.DDMONYYYY(radStartDate.SelectedDate.Value) & "' between PeriodStart and PeriodEnd ")
                Dim endsyear As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select name from Leave_Period where '" & Process.DDMONYYYY(radEndDate.SelectedDate.Value) & "' between PeriodStart and PeriodEnd ")


                If startsyear <> endsyear Then
                    lblstatus = "Leave Duration must span within same year, if leave splill to next year create a second leave request for the next year!"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    radEndDate.Focus()
                    Exit Sub
                End If
            End If



            iID = txtid.Text
            If (radStartDate.SelectedDate Is Nothing) Then
                lblstatus = "Leave Start Date required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                radStartDate.Focus()
                Exit Sub
            End If

            If (areason.Value.Trim = "") Then
                lblstatus = "Please specify the reason for the leave to assist in the approval process.!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                areason.Focus()
                Exit Sub
            End If


            EmpID_1 = Session("ManagerID")
            Dim apaydate As Date
            apaydate = Date.Now.AddYears(-100)

            If Session("LeaveDays") > 0 Then
                If CInt(aBalance.Value) < 0 Then
                    lblstatus = "You do not have sufficient leave days left!"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    Exit Sub
                End If
            End If

            Dim daysadd As Integer = 0
            If radEndDate.Visible = False Then
                For i As Integer = 0 To GridVwHeaderChckbox.Rows.Count - 1
                    Dim controls As TextBox = DirectCast(GridVwHeaderChckbox.Rows(i).Cells(5).FindControl("txtDays"), TextBox)
                    Dim actualdays As String = GridVwHeaderChckbox.Rows(i).Cells(3).Text
                    '
                    Dim days As String = controls.Text
                    daysadd = daysadd + CInt(days)
                    If CInt(days) > CInt(actualdays) Then
                        lblstatus = "Error in available days selected, application not yet accepted!"
                        Process.loadalert(divalert, msgalert, lblstatus, "danger")
                        controls.Focus()
                        Exit Sub
                    End If
                Next
                aDays.Value = daysadd
            End If


            ApplyLeave.EmpID = lblEmpID.Text
            ApplyLeave.Approver1 = Session("ManagerID")
            ApplyLeave.LeaveFrom = radStartDate.SelectedDate
            If radEndDate.Visible = True Then
                ApplyLeave.LeaveTo = radEndDate.SelectedDate
            Else
                ApplyLeave.LeaveTo = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select dbo.EndWorkingDay('" & Session("EmpGrade") & "','" & Process.DDMONYYYY(ApplyLeave.LeaveFrom) & "','" & Session("Mylocation") & "'," & daysadd & ")")
            End If
            ApplyLeave.LeaveType = lblleavetype.Text
            ApplyLeave.Location = Session("Mylocation")
            ApplyLeave.Reason = areason.Value
            ApplyLeave.Leave = radStartDate.SelectedDate.Value.Year.ToString
            ApplyLeave.PayDate = apaydate

            Dim OldValue As String = ""
            Dim NewValue As String = ""




            If txtid.Text <> "0" And txtid.Text <> "" Then
                If Not file1.PostedFile Is Nothing Then
                    Dim strtype As String = ""
                    Dim strname As String = ""
                    Dim imgdata As Byte() = Nothing
                    Dim strsize As Integer = 0

                    Dim img_strm As Stream = file1.PostedFile.InputStream
                    Dim img_len As Integer = file1.PostedFile.ContentLength
                    strtype = file1.PostedFile.ContentType.ToString()
                    strname = Path.GetFileName(file1.PostedFile.FileName)
                    strsize = file1.PostedFile.ContentLength

                    imgdata = New Byte(img_len - 1) {}
                    Dim n As Integer = img_strm.Read(imgdata, 0, img_len)
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Employee_Leavelist_Update_File", lblLoanRefNo.Text, ApplyLeave.EmpID, ApplyLeave.Approver1, ApplyLeave.LeaveType, ApplyLeave.Reason, ApplyLeave.LeaveFrom, ApplyLeave.LeaveTo, aBalance.Value, aDays.Value, Session("Mylocation"), Session("LoginID"), ApplyLeave.LeaveFrom.Year, ApplyLeave.PayDate, imgdata, strname, strtype, strsize, Process.casualcheck)
                Else
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Employee_Leavelist_Update", lblLoanRefNo.Text, ApplyLeave.EmpID, ApplyLeave.Approver1, ApplyLeave.LeaveType, ApplyLeave.Reason, ApplyLeave.LeaveFrom, ApplyLeave.LeaveTo, aBalance.Value, aDays.Value, Session("Mylocation"), Session("LoginID"), ApplyLeave.LeaveFrom.Year, ApplyLeave.PayDate, Process.casualcheck)
                End If

            Else
                If Not file1.PostedFile Is Nothing Then
                    Dim strtype As String = ""
                    Dim strname As String = ""
                    Dim imgdata As Byte() = Nothing
                    Dim strsize As Integer = 0

                    Dim img_strm As Stream = file1.PostedFile.InputStream
                    Dim img_len As Integer = file1.PostedFile.ContentLength
                    strtype = file1.PostedFile.ContentType.ToString()
                    strname = Path.GetFileName(file1.PostedFile.FileName)
                    strsize = file1.PostedFile.ContentLength

                    imgdata = New Byte(img_len - 1) {}
                    Dim n As Integer = img_strm.Read(imgdata, 0, img_len)
                    txtid.Text = GetIdentityImage(txtid.Text, ApplyLeave.EmpID, ApplyLeave.Approver1, ApplyLeave.LeaveType, ApplyLeave.Reason, ApplyLeave.LeaveFrom, ApplyLeave.LeaveTo, aBalance.Value, aDays.Value, Session("Mylocation"), Session("LoginID"), ApplyLeave.LeaveFrom.Year, ApplyLeave.PayDate, imgdata, strname, strtype, strsize)
                Else
                    txtid.Text = GetIdentity(txtid.Text, ApplyLeave.EmpID, ApplyLeave.Approver1, ApplyLeave.LeaveType, ApplyLeave.Reason, ApplyLeave.LeaveFrom, ApplyLeave.LeaveTo, aBalance.Value, aDays.Value, Session("Mylocation"), Session("LoginID"), ApplyLeave.LeaveFrom.Year, ApplyLeave.PayDate)
                End If


                If txtid.Text = "0" Then                    
                    Exit Sub
                End If

                If txtid.Text.Length < 6 Then
                    lblLoanRefNo.Text = txtid.Text.PadLeft(6, "0")
                Else
                    lblLoanRefNo.Text = txtid.Text
                End If

            End If

            If radEndDate.Visible = False Then
                For i As Integer = 0 To GridVwHeaderChckbox.Rows.Count - 1
                    Dim controls As TextBox = DirectCast(GridVwHeaderChckbox.Rows(i).Cells(5).FindControl("txtDays"), TextBox)
                    Dim drpcontrols As DropDownList = DirectCast(GridVwHeaderChckbox.Rows(i).Cells(4).FindControl("drpMode"), DropDownList)
                    Dim fromdate As String = GridVwHeaderChckbox.Rows(i).Cells(1).Text
                    Dim todate As String = GridVwHeaderChckbox.Rows(i).Cells(2).Text
                    Dim actualdays As String = GridVwHeaderChckbox.Rows(i).Cells(3).Text
                    '
                    Dim days As String = controls.Text
                    Dim modes As String = drpcontrols.SelectedItem.Text
                    Dim ID As String = _
                             GridVwHeaderChckbox.Rows(i).Cells(0).Text

                    If CInt(days) > 0 Then
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Employee_Leave_Casual_Generate", txtid.Text, ID, modes, days)
                    End If
                Next
            End If



            Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Inserted", "Leave Application")

            If iID = "0" Then
                If Session("ManagerID").Contains("N/A") = False And Session("ManagerID") <> "" Then
                    Process.Leave_Notification_Supervisor(Session("approver1_emailaddr"), lblLoanRefNo.Text, aempname.Value, lblleavetype.Text, radStartDate.SelectedDate, ApplyLeave.LeaveTo, areason.Value, lblEmpID.Text, Session("ManagerID"), Process.ApplicationURL + "/" + Process.GetMailLink(AuthenCode, 1), Process.ApplicationURL + "/" + Process.GetMailLink(AuthenCode, 2))

                    Process.Leave_Notification_(lblLoanRefNo.Text, lblEmpID.Text, Process.ApplicationURL + "/" + Process.GetMailLink(AuthenCode, 3))
                    lblstatus = "Application saved and forwarded to " & amgrname.Value & " for approval"                    
                End If
            Else
                lblstatus = "Application updated"                
            End If
            Process.loadalert(divalert, msgalert, lblstatus, "success")

            Dim ssss As New DataSet
            ssss = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Employee_Leavelist_get", txtid.Text)
            If ssss.Tables(0).Rows.Count > 0 Then
                lnkdownload.InnerText = ssss.Tables(0).Rows(0).Item("filename").ToString
            Else
                lnkdownload.InnerText = ""
            End If
            If lnkdownload.InnerText = "" Then
                lnkclr.Visible = False
                lnkdownload.Visible = False
            Else
                lnkclr.Visible = True
                lnkdownload.Visible = True
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

    Protected Sub radStartDate_SelectedDateChanged(sender As Object, e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles radStartDate.SelectedDateChanged
        If radStartDate.SelectedDate < Now.Date Then
            Dim lblstatus As String = "Leave Start Date cannot be in the past!"
            Process.loadalert(divalert1, msgalert1, lblstatus, "danger")
            radStartDate.SelectedDate = Now.Date
            radStartDate.Focus()
            Exit Sub
        End If

        If lblleavetype.Text Is Nothing Or radStartDate.SelectedDate Is Nothing Or radEndDate.SelectedDate Is Nothing Then
        Else
            ValidateLeave(lblEmpID.Text, lblleavetype.Text, radStartDate.SelectedDate, radEndDate.SelectedDate, Session("Mylocation"))

        End If

    End Sub

    Protected Sub radEndDate_SelectedDateChanged(sender As Object, e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles radEndDate.SelectedDateChanged
        If radStartDate.SelectedDate.Value.Year <> radEndDate.SelectedDate.Value.Year Then
            Dim lblstatus As String = "Leave Duration must span within same year, if leave splill to next year create a second leave request for the next year!"
            Process.loadalert(divalert1, msgalert1, lblstatus, "warning")
            radEndDate.Focus()
            Exit Sub
        End If

        If lblleavetype.Text Is Nothing Or radStartDate.SelectedDate Is Nothing Or radEndDate.SelectedDate Is Nothing Then
        Else     
            ValidateLeave(lblEmpID.Text, lblleavetype.Text, radStartDate.SelectedDate, radEndDate.SelectedDate, Session("Mylocation"))
        End If
    End Sub

    Protected Sub lnkClear_Click(sender As Object, e As EventArgs)
        Try

            Dim lblstatus As String = ""
            If amgrstatus.Value.ToLower = "approved" Then
                lblstatus = "request is already approved, clear aborted!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
            Else
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Employee_Leavelist_Update_File_Clear", txtid.Text)
                lblstatus = "Attachment cleared"
                Process.loadalert(divalert, msgalert, lblstatus, "success")
                lnkdownload.InnerText = ""            
            End If

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
            Dim lblstatus As String = ""
            If lnkdownload.InnerText.Trim = "" Then
                lblstatus = "No download"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
            Else

                Dim dt As DataTable = Process.SearchData("Employee_Leavelist_get", txtid.Text)
                If dt IsNot Nothing Then
                    downloadFile(CType(dt.Rows(0)("fileimage"), Byte()), dt.Rows(0)("filetype").ToString(), dt.Rows(0)("filename").ToString())
                End If
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub GridVwHeaderChckbox_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridVwHeaderChckbox.RowDataBound
        Try
            Dim ddlCountries As DropDownList = CType(e.Row.FindControl("drpMode"), DropDownList)
            ddlCountries.Items.Insert(0, New ListItem("Top"))
            ddlCountries.Items.Insert(1, New ListItem("Bottom"))
        Catch ex As Exception

        End Try
    End Sub
End Class