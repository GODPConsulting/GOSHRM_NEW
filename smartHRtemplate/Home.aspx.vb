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

Public Class Home
    Inherits System.Web.UI.Page
    Public train, per, leave, job, inter, emp, today As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        today = String.Format("{0: MMMM d, yyyy}", Date.Now.Date)
        Dim strDashBoard As New DataSet
        'Training
        strDashBoard = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Employee_Training_Sessions2_get_all", Session("UserEmpID"), Date.Now, Date.Now.AddYears(10))
        train = FormatNumber(strDashBoard.Tables(0).Rows.Count.ToString(), 0)
        strDashBoard.Clear()

        'Appraisal
        strDashBoard = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Appraisal_Summary_Employee_Get_All", Session("UserEmpID"))
        per = FormatNumber(strDashBoard.Tables(0).Rows.Count.ToString(), 0)
        strDashBoard.Clear()

        'Upcoming Leave
        strDashBoard = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Employee_Leavelist_dashboard", Session("UserEmpID"), "Approved", Date.Now, Date.Now.AddYears(10))
        leave = FormatNumber(strDashBoard.Tables(0).Rows.Count.ToString(), 0)
        strDashBoard.Clear()

        'Active Job Post
        strDashBoard = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Job_Post_dashboard", "Open", Session("Access"))
        job = FormatNumber(strDashBoard.Tables(0).Rows.Count.ToString(), 0)
        strDashBoard.Clear()

        'Upcoming Interview
        strDashBoard = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Job_Interview_Get_Upcoming", Date.Now)
        inter = FormatNumber(strDashBoard.Tables(0).Rows.Count.ToString(), 0)
        strDashBoard.Clear()

        'Employees
        strDashBoard = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_PersonalDetail_dashboard", Session("Access"))
        emp = FormatNumber(strDashBoard.Tables(0).Rows.Count.ToString(), 0)
        strDashBoard.Clear()

        Dim cald As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Calendar_Event_Get_All", Session("UserEmpID"), Date.Now)
        If cald.Tables(0).Rows.Count > 0 Then
            Dim c As Integer = cald.Tables(0).Rows.Count
            Dim y As StringBuilder = New StringBuilder("")
            If c > 0 Then
                For i As Integer = 0 To c - 1
                    Dim des As String = Convert.ToString(cald.Tables(0).Rows(i)("EventDescription"))
                    Dim datte As String = Convert.ToString(cald.Tables(0).Rows(i)("EventDate"))
                    Dim time As String = Convert.ToString(cald.Tables(0).Rows(i)("EventTime"))

                    y.Append("<tr><td><a href='#'>" + des + "</a></td>")
                    y.Append("<td><h2>" + datte + "</h2></td>")
                    y.Append("<td>" + time + "</td></tr>")
                Next
            End If
            Dim ww As String = y.ToString()
            caldatas.InnerHtml = ww
        End If
    End Sub

End Class