Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO


Public Class LoanRequest
    Inherits System.Web.UI.Page
    Dim ApplyLoan As New clsApplyLoan
    Dim ApproveLoan As New clsApproveLoan
    Dim AuthenCode As String = "EMPLOAN"
    Dim olddata(10) As String
    Dim LoanLimit As Double = 0
    Dim NoDays As Integer = 0
    Dim Level1(2) As String
    Dim Level2(2) As String
    Dim Separators() As Char = {":"c}
    Dim EmpID_1 As String = ""
    Dim EmpID_2 As String = ""
    Dim EmpID_1_Name As String = ""
    Dim EmpID_2_Name As String = ""
    Dim isEligible As String = "Yes"
    Dim maxTenor As Integer = 0
    Private Sub DisableControl()
        Try

            cboApprover1.Enabled = False
            datDate.Enabled = False
            aloanamt.Enabled = False
            arepaystart.Enabled = False
            aloanrepayamt.Enabled = False
            'radCurrency.Enabled = False
            areason.Attributes.Add("readonly", "readonly")
        Catch ex As Exception

        End Try
    End Sub
    Private Sub LoadApprover1()
        Try
            lblGradeApprover1.Visible = True
            Dim strGrade As New DataSet
            EmpID_1 = cboApprover1.SelectedItem.Value

            strGrade = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_PersonalDetail_get", EmpID_1)
            If strGrade.Tables(0).Rows.Count > 0 Then
                lblGradeApprover1.Text = strGrade.Tables(0).Rows(0).Item("Grade").ToString
                Session("approver1_emailaddr") = strGrade.Tables(0).Rows(0).Item("Office Email").ToString
            End If

        Catch ex As Exception

        End Try
    End Sub

    'Emp_Work_History_get_all
    Private Function ValidateLoanAmount(ByVal EmpID As String, ByVal Grade As String, ByVal LoanType As String) As Double
        Try
            If Grade.Trim = "" Or Grade.Trim = "" Then
                LoanLimit = 0
            Else
                Dim strGrade As New DataSet
                strGrade = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "ValidateLoanAmount", EmpID, Grade, LoanType)
                LoanLimit = strGrade.Tables(0).Rows(0).Item("LoanLimit").ToString

            End If
            Return LoanLimit
        Catch ex As Exception
            Return 0
        End Try
    End Function
    Private Function ValidateTenor(ByVal tenor As Integer, ByVal LoanType As String) As Boolean
        Try
            Dim boolre As Boolean = False
            Dim strGrade As New DataSet
            strGrade = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Loan_Tenor_Rule", lblEmpID.Text, LoanType)
            If tenor > CInt(strGrade.Tables(0).Rows(0).Item("result").ToString) Then
                boolre = False
            Else
                boolre = True
            End If
            maxTenor = CInt(strGrade.Tables(0).Rows(0).Item("result").ToString)

            Return boolre
        Catch ex As Exception
            Return False
        End Try
    End Function
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'If Session("roletype") = "ESS" Then
            '    txtIntRate.Enabled = False
            'Else
            '    txtIntRate.Enabled = True
            'End If

            If Not Me.IsPostBack Then
                cborequisition.Items.Clear()
                cborequisition.Items.Add("-- Select --")
                cborequisition.Items.Add("Repayment Amount")
                cborequisition.Items.Add("Tenor in Months")

                afinapproval.Value = "Pending"

                If Request.QueryString("id") IsNot Nothing Then
                    Dim Approver1Status As String = ""

                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Loan_get", Request.QueryString("id"))
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    aloanref.Value = strUser.Tables(0).Rows(0).Item("LoanRefNo").ToString

                    lblEmpID.Text = strUser.Tables(0).Rows(0).Item("empid").ToString
                    'lblEmpName.Text = strUser.Tables(0).Rows(0).Item("EmployeeName").ToString

                    Process.AssignRadComboValue(cborequisition, strUser.Tables(0).Rows(0).Item("requisitionmode").ToString)

                    aloanamt.Text = FormatNumber(strUser.Tables(0).Rows(0).Item("LoanAmount").ToString, 2)
                    aloanintrate.Value = strUser.Tables(0).Rows(0).Item("InterestRate").ToString
                    lblMarketRate.Text = strUser.Tables(0).Rows(0).Item("MarketRate").ToString
                    'Emp_PersonalDetail_get_all
                    Dim strGrade As New DataSet
                    strGrade = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_PersonalDetail_get", lblEmpID.Text)
                    lblGradeLevel.Text = strGrade.Tables(0).Rows(0).Item("Grade").ToString
                    lblLocation.Text = strGrade.Tables(0).Rows(0).Item("Location").ToString
                    Session("emp_emailaddr") = strGrade.Tables(0).Rows(0).Item("Office Email").ToString

                    Process.LoadRadComboTextAndValue(cboApprover1, "Emp_PersonalDetail_Get_Employees", "name", "EmpID", True)
                    Process.LoadRadComboTextAndValue(cboGuarantor, "Emp_PersonalDetail_Get_Employees", "name", "EmpID", True)
                    Process.AssignRadComboValue(cboApprover1, strUser.Tables(0).Rows(0).Item("Approver1").ToString)
                    Process.AssignRadComboValue(cboGuarantor, strUser.Tables(0).Rows(0).Item("guarantorname").ToString)
                    aguarantorstat.Value = strUser.Tables(0).Rows(0).Item("guarantorstatus").ToString
                    aguarantorcomment.Value = strUser.Tables(0).Rows(0).Item("comment").ToString
                    If strUser.Tables(0).Rows(0).Item("guarantor").ToString = "n/a" Or strUser.Tables(0).Rows(0).Item("guarantor").ToString.Trim = "" Then
                        divguarantor.Visible = False
                        divguarantorcomment.Visible = False
                        divguarantorstat.Visible = False
                    Else
                        divguarantor.Visible = True
                        divguarantorcomment.Visible = True
                        divguarantorstat.Visible = True
                        If aguarantorcomment.Value.Trim = "" Then
                            divguarantorcomment.Visible = False
                        End If
                    End If




                    areason.Value = strUser.Tables(0).Rows(0).Item("Description").ToString
                    datDate.SelectedDate = strUser.Tables(0).Rows(0).Item("LoanDate").ToString
                    arepaystart.SelectedDate = strUser.Tables(0).Rows(0).Item("RepaymentStartDate").ToString
                    aloanrepayamt.Text = FormatNumber(strUser.Tables(0).Rows(0).Item("MonthlyPay").ToString, 2)
                    aloantype.Value = strUser.Tables(0).Rows(0).Item("LoanType").ToString
                    'Process.LoadRadComboP1(radApprover2, "Emp_PersonalDetail_get_Superiors", lblGradeLevel.Text, 0)

                    aapproval.Value = strUser.Tables(0).Rows(0).Item("Status").ToString
                    afinapproval.Value = strUser.Tables(0).Rows(0).Item("Status2").ToString
                    lblstatustemp.Text = strUser.Tables(0).Rows(0).Item("Status").ToString
                    lblstatustemp2.Text = strUser.Tables(0).Rows(0).Item("Status2").ToString
                    aloantenor.Text = strUser.Tables(0).Rows(0).Item("LoanTerm").ToString

                    If Session("UserEmpID") IsNot Nothing Then
                        If cboApprover1.SelectedValue.Contains(Session("UserEmpID")) Then
                            'btnStatus.Visible = True
                            cboApprover1.Enabled = False
                            cboGuarantor.Enabled = False
                        Else
                            'btnStatus.Visible = False
                        End If
                    End If

                    If aapproval.Value.ToLower = "approved" Then
                        btnupdate.Disabled = True
                        DisableControl()
                    End If

                Else
                    Process.AssignRadComboValue(cborequisition, "Repayment Amount")

                    If cborequisition.SelectedItem.Text = "Repayment Amount" Then
                        aloanrepayamt.AutoPostBack = True
                        aloanrepayamt.Enabled = True
                        aloantenor.AutoPostBack = False
                        aloantenor.Enabled = False
                        aloantenor.Text = "0"
                        'aloantenor.ToolTip = "Period of Payment will be generated after loan is approved"
                    ElseIf cborequisition.SelectedItem.Text = "Tenor in Months" Then
                        aloanrepayamt.Text = "0"
                        aloantenor.AutoPostBack = True
                        aloantenor.Enabled = True
                        aloanrepayamt.AutoPostBack = False
                        aloanrepayamt.Enabled = False
                    End If

                    If Request.QueryString("loantype") IsNot Nothing Then
                        aloantype.Value = Request.QueryString("loantype")
                    End If
                    txtid.Text = "0"
                    lblEmpID.Text = Session("UserEmpID")
                    Dim strGrade As New DataSet
                    strGrade = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_PersonalDetail_get", lblEmpID.Text)
                    'lblEmpName.Text = strGrade.Tables(0).Rows(0).Item("LastName").ToString & " " & strGrade.Tables(0).Rows(0).Item("FirstName").ToString
                    lblGradeLevel.Text = strGrade.Tables(0).Rows(0).Item("Grade").ToString
                    Session("emp_emailaddr") = strGrade.Tables(0).Rows(0).Item("Office Email").ToString
                    lblLocation.Text = strGrade.Tables(0).Rows(0).Item("location").ToString

                    Process.LoadRadComboTextAndValueP2(cboApprover1, "Emp_PersonalDetail_get_Superiors", lblGradeLevel.Text, Session("Access"), "name", "EmpID", True)
                    Process.LoadRadComboTextAndValueP2(cboGuarantor, "Emp_PersonalDetail_get_Superiors", lblGradeLevel.Text, Session("Access"), "name", "EmpID", False)
                    datDate.SelectedDate = Now.Date
                    divsupapproval.Visible = False
                    divfinapproval.Visible = False
                    Process.AssignRadComboValue(cboApprover1, strGrade.Tables(0).Rows(0).Item("SupervisorID").ToString)
                    Validate_LoanType()
                    divguarantorcomment.Visible = False
                    divguarantorstat.Visible = False
                End If

                If cborequisition.SelectedItem.Text = "Repayment Amount" Then
                    aloanrepayamt.AutoPostBack = True
                    aloanrepayamt.Enabled = True
                    aloantenor.AutoPostBack = False
                    aloantenor.Enabled = False
                Else
                    aloantenor.AutoPostBack = True
                    aloantenor.Enabled = True
                    aloanrepayamt.AutoPostBack = False
                    aloanrepayamt.Enabled = False
                End If

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Function GetIdentity(ByVal loanref As String, ByVal empid As String, ByVal loantype As String, ByVal loandate As Date, ByVal loanamount As Double, ByVal monthlypay As Double, ByVal includeinpayslip As Boolean _
                                 , ByVal loanterm As Integer, ByVal repaystartdate As Date, ByVal intrate As Double, ByVal currency As String, ByVal Desc As String _
                                 , ByVal approver1 As String, ByVal reqtype As String, marketrate As Double) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Emp_Loan_update"
            cmd.Parameters.Add("@LoanRefNo", SqlDbType.VarChar).Value = loanref
            cmd.Parameters.Add("@EmpID", SqlDbType.VarChar).Value = empid
            cmd.Parameters.Add("@LoanType", SqlDbType.VarChar).Value = loantype
            cmd.Parameters.Add("@LoanDate", SqlDbType.Date).Value = loandate
            cmd.Parameters.Add("@LoanAmount", SqlDbType.Decimal).Value = loanamount
            cmd.Parameters.Add("@MonthlyPay", SqlDbType.Decimal).Value = monthlypay
            cmd.Parameters.Add("@IncludeInPaySlip", SqlDbType.Bit).Value = includeinpayslip
            cmd.Parameters.Add("@LoanTerm", SqlDbType.Int).Value = loanterm
            cmd.Parameters.Add("@RepaymentStartDate", SqlDbType.Date).Value = repaystartdate
            cmd.Parameters.Add("@InterestRate", SqlDbType.Decimal).Value = intrate
            cmd.Parameters.Add("@Currency", SqlDbType.VarChar).Value = currency
            cmd.Parameters.Add("@Desc", SqlDbType.VarChar).Value = Desc
            cmd.Parameters.Add("@Approver1", SqlDbType.VarChar).Value = approver1
            cmd.Parameters.Add("@reqtype", SqlDbType.VarChar).Value = reqtype
            cmd.Parameters.Add("@marketrate", SqlDbType.Decimal).Value = marketrate
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
            System.Threading.Thread.Sleep(300)
            'lblstatus = "Saving record, please wait ...."
            Dim lblmsg As String = ""
            Dim msgbuild As New StringBuilder()
            Dim lblstatus As String = ""
            isEligible = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "value_IsStaffLoanEligible", lblEmpID.Text, aloantype.Value)
            If isEligible.ToUpper <> "YES" Then
                lblstatus = "Employee not eligible for this loan"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                Exit Sub
            End If

            If (arepaystart.SelectedDate Is Nothing) Then
                lblstatus = "Repayment Start Date required!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                arepaystart.Focus()
                Exit Sub
            End If

            If (datDate.SelectedDate Is Nothing) Then
                lblstatus = "Loan Date required!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                datDate.Focus()
                Exit Sub
            End If

            If CDate(Process.DDMONYYYY(datDate.SelectedDate)) > CDate(Process.DDMONYYYY(arepaystart.SelectedDate)) Then
                lblstatus = "Loan Value Date can not be beyond Repayment Start Date!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                datDate.Focus()
                Exit Sub
            End If



            If cboApprover1.SelectedValue Is Nothing Then
                lblstatus = "Approver required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                cboApprover1.Focus()
                Exit Sub
            End If

            If cboApprover1.SelectedValue = "" Then
                lblstatus = "Approver required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                cboApprover1.Focus()
                Exit Sub
            End If


            If IsNumeric(aloanamt.Text) = False Or CDbl(aloanamt.Text) <= 0 Then
                lblstatus = "Loan Amount required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                aloanamt.Focus()
                Exit Sub
            End If

            If IsNumeric(aloanrepayamt.Text) = False Or CDbl(aloanrepayamt.Text) <= 0 Then
                lblstatus = "Repayment Amount required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                aloanrepayamt.Focus()
                Exit Sub
            End If

            If CDbl(aloanrepayamt.Text) > CDbl(aloanamt.Text) Then
                lblstatus = "Repayment Amount can not be more than loan amount required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                aloanrepayamt.Focus()
                Exit Sub
            End If

            If (areason.Value Is Nothing) Then
                lblstatus = "Reason required to help make approval faster!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                areason.Focus()
                Exit Sub
            End If


            EmpID_1 = cboApprover1.SelectedItem.Value

            Dim strGrade As New DataSet
            strGrade = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_PersonalDetail_get", EmpID_1)
            If strGrade.Tables(0).Rows.Count > 0 Then
                Session("approver1_emailaddr") = strGrade.Tables(0).Rows(0).Item("Office Email").ToString
            End If


            'If ValidateLoanAmount(lblEmpID.Text, lblGradeLevel.Text, aloantype.Value) < CDbl(aloanamt.Text) And lblguarantor.Visible = False Then
            '    lblstatus = "Maximum Loan Amount exceeded, request a loan amount not more than " & LoanLimit '& vbNewLine & "Provide a Guarantor to complete loan request for " & LoanLimit
            '    lblguarantor.Visible = True
            '    cboGuarantor.Visible = True
            '    cboGuarantor.Focus()
            '    Exit Sub
            'Else
            '    If ValidateLoanAmount(lblEmpID.Text, lblGradeLevel.Text, aloantype.Value) >= CDbl(aloanamt.Text) Then
            '        lblguarantor.Visible = False
            '        cboGuarantor.Visible = False
            '    End If
            'End If

            If ValidateLoanAmount(lblEmpID.Text, lblGradeLevel.Text, aloantype.Value) < CDbl(aloanamt.Text) Then
                lblstatus = "Maximum Loan Amount exceeded, request a loan amount not more than " & LoanLimit '& vbNewLine & "Provide a Guarantor to complete loan request for " & LoanLimit
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                Exit Sub
            End If

            Dim repaycheck As Integer = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Finance_Loan_Check_Repay_Factor", lblEmpID.Text, aloantype.Value, aloanrepayamt.Text)

            If repaycheck = 0 Then
                lblstatus = "Considering previous outstanding loans, you are not eligible for this loan!" '& vbNewLine & "Provide a Guarantor to complete loan request for " & LoanLimit
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                Exit Sub
            End If

            If divguarantor.Visible = True And (cboGuarantor.SelectedValue = "" Or cboGuarantor.SelectedValue = "n/a") Then
                lblstatus = "Employee is not eligible for this type of loan" & vbNewLine & "Provide a Guarantor to complete loan request"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                cboGuarantor.Focus()
                Exit Sub
            End If

            If ValidateTenor(aloantenor.Text, aloantype.Value) = False Then
                lblstatus = "Loan request not valid, maximum tenor of " & maxTenor & "  months exceeded"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                aloantenor.Focus()
                Exit Sub
            End If

            'check staff confirmation
            Dim result As Integer = 0
            result = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Finance_Loan_Check_Staff_Confirmation", lblEmpID.Text, aloantype.Value)
            If result = 0 Then
                lblstatus = "Only Confirmed Employees are eligible for this loan!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                Exit Sub
            End If

            'check months in service
            result = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Finance_Loan_Check_Months_Service", lblEmpID.Text, aloantype.Value)
            If result = 0 Then
                lblstatus = "Due to your months of service, you are not eligible for this loan!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                Exit Sub
            End If

            'check repay factor
            result = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Finance_Loan_Check_Repay_Factor", lblEmpID.Text, aloantype.Value, aloanrepayamt.Text)
            If result = 0 Then
                lblstatus = "You will not have sufficient credit to service this loan!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                Exit Sub
            End If

            'check gratuity rule
            If divguarantor.Visible = False Then
                result = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Finance_Loan_Check_Gratuity", lblEmpID.Text, aloantype.Value, aloanamt.Text)
                If result = 0 Then
                    lblstatus = "Your gratuity will not be sufficient to cover loans. You will require a Guarantor!"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    divguarantor.Visible = True
                    cboGuarantor.Visible = True
                    cboGuarantor.Focus()
                    Exit Sub
                End If
            Else
                result = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Finance_Loan_Check_Gratuity", cboGuarantor.SelectedValue, aloantype.Value, aloanamt.Text)
                If result = 0 Then
                    lblstatus = cboGuarantor.SelectedItem.Text & " is not eligible to stand in as your Guarantor, please select another Guarantor !"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    cboGuarantor.Focus()
                    Exit Sub
                End If
                result = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Finance_Loan_Guarantor_Exclude_Get", cboGuarantor.SelectedValue)
                If result = 0 Then
                    lblstatus = cboGuarantor.SelectedItem.Text & " has automatically declined request!"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    cboGuarantor.Focus()
                    Exit Sub
                End If
            End If

            If txtid.Text <> "0" Then
                Dim strUser As New DataSet
                strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Loan_get", txtid.Text)
                olddata(0) = strUser.Tables(0).Rows(0).Item("id").ToString
                olddata(1) = strUser.Tables(0).Rows(0).Item("empid").ToString
                olddata(2) = strUser.Tables(0).Rows(0).Item("loantype").ToString
                olddata(3) = strUser.Tables(0).Rows(0).Item("loandate").ToString
                olddata(4) = strUser.Tables(0).Rows(0).Item("loanamount").ToString
                olddata(5) = strUser.Tables(0).Rows(0).Item("repaymentstartdate").ToString
                olddata(6) = strUser.Tables(0).Rows(0).Item("monthlypay").ToString
                olddata(7) = strUser.Tables(0).Rows(0).Item("interestrate").ToString
                olddata(8) = strUser.Tables(0).Rows(0).Item("description").ToString
                olddata(9) = strUser.Tables(0).Rows(0).Item("approver1").ToString

            End If




            'Process.DisableButton(btnAdd)

            ApplyLoan.EmpID = lblEmpID.Text
            ApplyLoan.AnnualRate = aloanintrate.Value
            ApplyLoan.Description = areason.Value
            ApplyLoan.Level1Approver = cboApprover1.SelectedItem.Value
            ApplyLoan.LoanAmount = aloanamt.Text.Replace(",", "")
            ApplyLoan.LoanDate = datDate.SelectedDate
            ApplyLoan.LoanRefNo = txtid.Text
            ApplyLoan.LoanType = aloantype.Value
            ApplyLoan.MonthlyPay = aloanrepayamt.Text.Replace(",", "")
            ApplyLoan.RepaymentStartDate = arepaystart.SelectedDate
            Dim OldValue As String = ""
            Dim NewValue As String = ""

            Dim j As Integer = 0

            If txtid.Text <> "0" And txtid.Text.Trim <> "" Then
                For Each a In GetType(clsApplyLoan).GetProperties()
                    If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If IsNumeric(a.GetValue(ApplyLoan, Nothing)) = True And IsNumeric(olddata(j)) = True Then
                                If CDbl(a.GetValue(ApplyLoan, Nothing)) <> CDbl(olddata(j)) Then
                                    NewValue += a.Name + ": " + a.GetValue(ApplyLoan, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            Else
                                If a.GetValue(ApplyLoan, Nothing).ToString <> olddata(j).ToString Then
                                    NewValue += a.Name + ": " + a.GetValue(ApplyLoan, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            End If
                        End If
                    End If
                    j = j + 1
                Next
            Else
                For Each a In GetType(clsApplyLoan).GetProperties() 'New Entries
                    If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If a.GetValue(ApplyLoan, Nothing) = Nothing Then
                                NewValue += a.Name + ":" + " " & vbCrLf
                            Else
                                NewValue += a.Name + ": " + a.GetValue(ApplyLoan, Nothing).ToString & vbCrLf
                            End If
                        End If
                    End If
                Next
            End If




            If txtid.Text.Trim <> "" And txtid.Text.Trim <> "0" Then
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Loan_update", ApplyLoan.LoanRefNo, ApplyLoan.EmpID, ApplyLoan.LoanType, ApplyLoan.LoanDate, ApplyLoan.LoanAmount, ApplyLoan.MonthlyPay, True, aloantenor.Text, ApplyLoan.RepaymentStartDate, ApplyLoan.AnnualRate, "", ApplyLoan.Description, ApplyLoan.Level1Approver, cborequisition.SelectedItem.Text, lblMarketRate.Text)
            Else
                txtid.Text = GetIdentity(ApplyLoan.LoanRefNo, ApplyLoan.EmpID, ApplyLoan.LoanType, ApplyLoan.LoanDate, ApplyLoan.LoanAmount, ApplyLoan.MonthlyPay, True, aloantenor.Text, ApplyLoan.RepaymentStartDate, ApplyLoan.AnnualRate, "", ApplyLoan.Description, ApplyLoan.Level1Approver, cborequisition.SelectedItem.Text, lblMarketRate.Text)

                If txtid.Text = "0" Then
                    Exit Sub
                End If
                If txtid.Text.Length < 6 Then
                    aloanref.Value = txtid.Text.PadLeft(6, "0")
                Else
                    aloanref.Value = txtid.Text
                End If

            End If

            Dim reqGuarantor = "No"
            Dim guarantor As String = ""
            If divguarantor.Visible = True Then
                reqGuarantor = "Yes"
                guarantor = cboGuarantor.SelectedValue
            Else
                guarantor = "N/A"
            End If

            If divguarantor.Visible = True Then
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Loan_Guarantor_Create", txtid.Text, cboGuarantor.SelectedValue)


                Process.Loan_Guarantor_Notification(aloanref.Value, aloantype.Value, aloanamt.Text, lblEmpID.Text, cboGuarantor.SelectedValue, Process.ApplicationURL + "/" + Process.GetMailLink(AuthenCode, 2))
            Else
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Loan_Guarantor_Delete", txtid.Text)
                If cboApprover1.SelectedItem.Text.ToUpper.Contains("N/A") = False Then
                    Process.Loan_Approver_Notification(aloanref.Value, aloantype.Value, aloanamt.Text, aloanrepayamt.Text, arepaystart.SelectedDate, areason.Value, reqGuarantor, guarantor, lblEmpID.Text, cboApprover1.SelectedValue, Process.ApplicationURL + "/" + Process.GetMailLink(AuthenCode, 1))
                End If
            End If

            Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Inserted", "Loan Request")
            Process.Loan_Notification(aloanref.Value, aloantype.Value, aloanamt.Text, aloanrepayamt.Text, arepaystart.SelectedDate, aloantenor.Text, guarantor, lblEmpID.Text)

            lblstatus = "Application successfully sent"
            Process.loadalert(divalert, msgalert, lblstatus, "success")
            'btnAdd.Enabled = False
            'btnAdd.BackColor = Color.Gray
            'Response.Write("<script language='javascript'> { self.close() }</script>")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            ' Response.Write("<script language='javascript'> { self.close() }</script>")
            Response.Redirect("loansandadvances", True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Private Sub Validate_LoanType()
        Try
            Dim lblstatus As String = ""
            isEligible = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "value_IsStaffLoanEligible", lblEmpID.Text, aloantype.Value)
            If isEligible.ToUpper.Trim <> "YES" Then
                lblstatus = "You are not eligible for this type of loan" & vbNewLine & "Provide a Guarantor to complete loan request"
                Process.loadalert(divalert, msgalert, lblstatus, "info")
                aloanintrate.Value = "0"
                divguarantor.Visible = True
                cboGuarantor.Visible = True
                cboGuarantor.Focus()
                Exit Sub
            Else
                divguarantor.Visible = False
                cboGuarantor.Visible = False
                Dim strGrade As New DataSet
                strGrade = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Loan_Rules_get_rate", lblEmpID.Text, aloantype.Value, lblGradeLevel.Text)
                If strGrade.Tables(0).Rows.Count > 0 Then
                    If IsDBNull(strGrade.Tables(0).Rows(0).Item("InterestRate")) Then
                        aloanintrate.Value = "0"
                    Else
                        aloanintrate.Value = strGrade.Tables(0).Rows(0).Item("InterestRate").ToString
                    End If
                    If IsDBNull(strGrade.Tables(0).Rows(0).Item("marketrate")) Then
                        lblMarketRate.Text = "0"
                    Else
                        lblMarketRate.Text = strGrade.Tables(0).Rows(0).Item("marketrate").ToString
                    End If

                Else
                    aloanintrate.Value = "0"
                    lblMarketRate.Text = "0"
                End If

            End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try

    End Sub




    Protected Sub aloanrepayamt_TextChanged(sender As Object, e As EventArgs) Handles aloanrepayamt.TextChanged
        Try

            aloantenor.Text = CInt(NPer(CDbl(aloanintrate.Value) / 100, CDbl(aloanrepayamt.Text), CDbl(aloanamt.Text), 0, 0) * -1) 'CInt(CDbl(aloanamt.Text) / CDbl(aloanrepayamt.Text ))
        Catch ex As Exception
        End Try
    End Sub



    Protected Sub cborequisition_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cborequisition.SelectedIndexChanged
        Try
            'Repayment Amount
            If cborequisition.SelectedItem.Text = "Repayment Amount" Then
                aloanrepayamt.AutoPostBack = True
                aloanrepayamt.Enabled = True
                aloantenor.AutoPostBack = False
                aloantenor.Enabled = False
                aloantenor.Text = "0"
                'aloantenor.ToolTip = "Period of Payment will be generated after loan is approved"
            ElseIf cborequisition.SelectedItem.Text = "Tenor in Months" Then
                aloanrepayamt.Text = "0"
                aloantenor.AutoPostBack = True
                aloantenor.Enabled = True
                aloanrepayamt.AutoPostBack = False
                aloanrepayamt.Enabled = False
                'aloanrepayamt.ToolTip = "Monthly Repayment will be generated after loan is approved"
            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub aloantenor_TextChanged(sender As Object, e As EventArgs) Handles aloantenor.TextChanged
        Try
            'aloanrepayamt.Text  = CInt(CDbl(aloanamt.Text) / CInt(aloantenor.Text))
            aloanrepayamt.Text = Math.Round(Pmt(CDbl(aloanintrate.Value) / 1200, CInt(aloantenor.Text), CDbl(aloanamt.Text) * -1), 2)
        Catch ex As Exception
        End Try
    End Sub



    Protected Sub aloanamt_TextChanged(sender As Object, e As EventArgs) Handles aloanamt.TextChanged
        Try

            If cborequisition.SelectedItem.Text = "Repayment Amount" Then
                If IsNumeric(aloanrepayamt.Text) = True Then
                    aloantenor.Text = CInt(NPer(CDbl(aloanintrate.Value) / 100, CDbl(aloanrepayamt.Text), CDbl(aloanamt.Text), 0, 0) * -1)
                End If

            Else
                If IsNumeric(aloantenor.Text) = True Then
                    aloanrepayamt.Text = Math.Round(Pmt(CDbl(aloanintrate.Value) / 1200, CInt(aloantenor.Text), CDbl(aloanamt.Text) * -1), 2)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class