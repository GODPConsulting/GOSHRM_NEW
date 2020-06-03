Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports System.IO


Public Class LeaveDetails
    Inherits System.Web.UI.Page
    Dim ApplyLeave As New clsApplyLeave
    Dim ApproveLeave As New clsApproveLeave
    Dim AuthenCode As String = "LEAVECAL"

    Dim olddata(3) As String
    Dim LeaveBalance As Integer = 0
    Dim NoDays As Integer = 0
    Dim usedLeave As Integer = 0
    Dim entitled As Integer = 0
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

            If LeaveType.Trim = "" Or radStartDate.SelectedDate Is Nothing Or radEndDate.SelectedDate Is Nothing Then
                LeaveBalance = 0
                NoDays = 0
                aDays.Value = 0
            Else
                Dim strGrade As New DataSet
                strGrade = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Employee_Leave_Validate", EmpID, LeaveType, DateFrom, DateTo, Location)
                LeaveBalance = strGrade.Tables(0).Rows(0).Item("Balance").ToString
                entitled = strGrade.Tables(0).Rows(0).Item("Entitlement").ToString
                Session("LeaveDays") = entitled
                usedLeave = strGrade.Tables(0).Rows(0).Item("used").ToString
                NoDays = strGrade.Tables(0).Rows(0).Item("NoOfDays").ToString 'SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "Select dbo.NoOfWorkingDays('" & Session("EmpGrade") & "','" & Process.DDMONYYYY(DateFrom) & "','" & Process.DDMONYYYY(DateTo) & "','" & Location & "')")    'strGrade.Tables(0).Rows(0).Item("NoOfDays").ToString
                If (radlength.SelectedItem.Value = "Full") Then
                    aDays.Value = NoDays
                Else
                    aDays.Value = NoDays / 2
                End If
            End If

            If entitled > 0 Then
                If LeaveBalance < 0 Then
                    Process.loadalert(divalert1, msgalert1, "You don't have sufficient leave days (" & (entitled - usedLeave).ToString & ") left!", "danger")
                    btnupdate.Visible = False
                    Exit Sub
                Else
                    btnupdate.Visible = True
                    Process.loadalert(divalert1, msgalert1, "", "danger")
                End If
            End If

        Catch ex As Exception
            Process.loadalert(divalert1, msgalert1, ex.Message, "danger")
        End Try
    End Sub
    Private Sub LoadDetails(ByVal id As Integer)
        Dim Approver1Status As String = ""
        'btnAdd.Visible = False
        Dim strUser As New DataSet
        strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Employee_Leavelist_get", id)
        txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
        lblLoanRefNo.Text = strUser.Tables(0).Rows(0).Item("refno").ToString
        lblEmpID.Text = strUser.Tables(0).Rows(0).Item("empid").ToString
        aempname.Value = strUser.Tables(0).Rows(0).Item("EmployeeName").ToString
        lblBalance.Text = strUser.Tables(0).Rows(0).Item("BalanceDays").ToString
        aDays.Value = strUser.Tables(0).Rows(0).Item("NoofDays").ToString
        'Emp_PersonalDetail_get_all                   
        Session("EmpGrade") = strUser.Tables(0).Rows(0).Item("GradeLevel").ToString
        amgrname.Value = strUser.Tables(0).Rows(0).Item("ApproverName1").ToString
        Session("Mylocation") = strUser.Tables(0).Rows(0).Item("Location").ToString
        areason.Value = strUser.Tables(0).Rows(0).Item("Reason").ToString
        radStartDate.SelectedDate = strUser.Tables(0).Rows(0).Item("From").ToString
        radEndDate.SelectedDate = strUser.Tables(0).Rows(0).Item("To").ToString
        lblleavetype.Text = strUser.Tables(0).Rows(0).Item("Leavetype").ToString
        lnkdownload.InnerText = strUser.Tables(0).Rows(0).Item("filename").ToString
        amgrstatus.Value = strUser.Tables(0).Rows(0).Item("status").ToString
        ahrstatus.Value = strUser.Tables(0).Rows(0).Item("status2").ToString
        Session("ManagerID") = strUser.Tables(0).Rows(0).Item("Approver1").ToString
        Process.AssignRadComboValue(radlength, strUser.Tables(0).Rows(0).Item("leavelength").ToString)
        pagetitle.InnerText = lblleavetype.Text.ToUpper

        If CDate(strUser.Tables(0).Rows(0).Item("PayDate")) > CDate("12-DEC-1950") Then
            divpaydate.Visible = True
            If IsDBNull(strUser.Tables(0).Rows(0).Item("PayDate")) = False Then
                radPayDate.SelectedDate = strUser.Tables(0).Rows(0).Item("PayDate")
            End If
        Else
            divpaydate.Visible = False
        End If

        If IsDBNull(strUser.Tables(0).Rows(0).Item("SupervisorComment")) = True Then
            divmgrcomment.Visible = False
        Else
            If strUser.Tables(0).Rows(0).Item("SupervisorComment").ToString = "" Then
                divmgrcomment.Visible = False
            Else
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

        If amgrstatus.Value.ToUpper = "APPROVED" Then
            Me.radStartDate.Enabled = False
            radEndDate.Enabled = False
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                If Request.UrlReferrer.ToString.ToLower.Contains("leavedetails") = False Then
                    Session("PreviousPage") = Request.UrlReferrer.ToString
                End If

                If Request.QueryString("id") IsNot Nothing Then
                    LoadDetails(Request.QueryString("id"))
                Else
                    divmgrcomment.Visible = False
                    divhrcomment.Visible = False
                    If Request.QueryString("leaveid") IsNot Nothing Then

                        txtid.Text = "0"
                        lblEmpID.Text = Session("UserEmpID")
                        Dim strleavetype As New DataSet
                        strleavetype = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Leave_Type_get", Request.QueryString("leaveid"))
                        If (strleavetype.Tables(0).Rows.Count > 0) Then
                            lblleavetype.Text = strleavetype.Tables(0).Rows(0).Item("name").ToString 'Request.QueryString("leavetype")
                        End If

                        lblBalance.Text = Request.QueryString("balances")
                        pagetitle.InnerText = lblleavetype.Text.ToUpper

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

                        Dim ispayables As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "Select dbo.IsLeavePayable('" & lblEmpID.Text & "','" & lblleavetype.Text & "','" & Process.DDMONYYYY(DateTime.Now) & "')")

                        If ispayables.ToLower() = "yes" Then
                            divpaydate.Visible = True
                        Else
                            divpaydate.Visible = False
                        End If

                        Dim strLeaverule As New DataSet
                        strLeaverule = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Leave_Rule_get_grade", lblleavetype.Text, Session("EmpGrade"), Session("EmpStatus"))
                        If strLeaverule.Tables(0).Rows.Count > 0 Then
                            Session("LeaveDays") = strLeaverule.Tables(0).Rows(0).Item("LeavePerYear")
                        End If
                    End If
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
    Private Function GetIdentityImage(ByVal loanref As String, ByVal empid As String, ByVal approver1 As String, ByVal sleavetype As String,
                                 ByVal reason As String, ByVal leavefrom As Date, ByVal leaveto As Date, ByVal leavebal As Integer,
                                  ByVal leavedays As Integer, ByVal slocation As String, ByVal addedby As String,
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
            cmd.Parameters.Add("@casualcheck", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@leavelength", SqlDbType.VarChar).Value = radlength.SelectedItem.Value
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
            Return 0
        End Try
    End Function
    Private Function GetIdentity(ByVal loanref As String, ByVal empid As String, ByVal approver1 As String, ByVal sleavetype As String,
                                 ByVal reason As String, ByVal leavefrom As Date, ByVal leaveto As Date, ByVal leavebal As Integer,
                                  ByVal leavedays As Integer, ByVal slocation As String, ByVal addedby As String,
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
            cmd.Parameters.Add("@casualcheck", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@leavelength", SqlDbType.VarChar).Value = radlength.SelectedItem.Value
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



            Dim count = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "CheckLeaveExist", radStartDate.SelectedDate, radEndDate.SelectedDate, lblEmpID.Text)
            If count > 0 Then
                Process.loadalert(divalert, msgalert, "You have an Active Leave Between the Date selected!", "warning")
                Exit Sub
            End If

            If radPayDate.Visible = True Then
                If radPayDate.SelectedDate IsNot Nothing Then
                    If radPayDate.SelectedDate < radStartDate.SelectedDate Then
                        Process.loadalert(divalert, msgalert, "allowance payment can not be paid before leave schedule!", "warning")

                        Exit Sub
                    End If
                End If
            End If

            lbleligible.Text = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "value_LeaveGenderSpecific", lblEmpID.Text, lblGradeLevel.Text, lblleavetype.Text)


            If Session("LeaveDays") <> 0 Then
                If lbleligible.Text.ToUpper <> "YES" Then
                    Process.loadalert(divalert, msgalert, "Employee not eligible for this leave!", "warning")
                    Exit Sub
                End If
            End If

            Dim startsyear As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select name from Leave_Period where '" & Process.DDMONYYYY(radStartDate.SelectedDate.Value) & "' between PeriodStart and PeriodEnd ")
            Dim endsyear As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select name from Leave_Period where '" & Process.DDMONYYYY(radEndDate.SelectedDate.Value) & "' between PeriodStart and PeriodEnd ")

            Dim msg As String = ""
            If startsyear <> endsyear Then
                msg = "Leave Duration must span within same year, if leave spill to next year create a second leave request for the next year!"
                Process.loadalert(divalert, msgalert, msg, "warning")
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & msg + "')", True)
                radEndDate.Focus()
                Exit Sub
            End If

            iID = txtid.Text
            If (radStartDate.SelectedDate Is Nothing) Then
                msg = "Leave Start Date required!"
                Process.loadalert(divalert, msgalert, msg, "warning")
                radStartDate.Focus()
                Exit Sub
            End If

            If (radEndDate.SelectedDate Is Nothing) Then
                msg = "Leave End Date required!"
                Process.loadalert(divalert, msgalert, msg, "warning")
                radEndDate.Focus()
                Exit Sub
            End If

            If radStartDate.SelectedDate > radEndDate.SelectedDate Then
                msg = "Leave End Date must be beyond Start Date!"
                Process.loadalert(divalert, msgalert, msg, "warning")
                radEndDate.Focus()
                Exit Sub
            End If

            If (areason.Value.Trim = "") Then
                msg = "Please specify the reason for the leave to assist in the approval process.!"
                Process.loadalert(divalert, msgalert, msg, "warning")
                areason.Focus()
                Exit Sub
            End If



            EmpID_1 = Session("ManagerID")
            Dim apaydate As Date
            If radPayDate.Visible = False Or radPayDate.SelectedDate Is Nothing Then
                apaydate = Date.Now.AddYears(-100)
            Else
                apaydate = radPayDate.SelectedDate
            End If

            If Session("LeaveDays") > 0 Then
                If CInt(lblBalance.Text) < 0 Then
                    msg = "You do not have sufficient leave days left!"
                    Process.loadalert(divalert, msgalert, msg, "warning")
                    ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You do not have sufficient leave days left!" + "')", True)
                    Exit Sub
                End If
            End If



            ApplyLeave.EmpID = lblEmpID.Text
            ApplyLeave.Approver1 = Session("ManagerID")
            ApplyLeave.LeaveFrom = radStartDate.SelectedDate
            ApplyLeave.LeaveTo = radEndDate.SelectedDate
            ApplyLeave.LeaveType = lblleavetype.Text
            ApplyLeave.Location = Session("Mylocation")
            ApplyLeave.Reason = areason.Value
            ApplyLeave.Leave = radStartDate.SelectedDate.Value.Year.ToString
            ApplyLeave.PayDate = apaydate

            Dim OldValue As String = ""
            Dim NewValue As String = ""

            Dim j As Integer = 0

            For Each a In GetType(clsApplyLeave).GetProperties() 'New Entries
                If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
                    If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                        If a.GetValue(ApplyLeave, Nothing) = Nothing Then
                            NewValue += a.Name + ":" + " " & vbCrLf
                        Else
                            NewValue += a.Name + ": " + a.GetValue(ApplyLeave, Nothing).ToString & vbCrLf
                        End If
                    End If
                End If
            Next


            If txtid.Text <> "0" And txtid.Text <> "" Then
                If file1.PostedFile.ContentLength > 0 Then
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
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Employee_Leavelist_Update_File", lblLoanRefNo.Text, ApplyLeave.EmpID, ApplyLeave.Approver1, ApplyLeave.LeaveType, ApplyLeave.Reason, ApplyLeave.LeaveFrom, ApplyLeave.LeaveTo, lblBalance.Text, aDays.Value, Session("Mylocation"), Session("LoginID"), ApplyLeave.LeaveFrom.Year, ApplyLeave.PayDate, imgdata, strname, strtype, strsize, 0, radlength.SelectedItem.Value)
                Else
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Employee_Leavelist_Update", lblLoanRefNo.Text, ApplyLeave.EmpID, ApplyLeave.Approver1, ApplyLeave.LeaveType, ApplyLeave.Reason, ApplyLeave.LeaveFrom, ApplyLeave.LeaveTo, lblBalance.Text, aDays.Value, Session("Mylocation"), Session("LoginID"), ApplyLeave.LeaveFrom.Year, ApplyLeave.PayDate, 0, radlength.SelectedItem.Value)
                End If

            Else
                If file1.PostedFile.ContentLength > 0 Then
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
                    txtid.Text = GetIdentityImage(txtid.Text, ApplyLeave.EmpID, ApplyLeave.Approver1, ApplyLeave.LeaveType, ApplyLeave.Reason, ApplyLeave.LeaveFrom, ApplyLeave.LeaveTo, lblBalance.Text, aDays.Value, Session("Mylocation"), Session("LoginID"), ApplyLeave.LeaveFrom.Year, ApplyLeave.PayDate, imgdata, strname, strtype, strsize)
                Else
                    txtid.Text = GetIdentity(txtid.Text, ApplyLeave.EmpID, ApplyLeave.Approver1, ApplyLeave.LeaveType, ApplyLeave.Reason, ApplyLeave.LeaveFrom, ApplyLeave.LeaveTo, lblBalance.Text, aDays.Value, Session("Mylocation"), Session("LoginID"), ApplyLeave.LeaveFrom.Year, ApplyLeave.PayDate)
                End If


                If txtid.Text = "0" Then
                    Exit Sub
                End If

                If txtid.Text.Length < 6 And txtid.Text <> "0" Then
                    lblLoanRefNo.Text = txtid.Text.PadLeft(6, "0")
                Else
                    lblLoanRefNo.Text = txtid.Text
                End If

            End If

            Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Inserted", "Leave Application")

            If iID = "0" Then
                If Session("ManagerID").Contains("N/A") = False And Session("ManagerID") <> "" Then
                    Process.Leave_Notification_(lblLoanRefNo.Text, lblEmpID.Text, Process.ApplicationURL + "/" + Process.GetMailLink(AuthenCode, 3))
                    Process.Leave_Notification_Supervisor(Session("approver1_emailaddr"), lblLoanRefNo.Text, aempname.Value, lblleavetype.Text, radStartDate.SelectedDate, radEndDate.SelectedDate, areason.Value, lblEmpID.Text, Session("ManagerID"), Process.ApplicationURL + "/" + Process.GetMailLink(AuthenCode, 1), Process.ApplicationURL + "/" + Process.GetMailLink(AuthenCode, 2))


                    msg = "Application saved and forwarded to " & amgrname.Value & " for approval"
                    Process.loadalert(divalert, msgalert, msg, "success")
                End If
            Else
                msg = "Application updated"
                Process.loadalert(divalert, msgalert, msg, "success")
            End If

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
        Process.loadalert(divalert1, msgalert1, "", "warning")
        If radStartDate.SelectedDate Is Nothing Then
            Exit Sub
        End If
        If radEndDate.SelectedDate Is Nothing Then
            Exit Sub
        End If
        If radStartDate.SelectedDate < Now.Date Then
            Process.loadalert(divalert1, msgalert1, "Leave Start Date cannot be in the past!", "warning")
            radStartDate.SelectedDate = Now.Date
            radStartDate.Focus()
            Exit Sub
        End If

        If lblleavetype.Text Is Nothing Or radStartDate.SelectedDate Is Nothing Or radEndDate.SelectedDate Is Nothing Then
        Else
            'If ValidatePeriod(radStartDate.SelectedDate, radStartDate.SelectedDate.Value.Year) = False Then
            '    lblstatus.Text = "Selected Leave StartDate is outside the selected leave calendar year!"
            '    ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & lblstatus.Text + "')", True)
            '    radStartDate.Focus()
            '    Exit Sub
            'End If
            ValidateLeave(lblEmpID.Text, lblleavetype.Text, radStartDate.SelectedDate, radEndDate.SelectedDate, Session("Mylocation"))

        End If

    End Sub

    Protected Sub radEndDate_SelectedDateChanged(sender As Object, e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles radEndDate.SelectedDateChanged
        Process.loadalert(divalert1, msgalert1, "", "warning")
        If radStartDate.SelectedDate Is Nothing Then
            Exit Sub
        End If
        If radEndDate.SelectedDate Is Nothing Then
            Exit Sub
        End If
        If radStartDate.SelectedDate < Now.Date Then
            Process.loadalert(divalert1, msgalert1, "Leave Start Date cannot be in the past!", "warning")
            radStartDate.SelectedDate = Now.Date
            radStartDate.Focus()
            Exit Sub
        End If
        If radStartDate.SelectedDate.Value.Year <> radEndDate.SelectedDate.Value.Year Then
            Dim msgstr As String = "Leave Duration must span within same year, if leave splill to next year create a second leave request for the next year!"
            Process.loadalert(divalert1, msgalert1, msgstr, "warning")
            radEndDate.Focus()
            Exit Sub
        End If

        If lblleavetype.Text Is Nothing Or radStartDate.SelectedDate Is Nothing Or radEndDate.SelectedDate Is Nothing Then
        Else
            ValidateLeave(lblEmpID.Text, lblleavetype.Text, radStartDate.SelectedDate, radEndDate.SelectedDate, Session("Mylocation"))
            Dim ispayables As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "Select dbo.IsLeavePayable('" & lblEmpID.Text & "','" & lblleavetype.Text & "','" & Process.DDMONYYYY(radEndDate.SelectedDate) & "')")
            If ispayables.ToLower() = "yes" Then
                divpaydate.Visible = True
            Else
                divpaydate.Visible = False
            End If
        End If
    End Sub

    Protected Sub lnkClear_Click(sender As Object, e As EventArgs)
        Try
            Process.loadalert(divalert, msgalert, "", "warning")

            If amgrstatus.Value.ToLower = "approved" Then
                Process.loadalert(divalert, msgalert, "request is already approved, clear aborted!", "warning")
            Else
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Employee_Leavelist_Update_File_Clear", txtid.Text)
                Process.loadalert(divalert, msgalert, "Attachment cleared", "success")
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

            If lnkdownload.InnerText = "" Then
                Process.loadalert(divalert, msgalert, "No download", "danger")
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

    Protected Sub btnInvisible_Click(sender As Object, e As EventArgs) Handles btnApprove.Click
        Try
            Dim confirmValue As String = Request.Form("confirm_value")
            If confirmValue = "Yes" Then
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Employee_Leavelist_Cancel", Request.QueryString("id"))
                LoadDetails(Request.QueryString("id"))
            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub radlength_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles radlength.SelectedIndexChanged
        Try
            If lblleavetype.Text Is Nothing Or radStartDate.SelectedDate Is Nothing Or radEndDate.SelectedDate Is Nothing Then
            Else
                ValidateLeave(lblEmpID.Text, lblleavetype.Text, radStartDate.SelectedDate, radEndDate.SelectedDate, Session("Mylocation"))
                Dim ispayables As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "Select dbo.IsLeavePayable('" & lblEmpID.Text & "','" & lblleavetype.Text & "','" & Process.DDMONYYYY(radEndDate.SelectedDate) & "')")
                If ispayables.ToLower() = "yes" Then
                    divpaydate.Visible = True
                Else
                    divpaydate.Visible = False
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class