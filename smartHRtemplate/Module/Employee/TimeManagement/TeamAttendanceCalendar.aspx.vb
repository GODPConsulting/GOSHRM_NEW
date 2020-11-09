Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Public Class TeamAttendanceCalendar
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "APPATTENDANCE"
    Dim AuthenCode2 As String = "TEAMATTENDANCE"
    'TEAMATTENDANCE
    Dim Pages As String = "Team Attendance"
    Dim apptype As String = ConfigurationManager.AppSettings("apptype")
    Private socialEvents As DataTable

   
    Private Function LoadEmpTypes(LoadType As String) As DataTable
        Dim datatables As New DataTable
        pagetitle.InnerText = txtsearch.Value & " My Team Work Shift"
        If LoadType = "All" Then
            datatables = Process.SearchDataP2("Time_Employee_Attendance_Get_My_Team", Session("UserEmpID"), MyCalendar.SelectedDate)
        ElseIf LoadType = "Find" Then
            datatables = Process.SearchDataP3("Time_Employee_Attendance_Search_My_Team", Session("UserEmpID"), MyCalendar.SelectedDate, txtsearch.Value.Trim)
        End If

        Return datatables
    End Function
    Private Sub LoadGrid(LoadType As String)
        Try
            GridVwHeaderChckbox.DataSource = LoadEmpTypes(LoadType)
            GridVwHeaderChckbox.AllowSorting = True
            GridVwHeaderChckbox.AllowPaging = True
            GridVwHeaderChckbox.DataBind()
            pagetitle.InnerText = txtsearch.Value & " " & MyCalendar.SelectedDate.ToLongDateString & " Team Attendance"
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not Me.IsPostBack Then

                Session("LoadType") = "All"
                MyCalendar.SelectedDate = Date.Now
                Session("pageIndex1") = 0
                LoadGrid(Session("LoadType"))


            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub SortRecords(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
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
            Dim table As DataTable = LoadEmpTypes(Session("LoadType"))
            table.DefaultView.Sort = sortExpression & direction
            GridVwHeaderChckbox.PageIndex = Session("pageIndex1")
            GridVwHeaderChckbox.DataSource = table
            GridVwHeaderChckbox.DataBind()
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


    Protected Sub GridVwHeaderChckbox_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridVwHeaderChckbox.PageIndexChanging
        Try
            GridVwHeaderChckbox.PageIndex = e.NewPageIndex
            Session("pageIndex1") = e.NewPageIndex
            GridVwHeaderChckbox.DataSource = LoadEmpTypes(Session("LoadType"))
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub OnRowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridVwHeaderChckbox.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                For Each cell As TableCell In e.Row.Cells
                    Dim read As String = e.Row.Cells(10).Text.Trim
                    Dim imgProd As HyperLink = DirectCast(e.Row.FindControl("HyperLink1"), HyperLink)
                    If read = "" Or read.Contains("nbsp") Then
                        imgProd.NavigateUrl = ""
                        imgProd.Enabled = False
                    End If

                Next
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs)
        Try

            If txtsearch.Value = "" Then
                Session("LoadType") = "All"
            Else
                Session("LoadType") = "Find"
            End If
            'If Not Me.IsPostBack Then
            LoadGrid(Session("LoadType"))
            'End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


    Protected Sub MyCalendar_SelectionChanged(sender As Object, e As EventArgs) Handles MyCalendar.SelectionChanged
        Try
            LoadGrid(Session("LoadType"))
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    Protected Sub Approve(sender As Object, e As EventArgs) Handles btapprove.Click
        Try
            Dim msgbuild As New StringBuilder()
            Dim Separators() As Char = {";"c}
            Process.loadalert(divalert, msgalert, "", "warning")
            If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False Then
                Process.loadalert(divalert, msgalert, Process.privilegemsg, "warning")

                Exit Sub
            End If
            Dim count As Integer = 0
            Dim confirmValue As String = Request.Form("confirm_value")
            'If confirmValue = "Yes" Then
            Dim atLeastOneRowApproved As Boolean = False
            ' Iterate through the Products.Rows property
            For Each row As GridViewRow In GridVwHeaderChckbox.Rows
                ' Access the CheckBox
                Dim cb As CheckBox = row.FindControl("chkEmp")
                If cb IsNot Nothing AndAlso cb.Checked Then
                    count = count + 1
                    ' Delete row! (Well, not really...)
                    atLeastOneRowApproved = True
                    ' First, get the ProductID for the selected row
                    Dim ID As String =
                        Convert.ToString(GridVwHeaderChckbox.DataKeys(row.RowIndex).Value)
                    ' "Delete" the row
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Time_Employee_Attendance_Update_All_Status", ID, "Approved", "Approved", Session("UserEmpID"))
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Time_Employee_Attendance_Detail_My_Team", ID)
                    If strUser.Tables(0).Rows.Count > 0 Then
                        Dim empid As String = strUser.Tables(0).Rows(0).Item("empid").ToString
                        Dim empname As String = strUser.Tables(0).Rows(0).Item("Name").ToString
                        Dim mgrid As String = strUser.Tables(0).Rows(0).Item("managerid").ToString
                        Dim dateshift As String = strUser.Tables(0).Rows(0).Item("datenames").ToString
                        Dim shifts As String = strUser.Tables(0).Rows(0).Item("shifts").ToString
                        Dim checkindate As String = strUser.Tables(0).Rows(0).Item("checkindate").ToString
                        Dim checkoutdate As String = strUser.Tables(0).Rows(0).Item("checkoutdate").ToString
                        Dim checkintime As String = strUser.Tables(0).Rows(0).Item("checkindate").ToString
                        Dim checkouttime As String = strUser.Tables(0).Rows(0).Item("checkoutdate").ToString
                        Dim agreedtime As String = strUser.Tables(0).Rows(0).Item("agreedovertime").ToString
                        Dim shiftduration As Double = strUser.Tables(0).Rows(0).Item("duration")
                        Dim actualduration As Double = strUser.Tables(0).Rows(0).Item("actualduration")
                        Dim mgrname As String = strUser.Tables(0).Rows(0).Item("MgrName").ToString
                        Dim Link = Process.ApplicationURL & "/" & "Module/TimeManagement/OvertimeApprovals.aspx?id=" + ID
                        msgbuild.Clear()
                        msgbuild.AppendLine("Employee       : " & empname)
                        msgbuild.AppendLine(" ")
                        msgbuild.AppendLine("Shift          : " & shifts)
                        msgbuild.AppendLine("Duration (Hrs) : " & shiftduration.ToString)
                        msgbuild.AppendLine(" ")
                        msgbuild.AppendLine("CheckIn Date   : " & checkindate)
                        msgbuild.AppendLine("CheckIn Time   : " & checkintime)
                        msgbuild.AppendLine(" ")
                        msgbuild.AppendLine("CheckOut Date  : " & checkoutdate)
                        msgbuild.AppendLine("CheckOut Time  : " & checkouttime)
                        msgbuild.AppendLine(" ")
                        msgbuild.AppendLine("Work Hours     : " & actualduration.ToString)
                        msgbuild.AppendLine("System Overtime: " & (actualduration - shiftduration).ToString)
                        msgbuild.AppendLine(" ")
                        msgbuild.AppendLine("Approval Stat  : Approved by " & " by " & mgrname)
                        msgbuild.AppendLine(" ")
                        msgbuild.AppendLine("Agreed Overtime (Hr): " & agreedtime)


                        Process.MailNotification(checkindate, 2, checkindate & " Overtime Approval Request by " & mgrname & " on behalf of " & empname, msgbuild.ToString, empid, mgrid, Process.GetEmpIDMailList("hr"), empid, "")

                        Dim Arrays() As String = Process.GetEmpIDMailList("hr").Split(Separators, StringSplitOptions.RemoveEmptyEntries)
                        For i = 0 To Arrays.Count - 1
                            Process.MailNotification(checkindate, 2, checkindate & " Overtime Approval Request by " & mgrname & " on behalf of " & empname, msgbuild.ToString, Process.GetEmpIDMailList("hr"), mgrid, empid, Link, "")
                        Next
                    End If
                End If

            Next
            Process.loadalert(divalert, msgalert, count.ToString & " records successfully approved", "success")

            LoadGrid(Session("LoadType"))

            'End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub



End Class