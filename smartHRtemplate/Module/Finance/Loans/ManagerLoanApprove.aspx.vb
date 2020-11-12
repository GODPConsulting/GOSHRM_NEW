Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO


Public Class ManagerLoanApprove
    Inherits System.Web.UI.Page
    Dim ApplyLoan As New clsApplyLoan
    Dim ApproveLoan As New clsApproveLoan
    Dim AuthenCode As String = "APPLOANS"
    Dim olddata(3) As String
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
    Private Sub DisableControl()
        Try
           
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
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not Me.IsPostBack Then
                cboApproval.Items.Clear()
                cboApproval.Items.Add("Pending")
                cboApproval.Items.Add("Approved")
                cboApproval.Items.Add("Cancelled")
                cboApproval.Items.Add("Rejected")

                lblFinanceApproval.Value = "Pending"

                'Process.LoadRadDropDownTextAndValue(radCurrency, "Currency_Load_1", "Currency", "Code")

                'Company_Structure_get_parent
                If Request.QueryString("id") IsNot Nothing Then
                    Dim Approver1Status As String = ""
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Loan_get", Request.QueryString("id"))
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    lblLoanRefNo.Text = strUser.Tables(0).Rows(0).Item("LoanRefNo").ToString

                    lblEmpID.Text = strUser.Tables(0).Rows(0).Item("empid").ToString
                    lblEmpName.Value = strUser.Tables(0).Rows(0).Item("EmployeeName").ToString
                    lblFinalStatus.Value = strUser.Tables(0).Rows(0).Item("finalstatus").ToString

                    lblamount.Value = FormatNumber(strUser.Tables(0).Rows(0).Item("LoanAmount"))
            
                    'grade 
                    'Emp_PersonalDetail_get_all
                    Dim strGrade As New DataSet
                    strGrade = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_PersonalDetail_get", lblEmpID.Text)
                    lblGradeLevel.Text = strGrade.Tables(0).Rows(0).Item("Grade").ToString
                    Session("emp_emailaddr") = strGrade.Tables(0).Rows(0).Item("Office Email").ToString

                    lblguarantorname.Value = strUser.Tables(0).Rows(0).Item("guarantorname").ToString
                    lblApprover.Value = strUser.Tables(0).Rows(0).Item("approver1name").ToString

                    lblGuarantorStatus.Value = strUser.Tables(0).Rows(0).Item("guarantorstatus").ToString
                    lblguarantorcomment.Value = strUser.Tables(0).Rows(0).Item("comment").ToString
                    If strUser.Tables(0).Rows(0).Item("guarantor").ToString = "n/a" Or strUser.Tables(0).Rows(0).Item("guarantor").ToString.Trim = "" Then
                        lblguarantor.Visible = False
                        lblGuarantorStatus.Visible = False
                        lblGuarantorStatus1.Visible = False
                        lblgcomment.Visible = False
                        lblguarantorcomment.Visible = False
                        lblguarantorname.Visible = False
                    Else
                        lblguarantor.Visible = True
                        lblguarantorname.Visible = True
                        lblGuarantorStatus.Visible = True
                        lblGuarantorStatus1.Visible = True
                        lblgcomment.Visible = True
                        lblguarantorcomment.Visible = True
                    End If


                    lblDesc.Value = strUser.Tables(0).Rows(0).Item("Description").ToString
                    lblloandate.Value = CDate(strUser.Tables(0).Rows(0).Item("LoanDate")).ToLongDateString
                    lblrepaymentstartdate.Value = CDate(strUser.Tables(0).Rows(0).Item("RepaymentStartDate")).ToLongDateString
                    lblRepayAmount.Value = FormatNumber(strUser.Tables(0).Rows(0).Item("MonthlyPay"))
                    lblloantype.Text = strUser.Tables(0).Rows(0).Item("LoanType").ToString
                    pagetitle.InnerText = lblloantype.Text + " : " + lblLoanRefNo.Text
                    'Process.LoadRadComboP1(radApprover2, "Emp_PersonalDetail_get_Superiors", lblGradeLevel.Text, 0)

                    Process.AssignRadComboValue(cboApproval, strUser.Tables(0).Rows(0).Item("Status").ToString)
                    lblFinanceApproval.Value = strUser.Tables(0).Rows(0).Item("Status2").ToString
                    lblstatustemp2.Text = strUser.Tables(0).Rows(0).Item("Status2").ToString
                    lblTenor.Value = strUser.Tables(0).Rows(0).Item("LoanTerm").ToString

                    If Session("UserEmpID") IsNot Nothing Then
                        cboApproval.Enabled = True
                        btnStatus.Visible = True
                    End If
                    DisableControl()                
                End If

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect("/Module/Finance/Loans/LoansApproval")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


    Protected Sub radStartDate_SelectedDateChanged(sender As Object, e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs)
        'If radLoanType.SelectedText Is Nothing Or radStartDate.SelectedDate Is Nothing Then
        'Else
        '    'ValidateLeave(lblEmpID.Text, radLoanType.SelectedText, radStartDate.SelectedDate, radEndDate.SelectedDate, lblLocation.Text)
        'End If

    End Sub

    Protected Sub btnStatus_Click(sender As Object, e As EventArgs)
        Try
            Dim lblstatus As String
            lblstatus = "Updating status, please wait ...."
            Process.loadalert(divalert, msgalert, lblstatus, "info")
            Dim LevelApproval As Integer = 0
            Dim OldValue As String = ""
            Dim NewValue As String = ""

            If cboApproval.SelectedItem.Text.ToUpper = "REJECTED" And txtComment.Value.Trim = "" Then
                lblstatus = "Please reason why request was rejected!"
                Process.loadalert(divalert, msgalert, lblstatus, "info")
                txtComment.Focus()
                Exit Sub
            End If

            Dim j As Integer = 0
            Session("approver2_emailaddr") = ""
            btnStatus.Disabled = True
            If Request.QueryString("id") IsNot Nothing Then
                Dim strUser As New DataSet
                strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Loan_get", Request.QueryString("id"))
                olddata(0) = strUser.Tables(0).Rows(0).Item("LoanRefNo").ToString
                olddata(1) = strUser.Tables(0).Rows(0).Item("Status").ToString
                olddata(2) = strUser.Tables(0).Rows(0).Item("Status2").ToString

                If txtid.Text.Length < 6 Then
                    ApproveLoan.LoanRefNo = txtid.Text.PadLeft(6, "0")
                Else
                    ApproveLoan.LoanRefNo = txtid.Text
                End If


                ApproveLoan.Level1_Approval_Status = cboApproval.SelectedItem.Text
                ApproveLoan.Finance_Approval_Status = lblstatustemp2.Text

                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Loan_Update_Status", txtid.Text, ApproveLoan.Level1_Approval_Status, txtComment.Value, Session("UserEmpID"))

                Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Update Loan " & ApproveLoan.LoanRefNo, "Loan Approval Level " & txtapproverlevel.Text)
                lblstatus = "Approval Status Updated"
                Process.loadalert(divalert, msgalert, lblstatus, "success")


                Process.Loan_Approver_Approval(lblLoanRefNo.Text, cboApproval.SelectedItem.Text, lblEmpID.Text, Session("UserEmpID"), Process.ApplicationURL + "/" + Process.GetMailLink(AuthenCode, 2))
                If cboApproval.SelectedItem.Text.ToLower = "approved" Then
                    Process.Loan_Approver_HR_Notification(lblLoanRefNo.Text, lblEmpID.Text, Session("UserEmpID"), cboApproval.SelectedItem.Text, Process.ApplicationURL + "/" + Process.GetMailLink(AuthenCode, 1))
                End If
            End If

            lblstatus = "Status updated & notification sent"
            Process.loadalert(divalert, msgalert, lblstatus, "success")

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        Finally
            btnStatus.Disabled = False
        End Try
    End Sub



End Class