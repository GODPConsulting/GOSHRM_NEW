﻿Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Public Class Terminations
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "EMPTERMINATE"
    Private Function LoadTermData() As DataTable
        search.Value = Session("termsearch")
        Dim datatables As New DataTable
        If search.Value.Trim = "" Then
            datatables = Process.SearchDataP4("Emp_Termination_get_all", Session("termcompany"), radStatus.SelectedItem.Text, Process.DDMONYYYY(adatefrom.SelectedDate), Process.DDMONYYYY(adateto.SelectedDate))
        Else
            datatables = Process.SearchDataP5("Emp_Termination_search", search.Value.Trim, Session("termcompany"), radStatus.SelectedItem.Text, Process.DDMONYYYY(adatefrom.SelectedDate), Process.DDMONYYYY(adateto.SelectedDate))
        End If
        pagetitle.InnerText = Session("termcompany") & ": " & radStatus.SelectedItem.Text & " " & Process.DDMONYYYY(adatefrom.SelectedDate) & " : " & Process.DDMONYYYY(adateto.SelectedDate) & " Exits(" & datatables.Rows.Count & ")"
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
                radStatus.Items.Add("Pending")
                radStatus.Items.Add("Approved")
                radStatus.Items.Add("Cancelled")
                radStatus.Items.Add("Rejected")

                Dim ismulti As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select isnull(ismulticompany,'No')  from general_info")
                If ismulti.ToLower = "no" Then
                    Process.LoadRadComboTextAndValueP2(cboCompany, "Company_Structure_Get_ByLevel", "1", Session("Access"), "name", "name", False)
                    divcompany.Visible = False
                Else
                    Process.LoadRadComboTextAndValueP2(cboCompany, "Company_Structure_Get_ByLevel", "2", Session("Access"), "name", "name", False)
                End If
                If Session("termcompany") Is Nothing Then
                    Session("termcompany") = Session("Organisation")
                End If
                Process.AssignRadComboValue(cboCompany, Session("termcompany"))

                If Session("termstatus") Is Nothing Then
                    Session("termstatus") = "Pending"
                End If
                Process.AssignRadComboValue(radStatus, Session("termstatus"))

                If Session("termdatefrom") Is Nothing Then
                    Session("termdatefrom") = Date.Now.AddDays(1 - Date.Now.Day).AddMonths(1 - Date.Now.Month)
                End If
                adatefrom.SelectedDate = CDate(Session("termdatefrom"))

                If Session("termdateto") Is Nothing Then
                    Session("termdateto") = Date.Now.AddMonths(12 - Date.Now.Month).AddDays(31 - Date.Now.Day)
                End If
                adateto.SelectedDate = CDate(Session("termdateto"))

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
            Response.Redirect("TerminationUpdate", True)
            
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
                        Dim ID As String = _
                            Convert.ToString(GridVwHeaderChckbox.DataKeys(row.RowIndex).Value)
                        ' "Delete" the row
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Termination_delete", ID)
                    End If
                Next
                Process.loadalert(divalert, msgalert, count.ToString & " records successfully deleted", "success")
                LoadGrid()
           
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Protected Sub cboCompany_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboCompany.SelectedIndexChanged
        Try
            Session("termindex") = "0"
            Session("termsearch") = ""
            Session("termcompany") = cboCompany.SelectedValue
            LoadGrid()
        Catch ex As Exception

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