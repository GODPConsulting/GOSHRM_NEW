Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Public Class EmployeeThirdSkills
    Inherits System.Web.UI.Page
    Dim EmpSkill As New clsEmpSkill
    Dim olddata(3) As String
    Dim AuthenCode As String = "THIRDPARTYREC"



    Private Sub LoadDynamic()
        Try
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                Process.LoadRadDropDownTextAndValue(radSkill, "Skills_get_all", "name", "name", False)

                If Request.QueryString("Id1") IsNot Nothing Then
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Skills_ThirdParty_get", Request.QueryString("id1"))
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

        

            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Skills_ThirdParty_Update", EmpSkill.ID, EmpSkill.EmpID, EmpSkill.Skill)

            Response.Redirect("~/Module/Recruitment/ThirdPartyRecruitData.aspx?Id=" & Session("EmpID"), True)
        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Response.Redirect("~/Module/Recruitment/ThirdPartyRecruitData.aspx?Id=" & Session("EmpID"), True)
        Catch ex As Exception

        End Try
    End Sub
End Class