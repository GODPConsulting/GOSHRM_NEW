Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Public Class EmployeeSkills
    Inherits System.Web.UI.Page
    Dim EmpSkill As New clsEmpSkill
    Dim olddata(3) As String
    Dim AuthenCode As String = "EMPLIST"



    Private Sub LoadDynamic()
        Try
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                Process.LoadRadDropDownTextAndValue(radSkill, "Job_Title_Skills_Get_All_Distinct", "skills", "skills", False)

                Session("PreviousCareerPage") = Request.UrlReferrer.ToString

                If Request.QueryString("Id1") IsNot Nothing Then
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Skills_get", Request.QueryString("id1"))
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    txtEmpID.Text = strUser.Tables(0).Rows(0).Item("EmpID").ToString
                    radSkill.SelectedText = strUser.Tables(0).Rows(0).Item("Skill").ToString
                    txtEmpID.Enabled = False
                Else
                    txtEmpID.Text = Session("EmpID")
                    txtEmpID.Enabled = False
                End If
                Session("EmpID") = txtEmpID.Text
            End If
            radSkill.Focus()
        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
                Exit Sub
            End If


            If (radSkill.SelectedText.Trim = "") Then
                lblstatus.Text = "Language required!"
                radSkill.Focus()
                Exit Sub
            End If

            'If (radStatus.SelectedText.Trim = "" Or radStatus.SelectedText.Trim = "-- Select --") Then
            '    ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Status required!" + "')", True)
            '    radStatus.Focus()
            '    Exit Sub
            'End If

            If Request.QueryString("Id1") IsNot Nothing Then
                Dim strUser As New DataSet
                strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Skills_get", Request.QueryString("id1"))
                olddata(0) = strUser.Tables(0).Rows(0).Item("id").ToString
                olddata(1) = strUser.Tables(0).Rows(0).Item("EmpID").ToString
                olddata(2) = strUser.Tables(0).Rows(0).Item("Skill").ToString
            End If


            If txtid.Text.Trim = "" Then
                EmpSkill.ID = 0
            Else
                EmpSkill.ID = txtid.Text
            End If
            EmpSkill.EmpID = txtEmpID.Text.Trim
            EmpSkill.Skill = radSkill.SelectedText



            Dim OldValue As String = ""
            Dim NewValue As String = ""

            Dim j As Integer = 0

            If Request.QueryString("Id1") IsNot Nothing Then 'Updates
                For Each a In GetType(clsEmpSkill).GetProperties()
                    If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If IsNumeric(a.GetValue(EmpSkill, Nothing)) = True And IsNumeric(olddata(j)) = True Then
                                If CDbl(a.GetValue(EmpSkill, Nothing)) <> CDbl(olddata(j)) Then
                                    NewValue += a.Name + ": " + a.GetValue(EmpSkill, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            Else
                                If a.GetValue(EmpSkill, Nothing).ToString <> olddata(j).ToString Then
                                    NewValue += a.Name + ": " + a.GetValue(EmpSkill, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            End If
                        End If
                    End If
                    j = j + 1
                Next
            Else
                For Each a In GetType(clsEmpSkill).GetProperties() 'New Entries
                    If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If a.GetValue(EmpSkill, Nothing) = Nothing Then
                                NewValue += a.Name + ":" + " " & vbCrLf
                            Else
                                NewValue += a.Name + ": " + a.GetValue(EmpSkill, Nothing).ToString & vbCrLf
                            End If
                        End If
                    End If
                Next
            End If

            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Skills_Update", EmpSkill.ID, EmpSkill.EmpID, EmpSkill.Skill)

            If NewValue.Trim = "" And OldValue.Trim = "" Then
            Else
                If Request.QueryString("Id1") IsNot Nothing Then
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Updated " + EmpSkill.EmpID, "Employee Skill")
                Else
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Inserted", "Employee Skill")
                End If
            End If
            'ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Record saved" + "')", True)
            Response.Redirect("~/Module/Employee/EmployeeData.aspx?Id=" & Session("EmpID"), True)
        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            'Response.Write("<script language='javascript'> { self.close() }</script>")
            Response.Redirect("~/Module/Employee/EmployeeData.aspx?Id=" & Session("EmpID"), True)
        Catch ex As Exception

        End Try
    End Sub
End Class