Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Microsoft.VisualBasic


Public Class LeaveAllowanceGrade
    Inherits System.Web.UI.Page
    Dim TimeSheet As New clsTimeSheet
    Dim leaveemp As New clsLeaveAllowanceGrade
    Dim Pages As String = "Leave Allowance Setting"
    Dim AuthenCode As String = "LEAVEPAY"
    Dim olddata(11) As String
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



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                Process.LoadRadDropDownTextAndValue(radJobGrade, "Job_Grade_get_all", "name", "name", False)
                If Request.QueryString("id") IsNot Nothing Then                   
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Finance_Pension_Employee_Setup_Get", Request.QueryString("id"))
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    Process.AssignRadDropDownValue(radJobGrade, strUser.Tables(0).Rows(0).Item("gradename").ToString)
                    txtContribution.Value = strUser.Tables(0).Rows(0).Item("allowance").ToString
                    radJobGrade.Enabled = False
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
            cmd.CommandText = "Job_Grade_Leave_Allowance_Update"
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@GradeName", SqlDbType.VarChar).Value = radJobGrade.SelectedValue
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


            If (radJobGrade.SelectedValue Is Nothing) Then
                Process.loadalert(divalert, msgalert, "Job Grade required!", "danger")
                radJobGrade.Focus()
                Exit Sub
            End If

            If IsNumeric(txtContribution.Value) = False Then
                Process.loadalert(divalert, msgalert, "Percentage Allowance required!", "danger")
                txtContribution.Focus()
                Exit Sub
            End If

            If txtid.Text <> "0" Then
                Dim strUser As New DataSet
                strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Job_Grade_Leave_Allowance_Get", txtid.Text)
                olddata(0) = strUser.Tables(0).Rows(0).Item("id").ToString
                olddata(1) = strUser.Tables(0).Rows(0).Item("gradename").ToString
                olddata(2) = strUser.Tables(0).Rows(0).Item("allowance").ToString
            End If

            If txtid.Text.Trim = "" Then
                leaveemp.id = 0
            Else
                leaveemp.id = txtid.Text
            End If
            leaveemp.Grade = radJobGrade.SelectedValue
            leaveemp.Contribution = txtContribution.Value

            Dim OldValue As String = ""
            Dim NewValue As String = ""

            Dim j As Integer = 0

            If txtid.Text <> "0" Then 'Updates
                For Each a In GetType(clsLeaveAllowanceGrade).GetProperties()
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
                For Each a In GetType(clsLeaveAllowanceGrade).GetProperties() 'New Entries
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

            txtid.Text = GetIdentity()
            If txtid.Text = "0" Then
                Process.loadalert(divalert, msgalert, Process.strExp, "danger")
                Exit Sub
            End If

            If NewValue.Trim = "" And OldValue.Trim = "" Then
            Else
                If OldValue.Trim <> "" Then
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Updated " & radJobGrade.SelectedValue, Pages)
                Else
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Inserted", Pages)
                End If

            End If
            Process.loadalert(divalert, msgalert, "Record saved", "success")

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        Finally

        End Try
    End Sub


    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect("LeaveAllowanceSetup")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
End Class