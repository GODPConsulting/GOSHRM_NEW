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

Public Class hrdashboard2
    Inherits System.Web.UI.Page
    Public male, female, no, jobtitle As String
    Public CompletionStatusName, CompletionStatusNameScore As String
    Public PerformanceName, PerformanceNameScore As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Company Table list
        Dim strDashBoard As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Company_Structure_Get_ByLevel", 0, "")
        Dim n As Integer = strDashBoard.Tables(0).Rows.Count
        Dim s As StringBuilder = New StringBuilder("")
        If n > 0 Then
            For i As Integer = 0 To n - 1
                Dim name As String = Convert.ToString(strDashBoard.Tables(0).Rows(i)("name"))
                Dim noOFemployees As Integer = Convert.ToInt32(strDashBoard.Tables(0).Rows(i)("employees"))
                Dim location As String = Convert.ToString(strDashBoard.Tables(0).Rows(i)("location"))
                s.Append("<tr><td class='table-data td'><a onclick='viewdashboardInfo(" + name + ")' href='#'>" & name & "</a></td>")
                s.Append("<td class='table-data'>" & noOFemployees & "</td>")
                s.Append("<td class='table-data'>" & location & "</td>")
                s.Append("<td class='table-data'>0%</td></tr>")
            Next
        End If
        Dim rr As String = s.ToString()
        companytb.InnerHtml = rr
        strDashBoard.Clear()

    End Sub
End Class