Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Public Class EmployeeLanguage
    Inherits System.Web.UI.Page
    Dim EmpLang As New clsEmpLang
    Dim olddata(6) As String
    Dim AuthenCode As String = "EMPLIST"



    Private Sub LoadDynamic()
        Try
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                Process.LoadRadComboTextAndValue(cbolang, "Languages_get_all", "name", "name", False)

                cboreading.Items.Clear()
                cboreading.Items.Add("Basic")
                cboreading.Items.Add("Good")
                cboreading.Items.Add("Native")
                cboreading.Items.Add("Fluent")
                cboreading.Items.Add("Poor")

                cbospeak.Items.Clear()
                cbospeak.Items.Add("Basic")
                cbospeak.Items.Add("Good")
                cbospeak.Items.Add("Native")
                cbospeak.Items.Add("Fluent")
                cbospeak.Items.Add("Poor")

                cbowriting.Items.Clear()
                cbowriting.Items.Add("Basic")
                cbowriting.Items.Add("Good")
                cbowriting.Items.Add("Native")
                cbowriting.Items.Add("Fluent")
                cbowriting.Items.Add("Poor")

                If Request.QueryString("Id1") IsNot Nothing Then
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Languages_get", Request.QueryString("id1"))
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    txtEmpID.Text = strUser.Tables(0).Rows(0).Item("EmpID").ToString
                    Process.AssignRadComboValue(cbolang, strUser.Tables(0).Rows(0).Item("Language").ToString)
                    Process.AssignRadComboValue(cboreading, strUser.Tables(0).Rows(0).Item("Reading").ToString)
                    Process.AssignRadComboValue(cbowriting, strUser.Tables(0).Rows(0).Item("Writing").ToString)
                    Process.AssignRadComboValue(cbospeak, strUser.Tables(0).Rows(0).Item("Speaking").ToString)
                    txtEmpID.Enabled = False
                    cbolang.Enabled = False
                Else
                    Process.LoadRadComboTextAndValue(cbolang, "Languages_get_all", "name", "name", False)
                    txtEmpID.Text = Session("EmpID")
                    txtEmpID.Enabled = False
                    txtid.Text = "0"
                End If
                Session("EmpID") = txtEmpID.Text
                aname.Value = Process.GetEmployeeData(txtEmpID.Text, "fullname")
            End If
            cbolang.Focus()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                Process.loadalert(divalert, msgalert, Process.privilegemsg, "warning")
                Exit Sub
            End If

            Dim lblstatus As String = ""
            If (cbolang.SelectedItem.Text Is Nothing) Then
                lblstatus = "Language required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                cbolang.Focus()
                Exit Sub
            End If

            If (cboreading.SelectedItem.Text Is Nothing) Then
                lblstatus = "Read level required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                cboreading.Focus()
                Exit Sub
            End If

            If (cbospeak.SelectedItem.Text Is Nothing) Then
                lblstatus = "Speak level required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                cbospeak.Focus()
                Exit Sub
            End If

            If (cbowriting.SelectedItem.Text Is Nothing) Then
                lblstatus = "Writing level required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                cbowriting.Focus()
                Exit Sub
            End If


            If Request.QueryString("Id1") IsNot Nothing Then
                Dim strUser As New DataSet
                strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Languages_get", Request.QueryString("id1"))
                olddata(0) = strUser.Tables(0).Rows(0).Item("id").ToString
                olddata(1) = strUser.Tables(0).Rows(0).Item("EmpID").ToString
                olddata(2) = strUser.Tables(0).Rows(0).Item("Language").ToString
                olddata(3) = strUser.Tables(0).Rows(0).Item("Reading").ToString
                olddata(4) = strUser.Tables(0).Rows(0).Item("Writing").ToString
                olddata(5) = strUser.Tables(0).Rows(0).Item("Speaking").ToString
            End If


            If txtid.Text.Trim = "" Then
                EmpLang.ID = 0
            Else
                EmpLang.ID = txtid.Text
            End If
            EmpLang.EmpID = txtEmpID.Text.Trim
            EmpLang.Language = cbolang.SelectedItem.Text
            EmpLang.Read = cboreading.SelectedItem.Text
            EmpLang.Speak = cbospeak.SelectedItem.Text
            EmpLang.Write = cbowriting.SelectedItem.Text


            Dim OldValue As String = ""
            Dim NewValue As String = ""

            Dim j As Integer = 0

            If Request.QueryString("Id1") IsNot Nothing Then 'Updates
                For Each a In GetType(clsEmpLang).GetProperties()
                    If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If IsNumeric(a.GetValue(EmpLang, Nothing)) = True And IsNumeric(olddata(j)) = True Then
                                If CDbl(a.GetValue(EmpLang, Nothing)) <> CDbl(olddata(j)) Then
                                    NewValue += a.Name + ": " + a.GetValue(EmpLang, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            Else
                                If a.GetValue(EmpLang, Nothing).ToString <> olddata(j).ToString Then
                                    NewValue += a.Name + ": " + a.GetValue(EmpLang, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            End If
                        End If
                    End If
                    j = j + 1
                Next
            Else
                For Each a In GetType(clsEmpLang).GetProperties() 'New Entries
                    If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If a.GetValue(EmpLang, Nothing) = Nothing Then
                                NewValue += a.Name + ":" + " " & vbCrLf
                            Else
                                NewValue += a.Name + ": " + a.GetValue(EmpLang, Nothing).ToString & vbCrLf
                            End If
                        End If
                    End If
                Next
            End If



            If txtid.Text = "0" Or txtid.Text = "" Then
                txtid.Text = GetIdentity(EmpLang.EmpID, EmpLang.Language, EmpLang.Read, EmpLang.Write, EmpLang.Speak)
                If txtid.Text = "0" Then
                    Exit Sub
                End If
            Else
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Languages_updates", EmpLang.ID, EmpLang.EmpID, EmpLang.Language, EmpLang.Read, EmpLang.Write, EmpLang.Speak)               
            End If

            lblstatus = "record saved!"
            Process.loadalert(divalert, msgalert, lblstatus, "success")

            If NewValue.Trim = "" And OldValue.Trim = "" Then
            Else
                If Request.QueryString("Id1") IsNot Nothing Then
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Updated record " + EmpLang.EmpID, "Employee Language")
                Else
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Inserted", "Employee Dependants")
                End If
            End If
            
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Function GetIdentity(ByVal empid As String, ByVal lang As String, ByVal reading As String, ByVal writing As String, ByVal speak As String) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Emp_Languages_updates"
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@Emp", SqlDbType.VarChar).Value = empid
            cmd.Parameters.Add("@Language", SqlDbType.VarChar).Value = lang
            cmd.Parameters.Add("@Reading", SqlDbType.VarChar).Value = reading
            cmd.Parameters.Add("@Writing", SqlDbType.VarChar).Value = writing
            cmd.Parameters.Add("@Speaking", SqlDbType.VarChar).Value = speak

            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteNonQuery()
            Return obj.ToString()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
            Return "0"
        End Try
    End Function
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            ''Response.Write("<script language='javascript'> { self.close() }</script>")
            'Response.Redirect("~/Module/Employee/EmployeeData?Id=" & Session("EmpID"), True)
            If Request.QueryString("self") IsNot Nothing Then
                Response.Redirect("employeeprofile", True)
            Else
                Response.Redirect("EmployeeData?id=" + txtEmpID.Text, True)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnClose_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/Module/Employee/EmployeeData.aspx?Id=" & Session("EmpID"), True)
    End Sub
End Class