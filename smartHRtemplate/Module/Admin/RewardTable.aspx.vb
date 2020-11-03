Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Public Class RewardTable
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "EMPTERMINATE"
    Private Function LoadTermData() As DataTable
        search.Value = Session("termsearch")
        Dim datatables As New DataTable
        If search.Value.Trim = "" Then
            datatables = Process.SearchData("Job_grade_Reward_Get_All", radStatus.SelectedItem.Text)
        Else
            datatables = Process.SearchDataP2("Job_grade_Reward_Get_All_Search", search.Value.Trim, radStatus.SelectedItem.Text)
        End If
        pagetitle.InnerText = Session("termcompany") & ": " & radStatus.SelectedItem.Text & " "
        Return datatables
    End Function
    Private Sub LoadGrid()
        Try
            GridVwHeaderChckbox.PageIndex = CInt(Session("termindex"))
            GridVwHeaderChckbox.DataSource = LoadTermData()
            GridVwHeaderChckbox.DataBind()

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Read") = False Then
                Process.loadalert(divalert, msgalert, Process.privilegemsg, "success")

                Exit Sub
            End If


            If Not Me.IsPostBack Then
                radStatus.Items.Clear()
                radStatus.Items.Add("All Bonunses")
                radStatus.Items.Add("Bonunses by job grade")






                If Session("termstatus") Is Nothing Then
                    Session("termstatus") = "Pending"
                End If
                Process.AssignRadComboValue(radStatus, Session("termstatus"))





                If Session("termindex") Is Nothing Then
                    Session("termindex") = "0"
                End If

                If Session("termsearch") Is Nothing Then
                    Session("termsearch") = ""
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
            Session("termsort") = sortExpression
            Dim direction As String = String.Empty
            If SortsDirection = SortDirection.Ascending Then
                SortsDirection = SortDirection.Descending
                direction = " DESC"
            Else
                SortsDirection = SortDirection.Ascending
                direction = " ASC"
            End If
            Dim table As DataTable = LoadTermData()
            table.DefaultView.Sort = sortExpression & direction
            GridVwHeaderChckbox.PageIndex = CInt(Session("termindex"))
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
            Session("termindex") = e.NewPageIndex
            LoadGrid()
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
            Session("termsearch") = search.Value.Trim
            LoadGrid()

            'End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub


    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                Process.loadalert(divalert, msgalert, Process.privilegemsg, "success")
                Exit Sub
            End If
            Response.Redirect("reward", True)

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Delete(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Delete") = False Then
                Process.loadalert(divalert, msgalert, Process.privilegemsg, "success")
                Exit Sub
            End If
            Dim count As Integer = 0
            Dim confirmValue As String = Request.Form("confirm_value")
            If confirmValue = "Yes" Then
                Dim atLeastOneRowDeleted As Boolean = False
                ' Iterate through the Products.Rows property
                For Each row As GridViewRow In GridVwHeaderChckbox.Rows
                    ' Access the CheckBox
                    Dim cb As CheckBox = row.FindControl("chkEmp")
                    If cb IsNot Nothing AndAlso cb.Checked Then
                        count = count + 1
                        ' Delete row! (Well, not really...)
                        atLeastOneRowDeleted = True
                        ' First, get the ProductID for the selected row
                        Dim ID As String =
                            Convert.ToString(GridVwHeaderChckbox.DataKeys(row.RowIndex).Value)
                        ' "Delete" the row
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Reward_Delete", ID)
                    End If
                Next
                Process.loadalert(divalert, msgalert, count.ToString & " records successfully deleted", "success")
                LoadGrid()

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub



    Private Sub radStatus_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles radStatus.SelectedIndexChanged
        Try
            Session("termindex") = "0"
            Session("termsearch") = ""
            Session("termstatus") = radStatus.SelectedItem.Text
            LoadGrid()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridVwHeaderChckbox_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridVwHeaderChckbox.RowCreated
        Try
            Process.SortArrow(e, SortsDirection, Session("termsort"))
        Catch ex As Exception
        End Try

    End Sub
End Class