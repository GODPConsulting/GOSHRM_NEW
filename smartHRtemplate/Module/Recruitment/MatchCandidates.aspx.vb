Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports Telerik.Web.UI
Public Class MatchCandidates
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "JOBPOST"
    Private Function LoadDataTable() As DataTable
        Dim Datas As New DataTable
        Dim job As String = ""
        job = radJobPosts.SelectedValue
        Datas = Process.SearchDataP6("recruit_Applicant_System_Shortlisting", job, cboGender.SelectedItem.Text, cboEducation.SelectedItem.Text, cboAgeCriteria.SelectedItem.Text, cboExperience.SelectedItem.Text, "")
        lblHeader2.Text = cboCompany.SelectedValue & ":- " & radJobPosts.SelectedItem.Text & "(" & FormatNumber(Datas.Rows.Count, 0) & ")"
        Return Datas
    End Function


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            btnSave.Visible = False
            'lblStatus.Text = "Disable"
            For Each row As GridViewRow In gridTrainers.Rows
                ' Access the CheckBox
                Dim cb As CheckBox = row.FindControl("chkEmp")
                If cb IsNot Nothing AndAlso cb.Checked Then
                    btnSave.Visible = True
                    'lblStatus.Text = "Enabled"
                    Exit For
                End If
            Next

            

            If Not Me.IsPostBack Then
                ViewState("PreviousPage") = Request.UrlReferrer
                Process.LoadRadComboTextAndValueP2(cboCompany, "Company_Structure_Get_ByLevel", "2", Session("Access"), "name", "name", False)
                If Session("company") Is Nothing Then
                    Session("company") = Session("organisation")
                End If
                Process.AssignRadComboValue(cboCompany, Session("company"))

                cboGender.Items.Clear()
                cboGender.Items.Add("Any Gender")
                cboGender.Items.Add("Female")
                cboGender.Items.Add("Male")

                Process.LoadRadComboTextAndValueInitiate(cboNationality, "Nationalities_get_all", "Any Nationality", "name", "name")

                cboAgeCriteria.Items.Clear()
                cboAgeCriteria.Items.Add("No")
                cboAgeCriteria.Items.Add("Yes")

                cboEducation.Items.Clear()
                cboEducation.Items.Add("No")
                cboEducation.Items.Add("Yes")

                cboExperience.Items.Clear()
                cboExperience.Items.Add("No")
                cboExperience.Items.Add("Yes")

                chkOnline.Checked = True
                Process.LoadRadComboTextAndValueP2(radJobPosts, "Recruit_Job_Post_Get_Active", chkOnline.Checked, cboCompany.SelectedValue, "Postings", "Code")

            End If

            If ViewState("PreviousPage") Is Nothing Or ViewState("PreviousPage").ToString.Contains("MatchCandidates") Then
                btnDelete.Visible = False
            Else
                btnDelete.Visible = True
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
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
            Dim table As DataTable = LoadDataTable() 'Process.GetData("Recruitment_Job_Post_get_all")
            table.DefaultView.Sort = sortExpression & direction
            'GridVwHeaderChckbox.DataSource = table
            'GridVwHeaderChckbox.DataBind()
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

    Protected Sub GridVwHeaderChckbox_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gridTrainers.PageIndexChanging
        Try
            gridTrainers.PageIndex = e.NewPageIndex
            gridTrainers.DataSource = LoadDataTable()
            gridTrainers.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub OnRowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gridTrainers.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gridTrainers, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click checkbox of appropriate candidates for shortlisting"
        End If
    End Sub


    Protected Sub btnFind1_Click(sender As Object, e As EventArgs)

    End Sub
    Protected Sub radStatus_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles radJobPosts.SelectedIndexChanged
        Try
            'LoadGrid(Session("LoadType"))
            Dim strUser As New DataSet
            strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Job_Post_get", radJobPosts.SelectedValue)
            If strUser.Tables(0).Rows.Count > 0 Then
                lblAge1.Text = strUser.Tables(0).Rows(0).Item("StartAgeRange").ToString
                lblAge2.Text = strUser.Tables(0).Rows(0).Item("EndAgeRange").ToString
                lblExperience1.Text = strUser.Tables(0).Rows(0).Item("experience1").ToString
                lblExperience2.Text = strUser.Tables(0).Rows(0).Item("experience2").ToString
                lblEducation1.Text = strUser.Tables(0).Rows(0).Item("educationlevel").ToString
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    Protected Sub cboAgeCriteria_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboAgeCriteria.SelectedIndexChanged
        Try
            If cboAgeCriteria.SelectedItem.Text = "Yes" Then
                lblAge.Text = "Between " & lblAge1.Text & " and " & lblAge2.Text
            Else
                lblAge.Text = "Don't apply age criteria of job post"
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub cboEducation_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboEducation.SelectedIndexChanged
        Try
            If cboEducation.SelectedItem.Text = "Yes" Then
                lblEducation.Text = lblEducation1.Text
            Else
                lblEducation.Text = "Don't apply Education level criteria of job post"
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub cboExperience_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboExperience.SelectedIndexChanged
        Try
            If cboExperience.SelectedItem.Text = "Yes" Then
                lblExperience.Text = "Between " & lblExperience1.Text & " and " & lblExperience2.Text
            Else
                lblExperience.Text = "Don't apply Years of Experience criteria of job post"
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnSend_Click(sender As Object, e As EventArgs) Handles btnSend.Click
        Try
            lblJobID.Text = radJobPosts.SelectedValue
            Session("JobID") = radJobPosts.SelectedValue
            gridTrainers.DataSource = LoadDataTable()
            gridTrainers.AllowSorting = True
            gridTrainers.AllowPaging = True
            gridTrainers.DataBind()

            Response.Write("Candidates successfully shortlisted for " & radJobPosts.SelectedItem.Text)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Candidates successfully shortlisted for " & radJobPosts.SelectedItem.Text + "')", True)
            btnSave.Visible = False
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            'Response.Write("<script language='javascript'> { self.close() }</script>")
            If ViewState("PreviousPage") IsNot Nothing Then
                Response.Redirect(ViewState("PreviousPage").ToString())
            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btnSend0_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            'If Process.AuthenAction(Session("role"), AuthenCode, "Delete") = False Then
            '    ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
            '    Exit Sub
            'End If
            Dim loops As Integer = 0
            Dim confirmValue As String = Request.Form("save_value")
            If confirmValue = "Yes" Then
                ' Iterate through the Products.Rows property
                For Each row As GridViewRow In gridTrainers.Rows
                    ' Access the CheckBox
                    Dim cb As CheckBox = row.FindControl("chkEmp")
                    If cb IsNot Nothing AndAlso cb.Checked Then
                        loops = loops + 1
                        ' First, get the ProductID for the selected row
                        Dim ID As String = _
                            Convert.ToString(gridTrainers.DataKeys(row.RowIndex).Value)
                        ' "Delete" the row
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Applications_Update_State", ID, lblJobID.Text, "ShortListed", "Yes")
                    End If
                Next
                If loops = 0 Then
                    Response.Write("No matched candidate(s) is checked for shortlisting")
                    ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "No matched candidate(s) is checked for shortlisting" + "')", True)
                End If
            Else
                Response.Write("Parsing Shortlisted Candidates for Interview cancelled")
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Parsing Shortlisted Candidates for Interview cancelled" + "')", True)
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    Protected Sub chkOnline_CheckedChanged(sender As Object, e As EventArgs) Handles chkOnline.CheckedChanged
        Try
            Process.LoadRadComboTextAndValueP2(radJobPosts, "Recruit_Job_Post_Get_Active", chkOnline.Checked, Session("company"), "Postings", "Code", False)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub cboCompany_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboCompany.SelectedIndexChanged
        Try
            Session("company") = cboCompany.SelectedValue
            Process.LoadRadComboTextAndValueP2(radJobPosts, "Recruit_Job_Post_Get_Active", chkOnline.Checked, Session("company"), "Postings", "Code", False)
        Catch ex As Exception

        End Try
    End Sub
End Class