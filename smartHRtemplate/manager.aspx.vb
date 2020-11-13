Imports Microsoft.ApplicationBlocks.Data
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Collections.Generic
Imports System.Web.UI.WebControls
Imports Telerik.Web.UI
Imports Telerik.Charting
Imports Telerik.Web.UI.HtmlChart.PlotArea
Imports Telerik.Web.UI.HtmlChart
Imports Telerik.Web.UI.HtmlChart.Enums
Imports GOSHRM.GOSHRM.GOSHRM.BO

Public Class test1
    Inherits System.Web.UI.Page
    Public male, female, no, jobtitle As String
    Public CompletionStatusName, CompletionStatusNameScore As String
    Public PerformanceName, PerformanceNameScore As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                ' LoadGrid()
                Dim strEmployee As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_PersonalDetail_DirectReports_My_Team", Session("UserEmpID"))
                Dim s As StringBuilder = New StringBuilder("")
                For i As Integer = 0 To strEmployee.Tables(0).Rows.Count - 1
                    Dim sn = strEmployee.Tables(0).Rows(i).Item("Rows").ToString()
                    Dim Name = strEmployee.Tables(0).Rows(i).Item("Names").ToString()
                    Dim office = strEmployee.Tables(0).Rows(i).Item("Company").ToString()
                    Dim JobTitle = strEmployee.Tables(0).Rows(i).Item("JobTitle").ToString()
                    Dim JobGrade = strEmployee.Tables(0).Rows(i).Item("GradeLevel").ToString()
                    Dim Performance = strEmployee.Tables(0).Rows(i).Item("Score").ToString()
                    Dim empid = strEmployee.Tables(0).Rows(i).Item("empid").ToString()
                    Dim chap As char= Name.Substring(0, 1)
                    Dim jsfunction = " good('" + empid + "')"
                    Dim url = Process.ApplicationURL + "/Module/Employee/EmployeeProfile?empid=" + empid
                    s.Append("<tr>
                           <td><a href='" + url + "'  class='avatar'>" + chap + "</a><h2><a href='#' onclick=" + jsfunction + ">" + Name + "<span>" + JobTitle + "</span></a></h2>
											</td>
													<td>" + empid + " </td>
													<td>
														<h2><a href='#'>" + office + "</a></h2>
													</td>
													<td>" + JobGrade + "</td>
													<td> " + Performance + "</td>
											
													<td><a class='btn btn-white btn-sm rounded' onclick=" + jsfunction + ">Manage Employee</a></td>	
													<td>
                                                    <a href='" + url + "'
                                                    <span class='label label-success-border'> View Profile</span></a></td>
												</tr>
												")




                Next
                mgrbody.InnerHtml = s.ToString()
            End If
        Catch ex As Exception

        End Try

        'Direct Report Count
        'Dim strDashBoard As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_PersonalDetail_DirectReports", Session("UserEmpID"))
        'Gcount.InnerText = FormatNumber(strDashBoard.Tables(0).Rows.Count.ToString(), 0)
        'strDashBoard.Clear()

        ''Male to Female Ratio
        'strDashBoard = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_PersonalDetail_DirectReports_gender_ratio", Session("UserEmpID"))
        'Dim m As Integer = strDashBoard.Tables(0).Rows.Count
        'If m > 0 Then
        '    For i As Integer = 0 To m - 1
        '        male = Convert.ToString(strDashBoard.Tables(0).Rows(i)("male"))
        '        female = Convert.ToString(strDashBoard.Tables(0).Rows(i)("female"))
        '        'Dim total As String = Convert.ToString(strDashBoard.Tables(0).Rows(i)("total"))
        '    Next
        'End If
        'strDashBoard.Clear()

        ''jobtitle(Distribution)
        'strDashBoard = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_PersonalDetail_DirectReports_jobtitle_distribution", Session("UserEmpID"))
        'Dim n As Integer = strDashBoard.Tables(0).Rows.Count
        'Dim s As StringBuilder = New StringBuilder("")
        'If n > 0 Then
        '    For i As Integer = 0 To n - 1
        '        no = Convert.ToString(strDashBoard.Tables(0).Rows(i)("No"))
        '        jobtitle = Convert.ToString(strDashBoard.Tables(0).Rows(i)("JobTitle"))
        '        s.Append("<li><div class='experience-user'><div class='before-circle'></div></div>")
        '        s.Append("<div class='experience-content'><div class='timeline-content'>")
        '        s.Append("<a href='#/' class='name'>" + jobtitle + " (" + no + ")</a>")
        '        s.Append("<div></div></div></div></li>")
        '    Next
        'End If
        'Dim rr As String = s.ToString()
        'distribute.InnerHtml = rr
        'strDashBoard.Clear()

        ''Birthday
        'strDashBoard = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_PersonalDetail_DirectReports_Date_of_Birth", Session("UserEmpID"))
        'Dim nn As Integer = strDashBoard.Tables(0).Rows.Count
        'Dim ss As StringBuilder = New StringBuilder("")
        'If nn > 0 Then
        '    For i As Integer = 0 To nn - 1
        '        Dim name As String = Convert.ToString(strDashBoard.Tables(0).Rows(i)("Name"))
        '        Dim dept As String = Convert.ToString(strDashBoard.Tables(0).Rows(i)("Office"))
        '        ss.Append("<li><div class='experience-user'><div class='before-circle'></div></div>")
        '        ss.Append("<div class='experience-content'><div class='timeline-content'>")
        '        ss.Append("<a href='#/' class='name'>" + name + "</a>")
        '        ss.Append("<div>" + dept + "</div></div></div></li>")
        '    Next
        'Else
        '    ss.Append("<li><div class='experience-user'><div class='before-circle'></div></div>")
        '    ss.Append("<div class='experience-content'><div class='timeline-content'>")
        '    ss.Append("<a href='#/' class='name'>No Birthday Today</a>")
        '    ss.Append("<div></div></div></div></li>")
        'End If
        'Dim r As String = ss.ToString()
        'birthday.InnerHtml = r
        'strDashBoard.Clear()

        ''Top Performers
        'Dim Tperform As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_PersonalDetail_DirectReports_Top_Performers", Session("UserEmpID"))
        'Dim b As Integer = Tperform.Tables(0).Rows.Count
        'Dim f As StringBuilder = New StringBuilder("")
        'If b > 0 Then
        '    For i As Integer = 0 To b - 1
        '        Dim score As String = Convert.ToString(Tperform.Tables(0).Rows(i)("Score"))
        '        Dim name As String = Convert.ToString(Tperform.Tables(0).Rows(i)("Name"))
        '        Dim imgsrc As String = Convert.ToString(Tperform.Tables(0).Rows(i)("imgtype"))

        '        Dim new_img As String
        '        If imgsrc = "" Or Nothing Then
        '            new_img = "<img class='avatar' src='images/user.jpg'>"
        '        Else
        '            new_img = String.Concat("<img class='img-responsive img-circle' src=""", Page.ResolveClientUrl(imgsrc), """>")
        '        End If
        '        If score = "" Or score Is Nothing Then
        '            score = "0"
        '        End If
        '        f.Append("<li><div class='activity-user'><a href='#' title='" + name + "' data-toggle='tooltip' class='avatar'>")
        '        f.Append("" + new_img + "</a></div>")
        '        f.Append("<div class='activity-content'><div class='timeline-content'><a href='Module/Employee/Performance/DirectReportAppraisalObjectivesForm' class='name'>" + name + "")
        '        f.Append("<span class='time'>" + score + "%</span></div></div></li>")
        '    Next
        'End If
        'Dim g As String = f.ToString()
        'top_perform.InnerHtml = g

        ''Team Leave
        'Dim teamleave As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_PersonalDetail_DirectReports_Team_Leave", Session("UserEmpID"))
        'Dim y As Integer = teamleave.Tables(0).Rows.Count
        'Dim h As StringBuilder = New StringBuilder("")
        'If y > 0 Then
        '    For i As Integer = 0 To y - 1
        '        Dim name As String = Convert.ToString(teamleave.Tables(0).Rows(i)("Name"))
        '        Dim leavetype As String = Convert.ToString(teamleave.Tables(0).Rows(i)("LeaveType"))
        '        Dim leavefrom As String = Convert.ToString(teamleave.Tables(0).Rows(i)("LeaveFrom"))
        '        Dim leaveto As String = Convert.ToString(teamleave.Tables(0).Rows(i)("LeaveTo"))
        '        h.Append("<tr><td><class='block text-ellipsis'><span class='text-xs'>" + name + "</span></td>")
        '        h.Append("<td class='text'><class='block text-ellipsis'><span class='text-xs'>" + leavetype + "</span></td>")
        '        h.Append("<td class='text'><class='block text-ellipsis'><span class='text-xs'>" + leavefrom + "</span></td>")
        '        h.Append("<td class='text'><class='block text-ellipsis'><span class='text-xs'>" + leaveto + "</span></td></tr>")
        '    Next
        'Else
        '    h.Append("<tr><td><small class='block text-ellipsis'><span class='text-xs'></span></small></td>")
        '    h.Append("<td class='text'><small class='block text-ellipsis'><span class='text-xs'>No Leave Available</span></small></td>")
        '    h.Append("<td class='text'><small class='block text-ellipsis'><span class='text-xs'></span></small></td>")
        '    h.Append("<td class='text'><small class='block text-ellipsis'><span class='text-xs'></span></small></td></tr>")
        'End If
        'Dim p As String = h.ToString()
        'leavetable.InnerHtml = p


        ''My Team
        'Dim myteam As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_PersonalDetail_DirectReports_My_Team", Session("UserEmpID"))
        'Dim bb As Integer = myteam.Tables(0).Rows.Count
        'Dim ff As StringBuilder = New StringBuilder("")
        'If bb > 0 Then
        '    For i As Integer = 0 To bb - 1
        '        Dim dept As String = Convert.ToString(myteam.Tables(0).Rows(i)("Company"))
        '        Dim name As String = Convert.ToString(myteam.Tables(0).Rows(i)("Name"))
        '        Dim jtitle As String = Convert.ToString(myteam.Tables(0).Rows(i)("JobTitle"))
        '        Dim prate As String = Convert.ToString(myteam.Tables(0).Rows(i)("Score"))
        '        'Dim query As String = Convert.ToString(myteam.Tables(0).Rows(i)("Query"))
        '        'Dim comp As String = Convert.ToString(myteam.Tables(0).Rows(i)("Skills"))
        '        Dim id As String = Convert.ToString(myteam.Tables(0).Rows(i)("empid"))
        '        Dim gd As String = Convert.ToString(myteam.Tables(0).Rows(i)("GradeLevel"))
        '        Dim imgsrc As String = Convert.ToString(myteam.Tables(0).Rows(i)("imgtype"))

        '        Dim new_img As String
        '        If imgsrc = "" Or Nothing Then
        '            new_img = "<img class='avatar' src='images/user.jpg'>"
        '        Else
        '            'new_img = "<img class='avatar' src='data:image/png;base64," + base64String + "'>"
        '            new_img = String.Concat("<img class='avatar' src=""", Page.ResolveClientUrl(imgsrc), """>")
        '        End If
        '        ff.Append("<div class='col-lg-3 col-sm-4'><div class='card-box project-box'><div class='profile-img'><a href='#'>")
        '        ff.Append("" + new_img + "</a><h5 style='width:200px' class='user-name m-t-0'>" + name + "</h5></div><br><br>")
        '        ff.Append("<div class='project-members m-b-15'><ul class='personal-info'><li><span class='title'>Dept/Office:</span>")
        '        ff.Append("<span class='text'>" + dept + "</span></li>")
        '        ff.Append("<li><span class='title'>Job Title:</span><span class='text'>" + jtitle + "</span></li>")
        '        ff.Append("<li><span class='title'>Job Grade:</span><span class='text'>" + gd + "</span></li>")
        '        ff.Append("<li><span class='title'>P Rating:</span>")
        '        ff.Append("<span class='text'><a href='Module/Employee/Performance/DirectReportAppraisalObjectivesForm'>" + prate + "%</a></span></li>")
        '        ff.Append("<li><a href='Module/Employee/EmployeeData?id=" + id + "'style='margin-left:70px' class='btn btn-default btn-sm m-t-10'>View Profile</a></li></ul></div>")
        '        ff.Append("</div></div>")
        '    Next
        'End If
        'Dim gg As String = ff.ToString()
        'mTeam.InnerHtml = gg

        ''Manager's Approval Section
        ''Loan
        'strDashBoard = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Loan_Approver_get_manager", Session("UserEmpID"), "Pending")
        'Aloan.InnerText = FormatNumber(strDashBoard.Tables(0).Rows.Count.ToString(), 0)
        'strDashBoard.Clear()

        ''Leave
        'strDashBoard = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Employee_Leavelist_Approver_get_manager", Session("UserEmpID"), "Pending")
        'ALeave.InnerText = FormatNumber(strDashBoard.Tables(0).Rows.Count.ToString(), 0)
        'strDashBoard.Clear()

        ''Dev Plan
        'strDashBoard = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Development_Plan_Get_Surbodinate_manager", Session("UserEmpID"), "Pending")
        'ADev.InnerText = FormatNumber(strDashBoard.Tables(0).Rows.Count.ToString(), 0)
        'strDashBoard.Clear()

        ''Training
        'strDashBoard = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Training_Sessions_Request_Get_Surbodinate_manager", Session("UserEmpID"), "Pending")
        'ATrain.InnerText = FormatNumber(strDashBoard.Tables(0).Rows.Count.ToString(), 0)
        'strDashBoard.Clear()

        ''Performance Objective
        'strDashBoard = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Appraisal_Summary_Coach_Get_All", Session("UserEmpID"), Date.Now.Year)
        'APer.InnerText = FormatNumber(strDashBoard.Tables(0).Rows.Count.ToString(), 0)
        'strDashBoard.Clear()

        ''Performance Feedback
        'strDashBoard = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Appraisal_Summary_SecondReview_Get_All", Session("UserEmpID"), Date.Now.Year)
        'APerFeed.InnerText = FormatNumber(strDashBoard.Tables(0).Rows.Count.ToString(), 0)
        'strDashBoard.Clear()

        ''Query
        'strDashBoard = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Employee_Query_Get_manager", Session("UserEmpID"), "In-progress")
        'ADis.InnerText = FormatNumber(strDashBoard.Tables(0).Rows.Count.ToString(), 0)
        'strDashBoard.Clear()

        ''Promotion
        'strDashBoard = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Employee_Query_Get_manager", Session("UserEmpID"), "Pending")
        'APro.InnerText = FormatNumber(strDashBoard.Tables(0).Rows.Count.ToString(), 0)
        'strDashBoard.Clear()

        ''Job Exit
        'strDashBoard = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Termination_Get_Surbodinate_Employee_manager", Session("UserEmpID"), "Pending")
        'AJob.InnerText = FormatNumber(strDashBoard.Tables(0).Rows.Count.ToString(), 0)
        'strDashBoard.Clear()

        ''WorkForce Planning
        'strDashBoard = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_WorkForce_Approval_Get_All", Session("Organisation"), Session("UserEmpID"), "Pending", "plan")
        'AWork.InnerText = FormatNumber(strDashBoard.Tables(0).Rows.Count.ToString(), 0)
        'strDashBoard.Clear()

        ''Succession Plan
        'strDashBoard = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruitment_Succession_Get_All_Approver_manager", Session("UserEmpID"), "Pending")
        'ASucc.InnerText = FormatNumber(strDashBoard.Tables(0).Rows.Count.ToString(), 0)
        'strDashBoard.Clear()

        ''Emp confirmation
        'strDashBoard = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Confirmation_Get_All_manager", Session("UserEmpID"))
        'EmpCon.InnerText = FormatNumber(strDashBoard.Tables(0).Rows.Count.ToString(), 0)
        'strDashBoard.Clear()

        ''Attendance
        'strDashBoard = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Time_Employee_Attendance_Get_My_Team", Session("UserEmpID"), Date.Now)
        'EmpAttend.InnerText = FormatNumber(strDashBoard.Tables(0).Rows.Count.ToString(), 0)
        'strDashBoard.Clear()

        ''Timesheet
        'strDashBoard = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Time_Sheet_Get_PMAll_Projects", Session("UserEmpID"), "In-progress")
        'Emptime.InnerText = FormatNumber(strDashBoard.Tables(0).Rows.Count.ToString(), 0)
        'strDashBoard.Clear()

        ''Manager's Request Section
        ''WorkForce Planning
        'strDashBoard = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Time_Sheet_Get_PMAll_Projects", Session("UserEmpID"), "Pending")
        'RWork.InnerText = FormatNumber(strDashBoard.Tables(0).Rows.Count.ToString(), 0)
        'strDashBoard.Clear()

        ''Staff Requisition
        'strDashBoard = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Job_Requisition_Get_All", Session("UserEmpID"), "Pending")
        'RStaff.InnerText = FormatNumber(strDashBoard.Tables(0).Rows.Count.ToString(), 0)
        'strDashBoard.Clear()

        ''Succession Plan
        'strDashBoard = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruitment_Succession_Get_All_Manager", Session("UserEmpID"))
        'RSucc.InnerText = FormatNumber(strDashBoard.Tables(0).Rows.Count.ToString(), 0)
        'strDashBoard.Clear()

        ''Promotion
        'strDashBoard = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruitment_Promotion_Approver_Get_All", Session("UserEmpID"))
        '' RPro.InnerText = FormatNumber(strDashBoard.Tables(0).Rows.Count.ToString(), 0)
        'strDashBoard.Clear()

        'Dim strCompletionStatus As New DataSet
        'Using conn2 As New SqlConnection(WebConfig.ConnectionString)
        '    Dim comm2 As New SqlCommand("sp_Get_CompletionStatus_Managers_dashboard", conn2)
        '    comm2.CommandType = CommandType.StoredProcedure
        '    comm2.Parameters.AddWithValue("@Company", Session("UserEmpID"))
        '    'comm2.Parameters.AddWithValue("@Company", company)
        '    comm2.CommandTimeout = 157200
        '    Dim sdat2 As New SqlDataAdapter(comm2)
        '    sdat2.Fill(strCompletionStatus)
        '    conn2.Close()
        'End Using
        'If strCompletionStatus.Tables(0).Rows.Count > 0 Then
        '    Dim c As Integer = strCompletionStatus.Tables(0).Rows.Count
        '    Dim CompletionStatus(c) As String
        '    Dim CompletionStatusScore(c) As String
        '    If c > 0 Then
        '        For i As Integer = 0 To c - 1
        '            CompletionStatus(i) = Convert.ToString(strCompletionStatus.Tables(0).Rows(i)("Office"))
        '            CompletionStatusScore(i) = Convert.ToString(strCompletionStatus.Tables(0).Rows(i)("Status"))
        '        Next
        '    End If
        '    CompletionStatusName = String.Format("'{0}'", String.Join("','", CompletionStatus)).Replace("''", "")
        '    CompletionStatusNameScore = String.Join(",", (CompletionStatusScore))


        'End If


        'Dim strPerformance As New DataSet
        'Using conn2 As New SqlConnection(WebConfig.ConnectionString)
        '    Dim comm2 As New SqlCommand("sp_Get_Performance_Managers_dashboard", conn2)
        '    comm2.CommandType = CommandType.StoredProcedure
        '    comm2.Parameters.AddWithValue("@Company", Session("UserEmpID"))
        '    'comm2.Parameters.AddWithValue("@Company", company)
        '    comm2.CommandTimeout = 157200
        '    Dim sdat2 As New SqlDataAdapter(comm2)
        '    sdat2.Fill(strPerformance)
        '    conn2.Close()
        'End Using
        'If strPerformance.Tables(0).Rows.Count > 0 Then
        '    Dim c As Integer = strPerformance.Tables(0).Rows.Count
        '    Dim Performance(c) As String
        '    Dim PerformanceScore(c) As String
        '    If c > 0 Then
        '        For i As Integer = 0 To c - 1
        '            Performance(i) = Convert.ToString(strPerformance.Tables(0).Rows(i)("Name"))
        '            PerformanceScore(i) = Convert.ToString(strPerformance.Tables(0).Rows(i)("Score"))
        '        Next
        '    End If
        '    PerformanceName = String.Format("'{0}'", String.Join("','", Performance)).Replace("''", "")
        '    PerformanceNameScore = String.Join(",", (PerformanceScore))


        'End If


    End Sub
    'Private Sub LoadGrid()
    '    Try
    '        gridskills.PageIndex = CInt(Session("courseskillLoadindex"))
    '        gridskills.DataSource = LoadDatatable()
    '        gridskills.AllowSorting = True
    '        gridskills.DataBind()

    '    Catch ex As Exception

    '        Process.loadalert(divalert, msgalert, ex.Message, "danger")
    '    End Try
    'End Sub
    'Private Function LoadDatatable() As DataTable
    '    Dim dt As New DataTable

    '    dt = Process.SearchData("Emp_PersonalDetail_DirectReports_My_Team", Session("UserEmpID"))

    '    Return dt
    'End Function
    'Protected Sub SortRecords(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
    '    Try
    '        Dim sortExpression As String = e.SortExpression
    '        Session("courseskillsortExpression") = sortExpression
    '        Dim direction As String = String.Empty
    '        If SortsDirection = SortDirection.Ascending Then
    '            SortsDirection = SortDirection.Descending
    '            direction = " DESC"
    '        Else
    '            SortsDirection = SortDirection.Ascending
    '            direction = " ASC"
    '        End If
    '        gridskills.PageIndex = CInt(Session("courseskillLoadindex"))
    '        Dim table As DataTable = LoadDatatable()
    '        table.DefaultView.Sort = sortExpression & direction
    '        gridskills.DataSource = table
    '        gridskills.DataBind()
    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Public Property SortsDirection() As SortDirection
    '    Get
    '        If ViewState("SortDirection") Is Nothing Then
    '            ViewState("SortDirection") = SortDirection.Ascending
    '        End If
    '        Return DirectCast(ViewState("SortDirection"), SortDirection)
    '    End Get
    '    Set(ByVal value As SortDirection)
    '        ViewState("SortDirection") = value
    '    End Set
    'End Property

    'Private Sub gridskills_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gridskills.PageIndexChanging
    '    Try
    '        gridskills.PageIndex = e.NewPageIndex
    '        Session("courseskillLoadindex") = e.NewPageIndex
    '        LoadGrid()
    '    Catch ex As Exception
    '    End Try
    'End Sub

    'Private Sub gridskills_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridskills.RowCreated
    '    Try
    '        Process.SortArrow(e, SortsDirection, Session("courseskillsortExpression"))
    '    Catch ex As Exception
    '    End Try
    'End Sub
    'Protected Sub DrillDown(ByVal sender As Object, ByVal e As EventArgs)
    '    Try
    '        txtskillid.Text = CType(sender, LinkButton).CommandArgument
    '        Dim url As String = "courseskills?id=" & txtskillid.Text
    '        Response.Redirect(url, True)
    '    Catch ex As Exception

    '    End Try
    'End Sub
End Class