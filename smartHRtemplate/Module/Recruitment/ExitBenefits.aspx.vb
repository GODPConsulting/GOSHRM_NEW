Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Configuration
Imports Microsoft.Reporting.WebForms
Imports System.IO
Public Class ExitBenefits
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "STAFFLOANREPORT"
    Dim emailFile As String = ConfigurationManager.AppSettings("EmailURL")

    Private Sub LodaDataTable(empid As String, payid As Integer)
        Dim dtInfo As New DataTable
        dtInfo = Process.SearchData("Finance_Payslip_Terminal_Get", empid)

        Dim dtEarning As New DataTable
        dtEarning = Process.SearchDataP2("Finance_Payslip_Terminal_Detail_Report", payid, "earning")

        Dim dtDeduction As New DataTable
        dtDeduction = Process.SearchDataP2("Finance_Payslip_Terminal_Detail_Report", payid, "deduction")

        Generatereport(dtInfo, dtEarning, dtDeduction)
    End Sub
    Private Sub Generatereport(dtinfo As DataTable, dtearn As DataTable, dtdeduct As DataTable)
        'ReportViewer1.ProcessingMode = ProcessingMode.Local
        'ReportViewer1.SizeToReportContent = True
        'ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Module/Recruitment/Reports/ExitBenefits.rdlc")
        ''ReportViewer1.LocalReport.DataSources.Clear()
        'Dim _rsource As New ReportDataSource("StaffLoan", dt)
        'ReportViewer1.LocalReport.DataSources.Add(_rsource)
        'ReportViewer1.LocalReport.Refresh()


        ReportViewer1.ProcessingMode = ProcessingMode.Local
        ReportViewer1.SizeToReportContent = True
        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Module/Recruitment/Reports/ExitBenefits.rdlc")
        Dim _rsource0 As New ReportDataSource("payslip_info", dtinfo)
        Dim _rsource As New ReportDataSource("Payslip_Earning", dtearn)
        Dim _rsource1 As New ReportDataSource("Payslip_Deduction", dtdeduct)
        ReportViewer1.LocalReport.DataSources.Add(_rsource0)
        ReportViewer1.LocalReport.DataSources.Add(_rsource)
        ReportViewer1.LocalReport.DataSources.Add(_rsource1)
        ReportViewer1.LocalReport.Refresh()
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If Request.QueryString("empid") IsNot Nothing And Request.QueryString("payslipid") IsNot Nothing Then
                    LodaDataTable(Request.QueryString("empid"), Request.QueryString("payslipid"))
                End If

            End If
        Catch ex As Exception
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub


    


End Class