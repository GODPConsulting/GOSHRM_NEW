Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Configuration
Imports Microsoft.Reporting.WebForms
Imports System.IO
Public Class PaperResult
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "JOBPAPERRESULT"
    Dim emailFile As String = ConfigurationManager.AppSettings("EmailURL")
    Dim Pages As String = "Paper Test Result"

    Private Function LoadEmpTypes(LoadType As String) As DataTable
        Dim datatables As New DataTable
        If LoadType = "All" Then
            datatables = Process.SearchData("Recruit_Job_Test_Paper_Result_Get_All", lbltestid.Text)
        ElseIf LoadType = "Find" Then
            datatables = Process.SearchDataP2("Recruit_Job_Test_Paper_Result_Search", lbltestid.Text, txtsearch.Text.Trim)
        End If
        Return datatables
    End Function
    Private Sub LoadGrid(LoadType As String)
        Try

            GridVwHeaderChckbox.DataSource = LoadEmpTypes(LoadType)
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
            If Not IsPostBack Then

                lbltestid.Text = Request.QueryString("id")

                Dim strJT As New DataSet
                strJT = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Job_Test_Paper_Result_Get_All", lbltestid.Text)
                If strJT.Tables(0).Rows.Count > 0 Then
                    lbltesttitle.Text = strJT.Tables(0).Rows(0).Item("JobTest").ToString
                Else
                    lbltesttitle.Text = Session("JobTest")
                End If

                lblView.Text = lbltesttitle.Text & " Test Paper Result"

                'Process.LoadRadComboTextAndValueInitiateP1(cboJobPost, "Recruit_Job_Post_Get_All", "", "--Select a Job Post--", "job title", "code")
                Session("LoadType") = "All"
                LoadGrid(Session("LoadType"))

                


            End If
        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    'Protected Sub cboJobPost_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboJobPost.SelectedIndexChanged
    '    Try
    '        Process.LoadRadComboTextAndValueInitiateP1(cboJobTest, "Recruit_Job_Test_Get_By_JobID", cboJobPost.SelectedValue, "---Select Test--", "TestTitle", "id")
    '    Catch ex As Exception
    '        ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
    '    End Try
    'End Sub

    Protected Sub btnSend_Click(sender As Object, e As EventArgs) Handles btnSend.Click
        Try
            If txtsearch.Text.Trim = "" Then
                Session("LoadType") = "All"
            Else
                Session("LoadType") = "Find"
            End If

            LoadGrid(Session("LoadType"))
        Catch ex As Exception
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    'Protected Sub cboJobTest_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboJobTest.SelectedIndexChanged
    '    Try
    '        Process.LoadRadComboTextAndValueInitiateP2(cboStage, "Recruit_Job_Test_Get_By_JobID_TestID", cboJobPost.SelectedValue, cboJobTest.SelectedValue, "---Select Stage--", "StageNo", "StageNo")
    '    Catch ex As Exception
    '        ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
    '    End Try
    'End Sub
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
            Dim table As DataTable = LoadEmpTypes(Session("LoadType"))
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


    Protected Sub GridVwHeaderChckbox_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridVwHeaderChckbox.PageIndexChanging
        Try
            GridVwHeaderChckbox.PageIndex = e.NewPageIndex
            GridVwHeaderChckbox.DataSource = LoadEmpTypes(Session("LoadType"))
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

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try

            'Process.Export(GridVwHeaderChckbox, "JobGrades", 1, 2)
            Response.Clear()
            Response.Buffer = True
            Response.AddHeader("content-disposition", "attachment;filename=" & lbltesttitle.Text & ".csv")
            Response.Charset = ""
            Response.ContentType = "application/text"
            Dim sBuilder As StringBuilder = New System.Text.StringBuilder()
            For index As Integer = 1 To GridVwHeaderChckbox.Columns.Count - 1
                sBuilder.Append(GridVwHeaderChckbox.Columns(index).HeaderText + ","c)

            Next
            sBuilder.Append(vbCr & vbLf)
            For i As Integer = 0 To GridVwHeaderChckbox.Rows.Count - 1
                For k As Integer = 1 To GridVwHeaderChckbox.HeaderRow.Cells.Count - 1
                    sBuilder.Append(GridVwHeaderChckbox.Rows(i).Cells(k).Text.Replace(",", "") + ",")
                Next
                sBuilder.Append(vbCr & vbLf)
            Next
            Response.Output.Write(sBuilder.ToString())
            Response.Flush()
            Response.[End]()


            

        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    Protected Sub Delete(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try

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
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Job_Test_Paper_Result_delete", ID)
                    End If
                Next
                LoadGrid(Session("LoadType"))
            Else
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Delete has been cancelled" + "')", True)
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        Try

            If FileUpload1.HasFile AndAlso Not FileUpload1.PostedFile Is Nothing Then
                'To create a PostedFile
                Dim csvPath As String = Server.MapPath(Process.FileURL) + Path.GetFileName(FileUpload1.PostedFile.FileName)
                If File.Exists(csvPath) = True Then
                    File.Delete(csvPath)
                End If
                FileUpload1.SaveAs(csvPath)
                'Create byte Array with file len
                'File.ContentLength

                If Process.Import(csvPath, "Recruit_Job_Test_Paper_Result_upload", Pages) = True Then
                    Response.Write("Uploaded " & Session("uploadcnt") & " record(s)")
                Else
                    Response.Write(Process.strExp)
                End If
                Process.GetAuditTrailInsertandUpdate("", "Uploaded " & Session("uploadcnt") & " record(s) from " & csvPath, "File Upload", Pages)
            Else

            End If
            LoadGrid(Session("LoadType"))
            'Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri)
            'Response.Write("<script language='javascript'> { self.close() }</script>")
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
End Class