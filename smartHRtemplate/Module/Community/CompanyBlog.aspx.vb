Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Imports Telerik.Web.UI
Imports System.Web.UI
Imports System.Threading


Public Class CompanyBlog
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "HRBLACKBOARD"
    Public Shared Separator() As Char = {"."c}
    Private Function LoadBlogs() As DataTable
        Dim datatables As New DataTable
        If Session("LoadType") = "All" Then
            datatables = Process.SearchDataP2("Blogs_Get_All", Session("company"), cboBlogType.SelectedValue)
        ElseIf Session("LoadType") = "Find" Then
            datatables = Process.SearchDataP3("Blogs_Search", Session("company"), cboBlogType.SelectedValue, txtsearch.Value.Trim)
        End If

        pagetitle.InnerText = Session("company") & ": " & txtsearch.Value & " " & cboBlogType.SelectedValue & " Blog (" & datatables.Rows.Count & ")"

        Return datatables
    End Function

    Private Sub LoadGrid()
        Try
            dlBlogs.DataSource = LoadBlogs()
            dlBlogs.DataBind()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
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
                Process.LoadRadComboTextAndValueInitiate(cboBlogType, "Blogs_Type_Get_All", "Show All", "name", "name")
                If Session("blogtype") IsNot Nothing Then
                    If Session("blogtype") <> "" Then
                        Process.AssignRadComboValue(cboBlogType, Session("blogtype"))
                    End If
                End If

                Dim ismulti As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select isnull(ismulticompany,'No')  from general_info")
                If ismulti.ToLower = "no" Then
                    Process.LoadRadComboTextAndValueP2(cboCompany, "Company_Structure_Get_ByLevel", "1", Session("Access"), "name", "name", False)
                Else

                    Process.LoadRadComboTextAndValueP2(cboCompany, "Company_Structure_Get_ByLevel", "2", Session("Access"), "name", "name", False)
                End If
                If Session("company") Is Nothing Then
                    Session("company") = Session("Organisation")
                End If
                Process.AssignRadComboValue(cboCompany, Session("company"))

                If Session("blogsearch") IsNot Nothing Then
                    If Session("blogtype") <> "" Then
                        txtsearch.Value = Session("blogsearch")
                        Session("LoadType") = "Find"
                    Else
                        Session("LoadType") = "All"
                    End If
                Else
                    Session("LoadType") = "All"
                End If

                LoadGrid()
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try

            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "danger")
                Exit Sub
            End If
            Response.Redirect("~/Module/Employee/BlackboardView.aspx", True)

        Catch ex As Exception
             Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


    Protected Sub btnGo_Click(sender As Object, e As EventArgs)
        Try
            If txtsearch.Value.Trim = "" Then
                Session("LoadType") = "All"
            Else                Session("LoadType") = "Find"
            End If
            LoadGrid()
            Session("blogtype") = cboBlogType.SelectedValue
            Session("blogsearch") = txtsearch.Value.Trim
        Catch ex As Exception
             Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Private Sub cboCompany_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboCompany.SelectedIndexChanged
        Try
            Session("company") = cboCompany.SelectedValue
        Catch ex As Exception
             Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub lnkBlogType_Click(sender As Object, e As EventArgs) Handles lnkBlogType.Click
        Try
            Response.Write("<script language='javascript'> { popup = window.open(""BlogType.aspx"" , ""Stone Details"", ""height=600,width=800,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no"");  popup.focus(); }</script>")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    'Protected Sub dlBlogs_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlBlogs.ItemDataBound
    '    Try
    '        For Each row As DataListItem In dlBlogs.Items
    '            ' Access the CheckBox
    '            Dim chars As Integer = 0
    '            Dim charRows As Integer = 0
    '            Dim lb As TextBox = row.FindControl("Label2")
    '            Dim tbcontent As String = lb.Text
    '            lb.Columns = 10
    '            chars = tbcontent.Length
    '            charRows = chars / lb.Columns

    '            Dim remaining As Integer = chars - charRows * lb.Columns
    '            If remaining = 0 Then
    '                lb.Rows = charRows
    '                lb.TextMode = TextBoxMode.MultiLine
    '            Else
    '                lb.Rows = charRows + 1
    '                lb.TextMode = TextBoxMode.MultiLine
    '            End If
    '        Next
    '    Catch ex As Exception
    '        Response.Write(ex.Message)
    '        ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
    '    End Try
    'End Sub

    Protected Sub btnAdd0_Click(sender As Object, e As EventArgs) Handles btnAdd0.Click
        Try
            Dim rowcount As Integer = 0

            Dim confirmValue As String = Request.Form("confirm_value")
            'Dim confirmValue As String = "Yes"
            If confirmValue = "Yes" Then
                For Each row As DataListItem In dlBlogs.Items
                    ' Access the CheckBox
                    Dim cb As CheckBox = row.FindControl("chkEmp")
                    If cb IsNot Nothing AndAlso cb.Checked Then
                        rowcount = rowcount + 1
                        Dim ID As String = Convert.ToString(dlBlogs.DataKeys(row.ItemIndex).ToString)
                        ' "Delete" the row
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Blogs_Delete", ID)
                    End If
                Next
                If rowcount > 0 Then
                    Process.GetAuditTrailInsertandUpdate(rowcount.ToString & " records deleted!", "", "Delete", "Company Blog")
                End If
                LoadGrid()
                Process.loadalert(divalert, msgalert, "Delete Successful", "success")
            Else
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Delete has been cancelled" + "')", True)
            End If
            
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
End Class