Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Public Class JobInterviews
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "JOBINTERVIEW"

    Private Function LodaDataTable() As DataTable

        Dim Datas As New DataTable
        search.Value = Session("interviewsearch")
        If Session("interviewsearch") = "" Then
            Datas = Process.SearchData("Recruit_Job_Interview_Get_All", cbocompany.SelectedValue)
        Else

            Datas = Process.SearchDataP2("Recruit_Job_Interview_Search", cbocompany.SelectedValue, search.Value.Trim)
        End If
        pagetitle.InnerText = cbocompany.SelectedValue & ": Interviews (" & Datas.Rows.Count.ToString & ")"
        Return Datas
    End Function

    Private Sub LoadGrid()
        Try
            GridVwHeaderChckbox.PageIndex = CInt(Session("interviewpageindex"))
            GridVwHeaderChckbox.DataSource = LodaDataTable()
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
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "warning")
                Exit Sub
            End If



            If Not Me.IsPostBack Then
                Dim ismulti As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select isnull(ismulticompany,'No')  from general_info")
                If ismulti.ToLower = "no" Then
                    Process.LoadRadComboTextAndValueP2(cbocompany, "Company_Structure_Get_ByLevel", "1", Session("Access"), "name", "name", False)
                Else
                    Process.LoadRadComboTextAndValueP2(cbocompany, "Company_Structure_Get_ByLevel", "2", Session("Access"), "name", "name", False)
                End If

                If Session("interviewcompany") Is Nothing Then
                    Session("interviewcompany") = Session("Organisation")
                End If

                If Session("interviewsearch") Is Nothing Then
                    Session("interviewsearch") = ""
                End If
                If Session("interviewpageindex") Is Nothing Then
                    Session("interviewpageindex") = "0"
                End If

                Process.AssignRadComboValue(cbocompany, Session("interviewcompany"))

                LoadGrid()
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")            
        End Try
    End Sub

    Protected Sub SortRecords(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Try
            Dim sortExpression As String = e.SortExpression
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
            GridVwHeaderChckbox.PageIndex = CInt(Session("interviewpageindex"))
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
            Session("interviewpageindex") = e.NewPageIndex
            GridVwHeaderChckbox.DataSource = LodaDataTable(Session("LoadType")) 'Process.GetData("Recruitment_Job_Post_get_all")
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
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
            If search.Value.Trim = "" Then
                Session("interviewsearch") = ""
            Else
                Session("interviewsearch") = search.Value.Trim
            End If

            LoadGrid()

        Catch ex As Exception

        End Try
    End Sub


    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "warning")
                Exit Sub
            End If
            Response.Redirect("~/Module/Recruitment/JobInterviewUpdate", True)

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")            
        End Try
    End Sub

    Protected Sub Delete(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
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
                        atLeastOneRowDeleted = True
                        ' First, get the ProductID for the selected row
                        Dim ID As String = _
                            Convert.ToString(GridVwHeaderChckbox.DataKeys(row.RowIndex).Value)
                        ' Delete the row
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Job_Interview_delete", ID)
                    End If
                Next
                Process.loadalert(divalert, msgalert, count.ToString & " records successfully deleted", "success")
                LoadGrid()
          
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")            
        End Try
    End Sub


    Protected Sub radOffice_SelectedIndexChanged1(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cbocompany.SelectedIndexChanged
        Try
            Session("interviewcompany") = cbocompany.SelectedValue
            Session("interviewsearch") = ""
            LoadGrid()
        Catch ex As Exception

        End Try
    End Sub
End Class