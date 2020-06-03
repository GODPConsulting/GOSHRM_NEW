Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.Web.Script.Serialization

Public Class smartHR
    Inherits System.Web.UI.MasterPage
    Public c As Integer
    Public emp As String
    Dim apptype As String = ConfigurationManager.AppSettings("apptype")
    Public domainname As String = ConfigurationManager.AppSettings("domain")
    Public siteurl As String
    Private Sub MailRefresh()
        Try
            Dim txt_search As String = Session("jobsearch")
            Dim loc_search As String = Session("location")
            Dim spec_search As String = Session("spec")
            txt_search = "%" + txt_search + "%"
            loc_search = "%" + loc_search + "%"
            spec_search = "%" + spec_search + "%"            
            Dim calds As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_MailBox_Received_Unread_Get_All", Session("userempid"))
            If calds.Tables(0).Rows.Count > 0 Then
                Dim c As Integer = calds.Tables(0).Rows.Count
                Dim y As StringBuilder = New StringBuilder("")
                If c > 0 Then
                    For i As Integer = 0 To c - 1
                        Dim subject As String = Convert.ToString(calds.Tables(0).Rows(i)("subject"))
                        Dim sender As String = Convert.ToString(calds.Tables(0).Rows(i)("sender"))
                        Dim sendername As String = Convert.ToString(calds.Tables(0).Rows(i)("sendername"))
                        Dim body As String = Convert.ToString(calds.Tables(0).Rows(i)("body"))
                        'Dim duration As String = Convert.ToString(calds.Tables(0).Rows(i)("duration"))
                        Dim id As String = Convert.ToString(calds.Tables(0).Rows(i)("id"))
                        'Dim initials As String = Convert.ToString(calds.Tables(0).Rows(i)("initial"))
                        Dim url As String = Page.ResolveUrl("~/Module/Employee/notifications?id=" + id)
                        ''<%= Page.ResolveClientUrl('~/Module/Employee/Notifications?id=' " + id +  ")%>'
                        'y.Append("<a href='Module/Employee/notifications?id=" + id + "'>")
                        y.Append("<li class='media notification-message'>")
                        y.Append("<a href='" + url + "'>")
                        y.Append("<div class='media-left'>")
                        y.Append("<span class='avatar'>")
                        'y.Append("<img alt='John Doe' src='<%= Page.ResolveClientUrl('~/ImgHandler.ashx?imgid=" + sender + "')%>' class='img-responsive img-circle' onerror='this.onerror=null; this.src='~/images/blank-avatar.jpg';'>")
                        'y.Append(initials)
                        y.Append("</span>")
                        y.Append("</div>")
                        y.Append("<div class='media-body'>")
                        y.Append("<p class='m-0 noti-details'><span class='noti-title'>" + sendername + "</span></p>")
                        y.Append("<p class='m-0 noti-details'>" + subject + "</p>")
                        'y.Append("<p class='m-0'><span class='notification-time'>" + duration + "</span></p>")
                        y.Append("</div>")
                        y.Append("</a>")
                        y.Append("</li>")
                    Next                
                End If
                Dim ww As String = y.ToString()
                mail_list.InnerHtml = ww
            Else
                mail_list.InnerHtml = ""
            End If
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                Dim sKey As String = ""
                Dim registered As String = ""
                Dim hostip As String = ""
                Dim strKey As New DataSet
                If apptype.ToLower = "cloud" Then
                    strKey = SqlHelper.ExecuteDataset(WebConfig.CloudConnString, CommandType.Text, "Select expirationdate from Cloud_Subscribers where 'goshrm_' + domain ='" + domainname + "' and activationstat = 'Activated'")
                Else
                    strKey = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "User_Temp_Table_Get")
                End If

                If Session("LoginID") Is Nothing Then
                    FormsAuthentication.RedirectToLoginPage()
                    Exit Sub
                End If

                Dim empid As String = Session("UserEmpID")
                Dim nMonth As String = (Date.Now.Month).ToString()
                emp_count.InnerText = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select count(*) from Calendar_Event where empid ='" + empid + "' and month(eventdate) ='" + (Date.Now.Month).ToString() + "'")

                ClearApplicationCache()

                LoadBirthdays(DateTime.Now)

                If Session("EmpName") Is Nothing Then
                    Session("EmpName") = Session("LoginID")
                    divwelcome.InnerText = Session("EmpName").ToString.Trim
                Else
                    divwelcome.InnerText = Session("EmpName").ToString.Trim
                End If

                Dim url As String = Request.Url.PathAndQuery.ToLower.Replace("/" & Process.domainname.ToLower, "")
                url = url.Remove(0, 1)
                siteurl = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "menu_get_pathtext", url)

                Dim strmail As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_MailBox_Received_Unread_Get_All", Session("UserEmpID"))
                mailcount.InnerText = FormatNumber(strmail.Tables(0).Rows.Count.ToString(), 0)
                MailRefresh()

                Dim strchat As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Chat_Lists_All", Session("LoginID"))
                Dim m As Integer = strchat.Tables(0).Rows.Count
                Dim chatcounter As String = ""
                If m > 0 Then
                    For i As Integer = 0 To m - 1
                        chatcounter = FormatNumber(strchat.Tables(0).Rows(i)("isnewmessage"), 0)
                    Next
                End If
                chatcount.InnerText = chatcounter


                Dim strblog As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Blogs_Get_Unread", Session("UserEmpID"))
                blogcount.InnerText = FormatNumber(strchat.Tables(0).Rows.Count.ToString(), 0)

                'My(ovatar)
                Dim myteam As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_PersonalDetail_get", Session("UserEmpID"))
                Dim bb As Integer = myteam.Tables(0).Rows.Count
                'Avatar for employees with no profile 
                Dim imgs As Byte() = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "SELECT imgfile FROM avatar")
                Dim base64 As String = Convert.ToBase64String(imgs)
                If bb > 0 Then
                    For i As Integer = 0 To bb - 1
                        'imgphoto.Src = "ImgHandler.ashx?imgid=" & myteam.Tables(0).Rows(0).Item("id").ToString
                        Dim imgsrc As String = (myteam.Tables(0).Rows(i)("imgtype"))
                        If imgsrc = "" Or Nothing Then
                            ovatarImg.Src = "images/blank-avatar.jpg"
                        Else
                            ovatarImg.Src = imgsrc
                        End If

                        'ovatar.InnerHtml = "<img style='margin-top:10px;' height='40' width='40' src='" + imgsrc + "' class='img-circle' alt='Admin'>"
                        'Dim imgg As Byte() = CType((myteam.Tables(0).Rows(i)("imgfile")), Byte())
                        'Dim base64String As String = Convert.ToBase64String(imgg)
                        'If imgg.Length = 0 Then                            
                        '    ovatar.InnerHtml = "<img style='margin-top:10px;' height='40' width='40' src='data:image/png;base64," + base64 + "' class='img-circle' alt='Admin'>"
                        'Else
                        '    ovatar.InnerHtml += "<img style='margin-top:10px;' height='40' width='40' src='data:image/png;base64," + base64String + "' class='img-circle' alt='Admin'>"
                        'End If
                    Next
                Else
                    ovatar.InnerHtml = "<img style='margin-top:10px;' height='40' width='40' src='data:image/png;base64," + base64 + "' class='img-circle' alt='Admin'>"
                End If

                If Session("UserEmpID") IsNot Nothing Then
                    If IsMyBirthday(Session("UserEmpID"), Date.Now) = True Then
                        imgBirthdayWish.Visible = True
                        'birthday_box.Style.Add("display", "none")
                        Dim rnd As New Random
                        Dim rrr As Integer = rnd.Next(1, 11)

                        If rrr = 1 Then
                            imgBirthdayWish.ImageUrl = "~/images/birthday01.png"
                        ElseIf rrr = 2 Then
                            imgBirthdayWish.ImageUrl = "~/images/birthday02.png"
                        ElseIf rrr = 3 Then
                            imgBirthdayWish.ImageUrl = "~/images/birthday03.png"
                        ElseIf rrr = 4 Then
                            imgBirthdayWish.ImageUrl = "~/images/birthday04.png"
                        ElseIf rrr = 5 Then
                            imgBirthdayWish.ImageUrl = "~/images/birthday05.png"
                        ElseIf rrr = 6 Then
                            imgBirthdayWish.ImageUrl = "~/images/birthday06.png"
                        ElseIf rrr = 7 Then
                            imgBirthdayWish.ImageUrl = "~/images/birthday07.png"
                        ElseIf rrr = 8 Then
                            imgBirthdayWish.ImageUrl = "~/images/birthday08.png"
                        ElseIf rrr = 9 Then
                            imgBirthdayWish.ImageUrl = "~/images/birthday09.png"
                        ElseIf rrr = 10 Then
                            imgBirthdayWish.ImageUrl = "~/images/birthday10.png"
                        Else
                            imgBirthdayWish.ImageUrl = "~/images/birthday01.png"
                        End If

                    Else
                        imgBirthdayWish.Visible = False
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub ClearApplicationCache()
        Dim keys As List(Of String) = New List(Of String)()
        Dim enumerator As IDictionaryEnumerator = Cache.GetEnumerator()

        While enumerator.MoveNext()
            keys.Add(enumerator.Key.ToString())
        End While

        For i As Integer = 0 To keys.Count - 1
            Cache.Remove(keys(i))
        Next
    End Sub

    Protected Sub Signout_Click(sender As Object, e As EventArgs)
        Try
            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Chat_User_Room_Update_Stat", 0, Session("LoginID"), "N")
            Session.Clear()
            Session.Abandon()
            FormsAuthentication.SignOut()
            Response.Redirect("~/Default", True)
            'FormsAuthentication.RedirectToLoginPage()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub LoadBirthdays(ByVal Birthday As Date)
        Try
            'gridbirthday.DataSource = Process.SearchDataP2("Emp_Birthdays", Birthday, Session("Organisation"))
            Dim bday As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Birthdays", Birthday, Session("Organisation"))
            If bday.Tables(0).Rows.Count > 0 Then
                c = bday.Tables(0).Rows.Count
                Dim y As StringBuilder = New StringBuilder("")
                If c > 0 Then
                    For i As Integer = 0 To c - 1
                        Dim imgsrc As String
                        Dim name As String = Convert.ToString(bday.Tables(0).Rows(i)("Name"))
                        Dim dept As String = Convert.ToString(bday.Tables(0).Rows(i)("Dept/Unit"))
                        If Not IsDBNull(bday.Tables(0).Rows(i)("imgtype")) Then
                            imgsrc = (bday.Tables(0).Rows(i)("imgtype"))
                            imgsrc = imgsrc.Remove(0, 2)
                        Else
                            imgsrc = "~/images/user.jpg"
                        End If

                        y.Append("<li class='media notification-message'><a href='#'><div class='media-left'><span class='avatar'>")
                        y.Append(String.Concat("<img style='margin-top:10px;' height='40' width='40' alt='" + name + "' src=""", Page.ResolveClientUrl(imgsrc), """>"))
                        'y.Append("<img style='margin-top:10px;' height='40' width='40' alt='" + name + "' src='" + imgsrc + ">' class='img-responsive img-circle'>")
                        y.Append("</span></div>")
                        y.Append("<div class='media-body'>")
                        y.Append("<p class='m-0 noti-details'><span class='noti-title'>" + name + "</span> has a  <span class='noti-title'>birthday today</span></p>")
                        y.Append("<p class='m-0'><span class='notification-time'>" + dept + "</span></p></div></a></li>")

                    Next
                End If
                Dim ww As String = y.ToString()
                birthday_box.InnerHtml = ww
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Function IsMyBirthday(ByVal EmpID As String, ByVal Birthday As Date) As Boolean
        Try
            Dim oResponse As Boolean = CBool(SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Emp_Is_It_My_Birthday", EmpID, Birthday))
            Return oResponse
        Catch ex As Exception
            Return False
        End Try
    End Function


End Class