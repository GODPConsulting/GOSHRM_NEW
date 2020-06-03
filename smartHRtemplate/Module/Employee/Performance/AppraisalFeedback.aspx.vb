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


Public Class AppraisalFeedback
    Inherits System.Web.UI.Page
    Dim education As New clsEducation
    Dim Separators() As Char = {","c}
    Dim AuthenCode As String = "EMPAPPRAISAL"
    Dim rowCounts As Integer = 0
    Dim success, obj, kpitype As String

    Private Sub LoadReviewee(dataid As Integer)
        Try
            gridReviewee.DataSource = Process.SearchData("Performance_Appraisal_Get_All", dataid)
            'Getting custom Names
            success = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select SuccessTarget from Performance_Custom_Naming")
            obj = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select Objectives from Performance_Custom_Naming")
            KPIType = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select KPIType from Performance_Custom_Naming")

            If KPIType = "" Then
                gridReviewee.Columns(0).HeaderText = "KPI Type"
            Else
                gridReviewee.Columns(0).HeaderText = kpitype
            End If
            If obj = "" Then
                gridReviewee.Columns(1).HeaderText = "Objectives"
            Else
                gridReviewee.Columns(1).HeaderText = obj
            End If
            If success = "" Then
                gridReviewee.Columns(2).HeaderText = "Success Target"
            Else
                gridReviewee.Columns(2).HeaderText = success
            End If

            gridReviewee.DataBind()
        Catch ex As Exception

        End Try
    End Sub


    Private Sub LoadQuestionaire(ByVal question As Integer, ByVal summaryID As Integer, ByVal grade As String)
        Try
            Dim strUser As New DataSet
            strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Appraisal_Questionaire_Get", question, summaryID, grade)
            If strUser.Tables(0).Rows.Count > 0 Then
                Process.LoadRadioButtonsDb(rdoMyRatings, "Performance_Points_Get_All", "PointName", "point", "pointdescription")
                Process.LoadRadioButtonsDb(rdoMgrRatings, "Performance_Points_Get_All", "PointName", "point", "pointdescription")
                Process.LoadRadioButtonsDb(rdoMgrRatings2, "Performance_Points_Get_All", "PointName", "point", "pointdescription")

                ' Process.LoadRadioButtonsDb(rdoMyRatings, "Performance_Points_Get_All", "pointname", "point", "pointdescription")

                akpitype.InnerText = strUser.Tables(0).Rows(0).Item("KPIType").ToString
                aQuestNo.InnerText = strUser.Tables(0).Rows(0).Item("rows").ToString
                lblQuestID.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                aObjective.Value = strUser.Tables(0).Rows(0).Item("kpiobjectives").ToString
                'aObjDesc.InnerText = strUser.Tables(0).Rows(0).Item("KPIObjDesc").ToString.Replace(vbCrLf, "<br />")
                aObjDesc.InnerText = strUser.Tables(0).Rows(0).Item("objectives").ToString
                'aMyObjective.InnerText = strUser.Tables(0).Rows(0).Item("AppraisalItem").ToString.Replace(vbCrLf, "<br />-")
                aMyObjective.InnerText = strUser.Tables(0).Rows(0).Item("comment").ToString
                aMyPerformance.Value = strUser.Tables(0).Rows(0).Item("EmpIDComment").ToString
                Process.RadioListCheck(rdoMyRatings, strUser.Tables(0).Rows(0).Item("empratingdesc").ToString)
                lblMyRating.Text = strUser.Tables(0).Rows(0).Item("empIDRating").ToString

                amanager1.Value = strUser.Tables(0).Rows(0).Item("SupervisorComment").ToString
                Process.RadioListCheck(rdoMgrRatings, strUser.Tables(0).Rows(0).Item("mgrratingdesc").ToString)
                lblMgrRating.Text = strUser.Tables(0).Rows(0).Item("MgrIDRating").ToString

                amanager2.Value = strUser.Tables(0).Rows(0).Item("SupervisorComment2").ToString
                Process.RadioListCheck(rdoMgrRatings2, strUser.Tables(0).Rows(0).Item("mgrratingdesc2").ToString)
                lblMgrRating2.Text = strUser.Tables(0).Rows(0).Item("MgrIDRating2").ToString

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                Session("objPreviousPage") = Request.UrlReferrer.ToString
                Dim empSubmit As String, MgrSubmit As String, MgrSubmit2 As String, cyclestat As String
                Dim customName As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select AppraisalFeedback from Performance_Custom_Naming")
                If (customName = "") Then
                    pagetitle.InnerText = "Appraisal Feedback Form"
                Else
                    pagetitle.InnerText = customName + " Form"
                End If
                btSubmit.Disabled = True
                'Process.DisableButton(btnSubmit)
                lblend.Text = "False"
                Dim lblstatus As String = ""
                If Request.QueryString("id") IsNot Nothing Then
                    Button4.Visible = False
                    Dim reviewer1Visible As String = "NO"
                    Dim reviewer2Visible As String = "NO"
                    Dim straccess As New DataSet
                    straccess = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Appraisal_Preference_Get", Session("Organisation"))
                    If straccess.Tables(0).Rows.Count > 0 Then
                        reviewer1Visible = straccess.Tables(0).Rows(0).Item("ReviewerVisible").ToString.ToUpper
                        reviewer2Visible = straccess.Tables(0).Rows(0).Item("RevieweriiVisible").ToString.ToUpper
                    End If
                    lblvisibleI.Text = reviewer1Visible
                    lblvisibleII.Text = reviewer2Visible
                    'do actaul count
                    aratingdesc.InnerText = ""
                    Dim ratedesc As String = ""
                    Dim strpoint As New DataSet
                    strpoint = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Points_Get_All")
                    If strpoint.Tables(0).Rows.Count > 0 Then
                        For k As Integer = 0 To strpoint.Tables(0).Rows.Count - 1
                            aratingdesc.InnerText = aratingdesc.InnerText & "   " & strpoint.Tables(0).Rows(k).Item("pointdesc").ToString
                            If k = 0 Then
                                ratedesc = strpoint.Tables(0).Rows(k).Item("pointdescription").ToString
                            Else
                                ratedesc = ratedesc & vbNewLine & vbNewLine & strpoint.Tables(0).Rows(k).Item("pointdescription").ToString
                            End If

                        Next
                        aratingdesc.InnerText = aratingdesc.InnerText.Trim
                        aratingdesc.Attributes.Add("title", ratedesc)
                        'aratingdesc.InnerHtml = ratedesc
                    End If

                    MultiView1.ActiveViewIndex = 0
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Appraisal_Summary_Get", Request.QueryString("id").ToString)
                    lblid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    reviewyear.Value = strUser.Tables(0).Rows(0).Item("reviewyear").ToString
                    txtEmpID.Text = strUser.Tables(0).Rows(0).Item("empid").ToString
                    aname.Value = strUser.Tables(0).Rows(0).Item("empname").ToString
                    ajobtitle.Value = strUser.Tables(0).Rows(0).Item("jobtitle").ToString
                    ajobgrade.Value = strUser.Tables(0).Rows(0).Item("jobgrade").ToString
                    txtdept.Text = strUser.Tables(0).Rows(0).Item("dept").ToString

                    reviewstart.Value = Process.DDMONYYYY(CDate(strUser.Tables(0).Rows(0).Item("StartPeriod")))
                    reviewend.Value = Process.DDMONYYYY(CDate(strUser.Tables(0).Rows(0).Item("EndPeriod")))
                    MrgEndcycle.Text = strUser.Tables(0).Rows(0).Item("MrgEndCycle").ToString

                    apresentposition.Value = strUser.Tables(0).Rows(0).Item("MthInPosition").ToString
                    alenofservice.Value = strUser.Tables(0).Rows(0).Item("MthInService")
                    txtlocation.Text = strUser.Tables(0).Rows(0).Item("location").ToString

                    aoverdesc.Value = strUser.Tables(0).Rows(0).Item("gradename").ToString
                    If aoverdesc.Value.Trim = "" Then
                        divoverdesc.Visible = False
                    End If

                    lblreviewer.Text = strUser.Tables(0).Rows(0).Item("CoachID").ToString
                    lblrevieweremail.Text = strUser.Tables(0).Rows(0).Item("coachemail").ToString
                    areviewer1.Value = strUser.Tables(0).Rows(0).Item("CoachName").ToString
                    'Reviewer 2
                    lblreviewer2.Text = strUser.Tables(0).Rows(0).Item("SupervisorID2").ToString
                    areviewer2.Value = strUser.Tables(0).Rows(0).Item("supervisor2name2").ToString

                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_Questionaire_Count", lblid.Text, ajobgrade.Value)
                    'lblQuestCount.Text = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Performance_Appraisal_Questionaire_Count", lblid.Text, ajobgrade.Value )
                    lblQuestCount.Text = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Performance_Appraisal_Questionaire_Count_Actual", lblid.Text, ajobgrade.Value)
                    Session("QuestionNo") = 1

                    If lblQuestCount.Text > 0 Then
                        LoadQuestionaire(Session("QuestionNo"), lblid.Text, ajobgrade.Value)
                        apageview.InnerText = "1 of " & lblQuestCount.Text
                    Else
                        aQuestNo.InnerText = "0."
                        lblstatus = "No Objectives set for " & ajobgrade.Value
                        Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    End If

                    cyclestat = strUser.Tables(0).Rows(0).Item("cyclestat").ToString
                    If cyclestat.ToLower = "open" Then
                        Dim str360 As New DataSet
                        str360 = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Appraisal_360_Check", ajobgrade.Value)
                        If str360.Tables(0).Rows.Count > 0 Then
                            bt360degree.Visible = True
                        Else
                            bt360degree.Visible = False
                        End If
                    Else
                        bt360degree.Visible = False
                    End If

                    Dim actualcount As Integer = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Performance_Appraisal_Questionaire_Count_Actual", lblid.Text, ajobgrade.Value)



                    If txtEmpID.Text = Session("UserEmpID") Then

                        divrecommendation.Visible = False
                        Dim unanswered As Integer = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Performance_Appraisal_Questionaire_Reviewee_Unanswered", lblid.Text)
                        If actualcount = CInt(lblQuestCount.Text) And unanswered = 0 Then
                            'Process.EnableButton(btSubmit)
                            btSubmit.Visible = True
                        End If
                    ElseIf lblreviewer.Text = Session("UserEmpID") Then
                        divrecommendation.Visible = True
                        Dim unanswered As Integer = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Performance_Appraisal_Questionaire_Reviewer_Unanswered", lblid.Text)
                        If actualcount = CInt(lblQuestCount.Text) And unanswered = 0 Then
                            'Process.EnableButton(btnSubmit)
                            btSubmit.Visible = True
                        End If
                    ElseIf lblreviewer2.Text = Session("UserEmpID") Then
                        divrecommendation.Visible = True
                        Dim unanswered As Integer = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Performance_Appraisal_Questionaire_Reviewer_Unanswered", lblid.Text)
                        If actualcount = CInt(lblQuestCount.Text) And unanswered = 0 Then
                            'Process.EnableButton(btnSubmit)
                            btSubmit.Visible = True
                        End If
                    End If


                    If txtEmpID.Text = Session("UserEmpID") Then
                        divreviewer1.Visible = False

                        divreviewer2.Visible = False
                    Else
                        aMyPerformance.Disabled = True
                        rdoMyRatings.Enabled = False


                    End If
                    empSubmit = strUser.Tables(0).Rows(0).Item("empsubmited").ToString
                    MgrSubmit = strUser.Tables(0).Rows(0).Item("mgrsubmited").ToString
                    MgrSubmit2 = strUser.Tables(0).Rows(0).Item("mgrsubmited2").ToString

                    If txtEmpID.Text = Session("UserEmpID") Then
                        divreviewer1.Style.Add("display", "none")
                        divreviewer2.Style.Add("display", "none")
                        If empSubmit.ToUpper.Trim = "YES" Or MgrSubmit.ToUpper.Trim = "YES" Then
                            lblstatus = "Feedback form has already been submitted!"
                            Process.loadalert(divalert, msgalert, lblstatus, "info")
                            MultiView1.ActiveViewIndex = 1
                            gridReviewee.Visible = True
                            LoadReviewee(lblid.Text)
                            Button3.Visible = False

                            Process.DisableButton(btSubmitReview)
                            btback.Visible = False
                            btSubmitReview.Visible = False

                        End If
                    ElseIf lblreviewer.Text = Session("UserEmpID") Then
                        'emp_app.Style.Add("display", "none")
                        divreviewer2.Style.Add("display", "none")
                        amanager2.Disabled = True
                        rdoMgrRatings2.Enabled = False
                        btnsubmit.Visible = True
                        'btnsubmit.Style.Add("display", "block")
                        'If empSubmit.ToUpper.Trim = "NO" Then

                        '    MultiView1.ActiveViewIndex = 2
                        '    H1.InnerText = "Employee has not submitted Appraisal for review"
                        '    amanager1.Disabled = True
                        '    rdoMgrRatings.Enabled = False
                        '    btSubmit.Disabled = True
                        '    btnext.Disabled = True
                        '    btprevious.Disabled = True

                        '    'Process.DisableButton(btnSubmit)
                        '    'Process.DisableButton(btnNext)
                        '    'Process.DisableButton(btnPrevious)
                        '    Exit Sub
                        'End If
                        Process.AssignRadComboValue(cborecommendation, strUser.Tables(0).Rows(0).Item("MgrRecommendation").ToString)

                        If MgrSubmit.ToUpper.Trim = "YES" Then

                            MultiView1.ActiveViewIndex = 1
                            gridReviewee.Visible = True
                            LoadReviewee(lblid.Text)
                            btSubmitReview.Enabled = False
                            btback.Disabled = True
                            'Process.DisableButton(btnSubmitReview)
                            'Process.DisableButton(btnBack)
                            btback.Visible = False
                            btSubmitReview.Visible = False
                            cborecommendation.Enabled = False

                        End If
                    ElseIf lblreviewer2.Text = Session("UserEmpID") Then
                        'emp_app.Style.Add("display", "none")
                        'divreviewer1.Style.Add("display", "none")
                        Button3.Visible = False
                        Button4.Visible = True
                        btnsubmit.Visible = False
                        btnrefresh.Visible = False
                        Button6.Visible = False
                        Button5.Visible = False
                        Button1.Visible = False

                        Process.AssignRadComboValue(cborecommendation, strUser.Tables(0).Rows(0).Item("MgrRecommendation2").ToString)
                        amanager1.Disabled = True
                        rdoMgrRatings.Enabled = False
                        btnDisagree.Visible = True
                        'reviewee yet to submit review
                        If empSubmit.ToUpper.Trim = "NO" Then
                            MultiView1.ActiveViewIndex = 2

                            H1.InnerText = "Employee has not submitted Appraisal for review"
                            amanager1.Disabled = True
                            rdoMgrRatings.Enabled = False
                            btSubmit.Disabled = True
                            btnext.Disabled = True
                            btprevious.Disabled = True
                            'Process.DisableButton(btnSubmit)
                            'Process.DisableButton(btnNext)
                            'Process.DisableButton(btnPrevious)
                            btnDisagree.Visible = False
                            Exit Sub
                        End If

                        'Reviewer yet to submit review
                        If MgrSubmit.ToUpper.Trim = "NO" Then
                            btnDisagree.Visible = False
                            MultiView1.ActiveViewIndex = 2

                            H1.InnerText = "First Reviewer has not submitted Appraisal for 2nd review"
                            amanager1.Disabled = True
                            rdoMgrRatings.Enabled = False

                            btSubmit.Disabled = True
                            btnext.Disabled = True
                            btprevious.Disabled = True
                            'Process.DisableButton(btnSubmit)
                            'Process.DisableButton(btnNext)
                            'Process.DisableButton(btnPrevious)
                            Exit Sub
                        End If

                        If MgrSubmit2.ToUpper = "YES" Then

                            MultiView1.ActiveViewIndex = 1
                            gridReviewee.Visible = True
                            LoadReviewee(lblid.Text)

                            btSubmitReview.Enabled = False
                            btback.Visible = False
                            'Process.DisableButton(btnSubmitReview)

                            btSubmitReview.Visible = False
                            cborecommendation.Enabled = False
                        End If
                    End If
                    'Performance checks
                    'check weight
                    Dim strValid As String = ""
                    Dim strWeight As New DataSet
                    strWeight = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Appraisal_Total_Check", lblid.Text)
                    If strWeight.Tables(0).Rows.Count > 0 Then
                        For h As Integer = 0 To strWeight.Tables(0).Rows.Count - 1
                            If CDbl(strWeight.Tables(0).Rows(h).Item("weights").ToString) > 100 Then
                                strValid = "excess"
                            ElseIf CDbl(strWeight.Tables(0).Rows(h).Item("weights").ToString) < 100 Then
                                strValid = "incomplete"
                            End If
                        Next
                    Else
                        strValid = "incomplete"
                    End If

                    If strValid = "incomplete" Or strValid = "excess" Then
                        lblstatus = "Objective weight not properly configured!"
                        Process.loadalert(divalert, msgalert, lblstatus, "warning")
                        btprevious.Disabled = True
                        btnext.Disabled = True
                        'btsave.Disabled = True
                        btSubmit.Disabled = True
                        'Process.DisableButton(btnPrevious)
                        'Process.DisableButton(btnNext)
                        'Process.DisableButton(btnSave)
                        'Process.DisableButton(btnSubmit)
                        Exit Sub
                    End If

                    If strUser.Tables(0).Rows(0).Item("completed").ToString.ToLower <> "yes" Then
                        lblstatus = "Feedback cannot commence if Objectives setup haven't been completed!"
                        Process.loadalert(divalert, msgalert, lblstatus, "danger")
                        'Process.DisableButton(btnPrevious)
                        'Process.DisableButton(btnNext)
                        'Process.DisableButton(btnSave)
                        'Process.DisableButton(btnSubmit)
                        aMyPerformance.Disabled = True
                        btprevious.Disabled = True
                        btnext.Disabled = True
                        'btsave.Disabled = True
                        btSubmit.Disabled = True
                        Exit Sub
                    End If

                    If strUser.Tables(0).Rows(0).Item("empsubmited").ToString.ToLower.Contains("yes") = True Then
                        'Process.DisableButton(btnPrevious)
                        'Process.DisableButton(btnNext)
                        'Process.DisableButton(btnSave)
                        'Process.DisableButton(btnSubmit)
                        'btback.Visible = False
                        'btSubmitReview.Visible = False
                        'btSubmitReview.Enabled = False
                        'MultiView1.ActiveViewIndex = 1
                        'divrecommendation.Visible = False
                        'questionaira.Visible = False
                        'gridsss.Visible = True
                        'gridReviewee.Visible = True
                        'LoadReviewee(lblid.Text)
                        'btprevious.Disabled = True
                        'btnext.Disabled = True
                        'btsave.Disabled = True
                        'btSubmit.Disabled = True
                        Exit Sub
                    End If

                    If strUser.Tables(0).Rows(0).Item("CoachApprovalStatus").ToString.ToLower <> "discussed & agreed" Then
                        lblstatus = "Feedback cannot commence if Objectives haven't been discussed and agreed!"
                        Process.loadalert(divalert, msgalert, lblstatus, "warning")

                        btprevious.Disabled = True
                        btnext.Disabled = True
                        'btsave.Disabled = True
                        btSubmit.Disabled = True
                        Exit Sub
                    End If
                End If
            End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Protected Sub btnClose_Click(sender As Object, e As EventArgs)
        Try
            If lblid.Text <> "0" And lblid.Text <> "" Then
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_Questionaire_Update", lblQuestID.Text, lblid.Text, akpitype.InnerText, aObjective.Value, aMyObjective.InnerText, aMyPerformance.Value, lblMyRating.Text, amanager1.Value, lblMgrRating.Text, ajobgrade.Value, amanager2.Value.Trim, lblMgrRating2.Text)
            End If

            If Session("objPreviousPage") = "" Then
                If lblreviewer.Text = Session("UserEmpID") Then
                    Response.Redirect("~/Module/Employee/Performance/DirectReportAppraisalObjectivesForm", True)
                ElseIf lblreviewer2.Text = Session("UserEmpID") Then
                    Response.Redirect("~/Module/Employee/Performance/SecondRevewAppraisalObjectivesForm", True)
                Else
                    Response.Redirect("~/Module/Employee/Performance/AppraisalFeedBackList.aspx", True)
                End If
            Else
                Response.Redirect(Session("objPreviousPage"), True)
            End If


        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try

    End Sub

    Protected Sub btnContinue_Click(sender As Object, e As EventArgs)
        Try
            If ajobgrade.Value = "" Or ajobtitle.Value = "" Then
                Process.loadalert(divalert, msgalert, "Please select an Employee", "info")
                Exit Sub
            End If
            nuggetsquestion.Style.Add("display", "block")
            reviewerdetails.Style.Add("display", "none")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Protected Sub btnEndcycle(sender As Object, e As EventArgs)
        Try
            SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Performance_Appraisal_End_Cycle", lblid.Text)
            Process.Appraisal_Cycle_End(txtEmpID.Text, "", aname.Value, txtdept.Text, Process.ApplicationURL & "/" & Process.GetMailLink(AuthenCode, 0))
            Process.loadalert(divalert, msgalert, "Cycle Ended Successfully", "success")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try

    End Sub


    Protected Sub rdoMyRatings_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rdoMyRatings.SelectedIndexChanged
        Try
            lblMyRating.Text = rdoMyRatings.SelectedValue
            If lblend.Text = "True" Then
                btSubmit.Disabled = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub rdoMgrRatings_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rdoMgrRatings.SelectedIndexChanged
        Try
            lblMgrRating.Text = rdoMgrRatings.SelectedValue
            If lblend.Text = "True" Then
                btSubmit.Disabled = False
                'Process.EnableButton(btnSubmit)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnPrevious_Click(sender As Object, e As EventArgs)
        Try
                Session("QuestionNo") = CInt(Session("QuestionNo")) - 1
                If CInt(Session("QuestionNo")) < CInt(lblQuestCount.Text) Then
                    'Process.EnableButton(btnNext)
                    btnext.Disabled = False
                End If

                If CInt(Session("QuestionNo")) <= 0 Then
                    Session("QuestionNo") = CInt(Session("QuestionNo")) + 1
                    'Process.DisableButton(btnPrevious)
                    'Process.EnableButton(btnNext)
                    btprevious.Disabled = True
                    btnext.Disabled = False
                Else
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_Questionaire_Update", lblQuestID.Text, lblid.Text, akpitype.InnerText, aObjective.Value, aMyObjective.InnerText, aMyPerformance.Value, lblMyRating.Text, amanager1.Value, lblMgrRating.Text, ajobgrade.Value, amanager2.Value.Trim, lblMgrRating2.Text)
                    If Session("QuestionNo") = 1 Then
                        btprevious.Disabled = True
                        btnext.Disabled = False
                    Else
                        btprevious.Disabled = False
                    End If
                    LoadQuestionaire(Session("QuestionNo"), lblid.Text, ajobgrade.Value)
                End If
                apageview.InnerText = aQuestNo.InnerText & " of " & lblQuestCount.Text
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Protected Sub btnNext_Click(sender As Object, e As EventArgs)
        Try

            Session("QuestionNo") = CInt(Session("QuestionNo")) + 1
            If CInt(Session("QuestionNo")) > CInt(lblQuestCount.Text) Then
                Session("QuestionNo") = CInt(Session("QuestionNo")) - 1
                btnext.Disabled = True
                btprevious.Disabled = False
                'Process.DisableButton(btnNext)
                'Process.EnableButton(btnPrevious)
            Else
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_Questionaire_Update", lblQuestID.Text, lblid.Text, akpitype.InnerText, aObjective.Value, aMyObjective.InnerText, aMyPerformance.Value, lblMyRating.Text, amanager1.Value.Trim, lblMgrRating.Text, ajobgrade.Value, amanager2.Value.Trim, lblMgrRating2.Text)
                If CInt(Session("QuestionNo")) = CInt(lblQuestCount.Text) Then
                    btnext.Disabled = True
                    btprevious.Disabled = False
                    btSubmit.Disabled = False

                    'Process.DisableButton(btnNext)
                    'Process.EnableButton(btnPrevious)
                    'Process.EnableButton(btnSubmit)
                    lblend.Text = "True"
                Else
                    'Process.EnableButton(btnNext)
                    'Process.EnableButton(btnPrevious)
                    btprevious.Disabled = False
                    btnext.Disabled = False

                End If
                LoadQuestionaire(Session("QuestionNo"), lblid.Text, ajobgrade.Value)
            End If
            apageview.InnerText = aQuestNo.InnerText & " of " & lblQuestCount.Text
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs)
        Try
            Dim lblstatus As String = ""
            Dim d1 As Date = Convert.ToDateTime(reviewend.Value)
            Dim d2 As Date = Convert.ToDateTime(Now.Date)
            If ((d1 > d2)) Then
                If MrgEndcycle.Text.ToLower = "yes" Then
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_Questionaire_Update", lblQuestID.Text, lblid.Text, akpitype.InnerText, aObjective.Value, aMyObjective.InnerText, aMyPerformance.Value.Trim, lblMyRating.Text, amanager1.Value.Trim, lblMgrRating.Text, ajobgrade.Value, amanager2.Value.Trim, lblMgrRating2.Text)
                    MultiView1.ActiveViewIndex = 1
                    If txtEmpID.Text = Session("UserEmpID") Then
                        divrecommendation.Visible = False
                    End If
                    gridReviewee.Visible = True
                    LoadReviewee(lblid.Text)
                    Exit Sub
                End If
                lblstatus = "You cannot complete feedback until " & Process.DDMONYYYY(reviewend.Value)
                Process.loadalert(divalert, msgalert, lblstatus, "info")
                Exit Sub
            End If
            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_Questionaire_Update", lblQuestID.Text, lblid.Text, akpitype.InnerText, aObjective.Value, aMyObjective.InnerText, aMyPerformance.Value.Trim, lblMyRating.Text, amanager1.Value.Trim, lblMgrRating.Text, ajobgrade.Value, amanager2.Value.Trim, lblMgrRating2.Text)
            MultiView1.ActiveViewIndex = 1
            If txtEmpID.Text = Session("UserEmpID") Then
                divrecommendation.Visible = False
            End If

            gridReviewee.Visible = True
            LoadReviewee(lblid.Text)

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Protected Sub btnSubmitReview_Click(sender As Object, e As EventArgs) Handles btSubmitReview.Click
        Try
            Dim appIDD As String = Request.QueryString("id")
            Dim lblstatus As String = ""
            Dim confirmValue As String = Request.Form("confirm_value")
            If confirmValue = "Yes" Then
                If txtEmpID.Text = Session("UserEmpID") Then
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_Review_Submit", lblid.Text, "reviewee")
                    'Process.Appraisal_Review_Submit(txtEmpID.Text, Process.DDMONYYYY(reviewstart.Value) & ":" & Process.DDMONYYYY(reviewend.Value), aname.Value, areviewer1.Value, lblreviewer.Text, Process.GetMailLink(AuthenCode, 2))
                    'Process.Appraisal_Review_Submit(txtEmpID.Text, Process.DDMONYYYY(reviewstart.Value) & ":" & Process.DDMONYYYY(reviewend.Value), aname.Value, areviewer1.Value, lblreviewer.Text, Process.ApplicationURL & "/" & "Module/Employee/Performance/DirectReportAppraisalObjectivesForm.aspx")

                    Process.Appraisal_Review_Submit(txtEmpID.Text, Process.DDMONYYYY(reviewstart.Value) & ":" & Process.DDMONYYYY(reviewend.Value), aname.Value, areviewer1.Value, lblreviewer.Text, Process.ApplicationURL & "/" & "Module/Employee/Performance/AppraisalFeedback?id=" + appIDD)
                ElseIf lblreviewer.Text = Session("UserEmpID") Then
                    If cborecommendation.SelectedItem.Text.ToLower.Contains("--select") = True Then
                        lblstatus = "recommendation required!"
                        Process.loadalert(divalert, msgalert, lblstatus, "warning")

                        Exit Sub
                    End If
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_Review_Recommendation", lblid.Text, cborecommendation.SelectedItem.Text, "reviewer1")
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_Review_Submit", lblid.Text, "reviewer1")
                    If lblreviewer2.Text.ToLower = "n/a" Then
                        Process.Appraisal_Review_Complete(txtEmpID.Text, Process.DDMONYYYY(reviewstart.Value) & ":" & Process.DDMONYYYY(reviewend.Value), aname.Value, txtdept.Text, Process.ApplicationURL & "/" & Process.GetMailLink(AuthenCode, 0))
                    Else
                        'Process.Appraisal_Review_Submit(txtEmpID.Text, Process.DDMONYYYY(reviewstart.Value) & ":" & Process.DDMONYYYY(reviewend.Value), aname.Value, areviewer2.Value, lblreviewer2.Text, Process.ApplicationURL & "/" & Process.GetMailLink(AuthenCode, 2))
                        Process.Appraisal_Review_Submit(txtEmpID.Text, Process.DDMONYYYY(reviewstart.Value) & ":" & Process.DDMONYYYY(reviewend.Value), aname.Value, areviewer2.Value, lblreviewer2.Text, Process.ApplicationURL & "/" & "Module/Employee/Performance/AppraisalFeedback?id=" + appIDD)
                    End If

                ElseIf lblreviewer2.Text = Session("UserEmpID") Then
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Appraisal_Summary_Get", lblid.Text)
                    If strUser.Tables(0).Rows(0).Item("mgrsubmited").ToString.ToLower = "no" Then
                        lblstatus = "First Reviewer will require to review appraisal before you submit"
                        Process.loadalert(divalert, msgalert, lblstatus, "warning")

                        Exit Sub
                    Else
                        If cborecommendation.SelectedItem.Text.ToLower.Contains("--select") = True Then
                            lblstatus = "recommendation required!"
                            Process.loadalert(divalert, msgalert, lblstatus, "warning")
                            Exit Sub
                        End If
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_Review_Recommendation", lblid.Text, cborecommendation.SelectedItem.Text, "reviewer2")
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_Review_Submit", lblid.Text, "reviewer2")
                        Process.Appraisal_Review_Complete(txtEmpID.Text, Process.DDMONYYYY(reviewstart.Value) & ":" & Process.DDMONYYYY(reviewend.Value), aname.Value, txtdept.Text, Process.ApplicationURL & "/" & Process.GetMailLink(AuthenCode, 0))
                        Process.DisableButton(btnDisagree)
                    End If


                End If
                btSubmitReview.Enabled = False
                'Process.DisableButton(btnSubmitReview)
                Process.loadalert(divalert, msgalert, "Submit Successful", "success")
                MultiView1.ActiveViewIndex = 2
                'reviewerdetails.Style.Add("display", "block")
            Else
                lblstatus = "Submit cancelled"
                Process.loadalert(divalert, msgalert, lblstatus, "info")
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs)
        Try
            MultiView1.ActiveViewIndex = 0
            gridReviewee.Visible = False
            'gridreviewer.Visible = False
            'gridreviewer2.Visible = False
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Public Sub btnDev_Click(sender As Object, e As EventArgs)
        Try
            If lblreviewer.Text = Session("UserEmpID") Then
                Dim url As String = "DirectReportDevelopmentPlan"
                Dim s As String = "window.open('" & url + "', 'popup_window', 'width=950,height=1000,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no');"
                ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)
            Else
                Dim url As String = "DevelopmentPlans"
                Dim s As String = "window.open('" & url + "', 'popup_window', 'width=950,height=1000,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no');"
                ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub btnDev_Click1(sender As Object, e As EventArgs)
        Try
            If lblreviewer.Text = Session("UserEmpID") Then
                Dim url As String = Process.ApplicationURL & "/Module/Employee/TrainingPortal/MgrTrainings.aspx?id=" + txtEmpID.Text + ""
                Dim s As String = "window.open('" & url + "', 'popup_window', 'width=950,height=1000,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no');"
                ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)
            Else
                Dim url As String = Process.ApplicationURL & "/Module/Employee/TrainingPortal/Trainings.aspx"
                Dim s As String = "window.open('" & url + "', 'popup_window', 'width=950,height=1000,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no');"
                ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub btnDev_Click2(sender As Object, e As EventArgs)
        Try
            If lblreviewer.Text = Session("UserEmpID") Then
                Dim url As String = "AppraisalFeedBackNuggetsManagerList"
                Dim s As String = "window.open('" & url + "', 'popup_window', 'width=950,height=1000,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no');"
                ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)
            Else
                Dim url As String = "AppraisalFeedbacknuggetsOwnerlist"
                Dim s As String = "window.open('" & url + "', 'popup_window', 'width=950,height=1000,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no');"
                ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub btnDev_Click3(sender As Object, e As EventArgs)
        Try
            If lblreviewer.Text = Session("UserEmpID") Then
                Dim url As String = "MgrQueries"
                Dim s As String = "window.open('" & url + "', 'popup_window', 'width=1000,height=850,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no');"
                ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)
            Else
                Dim url As String = "Query"
                Dim s As String = "window.open('" & url + "', 'popup_window', 'width=1000,height=850,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no');"
                ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            Dim lblstatus As String = ""
            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_Questionaire_Update", lblQuestID.Text, lblid.Text, akpitype.InnerText, aObjective.Value, aMyObjective.InnerText, aMyPerformance.Value.Trim, lblMyRating.Text, amanager1.Value.Trim, lblMgrRating.Text, ajobgrade.Value, amanager2.Value.Trim, lblMgrRating2.Text)
            lblstatus = "Current review state saved"
            Process.loadalert(divalert, msgalert, lblstatus, "success")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub rdoMgrRatings2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rdoMgrRatings2.SelectedIndexChanged

        Try
            lblMgrRating2.Text = rdoMgrRatings2.SelectedValue
            If lblend.Text = "True" Then
                btSubmit.Disabled = False
                'Process.EnableButton(btnSubmit)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btn360degree_Click(sender As Object, e As EventArgs)
        Try
            Dim url As String = "feedbackselection.aspx?id=" & lblid.Text & "&empid=" & txtEmpID.Text & "&period=" & Process.DDMONYYYY(reviewstart.Value) & ":" & Process.DDMONYYYY(reviewend.Value)
            Dim s As String = "window.open('" & url + "', 'popup_window', 'width=900,height=600,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no');"
            ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub gridReviewee_DataBound(sender As Object, e As EventArgs) Handles gridReviewee.DataBound
        Try
            If txtEmpID.Text = Session("UserEmpID") Then
                If lblvisibleI.Text = "NO" Then
                    gridReviewee.Columns(5).Visible = False
                    gridReviewee.Columns(6).Visible = False
                End If
                If lblvisibleII.Text = "NO" Then
                    gridReviewee.Columns(7).Visible = False
                    gridReviewee.Columns(8).Visible = False
                End If
            ElseIf lblreviewer.Text = Session("UserEmpID") Then
                If areviewer2.Value.Trim = "" Or areviewer2.Value.Trim.ToLower = "n/a" Then
                    gridReviewee.Columns(7).Visible = False
                    gridReviewee.Columns(8).Visible = False
                End If
            End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Private Sub gridReviewee_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles gridReviewee.ItemDataBound
        Try
            If TypeOf e.Item Is GridDataItem Then
                Dim objectives As TableCell = TryCast(e.Item, GridDataItem)("objectives")
                objectives.Text = objectives.Text.Replace(vbCr & vbLf, "<br/>")

                Dim empidcomment As TableCell = TryCast(e.Item, GridDataItem)("empidcomment")
                empidcomment.Text = empidcomment.Text.Replace(vbCr & vbLf, "<br/>")

                Dim supervisorcomment As TableCell = TryCast(e.Item, GridDataItem)("supervisorcomment")
                supervisorcomment.Text = supervisorcomment.Text.Replace(vbCr & vbLf, "<br/>")

                Dim supervisorcomment2 As TableCell = TryCast(e.Item, GridDataItem)("supervisorcomment2")
                supervisorcomment2.Text = supervisorcomment2.Text.Replace(vbCr & vbLf, "<br/>")

                Dim successtarget As TableCell = TryCast(e.Item, GridDataItem)("successtarget")
                successtarget.Text = successtarget.Text.Replace(vbCr & vbLf, "<br/>")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnDisagree_Click(sender As Object, e As EventArgs) Handles btnDisagree.Click

        Try
            Dim lblstatus As String = ""

            Dim confirmValue As String = Request.Form("confirmplan_value")
            If confirmValue = "Yes" Then
                Process.disagree = 0
                Session("ManagerEmail") = lblrevieweremail.Text
                Dim url As String = "appraisaldisagree.aspx?empname=" & aname.Value & "&cycle=" & Process.DDMONYYYY(reviewstart.Value) & " : " & Process.DDMONYYYY(reviewend.Value) & "&reviewer=" & lblreviewer.Text & "&id=" & lblid.Text
                Dim s As String = "window.open('" & url + "', 'popup_window', 'width=500,height=400,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no');"
                ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)
            Else
                lblstatus = "Process cancelled"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
            End If



        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try

    End Sub
End Class