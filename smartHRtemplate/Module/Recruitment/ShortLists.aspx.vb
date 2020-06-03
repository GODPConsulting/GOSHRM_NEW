Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports GOSHRM.GOSHRM.GOSHRM.BO

Public Class ShortLists
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

    Private Sub LoadData(ByVal ID As String, ByVal status As String)
        Try
            gridTrainers.DataSource = Process.SearchDataP2("Recruit_Applications_Shortlist_Get", ID, status)
            gridTrainers.AllowSorting = True
            gridTrainers.AllowPaging = True
            gridTrainers.DataBind()

            Dim strUser As New DataSet
            Dim Padded As String = ID.PadLeft(7 - ID.Length, "0")
            strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Job_Post_get", Padded)
            lblHeader.Text = "Shortlisted Applicants for " & strUser.Tables(0).Rows(0).Item("Title").ToString


        Catch ex As Exception
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            btnSendInvite.Enabled = False
            btnSendInvite.BackColor = Color.Red
            'lblStatus.Text = "Disable"
            For Each row As GridViewRow In gridTrainers.Rows
                ' Access the CheckBox
                Dim cb As CheckBox = row.FindControl("chkEmp")
                If cb IsNot Nothing AndAlso cb.Checked Then
                    btnSendInvite.Enabled = True
                    btnSendInvite.BackColor = Color.Purple
                    'lblStatus.Text = "Enabled"
                    '#1BA691
                    Exit For
                End If
            Next

            If Not Me.IsPostBack Then
                LoadData(CInt(Request.QueryString("Jobid")), "Yes")
                Session("ID") = CInt(Request.QueryString("Jobid")).ToString.PadLeft(7 - CInt(Request.QueryString("Jobid")).ToString.Length, "0")
            End If
        Catch ex As Exception
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub




    Protected Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Try
            Response.Write("<script language='javascript'> { self.close() }</script>")
        Catch ex As Exception
        End Try
    End Sub


    Protected Sub btnRepay0_Click(sender As Object, e As EventArgs)

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
                        ' "Delete" the row
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Applications_Shortlist_delete", CInt(Request.QueryString("Jobid")), ID)
                    End If
                Next
                LoadData(Session("LoadType"), "Yes")
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
            gridTrainers.DataSource = Process.SearchDataP2("Recruit_Applications_Shortlist_Get", CInt(Request.QueryString("Jobid")), "Yes")
            gridTrainers.DataBind()
        Catch ex As Exception
            lblStatus.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnManual_Click(sender As Object, e As EventArgs) Handles btnSendInvite.Click
        Try
            'If Process.AuthenAction(Session("role"), AuthenCode, "Delete") = False Then
            '    ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
            '    Exit Sub
            'End If
            Session("MailList") = ""
            Dim loops As Integer = 0
            Dim confirmValue As String = Request.Form("send_value")
            If confirmValue = "Yes" Then
                ' Iterate through the Products.Rows property
                For Each row As GridViewRow In gridTrainers.Rows
                    ' Access the CheckBox
                    Dim cb As CheckBox = row.FindControl("chkEmp")
                    If cb IsNot Nothing AndAlso cb.Checked Then
                        loops = loops + 1
                        Dim emailid As String = Convert.ToString(gridTrainers.DataKeys(row.RowIndex).Value)
                        If Session("MailList").ToString.Trim = "" Then
                            Session("MailList") = emailid
                        Else
                            Session("MailList") = emailid & ";" & Session("MailList")
                        End If

                        'Dim applicantname As String = gridTrainers.Rows(row.RowIndex).Cells(1).Text
                        'SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Applications_Update_State", emailid, CInt(Request.QueryString("Jobid")), "ShortListed", "Yes")
                    End If
                Next
                If loops = 0 Then
                    For Each row As GridViewRow In gridTrainers.Rows
                        Dim emailid As String = Convert.ToString(gridTrainers.DataKeys(row.RowIndex).Value)
                        If Session("MailList").ToString.Trim = "" Then
                            Session("MailList") = emailid
                        Else
                            Session("MailList") = emailid & ";" & Session("MailList")
                        End If
                        'Dim applicantname As String = gridTrainers.Rows(row.RowIndex).Cells(1).Text
                        'SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Applications_Update_State", emailid, CInt(Request.QueryString("Jobid")), "ShortListed", "Yes")
                    Next
                End If
            Else
                lblStatus.Text = "Mail notification for position " & lblHeader.Text & " cancelled"
            End If
        Catch ex As Exception
            lblStatus.Text = ex.Message
        End Try
    End Sub

    'Protected Sub btnSystem_Click(sender As Object, e As EventArgs) Handles btnSystem.Click
    '    Try
    '        Response.Redirect("~/Module/Recruitment/MatchCandidates.aspx", True)
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Protected Sub gridTrainers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gridTrainers.SelectedIndexChanged

    End Sub
End Class