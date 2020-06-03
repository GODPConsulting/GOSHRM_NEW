Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Public Class DirectReports
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "EMPWORKHISTORY"
    Private Sub BlockPageIndex(pagelow As Integer, pagehigh As Integer)
        Try
            LoadBlock(pagelow, pagehigh)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub UserDataBound()
        Try
            For Each row As DataListItem In dlBlogs.Items
                Dim lblempid As Label = row.FindControl("lblgender")

                Dim imagebtn As ImageButton = row.FindControl("imgavatar")

                imagebtn.ImageUrl = "ImgHandler.ashx?imgid=" & lblempid.Text
            Next
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub
   
    Private Function GetPageCount(ByVal totalcount As Integer) As Integer
        Try
            Dim pgcount As Integer = 0
            Dim rems As Integer = 0
            If totalcount <= 100 Then
                pgcount = 1
            Else
                pgcount = totalcount \ 100
                rems = totalcount Mod 100
                If rems > 0 Then
                    pgcount = pgcount + 1
                End If
            End If
            Return pgcount
        Catch ex As Exception
            Return 0
        End Try
    End Function
    Protected Sub MoveFirst(sender As Object, e As EventArgs)
        Try
            Session("drpagelow") = "1"
            Session("drpagehigh") = "100"
            Session("drpageno") = "1"

            pageno.InnerText = Session("drpageno")

            BlockPageIndex(Session("drpagelow"), Session("drpagehigh"))
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub MoveLast(sender As Object, e As EventArgs)
        Try
            Session("drpagelow") = (((CInt(Session("drpagetotal")) * 100) - 100) + 1).ToString
            Session("drpagehigh") = (CInt(Session("drpagetotal")) * 100).ToString
            Session("drpageno") = Session("drpagetotal")
            'Session("drpagetotal") = "1"

            pageno.InnerText = Session("drpageno")
            BlockPageIndex(Session("drpagelow"), Session("drpagehigh"))
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub MoveNext(sender As Object, e As EventArgs)
        Try
            Session("drpageno") = CInt(Session("drpageno")) + 1
            If CInt(Session("drpageno")) > CInt(Session("drpagetotal")) Then
                Session("drpageno") = CInt(Session("drpageno")) - 1
            End If

            Session("drpagelow") = (((CInt(Session("drpageno")) * 100) - 100) + 1).ToString
            Session("drpagehigh") = (CInt(Session("drpageno")) * 100).ToString

            pageno.InnerText = Session("drpageno")

            BlockPageIndex(Session("drpagelow"), Session("drpagehigh"))
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub MovePrevious(sender As Object, e As EventArgs)
        Try
            Session("drpageno") = CInt(Session("drpageno")) - 1
            If CInt(Session("drpageno")) <= 0 Then
                Session("drpageno") = 1
            End If

            Session("drpagelow") = (((CInt(Session("drpageno")) * 100) - 100) + 1).ToString
            Session("drpagehigh") = (CInt(Session("drpageno")) * 100).ToString

            pageno.InnerText = Session("drpageno")
            BlockPageIndex(Session("drpagelow"), Session("drpagehigh"))
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Function LoadEmpTypes(pagelow As Integer, pagehigh As Integer) As DataTable
        Dim datatables As New DataTable
        search.Value = Session("dreportssearch")
        If search.Value.Trim = "" Then
            datatables = Process.SearchDataP8("Emp_PersonalDetail_DirectReports", lblemp.Text)
        Else
            datatables = Process.SearchDataP4("Emp_PersonalDetail_DirectReports_Search", lblemp.Text, pagelow, pagehigh, search.Value.Trim)
        End If
        Dim nn As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select name from Employees_All where empid = '" & lblemp.Text & "'")
        pagetitle.InnerText = "Direct Reports (" & datatables.Rows.Count.ToString & ")"
        Return datatables
    End Function
    'Private Sub LoadGrid()
    '    Try

    '        gridDirectReports.DataSource = LoadEmpTypes()
    '        gridDirectReports.AllowSorting = True
    '        gridDirectReports.AllowPaging = True
    '        gridDirectReports.DataBind()
    '    Catch ex As Exception
    '        response.write(ex.message)
    '        Process.loadalert(divalert, msgalert, ex.message , "danger")
    '    End Try
    'End Sub
    Private Sub LoadBlock(pagelow As Integer, pagehigh As Integer)
        Try
            Dim sdatatable As New DataTable
            sdatatable = LoadEmpTypes(pagelow, pagehigh)
            dlBlogs.DataSource = sdatatable
            dlBlogs.DataBind()
            UserDataBound()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not Me.IsPostBack Then
                If Session("drpageno") Is Nothing Then
                    Session("drpageno") = "1"
                End If

                If Session("drpagetotal") Is Nothing Then
                    Session("drpagetotal") = "1"
                End If

                If Session("drpagelow") Is Nothing Then
                    Session("drpagelow") = "1"
                End If

                If Session("drpagehigh") Is Nothing Then
                    Session("drpagehigh") = "100"
                End If

                If Session("dreportssearch") Is Nothing Then
                    Session("dreportssearch") = ""
                End If

                If Request.QueryString("empid") IsNot Nothing Then
                    lblemp.Text = Request.QueryString("empid")
                Else
                    lblemp.Text = Session("UserEmpID")
                End If
                LoadBlock(Session("drpagelow"), Session("drpagehigh"))
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
            Process.loadalert(divalert, msgalert, ex.message, "danger")
        End Try
    End Sub



    Protected Sub btnFind_Click(sender As Object, e As EventArgs)
        Try

            Session("dreportssearch") = search.Value.Trim
            LoadBlock(Session("drpagelow"), Session("drpagehigh"))
            'End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.message, "danger")
        End Try
    End Sub



    Protected Sub btnExport_Click(sender As Object, e As EventArgs)
        Try

            If Process.ExportExcel(LoadEmpTypes(1, 1000000), "directreports") = False Then
                Process.loadalert(divalert, msgalert, Session("exception"), "warning")
            End If

        Catch ex As Exception

            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
End Class