Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Telerik.Web.UI

Public Class PayrollOptionUpdate
    Inherits System.Web.UI.Page
    Dim payrolloption As New clsPayrollOption
    Dim AuthenCode As String = "PAYROLLOPT"
    Dim olddata(10) As String
    Dim oFileAttached1 As Byte
    Dim oFileAttached As Byte()
    Dim lblstatus As String = ""


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                drpAdjustment.Items.Clear()
                drpAdjustment.Items.Add("New Recruit get full pay regardless of date join in Salary Month")
                drpAdjustment.Items.Add("Divide by number of remaining days after date join of Salary Month")

                Dim ismulti As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select isnull(ismulticompany,'No')  from general_info")
               

                Process.LoadRadDropDownTextAndValue(drpCurrency, "Currency_Load_1", "Currency", "Code", False)

                If Request.QueryString("id") IsNot Nothing Then
                    If ismulti.ToLower = "no" Then
                        Process.LoadRadComboTextAndValueP2(cboCompany, "Company_Structure_Get_ByLevel", "1", Session("Access"), "name", "name", False)
                    Else
                        Process.LoadRadComboTextAndValueP2(cboCompany, "Company_Structure_Get_ByLevel", "2", Session("Access"), "name", "name", False)
                    End If

                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Payroll_Options_Get", Request.QueryString("id"))
                    If strUser.Tables(0).Rows.Count > 0 Then
                        lblid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                        Process.AssignRadComboValue(cboCompany, strUser.Tables(0).Rows(0).Item("company").ToString)
                        Process.LoadRadComboTextAndValueP1(cboApprove, "Emp_PersonalDetail_Get_Approvers", cboCompany.SelectedValue, "Employee2", "EmpID", False)
                        Process.LoadListAndComboxFromDataset(lstApprover, cboApprove, "Payroll_Option_Approver_Get", "employee2", "empid", lblid.Text)
                        Process.AssignRadDropDownValue(drpAdjustment, strUser.Tables(0).Rows(0).Item("perdaysalaryadjustment").ToString)
                        Process.AssignRadDropDownValue(drpCurrency, strUser.Tables(0).Rows(0).Item("payslipCurrency").ToString)
                        Process.AssignRadDropDownValue(radAutoEmailSlips, strUser.Tables(0).Rows(0).Item("autoemailpayslip").ToString)
                        Process.RadioListCheck(rdoAutoApprove, strUser.Tables(0).Rows(0).Item("autoapprove").ToString)
                        Process.AssignRadDropDownValue(radPayOnAttendance, strUser.Tables(0).Rows(0).Item("SalaryBasedOnAttendance").ToString)
                        Process.AssignRadDropDownValue(radPayOverTime, strUser.Tables(0).Rows(0).Item("PayOvertime").ToString)
                        lblauto.Text = strUser.Tables(0).Rows(0).Item("autoapprove").ToString
                        lblemail.Text = strUser.Tables(0).Rows(0).Item("autoemailpayslip").ToString
                        lblovertimeenabled.Text = strUser.Tables(0).Rows(0).Item("PayOvertime").ToString
                        lblattendance.Text = strUser.Tables(0).Rows(0).Item("SalaryBasedOnAttendance").ToString
                        txtOvertimeIndex.Text = strUser.Tables(0).Rows(0).Item("overtimeindex").ToString

                        txtAmount.Value = FormatNumber(strUser.Tables(0).Rows(0).Item("minamount").ToString, 2)
                        If lblauto.Text = "No" Then
                            cboApprove.Enabled = True
                        Else
                            cboApprove.Enabled = False
                        End If

                        If radPayOverTime.SelectedValue.ToUpper = "YES" Then
                            lblOvertimePaymentID.Visible = True
                            txtOvertimeIndex.Visible = True
                            lblpaydesc.Visible = True
                        Else
                            lblOvertimePaymentID.Visible = False
                            txtOvertimeIndex.Visible = False
                            lblpaydesc.Visible = False
                        End If
                        cboCompany.Enabled = False
                    End If
                Else

                    If ismulti.ToLower = "no" Then
                        Process.LoadRadComboTextAndValueP2(cboCompany, "Company_Structure_Get_ByLevel_Payroll", "1", Session("Access"), "name", "name", False)
                    Else
                        Process.LoadRadComboTextAndValueP2(cboCompany, "Company_Structure_Get_ByLevel_Payroll", "2", Session("Access"), "name", "name", False)
                    End If
                    Process.AssignRadComboValue(cboCompany, Session("Organisation"))
                    Process.LoadRadComboTextAndValueP1(cboApprove, "Emp_PersonalDetail_Get_Approvers", cboCompany.SelectedValue, "Employee2", "EmpID", False)
                    lblid.Text = "0"
                    lnkexception.Visible = False
                    txtAmount.Value = "0"
                    txtOvertimeIndex.Text = "0"
                    cboApprove.Enabled = False
                    Process.AssignRadDropDownValue(radPayOverTime, "No")
                    If radPayOverTime.SelectedValue.ToUpper = "YES" Then
                        lblOvertimePaymentID.Visible = True
                        txtOvertimeIndex.Visible = True
                        lblpaydesc.Visible = True
                    Else
                        lblOvertimePaymentID.Visible = False
                        txtOvertimeIndex.Visible = False
                        lblpaydesc.Visible = False
                    End If
                End If

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub



    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            If lblid.Text = "0" Then
                If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                    Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "danger")
                    Exit Sub
                End If
            Else
                If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False Then
                    Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "danger")
                    Exit Sub
                End If
            End If




            If txtAmount.Value.Trim Is Nothing Then
                txtAmount.Value = "0"
            End If

            If IsNumeric(txtAmount.Value) = False Then
                lblstatus = "Minimum Amount Adjustment for Approval required!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                txtAmount.Focus()
                Exit Sub
            End If

            If drpAdjustment.SelectedText Is Nothing Then
                lblstatus = "Salary Adjustment on New Recruits required!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                drpAdjustment.Focus()
                Exit Sub
            End If

            If drpCurrency.SelectedText Is Nothing Then
                lblstatus = "Currency required!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                drpCurrency.Focus()
                Exit Sub
            End If

            'Old Data
            payrolloption.Company = cboCompany.SelectedValue
            payrolloption.AutoApprovePayslip = lblauto.Text  'rdoAutoApprove.SelectedValue
            payrolloption.AutoMailPayslip = radAutoEmailSlips.SelectedValue    ' rdoEmail.SelectedValue
            payrolloption.Currency = drpCurrency.SelectedItem.Value
            payrolloption.PerDaySalaryAdjustment = drpAdjustment.SelectedText
            payrolloption.PayslipApprovers = cboApprove.CheckedItems.Count
            payrolloption.MinAmountForApproval = txtAmount.Value
            payrolloption.SalaryBasedOnAttendance = radPayOnAttendance.SelectedValue
            payrolloption.PayOvertime = radPayOverTime.SelectedValue
            payrolloption.OvertimeIndex = txtOvertimeIndex.Text

            Dim strUser As New DataSet
            strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Payroll_Options_Get", lblid.Text)
            Dim ccount As Integer = strUser.Tables(0).Rows.Count
            If strUser.Tables(0).Rows.Count > 0 Then
                olddata(0) = strUser.Tables(0).Rows(0).Item("company").ToString
                olddata(1) = strUser.Tables(0).Rows(0).Item("autoapprove").ToString
                olddata(2) = strUser.Tables(0).Rows(0).Item("autoemailpayslip").ToString
                olddata(3) = strUser.Tables(0).Rows(0).Item("perdaysalaryadjustment").ToString
                olddata(4) = strUser.Tables(0).Rows(0).Item("currency").ToString 'approvers 
                olddata(5) = strUser.Tables(0).Rows(0).Item("approvers").ToString
                olddata(6) = strUser.Tables(0).Rows(0).Item("minAmount").ToString
                olddata(7) = strUser.Tables(0).Rows(0).Item("SalaryBasedOnAttendance").ToString
                olddata(8) = strUser.Tables(0).Rows(0).Item("PayOvertime").ToString
                olddata(9) = strUser.Tables(0).Rows(0).Item("overtimeindex").ToString
            End If

            Dim NewValue As String = ""
            Dim OldValue As String = ""

            Dim j As Integer = 0

            If olddata(0) IsNot Nothing Then
                For Each a In GetType(clsPayrollOption).GetProperties()
                    If a.Name.ToLower <> "id" And a.Name.ToLower.Contains("password") = False Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If IsNumeric(a.GetValue(payrolloption, Nothing)) = True And IsNumeric(olddata(j)) = True Then
                                If CDbl(a.GetValue(payrolloption, Nothing)) <> CDbl(olddata(j)) Then
                                    NewValue += a.Name + ": " + a.GetValue(payrolloption, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            Else
                                If a.GetValue(payrolloption, Nothing).ToString <> olddata(j).ToString Then
                                    NewValue += a.Name + ": " + a.GetValue(payrolloption, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            End If
                        End If
                    End If
                    j = j + 1
                Next
            Else
                For Each a In GetType(clsPayrollOption).GetProperties() 'New Entries
                    If a.Name.ToLower <> "id" And a.Name.ToLower.Contains("password") Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If a.GetValue(payrolloption, Nothing) = Nothing Then
                                NewValue += a.Name + ":" + " " & vbCrLf
                            Else
                                NewValue += a.Name + ": " + a.GetValue(payrolloption, Nothing).ToString & vbCrLf
                            End If
                        End If
                    End If
                Next
            End If

            If lblid.Text = "0" Then
                lblid.Text = GetIdentity(lblid.Text, payrolloption.Company, payrolloption.AutoApprovePayslip, payrolloption.AutoMailPayslip, payrolloption.PerDaySalaryAdjustment, payrolloption.Currency, payrolloption.MinAmountForApproval, payrolloption.SalaryBasedOnAttendance, payrolloption.PayOvertime, payrolloption.OvertimeIndex, Session("LoginID"))
                If lblid.Text = "0" Then
                    Exit Sub
                End If
            Else
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Payroll_Options_Update", lblid.Text, payrolloption.Company, payrolloption.AutoApprovePayslip, payrolloption.AutoMailPayslip, payrolloption.PerDaySalaryAdjustment, payrolloption.Currency, payrolloption.MinAmountForApproval, payrolloption.SalaryBasedOnAttendance, payrolloption.PayOvertime, payrolloption.OvertimeIndex, Session("LoginID"))
            End If


            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Payroll_Option_Approver_Update_State", lblid.Text)
            If payrolloption.AutoApprovePayslip = "No" Then
                If cboApprove.CheckedItems.Count > 0 Then
                    Dim collection As IList(Of RadComboBoxItem) = cboApprove.CheckedItems
                    If (collection.Count <> 0) Then
                        For Each item As RadComboBoxItem In collection
                            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Payroll_Option_Approver_Update", lblid.Text, item.Value)
                        Next
                    End If
                End If
            End If
            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Payroll_Option_Approver_Delete", lblid.Text)
            If NewValue.Trim = "" And OldValue.Trim = "" Then
            Else
                If olddata(0) IsNot Nothing Then
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Updated", "Payroll Option")
                Else
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Inserted", "Payroll Option")
                End If
            End If
            cboCompany.Enabled = False
            Process.LoadListBoxFromCombo(lstApprover, cboApprove)
            lblstatus = "Record saved"
            Process.loadalert(divalert, msgalert, lblstatus, "success")
            lnkexception.Visible = True
            'Response.Redirect("~/Module/Finance/Settings/PayrollOption.aspx", False)

        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "danger")
        End Try
    End Sub
    Private Function GetIdentity(id As String, company As String, autoapprove As String, autoemailpayslip As String, perdaysalaryadjustment As String, currency As String, amount As Double, salaryonatendance As String, payovertime As String, sindex As Double, userid As String) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Payroll_Options_Update"
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id
            cmd.Parameters.Add("@company", SqlDbType.VarChar).Value = company
            cmd.Parameters.Add("@autoapprove", SqlDbType.VarChar).Value = autoapprove
            cmd.Parameters.Add("@autoemailpayslip", SqlDbType.VarChar).Value = autoemailpayslip
            cmd.Parameters.Add("@perdaysalaryadjustment", SqlDbType.VarChar).Value = perdaysalaryadjustment
            cmd.Parameters.Add("@currency", SqlDbType.VarChar).Value = currency
            cmd.Parameters.Add("@amount", SqlDbType.Decimal).Value = amount
            cmd.Parameters.Add("@salaryonatendance", SqlDbType.VarChar).Value = salaryonatendance
            cmd.Parameters.Add("@payovertime", SqlDbType.VarChar).Value = payovertime
            cmd.Parameters.Add("@index", SqlDbType.Decimal).Value = sindex
            cmd.Parameters.Add("@userid", SqlDbType.VarChar).Value = userid
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "danger")
            Return 0
        End Try
    End Function

    Protected Sub rdoAutoApprove_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rdoAutoApprove.SelectedIndexChanged
        Try
            If rdoAutoApprove.SelectedValue = "No" Then
                cboApprove.Enabled = True
            Else
                cboApprove.Enabled = False
            End If
            lblauto.Text = rdoAutoApprove.SelectedValue
        Catch ex As Exception

        End Try
    End Sub


    'Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
    '    Process.LoadListBoxFromCombo(lstApprover, cboApprove)
    'End Sub




    Protected Sub radPayOverTime_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.DropDownListEventArgs) Handles radPayOverTime.SelectedIndexChanged
        If radPayOverTime.SelectedValue.ToUpper = "YES" Then
            lblOvertimePaymentID.Visible = True
            txtOvertimeIndex.Visible = True
            lblpaydesc.Visible = True
        Else
            lblOvertimePaymentID.Visible = False
            txtOvertimeIndex.Visible = False
            lblpaydesc.Visible = False
        End If
    End Sub

    Private Sub cboCompany_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboCompany.SelectedIndexChanged
        Try
            Process.LoadRadComboTextAndValueP1(cboApprove, "Emp_PersonalDetail_Get_Approvers", cboCompany.SelectedValue, "Employee2", "EmpID", False)
            Process.LoadListBoxFromCombo(lstApprover, cboApprove)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect("PayrollOption.aspx", True)
        Catch ex As Exception

        End Try
    End Sub

   

    

    Protected Sub lnkResume0_Click(sender As Object, e As EventArgs) Handles lnkexception.Click

        Try
            Dim url As String = "payrolltimeexception.aspx?id=" & lblid.Text
            Dim s As String = "window.open('" & url + "', 'popup_window', 'width=800,height=700,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no');"
            ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)

        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "danger")
        End Try

    End Sub
End Class