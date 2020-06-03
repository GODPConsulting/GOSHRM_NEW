Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports Telerik.Web.UI
Public Class WorkForceBudget
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "WFBUDGET"
    Dim workplan As String = "plan"
    Dim workbudget As String = "budget"

    Private Function LoadBudgetTable() As DataTable
        Dim budget As Double = 0
        Dim Datas As New DataTable
        Dim serach As String = radStatus.SelectedItem.Text
        searchbudget.Value = Session("budgetsearch")

        If searchbudget.Value = "" Then
            Datas = Process.SearchDataP4("Recruit_WorkForce_Budget_get_all", cbobudgetCompany.SelectedValue, serach, workbudget, cboBudgetYear.SelectedValue)
        Else
            Datas = Process.SearchDataP5("Recruit_WorkForce_Budget_search", cbobudgetCompany.SelectedValue, serach, searchbudget.Value, workbudget, cboBudgetYear.SelectedValue)
        End If
        pagebudget.InnerText = cbobudgetCompany.SelectedValue & " " & cboBudgetYear.SelectedValue & ": " & serach & " WorkForce Budget (" & FormatNumber(Datas.Rows.Count, 0) & ")"

        If Datas.Rows.Count > 0 Then
            lbbudgetcurrency.InnerText = Datas.Rows(0).Item("currency").ToString
            For i As Integer = 0 To Datas.Rows.Count - 1
                budget = budget + CDbl(Datas.Rows(i).Item("budget").ToString)
            Next
        End If
        lbbudgettotal.InnerText = FormatNumber(budget, 2)
        Return Datas
    End Function

    Private Sub LoadBudget()
        Try
            GridVwHeaderChckbox.PageIndex = CInt(Session("budgetindex"))
            GridVwHeaderChckbox.DataSource = LoadBudgetTable()
            GridVwHeaderChckbox.AllowSorting = True
            GridVwHeaderChckbox.AllowPaging = True
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Private Function LoadPlanTable() As DataTable
        Dim budget As Double = 0
        Dim Datas As New DataTable
        Dim serach As String = ""
        serach = cboPlanStatus.SelectedItem.Text
        searchplan.Value = Session("plansearch")

        If searchplan.Value = "" Then
            Datas = Process.SearchDataP4("Recruit_WorkForce_Budget_get_all", cboplanCompany.SelectedValue, serach, workplan, cboPlanYear.SelectedValue)
        Else
            Datas = Process.SearchDataP5("Recruit_WorkForce_Budget_search", cboplanCompany.SelectedValue, serach, searchplan.Value, workplan, cboPlanYear.SelectedValue)
        End If
        pageplan.InnerText = cboplanCompany.SelectedValue & " " & cboPlanYear.SelectedValue & ": " & serach & " WorkForce Plan (" & Datas.Rows.Count.ToString & ")"

        If Datas.Rows.Count > 0 Then
            lbplancurrency.InnerText = Datas.Rows(0).Item("currency").ToString
            For i As Integer = 0 To Datas.Rows.Count - 1
                budget = budget + CDbl(Datas.Rows(i).Item("budget").ToString)
            Next

        End If
        lbplantotal.InnerText = FormatNumber(budget, 2)
        Return Datas
    End Function

    Private Sub LoadPlan()
        Try
            gridWorkPlan.PageIndex = CInt(Session("planindex"))
            gridWorkPlan.DataSource = LoadPlanTable()
            gridWorkPlan.AllowSorting = True
            gridWorkPlan.AllowPaging = True
            gridWorkPlan.DataBind()
            If cboplanCompany.SelectedValue.ToLower <> "all companies" Then
                gridWorkPlan.Columns(2).Visible = False
            Else
                gridWorkPlan.Columns(2).Visible = True
            End If
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
                If Session("clicked") Is Nothing Then
                    Session("clicked") = "0"
                End If

                radStatus.Items.Clear()
                radStatus.Items.Add("Pending")
                radStatus.Items.Add("Approved")
                radStatus.Items.Add("Cancelled")
                radStatus.Items.Add("Rejected")

                cboPlanStatus.Items.Clear()
                cboPlanStatus.Items.Add("Pending")
                cboPlanStatus.Items.Add("Approved")
                cboPlanStatus.Items.Add("Cancelled")
                cboPlanStatus.Items.Add("Rejected")

                Dim ismulti As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select isnull(ismulticompany,'No')  from general_info")
                If ismulti.ToLower = "no" Then
                    Process.LoadRadComboTextAndValueP2(cbobudgetCompany, "Company_Structure_Get_ByLevel", "1", Session("Access"), "name", "name", False)
                    Process.LoadRadComboTextAndValueP2(cboplanCompany, "Company_Structure_Get_ByLevel", "1", Session("Access"), "name", "name", False)
                Else
                    Process.LoadRadComboTextAndValueInitiateP2(cbobudgetCompany, "Company_Structure_Get_ByLevel", "2", Session("Access"), "All Companies", "name", "name")
                    Process.LoadRadComboTextAndValueInitiateP2(cboplanCompany, "Company_Structure_Get_ByLevel", "2", Session("Access"), "All Companies", "name", "name")
                End If

                Process.LoadRadComboTextAndValueP1(cboBudgetYear, "Recruit_WorkForce_Budget_Get_Year", Session("Access"), "budgetyear", "budgetyear", False)

                Process.LoadRadComboTextAndValueP1(cboPlanYear, "Recruit_WorkForce_Budget_Get_Year", Session("Access"), "budgetyear", "budgetyear", False)

                If Session("budgetcompany") Is Nothing Then
                    Session("budgetcompany") = Session("Organisation")
                End If

                If Session("plancompany") Is Nothing Then
                    Session("plancompany") = Session("Organisation")
                End If

                Process.AssignRadComboValue(cbobudgetCompany, Session("budgetcompany"))

                Process.AssignRadComboValue(cboplanCompany, Session("plancompany"))

                If Session("budgetyear") Is Nothing Then
                    Session("budgetyear") = Date.Now.Year.ToString
                End If

                If Session("planyear") Is Nothing Then
                    Session("planyear") = Date.Now.Year.ToString
                End If

                If Session("budgetstatus") Is Nothing Then
                    Session("budgetstatus") = "Pending"
                End If

                If Session("planstatus") Is Nothing Then
                    Session("planstatus") = "Pending"
                End If

                If Session("budgetsearch") Is Nothing Then
                    Session("budgetsearch") = ""
                End If

                If Session("plansearch") Is Nothing Then
                    Session("plansearch") = ""
                End If

                If Session("budgetindex") Is Nothing Then
                    Session("budgetindex") = "0"
                End If

                If Session("planindex") Is Nothing Then
                    Session("planindex") = "0"
                End If

                Process.AssignRadComboValue(cboBudgetYear, Session("budgetyear"))
                Process.AssignRadComboValue(cboPlanYear, Session("planyear"))
                Process.AssignRadComboValue(cboPlanStatus, Session("planstatus"))
                Process.AssignRadComboValue(radStatus, Session("budgetstatus"))


                LoadBudget()
                LoadPlan()

                If Session("clicked") = 1 Then
                    lnkworkbudget_Click(sender, e)
                ElseIf Session("clicked") = 2 Then
                    lnkworkplanapp_Click(sender, e)
                Else
                    lnkworkbudget_Click(sender, e)
                End If
            End If

            'Dim strPlanApproval As New DataSet
            'strPlanApproval = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_WorkForce_Budget_get_all", cboCompany.SelectedValue, cboPlanStatus.SelectedItem.Text, workplan, cboPlanYear.SelectedValue)
            'lnkworkplanapp.Text = "WorkForce Plan for Approval(" & strPlanApproval.Tables(0).Rows.Count.ToString & ")"

            'strPlanApproval = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_WorkForce_Budget_get_all", cboCompany.SelectedValue, radStatus.SelectedItem.Text, workbudget, cboBudgetYear.SelectedValue)
            'lnkworkbudget.Text = "WorkForce Budget(" & strPlanApproval.Tables(0).Rows.Count.ToString & ")"
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Protected Sub SortRecords(ByVal sender As Object, ByVal e As GridViewSortEventArgs) Handles GridVwHeaderChckbox.Sorting
        Try
            Dim sortExpression As String = e.SortExpression
            Session("budgetsort") = sortExpression
            Dim direction As String = String.Empty
            If SortsDirection = SortDirection.Ascending Then
                SortsDirection = SortDirection.Descending
                direction = " DESC"
            Else
                SortsDirection = SortDirection.Ascending
                direction = " ASC"
            End If
            Dim table As DataTable = LoadBudgetTable() 'Process.GetData("Recruitment_Job_Post_get_all")
            table.DefaultView.Sort = sortExpression & direction
            GridVwHeaderChckbox.PageIndex = CInt(Session("budgetindex"))
            GridVwHeaderChckbox.DataSource = table
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub SortPlanRecords(ByVal sender As Object, ByVal e As GridViewSortEventArgs) Handles gridWorkPlan.Sorting
        Try
            Dim sortExpression As String = e.SortExpression
            Session("plansort") = sortExpression
            Dim direction As String = String.Empty
            If SortsDirection = SortDirection.Ascending Then
                SortsDirection = SortDirection.Descending
                direction = " DESC"
            Else
                SortsDirection = SortDirection.Ascending
                direction = " ASC"
            End If
            Dim table As DataTable = LoadPlanTable() 'Process.GetData("Recruitment_Job_Post_get_all")
            table.DefaultView.Sort = sortExpression & direction
            gridWorkPlan.PageIndex = CInt(Session("planindex"))
            gridWorkPlan.DataSource = table
            gridWorkPlan.DataBind()
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
            Session("budgetindex") = e.NewPageIndex
            GridVwHeaderChckbox.DataSource = LoadBudgetTable() 'Process.GetData("Recruitment_Job_Post_get_all")
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub OnRowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridVwHeaderChckbox.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GridVwHeaderChckbox, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub

    Protected Sub gridWorkPlan_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gridWorkPlan.PageIndexChanging
        Try
            gridWorkPlan.PageIndex = e.NewPageIndex
            Session("planindex") = e.NewPageIndex
            gridWorkPlan.DataSource = LoadPlanTable() 'Process.GetData("Recruitment_Job_Post_get_all")
            gridWorkPlan.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub gridWorkPlan_OnRowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gridWorkPlan.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gridWorkPlan, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub

    Protected Sub btnFindBudget_Click(sender As Object, e As EventArgs)
        Try
            'If Not Me.IsPostBack Then
            Session("budgetsearch") = searchbudget.Value
            LoadBudget()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub


    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                Process.loadalert(divalert, msgalert, Process.privilegemsg, "warning")
                Exit Sub
            End If

            Response.Redirect("~/Module/Recruitment/WorkForceBudgetUpdate.aspx", True)

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Protected Sub Delete(sender As Object, e As EventArgs) Handles btDelete.Click
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Delete") = False Then
                Process.loadalert(divalert, msgalert, Process.privilegemsg, "warning")                
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
                        count = count + 1
                        ' Delete row! (Well, not really...)
                        atLeastOneRowDeleted = True
                        ' First, get the ProductID for the selected row
                        Dim ID As String = _
                            Convert.ToString(GridVwHeaderChckbox.DataKeys(row.RowIndex).Value)
                        ' "Delete" the row
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_WorkForce_Budget_delete", ID)
                    End If
                Next
                Process.loadalert(divalert, msgalert, count.ToString & " records successfully deleted", "success")
                LoadBudget()
            
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub


    Protected Sub radStatus_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles radStatus.SelectedIndexChanged
        Try
            Session("budgetstatus") = radStatus.SelectedItem.Text
            Session("budgetindex") = "0"
            LoadBudget()
        Catch ex As Exception

        End Try
    End Sub
    'Private Sub DefaultLinkFontSize(ByVal defval As String, ByVal curval As String, ByVal linkb As LinkButton)
    '    Try
    '        lnkworkbudget.Font.Size = FontUnit.Parse(defval.ToString)
    '        lnkworkplanapp.Font.Size = FontUnit.Parse(defval.ToString) 'FontUnit.Point(defval)
    '        linkb.Font.Size = FontUnit.Parse(defval.ToString) 'FontUnit.Point(curval)
    '        linkb.Font.Bold = True
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Protected Sub lnkworkbudget_Click(sender As Object, e As EventArgs)
        MultiView1.ActiveViewIndex = 0
        Session("clicked") = 1

        btbudget.Attributes.Remove("class")
        btplan.Attributes.Remove("class")
        btbudget.Attributes.Add("class", "btn btn-default")
        btplan.Attributes.Add("class", "btn btn-default")
        btbudget.Attributes.Add("class", "btn btn-success")
        LoadBudget()
    End Sub

    Protected Sub lnkworkplanapp_Click(sender As Object, e As EventArgs)
        MultiView1.ActiveViewIndex = 1
        Session("clicked") = 2
        btbudget.Attributes.Remove("class")
        btplan.Attributes.Remove("class")
        btbudget.Attributes.Add("class", "btn btn-default")
        btplan.Attributes.Add("class", "btn btn-default")
        btplan.Attributes.Add("class", "btn btn-success")
        LoadPlan()
    End Sub

    Protected Sub btnFindPlan_Click(sender As Object, e As EventArgs)
        Try
            Session("plansearch") = searchplan.Value
            LoadPlan()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Private Sub cbobudgetCompany_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cbobudgetCompany.SelectedIndexChanged
        Try
            Session("budgetcompany") = cbobudgetCompany.SelectedValue
            Session("budgetindex") = "0"
            LoadBudget()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub cboplanCompany_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboplanCompany.SelectedIndexChanged
        Try
            Session("plancompany") = cboplanCompany.SelectedValue
            Session("planindex") = "0"
            LoadPlan()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cboBudgetYear_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboBudgetYear.SelectedIndexChanged
        Try
            Session("budgetyear") = cboBudgetYear.SelectedValue
            Session("budgetindex") = "0"
            LoadBudget()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cboPlanStatus_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboPlanStatus.SelectedIndexChanged
        Try
            Session("planstatus") = cboPlanStatus.SelectedItem.Text
            Session("planindex") = "0"
            LoadPlan()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cboPlanYear_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboPlanYear.SelectedIndexChanged
        Try
            Session("planyear") = cboPlanYear.SelectedValue
            Session("planindex") = "0"
            LoadPlan()
        Catch ex As Exception

        End Try
    End Sub
End Class