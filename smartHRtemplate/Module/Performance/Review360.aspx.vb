Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports GOSHRM.GOSHRM.GOSHRM.BO

Public Class Review360
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "JOBPOST"
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
    Private Function LoadDataTable(ByVal summaryid As Integer, stype As String) As DataTable
        Dim sss As String = ""

        If Request.QueryString("type") = "s" Then
            sss = "Manager's Review"
        ElseIf Request.QueryString("type") = "d" Then
            sss = "Reports' Review"
        ElseIf Request.QueryString("type") = "p" Then
            sss = "Peer's Review"
        Else
            sss = "Self Review"
        End If

        Dim datatables As New DataTable
        If Session("LoadType") = "All" Then
            datatables = Process.SearchDataP2("Performance_Appraisal_360_Get_Reviews", summaryid, stype)
        ElseIf Session("LoadType") = "Find" Then
            datatables = Process.SearchDataP3("Performance_Appraisal_360_Search_Reviews", summaryid, stype, txtsearch.Text.Trim)
        End If

        If datatables.Rows.Count > 0 Then
            lblmaxgrade.Text = datatables.Rows(0).Item("maxgrade").ToString()
        End If

        Dim strForm As New DataSet
        strForm = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Appraisal_Summary_Get", summaryid)
        If strForm.Tables(0).Rows.Count > 0 Then
            lblHeader.Text = sss & ": " & txtsearch.Text & " " & strForm.Tables(0).Rows(0).Item("EmpName").ToString() & "'s " & strForm.Tables(0).Rows(0).Item("period").ToString() & " 360 Appraisal"
        End If

        Return datatables
    End Function
    Private Sub LoadData(ByVal summaryid As String, stype As String)
        Try
            'sessions = Session("JobID")
            Dim sstable As DataTable = LoadDataTable(summaryid, stype)
            gridTrainers.DataSource = sstable
            gridTrainers.AllowSorting = True
            gridTrainers.DataBind()

        Catch ex As Exception
            lblStatus.Text = ex.Message
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                Session("LoadType") = "All"
                LoadData(Request.QueryString("summaryid"), Request.QueryString("type"))
            End If
        Catch ex As Exception
            lblStatus.Text = ex.Message
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub





    Protected Sub gridTrainers_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridTrainers.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gridTrainers, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
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

            table = Process.SearchData("Recruit_Applications_Get", CInt(Request.QueryString("Jobid")))
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
            gridTrainers.DataSource = LoadDataTable(Request.QueryString("summaryid"), Request.QueryString("type"))
            gridTrainers.DataBind()
        Catch ex As Exception
            lblStatus.Text = ex.Message
        End Try
    End Sub

    
    Protected Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click
        Try
            If txtsearch.Text.Trim = "" Then
                Session("LoadType") = "All"
            Else
                Session("LoadType") = "Find"
            End If
            LoadData(Request.QueryString("summaryid"), Request.QueryString("type"))
        Catch ex As Exception

        End Try
    End Sub
End Class