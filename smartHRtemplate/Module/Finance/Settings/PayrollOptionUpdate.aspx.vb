Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Telerik.Web.UI
Imports System.IO

Public Class PayrollOptionUpdate
    Inherits System.Web.UI.Page
    Dim payrolloption As New clsPayrollOption
    Dim AuthenCode As String = "PAYROLLOPT"
    Dim olddata(10) As String
    Dim oFileAttached1 As Byte
    Dim oFileAttached As Byte()
    Dim lblstatus As String = ""
    Dim Pages As String = "Training Course"


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                drpAdjustment.Items.Clear()
                drpAdjustment.Items.Add("New Recruit get full pay regardless of date join in Salary Month")
                drpAdjustment.Items.Add("Divide by number of remaining days after date join of Salary Month")

                Dim ismulti As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select isnull(ismulticompany,'No')  from general_info")
                radovertimetaxable.Items.Clear()
                radovertimetaxable.Items.Add("Yes")
                radovertimetaxable.Items.Add("No")


                Process.LoadRadDropDownTextAndValue(drpCurrency, "Currency_Load_1", "Currency", "Code", False)

                If Request.QueryString("id") IsNot Nothing Then
                    If ismulti.ToLower = "no" Then
                        Process.LoadRadComboTextAndValueP2(cboCompany, "Company_Structure_Get_ByLevel", "1", Session("Access"), "name", "name", False)
                    Else
                        Process.LoadRadComboTextAndValueP2(cboCompany, "Company_Structure_Get_ByLevel", "2", Session("Access"), "name", "name", False)
                    End If

                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Payroll_Options_Get", Request.QueryString("id"))
                    If strUser.Tables(0).Rows.Count > 0 Then
                        lblid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                        Process.AssignRadComboValue(cboCompany, strUser.Tables(0).Rows(0).Item("company").ToString)
                        Process.LoadRadComboTextAndValueP1(cboApprove, "Emp_PersonalDetail_Get_Approvers", cboCompany.SelectedValue, "Employee2", "EmpID", False)
                        Process.LoadListAndComboxFromDataset(lstApprover, cboApprove, "Payroll_Option_Approver_Get", "employee2", "empid", lblid.Text)
                        Process.AssignRadDropDownValue(drpAdjustment, strUser.Tables(0).Rows(0).Item("perdaysalaryadjustment").ToString)
                        Process.AssignRadDropDownValue(drpCurrency, strUser.Tables(0).Rows(0).Item("payslipCurrency").ToString)
                        Process.AssignRadDropDownValue(radAutoEmailSlips, strUser.Tables(0).Rows(0).Item("autoemailpayslip").ToString)
                        Process.RadioListCheck(rdoAutoApprove, strUser.Tables(0).Rows(0).Item("autoapprove").ToString)
                        Process.AssignRadDropDownValue(radPayOnAttendance, strUser.Tables(0).Rows(0).Item("SalaryBasedOnAttendance").ToString)
                        Process.AssignRadDropDownValue(radPayOverTime, strUser.Tables(0).Rows(0).Item("PayOvertime").ToString)
                        Process.AssignRadDropDownValue(radovertimetaxable, strUser.Tables(0).Rows(0).Item("OvertimeTaxable").ToString)
                        lblauto.Text = strUser.Tables(0).Rows(0).Item("autoapprove").ToString
                        lblemail.Text = strUser.Tables(0).Rows(0).Item("autoemailpayslip").ToString
                        lblovertimeenabled.Text = strUser.Tables(0).Rows(0).Item("PayOvertime").ToString
                        lblattendance.Text = strUser.Tables(0).Rows(0).Item("SalaryBasedOnAttendance").ToString


                        txtAmount.Value = FormatNumber(strUser.Tables(0).Rows(0).Item("minamount").ToString, 2)
                        If lblauto.Text = "No" Then
                            cboApprove.Enabled = True
                        Else
                            cboApprove.Enabled = False
                        End If

                        If radPayOverTime.SelectedValue.ToUpper = "YES" Then

                        Else

                        End If
                        cboCompany.Enabled = False
                    End If
                    LoadGrid(lblid.Text)
                    PanelVisibility()
                Else

                    If ismulti.ToLower = "no" Then
                        Process.LoadRadComboTextAndValueP2(cboCompany, "Company_Structure_Get_ByLevel_Payroll", "1", Session("Access"), "name", "name", False)
                    Else
                        Process.LoadRadComboTextAndValueP2(cboCompany, "Company_Structure_Get_ByLevel_Payroll", "2", Session("Access"), "name", "name", False)
                    End If
                    Process.AssignRadComboValue(cboCompany, Session("Organisation"))
                    Process.LoadRadComboTextAndValueP1(cboApprove, "Emp_PersonalDetail_Get_Approvers", cboCompany.SelectedValue, "Employee2", "EmpID", False)
                    lblid.Text = "0"

                    txtAmount.Value = "0"

                    cboApprove.Enabled = False
                    PanelVisibility()
                    Process.AssignRadDropDownValue(radPayOverTime, "No")
                    If radPayOverTime.SelectedValue.ToUpper = "YES" Then
                        radovertimetaxable.Visible = True
                        overtimetaxablelabel.Visible = True
                    Else
                        radovertimetaxable.Visible = False
                        overtimetaxablelabel.Visible = False
                    End If
                End If

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub



    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            If lblid.Text = "0" Then
                If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                    Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "danger")
                    Exit Sub
                End If
            Else
                If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False Then
                    Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "danger")
                    Exit Sub
                End If
            End If




            If txtAmount.Value.Trim Is Nothing Then
                txtAmount.Value = "0"
            End If

            If IsNumeric(txtAmount.Value) = False Then
                lblstatus = "Minimum Amount Adjustment for Approval required!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                txtAmount.Focus()
                Exit Sub
            End If

            If drpAdjustment.SelectedText Is Nothing Then
                lblstatus = "Salary Adjustment on New Recruits required!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                drpAdjustment.Focus()
                Exit Sub
            End If

            If drpCurrency.SelectedText Is Nothing Then
                lblstatus = "Currency required!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                drpCurrency.Focus()
                Exit Sub
            End If

            'Old Data
            payrolloption.Company = cboCompany.SelectedValue
            payrolloption.AutoApprovePayslip = lblauto.Text  'rdoAutoApprove.SelectedValue
            payrolloption.AutoMailPayslip = radAutoEmailSlips.SelectedValue    ' rdoEmail.SelectedValue
            payrolloption.Currency = drpCurrency.SelectedItem.Value
            payrolloption.PerDaySalaryAdjustment = drpAdjustment.SelectedText
            payrolloption.PayslipApprovers = cboApprove.CheckedItems.Count
            payrolloption.MinAmountForApproval = txtAmount.Value
            payrolloption.SalaryBasedOnAttendance = radPayOnAttendance.SelectedValue
            payrolloption.PayOvertime = radPayOverTime.SelectedValue
            payrolloption.OvertimeTaxable = radovertimetaxable.SelectedText


            Dim strUser As New DataSet
            strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Payroll_Options_Get", lblid.Text)
            Dim ccount As Integer = strUser.Tables(0).Rows.Count
            If strUser.Tables(0).Rows.Count > 0 Then
                olddata(0) = strUser.Tables(0).Rows(0).Item("company").ToString
                olddata(1) = strUser.Tables(0).Rows(0).Item("autoapprove").ToString
                olddata(2) = strUser.Tables(0).Rows(0).Item("autoemailpayslip").ToString
                olddata(3) = strUser.Tables(0).Rows(0).Item("perdaysalaryadjustment").ToString
                olddata(4) = strUser.Tables(0).Rows(0).Item("currency").ToString 'approvers 
                olddata(5) = strUser.Tables(0).Rows(0).Item("approvers").ToString
                olddata(6) = strUser.Tables(0).Rows(0).Item("minAmount").ToString
                olddata(7) = strUser.Tables(0).Rows(0).Item("SalaryBasedOnAttendance").ToString
                olddata(8) = strUser.Tables(0).Rows(0).Item("PayOvertime").ToString
                olddata(9) = strUser.Tables(0).Rows(0).Item("overtimeindex").ToString
                olddata(10) = strUser.Tables(0).Rows(0).Item("OvertimeTaxable").ToString
            End If

            Dim NewValue As String = ""
            Dim OldValue As String = ""

            Dim j As Integer = 0

            If olddata(0) IsNot Nothing Then
                For Each a In GetType(clsPayrollOption).GetProperties()
                    If a.Name.ToLower <> "id" And a.Name.ToLower.Contains("password") = False Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If IsNumeric(a.GetValue(payrolloption, Nothing)) = True And IsNumeric(olddata(j)) = True Then
                                If CDbl(a.GetValue(payrolloption, Nothing)) <> CDbl(olddata(j)) Then
                                    NewValue += a.Name + ": " + a.GetValue(payrolloption, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            Else
                                If a.GetValue(payrolloption, Nothing).ToString <> olddata(j).ToString Then
                                    NewValue += a.Name + ": " + a.GetValue(payrolloption, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            End If
                        End If
                    End If
                    j = j + 1
                Next
            Else
                For Each a In GetType(clsPayrollOption).GetProperties() 'New Entries
                    If a.Name.ToLower <> "id" And a.Name.ToLower.Contains("password") Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If a.GetValue(payrolloption, Nothing) = Nothing Then
                                NewValue += a.Name + ":" + " " & vbCrLf
                            Else
                                NewValue += a.Name + ": " + a.GetValue(payrolloption, Nothing).ToString & vbCrLf
                            End If
                        End If
                    End If
                Next
            End If

            If lblid.Text = "0" Then
                lblid.Text = GetIdentity(lblid.Text, payrolloption.Company, payrolloption.AutoApprovePayslip, payrolloption.AutoMailPayslip, payrolloption.PerDaySalaryAdjustment, payrolloption.Currency, payrolloption.MinAmountForApproval, payrolloption.SalaryBasedOnAttendance, payrolloption.PayOvertime, Session("LoginID"), payrolloption.OvertimeTaxable)
                If lblid.Text = "0" Then
                    Exit Sub
                End If
            Else
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Payroll_Options_Update", lblid.Text, payrolloption.Company, payrolloption.AutoApprovePayslip, payrolloption.AutoMailPayslip, payrolloption.PerDaySalaryAdjustment, payrolloption.Currency, payrolloption.MinAmountForApproval, payrolloption.SalaryBasedOnAttendance, payrolloption.PayOvertime, Session("LoginID"), payrolloption.OvertimeTaxable)
            End If


            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Payroll_Option_Approver_Update_State", lblid.Text)
            If payrolloption.AutoApprovePayslip = "No" Then
                If cboApprove.CheckedItems.Count > 0 Then
                    Dim collection As IList(Of RadComboBoxItem) = cboApprove.CheckedItems
                    If (collection.Count <> 0) Then
                        For Each item As RadComboBoxItem In collection
                            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Payroll_Option_Approver_Update", lblid.Text, item.Value, 0)
                        Next
                    End If
                End If
            End If
            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Payroll_Option_Approver_Delete", lblid.Text)
            If NewValue.Trim = "" And OldValue.Trim = "" Then
            Else
                If olddata(0) IsNot Nothing Then
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Updated", "Payroll Option")
                Else
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Inserted", "Payroll Option")
                End If
            End If
            cboCompany.Enabled = False
            LoadGrid(lblid.Text)
            PanelVisibility()

            Process.LoadListBoxFromCombo(lstApprover, cboApprove)
            lblstatus = "Record saved"
            Process.loadalert(divalert, msgalert, lblstatus, "success")

            'Response.Redirect("~/Module/Finance/Settings/PayrollOption.aspx", False)

        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "danger")
        End Try
    End Sub
    Private Function GetIdentity(id As String, company As String, autoapprove As String, autoemailpayslip As String, perdaysalaryadjustment As String, currency As String, amount As Double, salaryonatendance As String, payovertime As String, userid As String, overtimetax As String) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Payroll_Options_Update"
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id
            cmd.Parameters.Add("@company", SqlDbType.VarChar).Value = company
            cmd.Parameters.Add("@autoapprove", SqlDbType.VarChar).Value = autoapprove
            cmd.Parameters.Add("@autoemailpayslip", SqlDbType.VarChar).Value = autoemailpayslip
            cmd.Parameters.Add("@perdaysalaryadjustment", SqlDbType.VarChar).Value = perdaysalaryadjustment
            cmd.Parameters.Add("@currency", SqlDbType.VarChar).Value = currency
            cmd.Parameters.Add("@amount", SqlDbType.Decimal).Value = amount
            cmd.Parameters.Add("@salaryonatendance", SqlDbType.VarChar).Value = salaryonatendance
            cmd.Parameters.Add("@payovertime", SqlDbType.VarChar).Value = payovertime
            cmd.Parameters.Add("@OvertimeTaxable", SqlDbType.VarChar).Value = overtimetax

            cmd.Parameters.Add("@userid", SqlDbType.VarChar).Value = userid
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "danger")
            Return 0
        End Try
    End Function

    Protected Sub rdoAutoApprove_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rdoAutoApprove.SelectedIndexChanged
        Try
            If rdoAutoApprove.SelectedValue = "No" Then
                cboApprove.Enabled = True
            Else
                cboApprove.Enabled = False
            End If
            lblauto.Text = rdoAutoApprove.SelectedValue
        Catch ex As Exception

        End Try
    End Sub


    'Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
    '    Process.LoadListBoxFromCombo(lstApprover, cboApprove)
    'End Sub




    Protected Sub radPayOverTime_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.DropDownListEventArgs) Handles radPayOverTime.SelectedIndexChanged
        If radPayOverTime.SelectedValue.ToUpper = "YES" Then
            radovertimetaxable.Visible = True
            overtimetaxablelabel.Visible = True
        Else
            radovertimetaxable.Visible = False
            overtimetaxablelabel.Visible = False
        End If
    End Sub

    Private Sub cboCompany_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboCompany.SelectedIndexChanged
        Try
            Process.LoadRadComboTextAndValueP1(cboApprove, "Emp_PersonalDetail_Get_Approvers", cboCompany.SelectedValue, "Employee2", "EmpID", False)
            Process.LoadListBoxFromCombo(lstApprover, cboApprove)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect("PayrollOption.aspx", True)
        Catch ex As Exception

        End Try
    End Sub






    Private Function LoadDatatable() As DataTable
        Dim dt As New DataTable
        search.Value = Session("courseskillLoadsearch")
        If search.Value.Trim = "" Then
            dt = Process.SearchData("Overtime_Category_get_all", lblid.Text)
        Else
            dt = Process.SearchDataP2("Overtime_Categor_get_search", lblid.Text, search.Value)
        End If
        Return dt
    End Function
    Protected Sub Delete(sender As Object, e As EventArgs) Handles btDelete.Click
        Try
            Process.loadalert(divalert, msgalert, "", "warning")
            If Process.AuthenAction(Session("role"), AuthenCode, "Delete") = False Then
                Process.loadalert(divalert, msgalert, Process.privilegemsg, "warning")

                Exit Sub
            End If
            Dim count As Integer = 0
            Dim confirmValue As String = Request.Form("confirm_value")
            'If confirmValue = "Yes" Then
            Dim atLeastOneRowDeleted As Boolean = False
            ' Iterate through the Products.Rows property
            For Each row As GridViewRow In gridskills.Rows
                ' Access the CheckBox
                Dim cb As CheckBox = row.FindControl("chkEmp")
                If cb IsNot Nothing AndAlso cb.Checked Then
                    count = count + 1
                    ' Delete row! (Well, not really...)
                    atLeastOneRowDeleted = True
                    ' First, get the ProductID for the selected row
                    Dim ID As String =
                            Convert.ToString(gridskills.DataKeys(row.RowIndex).Value)
                    ' "Delete" the row
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Overtime_Pay_Delete", ID)
                End If
            Next
            Process.loadalert(divalert, msgalert, count.ToString & " records successfully deleted", "success")

            LoadGrid(txtid.Text)

            'End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Sub LoadGrid(id As String)
        Try
            gridskills.PageIndex = CInt(Session("courseskillLoadindex"))
            gridskills.DataSource = LoadDatatable()
            gridskills.AllowSorting = True
            gridskills.DataBind()
            PanelVisibility()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub


    Private Sub PanelVisibility()
        Try
            If radPayOverTime.SelectedValue = "Yes" Then
                pnskill.Visible = True
            Else
                pnskill.Visible = False
            End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub DrillDown(ByVal sender As Object, ByVal e As EventArgs)
        Try
            txtskillid.Text = CType(sender, LinkButton).CommandArgument
            Dim url As String = "OvertimePayment?id=" & txtskillid.Text
            Response.Redirect(url, True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub SortRecords(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Try
            Dim sortExpression As String = e.SortExpression
            Session("courseskillsortExpression") = sortExpression
            Dim direction As String = String.Empty
            If SortsDirection = SortDirection.Ascending Then
                SortsDirection = SortDirection.Descending
                direction = " DESC"
            Else
                SortsDirection = SortDirection.Ascending
                direction = " ASC"
            End If
            gridskills.PageIndex = CInt(Session("courseskillLoadindex"))
            Dim table As DataTable = LoadDatatable()
            table.DefaultView.Sort = sortExpression & direction
            gridskills.DataSource = table
            gridskills.DataBind()
        Catch ex As Exception

        End Try
    End Sub
    Public Property SortsDirection() As SortDirection
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
    Protected Sub btnUpload_Click(sender As Object, e As EventArgs)
        Try

            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                Process.loadalert(divalert, msgalert, Process.privilegemsg, "warning")

                Exit Sub
            End If

            If Not file1.PostedFile Is Nothing Then
                'To create a PostedFile
                Dim csvPath As String = Server.MapPath(Process.FileURL) + Path.GetFileName(file1.PostedFile.FileName)
                If File.Exists(csvPath) = True Then
                    File.Delete(csvPath)
                End If
                file1.PostedFile.SaveAs(csvPath)
                'Create byte Array with file len
                'File.ContentLength
                If Process.Import(csvPath, "Overtime_JobGrade_upload", Pages) = True Then
                    Process.loadalert(divalert, msgalert, "Uploaded " & Session("uploadcnt") & " record(s)", "success")
                    Process.GetAuditTrailInsertandUpdate("", "Uploaded " & Session("uploadcnt") & " record(s) from " & csvPath, "File Upload", Pages)
                    LoadGrid(lblid.Text)
                Else
                    Process.loadalert(divalert, msgalert, Session("exception"), "danger")
                End If
                If File.Exists(csvPath) = True Then
                    File.Delete(csvPath)
                End If
            Else
                Process.loadalert(divalert, msgalert, "No files selected to upload", "warning")
                file1.Focus()
                Exit Sub
            End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub
    Protected Sub btnExport_Click(sender As Object, e As EventArgs)
        Try
            Dim dt As DataTable = Process.SearchData("Overtime_Download", lblid.Text)
            If Process.ExportExcel(dt, "CourseSkills") = False Then
                Response.Output.Write(Process.strExp)
            End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub
    Protected Sub btnAddSkill_Click(sender As Object, e As EventArgs)
        Try
            Dim url As String = "OverTimePayment?courseid=" & lblid.Text
            Response.Redirect(url, True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnFind_Click(sender As Object, e As EventArgs)
        Try
            'If Not Me.IsPostBack Then
            Session("courseskillLoadsearch") = search.Value.Trim
            LoadGrid(txtid.Text)
            'End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub
    Protected Sub Add_Details(sender As Object, e As EventArgs)

        Dim SalaryComponent = Request.Form("grateful").ToString()
        Dim DaysApplied = Request.Form("grateful1")
        If SalaryComponent Is Nothing Then
            lblstatus = "No Salary Component Selected"
            Process.loadalert(divalert, msgalert, lblstatus, "danger")
        End If
        If DaysApplied Is Nothing Then
            lblstatus = "No day is selected"
            Process.loadalert(divalert, msgalert, lblstatus, "danger")
        End If
        Dim str = Guid.NewGuid.ToString()
        Dim SalaryComponents As Array = SalaryComponent.Split(",")
        Dim DaysApplies As Array = DaysApplied.Split(",")

        For d As Integer = 0 To DaysApplies.Length - 1
            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Overtime_days_Update", 0, lblid.Text, Request.Form("performanceid"), DaysApplies(d), str)

        Next
        For d As Integer = 0 To SalaryComponents.Length - 1
            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Overtime_Components_Update", 0, lblid.Text, Request.Form("performanceid"), SalaryComponents(d), str)

        Next


    End Sub
    Protected Sub OnRowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gridskills, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub
End Class