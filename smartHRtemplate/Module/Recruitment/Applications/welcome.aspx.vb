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
Public Class welcome
    Inherits System.Web.UI.Page
    Public job_cv, job_app, job_comp, job_post, yaer As String
    Public company As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim q As String = "select name from company_structure where parent = 'N/A'"
        Dim qq As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, q)
        Session("Organisations") = qq

        company = qq
        'location get all
        Dim cald As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "location_get_all")
        If cald.Tables(0).Rows.Count > 0 Then
            Dim c As Integer = cald.Tables(0).Rows.Count
            Dim y As StringBuilder = New StringBuilder("")
            If c > 0 Then
                For i As Integer = 0 To c - 1
                    Dim name As String = Convert.ToString(cald.Tables(0).Rows(i)("Name"))
                    Dim id As String = Convert.ToString(cald.Tables(0).Rows(i)("id"))
                    locations.Items.Add(New ListItem(name, name))
                Next
            End If
            Dim ww As String = y.ToString()
            'locations.InnerText = ww
        End If

        Dim strUser As New DataSet
        strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "general_info_get")
        If IsDBNull(strUser.Tables(0).Rows(0).Item("imgLogo")) Or strUser.Tables(0).Rows(0).Item("imgLogo").ToString.Trim = "" Then
            imgProfile.ImageUrl = imgClear.ImageUrl
        Else
            imgProfile.ImageUrl = "~/Module/Admin/Organisation/CompanyLogo.ashx"
        End If

        'Job Post Count
        job_post = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select count(*) from Recruit_Job_Post where [status] not in ('closed')")

        'Job Applicants Count
        job_app = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select count(*) from Recruit_Applicants")

        'Job Resume Counts
        job_cv = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select count(*) from Recruit_Applications")

        'Job Company Counts
        job_comp = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select count(*) from Location")

        yaer = Convert.ToString(Date.Now.Year)
    End Sub

    Protected Sub btn_search_Click(sender As Object, e As EventArgs)
        Try
            Dim txt, loc, spec As String
            txt = (search.Value).ToString()
            loc = (locations.Value).ToString()
            spec = Specializations.Value

            If search.Value = "" Then
                Process.loadalert(divalert, msgalert, "Please enter a valid keyword", "danger")
                Exit Sub
            End If

            'txt += ""
            'loc += ""
            'spec += ""
            Dim characters As Char() = txt.ToCharArray()
            'Dim query As String = "SELECT * FROM Recruit_Job_Post WHERE title LIKE '" + txt + "' OR type LIKE '" + txt + "' or location like '" + loc + "' or Specialization like '" + spec + "'"
            Dim cald As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "job_search_get_all", txt, loc, spec)
            'Dim cald As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, query)
            If cald.Tables(0).Rows.Count > 0 Then
                Session("jobsearch") = search.Value
                Session("location") = loc
                Session("spec") = spec
                Response.Redirect("searchpage.aspx")
            Else
                Process.loadalert(divalert, msgalert, "The Job you are looking for is not available", "danger")
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

End Class