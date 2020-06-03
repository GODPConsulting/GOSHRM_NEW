Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports GOSHRM.GOSHRM.GOSHRM.BO

Public Class EmployeeShiftHistory
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "EMPSHIFT"
    Dim AuthenCode2 As String = "TEAMSHIFT"
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
    Private Function LodaDataTable(LoadType As String) As DataTable
        Dim Datas As New DataTable
        Datas = Process.SearchData("Job_Employee_Shift_Team_Get_Emp", LoadType)
        Return Datas
    End Function

    Private Sub LoadData(LoadType As String)
        Try
            gridTrainers.DataSource = LodaDataTable(LoadType)
            gridTrainers.AllowSorting = True
            gridTrainers.AllowPaging = True
            gridTrainers.DataBind()

            Dim strUser As New DataSet

            strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_PersonalDetail_get", LoadType)
            If strUser.Tables(0).Rows.Count > 0 Then
                lblHeader.Text = strUser.Tables(0).Rows(0).Item("fullname").ToString
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
                Session("LoadType") = "All"
                Session("EmpID") = Request.QueryString("empid")
                LoadData(Session("EmpID"))

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
            lblStatus.Text = ""
            If Process.AuthenAction(Session("role"), AuthenCode, "Delete") = False And Process.AuthenAction(Session("role"), AuthenCode2, "Delete") = False Then
                lblStatus.Text = "You don't have privilege to perform this action"
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
                Exit Sub
            End If
            Dim confirmValue As String = Request.Form("confirm_value")
            If confirmValue = "Yes" Then
                Dim atLeastOneRowDeleted As Boolean = False
                ' Iterate through the Products.Rows property
                For Each row As GridViewRow In gridTrainers.Rows
                    ' Access the CheckBox
                    Dim cb As CheckBox = row.FindControl("chkEmp")
                    If cb IsNot Nothing AndAlso cb.Checked Then
                        ' Delete row! (Well, not really...)
                        atLeastOneRowDeleted = True
                        ' First, get the ProductID for the selected row
                        Dim ID As String = Convert.ToString(gridTrainers.DataKeys(row.RowIndex).Value)
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Job_Employee_Shift_Delete", ID)
                    End If
                Next
                LoadData(Session("EmpID"))
            Else
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Delete has been cancelled" + "')", True)
            End If
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

            table = LodaDataTable(Session("EmpID"))
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
            gridTrainers.DataSource = LodaDataTable(Session("EmpID"))
            gridTrainers.DataBind()
        Catch ex As Exception
            lblStatus.Text = ex.Message
        End Try
    End Sub

End Class