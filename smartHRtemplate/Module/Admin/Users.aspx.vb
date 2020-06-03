Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Imports Telerik.Web.UI
Public Class Users
    Inherits System.Web.UI.Page
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "Users"
    Dim Pages As String = "Users"
    Dim sampleCSV As String = ConfigurationManager.AppSettings("sampleCSV")
    Dim ismulti As String = CStr(SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select isnull(ismulticompany,'No')  from general_info"))
    'Dim txtsearch As Object
    'Dim GridVwHeaderChckbox As Object
    Private Sub UserDataBound()
        Try
            For Each row As DataListItem In dlBlogs.Items
                Dim lblgender As Label = row.FindControl("lblgender")
                Dim imagebtn As ImageButton = row.FindControl("imgavatar")

                If lblgender.Text.ToLower.Substring(0, 1) = "f" Then
                    imagebtn.ImageUrl = "~/images/female-avatar.png"
                ElseIf lblgender.Text.ToLower.Substring(0, 1) = "m" Then
                    imagebtn.ImageUrl = "~/images/male-avatar.png"
                Else
                    imagebtn.ImageUrl = "~/images/blank-avatar.jpg"
                End If
            Next
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Private Function GetPageCount(ByVal totalcount As Integer) As Integer
        Try
            Dim pgcount As Integer = 0
            Dim rems As Integer = 0
            If totalcount <= 100 Then
                pgcount = 1
            Else
                pgcount = totalcount \ 100
                rems = totalcount Mod 100
                If rems > 0 Then
                    pgcount = pgcount + 1
                End If
            End If
            Return pgcount
        Catch ex As Exception
            Return 0
        End Try
    End Function
    Protected Sub MoveFirst(sender As Object, e As EventArgs)
        Try
            Session("userspagelow") = "1"
            Session("userspagehigh") = "100"
            Session("userspageno") = "1"

            pageno.InnerText = Session("userspageno")

            BlockPageIndex(Session("userspagelow"), Session("userspagehigh"))
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub MoveLast(sender As Object, e As EventArgs)
        Try
            Session("userspagelow") = (((CInt(Session("userspagetotal")) * 100) - 100) + 1).ToString
            Session("userspagehigh") = (CInt(Session("userspagetotal")) * 100).ToString
            Session("userspageno") = Session("userspagetotal")
            'Session("userspagetotal") = "1"

            pageno.InnerText = Session("userspageno")
            BlockPageIndex(Session("userspagelow"), Session("userspagehigh"))
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub MoveNext(sender As Object, e As EventArgs)
        Try
            Session("userspageno") = CInt(Session("userspageno")) + 1
            If CInt(Session("userspageno")) > CInt(Session("userspagetotal")) Then
                Session("userspageno") = CInt(Session("userspageno")) - 1
            End If

            Session("userspagelow") = (((CInt(Session("userspageno")) * 100) - 100) + 1).ToString
            Session("userspagehigh") = (CInt(Session("userspageno")) * 100).ToString
           
            pageno.InnerText = Session("userspageno")

            BlockPageIndex(Session("userspagelow"), Session("userspagehigh"))
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub MovePrevious(sender As Object, e As EventArgs)
        Try
            Session("userspageno") = CInt(Session("userspageno")) - 1
            If CInt(Session("userspageno")) <= 0 Then
                Session("userspageno") = 1
            End If

            Session("userspagelow") = (((CInt(Session("userspageno")) * 100) - 100) + 1).ToString
            Session("userspagehigh") = (CInt(Session("userspageno")) * 100).ToString

            pageno.InnerText = Session("userspageno")
            BlockPageIndex(Session("userspagelow"), Session("userspagehigh"))
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Private Sub BlockPageIndex(pagelow As Integer, pagehigh As Integer)
        Try
            LoadBlock(pagelow, pagehigh)
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub ListClick(sender As Object, e As EventArgs)
        Try
            LoadGrid()
            LinkClink(0)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub BlockClick(sender As Object, e As EventArgs)
        Try
            pagetotal.InnerText = Session("userspagetotal")
            pageno.Disabled = True
            pageof.Disabled = True
            pagetotal.Disabled = True
            LoadBlock(Session("userspagelow"), Session("userspagehigh"))
            LinkClink(1)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Private Sub LinkClink(ByVal ViewIndex As Integer)
        Try
            Session("userclicked") = ViewIndex
            MultiView1.ActiveViewIndex = ViewIndex
        Catch ex As Exception
        End Try
    End Sub

    Private Function LoadData(pagelow As Integer, pagehigh As Integer) As DataTable
        Dim datas As New DataTable
        If Request.QueryString("role") Is Nothing Then 'not drilldown from role
            If Session("usersloadtype") = "All" Then
                datas = Process.SearchDataP3("users_get_all", Session("Access"), pagelow, pagehigh)
            ElseIf Session("usersloadtype") = "Find" Then
                search.Value = Session("usersearch")
                datas = Process.SearchDataP4("users_search", Session("usersearch"), Session("Access"), pagelow, pagehigh)
            End If
            If pagehigh > 999900 Then
                Session("userspagetotal") = GetPageCount(datas.Rows.Count)
                pagetitle.InnerText = "Users (" & FormatNumber(datas.Rows.Count, 0) & ")"
            End If
            
        Else
            If Session("usersloadtype") = "All" Then
                datas = Process.SearchDataP4("users_role_get_all", Session("Access"), Request.QueryString("role"), pagelow, pagehigh)
            ElseIf Session("usersloadtype") = "Find" Then
                search.Value = Session("userrolesearch")
                datas = Process.SearchDataP5("users_role_search", Session("userrolesearch"), Session("Access"), Request.QueryString("role"), pagelow, pagehigh)
            End If
            'array columns
            Dim P(0) As String
            P(0) = "role"
            If pagehigh > 999900 Then
                Session("userspagetotal") = GetPageCount(datas.Rows.Count)
                Dim vals() As String = Process.GetScalarValues("roles_get", Request.QueryString("role"), P)
                pagetitle.InnerText = vals(0) & ": " & " Users (" & FormatNumber(datas.Rows.Count, 0) & ")"
            End If
            
        End If
        Return datas
    End Function

    Private Sub LoadGrid()
        Try
            Dim sdatatable As New DataTable
            sdatatable = LoadData(1, 1000000)
            GridVwHeaderChckbox.PageIndex = CInt(Session("userspageindex"))
            GridVwHeaderChckbox.DataSource = sdatatable
            GridVwHeaderChckbox.AllowSorting = True
            GridVwHeaderChckbox.AllowPaging = True
            GridVwHeaderChckbox.DataBind()

            LoadBlock(Session("userspagelow"), Session("userspagehigh"))
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Sub LoadBlock(pagelow As Integer, pagehigh As Integer)
        Try
            Dim sdatatable As New DataTable
            sdatatable = LoadData(pagelow, pagehigh)
            dlBlogs.DataSource = sdatatable
            dlBlogs.DataBind()
            UserDataBound()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Read") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "warning")

                Exit Sub
            End If

            If Not Me.IsPostBack Then
                If Session("usersclicked") Is Nothing Then
                    Session("userclicked") = "0"
                End If

                'Dim tooltips As String = "CSV File (Comma Delimited): USERID,FULLNAME, EMPID, USERROLE, ISSUPERUSER {Yes/No}, ISHRADMIN {Yes/No}, ISFINANCEADMIN {Yes/No}"
                'btnuploadfile.Attributes.Add("title", tooltips.ToLower)

                Dim ismulti As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select isnull(ismulticompany,'No')  from general_info")
                If ismulti.ToLower = "no" Then
                    accesslink.Visible = False
                Else
                    accesslink.Visible = True
                End If

                If Session("userspageindex") Is Nothing Then
                    Session("userspageindex") = "0"
                End If

                If Session("userspageno") Is Nothing Then
                    Session("userspageno") = "1"
                End If

                If Session("userspagetotal") Is Nothing Then
                    Session("userspagetotal") = "1"
                End If

                If Session("userspagelow") Is Nothing Then
                    Session("userspagelow") = "1"
                End If

                If Session("userspagehigh") Is Nothing Then
                    Session("userspagehigh") = "100"
                End If

                If Session("usersloadtype") Is Nothing Then
                    Session("usersloadtype") = "All"
                End If

                If Session("usersearch") Is Nothing Then
                    Session("usersearch") = ""
                End If
                search.Value = Session("usersearch")

                LoadGrid()
                LinkClink(Session("userclicked"))
            End If

            If ismulti.ToLower = "no" Then
                GridVwHeaderChckbox.Columns(5).Visible = False
            Else
                GridVwHeaderChckbox.Columns(5).Visible = True
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    Protected Sub SortRecords(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Try
            Dim sortExpression As String = e.SortExpression
            Session("sortuserExpression") = sortExpression
            Dim direction As String = String.Empty
            If SortsDirection = SortDirection.Ascending Then
                SortsDirection = SortDirection.Descending
                direction = " DESC"
            Else
                SortsDirection = SortDirection.Ascending
                direction = " ASC"
            End If
            Dim table As DataTable = LoadData(1, 1000000)
            table.DefaultView.Sort = sortExpression & direction
            GridVwHeaderChckbox.DataSource = table
            GridVwHeaderChckbox.PageIndex = Session("userspageindex")
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
            Session("userspageindex") = e.NewPageIndex
            GridVwHeaderChckbox.DataSource = LoadData(1, 1000000)
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridVwHeaderChckbox_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridVwHeaderChckbox.RowCreated
        Try
            Process.SortArrow(e, SortsDirection, Session("sortuserExpression"))
        Catch ex As Exception

        End Try
    End Sub

    'Protected Sub GridVwHeaderChckbox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridVwHeaderChckbox.SelectedIndexChanged
    '    Process.criteria = GridVwHeaderChckbox.SelectedRow.Cells(2).Text
    'End Sub

    Protected Sub OnRowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridVwHeaderChckbox.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GridVwHeaderChckbox, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub

    'Protected Sub OnSelectedIndexChanged(sender As Object, e As EventArgs)
    '    'For Each row As GridViewRow In GridVwHeaderChckbox.Rows
    '    '    If row.RowIndex = GridVwHeaderChckbox.SelectedIndex Then
    '    '        row.BackColor = ColorTranslator.FromHtml("#A1DCF2")
    '    '        row.ToolTip = String.Empty
    '    '        ' row.Cells(2).Text
    '    '        Exit For
    '    '    Else
    '    '        row.BackColor = ColorTranslator.FromHtml("#FFFFFF")
    '    '        row.ToolTip = "Click to select this row."
    '    '    End If
    '    'Next
    '    Session("ID") = GridVwHeaderChckbox.SelectedRow.Cells(2).Text
    '    'txtsearch.Text = GridVwHeaderChckbox.SelectedRow.Cells(2).Text
    'End Sub

    Protected Sub btnFind_Click(sender As Object, e As EventArgs)
        Try
            'If Not Me.IsPostBack Then
            If search.Value.Trim = "" Then
                Session("usersloadtype") = "All"
            Else
                Session("usersloadtype") = "Find"
            End If
            If Request.QueryString("role") Is Nothing Then
                Session("usersearch") = search.Value.Trim
            Else
                Session("userrolesearch") = search.Value.Trim
            End If

            Session("userspagelow") = "1"

            Session("userspagehigh") = "100"

            LoadGrid()

            'End If
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "warning")
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
                Exit Sub
            End If

            Response.Redirect("~/Module/Admin/AddUser", True)

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub


    Protected Sub Delete(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Delete") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "warning")
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
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
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "users_delete", ID)
                    End If
                Next
                Process.loadalert(divalert, msgalert, count.ToString & " records successfully deleted", "success")
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & count.ToString & " records successfully deleted" + "')", True)
                LoadGrid()

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Protected Sub btnExport_Click(sender As Object, e As EventArgs)
        Try

            If Process.ExportExcel(LoadData(1, 1000000), "users") = False Then
                Response.Write(Process.strExp)
            End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
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
                If Process.ImportUsers(csvPath, "users_upload", Pages) = True Then
                    ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Uploaded " & Session("uploadcnt") & " record(s)" + "')", True)
                    Process.loadalert(divalert, msgalert, "Uploaded " & Session("uploadcnt") & " record(s)", "success")
                    Process.GetAuditTrailInsertandUpdate("", "Uploaded " & Session("uploadcnt") & " record(s) from " & csvPath, "File Upload", Pages)
                Else
                    Process.loadalert(divalert, msgalert, Session("exception"), "danger")
                    ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & Session("exception") + "')", True)
                End If

            Else
                Process.loadalert(divalert, msgalert, Process.nofile, "warning")
            End If
            Session("usersloadtype") = "All"
            LoadGrid()

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs)

        Try
            Dim sPath As String = Server.MapPath(sampleCSV)
            Response.AppendHeader("Content-Disposition", "attachment; filename=USERS.csv")

            Response.TransmitFile(sPath & Convert.ToString("USERS.csv"))
            Response.Flush()
            Response.[End]()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try

    End Sub




    Protected Sub lnkAccess_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect("~/Module/Admin/UsersAccess", True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
End Class