Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Telerik.Web.UI
Imports Microsoft.Reporting.WebForms
Imports System.IO
Public Class Payroll
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "PAYROLL"
    Dim Approvalstatic As String = ""
    Dim emailFile As String = ConfigurationManager.AppSettings("EmailURL")
    Dim PayrollFile As String = ""
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
    Private Function DataData() As DataTable

        Dim strDataSet As New DataTable
        search.Value = Session("payrollsearch")
        If search.Value.Trim = "" Then
            strDataSet = Process.SearchDataP2("Finance_Payslip_Get_All", txtid.Text, Session("company"))
        Else
            strDataSet = Process.SearchDataP3("Finance_Payslip_Search", txtid.Text, Session("company"), search.Value.Trim)
        End If
        pagetitle.InnerText = Session("company") & ": " & " Payroll " & GetPeriod() & " and Total Net Pay: " & NetPay()

        Dim strApp As New DataSet
        strApp = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Finance_Payslip_Primary_Get", txtid.Text)
        If strApp.Tables(0).Rows.Count > 0 Then
            lbapproval.InnerText = strApp.Tables(0).Rows(0).Item("approvalstatus").ToString
        End If

        If lbapproval.InnerText.ToLower.Contains("approved") Then
            divapproval.Attributes.Remove("class")
            divapproval.Attributes.Add("class", "w3-panel w3-pale-green w3-bottombar w3-border-green w3-border")
        ElseIf lbapproval.InnerText.ToLower.Contains("reject") Then
            divapproval.Attributes.Add("class", "w3-panel w3-pale-red w3-bottombar w3-border-red w3-border")
        Else
            divapproval.Attributes.Add("class", "w3-panel w3-pale-yellow w3-bottombar w3-border-yellow w3-border")
        End If

        Return strDataSet
    End Function


    Private Sub LoadLoans()
        Try
            'SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, script)

            dtTable = DataData()
            GridVwHeaderChckbox.PageIndex = CInt(Session("payrollindex"))
            GridVwHeaderChckbox.DataSource = dtTable
            GridVwHeaderChckbox.AllowSorting = True
            GridVwHeaderChckbox.AllowPaging = True
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.message, "danger")

        End Try
    End Sub
    Private Function GetPeriod() As String
        Try
            Dim outcome As String = ""
            Dim strDataSet As New DataSet
            strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Finance_Payslip_Primary_Get", txtid.Text)
            If strDataSet.Tables(0).Rows.Count > 0 Then
                Dim rundate As Date = CDate(strDataSet.Tables(0).Rows(0).Item("Startdate"))
                Session("PayrollYear") = rundate.Year.ToString
                outcome = MonthName(rundate.Month, True) & ", " & rundate.Year
            End If
            Return outcome
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.message, "danger")
            Return ""
        End Try
    End Function
    Private Function NetPay() As String
        Try
            Dim outcome As String = "0"
            outcome = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Finance_Payslip_Get_All_NetPay", txtid.Text, Session("company"))
            Return outcome
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.message, "danger")
            Return "0"
        End Try
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            'Session("View") = "Loans"

            If Not Me.IsPostBack Then

                If Session("processid") IsNot Nothing Then
                    If Session("processid") <> 0 Then
                        txtid.Text = Session("processid")
                    Else
                        If Request.QueryString("id").ToString IsNot Nothing Then
                            txtid.Text = Request.QueryString("id").ToString
                        End If
                    End If

                Else
                    txtid.Text = Request.QueryString("id").ToString
                End If

                If Request.UrlReferrer.ToString().ToLower.Contains("payrollperiod") = True Then
                    Session("PreviousPage") = Request.UrlReferrer.ToString()
                End If

                Dim strcompany As New DataSet
                strcompany = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Finance_Payslip_Primary_get", txtid.Text)
                If strcompany.Tables(0).Rows.Count > 0 Then
                    Session("company") = strcompany.Tables(0).Rows(0).Item("company").ToString
                End If

                If Session("payrollsearch") Is Nothing Then
                    Session("payrollsearch") = ""
                End If

                If Session("payrollindex") Is Nothing Then
                    Session("payrollindex") = "0"
                End If

                LoadLoans()

            End If
           


        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.message, "danger")

        End Try
    End Sub

    Protected Sub SortRecords(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Try
            Dim table As New DataTable
            'SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, script)
            table = DataData()

            Dim sortExpression As String = e.SortExpression
            Session("payrollsort") = sortExpression
            Dim direction As String = String.Empty
            If SortsDirection = SortDirection.Ascending Then
                SortsDirection = SortDirection.Descending
                direction = " DESC"
            Else
                SortsDirection = SortDirection.Ascending
                direction = " ASC"
            End If
            GridVwHeaderChckbox.PageIndex = CInt(Session("payrollindex"))
            table.DefaultView.Sort = sortExpression & direction
            GridVwHeaderChckbox.DataSource = table
            GridVwHeaderChckbox.DataBind()
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


    Protected Sub GridVwHeaderChckbox_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridVwHeaderChckbox.PageIndexChanging
        Try
            Session("payrollindex") = e.NewPageIndex
            LoadLoans
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.message, "danger")
        End Try
    End Sub

    Protected Sub OnRowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GridVwHeaderChckbox, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub


    Protected Sub btnFind_Click(sender As Object, e As EventArgs)
        Try
            lbapproval.InnerText = "Payroll Approval Status: " & Approvalstatic
            Session("payrollsearch") = search.Value.Trim
            LoadLoans()
            'End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Protected Sub Delete(sender As Object, e As EventArgs) Handles btDelete.Click
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Delete") = False Then
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
                Exit Sub
            End If
            Dim count As Integer = 0
            Dim confirmValue As String = Request.Form("confirm_value")
            If confirmValue = "Yes" Then
                If lbapproval.InnerText.ToLower.Contains("approved") = False Then
                    Process.loadalert(divalert, msgalert, "Payroll is already approved, delete process can not be executed!", "danger")
                Else
                    Dim atLeastOneRowDeleted As Boolean = False
                    ' Iterate through the Products.Rows property
                    For Each row As GridViewRow In GridVwHeaderChckbox.Rows
                        Dim cb As CheckBox = row.FindControl("chkEmp")
                        If cb IsNot Nothing AndAlso cb.Checked Then
                            count = count + 1
                            ' Delete row! (Well, not really...)
                            atLeastOneRowDeleted = True
                            ' First, get the ProductID for the selected row
                            Dim ID As String = Convert.ToString(GridVwHeaderChckbox.DataKeys(row.RowIndex).Values(0).ToString)
                            Dim startdate As Date = CDate(GridVwHeaderChckbox.DataKeys(row.RowIndex).Values(1))
                            Dim enddate As Date = CDate(GridVwHeaderChckbox.DataKeys(row.RowIndex).Values(2))
                            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Finance_Payslip_Delete", ID, startdate, enddate)
                        End If
                    Next
                    Process.loadalert(divalert, msgalert, count.ToString & " records successfully deleted", "success")
                    LoadLoans()
                End If

           
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Protected Sub btnImport_Click(sender As Object, e As EventArgs)
        Try
            If lbapproval.InnerText.ToLower.Contains("approved") = False Then
                Session("View") = "multiple"
                Response.Write("<script language='javascript'> { popup = window.open(""SalaryPayslipGenerate.aspx"" , ""Stone Details"", ""height=450,width=600,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no"");  popup.focus(); }</script>")
            Else
                Process.loadalert(divalert, msgalert, "Payroll is already approved, payroll cannot be re-generated unless approve is dropped!", "danger")
            End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub


    'Protected Sub btnImport0_Click(sender As Object, e As EventArgs) Handles btnImport0.Click
    '    Try
    '        Session("View") = "single"
    '        Response.Write("<script language='javascript'> { popup = window.open(""SalaryPayslipGenerate.aspx"" , ""Stone Details"", ""height=350,width=450,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no"");  popup.focus(); }</script>")

    '    Catch ex As Exception
    '        
    '    End Try
    'End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect(Session("PreviousPage").ToString())
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnPayment_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect("Paystack?id=" + txtid.Text)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnapprovalnotify_Click(sender As Object, e As EventArgs)
        Try
            Dim PayrollPeriod As String = "", datecreated As String = "", netamount As String = "0"
            Dim strDataSet As New DataSet
            strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Finance_Payslip_Primary_Get", txtid.Text)
            If strDataSet.Tables(0).Rows.Count > 0 Then
                PayrollPeriod = strDataSet.Tables(0).Rows(0).Item("period").ToString
                datecreated = strDataSet.Tables(0).Rows(0).Item("datecreated").ToString
                netamount = strDataSet.Tables(0).Rows(0).Item("netpay").ToString
            End If

            strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Payroll_Option_Approver_Get_Company", Session("company"))
            Dim maillist As String = ""
            Dim empIDlist As String = ""
            If strDataSet.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To strDataSet.Tables(0).Rows.Count - 1
                    If i = 0 Then
                        maillist = Process.GetEmailAddress(strDataSet.Tables(0).Rows(i).Item("empid").ToString())
                        empIDlist = strDataSet.Tables(0).Rows(i).Item("empid").ToString()
                    Else
                        maillist = maillist & ";" & Process.GetEmailAddress(strDataSet.Tables(0).Rows(i).Item("empid").ToString())
                        empIDlist = empIDlist & ";" & strDataSet.Tables(0).Rows(i).Item("empid").ToString()
                    End If
                Next
                Dim url = Process.ApplicationURL + "/" + "Module/Finance/Payroll/PayrollPeriod.aspx"
                Process.Payroll_Notification(maillist, PayrollPeriod, datecreated, netamount, "", empIDlist, url)

            End If

            Process.loadalert(divalert, msgalert, "Approval Notification Mail sent", "success")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub
    Private Sub LodaDataTable(empid As String, startdate As Date, enddate As Date)
        Dim dtEarning As New DataTable
        dtEarning = Process.SearchDataP4("Finance_Employee_Payslip_Detail_Get", empid, "earning", startdate, enddate)

        Dim dtDeduction As New DataTable
        dtDeduction = Process.SearchDataP4("Finance_Employee_Payslip_Detail_Get", empid, "deduction", startdate, enddate)
        Generatereport(dtEarning, dtDeduction, Process.GetData("general_info_get"), Server.MapPath(emailFile & empid & "_" & MonthName(enddate.Month, True).ToUpper & enddate.Year.ToString & ".PDF"), MonthName(enddate.Month, True).ToUpper & " " & enddate.Year.ToString, empid)
    End Sub
    Private Sub Generatereport(dtearn As DataTable, dtdeduct As DataTable, logos As DataTable, ByVal savePath As String, ByVal reportdate As String, ByVal empid As String)
        Dim ReportViewer1 As New ReportViewer
        ReportViewer1.ProcessingMode = ProcessingMode.Local
        ReportViewer1.SizeToReportContent = True
        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Module/Finance/Report/Payslip.rdlc")
        Dim _rsource As New ReportDataSource("Payslip_Earning", dtearn)
        Dim _rsource1 As New ReportDataSource("Payslip_Deduction", dtdeduct)
        Dim _rsource2 As New ReportDataSource("logo", logos)
        Dim reportParameter As ReportParameter() = New ReportParameter(1) {}
        reportParameter(0) = New ReportParameter("company", Process.GetCompanyByEmpID(empid))
        reportParameter(1) = New ReportParameter("reportdate", reportdate)
        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.LocalReport.SetParameters(reportParameter)
        ReportViewer1.LocalReport.DataSources.Add(_rsource)
        ReportViewer1.LocalReport.DataSources.Add(_rsource1)
        ReportViewer1.LocalReport.DataSources.Add(_rsource2)
        ReportViewer1.LocalReport.Refresh()
        Dim Bytes() As Byte = ReportViewer1.LocalReport.Render("PDF", "", Nothing, Nothing, Nothing, Nothing, Nothing)

        If File.Exists(savePath) Then
            File.Delete(savePath)
        End If

        Using Stream As New FileStream(savePath, FileMode.Create)
            Stream.Write(Bytes, 0, Bytes.Length)
        End Using
        PayrollFile = savePath
    End Sub
    Protected Sub btnDelete0_Click(sender As Object, e As EventArgs) Handles btsendslips.Click
        Try

            Dim confirmValue As String = Request.Form("confirm_payslip")
            If confirmValue = "Yes" Then
                System.Threading.Thread.Sleep(300)
                If lbapproval.InnerText.ToLower.Contains("approved") Then
                    Dim startdate As Date, enddate As Date, sPeriod As String = ""
                    Dim DataSetDate As New DataSet
                    DataSetDate = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Finance_Payslip_Primary_Get", txtid.Text)
                    If DataSetDate.Tables(0).Rows.Count > 0 Then
                        startdate = CDate(DataSetDate.Tables(0).Rows(0).Item("Startdate"))
                        enddate = CDate(DataSetDate.Tables(0).Rows(0).Item("enddate"))
                        sPeriod = MonthName(enddate.Month, True) & " " & enddate.Year
                    End If


                    Dim strDataSet As New DataSet
                    strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Finance_Payslip_Get_All", txtid.Text, Session("company"))
                    If strDataSet.Tables(0).Rows.Count > 0 Then
                        For i As Integer = 0 To strDataSet.Tables(0).Rows.Count - 1
                            Dim empid As String = strDataSet.Tables(0).Rows(i).Item("empid").ToString
                            Dim empname As String = strDataSet.Tables(0).Rows(i).Item("name").ToString
                            Dim empemail As String = strDataSet.Tables(0).Rows(i).Item("email").ToString

                            LodaDataTable(empid, startdate, enddate)
                            Process.Payslip(empid, empemail, empname, sPeriod, PayrollFile)
                            'If File.Exists(PayrollFile) Then
                            '    File.Delete(PayrollFile)
                            'End If
                        Next
                        Process.loadalert(divalert, msgalert, "Email Payslip complete", "success")
                    End If
                Else
                    Process.loadalert(divalert, msgalert, "Payslips can be sent only if payroll is approved!", "warning")
                    Exit Sub
                End If
            End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Protected Sub btnExport_Click(sender As Object, e As EventArgs)
        Try
            If Process.ExportExcel(DataData(), Session("company") & "_payroll") = False Then
                Response.Write(Process.strExp)
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & Process.strExp + "')", True)
            End If


        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub
End Class