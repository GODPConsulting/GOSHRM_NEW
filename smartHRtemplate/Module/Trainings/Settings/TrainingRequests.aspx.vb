Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Public Class TrainingRequests
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "EMPTRAINSESSION"
    Private Function GetTable(LoadType As String) As DataTable
        Dim DD As New DataTable
          If LoadType = "All" Then
            DD = Process.SearchData("Training_Sessions_Request_get_all", Request.QueryString("sessionid"))
        ElseIf LoadType = "Find" Then
            DD = Process.SearchDataP2("Training_Sessions_Request_search", search.Value.Trim, Request.QueryString("sessionid"))
        End If
        Return DD
    End Function

    Private Sub LoadGrid(LoadType As String)
        Try
            GridVwHeaderChckbox.DataSource = GetTable(LoadType)
            GridVwHeaderChckbox.AllowSorting = True
            GridVwHeaderChckbox.AllowPaging = True
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception
            response.write(ex.message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not Me.IsPostBack Then
                If Request.QueryString("sessionid") IsNot Nothing Then
                    Session("PreviousPage") = "~/Module/Trainings/Settings/TrainingSessions.aspx"

                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Training_Sessions_get", Request.QueryString("sessionid"))
                    If strUser.Tables(0).Rows.Count > 0 Then
                        pagetitle.InnerText = "Employee Training Requests: " & strUser.Tables(0).Rows(0).Item("Name").ToString
                    End If
                    btnBack.Visible = True
                Else
                    btnBack.Visible = False
                End If

                Session("LoadType") = "All"
                LoadGrid(Session("LoadType"))

            End If
        Catch ex As Exception
            Process.strExp = ex.Message
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    Protected Sub btnApprove_Click(sender As Object, e As EventArgs)
        Try
            Dim confirmValue As String = "Yes"
            If confirmValue = "Yes" Then
                System.Threading.Thread.Sleep(300)
                Dim atLeastOneRowDeleted As Boolean = False
                ' Iterate through the Products.Rows property
                For Each row As GridViewRow In GridVwHeaderChckbox.Rows
                    ' Access the CheckBox
                    Dim cb As CheckBox = row.FindControl("chkEmp")
                    If cb IsNot Nothing AndAlso cb.Checked Then
                        atLeastOneRowDeleted = True
                        Dim ID As String = Convert.ToString(GridVwHeaderChckbox.DataKeys(row.RowIndex).Value)
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Training_Sessions_Request_Update_Status_HR", ID, Session("UserEmpID"), "Approved")
                        Dim str As New DataSet
                        str = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Training_Sessions_Request_Get", ID)
                        Dim empid As String = str.Tables(0).Rows(0).Item("empid").ToString
                        Dim tid As Integer = str.Tables(0).Rows(0).Item("TrainingSessionID").ToString

                        Dim strUser As New DataSet
                        strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Training_Sessions_get", tid)
                        If strUser.Tables(0).Rows.Count > 0 Then
                            Dim tsession As String = strUser.Tables(0).Rows(0).Item("name").ToString
                            Dim trainingdate As Date = CDate(strUser.Tables(0).Rows(0).Item("scheduledtime"))
                            Dim duedate As Date = CDate(strUser.Tables(0).Rows(0).Item("duedate"))
                            Dim deliverymethod As String = strUser.Tables(0).Rows(0).Item("deliverymethod").ToString
                            Dim location As String = strUser.Tables(0).Rows(0).Item("deliverylocation").ToString
                            Dim trainer As String = ""
                            Dim coordinator = strUser.Tables(0).Rows(0).Item("coordinator").ToString
                            Dim trainingtime As String = strUser.Tables(0).Rows(0).Item("trainingtime").ToString

                            Process.Training_Notification_Trainees(tsession, trainer, coordinator, location, trainingdate, Process.AMPM_Time(trainingtime), duedate, empid, Process.ApplicationURL & "/" & Process.GetMailLink(AuthenCode, 2))
                        End If
                    End If
                Next
                If atLeastOneRowDeleted = True Then
                    Process.loadalert(divalert, msgalert, "Multiple Training Approved successful", "success")
                Else
                    Process.loadalert(divalert, msgalert, "Multiple Approval cancelled, no selection made", "success")
                    'ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Multiple Approval cancelled, no selection made" + "')", True)
                End If
                LoadGrid(Session("LoadType"))
            Else
                Process.loadalert(divalert, msgalert, "Multiple Approval cancelled, no selection made", "success")
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
            Dim table As DataTable = GetTable(Session("LoadType"))
            table.DefaultView.Sort = sortExpression & direction
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

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "danger")
                Exit Sub
            End If
            Response.Redirect("trainingrequestupdate", True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub GridVwHeaderChckbox_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridVwHeaderChckbox.PageIndexChanging
        Try
            GridVwHeaderChckbox.PageIndex = e.NewPageIndex
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

    Protected Sub Button1_Click(sender As Object, e As EventArgs)
        Try
            'If Not Me.IsPostBack Then
            If search.Value.Trim = "" Then
                Session("LoadType") = "All"
            Else
                Session("LoadType") = "Find"
            End If
            LoadGrid(Session("LoadType"))
            'End If
        Catch ex As Exception
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub


    Protected Sub btnFind1_Click(sender As Object, e As EventArgs)

    End Sub
    Protected Sub Delete(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Delete") = False Then
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
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
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Training_Sessions_Request_delete", ID)
                    End If
                Next
                LoadGrid(Session("LoadType"))
            Else
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Delete has been cancelled" + "')", True)
            End If
        Catch ex As Exception
            response.write(ex.message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Try
            Response.Redirect(Session("PreviousPage").ToString)
        Catch ex As Exception
            response.write(ex.message)
        End Try
    End Sub
End Class