Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports Telerik.Web.UI
Public Class TimeSheet
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "EMPTIMESHEET"
   
    Private Sub GtCalendarView()
        Try
            schTmeSheet.DataStartField = "Activity Date"
            schTmeSheet.DataSubjectField = "Activity"
            schTmeSheet.DataEndField = "Activity End Date"
            schTmeSheet.DataDescriptionField = "Project"
            schTmeSheet.DataKeyField = "id"

            schTmeSheet.DataSource = Process.SearchDataP2("Time_Sheet_Get_All_Calendar", Session("UserEmpID"), "All Projects") 'Process.SearchDataP4("Employee_Leavelist_get_all", "", "Taken", Date.Now.AddMonths(1 - Date.Now.Month).AddDays(1 - Date.Now.Day), Date.Now.AddMonths(12 - Date.Now.Month).AddDays(31 - Date.Now.Day))
            schTmeSheet.DataBind()
            schTmeSheet.SelectedView = SchedulerViewType.MonthView
            schTmeSheet.Visible = True
        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub
    Private Function GetTimeSheetTable(LoadType As String) As DataTable
        'Try
        Dim sData As New DataTable

        If LoadType = "All" Then
            sData = Process.SearchDataP2("Time_Sheet_get_all", Session("UserEmpID"), cboProject.SelectedItem.Value)
        ElseIf LoadType = "Find" Then
            sData = Process.SearchDataP3("Time_Sheet_search", Session("UserEmpID"), cboProject.SelectedItem.Value, txtsearch.Value)
        End If
        pagetitle.InnerText = cboProject.SelectedItem.Value & ": " & txtsearch.Value & " My Time Sheet"
        Return sData
        'Catch ex As Exception
        '    Return Nothing
        'End Try
    End Function
    Private Sub LoadTimeSheets()
        Try

            GridVwHeaderChckbox.DataSource = GetTimeSheetTable(Session("LoadType"))
            GridVwHeaderChckbox.AllowSorting = True
            GridVwHeaderChckbox.AllowPaging = True
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub
   
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Process.AuthenAction(Session("role"), AuthenCode, "Read") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "danger")
                Exit Sub
            End If

            If Not Me.IsPostBack Then
                Process.RadioListCheck(rdoViewList, "Calendar View")
                MultiView1.ActiveViewIndex = 1
                Session("pageIndex1") = 0

                Session("LoadType") = "All"

                Process.LoadRadComboTextAndValueInitiateP1(cboProject, "Time_Projects_Get_My", Session("UserEmpID"), "All Projects", "Projects", "id")
                Process.AssignRadComboValue(cboProject, "All Projects")
                GtCalendarView()
                schTmeSheet.SelectedDate = Date.Now
                LoadTimeSheets()
            End If
        Catch ex As Exception
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
            Dim table As New DataTable

            table = GetTimeSheetTable(Session("LoadType"))
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
            GridVwHeaderChckbox.DataSource = GetTimeSheetTable(Session("LoadType"))
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
   

    Protected Sub Button1_Click(sender As Object, e As EventArgs)
        Try
            'If Not Me.IsPostBack Then
            If txtsearch.Value.Trim Is Nothing Then
                Session("LoadType") = "All"
            Else
                Session("LoadType") = "Find"
            End If
            LoadTimeSheets()



            'End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub Delete(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Delete") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "danger")
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
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Time_Sheet_Delete", ID)
                    End If
                Next
                LoadTimeSheets()
                GtCalendarView()
            Else
                Process.loadalert(divalert, msgalert, "Delete has been cancelled", "danger")
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnApply_Click(sender As Object, e As EventArgs)
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "danger")
                Exit Sub
            End If
            Process.loadtype = "Add"
            Response.Redirect("~/Module/TimeManagement/TimeSheetUpdate.aspx", True)
            Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Private Sub schTmeSheet_AppointmentClick(sender As Object, e As Telerik.Web.UI.SchedulerEventArgs) Handles schTmeSheet.AppointmentClick
        Try
            ScriptManager.RegisterStartupScript(Page, GetType(Page), "OpenWindow", "window.open('TimeSheetUpdate.aspx?id= " & e.Appointment.ID.ToString & "','mywindow','menubar=1,resizable=1,width=650,height=800');", True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
        'e.Appointment.ID
    End Sub

    Protected Sub rdoViewList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rdoViewList.SelectedIndexChanged
        'MultiView1.ActiveViewIndex = 1
        Try
            MultiView1.ActiveViewIndex = CInt(rdoViewList.SelectedValue)

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnApply0_Click(sender As Object, e As EventArgs) Handles btnApply0.Click
        btnApply_Click(sender, e)
    End Sub
End Class