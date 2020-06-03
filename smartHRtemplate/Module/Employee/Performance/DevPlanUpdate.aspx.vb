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


Public Class DevPlanUpdate
    Inherits System.Web.UI.Page
    Dim education As New clsEducation
    Dim Separators() As Char = {","c}
    Dim AuthenCode As String = "DEVPLAN"
    Dim AuthenCode2 As String = "APPDEV"
    Dim rowCounts As Integer = 0
    
 

    Private Sub LoadData(dataid As Integer)
        Try
            GridVwHeaderChckbox.DataSource = Process.SearchData("Performance_Development_Plan_Detail_Get_All", dataid)
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Private Function GetIdentity(ByVal empid As String, ByVal jobtitle As String, ByVal gradelevel As String, ByVal dept As String, _
                                ByVal loaction As String, ByVal planyear As Integer, _
                                 ByVal coach As String, ByVal coachgrade As String, ByVal empcomment As String, ByVal reviewdate As Date) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Performance_Development_Plan_Update"
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@EmpID", SqlDbType.VarChar).Value = empid
            cmd.Parameters.Add("@JobTitle", SqlDbType.VarChar).Value = jobtitle
            cmd.Parameters.Add("@JobGrade", SqlDbType.VarChar).Value = gradelevel
            cmd.Parameters.Add("@Dept", SqlDbType.VarChar).Value = dept
            cmd.Parameters.Add("@Location", SqlDbType.VarChar).Value = loaction
            cmd.Parameters.Add("@PlanYear", SqlDbType.Int).Value = planyear
            cmd.Parameters.Add("@Coach", SqlDbType.VarChar).Value = coach
            cmd.Parameters.Add("@CoachGrade", SqlDbType.VarChar).Value = coachgrade
            cmd.Parameters.Add("@EmpComment", SqlDbType.VarChar).Value = empcomment
            cmd.Parameters.Add("@reviewdate", SqlDbType.Date).Value = reviewdate
            cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = Session("UserEmpID")

            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
            Return "0"
        End Try
    End Function


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Session("PreviousPage") = ""
            If Session("devplan") = "True" Then
                plan.Style.Add("display", "block")
                dev.Style.Add("display", "none")
            End If
            Session("devplan") = ""
            If Not Me.IsPostBack Then
                Process.LoadRadComboTextAndValueInitiate(cboDevPlan, "Finance_Calendar_get_all", "--Select Year---", "name", "name")
                Process.LoadRadDropDownTextAndValue(radJobTitle, "Job_Titles_get_all", "Name", "Name", False)
                Process.LoadRadDropDownTextAndValue(radJobGrade, "Job_Grade_get_all", "Name", "Name", False)
                If Request.QueryString("id") IsNot Nothing Then                   
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Development_Plan_Get", Request.QueryString("id"))
                    lblid.Text = strUser.Tables(0).Rows(0).Item("id").ToString

                    Process.AssignRadComboValue(cboDevPlan, strUser.Tables(0).Rows(0).Item("planyear").ToString)
                    cboDevPlan.Enabled = False
                    txtempid.Text = strUser.Tables(0).Rows(0).Item("empid").ToString
                    aname.Value = strUser.Tables(0).Rows(0).Item("name").ToString
                    Process.AssignRadDropDownValue(radJobTitle, strUser.Tables(0).Rows(0).Item("jobtitle").ToString)
                    Process.AssignRadDropDownValue(radJobGrade, strUser.Tables(0).Rows(0).Item("jobgrade").ToString)
                    txtDept.Text = strUser.Tables(0).Rows(0).Item("dept").ToString

                    txtMthsInLastPos.Text = strUser.Tables(0).Rows(0).Item("monthsinposition").ToString
                    lblempemail.Text = strUser.Tables(0).Rows(0).Item("EmpEmail").ToString

                    If IsDBNull(strUser.Tables(0).Rows(0).Item("lastreviewdate")) = False Then
                        datLastReview.SelectedDate = CDate(strUser.Tables(0).Rows(0).Item("lastreviewdate"))
                    End If

                    If IsDBNull(strUser.Tables(0).Rows(0).Item("LastReviewRating")) = False Then
                        alastrating.Value = strUser.Tables(0).Rows(0).Item("LastReviewRating").ToString
                    End If

                    If IsDBNull(strUser.Tables(0).Rows(0).Item("reviewdate")) = False Then
                        datReviewDate.SelectedDate = CDate(strUser.Tables(0).Rows(0).Item("reviewdate"))
                    End If

                    Process.LoadRadComboTextAndValueP2(cbocoach, "Emp_PersonalDetail_get_Superiors", radJobGrade.SelectedValue, Session("Access"), "name", "EmpID", True)

                    Process.AssignRadComboValue2(cbocoach, strUser.Tables(0).Rows(0).Item("coachname").ToString, strUser.Tables(0).Rows(0).Item("Coach").ToString)
                    txtCoachGrade.Text = strUser.Tables(0).Rows(0).Item("CoachGrade").ToString
                    txtCoachMail.Text = strUser.Tables(0).Rows(0).Item("Coachemail").ToString
                    acoachcomment.Value = strUser.Tables(0).Rows(0).Item("CoachComment").ToString
                    aempcomment.Value = strUser.Tables(0).Rows(0).Item("EmpComment").ToString

                    datReviewDate.SelectedDate = CDate(strUser.Tables(0).Rows(0).Item("reviewdate"))
                    txtlocation.Text = strUser.Tables(0).Rows(0).Item("Location")
                    If IsDBNull(strUser.Tables(0).Rows(0).Item("approvaldate")) = True Then
                        asign.Visible = False
                    Else
                        asign.Visible = True
                        asign.InnerText = "Discussed & Agreed By " & strUser.Tables(0).Rows(0).Item("coachname") & " and " & strUser.Tables(0).Rows(0).Item("name") & " on " & strUser.Tables(0).Rows(0).Item("approvaldate")
                    End If

                    'If you employee has completed Dev Plan
                    If strUser.Tables(0).Rows(0).Item("Completed").ToString.ToLower.Contains("yes") = True Then
                        cbocoach.Enabled = True
                        btaddgrid.Visible = False
                        btdeletegrid.Visible = False
                        btComplete.Visible = False
                        btComplete.Enabled = False
                        btsave.Visible = False
                        Button1.Visible = False
                        plan.Style.Add("display", "block")
                        dev.Style.Add("display", "block")
                    End If

                    Session("UserJobgrade") = radJobGrade.SelectedText
                    'Session("ID") = lblid.Text
                    acreated.InnerText = "Created by " & strUser.Tables(0).Rows(0).Item("createdby").ToString & " on " & strUser.Tables(0).Rows(0).Item("CreatedOn").ToString
                    LoadData(lblid.Text)

                    If GridVwHeaderChckbox.Rows.Count > 0 Then
                        btComplete.Visible = True
                    Else
                        btComplete.Visible = False
                    End If

                    If strUser.Tables(0).Rows(0).Item("approvalstatus").ToString.ToUpper.Contains("AGREED") Then
                        btnagree.Visible = False
                    End If

                    lbapproval.InnerText = strUser.Tables(0).Rows(0).Item("approvalstatus").ToString

                    If aname.Value = Session("EmpName") Then
                        btnagree.Visible = False
                        'btsave.Visible = False
                        'aempcomment.Attributes.Add("readonly", "")
                        'aempcomment.Style.Add("readonly", "")
                        aempcomment.Disabled = False
                        acoachcomment.Disabled = True
                        btComplete.Visible = True
                        'btcoachsave.Visible = True
                    Else
                        'acoachcomment.Attributes.Add("readonly", "readonly")
                        'acoachcomment.Style.Add("readonly", "readonly")
                        btnagree.Visible = True
                        aempcomment.Disabled = True
                        acoachcomment.Disabled = False
                    End If
                Else
                    Process.AssignRadComboValue(cboDevPlan, Now.Year.ToString)
                    txtempid.Text = Session("UserEmpID")
                    'btcoachsave.Visible = False
                    lblid.Text = "0"
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "select a.JobTitle,a.Grade,a.Name,a.office,a.location, c.Employee2, a.CoachID, c.Email coachmail,c.Grade coachgrade, a.email empmail from dbo.Employees_All a left outer join dbo.Employees_All c on a.CoachID = c.EmpID where a.EmpID = '" & txtempid.Text & "'")

                    Process.AssignRadDropDownValue(radJobTitle, strUser.Tables(0).Rows(0).Item("jobtitle").ToString)
                    Process.AssignRadDropDownValue(radJobGrade, strUser.Tables(0).Rows(0).Item("Grade").ToString)
                    txtDept.Text = strUser.Tables(0).Rows(0).Item("office").ToString
                    txtlocation.Text = strUser.Tables(0).Rows(0).Item("location").ToString

                    aname.Value = strUser.Tables(0).Rows(0).Item("name").ToString
                    lblempemail.Text = strUser.Tables(0).Rows(0).Item("empmail").ToString

                    Process.LoadRadComboTextAndValueP2(cbocoach, "Emp_PersonalDetail_get_Superiors", radJobGrade.SelectedValue, Session("Access"), "name", "EmpID", True)
                    Process.AssignRadComboValue2(cbocoach, strUser.Tables(0).Rows(0).Item("Employee2").ToString, strUser.Tables(0).Rows(0).Item("CoachID").ToString)

                    txtCoachGrade.Text = strUser.Tables(0).Rows(0).Item("coachgrade").ToString
                    txtCoachMail.Text = strUser.Tables(0).Rows(0).Item("coachmail").ToString
                    LoadData(0)
                    btaddgrid.Visible = False
                    btdeletegrid.Visible = False
                    btComplete.Visible = False

                    datReviewDate.SelectedDate = Now.Date
                    Session("UserJobgrade") = radJobGrade.SelectedText
                    'btnAgreed.Visible = False
                    btnagree.Visible = False


                    If aname.Value = Session("EmpName") Then
                        btnagree.Visible = False
                        aempcomment.Disabled = False
                        acoachcomment.Disabled = True
                        btComplete.Visible = True
                    Else
                        acoachcomment.Disabled = False
                    End If
                End If
            End If

            If alastrating.Value.Trim = "" Then
                divlastreview.Visible = False
            Else
                divlastreview.Visible = True
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            Dim lblstatus As String = ""

            If cbocoach.SelectedValue = Session("userempid") Then
                If acoachcomment.Value.Trim = "" Then
                    lblstatus = "Coach comment required!"
                    acoachcomment.Focus()
                    Exit Sub
                End If

                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Development_Plan_SavedComment", lblid.Text, acoachcomment.Value)
                lblstatus = "Comment saved"
                Session("ReviewDate") = datReviewDate.SelectedDate
            Else
                If lblid.Text <> "0" Then
                    If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False And Process.AuthenAction(Session("role"), AuthenCode2, "Update") = False Then
                        Process.loadalert(divalert, msgalert, Process.privilegemsg, "warning")
                        Exit Sub
                    End If
                End If


                If lbapproval.InnerText.ToLower.Contains("agreed") Then
                    Process.loadalert(divalert, msgalert, "Development plan is already agreed and discussed, update cancelled!", "warning")
                    Exit Sub
                End If


                If IsNumeric(cboDevPlan.SelectedValue) = False Then
                    lblstatus = "Select a Development Plan Year"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    cboDevPlan.Focus()
                    Exit Sub
                End If

                'Work History
                If lblid.Text = "0" Then
                    lblid.Text = GetIdentity(txtempid.Text, radJobTitle.SelectedValue, radJobGrade.SelectedValue, txtDept.Text, txtlocation.Text, cboDevPlan.SelectedValue, cbocoach.SelectedValue, txtCoachGrade.Text, aempcomment.Value, datReviewDate.SelectedDate)
                    If lblid.Text = "0" Then
                        Exit Sub
                    End If
                Else
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Development_Plan_Update", lblid.Text, txtempid.Text, radJobTitle.SelectedValue, radJobGrade.SelectedValue, txtDept.Text, txtlocation.Text, cboDevPlan.SelectedValue, cbocoach.SelectedValue, txtCoachGrade.Text, aempcomment.Value, datReviewDate.SelectedDate, "")
                End If

                Session("ReviewDate") = datReviewDate.SelectedDate

                If lblid.Text <> "0" And IsNumeric(lblid.Text) = True Then
                    'Process.EnableButton(btnAddGrid)
                    btaddgrid.Visible = True
                    btdeletegrid.Visible = True
                Else
                    lblid.Text = "0"                    
                End If
                'lblstatus = "Development Plan Saved"
            End If

            plan.Style.Add("display", "block")
            dev.Style.Add("display", "none")
            Process.loadalert(divalert, msgalert, lblstatus, "success")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub radJobGrade_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.DropDownListEventArgs) Handles radJobGrade.SelectedIndexChanged
        Try
            Process.LoadRadComboTextAndValueP2(cbocoach, "Emp_PersonalDetail_get_Superiors", radJobGrade.SelectedText, Session("Access"), "name", "EmpID", True)
            Session("UserJobgrade") = radJobGrade.SelectedValue
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub



    Protected Sub btnClose_Click(sender As Object, e As EventArgs)
        'Response.Write("<script language='javascript'> { self.close() }</script>")
        Try
            Response.Redirect("DevelopmentPlans", True)
        Catch ex As Exception

        End Try
    End Sub



    Protected Sub radCoach_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cbocoach.SelectedIndexChanged
        Try
            Dim strUser As New DataSet
            strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "select a.JobTitle,a.Grade,a.Name, a.Email from dbo.Employees_All a where a.EmpID = '" & cbocoach.SelectedValue & "'")
            txtCoachGrade.Text = strUser.Tables(0).Rows(0).Item("Grade").ToString
            txtCoachMail.Text = strUser.Tables(0).Rows(0).Item("Email").ToString

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnAddGrid_Click(sender As Object, e As EventArgs)
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                Process.loadalert(divalert, msgalert, Process.privilegemsg, "warning")

                Exit Sub
            End If


            If lblid.Text <> "0" Then
                Response.Redirect("MyDevObjectives?planid=" & lblid.Text & "&empid=" & txtempid.Text, True)
            Else

                Process.loadalert(divalert, msgalert, "Save Data before adding kpi objectives!", "warning")
                Exit Sub
            End If

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub OpenDetail(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim id As String = CType(sender, LinkButton).CommandArgument
            'Dim url As String = "MyDevObjectives?id=" & id
            'Dim s As String = "window.open('" & url + "', 'popup_window', 'width=750,height=600,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no');"
            'ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)

            Response.Redirect("MyDevObjectives?planid=" & lblid.Text & "&empid=" & txtempid.Text & "&id=" & id, True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnDeleteGrid_Click(sender As Object, e As EventArgs) Handles btdeletegrid.Click
        Try
            Dim lblstatus As String = ""
            Process.loadalert(divalert, msgalert, lblstatus, "danger")
            If Process.AuthenAction(Session("role"), AuthenCode, "Delete") = False Then
                lblstatus = "You don't have privilege to perform this action"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                Exit Sub
            End If
            Dim counts As Integer = 0
            Dim confirmValue As String = Request.Form("confirm_value")
            If confirmValue = "Yes" Then
                Dim atLeastOneRowDeleted As Boolean = False
                ' Iterate through the Products.Rows property
                For Each row As GridViewRow In GridVwHeaderChckbox.Rows
                    ' Access the CheckBox
                    Dim cb As CheckBox = row.FindControl("chkEmp")
                    If cb IsNot Nothing AndAlso cb.Checked Then
                        counts = counts + 1
                        ' Delete row! (Well, not really...)
                        atLeastOneRowDeleted = True
                        ' First, get the ProductID for the selected row
                        Dim ID As String = _
                            Convert.ToString(GridVwHeaderChckbox.DataKeys(row.RowIndex).Value)
                        ' "Delete" the row
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Development_Plan_Detail_Delete", ID)
                    End If
                Next
                Process.loadalert(divalert, msgalert, counts.ToString & " records successfully deleted", "success")

                LoadData(lblid.Text)

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnAgreed_Click(sender As Object, e As EventArgs)
        Try
            Dim lblstatus As String = ""
            Dim confirmValue As String = Request.Form("confirmplan_value")
            If confirmValue = "Yes" Then

                'Process.DisableButton(btnAgreed)
                btnagree.Visible = True
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Development_Plan_Approve", lblid.Text, acoachcomment.Value)
                lblstatus = aname.Value & " Development Plan is discussed and agreed"
                Process.loadalert(divalert, msgalert, lblstatus, "success")
                Process.Development_Plan_Agreed(txtempid.Text, cbocoach.SelectedItem.Text, cboDevPlan.SelectedItem.Text, aname.Value, txtempid.Text, cbocoach.SelectedValue, Process.GetMailLink(AuthenCode, 2))
                Process.Development_Plan_Agreed_To_HR(txtempid.Text, cbocoach.SelectedItem.Text, cboDevPlan.SelectedItem.Text, aname.Value, "", txtempid.Text, cbocoach.SelectedValue, Process.GetMailLink(AuthenCode, 1))

                lbapproval.InnerText = "Discussed & Agreed"

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub



    Protected Sub GridVwHeaderChckbox_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridVwHeaderChckbox.PageIndexChanging
        Try
            GridVwHeaderChckbox.PageIndex = e.NewPageIndex
            GridVwHeaderChckbox.DataSource = Process.SearchData("Performance_Development_Plan_Detail_Get_All", lblid.Text)
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnComplete_Click(sender As Object, e As EventArgs) Handles btComplete.Click
        Try
            Dim lblstatus As String = ""
            Dim confirmValue As String = Request.Form("confirm_complete")
            If confirmValue = "Yes" Then
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Development_Plan_Update_Complete", lblid.Text)
                Process.Development_Plan_Completion(txtempid.Text, aname.Value, cboDevPlan.SelectedItem.Text, Session("UserEmpID"), cbocoach.SelectedValue, Process.ApplicationURL + "/" + Process.GetMailLink(AuthenCode, 2))
                lblstatus = "Completed and Notification Sent to Coach"
                Process.loadalert(divalert, msgalert, lblstatus, "success")
                btaddgrid.Visible = False
                btdeletegrid.Visible = False
                btComplete.Visible = False
                btsave.Visible = False
                Button1.Visible = False
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnComplete(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Dim lblstatus As String = ""
            Dim confirmValue As String = Request.Form("confirm_complete")
            If confirmValue = "Yes" Then
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Development_Plan_Update_Complete", lblid.Text)
                Process.Development_Plan_Completion(txtempid.Text, aname.Value, cboDevPlan.SelectedItem.Text, Session("UserEmpID"), cbocoach.SelectedValue, Process.ApplicationURL + "/" + Process.GetMailLink(AuthenCode, 2))
                lblstatus = "Completed and Notification Sent to Coach"
                Process.loadalert(divalert, msgalert, lblstatus, "success")
                btaddgrid.Visible = False
                btdeletegrid.Visible = False
                btComplete.Visible = False
                btsave.Visible = False
                plan.Style.Add("display", "block")
                dev.Style.Add("display", "block")
                Button1.Visible = False
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub cboDevPlan_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboDevPlan.SelectedIndexChanged
        Try

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnCoachSave_Click(sender As Object, e As EventArgs)
        Try
            Dim lblstatus As String = ""
            If acoachcomment.Value.Trim = "" Then
                lblstatus = "Coach comment required!"
                acoachcomment.Focus()
                Exit Sub
            End If
            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Development_Plan_SavedComment", lblid.Text, acoachcomment.Value)
            lblstatus = "Comment saved"
            Process.loadalert(divalert, msgalert, lblstatus, "success")

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub
End Class