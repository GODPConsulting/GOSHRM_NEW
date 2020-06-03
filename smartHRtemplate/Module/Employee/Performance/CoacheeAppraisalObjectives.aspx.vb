Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Public Class CoacheeAppraisalObjectives
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "APPPERF"


    Private Function LoadCoachGrid() As DataTable
        Dim datatables As New DataTable
        If Session("coachappobjLoadType") = "All" Then
            datatables = Process.SearchDataP2("Performance_Appraisal_Summary_Coach_Get_All", Session("UserEmpID"), cboYear.SelectedValue)
        ElseIf Session("coachappobjLoadType") = "Find" Then
            search.Value = Session("coachappobjSearch")
            datatables = Process.SearchDataP3("Performance_Appraisal_Summary_Coach_Search", Session("UserEmpID"), cboYear.SelectedValue, search.Value.Trim)
        End If
        pagetitle.InnerText = cboYear.SelectedValue.ToString & " Appraisal Objective (" & datatables.Rows.Count.ToString & ")"
        Return datatables
    End Function
    Private Sub LoadCoach()
        Try

            gridCoach.PageIndex = CInt(Session("coachappobjPageIndex"))
            gridCoach.DataSource = LoadCoachGrid()
            gridCoach.AllowSorting = True
            gridCoach.AllowPaging = True
            gridCoach.DataBind()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")            
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not Me.IsPostBack Then
                Process.LoadRadComboTextAndValueP1(cboYear, "Performance_Appraisal_Summary_ReviewYear", Session("UserEmpID"), "ReviewYear", "ReviewYear", False)

                Dim reviewyear As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select ReviewYear from Performance_Appraisal_Cycle a where a.company = '" + Session("Organisation") + "' and a.status = 'Open'")
                If Session("appCoachObjReview") Is Nothing Then
                    If cboYear.Items.Count > 0 Then
                        'Session("appCoachObjReview") = cboYear.SelectedValue
                        Session("appCoachObjReview") = reviewyear
                        Process.AssignRadComboValue(cboYear, Session("appCoachObjReview"))
                    End If
                Else
                    Process.AssignRadComboValue(cboYear, Session("appCoachObjReview"))
                End If

                If Session("coachappobjLoadType") Is Nothing Then
                    Session("coachappobjLoadType") = "All"
                End If

                If Session("coachappobjPageIndex") Is Nothing Then
                    Session("coachappobjPageIndex") = "0"
                End If


                LoadCoach()
            End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Private Sub gridCoach_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridCoach.RowCreated
        Try
            Process.SortArrow(e, SortsDirection, Session("coachappobjsortExpression"))
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub SortRecords(ByVal sender As Object, ByVal e As GridViewSortEventArgs) Handles gridCoach.Sorting
        Try
            Dim sortExpression As String = e.SortExpression
            Session("coachappobjsortExpression") = sortExpression
            Dim direction As String = String.Empty
            If SortsDirection = SortDirection.Ascending Then
                SortsDirection = SortDirection.Descending
                direction = " DESC"
            Else
                SortsDirection = SortDirection.Ascending
                direction = " ASC"
            End If
            Dim table As DataTable = LoadCoachGrid()
            table.DefaultView.Sort = sortExpression & direction
            gridCoach.PageIndex = CInt(Session("coachappobjPageIndex"))
            gridCoach.DataSource = table
            gridCoach.DataBind()
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



    Protected Sub btnFind_Click(sender As Object, e As EventArgs)
        If search.Value.Trim = "" Then
            Session("coachappobjLoadType") = "All"
        Else
            Session("coachappobjLoadType") = "Find"
        End If
        Session("coachappobjSearch") = search.Value
        Session("coachappobjPageIndex") = "0"
        LoadCoach()
    End Sub

    Protected Sub gridCoach_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gridCoach.PageIndexChanging
        Try
            gridCoach.PageIndex = e.NewPageIndex
            Session("coachappobjPageIndex") = e.NewPageIndex
            gridCoach.DataSource = LoadCoachGrid()
            gridCoach.DataBind()
        Catch ex As Exception
        End Try
    End Sub


    Private Sub cboYear_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboYear.SelectedIndexChanged
        Try
            Session("appCoachObjReview") = cboYear.SelectedValue
            LoadCoach()
        Catch ex As Exception

        End Try
    End Sub
End Class