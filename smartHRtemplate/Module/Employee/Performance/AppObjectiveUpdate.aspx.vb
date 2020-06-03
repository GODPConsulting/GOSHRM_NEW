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


Public Class AppObjectiveUpdate
    Inherits System.Web.UI.Page
    Dim education As New clsEducation
    Dim Separators() As Char = {","c}
    Dim AuthenCode As String = "EMPMYGOAL"
    Dim AuthenCode2 As String = "TEAMPERF"
    Dim AuthenCode3 As String = "APPPERF"
    Dim rowCounts As Integer = 0
    Public AppID As Integer = 0
    Dim success, obj, key, tdate, kpitype As String



    Protected Sub ToggleRowSelection(ByVal sender As Object, ByVal e As EventArgs)

        TryCast(TryCast(sender, CheckBox).NamingContainer, GridItem).Selected = TryCast(sender, CheckBox).Checked
        Dim checkHeader As Boolean = True
        For Each dataItem As GridDataItem In gridCompetency.MasterTableView.Items
            If Not TryCast(dataItem.FindControl("CheckBox1"), CheckBox).Checked Then
                checkHeader = False
                Exit For
            End If
        Next
        Dim headerItem As GridHeaderItem = TryCast(gridCompetency.MasterTableView.GetItems(GridItemType.Header)(0), GridHeaderItem)
        TryCast(headerItem.FindControl("headerChkbox"), CheckBox).Checked = checkHeader
    End Sub
    Protected Sub ToggleSelectedState(ByVal sender As Object, ByVal e As EventArgs)
        Dim headerCheckBox As CheckBox = TryCast(sender, CheckBox)
        For Each dataItem As GridDataItem In gridCompetency.MasterTableView.Items
            TryCast(dataItem.FindControl("CheckBox1"), CheckBox).Checked = headerCheckBox.Checked
            dataItem.Selected = headerCheckBox.Checked
        Next
    End Sub
    

    Private Sub LoadData(dataid As Integer)
        Try
            gridCompetency.DataSource = Process.SearchData("Performance_Appraisal_Get_All", dataid)

            'Getting custom Names
            success = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select SuccessTarget from Performance_Custom_Naming")
            obj = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select Objectives from Performance_Custom_Naming")
            key = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select KeyAction from Performance_Custom_Naming")
            tdate = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select TargetDate from Performance_Custom_Naming")
            kpitype = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select KPIType from Performance_Custom_Naming")

            If kpitype = "" Then
                gridCompetency.Columns(2).HeaderText = "KPI Type"
            Else
                gridCompetency.Columns(2).HeaderText = kpitype
            End If
            If obj = "" Then
                gridCompetency.Columns(3).HeaderText = "Objectives"
            Else
                gridCompetency.Columns(3).HeaderText = obj
            End If
            If success = "" Then
                gridCompetency.Columns(4).HeaderText = "Success Target"
            Else
                gridCompetency.Columns(4).HeaderText = success
            End If
            If key = "" Then
                gridCompetency.Columns(5).HeaderText = "Key Action"
            Else
                gridCompetency.Columns(5).HeaderText = key
            End If
            If tdate = "" Then
                gridCompetency.Columns(6).HeaderText = "Target Date"
            Else
                gridCompetency.Columns(6).HeaderText = tdate
            End If

            gridCompetency.DataBind()

            'If gridCompetency.MasterTableView.Items.Count > 0 And txtEmpID.Text = Session("UserEmpID") Then
            '    btnComplete.Visible = True
            '    btnDisagree.Visible = True
            'End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub


    Private Function GetIdentity(ByVal cycleid As Integer, ByVal empid As String, ByVal coachid As String, ByVal supervisorid As String, ByVal comment As String, ByVal supervisorid2 As String, ByVal jobgrade As String) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Performance_Appraisal_Summary_Update"
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@AppraisalCycleID", SqlDbType.Int).Value = cycleid
            cmd.Parameters.Add("@EmpID", SqlDbType.VarChar).Value = empid
            cmd.Parameters.Add("@CoachID", SqlDbType.VarChar).Value = coachid
            cmd.Parameters.Add("@SupervisorID", SqlDbType.VarChar).Value = supervisorid
            cmd.Parameters.Add("@EmpComment", SqlDbType.VarChar).Value = comment
            cmd.Parameters.Add("@SupervisorID2", SqlDbType.VarChar).Value = supervisorid2
            cmd.Parameters.Add("@ApplySystemWeight", SqlDbType.VarChar).Value = ""
            cmd.Parameters.Add("@JobGrade", SqlDbType.VarChar).Value = jobgrade
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
            Return "0"
        End Try
    End Function

    Private Sub GenerateTotalColor()
        Try
            Dim strValid As String = ""
            Dim strlabel As String = ""

            If lblid.Text <> "0" And lblid.Text <> "" Then
                Dim strWeight As New DataSet
                strWeight = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Appraisal_Total_Check", lblid.Text)
                If strWeight.Tables(0).Rows.Count > 0 Then
                    For h As Integer = 0 To strWeight.Tables(0).Rows.Count - 1
                        If CDbl(strWeight.Tables(0).Rows(h).Item("weights").ToString) > 100 Then
                            strValid = "excess"
                        ElseIf CDbl(strWeight.Tables(0).Rows(h).Item("weights").ToString) < 100 Then
                            strValid = "incomplete"
                        ElseIf CDbl(strWeight.Tables(0).Rows(h).Item("weights").ToString) = 0 Then
                            strValid = "zero"
                        End If
                        strlabel = strlabel & ", " & strWeight.Tables(0).Rows(h).Item("KPIType").ToString & ": " & strWeight.Tables(0).Rows(h).Item("weights").ToString
                    Next
                Else
                    strValid = "blank"
                End If

                'lblTotal0.Text = totalratio
                If strValid = "incomplete" Then
                    lbltotalweight.Attributes.Add("style", "background-color:Yellow; color: Black")
                    lblTotal.BackColor = Color.Yellow
                    lbltotalweight.InnerText = "Total Weight: " & strlabel
                    btnComplete.Visible = False
                    btnAgreed.Visible = False
                ElseIf strValid = "excess" Then
                    lbltotalweight.Attributes.Add("style", "background-color:Red; color: White")
                    lblTotal.BackColor = Color.Red
                    lbltotalweight.InnerText = "Total Weight: " & strlabel
                    btnComplete.Visible = False
                    btnAgreed.Visible = False
                ElseIf strValid = "blank" Then
                    btnComplete.Visible = False
                    btnAgreed.Visible = False
                    lbltotalweight.InnerText = ""
                ElseIf strValid = "zero" Then
                    lblTotal.BackColor = Color.Aqua
                    btnComplete.Visible = False
                    btnAgreed.Visible = False
                    lbltotalweight.InnerText = ""
                Else
                    lbltotalweight.InnerText = "Total Weight: " & strlabel
                    lbltotalweight.Attributes.Add("style", "background-color:Green; color: White")
                    lblTotal.BackColor = Color.Green

                    If Session("ManagerID") = Session("UserEmpID") Then
                        btnAgreed.Visible = True
                        mgrSavebtn.Visible = True
                        mgrSavebtn.Style.Add("display", "block")
                        continueBTN.Visible = False
                        p.Style.Add("display", "block")
                    End If

                End If

                Dim strUser As New DataSet
                strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Appraisal_Preference_Get", Session("Access"))
                If strUser.Tables(0).Rows.Count > 0 Then
                    If strUser.Tables(0).Rows(0).Item("ReviewerObjVisible").ToString = "Yes" Then
                        btnlink.Visible = True
                    Else
                        btnlink.Visible = False
                    End If
                Else
                    btnlink.Visible = False
                End If
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Sub LoadDetail(id As String)
        Dim strUser As New DataSet
        strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Appraisal_Summary_Get", id)
        lblid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
        'GenerateTotalColor()
        'Process.AssignRadComboValue(cboDevPlan, strUser.Tables(0).Rows(0).Item("reviewyear").ToString)
        cboDevPlan.Text = strUser.Tables(0).Rows(0).Item("reviewyear").ToString
        Process.LoadRadComboTextAndValueInitiateP2(cboStartReview, "Performance_Appraisal_Cycle_Get_Period_1", cboDevPlan.SelectedValue, Session("Organisation"), "--Select Review Period--", "Period", "id")

        txtEmpID.Text = strUser.Tables(0).Rows(0).Item("empid").ToString
        Process.AssignRadComboValue(cboStartReview, strUser.Tables(0).Rows(0).Item("cycleid").ToString)
        aname.Value = strUser.Tables(0).Rows(0).Item("empname").ToString
        ajobtitle.Value = strUser.Tables(0).Rows(0).Item("jobtitle").ToString
        ajobgrade.Value = strUser.Tables(0).Rows(0).Item("jobgrade").ToString
        txtDept.Text = strUser.Tables(0).Rows(0).Item("dept").ToString
        txtLocation.Text = strUser.Tables(0).Rows(0).Item("location").ToString
        aempcomment.Value = strUser.Tables(0).Rows(0).Item("EmpComment").ToString
        Session("emp_emailaddr") = strUser.Tables(0).Rows(0).Item("empmail").ToString

        alinemanager.Value = strUser.Tables(0).Rows(0).Item("SupervisorName").ToString
        Session("ManagerID") = strUser.Tables(0).Rows(0).Item("supervisorid").ToString
        'txtManagerGrade.Text = strUser.Tables(0).Rows(0).Item("SupervisorGrade").ToString
        txtManagerMail.Text = strUser.Tables(0).Rows(0).Item("SupervisorEmail").ToString
        amgrcomment.Value = strUser.Tables(0).Rows(0).Item("Comment").ToString


        Session("ReviewerI_ID") = strUser.Tables(0).Rows(0).Item("coachid").ToString
        areviewer1.Value = strUser.Tables(0).Rows(0).Item("CoachName").ToString
        txtemailreviewer.Text = strUser.Tables(0).Rows(0).Item("Coachemail").ToString
       
        Session("ReviewerII_ID") = strUser.Tables(0).Rows(0).Item("supervisorid2").ToString
        areviewer2.Value = strUser.Tables(0).Rows(0).Item("Supervisor2Name").ToString
        txtemailreviewer2.Text = strUser.Tables(0).Rows(0).Item("SupervisorEmail2").ToString

        'CoachApprovalStatus
        If (strUser.Tables(0).Rows(0).Item("CoachApprovalStatus").ToString.ToLower.Contains("pending") = True) And (strUser.Tables(0).Rows(0).Item("Completed").ToString.ToLower.Contains("yes") = True) Then
            'asign.Visible = True
            btnComplete.Visible = False
            btnComplete.Enabled = False
            SaveButton.Visible = False
            Btngridback.Visible = False
            'btnupdate.Visible = False
            continueBTN.Visible = False
            btnrefresh.Visible = False
            btnadd.Visible = False
            btnDeleteGrid.Visible = True
            aempcomment.Attributes.Add("readonly", "readonly")
            kpigroup.Visible = True
            p.Style.Add("display", "block")
            Btngridback.Visible = False
            asign.InnerText = ""
        ElseIf strUser.Tables(0).Rows(0).Item("CoachApprovalStatus").ToString.ToLower.Contains("disagree") = True Then
            asign.Visible = True
            asign.InnerText = "Disagreed By " & strUser.Tables(0).Rows(0).Item("SupervisorName")
            btnadd.Visible = True
            btnrefresh.Visible = True
            btnDeleteGrid.Visible = True
            btnComplete.Enabled = True
            btnComplete.Visible = True
            If Session("ManagerID") = Session("UserEmpID") Then
                btnAgreed.Visible = True
                mgrSavebtn.Visible = True
                continueBTN.Visible = False
                p.Style.Add("display", "block")
            End If
            btnupdate.Visible = True
            'continueBTN.Visible = True
            aempcomment.Attributes.Remove("readonly")
        ElseIf strUser.Tables(0).Rows(0).Item("Completed").ToString.ToLower.Contains("yes") = False Then
            'asign.Visible = True
            btnComplete.Enabled = True
            'btnupdate.Visible = True
            continueBTN.Visible = True
            btnrefresh.Visible = True
            btnadd.Visible = True
            btnDeleteGrid.Visible = True
            aempcomment.Attributes.Add("readonly", "readonly")
        ElseIf strUser.Tables(0).Rows(0).Item("CoachApprovalStatus").ToString.ToLower.Contains("discussed & agreed") = True Then
            Update.Visible = False
            'asign.Visible = True
            Ryear.Visible = False
            btnComplete.Enabled = False
            'btnupdate.Visible = False
            continueBTN.Visible = False
            btnrefresh.Visible = False
            btnadd.Visible = False
            btnDeleteGrid.Visible = False
            kpigroup.Visible = False
            p.Style.Add("display", "block")
            SaveButton.Visible = False
            Btngridback.Visible = False
            Update.Visible = True
            aempcomment.Attributes.Add("readonly", "readonly")
        ElseIf (strUser.Tables(0).Rows(0).Item("empsubmited").ToString.ToLower.Contains("yes")) = strUser.Tables(0).Rows(0).Item("CoachApprovalStatus").ToString.ToLower.Contains("discussed & agreed") = True Then
            Update.Visible = False
            Ryear.Visible = False
            btnComplete.Enabled = False
            'btnupdate.Visible = False
            continueBTN.Visible = False
            btnrefresh.Visible = False
            btnadd.Visible = False
            btnDeleteGrid.Visible = False
            aempcomment.Attributes.Add("readonly", "readonly")
        Else
            asign.Visible = True
            btnrefresh.Visible = False
            btnadd.Visible = False
            btnDeleteGrid.Visible = False
            aempcomment.Attributes.Add("readonly", "readonly")

            asign.InnerText = "Discussed & Agreed By " & strUser.Tables(0).Rows(0).Item("SupervisorName") & " and " & strUser.Tables(0).Rows(0).Item("empname") & " on " & CDate(strUser.Tables(0).Rows(0).Item("coachapprovedate")).ToLongDateString
        End If

        Session("UserJobgrade") = ajobgrade.Value

        LoadData(id)

        If strUser.Tables(0).Rows(0).Item("CoachApprovalStatus").ToString.ToUpper.Contains("AGREED") And strUser.Tables(0).Rows(0).Item("CoachApprovalStatus").ToString.ToUpper.Contains("DISCUSS") Then
            'btnAgreed.Enabled = False
            cboDevPlan.Enabled = False
            txtManagerMail.Enabled = False
            txtdept.Enabled = False
            btnadd.Visible = False
            btnDeleteGrid.Visible = False
        End If

        lblapproval.Text = strUser.Tables(0).Rows(0).Item("CoachApprovalStatus").ToString
        '
        If Session("ReviewerI_ID") = Session("UserEmpID") Or Session("ReviewerII_ID") = Session("UserEmpID") Then
            btnupdate.Visible = False
            btnComplete.Visible = False
            btnAgreed.Visible = False
            btnDisagree.Visible = False
            btnlink.Visible = False
        End If

        If Session("ManagerID") = Session("UserEmpID") Then
            amgrcomment.Attributes.Remove("readonly")
            btnAgreed.Visible = True
            btnDisagree.Visible = True
            Update.Visible = True
            btnupdate.Visible = False
            aempcomment.Attributes.Add("readonly", "readonly")
            btnlink.Visible = False
            mgrSavebtn.Visible = True
            continueBTN.Visible = False
            p.Style.Add("display", "block")
        Else
            amgrcomment.Attributes.Add("readonly", "readonly")
            btnAgreed.Visible = False
            btnDisagree.Visible = False
        End If
        cboDevPlan.Enabled = False
        cboStartReview.Enabled = False
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not Me.IsPostBack Then
                Session("objPreviousPage") = Request.UrlReferrer.ToString
                Dim msg As String = ""
                Dim endDaTE As Date = Date.Now
                Process.LoadRadComboTextAndValueP1(cboDevPlan, "Performance_Appraisal_Cycle_Get_Distinct", Session("Organisation"), "reviewyear", "reviewyear", False)
                If cboDevPlan.Items.Count <= 0 Then
                    msg = "No Appraisal cycle set on " & Session("Organisation") & "!"
                    Process.loadalert(divalert, msgalert, msg, "warning")
                    Exit Sub
                End If

                If Request.QueryString("id") IsNot Nothing Then
                    continueBTN.Style.Add("display", "block")
                    btnupdate.Style.Add("display", "none")
                    If Session("save") = "true" Then
                        ob.Style.Add("display", "none")
                        p.Style.Add("display", "block")
                    End If
                    Session("save") = ""

                    AppID = Request.QueryString("id")

                    LoadDetail(Request.QueryString("id"))
                    Dim coreValue As DataSet
                    Dim yy As StringBuilder = New StringBuilder("")
                    coreValue = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Competency_Group_Get_Appraisal1", ajobgrade.Value)
                    If coreValue.Tables(0).Rows.Count > 0 Then
                        Dim c As Integer = coreValue.Tables(0).Rows.Count
                        For i As Integer = 0 To c - 1
                            Dim cat As String = coreValue.Tables(0).Rows(i).Item("CompetencyType").ToString
                            yy.Append("<div class='col-md-3'>" + cat + "<a href='#' class='btn btn-default rounded' style='margin-left:4px;' onclick='corevalues(""" + cat + """)' data-toggle='modal' data-target='#loginModal'><i class='fa fa-plus'></i></a></div>")
                        Next
                    Else
                        yy.Append("<div class='col-md-4'><input type='text' value='Employee cannot set Objective' readonly class='form-control' width='100%' /></div>")
                    End If
                    Dim ww As String = yy.ToString()
                    kpigroup.InnerHtml = ww

                Else
                    Process.AssignRadComboValue(cboDevPlan, Now.Year.ToString)
                    Process.LoadRadComboTextAndValueInitiateP3(cboStartReview, "Performance_Appraisal_Cycle_Get_Period_New", cboDevPlan.SelectedValue, Session("Organisation"), Session("UserEmpID"), "--Select Review Period--", "Period", "id")

                    lblid.Text = "0"

                    txtEmpID.Text = Session("UserEmpID")

                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Work_History_Get_Actual", txtEmpID.Text, Now.Date)
                    If strUser.Tables(0).Rows.Count > 0 Then
                        txtdept.Text = strUser.Tables(0).Rows(0).Item("office").ToString
                        txtlocation.Text = strUser.Tables(0).Rows(0).Item("location").ToString
                        aname.Value = strUser.Tables(0).Rows(0).Item("EmpName").ToString
                        ajobgrade.Value = strUser.Tables(0).Rows(0).Item("Grade").ToString
                        ajobtitle.Value = strUser.Tables(0).Rows(0).Item("JobTitle").ToString
                        Session("emp_emailaddr") = strUser.Tables(0).Rows(0).Item("empmail").ToString

                        alinemanager.Value = strUser.Tables(0).Rows(0).Item("MgrFullName").ToString
                        Session("ManagerID") = strUser.Tables(0).Rows(0).Item("mgrid").ToString
                        'txtManagerGrade.Text = strUser.Tables(0).Rows(0).Item("mgrGrade").ToString
                        txtManagerMail.Text = strUser.Tables(0).Rows(0).Item("mgremail").ToString

                        areviewer1.Value = strUser.Tables(0).Rows(0).Item("CoachFullName").ToString
                        Session("ReviewerI_ID") = strUser.Tables(0).Rows(0).Item("coachid").ToString
                        'txtreviewergrade.Text = strUser.Tables(0).Rows(0).Item("coachgrade").ToString
                        txtemailreviewer.Text = strUser.Tables(0).Rows(0).Item("coachmail").ToString

                        'reviewer 2
                        areviewer2.Value = strUser.Tables(0).Rows(0).Item("MgrIIFullName").ToString
                        Session("ReviewerII_ID") = strUser.Tables(0).Rows(0).Item("mgridII").ToString
                        'txtreviewergrade2.Text = strUser.Tables(0).Rows(0).Item("mgrGradeII").ToString
                        txtemailreviewer2.Text = strUser.Tables(0).Rows(0).Item("mgremailII").ToString

                        If Session("ManagerID") = Session("UserEmpID") Then
                            amgrcomment.Attributes.Remove("readonly")
                            btnAgreed.Visible = True
                            btnDisagree.Visible = True
                            mgrSavebtn.Visible = True
                            Btngridback.Visible = False
                            'btnupdate.Visible = False
                            aempcomment.Attributes.Add("readonly", "readonly")
                            btnlink.Visible = False
                        Else
                            amgrcomment.Attributes.Add("readonly", "readonly")
                        End If
                    End If


                    LoadData(lblid.Text)
                    btnadd.Visible = False
                    btnDeleteGrid.Visible = False
                    btnrefresh.Visible = False
                    btnComplete.Visible = False
                    Session("UserJobgrade") = ajobgrade.Value

                    btnAgreed.Visible = False
                    btnDisagree.Visible = False

                End If
            End If

            Dim isV As String = ""
            Dim sst As New DataSet
            sst = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Appraisal_Preference_Get", Session("Access"))
            If sst.Tables(0).Rows.Count > 0 Then
                isV = sst.Tables(0).Rows(0).Item("reviewerobjvisible").ToString
            End If

            If txtEmpID.Text = Session("UserEmpID") Then
                btnlink.Visible = True
                btnDisagree.Visible = False
                btnAgreed.Visible = False
            Else
                btnlink.Visible = False
            End If
            GenerateTotalColor()

            Dim customName As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select PerformanceObjective from Performance_Custom_Naming")
            If (customName = "") Then
                page_title.InnerText = "Performance Objective: " & lblapproval.Text
                modal_title.InnerText = "PERFORMANCE OBJECTIVE FORM"
            Else
                page_title.InnerText = customName + ": " & lblapproval.Text
                modal_title.InnerText = customName + " FORM"
            End If
            AppID = lblid.Text
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnAdd_Click1(sender As Object, e As EventArgs)
        ob.Style.Add("display", "none")
        p.Style.Add("display", "block")
        AppID = lblid.Text
    End Sub

    Protected Sub btnAdd_Click2(sender As Object, e As EventArgs)
        ob.Style.Add("display", "block")
        p.Style.Add("display", "none")
        AppID = lblid.Text
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            Session("save") = "true"
            If cboStartReview.Text = "--Select Review Period--" Or cboStartReview.Text = "" Then
                Process.loadalert(divalert, msgalert, "Please Select a review period", "danger")
            End If

            Dim msg As String = ""
            If Session("UserEmpID") = Session("ManagerID") Then
                If amgrcomment.Value.Trim = "" Then
                    msg = "Comment required!"
                    Process.loadalert(divalert, msgalert, msg, "warning")
                    amgrcomment.Focus()
                    Exit Sub
                End If
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_Summary_SaveComment", lblid.Text, amgrcomment.Value)
                msg = "Saved"
                Process.loadalert(divalert, msgalert, "Saved", "success")
            Else
                If lblid.Text <> "0" Then
                    If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False And Process.AuthenAction(Session("role"), AuthenCode2, "Update") = False And Process.AuthenAction(Session("role"), AuthenCode3, "Update") = False Then
                        msg = "You don't have privilege to perform this action!"
                        Process.loadalert(divalert, msgalert, msg, "warning")
                        Exit Sub
                    End If
                End If


                If cboStartReview.SelectedValue Is Nothing Or cboStartReview.Text = "--Select Review Period--" Then
                    msg = "Select Review Period!"
                    Process.loadalert(divalert, msgalert, msg, "danger")
                    cboStartReview.Focus()
                    Exit Sub
                End If

                If cboStartReview.SelectedValue = "" Then
                    msg = "Select Review Period!"
                    Process.loadalert(divalert, msgalert, msg, "danger")
                    cboStartReview.Focus()
                    Exit Sub
                End If

                'Work History
                If lblid.Text = "0" Then
                    lblid.Text = GetIdentity(cboStartReview.SelectedValue, txtEmpID.Text, Session("ReviewerI_ID"), Session("ManagerID"), aempcomment.Value, Session("ReviewerII_ID"), ajobgrade.Value)
                    If lblid.Text = "0" Then
                        Exit Sub
                    End If
                    Session("id") = "0"
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_Questionaire_Count", lblid.Text, ajobgrade.Value)
                    Response.Redirect("~/Module/Employee/Performance/AppObjectiveUpdate?id=" & lblid.Text, True)
                Else
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_Summary_Update", lblid.Text, cboStartReview.SelectedValue, txtEmpID.Text, Session("ReviewerI_ID"), Session("ManagerID"), aempcomment.Value, Session("ReviewerII_ID"), "", ajobgrade.Value)
                End If


                msg = "Appraisal Objective Saved"
                Process.loadalert(divalert, msgalert, msg, "success")
                ob.Style.Add("display", "none")
                p.Style.Add("display", "block")

                'Process.id = lblid.Text
                If lblid.Text = "0" Then
                    btnadd.Visible = True
                    btnDeleteGrid.Visible = True
                    btnrefresh.Visible = True
                    btnComplete.Visible = True
                Else
                    'asign.Visible = True
                    'btnComplete.Visible = True
                    'btnrefresh.Visible = False
                    'btnadd.Visible = False
                    'btnDeleteGrid.Visible = False
                End If
                ob.Style.Add("display", "none")
                p.Style.Add("display", "block")
                btnupdate.Visible = False
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try

    End Sub

    Protected Sub btnClose_Click(sender As Object, e As EventArgs)
        '"~/Module/Employee/Performance/AppraisalObjectivesForm"
        'Response.Write("<script language='javascript'> { self.close() }</script>")
        Try
            'Response.Redirect(Session("objPreviousPage"), True)
            If Session("objPreviousPage") = "" Then
                If Session("ManagerID") = Session("UserEmpID") Then
                    Response.Redirect("~/Module/Employee/Performance/DirectReportAppraisalObjectivesForm", True)
                ElseIf Session("ReviewerII_ID") = Session("UserEmpID") Then
                    Response.Redirect("~/Module/Employee/Performance/SecondRevewAppraisalObjectivesForm", True)
                Else
                    Response.Redirect("~/Module/Employee/Performance/AppraisalObjectivesForm", True)
                End If
            Else
                Response.Redirect(Session("objPreviousPage"), True)
            End If
            

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub LinkDownLoad(ByVal sender As Object, ByVal e As EventArgs)

        Try
            Process.loadalert(divalert, msgalert, "", "warning")
            Dim sid As String = CType(sender, LinkButton).CommandArgument
            LoadObjectives(sid)
            'Dim url As String = "MyAppObjectives.aspx?id=" & sid
            'Dim s As String = "window.open('" & url + "', 'popup_window', 'width=900,height=750,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no');"
            'ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


    Protected Sub btnAddGrid_Click(sender As Object, e As EventArgs)
        Try
            Process.loadalert(divalert, msgalert, "", "warning")
            'Process.loadtype = "Add"
            Dim msg As String = ""
            If lblid.Text <> "0" Then
                If (asign.InnerText.ToLower.Contains("& agreed") Or asign.InnerText.ToLower.Contains("and agreed")) And asign.Visible = True Then
                    msg = "No more modification can be made to an agreed objective"
                    Process.loadalert(divalert, msgalert, msg, "danger")
                    Exit Sub
                Else
                    MultiView1.ActiveViewIndex = 1
                    Process.LoadRadComboTextAndValue(cboKPICategory, "Competency_Group_Get_All", "Name", "id", False)
                    LoadObjectives(0)
                End If
            Else
                msg = "Save data before adding Appraisal Objectives"
                Process.loadalert(divalert, msgalert, msg, "danger")
                Exit Sub
            End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Protected Sub RadGrid1_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs)
        If e.CommandName = "DoubleClickEdit" Then
            Dim item As GridDataItem = gridCompetency.Items(e.CommandArgument.ToString())
            item.Edit = True
            gridCompetency.MasterTableView.EditMode = GridEditMode.InPlace
            gridCompetency.Rebind()
        ElseIf e.CommandName = RadGrid.EditCommandName Then
            gridCompetency.MasterTableView.EditMode = GridEditMode.EditForms
        End If
    End Sub

    Protected Sub btnDeleteGrid_Click(sender As Object, e As EventArgs) Handles btnDeleteGrid.Click
        Try
            AppID = lblid.Text
            Process.loadalert(divalert, msgalert, "", "warning")
            Dim msg As String = ""
            If Process.AuthenAction(Session("role"), AuthenCode, "Delete") = False And Process.AuthenAction(Session("role"), AuthenCode2, "Delete") = False And Process.AuthenAction(Session("role"), AuthenCode3, "Delete") = False Then
                msg = "You don't have privilege to perform this action"
                Process.loadalert(divalert, msgalert, msg, "warning")
                Exit Sub
            End If

            Dim count As Integer = 0
            Dim confirmValue As String = Request.Form("confirm_value")
            If confirmValue = "Yes" Then
                If (asign.InnerText.ToLower.Contains("& agreed") Or asign.InnerText.ToLower.Contains("and agreed")) And asign.Visible = True Then
                    msg = "No more modification can be made to an agreed objective"
                    Process.loadalert(divalert, msgalert, msg, "warning")
                    Exit Sub
                End If


                Dim atLeastOneRowDeleted As Boolean = False
                Dim myCheckBox As New CheckBox()
                Dim myText As New GridTemplateColumn()
                For Each myItem As GridDataItem In gridCompetency.MasterTableView.Items

                    myCheckBox = DirectCast(myItem("CheckBoxTemplateColumn").FindControl("CheckBox1"), System.Web.UI.WebControls.CheckBox)

                    If myCheckBox IsNot Nothing AndAlso myCheckBox.Checked Then
                        Dim id As Integer = CInt(myItem.Cells(4).Text)

                        Dim strUser As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Metric_Get_All", id)
                        If strUser.Tables(0).Rows.Count > 0 Then
                            Session("kpi") = strUser.Tables(0).Rows(0).Item("KPIType").ToString
                        End If

                        Dim canSetObj As String = "yes"
                        Dim strWeight As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Competency_Group_Get", Session("kpi"))

                        If strWeight.Tables(0).Rows.Count > 0 Then
                            canSetObj = strWeight.Tables(0).Rows(0).Item("EmpSetObjective").ToString.ToLower
                        End If
                        'If canSetObj = "no" Then
                        '    msg = "You can't delete HR Set Objective"
                        '    Process.loadalert(divalert, msgalert, msg, "danger")
                        '    Exit Sub
                        'End If
                        count = count + 1
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_Delete", id)
                    End If
                Next
                Process.loadalert(divalert, msgalert, count.ToString & " records successfully deleted", "success")
                LoadData(lblid.Text)
                AppID = lblid.Text

            End If

            GenerateTotalColor()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Protected Sub btnAgreed_Click(sender As Object, e As EventArgs) Handles btnAgreed.Click
        Try
            Process.loadalert(divalert, msgalert, "", "warning")
            Dim msg As String = ""

            Dim confirmValue As String = Request.Form("confirmplan_value")
            If confirmValue = "Yes" Then
                If amgrcomment.Value.Trim = "" Then
                    msg = "*** Comment required!"
                    Process.loadalert(divalert, msgalert, msg, "warning")

                    amgrcomment.Focus()
                    Exit Sub
                End If

                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_Summary_Approve", lblid.Text, amgrcomment.Value)
                lblapproval.Text = "Discussed & Agreed"

                msg = "Appraisal Objective is discussed and agreed"
                Process.loadalert(divalert, msgalert, msg, "success")

                Process.Appraisal_Obj_Agreed(alinemanager.Value, aname.Value, cboDevPlan.SelectedItem.Text, txtEmpID.Text, Session("ManagerID"), Process.ApplicationURL + "/" + Process.GetMailLink(AuthenCode2, 2))
            Else
                msg = "Process cancelled"
                Process.loadalert(divalert, msgalert, msg, "info")

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub


    Protected Sub cboDevPlan_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboDevPlan.SelectedIndexChanged
        Try
            Process.LoadRadComboTextAndValueInitiateP2(cboStartReview, "Performance_Appraisal_Cycle_Get_Period", cboDevPlan.SelectedValue, Session("Organisation"), "--Select Review Period--", "Period", "id")
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub btnComplete_Click(sender As Object, e As EventArgs) Handles btnComplete.Click
        Try
            Process.loadalert(divalert, msgalert, "", "warning")
            Dim msg As String = ""
            Dim confirmValue As String = Request.Form("confirm_complete")
            If confirmValue = "Yes" Then
                If lblTotal.BackColor <> Color.Green And lblTotal.BackColor <> Color.Aqua Then
                    msg = "Total Weight on Performance Objectives not yet 100%, Complete process terminated"
                    Process.loadalert(divalert, msgalert, msg, "danger")
                    Exit Sub
                End If
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_Summary_Complete", lblid.Text)
                AppID = Request.QueryString("id")
                Dim appIDD As String = Request.QueryString("id")
                Dim link As String = "/Module/Employee/Performance/AppObjectiveUpdate?id=" + appIDD
                Process.Appraisal_Obj_Completion(alinemanager.Value, aname.Value, cboDevPlan.SelectedItem.Text, txtEmpID.Text, Session("ManagerID"), Process.ApplicationURL + link)
                msg = "Completed And Notification Sent to Line Manager"
                Process.loadalert(divalert, msgalert, msg, "success")

                'asign.Visible = True
                btnComplete.Enabled = False
                btnupdate.Visible = False
                btnrefresh.Visible = False
                btnadd.Visible = False
                btnDeleteGrid.Visible = False
                SaveButton.Visible = False
                Btngridback.Visible = False
                aempcomment.Attributes.Add("readonly", "readonly")
            End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Protected Sub cboStartReview_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboStartReview.SelectedIndexChanged
        Try
            Dim strPeriod As New DataSet
            Dim enddate As Date = Date.Now

            Dim msg As String = ""
            Dim datejoin As Date = Process.GetEmployeeData(txtEmpID.Text, "datejoin")
            strPeriod = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Appraisal_Cycle_Get", cboStartReview.SelectedValue)
            If strPeriod.Tables(0).Rows.Count > 0 Then
                enddate = strPeriod.Tables(0).Rows(0).Item("endperiod")
            End If

            If datejoin.Year > enddate.Year Then
                msg = aname.Value & " Not eligible for this review period, started work from " & Process.DDMONYYYY(datejoin) & "!"
                Process.loadalert(divalert, msgalert, msg, "danger")
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & msg + "')", True)
                btnupdate.Visible = False
                btnComplete.Visible = False

                Exit Sub
            Else
                btnupdate.Visible = True
                'btnComplete.Visible = True

            End If

            If datejoin.Month >= enddate.Month And datejoin.Year = enddate.Year Then
                msg = aname.Value & " not eligible for this review period, started work from " & Process.DDMONYYYY(datejoin) & "!"
                lblTotal.BackColor = Color.Yellow
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & msg + "')", True)
                btnupdate.Visible = False
                btnComplete.Visible = False
                Exit Sub
            Else
                btnupdate.Visible = True
                'btnComplete.Visible = True
            End If

            Dim strUser As New DataSet
            strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Work_History_Get_Actual", txtEmpID.Text, enddate)
            If strUser.Tables(0).Rows.Count > 0 Then
                txtDept.Text = strUser.Tables(0).Rows(0).Item("office").ToString
                txtLocation.Text = strUser.Tables(0).Rows(0).Item("location").ToString
                ajobgrade.Value = strUser.Tables(0).Rows(0).Item("Grade").ToString
                ajobtitle.Value = strUser.Tables(0).Rows(0).Item("JobTitle").ToString
            End If
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub lnkCoachObj_Click(sender As Object, e As EventArgs)
        Try
            Dim msg As String = ""

            If cboStartReview.SelectedValue Is Nothing Or cboStartReview.Text = "--Select Review Period--" Then
                msg = "Select Review Cycle!"
                Process.loadalert(divalert, msgalert, msg, "danger")
                cboStartReview.Focus()
                Exit Sub
            End If


            Dim cycleid As String = cboStartReview.SelectedValue
            Dim cc As String = Session("ReviewerI_ID")

            Dim cid As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "Select id from Performance_Appraisal_Summary where AppraisalCycleID = '" & cycleid & "' and empid = '" & cc & "'")

            If cid Is Nothing Then
                msg = "No Appraisal Objectives set"
                Process.loadalert(divalert, msgalert, msg, "warning")
            Else
                'Dim url As String = "CoachObjectives.aspx?id=" & cid
                'Dim s As String = "window.open('" & url + "', 'popup_window', 'width=1100,height=850,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no');"
                'ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)
                Response.Redirect("~/Module/Employee/Performance/CoachObjectives.aspx?id=" & cid, True)

            End If


        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


    Protected Sub btnDisagree_Click(sender As Object, e As EventArgs) Handles btnDisagree.Click
        Try

            Dim msg As String = ""
            If amgrcomment.Value.Trim = "" Then
                msg = "Comment required!"
                Process.loadalert(divalert, msgalert, msg, "warning")
                amgrcomment.Focus()
                Exit Sub
            End If

            Dim confirmValue As String = Request.Form("confirmplan_value")
            If confirmValue = "Yes" Then
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_Summary_DisApprove", lblid.Text, amgrcomment.Value)
                msg = "Appraisal Objective is marked as disagreed"
                Process.loadalert(divalert, msgalert, msg, "success")

                'Process.DisableButton(btnDisagree)
                lblapproval.Text = "Disagreed"
                Process.Appraisal_Obj_Disagree(alinemanager.Value, aname.Value, cboDevPlan.SelectedItem.Text, txtEmpID.Text, Session("ManagerID"), Process.ApplicationURL + "/" + Process.GetMailLink(AuthenCode2, 2))
            Else
                msg = "Process cancelled"
                Process.loadalert(divalert, msgalert, msg, "success")
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Protected Sub btnDisagree_Click1(sender As Object, e As EventArgs) Handles Update.Click
        Try           
            Dim msg As String = ""
            If Session("UserEmpID") = Session("ManagerID") Then
                If amgrcomment.Value.Trim = "" Then
                    msg = "Comment required!"
                    Process.loadalert(divalert, msgalert, msg, "warning")
                    amgrcomment.Focus()
                    Exit Sub
                End If

                Dim confirmValue As String = Request.Form("confirmplan_value")
                If confirmValue = "Yes" Then
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_Summary_DisApprove", lblid.Text, amgrcomment.Value)
                    msg = "Performance Objective is marked open"
                    Process.loadalert(divalert, msgalert, msg, "success")

                    'Process.DisableButton(btnDisagree)
                    lblapproval.Text = "Reopened"
                    Process.Appraisal_Obj_updated(alinemanager.Value, aname.Value, cboDevPlan.SelectedItem.Text, txtEmpID.Text, Session("ManagerID"), Process.ApplicationURL + "/" + Process.GetMailLink(AuthenCode2, 2))
                Else
                    msg = "Process cancelled"
                    Process.loadalert(divalert, msgalert, msg, "success")
                End If
            Else
                Dim confirmValue As String = Request.Form("confirmplan_value")
                If confirmValue = "Yes" Then
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_Summary_DisApprove_pending", lblid.Text, amgrcomment.Value)
                    msg = "Performance Objective is marked open"
                    Process.loadalert(divalert, msgalert, msg, "success")

                    'Process.DisableButton(btnDisagree)
                    lblapproval.Text = "Reopened"
                    Process.Appraisal_Obj_updated(alinemanager.Value, aname.Value, cboDevPlan.SelectedItem.Text, txtEmpID.Text, Session("ManagerID"), Process.ApplicationURL + "/" + Process.GetMailLink(AuthenCode2, 2))
                    btnrefresh.Visible = True
                    btnDeleteGrid.Visible = True
                    btnComplete.Visible = True
                    SaveButton.Visible = True
                    Btngridback.Visible = True
                    ob.Style.Add("display", "block")
                    p.Style.Add("display", "block")
                    kpigroup.Style.Add("display", "block")
                Else
                    msg = "Process cancelled"
                    Process.loadalert(divalert, msgalert, msg, "success")
                End If
            End If

            
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Public Sub btnRefresh_Click(sender As Object, e As EventArgs)
        Try
            AppID = lblid.Text
            LoadData(lblid.Text)
            btnDisagree.Visible = False
            AppID = lblid.Text
            Session("kpi") = AppID
            Process.loadalert(divalert, msgalert, "", "success")
        Catch ex As Exception

        End Try
    End Sub

    Public Sub btnRefresh_Click1(sender As Object, e As EventArgs) Handles SaveButton.Click
        Try
            ob.Style.Add("display", "block")
            p.Style.Add("display", "block")
            btnComplete.Visible = True
            btnDisagree.Visible = False
            continueBTN.Visible = False
            AppID = lblid.Text
            Process.loadalert(divalert, msgalert, "", "success")
        Catch ex As Exception

        End Try
    End Sub

    Public Sub btnDev_Click(sender As Object, e As EventArgs)
        Try
            If Session("UserEmpID") = Session("ManagerID") Then
                Dim url As String = "DirectReportDevelopmentPlan"
                Dim s As String = "window.open('" & url + "', 'popup_window', 'width=1100,height=850,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no');"
                ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)
            Else
                Dim url As String = "DevelopmentPlans"
                Dim s As String = "window.open('" & url + "', 'popup_window', 'width=1100,height=850,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no');"
                ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)
            End If
        Catch ex As Exception

        End Try
    End Sub


    Private Sub gridCompetency_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles gridCompetency.ItemDataBound
        Try
            'If TypeOf e.Item Is GridDataItem Then
            '    e.Item.Cells(2).Text = e.Item.Cells(2).Text.Replace(vbCrLf, "<br />")
            '    e.Item.Cells(3).Text = e.Item.Cells(3).Text.Replace(vbCrLf, "<br />")
            '    e.Item.Cells(4).Text = e.Item.Cells(4).Text.Replace(vbCrLf, "<br />")
            'End If
            If TypeOf e.Item Is GridDataItem Then
                Dim objectives As TableCell = TryCast(e.Item, GridDataItem)("objectives")
                objectives.Text = objectives.Text.Replace(vbCr & vbLf, "<br/>")

                Dim successtarget As TableCell = TryCast(e.Item, GridDataItem)("successtarget")
                successtarget.Text = successtarget.Text.Replace(vbCr & vbLf, "<br/>")

                Dim keyactions As TableCell = TryCast(e.Item, GridDataItem)("keyactions")
                keyactions.Text = keyactions.Text.Replace(vbCr & vbLf, "<br/>")
            End If
        Catch ex As Exception

        End Try
    End Sub

#Region "Objectives"
    Private Sub LoadObjectives(ByVal objid As Integer)
        Try
            MultiView1.ActiveViewIndex = 1
            txtobjid.Text = "0"
            'Company_Structure_get_parent

            Process.LoadRadComboTextAndValueInitiate(cboKPICategory, "Competency_Group_Get_Appraisal", "--select--", "Name", "id")
            If objid > 0 Then
                Dim strUser As New DataSet
                strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Appraisal_Get", objid)
                txtobjid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                Process.AssignRadComboValue(cboKPICategory, strUser.Tables(0).Rows(0).Item("KPIType").ToString)
                Process.LoadRadComboTextAndValueP2(cboKPI, "Competency_JobGrade_Get_Mapping", Session("UserJobgrade"), cboKPICategory.SelectedValue, "Name", "Name", False)

                Process.AssignRadComboValue(cboKPI, strUser.Tables(0).Rows(0).Item("KPIObjectives").ToString)
                a_keyactions.Value = strUser.Tables(0).Rows(0).Item("AppraisalItem").ToString
                aweight.Value = strUser.Tables(0).Rows(0).Item("customweight").ToString
                a_measure.Value = strUser.Tables(0).Rows(0).Item("comment").ToString
                a_objective.Value = strUser.Tables(0).Rows(0).Item("objectives").ToString
                If IsDBNull(strUser.Tables(0).Rows(0).Item("targetdate")) = False Then
                    datDate.SelectedDate = strUser.Tables(0).Rows(0).Item("targetdate").ToString
                End If
                lblmodel.Text = strUser.Tables(0).Rows(0).Item("weightmodel").ToString.ToLower
                If lblmodel.Text.ToLower = "independent allocation" Then
                    lblWeightView.Text = "NO"
                ElseIf lblmodel.Text.ToLower = "even distribution" Then
                    lblWeightView.Text = "YES"
                End If
                If lblWeightView.Text.ToUpper = "YES" Then
                    divweight.Visible = False
                    aweight.Value = "0"
                Else
                    divweight.Visible = True
                End If
                cboKPICategory.Enabled = False
                cboKPI.Enabled = False

                If strUser.Tables(0).Rows(0).Item("CoachApprovalStatus").ToString.ToLower.Contains("pending") = True Then
                    a_keyactions.Attributes.Remove("readonly")
                    a_measure.Attributes.Remove("readonly")
                    a_objective.Attributes.Remove("readonly")
                    aweight.Attributes.Remove("readonly")
                    datDate.Enabled = True
                ElseIf strUser.Tables(0).Rows(0).Item("CoachApprovalStatus").ToString.ToLower.Contains("disagree") = True Then
                    a_keyactions.Attributes.Remove("readonly")
                    a_measure.Attributes.Remove("readonly")
                    a_objective.Attributes.Remove("readonly")
                    aweight.Attributes.Remove("readonly")
                    datDate.Enabled = True
                Else
                    a_keyactions.Attributes.Add("readonly", "readonly")
                    a_measure.Attributes.Add("readonly", "readonly")
                    a_objective.Attributes.Add("readonly", "readonly")
                    aweight.Attributes.Add("readonly", "readonly")
                    datDate.Enabled = False
                End If
            Else
                txtobjid.Text = "0"
                Process.AssignRadComboValue(cboKPICategory, "")
                If cboKPICategory.SelectedValue IsNot Nothing And cboKPICategory.SelectedValue <> "" Then
                    Process.LoadRadComboTextAndValueP2(cboKPI, "Competency_JobGrade_Get_Mapping", Session("UserJobgrade"), cboKPICategory.SelectedValue, "Name", "Name", False)
                    LoadKPICategory_ChangeIndex()
                Else
                    Process.LoadRadComboTextAndValueP2(cboKPI, "Competency_JobGrade_Get_Mapping", Session("UserJobgrade"), 0, "Name", "Name", False)
                End If

                a_keyactions.Value = ""
                aweight.Value = "0"
                a_measure.Value = ""
                a_objective.Value = ""
                datDate.SelectedDate = DateTime.Now.Date
                lblmodel.Text = ""

                cboKPICategory.Enabled = True
                cboKPI.Enabled = True

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Function GetIdentityObj(ByVal appsummaryid As Integer, ByVal sKPIType As String, ByVal kpiobj As String, ByVal appitem As String, ByVal TargetDate As Date, ByVal weight As Double) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Performance_Appraisal_Update"
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@AppraisalSummaryID", SqlDbType.Int).Value = appsummaryid
            cmd.Parameters.Add("@kpitype", SqlDbType.VarChar).Value = sKPIType
            cmd.Parameters.Add("@kpiobjectives", SqlDbType.VarChar).Value = kpiobj
            cmd.Parameters.Add("@AppraisalItem", SqlDbType.VarChar).Value = appitem
            cmd.Parameters.Add("@targetdate", SqlDbType.Date).Value = TargetDate
            cmd.Parameters.Add("@customweight", SqlDbType.Decimal).Value = weight
            cmd.Parameters.Add("@target", SqlDbType.VarChar).Value = a_measure.Value
            cmd.Parameters.Add("@objectives", SqlDbType.VarChar).Value = a_objective.Value
            cmd.Parameters.Add("@weightmodel", SqlDbType.VarChar).Value = lblmodel.Text
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
            Return "0"
        End Try
    End Function

    Protected Sub btnAddObj_Click(sender As Object, e As EventArgs)
        Try
            If txtobjid.Text <> "0" Then
                If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False Then
                    Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "warning")
                    Exit Sub
                End If
            End If
            Dim lblstatus As String = ""

            If divobjective.Visible = True Then
                If a_objective.Value.Trim = "" Then
                    lblstatus = "Objective is required!"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    a_objective.Focus()
                    Exit Sub
                End If
            End If

            If a_measure.Value.Trim = "" Then
                lblstatus = "Success Target is required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                a_measure.Focus()
                Exit Sub
            End If

            If a_keyactions.Value.Trim = "" Then
                lblstatus = "Action statement is required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                a_keyactions.Focus()
                Exit Sub
            End If

            If datDate.SelectedDate Is Nothing Then
                lblstatus = "Target date is required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                datDate.Focus()
                Exit Sub
            End If

            If divweight.Visible = True Then
                If IsNumeric(aweight.Value) = False Then
                    lblstatus = "Weight of Objective is required!"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    aweight.Focus()
                    Exit Sub
                End If

                If CDbl(aweight.Value) < 5 Then
                    lblstatus = "Weight should not be less than 5%!"
                    aweight.Focus()
                    Exit Sub
                End If
            End If


            If txtobjid.Text = "0" Then
                txtobjid.Text = GetIdentityObj(lblid.Text, cboKPICategory.SelectedItem.Text, cboKPI.SelectedItem.Text, a_keyactions.Value, datDate.SelectedDate, aweight.Value)
                If txtobjid.Text = "0" Then
                    Exit Sub
                End If
            Else
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_Update", txtobjid.Text, lblid.Text, cboKPICategory.SelectedItem.Text, cboKPI.SelectedItem.Text, a_keyactions.Value, datDate.SelectedDate, aweight.Value, a_measure.Value, a_objective.Value, lblmodel.Text)
            End If
            LoadData(lblid.Text)


            lblstatus = "Objectives saved"
            Process.loadalert(divalert, msgalert, lblstatus, "success")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub btnCancelObj_Click(sender As Object, e As EventArgs)
        Try
            MultiView1.ActiveViewIndex = 0

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub cboKPICategory_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboKPICategory.SelectedIndexChanged
        Try
            LoadKPICategory_ChangeIndex()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub LoadKPICategory_ChangeIndex()
        Try
            Dim canSetObj As String = "yes"
            Dim strWeight As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Competency_Group_Get", cboKPICategory.SelectedValue)

            If strWeight.Tables(0).Rows.Count > 0 Then
                lblmodel.Text = strWeight.Tables(0).Rows(0).Item("weightmodel").ToString.ToLower
                canSetObj = strWeight.Tables(0).Rows(0).Item("EmpSetObjective").ToString.ToLower
                Session("btndel") = canSetObj
            End If


            If canSetObj = "yes" Then
                divkpitype.Visible = True
                cboKPI.Height = Unit.Parse("")
                Process.LoadRadComboTextAndValueP2(cboKPI, "Competency_JobGrade_Get_Mapping", Session("UserJobgrade"), cboKPICategory.SelectedValue, "Name", "Name", False)
                divobjective.Visible = True
            Else
                divkpitype.Visible = False
                divobjective.Visible = False
            End If

            If lblmodel.Text.ToLower = "independent allocation" Then
                lblWeightView.Text = "NO"
            ElseIf lblmodel.Text.ToLower = "even distribution" Then
                lblWeightView.Text = "YES"
            End If
            If lblWeightView.Text.ToUpper = "YES" Then
                divweight.Visible = False
                aweight.Value = "0"
            Else
                divweight.Visible = True
            End If
        Catch ex As Exception

        End Try
    End Sub
#End Region
    

End Class