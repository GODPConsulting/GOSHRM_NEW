Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Public Class TrainingSessions
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "TRAINSESSION"
    Private Function GetTable(LoadType As String) As DataTable
        Dim DD As New DataTable
        If Session("LoadType") = "All" Then
            DD = Process.SearchDataP2("Training_Sessions_get_all", Session("company"), Session("trainingyear"))
        ElseIf Session("LoadType") = "Find" Then
            DD = Process.SearchDataP3("Training_Sessions_search", Session("company"), Session("trainingyear"), txtsearch.Value.Trim)
        End If
        pagetitle.InnerText = cboCompany.SelectedValue & ": " & txtsearch.Value & " Training Sessions (" & FormatNumber(DD.Rows.Count, 0) & ")"
        Return DD
    End Function
    Private Sub LoadGrid(LoadType As String, page As Integer)
        Try
            GridVwHeaderChckbox.PageIndex = page
            GridVwHeaderChckbox.DataSource = GetTable(LoadType)
            GridVwHeaderChckbox.AllowSorting = True
            GridVwHeaderChckbox.AllowPaging = True
            GridVwHeaderChckbox.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message)
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Read") = False Then
                content.Style.Add("display", "none")
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform view this page", "info")
                Exit Sub
            End If
            If Not Me.IsPostBack Then
                Dim ismulti As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select isnull(ismulticompany,'No')  from general_info")
                If ismulti.ToLower = "no" Then
                    Process.LoadRadComboTextAndValueP2(cboCompany, "Company_Structure_Get_ByLevel", "1", Session("Access"), "name", "name", False)
                Else
                    Process.LoadRadComboTextAndValueP2(cboCompany, "Company_Structure_Get_ByLevel", "2", Session("Access"), "name", "name", False)
                End If
                If Session("company") Is Nothing Then
                    Session("company") = Session("Organisation")
                End If
                Process.AssignRadComboValue(cboCompany, Session("company"))
                Process.LoadRadComboTextAndValueP1(cboyear, "Surbodinate_Training_Sessions_Years", Session("UserEmpID"), "Year", "year", False)
                'Process.LoadRadComboTextAndValueP1(cboyear, "Employee_Training_Trainer_Years", Session("UserEmpID"), "year", "year", False)
                Session("trainingyear") = Date.Now.Year
                If cboyear.Items.Count > 0 Then
                    If Session("trainingyear") Is Nothing Then
                        Session("trainingyear") = Date.Now.Year
                    Else
                        Process.AssignRadComboValue(cboyear, Session("trainingyear"))
                    End If
                Else
                    Session("trainingyear") = Date.Now.Year
                End If
                Process.AssignRadComboValue(cboyear, Session("trainingyear"))
                Session("pageIndex1") = 0
                Session("LoadType") = "All"
                LoadGrid(Session("LoadType"), 0)
            End If
        Catch ex As Exception
            Response.Write(ex.message)
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub SortRecords(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
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
            Dim table As DataTable = GetTable(Session("LoadType"))
            table.DefaultView.Sort = sortExpression & direction
            GridVwHeaderChckbox.PageIndex = Session("pageIndex1")
            GridVwHeaderChckbox.DataSource = table
            GridVwHeaderChckbox.DataBind()
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


    Protected Sub GridVwHeaderChckbox_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridVwHeaderChckbox.PageIndexChanging
        Try
            GridVwHeaderChckbox.PageIndex = e.NewPageIndex
            Session("pageIndex1") = e.NewPageIndex
            GridVwHeaderChckbox.DataSource = GetTable(Session("LoadType"))
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

    Private Sub cboyear_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboyear.SelectedIndexChanged
        Try

            Session("trainingsearch") = ""

            Session("trainingindex") = "0"
            Session("trainingyear") = cboyear.Text
            Session("LoadType") = "All"
            LoadGrid(Session("LoadType"), 0)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs)
        Try
            'If Not Me.IsPostBack Then
            If txtsearch.Value.Trim = "" Then
                Session("LoadType") = "All"
            Else
                Session("LoadType") = "Find"
            End If
            Session("pageIndex1") = 0
            LoadGrid(Session("LoadType"), Session("pageIndex1"))
            'End If
        Catch ex As Exception
            Response.Write(ex.Message)
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                Response.Write("You don't have privilege to perform this action")
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
                Exit Sub
            End If
            Process.loadtype = "Add"
            Response.Redirect("~/Module/Trainings/Settings/TrainingSessionsUpdate", True)
        Catch ex As Exception
            Response.Write(ex.Message)
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub Delete(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Delete") = False Then
                Response.Write("You don't have privilege to perform this action")
                Process.loadalert(divalert, msgalert, Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", "danger")
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
                        ' Delete row! (Well, not really...)
                        atLeastOneRowDeleted = True
                        ' First, get the ProductID for the selected row
                        Dim ID As String = _
                            Convert.ToString(GridVwHeaderChckbox.DataKeys(row.RowIndex).Value)
                        ' "Delete" the row
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Training_Sessions_delete", ID)
                    End If
                Next
                LoadGrid(Session("LoadType"), Session("pageIndex1"))
            Else
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Delete has been cancelled" + "')", True)
            End If
        Catch ex As Exception
            Response.Write(ex.message)
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub cboCompany_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboCompany.SelectedIndexChanged
        Session("company") = cboCompany.SelectedValue
        LoadGrid(Session("LoadType"), 0)
    End Sub
End Class