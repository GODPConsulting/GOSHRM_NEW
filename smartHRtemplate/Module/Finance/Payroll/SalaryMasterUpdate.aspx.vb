Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports GOSHRM.GOSHRM.GOSHRM.BO
Public Class SalaryMasterUpdate
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "SALARY"
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
    Dim EmpSalary As New clsEmployeeSalary
    Dim olddata(3) As String
    Dim Pages As String = "Employee Salary Master"
 
  
    Private Sub LoadPayments(ByVal id As Integer)
        Try
            GridRepay.DataSource = Process.SearchData("Finance_Salary_Employee_Get_All", id)
            GridRepay.AllowSorting = False
            GridRepay.AllowPaging = False
            GridRepay.DataBind()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub
    Private Sub LoadDetail(id As String)
        Dim strSalary As New DataSet
        If (id = "0") Then
            strSalary = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Finance_Salary_Primary_Get_1", Request.QueryString("empid"))
        Else
            strSalary = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Finance_Salary_Primary_Get", id)
        End If


        If strSalary.Tables(0).Rows.Count > 0 Then

            aempid.Value = strSalary.Tables(0).Rows(0).Item("empid").ToString
            aempgrade.Value = strSalary.Tables(0).Rows(0).Item("grade").ToString
            aempname.Value = strSalary.Tables(0).Rows(0).Item("name").ToString
            alocation.Value = strSalary.Tables(0).Rows(0).Item("location").ToString
            aemptitle.Value = strSalary.Tables(0).Rows(0).Item("jobtitle").ToString
            aempoffice.Value = strSalary.Tables(0).Rows(0).Item("office").ToString
            lblactive.Text = strSalary.Tables(0).Rows(0).Item("active").ToString
            aempnbt.Value = FormatNumber(CDbl(strSalary.Tables(0).Rows(0).Item("amount").ToString), 2)
            aemptax.Value = "0"
            'aemptax.Value = FormatNumber(CDbl(SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select isnull(sum(isnull(amount,0)),0) from Tax_Calulator('0.00'," + aempid.Value + "'," & CDbl(strSalary.Tables(0).Rows(0).Item("amount").ToString) & ")")), 2)
            'aemptax.Value = FormatNumber(CDbl(SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select isnull(sum(isnull(amount,0)),0) from Tax_Calulator('eee'," & CDbl(strSalary.Tables(0).Rows(0).Item("amount").ToString) & ")")), 2)
            aempnat.Value = FormatNumber(CDbl(aempnbt.Value) - CDbl(aemptax.Value), 2)
            LoadPayments(id)
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                LoadDetail(Request.QueryString("id"))
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub






    Protected Sub OnRowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GridRepay, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub



    Protected Sub btnClose_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect("salarymasterpage", True)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btnRepay_Click(sender As Object, e As EventArgs)
        Try
            Dim lblPayStatus As String = ""
            If CBool(lblactive.Text) = False Then
                lblPayStatus = "Pay Grade is currently inactive, changes have been aborted"
                Process.loadalert(divalert, msgalert, lblPayStatus, "warning")
                LoadPayments(Request.QueryString("id"))
                Exit Sub
            End If

            For i As Integer = 0 To GridRepay.Rows.Count - 1
                ' Access the CheckBox
                Dim controls As TextBox = DirectCast(GridRepay.Rows(i).Cells(2).FindControl("txtAmount"), TextBox)
                Dim cellSalaryItem As String = GridRepay.Rows(i).Cells(1).Text
                Dim cellAmount As Double = controls.Text

                Dim strUser As New DataSet
                strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Finance_Salary_Employee_Get", Request.QueryString("id"), cellSalaryItem)
                Dim id As Integer = strUser.Tables(0).Rows(0).Item("id").ToString
                olddata(0) = strUser.Tables(0).Rows(0).Item("EmpID").ToString
                olddata(1) = strUser.Tables(0).Rows(0).Item("SalaryItem").ToString
                olddata(2) = strUser.Tables(0).Rows(0).Item("Amount").ToString

                EmpSalary.Employee = aempid.Value
                EmpSalary.SalaryItem = cellSalaryItem
                EmpSalary.Amount = cellAmount

                Dim OldValue As String = ""
                Dim NewValue As String = ""

                Dim j As Integer = 0

                For Each a In GetType(clsEmployeeSalary).GetProperties()
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

                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Finance_Salary_Employee_Update", id, cellAmount)

                If NewValue.Trim = "" And OldValue.Trim = "" Then
                Else
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Update On " & EmpSalary.Employee & ", " & EmpSalary.SalaryItem, Pages)
                End If
            Next
            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Finance_Salary_Reset_Computed_Items", aempid.Value)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Record(s) saved" + "')", True)
            lblPayStatus = "Record(s) saved"
            Process.loadalert(divalert, msgalert, lblPayStatus, "success")
            LoadDetail(Request.QueryString("id"))
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnRepay0_Click(sender As Object, e As EventArgs)

    End Sub



    Protected Sub GridRepay_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs)

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

    Protected Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
        Try
            Dim lblstatus As String = ""
            Dim id As String = ""
            Dim confirmGen As String = Request.Form("confirm_ref")
            If confirmGen = "Yes" Then
                id = Process.ResetEmployeeSalary(Request.QueryString("empid"))
                If id <> "0" Then
                    lblstatus = "Salary Items successfully refreshed"
                    Process.loadalert(divalert, msgalert, lblstatus, "success")
                Else
                    Process.loadalert(divalert, msgalert, Session("exception"), "danger")
                End If
                LoadDetail(id)
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
End Class