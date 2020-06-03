Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports System.IO


Public Class JobQuestionsUpdate
    Inherits System.Web.UI.Page
    Dim education As New clsEducation
    Dim AuthenCode As String = "JOBTEST"
    Dim olddata(4) As String
    Dim options(4) As String
    Dim Separators() As Char = {","c}
    Dim answer As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'SingleLineTextBox, // will render a textbox 
            'MultiLineTextBox, // will render a text area
            'YesOrNo, //will render a checkbox
            'SingleSelect, //will render a dropdownlist
            'MultiSelect //will render a listbo

            If Not Me.IsPostBack Then
                imgProfile.Width = Unit.Pixel(0)
                imgProfile.Height = Unit.Pixel(0)
                imgProfile.Visible = False
                rdoAnswers.Items.Clear()
                rdoAnswers.Items.Add("A")
                rdoAnswers.Items.Add("B")
                rdoAnswers.Items.Add("C")
                rdoAnswers.Items.Add("D")

                chkAnswers.Items.Clear()
                chkAnswers.Items.Add("A")
                chkAnswers.Items.Add("B")
                chkAnswers.Items.Add("C")
                chkAnswers.Items.Add("D")

                ViewState("PreviousPage") = Request.UrlReferrer

                Dim strTest As New DataSet
                strTest = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Job_Test_Get", CInt(Session("JobTestID")))
                Dim strOnline As String = "No"
                Dim testtitle As String = ""
                If strTest.Tables(0).Rows.Count > 0 Then
                    strOnline = strTest.Tables(0).Rows(0).Item("Online").ToString
                    testtitle = strTest.Tables(0).Rows(0).Item("TestTitle").ToString
                End If

                cboQuestionType.Items.Clear()
                cboQuestionType.Items.Add("MultipleChoice")
                cboQuestionType.Items.Add("SingleChoice")

                If strOnline = "No" Then
                    cboQuestionType.Items.Add("Text")
                End If

                Dim QuestionType As String = ""

                If Request.QueryString("id") IsNot Nothing Then
                    Dim strUser As New DataSet
                    pagetitle.InnerText = testtitle
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Job_Test_Questions_Get", Request.QueryString("id"))
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    QuestionType = strUser.Tables(0).Rows(0).Item("QuestionType").ToString
                    answer = strUser.Tables(0).Rows(0).Item("answer").ToString
                    Process.AssignRadComboValue(cboQuestionType, QuestionType)
                    aquestion.Value = strUser.Tables(0).Rows(0).Item("questions").ToString
                    lblimage.Text = strUser.Tables(0).Rows(0).Item("imgtype").ToString
                    imgProfile.ImageUrl = "~/Module/Recruitment/OnlineTest/QuestImage.ashx?imgid=" & txtid.Text

                    If lblimage.Text = "" Then
                        imgProfile.Width = Unit.Pixel(0)
                        imgProfile.Height = Unit.Pixel(0)
                        imgProfile.Visible = False
                    Else
                        imgProfile.Width = Unit.Pixel(500)
                        imgProfile.Height = Unit.Pixel(500)
                        imgProfile.Visible = True
                    End If
                    Select Case QuestionType
                        Case "SingleChoice"
                            divoptions.Visible = True                            
                            rdoAnswers.Visible = True
                            chkAnswers.Visible = False
                            answertext.Visible = False
                            Process.RadioListCheck(rdoAnswers, answer)
                        Case "MultipleChoice"
                            divoptions.Visible = True
                            rdoAnswers.Visible = False
                            chkAnswers.Visible = True
                            answertext.Visible = False

                            options = answer.Split(Separators, StringSplitOptions.RemoveEmptyEntries)
                            For j = 0 To options.Length - 1
                                If options(j) IsNot Nothing Then
                                    Process.CheckboxListCheck(chkAnswers, options(j).ToString)
                                End If
                            Next

                        Case "Text"
                            divoptions.Visible = False
                            rdoAnswers.Visible = False
                            chkAnswers.Visible = False
                            answertext.Visible = True
                            answertext.Value = answer
                    End Select

                    aoptiona.Value = strUser.Tables(0).Rows(0).Item("OptionA").ToString
                    aoptionb.Value = strUser.Tables(0).Rows(0).Item("OptionB").ToString
                    aoptionc.Value = strUser.Tables(0).Rows(0).Item("OptionC").ToString
                    aoptiond.Value = strUser.Tables(0).Rows(0).Item("OptionD").ToString
                    aposition.Value = strUser.Tables(0).Rows(0).Item("Ordering").ToString
                Else
                    txtid.Text = "0"
                    aposition.Value = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Recruit_Job_Test_Questions_Get_Position", Session("JobTestID"))
                    QuestionType = cboQuestionType.SelectedItem.Text
                    Select Case QuestionType
                        Case "SingleChoice"
                            divoptions.Visible = True
                            rdoAnswers.Visible = True
                            chkAnswers.Visible = False
                            answertext.Visible = False

                        Case "MultipleChoice"
                            divoptions.Visible = True
                            rdoAnswers.Visible = False
                            chkAnswers.Visible = True
                            answertext.Visible = False



                        Case "Text"
                            divoptions.Visible = False
                            rdoAnswers.Visible = False
                            chkAnswers.Visible = False
                            answertext.Visible = True

                    End Select
                End If

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Function GetImageIdentity(ByVal jobtestid As String, ByVal question As String, ByVal questiontype As String, _
                                 ByVal order As Integer, ByVal answers As String, ByVal opt11 As String, _
                                  ByVal opt12 As String, ByVal opt13 As String, ByVal opt14 As String, ByVal strImage As Byte(), ByVal strImageType As String) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Recruit_Job_Test_Questions_Image_Update"
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@JobTestID", SqlDbType.VarChar).Value = jobtestid
            cmd.Parameters.Add("@Questions", SqlDbType.VarChar).Value = question
            cmd.Parameters.Add("@QuestionType", SqlDbType.VarChar).Value = questiontype
            cmd.Parameters.Add("@Order", SqlDbType.Int).Value = order
            cmd.Parameters.Add("@Answer", SqlDbType.VarChar).Value = answers
            cmd.Parameters.Add("@Opt1", SqlDbType.VarChar).Value = opt11
            cmd.Parameters.Add("@Opt2", SqlDbType.VarChar).Value = opt12
            cmd.Parameters.Add("@Opt3", SqlDbType.VarChar).Value = opt13
            cmd.Parameters.Add("@Opt4", SqlDbType.VarChar).Value = opt14
            cmd.Parameters.Add("@Photo", SqlDbType.Image).Value = strImage
            cmd.Parameters.Add("@imagetype", SqlDbType.VarChar).Value = strImageType
            cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = Session("loginid")

            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception
            Process.strExp = ex.Message
            Return "0"
        End Try
    End Function
    Private Function GetIdentity(ByVal jobtestid As String, ByVal question As String, ByVal questiontype As String, _
                                 ByVal order As Integer, ByVal answers As String, ByVal opt11 As String, _
                                  ByVal opt12 As String, ByVal opt13 As String, ByVal opt14 As String) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Recruit_Job_Test_Questions_Update"
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@JobTestID", SqlDbType.VarChar).Value = jobtestid
            cmd.Parameters.Add("@Questions", SqlDbType.VarChar).Value = question
            cmd.Parameters.Add("@QuestionType", SqlDbType.VarChar).Value = questiontype
            cmd.Parameters.Add("@Order", SqlDbType.Int).Value = order
            cmd.Parameters.Add("@Answer", SqlDbType.VarChar).Value = answers
            cmd.Parameters.Add("@Opt1", SqlDbType.VarChar).Value = opt11
            cmd.Parameters.Add("@Opt2", SqlDbType.VarChar).Value = opt12
            cmd.Parameters.Add("@Opt3", SqlDbType.VarChar).Value = opt13
            cmd.Parameters.Add("@Opt4", SqlDbType.VarChar).Value = opt14
            cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = Session("loginid")
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
            If txtid.Text <> "0" Then
                If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False Then                    
                    Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "danger")
                    Exit Sub
                End If
            End If
            Dim lblstatus As String = ""
            If aquestion.Value.Trim = "" Then
                lblstatus = "Test Question required!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                aquestion.Focus()
                Exit Sub
            End If

            If cboquestiontype.SelectedItem.Text.Contains("SingleChoice") Or cboquestiontype.SelectedItem.Text.Contains("MultipleChoice") Then
                If aoptiona.Value.Trim = "" Then
                    lblstatus = "Options required!"
                    Process.loadalert(divalert, msgalert, lblstatus, "danger")
                    aoptiona.Focus()
                    Exit Sub
                End If

                If aoptionb.Value.Trim = "" Then
                    lblstatus = "Options required!"
                    Process.loadalert(divalert, msgalert, lblstatus, "danger")
                    aoptionb.Focus()
                    Exit Sub
                End If
            End If

            answer = ""

            If txtid.Text.Trim = "" Then
                txtid.Text = "0"
            End If

            If aposition.Value.Trim <> "" Then
                If IsNumeric(aposition.Value) = False Then
                    lblstatus = "Question No must be numeric!"
                    Process.loadalert(divalert, msgalert, lblstatus, "danger")
                    aposition.Focus()
                    Exit Sub
                End If
            Else
                aposition.Value = "0"
            End If

            Select Case cboquestiontype.SelectedItem.Text
                Case "SingleChoice"
                    For i = 0 To rdoAnswers.Items.Count - 1
                        If rdoAnswers.Items(i).Selected Then
                            answer = rdoAnswers.Items(i).Value
                            Exit For
                        End If
                    Next
                Case "MultipleChoice"
                    For i = 0 To chkAnswers.Items.Count - 1
                        If chkAnswers.Items(i).Selected Then
                            If i = 0 Then
                                answer = chkAnswers.Items(i).Value
                            Else
                                answer = answer & "," & chkAnswers.Items(i).Value
                            End If
                        End If
                    Next

                Case "Text"
                    answer = answertext.Value
            End Select


           


            If Not file1.PostedFile Is Nothing Then
                Dim img_strm As Stream = file1.PostedFile.InputStream
                'Retrieving the length of the file to upload
                Dim img_len As Integer = file1.PostedFile.ContentLength
                'retrieving the type of the file to upload
                Dim strtype As String = file1.PostedFile.ContentType.ToString()
                Dim strname As String = Path.GetFileName(file1.PostedFile.FileName)
                Dim imgdata As Byte() = New Byte(img_len - 1) {}
                Dim n As Integer = img_strm.Read(imgdata, 0, img_len)
                If txtid.Text = "0" Then
                    txtid.Text = GetImageIdentity(Session("JobTestID"), aquestion.Value.Trim, cboquestiontype.SelectedItem.Text, aposition.Value, answer, aoptiona.Value, aoptionb.Value, aoptionc.Value, aoptiond.Value, imgdata, strtype)
                    If txtid.Text = "0" Then
                        Exit Sub
                    End If
                Else
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Job_Test_Questions_Image_Update", txtid.Text, Session("JobTestID"), aquestion.Value.Trim, cboquestiontype.SelectedItem.Text, aposition.Value, answer, aoptiona.Value, aoptionb.Value, aoptionc.Value, aoptiond.Value, imgdata, strtype, Session("loginid"))
                End If
            Else
                If txtid.Text = "0" Then
                    txtid.Text = GetIdentity(Session("JobTestID"), aquestion.Value.Trim, cboquestiontype.SelectedItem.Text, aposition.Value, answer, aoptiona.Value, aoptionb.Value, aoptionc.Value, aoptiond.Value)
                    If txtid.Text = "0" Then
                        Exit Sub
                    End If
                Else
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Job_Test_Questions_Update", txtid.Text, Session("JobTestID"), aquestion.Value.Trim, cboquestiontype.SelectedItem.Text, aposition.Value, answer, aoptiona.Value, aoptionb.Value, aoptionc.Value, aoptiond.Value)
                End If
            End If
            imgProfile.ImageUrl = "~/Module/Recruitment/OnlineTest/QuestImage.ashx?imgid=" & txtid.Text

            Dim strimage As New DataSet
            strimage = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Job_Test_Questions_Get", txtid.Text)
            lblimage.Text = strimage.Tables(0).Rows(0).Item("imgtype").ToString
            
            If lblimage.Text = "" Then
                imgProfile.Width = Unit.Pixel(0)
                imgProfile.Height = Unit.Pixel(0)
                imgProfile.Visible = False
            Else
                imgProfile.Width = Unit.Pixel(500)
                imgProfile.Height = Unit.Pixel(500)
                imgProfile.Visible = True
            End If

            lblstatus = "Record saved"
            Process.loadalert(divalert, msgalert, lblstatus, "success")
            'Response.Write("<script language='javascript'> { self.close() }</script>")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            Try
                Response.Redirect("~/Module/Recruitment/JobQuestions?id=" & CInt(Session("JobTestID")), True)

            Catch ex As Exception
            End Try
            'If ViewState("PreviousPage") IsNot Nothing Then
            '    Response.Redirect(ViewState("PreviousPage").ToString())
            'End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


    Protected Sub cboQuestionType_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboquestiontype.SelectedIndexChanged
        Try
            Select Case cboQuestionType.SelectedItem.Text
                Case "SingleChoice"
                    divoptions.Visible = True
                    rdoAnswers.Visible = True
                    chkAnswers.Visible = False
                    answertext.Visible = False
                Case "MultipleChoice"
                    divoptions.Visible = True
                    rdoAnswers.Visible = False
                    chkAnswers.Visible = True
                    answertext.Visible = False
                Case "Text"
                    divoptions.Visible = False
                    rdoAnswers.Visible = False
                    chkAnswers.Visible = False
                    answertext.Visible = True
            End Select
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnremove_Click(sender As Object, e As EventArgs)
        Try
            imgProfile.ImageUrl = ""
            imgProfile.Width = Unit.Percentage(0)
            imgProfile.Height = Unit.Pixel(0)
            If txtid.Text <> "0" Then
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Job_Test_Questions_Image_Update", Session("JobTestID"), Nothing, "")
            End If
        Catch ex As Exception
        End Try
    End Sub


    Protected Sub btnImage_Click(sender As Object, e As EventArgs)
        Try
            'Dim lblstatus As String = ""
            'If Not file1.PostedFile Is Nothing Then
            '    Dim img_strm As Stream = file1.PostedFile.InputStream
            '    'Retrieving the length of the file to upload
            '    Dim img_len As Integer = file1.PostedFile.ContentLength
            '    'retrieving the type of the file to upload
            '    Dim strtype As String = file1.PostedFile.ContentType.ToString()
            '    Dim strname As String = Path.GetFileName(file1.PostedFile.FileName)
            '    Dim imgdata As Byte() = New Byte(img_len - 1) {}
            '    Dim n As Integer = img_strm.Read(imgdata, 0, img_len)

            '    If txtid.Text = "0" Then
            '        txtid.Text = GetImageIdentity(Session("JobTestID"), imgdata, strtype)
            '        If txtid.Text = "0" Then

            '            Exit Sub
            '        End If
            '    Else
            '        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Job_Test_Questions_Image_Update", txtid.Text, Session("JobTestID"), imgdata, strtype)
            '    End If
            '    imgProfile.ImageUrl = "~/Module/Recruitment/OnlineTest/QuestImage.ashx?imgid=" & txtid.Text

            '    lblstatus = "Photo uploaded"
            '    imgProfile.Width = Unit.Pixel(500)
            '    imgProfile.Height = Unit.Pixel(400)
            'Else
            '    lblstatus = "No photo selected for upload"
            '    file1.Focus()
            'End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
End Class