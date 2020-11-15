Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Public Class EmployeeDependantUpdate
    Inherits System.Web.UI.Page
    Dim dependants As New clsEmpDependants
    Dim olddata(5) As String
    Dim AuthenCode As String = "EMPLIST"



    Private Sub LoadDynamic(ByVal id As String)
        Try
            Dim strUser As New DataSet
            strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Dependents_get", id)
            txtEmpID.Text = strUser.Tables(0).Rows(0).Item("EmpID").ToString
            aname.Value = strUser.Tables(0).Rows(0).Item("Name").ToString
            Process.AssignRadDropDownValue(radRelationship, strUser.Tables(0).Rows(0).Item("Relationship").ToString)
            If IsDBNull(strUser.Tables(0).Rows(0).Item("DateOfBirth").ToString) = False Then
                radDOB.SelectedDate = CDate(strUser.Tables(0).Rows(0).Item("DateOfBirth"))
            End If
            aapprovalstat.Value = strUser.Tables(0).Rows(0).Item("approvalstat").ToString
            If (aapprovalstat.Value.ToLower = "pending") Then
                divchanges.Visible = True
            Else
                divchanges.Visible = False
            End If
            txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
            txtEmpID.Enabled = False
            atempdateofbirth.Value = strUser.Tables(0).Rows(0).Item("tempDateOfBirth")
            atempname.Value = strUser.Tables(0).Rows(0).Item("tempName")
            atemprelationship.Value = strUser.Tables(0).Rows(0).Item("tempRelationship")
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                Process.LoadRadDropDownTextAndValue(radRelationship, "emp_relationship_get_all", "name", "name", False)

                If (Request.UrlReferrer.ToString.ToLower.Contains("employeedependant") = False) Then
                    Session("PreviousEmployeePage") = Request.UrlReferrer.ToString
                End If


                If Request.QueryString("Id1") IsNot Nothing Or Request.QueryString("id") IsNot Nothing Then
                    If Request.QueryString("Id1") IsNot Nothing Then
                        LoadDynamic(Request.QueryString("id1"))
                    Else
                        LoadDynamic(Request.QueryString("id"))
                        divbtnapprove.Visible = False
                    End If


                ElseIf Request.QueryString("empid") IsNot Nothing Then
                    txtEmpID.Text = Request.QueryString("empid")
                    divapprovalstat.Visible = False
                    divchanges.Visible = False
                    txtid.Text = "0"
                Else
                    txtEmpID.Text = Session("EmpID")
                    txtEmpID.Enabled = False
                    divapprovalstat.Visible = False
                    divchanges.Visible = False
                    txtid.Text = "0"
                End If
            End If
            aname.Focus()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            Dim lblstatus As String = ""
            'If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
            '    lblstatus = "You don't have privilege to perform this action"
            '    ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
            '    Process.loadalert(divalert, msgalert, lblstatus, "warning")
            '    Exit Sub
            'End If

            If (radDOB.SelectedDate Is Nothing) Then
                lblstatus = "Date of Birth required"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                radDOB.Focus()
                Exit Sub
            End If

            If (radRelationship.SelectedText.Trim = "") Then
                lblstatus = "Dependant Relationship required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                radRelationship.Focus()
                Exit Sub
            End If

            If aname.Value.Trim = "" Then
                lblstatus = "Name is required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                aname.Focus()
                Exit Sub
            End If

            If Request.QueryString("Id1") IsNot Nothing Then
                Dim strUser As New DataSet
                strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Dependents", Request.QueryString("id1"))
                olddata(0) = strUser.Tables(0).Rows(0).Item("id").ToString
                olddata(1) = strUser.Tables(0).Rows(0).Item("EmpID").ToString
                olddata(2) = strUser.Tables(0).Rows(0).Item("Name").ToString
                olddata(3) = strUser.Tables(0).Rows(0).Item("Relationship").ToString
                olddata(4) = strUser.Tables(0).Rows(0).Item("DateOfBirth").ToString
            End If


            If txtid.Text.Trim = "" Then
                dependants.ID = 0
            Else
                dependants.ID = txtid.Text
            End If
            dependants.EmpID = txtEmpID.Text.Trim
            dependants.DependantName = aname.Value.Trim
            dependants.Relationship = radRelationship.SelectedText
            dependants.DateOfBirth = radDOB.SelectedDate

            Dim OldValue As String = ""
            Dim NewValue As String = ""

            Dim j As Integer = 0

            If Request.QueryString("Id1") IsNot Nothing Then 'Updates
                For Each a In GetType(clsEmpDependants).GetProperties()
                    If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If IsNumeric(a.GetValue(dependants, Nothing)) = True And IsNumeric(olddata(j)) = True Then
                                If CDbl(a.GetValue(dependants, Nothing)) <> CDbl(olddata(j)) Then
                                    NewValue += a.Name + ": " + a.GetValue(dependants, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            Else
                                If a.GetValue(dependants, Nothing).ToString <> olddata(j).ToString Then
                                    NewValue += a.Name + ": " + a.GetValue(dependants, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            End If
                        End If
                    End If
                    j = j + 1
                Next
            Else
                For Each a In GetType(clsEmpDependants).GetProperties() 'New Entries
                    If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If a.GetValue(dependants, Nothing) = Nothing Then
                                NewValue += a.Name + ":" + " " & vbCrLf
                            Else
                                NewValue += a.Name + ": " + a.GetValue(dependants, Nothing).ToString & vbCrLf
                            End If
                        End If
                    End If
                Next
            End If


            If txtid.Text = "0" Then
                txtid.Text = GetIdentity()
            Else
                If Request.QueryString("self") IsNot Nothing Then
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Dependents_Emp_update", dependants.ID, dependants.EmpID, dependants.DependantName, dependants.Relationship, dependants.DateOfBirth)
                Else
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Dependents_update", dependants.ID, dependants.EmpID, dependants.DependantName, dependants.Relationship, dependants.DateOfBirth)
                End If
            End If

            If Request.QueryString("self") IsNot Nothing Then
                If Process.Mail_HR(Process.GetMailList("hr"), Process.GetEmployeeData(txtEmpID.Text, "fullname"), "Dependant Info update", "", txtEmpID.Text, "", Process.MailSuccessionPlan, Process.ApplicationURL + "/Module/Employee/Employeedependant?id=" + txtid.Text) = True Then
                    lblstatus = "Update successfully sent to HR for approval"
                    Process.loadalert(divalert, msgalert, lblstatus, "success")
                End If
            Else
                lblstatus = "Record saved!"
            End If

            If NewValue.Trim = "" And OldValue.Trim = "" Then
            Else
                If Request.QueryString("Id1") IsNot Nothing Or Request.QueryString("Id") IsNot Nothing Then
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Updated record " + dependants.EmpID, "Employee Dependants")
                Else
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Inserted", "Employee Dependants")
                End If
            End If

            Process.loadalert(divalert, msgalert, lblstatus, "success")

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Function GetIdentity() As String
        Try

            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            If Request.QueryString("self") IsNot Nothing Then
                cmd.CommandText = "Emp_Dependents_Emp_update"
            Else
                cmd.CommandText = "Emp_Dependents_update"
            End If

            cmd.Parameters.Add("@id", SqlDbType.Int).Value = txtid.Text
            cmd.Parameters.Add("@empid", SqlDbType.VarChar).Value = txtEmpID.Text
            cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = aname.Value
            cmd.Parameters.Add("@relationship", SqlDbType.VarChar).Value = radRelationship.SelectedValue
            cmd.Parameters.Add("@dateofbirth", SqlDbType.DateTime).Value = radDOB.SelectedDate
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
            Return "0"
        End Try
    End Function
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            'Response.Write("<script language='javascript'> { self.close() }</script>")
            Response.Redirect(Session("PreviousEmployeePage"), True)

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub Approve(sender As Object, e As EventArgs) Handles lnkApprove.Click
        Try
            Dim confirmValue As String = Request.Form("confirmapprove_value")
            If confirmValue = "Yes" Then
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Dependents_Approve", txtid.Text, Session("UserEmpID"))
                Process.loadalert(divalert, msgalert, "Information approved", "success")
                LoadDynamic(txtid.Text)
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub
    Protected Sub CancelChange(sender As Object, e As EventArgs) Handles lnkCancel.Click
        Try
            Dim confirmValue As String = Request.Form("confirmcancel_value")
            If confirmValue = "Yes" Then
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Dependents_Cancel", txtid.Text, Session("UserEmpID"))
                Process.loadalert(divalert, msgalert, "Information update cancelled", "info")
                LoadDynamic(txtid.Text)
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub
End Class