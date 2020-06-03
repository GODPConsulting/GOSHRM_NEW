Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports Telerik.Web.UI
Public Class DirectReportTrainings
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "TEAMTRAIN"

    Private Function MySurbodinateData() As DataTable
        Dim strDataSet As New DataTable
        search.Value = Session("drtrainingsearch")
        If search.Value.Trim = "" Then
            strDataSet = Process.SearchDataP3("Surbodinate_Training_Sessions_get_all", Session("UserEmpID"), Process.FirstDay(radYear.SelectedValue, 1), Process.LastDay(radYear.SelectedValue, 1))
        Else
            strDataSet = Process.SearchDataP4("Surbodinate_Training_Sessions_Search", Session("UserEmpID"), Process.FirstDay(radYear.SelectedValue, 1), Process.LastDay(radYear.SelectedValue, 1), search.Value.Trim)
        End If
        pagetitle.InnerText = "Direct-Report Training & Development : " & radYear.SelectedValue & "(" & strDataSet.Rows.Count.ToString & ")"
        Return strDataSet
    End Function
    Private Sub LoadMySurbodinate()
        Try
            dtTable = MySurbodinateData()
            GridVwHeaderChckbox.PageIndex = CInt(Session("drtrainingindex"))
            GridVwHeaderChckbox.DataSource = dtTable 'Process.SearchDataP3("Finance_Salary_Get_All", radLocation.SelectedText, radLocation.SelectedValue, StGrade)
            GridVwHeaderChckbox.AllowSorting = True
            GridVwHeaderChckbox.AllowPaging = True
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception

            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Read") = False Then
                Process.loadalert(divalert, msgalert, Process.privilegemsg, "warning")
                Exit Sub
            End If


            If Not Me.IsPostBack Then
                Process.LoadRadComboTextAndValueP1(radYear, "Surbodinate_Training_Sessions_Years", Session("UserEmpID"), "Year", "year", False)
                If Session("drtrainingsearch") Is Nothing Then
                    Session("drtrainingsearch") = ""
                End If

                If Session("drtrainingyear") Is Nothing Then
                    Session("drtrainingyear") = Date.Now.Year
                End If

                If Session("drtrainingindex") = "" Or Session("drtrainingindex") Is Nothing Then
                    Session("drtrainingindex") = "0"
                End If

                If radYear.Items.Count < 1 Then
                    Dim itemTemp As New RadComboBoxItem()
                    itemTemp.Text = Session("drtrainingyear")
                    itemTemp.Value = Session("drtrainingyear")
                    radYear.Items.Add(itemTemp)
                    itemTemp.DataBind()
                End If


                Process.AssignRadComboValue(radYear, Session("drtrainingyear"))

                LoadMySurbodinate()
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.message, "danger")
        End Try
    End Sub

    Private Sub GridVwHeaderChckbox_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridVwHeaderChckbox.RowCreated
        Try
            Process.SortArrow(e, SortsDirection, Session("drtrainingsort"))
        Catch ex As Exception
        End Try
    End Sub


    Protected Sub SortRecords(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Try
            Dim sortExpression As String = e.SortExpression
            Session("drtrainingsort") = sortExpression
            Dim direction As String = String.Empty
            If SortsDirection = SortDirection.Ascending Then
                SortsDirection = SortDirection.Descending
                direction = " DESC"
            Else
                SortsDirection = SortDirection.Ascending
                direction = " ASC"
            End If
            Dim table As New DataTable
            table = MySurbodinateData()
            table.DefaultView.Sort = sortExpression & direction
            GridVwHeaderChckbox.PageIndex = CInt(Session("drtrainingindex"))
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
            Session("drtrainingindex") = e.NewPageIndex
            LoadMySurbodinate()
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub OnSubRowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridVwHeaderChckbox.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GridVwHeaderChckbox, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub

    Protected Sub btnFind_Click(sender As Object, e As EventArgs)
        Try
            Session("drtrainingsearch") = search.Value.Trim
            LoadMySurbodinate()
            'End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


    Protected Sub radYear_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles radYear.SelectedIndexChanged
        Try
            Session("drtrainingyear") = radYear.SelectedValue
            Session("drtrainingsearch") = ""
            LoadMySurbodinate()
        Catch ex As Exception

        End Try
    End Sub
End Class