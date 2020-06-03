Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports Telerik.Web.UI
Public Class WorkForceBudgetUpdate
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "WFBUDGET"

    Private Sub DrillDownLoad()
        Try
            If lblDate.Text.Trim <> "" Then
                Session("WorkForceBudgetDate") = lblDate.Text
            End If
           

            If Session("WorkForceBudgetDate") IsNot Nothing Then
                If Session("WorkForceBudgetDate") <> "" Then
                    lblDate.Text = Session("WorkForceBudgetDate")
                End If

            End If

            divdetail.Visible = True

            If lblDate.Text <> "" Then
                LoadGrid(txtid.Text, lblDate.Text)
            End If

            If Session("clicked") = 1 Then
                divdetailheader.InnerText = lblDate.Text & " Budget"
            ElseIf Session("clicked") = 2 Then
                divdetailheader.InnerText = lblDate.Text & " Work Plan"
            End If
            Session("workforceyear") = txtyear.Text
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub DrillDown(ByVal sender As Object, ByVal e As EventArgs)
        Try
            lblDate.Text = CType(sender, LinkButton).CommandArgument
            If lblDate.Text.Trim <> "" Then
                Session("WorkForceBudgetDate") = lblDate.Text
            End If

            If Session("WorkForceBudgetDate") IsNot Nothing Then
                If Session("WorkForceBudgetDate") <> "" Then
                    lblDate.Text = Session("WorkForceBudgetDate")
                End If

            End If

            divdetail.Visible = True
            If lblDate.Text <> "" Then
                LoadGrid(txtid.Text, lblDate.Text)
            End If


            If Session("clicked") = 1 Then
                divdetailheader.InnerText = lblDate.Text & " Budget"
            ElseIf Session("clicked") = 2 Then
                divdetailheader.InnerText = lblDate.Text & " Work Plan"
            End If
            Session("workforceyear") = txtyear.Text
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Sub LoadSummaryGrid(id As String)
        Try
            Dim found As Boolean = False
            gridsummary.DataSource = Process.SearchData("recruit_WorkForce_Budget_Detail_Summary", id)

            gridsummary.AllowSorting = False
            gridsummary.AllowPaging = False

            gridsummary.DataBind()

            If gridsummary.Rows.Count > 0 Then

                divsummary.Visible = True
            Else

                divsummary.Visible = False
            End If

            If Session("WorkForceBudgetDate") IsNot Nothing Then
                If Session("WorkForceBudgetDate") <> "" Then
                    For i As Integer = 0 To gridsummary.Rows.Count - 1
                        Dim controls As LinkButton = DirectCast(gridsummary.Rows(i).Cells(2).FindControl("lnkDownload"), LinkButton)
                        If Process.DDMONYYYY(CDate(controls.Text)) = Process.DDMONYYYY(CDate(Session("WorkForceBudgetDate"))) Then
                            found = True
                        End If
                    Next
                End If

            End If
            If found = False Then
                Session("WorkForceBudgetDate") = ""
            End If


            If Session("clicked") = 1 Then
                pagetitle.InnerText = "Work Force Budget"
            ElseIf Session("clicked") = 2 Then
                pagetitle.InnerText = "Work Force Plan"
                btncomplete.Visible = False
            Else
                pagetitle.InnerText = ""
            End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")            
        End Try
    End Sub

    Private Function GetIdentity(ByVal company As String, ByVal budgetyear As String, _
                                  ByVal sempid As String, ByVal sUser As String, entrymode As String) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Recruit_WorkForce_Budget_Update"
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@company", SqlDbType.VarChar).Value = company
            cmd.Parameters.Add("@office", SqlDbType.VarChar).Value = cboDept.SelectedValue
            cmd.Parameters.Add("@budgetyear", SqlDbType.VarChar).Value = budgetyear
            cmd.Parameters.Add("@empid", SqlDbType.VarChar).Value = sempid
            cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = sUser
            cmd.Parameters.Add("@entrymode", SqlDbType.VarChar).Value = entrymode
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
            Return 0
        End Try
    End Function
    Private Function LodaDataTable(id As String, sdate As Date) As DataTable
        Dim Datas As New DataTable
        Dim serach As String = ""

        search.Value = Session("wfpearch")
        If search.Value = "" Then
            Datas = Process.SearchDataP2("Recruit_WorkForce_Budget_Detail_Get_All", id, sdate)
        Else
            Datas = Process.SearchDataP3("Recruit_WorkForce_Budget_Detail_Search", id, sdate, search.Value)
        End If
        Return Datas
    End Function
    Protected Sub btnFind_Click(sender As Object, e As EventArgs)
        Try
            Session("wfpsearch") = search.Value.Trim
            LoadGrid(txtid.Text, lblDate.Text)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Sub LoadGrid(LoadType As String, sdate As Date)
        Try
            GridVwHeaderChckbox.PageIndex = CInt(Session("wfpindex"))
            GridVwHeaderChckbox.DataSource = LodaDataTable(LoadType, sdate)
            GridVwHeaderChckbox.AllowSorting = True
            GridVwHeaderChckbox.AllowPaging = True
            GridVwHeaderChckbox.DataBind()
            'GridVwHeaderChckbox.Columns(2).Visible = False


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
                If Session("wpfindex") Is Nothing Then
                    Session("wpfindex") = "0"
                End If

                'track when updates are made
                If Session("edit") Is Nothing Then
                    Session("edit") = "0"
                End If

                If Session("edit") = "1" Then
                    Process.loadalert(divalert, msgalert, "Record saved!", "success")
                    Session("edit") = "0"
                End If

                If Session("clicked") Is Nothing Then
                    Session("clicked") = "0"
                End If

                If Session("wpfsearch") Is Nothing Then
                    Session("wpfsearch") = ""
                End If

                ' Process.LoadRadDropDownTextAndValueP1(radBudgetYear, "Data_Year_Get_All", 2016, "year", "year", False)

                If Request.QueryString("id") IsNot Nothing Then
                    Session("PreviousPage") = Request.UrlReferrer.ToString
                    Dim strdata As New DataSet
                    strdata = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_WorkForce_Budget_Get", Request.QueryString("id"))

                    
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
                        Session("workforceyear") = strdata.Tables(0).Rows(0).Item("budgetyear").ToString
                        abudget.Value = strdata.Tables(0).Rows(0).Item("budget").ToString

                        lbloriginalentry.Text = strdata.Tables(0).Rows(0).Item("actualentry").ToString
                        lbstat.Value = strdata.Tables(0).Rows(0).Item("budgetstat").ToString
                        lblentry.Text = strdata.Tables(0).Rows(0).Item("entry").ToString
                        afinalstatus.Value = strdata.Tables(0).Rows(0).Item("finalstatus").ToString
                        Session("FinalApproval") = afinalstatus.Value
                        If lbstat.Value.ToUpper.Contains("COMPLETE") Then
                            btncomplete.Visible = False
                        Else
                            btncomplete.Visible = True
                        End If

                        If lblentry.Text.ToLower = "budget" Then
                            btnMove.Visible = False
                        End If

                        If IsDate(strdata.Tables(0).Rows(0).Item("createdon")) = True Then
                            createdon.InnerText = "Created on " & CDate(strdata.Tables(0).Rows(0).Item("createdon")).ToLongDateString & " by " & strdata.Tables(0).Rows(0).Item("createdby").ToString
                        End If
                        If IsDate(strdata.Tables(0).Rows(0).Item("updatedon")) = True Then
                            updatedon.InnerText = "Last modified on " & CDate(strdata.Tables(0).Rows(0).Item("UpdatedOn")).ToLongDateString & " by " & strdata.Tables(0).Rows(0).Item("updatedby").ToString
                        End If

                        cboDept.Enabled = False
                        txtyear.Enabled = False

                        If afinalstatus.Value.ToLower = "approved" Then
                            btyearcopy.Visible = True
                            btmonthcopy.Visible = True
                        Else
                            btyearcopy.Visible = False
                            btmonthcopy.Visible = False
                        End If

                    End If
                Else
                    txtyear.Text = "0"
                    lblcompany.Text = Session("company")

                    Process.LoadRadComboTextAndValueP2(cboDept, "Recruit_Budget_Company_Parent_Breakdown", lblcompany.Text, txtyear.Text, "Companys", "Companys", False)

                    txtid.Text = "0"
                    If txtid.Text = "0" Or txtid.Text.Trim = "" Then
                        divdetail.Visible = False
                    Else
                        divdetail.Visible = True
                    End If
                    btnMove.Visible = False
                    btmonthcopy.Visible = False
                    btyearcopy.Visible = False
                    btnapprovallink.Visible = False
                    Session("FinalApproval") = ""
                End If
            End If

            If Me.gridsummary.Rows.Count > 0 And lbstat.Value.ToLower <> "complete" Then
                btncomplete.Visible = True
            Else
                btncomplete.Visible = False
            End If

            If lblentry.Text = "budget" And lbloriginalentry.Text = "plan" Then
                lbprogressstat0.InnerText = "Moved from Plan to Budget"
                btnMove.Visible = False
            Else
                lbprogressstat0.InnerText = ""
                If afinalstatus.Value = "Approved" Then
                    btnMove.Visible = True
                Else
                    btnMove.Visible = False
                End If
            End If

            'Process.workbudgetid = 0
            If txtid.Text = "0" Then
                btnapprovallink.Visible = False
            End If

            If GridVwHeaderChckbox.Rows.Count <= 0 Then
                btdeletedetail.Enabled = False
            Else
                btdeletedetail.Enabled = True
            End If


        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")            
        End Try
    End Sub

    Protected Sub SortRecords(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Try
            Dim sortExpression As String = e.SortExpression
            Session("wfpsort") = sortExpression
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
            GridVwHeaderChckbox.PageIndex = CInt(Session("wfpindex"))
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
    Protected Sub SortSummaryRecords(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Try
            Dim sortExpression As String = e.SortExpression
            Session("wfpsummarysort") = sortExpression
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

    Protected Sub GridVwHeaderChckbox_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridVwHeaderChckbox.PageIndexChanging
        Try
            GridVwHeaderChckbox.PageIndex = e.NewPageIndex
            Session("wfpindex") = e.NewPageIndex
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
                Process.loadalert(divalert, msgalert, Process.privilegemsg, "warning")                
                Exit Sub
            End If

            Response.Redirect("~/Module/Employee/Recruitment/WorkForcePlanDetailUpdate?primaryid=" & txtid.Text & "&date=" & lblDate.Text & "&year=" & txtyear.Text & "mode=hr", True)


        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")            
        End Try
    End Sub

    Protected Sub Delete(sender As Object, e As EventArgs) Handles btdeletedetail.Click
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
                If cboDept.SelectedValue Is Nothing Or cboDept.SelectedItem.Text.ToUpper = "N/A" Then
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

                txtid.Text = GetIdentity(lblcompany.Text, txtyear.Text, Session("UserEmpID"), Session("LoginID"), "budget")
                If txtid.Text = "0" Or txtid.Text.Trim = "" Then
                    Exit Sub

                End If
                Session("id") = "0"

            Else
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_WorkForce_Budget_Update", txtid.Text, lblcompany.Text, cboDept.SelectedValue, txtyear.Text, Session("UserEmpID"), Session("LoginID"), "budget")
            End If

            If txtid.Text = "0" Or txtid.Text.Trim = "" Then
                btadddetail.Disabled = True
                btdeletedetail.Enabled = False
            Else
                btadddetail.Disabled = False
                btdeletedetail.Enabled = True
            End If

            lblstatus = "Record saved!"
            Process.loadalert(divalert, msgalert, lblstatus, "success")
            'If Request.QueryString("id") Is Nothing Then
            '    Response.Redirect("WorkForceBudgetUpdate?id=" & txtid.Text, True)
            '    Session("edit") = "1"
            'End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect("~/Module/Recruitment/WorkForceBudget", True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnComplete_Click(sender As Object, e As EventArgs) Handles btncomplete.Click
        Try
            Dim msg As String = ""
            Dim confirmValue As String = Request.Form("confirm_value")
            If confirmValue = "Yes" Then
                If gridsummary.Rows.Count <= 0 Then
                    msg = "Cannot be mark complete, no plan detail exist"
                    Process.loadalert(divalert, msgalert, msg, "warning")
                Else
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_WorkForce_Budget_Complete_Update", txtid.Text, "Complete")
                    msg = "Successfully marked as Complete, and has been forwarded for Approval"
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

    Protected Sub btnMove_Click(sender As Object, e As EventArgs) Handles btnMove.Click
        Try
            Dim msg As String = ""
            Dim confirmValue As String = Request.Form("confirm_value")
            If confirmValue = "Yes" Then
                If lbstat.Value.ToLower.Contains("complete") = False Then
                    msg = "Work Plan cannot be moved as Budget while not Complete"
                   Process.loadalert(divalert, msgalert, msg, "info")
                    Exit Sub
                End If

                If afinalstatus.Value.ToLower <> "approved" Then
                    msg = "Work Plan Approval process not complete!"
                    Process.loadalert(divalert, msgalert, msg, "info")
                    Exit Sub
                End If
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_WorkForce_Plan_Move", txtid.Text)
                If lblDate.Text.Trim <> "" Then
                    LoadGrid(txtid.Text, lblDate.Text)
                End If


                msg = "Work Plan successfully moved to Company Budget"
                Process.loadalert(divalert, msgalert, msg, "success")

                lbprogressstat0.InnerText = "Moved from Plan to Budget"
            Else
                msg = "Complete mark cancelled"
                Process.loadalert(divalert, msgalert, msg, "info")
            End If
        Catch ex As Exception            
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub



    Protected Sub lnkApprovalStat_Click(sender As Object, e As EventArgs)

        Try
            Dim url As String = "WorkForceApprovalStat.aspx?id=" & txtid.Text
            Dim s As String = "window.open('" & url + "', 'popup_window', 'width=600,height=700,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no');"
            ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")            
        End Try

    End Sub

    Protected Sub btnNewYear_Click(sender As Object, e As EventArgs)
        Try
            Dim url As String = "WorkForceMoveBudget.aspx?id=" & txtid.Text
            Dim s As String = "window.open('" & url + "', 'popup_window', 'width=500,height=300,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no');"
            ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)
            'Response.Write("<script language='javascript'> { popup = window.open(""WorkForceBudgetDetailUpdate.aspx?primaryid="" , ""Stone Details"", ""height=900,width=800,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no"");  popup.focus(); }</script>")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Protected Sub btnNewMonth_Click(sender As Object, e As EventArgs)
        Try
            Dim url As String = "WorkForceCopyMonthBudget.aspx?budgetyear=" & txtyear.Text & "&company=" & cboDept.SelectedValue
            Session("Dept") = cboDept.SelectedValue
            Dim s As String = "window.open('" & url + "', 'popup_window', 'width=500,height=300,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no');"
            ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)
            'Response.Write("<script language='javascript'> { popup = window.open(""WorkForceBudgetDetailUpdate.aspx?primaryid="" , ""Stone Details"", ""height=900,width=800,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no"");  popup.focus(); }</script>")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Protected Sub txtyear_TextChanged(sender As Object, e As EventArgs) Handles txtyear.TextChanged
        Try
            Process.LoadRadComboTextAndValueP2(cboDept, "Recruit_Budget_Company_Breakdown", Session("UserEmpID"), txtyear.Text, "name", "name", True)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridVwHeaderChckbox_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridVwHeaderChckbox.RowCreated
        Try
            Process.SortArrow(e, SortsDirection, Session("wfpsort"))
        Catch ex As Exception
        End Try

    End Sub

    Private Sub gridsummary_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridsummary.RowCreated
        'Session("wfpsummarysort")
        Try
            Process.SortArrow(e, SortsDirection, Session("wfpsummarysort"))
        Catch ex As Exception
        End Try

    End Sub
End Class