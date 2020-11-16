
Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Imports Telerik.Web.UI
Imports System.Web.UI
Imports System.Threading
Public Class Appraisals
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "APPRAISALPERIOD"
    Dim Pages As String = "Appraisal Adjustment"
    Dim FileURL As String = ConfigurationManager.AppSettings("FileURL")
    Private Function LoadMyGoals(loadtype As String, cycleid As Integer) As DataTable
        Dim datatables As New DataTable
        If Session("LoadType") = "All" Then
            datatables = Process.SearchData("Performance_Appraisal_Summary_Get_All", cycleid)
        ElseIf Session("LoadType") = "Find" Then
            datatables = Process.SearchDataP2("Performance_Appraisal_Summary_Search", cycleid, txtsearch.Value.Trim)
        End If
        'If datatables.Rows.Count < 1 Then
        '    btnUpload.Disabled = False
        '    FileUpload1.EnableViewState = False
        'Else
        '    btnUpload.Disabled = True
        '    FileUpload1.EnableViewState = True
        'End If
        Return datatables
    End Function

    Private Sub LoadGrid(loadtype As String, cycleid As Integer)
        Try
            GridVwHeaderChckbox.DataSource = LoadMyGoals(loadtype, cycleid)
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
                content.Style.Add("display", "none")
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform view this page", "info")
                Exit Sub
            End If
            If Not Me.IsPostBack Then
                Session("PreviousPage") = Request.UrlReferrer.ToString
                lblcycleid.Text = Request.QueryString("cycleid")
                Dim strForm As New DataSet
                strForm = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Appraisal_Cycle_Get", lblcycleid.Text)
                pagetitle.InnerText = strForm.Tables(0).Rows(0).Item("company").ToString() & " Appraisal: " & strForm.Tables(0).Rows(0).Item("period").ToString()
                Session("LoadType") = "All"
                LoadGrid(Session("LoadType"), lblcycleid.Text)
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
            Dim table As DataTable = LoadMyGoals(Session("LoadType"), lblcycleid.Text)
            table.DefaultView.Sort = sortExpression & direction
            GridVwHeaderChckbox.DataSource = table
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
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

    Protected Sub btnExport_Click(sender As Object, e As EventArgs)
        Try
            If Process.ExportExcel(LoadMyGoals(Session("LoadType"), lblcycleid.Text), "appraisals") = False Then
                Response.Write(Session("exception"))
            End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "success")
        End Try
    End Sub

    Protected Sub GridVwHeaderChckbox_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridVwHeaderChckbox.PageIndexChanging
        Try
            GridVwHeaderChckbox.PageIndex = e.NewPageIndex
            GridVwHeaderChckbox.DataSource = LoadMyGoals(Session("LoadType"), lblcycleid.Text)
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
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
            If txtsearch.Value.Trim = "" Then
                Session("LoadType") = "All"
            Else
                Session("LoadType") = "Find"
            End If
            LoadGrid(Session("LoadType"), lblcycleid.Text)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs)
        Try
            'Response.Write("<script language='javascript'> { self.close() }</script>")
            If Session("PreviousPage").ToString.Contains("AppraisalsFeedback") Then
                Response.Redirect("Settings/AppraisalPeriodList")
            Else
                Response.Redirect(Session("PreviousPage").ToString)
            End If

        Catch ex As Exception

        End Try
    End Sub


    Private Class DeleteObj
        Public Property Id As Integer
        Public Property EmpId As String
        Public Property Name As String
        Public Property EndPeriod As String
        Public Property StartPeriod As String
        Public Property Score As String
        Public Property ReviewYear As String
        Public Property CreatedBy As String
    End Class

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
                        Dim ID As String =
                            Convert.ToString(GridVwHeaderChckbox.DataKeys(row.RowIndex).Value)

                        Dim appraisalDeleted As New DeleteObj()
                        Dim strUser As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Appraisal_Summary_Get", ID)
                        appraisalDeleted.EmpId = strUser.Tables(0).Rows(0).Item("EmpID").ToString
                        appraisalDeleted.StartPeriod = strUser.Tables(0).Rows(0).Item("StartPeriod")
                        appraisalDeleted.EndPeriod = strUser.Tables(0).Rows(0).Item("EndPeriod")
                        appraisalDeleted.Name = strUser.Tables(0).Rows(0).Item("empname").ToString
                        appraisalDeleted.ReviewYear = strUser.Tables(0).Rows(0).Item("ReviewYear").ToString
                        appraisalDeleted.Score = strUser.Tables(0).Rows(0).Item("Score").ToString
                        appraisalDeleted.CreatedBy = Session("LoginID")
                        Dim OldValue As String = ""
                        Dim NewValue As String = ""

                        Dim j As Integer = 0

                        For Each a In GetType(DeleteObj).GetProperties() 'New Entries
                            If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
                                If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                                    If a.GetValue(appraisalDeleted, Nothing) = Nothing Then
                                        NewValue += a.Name + ":" + " " & vbCrLf
                                    Else
                                        NewValue += a.Name + ": " + a.GetValue(appraisalDeleted, Nothing).ToString & vbCrLf
                                    End If
                                End If
                            End If
                        Next
                        ' "Delete" the row
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_Summary_Delete_Admin", ID)
                        Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(NewValue, OldValue, "Deleted", "Employee Appraisal Page")
                    End If
                Next
                LoadGrid(Session("LoadType"), lblcycleid.Text)
            Else
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Delete has been cancelled" + "')", True)
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs)
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "danger")
                Exit Sub
            End If

            If FileUpload1.PostedFile IsNot Nothing Then

                'To create a PostedFile
                Dim csvPath As String = Server.MapPath(FileURL) + Path.GetFileName(FileUpload1.PostedFile.FileName)
                If File.Exists(csvPath) = True Then
                    File.Delete(csvPath)
                End If
                FileUpload1.PostedFile.SaveAs(csvPath)
                Dim proc As String = ""


                If Process.ImportWithUsersAndP1(csvPath, "Performance_Appraisal_Summary_Adjustment", lblcycleid.Text, Pages) = True Then
                    Process.loadalert(divalert, msgalert, "Uploaded " & Session("uploadcnt") & " record(s)", "success")
                Else
                    Response.Write(Process.strExp)
                End If
                Process.GetAuditTrailInsertandUpdate("", "Uploaded " & Session("uploadcnt") & " record(s) from " & FileUpload1.PostedFile.FileName, "File Upload", Pages)
                LoadGrid(Session("LoadType"), lblcycleid.Text)
            Else
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Choose file to upload" + "')", True)
                FileUpload1.Focus()
            End If

            'Response.Write("<script language='javascript'> { self.close() }</script>")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
End Class