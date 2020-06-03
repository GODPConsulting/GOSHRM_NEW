Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO


Public Class LearningAssessment
    Inherits System.Web.UI.Page
    Dim education As New clsEducation
    Dim AuthenCode As String = "JOBTEST"
    Dim olddata(4) As String
    Dim options(4) As String
    Dim Separators() As Char = {","c}
    Dim imgdata As Byte()
    Private Function GetIdentity(ByVal EmpTrainSessionID As Integer, ByVal Questions As String, ByVal QuestionType As String, _
                                 ByVal EmpAnswer As String, ByVal Answer As String, ByVal Ordering As Integer, ByVal OptionA As String, _
                                 ByVal OptionB As String, ByVal OptionC As String, ByVal OptionD As String, ByVal Images As String, ByVal Points As Integer, _
                                 imgtype As String, testquestid As Integer) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Emp_Training_Learning_Assessment_Update"
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@EmpTrainSessionID", SqlDbType.Int).Value = EmpTrainSessionID
            cmd.Parameters.Add("@Questions", SqlDbType.VarChar).Value = Questions
            cmd.Parameters.Add("@QuestionType", SqlDbType.VarChar).Value = QuestionType
            cmd.Parameters.Add("@EmpAnswer", SqlDbType.VarChar).Value = EmpAnswer
            cmd.Parameters.Add("@Answer", SqlDbType.VarChar).Value = Answer
            cmd.Parameters.Add("@Ordering", SqlDbType.Int).Value = Ordering
            cmd.Parameters.Add("@OptionA", SqlDbType.VarChar).Value = OptionA
            cmd.Parameters.Add("@OptionB", SqlDbType.VarChar).Value = OptionB
            cmd.Parameters.Add("@OptionC", SqlDbType.VarChar).Value = OptionC
            cmd.Parameters.Add("@OptionD", SqlDbType.VarChar).Value = OptionD
            cmd.Parameters.Add("@Images", SqlDbType.VarChar).Value = Images
            cmd.Parameters.Add("@Points", SqlDbType.Int).Value = Points
            cmd.Parameters.Add("@imgtype", SqlDbType.VarChar).Value = Images
            cmd.Parameters.Add("@testquestid", SqlDbType.Int).Value = Points
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
            'SingleLineTextBox, // will render a textbox 
            'MultiLineTextBox, // will render a text area
            'YesOrNo, //will render a checkbox
            'SingleSelect, //will render a dropdownlist
            'MultiSelect //will render a listbo

            If Not Me.IsPostBack Then
                txtQuestion.Text = "Click Start to commence assessment"
                btnNext.BackColor = Color.Gray
                btnPrevious.BackColor = Color.Gray
                btnFinish.BackColor = Color.Gray

                Session("strttime") = System.DateTime.Now
                btnNext.Enabled = False
                btnPrevious.Enabled = False
                rdoAnswers.Enabled = False
                chkAnswers.Enabled = False
                lblstatus.Text = ""
                Session("PreviousPage") = Request.UrlReferrer
                Session("ApplicationTestID") = Request.QueryString("id")

                imgProfile.Width = Unit.Percentage(0)
                imgProfile.Height = Unit.Percentage(0)

                Dim strCourse As New DataSet
                strCourse = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Courses_Get_SessionID", Session("ApplicationTestID"))
                If strCourse.Tables(0).Rows.Count > 0 Then
                    txtDuration.Text = strCourse.Tables(0).Rows(0).Item("testduration").ToString
                    txtTime.Text = "Test Duration: " & txtDuration.Text & " mins"
                    lblHeader.Text = strCourse.Tables(0).Rows(0).Item("CourseName").ToString
                    txtQuestionCount.Text = strCourse.Tables(0).Rows(0).Item("Counts").ToString
                    lbllsession.Text = strCourse.Tables(0).Rows(0).Item("TrainingSession").ToString
                    txtmin.Text = txtDuration.Text
                    txtsec.Text = "0"
                Else
                    Response.Write("No Assessment available!")
                End If

            End If
        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub
    Private Sub LoadQuestion(ByVal testID As String, ByVal questionno As String)
        Try
            Dim strTest As New DataSet
            strTest = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Training_Learning_Assessment_Get_All", testID, questionno)

            Dim Param(3) As String
            If strTest.Tables(0).Rows.Count > 0 Then
                lblQuestionNo.Text = strTest.Tables(0).Rows(0).Item("Rows").ToString & "."
                txtQuestionType.Text = strTest.Tables(0).Rows(0).Item("QuestionType").ToString
                txtQuestion.Text = strTest.Tables(0).Rows(0).Item("Questions").ToString
                Param(0) = "A. " & strTest.Tables(0).Rows(0).Item("OptionA").ToString
                Param(1) = "B. " & strTest.Tables(0).Rows(0).Item("OptionB").ToString
                Param(2) = "C. " & strTest.Tables(0).Rows(0).Item("OptionC").ToString
                Param(3) = "D. " & strTest.Tables(0).Rows(0).Item("OptionD").ToString
                'txtQuestionCount.Text = strTest.Tables(0).Rows(0).Item("QuestionsCount").ToString
                txtid.Text = strTest.Tables(0).Rows(0).Item("id").ToString
                txtRealAnswer.Text = strTest.Tables(0).Rows(0).Item("Answer").ToString.Trim
                lblimagetype.Text = strTest.Tables(0).Rows(0).Item("ImgType").ToString.Trim
                lbltestquestid.Text = strTest.Tables(0).Rows(0).Item("testquestid").ToString.Trim


                imgProfile.ImageUrl = "~/Module/trainings/portal/assessImage.ashx?imgid=" & strTest.Tables(0).Rows(0).Item("testid").ToString.Trim

                If strTest.Tables(0).Rows(0).Item("ImgType").ToString.Trim = "" Then
                    imgProfile.Width = Unit.Percentage(0)
                    imgProfile.Height = Unit.Percentage(0)
                Else
                    imgProfile.Width = Unit.Percentage(100)
                    imgProfile.Height = Unit.Pixel(300)
                End If

                lblPoint.Text = strTest.Tables(0).Rows(0).Item("Points").ToString
              

                Select Case txtQuestionType.Text
                    Case "Text"
                        rdoAnswers.Visible = False
                        chkAnswers.Visible = False
                        txtAnswers.Visible = True
                        txtAnswers.Text = strTest.Tables(0).Rows(0).Item("empAnswer").ToString

                    Case "SingleChoice"
                        rdoAnswers.Visible = True
                        chkAnswers.Visible = False
                        txtAnswers.Visible = False

                        Process.LoadTestOptions(rdoAnswers, chkAnswers, Param)

                        Dim ianswer As String = strTest.Tables(0).Rows(0).Item("empAnswer").ToString
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
                        txtAnswers.Visible = False
                        Process.LoadTestOptions(rdoAnswers, chkAnswers, Param)
                        Dim answer As String = ""
                        answer = strTest.Tables(0).Rows(0).Item("empAnswer").ToString
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
                Exit Sub
            End If

            'Save answer
            Dim selectedanswer As String = ""
            Dim options(4) As String
            options(0) = ""
            options(1) = ""
            options(2) = ""
            options(3) = ""
            Select Case txtQuestionType.Text
                Case "SingleChoice"
                    For i = 0 To rdoAnswers.Items.Count - 1
                        options(i) = rdoAnswers.Items(i).Text.Substring(2, rdoAnswers.Items(i).Text.Length - 2).Trim
                        If rdoAnswers.Items(i).Selected Then
                            selectedanswer = rdoAnswers.Items(i).Value
                            Exit For
                        End If
                    Next
                Case "MultipleChoice"
                    For i = 0 To chkAnswers.Items.Count - 1
                        options(i) = chkAnswers.Items(i).Text.Substring(2, chkAnswers.Items(i).Text.Length - 2).Trim
                        If chkAnswers.Items(i).Selected Then
                            If i = 0 Then
                                selectedanswer = chkAnswers.Items(i).Value
                            Else
                                selectedanswer = selectedanswer & "," & chkAnswers.Items(i).Value
                            End If
                        End If
                    Next
                Case "Text"
                    selectedanswer = txtAnswers.Text
            End Select

            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Training_Learning_Assessment_Update", txtid.Text, Session("ApplicationTestID"), txtQuestion.Text, txtQuestionType.Text, selectedanswer, txtRealAnswer.Text, lblQuestionNo.Text.Replace(".", ""), options(0), options(1), options(2), options(3), lblimage.Text, lblPoint.Text, lblimagetype.Text, lbltestquestid.Text)

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
                Exit Sub
            End If

            'Save answer
            Dim selectedanswer As String = ""
            Dim options(4) As String
            options(0) = ""
            options(1) = ""
            options(2) = ""
            options(3) = ""
            Select Case txtQuestionType.Text
                Case "SingleChoice"
                    For i = 0 To rdoAnswers.Items.Count - 1
                        options(i) = rdoAnswers.Items(i).Text.Substring(2, rdoAnswers.Items(i).Text.Length - 2).Trim
                        If rdoAnswers.Items(i).Selected Then
                            selectedanswer = rdoAnswers.Items(i).Value
                            Exit For
                        End If
                    Next
                Case "MultipleChoice"
                    For i = 0 To chkAnswers.Items.Count - 1
                        options(i) = chkAnswers.Items(i).Text.Substring(2, chkAnswers.Items(i).Text.Length - 2).Trim
                        If chkAnswers.Items(i).Selected Then
                            If i = 0 Then
                                selectedanswer = chkAnswers.Items(i).Value
                            Else
                                selectedanswer = selectedanswer & "," & chkAnswers.Items(i).Value
                            End If
                        End If
                    Next
                Case "Text"
                    selectedanswer = txtAnswers.Text
            End Select

            If txtid.Text = "0" Then
                txtid.Text = GetIdentity(Session("ApplicationTestID"), txtQuestion.Text, txtQuestionType.Text, selectedanswer, txtRealAnswer.Text, lblQuestionNo.Text.Replace(".", ""), options(0), options(1), options(2), options(3), lblimage.Text, lblPoint.Text, lblimagetype.Text, lbltestquestid.Text)
            Else
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Training_Learning_Assessment_Update", txtid.Text, Session("ApplicationTestID"), txtQuestion.Text, txtQuestionType.Text, selectedanswer, txtRealAnswer.Text, lblQuestionNo.Text.Replace(".", ""), options(0), options(1), options(2), options(3), lblimage.Text, lblPoint.Text, lblimagetype.Text, lbltestquestid.Text)
            End If

            Session("QuestionNo") = Session("QuestionNo") + 1

            If CInt(Session("QuestionNo")) > 1 Then
                btnPrevious.Enabled = True
                btnPrevious.BackColor = Color.FromArgb(102, 153, 0)
            End If

            If CInt(Session("QuestionNo")) > CInt(txtQuestionCount.Text) Then
                Session("QuestionNo") = Session("QuestionNo") - 1
                btnNext.Enabled = False
                btnNext.BackColor = Color.Gray
                Exit Sub
            End If

            If CInt(Session("QuestionNo")) = CInt(txtQuestionCount.Text) Then
                btnNext.Enabled = False
                btnNext.BackColor = Color.Gray
            End If

            LoadQuestion(Session("ApplicationTestID"), Session("QuestionNo"))

            lblPage.Text = lblQuestionNo.Text & " of " & txtQuestionCount.Text
        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub



    Protected Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        Try
            lblTimeStart.Text = "Assessment Start Time: " & System.DateTime.Now
            lblstart.Text = System.DateTime.Now

            txtQuestion.Text = ""
            btnStart.Enabled = False
            statusid.Value = "3"

            btnNext.Enabled = True
            btnPrevious.Enabled = True
            rdoAnswers.Enabled = True
            chkAnswers.Enabled = True
            btnStart.BackColor = Color.LightGray

            'get quest

            Dim strTest As New DataSet
            'Prepare Test         
            Session("QuestionNo") = 1
            LoadQuestion(Session("ApplicationTestID"), Session("QuestionNo"))
            Dim strCourse As New DataSet
            strCourse = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Courses_Get_SessionID", Session("ApplicationTestID"))
            If strCourse.Tables(0).Rows.Count > 0 Then
                txtDuration.Text = strCourse.Tables(0).Rows(0).Item("testduration").ToString
                txtTime.Text = "Test Duration: " & txtDuration.Text & " mins"
                lblHeader.Text = strCourse.Tables(0).Rows(0).Item("CourseName").ToString
                txtQuestionCount.Text = strCourse.Tables(0).Rows(0).Item("Counts").ToString
                txtmin.Text = txtDuration.Text
                txtsec.Text = "0"
            End If


            'strTest = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Training_Learning_Assessment_Get_All", Session("ApplicationTestID"), Session("QuestionNo"))

          

            'Dim Param(3) As String
            'If strTest.Tables(0).Rows.Count > 0 Then

            
            '    lblPoint.Text = strTest.Tables(0).Rows(0).Item("Points").ToString
            '    lblQuestionNo.Text = strTest.Tables(0).Rows(0).Item("rows").ToString & "."
            '    txtQuestionType.Text = strTest.Tables(0).Rows(0).Item("QuestionType").ToString
            '    lblimage.Text = strTest.Tables(0).Rows(0).Item("images").ToString
            '    imgProfile.ImageUrl = strTest.Tables(0).Rows(0).Item("images").ToString
            '    If strTest.Tables(0).Rows(0).Item("images").ToString = "" Then
            '        imgProfile.Width = Unit.Percentage(0)
            '        imgProfile.Height = Unit.Percentage(0)
            '    Else
            '        imgProfile.Width = Unit.Percentage(100)
            '        imgProfile.Height = Unit.Pixel(300)
            '    End If

            '    txtQuestion.Text = strTest.Tables(0).Rows(0).Item("Questions").ToString
            '    Param(0) = "A. " & strTest.Tables(0).Rows(0).Item("OptionA").ToString
            '    Param(1) = "B. " & strTest.Tables(0).Rows(0).Item("OptionB").ToString
            '    Param(2) = "C. " & strTest.Tables(0).Rows(0).Item("OptionC").ToString
            '    Param(3) = "D. " & strTest.Tables(0).Rows(0).Item("OptionD").ToString

            '    txtid.Text = strTest.Tables(0).Rows(0).Item("id").ToString
            '    txtRealAnswer.Text = strTest.Tables(0).Rows(0).Item("Answer").ToString.Trim

            '    Select Case txtQuestionType.Text
            '        Case "SingleChoice"
            '            rdoAnswers.Visible = True
            '            chkAnswers.Visible = False
            '            txtAnswers.Visible = False
            '            Process.LoadTestOptions(rdoAnswers, chkAnswers, Param)
            '        Case "MultipleChoice"
            '            rdoAnswers.Visible = False
            '            chkAnswers.Visible = True
            '            txtAnswers.Visible = False
            '            Process.LoadTestOptions(rdoAnswers, chkAnswers, Param)
            '        Case "Text"
            '            rdoAnswers.Visible = False
            '            chkAnswers.Visible = False
            '            txtAnswers.Visible = True
            '            txtAnswers.Text = ""
            '    End Select
            'End If
            ''End If
            lblPage.Text = lblQuestionNo.Text & " of " & txtQuestionCount.Text

            btnNext.BackColor = Color.FromArgb(102, 153, 0)
            btnPrevious.BackColor = Color.LightGray
            btnPrevious.Enabled = False
            btnFinish.BackColor = Color.FromArgb(102, 153, 0)
        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles btnFinish.Click
        Try
            'Save answer
            Dim selectedanswer As String = ""
            Select Case txtQuestionType.Text
                Case "SingleChoice"
                    For i = 0 To rdoAnswers.Items.Count - 1
                        options(i) = rdoAnswers.Items(i).Text.Substring(2, rdoAnswers.Items(i).Text.Length - 2).Trim
                        If rdoAnswers.Items(i).Selected Then
                            selectedanswer = rdoAnswers.Items(i).Value
                            Exit For
                        End If
                    Next
                Case "MultipleChoice"
                    For i = 0 To chkAnswers.Items.Count - 1
                        options(i) = chkAnswers.Items(i).Text.Substring(2, chkAnswers.Items(i).Text.Length - 2).Trim
                        If chkAnswers.Items(i).Selected Then
                            If i = 0 Then
                                selectedanswer = chkAnswers.Items(i).Value
                            Else
                                selectedanswer = selectedanswer & "," & chkAnswers.Items(i).Value
                            End If
                        End If
                    Next
                Case "Text"
                    selectedanswer = txtAnswers.Text
            End Select

            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Training_Learning_Assessment_Update", txtid.Text, Session("ApplicationTestID"), txtQuestion.Text, txtQuestionType.Text, selectedanswer, txtRealAnswer.Text, lblQuestionNo.Text.Replace(".", ""), options(0), options(1), options(2), options(3), lblimage.Text, lblPoint.Text, lblimagetype.Text, lbltestquestid.Text)
            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Employee_Training_Sessions_Update_AssessmentState", Session("ApplicationTestID"))

            Dim gg As Double = DateDiff(DateInterval.Minute, System.DateTime.Now, CDate(lblstart.Text))

            Session("ApplicationTestID") = ""
            Process.Training_Assessment_Complete("Learning", lbllsession.Text, Session("userempid"), Process.GetEmployeeData(Session("userempid"), "linemanagerid"), Process.ApplicationURL + "/" + Process.GetMailLink(AuthenCode, 2), Process.ApplicationURL + "/" + Process.GetMailLink(AuthenCode, 1))
            Label4.Text = "Thank you for taking your time to this assessment"
            MultiView1.ActiveViewIndex = 1
        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnNext0_Click(sender As Object, e As EventArgs) Handles btnNext0.Click
        Try
            Response.Write("<script language='javascript'> { self.close() }</script>")
            'Response.Redirect(Session("PreviousPage"), True)
        Catch ex As Exception

        End Try
    End Sub
End Class