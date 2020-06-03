Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Imports Telerik.Web.UI
Imports System.Web.UI
Imports System.Threading


Public Class SuccessionPlansApprove
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "APPSUCCESSION"
    Dim Pages As String = "Succession Plan"
    Dim PhotoURL As String = ConfigurationManager.AppSettings("PhotoURL")
    Dim FileURL As String = ConfigurationManager.AppSettings("FileURL")
    Dim sampleCSV As String = ConfigurationManager.AppSettings("sampleCSV")
    Public Shared Separator() As Char = {"."c}
    Private Function LoadEmployeeGrid() As DataTable
        Dim datatables As New DataTable
        search.Value = Session("appsuccplansearch").ToString.Trim
        If search.Value.Trim = "" Then
            datatables = Process.SearchData("Recruitment_Succession_Get_All_Approver", Session("UserEmpID"))
        Else
            datatables = Process.SearchDataP2("Recruitment_Succession_Search_Approver", Session("UserEmpID"), search.Value.Trim)
        End If

        pagetitle.InnerText = "Approvals : Succession Plan (" & datatables.Rows.Count & ")"
        Return datatables
    End Function

    Private Sub LoadGrid()
        Try
            GridVwHeaderChckbox.PageIndex = CInt(Session("appsuccplanindex"))
            GridVwHeaderChckbox.DataSource = LoadEmployeeGrid()
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
                Process.loadalert(divalert, msgalert, Process.privilegemsg, "warning")                
                Exit Sub
            End If


            If Not Me.IsPostBack Then
                If Session("appsuccplansearch") Is Nothing Then
                    Session("appsuccplansearch") = ""
                End If

                If Session("appsuccplanindex") Is Nothing Then
                    Session("appsuccplanindex") = "0"
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
            Session("appsuccplansort") = sortExpression
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
            GridVwHeaderChckbox.PageIndex = CInt(Session("appsuccplanindex"))
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
            Session("appsuccplanindex") = e.NewPageIndex
            LoadGrid()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub GridVwHeaderChckbox_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridVwHeaderChckbox.RowCreated
        'Session("appsuccplanindex")
        Try
            Process.SortArrow(e, SortsDirection, Session("appsuccplanindex"))
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
            Session("appsuccplansearch") = search.Value.Trim
            LoadGrid()
            'End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

End Class