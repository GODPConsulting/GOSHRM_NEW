Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Configuration
Imports Microsoft.Reporting.WebForms
Imports System.IO
Public Class TrainingSession
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "TRASESSIONREPORT"
    Dim emailFile As String = ConfigurationManager.AppSettings("EmailURL")

    Private Sub LodaDataTable(sessionid As Integer)
        Dim sessiondata As New DataTable
        sessiondata = Process.SearchData("Training_Sessions_Report", sessionid)
        Dim sessiontrainer As DataTable = Process.SearchData("Training_Sessions_Trainer_Report", sessionid)
        Dim sessiontrainee As DataTable = Process.SearchDataP4("Training_Sessions_Trainee_Report", sessionid, chkTrainAssessment.Checked, chkLearnAssessment.Checked, True)
        Generatereport(sessiondata, sessiontrainer, sessiontrainee)
        pagetitle.InnerText = cboCompany.SelectedValue & ": " & cboSession.SelectedItem.Text
    End Sub
    Private Sub Generatereport(projectdata As DataTable, projectactivity As DataTable, projectmemactivity As DataTable)
        ReportViewer1.ProcessingMode = ProcessingMode.Local
        ReportViewer1.SizeToReportContent = True
        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Module/Reports/Trainings/TrainingSession.rdlc")

        Dim _rsource As New ReportDataSource("Sessions", projectdata)
        Dim _rsource1 As New ReportDataSource("Trainer", projectactivity)
        Dim _rsource2 As New ReportDataSource("trainee", projectmemactivity)
        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.LocalReport.DataSources.Add(_rsource)
        ReportViewer1.LocalReport.DataSources.Add(_rsource1)
        ReportViewer1.LocalReport.DataSources.Add(_rsource2)
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
                Process.LoadRadComboTextAndValueInitiate(cboCourse, "Courses_get_all", "--Select--", "Course Title", "id")
                'LodaDataTable(cboJobPost.SelectedValue)
            End If
        Catch ex As Exception
            response.write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub


    Protected Sub btnSend_Click(sender As Object, e As EventArgs) Handles btnSend.Click

        Try

            LodaDataTable(cboSession.SelectedValue)
        Catch ex As Exception
            response.write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try

    End Sub

    Protected Sub cboClient_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboCourse.SelectedIndexChanged
        Try

            Process.LoadRadComboTextAndValueP2(cboSession, "Training_Sessions_Course_Get", cboCourse.SelectedValue, cboCompany.SelectedValue, "Name", "id", False)
        Catch ex As Exception
            response.write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub


    Private Sub cboCompany_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboCompany.SelectedIndexChanged
        Try

            Process.LoadRadComboTextAndValueP2(cboSession, "Training_Sessions_Course_Get", cboCourse.SelectedValue, cboCompany.SelectedValue, "Name", "id", False)
        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub
End Class