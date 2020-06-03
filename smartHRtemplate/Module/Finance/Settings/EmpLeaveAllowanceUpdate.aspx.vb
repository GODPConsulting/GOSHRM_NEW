Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Microsoft.VisualBasic


Public Class EmpLeaveAllowanceUpdate
    Inherits System.Web.UI.Page
    Dim TimeSheet As New clsTimeSheet

    Dim AuthenCode As String = "LEAVEPAY"
    Dim olddata(3) As String
    Dim emp_emailaddr As String
    Dim approver1_emailaddr As String
    Dim approver2_emailaddr As String
    Dim LeaveBalance As Integer = 0
    Dim NoDays As Integer = 0
    Dim Level1(2) As String
    Dim Level2(2) As String
    Dim Separators() As Char = {":"c}
    Dim EmpID_1 As String = ""
    Dim EmpID_2 As String = ""
    Dim EmpID_1_Name As String = ""
    Dim EmpID_2_Name As String = ""
    Dim isEligible As String = "Yes"
    Dim leaveemp As New clsLeaveAllowanceEmployee
    Dim Pages As String = "Employee Leave Allowance Setting"



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                Dim ismulti As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select isnull(ismulticompany,'No')  from general_info")
                If ismulti.ToLower = "no" Then
                    Process.LoadRadComboTextAndValueP2(cboCompany, "Company_Structure_Get_ByLevel", "1", Session("Access"), "name", "name", False)
                Else
                    Process.LoadRadComboTextAndValueP2(cboCompany, "Company_Structure_Get_ByLevel", "2", Session("Access"), "name", "name", False)
                End If
                Process.LoadRadComboTextAndValueP2(cboEmployee, "Emp_PersonalDetail_get_all_Specific", "", cboCompany.SelectedValue, "employee2", "empid", False)
                If Request.QueryString("id") IsNot Nothing Then
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Leave_Allowance_Get", Request.QueryString("id"))
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    Process.AssignRadComboValue(cboCompany, strUser.Tables(0).Rows(0).Item("company").ToString)
                    Process.AssignRadComboValue(cboEmployee, strUser.Tables(0).Rows(0).Item("empid").ToString)
                    txtContribution.Value = strUser.Tables(0).Rows(0).Item("allowance").ToString
                    cboCompany.Enabled = False
                    cboEmployee.Enabled = False
                Else
                    txtid.Text = "0"
                End If
            End If
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
            cmd.CommandText = "Emp_Leave_Allowance_Update"
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@Empid", SqlDbType.VarChar).Value = cboEmployee.SelectedValue
            cmd.Parameters.Add("@allowance", SqlDbType.Decimal).Value = txtContribution.Value
            cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = Session("UserEmpID")
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()


        Catch ex As Exception
            Process.strExp = ex.Message
            Return 0
        End Try
    End Function

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            If (txtid.Text <> "0" And txtid.Text.Trim <> "") Then
                If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False And Process.AuthenAction(Session("role"), AuthenCode, "Update") = False Then
                    Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "danger")
                    Exit Sub
                End If
            Else
                If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False And Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                    Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "danger")
                    Exit Sub
                End If
            End If

            Process.loadalert(divalert, msgalert, "Record saving, please wait ...", "danger")

            If IsNumeric(txtContribution.Value) = False Then
                Process.loadalert(divalert, msgalert, "Percentage Allowance required!", "danger")
                txtContribution.Focus()
                Exit Sub
            End If

            If txtid.Text <> "0" Then
                Dim strUser As New DataSet
                strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Leave_Allowance_Get", txtid.Text)
                olddata(0) = strUser.Tables(0).Rows(0).Item("id").ToString
                olddata(1) = strUser.Tables(0).Rows(0).Item("empid").ToString
                olddata(2) = strUser.Tables(0).Rows(0).Item("allowance").ToString
            End If

            If txtid.Text.Trim = "" Then
                leaveemp.id = 0
            Else
                leaveemp.id = txtid.Text
            End If
            leaveemp.EmpID = cboEmployee.SelectedValue
            leaveemp.Contribution = txtContribution.Value

            Dim OldValue As String = ""
            Dim NewValue As String = ""

            Dim j As Integer = 0

            If txtid.Text <> "0" Then 'Updates
                For Each a In GetType(clsLeaveAllowanceEmployee).GetProperties()
                    If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If IsNumeric(a.GetValue(leaveemp, Nothing)) = True And IsNumeric(olddata(j)) = True Then
                                If CDbl(a.GetValue(leaveemp, Nothing)) <> CDbl(olddata(j)) Then
                                    NewValue += a.Name + ": " + a.GetValue(leaveemp, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            Else
                                If a.GetValue(leaveemp, Nothing).ToString <> olddata(j).ToString Then
                                    NewValue += a.Name + ": " + a.GetValue(leaveemp, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            End If
                        End If
                    End If
                    j = j + 1
                Next
            Else
                For Each a In GetType(clsLeaveAllowanceEmployee).GetProperties() 'New Entries
                    If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If a.GetValue(leaveemp, Nothing) = Nothing Then
                                NewValue += a.Name + ":" + " " & vbCrLf
                            Else
                                NewValue += a.Name + ": " + a.GetValue(leaveemp, Nothing).ToString & vbCrLf
                            End If
                        End If
                    End If
                Next
            End If

            If txtid.Text = "0" Then
                txtid.Text = GetIdentity()
                If txtid.Text = "0" Then
                    Process.loadalert(divalert, msgalert, Process.strExp, "danger")
                    Exit Sub
                End If
            Else
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Leave_Allowance_Update", txtid.Text, cboEmployee.SelectedValue, txtContribution.Value, Session("UserEmpID"))
            End If

            If NewValue.Trim = "" And OldValue.Trim = "" Then
            Else
                If OldValue.Trim <> "" Then
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Updated " & cboEmployee.SelectedValue, Pages)
                Else
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Inserted", Pages)
                End If

            End If
            Process.loadalert(divalert, msgalert, "Record saved", "danger")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect("EmpLeaveAllowanceSetup")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub cboCompany_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboCompany.SelectedIndexChanged
        Try
            Process.LoadRadComboTextAndValueP2(cboEmployee, "Emp_PersonalDetail_get_all_Specific", "", cboCompany.SelectedValue, "employee2", "empid", False)
        Catch ex As Exception

        End Try
    End Sub
End Class