Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Microsoft.Reporting.WebForms
Imports System.IO
Imports Telerik.Web.UI


Public Class LoanDetail
    Inherits System.Web.UI.Page
    Dim ApplyLoan As New clsApplyLoan
    Dim ApproveLoan As New clsApproveLoan
    Dim AuthenCode As String = "LOANS"
    Dim olddata(6) As String
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
    Dim emailFile As String = ConfigurationManager.AppSettings("EmailURL")
    Private Sub DisableControl()
        Try
        Catch ex As Exception

        End Try
    End Sub
    Private Sub LodaDataTable(id As String)
        Dim dtEarning As New DataTable
        dtEarning = Process.SearchData("Emp_Loan_get", id)
        GenerateLoanLetter(dtEarning, Process.GetData("general_info_get"), Server.MapPath(emailFile & "LoanLetter" & id & ".PDF"))
    End Sub
    Private Sub GenerateLoanLetter(dtearn As DataTable, logos As DataTable, ByVal savePath As String)
        Dim ReportViewer1 As New ReportViewer
        ReportViewer1.ProcessingMode = ProcessingMode.Local
        ReportViewer1.SizeToReportContent = True
        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Module/Reports/Finance/LoanLetter.rdlc")
        Dim _rsource As New ReportDataSource("loan", dtearn)
        Dim _rsource2 As New ReportDataSource("logo", logos)
        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.LocalReport.DataSources.Add(_rsource)
        ReportViewer1.LocalReport.DataSources.Add(_rsource2)
        ReportViewer1.LocalReport.Refresh()
        Dim Bytes() As Byte = ReportViewer1.LocalReport.Render("PDF", "", Nothing, Nothing, Nothing, Nothing, Nothing)

        If File.Exists(savePath) Then
            File.Delete(savePath)
        End If

        Using Stream As New FileStream(savePath, FileMode.Create)
            Stream.Write(Bytes, 0, Bytes.Length)
        End Using
        Session("rptAttachment") = savePath
    End Sub


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
                If Session("roletype") = "ESS" Then
                    txtIntRate.Disabled = True
                Else
                    txtIntRate.Disabled = False
                End If

                cboApproval.Items.Clear()
                cboApproval.Items.Add("Pending")
                cboApproval.Items.Add("Approved")
                cboApproval.Items.Add("Cancelled")
                cboApproval.Items.Add("Rejected")

                radFairValue.Items.Clear()
                radFairValue.Items.Add("-- Select --")
                radFairValue.Items.Add("No")
                radFairValue.Items.Add("Yes")

                'Company_Structure_get_parent
                If Request.QueryString("id") IsNot Nothing Then
                    Dim Approver1Status As String = ""

                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Loan_get", Request.QueryString("id"))
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    lblLoanRefNo.Text = strUser.Tables(0).Rows(0).Item("LoanRefNo").ToString

                    lblEmpID.Text = strUser.Tables(0).Rows(0).Item("empid").ToString
                    lblEmpName.Value = strUser.Tables(0).Rows(0).Item("EmployeeName").ToString
                    Process.LoadRadComboTextAndValueP2(cboApproverII, "Emp_PersonalDetail_get_Superiors", strUser.Tables(0).Rows(0).Item("grade").ToString, Process.GetCompanyName, "Employee2", "EmpID", True)
                    If strUser.Tables(0).Rows(0).Item("HigherApproval") = True Then
                        divhigherapproval2.Visible = True
                        chkHigherApproval2.Checked = True
                        Process.LoadListAndComboxFromDataset(lstMakeup, cboApproverII, "Emp_Loan_Approval_Get", "AproverID", "AproverID", Request.QueryString("id"))
                        Dim strUser2 As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Loan_Approval_Get", Request.QueryString("id"))
                        For i As Integer = 0 To strUser2.Tables(0).Rows.Count - 1
                            If strUser2.Tables(0).Rows(0).Item("Status") = False Then
                                cboApproval.Enabled = False
                            End If
                        Next

                    Else
                        chkHigherApproval2.Checked = False
                        divhigherapproval2.Visible = False
                    End If
                    txtAmount.Value = FormatNumber(strUser.Tables(0).Rows(0).Item("LoanAmount").ToString, 2)

                    lblfAIRVALUE.Text = FormatNumber(strUser.Tables(0).Rows(0).Item("FairValue"), 2)
                    txtIntRate.Value = strUser.Tables(0).Rows(0).Item("InterestRate").ToString

                    'grade 
                    'Emp_PersonalDetail_get_all
                    Dim strGrade As New DataSet
                    strGrade = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_PersonalDetail_get", lblEmpID.Text)
                    lblGradeLevel.Text = strGrade.Tables(0).Rows(0).Item("Grade").ToString
                    lblLocation.Value = strGrade.Tables(0).Rows(0).Item("Location").ToString
                    Process.AssignRadDropDownValue(radFairValue, strUser.Tables(0).Rows(0).Item("FairValueLoan").ToString)

                    lblApprover1.Value = strUser.Tables(0).Rows(0).Item("approver1name").ToString
                    lblguarantorname.Value = strUser.Tables(0).Rows(0).Item("guarantorname").ToString
                    txtComment.Value = strUser.Tables(0).Rows(0).Item("approver2comment").ToString
                    lblapprover1comment.Value = strUser.Tables(0).Rows(0).Item("approver1comment").ToString
                    txtMarketrate.Value = strUser.Tables(0).Rows(0).Item("marketrate").ToString
                    lblGuarantorStatus.Value = strUser.Tables(0).Rows(0).Item("guarantorstatus").ToString
                    lblguarantorcomment.Value = strUser.Tables(0).Rows(0).Item("comment").ToString
                    lblfinancemember.Value = strUser.Tables(0).Rows(0).Item("approver2name").ToString

                    If strUser.Tables(0).Rows(0).Item("guarantor").ToString = "n/a" Or strUser.Tables(0).Rows(0).Item("guarantor").ToString.Trim = "" Then
                        lblguarantor.Visible = False
                        lblguarantorname.Visible = False
                        lblGuarantorStatus.Visible = False
                        lblGuarantorStatus1.Visible = False
                        lblgcomment.Visible = False
                        lblguarantorcomment.Visible = False
                    Else
                        lblguarantor.Visible = True
                        lblguarantorname.Visible = True
                        lblGuarantorStatus.Visible = True
                        lblGuarantorStatus1.Visible = True
                        lblgcomment.Visible = True
                        lblguarantorcomment.Visible = True
                    End If


                    lblloandesc.Value = strUser.Tables(0).Rows(0).Item("Description").ToString
                    lblloandate.Value = CDate(strUser.Tables(0).Rows(0).Item("LoanDate")).ToLongDateString
                    lblrepaystartdate.Value = CDate(strUser.Tables(0).Rows(0).Item("RepaymentStartDate")).ToLongDateString
                    lblrepayamount.Value = FormatNumber(strUser.Tables(0).Rows(0).Item("MonthlyPay").ToString, 2)
                    lblloantype.Text = strUser.Tables(0).Rows(0).Item("LoanType").ToString
                    pagetitle.InnerText = lblloantype.Text + " : " + lblLoanRefNo.Text

                    'Process.LoadRadComboP1(radApprover2, "Emp_PersonalDetail_get_Superiors", lblGradeLevel.Text, 0)
                    lblFinalStatus.Value = strUser.Tables(0).Rows(0).Item("finalstatus").ToString
                    lblapprover1status.Value = strUser.Tables(0).Rows(0).Item("Status").ToString
                    Process.AssignRadComboValue(cboApproval, strUser.Tables(0).Rows(0).Item("Status2").ToString)
                    lblTenor.Value = strUser.Tables(0).Rows(0).Item("LoanTerm").ToString
                    If Session("UserEmpID") IsNot Nothing Then
                        If Process.IsFinance(Session("UserEmpID")) Then
                            lblStatus2.Visible = True
                            If lblapprover1status.Value = "Pending" Then
                                cboApproval.Enabled = False
                            Else
                                cboApproval.Enabled = True
                                btnStatus.Visible = True
                            End If
                        End If
                    End If
                    DisableControl()

                    Dim strLoanSchedule As New DataSet
                    strLoanSchedule = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Loan_Schedule_Get_All", lblLoanRefNo.Text)
                    If strLoanSchedule.Tables(0).Rows.Count > 0 Then
                        radFairValue.Enabled = False
                    Else
                        radFairValue.Enabled = True
                    End If
                End If

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Function GetIdentity(ByVal loanref As String, ByVal empid As String, ByVal loantype As String, ByVal loandate As Date, ByVal loanamount As Double, ByVal monthlypay As Double, ByVal includeinpayslip As Boolean _
                                 , ByVal loanterm As Integer, ByVal repaystartdate As Date, ByVal intrate As Double, ByVal currency As String, ByVal Desc As String _
                                 , ByVal approver1 As String, ByVal approver2 As String, ByVal status1 As String, ByVal status2 As String) As String
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
            cmd.Parameters.Add("@InterestRate", SqlDbType.VarChar).Value = intrate
            cmd.Parameters.Add("@Currency", SqlDbType.VarChar).Value = currency
            cmd.Parameters.Add("@Desc", SqlDbType.VarChar).Value = Desc
            cmd.Parameters.Add("@Approver1", SqlDbType.VarChar).Value = approver1
            cmd.Parameters.Add("@Approver2", SqlDbType.VarChar).Value = approver2
            cmd.Parameters.Add("@Status1", SqlDbType.VarChar).Value = status1
            cmd.Parameters.Add("@Status2", SqlDbType.VarChar).Value = status2
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception
            Process.strExp = ex.Message
            Return 0
        End Try
    End Function



    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect("~/Module/Finance/Loans/StaffLoans")
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
            Process.loadalert(divalert, msgalert, lblstatus, "danger")
            Dim LevelApproval As Integer = 0
            Dim OldValue As String = ""
            Dim NewValue As String = ""

            If txtIntRate.Value.Trim = "" Then
                txtIntRate.Value = "0"
            End If

            If IsNumeric(txtIntRate.Value) = False Then
                lblstatus = "Interest must be numeric!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                txtIntRate.Focus()
                Exit Sub
            End If
            If chkHigherApproval2.Checked = True And cboApproval.SelectedItem.Text.ToLower = "approved" Then
                lblstatus = "Higher Level aproval needed"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
            End If
            If cboApproval.SelectedItem.Text.ToLower <> "approved" Then

                If chkHigherApproval2.Checked = True Then
                    Dim aproversdb = ""
                    Dim aproversmain = ""
                    Dim strUser2 As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Loan_Approval_Get", Request.QueryString("id"))
                    If strUser2.Tables(0).Rows.Count > 0 Then
                        For i As Integer = 0 To strUser2.Tables(0).Rows.Count - 1
                            aproversdb = aproversdb + strUser2.Tables(0).Rows(i).Item("Aprovers")


                        Next
                        For h As Integer = 0 To cboApproverII.CheckedItems.Count - 1
                            aproversmain = aproversmain + cboApproverII.CheckedItems.Item(h).Text
                        Next
                        If aproversdb <> aproversmain Then
                            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Loan_Approval_Delete", Request.QueryString("id"))
                            If cboApproverII.CheckedItems.Count > 0 Then
                                For d As Integer = 0 To cboApproverII.CheckedItems.Count - 1
                                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Loan_Approval_Update", Request.QueryString("id"), cboApproverII.CheckedItems.Item(d).Text)
                                Next
                            End If
                        End If
                    Else
                            If cboApproverII.CheckedItems.Count > 0 Then
                            For d As Integer = 0 To cboApproverII.CheckedItems.Count - 1
                                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Loan_Approval_Update", Request.QueryString("id"), cboApproverII.CheckedItems.Item(d).Text)
                            Next
                        End If
                    End If
                End If
            End If
            Dim marketrate As Double = 0
            Dim interestrate As Double = 0
            Dim monthlypay As Double = 0
            Dim loanamount As Double = 0
            Dim tenor As Integer = 0
            'Dim fairvalue As Double = 0
            'Dim EIR As Double = 0
            Dim AmortEIR As Double = 0
            Dim AmortFairValue As Double = 0
            Dim repaystartdate As Date
            Dim EMPID As String = ""
            Dim LoanType As String = ""

            Dim j As Integer = 0
            approver2_emailaddr = ""
            btnStatus.Disabled = True
            If Request.QueryString("id") IsNot Nothing Then
                Dim strUser As New DataSet
                strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Loan_get", Request.QueryString("id"))
                olddata(0) = strUser.Tables(0).Rows(0).Item("LoanRefNo").ToString
                olddata(1) = strUser.Tables(0).Rows(0).Item("Status").ToString
                olddata(2) = strUser.Tables(0).Rows(0).Item("Status2").ToString
                olddata(3) = strUser.Tables(0).Rows(0).Item("LoanAmount").ToString
                olddata(4) = strUser.Tables(0).Rows(0).Item("InterestRate").ToString
                olddata(5) = strUser.Tables(0).Rows(0).Item("marketrate").ToString

                If txtid.Text.Length < 6 Then
                    ApproveLoan.LoanRefNo = txtid.Text.PadLeft(6, "0")
                Else
                    ApproveLoan.LoanRefNo = txtid.Text
                End If

                ApproveLoan.Level1_Approval_Status = lblapprover1status.Value
                ApproveLoan.Finance_Approval_Status = cboApproval.SelectedItem.Text

                For Each a In GetType(clsApproveLoan).GetProperties()
                    If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If IsNumeric(a.GetValue(ApproveLoan, Nothing)) = True And IsNumeric(olddata(j)) = True Then
                                If CDbl(a.GetValue(ApproveLoan, Nothing)) <> CDbl(olddata(j)) Then
                                    NewValue += a.Name + ": " + a.GetValue(ApproveLoan, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            Else
                                If a.GetValue(ApproveLoan, Nothing).ToString <> olddata(j).ToString Then
                                    NewValue += a.Name + ": " + a.GetValue(ApproveLoan, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            End If
                        End If
                    End If
                    j = j + 1
                Next

                repaystartdate = lblrepaystartdate.Value
                tenor = lblTenor.Value
                interestrate = txtIntRate.Value
                monthlypay = lblrepayamount.Value
                loanamount = txtAmount.Value
                LoanType = lblloantype.Text
                marketrate = txtMarketrate.Value

                Dim maturitydate As DateTime = repaystartdate.AddMonths(tenor)
                Dim PayType As DueDate = DueDate.BegOfPeriod

                'lblamortisedcost.Text = FormatNumber(PV(CDbl(txtMarketrate.Value) / 1200, CInt(lblTenor.Value), CDbl(lblrepayamount.Value) * -1), 2)
                lblamortisedcost.Text = (PV(CDbl(txtMarketrate.Value) / 1200, CInt(lblTenor.Value), CDbl(lblrepayamount.Value) * -1))
                'Dim amortisedeir As Double = Rate(CDbl(tenor), CDbl(lblamortisedcost.Text), CDbl(monthlypay * -1), 0, 0, 0)
                Dim amortisedeir As Double = 0
                If marketrate = 0 And interestrate = 0 Then
                    amortisedeir = 0
                Else
                    amortisedeir = Rate(CInt(lblTenor.Value), CDbl(monthlypay * -1), CDbl(lblamortisedcost.Text), CDbl(0), PayType, CDbl(0.1))
                End If


                'Dim amortisedeir As Double = Rate(CInt(lblTenor.Value), CDbl(monthlypay * -1), CDbl(lblamortisedcost.Text), CDbl(0), PayType, CDbl(0.1))
                'Dim amortisedeir As Double = 0

                Dim strGrade As New DataSet
                Dim approvername As String = ""
                strGrade = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "select name, email from dbo.Employees_All where empid = '" & Session("UserEmpID") & "'")
                If strGrade.Tables(0).Rows.Count > 0 Then
                    approver2_emailaddr = strGrade.Tables(0).Rows(0).Item("email").ToString
                    approvername = strGrade.Tables(0).Rows(0).Item("name").ToString
                End If

                'requester email
                strGrade = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_PersonalDetail_get", lblEmpID.Text)
                If strGrade.Tables(0).Rows.Count > 0 Then
                    emp_emailaddr = strGrade.Tables(0).Rows(0).Item("Office Email").ToString
                End If

                'get final status
                Dim strFinalStatus As New DataSet
                Dim finalstatus As String = ""
                Dim approver1name As String = ""
                Dim approver2name As String = ""
                Dim status_approver1name As String = ""
                Dim status_approver2name As String = ""
                strFinalStatus = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Loan_get", lblLoanRefNo.Text)
                If strFinalStatus.Tables(0).Rows.Count > 0 Then
                    finalstatus = strFinalStatus.Tables(0).Rows(0).Item("FinalStatus").ToString
                    status_approver1name = strFinalStatus.Tables(0).Rows(0).Item("status").ToString
                    status_approver2name = strFinalStatus.Tables(0).Rows(0).Item("status2").ToString
                    approver1name = strFinalStatus.Tables(0).Rows(0).Item("Approver1Name").ToString
                    approver2name = strFinalStatus.Tables(0).Rows(0).Item("Approver2Name").ToString
                End If



                'generate schedule
                If (cboApproval.SelectedItem.Text.ToUpper = "APPROVED") And (status_approver2name <> cboApproval.SelectedItem.Text) Then
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "auto_loan_schedule_run", txtid.Text, lblfAIRVALUE.Text, lblrepayamount.Value, lblrepaystartdate.Value, CDbl(txtIntRate.Value) / 1200)
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "auto_loan_schedule_amortised_run", txtid.Text, lblamortisedcost.Text, lblrepayamount.Value, lblrepaystartdate.Value, AmortEIR)
                End If

                ''
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Loan_Update_Status_Level_2", txtid.Text, txtAmount.Value, ApproveLoan.Finance_Approval_Status, txtComment.Value, Session("UserEmpID"), radFairValue.SelectedText, lblfAIRVALUE.Text, txtIntRate.Value, txtMarketrate.Value, lblamortisedcost.Text, amortisedeir, chkHigherApproval2.Checked)
                Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Update Loan " & ApproveLoan.LoanRefNo, "Loan Approval Level " & txtapproverlevel.Text)

                If cboApproval.SelectedItem.Text.ToLower = "approved" Then
                    LodaDataTable(txtid.Text)
                End If

                'If status_approver2name <> cboApproval.SelectedItem.Text And cboApproval.SelectedItem.Text.ToLower = "approved" Then
                '    Process.Loan_Notification_Final(emp_emailaddr, ApproveLoan.LoanRefNo, cboApproval.SelectedItem.Text, lblEmpName.Value, lblloantype.Text, txtAmount.Value, lblrepayamount.Value, lblTenor.Value, lblrepaystartdate.Value, txtIntRate.Value, lblEmpID.Text, Session("rptAttachment"), Process.ApplicationURL + "/" + Process.GetMailLink(AuthenCode, 2))
                'End If

                If status_approver2name = cboApproval.SelectedItem.Text And cboApproval.SelectedItem.Text.ToLower = "approved" Then
                    Process.Loan_Notification_Final(emp_emailaddr, ApproveLoan.LoanRefNo, cboApproval.SelectedItem.Text, lblEmpName.Value, lblloantype.Text, txtAmount.Value, lblrepayamount.Value, lblTenor.Value, lblrepaystartdate.Value, txtIntRate.Value, lblEmpID.Text, Session("rptAttachment"), Process.ApplicationURL + "/" + Process.GetMailLink(AuthenCode, 2))
                End If

                lblstatus = "Approval Status Updated"
                Process.loadalert(divalert, msgalert, lblstatus, "success")
            End If

            'lblstatus.Text = "Status updated & notification sent"

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        Finally
            btnStatus.Disabled = False
        End Try
    End Sub



    Protected Sub cboApproval_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboApproval.SelectedIndexChanged
        Try
            If lblapprover1status.Value = "Pending" Then
                Process.loadalert(divalert, msgalert, "First Level approver is still pending, Status update has been disabled", "danger")
                Process.AssignRadComboValue(cboApproval, "Pending")
                txtapproverlevel.Text = "2"
                btnStatus.Disabled = True
            Else
                Process.loadalert(divalert, msgalert, "", "danger")
                txtapproverlevel.Text = "2"
            End If
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub radFairValue_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.DropDownListEventArgs) Handles radFairValue.SelectedIndexChanged
        Try
            Dim marketrate As Double = 0
            Dim interestrate As Double = 0
            Dim monthlypay As Double = 0
            Dim loanamount As Double = 0
            Dim tenor As Integer = 0
            Dim fairvalue As Double = 0
            Dim EIR As Double = 0
            Dim AmortEIR As Double = 0
            Dim AmortFairValue As Double = 0
            Dim repaystartdate As Date
            Dim EMPID As String = ""
            Dim LoanType As String = ""

            repaystartdate = lblrepaystartdate.Value
            tenor = lblTenor.Value
            interestrate = txtIntRate.Value
            monthlypay = lblrepayamount.Value
            loanamount = txtAmount.Value
            LoanType = lblloantype.Text
            marketrate = txtMarketrate.Value
            EIR = interestrate / 1200

            If radFairValue.SelectedText.ToUpper = "YES" Then
                lblfAIRVALUE.Text = FormatNumber(PV(marketrate / 1200, tenor, monthlypay * -1), 2)
            Else
                lblfAIRVALUE.Text = FormatNumber(txtAmount.Value, 2)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub txtMarketrate_TextChanged(sender As Object, e As EventArgs)
        Try
            If IsNumeric(txtMarketrate.Value) = True Then
                Dim marketrate As Double = 0
                Dim interestrate As Double = 0
                Dim monthlypay As Double = 0
                Dim loanamount As Double = 0
                Dim tenor As Integer = 0
                Dim fairvalue As Double = 0
                Dim EIR As Double = 0

                tenor = lblTenor.Value
                interestrate = txtIntRate.Value
                monthlypay = lblrepayamount.Value
                loanamount = txtAmount.Value
                marketrate = txtMarketrate.Value
                EIR = interestrate / 1200

                If radFairValue.SelectedText.ToUpper = "YES" Then
                    lblfAIRVALUE.Text = FormatNumber(PV(marketrate / 1200, tenor, monthlypay * -1), 2)
                Else
                    lblfAIRVALUE.Text = FormatNumber(txtAmount.Value, 2)
                End If
            Else
                If radFairValue.SelectedText.ToUpper = "YES" Then
                    lblfAIRVALUE.Text = 0
                End If
            End If

        Catch ex As Exception

        End Try

    End Sub
    Protected Sub chkHigherApproval_CheckedChanged(sender As Object, e As EventArgs) Handles chkHigherApproval2.CheckedChanged
        Try
            If chkHigherApproval2.Checked = True Then
                divhigherapproval2.Visible = True
                divhigherapproval.Visible = True
                cboApproval.Enabled = False

            Else
                divhigherapproval2.Visible = False
                divhigherapproval.Visible = False
                cboApproval.Enabled = True

            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub ReloadComponentList()
        Try
            lstMakeup.Items.Clear()
            Dim collection As IList(Of RadComboBoxItem) = cboApproverII.CheckedItems
            If (collection.Count > 0) Then
                For Each item As RadComboBoxItem In collection
                    lstMakeup.Items.Add(item.Text)

                Next
            Else
                lstMakeup.Items.Clear()
            End If
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub cboApproverII_ItemChecked(sender As Object, e As Telerik.Web.UI.RadComboBoxItemEventArgs) Handles cboApproverII.ItemChecked
        ReloadComponentList()
    End Sub
End Class