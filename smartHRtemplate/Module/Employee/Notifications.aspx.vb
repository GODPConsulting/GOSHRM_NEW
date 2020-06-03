Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports Telerik.Web.UI
Public Class Notifications
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "EMPNOTIFICATION"
    Public Shared copies() As String
    Public Shared mailsto() As String
    Public Shared Separators() As Char = {";"c}
    Protected Sub ToggleRowSelection(ByVal sender As Object, ByVal e As EventArgs)

        TryCast(TryCast(sender, CheckBox).NamingContainer, GridItem).Selected = TryCast(sender, CheckBox).Checked
        Dim checkHeader As Boolean = True
        For Each dataItem As GridDataItem In gridMailList.MasterTableView.Items
            If Not TryCast(dataItem.FindControl("CheckBox1"), CheckBox).Checked Then
                checkHeader = False
                Exit For
            End If
        Next
        Dim headerItem As GridHeaderItem = TryCast(gridMailList.MasterTableView.GetItems(GridItemType.Header)(0), GridHeaderItem)
        TryCast(headerItem.FindControl("headerChkbox"), CheckBox).Checked = checkHeader
    End Sub
    Protected Sub ToggleSelectedState(ByVal sender As Object, ByVal e As EventArgs)
        Dim headerCheckBox As CheckBox = TryCast(sender, CheckBox)
        For Each dataItem As GridDataItem In gridMailList.MasterTableView.Items
            TryCast(dataItem.FindControl("CheckBox1"), CheckBox).Checked = headerCheckBox.Checked
            dataItem.Selected = headerCheckBox.Checked
        Next
    End Sub
    Private Function GetData() As DataTable
        search.Value = Session("mailsearch")
        Dim s As New DataTable
        If Session("clicked") = 1 Then
            If search.Value.Trim = "" Then
                s = Process.SearchData("Emp_MailBox_Received_get_all", Session("UserEmpID"))
            Else
                s = Process.SearchDataP2("Emp_MailBox_Received_search", Session("UserEmpID"), search.Value.Trim)
            End If
        Else
        End If
        Return s
    End Function

    Private Sub LoadGrid()
        Try
            Dim sdTable As DataTable = GetData()
            gridMailList.CurrentPageIndex = CInt(Session("mailindex"))
            gridMailList.DataSource = sdTable
            gridMailList.DataBind()

            Dim strDashBoard As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_MailBox_Received_Unread_Get_All", Session("UserEmpID"))
            lnkWorkHistory.Text = "Inbox(" & strDashBoard.Tables(0).Rows.Count.ToString & ")"

            Dim stroutbx As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_MailBox_sent_Unread_Get_All", Session("UserEmpID"))
            lnkoutbox.Text = "Outbox(" & stroutbx.Tables(0).Rows.Count.ToString & ")"
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Read") = False Then
                Process.loadalert(divalert, msgalert, Process.privilegemsg, "warning")

                Exit Sub
            End If

            If Not Me.IsPostBack Then
                If Session("clicked") Is Nothing Then
                    Session("clicked") = "0"
                End If

                MultiView1.ActiveViewIndex = 0

                If Session("mailsearch") Is Nothing Then
                    Session("mailsearch") = ""
                End If

                If Session("mailindex") Is Nothing Then
                    Session("mailindex") = "0"
                End If

                LoadGrid()
                lnkWorkHistory_Click(sender, e)
                If Request.QueryString("id") IsNot Nothing Then
                    DrillDown(Request.QueryString("id"))
                End If
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.message, "danger")
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

    Protected Sub btnFind_Click(sender As Object, e As EventArgs)
        Try
            'If Not Me.IsPostBack Then
            Session("mailsearch") = search.Value.Trim
            LoadGrid()
            'End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btDelete.Click
        Try

            If Process.AuthenAction(Session("role"), AuthenCode, "Delete") = False Then
                Process.loadalert(divalert, msgalert, Process.privilegemsg, "warning")

                Exit Sub
            End If
            Dim count As Integer = 0
            Dim confirmValue As String = Request.Form("confirm_value")
            If confirmValue = "Yes" Then
                If MultiView1.ActiveViewIndex = 1 Then
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_MailBox_Received_delete", Process.selectlist)
                Else
                    Dim atLeastOneRowDeleted As Boolean = False
                    Dim myCheckBox As New CheckBox()
                    Dim myText As New GridTemplateColumn()
                    For Each myItem As GridDataItem In gridMailList.MasterTableView.Items

                        myCheckBox = DirectCast(myItem("CheckBoxTemplateColumn").FindControl("CheckBox1"), System.Web.UI.WebControls.CheckBox)

                        If myCheckBox IsNot Nothing AndAlso myCheckBox.Checked Then
                            count = count + 1
                            Dim id As Integer = CInt(myItem.Cells(4).Text)
                            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_MailBox_Received_delete", id)
                        End If
                    Next
                    Process.loadalert(divalert, msgalert, count.ToString & " records successfully deleted", "success")
                    LoadGrid()
                End If
            End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Sub DefaultLinkFontSize(ByVal defval As String, ByVal curval As String, ByVal linkb As LinkButton)
        Try
            lnkWorkHistory.Font.Bold = False
            lnkoutbox.Font.Bold = False
            lnkWorkHistory.Font.Size = FontUnit.Parse(defval.ToString)
            lnkoutbox.Font.Size = FontUnit.Parse(defval.ToString) 'FontUnit.Point(defval)
            linkb.Font.Size = FontUnit.Parse(defval.ToString) 'FontUnit.Point(curval)
            linkb.Font.Bold = True
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub lnkWorkHistory_Click(sender As Object, e As EventArgs) Handles lnkWorkHistory.Click
        Try
            Session("clicked") = 1
            MultiView1.ActiveViewIndex = 0
            Session("LoadType") = "All"
            DefaultLinkFontSize("12px", "13px", lnkWorkHistory)
            LoadGrid()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles lnkoutbox.Click
        Try
            Session("clicked") = 2
            MultiView1.ActiveViewIndex = 0
            Session("LoadType") = "All"
            DefaultLinkFontSize("12px", "13px", lnkoutbox)
            LoadGrid()
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub LinkDownLoad(ByVal sender As Object, ByVal e As EventArgs)

        Try
            Dim sid As String = CType(sender, LinkButton).CommandArgument

            Process.selectlist = sid
            DrillDown(sid)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.message, "danger")
        End Try
    End Sub

    Protected Sub DrillDown(id As String)
        Try
            lblpath0.Text = ""
            lblpath1.Text = ""
            lblpath2.Text = ""
            lblpath3.Text = ""

            mailliattach1.Visible = False
            mailliattach2.Visible = False
            mailliattach3.Visible = False
            mailliattach4.Visible = False

            ' lblDate.Text = CType(sender, LinkButton).CommandArgument
            MultiView1.ActiveViewIndex = 1
            Dim strUser As New DataSet
            strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_MailBox_Received_Get", id)
            LoadGrid()
            mailsubject.InnerText = strUser.Tables(0).Rows(0).Item("subject").ToString

            Dim context As String = ""
            mailtime.InnerText = strUser.Tables(0).Rows(0).Item("daterecevied")
            mailsender.InnerText = strUser.Tables(0).Rows(0).Item("SenderName").ToString

            'txtBody.Text = strUser.Tables(0).Rows(0).Item("body").ToString
            mailsto = strUser.Tables(0).Rows(0).Item("receiver").ToString.Split(Separators, StringSplitOptions.RemoveEmptyEntries)
            Dim copied As String = ""
            Dim mailto As String = ""
            For i = 0 To mailsto.Count - 1
                Dim copiedemp As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select a.firstname + ' ' + a.lastname  from dbo.Employees_All a where empid = '" & mailsto(i).ToString.Trim & "'")
                If copiedemp = "" Or copiedemp Is Nothing Then
                    copiedemp = mailsto(i).ToString.Trim
                End If
                If i = 0 Then
                    mailto = copiedemp
                Else
                    mailto = mailto & ", " & copiedemp
                End If

            Next
            mailreceiver.InnerText = "To: " & mailto

            copies = strUser.Tables(0).Rows(0).Item("copied").ToString.Split(Separators, StringSplitOptions.RemoveEmptyEntries)
            For i = 0 To copies.Count - 1
                Dim copiedemp As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select a.firstname + ' ' + a.lastname  from dbo.Employees_All a where empid = '" & copies(i).ToString.Trim & "'")
                If copiedemp = "" Or copiedemp Is Nothing Then
                    copiedemp = copies(i).ToString.Trim
                End If
                If i = 0 Then
                    copied = copiedemp
                Else
                    copied = copied & ", " & copiedemp
                End If

            Next
            copied = "CC: " & copied

            'context = context & vbNewLine & vbNewLine & copied
            'context = context & vbNewLine & vbNewLine & vbNewLine & strUser.Tables(0).Rows(0).Item("body").ToString
            mailcontent.InnerHtml = strUser.Tables(0).Rows(0).Item("body").ToString.Replace(vbCrLf, vbNewLine)

            lnknavigate.Text = strUser.Tables(0).Rows(0).Item("link").ToString
            If lnknavigate.Text.Trim = "" Then
                lnknavigate.Visible = False
            End If

            Dim strattachment As New DataSet
            strattachment = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_MailBox_Attachement_Get", id)
            For i As Integer = 0 To strattachment.Tables(0).Rows.Count - 1
                If i = 0 Then
                    lblpath0.Text = strattachment.Tables(0).Rows(i).Item("filepath").ToString()

                    Dim arrAttachment As String()
                    arrAttachment = lblpath0.Text.Split(Process.Separators2)
                    If arrAttachment.Length > 0 Then
                        mailliattach1.Visible = True
                        'mailattach1.InnerText = arrAttachment(arrAttachment.Length - 1).ToString
                        lnkattach.InnerText = arrAttachment(arrAttachment.Length - 1).ToString
                    End If
                ElseIf i = 1 Then
                    lblpath1.Text = strattachment.Tables(0).Rows(i).Item("filepath").ToString()
                    Dim arrAttachment As String()
                    arrAttachment = lblpath1.Text.Split(Process.Separators2)
                    If arrAttachment.Length > 0 Then
                        mailliattach2.Visible = True
                        'mailattach2.InnerText = arrAttachment(arrAttachment.Length - 1).ToString
                        lnkattach.InnerText = arrAttachment(arrAttachment.Length - 1).ToString
                    End If
                ElseIf i = 2 Then
                    lblpath2.Text = strattachment.Tables(0).Rows(i).Item("filepath").ToString()
                    Dim arrAttachment As String()
                    arrAttachment = lblpath2.Text.Split(Process.Separators2)
                    If arrAttachment.Length > 0 Then
                        mailliattach3.Visible = True
                        'mailattach3.InnerText = arrAttachment(arrAttachment.Length - 1).ToString
                        lnkattach.InnerText = arrAttachment(arrAttachment.Length - 1).ToString
                    End If
                ElseIf i = 3 Then
                    lblpath3.Text = strattachment.Tables(0).Rows(i).Item("filepath").ToString()
                    Dim arrAttachment As String()
                    arrAttachment = lblpath3.Text.Split(Process.Separators2)
                    If arrAttachment.Length > 0 Then
                        mailliattach4.Visible = True
                        'mailattach1.InnerText = arrAttachment(arrAttachment.Length - 1).ToString
                        lnkattach.InnerText = arrAttachment(arrAttachment.Length - 1).ToString
                    End If
                End If
            Next
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub lnknavigate_Click(sender As Object, e As EventArgs) Handles lnknavigate.Click
        Try
            Response.Redirect(lnknavigate.Text, True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Private Sub attachment0_Click(sender As Object, e As System.EventArgs)
        Try
            Process.downloadFile(lblpath0.Text)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Private Sub attachment1_Click(sender As Object, e As System.EventArgs)
        Try
            Process.downloadFile(lblpath1.Text)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Private Sub attachment2_Click(sender As Object, e As System.EventArgs)
        Try
            Process.downloadFile(lblpath2.Text)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Private Sub attachment3_Click(sender As Object, e As System.EventArgs)
        Try
            Process.downloadFile(lblpath3.Text)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Private Sub gridMailList_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles gridMailList.ItemDataBound
        Try
            If TypeOf e.Item Is GridDataItem Then
                Dim objread As TableCell = TryCast(e.Item, GridDataItem)("markread")
                Dim asread As Boolean = CBool(objread.Text)

                For Each cell As TableCell In e.Item.Cells
                    Dim imgProd As ImageButton = DirectCast(e.Item.FindControl("imgBetsmen1"), ImageButton)
                    If asread = True Then
                        imgProd.ImageUrl = "~/Images/read-mail.png"
                    Else
                        imgProd.ImageUrl = "~/Images/mail.png"
                    End If
                Next
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gridMailList_PageIndexChanged(sender As Object, e As Telerik.Web.UI.GridPageChangedEventArgs) Handles gridMailList.PageIndexChanged
        Try
            Session("mailindex") = e.NewPageIndex
            LoadGrid()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkattachment_Click(sender As Object, e As EventArgs)
        Try
            Process.downloadFile(lblpath0.Text)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
End Class