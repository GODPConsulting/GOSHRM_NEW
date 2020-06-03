Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports System.IO
Imports Telerik.Web.UI


Public Class WorkForce
    Inherits System.Web.UI.Page
    Dim education As New clsEducation
    Dim Separators() As Char = {","c}
    Dim section As String = ""
    Dim workplan As String = "plan"
    Dim workbudget As String = "budget"
    Dim AuthenCode As String = "EMPWFPLAN"
#Region "WFPlan"
    Private Function GetWFPlanGrid() As DataTable
        Dim Datas As New DataTable

        search.Value = Session("workforcesearch").ToString.Trim

        If search.Value = "" Then
            Datas = Process.SearchDataP3("Recruit_WorkForce_Plan_get_all", cboyear.SelectedValue, Session("UserEmpID"), cbostatus.SelectedValue)
        Else
            Datas = Process.SearchDataP4("Recruit_WorkForce_Plan_search", cboyear.SelectedValue, Session("UserEmpID"), cbostatus.SelectedValue, search.Value)
        End If
        pagetitle.InnerText = cboyear.SelectedValue & " " & cbostatus.SelectedValue & ": WorkForce Plan(" & Datas.Rows.Count & ")"
        Return Datas
    End Function

    Private Sub LoadWFPlanGrid()
        Try
            GridVwHeaderChckbox.AllowPaging = True
            GridVwHeaderChckbox.AllowSorting = True
            GridVwHeaderChckbox.PageIndex = CInt(Session("workforceindex"))
            GridVwHeaderChckbox.DataSource = GetWFPlanGrid()
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub
#End Region





    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Read") = False Then
                Process.loadalert(divalert, msgalert, Process.privilegemsg, "warning")
                Exit Sub
            End If

            If Not Me.IsPostBack Then

                Process.LoadRadComboTextAndValueP1(cboyear, "Recruit_WorkForce_Plan_Get_Years", Session("userempid"), "budgetyear", "budgetyear", False)

                If Session("workforceyear") Is Nothing Then
                    If cboyear.Items.Count > 0 Then
                        Session("workforceyear") = cboyear.SelectedValue
                    Else
                        Session("workforceyear") = "0"
                    End If
                End If
                Process.AssignRadComboValue(cboyear, Session("workforceyear"))

                If Session("workforcesearch") Is Nothing Then
                    Session("workforcesearch") = ""
                End If

                If Session("workforcestatus") Is Nothing Then
                    Session("workforcestatus") = "Pending"
                End If
                Process.AssignRadComboValue(cbostatus, Session("workforcestatus"))

                If Session("workforceindex") Is Nothing Then
                    Session("workforceindex") = "0"
                End If

                LoadWFPlanGrid()

            End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Private Sub GridVwHeaderChckbox_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridVwHeaderChckbox.RowCreated
        Try
            Process.SortArrow(e, SortsDirection, Session("workforcesort"))
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
            Session("workforcesort") = sortExpression
            Dim direction As String = String.Empty
            If SortsDirection = SortDirection.Ascending Then
                SortsDirection = SortDirection.Descending
                direction = " DESC"
            Else
                SortsDirection = SortDirection.Ascending
                direction = " ASC"
            End If
            Dim table As DataTable = GetWFPlanGrid() 'Process.GetData("Recruitment_Job_Post_get_all")
            table.DefaultView.Sort = sortExpression & direction            
            GridVwHeaderChckbox.PageIndex = CInt(Session("workforceindex"))
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
            Session("workforceindex") = e.NewPageIndex
            LoadWFPlanGrid()
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
                        Dim ID As String = Convert.ToString(GridVwHeaderChckbox.DataKeys(row.RowIndex).Value)
                        ' "Delete" the row
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_WorkForce_Plan_delete", ID)
                    End If
                Next
                Process.loadalert(divalert, msgalert, count.ToString & " records successfully deleted", "success")
                LoadWFPlanGrid()
            
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")            
        End Try
    End Sub

    Protected Sub btnFind_Click(sender As Object, e As EventArgs)
        Try
            Session("workforcesearch") = search.Value.Trim
            LoadWFPlanGrid()
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

            Response.Redirect("~/Module/Employee/Recruitment/WorkForcePlanUpdate.aspx", True) 
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")            
        End Try
    End Sub


    Protected Sub cboStatus_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cbostatus.SelectedIndexChanged
        Session("workforcestatus") = cbostatus.SelectedValue
        Session("workforceindex") = 0
        LoadWFPlanGrid()
    End Sub

    Private Sub cboCompany_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboyear.SelectedIndexChanged
        Try
            Session("workforceindex") = 0
            Session("workforceyear") = cboYear.SelectedValue
            LoadWFPlanGrid()
        Catch ex As Exception

        End Try
    End Sub
End Class