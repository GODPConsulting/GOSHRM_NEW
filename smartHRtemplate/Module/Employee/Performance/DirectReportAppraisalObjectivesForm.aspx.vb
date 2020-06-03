Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports Telerik.Web.UI
Public Class DirectReportAppraisalObjectivesForm
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "TEAMPERF"
 
    Private Function LoadSurbodinateGrid() As DataTable
        Dim datatables As New DataTable
        If Session("directrepappLoadType") = "All" Then
            datatables = Process.SearchDataP2("Performance_Appraisal_Summary_Surbodinate_Get_All", Session("UserEmpID"), cboYear.SelectedValue)
        ElseIf Session("directrepappLoadType") = "Find" Then
            search.Value = Session("directrepappSearch")
            datatables = Process.SearchDataP3("Performance_Appraisal_Summary_Surbodinate_Search", Session("UserEmpID"), search.Value, cboYear.SelectedValue)
        End If
        pagetitle.InnerText = cboYear.SelectedValue & ": Direct-Report Appraisal (" & datatables.Rows.Count.ToString & ")"
        Return datatables
    End Function
    Private Sub LoadSurbodinate()
        Try
            gridSurbodinate.PageIndex = CInt(Session("directrepappPageIndex"))
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
                Process.LoadRadComboTextAndValueP1(cboYear, "Performance_Appraisal_Summary_ReviewYear", Session("UserEmpID"), "ReviewYear", "ReviewYear", False)

                Dim reviewyear As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select ReviewYear from Performance_Appraisal_Cycle a where a.company = '" + Session("Organisation") + "' and a.status = 'Open'")
                If Session("directrepappReview") Is Nothing Then
                    If cboYear.Items.Count > 0 Then
                        'Session("directrepappReview") = cboYear.SelectedValue
                        'Dim yr As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Performance_Appraisal_Summary_ReviewYear_status", Session("UserEmpID"))
                        Session("directrepappReview") = reviewyear
                    End If
                    Process.AssignRadComboValue(cboYear, Session("directrepappReview"))
                Else
                    Process.AssignRadComboValue(cboYear, Session("directrepappReview"))
                End If

                If Session("directrepappLoadType") Is Nothing Then
                    Session("directrepappLoadType") = "All"
                End If

                If Session("directrepappPageIndex") Is Nothing Then
                    Session("directrepappPageIndex") = "0"
                End If



                LoadSurbodinate()

            End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Private Sub gridSurbodinate_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridSurbodinate.RowCreated
        Try
            Process.SortArrow(e, SortsDirection, Session("directrepappsortExpression"))
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub SortRecords(ByVal sender As Object, ByVal e As GridViewSortEventArgs) Handles gridSurbodinate.Sorting
        Try
            Dim sortExpression As String = e.SortExpression
            Session("directrepappsortExpression") = sortExpression
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
            gridSurbodinate.PageIndex = CInt(Session("directrepappPageIndex"))
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


    Protected Sub OnRowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gridSurbodinate, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub

    Protected Sub gridSurbodinate_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gridSurbodinate.PageIndexChanging
        Try
            gridSurbodinate.PageIndex = e.NewPageIndex
            Session("directrepappPageIndex") = e.NewPageIndex
            gridSurbodinate.DataSource = LoadSurbodinateGrid()
            gridSurbodinate.DataBind()
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub btnFind_Click(sender As Object, e As EventArgs)
        If search.Value.Trim = "" Then
            Session("directrepappLoadType") = "All"
        Else
            Session("directrepappLoadType") = "Find"
        End If
        Session("directrepappSearch") = search.Value.Trim()
        Session("directrepappPageIndex") = "0"
        LoadSurbodinate()

    End Sub


    Private Sub cboYear_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboYear.SelectedIndexChanged
        Try
            Session("directrepappReview") = cboYear.SelectedValue
            LoadSurbodinate()
        Catch ex As Exception

        End Try
    End Sub
End Class