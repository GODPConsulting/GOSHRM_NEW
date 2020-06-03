Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Telerik.Web.UI


Public Class PayrollPeriodData
    Inherits System.Web.UI.Page
    Dim loantype As New clsLoanType
    Dim AuthenCode As String = "PAYROLLPERIOD"
    Dim olddata(3) As String
    Dim Level1(2) As String
    Dim Separators() As Char = {":"c}
    Dim EmpID_1 As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                cboapprovalstat.Items.Clear()
                cboapprovalstat.Items.Add("Pending")
                cboapprovalstat.Items.Add("Not Approved")
                cboapprovalstat.Items.Add("Approved")

                cbopayrollstat.Items.Clear()
                cbopayrollstat.Items.Add("Open")
                cbopayrollstat.Items.Add("Lock")

                If Process.IsPayrollApprover(Session("UserEmpID")) = True Then
                    cboapprovalstat.Enabled = True
                Else
                    cboapprovalstat.Enabled = False
                End If

                Dim strDataSet As New DataSet
                strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Finance_Payslip_Primary_Get", Request.QueryString("id"))
                If strDataSet.Tables(0).Rows.Count > 0 Then
                    pagetitle.InnerText = strDataSet.Tables(0).Rows(0).Item("company").ToString()
                    lblid.Text = strDataSet.Tables(0).Rows(0).Item("id").ToString()
                    lblapprovedby.Value = strDataSet.Tables(0).Rows(0).Item("ApproverName").ToString()
                    lblcreatedby.Value = strDataSet.Tables(0).Rows(0).Item("createdby").ToString()
                    lblcreatedon.Value = strDataSet.Tables(0).Rows(0).Item("datecreated").ToString()
                    lbldateapproved.Value = strDataSet.Tables(0).Rows(0).Item("approveddate").ToString()
                    lblNetPay.Value = strDataSet.Tables(0).Rows(0).Item("netpay").ToString()
                    lblPeriod.Value = strDataSet.Tables(0).Rows(0).Item("Period").ToString()
                    Process.AssignRadComboValue(cboapprovalstat, strDataSet.Tables(0).Rows(0).Item("approvalstatus").ToString())
                    Process.AssignRadComboValue(cbopayrollstat, strDataSet.Tables(0).Rows(0).Item("status").ToString())
                    txtcomment.Value = strDataSet.Tables(0).Rows(0).Item("approvalcomment").ToString()
                    lblStart.Text = strDataSet.Tables(0).Rows(0).Item("startdate").ToString
                    lblEnd.Text = strDataSet.Tables(0).Rows(0).Item("enddate").ToString
                    lblpayotionid.Text = strDataSet.Tables(0).Rows(0).Item("payoptionid").ToString
                    If cboapprovalstat.SelectedItem.Text.ToLower <> "approved" Then
                        lblPayrollStat.Style.Add("display", "none")
                        cbopayrollstat.Visible = False
                        btnAdd.Visible = False
                        'btnCancel.Visible = False
                    End If
                    If strDataSet.Tables(0).Rows(0).Item("status").ToString().ToLower = "locked" Then
                        cboapprovalstat.Enabled = False
                    End If

                    If cboapprovalstat.SelectedItem.Text.ToLower = "approved" Then
                        cboapprovalstat.Enabled = False
                    End If
                End If
              
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim lblstatus As String
        Try
            If cboapprovalstat.SelectedItem.Text.ToLower <> "approved" And cbopayrollstat.SelectedItem.Text.ToLower = "lock" Then
                lblstatus = "Payroll has to be approved before it can be locked"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                Exit Sub
            End If

            'check open
            If cbopayrollstat.SelectedItem.Text.ToLower = "open" Then
                Dim countOpen As Integer = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Finance_Payslip_Primary_IsOpen", CDate(lblStart.Text), CDate(lblEnd.Text), pagetitle.InnerText)
                If countOpen > 0 Then
                    lblstatus = "Lock any open Payroll Period before opening another period"
                    Process.loadalert(divalert, msgalert, lblstatus, "danger")                   
                    Exit Sub
                End If
            End If

            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Finance_Payslip_Primary_Stat_Update", lblid.Text, cbopayrollstat.SelectedItem.Text, Session("LoginID"))
            lblstatus = "Payroll successfully " & cbopayrollstat.SelectedItem.Text
            Process.loadalert(divalert, msgalert, lblstatus, "success")
            cboapprovalstat.Enabled = False

            Response.Redirect("~/Module/Finance/Payroll/PayrollPeriod.aspx", True)
        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "danger")
            'Response.Redirect("~/Module/Finance/Payroll/PayrollPeriod.aspx", True)
        End Try
    End Sub


    'Protected Sub btnCancel0_Click(sender As Object, e As EventArgs) Handles btnCancel0.Click
    '    Try
    '        Response.Write("<script language='javascript'> { self.close() }</script>")
    '    Catch ex As Exception
    '        lblstatus.Text = ex.Message
    '    End Try
    'End Sub

    Protected Sub btnStat_Click(sender As Object, e As EventArgs) Handles btnStat.Click
        Dim lblstatus As String
        Try
            If Process.IsPayrollApprover(Session("UserEmpID")) = False Then
                lblstatus = "You are not eligible to approve payroll"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                Exit Sub
            End If
            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Finance_Payslip_Primary_Approval_Update", lblid.Text, cboapprovalstat.SelectedItem.Text, Session("UserEmpID"), txtcomment.Value)
            lblstatus = "Payroll Approval Status updated"
            Process.loadalert(divalert, msgalert, lblstatus, "success")

            'Get mailing list
            Dim strDataSet As New DataSet
            strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Payroll_Option_Approver_Get", lblpayotionid.Text)
            Dim maillist As String = ""
            Dim empIDlist As String = ""
            If strDataSet.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To strDataSet.Tables(0).Rows.Count - 1
                    If i = 0 Then
                        maillist = strDataSet.Tables(0).Rows(i).Item("email").ToString()
                        empIDlist = strDataSet.Tables(0).Rows(i).Item("empid").ToString()
                    Else
                        maillist = maillist & ";" & strDataSet.Tables(0).Rows(i).Item("email").ToString()
                        empIDlist = empIDlist & ";" & strDataSet.Tables(0).Rows(i).Item("empid").ToString()
                    End If
                Next
                'get user who created payroll
                strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "users_get_byuserid", lblcreatedby.Value.Trim)
                If strDataSet.Tables(0).Rows.Count > 0 Then
                    If maillist.Length > 0 Then
                        maillist = maillist & ";" & strDataSet.Tables(0).Rows(0).Item("email").ToString()
                        empIDlist = empIDlist & ";" & strDataSet.Tables(0).Rows(0).Item("empid").ToString()
                    Else
                        maillist = strDataSet.Tables(0).Rows(0).Item("email").ToString()
                        empIDlist = strDataSet.Tables(0).Rows(0).Item("empid").ToString()
                    End If
                End If
                Process.Payroll_Approval(pagetitle.InnerText, maillist, lblPeriod.Value, cboapprovalstat.SelectedItem.Text, Session("EmpName"), DateTime.Now, txtcomment.Value, lblNetPay.Value, Session("UserEmpID"), empIDlist)
            End If

            Process.loadalert(divalert, msgalert, lblstatus, "success")
        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "danger")
        End Try
    End Sub

   
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect("~/Module/Finance/Payroll/PayrollPeriod.aspx", True)
        Catch ex As Exception
        End Try
    End Sub
End Class