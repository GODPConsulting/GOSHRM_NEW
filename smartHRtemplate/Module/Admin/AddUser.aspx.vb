Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Telerik.Web.UI

Public Class AddUser

    Inherits System.Web.UI.Page
    Dim AuthenCode As String = "Users"
    Dim newdata As New AppUser

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not Me.IsPostBack Then
                Process.ismulti = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select isnull(ismulticompany,'No')  from general_info")
                If Process.ismulti.ToLower = "no" Then
                    divaccess.Visible = False
                    divaccesslevel.Visible = False
                Else
                    divaccess.Visible = True
                    divaccesslevel.Visible = True
                    Process.LoadRadComboTextAndValueInitiateP1(cbolevel, "Users_Access_Level", Session("Level"), "--select--", "Definition", "level")
                End If


                Process.LoadRadComboTextAndValue(radroletypes, "roles_get_all", "role", "role", False)

                cboIsEmp.Items.Clear()
                cboIsEmp.Items.Add("Yes")
                cboIsEmp.Items.Add("No")

                cbostatus.Items.Clear()
                cbostatus.Items.Add("Enabled")
                cbostatus.Items.Add("Disabled")


                cboEmployee.Visible = True
                fullname.Visible = False
                txtid.Text = "0"
                If Request.QueryString("id") IsNot Nothing Then
                    Process.LoadRadComboTextAndValueInitiateP1(cboEmployee, "Emp_PersonalDetail_Get_Employees", Session("Access"), "--select--", "name", "EmpID")
                    divpwd.Visible = False

                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "users_get", Request.QueryString("id"))
                    Process.AssignRadComboValue(cboEmployee, strUser.Tables(0).Rows(0).Item("empid").ToString)
                    username.Value = strUser.Tables(0).Rows(0).Item("userid").ToString
                    Process.AssignRadComboValue(cboIsEmp, strUser.Tables(0).Rows(0).Item("isemployee").ToString)

                    Process.AssignRadComboValue(cbostatus, strUser.Tables(0).Rows(0).Item("status").ToString)
                    Process.AssignRadComboValue(radroletypes, strUser.Tables(0).Rows(0).Item("role").ToString)

                    usermail.Value = strUser.Tables(0).Rows(0).Item("email").ToString

                    If CBool(strUser.Tables(0).Rows(0).Item("isadmin").ToString) = True Then
                        isadminsystem.Value = "Yes"
                    Else
                        isadminsystem.Value = "No"
                    End If

                    If CBool(strUser.Tables(0).Rows(0).Item("ishr").ToString) = True Then
                        isadminhr.Value = "Yes"
                    Else
                        isadminhr.Value = "No"
                    End If

                    If CBool(strUser.Tables(0).Rows(0).Item("isfinance").ToString) = True Then
                        isadminfinance.Value = "Yes"
                    Else
                        isadminfinance.Value = "No"
                    End If


                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString



                    If Process.ismulti.ToLower = "yes" Then
                        Process.AssignRadComboValue(cbolevel, strUser.Tables(0).Rows(0).Item("accesslevel").ToString)
                        Process.LoadRadComboTextAndValueP1(cboaccess, "Company_Structure_Get_Level", cbolevel.SelectedValue, "name", "name", False)
                        Process.LoadListAndComboxFromDataset(lstaccess, cboaccess, "users_access_get", "structurename", "structurename", username.Value)
                    End If
                    If cboIsEmp.SelectedItem.Text = "Yes" Then
                        cboEmployee.Visible = True
                        fullname.Visible = False
                        Process.AssignRadComboValue(cboEmployee, strUser.Tables(0).Rows(0).Item("empid").ToString)
                        cboEmployee.Enabled = True
                    Else
                        cboEmployee.Visible = False
                        fullname.Visible = True
                        fullname.Value = strUser.Tables(0).Rows(0).Item("name").ToString
                    End If
                Else
                    Process.LoadRadComboTextAndValueInitiateP1(cboEmployee, "Emp_PersonalDetail_Get_Users", Session("Access"), "--select--", "name", "EmpID")
                    txtid.Text = "0"
                End If



            End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub
    Private Function GetIdentity(userid As String, uname As String, role As String, status As String, email As String, isemployee As String, pwd As String, uid As String, empid As String, level As Integer) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "users_add"
            cmd.Parameters.Add("@userid", SqlDbType.VarChar).Value = userid
            cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = uname
            cmd.Parameters.Add("@role", SqlDbType.VarChar).Value = role
            cmd.Parameters.Add("@status", SqlDbType.VarChar).Value = status
            cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = email
            cmd.Parameters.Add("@isemployee", SqlDbType.VarChar).Value = isemployee
            cmd.Parameters.Add("@pwd", SqlDbType.VarChar).Value = pwd
            cmd.Parameters.Add("@Uid", SqlDbType.VarChar).Value = uid
            cmd.Parameters.Add("@empid", SqlDbType.VarChar).Value = empid
            cmd.Parameters.Add("@level", SqlDbType.Int).Value = level
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
            Return 0
        End Try
    End Function

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            Dim appusers As New AppUser

            If txtid.Text <> "0" Then
                If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False Then
                    Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "warning")
                    Exit Sub
                End If
            End If

            Dim lblstatus As String = ""
            If usermail.Value.Contains("@") = False Or usermail.Value.Contains(".") = False Then
                lblstatus = "Valid Email required"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                usermail.Focus()
                Exit Sub
            End If

            If divpwd.Visible = True Then
                If pwd.Value <> pwdconfirm.Value Then
                    lblstatus = "Password Mismatch"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    Exit Sub
                End If

                If pwd.Value.Trim = "" Then
                    lblstatus = "Password is required!"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    pwd.Focus()
                    Exit Sub
                End If
            End If


            If cboIsEmp.SelectedItem.Text.ToUpper = "NO" Then
                If fullname.Value.Trim = "" Then
                    lblstatus = "Name is required!"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    fullname.Focus()
                    Exit Sub
                End If
            Else
                If cboEmployee.SelectedItem.Text = "N/A" Then
                    lblstatus = "User Employee is required!"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    cboEmployee.Focus()
                    Exit Sub
                End If
            End If


            If username.Value.Trim = "" Then
                lblstatus = "Username is required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                username.Focus()
                Exit Sub
            End If

            If username.Value.Contains(" ") Then
                lblstatus = "text space not allowed in Username!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                username.Focus()
                Exit Sub
            End If

            If ((cboIsEmp.SelectedItem.Text = "No") And (isadminhr.Value = "Yes" Or isadminfinance.Value = "Yes")) Then
                lblstatus = "User is not an Employee, cannot be assigned HR Role or Finance Role"
                Exit Sub
            End If

            Dim olddata(11) As String
            If txtid.Text <> "0" Then
                Dim strUser As New DataSet
                strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "users_get", txtid.Text)
                olddata(0) = strUser.Tables(0).Rows(0).Item("userid").ToString
                olddata(1) = strUser.Tables(0).Rows(0).Item("name").ToString
                olddata(2) = strUser.Tables(0).Rows(0).Item("role").ToString
                olddata(3) = strUser.Tables(0).Rows(0).Item("status").ToString
                olddata(4) = strUser.Tables(0).Rows(0).Item("email").ToString
                olddata(5) = strUser.Tables(0).Rows(0).Item("isemployee").ToString
                olddata(6) = strUser.Tables(0).Rows(0).Item("isAdmin").ToString
                olddata(7) = strUser.Tables(0).Rows(0).Item("isHR").ToString
                olddata(8) = strUser.Tables(0).Rows(0).Item("isFinance").ToString
                olddata(9) = strUser.Tables(0).Rows(0).Item("EmpID").ToString
                olddata(10) = strUser.Tables(0).Rows(0).Item("StructureType").ToString
            End If



            appusers.Userid = username.Value.Trim
            If cboIsEmp.SelectedItem.Text = "Yes" Then
                appusers.Name = ""
                appusers.EmpID = cboEmployee.SelectedItem.Value
            Else
                appusers.Name = fullname.Value.Trim
                appusers.EmpID = ""
            End If
            appusers.Role = radroletypes.SelectedItem.Value
            appusers.Status = cbostatus.SelectedItem.Text
            appusers.IsEmployee = cboIsEmp.SelectedItem.Text

            appusers.EMail = usermail.Value.Trim

            Dim slevel As String = ""
            If Process.ismulti.ToLower = "no" Then
                slevel = 1
                appusers.AccessLevel = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select top 1 [Definition]  from StructureDefinition order by [Level] ")
            Else
                slevel = cbolevel.SelectedValue
                appusers.AccessLevel = cbolevel.SelectedItem.Text
            End If


            Dim Description As String = ""

            'For Each a In GetType(AppUser).GetProperties()
            '    If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
            '        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
            '            If a.GetValue(appusers, Nothing) = Nothing Then
            '                Description += a.Name + ":" + " " & vbCrLf
            '            Else
            '                Description += a.Name + ": " + a.GetValue(appusers, Nothing).ToString & vbCrLf
            '            End If
            '        End If
            '    End If
            'Next


            If txtid.Text = "0" Then
                txtid.Text = GetIdentity(appusers.Userid, appusers.Name, appusers.Role, appusers.Status, appusers.EMail, appusers.IsEmployee, Process.Encrypt(pwd.Value), Session("LoginID"), appusers.EmpID, slevel)
                If txtid.Text = "0" Then
                    Exit Sub
                End If
                divpwd.Visible = False

                Process.User_Notification(usermail.Value, appusers.Name, username.Value, pwd.Value, Process.ApplicationURL & "/Default.aspx")
            Else
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "users_update", txtid.Text, _
                                      appusers.Userid, _
                                      appusers.Name, appusers.Role, _
                                      appusers.Status, appusers.EMail, _
                                      appusers.IsEmployee, Session("LoginID"), _
                                      appusers.EmpID, slevel)
            End If


            'SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "users_add", appusers.Userid, appusers.Name, appusers.Role, appusers.Status, appusers.EMail, appusers.IsEmployee, Process.Encrypt(pwd.Value), Session("LoginID"), appusers.EmpID, slevel)
            'Role select


            If cboIsEmp.SelectedItem.Text = "Yes" Then
                If isadminhr.Value = "Yes" Then
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "users_hr_managers_update", cboEmployee.SelectedItem.Value)
                Else
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "users_hr_managers_delete", cboEmployee.SelectedItem.Value)
                End If
            End If



            If cboIsEmp.SelectedItem.Text = "Yes" Then
                If isadminfinance.Value = "Yes" Then
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "users_finance_managers_update", cboEmployee.SelectedItem.Value)
                Else
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "users_finance_managers_delete", cboEmployee.SelectedItem.Value)
                End If
            End If


            If isadminsystem.Value = "Yes" Then
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "users_system_admin_update", username.Value.Trim)
            Else
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "users_system_admin_delete", username.Value.Trim)
            End If

            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "users_access_delete", username.Value.Trim)
            Dim collection As IList(Of RadComboBoxItem) = cboaccess.CheckedItems
            If (collection.Count <> 0) Then
                For Each item As RadComboBoxItem In collection
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "users_access_update", username.Value.Trim, item.Value)
                Next

            End If

            Dim OldValue As String = ""
            Dim NewValue As String = ""
            Dim j As Integer = 0
            If olddata(0) IsNot Nothing Then 'Updates
                For Each a In GetType(AppUser).GetProperties()
                    If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If IsNumeric(a.GetValue(appusers, Nothing)) = True And IsNumeric(olddata(j)) = True Then
                                If CDbl(a.GetValue(appusers, Nothing)) <> CDbl(olddata(j)) Then
                                    NewValue += a.Name + ": " + a.GetValue(appusers, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            Else
                                If a.GetValue(appusers, Nothing).ToString <> olddata(j).ToString Then
                                    NewValue += a.Name + ": " + a.GetValue(appusers, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            End If
                        End If
                    End If
                    j = j + 1
                Next
                If NewValue.Trim = "" And OldValue.Trim = "" Then
                Else
                    Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Updated " + appusers.Userid, "User")
                End If
            Else
                For Each a In GetType(AppUser).GetProperties() 'New Entries
                    If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If a.GetValue(appusers, Nothing) = Nothing Then
                                NewValue += a.Name + ":" + " " & vbCrLf
                            Else
                                NewValue += a.Name + ": " + a.GetValue(appusers, Nothing).ToString & vbCrLf
                            End If
                        End If
                    End If
                Next
                Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate("", Description, "Inserted", "User")
            End If


            Process.loadalert(divalert, msgalert, "Record saved", "success")

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            'Response.Write("<script language='javascript'> { self.close() }</script>")
            Response.Redirect("~/Module/Admin/users.aspx", True)
        Catch ex As Exception

        End Try
    End Sub





    Protected Sub rdoIsEmp_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboIsEmp.SelectedIndexChanged
        If cboIsEmp.SelectedItem.Text = "Yes" Then
            cboEmployee.Visible = True
            fullname.Visible = False
        Else
            cboEmployee.Visible = False
            fullname.Visible = True
        End If

    End Sub

    Protected Sub cboEmployee_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboEmployee.SelectedIndexChanged
        Try
            Dim strUser As New DataSet
            strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_PersonalDetail_get", cboEmployee.SelectedItem.Value)
            If strUser.Tables(0).Rows.Count > 0 Then
                usermail.Value = strUser.Tables(0).Rows(0).Item("Office Email").ToString
            End If
        Catch ex As Exception

        End Try
    End Sub




    Private Sub cbolevel_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cbolevel.SelectedIndexChanged
        Try
            Process.LoadRadComboTextAndValueP1(cboaccess, "Company_Structure_Get_Level", cbolevel.SelectedValue, "Name", "Name", False)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnaccess_Click(sender As Object, e As System.EventArgs) Handles btnaccess.Click
        Try
            Process.LoadListBoxFromCombo(lstaccess, cboaccess)
        Catch ex As Exception

        End Try
    End Sub
End Class