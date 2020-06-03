Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Telerik.Web.UI
Imports System.IO

Public Class GratuityGradeSetup
    Inherits System.Web.UI.Page
    Dim payrolloption As New clsPayrollOption
    Dim AuthenCode As String = "GRAUTITYSETUP"
    Dim olddata(5) As String
    Dim oFileAttached1 As Byte
    Dim oFileAttached As Byte()
    Dim Pages As String = "Gratuity Grade Setting"
    Dim lblstatus As String = ""
    Private Function LoadData() As DataTable
        Dim datatables As New DataTable
        If Session("LoadType") = "All" Then
            datatables = Process.GetData("Finance_Gratuity_Grade_Get_All")
        ElseIf Session("LoadType") = "Find" Then
            datatables = Process.SearchData("Finance_Gratuity_Grade_Search", txtsearch.Value.Trim)
        End If
        pagetitle.InnerText = txtsearch.Value & " Gratuity Setup By Grades (" & datatables.Rows.Count.ToString & ")"
        Return datatables
    End Function
    Private Sub LoadGrid()
        Try
            GridVwHeaderChckbox.DataSource = LoadData()
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
                Session("pageIndex1") = 0
                Session("LoadType") = "All"
                LoadGrid()
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
            Dim table As DataTable = LoadData()
            table.DefaultView.Sort = sortExpression & direction
            GridVwHeaderChckbox.PageIndex = Session("pageIndex1")
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


    Protected Sub btnAddGrade_Click(sender As Object, e As EventArgs)
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                lblstatus = "You don't have privilege to perform this action"
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
                Exit Sub
            End If

            Response.Redirect("GratuityGradeUpdate.aspx")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub GridVwHeaderChckbox_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridVwHeaderChckbox.PageIndexChanging
        Try
            GridVwHeaderChckbox.PageIndex = e.NewPageIndex
            Session("pageIndex1") = e.NewPageIndex
            GridVwHeaderChckbox.DataSource = LoadData()
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            lblstatus = ""
            If Process.AuthenAction(Session("role"), AuthenCode, "Delete") = False Then
                lblstatus = "You don't have privilege to perform this action"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
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
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Finance_Gratuity_Grade_Delete", ID)
                    End If
                Next
                LoadGrid()
            Else
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Delete has been cancelled" + "')", True)
            End If
        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "danger")
        End Try
    End Sub

    Protected Sub btnExport_Click(sender As Object, e As EventArgs)
        Try
            lblstatus = ""
            Dim filename As String = ""
            Dim empIDList As String = ""

            Dim TitleHeader As String()
            Dim TitleData As String()

            Dim scripts As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Scripts_get", "gratuitygrade")
            For i As Integer = 0 To GridVwHeaderChckbox.PageCount - 1
                GridVwHeaderChckbox.PageIndex = i
                For j As Integer = 0 To GridVwHeaderChckbox.Rows.Count - 1
                    Dim controls As HyperLink = DirectCast(GridVwHeaderChckbox.Rows(j).Cells(2).FindControl("HyperLink1"), HyperLink)
                    If i = 0 And j = 0 Then
                        empIDList = "'" & controls.Text.Replace(",", "") & "'"
                    Else
                        empIDList = empIDList & "," & "'" & controls.Text.Replace(",", "") & "'"
                    End If
                Next
            Next
            If empIDList = "" Then
                empIDList = "''"
            End If
            scripts = scripts.Replace("@grade", empIDList)
            Dim strDataSet As New DataSet
            strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, scripts)
            Dim dataTables As DataTable = strDataSet.Tables(0)
            'Dim Columns(dataTables.Columns.Count - 1) As String
            'Dim ColumnHeaders(dataTables.Columns.Count - 1) As String

            'For i As Integer = 0 To dataTables.Columns.Count - 1
            '    ColumnHeaders(i) = dataTables.Columns(i).ColumnName
            '    Columns(i) = dataTables.Columns(i).ColumnName
            'Next
            filename = txtsearch.Value & "_gratuitygrade"
            'Process.ExportDataSet(TitleHeader, TitleData, dataTables, ColumnHeaders, Columns, filename)
            'lblstatus.Text = "File saved as " & filename & ".csv"
            If Process.ExportExcel(dataTables, filename) = False Then
                Response.Write(Process.strExp)
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & Process.strExp + "')", True)
            Else
                Response.Write("File saved as " & filename & ".xls")
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "File saved as " & filename & ".xls" + "')", True)
            End If


        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "danger")
        End Try
    End Sub

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs)
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
                Exit Sub
            End If


            If FileUpload1.PostedFile Is Nothing Then
                'To create a PostedFile
                Dim csvPath As String = Server.MapPath(Process.FileURL) + Path.GetFileName(FileUpload1.PostedFile.FileName)
                If File.Exists(csvPath) = True Then
                    File.Delete(csvPath)
                End If
                FileUpload1.PostedFile.SaveAs(csvPath)
                'Create byte Array with file len
                'File.ContentLength

                If Process.Import(csvPath, "Finance_Gratuity_Grade_Upload", Pages) = True Then
                    lblstatus = "Uploaded " & Session("uploadcnt") & " record(s)"
                    Process.loadalert(divalert, msgalert, lblstatus, "success")
                Else
                    lblstatus = Process.strExp
                    Process.loadalert(divalert, msgalert, lblstatus, "danger")

                End If
                Process.GetAuditTrailInsertandUpdate("", "Uploaded " & Session("uploadcnt") & " record(s) from " & csvPath, "File Upload", Pages)

            End If
            LoadGrid()
        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "danger")
        End Try
    End Sub


    Protected Sub btnFind_Click(sender As Object, e As EventArgs)
        Try
            'If Not Me.IsPostBack Then
            If txtsearch.Value.Trim = "" Then
                Session("LoadType") = "All"
            Else
                Session("LoadType") = "Find"
            End If

            LoadGrid()
        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "danger")
        End Try
    End Sub

    Protected Sub btnAddGrade0_Click(sender As Object, e As EventArgs) Handles btnAddGrade0.Click
        Try
            Response.Redirect("~/Module/Finance/Settings/gratuitysetup.aspx", True)
        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "danger")
        End Try
    End Sub
End Class