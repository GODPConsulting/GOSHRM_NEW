Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Imports Telerik.Web.UI
Imports System.Web.UI
Imports System.Threading


Public Class BlackBoard
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "EMPBLACKBOARD"
    Public Shared Separator() As Char = {"."c}
    Private Function LoadBlogs() As DataTable
        Dim datatables As New DataTable
        If Session("LoadType") = "All" Then
            datatables = Process.SearchDataP2("Emp_Blogs_Get_All", Session("UserEmpID"), cboBlogType.SelectedValue)
        ElseIf Session("LoadType") = "Find" Then
            datatables = Process.SearchDataP3("Emp_Blogs_Search", Session("UserEmpID"), cboBlogType.SelectedValue, txtsearch.Value.Trim)
        End If

        pagetitle.InnerText = Process.GetCompanyByEmpID(Session("UserEmpID")) & ": " & txtsearch.Value & " " & cboBlogType.SelectedValue & " Blog (" & datatables.Rows.Count & ")"

        Return datatables
    End Function

    Private Sub LoadGrid()
        Try
            'dlBlogs.DataSource = LoadBlogs()
            'dlBlogs.DataBind()
            Dim strDashBoard As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Blogs_Get_All", Session("UserEmpID"), cboBlogType.SelectedValue)
            Dim n As StringBuilder = New StringBuilder("")
            Dim imgs As Byte() = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "SELECT imgfile FROM avatar")
            Dim base64String As String
            Dim m As Integer = strDashBoard.Tables(0).Rows.Count
            If m > 0 Then
                For i As Integer = 0 To m - 1
                    Dim postedby As String = Convert.ToString(strDashBoard.Tables(0).Rows(i)("PostedBy"))
                    Dim heading As String = Convert.ToString(strDashBoard.Tables(0).Rows(i)("heading"))
                    Dim post As String = Convert.ToString(strDashBoard.Tables(0).Rows(i)("message"))
                    Dim postedon As String = Convert.ToString(strDashBoard.Tables(0).Rows(i)("createdon"))
                    Dim id As String = Convert.ToString(strDashBoard.Tables(0).Rows(i)("id"))
                    If Not IsDBNull((strDashBoard.Tables(0).Rows(i)("imgfile"))) And (strDashBoard.Tables(0).Rows(i)("imgfile")) IsNot Nothing Then
                        Dim imgg As Byte() = CType((strDashBoard.Tables(0).Rows(i)("imgfile")), Byte())
                        If imgg.Length = 0 Then
                            base64String = Convert.ToBase64String(imgs)
                        Else
                            base64String = Convert.ToBase64String(imgg)
                        End If
                    Else
                        base64String = Convert.ToBase64String(imgs)
                    End If
                    n.Append("<li><div class='activity-user'>")
                    n.Append("<a href='profile.html' data-toggle='tooltip' class='avatar' data-original-title='Lesley Grauer'>")
                    n.Append("<img alt='" + postedby + "' src='data:image/png;base64," + base64String + "' class='img-responsive img-circle'></a></div>") '
                    n.Append("<div class='activity-content'><div class='timeline-content'>")
                    n.Append("<a href='#' class='name'>" + postedby + "</a> added new blog topic <a href='BlackBoardView.aspx?id=" + id + "'><b style='color:#1BA691'>" + heading + "</b></a>")
                    n.Append("<span class='time'>on " + postedon + "</span></div></div></li>")

                Next
            Else
                n.Append("<li><div class='activity-content'><div class='timeline-content'>")
                n.Append("<a href='#' class='name'>No Blog Post</a></div></div></li>")

            End If
            Dim rr As String = n.ToString()
            blogger.InnerHtml = rr
            strDashBoard.Clear()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try

       
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Read") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "danger")
                Exit Sub
            End If

            If Not Me.IsPostBack Then
                Process.LoadRadComboTextAndValueInitiate(cboBlogType, "Blogs_Type_Get_All", "Show All", "name", "name")
                If Session("blogtype") IsNot Nothing Then
                    If Session("blogtype") <> "" Then
                        Process.AssignRadComboValue(cboBlogType, Session("blogtype"))
                    End If
                End If

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
End Class