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
Public Class TerminalBenefits
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "TERMINALPAY"
    Dim Approvalstatic As String = ""
    Dim emailFile As String = ConfigurationManager.AppSettings("EmailURL")
    Dim PayrollFile As String = ""
    
    Private Function DataData(LoadType As String) As DataTable

        Dim strDataSet As New DataTable

        If Session("LoadType") = "All" Then
            strDataSet = Process.SearchData("Finance_Payslip_Terminal_Get_All", cboCompany.SelectedValue)
        ElseIf Session("LoadType") = "Find" Then
            strDataSet = Process.SearchDataP2("Finance_Payslip_Terminal_Search", cboCompany.SelectedValue, txtsearch.Value.Trim)
        End If
        pagetitle.InnerText = cboCompany.SelectedValue & ": Terminal Benefits " & txtsearch.Value.Trim & "(" & FormatNumber(strDataSet.Rows.Count, 0) & ")"
        Return strDataSet
    End Function


    Private Sub LoadLoans(LoadType As String)
        Try
            'SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, script)

            dtTable = DataData(LoadType)

            GridVwHeaderChckbox.DataSource = dtTable 'Process.SearchDataP3("Finance_Salary_Get_All", radLocation.SelectedText, radLocation.SelectedValue, StGrade)

            GridVwHeaderChckbox.AllowSorting = True
            GridVwHeaderChckbox.AllowPaging = True
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    'Private Function GetPeriod() As String
    '    Try
    '        Dim outcome As String = ""
    '        Dim strDataSet As New DataSet
    '        strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Finance_Payslip_Primary_Get", txtid.Text)
    '        If strDataSet.Tables(0).Rows.Count > 0 Then
    '            Dim rundate As Date = CDate(strDataSet.Tables(0).Rows(0).Item("Startdate"))
    '            outcome = MonthName(rundate.Month, True) & ", " & rundate.Year
    '            Approvalstatic = strDataSet.Tables(0).Rows(0).Item("approvalstatus").ToString
    '        End If
    '        Return outcome
    '    Catch ex As Exception
    '        Return ""
    '    End Try
    'End Function
    

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            'Session("View") = "Loans"
            If Process.AuthenAction(Session("role"), AuthenCode, "Read") = False Then
                content.Style.Add("display", "none")
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform view this page", "info")
                Exit Sub
            End If
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

                Session("LoadType") = "All"
                LoadLoans(Session("LoadType"))

            End If
        Catch ex As Exception
             Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub SortRecords(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Try
            Dim table As New DataTable
            'SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, script)
            table = DataData(Session("LoadType"))

            Dim sortExpression As String = e.SortExpression
            Dim direction As String = String.Empty
            If SortDirection = SortDirection.Ascending Then
                SortDirection = SortDirection.Descending
                direction = " DESC"
            Else
                SortDirection = SortDirection.Ascending
                direction = " ASC"
            End If

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
            Dim table As New DataTable
            'SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, script)
            table = DataData(Session("LoadType"))

            GridVwHeaderChckbox.PageIndex = e.NewPageIndex

            GridVwHeaderChckbox.DataSource = table
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

            lblAppoval.Text = "Approval Stat: " & Approvalstatic
            If txtsearch.Value.Trim = "" Then
                Session("LoadType") = "All"
            Else
                Session("LoadType") = "Find"
            End If
            LoadLoans(Session("LoadType"))
            'End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub Delete(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Delete") = False Then
                Response.Write("You don't have privilege to perform this action")
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
                Exit Sub
            End If
            Dim confirmValue As String = Request.Form("confirm_value")
            If confirmValue = "Yes" Then
                Dim atLeastOneRowDeleted As Boolean = False
                ' Iterate through the Products.Rows property
                For Each row As GridViewRow In GridVwHeaderChckbox.Rows
                    Dim cb As CheckBox = row.FindControl("chkEmp")
                    If cb IsNot Nothing AndAlso cb.Checked Then
                        ' Delete row! (Well, not really...)
                        atLeastOneRowDeleted = True
                        ' First, get the ProductID for the selected row
                        Dim ID As String = Convert.ToString(GridVwHeaderChckbox.DataKeys(row.RowIndex).Value)
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Finance_Payslip_Terminal_Delete", ID)
                    End If
                Next
                LoadLoans(Session("LoadType"))
            Else
                Process.loadalert(divalert, msgalert, "Delete has been cancelled", "danger")
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


    'Private Sub LodaDataTable(empid As String, startdate As Date, enddate As Date)
    '    Dim dtEarning As New DataTable
    '    dtEarning = Process.SearchDataP4("Finance_Employee_Payslip_Detail_Get", empid, "earning", startdate, enddate)

    '    Dim dtDeduction As New DataTable
    '    dtDeduction = Process.SearchDataP4("Finance_Employee_Payslip_Detail_Get", empid, "deduction", startdate, enddate)
    '    Generatereport(dtEarning, dtDeduction, Server.MapPath(emailFile & empid & "_" & MonthName(enddate.Month, True).ToUpper & enddate.Year.ToString & ".PDF"))
    'End Sub
    'Private Sub Generatereport(dtearn As DataTable, dtdeduct As DataTable, ByVal savePath As String)
    '    Dim ReportViewer1 As New ReportViewer
    '    ReportViewer1.ProcessingMode = ProcessingMode.Local
    '    ReportViewer1.SizeToReportContent = True
    '    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Module/Finance/Report/Payslip.rdlc")
    '    Dim _rsource As New ReportDataSource("Payslip_Earning", dtearn)
    '    Dim _rsource1 As New ReportDataSource("Payslip_Deduction", dtdeduct)
    '    ReportViewer1.LocalReport.DataSources.Add(_rsource)
    '    ReportViewer1.LocalReport.DataSources.Add(_rsource1)
    '    ReportViewer1.LocalReport.Refresh()
    '    Dim Bytes() As Byte = ReportViewer1.LocalReport.Render("PDF", "", Nothing, Nothing, Nothing, Nothing, Nothing)

    '    If File.Exists(savePath) Then
    '        File.Delete(savePath)
    '    End If

    '    Using Stream As New FileStream(savePath, FileMode.Create)
    '        Stream.Write(Bytes, 0, Bytes.Length)
    '    End Using
    '    PayrollFile = savePath
    'End Sub

    Private Sub cboCompany_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboCompany.SelectedIndexChanged
        Try
            Session("company") = cboCompany.SelectedValue
            Session("LoadType") = "All"
            LoadLoans(Session("LoadType"))
        Catch ex As Exception

        End Try
    End Sub
End Class