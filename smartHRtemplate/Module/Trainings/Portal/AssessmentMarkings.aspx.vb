Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports System.Web.UI
Imports Telerik.Web.UI


Public Class AssessmentMarkings
    Inherits System.Web.UI.Page
    Dim education As New clsEducation
    Dim AuthenCode As String = "JOBTEST"
    Dim olddata(4) As String
    Dim options(4) As String
    Dim Separators() As Char = {","c}

    Private Function LoadDataTable() As DataTable
        Dim datatables As New DataTable
        datatables = Process.SearchData("Emp_Training_Learning_Assessment_Get_table", Session("ApplicationTestID"))
        Return datatables
    End Function

    Private Sub LoadGrid()
        Try
            GridVwHeaderChckbox.PageIndex = CInt(Session("appfeedbacklistPageIndex"))
            GridVwHeaderChckbox.DataSource = LoadDataTable()
            GridVwHeaderChckbox.AllowSorting = True
            GridVwHeaderChckbox.AllowPaging = True
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub SortRecords(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Try
            Dim sortExpression As String = e.SortExpression
            Session("appfeedbacklistsortExpression") = sortExpression
            Dim direction As String = String.Empty
            If SortsDirection = SortDirection.Ascending Then
                SortsDirection = SortDirection.Descending
                direction = " DESC"
            Else
                SortsDirection = SortDirection.Ascending
                direction = " ASC"
            End If
            Dim table As DataTable = LoadDataTable()
            table.DefaultView.Sort = sortExpression & direction
            GridVwHeaderChckbox.PageIndex = CInt(Session("appfeedbacklistPageIndex"))
            GridVwHeaderChckbox.DataSource = table
            GridVwHeaderChckbox.DataBind()
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

    Protected Sub GridVwHeaderChckbox_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridVwHeaderChckbox.PageIndexChanging
        Try
            GridVwHeaderChckbox.PageIndex = e.NewPageIndex
            Session("appfeedbacklistPageIndex") = e.NewPageIndex
            GridVwHeaderChckbox.DataSource = LoadDataTable()
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub OnRowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GridVwHeaderChckbox, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim rbl As RadioButtonList = TryCast(e.Row.FindControl("rblwrongcorrect"), RadioButtonList)
            Dim zeros As Integer = 0
            Dim ones As Integer = 1
            If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "mark")) Then
                Dim mark As String = DataBinder.Eval(e.Row.DataItem, "mark")
                If mark = "Wrong" Then
                    rbl.Items.FindByValue(zeros.ToString()).Selected = True
                Else
                    rbl.Items.FindByValue(ones.ToString()).Selected = True
                End If
            Else
                'Failure
            End If
        End If
    End Sub

    Protected Sub btnClose_Click(sender As Object, e As EventArgs)
        Try
            If Session("objPreviousPage") = "" Then

            Else
                Response.Redirect(Session("objPreviousPage"), True)
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try

    End Sub
    Protected Sub btnSaveGrid_Click(sender As Object, e As EventArgs)
        Try

            Dim count As Integer = 0
            ' Iterate through the Products.Rows property
            For Each row As GridViewRow In GridVwHeaderChckbox.Rows
                ' Access the CheckBox
                Dim cb As RadioButtonList = row.FindControl("rblwrongcorrect")
                If cb IsNot Nothing AndAlso cb.SelectedValue IsNot Nothing Then
                    count = count + 1
                    ' First, get the ProductID for the selected row
                    'Dim ID As String = Convert.ToString(GridVwHeaderChckbox.SelectedRow.Cells(1).Text)
                    Dim ID As String = Convert.ToString(GridVwHeaderChckbox.DataKeys(row.RowIndex).Values(0))
                    Dim ID3 As String = cb.SelectedValue
                    If ID3 = "1" Then
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Training_Learning_Assessment_Marking_Update", ID, "Correct")
                    Else
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Training_Learning_Assessment_Marking_Update", ID, "Wrong")
                    End If
                End If
            Next
            Dim strCourse As New DataSet
            strCourse = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Employee_Training_Sessions_get", lblEmpSession.Text)
            Dim score As Double = 0
            Dim tscore As Double = 0
            If strCourse.Tables(0).Rows.Count > 0 Then
                score = strCourse.Tables(0).Rows(0).Item("Points").ToString
                tscore = strCourse.Tables(0).Rows(0).Item("TPoints").ToString
            End If

            lblFinalStatus.Text = "Test result successfully submitted: " & score & " out of " & tscore & " points"
            Process.loadalert(divalert, msgalert, lblFinalStatus.Text, "success")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Private Function GetIdentity(ByVal EmpTrainSessionID As Integer, ByVal Questions As String, ByVal QuestionType As String, _
                                 ByVal EmpAnswer As String, ByVal Answer As String, ByVal Ordering As Integer, ByVal OptionA As String, _
                                 ByVal OptionB As String, ByVal OptionC As String, ByVal OptionD As String, ByVal Images As String) As String
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

                Session("objPreviousPage") = Request.UrlReferrer.ToString
                rdoMarking.Items.Clear()

                Dim itemTemp As New ListItem()
                itemTemp.Text = "Correct"
                itemTemp.Value = "Correct"
                rdoMarking.Items.Add(itemTemp)

                Dim itemTemp1 As New ListItem()
                itemTemp1.Text = "Wrong"
                itemTemp1.Value = "Wrong"
                rdoMarking.Items.Add(itemTemp1)



                'btnNext.BackColor = Color.Gray
                btnPrevious.BackColor = Color.Gray
                'Button1.BackColor = Color.Gray

                Session("strttime") = System.DateTime.Now
                'btnNext.Enabled = False
                btnPrevious.Enabled = False
                rdoAnswers.Enabled = False
                chkAnswers.Enabled = False
                txtAnswers.Enabled = False
                txtRealAnswer.Enabled = False
                lblstatus.Text = ""
                ViewState("PreviousPage") = Request.UrlReferrer

                Session("ApplicationTestID") = Request.QueryString("id")
                LoadGrid()

                imgProfile.Width = Unit.Percentage(0)
                imgProfile.Height = Unit.Percentage(0)

                'Check Test

                Dim strTest As New DataSet
                strTest = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Training_Learning_Assessment_Get", Session("ApplicationTestID"), 1)
                If strTest.Tables(0).Rows.Count > 0 Then
                    lblEmpSession.Text = strTest.Tables(0).Rows(0).Item("EmpTrainSessionID").ToString
                    StartProcess()
                    LoadQuestion(Session("ApplicationTestID"), 1)
                Else
                    txtQuestion.Text = "No Learning Assessment Taken by Employee"
                    btnPrevious.Enabled = False
                    btnNext.Enabled = False
                    btnFinish.Enabled = False
                    btnPrevious.BackColor = Color.Gray
                    btnNext.BackColor = Color.Gray
                    btnFinish.BackColor = Color.Gray
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
                imgProfile.ImageUrl = strTest.Tables(0).Rows(0).Item("images").ToString
                lblimage.Text = strTest.Tables(0).Rows(0).Item("images").ToString


                If IsDBNull(strTest.Tables(0).Rows(0).Item("mark")) = True Or strTest.Tables(0).Rows(0).Item("mark").ToString.Trim = "Nil" Then
                    rdoMarking.Items(0).Selected = False
                    rdoMarking.Items(1).Selected = False
                    lblmark.Text = ""
                    lblmark.ForeColor = Color.Black
                Else
                    rdoMarking.Items(0).Selected = False
                    rdoMarking.Items(1).Selected = False
                    lblmark.Text = strTest.Tables(0).Rows(0).Item("mark").ToString.Trim

                    If lblmark.Text.ToLower.Contains("wrong") Then
                        lblmark.ForeColor = Color.Red
                    ElseIf lblmark.Text.ToLower.Contains("correct") Then
                        lblmark.ForeColor = Color.Green
                    End If

                End If

                If strTest.Tables(0).Rows(0).Item("images").ToString = "" Then
                    imgProfile.Width = Unit.Percentage(0)
                    imgProfile.Height = Unit.Percentage(0)
                Else

                    imgProfile.Width = Unit.Percentage(100)
                    imgProfile.Height = Unit.Pixel(300)
                End If

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
                lblPage.Text = lblQuestionNo.Text & " of " & txtQuestionCount.Text
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnPrevious.Click
        Try


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
                        options(i) = rdoAnswers.Items(i).Value
                        If rdoAnswers.Items(i).Selected Then
                            selectedanswer = rdoAnswers.Items(i).Value
                            Exit For
                        End If
                    Next
                Case "MultipleChoice"
                    For i = 0 To chkAnswers.Items.Count - 1
                        options(i) = chkAnswers.Items(i).Value
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

            'SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Training_Learning_Assessment_Update", txtid.Text, Session("ApplicationTestID"), txtQuestion.Text, txtQuestionType.Text, selectedanswer, txtRealAnswer.Text, lblQuestionNo.Text.Replace(".", ""), options(0), options(1), options(2), options(3), lblimage.Text)
            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Training_Learning_Assessment_Marking_Update", txtid.Text, lblmark.Text)

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
                        options(i) = rdoAnswers.Items(i).Value
                        If rdoAnswers.Items(i).Selected Then
                            selectedanswer = rdoAnswers.Items(i).Value
                            Exit For
                        End If
                    Next
                Case "MultipleChoice"
                    For i = 0 To chkAnswers.Items.Count - 1
                        options(i) = chkAnswers.Items(i).Value
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

            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Training_Learning_Assessment_Marking_Update", txtid.Text, lblmark.Text)

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

    Private Sub StartProcess()
        Try

            btnNext.Enabled = True
            btnPrevious.Enabled = False
            rdoAnswers.Enabled = True
            chkAnswers.Enabled = True

            'get quest

            Dim strTest As New DataSet
            'Prepare Test         
            Session("QuestionNo") = 1
            'strTest = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Training_Learning_Assessment_Get_All", Session("ApplicationTestID"), Session("QuestionNo"))

            Dim strCourse As New DataSet
            strCourse = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Courses_Get_SessionID", Session("ApplicationTestID"))
            If strCourse.Tables(0).Rows.Count > 0 Then
                lblHeader.Text = strCourse.Tables(0).Rows(0).Item("CourseName").ToString
                divdetailheader.InnerText = strCourse.Tables(0).Rows(0).Item("CourseName").ToString
                txtQuestionCount.Text = strCourse.Tables(0).Rows(0).Item("Counts").ToString
            End If


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
                        options(i) = rdoAnswers.Items(i).Value
                        If rdoAnswers.Items(i).Selected Then
                            selectedanswer = rdoAnswers.Items(i).Value
                            Exit For
                        End If
                    Next
                Case "MultipleChoice"
                    For i = 0 To chkAnswers.Items.Count - 1
                        options(i) = chkAnswers.Items(i).Value
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

            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Training_Learning_Assessment_Marking_Update", txtid.Text, lblmark.Text)
            MultiView1.ActiveViewIndex = 1

            Dim strCourse As New DataSet
            strCourse = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Employee_Training_Sessions_get", lblEmpSession.Text)
            Dim score As Double = 0
            Dim tscore As Double = 0
            If strCourse.Tables(0).Rows.Count > 0 Then
                score = strCourse.Tables(0).Rows(0).Item("Points").ToString
                tscore = strCourse.Tables(0).Rows(0).Item("TPoints").ToString
            End If

            lblFinalStatus.Text = "Test result successfully submitted: " & score & " out of " & tscore & " points"
        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnNext0_Click(sender As Object, e As EventArgs) Handles btnNext0.Click
        Try

            Response.Redirect("~/Module/Trainings/Settings/TrainingSessions.aspx")
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub rdoMarking_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rdoMarking.SelectedIndexChanged
        Try
            lblmark.Text = rdoMarking.SelectedValue
        Catch ex As Exception

        End Try
    End Sub
End Class