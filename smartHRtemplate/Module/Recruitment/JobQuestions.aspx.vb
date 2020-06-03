Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports System.IO
Public Class JobQuestions
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "JOBTEST"
    Dim Pages As String = "Recruitment Questions"
    Dim olddata(5) As String
    Private Function LodaDataTable(ByVal JobTestID As Integer) As DataTable
        Dim Datas As New DataTable
        Dim serach As String = ""
        If Session("testquestsearch") = "" Then
            Datas = Process.SearchData("Recruit_Job_Test_Questions_Get_All", JobTestID)
        Else
            search.Value = Session("testquestsearch")
            Datas = Process.SearchDataP2("Recruit_Job_Test_Questions_Search", JobTestID, search.Value.Trim)
        End If
        Dim strheader As New DataSet
        strheader = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Job_Test_Get", Request.QueryString("id"))
        If strheader.Tables(0).Rows.Count > 0 Then
            pagetitle.InnerText = strheader.Tables(0).Rows(0).Item("TestTitle").ToString & ": Test Questions (" & Datas.Rows.Count.ToString & ")"
        End If
        Return Datas
    End Function

    Private Sub LoadQuestions(ByVal JobTestID As Integer)
        Try
            GridVwHeaderChckbox.PageIndex = CInt(Session("testquestpage"))
            GridVwHeaderChckbox.DataSource = LodaDataTable(JobTestID)
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
                'Load Payment
                'Loan Schedule
                txtID.Text = Request.QueryString("id")
                Session("JobTestID") = Request.QueryString("id")

                If Session("testquestsearch") Is Nothing Then
                    Session("testquestsearch") = ""
                End If
                If Session("testquestpage") Is Nothing Then
                    Session("testquestpage") = "0"
                End If

                LoadQuestions(Request.QueryString("id"))
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub SortRecords(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Try
            Dim sortExpression As String = e.SortExpression
            Session("testquestsort") = sortExpression
            Dim direction As String = String.Empty
            If SortsDirection = SortDirection.Ascending Then
                SortsDirection = SortDirection.Descending
                direction = " DESC"
            Else
                SortsDirection = SortDirection.Ascending
                direction = " ASC"
            End If
            Dim table As New DataTable
            table = LodaDataTable(txtID.Text)
            GridVwHeaderChckbox.PageIndex = CInt(Session("testquestpage"))
            table.DefaultView.Sort = sortExpression & direction
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
    Protected Sub btnExport_Click(sender As Object, e As EventArgs)
        Try

            If Process.ExportExcel(LodaDataTable(txtID.Text), "recruitmentquestion") = False Then
                Response.Write(Process.strExp)
            End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub btnUpload_Click(sender As Object, e As EventArgs)
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "warning")
                Exit Sub
            End If

            If Not file1.PostedFile Is Nothing Then
                'To create a PostedFile
                Dim csvPath As String = Server.MapPath(Process.FileURL) + Path.GetFileName(file1.PostedFile.FileName)
                If File.Exists(csvPath) = True Then
                    File.Delete(csvPath)
                End If
                file1.PostedFile.SaveAs(csvPath)
                'Create byte Array with file len
                'File.ContentLength
                If Process.ImportWithUsersAndP1(csvPath, "Recruit_Job_Test_Questions_Update", txtID.Text, Pages) = True Then
                    Process.loadalert(divalert, msgalert, "Uploaded " & Session("uploadcnt") & " record(s)", "success")
                    Process.GetAuditTrailInsertandUpdate("", "Uploaded " & Session("uploadcnt") & " record(s) from " & csvPath, "File Upload", Pages)
                Else
                    Process.loadalert(divalert, msgalert, Session("exception"), "danger")
                End If

                Process.GetAuditTrailInsertandUpdate("", "Uploaded " & Session("uploadcnt") & " record(s) from " & csvPath, "File Upload", Pages)
            End If
            
            Session("testquestpage") = "0"
            Session("testquestsearch") = ""
            LoadQuestions(txtID.Text)

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Protected Sub GridVwHeaderChckbox_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridVwHeaderChckbox.PageIndexChanging
        Try
            GridVwHeaderChckbox.PageIndex = e.NewPageIndex
            Session("testquestpage") = e.NewPageIndex
            GridVwHeaderChckbox.DataSource = LodaDataTable(txtID.Text)
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
    Protected Sub btnClose_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect("~/Module/Recruitment/JobTests", True)
        Catch ex As Exception
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
                        ' Delete row! (Well, not really...)
                        atLeastOneRowDeleted = True
                        ' First, get the ProductID for the selected row
                        Dim Question As String = _
                            Convert.ToString(GridVwHeaderChckbox.DataKeys(row.RowIndex).Value)
                        ' "Delete" the row
                        Dim empPhoto As String = ""
                        Dim strPersonal As New DataSet
                        strPersonal = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Job_Test_Questions_Get", Question)
                        If IsDBNull(strPersonal.Tables(0).Rows(0).Item("images")) = False Then
                            empPhoto = strPersonal.Tables(0).Rows(0).Item("images")
                        End If


                        If empPhoto <> "" Then
                            Dim Path As String = Server.MapPath(empPhoto)
                            If File.Exists(Path) Then
                                File.Delete(Path)
                            End If
                        End If


                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Job_Test_Questions_Delete", Question)
                    End If
                Next
                Process.loadalert(divalert, msgalert, count.ToString & " records successfully deleted", "success")
                LoadQuestions(txtID.Text)
            
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect("~/Module/Recruitment/JobQuestionsUpdate.aspx", True)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnFind_Click(sender As Object, e As EventArgs)
        Try
            If search.Value.Trim = "" Then
                Session("testquestsearch") = ""
            Else
                Session("testquestsearch") = search.Value.Trim
            End If
            LoadQuestions(txtID.Text)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub GridVwHeaderChckbox_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridVwHeaderChckbox.RowCreated
        Try
            Process.SortArrow(e, SortsDirection, Session("testquestsort"))
        Catch ex As Exception

        End Try
    End Sub
End Class