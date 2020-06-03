Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports GOSHRM.GOSHRM.GOSHRM.BO

Public Class EmployeeAttendance
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "EMPSHIFT"
    Dim marketrate As Double = 0
    Dim interestrate As Double = 0
    Dim monthlypay As Double
    Dim loanamount As Double = 0
    Dim tenor As Integer = 0
    Dim fairvalue As Double = 0
    Dim EIR As Double = 0
    Dim AmortEIR As Double = 0
    Dim AmortFairValue As Double = 0
    Dim repaystartdate As Date
    Dim EMPID As String = ""
    Dim LoanType As String = ""
    Dim trainingapproval As New clsEmpTraningApproval
    Dim olddata(3) As String
    Dim Pages As String = ""
    Private Function LodaDataTable(empid As String, datefrom As Date, dateto As Date) As DataTable
        Dim Datas As New DataTable
        Datas = Process.SearchDataP3("Time_Employee_Attendance_Get_Present", empid, datefrom, dateto)
        Return Datas
    End Function
    Private Function LodaNullDataTable(empid As String, datefrom As Date, dateto As Date) As DataTable
        Dim Datas As New DataTable
        Datas = Process.SearchDataP3("Time_Employee_Attendance_Get_All", empid, datefrom, dateto)
        Return Datas
    End Function

    Private Sub LoadData()
        Try
            If chkAbsent.Checked Then
                gridTrainers.DataSource = LodaNullDataTable(Session("EmpID"), Session("Date1"), Session("Date2"))
                lblHeader.Text = lblHeader.Text & " with days absent"
            Else
                gridTrainers.DataSource = LodaDataTable(Session("EmpID"), Session("Date1"), Session("Date2"))
                lblHeader.Text = lblHeader.Text & " days present only"
            End If

            gridTrainers.AllowSorting = True
            gridTrainers.AllowPaging = True
            gridTrainers.DataBind()

            Dim strUser As New DataSet
            lblHeader.Text = ""
            strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_PersonalDetail_get", Session("EmpID"))
            If strUser.Tables(0).Rows.Count > 0 Then
                lblHeader.Text = strUser.Tables(0).Rows(0).Item("fullname").ToString & " : " & Session("Date1") & " to " & Session("Date2")
            End If
            'interviewdate


        Catch ex As Exception
            lblStatus.Text = ex.Message
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try


            If Not Me.IsPostBack Then
                Session("PreviousPage") = Request.UrlReferrer.ToString
                Session("EmpID") = Request.QueryString("empid")
                Session("Date1") = Request.QueryString("start")
                Session("Date2") = Request.QueryString("to")
                LoadData()

            End If
        Catch ex As Exception
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub


    Protected Sub gridTrainers_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridTrainers.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gridTrainers, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub

    Protected Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            Response.Redirect(Session("PreviousPage").ToString)
        Catch ex As Exception
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
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
    Protected Sub SortRecords(ByVal sender As Object, ByVal e As GridViewSortEventArgs) Handles gridTrainers.Sorting
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

            If chkAbsent.Checked Then
                table = LodaNullDataTable(Session("EmpID"), Session("Date1"), Session("Date2"))
            Else
                table = LodaDataTable(Session("EmpID"), Session("Date1"), Session("Date2"))
            End If

            table.DefaultView.Sort = sortExpression & direction
            gridTrainers.DataSource = table
            gridTrainers.DataBind()
        Catch ex As Exception
            lblStatus.Text = ex.Message
        End Try
    End Sub

    Protected Sub gridTrainers_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gridTrainers.PageIndexChanging
        Try
            gridTrainers.PageIndex = e.NewPageIndex
            If chkAbsent.Checked Then
                gridTrainers.DataSource = LodaNullDataTable(Session("EmpID"), Session("Date1"), Session("Date2"))
            Else
                gridTrainers.DataSource = LodaDataTable(Session("EmpID"), Session("Date1"), Session("Date2"))
            End If
            gridTrainers.DataBind()
        Catch ex As Exception
            lblStatus.Text = ex.Message
        End Try
    End Sub

    Protected Sub chkAbsent_CheckedChanged(sender As Object, e As EventArgs) Handles chkAbsent.CheckedChanged
        Try
            LoadData()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnRequest_Click(sender As Object, e As EventArgs) Handles btnRequest.Click
        Try
            Dim url As String = "overtimeapprovalrequest.aspx?empid=" & Session("EmpID")
            Dim s As String = "window.open('" & url + "', 'popup_window', 'width=700,height=600,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no');"
            ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)

        Catch ex As Exception
            lblStatus.Text = ex.Message
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & lblStatus.Text + "')", True)
        End Try
    End Sub
End Class