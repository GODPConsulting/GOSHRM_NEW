Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO

Public Class EmployeeTrainingSession
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "EMPTRAINSESSION"
    Private Function GetTable(LoadType As String) As DataTable
        Dim DD As New DataTable
        If Request.QueryString("sessionid") Is Nothing Then
            If LoadType = "All" Then
                DD = Process.SearchData("Employee_Training_Sessions_get_all", Session("company"))
            ElseIf LoadType = "Find" Then
                DD = Process.SearchDataP2("Employee_Training_Sessions_search", Session("company"), search.Value.Trim)
            End If
            pagetitle.InnerText = cboCompany.SelectedValue & ": " & search.Value & " Employee Training Sessions"
        Else
            If LoadType = "All" Then
                DD = Process.SearchDataP2("Employee_Training_Sessions_get_all_1", Session("company"), Request.QueryString("sessionid"))
            ElseIf LoadType = "Find" Then
                DD = Process.SearchDataP3("Employee_Training_Sessions_search_1", Session("company"), search.Value.Trim, Request.QueryString("sessionid"))
            End If

            Dim strUser As New DataSet
            strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Training_Sessions_get", Request.QueryString("sessionid"))
            If strUser.Tables(0).Rows.Count > 0 Then
                pagetitle.InnerText = cboCompany.SelectedValue & ": " & search.Value & strUser.Tables(0).Rows(0).Item("name").ToString & " Employee Training Sessions"
            Else
                pagetitle.InnerText = cboCompany.SelectedValue & ": " & search.Value & " Employee Training Sessions"
            End If
        End If
        Return DD
    End Function
    Private Sub LoadGrid(LoadType As String, page As Integer)
        Try
            If Request.QueryString("sessionid") Is Nothing Then
                btnBack.Visible = False
            Else
                btnBack.Visible = True
            End If
            GridVwHeaderChckbox.PageIndex = page
            GridVwHeaderChckbox.DataSource = GetTable(LoadType)
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
                If Request.QueryString("sessionid") IsNot Nothing Then
                    If Request.UrlReferrer.ToString.Contains("EmployeeTrainingSessionUpdate") = True Then
                        'Session("PreviousPage") = Request.UrlReferrer.ToString
                        Session("PreviousPage") = "trainingsessions"
                    End If
                End If

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
                '
                Session("pageIndex1") = 0
                Session("LoadType") = "All"
                LoadGrid(Session("LoadType"), Session("pageIndex1"))
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "warning")
        End Try
    End Sub

    Private Sub GridVwHeaderChckbox_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridVwHeaderChckbox.RowDataBound
        Try
            Dim chcount As HtmlGenericControl = e.Row.FindControl("datscore")
            Dim datprogress As HtmlGenericControl = e.Row.FindControl("datprogress")
            Dim htmlclass As String = ""
            Dim htmlstyle As String = ""
            Dim htmltitle As String = ""
            If IsNumeric(chcount.InnerText.Replace("%", "")) Then
                If (CInt(chcount.InnerText.Replace("%", "")) > 69) Then
                    htmlclass = "progress-bar progress-bar-success"
                ElseIf (CInt(chcount.InnerText.Replace("%", "")) > 49) And (CInt(chcount.InnerText.Replace("%", "")) <= 69) Then
                    htmlclass = "progress-bar progress-bar-info"
                ElseIf (CInt(chcount.InnerText.Replace("%", "")) > 39) And (CInt(chcount.InnerText.Replace("%", "")) <= 49) Then
                    htmlclass = "progress-bar progress-bar-warning"
                Else
                    htmlclass = "progress-bar progress-bar-danger"
                End If


                htmlstyle = "width:" + chcount.InnerText
                htmltitle = chcount.InnerText + " training accomplishment"
                datprogress.Attributes.Add("class", htmlclass)
                datprogress.Attributes.Add("style", htmlstyle)
                datprogress.Attributes.Add("title", htmltitle)
            Else
                datprogress.Visible = False
                chcount.Visible = False
            End If


        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs)
        Try
            Dim Pages As String = "Training Status"
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "danger")
                Exit Sub
            End If

            If FileUpload1.PostedFile IsNot Nothing Then
                'To create a PostedFile
                Dim csvPath As String = Server.MapPath(Process.FileURL) + Path.GetFileName(FileUpload1.PostedFile.FileName)
                If File.Exists(csvPath) = True Then
                    File.Delete(csvPath)
                End If
                FileUpload1.PostedFile.SaveAs(csvPath)
                If Process.Import(csvPath, "Training_Session_trainee_Update_Upload", Pages) = True Then
                    Process.loadalert(divalert, msgalert, "Uploaded " & Session("uploadcnt") & " record(s)", "success")
                Else
                    Process.loadalert(divalert, msgalert, Process.strExp, "danger")
                End If
                Process.GetAuditTrailInsertandUpdate("", "Uploaded " & Session("uploadcnt") & " record(s) from " & csvPath, "File Upload", Pages)
            End If
            LoadGrid(Session("LoadType"), Session("pageIndex1"))
            'Response.Write("<script language='javascript'> { self.close() }</script>")
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

    Protected Sub Button1_Click(sender As Object, e As EventArgs)
        Try
            'If Not Me.IsPostBack Then
            If search.Value.Trim = "" Then
                Session("LoadType") = "All"
            Else
                Session("LoadType") = "Find"
            End If
            Session("pageIndex1") = 0
            LoadGrid(Session("LoadType"), 0)
            'End If
        Catch ex As Exception
            Response.Write(ex.Message)
            Process.loadalert(divalert, msgalert, ex.Message, "warning")
        End Try
    End Sub


    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "danger")
                Exit Sub
            End If
            Dim url As String = "EmployeeTrainingSessionUpdate.aspx?sessionid=" & Request.QueryString("sessionid")
            Response.Redirect(url, True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnFind1_Click(sender As Object, e As EventArgs)

    End Sub
    Protected Sub Delete(sender As Object, e As EventArgs)
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
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Employee_Training_Sessions_delete", ID)
                    End If
                Next
                LoadGrid(Session("LoadType"), Session("pageIndex1"))
            Else
                Process.loadalert(divalert, msgalert, "Delete has been cancelled", "danger")
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Try
            'Response.Redirect(Session("PreviousPage").ToString)
            Response.Redirect("~/Module/Trainings/Settings/TrainingSessions")
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub cboCompany_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboCompany.SelectedIndexChanged
        Try
            Session("LoadType") = "All"
            Session("pageIndex1") = 0
            Session("company") = cboCompany.SelectedValue
            LoadGrid(Session("LoadType"), Session("pageIndex1"))
        Catch ex As Exception

        End Try
    End Sub
End Class