Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports System.IO


Public Class SessionQuestionsUpdate
    Inherits System.Web.UI.Page
    Dim education As New clsEducation
    Dim AuthenCode As String = "TRAINSESSION"
    Dim olddata(4) As String
    Dim options(4) As String
    Dim Separators() As Char = {","c}
    Dim answer As String = ""
    Dim lblstatus As String = ""
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

                Dim strOnline As String = "No"
                Dim testtitle As String = ""

                cboQuestionType.Items.Clear()
                cboQuestionType.Items.Add("MultipleChoice")
                cboQuestionType.Items.Add("SingleChoice")

                If strOnline = "No" Then
                    cboQuestionType.Items.Add("Text")
                End If

                Dim QuestionType As String = ""

                If Request.QueryString("id") IsNot Nothing Then
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Training_Test_Questions_Get", Request.QueryString("id"))
                    lblHeader.Text = strUser.Tables(0).Rows(0).Item("Name").ToString

                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    QuestionType = strUser.Tables(0).Rows(0).Item("QuestionType").ToString
                    answer = strUser.Tables(0).Rows(0).Item("answer").ToString
                    Process.AssignRadComboValue(cboQuestionType, QuestionType)
                    txtQuestion.Value = strUser.Tables(0).Rows(0).Item("questions").ToString
                    txtPoint.Value = strUser.Tables(0).Rows(0).Item("Points").ToString
                    lblimage.Text = strUser.Tables(0).Rows(0).Item("imgtype").ToString
                    imgProfile.ImageUrl = "TrainQuestImage.ashx?imgid=" & txtid.Text


                    If lblimage.Text = "" Then
                        imgProfile.Width = Unit.Pixel(0)
                        imgProfile.Height = Unit.Pixel(0)
                    Else
                        imgProfile.Width = Unit.Pixel(500)
                        imgProfile.Height = Unit.Pixel(400)
                    End If
                    Select Case QuestionType
                        Case "SingleChoice"
                            opt1.Visible = True
                            txtOption1.Visible = True
                            opt2.Visible = True
                            txtOption2.Visible = True
                            opt3.Visible = True
                            txtOption3.Visible = True
                            opt4.Visible = True
                            txtOption4.Visible = True
                            rdoAnswers.Visible = True
                            chkAnswers.Visible = False
                            txtAnswer.Visible = False
                            Process.RadioListCheck(rdoAnswers, answer)
                        Case "MultipleChoice"
                            opt1.Visible = True
                            txtOption1.Visible = True
                            opt2.Visible = True
                            txtOption2.Visible = True
                            opt3.Visible = True
                            txtOption3.Visible = True
                            opt4.Visible = True
                            txtOption4.Visible = True
                            rdoAnswers.Visible = False
                            chkAnswers.Visible = True
                            txtAnswer.Visible = False

                            options = answer.Split(Separators, StringSplitOptions.RemoveEmptyEntries)
                            For j = 0 To options.Length - 1
                                If options(j) IsNot Nothing Then
                                    Process.CheckboxListCheck(chkAnswers, options(j).ToString)
                                End If
                            Next

                        Case "Text"
                            opt1.Visible = False
                            txtOption1.Visible = False
                            opt2.Visible = False
                            txtOption2.Visible = False
                            opt3.Visible = False
                            txtOption3.Visible = False
                            opt4.Visible = False
                            txtOption4.Visible = False
                            rdoAnswers.Visible = False
                            chkAnswers.Visible = False
                            txtAnswer.Visible = True
                            txtAnswer.Text = answer
                    End Select

                    txtOption1.Value = strUser.Tables(0).Rows(0).Item("OptionA").ToString
                    txtOption2.Value = strUser.Tables(0).Rows(0).Item("OptionB").ToString
                    txtOption3.Value = strUser.Tables(0).Rows(0).Item("OptionC").ToString
                    txtOption4.Value = strUser.Tables(0).Rows(0).Item("OptionD").ToString
                    txtOrder.Value = strUser.Tables(0).Rows(0).Item("Ordering").ToString
                Else
                    txtOrder.Value = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Training_Test_Questions_Get_Position", Session("JobTestID"))
                    QuestionType = cboQuestionType.SelectedItem.Text
                    Select Case QuestionType
                        Case "SingleChoice"
                            opt1.Visible = True
                            txtOption1.Visible = True
                            opt2.Visible = True
                            txtOption2.Visible = True
                            opt3.Visible = True
                            txtOption3.Visible = True
                            opt4.Visible = True
                            txtOption4.Visible = True
                            rdoAnswers.Visible = True
                            chkAnswers.Visible = False
                            txtAnswer.Visible = False

                        Case "MultipleChoice"
                            opt1.Visible = True
                            txtOption1.Visible = True
                            opt2.Visible = True
                            txtOption2.Visible = True
                            opt3.Visible = True
                            txtOption3.Visible = True
                            opt4.Visible = True
                            txtOption4.Visible = True
                            rdoAnswers.Visible = False
                            chkAnswers.Visible = True
                            txtAnswer.Visible = False



                        Case "Text"
                            opt1.Visible = False
                            txtOption1.Visible = False
                            opt2.Visible = False
                            txtOption2.Visible = False
                            opt3.Visible = False
                            txtOption3.Visible = False
                            opt4.Visible = False
                            txtOption4.Visible = False
                            rdoAnswers.Visible = False
                            chkAnswers.Visible = False
                            txtAnswer.Visible = True

                    End Select
                End If

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Function GetIdentity(ByVal jobtestid As String, ByVal question As String, ByVal questiontype As String, _
                                 ByVal order As Integer, ByVal answers As String, ByVal opt11 As String, _
                                  ByVal opt12 As String, ByVal opt13 As String, ByVal opt14 As String, ByVal Points As Integer) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Training_Test_Questions_Update"
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@Questions", SqlDbType.VarChar).Value = question
            cmd.Parameters.Add("@QuestionType", SqlDbType.VarChar).Value = questiontype
            cmd.Parameters.Add("@Order", SqlDbType.Int).Value = order
            cmd.Parameters.Add("@Answer", SqlDbType.VarChar).Value = answers
            cmd.Parameters.Add("@Opt1", SqlDbType.VarChar).Value = opt11
            cmd.Parameters.Add("@Opt2", SqlDbType.VarChar).Value = opt12
            cmd.Parameters.Add("@Opt3", SqlDbType.VarChar).Value = opt13
            cmd.Parameters.Add("@Opt4", SqlDbType.VarChar).Value = opt14
            cmd.Parameters.Add("@Points", SqlDbType.Int).Value = Points
            cmd.Parameters.Add("@SessionID", SqlDbType.VarChar).Value = jobtestid
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteNonQuery()
            Return obj.ToString()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
            Return 0
        End Try
    End Function

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            If txtid.Text <> "0" Then
                If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False Then
                    ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
                    Exit Sub
                End If
            End If
            If txtQuestion.Value.Trim = "" Then
                Process.loadalert(divalert, msgalert, "Test Question required!", "danger")
                txtQuestion.Focus()
                Exit Sub
            End If

            If cboQuestionType.SelectedItem.Text.Contains("SingleChoice") Or cboQuestionType.SelectedItem.Text.Contains("MultipleChoice") Then
                If txtOption1.Value.Trim = "" Then
                    lblstatus = "Options required!"
                    Process.loadalert(divalert, msgalert, lblstatus, "danger")
                    txtOption1.Focus()
                    Exit Sub
                End If

                If txtOption2.Value.Trim = "" Then
                    lblstatus = "Options required!"
                    Process.loadalert(divalert, msgalert, lblstatus, "danger")
                    txtOption2.Focus()
                    Exit Sub
                End If
            End If

            answer = ""

            If txtid.Text.Trim = "" Then
                txtid.Text = "0"
            End If

            If txtOrder.Value.Trim <> "" Then
                If IsNumeric(txtOrder.Value) = False Then
                    lblstatus = "Question No must be numeric!"
                    Process.loadalert(divalert, msgalert, lblstatus, "danger")
                    txtOrder.Focus()
                    Exit Sub
                End If
            Else
                txtOrder.Value = "0"
            End If

            If IsNumeric(txtPoint.Value) = False Then
                lblstatus = "Point Score must be numeric!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                txtPoint.Focus()
                Exit Sub
            End If

            Select Case cboQuestionType.SelectedItem.Text
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
                    answer = txtAnswer.Text
            End Select
            'If txtid.Text = "0" And Session("JobTestID") <> "" Then
            '    txtid.Text = GetIdentity(Session("JobTestID"), txtQuestion.Value.Trim, cboQuestionType.SelectedItem.Text, txtOrder.Value, answer, txtOption1.Value, txtOption2.Value, txtOption3.Value, txtOption4.Value, txtPoint.Value)
            'Else
            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Training_Test_Questions_Update", txtQuestion.Value.Trim, cboQuestionType.SelectedItem.Text, txtOrder.Value, answer, txtOption1.Value, txtOption2.Value, txtOption3.Value, txtOption4.Value, txtPoint.Value, txtid.Text, Session("JobTestID"))
            'End If

            lblstatus = "Record saved"
            Process.loadalert(divalert, msgalert, lblstatus, "success")
        Catch ex As Exception
            lblstatus = ex.Message
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try

            If ViewState("PreviousPage") IsNot Nothing Then
                Response.Redirect(ViewState("PreviousPage").ToString())
            End If
        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "danger")
        End Try
    End Sub


    Protected Sub cboQuestionType_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboQuestionType.SelectedIndexChanged
        Try
            Select Case cboQuestionType.SelectedItem.Text
                Case "SingleChoice"
                    opt1.Visible = True
                    txtOption1.Visible = True
                    opt2.Visible = True
                    txtOption2.Visible = True
                    opt3.Visible = True
                    txtOption3.Visible = True
                    opt4.Visible = True
                    txtOption4.Visible = True
                    rdoAnswers.Visible = True
                    chkAnswers.Visible = False
                    txtAnswer.Visible = False
                Case "MultipleChoice"
                    opt1.Visible = True
                    txtOption1.Visible = True
                    opt2.Visible = True
                    txtOption2.Visible = True
                    opt3.Visible = True
                    txtOption3.Visible = True
                    opt4.Visible = True
                    txtOption4.Visible = True
                    rdoAnswers.Visible = False
                    chkAnswers.Visible = True
                    txtAnswer.Visible = False
                Case "Text"
                    opt1.Visible = False
                    txtOption1.Visible = False
                    opt2.Visible = False
                    txtOption2.Visible = False
                    opt3.Visible = False
                    txtOption3.Visible = False
                    opt4.Visible = False
                    txtOption4.Visible = False
                    rdoAnswers.Visible = False
                    chkAnswers.Visible = False
                    txtAnswer.Visible = True
            End Select
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnImage0_Click(sender As Object, e As EventArgs) Handles btnImage0.Click
        Try
            imgProfile.ImageUrl = ""
            imgProfile.Width = Unit.Percentage(0)
            imgProfile.Height = Unit.Pixel(0)
            If txtid.Text <> "0" Then
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Training_Test_Questions_Image_Update", Nothing, "")
            End If
            lblstatus = "Image deleted successfully"
            Process.loadalert(divalert, msgalert, lblstatus, "success")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Function GetImageIdentity(ByVal strImage As Byte(), ByVal strImageType As String) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Training_Test_Questions_Image_Update"
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@Photo", SqlDbType.Image).Value = strImage
            cmd.Parameters.Add("@imagetype", SqlDbType.VarChar).Value = strImageType
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception
            Process.strExp = ex.Message
            Return 0
        End Try
    End Function

    Protected Sub btnImage_Click(sender As Object, e As EventArgs) Handles btnImage.Click
        Try
            If imgUpload.HasFile AndAlso Not imgUpload.PostedFile Is Nothing Then
                Dim img_strm As Stream = imgUpload.PostedFile.InputStream
                'Retrieving the length of the file to upload
                Dim img_len As Integer = imgUpload.PostedFile.ContentLength
                'retrieving the type of the file to upload
                Dim strtype As String = imgUpload.PostedFile.ContentType.ToString()
                Dim strname As String = Path.GetFileName(imgUpload.PostedFile.FileName)
                Dim imgdata As Byte() = New Byte(img_len - 1) {}
                Dim n As Integer = img_strm.Read(imgdata, 0, img_len)

                If txtid.Text = "0" Then
                    txtid.Text = GetImageIdentity(imgdata, strtype)
                    If txtid.Text = "0" Then
                        lblstatus = Process.strExp
                        Process.loadalert(divalert, msgalert, lblstatus, "danger")
                        Exit Sub
                    End If
                Else
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Training_Test_Questions_Image_Update", Convert.ToInt64(txtid.Text), imgdata, strtype)
                End If
                imgProfile.ImageUrl = "TrainQuestImage.ashx?imgid=" & txtid.Text

                lblstatus = "Photo uploaded"
                Process.loadalert(divalert, msgalert, lblstatus, "success")
                imgProfile.Width = Unit.Pixel(500)
                imgProfile.Height = Unit.Pixel(400)
            Else
                lblstatus = "No photo selected for upload"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                imgUpload.Focus()
            End If

        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "danger")
        End Try
    End Sub
End Class