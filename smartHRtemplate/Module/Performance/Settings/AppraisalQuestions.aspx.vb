﻿Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Public Class AppraisalQuestions
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "APPRAISALQUEST"
    Private Sub LoadGrid(LoadType As String)
        Try
            If LoadType = "All" Then
                GridVwHeaderChckbox.DataSource = Process.GetData("Performance_Appraisal_Questions_get_all")
                'ElseIf LoadType = "Find" Then
                '    GridVwHeaderChckbox.DataSource = Process.SearchData("Performance_Appraisal_Questions_search", txtsearch.Text.Trim)
            End If

            GridVwHeaderChckbox.AllowSorting = True
            GridVwHeaderChckbox.AllowPaging = True
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception
            lblstatus.Text = ex.Message
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            lblView.Text = "Appraisal Cycle List"
            If Not Me.IsPostBack Then
                LoadGrid("All")
            End If
        Catch ex As Exception
            lblstatus.Text = ex.Message
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
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
            Dim table As DataTable = Process.GetData("Performance_Appraisal_Questions_get_all")
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


    Protected Sub GridVwHeaderChckbox_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridVwHeaderChckbox.PageIndexChanging
        Try
            GridVwHeaderChckbox.PageIndex = e.NewPageIndex
            GridVwHeaderChckbox.DataSource = Process.GetData("Performance_Appraisal_Questions_get_all")
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

    'Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles btnFind.Click
    '    Try
    '        'If Not Me.IsPostBack Then
    '        LoadGrid("Find")
    '        'End If
    '    Catch ex As Exception
    '        ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
    '    End Try
    'End Sub


    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
                Exit Sub
            End If
            Process.loadtype = "Add"
            Response.Write("<script language='javascript'> { popup = window.open(""AppraisalQuestionUpdate.aspx"" , ""Stone Details"", ""height=400,width=400,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no"");  popup.focus(); }</script>")
        Catch ex As Exception
            lblstatus.Text = ex.Message
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    Protected Sub btnFind1_Click(sender As Object, e As EventArgs)

    End Sub
    Protected Sub Delete(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Delete") = False Then
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
                Exit Sub
            End If
            Dim confirmValue As String = Request.Form("confirm_value")
            If confirmValue = "Yes" Then
                Dim atLeastOneRowDeleted As Boolean = False
                ' Iterate through the Products.Rows property
                For Each row As GridViewRow In GridVwHeaderChckbox.Rows
                    ' Access the CheckBox
                    Dim cb As CheckBox = row.FindControl("chkEmp")
                    If cb IsNot Nothing AndAlso cb.Checked Then
                        ' Delete row! (Well, not really...)
                        atLeastOneRowDeleted = True
                        ' First, get the ProductID for the selected row
                        Dim ID As String = _
                            Convert.ToString(GridVwHeaderChckbox.DataKeys(row.RowIndex).Value)
                        ' "Delete" the row
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_Questions_delete", ID)
                    End If
                Next
                LoadGrid("All")
            Else
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Delete has been cancelled" + "')", True)
            End If
        Catch ex As Exception
            lblstatus.Text = ex.Message
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

End Class