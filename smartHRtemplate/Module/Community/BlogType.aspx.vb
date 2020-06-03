Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports System.IO


Public Class BlogType
    Inherits System.Web.UI.Page
    Dim skills As New clsSkills
    Dim AuthenCode As String = "SKILL"
    Dim olddata(3) As String
    Dim Pages As String = "Blog Type"
    Private Function GetIdentity() As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Blogs_Type_Update"
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = txtname.Value.Trim
            cmd.Parameters.Add("@view", SqlDbType.VarChar).Value = cboView.SelectedItem.Text
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception
            Process.strExp = ex.Message
            Return 0
        End Try
    End Function
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
            Dim table As DataTable = LoadGrid()
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
    Private Function LoadGrid() As DataTable
        Dim datatables As New DataTable

        If Session("LoadType") = "All" Then
            datatables = Process.GetData("Blogs_Type_Get_All")
        ElseIf Session("LoadType") = "Find" Then
            datatables = Process.SearchData("Blogs_Type_Search", txtsearch.Value.Trim)
        End If
        pagetitle.InnerText = txtsearch.Value & " Blog Type (" & FormatNumber(datatables.Rows.Count, 0) & ")"
        Return datatables
    End Function
    Private Sub LoadGrid(LoadType As String)
        Try
            GridVwHeaderChckbox.DataSource = LoadGrid()
            GridVwHeaderChckbox.AllowSorting = True
            GridVwHeaderChckbox.AllowPaging = True
            GridVwHeaderChckbox.DataBind()

            'GridVwHeaderChckbox.Columns(2).Visible = False
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                'Company_Structure_get_parent
                cboView.Items.Clear()
                cboView.Items.Add("Yes")
                cboView.Items.Add("No")
                LoadGrid(Session("LoadType"))
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub



    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            'Response.Write("<script language='javascript'> { self.close() }</script>")
            MultiView1.ActiveViewIndex = 0
            LoadGrid(Session("LoadType"))
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


    
    Protected Sub btnExport_Click(sender As Object, e As EventArgs)
        Try
            Dim filename As String = "blog_type"
            If Process.ExportExcel(LoadGrid(), filename) = False Then
                Response.Write(Process.strExp)
            Else
                Response.Write("File saved as " & filename & ".xls")
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs)
        Try
            If FileUpload1.PostedFile Is Nothing Then
                'To create a PostedFile
                Dim csvPath As String = Server.MapPath(Process.FileURL) + Path.GetFileName(FileUpload1.PostedFile.FileName)
                If File.Exists(csvPath) = True Then
                    File.Delete(csvPath)
                End If
                FileUpload1.PostedFile.SaveAs(csvPath)
                'Create byte Array with file len
                'File.ContentLength
                If Process.Import(csvPath, "Blogs_Type_Upload", Pages) = True Then

                    ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Uploaded " & Session("uploadcnt") & " record(s)" + "')", True)
                Else
                    Response.Write(Process.strExp)
                    ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & Process.strExp + "')", True)
                End If

                LoadGrid(Session("LoadType"))
                Process.loadalert(divalert, msgalert, "Uploaded " & Session("uploadcnt") & " record(s) from " & csvPath, "success")
            Else
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "No file selected" + "')", True)
            End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub Delete(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try

         
            'Dim confirmValue As String = Request.Form("confirm_value")
            Dim confirmValue As String = "Yes"
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
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Blogs_Type_Delete", ID)
                    End If
                Next
                LoadGrid(Session("LoadType"))
                Process.loadalert(divalert, msgalert, "Delete Successful", "success")
            Else
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Delete has been cancelled" + "')", True)
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnFind_Click(sender As Object, e As EventArgs)
        Try
            If txtsearch.Value = "" Then
                Session("LoadType") = "All"
            Else
                Session("LoadType") = "Find"
            End If
            'If Not Me.IsPostBack Then
            LoadGrid(Session("LoadType"))
            'End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            MultiView1.ActiveViewIndex = 1
            lblid.Text = "0"
            txtname.Value = ""
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub LinkDetail(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim sid As String = CType(sender, LinkButton).CommandArgument
            Dim dt As DataTable = Process.SearchData("Blogs_Type_Get", sid)
            If dt IsNot Nothing Then
                txtname.Value = dt.Rows(0)("name").ToString()
                Process.AssignRadComboValue(cboView, dt.Rows(0)("view_other_comments").ToString())
                lblid.Text = dt.Rows(0)("id").ToString()
            End If
            MultiView1.ActiveViewIndex = 1
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            If lblid.Text = "0" Then
                lblid.Text = GetIdentity()
            Else
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Blogs_Type_Update", lblid.Text, txtname.Value.Trim, cboView.SelectedItem.Text)
            End If
            Process.loadalert(divalert, msgalert, "Record Saved!", "success")
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
End Class