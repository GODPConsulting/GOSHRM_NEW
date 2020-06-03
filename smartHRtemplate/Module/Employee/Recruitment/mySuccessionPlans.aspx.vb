Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Imports Telerik.Web.UI
Imports System.Web.UI
Imports System.Threading


Public Class mySuccessionPlans
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "MYSUCCESSIONS"
    Dim Pages As String = "Succession Plan"
    Dim PhotoURL As String = ConfigurationManager.AppSettings("PhotoURL")
    Dim FileURL As String = ConfigurationManager.AppSettings("FileURL")
    Dim sampleCSV As String = ConfigurationManager.AppSettings("sampleCSV")
    Public Shared Separator() As Char = {"."c}
    Private Function LoadData() As DataTable
        Dim datatables As New DataTable
        search.Value = Session("mysuccessionsearch")
        If Session("mysuccessionsearch") = "" Then
            datatables = Process.SearchData("Recruitment_Succession_Get_All", Session("UserEmpID"))
        Else
            datatables = Process.SearchDataP2("Recruitment_Succession_Search", Session("UserEmpID"), search.Value)
        End If

        pagetitle.InnerText = "Succession Plan (" & datatables.Rows.Count & ")"

        Return datatables
    End Function

    Private Sub LoadGrid()
        Try

            GridVwHeaderChckbox.PageIndex = CInt(Session("mysuccessionindex"))
            GridVwHeaderChckbox.DataSource = LoadData()
            GridVwHeaderChckbox.AllowSorting = True
            GridVwHeaderChckbox.AllowPaging = True

            GridVwHeaderChckbox.DataBind()

            'GridVwHeaderChckbox.Columns(2).Visible = False
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Read") = False Then
                divshow.Style.Add("display", "none")
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "danger")
                Exit Sub
            End If


            If Not Me.IsPostBack Then

                If Session("mysuccessionindex") Is Nothing Then
                    Session("mysuccessionindex") = "0"
                End If

                If Session("mysuccessionsearch") Is Nothing Then
                    Session("mysuccessionsearch") = ""
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
            Session("mysuccessionsort") = sortExpression
            Dim direction As String = String.Empty
            If SortsDirection = SortDirection.Ascending Then
                SortsDirection = SortDirection.Descending
                direction = " DESC"
            Else
                SortsDirection = SortDirection.Ascending
                direction = " ASC"
            End If
            Dim table As New DataTable
            table = LoadData()
            table.DefaultView.Sort = sortExpression & direction
            GridVwHeaderChckbox.PageIndex = CInt(Session("mysuccessionindex"))
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
            Session("mysuccessionindex") = e.NewPageIndex
            GridVwHeaderChckbox.DataSource = LoadData()
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub GridVwHeaderChckbox_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridVwHeaderChckbox.RowCreated
        Try
            Process.SortArrow(e, SortsDirection, Session("mysuccessionsort"))
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
            'If Not Me.IsPostBack Then

            If search.Value.Trim = "" Then
                Session("mysuccessionsearch") = ""
            Else
                Session("mysuccessionsearch") = search.Value.Trim
            End If

            LoadGrid()
            'End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub





    Protected Sub btnExport_Click(sender As Object, e As EventArgs)
        Try

            Dim filename As String = ""
            Dim empIDList As String = ""

            Dim TitleHeader As String()
            Dim TitleData As String()

            Dim scripts As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Scripts_get", "successionplan")
            For i As Integer = 0 To GridVwHeaderChckbox.PageCount - 1
                GridVwHeaderChckbox.PageIndex = i
                For j As Integer = 0 To GridVwHeaderChckbox.Rows.Count - 1
                    Dim controls As HyperLink = DirectCast(GridVwHeaderChckbox.Rows(j).Cells(2).FindControl("HyperLink1"), HyperLink)
                    If i = 0 And j = 0 Then
                        empIDList = "'" & controls.Text.Replace(",", "") & "'"
                    Else
                        empIDList = empIDList & "," & "'" & controls.Text.Replace(",", "") & "'"
                    End If
                Next
            Next
            If empIDList = "" Then
                empIDList = "''"
            End If
            scripts = scripts.Replace("@empid", empIDList)
            Dim strDataSet As New DataSet
            strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, scripts)
            Dim dataTables As DataTable = strDataSet.Tables(0)
            'Dim Columns(dataTables.Columns.Count - 1) As String
            'Dim ColumnHeaders(dataTables.Columns.Count - 1) As String

            'For i As Integer = 0 To dataTables.Columns.Count - 1
            '    ColumnHeaders(i) = dataTables.Columns(i).ColumnName
            '    Columns(i) = dataTables.Columns(i).ColumnName
            'Next
            filename = "_succession"
            'Process.ExportDataSet(TitleHeader, TitleData, dataTables, ColumnHeaders, Columns, filename)

            If Process.ExportExcel(dataTables, filename) = False Then
                Response.Write(Process.strExp)
            Else
                Response.Write("File saved as " & filename & ".csv")
            End If

            'Response.Write("File saved as " & filename & ".csv")


        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub
End Class