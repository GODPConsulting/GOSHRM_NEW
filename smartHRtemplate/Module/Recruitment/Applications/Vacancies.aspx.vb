Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports GOSHRM.GOSHRM.GOSHRM.BO
Public Class Vacancies
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "EMPLOAN"
    Dim marketrate As Double = 0
    Dim interestrate As Double = 0
    Dim monthlypay As Double = 0
    Dim loanamount As Double = 0
    Dim tenor As Integer = 0
    Dim fairvalue As Double = 0
    Dim EIR As Double = 0
    Dim AmortEIR As Double = 0
    Dim AmortFairValue As Double = 0
    Dim repaystartdate As Date
    Dim EMPID As String = ""
    Dim LoanType As String = ""
    Dim repayment As New clsRepaymentLoan
    Dim olddata(5) As String
    Private Function LodaDataTable() As DataTable
        Dim Datas As New DataTable
        Dim serach As String = ""
        serach = "Open"
        If Session("jobsearch") = "" Then
            Datas = Process.SearchData("Vacancies_Get_All", serach)
        Else
            search.Value = Session("jobsearch")
            Datas = Process.SearchDataP2("Vacancies_Search", serach, search.Value)
        End If
        Return Datas
    End Function
   
    Private Sub LoadData()
        Try
            GridVwHeaderChckbox.PageIndex = CInt(Session("jobpageindex"))
            GridVwHeaderChckbox.DataSource = LodaDataTable()
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
                If Session("jobsearch") Is Nothing Then
                    Session("jobsearch") = ""
                End If
                If Session("jobpageindex") Is Nothing Then
                    Session("jobpageindex") = "0"
                End If

                LoadData()

            End If
        Catch ex As Exception

            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub SortRecords(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Try
            Dim sortExpression As String = e.SortExpression
            Session("jobsort") = sortExpression
            Dim direction As String = String.Empty
            If SortsDirection = SortDirection.Ascending Then
                SortsDirection = SortDirection.Descending
                direction = " DESC"
            Else
                SortsDirection = SortDirection.Ascending
                direction = " ASC"
            End If
            Dim table As New DataTable
            table = LodaDataTable()
            GridVwHeaderChckbox.PageIndex = CInt(Session("jobpageindex"))
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
            Session("jobpageindex") = e.NewPageIndex
            GridVwHeaderChckbox.DataSource = LodaDataTable()
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
                Session("jobsearch") = ""
            Else
                Session("jobsearch") = search.Value.Trim
            End If
            LoadData()
        Catch ex As Exception
        End Try
    End Sub
End Class