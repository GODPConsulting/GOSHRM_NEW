Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Public Class EmployeeThirdCertification
    Inherits System.Web.UI.Page
    Dim EmpCertification As New clsEmpCertificate
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
                Process.LoadRadDropDownTextAndValue(radCertification, "Certifications_get_all", "name", "name", False)
                Process.LoadRadComboTextAndValueP2(radGrantDate, "Data_Period_get_all", "N/A", "Present", "period", "period", False)
                Process.LoadRadComboTextAndValueP2(radExpiryDate, "Data_Period_get_all", "N/A", "", "period", "period", False)

                radGrantYear.Items.Clear()
                For z As Integer = 1900 To 2050
                    radGrantYear.Items.Add(z.ToString)
                    radExpiryYear.Items.Add(z.ToString)
                Next

                If Request.QueryString("Id1") IsNot Nothing Then
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Certifications_ThirdParty_get", Request.QueryString("id1"))
                    txtEmpID.Text = strUser.Tables(0).Rows(0).Item("EmpID").ToString

                    Process.AssignRadDropDownValue(radCertification, strUser.Tables(0).Rows(0).Item("Certification").ToString)
                    txtInstitute.Text = strUser.Tables(0).Rows(0).Item("Institute").ToString
                    radGrantDate.Text = strUser.Tables(0).Rows(0).Item("DateGranted").ToString
                    radGrantYear.Text = strUser.Tables(0).Rows(0).Item("YearGranted").ToString
                    radExpiryDate.Text = strUser.Tables(0).Rows(0).Item("ExpiryDate").ToString
                    radExpiryYear.Text = strUser.Tables(0).Rows(0).Item("YearExpire").ToString
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    txtEmpID.Enabled = False
                Else
                    txtEmpID.Text = Session("EmpID")
                    txtEmpID.Enabled = False
                End If

                If radExpiryDate.Text.ToLower = "n/a" Then
                    radExpiryYear.Visible = False
                Else
                    radExpiryYear.Visible = True
                End If

                If radGrantDate.Text.ToLower = "n/a" Then
                    radGrantYear.Visible = False
                Else
                    radGrantYear.Visible = True
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

            If (radCertification.SelectedText.Trim = "") Then
                lblstatus.Text = "Certification required!"
                radCertification.Focus()
                Exit Sub
            End If

            If txtInstitute.Text.Trim = "" Then
                lblstatus.Text = "Institute where certificate was obtained is required!"
                txtInstitute.Focus()
                Exit Sub
            End If

            If radGrantDate.Text.Trim = "" Then
                lblstatus.Text = "Date Granted required!"
                radGrantDate.Focus()
                Exit Sub
            End If



            If radGrantYear.Text.Trim = "" Then
                lblstatus.Text = "Year required!"
                radGrantYear.Focus()
                Exit Sub
            End If

            If radExpiryYear.Text.Trim <> "" Then
                If CInt(radGrantYear.Text) > CInt(radExpiryYear.Text) Then
                    lblstatus.Text = "Start Year cannot be great Year Completed"
                    radGrantYear.Focus()
                    Exit Sub
                End If
            End If




            If txtid.Text.Trim = "" Then
                EmpCertification.ID = 0
            Else
                EmpCertification.ID = txtid.Text
            End If
            EmpCertification.EmpID = txtEmpID.Text.Trim
            EmpCertification.Institute = txtInstitute.Text.Trim
            EmpCertification.Certification = radCertification.SelectedText
            EmpCertification.DateGranted = radGrantDate.Text
            EmpCertification.YearGranted = radGrantYear.Text
            EmpCertification.Expirydate = radExpiryDate.Text
            EmpCertification.ExpiryYear = radExpiryYear.Text

            Dim OldValue As String = ""
            Dim NewValue As String = ""

            Dim j As Integer = 0

       

            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Certifications_ThirdParty_update", EmpCertification.ID, EmpCertification.EmpID, EmpCertification.Certification, EmpCertification.Institute, EmpCertification.DateGranted, EmpCertification.YearGranted, EmpCertification.Expirydate, EmpCertification.ExpiryYear)

        
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

    Protected Sub radExpiryDate_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles radExpiryDate.SelectedIndexChanged
        Try
            If radExpiryDate.Text.ToLower = "n/a" Then
                radExpiryYear.Visible = False
            Else
                radExpiryYear.Visible = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub radGrantDate_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles radGrantDate.SelectedIndexChanged
        If radGrantDate.Text.ToLower = "n/a" Then
            radGrantYear.Visible = False
        Else
            radGrantYear.Visible = True
        End If
    End Sub
End Class