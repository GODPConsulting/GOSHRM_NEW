Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Public Class WorkForceApproval
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "APPWFBUDGET"
    Dim AuthenCode2 As String = "APPWFPLAN"
    'APPWFPLAN
    
    Protected Sub DrillDown(ByVal sender As Object, ByVal e As EventArgs)
        Try
            lblDate.Text = CType(sender, LinkButton).CommandArgument
            If IsDate(lblDate.Text) = True Then
                LoadGrid(lblid.Text, lblDate.Text)
                divdetail.Visible = True
                divdetailheader.InnerText = "As At " & lblDate.Text & " Work Plan"
            Else
                divdetail.Visible = False
            End If
            

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Sub LoadSummaryGrid(id As String)
        Try
            gridSummary.DataSource = Process.SearchData("recruit_WorkForce_Budget_Detail_Summary", id)
            gridSummary.AllowSorting = False
            gridSummary.AllowPaging = False

            gridSummary.DataBind()
            pagetitle.InnerText = "Work Force Plan"

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub
    Private Function LodaDataTable(id As String, sdate As Date) As DataTable
        Dim Datas As New DataTable
        Dim serach As String = ""

        search.Value = Session("workapprovalsearch")
        If search.Value.Trim = "" Then
            Datas = Process.SearchDataP2("Recruit_WorkForce_Budget_Detail_Get_All", id, sdate)
        Else
            Datas = Process.SearchDataP3("Recruit_WorkForce_Budget_Detail_Search", id, sdate, search.Value.Trim)
        End If

        Return Datas
    End Function
    Protected Sub btnFind_Click(sender As Object, e As EventArgs)
        Try
            Session("workapprovalsearch") = search.Value.Trim
            LoadGrid(lblid.Text, lblDate.Text)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Sub LoadGrid(id As String, sdate As Date)
        Try
            GridVwHeaderChckbox.PageIndex = CInt(Session("workapprovalindex"))
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
            If Not Me.IsPostBack Then
                If Session("clicked") Is Nothing Then
                    Session("clicked") = "0"
                End If

                If Session("workapprovalindex") Is Nothing Then
                    Session("workapprovalindex") = "0"
                End If

                If Session("workapprovalsearch") Is Nothing Then
                    Session("workapprovalsearch") = ""
                End If

                If Request.QueryString("id") IsNot Nothing Then

                    Dim strdata As New DataSet
                    strdata = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_WorkForce_Budget_Get", Request.QueryString("id"))

                    If strdata.Tables(0).Rows.Count > 0 Then

                        lblid.Text = strdata.Tables(0).Rows(0).Item("id").ToString
                        acompany.Value = strdata.Tables(0).Rows(0).Item("company").ToString
                        aoffice.Value = strdata.Tables(0).Rows(0).Item("office").ToString
                        ayear.Value = strdata.Tables(0).Rows(0).Item("budgetyear").ToString
                        abudget.Value = strdata.Tables(0).Rows(0).Item("budget").ToString
                        Session("varYear") = strdata.Tables(0).Rows(0).Item("budgetyear").ToString
                        lbstat.InnerText = strdata.Tables(0).Rows(0).Item("budgetstat").ToString

                        If IsDBNull(strdata.Tables(0).Rows(0).Item("createdby")) = False Then
                            createdon.InnerText = "Created by " & strdata.Tables(0).Rows(0).Item("createdby").ToString & " on the " & CDate(strdata.Tables(0).Rows(0).Item("createdon"))
                        Else
                            createdon.Visible = False
                        End If

                        If IsDBNull(strdata.Tables(0).Rows(0).Item("updatedby")) = False Then
                            updatedon.InnerText = "Last modified by " & strdata.Tables(0).Rows(0).Item("updatedby").ToString & " on the " & CDate(strdata.Tables(0).Rows(0).Item("updatedon"))
                        Else
                            updatedon.Visible = False
                        End If



                    End If
                    LoadSummaryGrid(lblid.Text)
                    'LoadGrid(lblid.Text)

                End If
            End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Protected Sub SortRecords(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Try
            Dim sortExpression As String = e.SortExpression
            Session("workapprovalsort") = sortExpression
            Dim direction As String = String.Empty
            If SortsDirection = SortDirection.Ascending Then
                SortsDirection = SortDirection.Descending
                direction = " DESC"
            Else
                SortsDirection = SortDirection.Ascending
                direction = " ASC"
            End If
            Dim table As DataTable = LodaDataTable(lblid.Text, lblDate.Text) 'Process.GetData("Recruitment_Job_Post_get_all")
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
            Session("workapprovalindex") = e.NewPageIndex
            GridVwHeaderChckbox.DataSource = LodaDataTable(lblid.Text, lblDate.Text) 'Process.GetData("Recruitment_Job_Post_get_all")
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
    Protected Sub btnBack_Click(sender As Object, e As EventArgs)
        Try
            If Session("clicked") = 2 Then
                Response.Redirect("~/Module/Employee/Recruitment/WorkForcePlanning", True)
            Else
                Response.Redirect("~/Module/Employee/Recruitment/WorkForceBudgit", True)
            End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub lnkapprovalupdate_Click(sender As Object, e As EventArgs)
        Try
            Dim url As String = "WorkForceApprovalUpdate.aspx?id=" & lblid.Text
            Dim s As String = "window.open('" & url + "', 'popup_window', 'width=650,height=600,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no');"
            ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Protected Sub lnkApprovalStat_Click(sender As Object, e As EventArgs)
        Try
            Try
                Dim url As String = "WorkApprovalStatView.aspx?id=" & lblid.Text
                Dim s As String = "window.open('" & url + "', 'popup_window', 'width=600,height=600,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no');"
                ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)

            Catch ex As Exception
                Process.loadalert(divalert, msgalert, ex.Message, "danger")

            End Try
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridVwHeaderChckbox_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridVwHeaderChckbox.RowCreated
        Try
            Process.SortArrow(e, SortsDirection, Session("workapprovalsort"))
        Catch ex As Exception
        End Try

    End Sub
End Class