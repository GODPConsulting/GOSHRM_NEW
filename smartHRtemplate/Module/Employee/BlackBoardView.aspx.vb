Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Imports Telerik.Web.UI
Imports System.Web.UI
Imports System.Threading


Public Class BlackBoardView
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "EMPBLACKBOARD"
    Public Shared Separator() As Char = {"."c}   
    Dim base64String As String
    Public postedon, postedby, topic, post, comp As String
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
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub
    Private Sub LoadComments(sid As Integer)
        Try
            Dim datatables As New DataTable
            datatables = Process.SearchDataP2("emp_blogs_comment_get", sid, Session("UserEmpID"))
            'dlcomments.DataSource = datatables
            'dlcomments.DataBind()
            'lblcomment.Text = datatables.Rows.Count.ToString + " Comments"

            Dim strDashBoard As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "emp_blogs_comment_get", sid, Session("UserEmpID"))
            Dim imgs As Byte() = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "SELECT imgfile FROM avatar")
            Dim n As StringBuilder = New StringBuilder("")
            Dim m As Integer = strDashBoard.Tables(0).Rows.Count
            If m > 0 Then
                For i As Integer = 0 To m - 1
                    Dim comments As String = Convert.ToString(strDashBoard.Tables(0).Rows(i)("comment"))
                    Dim timestamps As String = Convert.ToDateTime(strDashBoard.Tables(0).Rows(i)("timestamps"))
                    timestamps = TimeAgo(timestamps)
                    Dim name As String = Convert.ToString(strDashBoard.Tables(0).Rows(i)("Name"))
                    Dim office As String = Convert.ToString(strDashBoard.Tables(0).Rows(i)("Office"))
                    Dim id As String = Convert.ToString(strDashBoard.Tables(0).Rows(i)("id"))
                    Dim imgg As Byte()
                    If Not IsDBNull((strDashBoard.Tables(0).Rows(i)("imgfile"))) And (strDashBoard.Tables(0).Rows(i)("imgfile")) IsNot Nothing Then
                        imgg = CType((strDashBoard.Tables(0).Rows(i)("imgfile")), Byte())
                        If imgg.Length = 0 Then
                            base64String = Convert.ToBase64String(imgs)
                        Else
                            base64String = Convert.ToBase64String(imgg)
                        End If
                    Else
                        base64String = Convert.ToBase64String(imgs)
                    End If

                    n.Append("<div class='chat chat-left'><div class='chat-avatar'>")
                    n.Append("<a href='#' data-placement='right' data-toggle='tooltip' class='avatar' data-original-title='" + name + "'>")
                    n.Append("<img alt='" + name + "' src='data:image/png;base64," + base64String + "' class='img-responsive img-circle'></a></div>")
                    n.Append("<div class='chat-body'><div class='chat-bubble'><div class='chat-content'><p>" + comments + "</p>")
                    n.Append("<span class='chat-time'>" + timestamps + "</span></div></div></div></div>")

                Next
            Else
                n.Append("")
                n.Append("")

            End If
            Dim rr As String = n.ToString()
            chatter.InnerHtml = rr
            strDashBoard.Clear()
            'If datatables.Rows.Count > 0 Then
            '    dlcomments.Visible = True
            '    divcomment.Visible = True
            'Else
            '    dlcomments.Visible = False
            '    divcomment.Visible = False
            'End If
        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Read") = False Then
                Response.Write("You don't have privilege to perform this action")
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
                Exit Sub
            End If


            If Not Me.IsPostBack Then
                Process.LoadRadComboTextAndValue(cboBlogType, "Blogs_Type_Get_All", "name", "name", False)
                Process.LoadRadComboTextAndValueP1(radoffice, "Company_Parent_Breakdown", Process.GetCompanyName, "companys", "companys", False)
                If Request.QueryString("id") IsNot Nothing Then

                    Dim strdetail As New DataSet
                    strdetail = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Blogs_Get", Request.QueryString("id"))
                    Dim n As StringBuilder = New StringBuilder("")
                    lblid.Text = strdetail.Tables(0).Rows(0).Item("id").ToString

                    Dim strattach As New DataSet
                    strattach = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Blogs_Attachement_Get_All", lblid.Text)
                    Dim imgs As Byte() = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "SELECT imgfile FROM avatar")
                    If strdetail.Tables(0).Rows(0).Item("approval").ToString = "Approved" Then
                        'Session("UserEmpID") <> strdetail.Tables(0).Rows(0).Item("createdby").ToString And
                        MultiView1.ActiveViewIndex = 0
                        topic = strdetail.Tables(0).Rows(0).Item("heading").ToString
                        postedon = Convert.ToDateTime(strdetail.Tables(0).Rows(0).Item("createdon"))
                        postedon = TimeAgo(postedon)
                        postedby = strdetail.Tables(0).Rows(0).Item("postedby").ToString
                        post = strdetail.Tables(0).Rows(0).Item("message").ToString
                        comp = strdetail.Tables(0).Rows(0).Item("Office").ToString
                        Dim base64 As String
                        If Not IsDBNull((strdetail.Tables(0).Rows(0)("imgfile"))) And (strdetail.Tables(0).Rows(0)("imgfile")) IsNot Nothing Then
                            Dim imgg As Byte() = CType((strdetail.Tables(0).Rows(0)("imgfile")), Byte())
                            If imgg.Length = 0 Then
                                base64 = Convert.ToBase64String(imgs)
                            Else
                                base64 = Convert.ToBase64String(imgg)
                            End If
                        Else
                            base64 = Convert.ToBase64String(imgs)
                        End If

                        n.Append("<div class='activity-box'><ul class='activity-list'><li><div class='activity-user'>")
                        n.Append("<a href='#' data-toggle='tooltip' class='avatar' data-original-title='" + postedby + "'>")
                        n.Append("<img alt='' src='data:image/png;base64," + base64 + "' class='img-responsive img-circle'>")
                        n.Append("</a></div><div class='activity-content'><div class='timeline-content'><a href='#' class='name'>" + postedby + "</a>")
                        n.Append("<span class='time'><i class='fa fa-clock-o'></i>&nbsp;" + postedon + " &nbsp;&nbsp;&nbsp;&nbsp;<i class='fa fa-bank'></i>&nbsp; " + comp + "</span><br />")
                        n.Append("<h6><a class='name'><b>" + topic + "</b></a></h6>")
                        n.Append("<span>" + post + "</span></div></div></li></ul></div>")

                        Dim kk As String = n.ToString()
                        blogpost.InnerHtml = kk

                        'Process.Textbox_AdjustHeight(txtmessage)
                        'If strattach.Tables(0).Rows.Count > 0 Then
                        '    lnkattachment.Text = strattach.Tables(0).Rows(0).Item("filename").ToString
                        '    lnkattachment.Visible = True
                        '    lblattachment.Visible = True
                        'Else
                        '    lnkattachment.Visible = False
                        '    lblattachment.Visible = False
                        'End If
                    Else
                        MultiView1.ActiveViewIndex = 1
                        Process.AssignRadComboValue(cboBlogType, strdetail.Tables(0).Rows(0).Item("type").ToString)
                        txtHeading.Value = strdetail.Tables(0).Rows(0).Item("heading").ToString
                        txtbody.Value = strdetail.Tables(0).Rows(0).Item("message").ToString
                        ' Process.Textbox_AdjustHeight(txtbody)
                        datRetireDate.SelectedDate = CDate(strdetail.Tables(0).Rows(0).Item("retiredate").ToString)
                        txtapprovalcomment.Value = strdetail.Tables(0).Rows(0).Item("approvalcomment").ToString.Trim
                        If txtapprovalcomment.Value = "" Or txtapprovalcomment.Value.Trim.Length < 2 Then
                            'lblappcomment.Visible = False
                            txtapprovalcomment.Visible = False
                        Else
                            'lblappcomment.Visible = True
                            txtapprovalcomment.Visible = True
                        End If
                        pagetitle.InnerText = "Posted on " & strdetail.Tables(0).Rows(0).Item("createdon").ToString & "   Approval Stat: " & strdetail.Tables(0).Rows(0).Item("approval").ToString
                        Dim lstoffice As New RadListBox
                        Process.LoadListAndComboxFromDataset(lstoffice, radoffice, "Blogs_Subscribers_Get", "office", "office", lblid.Text)
                        Process.LoadTooltipComboBox(radoffice)
                        If strattach.Tables(0).Rows.Count > 0 Then
                            'lblstat.Text = strattach.Tables(0).Rows(0).Item("filename").ToString
                            lnkclear.Visible = True
                            lnkDownloadAttach.Visible = True
                        Else
                            lnkclear.Visible = False
                            lnkDownloadAttach.Visible = False
                        End If

                    End If
                    LoadComments(lblid.Text)
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "blog_read_update", lblid.Text, Session("UserEmpID"))
                Else
                    MultiView1.ActiveViewIndex = 1
                    lblid.Text = "0"
                    lnkclear.Visible = False
                    lnkDownloadAttach.Visible = False
                    'lblstat.Text = "Approval Stat: Pending"
                    btnDelete.Visible = False
                    'lblcomment.Visible = False
                    txtpost.Visible = False
                    'btnPost.Visible = False
                    'lblappcomment.Visible = False
                    txtapprovalcomment.Visible = False
                    LoadComments(lblid.Text)
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    Public Shared Function TimeAgo(ByVal dateTime As DateTime) As String
        Dim result As String = String.Empty
        Dim timeSpan = System.DateTime.Now.Subtract(dateTime)

        If timeSpan <= TimeSpan.FromSeconds(60) Then
            result = String.Format("{0} seconds ago", timeSpan.Seconds)
        ElseIf timeSpan <= TimeSpan.FromMinutes(60) Then
            result = If(timeSpan.Minutes > 1, String.Format(" {0} minutes ago", timeSpan.Minutes), "about a minute ago")
        ElseIf timeSpan <= TimeSpan.FromHours(24) Then
            result = If(timeSpan.Hours > 1, String.Format(" {0} hours ago", timeSpan.Hours), "about an hour ago")
        ElseIf timeSpan <= TimeSpan.FromDays(30) Then
            result = If(timeSpan.Days > 1, String.Format(" {0} days ago", timeSpan.Days), "yesterday")
        ElseIf timeSpan <= TimeSpan.FromDays(365) Then
            result = If(timeSpan.Days > 30, String.Format(" {0} months ago", timeSpan.Days / 30), "about a month ago")
        Else
            result = If(timeSpan.Days > 365, String.Format(" {0} years ago", timeSpan.Days / 365), "about a year ago")
        End If

        Return result
    End Function

    Private Function GetIdentity() As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Emp_Blogs_Add"
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@heading", SqlDbType.VarChar).Value = txtHeading.Value.Trim
            cmd.Parameters.Add("@type", SqlDbType.VarChar).Value = cboBlogType.SelectedValue
            cmd.Parameters.Add("@message", SqlDbType.VarChar).Value = txtbody.Value
            cmd.Parameters.Add("@empid", SqlDbType.VarChar).Value = Session("UserEmpID")
            cmd.Parameters.Add("@retiredate", SqlDbType.Date).Value = datRetireDate.SelectedDate
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
            Return "0"
        End Try
    End Function
    Private Sub SaveUpload()
        Try
            If imgUpload.HasFile AndAlso Not imgUpload.PostedFile Is Nothing Then

                Dim strtype As String = ""
                Dim strname As String = ""
                Dim imgdata As Byte() = Nothing
                Dim strsize As Integer = 0

                Dim img_strm As Stream = imgUpload.PostedFile.InputStream
                Dim img_len As Integer = imgUpload.PostedFile.ContentLength
                strtype = imgUpload.PostedFile.ContentType.ToString()
                strname = Path.GetFileName(imgUpload.PostedFile.FileName)
                strsize = imgUpload.PostedFile.ContentLength

                If strtype.Contains("video") Or strtype.Contains("audio") Then
                    Using br As New BinaryReader(imgUpload.PostedFile.InputStream)
                        imgdata = br.ReadBytes(CInt(imgUpload.PostedFile.InputStream.Length))
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

                lblfile.Text = strname
                lnkclear.Visible = True
                lnkDownloadAttach.Visible = True
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub
    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            Dim msg As String = ""
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                Response.Write("You don't have privilege to perform this action")
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
                Exit Sub
            End If

            If txtHeading.Value.Trim = "" Then
                msg = "Heading required!"
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & msg + "')", True)
                txtHeading.Focus()
                Exit Sub
            End If

            If IsDate(datRetireDate.SelectedDate) = False Then
                msg = "invalid date format!"
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & msg + "')", True)
                datRetireDate.Focus()
                Exit Sub
            Else
                If datRetireDate.SelectedDate Is Nothing Then
                    msg = "Retire date required!"
                    ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & msg + "')", True)
                    datRetireDate.Focus()
                    Exit Sub
                Else
                    If datRetireDate.SelectedDate < Now.Date Then
                        msg = "Retire date cannot be set to past!"
                        ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & msg + "')", True)
                        datRetireDate.Focus()
                        Exit Sub
                    End If
                End If
            End If
            

            If txtbody.Value.Trim = "" Then
                msg = "No message in blog!"
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & msg + "')", True)
                txtbody.Focus()
                Exit Sub
            End If

            If lblid.Text = "0" Then
                lblid.Text = GetIdentity()
                Process.Blog_Request(txtHeading.Value, Session("UserEmpID"), Process.DDMONYYYY(Now.Date), Process.ApplicationURL + "/" + Process.GetMailLink(AuthenCode, 1))
                'lblstat.Text = "Posted on " & Now.Date.ToLongDateString & "   Approval Stat: Pending"
                msg = "Blog saved, will be published after approval from HR!"
                Process.loadalert(divalert, msgalert, msg, "success")
            Else
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Blogs_Add", lblid.Text, txtHeading.Value.Trim, cboBlogType.SelectedValue, txtbody.Value, Session("UserEmpID"), datRetireDate.SelectedDate)
                msg = "Blog saved!"
                Process.loadalert(divalert, msgalert, msg, "success")
            End If
            btnDelete.Visible = True
            'lblcomment.Visible = True
            txtpost.Visible = True
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
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnPost_Click(sender As Object, e As EventArgs)
        Try
            If txtpost.Value.Trim <> "" Then
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "blogs_comment_add", lblid.Text, txtpost.Value, Session("UserEmpID"))               
                LoadComments(lblid.Text)
                txtpost.Value = ""
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Delete") = False Then
                Response.Write("You don't have privilege to perform this action")
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
                Exit Sub
            End If
            Dim msg As String = "Post successfully delete!"
            Dim confirmValue As String = Request.Form("confirm_value")
            If confirmValue.ToLower.Trim = "yes" Then
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Blogs_Delete", lblid.Text)
                'lblcomment.Visible = False
                txtpost.Visible = False
                'btnPost.Visible = False
                txtHeading.Value = ""
                txtbody.Value = ""
                'lblstat.Text = ""
                Response.Write(msg)
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & msg + "')", True)
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    Protected Sub lnkBack_Click(sender As Object, e As EventArgs) Handles lnkBack.Click
        Try
            Response.Redirect("~/Module/Employee/Blackboard.aspx", True)
        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    Protected Sub btnRefresh_Click(sender As Object, e As EventArgs)
        Try
            'Process.LoadListBoxFromCombo(lstoffice, radoffice)
            Process.LoadTooltipComboBox(radoffice)
        Catch ex As Exception

        End Try
    End Sub

  

    Protected Sub lnkclear_Click(sender As Object, e As EventArgs) Handles lnkclear.Click
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Delete") = False Then
                Response.Write("You don't have privilege to perform this action")
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
                Exit Sub
            End If
            Dim msg As String = "Attachement removed!"
            Dim confirmValue As String = Request.Form("confirm_value")
            If confirmValue.ToLower.Trim = "yes" Then
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Blogs_Attachement_Delete", lblid.Text)
                lblfile.Text = ""
                lnkclear.Visible = False
                lnkDownloadAttach.Visible = False
                Response.Write(msg)
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & msg + "')", True)
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    'Protected Sub lnkattachment_Click(sender As Object, e As EventArgs) Handles lnkattachment.Click
    '    download_click(lblid.Text)
    'End Sub

    Protected Sub lnkDownloadAttach_Click(sender As Object, e As EventArgs) Handles lnkDownloadAttach.Click
        download_click(lblid.Text)
    End Sub

    
End Class