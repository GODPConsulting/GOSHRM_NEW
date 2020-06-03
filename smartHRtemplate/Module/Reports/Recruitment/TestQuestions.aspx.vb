Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Configuration
Imports Microsoft.Reporting.WebForms
Imports System.IO
Public Class TestQuestions
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "JOBTESTREP"
    Dim emailFile As String = ConfigurationManager.AppSettings("EmailURL")

    Private Sub LodaDataTable(JobID As Integer, TestTitle As String)
        Dim Datas As New DataTable
        Datas = Process.SearchDataP2("Recruit_Job_Test_Questions_Report", JobID, TestTitle)
        Generatereport(Datas)
    End Sub
    Private Sub Generatereport(dt As DataTable)
        ReportViewer1.ProcessingMode = ProcessingMode.Local
        ReportViewer1.SizeToReportContent = True
        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Module/Recruitment/Reports/TestQuestion.rdlc")
        'ReportViewer1.LocalReport.DataSources.Clear()
        Dim _rsource As New ReportDataSource("TestQuestion", dt)
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

                Process.LoadRadComboTextAndValueP2(cboJobPost, "Recruit_Job_Post_Get_All", "", cboCompany.SelectedValue, "Job Title", "Code", False)
                Process.LoadRadComboTextAndValueP1(cboJobTest, "Recruit_Job_Test_Get_By_JobID", cboJobPost.SelectedValue, "TestTitle", "TestTitle", False)
                LodaDataTable(cboJobPost.SelectedValue, cboJobTest.SelectedItem.Text)
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    Protected Sub cboJobPost_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboJobPost.SelectedIndexChanged
        Try
            Process.LoadRadComboTextAndValueP1(cboJobTest, "Recruit_Job_Test_Get_By_JobID", cboJobPost.SelectedValue, "TestTitle", "TestTitle")
        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    Protected Sub btnSend_Click(sender As Object, e As EventArgs) Handles btnSend.Click
        Try
            LodaDataTable(cboJobPost.SelectedValue, cboJobTest.SelectedItem.Text)
        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    Private Sub cboCompany_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboCompany.SelectedIndexChanged
        Try
            Process.LoadRadComboTextAndValueP2(cboJobPost, "Recruit_Job_Post_Get_All", "", cboCompany.SelectedValue, "Job Title", "Code", False)
            Process.LoadRadComboTextAndValueP1(cboJobTest, "Recruit_Job_Test_Get_By_JobID", cboJobPost.SelectedValue, "TestTitle", "TestTitle", False)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
End Class