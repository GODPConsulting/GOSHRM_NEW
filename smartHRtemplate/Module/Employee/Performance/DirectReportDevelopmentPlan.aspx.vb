Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Public Class DirectReportDevelopmentPlan
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "TEAMDEV"


    Private Function LoadSurbodinateGrid() As DataTable
        Dim datatables As New DataTable
        If Session("directrepplanLoadType") = "All" Then
            datatables = Process.SearchData("Performance_Development_Plan_Get_Surbodinate", Session("UserEmpID"))
        ElseIf Session("directrepplanLoadType") = "Find" Then
            search.Value = Session("directrepplanSearch")
            datatables = Process.SearchDataP2("Performance_Development_Plan_Search_Surbodinate", Session("UserEmpID"), search.Value.Trim)
        End If
        pagetitle.InnerText = "Team 's Development Plan(" & datatables.Rows.Count & ")"
        Return datatables
    End Function
    Private Sub LoadSurbodinate()
        Try
            gridSurbodinate.PageIndex = CInt(Session("directrepplanPageIndex"))
            gridSurbodinate.DataSource = LoadSurbodinateGrid()
            gridSurbodinate.AllowSorting = True
            gridSurbodinate.AllowPaging = True
            gridSurbodinate.DataBind()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not Me.IsPostBack Then
                Process.LoadRadComboTextAndValueP1(cboYear, "Performance_Development_Plan_Get_Year", Session("UserEmpID"), "planyear", "planyear", False)

                If Session("directrepdevPlanYear") Is Nothing Then
                    If cboYear.Items.Count > 0 Then
                        Session("directrepdevPlanYear") = cboYear.SelectedValue
                    End If
                Else
                    Process.AssignRadComboValue(cboYear, Session("directrepdevPlanYear"))
                End If

                If Session("directrepplanLoadType") Is Nothing Then
                    Session("directrepplanLoadType") = "All"
                End If

                If Session("directrepplanPageIndex") Is Nothing Then
                    Session("directrepplanPageIndex") = "0"
                End If
                LoadSurbodinate()
            End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Private Sub gridSurbodinate_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridSurbodinate.RowCreated
        Try
            Process.SortArrow(e, SortsDirection, Session("directrepplanSort"))
        Catch ex As Exception
        End Try
    End Sub


    Protected Sub SortRecords(ByVal sender As Object, ByVal e As GridViewSortEventArgs) Handles gridSurbodinate.Sorting
        Try
            Dim sortExpression As String = e.SortExpression
            Session("directrepplanSort") = sortExpression
            Dim direction As String = String.Empty
            If SortsDirection = SortDirection.Ascending Then
                SortsDirection = SortDirection.Descending
                direction = " DESC"
            Else
                SortsDirection = SortDirection.Ascending
                direction = " ASC"
            End If
            Dim table As DataTable = LoadSurbodinateGrid()
            table.DefaultView.Sort = sortExpression & direction
            gridSurbodinate.PageIndex = CInt(Session("directrepplanPageIndex"))
            gridSurbodinate.DataSource = table
            gridSurbodinate.DataBind()
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

    Protected Sub OnRowDataBound1(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gridSurbodinate, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub

    Protected Sub gridSurbodinate_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gridSurbodinate.PageIndexChanging
        Try
            gridSurbodinate.PageIndex = e.NewPageIndex
            Session("directrepplanPageIndex") = e.NewPageIndex
            gridSurbodinate.DataSource = LoadSurbodinateGrid(Session("directrepplanLoadType"))
            gridSurbodinate.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btnFind_Click(sender As Object, e As EventArgs)
        If search.Value.Trim = "" Then
            Session("directrepplanLoadType") = "All"
        Else
            Session("directrepplanLoadType") = "Find"
        End If

        search.Value = Session("directrepplanSearch")

        Session("directrepplanPageIndex") = 0
        LoadSurbodinate()
    End Sub


    Private Sub cboYear_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboYear.SelectedIndexChanged
        Try
            Session("directrepdevPlanYear") = cboYear.SelectedValue
            LoadSurbodinate()
        Catch ex As Exception
        End Try
    End Sub
End Class