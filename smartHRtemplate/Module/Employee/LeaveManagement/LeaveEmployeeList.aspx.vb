Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Public Class LeaveEmployeeList
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "LEAVECAL"

    'End Function

    Private Function LoadSurbodinateDataTable(empid As String, date1 As Date, date2 As Date) As DataTable
        Dim datatables As New DataTable
        datatables = Process.SearchDataP3("Employee_Leavelist_Approver_get_all", empid, date1, date2)

        Dim strLoan As New DataSet
        strLoan = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_PersonalDetail_get", empid)
        pagetitle.InnerText = strLoan.Tables(0).Rows(0).Item("fullname").ToString & " : " & Process.DDMONYYYY(date1) & " - " & Process.DDMONYYYY(date2)
        Return datatables
    End Function
    Private Sub LoadSurbodinateLeaves(emp As String, date1 As Date, date2 As Date, pageindex As Integer)
        Try
            GridView1.PageIndex = pageindex
            GridView1.DataSource = LoadSurbodinateDataTable(emp, date1, date2)
            GridView1.AllowSorting = True
            GridView1.AllowPaging = True
            GridView1.DataBind()

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub SortRecords(sender As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GridView1.Sorting
        Try
            Dim sortExpression As String = e.SortExpression
            Session("sortleaveExpression") = sortExpression
            Dim direction As String = String.Empty
            If SortsDirection = SortDirection.Ascending Then
                SortsDirection = SortDirection.Descending
                direction = " DESC"
            Else
                SortsDirection = SortDirection.Ascending
                direction = " ASC"
            End If
            Dim table As New DataTable
            table = LoadSurbodinateDataTable(Request.QueryString("EmpID"), Request.QueryString("datefrom"), Request.QueryString("dateto"))
            table.DefaultView.Sort = sortExpression & direction
            GridView1.PageIndex = CInt(Session("leavelistpageIndex"))
            GridView1.DataSource = table
            GridView1.DataBind()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                Dim script As String = "$(document).ready(function () { $('[id*=btnApprove]').click(); });"
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "load", script, True)
                If Session("leavelistpageIndex") Is Nothing Then
                    Session("leavelistpageIndex") = "0"
                End If

                LoadSurbodinateLeaves(Request.QueryString("EmpID"), Request.QueryString("datefrom"), Request.QueryString("dateto"), Session("leavelistpageIndex"))
                If Request.QueryString("EmpID") <> Session("UserEmpID") Then
                    btnDelete.Visible = False
                End If
            End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


    Protected Sub SortSurbodinateRecords(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Try
            Dim sortExpression As String = e.SortExpression
            Session("sortleaveExpression") = sortExpression
            Dim direction As String = String.Empty
            If SortsDirection = SortDirection.Ascending Then
                SortsDirection = SortDirection.Descending
                direction = " DESC"
            Else
                SortsDirection = SortDirection.Ascending
                direction = " ASC"
            End If
            Dim table As New DataTable
            table = LoadSurbodinateDataTable(Request.QueryString("EmpID"), Request.QueryString("datefrom"), Request.QueryString("dateto"))

            table.DefaultView.Sort = sortExpression & direction
            GridView1.PageIndex = Session("leavelistpageIndex")
            GridView1.DataSource = table
            GridView1.DataBind()
        Catch ex As Exception

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

    Private Sub GridView1_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowCreated
        Try
            Process.SortArrow(e, SortsDirection, Session("sortleaveExpression"))
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub OnRowSurbodinateDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound
        'If e.Row.RowType = DataControlRowType.DataRow Then
        '    e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GridView1, "Select$" & e.Row.RowIndex)
        '    e.Row.ToolTip = "Click to select this row."
        'End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim read As String = e.Row.Cells(4).Text
            Dim lrefno As String = Convert.ToString(GridView1.DataKeys(e.Row.RowIndex).Value) 'e.Row.Cells(2).Text
            For Each cell As TableCell In e.Row.Cells
                Dim imgProd As HyperLink = DirectCast(e.Row.FindControl("HyperLink1"), HyperLink)
                If read = Session("UserEmpID") Then
                    imgProd.NavigateUrl = "~/Module/Employee/LeaveManagement/LeaveDetails?id=" & lrefno
                End If
            Next
        End If
    End Sub

    Protected Sub Delete(sender As Object, e As EventArgs)
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Delete") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "warning")
                Exit Sub
            End If
            Dim count As Integer = 0
            Dim confirmValue As String = Request.Form("confirm_value")
            If confirmValue = "Yes" Then
                Dim atLeastOneRowDeleted As Boolean = False
                ' Iterate through the Products.Rows property
                For Each row As GridViewRow In GridView1.Rows
                    ' Access the CheckBox
                    Dim cb As CheckBox = row.FindControl("chkEmp")
                    If cb IsNot Nothing AndAlso cb.Checked Then
                        count = count + 1
                        ' Delete row! (Well, not really...)
                        atLeastOneRowDeleted = True
                        ' First, get the ProductID for the selected row
                        Dim ID As String = _
                            Convert.ToString(GridView1.DataKeys(row.RowIndex).Value)
                        ' "Delete" the row
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Employee_Leavelist_Delete", ID)
                    End If
                Next
                Process.loadalert(divalert, msgalert, count.ToString & " records successfully deleted", "success")
                LoadSurbodinateLeaves(Request.QueryString("EmpID"), Request.QueryString("datefrom"), Request.QueryString("dateto"), Session("leavelistpageIndex"))
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Protected Sub GridView1_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        Try
            GridView1.PageIndex = e.NewPageIndex
            'LoadLeaves(Session("LoadType"))
            Session("leavelistpageIndex") = e.NewPageIndex
            GridView1.DataSource = LoadSurbodinateDataTable(Request.QueryString("EmpID"), Request.QueryString("datefrom"), Request.QueryString("dateto"))
            GridView1.DataBind()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect("~/Module/Employee/LeaveManagement/leaveroster?id=mgr", True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

End Class