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
Public Class PensionReport
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
            strDataSet = Process.SearchDataP2("Pension_Contribution_Report", cboCompany.SelectedValue, cbodate.SelectedValue)
            'ElseIf LoadType = "Find" Then
            '    strDataSet = Process.SearchDataP2("Finance_Payroll_Search_Report", cboMonth.SelectedValue, txtsearch.Value.Trim)
        End If
        pagetitle.InnerText = cboCompany.SelectedValue & ": " & " Pension Contribution"
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

                LoadRadComboTextAndValue1(cbodate, "select distinct EndDate from finance_payslip", "", "", "EndDate", "EndDate", True)

                'lblview.Text = cboPeriod.SelectedItem.Text & " Payroll"
                Session("LoadType") = "All"
                'LoadLoans(Session("LoadType"))

            End If
        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    Public Shared Sub LoadRadComboTextAndValue1(ByVal radBox As RadComboBox, ByVal SP As String, ByVal Param1 As String, ByVal Param2 As String, ByVal DisplayText As String, ByVal setValue As String, Optional ByVal ApplyNA As Boolean = True)

        'Load RadCombo Box with Display Text and Value
        Dim strDataSet As New DataSet
        radBox.Items.Clear()
        If ApplyNA = True Then
            Dim itemTemp As New RadComboBoxItem()
            itemTemp.Text = "--Select Date--"
            itemTemp.Value = "--Select Date--"
            radBox.Items.Add(itemTemp)
            itemTemp.DataBind()
        End If
        If SP = "Emp_PersonalDetail_get_all_Specific" Then
            strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, SP, Param1, Param2, 1, 10000000)
        Else
            strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, SP)
        End If

        For i As Integer = 0 To strDataSet.Tables(0).Rows.Count - 1
            'strDataSet.Tables(0).Rows(i).Item(Index).ToString()
            Dim item As New RadComboBoxItem()
            item.Text = Process.DDMONYYYY(Convert.ToDateTime(strDataSet.Tables(0).Rows(i).Item(DisplayText.Trim).ToString()))
            item.Value = strDataSet.Tables(0).Rows(i).Item(setValue.Trim).ToString()
            radBox.Items.Add(item)
            item.DataBind()
        Next
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
            pagetitle.InnerText = cbodate.SelectedItem.Text & " Pension Contribution"

            Session("LoadType") = "All"

            LoadLoans(Session("LoadType"))
            'End If

        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub




    'Protected Sub cboYear_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboYear.SelectedIndexChanged
    '    Try
    '        Process.LoadRadComboTextAndValueP2(cboMonth, "Finance_Payslip_Primary_Get_All", cboYear.SelectedValue, cboCompany.SelectedValue, "Period", "id", False)
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Protected Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            If Process.ExportPayroll(DataData("All"), "PensionContributionReport") = False Then
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & Process.strExp + "')", True)
            End If
        Catch ex As Exception
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    'Private Sub cboCompany_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboCompany.SelectedIndexChanged
    '    Try
    '        Process.LoadRadComboTextAndValueP2(cboMonth, "Finance_Payslip_Primary_Get_All", cboYear.SelectedValue, cboCompany.SelectedValue, "Period", "id", False)
    '    Catch ex As Exception

    '    End Try
    'End Sub
End Class