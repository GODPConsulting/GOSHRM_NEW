Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Configuration
Imports Microsoft.Reporting.WebForms
Imports System.IO
Imports Telerik.Web.UI
Public Class WorkForceAnalysis
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "WFANALYSISREPORT"
   
    Private Sub LodaDataTable(startdate As String, enddate As String, dept As String)
        Dim Datas As New DataTable
        Dim chart1 As New DataTable
        Dim chart2 As New DataTable
        Datas = Process.SearchDataP3("Recruit_WorkForceAnalysis_Report", startdate, enddate, dept)
        chart1 = Process.SearchDataP4("Recruit_WorkForceAnalysis_Chart_Report", startdate, enddate, dept, "Actual")
        chart2 = Process.SearchDataP4("Recruit_WorkForceAnalysis_Chart_Report", startdate, enddate, dept, "Budget")
        Generatereport(Datas, chart1, chart2)
    End Sub
    Private Sub Generatereport(dt As DataTable, dt2 As DataTable, dt3 As DataTable)
        ReportViewer1.ProcessingMode = ProcessingMode.Local
        ReportViewer1.SizeToReportContent = True
        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Module/Reports/Recruitment/WorkForceAnalysis.rdlc")
        Dim _rsource As New ReportDataSource("WorkForceAnalysis", dt)
        Dim _rsource1 As New ReportDataSource("ChartActual", dt2)
        Dim _rsource2 As New ReportDataSource("ChartBudget", dt3)
        Dim reportParameter As ReportParameter() = New ReportParameter(2) {}
        reportParameter(0) = New ReportParameter("dept", cboDept.SelectedValue)
        reportParameter(1) = New ReportParameter("reportdate", radMonth.SelectedItem.Text & " " & radYear.SelectedValue)
        If chkChart.Checked = True Then
            reportParameter(2) = New ReportParameter("showchart", 1)
        Else
            reportParameter(2) = New ReportParameter("showchart", 0)
        End If

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
                radYear.Items.Clear()
                For i As Integer = 2017 To 2100
                    Dim itemTemp As New RadComboBoxItem()
                    itemTemp.Text = i
                    itemTemp.Value = i
                    radYear.Items.Add(itemTemp)
                    itemTemp.DataBind()
                Next

                Dim strCal As New DataSet
                strCal = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "select id, calmonths from dbo.Calendar('Finance','" & radYear.SelectedValue & "')")
                If strCal.Tables(0).Rows.Count > 0 Then
                    radMonth.Items.Clear()
                    For z As Integer = 0 To strCal.Tables(0).Rows.Count - 1
                        Dim itemTemp As New RadComboBoxItem()
                        itemTemp.Text = strCal.Tables(0).Rows(z).Item("calmonths").ToString
                        itemTemp.Value = strCal.Tables(0).Rows(z).Item("id").ToString
                        radMonth.Items.Add(itemTemp)
                        itemTemp.DataBind()
                    Next
                End If
                chkChart.Checked = True

                Dim ismulti As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select isnull(ismulticompany,'No')  from general_info")
                If ismulti.ToLower = "no" Then
                    Process.LoadRadComboTextAndValueP2(cboCompany, "Company_Structure_Get_ByLevel", "1", Session("Access"), "name", "name", False)
                Else
                    Process.LoadRadComboTextAndValueP2(cboCompany, "Company_Structure_Get_ByLevel", "2", Session("Access"), "name", "name", False)
                End If

                Process.AssignRadComboValue(cboCompany, Session("Organisation"))

                If Process.IsHRManager(Session("UserEmpID")) = True Or Process.IsFinance(Session("UserEmpID")) = True Or Process.IsAdmin(Session("LoginID")) = True Then
                    Process.LoadRadComboTextAndValueP1(cboDept, "Company_Parent_Breakdown", cboCompany.SelectedValue, "companys", "companys", False)
                Else
                    cboCompany.Enabled = False
                    Process.LoadRadComboTextAndValueP1(cboDept, "Company_Parent_Breakdown", Session("Dept"), "companys", "companys", False)
                End If

         


            End If
        Catch ex As Exception
            response.write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub


    Protected Sub btnSend_Click(sender As Object, e As EventArgs) Handles btnSend.Click

        Try

            Dim startdate As Date = Process.FirstDay(radYear.SelectedValue, radMonth.SelectedValue)
            Dim enddate As Date = Process.LastDay(radYear.SelectedValue, radMonth.SelectedValue)
            LodaDataTable(Process.DDMONYYYY(startdate), Process.DDMONYYYY(enddate), cboDept.SelectedValue)
        Catch ex As Exception
            response.write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    
    Protected Sub radYear_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles radYear.SelectedIndexChanged
        Try
            Dim strCal As New DataSet
            strCal = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "select id, calmonths from dbo.Calendar('Finance','" & radYear.SelectedValue & "')")
            If strCal.Tables(0).Rows.Count > 0 Then
                radMonth.Items.Clear()
                For z As Integer = 0 To strCal.Tables(0).Rows.Count - 1
                    Dim itemTemp As New RadComboBoxItem()
                    itemTemp.Text = strCal.Tables(0).Rows(z).Item("calmonths").ToString
                    itemTemp.Value = strCal.Tables(0).Rows(z).Item("id").ToString
                    radMonth.Items.Add(itemTemp)
                    itemTemp.DataBind()
                Next
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub cboCompany_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboCompany.SelectedIndexChanged
        Try
            If Process.IsHRManager(Session("UserEmpID")) = True Or Process.IsFinance(Session("UserEmpID")) = True Or Process.IsAdmin(Session("LoginID")) = True Then
                Process.LoadRadComboTextAndValueP1(cboDept, "Company_Parent_Breakdown", cboCompany.SelectedValue, "companys", "companys", False)
            Else
                cboCompany.Enabled = False
                Process.LoadRadComboTextAndValueP1(cboDept, "Company_Parent_Breakdown", Session("Dept"), "companys", "companys", False)
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
End Class