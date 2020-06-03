Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Configuration
Imports Microsoft.Reporting.WebForms
Imports System.IO
Imports Telerik.Web.UI
Public Class PersonalTaxReport
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "EMPTAXREPORT"
    Dim emailFile As String = ConfigurationManager.AppSettings("EmailURL")
    Private Sub LoadPaySlip(empid As String, startdate As Date, enddate As Date)
        Dim dtEarning As New DataTable
        dtEarning = Process.SearchDataP3("Finance_Tax_Schedule_Employee", empid, startdate, enddate)
        GenerateSlip(dtEarning, empid, MonthName(startdate.Month, True).ToUpper & " " & startdate.Year.ToString & " - " & MonthName(enddate.Month, True).ToUpper & " " & enddate.Year.ToString)
    End Sub
    Private Sub GenerateSlip(dtearn As DataTable, empid As String, reportdate As String)
        Dim nn As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select name from Employees_All where empid = '" & empid & "'")
        ReportViewer1.ProcessingMode = ProcessingMode.Local
        ReportViewer1.SizeToReportContent = True
        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Module/Reports/Employees/PersonalTaxReport.rdlc")
        Dim _rsource As New ReportDataSource("tax", dtearn)
        Dim reportParameter As ReportParameter() = New ReportParameter(1) {}
        reportParameter(0) = New ReportParameter("employee", nn)
        reportParameter(1) = New ReportParameter("period", reportdate)
        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.LocalReport.SetParameters(reportParameter)
        ReportViewer1.LocalReport.DataSources.Add(_rsource)
        ReportViewer1.LocalReport.Refresh()
    End Sub



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                cboEndYear.Items.Clear()
                cboStartYear.Items.Clear()

                For z As Integer = 2015 To 2050
                    Dim itemTemp As New RadComboBoxItem()
                    itemTemp.Text = z.ToString
                    itemTemp.Value = z.ToString
                    cboStartYear.Items.Add(itemTemp)
                    itemTemp.DataBind()
                    If (z = Now.Year) Then
                        Exit For
                    End If
                Next

                For z As Integer = 2015 To 2050
                    Dim itemTemp As New RadComboBoxItem()
                    itemTemp.Text = z.ToString
                    itemTemp.Value = z.ToString
                    cboEndYear.Items.Add(itemTemp)
                    itemTemp.DataBind()
                    If (z = Now.Year) Then
                        Exit For
                    End If
                Next

                Process.AssignRadComboValue(cboStartYear, Date.Now.Year)
                Process.AssignRadComboValue(cboEndYear, Date.Now.Year)
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


    Protected Sub btnSend_Click(sender As Object, e As EventArgs) Handles btnSend.Click

        Try
            Dim startdate As Date = Process.FirstDay(cboStartYear.SelectedValue, 1)
            Dim enddate As Date = Process.LastDay(cboEndYear.SelectedValue, 12)
            LoadPaySlip(Session("UserEmpID"), Process.DDMONYYYY(startdate), Process.DDMONYYYY(enddate))
            Dim nn As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select name from Employees_All where empid = '" & Session("UserEmpID") & "'")
            pagetitle.InnerText = nn & "'s Tax Report"
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

End Class