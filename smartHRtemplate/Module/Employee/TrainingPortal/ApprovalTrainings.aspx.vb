Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports Telerik.Web.UI
Public Class ApprovalTrainings
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "APPTRAINING"
    Private Function MyApprovalData(LoadType As String) As DataTable
        Dim strDataSet As New DataTable
        If LoadType = "All" Then
            strDataSet = Process.SearchDataP2("Training_Sessions_Request_Get_Surbodinate", Session("UserEmpID"), Session("reviewyear"))
        ElseIf LoadType = "Find" Then
            strDataSet = Process.SearchDataP2("Training_Sessions_Request_Get_Surbodinate_Search", Session("UserEmpID"), txtsearch3.Value)
        End If
        pagetitle.InnerText = "Trainings to Approve (" & strDataSet.Rows.Count.ToString & ")"
        Return strDataSet
    End Function
    Private Sub LoadMyApprovals(LoadType As String)
        Try
            dtTable = MyApprovalData(LoadType)
            gridRequest.DataSource = dtTable
            gridRequest.AllowSorting = True
            gridRequest.AllowPaging = True
            gridRequest.DataBind()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Read") = False Then
                Response.Write("You don't have privilege to perform this action")
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
                Exit Sub
            End If

            If Not Me.IsPostBack Then
                Process.LoadRadComboTextAndValueP1(radYear, "Training_Sessions_Request_Year", Session("UserEmpID"), "reviewyear", "reviewyear", False)
                Dim reviewyear As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select top 1 Year(b.ScheduledTime) ReviewYear from Training_Sessions_Request a inner join Training_Sessions b on a.TrainingSessionID = b.id where a.ForwardTo = '" + Session("UserEmpID") + "' order by Year(b.ScheduledTime) desc")

                Session("LoadType") = "All"
                Session("reviewyear") = reviewyear

                Process.AssignRadComboValue(radYear, Session("reviewyear"))
                LoadMyApprovals(Session("LoadType"))
            End If
        Catch ex As Exception
           Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub radYear_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles radYear.SelectedIndexChanged
        Try
            Session("reviewyear") = radYear.Text
            Session("LoadType") = "All"
            Session("drtrainingsearch") = ""
            LoadMyApprovals(Session("LoadType"))
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub SortApprovals(ByVal sender As Object, ByVal e As GridViewSortEventArgs) Handles gridRequest.Sorting
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
            Dim table As New DataTable
            table = MyApprovalData(Session("LoadType"))
            table.DefaultView.Sort = sortExpression & direction
            gridRequest.DataSource = table
            gridRequest.DataBind()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
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

    Protected Sub gridRequest_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gridRequest.PageIndexChanging
        Try
            Dim table As New DataTable
            'SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, script)
            table = MyApprovalData(Session("LoadType"))

            gridRequest.PageIndex = e.NewPageIndex
            gridRequest.DataSource = table
            gridRequest.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub OnApprovalRowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gridRequest.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gridRequest, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub


    Protected Sub Button2_Click(sender As Object, e As EventArgs)
        Try
            'If Not Me.IsPostBack Then

            If txtsearch3.Value Is Nothing Then
                Session("LoadType") = "All"
            Else
                Session("LoadType") = "Find"
            End If
            LoadMyApprovals(Session("LoadType"))
            'End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
End Class