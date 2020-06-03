Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Public Class CoacheeDevelopmentPlan
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "APPDEV"
  

    Private Function LoadCoachGrid() As DataTable
        Dim datatables As New DataTable

        If Session("coachdevplanLoadType") = "All" Then
            datatables = Process.SearchDataP2("Performance_Development_Plan_Get_Coach", Session("UserEmpID"), radStatus.SelectedItem.Text)
        ElseIf Session("coachdevplanLoadType") = "Find" Then
            search.Value = Session("coachdevplanSearch")
            datatables = Process.SearchDataP3("Performance_Development_Plan_Get_Coach_Search", Session("UserEmpID"), radStatus.SelectedItem.Text, search.Value.Trim)
        End If

        pagetitle.InnerText = "Approvals: " & radStatus.SelectedItem.Text & " Development Plan(" & datatables.Rows.Count & ")"
        Return datatables
    End Function
    Private Sub LoadCoach()
        Try
            gridCoach.PageIndex = CInt(Session("coachdevplanPageIndex"))
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
                radStatus.Items.Clear()
                radStatus.Items.Add("Pending")
                radStatus.Items.Add("Discussed & Agreed")

                If Session("coachdevplanLoadType") Is Nothing Then
                    Session("coachdevplanLoadType") = "All"
                End If

                If Session("coachdevplanPageIndex") Is Nothing Then
                    Session("coachdevplanPageIndex") = "0"
                End If

                If Session("coachdevplanStat") Is Nothing Then
                    Session("coachdevplanStat") = "Pending"
                End If
                Process.AssignRadComboValue(radStatus, Session("coachdevplanStat"))
                LoadCoach()

            End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")            
        End Try
    End Sub

    Private Sub gridCoach_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridCoach.RowCreated
        Try
            Process.SortArrow(e, SortsDirection, Session("coachdevplansortExpress"))
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub SortRecords(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Try
            Dim sortExpression As String = e.SortExpression
            Session("coachdevplansortExpress") = sortExpression
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
            gridCoach.PageIndex = CInt(Session("coachdevplanPageIndex"))
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
        Session("coachdevplanStat") = radStatus.SelectedItem.Text
        Session("coachdevplanSearch") = search.Value.Trim
        If search.Value.Trim = "" Then
            Session("coachdevplanLoadType") = "All"
        Else
            Session("coachdevplanLoadType") = "Find"
        End If
        Session("coachdevplanPageIndex") = "0"
        LoadCoach()
    End Sub



    Protected Sub gridCoach_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gridCoach.PageIndexChanging
        Try
            gridCoach.PageIndex = e.NewPageIndex
            Session("coachdevplanPageIndex") = e.NewPageIndex
            gridCoach.DataSource = LoadCoachGrid()
            gridCoach.DataBind()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


    Private Sub radStatus_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles radStatus.SelectedIndexChanged
        Try
            Session("coachdevplanStat") = radStatus.SelectedItem.Text
            LoadCoach()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
End Class