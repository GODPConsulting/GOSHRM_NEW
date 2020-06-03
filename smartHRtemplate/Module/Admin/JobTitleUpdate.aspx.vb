Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Telerik.Web.UI
Imports System.IO

Public Class JobTitleUpdate
    Inherits System.Web.UI.Page
    Dim jobtitle As New clsJobTitle
    Dim AuthenCode As String = "Job"
    Dim Pages As String = "Job Title"
    Dim olddata(3) As String
    Protected Sub DrillDown(ByVal sender As Object, ByVal e As EventArgs)
        Try
            txtskillid.Text = CType(sender, LinkButton).CommandArgument
            Dim url As String = "jobtitleskills?id=" & txtskillid.Text
            Response.Redirect(url, True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Function LoadDatatable() As DataTable
        Dim dt As New DataTable
        search.Value = Session("jobskillLoadsearch")
        If search.Value.Trim = "" Then
            dt = Process.SearchData("Job_Title_Skills_Get_All", txtid.Text)
        Else
            dt = Process.SearchDataP2("Job_Title_Skills_Search", txtid.Text, search.Value)
        End If
        Return dt
    End Function

    Private Sub LoadGrid(id As String)
        Try
            gridskills.PageIndex = CInt(Session("jobskillLoadindex"))
            gridskills.DataSource = LoadDatatable()
            gridskills.AllowSorting = True
            gridskills.DataBind()

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub
    Private Sub PanelVisibility()
        Try
            If txtid.Text = "0" Then
                pnskill.Visible = False
            Else
                pnskill.Visible = True
            End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then

                If Session("jobskillLoadsearch") Is Nothing Then
                    Session("jobskillLoadsearch") = ""
                End If

                If Session("jobskillLoadindex") Is Nothing Then
                    Session("jobskillLoadindex") = "0"
                End If

                If Request.QueryString("id") IsNot Nothing Then
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Job_Titles_get", Request.QueryString("id"))
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    jobname.Value = strUser.Tables(0).Rows(0).Item("name").ToString
                    txtdescription.Value = strUser.Tables(0).Rows(0).Item("jobdescription").ToString
                    LoadGrid(txtid.Text)
                    PanelVisibility()
                Else
                    txtid.Text = "0"
                    PanelVisibility()
                End If

            End If
       

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Function GetIdentity() As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Job_Titles_update"
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = txtid.Text
            cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = jobname.Value.Trim
            cmd.Parameters.Add("@desc", SqlDbType.VarChar).Value = txtdescription.Value
            cmd.Parameters.Add("@skill", SqlDbType.VarChar).Value = ""
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
            Return 0
        End Try
    End Function

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try

            Dim idd As Integer = txtid.Text
            If txtid.Text <> "0" Then
                If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False Then
                    Process.loadalert(divalert, msgalert, Process.privilegemsg, "warning")


                    Exit Sub
                End If
            End If


            Dim lblstatus As String = ""
            If (jobname.Value.Trim = "") Then
                lblstatus = "Job Title required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                jobname.Focus()
                Exit Sub
            End If

            jobtitle.id = txtid.Text
            jobtitle.Name = jobname.Value.Trim
            jobtitle.Desc = txtdescription.Value

            Dim strUser As New DataSet
            If txtid.Text <> "0" Then
                strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Job_Titles_get", txtid.Text)
                olddata(0) = strUser.Tables(0).Rows(0).Item("id").ToString
                olddata(1) = strUser.Tables(0).Rows(0).Item("name").ToString
                olddata(2) = strUser.Tables(0).Rows(0).Item("jobdescription").ToString
            End If

            Dim OldValue As String = ""
            Dim NewValue As String = ""

            Dim j As Integer = 0
            If txtid.Text <> "0" Then
                For Each a In GetType(clsJobTitle).GetProperties()
                    If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If a.GetValue(jobtitle, Nothing).ToString <> olddata(j).ToString Then
                                NewValue += a.Name + ": " + a.GetValue(jobtitle, Nothing).ToString & vbCrLf
                                OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                            End If
                        End If
                    End If
                    j = j + 1
                Next
            Else
                For Each a In GetType(clsJobTitle).GetProperties() 'New Entries
                    If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If a.GetValue(jobtitle, Nothing) = Nothing Then
                                NewValue += a.Name + ":" + " " & vbCrLf
                            Else
                                NewValue += a.Name + ": " + a.GetValue(jobtitle, Nothing).ToString & vbCrLf
                            End If
                        End If
                    End If
                Next
            End If


            If txtid.Text = "0" Then
                txtid.Text = GetIdentity()
                If txtid.Text = "0" Then
                    Exit Sub
                End If
            Else
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Job_Titles_update", txtid.Text, jobname.Value.Trim, txtdescription.Value, "")
            End If



            If NewValue.Trim = "" And OldValue.Trim = "" Then
            Else
                If txtid.Text <> "0" Then
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Updated", "Job Title")
                Else
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Inserted", "Job Title")
                End If

            End If

            PanelVisibility()
            LoadGrid(txtid.Text)
            lblstatus = "Record saved!"
            Process.loadalert(divalert, msgalert, lblstatus, "success")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub



    Protected Sub Delete(sender As Object, e As EventArgs) Handles btDelete.Click
        Try
            Process.loadalert(divalert, msgalert, "", "warning")
            If Process.AuthenAction(Session("role"), AuthenCode, "Delete") = False Then
                Process.loadalert(divalert, msgalert, Process.privilegemsg, "warning")

                Exit Sub
            End If
            Dim count As Integer = 0
            Dim confirmValue As String = Request.Form("confirm_value")
            If confirmValue = "Yes" Then
                Dim atLeastOneRowDeleted As Boolean = False
                ' Iterate through the Products.Rows property
                For Each row As GridViewRow In gridskills.Rows
                    ' Access the CheckBox
                    Dim cb As CheckBox = row.FindControl("chkEmp")
                    If cb IsNot Nothing AndAlso cb.Checked Then
                        count = count + 1
                        ' Delete row! (Well, not really...)
                        atLeastOneRowDeleted = True
                        ' First, get the ProductID for the selected row
                        Dim ID As String = _
                            Convert.ToString(gridskills.DataKeys(row.RowIndex).Value)
                        ' "Delete" the row
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Job_Title_Skills_delete", ID)
                    End If
                Next
                Process.loadalert(divalert, msgalert, count.ToString & " records successfully deleted", "success")

                LoadGrid(txtid.Text)

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub btnUpload_Click(sender As Object, e As EventArgs)
        Try

            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                Process.loadalert(divalert, msgalert, Process.privilegemsg, "warning")

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
                If Process.Import(csvPath, "Job_Title_Skills_Upload", Pages) = True Then
                    Process.loadalert(divalert, msgalert, "Uploaded " & Session("uploadcnt") & " record(s)", "success")
                    Process.GetAuditTrailInsertandUpdate("", "Uploaded " & Session("uploadcnt") & " record(s) from " & csvPath, "File Upload", Pages)
                    LoadGrid(txtid.Text)
                Else
                    Process.loadalert(divalert, msgalert, Session("exception"), "danger")
                End If
                If File.Exists(csvPath) = True Then
                    File.Delete(csvPath)
                End If
            Else
                Process.loadalert(divalert, msgalert, "No files selected to upload", "warning")
                file1.Focus()
                Exit Sub
            End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub
    Protected Sub btnExport_Click(sender As Object, e As EventArgs)
        Try

            If Process.ExportExcel(LoadDatatable(), "JobSkills") = False Then
                Response.Output.Write(Process.strExp)
            End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub
    Protected Sub btnAddSkill_Click(sender As Object, e As EventArgs)
        Try
            Dim url As String = "jobtitleskills?jobid=" & txtid.Text
            Response.Redirect(url, True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnFind_Click(sender As Object, e As EventArgs)
        Try
            'If Not Me.IsPostBack Then
            Session("jobskillLoadsearch") = search.Value.Trim
            LoadGrid(txtid.Text)
            'End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            Session("jobskillLoadsearch") = ""
            Session("jobskillLoadindex") = "0"
            Response.Redirect("~/Module/Admin/JobTitles", True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub SortRecords(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Try
            Dim sortExpression As String = e.SortExpression
            Session("jobskillsortExpression") = sortExpression
            Dim direction As String = String.Empty
            If SortsDirection = SortDirection.Ascending Then
                SortsDirection = SortDirection.Descending
                direction = " DESC"
            Else
                SortsDirection = SortDirection.Ascending
                direction = " ASC"
            End If
            gridskills.PageIndex = CInt(Session("jobskillLoadindex"))
            Dim table As DataTable = LoadDatatable()
            table.DefaultView.Sort = sortExpression & direction
            gridskills.DataSource = table
            gridskills.DataBind()
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

    Private Sub gridskills_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gridskills.PageIndexChanging
        Try
            gridskills.PageIndex = e.NewPageIndex
            Session("jobskillLoadindex") = e.NewPageIndex
            LoadGrid(txtid.Text)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gridskills_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridskills.RowCreated
        Try
            Process.SortArrow(e, SortsDirection, Session("jobskillsortExpression"))
        Catch ex As Exception
        End Try
    End Sub
End Class