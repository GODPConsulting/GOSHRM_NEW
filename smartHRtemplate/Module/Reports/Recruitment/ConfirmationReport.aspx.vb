﻿Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Configuration
Imports Microsoft.Reporting.WebForms
Imports System.IO
Imports Telerik.Web.UI
Public Class ConfirmationReport
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "CONFIRMATIONREPORT"

    Private Sub LodaDataTable(startdate As String, enddate As String, dept As String)
        Dim Datas As New DataTable
        Dim cc As Integer = 0
        If chkInclude.Checked = True Then
            cc = 1
        End If
        Datas = Process.SearchDataP5("Recruit_Confirmation_Report", dept, startdate, enddate, cboDateCriteria.SelectedItem.Text, cc)
        Generatereport(Datas)
    End Sub
    Private Sub Generatereport(dt As DataTable)
        ReportViewer1.ProcessingMode = ProcessingMode.Local
        ReportViewer1.SizeToReportContent = True
        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Module/Reports/Recruitment/ConfirmationReport.rdlc")
        Dim _rsource As New ReportDataSource("confirmations", dt)
        Dim reportParameter As ReportParameter() = New ReportParameter(4) {}
        reportParameter(0) = New ReportParameter("dept", cboDept.SelectedValue)
        reportParameter(1) = New ReportParameter("startdate", Process.DDMONYYYY(datStart.SelectedDate))
        reportParameter(2) = New ReportParameter("enddate", Process.DDMONYYYY(datEnd.SelectedDate))
        reportParameter(3) = New ReportParameter("datecriteria", cboDateCriteria.SelectedItem.Text)
        reportParameter(4) = New ReportParameter("company", Process.GetCompanyName(cboDept.SelectedValue))
        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.LocalReport.SetParameters(reportParameter)
        ReportViewer1.LocalReport.DataSources.Add(_rsource)
        ReportViewer1.LocalReport.Refresh()
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                If Process.AuthenAction(Session("role"), AuthenCode, "Read") = False Then
                    Response.Write("You don't have privilege to perform this action")
                    ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
                    Exit Sub
                End If

                cboDateCriteria.Items.Clear()
                cboDateCriteria.Items.Add("Confirmation Date")
                cboDateCriteria.Items.Add("Date Joined")

                lblDateRange.Text = cboDateCriteria.SelectedItem.Text

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
                datStart.SelectedDate = Process.FirstDay(Date.Now.Year, Date.Now.Month)
                datEnd.SelectedDate = Process.LastDay(Date.Now.Year, Date.Now.Month)
            End If
        Catch ex As Exception
            response.write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub


    Protected Sub btnSend_Click(sender As Object, e As EventArgs) Handles btnSend.Click

        Try

            Dim startdate As Date = datStart.SelectedDate
            Dim enddate As Date = datEnd.SelectedDate
            pagetitle.InnerText = "CONFIRMATION REPORT BASED ON " & cboDateCriteria.SelectedItem.Text
            LodaDataTable(Process.DDMONYYYY(startdate), Process.DDMONYYYY(enddate), cboDept.SelectedValue)
        Catch ex As Exception
            response.write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub



    Protected Sub cboDateCriteria_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboDateCriteria.SelectedIndexChanged
        Try
            lblDateRange.Text = cboDateCriteria.SelectedItem.Text
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cboCompany_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboCompany.SelectedIndexChanged
        If Process.IsHRManager(Session("UserEmpID")) = True Or Process.IsFinance(Session("UserEmpID")) = True Or Process.IsAdmin(Session("LoginID")) = True Then
            Process.LoadRadComboTextAndValueP1(cboDept, "Company_Parent_Breakdown", cboCompany.SelectedValue, "companys", "companys", False)
        Else
            cboCompany.Enabled = False
            Process.LoadRadComboTextAndValueP1(cboDept, "Company_Parent_Breakdown", Session("Dept"), "companys", "companys", False)
        End If
    End Sub
End Class