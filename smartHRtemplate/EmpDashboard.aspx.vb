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
Imports System.Globalization

Public Class EmpDashboard
    Inherits System.Web.UI.Page
    Public cur_lenght, cur_per, cur_per_forcast, obj As String
    Public score, year, actuallSkills, expectedSkills, actualWeight, expectedWeight As String

    Protected Sub TrainingLib_ServerClick(sender As Object, e As EventArgs)
        Try
            Response.Redirect("~/Module/Employee/TrainingPortal/AvailableTrainings.aspx?id=emp", True)
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub Devplan_ServerClick(sender As Object, e As EventArgs)
        Try
            Response.Redirect("~/Module/Employee/Performance/DevelopmentPlans", True)
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub Training_ServerClick(sender As Object, e As EventArgs)
        Try
            Response.Redirect("~/Module/Employee/TrainingPortal/Training", True)
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub ApplyLeave_ServerClick(sender As Object, e As EventArgs)
        Try
            Response.Redirect("~/Module/Employee/TrainingPortal/AvailableTrainings.aspx?id=emp", True)
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub FeedbackList_ServerClick(sender As Object, e As EventArgs)
        Try
            Response.Redirect("~/Module/Employee/Performance/AppraisalFeedbackList", True)
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub ObjectiveList_ServerClick(sender As Object, e As EventArgs)
        Try
            Response.Redirect("~/Module/Employee/Performance/AppraisalObjectivesForm", True)
        Catch ex As Exception

        End Try
    End Sub
    'Public weight As Integer

    Public Class cal_events
        Public Property start As String
        Public Property eventtime As String
        Public Property eventdes As String
        Public Property enddate As String
        Public Property eventstat As String
        Public Property title As String
        Public Property className As String
        Public Property allDay As Boolean
    End Class

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Me.IsPostBack Then

            obj = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select Objectives from Performance_Custom_Naming")
            'Expected Skills Rating
            Dim jobskills As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Job_Titles_get_index", Session("UserEmpID"))
            If jobskills.Tables(0).Rows.Count > 0 Then
                Dim c As Integer = jobskills.Tables(0).Rows.Count
                Dim skiller(c) As String
                Dim weighter(c) As Integer
                If c > 0 Then
                    For i As Integer = 0 To c - 1
                        skiller(i) = Convert.ToString(jobskills.Tables(0).Rows(i)("Skills"))
                        weighter(i) = Convert.ToInt32(jobskills.Tables(0).Rows(i)("rating"))
                        'y.Append("<div><span class='glyphicon glyphicon-ok'></span> " + skills + "</div>")
                    Next
                    expectedSkills = String.Format("'{0}'", String.Join("','", skiller)).Replace("''", "")
                    expectedWeight = String.Join(",", (weighter))
                End If
            End If

            'Actual Skills Rating
            Dim q1 As String = Session("UserEmpID")
            Dim actualskills As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "select * from Employee_Training_Sessions where empid ='" & q1 & "'")
            If actualskills.Tables(0).Rows.Count > 0 Then
                Dim c As Integer = actualskills.Tables(0).Rows.Count
                For i As Integer = 0 To c - 1
                    Dim emptrainingsessionId As Integer = Convert.ToString(actualskills.Tables(0).Rows(i)("id"))
                    Dim str As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Training_Application_Assessment_Scores", emptrainingsessionId)
                    If str.Tables(0).Rows.Count > 0 Then
                        Dim z As Integer = str.Tables(0).Rows.Count
                        Dim skiller(z) As String
                        Dim weighter(z) As Integer
                        Dim achievement As Decimal = 0
                        Dim ratez As Decimal = 0

                        For j As Integer = 0 To z - 1
                            Dim jobs_kills As New DataTable
                            jobs_kills = Process.SearchData("Job_Titles_get_index", Session("UserEmpID"))
                            skiller(j) = Convert.ToString(str.Tables(0).Rows(j)("KPIObjectives"))
                            Dim empskillAssessmentid As Integer = Convert.ToInt32(str.Tables(0).Rows(j)("id"))
                            If IsDBNull(str.Tables(0).Rows(j)("achievement")) = False Then
                                achievement = Convert.ToDecimal(str.Tables(0).Rows(j)("achievement"))
                            End If
                            If IsDBNull(str.Tables(0).Rows(j)("achievement")) = False Then
                                ratez = Convert.ToDecimal(str.Tables(0).Rows(j)("rating"))
                            End If
                            Dim foundSkills As DataRow() = jobs_kills.[Select]("Skills = '" & skiller(j) & "'")
                            If foundSkills.Length <> 0 Then
                                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Actual_skills_update", empskillAssessmentid, skiller(j), ratez, achievement, q1)
                                'Else
                                '    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Actual_skills_update", empskillAssessmentid, skiller(j), ratez, 0, q1)
                            End If
                            If IsDBNull(str.Tables(0).Rows(j)("achievement")) = False Then
                                weighter(j) = Convert.ToInt32(str.Tables(0).Rows(j)("achievement"))
                            End If
                        Next
                    End If
                Next

                Dim fred As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Actual_skills_get", Session("UserEmpID"))
                If fred.Tables(0).Rows.Count > 0 Then
                    Dim p As Integer = fred.Tables(0).Rows.Count
                    Dim skiller(p) As String
                    Dim weighter(p) As Integer
                    If p > 0 Then
                        For i As Integer = 0 To p - 1
                            Dim jobs_kills As New DataTable
                            jobs_kills = Process.SearchData("Job_Titles_get_index", Session("UserEmpID"))
                            skiller(i) = Convert.ToString(fred.Tables(0).Rows(i)("KpiObjective"))
                            weighter(i) = Convert.ToInt32(fred.Tables(0).Rows(i)("ActualScore"))
                            Dim foundSkills As DataRow() = jobs_kills.[Select]("Skills = '" & skiller(i) & "'")
                            If foundSkills.Length <> 0 Then
                                Dim ratingz As Integer = Convert.ToInt32(foundSkills(0).Item("rating"))
                                If ratingz < weighter(i) Then
                                    weighter(i) = ratingz
                                End If
                            Else
                                weighter(i) = 0
                            End If

                        Next
                        actuallSkills = String.Format("'{0}'", String.Join("','", skiller)).Replace("''", "")
                        actualWeight = String.Join(",", (weighter))
                    End If
                    'SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, CommandType.Text, "delete from Emp_Assessment_Skills_Record where empid ='" & Session("UserEmpID") & "'")
                End If
            End If

            'Trainings
            Dim trainings As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Employee_Training_Sessions2_get_index", Session("UserEmpID"), Date.Now, Date.Now)
            Dim cc As Integer
            Dim yy As StringBuilder = New StringBuilder("")
            If trainings.Tables(0).Rows.Count > 0 Then
                cc = trainings.Tables(0).Rows.Count
                'trainer.InnerText = cc
                If cc > 0 Then
                    For i As Integer = 0 To cc - 1
                        Dim courses As String = Convert.ToString(trainings.Tables(0).Rows(i)("Course"))
                        yy.Append("<div><span class='glyphicon glyphicon-ok'></span> " + courses + "</div>")
                    Next
                End If
                Dim ww As String = yy.ToString()
                'train.InnerHtml = ww
            End If

            'Performance Metrics
            Dim metric As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_objective_Get_All", Session("UserEmpID"), Session("Organisation"))
            Dim ccc As Integer
            Dim yyy As StringBuilder = New StringBuilder("")
            If metric.Tables(0).Rows.Count > 0 Then
                ccc = metric.Tables(0).Rows.Count
                If ccc > 0 Then
                    For i As Integer = 0 To ccc - 1
                        Dim comp As String = Convert.ToString(metric.Tables(0).Rows(i)("objectives"))
                        yyy.Append("<div><span class='glyphicon glyphicon-ok'></span> " + comp + "</div>")
                    Next
                End If
                Dim ww As String = yyy.ToString()
                ' metrics.InnerHtml = ww
            End If

            'Performance Rating
            Dim rating As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Appraisal_Cycle_Get_index", Session("Organisation"), Session("UserEmpID"))
            Dim yyyy As StringBuilder = New StringBuilder("")
            If rating.Tables(0).Rows.Count > 0 Then
                Dim c As Integer = rating.Tables(0).Rows.Count
                Dim scorer(c) As String
                Dim yearer(c) As String
                If c > 0 Then
                    For i As Integer = 0 To c - 1
                        scorer(i) = Convert.ToString(rating.Tables(0).Rows(i)("Score"))
                        yearer(i) = Convert.ToString(rating.Tables(0).Rows(i)("Year"))
                        Dim start As String = Convert.ToString(rating.Tables(0).Rows(i)("Start"))
                        Dim ends As String = Convert.ToString(rating.Tables(0).Rows(i)("End"))
                        Dim period As String = start + " - " + ends + ":" + score + "%"
                        yyyy.Append("<div><span class='glyphicon glyphicon-ok'></span> " + period + "</div>")
                    Next
                End If
                score = String.Format("'{0}'", String.Join("','", scorer)).Replace("''", "")
                year = String.Join(",", (yearer))
                'Dim ww As String = yyyy.ToString()
                'rates.InnerHtml = ww
            End If

            'Job History
            Dim JobHistory As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Work_History_get_all", Session("UserEmpID"))
            Dim k As Integer
            Dim s As StringBuilder = New StringBuilder("")
            If JobHistory.Tables(0).Rows.Count > 0 Then
                k = JobHistory.Tables(0).Rows.Count
                If k > 0 Then
                    For i As Integer = 0 To k - 1
                        Dim jobtitle As String = Convert.ToString(JobHistory.Tables(0).Rows(i)("Job Title"))
                        Dim jobgrade As String = Convert.ToString(JobHistory.Tables(0).Rows(i)("Grade Level"))
                        Dim office As String = Convert.ToString(JobHistory.Tables(0).Rows(i)("Office"))
                        Dim location As String = Convert.ToString(JobHistory.Tables(0).Rows(i)("Location"))
                        Dim start As String = Convert.ToString(JobHistory.Tables(0).Rows(i)("Start Date"))
                        Dim ends As String = Convert.ToString(JobHistory.Tables(0).Rows(i)("End Date"))
                        If ends = "" Then
                            ends = "Present"
                        End If
                        Dim period As String = start + " - " + ends
                        s.Append("<div>" + jobtitle + "</div>")
                        s.Append("<div>" + jobgrade + "</div>")
                        s.Append("<div>" + office + "</div>")
                        s.Append("<span class='time'>" + period + "</span>")
                    Next
                End If
                Dim ww As String = s.ToString()
                'JobH.InnerHtml = ww
            End If

            'Next Performance Metrics
            Dim nextP As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Competency_JobGrade_Get_index_next", Session("UserEmpID"))
            Dim kk As Integer
            Dim ss As StringBuilder = New StringBuilder("")
            If nextP.Tables(0).Rows.Count > 0 Then
                kk = nextP.Tables(0).Rows.Count
                If kk > 0 Then
                    For i As Integer = 0 To kk - 1
                        Dim comp As String = Convert.ToString(nextP.Tables(0).Rows(i)("Competency"))
                        ss.Append("<div><span class='glyphicon glyphicon-ok'></span> " + comp + "</div>")
                    Next
                End If
                Dim ww As String = ss.ToString()
                'nxtP.InnerHtml = ww
            End If

            'Loans
            Dim strDashBoard As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Loan_get_index", Session("UserEmpID"), "Pending")
            Span1.InnerText = FormatNumber(strDashBoard.Tables(0).Rows.Count.ToString(), 0)
            strDashBoard.Clear()
            'Feedback Nuggets
            strDashBoard = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Appraisal_NuggetList_Owner_Get_All", Session("UserEmpID"), Session("Organisation"), "Dashboard")
            span2.InnerText = FormatNumber(strDashBoard.Tables(0).Rows.Count.ToString(), 0)
            'Payslip
            Dim strDashboard2 As DataSet
            strDashboard2 = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Payslip_count", Session("UserEmpID"))
            Span3.InnerText = FormatNumber(strDashboard2.Tables(0).Rows.Count.ToString(), 0)

            'scheduled Events
            Dim strEvent As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Calendar_Event_Get_All", Session("UserEmpID"), Date.Today.AddDays(1))
            Span4.InnerText = FormatNumber(strEvent.Tables(0).Rows.Count.ToString(), 0)
            Dim V2 As Integer = Integer.Parse(Span4.InnerText)
            'Today Event
            Dim strEvent1 As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Calendar_Event_Get_All", Session("UserEmpID"), Date.Today)
            Dim V1 As Integer = FormatNumber(strEvent1.Tables(0).Rows.Count.ToString(), 0)
            Span5.InnerText = V1 - V2
            'Overtime hrs
            Dim strOvertime As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Get_Over_hours", Session("UserEmpID"), Month(Date.Today))
            If strOvertime.Tables(0).Rows(0).Item("Overtime").ToString = "" Then
                span12.InnerText = "0"
            Else
                span12.InnerText = strOvertime.Tables(0).Rows(0).Item("Overtime").ToString
            End If

            'All days
            Dim strPresentDays As DataTable = Process.SearchDataP3("Time_Employee_Attendance_Get_All_Working", Session("UserEmpID"), DateSerial(Now.Year, Now.Month, 1), Date.Today)
            ' Dim strPresentDays As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Time_Employee_Attendance_Get_All", Session("UserEmpID"), DateSerial(Now.Year, Now.Month, 1), Date.Today)
            Dim foundRow As DataRow() = strPresentDays.[Select]("isworkday = 1")
            Dim foundRow1 As DataRow() = strPresentDays.[Select]("leaveid <> '0' and isworkday = 1")
            Dim foundRow2 As DataRow() = strPresentDays.[Select]("checkindate ='' and isworkday = 1 and leaveid = '0'")
            Span13.InnerText = foundRow.Length.ToString()
            span14.InnerText = foundRow1.Length.ToString
            span15.InnerText = foundRow2.Length.ToString()
            span16.InnerText = (foundRow.Length - (foundRow1.Length + foundRow2.Length)).ToString()
            'Leave
            Dim strLeave As DataTable
            strLeave = Process.SearchData("Emp_Leave_Chart", Session("UserEmpID"))

            Dim sum = IIf(IsDBNull(strLeave.Compute("SUM(totalbalance)", "")), "0", strLeave.Compute("SUM(totalbalance)", ""))
            'Dim sum2 As Integer = Convert.ToInt32(strLeave.Compute("SUM(ApprovedDays)", String.Empty))
            Dim sum2 = IIf(IsDBNull(strLeave.Compute("SUM(ApprovedDays)", "")), "0", strLeave.Compute("SUM(ApprovedDays)", ""))
            Dim sums As String = sum.ToString()
            Dim sums2 As String = sum2.ToString()
            Span6.InnerText = sums2.Split(".")(0)
            span7.InnerText = sums.Split(".")(0)

            'strDashBoard = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Employee_Leavelist_Approver_get_index", Session("UserEmpID"), "Pending")
            'Span5.InnerText = FormatNumber(strDashBoard.Tables(0).Rows.Count.ToString(), 0)
            strDashBoard.Clear()

            'Current length 
            cur_lenght = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Employee_Current_length_On_job", Session("UserEmpID")).ToString()

            Dim xx As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Avg_Performance_score_get_all", Session("Organisation")).ToString()
            If xx = "" Then
                cur_per = "0"
            Else
                cur_per = xx
            End If


            'Average Performance forcast
            Dim xxx As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Avg_Performance_forcast_get_all", Session("Organisation"), Session("UserEmpID")).ToString()
            If xxx = "" Then
                cur_per_forcast = "0"
            Else
                cur_per_forcast = xxx
            End If
            strDashBoard.Clear()

            'Dev Plan
            strDashBoard = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Development_Plan_Get_index", Session("UserEmpID"))
            ' Span6.InnerText = FormatNumber(strDashBoard.Tables(0).Rows.Count.ToString(), 0)
            strDashBoard.Clear()

            'Performance
            strDashBoard = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Appraisal_Summary_Employee_Get_index", Session("UserEmpID"))
            ' Span8.InnerText = FormatNumber(strDashBoard.Tables(0).Rows.Count.ToString(), 0)
            strDashBoard.Clear()

            'Query
            strDashBoard = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Employee_Query_Get_index", Session("UserEmpID"))
            'Span9.InnerText = FormatNumber(strDashBoard.Tables(0).Rows.Count.ToString(), 0)
            strDashBoard.Clear()

            'Training Count
            strDashBoard = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Employee_Training_Sessions2_get_all_index", Session("UserEmpID"))
            ' Span7.InnerText = FormatNumber(strDashBoard.Tables(0).Rows.Count.ToString(), 0)
            strDashBoard.Clear()

            'Work Anniversary
            strDashBoard = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Work_Anniversary_Get_All", Session("Organisation"))
            Dim n As StringBuilder = New StringBuilder("")
            Dim m As Integer = strDashBoard.Tables(0).Rows.Count
            If m > 0 Then
                For i As Integer = 0 To m - 1
                    Dim name As String = Convert.ToString(strDashBoard.Tables(0).Rows(i)("Name"))
                    Dim dept As String = Convert.ToString(strDashBoard.Tables(0).Rows(i)("Office"))
                    Dim start As String = Convert.ToString(strDashBoard.Tables(0).Rows(i)("StartWork"))
                    n.Append("<li><div class='experience-user'><div class='before-circle'></div></div>")
                    n.Append("<div class='experience-content'><div class='timeline-content'><a href='#/' class='name'>" + name + "</a>")
                    n.Append("<div></div><span class='time'>" + dept + "</span>") '
                    n.Append("<span class='time'>Date Joined: " + start + "</span></div></div></li>")
                Next
            Else
                n.Append("<li><div class='experience-user'><div class='before-circle'></div></div>")
                n.Append("<div class='experience-content'><div class='timeline-content'><a href='#/' class='name'></a>")
                n.Append("<div>No Work Anniversary</div><span class='time'></span>") '
                n.Append("<span class='time'></span></div></div></li>")
            End If
            Dim rr As String = n.ToString()
            'work_history.InnerHtml = rr
            strDashBoard.Clear()

            'Development plan
            strDashBoard = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Development_Plan_Get_All", Session("UserEmpID"))
            Dim kkk, a, b As Integer
            Dim p_d_id As String
            Dim myobj As String
            Dim inter As String
            Dim training, targetdate As String
            Dim sss As StringBuilder = New StringBuilder("")
            If strDashBoard.Tables(0).Rows.Count > 0 Then
                kkk = strDashBoard.Tables(0).Rows.Count
                If kkk > 0 Then
                    For i As Integer = 0 To kkk - 1
                        Dim plan_id As String = Convert.ToString(strDashBoard.Tables(0).Rows(i)("id"))
                        Dim dev1 As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Development_Plan_Detail_Get_All", plan_id)
                        Dim j As Integer = dev1.Tables(0).Rows.Count
                        If j > 0 Then
                            For x As Integer = 0 To j - 1
                                p_d_id = Convert.ToString(dev1.Tables(0).Rows(x)("id"))
                                myobj = Convert.ToString(dev1.Tables(0).Rows(x)("MyObjectives"))
                                inter = Convert.ToString(dev1.Tables(0).Rows(x)("intervention"))
                                targetdate = Convert.ToString(dev1.Tables(0).Rows(x)("targetdate"))
                                Dim dev2 As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Development_Plan_Training_Get_All_index", p_d_id)
                                a = dev2.Tables(0).Rows.Count
                                If a > 0 Then
                                    For g As Integer = 0 To a - 1
                                        training = Convert.ToString(dev2.Tables(0).Rows(g)("Training"))
                                        sss.Append("<tr><td><span class='block text-ellipsis'><span class='text-xs'>" + inter + "</span></span></td>")
                                        sss.Append("<td class='text'><span class='block text-ellipsis'><span class='text-muted'>" + training + "</span></span></td>")
                                        sss.Append("<td><h2><a href='#'>" + targetdate + "</a></h2></td></tr>")
                                    Next
                                End If
                            Next
                        End If
                    Next
                End If
                Dim ww As String = sss.ToString()
                'dev_plan.InnerHtml = ww
            Else
                Dim v As StringBuilder = New StringBuilder("")
                v.Append("<tr><td></td><td style='width:100px;'><span class='block text-muted'><span class='text-xs'>No Development Plan</span></span></td><td></td></tr>")
                Dim ww As String = v.ToString()
                'dev_plan.InnerHtml = ww
            End If
            strDashBoard.Clear()

            'My Tasks
            Dim strtest As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "My_Tasks_Get_All", Session("UserEmpID"), Date.Now)
            Dim o As StringBuilder = New StringBuilder("")
            If strtest.Tables(0).Rows.Count > 0 Then
                Dim ll As Integer = strtest.Tables(0).Rows.Count
                If ll > 0 Then
                    For i As Integer = 0 To ll - 1
                        Dim ce As cal_events = New cal_events()
                        ce.start = Convert.ToString(strtest.Tables(0).Rows(i)("EventDate"))
                        ce.eventdes = Convert.ToString(strtest.Tables(0).Rows(i)("EventDescription"))
                        'ce.enddate = Convert.ToString(strtest.Tables(0).Rows(i)("EventEndDate"))
                        'ce.eventstat = Convert.ToString(strtest.Tables(0).Rows(i)("EventStat"))
                        ce.eventtime = Convert.ToString(strtest.Tables(0).Rows(i)("EventTime"))
                        ce.title = Convert.ToString(strtest.Tables(0).Rows(i)("EventTitle"))
                        'ce.className = "bg-success"
                        'ce.allDay = False                  
                        o.Append("<li><div class='experience-user'><div class='before-circle'></div></div>")
                        o.Append("<div class='experience-content'><div class='timeline-content'><a href='#/' class='name'>" + ce.title + "</a>")
                        o.Append("<div></div><span class='time'><b>Start Date:</b> " + ce.start + "</span>") '
                        If ce.eventdes <> "" Then
                            o.Append("<span class='time'><b>Time:</b> " + ce.eventtime + "</span></div></div></li>")
                        Else
                            o.Append("<span class='time'><b>End Date:</b> " + ce.eventtime + "</span></div></div></li>")
                        End If
                    Next
                End If
            Else
                o.Append("<li><div class='experience-user'><div class='before-circle'></div></div>")
                o.Append("<div class='experience-content'><div class='timeline-content'><a href='#/' class='name'></a>")
                o.Append("<div>No Task</div><span class='time'></span>") '
                o.Append("<span class='time'></span></div></div></li>")
            End If
            Dim mo As String = o.ToString()
            'taskss.InnerHtml = mo

            'Average Length of Stay
            Dim ii, cemp, ans As Double
            Dim oo As String = "(select count(*) from dbo.Employees_All b where b.Terminated = 'No' and b.Office in (Select m.companys from Fn_Company_Filter('" + Session("Organisation") + "') m))"

            If IsDBNull(SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Employee_Avg_length_On_job", Session("Organisation"))) = False And IsDBNull(SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Employee_Avg_length_On_job", Session("Organisation"))) = False Then
                ii = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Employee_Avg_length_On_job", Session("Organisation"))
                cemp = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, oo)
                ans = (ii / cemp)
                'avgLength.InnerText = Math.Round(ans, 0)
            Else
                ans = 0
                'avgLength.InnerText = Math.Round(ans, 0)
            End If
            LoadSkill(Session("UserEmpID"))
            strDashBoard.Clear()
        End If
    End Sub
    Protected Sub InitiateNew(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Response.Redirect("~/cal_view", True)
        Catch ex As Exception

        End Try

    End Sub
    Private Sub LoadSkill(ByVal EmpID As String)
        Try
            Dim datatables As New DataTable
            datatables = Process.SearchData("Actual_skills_get", EmpID)
            dlEducation.DataSource = datatables
            dlEducation.DataBind()
        Catch ex As Exception

        End Try
    End Sub
End Class