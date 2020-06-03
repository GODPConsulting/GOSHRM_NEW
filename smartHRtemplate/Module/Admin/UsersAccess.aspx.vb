Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Imports Telerik.Web.UI
Public Class UsersAccess
    Inherits System.Web.UI.Page
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "Users"
    Dim Pages As String = "Users"
    Dim sampleCSV As String = ConfigurationManager.AppSettings("sampleCSV")
    'Dim txtsearch As Object
    'Dim GridVwHeaderChckbox As Object

    Private Function LoadData() As DataTable
        Dim datas As New DataTable
        If Session("LoadType") = "All" Then
            datas = Process.SearchData("users_access_get_all", Session("Access"))
        ElseIf Session("LoadType") = "Find" Then
            datas = Process.SearchDataP2("users_access_search", Session("Access"), search.Value.Trim)
        End If
        pagetitle.InnerText = "Users Access (" & FormatNumber(datas.Rows.Count, 0) & ")"
        Return datas
    End Function

    Private Sub LoadGrid(LoadType As String)
        Try
            'GridVwHeaderChckbox.HeaderRow.ToolTip = "click to sort records"

            GridVwHeaderChckbox.PageIndex = Session("accesspageIndex")
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
                Dim tooltips As String = "CSV File (Comma Delimited): USERID, ACCESS (Company name/Subsidiary name)"
                btnuploadfile.Attributes.Add("title", tooltips.ToLower)


                If Session("accesspageIndex") Is Nothing Then
                    Session("accesspageIndex") = 0
                End If

                If Request.UrlReferrer.ToString.ToLower.Contains("/users") = True Then
                    Session("PreviousPage") = Request.UrlReferrer.ToString
                End If


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
            Session("accesssortExpression") = sortExpression
            Dim direction As String = String.Empty
            If SortsDirection = SortDirection.Ascending Then
                SortsDirection = SortDirection.Descending
                direction = " DESC"
            Else
                SortsDirection = SortDirection.Ascending
                direction = " ASC"
            End If
            Dim table As DataTable = LoadData()
            table.DefaultView.Sort = sortExpression & direction
            GridVwHeaderChckbox.PageIndex = Session("accesspageIndex")
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
            Session("accesspageIndex") = e.NewPageIndex
            GridVwHeaderChckbox.DataSource = LoadData()
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridVwHeaderChckbox_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridVwHeaderChckbox.RowCreated
        Try

            Process.SortArrow(e, SortsDirection, Session("accesssortExpression"))

        Catch ex As Exception

        End Try
    End Sub


    Protected Sub OnRowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridVwHeaderChckbox.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GridVwHeaderChckbox, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub



    Protected Sub btnFind_Click(sender As Object, e As EventArgs)
        Try
            'If Not Me.IsPostBack Then
            If search.Value.Trim = "" Then
                Session("LoadType") = "All"
            Else
                Session("LoadType") = "Find"
            End If

            LoadGrid(Session("LoadType"))
            'End If
        Catch ex As Exception

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
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "users_access_delete_id", ID)
                    End If
                Next
                Process.loadalert(divalert, msgalert, count.ToString & " records successfully deleted", "success")
                LoadGrid(Session("loadtype"))

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Protected Sub btnExport_Click(sender As Object, e As EventArgs)
        Try

            If Process.Export(GridVwHeaderChckbox, "users", 1, 2) = False Then
                Response.Write(Process.strExp)
            End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs)
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "warning")
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
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
                If Process.Import(csvPath, "users_ACCESS_upload", Pages) = True Then
                     Process.loadalert(divalert, msgalert, "Uploaded " & Session("uploadcnt") & " record(s)", "success")
                    Process.GetAuditTrailInsertandUpdate("", "Uploaded " & Session("uploadcnt") & " record(s) from " & csvPath, "File Upload", Pages)
                Else
                    Process.loadalert(divalert, msgalert, Session("exception"), "danger")
                End If
                If File.Exists(csvPath) = True Then
                    File.Delete(csvPath)
                End If
            Else                
                Process.loadalert(divalert, msgalert, "No files selected to upload", "warning")
                file1.Focus()
                Exit Sub
            End If
            LoadGrid("All")
            'Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri)
            'Response.Write("<script language='javascript'> { self.close() }</script>")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs)

        Try
            Dim sPath As String = Server.MapPath(sampleCSV)
            Response.AppendHeader("Content-Disposition", "attachment; filename=USER_access.csv")

            Response.TransmitFile(sPath & Convert.ToString("USER_access.csv"))
            Response.Flush()
            Response.[End]()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try

    End Sub


    Protected Sub btnBack_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect(Session("PreviousPage"), True)
            'Response.Redirect("~/Module/Admin/Users", True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub
End Class