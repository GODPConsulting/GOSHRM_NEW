Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Configuration
Imports Microsoft.Reporting.WebForms
Imports System.IO
Public Class PayScenario
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "SIMULATEPAYROLL"
    Dim emailFile As String = ConfigurationManager.AppSettings("EmailURL")
    Private Sub LoadPayments()
        Try
            GridRepay.DataSource = Process.SearchData("Finance_Monthly_Earning_Items_Type", "Fixed Amount")
            GridRepay.AllowSorting = False
            GridRepay.AllowPaging = False
            GridRepay.DataBind()
        Catch ex As Exception
            response.write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub
    Private Sub LodaDataTable(P As String())
        Dim Datas As New DataTable
        Dim Datas1 As New DataTable
        Dim strDataSet As New DataSet
        Dim strDataSet1 As New DataSet
        strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Finance_Payroll_Simulation", P)
        strDataSet1 = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Finance_Payroll_Simulation_rates", P)
        Datas = strDataSet.Tables(0)
        Datas1 = strDataSet1.Tables(0)
        Generatereport(Datas, Datas1)
    End Sub
    Private Sub Generatereport(dt As DataTable, dt1 As DataTable)

        ReportViewer1.ProcessingMode = ProcessingMode.Local
        ReportViewer1.SizeToReportContent = True
        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Module/Reports/Finance/PayScenario.rdlc")
        Dim _rsource As New ReportDataSource("simulation", dt)
        Dim _rsource1 As New ReportDataSource("rates", dt1)
        Dim reportParameter As ReportParameter() = New ReportParameter(2) {}
        reportParameter(0) = New ReportParameter("grade", cboGrade.SelectedValue)
        reportParameter(1) = New ReportParameter("company", cboCompany.SelectedValue)
        reportParameter(2) = New ReportParameter("dept", cboDept.SelectedValue)

        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.LocalReport.SetParameters(reportParameter)
        ReportViewer1.LocalReport.DataSources.Add(_rsource)
        ReportViewer1.LocalReport.DataSources.Add(_rsource1)
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
                Process.LoadRadComboTextAndValueInitiateP1(cboDept, "Company_Parent_Breakdown", cboCompany.SelectedValue, "--All--", "Companys", "Companys")
                Process.LoadRadComboTextAndValueInitiate(cboGrade, "Job_Grade_get_all", "--All--", "name", "name")

                LoadPayments()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub


    Protected Sub btnSend_Click(sender As Object, e As EventArgs) Handles btnSend.Click

        Try

            pagetitle.InnerText = "PAYROLL SIMULATION REPORT"
            Dim rates(32) As String

            For i As Integer = 0 To GridRepay.Rows.Count - 1
                ' Access the CheckBox
                Dim controls As TextBox = DirectCast(GridRepay.Rows(i).Cells(2).FindControl("txtAmount"), TextBox)
                Dim cellSalaryItem As String = GridRepay.Rows(i).Cells(1).Text
                Dim cellAmount As Double = 0
                If IsNumeric(controls.Text.Trim) = True Then
                    cellAmount = controls.Text
                End If

                rates(i * 2) = GridRepay.Rows(i).Cells(1).Text
                rates((i * 2) + 1) = cellAmount
            Next

            For i As Integer = GridRepay.Rows.Count To 14
                rates(i * 2) = Nothing
                rates((i * 2) + 1) = 0
            Next
            rates(30) = cboCompany.SelectedValue
            rates(31) = cboDept.SelectedValue
            rates(32) = cboGrade.SelectedValue
            LodaDataTable(rates)
        Catch ex As Exception
            response.write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub


    Protected Sub cboCriteria_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboCompany.SelectedIndexChanged
        Try
            Process.LoadRadComboTextAndValueInitiateP1(cboDept, "Company_Parent_Breakdown", cboCompany.SelectedValue, "--All--", "Companys", "Companys")
        Catch ex As Exception
        End Try
    End Sub
End Class