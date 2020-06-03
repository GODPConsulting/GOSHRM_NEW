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

Public Class searchpage
    Inherits System.Web.UI.Page
    Public yaer As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            If Session("jobsearch") = "" Then
                Process.loadalert(divalert, msgalert, "No Jobs Available", "info")
            Else
                Try
                    Dim txt_search As String = Session("jobsearch")
                    Dim loc_search As String = Session("location")
                    Dim spec_search As String = Session("spec")
                    'txt_search = "%" + txt_search + "%"
                    'loc_search = "%" + loc_search + "%"
                    'spec_search = "%" + spec_search + "%"
                    'Dim query As String = "SELECT * FROM Recruit_Job_Post WHERE title LIKE ('" + txt_search + "' OR type LIKE '" + txt_search + "' or location like '" + loc_search + "' or Specialization like '" + spec_search + "') and ClosingDate > '" + CDate(Date.Now) + ""
                    Dim calds As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "job_search_get_all", txt_search, loc_search, spec_search)
                    If calds.Tables(0).Rows.Count > 0 Then
                        Dim c As Integer = calds.Tables(0).Rows.Count
                        Process.loadalert(divalert, msgalert, c.ToString() + " result(s) found", "success")
                        Dim y As StringBuilder = New StringBuilder("")
                        If c > 0 Then
                            For i As Integer = 0 To c - 1
                                Dim jobtitle As String = Convert.ToString(calds.Tables(0).Rows(i)("Title"))
                                Dim jobtype As String = Convert.ToString(calds.Tables(0).Rows(i)("Type"))
                                Dim jobdesc As String = Convert.ToString(calds.Tables(0).Rows(i)("JobDescription"))
                                Dim specialize As String = Convert.ToString(calds.Tables(0).Rows(i)("Specialization"))
                                Dim location As String = Convert.ToString(calds.Tables(0).Rows(i)("location"))
                                Dim code As String = Convert.ToString(calds.Tables(0).Rows(i)("code"))
                                Dim ccsdate As String = CDate(calds.Tables(0).Rows(i).Item("closingdate").ToString).ToShortDateString
                                Dim url As String = "Vacancyview?Code=" + code
                                y.Append("<div class='row'><div class='job-list'><div class='job-list-content'>")
                                y.Append("<h4><a href='#'>" + jobtitle + "</a><span class='full-time'>" + jobtype + "</span></h4>")
                                y.Append("<p><b>Job Description</b><br/>" + jobdesc + "</p>")
                                y.Append("<div class='job-tag'><div class='pull-left'><div class='meta-tag'>")
                                y.Append("<span><a href='#'><i class='ti-brush'></i>" + specialize + "</a></span>")
                                y.Append("<span><i class='ti-location-pin'></i>" + location + "</span><span><i class='ti-time'></i>Closing Date: " + ccsdate + "</span></div></div>")
                                y.Append("<div class='pull-right'>")
                                y.Append("<a href='" + url + "' class='btn btn-common btn-rm'>More Detail</a>")
                                y.Append("</div></div></div></div></div>")

                            Next
                        Else
                            Process.loadalert(divalert, msgalert, "The Job you are looking for is not available", "info")
                        End If
                        Dim ww As String = y.ToString()
                        job_list.InnerHtml = ww
                    Else
                        Process.loadalert(divalert, msgalert, "The Job you are looking for is not available", "info")
                        job_list.InnerHtml = ""
                    End If
                Catch ex As Exception
                    Process.loadalert(divalert, msgalert, ex.Message, "danger")
                End Try
            End If
        End If

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

        yaer = Convert.ToString(Date.Now.Year)
    End Sub


    Protected Sub btn_search_Click(sender As Object, e As EventArgs)
        Try
            job_list.InnerHtml = ""
            If search.Value = "" Then
                Process.loadalert(divalert, msgalert, "Please enter a valid keyword", "danger")
                Exit Sub
            End If

            Dim txt, loc, spec As String
            txt = (search.Value).ToString()
            loc = (locations.Value).ToString()
            spec = (Specializations.Value).ToString()
            'txt += "%"
            'loc += "%"
            'spec += "%"
            'Dim query As String = "SELECT * FROM Recruit_Job_Post WHERE title LIKE '" + txt + "' OR type LIKE '" + txt + "' or location like '" + loc + "' or Specialization like'" + spec + "' and ClosingDate > '" + Process.DDMONYYYY(Date.Now) + ""
            Dim cald As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "job_search_get_all", txt, loc, spec)
            If cald.Tables(0).Rows.Count > 0 Then
                Dim c As Integer = cald.Tables(0).Rows.Count
                Process.loadalert(divalert, msgalert, c.ToString() + " result(s) found", "success")
                Dim y As StringBuilder = New StringBuilder("")
                If c > 0 Then
                    For i As Integer = 0 To cald.Tables(0).Rows.Count - 1
                        Dim jobtitle As String = Convert.ToString(cald.Tables(0).Rows(i)("Title"))
                        Dim jobtype As String = Convert.ToString(cald.Tables(0).Rows(i)("Type"))
                        Dim jobdesc As String = Convert.ToString(cald.Tables(0).Rows(i)("JobDescription"))
                        Dim specialize As String = Convert.ToString(cald.Tables(0).Rows(i)("Specialization"))
                        Dim location As String = Convert.ToString(cald.Tables(0).Rows(i)("location"))
                        Dim code As String = Convert.ToString(cald.Tables(0).Rows(i)("code"))
                        Dim ccdate As String = CDate(cald.Tables(0).Rows(i).Item("closingdate").ToString).ToShortDateString
                        Dim url As String = "Vacancyview?Code=" + code
                        y.Append("<div class='row'><div class='job-list'><div class='job-list-content'>")
                        y.Append("<h4><a href='#'>" + jobtitle + "</a><span class='full-time'>" + jobtype + "</span></h4>")
                        y.Append("<p><b>Job Description</b><br/>" + jobdesc + "</p>")
                        y.Append("<div class='job-tag'><div class='pull-left'><div class='meta-tag'>")
                        y.Append("<span><a href='#'><i class='ti-brush'></i>" + specialize + "</a></span>")
                        y.Append("<span><i class='ti-location-pin'></i>" + location + "</span><span><i class='ti-time'></i>Closing Date: " + ccdate + "</span></div></div>")
                        y.Append("<div class='pull-right'>")
                        y.Append("<a href='" + url + "' class='btn btn-common btn-rm'>More Detail</a>")
                        y.Append("</div></div></div></div></div>")

                    Next
                Else
                    Process.loadalert(divalert, msgalert, "The Job you are looking for is not available", "info")
                End If
                Dim ww As String = y.ToString()
                job_list.InnerHtml = ww
            Else
                Process.loadalert(divalert, msgalert, "The Job you are looking for is not available", "info")
                job_list.InnerHtml = ""
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
End Class