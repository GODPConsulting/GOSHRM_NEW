Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports GOSHRM.GOSHRM.GOSHRM.BO

Public Class Interviewees
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
    Private Function LodaDataTable(interviewid As Integer) As DataTable
        Dim Datas As New DataTable

        search.Value = Session("intevieweesearch")
        If search.Value.Trim = "" Then
            Datas = Process.SearchData("Recruit_Job_Interview_Applicant_Get_All", interviewid)
        Else
            Datas = Process.SearchDataP2("Recruit_Job_Interview_Applicant_Search", interviewid, search.Value.Trim)
        End If

        Dim strHeader As New DataSet
        strHeader = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Job_Interview_Applicant_Get_All", interviewid)
        If strHeader.Tables(0).Rows.Count > 0 Then
            pagetitle.InnerText = CDate(strHeader.Tables(0).Rows(0).Item("interviewdate")).ToLongDateString & " " & strHeader.Tables(0).Rows(0).Item("Title").ToString & " 's Shortlist Interview"
        End If
        Return Datas
    End Function

    Private Sub LoadData(interviewid As Integer)
        Try
            GridVwHeaderChckbox.PageIndex = CInt(Session("intevieweePageIndex"))
            GridVwHeaderChckbox.DataSource = LodaDataTable(interviewid)
            GridVwHeaderChckbox.AllowSorting = True
            GridVwHeaderChckbox.AllowPaging = True
            GridVwHeaderChckbox.DataBind()

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                If Session("intevieweesearch") Is Nothing Then
                    Session("intevieweesearch") = ""
                End If

                If Session("intevieweePageIndex") Is Nothing Then
                    Session("intevieweePageIndex") = "0"
                End If

                If Request.QueryString("id") IsNot Nothing Then
                    Session("interviewid") = Request.QueryString("id")
                End If
                LoadData(CInt(Session("interviewid")))
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Private Sub GridVwHeaderChckbox_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridVwHeaderChckbox.RowCreated
        Try
            Process.SortArrow(e, SortsDirection, Session("intevieweeSort"))
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub GridVwHeaderChckbox_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridVwHeaderChckbox.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GridVwHeaderChckbox, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
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
            Session("intevieweeSort") = sortExpression
            Dim direction As String = String.Empty
            If SortsDirection = SortDirection.Ascending Then
                SortsDirection = SortDirection.Descending
                direction = " DESC"
            Else
                SortsDirection = SortDirection.Ascending
                direction = " ASC"
            End If
            Dim table As New DataTable

            table = LodaDataTable(CInt(Session("interviewid")))
            table.DefaultView.Sort = sortExpression & direction
            GridVwHeaderChckbox.PageIndex = CInt(Session("intevieweePageIndex"))
            GridVwHeaderChckbox.DataSource = table
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub GridVwHeaderChckbox_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridVwHeaderChckbox.PageIndexChanging
        Try
            GridVwHeaderChckbox.PageIndex = e.NewPageIndex
            Session("intevieweePageIndex") = e.NewPageIndex
            GridVwHeaderChckbox.DataSource = LodaDataTable(CInt(Session("interviewid")))
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub btnFind_Click(sender As Object, e As EventArgs)
        Try
            If search.Value.Trim = "" Then
                Session("testsearch") = ""
            Else
                Session("testsearch") = search.Value.Trim
            End If
            LoadData(CInt(Session("interviewid")))
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnClose_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect("~/Module/Recruitment/JobInterviews", True)
        Catch ex As Exception
        End Try
    End Sub
End Class