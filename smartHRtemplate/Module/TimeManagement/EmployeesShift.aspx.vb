﻿Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Public Class EmployeesShift
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "EMPSHIFT"
    Dim Pages As String = "Employee Shift"
    Dim apptype As String = ConfigurationManager.AppSettings("apptype")
    Private Function LoadEmpTypes(LoadType As String) As DataTable
        Dim datatables As New DataTable

        If Session("LoadType") = "All" Then
            datatables = Process.SearchData("Job_Employee_Shift_Get_All", cboCompany.SelectedValue)
        ElseIf Session("LoadType") = "Find" Then
            datatables = Process.SearchDataP2("Job_Employee_Shift_Search", cboCompany.SelectedValue, txtsearch.Value.Trim)
        End If
        pagetitle.InnerText = cboCompany.SelectedValue & ": " & txtsearch.Value & " Employee Shifts (" & FormatNumber(datatables.Rows.Count, 0) & ")"
        Return datatables
    End Function
    Private Sub LoadGrid(LoadType As String)
        Try
            GridVwHeaderChckbox.DataSource = LoadEmpTypes(LoadType)
            GridVwHeaderChckbox.AllowSorting = True
            GridVwHeaderChckbox.AllowPaging = True
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not Me.IsPostBack Then
                Dim ismulti As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select isnull(ismulticompany,'No')  from general_info")
                If ismulti.ToLower = "no" Then
                    Process.LoadRadComboTextAndValueP2(cboCompany, "Company_Structure_Get_ByLevel", "1", Session("Access"), "name", "name", False)
                Else
                    Process.LoadRadComboTextAndValueP2(cboCompany, "Company_Structure_Get_ByLevel", "2", Session("Access"), "name", "name", False)
                End If
                If Session("company") Is Nothing Then
                    Session("company") = Session("Organisation")
                End If
                Process.AssignRadComboValue(cboCompany, Session("company"))


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
            Dim table As DataTable = LoadEmpTypes(Session("LoadType"))
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
            GridVwHeaderChckbox.DataSource = LoadEmpTypes(Session("LoadType"))
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
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
            If txtsearch.Value = "" Then
                Session("LoadType") = "All"
            Else
                Session("LoadType") = "Find"
            End If
            'If Not Me.IsPostBack Then
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
            Response.Redirect("~/Module/TimeManagement/EmployeesShiftUpdate.aspx", True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub Delete(sender As Object, e As EventArgs)
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Delete") = False Then
                 Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "danger")
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
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Job_Employee_Shift_Team_delete", ID)
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

    Private Sub cboCompany_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboCompany.SelectedIndexChanged
        Try

            Session("company") = cboCompany.SelectedValue
            txtsearch.Value = ""
            Session("LoadType") = "All"
            LoadGrid(Session("LoadType"))
        Catch ex As Exception
           Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnExport_Click(sender As Object, e As EventArgs)
        Try
            Dim filename As String = cboCompany.SelectedValue & "_employee_shift"
            If Process.ExportExcel(LoadEmpTypes(""), filename) = False Then
                Response.Write(Process.strExp)
            Else
                Response.Write("File saved as " & filename & ".xls")
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
                'Create byte Array with file len
                'File.ContentLength
                If Process.Import(csvPath, "Job_Employee_Shift_Upload", Pages) = True Then
                    Process.loadalert(divalert, msgalert, "Uploaded " & Session("uploadcnt") & " record(s)", "success")
                Else
                    Process.loadalert(divalert, msgalert, Process.strExp, "danger")
                End If
                Process.GetAuditTrailInsertandUpdate("", "Uploaded " & Session("uploadcnt") & " record(s) from " & csvPath, "File Upload", Pages)
                LoadGrid(Session("LoadType"))
            Else
                Process.loadalert(divalert, msgalert, "no file selected", "danger")
            End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
End Class