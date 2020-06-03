Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Imports Telerik.Web.UI
Imports System.Web.UI
Imports System.Threading

Public Class HRISChat
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "EMPCHAT"
    Public Shared Separator() As Char = {"."c}
    Dim rans As New Random
    Protected Sub ChatRefresh(sender As Object, e As EventArgs) Handles Timer1.Tick
        Try
            'DateTime.Now.ToString("hh:mm:ss tt")
            If Session("LoginID") Is Nothing Then
                Response.Write("<script language='javascript'> { self.close() }</script>")
            End If

            Dim leftx As Integer = rans.Next(10, 100)
            Dim topx As Integer = rans.Next(10, 100)
            Dim livechat As New DataSet
            livechat = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Chat_User_Room_Launch", Session("LoginID"))

            If livechat.Tables(0).Rows.Count > 0 Then
                For i = 0 To livechat.Tables(0).Rows.Count - 1
                    Dim url As String = "Messenger.aspx?key=" + livechat.Tables(0).Rows(i).Item("roomid").ToString
                    Dim s As String = "window.open('" & url + "', '" + livechat.Tables(0).Rows(i).Item("name").ToString + "', 'width=600,height=600,left=" + leftx.ToString + ",top=" + topx.ToString + ",resizable=no');"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "AjaxWindow", s, True)
                Next
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
   

    Private Sub LoadGrid()
        Try
            Dim datatables As New DataTable
            datatables = Process.SearchDataP3("emp_list_chat", Process.companylist, Session("UserEmpID"), txtsearch.Text)

            dlBlogs.DataSource = datatables
            dlBlogs.DataBind()
            lblView.Text = datatables.Rows.Count.ToString() & " Members: " & Process.companylist.Replace(";", ", ")
        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try

    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not Me.IsPostBack Then
                Dim ismulti As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select isnull(ismulticompany,'No')  from general_info")
                If ismulti.ToLower = "no" Then
                    Process.LoadRadComboTextAndValueP2(cboCompany, "Company_Structure_Get_ByLevel", "1", Session("Access"), "name", "name", False)
                Else

                    Process.LoadRadComboTextAndValueP2(cboCompany, "Company_Structure_Get_ByLevel", "2", Session("Access"), "name", "name", False)
                End If
                If Process.companylist = "" Then
                    Process.companylist = Session("Organisation")
                End If

                For l As Integer = 0 To cboCompany.Items.Count - 1
                    If cboCompany.Items(l).Value.ToLower = Process.companylist.ToLower Then
                        cboCompany.Items(l).Checked = True
                        Exit For
                    End If
                Next

                'Process.AssignRadComboValue(cboCompany, Process.company)

                LoadGrid()
            End If
            lblView.Text = Process.companylist.Replace(";", ", ")
        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub
    Private Function GetIdentity(skey As String) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Chat_Room_Initiate"
            cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = skey
            cmd.Parameters.Add("@userid", SqlDbType.VarChar).Value = Session("LoginID")
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception
            Process.strExp = ex.Message
            Return 0
        End Try
    End Function

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            Dim rans As New Random
            Dim leftx As Integer = rans.Next(10, 50)
            Dim topx As Integer = rans.Next(10, 50)
            Dim temp As String = Date.Today.Day.ToString + "" + DateTime.Now.Minute.ToString + DateTime.Now.Second.ToString
            Dim key As String = GetIdentity(temp)
            Dim rowcount As Integer = 0
            For Each row As DataListItem In dlBlogs.Items
                ' Access the CheckBox
                Dim cb As CheckBox = row.FindControl("chkEmp")
                Dim lb As Label = row.FindControl("suserid")
                If cb IsNot Nothing AndAlso cb.Checked Then
                    rowcount = rowcount + 1
                    Dim ID As String = lb.Text  'Convert.ToString(dlBlogs.DataKeys(row.ItemIndex).Value)
                    ' "Delete" the row
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Chat_User_Room_Initiate", key, ID, "N")
                End If
            Next
            If rowcount > 0 Then
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Chat_User_Room_Initiate", key, Session("LoginID"), "Y")
                Dim url As String = "Messenger.aspx?key=" + key
                Dim s As String = "window.open('" & url + "', '" + temp + "', 'width=600,height=600,left=" + leftx.ToString + ",top=" + topx.ToString + ",resizable=no');"
                ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)
            Else
                Response.Write("No user selected for chat!")
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "No user selected for chat!" + "')", True)
            End If



        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

   

    Protected Sub txtsearch_TextChanged(sender As Object, e As EventArgs) Handles txtsearch.TextChanged
        Try
            Dim rowcount As Integer = 0
            For Each row As DataListItem In dlBlogs.Items
                ' Access the CheckBox
                Dim cb As CheckBox = row.FindControl("chkEmp")
                If cb IsNot Nothing AndAlso cb.Checked Then
                    Dim empID As String = Convert.ToString(dlBlogs.DataKeys(row.ItemIndex).Value)
                    rowcount = rowcount + 1
                    If rowcount = 1 And Process.selectlist = "" Then
                        Process.selectlist = empID
                    Else
                        Process.selectlist = Process.selectlist & ";" & empID
                    End If
                End If
            Next
            LoadGrid()
        Catch ex As Exception

        End Try

    End Sub


    Protected Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            Dim i As Integer = 0
            Process.companylist = ""
            If cboCompany.CheckedItems.Count > 0 Then
                Dim collection As IList(Of RadComboBoxItem) = cboCompany.CheckedItems
                If (collection.Count > 0) Then
                    For Each item As RadComboBoxItem In collection
                        i = i + 1
                        If i = 1 Then
                            Process.companylist = item.Value
                        Else
                            Process.companylist = Process.companylist & ";" & item.Value
                        End If
                    Next
                End If
            End If

            Process.LoadTooltipComboBox(cboCompany)
            LoadGrid()
        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    Protected Sub dlBlogs_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlBlogs.ItemDataBound
        Try
            For Each row As DataListItem In dlBlogs.Items
                ' Access the CheckBox
                Dim chcount As Label = row.FindControl("lblchatcount")
                If chcount.Text = "0" Then
                    chcount.Visible = False
                Else
                    chcount.Visible = True
                End If
                Dim imagebtn As ImageButton = row.FindControl("Image1")
                Dim ID As String = Convert.ToString(dlBlogs.DataKeys(row.ItemIndex).ToString())
                imagebtn.ImageUrl = "Imgchat.ashx?imgid=" & ID
                
            Next
        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub
End Class