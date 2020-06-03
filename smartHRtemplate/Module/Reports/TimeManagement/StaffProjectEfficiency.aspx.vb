Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Configuration
Imports Microsoft.Reporting.WebForms
Imports System.IO
Public Class StaffProjectEfficiency
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "STAFFPROJECTREPORT"
    Dim emailFile As String = ConfigurationManager.AppSettings("EmailURL")

    Private Sub LodaDataTable(projectID As Integer, EmpID As String)
        Dim projectdata As New DataTable
        projectdata = Process.SearchData("Time_Projects_Report", projectID)
        Dim projectactivity As DataTable = Process.SearchDataP2("Time_Projects_Staff_Activities_Report", projectID, EmpID)
        Dim projectmemactivity As DataTable = Process.SearchDataP2("Time_Projects_Staff_Member_Activities_Report", projectID, EmpID)
        Generatereport(projectdata, projectactivity, projectmemactivity)
    End Sub
    Private Sub Generatereport(projectdata As DataTable, projectactivity As DataTable, projectmemactivity As DataTable)
        ReportViewer1.ProcessingMode = ProcessingMode.Local
        ReportViewer1.SizeToReportContent = True
        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Module/Reports/TimeManagement/StaffProjectEfficiency.rdlc")
        'ReportViewer1.LocalReport.DataSources.Clear()
        Dim _rsource As New ReportDataSource("Project", projectdata)
        Dim _rsource2 As New ReportDataSource("Project_Activities_Summary", projectactivity)
        Dim _rsource3 As New ReportDataSource("Member_Activity", projectmemactivity)
        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.LocalReport.DataSources.Add(_rsource)
        ReportViewer1.LocalReport.DataSources.Add(_rsource2)
        ReportViewer1.LocalReport.DataSources.Add(_rsource3)
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
                Process.LoadRadComboTextAndValueInitiate(cboClient, "Time_Clients_Get_All", "--Select--", "Name", "Name")
                'LodaDataTable(cboJobPost.SelectedValue)
            End If
        Catch ex As Exception
            response.write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub


    Protected Sub btnSend_Click(sender As Object, e As EventArgs) Handles btnSend.Click

        Try

            LodaDataTable(cboProject.SelectedValue, cboMember.SelectedValue)
        Catch ex As Exception
            response.write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try

    End Sub

    Protected Sub cboClient_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboClient.SelectedIndexChanged
        Try

            Process.LoadRadComboTextAndValueP2(cboProject, "Time_Projects_Get_Clients", cboClient.SelectedValue, cboCompany.SelectedValue, "Name", "id", False)
            Process.LoadRadComboTextAndValueP1(cboMember, "Time_Projects_Members_Get_All", cboProject.SelectedValue, "Name", "EmpID", False)
        Catch ex As Exception
            response.write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub


    Protected Sub cboProject_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboProject.SelectedIndexChanged
        Try

            Process.LoadRadComboTextAndValueP1(cboMember, "Time_Projects_Members_Get_All", cboProject.SelectedValue, "Employee2", "EmpID", False)
        Catch ex As Exception
            response.write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    Private Sub cboCompany_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboCompany.SelectedIndexChanged
        Try

            Process.LoadRadComboTextAndValueP2(cboProject, "Time_Projects_Get_Clients", cboClient.SelectedValue, cboCompany.SelectedValue, "Name", "id", False)
            Process.LoadRadComboTextAndValueP1(cboMember, "Time_Projects_Members_Get_All", cboProject.SelectedValue, "Name", "EmpID", False)
        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub
End Class