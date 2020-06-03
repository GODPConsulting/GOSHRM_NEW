Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports GOSHRM.GOSHRM.GOSHRM.BO

Public Class Review360Detail
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
   
    Private Sub LoadData()
        Try
            'sessions = Session("JobID")
            Dim sstable As DataTable = Process.SearchData("Performance_Appraisal_360_Get_Reviews_Detail", Request.QueryString("id"))
            gridTrainers.DataSource = sstable
            gridTrainers.AllowSorting = True
            gridTrainers.DataBind()
            Dim strForm As New DataSet
            strForm = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Appraisal_Summary_Get", Request.QueryString("summaryid"))
            If strForm.Tables(0).Rows.Count > 0 Then
                lblHeader.Text = strForm.Tables(0).Rows(0).Item("EmpName").ToString() & "'s " & strForm.Tables(0).Rows(0).Item("period").ToString() & " 360 Appraisal"
            End If
            lblreview.Text = Request.QueryString("reviewer") & "'s review"
        Catch ex As Exception
            lblStatus.Text = ex.Message
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                Process.requestedURL = Request.UrlReferrer.ToString
                LoadData()
            End If
        Catch ex As Exception
            lblStatus.Text = ex.Message
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub





    Protected Sub gridTrainers_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridTrainers.RowDataBound
        Dim row As GridViewRow = e.Row
        If e.Row.RowType = DataControlRowType.DataRow Then
            row.Cells(1).Text = row.Cells(1).Text.Replace(vbCrLf, "<br />")
            row.Cells(2).Text = row.Cells(2).Text.Replace(vbCrLf, "<br />")
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
       
    End Sub



    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Try
            Response.Redirect(Process.requestedURL, True)
        Catch ex As Exception

        End Try
    End Sub
End Class