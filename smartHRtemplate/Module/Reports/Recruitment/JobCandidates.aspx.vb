Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Configuration
Imports Microsoft.Reporting.WebForms
Imports System.IO
Public Class JobCandidates
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "JOBAPPREPORT"
    Dim emailFile As String = ConfigurationManager.AppSettings("EmailURL")

    Private Sub LodaDataTable(JobID As Integer)
        Dim Datas As New DataTable
        Datas = Process.SearchData("Recruit_Job_Applicants_Report", JobID)
        Generatereport(Datas)
    End Sub
    Private Sub Generatereport(dt As DataTable)
        ReportViewer1.ProcessingMode = ProcessingMode.Local
        ReportViewer1.SizeToReportContent = True
        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Module/Reports/Recruitment/JobCandidates.rdlc")

        Dim _rsource As New ReportDataSource("JobCandidates", dt)
        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.LocalReport.DataSources.Add(_rsource)
        ReportViewer1.LocalReport.Refresh()
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Dim ismulti As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select isnull(ismulticompany,'No')  from general_info")
                If ismulti.ToLower = "no" Then
                    Process.LoadRadComboTextAndValueP2(cboCompany, "Company_Structure_Get_ByLevel", "1", Session("Access"), "name", "name", False)
                Else
                    Process.LoadRadComboTextAndValueP2(cboCompany, "Company_Structure_Get_ByLevel", "2", Session("Access"), "name", "name", False)
                End If

                Process.AssignRadComboValue(cboCompany, Session("Organisation"))

                Process.LoadRadComboTextAndValueInitiateP2(cboJobPost, "Recruit_Job_Post_Get_All", "", cboCompany.SelectedValue, "--Select--", "Job Title", "Code")
                'LodaDataTable(cboJobPost.SelectedValue)
            End If
        Catch ex As Exception
            response.write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub


    Protected Sub btnSend_Click(sender As Object, e As EventArgs) Handles btnSend.Click

        Try
            LodaDataTable(cboJobPost.SelectedValue)
        Catch ex As Exception
            response.write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try

    End Sub

    Private Sub cboCompany_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboCompany.SelectedIndexChanged
        Try
            Process.LoadRadComboTextAndValueInitiateP2(cboJobPost, "Recruit_Job_Post_Get_All", "", cboCompany.SelectedValue, "--Select--", "Job Title", "Code")
        Catch ex As Exception

        End Try
    End Sub
End Class