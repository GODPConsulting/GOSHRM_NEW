Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Configuration
Imports Microsoft.Reporting.WebForms
Imports System.IO
Imports Telerik.Web.UI
Public Class AuditTrailManagement
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "EMPLOYEEREPORT"

    Private Sub LodaDataTable(startdate As String, enddate As String, type As String, modz As String)
        Dim Datas As New DataTable
        Dim str As String
        If type = "All" Then
            str = "Select ROW_NUMBER() OVER(ORDER BY a.id) AS [Rows], a.ActionDate, a.Action, a.ActionBy, a.OriginalValues, a.NewValues, a.IPAddress, a.HostAddress, a.Page, a.ActionType FROM audittrail a where a.page ='" + modz + "' and a.ActionDate between '" + datStart.SelectedDate + "' and '" + datEnd.SelectedDate + "'"
            If type = "All" And modz = "All" Then
                str = "Select ROW_NUMBER() OVER(ORDER BY a.id) AS [Rows], a.ActionDate, a.Action, a.ActionBy, a.OriginalValues, a.NewValues, a.IPAddress, a.HostAddress, a.Page, a.ActionType FROM audittrail a where a.ActionDate between '" + datStart.SelectedDate + "' and '" + datEnd.SelectedDate + "'"
            End If
        ElseIf modz = "All" Then
            str = "Select ROW_NUMBER() OVER(ORDER BY a.id) AS [Rows], a.ActionDate, a.Action, a.ActionBy, a.OriginalValues, a.NewValues, a.IPAddress, a.HostAddress, a.Page, a.ActionType FROM audittrail a where a.ActionType = '" + type + "' and a.ActionDate between '" + datStart.SelectedDate + "' and '" + datEnd.SelectedDate + "'"
            If type = "All" And modz = "All" Then
                str = "Select ROW_NUMBER() OVER(ORDER BY a.id) AS [Rows], a.ActionDate, a.Action, a.ActionBy, a.OriginalValues, a.NewValues, a.IPAddress, a.HostAddress, a.Page, a.ActionType FROM audittrail a where a.ActionDate between '" + datStart.SelectedDate + "' and '" + datEnd.SelectedDate + "'"
            End If
        Else
            str = "Select ROW_NUMBER() OVER(ORDER BY a.id) AS [Rows], a.ActionDate, a.Action, a.ActionBy, a.OriginalValues, a.NewValues, a.IPAddress, a.HostAddress, a.Page, a.ActionType FROM audittrail a where a.ActionType = '" + type + "' and a.page ='" + modz + "' and a.ActionDate between '" + datStart.SelectedDate + "' and '" + datEnd.SelectedDate + "'"
        End If


        'Datas = Process.SearchDataP3("Audit_Trail_Report", startdate, enddate, type)
        Dim strDataSet As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, str)
        Datas = strDataSet.Tables(0)
        Generatereport(Datas)
    End Sub

    Private Sub Generatereport(dt As DataTable)
        ReportViewer1.ProcessingMode = ProcessingMode.Local
        ReportViewer1.SizeToReportContent = True
        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Module/Reports/employees/audittrailreport.rdlc")
        Dim _rsource As New ReportDataSource("AuditTrail", dt)
        Dim reportParameter As ReportParameter() = New ReportParameter(2) {}
        reportParameter(0) = New ReportParameter("actiontype", RadComboBox1.SelectedItem.Text)
        reportParameter(1) = New ReportParameter("startdate", Process.DDMONYYYY(datStart.SelectedDate))
        reportParameter(2) = New ReportParameter("enddate", Process.DDMONYYYY(datEnd.SelectedDate))
        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.LocalReport.SetParameters(reportParameter)
        ReportViewer1.LocalReport.DataSources.Add(_rsource)
        ReportViewer1.LocalReport.Refresh()
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                pagetitle.InnerText = "AUDIT TRAIL"
                LoadRadComboTextAndValue1(RadComboBox1, "select distinct ActionType from audittrail", "", "", "ActionType", "ActionType", True)
                LoadRadComboTextAndValue1(RadComboBox2, "select distinct page from audittrail", "", "", "page", "page", True)
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    Public Shared Sub LoadRadComboTextAndValue1(ByVal radBox As RadComboBox, ByVal SP As String, ByVal Param1 As String, ByVal Param2 As String, ByVal DisplayText As String, ByVal setValue As String, Optional ByVal ApplyNA As Boolean = True)

        'Load RadCombo Box with Display Text and Value
        Dim strDataSet As New DataSet
        radBox.Items.Clear()
        If ApplyNA = True Then
            Dim itemTemp As New RadComboBoxItem()
            itemTemp.Text = "All"
            itemTemp.Value = "All"
            radBox.Items.Add(itemTemp)
            itemTemp.DataBind()
        End If


        If SP = "Emp_PersonalDetail_get_all_Specific" Then
            strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, SP, Param1, Param2, 1, 10000000)
        Else
            strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, SP)
        End If

        For i As Integer = 0 To strDataSet.Tables(0).Rows.Count - 1
            'strDataSet.Tables(0).Rows(i).Item(Index).ToString()
            Dim item As New RadComboBoxItem()
            item.Text = strDataSet.Tables(0).Rows(i).Item(DisplayText.Trim).ToString()
            item.Value = strDataSet.Tables(0).Rows(i).Item(setValue.Trim).ToString()
            radBox.Items.Add(item)
            item.DataBind()
        Next
    End Sub


    Protected Sub btnSend_Click(sender As Object, e As EventArgs) Handles btnSend.Click
        Try
            Dim startdate As Date = datStart.SelectedDate
            Dim enddate As Date = datEnd.SelectedDate
            LodaDataTable(Process.DDMONYYYY(startdate), Process.DDMONYYYY(enddate), RadComboBox1.SelectedValue, RadComboBox2.SelectedValue)
        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub


End Class