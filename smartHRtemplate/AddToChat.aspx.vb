Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Imports Telerik.Web.UI
Imports System.Web.UI
Imports System.Threading


Public Class AddToChat
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "EMPCHAT"
    Public Shared Separator() As Char = {"."c}
    Dim rans As New Random

    Private Sub LoadGrid()
        Try
            Dim datatables As New DataTable
            If Request.QueryString("key") IsNot Nothing Then
                datatables = Process.SearchDataP4("emp_list_add_to_chat", Process.companylist, Session("UserEmpID"), txtsearch.Text, Request.QueryString("key").ToString)
            Else
                datatables = Process.SearchDataP4("emp_list_add_to_chat", Process.companylist, Session("UserEmpID"), txtsearch.Text, "0")
            End If


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
                System.Threading.Thread.Sleep(300)
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

                If Request.QueryString("key") IsNot Nothing Then
                    lblroom.Text = "Add Users to Chat Room"
                    lblView.Text = Process.companylist.Replace(";", ", ")
                End If

                LoadGrid()
            End If
           
           
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
            Process.selectlist = ""
            Dim found As Boolean = False
            Dim rowcount As Integer = 0
            For Each row As DataListItem In dlBlogs.Items
                ' Access the CheckBox
                Dim cb As CheckBox = row.FindControl("chkEmp")
                Dim lb As Label = row.FindControl("suserid")
                If cb IsNot Nothing AndAlso cb.Checked Then
                    rowcount = rowcount + 1
                    If rowcount = 1 Then
                        Process.selectlist = lb.Text
                    Else
                        Process.selectlist = Process.selectlist & Process.Separators1 & lb.Text
                    End If
                End If
            Next
            Process.Arrays = Process.selectlist.Split(Process.Separators1, StringSplitOptions.RemoveEmptyEntries)
            If rowcount < 1 Then
                Response.Write("no select made!")
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "no selection made" + "')", True)
            End If

            If Request.QueryString("key") Is Nothing Then
                Dim strRoomCount As New DataSet
                strRoomCount = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Chat_Room_Get_Counts", Session("LoginID"), rowcount)
                If strRoomCount.Tables(0).Rows.Count > 0 Then
                    For j As Integer = 0 To strRoomCount.Tables(0).Rows.Count - 1
                        Process.checkid = strRoomCount.Tables(0).Rows(j).Item("roomid").ToString
                        For y As Integer = 0 To Process.Arrays.Count - 1
                            found = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Chat_Room_Check_User", Process.Arrays(y), Process.checkid)
                            If found = False Then
                                Exit For 'loop y
                            End If
                        Next
                        If found = True Then
                            Exit For 'loop y
                        End If
                    Next
                End If
            End If

            If found = True Then
                Session("curchatid") = Process.checkid
                Response.Write("<script language='javascript'> { self.close() }</script>")
                Exit Sub
            End If

            Dim temp As String = DateTime.Today.Day.ToString + "" + DateTime.Now.Minute.ToString + DateTime.Now.Second.ToString
            Dim key As String = ""
            If Request.QueryString("key") IsNot Nothing Then
                key = Request.QueryString("key")
            Else
                key = GetIdentity(temp)
            End If

            If Process.Arrays.Count > 0 Then
                For i As Integer = 0 To Process.Arrays.Count - 1
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Chat_User_Room_Initiate", key, Process.Arrays(i).ToString(), "N")
                Next
            End If

            'For Each row As DataListItem In dlBlogs.Items
            '    ' Access the CheckBox
            '    Dim cb As CheckBox = row.FindControl("chkEmp")
            '    Dim lb As Label = row.FindControl("suserid")
            '    If cb IsNot Nothing AndAlso cb.Checked Then
            '        rowcount = rowcount + 1
            '        Dim ID As String = lb.Text
            '        ' "Delete" the row
            '        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Chat_User_Room_Initiate", key, ID, "N")
            '    End If
            'Next
            If Request.QueryString("key") Is Nothing Then
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Chat_User_Room_Initiate", key, Session("LoginID"), "Y")
            End If
            Session("curchatid") = key
            Response.Write("<script language='javascript'> { self.close() }</script>")
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

    Private Sub dlBlogs_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlBlogs.ItemDataBound
        Try
            For Each row As DataListItem In dlBlogs.Items
                ' Access the CheckBox

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