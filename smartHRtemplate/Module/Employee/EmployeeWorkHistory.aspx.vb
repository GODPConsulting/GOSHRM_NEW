Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Public Class EmployeeWorkHistory
    Inherits System.Web.UI.Page
   

    Dim EmpWork As New clsEmpWorkHistory
    Dim olddata(16) As String
    Dim SupervisorArray(2) As String
    Dim CoachArray(2) As String
    Dim Separators() As Char = {":"c}
    Dim AuthenCode As String = "EMPLIST"
    Private Shared country As String


    Private Sub LoadDynamic()
        Try

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                Process.LoadRadComboTextAndValueInitiate(cboJobGrade, "Job_Grade_get_all", "--select--", "Name", "Name")
                Process.LoadRadComboTextAndValueInitiate(cboJobTitle, "Job_Titles_get_all", "--select--", "Name", "Name")

                Process.LoadRadComboTextAndValue(cbojobtype, "employment_status_get_all", "Name", "Name", False)
                Process.LoadRadComboTextAndValueInitiate(cboCountry, "CountryTable_get", "--select--", "Country", "Country")
                Process.LoadRadComboTextAndValueP2(cbostartmonth, "Data_Period_get_all", "N/A", "Present", "Period", "Period", False)
                Process.LoadRadComboTextAndValueP2(cboendmonth, "Data_Period_get_all", "N/A", "", "Period", "Period", False)


                cbostartyear.Items.Clear()
                cboendyear.Items.Clear()
                For z As Integer = 1980 To 2050
                    cbostartyear.Items.Add(z.ToString)
                    cboendyear.Items.Add(z.ToString)
                Next
                Session("PreviousCareerPage") = Request.UrlReferrer.ToString
         

                If Request.QueryString("Id1") IsNot Nothing Then
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Work_History_get", Request.QueryString("id1"))


                    If strUser.Tables(0).Rows.Count > 0 Then
                        txtempid.Text = strUser.Tables(0).Rows(0).Item("EmpID").ToString
                        aname.Value = Process.GetEmployeeName(txtempid.Text)
                        Process.AssignRadComboValue(cboJobGrade, strUser.Tables(0).Rows(0).Item("GradeLevel").ToString)
                        Process.AssignRadComboValue(cboJobTitle, strUser.Tables(0).Rows(0).Item("JobTitle").ToString)
                        Process.AssignRadComboValue(cbojobtype, strUser.Tables(0).Rows(0).Item("JobType").ToString)
                        Process.AssignRadComboValue(cboCountry, strUser.Tables(0).Rows(0).Item("Country").ToString)

                        Process.LoadRadComboTextAndValueInitiateP1(cboLocation, "location_get_country", cboCountry.SelectedValue, "--select--", "name", "name")
                        Process.AssignRadComboValue(cboLocation, strUser.Tables(0).Rows(0).Item("Location").ToString)

                        Process.AssignRadComboValue(cbostartmonth, strUser.Tables(0).Rows(0).Item("StartDate").ToString)
                        Process.AssignRadComboValue(cbostartyear, strUser.Tables(0).Rows(0).Item("StartYear").ToString)
                        Process.AssignRadComboValue(cboendmonth, strUser.Tables(0).Rows(0).Item("EndDate").ToString)
                        Process.AssignRadComboValue(cboendyear, strUser.Tables(0).Rows(0).Item("EndYear").ToString)

                        txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    End If
                   

                    If Request.QueryString("recommendation") IsNot Nothing Then
                        pagetitle.InnerText = "Employee Promotion"
                        txtid.Text = "0"
                    Else
                        pagetitle.InnerText = "Employee Work History"
                    End If

                    txtEmpID.Enabled = False
                    '

                    Process.LoadRadComboTextAndValueP2(cboSupervisor, "Emp_PersonalDetail_get_Superiors", cboJobGrade.SelectedValue, Process.GetCompanyName, "name", "EmpiD", True)
                    Process.AssignRadComboValue(cboSupervisor, strUser.Tables(0).Rows(0).Item("Supervisorid").ToString)

                    Process.LoadRadComboTextAndValueP2(cboReviewerII, "Emp_PersonalDetail_get_Superiors", cboJobGrade.SelectedValue, Process.GetCompanyName, "name", "EmpiD", True)
                    Process.AssignRadComboValue(cboReviewerII, strUser.Tables(0).Rows(0).Item("Supervisor2id").ToString)


                    'Process.LoadRadComboTextAndValueP1(cboOffice, "Company_Structure_Get_Country", cboCountry.SelectedValue, "name", "name", False)
                    Process.LoadRadComboTextAndValueP1(cboOffice, "Company_Structure_Get_Country", cboCountry.SelectedValue, "name", "name", False)
                    'Process.LoadRadComboTextAndValueP2(cboOffice, "Company_Structure_Get_ByLevel", "2", Process.GetCompanyName(""), "name", "name", False)
                    Process.AssignRadComboValue(cboOffice, strUser.Tables(0).Rows(0).Item("Office").ToString)

                    Process.LoadRadComboTextAndValueP2(cboreviewerI, "Emp_PersonalDetail_get_Superiors", cboJobGrade.SelectedValue, Process.GetCompanyName, "name", "EmpiD", True)
                    Process.AssignRadComboValue(cboreviewerI, strUser.Tables(0).Rows(0).Item("coachid").ToString)

                Else
                    txtempid.Text = Session("EmpID")
                    aname.Value = Process.GetEmployeeName(txtempid.Text)
                    txtEmpID.Enabled = False
                    txtid.Text = "0"

                    Dim strWork As New DataSet
                    strWork = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "Select f.Period, mm.Years from (select case when b.[No] >= 12 then 1 Else b.[No] + 1 End MonthNo,case when b.[No] >= 12 then a.StartYear + 1 Else a.StartYear  End Years from Emp_Work_History a inner join Data_Period b on rtrim(ltrim(a.StartDate)) = ltrim(rtrim(b.Period))  where EndDate = 'Present' and a.EmpID = '" & txtEmpID.Text & "') mm inner join dbo.Data_Period f on mm.MonthNo = f.[No]")
                    If strWork.Tables(0).Rows.Count > 0 Then
                        Process.AssignRadComboValue(cbostartmonth, strWork.Tables(0).Rows(0).Item("Period").ToString)
                        Process.AssignRadComboValue(cbostartyear, strWork.Tables(0).Rows(0).Item("Years").ToString)
                    End If
                End If

                If cboendmonth.SelectedValue.ToLower = "n/a" Or cboendmonth.SelectedValue.ToLower = "present" Then

                    divendyear.Visible = False
                Else
                    divendyear.Visible = True
                End If

            End If
            cboJobGrade.Focus()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.message, "danger")
        End Try
    End Sub
    Private Function GetIdentity(ByVal empid As String, ByVal grade As String, ByVal jobtitle As String, _
                                 ByVal supervisor As String, ByVal coach As String, ByVal dept As String, _
                                  ByVal office As String, ByVal jobtype As String, ByVal slocation As String, ByVal scountry As String,
                                   ByVal startdate As String, ByVal startyear As String, ByVal enddate As String, ByVal endyear As String) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Emp_Work_History_Update"
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@EmpID", SqlDbType.VarChar).Value = empid
            cmd.Parameters.Add("@GradeLevel", SqlDbType.VarChar).Value = grade
            cmd.Parameters.Add("@JobTitle", SqlDbType.VarChar).Value = jobtitle
            cmd.Parameters.Add("@Supervisor", SqlDbType.VarChar).Value = supervisor
            cmd.Parameters.Add("@Supervisor2", SqlDbType.VarChar).Value = cboReviewerII.SelectedValue
            cmd.Parameters.Add("@IndirectSupervisor", SqlDbType.VarChar).Value = coach
            cmd.Parameters.Add("@DeptType", SqlDbType.VarChar).Value = dept
            cmd.Parameters.Add("@Office", SqlDbType.VarChar).Value = office
            cmd.Parameters.Add("@JobType", SqlDbType.VarChar).Value = jobtype
            cmd.Parameters.Add("@Location", SqlDbType.VarChar).Value = slocation
            cmd.Parameters.Add("@Country", SqlDbType.VarChar).Value = scountry
            cmd.Parameters.Add("@StartDate", SqlDbType.VarChar).Value = startdate
            cmd.Parameters.Add("@StartYear", SqlDbType.VarChar).Value = startyear
            cmd.Parameters.Add("@EndDate", SqlDbType.VarChar).Value = enddate
            cmd.Parameters.Add("@EndYear", SqlDbType.VarChar).Value = endyear
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.message, "danger")
            Return 0
        End Try
    End Function
    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            Dim lblstatus As String = ""
            If txtid.Text <> "0" Then
                If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False Then
                    Process.loadalert(divalert, msgalert, Process.privilegemsg, "danger")
                    Exit Sub
                End If
            End If



            If (cboJobGrade.SelectedValue Is Nothing) Then
                lblstatus = "Grade required!"
                cboJobGrade.Focus()
                Exit Sub
            End If

            If (cbojobtype.SelectedValue Is Nothing) Then
                lblstatus = "Job Type required!"
                cbojobtype.Focus()
                Exit Sub
            End If

            If (cboJobTitle.SelectedValue Is Nothing) Then
                lblstatus = "Job Title required!"
                cboJobTitle.Focus()
                Exit Sub
            End If

            If (cboCountry.SelectedValue Is Nothing) Then
                lblstatus = "Country required!"
                cboCountry.Focus()
                Exit Sub
            End If

            If (cboCountry.SelectedItem.Text.ToLower = "--select--") Then
                lblstatus = "Country required!"
                cboCountry.Focus()
                Exit Sub
            End If

            If (cboOffice.SelectedValue Is Nothing) Then
                lblstatus = "Office required!"
                cboOffice.Focus()
                Exit Sub
            End If

            If cbostartmonth.SelectedValue Is Nothing Then
                lblstatus = "Job start month required!"
                cbostartmonth.Focus()
                Exit Sub
            End If

            If cbostartyear.SelectedValue Is Nothing Then
                lblstatus = "Job start year required!"
                cbostartyear.Focus()
                Exit Sub
            End If

            If cboendmonth.SelectedValue.Trim = "" Then
                lblstatus = "Job end month required, if position is current then select Present!"
                cboendmonth.Focus()
                Exit Sub
            End If

            If cboendyear.SelectedItem.Text.Trim <> "" And cboendyear.Visible = True Then
                If CInt(cboendyear.SelectedItem.Text) > CInt(cboendyear.SelectedItem.Text) Then
                    lblstatus = "Start Year cannot be great Year Completed"
                    cboendyear.Focus()
                    Exit Sub
                End If
            End If

            If txtid.Text <> "0" Then
                Dim strUser As New DataSet
                strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Work_History_get", Request.QueryString("id1"))
                olddata(0) = strUser.Tables(0).Rows(0).Item("id").ToString
                olddata(1) = strUser.Tables(0).Rows(0).Item("EmpID").ToString
                olddata(2) = strUser.Tables(0).Rows(0).Item("GradeLevel").ToString
                olddata(3) = strUser.Tables(0).Rows(0).Item("JobTitle").ToString
                olddata(4) = strUser.Tables(0).Rows(0).Item("Supervisor").ToString
                olddata(5) = strUser.Tables(0).Rows(0).Item("Supervisor2").ToString
                olddata(6) = strUser.Tables(0).Rows(0).Item("IndirectSupervisor").ToString
                olddata(7) = strUser.Tables(0).Rows(0).Item("DeptType").ToString
                olddata(8) = strUser.Tables(0).Rows(0).Item("Office").ToString
                olddata(9) = strUser.Tables(0).Rows(0).Item("JobType").ToString
                olddata(10) = strUser.Tables(0).Rows(0).Item("Country").ToString
                olddata(11) = strUser.Tables(0).Rows(0).Item("Location").ToString
                olddata(12) = strUser.Tables(0).Rows(0).Item("StartDate").ToString
                olddata(13) = strUser.Tables(0).Rows(0).Item("StartYear").ToString
                olddata(14) = strUser.Tables(0).Rows(0).Item("EndDate").ToString
                olddata(15) = strUser.Tables(0).Rows(0).Item("EndYear").ToString
            End If
       

            If txtid.Text.Trim = "" Or txtid.Text = "0" Then
                EmpWork.ID = 0
            Else
                EmpWork.ID = txtid.Text
            End If
            EmpWork.EmpID = txtEmpID.Text.Trim
            EmpWork.JobGrade = cboJobGrade.SelectedValue
            EmpWork.JobTitle = cboJobTitle.SelectedValue
            EmpWork.Supervisor = cboSupervisor.SelectedValue
            EmpWork.SupervisorII = cboReviewerII.SelectedValue
            EmpWork.Coach = cboreviewerI.SelectedValue
            EmpWork.OfficeLevel = ""
            EmpWork.Office = cboOffice.SelectedValue
            EmpWork.JobStatus = cbojobtype.SelectedValue
            EmpWork.Country = cboCountry.SelectedValue
            EmpWork.Location = cboLocation.SelectedValue
            EmpWork.StartDate = cbostartmonth.SelectedValue
            EmpWork.StartYear = cbostartyear.SelectedItem.Text
            EmpWork.EndDate = cboendmonth.SelectedValue
            If divendyear.Visible = False Then
                EmpWork.EndYear = 0
            Else
                EmpWork.EndYear = cboendyear.SelectedItem.Text
            End If


            Dim OldValue As String = ""
            Dim NewValue As String = ""

            Dim j As Integer = 0

            If txtid.Text <> "0" Then 'Updates
                For Each a In GetType(clsEmpWorkHistory).GetProperties()
                    If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If IsNumeric(a.GetValue(EmpWork, Nothing)) = True And IsNumeric(olddata(j)) = True Then
                                If CDbl(a.GetValue(EmpWork, Nothing)) <> CDbl(olddata(j)) Then
                                    NewValue += a.Name + ": " + a.GetValue(EmpWork, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            Else
                                If a.GetValue(EmpWork, Nothing).ToString <> olddata(j).ToString Then
                                    NewValue += a.Name + ": " + a.GetValue(EmpWork, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            End If
                        End If
                    End If
                    j = j + 1
                Next
            Else
                For Each a In GetType(clsEmpWorkHistory).GetProperties() 'New Entries
                    If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If a.GetValue(EmpWork, Nothing) = Nothing Then
                                NewValue += a.Name + ":" + " " & vbCrLf
                            Else
                                NewValue += a.Name + ": " + a.GetValue(EmpWork, Nothing).ToString & vbCrLf
                            End If
                        End If
                    End If
                Next
            End If


            If txtid.Text = "0" Or txtid.Text = "" Then
                txtid.Text = GetIdentity(EmpWork.EmpID, EmpWork.JobGrade, EmpWork.JobTitle, EmpWork.Supervisor, EmpWork.Coach, EmpWork.OfficeLevel, EmpWork.Office, EmpWork.JobStatus, EmpWork.Location.Trim, EmpWork.Country, EmpWork.StartDate.Trim, EmpWork.StartYear, EmpWork.EndDate.Trim, EmpWork.EndYear)
                If txtid.Text = "0" Then
                    Exit Sub
                End If
            Else
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Work_History_Update", EmpWork.ID, EmpWork.EmpID, EmpWork.JobGrade, EmpWork.JobTitle, EmpWork.Supervisor, EmpWork.SupervisorII, EmpWork.Coach, EmpWork.OfficeLevel, EmpWork.Office, EmpWork.JobStatus, EmpWork.Location.Trim, EmpWork.Country, EmpWork.StartDate.Trim, EmpWork.StartYear, EmpWork.EndDate.Trim, EmpWork.EndYear)
            End If

            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Finance_Salary_Reset_Employee", EmpWork.EmpID)


            If NewValue.Trim = "" And OldValue.Trim = "" Then
            Else
                If txtid.Text = "0" Or txtid.Text = "" Then
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Inserted", "Employee Work History")
                Else
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Updated " + EmpWork.EmpID, "Employee Work Hstory")
                End If
            End If

            If Request.QueryString("recommendation") IsNot Nothing Then
                lblstatus = "Employee Promotion saved"
                Process.loadalert(divalert, msgalert, lblstatus, "success")
            Else
                lblstatus = "Employee Work History updated"
                Process.loadalert(divalert, msgalert, lblstatus, "success")
            End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            'If Session("PreviousCareerPage") IsNot Nothing Then
            '    If Session("PreviousCareerPage").ToString.ToLower.Contains("employeedata") = True Then
            '        Response.Redirect("~/Module/Employee/EmployeeData.aspx?Id=" & txtempid.Text, True)
            '    Else
            '        'Response.Redirect(Session("PreviousCareerPage"), True)
            '        Response.Write("<script language='javascript'> { self.close() }</script>")
            '    End If
            'End If

            'Response.Redirect("~/empdashboard", True)
            Response.Redirect("~/Module/Employee/EmployeeData.aspx?Id=" & txtempid.Text, True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub




    Protected Sub radEndDate_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboendmonth.SelectedIndexChanged
        Try
            If cboendmonth.SelectedValue.ToLower = "n/a" Or cboendmonth.SelectedValue.ToLower = "present" Then
                divendyear.Visible = False
            Else
                divendyear.Visible = True
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub



    Protected Sub radGrade_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboJobGrade.SelectedIndexChanged
        Try
            Process.LoadRadComboTextAndValueP2(cboSupervisor, "Emp_PersonalDetail_get_Superiors", cboJobGrade.SelectedValue, Process.GetCompanyName(""), "name", "EmpID", True)
            Process.LoadRadComboTextAndValueP2(cboreviewerI, "Emp_PersonalDetail_get_Superiors", cboJobGrade.SelectedValue, Process.GetCompanyName(""), "name", "EmpID", True)
            Process.LoadRadComboTextAndValueP2(cboReviewerII, "Emp_PersonalDetail_get_Superiors", cboJobGrade.SelectedValue, Process.GetCompanyName(""), "name", "EmpID", True)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub cboCountry_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboCountry.SelectedIndexChanged
        Try
            Process.LoadRadComboTextAndValueP1(cboLocation, "location_get_country", cboCountry.SelectedValue, "name", "name", False)
            Process.LoadRadComboTextAndValueP1(cboOffice, "Company_Structure_Get_Country", cboCountry.SelectedValue, "name", "name", False)
        Catch ex As Exception

        End Try
    End Sub
End Class