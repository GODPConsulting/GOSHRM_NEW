Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports GOSHRM.GOSHRM.GOSHRM.BO
Public Class EmployeePayroll
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "PAYROLL"
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
    Dim Pages As String = "Employee Payroll"


    Private Sub LoadEarnings(ByVal id As String)
        Try
            'empID = "EMP001"
            'lblName.Text = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select Name from dbo.Employees_All where empid = '" & empID & "'")
            gridEarning.DataSource = Process.SearchDataP2("Finance_Payslip_Detail_Category_Get", id, "Earning")
            gridEarning.AllowSorting = False
            gridEarning.AllowPaging = False
            gridEarning.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub
    Private Sub LoadDeduction(ByVal id As String)
        Try
            'empID = "EMP001"
            'lblName.Text = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select Name from dbo.Employees_All where empid = '" & empID & "'")
            gridDeduction.DataSource = Process.SearchDataP2("Finance_Payslip_Detail_Category_Get", id, "Deduction")
            gridDeduction.AllowSorting = False
            gridDeduction.AllowPaging = False
            gridDeduction.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                'Request.QueryString("id")
                LoadEarnings(Request.QueryString("id"))
                LoadDeduction(Request.QueryString("id"))
                Dim strSalary As New DataSet
                strSalary = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Finance_Payslip_Get", Request.QueryString("id"))
                If strSalary.Tables(0).Rows.Count > 0 Then
                    lblName.Text = strSalary.Tables(0).Rows(0).Item("name").ToString & " (" & strSalary.Tables(0).Rows(0).Item("empid").ToString & ")"
                    lblLocation.Text = strSalary.Tables(0).Rows(0).Item("location").ToString
                    lbljobtitle.Text = strSalary.Tables(0).Rows(0).Item("jobtitle").ToString
                    lblOffice.Text = strSalary.Tables(0).Rows(0).Item("office").ToString
                    'lblEmpID.Text = strSalary.Tables(0).Rows(0).Item("empid").ToString
                    lblGrade.Text = strSalary.Tables(0).Rows(0).Item("grade").ToString
                    'lblBank.Text = strSalary.Tables(0).Rows(0).Item("bank").ToString
                    'lblAccount.Text = strSalary.Tables(0).Rows(0).Item("accountnumber").ToString
                    lbldays.Text = strSalary.Tables(0).Rows(0).Item("WorkingDays").ToString
                    lblactualdays.Text = strSalary.Tables(0).Rows(0).Item("ActualWorkingdays").ToString
                    lbldate.Text = Process.DDMONYYYY(CDate(strSalary.Tables(0).Rows(0).Item("startdate"))) & " : " & Process.DDMONYYYY(CDate(strSalary.Tables(0).Rows(0).Item("enddate")))
                End If

            End If
        Catch ex As Exception
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub






    Protected Sub OnRowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gridEarning, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub





    Protected Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Try
            Response.Write("<script language='javascript'> { self.close() }</script>")
        Catch ex As Exception
        End Try
    End Sub

   

    Protected Sub btnRepay0_Click(sender As Object, e As EventArgs)

    End Sub



    Protected Sub GridRepay_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs)

    End Sub

  
End Class