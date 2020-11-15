Imports System.Security
Imports System.Collections.Generic
Imports System.Security.Cryptography
Imports System.IO
Imports System.Globalization
Imports System.Configuration
Imports System.Configuration.ConfigurationSettings
Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports AjaxControlToolkit
Imports Telerik.Web.UI
Imports System.Net
Imports System.Net.Mail
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.FileIO
Imports System.Web
Imports System.Web.UI
Imports System.Web.HttpServerUtility
Imports System.Web.Services
Imports System.Web.Script.Services
Imports System.ComponentModel
Imports System.Management
Imports System.Net.NetworkInformation
Imports System.Xml
Imports System.Web.SessionState
Imports System.Text
Imports System.Collections
Imports System.DirectoryServices
Imports Twilio
Imports Twilio.Rest.Api.V2010.Account
Imports Twilio.Types
Imports com.esendex.sdk.messaging


'Imports Microsoft.Office.Core
'Imports Microsoft.Office.Interop
'Imports Microsoft.Office.Interop.Excel

Public Class Process

#Region "Variables"
    Public Shared privilegemsg As String = "You don't have privilege to perform this action"
    Public Shared nofile As String = "No file selected to upload!"
    Public Shared byts As Byte() = ASCIIEncoding.ASCII.GetBytes("ZeroCool")
    Public Shared AppName As String = ConfigurationManager.AppSettings("AppName")
    Public Shared URL As String = ConfigurationManager.AppSettings("URL")

    Public Shared PhotoURL As String = ConfigurationManager.AppSettings("PhotoURL")
    Public Shared FileURL As String = ConfigurationManager.AppSettings("FileURL")
    Public Shared sampleCSV As String = ConfigurationManager.AppSettings("sampleCSV")
    Public Shared QuestImageURL As String = ConfigurationManager.AppSettings("TrainingImageURL")
    Public Shared TrainingImageURL As String = ConfigurationManager.AppSettings("TrainingQuestImageURL")
    Public Shared apptype As String = ConfigurationManager.AppSettings("apptype")
    Public Shared domainname As String = ConfigurationManager.AppSettings("domain")
    Public Shared hostsite As String = ConfigurationManager.AppSettings("GOSHRMSite")
    Public Shared MailContentURL As String = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings("MailContentURL"))
    'TrainingQuestImageURL

    'Public Shared applicantid As Integer = 0
    Public Shared ismulti As String = ""
    Public Shared saved As Boolean = False
    Public Shared appcycleid As Integer = 0
    Public Shared casualcheck As Integer = 0 '0 regular, 1 Pay, 2 Carry Forward
    Public Shared requestedURL As String = ""
    Public Shared company As String = ""
    Public Shared companylist As String = ""
    Public Shared selectlist As String = ""
    Shared _sortDirection As String = ""
    Shared dtTable As System.Data.DataTable
    Public Shared track As String = ""
    Public Shared criteria As String = ""
    Public Shared loadtype As String = ""
    Public Shared loginid As String = ""
    Public Shared username As String = ""
    Public Shared name As String = ""
    Public Shared applicantid As String = ""
    Public Shared role As String = ""
    Public Shared strExp As String = ""
    Public Shared roletype As String = ""
    'Public Shared uploadCount As Integer = 0
    Public Shared disagree As Integer = 0
    Public Shared checkid As Integer = 0
    Public Shared Arrays() As String
    Public Shared Separators() As Char = {":"c}
    Public Shared Separators1() As Char = {"/"c}
    Public Shared Separators2() As Char = {"\"c}
    Public Shared Separators3() As Char = {"."c}

    Public Shared curchatcount As Integer = 0
    Public Shared prevchatcount As Integer = 0
    Public Shared tabindex1 As Integer = 0

    Public Shared LeaveDays As Integer = 0
    'Public Shared FinalApproval As String = ""
    'Public Shared EmpID As String = ""
    Public Shared Date1 As Date = Date.Now
    Public Shared Date2 As Date = Date.Now
    Public Shared HRTeam As String = "Human Resource Team"
    Public Shared FinTeam As String = "Finance Team"
    'Public Shared emp_emailaddr As String = ""
    'Public Shared approver1_emailaddr As String = ""
    'Public Shared approver2_emailaddr As String = ""
    Public Shared msgbuild As New StringBuilder()
    Public Shared msgbuild1 As New StringBuilder()
    Public Shared SeparatorSemi() As Char = {";"c}
    Public Shared view As String = ""

    Public Shared MailLeave As Integer = 1
    Public Shared MailTimeSheet As Integer = 2
    Public Shared MailEmployeeConfirm As Integer = 3
    Public Shared MailLoanRequest As Integer = 4
    Public Shared MailWorkForcePlan As Integer = 5
    Public Shared MailEmployeeExit As Integer = 6
    Public Shared MailQuery As Integer = 7
    Public Shared MailRecruitment As Integer = 8
    Public Shared MailTraining As Integer = 9
    Public Shared MailPayroll As Integer = 10
    Public Shared MailPerfromance As Integer = 12
    Public Shared MailQualification As Integer = 13
    Public Shared MailSuccessionPlan As Integer = 14
    Public Shared MailPromotion As Integer = 15
    Public Shared MailBlog As Integer = 16

    Public Shared MailFolderLeave As String = "Leave\"
    Public Shared MailFolderWorkForce As String = "WorkForcePlan\"
    Public Shared MailFolderQuery As String = "Query\"
    Public Shared MailFolderExit As String = "EmployeeExit\"
    Public Shared MailFolderRecruitment As String = "Recruitment\"
    Public Shared MailFolderPromotion As String = "Promotion\"
    Public Shared MailFolderConfirmation As String = "Confirmation\"
    Public Shared MailFolderPerformance As String = "Performance\"
    Public Shared MailFolderQualification As String = "Qualification\"
    Public Shared MailFolderSuccession As String = "Succession\"
    Public Shared MailFolderLoan As String = "Loan\"
    Public Shared MailFolderTimeSheet As String = "TimeSheet\"
    Public Shared MailFolderTraining As String = "Training\"
    Public Shared MailFolderBlog As String = "Blog\"
    'Public Shared confirmtype As String = ""
    Public Shared rptAttachment As String = ""
    Public Shared WorkForceBudgetDate As String = ""
    Public Shared WorkForcePlanDate As String = ""
    Shared random As New Random()
    Private Declare Function inet_addr Lib "wsock32.dll" (ByVal s As String) As Integer

    Private Declare Function SendARP Lib "iphlpapi.dll" (ByVal DestIP As Integer, ByVal SrcIP As Integer, ByRef pMACAddr As Integer, ByRef PhyAddrLen As Integer) As Integer

    Private Declare Sub CopyMemory Lib "kernel32" Alias "RtlMoveMemory" (ByRef dst As Byte, ByRef src As Integer, ByVal bcount As Integer)
#End Region
#Region "Sessions"
    '1	Leave
    '2	Time Sheet
    '3	Employee Confirmation
    '4	Loan Request
    '5	Workforce Plan
    '6	Employee Exit
    '7	Query

    'General
    'Session("exception")
    'Session("isnew")
    'Session("LastName")
    'Session("Organisation")
    'Session("Access")
    'Session("Level")
    'Session("Dept")
    'Session("View")
    'Session("IPAddress")
    'Session("LoginID") -- Login ID
    'Session("UserEmpID") -- User Emp ID
    'Session("EmpName") -- User Emp ID
    'Session("role") --Login role
    'Session("roletype")   Admin/ESS
    'Session("create")
    'Session("delete")
    'Session("update")
    'Session("EmpID")
    'Session("ID")
    'Session("LoadType")
    'Session("LoginEmail")
    'Session("PasswordChange")
    'Session("PreviousPage")
    'Session("PreviousPage2")
    'Session("Page")
    'Session("UserJobgrade")
    'Session("PayrollYear")
    'Session("Userjobtitle")
    'Recruitment

    'Session("JobID")
    'Session("stage")
    'Session("ProjectID")
    'Session("JobTestID")
    'Session("JobTest")
    'Session("MailList")
    'Session("ApplcantID")  Email Address is Applicant
    'Session("AppID") 
    'Session("ApplcantName")
    'Session("ApplicantJobID") Job ID when applying
    'Session("QuestionNo")  Test Question No
    'Session("ApplicationID") ID on Recruit Application table
    'Session("ApplicationTestID") ID of Applicant in Recruit Test table
    'Session("Interviewer")
    'Session("InterviewerID")
    'Approval Status
    '("Approved")
    '("Cancelled")
    '("Rejected")
    '("Pending")

    'Project/Task Status
    'Completed
    'In progress
    'Cancelled
    'Not Started

    'System
    'Open
    'Locked
    'Closed

    'Dashboard
    'Session("ADM")
    'Session("REC")
    'Session("EMP")
    'Session("PERF")
    'Session("TIME")
    'Session("TRA")
    'Session("FIN")
    'Session("REP")

    'Mail
    'Leave
    'Payroll
    'Appraisal
    'Timesheet




#End Region
#Region "Basic Actions"
    'Public Shared Sub Textbox_AdjustHeight(txtbody As TextBox)
    '    Try
    '        Dim chars As Integer = 0
    '        Dim charRows As Integer = 0
    '        Dim tbcontent As String = txtbody.Text

    '        txtbody.Columns = 80
    '        chars = tbcontent.Length
    '        charRows = chars / txtbody.Columns

    '        Dim remaining As Integer = chars - charRows * txtbody.Columns

    '        If remaining = 0 Then
    '            txtbody.Rows = charRows
    '            txtbody.TextMode = TextBoxMode.MultiLine
    '        Else
    '            txtbody.Rows = charRows + 1
    '            txtbody.TextMode = TextBoxMode.MultiLine
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub
    Public Shared Function GenerateKey() As String
        Dim value As String = CStr(random.Next(1, 999999))
        Return value.PadLeft(6, "0")
    End Function
    Public Shared Sub downloadFile(strURL As String)
        Try
            'HttpContext.Current.Server.MapPath(strURL)
            Dim req As New WebClient()
            Dim response As HttpResponse = HttpContext.Current.Response
            response.Clear()
            response.ClearContent()
            response.ClearHeaders()
            response.Buffer = True
            response.AddHeader("Content-Disposition", "attachment;filename=""" + strURL + """")
            Dim data As Byte() = req.DownloadData(strURL)
            response.BinaryWrite(data)

            response.Flush()
            response.[End]()
        Catch ex As Exception
        End Try
    End Sub

    Public Shared Sub SaveFiles(ByVal FileUpload3 As FileUpload, ByVal certtype As String, certname As String, certfile As Byte(), certsize As Integer)
        Try
            Dim img_strm As Stream = FileUpload3.PostedFile.InputStream
            certsize = FileUpload3.PostedFile.ContentLength
            certtype = FileUpload3.PostedFile.ContentType.ToString()
            certname = Path.GetFileName(FileUpload3.PostedFile.FileName)
            certfile = New Byte(certsize - 1) {}
            Dim n As Integer = img_strm.Read(certfile, 0, certsize)
        Catch ex As Exception
        End Try
    End Sub
    Public Shared Sub DisableButton(ByVal buttons As Button)
        Try
            buttons.Enabled = False
            buttons.BackColor = Color.Gray
            'buttons.Visible = False
        Catch ex As Exception
        End Try
    End Sub
    Public Shared Sub EnableButton(ByVal buttons As Button, Optional ByVal r As Integer = 76, Optional ByVal g As Integer = 175, Optional ByVal b As Integer = 80)
        Try
            buttons.Enabled = True
            buttons.BackColor = Color.FromArgb(r, g, b)
            'buttons.Visible = True
        Catch ex As Exception
        End Try
    End Sub
    Public Shared Sub DeactivateButton(ByVal buttons As Button)
        Try
            '102,153,0
            'buttons.BackColor = Color.Gray '76, 175, 80
            buttons.Font.Bold = True
            buttons.CssClass = "btn btn-info"
        Catch ex As Exception
        End Try
    End Sub
    Public Shared Sub ActivateButton(ByVal buttons As Button)
        Try
            'buttons.BackColor = Color.Green
            buttons.Font.Bold = True
            buttons.CssClass = "btn btn-success"
        Catch ex As Exception
        End Try
    End Sub

    Public Shared Sub activatehtmlmenu(menuhtml As HtmlControls.HtmlButton)
        Try
            menuhtml.Attributes.Remove("class")
            menuhtml.Attributes.Add("class", "btn btn-success btn-lg")
        Catch ex As Exception
        End Try
    End Sub
    Public Shared Sub deactivatehtmlmenu(menuhtml As HtmlControls.HtmlButton)
        Try
            menuhtml.Attributes.Remove("class")
            menuhtml.Attributes.Add("class", "btn btn-default btn-lg")
        Catch ex As Exception
        End Try
    End Sub
    Public Shared Sub loadalert(divalert As HtmlControls.HtmlGenericControl, msgalert As HtmlControls.HtmlGenericControl, message As String, Optional alerttype As String = "success")
        Try
            Dim alert As String = ""
            If alerttype = "success" Then
                alert = "alert alert-success alert-dismissible"
            ElseIf alerttype = "danger" Then
                alert = "alert alert-danger alert-dismissible"
            ElseIf alerttype = "info" Then
                alert = "alert alert-info alert-dismissible"
            Else
                alert = "alert alert-warning alert-dismissible"
            End If
            If message = "" Then
                divalert.Visible = False
            Else
                divalert.Visible = True
                divalert.Attributes.Add("class", alert)
                msgalert.InnerText = message
                'divalert.InnerHtml = message & "<button type=""button"" class=""close"" data-dismiss=""alert"" aria-label=""Close""><span aria-hidden=""true"">&times;</span></button>"
            End If
        Catch ex As Exception
        End Try
    End Sub
    Public Shared Function LoginURL() As String
        Return Process.ApplicationURL & "/Default.aspx"
    End Function
    Public Shared Function TestAccessConnection(ByVal path As String) As Boolean
        Try
            Dim conn As New ADODB.Connection, rec As New ADODB.Recordset
            Dim esql As String = "", esql2 As String = "", searchvar As String = ""
            Dim test As Boolean = False
            'Dim strDataSet As New DataSet

            conn = New ADODB.Connection
            rec = New ADODB.Recordset

            conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & path & ";Persist Security Info=False"
            conn.Open()
            esql = "SELECT [USERID], [CHECKTIME], [CHECKTYPE], [VERIFYCODE] FROM [CHECKINOUT]"
            rec.Open(esql, conn, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
            Return (True)

        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
            Return False
        End Try
    End Function
    Public Shared Function GetInitials(ByVal MyText As String) As String
        Dim Initials As String = ""
        Dim AllWords() As String = MyText.Split(" "c)
        For Each Word As String In AllWords
            If Word.Length > 0 Then
                Initials = Initials & Word.Chars(0).ToString.ToUpper
            End If
        Next
        Return Initials
    End Function
    Public Shared Function UpdateAccessConnString(ByVal datasource As String, ByVal user As String, ByVal pwd As String) As Boolean
        Try
            Dim config As System.Configuration.Configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~")
            'Create
            Dim connbuilder As New SqlConnectionStringBuilder
            connbuilder.DataSource = datasource
            connbuilder.UserID = user
            connbuilder.Password = pwd

            config.ConnectionStrings.ConnectionStrings("att2000ConnectionString").ConnectionString = connbuilder.ConnectionString
            config.Save(ConfigurationSaveMode.Minimal)
            ConfigurationManager.RefreshSection("connectionStrings")
            Return True
        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
            Return False
        End Try

    End Function
    Public Shared Function getPageName(vpath) As String
        Try
            Dim pg As New Page
            Dim fileinfo As New FileInfo(vpath)
            Dim pgname As String = fileinfo.Name
            Return pgname
        Catch ex As Exception
            Return ""
        End Try

    End Function
    Public Shared Function getMacAddress(ipaddr As String) As String
        Dim sip As String = ""
        Dim inet As Integer
        Dim b(6) As Byte
        Dim pMACAddr As Integer
        Dim i As Short
        Dim sResult As String = ""

        If ipaddr.Length > 0 Then
            If ipaddr.Contains(".") And IsNumeric(ipaddr.Substring(0, 1)) = True Then
                sip = ipaddr
            Else
                Dim Tempaddr As System.Net.IPHostEntry = DirectCast(Dns.GetHostByName(ipaddr), System.Net.IPHostEntry)
                Dim TempAd As System.Net.IPAddress() = Tempaddr.AddressList
                sip = TempAd(0).ToString
            End If
        End If

        inet = inet_addr(sip)
        If SendARP(inet, 0, pMACAddr, 6) = 0 Then
            CopyMemory(b(0), pMACAddr, 6)
            For i = 0 To 5
                sResult = sResult & Microsoft.VisualBasic.Right("0" & Hex(b(i)), 2)
                If i < 5 Then sResult &= "-"
            Next
        End If

        Return sResult.Replace("-", "").Trim
    End Function
    Public Shared Function DnsTest(ByVal webaddr As String) As Boolean
        Try
            '"www.google.com"
            Dim ipHe As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(webaddr)
            Return True
        Catch
            Return False
        End Try
    End Function
    Public Shared Function RegisterApp(ByVal URL As String, ByVal sKey As String, ByVal macaddress As String) As Boolean
        Try
            strExp = ""
            Dim key = "key=" + sKey
            Dim fingerprint = "?finger_print=" + macaddress
            Dim sep = "&"
            Dim sb As New StringBuilder()
            sb.Append(fingerprint).Append(sep).Append(key)
            Dim getVars As String = sb.ToString()

            Dim WebReq As HttpWebRequest = DirectCast(WebRequest.Create(String.Format(URL, getVars)), HttpWebRequest)
            WebReq.Method = "GET"

            Dim WebResp As HttpWebResponse = DirectCast(WebReq.GetResponse(), HttpWebResponse)

            Dim Answer As Stream = WebResp.GetResponseStream()
            Dim _Answer As New StreamReader(Answer)
            Dim result As Boolean = _Answer.ReadLine

            Return result
        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
            Return False
        End Try
    End Function
    Public Shared Function ValidateCalendar(CalType As String, YearName As String, sDate As Date) As String
        Dim r As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select dbo.ValidateCalendar('" & CalType & "','" & YearName & "','" & Process.DDMONYYYY(sDate) & "')")
        Return r
    End Function

    Public Shared Function FirstDateofYear() As Date
        Dim FirstDate As Date = Date.Now.AddDays(1 - Date.Now.Day).AddMonths(1 - Date.Now.Month)
        Return FirstDate
    End Function
    Public Shared Function LastDateofYear() As Date
        Dim FirstDate As Date = Date.Now.AddMonths(12 - Date.Now.Month).AddDays(31 - Date.Now.Day)
        Return FirstDate
    End Function
    Public Shared Function FirstDay(ByVal YearNo As Integer, ByVal MonthNo As Integer) As Date
        Dim FirstDate As Date = DateSerial(YearNo, MonthNo, 1)
        'Date.Now.AddDays(1 - Date.Now.Day).AddMonths(1 - Date.Now.Month)
        Return FirstDate
    End Function
    Public Shared Function LastDay(ByVal YearNo As Integer, ByVal MonthNo As Integer) As Date
        Dim LastDate As Date = DateSerial(YearNo, MonthNo, Date.DaysInMonth(YearNo, MonthNo))
        Return LastDate
    End Function
    Public Shared Function FirstDayOfWeek(sdate As Date) As Date
        Dim fdow As DayOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek
        Dim offset As Integer = fdow - sdate.DayOfWeek
        Dim fdowDate As Date = sdate.AddDays(offset)
        Return fdowDate
    End Function
    Public Shared Function LastDayOfWeek(sdate As Date) As Date
        Dim ldowDate As Date = FirstDayOfWeek(sdate).AddDays(6)
        Return ldowDate
    End Function

    Public Shared Function ApplicationURL() As String
        Try
            Dim url As String = ""
            Dim premiseserver As String = ""
            Dim allowHTTPS As Boolean = False
            Dim ccc As String = ""
            'Dim IP As String = System.Net.Dns.GetHostEntry(HttpContext.Current.Server.MachineName.ToString).AddressList(1).ToString

            Dim strHost As New DataSet
            strHost = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Admin_HostServer_Get")
            If strHost.Tables(0).Rows.Count > 0 Then
                premiseserver = strHost.Tables(0).Rows(0).Item("HostServer").ToString()
                allowHTTPS = strHost.Tables(0).Rows(0).Item("allowHTTPS").ToString()
            End If
            ccc = premiseserver.Substring(premiseserver.Length - 1, 1)
            If premiseserver.Length > 2 Then
                If premiseserver.Substring(premiseserver.Length - 1, 1) = "/" Then
                    premiseserver = premiseserver.Substring(0, premiseserver.Trim.Length - 1)
                End If
            End If

            Dim myHost As String = System.Net.Dns.GetHostName
            Dim myHTTPS As String = ""
            If allowHTTPS = False Then
                myHTTPS = "http://"
            Else
                myHTTPS = "https://"
            End If


            If apptype.ToLower = "cloud" Then
                If myHost.ToLower.Contains("http") = False Then
                    url = myHTTPS & myHost & "/" & domainname
                Else
                    url = myHost & "/" & domainname 'hostsite & "/" & domainname
                End If
            Else
                If premiseserver.Trim <> "" And domainname <> "" Then
                    myHost = premiseserver.Trim
                    url = myHTTPS & myHost & "/" & domainname  '"/GOSHRM"
                Else
                    myHost = premiseserver.Trim
                    url = myHTTPS & myHost
                End If

            End If

            'Dim myIPs As System.Net.IPHostEntry = System.Net.Dns.GetHostByName(myHost)
            'For Each myIP As System.Net.IPAddress In myIPs.AddressList
            '    IP = myIP.ToString
            'Next

            Return url
        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
            Return ""
        End Try
    End Function
    Public Shared Sub LoadTimeToRadCombo(ByVal radHour As RadComboBox, ByVal radMinute As RadComboBox, ByVal radTime As RadComboBox, ByVal TimeString As String)
        Try
            Dim AMString As String = "AM"

            Arrays = TimeString.Split(Separators, StringSplitOptions.RemoveEmptyEntries)
            Dim Hours As Integer = Arrays(0)
            Dim Minutes As String = Arrays(1)

            If Hours > 12 Then
                Hours = Hours - 12
                AMString = "PM"
            Else
                AMString = "AM"
            End If

            For j = 0 To radHour.Items.Count - 1
                If radHour.Items.Item(j).Text = Hours Then
                    radHour.Items.Item(j).Selected = True
                    Exit For
                End If
            Next
            For j = 0 To radMinute.Items.Count - 1
                If radMinute.Items.Item(j).Text = Minutes Then
                    radMinute.Items.Item(j).Selected = True
                    Exit For
                End If
            Next
            For j = 0 To radTime.Items.Count - 1
                If radTime.Items.Item(j).Text = AMString Then
                    radTime.Items.Item(j).Selected = True
                    Exit For
                End If
            Next

        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
        End Try
    End Sub
    Private Shared Function Convert_ToMMSS(duration As Integer) As String
        Try
            Return duration.ToString & ":00"
        Catch ex As Exception
            Return "00:00"
        End Try
    End Function
    Public Shared Function IsHRManager(ByVal empid As String) As Boolean
        Try
            Dim response As Boolean = False
            Dim strStaff As New DataSet
            strStaff = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "Select * from dbo.Employees_All a where a.ishr = 'Yes' and EmpID = '" & empid & "'")
            If strStaff.Tables(0).Rows.Count > 0 Then
                response = True
            Else
                response = False
            End If
            Return response
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Shared Function IsFinance(ByVal empid As String) As Boolean
        Try
            Dim response As Boolean = False
            Dim strStaff As New DataSet
            strStaff = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "Select * from dbo.Employees_All a where a.isfinance = 'Yes' and EmpID = '" & empid & "'")
            If strStaff.Tables(0).Rows.Count > 0 Then
                response = True
            Else
                response = False
            End If
            Return response
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Shared Function IsAdmin(ByVal userid As String) As Boolean
        Try
            Dim response As Boolean = False
            Dim strStaff As New DataSet
            strStaff = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "users_system_admin_get", userid)
            If strStaff.Tables(0).Rows.Count > 0 Then
                response = True
            Else
                response = False
            End If
            Return response
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Shared Function IsPayrollApprover(ByVal empid As String) As Boolean
        Try
            Dim response As Boolean = False
            Dim strStaff As New DataSet
            strStaff = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "select * from Payroll_Option_Approver where EmpID = '" & empid & "'")
            If strStaff.Tables(0).Rows.Count > 0 Then
                response = True
            Else
                response = False
            End If
            Return response
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Shared Function ConvertCtrlToTime(ByVal radHour As String, ByVal radMinute As String, ByVal radTime As String) As String
        Try
            'Dim sHour As String = radHour.SelectedItem.Value
            'Dim sMin As String = radMinute.SelectedItem.Value
            'Dim sTime As String = radTime.SelectedItem.Value
            Dim sHour As String = radHour
            Dim sMin As String = radMinute
            Dim sTime As String = radTime

            If sTime = "PM" Then
                If sHour = "12" Then
                    sHour = "12"
                Else
                    sHour = (12 + CInt(sHour)).ToString.PadLeft(2, "0")
                End If

            Else
                If sHour = "12" Then
                    sHour = "00"
                Else
                    sHour = sHour.PadLeft(2, "0")
                End If

            End If

            sMin = sMin.PadLeft(2, "0")
            Return sHour & ":" & sMin & ":00"
        Catch ex As Exception
            Return "00:00:00"
        End Try
    End Function
    Public Shared Function AMPM_Time(ByVal TimeString As String) As String
        Try
            Dim AMString As String = "AM"

            Arrays = TimeString.Split(Separators, StringSplitOptions.RemoveEmptyEntries)
            Dim Hours As Integer = Arrays(0)
            Dim Minutes As String = Arrays(1)

            If Hours > 12 Then
                Hours = Hours - 12
                AMString = "PM"
            Else
                AMString = "AM"
            End If

            Return Hours.ToString.PadLeft(2, "0") & ":" & Minutes & " " & AMString
        Catch ex As Exception
            Return TimeString
        End Try
    End Function
    Public Shared Function DDMONYYYY(ByVal sDate As Date) As String

        Try
            Dim mon As String = ""
            Dim monthstring As String = ""
            'Arrays = sDate.Split(Separators1, StringSplitOptions.RemoveEmptyEntries)
            'Dim dd As String = Arrays(0)
            'Dim mm As String = Arrays(1)
            'Dim yy As String = Arrays(2)
            Dim dd As Integer = sDate.Day
            Dim mm As Integer = sDate.Month
            Dim yy As Integer = sDate.Year

            Select Case mm
                Case 1
                    monthstring = "Jan"
                Case 2
                    monthstring = "Feb"
                Case 3
                    monthstring = "Mar"
                Case 4
                    monthstring = "Apr"
                Case 5
                    monthstring = "May"
                Case 6
                    monthstring = "Jun"
                Case 7
                    monthstring = "Jul"
                Case 8
                    monthstring = "Aug"
                Case 9
                    monthstring = "Sep"
                Case 10
                    monthstring = "Oct"
                Case 11
                    monthstring = "Nov"
                Case 12
                    monthstring = "Dec"
            End Select
            mon = dd.ToString.PadLeft(2 - dd.ToString.Length, "0") & "-" & monthstring & "-" & yy
            Return mon
        Catch ex As Exception
            Return ""
        End Try
    End Function
    Public Shared Function GetScalarValues(ByVal SP As String, param As String, Columns() As String) As String()
        Dim val() As String = Columns
        Dim col() As String = Columns
        Dim strDataSet As New DataSet

        If param = "" Then
            strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, SP)
        Else
            strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, SP, param)
        End If
        If strDataSet.Tables(0).Rows.Count = 1 Then
            For i As Integer = 0 To Columns.Count - 1
                val(i) = strDataSet.Tables(0).Rows(0).Item(col(i).ToString).ToString()
            Next
        End If

        Return val
    End Function
    Public Shared Function GetCompanyByEmpID(ByVal empid As String) As String
        Try
            Dim name As String = ""
            name = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select dbo.My_CompanyByEmpID('" & empid & "') ")

            Return name
        Catch ex As Exception
            Return ""
        End Try
    End Function
    Public Shared Function GetMailLink(AuthenCode As String, columnmodule As Integer) As String
        Try
            Return SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "menu_get_link", AuthenCode, columnmodule)
        Catch ex As Exception
            Return ""
        End Try
    End Function
    Public Shared Function GetEmployeeData(ByVal empid As String, ByVal column As String) As String
        'Load RadCombo Box with Display Text and Value

        Dim strDataSet As New DataSet
        Dim result As String = ""
        strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_PersonalDetail_get", empid)
        For i As Integer = 0 To strDataSet.Tables(0).Rows.Count - 1
            result = strDataSet.Tables(0).Rows(i).Item(column).ToString()
        Next
        Return result
    End Function
    Public Shared Function GetRecruitData(ByVal GUID As String, ByVal column As String) As String
        'Load RadCombo Box with Display Text and Value

        Dim strDataSet As New DataSet
        Dim result As String = ""
        strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Applicant_Get", GUID)
        For i As Integer = 0 To strDataSet.Tables(0).Rows.Count - 1
            result = strDataSet.Tables(0).Rows(i).Item(column).ToString()
        Next
        Return result
    End Function
    Public Shared Function GetEmailAddress(empid As String) As String
        Try
            Dim name As String = ""
            name = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Emp_Contact_Info_Get_Email", empid)

            Return name
        Catch ex As Exception
            Return ""
        End Try
    End Function
    Public Shared Function GetCompanyName(Optional deptname As String = "") As String
        Try
            Dim name As String = ""
            name = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select dbo.My_Company('" & deptname & "') ")
            'If deptname = "" Then
            '    name = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select organisationname from general_info")
            'Else
            '    name = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select dbo.My_Company('" & deptname & "') ")
            'End If
            Return name
        Catch ex As Exception
            Return ""
        End Try
    End Function
    Public Shared Function GetEmployeeName(ByVal empid As String) As String
        Try
            Dim name As String = ""
            name = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select name from dbo.employees_all where empid = '" & empid & "'")

            Return name
        Catch ex As Exception
            Return ""
        End Try
    End Function
    Public Shared Function GetMailList(ByVal officetype As String) As String
        Try
            Dim strDataSet As New DataSet
            If officetype.ToUpper = "HR" Then
                strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_PersonalDetail_Get_hr_Staff", HttpContext.Current.Session.Item("organisation"))
            ElseIf officetype.ToUpper = "FINANCE" Then
                strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_PersonalDetail_Get_Finance_Staff", HttpContext.Current.Session.Item("organisation"))
            End If

            Dim maillist As String = ""
            If strDataSet.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To strDataSet.Tables(0).Rows.Count - 1
                    If i = 0 Then
                        maillist = strDataSet.Tables(0).Rows(i).Item("emailaddress").ToString()
                    Else
                        maillist = maillist & ";" & strDataSet.Tables(0).Rows(i).Item("emailaddress").ToString()
                    End If
                Next
            End If
            Return maillist
        Catch ex As Exception
            Return ""
        End Try
    End Function
    Public Shared Function GetEmpIDMailList(ByVal officetype As String) As String
        Try
            Dim strDataSet As New DataSet
            If officetype.ToUpper = "HR" Then
                strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_PersonalDetail_Get_hr_Staff", HttpContext.Current.Session.Item("organisation"))
            ElseIf officetype.ToUpper = "FINANCE" Then
                strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_PersonalDetail_Get_Finance_Staff", HttpContext.Current.Session.Item("organisation"))
            End If

            Dim maillist As String = ""
            If strDataSet.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To strDataSet.Tables(0).Rows.Count - 1
                    If i = 0 Then
                        maillist = strDataSet.Tables(0).Rows(i).Item("empid").ToString()
                    Else
                        maillist = maillist & ";" & strDataSet.Tables(0).Rows(i).Item("empid").ToString()
                    End If
                Next
            End If
            Return maillist
        Catch ex As Exception
            Return ""
        End Try
    End Function
    Private Shared Function SendSMSEsendex(ByVal phoneNumber As String, ByVal strBody As String, ByVal accountReference As String, ByVal sid As String, ByVal sidpassword As String) As Boolean
        Try
            'Esendex SMS API
            Dim datSMS As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "SMS_Configuration_Get_Provider")
            If datSMS.Tables(0).Rows.Count > 0 Then
                accountReference = accountReference
                Dim messagingService = New MessagingService(sid, sidpassword)

                Dim singleMessage = New SmsMessage(phoneNumber, strBody, accountReference)
                messagingService.SendMessage(singleMessage)
            End If


            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Shared Function SendSMS(ByVal phoneNumber As String, ByVal strBody As String) As Boolean
        Try
            Dim result As Boolean = False
            Dim sid As String = ""
            Dim sidpassword As String = ""
            Dim token As String = ""
            Dim datSMS As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "SMS_Configuration_Get_Provider")
            Dim smsprovider = datSMS.Tables(0).Rows(0).Item("smsprovider").ToString()
            If smsprovider.ToLower() = "esendex" Then
                token = datSMS.Tables(0).Rows(0).Item("token").ToString()
                sid = datSMS.Tables(0).Rows(0).Item("sid").ToString()
                sidpassword = datSMS.Tables(0).Rows(0).Item("sidpassword").ToString()
                result = SendSMSEsendex(phoneNumber, strBody, token, sid, sidpassword)
            End If

            'Const accountSid = "AC5d75a1a044468cb0001ce7b492d5f6b7"
            'Const authToken = "3d418924311a4f2fd04ff95e7d2f98ff"
            'TwilioClient.Init(accountSid, authToken)

            'Dim toNumber = New PhoneNumber(phoneNumber)
            'Dim message = MessageResource.Create(
            '    toNumber, from:=New PhoneNumber("+15412050890"),
            '    body:=strBody)

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Shared Function SendEmail(ByVal strFrom As String, ByVal strFromName As String, ByVal strTo As String, ByVal strSubject As String, ByVal strBody As String, ByVal strAttachmentPath As String, ByVal IsBodyHTML As Boolean, Optional ByVal copymail As String = "") As Boolean
        Dim strUser As New DataSet
        strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Email_Configuration_get")
        Dim _senderemail As String = ""
        Dim _sendername As String = ""
        Dim _smtphost As String = ""
        Dim _smtpport As Integer = 0
        Dim _useauthen As String = ""
        Dim _smtpuser As String = ""
        Dim _password As String = ""
        Dim _type As String = ""
        Dim UseSSL As Boolean = False
        If strUser.Tables(0).Rows.Count > 0 Then
            _senderemail = strUser.Tables(0).Rows(0).Item("senderemail").ToString
            _sendername = strUser.Tables(0).Rows(0).Item("sendername").ToString
            _smtphost = strUser.Tables(0).Rows(0).Item("smtphost").ToString
            _smtpport = strUser.Tables(0).Rows(0).Item("smtpport").ToString
            _useauthen = strUser.Tables(0).Rows(0).Item("usesmtpauthentication").ToString
            _smtpuser = strUser.Tables(0).Rows(0).Item("smtpuser").ToString
            _password = Process.Decrypt(strUser.Tables(0).Rows(0).Item("smptpwd").ToString)
            'UseSSL = CBool(strUser.Tables(0).Rows(0).Item("secureconnection").ToString)
        End If

        'If _type.ToUpper.Trim = "SSL" Then
        '    UseSSL = True
        'Else
        '    UseSSL = False
        'End If

        Dim arrToArray As String()
        Dim arrToArrayCopy As String()
        Dim arrAttachment As String()

        Dim splitter As Char = ";"
        arrToArray = strTo.Split(splitter)



        Dim mm As MailMessage = New MailMessage()
        If strFrom <> "" Then
            _senderemail = strFrom
        End If

        If strFromName <> "" Then
            _sendername = strFromName
        End If
        mm.From = New MailAddress(_senderemail, _sendername)
        mm.Subject = strSubject
        mm.Body = strBody
        mm.IsBodyHtml = IsBodyHTML
        ''Reply To Address (Optional)
        'mm.ReplyTo = New MailAddress(_senderemail)

        'Add the recepient email Addresses
        For Each s As String In arrToArray
            mm.To.Add(New MailAddress(s))
        Next

        If copymail <> "" Then
            arrToArrayCopy = copymail.Split(splitter)
            If arrToArrayCopy.Length > 0 Then
                For Each s As String In arrToArrayCopy
                    mm.CC.Add(New MailAddress(s))
                Next
            End If
        End If




        Try
            'Add Attachment
            If strAttachmentPath.Length > 0 Then
                arrAttachment = strAttachmentPath.Split(splitter)
                If arrAttachment.Length > 0 Then
                    For Each s As String In arrAttachment
                        mm.Attachments.Add(New Attachment(s))
                    Next
                End If


            End If

        Catch
        End Try

        Dim smtp As SmtpClient = New SmtpClient()

        Try
            'Your SMTP Server
            smtp.Host = _smtphost
            'SSL Settings depending on your Server
            smtp.EnableSsl = True
            'Creadentials for the Server
            Dim NetworkCred As NetworkCredential = New System.Net.NetworkCredential()

            'Your Email
            NetworkCred.UserName = _smtpuser
            NetworkCred.Password = _password

            smtp.UseDefaultCredentials = True
            smtp.Credentials = NetworkCred
            smtp.Timeout = 600000
            'Port No of the Server 
            smtp.Port = _smtpport
            smtp.Send(mm)
            Return True
        Catch ex As Exception
            Return False
            'HttpContext.Current.Session.Item("exception") = ex.Message
            Return False
        Finally

        End Try
    End Function
    Public Shared Function AuthenAction(ByVal role As String, ByVal Code As String, ByVal action As String) As Boolean
        Try
            Dim oAuthen As Boolean = False
            oAuthen = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "role_privilege_auth", role, Code, action)
            Return oAuthen
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Shared Function DualAuthen(ByVal username As String, ByVal password As String) As Boolean
        Try
            Dim oAuthen As Boolean = False
            Dim strDataSet As New DataSet
            strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Two_Way_Authentication_Get", username, password)
            Dim count As Integer = strDataSet.Tables(0).Rows.Count
            If count > 0 Then
                oAuthen = True
            Else
                oAuthen = False
            End If
            Return oAuthen
        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
            Return False
        End Try
    End Function
    Public Shared Function ApplicantAuthen(ByVal username As String, ByVal password As String) As Boolean
        Try
            Dim oAuthen As Boolean = False
            Dim strDataSet As New DataSet
            strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Applicant_Authen", username, password)
            Dim count As Integer = strDataSet.Tables(0).Rows.Count
            If count > 0 Then
                For i As Integer = 0 To count - 1
                    HttpContext.Current.Session.Item("AppID") = strDataSet.Tables(0).Rows(i).Item("id").ToString()
                    HttpContext.Current.Session.Item("ApplicantName") = strDataSet.Tables(0).Rows(i).Item("applicant").ToString()
                    HttpContext.Current.Session.Item("applicantid") = strDataSet.Tables(0).Rows(i).Item("emailaddress").ToString()
                Next
                oAuthen = True
            Else
                oAuthen = False
            End If
            Return oAuthen
        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
            Return False
        End Try
    End Function
    Public Shared Function UserADAuthen(ByVal ldap As String, ByVal domain As String, ByVal username As String, ByVal pwd As String) As Boolean
        Dim domainAndUsername As String = ""
        domainAndUsername = domain & "\" & username
        Dim entry As New DirectoryEntry(ldap, domainAndUsername, pwd)
        Dim obj As Object
        Try
            obj = entry.NativeObject
            Dim search As New DirectorySearcher(entry)
            Dim result As SearchResult
            search.Filter = "(SAMAccountName=" + username + ")"
            search.PropertiesToLoad.Add("cn")
            result = search.FindOne()
            If result Is Nothing Then
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try

        If (UserAuthen2(username)) Then
            Return True
        Else
            Return False
        End If

    End Function
    Public Shared Function UserAuthen2(ByVal username As String) As Boolean
        Try
            Dim oAuthen As Boolean = False
            Dim strDataSet As New DataSet
            strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "users_authen_AD", username.ToLower)
            Dim count As Integer = strDataSet.Tables(0).Rows.Count
            If count > 0 Then
                For i As Integer = 0 To count - 1
                    HttpContext.Current.Session.Item("role") = strDataSet.Tables(0).Rows(i).Item("Role").ToString()
                    HttpContext.Current.Session.Item("roletype") = strDataSet.Tables(0).Rows(i).Item("Roletype").ToString()
                    HttpContext.Current.Session.Item("UserEmpID") = strDataSet.Tables(0).Rows(i).Item("EmpID").ToString()
                    HttpContext.Current.Session.Item("EmpName") = strDataSet.Tables(0).Rows(i).Item("Fullname").ToString()
                    HttpContext.Current.Session.Item("LoginEmail") = strDataSet.Tables(0).Rows(i).Item("email").ToString()
                    HttpContext.Current.Session.Item("PasswordChange") = strDataSet.Tables(0).Rows(i).Item("changedpwd").ToString()
                    HttpContext.Current.Session.Item("LastName") = strDataSet.Tables(0).Rows(i).Item("LastName").ToString()
                    HttpContext.Current.Session.Item("UserJobgrade") = strDataSet.Tables(0).Rows(i).Item("Grade").ToString()
                    HttpContext.Current.Session.Item("Access") = username  'strDataSet.Tables(0).Rows(i).Item("StructureName").ToString()
                    HttpContext.Current.Session.Item("Organisation") = strDataSet.Tables(0).Rows(i).Item("mycompany").ToString()
                    HttpContext.Current.Session.Item("company") = strDataSet.Tables(0).Rows(i).Item("mycompany").ToString()
                    HttpContext.Current.Session.Item("Dept") = strDataSet.Tables(0).Rows(i).Item("Office").ToString()
                    HttpContext.Current.Session.Item("Level") = strDataSet.Tables(0).Rows(i).Item("accesslevel").ToString()
                    HttpContext.Current.Session.Item("UserJobtitle") = strDataSet.Tables(0).Rows(i).Item("jobtitle").ToString()
                    HttpContext.Current.Session.Item("UserMobileNo") = strDataSet.Tables(0).Rows(i).Item("mobileno").ToString()
                    'Session("Level")                    
                Next
                oAuthen = True
            Else
                oAuthen = False
            End If
            Return oAuthen
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Shared Function UserAuthen(ByVal username As String, ByVal password As String) As Boolean
        Try
            Dim oAuthen As Boolean = False
            Dim strDataSet As New DataSet
            strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "users_authen", username.ToLower, password)
            Dim count As Integer = strDataSet.Tables(0).Rows.Count
            If count > 0 Then
                For i As Integer = 0 To count - 1
                    HttpContext.Current.Session.Item("role") = strDataSet.Tables(0).Rows(i).Item("Role").ToString()
                    HttpContext.Current.Session.Item("roletype") = strDataSet.Tables(0).Rows(i).Item("Roletype").ToString()
                    HttpContext.Current.Session.Item("UserEmpID") = strDataSet.Tables(0).Rows(i).Item("EmpID").ToString()
                    HttpContext.Current.Session.Item("EmpName") = strDataSet.Tables(0).Rows(i).Item("Fullname").ToString()
                    HttpContext.Current.Session.Item("LoginEmail") = strDataSet.Tables(0).Rows(i).Item("email").ToString()
                    HttpContext.Current.Session.Item("PasswordChange") = strDataSet.Tables(0).Rows(i).Item("changedpwd").ToString()
                    HttpContext.Current.Session.Item("LastName") = strDataSet.Tables(0).Rows(i).Item("LastName").ToString()
                    HttpContext.Current.Session.Item("UserJobgrade") = strDataSet.Tables(0).Rows(i).Item("Grade").ToString()
                    HttpContext.Current.Session.Item("Access") = username  'strDataSet.Tables(0).Rows(i).Item("StructureName").ToString()
                    HttpContext.Current.Session.Item("Organisation") = strDataSet.Tables(0).Rows(i).Item("mycompany").ToString()
                    HttpContext.Current.Session.Item("company") = strDataSet.Tables(0).Rows(i).Item("mycompany").ToString()
                    HttpContext.Current.Session.Item("Dept") = strDataSet.Tables(0).Rows(i).Item("Office").ToString()
                    HttpContext.Current.Session.Item("Level") = strDataSet.Tables(0).Rows(i).Item("accesslevel").ToString()
                    HttpContext.Current.Session.Item("UserJobtitle") = strDataSet.Tables(0).Rows(i).Item("jobtitle").ToString()
                    HttpContext.Current.Session.Item("UserMobileNo") = strDataSet.Tables(0).Rows(i).Item("mobileno").ToString()
                    'Session("Level")
                Next
                oAuthen = True
            Else
                oAuthen = False
            End If
            Return oAuthen
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Shared Function ExportDataSet(TitleHeader As String(), TitleData As String(), ByVal Grid As System.Data.DataTable, ByVal ColumnHeaders As String(), ByVal Columns As String(), filename As String) As Boolean
        Try
            Dim s As New StringBuilder
            Dim strHeader As String = ""
            Dim strData As String = ""
            Dim Titles As String = ""

            System.Web.HttpContext.Current.Response.Clear()
            System.Web.HttpContext.Current.Response.Buffer = True
            System.Web.HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" & filename & ".csv")
            System.Web.HttpContext.Current.Response.Charset = ""
            System.Web.HttpContext.Current.Response.ContentType = "application/text"
            'filename = filename & ".csv"
            Dim cCount As Integer = ColumnHeaders.Count
            Dim rCount As Integer = Grid.Rows.Count

            If TitleHeader IsNot Nothing Then
                For g = 0 To TitleHeader.Count - 1
                    Dim data As String = TitleData(g).ToString().Replace(",", " ")
                    If IsNumeric(data.Replace(" ", "")) = True Then
                        data = data.Replace(" ", "")

                    End If
                    Titles = TitleHeader(g).ToString & "," & data
                    s.AppendLine(Titles)
                Next g
                If TitleHeader.Count > 0 Then
                    s.AppendLine("")
                End If
            End If


            For y As Integer = 0 To cCount - 1
                strHeader = strHeader & ColumnHeaders(y).ToString & ","
            Next
            strHeader = strHeader.Substring(0, strHeader.Length - 1)
            s.AppendLine(strHeader)

            If rCount > 0 Then
                For m As Integer = 0 To rCount - 1
                    strData = ""
                    For j As Integer = 0 To cCount - 1
                        Dim data As String = Grid.Rows(m).Item(Columns(j).ToString).ToString.Replace(",", " ")
                        data = data.Replace(",", " ").Replace(vbCrLf, "<br/>").Replace(vbCr, "<br/>")
                        data = data.Replace("<br/>", " ")
                        If IsNumeric(data.Replace(" ", "")) = True Then
                            data = data.Replace(" ", "")
                        Else
                            If data.Length > 0 Then
                                If data.Trim.Substring(0, 1) = "-" Then
                                    data = data.Replace("-", "")
                                End If
                            End If


                        End If
                        strData = strData & data & ","  'Grid.Tables(0).Rows(m).Item(Columns(j).ToString).ToString.Replace(",", " ") & ","
                    Next
                    strData = strData.Substring(0, strData.Length - 1)
                    s.AppendLine(strData)
                Next
            End If
            'FileSystem.WriteAllText(filename, s.ToString, True)

            System.Web.HttpContext.Current.Response.Output.Write(s.ToString())
            System.Web.HttpContext.Current.Response.Flush()
            System.Web.HttpContext.Current.Response.[End]()
            Return True
        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
            Return False
        End Try
    End Function
    Public Shared Sub SortArrow(e As System.Web.UI.WebControls.GridViewRowEventArgs, SortsDirection As SortDirection, sortExpression As Object)
        Try
            If sortExpression Is Nothing Then
                sortExpression = "rows"
            ElseIf sortExpression.ToString = "" Then
                sortExpression = "rows"

            End If
            If e.Row.RowType = DataControlRowType.Header Then
                For Each tc As TableCell In e.Row.Cells
                    If tc.HasControls() Then

                        Dim lnk As LinkButton = TryCast(tc.Controls(0), LinkButton)
                        If lnk IsNot Nothing Then

                            Dim img As New System.Web.UI.WebControls.Image()

                            img.ImageUrl = "~/images/" & (If(SortsDirection = SortDirection.Ascending, "desc", "asc")) & ".gif"

                            If sortExpression.ToString.ToLower = lnk.CommandArgument.ToLower Then

                                tc.Controls.Add(New LiteralControl(" "))
                                tc.Controls.Add(img)

                            End If
                        End If
                    End If
                Next
            End If
        Catch ex As Exception

        End Try
    End Sub
    'Public Shared Function ExportTest(GridVwHeaderChckbox As GridView, filename As String, startpos As Integer, controlpos As Integer) As Boolean
    '    Try
    '        System.Web.HttpContext.Current.Response.Clear()
    '        System.Web.HttpContext.Current.Response.Buffer = True
    '        System.Web.HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" & filename & ".xls")
    '        System.Web.HttpContext.Current.Response.Charset = ""
    '        System.Web.HttpContext.Current.Response.ContentType = "application/vnd.ms-excel"
    '        Using sw As New StringWriter()
    '            Dim hw As New HtmlTextWriter(sw)
    '            GridVwHeaderChckbox.RenderControl(hw)

    '            System.Web.HttpContext.Current.Response.Output.Write(sw.ToString())
    '            System.Web.HttpContext.Current.Response.Flush()
    '            'System.Web.HttpContext.Current.Response.[End]()
    '            HttpContext.Current.Response.SuppressContent = True
    '            HttpContext.Current.ApplicationInstance.CompleteRequest()
    '            HttpContext.Current.Response.End()
    '        End Using
    '        Return True
    '    Catch ex As Exception
    '        HttpContext.Current.Session.Item("exception") = ex.Message
    '        Return False
    '    End Try
    'End Function
    Public Shared Function ExportExcel(dt As System.Data.DataTable, filename As String) As Boolean
        Try
            Dim GridVwHeaderChckbox As New DataGrid
            GridVwHeaderChckbox.AllowPaging = False

            GridVwHeaderChckbox.DataSource = dt
            GridVwHeaderChckbox.DataBind()

            System.Web.HttpContext.Current.Response.Clear()
            System.Web.HttpContext.Current.Response.Buffer = True
            System.Web.HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" & filename & ".xls")
            System.Web.HttpContext.Current.Response.Charset = ""
            System.Web.HttpContext.Current.Response.ContentType = "application/vnd.ms-excel"
            Using sw As New StringWriter()
                Dim hw As New HtmlTextWriter(sw)
                GridVwHeaderChckbox.RenderControl(hw)

                System.Web.HttpContext.Current.Response.Output.Write(sw.ToString())
                'System.Web.HttpContext.Current.Response.Flush()

                System.Web.HttpContext.Current.Response.Flush()
                HttpContext.Current.Response.SuppressContent = True
                HttpContext.Current.ApplicationInstance.CompleteRequest()
                HttpContext.Current.Response.End()
                'System.Web.HttpContext.Current.Response.[End]()
            End Using
            Return True
        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
            Return False
        End Try
    End Function
    Public Shared Function Export(GridVwHeaderChckbox As GridView, filename As String, startpos As Integer, controlpos As Integer) As Boolean
        Try
            'System.Web.HttpResponse(response = System.Web.HttpContext.Current.Response)
            'Dim response As System.Web.HttpResponse = New System.Web.HttpContext.current.response
            GridVwHeaderChckbox.AllowPaging = False
            System.Web.HttpContext.Current.Response.Clear()
            System.Web.HttpContext.Current.Response.Buffer = True
            System.Web.HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" & filename & ".csv")
            System.Web.HttpContext.Current.Response.Charset = ""
            System.Web.HttpContext.Current.Response.ContentType = "application/text"
            Dim sBuilder As StringBuilder = New System.Text.StringBuilder()
            For index As Integer = 1 To GridVwHeaderChckbox.Columns.Count - 1  'GridVwHeaderChckbox.Columns.Count - 1
                'sBuilder.Append(GridVwHeaderChckbox.Columns(index).HeaderText + ","c)
                sBuilder.Append(GridVwHeaderChckbox.Columns(index).HeaderText + ","c)
            Next
            sBuilder.Append(vbCr & vbLf)

            For i As Integer = 0 To GridVwHeaderChckbox.Rows.Count - 1
                For k As Integer = startpos To GridVwHeaderChckbox.HeaderRow.Cells.Count - 1
                    If k = controlpos Then
                        Dim controls As HyperLink = DirectCast(GridVwHeaderChckbox.Rows(i).Cells(k).FindControl("HyperLink1"), HyperLink)
                        sBuilder.Append(controls.Text.Replace(",", "") + ",")
                    Else
                        sBuilder.Append(GridVwHeaderChckbox.Rows(i).Cells(k).Text.Replace(",", "") + ",")
                    End If

                Next
                sBuilder.Append(vbCr & vbLf)
            Next

            System.Web.HttpContext.Current.Response.Output.Write(sBuilder.ToString())
            System.Web.HttpContext.Current.Response.Flush()
            'System.Web.HttpContext.Current.Response.[End]()
            HttpContext.Current.Response.SuppressContent = True
            HttpContext.Current.ApplicationInstance.CompleteRequest()
            HttpContext.Current.Response.End()
            Return True
        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
            Return False
        End Try
    End Function
    Public Shared Function ExportPayroll(dt As System.Data.DataTable, filename As String) As Boolean
        Try
            Dim GridVwHeaderChckbox As New DataGrid
            GridVwHeaderChckbox.AllowPaging = False

            GridVwHeaderChckbox.DataSource = dt
            GridVwHeaderChckbox.DataBind()

            System.Web.HttpContext.Current.Response.Clear()
            System.Web.HttpContext.Current.Response.Buffer = True
            System.Web.HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" & filename & ".xls")
            System.Web.HttpContext.Current.Response.Charset = ""
            System.Web.HttpContext.Current.Response.ContentType = "application/vnd.ms-excel"
            Using sw As New StringWriter()
                Dim hw As New HtmlTextWriter(sw)
                GridVwHeaderChckbox.RenderControl(hw)

                System.Web.HttpContext.Current.Response.Output.Write(sw.ToString())
                'System.Web.HttpContext.Current.Response.Flush()

                System.Web.HttpContext.Current.Response.Flush()
                HttpContext.Current.Response.SuppressContent = True
                HttpContext.Current.ApplicationInstance.CompleteRequest()
                HttpContext.Current.Response.End()
                'System.Web.HttpContext.Current.Response.[End]()
            End Using
            Return True
        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
            Return False
        End Try
    End Function


    Public Shared Function Export2(GridVwHeaderChckbox As GridView, filename As String, startpos As Integer, controlpos As Integer, controlpos2 As Integer) As Boolean
        Try
            'System.Web.HttpResponse(response = System.Web.HttpContext.Current.Response)
            'Dim response As System.Web.HttpResponse = New System.Web.HttpContext.current.response

            System.Web.HttpContext.Current.Response.Clear()
            System.Web.HttpContext.Current.Response.Buffer = True
            System.Web.HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" & filename & ".csv")
            System.Web.HttpContext.Current.Response.Charset = ""
            System.Web.HttpContext.Current.Response.ContentType = "application/text"
            Dim sBuilder As StringBuilder = New System.Text.StringBuilder()
            For index As Integer = startpos To GridVwHeaderChckbox.HeaderRow.Cells.Count - 1 'GridVwHeaderChckbox.Columns.Count - 1
                'sBuilder.Append(GridVwHeaderChckbox.Columns(index).HeaderText + ","c)
                sBuilder.Append(GridVwHeaderChckbox.Columns(index).HeaderText + ","c)
            Next
            sBuilder.Append(vbCr & vbLf)
            For i As Integer = 0 To GridVwHeaderChckbox.Rows.Count - 1
                For k As Integer = startpos To GridVwHeaderChckbox.HeaderRow.Cells.Count - 1
                    If k = controlpos Then
                        Dim controls As HyperLink = DirectCast(GridVwHeaderChckbox.Rows(i).Cells(k).FindControl("HyperLink1"), HyperLink)
                        sBuilder.Append(controls.Text.Replace(",", "") + ",")
                    ElseIf k = controlpos2 Then
                        Dim controls As HyperLink = DirectCast(GridVwHeaderChckbox.Rows(i).Cells(k).FindControl("HyperLink2"), HyperLink)
                        sBuilder.Append(controls.Text.Replace(",", "") + ",")
                    Else
                        sBuilder.Append(GridVwHeaderChckbox.Rows(i).Cells(k).Text.Replace(",", "") + ",")
                    End If

                Next
                sBuilder.Append(vbCr & vbLf)
            Next
            'w.Write(sBuilder.ToString())
            'w.Flush()
            'w.Close()
            System.Web.HttpContext.Current.Response.Output.Write(sBuilder.ToString())
            System.Web.HttpContext.Current.Response.Flush()
            'System.Web.HttpContext.Current.Response.[End]()
            HttpContext.Current.Response.SuppressContent = True
            HttpContext.Current.ApplicationInstance.CompleteRequest()
            HttpContext.Current.Response.End()
            Return True
        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
            Return False
        End Try
    End Function
    Public Shared Function ImportUsers(ByVal filename As String, ByVal SP As String, ByVal Page As String) As Boolean
        'Min 1 and Max 26 columns import

        Dim fields As String()

        Dim hCont As Integer = 0
        Dim Delimiter As String = ","
        Dim columnCnt As Integer = 0

        hCont = 0
        HttpContext.Current.Session.Item("uploadcnt") = 0
        Try
            Using parser As New TextFieldParser(filename)
                parser.SetDelimiters(Delimiter)
                While Not parser.EndOfData

                    Dim P(7) As String
                    fields = parser.ReadFields()
                    'P = fields

                    columnCnt = fields.Length
                    For y As Integer = 0 To columnCnt - 1
                        If hCont > 0 Then
                            ' fields(y) <> String.Empty AndAlso fields(y).Replace("""", "") <> "-" AndAlso
                            P(y) = CStr(fields(y).Replace("""", "").Trim)
                            If P(y).Length > 0 Then
                                If P(y).Substring(0, 1) = "'" Then
                                    P(y) = P(y).Replace("'", "")
                                End If
                            End If
                            If P(y).Trim = "-" Then
                                P(y) = "0"
                            End If
                            If P(y).Trim = String.Empty Or P(y).Trim = "" Then
                                P(y) = Nothing
                            End If
                        End If
                    Next
                    P(7) = Encrypt(CStr(fields(0).Replace("""", "").Trim))

                    If hCont > 0 Then
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, SP, P)
                        HttpContext.Current.Session.Item("uploadcnt") = CInt(HttpContext.Current.Session.Item("uploadcnt")) + 1
                    End If
                    hCont = 1
                End While
            End Using
            'delete temp file after upload
            If File.Exists(filename) = True Then
                File.Delete(filename)
            End If

            'Activity_Log(DateTime.Now, Processing.User_Name, SP, "0", "File Upload", "Uploaded " & uploadCount.ToString & " records from " & filename, 0, 0)
            Return True
        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
            Return False
        End Try
    End Function
    Public Shared Function ImportWithUsersAndP1(ByVal filename As String, ByVal SP As String, P1 As String, ByVal Page As String) As Boolean
        'Min 1 and Max 26 columns import

        Dim fields As String()

        Dim hCont As Integer = 0
        Dim Delimiter As String = ","
        Dim columnCnt As Integer = 0

        hCont = 0
        HttpContext.Current.Session.Item("uploadcnt") = 0
        Try
            Using parser As New TextFieldParser(filename)
                parser.SetDelimiters(Delimiter)
                While Not parser.EndOfData


                    fields = parser.ReadFields()
                    'P = fields

                    columnCnt = fields.Length
                    Dim P(columnCnt + 1) As String
                    For y As Integer = 0 To columnCnt - 1
                        If hCont > 0 Then
                            ' fields(y) <> String.Empty AndAlso fields(y).Replace("""", "") <> "-" AndAlso
                            P(y) = CStr(fields(y).Replace("""", "").Trim)
                            If P(y).Length > 0 Then
                                If P(y).Substring(0, 1) = "'" Then
                                    P(y) = P(y).Replace("'", "")
                                End If
                            End If
                            If P(y).Trim = "-" Then
                                P(y) = "0"
                            End If
                            If P(y).Trim = String.Empty Or P(y).Trim = "" Then
                                P(y) = Nothing
                            End If
                        End If
                    Next
                    P(columnCnt) = P1
                    P(columnCnt + 1) = HttpContext.Current.Session("JobTestID")

                    If hCont > 0 Then
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, SP, P)
                        HttpContext.Current.Session.Item("uploadcnt") = CInt(HttpContext.Current.Session.Item("uploadcnt")) + 1
                    End If
                    hCont = 1
                End While
            End Using
            'delete temp file after upload
            If File.Exists(filename) = True Then
                File.Delete(filename)
            End If

            'Activity_Log(DateTime.Now, Processing.User_Name, SP, "0", "File Upload", "Uploaded " & uploadCount.ToString & " records from " & filename, 0, 0)
            Return True
        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
            Return False
        End Try
    End Function
    Public Shared Function ImportWithUsers(ByVal filename As String, ByVal SP As String, ByVal Page As String) As Boolean
        'Min 1 and Max 26 columns import

        Dim fields As String()

        Dim hCont As Integer = 0
        Dim Delimiter As String = ","
        Dim columnCnt As Integer = 0

        hCont = 0
        HttpContext.Current.Session.Item("uploadcnt") = 0
        Try
            Using parser As New TextFieldParser(filename)
                parser.SetDelimiters(Delimiter)
                While Not parser.EndOfData


                    fields = parser.ReadFields()
                    'P = fields

                    columnCnt = fields.Length
                    Dim P(columnCnt) As String
                    For y As Integer = 0 To columnCnt - 1
                        If hCont > 0 Then
                            ' fields(y) <> String.Empty AndAlso fields(y).Replace("""", "") <> "-" AndAlso
                            P(y) = CStr(fields(y).Replace("""", "").Trim)
                            If P(y).Length > 0 Then
                                If P(y).Substring(0, 1) = "'" Then
                                    P(y) = P(y).Replace("'", "")
                                End If
                            End If
                            If P(y).Trim = "-" Then
                                P(y) = "0"
                            End If
                            If P(y).Trim = String.Empty Or P(y).Trim = "" Then
                                P(y) = Nothing
                            End If
                        End If
                    Next
                    P(columnCnt) = HttpContext.Current.Session.Item("LoginID")

                    If hCont > 0 Then
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, SP, P)
                        HttpContext.Current.Session.Item("uploadcnt") = CInt(HttpContext.Current.Session.Item("uploadcnt")) + 1
                    End If
                    hCont = 1
                End While
            End Using
            'delete temp file after upload
            If File.Exists(filename) = True Then
                File.Delete(filename)
            End If

            'Activity_Log(DateTime.Now, Processing.User_Name, SP, "0", "File Upload", "Uploaded " & uploadCount.ToString & " records from " & filename, 0, 0)
            Return True
        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
            Return False
        End Try
    End Function
    Public Shared Function ImportTransposed(ByVal filename As String, ByVal SP As String, ByVal Page As String) As Boolean
        'Min 1 and Max 26 columns import

        Dim fields As String()
        Dim col0 As String = ""
        Dim col1 As String = ""
        Dim col2 As String = ""
        Dim C(100) As String


        Dim hCont As Integer = 0
        Dim Delimiter As String = ","
        Dim filecolumns As Integer = 0

        hCont = 0
        HttpContext.Current.Session.Item("uploadcnt") = 0
        Try
            'get header captions

            Using parser As New TextFieldParser(filename)
                parser.SetDelimiters(Delimiter)
                While Not parser.EndOfData


                    fields = parser.ReadFields()
                    filecolumns = fields.Count
                    Dim P(filecolumns) As String
                    'If hCont = 0 Then
                    '    C = fields
                    'End If

                    For y As Integer = 0 To fields.Length - 1
                        If hCont > 0 Then
                            ' fields(y) <> String.Empty AndAlso fields(y).Replace("""", "") <> "-" AndAlso
                            P(y) = CStr(fields(y).Replace("""", "").Trim)
                            If P(y).Length > 0 Then
                                If P(y).Substring(0, 1) = "'" Then
                                    P(y) = P(y).Replace("'", "")
                                End If
                            End If
                            If P(y).Trim = "-" Or P(y).Trim = String.Empty Or P(y).Trim = "" Then
                                P(y) = "0"
                            End If
                        Else
                            C(y) = CStr(fields(y).Replace("""", "").Trim)
                        End If
                    Next
                    'P(columnCnt) = HttpContext.Current.Session.Item("LoginID")

                    If hCont > 0 Then
                        For j As Integer = 0 To P.Count - 2
                            If j > 0 Then
                                col2 = P(j).ToString
                                col1 = C(j).ToString

                                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, SP, col0, col1, col2, HttpContext.Current.Session.Item("LoginID"))
                                HttpContext.Current.Session.Item("uploadcnt") = CInt(HttpContext.Current.Session.Item("uploadcnt")) + 1
                            Else
                                col0 = P(j).ToString
                            End If

                        Next

                    End If
                    hCont = 1
                End While
            End Using
            'delete temp file after upload
            If File.Exists(filename) = True Then
                File.Delete(filename)
            End If

            'Activity_Log(DateTime.Now, Processing.User_Name, SP, "0", "File Upload", "Uploaded " & uploadCount.ToString & " records from " & filename, 0, 0)
            Return True
        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
            Return False
        End Try
    End Function
    Public Shared Function Import1(ByVal filename As String, ByVal SP As String, ByVal Page As String) As Boolean
        'Min 1 and Max 26 columns import

        Dim fields As String()

        Dim hCont As Integer = 0
        Dim Delimiter As String = ","
        Dim columnCnt As Integer = 0

        hCont = 0
        HttpContext.Current.Session.Item("uploadcnt") = 0
        'Dim P As String()
        Try
            Using parser As New TextFieldParser(filename)
                parser.SetDelimiters(Delimiter)
                While Not parser.EndOfData


                    fields = parser.ReadFields()
                    'P = fields

                    columnCnt = fields.Length
                    Dim P(columnCnt) As String


                    For y As Integer = 0 To columnCnt - 1
                        If hCont > 0 Then
                            ' fields(y) <> String.Empty AndAlso fields(y).Replace("""", "") <> "-" AndAlso
                            P(y) = CStr(fields(y).Replace("""", "").Trim)
                            If P(y).Length > 0 Then
                                If P(y).Substring(0, 1) = "'" Then
                                    P(y) = P(y).Replace("'", "")
                                End If
                            End If

                            If P(y).Trim = "-" Then
                                P(y) = "0"
                            End If
                            If P(y).Trim = String.Empty Or P(y).Trim = "" Then
                                P(y) = Nothing
                            End If
                        End If
                    Next
                    If SP.ToLower = "emp_personaldetail_upload" Then
                        P(columnCnt) = Encrypt(CStr(fields(26).Replace("""", "").Trim))
                    End If


                    If hCont > 0 Then
                        If P(0) IsNot Nothing Then
                            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, SP, P)
                            HttpContext.Current.Session.Item("uploadcnt") = CInt(HttpContext.Current.Session.Item("uploadcnt")) + 1
                        End If
                    End If
                    hCont = 1
                End While
            End Using
            'delete temp file after upload
            If File.Exists(filename) = True Then
                File.Delete(filename)
            End If

            'Activity_Log(DateTime.Now, Processing.User_Name, SP, "0", "File Upload", "Uploaded " & uploadCount.ToString & " records from " & filename, 0, 0)
            Return True
        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
            Return False
        End Try
    End Function
    Public Shared Function Import(ByVal filename As String, ByVal SP As String, ByVal Page As String) As Boolean
        'Min 1 and Max 26 columns import

        Dim fields As String()

        Dim hCont As Integer = 0
        Dim Delimiter As String = ","
        Dim columnCnt As Integer = 0

        hCont = 0
        HttpContext.Current.Session.Item("uploadcnt") = 0
        'Dim P As String()
        Try
            Using parser As New TextFieldParser(filename)
                parser.SetDelimiters(Delimiter)
                While Not parser.EndOfData


                    fields = parser.ReadFields()
                    'P = fields

                    columnCnt = fields.Length
                    Dim P(columnCnt - 1) As String


                    For y As Integer = 0 To columnCnt - 1
                        If hCont > 0 Then
                            ' fields(y) <> String.Empty AndAlso fields(y).Replace("""", "") <> "-" AndAlso
                            P(y) = CStr(fields(y).Replace("""", "").Trim)
                            If P(y).Length > 0 Then
                                If P(y).Substring(0, 1) = "'" Then
                                    P(y) = P(y).Replace("'", "")
                                End If
                            End If

                            If P(y).Trim = "-" Then
                                P(y) = "0"
                            End If
                            If P(y).Trim = String.Empty Or P(y).Trim = "" Then
                                P(y) = Nothing
                            End If
                        End If
                    Next


                    If hCont > 0 Then
                        If P(0) IsNot Nothing Then
                            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, SP, P)
                            HttpContext.Current.Session.Item("uploadcnt") = CInt(HttpContext.Current.Session.Item("uploadcnt")) + 1
                        End If
                    End If
                    hCont = 1
                End While
            End Using
            'delete temp file after upload
            If File.Exists(filename) = True Then
                File.Delete(filename)
            End If

            'Activity_Log(DateTime.Now, Processing.User_Name, SP, "0", "File Upload", "Uploaded " & uploadCount.ToString & " records from " & filename, 0, 0)
            Return True
        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
            Return False
        End Try
    End Function
    Public Shared Function ResetEmployeeExpenseItem(ByVal scompany As String) As Boolean
        Try
            Using conn2 As New SqlConnection(WebConfig.ConnectionString)
                Dim comm2 As New SqlCommand("Finance_Non_Payroll_All_Reset", conn2)
                comm2.CommandType = CommandType.StoredProcedure
                comm2.Parameters.AddWithValue("@company", scompany)
                comm2.CommandTimeout = 157200
                Dim checkDS As New DataSet
                Dim sdat2 As New SqlDataAdapter(comm2)
                sdat2.Fill(checkDS)
                conn2.Close()
            End Using
            Return True
        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
            Return False
        End Try
    End Function
    Public Shared Function ResetEmployeeAllSalaryItem(ByVal scompany As String) As Boolean
        Try
            Using conn2 As New SqlConnection(WebConfig.ConnectionString)
                'Dim conn2 As New SqlConnection(WebConfig.ConnectionString)
                Dim comm2 As New SqlCommand("Finance_Salary_All_Reset", conn2)
                comm2.CommandType = CommandType.StoredProcedure
                comm2.Parameters.AddWithValue("@company", scompany)
                comm2.CommandTimeout = 157200
                Dim checkDS As New DataSet
                Dim sdat2 As New SqlDataAdapter(comm2)
                sdat2.Fill(checkDS)
                conn2.Close()
            End Using

            Return True
        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
            Return False
        End Try
    End Function
    Public Shared Function ResetEmployeeSalary(ByVal empidd As String) As String
        Try
            'Dim obj As String = "0"
            'Using conn2 As New SqlConnection(WebConfig.ConnectionString)
            '    Dim comm2 As New SqlCommand("Finance_Salary_All_Reset_Emp", conn2)
            '    comm2.CommandType = CommandType.StoredProcedure
            '    comm2.Parameters.AddWithValue("@company", empidd)
            '    comm2.CommandTimeout = 157200
            '    obj = comm2.ExecuteScalar()
            '    'conn2.Close()
            'End Using
            Dim strConnString As String = WebConfig.ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Finance_Salary_All_Reset_Emp"
            cmd.Parameters.Add("@empid", SqlDbType.VarChar).Value = empidd
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            con.Close()
            Return obj.ToString()

        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
            Return "0"
        End Try
    End Function
    Public Shared Function ResetEmployeeSalaryItem(ByVal scompany As String) As Boolean
        Try
            Using conn2 As New SqlConnection(WebConfig.ConnectionString)
                Dim comm2 As New SqlCommand("Finance_Salary_Reset", conn2)
                comm2.CommandType = CommandType.StoredProcedure
                comm2.Parameters.AddWithValue("@company", scompany)
                comm2.CommandTimeout = 157200
                Dim checkDS As New DataSet
                Dim sdat2 As New SqlDataAdapter(comm2)
                sdat2.Fill(checkDS)
                conn2.Close()
            End Using
            'Dim conn2 As New SqlConnection(WebConfig.ConnectionString)

            Return True
        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
            Return False
        End Try
    End Function
    Public Shared Function ProcessSinglePayslip(empid As String, startdate As Date, enddate As Date) As Boolean
        Try
            Using conn2 As New SqlConnection(WebConfig.ConnectionString)
                Dim comm2 As New SqlCommand("Finance_Generate_Single_PaySlip", conn2)
                comm2.CommandType = CommandType.StoredProcedure
                comm2.Parameters.AddWithValue("@EmpID", empid)
                comm2.Parameters.AddWithValue("@startdate", startdate)
                comm2.Parameters.AddWithValue("@enddate", enddate)
                comm2.CommandTimeout = 157200
                Dim checkDS As New DataSet
                Dim sdat2 As New SqlDataAdapter(comm2)
                sdat2.Fill(checkDS)
                conn2.Close()
            End Using
            'Dim conn2 As New SqlConnection(WebConfig.ConnectionString)


            Return True
        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
            Return False
        End Try
    End Function
    Public Shared Function ProcessPayslip(startdate As Date, enddate As Date, user As String, company As String) As String
        Try
            Dim result As String = ""
            'location As String, locvalue As Integer,
            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "finance_auto_loan_delete", startdate, enddate, company)
            Dim strLoan As New DataSet
            strLoan = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "finance_auto_loan_get", startdate, enddate, company)

            If strLoan.Tables(0).Rows.Count Then
                For i As Integer = 0 To strLoan.Tables(0).Rows.Count - 1
                    Dim loanref As String = strLoan.Tables(0).Rows(i).Item("LoanRefNo").ToString
                    Dim paydate As Date = strLoan.Tables(0).Rows(i).Item("PaymentDate")
                    Dim repayment As Double = strLoan.Tables(0).Rows(i).Item("Payment")
                    Dim intrate As Double = strLoan.Tables(0).Rows(i).Item("InterestRate")
                    Dim sUser As String = "auto"
                    Dim sMode As String = "auto"
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Loan_Schedule_Update", loanref, paydate, repayment, intrate, sUser, sMode)
                Next
            End If

            Using conn2 As New SqlConnection(WebConfig.ConnectionString)
                Dim comm2 As New SqlCommand("Finance_Generate_PaySlip", conn2)
                comm2.CommandType = CommandType.StoredProcedure
                comm2.Parameters.AddWithValue("@startdate", startdate)
                comm2.Parameters.AddWithValue("@enddate", enddate)
                comm2.Parameters.AddWithValue("@user", user)
                comm2.Parameters.AddWithValue("@company", company)
                comm2.CommandTimeout = 157200

                comm2.Connection = conn2
                conn2.Open()
                Dim obj As Object = comm2.ExecuteScalar()
                result = obj.ToString
                conn2.Close()
            End Using

            'Dim conn2 As New SqlConnection(WebConfig.ConnectionString)


            Return result
        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
            Return "0"
        End Try
    End Function
#End Region
#Region "Encryption Code"

    Public Shared Function Decrypt(ByVal cryptedString As String) As String
        Try
            Dim decryptedtext As String = ""
            If (cryptedString.Trim = "") Then
                decryptedtext = ""
            Else

                Dim cryptoProvider As DESCryptoServiceProvider = New DESCryptoServiceProvider()
                Dim memoryStream As MemoryStream = New MemoryStream(Convert.FromBase64String(cryptedString))
                Dim cryptoStrm As CryptoStream = New CryptoStream(memoryStream, cryptoProvider.CreateDecryptor(byts, byts), CryptoStreamMode.Read)
                Dim reader As StreamReader = New StreamReader(cryptoStrm)

                decryptedtext = reader.ReadToEnd()
            End If
            Return decryptedtext
        Catch
            Return ""
        End Try
    End Function
    Public Shared Function Encrypt(ByVal originalString As String) As String
        Try

            Dim encryptedtext As String = ""



            If originalString.Trim <> "" Then
                Dim cryptoProvider As DESCryptoServiceProvider = New DESCryptoServiceProvider()
                Dim MemoryStream As MemoryStream = New MemoryStream()
                Dim cryptoStrm As CryptoStream = New CryptoStream(MemoryStream, cryptoProvider.CreateEncryptor(byts, byts), CryptoStreamMode.Write)
                Dim writer As StreamWriter = New StreamWriter(cryptoStrm)
                writer.Write(originalString)
                writer.Flush()
                cryptoStrm.FlushFinalBlock()
                writer.Flush()
                encryptedtext = Convert.ToBase64String(MemoryStream.GetBuffer(), 0, CInt(MemoryStream.Length))
            Else
                encryptedtext = ""
            End If
            Return encryptedtext
        Catch
            Return ""
        End Try
    End Function

#End Region
#Region "EMAIL MESSAGES"
    Public Shared Sub MailNotification(ByVal msgrefid As String, ByVal msgcategoryid As Integer, ByVal msgsubject As String, ByVal msgbody As String, _
                                      ByVal receiver As String, ByVal sender As String, ByVal copied As String, ByVal mailbox As String, ByVal link As String, Optional ByVal strAttachmentPath As String = "")
        Try
            Dim mailid As String = "0"

            'SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_MailBox_Update", msgrefid, msgcategoryid, msgsubject, msgbody, receiver, sender, copied, mailbox, link)

            Dim strConnString As String = WebConfig.ConnectionString

            Using con As New SqlConnection(WebConfig.ConnectionString)
                Dim cmd As New SqlCommand()
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "Emp_MailBox_Update"
                cmd.Parameters.Add("@refid", SqlDbType.VarChar).Value = msgrefid
                cmd.Parameters.Add("@categoryid", SqlDbType.VarChar).Value = msgcategoryid
                cmd.Parameters.Add("@subject", SqlDbType.VarChar).Value = msgsubject
                cmd.Parameters.Add("@body", SqlDbType.VarChar).Value = msgbody
                cmd.Parameters.Add("@receiver", SqlDbType.VarChar).Value = receiver
                cmd.Parameters.Add("@sender", SqlDbType.VarChar).Value = sender
                cmd.Parameters.Add("@copied", SqlDbType.VarChar).Value = copied
                cmd.Parameters.Add("@mailbox", SqlDbType.VarChar).Value = mailbox
                cmd.Parameters.Add("@link", SqlDbType.VarChar).Value = link
                cmd.Connection = con
                con.Open()
                Dim obj As Object = cmd.ExecuteScalar()
                mailid = obj.ToString
                con.Close()
            End Using
            'Dim con As New SqlConnection(strConnString)


            Dim arrAttachment As String()
            If strAttachmentPath.Trim.Length > 0 Then
                arrAttachment = strAttachmentPath.Split(SeparatorSemi, System.StringSplitOptions.RemoveEmptyEntries)
                If arrAttachment.Length > 0 Then
                    For Each s As String In arrAttachment
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_MailBox_Attachement_Update", mailid, s.Trim)
                    Next
                End If
            End If
        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
        End Try
    End Sub
    Public Shared Function SendNotification() As String
        Try
            Dim notify As String = "No"
            Dim strDataSet As New DataSet
            strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Email_Configuration_get")
            For i As Integer = 0 To strDataSet.Tables(0).Rows.Count - 1
                notify = strDataSet.Tables(0).Rows(i).Item("sendnotification").ToString()
            Next
            Return notify
        Catch ex As Exception
            Return "No"
        End Try
    End Function
    Public Shared Function Mail_HR(ByVal receivermail As String, ByVal employeename As String, ByVal requesttype As String, ByVal copyMail As String, sempid As String, mgrid As String, mailtype As Integer, Optional slink As String = "") As Boolean
        Try
            msgbuild.Clear()
            msgbuild.AppendLine("Dear HR Manager,")
            msgbuild.AppendLine("")
            msgbuild.AppendLine("")
            msgbuild.AppendLine("You have just received a " & StrConv(requesttype, VbStrConv.ProperCase) & " for " & employeename & ", kindly proceed to " & requesttype & " page for necessary action.")
            msgbuild.AppendLine(" ")

            'Process.MailNotification(sempid, MailQuery, "Re:Query for " & queried, msgbuild.ToString, mgrid, Process.AppName, HRTeam, mgrid)
            Dim Arrays() As String = Process.GetEmpIDMailList("hr").Split(SeparatorSemi, StringSplitOptions.RemoveEmptyEntries)
            For i = 0 To Arrays.Count - 1
                Process.MailNotification(sempid, mailtype, requesttype & " for " & employeename, msgbuild.ToString, mgrid, Process.AppName, HRTeam, Arrays(i), slink, "")
            Next

            If SendNotification() = "Yes" Then
                Return Process.SendEmail("", GetCompanyName(HttpContext.Current.Session.Item("Organisation")), receivermail, _
                                   StrConv(requesttype, VbStrConv.ProperCase) & " for " & employeename, _
                                    "<div style=""font-family:Arial; font-size:12px;"">" & _
                                       ",<br />Dear HR Manager," & _
                                    ", <br /> <br />You have just received a " & requesttype & " for " & employeename & ", kindly proceed to " & requesttype & " page for necessary action." _
                                     & "<br /> <br /> <table> " _
                                     & "<tr><td style=""width:400px""> </td></tr>" _
                                     & "<tr><td style=""width:400px""> </td></tr>  </table> <br/> <br/> <a href=" & slink & ">" & slink & "</a></div>", _
                                 "", True, copyMail)
            Else
                Return True
            End If

        Catch ex As Exception
            'HttpContext.Current.Session.Item("exception") = ex.Message
            Return False
        End Try
    End Function
    Public Shared Function Credential_Acceptance_Request(ByVal certname As String, ByVal receivermail As String, ByVal employee As String, ByVal certtype As String, ByVal copyMail As String, sempid As String, mgrid As String, links As String) As Boolean
        Try
            msgbuild1.Clear()
            msgbuild1.AppendLine("Dear " & employee & ",")
            msgbuild1.AppendLine("")
            msgbuild1.AppendLine("")
            msgbuild1.AppendLine("Your new " & certtype & " has been forwarded to HR for acceptance")
            msgbuild1.AppendLine(" ")

            Dim subject As String = "New " & certtype & " added by " & employee
            Dim filePath As String = MailContentURL & MailFolderQualification & "Qualification_Approval_Request.txt"
            Dim readertxt As New StreamReader(filePath)
            msgbuild.Clear()
            While Not readertxt.EndOfStream
                Dim line As String = readertxt.ReadLine()
                msgbuild.AppendLine(line)
            End While
            readertxt.Close()
            msgbuild = msgbuild.Replace("@empname", GetEmployeeData(sempid, "fullname")).Replace("@mgrname", GetEmployeeData(mgrid, "fullname")).Replace("@company", GetCompanyByEmpID(mgrid)).Replace("@certtype", certtype) _
                .Replace("@certname", certname).Replace("@empid", sempid)

            Process.MailNotification(sempid, MailQualification, "New " & certtype & " added by " & employee, msgbuild1.ToString, sempid, Process.AppName, "", sempid, "", "")

            Dim Arrays() As String = Process.GetEmpIDMailList("hr").Split(SeparatorSemi, StringSplitOptions.RemoveEmptyEntries)
            For i = 0 To Arrays.Count - 1
                Process.MailNotification(sempid, MailQualification, subject, msgbuild.ToString, sempid, HRTeam, "", Arrays(i), links)
            Next

            If SendNotification() = "Yes" Then
                'Notification for requester
                Process.SendEmail("", GetEmployeeData(sempid, "company"), GetMailList("hr"), _
                                     subject, "<div style=""font-family:Arial; font-size:12px;"">" & msgbuild.ToString & " <br /> <br /><a href=" & links & ">" & links & "</a> <br /> <br />" & GetEmployeeData(sempid, "company") & "</div>", "", True)
            End If
            Return True

        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
            Return False
        End Try
    End Function

#Region "Work Force Planning"
    Public Shared Sub Work_Force_Complete(strBudgetYear As String, strDept As String, sempid As String, ByVal links As String)
        Try
            Dim subject As String = "Completion of " & strBudgetYear & " Workforce Plan for " & strDept
            Dim filePath As String = MailContentURL & MailFolderWorkForce & "Work_Force_Complete.txt"
            Dim readertxt As New StreamReader(filePath)
            msgbuild.Clear()
            While Not readertxt.EndOfStream
                Dim line As String = readertxt.ReadLine()
                msgbuild.AppendLine(line)
            End While
            readertxt.Close()
            msgbuild = msgbuild.Replace("@dept", strDept).Replace("@budgetyear", strBudgetYear).Replace("@company", GetCompanyName(strDept))

            Dim Arrays() As String = Process.GetEmpIDMailList("hr").Split(SeparatorSemi, StringSplitOptions.RemoveEmptyEntries)
            For i = 0 To Arrays.Count - 1
                Process.MailNotification(strBudgetYear, Process.MailWorkForcePlan, subject, msgbuild.ToString, HRTeam, sempid, "", Arrays(i), links)
            Next

            If SendNotification() = "Yes" Then
                'Final Notification of Requester
                Process.SendEmail("", Process.AppName, GetMailList("hr"), _
                           subject, _
                           "<div style=""font-family:Arial; font-size:12px;"">" & msgbuild.ToString & "</div> <br /> <a href=" & links & ">" & links & "</a> <br /> ", "", True, "")
            End If
        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
        End Try
    End Sub
    Public Shared Sub Work_Force_To_Approvers(sType As String, strBudgetYear As String, strDept As String, sempid As String, ByVal links As String)
        Try
            Dim subject As String = "Approval for " & strBudgetYear & " Workforce " & sType & " of " & strDept & " is required"
            Dim filePath As String = MailContentURL & MailFolderWorkForce & "Work_Force_To_Approvers.txt"
            Dim readertxt As New StreamReader(filePath)
            msgbuild.Clear()
            While Not readertxt.EndOfStream
                Dim line As String = readertxt.ReadLine()
                msgbuild.AppendLine(line)
            End While
            readertxt.Close()
            msgbuild = msgbuild.Replace("@dept", strDept).Replace("@budgetyear", strBudgetYear).Replace("@company", GetCompanyName(strDept)).Replace("@workforcetype", sType)


            Process.MailNotification(strBudgetYear, Process.MailWorkForcePlan, subject, msgbuild.ToString, sempid, HRTeam, "", sempid, links)

            If SendNotification() = "Yes" Then
                'Final Notification of Requester
                Process.SendEmail("", HRTeam, GetEmailAddress(sempid), _
                           subject, _
                           "<div style=""font-family:Arial; font-size:12px;"">" & msgbuild.ToString & " </div> <br /> <a href=" & links & ">" & links & "</a> <br /> ", "", True, "")
            End If
        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
        End Try
    End Sub
    Public Shared Sub Work_Force_Approvals(sType As String, strBudgetYear As String, strDept As String, approverid As String, approver As String, strStatus As String, strComment As String, ByVal links As String)
        Try
            'Send approval status to HR
            Dim subject As String = "Approval Notification from " & approver & ": " & strBudgetYear & " Workforce " & sType & " of " & strDept
            Dim filePath As String = MailContentURL & MailFolderWorkForce & "Work_Force_Approvals.txt"
            Dim readertxt As New StreamReader(filePath)
            msgbuild.Clear()
            While Not readertxt.EndOfStream
                Dim line As String = readertxt.ReadLine()
                msgbuild.AppendLine(line)
            End While
            readertxt.Close()
            msgbuild = msgbuild.Replace("@dept", strDept).Replace("@budgetyear", strBudgetYear).Replace("@company", GetCompanyName(strDept)).Replace("@workforcetype", sType).Replace("@mgrname", GetEmployeeData(approverid, "fullname")).Replace("@status", strStatus).Replace("@comment", strComment)

            Dim Arrays() As String = Process.GetEmpIDMailList("hr").Split(SeparatorSemi, StringSplitOptions.RemoveEmptyEntries)
            For i = 0 To Arrays.Count - 1
                Process.MailNotification(strBudgetYear, Process.MailWorkForcePlan, subject, msgbuild.ToString, HRTeam, approverid, "", Arrays(i), links)
            Next

            If SendNotification() = "Yes" Then
                'Final Notification of Requester
                Process.SendEmail("", Process.AppName, GetMailList("hr"), _
                           subject, _
                           "<div style=""font-family:Arial; font-size:12px;"">" & msgbuild.ToString & " </div> <br /> <a href=" & links & ">" & links & "</a> <br /> ", "", True, "")
            End If

        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
        End Try
    End Sub
    Public Shared Sub Work_Force_Approvals_Complete(sType As String, strBudgetYear As String, strDept As String, ByVal links As String)
        Try
            'Send approval status to HR
            Dim subject As String = "Complete Approval Notification: " & strBudgetYear & " Workforce " & sType & " of " & strDept
            Dim filePath As String = MailContentURL & MailFolderWorkForce & "Work_Force_Approvals_Complete.txt"
            Dim readertxt As New StreamReader(filePath)
            msgbuild.Clear()
            While Not readertxt.EndOfStream
                Dim line As String = readertxt.ReadLine()
                msgbuild.AppendLine(line)
            End While
            readertxt.Close()
            msgbuild = msgbuild.Replace("@dept", strDept).Replace("@budgetyear", strBudgetYear).Replace("@company", GetCompanyName(strDept)).Replace("@workforcetype", sType)

            Dim Arrays() As String = Process.GetEmpIDMailList("hr").Split(SeparatorSemi, StringSplitOptions.RemoveEmptyEntries)
            For i = 0 To Arrays.Count - 1
                Process.MailNotification(strBudgetYear, Process.MailWorkForcePlan, subject, msgbuild.ToString, HRTeam, Process.AppName, "", Arrays(i), links)
            Next

            If SendNotification() = "Yes" Then
                'Final Notification of Requester
                Process.SendEmail("", Process.AppName, GetMailList("hr"), _
                           subject, _
                           "<div style=""font-family:Arial; font-size:12px;"">" & msgbuild.ToString & " </div> <br /> <a href=" & links & ">" & links & "</a> <br /> ", "", True, "")
            End If

        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
        End Try
    End Sub
    Public Shared Sub Work_Force_Plan_Adopt(strBudgetYear As String, strDept As String, sempid As String, ByVal links As String, links2 As String)
        Try
            'Send approval status to HR
            Dim subject As String = "Workforce Plan " & strBudgetYear & " of " & strDept & "is Complete and adopted as Budget"
            Dim filePath As String = MailContentURL & MailFolderWorkForce & "Work_Force_Plan_Adopt.txt"
            Dim readertxt As New StreamReader(filePath)
            msgbuild.Clear()
            While Not readertxt.EndOfStream
                Dim line As String = readertxt.ReadLine()
                msgbuild.AppendLine(line)
            End While
            readertxt.Close()
            msgbuild = msgbuild.Replace("@dept", strDept).Replace("@budgetyear", strBudgetYear).Replace("@company", GetCompanyName(strDept)).Replace("@empname", GetEmployeeData(sempid, "fullname"))

            Dim Arrays() As String = Process.GetEmpIDMailList("hr").Split(SeparatorSemi, StringSplitOptions.RemoveEmptyEntries)
            For i = 0 To Arrays.Count - 1
                Process.MailNotification(strBudgetYear, Process.MailWorkForcePlan, subject, msgbuild.ToString, HRTeam, HRTeam, "", Arrays(i), links)
            Next
            Process.MailNotification(strBudgetYear, Process.MailWorkForcePlan, subject, msgbuild.ToString, sempid, HRTeam, "", sempid, links2)

            If SendNotification() = "Yes" Then
                'Final Notification of Requester

                Process.SendEmail("", GetCompanyName(strDept), GetEmailAddress(sempid), _
                           subject, _
                           "<div style=""font-family:Arial; font-size:12px;"">" & msgbuild.ToString & " </div> <br /> <br /> <a href=" & links2 & ">" & links2 & "</a> <br /> <a href=" & links & ">" & links & "</a> <br /> ", "", True, GetMailList("hr"))
            End If

        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
        End Try
    End Sub
#End Region

#Region "Query Notification"
    Public Shared Function Query_Notification_Reply(ByVal querydate As Date, ByVal copyMail As String, sempid As String, mgrid As String, links As String) As Boolean
        Try
            Dim subject As String = "Response to Query for  " & GetEmployeeData(sempid, "fullname")
            Dim filePath As String = MailContentURL & MailFolderQuery & "Query_Notification_Reply.txt"
            Dim readertxt As New StreamReader(filePath)
            msgbuild.Clear()
            While Not readertxt.EndOfStream
                Dim line As String = readertxt.ReadLine()
                msgbuild.AppendLine(line)
            End While
            readertxt.Close()
            msgbuild = msgbuild.Replace("@querydate", DDMONYYYY(querydate)).Replace("@mgrname", GetEmployeeData(mgrid, "fullname")).Replace("@company", GetCompanyByEmpID(mgrid)).Replace("@empname", GetEmployeeData(sempid, "fullname")).Replace("@empid", sempid)

            Process.MailNotification(sempid, MailQuery, subject, msgbuild.ToString, mgrid, sempid, HRTeam, mgrid, links)

            If SendNotification() = "Yes" Then
                Return Process.SendEmail("", GetCompanyName(GetEmployeeData(mgrid, "company")), GetEmailAddress(mgrid), _
                                  subject, _
                                    "<div style=""font-family:Arial; font-size:12px;"">" & msgbuild.ToString & "<br/> <br/> <a href=" & links & ">" & links & "</a>  </div>", _
                                 "", True, "")
            Else
                Return True
            End If

        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
            Return False
        End Try
    End Function
    Public Shared Function Query_Recommendation_HR(id As String, links As String) As Boolean
        Try

            Dim strquery As New DataSet
            Dim querydate As Date
            Dim expecteddate As Date
            Dim expectedtime As String = ""
            Dim query As String = ""
            Dim sempid As String = ""
            Dim mgrid As String = ""
            Dim empresponse As String = ""
            Dim empresponsedate As Date
            Dim mgrcomment As String = ""
            Dim hrcomment As String = ""
            Dim recommend As String = ""
            strquery = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Employee_Query_Get", id)
            If strquery.Tables(0).Rows.Count > 0 Then
                querydate = strquery.Tables(0).Rows(0).Item("QueryDate")
                expecteddate = strquery.Tables(0).Rows(0).Item("ExpectedResponseDate")
                query = strquery.Tables(0).Rows(0).Item("ROQuery").ToString
                sempid = strquery.Tables(0).Rows(0).Item("empid").ToString
                mgrid = strquery.Tables(0).Rows(0).Item("ReportingOfficerid").ToString
                empresponse = strquery.Tables(0).Rows(0).Item("EmpComment").ToString
                empresponsedate = strquery.Tables(0).Rows(0).Item("EmpResponseDate").ToString
                mgrcomment = strquery.Tables(0).Rows(0).Item("ROComment").ToString
                hrcomment = strquery.Tables(0).Rows(0).Item("HRComment").ToString
                recommend = strquery.Tables(0).Rows(0).Item("HRAction").ToString
                expectedtime = strquery.Tables(0).Rows(0).Item("ExpectedResponseTime").ToString
            End If

            Dim subject As String = "Recommendation: Query for " & GetEmployeeData(sempid, "fullname")
            Dim filePath As String = MailContentURL & MailFolderQuery & "Query_Recommendation.txt"
            Dim readertxt As New StreamReader(filePath)
            msgbuild.Clear()
            While Not readertxt.EndOfStream
                Dim line As String = readertxt.ReadLine()
                msgbuild.AppendLine(line)
            End While
            readertxt.Close()

            msgbuild = msgbuild.Replace("@querydate", DDMONYYYY(querydate)).Replace("@mgrname", GetEmployeeData(mgrid, "fullname")).Replace("@company", GetCompanyByEmpID(mgrid)).Replace("@empname", GetEmployeeData(sempid, "fullname")).Replace("@empid", sempid) _
                .Replace("@expecteddate", DDMONYYYY(expecteddate)).Replace("@expectedtime", expectedtime).Replace("@empresponsedate", empresponsedate.ToLongDateString & " " & empresponsedate.ToShortTimeString).Replace("@recommend", recommend).Replace("@query", query) _
                .Replace("@empresponse", empresponse).Replace("@mgrcomment", mgrcomment).Replace("@hrcomment", hrcomment)


            Dim Arrays() As String = Process.GetEmpIDMailList("hr").Split(SeparatorSemi, StringSplitOptions.RemoveEmptyEntries)
            For i = 0 To Arrays.Count - 1
                Process.MailNotification(sempid, MailQuery, subject, msgbuild.ToString, HRTeam, Process.AppName, "", Arrays(i), links)
            Next

            If SendNotification() = "Yes" Then
                Return Process.SendEmail("", GetCompanyName(GetEmployeeData(mgrid, "office")), GetMailList("hr"), _
                                         subject, _
                                           "<div style=""font-family:Arial; font-size:12px;"">" & _
                                            "" & msgbuild.ToString _
                                            & "<br/> <br/> <a href=" & links & ">" & links & "</a></div>", _
                                        "", True, "")
            Else
                Return True
            End If
        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
            Return False
        End Try
    End Function
    Public Shared Function Query_Recommendation(id As String, links As String, link2s As String) As Boolean
        Try
            Dim strquery As New DataSet
            Dim querydate As Date
            Dim expecteddate As Date
            Dim expectedtime As String = ""
            Dim query As String = ""
            Dim sempid As String = ""
            Dim mgrid As String = ""
            Dim empresponse As String = ""
            Dim empresponsedate As Date
            Dim mgrcomment As String = ""
            Dim hrcomment As String = ""
            Dim recommend As String = ""
            strquery = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Employee_Query_Get", id)
            If strquery.Tables(0).Rows.Count > 0 Then
                querydate = strquery.Tables(0).Rows(0).Item("QueryDate")
                expecteddate = strquery.Tables(0).Rows(0).Item("ExpectedResponseDate")
                query = strquery.Tables(0).Rows(0).Item("ROQuery").ToString
                sempid = strquery.Tables(0).Rows(0).Item("empid").ToString
                mgrid = strquery.Tables(0).Rows(0).Item("ReportingOfficerid").ToString
                empresponse = strquery.Tables(0).Rows(0).Item("EmpComment").ToString
                empresponsedate = strquery.Tables(0).Rows(0).Item("EmpResponseDate").ToString
                mgrcomment = strquery.Tables(0).Rows(0).Item("ROComment").ToString
                hrcomment = strquery.Tables(0).Rows(0).Item("HRComment").ToString
                recommend = strquery.Tables(0).Rows(0).Item("HRAction").ToString
                expectedtime = strquery.Tables(0).Rows(0).Item("ExpectedResponseTime").ToString
            End If

            Dim subject As String = "Recommendation: Query for " & GetEmployeeData(sempid, "fullname")
            Dim filePath As String = MailContentURL & MailFolderQuery & "Query_Recommendation.txt"
            Dim readertxt As New StreamReader(filePath)
            msgbuild.Clear()
            While Not readertxt.EndOfStream
                Dim line As String = readertxt.ReadLine()
                msgbuild.AppendLine(line)
            End While
            readertxt.Close()

            msgbuild = msgbuild.Replace("@querydate", DDMONYYYY(querydate)).Replace("@mgrname", GetEmployeeData(mgrid, "fullname")).Replace("@company", GetCompanyByEmpID(mgrid)).Replace("@empname", GetEmployeeData(sempid, "fullname")).Replace("@empid", sempid) _
                .Replace("@expecteddate", DDMONYYYY(expecteddate)).Replace("@expectedtime", expectedtime).Replace("@empresponsedate", empresponsedate.ToLongDateString & " " & empresponsedate.ToShortTimeString).Replace("@recommend", recommend).Replace("@query", query) _
                .Replace("@empresponse", empresponse).Replace("@mgrcomment", mgrcomment).Replace("@hrcomment", hrcomment)




            Process.MailNotification(sempid, MailQuery, subject, msgbuild.ToString, sempid, HRTeam, mgrid, sempid, link2s)
            Process.MailNotification(sempid, MailQuery, subject, msgbuild.ToString, sempid, HRTeam, mgrid, mgrid, links)


            If SendNotification() = "Yes" Then
                Return Process.SendEmail("", GetCompanyName(GetEmployeeData(mgrid, "office")), GetEmailAddress(sempid), _
                                         subject, _
                                           "<div style=""font-family:Arial; font-size:12px;"">" & _
                                            "" & msgbuild.ToString _
                                            & "<br /> <br /> <a href=" & link2s & ">" & link2s & "</a> <br /> <br /> <a href=" & links & ">" & links & "</a></div>", _
                                        "", True, GetEmailAddress(mgrid))
            Else
                Return True
            End If
        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
            Return False
        End Try
    End Function
    Public Shared Function Query_Completion_HR(ByVal querydate As Date, ByVal copyMail As String, sempid As String, mgrid As String, links As String) As Boolean
        Try
            Dim subject As String = "Query for " & GetEmployeeData(sempid, "fullname") & " Completed"
            Dim filePath As String = MailContentURL & MailFolderQuery & "Query_Completion_HR.txt"
            Dim readertxt As New StreamReader(filePath)
            msgbuild.Clear()
            While Not readertxt.EndOfStream
                Dim line As String = readertxt.ReadLine()
                msgbuild.AppendLine(line)
            End While
            readertxt.Close()

            msgbuild = msgbuild.Replace("@querydate", DDMONYYYY(querydate)).Replace("@mgrname", GetEmployeeData(mgrid, "fullname")) _
                .Replace("@company", GetCompanyByEmpID(mgrid)).Replace("@empname", GetEmployeeData(sempid, "fullname")).Replace("@empid", sempid)

            Dim Arrays() As String = Process.GetEmpIDMailList("hr").Split(SeparatorSemi, StringSplitOptions.RemoveEmptyEntries)
            For i = 0 To Arrays.Count - 1
                Process.MailNotification(sempid, MailQuery, subject, msgbuild.ToString, HRTeam, mgrid, "", Arrays(i), links)
            Next

            If SendNotification() = "Yes" Then
                Return Process.SendEmail("", GetCompanyName(GetEmployeeData(mgrid, "office")), GetMailList("hr"), _
                                         subject, _
                                           "<div style=""font-family:Arial; font-size:12px;"">" & msgbuild.ToString & "<br /> <br /><a href=" & links & ">" & links & "</a> </div>", _
                                        "", True, copyMail)
            Else
                Return True
            End If
        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
            Return False
        End Try
    End Function
    Public Shared Function Query_Notification_HR(ByVal querydate As Date, ByVal expecteddate As Date, ByVal expectedtime As String, ByVal copyMail As String, sempid As String, mgrid As String, links As String) As Boolean
        Try
            Dim subject As String = "Query for " & GetEmployeeData(sempid, "fullname") & " of " & GetEmployeeData(sempid, "office")
            Dim filePath As String = MailContentURL & MailFolderQuery & "Query_Notification_HR.txt"
            Dim readertxt As New StreamReader(filePath)
            msgbuild.Clear()
            While Not readertxt.EndOfStream
                Dim line As String = readertxt.ReadLine()
                msgbuild.AppendLine(line)
            End While
            readertxt.Close()
            msgbuild = msgbuild.Replace("@querydate", DDMONYYYY(querydate)).Replace("@mgrname", GetEmployeeData(mgrid, "fullname")).Replace("@company", GetCompanyByEmpID(mgrid)).Replace("@empname", GetEmployeeData(sempid, "fullname")).Replace("@empid", sempid) _
                .Replace("@expecteddate", DDMONYYYY(expecteddate)).Replace("@expectedtime", expectedtime)


            Dim Arrays() As String = Process.GetEmpIDMailList("hr").Split(SeparatorSemi, StringSplitOptions.RemoveEmptyEntries)
            For i = 0 To Arrays.Count - 1
                Process.MailNotification(sempid, MailQuery, subject, msgbuild.ToString, sempid, mgrid, HRTeam, Arrays(i), links)
            Next

            If SendNotification() = "Yes" Then
                Return Process.SendEmail("", GetCompanyName(GetEmployeeData(mgrid, "office")), GetMailList("hr"), _
                                         subject, _
                                           "<div style=""font-family:Arial; font-size:12px;"">" & msgbuild.ToString & "<br /> <br /><a href=" & links & ">" & links & "</a> </div>", _
                                        "", True, copyMail)
            Else
                Return True
            End If
        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
            Return False
        End Try
    End Function
    Public Shared Function Query_Notification(ByVal querydate As Date, ByVal expecteddate As Date, ByVal expectedtime As String, ByVal copyMail As String, sempid As String, mgrid As String, links As String) As Boolean
        Try
            Dim subject As String = "Query for " & GetEmployeeData(sempid, "fullname")

            Dim filePath As String = MailContentURL & MailFolderQuery & "Query_Notification.txt"
            Dim readertxt As New StreamReader(filePath)
            msgbuild.Clear()
            While Not readertxt.EndOfStream
                Dim line As String = readertxt.ReadLine()
                msgbuild.AppendLine(line)
            End While
            readertxt.Close()
            msgbuild = msgbuild.Replace("@empfname", GetEmployeeData(sempid, "fullname")).Replace("@querydate", DDMONYYYY(querydate)).Replace("@mgrname", GetEmployeeData(mgrid, "fullname")).Replace("@company", GetCompanyByEmpID(mgrid)).Replace("@empname", GetEmployeeData(sempid, "fullname")).Replace("@empid", sempid) _
                .Replace("@expecteddate", DDMONYYYY(expecteddate)).Replace("@expectedtime", expectedtime)


            Process.MailNotification(sempid, MailQuery, subject, msgbuild.ToString, sempid, mgrid, HRTeam, sempid, links)

            If SendNotification() = "Yes" Then
                Return Process.SendEmail("", GetCompanyName(GetEmployeeData(mgrid, "office")), GetEmailAddress(sempid), _
                                         subject, _
                                           "<div style=""font-family:Arial; font-size:12px;"">" & _
                                              ",<br />" & msgbuild.ToString & "<br /> <br /><a href=" & links & ">" & links & "</a> </div>", _
                                        "", True, copyMail)
            Else
                Return True
            End If
        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
            Return False
        End Try
    End Function
#End Region
#Region "Exit Notification"
    Public Shared Function Exit_Approval_By_HR(exitdate As Date, ByVal approval As String, ByVal approvaldate As Date, ByVal comment As String, ByVal copyMail As String, sempid As String, mgrid As String, links As String, links2 As String, attachement As String) As Boolean
        Try
            Dim subject As String = "Re: Employeement Exit request of " & GetEmployeeData(sempid, "fullname") & " is " & approval

            Dim filePath As String = MailContentURL & MailFolderExit & "Exit_Approval_By_HR.txt"
            Dim readertxt As New StreamReader(filePath)
            msgbuild.Clear()
            While Not readertxt.EndOfStream
                Dim line As String = readertxt.ReadLine()
                msgbuild.AppendLine(line)
            End While
            readertxt.Close()
            msgbuild = msgbuild.Replace("@empfname", GetEmployeeData(sempid, "fullname")).Replace("@mgrname", GetEmployeeData(mgrid, "fullname")).Replace("@company", GetCompanyByEmpID(sempid)).Replace("@empname", GetEmployeeData(sempid, "fullname")).Replace("@empid", sempid) _
                .Replace("@exitdate", DDMONYYYY(exitdate)).Replace("@comment", comment).Replace("@status", approval).Replace("@dept", GetEmployeeData(sempid, "office"))


            Process.MailNotification(sempid, MailEmployeeExit, subject, msgbuild.ToString, sempid, Process.AppName, mgrid, sempid, "", attachement)
            Process.MailNotification(sempid, MailEmployeeExit, subject, msgbuild.ToString, sempid, Process.AppName, mgrid, mgrid, links2, attachement)

            Dim Arrays() As String = Process.GetEmpIDMailList("hr").Split(SeparatorSemi, StringSplitOptions.RemoveEmptyEntries)
            For i = 0 To Arrays.Count - 1
                Process.MailNotification(sempid, MailEmployeeExit, subject, msgbuild.ToString, sempid, Process.AppName, mgrid, Arrays(i), links, attachement)
            Next

            If SendNotification() = "Yes" Then
                Return Process.SendEmail("", GetCompanyByEmpID(sempid), GetEmailAddress(sempid), _
                                 subject, _
                                   "<div style=""font-family:Arial; font-size:12px;"">" & msgbuild.ToString & "<br /> <br /><a href=" & links & ">" & links & "</a> </div>", _
                                attachement, True, copyMail)
            Else
                Return True
            End If

        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
            Return False
        End Try
    End Function
    Public Shared Function Exit_Approval_To_HR(sstatus As String, ByVal comment As String, sempid As String, mgrid As String, links As String) As Boolean
        Try
            Dim empname As String = GetEmployeeData(sempid, "fullname")
            Dim subject As String = "Re:Employment Exit from " & empname

            Dim filePath As String = MailContentURL & MailFolderExit & "Exit_Approval_To_HR.txt"
            Dim readertxt As New StreamReader(filePath)
            msgbuild.Clear()
            While Not readertxt.EndOfStream
                Dim line As String = readertxt.ReadLine()
                msgbuild.AppendLine(line)
            End While
            readertxt.Close()
            msgbuild = msgbuild.Replace("@empfname", GetEmployeeData(sempid, "fullname")).Replace("@mgrname", GetEmployeeData(mgrid, "fullname")).Replace("@company", GetCompanyByEmpID(sempid)).Replace("@empname", GetEmployeeData(sempid, "fullname")).Replace("@empid", sempid) _
                .Replace("@comment", comment).Replace("@dept", GetEmployeeData(sempid, "office")).Replace("@status", sstatus)



            Dim Arrays() As String = Process.GetEmpIDMailList("hr").Split(SeparatorSemi, StringSplitOptions.RemoveEmptyEntries)
            For i = 0 To Arrays.Count - 1
                Process.MailNotification(sempid, MailEmployeeExit, subject, msgbuild.ToString, HRTeam, mgrid, "", Arrays(i), links)
            Next

            If SendNotification() = "Yes" Then
                'Notification for requester
                Return Process.SendEmail("", GetEmployeeData(sempid, "company"), GetMailList("hr"), _
                                     subject, "<div style=""font-family:Arial; font-size:12px;"">" & msgbuild.ToString & "<br /> <br /><a href=" & links & ">" & links & "</a> <br /> <br />" & GetEmployeeData(sempid, "company") & "</div>", "", True)
            Else
                Return True
            End If


        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
            Return False
        End Try
    End Function
    Public Shared Function Exit_From_HR(ByVal exitdate As Date, ByVal exittype As String, ByVal reason As String, ByVal copyMail As String, sempid As String, mgrid As String, links As String) As Boolean
        Try
            Dim subject As String = "Employeement Exit Notification for " & GetEmployeeData(sempid, "fullname") & " requires your approval"
            Dim filePath As String = MailContentURL & MailFolderExit & "Exit_From_HR.txt"
            Dim readertxt As New StreamReader(filePath)
            msgbuild.Clear()
            While Not readertxt.EndOfStream
                Dim line As String = readertxt.ReadLine()
                msgbuild.AppendLine(line)
            End While
            readertxt.Close()
            msgbuild = msgbuild.Replace("@empfname", GetEmployeeData(sempid, "fullname")).Replace("@mgrname", GetEmployeeData(mgrid, "fullname")).Replace("@company", GetCompanyByEmpID(sempid)).Replace("@empname", GetEmployeeData(sempid, "fullname")).Replace("@empid", sempid) _
                .Replace("@reason", reason).Replace("@dept", GetEmployeeData(sempid, "office")).Replace("@supervisor", GetEmployeeName(GetEmployeeData(sempid, "linemanagerid"))).Replace("@job", GetEmployeeData(sempid, "jobtitle")) _
                .Replace("@exitdate", Process.DDMONYYYY(exitdate))



            Process.MailNotification(sempid, MailEmployeeExit, subject, msgbuild.ToString, mgrid, HRTeam, "", mgrid, links)

            If SendNotification() = "Yes" Then
                Return Process.SendEmail("", HRTeam, GetEmailAddress(mgrid), _
                                   subject, _
                                     "<div style=""font-family:Arial; font-size:12px;"">" & msgbuild.ToString & "<br /> <br /> <a href=" & links & ">" & links & "</a></div>", _
                                  "", True, copyMail)
            Else
                Return True
            End If

        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
            Return False
        End Try
    End Function
    Public Shared Function Exit_For_HOD_Approval(ByVal exitdate As Date, ByVal exittype As String, ByVal reason As String, sempid As String, mgrid As String, links As String) As Boolean
        Try
            Dim subject As String = "Employment Exit request by " & GetEmployeeData(sempid, "fullname")
            Dim filePath As String = MailContentURL & MailFolderExit & "Exit_From_HR.txt"
            Dim readertxt As New StreamReader(filePath)
            msgbuild.Clear()
            While Not readertxt.EndOfStream
                Dim line As String = readertxt.ReadLine()
                msgbuild.AppendLine(line)
            End While
            readertxt.Close()
            msgbuild = msgbuild.Replace("@empfname", GetEmployeeData(sempid, "fullname")).Replace("@mgrname", GetEmployeeData(mgrid, "fullname")).Replace("@company", GetCompanyByEmpID(sempid)).Replace("@empname", GetEmployeeData(sempid, "fullname")).Replace("@empid", sempid) _
                .Replace("@reason", reason).Replace("@dept", GetEmployeeData(sempid, "office")).Replace("@supervisor", GetEmployeeName(GetEmployeeData(sempid, "linemanagerid"))).Replace("@job", GetEmployeeData(sempid, "jobtitle")) _
                .Replace("@exitdate", Process.DDMONYYYY(exitdate))

            Process.MailNotification(sempid, MailEmployeeExit, subject, msgbuild.ToString, mgrid, sempid, "", mgrid, links)

            If SendNotification() = "Yes" Then
                'Notification for requester
                Return Process.SendEmail("", GetEmployeeData(sempid, "company"), GetEmailAddress(mgrid), _
                                     subject, "<div style=""font-family:Arial; font-size:12px;"">" & msgbuild.ToString & "<br /> <br /><a href=" & links & ">" & links & "</a> <br /> <br />" & GetEmployeeData(sempid, "company") & "</div>", "", True)
            Else
                Return True
            End If

        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
            Return False
        End Try
    End Function
#End Region
#Region "Users"
    Public Shared Function User_Notification(ByVal receiver As String, ByVal staffname As String, userid As String, pwd As String, loginurl As String) As Boolean
        Try
            Return SendEmail("", Process.AppName, receiver, _
                                   "User Account on " & Process.AppName, _
                                   "<div style=""font-family:Arial; font-size:12px;"">Dear " & staffname & ", <br /> <br /> A new account have been created for you on " & Process.AppName _
                                   & ". You can login with the following credentials: <br /> <br /> <br /> <table> <tr><td style=""width:100px"">User Name: </td> <td style=""width:42px""> </td><td style=""width:250px"">" & userid & _
                                   "</td> </tr>	<tr><td style=""width:100px"">Password: </td> <td style=""width:42px""> </td><td style=""width:250px"">" & pwd & " (Please change your password on your first login)" & _
                                   "</td> </tr>	</table>  </div> <br /> <br /><a href=" & loginurl & ">Click Here to Login</a>", _
                                   "", True)
        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
            Return False
        End Try
    End Function
#End Region
#Region "Staff Requisition"
    Public Shared Function Staff_Requisition_Alert_Approver(ByVal hiringmgr As String, ByVal jobpost As String, ByVal dept As String, ByVal slots As Integer, ByVal resumption As Date, ByVal copyMail As String, sempid As String, mgrid As String, links As String) As Boolean
        Try
            Dim subject As String = "Staff Requisition: " & jobpost
            Dim filePath As String = MailContentURL & MailFolderRecruitment & "Staff_Requisition_Alert_Approver.txt"
            Dim readertxt As New StreamReader(filePath)
            msgbuild.Clear()
            While Not readertxt.EndOfStream
                Dim line As String = readertxt.ReadLine()
                msgbuild.AppendLine(line)
            End While
            readertxt.Close()
            msgbuild = msgbuild.Replace("@empfname", GetEmployeeData(sempid, "firstname")).Replace("@mgrname", GetEmployeeData(mgrid, "fullname")).Replace("@company", GetCompanyByEmpID(mgrid)).Replace("@empname", GetEmployeeData(sempid, "fullname")).Replace("@empid", sempid) _
                .Replace("@dept", dept).Replace("@hiringmgr", hiringmgr).Replace("@job", jobpost).Replace("@number", FormatNumber(slots, 0)).Replace("@resumptiondate", DDMONYYYY(resumption))


            Process.MailNotification("0", MailRecruitment, subject, msgbuild.ToString, mgrid, HRTeam, "", mgrid, links)

            If SendNotification() = "Yes" Then
                Return Process.SendEmail("", Process.GetCompanyByEmpID(mgrid), GetEmailAddress(mgrid), _
                                   subject, _
                                     "<div style=""font-family:Arial; font-size:12px;"">" & _
                                     ", <br /> <br /> " & msgbuild.ToString & "<br /> <br /> <a href=" & links & ">" & links & "</a></div>", _
                                  "", True, "")
            Else
                Return True
            End If


        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
            Return False
        End Try
    End Function

    'Interview Detail Complete Notification
    Public Shared Function Interview_Detail_Alert_Approver_complete(ByVal interviewer As String, ByVal interviewee As String, ByVal dept As String, ByVal gender As String, sempid As String, mgrid As String, links As String) As Boolean
        Try
            Dim subject As String = "Interview Evaluation Complete"

            Dim filePath As String = MailContentURL & MailFolderRecruitment & "Interview_Evaluation_Complete_Approvals.txt"
            Dim readertxt As New StreamReader(filePath)
            msgbuild.Clear()
            While Not readertxt.EndOfStream
                Dim line As String = readertxt.ReadLine()
                msgbuild.AppendLine(line)
            End While
            readertxt.Close()
            msgbuild = msgbuild.Replace("@company", GetCompanyName(dept)) _
                .Replace("@dept", dept).Replace("@hiringmgr", interviewer).Replace("@interviewee", interviewee).Replace("@gender", gender)

            Process.MailNotification("0", MailRecruitment, subject, msgbuild.ToString, mgrid, GetCompanyName(dept), sempid, mgrid, links)
            Process.MailNotification("0", MailRecruitment, subject, msgbuild.ToString, mgrid, GetCompanyName(dept), sempid, sempid, links)


            Dim Arrays() As String = Process.GetEmpIDMailList("hr").Split(SeparatorSemi, StringSplitOptions.RemoveEmptyEntries)
            For i = 0 To Arrays.Count - 1
                Process.MailNotification("0", MailRecruitment, subject, msgbuild.ToString, HRTeam, GetCompanyName(dept), "", Arrays(i), links)
            Next

            If SendNotification() = "Yes" Then
                Process.SendEmail("", GetCompanyName(dept), GetEmailAddress(mgrid), _
                                   subject, _
                                     "<div style=""font-family:Arial; font-size:12px;"">" & _
                                     ", <br /> <br /> " & msgbuild.ToString & "</td></tr> </table>" _
                                      & "<br /> <br /> <a href=" & links & ">" & links & "</a></div>", _
                                  "", True, GetEmailAddress(sempid))

                Return Process.SendEmail("", GetCompanyName(dept), GetMailList("hr"), _
                                   subject, _
                                     "<div style=""font-family:Arial; font-size:12px;"">" & _
                                     ", <br /> <br /> " & msgbuild.ToString & "</td></tr> </table>" _
                                      & "<br /> <br /> <a href=" & links & ">" & links & "</a></div>", _
                                  "", True, "")
            Else
                Return True
            End If


        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
            Return False
        End Try
    End Function

    Public Shared Function Staff_Requisition_Alert_Approvals(stype As String, sStatus As String, sComment As String, ByVal hiringmgr As String, ByVal approver As String, ByVal jobpost As String, ByVal dept As String, ByVal slots As Integer, ByVal resumption As Date, ByVal copyMail As String, links As String) As Boolean
        Try
            Dim subject As String = "Staff Requisition Higher Approval: " & jobpost
            If stype = "hod" Then
                subject = "Staff Requisition HOD Approval: " & jobpost
            End If

            Dim filePath As String = MailContentURL & MailFolderRecruitment & "Staff_Requisition_Alert_Approvals.txt"
            Dim readertxt As New StreamReader(filePath)
            msgbuild.Clear()
            While Not readertxt.EndOfStream
                Dim line As String = readertxt.ReadLine()
                msgbuild.AppendLine(line)
            End While
            readertxt.Close()
            msgbuild = msgbuild.Replace("@mgrname", GetEmployeeData(approver, "fullname")).Replace("@company", GetCompanyByEmpID(approver)) _
                .Replace("@dept", dept).Replace("@hiringmgr", hiringmgr).Replace("@job", jobpost).Replace("@number", FormatNumber(slots, 0)).Replace("@resumptiondate", DDMONYYYY(resumption)) _
                .Replace("@status", sStatus).Replace("@comment", sComment)




            Dim Arrays() As String = Process.GetEmpIDMailList("hr").Split(SeparatorSemi, StringSplitOptions.RemoveEmptyEntries)
            For i = 0 To Arrays.Count - 1
                Process.MailNotification("0", MailRecruitment, subject, msgbuild.ToString, HRTeam, GetEmployeeData(approver, "fullname"), "", Arrays(i), links)
            Next

            If SendNotification() = "Yes" Then
                Return Process.SendEmail("", GetCompanyByEmpID(approver), GetMailList("hr"), _
                                   subject, _
                                     "<div style=""font-family:Arial; font-size:12px;"">" & msgbuild.ToString & " <br /> <br /> <a href=" & links & ">" & links & "</a></div>", _
                                  "", True, "")
            Else
                Return True
            End If


        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
            Return False
        End Try
    End Function
    Public Shared Function Staff_Requisition_Approvals_Complete(ByVal hiringmgr As String, ByVal jobpost As String, ByVal dept As String, ByVal slots As Integer, ByVal resumption As Date, sempid As String, mgrid As String, links As String, link2s As String) As Boolean
        Try
            Dim subject As String = "Staff Requisition Approvals Complete: " & jobpost

            Dim filePath As String = MailContentURL & MailFolderRecruitment & "Staff_Requisition_Approvals_Complete.txt"
            Dim readertxt As New StreamReader(filePath)
            msgbuild.Clear()
            While Not readertxt.EndOfStream
                Dim line As String = readertxt.ReadLine()
                msgbuild.AppendLine(line)
            End While
            readertxt.Close()
            msgbuild = msgbuild.Replace("@company", GetCompanyName(dept)) _
                .Replace("@dept", dept).Replace("@hiringmgr", hiringmgr).Replace("@job", jobpost).Replace("@number", FormatNumber(slots, 0)).Replace("@resumptiondate", DDMONYYYY(resumption))

            Process.MailNotification("0", MailRecruitment, subject, msgbuild.ToString, mgrid, GetCompanyName(dept), sempid, mgrid, link2s)
            Process.MailNotification("0", MailRecruitment, subject, msgbuild.ToString, mgrid, GetCompanyName(dept), sempid, sempid, link2s)


            Dim Arrays() As String = Process.GetEmpIDMailList("hr").Split(SeparatorSemi, StringSplitOptions.RemoveEmptyEntries)
            For i = 0 To Arrays.Count - 1
                Process.MailNotification("0", MailRecruitment, subject, msgbuild.ToString, HRTeam, GetCompanyName(dept), "", Arrays(i), links)
            Next

            If SendNotification() = "Yes" Then
                Process.SendEmail("", GetCompanyName(dept), GetEmailAddress(mgrid), _
                                   subject, _
                                     "<div style=""font-family:Arial; font-size:12px;"">" & _
                                     ", <br /> <br /> " & msgbuild.ToString & "</td></tr> </table>" _
                                      & "<br /> <br /> <a href=" & links & ">" & links & "</a></div>", _
                                  "", True, GetEmailAddress(sempid))

                Return Process.SendEmail("", GetCompanyName(dept), GetMailList("hr"), _
                                   subject, _
                                     "<div style=""font-family:Arial; font-size:12px;"">" & _
                                     ", <br /> <br /> " & msgbuild.ToString & "</td></tr> </table>" _
                                      & "<br /> <br /> <a href=" & links & ">" & links & "</a></div>", _
                                  "", True, "")
            Else
                Return True
            End If


        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
            Return False
        End Try
    End Function

    'Public Shared Function Staff_Requisition_Complete(ByVal jobpost As String, ByVal dept As String, ByVal slots As Integer, ByVal resumption As Date, sempid As String, mgrid As String, ByVal links As String) As Boolean
    '    Try
    '        Dim line1 As String = "Staff Requisition approvals are complete"
    '        Dim line2 As String = "Here are the details:"
    '        Dim subject As String = "Staff Requisition Approvals Complete: " & jobpost
    '        msgbuild.Clear()
    '        msgbuild.AppendLine(line1)
    '        msgbuild.AppendLine(" ")
    '        msgbuild.AppendLine(line2)
    '        msgbuild.AppendLine(" ")
    '        msgbuild.AppendLine("Dept/Unit        : " & dept)
    '        msgbuild.AppendLine("Position Desc.   : " & jobpost)
    '        msgbuild.AppendLine("No. of Positions : " & slots)
    '        msgbuild.AppendLine("Latest Resumption: " & resumption.ToLongDateString)


    '        Process.MailNotification("0", MailRecruitment, subject, msgbuild.ToString, mgrid, HRTeam, "", mgrid, links)
    '        Process.MailNotification("0", MailRecruitment, subject, msgbuild.ToString, sempid, HRTeam, "", sempid, links)

    '        Dim Arrays() As String = Process.GetEmpIDMailList("hr").Split(SeparatorSemi, StringSplitOptions.RemoveEmptyEntries)
    '        For i = 0 To Arrays.Count - 1
    '            If Arrays(i) <> sempid And Arrays(i) <> mgrid Then
    '                Process.MailNotification("0", MailRecruitment, subject, msgbuild.ToString, HRTeam, Process.AppName, "", Arrays(i), "")
    '            End If
    '        Next

    '        If SendNotification() = "Yes" Then
    '            Process.SendEmail("", Process.AppName, GetMailList("hr"), _
    '                             subject, _
    '                               "<div style=""font-family:Arial; font-size:12px;"">" & _
    '                               ", <br /> <br /> " & line1 _
    '                               & "<br /> <br /> " & line2 _
    '                               & "<br /> <br /> <br /> <table> <tr><td style=""width:150px"">Position Desc.: </td> <td style=""width:42px""> </td><td style=""width:50px"">" & jobpost _
    '                               & "</td> </tr> <tr><td style=""width:150px"">No. of Positions: </td> <td style=""width:42px""> </td><td style=""width:250px"">" & slots & _
    '                               "</td> </tr>	<tr><td style=""width:150px"">Expected Resumption: </td> <td style=""width:42px""> </td><td style=""width:250px"">" & resumption.ToLongDateString _
    '                               & "</td> </tr> <tr><td style=""width:150px"">Department/Office: </td> <td style=""width:42px""> </td><td style=""width:250px"">" & dept & "</td></tr> </table>" _
    '                                & "<br /> <br /> <table> " _
    '                                & "<tr><td style=""width:400px"">Kindly consider for your review. </td></tr>" _
    '                                & "<tr><td style=""width:400px""> </td></tr></table> </div> <br /> <a href=" & links & ">" & links & "</a> <br /> " _
    '              & "<br /><br /> " & Process.AppName, _
    '                          "", True, "")

    '            Return Process.SendEmail("", Process.AppName, GetEmailAddress(mgrid), _
    '                                 subject, _
    '                                   "<div style=""font-family:Arial; font-size:12px;"">" & _
    '                                   ", <br /> <br /> " & line1 _
    '                                   & "<br /> <br /> " & line2 _
    '                                   & "<br /> <br /> <br /> <table> <tr><td style=""width:150px"">Position Desc.: </td> <td style=""width:42px""> </td><td style=""width:50px"">" & jobpost _
    '                                   & "</td> </tr> <tr><td style=""width:150px"">No. of Positions: </td> <td style=""width:42px""> </td><td style=""width:250px"">" & slots & _
    '                                   "</td> </tr>	<tr><td style=""width:150px"">Expected Resumption: </td> <td style=""width:42px""> </td><td style=""width:250px"">" & resumption.ToLongDateString _
    '                                   & "</td> </tr> <tr><td style=""width:150px"">Department/Office: </td> <td style=""width:42px""> </td><td style=""width:250px"">" & dept & "</td></tr> </table>" _
    '                                    & "<br /> <br /> <table> " _
    '                                    & "<tr><td style=""width:400px"">Kindly consider for your review. </td></tr>" _
    '                                    & "<tr><td style=""width:400px""> </td></tr></table> </div> <br /> <a href=" & links & ">" & links & "</a> <br /> " _
    '                  & "<br /><br /> " & Process.AppName, _
    '                              "", True, GetEmailAddress(sempid))
    '        Else
    '            Return True
    '        End If

    '    Catch ex As Exception
    '        HttpContext.Current.Session.Item("exception") = ex.Message
    '        Return False
    '    End Try
    'End Function
#End Region
#Region "Succession Plan"
    Public Shared Sub Staff_Succession_Approver_HR_Reply(sindex As String, status As String, scomment As String, oldjobtitle As String, oldjobgrade As String, pjobtitle As String, pjobgrade As String, sempid As String, mgrid As String, links As String)
        Try
            Dim subject As String = "Approval " & sindex & " RE: Succession Request for " & GetEmployeeData(sempid, "fullname")
            Dim filePath As String = MailContentURL & MailFolderSuccession & "Staff_Succession_Approver_HR_Reply.txt"
            Dim readertxt As New StreamReader(filePath)
            msgbuild.Clear()
            While Not readertxt.EndOfStream
                Dim line As String = readertxt.ReadLine()
                msgbuild.AppendLine(line)
            End While
            readertxt.Close()
            msgbuild = msgbuild.Replace("@empname", GetEmployeeData(sempid, "fullname")).Replace("@mgrname", GetEmployeeData(mgrid, "fullname")).Replace("@company", GetCompanyByEmpID(mgrid)).Replace("@comment", scomment) _
                .Replace("@oldjob", oldjobtitle).Replace("@oldgrade", oldjobgrade).Replace("@job", pjobtitle).Replace("@grade", pjobgrade).Replace("@status", status).Replace("@empid", sempid)


            Dim Arrays() As String = Process.GetEmpIDMailList("hr").Split(SeparatorSemi, StringSplitOptions.RemoveEmptyEntries)
            For i = 0 To Arrays.Count - 1
                Process.MailNotification(sempid, Process.MailSuccessionPlan, subject, msgbuild.ToString, sempid, HRTeam, "", Arrays(i), links)
            Next

            If SendNotification() = "Yes" Then
                'Notification for requester
                Process.SendEmail("", GetEmployeeData(sempid, "company"), GetMailList("hr"), _
                                     subject, "<div style=""font-family:Arial; font-size:12px;"">" & msgbuild.ToString & " <br /> <br /><a href=" & links & ">" & links & "</a> <br /> <br />" & GetEmployeeData(sempid, "company") & "</div>", "", True)
            End If
            '<a href=" & links & ">" & links & "</a>
        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
        End Try
    End Sub
    Public Shared Sub Staff_Succession_Approver_Notification(oldjobtitle As String, oldjobgrade As String, pjobtitle As String, pjobgrade As String, sempid As String, mgrid As String, approverid As String, links As String)
        Try
            Dim subject As String = "Approval Request: Success Plan for " & GetEmployeeData(sempid, "fullname")
            Dim filePath As String = MailContentURL & MailFolderSuccession & "Staff_Succession_Approver_Notification.txt"
            Dim readertxt As New StreamReader(filePath)
            msgbuild.Clear()
            While Not readertxt.EndOfStream
                Dim line As String = readertxt.ReadLine()
                msgbuild.AppendLine(line)
            End While
            readertxt.Close()
            msgbuild = msgbuild.Replace("@empname", GetEmployeeData(sempid, "fullname")).Replace("@mgrname", GetEmployeeData(mgrid, "fullname")).Replace("@company", GetCompanyByEmpID(mgrid)) _
                .Replace("@oldjob", oldjobtitle).Replace("@oldgrade", oldjobgrade).Replace("@job", pjobtitle).Replace("@grade", pjobgrade).Replace("@empid", sempid).Replace("@approver", GetEmployeeData(approverid, "firstname"))


            Process.MailNotification(sempid, Process.MailSuccessionPlan, subject, msgbuild.ToString, approverid, HRTeam, "", approverid, links)

            If SendNotification() = "Yes" Then
                'Notification for requester
                Process.SendEmail("", HRTeam, GetEmailAddress(approverid), _
                                     subject, "<div style=""font-family:Arial; font-size:12px;"">" & msgbuild.ToString & " <br /> <br /><a href=" & links & ">" & links & "</a> <br /> <br />" & GetEmployeeData(mgrid, "company") & "</div>", "", True)
            End If
            '<a href=" & links & ">" & links & "</a>
        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
        End Try
    End Sub
#End Region
#Region "Promotion"
    Public Shared Sub Staff_Promotion_Complete(sempid As String, mgrid As String, attachement As String, links As String)
        Try
            Dim subject As String = "RE: Promotion Request for " & GetEmployeeData(sempid, "fullname")
            Dim filePath As String = MailContentURL & MailFolderPromotion & "Staff_Promotion_Complete.txt"
            Dim readertxt As New StreamReader(filePath)
            msgbuild.Clear()
            While Not readertxt.EndOfStream
                Dim line As String = readertxt.ReadLine()
                msgbuild.AppendLine(line)
            End While
            readertxt.Close()
            msgbuild = msgbuild.Replace("@empname", GetEmployeeData(sempid, "fullname")).Replace("@mgrname", GetEmployeeData(mgrid, "fullname")).Replace("@company", GetCompanyByEmpID(mgrid)).Replace("@empid", sempid)

            Process.MailNotification(sempid, Process.MailPromotion, subject, msgbuild.ToString, mgrid, HRTeam, "", mgrid, links, attachement)

            If SendNotification() = "Yes" Then
                'Notification for requester
                Process.SendEmail("", HRTeam, GetEmailAddress(mgrid), _
                                     subject, "<div style=""font-family:Arial; font-size:12px;"">" & msgbuild.ToString & " <br /> <br /><a href=" & links & ">" & links & "</a> <br /> <br />" & GetEmployeeData(mgrid, "company") & "</div>", attachement, True)
            End If
            '<a href=" & links & ">" & links & "</a>
        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
        End Try
    End Sub
    Public Shared Sub Staff_Promotion_Approver_HR_Reply(sindex As String, status As String, scomment As String, oldjobtitle As String, oldjobgrade As String, pjobtitle As String, pjobgrade As String, sempid As String, mgrid As String, links As String)
        Try
            Dim subject As String = "Approval " & sindex & " RE: Promotion Request for " & GetEmployeeData(sempid, "fullname")
            Dim filePath As String = MailContentURL & MailFolderPromotion & "Staff_Promotion_Approver_HR_Reply.txt"
            Dim readertxt As New StreamReader(filePath)
            msgbuild.Clear()
            While Not readertxt.EndOfStream
                Dim line As String = readertxt.ReadLine()
                msgbuild.AppendLine(line)
            End While
            readertxt.Close()
            msgbuild = msgbuild.Replace("@empname", GetEmployeeData(sempid, "fullname")).Replace("@mgrname", GetEmployeeData(mgrid, "fullname")).Replace("@company", GetCompanyByEmpID(mgrid)).Replace("@comment", scomment) _
                .Replace("@oldjob", oldjobtitle).Replace("@oldgrade", oldjobgrade).Replace("@job", pjobtitle).Replace("@grade", pjobgrade).Replace("@status", status).Replace("@empid", sempid)


            Dim Arrays() As String = Process.GetEmpIDMailList("hr").Split(SeparatorSemi, StringSplitOptions.RemoveEmptyEntries)
            For i = 0 To Arrays.Count - 1
                Process.MailNotification(sempid, Process.MailPromotion, subject, msgbuild.ToString, sempid, HRTeam, "", Arrays(i), links)
            Next

            If SendNotification() = "Yes" Then
                'Notification for requester
                Process.SendEmail("", GetEmployeeData(sempid, "company"), GetMailList("hr"), _
                                     subject, "<div style=""font-family:Arial; font-size:12px;"">" & msgbuild.ToString & " <br /> <br /><a href=" & links & ">" & links & "</a> <br /> <br />" & GetEmployeeData(sempid, "company") & "</div>", "", True)
            End If
            '<a href=" & links & ">" & links & "</a>
        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
        End Try
    End Sub
    Public Shared Sub Staff_Promotion_HR_Notification(oldjobtitle As String, oldjobgrade As String, pjobtitle As String, pjobgrade As String, sempid As String, mgrid As String, links As String)
        Try
            Dim subject As String = "Promotion Request for " & GetEmployeeData(sempid, "fullname")
            Dim filePath As String = MailContentURL & MailFolderPromotion & "Staff_Promotion_HR_Notification.txt"
            Dim readertxt As New StreamReader(filePath)
            msgbuild.Clear()
            While Not readertxt.EndOfStream
                Dim line As String = readertxt.ReadLine()
                msgbuild.AppendLine(line)
            End While
            readertxt.Close()
            msgbuild = msgbuild.Replace("@empname", GetEmployeeData(sempid, "fullname")).Replace("@mgrname", GetEmployeeData(mgrid, "fullname")).Replace("@company", GetCompanyByEmpID(mgrid)) _
                .Replace("@oldjob", oldjobtitle).Replace("@oldgrade", oldjobgrade).Replace("@job", pjobtitle).Replace("@grade", pjobgrade).Replace("@empid", sempid)


            Dim Arrays() As String = Process.GetEmpIDMailList("hr").Split(SeparatorSemi, StringSplitOptions.RemoveEmptyEntries)
            For i = 0 To Arrays.Count - 1
                Process.MailNotification(sempid, Process.MailPromotion, subject, msgbuild.ToString, sempid, HRTeam, "", Arrays(i), links)
            Next

            If SendNotification() = "Yes" Then
                'Notification for requester
                Process.SendEmail("", GetEmployeeData(mgrid, "company"), GetMailList("hr"), _
                                     subject, "<div style=""font-family:Arial; font-size:12px;"">" & msgbuild.ToString & " <br /> <br /><a href=" & links & ">" & links & "</a> <br /> <br />" & GetEmployeeData(mgrid, "company") & "</div>", "", True)
            End If
            '<a href=" & links & ">" & links & "</a>
        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
        End Try
    End Sub
    Public Shared Sub Staff_Promotion_Approver_Notification(oldjobtitle As String, oldjobgrade As String, pjobtitle As String, pjobgrade As String, sempid As String, mgrid As String, approverid As String, links As String)
        Try
            Dim subject As String = "Approval Request: Promotion for " & GetEmployeeData(sempid, "fullname")
            Dim filePath As String = MailContentURL & MailFolderPromotion & "Staff_Promotion_Approver_Notification.txt"
            Dim readertxt As New StreamReader(filePath)
            msgbuild.Clear()
            While Not readertxt.EndOfStream
                Dim line As String = readertxt.ReadLine()
                msgbuild.AppendLine(line)
            End While
            readertxt.Close()
            msgbuild = msgbuild.Replace("@empname", GetEmployeeData(sempid, "fullname")).Replace("@mgrname", GetEmployeeData(mgrid, "fullname")).Replace("@company", GetCompanyByEmpID(mgrid)) _
                .Replace("@oldjob", oldjobtitle).Replace("@oldgrade", oldjobgrade).Replace("@job", pjobtitle).Replace("@grade", pjobgrade).Replace("@empid", sempid).Replace("@approver", GetEmployeeData(approverid, "firstname"))


            Process.MailNotification(sempid, Process.MailPromotion, subject, msgbuild.ToString, approverid, HRTeam, "", approverid, links)

            If SendNotification() = "Yes" Then
                'Notification for requester
                Process.SendEmail("", HRTeam, GetEmailAddress(approverid), _
                                     subject, "<div style=""font-family:Arial; font-size:12px;"">" & msgbuild.ToString & " <br /> <br /><a href=" & links & ">" & links & "</a> <br /> <br />" & GetEmployeeData(mgrid, "company") & "</div>", "", True)
            End If
            '<a href=" & links & ">" & links & "</a>
        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
        End Try
    End Sub
#End Region
#Region "Confirmation"
    Public Shared Sub Staff_Confirmation_Notification(sempid As String, mgrid As String)
        Try
            Dim subject As String = "Employment Confirmation for " & GetEmployeeData(sempid, "fullname")
            Dim filePath As String = MailContentURL & MailFolderConfirmation & "Staff_Confirmation_Notification.txt"
            Dim readertxt As New StreamReader(filePath)
            msgbuild.Clear()
            While Not readertxt.EndOfStream
                Dim line As String = readertxt.ReadLine()
                msgbuild.AppendLine(line)
            End While
            readertxt.Close()
            msgbuild = msgbuild.Replace("@empname", GetEmployeeData(sempid, "fullname")).Replace("@mgrname", GetEmployeeData(mgrid, "fullname")).Replace("@company", GetCompanyByEmpID(mgrid)).Replace("@dept", GetEmployeeData(mgrid, "office"))


            Process.MailNotification(sempid, MailEmployeeConfirm, subject, msgbuild.ToString, sempid, Process.AppName, HRTeam, sempid, "")

            If SendNotification() = "Yes" Then
                'Notification for requester
                Process.SendEmail("", GetEmployeeData(mgrid, "company"), GetEmailAddress(sempid), _
                                     subject, "<div style=""font-family:Arial; font-size:12px;"">" & msgbuild.ToString & " <br /> <br />" & GetEmployeeData(mgrid, "company") & "</div>", "", True)
            End If
            '<a href=" & links & ">" & links & "</a>
        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
        End Try
    End Sub
    Public Shared Sub Staff_Confirmation_HR_Notification(sempid As String, mgrid As String, links As String)
        Try
            Dim subject As String = "Employment Confirmation for " & GetEmployeeData(sempid, "fullname")
            Dim filePath As String = MailContentURL & MailFolderConfirmation & "Staff_Confirmation_HR_Notification.txt"
            Dim readertxt As New StreamReader(filePath)
            msgbuild.Clear()
            While Not readertxt.EndOfStream
                Dim line As String = readertxt.ReadLine()
                msgbuild.AppendLine(line)
            End While
            readertxt.Close()
            msgbuild = msgbuild.Replace("@empname", GetEmployeeData(sempid, "fullname")).Replace("@mgrname", GetEmployeeData(mgrid, "fullname")).Replace("@company", GetCompanyByEmpID(mgrid)).Replace("@dept", GetEmployeeData(mgrid, "office"))

            Dim Arrays() As String = Process.GetEmpIDMailList("hr").Split(SeparatorSemi, StringSplitOptions.RemoveEmptyEntries)
            For i = 0 To Arrays.Count - 1
                Process.MailNotification(sempid, Process.MailEmployeeConfirm, subject, msgbuild.ToString, sempid, HRTeam, "", Arrays(i), links)
            Next

            If SendNotification() = "Yes" Then
                'Notification for requester
                Process.SendEmail("", GetEmployeeData(mgrid, "company"), GetMailList("hr"), _
                                     subject, "<div style=""font-family:Arial; font-size:12px;"">" & msgbuild.ToString & " <br /> <br /><a href=" & links & ">" & links & "</a> <br /> <br />" & GetEmployeeData(mgrid, "company") & "</div>", "", True)
            End If
            '<a href=" & links & ">" & links & "</a>
        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
        End Try
    End Sub
#End Region
#Region "Recruitment"
    Public Shared Function Welcome_Recruit(ByVal scompany As String, ByVal receiver As String, ByVal staffid As String, ByVal staffname As String) As Boolean
        Try
            Dim subject As String = "You are welcome"
            Dim filePath As String = MailContentURL & MailFolderRecruitment & "Welcome_Recruit.txt"
            Dim readertxt As New StreamReader(filePath)
            msgbuild.Clear()
            While Not readertxt.EndOfStream
                Dim line As String = readertxt.ReadLine()
                msgbuild.AppendLine(line)
            End While
            readertxt.Close()
            msgbuild = msgbuild.Replace("@empname", GetEmployeeData(staffid, "fullname")).Replace("@empid", staffid).Replace("@company", scompany)



            Return SendEmail("", scompany, receiver,
                                   subject,
                                   "<div style=""font-family:Arial; font-size:12px;"">" & msgbuild.ToString.Replace(vbCrLf, "<br />") & "</div>",
                                   "", True)
        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
            Return False
        End Try
    End Function
    Public Shared Function Verify_Recruit(ByVal scompany As String, ByVal receiver As String, ByVal recruitName As String, ByVal Link As String) As Boolean
        Try
            Dim subject As String = "Comfirm Your Email"
            Dim filePath As String = MailContentURL & MailFolderRecruitment & "Verify_Recruit.txt"
            Dim readertxt As New StreamReader(filePath)
            msgbuild.Clear()
            While Not readertxt.EndOfStream
                Dim line As String = readertxt.ReadLine()
                msgbuild.AppendLine(line)
            End While
            readertxt.Close()
            msgbuild = msgbuild.Replace("@empname", recruitName).Replace("@Link", Link).Replace("@company", scompany)



            Return SendEmail("", scompany, receiver,
                                   subject,
                                   "<div style=""font-family:Arial; font-size:12px;"">" & msgbuild.ToString.Replace(vbCrLf, "<br />") & "</div>",
                                   "", True)
        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
            Return False
        End Try
    End Function
    Public Shared Function Applicant_New_Profile(ByVal receiver As String, ByVal applicant As String, emailaddress As String, pwd As String) As Boolean
        Try
            Dim url As String = Process.ApplicationURL()
            Dim TestURL As String = ConfigurationManager.AppSettings("TestURL")
            Dim links As String = url & TestURL.Replace("?id=", "")

            Dim subject As String = Process.GetCompanyName & " Recruitment Profile"


            Return Process.SendEmail("", Process.GetCompanyName, receiver, _
                                    subject, _
                                      "<div style=""font-family:Arial; font-size:12px;"">Dear " & applicant & ", <br /> <br /> Your " & Process.GetCompanyName & " recruitment profile is as follows" _
                                      & "<br /> <br /> Username:  " & emailaddress _
                                      & "<br />  Password:  " & pwd _
                                      & "<br /> <br /><a href=" & links & ">" & links & "</a><br /> Best regards <br /><br /> HR Team,<br /> " & Process.GetCompanyName & " </div>", _
                                      "", True)
        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
            Return False
        End Try
    End Function
    Public Shared Function Employee_Recovery(emailaddress As String) As Boolean
        Try
            Dim url As String = Process.ApplicationURL()
            Dim TestURL As String = ConfigurationManager.AppSettings("TestURL")
            Dim res As String = "/"
            Dim links As String = url & res

            Dim result As String = ""
            Dim applicant As String = ""
            Dim pwd As String = ""
            Dim userid As String = ""
            Dim strUser As New DataSet
            strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Employee_Password_Recovery", emailaddress)
            If strUser.Tables(0).Rows.Count > 0 Then
                applicant = strUser.Tables(0).Rows(0).Item("name").ToString
                pwd = Decrypt(strUser.Tables(0).Rows(0).Item("password").ToString)
                userid = strUser.Tables(0).Rows(0).Item("userid").ToString
            Else
                Return False
                Exit Function
            End If

            Return Process.SendEmail("", Process.GetCompanyName, emailaddress, _
                                      "GOSHRM Profile Recovery", _
                                      "<div style=""font-family:Arial; font-size:12px;"">Dear " & applicant & ", <br /> <br /> Your GOSHRM profile is as follows" _
                                      & "<br /> <br /> Username:  " & userid _
                                      & "<br />  Password:  " & pwd _
                                      & "<br /> <br /> <a href=" & links & ">" & "Login" & "</a></div>", _
                                      "", True)

        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
            Return False
        End Try
    End Function
    Public Shared Function Applicant_Recovery(emailaddress As String) As Boolean
        Try
            Dim url As String = Process.ApplicationURL()
            Dim TestURL As String = ConfigurationManager.AppSettings("TestURL")
            Dim links As String = url & TestURL.Replace("?id=", "")

            Dim result As String = ""
            Dim applicant As String = ""
            Dim pwd As String = ""
            Dim strUser As New DataSet
            strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Applicant_Password_Recovery", emailaddress)
            If strUser.Tables(0).Rows.Count > 0 Then
                applicant = strUser.Tables(0).Rows(0).Item("applicant").ToString
                pwd = Decrypt(strUser.Tables(0).Rows(0).Item("Pwd").ToString)
            Else
                Return False
                Exit Function
            End If

            Return Process.SendEmail("", Process.GetCompanyName, emailaddress, _
                                    Process.GetCompanyName & " Recruitment Profile Recovery", _
                                      "<div style=""font-family:Arial; font-size:12px;"">Dear " & applicant & ", <br /> <br /> Your " & Process.GetCompanyName & " recruitment profile is as follows" _
                                      & "<br /> <br /> Username:  " & emailaddress _
                                      & "<br />  Password:  " & pwd _
                                      & "<br /> <br /> <a href=" & links & ">" & links & "</a></div>", _
                                      "", True)

        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
            Return False
        End Try
    End Function
    Public Shared Function Recruit_Interviewee_Invite(ByVal receiver As String, ByVal scompany As String, ByVal jobpost As String, ByVal applicant As String, interviewdate As Date, interviewtime As String, venue As String) As Boolean
        Try
            Return Process.SendEmail("", scompany, receiver, _
                                    "Job Interview for " & jobpost & " at " & scompany, _
                                      "<div style=""font-family:Arial; font-size:12px;"">Dear " & applicant & ", <br /> <br /> You have been shortlisted for an interview for the position of " & jobpost & " at " & scompany _
                                      & "<br /> <br /> Here are the details:  " & "" _
                                      & "<br /> <br /> <br /> <table> <tr><td style=""width:150px"">Job Post: </td> <td style=""width:42px""> </td><td style=""width:400px"">" & jobpost _
                                      & "</td> </tr> <tr><td style=""width:150px"">Interview Date: </td> <td style=""width:42px""> </td><td style=""width:400px"">" & interviewdate.ToLongDateString & _
                                      "</td> </tr>	<tr><td style=""width:150px"">Interview Time: </td> <td style=""width:42px""> </td><td style=""width:400px"">" & interviewtime _
                                      & "</td> </tr> <tr><td style=""width:150px"">Venue: </td> <td style=""width:42px""> </td><td style=""width:400px""><br /> <br /><br />" & venue _
                                      & "<br /> Best regards <br /><br /> HR Department,<br /> " & scompany & " </div>", _
                                      "", True)
        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
            Return False
        End Try
    End Function
    Public Shared Function Recruit_Test_Invite(ByVal receiver As String, ByVal scompany As String, ByVal jobpost As String, ByVal applicant As String, interviewdate As Date, interviewtime As String, venue As String) As Boolean
        Try
            Return Process.SendEmail("", scompany, receiver, _
                                    "Job Test for " & jobpost & " at " & scompany, _
                                      "<div style=""font-family:Arial; font-size:12px;"">Dear " & applicant & ", <br /> <br /> You have been shortlisted for Job Test for the position of " & jobpost & " at " & scompany _
                                      & "<br /> <br /> Here are the details:  " & "" _
                                      & "<br /> <br /> <br /> <table> <tr><td style=""width:150px"">Job Title: </td> <td style=""width:42px""> </td><td style=""width:400px"">" & jobpost _
                                      & "</td> </tr> <tr><td style=""width:150px"">Date: </td> <td style=""width:42px""> </td><td style=""width:400px"">" & interviewdate.ToLongDateString & _
                                      "</td> </tr>	<tr><td style=""width:150px"">Time: </td> <td style=""width:42px""> </td><td style=""width:400px"">" & interviewtime _
                                      & "</td> </tr> <tr><td style=""width:150px"">Venue: </td> <td style=""width:42px""> </td><td style=""width:400px"">" & venue _
                                      & "<br /> <br /><br /> Best regards <br /><br /> HR Department,<br /> " & scompany & " </div>", _
                                      "", True)
        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
            Return False
        End Try
    End Function

    Public Shared Function Recruit_Interviewer_Invite(company As String, ByVal receiver As String, ByVal jobpost As String, ByVal interviewer As String, interviewdate As Date, interviewtime As String, venue As String, sempid As String, links As String) As Boolean
        Try
            Dim subject As String = "Job Interview for " & jobpost & " at " & company
            Dim filePath As String = MailContentURL & MailFolderRecruitment & "Recruit_Interviewer_Invite.txt"
            Dim readertxt As New StreamReader(filePath)
            msgbuild.Clear()
            While Not readertxt.EndOfStream
                Dim line As String = readertxt.ReadLine()
                msgbuild.AppendLine(line)
            End While
            readertxt.Close()
            msgbuild = msgbuild.Replace("@empname", GetEmployeeData(sempid, "fullname")).Replace("@empid", sempid).Replace("@company", company).Replace("@jobpost", jobpost) _
                .Replace("@interviewdate", DDMONYYYY(interviewdate)).Replace("@interviewtime", interviewtime).Replace("@venue", venue)


            Process.MailNotification("0", MailRecruitment, subject, msgbuild.ToString, HRTeam, sempid, HRTeam, sempid, links)

            If SendNotification() = "Yes" Then
                Return Process.SendEmail("", company, receiver, _
                                   subject, _
                                     "<div style=""font-family:Arial; font-size:12px;"">" & msgbuild.ToString & ", <br /> <br />" _
                                     & "<br /> <a href=" & links & ">" & links & "</a> <br /><br /> Best regards <br /><br /> HR Department,<br /> " & company & " </div>", _
                                     "", True)
            Else
                Return True
            End If

        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
            Return False
        End Try
    End Function
    Public Shared Sub Applicant_Notification(ByVal scompany As String, ByVal receiver As String, ByVal jobpost As String, ByVal applicant As String)
        Try

            Dim subject As String = "Application for " & jobpost & " at " & scompany
            Dim filePath As String = MailContentURL & MailFolderRecruitment & "Applicant_Notification.txt"
            Dim readertxt As New StreamReader(filePath)
            msgbuild.Clear()
            While Not readertxt.EndOfStream
                Dim line As String = readertxt.ReadLine()
                msgbuild.AppendLine(line)
            End While
            readertxt.Close()
            msgbuild = msgbuild.Replace("@applicant", applicant).Replace("@company", scompany)

            Process.SendEmail("", scompany, receiver, _
                                subject, _
                                "<div style=""font-family:Arial; font-size:12px;"">" & msgbuild.ToString & " </div>", _
                                "", True)
        Catch ex As Exception
        End Try
    End Sub
    Public Shared Function Candidate_Medical_Info_Notification(scompany As String, ByVal receiver As String, ByVal jobpost As String, ByVal applicant As String, loginurl As String) As Boolean
        Try
            Return Process.SendEmail("", scompany, receiver, _
                            "Medical Information requirement for " & jobpost, _
                            "<div style=""font-family:Arial; font-size:12px;"">Hello " & applicant _
                            & ", <br /> <br />In continuation with your application for the position of " & jobpost & " position at " & scompany & "." & "" _
                             & "<br /> <br />Kindly populate the <a href=" & loginurl & ">Medical Information Form</a> with the following details:" _
                             & "<br /> <br /> - Medical" _
                             & "<br /> <br /> Also kindly forward your most recent payslips (three, if available to this email)" _
                            & "<br /> <br /><br /> Thank you" _
                            & "<br /><br />" & scompany & " Team </div>", _
                            "", True)
        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
            Return False
        End Try
    End Function
    Public Shared Function Candidate_BankAccount_Info_Notification(ByVal scompany As String, ByVal receiver As String, ByVal jobpost As String, ByVal applicant As String, loginurl As String) As Boolean
        Try
            Return Process.SendEmail("", scompany, receiver, _
                            "Bank Account Information requirement for " & jobpost, _
                            "<div style=""font-family:Arial; font-size:12px;"">Hello " & applicant _
                            & ", <br /> <br />In continuation with your application for the position of " & jobpost & " position at " & scompany & "." & "" _
                             & "<br /> <br />Kindly populate the <a href=" & loginurl & ">Account Information Form</a> with the following details:" _
                             & "<br /> <br /> - Bank Account" _
                             & "<br /> <br /> - Retirement Savings Account" _
                             & "<br /> <br /> - National Housing Fund" _
                            & "<br /> <br /><br /> Thank you" _
                            & "<br /><br />" & scompany & " Team </div>", _
                            "", True)
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Shared Sub OnlineTest_Unsuccessful_Criteria(ByVal receiver As String, sCompany As String, ByVal jobpost As String, ByVal applicant As String, edulevel As String, explevel As String, _
                                                       startage As Integer, endage As Integer, expstart As Integer, expend As Integer)
        Try
            Process.SendEmail("", sCompany, receiver, _
                                "Application Status for " & jobpost & " at " & sCompany, _
                               "<div style=""font-family:Arial; font-size:12px;"">Hi " & applicant & ", <br /> <br />Thank you for taking the time to apply for the " & jobpost & " position at " & sCompany & "." _
                                & "<br /> <br />" _
                                & "The application process has been extremely competitive, and we greatly appreciate your contribution to that competition. After accessing your credentials, we are sorry to inform you that you do not meet the minimum requirement for the job which is as follow:" _
                                & "<br /> Minimum Education: " & edulevel _
                                & "<br /> Area of Specialisation: " & explevel _
                                & "<br /> Age Range: " & startage & " and " & endage _
                                & "<br /> Experience Range: " & expstart & " and " & expend & " years" _
                                & "<br /> <br />We wish you the best in your professional endeavors!" _
                               & "<br /> <br /><br /> Sincerely, <br /><br /> HR Department,<br /> " & sCompany & " Team</div>", _
                               "", True)
        Catch ex As Exception
        End Try
    End Sub
    Public Shared Sub OnlineTest_Notification(ByVal receiver As String, ByVal sCompany As String, ByVal jobpost As String, ByVal applicant As String, password As String, loginurl As String)
        Try
            Process.SendEmail("", sCompany, receiver, _
                             sCompany & " Online Challenge", _
                             "<div style=""font-family:Arial; font-size:12px;"">Hi " & applicant _
                             & ", <br /> <br />It's time for your first custom assessment screen for the " & jobpost & " position at " & sCompany & "." & "" _
                             & "<br /> <br /><br /> Click on the link below to begin: " _
                             & "<br /> <br /><a href=" & loginurl & ">Test Login</a>" _
                             & "<br /><br /> Good luck!,<br /> " & sCompany & " Team </div>", _
                             "", True)
        Catch ex As Exception
        End Try
    End Sub
    Public Shared Sub OnlineTest_First_Result(ByVal currentStage As Integer, sCompany As String, ByVal isPass As Boolean, ByVal receiver As String, ByVal jobpost As String, ByVal applicant As String, ByVal score As String, ByVal Passmark As String, loginurl As String, Optional stageno As Integer = 1)
        Try
            If isPass = True Then
                Process.SendEmail("", sCompany, receiver, _
                          "Stage " & currentStage.ToString & " Assessment outcome for the " & jobpost & " role at " & sCompany, _
                          "<div style=""font-family:Arial; font-size:12px;"">Hi " & applicant & ", <br /> <br />Congratulations. We have evaluated your answers and you have been selected to move forward in the process for the position of " & jobpost & "." _
                          & "<br /> <br />" _
                          & "You can assess the Next Stage by clicking the link below using same credentials" _
                          & "<br /><a href=" & loginurl & ">Test Stage " & stageno.ToString & "</a>" _
                          & "<br /> <br /><br /> Thanks <br /><br /> HR Department,<br /> " & sCompany & " </div>", _
                          "", True)

            Else
                Process.SendEmail("", sCompany, receiver, _
                               "Stage " & currentStage.ToString & " Assessment outcome for the " & jobpost & " role at " & sCompany, _
                               "<div style=""font-family:Arial; font-size:12px;"">Hi " & applicant & ", <br /> <br />Thank you for taking the time to apply for the " & jobpost & " position at " & sCompany & "." _
                                & "<br /> <br />" _
                                & "The application process has been extremely competitive, and we greatly appreciate your contribution to that competition.After grading your first assessment, we are sorry to inform you that your score did not meet our minimum passing requirement." _
                                & "<br /> <br />Your score was " & score & " which was below the passing score " & Passmark & "." _
                                & "<br /> <br />Although we are unable to continue with your application, we want to thank you for your interest in " & sCompany & " and for the valuable time and effort you have invested in this process." _
                                & "<br /> <br />We wish you the best in your professional endeavors!" _
                               & "<br /> <br /><br /> Sincerely, <br /><br /> HR Department,<br /> " & sCompany & " Team</div>", _
                               "", True)
            End If
            'Your score was 55.0 which was below the passing score (60) .
        Catch ex As Exception
        End Try
    End Sub
    Public Shared Sub OnlineTest_Last_Result(company As String, ByVal isPass As Boolean, ByVal receiver As String, ByVal jobpost As String, ByVal applicant As String, ByVal score As String, ByVal Passmark As String)
        Try
            If isPass = True Then

                Process.SendEmail("", company, receiver, _
                                              "Final Assessment outcome for the " & jobpost & " role at " & company, _
                                              "<div style=""font-family:Arial; font-size:12px;"">Hi " & applicant & ", <br /> <br />Congratulations. We have evaluated your answers and you have been selected to move forward in the process for the position of " & jobpost & "." _
                                              & "<br /> <br />" _
                                              & "You will soon be contacted with regards the next recruitment step." _
                                              & "<br /> <br /><br /> Thanks <br /><br /> HR Department,<br /> " & company & " </div>", _
                                              "", True)
            Else
                Process.SendEmail("", company, receiver, _
                               "Final Assessment outcome for the " & jobpost & " role at " & company, _
                               "<div style=""font-family:Arial; font-size:12px;"">Hi " & applicant & ", <br /> <br />Thank you for taking the time to apply for the " & jobpost & " position at " & company & "." _
                                & "<br /> <br />" _
                                & "The application process has been extremely competitive, and we greatly appreciate your contribution to that competition.After grading your first assessment, we are sorry to inform you that your score did not meet our minimum passing requirement." _
                                & "<br /> <br />Your score was " & score & " which was below the passing score " & Passmark & "." _
                                & "<br /> <br />Although we are unable to continue with your application, we want to thank you for your interest in " & company & " and for the valuable time and effort you have invested in this process." _
                                & "<br /> <br />We wish you the best in your professional endeavors!" _
                               & "<br /> <br /><br /> Sincerely, <br /><br /> HR Department,<br /> " & company & " Team</div>", _
                               "", True)
            End If
            'Your score was 55.0 which was below the passing score (60) .
        Catch ex As Exception
        End Try
    End Sub

    Public Shared Function Candidate_Medical_Account_Complete(stype As String, ByVal scompany As String, ByVal applicant As String, ByVal jobpost As String, ByVal links As String) As Boolean
        Try
            Dim line1 As String = applicant & " has completed the " & stype & " information form for the position of " & jobpost & " at " & scompany & ", kindly proceed to review and make your recommendation"
            Dim line2 As String = "Thank you"
            Dim line3 As String = scompany & " Team"
            Dim subject As String = applicant & " " & stype & " Information  for the position of " & jobpost & " at " & scompany & " has been completed"
            msgbuild.Clear()
            msgbuild.AppendLine(line1)
            msgbuild.AppendLine(" ")
            msgbuild.AppendLine(line2)
            msgbuild.AppendLine(" ")
            msgbuild.AppendLine(line3)


            Dim Arrays() As String = Process.GetEmpIDMailList("hr").Split(SeparatorSemi, StringSplitOptions.RemoveEmptyEntries)
            For i = 0 To Arrays.Count - 1
                Process.MailNotification("0", MailRecruitment, subject, msgbuild.ToString, HRTeam, Process.AppName, "", Arrays(i), links)
            Next


            Return Process.SendEmail("", Process.AppName, GetMailList("hr"), _
                                     subject, _
                                       "<div style=""font-family:Arial; font-size:12px;"">" & _
                                       ", <br /> <br /> " & line1 _
                                       & "<br /> <br /> <a href=" & links & ">" & links & "</a> <br /> <br />" & line2 _
                                       & "<br /> <br /> " & line3 _
                      & "<br /><br /> " & Process.AppName, "", True, "")

        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
            Return False
        End Try
    End Function

#End Region
#Region "Leave Notification"
    Public Shared Sub Leave_Notification_Final(ByVal ref As String, ByVal EmpName As String, leavetype As String, startdate As Date, enddate As Date, ByVal Desc As String, status As String, authoriser As String, status2 As String, authoriser2 As String, sempid As String, mgrid As String, attachment As String, links As String, links2 As String)
        Try
            Dim subject As String = "Leave " & ref & " " & status2 & " by HR"

            Dim filePath As String = MailContentURL & MailFolderLeave & "Leave_Notification_Final.txt"
            Dim readertxt As New StreamReader(filePath)
            msgbuild.Clear()
            While Not readertxt.EndOfStream
                Dim line As String = readertxt.ReadLine()
                msgbuild.AppendLine(line)
            End While
            readertxt.Close()

            msgbuild = msgbuild.Replace("@empname", GetEmployeeData(sempid, "firstname")).Replace("@ref", ref).Replace("@company", GetCompanyByEmpID(sempid)).Replace("@empid", sempid) _
                .Replace("@startdate", Process.DDMONYYYY(startdate)).Replace("@enddate", Process.DDMONYYYY(enddate)).Replace("@status", status).Replace("@approver", authoriser) _
                .Replace("@leavetype", leavetype)


            Process.MailNotification(ref, Process.MailLeave, subject, msgbuild.ToString, sempid, HRTeam, "", sempid, links, attachment)

            If SendNotification() = "Yes" Then
                'Final Notification of Requester
                Process.SendEmail("", Process.AppName, GetEmailAddress(sempid), _
                           subject, _
                           "<div style=""font-family:Arial; font-size:12px;"">" & msgbuild.ToString & " </div> <br /> <br /> <a href=" & links & ">" & links & "</a>", attachment, True, "")
            End If
        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
        End Try
    End Sub
    '    Public Shared Sub Leave_HR_Level2_Approval(ByVal receiver As String, ByVal ref As String, ByVal EmpName As String, leavetype As String, startdate As Date, enddate As Date, ByVal Desc As String, status As String, authoriser As String,
    'sempid As String, mgrid As String)
    '        Try
    '            Dim subject As String = "RE: HR Approval on Leave Application " & ref & " has been " & status
    '            Dim filePath As String = MailContentURL & MailFolderLeave & "Leave_HR_Level2_Approval.txt"
    '            Dim readertxt As New StreamReader(filePath)
    '            msgbuild.Clear()
    '            While Not readertxt.EndOfStream
    '                Dim line As String = readertxt.ReadLine()
    '                msgbuild.AppendLine(line)
    '            End While
    '            readertxt.Close()

    '            msgbuild = msgbuild.Replace("@empname", GetEmployeeData(sempid, "fullname")).Replace("@ref", ref).Replace("@company", GetCompanyByEmpID(sempid)).Replace("@empid", sempid) _
    '                .Replace("@startdate", Process.DDMONYYYY(startdate)).Replace("@enddate", Process.DDMONYYYY(enddate)).Replace("@status", status).Replace("@approver", authoriser) _
    '                .Replace("@leavetype", leavetype)
    '            msgbuild.Clear()
    '            msgbuild.AppendLine("Here are the request details:")
    '            msgbuild.AppendLine(" ")
    '            msgbuild.AppendLine("Request By       : " & EmpName)
    '            msgbuild.AppendLine(" ")
    '            msgbuild.AppendLine("Leave Type       : " & leavetype)
    '            msgbuild.AppendLine(" ")
    '            msgbuild.AppendLine("Leave Period     : " & Process.DDMONYYYY(startdate) & " : " & Process.DDMONYYYY(enddate))
    '            msgbuild.AppendLine(" ")
    '            msgbuild.AppendLine("Manager Approval : " & status)
    '            msgbuild.AppendLine(" ")
    '            Process.MailNotification(ref, Process.MailLeave, "RE: HR Approval on Leave Application " & ref & " has been " & status, msgbuild.ToString, sempid, HRTeam, "", mgrid, "")
    '            Process.MailNotification(ref, Process.MailLeave, "RE: HR Approval on Leave Application " & ref & " has been " & status, msgbuild.ToString, sempid, HRTeam, "", sempid, "")
    '            Dim Arrays() As String = Process.GetEmpIDMailList("hr").Split(SeparatorSemi, StringSplitOptions.RemoveEmptyEntries)
    '            For i = 0 To Arrays.Count - 1
    '                Process.MailNotification(ref, Process.MailLeave, "RE: HR Approval on Leave Application " & ref & " has been " & status, msgbuild.ToString, sempid, HRTeam, HRTeam, Arrays(i), "")
    '            Next

    '            If SendNotification() = "Yes" Then
    '                'Final Notification of Requester
    '                Process.SendEmail("", Process.AppName, receiver, _
    '                       "RE: HR Approval on Leave Application " & ref & " has been " & status, _
    '                       "<div style=""font-family:Arial; font-size:12px;"">Here are the details:  " & "" _
    '                       & "<br /> <br /> <br /> <table> <tr><td style=""width:50px"">Request By: </td> <td style=""width:42px""> </td><td style=""width:50px"">" & EmpName _
    '                    & "</td> </tr>	<tr><td style=""width:150px"">Leave Type: </td> <td style=""width:42px""> </td><td style=""width:250px"">" & leavetype _
    '                   & "</td> </tr> <tr><td style=""width:100px"">Leave From: </td> <td style=""width:42px""> </td><td style=""width:250px"">" & startdate.ToLongDateString _
    '                                 & "</td> </tr>	<tr><td style=""width:100px"">Leave To: </td> <td style=""width:42px""> </td><td style=""width:250px"">" & enddate.ToLongDateString _
    '                    & "</td> </tr> <tr><td style=""width:150px"">HR Approval Status: </td> <td style=""width:42px""> </td><td style=""width:250px"">" & status _
    '                       & " by " & authoriser & "</td> </tr> </table> </div> <br /> <br /> ", _
    '                       "", True)
    '            End If

    '            'Notification of Final Approval to HR Unit          
    '        Catch ex As Exception
    '            HttpContext.Current.Session.Item("exception") = ex.Message
    '        End Try
    '    End Sub
    Public Shared Sub Leave_Approver_Approvals(ByVal receiver As String, ByVal ref As String, ByVal EmpName As String, leavetype As String, startdate As Date, enddate As Date, ByVal Desc As String, status As String, authoriser As String, comment As String,
sempid As String, mgrid As String, links As String)

        Try
            Dim subject As String = "Leave " & ref & " " & status & " by " & authoriser
            Dim filePath As String = MailContentURL & MailFolderLeave & "Leave_Approver_Approvals.txt"
            Dim readertxt As New StreamReader(filePath)
            msgbuild.Clear()
            While Not readertxt.EndOfStream
                Dim line As String = readertxt.ReadLine()
                msgbuild.AppendLine(line)
            End While
            readertxt.Close()

            msgbuild = msgbuild.Replace("@empname", GetEmployeeData(sempid, "fullname")).Replace("@ref", ref).Replace("@company", GetCompanyByEmpID(sempid)).Replace("@empid", sempid) _
                .Replace("@startdate", Process.DDMONYYYY(startdate)).Replace("@enddate", Process.DDMONYYYY(enddate)).Replace("@status", status).Replace("@approver", authoriser) _
                .Replace("@leavetype", leavetype).Replace("@comment", comment)


            Dim Arrays() As String = Process.GetEmpIDMailList("hr").Split(SeparatorSemi, StringSplitOptions.RemoveEmptyEntries)
            For i = 0 To Arrays.Count - 1
                Process.MailNotification(ref, Process.MailLeave, subject, msgbuild.ToString, HRTeam, mgrid, "", Arrays(i), links)
            Next

            If SendNotification() = "Yes" Then
                Process.SendEmail("", Process.AppName, receiver, _
                   subject, _
                   "<div style=""font-family:Arial; font-size:12px;"">" & msgbuild.ToString & "</div> <br /> <br /> <a href=" & links & ">" & links & "</a>", _
                   "", True)
            End If
        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
        End Try
    End Sub
    Public Shared Sub Leave_Notification_(ByVal loanref As String, sempid As String, links As String)
        Try
            Dim subject As String = "Leave request with reference " & loanref & " has been forwarded for approval"
            Dim filePath As String = MailContentURL & MailFolderLeave & "Leave_Notification.txt"
            Dim readertxt As New StreamReader(filePath)
            msgbuild.Clear()
            While Not readertxt.EndOfStream
                Dim line As String = readertxt.ReadLine()
                msgbuild.AppendLine(line)
            End While
            readertxt.Close()

            msgbuild = msgbuild.Replace("@empname", GetEmployeeData(sempid, "firstname")).Replace("@ref", loanref).Replace("@company", GetCompanyByEmpID(sempid))

            Process.MailNotification(loanref, MailLeave, subject, msgbuild.ToString, sempid, GetCompanyByEmpID(sempid), "", sempid, links)

            If SendNotification() = "Yes" Then
                Process.SendEmail("", GetCompanyByEmpID(sempid), GetEmailAddress(sempid), _
                                     subject, _
                                     "<div style=""font-family:Arial; font-size:12px;"">" & msgbuild.ToString & "<br /> <br /> <a href=" & links & ">" & links & "</a></div>", _
                                     "", True)
            End If
            '<a href=" & links & ">" & links & "</a>
        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
        End Try
    End Sub

    Public Shared Sub Employee_Comments_Notification_(ByVal sempid As String, links As String)
        Try
            Dim subject As String = "Appraisal Final Comment"
            Dim filePath As String = MailContentURL & MailFolderLeave & "Employee_Comments_Notification.txt"
            Dim readertxt As New StreamReader(filePath)
            msgbuild.Clear()
            While Not readertxt.EndOfStream
                Dim line As String = readertxt.ReadLine()
                msgbuild.AppendLine(line)
            End While
            readertxt.Close()

            msgbuild = msgbuild.Replace("@empname", GetEmployeeData(sempid, "firstname")).Replace("@ref", "").Replace("@company", GetCompanyByEmpID(sempid))

            Process.MailNotification("000000", MailLeave, subject, msgbuild.ToString, sempid, GetCompanyByEmpID(sempid), "", sempid, links)

            If SendNotification() = "Yes" Then
                Process.SendEmail("", GetCompanyByEmpID(sempid), GetEmailAddress(sempid), _
                                     subject, _
                                     "<div style=""font-family:Arial; font-size:12px;"">" & msgbuild.ToString & "<br /> <br /> <a href=" & links & ">" & links & "</a></div>", _
                                     "", True)
            End If
            '<a href=" & links & ">" & links & "</a>
        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
        End Try
    End Sub
    Public Shared Sub Leave_Notification_Supervisor(ByVal receiver As String, ByVal refnum As String, ByVal empname As String, ByVal leavetype As String, ByVal startdate As Date, ByVal enddate As Date, ByVal reason As String, eempid As String, mgrid As String, links1 As String, links2 As String)
        Try
            Dim subject As String = "Leave Request from " & empname & " for your approval "
            Dim filePath As String = MailContentURL & MailFolderLeave & "Leave_Notification_Supervisor.txt"
            Dim readertxt As New StreamReader(filePath)
            msgbuild.Clear()
            While Not readertxt.EndOfStream
                Dim line As String = readertxt.ReadLine()
                msgbuild.AppendLine(line)
            End While
            readertxt.Close()

            msgbuild = msgbuild.Replace("@empname", GetEmployeeData(eempid, "fullname")).Replace("@ref", refnum).Replace("@company", GetCompanyByEmpID(eempid)).Replace("@empid", eempid) _
               .Replace("@startdate", Process.DDMONYYYY(startdate)).Replace("@enddate", Process.DDMONYYYY(enddate)) _
               .Replace("@leavetype", leavetype).Replace("@reason", reason).Replace("@dept", GetEmployeeData(eempid, "office"))

            Process.MailNotification(refnum, MailLeave, subject, msgbuild.ToString, mgrid, eempid, HRTeam, mgrid, links2)

            Dim Arrays() As String = Process.GetEmpIDMailList("hr").Split(SeparatorSemi, StringSplitOptions.RemoveEmptyEntries)
            For i = 0 To Arrays.Count - 1
                Process.MailNotification(refnum, 1, subject, msgbuild.ToString, mgrid, eempid, Process.GetEmpIDMailList("hr"), Arrays(i), links1)
            Next


            If SendNotification() = "Yes" Then
                'Leave notification for supervisor to approve
                Process.SendEmail("", Process.AppName, receiver, _
                                    subject, _
                                     "<div style=""font-family:Arial; font-size:12px;"">" & msgbuild.ToString & "<br /> <br /> <a href=" & links1 & ">" & links1 & "</a></div>", _
                                     GetMailList("hr"), True)
            End If
        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
        End Try
    End Sub

    Public Shared Sub Employee_Comments_Notification_Supervisor(ByVal receiver As String, ByVal empname As String, ByVal reason As String, eempid As String, mgrid As String, links1 As String, links2 As String)
        Try
            Dim subject As String = "Appraisal Final Comment "
            Dim filePath As String = MailContentURL & MailFolderLeave & "Employee_Comments_Notification_Supervisor.txt"
            Dim readertxt As New StreamReader(filePath)
            msgbuild.Clear()
            While Not readertxt.EndOfStream
                Dim line As String = readertxt.ReadLine()
                msgbuild.AppendLine(line)
            End While
            readertxt.Close()

            msgbuild = msgbuild.Replace("@empname", GetEmployeeData(eempid, "fullname")).Replace("@company", GetCompanyByEmpID(eempid)).Replace("@empid", eempid) _
               .Replace("@reason", reason).Replace("@dept", GetEmployeeData(eempid, "office"))

            Process.MailNotification("000000", MailLeave, subject, msgbuild.ToString, mgrid, eempid, HRTeam, mgrid, links2)

            Dim Arrays() As String = Process.GetEmpIDMailList("hr").Split(SeparatorSemi, StringSplitOptions.RemoveEmptyEntries)
            For i = 0 To Arrays.Count - 1
                Process.MailNotification("000000", 1, subject, msgbuild.ToString, mgrid, eempid, Process.GetEmpIDMailList("hr"), Arrays(i), links1)
            Next


            If SendNotification() = "Yes" Then
                'Leave notification for supervisor to approve
                Process.SendEmail("", Process.AppName, GetEmailAddress(mgrid), _
                                    subject, _
                                     "<div style=""font-family:Arial; font-size:12px;"">" & msgbuild.ToString & "<br /> <br /> <a href=" & links1 & ">" & links1 & "</a></div>", _
                                     GetMailList("hr"), True)
            End If
        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
        End Try
    End Sub
    'LOAN Notification
#End Region
#Region "Loan Notification"



    'Public Shared Sub Loan_Finance_Level2_Approval(ByVal receiver As String, ByVal loanref As String, ByVal EmpName As String, ByVal loantype As String, ByVal Amount As Double, ByVal Repayment As Double, sStartDate As Date, ByVal Desc As String, status As String, authoriser As String, ByVal intrate As Double)
    '    Try
    '        msgbuild.Clear()
    '        msgbuild.AppendLine("Here are the details:")
    '        msgbuild.AppendLine(" ")
    '        msgbuild.AppendLine("Loan Type     : " & loantype)
    '        msgbuild.AppendLine(" ")
    '        msgbuild.AppendLine("Amount         : " & Amount.ToString("#,##0.00"))
    '        msgbuild.AppendLine(" ")
    '        msgbuild.AppendLine("Monthly Payment: " & Repayment.ToString("#,##0.00"))
    '        msgbuild.AppendLine(" ")
    '        msgbuild.AppendLine("Start Date     : " & sStartDate.ToLongDateString)
    '        msgbuild.AppendLine(" ")
    '        msgbuild.AppendLine("Approval       : " & status & " " & authoriser)

    '        Dim Arrays() As String = Process.GetEmpIDMailList("finance").Split(SeparatorSemi, StringSplitOptions.RemoveEmptyEntries)
    '        For i = 0 To Arrays.Count - 1
    '            Process.MailNotification(loanref, MailLoanRequest, "Approval from Finance Unit on Loan reference " & loanref & " has been " & status, msgbuild.ToString, FinTeam, Process.AppName, "", Arrays(i))
    '        Next

    '        If SendNotification() = "Yes" Then
    '            'Notification from Final Approval to Finance
    '            Process.SendEmail("", Process.AppName, receiver, _
    '                       "Approval from Finance Unit on Loan reference " & loanref & " has been " & status, _
    '                       "<div style=""font-family:Arial; font-size:12px;"">Here are the details:  " & "" _
    '                       & "<br /> <br /> <br /> <table> <tr><td style=""width:150px"">Loan Reference: </td> <td style=""width:42px""> </td><td style=""width:50px"">" & loanref _
    '                       & "</td> </tr><tr><td style=""width:150px"">Request By: </td> <td style=""width:42px""> </td><td style=""width:50px"">" & EmpName _
    '                    & "</td> </tr>	<tr><td style=""width:150px"">Loan Type: </td> <td style=""width:42px""> </td><td style=""width:250px"">" & loantype _
    '                    & "</td> </tr> <tr><td style=""width:150px"">Loan Amount: </td> <td style=""width:42px""> </td><td style=""width:250px"">" & Amount.ToString("#,##0.00") _
    '                     & "</td> </tr> <tr><td style=""width:150px"">Interest Rate: </td> <td style=""width:42px""> </td><td style=""width:250px"">" & intrate.ToString("#,##0.00") _
    '                    & "</td> </tr>	<tr><td style=""width:150px"">Monthly Repayment Amount: </td> <td style=""width:42px""> </td><td style=""width:250px"">" & Repayment.ToString("#,##0.00") _
    '                    & "</td> </tr> <tr><td style=""width:150px"">Repayment Start Date: </td> <td style=""width:42px""> </td><td style=""width:250px"">" & sStartDate _
    '                    & "</td> </tr> <tr><td style=""width:150px"">Finance Unit Approval Status: </td> <td style=""width:42px""> </td><td style=""width:250px"">" & status _
    '                       & " by " & authoriser & "</td> </tr> </table> </div> <br /> <br /> ", _
    '                       "", True)
    '        End If

    '    Catch ex As Exception
    '        HttpContext.Current.Session.Item("exception") = ex.Message
    '    End Try
    'End Sub


    'Public Shared Sub Loan_Notification_FinanceAdmin(ByVal receiver As String, ByVal loanref As String, ByVal EmpName As String, ByVal loantype As String, ByVal Amount As Double, ByVal Repayment As Double, sStartDate As Date, ByVal Desc As String, ByVal reqGuarantor As String)
    '    Try
    '        msgbuild.Clear()
    '        msgbuild.AppendLine("Here are the details:")
    '        msgbuild.AppendLine(" ")
    '        msgbuild.AppendLine("Loan Reference " & loanref & " has been initiated and is pending approval at First Level <br/> Here are the request details:")
    '        msgbuild.AppendLine("Request By        : " & EmpName)
    '        msgbuild.AppendLine("Loan ID           : " & loanref)
    '        msgbuild.AppendLine("Loan              : " & loantype)
    '        msgbuild.AppendLine("Amount            : " & Amount.ToString("#,##0.00"))
    '        msgbuild.AppendLine("Monthly Pay       : " & Repayment.ToString("#,##0.00"))
    '        msgbuild.AppendLine("Start Date        : " & sStartDate.ToLongDateString)
    '        msgbuild.AppendLine("Guarantor Required: " & reqGuarantor)
    '        msgbuild.AppendLine("Description       : " & Desc)

    '        Dim Arrays() As String = Process.GetEmpIDMailList("finance").Split(SeparatorSemi, StringSplitOptions.RemoveEmptyEntries)
    '        For i = 0 To Arrays.Count - 1
    '            Process.MailNotification(loanref, MailLoanRequest, "Loan reference " & loanref & " initiated by " & EmpName, msgbuild.ToString, FinTeam, Process.AppName, "", Arrays(i))
    '        Next

    '        If SendNotification() = "Yes" Then
    '            'Notification for Admin Finance about loan initiated
    '            Process.SendEmail("", Process.AppName, receiver, _
    '                                 "Loan reference " & loanref & " initiated by " & EmpName, _
    '                                 "<div style=""font-family:Arial; font-size:12px;"">Dear Sir/Ma, <br /> <br />Loan Reference " & loanref & " has been initiated and is pending approval at First Level <br/> Here are the request details:  " & "" _
    '                                 & "<br /> <br /> <br /> <table> <tr><td style=""width:50px"">Request By: </td> <td style=""width:42px""> </td><td style=""width:50px"">" & EmpName _
    '                                 & "</td> </tr> <tr><td style=""width:100px"">Reference Number: </td> <td style=""width:42px""> </td><td style=""width:250px"">" & loanref & _
    '                                 "</td> </tr>	<tr><td style=""width:100px"">Loan: </td> <td style=""width:42px""> </td><td style=""width:250px"">" & loantype _
    '                                 & "</td> </tr> <tr><td style=""width:100px"">Loan Amount: </td> <td style=""width:42px""> </td><td style=""width:250px"">" & Amount.ToString("#,##0.00") _
    '                                 & "</td> </tr>	<tr><td style=""width:100px"">Monthly Repayment Amount: </td> <td style=""width:42px""> </td><td style=""width:250px"">" & Repayment.ToString("#,##0.00") _
    '                                 & "</td> </tr> <tr><td style=""width:100px"">Repayment Start Date: </td> <td style=""width:42px""> </td><td style=""width:250px"">" & sStartDate.ToLongDateString _
    '                                 & "</td> </tr> <tr><td style=""width:100px"">Guarantor Required: </td> <td style=""width:42px""> </td><td style=""width:250px"">" & reqGuarantor _
    '                                 & "</td> </tr> <tr><td style=""width:100px"">Description: </td> <td style=""width:42px""> </td><td style=""width:250px"">" & Desc _
    '                                 & "</td> </tr> </table> <br /> <br /> <br /> </div>", _
    '                                 "", True)
    '        End If

    '    Catch ex As Exception
    '        HttpContext.Current.Session.Item("exception") = ex.Message
    '    End Try
    'End Sub


    Public Shared Sub Loan_Notification_Final(ByVal receiver As String, ByVal loanref As String, finalstatus As String, ByVal EmpName As String, ByVal loantype As String, ByVal Amount As Double, ByVal Repayment As Double, repayperiod As Integer, sStartDate As Date, intrate As Double, sempid As String, attachement As String, links As String)
        Try
            Dim subject As String = "Loan " & loanref & " " & finalstatus & " by HR/Finance "

            Dim filePath As String = MailContentURL & MailFolderLoan & "Loan_Notification_Final.txt"
            Dim readertxt As New StreamReader(filePath)
            msgbuild.Clear()
            While Not readertxt.EndOfStream
                Dim line As String = readertxt.ReadLine()
                msgbuild.AppendLine(line)
            End While
            readertxt.Close()

            msgbuild = msgbuild.Replace("@empname", GetEmployeeData(sempid, "fullname")).Replace("@empfname", GetEmployeeData(sempid, "firstname")).Replace("@ref", loanref).Replace("@company", GetCompanyByEmpID(sempid)).Replace("@empid", sempid) _
               .Replace("@startdate", Process.DDMONYYYY(sStartDate)) _
               .Replace("@tenor", FormatNumber(repayperiod, 0)).Replace("@monthlypay", FormatNumber(Repayment, 2)).Replace("@amount", FormatNumber(Amount, 2)).Replace("@interestrate", FormatNumber(intrate, 2))


            Process.MailNotification(loanref, MailLoanRequest, subject, msgbuild.ToString, sempid, HRTeam, "", sempid, links, attachement)

            If SendNotification() = "Yes" Then
                'Notification from Final Approval to Finance
                Process.SendEmail("", HRTeam, receiver, _
                       subject, _
                       "<div style=""font-family:Arial; font-size:12px;"">Here are the details:  " & "" _
                       & "<br /> <br />" & msgbuild.ToString & "</div> <br /> <br /> <a href=" & links & ">" & links & "</a>", _
                       attachement, True, "")
            End If


        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
        End Try
    End Sub
    Public Shared Sub Loan_Approver_HR_Notification(ByVal loanref As String, sempid As String, ByVal mgrid As String, status As String, links As String)
        Try
            Dim subject As String = "Approval: Loan " & loanref & " has been " & status & " by " & GetEmployeeData(mgrid, "fullname")

            Dim filePath As String = MailContentURL & MailFolderLoan & "Loan_Approver_HR_Notification.txt"
            Dim readertxt As New StreamReader(filePath)
            msgbuild.Clear()
            While Not readertxt.EndOfStream
                Dim line As String = readertxt.ReadLine()
                msgbuild.AppendLine(line)
            End While
            readertxt.Close()

            msgbuild = msgbuild.Replace("@empname", GetEmployeeData(sempid, "fullname")).Replace("@empfname", GetEmployeeData(sempid, "firstname")).Replace("@ref", loanref).Replace("@company", GetCompanyByEmpID(sempid)).Replace("@empid", sempid)


            Dim Arrays() As String = Process.GetEmpIDMailList("hr").Split(SeparatorSemi, StringSplitOptions.RemoveEmptyEntries)
            For i = 0 To Arrays.Count - 1
                Process.MailNotification(loanref, MailLoanRequest, subject, msgbuild.ToString, HRTeam, GetCompanyByEmpID(sempid), "", Arrays(i), links)
            Next

            If SendNotification() = "Yes" Then
                'Notification from Supervisor Approval to Finance
                Process.SendEmail("", GetCompanyByEmpID(sempid), GetMailList("hr"), _
                           subject, _
                           "<div style=""font-family:Arial; font-size:12px;"">" & msgbuild.ToString & "</div> <br /> <br /> <a href=" & links & ">" & links & "</a>", _
                           "", True)
            End If

        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
        End Try
    End Sub
    Public Shared Sub Loan_Approver_Approval(ByVal loanref As String, status As String, sempid As String, mgrid As String, links As String)
        Try
            Dim subject As String = "Loan " & loanref & " " & status & " by " & GetEmployeeData(mgrid, "fullname")

            Dim filePath As String = MailContentURL & MailFolderLoan & "Loan_Approver_Approval.txt"
            Dim readertxt As New StreamReader(filePath)
            msgbuild.Clear()
            While Not readertxt.EndOfStream
                Dim line As String = readertxt.ReadLine()
                msgbuild.AppendLine(line)
            End While
            readertxt.Close()

            msgbuild = msgbuild.Replace("@empname", GetEmployeeData(sempid, "fullname")).Replace("@empfname", GetEmployeeData(sempid, "firstname")).Replace("@ref", loanref).Replace("@company", GetCompanyByEmpID(sempid)).Replace("@empid", sempid) _
            .Replace("@mgrname", GetEmployeeData(mgrid, "fullname"))


            Process.MailNotification(loanref, MailLoanRequest, subject, msgbuild.ToString, sempid, GetCompanyByEmpID(sempid), "", sempid, links)

            If SendNotification() = "Yes" Then
                'Notification from Supervisor Approval to Finance
                Process.SendEmail("", GetCompanyByEmpID(sempid), GetEmailAddress(sempid), _
                           subject, _
                           "<div style=""font-family:Arial; font-size:12px;"">" & msgbuild.ToString & " </div> <br /> <br /> <a href=" & links & ">" & links & "</a>", _
                           "", True)
            End If

        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
        End Try
    End Sub
    Public Shared Sub Loan_Guarantor_Approval(ByVal loanref As String, status As String, sempid As String, mgrid As String, links As String)
        Try
            Dim subject As String = "Guarantee on loan " & loanref & " is " & status

            Dim filePath As String = MailContentURL & MailFolderLoan & "Loan_Guarantor_Approval.txt"
            Dim readertxt As New StreamReader(filePath)
            msgbuild.Clear()
            While Not readertxt.EndOfStream
                Dim line As String = readertxt.ReadLine()
                msgbuild.AppendLine(line)
            End While
            readertxt.Close()

            msgbuild = msgbuild.Replace("@empname", GetEmployeeData(sempid, "fullname")).Replace("@empfname", GetEmployeeData(sempid, "firstname")).Replace("@ref", loanref).Replace("@company", GetCompanyByEmpID(sempid)).Replace("@empid", sempid) _
            .Replace("@mgrname", GetEmployeeData(mgrid, "fullname")).Replace("@status", status)

            Process.MailNotification(loanref, MailLoanRequest, subject, msgbuild.ToString, sempid, GetCompanyByEmpID(sempid), "", sempid, links)

            If SendNotification() = "Yes" Then
                'Notification for requester
                Process.SendEmail("", GetCompanyByEmpID(sempid), GetEmailAddress(sempid), _
                                     subject, _
                                     "<div style=""font-family:Arial; font-size:12px;"">" & msgbuild.ToString & "<br /> <br /> <a href=" & links & ">" & links & "</a></div>", _
                                      "", True)
            End If

        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
        End Try
    End Sub
    Public Shared Sub Loan_Guarantor_Notification(ByVal loanref As String, ByVal loantype As String, ByVal amount As Double, sempid As String, mgrid As String, links As String)
        Try
            Dim subject As String = "Guarantee Request on loan: " & loanref

            Dim filePath As String = MailContentURL & MailFolderLoan & "Loan_Guarantor_Notification.txt"
            Dim readertxt As New StreamReader(filePath)
            msgbuild.Clear()
            While Not readertxt.EndOfStream
                Dim line As String = readertxt.ReadLine()
                msgbuild.AppendLine(line)
            End While
            readertxt.Close()

            msgbuild = msgbuild.Replace("@empname", GetEmployeeData(sempid, "fullname")).Replace("@empfname", GetEmployeeData(sempid, "firstname")).Replace("@ref", loanref).Replace("@company", GetCompanyByEmpID(sempid)).Replace("@empid", sempid) _
            .Replace("@mgrfname", GetEmployeeData(mgrid, "firstname")).Replace("@amount", FormatNumber(amount, 2)).Replace("@loantype", loantype)

            Process.MailNotification(loanref, MailLoanRequest, subject, msgbuild.ToString, mgrid, sempid, "", mgrid, links)

            If SendNotification() = "Yes" Then
                'Notification for requester
                Process.SendEmail("", GetCompanyByEmpID(sempid), GetEmailAddress(mgrid), _
                                     subject, _
                                     "<div style=""font-family:Arial; font-size:12px;"">" & msgbuild.ToString & "<br /> <br /> <a href=" & links & ">" & links & "</a></div>", _
                                      "", True)
            End If

        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
        End Try
    End Sub
    Public Shared Sub Loan_Approver_Notification(ByVal loanref As String, ByVal loantype As String, ByVal Amount As Double, ByVal Repayment As Double, sStartDate As Date, ByVal Desc As String, ByVal reqGuarantor As String, ByVal Guarantor As String, sempid As String, mgrid As String, links As String)
        Try

            Dim subject As String = "Loan reference " & loanref & " has been forwarded to you for your approval"

            Dim filePath As String = MailContentURL & MailFolderLoan & "Loan_Approver_Notification.txt"
            Dim readertxt As New StreamReader(filePath)
            msgbuild.Clear()
            While Not readertxt.EndOfStream
                Dim line As String = readertxt.ReadLine()
                msgbuild.AppendLine(line)
            End While
            readertxt.Close()

            msgbuild = msgbuild.Replace("@empname", GetEmployeeData(sempid, "fullname")).Replace("@empfname", GetEmployeeData(sempid, "firstname")).Replace("@ref", loanref).Replace("@company", GetCompanyByEmpID(sempid)).Replace("@empid", sempid) _
            .Replace("@mgrfname", GetEmployeeData(mgrid, "firstname")).Replace("@amount", FormatNumber(Amount, 2)).Replace("@loantype", loantype).Replace("@monthlypay", FormatNumber(Repayment, 2)).Replace("@startdate", DDMONYYYY(sStartDate)) _
            .Replace("@guarantor", GetEmployeeData(Guarantor, "fullname")).Replace("@guadept", GetEmployeeData(Guarantor, "office")).Replace("@desc", Desc)


            Process.MailNotification(loanref, MailLoanRequest, subject, msgbuild.ToString, mgrid, sempid, "", mgrid, links)

            If SendNotification() = "Yes" Then
                'Notification for Supervisor to approve
                Process.SendEmail("", GetCompanyByEmpID(sempid), GetEmailAddress(mgrid), _
                                     subject, _
                                     "<div style=""font-family:Arial; font-size:12px;"">" & msgbuild.ToString & "<br /> <br /><a href=" & links & ">" & links & "</a></div>", _
                                     "", True)
            End If

        Catch ex As Exception
        End Try
    End Sub
    Public Shared Sub Loan_Notification(ByVal loanref As String, ByVal loantype As String, ByVal Amount As Double, ByVal Repayment As Double, sStartDate As Date, tenor As Integer, ByVal Guarantor As String, sempid As String)
        Try

            Dim subject As String = "Your loan request with reference " & loanref & " has been forwarded for approval"

            Dim filePath As String = MailContentURL & MailFolderLoan & "Loan_Notification.txt"
            Dim readertxt As New StreamReader(filePath)
            msgbuild.Clear()
            While Not readertxt.EndOfStream
                Dim line As String = readertxt.ReadLine()
                msgbuild.AppendLine(line)
            End While
            readertxt.Close()

            msgbuild = msgbuild.Replace("@empname", GetEmployeeData(sempid, "fullname")).Replace("@empfname", GetEmployeeData(sempid, "firstname")).Replace("@ref", loanref).Replace("@company", GetCompanyByEmpID(sempid)).Replace("@empid", sempid) _
            .Replace("@amount", FormatNumber(Amount, 2)).Replace("@loantype", loantype).Replace("@monthlypay", FormatNumber(Repayment, 2)).Replace("@startdate", DDMONYYYY(sStartDate)) _
            .Replace("@guarantor", GetEmployeeData(Guarantor, "fullname")).Replace("@guadept", GetEmployeeData(Guarantor, "office")).Replace("@tenor", FormatNumber(tenor, 0))


            Process.MailNotification(loanref, MailLoanRequest, subject, msgbuild.ToString, sempid, GetCompanyByEmpID(sempid), "", sempid, "")

            If SendNotification() = "Yes" Then
                'Notification for Supervisor to approve
                Process.SendEmail("", GetCompanyByEmpID(sempid), GetEmailAddress(sempid), _
                                     subject, _
                                     "<div style=""font-family:Arial; font-size:12px;"">" & msgbuild.ToString & "</div>", _
                                     "", True)
            End If

        Catch ex As Exception
        End Try
    End Sub




#End Region
#Region "Training Notification"
    Public Shared Function Training_Emp_UnApproved(ByVal Approval As String, ByVal TrainingTitle As String, sempid As String, mgrid As String) As Boolean
        Try
            msgbuild.Clear()
            msgbuild.AppendLine("Your Training Request is " & Approval & " by " & GetEmployeeData(mgrid, "fullname"))

            Process.MailNotification("0", MailTraining, "Re:Training Request: " & TrainingTitle, msgbuild.ToString, sempid, Process.AppName, "", sempid, "")


            If SendNotification() = "Yes" Then
                'Notification for Supervisor to approve
                Return SendEmail("", Process.AppName, GetEmailAddress(sempid), _
                                    "Re:Training Request: " & TrainingTitle, _
                                     "<div style=""font-family:Arial; font-size:12px;""> " _
                                     & ", <br /> <br />Your Training Request is " & Approval & " by " & GetEmployeeData(mgrid, "fullname") _
                                     & "<br /> <br /> </div> <br /> <br/>" & GetCompanyByEmpID(sempid) & " <br />", _
                                     "", True)
            Else
                Return True
            End If

        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
            Return False
        End Try
    End Function
    Public Shared Function Training_Manager_Request(ByVal TrainingTitle As String, eventdate As String, eventtime As String, ByVal venue As String, ByVal copymail As String, sempid As String, mgrid As String, links As String) As Boolean
        Try
            Dim firstname As String = GetEmployeeData(sempid, "fullname")
            Dim subject As String = "Training Request: " & TrainingTitle & " for " & GetEmployeeData(sempid, "fullname")

            Dim filePath As String = MailContentURL & MailFolderTraining & "Training_Request.txt"
            Dim readertxt As New StreamReader(filePath)
            msgbuild.Clear()
            While Not readertxt.EndOfStream
                Dim line As String = readertxt.ReadLine()
                msgbuild.AppendLine(line)
            End While
            readertxt.Close()

            msgbuild = msgbuild.Replace("@dept", GetEmployeeData(sempid, "office")).Replace("@empname", GetEmployeeData(sempid, "fullname")).Replace("@empfname", GetEmployeeData(sempid, "firstname")).Replace("@company", GetCompanyByEmpID(mgrid)).Replace("@empid", sempid) _
            .Replace("@mgrname", GetEmployeeData(mgrid, "fullname")).Replace("@mgrfname", GetEmployeeData(mgrid, "firstname")).Replace("@mgrdept", GetEmployeeData(mgrid, "office")).Replace("@training", TrainingTitle).Replace("@venue", venue).Replace("@eventdate", eventdate) _
            .Replace("@mgrjob", GetEmployeeData(mgrid, "jobtitle")).Replace("@eventtime", eventtime)


            Dim Arrays() As String = Process.GetEmpIDMailList("hr").Split(SeparatorSemi, StringSplitOptions.RemoveEmptyEntries)
            For i = 0 To Arrays.Count - 1
                Process.MailNotification(sempid, MailTraining, subject, msgbuild.ToString, HRTeam, mgrid, "", Arrays(i), links)
            Next

            If SendNotification() = "Yes" Then
                Return SendEmail("", GetCompanyByEmpID(mgrid), GetEmailAddress(mgrid), _
                                 subject, _
                                   "<div style=""font-family:Arial; font-size:12px;"">" & msgbuild.ToString & "<br /> <br /><a href=" & links & ">" & links & "</a> <br /> <br /> </div>", _
                                 "", True, copymail)
            Else
                Return True
            End If

        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
            Return False
        End Try
    End Function
    Public Shared Function Training_Emp_Request(ByVal TrainingTitle As String, ByVal strCoordinator As String, eventdate As String, eventtime As String, ByVal venue As String, ByVal copymail As String, sempid As String, mgrid As String, links As String) As Boolean
        Try
            Dim firstname As String = GetEmployeeData(sempid, "fullname")
            Dim subject As String = "Training Request: " & TrainingTitle & " from " & GetEmployeeData(sempid, "fullname")


            Dim filePath As String = MailContentURL & MailFolderTraining & "Training_Request.txt"
            Dim readertxt As New StreamReader(filePath)
            msgbuild.Clear()
            While Not readertxt.EndOfStream
                Dim line As String = readertxt.ReadLine()
                msgbuild.AppendLine(line)
            End While
            readertxt.Close()

            msgbuild = msgbuild.Replace("@dept", GetEmployeeData(sempid, "office")).Replace("@empname", GetEmployeeData(sempid, "fullname")).Replace("@empfname", GetEmployeeData(sempid, "firstname")).Replace("@company", GetCompanyByEmpID(mgrid)).Replace("@empid", sempid) _
            .Replace("@mgrname", GetEmployeeData(sempid, "fullname")).Replace("@mgrfname", GetEmployeeData(sempid, "firstname")).Replace("@mgrdept", GetEmployeeData(sempid, "office")).Replace("@training", TrainingTitle).Replace("@venue", venue).Replace("@eventdate", eventdate) _
            .Replace("@mgrjob", GetEmployeeData(sempid, "jobtitle")).Replace("@eventtime", eventtime)


            Process.MailNotification(sempid, MailTraining, subject, msgbuild.ToString, mgrid, sempid, "", mgrid, links)

            If SendNotification() = "Yes" Then
                Return SendEmail("", GetCompanyByEmpID(mgrid), GetEmailAddress(mgrid), _
                                 subject, _
                                   "<div style=""font-family:Arial; font-size:12px;"">" & msgbuild.ToString & "<br /> <br /><a href=" & links & ">" & links & "</a> <br /> </div>", _
                                 "", True, copymail)
            Else
                Return True
            End If

        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
            Return False
        End Try
    End Function
    Public Shared Function Training_Emp_Request_To_HR(ByVal ApprovedDate As Date, ByVal TrainingTitle As String, ByVal strCoordinator As String, eventdate As String, eventtime As String, ByVal venue As String, sempid As String, mgrid As String, links As String) As Boolean
        Try
            Dim subject As String = "Training Request: " & TrainingTitle & " for " & GetEmployeeData(sempid, "fullname")

            Dim filePath As String = MailContentURL & MailFolderTraining & "Training_Request.txt"
            Dim readertxt As New StreamReader(filePath)
            msgbuild.Clear()
            While Not readertxt.EndOfStream
                Dim line As String = readertxt.ReadLine()
                msgbuild.AppendLine(line)
            End While
            readertxt.Close()

            msgbuild = msgbuild.Replace("@dept", GetEmployeeData(sempid, "office")).Replace("@empname", GetEmployeeData(sempid, "fullname")).Replace("@empfname", GetEmployeeData(sempid, "firstname")).Replace("@company", GetCompanyByEmpID(mgrid)).Replace("@empid", sempid) _
            .Replace("@mgrname", GetEmployeeData(mgrid, "fullname")).Replace("@mgrfname", GetEmployeeData(mgrid, "firstname")).Replace("@mgrdept", GetEmployeeData(mgrid, "office")).Replace("@training", TrainingTitle).Replace("@venue", venue).Replace("@eventdate", eventdate) _
            .Replace("@mgrjob", GetEmployeeData(mgrid, "jobtitle")).Replace("@eventtime", eventtime)


            Dim Arrays() As String = Process.GetEmpIDMailList("hr").Split(SeparatorSemi, StringSplitOptions.RemoveEmptyEntries)
            For i = 0 To Arrays.Count - 1
                Process.MailNotification(sempid, MailTraining, subject, msgbuild.ToString, HRTeam, mgrid, HRTeam, Arrays(i), links)
            Next

            If SendNotification() = "Yes" Then
                Return SendEmail("", GetCompanyByEmpID(mgrid), GetMailList("hr"), _
                                 subject, _
                                   "<div style=""font-family:Arial; font-size:12px;"">" & msgbuild.ToString & "<br /> <br /> <a href=" & links & ">" & links & "</a></div>", _
                                 "", True, "")
            Else
                Return True
            End If

        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
            Return False
        End Try
    End Function

    Public Shared Function Training_Notification_coordinator(ByVal TrainingTitle As String, ByVal strTrainer As String, ByVal strCoordinator As String, venue As String, eventdate As String, eventtime As String, sempid As String, links As String) As Boolean
        Try
            Dim subject As String = "Formal Invitation as Training Coordinator for " & TrainingTitle

            Dim filePath As String = MailContentURL & MailFolderTraining & "Training_Notification_Coordinators.txt"
            Dim readertxt As New StreamReader(filePath)
            msgbuild.Clear()
            While Not readertxt.EndOfStream
                Dim line As String = readertxt.ReadLine()
                msgbuild.AppendLine(line)
            End While
            readertxt.Close()

            msgbuild = msgbuild.Replace("@dept", GetEmployeeData(sempid, "office")).Replace("@empname", GetEmployeeData(sempid, "fullname")).Replace("@empfname", GetEmployeeData(sempid, "firstname")).Replace("@company", GetCompanyByEmpID(sempid)).Replace("@empid", sempid) _
            .Replace("@mgrname", GetEmployeeData(sempid, "fullname")).Replace("@mgrfname", GetEmployeeData(sempid, "firstname")).Replace("@mgrdept", GetEmployeeData(sempid, "office")).Replace("@training", TrainingTitle).Replace("@venue", venue).Replace("@eventdate", eventdate) _
            .Replace("@mgrjob", GetEmployeeData(sempid, "jobtitle")).Replace("@eventtime", eventtime)

            Process.MailNotification("0", MailTraining, subject, msgbuild.ToString, sempid, HRTeam, "", sempid, links, "")


            If SendNotification() = "Yes" Then
                Return SendEmail("", HRTeam, GetEmailAddress(sempid),
                                     subject,
                                     "<div style=""font-family:Arial; font-size:12px;"">" & msgbuild.ToString & "<br /> <br /> <a href=" & links & ">" & links & "</a></div>",
                                     "", True)
            Else
                Return True
            End If
        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
            Return False
        End Try
    End Function

    Public Shared Function Training_Notification_Trainers(ByVal TrainingTitle As String, ByVal strTrainer As String, ByVal strCoordinator As String, venue As String, eventdate As String, eventtime As String, sempid As String, links As String) As Boolean
        Try
            Dim subject As String = "Formal Invitation as training instructor for " & TrainingTitle

            Dim filePath As String = MailContentURL & MailFolderTraining & "Training_Notification_Trainers.txt"
            Dim readertxt As New StreamReader(filePath)
            msgbuild.Clear()
            While Not readertxt.EndOfStream
                Dim line As String = readertxt.ReadLine()
                msgbuild.AppendLine(line)
            End While
            readertxt.Close()

            msgbuild = msgbuild.Replace("@dept", GetEmployeeData(sempid, "office")).Replace("@empname", GetEmployeeData(sempid, "fullname")).Replace("@empfname", GetEmployeeData(sempid, "firstname")).Replace("@company", GetCompanyByEmpID(sempid)).Replace("@empid", sempid) _
            .Replace("@mgrname", GetEmployeeData(sempid, "fullname")).Replace("@mgrfname", GetEmployeeData(sempid, "firstname")).Replace("@mgrdept", GetEmployeeData(sempid, "office")).Replace("@training", TrainingTitle).Replace("@venue", venue).Replace("@eventdate", eventdate) _
            .Replace("@mgrjob", GetEmployeeData(sempid, "jobtitle")).Replace("@eventtime", eventtime)

            Process.MailNotification("0", MailTraining, subject, msgbuild.ToString, sempid, HRTeam, "", sempid, links, "")


            If SendNotification() = "Yes" Then
                Return SendEmail("", HRTeam, GetEmailAddress(sempid), _
                                     subject, _
                                     "<div style=""font-family:Arial; font-size:12px;"">" & msgbuild.ToString & "<br /> <br /> <a href=" & links & ">" & links & "</a></div>", _
                                     "", True)
            Else
                Return True
            End If

        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
            Return False
        End Try
    End Function
    Public Shared Function Training_Notification_Trainees(ByVal TrainingTitle As String, ByVal strTrainer As String, ByVal strCoordinator As String, venue As String, eventdate As Date, eventtime As String, duedate As Date, sempid As String, links As String) As Boolean

        Try
            Dim firstname As String = GetEmployeeData(sempid, "firstname")
            Dim managerid As String = GetEmployeeData(sempid, "linemanagerid")

            Dim subject As String = "Invitation: Training Session for " & TrainingTitle
            msgbuild.Clear()
            msgbuild.AppendLine("Dear " & firstname & ",")
            msgbuild.AppendLine("You have been invited to a training session, below are the details:")
            msgbuild.AppendLine("")
            msgbuild.AppendLine("Training    : " & TrainingTitle)
            msgbuild.AppendLine("Start Date  : " & eventdate.ToLongDateString)
            msgbuild.AppendLine("Start Time  : " & eventtime)
            msgbuild.AppendLine("End Date    : " & duedate.ToLongDateString)
            msgbuild.AppendLine("Venue       : " & venue)
            msgbuild.AppendLine("Coordinator : " & strCoordinator)
            msgbuild.AppendLine("  ")
            msgbuild.AppendLine("  ")
            msgbuild.AppendLine("Signed:  " & HRTeam)
            msgbuild.AppendLine("         " & GetCompanyByEmpID(sempid))

            Process.MailNotification("0", MailTraining, subject, msgbuild.ToString, sempid, HRTeam, managerid, sempid, links)

            If SendNotification() = "Yes" Then
                Return SendEmail("", HRTeam, GetEmailAddress(sempid), _
                                subject, _
                                "<div style=""font-family:Arial; font-size:12px;"">Dear " & firstname & ", <br /> <br /> You have been invited to a training session, below are the details:  " & "" _
                                & "<br /> <br /> <br /> <table> <tr><td style=""width:50px"">Training Title: </td> <td style=""width:42px""> </td><td style=""width:50px"">" & TrainingTitle _
                                & "</td> </tr> <tr><td style=""width:100px"">Coordinator: </td> <td style=""width:42px""> </td><td style=""width:250px"">" & strCoordinator _
                                & "</td> </tr> <tr><td style=""width:100px"">Trainer: </td> <td style=""width:42px""> </td><td style=""width:250px"">" & strTrainer _
                                & "</td> </tr>	<tr><td style=""width:100px"">Date Scheduled: </td> <td style=""width:42px""> </td><td style=""width:250px"">" & eventdate.ToLongDateString _
                                    & "</td> </tr> <tr><td style=""width:100px"">Time: </td> <td style=""width:42px""> </td><td style=""width:250px"">" & eventtime _
                                & "</td> </tr> <tr><td style=""width:100px"">Date Due: </td> <td style=""width:42px""> </td><td style=""width:250px"">" & duedate.ToLongDateString _
                                & "</td> </tr>	<tr><td style=""width:100px"">Venue: </td> <td style=""width:42px""> </td><td style=""width:250px"">" & venue _
                                & "</td> </tr> </table> <br /> <br /> <a href=" & links & ">" & links & "</a><br /><br /><br /> HR Department <br />" & GetCompanyByEmpID(sempid) & "</div>", _
                                "", True, "")
            Else
                Return True
            End If

        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
            Return False
        End Try
    End Function
    Public Shared Sub Training_Learning_Assessment_Notify(ByVal receiver As String, ByVal empName As String, ByVal TrainingTitle As String, sempid As String, mgrid As String)
        Try
            msgbuild.Clear()
            msgbuild.AppendLine("Dear " & empName & ",")
            msgbuild.AppendLine("This questioning is used as a deliberate way to find out what trainee learnt during and after training ")
            msgbuild.AppendLine("")
            msgbuild.AppendLine("Navigate to Training Section of the Employee Module on to start assessment")
            msgbuild.AppendLine("  ")
            msgbuild.AppendLine("  ")
            msgbuild.AppendLine("Signed:  " & HRTeam)

            Process.MailNotification("0", MailTraining, TrainingTitle & " Learning Assessment", msgbuild.ToString, sempid, Process.AppName, "", sempid, "")

            If SendNotification() = "Yes" Then
                SendEmail("", Process.AppName, receiver, _
                                   TrainingTitle & " Learning Assessment", _
                                   "<div style=""font-family:Arial; font-size:12px;"">Dear " & empName & ", <br /> <br /> This questioning is used as a deliberate way to find out what trainee learnt during and after training  " & "" _
                                      & "<br /> <br />Login to the Training Section of the Employee Module on " & Process.AppName & " to start learning assessment" _
                                      & "<br /> <br />HR Department <br />" & Process.GetCompanyName & "</div>", _
                                   "", True)

            End If
            strExp = ""
        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
        End Try
    End Sub
    Public Shared Sub Training_Assessment_Review_Complete(stype As String, ByVal TrainingTitle As String, sempid As String, mgrid As String, link2 As String)
        Try
            Dim subject As String = GetEmployeeData(mgrid, "fullname") & " has reviewed " & TrainingTitle & " " & stype & " Assessment Completed by " & GetEmployeeData(sempid, "fullname")
            Dim line0 As String = "Employee ID: " & sempid
            Dim line1 As String = GetEmployeeData(sempid, "fullname") & " has completed & " & stype & " assessment for " & TrainingTitle & " and has been successfully reviewed by " & GetEmployeeData(mgrid, "fullname")
            Dim line2 As String = "Thank you"
            msgbuild.Clear()
            msgbuild.AppendLine(line0)
            msgbuild.AppendLine(" ")
            msgbuild.AppendLine(line1)
            msgbuild.AppendLine("  ")
            msgbuild.AppendLine(line2)
            msgbuild.AppendLine(" ")
            msgbuild.AppendLine(GetCompanyByEmpID(sempid))

            Dim Arrays() As String = Process.GetEmpIDMailList("hr").Split(SeparatorSemi, StringSplitOptions.RemoveEmptyEntries)
            For i = 0 To Arrays.Count - 1
                Process.MailNotification(sempid, MailTraining, subject, msgbuild.ToString, mgrid, HRTeam, "", Arrays(i), link2)
            Next

            If SendNotification() = "Yes" Then


                SendEmail("", Process.AppName, GetMailList("hr"), _
                         subject, _
                           "<div style=""font-family:Arial; font-size:12px;"">" & line0 & ", <br /> <br />" & line1 & "" _
                              & "<br /> <br />" & line2 & "<br /> <br /> <a href=" & link2 & ">" & link2 & "</a> <br /> <br /> <br />" & GetCompanyByEmpID(sempid) & "</div>", _
                           "", True)
            End If

            strExp = ""
        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
        End Try
    End Sub
    Public Shared Sub Training_Assessment_Complete(stype As String, ByVal TrainingTitle As String, sempid As String, mgrid As String, links As String, link2 As String)
        Try
            Dim subject As String = TrainingTitle & " " & stype & " by " & GetEmployeeData(sempid, "fullname")
            Dim line0 As String = "Employee ID: " & sempid
            Dim line1 As String = GetEmployeeData(sempid, "fullname") & " has completed & " & stype & " assessment for " & TrainingTitle
            Dim line2 As String = "Thank you"
            msgbuild.Clear()
            msgbuild.AppendLine(line0)
            msgbuild.AppendLine(" ")
            msgbuild.AppendLine(line1)
            msgbuild.AppendLine("  ")
            msgbuild.AppendLine(line2)
            msgbuild.AppendLine(" ")
            msgbuild.AppendLine(GetCompanyByEmpID(sempid))

            Process.MailNotification(sempid, MailTraining, subject, msgbuild.ToString, mgrid, HRTeam, "", mgrid, links)

            'Dim Arrays() As String = Process.GetEmpIDMailList("hr").Split(SeparatorSemi, StringSplitOptions.RemoveEmptyEntries)
            'For i = 0 To Arrays.Count - 1
            '    Process.MailNotification(sempid, MailTraining, subject, msgbuild.ToString, mgrid, HRTeam, "", Arrays(i), link2)
            'Next

            If SendNotification() = "Yes" Then
                SendEmail("", Process.AppName, GetEmailAddress(mgrid),
                            subject,
                              "<div style=""font-family:Arial; font-size:12px;"">" & line0 & ", <br /> <br />" & line1 & "" _
                                 & "<br /> <br />" & line2 & "<br /> <br /> <a href=" & links & ">" & links & "</a> <br /> <br /> <br />" & GetCompanyByEmpID(sempid) & "</div>",
                              "", True)

                'SendEmail("", Process.AppName, GetMailList("hr"), _
                '         subject, _
                '           "<div style=""font-family:Arial; font-size:12px;"">" & line0 & ", <br /> <br />" & line1 & "" _
                '              & "<br /> <br />" & line2 & "<br /> <br /> <a href=" & link2 & ">" & link2 & "</a> <br /> <br /> <br />" & GetCompanyByEmpID(sempid) & "</div>", _
                '           "", True)
            End If

            strExp = ""
        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
        End Try
    End Sub
    Public Shared Sub Training_Application_Assessment_AutoAlert()
        Try
            Dim strsessionId As String = ""
            Dim strsessionName As String = ""
            Dim AuthenCode As String = "TRAINSESSION"
            'Get trainings which the application assessment is ready
            Dim strTrainings As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Training_Sessions_Application_Assessment_Get")

            For i As Integer = 0 To strTrainings.Tables(0).Rows.Count - 1
                strsessionId = strTrainings.Tables(0).Rows(i).Item("id").ToString()
                strsessionName = strTrainings.Tables(0).Rows(i).Item("name").ToString()

                Dim strTrainee As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Training_Sessions_Application_Assessment_Get_Trainees", strsessionId)

                For d As Integer = 0 To strTrainee.Tables(0).Rows.Count - 1
                    Dim EmpID As String = strTrainee.Tables(0).Rows(d).Item("empid").ToString()
                    'Mail Trainees                    
                    Process.Training_Application_Assessment_Notify(strsessionId, strsessionName, EmpID, ApplicationURL() + "/" + Process.GetMailLink(AuthenCode, 2))
                Next
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Employee_Training_Sessions_Update_App_Assessment", strsessionId)
            Next
        Catch ex As Exception

        End Try
    End Sub
    Public Shared Sub Training_Application_Assessment_Notify(ByVal refid As String, ByVal TrainingTitle As String, sempid As String, links As String)
        Try
            msgbuild.Clear()
            msgbuild.AppendLine("Dear " & GetEmployeeData(sempid, "firstname") & ",")
            msgbuild.AppendLine("This assessment ensures that your training satisfactorily meets specialty-specific training requirements and are able to apply training to their day-to-day job routine")
            msgbuild.AppendLine("")
            msgbuild.AppendLine("Navigate to Training Section of the Employee Module on to start application assessment")
            msgbuild.AppendLine("  ")
            msgbuild.AppendLine("  ")
            msgbuild.AppendLine("Signed:  " & HRTeam)

            Process.MailNotification(refid, MailTraining, "Time for " & TrainingTitle & " Application Assessment", msgbuild.ToString, sempid, HRTeam, "", sempid, links)

            If SendNotification() = "Yes" Then
                SendEmail("", HRTeam, GetEmailAddress(sempid),
                            "Time for " & TrainingTitle & " Application Assessment",
                              "<div style=""font-family:Arial; font-size:12px;"">Dear " & GetEmployeeData(sempid, "firstname") & ", <br /> <br />This assessment ensures that your training satisfactorily meets specialty-specific training requirements and are able to apply training to their day-to-day job routine  " & "" _
                                 & "<br /> <br /><a href=" & links & ">" & links & "</a> <br /> <br /> HR Department <br />" & GetCompanyByEmpID(sempid) & "</div>",
                              "", True)
            End If

            strExp = ""
        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
        End Try
    End Sub
    Public Shared Sub Training_Request_By_Trainee(ByVal receiver As String, ByVal empName As String, ByVal TrainingTitle As String, eventdate As Date, sempid As String, mgrid As String)
        Try
            msgbuild.Clear()
            msgbuild.AppendLine("Dear " & empName & ",")
            msgbuild.AppendLine("Your training request for " & TrainingTitle & " scheduled for " & eventdate & " has been forwarded")
            msgbuild.AppendLine("")
            msgbuild.AppendLine("You will be informed once there is a change in approval status")
            msgbuild.AppendLine("  ")
            msgbuild.AppendLine("  ")
            msgbuild.AppendLine("Signed:  " & HRTeam)

            Process.MailNotification("0", MailTraining, "Training Request for " & TrainingTitle & " has been forwarded", msgbuild.ToString, sempid, Process.AppName, "", sempid, "")

            If SendNotification() = "Yes" Then
                Process.SendEmail("", Process.AppName, receiver, _
                                 "Training Request for " & TrainingTitle & " sent", _
                                 "<div style=""font-family:Arial; font-size:12px;"">Dear " & empName & ", <br /> <br />Your training request for " & TrainingTitle & " scheduled for " & eventdate & " has been forwarded" _
                                 & "<br /> <br /> You will be informed once there is a change in approval status. </div>", _
                                 "", True)
            End If

        Catch ex As Exception
        End Try
    End Sub
    Public Shared Sub Training_For_Approval_Supervisors(ByVal receiver As String, ByVal empname As String, ByVal office As String, ByVal TrainingTitle As String, venue As String, eventdate As Date, eventtime As String, duedate As Date, sempid As String, mgrid As String, Optional copymail As String = "")
        Try
            msgbuild.Clear()
            msgbuild.AppendLine(empname & " of " & office & " has requested to participate for the training session stated below: ")
            msgbuild.AppendLine(" ")
            msgbuild.AppendLine("Training    : " & TrainingTitle)
            msgbuild.AppendLine("Start Date  : " & eventdate.ToLongDateString)
            msgbuild.AppendLine("Start Time  : " & eventtime)
            msgbuild.AppendLine("End Date    : " & duedate.ToLongDateString)
            msgbuild.AppendLine("Venue       : " & venue)
            msgbuild.AppendLine(" ")
            msgbuild.AppendLine(" ")
            msgbuild.AppendLine("Request requires your approval")

            Process.MailNotification("0", MailTraining, TrainingTitle & " training request by" & empname, msgbuild.ToString, mgrid, Process.AppName, HRTeam, mgrid, "")
            Dim Arrays() As String = Process.GetEmpIDMailList("hr").Split(SeparatorSemi, StringSplitOptions.RemoveEmptyEntries)
            For i = 0 To Arrays.Count - 1
                Process.MailNotification("0", MailTraining, TrainingTitle & " training request by" & empname, msgbuild.ToString, mgrid, Process.AppName, HRTeam, Arrays(i), "")
            Next


            If SendNotification() = "Yes" Then
                Process.SendEmail("", Process.AppName, receiver, _
                                  TrainingTitle & " training request by" & empname, _
                                  "<div style=""font-family:Arial; font-size:12px;"">" & empname & " of " & office & " has requested to participate for the training session stated below:  " & "" _
                                  & "<br /> <br /> <br /> <table> <tr><td style=""width:50px"">Training Title: </td> <td style=""width:42px""> </td><td style=""width:50px"">" & TrainingTitle _
                                  & "</td> </tr>	<tr><td style=""width:100px"">Date Scheduled: </td> <td style=""width:42px""> </td><td style=""width:250px"">" & eventdate _
                                      & "</td> </tr> <tr><td style=""width:100px"">Time: </td> <td style=""width:42px""> </td><td style=""width:250px"">" & eventtime _
                                  & "</td> </tr> <tr><td style=""width:100px"">Date Due: </td> <td style=""width:42px""> </td><td style=""width:250px"">" & duedate _
                                  & "</td> </tr>	<tr><td style=""width:100px"">Venue: </td> <td style=""width:42px""> </td><td style=""width:250px"">" & venue _
                                   & "</td> </tr> </table> <br /> <br /> <br />HR Department <br />" & Process.GetCompanyName & "</div>", _
                                  "", True, copymail)
            End If

        Catch ex As Exception

        End Try
    End Sub

    Public Shared Function Training_Employee(ByVal Approval As String, ByVal TrainingTitle As String, sempid As String, mgrid As String, ByVal certname As String) As Boolean
        Try
            msgbuild.Clear()
            msgbuild.AppendLine(TrainingTitle & "('" & certname & "')" & " by " & GetEmployeeData(mgrid, "fullname"))

            Process.MailNotification("0", MailTraining, TrainingTitle, msgbuild.ToString, sempid, Process.AppName, "", sempid, "")


            If SendNotification() = "Yes" Then
                'Notification for Supervisor to approve
                Return SendEmail("", Process.AppName, GetEmailAddress(sempid), _
                                    " ", _
                                     "<div style=""font-family:Arial; font-size:12px;""> " _
                                     & ", <br /> <br />" & TrainingTitle & "('" & certname & "')" & " by " & GetEmployeeData(mgrid, "fullname") _
                                     & "<br /> <br /> </div> <br /> <br/>" & GetCompanyByEmpID(sempid) & " <br />", _
                                     "", True)
            Else
                Return True
            End If

        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
            Return False
        End Try
    End Function
#End Region
#Region "Time Sheet Notification"
    Public Shared Sub PM_TimeSheet_Approval(ByVal pmEmail As String, ByVal pmName As String, ByVal projecttitle As String, ByVal clientname As String, ByVal employee As String, ByVal activityDate As Date, ByVal activityEndDate As Date, ByVal starttime As String, ByVal endtime As String, sempid As String, mgrid As String)
        Try
            msgbuild.Clear()
            msgbuild.AppendLine("Dear " & pmName & ", ")
            msgbuild.AppendLine(" ")
            msgbuild.AppendLine("As Project Manager for " & projecttitle)
            msgbuild.AppendLine("Here are the Time Sheet details for your approval:")
            msgbuild.AppendLine(" ")
            msgbuild.AppendLine(" ")
            msgbuild.AppendLine("Project    : " & projecttitle)
            msgbuild.AppendLine("Client     : " & clientname)
            msgbuild.AppendLine("Employee   : " & employee)
            msgbuild.AppendLine("Date In    : " & activityDate.ToLongDateString)
            msgbuild.AppendLine("Time In    : " & Process.AMPM_Time(starttime.ToString))
            msgbuild.AppendLine("Date Out   : " & activityEndDate.ToLongDateString)
            msgbuild.AppendLine("Time Out   : " & Process.AMPM_Time(endtime.ToString))
            msgbuild.AppendLine(" ")


            Process.MailNotification("0", MailTimeSheet, projecttitle & " Time Sheet " & activityDate & " for Approval", msgbuild.ToString, mgrid, sempid, "", mgrid, "")
            Process.MailNotification("0", MailTimeSheet, projecttitle & " Time Sheet " & activityDate & " for Approval", msgbuild.ToString, mgrid, sempid, "", sempid, "")

            If SendNotification() = "Yes" Then
                Process.SendEmail("", Process.AppName, pmEmail, _
                                    projecttitle & " Time Sheet " & activityDate & " for Approval", _
                                   "<div style=""font-family:Arial; font-size:12px;"">Dear " & pmName & ", <br /> <br /> As Project Manager for " & projecttitle & "" _
                                   & "<br /> Here are the Time Sheet details for your approval:" _
                                   & "<br /> <br /> <br /> <table> <tr><td style=""width:50px"">Project: </td> <td style=""width:42px""> </td><td style=""width:50px"">" & projecttitle _
                                   & "</td> </tr> <tr><td style=""width:100px"">Client: </td> <td style=""width:42px""> </td><td style=""width:250px"">" & clientname & _
                                   "</td> </tr>	<tr><td style=""width:100px"">Employee: </td> <td style=""width:42px""> </td><td style=""width:250px"">" & employee _
                                   & "</td> </tr> <tr><td style=""width:100px"">Activity Date: </td> <td style=""width:42px""> </td><td style=""width:250px"">" & activityDate _
                                   & "</td> </tr> <tr><td style=""width:100px"">Activity End Date: </td> <td style=""width:42px""> </td><td style=""width:250px"">" & activityEndDate _
                                   & "</td> </tr>	<tr><td style=""width:100px"">Time In: </td> <td style=""width:42px""> </td><td style=""width:250px"">" & Process.AMPM_Time(starttime.ToString) _
                                   & "</td> </tr> <tr><td style=""width:100px"">Time Out: </td> <td style=""width:42px""> </td><td style=""width:250px"">" & Process.AMPM_Time(endtime.ToString) _
                                   & "</td> </tr> </table> <br /> <br /> <br />To update approval status, please login to " & Process.AppName & "</div>", _
                                   "", True)
            End If

        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
        End Try
    End Sub
    Public Shared Sub HR_TimeSheet_Approval(ByVal hremail As String, ByVal pmName As String, ByVal projecttitle As String, ByVal clientname As String, ByVal employee As String, ByVal activityDate As Date, ByVal starttime As String, ByVal endtime As String, ByVal pmstatus As String, ByVal pmcomment As String, sempid As String, mgrid As String)
        Try
            msgbuild.Clear()
            msgbuild.AppendLine("Dear HR Manager,")
            msgbuild.AppendLine(" ")
            msgbuild.AppendLine("Here are the Time Sheet details for HR Documentation:")
            msgbuild.AppendLine(" ")
            msgbuild.AppendLine(" ")
            msgbuild.AppendLine("Project    : " & projecttitle)
            msgbuild.AppendLine("Client     : " & clientname)
            msgbuild.AppendLine("Project Mgr: " & pmName)
            msgbuild.AppendLine("Employee   : " & employee)
            msgbuild.AppendLine("Date In    : " & activityDate.ToLongDateString)
            msgbuild.AppendLine("Time In    : " & Process.AMPM_Time(starttime.ToString))
            msgbuild.AppendLine("Time Out   : " & Process.AMPM_Time(endtime.ToString))
            msgbuild.AppendLine("PM Approval: " & pmstatus)
            If pmcomment.Trim <> "" Then
                msgbuild.AppendLine("PM Comment : " & pmcomment)
            End If

            Process.MailNotification("0", MailTimeSheet, "HR Approval for Time Sheet " & projecttitle & " of " & activityDate & " by " & employee, msgbuild.ToString, HRTeam, mgrid, sempid, sempid, "")
            Dim Arrays() As String = Process.GetEmpIDMailList("hr").Split(SeparatorSemi, StringSplitOptions.RemoveEmptyEntries)
            For i = 0 To Arrays.Count - 1
                Process.MailNotification("0", MailTimeSheet, "HR Approval for Time Sheet " & projecttitle & " of " & activityDate & " by " & employee, msgbuild.ToString, HRTeam, mgrid, sempid, Arrays(i), "")
            Next

            If SendNotification() = "Yes" Then
                Process.SendEmail("", Process.AppName, hremail, _
                                   "HR Approval for Time Sheet " & projecttitle & " of " & activityDate & " by " & employee, _
                                   "<div style=""font-family:Arial; font-size:12px;"">Dear HR Manager, <br /> <br />" _
                                   & "<br /> Here are the Time Sheet details:" _
                                   & "<br /> <br /> <br /> <table> <tr><td style=""width:50px"">Project: </td> <td style=""width:42px""> </td><td style=""width:50px"">" & projecttitle _
                                   & "</td> </tr> <tr><td style=""width:100px"">Client: </td> <td style=""width:42px""> </td><td style=""width:250px"">" & clientname & _
                                   "</td> </tr>	<tr><td style=""width:100px"">Employee: </td> <td style=""width:42px""> </td><td style=""width:250px"">" & employee _
                                   & "</td> </tr> <tr><td style=""width:100px"">Project Manager: </td> <td style=""width:42px""> </td><td style=""width:250px"">" & pmName _
                                   & "</td> </tr> <tr><td style=""width:100px"">Activity Date: </td> <td style=""width:42px""> </td><td style=""width:250px"">" & activityDate _
                                   & "</td> </tr>	<tr><td style=""width:100px"">Time In: </td> <td style=""width:42px""> </td><td style=""width:250px"">" & Process.AMPM_Time(starttime.ToString) _
                                   & "</td> </tr> <tr><td style=""width:100px"">Time Out: </td> <td style=""width:42px""> </td><td style=""width:250px"">" & Process.AMPM_Time(endtime.ToString) _
                                   & "</td> </tr> <tr><td style=""width:100px"">PM Approval Status: </td> <td style=""width:42px""> </td><td style=""width:250px"">" & pmstatus _
                                   & "</td> </tr> <tr><td style=""width:100px"" valign=""top"">PM Comment: </td> <td style=""width:42px""> </td><td style=""width:250px"">" & pmcomment _
                                   & "</td> </tr> </table> <br /> <br /> <br />To update HR Approval status, please login to " & Process.AppName & "</div>", _
                                   "", True)
            End If

        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
        End Try
    End Sub
#End Region
#Region "Payroll Notification"
    'Payroll Mails
    Public Shared Sub Payroll_Notification(ByVal hremail As String, ByVal period As String, ByVal datecreated As String, ByVal netpay As String, sempid As String, mgrid As String, links As String)
        Try
            msgbuild.Clear()
            msgbuild.AppendLine("Payroll " & period & " requires approval from an authorised Payroll Approver Member")
            msgbuild.AppendLine(" ")
            msgbuild.AppendLine("Below is the detail of payroll " & period & " which requires a member's approver:")
            msgbuild.AppendLine(" ")
            msgbuild.AppendLine("Payroll Period : " & period)
            msgbuild.AppendLine("Date Generated : " & datecreated)
            msgbuild.AppendLine("Total Net Pay  : " & netpay)

            Dim ArrayMgrs() As String = mgrid.Split(SeparatorSemi, StringSplitOptions.RemoveEmptyEntries)
            For i = 0 To ArrayMgrs.Count - 1
                Process.MailNotification(period, MailPayroll, "Payroll  " & period & " requires approval", msgbuild.ToString, mgrid, Process.AppName, "", ArrayMgrs(i), links)
            Next

            If SendNotification() = "Yes" Then
                Process.SendEmail("", Process.AppName, hremail, _
                                     "Payroll  " & period & " requires approval", _
                                     "<div style=""font-family:Arial; font-size:12px;"">" _
                                     & "Payroll " & period & " requires approval from an authorised Payroll Approver Member" _
                                     & "<br/> Below is the detail of payroll " & period & ":" _
                                     & "<br /> <br /> <br /> <table> <tr><td style=""width:50px"">Payroll Period: </td> <td style=""width:42px""> </td><td style=""width:50px"">" & period _
                                     & "</td> </tr> <tr><td style=""width:100px"">Date Generated: </td> <td style=""width:42px""> </td><td style=""width:250px"">" & datecreated & _
                                     "</td> </tr>	<tr><td style=""width:100px"">Total Net Pay: </td> <td style=""width:42px""> </td><td style=""width:250px"">" & netpay _
                                      & "</td> </tr> </table> <br /> <br /> <br />To update Approval status, please login to Finance Module on " & Process.AppName & " <br /> <br /><a href=" & links & ">" & links & "</a> </div>", _
                                     "", True)

                '"<div style=""font-family:Arial; font-size:12px;"">" & msgbuild.ToString & "<br /> <br /><a href=" & links & ">" & links & "</a> </div>", _
                '                       "", True, copyMail)
            End If

        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
        End Try
    End Sub
    Public Shared Sub Payroll_Approval(ByVal company As String, ByVal hremail As String, ByVal period As String, ByVal approvalstat As String, ByVal approver As String, ByVal dateapproved As String, ByVal comment As String, payment As String, sempid As String, mgrid As String)
        Try
            msgbuild.Clear()
            msgbuild.AppendLine(company & " Payroll " & period & " " & approvalstat)
            msgbuild.AppendLine(" ")
            msgbuild.AppendLine("Below is the detail of payroll " & period & ":")
            msgbuild.AppendLine(" ")
            msgbuild.AppendLine("Company        : " & company)
            msgbuild.AppendLine("Payroll Period : " & period)
            msgbuild.AppendLine("Total Net Pay  : " & payment)
            msgbuild.AppendLine("Approved By    : " & approver)
            msgbuild.AppendLine("Approval Stat  : " & approvalstat)
            msgbuild.AppendLine("Approval Date  : " & dateapproved)
            If comment.Trim <> "" Then
                msgbuild.AppendLine("Comment        : " & comment)
            End If

            Dim ArrayMgrs() As String = mgrid.Split(SeparatorSemi, StringSplitOptions.RemoveEmptyEntries)
            For i = 0 To ArrayMgrs.Count - 1
                Process.MailNotification(period, MailPayroll, "Payroll  " & period & " " & approvalstat, msgbuild.ToString, mgrid, sempid, "", ArrayMgrs(i), "")
            Next

            If SendNotification() = "Yes" Then
                Process.SendEmail("", Process.AppName, hremail, _
                                   company & " Payroll  " & period & ": " & approvalstat, _
                                   "<div style=""font-family:Arial; font-size:12px;"">" _
                                   & "Below is the approval detail of payroll " & period & ":" _
                                   & "<br /> <br /> <br /> <table> <tr><td style=""width:50px"">Payroll Period: </td> <td style=""width:42px""> </td><td style=""width:50px"">" & period _
                                   & "</td> </tr> <tr><td style=""width:100px"">Total Net Pay: </td> <td style=""width:42px""> </td><td style=""width:250px"">" & payment _
                                   & "</td> </tr> <tr><td style=""width:100px"">Approved By: </td> <td style=""width:42px""> </td><td style=""width:250px"">" & approver _
                                  & "</td> </tr>	<tr><td style=""width:100px"">Approval Stat: </td> <td style=""width:42px""> </td><td style=""width:250px"">" & approvalstat _
                                   & "</td> </tr> <tr><td style=""width:100px"">Date Approval: </td> <td style=""width:42px""> </td><td style=""width:250px"">" & dateapproved _
                                   & "</td> </tr> <tr><td style=""width:100px"">Comment: </td> <td style=""width:42px""> </td><td style=""width:250px"">" & comment _
                                   & "</td> </tr> </table> </div>", _
                                   "", True)
            End If


        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
        End Try

    End Sub
    Public Shared Sub Payslip(ByVal sempid As String, ByVal email As String, ByVal empname As String, ByVal period As String, ByVal files As String)
        Try
            MailNotification(sempid, MailPayroll, "Payslip for period for " & period, "Kindly find attached your paylip of " & period, sempid, HRTeam, "", sempid, "", files)

            Process.SendEmail("", Process.AppName, email, _
                                  "Payslip for period for " & period, _
                                  "<div style=""font-family:Arial; font-size:12px;"">Dear " & empname & ", <br /> <br />Kindly find attached your paylip of " & period _
                                  & " </div>", _
                                  files, True)
        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
        End Try

    End Sub

    'Payroll Adjustments
    Public Shared Sub Payroll_Adjustment_Notification(ByVal hremail As String, ByVal transref As String, ByVal title As String, ByVal amount As Double, ByVal paydate As String, ByVal employee As String, ByVal adjType As String, grade As String, jobtitle As String, dept As String, locat As String, sempid As String, mgrid As String)
        Try
            msgbuild.Clear()
            msgbuild.AppendLine("Below is the detail of payroll adjustment:")
            msgbuild.AppendLine(" ")
            msgbuild.AppendLine("Ref Number : " & transref)
            msgbuild.AppendLine("Employee   : " & employee)
            msgbuild.AppendLine("Grade      : " & grade)
            msgbuild.AppendLine("Job Title  : " & jobtitle)
            msgbuild.AppendLine("Dept/Unit  : " & dept)
            msgbuild.AppendLine("Location   : " & locat)
            msgbuild.AppendLine("Amount     : " & FormatNumber(amount, 2))
            msgbuild.AppendLine("Adjustment : " & adjType)
            msgbuild.AppendLine("Pay Date   : " & paydate)
            If title.Trim <> "" Then
                msgbuild.AppendLine("Narration  : " & title)
            End If

            Dim ArrayMgrs() As String = mgrid.Split(SeparatorSemi, StringSplitOptions.RemoveEmptyEntries)
            For i = 0 To ArrayMgrs.Count - 1
                Process.MailNotification(transref, MailPayroll, "Payroll Adjustment " & transref & " requires approval", msgbuild.ToString, mgrid, sempid, "", ArrayMgrs(i), "")
            Next

            If SendNotification() = "Yes" Then
                Process.SendEmail("", Process.AppName, hremail, _
                                    "Payroll Adjustment " & transref & " requires approval", _
                                    "<div style=""font-family:Arial; font-size:12px;"">" _
                                    & "Below is the detail of payroll adjustment:" _
                                    & "<br /> <br /> <br /> <table> <tr><td style=""width:50px"">Reference Number: </td> <td style=""width:42px""> </td><td style=""width:50px"">" & transref _
                                    & "</td> </tr> <tr><td style=""width:100px"">Employee: </td> <td style=""width:42px""> </td><td style=""width:250px"">" & employee _
                                    & "</td> </tr> <tr><td style=""width:100px"">Grade: </td> <td style=""width:42px""> </td><td style=""width:250px"">" & grade _
                                    & "</td> </tr> <tr><td style=""width:100px"">Job Title: </td> <td style=""width:42px""> </td><td style=""width:250px"">" & jobtitle _
                                     & "</td> </tr> <tr><td style=""width:100px"">Office/Department: </td> <td style=""width:42px""> </td><td style=""width:250px"">" & dept _
                                      & "</td> </tr> <tr><td style=""width:100px"">Location: </td> <td style=""width:42px""> </td><td style=""width:250px"">" & locat _
                                    & "</td> </tr> <tr><td style=""width:100px"">Amount: </td> <td style=""width:42px""> </td><td style=""width:250px"">" & FormatNumber(amount, 2) _
                                     & "</td> </tr> <tr><td style=""width:100px"">Adjustment Type: </td> <td style=""width:42px""> </td><td style=""width:250px"">" & adjType _
                                   & "</td> </tr>	<tr><td style=""width:100px"">Narration: </td> <td style=""width:42px""> </td><td style=""width:250px"">" & title _
                                    & "</td> </tr> <tr><td style=""width:100px"">Pay Date: </td> <td style=""width:42px""> </td><td style=""width:250px"">" & paydate _
                                    & "</td> </tr> </table> </div>", "", True)
            End If

        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
        End Try
    End Sub
#End Region
#Region "Performance Notification"
    'Performance
    Public Shared Function Appraisal_Objective_Alert(sempid As String, semail As String, appstartdate As String, links As String) As Boolean
        Try
            Dim subject As String = "Objective Alert for review cycle starting " & appstartdate
            Dim filePath As String = MailContentURL & MailPerfromance & "Objective_Alert_notification.txt"

            Dim Arrays() As String = sempid.Split(SeparatorSemi, StringSplitOptions.RemoveEmptyEntries)
            Dim ArraysMail() As String = semail.Split(SeparatorSemi, StringSplitOptions.RemoveEmptyEntries)
            For i = 0 To Arrays.Count - 1
                Dim rname As String = GetEmployeeData(Arrays(i), "firstname")
                Dim readertxt As New StreamReader(filePath)
                msgbuild.Clear()
                While Not readertxt.EndOfStream
                    Dim line As String = readertxt.ReadLine()
                    msgbuild.AppendLine(line)
                End While
                readertxt.Close()
                msgbuild = msgbuild.Replace("@empname", rname).Replace("@startdate", appstartdate).Replace("@company", GetCompanyByEmpID(sempid)).Replace("@empid", sempid)

                Process.MailNotification(Arrays(i), MailPerfromance, subject, msgbuild.ToString, Arrays(i), Process.AppName, "", Arrays(i), links, "")

                If ArraysMail(i) <> "aa" Then
                    If SendNotification() = "Yes" Then
                        Process.SendEmail("", GetCompanyByEmpID(Arrays(i)), ArraysMail(i),
                                         subject,
                                           "<div style=""font-family:Arial; font-size:12px;"">" & msgbuild.ToString & "<br /> <br /><a href=" & links & ">" & links & "</a> </div>",
                                        "", True, "")

                    End If
                End If

            Next
            Return True


        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
            Return False
        End Try
    End Function
    Public Shared Function Coaching_Alert(empid As String, email As String, Coachname As String, appstartdate As String, links As String, time As String) As Boolean
        Try
            Dim subject As String = "Coaching Session" & appstartdate
            Dim filePath As String = MailContentURL & MailFolderPerformance & "Coaching_Alert.txt"




            Dim rname As String = GetEmployeeData(empid, "firstname")
            Dim readertxt As New StreamReader(filePath)
            msgbuild.Clear()
            While Not readertxt.EndOfStream
                Dim line As String = readertxt.ReadLine()
                msgbuild.AppendLine(line)
            End While
            readertxt.Close()
            msgbuild = msgbuild.Replace("@empname", rname).Replace("@startdate", appstartdate).Replace("@Coach", Coachname).Replace("@Time", time).Replace("@company", GetCompanyByEmpID(empid)).Replace("@empid", empid)

            Process.MailNotification(empid, MailPerfromance, subject, msgbuild.ToString, empid, Process.AppName, "", empid, links, "")

            If email <> "aa" Then
                If SendNotification() = "Yes" Then
                    Process.SendEmail("", GetCompanyByEmpID(empid), email,
                                         subject,
                                           "<div style=""font-family:Arial; font-size:12px;"">" & msgbuild.ToString & "<br /> <br /><a href=" & links & ">" & links & "</a> </div>",
                                        "", True, "")

                End If
            End If


            Return True


        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
            Return False
        End Try
    End Function
    Public Shared Function Asset_Return_Request(ByVal certname As String, ByVal receivermail As String, ByVal employee As String, ByVal certtype As String, ByVal copyMail As String, sempid As String, mgrid As String, links As String) As Boolean
        Try
            msgbuild1.Clear()
            msgbuild1.AppendLine("Dear " & employee & ",")
            msgbuild1.AppendLine("")
            msgbuild1.AppendLine("")
            msgbuild1.AppendLine("Your  " & certtype & " with" & certtype & " has been forwarded to HR for acceptance")
            msgbuild1.AppendLine(" ")

            Dim subject As String = "New  Asset :" & certtype & " Returned by " & employee
            Dim filePath As String = MailContentURL & MailFolderPerformance & "Asset_Alert.txt"
            Dim readertxt As New StreamReader(filePath)
            msgbuild.Clear()
            While Not readertxt.EndOfStream
                Dim line As String = readertxt.ReadLine()
                msgbuild.AppendLine(line)
            End While
            readertxt.Close()
            msgbuild = msgbuild.Replace("@empname", GetEmployeeData(sempid, "fullname")).Replace("@mgrname", GetEmployeeData(mgrid, "fullname")).Replace("@company", GetCompanyByEmpID(mgrid)).Replace("@certtype", certtype) _
                .Replace("@certname", certname).Replace("@empid", sempid)

            Process.MailNotification(sempid, MailQualification, "New " & certtype & " added by " & employee, msgbuild1.ToString, sempid, Process.AppName, "", sempid, "", "")

            Dim Arrays() As String = Process.GetEmpIDMailList("hr").Split(SeparatorSemi, StringSplitOptions.RemoveEmptyEntries)
            For i = 0 To Arrays.Count - 1
                Process.MailNotification(sempid, MailQualification, subject, msgbuild.ToString, sempid, HRTeam, "", Arrays(i), links)
            Next

            If SendNotification() = "Yes" Then
                'Notification for requester
                Process.SendEmail("", GetEmployeeData(sempid, "company"), GetMailList("hr"),
                                     subject, "<div style=""font-family:Arial; font-size:12px;"">" & msgbuild.ToString & " <br /> <br /><a href=" & links & ">" & links & "</a> <br /> <br />" & GetEmployeeData(sempid, "company") & "</div>", "", True)
            End If
            Return True

        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
            Return False
        End Try
    End Function

    Public Shared Function Appraisal_Feedback_Alert(sempid As String, semail As String, appenddate As String, links As String) As Boolean
        Try
            Dim subject As String = "Appraisal Feedback Alert for review cycle ending " & appenddate & " is due"
            Dim filePath As String = MailContentURL & MailPerfromance & "Feedback_Alert_notification.txt"

            Dim Arrays() As String = sempid.Split(SeparatorSemi, StringSplitOptions.RemoveEmptyEntries)
            Dim ArraysMail() As String = semail.Split(SeparatorSemi, StringSplitOptions.RemoveEmptyEntries)
            For i = 0 To Arrays.Count - 1
                Dim rname As String = GetEmployeeData(Arrays(i), "firstname")
                Dim readertxt As New StreamReader(filePath)
                msgbuild.Clear()
                While Not readertxt.EndOfStream
                    Dim line As String = readertxt.ReadLine()
                    msgbuild.AppendLine(line)
                End While
                readertxt.Close()
                msgbuild = msgbuild.Replace("@empname", rname).Replace("@enddate", appenddate).Replace("@company", GetCompanyByEmpID(sempid)).Replace("@empid", sempid)

                Process.MailNotification(Arrays(i), MailPerfromance, subject, msgbuild.ToString, Arrays(i), Process.AppName, "", Arrays(i), links, "")

                If ArraysMail(i) <> "aa" Then
                    If SendNotification() = "Yes" Then
                        Process.SendEmail("", GetCompanyByEmpID(Arrays(i)), ArraysMail(i), _
                                         subject, _
                                           "<div style=""font-family:Arial; font-size:12px;"">" & msgbuild.ToString & "<br /> <br /><a href=" & links & ">" & links & "</a> </div>", _
                                        "", True, "")

                    End If
                End If

            Next
            Return True
        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
            Return False
        End Try
    End Function
    Public Shared Sub Development_Plan_Completion(ByVal mailid As String, ByVal empname As String, ByVal devplan As String, sempid As String, mgrid As String, links As String)
        Try
            Dim subject As String = empname & "'s " & devplan & " Development Plan is complete for review"
            Dim line1 As String = empname & "'s Development Plan for " & devplan & " has been marked completed and ready for review"
            Dim line2 As String = "Kindly review plan before AGREEING"
            msgbuild.Clear()
            msgbuild.AppendLine(line1)
            msgbuild.AppendLine(" ")
            msgbuild.AppendLine(line2)

            Process.MailNotification(mailid, MailPerfromance, subject, msgbuild.ToString, mgrid, sempid, "", mgrid, links)

            If SendNotification() = "Yes" Then
                Process.SendEmail("", Process.AppName, GetEmailAddress(mgrid), _
                                 subject, _
                                 "<div style=""font-family:Arial; font-size:12px;"">" _
                                 & line1 & "<br /> <br /> " & line2 & _
                                  "</div>", "", True)
            End If

        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
        End Try
    End Sub
    Public Shared Sub Development_Plan_Agreed(ByVal mailid As String, ByVal coachname As String, ByVal devplan As String, empname As String, sempid As String, mgrid As String, links As String)
        Try
            Dim subject As String = devplan & " Development Plan has been signed AGREED"
            Dim line1 As String = devplan & " Development Plan has been signed AGREED by  " & coachname
            msgbuild.Clear()
            msgbuild.AppendLine("")
            msgbuild.AppendLine("Dear " & empname & ",")
            msgbuild.AppendLine("")
            msgbuild.AppendLine(line1)


            Process.MailNotification(mailid, MailPerfromance, subject, msgbuild.ToString, sempid, Process.AppName, "", sempid, links)

            If SendNotification() = "Yes" Then
                Process.SendEmail("", Process.AppName, GetEmailAddress(sempid), _
                                    subject, _
                                     "<div style=""font-family:Arial; font-size:12px;""> Dear " & empname & "," _
                                     & "<br/> <br/>" & line1 _
                                     & "</div><br/> <br/> <a href=" & links & ">" & links & "</a>", "", True)
            End If
        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
        End Try
    End Sub
    Public Shared Sub Development_Plan_Agreed_To_HR(ByVal mailid As String, ByVal coachname As String, ByVal devplan As String, empname As String, dept As String, sempid As String, mgrid As String, links As String)
        Try
            Dim subject As String = empname & " Development Plan " & devplan & " has been signed AGREED"

            Dim line1 As String = "Employee : " & empname
            Dim line2 As String = "Unit/Dept: " & dept
            Dim line3 As String = devplan & " Development Plan has been signed AGREED by  " & coachname
            msgbuild.Clear()

            msgbuild.AppendLine("")
            msgbuild.AppendLine(line1)
            msgbuild.AppendLine("")
            msgbuild.AppendLine(line2)
            msgbuild.AppendLine("")
            msgbuild.AppendLine(line3)

            Dim Arrays() As String = Process.GetEmpIDMailList("hr").Split(SeparatorSemi, StringSplitOptions.RemoveEmptyEntries)
            For i = 0 To Arrays.Count - 1
                Process.MailNotification(mailid, MailPerfromance, subject, msgbuild.ToString, HRTeam, Process.AppName, "", Arrays(i), links)
            Next

            If SendNotification() = "Yes" Then
                Process.SendEmail("", Process.AppName, GetMailList("hr"), _
                                    subject, _
                                     "<div style=""font-family:Arial; font-size:12px;"">" _
                                     & line1 & "<br/> <br/>" & line2 & "<br/> <br/>" & line3 _
                                     & "</div> <br/> <br/> <a href=" & links & ">" & links & "</a>", "", True)
            End If
        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
        End Try
    End Sub
    Public Shared Sub Appraisal_Obj_updated(ByVal coachname As String, empname As String, ByVal appperiod As String, sempid As String, mgrid As String, links As String)
        Try
            Dim subject As String = "Performance Objective(" + appperiod + ") reopened by  " & coachname
            Dim line0 As String = "Dear " & empname & ","
            Dim line1 As String = coachname & " has reopen your objective form, kindly review and contact your Line Manager if necessary"
            msgbuild.Clear()
            msgbuild.AppendLine(" ")
            msgbuild.AppendLine(line0)
            msgbuild.AppendLine(" ")
            msgbuild.AppendLine(line1)

            Process.MailNotification(appperiod, MailPerfromance, subject, msgbuild.ToString, sempid, mgrid, "", sempid, links)

            If SendNotification() = "Yes" Then
                Process.SendEmail("", Process.AppName, GetEmailAddress(sempid), _
                                subject, _
                                "<div style=""font-family:Arial; font-size:12px;"">" & line0 & "<br /> <br />" _
                                & line1 & "<br /> <br /> Kindly review objectives with Employee before AGREEING to plan " & _
                                 "</div> <br/> <br/> <a href=" & links & ">" & links & "</a>", "", True)
            End If

        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
        End Try
    End Sub

    Public Shared Sub Appraisal_Obj_Disagree(ByVal coachname As String, empname As String, ByVal appperiod As String, sempid As String, mgrid As String, links As String)
        Try
            Dim subject As String = coachname & " disagrees with your " & appperiod & " Appraisal Objectives"
            Dim line0 As String = "Dear " & empname & ","
            Dim line1 As String = coachname & " disagrees with your objectives, kindly review and contact your Line Manager if necessary"
            msgbuild.Clear()
            msgbuild.AppendLine(" ")
            msgbuild.AppendLine(line0)
            msgbuild.AppendLine(" ")
            msgbuild.AppendLine(line1)

            Process.MailNotification(appperiod, MailPerfromance, subject, msgbuild.ToString, sempid, mgrid, "", sempid, links)

            If SendNotification() = "Yes" Then
                Process.SendEmail("", Process.AppName, GetEmailAddress(sempid), _
                                subject, _
                                "<div style=""font-family:Arial; font-size:12px;"">" & line0 & "<br /> <br />" _
                                & line1 & "<br /> <br /> Kindly review objectives with Employee before AGREEING to plan " & _
                                 "</div> <br/> <br/> <a href=" & links & ">" & links & "</a>", "", True)
            End If

        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
        End Try
    End Sub

    Public Shared Sub Appraisal_Obj_UpdateKPI(ByVal coachname As String, ByVal empname As String, ByVal appperiod As String, sempid As String, mgrid As String, links As String)
        Try
            Dim subject As String = empname & "'s " & appperiod & "Performance Objective Updates"
            Dim line0 As String = "Dear " & coachname & ","
            Dim line1 As String = empname & "'s Performance Objective for " & appperiod & " has been updated."
            Dim line2 As String = "Kindly review the objectives."

            msgbuild.Clear()
            msgbuild.AppendLine(" ")
            msgbuild.AppendLine(line0)
            msgbuild.AppendLine(" ")
            msgbuild.AppendLine(line1)
            msgbuild.AppendLine(" ")
            msgbuild.AppendLine(line2)

            Process.MailNotification(appperiod, MailPerfromance, subject, msgbuild.ToString, mgrid, sempid, "", mgrid, links)

            If SendNotification() = "Yes" Then
                Process.SendEmail("", Process.AppName, GetEmailAddress(mgrid), _
                                subject, _
                                "<div style=""font-family:Arial; font-size:12px;"">" & line0 & " <br /> <br />" _
                                & line1 & " <br /> <br /> " & line2 & _
                                 "<br /> <br /> <a href=" & links & ">" & links & "</a></div>", "", True)
            End If

        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
        End Try
    End Sub

    Public Shared Sub Appraisal_Obj_Completion(ByVal coachname As String, ByVal empname As String, ByVal appperiod As String, sempid As String, mgrid As String, links As String)
        Try
            Dim subject As String = empname & "'s " & appperiod & "Performance Objective is Completed"
            Dim line0 As String = "Dear " & coachname & ","
            Dim line1 As String = empname & "'s Performance Objective for " & appperiod & " has been marked completed and ready for your review."
            Dim line2 As String = "Kindly review objective before marking as AGREED."

            msgbuild.Clear()
            msgbuild.AppendLine(" ")
            msgbuild.AppendLine(line0)
            msgbuild.AppendLine(" ")
            msgbuild.AppendLine(line1)
            msgbuild.AppendLine(" ")
            msgbuild.AppendLine(line2)

            Process.MailNotification(appperiod, MailPerfromance, subject, msgbuild.ToString, mgrid, sempid, "", mgrid, links)

            If SendNotification() = "Yes" Then
                Process.SendEmail("", Process.AppName, GetEmailAddress(mgrid), _
                                subject, _
                                "<div style=""font-family:Arial; font-size:12px;"">" & line0 & " <br /> <br />" _
                                & line1 & " <br /> <br /> " & line2 & _
                                 "<br /> <br /> <a href=" & links & ">" & links & "</a></div>", "", True)
            End If

        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
        End Try
    End Sub
    Public Shared Sub Appraisal_Obj_Agreed(ByVal coachname As String, ByVal empname As String, ByVal appperiod As String, sempid As String, mgrid As String, links As String)
        Try
            Dim subject As String = "Your " & appperiod & " Performance Objective is Marked Discussed and Agreed"
            Dim line0 As String = "Dear " & empname & ","
            Dim line1 As String = "Your Performance Objective for " & appperiod & " has been marked Discussed and Agreed by" & coachname

            msgbuild.Clear()
            msgbuild.AppendLine(" ")
            msgbuild.AppendLine(line0)
            msgbuild.AppendLine(" ")
            msgbuild.AppendLine(line1)


            Process.MailNotification(appperiod, MailPerfromance, subject, msgbuild.ToString, sempid, mgrid, "", sempid, links)

            If SendNotification() = "Yes" Then
                Process.SendEmail("", Process.AppName, GetEmailAddress(sempid), _
                                subject, _
                                "<div style=""font-family:Arial; font-size:12px;"">" & line0 & " <br /> <br />" _
                                & line1 & _
                                 "<br /> <br /> <a href=" & links & ">" & links & "</a></div>", "", True)
            End If

        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
        End Try
    End Sub

    Public Shared Sub Appraisal_Review_Disagree(ByVal sempid As String, ByVal appperiod As String, empname As String, comment As String, reviewer As String, reviewerid As String, reviewer2 As String, reviewerid2 As String, links As String)
        Try
            Dim subject As String = reviewer2 & " disagrees with " & appperiod & " Performance Feedback Review for " & empname
            Dim line1 As String = "Dear " & reviewer & ","
            Dim line2 As String = reviewer2 & " disagrees with " & appperiod & " Performance Feedback Review for " & empname
            Dim line3 As String = "Find below comment concerning the disagree:"
            Dim line4 As String = comment

            msgbuild.Clear()
            msgbuild.AppendLine(" ")
            msgbuild.AppendLine(line1)
            msgbuild.AppendLine(" ")
            msgbuild.AppendLine(line2)
            msgbuild.AppendLine(" ")
            msgbuild.AppendLine(line3)
            msgbuild.AppendLine(line4)

            Process.MailNotification(sempid, MailPerfromance, subject, msgbuild.ToString, reviewerid, reviewerid2, "", reviewerid, links)

            If SendNotification() = "Yes" Then
                Process.SendEmail("", Process.AppName, GetEmailAddress(reviewerid), _
                                subject, _
                                "<div style=""font-family:Arial; font-size:12px;"">" & line1 & " <br /> <br />" _
                                & line2 & "<br /> <br />" & line3 & " <br /> " & line4 & " <br /> <br /> <a href=" & links & ">" & links & "</a></div>", "", True, GetEmailAddress(reviewerid2))
            End If

        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
        End Try
    End Sub

    Public Shared Sub Appraisal_Review_Submit(ByVal sempid As String, ByVal appperiod As String, empname As String, reviewer As String, reviewerid As String, links As String)
        Try
            Dim subject As String = empname & " Performance Feedback for " & appperiod & " is Completed and Ready for your Review"
            Dim line1 As String = "Dear " & reviewer & ","
            Dim line2 As String = empname & "'s Performance feedback for " & appperiod & " is ready for your review"
            Dim line3 As String = "Kindly review feedback and rate " & empname & "'s performance for the period"
            msgbuild.Clear()
            msgbuild.AppendLine(" ")
            msgbuild.AppendLine(line1)
            msgbuild.AppendLine(" ")
            msgbuild.AppendLine(line2)
            msgbuild.AppendLine(" ")
            msgbuild.AppendLine(line3)

            Process.MailNotification(sempid, MailPerfromance, subject, msgbuild.ToString, reviewerid, Process.AppName, "", reviewerid, links)

            If SendNotification() = "Yes" Then
                Process.SendEmail("", Process.AppName, GetEmailAddress(reviewerid), _
                                subject, _
                                "<div style=""font-family:Arial; font-size:12px;"">" & line1 & "<br /> <br />" _
                                & line2 & ". <br /> <br />" & line3 & "<br /> <br /> <a href=" & links & ">" & links & "</a>" & _
                                 "</div>", "", True)
            End If

        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
        End Try
    End Sub

    Public Shared Sub Appraisal_Nugget_Submit(ByVal sempid As String, ByVal appperiod As String, empname As String, revname As String, reviewer As String, reviewerid As String, links As String)
        Try
            Dim subject As String = revname & " has just completed a feedback kudos for " & empname & ""
            Dim line1 As String = "Dear " & GetEmployeeData(reviewerid, "Firstname") & ","
            Dim line2 As String = revname & " has just completed a feedback kudos for " + empname + ". Thanks."
            msgbuild.Clear()
            msgbuild.AppendLine(" ")
            msgbuild.AppendLine(line1)
            msgbuild.AppendLine(" ")
            msgbuild.AppendLine(line2)

            Process.MailNotification(sempid, MailPerfromance, subject, msgbuild.ToString, reviewerid, Process.AppName, "", reviewerid, links)

            If SendNotification() = "Yes" Then
                Process.SendEmail("", Process.AppName, GetEmailAddress(reviewerid), _
                                subject, _
                                "<div style=""font-family:Arial; font-size:12px;"">" & line1 & "<br /> <br />" _
                                & line2 & ". <br /> <br /> <a href=" & links & ">" & links & "</a>" & _
                                 "</div>", "", True)
            End If

        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
        End Try
    End Sub

    Public Shared Sub Appraisal_Nugget_Emp_Submit(ByVal sempid As String, ByVal appperiod As String, empname As String, revname As String, reviewer As String, reviewerid As String, links As String)
        Try
            Dim subject As String = revname & " has just completed a feedback kudos for you"
            Dim line1 As String = "Dear " & empname & ","
            Dim line2 As String = "Congratulations, " & revname & " has just completed a feedback kudos for you. Thanks "
            msgbuild.Clear()
            msgbuild.AppendLine(" ")
            msgbuild.AppendLine(line1)
            msgbuild.AppendLine(" ")
            msgbuild.AppendLine(line2)

            Process.MailNotification(sempid, MailPerfromance, subject, msgbuild.ToString, reviewerid, Process.AppName, "", reviewerid, links)

            If SendNotification() = "Yes" Then
                Process.SendEmail("", Process.AppName, GetEmailAddress(reviewerid), _
                                subject, _
                                "<div style=""font-family:Arial; font-size:12px;"">" & line1 & "<br /> <br />" _
                                & line2 & ". <br /> <br /> <a href=" & links & ">" & links & "</a>" & _
                                 "</div>", "", True)
            End If

        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
        End Try
    End Sub

    Public Shared Sub Appraisal_Nugget_Rev_Submit(ByVal sempid As String, ByVal appperiod As String, empname As String, revname As String, reviewer As String, reviewerid As String, links As String)
        Try
            Dim subject As String = "You have just completed a feedback kudos for " & empname
            Dim line1 As String = "Dear " & revname & ","
            Dim line2 As String = "Congratulations, you have just completed a feedback kudos for " & empname & ". Thanks"
            msgbuild.Clear()
            msgbuild.AppendLine(" ")
            msgbuild.AppendLine(line1)
            msgbuild.AppendLine(" ")
            msgbuild.AppendLine(line2)

            Process.MailNotification(sempid, MailPerfromance, subject, msgbuild.ToString, reviewerid, Process.AppName, "", reviewerid, links)

            If SendNotification() = "Yes" Then
                Process.SendEmail("", Process.AppName, GetEmailAddress(reviewerid), _
                                subject, _
                                "<div style=""font-family:Arial; font-size:12px;"">" & line1 & "<br /> <br />" _
                                & line2 & ". <br /> <br /> <a href=" & links & ">" & links & "</a>" & _
                                 "</div>", "", True)
            End If

        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
        End Try
    End Sub

    Public Shared Sub Appraisal_Cycle_End(ByVal sempid As String, ByVal appperiod As String, empname As String, dept As String, links As String)
        Try
            Dim subject As String = " Congratulations " + GetEmployeeData(sempid, "Firstname") & ", you can now proceed with your feedback.  "

            Dim line0 As String = "Dear " & GetEmployeeData(sempid, "Firstname") & ","
            Dim line1 As String = "Congratulations " + empname + ", you can now proceed with your feedback.Thanks "

            msgbuild.Clear()
            msgbuild.AppendLine(" ")
            msgbuild.AppendLine(line0)
            msgbuild.AppendLine(" ")
            msgbuild.AppendLine(line1)

           
            Process.MailNotification(sempid, MailPerfromance, subject, msgbuild.ToString, sempid, Process.AppName, "", sempid, links)

            If SendNotification() = "Yes" Then
                Process.SendEmail("", Process.AppName, GetEmailAddress(sempid), _
                                subject, _
                                "<div style=""font-family:Arial; font-size:12px;"">" & line0 & "<br /> <br />" _
                                & line1 & ". <br /> <br /> <a href=" & links & ">" & links & "</a>" & _
                                 "</div>", "", True)
            End If

        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
        End Try
    End Sub

    Public Shared Sub Appraisal_Review_Complete(ByVal sempid As String, ByVal appperiod As String, empname As String, dept As String, links As String)
        Try
            Dim subject As String = empname & " Performance Feedback Review for " & appperiod & " is Complete and rated"

            Dim line0 As String = "Company  : " & GetCompanyName(dept)
            Dim line1 As String = "Unit/Dept: " & dept
            Dim line2 As String = empname & "'s Performance feedback for " & appperiod & " is complete"

            msgbuild.Clear()
            msgbuild.AppendLine(" ")
            msgbuild.AppendLine(line1)
            msgbuild.AppendLine(" ")
            msgbuild.AppendLine(line2)

            Dim Arrays() As String = Process.GetEmpIDMailList("hr").Split(SeparatorSemi, StringSplitOptions.RemoveEmptyEntries)
            For i = 0 To Arrays.Count - 1
                Process.MailNotification(sempid, MailPerfromance, subject, msgbuild.ToString, HRTeam, Process.AppName, "", Arrays(i), links)
            Next


            If SendNotification() = "Yes" Then
                Process.SendEmail("", Process.AppName, GetMailList("hr"), _
                                subject, _
                                "<div style=""font-family:Arial; font-size:12px;"">" & line0 & "<br /> <br />" & line1 & "<br /> <br />" _
                                & line2 & ". <br /> <br /> <a href=" & links & ">" & links & "</a>" & _
                                 "</div>", "", True)
            End If

        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
        End Try
    End Sub

    Public Shared Sub Appraisal_360_Notification(ByVal appperiod As String, empid As String, reviewerid As String, links As String)
        Try
            Dim empname As String = GetEmployeeData(empid, "fullname")
            Dim rname As String = GetEmployeeData(reviewerid, "fullname")
            Dim empjob As String = GetEmployeeData(reviewerid, "jobtitle")
            Dim empdept As String = GetEmployeeData(reviewerid, "office")
            Dim empcompany As String = GetCompanyByEmpID(empid)
            Dim revemail As String = GetEmployeeData(reviewerid, "workemail")
            Dim subject As String = GetEmployeeData(empid, "fullname") & " 360 Appraisal Feedback for " & appperiod
            Dim filePath As String = MailContentURL & MailFolderPerformance & "Feedback360_Alert_notification.txt"

            Dim line2 As String = "You have been selected to perform a 360 Appraisal Feedback on " & empname
            If reviewerid = empid Then
                line2 = "Your 360 Appraisal Feedback is ready for your rating"
            End If

            Dim readertxt As New StreamReader(filePath)
            msgbuild.Clear()
            While Not readertxt.EndOfStream
                Dim line As String = readertxt.ReadLine()
                msgbuild.AppendLine(line)
            End While
            readertxt.Close()
            msgbuild = msgbuild.Replace("@empname", empname).Replace("@appperiod", appperiod).Replace("@company", empcompany).Replace("@name", rname).Replace("@empjobtitle", empjob) _
                .Replace("@empdept", empdept).Replace("@line", line2)

            Process.MailNotification(empid, MailPerfromance, subject, msgbuild.ToString, reviewerid, Process.AppName, "", reviewerid, links, "")


            If SendNotification() = "Yes" Then
                Process.SendEmail("", empcompany, revemail, _
                                 subject, _
                                   "<div style=""font-family:Arial; font-size:12px;"">" & msgbuild.ToString & "<br /> <br /><a href=" & links & ">" & links & "</a> </div>", _
                                "", True, "")

            End If

        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
        End Try
    End Sub
    Public Shared Sub Appraisal_360_Complete(ByVal appperiod As String, empid As String, reviewerid As String, links As String)
        Try
            Dim subject As String = GetEmployeeData(reviewerid, "fullname") & " 360 Performance Feedback Review for " & appperiod & " on " & GetEmployeeData(empid, "fullname") & "is Complete"

            Dim line As String = GetEmployeeData(reviewerid, "fullname") & " 360 Performance Feedback Review for " & appperiod & " on " & GetEmployeeData(empid, "fullname") & "is Complete"
            Dim line0 As String = "Reviewer  : " & GetEmployeeData(reviewerid, "fullname")
            Dim line1 As String = "Company   : " & GetCompanyName(GetEmployeeData(reviewerid, "office"))
            Dim line2 As String = "Unit/Dept : " & GetEmployeeData(reviewerid, "office")
            Dim line3 As String = "Grade     : " & GetEmployeeData(reviewerid, "grade")
            Dim line4 As String = "Job Title : " & GetEmployeeData(reviewerid, "jobtitle")

            Dim line5 As String = "Reviewee  : " & GetEmployeeData(empid, "fullname")
            Dim line6 As String = "Company   : " & GetCompanyName(GetEmployeeData(empid, "office"))
            Dim line7 As String = "Unit/Dept : " & GetEmployeeData(empid, "office")
            Dim line8 As String = "Grade     : " & GetEmployeeData(empid, "grade")
            Dim line9 As String = "Job Title : " & GetEmployeeData(empid, "jobtitle")

            msgbuild.Clear()
            msgbuild.AppendLine(" ")
            msgbuild.AppendLine(line)
            msgbuild.AppendLine(" ")
            msgbuild.AppendLine(line0)
            msgbuild.AppendLine(line1)
            msgbuild.AppendLine(line2)
            msgbuild.AppendLine(line3)
            msgbuild.AppendLine(line4)

            msgbuild.AppendLine(" ")
            msgbuild.AppendLine(" ")
            msgbuild.AppendLine(line5)
            msgbuild.AppendLine(line6)
            msgbuild.AppendLine(line7)
            msgbuild.AppendLine(line8)
            msgbuild.AppendLine(line9)

            Dim Arrays() As String = Process.GetEmpIDMailList("hr").Split(SeparatorSemi, StringSplitOptions.RemoveEmptyEntries)
            For i = 0 To Arrays.Count - 1
                Process.MailNotification(appperiod, MailPerfromance, subject, msgbuild.ToString, HRTeam, Process.AppName, "", Arrays(i), links)
            Next


            If SendNotification() = "Yes" Then
                Process.SendEmail("", Process.AppName, GetMailList("hr"), _
                                subject, _
                                "<div style=""font-family:Arial; font-size:12px;"">" & line & "<br /> <br />" & line0 & "<br />" & line1 & "<br />" _
                                & line2 & " <br />" & line3 & " <br />" & line4 & " <br /> <br />" & line5 & " <br />" & line6 & " <br />" & line7 & " <br />" & line8 & " <br />" & line9 & " <br /> <a href=" & links & ">" & links & "</a>" & _
                                 "</div>", "", True)
            End If

        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
        End Try
    End Sub
#End Region

#Region "blog"
    Public Shared Function Blog_Request(ByVal blogtitle As String, sempid As String, blogdate As String, links As String) As Boolean
        Try
            Dim rname As String = GetEmployeeData(sempid, "fullname")
            Dim subject As String = "Blog posted by " & rname & " requires approval"

            Dim filePath As String = MailContentURL & MailFolderBlog & "BlogInitiate.txt"
            Dim readertxt As New StreamReader(filePath)
            msgbuild.Clear()
            While Not readertxt.EndOfStream
                Dim line As String = readertxt.ReadLine()
                msgbuild.AppendLine(line)
            End While
            readertxt.Close()
            msgbuild = msgbuild.Replace("@empname", rname).Replace("@date", blogdate).Replace("@company", GetCompanyByEmpID(sempid)).Replace("@empname", GetEmployeeData(sempid, "fullname")).Replace("@empid", sempid) _
                .Replace("@title", blogtitle).Replace("@dept", GetEmployeeData(sempid, "office"))

            Dim Arrays() As String = Process.GetEmpIDMailList("hr").Split(SeparatorSemi, StringSplitOptions.RemoveEmptyEntries)
            For i = 0 To Arrays.Count - 1
                Process.MailNotification(sempid, MailBlog, subject, msgbuild.ToString, sempid, Process.AppName, "", Arrays(i), links, "")
            Next

            If SendNotification() = "Yes" Then
                Return Process.SendEmail("", GetCompanyByEmpID(sempid), GetMailList("hr"), _
                                 subject, _
                                   "<div style=""font-family:Arial; font-size:12px;"">" & msgbuild.ToString & "<br /> <br /><a href=" & links & ">" & links & "</a> </div>", _
                                "", True, "")
            Else
                Return True
            End If

        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
            Return False
        End Try
    End Function
#End Region

#End Region

#Region "Grid Actions and Load Objects"
    Public Shared Sub LoadTestOptions(ByVal radioButtons As RadioButtonList, checkBoxes As CheckBoxList, Param() As String)

        'Load RadCombo Box with Display Text and Value
        Dim strDataSet As New DataSet
        radioButtons.Items.Clear()
        checkBoxes.Items.Clear()
        For i As Integer = 0 To Param.Length - 1
            Dim item As New ListItem
            item.Text = Param(i).ToString
            item.Value = Param(i).Substring(0, 1)
            radioButtons.Items.Add(item)
            checkBoxes.Items.Add(item)
        Next

    End Sub
    Public Shared Sub LoadRadioButtonsDb(ByVal radioButtons As RadioButtonList, SP As String, ByVal DisPlayText As String, ByVal setValue As String, ByVal Tip As String)

        'LoadRadioButtonsDb
        'Load RadCombo Box with Display Text and Value
        Dim strDataSet As New DataSet
        radioButtons.Items.Clear()
        strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, SP)
        For i As Integer = 0 To strDataSet.Tables(0).Rows.Count - 1
            Dim item As New ListItem
            item.Text = strDataSet.Tables(0).Rows(i).Item(DisPlayText).ToString
            item.Value = strDataSet.Tables(0).Rows(i).Item(setValue).ToString
            item.Attributes("title") = strDataSet.Tables(0).Rows(i).Item(Tip).ToString
            radioButtons.Items.Add(item)
        Next

    End Sub
    'HtmlTextArea
    Public Shared Sub LoadHTMLSelectTextAndValue(ByVal radBox As HtmlSelect, ByVal SP As String, ByVal DisplayText As String, ByVal setValue As String, Optional ByVal ApplyNA As Boolean = True)
        'Load RadCombo Box with Display Text and Value

        Dim strDataSet As New DataSet
        radBox.Items.Clear()


        If ApplyNA = True Then
            Dim itemTemp As New ListItem
            itemTemp.Text = "N/A"
            itemTemp.Value = "N/A"
            radBox.Items.Add(itemTemp)
        End If

        strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, SP)
        For i As Integer = 0 To strDataSet.Tables(0).Rows.Count - 1
            'strDataSet.Tables(0).Rows(i).Item(Index).ToString()
            Dim item As New ListItem()
            item.Text = strDataSet.Tables(0).Rows(i).Item(DisplayText).ToString()
            item.Value = strDataSet.Tables(0).Rows(i).Item(setValue).ToString()
            radBox.Items.Add(item)
        Next
    End Sub
    Public Shared Sub LoadRadDropDownTextAndValueInitiate(ByVal radBox As RadDropDownList, ByVal SP As String, ByVal Entry As String, ByVal DisplayText As String, ByVal setValue As String)

        Dim strDataSet As New DataSet
        radBox.Items.Clear()
        strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, SP)

        Dim itemTemp As New DropDownListItem()
        itemTemp.Text = Entry
        itemTemp.Value = ""
        radBox.Items.Add(itemTemp)
        itemTemp.DataBind()

        For i As Integer = 0 To strDataSet.Tables(0).Rows.Count - 1
            'strDataSet.Tables(0).Rows(i).Item(Index).ToString()
            Dim item As New DropDownListItem()
            item.Text = strDataSet.Tables(0).Rows(i).Item(DisplayText).ToString()
            item.Value = strDataSet.Tables(0).Rows(i).Item(setValue).ToString()
            radBox.Items.Add(item)
            item.DataBind()
        Next
    End Sub
    Public Shared Sub LoadRadDropDownTextAndValue(ByVal radBox As RadDropDownList, ByVal SP As String, ByVal DisplayText As String, ByVal setValue As String, Optional ByVal ApplyNA As Boolean = True)
        Dim strDataSet As New DataSet
        radBox.Items.Clear()


        If ApplyNA = True Then
            Dim itemTemp As New DropDownListItem
            itemTemp.Text = "N/A"
            itemTemp.Value = "N/A"
            radBox.Items.Add(itemTemp)
            itemTemp.DataBind()
        End If

        strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, SP)
        For i As Integer = 0 To strDataSet.Tables(0).Rows.Count - 1
            'strDataSet.Tables(0).Rows(i).Item(Index).ToString()
            Dim item As New DropDownListItem()
            item.Text = strDataSet.Tables(0).Rows(i).Item(DisplayText).ToString()
            item.Value = strDataSet.Tables(0).Rows(i).Item(setValue).ToString()
            radBox.Items.Add(item)
            item.DataBind()
        Next
        'radBox.DataTextField = DisplayText
        'radBox.DataValueField = setValue
        'radBox.DataSource = GetData(SP)
        'radBox.DataBind()
    End Sub
    Public Shared Sub LoadRadDropDownTextAndValueP1(ByVal radBox As RadDropDownList, ByVal SP As String, ByVal Param1 As String, ByVal DisplayText As String, ByVal setValue As String, Optional ByVal ApplyNA As Boolean = True)
        'Load RadCombo Box with Display Text and Value

        Dim strDataSet As New DataSet
        radBox.Items.Clear()

        If ApplyNA = True Then
            Dim itemTemp As New DropDownListItem
            itemTemp.Text = "N/A"
            itemTemp.Value = "N/A"
            radBox.Items.Add(itemTemp)
            itemTemp.DataBind()
        End If

        strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, SP, Param1)
        For i As Integer = 0 To strDataSet.Tables(0).Rows.Count - 1
            'strDataSet.Tables(0).Rows(i).Item(Index).ToString()
            Dim item As New DropDownListItem()
            item.Text = strDataSet.Tables(0).Rows(i).Item(DisplayText).ToString()
            item.Value = strDataSet.Tables(0).Rows(i).Item(setValue).ToString()
            radBox.Items.Add(item)
            item.DataBind()
        Next
        'radBox.DataTextField = DisplayText
        'radBox.DataValueField = setValue
        'radBox.DataSource = SearchData(SP, Param1)
        'radBox.DataBind()

    End Sub
    Public Shared Sub LoadRadDropDownTextAndValueP2(ByVal radBox As RadDropDownList, ByVal SP As String, ByVal Param1 As String, ByVal Param2 As String, ByVal DisplayText As String, ByVal setValue As String, Optional ByVal ApplyNA As Boolean = True)
        'Load RadCombo Box with Display Text and Value

        Dim strDataSet As New DataSet
        radBox.Items.Clear()

        If ApplyNA = True Then
            Dim itemTemp As New DropDownListItem
            itemTemp.Text = "N/A"
            itemTemp.Value = "N/A"
            radBox.Items.Add(itemTemp)
            itemTemp.DataBind()
        End If

        If SP = "Emp_PersonalDetail_get_all_Specific" Then
            strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, SP, Param1, Param2, 1, 10000000)
        Else
            strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, SP, Param1, Param2)
        End If

        For i As Integer = 0 To strDataSet.Tables(0).Rows.Count - 1
            'strDataSet.Tables(0).Rows(i).Item(Index).ToString()
            Dim item As New DropDownListItem()
            item.Text = strDataSet.Tables(0).Rows(i).Item(DisplayText).ToString()
            item.Value = strDataSet.Tables(0).Rows(i).Item(setValue).ToString()
            radBox.Items.Add(item)
            item.DataBind()
        Next
        'radBox.DataTextField = DisplayText
        'radBox.DataValueField = setValue
        'radBox.DataSource = SearchDataP2(SP, Param1, Param2)
        'radBox.DataBind()

    End Sub
    Public Shared Sub LoadTooltipComboBox(ByVal radBox As RadComboBox)

        Dim slist As String = ""
        Dim z As Integer = 0
        Dim collection As IList(Of RadComboBoxItem) = radBox.CheckedItems
        If (collection.Count <> 0) Then
            For Each item As RadComboBoxItem In collection
                z = z + 1
                If z = 0 Then
                    slist = item.Text
                Else
                    slist = slist & vbNewLine & item.Text
                End If
            Next
        Else
            radBox.ToolTip = ""
        End If
        radBox.ToolTip = slist
    End Sub
    Public Shared Sub LoadListAndComboxFromDataset(ByVal rListBox As RadListBox, ByVal radBox As RadComboBox, ByVal SP As String, ByVal ColumnText As String, ByVal ColumnValue As String, ByVal Param As String)
        rListBox.Items.Clear()

        Dim strDataSet As New DataSet
        strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, SP, Param)
        If strDataSet.Tables(0).Rows.Count > 0 Then
            For z As Integer = 0 To strDataSet.Tables(0).Rows.Count - 1
                'rListBox.Items.Add(strDataSet.Tables(0).Rows(z).Item(ColumnText).ToString)

                Dim listitem As New RadListBoxItem()
                listitem.Text = strDataSet.Tables(0).Rows(z).Item(ColumnText).ToString
                listitem.Value = strDataSet.Tables(0).Rows(z).Item(ColumnValue).ToString
                rListBox.Items.Add(listitem)
                listitem.DataBind()

                For l As Integer = 0 To radBox.Items.Count - 1
                    If radBox.Items(l).Value.ToLower = strDataSet.Tables(0).Rows(z).Item(ColumnValue).ToString.ToLower Then
                        radBox.Items(l).Checked = True
                        Exit For
                    End If
                Next
            Next
        End If

    End Sub
    Public Shared Sub LoadRadTree(ByVal rListBox As RadDropDownTree, ByVal SP As String, ByVal ColumnText As String, ByVal ColumnValue As String, ByVal Param As String)
        rListBox.EmbeddedTree.UncheckAllNodes()

        Dim strDataSet As New DataSet
        strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, SP, Param)
        If strDataSet.Tables(0).Rows.Count > 0 Then
            For z As Integer = 0 To strDataSet.Tables(0).Rows.Count - 1
                'For l As Integer = 0 To rListBox.EmbeddedTree.Nodes.Count - 1
                '    If rListBox.EmbeddedTree.Nodes(l).Text.ToLower = strDataSet.Tables(0).Rows(z).Item(ColumnValue).ToString.ToLower Then
                '        rListBox.EmbeddedTree.Nodes(l).Checked = True
                '        Exit For
                '    End If
                'Next
                For Each rootNode As RadTreeNode In rListBox.EmbeddedTree.Nodes
                    For Each parentNode As RadTreeNode In rootNode.GetAllNodes()
                        'If parentNode.Text = "Fixed" Then
                        If parentNode.Value.ToLower = strDataSet.Tables(0).Rows(z).Item(ColumnValue).ToString.ToLower Then
                            parentNode.Selected = True
                            parentNode.Checked = True
                            Exit For
                        End If
                        For Each childNode1 As RadTreeNode In parentNode.GetAllNodes()
                            If childNode1.Value.ToLower = strDataSet.Tables(0).Rows(z).Item(ColumnValue).ToString.ToLower Then
                                childNode1.Selected = True
                                childNode1.Checked = True
                                Exit For
                            End If
                            For Each childNode2 As RadTreeNode In childNode1.GetAllNodes()
                                If childNode2.Value.ToLower = strDataSet.Tables(0).Rows(z).Item(ColumnValue).ToString.ToLower Then
                                    childNode2.Selected = True
                                    childNode2.Checked = True
                                    Exit For
                                End If
                                For Each childNode3 As RadTreeNode In childNode2.GetAllNodes()
                                    If childNode3.Value.ToLower = strDataSet.Tables(0).Rows(z).Item(ColumnValue).ToString.ToLower Then
                                        childNode3.Selected = True
                                        childNode3.Checked = True
                                        Exit For
                                    End If
                                Next
                            Next
                        Next
                        'End If
                    Next
                Next
            Next
        End If
    End Sub
    Public Shared Sub LoadListAndComboxFromDatasetP2(ByVal rListBox As RadListBox, ByVal radBox As RadComboBox, ByVal SP As String, ByVal ColumnText As String, ByVal ColumnValue As String, ByVal Param As String, ByVal Param2 As String)

        rListBox.Items.Clear()

        Dim strDataSet As New DataSet
        strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, SP, Param, Param2)
        If strDataSet.Tables(0).Rows.Count > 0 Then
            For z As Integer = 0 To strDataSet.Tables(0).Rows.Count - 1
                'rListBox.Items.Add(strDataSet.Tables(0).Rows(z).Item(ColumnText).ToString)

                Dim listitem As New RadListBoxItem()
                listitem.Text = strDataSet.Tables(0).Rows(z).Item(ColumnText).ToString
                listitem.Value = strDataSet.Tables(0).Rows(z).Item(ColumnValue).ToString
                rListBox.Items.Add(listitem)
                listitem.DataBind()

                For l As Integer = 0 To radBox.Items.Count - 1
                    If radBox.Items(l).Value.ToLower = strDataSet.Tables(0).Rows(z).Item(ColumnValue).ToString.ToLower Then
                        radBox.Items(l).Checked = True
                        Exit For
                    End If
                Next
            Next
        End If

    End Sub
    Public Shared Sub LoadRadComboTextAndValue(ByVal radBox As RadComboBox, ByVal SP As String, ByVal DisplayText As String, ByVal setValue As String, Optional ByVal ApplyNA As Boolean = True)
        'Load RadCombo Box with Display Text and Value

        Dim strDataSet As New DataSet
        radBox.Items.Clear()
        If ApplyNA = True Then
            Dim itemTemp As New RadComboBoxItem()
            itemTemp.Text = "N/A"
            itemTemp.Value = "N/A"
            radBox.Items.Add(itemTemp)
            itemTemp.DataBind()
        End If

        strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, SP)
        For i As Integer = 0 To strDataSet.Tables(0).Rows.Count - 1
            'strDataSet.Tables(0).Rows(i).Item(Index).ToString()
            Dim item As New RadComboBoxItem()
            item.Text = strDataSet.Tables(0).Rows(i).Item(DisplayText).ToString()
            item.Value = strDataSet.Tables(0).Rows(i).Item(setValue).ToString()
            radBox.Items.Add(item)
            item.DataBind()
        Next

    End Sub
    Public Shared Sub LoadRadComboTextAndValue1(ByVal radBox As RadComboBox, ByVal SP As String, ByVal DisplayText As String, ByVal setValue As String, ByVal Parameter1 As Int16, Optional ByVal ApplyNA As Boolean = True)
        'Load RadCombo Box with Display Text and Value

        Dim strDataSet As New DataSet
        radBox.Items.Clear()
        If ApplyNA = True Then
            Dim itemTemp As New RadComboBoxItem()
            itemTemp.Text = "N/A"
            itemTemp.Value = "N/A"
            radBox.Items.Add(itemTemp)
            itemTemp.DataBind()
        End If

        strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, SP, Parameter1)
        For i As Integer = 0 To strDataSet.Tables(0).Rows.Count - 1
            'strDataSet.Tables(0).Rows(i).Item(Index).ToString()
            Dim item As New RadComboBoxItem()
            item.Text = strDataSet.Tables(0).Rows(i).Item(DisplayText).ToString()
            item.Value = strDataSet.Tables(0).Rows(i).Item(setValue).ToString()
            radBox.Items.Add(item)
            item.DataBind()
        Next

    End Sub
    Public Shared Sub LoadRadComboTextAndValueInitiate(ByVal radBox As RadComboBox, ByVal SP As String, ByVal Entry As String, ByVal DisplayText As String, ByVal setValue As String)

        Dim strDataSet As New DataSet
        radBox.Items.Clear()
        strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, SP)

        Dim itemTemp As New RadComboBoxItem()
        itemTemp.Text = Entry
        itemTemp.Value = ""
        radBox.Items.Add(itemTemp)
        itemTemp.DataBind()

        For i As Integer = 0 To strDataSet.Tables(0).Rows.Count - 1
            'strDataSet.Tables(0).Rows(i).Item(Index).ToString()
            Dim item As New RadComboBoxItem()
            item.Text = strDataSet.Tables(0).Rows(i).Item(DisplayText).ToString()
            item.Value = strDataSet.Tables(0).Rows(i).Item(setValue).ToString()
            radBox.Items.Add(item)
            item.DataBind()
        Next
    End Sub
    Public Shared Sub LoadRadComboTextAndValueInitiateP1(ByVal radBox As RadComboBox, ByVal SP As String, ByVal Param1 As String, ByVal Entry As String, ByVal DisplayText As String, ByVal setValue As String)
        Dim strDataSet As New DataSet
        radBox.Items.Clear()


        Dim itemTemp As New RadComboBoxItem()
        itemTemp.Text = Entry
        itemTemp.Value = Entry
        radBox.Items.Add(itemTemp)
        itemTemp.DataBind()

        If SP.ToLower().Trim() = "emp_personaldetail_get_employees" Then
            Using conn2 As New SqlConnection(WebConfig.ConnectionString)
                Dim comm2 As New SqlCommand(SP, conn2)
                comm2.CommandType = CommandType.StoredProcedure
                comm2.Parameters.AddWithValue("@Company", Param1)
                comm2.CommandTimeout = 157200
                Dim sdat2 As New SqlDataAdapter(comm2)
                sdat2.Fill(strDataSet)
                conn2.Close()

                For i As Integer = 0 To strDataSet.Tables(0).Rows.Count - 1
                    'strDataSet.Tables(0).Rows(i).Item(Index).ToString()
                    Dim item As New RadComboBoxItem()
                    item.Text = strDataSet.Tables(0).Rows(i).Item(DisplayText.Trim).ToString()
                    item.Value = strDataSet.Tables(0).Rows(i).Item(setValue.Trim).ToString()
                    radBox.Items.Add(item)
                    item.DataBind()
                Next
            End Using
        Else
            strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, SP, Param1)
            For i As Integer = 0 To strDataSet.Tables(0).Rows.Count - 1
                'strDataSet.Tables(0).Rows(i).Item(Index).ToString()
                Dim item As New RadComboBoxItem()
                item.Text = strDataSet.Tables(0).Rows(i).Item(DisplayText).ToString()
                item.Value = strDataSet.Tables(0).Rows(i).Item(setValue).ToString()
                radBox.Items.Add(item)
                item.DataBind()
            Next
        End If
    End Sub
    Public Shared Sub LoadRadComboTextAndValueInitiateP3(ByVal radBox As RadComboBox, ByVal SP As String, ByVal Param1 As String, ByVal Param2 As String, ByVal Param3 As String, ByVal Entry As String, ByVal DisplayText As String, ByVal setValue As String)

        Dim strDataSet As New DataSet
        radBox.Items.Clear()
        strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, SP, Param1, Param2, Param3)

        Dim itemTemp As New RadComboBoxItem()
        itemTemp.Text = Entry
        itemTemp.Value = Entry
        radBox.Items.Add(itemTemp)
        itemTemp.DataBind()

        For i As Integer = 0 To strDataSet.Tables(0).Rows.Count - 1
            'strDataSet.Tables(0).Rows(i).Item(Index).ToString()
            Dim item As New RadComboBoxItem()
            item.Text = strDataSet.Tables(0).Rows(i).Item(DisplayText.Trim).ToString()
            item.Value = strDataSet.Tables(0).Rows(i).Item(setValue.Trim).ToString()
            radBox.Items.Add(item)
            item.DataBind()
        Next
        'radBox.DataTextField = DisplayText

    End Sub
    Public Shared Sub LoadRadComboTextAndValueInitiateP2(ByVal radBox As RadComboBox, ByVal SP As String, ByVal Param1 As String, ByVal Param2 As String, ByVal Entry As String, ByVal DisplayText As String, ByVal setValue As String)

        Dim strDataSet As New DataSet
        radBox.Items.Clear()

        If SP = "Emp_PersonalDetail_get_all_Specific" Then
            strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, SP, Param1, Param2, 1, 10000000)
        Else
            strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, SP, Param1, Param2)
        End If

        Dim itemTemp As New RadComboBoxItem()
        itemTemp.Text = Entry
        itemTemp.Value = Entry
        radBox.Items.Add(itemTemp)
        itemTemp.DataBind()

        For i As Integer = 0 To strDataSet.Tables(0).Rows.Count - 1
            'strDataSet.Tables(0).Rows(i).Item(Index).ToString()
            Dim item As New RadComboBoxItem()
            item.Text = strDataSet.Tables(0).Rows(i).Item(DisplayText.Trim).ToString()
            item.Value = strDataSet.Tables(0).Rows(i).Item(setValue.Trim).ToString()
            radBox.Items.Add(item)
            item.DataBind()
        Next
        'radBox.DataTextField = DisplayText

    End Sub
    Public Shared Sub LoadRadComboTextAndValueP1(ByVal radBox As RadComboBox, ByVal SP As String, ByVal Param1 As String, ByVal DisplayText As String, ByVal setValue As String, Optional ByVal ApplyNA As Boolean = True)

        'Load RadCombo Box with Display Text and Value
        Dim strDataSet As New DataSet
        radBox.Items.Clear()
        If ApplyNA = True Then
            Dim itemTemp As New RadComboBoxItem()
            itemTemp.Text = "N/A"
            itemTemp.Value = "N/A"
            radBox.Items.Add(itemTemp)
            itemTemp.DataBind()
        End If

        If SP.ToLower().Trim() = "emp_personaldetail_get_employees" Then
            Using conn2 As New SqlConnection(WebConfig.ConnectionString)
                Dim comm2 As New SqlCommand(SP, conn2)
                comm2.CommandType = CommandType.StoredProcedure
                comm2.Parameters.AddWithValue("@Company", Param1)
                comm2.CommandTimeout = 157200
                Dim sdat2 As New SqlDataAdapter(comm2)
                sdat2.Fill(strDataSet)
                conn2.Close()

                For i As Integer = 0 To strDataSet.Tables(0).Rows.Count - 1
                    'strDataSet.Tables(0).Rows(i).Item(Index).ToString()
                    Dim item As New RadComboBoxItem()
                    item.Text = strDataSet.Tables(0).Rows(i).Item(DisplayText.Trim).ToString()
                    item.Value = strDataSet.Tables(0).Rows(i).Item(setValue.Trim).ToString()
                    radBox.Items.Add(item)
                    item.DataBind()
                Next
            End Using
        Else
            strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, SP, Param1)
            For i As Integer = 0 To strDataSet.Tables(0).Rows.Count - 1
                'strDataSet.Tables(0).Rows(i).Item(Index).ToString()
                Dim item As New RadComboBoxItem()
                item.Text = strDataSet.Tables(0).Rows(i).Item(DisplayText.Trim).ToString()
                item.Value = strDataSet.Tables(0).Rows(i).Item(setValue.Trim).ToString()
                radBox.Items.Add(item)
                item.DataBind()
            Next
        End If
    End Sub
    Public Shared Sub LoadRadComboTextAndValueP2(ByVal radBox As RadComboBox, ByVal SP As String, ByVal Param1 As String, ByVal Param2 As String, ByVal DisplayText As String, ByVal setValue As String, Optional ByVal ApplyNA As Boolean = True)

        'Load RadCombo Box with Display Text and Value
        Dim strDataSet As New DataSet
        radBox.Items.Clear()
        If ApplyNA = True Then
            Dim itemTemp As New RadComboBoxItem()
            itemTemp.Text = "N/A"
            itemTemp.Value = "N/A"
            radBox.Items.Add(itemTemp)
            itemTemp.DataBind()
        Else
            'Dim itemTemp As New RadComboBoxItem()
            ''itemTemp.Text = "N/A"
            ''itemTemp.Value = "N/A"
            'radBox.Items.Add(itemTemp)
            'itemTemp.DataBind()
        End If


        If SP = "Emp_PersonalDetail_get_all_Specific" Then
            strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, SP, Param1, Param2, 1, 10000000)
        Else
            strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, SP, Param1, Param2)
        End If

        For i As Integer = 0 To strDataSet.Tables(0).Rows.Count - 1
            'strDataSet.Tables(0).Rows(i).Item(Index).ToString()
            Dim item As New RadComboBoxItem()
            item.Text = strDataSet.Tables(0).Rows(i).Item(DisplayText.Trim).ToString()
            item.Value = strDataSet.Tables(0).Rows(i).Item(setValue.Trim).ToString()
            radBox.Items.Add(item)
            item.DataBind()
        Next
    End Sub
    Public Shared Sub LoadRadComboTextAndValueP3(ByVal radBox As RadComboBox, ByVal SP As String, ByVal Param1 As String, ByVal DisplayText As String, ByVal setValue As String, Optional ByVal ApplyNA As Boolean = True)

        'Load RadCombo Box with Display Text and Value
        Dim strDataSet As New DataSet
        radBox.Items.Clear()
        If ApplyNA = True Then
            Dim itemTemp As New RadComboBoxItem()
            itemTemp.Text = "N/A"
            itemTemp.Value = "N/A"
            radBox.Items.Add(itemTemp)
            itemTemp.DataBind()
        End If

        strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, SP, Param1)

        For i As Integer = 0 To strDataSet.Tables(0).Rows.Count - 1
            'strDataSet.Tables(0).Rows(i).Item(Index).ToString()
            Dim item As New RadComboBoxItem()
            item.Text = strDataSet.Tables(0).Rows(i).Item(DisplayText.Trim).ToString()
            item.Value = strDataSet.Tables(0).Rows(i).Item(setValue.Trim).ToString()
            radBox.Items.Add(item)
            item.DataBind()
        Next
    End Sub
    Public Shared Sub LoadTextBoxP1(ByVal WebTextBox As TextBox, ByVal SP As String, ByVal Param1 As String, ByVal DisplayText As String)
        'Load Text Box based on check items in combobox
        WebTextBox.Text = ""
        Dim strDataSet As New DataSet
        strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, SP, Param1)
        For i As Integer = 0 To strDataSet.Tables(0).Rows.Count - 1
            If WebTextBox.Text.Trim = "" Then
                WebTextBox.Text = "* " & strDataSet.Tables(0).Rows(i).Item(DisplayText.Trim).ToString()
            Else
                WebTextBox.Text = WebTextBox.Text & vbNewLine & "* " & strDataSet.Tables(0).Rows(i).Item(DisplayText.Trim).ToString()
            End If
        Next
    End Sub
    Public Shared Sub LoadTextAreaP1(ByVal txtarea As HtmlTextArea, ByVal SP As String, ByVal Param1 As String, ByVal DisplayText As String)
        'Load Text Box based on check items in combobox
        Dim outpt As String = ""
        Dim strDataSet As New DataSet
        strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, SP, Param1)
        For i As Integer = 0 To strDataSet.Tables(0).Rows.Count - 1
            If outpt <> "" Then
                txtarea.Value = "* " & strDataSet.Tables(0).Rows(i).Item(DisplayText.Trim).ToString()
            Else
                txtarea.Value = txtarea.Value & vbNewLine & "* " & strDataSet.Tables(0).Rows(i).Item(DisplayText.Trim).ToString()
            End If
        Next

    End Sub
    'Assign Value to Telerik ComboBox ByVal radBox As HtmlSelect
    Public Shared Sub AssignHTMLSelectValue(ByVal ComboBox As HtmlSelect, ByVal ControlValue As String)

        For j = 0 To ComboBox.Items.Count - 1
            If ComboBox.Items.Item(j).Text.ToLower = ControlValue.ToLower Or ComboBox.Items.Item(j).Value.ToLower = ControlValue.ToLower Then
                ComboBox.Items.Item(j).Selected = True
                Exit For
            End If
        Next
    End Sub
    Public Shared Sub AssignRadComboValue(ByVal ComboBox As RadComboBox, ByVal ControlValue As String)

        For j = 0 To ComboBox.Items.Count - 1
            If ComboBox.Items.Item(j).Text.ToLower = ControlValue.ToLower Or ComboBox.Items.Item(j).Value.ToLower = ControlValue.ToLower Then
                ComboBox.Items.Item(j).Selected = True
                Exit For
            End If
        Next
    End Sub
    Public Shared Sub AssignRadComboValue2(ByVal ComboBox As RadComboBox, ByVal ControlItem As String, ByVal ControlValue As String)
        Dim item As New RadComboBoxItem()
        ComboBox.Text = ControlItem
        ComboBox.SelectedValue = ControlValue
        item.DataBind()
    End Sub
    Public Shared Sub AssignRadDropDownValue(ByVal ComboBox As RadDropDownList, ByVal ControlValue As String)
        For j = 0 To ComboBox.Items.Count - 1
            If ComboBox.Items.Item(j).Text.ToLower = ControlValue.ToLower Or ComboBox.Items.Item(j).Value.ToLower = ControlValue.ToLower Then
                ComboBox.Items.Item(j).Selected = True
                Exit For
            End If
        Next
    End Sub
    'Assign Value to CheckboxList
    Public Shared Sub CheckboxListCheck(ByVal chkBox As CheckBoxList, ByVal ControlValue As String)
        For j = 0 To chkBox.Items.Count - 1
            If chkBox.Items.Item(j).Text.ToLower = ControlValue.ToLower Or chkBox.Items.Item(j).Value.ToLower = ControlValue.ToLower Then
                chkBox.Items.Item(j).Selected = True
                Exit For
            End If
        Next
    End Sub
    'Assign Value to Radio Button
    Public Shared Sub RadioListCheck(ByVal chkBox As RadioButtonList, ByVal ControlValue As String)
        For j = 0 To chkBox.Items.Count - 1
            If chkBox.Items.Item(j).Text.ToLower = ControlValue.ToLower Or chkBox.Items.Item(j).Value.ToLower = ControlValue.ToLower Then
                chkBox.Items.Item(j).Selected = True
                Exit For
            End If
        Next
    End Sub
    Public Shared Sub CheckComboFromText(ByVal WebTextBox As TextBox, ByVal combobox As RadComboBox)
        'Check Items in ComboBox based on item lists in textbox
        If WebTextBox.Text.Trim <> "" Then
            Dim Array(WebTextBox.Text.Split(New String() {vbNewLine}, StringSplitOptions.RemoveEmptyEntries).Count) As String
            Array = WebTextBox.Text.Split(New String() {vbNewLine}, StringSplitOptions.RemoveEmptyEntries)
            For z As Integer = 0 To Array.Count - 1
                For l As Integer = 0 To combobox.Items.Count - 1
                    If combobox.Items(l).Text.ToLower.Contains(Array(z).ToString.ToLower) Then
                        combobox.Items(l).Checked = True
                        Exit For
                    End If
                Next
            Next
        End If
    End Sub
    Public Shared Sub LoadTextBoxFromCombo(ByVal WebTextBox As TextBox, ByVal combobox As RadComboBox)
        'Load Text Box based on check items in combobox
        WebTextBox.Text = ""
        Dim collection As IList(Of RadComboBoxItem) = combobox.CheckedItems
        If (collection.Count <> 0) Then
            For Each item As RadComboBoxItem In collection
                If WebTextBox.Text.Trim = "" Then
                    WebTextBox.Text = item.Text
                Else
                    WebTextBox.Text = WebTextBox.Text & vbNewLine & item.Text
                End If
            Next
        Else
            WebTextBox.Text = ""
        End If
    End Sub
    Public Shared Sub LoadListBoxFromCombo(ByVal listbox As RadListBox, ByVal combobox As RadComboBox)
        'Load Listbox based on check items in combobox
        listbox.Items.Clear()
        Dim collection As IList(Of RadComboBoxItem) = combobox.CheckedItems
        If (collection.Count <> 0) Then
            For Each item As RadComboBoxItem In collection
                Dim listitem As New RadListBoxItem()
                listitem.Text = item.Text
                listitem.Value = item.Value
                listbox.Items.Add(listitem)
                listitem.DataBind()
            Next
        Else
            listbox.Items.Clear()
        End If
        listbox.Visible = True
    End Sub
    Public Shared Sub LoadListBoxFromComboNoClear(ByVal listbox As RadListBox, ByVal combobox As RadComboBox)
        'Load Listbox based on check items in combobox
        'listbox.Items.Clear()

        Dim collection As IList(Of RadComboBoxItem) = combobox.CheckedItems
        If (collection.Count <> 0) Then
            For Each item As RadComboBoxItem In collection
                Dim checkexist As Boolean = False
                'If listbox.Items. = False Then

                For d As Integer = 0 To listbox.Items.Count - 1
                    If listbox.Items(d).Text = item.Text Then
                        checkexist = True
                        Exit For
                    End If
                Next

                If checkexist = False Then
                    'listbox.Items.Add(item.Text)
                    Dim listitem As New RadListBoxItem()
                    listitem.Text = item.Text
                    listitem.Value = item.Value
                    listbox.Items.Add(listitem)
                    listitem.DataBind()
                End If
            Next
        Else
            'listbox.Items.Clear()
        End If
    End Sub

    Public Shared Function GetLocationData(company As String) As System.Data.DataTable
        Dim table As New System.Data.DataTable()
        table.Columns.Add("ID")
        table.Columns.Add("ParentID")
        table.Columns.Add("Value")
        table.Columns.Add("Text")

        Dim strDataSet As New DataSet
        strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "location_dropdown_view", company)
        If strDataSet.Tables(0).Rows.Count > 0 Then
            For i As Integer = 0 To strDataSet.Tables(0).Rows.Count - 1
                Dim id As String = ""
                Dim Parent As String = ""
                Dim Value As String = ""
                Dim TextField As String = ""

                id = strDataSet.Tables(0).Rows(i).Item("Locations").ToString
                If strDataSet.Tables(0).Rows(i).Item("Parent").ToString = "" Then
                    Parent = Nothing
                ElseIf IsDBNull(strDataSet.Tables(0).Rows(i).Item("Parent")) Then
                    Parent = Nothing
                Else
                    Parent = strDataSet.Tables(0).Rows(i).Item("Parent").ToString
                End If

                Value = strDataSet.Tables(0).Rows(i).Item("levels").ToString
                TextField = strDataSet.Tables(0).Rows(i).Item("Locations").ToString

                table.Rows.Add(New [String]() {id, Parent, Value, TextField})
            Next
        End If

        Return table
    End Function
    Public Shared Function GetUnitData() As System.Data.DataTable
        Dim table As New System.Data.DataTable()
        table.Columns.Add("ID")
        table.Columns.Add("ParentID")
        table.Columns.Add("Value")
        table.Columns.Add("Text")

        Dim strDataSet As New DataSet
        strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Company_Structure_dropdwon")
        If strDataSet.Tables(0).Rows.Count > 0 Then
            For i As Integer = 0 To strDataSet.Tables(0).Rows.Count - 1
                Dim id As String = ""
                Dim Parent As String = ""
                Dim Value As String = ""
                Dim TextField As String = ""

                id = strDataSet.Tables(0).Rows(i).Item("ID").ToString
                If strDataSet.Tables(0).Rows(i).Item("Parent").ToString = "" Then
                    Parent = Nothing
                ElseIf IsDBNull(strDataSet.Tables(0).Rows(i).Item("Parent")) Then
                    Parent = Nothing
                Else
                    Parent = strDataSet.Tables(0).Rows(i).Item("Parent").ToString
                End If

                Value = strDataSet.Tables(0).Rows(i).Item("Name").ToString
                TextField = strDataSet.Tables(0).Rows(i).Item("Name").ToString

                table.Rows.Add(New [String]() {id, Parent, Value, TextField})
            Next
        End If

        Return table
    End Function
    Public Shared Function GetUnitsData(ByVal company As String, ByVal compGrade As String) As System.Data.DataTable
        Dim table As New System.Data.DataTable()
        table.Columns.Add("ID")
        table.Columns.Add("ParentID")
        table.Columns.Add("Value")
        table.Columns.Add("Text")
        'Dim sql As String = "select distinct a.Name id, a.Name, Structuretype,[Level] ,case when Parent='n/a' then null else Parent end Parent from dbo.Fn_Company() a left outer join dbo.Employees_All b on a.Structuretype = b.OfficeLevel order by [Level] asc, Parent"
        Dim sql As String = "select distinct a.Companys name,a.Companys id,a.Levels [level],a.parent  from Fn_Company_Filter('" & company & "') a order by a.Levels asc,a.parent "
        sql = sql.Replace("@grade", compGrade)
        Dim strDataSet As New DataSet
        strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, sql)
        If strDataSet.Tables(0).Rows.Count > 0 Then
            For i As Integer = 0 To strDataSet.Tables(0).Rows.Count - 1
                Dim id As String = ""
                Dim Parent As String = ""
                Dim Value As String = ""
                Dim TextField As String = ""

                id = strDataSet.Tables(0).Rows(i).Item("ID").ToString
                If strDataSet.Tables(0).Rows(i).Item("Parent").ToString = "" Then
                    Parent = Nothing
                ElseIf IsDBNull(strDataSet.Tables(0).Rows(i).Item("Parent")) Then
                    Parent = Nothing
                Else
                    Parent = strDataSet.Tables(0).Rows(i).Item("Parent").ToString
                End If

                Value = strDataSet.Tables(0).Rows(i).Item("Name").ToString
                TextField = strDataSet.Tables(0).Rows(i).Item("Name").ToString

                table.Rows.Add(New [String]() {id, Parent, Value, TextField})
            Next
        End If

        Return table
    End Function
    Public Shared Sub LoadListBox(ByVal radBox As RadListBox, ByVal SP As String, ByVal Index As Integer)
        Try
            Dim strDataSet As New DataSet
            radBox.Items.Clear()
            strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, SP)
            For i As Integer = 0 To strDataSet.Tables(0).Rows.Count - 1
                radBox.Items.Add(strDataSet.Tables(0).Rows(i).Item(Index).ToString())
            Next
        Catch ex As Exception

        End Try
    End Sub
    Public Shared Sub LoadListBoxP1(ByVal radBox As RadListBox, ByVal SP As String, ByVal Param1 As String, ByVal Column1 As String)
        Try
            Dim strDataSet As New DataSet
            radBox.Items.Clear()
            strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, SP, Param1)
            For i As Integer = 0 To strDataSet.Tables(0).Rows.Count - 1
                Dim listitem As New RadListBoxItem()
                listitem.Text = strDataSet.Tables(0).Rows(i).Item(Column1).ToString
                listitem.Value = strDataSet.Tables(0).Rows(i).Item(Column1).ToString
                radBox.Items.Add(listitem)
                listitem.DataBind()
            Next
        Catch ex As Exception

        End Try
    End Sub
    Public Shared Sub LoadListBoxP2(ByVal radBox As RadListBox, ByVal SP As String, ByVal Param1 As String, ByVal Param2 As String, ByVal Column1 As String)
        Try
            Dim strDataSet As New DataSet
            radBox.Items.Clear()
            strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, SP, Param1, Param2)
            For i As Integer = 0 To strDataSet.Tables(0).Rows.Count - 1
                Dim listitem As New RadListBoxItem()
                listitem.Text = strDataSet.Tables(0).Rows(i).Item(Column1).ToString
                listitem.Value = strDataSet.Tables(0).Rows(i).Item(Column1).ToString
                radBox.Items.Add(listitem)
                listitem.DataBind()
            Next
        Catch ex As Exception

        End Try
    End Sub
    'Public Shared Sub LoadCombo(ByVal radBox As RadComboBox, ByVal SP As String, ByVal Index As Integer)
    '    Try
    '        Dim strDataSet As New DataSet
    '        radBox.Items.Clear()
    '        strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, SP)
    '        For i As Integer = 0 To strDataSet.Tables(0).Rows.Count - 1
    '            radBox.Items.Add(strDataSet.Tables(0).Rows(i).Item(Index).ToString())
    '        Next
    '    Catch ex As Exception

    '    End Try
    'End Sub
    'Public Shared Sub LoadComboP1(ByVal radBox As RadComboBox, ByVal SP As String, ByVal Param As String, ByVal Index As Integer)
    '    Try
    '        Dim strDataSet As New DataSet
    '        radBox.Items.Clear()
    '        strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, SP, Param)
    '        For i As Integer = 0 To strDataSet.Tables(0).Rows.Count - 1
    '            radBox.Items.Add(strDataSet.Tables(0).Rows(i).Item(Index).ToString())
    '        Next
    '    Catch ex As Exception

    '    End Try
    'End Sub
    'Public Shared Sub LoadComboP2(ByVal radBox As RadComboBox, ByVal SP As String, ByVal Param As String, ByVal Param2 As String, ByVal Index As Integer)
    '    Try
    '        Dim strDataSet As New DataSet
    '        radBox.Items.Clear()
    '        strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, SP, Param, Param2)
    '        For i As Integer = 0 To strDataSet.Tables(0).Rows.Count - 1
    '            radBox.Items.Add(strDataSet.Tables(0).Rows(i).Item(Index).ToString())
    '        Next
    '    Catch ex As Exception

    '    End Try
    'End Sub
    'Public Shared Sub LoadComboInitiate(ByVal radBox As RadComboBox, ByVal SP As String, ByVal Entry As String, ByVal Index As Integer)
    '    Try
    '        Dim strDataSet As New DataSet
    '        radBox.Items.Clear()
    '        strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, SP)

    '        If Entry <> "" Then
    '            radBox.Items.Add(Entry)
    '        End If
    '        For i As Integer = 0 To strDataSet.Tables(0).Rows.Count - 1
    '            radBox.Items.Add(strDataSet.Tables(0).Rows(i).Item(Index).ToString())
    '        Next
    '    Catch ex As Exception

    '    End Try
    'End Sub
    'Public Shared Sub LoadRadComboInitiate(ByVal radBox As RadDropDownList, ByVal SP As String, ByVal Entry As String, ByVal Index As Integer)
    '    Try
    '        Dim strDataSet As New DataSet
    '        radBox.Items.Clear()
    '        strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, SP)

    '        If Entry <> "" Then
    '            radBox.Items.Add(Entry)
    '        End If
    '        For i As Integer = 0 To strDataSet.Tables(0).Rows.Count - 1
    '            radBox.Items.Add(strDataSet.Tables(0).Rows(i).Item(Index).ToString())
    '        Next
    '    Catch ex As Exception

    '    End Try
    'End Sub
    'Public Shared Sub LoadRadCombo(ByVal radBox As RadDropDownList, ByVal SP As String, ByVal Index As Integer)
    '    Try
    '        Dim strDataSet As New DataSet
    '        radBox.Items.Clear()
    '        strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, SP)
    '        For i As Integer = 0 To strDataSet.Tables(0).Rows.Count - 1
    '            radBox.Items.Add(strDataSet.Tables(0).Rows(i).Item(Index).ToString())
    '        Next
    '    Catch ex As Exception

    '    End Try
    'End Sub
    'Public Shared Sub LoadRadComboP1(ByVal radBox As RadDropDownList, ByVal SP As String, ByVal Param1 As String, ByVal Index As Integer)
    '    Try
    '        Dim strDataSet As New DataSet
    '        radBox.Items.Clear()
    '        strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, SP, Param1)
    '        For i As Integer = 0 To strDataSet.Tables(0).Rows.Count - 1
    '            radBox.Items.Add(strDataSet.Tables(0).Rows(i).Item(Index).ToString())
    '        Next
    '    Catch ex As Exception

    '    End Try
    'End Sub
    'Public Shared Sub LoadRadComboP2(ByVal radBox As RadDropDownList, ByVal SP As String, ByVal Param1 As String, ByVal Param2 As String, ByVal Index As Integer)
    '    Try
    '        Dim strDataSet As New DataSet
    '        radBox.Items.Clear()
    '        strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, SP, Param1, Param2)
    '        For i As Integer = 0 To strDataSet.Tables(0).Rows.Count - 1
    '            radBox.Items.Add(strDataSet.Tables(0).Rows(i).Item(Index).ToString())
    '        Next
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Public Shared Function GetData(SP As String) As System.Data.DataTable
        Try
            Dim strDataSet As New DataSet
            strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, SP)
            dtTable = strDataSet.Tables(0)
            Return dtTable
        Catch ex As Exception
            Return dtTable
        End Try
    End Function
    'Public Shared Function GetDataParam1(SP As String, Param1 As String) As DataTable
    '    Try
    '        Dim strDataSet As New DataSet
    '        strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, SP, Param1)
    '        dtTable = strDataSet.Tables(0)
    '        Return dtTable
    '    Catch ex As Exception
    '        Return dtTable
    '    End Try
    'End Function

    Public Shared Function SearchData(SP As String, criteria As String) As System.Data.DataTable
        Try
            Dim strDataSet As New DataSet
            strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, SP, criteria)
            dtTable = strDataSet.Tables(0)
            Return dtTable
        Catch ex As Exception
            Return dtTable
        End Try

    End Function
    Public Shared Function SearchDataP2(SP As String, criteria As String, criteria2 As String) As System.Data.DataTable
        Try
            Dim strDataSet As New DataSet
            strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, SP, criteria, criteria2)
            dtTable = strDataSet.Tables(0)
            Return dtTable
        Catch ex As Exception
            Return dtTable
        End Try

    End Function
    Public Shared Function SearchDataP3(SP As String, criteria As String, criteria2 As String, criteria3 As String) As System.Data.DataTable
        Try
            Dim strDataSet As New DataSet
            strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, SP, criteria, criteria2, criteria3)
            dtTable = strDataSet.Tables(0)
            Return dtTable
        Catch ex As Exception
            Return dtTable
        End Try

    End Function
    Public Shared Function SearchDataP4(SP As String, criteria As String, criteria2 As String, criteria3 As String, criteria4 As String) As System.Data.DataTable
        Try
            Dim strDataSet As New DataSet
            strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, SP, criteria, criteria2, criteria3, criteria4)
            dtTable = strDataSet.Tables(0)
            Return dtTable
        Catch ex As Exception
            Return dtTable
        End Try

    End Function
    Public Shared Function SearchDataP5(SP As String, criteria As String, criteria2 As String, criteria3 As String, criteria4 As String, criteria5 As String) As System.Data.DataTable
        Try
            Dim strDataSet As New DataSet
            strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, SP, criteria, criteria2, criteria3, criteria4, criteria5)
            dtTable = strDataSet.Tables(0)
            Return dtTable
        Catch ex As Exception
            Return dtTable
        End Try

    End Function
    Public Shared Function SearchDataP6(SP As String, criteria As String, criteria2 As String, criteria3 As String, criteria4 As String, criteria5 As String, criteria6 As String) As System.Data.DataTable
        Try
            Dim strDataSet As New DataSet
            strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, SP, criteria, criteria2, criteria3, criteria4, criteria5, criteria6)
            dtTable = strDataSet.Tables(0)
            Return dtTable
        Catch ex As Exception
            Return dtTable
        End Try

    End Function
    Public Shared Function SearchDataP7(SP As String, criteria As String, criteria2 As String, criteria3 As String, criteria4 As String, criteria5 As String, criteria6 As String, criteria7 As String) As System.Data.DataTable
        Try
            Dim strDataSet As New DataSet
            strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, SP, criteria, criteria2, criteria3, criteria4, criteria5, criteria6, criteria7)
            dtTable = strDataSet.Tables(0)
            Return dtTable
        Catch ex As Exception
            Return dtTable
        End Try

    End Function
    Public Shared Function SearchDataP8(SP As String, criteria As String) As System.Data.DataTable
        Try
            Dim strDataSet As New DataSet
            strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, SP, criteria)
            dtTable = strDataSet.Tables(0)
            Return dtTable
        Catch ex As Exception
            Return dtTable
        End Try

    End Function
#End Region
#Region "Audit Trail"

    Public Shared Function GetAuditTrailInsertandUpdate(ByVal oldvalue As String, ByVal newvalue As String, ByVal Operation As String, ByVal PageForm As String) As Boolean
        Try
            Dim ta As New tblAuditTrail
            ta.HostName = System.Net.Dns.GetHostName
            ta.ActionBy = HttpContext.Current.Session.Item("LoginID").ToString
            ta.Action = Operation
            ta.Page = PageForm
            ta.IPAddress = HttpContext.Current.Session.Item("IPAddress").ToString
            ta.ProcessTime = DateTime.Now
            ta.OldValue = oldvalue
            ta.NewValue = newvalue
            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "audittrail_insert", ta.Action, ta.ActionBy, ta.OldValue, ta.NewValue, ta.IPAddress, ta.HostName, ta.Page)
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function
#End Region
End Class
