Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Telerik.Web.UI


Public Class SalaryPayslipGenerate
    Inherits System.Web.UI.Page
    Dim loantype As New clsLoanType
    Dim AuthenCode As String = "PAYSLIP"
    Dim olddata(3) As String
    Dim Level1(2) As String
    Dim Separators() As Char = {":"c}
    Dim EmpID_1 As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                'Company_Structure_get_parent
                Dim ismulti As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select isnull(ismulticompany,'No')  from general_info")

                If ismulti.ToLower = "no" Then
                    Process.LoadRadComboTextAndValueP2(cbocompany, "Company_Structure_Get_ByLevel", "1", Session("Access"), "name", "name", False)
                Else
                    Process.LoadRadComboTextAndValueP2(cbocompany, "Company_Structure_Get_ByLevel", "2", Session("Access"), "name", "name", False)
                End If
                If Session("organisation") IsNot Nothing Then
                    Process.AssignRadComboValue(cbocompany, Session("organisation"))
                End If

                If (dateFrom.SelectedDate Is Nothing) Then
                    dateFrom.SelectedDate = Process.FirstDay(DateTime.Now.Year, DateTime.Now.Month)
                End If

                If (datEnd.SelectedDate Is Nothing) Then
                    datEnd.SelectedDate = Process.LastDay(DateTime.Now.Year, DateTime.Now.Month)
                End If
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.message, "danger")
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            Dim strChecker As New DataSet
            strChecker = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Payroll_Options_Get_Company", cbocompany.SelectedValue)

            If (strChecker.Tables(0).Rows.Count <= 0) Then
                strChecker = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Payroll_Options_Get_Company", Process.GetCompanyName(""))
                If (strChecker.Tables(0).Rows.Count <= 0) Then
                    Process.loadalert(divalert, msgalert, "No payroll option available for " & cbocompany.SelectedValue & vbNewLine & "Payroll computation cancelled!", "danger")
                    Exit Sub
                End If
            End If


            Dim lblstatus As String = ""
            System.Threading.Thread.Sleep(300)
            Dim countOpen As Integer = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Finance_Payslip_Primary_IsOpen", dateFrom.SelectedDate, datEnd.SelectedDate, cbocompany.SelectedValue)
            Dim countLocked As Integer = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Finance_Payslip_Primary_IsLocked", dateFrom.SelectedDate, datEnd.SelectedDate, cbocompany.SelectedValue)
            If countOpen = 0 Then
                If countLocked = 0 Then
                    Session("processid") = Process.ProcessPayslip(dateFrom.SelectedDate, datEnd.SelectedDate, Session("LoginID"), cbocompany.SelectedValue)
                    If CInt(Session("processid")) > 0 Then
                        lblstatus = cbocompany.SelectedValue & ": " & Process.DDMONYYYY(dateFrom.SelectedDate) & " - " & Process.DDMONYYYY(datEnd.SelectedDate) & " Payroll successfully generated"
                        Process.loadalert(divalert, msgalert, lblstatus, "success")
                    Else
                        Process.loadalert(divalert, msgalert, Session("exception"), "danger")
                    End If

                Else
                    lblstatus = cbocompany.SelectedValue & " Payroll Period already locked"
                    Process.loadalert(divalert, msgalert, lblstatus, "info")
                End If
            Else
                lblstatus = "Close any open period before generating new payroll"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
            End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect("PayrollPeriod")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
End Class