Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports GOSHRM.GOSHRM.GOSHRM.BO

Public Class Applicants
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

    Private Sub LoadData(ByVal sessions As String)
        Try
            'sessions = Session("JobID")

            search.Value = Session("applicantsearch")
            Dim sstable As New DataTable

            If search.Value.Trim = "" Then
                sstable = Process.SearchDataP2("Recruit_Applications_Get", sessions, Request.QueryString("type"))
            Else
                sstable = Process.SearchDataP3("Recruit_Applications_Get_Search", sessions, Request.QueryString("type"), search.Value.Trim)
            End If

            gridTrainers.PageIndex = CInt(Session("applicantindex"))
            gridTrainers.DataSource = sstable
            gridTrainers.AllowSorting = True
            gridTrainers.AllowPaging = True
            gridTrainers.DataBind()

            Dim strUser As New DataSet
            strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Job_Post_get", sessions)
            pagetitle.InnerText = strUser.Tables(0).Rows(0).Item("Title").ToString & "(" & sstable.Rows.Count & ")"


        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")            
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not Me.IsPostBack Then
                Session("FromApplicant") = "True"
                If Session("PreviousRPage") IsNot Nothing Then
                    If Session("PreviousRPage").ToString.ToLower.Contains("/applicants") = False Then
                        Session("PreviousRPage") = Request.UrlReferrer.ToString
                    End If
                Else
                    Session("PreviousRPage") = Request.UrlReferrer.ToString
                End If

                If Session("applicantsearch") Is Nothing Then
                    Session("applicantsearch") = ""
                End If

                If Session("applicantindex") Is Nothing Then
                    Session("applicantindex") = "0"
                End If

                Session("JobID") = CInt(Request.QueryString("Jobid")).ToString.PadLeft(7 - CInt(Request.QueryString("Jobid")).ToString.Length, "0")
                LoadData(Session("JobID"))
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Protected Sub btnFind_Click(sender As Object, e As EventArgs)  'Handles btnFind.Click
        Try
            Session("applicantsearch") = search.Value.Trim
           LoadData(Session("JobID"))

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


    Protected Sub btnClose_Click(sender As Object, e As EventArgs)
        Try
            If Session("FromApplicantView") = "True" Then
                Response.Redirect("~/Module/Recruitment/JobPostings.aspx")
            Else
                Response.Redirect(Session("PreviousRPage"), True)
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub gridTrainers_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridTrainers.RowCreated
        Try
            Process.SortArrow(e, SortsDirection, Session("applicantsort"))
        Catch ex As Exception
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
            If Process.AuthenAction(Session("role"), AuthenCode, "Delete") = False Then
                Process.loadalert(divalert, msgalert, Process.privilegemsg, "danger")
                Exit Sub
            End If
            Dim count As Integer = 0
            Dim confirmValue As String = Request.Form("confirm_value")
            If confirmValue = "Yes" Then
                Dim atLeastOneRowDeleted As Boolean = False
                ' Iterate through the Products.Rows property
                For Each row As GridViewRow In gridTrainers.Rows
                    ' Access the CheckBox
                    Dim cb As CheckBox = row.FindControl("chkEmp")
                    If cb IsNot Nothing AndAlso cb.Checked Then
                        count = count + 1
                        ' Delete row! (Well, not really...)
                        atLeastOneRowDeleted = True
                        ' First, get the ProductID for the selected row
                        Dim ID As String = Convert.ToString(gridTrainers.DataKeys(row.RowIndex).Value)
                        ' "Delete" the row
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Applications_delete", ID)
                    End If
                Next
                Process.loadalert(divalert, msgalert, count.ToString & " records successfully deleted", "success")
                LoadData(Session("JobID"))
            
            End If
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
    Protected Sub SortRecords(ByVal sender As Object, ByVal e As GridViewSortEventArgs) Handles gridTrainers.Sorting
        Try
            Dim sortExpression As String = e.SortExpression
            Session("applicantsort") = sortExpression
            Dim direction As String = String.Empty
            If SortsDirection = SortDirection.Ascending Then
                SortsDirection = SortDirection.Descending
                direction = " DESC"
            Else
                SortsDirection = SortDirection.Ascending
                direction = " ASC"
            End If
            Dim table As New DataTable

            table = Process.SearchDataP2("Recruit_Applications_Get", CInt(Request.QueryString("Jobid")), Request.QueryString("Jobid"))
            table.DefaultView.Sort = sortExpression & direction
            gridTrainers.PageIndex = CInt(Session("applicantindex"))
            gridTrainers.DataSource = table
            gridTrainers.DataBind()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub gridTrainers_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gridTrainers.PageIndexChanging
        Try
            gridTrainers.PageIndex = e.NewPageIndex
            Session("applicantindex") = e.NewPageIndex
            LoadData(Request.QueryString("Jobid"))
        
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnSystem_Click(sender As Object, e As EventArgs) Handles btnshort.Click
        Try
            Dim lblStatus As String = ""
            Dim loops As Integer = 0
            Dim confirmValue As String = Request.Form("save_value")
            If confirmValue = "Yes" Then
                ' Iterate through the Products.Rows property
                For Each row As GridViewRow In gridTrainers.Rows
                    ' Access the CheckBox
                    Dim cb As CheckBox = row.FindControl("chkEmp")
                    If cb IsNot Nothing AndAlso cb.Checked Then
                        loops = loops + 1
                        Dim ID As String = Convert.ToString(gridTrainers.DataKeys(row.RowIndex).Value)
                        Dim q1 = "select a.EmailAddress from Recruit_Applicants a inner join Recruit_Applications p on a.id = p.applicantid where p.id = " + ID + ""
                        Dim emailAddress As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, q1)
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Applications_Update_State", emailAddress, CInt(Request.QueryString("Jobid")), "ShortListed", "Yes")
                        'SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Applications_Update_State", aemailaddr.Value, Session("JobID"), "ShortListed", "Yes")
                    End If
                Next
                If loops = 0 Then
                    lblStatus = "No matched candidate(s) is checked for shortlisting"
                    Process.loadalert(divalert, msgalert, lblStatus, "warning")
                Else
                    lblStatus = loops & " candidate(s) have been added to shortlist"
                    Process.loadalert(divalert, msgalert, lblStatus, "success")
                End If
            Else
                lblStatus = "Shortlisting Candidates for position is cancelled"
                Process.loadalert(divalert, msgalert, lblStatus, "warning")
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    'Protected Sub btnSystem_Click(sender As Object, e As EventArgs) Handles btnSystem.Click
    '    Try
    '        Response.Redirect("~/Module/Recruitment/MatchCandidates.aspx", True)
    '    Catch ex As Exception

    '    End Try
    'End Sub
End Class