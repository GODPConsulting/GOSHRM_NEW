Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Configuration
Imports Microsoft.Reporting.WebForms
Imports System.IO
Public Class StaffLoan
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "STAFFLOANREPORT"
    Dim emailFile As String = ConfigurationManager.AppSettings("EmailURL")

    Private Sub LodaDataTable(reportdate As Date, paystatus As String, loantype As String, dept As String, sview As String)
        Dim Datas As New DataTable
        Datas = Process.SearchDataP5("Finance_Loan_Report", reportdate, paystatus, loantype, dept, sview)
        Generatereport(Datas)
    End Sub
    Private Sub Generatereport(dt As DataTable)

        ReportViewer1.ProcessingMode = ProcessingMode.Local
        ReportViewer1.SizeToReportContent = True
        If rdoreport.SelectedValue = "gaap" Then
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Module/Reports/Finance/StaffLoan.rdlc")
        Else
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Module/Reports/Finance/StaffAmortisedLoan.rdlc")
        End If

        Dim _rsource As New ReportDataSource("StaffLoan", dt)
        Dim reportParameter As ReportParameter() = New ReportParameter(5) {}
        reportParameter(0) = New ReportParameter("sdate", Process.DDMONYYYY(radReportDate.SelectedDate))
        reportParameter(1) = New ReportParameter("status", rdoStatus.SelectedItem.Text)
        reportParameter(2) = New ReportParameter("loantype", cboLoanType.SelectedValue)
        reportParameter(3) = New ReportParameter("person", cboDept.SelectedValue)
        If cboCriteria.SelectedItem.Text.ToUpper.Contains("OFFICE") = True Then
            reportParameter(4) = New ReportParameter("company", Process.GetCompanyName(cboDept.SelectedValue))
            reportParameter(5) = New ReportParameter("dept", cboDept.SelectedValue)
        ElseIf cboCriteria.SelectedItem.Text.ToUpper.Contains("EMPLOYEE") = True Then
            Dim strUser As New DataSet
            strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_PersonalDetail_get", cboDept.SelectedValue)
            Dim sOffice As String = strUser.Tables(0).Rows(0).Item("office").ToString()
            reportParameter(4) = New ReportParameter("company", Process.GetCompanyName(sOffice))
            reportParameter(5) = New ReportParameter("dept", sOffice)
        End If

        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.LocalReport.SetParameters(reportParameter)
        ReportViewer1.LocalReport.DataSources.Add(_rsource)
        ReportViewer1.LocalReport.Refresh()
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                cboCriteria.Items.Clear()
                cboCriteria.Items.Add("Employee")
                cboCriteria.Items.Add("Department/Office")

                Dim ismulti As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select isnull(ismulticompany,'No')  from general_info")
                If ismulti.ToLower = "no" Then
                    Process.LoadRadComboTextAndValueP2(cboCompany, "Company_Structure_Get_ByLevel", "1", Session("Access"), "name", "name", False)
                Else
                    Process.LoadRadComboTextAndValueP2(cboCompany, "Company_Structure_Get_ByLevel", "2", Session("Access"), "name", "name", False)
                End If

                Process.AssignRadComboValue(cboCompany, Session("Organisation"))

                'lblQuery.Text = cboCriteria.SelectedItem.Text
                If cboCriteria.SelectedItem.Text.ToUpper.Contains("OFFICE") = True Then
                    If Process.IsHRManager(Session("UserEmpID")) = True Or Process.IsFinance(Session("UserEmpID")) = True Or Process.IsAdmin(Session("LoginID")) = True Then
                        Process.LoadRadComboTextAndValueP1(cboDept, "Company_Parent_Breakdown", cboCompany.SelectedValue, "companys", "companys", False)
                    Else
                        cboCompany.Enabled = False
                        Process.LoadRadComboTextAndValueP1(cboDept, "Company_Parent_Breakdown", Session("Dept"), "companys", "companys", False)
                    End If
                ElseIf cboCriteria.SelectedItem.Text.ToUpper.Contains("EMPLOYEE") = True Then
                    If Process.IsHRManager(Session("UserEmpID")) = True Or Process.IsFinance(Session("UserEmpID")) = True Or Process.IsAdmin(Session("LoginID")) = True Then
                        Process.LoadRadComboTextAndValueP1(cboDept, "Emp_PersonalDetail_Get_Employees", cboCompany.SelectedValue, "Employee3", "empid", False)
                    Else
                        cboCompany.Enabled = False
                        Process.LoadRadComboTextAndValueP1(cboDept, "Emp_PersonalDetail_Get_Employees", Session("Dept"), "Employee2", "empid", False)
                    End If
                End If
                Process.RadioListCheck(rdoreport, "Loan Report")
                Process.RadioListCheck(rdoStatus, "All")
                radReportDate.SelectedDate = Process.FirstDay(Date.Now.Year, Date.Now.Month)
                Process.LoadRadComboTextAndValueInitiate(cboLoanType, "Loan_Type_get_all", "All Loans", "Name", "Name")
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub


    Protected Sub btnSend_Click(sender As Object, e As EventArgs) Handles btnSend.Click

        Try
            pagetitle.InnerText = "STAFF LOAN REPORT"
            LodaDataTable(radReportDate.SelectedDate, rdoStatus.SelectedValue, cboLoanType.SelectedValue, cboDept.SelectedValue, cboCriteria.SelectedItem.Text)
        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub


    Protected Sub cboCriteria_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboCriteria.SelectedIndexChanged
        Try
            'lblQuery.Text = cboCriteria.SelectedItem.Text
            If cboCriteria.SelectedItem.Text.ToUpper.Contains("OFFICE") = True Then
                If Process.IsHRManager(Session("UserEmpID")) = True Or Process.IsFinance(Session("UserEmpID")) = True Or Process.IsAdmin(Session("LoginID")) = True Then
                    Process.LoadRadComboTextAndValueP1(cboDept, "Company_Parent_Breakdown", Session("Access"), "companys", "companys", False)
                Else
                    Process.LoadRadComboTextAndValueP1(cboDept, "Company_Parent_Breakdown", Session("Dept"), "companys", "companys", False)
                End If
            ElseIf cboCriteria.SelectedItem.Text.ToUpper.Contains("EMPLOYEE") = True Then
                If Process.IsHRManager(Session("UserEmpID")) = True Or Process.IsFinance(Session("UserEmpID")) = True Or Process.IsAdmin(Session("LoginID")) = True Then
                    Process.LoadRadComboTextAndValueP1(cboDept, "Emp_PersonalDetail_Get_Employees", Session("Access"), "Employee2", "empid", False)
                Else
                    Process.LoadRadComboTextAndValueP1(cboDept, "Emp_PersonalDetail_Get_Employees", Session("Dept"), "Employee2", "empid", False)
                End If
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cboCompany_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboCompany.SelectedIndexChanged
        Try
            If cboCriteria.SelectedItem.Text.ToUpper.Contains("OFFICE") = True Then
                If Process.IsHRManager(Session("UserEmpID")) = True Or Process.IsFinance(Session("UserEmpID")) = True Or Process.IsAdmin(Session("LoginID")) = True Then
                    Process.LoadRadComboTextAndValueP1(cboDept, "Company_Parent_Breakdown", cboCompany.SelectedValue, "companys", "companys", False)
                Else
                    cboCompany.Enabled = False
                    Process.LoadRadComboTextAndValueP1(cboDept, "Company_Parent_Breakdown", Session("Dept"), "companys", "companys", False)
                End If
            ElseIf cboCriteria.SelectedItem.Text.ToUpper.Contains("EMPLOYEE") = True Then
                If Process.IsHRManager(Session("UserEmpID")) = True Or Process.IsFinance(Session("UserEmpID")) = True Or Process.IsAdmin(Session("LoginID")) = True Then
                    Process.LoadRadComboTextAndValueP1(cboDept, "Emp_PersonalDetail_Get_Employees", cboCompany.SelectedValue, "Employee3", "empid", False)
                Else
                    cboCompany.Enabled = False
                    Process.LoadRadComboTextAndValueP1(cboDept, "Emp_PersonalDetail_Get_Employees", Session("Dept"), "Employee2", "empid", False)
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
        
    End Sub
End Class