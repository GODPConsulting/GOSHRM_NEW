Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Public Class StaffRequisitionApprove
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "APPSTAFFREQUISITE"


    Private Function LoadApprovalDataTable() As DataTable
        Dim Datas As New DataTable
        search.Value = Session("mgrrequisitionSearch")
        If search.Value.Trim = "" Then
            Datas = Process.SearchDataP2("Recruit_Job_Requisition_Approval_all", Session("UserEmpID"), cboStatus.SelectedItem.Text)
        Else
            Datas = Process.SearchDataP3("Recruit_Job_Requisition_Approval_Search", Session("UserEmpID"), cboStatus.SelectedItem.Text, search.Value.Trim)
        End If
        If cboStatus.SelectedItem.Text.ToLower = "approved" Then
            pagetitle.InnerText = cboStatus.SelectedItem.Text & " Staff Requisition (" & Datas.Rows.Count & ")"
        Else
            pagetitle.InnerText = cboStatus.SelectedItem.Text & " Staff Requisition for Approval (" & Datas.Rows.Count & ")"
        End If

        Return Datas
    End Function

    Private Sub LoadApprovalGrid()
        Try
            GridVwHeaderChckbox.PageIndex = CInt(Session("mgrrequisitionPageIndex"))
            GridVwHeaderChckbox.DataSource = LoadApprovalDataTable()
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
                If Session("mgrrequisitionStatus") Is Nothing Then
                    Session("mgrrequisitionStatus") = "Pending"
                End If

                Process.AssignRadComboValue(cboStatus, Session("mgrrequisitionStatus"))

                If Session("mgrrequisitionSearch") Is Nothing Then
                    Session("mgrrequisitionSearch") = ""
                End If

                If Session("mgrrequisitionPageIndex") Is Nothing Then
                    Session("mgrrequisitionPageIndex") = "0"
                End If

                LoadApprovalGrid()
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Private Sub GridVwHeaderChckbox_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridVwHeaderChckbox.RowCreated
        Try
            Process.SortArrow(e, SortsDirection, Session("mgrrequisitionsort"))
        Catch ex As Exception
        End Try

    End Sub


    Protected Sub SortRecords(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Try
            Dim sortExpression As String = e.SortExpression
            Session("mgrrequisitionsort") = sortExpression
            Dim direction As String = String.Empty
            If SortsDirection = SortDirection.Ascending Then
                SortsDirection = SortDirection.Descending
                direction = " DESC"
            Else
                SortsDirection = SortDirection.Ascending
                direction = " ASC"
            End If
            Dim table As DataTable = LoadApprovalDataTable() 'Process.GetData("Recruit_Job_Requisition_get_all")
            table.DefaultView.Sort = sortExpression & direction
            GridVwHeaderChckbox.PageIndex = CInt(Session("mgrrequisitionPageIndex"))
            Me.GridVwHeaderChckbox.DataSource = table
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
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
            Session("mgrrequisitionPageIndex") = e.NewPageIndex
            LoadApprovalGrid()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


    Protected Sub OnRowDataBoundApprove(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GridVwHeaderChckbox, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub




    Protected Sub btnFind_Click(sender As Object, e As EventArgs)
        Try
            'If Not Me.IsPostBack Then
            Session("mgrrequisitionSearch") = search.Value.Trim
            LoadApprovalGrid()

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub
End Class