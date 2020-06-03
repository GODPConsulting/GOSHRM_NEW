Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Imports Telerik.Web.UI
Imports System.Web.UI
Imports System.Threading


Public Class PromotionsApproval
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "APPPROMOTIONS"
    Dim Pages As String = "Promotions"
    Dim PhotoURL As String = ConfigurationManager.AppSettings("PhotoURL")
    Dim FileURL As String = ConfigurationManager.AppSettings("FileURL")
    Dim sampleCSV As String = ConfigurationManager.AppSettings("sampleCSV")
    Public Shared Separator() As Char = {"."c}
    Private Function LoadEmployeeGrid() As DataTable
        Dim datatables As New DataTable
        search.Value = Session("apppromosearch")
        If search.Value.Trim = "" Then
            datatables = Process.SearchData("Recruitment_Promotion_Approver_Get_All", Session("UserEmpID"))
        Else
            datatables = Process.SearchDataP3("Recruitment_Promotion_Approver_Search", Session("UserEmpID"), cboStatus.SelectedValue, search.Value.Trim)
        End If

        If cboStatus.SelectedItem.Text.ToLower = "approved" Then
            pagetitle.InnerText = cboStatus.SelectedItem.Text & " Promotions (" & datatables.Rows.Count & ")"
        Else
            pagetitle.InnerText = cboStatus.SelectedItem.Text & " Promotions for Approval (" & datatables.Rows.Count & ")"
        End If

        Return datatables
    End Function

    Private Sub LoadGrid()
        Try

            GridVwHeaderChckbox.PageIndex = CInt(Session("apppromoindex"))
            GridVwHeaderChckbox.DataSource = LoadEmployeeGrid()
            GridVwHeaderChckbox.DataBind()

            'GridVwHeaderChckbox.Columns(2).Visible = False
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Read") = False Then
                Process.loadalert(divalert, msgalert, Process.privilegemsg, "warning")

                Exit Sub
            End If


            If Not Me.IsPostBack Then
                If Session("apppromostatus") Is Nothing Then
                    Session("apppromostatus") = "Pending"
                End If
                Process.AssignRadComboValue(cboStatus, Session("apppromostatus"))
                If Session("apppromosearch") Is Nothing Then
                    Session("apppromosearch") = ""
                End If

                If Session("apppromoindex") Is Nothing Then
                    Session("apppromoindex") = "0"
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
            Session("apppromosort") = sortExpression
            Dim direction As String = String.Empty
            If SortsDirection = SortDirection.Ascending Then
                SortsDirection = SortDirection.Descending
                direction = " DESC"
            Else
                SortsDirection = SortDirection.Ascending
                direction = " ASC"
            End If
            Dim table As New DataTable
            table = LoadEmployeeGrid()
            table.DefaultView.Sort = sortExpression & direction
            GridVwHeaderChckbox.PageIndex = CInt(Session("apppromoindex"))
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
            Session("apppromoindex") = e.NewPageIndex
            LoadGrid()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub GridVwHeaderChckbox_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridVwHeaderChckbox.RowCreated
        Try
            Process.SortArrow(e, SortsDirection, Session("apppromosort"))
        Catch ex As Exception
        End Try

    End Sub

    Protected Sub OnRowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridVwHeaderChckbox.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GridVwHeaderChckbox, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub

    Protected Sub btnFind_Click(sender As Object, e As EventArgs)
        Try
            Session("apppromosearch") = search.Value.Trim
            LoadGrid()
            'End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


    Private Sub cboStatus_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboStatus.SelectedIndexChanged
        Try
            Session("apppromostatus") = cboStatus.SelectedValue
            LoadGrid()
        Catch ex As Exception

        End Try
    End Sub
End Class