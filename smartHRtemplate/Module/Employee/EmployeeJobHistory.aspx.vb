Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Public Class EmployeeJobHistory
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "EMPWORKHISTORY"

    Private Function LoadEmpTypes() As DataTable
        Dim datatables As New DataTable
        If Session("LoadType") = "All" Then
            datatables = Process.SearchData("Emp_Work_History_get_all", lblemp.Text)
        ElseIf Session("LoadType") = "Find" Then
            datatables = Process.SearchDataP2("Emp_Work_History_Search", lblemp.Text, search.Value.Trim)
        End If
        Dim nn As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select name from Employees_All where empid = '" & lblemp.Text & "'")

        Return datatables
    End Function
    Private Sub LoadGrid()
        Try
            'GridVwHeaderChckbox.DataSource = LoadEmpTypes()
            'GridVwHeaderChckbox.AllowSorting = True
            'GridVwHeaderChckbox.AllowPaging = True
            'GridVwHeaderChckbox.DataBind()

            dlworkhistory.DataSource = LoadEmpTypes()
            dlworkhistory.DataBind()
        Catch ex As Exception
            response.write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not Me.IsPostBack Then
                Session("LoadType") = "All"
                If Request.QueryString("empid") IsNot Nothing Then
                    lblemp.Text = Request.QueryString("empid")
                Else
                    lblemp.Text = Session("UserEmpID")
                End If
                LoadGrid()
            End If
        Catch ex As Exception
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    'Protected Sub SortRecords(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
    '    Try
    '        Dim sortExpression As String = e.SortExpression
    '        Dim direction As String = String.Empty
    '        If SortDirection = SortDirection.Ascending Then
    '            SortDirection = SortDirection.Descending
    '            direction = " DESC"
    '        Else
    '            SortDirection = SortDirection.Ascending
    '            direction = " ASC"
    '        End If
    '        Dim table As DataTable = LoadEmpTypes()
    '        table.DefaultView.Sort = sortExpression & direction
    '        GridVwHeaderChckbox.DataSource = table
    '        GridVwHeaderChckbox.DataBind()
    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Public Property SortDirection() As SortDirection
    '    Get
    '        If ViewState("SortDirection") Is Nothing Then
    '            ViewState("SortDirection") = SortDirection.Ascending
    '        End If
    '        Return DirectCast(ViewState("SortDirection"), SortDirection)
    '    End Get
    '    Set(ByVal value As SortDirection)
    '        ViewState("SortDirection") = value
    '    End Set
    'End Property


    'Protected Sub GridVwHeaderChckbox_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridVwHeaderChckbox.PageIndexChanging
    '    Try
    '        GridVwHeaderChckbox.PageIndex = e.NewPageIndex
    '        GridVwHeaderChckbox.DataSource = LoadEmpTypes(Session("LoadType"))
    '        GridVwHeaderChckbox.DataBind()
    '    Catch ex As Exception
    '    End Try
    'End Sub

    'Protected Sub OnRowDataBound(sender As Object, e As GridViewRowEventArgs)
    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GridVwHeaderChckbox, "Select$" & e.Row.RowIndex)
    '        e.Row.ToolTip = "Click to select this row."
    '    End If
    'End Sub

    Protected Sub btnFind_Click(sender As Object, e As EventArgs)
        Try
            If search.Value.Trim = "" Then
                Session("LoadType") = "All"
            Else
                Session("LoadType") = "Find"
            End If
            'If Not Me.IsPostBack Then
            LoadGrid()
            'End If
        Catch ex As Exception
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub



    Protected Sub btnExport_Click(sender As Object, e As EventArgs)
        Try
            ''Process.Export(GridVwHeaderChckbox, "JobGrades", 1, 2)
            'Response.Clear()
            'Response.Buffer = True
            'Response.AddHeader("content-disposition", "attachment;filename=" & lblemp.Text & "_workhistory.csv")
            'Response.Charset = ""
            'Response.ContentType = "application/text"
            'Dim sBuilder As StringBuilder = New System.Text.StringBuilder()
            'For index As Integer = 1 To GridVwHeaderChckbox.Columns.Count - 1
            '    sBuilder.Append(GridVwHeaderChckbox.Columns(index).HeaderText + ","c)

            'Next
            'sBuilder.Append(vbCr & vbLf)
            'For i As Integer = 0 To GridVwHeaderChckbox.Rows.Count - 1
            '    For k As Integer = 1 To GridVwHeaderChckbox.HeaderRow.Cells.Count - 1
            '        'If k = 2 Then
            '        '    Dim controls As HyperLink = DirectCast(GridVwHeaderChckbox.Rows(i).Cells(k).FindControl("HyperLink1"), HyperLink)
            '        '    sBuilder.Append(controls.Text.Replace(",", "") + ",")
            '        'Else
            '        '    sBuilder.Append(GridVwHeaderChckbox.Rows(i).Cells(k).Text.Replace(",", "") + ",")
            '        'End If
            '        sBuilder.Append(GridVwHeaderChckbox.Rows(i).Cells(k).Text.Replace(",", "") + ",")
            '    Next
            '    sBuilder.Append(vbCr & vbLf)
            'Next
            'Response.Output.Write(sBuilder.ToString())
            'Response.Flush()
            'Response.[End]()

        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub
End Class