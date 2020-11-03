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
    Dim emailFile As String = ConfigurationManager.AppSettings("FileURL")
    Dim education As New clsEducation
    Dim Separators() As Char = {","c}
    Dim AuthenCode As String = "EMPAPPRAISAL"
    Dim rowCounts As Integer = 0
    Dim success, obj, kpitype As String


    'Private Sub LoadReviewee(dataid As Integer)
    '    Try
    '        gridReviewee.DataSource = Process.SearchData("Performance_Appraisal_Get_All", dataid)
    '        'Getting custom Names
    '        success = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select SuccessTarget from Performance_Custom_Naming")
    '        obj = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select Objectives from Performance_Custom_Naming")
    '        kpitype = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select KPIType from Performance_Custom_Naming")

    '        If kpitype = "" Then
    '            gridReviewee.Columns(0).HeaderText = "KPI Type"
    '        Else
    '            gridReviewee.Columns(0).HeaderText = kpitype
    '        End If
    '        If obj = "" Then
    '            gridReviewee.Columns(1).HeaderText = "Objectives"
    '        Else
    '            gridReviewee.Columns(1).HeaderText = obj
    '        End If
    '        If success = "" Then
    '            gridReviewee.Columns(2).HeaderText = "Success Target"
    '        Else
    '            gridReviewee.Columns(2).HeaderText = success
    '        End If

    '        gridReviewee.DataBind()
    '    Catch ex As Exception

    '    End Try
    'End Sub


    'Private Sub LoadQuestionaire(ByVal question As Integer, ByVal summaryID As Integer, ByVal grade As String)
    '    Try
    '        Dim strUser As New DataSet
    '        strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Appraisal_Questionaire_Get", question, summaryID, grade)
    '        If strUser.Tables(0).Rows.Count > 0 Then
    '            Process.LoadRadioButtonsDb(rdoMyRatings, "Performance_Points_Get_All", "PointName", "point", "pointdescription")
    '            Process.LoadRadioButtonsDb(rdoMgrRatings, "Performance_Points_Get_All", "PointName", "point", "pointdescription")
    '            Process.LoadRadioButtonsDb(rdoMgrRatings2, "Performance_Points_Get_All", "PointName", "point", "pointdescription")

    '            ' Process.LoadRadioButtonsDb(rdoMyRatings, "Performance_Points_Get_All", "pointname", "point", "pointdescription")

    '            akpitype.InnerText = strUser.Tables(0).Rows(0).Item("KPIType").ToString
    '            aQuestNo.InnerText = strUser.Tables(0).Rows(0).Item("rows").ToString
    '            lblQuestID.Text = strUser.Tables(0).Rows(0).Item("id").ToString
    '            aObjective.Value = strUser.Tables(0).Rows(0).Item("kpiobjectives").ToString
    '            'aObjDesc.InnerText = strUser.Tables(0).Rows(0).Item("KPIObjDesc").ToString.Replace(vbCrLf, "<br />")
    '            aObjDesc.InnerText = strUser.Tables(0).Rows(0).Item("objectives").ToString
    '            'aMyObjective.InnerText = strUser.Tables(0).Rows(0).Item("AppraisalItem").ToString.Replace(vbCrLf, "<br />-")
    '            aMyObjective.InnerText = strUser.Tables(0).Rows(0).Item("comment").ToString
    '            aMyPerformance.Value = strUser.Tables(0).Rows(0).Item("EmpIDComment").ToString
    '            Process.RadioListCheck(rdoMyRatings, strUser.Tables(0).Rows(0).Item("empratingdesc").ToString)
    '            lblMyRating.Text = strUser.Tables(0).Rows(0).Item("empIDRatimultiviewng").ToString

    '            amanager1.Value = strUser.Tables(0).Rows(0).Item("SupervisorComment").ToString
    '            Process.RadioListCheck(rdoMgrRatings, strUser.Tables(0).Rows(0).Item("mgrratingdesc").ToString)
    '            lblMgrRating.Text = strUser.Tables(0).Rows(0).Item("MgrIDRating").ToString

    '            amanager2.Value = strUser.Tables(0).Rows(0).Item("SupervisorComment2").ToString
    '            Process.RadioListCheck(rdoMgrRatings2, strUser.Tables(0).Rows(0).Item("mgrratingdesc2").ToString)
    '            lblMgrRating2.Text = strUser.Tables(0).Rows(0).Item("MgrIDRating2").ToString

    '        End If
    '    Catch ex As Exception
    '        Process.loadalert(divalert, msgalert, ex.Message, "danger")
    '    End Try
    'End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not Me.IsPostBack Then
                'Process.LoadRadComboTextAndValue1(radObjectives, "Performance_Objectives_Get_All", "KPIObjectives", "KPIObjectives", Request.QueryString("id"))
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
                Button9.Visible = False
                Button15.Visible = False
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
                    empemail.Text = strUser.Tables(0).Rows(0).Item("empMail").ToString
                    ajobtitle.Value = strUser.Tables(0).Rows(0).Item("jobtitle").ToString
                    ajobgrade.Value = strUser.Tables(0).Rows(0).Item("jobgrade").ToString
                    txtdept.Text = strUser.Tables(0).Rows(0).Item("dept").ToString
                    Period.Text = strUser.Tables(0).Rows(0).Item("Period").ToString
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
                    If Session("UserEmpID") = txtEmpID.Text Then
                        Text1.Value = strUser.Tables(0).Rows(0).Item("CoachName").ToString
                        Text3.Value = strUser.Tables(0).Rows(0).Item("CoachID").ToString
                    Else
                        Text1.Value = strUser.Tables(0).Rows(0).Item("empname").ToString
                        Text3.Value = strUser.Tables(0).Rows(0).Item("empid").ToString
                    End If
                    If Session("UserEmpID") = txtEmpID.Text Then
                            pointsheader.InnerText = "Reviewee Score"
                        B1.InnerText = "Enter Reviewee Comment"
                        'B6.InnerText = "All Reviewee Comments"
                        'B3.InnerText = "Reviewee Comments"
                    ElseIf Session("UserEmpID") = lblreviewer.Text Then
                            pointsheader.InnerText = "Reviewer I Score"
                        B1.InnerText = "Enter Reviewer I Comment"
                        'B6.InnerText = "All Reviewer I Comments"
                        'B3.InnerText = "Reviewer I Comments"
                    Else
                            pointsheader.InnerText = "Reviewer II Score"
                        B1.InnerText = "Enter Reviewer II Comment"
                        'B6.InnerText = "All Reviewer II Comments"
                        'B3.InnerText = "Reviewer II Comments"
                    End If
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_Questionaire_Count", lblid.Text, ajobgrade.Value)
                        'lblQuestCount.Text = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Performance_Appraisal_Questionaire_Count", lblid.Text, ajobgrade.Value )
                        lblQuestCount.Text = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Performance_Appraisal_Questionaire_Count_Actual", lblid.Text, ajobgrade.Value)
                        Session("QuestionNo") = 1

                    'If lblQuestCount.Text > 0 Then
                    '    LoadQuestionaire(Session("QuestionNo"), lblid.Text, ajobgrade.Value)
                    '    apageview.InnerText = "1 of " & lblQuestCount.Text
                    'Else
                    '    aQuestNo.InnerText = "0."
                    '    lblstatus = "No Objectives set for " & ajobgrade.Value
                    '    Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    'End If
                    mate.Visible = False
                    mate2.Visible = False
                    cyclestat = strUser.Tables(0).Rows(0).Item("cyclestat").ToString
                        If cyclestat.ToLower = "open" Then
                            Dim str360 As New DataSet
                            str360 = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Appraisal_360_Check", ajobgrade.Value)
                            If str360.Tables(0).Rows.Count > 0 Then
                            'bt360degree.Visible = True
                        Else
                            'bt360degree.Visible = False
                        End If
                        Else
                        ' bt360degree.Visible = False
                    End If

                        Dim actualcount As Integer = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Performance_Appraisal_Questionaire_Count_Actual", lblid.Text, ajobgrade.Value)



                        If txtEmpID.Text = Session("UserEmpID") Then

                        ' divrecommendation.Visible = False
                        Dim unanswered As Integer = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Performance_Appraisal_Questionaire_Reviewee_Unanswered", lblid.Text)
                            If actualcount = CInt(lblQuestCount.Text) And unanswered = 0 Then
                                'Process.EnableButton(btSubmit)
                                btSubmit.Visible = True
                            End If
                        ElseIf lblreviewer.Text = Session("UserEmpID") Then
                        'divrecommendation.Visible = True
                        Dim unanswered As Integer = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Performance_Appraisal_Questionaire_Reviewer_Unanswered", lblid.Text)
                            If actualcount = CInt(lblQuestCount.Text) And unanswered = 0 Then
                                'Process.EnableButton(btnSubmit)
                                btSubmit.Visible = True
                            End If
                        ElseIf lblreviewer2.Text = Session("UserEmpID") Then
                        ' divrecommendation.Visible = True
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
                    Views4.Visible = False
                    If txtEmpID.Text = Session("UserEmpID") Then
                            divreviewer1.Style.Add("display", "none")
                        divreviewer2.Style.Add("display", "none")

                        If empSubmit.ToUpper.Trim = "YES" Or MgrSubmit.ToUpper.Trim = "YES" Then
                                lblstatus = "Feedback form has already been submitted!"
                                Process.loadalert(divalert, msgalert, lblstatus, "info")
                                MultiView1.ActiveViewIndex = 1
                            'gridReviewee.Visible = True
                            'LoadReviewee(lblid.Text)
                            LoadGrid()
                                Button3.Visible = False
                            Views4.Visible = True
                            'Process.DisableButton(btSubmitReview)
                            'btback.Visible = False
                            'btSubmitReview.Visible = False

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
                        Process.AssignRadComboValue(RadComboBox2, strUser.Tables(0).Rows(0).Item("MgrRecommendation").ToString)
                        recomm.Value = strUser.Tables(0).Rows(0).Item("mngrrecommendation").ToString
                        If MgrSubmit.ToUpper.Trim = "YES" Then

                                MultiView1.ActiveViewIndex = 1
                            'gridReviewee.Visible = True
                            'LoadReviewee(lblid.Text)
                            'btSubmitReview.Enabled = False
                            'btback.Disabled = True
                            'Process.DisableButton(btnSubmitReview)
                            'Process.DisableButton(btnBack)
                            'btback.Visible = False
                            'btSubmitReview.Visible = False
                            Button3.Visible = False
                            Views4.Visible = True
                            LoadGrid()
                            gridskills.Visible = True
                            mate.Visible = True
                            mate2.Visible = False
                            RadComboBox2.Enabled = False
                            recomm.Disabled = True

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

                        Process.AssignRadComboValue(RadComboBox2, strUser.Tables(0).Rows(0).Item("MgrRecommendation2").ToString)
                        recomm.Value = strUser.Tables(0).Rows(0).Item("mngr1recommendation").ToString
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
                            Button3.Visible = False
                            Button4.Visible = False

                            MultiView1.ActiveViewIndex = 1
                            ' gridReviewee.Visible = True
                            'LoadReviewee(lblid.Text)
                            LoadGrid()
                            'btSubmitReview.Enabled = False
                            ' btback.Visible = False
                            'Process.DisableButton(btnSubmitReview)
                            mate.Visible = True
                            Views4.Visible = True
                            mate2.Visible = False
                            'btSubmitReview.Visible = False
                            RadComboBox2.Enabled = False
                            recomm.Disabled = True
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
            'If lblid.Text <> "0" And lblid.Text <> "" Then
            '    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_Questionaire_Update", lblQuestID.Text, lblid.Text, akpitype.InnerText, aObjective.Value, aMyObjective.InnerText, aMyPerformance.Value, lblMyRating.Text, amanager1.Value, lblMgrRating.Text, ajobgrade.Value, amanager2.Value.Trim, lblMgrRating2.Text)
            'End If


            If lblreviewer.Text = Session("UserEmpID") Then
                    Response.Redirect("~/Module/Employee/Performance/DirectReportAppraisalObjectivesForm", True)
                ElseIf lblreviewer2.Text = Session("UserEmpID") Then
                    Response.Redirect("~/Module/Employee/Performance/SecondRevewAppraisalObjectivesForm", True)
                Else
                    Response.Redirect("~/Module/Employee/Performance/AppraisalFeedBackList.aspx", True)
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
            'nuggetsquestion.Style.Add("display", "block")
            'reviewerdetails.Style.Add("display", "none")
            LoadGrid()
            gridskills.Visible = True
            Views4.Visible = True
            Button4.Visible = False
            Button3.Visible = False
            If Session("UserEmpID") = txtEmpID.Text Then
                Button9.Visible = True
            Else
                Button15.Visible = True
            End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub
    Protected Sub btnSubmitRecommend_Click(sender As Object, e As EventArgs)
        Dim strUser As DataSet
        strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Appraisal_Summary_Get", Request.QueryString("id").ToString)
        Dim empSubmit = strUser.Tables(0).Rows(0).Item("empsubmited").ToString
        Dim MgrSubmit = strUser.Tables(0).Rows(0).Item("mgrsubmited").ToString
        If Session("UserEmpID") = lblreviewer.Text Then
            If empSubmit.ToUpper.Trim = "NO" Then
                Process.loadalert(divalert, msgalert, "Reviewee has not yet completed feedback", "danger")
                Exit Sub
            End If
        End If
        If Session("UserEmpID") = lblreviewer2.Text Then
            If MgrSubmit.ToUpper.Trim = "NO" Then
                Process.loadalert(divalert, msgalert, "Reviewer I has not yet completed feedback", "danger")
                Exit Sub
            End If
        End If
        mate.Visible = True
            mate2.Visible = True
            LoadGrid()
            gridskills.Visible = True
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
                'LoadQuestionaire(Session("QuestionNo"), lblid.Text, ajobgrade.Value)
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
                'LoadQuestionaire(Session("QuestionNo"), lblid.Text, ajobgrade.Value)
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
                        'divrecommendation.Visible = False
                    End If
                    'gridReviewee.Visible = True
                    ' LoadReviewee(lblid.Text)
                    Exit Sub
                End If
                lblstatus = "You cannot complete feedback until " & Process.DDMONYYYY(reviewend.Value)
                Process.loadalert(divalert, msgalert, lblstatus, "info")
                Exit Sub
            End If
            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_Questionaire_Update", lblQuestID.Text, lblid.Text, akpitype.InnerText, aObjective.Value, aMyObjective.InnerText, aMyPerformance.Value.Trim, lblMyRating.Text, amanager1.Value.Trim, lblMgrRating.Text, ajobgrade.Value, amanager2.Value.Trim, lblMgrRating2.Text)
            MultiView1.ActiveViewIndex = 1
            'If txtEmpID.Text = Session("UserEmpID") Then
            '    divrecommendation.Visible = False
            'End If

            'gridReviewee.Visible = True
            'LoadReviewee(lblid.Text)

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Protected Sub btnSubmitReview_Click(sender As Object, e As EventArgs) Handles btSubmitReview.Click
        Try
            Dim appIDD As String = Request.QueryString("id")
            Dim lblstatus As String = ""
            Dim confirmvalue As String = Request.Form("confirm_value")
            Dim d1 As Date = Convert.ToDateTime(reviewend.Value)
            Dim d2 As Date = Convert.ToDateTime(Now.Date)

            If txtEmpID.Text = Session("UserEmpID") Then
                If ((d1 > d2)) And MrgEndcycle.Text.ToLower <> "yes" Then
                    lblstatus = "You cannot complete feedback until " & Process.DDMONYYYY(reviewend.Value)
                    Process.loadalert(divalert, msgalert, lblstatus, "info")
                    Exit Sub
                End If
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_Review_Submit", lblid.Text, "reviewee")
                'Process.Appraisal_Review_Submit(txtEmpID.Text, Process.DDMONYYYY(reviewstart.Value) & ":" & Process.DDMONYYYY(reviewend.Value), aname.Value, areviewer1.Value, lblreviewer.Text, Process.GetMailLink(AuthenCode, 2))
                'Process.Appraisal_Review_Submit(txtEmpID.Text, Process.DDMONYYYY(reviewstart.Value) & ":" & Process.DDMONYYYY(reviewend.Value), aname.Value, areviewer1.Value, lblreviewer.Text, Process.ApplicationURL & "/" & "Module/Employee/Performance/DirectReportAppraisalObjectivesForm.aspx")

                Process.Appraisal_Review_Submit(txtEmpID.Text, Process.DDMONYYYY(reviewstart.Value) & ":" & Process.DDMONYYYY(reviewend.Value), aname.Value, areviewer1.Value, lblreviewer.Text, Process.ApplicationURL & "/" & "Module/Employee/Performance/AppraisalFeedback?id=" + appIDD)
                ElseIf lblreviewer.Text = Session("UserEmpID") Then
                    If confirmvalue = "Yes" Then
                        If RadComboBox2.SelectedItem.Text.ToLower.Contains("--select") = True Then
                            lblstatus = "recommendation required!"
                            Process.loadalert(divalert, msgalert, lblstatus, "warning")

                            Exit Sub
                        End If
                        If recomm.Value = "" Then
                            lblstatus = "recommendation comments required!"
                            Process.loadalert(divalert, msgalert, lblstatus, "warning")
                            Exit Sub
                        End If
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_Review_Recommendation", lblid.Text, RadComboBox2.SelectedItem.Text, recomm.Value, "reviewer1")
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_Review_Submit", lblid.Text, "reviewer1")
                    Else lblstatus = "Process is cancelled"
                        Process.loadalert(divalert, msgalert, lblstatus, "warning")
                        Exit Sub
                    End If
                    If lblreviewer2.Text.ToLower = "n/a" Then
                        Process.Appraisal_Review_Complete(txtEmpID.Text, Process.DDMONYYYY(reviewstart.Value) & ":" & Process.DDMONYYYY(reviewend.Value), aname.Value, txtdept.Text, Process.ApplicationURL & "/" & Process.GetMailLink(AuthenCode, 0))
                    Else
                        'Process.Appraisal_Review_Submit(txtEmpID.Text, Process.DDMONYYYY(reviewstart.Value) & ":" & Process.DDMONYYYY(reviewend.Value), aname.Value, areviewer2.Value, lblreviewer2.Text, Process.ApplicationURL & "/" & Process.GetMailLink(AuthenCode, 2))
                        Process.Appraisal_Review_Submit(txtEmpID.Text, Process.DDMONYYYY(reviewstart.Value) & ":" & Process.DDMONYYYY(reviewend.Value), aname.Value, areviewer2.Value, lblreviewer2.Text, Process.ApplicationURL & "/" & "Module/Employee/Performance/AppraisalFeedback?id=" + appIDD)
                    End If

                ElseIf lblreviewer2.Text = Session("UserEmpID") Then
                    If confirmvalue = "Yes" Then
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Appraisal_Summary_Get", lblid.Text)
                    If strUser.Tables(0).Rows(0).Item("mgrsubmited").ToString.ToLower = "no" Then
                        lblstatus = "First Reviewer will require to review appraisal before you submit"
                        Process.loadalert(divalert, msgalert, lblstatus, "warning")

                        Exit Sub
                    Else
                        If RadComboBox2.SelectedItem.Text.ToLower.Contains("--select") = True Then
                            lblstatus = "recommendation required!"
                            Process.loadalert(divalert, msgalert, lblstatus, "warning")
                            Exit Sub
                        End If
                        If recomm.Value = "" Then
                            lblstatus = "recommendation comments required!"
                            Process.loadalert(divalert, msgalert, lblstatus, "warning")
                            Exit Sub
                        End If
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_Review_Recommendation", lblid.Text, RadComboBox2.SelectedItem.Text, recomm.Value, "reviewer2")
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_Review_Submit", lblid.Text, "reviewer2")
                        Process.Appraisal_Review_Complete(txtEmpID.Text, Process.DDMONYYYY(reviewstart.Value) & ":" & Process.DDMONYYYY(reviewend.Value), aname.Value, txtdept.Text, Process.ApplicationURL & "/" & Process.GetMailLink(AuthenCode, 0))
                        '  Process.DisableButton(btnDisagree)
                    End If
                Else lblstatus = "Process is cancelled"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    Exit Sub
                End If


            End If
                'btSubmitReview.Enabled = False
                'Process.DisableButton(btnSubmitReview)
                Process.loadalert(divalert, msgalert, "Submit Successful", "success")
            MultiView1.ActiveViewIndex = 2
            'reviewerdetails.Style.Add("display", "block")

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs)
        Try
            MultiView1.ActiveViewIndex = 0
            ' gridReviewee.Visible = False
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

    'Protected Sub gridReviewee_DataBound(sender As Object, e As EventArgs) Handles gridReviewee.DataBound
    '    Try
    '        If txtEmpID.Text = Session("UserEmpID") Then
    '            If lblvisibleI.Text = "NO" Then
    '                gridReviewee.Columns(5).Visible = False
    '                gridReviewee.Columns(6).Visible = False
    '            End If
    '            If lblvisibleII.Text = "NO" Then
    '                gridReviewee.Columns(7).Visible = False
    '                gridReviewee.Columns(8).Visible = False
    '            End If
    '        ElseIf lblreviewer.Text = Session("UserEmpID") Then
    '            If areviewer2.Value.Trim = "" Or areviewer2.Value.Trim.ToLower = "n/a" Then
    '                gridReviewee.Columns(7).Visible = False
    '                gridReviewee.Columns(8).Visible = False

    '            End If

    '        End If

    '    Catch ex As Exception
    '        Process.loadalert(divalert, msgalert, ex.Message, "danger")
    '    End Try
    'End Sub

    'Private Sub gridReviewee_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles gridReviewee.ItemDataBound
    '    Try
    '        If TypeOf e.Item Is GridDataItem Then
    '            Dim objectives As TableCell = TryCast(e.Item, GridDataItem)("objectives")
    '            objectives.Text = objectives.Text.Replace(vbCr & vbLf, "<br/>")

    '            Dim empidcomment As TableCell = TryCast(e.Item, GridDataItem)("empidcomment")
    '            empidcomment.Text = empidcomment.Text.Replace(vbCr & vbLf, "<br/>")

    '            Dim supervisorcomment As TableCell = TryCast(e.Item, GridDataItem)("supervisorcomment")
    '            supervisorcomment.Text = supervisorcomment.Text.Replace(vbCr & vbLf, "<br/>")

    '            Dim supervisorcomment2 As TableCell = TryCast(e.Item, GridDataItem)("supervisorcomment2")
    '            supervisorcomment2.Text = supervisorcomment2.Text.Replace(vbCr & vbLf, "<br/>")

    '            Dim successtarget As TableCell = TryCast(e.Item, GridDataItem)("successtarget")
    '            successtarget.Text = successtarget.Text.Replace(vbCr & vbLf, "<br/>")
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub
    Private Sub gridskills_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gridskills.PageIndexChanging
        Try
            gridskills.PageIndex = e.NewPageIndex
            Session("courseskillLoadindex") = e.NewPageIndex
            LoadGrid()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gridskills_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridskills.RowCreated
        Try
            Process.SortArrow(e, SortsDirection, Session("courseskillsortExpression"))
        Catch ex As Exception
        End Try
    End Sub
    Private Sub LoadGrid()
        Try
            gridskills.PageIndex = CInt(Session("courseskillLoadindex"))
            gridskills.DataSource = LoadDatatable()
            gridskills.AllowSorting = True
            gridskills.DataBind()
            Try
                Dim strUser As New DataSet
                strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Appraisal_Summary_Get", lblid.Text)
                Dim EmpSummit = strUser.Tables(0).Rows(0).Item("empsubmited").ToString.ToLower
                Dim MngrSummit = strUser.Tables(0).Rows(0).Item("mgrsubmited").ToString.ToLower
                Dim SupSummit = strUser.Tables(0).Rows(0).Item("mgrsubmited2").ToString.ToLower
                If txtEmpID.Text = Session("UserEmpID") Then

                    gridskills.Columns(7).Visible = False
                    gridskills.Columns(10).Visible = False
                    gridskills.Columns(11).Visible = False
                    gridskills.Columns(14).Visible = False
                    If EmpSummit = "no" Then
                        gridskills.Columns(5).Visible = False
                        gridskills.Columns(4).Visible = False
                    Else
                        gridskills.Columns(6).Visible = False
                        gridskills.Columns(3).Visible = False
                    End If
                    If lblvisibleI.Text = "NO" Then
                        gridskills.Columns(8).Visible = False
                        gridskills.Columns(9).Visible = False


                    End If
                    If lblvisibleII.Text = "NO" Then
                        gridskills.Columns(12).Visible = False
                        gridskills.Columns(13).Visible = False
                    End If
                ElseIf lblreviewer.Text = Session("UserEmpID") Then
                    gridskills.Columns(3).Visible = False
                    gridskills.Columns(6).Visible = False
                    gridskills.Columns(11).Visible = False
                    gridskills.Columns(14).Visible = False
                    If MngrSummit = "no" Then
                        gridskills.Columns(8).Visible = False
                        gridskills.Columns(9).Visible = False
                    Else
                        gridskills.Columns(7).Visible = False
                        gridskills.Columns(10).Visible = False
                    End If
                    If areviewer2.Value.Trim = "" Or areviewer2.Value.Trim.ToLower = "n/a" Then
                        gridskills.Columns(12).Visible = False
                        gridskills.Columns(13).Visible = False
                    End If
                ElseIf lblreviewer2.Text = Session("UserEmpID") Then
                    gridskills.Columns(3).Visible = False
                    gridskills.Columns(6).Visible = False
                    gridskills.Columns(7).Visible = False
                    gridskills.Columns(10).Visible = False
                    If SupSummit = "no" Then
                        gridskills.Columns(12).Visible = False
                        gridskills.Columns(13).Visible = False
                    Else
                        gridskills.Columns(11).Visible = False
                        gridskills.Columns(14).Visible = False
                    End If
                End If
            Catch ex As Exception

            End Try
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub
    Protected Sub OnRowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gridskills, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub
    Protected Sub SortRecords(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Try
            Dim sortExpression As String = e.SortExpression
            Session("termsort") = sortExpression
            Dim direction As String = String.Empty
            If SortsDirection = SortDirection.Ascending Then
                SortsDirection = SortDirection.Descending
                direction = " DESC"
            Else
                SortsDirection = SortDirection.Ascending
                direction = " ASC"
            End If
            Dim table As DataTable = LoadDatatable()
            table.DefaultView.Sort = sortExpression & direction
            gridskills.PageIndex = CInt(Session("termindex"))
            gridskills.DataSource = table
            gridskills.DataBind()
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
    Private Function LoadDatatable() As DataTable
        Dim dt As New DataTable


        dt = Process.SearchData("Performance_Appraisal_Get_All", Request.QueryString("id"))

        Return dt
    End Function

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
    Public Sub submitschedule(sender As Object, e As EventArgs)
        Try
            'Dim babe = performanceid.Value
            Dim lblstatus As String = ""
            Dim Time = Request.Form("appt")
            Dim Goat = Request.Form("grateful")
            Dim Array As Array = Goat.Split(",")


            If Request.Form("coachdate") = "" Then
                lblstatus = "No date selected"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
            End If
            If Time = "" Then
                lblstatus = "Please State your time"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
            End If
            Dim Time1 = TimeSpan.Parse(Request.Form("appt"))
            Dim CoachieDate = Date.Parse(Request.Form("coachdate"))
            CoachieDate = CoachieDate.Add(Time1)
            If Goat = "" Then

            End If
            If comment1.Value Is Nothing Then
                lblstatus = "No Comments is inputed"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
            End If
            Dim str = Guid.NewGuid.ToString()


            Dim uid = GetIdentity(lblid.Text, aname.Value, areviewer1.Value, str, comment1.Value, CoachieDate, Time)

            For d As Integer = 0 To Array.Length - 1
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Coaching_Add_Objectives", str, Array(d), lblid.Text)
            Next
            Dim Coachingid = 0
            Dim Title = reviewyear.Value + " " + "Appraisal:" + Period.Text + " " + "Aprraisal Period Coaching Session"
            Dim Topic = "Discussions on ;" + Goat

            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Calendar_Event_Update", Coachingid, Session("UserEmpID"), CoachieDate, Time, Title, Topic, CoachieDate, "Not started")
            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Calendar_Event_Update", Coachingid, Text3.Value, CoachieDate, Time, Title, Topic, CoachieDate, "Not started")
            Process.Coaching_Alert(Session("UserEmpID"), empemail.Text, Text1.Value, Process.DDMONYYYY(CoachieDate), Process.ApplicationURL & "/" & "Module/Employee/Performance/CoachingForm?id=" + uid, Time)
            Process.Coaching_Alert(Text3.Value, lblrevieweremail.Text, Text1.Value, Process.DDMONYYYY(CoachieDate), Process.ApplicationURL & "/" & "Module/Employee/Performance/CoachingForm?id=" + uid, Time)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try

    End Sub
    'Protected Sub lblcertificate_Click(sender As Object, e As EventArgs)
    '    Try
    '        Dim dt As DataTable = Process.SearchData("Recruit_Job_Applicant_Profile", "cdcdnnc")
    '        If dt IsNot Nothing Then
    '            'downloadFile(CType(dt.Rows(0)("certfile"), Byte()), dt.Rows(0)("certtype").ToString(), dt.Rows(0)("certname").ToString())
    '        End If
    '        Dim fileName As String = dt.Rows(0)("certname").ToString()
    '        Dim filePath As String = Server.MapPath(emailFile & fileName)
    '        Response.ContentType = ContentType
    '        Response.AppendHeader("Content-Disposition", ("attachment; filename=" + Path.GetFileName(filePath)))
    '        Response.WriteFile(filePath)
    '        Response.End()
    '    Catch ex As Exception
    '        Process.loadalert(divalert, msgalert, ex.Message, "danger")
    '    End Try
    'End Sub
    Protected Sub Coaching_Sessions(sender As Object, e As EventArgs)
        Try
            Response.Redirect("~/Module/Employee/Performance/CoachingList", True)
        Catch ex As Exception

        End Try

    End Sub
    Protected Sub CompleteFeedback_Click(sender As Object, e As EventArgs)
        Try
            Dim lblstatus = ""
            Dim strUser As New DataSet
            strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Appraisal_Summary_Get", lblid.Text)
            If Session("UserEmpID") = txtEmpID.Text Then
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_Review_Submit", lblid.Text, "reviewee")
                Process.Appraisal_Review_Submit(txtEmpID.Text, Process.DDMONYYYY(reviewstart.Value) & ":" & Process.DDMONYYYY(reviewend.Value), aname.Value, areviewer1.Value, lblreviewer.Text, Process.ApplicationURL & "/" & "Module/Employee/Performance/AppraisalFeedback?id=" + lblid.Text)
            End If
            If Session("UserEmpID") = lblreviewer.Text Then
                Dim Recommend = Request.Form("RadComboBox1")
                If strUser.Tables(0).Rows(0).Item("empsubmited").ToString.ToLower = "no" Then
                    lblstatus = "reviewee will require to review appraisal before you submit"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")

                    Exit Sub
                End If
                If Recommend = "" Then
                    lblstatus = "Please Enter Recommendation Before You submit"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")

                    Exit Sub
                End If
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_Review_Recommendation", lblid.Text, Recommend, "reviewer1")
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_Review_Submit", lblid.Text, "reviewer")
                If lblreviewer2.Text.ToLower = "n/a" Then
                    Process.Appraisal_Review_Complete(txtEmpID.Text, Process.DDMONYYYY(reviewstart.Value) & ":" & Process.DDMONYYYY(reviewend.Value), aname.Value, txtdept.Text, Process.ApplicationURL & "/" & Process.GetMailLink(AuthenCode, 0))
                Else
                    'Process.Appraisal_Review_Submit(txtEmpID.Text, Process.DDMONYYYY(reviewstart.Value) & ":" & Process.DDMONYYYY(reviewend.Value), aname.Value, areviewer2.Value, lblreviewer2.Text, Process.ApplicationURL & "/" & Process.GetMailLink(AuthenCode, 2))
                    Process.Appraisal_Review_Submit(txtEmpID.Text, Process.DDMONYYYY(reviewstart.Value) & ":" & Process.DDMONYYYY(reviewend.Value), aname.Value, areviewer2.Value, lblreviewer2.Text, Process.ApplicationURL & "/" & "Module/Employee/Performance/AppraisalFeedback?id=" + lblid.Text)
                End If
            End If
            If Session("UserEmpID") = lblreviewer2.Text Then
                Dim Recommend = Request.Form("RadComboBox1")

                If strUser.Tables(0).Rows(0).Item("mgrsubmited").ToString.ToLower = "no" Then
                    lblstatus = "First Reviewer will require to review appraisal before you submit"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")

                    Exit Sub
                End If
                If Recommend = "" Then
                    lblstatus = "Please Enter Recommendation Before You submit"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")

                    Exit Sub
                End If
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_Review_Recommendation", lblid.Text, Recommend, "reviewer1")
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_Review_Submit", lblid.Text, "reviewer1")
                Process.Appraisal_Review_Complete(txtEmpID.Text, Process.DDMONYYYY(reviewstart.Value) & ":" & Process.DDMONYYYY(reviewend.Value), aname.Value, txtdept.Text, Process.ApplicationURL & "/" & Process.GetMailLink(AuthenCode, 0))
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Function GetIdentity(ByVal kpiid As String, ByVal empname As String, ByVal revname As String, ByVal guid As String, ByVal comments As String, ByVal vdate As Date, ByVal time As String) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Performance_Coaching_Add"
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@kpiid", SqlDbType.Int).Value = kpiid
            cmd.Parameters.Add("@emp_name", SqlDbType.VarChar).Value = empname
            cmd.Parameters.Add("@reviewer_name", SqlDbType.VarChar).Value = revname
            cmd.Parameters.Add("@GUID", SqlDbType.VarChar).Value = guid
            cmd.Parameters.Add("@comments", SqlDbType.VarChar).Value = comments
            cmd.Parameters.Add("@Date", SqlDbType.Date).Value = vdate
            cmd.Parameters.Add("@time", SqlDbType.VarChar).Value = time
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
            Return 0
        End Try
    End Function

    'Private Sub gridskills_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridskills.RowCreated
    '    Try
    '        Process.SortArrow(e, SortsDirection1, Session("courseskillsortExpression"))
    '    Catch ex As Exception
    '    End Try
    'End Sub
    'Private Sub LoadGrid1()
    '    Try
    '        gridskills.PageIndex = CInt(Session("courseskillLoadindex"))
    '        gridskills.DataSource = LoadDatatable1()
    '        gridskills.AllowSorting = True
    '        gridskills.DataBind()

    'End Sub
    'Protected Sub OnRowDataBound(sender As Object, e As GridViewRowEventArgs)
    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gridskills, "Select$" & e.Row.RowIndex)
    '        e.Row.ToolTip = "Click to select this row."
    '    End If
    'End Sub
    'Protected Sub SortRecords1(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
    '    Try
    '        Dim sortExpression As String = e.SortExpression
    '        Session("termsort") = sortExpression
    '        Dim direction As String = String.Empty
    '        If SortsDirection1 = SortDirection1.Ascending Then
    '            SortsDirection1 = SortDirection1.Descending
    '            direction = " DESC"
    '        Else
    '            SortsDirection1 = SortDirection.Ascending
    '            direction = " ASC"
    '        End If
    '        Dim table As DataTable = LoadDatatable1()
    '        table.DefaultView.Sort = sortExpression & direction
    '        gridskills.PageIndex = CInt(Session("termindex"))
    '        gridskills.DataSource = table
    '        gridskills.DataBind()
    '    Catch ex As Exception
    '    End Try
    'End Sub
    'Public Property SortsDirection1() As SortDirection1
    '    Get
    '        If ViewState("SortDirection1") Is Nothing Then
    '            ViewState("SortDirection1") = SortDirection1.Ascending
    '        End If
    '        Return DirectCast(ViewState("SortDirection1"), SortDirection1)
    '    End Get
    '    Set(ByVal value As SortDirection1)
    '        ViewState("SortDirection1") = value
    '    End Set
    'End Property
    'Private Function LoadDatatable1() As DataTable
    '    Dim dt As New DataTable


    '    dt = Process.SearchData("Performance_Comment_get_All", Request.QueryString("id"))

    '    Return dt
    'End Function
    Protected Sub download_click(sender As Object, e As EventArgs)
        Try
            Dim strDataSet As New DataSet
            strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_comment_download", lblid.Text, txtEmpID.Text)
            Dim dataTables As DataTable = strDataSet.Tables(0)
            Dim filename = "All Comments"
            If Process.ExportPayroll(dataTables, filename) = False Then
                Process.loadalert(divalert, msgalert, Session("exception"), "danger")
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & Process.strExp + "')", True)
            Else
                Response.Write("File saved as " & filename & ".xls")
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "File saved as " & filename & ".xls" + "')", True)
            End If
        Catch ex As Exception

        End Try
    End Sub




End Class