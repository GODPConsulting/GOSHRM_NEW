Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports System.IO


Public Class InterviewEvaluationForm
    Inherits System.Web.UI.Page
    Dim education As New clsEducation
    Dim Separators() As Char = {","c}
    Dim section As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not Me.IsPostBack Then
                'Session("PreviousPage") = Request.UrlReferrer.ToString

                Dim ratings As String = ""
                Dim strPoints As New DataSet
                strPoints = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Points_Get_All")
                If strPoints.Tables(0).Rows.Count > 0 Then
                    For i = 0 To strPoints.Tables(0).Rows.Count - 1
                        ratings = strPoints.Tables(0).Rows(i).Item("point").ToString & " - " & strPoints.Tables(0).Rows(i).Item("pointname").ToString & " " & ratings.Trim
                    Next
                End If
                lbrating.InnerText = ratings

                Process.LoadRadioButtonsDb(rdoadmin, "Performance_Points_Get_All", "point", "point", "pointdescription")
                Process.LoadRadioButtonsDb(rdocommunication, "Performance_Points_Get_All", "point", "point", "pointdescription")
                Process.LoadRadioButtonsDb(rdocustomer, "Performance_Points_Get_All", "point", "point", "pointdescription")
                Process.LoadRadioButtonsDb(rdoeducation, "Performance_Points_Get_All", "point", "point", "pointdescription")
                Process.LoadRadioButtonsDb(rdoenthusiam, "Performance_Points_Get_All", "point", "point", "pointdescription")
                Process.LoadRadioButtonsDb(rdoimpression, "Performance_Points_Get_All", "point", "point", "pointdescription")
                Process.LoadRadioButtonsDb(rdoleadership, "Performance_Points_Get_All", "point", "point", "pointdescription")
                Process.LoadRadioButtonsDb(rdotechnical, "Performance_Points_Get_All", "point", "point", "pointdescription")
                Process.LoadRadioButtonsDb(rdoworkexp, "Performance_Points_Get_All", "point", "point", "pointdescription")

                ainterviewer.Value = Session("Interviewer")
                acandidate.Value = Session("ApplcantName")
                ainterviewdate.Value = Date.Now
                lblid.Text = "0"
                lblIntid.Text = Request.QueryString("id")
                lblApplicantID.Text = Session("ApplcantID")
                lblInterviewerID.Text = Session("InterviewerID")


                Dim strEval As New DataSet
                strEval = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Candidate_Evaluation_Get", Request.QueryString("id"))
                If strEval.Tables(0).Rows.Count > 0 Then
                    lblid.Text = strPoints.Tables(0).Rows(0).Item("id").ToString
                    lblIntid.Text = Request.QueryString("id")
                    lbrating.InnerText = strEval.Tables(0).Rows(0).Item("ratings").ToString

                    ainterviewdate.Value = strEval.Tables(0).Rows(0).Item("InterviewDate").ToString
                    Process.RadioListCheck(rdoadmin, strEval.Tables(0).Rows(0).Item("adminratng").ToString)
                    aadmin.Value = strEval.Tables(0).Rows(0).Item("admincomment").ToString


                    Process.RadioListCheck(rdocommunication, strEval.Tables(0).Rows(0).Item("communicationrating").ToString)
                    acommunication.Value = strEval.Tables(0).Rows(0).Item("communicationcomment").ToString

                    Process.RadioListCheck(rdocustomer, strEval.Tables(0).Rows(0).Item("customerrating").ToString)
                    acustomer.Value = strEval.Tables(0).Rows(0).Item("customercomment").ToString


                    Process.RadioListCheck(rdoeducation, strEval.Tables(0).Rows(0).Item("educationrating").ToString)
                    aeducation.Value = strEval.Tables(0).Rows(0).Item("educationcomment").ToString


                    Process.RadioListCheck(rdoenthusiam, strEval.Tables(0).Rows(0).Item("euthuasiasmrating").ToString)
                    aenthisiam.Value = strEval.Tables(0).Rows(0).Item("euthuasiasmcomment").ToString


                    Process.RadioListCheck(rdoimpression, strEval.Tables(0).Rows(0).Item("impressionrating").ToString)
                    aimpression.Value = strEval.Tables(0).Rows(0).Item("impressioncomment").ToString


                    Process.RadioListCheck(rdoleadership, strEval.Tables(0).Rows(0).Item("leadershiprating").ToString)
                    aleadership.Value = strEval.Tables(0).Rows(0).Item("leadershipcomment").ToString


                    Process.RadioListCheck(rdotechnical, strEval.Tables(0).Rows(0).Item("technicalrating").ToString)
                    atechnical.Value = strEval.Tables(0).Rows(0).Item("technicalcomment").ToString


                    Process.RadioListCheck(rdoworkexp, strEval.Tables(0).Rows(0).Item("workexprating").ToString)
                    aworkexperience.Value = strEval.Tables(0).Rows(0).Item("workexpcomment").ToString


                End If
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub




    Protected Sub btnMedSaveSection_Click(sender As Object, e As EventArgs)
        Try
            Dim lblstatus As String = ""
            If rdoeducation.SelectedValue.Trim = "" Then
                lblstatus = "Education background rating required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                rdoeducation.Focus()
                Exit Sub
            End If

            If rdoworkexp.SelectedValue.Trim = "" Then
                lblstatus = "Prior Work Experience rating required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                rdoworkexp.Focus()
                Exit Sub
            End If

            If rdotechnical.SelectedValue.Trim = "" Then
                lblstatus = "Technical background rating required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                rdotechnical.Focus()
                Exit Sub
            End If

            If rdoadmin.SelectedValue.Trim = "" Then
                lblstatus = "Administrative edxperience rating required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                rdoadmin.Focus()
                Exit Sub
            End If

            If rdoleadership.SelectedValue.Trim = "" Then
                lblstatus = "Leadership ability rating required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                rdoleadership.Focus()
                Exit Sub
            End If


            If rdocustomer.SelectedValue.Trim = "" Then
                lblstatus = "Customer Service rating required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                rdocustomer.Focus()
                Exit Sub
            End If

            If rdocommunication.SelectedValue.Trim = "" Then
                lblstatus = "Communication rating required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                rdocommunication.Focus()
                Exit Sub
            End If

            If rdoenthusiam.SelectedValue.Trim = "" Then
                lblstatus = "Candidate Enthusiasm rating required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                rdoenthusiam.Focus()
                Exit Sub
            End If

            If rdoimpression.SelectedValue.Trim = "" Then
                lblstatus = "Overall impression rating required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                rdoimpression.Focus()
                Exit Sub
            End If

            If lblid.Text = "0" Then
                lblid.Text = GetIdentity(lblApplicantID.Text, lblInterviewerID.Text, ainterviewdate.Value, lbrating.InnerText, _
                                                      rdoeducation.SelectedItem.Value, aeducation.Value, rdoworkexp.SelectedItem.Value, aworkexperience.Value, rdotechnical.SelectedItem.Value, atechnical.Value, rdoadmin.SelectedItem.Value, aadmin.Value, _
                                                      rdoleadership.SelectedItem.Value, aleadership.Value, rdocustomer.SelectedItem.Value, acustomer.Value, rdocommunication.SelectedItem.Value, acommunication.Value, rdoenthusiam.SelectedItem.Value, _
                                                      aenthisiam.Value, rdoimpression.SelectedItem.Value, aimpression.Value, lblIntid.Text)
                If lblid.Text = "0" Then                    
                    Exit Sub
                End If
            Else
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Candidate_Evaluation_Update", lblid.Text, lblApplicantID.Text, lblInterviewerID.Text, ainterviewdate.Value, lbrating.InnerText, _
                                                      rdoeducation.SelectedItem.Value, aeducation.Value, rdoworkexp.SelectedItem.Value, aworkexperience.Value, rdotechnical.SelectedItem.Value, atechnical.Value, rdoadmin.SelectedItem.Value, aadmin.Value, _
                                                      rdoleadership.SelectedItem.Value, aleadership.Value, rdocustomer.SelectedItem.Value, acustomer.Value, rdocommunication.SelectedItem.Value, acommunication.Value, rdoenthusiam.SelectedItem.Value, _
                                                      aenthisiam.Value, rdoimpression.SelectedItem.Value, aimpression.Value, lblIntid.Text)
            End If
            lblstatus = "Interview Evaluation saved!"
            Process.loadalert(divalert, msgalert, lblstatus, "success")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Function GetIdentity(ByVal ApplicantID As String, ByVal Interviewer As String, ByVal InterviewDate As Date, _
                                 ByVal ratings As String, ByVal educationrating As String, ByVal educationcomment As String, ByVal workexprating As String, _
                                  ByVal workexpcomment As String, ByVal technicalrating As String, ByVal technicalcomment As String, ByVal adminratng As String, ByVal admincomment As String, _
                                  ByVal leadershiprating As String, ByVal leadershipcomment As String, ByVal customerrating As String, ByVal customercomment As String, _
                                  ByVal communicationrating As String, ByVal communicationcomment As String, ByVal euthuasiasmrating As String, ByVal euthuasiasmcomment As String, _
                                  ByVal impressionrating As String, ByVal impressioncomment As String, ByVal interviewcommentid As Integer) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Recruit_Candidate_Evaluation_Update"
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@ApplicantID", SqlDbType.Int).Value = ApplicantID
            cmd.Parameters.Add("@Interviewer", SqlDbType.VarChar).Value = Interviewer
            cmd.Parameters.Add("@InterviewDate", SqlDbType.Date).Value = InterviewDate
            cmd.Parameters.Add("@ratings", SqlDbType.VarChar).Value = ratings
            cmd.Parameters.Add("@educationrating", SqlDbType.VarChar).Value = educationrating
            cmd.Parameters.Add("@educationcomment", SqlDbType.VarChar).Value = educationcomment
            cmd.Parameters.Add("@workexprating", SqlDbType.VarChar).Value = workexprating
            cmd.Parameters.Add("@workexpcomment", SqlDbType.VarChar).Value = workexpcomment
            cmd.Parameters.Add("@technicalrating", SqlDbType.VarChar).Value = technicalrating
            cmd.Parameters.Add("@technicalcomment", SqlDbType.VarChar).Value = technicalcomment
            cmd.Parameters.Add("@adminratng", SqlDbType.VarChar).Value = adminratng
            cmd.Parameters.Add("@admincomment", SqlDbType.VarChar).Value = admincomment
            cmd.Parameters.Add("@leadershiprating", SqlDbType.VarChar).Value = leadershiprating
            cmd.Parameters.Add("@leadershipcomment", SqlDbType.VarChar).Value = leadershipcomment
            cmd.Parameters.Add("@customerrating", SqlDbType.VarChar).Value = customerrating
            cmd.Parameters.Add("@customercomment", SqlDbType.VarChar).Value = customercomment
            cmd.Parameters.Add("@communicationrating", SqlDbType.VarChar).Value = communicationrating
            cmd.Parameters.Add("@communicationcomment", SqlDbType.VarChar).Value = communicationcomment
            cmd.Parameters.Add("@euthuasiasmrating", SqlDbType.VarChar).Value = euthuasiasmrating
            cmd.Parameters.Add("@euthuasiasmcomment", SqlDbType.VarChar).Value = euthuasiasmcomment
            cmd.Parameters.Add("@impressionrating", SqlDbType.VarChar).Value = impressionrating
            cmd.Parameters.Add("@impressioncomment", SqlDbType.VarChar).Value = impressioncomment
            cmd.Parameters.Add("@interviewcommentid", SqlDbType.Int).Value = interviewcommentid
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception            
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
            Return 0
        End Try
    End Function


    Protected Sub btnExit_Click(sender As Object, e As EventArgs)
        Try
            Response.Write("<script language='javascript'> { self.close() }</script>")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
End Class