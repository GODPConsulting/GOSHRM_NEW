Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports Telerik.Web.UI
Public Class Query
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "EMPQUERIES"


    Private Function GetMyQueryTable(LoadType As String) As DataTable
        Try
            Dim sData As New DataTable
            If LoadType = "All" Then
                sData = Process.SearchData("Performance_Employee_Query_Get_Employee", Session("UserEmpID"))

            ElseIf LoadType = "Find" Then
                sData = Process.SearchDataP2("Performance_Employee_Query_Search_Employee", Session("UserEmpID"), search.Value)
            End If
            Return sData
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
            Return Nothing
        End Try
    End Function
    Private Sub LoadMyQuery()
        Try
            'Session("LoadType")
            GridView1.DataSource = GetMyQueryTable(Session("LoadType"))
            GridView1.AllowSorting = True
            GridView1.AllowPaging = True
            GridView1.DataBind()
            If CInt(Session("pageIndex1")) > GridView1.PageCount Then
                GridView1.PageIndex = GridView1.PageCount - 1
            Else
                GridView1.PageIndex = Session("pageIndex1")
            End If
        Catch ex As Exception
            response.write(ex.message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Process.AuthenAction(Session("role"), AuthenCode, "Read") = False Then
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
                Exit Sub
            End If

            If Not Me.IsPostBack Then
                Session("pageIndex1") = 0
                Session("LoadType") = "All"
                LoadMyQuery()
            End If
        Catch ex As Exception
            response.write(ex.message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
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
            table = GetMyQueryTable(Session("LoadType")) 'Process.SearchDataP4("Time_Sheet_Approver_get_all", Session("UserEmpID"), radSubStatus.SelectedItem.Text, radSubDateFrom.SelectedDate, radSubDateTo.SelectedDate)
            GridView1.PageIndex = Session("pageIndex1")
            table.DefaultView.Sort = sortExpression & direction
            GridView1.DataSource = table
            GridView1.DataBind()

        Catch ex As Exception
            response.write(ex.message)
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

    Protected Sub OnRowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GridView1, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub



    Protected Sub GridView1_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        Try
            GridView1.PageIndex = e.NewPageIndex
            Session("pageIndex1") = e.NewPageIndex
            GridView1.DataSource = GetMyQueryTable(Session("LoadType")) 'Process.SearchDataP4("Time_Sheet_get_all", Session("UserEmpID"), radSubStatus.SelectedItem.Text, radSubDateFrom.SelectedDate, radSubDateTo.SelectedDate)
            GridView1.DataBind()
        Catch ex As Exception
        End Try
    End Sub


    Protected Sub btnSubFind_Click(sender As Object, e As EventArgs)
        Try
            'If Not Me.IsPostBack Then
            If search.Value.Trim = "" Then
                Session("LoadType") = "All"
            Else
                Session("LoadType") = "Find"
            End If
            LoadMyQuery()
            'End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub



   
End Class