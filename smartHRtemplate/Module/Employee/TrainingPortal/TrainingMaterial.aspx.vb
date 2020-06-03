Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO

Public Class TrainingMaterial
    Inherits System.Web.UI.Page
    Dim emailFile As String = ConfigurationManager.AppSettings("FileURL")
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "EMPTRAINING"
    Private Function GetTable() As DataTable
        Dim DD As New DataTable
        search.Value = Session("empmaterialsearch")
        If search.Value.Trim = "" Then
            DD = Process.SearchData("Training_Session_Attachement_Get_All", Request.QueryString("sessionid"))
        Else
            DD = Process.SearchDataP2("Training_Session_Attachement_Search", search.Value.Trim, Request.QueryString("sessionid"))
        End If

        Return DD
    End Function
    Private Sub LoadGrid()
        Try
            If Request.QueryString("sessionid") Is Nothing Then
                btBack.Visible = False
            Else
                btBack.Visible = True
            End If
            GridVwHeaderChckbox.PageIndex = CInt(Session("empmaterialindex"))
            GridVwHeaderChckbox.DataSource = GetTable()
            GridVwHeaderChckbox.AllowSorting = True
            GridVwHeaderChckbox.AllowPaging = True
            GridVwHeaderChckbox.DataBind()

            Dim strUser As New DataSet
            strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Training_Sessions_get", Request.QueryString("sessionid"))
            If strUser.Tables(0).Rows.Count > 0 Then
                pagetitle.InnerText = strUser.Tables(0).Rows(0).Item("name").ToString & " Training Materials"
            Else
                pagetitle.InnerText = "Training Materials"
            End If


        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                If Request.QueryString("sessionid") IsNot Nothing And Request.UrlReferrer.ToString.ToLower.Contains("trainings") Then
                    Session("PreviousPage") = Request.UrlReferrer.ToString
                End If
                '
                If Session("empmaterialsearch") Is Nothing Then
                    Session("empmaterialsearch") = ""
                End If

                If Session("empmaterialindex") Is Nothing Then
                    Session("empmaterialindex") = "0"
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
            Session("empmaterialsort") = sortExpression
            Dim direction As String = String.Empty
            If SortsDirection = SortDirection.Ascending Then
                SortsDirection = SortDirection.Descending
                direction = " DESC"
            Else
                SortsDirection = SortDirection.Ascending
                direction = " ASC"
            End If
            Dim table As DataTable = GetTable()
            table.DefaultView.Sort = sortExpression & direction
            GridVwHeaderChckbox.PageIndex = CInt(Session("empmaterialindex"))
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
            Session("empmaterialindex") = e.NewPageIndex
            LoadGrid()
        Catch ex As Exception
        End Try
    End Sub


    Protected Sub btnFind_Click(sender As Object, e As EventArgs)
        Try
            Session("empmaterialsearch") = search.Value.Trim
            LoadGrid()
            'End If
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub btnBack_Click(sender As Object, e As EventArgs)
        Try
            'Response.Redirect("~/Module/Employee/TrainingPortal/Trainings", True)
            Response.Redirect(Session("PreviousPage"), True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub LinkDownLoad(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim sid As String = CType(sender, LinkButton).CommandArgument
            Dim dt As DataTable = Process.SearchData("Training_Session_Attachement_Get", sid)
            If dt IsNot Nothing Then
                'downloadFile(CType(dt.Rows(0)("fileimage"), Byte()), dt.Rows(0)("filetype").ToString(), dt.Rows(0)("filename").ToString())
            End If
            Dim fileName As String = dt.Rows(0)("filename").ToString()
            Dim filePath As String = Server.MapPath(emailFile & fileName)
            Response.ContentType = ContentType
            Response.AppendHeader("Content-Disposition", ("attachment; filename=" + Path.GetFileName(filePath)))
            Response.WriteFile(filePath)
            Response.End()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub downloadFile(ByVal bytefile As Byte(), ByVal filetype As String, ByVal filename As String)
        Dim bytes() As Byte = bytefile
        Response.Buffer = True
        Response.Charset = ""
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = filetype
        Response.AddHeader("content-disposition", "attachment;filename=" & filename)
        Response.BinaryWrite(bytes)
        Response.Flush()
        Response.End()
    End Sub

    Private Sub GridVwHeaderChckbox_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridVwHeaderChckbox.RowCreated
        Try
            Process.SortArrow(e, SortsDirection, Session("empmaterialsort"))
        Catch ex As Exception
        End Try

    End Sub
End Class