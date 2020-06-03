Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Telerik.Web.UI
Public Class CompetencyList
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "JOBTEST"
    Dim olddata(5) As String
    Protected Sub ToggleRowSelection(ByVal sender As Object, ByVal e As EventArgs)

        TryCast(TryCast(sender, CheckBox).NamingContainer, GridItem).Selected = TryCast(sender, CheckBox).Checked
        Dim checkHeader As Boolean = True
        For Each dataItem As GridDataItem In gridCompetency.MasterTableView.Items
            If Not TryCast(dataItem.FindControl("CheckBox1"), CheckBox).Checked Then
                checkHeader = False
                Exit For
            End If
        Next
        Dim headerItem As GridHeaderItem = TryCast(gridCompetency.MasterTableView.GetItems(GridItemType.Header)(0), GridHeaderItem)
        TryCast(headerItem.FindControl("headerChkbox"), CheckBox).Checked = checkHeader
    End Sub
    Protected Sub ToggleSelectedState(ByVal sender As Object, ByVal e As EventArgs)
        Dim headerCheckBox As CheckBox = TryCast(sender, CheckBox)
        For Each dataItem As GridDataItem In gridCompetency.MasterTableView.Items
            TryCast(dataItem.FindControl("CheckBox1"), CheckBox).Checked = headerCheckBox.Checked
            dataItem.Selected = headerCheckBox.Checked
        Next
    End Sub
    Private Function LodaDataTable(LoadType As String) As DataTable
        Dim Datas As New DataTable
        Dim serach As String = ""
        If LoadType = "All" Then
            Datas = Process.GetData("Competency_get_all")
        ElseIf LoadType = "Find" Then
            Datas = Process.SearchData("Competency_get_search", txtsearch.Text.Trim)
        End If
        lblView.Text = txtsearch.Text & " Competency (" & Datas.Rows.Count & ")"
        Return Datas
    End Function

    Private Sub LoadData(LoadType As String)
        Try
            gridCompetency.DataSource = LodaDataTable(LoadType)
            gridCompetency.DataBind()
        Catch ex As Exception
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                Session("LoadType") = "All"
                LoadData(Session("LoadType"))
            End If
        Catch ex As Exception
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    'Protected Sub SortRecords(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
    '    Try
    '        Dim sortExpression As String = e.SortExpression
    '        Dim direction As String = String.Empty
    '        If SortDirection = SortDirection.Ascending Then
    '            SortDirection = SortDirection.Descending
    '            direction = " DESC"
    '        Else
    '            SortDirection = SortDirection.Ascending
    '            direction = " ASC"
    '        End If
    '        Dim table As New DataTable
    '        table = LodaDataTable(Session("LoadType"), txtID.Text)
    '        table.DefaultView.Sort = sortExpression & direction
    '        GridVwHeaderChckbox.DataSource = table
    '        GridVwHeaderChckbox.DataBind()
    '    Catch ex As Exception
    '    End Try
    'End Sub


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


    'Protected Sub GridVwHeaderChckbox_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridVwHeaderChckbox.PageIndexChanging
    '    Try
    '        GridVwHeaderChckbox.PageIndex = e.NewPageIndex

    '        GridVwHeaderChckbox.DataSource = LodaDataTable(Session("LoadType"), txtID.Text)
    '        GridVwHeaderChckbox.DataBind()
    '    Catch ex As Exception
    '    End Try
    'End Sub

    'Protected Sub OnRowDataBound(sender As Object, e As GridViewRowEventArgs)
    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GridVwHeaderChckbox, "Select$" & e.Row.RowIndex)
    '        e.Row.ToolTip = "Click to select this row."
    '    End If
    'End Sub
    Protected Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Try
            Response.Write("<script language='javascript'> { self.close() }</script>")
        Catch ex As Exception
        End Try
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
                Dim myCheckBox As New CheckBox()
                Dim myText As New GridTemplateColumn()
                For Each myItem As GridDataItem In gridCompetency.MasterTableView.Items
                    myCheckBox = DirectCast(myItem("CheckBoxTemplateColumn").FindControl("CheckBox1"), System.Web.UI.WebControls.CheckBox)

                    If myCheckBox IsNot Nothing AndAlso myCheckBox.Checked Then
                        Dim id As Integer = CInt(myItem.Cells(4).Text)
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Competency_Delete", id)
                    End If
                Next
                LoadData(Session("LoadType"))
            Else
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Delete has been cancelled" + "')", True)
            End If
        Catch ex As Exception
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
       
    End Sub

    Protected Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click
        Try
            If txtsearch.Text.Trim = "" Then
                Session("LoadType") = "All"
            Else
                Session("LoadType") = "Find"
            End If
            LodaDataTable(Session("LoadType"))
        Catch ex As Exception

        End Try
    End Sub
End Class