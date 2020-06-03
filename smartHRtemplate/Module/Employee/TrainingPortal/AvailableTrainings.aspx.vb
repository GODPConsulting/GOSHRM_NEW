Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports Telerik.Web.UI
Public Class AvailableTrainings
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "EMPTRAININGS"
    Dim AuthenCode2 As String = "MGRTRAININGS"
    Private Function TrainingData() As DataTable
        Dim strDataSet As New DataTable
        Dim eeee As String = ""
        search.Value = Session("availablesearch")
        If Request.QueryString("id") IsNot Nothing Then
            eeee = "emp"
        End If

        If Session("availablesearch") = "" Then
            strDataSet = Process.SearchDataP5("Training_Sessions_Available_get_all", Session("Organisation"), Session("Dept"), Session("UserJobgrade"), Session("UserJobtitle"), Session("UserEmpID"))
        Else
            strDataSet = Process.SearchDataP6("Training_Sessions_Available_search", Session("Organisation"), Session("Dept"), Session("UserJobgrade"), Session("UserJobtitle"), Session("UserEmpID"), search.Value.Trim)
        End If
        pagetitle.InnerText = "Training Library (" & strDataSet.Rows.Count.ToString & ")"
        Return strDataSet
    End Function

    Private Sub LoadTrainings()
        Try
            GridVwHeaderChckbox.PageIndex = CInt(Session("availableindex"))
            GridVwHeaderChckbox.DataSource = TrainingData()
            GridVwHeaderChckbox.AllowSorting = True
            GridVwHeaderChckbox.AllowPaging = True
            GridVwHeaderChckbox.DataBind()

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Read") = False And Process.AuthenAction(Session("role"), AuthenCode2, "Read") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "danger")

                Exit Sub
            End If


            If Not Me.IsPostBack Then
                If Session("availablesearch") Is Nothing Then
                    Session("availablesearch") = ""
                End If

                If Session("availableindex") Is Nothing Then
                    Session("availableindex") = "0"
                End If

                LoadTrainings()
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

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

    Private Sub GridVwHeaderChckbox_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridVwHeaderChckbox.RowCreated
        Try
            Process.SortArrow(e, SortsDirection, Session("availablesort"))
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub SortRecords(sender As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GridVwHeaderChckbox.Sorting
        Try
            Dim sortExpression As String = e.SortExpression
            Session("availablesort") = sortExpression
            Dim direction As String = String.Empty
            If SortsDirection = SortDirection.Ascending Then
                SortsDirection = SortDirection.Descending
                direction = " DESC"
            Else
                SortsDirection = SortDirection.Ascending
                direction = " ASC"
            End If
            Dim table As New DataTable
            table = TrainingData()
            table.DefaultView.Sort = sortExpression & direction
            GridVwHeaderChckbox.PageIndex = CInt(Session("availableindex"))
            GridVwHeaderChckbox.DataSource = table
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub GridVwHeaderChckbox_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridVwHeaderChckbox.PageIndexChanging
        Try
            Dim table As New DataTable
            'SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, script)
            table = TrainingData()
            Session("availableindex") = e.NewPageIndex
            GridVwHeaderChckbox.PageIndex = e.NewPageIndex
            GridVwHeaderChckbox.DataSource = table
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btnFind_Click(sender As Object, e As EventArgs)
        Try

            'If Not Me.IsPostBack Then
            If search.Value.Trim = "" Then
                Session("availablesearch") = ""
            Else
                Session("availablesearch") = search.Value.Trim
            End If
            LoadTrainings()
            'End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")            
        End Try
    End Sub

    Protected Sub GridVwHeaderChckbox_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridVwHeaderChckbox.RowDataBound

        'If e.Row.RowType = DataControlRowType.DataRow Then
        '    e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GridVwHeaderChckbox, "Select$" & e.Row.RowIndex)
        '    e.Row.ToolTip = "Click to select this row."
        'End If


    End Sub
    Private Sub OpenPage(sid As String)
        Try
            Dim url As String = ""
            If Request.QueryString("id") IsNot Nothing Then
                url = "LibraryDetail?id=" & sid
                Response.Redirect(url, True)
            Else
                url = "LibraryToInitiate?id=" & sid
                'Dim s As String = "window.open('" & url + "', 'popup_window', 'width=900,height=800,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no');"
                'ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)
                Response.Redirect(url, True)
            End If

            
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub GridVwHeaderChckbox_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridVwHeaderChckbox.RowCommand
        If (e.CommandName = "AddToCart") Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            OpenPage(index)
        End If
    End Sub
End Class