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
Public Class StaffLoans
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "LOANS"
    Dim emailFile As String = ConfigurationManager.AppSettings("EmailURL")
    'Public Function GetLocationData() As DataTable
    '    Dim table As New DataTable()
    '    table.Columns.Add("ID")
    '    table.Columns.Add("ParentID")
    '    table.Columns.Add("Value")
    '    table.Columns.Add("Text")

    '    Dim strDataSet As New DataSet
    '    strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "location_dropdown_view")
    '    If strDataSet.Tables(0).Rows.Count > 0 Then
    '        For i As Integer = 0 To strDataSet.Tables(0).Rows.Count - 1
    '            Dim id As String = ""
    '            Dim Parent As String = ""
    '            Dim Value As String = ""
    '            Dim TextField As String = ""

    '            id = strDataSet.Tables(0).Rows(i).Item("Locations").ToString
    '            If strDataSet.Tables(0).Rows(i).Item("Parent").ToString = "" Then
    '                Parent = Nothing
    '            ElseIf IsDBNull(strDataSet.Tables(0).Rows(i).Item("Parent")) Then
    '                Parent = Nothing
    '            Else
    '                Parent = strDataSet.Tables(0).Rows(i).Item("Parent").ToString
    '            End If

    '            Value = strDataSet.Tables(0).Rows(i).Item("levels").ToString
    '            TextField = strDataSet.Tables(0).Rows(i).Item("Locations").ToString

    '            table.Rows.Add(New [String]() {id, Parent, Value, TextField})
    '        Next
    '    End If

    '    Return table
    'End Function
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
    Public Function GetUnitData() As DataTable
        Dim table As New DataTable()
        table.Columns.Add("ID")
        table.Columns.Add("ParentID")
        table.Columns.Add("Value")
        table.Columns.Add("Text")

        Dim strDataSet As New DataSet
        strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Company_Structure_dropdwon")
        If strDataSet.Tables(0).Rows.Count > 0 Then
            For i As Integer = 0 To strDataSet.Tables(0).Rows.Count - 1
                Dim id As String = ""
                Dim Parent As String = ""
                Dim Value As String = ""
                Dim TextField As String = ""

                id = strDataSet.Tables(0).Rows(i).Item("ID").ToString
                If strDataSet.Tables(0).Rows(i).Item("Parent").ToString = "" Then
                    Parent = Nothing
                ElseIf IsDBNull(strDataSet.Tables(0).Rows(i).Item("Parent")) Then
                    Parent = Nothing
                Else
                    Parent = strDataSet.Tables(0).Rows(i).Item("Parent").ToString
                End If

                Value = strDataSet.Tables(0).Rows(i).Item("Name").ToString
                TextField = strDataSet.Tables(0).Rows(i).Item("Name").ToString

                table.Rows.Add(New [String]() {id, Parent, Value, TextField})
            Next
        End If

        Return table
    End Function
    Private Function LoadAllLoans(loadtype As String) As DataTable
        Dim datatables As New DataTable

        If rdoStatusType.SelectedItem.Text = "Payment" Then
            If Session("LoadType") = "All" Then
                datatables = Process.SearchDataP4("Emp_Loan_get_all_Taken", dateFrom.SelectedDate, dateTo.SelectedDate, radStatus.SelectedItem.Text, cboCompany.SelectedValue)
            ElseIf Session("LoadType") = "Find" Then
                datatables = Process.SearchDataP5("Emp_Loan_search_Taken", dateFrom.SelectedDate, dateTo.SelectedDate, txtsearch.Value, radStatus.SelectedItem.Text, cboCompany.SelectedValue)
            End If
        Else
            If Session("LoadType") = "All" Then
                datatables = Process.SearchDataP4("Finance_Emp_Loan_Get_all", dateFrom.SelectedDate, dateTo.SelectedDate, radStatus.SelectedItem.Text, cboCompany.SelectedValue)
            ElseIf Session("LoadType") = "Find" Then
                datatables = Process.SearchDataP5("Finance_Emp_Loan_Search", dateFrom.SelectedDate, dateTo.SelectedDate, txtsearch.Value, radStatus.SelectedItem.Text, cboCompany.SelectedValue)
            End If
        End If
        pagetitle.InnerText = cboCompany.SelectedValue & ": " & radStatus.SelectedItem.Text & " " & txtsearch.Value.Trim & " Staff Loans (" & FormatNumber(datatables.Rows.Count, 0) & ")"
        Return datatables
    End Function

    Private Sub LoadLoans(LoadType As String)
        Try
            GridVwHeaderChckbox.DataSource = LoadAllLoans(LoadType)
            GridVwHeaderChckbox.AllowSorting = True
            GridVwHeaderChckbox.AllowPaging = True
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception
             Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Read") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "danger")
                Exit Sub
            End If


            Session("View") = "Loans"

            If Not Me.IsPostBack Then

                Dim ismulti As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select isnull(ismulticompany,'No')  from general_info")
                If ismulti.ToLower = "no" Then
                    Process.LoadRadComboTextAndValueP2(cboCompany, "Company_Structure_Get_ByLevel", "1", Session("Access"), "name", "name", False)
                Else

                    Process.LoadRadComboTextAndValueP2(cboCompany, "Company_Structure_Get_ByLevel", "2", Session("Access"), "name", "name", False)
                End If
                If Session("company") Is Nothing Then
                    Session("company") = Session("Organisation")
                End If
                Process.AssignRadComboValue(cboCompany, Session("company"))

                Process.RadioListCheck(rdoStatusType, "Approval")
                radStatus.Items.Clear()
                If rdoStatusType.SelectedItem.Value = "Approval" Then
                    radStatus.Items.Add("Pending")
                    radStatus.Items.Add("Cancelled")
                    radStatus.Items.Add("Rejected")
                    radStatus.Items.Add("Approved")
                Else
                    radStatus.Items.Add("Active")
                    radStatus.Items.Add("Liquidated")
                End If


                'Loans and Advances
                dateFrom.SelectedDate = Date.Now.AddDays(1 - Date.Now.Day).AddMonths(1 - Date.Now.Month)
                dateTo.SelectedDate = Date.Now.AddMonths(12 - Date.Now.Month).AddDays(31 - Date.Now.Day)

                Session("LoadType") = "All"
                LoadLoans(Session("LoadType"))
            End If
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
            table = LoadAllLoans(Session("LoadType"))
            'If Session("roletype") = "ESS" Then
            '    table = Process.SearchDataP5("Emp_Loan_get_all_Taken", Session("UserEmpID"), "Approved", dateFrom.SelectedDate, dateTo.SelectedDate, radStatus.SelectedItem.Text)
            'Else
            '    table = Process.SearchDataP5("Emp_Loan_get_all_Taken", "", "Approved", dateFrom.SelectedDate, dateTo.SelectedDate, radStatus.SelectedItem.Text)
            'End If

            table.DefaultView.Sort = sortExpression & direction
            GridVwHeaderChckbox.DataSource = table
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception
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

            'If Session("roletype") = "ESS" Then
            '    GridVwHeaderChckbox.DataSource = Process.SearchDataP5("Emp_Loan_get_all_Taken", Session("UserEmpID"), "Approved", dateFrom.SelectedDate, dateTo.SelectedDate, radStatus.SelectedItem.Text)
            'Else
            '    GridVwHeaderChckbox.DataSource = Process.SearchDataP5("Emp_Loan_get_all_Taken", "", "Approved", dateFrom.SelectedDate, dateTo.SelectedDate, radStatus.SelectedItem.Text)
            'End If

            GridVwHeaderChckbox.DataSource = LoadAllLoans(Session("LoadType"))
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub OnRowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GridVwHeaderChckbox, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub


    Protected Sub Button1_Click(sender As Object, e As EventArgs)
        Try
            'If Not Me.IsPostBack Then
            If txtsearch.Value.Trim = "" Then
                Session("LoadType") = "All"
                LoadLoans("All")
            Else
                Session("LoadType") = "Find"
                LoadLoans("Find")
            End If

            'End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub Delete(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Delete") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "danger")
                Exit Sub
            End If
            Dim confirmValue As String = Request.Form("confirm_value")
            If confirmValue = "Yes" Then
                Dim atLeastOneRowDeleted As Boolean = False
                ' Iterate through the Products.Rows property
                For Each row As GridViewRow In GridVwHeaderChckbox.Rows
                    ' Access the CheckBox
                    Dim cb As CheckBox = row.FindControl("chkEmp")
                    If cb IsNot Nothing AndAlso cb.Checked Then
                        ' Delete row! (Well, not really...)
                        atLeastOneRowDeleted = True
                        ' First, get the ProductID for the selected row
                        Dim ID As String = _
                            Convert.ToString(GridVwHeaderChckbox.DataKeys(row.RowIndex).Value)
                        ' "Delete" the row
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Loan_Delete", ID)
                    End If
                Next
                LoadLoans(Session("LoadType"))
            Else
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Delete has been cancelled" + "')", True)
            End If
        Catch ex As Exception
             Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnApply_Click(sender As Object, e As EventArgs)
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                Response.Write("You don't have privilege to perform this action")
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
                Exit Sub
            End If
            Process.loadtype = "Add"
            Response.Write("<script language='javascript'> { popup = window.open(""LoanRequest.aspx"" , ""Stone Details"", ""height=800,width=800,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no"");  popup.focus(); }</script>")
        Catch ex As Exception
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    Protected Sub rdoStatusType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rdoStatusType.SelectedIndexChanged
        radStatus.Items.Clear()
        If rdoStatusType.SelectedItem.Value = "Approval" Then
            radStatus.Items.Add("Pending")
            radStatus.Items.Add("Approved")
            radStatus.Items.Add("Cancelled")
            radStatus.Items.Add("Rejected")
        Else
            radStatus.Items.Add("Active")
            radStatus.Items.Add("Liquidated")
        End If
        If txtsearch.Value.Trim = "" Then
            LoadLoans("All")
        Else
            LoadLoans("Find")
        End If
    End Sub

    Protected Sub radStatus_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles radStatus.SelectedIndexChanged
        Try
            If radStatus.SelectedItem.Text <> "Approved" And rdoStatusType.SelectedItem.Value = "Approval" Then
                btnApprove.Enabled = True
                btnApprove.BackColor = Color.FromArgb(102, 153, 0)
            Else
                btnApprove.Enabled = False
                btnApprove.BackColor = Color.Gray
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnApprove_Click(sender As Object, e As EventArgs) Handles btnApprove.Click
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
            Dim repaystartdate As Date = Date.Now
            Dim EMPID As String = ""
            Dim LoanType As String = ""
            Dim IsFairValue As String = ""
            Dim LoanRef As String = ""
            Dim RequesterID As String = ""
            Dim RequesterName As String = ""
            Dim RequesterMail As String = ""
            Dim supervisorStat As String = ""
            Dim loandesc As String = ""

            Dim finalstatus As String = ""
            Dim approver1name As String = ""
            Dim approver2name As String = ""
            Dim status_approver2name As String = ""

            Dim confirmValue As String = Request.Form("confirm_app")
            If confirmValue = "Yes" Then
                System.Threading.Thread.Sleep(300)
                Dim atLeastOneRowDeleted As Boolean = False
                ' Iterate through the Products.Rows property
                For Each row As GridViewRow In GridVwHeaderChckbox.Rows
                    ' Access the CheckBox
                    Dim cb As CheckBox = row.FindControl("chkEmp")
                    If cb IsNot Nothing AndAlso cb.Checked Then
                        atLeastOneRowDeleted = True

                        Dim ID As String = Convert.ToString(GridVwHeaderChckbox.DataKeys(row.RowIndex).Value)
                        Dim strLoan As New DataSet
                        strLoan = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Loan_get", ID)
                        If strLoan.Tables(0).Rows.Count > 0 Then
                            repaystartdate = strLoan.Tables(0).Rows(0).Item("RepaymentStartDate")
                            tenor = strLoan.Tables(0).Rows(0).Item("LoanTerm").ToString
                            interestrate = strLoan.Tables(0).Rows(0).Item("InterestRate").ToString
                            monthlypay = strLoan.Tables(0).Rows(0).Item("MonthlyPay").ToString
                            loanamount = strLoan.Tables(0).Rows(0).Item("LoanAmount").ToString
                            LoanType = strLoan.Tables(0).Rows(0).Item("LoanType").ToString
                            EIR = interestrate / 1200
                            IsFairValue = strLoan.Tables(0).Rows(0).Item("FairValueLoan").ToString
                            LoanRef = strLoan.Tables(0).Rows(0).Item("LoanRefNo").ToString
                            RequesterID = strLoan.Tables(0).Rows(0).Item("EmpID").ToString
                            supervisorStat = strLoan.Tables(0).Rows(0).Item("status").ToString
                            loandesc = strLoan.Tables(0).Rows(0).Item("description").ToString

                            finalstatus = strLoan.Tables(0).Rows(0).Item("FinalStatus").ToString
                            status_approver2name = strLoan.Tables(0).Rows(0).Item("status2").ToString
                            approver1name = strLoan.Tables(0).Rows(0).Item("Approver1Name").ToString
                            approver2name = strLoan.Tables(0).Rows(0).Item("Approver2Name").ToString
                            Dim amortisedeir As Double = 0
                            If IsFairValue.ToUpper = "YES" Then
                                fairvalue = PV(interestrate / 1200, tenor, monthlypay * -1)

                            Else
                                fairvalue = loanamount
                            End If
                            AmortFairValue = PV(interestrate / 1200, tenor, monthlypay * -1)
                            amortisedeir = Rate(CDbl(tenor), monthlypay * -1, fairvalue, 0, 0, 0)

                            'SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Loan_Update_Status_Level_2", txtid.Text, txtAmount.Text, ApproveLoan.Finance_Approval_Status, txtComment.Text, Session("UserEmpID"), radFairValue.SelectedText, lblfAIRVALUE.Text, txtIntRate.Text, txtMarketrate.Text, lblamortisedcost.Text, amortisedeir)
                            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Loan_Update_Status_Level_2", LoanRef, loanamount, "Approved", "Approved", Session("UserEmpID"), IsFairValue, fairvalue, interestrate, marketrate, AmortFairValue, amortisedeir)
                            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "auto_loan_schedule_run", CStr(CInt(LoanRef)), fairvalue, monthlypay, repaystartdate, EIR)

                            Dim strGrade As New DataSet
                            Dim approvername As String = ""

                            'Get Admin Approver
                            strGrade = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "select name, email from dbo.Employees_All where empid = '" & Session("UserEmpID") & "'")
                            If strGrade.Tables(0).Rows.Count > 0 Then
                                approvername = strGrade.Tables(0).Rows(0).Item("name").ToString
                            End If

                            'Get Employee
                            strGrade = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "select name, email from dbo.Employees_All where empid = '" & RequesterID & "'")

                            If strGrade.Tables(0).Rows.Count > 0 Then
                                RequesterMail = strGrade.Tables(0).Rows(0).Item("email").ToString
                                RequesterName = strGrade.Tables(0).Rows(0).Item("name").ToString
                            End If
                            LodaDataTable(ID)
                            Process.Loan_Notification_Final(RequesterMail, LoanRef, "Approved", RequesterName, LoanType, loanamount, monthlypay, tenor, repaystartdate, interestrate, RequesterID, Session("rptAttachment"), Process.GetMailLink(AuthenCode, 2))
                            'Send Mails to Finance Team
                            'Process.Loan_Finance_Level2_Approval(Process.GetMailList("FINANCE"), LoanRef, RequesterName, LoanType, loanamount, monthlypay, repaystartdate, loandesc, supervisorStat, approvername, interestrate)

                            'Process.Loan_Notification_Final(RequesterMail, LoanRef, "Approved", RequesterName, LoanType, loanamount, monthlypay, repaystartdate, loandesc, supervisorStat, approver1name, status_approver2name, approver2name, interestrate, RequesterID, "")
                        End If
                    End If
                Next
                If atLeastOneRowDeleted = True Then
                    Response.Write("Multiple Loans Approved successful")
                    ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Multiple Loans Approved successful" + "')", True)
                Else
                    ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Multiple Approval cancelled, no selection made" + "')", True)
                End If
                LoadLoans(Session("LoadType"))
            Else
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Multiple Approval cancelled" + "')", True)
            End If
        Catch ex As Exception
             Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Private Sub cboCompany_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboCompany.SelectedIndexChanged
        Try

            Session("company") = cboCompany.SelectedValue
            Session("LoadType") = "All"
            LoadLoans(Session("LoadType"))
        Catch ex As Exception

        End Try
    End Sub
End Class