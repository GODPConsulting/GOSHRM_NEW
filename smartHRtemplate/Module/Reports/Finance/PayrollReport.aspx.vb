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
Public Class PayrollReport
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "PAYROLLREPORT"
    Dim Approvalstatic As String = ""
    Dim emailFile As String = ConfigurationManager.AppSettings("EmailURL")
    Dim PayrollFile As String = ""
    
    Private Function DataData(LoadType As String) As DataTable

        Dim strDataSet As New DataTable

        If LoadType = "All" Then
            strDataSet = Process.SearchData("Finance_Payroll_Report", cboPeriod.SelectedValue)
        ElseIf LoadType = "Find" Then
            strDataSet = Process.SearchDataP2("Finance_Payroll_Search_Report", cboPeriod.SelectedValue, txtsearch.Value.Trim)
        End If
        pagetitle.InnerText = cboCompany.SelectedValue & ": " & cboPeriod.SelectedItem.Text & " Payroll"
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
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                Dim ismulti As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select isnull(ismulticompany,'No')  from general_info")
                If ismulti.ToLower = "no" Then
                    Process.LoadRadComboTextAndValueP2(cboCompany, "Company_Structure_Get_ByLevel", "1", Session("Access"), "name", "name", False)
                Else
                    Process.LoadRadComboTextAndValueP2(cboCompany, "Company_Structure_Get_ByLevel", "2", Session("Access"), "name", "name", False)
                End If

                Process.AssignRadComboValue(cboCompany, Session("Organisation"))

                Process.LoadRadComboTextAndValueP1(cboYear, "Data_Year_Get_All", 2000, "Year", "Year", False)
                Process.AssignRadComboValue(cboYear, Date.Now.Year)
                Process.LoadRadComboTextAndValueP2(cboPeriod, "Finance_Payslip_Primary_Get_All", cboYear.SelectedValue, cboCompany.SelectedValue, "Period", "id", False)

                'lblview.Text = cboPeriod.SelectedItem.Text & " Payroll"
                Session("LoadType") = "All"
                'LoadLoans(Session("LoadType"))

            End If
        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
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
            pagetitle.InnerText = cboPeriod.SelectedItem.Text & " Payroll"
            If txtsearch.Value.Trim = "" Then
                Session("LoadType") = "All"
            Else
                Session("LoadType") = "Find"
            End If
            LoadLoans(Session("LoadType"))
            'End If

        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub




    Protected Sub cboYear_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboYear.SelectedIndexChanged
        Try
            Process.LoadRadComboTextAndValueP2(cboPeriod, "Finance_Payslip_Primary_Get_All", cboYear.SelectedValue, cboCompany.SelectedValue, "Period", "id", False)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            'If Process.Export(GridVwHeaderChckbox, cboPeriod.SelectedItem.Text.Replace("/", "%") & "PayrollReport", 0, -1) = False Then
            If Process.ExportPayroll(DataData("All"), "PayrollReport") = False Then
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & Process.strExp + "')", True)
            End If
        Catch ex As Exception
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    Private Sub cboCompany_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboCompany.SelectedIndexChanged
        Try
            Process.LoadRadComboTextAndValueP2(cboPeriod, "Finance_Payslip_Primary_Get_All", cboYear.SelectedValue, cboCompany.SelectedValue, "Period", "id", False)
        Catch ex As Exception

        End Try
    End Sub
End Class