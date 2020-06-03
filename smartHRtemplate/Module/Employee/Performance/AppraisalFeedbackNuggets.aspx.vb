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


Public Class AppraisalFeedbackNuggets
    Inherits System.Web.UI.Page
    Dim education As New clsEducation
    Dim Separators() As Char = {","c}
    Dim AuthenCode As String = "APPRAISALNUGGET"
    Dim rowCounts As Integer = 0

    Private Sub LoadReviewee(dataid As String, company As String)
        Try
            gridReviewee.DataSource = Process.SearchData("Performance_Appraisal_NuggetList_Review", lblid.Text)
            gridReviewee.DataBind()

        Catch ex As Exception

        End Try
    End Sub


    Protected Sub cboEmployee_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboEmployee.SelectedIndexChanged
        Try
            Dim lblstatus As String = ""
            Dim strUser As New DataSet
            strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_PersonalDetail_get", cboEmployee.SelectedValue)
            If strUser.Tables(0).Rows.Count > 0 Then
                dept.Value = strUser.Tables(0).Rows(0).Item("office").ToString
                ajobgrade.Value = strUser.Tables(0).Rows(0).Item("grade").ToString
                ajobtitle.Value = strUser.Tables(0).Rows(0).Item("jobtitle").ToString
                alenofservice.Value = strUser.Tables(0).Rows(0).Item("duration").ToString
            Else
                dept.Value = ""
                ajobgrade.Value = ""
                ajobtitle.Value = ""
                alenofservice.Value = ""
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Private Sub LoadQuestionaire(ByVal question As Integer, ByVal summaryID As String, ByVal grade As String)
        Try
            Dim strUser As New DataSet
            strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Appraisal_Nugget_Questionaire_Get", question, summaryID)
            If strUser.Tables(0).Rows.Count > 0 Then
                Process.LoadRadioButtonsDb(rdoMyRatings, "Performance_Points_Get_All", "PointName", "point", "pointdescription")
                Process.LoadRadioButtonsDb(rdoMgrRatings, "Performance_Points_Get_All", "PointName", "point", "pointdescription")
                Process.LoadRadioButtonsDb(rdoMgrRatings2, "Performance_Points_Get_All", "PointName", "point", "pointdescription")

                ' Process.LoadRadioButtonsDb(rdoMyRatings, "Performance_Points_Get_All", "pointname", "point", "pointdescription")

                akpitype.InnerText = strUser.Tables(0).Rows(0).Item("KPIType").ToString
                aQuestNo.InnerText = strUser.Tables(0).Rows(0).Item("rows").ToString
                lblQuestID.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                aObjective.Value = strUser.Tables(0).Rows(0).Item("KPIType").ToString
                'aObjDesc.InnerText = strUser.Tables(0).Rows(0).Item("KPIObjDesc").ToString.Replace(vbCrLf, "<br />")
                'aObjDesc.InnerText = strUser.Tables(0).Rows(0).Item("KPIObjDesc").ToString
                'aMyObjective.InnerText = strUser.Tables(0).Rows(0).Item("AppraisalItem").ToString.Replace(vbCrLf, "<br />-")
                aMyObjective.InnerText = strUser.Tables(0).Rows(0).Item("AppraisalItem").ToString
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
                Dim empSubmit As String, MgrSubmit As String = "No", MgrSubmit2 As String = "No", cyclestat As String
                btSubmit.Disabled = True
                Dim customName As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select AppraisalFeedbackNugget from Performance_Custom_Naming")
                If (customName = "") Then
                    pagetitle.InnerText = " Performance Appraisal Feedback Nugget Form"
                Else
                    pagetitle.InnerText = customName + " Form"
                End If

                'Process.DisableButton(btnSubmit)
                lblend.Text = "False"
                Dim lblstatus As String = ""
                If Request.QueryString("id") IsNot Nothing Then
                    Button1.Visible = False
                    Button4.Visible = False
                    Dim reviewer1Visible As String = "NO"
                    Dim reviewer2Visible As String = "NO"
                    
                    'Dim straccess As New DataSet
                    'straccess = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Appraisal_Preference_Get", Session("Organisation"))
                    'If straccess.Tables(0).Rows.Count > 0 Then
                    '    reviewer1Visible = straccess.Tables(0).Rows(0).Item("ReviewerVisible").ToString.ToUpper
                    '    reviewer2Visible = straccess.Tables(0).Rows(0).Item("RevieweriiVisible").ToString.ToUpper
                    'End If
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
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Appraisal_Nugget_Get", Request.QueryString("id").ToString, Session("Organisation"))
                    lblid.Text = strUser.Tables(0).Rows(0).Item("AppraisalItem").ToString
                    reviewyear.Value = strUser.Tables(0).Rows(0).Item("emp_period").ToString
                    txtEmpID.Text = strUser.Tables(0).Rows(0).Item("rev_empid").ToString

                    dept.Value = strUser.Tables(0).Rows(0).Item("emp_dept").ToString
                    ajobgrade.Value = strUser.Tables(0).Rows(0).Item("emp_jgrade").ToString
                    ajobtitle.Value = strUser.Tables(0).Rows(0).Item("emp_title").ToString
                    alenofservice.Value = strUser.Tables(0).Rows(0).Item("emp_length").ToString

                    jdept.Value = strUser.Tables(0).Rows(0).Item("rev_dept").ToString
                    jgrade.Value = strUser.Tables(0).Rows(0).Item("rev_grade").ToString
                    jtitle.Value = strUser.Tables(0).Rows(0).Item("rev_title").ToString
                    jname.Value = strUser.Tables(0).Rows(0).Item("rev_name").ToString
                    Session("rev_empid") = strUser.Tables(0).Rows(0).Item("rev_empid").ToString

                    txtdept.Text = strUser.Tables(0).Rows(0).Item("emp_dept").ToString


                    Process.LoadRadComboTextAndValueP3(cboEmployee, "Emp_PersonalDetail_Get_Employees", Session("Organisation"), "name", "empid", False)
                    cboEmployee.Text = strUser.Tables(0).Rows(0).Item("emp_name").ToString
                    cboEmployee.Enabled = False

                    rev_name.Text = strUser.Tables(0).Rows(0).Item("rev_name").ToString

                    'aoverdesc.Value = strUser.Tables(0).Rows(0).Item("gradename").ToString
                    'If aoverdesc.Value.Trim = "" Then
                    '    divoverdesc.Visible = False
                    'End If

                    lblreviewer.Text = strUser.Tables(0).Rows(0).Item("CoachID").ToString
                    lblrevieweremail.Text = strUser.Tables(0).Rows(0).Item("CoachName").ToString
                    'lblreviewer2.Text = strUser.Tables(0).Rows(0).Item("SupervisorID2").ToString
                    'areviewer2.Value = strUser.Tables(0).Rows(0).Item("supervisor2name2").ToString

                    lblQuestCount.Text = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Performance_Appraisal_Nugget_Questionaire_Count", lblid.Text)
                    Session("QuestionNo") = 1

                    If lblQuestCount.Text > 0 Then
                        If CInt(Session("QuestionNo")) = CInt(lblQuestCount.Text) Then
                            btnext.Disabled = True
                            btprevious.Disabled = False
                            btSubmit.Disabled = False
                        End If
                        LoadQuestionaire(Session("QuestionNo"), lblid.Text, ajobgrade.Value)
                        apageview.InnerText = "1 of " & lblQuestCount.Text

                    Else
                        aQuestNo.InnerText = "0."
                        lblstatus = "No KPI Category set for " & Session("Organisation")
                        Process.loadalert(divalert, msgalert, lblstatus, "info")
                    End If

                    cyclestat = strUser.Tables(0).Rows(0).Item("AppraisalStatus").ToString
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

                    Dim actualcount As Integer = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Performance_Appraisal_Nugget_Questionaire_Count", lblid.Text)



                    If txtEmpID.Text = Session("UserEmpID") Then

                        divrecommendation.Visible = False
                        Dim unanswered As Integer = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Performance_Appraisal_Nugget_Questionaire_Reviewee_Unanswered", lblid.Text)
                        If actualcount = CInt(lblQuestCount.Text) And unanswered = 0 Then
                            'Process.EnableButton(btSubmit)
                            btSubmit.Visible = True
                        End If
                    ElseIf lblreviewer.Text = Session("UserEmpID") Then
                        divrecommendation.Visible = True
                        Dim unanswered As Integer = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Performance_Appraisal_Nugget_Questionaire_Reviewee_Unanswered", lblid.Text)
                        If actualcount = CInt(lblQuestCount.Text) And unanswered = 0 Then
                            'Process.EnableButton(btnSubmit)
                            btSubmit.Visible = True
                        End If
                    ElseIf lblreviewer2.Text = Session("UserEmpID") Then
                        divrecommendation.Visible = True
                        Dim unanswered As Integer = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Performance_Appraisal_Nugget_Questionaire_Reviewee_Unanswered", lblid.Text)
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
                        divreviewer1.Visible = False
                        divreviewer2.Visible = False
                    End If
                    empSubmit = strUser.Tables(0).Rows(0).Item("rev_submitted").ToString
                    'MgrSubmit = strUser.Tables(0).Rows(0).Item("mgrsubmited").ToString
                    'MgrSubmit2 = strUser.Tables(0).Rows(0).Item("mgrsubmited2").ToString

                    If txtEmpID.Text = Session("UserEmpID") Then
                        If empSubmit.ToUpper.Trim = "YES" Or MgrSubmit.ToUpper.Trim = "YES" Then
                            lblstatus = "Feedback Nugget form has already been submitted!"
                            Process.loadalert(divalert, msgalert, lblstatus, "info")
                            MultiView1.ActiveViewIndex = 2
                            Button3.Disabled = True
                            reviewerdetails.Style.Add("display", "none")
                            gridReviewee.Visible = True
                            LoadReviewee(lblid.Text, Session("Organisation"))

                            Process.DisableButton(btSubmitReview)
                            btback.Visible = False
                            btSubmitReview.Visible = False

                        End If
                    ElseIf lblreviewer.Text = Session("UserEmpID") Then

                        amanager2.Disabled = True
                        rdoMgrRatings2.Enabled = False
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
                            Exit Sub
                        End If
                        'Process.AssignRadComboValue(cborecommendation, strUser.Tables(0).Rows(0).Item("MgrRecommendation").ToString)
                        Button3.Visible = False
                        Button1.Visible = False
                        Button4.Visible = True
                        btsave.Visible = False
                        'MultiView1.ActiveViewIndex = 1
                        'gridReviewee.Visible = True
                        'LoadReviewee(lblid.Text, Session("Organisation"))
                        'btSubmitReview.Enabled = False
                        'btback.Disabled = True
                        ''Process.DisableButton(btnSubmitReview)
                        ''Process.DisableButton(btnBack)
                        'btback.Visible = False
                        'btSubmitReview.Visible = False

                    ElseIf lblreviewer2.Text = Session("UserEmpID") Then
                        'Process.AssignRadComboValue(cborecommendation, strUser.Tables(0).Rows(0).Item("MgrRecommendation2").ToString)
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
                            LoadReviewee(lblid.Text, Session("Organisation"))

                            btSubmitReview.Enabled = False
                            btback.Visible = False
                            'Process.DisableButton(btnSubmitReview)

                            btSubmitReview.Visible = False
                        End If
                    End If
                    'Performance checks
                    'check weight
                    'Dim strValid As String = ""
                    'Dim strWeight As New DataSet
                    'strWeight = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Appraisal_Total_Check", lblid.Text)
                    'If strWeight.Tables(0).Rows.Count > 0 Then
                    '    For h As Integer = 0 To strWeight.Tables(0).Rows.Count - 1
                    '        If CDbl(strWeight.Tables(0).Rows(h).Item("weights").ToString) > 100 Then
                    '            strValid = "excess"
                    '        ElseIf CDbl(strWeight.Tables(0).Rows(h).Item("weights").ToString) < 100 Then
                    '            strValid = "incomplete"
                    '        End If
                    '    Next
                    'Else
                    '    strValid = "incomplete"
                    'End If

                    'If strValid = "incomplete" Or strValid = "excess" Then
                    '    lblstatus = "Objective weight not properly configured!"
                    '    Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    '    btprevious.Disabled = True
                    '    btnext.Disabled = True
                    '    btsave.Disabled = True
                    '    btSubmit.Disabled = True
                    '    'Process.DisableButton(btnPrevious)
                    '    'Process.DisableButton(btnNext)
                    '    'Process.DisableButton(btnSave)
                    '    'Process.DisableButton(btnSubmit)
                    '    Exit Sub
                    'End If

                    'If strUser.Tables(0).Rows(0).Item("completed").ToString.ToLower <> "yes" Then
                    '    lblstatus = "Feedback cannot commence if Objectives setup haven't been completed!"
                    '    Process.loadalert(divalert, msgalert, lblstatus, "danger")
                    '    'Process.DisableButton(btnPrevious)
                    '    'Process.DisableButton(btnNext)
                    '    'Process.DisableButton(btnSave)
                    '    'Process.DisableButton(btnSubmit)
                    '    aMyPerformance.Disabled = True
                    '    btprevious.Disabled = True
                    '    btnext.Disabled = True
                    '    btsave.Disabled = True
                    '    btSubmit.Disabled = True
                    '    Exit Sub
                    'End If

                    'If strUser.Tables(0).Rows(0).Item("CoachApprovalStatus").ToString.ToLower <> "discussed & agreed" Then
                    '    lblstatus = "Feedback cannot commence if Objectives haven't been discussed and agreed!"
                    '    Process.loadalert(divalert, msgalert, lblstatus, "info")

                    '    btprevious.Disabled = True
                    '    btnext.Disabled = True
                    '    btsave.Disabled = True
                    '    btSubmit.Disabled = True
                    '    Exit Sub
                    'End If
                Else
                    Button4.Visible = False
                    Button3.Visible = False
                    divreviewer1.Visible = False
                    divreviewer2.Visible = False
                    lblvisibleI.Text = "NO"
                    lblvisibleII.Text = "NO"
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
                    Process.LoadRadComboTextAndValueP1(cboEmployee, "Emp_PersonalDetail_Get_Employees", Session("Organisation"), "name", "empid", False)
                    reviewyear.Value = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select Period from Performance_Appraisal_Cycle where status = 'open' and company = '" + Session("Organisation") + "'")

                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_PersonalDetail_get", Session("UserEmpID"))
                    If strUser.Tables(0).Rows.Count > 0 Then
                        jdept.Value = strUser.Tables(0).Rows(0).Item("office").ToString
                        jgrade.Value = strUser.Tables(0).Rows(0).Item("grade").ToString
                        jtitle.Value = strUser.Tables(0).Rows(0).Item("jobtitle").ToString
                        jname.Value = strUser.Tables(0).Rows(0).Item("fullname").ToString
                        Session("rev_empid") = strUser.Tables(0).Rows(0).Item("EmpID").ToString
                    End If

                    lblQuestCount.Text = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Performance_Appraisal_Nugget_Questionaire_Count", Session("NuggetID"))
                    Session("QuestionNo") = 1

                    If lblQuestCount.Text > 0 Then
                        nuggetsquestion.Style.Add("display", "block")
                        LoadQuestionaire(Session("QuestionNo"), Session("NuggetID"), ajobgrade.Value)
                        apageview.InnerText = "1 of " & lblQuestCount.Text
                    Else
                        aQuestNo.InnerText = "0."
                        'lblstatus = "Save details to begin feedback nuggets"
                        Process.loadalert(divalert, msgalert, lblstatus, "info")
                    End If
                End If
            End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Protected Sub btnClose_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect("~/Module/Employee/Performance/AppraisalFeedBackNuggetsList", True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
            nuggetsquestion.Style.Add("display", "none")
            topdetails.Style.Add("display", "block")
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

    Protected Sub btnStart_Click(sender As Object, e As EventArgs)
        Try
            If ajobgrade.Value = "" Or ajobtitle.Value = "" Then
                Process.loadalert(divalert, msgalert, "Please select an Employee", "info")
                Exit Sub
            End If
            Dim appID As String = randomString(6)
            Dim coreValue As DataSet
            coreValue = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Competency_Group_Get_Appraisal2", ajobgrade.Value)
            If coreValue.Tables(0).Rows.Count > 0 Then
                Dim c As Integer = coreValue.Tables(0).Rows.Count
                For i As Integer = 0 To c - 1
                    Dim cat As String = coreValue.Tables(0).Rows(i).Item("CompetencyType").ToString
                    Session("NuggetID") = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Performance_Appraisal_Nugget_Save_Details", cboEmployee.SelectedItem.Text, cboEmployee.SelectedValue, ajobgrade.Value, ajobtitle.Value, alenofservice.Value, reviewyear.Value, dept.Value, jname.Value, Session("UserEmpID"), jdept.Value, jgrade.Value, jtitle.Value, cat, appID)
                Next
            End If
            Dim strUser As New DataSet
            strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Appraisal_Nugget_Get", Session("NuggetID"), Session("Organisation"))
            lblreviewer.Text = strUser.Tables(0).Rows(0).Item("CoachID").ToString
            lblrevieweremail.Text = strUser.Tables(0).Rows(0).Item("CoachName").ToString
            txtEmpID.Text = strUser.Tables(0).Rows(0).Item("rev_empid").ToString

            nuggetsquestion.Style.Add("display", "block")
            topdetails.Style.Add("display", "none")
            lblid.Text = Session("NuggetID")
            lblQuestCount.Text = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Performance_Appraisal_Nugget_Questionaire_Count", Session("NuggetID"))
            Session("QuestionNo") = 1

            If lblQuestCount.Text > 0 Then
                nuggetsquestion.Style.Add("display", "block")
                LoadQuestionaire(Session("QuestionNo"), Session("NuggetID"), ajobgrade.Value)
                apageview.InnerText = "1 of " & lblQuestCount.Text
            Else
                aQuestNo.InnerText = "0."
                Dim lblstatus As String = "KPI Category has not been set for '" + Session("Organisation") + "'"
                Process.loadalert(divalert, msgalert, lblstatus, "info")
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
            topdetails.Style.Add("display", "none")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Private Shared random As Random = New Random()

    Private Function randomString(ByVal size As Integer) As String
        Dim input As String = "abcdefghijklmnopqrstuvwxyz0123456789"
        Dim chars = Enumerable.Range(0, size).[Select](Function(x) input(random.[Next](0, input.Length)))
        Return New String(chars.ToArray())
    End Function


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
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_Nugget_Questionaire_Update", lblQuestID.Text, lblid.Text, akpitype.InnerText, aObjective.Value, aMyObjective.InnerText, aMyPerformance.Value, lblMyRating.Text, amanager1.Value, lblMgrRating.Text, ajobgrade.Value, amanager2.Value.Trim, lblMgrRating2.Text)
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
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_Nugget_Questionaire_Update", lblQuestID.Text, lblid.Text, akpitype.InnerText, aObjective.Value, aMyObjective.InnerText, aMyPerformance.Value, lblMyRating.Text, amanager1.Value.Trim, lblMgrRating.Text, ajobgrade.Value, amanager2.Value.Trim, lblMgrRating2.Text)
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
            Process.loadalert(divalert, msgalert, lblstatus, "danger")
            'If reviewend.Value > Now.Date Then
            '    lblstatus = "Appraisal cannot finish not until after " & Process.DDMONYYYY(reviewend.Value)
            '    Process.loadalert(divalert, msgalert, lblstatus, "danger")
            '    Exit Sub
            'End If
            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_Nugget_Questionaire_Update", lblQuestID.Text, lblid.Text, akpitype.InnerText, aObjective.Value, aMyObjective.InnerText, aMyPerformance.Value.Trim, lblMyRating.Text, amanager1.Value.Trim, lblMgrRating.Text, ajobgrade.Value, amanager2.Value.Trim, lblMgrRating2.Text)

            If txtEmpID.Text <> Session("UserEmpID") Then
                nuggetsquestion.Style.Add("display", "none")
                topdetails.Style.Add("display", "block")
            Else
                MultiView1.ActiveViewIndex = 1
                topdetails.Style.Add("display", "block")
                'nuggetsquestion.Style.Add("display", "none")
                divrecommendation.Visible = False
                'gridsss.Style.Add("display", "block")
                reviewerdetails.Style.Add("display", "none")
                'revieweedetails.Style.Add("display", "block")
            End If

            gridReviewee.Visible = True
            LoadReviewee(lblid.Text, Session("Organisation"))

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Protected Sub btnSubmitReview_Click(sender As Object, e As EventArgs) Handles btSubmitReview.Click
        Try

            Dim lblstatus As String = ""
            Dim confirmValue As String = Request.Form("confirm_value")
            If confirmValue = "Yes" Then
                If txtEmpID.Text = Session("UserEmpID") Then
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, CommandType.Text, "update Performance_Appraisal_Nuggets set rev_submitted = 'Yes' where AppraisalItem ='" + lblid.Text + "'")
                    Process.Appraisal_Nugget_Submit(cboEmployee.SelectedValue, reviewyear.Value, cboEmployee.Text, jname.Value, lblrevieweremail.Text, lblreviewer.Text, Process.ApplicationURL & "/" & "Module/Employee/Performance/AppraisalFeedBackNuggetsManagerList")
                    Process.Appraisal_Nugget_Emp_Submit(cboEmployee.SelectedValue, reviewyear.Value, cboEmployee.Text, jname.Value, cboEmployee.Text, cboEmployee.SelectedValue, Process.ApplicationURL & "/" & "Module/Employee/Performance/AppraisalFeedBackNuggetsOwnerList")
                    Process.Appraisal_Nugget_Rev_Submit(cboEmployee.SelectedValue, reviewyear.Value, cboEmployee.Text, jname.Value, cboEmployee.Text, Session("rev_empid"), Process.ApplicationURL & "/" & "Module/Employee/Performance/AppraisalFeedBackNuggetsList")
                ElseIf lblreviewer.Text = Session("UserEmpID") Then
                    If cborecommendation.SelectedItem.Text.ToLower.Contains("--select") = True Then
                        lblstatus = "recommendation required!"
                        Process.loadalert(divalert, msgalert, lblstatus, "warning")

                        Exit Sub
                    End If
                    'SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_Review_Recommendation", lblid.Text, cborecommendation.SelectedItem.Text, "reviewer1")
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, CommandType.Text, "update Performance_Appraisal_Nuggets set rev_submitted = Yes where AppraisalItem ='" + lblid.Text + "'")
                    'If lblreviewer2.Text.ToLower = "n/a" Then
                    '    Process.Appraisal_Review_Complete(txtEmpID.Text, Process.DDMONYYYY(reviewstart.Value) & ":" & Process.DDMONYYYY(reviewend.Value), aname.Value, txtdept.Text, Process.ApplicationURL & "/" & Process.GetMailLink(AuthenCode, 0))
                    'Else
                    '    Process.Appraisal_Review_Submit(txtEmpID.Text, Process.DDMONYYYY(reviewstart.Value) & ":" & Process.DDMONYYYY(reviewend.Value), aname.Value, areviewer2.Value, lblreviewer2.Text, Process.ApplicationURL & "/" & Process.GetMailLink(AuthenCode, 2))
                    'End If

                ElseIf lblreviewer2.Text = Session("UserEmpID") Then
                    'Dim strUser As New DataSet
                    'strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Appraisal_Summary_Get", lblid.Text)
                    'If strUser.Tables(0).Rows(0).Item("mgrsubmited").ToString.ToLower = "no" Then
                    '    lblstatus = "First Reviewer will require to review appraisal before you submit"
                    '    Process.loadalert(divalert, msgalert, lblstatus, "warning")

                    '    Exit Sub
                    'Else
                    '    If cborecommendation.SelectedItem.Text.ToLower.Contains("--select") = True Then
                    '        lblstatus = "recommendation required!"
                    '        Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    '        Exit Sub
                    '    End If
                    '    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_Review_Recommendation", lblid.Text, cborecommendation.SelectedItem.Text, "reviewer2")
                    '    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_Review_Submit", lblid.Text, "reviewer2")
                    '    Process.Appraisal_Review_Complete(txtEmpID.Text, Process.DDMONYYYY(reviewstart.Value) & ":" & Process.DDMONYYYY(reviewend.Value), aname.Value, txtdept.Text, Process.ApplicationURL & "/" & Process.GetMailLink(AuthenCode, 0))
                    '    Process.DisableButton(btnDisagree)
                    'End If


                End If
                btSubmitReview.Enabled = False
                'Process.DisableButton(btnSubmitReview)
                Button1.Disabled = True
                Process.loadalert(divalert, msgalert, "Submit Successful", "success")
                MultiView1.ActiveViewIndex = 2
                nuggetsquestion.Style.Add("display", "none")
                topdetails.Style.Add("display", "block")
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

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            Dim lblstatus As String = ""
            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_Nugget_Questionaire_Update", lblQuestID.Text, lblid.Text, akpitype.InnerText, aObjective.Value, aMyObjective.InnerText, aMyPerformance.Value.Trim, lblMyRating.Text, amanager1.Value.Trim, lblMgrRating.Text, ajobgrade.Value, amanager2.Value.Trim, lblMgrRating2.Text)
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
            'Dim url As String = "feedbackselection.aspx?id=" & lblid.Text & "&empid=" & txtEmpID.Text & "&period=" & Process.DDMONYYYY(reviewstart.Value) & ":" & Process.DDMONYYYY(reviewend.Value)
            'Dim s As String = "window.open('" & url + "', 'popup_window', 'width=900,height=600,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no');"
            'ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub gridReviewee_DataBound(sender As Object, e As EventArgs) Handles gridReviewee.DataBound
        Try
            'If txtEmpID.Text = Session("UserEmpID") Then
            '    If lblvisibleI.Text = "NO" Then
            '        gridReviewee.Columns(5).Visible = False
            '        gridReviewee.Columns(6).Visible = False
            '    End If
            '    If lblvisibleII.Text = "NO" Then
            '        gridReviewee.Columns(7).Visible = False
            '        gridReviewee.Columns(8).Visible = False
            '    End If
            'ElseIf lblreviewer.Text = Session("UserEmpID") Then
            '    'If areviewer2.Value.Trim = "" Or areviewer2.Value.Trim.ToLower = "n/a" Then
            '    '    gridReviewee.Columns(7).Visible = False
            '    '    gridReviewee.Columns(8).Visible = False
            '    'End If
            'End If

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
            Dim table As DataTable = Process.SearchData("Performance_Appraisal_NuggetList_Review", lblid.Text)
            table.DefaultView.Sort = sortExpression & direction
            gridReviewee.PageIndex = CInt(Session("appfeedbacklistPageIndex"))
            gridReviewee.DataSource = table
            gridReviewee.DataBind()
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


    Protected Sub GridVwHeaderChckbox_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gridReviewee.PageIndexChanging
        Try
            gridReviewee.PageIndex = e.NewPageIndex
            Session("appfeedbacklistPageIndex") = e.NewPageIndex
            gridReviewee.DataSource = Process.SearchData("Performance_Appraisal_NuggetList_Review", lblid.Text)
            gridReviewee.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub OnRowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gridReviewee, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub

    Protected Sub btnDisagree_Click(sender As Object, e As EventArgs) Handles btnDisagree.Click

        Try
            Dim lblstatus As String = ""

            Dim confirmValue As String = Request.Form("confirmplan_value")
            If confirmValue = "Yes" Then
                Process.disagree = 0
                Session("ManagerEmail") = lblrevieweremail.Text
                'Dim url As String = "appraisaldisagree.aspx?empname=" & aname.Value & "&cycle=" & Process.DDMONYYYY(reviewstart.Value) & " : " & Process.DDMONYYYY(reviewend.Value) & "&reviewer=" & lblreviewer.Text & "&id=" & lblid.Text
                'Dim s As String = "window.open('" & url + "', 'popup_window', 'width=500,height=400,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no');"
                'ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)
            Else
                lblstatus = "Process cancelled"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
            End If



        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try

    End Sub
End Class