Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Imports Telerik.Web.UI
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Threading

Public Class Chat
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "EMPCHAT"
    Public Shared Separator() As Char = {"."c}
    Dim rans As New Random
    Protected Sub ChatRefresh(sender As Object, e As EventArgs) Handles refresh.Tick
        Try
            'reload.Enabled = False
            'DateTime.Now.ToString("hh:mm:ss tt")

            If Session("LoginID") Is Nothing Then
                Dim s = "self.close();"
                ClientScript.RegisterStartupScript(Me.GetType(), "endscript", s, True)
            End If
            RefreshTypingStat(Session("LoginID"))
            LoadMembers(Session("LoginID"))
            MembersDataBound()
            Session("prevchatid") = Process.curchatcount
            LoadComments(Session("curchatid"))
            MsgDataBound()
            'reload.Enabled = True
            If Session("prevchatid") <> Process.curchatcount Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Javascript", "javascript:UpdateMsgScroll();", True)
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    Private Sub RefreshTypingStat(username As String)
        Try
            If txmsg.Value.Trim <> "" Then
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Chat_User_Room_Typing_Stat", Session("curchatid"), Session("LoginID"), Session("LoginID"))
            End If
            lblstatus.Text = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Chat_User_Room_Typing_Get_Stat", Session("curchatid"), Session("LoginID"))
        Catch ex As Exception

        End Try

    End Sub

    Private Sub LoadMembers(username As String)
        Try
            If Session("curchatid") = 0 Then
                Dim strFirst As New DataSet
                strFirst = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Chat_Lists_All", username)
                If strFirst.Tables(0).Rows.Count > 0 Then
                    Session("curchatid") = strFirst.Tables(0).Rows(0).Item("roomid").ToString()
                    lblView.Text = strFirst.Tables(0).Rows(0).Item("members").ToString()
                    'If CInt(strFirst.Tables(0).Rows(0).Item("counts").ToString()) > 1 Then
                    '    lnkExit.Visible = True
                    'Else
                    '    lnkExit.Visible = False
                    'End If

                End If
            End If

            Dim datatables As New DataTable
            If txtsearch.Text.Trim = "" Then
                datatables = Process.SearchData("Chat_Lists_All", username)
            Else
                datatables = Process.SearchDataP2("Chat_Lists_Search", username, txtsearch.Text.Trim)
            End If
            dlchats.DataSource = datatables
            dlchats.DataBind()

            If datatables.Rows.Count > 0 Then
                lnkAdd.Visible = True
                lnkExit.Visible = True
            Else
                lnkAdd.Visible = False
                lnkExit.Visible = False
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    Private Sub LoadComments(sid As Integer)
        Try
            If Session("prevchatid") > 0 Then
                If Session("prevchatid") <> sid Then
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Chat_User_Room_Update_Stat", Session("prevchatid"), Session("LoginID"), "N")
                End If
            End If

            Dim strRoom As New DataSet
            strRoom = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Chat_Room_Get", sid, Session("LoginID"))
            If strRoom.Tables(0).Rows.Count > 0 Then
                lblView.Text = strRoom.Tables(0).Rows(0).Item("members").ToString
            End If


            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Chat_User_Room_Update_Stat", sid, Session("LoginID"), "Y")
            Dim datatables As New DataTable
            datatables = Process.SearchDataP2("Chat_Message_Get", sid, Session("LoginID"))
            dlcomments.DataSource = datatables
            dlcomments.DataBind()
            Process.curchatcount = datatables.Rows.Count()

        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not Me.IsPostBack Then
                Session("curchatid") = 0
                Session("prevchatid") = 0
                'Me.Form.DefaultButton = btnPost.ID

                LoadMembers(Session("LoginID"))
                LoadComments(Session("curchatid"))
            End If
            'lblView.Text = Process.companylist.Replace(";", ", ")
        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub


    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            Dim widthx As Integer = 400
            Dim heightx As Integer = 600
            Dim leftx As Integer = (Request.Browser.ScreenPixelsWidth - widthx) / 2
            Dim topx As Integer = (Request.Browser.ScreenPixelsHeight - heightx) / 2

            Dim url As String = "AddToChat.aspx"
            'Dim s As String = "window.open('" & url + "', 'AddChat', 'width=400,height=600,left=" + leftx.ToString + ",top=" + topx.ToString + ",resizable=no');"
            Dim s As String = "window.open('" & url + "', 'AddChat', 'width=" + widthx.ToString + ",height=" + heightx.ToString + ",left=" + leftx.ToString + ",top=" + topx.ToString + ",resizable=no');"
            ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)


        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub


    Protected Sub txtsearch_TextChanged(sender As Object, e As EventArgs) Handles txtsearch.TextChanged
        Try

        Catch ex As Exception

        End Try

    End Sub


    Protected Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        Try
            If txmsg.Value.Trim <> "" Then
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Chat_Message_Update", Session("curchatid"), Session("LoginID"), txmsg.Value)
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Chat_User_Room_Typing_Stat", Session("curchatid"), Session("LoginID"), "")
                txmsg.Value = ""
                'Me.Form.DefaultButton = btnPost.ID
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    Protected Sub dlchats_ItemCommand(source As Object, e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles dlchats.ItemCommand
        Try
            If e.CommandName = "chatdetail" Then

                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                Dim lnkmembers As LinkButton = e.Item.FindControl("lnkmembers")
                Dim lblmemcount As Label = e.Item.FindControl("lblmemcount")
                If Session("prevchatid") <> Session("curchatid") Then
                    Session("prevchatid") = Session("curchatid")
                End If
                Session("curchatid") = index
                lnkAdd.Visible = True

                LoadComments(index)

                'If CInt(lblmemcount.Text) > 1 Then
                '    lnkExit.Visible = True
                'Else
                '    lnkExit.Visible = False
                'End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkExit_Click(sender As Object, e As EventArgs) Handles lnkExit.Click
        Try
            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Chat_User_Room_Delete", Session("curchatid"), Session("LoginID"))
            Dim strFirst As New DataSet
            strFirst = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Chat_Lists_All", Session("LoginID"))
            If strFirst.Tables(0).Rows.Count > 0 Then
                Session("curchatid") = strFirst.Tables(0).Rows(0).Item("roomid").ToString()
                lblView.Text = strFirst.Tables(0).Rows(0).Item("members").ToString()
            End If
            LoadMembers(Session("LoginID"))
            LoadComments(Session("curchatid"))
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs) Handles lnkAdd.Click
        Try
            Dim rans As New Random
            Dim widthx As Integer = 400
            Dim heightx As Integer = 600
            Dim leftx As Integer = (Request.Browser.ScreenPixelsWidth - widthx) / 2
            Dim topx As Integer = (Request.Browser.ScreenPixelsHeight - heightx) / 2
            Dim url As String = "AddToChat.aspx?key=" + Session("curchatid").ToString() & "&room=" & Process.criteria
            Dim s As String = "window.open('" & url + "', 'AddChat', 'width=" + widthx.ToString + ",height=" + heightx.ToString + ",left=" + leftx.ToString + ",top=" + topx.ToString + ",resizable=no');"
            ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)

        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub


    Protected Sub reload_Tick(sender As Object, e As EventArgs) Handles reload.Tick
        Try

        Catch ex As Exception

        End Try
    End Sub
    Private Sub MembersDataBound()
        Try
            For Each row As DataListItem In dlchats.Items
                ' Access the CheckBox
                'Dim chcount As Label = row.FindControl("lblchatcount")
                Dim usercount As Label = row.FindControl("lblmemcount")
                Dim userempid As Label = row.FindControl("lblmember")
                Dim lblmembers As LinkButton = row.FindControl("lnkmembers")
                Dim chcount As HtmlGenericControl = row.FindControl("spchatcount")

                If chcount.InnerText = "0" Then
                    chcount.Visible = False
                    lblmembers.Font.Bold = False
                Else
                    chcount.Visible = True
                    lblmembers.Font.Bold = True
                    'chcount.Text = "New Message!"
                End If

                Dim imagebtn As ImageButton = row.FindControl("Image1")
                Dim ID As String = userempid.Text

                If CInt(usercount.Text) > 1 Then
                    imagebtn.ImageUrl = "images/group_chat.png"
                Else
                    imagebtn.ImageUrl = "Imgchat.ashx?imgid=" & ID
                End If
            Next
        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub
    Private Sub MsgDataBound()
        Try
            Dim prevuser As String = ""
            Dim curuser As String = ""
            For Each row As DataListItem In dlcomments.Items
                ' Access the CheckBox
                Dim lb As Label = row.FindControl("lbluser")
                curuser = lb.Text
                Dim selfmsg As Control = row.FindControl("selfmsg")
                Dim othermsg As Control = row.FindControl("othersmsg")
                If lb.Text.ToLower = "you" Or lb.Text.ToLower = "you:" Then
                    selfmsg.Visible = True
                    othermsg.Visible = False
                Else
                    selfmsg.Visible = False
                    othermsg.Visible = True
                End If
                If curuser = prevuser Then
                    lb.Visible = False
                Else
                    lb.Visible = True
                End If

                prevuser = curuser
            Next
        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub
    Protected Sub DeleteMessage(sender As Object, e As EventArgs)  'Handles btnFind.Click
        Try
            Dim sid As String = CType(sender, HtmlAnchor).Title
            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Chat_Message_Delete", sid)
        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

End Class