Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Public Class Roles
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "UserRole"
    Dim ismulti As String = CStr(SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select isnull(ismulticompany,'No')  from general_info"))
    Private Function LoadRoles(LoadType As String) As DataTable
        Dim datatables As New DataTable
        If LoadType = "All" Then
            datatables = Process.GetData("roles_get_all")
        ElseIf LoadType = "Find" Then
            datatables = Process.SearchData("roles_search", search.Value.Trim)
        End If
        pagetitle.InnerText = "User Roles (" & FormatNumber(datatables.Rows.Count, 0) & ")"
        Return datatables
    End Function
    Private Sub LoadGrid(LoadType As String)
        Try

            'GridVwHeaderChckbox.HeaderRow.ToolTip = "click to sort records"
            GridVwHeaderChckbox.DataSource = LoadRoles(LoadType)
            GridVwHeaderChckbox.AllowSorting = True
            GridVwHeaderChckbox.AllowPaging = True
            GridVwHeaderChckbox.DataBind()

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Read") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "warning")

                Exit Sub
            End If


            If Not Me.IsPostBack Then
                Session("LoadType") = "All"
                LoadGrid(Session("LoadType"))
            End If
            If ismulti.ToLower = "no" Then
                GridVwHeaderChckbox.Columns(4).Visible = False
            Else
                GridVwHeaderChckbox.Columns(4).Visible = True
            End If
            'txtsearch.Text = Request.QueryString("id")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub SortRecords(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Try
            Dim sortExpression As String = e.SortExpression
            Session("sortroleExpression") = sortExpression
            Dim direction As String = String.Empty
            If SortsDirection = SortDirection.Ascending Then
                SortsDirection = SortDirection.Descending
                direction = " DESC"
            Else
                SortsDirection = SortDirection.Ascending
                direction = " ASC"
            End If
            Dim table As New DataTable
            table = LoadRoles(Session("LoadType"))

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
            GridVwHeaderChckbox.PageIndex = e.NewPageIndex
            GridVwHeaderChckbox.DataSource = LoadRoles(Session("LoadType"))
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    'Protected Sub GridVwHeaderChckbox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridVwHeaderChckbox.SelectedIndexChanged
    '    Process.criteria = GridVwHeaderChckbox.SelectedRow.Cells(2).Text
    'End Sub

    Protected Sub OnRowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GridVwHeaderChckbox, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub

    'Protected Sub OnSelectedIndexChanged(sender As Object, e As EventArgs)
    '    'For Each row As GridViewRow In GridVwHeaderChckbox.Rows
    '    '    If row.RowIndex = GridVwHeaderChckbox.SelectedIndex Then
    '    '        row.BackColor = ColorTranslator.FromHtml("#A1DCF2")
    '    '        row.ToolTip = String.Empty
    '    '        ' row.Cells(2).Text
    '    '        Exit For
    '    '    Else
    '    '        row.BackColor = ColorTranslator.FromHtml("#FFFFFF")
    '    '        row.ToolTip = "Click to select this row."
    '    '    End If
    '    'Next
    '    Session("ID") = GridVwHeaderChckbox.SelectedRow.Cells(2).Text
    '    'txtsearch.Text = GridVwHeaderChckbox.SelectedRow.Cells(2).Text
    'End Sub

    Protected Sub btnFind_Click(sender As Object, e As EventArgs)
        Try
            'If Not Me.IsPostBack Then
            Session("SelectedParentNode") = "Admin"
            Session("SelectedChildNode") = "Roles"
            LoadGrid("Find")
            'End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "warning")
                Exit Sub
            End If

            Response.Redirect("~/Module/Admin/AddRole", True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub Delete(sender As Object, e As EventArgs) Handles btnDeleteRole.Click
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Delete") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "warning")

                Exit Sub
            End If
            Dim count As Integer = 0
            Dim confirmValue As String = Request.Form("confirm_value")
            If confirmValue = "Yes" Then
                Dim atLeastOneRowDeleted As Boolean = False
                ' Iterate through the Products.Rows property
                For Each row As GridViewRow In GridVwHeaderChckbox.Rows
                    ' Access the CheckBox
                    Dim cb As CheckBox = row.FindControl("chkEmp")
                    If cb IsNot Nothing AndAlso cb.Checked Then
                        ' Delete row! (Well, not really...)
                        atLeastOneRowDeleted = True
                        count = count + 1
                        ' First, get the ProductID for the selected row
                        Dim ID As String = _
                            Convert.ToString(GridVwHeaderChckbox.DataKeys(row.RowIndex).Value)
                        ' "Delete" the row
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "roles_delete", ID)
                    End If
                Next
                Process.loadalert(divalert, msgalert, count.ToString & " records successfully deleted", "success")
                LoadGrid(Session("LoadType"))
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Private Sub GridVwHeaderChckbox_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridVwHeaderChckbox.RowCreated
        Try
            Process.SortArrow(e, SortsDirection, Session("sortroleExpression"))
        Catch ex As Exception

        End Try
    End Sub
  
    Private Sub GridVwHeaderChckbox_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridVwHeaderChckbox.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GridVwHeaderChckbox, "Select$" & e.Row.RowIndex)
                e.Row.ToolTip = "Click to select this row."
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class