Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Public Class TeamAttendanceCalendar
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "APPATTENDANCE"
    Dim AuthenCode2 As String = "TEAMATTENDANCE"
    'TEAMATTENDANCE
    Dim Pages As String = "Team Attendance"
    Dim apptype As String = ConfigurationManager.AppSettings("apptype")
    Private socialEvents As DataTable

   
    Private Function LoadEmpTypes(LoadType As String) As DataTable
        Dim datatables As New DataTable
        pagetitle.InnerText = txtsearch.Value & " My Team Work Shift"
        If LoadType = "All" Then
            datatables = Process.SearchDataP2("Time_Employee_Attendance_Get_My_Team", Session("UserEmpID"), MyCalendar.SelectedDate)
        ElseIf LoadType = "Find" Then
            datatables = Process.SearchDataP3("Time_Employee_Attendance_Search_My_Team", Session("UserEmpID"), MyCalendar.SelectedDate, txtsearch.Value.Trim)
        End If

        Return datatables
    End Function
    Private Sub LoadGrid(LoadType As String)
        Try
            GridVwHeaderChckbox.DataSource = LoadEmpTypes(LoadType)
            GridVwHeaderChckbox.AllowSorting = True
            GridVwHeaderChckbox.AllowPaging = True
            GridVwHeaderChckbox.DataBind()
            pagetitle.InnerText = txtsearch.Value & " " & MyCalendar.SelectedDate.ToLongDateString & " Team Attendance"
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not Me.IsPostBack Then

                Session("LoadType") = "All"
                MyCalendar.SelectedDate = Date.Now
                Session("pageIndex1") = 0
                LoadGrid(Session("LoadType"))


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
            Dim table As DataTable = LoadEmpTypes(Session("LoadType"))
            table.DefaultView.Sort = sortExpression & direction
            GridVwHeaderChckbox.PageIndex = Session("pageIndex1")
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
            GridVwHeaderChckbox.DataSource = LoadEmpTypes(Session("LoadType"))
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub OnRowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridVwHeaderChckbox.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                For Each cell As TableCell In e.Row.Cells
                    Dim read As String = e.Row.Cells(10).Text.Trim
                    Dim imgProd As HyperLink = DirectCast(e.Row.FindControl("HyperLink1"), HyperLink)
                    If read = "" Or read.Contains("nbsp") Then
                        imgProd.NavigateUrl = ""
                        imgProd.Enabled = False
                    End If

                Next
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs)
        Try

            If txtsearch.Value = "" Then
                Session("LoadType") = "All"
            Else
                Session("LoadType") = "Find"
            End If
            'If Not Me.IsPostBack Then
            LoadGrid(Session("LoadType"))
            'End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


    Protected Sub MyCalendar_SelectionChanged(sender As Object, e As EventArgs) Handles MyCalendar.SelectionChanged
        Try
            LoadGrid(Session("LoadType"))
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    

    
End Class