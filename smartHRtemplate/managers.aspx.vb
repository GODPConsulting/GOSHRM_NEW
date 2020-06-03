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

Public Class managers
    Inherits System.Web.UI.Page
    'Public male, female, no, jobtitle As String
    'Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    '    'Direct Report Count
    '    Dim strDashBoard As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_PersonalDetail_DirectReports", Session("UserEmpID"))
    '    Gcount.InnerText = FormatNumber(strDashBoard.Tables(0).Rows.Count.ToString(), 0)
    '    strDashBoard.Clear()

    '    'Male to Female Ratio
    '    strDashBoard = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_PersonalDetail_DirectReports_gender_ratio", Session("UserEmpID"))
    '    Dim m As Integer = strDashBoard.Tables(0).Rows.Count
    '    If m > 0 Then
    '        For i As Integer = 0 To m - 1
    '            male = Convert.ToString(strDashBoard.Tables(0).Rows(i)("male"))
    '            female = Convert.ToString(strDashBoard.Tables(0).Rows(i)("female"))
    '            'Dim total As String = Convert.ToString(strDashBoard.Tables(0).Rows(i)("total"))
    '        Next
    '    End If
    '    strDashBoard.Clear()

    '    'jobtitle(Distribution)
    '    strDashBoard = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_PersonalDetail_DirectReports_jobtitle_distribution", Session("UserEmpID"))
    '    Dim n As Integer = strDashBoard.Tables(0).Rows.Count
    '    Dim s As StringBuilder = New StringBuilder("")
    '    If n > 0 Then
    '        For i As Integer = 0 To m - 1
    '            no = Convert.ToString(strDashBoard.Tables(0).Rows(i)("No"))
    '            jobtitle = Convert.ToString(strDashBoard.Tables(0).Rows(i)("JobTitle"))
    '            s.Append("<li><div class='experience-user'><div class='before-circle'></div></div>")
    '            s.Append("<div class='experience-content'><div class='timeline-content'>")
    '            s.Append("<a href='#/' class='name'>" + jobtitle + " (" + no + ")</a>")
    '            s.Append("<div></div></div></div></li>")
    '        Next
    '    End If
    '    Dim rr As String = s.ToString()
    '    distribute.InnerHtml = rr
    '    strDashBoard.Clear()
    'End Sub


End Class