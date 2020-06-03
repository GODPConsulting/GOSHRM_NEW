Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO


Public Class LoanGuarantorSign
    Inherits System.Web.UI.Page
    Dim ApplyLoan As New clsApplyLoan
    Dim ApproveLoan As New clsApproveLoan
    Dim AuthenCode As String = "EMPLOANGUA"
    Dim olddata(3) As String
    Dim emp_emailaddr As String
    Dim approver1_emailaddr As String
    Dim approver2_emailaddr As String
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
                cboApproval.Items.Add("Rejected")

                If Request.QueryString("id") IsNot Nothing Then
                    Dim Approver1Status As String = ""
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Loan_Guarantor_Get", Request.QueryString("id"))
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    lblLoanRefNo.Text = strUser.Tables(0).Rows(0).Item("LoanRefNo").ToString
                    lblEmpName.Text = strUser.Tables(0).Rows(0).Item("Name").ToString
                    lblOffice.Text = strUser.Tables(0).Rows(0).Item("office").ToString
                    lblloanamount.Text = strUser.Tables(0).Rows(0).Item("amount").ToString
                    lblLoanType.Text = strUser.Tables(0).Rows(0).Item("loantype").ToString
                    lblDesc.Text = strUser.Tables(0).Rows(0).Item("loandesc").ToString
                    lblrepay.Text = strUser.Tables(0).Rows(0).Item("monthlypay").ToString
                    Process.AssignRadComboValue(cboApproval, strUser.Tables(0).Rows(0).Item("approvalstatus").ToString)
                    txtComment.Text = strUser.Tables(0).Rows(0).Item("comment").ToString
                    lblempid.Text = strUser.Tables(0).Rows(0).Item("empid").ToString
                    lblStartDate.Text = strUser.Tables(0).Rows(0).Item("LoanDate").ToString
                    lblapprover.Text = strUser.Tables(0).Rows(0).Item("1stApprover").ToString
                    lblguarantor.Text = strUser.Tables(0).Rows(0).Item("guarantor").ToString
                End If

            End If
        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub




    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Response.Write("<script language='javascript'> { self.close() }</script>")
        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub






    Protected Sub btnStatus_Click(sender As Object, e As EventArgs) Handles btnStatus.Click
        Try
            If txtComment.Text.Trim = "" Then
                lblstatus.Text = "Guarantor's comment required!"
                txtComment.Focus()
                Exit Sub
            End If

            lblstatus.Text = "Updating status, please wait ...."
            Dim LevelApproval As Integer = 0
            Dim OldValue As String = ""
            Dim NewValue As String = ""

            Dim j As Integer = 0
            approver2_emailaddr = ""
            btnStatus.Enabled = False

            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Loan_Guarantor_Update", txtid.Text, txtComment.Text, cboApproval.SelectedItem.Text)
            lblstatus.Text = "Status Updated"

            Process.Loan_Guarantor_Approval(lblLoanRefNo.Text, cboApproval.SelectedItem.Text, lblempid.Text, lblguarantor.Text, Process.ApplicationURL + "/" + Process.GetMailLink(AuthenCode, 2))
            Process.Loan_Approver_Notification(lblLoanRefNo.Text, lblLoanType.Text, lblloanamount.Text, lblrepay.Text, lblStartDate.Text, lblDesc.Text, "Yes", Session("EmpName"), lblempid.Text, lblapprover.Text, Process.ApplicationURL + "/" + Process.GetMailLink(AuthenCode, 1))

            lblstatus.Text = "Status updated & notification sent"

        Catch ex As Exception
            lblstatus.Text = ex.Message
        Finally
            btnStatus.Enabled = True
        End Try
    End Sub



End Class