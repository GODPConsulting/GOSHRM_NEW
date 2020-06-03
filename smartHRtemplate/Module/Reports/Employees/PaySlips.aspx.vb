Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Configuration
Imports Microsoft.Reporting.WebForms
Imports System.IO
Imports Telerik.Web.UI
Public Class PaySlips
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "EMPPAYSLIP"
    Dim emailFile As String = ConfigurationManager.AppSettings("EmailURL")
    Private Sub LoadPaySlip(empid As String, startdate As Date, enddate As Date)
        Dim dtEarning As New DataTable
        dtEarning = Process.SearchDataP4("Finance_Employee_Payslip_Detail_Get", empid, "earning", startdate, enddate)

        Dim dtDeduction As New DataTable
        dtDeduction = Process.SearchDataP4("Finance_Employee_Payslip_Detail_Get", empid, "deduction", startdate, enddate)

        Dim dtlogo As New DataTable
        dtlogo = Process.GetData("general_info_get")
        GenerateSlip(dtEarning, dtDeduction, dtlogo, empid, MonthName(enddate.Month, True).ToUpper & " " & enddate.Year.ToString)
    End Sub
    Private Sub GenerateSlip(dtearn As DataTable, dtdeduct As DataTable, logos As DataTable, empid As String, reportdate As String)
        ReportViewer1.ProcessingMode = ProcessingMode.Local
        ReportViewer1.SizeToReportContent = True
        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Module/Finance/Report/Payslip.rdlc")
        Dim _rsource As New ReportDataSource("Payslip_Earning", dtearn)
        Dim _rsource1 As New ReportDataSource("Payslip_Deduction", dtdeduct)
        Dim _rsource2 As New ReportDataSource("logo", logos)
        Dim reportParameter As ReportParameter() = New ReportParameter(1) {}
        reportParameter(0) = New ReportParameter("company", Process.GetCompanyByEmpID(empid))
        reportParameter(1) = New ReportParameter("reportdate", reportdate)
        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.LocalReport.SetParameters(reportParameter)
        ReportViewer1.LocalReport.DataSources.Add(_rsource)
        ReportViewer1.LocalReport.DataSources.Add(_rsource1)
        ReportViewer1.LocalReport.DataSources.Add(_rsource2)
        ReportViewer1.LocalReport.Refresh()
    End Sub



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                cboYear.Items.Clear()
                cboMonth.Items.Clear()
                For z As Integer = 2015 To 2050
                    Dim itemTemp As New RadComboBoxItem()
                    itemTemp.Text = z.ToString
                    itemTemp.Value = z.ToString
                    cboYear.Items.Add(itemTemp)
                    itemTemp.DataBind()
                    If (z = Now.Year) Then
                        Exit For
                    End If
                Next
                Dim strCalendar As New DataSet
                strCalendar = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "select * from dbo.Calendar('finance',2016)")
                If strCalendar.Tables(0).Rows.Count > 0 Then
                    ' strPersonal.Tables(0).Rows(0).Item("empid").ToString
                    For j As Integer = 0 To strCalendar.Tables(0).Rows.Count - 1
                        Dim itemTemp As New RadComboBoxItem()
                        itemTemp.Text = strCalendar.Tables(0).Rows(j).Item("calmonths").ToString
                        itemTemp.Value = strCalendar.Tables(0).Rows(j).Item("id").ToString
                        cboMonth.Items.Add(itemTemp)
                        itemTemp.DataBind()
                    Next
                End If

                Process.AssignRadComboValue(cboMonth, Date.Now.Month)
                Process.AssignRadComboValue(cboYear, Date.Now.Year)
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


    Protected Sub btnSend_Click(sender As Object, e As EventArgs) Handles btnSend.Click

        Try
            Dim startdate As Date = Process.FirstDay(cboYear.SelectedValue, cboMonth.SelectedValue)
            Dim enddate As Date = Process.LastDay(cboYear.SelectedValue, cboMonth.SelectedValue)
            LoadPaySlip(Session("UserEmpID"), Process.DDMONYYYY(startdate), Process.DDMONYYYY(enddate))
            Dim nn As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select name from Employees_All where empid = '" & Session("UserEmpID") & "'")
            pagetitle.InnerText = nn & "'s PaySlip"
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

End Class