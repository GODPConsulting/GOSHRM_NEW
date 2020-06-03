Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Collections.Generic
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Telerik.Web.UI

Public Class WorkForcePlanDetailUpdate
    Inherits System.Web.UI.Page
    Dim jobpost As New clsJobPost
    Dim olddata(22) As String
    Dim AuthenCode As String = "EMPWFPLAN"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then

                If Request.QueryString("mode") Is Nothing Then
                    divemplink.Visible = True
                    divhrlink.Visible = False
                Else
                    divemplink.Visible = False
                    divhrlink.Visible = True
                End If

                Process.LoadRadComboTextAndValueInitiate(cboJobTitle, "Job_Titles_get_all", "-- Select --", "name", "name")
                Process.LoadRadComboTextAndValueInitiate(radJobGrade, "Job_Grade_get_all", "-- Select --", "name", "name")
                Process.LoadRadComboTextAndValue(radeducation, "Education_get_all", "name", "name")

                varyear.Text = Request.QueryString("year")

                Dim strCal As New DataSet
                strCal = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "select id, calmonths from dbo.Calendar('Finance','" & varyear.Text & "')")
                If strCal.Tables(0).Rows.Count > 0 Then
                    radMonthStart.Items.Clear()
                    For z As Integer = 0 To strCal.Tables(0).Rows.Count - 1
                        Dim itemTemp As New RadComboBoxItem()
                        itemTemp.Text = strCal.Tables(0).Rows(z).Item("calmonths").ToString
                        itemTemp.Value = strCal.Tables(0).Rows(z).Item("id").ToString
                        radMonthStart.Items.Add(itemTemp)
                        itemTemp.DataBind()
                    Next
                End If

                lbyear.InnerText = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select calyear from dbo.Calendar('Finance','" & varyear.Text & "') where calmonths= '" & radMonthStart.SelectedItem.Text & "'")

                If Request.QueryString("id") IsNot Nothing Then
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_WorkForce_Budget_Detail_Get", Request.QueryString("id"))
                    lblid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    Process.AssignRadComboValue(cboJobTitle, strUser.Tables(0).Rows(0).Item("jobtitle").ToString)
                    lblprimaryid.Text = strUser.Tables(0).Rows(0).Item("primaryid").ToString
                    ajobdesc.Value = strUser.Tables(0).Rows(0).Item("jobdescription").ToString
                    Process.AssignRadComboValue(radJobGrade, strUser.Tables(0).Rows(0).Item("jobgrade").ToString)
                    Process.AssignRadComboValue(radeducation, strUser.Tables(0).Rows(0).Item("min_education").ToString)
                    aminsalary.Value = strUser.Tables(0).Rows(0).Item("salary_min").ToString

                    amaxsalary.Text = strUser.Tables(0).Rows(0).Item("salary_max").ToString
                    aexpectedstaffno.Text = strUser.Tables(0).Rows(0).Item("amountrequired").ToString
                    abudgetpayroll.Text = strUser.Tables(0).Rows(0).Item("payrollbudget").ToString
                    abudgetgratuity.Text = strUser.Tables(0).Rows(0).Item("gratuity").ToString
                    abudgetotherexp.Text = strUser.Tables(0).Rows(0).Item("otherexpense").ToString
                    abudgettraining.Text = strUser.Tables(0).Rows(0).Item("trainingbudget").ToString
                    lblentry.Text = strUser.Tables(0).Rows(0).Item("entry").ToString
                    If IsDBNull(strUser.Tables(0).Rows(0).Item("expected_recruit_start")) = False Then
                        radRecruitDate.SelectedDate = CDate(strUser.Tables(0).Rows(0).Item("expected_recruit_start").ToString)
                    End If
                    acurrentstaffno.Value = FormatNumber(SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Emp_Dept_Grade_Count", Session("Dept"), radJobGrade.SelectedValue, cboJobTitle.SelectedValue))
                    If acurrentstaffno.Value.Trim = "" Then
                        acurrentstaffno.Value = "0"
                    End If

                    acompetency.Value = strUser.Tables(0).Rows(0).Item("Skills").ToString
                    apayrolldesc.Value = strUser.Tables(0).Rows(0).Item("payrolldesc").ToString
                    atrainingdesc.Value = strUser.Tables(0).Rows(0).Item("trainingdesc").ToString
                    aotherexpdesc.Value = strUser.Tables(0).Rows(0).Item("otherexpdesc").ToString
                    agratuitydesc.Value = strUser.Tables(0).Rows(0).Item("gratuitydesc").ToString

                    If abudgetpayroll.Text = "0" Then
                        divpayrolldesc.Visible = False
                    Else
                        divpayrolldesc.Visible = True
                    End If

                    If abudgettraining.Text = "0" Then
                        divtrainingdesc.Visible = False
                    Else
                        divtrainingdesc.Visible = True
                    End If

                    If abudgetgratuity.Text = "0" Then
                        divgratuitydesc.Visible = False
                    Else
                        divgratuitydesc.Visible = True
                    End If

                    If abudgetotherexp.Text = "0" Then
                        divotherexpdesc.Visible = False
                    Else
                        divotherexpdesc.Visible = True
                    End If

                    Process.AssignRadComboValue(radMonthStart, CDate(strUser.Tables(0).Rows(0).Item("endperiod")).Month)
                    lbyear.InnerText = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select calyear from dbo.Calendar('Finance','" & varyear.Text & "') where calmonths= '" & radMonthStart.SelectedItem.Text & "'")
                    'If Session("FinalApproval").ToLower = "approved" Then
                    '    btnupdate.Disabled = True
                    'End If

                    radMonthStart.Enabled = False
                    cboJobTitle.Enabled = False
                    radJobGrade.Enabled = False
                Else
                    lblid.Text = "0"
                    lblprimaryid.Text = Request.QueryString("primaryid")

                    If IsDate(Request.QueryString("date")) = True Then
                        Process.AssignRadComboValue(radMonthStart, CDate(Request.QueryString("date")).Month)
                        lbyear.InnerText = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select calyear from dbo.Calendar('Finance','" & varyear.Text & "') where calmonths= '" & radMonthStart.SelectedItem.Text & "'")
                    End If
                End If

                'Process.workplanid = lblprimaryid.Text
                Dim companys As String = ""

                pagetitle.InnerText = "Budget " & varyear.Text & ": " & Session("Dept")
                If lblentry.Text = "budget" Then
                    btnupdate.Disabled = True
                End If


            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            Dim lblstatus As String = ""
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                lblstatus = "You don't have privilege to perform this action"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                Exit Sub
            End If

            If cboJobTitle.SelectedItem.Text.Contains("--") = True Then
                lblstatus = "Job Title required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                cboJobTitle.Focus()
                Exit Sub
            End If

            If radMonthStart.SelectedValue Is Nothing Then
                lblstatus = "Budget Period required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                radMonthStart.Focus()
                Exit Sub
            End If

            If radJobGrade.SelectedValue Is Nothing Then
                lblstatus = "Job Grade required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                radJobGrade.Focus()
                Exit Sub
            End If


            If radeducation.SelectedValue Is Nothing Then
                lblstatus = "Min Education required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                radeducation.Focus()
                Exit Sub
            End If

            If IsNumeric(aexpectedstaffno.Text) = False Then
                lblstatus = "Number of Position must be numeric only!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                aexpectedstaffno.Focus()
                Exit Sub
            End If

            'If IsNumeric(txtAgeMin.Text) = False Then
            '    lblstatus.Text = "Minimum Age must be numeric only!"
            '    txtAgeMin.Focus()
            '    Exit Sub
            'End If

            If radRecruitDate.SelectedDate Is Nothing Then
                lblstatus = "Latest Resumption Date required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                radRecruitDate.Focus()
                Exit Sub
            End If

            If IsDate(radRecruitDate.SelectedDate) = False Then
                lblstatus = "Latest Resumption Date is invalid!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                radRecruitDate.Focus()
                Exit Sub
            End If

            If radRecruitDate.SelectedDate > Process.LastDay(lbyear.InnerText, radMonthStart.SelectedValue) Then
                lblstatus = "Latest Resumption Date is invalid!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                radRecruitDate.Focus()
                Exit Sub
            End If



            'If IsNumeric(txtAgeMax.Text) = False Then
            '    lblstatus.Text = "Maximum Age must be numeric only!"
            '    txtAgeMax.Focus()
            '    Exit Sub
            'End If

            If IsNumeric(aminsalary.Value) = False Then
                lblstatus = "Minimum Salary Range must be numeric only!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                aminsalary.Focus()
                Exit Sub
            End If

            If IsNumeric(amaxsalary.Text) = False Then
                lblstatus = "Maximum Salary Range must be numeric only!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                amaxsalary.Focus()
                Exit Sub
            End If

            If CInt(aexpectedstaffno.Text) < 0 Then
                lblstatus = "Set Number of Positions required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                aexpectedstaffno.Focus()
                Exit Sub
            End If


            If IsNumeric(abudgetpayroll.Text) = False Then
                abudgetpayroll.Text = 0
            End If
            If IsNumeric(abudgetgratuity.Text) = False Then
                abudgetgratuity.Text = 0
            End If
            If IsNumeric(abudgetotherexp.Text) = False Then
                abudgetotherexp.Text = 0
            End If



            If lblid.Text = "0" Then
                lblid.Text = GetIdentity(lblprimaryid.Text, radJobGrade.SelectedValue, "", "", radeducation.SelectedValue, 0, 0, aminsalary.Value, amaxsalary.Text, radRecruitDate.SelectedDate, aexpectedstaffno.Text, abudgetpayroll.Text, acompetency.Value, ajobdesc.Value, Process.LastDay(lbyear.InnerText, radMonthStart.SelectedValue))
                If lblid.Text = "0" Then
                    Exit Sub
                End If
            Else
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_WorkForce_Budget_Detail_Update", lblid.Text, lblprimaryid.Text, radJobGrade.SelectedValue, "", "", radeducation.SelectedValue, 0, 0, aminsalary.Value, amaxsalary.Text, radRecruitDate.SelectedDate, aexpectedstaffno.Text, abudgetpayroll.Text, acompetency.Value, ajobdesc.Value, Process.LastDay(lbyear.InnerText, radMonthStart.SelectedValue), abudgetgratuity.Text, abudgetotherexp.Text, abudgettraining.Text, cboJobTitle.SelectedValue, atrainingdesc.Value, apayrolldesc.Value, agratuitydesc.Value, aotherexpdesc.Value, Session("LoginID"))
            End If
            'Process.workplanid = lblprimaryid.Text
            lblstatus = "Record saved"
            Process.loadalert(divalert, msgalert, lblstatus, "success")

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Function GetIdentity(ByVal primaryid As Integer, ByVal jobgrade As String, ByVal JobType As String, ByVal gender As String, ByVal min_education As String, _
                                ByVal age_min As Integer, ByVal age_max As Integer, ByVal salary_min As Double, ByVal salary_max As Double, ByVal expected_recruit_start As Date, _
                                ByVal amountrequired As Integer, ByVal budget As Double, ByVal Skills As String, ByVal JobDescription As String, ByVal budgetperiod As Date) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Recruit_WorkForce_Budget_Detail_Update"
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@primaryid", SqlDbType.Int).Value = primaryid
            cmd.Parameters.Add("@jobgrade", SqlDbType.VarChar).Value = jobgrade
            cmd.Parameters.Add("@jobtype", SqlDbType.VarChar).Value = JobType
            cmd.Parameters.Add("@gender", SqlDbType.VarChar).Value = gender
            cmd.Parameters.Add("@min_education", SqlDbType.VarChar).Value = min_education
            cmd.Parameters.Add("@age_min", SqlDbType.Int).Value = age_min
            cmd.Parameters.Add("@age_max", SqlDbType.Int).Value = age_max
            cmd.Parameters.Add("@salary_min", SqlDbType.Decimal).Value = salary_min
            cmd.Parameters.Add("@salary_max", SqlDbType.Decimal).Value = salary_max
            cmd.Parameters.Add("@expected_recruit_start", SqlDbType.Date).Value = expected_recruit_start
            cmd.Parameters.Add("@amountrequired", SqlDbType.Int).Value = amountrequired
            cmd.Parameters.Add("@budget", SqlDbType.Decimal).Value = budget
            cmd.Parameters.Add("@skills", SqlDbType.VarChar).Value = Skills
            cmd.Parameters.Add("@jobdescription", SqlDbType.VarChar).Value = JobDescription
            cmd.Parameters.Add("@date", SqlDbType.Date).Value = Process.DDMONYYYY(budgetperiod)
            cmd.Parameters.Add("@grauity", SqlDbType.Decimal).Value = abudgetgratuity.Text
            cmd.Parameters.Add("@others", SqlDbType.Decimal).Value = abudgetotherexp.Text
            cmd.Parameters.Add("@training", SqlDbType.Decimal).Value = abudgettraining.Text
            cmd.Parameters.Add("@jobtitle", SqlDbType.VarChar).Value = cboJobTitle.SelectedValue
            cmd.Parameters.Add("@trainingdesc", SqlDbType.VarChar).Value = atrainingdesc.Value
            cmd.Parameters.Add("@payrolldesc", SqlDbType.VarChar).Value = apayrolldesc.Value
            cmd.Parameters.Add("@gratuitydesc", SqlDbType.VarChar).Value = agratuitydesc.Value
            cmd.Parameters.Add("@otherexpdesc", SqlDbType.VarChar).Value = aotherexpdesc.Value
            cmd.Parameters.Add("@login", SqlDbType.VarChar).Value = Session("LoginID")
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception
            Process.strExp = ex.Message
            Return 0
        End Try
    End Function
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            'Response.Write("<script language='javascript'> { self.close() }</script>")
            Response.Redirect("WorkForcePlanUpdate?id=" & lblprimaryid.Text, True)
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub btnCancel1_Click(sender As Object, e As EventArgs)
        Try
            'Response.Write("<script language='javascript'> { self.close() }</script>")
            Response.Redirect("~/Module/Recruitment/WorkForceBudgetUpdate?id=" & lblprimaryid.Text, True)
        Catch ex As Exception

        End Try
    End Sub



    Protected Sub radJobgrade_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles radJobGrade.SelectedIndexChanged
        Try
            Dim lblstatus As String = ""
            If radJobGrade.SelectedValue IsNot Nothing Then
                acurrentstaffno.Value = FormatNumber(SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Emp_Dept_Grade_Count", Session("Dept"), radJobGrade.SelectedValue, cboJobTitle.SelectedValue), 0)
                If acurrentstaffno.Value.Trim = "" Then
                    acurrentstaffno.Value = "0"
                End If
                amaxsalary.Text = FormatNumber(CDbl(SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Emp_Grade_Pay_Sum", radJobGrade.SelectedValue)), 2)
                If IsNumeric(aexpectedstaffno.text) = False Then
                    aexpectedstaffno.text = 0
                End If
                If IsNumeric(amaxsalary.Text) = False Then
                    amaxsalary.Text = 0
                End If
                abudgetpayroll.Text = FormatNumber((CInt(aexpectedstaffno.text)) * CDbl(amaxsalary.Text), 2)
                abudgetotherexp.Text = FormatNumber((CInt(aexpectedstaffno.text)) * CDbl(SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Emp_Grade_Non_Pay_Sum", radJobGrade.SelectedValue)), 2)
                abudgetgratuity.Text = FormatNumber((CInt(aexpectedstaffno.Text)) * CDbl(SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Emp_Grade_Gratuity_Sum", lbyear.InnerText, radMonthStart.SelectedValue, radJobGrade.SelectedValue, Session("Dept"))), 2)

                Dim strDesc As New DataSet
                strDesc = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Courses_WorkForce_Cost", Session("Dept"), cboJobTitle.SelectedValue, radJobGrade.SelectedValue, Process.FirstDay(lbyear.InnerText, radMonthStart.SelectedValue), Process.LastDay(lbyear.InnerText, radMonthStart.SelectedValue))
                If IsDBNull(strDesc.Tables(0).Rows(0).Item("TotalCost").ToString) = False Then
                    If IsNumeric(strDesc.Tables(0).Rows(0).Item("TotalCost").ToString) = True Then
                        abudgettraining.Text = FormatNumber(CDbl(strDesc.Tables(0).Rows(0).Item("TotalCost").ToString), 2)
                    End If
                End If

            Else
                acurrentstaffno.Value = "0"
            End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub



    Protected Sub txtPositions_TextChanged(sender As Object, e As EventArgs) Handles aexpectedstaffno.TextChanged
        Try
            Dim lblstatus As String = ""
            If acurrentstaffno.Value.Trim = "" Then
                acurrentstaffno.Value = "0"
            End If
            If IsNumeric(aexpectedstaffno.text) = False Then
                aexpectedstaffno.text = 0
            End If
            If IsNumeric(amaxsalary.Text) = False Then
                amaxsalary.Text = 0
            End If
            abudgetpayroll.Text = FormatNumber((CInt(aexpectedstaffno.text)) * CDbl(amaxsalary.Text), 2)
            abudgetotherexp.Text = FormatNumber((CInt(aexpectedstaffno.text)) * CDbl(SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Emp_Grade_Non_Pay_Sum", radJobgrade.SelectedValue)), 2)
            abudgetgratuity.Text = FormatNumber((CInt(aexpectedstaffno.text)) * CDbl(SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Emp_Grade_Gratuity_Sum", lbyear.InnerText, radMonthStart.SelectedValue, radJobgrade.SelectedValue, Session("Dept"))), 2)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


    Protected Sub radMonthStart_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles radMonthStart.SelectedIndexChanged
        Try

            lbyear.InnerText = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select calyear from dbo.Calendar('Finance','" & varyear.Text & "') where calmonths= '" & radMonthStart.SelectedItem.Text & "'")
            Dim strDesc As New DataSet
            strDesc = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Courses_WorkForce_Cost", Session("Dept"), cboJobTitle.SelectedValue, radJobGrade.SelectedValue, Process.FirstDay(lbyear.InnerText, radMonthStart.SelectedValue), Process.LastDay(lbyear.InnerText, radMonthStart.SelectedValue))
            If IsDBNull(strDesc.Tables(0).Rows(0).Item("TotalCost").ToString) = False Then
                If IsNumeric(strDesc.Tables(0).Rows(0).Item("TotalCost").ToString) = True Then
                    abudgettraining.Text = FormatNumber(CDbl(strDesc.Tables(0).Rows(0).Item("TotalCost").ToString), 2)
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub cboJobTitle_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboJobTitle.SelectedIndexChanged
        Try
            acurrentstaffno.Value = FormatNumber(SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Emp_Dept_Grade_Count", Session("Dept"), radJobGrade.SelectedValue, cboJobTitle.SelectedValue), 0)
            If acurrentstaffno.Value.Trim = "" Then
                acurrentstaffno.Value = "0"
            End If

            Dim strDesc As New DataSet
            strDesc = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Job_Titles_get", cboJobTitle.SelectedValue)
            ajobdesc.Value = strDesc.Tables(0).Rows(0).Item("jobdescription").ToString
            Process.LoadTextAreaP1(acompetency, "Job_Title_Skills_Get_All_2", cboJobTitle.SelectedValue, "skills")

            Dim strr As New DataSet
            strr = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Courses_WorkForce_Cost", Session("Dept"), cboJobTitle.SelectedValue, radJobgrade.SelectedValue, Process.FirstDay(lbyear.InnerText, radMonthStart.SelectedValue), Process.LastDay(lbyear.InnerText, radMonthStart.SelectedValue))
            If strr.Tables(0).Rows.Count > 0 Then
                If IsDBNull(strr.Tables(0).Rows(0).Item("TotalCost").ToString) = False Then
                    If IsNumeric(strr.Tables(0).Rows(0).Item("TotalCost").ToString) = True Then
                        abudgettraining.Text = FormatNumber(CDbl(strr.Tables(0).Rows(0).Item("TotalCost").ToString), 2)
                    End If
                End If
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub txtSalaryMax_TextChanged(sender As Object, e As EventArgs) Handles amaxsalary.TextChanged
        Try
            abudgetpayroll.Text = FormatNumber((CInt(aexpectedstaffno.Text)) * CDbl(amaxsalary.Text), 2)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub txtBudgetAmount_TextChanged(sender As Object, e As EventArgs) Handles abudgetpayroll.TextChanged
        Try
            If abudgetpayroll.Text = "0" Then
                divpayrolldesc.Visible = False

            Else
                divpayrolldesc.Visible = True
            End If


        Catch ex As Exception

        End Try
    End Sub

    Protected Sub txtGratuityBudget_TextChanged(sender As Object, e As EventArgs) Handles abudgetgratuity.TextChanged
        Try
            If abudgetgratuity.Text = "0" Then
                divgratuitydesc.Visible = False
            Else
                divgratuitydesc.Visible = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub txtOtherExpBudget_TextChanged(sender As Object, e As EventArgs) Handles abudgetotherexp.TextChanged
        Try
            If abudgetotherexp.Text = "0" Then
                divotherexpdesc.Visible = False
            Else
                divotherexpdesc.Visible = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub txtTrainingBudget_TextChanged(sender As Object, e As EventArgs) Handles abudgettraining.TextChanged
        Try
            If abudgettraining.Text = "0" Then
                divtrainingdesc.Visible = False
            Else
                divtrainingdesc.Visible = True
            End If
        Catch ex As Exception

        End Try
    End Sub

End Class