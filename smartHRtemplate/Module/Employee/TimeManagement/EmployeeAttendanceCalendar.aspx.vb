Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Public Class EmployeeAttendanceCalendar
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "EMPATTEND"
    Dim Pages As String = "Employee Attendance"
    Dim apptype As String = ConfigurationManager.AppSettings("apptype")
    Private socialEvents As DataTable

    Private Sub LoadControls(ByVal sDate As Date)
        Try
            pagetitle.InnerText = "My Attendance: " & MonthName(sDate.Month) & ", " & sDate.Year
            'Session("UserEmpID")
            Dim finalstatus As String = ""
            Dim stat As String = ""
            Dim strAttend As New DataSet

            strAttend = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Time_Employee_Attendance_Get_All", Session("UserEmpID"), sDate, sDate)
            'strUser.Tables(0).Rows(0).Item("empid").ToString
            If strAttend.Tables(0).Rows.Count > 0 Then
                lblid.Text = strAttend.Tables(0).Rows(0).Item("id").ToString
                ashift.Value = strAttend.Tables(0).Rows(0).Item("shifts").ToString
                ashiftduration.Value = strAttend.Tables(0).Rows(0).Item("duration").ToString
                lblholidayid.Text = strAttend.Tables(0).Rows(0).Item("isworkday").ToString
                If IsDBNull(strAttend.Tables(0).Rows(0).Item("checkintime")) = False Then
                    btrequest.Disabled = True
                    aclockin.Value = strAttend.Tables(0).Rows(0).Item("checkintime").ToString
                    aclockout.Value = strAttend.Tables(0).Rows(0).Item("checkouttime").ToString
                    If lblholidayid.Text = "0" Then
                        lbdateheader.InnerText = strAttend.Tables(0).Rows(0).Item("checkindate").ToString & ": Holiday/Week-End"
                    Else
                        lbdateheader.InnerText = strAttend.Tables(0).Rows(0).Item("checkindate").ToString
                    End If


                    aDateOut.Value = strAttend.Tables(0).Rows(0).Item("checkoutdate").ToString
                    ahourlogged.Value = strAttend.Tables(0).Rows(0).Item("actualduration").ToString
                    aapprovedovertime.Value = strAttend.Tables(0).Rows(0).Item("agreedovertime").ToString
                    aovertimestat.Value = strAttend.Tables(0).Rows(0).Item("finalstatus").ToString()
                    stat = strAttend.Tables(0).Rows(0).Item("overtimepayrequest").ToString()

                    If CDbl(ahourlogged.Value) > CDbl(ashiftduration.Value) Then
                        aovertime.Value = FormatNumber(CDbl(ahourlogged.Value) - CDbl(ashiftduration.Value), 2)
                        btrequest.Disabled = False
                        Process.DisableButton(btnCancel)
                    Else
                        aovertime.Value = "0"
                        aapprovedovertime.Value = "0"
                        btrequest.Disabled = True
                        Process.DisableButton(btnCancel)
                    End If

                    If CDbl(ahourlogged.Value) > 0 And lblholidayid.Text.Trim = "0" Then
                        btrequest.Disabled = False
                        Process.DisableButton(btnCancel)
                    End If

                    If stat = "0" Or stat.ToUpper = "FALSE" Then
                        divovertimestatid.Visible = False
                        btrequest.Disabled = False
                        Process.DisableButton(btnCancel)
                    Else
                        btrequest.Disabled = True
                        Process.EnableButton(btnCancel)
                        divovertimestatid.Visible = True

                        If aovertimestat.Value = "Approved" Then
                            btrequest.Disabled = True
                            Process.DisableButton(btnCancel)
                        ElseIf aovertimestat.Value = "Pending" Then
                            btrequest.Disabled = True
                            Process.EnableButton(btnCancel)
                        Else
                            btrequest.Disabled = True
                            Process.DisableButton(btnCancel)
                        End If
                    End If


                    'lblholidayid
                Else
                    If lblholidayid.Text = "0" Then
                        lbdateheader.InnerText = Process.DDMONYYYY(MyPCalendar.SelectedDate) & ": Holiday/Week-End"
                    ElseIf strAttend.Tables(0).Rows(0).Item("leaveid").ToString = "0" Then
                        lbdateheader.InnerText = Process.DDMONYYYY(MyPCalendar.SelectedDate) & ": Absent"
                    Else
                        lbdateheader.InnerText = Process.DDMONYYYY(MyPCalendar.SelectedDate) & ": On Leave"
                    End If



                    btrequest.Disabled = True
                    aclockin.Value = ""
                    aclockout.Value = ""
                    aDateOut.Value = ""
                    ahourlogged.Value = ""
                    aapprovedovertime.Value = ""
                    aovertime.Value = ""
                    aovertimestat.Value = ""
                End If


            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not Me.IsPostBack Then

                Session("LoadType") = "All"

                MyPCalendar.SelectedDate = Date.Now
                Session("Date1") = Process.FirstDay(Date.Now.Year, Date.Now.Month)
                Session("Date2") = Process.LastDay(Date.Now.Year, Date.Now.Month)

                'btrequest.Disabled = True
                LoadControls(MyPCalendar.SelectedDate)



            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub



    Public Property SortDirection() As SortDirection
        Get
            If ViewState("SortDirection") Is Nothing Then
                ViewState("SortDirection") = SortDirection.Ascending
            End If
            Return DirectCast(ViewState("SortDirection"), SortDirection)
        End Get
        Set(ByVal value As SortDirection)
            ViewState("SortDirection") = value
        End Set
    End Property

    Private Sub MyPCalendar_DayRender(sender As Object, e As System.Web.UI.WebControls.DayRenderEventArgs) Handles MyPCalendar.DayRender
        Try

            Dim firstdate As String = Process.DDMONYYYY(Session("Date1"))  'Process.FirstDay(Date.Now.Year, Date.Now.Month - 1)
            Dim lastdate As String = Process.DDMONYYYY(Session("Date2")) ' Process.LastDay(Date.Now.Year, Date.Now.Month)
            socialEvents = Process.SearchDataP3("Time_Employee_Attendance_Get_All", Session("UserEmpID"), firstdate, lastdate)

            Dim Separators() As Char = {","c}
            Dim rowss As DataRow() = socialEvents.[Select]([String].Format("Dates >= '" & firstdate & "' AND Dates <= '" & lastdate & "'"))
            For Each row As DataRow In rowss
                Dim labels As String = ""
                If e.Day.[Date] = Convert.ToDateTime(row("Dates")) Then
                    Dim tid As String = row("id").ToString()
                    If tid.Trim = "0" Then
                        e.Cell.BackColor = Color.Yellow
                    Else
                        e.Cell.BackColor = Color.FromArgb(76, 175, 80)
                    End If
                    e.Cell.ToolTip = tid
                End If
            Next

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub MyPCalendar_SelectionChanged(sender As Object, e As EventArgs) Handles MyPCalendar.SelectionChanged
        LoadControls(MyPCalendar.SelectedDate)
    End Sub


    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Dim confirmValue As String = Request.Form("confirm_value")
            If confirmValue = "Yes" Then
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Time_Employee_Attendance_Overtime_Cancel", lblid.Text)                
                Process.loadalert(divalert, msgalert, "Overtime request has been withdrawn!", "info")
                Process.DisableButton(btnCancel)
                LoadControls(MyPCalendar.SelectedDate)
            Else
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Action has been cancelled" + "')", True)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnRequest_Click(sender As Object, e As EventArgs)
        Try
            Dim url As String = "OvertimeRequest?id=" & lblid.Text
            Dim s As String = "window.open('" & url + "', 'popup_window', 'width=500,height=400,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no');"
            ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Private Sub MyPCalendar_VisibleMonthChanged(sender As Object, e As System.Web.UI.WebControls.MonthChangedEventArgs) Handles MyPCalendar.VisibleMonthChanged
        Try
            Session("Date1") = Process.DDMONYYYY(Process.FirstDay(e.NewDate.Year, e.NewDate.Month))
            Session("Date2") = Process.DDMONYYYY(Process.LastDay(e.NewDate.Year, e.NewDate.Month))

        Catch ex As Exception

        End Try
    End Sub
End Class