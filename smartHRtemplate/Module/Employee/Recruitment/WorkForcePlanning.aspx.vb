Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports System.IO

Public Class WorkForcePlanning
    Inherits System.Web.UI.Page
    Dim education As New clsEducation
    Dim Separators() As Char = {","c}
    Dim section As String = ""
    Dim workplan As String = "plan"
    Dim workbudget As String = "budget"
    Dim AuthenCode As String = "APPWFPLAN"

#Region "WFPlanApprove"
    Private Function GetWFPlanApproveGrid() As DataTable
        Dim Datas As New DataTable
        search.Value = Session("workplanningappsearch")
        Dim serach As String = cboPlanApproveStatus.SelectedValue
        Dim budget As Double = 0
        If search.Value.Trim = "" Then
            Datas = Process.SearchDataP4("Recruit_WorkForce_Approval_Get_All", cboCompany.SelectedValue, Session("UserEmpID"), cboPlanApproveStatus.SelectedValue, workplan)
        Else
            Datas = Process.SearchDataP5("Recruit_WorkForce_Approval_Search", cboCompany.SelectedValue, Session("UserEmpID"), cboPlanApproveStatus.SelectedValue, workplan, search.Value.Trim)
        End If
        If serach.ToLower = "approved" Then
            pagetitle.InnerText = cboCompany.SelectedValue & " Approvals: " & serach & " WorkForce Plan(" & FormatNumber(Datas.Rows.Count, 0) & ")"
        Else
            pagetitle.InnerText = cboCompany.SelectedValue & " " & serach & " WorkForce Plan For Approval(" & FormatNumber(Datas.Rows.Count, 0) & ")"
        End If
        If Datas.Rows.Count > 0 Then
            lbcurreny.InnerText = Datas.Rows(0).Item("currency").ToString
            For i As Integer = 0 To Datas.Rows.Count - 1
                budget = budget + CDbl(Datas.Rows(i).Item("budget").ToString)
            Next
        End If
        lbbudget.InnerText = FormatNumber(budget, 2)
        Return Datas
    End Function

    Private Sub LoadWFPlanApproveGrid()
        Try
            grdWFPlanApprove.PageIndex = CInt(Session("workplanningappindex"))
            grdWFPlanApprove.DataSource = GetWFPlanApproveGrid()
            grdWFPlanApprove.DataBind()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Read") = False Then
                Process.loadalert(divalert, msgalert, Process.privilegemsg, "warning")
                Exit Sub
            End If

            '

            If Not Me.IsPostBack Then
                Session("clicked") = 2
                Dim ismulti As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select isnull(ismulticompany,'No')  from general_info")
                If ismulti.ToLower = "no" Then
                    Process.LoadRadComboTextAndValueP2(cboCompany, "Company_Structure_Get_ByLevel", "1", Process.GetCompanyName(""), "name", "name", False)
                Else
                    Process.LoadRadComboTextAndValueInitiateP2(cboCompany, "Company_Structure_Get_ByLevel", "2", Process.GetCompanyName(""), "All Companies", "name", "name")
                End If

                If Session("workplanningappcompany") Is Nothing Then
                    Session("workplanningappcompany") = Session("organisation")
                End If
                Process.AssignRadComboValue(cboCompany, Session("workplanningappcompany"))

                If Session("workplanningappstatus") Is Nothing Then
                    Session("workplanningappstatus") = "Pending"
                End If
                Process.AssignRadComboValue(cboPlanApproveStatus, Session("workplanningappstatus"))

                If Session("workplanningappindex") Is Nothing Then
                    Session("workplanningappindex") = "0"
                End If

                If Session("workplanningappsearch") Is Nothing Then
                    Session("workplanningappsearch") = ""
                End If

                If Session("UserEmpID") IsNot Nothing Then
                    LoadWFPlanApproveGrid()
                End If

            End If


        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Private Sub grdWFPlanApprove_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdWFPlanApprove.RowCreated
        Try
            Process.SortArrow(e, SortsDirection, Session("workplanningappsort"))
        Catch ex As Exception
        End Try

    End Sub

    Protected Sub grdWFPlanApprove_OnRowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdWFPlanApprove.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(grdWFPlanApprove, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub
    Protected Sub SortRecords(sender As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs)
        Try
            Dim sortExpression As String = e.SortExpression
            Session("workplanningappsort") = sortExpression
            Dim direction As String = String.Empty
            If SortsDirection = SortDirection.Ascending Then
                SortsDirection = SortDirection.Descending
                direction = " DESC"
            Else
                SortsDirection = SortDirection.Ascending
                direction = " ASC"
            End If
            Dim table As DataTable = GetWFPlanApproveGrid()
            table.DefaultView.Sort = sortExpression & direction
            grdWFPlanApprove.PageIndex = CInt(Session("workplanningappindex"))
            grdWFPlanApprove.DataSource = table
            grdWFPlanApprove.DataBind()
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

    Protected Sub grdWFPlanApprove_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdWFPlanApprove.PageIndexChanging
        Try
            Session("workplanningappindex") = e.NewPageIndex
            LoadWFPlanApproveGrid()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
            '
        End Try
    End Sub


    Protected Sub btnFind_Click(sender As Object, e As EventArgs)
        Try
            Session("workplanningappsearch") = search.Value
            LoadWFPlanApproveGrid()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub


    Protected Sub cboCompany_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboCompany.SelectedIndexChanged
        Try
            Session("workplanningappcompany") = cboCompany.SelectedValue
            Session("workplanningappsearch") = ""
            search.Value = ""
            Session("workplanningappindex") = "0"
            LoadWFPlanApproveGrid()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnApprove_Click(sender As Object, e As EventArgs) Handles btnApprove.Click
        Try

            Dim lblstatus As String = ""
            Dim confirmValue As String = Request.Form("confirm_app")
            If confirmValue = "No" Then
                lblstatus = "Multiple Approval cancelled"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
            Else

                Dim dept As String = ""
                Dim budgetyear As String = ""
                System.Threading.Thread.Sleep(300)
                Dim atLeastOneRowDeleted As Boolean = False
                ' Iterate through the Products.Rows property
                For Each row As GridViewRow In grdWFPlanApprove.Rows
                    ' Access the CheckBox
                    Dim cb As CheckBox = row.FindControl("chkEmp")
                    If cb IsNot Nothing AndAlso cb.Checked Then
                        atLeastOneRowDeleted = True

                        Dim ID As Integer = (grdWFPlanApprove.DataKeys(row.RowIndex).Value)
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_WorkForce_Approval_Update", ID, "Approved", "Approved", "0", Session("UserEmpID"))

                        Dim strUser As New DataSet
                        strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_WorkForce_Budget_Get", ID)
                        If strUser.Tables(0).Rows.Count > 0 Then

                            Dim approver As String = "n/a"
                            If Session("UserEmpID") = strUser.Tables(0).Rows(0).Item("approver1").ToString Then
                                approver = strUser.Tables(0).Rows(0).Item("approver2").ToString
                            ElseIf Session("UserEmpID") = strUser.Tables(0).Rows(0).Item("approver2").ToString Then
                                approver = strUser.Tables(0).Rows(0).Item("approver3").ToString
                            ElseIf Session("UserEmpID") = strUser.Tables(0).Rows(0).Item("approver3").ToString Then
                                approver = strUser.Tables(0).Rows(0).Item("approver4").ToString
                            End If

                            dept = strUser.Tables(0).Rows(0).Item("office").ToString
                            budgetyear = strUser.Tables(0).Rows(0).Item("budgetyear").ToString

                            If approver.ToLower <> "n/a" Then
                                Process.requestedURL = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "menu_get_link", AuthenCode, 3)
                                Process.Work_Force_To_Approvers("Plan", budgetyear, dept, approver, Process.ApplicationURL & "/" & Process.requestedURL)
                            Else
                                Process.requestedURL = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "menu_get_link", AuthenCode, 1)
                                Process.Work_Force_Approvals_Complete("Budget", budgetyear, dept, Process.ApplicationURL & "/" & Process.requestedURL)
                            End If

                        End If
                        Process.requestedURL = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "menu_get_link", AuthenCode, 1)
                        Process.Work_Force_Approvals("Plan", budgetyear, dept, Session("UserEmpID"), Session("EmpName"), "Approved", "", Process.ApplicationURL & Process.requestedURL)


                    End If
                Next
                If atLeastOneRowDeleted = True Then
                    LoadWFPlanApproveGrid()
                    lblstatus = "Multiple Work Force Plan successfully approved"
                    Process.loadalert(divalert, msgalert, lblstatus, "success")
                Else
                    lblstatus = "Multiple Approval cancelled, no selection made"
                    Process.loadalert(divalert, msgalert, lblstatus, "info")
                End If

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub cboPlanApproveStatus_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboPlanApproveStatus.SelectedIndexChanged

        Try
            Session("workplanningappstatus") = cboPlanApproveStatus.SelectedValue
            Session("workplanningappsearch") = ""
            search.Value = ""
            Session("workplanningappindex") = "0"

            If cboPlanApproveStatus.SelectedItem.Text.ToLower = "approved" Then
                btnApprove.Visible = False
            Else
                btnApprove.Visible = True
            End If

            LoadWFPlanApproveGrid()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
End Class