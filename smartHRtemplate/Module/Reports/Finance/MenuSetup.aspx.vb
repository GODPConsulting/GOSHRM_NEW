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
Public Class MenuSetup
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "MENUSETUP"
    Dim Approvalstatic As String = ""
    Dim emailFile As String = ConfigurationManager.AppSettings("EmailURL")
    Dim PayrollFile As String = ""

    Private Function DataData(LoadType As String) As DataTable

        Dim strDataSet As New DataTable

        If LoadType = "All" Then
            Dim sDataset As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "SELECT id, modulecode, SubModule, SubModule1, submodule2 FROM menu")
            strDataSet = sDataset.Tables(0)
        ElseIf LoadType = "Find" Then
            Dim sDataset As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "SELECT id, modulecode, SubModule, SubModule1, submodule2 FROM menu where modulecode = '" + modules.SelectedValue + "'")
            strDataSet = sDataset.Tables(0)
        End If
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                LoadRadComboTextAndValueP2(modules, "select module, modulecode from modules where status = 1", "", "", "module", "modulecode", True)

                Session("LoadType") = "All"
                LoadLoans(Session("LoadType"))
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Public Shared Sub LoadRadComboTextAndValueP2(ByVal radBox As RadComboBox, ByVal SP As String, ByVal Param1 As String, ByVal Param2 As String, ByVal DisplayText As String, ByVal setValue As String, Optional ByVal ApplyNA As Boolean = True)

        'Load RadCombo Box with Display Text and Value
        Dim strDataSet As New DataSet
        radBox.Items.Clear()
        If ApplyNA = True Then
            Dim itemTemp As New RadComboBoxItem()
            itemTemp.Text = "--SELECT--"
            itemTemp.Value = "--SELECT--"
            radBox.Items.Add(itemTemp)
            itemTemp.DataBind()
        End If

        strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, SP)

        For i As Integer = 0 To strDataSet.Tables(0).Rows.Count - 1
            Dim item As New RadComboBoxItem()
            item.Text = strDataSet.Tables(0).Rows(i).Item(DisplayText.Trim).ToString()
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


    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try

            Session("LoadType") = "Find"
            LoadLoans(Session("LoadType"))
            'End If

        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

End Class