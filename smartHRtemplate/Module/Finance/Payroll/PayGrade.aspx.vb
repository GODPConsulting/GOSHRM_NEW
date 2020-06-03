﻿Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports Telerik.Web.UI
Imports System.IO
Public Class PayGrade
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "PAYGRADE"
    Dim Pages As String = "Pay Grade"
  
    Private Function LoadDataTables(loadtype As String) As DataTable
        Dim sData As New DataTable
        If Session("LoadType") = "All" Then
            sData = Process.GetData("Finance_Payslip_Grade_Breakdown_Get_All")
        ElseIf Session("LoadType") = "Find" Then
            sData = Process.SearchData("Finance_Payslip_Grade_Breakdown_Search", txtsearch.Value.Trim)
        End If
        Return sData
    End Function
    Private Sub LoadGrid(LoadType As String)
        Try
            GridView1.DataSource = LoadDataTables(LoadType)

            GridView1.AllowSorting = True
            GridView1.AllowPaging = True
            GridView1.DataBind()
            pagetitle.InnerText = txtsearch.Value & " Pay Grade (" & GridView1.Rows.Count.ToString & ")"
        Catch ex As Exception
           Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then

                Session("LoadType") = "All"
                LoadGrid(Session("LoadType"))
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
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
            Dim table As DataTable = LoadDataTables(Session("LoadType"))
            table.DefaultView.Sort = sortExpression & direction
            GridView1.DataSource = table
            GridView1.DataBind()
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


    Protected Sub GridVwHeaderChckbox_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        Try
            GridView1.PageIndex = e.NewPageIndex
            GridView1.DataSource = LoadDataTables(Session("LoadType"))
            GridView1.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub OnRowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GridView1, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs)
        Try
            'If Not Me.IsPostBack Then
            If txtsearch.Value.Trim = "" Then
                Session("LoadType") = "All"
            Else
                Session("LoadType") = "Find"
            End If

            LoadGrid(Session("LoadType"))
            'End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "danger")
                Exit Sub
            End If
            Process.loadtype = "Add"
            Response.Redirect("PayGradesUpdate")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnFind1_Click(sender As Object, e As EventArgs)

    End Sub
    Protected Sub Delete(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Delete") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "danger")
                Exit Sub
            End If
            Dim confirmValue As String = Request.Form("confirm_value")
            If confirmValue = "Yes" Then
                Dim atLeastOneRowDeleted As Boolean = False
                ' Iterate through the Products.Rows property
                For Each row As GridViewRow In GridView1.Rows
                    ' Access the CheckBox
                    Dim cb As CheckBox = row.FindControl("chkEmp")
                    If cb IsNot Nothing AndAlso cb.Checked Then
                        ' Delete row! (Well, not really...)
                        atLeastOneRowDeleted = True
                        ' First, get the ProductID for the selected row
                        Dim ID As String = _
                            Convert.ToString(GridView1.DataKeys(row.RowIndex).Value)
                        ' "Delete" the row
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Finance_Payslip_Grade_Breakdown_Delete", ID)
                    End If
                Next
                LoadGrid(Session("LoadType"))
            Else
                Process.loadalert(divalert, msgalert, "Delete has been cancelled", "danger")
            End If
        Catch ex As Exception
           Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub



    Protected Sub Button1_Click1(sender As Object, e As EventArgs)
        Try
            If Process.ExportExcel(LoadDataTables(""), "PayGrades") = False Then
                Response.Write(Process.strExp)
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & Process.strExp + "')", True)
            Else
                Response.Write("File saved as " & "PayGrades" & ".xls")
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "File saved as " & "PayGrades.xls" + "')", True)
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


    Protected Sub btnUpload_Click(sender As Object, e As EventArgs)
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "danger")
                Exit Sub
            End If

            If FileUpload1.PostedFile IsNot Nothing Then
                'To create a PostedFile
                Dim csvPath As String = Server.MapPath(Process.FileURL) + Path.GetFileName(FileUpload1.PostedFile.FileName)
                If File.Exists(csvPath) = True Then
                    File.Delete(csvPath)
                End If
                FileUpload1.PostedFile.SaveAs(csvPath)

                If chkIsTransposed.Checked = True Then
                    If Process.ImportTransposed(csvPath, "Finance_Payslip_Grade_Breakdown_Upload", Pages) = True Then
                        Process.loadalert(divalert, msgalert, "Uploaded " & Session("uploadcnt") & " record(s)", "success")
                    Else
                        Process.loadalert(divalert, msgalert, Process.strExp, "danger")
                    End If
                Else
                    If Process.Import(csvPath, "Finance_Payslip_Grade_Breakdown_Upload", Pages) = True Then
                        Process.loadalert(divalert, msgalert, "Uploaded " & Session("uploadcnt") & " record(s)", "success")
                    Else
                        Process.loadalert(divalert, msgalert, Process.strExp, "danger")
                    End If
                End If


                Process.GetAuditTrailInsertandUpdate("", "Uploaded " & Session("uploadcnt") & " record(s) from " & csvPath, "File Upload", Pages)
            End If
            LoadGrid(Session("LoadType"))
            'Response.Redirect("~/Module/Finance/Payroll/PayGrade.aspx")
            'Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
End Class