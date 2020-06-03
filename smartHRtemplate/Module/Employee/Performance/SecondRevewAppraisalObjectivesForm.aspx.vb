Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports Telerik.Web.UI
Public Class SecondRevewAppraisalObjectivesForm
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "APPFORM"

    Private Function LoadSurbodinateGrid(loadtype As String) As DataTable
        Dim datatables As New DataTable
        If loadtype = "All" Then
            datatables = Process.SearchDataP2("Performance_Appraisal_Summary_secondreview_Get_All", Session("UserEmpID"), cboBudgetYear.SelectedValue)
        ElseIf loadtype = "Find" Then
            datatables = Process.SearchDataP3("Performance_Appraisal_Summary_secondreview_Search", Session("UserEmpID"), txtSurbodinateSearch.Value.Trim, cboBudgetYear.SelectedValue)
        End If
        pagetitle.InnerText = cboBudgetYear.SelectedValue & ": " & txtSurbodinateSearch.Value & " Direct-Report Appraisal Objective (" & datatables.Rows.Count.ToString & ")"
        Return datatables
    End Function
    Private Sub LoadSurbodinate(LoadType As String, ByVal pageindex As Integer)
        Try
            gridSurbodinate.PageIndex = pageindex
            gridSurbodinate.DataSource = LoadSurbodinateGrid(LoadType)
            gridSurbodinate.AllowSorting = True
            gridSurbodinate.AllowPaging = True
            gridSurbodinate.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not Me.IsPostBack Then
                'cboBudgetYear.Items.Clear()
                'For z As Integer = 2016 To 2100
                '    Dim itemTemp As New RadComboBoxItem ' RadComboBoxItem()
                '    itemTemp.Text = z.ToString
                '    itemTemp.Value = z.ToString
                '    cboBudgetYear.Items.Add(itemTemp)
                '    itemTemp.DataBind()
                'Next
                Process.LoadRadComboTextAndValueP1(cboBudgetYear, "Performance_Appraisal_Summary_ReviewYear", Session("UserEmpID"), "ReviewYear", "ReviewYear", False)

                Dim reviewyear As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select ReviewYear from Performance_Appraisal_Cycle a where a.company = '" + Session("Organisation") + "' and a.status = 'Open'")
                If Session("secondrepappReview") Is Nothing Then
                    If cboBudgetYear.Items.Count > 0 Then
                        'Session("directrepappReview") = cboBudgetYear.SelectedValue
                        Session("secondrepappReview") = reviewyear
                        Process.AssignRadComboValue(cboBudgetYear, Session("secondrepappReview"))
                    End If
                Else
                    Process.AssignRadComboValue(cboBudgetYear, Session("secondrepappReview"))
                End If
                'Process.AssignRadComboValue(cboBudgetYear, Date.Now.Year.ToString)
                Session("LoadType") = "All"

                LoadSurbodinate(Session("LoadType"), 0)
                Session("pageIndex2") = gridSurbodinate.PageIndex
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub


    Protected Sub SortSurbodinateRecords(ByVal sender As Object, ByVal e As GridViewSortEventArgs) Handles gridSurbodinate.Sorting
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
            Dim table As DataTable = LoadSurbodinateGrid(Session("LoadType"))
            table.DefaultView.Sort = sortExpression & direction
            gridSurbodinate.PageIndex = Session("pageIndex2")
            gridSurbodinate.DataSource = table
            gridSurbodinate.DataBind()
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


    Protected Sub OnRowDataBound1(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gridSurbodinate, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub

    Protected Sub gridSurbodinate_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gridSurbodinate.PageIndexChanging
        Try
            gridSurbodinate.PageIndex = e.NewPageIndex
            Session("pageIndex2") = e.NewPageIndex
            gridSurbodinate.DataSource = LoadSurbodinateGrid(Session("LoadType"))
            gridSurbodinate.DataBind()
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub btnSurbodinateSearch_Click(sender As Object, e As EventArgs)
        If txtSurbodinateSearch.Value.Trim Is Nothing Then
            Session("LoadType") = "All"
        Else
            Session("LoadType") = "Find"
        End If

        LoadSurbodinate(Session("LoadType"), 0)

    End Sub

    Protected Sub cboBudgetYear_SelectedIndexChanged1(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboBudgetYear.SelectedIndexChanged
        If txtSurbodinateSearch.Value.Trim Is Nothing Then
            Session("LoadType") = "All"
        Else
            Session("LoadType") = "Find"
        End If

        LoadSurbodinate(Session("LoadType"), 0)
    End Sub
End Class