Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Configuration
Imports Microsoft.Reporting.WebForms
Imports System.IO
Imports Telerik.Web.UI
Public Class DevelopmentReport
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "DEVELOPEMENTREPORT"

    Private Sub LodaDataTable()
        Dim i As Integer = 0
        Dim IDList As String = ""
        Dim criterias As String = ""
        Dim Datas As New DataTable
        Dim strDataSet As New DataSet

        'Using conn2 As New SqlConnection(WebConfig.ConnectionString)
        '    Dim comm2 As New SqlCommand(scripts, conn2)
        '    comm2.CommandType = CommandType.Text
        '    comm2.CommandTimeout = 157200
        '    Dim sdat2 As New SqlDataAdapter(comm2)
        '    sdat2.Fill(strDataSet)
        '    conn2.Close()
        'End Using

        strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Development_report", cboCompany.SelectedValue, planyear.SelectedValue)
        Datas = strDataSet.Tables(0)
        Generatereport(Datas)
    End Sub

    Private Sub Generatereport(dt As DataTable)
        ReportViewer1.ProcessingMode = ProcessingMode.Local
        ReportViewer1.SizeToReportContent = True
        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Module/Reports/Performance/DevelopmentReport.rdlc")

        Dim _rsource As New ReportDataSource("Development", dt)
        Dim reportParameter As ReportParameter() = New ReportParameter(cboColumns.Items.Count + 1) {}
        reportParameter(0) = New ReportParameter("column0", cboCompany.SelectedValue)
        reportParameter(1) = New ReportParameter("column1", planyear.SelectedItem.Text)
        'reportParameter(2) = New ReportParameter("column2", cboCompany.SelectedValue)
        For i As Integer = 0 To cboColumns.Items.Count - 1
            If cboColumns.Items(i).Checked = True Then
                reportParameter(i + 2) = New ReportParameter("column" & (i + 2).ToString, "true")
            Else
                reportParameter(i + 2) = New ReportParameter("column" & (i + 2).ToString, "false")
            End If
        Next

        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.LocalReport.SetParameters(reportParameter)
        ReportViewer1.LocalReport.DataSources.Add(_rsource)
        ReportViewer1.LocalReport.Refresh()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                pagetitle.InnerText = "DEVELOPEMENT PLAN REPORT"
                Dim ismulti As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select isnull(ismulticompany,'No')  from general_info")
                If ismulti.ToLower = "no" Then
                    Process.LoadRadComboTextAndValueP2(cboCompany, "Company_Structure_Get_ByLevel", "1", Session("Access"), "name", "name", False)
                Else
                    Process.LoadRadComboTextAndValueP2(cboCompany, "Company_Structure_Get_ByLevel", "2", Session("Access"), "name", "name", False)
                End If

                LoadRadComboTextAndValue1(planyear, "select distinct PlanYear from Performance_Development_Plan", "", "", "PlanYear", "PlanYear", False)
                Process.LoadRadComboTextAndValue(cboColumns, "Developement_All_Columns", "name", "name", False)
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub


    Protected Sub btnSend_Click(sender As Object, e As EventArgs) Handles btnSend.Click
        Try
            LodaDataTable()
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
            itemTemp.Text = "--Select Date--"
            itemTemp.Value = "--Select Date--"
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

End Class