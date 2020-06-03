Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Public Class ProjectMembers
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "JOBPOST"

    Private Function LodaDataTable(SID As Integer) As DataTable
        Dim Datas As New DataTable
        Datas = Process.SearchData("Time_Projects_Members_Get_All", SID)
        Return Datas
    End Function

    Private Sub LoadGrid(SID As Integer)
        Try

            GridVwHeaderChckbox.DataSource = LodaDataTable(SID)
            GridVwHeaderChckbox.AllowSorting = True
            GridVwHeaderChckbox.AllowPaging = True

            GridVwHeaderChckbox.DataBind()
            'GridVwHeaderChckbox.Columns(2).Visible = False
        Catch ex As Exception
            response.write(ex.message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try


            If Not Me.IsPostBack Then
                If Session("PreviousPage") Is Nothing Then
                    Session("PreviousPage") = Request.UrlReferrer
                End If

                Dim strDataSet As New DataSet
                strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Time_Projects_Get", CInt(Request.QueryString("ProjectID")))
                If strDataSet.Tables(0).Rows.Count > 0 Then
                    pagetitle.InnerText = strDataSet.Tables(0).Rows(0).Item("Name").ToString & " Project Members"
                End If

                Session("ProjectID") = CInt(Request.QueryString("ProjectID"))

                LoadGrid(CInt(Request.QueryString("ProjectID")))
            End If
        Catch ex As Exception
            response.write(ex.message)
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
            Dim table As DataTable = LodaDataTable(Session("LoadType")) 'Process.GetData("Recruitment_Job_Post_get_all")
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
            GridVwHeaderChckbox.DataSource = LodaDataTable(Session("LoadType")) 'Process.GetData("Recruitment_Job_Post_get_all")
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

    'Protected Sub btnSystem_Click(sender As Object, e As EventArgs) Handles btnSystem.Click
    '    Try
    '        Response.Redirect("~/Module/Recruitment/MatchCandidates.aspx", True)
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            'Response.Write("<script language='javascript'> { self.close() }</script>")
            If Session("PreviousPage") IsNot Nothing Then
                Response.Redirect(Session("PreviousPage").ToString())
            Else
                Response.Write("<script language='javascript'> { self.close() }</script>")
            End If
        Catch ex As Exception
        End Try
    End Sub
End Class