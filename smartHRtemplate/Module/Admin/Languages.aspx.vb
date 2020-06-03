Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Public Class Languages
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "LANG"
    Dim Pages As String = "Language"
    Private Function LoadDataTables() As DataTable
        Dim sData As New DataTable
        If Session("langLoadType") = "All" Then
            sData = Process.GetData("Languages_get_all")
        ElseIf Session("langLoadType") = "Find" Then
            search.Value = Session("langSearch")
            sData = Process.SearchData("Languages_search", search.Value.Trim)
        End If
        pagetitle.InnerText = "Language (" & FormatNumber(sData.Rows.Count, 0) & ")"
        Return sData
    End Function

    Private Sub LoadGrid()
        Try
            GridVwHeaderChckbox.DataSource = LoadDataTables()
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
                If Session("langLoadType") Is Nothing Then
                    Session("langLoadType") = "All"
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
            Session("langsortExpression") = sortExpression
            Dim direction As String = String.Empty
            If SortsDirection = SortDirection.Ascending Then
                SortsDirection = SortDirection.Descending
                direction = " DESC"
            Else
                SortsDirection = SortDirection.Ascending
                direction = " ASC"
            End If
            Dim table As DataTable = LoadDataTables()
            table.DefaultView.Sort = sortExpression & direction
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
            GridVwHeaderChckbox.PageIndex = e.NewPageIndex
            GridVwHeaderChckbox.DataSource = LoadDataTables()
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

    Protected Sub btnFind_Click(sender As Object, e As EventArgs)
        Try
            If search.Value.Trim = "" Then
                Session("langLoadType") = "All"
            Else
                Session("langLoadType") = "Find"
                Session("langSearch") = search.Value.Trim
            End If

            LoadGrid()
            'End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "warning")                
                Exit Sub
            End If
            Response.Redirect("~/Module/Admin/LanguagesUpdate", True)
           
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub Delete(sender As Object, e As EventArgs) Handles btDelete.Click
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Delete") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "warning")
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
                        ' Delete row! (Well, not really...)
                        count = count + 1
                        atLeastOneRowDeleted = True
                        ' First, get the ProductID for the selected row
                        Dim ID As String = _
                            Convert.ToString(GridVwHeaderChckbox.DataKeys(row.RowIndex).Value)
                        ' "Delete" the row
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Languages_delete", ID)
                    End If
                Next
                Process.loadalert(divalert, msgalert, count.ToString & " records successfully deleted", "success")
                LoadGrid()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnExport_Click(sender As Object, e As EventArgs)
        Try
            If Process.ExportExcel(LoadDataTables(), "Language") = False Then
                Response.Output.Write(Process.strExp)
            End If


            'Response.Clear()
            'Response.Buffer = True
            'Response.AddHeader("content-disposition", "attachment;filename=Languages.csv")
            'Response.Charset = ""
            'Response.ContentType = "application/text"
            'Dim sBuilder As StringBuilder = New System.Text.StringBuilder()
            'For index As Integer = 1 To GridVwHeaderChckbox.Columns.Count - 1
            '    sBuilder.Append(GridVwHeaderChckbox.Columns(index).HeaderText + ","c)

            'Next
            'sBuilder.Append(vbCr & vbLf)
            'For i As Integer = 0 To GridVwHeaderChckbox.Rows.Count - 1
            '    For k As Integer = 1 To GridVwHeaderChckbox.HeaderRow.Cells.Count - 1
            '        If k = 2 Then
            '            Dim controls As HyperLink = DirectCast(GridVwHeaderChckbox.Rows(i).Cells(k).FindControl("HyperLink1"), HyperLink)
            '            sBuilder.Append(controls.Text.Replace(",", "") + ",")
            '        Else
            '            sBuilder.Append(GridVwHeaderChckbox.Rows(i).Cells(k).Text.Replace(",", "") + ",")
            '        End If
            '    Next
            '    sBuilder.Append(vbCr & vbLf)
            'Next
            'Response.Output.Write(sBuilder.ToString())
            'Response.Flush()
            'Response.[End]()



        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs)
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "warning")
                Exit Sub
            End If


            If Not file1.PostedFile Is Nothing Then
                'To create a PostedFile
                Dim csvPath As String = Server.MapPath(Process.FileURL) + Path.GetFileName(file1.PostedFile.FileName)
                If File.Exists(csvPath) = True Then
                    File.Delete(csvPath)
                End If
                file1.PostedFile.SaveAs(csvPath)
                'Create byte Array with file len
                'File.ContentLength

                If Process.Import(csvPath, "languages_upload", Pages) = True Then
                    Process.loadalert(divalert, msgalert, "Uploaded " & Session("uploadcnt") & " record(s)", "success")
                    Process.GetAuditTrailInsertandUpdate("", "Uploaded " & Session("uploadcnt") & " record(s) from " & csvPath, "File Upload", Pages)
                    LoadGrid()
                Else
                    Process.loadalert(divalert, msgalert, Session("exception"), "danger")
                End If
                If File.Exists(csvPath) = True Then
                    File.Delete(csvPath)
                End If
                LoadGrid()
            Else
                Process.loadalert(divalert, msgalert, "No files selected to upload", "warning")
                file1.Focus()
                Exit Sub
            End If


        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Private Sub GridVwHeaderChckbox_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridVwHeaderChckbox.RowCreated
        Try
            Process.SortArrow(e, SortsDirection, Session("langsortExpression"))
        Catch ex As Exception

        End Try
    End Sub
End Class