Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Public Class EmployeeThirdEducation
    Inherits System.Web.UI.Page
    Dim EmpEducation As New clsEmpEducation
    Dim olddata(8) As String
    Dim AuthenCode As String = "THIRDPARTYREC"



    Private Sub LoadDynamic()
        Try
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                Process.LoadRadDropDownTextAndValue(radQualification, "Education_get_all", "name", "name", False)
                Process.LoadRadComboTextAndValueP2(radStartDate, "Data_Period_get_all", "N/A", "Present", "period", "period", False)
                Process.LoadRadComboTextAndValueP2(radEndDate, "Data_Period_get_all", "N/A", "Present", "period", "period", False)

                radStartYear.Items.Clear()
                radEndYear.Items.Clear()
                For z As Integer = 1900 To 2050
                    radStartYear.Items.Add(z.ToString)
                    radEndYear.Items.Add(z.ToString)
                Next

                If Request.QueryString("Id1") IsNot Nothing Then
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Education_ThirdParty_get", Request.QueryString("id1"))
                    txtEmpID.Text = strUser.Tables(0).Rows(0).Item("EmpID").ToString
                    radQualification.SelectedText = strUser.Tables(0).Rows(0).Item("Qualification").ToString
                    txtInstitute.Text = strUser.Tables(0).Rows(0).Item("Institute").ToString
                    radStartDate.Text = strUser.Tables(0).Rows(0).Item("StartDate").ToString
                    radStartYear.Text = strUser.Tables(0).Rows(0).Item("StartYear").ToString
                    radEndDate.Text = strUser.Tables(0).Rows(0).Item("CompletedOn").ToString
                    radEndYear.Text = strUser.Tables(0).Rows(0).Item("CompletedYear").ToString
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    txtEmpID.Enabled = False
                Else
                    txtEmpID.Text = Session("EmpID")
                    txtEmpID.Enabled = False
                End If
            End If
            txtInstitute.Focus()
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

            If (radQualification.SelectedText.Trim = "") Then
                lblstatus.Text = "Educational Qualification required!"
                radQualification.Focus()
                Exit Sub
            End If

            If txtInstitute.Text.Trim = "" Then
                lblstatus.Text = "Educational Institute where qualification was obtained is required!"
                txtInstitute.Focus()
                Exit Sub
            End If

            If radStartDate.Text.Trim = "" Then
                lblstatus.Text = "Start Date required!"
                radStartDate.Focus()
                Exit Sub
            End If

            If radEndDate.Text.Trim = "" Then
                lblstatus.Text = "Date Complete required!"
                radEndDate.Focus()
                Exit Sub
            End If

            If radStartYear.Text.Trim = "" Then
                lblstatus.Text = "Start Year required!"
                radStartYear.Focus()
                Exit Sub
            End If

            If radEndYear.Text.Trim = "" Then
                lblstatus.Text = "Year Complete required!"
                radEndYear.Focus()
                Exit Sub
            End If

            If CInt(radStartYear.Text) > CInt(radEndYear.Text) Then
                lblstatus.Text = "Start Year cannot be great Year Completed"
                radStartYear.Focus()
                Exit Sub
            End If

            If txtid.Text.Trim = "" Then
                EmpEducation.ID = 0
            Else
                EmpEducation.ID = txtid.Text
            End If
            EmpEducation.EmpID = txtEmpID.Text.Trim
            EmpEducation.Institute = txtInstitute.Text.Trim
            EmpEducation.Qualification = radQualification.SelectedText
            EmpEducation.StartDate = radStartDate.Text
            EmpEducation.StartYear = radStartYear.Text
            EmpEducation.CompletedOn = radEndDate.Text
            EmpEducation.YearCompleted = radEndYear.Text

            Dim OldValue As String = ""
            Dim NewValue As String = ""

            Dim j As Integer = 0

           

            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Education_ThirdParty_update", EmpEducation.ID, EmpEducation.EmpID, EmpEducation.Qualification, EmpEducation.Institute, EmpEducation.StartDate, EmpEducation.StartYear, EmpEducation.CompletedOn, EmpEducation.YearCompleted)

           
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