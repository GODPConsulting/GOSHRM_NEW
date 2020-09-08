Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports GOSHRM.GOSHRM.GOSHRM.BO
Public Class EmployeeTerminalBenefit
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "TERMINALPAY"
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
    Dim Pages As String = "Employee Terminal Benefits"


    Private Sub LoadPayments(ByVal id As Integer)
        Try
            Dim strSalary As New DataSet
            strSalary = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Finance_Payslip_Terminal_Get", lblEmpID.Text)
            If strSalary.Tables(0).Rows.Count > 0 Then
                lblName.Text = strSalary.Tables(0).Rows(0).Item("name").ToString
                lblLocation.Text = strSalary.Tables(0).Rows(0).Item("location").ToString
                lbljobtitle.Text = strSalary.Tables(0).Rows(0).Item("jobtitle").ToString
                lblOffice.Text = strSalary.Tables(0).Rows(0).Item("office").ToString
                lblGrade.Text = strSalary.Tables(0).Rows(0).Item("Grade").ToString
                lblid.Text = strSalary.Tables(0).Rows(0).Item("id").ToString
                datExitDate.SelectedDate = strSalary.Tables(0).Rows(0).Item("EndDate")
                chkLeaveAllowance.Checked = CBool(strSalary.Tables(0).Rows(0).Item("ApplyLeaveAllowance"))
                chkLeaveDays.Checked = CBool(strSalary.Tables(0).Rows(0).Item("ApplyLeaveDays"))
                chkLoan.Checked = CBool(strSalary.Tables(0).Rows(0).Item("ApplyLoan"))
                chkNetPay.Checked = CBool(strSalary.Tables(0).Rows(0).Item("ApplyNetPay"))
                chkGratuity.Checked = CBool(strSalary.Tables(0).Rows(0).Item("ApplyGratuity"))
                lblnet.Text = FormatNumber(CDbl(strSalary.Tables(0).Rows(0).Item("TotalPay")), 2)
                lblearning.Text = FormatNumber(CDbl(strSalary.Tables(0).Rows(0).Item("TotalEarning")), 2)
                lbldeduction.Text = FormatNumber(CDbl(strSalary.Tables(0).Rows(0).Item("TotalDeduction")), 2)

            End If

            GridRepay.DataSource = Process.SearchData("Finance_Payslip_Terminal_Detail_Get", lblid.Text)
            GridRepay.AllowSorting = False
            GridRepay.AllowPaging = False
            GridRepay.DataBind()
        Catch ex As Exception
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
           
            If Not Me.IsPostBack Then
                lblid.Text = "0"
                lbldeduction.Text = FormatNumber(0, 2)
                lblearning.Text = FormatNumber(0, 2)
                lblnet.Text = FormatNumber(0, 2)

                lblEmpID.Text = Request.QueryString("empid")

                Dim strEmp As New DataSet
                strEmp = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "select name,location ,JobTitle , Office ,Grade  from dbo.Employees_All a where a.empid = '" & lblEmpID.Text & "'")
                If strEmp.Tables(0).Rows.Count > 0 Then
                    lblName.Text = strEmp.Tables(0).Rows(0).Item("name").ToString
                    lblLocation.Text = strEmp.Tables(0).Rows(0).Item("location").ToString
                    lbljobtitle.Text = strEmp.Tables(0).Rows(0).Item("jobtitle").ToString
                    lblOffice.Text = strEmp.Tables(0).Rows(0).Item("office").ToString
                    lblGrade.Text = strEmp.Tables(0).Rows(0).Item("Grade").ToString
                End If

                Dim strTermination As New DataSet
                strTermination = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "select TerminationDate from Emp_Termination where Empid = '" & lblEmpID.Text & "' and supervisorapproval = 'Approved'")
                If strTermination.Tables(0).Rows.Count > 0 Then
                    datExitDate.SelectedDate = strTermination.Tables(0).Rows(0).Item("TerminationDate")                    
                End If


                

            End If

            LoadPayments(lblid.Text)
            If GridRepay.Rows.Count <= 0 Then
                btnRepay.Enabled = False
                btnRepay.BackColor = Color.Gray
                lnkPrint.Visible = False
            Else
                btnRepay.Enabled = True
                btnRepay.BackColor = Color.FromArgb(102, 153, 0)
                lnkPrint.Visible = True
            End If

        Catch ex As Exception
            lblPayStatus.Text = ex.Message
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
            Response.Redirect("~/Module/Finance/Payroll/TerminalBenefits.aspx")
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btnRepay_Click(sender As Object, e As EventArgs) Handles btnRepay.Click
        Try
            'If lblactive.Text = 0 Then
            '    ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Pay Grade is currently inactive, changes have been aborted" + "')", True)
            '    lblPayStatus.Text = "Pay Grade is currently inactive, changes have been aborted"
            '    LoadPayments(lblid.Text)
            '    Exit Sub
            'End If
            System.Threading.Thread.Sleep(300)
            For i As Integer = 0 To GridRepay.Rows.Count - 1
                ' Access the CheckBox
                Dim controls As TextBox = DirectCast(GridRepay.Rows(i).Cells(2).FindControl("txtAmount"), TextBox)
                Dim cellSalaryItem As String = GridRepay.Rows(i).Cells(1).Text
                Dim cellAmount As Double = controls.Text

                Dim strUser As New DataSet
                strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Finance_Payslip_Terminal_Detail_Get_Detail", lblEmpID.Text, cellSalaryItem)
                Dim id As Integer = strUser.Tables(0).Rows(0).Item("id").ToString

                
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Finance_Payslip_Terminal_Detail_Update", id, cellAmount)
            Next
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Record(s) saved" + "')", True)
            lblPayStatus.Text = "Record(s) saved"
            LoadPayments(lblid.Text)
        Catch ex As Exception
            lblPayStatus.Text = ex.Message
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & lblPayStatus.Text + "')", True)
        End Try
    End Sub

    Protected Sub btnRepay0_Click(sender As Object, e As EventArgs)

    End Sub



    Protected Sub GridRepay_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs)

    End Sub

    Protected Sub btnImport0_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            'If datExitDate.SelectedDate IsNot Nothing Then
            '    Dim confirmGen As String = Request.Form("confirm_gen")
            '    If confirmGen = "Yes" Then
            '        System.Threading.Thread.Sleep(300000)
            '        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Finance_Generate_Single_Terminal_PaySlip", lblEmpID.Text, Process.FirstDay(datExitDate.SelectedDate.Value.Year, datExitDate.SelectedDate.Value.Month), datExitDate.SelectedDate, Process.FirstDay(datExitDate.SelectedDate.Value.Year, datExitDate.SelectedDate.Value.Month), Process.LastDay(datExitDate.SelectedDate.Value.Year, datExitDate.SelectedDate.Value.Month), chkNetPay.Checked, chkLoan.Checked, chkLeaveAllowance.Checked, chkLeaveDays.Checked, chkGratuity.Checked)
            '        LoadPayments(lblid.Text)
            '    Else
            '        ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Salary Item refresh per Employee cancelled" + "')", True)
            '    End If
            'Else
            '    lblPayStatus.Text = "No Exit Date stated, confirm Exit is approved by Supervisor before generating benefits!"
            '    ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & lblPayStatus.Text + "')", True)
            'End If
            If datExitDate.SelectedDate IsNot Nothing Then
                Dim confirmGen As String = Request.Form("confirm_gen")
                If confirmGen = "Yes" Then
                    Dim strCompletionStatus As New DataSet
                    Using conn2 As New SqlConnection(WebConfig.ConnectionString)
                        conn2.Open()
                        Dim comm2 As New SqlCommand("Finance_Generate_Single_Terminal_PaySlip", conn2)
                        comm2.CommandType = CommandType.StoredProcedure
                        comm2.Parameters.AddWithValue("@employee", lblEmpID.Text)
                        comm2.Parameters.AddWithValue("@startdate", Process.FirstDay(datExitDate.SelectedDate.Value.Year, datExitDate.SelectedDate.Value.Month))
                        comm2.Parameters.AddWithValue("@enddate", datExitDate.SelectedDate)
                        comm2.Parameters.AddWithValue("@FirstDate", Process.FirstDay(datExitDate.SelectedDate.Value.Year, datExitDate.SelectedDate.Value.Month))
                        comm2.Parameters.AddWithValue("@LastDate", Process.LastDay(datExitDate.SelectedDate.Value.Year, datExitDate.SelectedDate.Value.Month))
                        comm2.Parameters.AddWithValue("@ApplyNetPay", chkNetPay.Checked)
                        comm2.Parameters.AddWithValue("@ApplyLoan", chkLoan.Checked)
                        comm2.Parameters.AddWithValue("@ApplyLeaveAllowance", chkLeaveAllowance.Checked)
                        comm2.Parameters.AddWithValue("@ApplyLeaveDays", chkLeaveDays.Checked)
                        comm2.Parameters.AddWithValue("@ApplyGratuity", chkGratuity.Checked)
                        comm2.CommandTimeout = 157200
                        Dim obj As Object = comm2.ExecuteScalar()
                        'Dim sdat2 As New SqlDataAdapter(comm2)
                        'sdat2.Fill(strCompletionStatus)
                        conn2.Close()
                    End Using
                    LoadPayments(lblid.Text)
                Else
                    ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Salary Item refresh per Employee cancelled" + "')", True)
            End If
            Else
            lblPayStatus.Text = "No Exit Date stated, confirm Exit is approved by Supervisor before generating benefits!"
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & lblPayStatus.Text + "')", True)
            End If

        Catch ex As Exception
            lblPayStatus.Text = ex.Message
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & lblPayStatus.Text + "')", True)
        End Try
    End Sub

    Protected Sub lnkPrint_Click(sender As Object, e As EventArgs) Handles lnkPrint.Click
        Try
            Dim url As String = "ExitBenefits.aspx?empid=" & lblEmpID.Text & "&payslipid=" & lblid.Text
            Dim s As String = "window.open('" & url + "', 'popup_window', 'width=1000,height=1000,status=yes,resizable=yes,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no');"
            ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)
        Catch ex As Exception
            lblPayStatus.Text = ex.Message
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & lblPayStatus.Text + "')", True)
        End Try
    End Sub
End Class