Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Public Class Interviews
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "EMPINTERVIEW"
    Private Function GetData() As DataTable
        search.Value = Session("empinterviewsearch")
        Dim s As New DataTable
        If search.Value.Trim = "" Then
            s = Process.SearchDataP2("Recruit_Job_Interview_Comment_get_all", Session("UserEmpID"), Session("reviewyear"))
        Else
            s = Process.SearchDataP2("Recruit_Job_Interview_Comment_search", Session("UserEmpID"), search.Value.Trim)
        End If
        pagetitle.InnerText = "Interviews (" & s.Rows.Count.ToString & ")"
        Return s


    End Function

    Private Sub LoadGrid()
        Try
            GridVwHeaderChckbox.PageIndex = CInt(Session("empinterviewindex"))
            GridVwHeaderChckbox.DataSource = GetData()
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
                Process.LoadRadComboTextAndValueP1(radYear, "Performance_Appraisal_Summary_ReviewYear", Session("UserEmpID"), "reviewyear", "reviewyear", False)
                'Dim reviewyear As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select ReviewYear from Performance_Appraisal_Cycle a where a.company = '" + Session("Organisation") + "' and a.status = 'Open'")
                If Session("empinterviewsearch") Is Nothing Then
                    Session("empinterviewsearch") = ""
                End If
                Session("reviewyear") = Date.Now.Year
                Process.AssignRadComboValue(radYear, Session("reviewyear"))
                If Session("empinterviewindex") Is Nothing Then
                    Session("empinterviewindex") = "0"
                End If
                LoadGrid()
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Protected Sub radYear_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles radYear.SelectedIndexChanged
        Try
            Session("reviewyear") = radYear.Text
            Session("empinterviewsearch") = ""
            LoadGrid()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub SortRecords(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Try
            Dim sortExpression As String = e.SortExpression
            Session("empinterviewsort") = sortExpression
            Dim direction As String = String.Empty
            If SortsDirection = SortDirection.Ascending Then
                SortsDirection = SortDirection.Descending
                direction = " DESC"
            Else
                SortsDirection = SortDirection.Ascending
                direction = " ASC"
            End If
            GridVwHeaderChckbox.PageIndex = CInt(Session("empinterviewindex"))
            Dim table As DataTable = GetData()
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
            Session("empinterviewindex") = e.NewPageIndex
            LoadGrid()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub OnRowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GridVwHeaderChckbox, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub

    Protected Sub btnFind_Click(sender As Object, e As EventArgs)
        Try
            Session("empinterviewsearch") = search.Value.Trim
            LoadGrid()
            'End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

   

    Private Sub GridVwHeaderChckbox_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridVwHeaderChckbox.RowCreated
        Try
            Process.SortArrow(e, SortsDirection, Session("empinterviewsort"))
        Catch ex As Exception
        End Try

    End Sub
End Class