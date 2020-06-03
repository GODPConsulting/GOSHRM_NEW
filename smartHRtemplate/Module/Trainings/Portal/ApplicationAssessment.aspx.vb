Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports System.IO
Imports Telerik.Web.UI


Public Class ApplicationAssessment
    Inherits System.Web.UI.Page
    Dim education As New clsEducation
    Dim Separators() As Char = {","c}
    Dim AuthenCode As String = "EMPMYGOAL"
    Dim rowCounts As Integer = 0
    Private Function CheckWeight() As Boolean


        Return False
    End Function
    Private Sub LoadReviewee(dataid As Integer)
        Try
            'gridReviewee.DataSource = Process.SearchData("Emp_Training_Application_Assessment_Get_All", dataid)
            'gridReviewee.DataBind()
            gridReview.DataSource = Process.SearchData("Emp_Training_Application_Assessment_Get_All", dataid)
            gridReview.DataBind()

            gridManager.DataSource = Process.SearchData("Emp_Training_Application_Assessment_Get_All", dataid)
            gridManager.DataBind()
        Catch ex As Exception
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub
    Private Sub LoadQuestionaire(ByVal question As Integer, ByVal summaryID As Integer, ByVal grade As String)
        Try
            Process.LoadRadComboTextAndValueInitiate(cboempScore, "Performance_Points_Get_All", "--Select--", "pointdesc", "point")
            Process.LoadRadComboTextAndValueInitiate(cboreviewerscore, "Performance_Points_Get_All", "--Select--", "pointdesc", "point")
            Dim strUser As New DataSet
            strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Training_Application_Assessment_Get", question, summaryID, grade)
            If strUser.Tables(0).Rows.Count > 0 Then
                lblKPIType.InnerText = strUser.Tables(0).Rows(0).Item("KPIType").ToString
                lblQuestNo.InnerText = strUser.Tables(0).Rows(0).Item("rows").ToString
                lblQuestID.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                lblObjDescription.InnerText = strUser.Tables(0).Rows(0).Item("KPIObjDesc").ToString
                txtMyperformance1.Value = strUser.Tables(0).Rows(0).Item("EmpIDComment").ToString
                txtindicator.Value = strUser.Tables(0).Rows(0).Item("KPIObjectives").ToString
                txtManager1.Value = strUser.Tables(0).Rows(0).Item("SupervisorComment").ToString
                Process.AssignRadComboValue(cboreviewerscore, strUser.Tables(0).Rows(0).Item("reviewerpoint").ToString)
                Process.AssignRadComboValue(cboempScore, strUser.Tables(0).Rows(0).Item("emppoint").ToString)
            Else
                If lblbutton.Text = "next" Then
                    Process.DisableButton(btnNext)
                End If
                If lblbutton.Text = "previous" Then
                    Process.DisableButton(btnPrevious)
                End If
            End If
        Catch ex As Exception
            lblstatus.Text = ex.Message
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub
    Private Sub PopSelection()
            Try
            Session("varJobTitle") = txtJobTitle.Value
            Dim url As String = "picks?jobtitle=" & txtJobTitle.Value & "&session=" & lblEmpSessionID.Text
            Dim s As String = "window.open('" & url + "', 'popup_window', 'width=1050,height=900,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no');"
                ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)

            Catch ex As Exception
                lblstatus.Text = ex.Message
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & lblstatus.Text + "')", True)
            End Try
    End Sub
    Public Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim empSubmit As String, MgrSubmit As String, ratings As Double = 0
            If Not Me.IsPostBack Then
                Session("PreviousPage") = Request.UrlReferrer.ToString
                Process.DisableButton(btnSubmit)
                lblend.Text = "False"
                If Request.UrlReferrer.ToString.ToLower.Contains("applicationassessment") = False Then
                    Session("PreviousPage") = Request.UrlReferrer.ToString
                End If
                Process.LoadRadComboTextAndValueInitiate(cboempScore, "Performance_Points_Get_All", "--Select--", "pointdesc", "point")
                Process.LoadRadComboTextAndValueInitiate(cboreviewerscore, "Performance_Points_Get_All", "--Select--", "pointdesc", "point")
                If Request.QueryString("id") IsNot Nothing Then
                    'do actual count
                    MultiView1.ActiveViewIndex = 0
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Employee_Training_Sessions_get", Request.QueryString("id").ToString)
                    pagetitle.InnerText = strUser.Tables(0).Rows(0).Item("sessions").ToString + ": Application Assessment Form"
                    lblid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    lblEmpSessionID.Text = lblid.Text
                    txtEmpID.Value = strUser.Tables(0).Rows(0).Item("empid").ToString
                    txtName.Value = strUser.Tables(0).Rows(0).Item("empname").ToString
                    txtJobTitle.Value = strUser.Tables(0).Rows(0).Item("jobtitle").ToString
                    txtJobGrade.Value = strUser.Tables(0).Rows(0).Item("jobgrade").ToString
                    txtreviewercomment.Value = strUser.Tables(0).Rows(0).Item("reviewercomment").ToString
                    lbltrainid.Text = strUser.Tables(0).Rows(0).Item("trainingsessionid").ToString

                    If IsDBNull(strUser.Tables(0).Rows(0).Item("applicationassessmentdate")) = False Then
                        lblappdateassessment.Text = strUser.Tables(0).Rows(0).Item("applicationassessmentdate").ToString
                    Else
                        lblappdateassessment.Text = ""
                    End If

                    If IsDBNull(strUser.Tables(0).Rows(0).Item("reviewerapplicationassessmentdate")) = False Then
                        lbldateassessment.Text = strUser.Tables(0).Rows(0).Item("reviewerapplicationassessmentdate").ToString
                    Else
                        lbldateassessment.Text = ""
                    End If

                    Process.LoadRadDropDownTextAndValueP2(radReviewer, "Emp_PersonalDetail_Get_Superiors", txtJobGrade.Value, Process.GetCompanyName(""), "Employee2", "EmpID", True)
                    lblreviewer.Text = strUser.Tables(0).Rows(0).Item("SupervisorID").ToString
                    Process.AssignRadDropDownValue(radReviewer, strUser.Tables(0).Rows(0).Item("SupervisorID").ToString)

                    Dim strChecker As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Training_Application_Assessment_Count", lblEmpSessionID.Text, "")
                    lblQuestCount.Text = strChecker.Tables(0).Rows(0).Item("counts").ToString()
                    ratings = strChecker.Tables(0).Rows(0).Item("ratings").ToString()

                    If (CInt(lblQuestCount.Text) <= 0) Or (ratings < 100) Or (ratings > 100) Then
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "emp_Training_Application_Assessment_Create", lblEmpSessionID.Text)

                        strChecker = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Training_Application_Assessment_Count", lblEmpSessionID.Text, "")
                        lblQuestCount.Text = strChecker.Tables(0).Rows(0).Item("counts").ToString()
                        ratings = strChecker.Tables(0).Rows(0).Item("ratings").ToString()
                    End If

                    If CInt(lblQuestCount.Text) <= 0 Then
                        ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "No skills set for your assessment, contact your administrator!" + "')", True)
                        Process.loadalert(divalert, msgalert, "No skills set for your assessment, contact your administrator!", "warning")
                        Process.DisableButton(btnPrevious)
                        Process.DisableButton(btnNext)
                        Process.DisableButton(btnSubmit)
                        Exit Sub
                    End If
                    If ratings < 100 Or ratings > 100 Then
                        ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Weight on skill sets not complete on course, contact your administrator!" + "')", True)
                        Process.loadalert(divalert, msgalert, "Weight on skill sets not complete on course, contact your administrator!", "warning")
                        Process.DisableButton(btnPrevious)
                        Process.DisableButton(btnNext)
                        Process.DisableButton(btnSubmit)
                        Exit Sub
                    End If
                    Session("QuestionNo") = 1

                    LoadQuestionaire(Session("QuestionNo"), lblEmpSessionID.Text, txtJobTitle.Value)
                    lblcountStatus.Text = lblQuestNo.InnerText & " of " & lblQuestCount.Text

                    If txtEmpID.Value = Session("UserEmpID") Then
                        txtManager1.Disabled = True
                        txtManager1.Visible = False
                        cboreviewerscore.Visible = txtManager1.Visible
                        lblReviewerComment.Style.Add("display", "none")
                        lbloverall.Visible = False
                    Else
                        txtMyperformance1.Disabled = True
                        cboempScore.Enabled = False
                    End If
                    empSubmit = strUser.Tables(0).Rows(0).Item("applicationassessment").ToString
                    MgrSubmit = strUser.Tables(0).Rows(0).Item("reviewersubmit_appassess").ToString

                    lblmgrsubmit.Text = MgrSubmit
                    lblempsubmit.Text = empSubmit

                    If txtEmpID.Value = Session("UserEmpID") Then
                        'lnkmore.Visible = True
                        If empSubmit.ToUpper = "YES" Then
                            MultiView1.ActiveViewIndex = 1
                            lnkselfevaluation_Click(sender, e)
                            LoadReviewee(lblid.Text)
                            Process.DisableButton(btnSend2)
                            txtreviewercomment.Disabled = True
                        End If
                    ElseIf lblreviewer.Text = Session("UserEmpID") Then
                        lnkmore.Visible = False
                        radReviewer.Enabled = False
                        If empSubmit.ToUpper = "NO" Then
                            MultiView1.ActiveViewIndex = 2
                            Process.loadalert(divalert, msgalert, "Employee has not submitted Assessment for review!", "warning")
                            txtManager1.Disabled = True
                            cboreviewerscore.Enabled = False
                            Process.DisableButton(btnSubmit)
                            Process.DisableButton(btnNext)
                            Process.DisableButton(btnPrevious)
                            Exit Sub
                        End If

                        If MgrSubmit.ToUpper = "YES" Then
                            MultiView1.ActiveViewIndex = 1
                            lnkmanagerevaluation_Click(sender, e)
                            LoadReviewee(lblid.Text)
                            Process.DisableButton(btnSend2)
                            txtreviewercomment.Disabled = True
                        End If
                    End If

                End If
            End If

        Catch ex As Exception
             Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        If Session("PreviousPage") IsNot Nothing Then
            Response.Redirect(Session("PreviousPage"))
        Else
            Response.Redirect(Session("PreviousPage"))
        End If
    End Sub
    Private Function UpdateAssessment(ByVal sid As Integer, ByVal emptrainid As Integer, ByVal skpitype As String,
                                 ByVal skpiobj As String, ByVal empcomment As String, ByVal reviewercomment As String, reviewpoint As String, emppoint As String) As String
        Try
            'SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Training_Application_Assessment_Update", lblid.Text, lblEmpSessionID.Text, lblKPIType.Text, radIndicator.SelectedValue, txtMyperformance1.Text.Trim, txtManager1.Text.Trim)
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Emp_Training_Application_Assessment_Update"
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = sid
            cmd.Parameters.Add("@EmpTrainSessionID", SqlDbType.Int).Value = emptrainid
            cmd.Parameters.Add("@Kpitype", SqlDbType.VarChar).Value = skpitype
            cmd.Parameters.Add("@KPIObjectives", SqlDbType.VarChar).Value = skpiobj
            cmd.Parameters.Add("@EmpComment", SqlDbType.VarChar).Value = empcomment
            cmd.Parameters.Add("@ReviewerComment", SqlDbType.VarChar).Value = reviewercomment
            cmd.Parameters.Add("@reviewerid", SqlDbType.VarChar).Value = radReviewer.SelectedValue
            cmd.Parameters.Add("@reviewerpoint", SqlDbType.VarChar).Value = cboreviewerscore.SelectedValue
            cmd.Parameters.Add("@emppoint", SqlDbType.VarChar).Value = cboempScore.SelectedValue
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception
            lblstatus.Text = ex.Message
            Return 0
        End Try
    End Function


    Private Sub btnPrevious_Click(sender As Object, e As EventArgs) Handles btnPrevious.Click
        Try
            Session("QuestionNo") = CInt(Session("QuestionNo")) - 1
            If CInt(Session("QuestionNo")) < CInt(lblQuestCount.Text) Then
                btnNext.Enabled = True
                btnNext.BackColor = Color.FromArgb(102, 153, 0)
            End If

            If CInt(Session("QuestionNo")) <= 0 Then
                Session("QuestionNo") = CInt(Session("QuestionNo")) + 1

                Process.DisableButton(btnPrevious)
                Process.EnableButton(btnNext)
            Else
                lblid0.Text = UpdateAssessment(lblQuestID.Text, lblEmpSessionID.Text, lblKPIType.InnerText, txtindicator.Value, txtMyperformance1.Value.Trim, txtManager1.Value.Trim, cboreviewerscore.SelectedValue, cboempScore.SelectedValue)
                If lblid0.Text = 0 Then
                    Exit Sub
                Else
                    If lblid.Text = "0" Then
                        lblid.Text = lblid0.Text
                    End If
                End If


                If CInt(lblQuestNo.InnerText) = 1 Then
                    Process.DisableButton(btnPrevious)
                    Process.EnableButton(btnNext)
                Else
                    Process.EnableButton(btnPrevious)
                End If
                LoadQuestionaire(CInt(Session("QuestionNo")), lblEmpSessionID.Text, txtJobTitle.Value)

            End If
            'Process.EnableButton(btnSave)
            lblcountStatus.Text = lblQuestNo.InnerText & " of " & lblQuestCount.Text
            lblstatus.Text = ""
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        Try
            lblbutton.Text = "next"
            Session("QuestionNo") = CInt(Session("QuestionNo")) + 1


            If CInt(Session("QuestionNo")) > CInt(lblQuestCount.Text) Then
                Process.DisableButton(btnNext)
                Process.EnableButton(btnPrevious)
                LoadQuestionaire(CInt(lblQuestNo.InnerText), lblEmpSessionID.Text, txtJobTitle.Value)

                If lblreviewer.Text = Session("UserEmpID") Then
                    If CInt(Session("QuestionNo")) >= CInt(lblQuestCount.Text) Then
                        Process.DisableButton(btnNext)
                    End If
                End If
            Else

                lblid0.Text = UpdateAssessment(lblQuestID.Text, lblEmpSessionID.Text, lblKPIType.InnerText, txtindicator.Value, txtMyperformance1.Value.Trim, txtManager1.Value.Trim, cboreviewerscore.SelectedValue, cboempScore.SelectedValue)
                If lblid0.Text = 0 Then
                    Exit Sub
                Else
                    If lblid.Text = "0" Then
                        lblid.Text = lblid0.Text
                    End If
                End If

                If CInt(Session("QuestionNo")) = CInt(lblQuestCount.Text) Then
                    Process.EnableButton(btnPrevious)
                    Process.DisableButton(btnNext)
                    Process.EnableButton(btnSubmit)
                    lblend.Text = "True"
                Else

                    Process.EnableButton(btnNext)
                    Process.EnableButton(btnPrevious)
                End If
                LoadQuestionaire(CInt(Session("QuestionNo")), lblEmpSessionID.Text, txtJobTitle.Value)
            End If
            'Process.EnableButton(btnSave)

            lblcountStatus.Text = lblQuestNo.InnerText & " of " & lblQuestCount.Text
            lblstatus.Text = ""
        Catch ex As Exception
            lblstatus.Text = ex.Message
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Try
            MultiView1.ActiveViewIndex = 1
            radReviewer.Enabled = True
            'LoadReviewee
            lblid0.Text = UpdateAssessment(lblQuestID.Text, lblEmpSessionID.Text, lblKPIType.InnerText, txtindicator.Value, txtMyperformance1.Value.Trim, txtManager1.Value.Trim, cboreviewerscore.SelectedValue, cboempScore.SelectedValue)
            If txtEmpID.Value = Session("UserEmpID") Then
                lnkselfevaluation_Click(sender, e)
                LoadReviewee(lblid.Text)
                txtreviewercomment.Visible = False
                lnkmanagerevaluation.Visible = False
                lnkselfevaluation.Visible = False
            ElseIf lblreviewer.Text = Session("UserEmpID") Then
                lnkmanagerevaluation_Click(sender, e)
                LoadReviewee(lblid.Text)
                txtreviewercomment.Visible = True
                lnkmanagerevaluation.Visible = False
                lnkselfevaluation.Visible = False
            End If
            lblstatus.Text = ""
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnSend2.Click
        Try
            Dim confirmValue As String = Request.Form("confirm_value")
            If confirmValue = "Yes" Then
                If txtEmpID.Value = Session("UserEmpID") Then
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Employee_Training_Sessions_Submit", lblid.Text, radReviewer.SelectedValue)
                    Process.Training_Assessment_Complete("Application", pagetitle.InnerText, Session("userempid"), Process.GetEmployeeData(Session("userempid"), "linemanagerid"), Process.ApplicationURL + "/" + "Module/Trainings/Portal/TrainingParticipants?id=" + lbltrainid.Text, Process.ApplicationURL + "/" + Process.GetMailLink(AuthenCode, 1))
                ElseIf lblreviewer.Text = Session("UserEmpID") Then
                    If txtreviewercomment.Value.Trim = "" Then
                        lblstatus.Text = "Your overall impression is required!"
                        ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & lblstatus.Text + "')", True)
                        txtreviewercomment.Focus()
                        Exit Sub
                    Else
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Employee_Training_Sessions_Reviewer_Submit", lblid.Text, txtreviewercomment.Value)
                        Process.Training_Assessment_Review_Complete("Application", pagetitle.InnerText, txtEmpID.Value, lblreviewer.Text, Process.ApplicationURL + "Module/Trainings/Portal/TrainingParticipants?id=" + lbltrainid.Text)
                    End If
                End If
                btnSend2.Enabled = False
                btnSend2.BackColor = Color.Gray
                MultiView1.ActiveViewIndex = 2
                lblstatus.Text = ""
            Else
                lblstatus.Text = "Submit cancelled"
            End If
            radReviewer.Enabled = False
        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Try
            If txtEmpID.Value = Session("UserEmpID") Then
                If lblempsubmit.Text.ToUpper = "YES" Then
                    Response.Redirect(Session("PreviousPage"))
                Else
                    MultiView1.ActiveViewIndex = 0
                    'gridReviewee.Visible = False
                    gridReview.Visible = False
                    gridManager.Visible = False
                End If
            Else
                If lblmgrsubmit.Text.ToUpper = "YES" Then
                    Response.Redirect(Session("PreviousPage"))
                Else
                    MultiView1.ActiveViewIndex = 0
                    'gridReviewee.Visible = False
                    gridReview.Visible = False
                    gridManager.Visible = False
                End If
            End If

        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub
    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles lnkmore.Click
        Try
            PopSelection()
        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkselfevaluation_Click(sender As Object, e As EventArgs) Handles lnkselfevaluation.Click
        Try
            summaryheader.InnerText = "Employee Self Assessment"
            If lblappdateassessment.Text <> "" Then
                dateheader.InnerText = "Submitted on " & lblappdateassessment.Text
            End If

            gridReview.Visible = True
            gridManager.Visible = False

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkmanagerevaluation_Click(sender As Object, e As EventArgs) Handles lnkmanagerevaluation.Click
        Try
            summaryheader.InnerText = "Manager's Assessment"
            If lblappdateassessment.Text <> "" Then
                dateheader.InnerText = "Submitted on " & lbldateassessment.Text
            End If
            gridReview.Visible = False
            gridManager.Visible = True
        Catch ex As Exception

        End Try
    End Sub
End Class