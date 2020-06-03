Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports GOSHRM.GOSHRM.GOSHRM.BO
Public Class ExpenseMasterUpdate
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "EMPNONPAYROLL"
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
    Dim EmpSalary As New clsEmployeeExpense
    Dim olddata(3) As String
    Dim Pages As String = "Employee Non Payroll Expense"


    Private Sub LoadPayments(ByVal id As String)
        Try
            GridRepay.DataSource = Process.SearchData("Finance_Non_Payroll_Employee_Get_All", id)
            GridRepay.AllowSorting = False
            GridRepay.AllowPaging = False
            GridRepay.DataBind()
        Catch ex As Exception
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub
    Private Sub LoadDetail(id As String)
        Dim strSalary As New DataSet
        strSalary = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Finance_Non_Salary_Primary_Get", id)
        If strSalary.Tables(0).Rows.Count > 0 Then
            lblEmpID.Text = strSalary.Tables(0).Rows(0).Item("empid").ToString
            lblGrade.Text = strSalary.Tables(0).Rows(0).Item("grade").ToString
            lblName.Text = strSalary.Tables(0).Rows(0).Item("name").ToString
            lblLocation.Text = strSalary.Tables(0).Rows(0).Item("location").ToString
            lbljobtitle.Text = strSalary.Tables(0).Rows(0).Item("jobtitle").ToString
            lblOffice.Text = strSalary.Tables(0).Rows(0).Item("office").ToString
            lblactive.Text = strSalary.Tables(0).Rows(0).Item("active").ToString
            lblnetpay.Text = FormatNumber(CDbl(strSalary.Tables(0).Rows(0).Item("amount").ToString), 2)
            LoadPayments(id)
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
             If Not Me.IsPostBack Then
                LoadDetail(Request.QueryString("id"))

            End If
        Catch ex As Exception
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub






    Protected Sub OnRowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GridRepay, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub





    Protected Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Try
            Response.Write("<script language='javascript'> { self.close() }</script>")
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btnRepay_Click(sender As Object, e As EventArgs) Handles btnRepay.Click
        Try
            If CBool(lblactive.Text) = False Then
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Currently inactive, changes have been aborted" + "')", True)
                lblPayStatus.Text = "Currently inactive, changes have been aborted"
                LoadPayments(lblEmpID.Text)
                Exit Sub
            End If

            For i As Integer = 0 To GridRepay.Rows.Count - 1
                ' Access the CheckBox
                Dim controls As TextBox = DirectCast(GridRepay.Rows(i).Cells(2).FindControl("txtAmount"), TextBox)
                Dim cellSalaryItem As String = GridRepay.Rows(i).Cells(1).Text
                Dim cellAmount As Double = controls.Text

                Dim strUser As New DataSet
                strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Finance_Non_Payroll_Employee_Get", Request.QueryString("id"), cellSalaryItem)
                Dim id As Integer = strUser.Tables(0).Rows(0).Item("id").ToString
                olddata(0) = strUser.Tables(0).Rows(0).Item("EmpID").ToString
                olddata(1) = strUser.Tables(0).Rows(0).Item("SalaryItem").ToString
                olddata(2) = strUser.Tables(0).Rows(0).Item("Amount").ToString

                EmpSalary.Employee = lblEmpID.Text
                EmpSalary.Item = cellSalaryItem
                EmpSalary.Amount = cellAmount

                Dim OldValue As String = ""
                Dim NewValue As String = ""

                Dim j As Integer = 0

                For Each a In GetType(clsEmployeeExpense).GetProperties()
                    If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" And a.Name.ToLower <> "employee" Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If IsNumeric(a.GetValue(EmpSalary, Nothing)) = True And IsNumeric(olddata(j)) = True Then
                                If CDbl(a.GetValue(EmpSalary, Nothing)) <> CDbl(olddata(j)) Then
                                    NewValue += a.Name + ": " + a.GetValue(EmpSalary, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            Else
                                If a.GetValue(EmpSalary, Nothing).ToString <> olddata(j).ToString Then
                                    NewValue += a.Name + ": " + a.GetValue(EmpSalary, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            End If
                        End If
                    End If
                    j = j + 1
                Next

                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Finance_Non_Payroll_Employee_Update", id, cellAmount)

                If NewValue.Trim = "" And OldValue.Trim = "" Then
                Else
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Update On " & EmpSalary.Employee & ", " & EmpSalary.Item, Pages)
                End If
            Next
            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Finance_Non_Payroll_Reset_Computed_Items", lblEmpID.Text)
            LoadDetail(Request.QueryString("id"))
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Record(s) saved" + "')", True)
            lblPayStatus.Text = "Record(s) saved"
            LoadPayments(lblEmpID.Text)
        Catch ex As Exception
            lblPayStatus.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnRepay0_Click(sender As Object, e As EventArgs)

    End Sub



    Protected Sub GridRepay_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs)

    End Sub

    Protected Sub btnImport0_Click(sender As Object, e As EventArgs) Handles btnImport0.Click
        'Try
        '    Try
        '        Dim confirmGen As String = Request.Form("confirm_gen")
        '        If confirmGen = "Yes" Then
        '            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Finance_Salary_Primary_Update", lblEmpID.Text, radCurrency.SelectedItem.Text, radSalaryType.SelectedItem.Text)
        '            LoadPayments(lblEmpID.Text)
        '        Else
        '            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Salary Item refresh per Employee cancelled" + "')", True)
        '        End If
        '    Catch ex As Exception
        '        ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        '    End Try
        'Catch ex As Exception

        'End Try
    End Sub

    Protected Sub GridRepay_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridRepay.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim stype As String = e.Row.Cells(3).Text
                
                For Each cell As TableCell In e.Row.Cells
                    If stype.ToLower = "calculated" Or stype.ToLower = "computed" Then
                        e.Row.Cells(2).Enabled = False
                    Else
                        e.Row.Cells(2).Enabled = True
                    End If
                Next
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class