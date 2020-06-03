Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Public Class FeedBack360Request
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "APP360FEEDBACK"
    Private Function LoadDataTable() As DataTable
        Dim datatables As New DataTable
        search.Value = Session("feedbacksearch")
        If Session("feedbacksearch") = "" Then
            datatables = Process.SearchDataP2("Performance_Appraisal_360_Reviewer_Get_All", Session("UserEmpID"), Session("appcycleid"))
        Else
            datatables = Process.SearchDataP3("Performance_Appraisal_360_Reviewer_Search", Session("UserEmpID"), Session("appcycleid"), search.Value)
        End If
        'pagetitle.InnerText = Session("company") & " " & cboperiod.SelectedItem.Text & ": 360 Appraisal Feedback (" & datatables.Rows.Count.ToString & ")"
        divdetailheader.InnerText = Session("company") & " " & cboperiod.SelectedItem.Text & ": 360 Appraisal Feedback (" & datatables.Rows.Count.ToString & ")"
        Return datatables
    End Function
    Private Sub LoadGrid()
        Try
            GridVwHeaderChckbox.PageIndex = CInt(Session("feedbackindex"))
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
                If Process.AuthenAction(Session("role"), AuthenCode, "Read") = False Then
                    Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "warning")
                    Exit Sub
                End If

                Dim ismulti As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select isnull(ismulticompany,'No')  from general_info")

                If ismulti.ToLower = "no" Then
                    Process.LoadRadComboTextAndValueP2(cbocompany, "Company_Structure_Get_ByLevel", "1", Process.GetCompanyName(""), "name", "name", False)
                Else
                    Process.LoadRadComboTextAndValueP2(cbocompany, "Company_Structure_Get_ByLevel", "2", Process.GetCompanyName(""), "name", "name", False)
                End If
                If Session("company") Is Nothing Then
                    Session("company") = Session("Organisation")
                End If
                Process.AssignRadComboValue(cbocompany, Session("company"))
                Process.LoadRadComboTextAndValueP1(cboPeriod, "Performance_Appraisal_Cycle_Get_All", cbocompany.SelectedValue, "period", "id", False)

                If Session("appcycleid") Is Nothing Then
                    Session("appcycleid") = cboperiod.SelectedValue
                Else
                    Process.AssignRadComboValue(cboperiod, Session("appcycleid"))
                End If

                If Session("feedbacksearch") Is Nothing Then
                    Session("feedbacksearch") = ""
                End If

                If Session("feedbackindex") Is Nothing Then
                    Session("feedbackindex") = "0"
                End If

                LoadGrid()
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Protected Sub SortRecords(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Try
            Dim sortExpression As String = e.SortExpression
            Session("feedbacksort") = sortExpression
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
            Session("feedbackindex") = e.NewPageIndex
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

    Protected Sub btnFind_Click(sender As Object, e As EventArgs)
        Try
            If search.Value.Trim = "" Then
                Session("feedbacksearch") = ""
            Else
                Session("feedbacksearch") = search.Value.Trim
            End If
            LoadGrid()
            'End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


    Private Sub cbocompany_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cbocompany.SelectedIndexChanged
        Try
            Session("company") = cbocompany.SelectedValue
            Process.LoadRadComboTextAndValueP1(cboPeriod, "Performance_Appraisal_Cycle_Get_All", cbocompany.SelectedValue, "period", "id", False)
            LoadGrid()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Private Sub cboPeriod_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboPeriod.SelectedIndexChanged
        Try
            Session("appcycleid") = cboPeriod.SelectedValue
            LoadGrid()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Private Sub GridVwHeaderChckbox_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridVwHeaderChckbox.RowCreated
        Try
            Process.SortArrow(e, SortsDirection, Session("feedbacksort"))
        Catch ex As Exception

        End Try
    End Sub
End Class