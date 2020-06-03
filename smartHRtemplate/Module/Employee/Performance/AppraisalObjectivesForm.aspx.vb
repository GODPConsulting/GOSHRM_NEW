Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports Telerik.Web.UI

Public Class AppraisalObjectivesForm
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "EMPMYGOAL"

    Private Sub MyLoad()
        Try
            GridVwHeaderChckbox.PageIndex = CInt(Session("appobjformpageindex"))
            GridVwHeaderChckbox.DataSource = LoadGrid()
            GridVwHeaderChckbox.AllowSorting = True
            GridVwHeaderChckbox.AllowPaging = True
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Function LoadGrid() As DataTable
        Dim datatables As New DataTable
        If Session("appobjformloadtype") = "All" Then
            datatables = Process.SearchDataP2("Performance_Appraisal_Summary_Employee_Get_All", Session("UserEmpID"), Session("reviewyear"))
        ElseIf Session("appobjformloadtype") = "Find" Then
            search.Value = Session("appobjformsearch")
            datatables = Process.SearchDataP2("Performance_Appraisal_Summary_Employee_Search", Session("UserEmpID"), search.Value)
        End If
        Dim customName As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select PerformanceObjective from Performance_Custom_Naming")
        If (customName = "") Then
            divdetailheader.InnerText = "Performance Objective Form (" & datatables.Rows.Count.ToString & ")"
        Else
            divdetailheader.InnerText = customName + " Form (" & datatables.Rows.Count.ToString & ")"
        End If
        Return datatables
    End Function



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not Me.IsPostBack Then
                Process.LoadRadComboTextAndValueP1(radYear, "Performance_Appraisal_Summary_ReviewYear", Session("UserEmpID"), "reviewyear", "reviewyear", False)
                Dim reviewyear As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select ReviewYear from Performance_Appraisal_Cycle a where a.company = '" + Session("Organisation") + "' and a.status = 'Open'")
                If radYear.Items.Count <= 0 Then
                    Dim msg As String = "No Appraisal cycle set on " & Session("Organisation") & "!"
                    Process.loadalert(divalert, msgalert, msg, "warning")
                    Exit Sub
                End If

                Session("reviewyear") = reviewyear


                'If radYear.Items.Count < 1 Then
                '    Dim itemTemp As New RadComboBoxItem()
                '    itemTemp.Text = Session("reviewyear")
                '    itemTemp.Value = Session("reviewyear")
                '    radYear.Items.Add(itemTemp)
                '    itemTemp.DataBind()
                'End If


            Process.AssignRadComboValue(radYear, Session("reviewyear"))

            If Session("appobjformloadtype") Is Nothing Then
                Session("appobjformloadtype") = "All"
            End If

            If Session("appobjformsearch") Is Nothing Then
                Session("appobjformsearch") = ""
            End If

            If Session("appobjformpageindex") Is Nothing Then
                Session("appobjformpageindex") = "0"
            End If

            search.Value = Session("appobjformsearch")

            MyLoad()
            End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

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

    Protected Sub btnFind_Click(sender As Object, e As EventArgs)
        Try
            If search.Value.Trim = "" Then
                Session("appobjformloadtype") = "All"
            Else
                Session("appobjformloadtype") = "Find"
            End If
            Session("appobjformsearch") = search.Value.Trim

            MyLoad()
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "warning")
                Exit Sub
            End If
            Response.Redirect("~/Module/Employee/Performance/AppObjectiveUpdate", True)

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Protected Sub Delete(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            Dim count As Integer = 0
            If Process.AuthenAction(Session("role"), AuthenCode, "Delete") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "warning")
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
                        count = count + 1
                        ' Delete row! (Well, not really...)
                        atLeastOneRowDeleted = True
                        ' First, get the ProductID for the selected row
                        Dim ID As String = _
                            Convert.ToString(GridVwHeaderChckbox.DataKeys(row.RowIndex).Value)
                        ' "Delete" the row
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_summary_delete", ID)
                    End If
                Next
                Process.loadalert(divalert, msgalert, count.ToString & " records successfully deleted", "success")
                MyLoad()

            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GridVwHeaderChckbox_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridVwHeaderChckbox.PageIndexChanging
        Try
            GridVwHeaderChckbox.PageIndex = e.NewPageIndex
            Session("appobjformpageindex") = e.NewPageIndex
            GridVwHeaderChckbox.DataSource = LoadGrid()
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub GridVwHeaderChckbox_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridVwHeaderChckbox.RowCreated
        Try
            Process.SortArrow(e, SortsDirection, Session("sortappobjformExpression"))
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub OnRowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridVwHeaderChckbox.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GridVwHeaderChckbox, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub

    Protected Sub SortRecords(sender As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GridVwHeaderChckbox.Sorting
        Try
            Dim sortExpression As String = e.SortExpression
            Session("sortappobjformExpression") = sortExpression
            Dim direction As String = String.Empty
            If SortsDirection = SortDirection.Ascending Then
                SortsDirection = SortDirection.Descending
                direction = " DESC"
            Else
                SortsDirection = SortDirection.Ascending
                direction = " ASC"
            End If
            Dim table As DataTable = LoadGrid()
            table.DefaultView.Sort = sortExpression & direction
            GridVwHeaderChckbox.DataSource = table
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception
        End Try
    End Sub


    Protected Sub btnCopy_Click(sender As Object, e As EventArgs)
        Try
            Dim url As String = "AppObjectiveCopy.aspx"
            Dim s As String = "window.open('" & url + "', 'popup_window', 'width=500,height=300,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no');"
            ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)
            'Response.Write("<script language='javascript'> { popup = window.open(""WorkForceBudgetDetailUpdate.aspx?primaryid="" , ""Stone Details"", ""height=900,width=800,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no"");  popup.focus(); }</script>")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Protected Sub radYear_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles radYear.SelectedIndexChanged
        Try
            Session("reviewyear") = radYear.Text
            Session("drtrainingsearch") = ""
            MyLoad()
        Catch ex As Exception

        End Try
    End Sub
End Class