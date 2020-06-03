Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports GOSHRM.GOSHRM.GOSHRM.BO
Public Class LoansSchedule
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "EMPLOAN"
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
    Dim repayment As New clsRepaymentLoan
    Dim olddata(5) As String
    Dim lblstatus As String
    Private Sub PaymentVisible()
        Try
            lblPayLine1.Visible = True
            lblPayLine2.Visible = True
            lblPaymentAmount.Visible = True
            lblPaymentDate.Visible = True
            txtPaymentAmount.Visible = True
            radPaymentDate.Visible = True
            btnSavePayment.Visible = True
            btnClosePayment.Visible = True
            lblPaymentRate.Visible = True
            txtPaymentRate.Visible = True
            txtPaymentRate.Value = interestrate
            txtPaymentAmount.Value = lblMonthlyPay.Value
            btnRepay.Visible = False
            btnDeleteRepay.Visible = False
        Catch ex As Exception

        End Try
    End Sub
    Private Sub PaymentInVisible()
        Try
            lblPayLine1.Visible = False
            lblPayLine2.Visible = False
            lblPaymentAmount.Visible = False
            lblPaymentDate.Visible = False
            txtPaymentAmount.Visible = False
            radPaymentDate.Visible = False
            btnSavePayment.Visible = False
            btnClosePayment.Visible = False
            lblPaymentRate.Visible = False
            txtPaymentRate.Visible = False
            btnRepay.Visible = True
            btnDeleteRepay.Visible = True
        Catch ex As Exception

        End Try
    End Sub
    Private Sub LoadPayments(ByVal LoanrefNo As String)
        Try
            GridRepay.DataSource = Process.SearchData("Emp_Loan_Schedule_Get_All", LoanrefNo)
            GridRepay.AllowSorting = False
            GridRepay.AllowPaging = True
            GridRepay.DataBind()
        Catch ex As Exception
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub
    Private Function RepaySchedule() As DataTable
        EMPID = lblEmpID.Text
        repaystartdate = lblStartDate.Value
        tenor = lblTenor.Value
        interestrate = lblInterestRate.Value
        monthlypay = lblMonthlyPay.Value
        loanamount = lblLoanAmt.Value
        LoanType = lblLoanType.Value
        EIR = interestrate / 1200

        'If chkFairValue.Checked = True Then
        '    fairvalue = PV((CDbl(lblAmortMarketRate.Text)) / 1200, tenor, monthlypay * -1) ' PV(interestrate / 1200, tenor, monthlypay * -1)
        'Else
        fairvalue = lblLoanAmt.Value
        'End If


        lblEIR.Value = EIR
        'lblFairValue.Text = FormatNumber(fairvalue, 2)
        'LoadRepaySchedule(fairvalue, monthlypay, repaystartdate, EIR)
        Return Process.SearchDataP4("loan_schedule_run", fairvalue, monthlypay, repaystartdate, EIR)
    End Function
    Private Function AmortSchedule() As DataTable
        EMPID = lblEmpID.Text
        repaystartdate = lblStartDate.Value
        interestrate = lblInterestRate.Value
        monthlypay = lblMonthlyPay.Value
        LoanType = lblLoanType.Value
        tenor = lblTenor.Value
        lblAmortTenor.Value = tenor
        lblAmortLoanAmount.Value = FormatNumber(lblLoanAmt.Value, 2)
        lblAmortMonthlyPay.Value = FormatNumber(lblMonthlyPay.Value, 2)
        lblAmortRepayStartDate.Value = FormatDateTime(lblStartDate.Value, DateFormat.ShortDate)
        'marketrate = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Loan_Rules_get_Market_Rate", EMPID, LoanType)
        lblAmortMarketRate.Value = FormatNumber(marketrate, 2)
        AmortFairValue = PV((marketrate) / 1200, tenor, monthlypay * -1)
        AmortEIR = Rate(CDbl(tenor), monthlypay * -1, AmortFairValue, 0, 0, 0)
        lblAmortFairValue.Value = FormatNumber(AmortFairValue, 2)
        lblAmortEIR.Value = FormatNumber(AmortEIR * 100, 4)
        'LoadAmortisation(AmortFairValue, monthlypay, repaystartdate, AmortEIR)
        Return Process.SearchDataP4("loan_schedule_run", AmortFairValue, monthlypay, repaystartdate, AmortEIR)
    End Function

    Private Sub LoadRepaySchedule()
        Try
            'ByVal FairValue As Double, ByVal MonthlyPay As Double, ByVal RepayDate As Date, ByVal Rate As Double
            GridVwHeaderChckbox.DataSource = RepaySchedule() ' Process.SearchDataP4("loan_schedule_run", FairValue, MonthlyPay, RepayDate, Rate)
            GridVwHeaderChckbox.AllowSorting = True
            GridVwHeaderChckbox.AllowPaging = True
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Sub LoadAmortisation()
        Try
            GridView1.DataSource = AmortSchedule() 'Process.SearchDataP4("loan_schedule_run", FairValue, MonthlyPay, RepayDate, Rate)

            GridView1.AllowSorting = True
            GridView1.AllowPaging = True
            GridView1.DataBind()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "warning")
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then

                If Session("clicked") Is Nothing Then
                    Session("clicked") = "0"
                End If

                'Load Payment
                LoadPayments(Request.QueryString("id"))

                'Loan Schedule
                Dim strUser As New DataSet
                lblLoanID.Value = Request.QueryString("id")
                strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Loan_get", Request.QueryString("id"))
                EMPID = strUser.Tables(0).Rows(0).Item("empid").ToString
                lblEmpID.Text = strUser.Tables(0).Rows(0).Item("empid").ToString
                lblBorrower.Value = strUser.Tables(0).Rows(0).Item("EmployeeName").ToString

                lblLoanAmt.Value = FormatNumber(CDbl(strUser.Tables(0).Rows(0).Item("LoanAmount").ToString), 2)
                lblMonthlyPay.Value = FormatNumber(CDbl(strUser.Tables(0).Rows(0).Item("monthlypay").ToString), 2)
                lblInterestRate.Value = FormatNumber(CDbl(strUser.Tables(0).Rows(0).Item("interestrate").ToString), 2)
                lblTenor.Value = FormatNumber(CDbl(strUser.Tables(0).Rows(0).Item("loanterm").ToString), 2)
                lblStartDate.Value = FormatDateTime(strUser.Tables(0).Rows(0).Item("RepaymentStartDate").ToString, DateFormat.ShortDate)
                lblLoanType.Value = strUser.Tables(0).Rows(0).Item("LoanType").ToString

                lblAmortInterestRate.Value = FormatNumber(CDbl(strUser.Tables(0).Rows(0).Item("interestrate").ToString), 2)
                lblAmortMarketRate.Value = FormatNumber(CDbl(strUser.Tables(0).Rows(0).Item("marketrate").ToString), 2)
                marketrate = lblAmortMarketRate.Value
                Dim isfairV As String = strUser.Tables(0).Rows(0).Item("FairValueLoan").ToString
                'If isfairV.ToUpper = "YES" Then
                '    chkFairValue.Checked = True
                'Else
                '    chkFairValue.Checked = False
                'End If


                lblRepayLoanAmount.Value = lblLoanAmt.Value
                lblRepayFairValue.Value = FormatNumber(CDbl(strUser.Tables(0).Rows(0).Item("FairValue").ToString), 2)
                lblRepayMonthly.Value = lblMonthlyPay.Value
                lblRepayStartDate.Value = lblStartDate.Value
                lblRepayTenor.Value = lblTenor.Value
                lblRepayIntRate.Value = lblInterestRate.Value
                lblRepayEIR.Value = interestrate / 1200

                LoadRepaySchedule()
                'fairvalue, monthlypay, repaystartdate, EIR
                LoadAmortisation()
                If lblEmpID.Text = Session("UserEmpID") Then
                    btnRepay.Visible = False
                    btnDeleteRepay.Visible = False
                    btnRepay.Height = Unit.Pixel(0)
                    btnDeleteRepay.Height = Unit.Pixel(0)
                End If
                Startbutton()

            End If
            PaymentInVisible()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub SortRecords(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Try
            Dim sortExpression As String = e.SortExpression
            Dim direction As String = String.Empty
            If SortDirection = SortDirection.Ascending Then
                SortDirection = SortDirection.Descending
                direction = " DESC"
            Else
                SortDirection = SortDirection.Ascending
                direction = " ASC"
            End If
            Dim table As New DataTable
            table = RepaySchedule() 'Process.SearchDataP4("loan_schedule_run", fairvalue, monthlypay, repaystartdate, EIR)
            table.DefaultView.Sort = sortExpression & direction
            GridVwHeaderChckbox.DataSource = table
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub SortSurbodinateRecords(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Try
            Dim sortExpression As String = e.SortExpression
            Dim direction As String = String.Empty
            If SortDirection = SortDirection.Ascending Then
                SortDirection = SortDirection.Descending
                direction = " DESC"
            Else
                SortDirection = SortDirection.Ascending
                direction = " ASC"
            End If
            Dim table As New DataTable
            table = AmortSchedule() ' Process.SearchDataP4("loan_schedule_run", AmortFairValue, monthlypay, repaystartdate, AmortEIR)

            table.DefaultView.Sort = sortExpression & direction
            GridView1.DataSource = table
            GridView1.DataBind()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Public Property SortDirection() As SortDirection
        Get
            If ViewState("SortDirection") Is Nothing Then
                ViewState("SortDirection") = SortDirection.Ascending
            End If
            Return DirectCast(ViewState("SortDirection"), SortDirection)
        End Get
        Set(ByVal value As SortDirection)
            ViewState("SortDirection") = value
        End Set
    End Property


    Protected Sub GridVwHeaderChckbox_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridVwHeaderChckbox.PageIndexChanging
        Try
            GridVwHeaderChckbox.PageIndex = e.NewPageIndex

            GridVwHeaderChckbox.DataSource = RepaySchedule() 'Process.SearchDataP4("loan_schedule_run", fairvalue, monthlypay, repaystartdate, EIR)
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception
            'lblstatus.Text = ex.Message
        End Try
    End Sub

    Protected Sub OnRowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GridVwHeaderChckbox, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub
    Protected Sub OnRowSurbodinateDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GridView1, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub

    Protected Sub GridView1_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        Try
            GridView1.PageIndex = e.NewPageIndex
            GridView1.DataSource = AmortSchedule() 'Process.SearchDataP4("loan_schedule_run", AmortFairValue, monthlypay, repaystartdate, AmortEIR)
            GridView1.DataBind()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub




    'Protected Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
    '    Try
    '        Response.Redirect("~/Module/Finance/Loans/LoansApproval.aspx", True)
    '    Catch ex As Exception
    '    End Try
    'End Sub

    Protected Sub btnRepay_Click(sender As Object, e As EventArgs) Handles btnRepay.Click
        Try
            Dim IsApproved As Boolean = CBool(SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "IsLoanApproved", lblLoanID.Value))
            If IsApproved = False Then
                lblPayStatus.Text = "Loan not fully approved"
                Exit Sub
            End If
            PaymentVisible()
        Catch ex As Exception
            lblPayStatus.Text = ex.Message
            Process.loadalert(divalert, msgalert, ex.Message, "warning")
        End Try
    End Sub

    Protected Sub btnClosePayment_Click(sender As Object, e As EventArgs) Handles btnClosePayment.Click
        Try
            PaymentInVisible()

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btnSavePayment_Click(sender As Object, e As EventArgs) Handles btnSavePayment.Click
        Try

            If IsNumeric(txtPaymentAmount.Value) = False Or CDbl(txtPaymentAmount.Value) <= 0 Then
                lblPayStatus.Text = "Payment Amount required!"
                txtPaymentAmount.Focus()
                Exit Sub
            End If

            repayment.Payment = txtPaymentAmount.Value
            repayment.LoanRefNo = lblLoanID.Value
            repayment.InterestRate = txtPaymentRate.Value
            repayment.PaymentDate = radPaymentDate.SelectedDate

            Dim OldValue As String = ""
            Dim NewValue As String = ""

            Dim j As Integer = 0

            For Each a In GetType(clsRepaymentLoan).GetProperties() 'New Entries
                If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
                    If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                        If a.GetValue(repayment, Nothing) = Nothing Then
                            NewValue += a.Name + ":" + " " & vbCrLf
                        Else
                            NewValue += a.Name + ": " + a.GetValue(repayment, Nothing).ToString & vbCrLf
                        End If
                    End If
                End If
            Next
            btnSavePayment.Enabled = False

            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Loan_Schedule_Update", repayment.LoanRefNo, repayment.PaymentDate, repayment.Payment, repayment.InterestRate, Session("UserEmpID"), "manual")
            Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Inserted", "Loan Repayment")
            lblPayStatus.Text = "Monthly Payment saved"
        Catch ex As Exception
            lblPayStatus.Text = ex.Message
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        Finally
            btnSavePayment.Enabled = True
            PaymentInVisible()
            LoadPayments(lblLoanID.Value)
        End Try
    End Sub

    Protected Sub btnRepay0_Click(sender As Object, e As EventArgs)

    End Sub

    Protected Sub btnDeleteRepay_Click(sender As Object, e As EventArgs) Handles btnDeleteRepay.Click
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Delete") = False Then
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
                Exit Sub
            End If
            'Dim confirmValue As String = Request.Form("confirm_value")
            Dim confirmValue As String = "Yes"
            If confirmValue = "Yes" Then
                Dim atLeastOneRowDeleted As Boolean = False
                ' Iterate through the Products.Rows property
                For Each row As GridViewRow In GridRepay.Rows
                    ' Access the CheckBox
                    Dim cb As CheckBox = row.FindControl("chkEmp")
                    If cb IsNot Nothing AndAlso cb.Checked Then
                        ' Delete row! (Well, not really...)
                        atLeastOneRowDeleted = True
                        ' First, get the ProductID for the selected row
                        Dim ID As String = _
                            Convert.ToString(GridRepay.DataKeys(row.RowIndex).Value)
                        ' "Delete" the row
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Loan_Schedule_Delete", ID)
                    End If
                Next
                LoadPayments(lblLoanID.Value)
                lblstatus = "Record Deleted Successfully"
                Process.loadalert(divalert, msgalert, lblstatus, "success")
            Else
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Delete has been cancelled" + "')", True)
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub GridRepay_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridRepay.PageIndexChanging
        Try
            GridRepay.PageIndex = e.NewPageIndex
            GridRepay.DataSource = Process.SearchData("Emp_Loan_Schedule_Get_All", lblLoanID.Value)
            GridRepay.DataBind()
            If GridRepay.Rows.Count > 0 Then
                Process.EnableButton(btnDeleteRepay)
            Else
                Process.DisableButton(btnDeleteRepay)
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    'Protected Sub chkFairValue_CheckedChanged(sender As Object, e As EventArgs) Handles chkFairValue.CheckedChanged
    '    Try
    '        LoadRepaySchedule()
    '    Catch ex As Exception
    '        lblstatus.Text = ex.Message
    '    End Try
    'End Sub

    Protected Sub btnExportAmort_Click(sender As Object, e As EventArgs) Handles btnExportAmort.Click
        Try
            Dim TitleHeader(10) As String
            Dim TitleData(10) As String
            Dim ColumnHeaders(6) As String
            Dim Columns(6) As String
            Dim filename As String = ""

            TitleHeader(0) = "Employee"
            TitleHeader(1) = "Loan Reference"
            TitleHeader(2) = "Loan Type"
            TitleHeader(3) = "Amount"
            TitleHeader(4) = "Fair Value"
            TitleHeader(5) = "Repayment"
            TitleHeader(6) = "Start Date"
            TitleHeader(7) = "Tenor (Month)"
            TitleHeader(8) = "Interest Rate(%)"
            TitleHeader(9) = "Market Rate(%)"
            TitleHeader(10) = "EIR(%)"

            TitleData(0) = lblBorrower.Value
            TitleData(1) = lblLoanID.Value
            TitleData(2) = lblLoanType.Value
            TitleData(3) = lblAmortLoanAmount.Value.Replace(",", "")
            TitleData(4) = lblAmortFairValue.Value.Replace(",", "")
            TitleData(5) = lblAmortMonthlyPay.Value.Replace(",", "")
            TitleData(6) = lblAmortRepayStartDate.Value.Replace(",", "")
            TitleData(7) = lblAmortTenor.Value.Replace(",", "")
            TitleData(8) = lblAmortInterestRate.Value.Replace(",", "")
            TitleData(9) = lblAmortMarketRate.Value.Replace(",", "")
            TitleData(10) = lblAmortEIR.Value.Replace(",", "")

            ColumnHeaders(0) = "Payment No"
            ColumnHeaders(1) = "Payment Date"
            ColumnHeaders(2) = "Payment"
            ColumnHeaders(3) = "Principal"
            ColumnHeaders(4) = "Interest"
            ColumnHeaders(5) = "Total Interest"
            ColumnHeaders(6) = "Outstanding Balance"

            Columns(0) = "payno"
            Columns(1) = "PaymentDate"
            Columns(2) = "Payment"
            Columns(3) = "Principal"
            Columns(4) = "Interest"
            Columns(5) = "TotalInterest"
            Columns(6) = "Balance"

            filename = "AmortisedCost-" & lblEmpID.Text & ":" & lblLoanID.Value
            Dim dataTables As DataTable = Process.SearchDataP4("loan_schedule_run", TitleData(4).ToString, TitleData(5).ToString, CDate(lblAmortRepayStartDate.Value), TitleData(10).ToString)    ' Process.SearchDataP4("loan_schedule_run", AmortFairValue, monthlypay, repaystartdate, AmortEIR)
            'Process.ExportDataSet(TitleHeader, TitleData, dataTables, ColumnHeaders, Columns, filename)
            If Process.ExportExcel(dataTables, filename) = False Then
                lblstatus = Process.strExp
                Process.loadalert(divalert, msgalert, lblstatus, "success")
            Else
                lblstatus = "File saved as " & filename & ".xls"
                Process.loadalert(divalert, msgalert, lblstatus, "success")
            End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnExportSchedule_Click(sender As Object, e As EventArgs) Handles btnExportSchedule.Click
        Try
            Dim TitleHeader(8) As String
            Dim TitleData(8) As String
            Dim ColumnHeaders(6) As String
            Dim Columns(6) As String
            Dim filename As String = ""

            TitleHeader(0) = "Employee"
            TitleHeader(1) = "Loan Reference"
            TitleHeader(2) = "Loan Type"
            TitleHeader(3) = "Amount"
            TitleHeader(4) = "Repayment"
            TitleHeader(5) = "Start Date"
            TitleHeader(6) = "Tenor (Month)"
            TitleHeader(7) = "Interest Rate(%)"
            TitleHeader(8) = "EIR(%)"

            TitleData(0) = lblBorrower.Value
            TitleData(1) = lblLoanID.Value
            TitleData(2) = lblLoanType.Value
            TitleData(3) = lblLoanAmt.Value.Replace(",", "")
            TitleData(4) = lblMonthlyPay.Value.Replace(",", "")
            TitleData(5) = lblRepayStartDate.Value.Replace(",", "")
            TitleData(6) = lblTenor.Value.Replace(",", "")
            TitleData(7) = lblInterestRate.Value.Replace(",", "")
            TitleData(8) = lblEIR.Value.Replace(",", "")

            ColumnHeaders(0) = "Payment No"
            ColumnHeaders(1) = "Payment Date"
            ColumnHeaders(2) = "Payment"
            ColumnHeaders(3) = "Principal"
            ColumnHeaders(4) = "Interest"
            ColumnHeaders(5) = "Total Interest"
            ColumnHeaders(6) = "Outstanding Balance"

            Columns(0) = "payno"
            Columns(1) = "PaymentDate"
            Columns(2) = "Payment"
            Columns(3) = "Principal"
            Columns(4) = "Interest"
            Columns(5) = "TotalInterest"
            Columns(6) = "Balance"

            filename = "Schedule" & "-" & lblEmpID.Text & ":" & lblLoanID.Value
            Dim dataTables As DataTable = Process.SearchDataP4("loan_schedule_run", TitleData(4).ToString, TitleData(5).ToString, CDate(lblAmortRepayStartDate.Value), TitleData(10).ToString)    ' Process.SearchDataP4("loan_schedule_run", AmortFairValue, monthlypay, repaystartdate, AmortEIR)
            'Process.ExportDataSet(TitleHeader, TitleData, dataTables, ColumnHeaders, Columns, filename)
            lblstatus = "File saved as " & filename & ".csv"
            Process.loadalert(divalert, msgalert, lblstatus, "success")
            If Process.ExportExcel(dataTables, filename) = False Then
                lblstatus = Process.strExp
                Process.loadalert(divalert, msgalert, lblstatus, "success")
            Else
                lblstatus = "File saved as " & filename & ".xls"
                Process.loadalert(divalert, msgalert, lblstatus, "success")
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnExportPay_Click(sender As Object, e As EventArgs) Handles btnExportPay.Click
        Try
            'lblstatus.Text = ""
            Dim TitleHeader(8) As String
            Dim TitleData(8) As String
            Dim ColumnHeaders(5) As String
            Dim Columns(5) As String
            Dim filename As String = ""

            TitleHeader(0) = "Employee"
            TitleHeader(1) = "Loan Reference"
            TitleHeader(2) = "Loan Type"
            TitleHeader(3) = "Amount"
            TitleHeader(4) = "Repayment"
            TitleHeader(5) = "Start Date"
            TitleHeader(6) = "Tenor (Month)"
            TitleHeader(7) = "Interest Rate(%)"
            TitleHeader(8) = "EIR(%)"

            TitleData(0) = lblBorrower.Value
            TitleData(1) = lblLoanID.Value
            TitleData(2) = lblLoanType.Value
            TitleData(3) = lblLoanAmt.Value.Replace(",", "")
            TitleData(4) = lblMonthlyPay.Value.Replace(",", "")
            TitleData(5) = lblRepayStartDate.Value.Replace(",", "")
            TitleData(6) = lblTenor.Value.Replace(",", "")
            TitleData(7) = lblInterestRate.Value.Replace(",", "")
            TitleData(8) = lblEIR.Value.Replace(",", "")

            ColumnHeaders(0) = "Payment No"
            ColumnHeaders(1) = "Payment Date"
            ColumnHeaders(2) = "Payment"
            ColumnHeaders(3) = "Principal"
            ColumnHeaders(4) = "Interest"
            ColumnHeaders(5) = "Outstanding Balance"

            Columns(0) = "paymentno"
            Columns(1) = "PaymentDate"
            Columns(2) = "Payment"
            Columns(3) = "Principal"
            Columns(4) = "Interest"
            Columns(5) = "Balance"

            filename = lblEmpID.Text & ":" & lblLoanID.Value
            Dim dataTables As DataTable = Process.SearchData("Emp_Loan_Schedule_Get_All", lblLoanID.Value)
            'Process.ExportDataSet(TitleHeader, TitleData, dataTables, ColumnHeaders, Columns, filename)
            lblstatus = "File saved as " & filename & ".csv"
            Process.loadalert(divalert, msgalert, lblstatus, "success")
            If Process.ExportExcel(dataTables, filename) = False Then
                lblstatus = Process.strExp
                Process.loadalert(divalert, msgalert, lblstatus, "success")
            Else
                lblstatus = "File saved as " & filename & ".xls"
                Process.loadalert(divalert, msgalert, lblstatus, "success")
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Sub Startbutton()
        Try
            Session("clicked") = 0
            Process.DeactivateButton(btnloanrepay)
            Process.DeactivateButton(btnloanschedule)
            Process.DeactivateButton(btnamortised)
            Process.ActivateButton(btnloanrepay)
            MultiView1.ActiveViewIndex = 0
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub loanrepay_Click(sender As Object, e As EventArgs) Handles btnloanrepay.Click
        Startbutton()
    End Sub

    Protected Sub btnloanschedule_Click(sender As Object, e As EventArgs) Handles btnloanschedule.Click
        Try
            Session("clicked") = 1
            Process.DeactivateButton(btnloanrepay)
            Process.DeactivateButton(btnloanschedule)
            Process.DeactivateButton(btnamortised)
            Process.ActivateButton(btnloanschedule)
            MultiView1.ActiveViewIndex = 1
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnamortised_Click(sender As Object, e As EventArgs) Handles btnamortised.Click
        Try
            Session("clicked") = 2
            Process.DeactivateButton(btnloanrepay)
            Process.DeactivateButton(btnloanschedule)
            Process.DeactivateButton(btnamortised)
            Process.ActivateButton(btnamortised)
            MultiView1.ActiveViewIndex = 2
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Try
            Response.Redirect("~/Module/Finance/Loans/StaffLoans.aspx", True)
        Catch ex As Exception
        End Try
    End Sub
End Class