Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Public Class EmployeeLeaveList
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "LEAVE"

    'End Function

    Private Function LoadSurbodinateDataTable(empid As String, date1 As Date, date2 As Date) As DataTable
        Dim datatables As New DataTable
        datatables = Process.SearchDataP3("Employee_Leavelist_Approver_get_all", empid, date1, date2)
        Return datatables
    End Function
    Private Sub LoadSurbodinateLeaves(emp As String, date1 As Date, date2 As Date, pageindex As Integer)
        Try
            GridView1.PageIndex = pageindex
            GridView1.DataSource = LoadSurbodinateDataTable(emp, date1, date2)
            GridView1.AllowSorting = True
            GridView1.AllowPaging = True
            GridView1.DataBind()

            Dim strLoan As New DataSet
            strLoan = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_PersonalDetail_get", emp)
            pagetitle.InnerText = strLoan.Tables(0).Rows(0).Item("fullname").ToString & " : " & Process.DDMONYYYY(date1) & " - " & Process.DDMONYYYY(date2)
            'lnkDirectReportLeave.Text = "Direct Report Leaves(" & GridView1.Rows.Count & ")"
        Catch ex As Exception
            response.write(ex.message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            pagetitle.InnerText = "Leaves"

            If Not Me.IsPostBack Then
                Dim script As String = "$(document).ready(function () { $('[id*=btnApprove]').click(); });"
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "load", script, True)
                'Leave
                Session("pageIndex1") = 0
                LoadSurbodinateLeaves(Request.QueryString("EmpID"), Request.QueryString("datefrom"), Request.QueryString("dateto"), Session("pageIndex1"))
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub


    Protected Sub SortSurbodinateRecords(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Try
            Dim sortExpression As String = e.SortExpression
            Dim direction As String = String.Empty
            If SortDirection = SortDirection.Ascending Then
                SortDirection = SortDirection.Descending
                direction = " DESC"
            Else
                SortDirection = SortDirection.Ascending
                direction = " ASC"
            End If
            Dim table As New DataTable
            table = LoadSurbodinateDataTable(Request.QueryString("EmpID"), Request.QueryString("datefrom"), Request.QueryString("dateto"))

            table.DefaultView.Sort = sortExpression & direction
            GridView1.PageIndex = Session("pageIndex1")
            GridView1.DataSource = table
            GridView1.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect("employeeleaves", True)
        Catch ex As Exception
        End Try
    End Sub

    Public Property SortDirection() As SortDirection
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
    'Protected Sub OnRowSurbodinateDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound
    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        Dim read As String = e.Row.Cells(4).Text
    '        Dim lrefno As String = Convert.ToString(GridView1.DataKeys(e.Row.RowIndex).Value) 'e.Row.Cells(2).Text
    '        For Each cell As TableCell In e.Row.Cells
    '            Dim imgProd As HyperLink = DirectCast(e.Row.FindControl("HyperLink1"), HyperLink)
    '            If read = Session("UserEmpID") Then
    '                imgProd.NavigateUrl = "~/Module/Employee/LeaveManagement/LeaveDetails.aspx?id=" & lrefno
    '            End If
    '        Next
    '    End If
    'End Sub

    Protected Sub Delete(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try

            Dim confirmValue As String = Request.Form("confirm_value")
            If confirmValue = "Yes" Then
                Dim atLeastOneRowDeleted As Boolean = False
                ' Iterate through the Products.Rows property
                For Each row As GridViewRow In GridView1.Rows
                    ' Access the CheckBox
                    Dim cb As CheckBox = row.FindControl("chkEmp")
                    If cb IsNot Nothing AndAlso cb.Checked Then
                        ' Delete row! (Well, not really...)
                        atLeastOneRowDeleted = True
                        ' First, get the ProductID for the selected row
                        Dim ID As String = _
                            Convert.ToString(GridView1.DataKeys(row.RowIndex).Value)
                        ' "Delete" the row
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Employee_Leavelist_HR_Delete", ID)
                    End If
                Next
                LoadSurbodinateLeaves(Request.QueryString("EmpID"), Request.QueryString("datefrom"), Request.QueryString("dateto"), Session("pageIndex1"))
            Else
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Delete has been cancelled" + "')", True)
            End If
        Catch ex As Exception
            response.write(ex.message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    'Protected Sub btnApply_Click(sender As Object, e As EventArgs) Handles btnApply.Click
    '    Try
    '        If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
    '            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
    '            Exit Sub
    '        End If
    '        Process.loadtype = "Add"
    '        Response.Write("<script language='javascript'> { popup = window.open(""LeaveRequest.aspx"" , ""Stone Details"", ""height=600,width=700,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no"");  popup.focus(); }</script>")
    '    Catch ex As Exception
    '        response.write(ex.message)
    '        ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
    '    End Try
    'End Sub

    Protected Sub GridView1_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        Try
            GridView1.PageIndex = e.NewPageIndex
            'LoadLeaves(Session("LoadType"))
            Session("pageIndex1") = e.NewPageIndex
            GridView1.DataSource = LoadSurbodinateDataTable(Request.QueryString("EmpID"), Request.QueryString("datefrom"), Request.QueryString("dateto"))
            GridView1.DataBind()
        Catch ex As Exception
            response.write(ex.message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub


    'Protected Sub btnSubFind_Click(sender As Object, e As EventArgs) Handles btnSubFind.Click
    '    Try
    '        If txtSubSearch.Text.Trim = "" Then
    '            Session("LoadType") = "All"
    '        Else
    '            Session("LoadType") = "Find"
    '        End If

    '        LoadSurbodinateLeaves(Session("LoadType"), 0)
    '        lblView.Text = txtSubSearch.Text & " " & radSubStatus.SelectedItem.Text & " Leaves"
    '        'End If
    '    Catch ex As Exception
    '        response.write(ex.message)
    '        ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
    '    End Try
    'End Sub




End Class