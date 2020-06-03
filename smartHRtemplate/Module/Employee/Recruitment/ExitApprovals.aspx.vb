Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Public Class ExitApprovals
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "EMPAPPROVALEXIT"
   
    Private Function LoadExitDataTable(loadtype As String) As DataTable
        Dim datatables As New DataTable
        If loadtype = "All" Then
            datatables = Process.SearchDataP4("Emp_Termination_Get_Surbodinate_Employee", Session("UserEmpID"), radStatus.SelectedItem.Text, dateFrom.SelectedDate, dateTo.SelectedDate)
        ElseIf loadtype = "Find" Then
            datatables = Process.SearchDataP5("Emp_Termination_Search_Surbodinate_Employee", Session("UserEmpID"), radStatus.SelectedItem.Text, dateFrom.SelectedDate, dateTo.SelectedDate, txtsearch.Value)
        End If
        pagetitle.InnerText = radStatus.SelectedItem.Text & " " & Process.DDMONYYYY(dateFrom.SelectedDate) & " : " & Process.DDMONYYYY(dateTo.SelectedDate) & " " & txtsearch.Value & " JOB EXIT (" & datatables.Rows.Count.ToString & ")"
        Return datatables
    End Function
    Private Sub LoadLeaves(LoadType As String)
        Try

            GridVwHeaderChckbox.DataSource = LoadExitDataTable(LoadType)
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


                radStatus.Items.Clear()
                radStatus.Items.Add("Pending")
                radStatus.Items.Add("Approved")
                radStatus.Items.Add("Cancelled")
                radStatus.Items.Add("Rejected")

                'Leave
                dateFrom.SelectedDate = Date.Now.AddDays(1 - Date.Now.Day).AddMonths(1 - Date.Now.Month)
                dateTo.SelectedDate = Date.Now.AddMonths(12 - Date.Now.Month).AddDays(31 - Date.Now.Day)
                Session("LoadType") = "All"
                LoadLeaves(Session("LoadType"))
            End If
        Catch ex As Exception
             Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub SortRecords(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Try
            Dim sortExpression As String = e.SortExpression
            Dim direction As String = String.Empty
            If SortDirection = SortDirection.Ascending Then
                SortDirection = SortDirection.Descending
                direction = " DESC"
            Else
                SortDirection = SortDirection.Ascending
                direction = " ASC"
            End If
            Dim table As New DataTable
            table = LoadExitDataTable(Session("LoadType"))
            table.DefaultView.Sort = sortExpression & direction
            GridVwHeaderChckbox.DataSource = table
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception
             Process.loadalert(divalert, msgalert, ex.Message, "danger")
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
            GridVwHeaderChckbox.PageIndex = e.NewPageIndex
            GridVwHeaderChckbox.DataSource = LoadExitDataTable(Session("LoadType"))
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception
             Process.loadalert(divalert, msgalert, ex.Message, "danger")
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
            If txtsearch.Value.Trim = "" Then
                Session("LoadType") = "All"
            Else
                Session("LoadType") = "Find"
            End If

            LoadLeaves(Session("LoadType"))
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

   



End Class