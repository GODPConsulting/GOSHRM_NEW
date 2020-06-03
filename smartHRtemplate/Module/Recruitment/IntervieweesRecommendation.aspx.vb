Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports GOSHRM.GOSHRM.GOSHRM.BO

Public Class IntervieweesRecommendation
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
    Protected Sub LinkDownLoad(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim sid As String = CType(sender, LinkButton).CommandArgument
         

            Dim dt As DataTable = Process.SearchData("Recruit_Job_Interview_Comment_Get", sid)
            If dt IsNot Nothing Then
                downloadFile(CType(dt.Rows(0)("evaluationfileimage"), Byte()), dt.Rows(0)("evaluationfiletype").ToString(), dt.Rows(0)("evaluationfilename").ToString())
            End If
        Catch ex As Exception
            response.write(ex.Message)
        End Try
    End Sub
    Protected Sub downloadFile(ByVal bytefile As Byte(), ByVal filetype As String, ByVal filename As String)
        Dim bytes() As Byte = bytefile
        Response.Buffer = True
        Response.Charset = ""
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = filetype
        Response.AddHeader("content-disposition", "attachment;filename=" & filename)
        Response.BinaryWrite(bytes)
        Response.Flush()
        Response.End()
    End Sub
    Private Function LodaDataTable(jobid As Integer, candid As String) As DataTable
        Dim Datas As New DataTable
        Dim serach As String = ""
        search.Value = Session("intevieweesearch")
        If search.Value.Trim = "" Then
            Datas = Process.SearchDataP2("Recruit_Job_Interview_Applicant_Get", jobid, candid)
        Else
            Datas = Process.SearchDataP3("Recruit_Job_Interview_Applicant_Get_Search", jobid, candid, search.Value.Trim)
        End If
        Return Datas
    End Function

    Private Sub LoadData(id As Integer, candid As String)
        Try
            GridVwHeaderChckbox.DataSource = LodaDataTable(id, candid)
            GridVwHeaderChckbox.AllowSorting = True
            GridVwHeaderChckbox.DataBind()

            Dim strUser As New DataSet

            strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Job_Interview_Applicant_Get", id, candid)
            If strUser.Tables(0).Rows.Count > 0 Then
                pagetitle.InnerText = "Recommendation about " & strUser.Tables(0).Rows(0).Item("candidatename").ToString
            End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try


            If Not Me.IsPostBack Then
                If Session("intrecommsearch") Is Nothing Then
                    Session("intrecommsearch") = ""
                End If

                LoadData(CInt(Request.QueryString("id")), Request.QueryString("Email"))

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Private Sub GridVwHeaderChckbox_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridVwHeaderChckbox.RowCreated
        Try
            Process.SortArrow(e, SortsDirection, Session("intrecommsort"))
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub btnFind_Click(sender As Object, e As EventArgs)
        Try
            If search.Value.Trim = "" Then
                Session("intrecommsearch") = ""
            Else
                Session("intrecommsearch") = search.Value.Trim
            End If
            LoadData(CInt(Request.QueryString("id")), Request.QueryString("Email"))
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub





    Protected Sub GridVwHeaderChckbox_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridVwHeaderChckbox.RowDataBound
        Dim row As GridViewRow = e.Row
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.ToolTip = "Click to select this row."
            row.Cells(2).Text = row.Cells(2).Text.Replace(vbCrLf, "<br />")
        End If
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
    Protected Sub SortRecords(ByVal sender As Object, ByVal e As GridViewSortEventArgs) Handles GridVwHeaderChckbox.Sorting
        Try
            Dim sortExpression As String = e.SortExpression
            Session("intrecommsort") = sortExpression
            Dim direction As String = String.Empty
            If SortsDirection = SortDirection.Ascending Then
                SortsDirection = SortDirection.Descending
                direction = " DESC"
            Else
                SortsDirection = SortDirection.Ascending
                direction = " ASC"
            End If
            Dim table As New DataTable

            table = LodaDataTable(CInt(Request.QueryString("id")), Request.QueryString("Email"))
            table.DefaultView.Sort = sortExpression & direction
            GridVwHeaderChckbox.DataSource = table
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

End Class