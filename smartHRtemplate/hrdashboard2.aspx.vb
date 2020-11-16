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
        Dim strDashBoard As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Company_Structure_Get_ByLevel", 0, Session("Access"))
        Dim n As Integer = strDashBoard.Tables(0).Rows.Count
        Dim s As StringBuilder = New StringBuilder("")
        If n > 0 Then
            For i As Integer = 0 To n - 1
                Dim name As String = Convert.ToString(strDashBoard.Tables(0).Rows(i)("name"))
                Dim noOFemployees As Integer = Convert.ToInt32(strDashBoard.Tables(0).Rows(i)("employees"))
                Dim location As String = Convert.ToString(strDashBoard.Tables(0).Rows(i)("location"))
                Dim strEmpcount As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "select * from company_structure where parent ='" & name & "'")
                Dim k As Integer = strEmpcount.Tables(0).Rows.Count
                'If k > 0 Then
                '    For j As Integer = 0 To k - 1
                '        Dim compName As String = Convert.ToString(strEmpcount.Tables(0).Rows(j)("name"))
                '        Dim compCount As Integer = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "Select count(*) from dbo.Employees_All e where Terminated = 'No' and e.Office = '" & compName & "'")
                '        noOFemployees = noOFemployees + compCount
                '        Dim strEmpcount2 As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "select * from company_structure where parent ='" & compName & "'")
                '        Dim z As Integer = strEmpcount2.Tables(0).Rows.Count
                '        If z > 0 Then
                '            For y As Integer = 0 To z - 1
                '                Dim compName2 As String = Convert.ToString(strEmpcount2.Tables(0).Rows(y)("name"))
                '                Dim compCount2 As Integer = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "Select count(*) from dbo.Employees_All e where Terminated = 'No' and e.Office = '" & compName2 & "'")
                '                noOFemployees = noOFemployees + compCount2
                '                Dim strEmpcount3 As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "select * from company_structure where parent ='" & compName2 & "'")
                '                Dim a As Integer = strEmpcount3.Tables(0).Rows.Count
                '                If a > 0 Then
                '                    For t As Integer = 0 To a - 1
                '                        Dim compName3 As String = Convert.ToString(strEmpcount3.Tables(0).Rows(t)("name"))
                '                        Dim compCount3 As Integer = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "Select count(*) from dbo.Employees_All e where Terminated = 'No' and e.Office = '" & compName3 & "'")
                '                        noOFemployees = noOFemployees + compCount3
                '                        Dim strEmpcount4 As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "select * from company_structure where parent ='" & compName3 & "'")
                '                        Dim b As Integer = strEmpcount4.Tables(0).Rows.Count
                '                        If b > 0 Then
                '                            For tt As Integer = 0 To b - 1
                '                                Dim compName4 As String = Convert.ToString(strEmpcount4.Tables(0).Rows(tt)("name"))
                '                                Dim compCount4 As Integer = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "Select count(*) from dbo.Employees_All e where Terminated = 'No' and e.Office = '" & compName4 & "'")
                '                                noOFemployees = noOFemployees + compCount3
                '                            Next
                '                        End If
                '                    Next
                '                End If
                '            Next
                '        End If
                '    Next
                'End If

                s.Append("<tr role = 'row' class='odd'><td class='sorting_1'>")
                's.Append("<a href = '#' class='avatar table-data td' href = '#' onclick='viewdashboardInfo(""" & name & """)'>" & name.Substring(0, 1) & "</a>")
                s.Append("<h2> <a class='table-data td' href = '#' onclick='viewdashboardInfo(""" & name & """)'>" & name & "</a></h2></td>")
                s.Append("<td> " & noOFemployees & "</td>")
                s.Append("<td>" & location & "</td>")
                s.Append("<td>0%</td>")
                s.Append("<td><a href = '#' onclick='viewdashboardInfo(""" & name & """)' class='btn btn-white btn-sm rounded table-data td' aria-expanded='false'>View Details</a></td></tr>")
            Next
        End If
        Dim rr As String = s.ToString()
        companytb.InnerHtml = rr
        strDashBoard.Clear()

    End Sub
End Class