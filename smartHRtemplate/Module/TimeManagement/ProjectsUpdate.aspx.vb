Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Telerik.Web.UI
Public Class ProjectsUpdate
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "PROJECT"
    Dim marketrate As Double = 0
    Dim interestrate As Double = 0
    Dim monthlypay As Double = 0
    Dim loanamount As Double = 0
    Dim tenor As Integer = 0
    Dim fairvalue As Double = 0
    Dim EIR As Double = 0
    Dim AmortEIR As Double = 0
    Dim AmortFairValue As Double = 0
    Dim repaystartdate As Date
    Dim EMPID As String = ""
    Dim LoanType As String = ""
    Dim project As New clsProject
    'Dim projectmembers As New clsProjectMember
    Dim projectactivities As New clsProjectActivity
    Dim olddata(11) As String
    Dim Level1(2) As String
    Dim Separators() As Char = {":"c}
    Private Sub ActivityCtrlVisible()
        lblActivityProject.Visible = True
        lblActivity.Visible = True
        txtActivity.Visible = True
        btnActivitySave.Visible = True
        btnActivityCancel.Visible = True
        lblActEstimate.Visible = True
        txtEstimation.Visible = True
        'button
        btnActivityAdd.Visible = False
        btnActivityDelete.Visible = False
    End Sub
    Private Sub ActivityCtrlInvisible()
        lblActivityProject.Visible = False
        lblActivity.Visible = False
        txtActivity.Visible = False
        btnActivitySave.Visible = False
        btnActivityCancel.Visible = False
        lblActEstimate.Visible = False
        txtEstimation.Visible = False
        'button
        btnActivityAdd.Visible = True
        btnActivityDelete.Visible = True
    End Sub
    Private Sub MemberControlsVisible()
        lblMemberProject.Visible = True
        lblmember.Visible = True
        btnMemberSave.Visible = True
        btnMemberCancel.Visible = True
        cboMemberList.Visible = True
        lstMembers.Visible = True
        'buttons
        btnMemberDelete.Visible = False
        btnMemberAdd.Visible = False
    End Sub
    Private Sub MemberControlsInvisible()
        lblMemberProject.Visible = False
        lblmember.Visible = False
        lstMembers.Visible = False
        btnMemberSave.Visible = False
        btnMemberCancel.Visible = False
        cboMemberList.Visible = False

        'buttons
        btnMemberDelete.Visible = True
        btnMemberAdd.Visible = True
    End Sub
 
    Private Sub LoadActivity(ByVal ProjectID As Integer)
        Try
            GridActivity.DataSource = Process.SearchData("Time_Projects_Activities_Get_All", ProjectID)
            GridActivity.AllowSorting = False
            GridActivity.AllowPaging = True
            GridActivity.DataBind()
        Catch ex As Exception
            lblstatus1.Text = ex.Message
        End Try
    End Sub

    Private Sub LoadMembers(ByVal ProjectID As Integer)
        Try
            GridMember.DataSource = Process.SearchData("Time_Projects_Members_Get_All", ProjectID)
            'Process.LoadListAndComboxFromDataset(lstTrainer, cboTrainer, "Time_Projects_Members_Get_All", "Trainers", "EmpiD", ProjectID)
            GridMember.AllowSorting = True
            GridMember.AllowPaging = True
            GridMember.DataBind()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                If Session("clicked") Is Nothing Then
                    Session("clicked") = "0"
                End If

                Dim ismulti As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select isnull(ismulticompany,'No')  from general_info")
                If ismulti.ToLower = "no" Then
                    Process.LoadRadDropDownTextAndValueP2(radCompany, "Company_Structure_Get_ByLevel", "1", Session("Access"), "name", "name", False)
                Else
                    Process.LoadRadDropDownTextAndValueP2(radCompany, "Company_Structure_Get_ByLevel", "2", Session("Access"), "name", "name", False)
                End If
               

                Process.LoadRadComboTextAndValueP1(cboMemberList, "Emp_PersonalDetail_Get_Employees", Session("Access"), "name", "EmpID", False)
                Process.LoadRadComboTextAndValueP1(cboTeamLead, "Emp_PersonalDetail_Get_Employees", Session("Access"), "name", "EmpID", False)
                Process.LoadRadComboTextAndValueP1(cboProjectManager, "Emp_PersonalDetail_Get_Employees", Session("Access"), "name", "EmpID", False)
                Process.LoadRadComboTextAndValue(cboClient, "Time_Clients_Get_All", "name", "name")
                If Request.QueryString("id") IsNot Nothing Then                 
                    btnSave.InnerText = "Update"
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Time_Projects_Get", Request.QueryString("id"))
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    txtName.Value = strUser.Tables(0).Rows(0).Item("Name").ToString
                    Process.AssignRadComboValue(cboClient, strUser.Tables(0).Rows(0).Item("ClientName").ToString)
                    Process.AssignRadComboValue(cboTeamLead, strUser.Tables(0).Rows(0).Item("TeamLead").ToString)
                    Process.AssignRadComboValue(cboProjectManager, strUser.Tables(0).Rows(0).Item("ProjectManager").ToString)
                    Process.AssignRadDropDownValue(radCompany, strUser.Tables(0).Rows(0).Item("CompanyName").ToString)

                    If IsDBNull(strUser.Tables(0).Rows(0).Item("StartDate")) Or strUser.Tables(0).Rows(0).Item("StartDate").ToString.Trim = "" Then
                    Else
                        dtStartDate.SelectedDate = strUser.Tables(0).Rows(0).Item("StartDate")
                    End If

                    If IsDBNull(strUser.Tables(0).Rows(0).Item("enddate")) Or strUser.Tables(0).Rows(0).Item("enddate").ToString.Trim = "" Then
                        dtEndDate.SelectedDate = Nothing
                    Else
                        dtEndDate.SelectedDate = strUser.Tables(0).Rows(0).Item("enddate")
                    End If

                    If IsDBNull(strUser.Tables(0).Rows(0).Item("ExpectedEndDate")) Or strUser.Tables(0).Rows(0).Item("ExpectedEndDate").ToString.Trim = "" Then
                        dtBudgetEndDate.SelectedDate = Nothing
                    Else
                        dtBudgetEndDate.SelectedDate = strUser.Tables(0).Rows(0).Item("ExpectedEndDate")
                    End If

                    txtDetail.Value = strUser.Tables(0).Rows(0).Item("Detail").ToString
                    radStatus.SelectedText = strUser.Tables(0).Rows(0).Item("status").ToString

                    If radStatus.SelectedText.Contains("Complete") = False Then
                        lblEndDate.Visible = False
                        dtEndDate.Visible = False
                    End If

                    Process.LoadListAndComboxFromDataset(lstMembers, cboMemberList, "Time_Projects_Members_Get_All", "Members", "EmpiD", txtid.Text)

                    lblMembers.Visible = True
                    lblActivityProject.Visible = True
                    'TabProjectDetail.Visible = True
                    LoadMembers(txtid.Text)
                    LoadActivity(txtid.Text)
                    'Process.LoadListAndComboxFromDataset(txtMembers, cboMemberList, "Time_Projects_Members_Get_All", "Members", "EmpID", txtid.Text)
                    radCompany.Enabled = False
                    cboClient.Enabled = False
                Else
                    Process.AssignRadDropDownValue(radCompany, Session("Organisation"))
                    details.Visible = False
                    btnSave.InnerText = "Save"
                    txtid.Text = "0"
                    lblMembers.Visible = False
                    lblActivityProject.Visible = False
                    'TabProjectDetail.Visible = False
                    lblEndDate.Visible = False
                    dtEndDate.Visible = False
                    cboMemberList.Visible = False
                    If radCompany.Items.Count = 1 Then
                        radCompany.SelectedIndex = 0
                    End If
                End If
                MemberControlsInvisible()
            End If
            If Session("clicked") = 1 Then
                lnkmembers_Click(sender, e)
            ElseIf Session("clicked") = 2 Then
                lnkactivities_Click(sender, e)
            Else
                lnkmembers_Click(sender, e)
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Sub DefaultLinkFontSize(ByVal defval As String, ByVal curval As String, ByVal linkb As LinkButton)
        Try
            lnkactivities.Font.Size = FontUnit.Parse(defval.ToString)
            lnkmembers.Font.Size = FontUnit.Parse(defval.ToString) 'FontUnit.Point(defval)
            linkb.Font.Size = FontUnit.Parse(defval.ToString) 'FontUnit.Point(curval)
            linkb.Font.Bold = True
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub SortMemberRecords(ByVal sender As Object, ByVal e As GridViewSortEventArgs) Handles GridMember.Sorting
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
            table = Process.SearchData("Time_Projects_members_Get_All", txtid.Text)

            table.DefaultView.Sort = sortExpression & direction
            GridMember.DataSource = table
            GridMember.DataBind()
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub SortActivityRecords(ByVal sender As Object, ByVal e As GridViewSortEventArgs) Handles GridActivity.Sorting
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
            table = Process.SearchData("Time_Projects_Activities_Get_All", txtid.Text)
            table.DefaultView.Sort = sortExpression & direction
            GridActivity.DataSource = table
            GridActivity.DataBind()
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
    Protected Sub OnRowActivityDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridActivity.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GridActivity, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub


    Protected Sub OnRowMemberDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridMember.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GridMember, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub

    Protected Sub GridView1_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridMember.PageIndexChanging
        Try
            GridMember.PageIndex = e.NewPageIndex
            GridMember.DataSource = Process.SearchData("Time_Projects_Members_Get_All", txtid.Text)
            GridMember.DataBind()
        Catch ex As Exception
        End Try
    End Sub




    Protected Sub btnDeleteRepay_Click(sender As Object, e As EventArgs) Handles btnActivityDelete.Click
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Delete") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "danger")
                Exit Sub
            End If
            Dim confirmValue As String = Request.Form("confirm_value")
            If confirmValue Is Nothing Then
                confirmValue = "Yes"
            End If
            If confirmValue = "Yes" Then
                Dim atLeastOneRowDeleted As Boolean = False
                ' Iterate through the Products.Rows property
                For Each row As GridViewRow In GridActivity.Rows
                    ' Access the CheckBox
                    Dim cb As CheckBox = row.FindControl("chkEmp")
                    If cb IsNot Nothing AndAlso cb.Checked Then
                        ' Delete row! (Well, not really...)
                        atLeastOneRowDeleted = True
                        ' First, get the ProductID for the selected row
                        Dim ID As String = _
                            Convert.ToString(GridActivity.DataKeys(row.RowIndex).Value)
                        ' "Delete" the row
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Time_Projects_Activities_Delete", ID)
                    End If
                Next
                LoadActivity(txtid.Text)
            Else
                Process.loadalert(divalert, msgalert, "Delete has been cancelled", "danger")
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub



    Protected Sub radStatus_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.DropDownListEventArgs) Handles radStatus.SelectedIndexChanged
        Try
            If radStatus.SelectedText.Contains("Complete") Then
                lblEndDate.Visible = True
                dtEndDate.Visible = True
            Else
                lblEndDate.Visible = False
                dtEndDate.Visible = False
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Function GetIdentity(ByVal projname As String, ByVal clientname As String, ByVal teamlead As String, ByVal projmgr As String, ByVal status As String, ByVal detail As String, ByVal startdate As Date, ByVal enddate As Date, ByVal expectdate As Date, company As String) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Time_Projects_Update"
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = projname
            cmd.Parameters.Add("@ClientName", SqlDbType.VarChar).Value = clientname
            cmd.Parameters.Add("@TeamLead", SqlDbType.VarChar).Value = teamlead
            cmd.Parameters.Add("@ProjectManager", SqlDbType.VarChar).Value = projmgr
            cmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = status
            cmd.Parameters.Add("@Detail", SqlDbType.VarChar).Value = detail
            cmd.Parameters.Add("@StartDate", SqlDbType.Date).Value = startdate
            cmd.Parameters.Add("@EndDate", SqlDbType.Date).Value = enddate
            cmd.Parameters.Add("@ExpectedEndDate", SqlDbType.Date).Value = expectdate
            cmd.Parameters.Add("@Company", SqlDbType.VarChar).Value = company
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "danger")
                Exit Sub
            End If
            Dim lblstatus As String = ""
            If radCompany.SelectedText = Nothing Then
                lblstatus = "Company required!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                radCompany.Focus()
                Exit Sub
            End If

            If (txtName.Value.Trim = "") Then
                lblstatus = "Project Name required!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                txtName.Focus()
                Exit Sub
            End If

            If cboClient.SelectedValue Is Nothing Then
                lblstatus = "Client required!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                cboClient.Focus()
                Exit Sub
            End If

            If radStatus.SelectedText = Nothing Then
                lblstatus = "Status required!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                radStatus.Focus()
                Exit Sub
            End If

            If dtStartDate.SelectedDate Is Nothing Then
                lblstatus = "Start Date required!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                dtStartDate.Focus()
                Exit Sub
            End If

            If dtBudgetEndDate.SelectedDate Is Nothing Then
                lblstatus = "Expected End Date required!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                dtBudgetEndDate.Focus()
                Exit Sub
            End If

            'Old Data
            If Request.QueryString("id") IsNot Nothing Then
                Dim strUser As New DataSet
                strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Time_Projects_get", Request.QueryString("id"))
                olddata(0) = strUser.Tables(0).Rows(0).Item("id").ToString
                olddata(1) = strUser.Tables(0).Rows(0).Item("name").ToString
                olddata(2) = strUser.Tables(0).Rows(0).Item("ClientName").ToString
                olddata(3) = strUser.Tables(0).Rows(0).Item("ProjectMgrID").ToString
                olddata(4) = strUser.Tables(0).Rows(0).Item("TeamLeadID").ToString
                olddata(5) = strUser.Tables(0).Rows(0).Item("Detail").ToString
                olddata(6) = strUser.Tables(0).Rows(0).Item("Detail").ToString
                olddata(7) = strUser.Tables(0).Rows(0).Item("status").ToString
                olddata(8) = strUser.Tables(0).Rows(0).Item("StartDate").ToString
                olddata(9) = strUser.Tables(0).Rows(0).Item("ExpectedEndDate").ToString()
                olddata(10) = strUser.Tables(0).Rows(0).Item("enddate").ToString
            End If

            If txtid.Text.Trim = "" Then
                project.id = 0
            Else
                project.id = txtid.Text
            End If

            Dim PMEmpID As String = ""
            Dim TeamLeadEmpID As String = ""

            project.Name = txtName.Value.Trim.Trim
            project.Client = cboClient.SelectedValue
            project.ProjectManager = cboProjectManager.SelectedValue  'PMEmpID
            project.TeamLead = cboTeamLead.SelectedValue
            project.Detail = txtDetail.Value

            If dtBudgetEndDate.SelectedDate Is Nothing Then
            Else
                project.ExpectedEndDate = dtBudgetEndDate.SelectedDate
            End If

            If dtEndDate.SelectedDate Is Nothing Then
            Else
                project.EndDate = dtEndDate.SelectedDate
            End If

            If dtStartDate.SelectedDate Is Nothing Then
            Else
                project.StartDate = dtStartDate.SelectedDate
            End If

            project.Status = radStatus.SelectedText

            Dim OldValue As String = ""
            Dim NewValue As String = ""

            Dim j As Integer = 0
            If Request.QueryString("id") IsNot Nothing Then 'Updates
                For Each a In GetType(clsProject).GetProperties()
                    If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If IsNumeric(a.GetValue(project, Nothing)) = True And IsNumeric(olddata(j)) = True Then
                                If CDbl(a.GetValue(project, Nothing)) <> CDbl(olddata(j)) Then
                                    NewValue += a.Name + ": " + a.GetValue(project, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            Else
                                If a.GetValue(project, Nothing).ToString <> olddata(j).ToString Then
                                    NewValue += a.Name + ": " + a.GetValue(project, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            End If
                        End If
                    End If
                    j = j + 1
                Next
            Else
                For Each a In GetType(clsProject).GetProperties() 'New Entries
                    If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If a.GetValue(project, Nothing) = Nothing Then
                                NewValue += a.Name + ":" + " " & vbCrLf
                            Else
                                NewValue += a.Name + ": " + a.GetValue(project, Nothing).ToString & vbCrLf
                            End If
                        End If
                    End If
                Next
            End If

            'GetIdentity

            If txtid.Text <> "0" And txtid.Text.Trim <> "" Then
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Time_Projects_update", project.id, project.Name, project.Client, project.TeamLead, project.ProjectManager, project.Status, project.Detail, project.StartDate, project.EndDate, project.ExpectedEndDate, radCompany.SelectedText)
            Else
                txtid.Text = GetIdentity(project.Name, project.Client, project.TeamLead, project.ProjectManager, project.Status, project.Detail, project.StartDate, project.EndDate, project.ExpectedEndDate, radCompany.SelectedText)
            End If

            If NewValue.Trim = "" And OldValue.Trim = "" Then
            Else
                If txtid.Text <> "0" And txtid.Text.Trim <> "" Then
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Updated " & project.id & ". " & project.Name, "Projects")
                Else
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Inserted", "Projects")
                End If
            End If
            Process.loadalert(divalert, msgalert, "Record saved", "success")

            details.Visible = True
            'After save/edit
            lblMembers.Visible = True
            LoadActivity(txtid.Text)
            LoadMembers(txtid.Text)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "success")
        End Try
    End Sub

    Protected Sub GridActivity_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridActivity.PageIndexChanging
        Try
            GridActivity.PageIndex = e.NewPageIndex
            GridActivity.DataSource = Process.SearchData("Time_Projects_Activities_Get_All", txtid.Text)
            GridActivity.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btnMemberAdd_Click(sender As Object, e As EventArgs) Handles btnMemberAdd.Click
        Try
            MemberControlsVisible()
        Catch ex As Exception
            btnMemberAdd.Enabled = True
        End Try
    End Sub

    Protected Sub btnMemberCancel_Click(sender As Object, e As EventArgs) Handles btnMemberCancel.Click
        Try
            MemberControlsInvisible()
        Catch ex As Exception
            btnMemberAdd.Enabled = True
        End Try
    End Sub

    Protected Sub btnMemberSave_Click(sender As Object, e As EventArgs) Handles btnMemberSave.Click
        Try
            btnMemberSave.Enabled = False
            Dim EmpID_1 As String = ""

            Dim projID As Integer = 0
            If txtid.Text <> "0" Then
                projID = txtid.Text
            Else
                projID = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Time_Projects_GetID", txtName.Value.Trim, cboClient.SelectedValue)
            End If


            Dim collection As IList(Of RadComboBoxItem) = cboMemberList.CheckedItems
            If (collection.Count <> 0) Then
                For Each item As RadComboBoxItem In collection
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Time_Projects_Members_Update", projID, item.Value)
                Next
            Else
                lblstatus.Text = "No additional Project Members select!"
                Exit Sub
            End If



            lblstatus.Text = "Record saved"
            LoadMembers(projID)
            txtid.Text = projID
        Catch ex As Exception
            lblstatus.Text = ex.Message
        Finally
            btnMemberSave.Enabled = True
            MemberControlsInvisible()
        End Try
    End Sub

    Protected Sub btnMemberDelete_Click(sender As Object, e As EventArgs) Handles btnMemberDelete.Click
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Delete") = False Then
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
                Exit Sub
            End If
            Dim confirmValue As String = Request.Form("confirm_value")
            If confirmValue Is Nothing Then
                confirmValue = "Yes"
            End If
            If confirmValue = "Yes" Then
                Dim atLeastOneRowDeleted As Boolean = False
                ' Iterate through the Products.Rows property
                For Each row As GridViewRow In GridMember.Rows
                    ' Access the CheckBox
                    Dim cb As CheckBox = row.FindControl("chkEmp")
                    If cb IsNot Nothing AndAlso cb.Checked Then
                        ' Delete row! (Well, not really...)
                        atLeastOneRowDeleted = True
                        ' First, get the ProductID for the selected row
                        Dim ID As String = _
                            Convert.ToString(GridMember.DataKeys(row.RowIndex).Value)
                        ' "Delete" the row
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Time_Projects_Members_Delete", ID)
                    End If
                Next
                LoadMembers(txtid.Text)
            Else
                Process.loadalert(divalert, msgalert, "Delete has been cancelled", "success")
            End If
        Catch ex As Exception
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    Protected Sub btnActivityCancel_Click(sender As Object, e As EventArgs) Handles btnActivityCancel.Click
        Try
            ActivityCtrlInvisible()
        Catch ex As Exception
            btnActivityAdd.Enabled = True
        End Try
    End Sub

    Protected Sub btnActivitySave_Click(sender As Object, e As EventArgs) Handles btnActivitySave.Click
        Try
            If (txtActivity.Text.Trim = "") Then
                lblstatus1.Text = "Activity required!"
                txtActivity.Focus()
                Exit Sub
            End If

            If IsNumeric(txtEstimation.Text) = False Then
                lblstatus.Text = "Estimated Time in hours to completion is required!"
                txtEstimation.Focus()
                Exit Sub
            End If

            btnActivitySave.Enabled = False

            projectactivities.Activity = txtActivity.Text.Trim
            projectactivities.Client = cboClient.SelectedValue
            projectactivities.Project = txtName.Value
            Dim projID As Integer = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Time_Projects_GetID", txtName.Value.Trim, cboClient.SelectedValue)
            Dim NewValue As String = ""
            Dim OldValue As String = ""
            Dim j As Integer = 0
            For Each a In GetType(clsProjectActivity).GetProperties() 'New Entries
                If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
                    If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                        If a.GetValue(projectactivities, Nothing) = Nothing Then
                            NewValue += a.Name + ":" + " " & vbCrLf
                        Else
                            NewValue += a.Name + ": " + a.GetValue(projectactivities, Nothing).ToString & vbCrLf
                        End If
                    End If
                End If
            Next
            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Time_Projects_Activities_Update", 0, projID, projectactivities.Activity, txtEstimation.Text)

            Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Inserted ", "Project Activity")
            lblstatus1.Text = "Record saved"
            btnActivityAdd.Enabled = True

            lblActivityProject.Visible = False
            lblActivity.Visible = False
            txtActivity.Visible = False
            btnActivitySave.Visible = False
            btnActivityCancel.Visible = False
            ActivityCtrlInvisible()
            txtid.Text = projID
            LoadActivity(projID)
            btnActivitySave.Enabled = True
            btnActivityAdd.Visible = True
            btnActivityDelete.Visible = True
        Catch ex As Exception
            btnActivitySave.Enabled = True
            'button
            btnActivityAdd.Visible = True
            btnActivityDelete.Visible = True
            lblstatus1.Text = ex.Message

        End Try
    End Sub





    Protected Sub btnActivityAdd_Click(sender As Object, e As EventArgs) Handles btnActivityAdd.Click
        Try
            ActivityCtrlVisible()
            txtActivity.Text = ""
        Catch ex As Exception
            btnActivityAdd.Enabled = True
        End Try
    End Sub



    Protected Sub cboTeamLead_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboTeamLead.SelectedIndexChanged

    End Sub

    Protected Sub cboProjectManager_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboProjectManager.SelectedIndexChanged

    End Sub

    'Protected Sub cboMemberList_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboMemberList.SelectedIndexChanged
    '    Try
    '        'txtMember.Text = cboMemberList.SelectedItem.Text
    '    Catch ex As Exception

    '    End Try
    'End Sub


    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/Module/TimeManagement/Projects.aspx", True)
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    End Sub

    Protected Sub cboMemberList_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboMemberList.SelectedIndexChanged
        Process.LoadListBoxFromCombo(lstMembers, cboMemberList)
    End Sub

    Protected Sub lnkmembers_Click(sender As Object, e As EventArgs) Handles lnkmembers.Click
        MultiView1.ActiveViewIndex = 0
        lnkactivities.Font.Bold = False
        lnkmembers.Font.Bold = False
        DefaultLinkFontSize("12px", "12px", lnkmembers)
        Session("clicked") = 1
        LoadMembers(txtid.Text)
    End Sub

    Protected Sub lnkactivities_Click(sender As Object, e As EventArgs) Handles lnkactivities.Click
        MultiView1.ActiveViewIndex = 1
        lnkactivities.Font.Bold = False
        lnkmembers.Font.Bold = False
        DefaultLinkFontSize("12px", "12px", lnkactivities)
        Session("clicked") = 2
        LoadActivity(txtid.Text)
    End Sub
End Class