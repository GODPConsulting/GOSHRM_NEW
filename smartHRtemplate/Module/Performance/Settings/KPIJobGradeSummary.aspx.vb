Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO


Public Class KPIJobGradeSummary
    Inherits System.Web.UI.Page
    Dim skills As New clsSkills
    Dim AuthenCode As String = "SKILL"
    Dim olddata(3) As String
    Private Function LoadTable() As DataTable
        Dim datatables As New DataTable
        datatables = Process.GetData("Competency_JobGrade_Summary")
        Return datatables
    End Function
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                'Company_Structure_get_parent
                GridVwHeaderChckbox.DataSource = LoadTable()
                GridVwHeaderChckbox.AllowSorting = True
                GridVwHeaderChckbox.AllowPaging = True
                GridVwHeaderChckbox.DataBind()
            End If
        Catch ex As Exception
        End Try
    End Sub

    

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect("~/Module/Performance/Settings/KPIJobGrade.aspx", True)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridVwHeaderChckbox_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridVwHeaderChckbox.PageIndexChanging
        Try
            GridVwHeaderChckbox.DataSource = LoadTable()
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub OnRowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridVwHeaderChckbox.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim quantity As Double = Double.Parse(e.Row.Cells(2).Text)

                For Each cell As TableCell In e.Row.Cells                    
                    If quantity = 0 Then
                        'cell.BackColor = Color.Red
                        e.Row.Cells(2).BackColor = Color.Red
                    ElseIf quantity > 0 AndAlso quantity <= 99.9 Then
                        'cell.BackColor = Color.Yellow
                        e.Row.Cells(2).BackColor = Color.Yellow
                    ElseIf quantity > 99.9 AndAlso quantity <= 100 Then
                        'cell.BackColor = Color.LawnGreen
                        e.Row.Cells(2).BackColor = Color.LawnGreen
                    Else
                        'cell.BackColor = Color.Red
                        e.Row.Cells(2).BackColor = Color.Red
                    End If

                Next
            End If
        Catch ex As Exception

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
            Dim table As DataTable = LoadTable()
            table.DefaultView.Sort = sortExpression & direction
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
End Class