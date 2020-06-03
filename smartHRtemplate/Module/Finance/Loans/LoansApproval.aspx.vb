Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Public Class LoansApproval
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "APPLOANS"
   

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
   
    Private Function MyLoansToApprove(LoadType As String) As DataTable
        Dim DataTables As New DataTable
        If LoadType = "All" Then
            DataTables = Process.SearchDataP4("Emp_Loan_Approver_get_all", Session("UserEmpID"), cboSubStatus.SelectedItem.Text, Process.DDMONYYYY(radSubDateFrom.SelectedDate), Process.DDMONYYYY(radSubDateTo.SelectedDate))

        ElseIf LoadType = "Find" Then
            DataTables = Process.SearchDataP5("Emp_Loan_Approver_get_search", Session("UserEmpID"), cboSubStatus.SelectedItem.Text, radSubDateFrom.SelectedDate, radSubDateTo.SelectedDate, txtSubSearch.Value)
        End If
        pagetitle.InnerText = "APPROVAL PAGE :- " & cboSubStatus.SelectedItem.Text & ": as at " & Process.DDMONYYYY(radSubDateTo.SelectedDate) & " Loans (" & DataTables.Rows.Count.ToString & ")"
        Return DataTables
    End Function
    
    Private Sub LoadSurbodinateLeaves(LoadType As String, ByVal pagein As Integer)
        Try

            GridView1.PageIndex = pagein
            GridView1.DataSource = MyLoansToApprove(LoadType)
            GridView1.AllowSorting = True
            GridView1.AllowPaging = True
            GridView1.DataBind()

        Catch ex As Exception
            response.write(ex.message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Read") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "danger")
                Exit Sub
            End If

            Session("View") = "LoanAndAdvances"
            If Not Me.IsPostBack Then
                Dim script As String = "$(document).ready(function () { $('[id*=btnApprove]').click(); });"
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "load", script, True)


                cboSubStatus.Items.Clear()
                cboSubStatus.Items.Add("Pending")
                cboSubStatus.Items.Add("Cancelled")
                cboSubStatus.Items.Add("Rejected")
                cboSubStatus.Items.Add("Approved")


                'Loans and Advances

                'Surbodinate Leave
                radSubDateFrom.SelectedDate = Date.Now.AddDays(1 - Date.Now.Day).AddMonths(1 - Date.Now.Month)
                radSubDateTo.SelectedDate = Date.Now.AddMonths(12 - Date.Now.Month).AddDays(31 - Date.Now.Day)

                Session("pageIndex1") = 0
                LoadSurbodinateLeaves("All", 0)

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
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
            table = MyLoansToApprove(Session("LoadType"))

            table.DefaultView.Sort = sortExpression & direction
            GridView1.PageIndex = Session("pageIndex1")
            GridView1.DataSource = table
            GridView1.DataBind()
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

    Protected Sub OnRowSurbodinateDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GridView1, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub



    Protected Sub GridView1_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        Try
            GridView1.PageIndex = e.NewPageIndex
            Session("pageIndex1") = e.NewPageIndex
            GridView1.DataSource = MyLoansToApprove(Session("LoadType"))
            GridView1.DataBind()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


    Protected Sub btnSubFind_Click(sender As Object, e As EventArgs)
        Try
            'If Not Me.IsPostBack Then
            If txtSubSearch.Value.Trim = "" Then
                Session("LoadType") = "All"
            Else
                Session("LoadType") = "Find"
            End If
            Session("pageIndex1") = 0
            LoadSurbodinateLeaves(Session("LoadType"), 0)
            'End If
        Catch ex As Exception
           Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


    Protected Sub cboSubStatus_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboSubStatus.SelectedIndexChanged
        Try
            If cboSubStatus.SelectedItem.Text <> "Approved" Then
                Process.EnableButton(btnApprove)
            Else
                Process.DisableButton(btnApprove)
            End If
            Session("pageIndex1") = 0
            LoadSurbodinateLeaves(Session("LoadType"), 0)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
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

            Dim confirmValue As String = Request.Form("confirm_app")
            If confirmValue = "No" Then
                Process.loadalert(divalert, msgalert, "Multiple Approval cancelled", "danger")
            Else

                System.Threading.Thread.Sleep(1000)
                Dim atLeastOneRowDeleted As Boolean = False
                ' Iterate through the Products.Rows property
                For Each row As GridViewRow In GridView1.Rows
                    ' Access the CheckBox
                    Dim cb As CheckBox = row.FindControl("chkEmp")
                    If cb IsNot Nothing AndAlso cb.Checked Then
                        atLeastOneRowDeleted = True

                        Dim ID As String = Convert.ToString(GridView1.DataKeys(row.RowIndex).Value)
                        Dim strLoan As New DataSet
                        strLoan = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Loan_get", ID)
                        If strLoan.Tables(0).Rows.Count > 0 Then
                            'repaystartdate = strLoan.Tables(0).Rows(0).Item("RepaymentStartDate")
                            'tenor = strLoan.Tables(0).Rows(0).Item("LoanTerm").ToString
                            'interestrate = strLoan.Tables(0).Rows(0).Item("InterestRate").ToString
                            'monthlypay = strLoan.Tables(0).Rows(0).Item("MonthlyPay").ToString
                            'loanamount = strLoan.Tables(0).Rows(0).Item("LoanAmount").ToString
                            'LoanType = strLoan.Tables(0).Rows(0).Item("LoanType").ToString
                            'EIR = interestrate / 1200
                            'IsFairValue = strLoan.Tables(0).Rows(0).Item("FairValueLoan").ToString
                            LoanRef = strLoan.Tables(0).Rows(0).Item("LoanRefNo").ToString
                            RequesterID = strLoan.Tables(0).Rows(0).Item("EmpID").ToString
                            'supervisorStat = strLoan.Tables(0).Rows(0).Item("status").ToString
                            'loandesc = strLoan.Tables(0).Rows(0).Item("description").ToString

                            'If IsFairValue.ToUpper = "YES" Then
                            '    fairvalue = PV(interestrate / 1200, tenor, monthlypay * -1)
                            'Else
                            '    fairvalue = loanamount
                            'End If

                            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Loan_Update_Status", ID, "Approved", "Approved")

                            'SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Loan_Update_Status_Level_2", LoanRef, supervisorStat, "Approved", "2", Session("UserEmpID"), Session("LoginID"), IsFairValue, fairvalue, interestrate)
                            'SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "auto_loan_schedule_run", CStr(CInt(LoanRef)), fairvalue, monthlypay, repaystartdate, EIR)

                            'Dim strGrade As New DataSet
                            'Dim approvername As String = ""

                            ''Get Admin Approver
                            'strGrade = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "select name, email from dbo.Employees_All where empid = '" & Session("UserEmpID") & "'")
                            'If strGrade.Tables(0).Rows.Count > 0 Then
                            '    approvername = strGrade.Tables(0).Rows(0).Item("name").ToString
                            'End If

                            ''Get Employee
                            'strGrade = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "select name, email from dbo.Employees_All where empid = '" & RequesterID & "'")

                            'If strGrade.Tables(0).Rows.Count > 0 Then
                            '    RequesterMail = strGrade.Tables(0).Rows(0).Item("email").ToString
                            '    RequesterName = strGrade.Tables(0).Rows(0).Item("name").ToString
                            'End If

                            'Send Mails to Finance Team
                            'Process.Loan_Finance_Level1_Approval(Process.GetMailList("FINANCE"), LoanRef, RequesterName, LoanType, loanamount, monthlypay, repaystartdate, loandesc, supervisorStat, approvername, RequesterID, Session("UserEmpID"))
                            Process.Loan_Approver_Approval(LoanRef, "Approved", RequesterID, Session("UserEmpID"), Process.ApplicationURL + "/" + Process.GetMailLink(AuthenCode, 2))
                            Process.Loan_Approver_HR_Notification(LoanRef, RequesterID, Session("UserEmpID"), "Approved", Process.ApplicationURL + "/" + Process.GetMailLink(AuthenCode, 1))

                        End If
                    End If
                Next
                If atLeastOneRowDeleted = True Then
                    Response.Write("Multiple Loans Approved successful")
                    ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Multiple Loans Approved successful" + "')", True)
                Else
                    ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Multiple Approval cancelled, no selection made" + "')", True)
                End If
                LoadSurbodinateLeaves(Session("LoadType"), Session("pageIndex1"))

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    
End Class