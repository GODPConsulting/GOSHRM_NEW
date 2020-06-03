Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Public Class JobPostings
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "JOBPOST"
  
    Private Function LodaDataTable() As DataTable
        Dim Datas As New DataTable

        If Session("jobpostLoadType") = "All" Then
            Datas = Process.SearchDataP2("Recruit_Job_Post_get_all", Session("jobstatus"), Session("company"))
        ElseIf Session("jobpostLoadType") = "Find" Then
            search.Value = Session("jobpostSearch")
            Datas = Process.SearchDataP3("Recruit_Job_Post_search", Session("jobstatus"), search.Value.Trim, Session("company"))
        End If
        pagetitle.InnerText = cboCompany.SelectedValue & ": " & radStatus.SelectedItem.Text & " Job Posting(" & Datas.Rows.Count.ToString & ")"
        Return Datas
    End Function

    Private Sub LoadGrid()
        Try

            GridVwHeaderChckbox.PageIndex = CInt(Session("jobpostPageIndex"))
            GridVwHeaderChckbox.DataSource = LodaDataTable()

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
                'btnAdd.Attributes.Add("onclick", " return OpenMaxWindow(""JobPostingUpdate.aspx"");")

                radStatus.Items.Clear()
                radStatus.Items.Add("Open")
                radStatus.Items.Add("Closed")
                radStatus.Items.Add("On-Hold")

                Dim ismulti As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select isnull(ismulticompany,'No')  from general_info")
                If ismulti.ToLower = "no" Then
                    Process.LoadRadComboTextAndValueP2(cboCompany, "Company_Structure_Get_ByLevel", "1", Session("Access"), "name", "name", False)
                Else

                    Process.LoadRadComboTextAndValueP2(cboCompany, "Company_Structure_Get_ByLevel", "2", Session("Access"), "name", "name", False)
                End If

                If Session("company") Is Nothing Then
                    Session("company") = Session("Organisation")
                End If

                If Session("jobstatus") Is Nothing Then
                    Session("jobstatus") = "Open"
                End If

                If Session("jobpostLoadType") Is Nothing Then
                    Session("jobpostLoadType") = "All"
                End If

                If Session("jobpostPageIndex") Is Nothing Then
                    Session("jobpostPageIndex") = "0"
                End If

                Process.AssignRadComboValue(cboCompany, Session("company"))
                Process.AssignRadComboValue(radStatus, Session("jobstatus"))

                LoadGrid()

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Protected Sub SortRecords(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Try
            Dim sortExpression As String = e.SortExpression
            Session("jobpostSort") = sortExpression
            Dim direction As String = String.Empty
            If SortsDirection = SortDirection.Ascending Then
                SortsDirection = SortDirection.Descending
                direction = " DESC"
            Else
                SortsDirection = SortDirection.Ascending
                direction = " ASC"
            End If
            Dim table As DataTable = LodaDataTable() 'Process.GetData("Recruitment_Job_Post_get_all")
            table.DefaultView.Sort = sortExpression & direction
            GridVwHeaderChckbox.PageIndex = CInt(Session("jobpostPageIndex"))
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
            Session("jobpostPageIndex") = e.NewPageIndex
            GridVwHeaderChckbox.DataSource = LodaDataTable() 'Process.GetData("Recruitment_Job_Post_get_all")
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

    Protected Sub btnFind_Click(sender As Object, e As EventArgs)
        Try
            'If Not Me.IsPostBack Then
            If search.Value.Trim = "" Then
                Session("jobpostLoadType") = "All"
            Else
                Session("jobpostLoadType") = "Find"
            End If
            Session("jobpostSearch") = search.Value
            Session("jobstatus") = radStatus.SelectedItem.Text
            LoadGrid()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub


    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "warning")                
                Exit Sub
            End If
            '            Response.Write("<script type='text/javascript'>OpenMaxWindow(""JobPostingUpdate.aspx"");</script>")
            Response.Redirect("~/Module/Recruitment/JobPostingUpdate", True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try

    End Sub


    Protected Sub btnAdd_jobportal(sender As Object, e As EventArgs)
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "warning")
                Exit Sub
            End If
            'Response.Write("<script type='text/javascript'>OpenMaxWindow(""JobPostingUpdate.aspx"");</script>")
            Dim url As String = Process.ApplicationURL & "/Module/Recruitment/Applications/welcome"
            Dim s As String = "window.open('" & url + "', 'popup_window', 'width=1000,height=1000,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no');"
            ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try

    End Sub


    Protected Sub Delete(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            Process.loadalert(divalert, msgalert, "", "warning")
            If Process.AuthenAction(Session("role"), AuthenCode, "Delete") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "warning")
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
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Job_Post_delete", ID)
                    End If
                Next
                Process.loadalert(divalert, msgalert, count.ToString & " records successfully deleted", "success")
                LoadGrid()

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub


    Protected Sub radStatus_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles radStatus.SelectedIndexChanged
        Try
            Session("jobstatus") = radStatus.SelectedItem.Text
            LoadGrid()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnSystem_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect("~/Module/Recruitment/MatchCandidates.aspx", True)
        Catch ex As Exception

        End Try
    End Sub



    Private Sub cboCompany_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboCompany.SelectedIndexChanged
        Session("company") = cboCompany.SelectedValue
        LoadGrid()
    End Sub

    Private Sub GridVwHeaderChckbox_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridVwHeaderChckbox.RowCreated
        Try
            Process.SortArrow(e, SortsDirection, Session("jobpostSort"))
        Catch ex As Exception

        End Try
    End Sub
End Class