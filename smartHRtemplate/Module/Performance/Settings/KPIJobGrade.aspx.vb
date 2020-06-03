Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Public Class KPIJobGrade
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "COMPETJOBGRADE"
    Private Function LoadTable() As DataTable
        Dim datatables As New DataTable

        If Session("LoadType") = "All" Then
            datatables = Process.GetData("Competency_JobGrade_get_all")
        ElseIf Session("LoadType") = "Find" Then
            datatables = Process.SearchData("Competency_JobGrade_search", txtsearch.Value.Trim)
        End If
        Dim customName As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select KPIToJobGrade from Performance_Custom_Naming")
        If (customName = "") Then
            pagetitle.InnerText = txtsearch.Value & " " & " KPI Mapping to Grade (" & FormatNumber(datatables.Rows.Count, 0) & ")"
        Else
            pagetitle.InnerText = txtsearch.Value & customName & "(" & FormatNumber(datatables.Rows.Count, 0) & ")"
        End If
        Return datatables
    End Function
   

    Private Sub LoadGrid(LoadType As String)
        Try
            GridVwHeaderChckbox.DataSource = LoadTable()
            GridVwHeaderChckbox.AllowSorting = True
            GridVwHeaderChckbox.AllowPaging = True
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'lblView.Text = "KPI Mapping to Grade"
            If Not Me.IsPostBack Then
                Session("pageIndex1") = 0
                Session("LoadType") = "All"
                LoadGrid("All")
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
            Dim table As DataTable = LoadTable()
            GridVwHeaderChckbox.PageIndex = Session("pageIndex1")
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
            GridVwHeaderChckbox.PageIndex = e.NewPageIndex
            Session("pageIndex1") = e.NewPageIndex
            GridVwHeaderChckbox.DataSource = LoadTable()
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub OnRowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridVwHeaderChckbox.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GridVwHeaderChckbox, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs)
        Try
            If txtsearch.Value.Trim Is Nothing Then
                Session("LoadType") = "All"
            Else
                Session("LoadType") = "Find"
            End If
            Session("pageIndex1") = 0
            LoadGrid("")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "danger")
                Exit Sub
            End If
            Process.loadtype = "Add"
            Response.Redirect("~/Module/Performance/Settings/KPIJobGradeUpdate.aspx", True)            
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub Delete(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Delete") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "danger")
                Exit Sub
            End If
            Dim confirmValue As String = Request.Form("confirm_value")
            If confirmValue = "Yes" Then
                Dim atLeastOneRowDeleted As Boolean = False
                ' Iterate through the Products.Rows property
                For Each row As GridViewRow In GridVwHeaderChckbox.Rows
                    ' Access the CheckBox
                    Dim cb As CheckBox = row.FindControl("chkEmp")
                    'Dim ID, ID1 As String
                    If cb IsNot Nothing AndAlso cb.Checked Then
                        ' Delete row! (Well, not really...)
                        atLeastOneRowDeleted = True

                        'Dim ID As String = Convert.ToString(GridVwHeaderChckbox.DataKeys(row.RowIndex).Values(0))
                        Dim ID As String = Convert.ToString(GridVwHeaderChckbox.SelectedRow.Cells(2).Text)
                        Dim ID1 As String = _
                            Convert.ToString(GridVwHeaderChckbox.Rows(row.RowIndex).Cells(3).Text)
                        ' "Delete" the row
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Competency_JobGrade_delete", ID, ID1)
                    End If
                Next
                LoadGrid("All")
                Process.loadalert(divalert, msgalert, "Delete Successful", "success")
            Else
                Process.loadalert(divalert, msgalert, "Delete has been cancelled", "info")
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub LinkButton2_Click(sender As Object, e As EventArgs) Handles LinkButton2.Click
        Try
            Response.Redirect("~/Module/Performance/Settings/KPIJobGradeSummary.aspx")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
End Class