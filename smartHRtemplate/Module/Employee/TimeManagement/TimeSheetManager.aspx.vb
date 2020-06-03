Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports Telerik.Web.UI
Public Class TimeSheetManager
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "APPTIMESHEET"

    
    Private Function GeSurbodinateTable(LoadType As String) As DataTable
        'Try
        Dim sData As New DataTable
        If LoadType = "All" Then
            sData = Process.SearchDataP2("Time_Sheet_Get_PMAll_Projects", Session("UserEmpID"), radSubStatus.SelectedItem.Text)

        ElseIf LoadType = "Find" Then
            sData = Process.SearchDataP3("Time_Sheet_Get_PMAll_Projects_search", Session("UserEmpID"), radSubStatus.SelectedItem.Text, txtSubSearch.Value)
        End If
        pagetitle.InnerText = "Approvals: " & radSubStatus.SelectedItem.Text & " " & txtSubSearch.Value & " Time Sheet(" & sData.Rows.Count.ToString & ")"
        Return sData
    End Function
    Private Sub LoadSurbodinateTimeSheet()
        Try
            'Session("LoadType")
            GridView1.DataSource = GeSurbodinateTable(Session("LoadType"))
            GridView1.AllowSorting = True
            GridView1.AllowPaging = True
            GridView1.DataBind()
            If CInt(Session("pageIndex1")) > GridView1.PageCount Then
                GridView1.PageIndex = Session("pageIndex1")
            Else
                GridView1.PageIndex = GridView1.PageCount - 1
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            pagetitle.InnerText = "Time Sheet"
            If Process.AuthenAction(Session("role"), AuthenCode, "Read") = False Then                
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "danger")
                Exit Sub
            End If

            If Not Me.IsPostBack Then
                Session("pageIndex1") = 0
                radSubStatus.Items.Clear()
                radSubStatus.Items.Add("In Progress")
                radSubStatus.Items.Add("Not Started")
                radSubStatus.Items.Add("Completed")
                radSubStatus.Items.Add("Cancelled")

                Process.AssignRadComboValue(radSubStatus, "In Progress")
                Session("LoadType") = "All"
                LoadSurbodinateTimeSheet()



            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub SortSurbodinateRecords(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
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
            table = GeSurbodinateTable(Session("LoadType")) 'Process.SearchDataP4("Time_Sheet_Approver_get_all", Session("UserEmpID"), radSubStatus.SelectedItem.Text, radSubDateFrom.SelectedDate, radSubDateTo.SelectedDate)
            GridView1.PageIndex = Session("pageIndex1")
            table.DefaultView.Sort = sortExpression & direction
            GridView1.DataSource = table
            GridView1.DataBind()
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

    Protected Sub OnRowSurbodinateDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GridView1, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub



    Protected Sub GridView1_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        Try
            GridView1.PageIndex = e.NewPageIndex
            Session("pageIndex1") = e.NewPageIndex
            GridView1.DataSource = GeSurbodinateTable(Session("LoadType")) 'Process.SearchDataP4("Time_Sheet_get_all", Session("UserEmpID"), radSubStatus.SelectedItem.Text, radSubDateFrom.SelectedDate, radSubDateTo.SelectedDate)
            GridView1.DataBind()
        Catch ex As Exception
        End Try
    End Sub


    Protected Sub btnSubFind_Click(sender As Object, e As EventArgs)
        Try
            'If Not Me.IsPostBack Then
            If txtSubSearch.Value.Trim = "" Then
                Session("LoadType") = "All"
            Else
                Session("LoadType") = "Find"
            End If
            LoadSurbodinateTimeSheet()
            'End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


End Class