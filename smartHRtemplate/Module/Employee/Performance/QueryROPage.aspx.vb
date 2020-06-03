Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Public Class QueryROPage
    Inherits System.Web.UI.Page
    Dim exits As New clsExit
    Dim AuthenCode As String = "TEAMQUERIES"
    Dim olddata(3) As String
    Dim lblstatus As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                cboApproval.Items.Clear()
                cboApproval.Items.Add("In-progress")
                cboApproval.Items.Add("Complete")

                'Company_Structure_get_parent
                If Request.QueryString("id") IsNot Nothing Then
                    Process.LoadRadComboTextAndValue(cboEmployee, "Emp_PersonalDetail_Get_Employees", "Employee2", "EmpID", False)
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Employee_Query_Get", Request.QueryString("id"))
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    datNotice.SelectedDate = strUser.Tables(0).Rows(0).Item("QueryDate")
                    datExpectedDate.SelectedDate = strUser.Tables(0).Rows(0).Item("ExpectedResponseDate")
                    txtQuery.Value = strUser.Tables(0).Rows(0).Item("ROQuery").ToString
                    Process.AssignRadComboValue(cboEmployee, strUser.Tables(0).Rows(0).Item("employee").ToString)

                    lblEmployeeResponse.Value = strUser.Tables(0).Rows(0).Item("EmpComment").ToString.Replace(vbCrLf, "<br />")
                    lblEmpDate.Value = strUser.Tables(0).Rows(0).Item("EmpResponseDate").ToString
                    lblEmpStatus.Value = strUser.Tables(0).Rows(0).Item("EmpStatus").ToString

                    lblEmpName.InnerText = strUser.Tables(0).Rows(0).Item("employee").ToString
                    txtComment.Value = strUser.Tables(0).Rows(0).Item("ROComment").ToString
                    Process.AssignRadComboValue(cboApproval, strUser.Tables(0).Rows(0).Item("ROStatus").ToString)
                    lblhrcomment.Value = strUser.Tables(0).Rows(0).Item("HRComment").ToString.Replace(vbCrLf, "<br />")
                    lblrecomm.Value = strUser.Tables(0).Rows(0).Item("HRAction").ToString
                    Process.LoadTimeToRadCombo(radHourStart, radMinStart, radTimeStart, strUser.Tables(0).Rows(0).Item("ExpectedResponseTime"))
                    lblinitiator.Text = strUser.Tables(0).Rows(0).Item("ReportingOfficer").ToString
                    If lblEmpStatus.Value.ToUpper = "COMPLETE" Then
                        Process.DisableButton(btnNotification)
                        Process.DisableButton(btnAdd)
                    End If
                    If Session("UserEmpID") = cboEmployee.SelectedValue Then
                        lnkResponse.Visible = True
                    End If
                Else
                    Process.LoadRadComboTextAndValueP1(cboEmployee, "Emp_PersonalDetail_Get_Employees", Session("Access"), "Employee2", "EmpID", False)
                    txtid.Text = "0"
                    datNotice.SelectedDate = Date.Now
                    Process.AssignRadComboValue(radHourStart, "9")
                    Process.AssignRadComboValue(radMinStart, "00")
                    Process.AssignRadComboValue(radTimeStart, "AM")
                    If Session("UserEmpID") = cboEmployee.SelectedValue Then
                        lnkResponse.Visible = True
                    End If
                End If

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
   
            If txtQuery.Value.Trim = "" Then
                lblstatus = "Query raised requires reason for query!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                txtQuery.Focus()
                Exit Sub
            End If

            If datExpectedDate.SelectedDate Is Nothing Then
                lblstatus = "Expected response date required!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                datExpectedDate.Focus()
                Exit Sub
            End If

            If datExpectedDate.SelectedDate < datNotice.SelectedDate Then
                lblstatus = "invalid response date!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                datExpectedDate.Focus()
                Exit Sub
            End If

            If Session("UserEmpID") = cboEmployee.SelectedValue Then
                lblstatus = "Employee cannot reaise query against self!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                cboEmployee.Focus()
                Exit Sub
            End If

            Dim OldValue As String = ""
            Dim NewValue As String = ""

            Dim j As Integer = 0

            Dim forwardemail As String = ""
            Dim forwardname As String = ""
            Dim jobtitle As String = ""
            Dim jobgrade As String = ""
            Dim office As String = ""
            Dim strEmp As New DataSet
            strEmp = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "select a.JobTitle,a.Grade,a.Office,a.Email ,a.Name     from dbo.Employees_All a where a.EmpID = '" & cboEmployee.SelectedItem.Value & "'")
            If strEmp.Tables(0).Rows.Count > 0 Then
                forwardemail = strEmp.Tables(0).Rows(0).Item("Email").ToString
                forwardname = strEmp.Tables(0).Rows(0).Item("Name").ToString
                jobtitle = strEmp.Tables(0).Rows(0).Item("JobTitle").ToString
                jobgrade = strEmp.Tables(0).Rows(0).Item("Grade").ToString
                office = strEmp.Tables(0).Rows(0).Item("Office").ToString
            End If

            If txtid.Text <> "0" And txtid.Text <> "" Then
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Employee_Query_Update_RO", txtid.Text, cboEmployee.SelectedItem.Value, jobtitle, jobgrade, office, datNotice.SelectedDate, txtQuery.Value, Session("UserEmpID"), datExpectedDate.SelectedDate, Process.ConvertCtrlToTime(radHourStart.SelectedValue, radMinStart.SelectedValue, radTimeStart.SelectedValue))
            Else
                txtid.Text = GetIdentity(txtid.Text, cboEmployee.SelectedItem.Value, jobtitle, jobgrade, office, datNotice.SelectedDate, txtQuery.Value, Session("UserEmpID"), datExpectedDate.SelectedDate, Process.ConvertCtrlToTime(radHourStart.SelectedValue, radMinStart.SelectedValue, radTimeStart.SelectedValue))
                If txtid.Text = "0" Then
                    lblstatus = Process.strExp
                    Process.loadalert(divalert, msgalert, lblstatus, "danger")
                    Exit Sub
                End If
            End If




            lblstatus = "Record updated"
            Process.loadalert(divalert, msgalert, lblstatus, "success")
            'Response.Write("<script language='javascript'> { self.close() }</script>")
        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "danger")
        End Try
    End Sub
    Private Function GetIdentity(ByVal id As Integer, ByVal empid As String, ByVal jobtitle As String, ByVal jobgrade As String, ByVal office As String, ByVal querydate As Date, ByVal strquery As String, ByVal userempid As String, ByVal expecteddate As Date, ByVal expectedtime As String) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Performance_Employee_Query_Update_RO"
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@EmpID", SqlDbType.VarChar).Value = empid
            cmd.Parameters.Add("@JobTilte", SqlDbType.VarChar).Value = jobtitle
            cmd.Parameters.Add("@JobGrade", SqlDbType.VarChar).Value = jobgrade
            cmd.Parameters.Add("@Office", SqlDbType.VarChar).Value = office
            cmd.Parameters.Add("@QueryDate", SqlDbType.Date).Value = querydate
            cmd.Parameters.Add("@Query", SqlDbType.VarChar).Value = strquery
            cmd.Parameters.Add("@UserID", SqlDbType.VarChar).Value = userempid
            cmd.Parameters.Add("@expecteddate", SqlDbType.Date).Value = expecteddate
            cmd.Parameters.Add("@expecttime", SqlDbType.VarChar).Value = expectedtime

            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception
            Process.strExp = ex.Message
            Return 0
        End Try
    End Function
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Response.Redirect("MgrQueries.aspx")

        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "danger")
        End Try
    End Sub



    'Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles lnkTerminalBenefit.Click
    '    Try
    '        Dim strUser As New DataSet
    '        strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Termination_Get", txtid.Text)
    '        Dim hrapproval As String = strUser.Tables(0).Rows(0).Item("supervisorapproval").ToString
    '        'Dim supapproval As String = ""
    '        If hrapproval.ToLower <> "approved" Then
    '            lblstatus.Text = "Termination has not be formally approved, please approve for Benefits to be generated"
    '            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & lblstatus.Text + "')", True)
    '            Exit Sub
    '        End If

    '        Dim url As String = "~/Module/Finance/Payroll/EmployeeTerminalBenefit?empid=" & cboEmployee.SelectedValue
    '        Dim s As String = "window.open('" & url + "', 'popup_window', 'width=700,height=600,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no');"
    '        ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)

    '    Catch ex As Exception
    '        lblstatus.Text = ex.Message
    '        ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & lblstatus.Text + "')", True)
    '    End Try
    'End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Try
            MultiView1.ActiveViewIndex = 0
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkResponse_Click(sender As Object, e As EventArgs) Handles lnkResponse.Click
        Try
            MultiView1.ActiveViewIndex = 1
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles btnNotification.Click
        Try
            lblstatus = ""
            Dim forwardemail As String = ""
            Dim forwardname As String = ""
            Dim jobtitle As String = ""
            Dim jobgrade As String = ""
            Dim office As String = ""

            Dim initiator As String = ""
            Dim initiator2 As String = ""

            If txtid.Text = "0" Then
                lblstatus = "Saved before notifying employee!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                Exit Sub
            End If

            Dim strEmp As New DataSet
            strEmp = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "select a.JobTitle,a.Grade,a.Office,a.Email ,a.Name     from dbo.Employees_All a where a.EmpID = '" & cboEmployee.SelectedItem.Value & "'")
            If strEmp.Tables(0).Rows.Count > 0 Then
                forwardemail = strEmp.Tables(0).Rows(0).Item("Email").ToString
                forwardname = strEmp.Tables(0).Rows(0).Item("Name").ToString
                jobtitle = strEmp.Tables(0).Rows(0).Item("JobTitle").ToString
                jobgrade = strEmp.Tables(0).Rows(0).Item("Grade").ToString
                office = strEmp.Tables(0).Rows(0).Item("Office").ToString
            End If


            strEmp = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "select a.Name, a.Employee2 Employee     from dbo.Employees_All a where a.EmpID = '" & Session("UserEmpID") & "'")
            If strEmp.Tables(0).Rows.Count > 0 Then
                initiator = strEmp.Tables(0).Rows(0).Item("Employee").ToString
                initiator2 = strEmp.Tables(0).Rows(0).Item("Name").ToString
            End If

            Process.Query_Notification(datNotice.SelectedDate, datExpectedDate.SelectedDate, Process.ConvertCtrlToTime(radHourStart.SelectedValue, radMinStart.SelectedValue, radTimeStart.SelectedValue), "", cboEmployee.SelectedValue, Session("UserEmpID"), Process.ApplicationURL + "/" + Process.GetMailLink(AuthenCode, 2))
            Process.Query_Notification_HR(datNotice.SelectedDate, datExpectedDate.SelectedDate, Process.ConvertCtrlToTime(radHourStart.SelectedValue, radMinStart.SelectedValue, radTimeStart.SelectedValue), "", cboEmployee.SelectedValue, Session("UserEmpID"), Process.ApplicationURL + "/" + Process.GetMailLink(AuthenCode, 2))
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Query notification successfully sent to " & forwardname + "')", True)
            lblstatus = "Query notification successfully sent to " & forwardname
            Process.loadalert(divalert, msgalert, lblstatus, "success")



        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "danger")
        End Try
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles btnUpdateStatus.Click
        Try
            If txtComment.Value.Trim = "" Then
                lblstatus = "Comment required for HR Recommendation!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                txtComment.Focus()
                Exit Sub
            End If

            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Employee_Query_UpdateStatus_RO", txtid.Text, txtComment.Value, cboApproval.SelectedItem.Text)
            lblstatus = "Record updated"
            Process.loadalert(divalert, msgalert, lblstatus, "success")
        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "danger")
        End Try
    End Sub

    Protected Sub btnNotifyHR_Click(sender As Object, e As EventArgs) Handles btnNotifyHR.Click
        Try
            If cboApproval.SelectedItem.Text.ToUpper <> "COMPLETE" Then
                lblstatus = "Complete Query before notifying HR Dept"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")

            Else
                Process.Query_Completion_HR(datNotice.SelectedDate, "", cboEmployee.SelectedValue, Session("UserEmpID"), Process.ApplicationURL + "/" + Process.GetMailLink("QUERIES", 3))
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Query notification successfully sent to HR Dept')", True)
                lblstatus = "Query notification successfully sent to HR Dept"
                Process.loadalert(divalert, msgalert, lblstatus, "success")
            End If
           
        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "danger")
        End Try
    End Sub

    Protected Sub Button1_Click1(sender As Object, e As EventArgs)
        Try
            Response.Redirect("MgrQueries")

        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "danger")
        End Try
    End Sub
End Class