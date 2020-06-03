Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Public Class AppraisalFeedBackNuggetsReview
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "APPRAISALNUGGETLISTREV"
    Private Function LoadDataTable() As DataTable
        Dim datatables As New DataTable
        If Session("appfeedbacklistLoadType") = "All" Then
            datatables = Process.SearchData("Performance_Appraisal_NuggetList_Review", Request.QueryString("id"))
        ElseIf Session("appfeedbacklistLoadType") = "Find" Then
            datatables = Process.SearchData("Performance_Appraisal_NuggetList_Review_Search", Request.QueryString("id"))
        End If
        'pagetitle.InnerText = " Appraisal Feedback (" & datatables.Rows.Count.ToString & ")"
        divdetailheader.InnerText = "Appraisal Feedback Nuggets Review Page"
        Dim customName As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select AppraisalFeedbackNugget from Performance_Custom_Naming")
        If (customName = "") Then
            divdetailheader.InnerText = " Appraisal Feedback Nuggets Review Page"
        Else
            divdetailheader.InnerText = customName + " Review Page"
        End If
        Return datatables
    End Function
    Private Sub LoadGrid()
        Try
            GridVwHeaderChckbox.PageIndex = CInt(Session("appfeedbacklistPageIndex"))
            GridVwHeaderChckbox.DataSource = LoadDataTable()
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
                Session("objPreviousPage") = Request.UrlReferrer.ToString
                If Request.QueryString("id") IsNot Nothing Then
                    If Session("appfeedbacklistLoadType") Is Nothing Then
                        Session("appfeedbacklistLoadType") = "All"
                    End If

                    If Session("appfeedbacklistPageIndex") Is Nothing Then
                        Session("appfeedbacklistPageIndex") = "0"
                    End If
                    LoadGrid()
                End If

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Protected Sub SortRecords(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Try
            Dim sortExpression As String = e.SortExpression
            Session("appfeedbacklistsortExpression") = sortExpression
            Dim direction As String = String.Empty
            If SortsDirection = SortDirection.Ascending Then
                SortsDirection = SortDirection.Descending
                direction = " DESC"
            Else
                SortsDirection = SortDirection.Ascending
                direction = " ASC"
            End If
            Dim table As DataTable = LoadDataTable()
            table.DefaultView.Sort = sortExpression & direction
            GridVwHeaderChckbox.PageIndex = CInt(Session("appfeedbacklistPageIndex"))
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
            Session("appfeedbacklistPageIndex") = e.NewPageIndex
            GridVwHeaderChckbox.DataSource = LoadDataTable()
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


    'Protected Sub Delete(sender As Object, e As EventArgs) Handles btnDelete.Click
    '    Try
    '        Dim count As Integer = 0
    '        'If Process.AuthenAction(Session("role"), AuthenCode, "Delete") = False Then
    '        '    Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "warning")
    '        '    Exit Sub
    '        'End If
    '        Dim confirmValue As String = Request.Form("confirm_value")
    '        If confirmValue = "Yes" Then
    '            Dim atLeastOneRowDeleted As Boolean = False
    '            ' Iterate through the Products.Rows property
    '            For Each row As GridViewRow In GridVwHeaderChckbox.Rows
    '                ' Access the CheckBox
    '                Dim cb As CheckBox = row.FindControl("chkEmp")
    '                If cb IsNot Nothing AndAlso cb.Checked Then
    '                    count = count + 1
    '                    ' Delete row! (Well, not really...)
    '                    atLeastOneRowDeleted = True
    '                    ' First, get the ProductID for the selected row
    '                    Dim ID As String = _
    '                        Convert.ToString(GridVwHeaderChckbox.DataKeys(row.RowIndex).Value)
    '                    ' "Delete" the row
    '                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_Nugget_Delete", ID)
    '                End If
    '            Next
    '            Process.loadalert(divalert, msgalert, count.ToString & " records successfully deleted", "success")
    '            LoadGrid()

    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs)
        Try
            If Session("objPreviousPage") = "" Then
                Response.Redirect("AppraisalFeedBackNuggetsManagerList")
            Else
                Response.Redirect(Session("objPreviousPage"))
            End If

        Catch ex As Exception

        End Try
    End Sub


    Private Sub GridVwHeaderChckbox_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridVwHeaderChckbox.RowCreated
        Try
            Process.SortArrow(e, SortsDirection, Session("appfeedbacklistsortExpression"))
        Catch ex As Exception

        End Try
    End Sub
End Class