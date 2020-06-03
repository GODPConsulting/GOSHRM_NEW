Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO


Public Class Test
    Inherits System.Web.UI.Page
    Dim education As New clsEducation
    Dim AuthenCode As String = "JOBTEST"
    Dim olddata(4) As String
    Dim options(4) As String
    Dim Separators() As Char = {","c}
    Private stagenumber As Integer
    Private Function GetIdentity(ByVal applicant As String, ByVal jobid As Integer, ByVal stage As Integer) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Recruit_Prepare_OnlineTest"
            cmd.Parameters.Add("@Applicant", SqlDbType.VarChar).Value = applicant
            cmd.Parameters.Add("@JobID", SqlDbType.Int).Value = jobid
            cmd.Parameters.Add("@Stage", SqlDbType.Int).Value = stage

            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception
            Return 0
        End Try
    End Function
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim strTest As New DataSet

            If Session("ApplicationTestID") Is Nothing Then
                Session("ApplicationTestID") = GetIdentity(Session("ApplcantID"), Session("ApplicantJobID"), Session("stage"))
            End If

            strTest = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Applicant_OnlineTest", Session("ApplicationTestID"), Session("stage"))
            If strTest.Tables(0).Rows.Count > 0 Then
                txtDuration.Text = strTest.Tables(0).Rows(0).Item("TestDuration").ToString
            End If

            If Not Me.IsPostBack Then

                'If Session("stage") = 1 Then
                '    Session("ApplicationTestID") = GetIdentity(Session("ApplcantID"), Session("ApplicantJobID"), Session("stage"))
                'End If

                If strTest.Tables(0).Rows.Count > 0 Then
                    txtDuration.Text = strTest.Tables(0).Rows(0).Item("TestDuration").ToString
                    txtTime.Text = "Test Duration: " & txtDuration.Text & " mins"
                    txtmin.Text = txtDuration.Text
                End If
                lblTimeStart.Text = "Test Start Time: " & System.DateTime.Now
                lblstart.Text = System.DateTime.Now
                txtTime.Text = "Test Duration: " & txtDuration.Text & " mins"
                txtmin.Text = txtDuration.Text
                txtsec.Text = "0"
                'SingleLineTextBox, // will render a textbox 
                'MultiLineTextBox, // will render a text area 
                'YesOrNo, //will render a checkbox
                'SingleSelect, //will render a dropdownlist
                'MultiSelect //will render a listbo

                btnNext.BackColor = Color.Gray
                btnPrevious.BackColor = Color.Gray
                btnFinalSubmit.BackColor = Color.Gray

                Session("strttime") = System.DateTime.Now
                btnNext.Enabled = False
                btnPrevious.Enabled = False
                rdoAnswers.Enabled = False
                chkAnswers.Enabled = False
                lblstatus.Text = ""
                ViewState("PreviousPage") = Request.UrlReferrer

                imgProfile.Width = Unit.Percentage(0)
                imgProfile.Height = Unit.Percentage(0)

            End If
            'End If
        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub
    Private Sub LoadQuestion(ByVal testID As String, ByVal questionno As String)
        Try
            Dim strTest As New DataSet

            strTest = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Applicant_OnlineTest", testID, questionno)

            Dim Param(3) As String
            If strTest.Tables(0).Rows.Count > 0 Then
                lblQuestionNo.Text = strTest.Tables(0).Rows(0).Item("Rows").ToString & "."
                txtQuestionType.Text = strTest.Tables(0).Rows(0).Item("QuestionType").ToString
                txtQuestion.Text = strTest.Tables(0).Rows(0).Item("Questions").ToString
                Param(0) = "A. " & strTest.Tables(0).Rows(0).Item("OptionA").ToString
                Param(1) = "B. " & strTest.Tables(0).Rows(0).Item("OptionB").ToString
                Param(2) = "C. " & strTest.Tables(0).Rows(0).Item("OptionC").ToString
                Param(3) = "D. " & strTest.Tables(0).Rows(0).Item("OptionD").ToString
                txtQuestionCount.Text = strTest.Tables(0).Rows(0).Item("QuestionsCount").ToString
                txtid.Text = strTest.Tables(0).Rows(0).Item("id").ToString
                txtRealAnswer.Text = strTest.Tables(0).Rows(0).Item("RealAnswer").ToString.Trim
                'imgProfile.ImageUrl = strTest.Tables(0).Rows(0).Item("images").ToString

                imgProfile.ImageUrl = "~/Module/Recruitment/OnlineTest/QuestImage.ashx?imgid=" & strTest.Tables(0).Rows(0).Item("questid").ToString.Trim

                If strTest.Tables(0).Rows(0).Item("QuestImgType").ToString.Trim = "" Then
                    imgProfile.Width = Unit.Percentage(0)
                    imgProfile.Height = Unit.Percentage(0)
                Else

                    imgProfile.Width = Unit.Pixel(500)
                    imgProfile.Height = Unit.Pixel(400)
                End If

                Select Case txtQuestionType.Text
                    Case "SingleChoice"
                        rdoAnswers.Visible = True
                        chkAnswers.Visible = False
                        Process.LoadTestOptions(rdoAnswers, chkAnswers, Param)

                        Dim ianswer As String = strTest.Tables(0).Rows(0).Item("Answer").ToString
                        Select Case ianswer
                            Case "A"
                                ianswer = "A. " & strTest.Tables(0).Rows(0).Item("OptionA").ToString
                            Case "B"
                                ianswer = "B. " & strTest.Tables(0).Rows(0).Item("OptionB").ToString
                            Case "C"
                                ianswer = "C. " & strTest.Tables(0).Rows(0).Item("OptionC").ToString
                            Case "D"
                                ianswer = "D. " & strTest.Tables(0).Rows(0).Item("OptionD").ToString
                        End Select

                        Process.RadioListCheck(rdoAnswers, ianswer)
                    Case "MultipleChoice"
                        rdoAnswers.Visible = False
                        chkAnswers.Visible = True
                        Process.LoadTestOptions(rdoAnswers, chkAnswers, Param)
                        Dim answer As String = ""
                        answer = strTest.Tables(0).Rows(0).Item("Answer").ToString
                        options = answer.Split(Separators, StringSplitOptions.RemoveEmptyEntries)
                        Dim ianswer As String = ""
                        For j = 0 To options.Length - 1
                            If options(j) IsNot Nothing Then
                                Select Case options(j)
                                    Case "A"
                                        ianswer = "A. " & strTest.Tables(0).Rows(0).Item("OptionA").ToString
                                    Case "B"
                                        ianswer = "B. " & strTest.Tables(0).Rows(0).Item("OptionB").ToString
                                    Case "C"
                                        ianswer = "C. " & strTest.Tables(0).Rows(0).Item("OptionC").ToString
                                    Case "D"
                                        ianswer = "D. " & strTest.Tables(0).Rows(0).Item("OptionD").ToString
                                End Select
                                Process.CheckboxListCheck(chkAnswers, ianswer)
                            End If
                        Next
                End Select

            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnPrevious.Click
        Try
            If CInt(txtmin.Text) = 0 And CInt(txtsec.Text) = 0 Then
                lblstatus.Text = "Time up, click submit test"
                btnNext.Enabled = False
                btnNext.BackColor = Color.Gray
                btnPrevious.Enabled = False
                btnPrevious.BackColor = Color.Gray
                btnFinalSubmit.Enabled = True
                btnFinalSubmit.BackColor = Color.FromArgb(102, 153, 0)
                timeOut()
                Exit Sub
            End If

            Dim selectedanswer As String = ""
            Select Case txtQuestionType.Text
                Case "SingleChoice"
                    For i = 0 To rdoAnswers.Items.Count - 1
                        If rdoAnswers.Items(i).Selected Then
                            selectedanswer = rdoAnswers.Items(i).Value
                            Exit For
                        End If
                    Next
                Case "MultipleChoice"
                    For i = 0 To chkAnswers.Items.Count - 1
                        If chkAnswers.Items(i).Selected Then
                            If i = 0 Then
                                selectedanswer = chkAnswers.Items(i).Value
                            Else
                                selectedanswer = selectedanswer & "," & chkAnswers.Items(i).Value
                            End If
                        End If
                    Next
            End Select
            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Job_Test_Online_Result_Breakdown_Update", txtid.Text, selectedanswer, txtRealAnswer.Text)

            lblstatus.Text = ""

            Session("QuestionNo") = Session("QuestionNo") - 1

            If CInt(Session("QuestionNo")) < 1 Then
                Session("QuestionNo") = Session("QuestionNo") + 1
                btnPrevious.Enabled = False
                btnPrevious.BackColor = Color.Gray
                Exit Sub
            End If

            LoadQuestion(Session("ApplicationTestID"), Session("QuestionNo"))

            If CInt(Session("QuestionNo")) < CInt(txtQuestionCount.Text) Then
                btnNext.Enabled = True
                btnNext.BackColor = Color.FromArgb(102, 153, 0)
                btnFinalSubmit.Enabled = False
                btnFinalSubmit.BackColor = Color.Gray

            End If
            lblPage.Text = lblQuestionNo.Text & " of " & txtQuestionCount.Text
        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        Try
            lblstatus.Text = ""

            If CInt(txtmin.Text) <= 0 And CInt(txtsec.Text) <= 0 Then
                lblstatus.Text = "Time up, click submit test"
                btnNext.Enabled = False
                btnNext.BackColor = Color.Gray
                btnPrevious.Enabled = False
                btnPrevious.BackColor = Color.Gray
                btnFinalSubmit.Enabled = True
                btnFinalSubmit.BackColor = Color.FromArgb(102, 153, 0)
                timeOut()
                Exit Sub
            End If

            'Save answer
            Dim selectedanswer As String = ""
            Select Case txtQuestionType.Text
                Case "SingleChoice"
                    For i = 0 To rdoAnswers.Items.Count - 1
                        If rdoAnswers.Items(i).Selected Then
                            selectedanswer = rdoAnswers.Items(i).Value
                            Exit For
                        End If
                    Next
                Case "MultipleChoice"
                    For i = 0 To chkAnswers.Items.Count - 1
                        If chkAnswers.Items(i).Selected Then
                            If i = 0 Then
                                selectedanswer = chkAnswers.Items(i).Value
                            Else
                                selectedanswer = selectedanswer & "," & chkAnswers.Items(i).Value
                            End If
                        End If
                    Next
            End Select

            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Job_Test_Online_Result_Breakdown_Update", txtid.Text, selectedanswer, txtRealAnswer.Text)



            Session("QuestionNo") = Session("QuestionNo") + 1

            If CInt(Session("QuestionNo")) > 1 Then
                btnPrevious.Enabled = True
                btnPrevious.BackColor = Color.FromArgb(102, 153, 0)
            End If

            If CInt(Session("QuestionNo")) > CInt(txtQuestionCount.Text) Then
                Session("QuestionNo") = Session("QuestionNo") - 1
                btnNext.Enabled = False
                btnNext.BackColor = Color.Gray
                btnFinalSubmit.Enabled = True
                btnFinalSubmit.BackColor = Color.FromArgb(102, 153, 0)
                Exit Sub
            End If

            LoadQuestion(Session("ApplicationTestID"), Session("QuestionNo"))


            lblPage.Text = lblQuestionNo.Text & " of " & txtQuestionCount.Text
        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub



    Protected Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        Try
            Dim strConfirmDone As New DataSet
            strConfirmDone = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Prepare_OnlineTest_Check", Session("ApplcantID"), Session("ApplicantJobID"), Session("stage"))
            If strConfirmDone.Tables(0).Rows.Count > 0 Then
                MultiView1.ActiveViewIndex = 1
                Exit Sub
            End If

            lblTimeStart.Text = "Test Start Time: " & System.DateTime.Now
            lblstart.Text = System.DateTime.Now

            btnStart.Enabled = False
            statusid.Value = "3"

            btnNext.Enabled = True
            btnPrevious.Enabled = True
            rdoAnswers.Enabled = True
            chkAnswers.Enabled = True
            btnFinalSubmit.Enabled = False
            btnFinalSubmit.BackColor = Color.LightGray
            btnStart.BackColor = Color.LightGray

            'get quest
            'If Request.QueryString("id") IsNot Nothing Then
            Dim strTest As New DataSet
            'Session("ApplicationTestID") = Request.QueryString("id")
            'Session("stage") = Request.QueryString("stage")
            'Session("ApplicantJobID") = CInt(Request.QueryString("Code"))

            'Prepare Test
            '........................By GBENGA..................................
            Session("ApplicationTestID") = GetIdentity(Session("ApplcantID"), Session("ApplicantJobID"), Session("stage"))
            'SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Prepare_OnlineTest", Session("ApplcantID"), Session("ApplicantJobID"), Session("stage"))

            Session("QuestionNo") = 1
            strTest = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Applicant_OnlineTest", Session("ApplicationTestID"), Session("QuestionNo"))

            Dim Param(3) As String
            If strTest.Tables(0).Rows.Count > 0 Then
                txtDuration.Text = strTest.Tables(0).Rows(0).Item("TestDuration").ToString
                txtTime.Text = "Test Duration: " & txtDuration.Text & " mins"
                txtmin.Text = txtDuration.Text
                txtsec.Text = "0"

                lblHeader.Text = strTest.Tables(0).Rows(0).Item("jobtitle").ToString
                lblQuestionNo.Text = strTest.Tables(0).Rows(0).Item("rows").ToString & "."
                txtQuestionType.Text = strTest.Tables(0).Rows(0).Item("QuestionType").ToString

                imgProfile.ImageUrl = "QuestImage.ashx?imgid=" & strTest.Tables(0).Rows(0).Item("questid").ToString.Trim

                If strTest.Tables(0).Rows(0).Item("QuestImgType").ToString.Trim = "" Then
                    imgProfile.Width = Unit.Percentage(0)
                    imgProfile.Height = Unit.Percentage(0)
                Else

                    imgProfile.Width = Unit.Pixel(500)
                    imgProfile.Height = Unit.Pixel(400)
                End If

                txtQuestion.Text = strTest.Tables(0).Rows(0).Item("Questions").ToString
                Param(0) = "A. " & strTest.Tables(0).Rows(0).Item("OptionA").ToString
                Param(1) = "B. " & strTest.Tables(0).Rows(0).Item("OptionB").ToString
                Param(2) = "C. " & strTest.Tables(0).Rows(0).Item("OptionC").ToString
                Param(3) = "D. " & strTest.Tables(0).Rows(0).Item("OptionD").ToString
                txtQuestionCount.Text = strTest.Tables(0).Rows(0).Item("QuestionsCount").ToString
                txtid.Text = strTest.Tables(0).Rows(0).Item("id").ToString
                txtRealAnswer.Text = strTest.Tables(0).Rows(0).Item("RealAnswer").ToString.Trim

                Select Case txtQuestionType.Text
                    Case "SingleChoice"
                        rdoAnswers.Visible = True
                        chkAnswers.Visible = False
                        Process.LoadTestOptions(rdoAnswers, chkAnswers, Param)
                    Case "MultipleChoice"
                        rdoAnswers.Visible = False
                        chkAnswers.Visible = True
                        Process.LoadTestOptions(rdoAnswers, chkAnswers, Param)
                End Select
            End If
            'End If
            lblPage.Text = lblQuestionNo.Text & " of " & txtQuestionCount.Text

            btnNext.BackColor = Color.FromArgb(102, 153, 0)
            btnPrevious.BackColor = Color.FromArgb(102, 153, 0)
            'btnFinalSubmit.BackColor = Color.FromArgb(102, 153, 0)
        Catch ex As Exception

        End Try
    End Sub



    Protected Sub btnNext0_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        Try

            Response.Write("<script language='javascript'> { self.close() }</script>")
        Catch ex As Exception

        End Try
    End Sub
    Private Sub timeOut()
        Try
            'Save answer
            Dim selectedanswer As String = ""
            Select Case txtQuestionType.Text
                Case "SingleChoice"
                    For i = 0 To rdoAnswers.Items.Count - 1
                        If rdoAnswers.Items(i).Selected Then
                            selectedanswer = rdoAnswers.Items(i).Value
                            Exit For
                        End If
                    Next
                Case "MultipleChoice"
                    For i = 0 To chkAnswers.Items.Count - 1
                        If chkAnswers.Items(i).Selected Then
                            If i = 0 Then
                                selectedanswer = chkAnswers.Items(i).Value
                            Else
                                selectedanswer = selectedanswer & "," & chkAnswers.Items(i).Value
                            End If
                        End If
                    Next
            End Select

            Dim timetaken As Double = DateDiff(DateInterval.Minute, CDate(lblstart.Text), System.DateTime.Now)

            'Save result and calculate score
            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Job_Test_Online_Result_Breakdown_Update", txtid.Text, selectedanswer, txtRealAnswer.Text)
            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Job_Test_Online_Result_Update", Session("ApplicationTestID"), Session("stage"), timetaken)

            'Get saved result and send to candidate
            Dim strTest As New DataSet
            strTest = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Job_Test_Online_Result_Get", Session("ApplicationTestID"))
            Dim passmark As String = ""
            Dim score As String = ""
            Dim isPass As Boolean = False
            Dim remark As String = ""
            Dim jobpost As String = ""
            Dim emailaddr As String = ""
            Dim applicant As String = ""
            Dim jobid As Integer = 0
            Dim stageno As Integer = 0
            Dim company As String = ""
            If strTest.Tables(0).Rows.Count > 0 Then
                passmark = strTest.Tables(0).Rows(0).Item("passmark").ToString & "."
                score = strTest.Tables(0).Rows(0).Item("score").ToString
                remark = strTest.Tables(0).Rows(0).Item("remark").ToString
                jobpost = strTest.Tables(0).Rows(0).Item("Title").ToString
                emailaddr = strTest.Tables(0).Rows(0).Item("EmailAddress").ToString
                applicant = strTest.Tables(0).Rows(0).Item("Applicant").ToString
                jobid = strTest.Tables(0).Rows(0).Item("jobid").ToString
                stageno = strTest.Tables(0).Rows(0).Item("stageno").ToString
                company = strTest.Tables(0).Rows(0).Item("company").ToString
            End If
            If remark.ToUpper.Trim = "PASS" Then
                isPass = True
            Else
                isPass = False
            End If
            stageno = Session("stage")

            'confirm if there is are other online tests available
            'Dim counts As Integer = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Recruit_Job_Test_Confirm_Activation", 0, jobid, 0)

            Dim URL As String = Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath 'Request.ServerVariables("HTTP_HOST") 
            'URL = "http://" & System.Net.Dns.GetHostByName(Server.MachineName.ToString).AddressList(0).ToString() & "/GOSHRM"
            URL = Process.ApplicationURL()
            Dim TestURL As String = ConfigurationManager.AppSettings("TestURL")

            'Check More Online Stage
            Dim countMoreOnline As Integer = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select Count(*) from Recruit_Job_Test a where a.[Online] = 'Yes' and a.Active = 'Yes' and  a.jobid = " & jobid & " and stageno > " & stageno)
            If countMoreOnline > 0 Then
                'next stage
                Dim nextStage As Integer = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select min(StageNo) from Recruit_Job_Test a where a.[Online] = 'Yes' and a.Active = 'Yes' and  a.jobid = " & jobid & " and stageno > " & stageno)
                Process.OnlineTest_First_Result(stageno, company, isPass, emailaddr, jobpost, applicant, score, passmark, URL & TestURL & jobid & "&stage=" & nextStage.ToString, nextStage)
            Else
                Process.OnlineTest_Last_Result(company, isPass, emailaddr, jobpost, applicant, score, passmark)
            End If



            Label4.Text = "Assessment outcome will be sent to " & emailaddr
            MultiView1.ActiveViewIndex = 1
        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub
    Protected Sub btnFinalSubmit_Click(sender As Object, e As EventArgs) Handles btnFinalSubmit.Click
        'Try
        '    'Save answer
        '    Dim selectedanswer As String = ""
        '    Select Case txtQuestionType.Text
        '        Case "SingleChoice"
        '            For i = 0 To rdoAnswers.Items.Count - 1
        '                If rdoAnswers.Items(i).Selected Then
        '                    selectedanswer = rdoAnswers.Items(i).Value
        '                    Exit For
        '                End If
        '            Next
        '        Case "MultipleChoice"
        '            For i = 0 To chkAnswers.Items.Count - 1
        '                If chkAnswers.Items(i).Selected Then
        '                    If i = 0 Then
        '                        selectedanswer = chkAnswers.Items(i).Value
        '                    Else
        '                        selectedanswer = selectedanswer & "," & chkAnswers.Items(i).Value
        '                    End If
        '                End If
        '            Next
        '    End Select

        '    Dim timetaken As Double = DateDiff(DateInterval.Minute, CDate(lblstart.Text), System.DateTime.Now)

        '    'Save result and calculate score
        '    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Job_Test_Online_Result_Breakdown_Update", txtid.Text, selectedanswer, txtRealAnswer.Text)
        '    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Job_Test_Online_Result_Update", Session("ApplicationTestID"), Session("stage"), timetaken)

        '    'Get saved result and send to candidate
        '    Dim strTest As New DataSet
        '    strTest = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Job_Test_Online_Result_Get", Session("ApplicationTestID"))
        '    Dim passmark As String = ""
        '    Dim score As String = ""
        '    Dim isPass As Boolean = False
        '    Dim remark As String = ""
        '    Dim jobpost As String = ""
        '    Dim emailaddr As String = ""
        '    Dim applicant As String = ""
        '    Dim jobid As Integer = 0
        '    Dim stageno As Integer = 0
        '    Dim company As String = ""
        '    If strTest.Tables(0).Rows.Count > 0 Then
        '        passmark = strTest.Tables(0).Rows(0).Item("passmark").ToString & "."
        '        score = strTest.Tables(0).Rows(0).Item("score").ToString
        '        remark = strTest.Tables(0).Rows(0).Item("remark").ToString
        '        jobpost = strTest.Tables(0).Rows(0).Item("Title").ToString
        '        emailaddr = strTest.Tables(0).Rows(0).Item("EmailAddress").ToString
        '        applicant = strTest.Tables(0).Rows(0).Item("Applicant").ToString
        '        jobid = strTest.Tables(0).Rows(0).Item("jobid").ToString
        '        stageno = strTest.Tables(0).Rows(0).Item("stageno").ToString
        '        company = strTest.Tables(0).Rows(0).Item("company").ToString
        '    End If
        '    If remark.ToUpper.Trim = "PASS" Then
        '        isPass = True
        '    Else
        '        isPass = False
        '    End If
        '    stageno = Session("stage")

        '    'confirm if there is are other online tests available
        '    'Dim counts As Integer = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Recruit_Job_Test_Confirm_Activation", 0, jobid, 0)

        '    Dim URL As String = Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath 'Request.ServerVariables("HTTP_HOST") 
        '    'URL = "http://" & System.Net.Dns.GetHostByName(Server.MachineName.ToString).AddressList(0).ToString() & "/GOSHRM"
        '    URL = Process.ApplicationURL()
        '    Dim TestURL As String = ConfigurationManager.AppSettings("TestURL")

        '    'Check More Online Stage
        '    Dim countMoreOnline As Integer = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select Count(*) from Recruit_Job_Test a where a.[Online] = 'Yes' and a.Active = 'Yes' and  a.jobid = " & jobid & " and stageno > " & stageno)
        '    If countMoreOnline > 0 Then
        '        'next stage
        '        Dim nextStage As Integer = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select min(StageNo) from Recruit_Job_Test a where a.[Online] = 'Yes' and a.Active = 'Yes' and  a.jobid = " & jobid & " and stageno > " & stageno)
        '        Process.OnlineTest_First_Result(stageno, company, isPass, emailaddr, jobpost, applicant, score, passmark, URL & TestURL & jobid & "&stage=" & nextStage.ToString, nextStage)
        '    Else
        '        Process.OnlineTest_Last_Result(company, isPass, emailaddr, jobpost, applicant, score, passmark)
        '    End If



        '    Label4.Text = "Assessment outcome will be sent to " & emailaddr
        '    MultiView1.ActiveViewIndex = 1
        'Catch ex As Exception
        '    lblstatus.Text = ex.Message
        'End Try
        Try
            timeOut()
        Catch ex As Exception

        End Try
    End Sub
End Class