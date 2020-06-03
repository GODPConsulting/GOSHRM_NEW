Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Telerik.Web.UI


Public Class JobInterviewUpdate
    Inherits System.Web.UI.Page
    'Dim education As New clsEducation
    Dim AuthenCode As String = "JOBINTERVIEW"
    Dim olddata(4) As String
    Dim Level(2) As String
    Dim Separators() As Char = {":"c}
    Dim EmpID_1 As String = ""
    Dim track As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'Process.LoadRadCombo(radEducationLevel, "Education_Level_Get_all", 0)

            If Not Me.IsPostBack Then
                'txtCandidates.Visible = True
                Process.LoadRadComboTextAndValueP1(cboInterviewers, "Emp_PersonalDetail_Get_Employees", Session("Access"), "name", "EmpID", False)
                'acompany.Value = Session("company")
                acompany.Value = Session("interviewcompany")

                'Company_Structure_get_parent
                If Request.QueryString("id") IsNot Nothing Then
                    ViewState("PreviousPage") = Request.UrlReferrer
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Job_Interview_Get", Request.QueryString("id"))


                    Process.LoadRadComboTextAndValueInitiateP2(cbojobpost, "Recruit_Job_Post_Get_All", "", acompany.Value, "--Select--", "Job Title", "Code")
                    Process.AssignRadComboValue(cbojobpost, strUser.Tables(0).Rows(0).Item("Title").ToString)
                    cbojobpost.Enabled = False

                    Process.LoadRadComboTextAndValueP2(cboCandidates, "Recruit_Applications_Shortlist_Get", cbojobpost.SelectedValue, "Yes", "Applicant", "email", False)
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString

                    Process.LoadTimeToRadCombo(radHourStart0, radMinStart0, radTimeStart0, strUser.Tables(0).Rows(0).Item("interviewtime"))
                    dateInterview.SelectedDate = strUser.Tables(0).Rows(0).Item("interviewdate").ToString
                    Process.LoadListAndComboxFromDataset(lstInterviewers, cboInterviewers, "Recruit_Job_Interview_Interviewer_get", "Employee", "interviewer", txtid.Text)
                    Process.LoadListAndComboxFromDataset(lstCandidates, cboCandidates, "Recruit_Job_Interview_Interviewee_get", "Name", "interviewee", txtid.Text)
                    avenue.Value = strUser.Tables(0).Rows(0).Item("interviewplace").ToString

                    If IsDate(strUser.Tables(0).Rows(0).Item("datecreated")) = True Then
                        createdon.InnerText = "Created on " & CDate(strUser.Tables(0).Rows(0).Item("datecreated")).ToLongDateString
                    End If
                    If IsDate(strUser.Tables(0).Rows(0).Item("datemodified")) = True Then
                        updatedon.InnerText = "Last modified on " & CDate(strUser.Tables(0).Rows(0).Item("datemodified")).ToLongDateString
                    End If



                    albli.Value = strUser.Tables(0).Rows(0).Item("InterviewerInviteSent").ToString
                    alblc.Value = strUser.Tables(0).Rows(0).Item("CandidateInviteSent").ToString
               
                Else
                    txtid.Text = "0"
                    Process.LoadRadComboTextAndValueInitiateP2(cbojobpost, "Recruit_Job_Post_Get_All", "", acompany.Value, "--Select--", "Job Title", "Code")
                    divcandidate.Visible = False
                    divinterviewer.Visible = False

                    btnotify1.Visible = False
                    btnotify2.Visible = False
                End If

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Function GetIdentity(ByVal jobid As Integer, ByVal interviewdate As Date, ByVal interviewtime As String, ByVal interviewplace As String, userid As String) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Recruit_Job_Interview_Update"
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@jobid", SqlDbType.Int).Value = jobid
            cmd.Parameters.Add("@interviewdate", SqlDbType.Date).Value = interviewdate
            cmd.Parameters.Add("@interviewtime", SqlDbType.VarChar).Value = interviewtime
            cmd.Parameters.Add("@interviewplace", SqlDbType.VarChar).Value = interviewplace
            cmd.Parameters.Add("@userid", SqlDbType.VarChar).Value = userid
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception
            Return 0
        End Try
    End Function
    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            System.Threading.Thread.Sleep(300)
            If txtid.Text <> "0" Then
                If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False Then
                    Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "warning")
                    Exit Sub
                End If
            End If
            If txtid.Text.Trim = "" Then
                txtid.Text = "0"
            End If
            Dim ID As Integer = 0

            Dim lblstatus As String = ""
            If cbojobpost.SelectedValue Is Nothing Then
                lblstatus = "Job Post required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                Exit Sub
                cbojobpost.Focus()
            End If

            If txtid.Text <> "0" Then
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Job_Interview_Update", txtid.Text, CInt(cbojobpost.SelectedValue), dateInterview.SelectedDate, Process.ConvertCtrlToTime(radHourStart0.SelectedValue, radMinStart0.SelectedValue, radTimeStart0.SelectedValue), avenue.Value, Session("LoginID"))
            Else
                txtid.Text = GetIdentity(CInt(cbojobpost.SelectedValue), dateInterview.SelectedDate, Process.ConvertCtrlToTime(radHourStart0.SelectedValue, radMinStart0.SelectedValue, radTimeStart0.SelectedValue), avenue.Value, Session("LoginID"))
            End If

            'Update Interviewers
            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Job_Interview_Interviewer_Clear", txtid.Text, "Y")
            For i As Integer = 0 To lstInterviewers.Items.Count - 1
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Job_Interview_Interviewer_Update", txtid.Text, lstInterviewers.Items(i).Value)
            Next
            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Job_Interview_Interviewer_Clear_Delete", txtid.Text, "Y")

            'Update Interviewees
            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Job_Interview_Interviewee_Clear", txtid.Text, "Y")
            Dim collection As IList(Of RadComboBoxItem) = cboCandidates.CheckedItems
            If (collection.Count <> 0) Then
                For Each item As RadComboBoxItem In collection
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Job_Interview_Interviewee_Update", txtid.Text, item.Text, item.Value)
                Next
            End If
            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Job_Interview_Interviewee_Clear_Delete", txtid.Text, "Y")
            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Interview_Comment_Update", txtid.Text)

            lblstatus = "Record saved"
            Process.loadalert(divalert, msgalert, lblstatus, "success")
            btnotify1.Visible = True
            btnotify2.Visible = True
            cbojobpost.Enabled = False

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try

            'If ViewState("PreviousPage") IsNot Nothing Then
            '    Response.Redirect(ViewState("PreviousPage").ToString())
            'End If

            Response.Redirect("~/Module/Recruitment/JobInterviews", True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


    Protected Sub cbojobpost_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cbojobpost.SelectedIndexChanged
        Try
            Process.LoadRadComboTextAndValueP2(cboCandidates, "Recruit_Applications_Shortlist_Get", cbojobpost.SelectedValue, "Yes", "Applicant", "email")
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub btnNotifyInterviewers_Click(sender As Object, e As EventArgs)
        Try
            Dim email As String = ""
            Dim staffname As String = ""
            Dim lblstatus As String = ""
            System.Threading.Thread.Sleep(300)
            txtCandidates.Text = ""
            txtCandidates.Visible = False
            If lstInterviewers.Items.Count > 0 Then
                For i As Integer = 0 To lstInterviewers.Items.Count - 1
                    EmpID_1 = lstInterviewers.Items(i).Value
                    email = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select email from dbo.Employees_All where empid ='" & EmpID_1 & "'")
                    staffname = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select name from dbo.Employees_All where empid ='" & EmpID_1 & "'")

                    If Process.Recruit_Interviewer_Invite(acompany.Value, email, cbojobpost.SelectedItem.Text, staffname, dateInterview.SelectedDate, Process.AMPM_Time(Process.ConvertCtrlToTime(radHourStart0.SelectedValue, radMinStart0.SelectedValue, radTimeStart0.SelectedValue)), avenue.Value, EmpID_1, Process.ApplicationURL + "/" + Process.GetMailLink(AuthenCode, 1)) = False Then
                        Process.loadalert(divalert, msgalert, Session("exception"), "danger")
                    Else
                        lblstatus = "Interviewers successfully emailed"
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Job_Interview_Mail_Sent", txtid.Text, "i")
                        divinterviewer.Visible = True
                        albli.Value = "Yes"
                        ainterviewerdate.Value = DateTime.Now.ToLongDateString
                        Process.loadalert(divalert, msgalert, lblstatus, "success")
                    End If
                Next
                lblstatus = "Interviewers successfully emailed"
                Process.loadalert(divalert, msgalert, lblstatus, "success")
            Else
                lblstatus = "No interviewer is available"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
            End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub



    Protected Sub btnNotifyCandiddate_Click(sender As Object, e As EventArgs)
        Try
            System.Threading.Thread.Sleep(300)
            txtCandidates.Visible = True
            Dim email As String = ""
            Dim staffname As String = ""
            Dim messgage As New StringBuilder
            txtCandidates.Text = ""
            Dim lblstatus As String = ""
            If lstCandidates.Items.Count > 0 Then
                Dim collection As IList(Of RadComboBoxItem) = cboCandidates.CheckedItems
                If (collection.Count <> 0) Then
                    For Each item As RadComboBoxItem In collection
                        If Process.Recruit_Interviewee_Invite(item.Value, acompany.Value, cbojobpost.SelectedItem.Text, item.Text, dateInterview.SelectedDate, Process.AMPM_Time(Process.ConvertCtrlToTime(radHourStart0.SelectedValue, radMinStart0.SelectedValue, radTimeStart0.SelectedValue)), avenue.Value) = False Then
                            Process.loadalert(divalert, msgalert, Session("exception"), "danger")
                            messgage.AppendLine(item.Value & ": mail failed, " & Process.strExp)
                        Else
                            messgage.AppendLine(item.Text & " (" & item.Value & ")" & ": mail sent")
                            lblstatus = "Candidates successfully emailed"
                            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Job_Interview_Mail_Sent", txtid.Text, "c")
                            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Applications_Update_Stat", 0, item.Value, cbojobpost.SelectedValue, "Interview")
                            divcandidate.Visible = True
                            alblc.Value = "Yes"
                            acandidatedate.Value = DateTime.Now.ToLongDateString
                            Process.loadalert(divalert, msgalert, lblstatus, "success")
                        End If

                    Next
                End If
            Else
                lblstatus = "No Candidates is available"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
            End If
            txtCandidates.Text = messgage.ToString
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    

    Private Sub cboCandidates_CheckAllCheck(sender As Object, e As Telerik.Web.UI.RadComboBoxCheckAllCheckEventArgs) Handles cboCandidates.CheckAllCheck
        Process.LoadListBoxFromCombo(lstCandidates, cboCandidates)
    End Sub

    Protected Sub cboCandidates_ItemChecked(sender As Object, e As Telerik.Web.UI.RadComboBoxItemEventArgs) Handles cboCandidates.ItemChecked
        Process.LoadListBoxFromCombo(lstCandidates, cboCandidates)
    End Sub

  

    Private Sub cboInterviewers_ItemChecked(sender As Object, e As Telerik.Web.UI.RadComboBoxItemEventArgs) Handles cboInterviewers.ItemChecked
        Process.LoadListBoxFromCombo(lstInterviewers, cboInterviewers)
    End Sub

End Class