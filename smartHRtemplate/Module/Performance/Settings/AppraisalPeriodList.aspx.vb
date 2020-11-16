Imports Microsoft.ApplicationBlocks.Data
Imports Microsoft.VisualBasic.FileIO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Imports Telerik.Web.UI
Public Class AppraisalPeriodList
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "APPRAISALPERIOD"
    Private Sub LoadGrid(LoadType As String)
        Try

            GridVwHeaderChckbox.DataSource = Process.SearchData("Performance_Appraisal_Cycle_get_all", cboCompany.SelectedValue)
            GridVwHeaderChckbox.AllowSorting = True
            GridVwHeaderChckbox.AllowPaging = True
            GridVwHeaderChckbox.DataBind()
            pagetitle.InnerText = cboCompany.SelectedValue & ": Appraisal Cycle List"

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
                Dim ismulti As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select isnull(ismulticompany,'No')  from general_info")

                If ismulti.ToLower = "no" Then
                    Process.LoadRadComboTextAndValueP2(cboCompany, "Company_Structure_Get_ByLevel", "1", Session("Access"), "name", "name", False)
                Else
                    Process.LoadRadComboTextAndValueP2(cboCompany, "Company_Structure_Get_ByLevel", "2", Session("Access"), "name", "name", False)
                End If

                If Session("company") <> "" Then
                    Process.AssignRadComboValue(cboCompany, Session("company"))
                Else
                    Process.AssignRadComboValue(cboCompany, Session("Organisation"))
                End If
                LoadGrid("All")
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
            Dim table As DataTable = Process.GetData("Performance_Appraisal_Cycle_get_all")
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

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) 'Handles btnUpload.Click
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                Process.loadalert(divalert, msgalert, Process.privilegemsg, "info")

                Exit Sub
            End If


            If Not file1.PostedFile Is Nothing Then
                'System.Threading.Thread.Sleep(300)
                'To create a PostedFile
                Dim csvPath As String = Server.MapPath(Process.FileURL) + Path.GetFileName(file1.PostedFile.FileName)
                If File.Exists(csvPath) = True Then
                    File.Delete(csvPath)
                End If
                file1.PostedFile.SaveAs(csvPath)

                If Import1(csvPath, "Performance_Appraisal_360_Upload_Reviewer", "") = True Then
                    Process.loadalert(divalert, msgalert, "Uploaded " & Session("uploadcnt") & " record(s)", "success")
                Else
                    Process.loadalert(divalert, msgalert, Session("exception"), "danger")
                End If

            End If
            Process.GetAuditTrailInsertandUpdate("", "Uploaded " & Session("uploadcnt") & " record(s) from " & file1.PostedFile.FileName, "File Upload", "")

            'Response.Write("<script language='javascript'> { self.close() }</script>")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub
    Protected Sub btnUpload_Click2(sender As Object, e As EventArgs) 'Handles btnUpload.Click
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                Process.loadalert(divalert, msgalert, Process.privilegemsg, "info")

                Exit Sub
            End If


            If Not file1.PostedFile Is Nothing Then
                'System.Threading.Thread.Sleep(300)
                'To create a PostedFile
                Dim csvPath As String = Server.MapPath(Process.FileURL) + Path.GetFileName(file1.PostedFile.FileName)
                If File.Exists(csvPath) = True Then
                    File.Delete(csvPath)
                End If
                file1.PostedFile.SaveAs(csvPath)

                If Import1(csvPath, "Performance_Appraisal_Upload_Reviewer", "") = True Then
                    Process.loadalert(divalert, msgalert, "Uploaded " & Session("uploadcnt") & " record(s)", "success")
                Else
                    Process.loadalert(divalert, msgalert, Session("exception"), "danger")
                End If

            End If
            Process.GetAuditTrailInsertandUpdate("", "Uploaded " & Session("uploadcnt") & " record(s) from " & file1.PostedFile.FileName, "File Upload", "")

            'Response.Write("<script language='javascript'> { self.close() }</script>")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Public Shared Function Import1(ByVal filename As String, ByVal SP As String, ByVal Page As String) As Boolean
        'Min 1 and Max 26 columns import

        Dim fields As String()

        Dim hCont As Integer = 0
        Dim Delimiter As String = ","
        Dim columnCnt As Integer = 0

        hCont = 0
        HttpContext.Current.Session.Item("uploadcnt") = 0
        'Dim P As String()
        Try
            Using parser As New TextFieldParser(filename)
                parser.SetDelimiters(Delimiter)
                While Not parser.EndOfData


                    fields = parser.ReadFields()
                    'P = fields

                    columnCnt = fields.Length
                    Dim P(columnCnt - 1) As String


                    For y As Integer = 0 To columnCnt - 1
                        If hCont > 0 Then
                            ' fields(y) <> String.Empty AndAlso fields(y).Replace("""", "") <> "-" AndAlso
                            P(y) = CStr(fields(y).Replace("""", "").Trim)
                            If P(y).Length > 0 Then
                                If P(y).Substring(0, 1) = "'" Then
                                    P(y) = P(y).Replace("'", "")
                                End If
                            End If

                            If P(y).Trim = "-" Then
                                P(y) = "0"
                            End If
                            If P(y).Trim = String.Empty Or P(y).Trim = "" Then
                                P(y) = Nothing
                            End If
                        End If
                    Next


                    If hCont > 0 Then
                        If P(0) IsNot Nothing Then
                            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, SP, P)
                            Dim AuthenCode As String = "APP360FEEDBACK"
                            Process.Appraisal_360_Notification(P(2), P(0), P(1), Process.ApplicationURL + "/" + Process.GetMailLink(AuthenCode, 3))
                            HttpContext.Current.Session.Item("uploadcnt") = CInt(HttpContext.Current.Session.Item("uploadcnt")) + 1
                        End If
                    End If
                    hCont = 1
                End While
            End Using
            'delete temp file after upload
            If File.Exists(filename) = True Then
                File.Delete(filename)
            End If

            'Activity_Log(DateTime.Now, Processing.User_Name, SP, "0", "File Upload", "Uploaded " & uploadCount.ToString & " records from " & filename, 0, 0)
            Return True
        Catch ex As Exception
            HttpContext.Current.Session.Item("exception") = ex.Message
            Return False
        End Try
    End Function

    Private Function emailFile() As String
        Throw New NotImplementedException()
    End Function

    Protected Sub GridVwHeaderChckbox_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridVwHeaderChckbox.PageIndexChanging
        Try
            GridVwHeaderChckbox.PageIndex = e.NewPageIndex
            GridVwHeaderChckbox.DataSource = Process.GetData("Performance_Appraisal_Cycle_get_all")
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub OnRowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridVwHeaderChckbox.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GridVwHeaderChckbox, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs)
        Try
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


    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try

            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "danger")
                Exit Sub
            End If
            Process.loadtype = "Add"
            Response.Redirect("~/Module/Performance/Settings/AppraisalPeriodUpdate", True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Private Class DeleteObj
        Public Property Id As Integer
        Public Property EmpId As String
        Public Property Company As String
        Public Property StartPeriod As String
        Public Property EndPeriod As String
        Public Property ReviewYear As String
        Public Property CreatedBy As String
    End Class
    Protected Sub Delete(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Delete") = False Then
                Response.Write("You don't have privilege to perform this action")
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
                        Dim strUser As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Appraisal_Cycle_Get", ID)
                        appraisalDeleted.Company = strUser.Tables(0).Rows(0).Item("company").ToString
                        appraisalDeleted.StartPeriod = strUser.Tables(0).Rows(0).Item("StartPeriod").ToString
                        appraisalDeleted.EndPeriod = strUser.Tables(0).Rows(0).Item("EndPeriod").ToString
                        appraisalDeleted.CreatedBy = Session("LoginID")
                        appraisalDeleted.ReviewYear = strUser.Tables(0).Rows(0).Item("ReviewYear").ToString
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
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_Cycle_delete", ID)
                        Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(NewValue, OldValue, "Deleted", "Appraisal Cycle Page")
                    End If
                Next
                LoadGrid("All")
            Else
                Process.loadalert(divalert, msgalert, "Delete has been cancelled", "danger")
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub cboCompany_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboCompany.SelectedIndexChanged
        Try
            Session("company") = cboCompany.SelectedValue
            LoadGrid("All")
        Catch ex As Exception

        End Try
    End Sub
End Class