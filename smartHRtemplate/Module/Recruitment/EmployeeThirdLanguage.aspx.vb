Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Public Class EmployeeThirdLanguage
    Inherits System.Web.UI.Page
    Dim EmpLang As New clsEmpLang
    Dim olddata(6) As String
    Dim AuthenCode As String = "THIRDPARTYREC"



    Private Sub LoadDynamic()
        Try
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                Process.LoadRadDropDownTextAndValue(radLanguage, "Languages_get_all", "name", "name", False)

                radRead.Items.Clear()
                radRead.Items.Add("Basic")
                radRead.Items.Add("Good")
                radRead.Items.Add("Native")
                radRead.Items.Add("Fluent")
                radRead.Items.Add("Poor")

                radSpeak.Items.Clear()
                radSpeak.Items.Add("Basic")
                radSpeak.Items.Add("Good")
                radSpeak.Items.Add("Native")
                radSpeak.Items.Add("Fluent")
                radSpeak.Items.Add("Poor")

                radWrite.Items.Clear()
                radWrite.Items.Add("Basic")
                radWrite.Items.Add("Good")
                radWrite.Items.Add("Native")
                radWrite.Items.Add("Fluent")
                radWrite.Items.Add("Poor")

                If Request.QueryString("Id1") IsNot Nothing Then
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Languages_ThirdParty_get", Request.QueryString("id1"))
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    txtEmpID.Text = strUser.Tables(0).Rows(0).Item("EmpID").ToString
                    radLanguage.SelectedText = strUser.Tables(0).Rows(0).Item("Language").ToString
                    radRead.SelectedText = strUser.Tables(0).Rows(0).Item("Reading").ToString
                    radWrite.SelectedText = strUser.Tables(0).Rows(0).Item("Writing").ToString
                    radSpeak.SelectedText = strUser.Tables(0).Rows(0).Item("Speaking").ToString
                    txtEmpID.Enabled = False
                Else
                    txtEmpID.Text = Session("EmpID")
                    txtEmpID.Enabled = False
                End If
                Session("EmpID") = txtEmpID.Text
            End If
            radLanguage.Focus()
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


            If (radLanguage.SelectedText.Trim = "") Then
                lblstatus.Text = "Language required!"
                radLanguage.Focus()
                Exit Sub
            End If

            If (radRead.SelectedText.Trim = "") Then
                lblstatus.Text = "Read level required!"
                radRead.Focus()
                Exit Sub
            End If

            If (radSpeak.SelectedText.Trim = "") Then
                lblstatus.Text = "Speak level required!"
                radSpeak.Focus()
                Exit Sub
            End If

            If (radWrite.SelectedText.Trim = "") Then
                lblstatus.Text = "Writing level required!"
                radWrite.Focus()
                Exit Sub
            End If

            If txtid.Text.Trim = "" Or txtid.Text = "0" Then
                EmpLang.ID = 0
            Else
                EmpLang.ID = txtid.Text
            End If
            EmpLang.EmpID = txtEmpID.Text.Trim
            EmpLang.Language = radLanguage.SelectedText
            EmpLang.Read = radRead.SelectedText
            EmpLang.Speak = radSpeak.SelectedText
            EmpLang.Write = radWrite.SelectedText


            Dim OldValue As String = ""
            Dim NewValue As String = ""

            Dim j As Integer = 0

     

            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Languages_ThirdParty_updates", EmpLang.ID, EmpLang.EmpID, EmpLang.Language, EmpLang.Read, EmpLang.Write, EmpLang.Speak)

         
            'ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Record saved" + "')", True)
            Response.Redirect("~/Module/Recruitment/ThirdPartyRecruitData.aspx?Id=" & Session("EmpID"), True)
        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            'Response.Write("<script language='javascript'> { self.close() }</script>")
            Response.Redirect("~/Module/Recruitment/ThirdPartyRecruitData.aspx?Id=" & Session("EmpID"), True)
        Catch ex As Exception

        End Try
    End Sub
End Class