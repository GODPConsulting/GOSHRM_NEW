Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO


Public Class AdjustmentUpdate
    Inherits System.Web.UI.Page
    Dim payrolladj As New clsPayrollAdjustment
    Dim AuthenCode As String = "PAYGRADE"
    Dim olddata(7) As String
    Private employeeId As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                lblcompany.Value = Session("company")
                cboApproval.Items.Clear()
                cboApproval.Items.Add("Pending")
                cboApproval.Items.Add("Approved")
                cboApproval.Items.Add("Cancelled")
                cboApproval.Items.Add("Rejected")

                cboTaxable.Items.Clear()
                cboTaxable.Items.Add("No")
                cboTaxable.Items.Add("Yes")


                'Process.LoadRadDropDownTextAndValue(cboAdj, "vw_Finance_Paylip_Item_Get_All", "item", "item", False)
                txtAmount.Value = 0
                'Holidays_get_parent
                If Request.QueryString("id") IsNot Nothing Then
                    Process.LoadRadComboTextAndValueP1(cboemployee, "Emp_PersonalDetail_Get_Employees", "", "Employee2", "EmpID")
                    cboemployee.Visible = False
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Finance_Payslip_Adjustment_Get", Request.QueryString("id"))
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    lblref.Text = strUser.Tables(0).Rows(0).Item("transref").ToString
                    Process.AssignRadComboValue(cboemployee, strUser.Tables(0).Rows(0).Item("EmpID").ToString)
                    'Session("Adjempid") = strUser.Tables(0).Rows(0).Item("EmpID").ToString
                    emp1.Value = strUser.Tables(0).Rows(0).Item("Employee").ToString
                    Process.AssignRadDropDownValue(cboAdj, strUser.Tables(0).Rows(0).Item("AdjType").ToString)
                    txtAmount.Value = strUser.Tables(0).Rows(0).Item("amount").ToString
                    datPayDate.SelectedDate = strUser.Tables(0).Rows(0).Item("Paydate").ToString
                    Process.AssignRadComboValue(cboTaxable, strUser.Tables(0).Rows(0).Item("taxable").ToString)
                    txtnote.Value = strUser.Tables(0).Rows(0).Item("note").ToString
                    Process.AssignRadComboValue(cboApproval, strUser.Tables(0).Rows(0).Item("approvalstatus").ToString)

                    lblApprovedBy.Value = strUser.Tables(0).Rows(0).Item("approver").ToString
                    txtDesc.Value = strUser.Tables(0).Rows(0).Item("Title").ToString
                    lblID.Text = strUser.Tables(0).Rows(0).Item("transref").ToString
                    If IsDBNull(strUser.Tables(0).Rows(0).Item("UpdatedOn")) = False Then
                        lblupdatedon.Value = strUser.Tables(0).Rows(0).Item("UpdatedOn")
                    End If
                    If IsDBNull(strUser.Tables(0).Rows(0).Item("approvaldate")) = False Then
                        lblApprovedOn.Value = strUser.Tables(0).Rows(0).Item("approvaldate")
                    End If

                    lblupdatedby.Value = strUser.Tables(0).Rows(0).Item("updatedby").ToString
                    cboemployee.Enabled = False

                    If cboApproval.SelectedItem.Text = "Approved" Then
                        txtAmount.ViewStateMode = True
                    Else
                        txtAmount.ViewStateMode = False
                    End If
                Else
                    Process.LoadRadComboTextAndValueInitiateP2(cboemployee, "Emp_PersonalDetail_get_all_Specific", "", Session("company"), "-- Select --", "Employee2", "EmpID")
                    txtid.Text = "0"
                    cboAdj.Enabled = True
                    cboemployee.Enabled = True
                    Dim Dates As Array = Session("varMonth").Split(":")
                    datPayDate.SelectedDate = Convert.ToDateTime(Dates(1))
                    emp1.Visible = False
                End If

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Function GetIdentity(ByVal empid As String, linetitle As String, adjtype As String, amount As Double, paydate As Date, note As String, tax As String, approval As String, user As String) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Finance_Payslip_Adjustment_Update"
            cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = 0
            cmd.Parameters.Add("@empid", SqlDbType.VarChar).Value = empid
            cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = linetitle
            cmd.Parameters.Add("@adjtype", SqlDbType.VarChar).Value = adjtype
            cmd.Parameters.Add("@amount", SqlDbType.Decimal).Value = amount
            cmd.Parameters.Add("@paydate", SqlDbType.Date).Value = paydate
            cmd.Parameters.Add("@note", SqlDbType.VarChar).Value = note
            cmd.Parameters.Add("@tax", SqlDbType.VarChar).Value = tax
            cmd.Parameters.Add("@approval", SqlDbType.VarChar).Value = approval
            cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = user
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
            Return 0
        End Try
    End Function

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            Dim lblstatus As String
            If (cboemployee.SelectedValue Is Nothing) Then
                lblstatus = "Affected Employee required!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                cboemployee.Focus()
                Exit Sub
            End If

            If (cboAdj.SelectedText Is Nothing Or cboAdj.SelectedText.Contains("Select") = True) Then
                lblstatus = "Adjustment Type required!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                cboAdj.Focus()
                Exit Sub
            End If

            If (IsNumeric(txtAmount.Value) = False) Then
                lblstatus = "Amount required in numbers!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                txtAmount.Focus()
                Exit Sub
            End If

            If (datPayDate.SelectedDate Is Nothing) Then
                lblstatus = "Payment Month required!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                datPayDate.Focus()
                Exit Sub
            End If

            Dim tempapproval As String = ""

            Dim minadjforApproval As Double = 0
            Dim minadjCount As Integer = 0

            'Old Data
            If txtid.Text <> "0" Then
                If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False Then
                    lblstatus = "You don't have privilege to perform this action"
                    Process.loadalert(divalert, msgalert, lblstatus, "danger")
                    Exit Sub
                End If
                Dim strUser As New DataSet
                strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Finance_Payslip_Adjustment_Get", txtid.Text)
                olddata(0) = strUser.Tables(0).Rows(0).Item("EmpID").ToString
                olddata(1) = strUser.Tables(0).Rows(0).Item("Title").ToString
                olddata(2) = strUser.Tables(0).Rows(0).Item("AdjType").ToString
                olddata(3) = strUser.Tables(0).Rows(0).Item("amount").ToString
                olddata(4) = strUser.Tables(0).Rows(0).Item("Paydate").ToString
                olddata(5) = strUser.Tables(0).Rows(0).Item("taxable").ToString
                olddata(6) = strUser.Tables(0).Rows(0).Item("note").ToString
                tempapproval = strUser.Tables(0).Rows(0).Item("approvalstatus").ToString
            End If

            If tempapproval = "Approved" Then
                lblstatus = "Approved Adjustments cannot be updated, update revoked"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                Exit Sub
            End If

            'Adjustment approval notification
            Dim strMinAmount As New DataSet
            strMinAmount = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Payroll_Options_Get_Company", lblcompany.Value)
            minadjCount = strMinAmount.Tables(0).Rows.Count
            If minadjCount > 0 Then
                minadjforApproval = strMinAmount.Tables(0).Rows(0).Item("minAmount").ToString
                If payrolladj.Amount < minadjforApproval Then
                    Process.AssignRadComboValue(cboApproval, "Approved")
                End If
            Else
                strMinAmount = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Payroll_Options_Get_Company", Process.GetCompanyName(""))
                minadjCount = strMinAmount.Tables(0).Rows.Count
                If minadjCount > 0 Then
                    minadjforApproval = strMinAmount.Tables(0).Rows(0).Item("minAmount").ToString
                    If payrolladj.Amount < minadjforApproval Then
                        Process.AssignRadComboValue(cboApproval, "Approved")
                    End If
                End If
            End If

            If txtid.Text.Trim = "" Then
                txtid.Text = "0"
            End If

            payrolladj.Adjustment = cboAdj.SelectedText
            payrolladj.Amount = txtAmount.Value
            payrolladj.Employee = cboemployee.SelectedValue
            payrolladj.Note = txtnote.Value
            payrolladj.PayDate = datPayDate.SelectedDate
            payrolladj.PayrollDesc = txtDesc.Value
            payrolladj.Taxable = cboTaxable.SelectedItem.Text
            payrolladj.ApprovalStatus = cboApproval.SelectedItem.Text

            If cboAdj.SelectedText.Trim.ToUpper = "DEDUCTION" Then
                payrolladj.Amount = Math.Abs(payrolladj.Amount) * -1
            Else
                payrolladj.Amount = Math.Abs(payrolladj.Amount)
            End If

            Dim OldValue As String = ""
            Dim NewValue As String = ""

            Dim j As Integer = 0

            'If txtid.Text <> "0" Then 'Updates
            '    For Each a In GetType(clsPayrollAdjustment).GetProperties()
            '        If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
            '            If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
            '                If IsNumeric(a.GetValue(payrolladj, Nothing)) = True And IsNumeric(olddata(j)) = True Then
            '                    If CDbl(a.GetValue(payrolladj, Nothing)) <> CDbl(olddata(j)) Then
            '                        NewValue += a.Name + ": " + a.GetValue(payrolladj, Nothing).ToString & vbCrLf
            '                        OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
            '                    End If
            '                Else
            '                    If a.GetValue(payrolladj, Nothing).ToString <> olddata(j).ToString Then
            '                        NewValue += a.Name + ": " + a.GetValue(payrolladj, Nothing).ToString & vbCrLf
            '                        OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
            '                    End If
            '                End If
            '            End If
            '        End If
            '        j = j + 1
            '    Next
            'Else
            '    For Each a In GetType(clsPayrollAdjustment).GetProperties() 'New Entries
            '        If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
            '            If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
            '                If a.GetValue(payrolladj, Nothing) = Nothing Then
            '                    NewValue += a.Name + ":" + " " & vbCrLf
            '                Else
            '                    NewValue += a.Name + ": " + a.GetValue(payrolladj, Nothing).ToString & vbCrLf
            '                End If
            '            End If
            '        End If
            '    Next
            'End If

            If txtid.Text = "0" Then
                txtid.Text = GetIdentity(cboemployee.SelectedValue, payrolladj.PayrollDesc, payrolladj.Adjustment, payrolladj.Amount, payrolladj.PayDate, payrolladj.Note, payrolladj.Taxable, payrolladj.ApprovalStatus, Session("LoginID"))
                If txtid.Text = "0" Then
                    Exit Sub
                End If
            Else
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Finance_Payslip_Adjustment_Update", txtid.Text, cboemployee.SelectedValue, payrolladj.PayrollDesc, payrolladj.Adjustment, payrolladj.Amount, payrolladj.PayDate, payrolladj.Note, payrolladj.Taxable, payrolladj.ApprovalStatus, Session("LoginID"))
            End If

            'Adjustment approval notification
            If minadjCount > 0 Then
                If payrolladj.Amount >= minadjforApproval Then
                    Dim strDataSet As New DataSet
                    strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Payroll_Option_Approver_Get_Company", lblcompany.Value)
                    Dim maillist As String = ""
                    Dim empIDlist As String = ""
                    If strDataSet.Tables(0).Rows.Count > 0 Then
                        For i As Integer = 0 To strDataSet.Tables(0).Rows.Count - 1
                            If i = 0 Then
                                maillist = Process.GetEmailAddress(strDataSet.Tables(0).Rows(i).Item("empid").ToString())
                                empIDlist = strDataSet.Tables(0).Rows(i).Item("empid").ToString()
                            Else
                                maillist = maillist & ";" & Process.GetEmailAddress(strDataSet.Tables(0).Rows(i).Item("empid").ToString())
                                empIDlist = empIDlist & ";" & strDataSet.Tables(0).Rows(i).Item("empid").ToString()
                            End If
                        Next
                        ''get user who created payroll
                        strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "select * from dbo.Employees_All where empid = '" & payrolladj.Employee & "'")
                        Dim location As String = ""
                        Dim grade As String = ""
                        Dim dept As String = ""
                        Dim jobtitle As String = ""
                        Dim empname As String = ""
                        If strDataSet.Tables(0).Rows.Count > 0 Then
                            location = strDataSet.Tables(0).Rows(0).Item("location").ToString()
                            grade = strDataSet.Tables(0).Rows(0).Item("grade").ToString()
                            dept = strDataSet.Tables(0).Rows(0).Item("office").ToString()
                            jobtitle = strDataSet.Tables(0).Rows(0).Item("jobtitle").ToString()
                            empname = strDataSet.Tables(0).Rows(0).Item("name").ToString()
                        End If
                        lblID.Text = txtid.Text.PadLeft(6 - txtid.Text.Length, "0")
                        Process.Payroll_Adjustment_Notification(maillist, lblID.Text, payrolladj.PayrollDesc, payrolladj.Amount, payrolladj.PayDate, empname, payrolladj.Adjustment, grade, jobtitle, dept, location, Session("UserEmpID"), empIDlist)
                        ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Nofication has been sent for approval of this adjustment" + "')", True)
                    End If

                End If
            End If

            If NewValue.Trim = "" And OldValue.Trim = "" Then
            Else
                If txtid.Text <> "0" Then
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Updated Record" & txtid.Text, "Payroll Adjustment")
                Else
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Inserted", "Payroll Adjustment")
                    ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Adjustment saved with Reference " & lblID.Text + "')", True)
                End If
            End If
            lblstatus = "Record saved"
            Process.loadalert(divalert, msgalert, lblstatus, "success")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect("adjustment")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub




    Protected Sub rdoApproval_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboApproval.SelectedIndexChanged
        Try
            Dim counts As Integer = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select count(*) from Payroll_Option_Approver where EmpID  in (select a.EmpID from dbo.Employees_All a )")
            Dim minAmount As Double = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select isnull(minAmount,0) minAmount  from Payroll_Options")
            'select isnull(minAmount,0) minAmount  from Payroll_Options
            If minAmount = Nothing Then
                minAmount = 0
            End If
            If txtAmount.Value > minAmount Then

                If counts > 0 Then
                    If Process.IsPayrollApprover(Session("UserEmpID")) = False Then

                        Process.loadalert(divalert, msgalert, "You are not eligible to update approval status", "danger")
                        ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You are not eligible to update approval status" + "')", True)
                        Process.DisableButton(btnStatus)
                        Exit Sub
                    End If
                End If
            End If

        Catch ex As Exception
           Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    'Protected Sub txtAmount_TextChanged(sender As Object, e As EventArgs) Handles txtAmount.TextChanged
    '    Try
    '        If txtid.Text <> "0" Then
    '            If cboApproval.SelectedItem.Text.ToLower = "approved" Then
    '                lblstatus.Text = "Approved Adjustments cannot be updated, update revoked"
    '                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & lblstatus.Text + "')", True)
    '                Process.DisableButton(btnAdd)
    '            End If
    '        End If

    '    Catch ex As Exception
    '        lblstatus.Text = ex.Message
    '    End Try
    'End Sub

    Protected Sub btnStatus_Click(sender As Object, e As EventArgs) Handles btnStatus.Click
        Try
            If txtid.Text = 0 Or txtid.Text.Trim = "" Then
                Process.loadalert(divalert, msgalert, "Save adjustment before updating approval status", "danger")
                Exit Sub
            Else
                Dim counts As Integer = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select count(*) from Payroll_Option_Approver a inner join Payroll_Options b on a.payoptionid = b.id where b.company = '" & lblcompany.Value & "'")
                Dim minAmount As Double = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select isnull(minAmount,0) minAmount  from Payroll_Options where company = '" & lblcompany.Value & "'")

                If minAmount = Nothing Then
                    minAmount = 0
                End If
                If CDbl(txtAmount.Value) <= minAmount Then
                    'lblapproval.Text = rdoApproval.SelectedItem.Text
                Else
                    If counts > 0 Then
                        If Process.IsPayrollApprover(Session("UserEmpID")) = False Then
                            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You are not eligible to update approval status" + "')", True)
                            Exit Sub
                        End If
                    End If
                End If
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Finance_Payslip_Adjustment_Update_Status", txtid.Text, Session("UserEmpID"), cboApproval.SelectedItem.Text)
                Process.loadalert(divalert, msgalert, "Approval Status Updated", "success")
            End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    
End Class