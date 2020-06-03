Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports Telerik.Web.UI
Public Class WorkForcePlanUpdate
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "EMPWFPLAN"
    Protected Sub DrillDown(ByVal sender As Object, ByVal e As EventArgs)
        Try
            lblDate.Text = CType(sender, LinkButton).CommandArgument
            If lblDate.Text.Trim <> "" Then
                Session("WorkForcePlanDate") = lblDate.Text
            End If
            If Session("WorkForcePlanDate") IsNot Nothing Then
                If Session("WorkForcePlanDate") <> "" Then
                    lblDate.Text = Session("WorkForcePlanDate")
                End If

            End If

            divdetail.Visible = True
            If lblDate.Text <> "" Then
                LoadGrid(txtid.Text, lblDate.Text)
            End If
            divdetailheader.InnerText = "As At " & lblDate.Text & " Work Plan"
            Session("workforceyear") = txtyear.Text
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Sub DrillDownLoad()
        Try

            If lblDate.Text.Trim <> "" Then
                Session("WorkForcePlanDate") = lblDate.Text
            End If

            If Session("WorkForcePlanDate") IsNot Nothing Then
                If Session("WorkForcePlanDate") <> "" Then
                    lblDate.Text = Session("WorkForcePlanDate")
                End If

            End If

            Session("workforceyear") = txtyear.Text

            divdetail.Visible = True
            If lblDate.Text <> "" Then
                LoadGrid(txtid.Text, lblDate.Text)
            End If
            divdetailheader.InnerText = "As At " & lblDate.Text & " Work Plan"

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Function GetIdentity(ByVal company As String, ByVal office As String, ByVal budgetyear As String, _
                                  ByVal sempid As String, ByVal sUser As String, entrymode As String) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Recruit_WorkForce_Plan_Update"
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@company", SqlDbType.VarChar).Value = company
            cmd.Parameters.Add("@office", SqlDbType.VarChar).Value = office
            cmd.Parameters.Add("@budgetyear", SqlDbType.VarChar).Value = budgetyear
            cmd.Parameters.Add("@empid", SqlDbType.VarChar).Value = sempid
            cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = sUser
            cmd.Parameters.Add("@entrymode", SqlDbType.VarChar).Value = entrymode
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception            
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
            Return 0
        End Try
    End Function

    Private Sub LoadSummaryGrid(id As String)
        Try
            Dim found As Boolean = False
            gridsummary.DataSource = Process.SearchData("recruit_WorkForce_Budget_Detail_Summary", id)
            gridsummary.AllowSorting = False
            gridsummary.AllowPaging = False

            gridsummary.DataBind()
            If gridsummary.Rows.Count > 0 Then
                btncomplete.Enabled = True
                divsummary.Visible = True
            Else
                btncomplete.Enabled = False
                divsummary.Visible = False
            End If

            If Session("WorkForcePlanDate") IsNot Nothing Then
                If Session("WorkForcePlanDate") <> "" Then
                    For i As Integer = 0 To gridsummary.Rows.Count - 1
                        Dim controls As LinkButton = DirectCast(gridsummary.Rows(i).Cells(2).FindControl("lnkDownload"), LinkButton)
                        If Process.DDMONYYYY(CDate(controls.Text)) = Process.DDMONYYYY(CDate(Session("WorkForcePlanDate"))) Then
                            found = True
                        End If
                    Next
                End If

            End If
            If found = False Then
                Session("WorkForcePlanDate") = ""
            End If
            pagetitle.InnerText = "Work Force Plan"

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")            
        End Try
    End Sub
    Private Function LodaDataTable(id As String, sdate As Date) As DataTable
        Dim Datas As New DataTable
        Dim serach As String = ""

        search.Value = Session("workforceplansearch")
        If search.Value = "" Then
            Datas = Process.SearchDataP2("Recruit_WorkForce_Budget_Detail_Get_All", id, sdate)
        Else
            Datas = Process.SearchDataP3("Recruit_WorkForce_Budget_Detail_Search", id, sdate, search.Value)
        End If

        Return Datas
    End Function
    Protected Sub btnFind_Click(sender As Object, e As EventArgs)
        Try
            Session("workforceplansearch") = search.Value.Trim
            If lblDate.Text = "" Then
                Exit Sub
            Else
                '     Dim dd As Date = Date.ParseExact(lblDate.Text, "dd/MM/yyyy",
                'System.Globalization.DateTimeFormatInfo.InvariantInfo)
                '     LoadGrid(txtid.Text, dd)
                Dim dd As Date = CDate(lblDate.Text)
                'System.Globalization.DateTimeFormatInfo.InvariantInfo)
                LoadGrid(txtid.Text, dd)
            End If          
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Sub LoadGrid(id As String, sdate As Date)
        Try
            GridVwHeaderChckbox.PageIndex = CInt(Session("workforceplanindex"))
            GridVwHeaderChckbox.DataSource = LodaDataTable(id, sdate)
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

            If Session("workforceplanindex") Is Nothing Then
                Session("workforceplanindex") = "0"
            End If

            'track when updates are made
            If Session("edit") Is Nothing Then
                Session("edit") = "0"
            End If

            If Session("edit") = "1" Then
                Process.loadalert(divalert, msgalert, "Record saved!", "success")
                Session("edit") = "0"
            End If

            If Not Me.IsPostBack Then
                If Request.QueryString("id") IsNot Nothing Then

                    Session("PreviousPage") = Request.UrlReferrer.ToString
                    txtyear.Enabled = False

                    If Session("workforceplanid") Is Nothing Then
                        Session("workforceplanid") = Request.QueryString("id")
                    End If

                    If Session("workforceplansearch") Is Nothing Then
                        Session("workforceplansearch") = ""
                    End If

                    Dim strdata As New DataSet
                    If Session("workforceplanid") IsNot Nothing Then
                        strdata = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_WorkForce_Budget_Get", Session("workforceplanid"))
                    End If


                    If strdata.Tables(0).Rows.Count > 0 Then
                        txtid.Text = strdata.Tables(0).Rows(0).Item("id").ToString
                        LoadSummaryGrid(txtid.Text)
                        If Request.QueryString("id") IsNot Nothing Then
                            If lblDate.Text = "" Then
                                lblDate.Text = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Recruit_WorkForce_Budget_Detail_Get_First", txtid.Text)
                            End If
                        End If
                        DrillDownLoad()
                        lblcompany.Text = strdata.Tables(0).Rows(0).Item("company").ToString
                        Process.LoadRadComboTextAndValueP1(cboDept, "Company_Parent_Breakdown", lblcompany.Text, "Companys", "Companys", False)
                        Process.AssignRadComboValue(cboDept, strdata.Tables(0).Rows(0).Item("office").ToString)
                        txtyear.Text = strdata.Tables(0).Rows(0).Item("budgetyear").ToString

                        abudget.Value = strdata.Tables(0).Rows(0).Item("budget").ToString

                        lbstat.Value = strdata.Tables(0).Rows(0).Item("budgetstat").ToString

                        lblentry.Text = strdata.Tables(0).Rows(0).Item("entry").ToString
                        afinalstatus.Value = strdata.Tables(0).Rows(0).Item("finalstatus").ToString
                        Session("FinalApproval") = afinalstatus.Value

                        If afinalstatus.Value.ToLower = "approved" Then
                            btadddetail.Disabled = True
                            btdeletedetail.Enabled = False
                        Else
                            'btmonthcopy.Visible = False
                        End If

                        If lblentry.Text.ToLower = "budget" Then
                            lbprogressstat.InnerText = "Adopted into Company Budget"
                        End If

                        If lbstat.Value = "Complete" Then
                            btncomplete.Visible = False
                            btadddetail.Visible = False
                            btdeletedetail.Visible = False

                        End If

                        If IsDate(strdata.Tables(0).Rows(0).Item("createdon")) = True Then
                            createdon.InnerText = "Created on " & CDate(strdata.Tables(0).Rows(0).Item("createdon")).ToLongDateString
                        End If
                        If IsDate(strdata.Tables(0).Rows(0).Item("updatedon")) = True Then
                            updatedon.InnerText = "Last modified on " & CDate(strdata.Tables(0).Rows(0).Item("UpdatedOn")).ToLongDateString
                        End If


                        cboDept.Enabled = False
                        btsave.Disabled = True
                        txtyear.Enabled = False

                        'If afinalstatus.Value = "Approved" Then
                        '    btyearcopy.Visible = True
                        '    btmonthcopy.Visible = True
                        'Else
                        '    btyearcopy.Visible = False
                        '    btmonthcopy.Visible = False
                        'End If
                        Session("workforceyear") = txtyear.Text
                    End If

                Else
                    If Session("workforceyear") IsNot Nothing Then
                        txtyear.Text = Session("workforceyear")
                    Else
                        txtyear.Text = Date.Now.Year
                    End If
                    lblcompany.Text = Session("Organisation")

                    Process.LoadRadComboTextAndValueP2(cboDept, "Recruit_Budget_Company_Breakdown", Session("UserEmpID"), txtyear.Text, "name", "name", False)
                    abudget.Value = "0"
                    divappstat.Visible = False
                    txtid.Text = "0"
                    Session("edit") = "0"
                    Session("FinalApproval") = ""
                    LoadSummaryGrid(txtid.Text)
                    If txtid.Text = "0" Or txtid.Text.Trim = "" Then
                        divdetail.Visible = False
                    Else
                        divdetail.Visible = True
                    End If
                    btmonthcopy.Visible = False
                    btyearcopy.Visible = False
                    btnapprovallink.Visible = False
                End If
            End If

            If Me.gridsummary.Rows.Count > 0 Then
                btncomplete.Enabled = True
            Else
                btncomplete.Enabled = False
            End If

            If GridVwHeaderChckbox.Rows.Count <= 0 Then
                Process.DisableButton(btdeletedetail)
            Else
                Process.EnableButton(btdeletedetail, 255, 51, 0)
            End If

            If Me.gridsummary.Rows.Count > 0 And lbstat.Value.ToLower <> "complete" Then
                btncomplete.Enabled = True
            Else
                btncomplete.Enabled = False
            End If

            'Process.workplanid = 0
            If cboDept.SelectedValue IsNot Nothing Then
                Session("Dept") = cboDept.SelectedValue
                Session("workforceyear") = txtyear.Text
                Session("company") = lblcompany.Text
            End If

            LoadSummaryGrid(txtid.Text)

            If lblentry.Text = "budget" Then
                btadddetail.Disabled = True
                Process.DisableButton(btdeletedetail)
                btsave.Disabled = True
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub SortSummaryRecords(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Try
            Dim sortExpression As String = e.SortExpression
            Session("workforcesummarysort") = sortExpression
            Dim direction As String = String.Empty
            If SortsDirection1 = SortDirection.Ascending Then
                SortsDirection1 = SortDirection.Descending
                direction = " DESC"
            Else
                SortsDirection1 = SortDirection.Ascending
                direction = " ASC"
            End If
            Dim table As DataTable = Process.SearchData("recruit_WorkForce_Budget_Detail_Summary", txtid.Text)
            table.DefaultView.Sort = sortExpression & direction
            gridsummary.DataSource = table
            gridsummary.DataBind()
        Catch ex As Exception
        End Try
    End Sub
    Public Property SortsDirection1() As SortDirection
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

    Protected Sub SortRecords(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Try
            Dim sortExpression As String = e.SortExpression
            Session("workforceplansort") = sortExpression
            Dim direction As String = String.Empty
            If SortsDirection = SortDirection.Ascending Then
                SortsDirection = SortDirection.Descending
                direction = " DESC"
            Else
                SortsDirection = SortDirection.Ascending
                direction = " ASC"
            End If
            Dim table As DataTable = LodaDataTable(txtid.Text, lblDate.Text) 'Process.GetData("Recruitment_Job_Post_get_all")
            table.DefaultView.Sort = sortExpression & direction
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
            Session("workforceplanindex") = e.NewPageIndex
            GridVwHeaderChckbox.DataSource = LodaDataTable(txtid.Text, lblDate.Text) 'Process.GetData("Recruitment_Job_Post_get_all")
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




    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                Process.loadalert(divalert, msgalert, Process.privilegemsg, "danger")
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
                Exit Sub
            End If
            Session("workforceyear") = txtyear.Text
            Response.Redirect("WorkForcePlanDetailUpdate.aspx?primaryid=" & txtid.Text & "&date=" & lblDate.Text & "&year=" & txtyear.Text, True)

            

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Protected Sub Delete(sender As Object, e As EventArgs) Handles btdeletedetail.Click
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Delete") = False Then
                Process.loadalert(divalert, msgalert, Process.privilegemsg, "danger")

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
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_WorkForce_Budget_Detail_Delete", ID)
                    End If
                Next
                Process.loadalert(divalert, msgalert, count.ToString & " records successfully deleted", "success")
                LoadGrid(txtid.Text, lblDate.Text)
            
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            'GetIdentity
            Dim lblstatus As String = ""
            If txtid.Text = "0" Or txtid.Text.Trim = "" Then

                If cboDept.SelectedValue Is Nothing Then
                        lblstatus = "Office budgeted for is required!"
                        Process.loadalert(divalert, msgalert, lblstatus, "warning")
                        cboDept.Focus()
                        Exit Sub
                End If

                If (cboDept.SelectedItem.Text.ToUpper = "N/A" Or cboDept.SelectedItem.Text.Trim = "") Then
                    lblstatus = "Office budgeted for is required!"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    cboDept.Focus()
                    Exit Sub
                End If


                If IsNumeric(txtyear.Text) = False Then
                    lblstatus = "Budget Year is required!"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    txtyear.Focus()
                    Exit Sub
                End If

                txtid.Text = GetIdentity(lblcompany.Text, cboDept.SelectedValue, txtyear.Text, Session("UserEmpID"), Session("LoginID"), "plan")
                If txtid.Text = "0" Then
                    btadddetail.Disabled = True
                    Process.DisableButton(btdeletedetail)
                    btncomplete.Enabled = False

                    Exit Sub
                End If
                'Session("id") = "0"
            Else
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_WorkForce_Plan_Update", txtid.Text, lblcompany.Text, cboDept.SelectedValue, txtyear.Text, Session("UserEmpID"), Session("LoginID"), "plan")
            End If
            '

            'If Request.QueryString("id") Is Nothing Then
            '    Response.Redirect("~/Module/Employee/Recruitment/WorkForcePlanUpdate?id=" & txtid.Text, True)
            Session("edit") = "1"
            'End If

            Session("Dept") = cboDept.SelectedValue
            'Session("id") = txtid.text
            Session("company") = lblcompany.Text
            If txtid.Text = "0" Or txtid.Text.Trim = "" Then
                btadddetail.Disabled = True
                Process.DisableButton(btdeletedetail)
                btncomplete.Enabled = False
            Else
                btadddetail.Disabled = False
                Process.EnableButton(btdeletedetail, 255, 51, 0)
                lblstatus = "Record saved!"
                Process.loadalert(divalert, msgalert, lblstatus, "success")
                divdetail.Visible = True
            End If
            cboDept.Enabled = False
            txtyear.Enabled = False
            'btmonthcopy.Visible = True
            'btyearcopy.Visible = True
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect("~/Module/Employee/Recruitment/WorkForce.aspx", True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnComplete_Click(sender As Object, e As EventArgs) Handles btncomplete.Click
        Try
            Dim msg As String = ""
            Dim msgbuild As New StringBuilder()
            Dim confirmValue As String = Request.Form("confirm_value")
            If confirmValue = "Yes" Then
                If gridsummary.Rows.Count <= 0 Then
                    msg = "Plan cannot be mark complete, no plan detail exist"
                    Process.loadalert(divalert, msgalert, msg, "warning")
                Else
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_WorkForce_Budget_Complete_Update", txtid.Text, "Complete")
                    Process.requestedURL = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "menu_get_link", AuthenCode, 1)
                    Dim new_url As String = Process.ApplicationURL & "/" & Process.requestedURL
                    Process.Work_Force_Complete(txtyear.Text, cboDept.SelectedValue, Session("UserEmpID"), new_url)
                    msg = "Successfully marked as Complete, and has been forwarded for Approval"
                    lbstat.Value = "Complete"
                    Process.loadalert(divalert, msgalert, msg, "success")
                End If
                If lblDate.Text.Trim <> "" Then
                    LoadGrid(txtid.Text, lblDate.Text)
                End If

            Else
                msg = "Complete mark cancelled"
                Process.loadalert(divalert, msgalert, msg, "info")
            End If
        Catch ex As Exception            
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub lnkApproval_Click(sender As Object, e As EventArgs)

        Try
            Dim url As String = "WorkApprovalStatView.aspx?id=" & txtid.Text
            Dim s As String = "window.open('" & url + "', 'popup_window', 'width=600,height=700,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no');"
            ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try

    End Sub

    Protected Sub btnNewYear_Click(sender As Object, e As EventArgs)
        Try
            Dim url As String = "WorkForceMovePlan.aspx?id=" & txtid.Text
            Dim s As String = "window.open('" & url + "', 'popup_window', 'width=500,height=500,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no');"
            ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")            
        End Try
    End Sub

    Protected Sub btnNewMonth_Click(sender As Object, e As EventArgs)
        Try
            Dim url As String = "WorkForceCopyMonthPlan.aspx?budgetyear=" & txtyear.Text & "&company=" & cboDept.SelectedValue
            Session("Dept") = cboDept.SelectedValue
            Dim s As String = "window.open('" & url + "', 'popup_window', 'width=600,height=500,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes');"
            ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)
            'Response.Write("<script language='javascript'> { popup = window.open(""WorkForceBudgetDetailUpdate.aspx?primaryid="" , ""Stone Details"", ""height=900,width=800,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no"");  popup.focus(); }</script>")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")            
        End Try
    End Sub

   

    
    Protected Sub cboDept_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboDept.SelectedIndexChanged
        Try
            lblcompany.Text = Process.GetCompanyName(cboDept.SelectedValue)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridVwHeaderChckbox_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridVwHeaderChckbox.RowCreated
        Try
            Process.SortArrow(e, SortsDirection, Session("workforceplansort"))
        Catch ex As Exception
        End Try

    End Sub

    Private Sub txtyear_TextChanged(sender As Object, e As System.EventArgs) Handles txtyear.TextChanged
        Try
            Process.LoadRadComboTextAndValueP2(cboDept, "Recruit_Budget_Company_Breakdown", Session("UserEmpID"), txtyear.Text, "name", "name", True)
        Catch ex As Exception

        End Try

    End Sub

    Private Sub gridsummary_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridsummary.RowCreated
        Try
            Process.SortArrow(e, SortsDirection, Session("workforcesummarysort"))
        Catch ex As Exception
        End Try
    End Sub
End Class