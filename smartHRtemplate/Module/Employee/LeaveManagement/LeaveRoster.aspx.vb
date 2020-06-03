Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports Telerik.Web.UI
Imports DayPilot.Web.Ui
Imports DayPilot.Web.Ui.Data
Imports DayPilot.Web.Ui.Enums
Imports DayPilot.Web.Ui.Events.Scheduler
Public Class LeaveRoster
    Inherits System.Web.UI.Page
    Dim AuthenCode As String = "LEAVECAL"
    Dim AuthenCode2 As String = "APPLEAVES"
    Private Function LoadLeavesDataTable() As DataTable
        Dim datatables As New DataTable
        If Session("leaverosterLoadType") = "All" Then
            datatables = Process.SearchDataP3("Employee_Leavelist_Approver_Summary_get_all", Session("UserEmpID"), CDate(Session("leaverosterdateFrom")), CDate(Session("leaverosterdateTo")))
        ElseIf Session("leaverosterLoadType") = "Find" Then
            search.Value = Session("leaverosterSearch")
            datatables = Process.SearchDataP4("Employee_Leavelist_Approver_Summary_Search", Session("UserEmpID"), CDate(Session("leaverosterdateFrom")), CDate(Session("leaverosterdateTo")), search.Value.Trim)
        End If

        pagetitle.InnerText = "LEAVE (" & FormatNumber(datatables.Rows.Count, 0) & ")"
        Return datatables
    End Function
    Private Sub LoadLeaves()
        Try

            GridVwHeaderChckbox.PageIndex = CInt(Session("leaverosterPageindex"))
            GridVwHeaderChckbox.DataSource = LoadLeavesDataTable()
            GridVwHeaderChckbox.AllowSorting = True
            GridVwHeaderChckbox.AllowPaging = True
            GridVwHeaderChckbox.DataBind()

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Sub LoadEmpLeaves()
        Try
            grdEmp.PageIndex = CInt(Session("leavePageindex"))
            grdEmp.DataSource = Process.SearchDataP3("Employee_Leave_get_all", Session("UserEmpID"), CDate(Session("leaverosterdateFrom")), CDate(Session("leaverosterdateto")))
            grdEmp.AllowSorting = True
            grdEmp.AllowPaging = True
            grdEmp.DataBind()

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Sub LoadChart()
        Try
            gridLeaveChart.DataSource = Process.SearchData("Emp_Leave_Chart", Session("UserEmpID"))
            gridLeaveChart.AllowSorting = False
            gridLeaveChart.AllowPaging = False
            gridLeaveChart.DataBind()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Function dbGetEvents(ByVal supid As String, ByVal start As Date, ByVal days As Integer) As DataTable
        Dim da As New SqlDataAdapter("SELECT a.name Employee, isnull(b.RefNo,'0') RefNo, isnull(b.EmpID,a.EmpID) EmpID, isnull(b.LeaveType,'n/a') LeaveType, isnull(b.LeaveFrom,'01-JAN-1900') LeaveFrom, isnull(DATEADD(day,1,b.LeaveTo),'01-JAN-1900') LeaveTo, isnull(b.NoOfDays,0) NoOfDays, isnull(b.[Status],'') [Status], b.id, b.LeavePeriod, isnull(b.FinalStatus,'n/a') FinalStatus, a.EmpID SubID FROM dbo.Employees_All a left outer join Employee_Leavelist b on a.EmpID = b.EmpID  and Terminated = 'No' and NOT ((b.LeaveTo <= @start) OR (b.LeaveFrom >= @end)) where (a.SupervisorID = @empid or a.empid = @empid)", WebConfig.ConnectionString)
        da.SelectCommand.Parameters.AddWithValue("start", start)
        da.SelectCommand.Parameters.AddWithValue("end", start.AddDays(days))
        da.SelectCommand.Parameters.AddWithValue("empid", supid)
        'Dim da As New SqlDataAdapter("SELECT * FROM [Employee_Leavelist] WHERE NOT (([leaveto] <= @start) OR ([leavefrom] >= @end)) and EmpID in (Select a.EmpID  from Emp_Work_History a where EndWork >= cast(getdate() as date) and Supervisor = @empid union all Select @empid)", WebConfig.ConnectionString)
        Dim dt As New DataTable()
        da.Fill(dt)
        Return dt
    End Function
    Private Sub LoadResources()
        DayPilotScheduler1.Resources.Clear()
        Dim supid As String = Session("UserEmpID")
        Dim start As Date = DayPilotScheduler1.StartDate
        Dim [end] As Date = DayPilotScheduler1.EndDate

        Dim da As New SqlDataAdapter("select a.name Employee,  a.EmpID,a.Name,isnull(sum(datediff(day, b.LeaveFrom, b.LeaveTo)),0)  as [Total]   from dbo.Employees_All a left outer join Employee_Leavelist b on a.EmpID = b.EmpID  and Terminated = 'No' and NOT ((b.LeaveTo <= @start) OR (b.LeaveFrom >= @end)) where (a.SupervisorID = @empid or a.empid = @empid) group by  a.EmpID,a.Name,a.Employee2", WebConfig.ConnectionString)
        da.SelectCommand.Parameters.AddWithValue("start", start)
        da.SelectCommand.Parameters.AddWithValue("end", [end])
        da.SelectCommand.Parameters.AddWithValue("empid", supid)
        Dim dt As New DataTable()
        da.Fill(dt)

        For Each r As DataRow In dt.Rows
            Dim first As String = CStr(r("Employee"))
            Dim id_Renamed As String = Convert.ToString(r("Employee"))
            Dim leavetype As String = "" ' CStr(r("leavetype"))
            Dim totalDays As Double = 0
            If Not r.IsNull("Total") Then
                Dim [to] As Object = r("Total")
                totalDays = CInt(Fix(r("Total")))
            End If


            Dim res As New DayPilot.Web.Ui.Resource(first, id_Renamed)
            'res.DataItem = r

            res.Columns.Add(New ResourceColumn(leavetype & ": " & String.Format("{0:0.0} day", totalDays)))

            DayPilotScheduler1.Resources.Add(res)
        Next r

    End Sub
    Private Sub LoadSeparators()
        'DayPilotScheduler1.Separators.Clear()
        'For i As Integer = 0 To DayPilotScheduler1.Days - 1
        '    Dim start As Date = DayPilotScheduler1.StartDate.AddDays(i)
        '    DayPilotScheduler1.Separators.Add(start, ColorTranslator.FromHtml("#cccccc"))
        'Next i
        'DayPilotScheduler1.Separators.Add(Date.Today, ColorTranslator.FromHtml("#ffaaaa"))

    End Sub
    Private Sub LoadDayPilot(ByVal sMonth As Integer, ByVal sYear As Integer)
        Try
            DayPilotScheduler1.StartDate = New Date(sYear, sMonth, 1)
            DayPilotScheduler1.Days = DateTime.DaysInMonth(sYear, sMonth)
            DayPilotScheduler1.DataSource = dbGetEvents(Session("UserEmpID"), DayPilotScheduler1.StartDate, DayPilotScheduler1.Days)
            DayPilotScheduler1.DataBind()
            pagetitle.InnerText = "My Team Leave Roaster " & cboMonth.SelectedItem.Text & ", " & cboYear.SelectedValue
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If Session("clicked") Is Nothing Then
                    Session("clicked") = "0"
                End If

                cboView.Items.Clear()
                cboView.Items.Add("Roaster")
                cboView.Items.Add("GridView")

                Dim strDataSet As New DataSet
                strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "select [year] from Data_Year")
                For i As Integer = 0 To strDataSet.Tables(0).Rows.Count - 1
                    Dim item As New RadComboBoxItem()
                    item.Text = strDataSet.Tables(0).Rows(i).Item("year").ToString()
                    item.Value = strDataSet.Tables(0).Rows(i).Item("year").ToString()
                    cboYear.Items.Add(item)
                Next


                Process.AssignRadComboValue(cboMonth, Date.Now.Month)
                Process.AssignRadComboValue(cboYear, Date.Now.Year)

                LoadDayPilot(cboMonth.SelectedValue, cboYear.SelectedValue)
                LoadResources()
                LoadChart()

                If Session("leaverosterLoadType") Is Nothing Then
                    Session("leaverosterLoadType") = "All"
                End If

                If Session("leaverosterPageindex") Is Nothing Then
                    Session("leaverosterPageindex") = "0"
                End If

                If Session("leavePageindex") Is Nothing Then
                    Session("leavePageindex") = "0"
                End If


                Dim strDateSet As New DataSet
                strDateSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Leave_Period_get_date", Date.Now)
                If strDataSet.Tables(0).Rows.Count > 0 Then
                    dateFrom.SelectedDate = CDate(strDateSet.Tables(0).Rows(0).Item("periodstart"))
                    dateTo.SelectedDate = CDate(strDateSet.Tables(0).Rows(0).Item("periodend"))
                Else
                    dateFrom.SelectedDate = Date.Now.AddDays(1 - Date.Now.Day).AddMonths(1 - Date.Now.Month)
                    dateTo.SelectedDate = Date.Now.AddMonths(12 - Date.Now.Month).AddDays(31 - Date.Now.Day)
                End If

                If Session("leaverosterdateFrom") Is Nothing Then
                    Session("leaverosterdateFrom") = dateFrom.SelectedDate
                    Session("leaverosterdateTo") = dateTo.SelectedDate
                Else
                    dateFrom.SelectedDate = CDate(Session("leaverosterdateFrom"))
                    dateTo.SelectedDate = CDate(Session("leaverosterdateTo"))
                End If


                If Session("clicked") = 2 Then
                    Process.AssignRadComboValue(cboView, "GridView")
                    MultiView1.ActiveViewIndex = 1
                    LoadLeaves()
                Else
                    Process.AssignRadComboValue(cboView, "Roaster")
                    MultiView1.ActiveViewIndex = 0
                    pagetitle.InnerText = "LEAVE ROSTER"
                End If


            Else
                LoadDayPilot(cboMonth.SelectedValue, cboYear.SelectedValue)
                LoadResources()
                LoadChart()

            End If

            LoadSeparators()
            If (Request.QueryString("id") = "emp") Then
                divsummary.Visible = True
                btAdd.Visible = True
                mgrview.Visible = False
                MultiView1.ActiveViewIndex = 1
                cboView.Visible = False
                search.Visible = False
                btsearch.Visible = False
                btnApprove.Visible = False
                LoadEmpLeaves()
            ElseIf (Request.QueryString("id") = "mgr") Then
                divsummary.Visible = False
                btAdd.Visible = False
                mgrview.Visible = True
                btnApprove.Visible = True
                empview.Visible = False
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub DayPilotScheduler1_BeforeEventRender(sender As Object, e As DayPilot.Web.Ui.Events.Scheduler.BeforeEventRenderEventArgs) Handles DayPilotScheduler1.BeforeEventRender
        Dim duration As TimeSpan = e.End - e.Start
        'e.Html = String.Format("{0} days", duration.TotalDays)
        'e.EventMoveVerticalEnabled = False
        If e.DataItem IsNot Nothing Then
            e.Html = e.DataItem("leavetype").ToString & ": " & String.Format("{0} days", duration.TotalDays)
            If e.DataItem("finalstatus").ToString.ToUpper = "APPROVED" Then
                e.BackgroundColor = "#00CC00"
            ElseIf e.DataItem("finalstatus").ToString.ToUpper = "PENDING" Then
                e.BackgroundColor = "#FFFF00"
            ElseIf e.DataItem("finalstatus").ToString.ToUpper = "REJECTED" Then
                e.BackgroundColor = "#FF3300"

            End If
        End If


    End Sub




    Protected Sub DayPilotScheduler1_BeforeResHeaderRender(sender As Object, e As DayPilot.Web.Ui.Events.Scheduler.BeforeResHeaderRenderEventArgs) Handles DayPilotScheduler1.BeforeResHeaderRender

    End Sub



    Protected Sub DayPilotScheduler1_HeaderColumnWidthChanged(sender As Object, e As DayPilot.Web.Ui.Events.Scheduler.HeaderColumnWidthChangedEventArgs) Handles DayPilotScheduler1.HeaderColumnWidthChanged

    End Sub



    Protected Sub gridLeaveChart_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridLeaveChart.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim leavedays As Integer = Integer.Parse(e.Row.Cells(2).Text)
                Dim balance As Integer = Integer.Parse(e.Row.Cells(5).Text)

                For Each cell As TableCell In e.Row.Cells
                    If leavedays = 0 Then
                        e.Row.Cells(6).Enabled = True
                    Else
                        If balance <= 0 Then
                            e.Row.Cells(6).Enabled = False
                        End If
                    End If
                Next
            End If
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub GridVwHeaderChckbox_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridVwHeaderChckbox.PageIndexChanging
        Try
            GridVwHeaderChckbox.PageIndex = e.NewPageIndex
            Session("pageIndex1") = e.NewPageIndex
            GridVwHeaderChckbox.DataSource = LoadLeavesDataTable(Session("LoadType"))
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Public Property SortsDirection() As SortDirection
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

    Private Sub GridVwHeaderChckbox_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridVwHeaderChckbox.RowCreated
        Try

            Process.SortArrow(e, SortsDirection, Session("sortleaveExpression"))

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub SortRecords(sender As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GridVwHeaderChckbox.Sorting
        Try
            Dim sortExpression As String = e.SortExpression
            Session("sortleaveExpression") = sortExpression
            Dim direction As String = String.Empty
            If SortsDirection = SortDirection.Ascending Then
                SortsDirection = SortDirection.Descending
                direction = " DESC"
            Else
                SortsDirection = SortDirection.Ascending
                direction = " ASC"
            End If
            Dim table As New DataTable
            table = LoadLeavesDataTable()
            table.DefaultView.Sort = sortExpression & direction
            GridVwHeaderChckbox.PageIndex = CInt(Session("pageIndex1"))
            GridVwHeaderChckbox.DataSource = table
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub



    Protected Sub btnFind_Click(sender As Object, e As EventArgs)
        Try
            If search.Value.Trim = "" Then
                Session("LoadType") = "All"
            Else
                Session("LoadType") = "Find"
            End If

            LoadLeaves()
        Catch ex As Exception
        End Try
    End Sub


    Protected Sub btnApprove_Click(sender As Object, e As EventArgs) Handles btnApprove.Click
        Try
            Dim msgbuild As New StringBuilder()
            Dim Separators() As Char = {";"c}
            Dim countsa As Integer = 1
            Dim leaveref As String = ""
            Dim finalstatus As String = ""
            Dim RequesterName As String = ""
            Dim RequesterMail As String = ""
            Dim supervisorStat As String = ""
            Dim RequesterID As String = ""
            Dim startdate As Date, enddate As Date, approver1name As String = "", approver2name As String = ""
            Dim leavetype As String = "", status_approver1name As String = "", status_approver2name As String = ""
            Dim strLoan As New DataSet
            Dim confirmValue As String = Request.Form("confirm_app")
            If confirmValue = "No" Then
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Multiple Approval cancelled" + "')", True)
            Else

                System.Threading.Thread.Sleep(300)
                Dim atLeastOneRowDeleted As Boolean = False
                ' Iterate through the Products.Rows property
                For Each row As GridViewRow In GridVwHeaderChckbox.Rows
                    ' Access the CheckBox
                    Dim cb As CheckBox = row.FindControl("chkEmp")
                    If cb IsNot Nothing AndAlso cb.Checked Then
                        atLeastOneRowDeleted = True

                        Dim ID As String = Convert.ToString(GridVwHeaderChckbox.DataKeys(row.RowIndex).Values(0))
                        Dim datefrom As String = Process.DDMONYYYY(Convert.ToDateTime(GridVwHeaderChckbox.DataKeys(row.RowIndex).Values(1)).Date)
                        Dim dateto As String = Process.DDMONYYYY(Convert.ToDateTime(GridVwHeaderChckbox.DataKeys(row.RowIndex).Values(2)).Date)

                        strLoan = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Employee_Leavelist_get_pending", ID, Session("UserEmpID"), datefrom, dateto)

                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Employee_Leavelist_Update_Multiple_Status", ID, Session("UserEmpID"), datefrom, dateto, "Approved")

                        If strLoan.Tables(0).Rows.Count > 0 Then
                            countsa = countsa + 1
                            For i = 0 To strLoan.Tables(0).Rows.Count - 1
                                finalstatus = strLoan.Tables(0).Rows(i).Item("FinalStatus").ToString
                                status_approver1name = strLoan.Tables(0).Rows(i).Item("status").ToString
                                status_approver2name = strLoan.Tables(0).Rows(i).Item("status2").ToString
                                approver1name = strLoan.Tables(0).Rows(i).Item("Approver1Name").ToString
                                approver2name = strLoan.Tables(0).Rows(i).Item("Approver2Name").ToString
                                leaveref = strLoan.Tables(0).Rows(i).Item("refno").ToString
                                RequesterID = strLoan.Tables(0).Rows(i).Item("EmpID").ToString
                                leavetype = strLoan.Tables(0).Rows(i).Item("Leave Type").ToString
                                startdate = CDate(strLoan.Tables(0).Rows(i).Item("from"))
                                enddate = CDate(strLoan.Tables(0).Rows(i).Item("to"))
                                Dim strGrade As New DataSet
                                Dim approvername As String = ""

                                'Get Admin Approver
                                strGrade = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "select name, email from dbo.Employees_All where empid = '" & Session("UserEmpID") & "'")
                                If strGrade.Tables(0).Rows.Count > 0 Then
                                    approvername = strGrade.Tables(0).Rows(0).Item("name").ToString
                                End If

                                'Get Employee
                                strGrade = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "select name, email from dbo.Employees_All where empid = '" & RequesterID & "'")

                                If strGrade.Tables(0).Rows.Count > 0 Then
                                    RequesterMail = strGrade.Tables(0).Rows(i).Item("email").ToString
                                    RequesterName = strGrade.Tables(0).Rows(i).Item("name").ToString
                                End If

                                Process.Leave_Approver_Approvals(Process.GetMailList("hr"), leaveref, RequesterName, leavetype, startdate, enddate, "", "Approved", approvername, "", "", Session("UserEmpID"), Process.ApplicationURL + "/" + Process.GetMailLink(AuthenCode, 1))

                                'Process.Leave_HR_Level1_Approval(Process.GetMailList("hr"), leaveref, RequesterName, leavetype, startdate, enddate, "", status_approver1name, approvername, "", Session("UserEmpID"))
                            Next

                        End If
                    End If
                Next
                Dim msgg As String = ""
                If atLeastOneRowDeleted = True Then                    
                    msgg = countsa.ToString & " Multiple Pending Leaves Approved successful"
                    Process.loadalert(divalert, msgalert, msgg, "success")
                Else
                    msgg = "Multiple Approval cancelled, no selection made"
                    Process.loadalert(divalert, msgalert, msgg, "warning")

                End If
                LoadLeaves()

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnApplyCasual_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect("~/Module/Employee/LeaveManagement/CasualLeaves", True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub cboView_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboView.SelectedIndexChanged
        Try
            If cboView.SelectedItem.Text = "Roaster" Then
                MultiView1.ActiveViewIndex = 0
                Session("clicked") = 1
                pagetitle.InnerText = "LEAVE ROSTER"
            Else
                MultiView1.ActiveViewIndex = 1
                Session("clicked") = 2
                LoadLeaves()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cboMonth_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboMonth.SelectedIndexChanged
        LoadDayPilot(cboMonth.SelectedValue, cboYear.SelectedValue)
        LoadResources()
    End Sub

    Private Sub cboYear_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboYear.SelectedIndexChanged
        LoadDayPilot(cboMonth.SelectedValue, cboYear.SelectedValue)
        LoadResources()
    End Sub

    Private Sub grdEmp_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles grdEmp.PageIndexChanging
        Try
            grdEmp.PageIndex = e.NewPageIndex
            Session("leavePageindex") = e.NewPageIndex
            grdEmp.DataSource = Process.SearchDataP3("Employee_Leave_get_all", Session("UserEmpID"), CDate(Session("leaveroasterdateFrom")), CDate(Session("leaveroasterdateTo")))
            grdEmp.DataBind()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Private Sub grdEmp_Sorting(sender As Object, e As GridViewSortEventArgs) Handles grdEmp.Sorting

    End Sub

    Protected Sub dateFrom_SelectedDateChanged(sender As Object, e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles dateFrom.SelectedDateChanged
        Try
            Session("leaverosterdateFrom") = dateFrom.SelectedDate
            If (Request.QueryString("id") = "mgr") Then
                LoadLeaves()
            Else
                LoadEmpLeaves()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub dateTo_SelectedDateChanged(sender As Object, e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles dateTo.SelectedDateChanged
        Try
            Session("leaverosterdateTo") = dateTo.SelectedDate
            If (Request.QueryString("id") = "mgr") Then
                LoadLeaves()
            Else
                LoadEmpLeaves()
            End If
        Catch ex As Exception
        End Try
    End Sub
End Class