Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Imports Telerik.Web.UI
Imports System.Web.UI
Imports System.Threading


Public Class CompanyBlogView
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "HRBLACKBOARD"
    Public Shared Separator() As Char = {"."c}
    Protected Sub downloadFile(ByVal bytefile As Byte(), ByVal filetype As String, ByVal filename As String)
        Dim bytes() As Byte = bytefile
        Response.Buffer = True
        Response.Charset = ""
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = filetype
        Response.AddHeader("content-disposition", "attachment;filename=" & filename)
        Response.BinaryWrite(bytes)
        Response.Flush()
        Response.End()
    End Sub
    Private Sub download_click(sid As String)
        Try
            Dim dt As DataTable = Process.SearchData("Blogs_Attachement_Get", sid)
            If dt IsNot Nothing Then
                downloadFile(CType(dt.Rows(0)("fileimage"), Byte()), dt.Rows(0)("filetype").ToString(), dt.Rows(0)("filename").ToString())
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub
    'Private Sub LoadComments(sid As Integer)
    '    Try
    '        Dim datatables As New DataTable
    '        datatables = Process.SearchData("blogs_comment_get", sid)
    '        dlcomments.DataSource = datatables
    '        dlcomments.DataBind()
    '        lblcomment.Text = datatables.Rows.Count.ToString + " Comments"
    '        If datatables.Rows.Count < 1 Then
    '            divcomments.Visible = False
    '            dlcomments.Visible = False
    '        Else
    '            divcomments.Visible = True
    '            dlcomments.Visible = True
    '        End If
    '    Catch ex As Exception
    '        Process.loadalert(divalert, msgalert, ex.Message, "danger")

    '    End Try
    'End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Read") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "warning")

                Exit Sub
            End If


            If Not Me.IsPostBack Then
                Process.LoadRadComboTextAndValue(cboBlogType, "Blogs_Type_Get_All", "name", "name", False)
                Process.LoadRadComboTextAndValueP1(radoffice, "Company_Parent_Breakdown", Process.GetCompanyName, "companys", "companys", False)
                radStatus.Items.Clear()
                radStatus.Items.Add("Pending")
                radStatus.Items.Add("Approved")
                radStatus.Items.Add("Rejected")
                radStatus.Items.Add("Cancelled")

                If Request.QueryString("id") IsNot Nothing Then
                    Dim strdetail As New DataSet
                    strdetail = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Blogs_Get", Request.QueryString("id"))

                    lblid.Text = strdetail.Tables(0).Rows(0).Item("id").ToString

                    Dim strattach As New DataSet
                    strattach = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Blogs_Attachement_Get_All", lblid.Text)

                    MultiView1.ActiveViewIndex = 0

                    Process.AssignRadComboValue(cboBlogType, strdetail.Tables(0).Rows(0).Item("type").ToString)
                    heading.Value = strdetail.Tables(0).Rows(0).Item("heading").ToString
                    blogbody.Value = strdetail.Tables(0).Rows(0).Item("message").ToString
                    'Process.Textbox_AdjustHeight(txtbody)
                    datRetireDate.SelectedDate = CDate(strdetail.Tables(0).Rows(0).Item("retiredate").ToString)

                    Process.AssignRadDropDownValue(radStatus, strdetail.Tables(0).Rows(0).Item("approval").ToString)


                    dateposted.Value = CDate(strdetail.Tables(0).Rows(0).Item("createdon")).ToLongDateString

                    Dim lstoffice As New RadListBox
                    Process.LoadListAndComboxFromDataset(lstoffice, radoffice, "Blogs_Subscribers_Get", "office", "office", lblid.Text)
                    Process.LoadTooltipComboBox(radoffice)

                    If strattach.Tables(0).Rows.Count > 0 Then
                        fileattached.InnerText = strattach.Tables(0).Rows(0).Item("filename").ToString
                        lnkclear.Visible = True
                        lnkDownloadAttach.Visible = True
                    Else
                        lnkclear.Visible = False
                        lnkDownloadAttach.Visible = False
                    End If

                    'End If
                    'LoadComments(lblid.Text)
                    divapproval.Visible = True
                Else
                    MultiView1.ActiveViewIndex = 0
                    lblid.Text = "0"
                    lnkclear.Visible = False
                    lnkDownloadAttach.Visible = False
                    'lblstat.Text = "Approval Stat: Pending"
                    btnDelete.Visible = False
                    'lblcomment.Visible = False
                    'txtpost.Visible = False
                    'btnPost.Visible = False

                    divapproval.Visible = False
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
            cmd.CommandText = "Blogs_Add"
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@heading", SqlDbType.VarChar).Value = heading.Value.Trim
            cmd.Parameters.Add("@type", SqlDbType.VarChar).Value = cboBlogType.SelectedValue
            cmd.Parameters.Add("@message", SqlDbType.VarChar).Value = blogbody.Value
            cmd.Parameters.Add("@empid", SqlDbType.VarChar).Value = Session("UserEmpID")
            cmd.Parameters.Add("@company", SqlDbType.VarChar).Value = Session("company")
            cmd.Parameters.Add("@approval", SqlDbType.VarChar).Value = radStatus.SelectedText
            cmd.Parameters.Add("@retiredate", SqlDbType.Date).Value = datRetireDate.SelectedDate
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

            Return "0"
        End Try
    End Function
    Private Sub SaveUpload()
        Try
            If Not file1.PostedFile Is Nothing Then

                Dim strtype As String = ""
                Dim strname As String = ""
                Dim imgdata As Byte() = Nothing
                Dim strsize As Integer = 0

                Dim img_strm As Stream = file1.PostedFile.InputStream
                Dim img_len As Integer = file1.PostedFile.ContentLength
                strtype = file1.PostedFile.ContentType.ToString()
                strname = Path.GetFileName(file1.PostedFile.FileName)
                strsize = file1.PostedFile.ContentLength

                If strtype.Contains("video") Or strtype.Contains("audio") Then
                    Using br As New BinaryReader(file1.PostedFile.InputStream)
                        imgdata = br.ReadBytes(CInt(file1.PostedFile.InputStream.Length))
                    End Using
                Else
                    imgdata = New Byte(img_len - 1) {}
                    Dim n As Integer = img_strm.Read(imgdata, 0, img_len)
                End If

                ' Process.SaveFiles(imgUpload, strtype, strname, imgdata, strsize)

                If strtype.ToLower.Contains("video") Or strtype.ToLower.Contains("audio") Then
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Blogs_Attachement_Upload_VA", 0, lblid.Text, imgdata, strname, strtype, strsize)
                Else
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Blogs_Attachement_Upload_File_Flat", 0, lblid.Text, imgdata, strname, strtype, strsize)
                End If

                fileattached.InnerText = strname
                lnkclear.Visible = True
                lnkDownloadAttach.Visible = True
            End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub
    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            Dim msg As String = ""
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "warning")

                Exit Sub
            End If

            If heading.Value.Trim = "" Then
                msg = "Heading required!"

                heading.Focus()
                Exit Sub
            End If

            If IsDate(datRetireDate.SelectedDate) = False Then
                msg = "invalid date format!"

                datRetireDate.Focus()
                Exit Sub
            Else
                If datRetireDate.SelectedDate Is Nothing Then
                    msg = "Retire date required!"

                    datRetireDate.Focus()
                    Exit Sub
                Else
                    If datRetireDate.SelectedDate < Now.Date Then
                        msg = "Retire date cannot be set to past!"

                        datRetireDate.Focus()
                        Exit Sub
                    End If
                End If
            End If


            If blogbody.Value.Trim = "" Then
                msg = "No message in blog!"

                blogbody.Focus()
                Exit Sub
            End If

            If lblid.Text = "0" Then
                lblid.Text = GetIdentity()
                divapproval.Visible = True
                'Process.Blog_Request(txtHeading.Text, Session("UserEmpID"), Process.DDMONYYYY(Now.Date), Process.GetMailLink(AuthenCode, 1))
            Else
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Blogs_Add", lblid.Text, heading.Value.Trim, cboBlogType.SelectedValue, blogbody.Value, Session("UserEmpID"), Session("company"), radStatus.SelectedText, datRetireDate.SelectedDate)
            End If

            If radStatus.SelectedText.ToLower = "approved" Then
                msg = "Blog saved and published!"
            Else
                msg = "Blog saved!"
            End If

            btnDelete.Visible = True
            'lblcomment.Visible = True
            'txtpost.Visible = True
            'btnPost.Visible = True
            'refresh subscribers list for updating
            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Blogs_Subscribers_Refresh", lblid.Text)
            If radoffice.CheckedItems.Count > 0 Then
                Dim collection As IList(Of RadComboBoxItem) = radoffice.CheckedItems
                If (collection.Count <> 0) Then
                    For Each item As RadComboBoxItem In collection
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Blogs_Subscribers_Update", lblid.Text, item.Value, Session("LoginID"))
                    Next
                End If
            End If
            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Blogs_Subscribers_Drop_Unchecked", lblid.Text)
            SaveUpload()

            Process.LoadTooltipComboBox(radoffice)

            Process.loadalert(divalert, msgalert, msg, "success")

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    'Protected Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
    '    Try
    '        If txtpost.Text.Trim <> "" Then
    '            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "blogs_comment_add", lblid.Text, txtpost.Text, Session("UserEmpID"))
    '            LoadComments(lblid.Text)
    '        End If
    '    Catch ex As Exception
    '        Process.loadalert(divalert, msgalert, ex.Message, "danger")

    '    End Try
    'End Sub

    Protected Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Delete") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "warning")
                Exit Sub
            End If
            Dim msg As String = "Post successfully delete!"
            Dim confirmValue As String = Request.Form("confirm_value")
            If confirmValue.ToLower.Trim = "yes" Then
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Blogs_Delete", lblid.Text)
                'lblcomment.Visible = False
                'txtpost.Visible = False
                'btnPost.Visible = False
                heading.Value = ""
                blogbody.Value = ""
                'lblstat.Text = ""
                Process.loadalert(divalert, msgalert, msg, "success")

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Protected Sub lnkBack_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect("~/Module/Community/CompanyBlog", True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    'Protected Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
    '    Try
    '        'Process.LoadListBoxFromCombo(lstoffice, radoffice)
    '        Process.LoadTooltipComboBox(radoffice)
    '    Catch ex As Exception

    '    End Try
    'End Sub



    Protected Sub lnkclear_Click(sender As Object, e As EventArgs) Handles lnkclear.Click
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Delete") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "warning")

                Exit Sub
            End If
            Dim msg As String = "Attachement removed!"
            Dim confirmValue As String = Request.Form("confirm_value")
            If confirmValue.ToLower.Trim = "yes" Then
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Blogs_Attachement_Delete", lblid.Text)
                fileattached.InnerText = ""
                lnkclear.Visible = False
                lnkDownloadAttach.Visible = False
                Process.loadalert(divalert, msgalert, msg, "info")
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Protected Sub lnkDownloadAttach_Click(sender As Object, e As EventArgs) Handles lnkDownloadAttach.Click
        download_click(lblid.Text)
    End Sub


    'Protected Sub lnkapprovalcoment_Click(sender As Object, e As EventArgs) Handles lnkapprovalcoment.Click
    '    Try
    '        Dim url As String = "blogapproval.aspx?id=" & lblid.Text
    '        Dim s As String = "window.open('" & url + "', 'popup_window', 'width=500,height=400,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no');"
    '        ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)
    '    Catch ex As Exception
    '        Process.loadalert(divalert, msgalert, ex.Message, "danger")
    '        
    '    End Try
    'End Sub

   
End Class