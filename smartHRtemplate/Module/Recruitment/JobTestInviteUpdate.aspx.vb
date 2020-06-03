Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Telerik.Web.UI


Public Class JobTestInviteUpdate
    Inherits System.Web.UI.Page
    'Dim education As New clsEducation
    Dim AuthenCode As String = "JOBINTERVIEW"
    Dim olddata(4) As String
    Dim Level(2) As String
    Dim Separators() As Char = {":"c}
    Dim EmpID_1 As String = ""
    Dim stageno As Integer = 0
    Dim lblstatus As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'Process.LoadRadCombo(radEducationLevel, "Education_Level_Get_all", 0)

            If Not Me.IsPostBack Then
                lblJobTest.Value = Session("JobTest")
                lbljobid.Text = CInt(Session("JobID"))


                Dim strTest As New DataSet
                strTest = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Job_Test_Get", Session("JobTestID"))
                If strTest.Tables(0).Rows.Count > 0 Then
                    lblJobPost.Text = strTest.Tables(0).Rows(0).Item("title").ToString
                    lblJobTest.Value = strTest.Tables(0).Rows(0).Item("TestTitle").ToString
                    stageno = strTest.Tables(0).Rows(0).Item("stageno").ToString
                    lbltestid.Text = strTest.Tables(0).Rows(0).Item("id").ToString
                    lblcompany.Text = strTest.Tables(0).Rows(0).Item("company").ToString
                    pagetitle.InnerText = lblcompany.Text & ": " & lblJobPost.Text
                End If

                If Request.QueryString("id") IsNot Nothing Then
                    ViewState("PreviousPage") = Request.UrlReferrer
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Job_Test_Invite_Get", Request.QueryString("id"))
                    lblid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    lbltestid.Text = strUser.Tables(0).Rows(0).Item("JobTestid").ToString
                    lblJobPost.Text = strUser.Tables(0).Rows(0).Item("jobtitle").ToString
                    lblJobTest.Value = strUser.Tables(0).Rows(0).Item("TestTitle").ToString
                    Process.LoadTimeToRadCombo(radHourStart0, radMinStart0, radTimeStart0, strUser.Tables(0).Rows(0).Item("testtime"))
                    dateInterview.SelectedDate = strUser.Tables(0).Rows(0).Item("testdate").ToString
                    Process.LoadRadComboTextAndValueP2(cboCandidates, "Recruit_Job_Test_Shortlist_Get_All", CInt(Session("JobID")), stageno, "applicant", "applicationid", False)
                    Process.LoadListAndComboxFromDataset(lstCandidates, cboCandidates, "Recruit_Job_Test_Paper_Result_Get_All", "Applicant", "ApplicationID", lblid.Text)
                    txtVenue.Value = strUser.Tables(0).Rows(0).Item("interviewplace").ToString
                    lblCreateBy.Value = strUser.Tables(0).Rows(0).Item("createdby").ToString
                    lblCreateOn.Value = strUser.Tables(0).Rows(0).Item("datecreated").ToString
                    lblUpdatedBy.Value = strUser.Tables(0).Rows(0).Item("updatedby").ToString
                    lblUpdatedOn.Value = strUser.Tables(0).Rows(0).Item("datemodified").ToString
                    pagetitle.InnerText = lblJobPost.Text

                    lblc.Text = strUser.Tables(0).Rows(0).Item("CandidateInviteSent").ToString
                    If lblc.Text.ToLower = "yes" Then
                        lblcandidatedate.Visible = True
                        lblcandidatedate.Text = strUser.Tables(0).Rows(0).Item("CandInviteSentDate").ToString
                    Else
                        lblcandidatedate.Visible = False
                    End If

                Else
                    lblc.Visible = False
                    lblcandidatedate.Visible = False
                    Label12.Visible = False
                    lblid.Text = "0"
                    Process.DisableButton(btnInvite)
                    Process.LoadRadComboTextAndValueP2(cboCandidates, "Recruit_Job_Test_Shortlist_New", CInt(Session("JobID")), stageno, "applicant", "applicationid", False)
                End If

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Function GetIdentity(ByVal jobtestid As Integer, ByVal interviewdate As Date, ByVal interviewtime As String, ByVal interviewplace As String, userid As String) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Recruit_Job_Test_Invite_Update"
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@jobtestid", SqlDbType.Int).Value = jobtestid
            cmd.Parameters.Add("@testdate", SqlDbType.Date).Value = interviewdate
            cmd.Parameters.Add("@testtime", SqlDbType.VarChar).Value = interviewtime
            cmd.Parameters.Add("@testplace", SqlDbType.VarChar).Value = interviewplace
            cmd.Parameters.Add("@createdby", SqlDbType.VarChar).Value = userid
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception
            Process.strExp = ex.Message
            Return 0
        End Try
    End Function
    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try

            If lblid.Text.Trim = "" Then
                lblid.Text = "0"
            End If
            Dim ID As Integer = 0

            If lblid.Text <> "0" Then
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Job_Test_Invite_Update", lblid.Text, lbltestid.Text, dateInterview.SelectedDate, Process.ConvertCtrlToTime(radHourStart0.SelectedValue, radMinStart0.SelectedValue, radTimeStart0.SelectedValue), txtVenue.Value, Session("LoginID"))
            Else
                lblid.Text = GetIdentity(lbltestid.Text, dateInterview.SelectedDate, Process.ConvertCtrlToTime(radHourStart0.SelectedValue, radMinStart0.SelectedValue, radTimeStart0.SelectedValue), txtVenue.Value, Session("LoginID"))
                If lblid.Text = "0" Then
                    lblstatus = Process.strExp
                    Process.loadalert(divalert, msgalert, lblstatus, "danger")
                    Exit Sub
                End If
            End If

            'Update Interviewees
            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Job_Test_Paper_Result_Clear", lblid.Text, "Y")
            Dim collection As IList(Of RadComboBoxItem) = cboCandidates.CheckedItems
            If (collection.Count <> 0) Then
                For Each item As RadComboBoxItem In collection
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Job_Test_Paper_Result_Applicants_Update", lbltestid.Text, lblid.Text, item.Value)
                Next
            End If
            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Job_Test_Paper_Result_Clear_Delete", lbltestid.Text, "Y")

            Process.EnableButton(btnInvite)
            lblstatus = "Record saved"
            Process.loadalert(divalert, msgalert, lblstatus, "success")
            Response.Redirect(Session("PreviousPage").ToString)
        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "danger")
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect(Session("PreviousPage").ToString)
        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "danger")
        End Try
    End Sub



    Private Sub cboCandidates_CheckAllCheck(sender As Object, e As Telerik.Web.UI.RadComboBoxCheckAllCheckEventArgs) Handles cboCandidates.CheckAllCheck
        Try
            Process.LoadListBoxFromCombo(lstCandidates, cboCandidates)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub cboCandidates_ItemChecked(sender As Object, e As Telerik.Web.UI.RadComboBoxItemEventArgs) Handles cboCandidates.ItemChecked
        Try
            Process.LoadListBoxFromCombo(lstCandidates, cboCandidates)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles btnInvite.Click
        Try
            Dim email As String = ""
            Dim staffname As String = ""
            Dim messgage As New StringBuilder

            If lstCandidates.Items.Count > 0 Then
                Dim collection As IList(Of RadComboBoxItem) = cboCandidates.CheckedItems
                If (collection.Count <> 0) Then
                    For Each item As RadComboBoxItem In collection
                        email = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select a.EmailAddress  from Recruit_Applicants a inner join Recruit_Applications b on a.id = b.applicantid where b.id=" & item.Value)
                        If Process.Recruit_Test_Invite(email, lblcompany.Text, lblJobPost.Text, item.Text, dateInterview.SelectedDate, Process.AMPM_Time(Process.ConvertCtrlToTime(radHourStart0.SelectedValue, radMinStart0.SelectedValue, radTimeStart0.SelectedValue)), txtVenue.Value) = False Then
                            lblstatus = Process.strExp
                            Process.loadalert(divalert, msgalert, lblstatus, "danger")
                            messgage.AppendLine(email & ": mail failed, " & Process.strExp)
                        Else
                            messgage.AppendLine(email & ": mail sent")
                            lblstatus = "Candidates successfully emailed"
                            Process.loadalert(divalert, msgalert, lblstatus, "success")
                            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Applications_Update_Stat", 0, email, lbljobid.Text, "Aptitude Test")
                            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Job_Test_Invite_Mail_Sent", lblid.Text)
                            lblc.Visible = True
                            lblcandidatedate.Visible = True
                            Label12.Visible = True
                            lblc.Text = "Yes"
                            lblcandidatedate.Text = DateTime.Now.ToString

                        End If

                    Next
                End If
                lblstatus = "Candidates successfully emailed"
                Process.loadalert(divalert, msgalert, lblstatus, "success")
            Else
                lblstatus = "No Candidates is available"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
            End If
            Process.loadalert(divalert, msgalert, "Candidates successfully emailed", "success")

        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "danger")
        End Try
    End Sub

    Protected Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Process.LoadListBoxFromCombo(lstCandidates, cboCandidates)
    End Sub

 
End Class