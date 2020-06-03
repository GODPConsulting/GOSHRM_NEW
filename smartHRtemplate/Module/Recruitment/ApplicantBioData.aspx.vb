Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports System.IO


Public Class ApplicantBioData
    Inherits System.Web.UI.Page
    Dim education As New clsEducation
    Dim Separators() As Char = {","c}
    Dim section As String = ""
    Dim AuthenCode As String = "JOBINTERVIEW"
    
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'SingleLineTextBox, // will render a textbox 
            'MultiLineTextBox, // will render a text area
            'YesOrNo, //will render a checkbox
            'SingleSelect, //will render a dropdownlist
            'MultiSelect //will render a listbo

            If Not Me.IsPostBack Then


                If Request.QueryString("ApplicantID") IsNot Nothing Then
                    lblApplicantID.Text = Request.QueryString("ApplicantID")
                    MultiView1.ActiveViewIndex = 3
                ElseIf Request.QueryString("ID") IsNot Nothing Then
                    lblApplicantID.Text = Request.QueryString("ID")
                    MultiView1.ActiveViewIndex = 0
                End If

              

                Dim strApp As DataSet
                strApp = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "select b.applicant,c.Title, c.company   from Recruit_Applications a inner join Recruit_Applicants b on a.applicantid = b.id inner join dbo.Recruit_Job_Post c on a.JobID = c.id where a.id =" & lblApplicantID.Text)
                If strApp.Tables(0).Rows.Count > 0 Then
                    txtBankApplicant.Text = strApp.Tables(0).Rows(0).Item("applicant").ToString
                    txtPenApplicant.Text = strApp.Tables(0).Rows(0).Item("applicant").ToString
                    txtNHFApplicant.Text = strApp.Tables(0).Rows(0).Item("applicant").ToString
                    lblname.Text = strApp.Tables(0).Rows(0).Item("applicant").ToString.ToUpper
                    lblcompany.Text = strApp.Tables(0).Rows(0).Item("company").ToString
                    lbljobpost.Text = strApp.Tables(0).Rows(0).Item("Title").ToString
                End If
                

                Process.LoadRadDropDownTextAndValue(radBank, "Finance_Payroll_Accounts_Get_Banks", "Bank", "Bank", False)
                Process.LoadRadDropDownTextAndValue(radPenMgr, "Finance_Pension_Get_PenManagers", "Bank", "Bank", False)
                txtAccountNo.Focus()
                lblSection1ID.Text = "0"
                lblSection2ID.Text = "0"
                lblPFAID.Text = "0"
                lblBankID.Text = "0"

                If Request.QueryString("ApplicantID") Is Nothing And Request.QueryString("ID") Is Nothing Then
                    Process.DisableButton(btnSaveAcct)
                    Process.DisableButton(btnNoAcct)
                    Exit Sub
                End If

                'Checks bank
                Dim Modules As DataSet

                If Request.QueryString("ApplicantID") IsNot Nothing Then
                    Modules = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "Select * from Recruit_Medical_Data_SectionI where ApplicantID  = " & Request.QueryString("ApplicantID"))
                    If Modules.Tables(0).Rows.Count > 0 Then
                        MultiView1.ActiveViewIndex = 4 'Move to Medical Section 2

                        'Check Section 2
                        Modules = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "Select * from Recruit_Medical_Data_SectionII where ApplicantID  = " & Request.QueryString("ApplicantID"))
                        If Modules.Tables(0).Rows.Count > 0 Then
                            MultiView1.ActiveViewIndex = 7 'Means all form already filled and submitted
                            lblend.Text = "Medical Declaration has already been successfully submitted"
                        End If
                    End If
                ElseIf Request.QueryString("ID") IsNot Nothing Then
                    Modules = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "Select * from Recruit_Payroll_Accounts where ApplicantID =" & Request.QueryString("ID"))
                    If Modules.Tables(0).Rows.Count > 0 Then
                        'Means applicant already submitted BAnk Details
                        MultiView1.ActiveViewIndex = 1 'Move to Pension

                        'Check Pension
                        Modules = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "Select * from Recruit_Pension where ApplicantID =" & Request.QueryString("ID"))
                        If Modules.Tables(0).Rows.Count > 0 Then
                            MultiView1.ActiveViewIndex = 2 'Move to NHF

                            'Check Section 1 
                            Modules = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "Select * from Recruit_Payroll_NHF where ApplicantID  = " & Request.QueryString("ID"))
                            If Modules.Tables(0).Rows.Count > 0 Then
                                MultiView1.ActiveViewIndex = 7 'Move to Medical Section 2
                                lblend.Text = "Account Information has already been successfully submitted"

                            End If
                        End If
                    End If
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSaveAcct.Click
        Try

            If (radBank.SelectedText.Contains("Select")) Then
                lblbankstatus.Text = "Your Bank is required!"
                radBank.Focus()
                Exit Sub
            End If

            If txtBankAcct.Visible = True Then
                If txtBankAcct.Text.Trim = "" Then
                    lblbankstatus.Text = "Enter your Bank name required!"
                    txtBankAcct.Focus()
                    Exit Sub
                End If
            End If

            Dim banker As String = ""

            If txtBankAcct.Visible = True Then
                banker = txtBankAcct.Text
            Else
                banker = radBank.SelectedItem.Value
            End If

            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Payroll_Accounts_Update", lblApplicantID.Text, txtBankApplicant.Text, txtAccountNo.Text, banker.Trim.ToUpper)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Your Bank Account Detail have been accepted" + "')", True)
            MultiView1.ActiveViewIndex = 1
        Catch ex As Exception
            lblbankstatus.Text = section & ": " & ex.Message
        End Try

    End Sub



    Protected Sub radBank_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.DropDownListEventArgs) Handles radBank.SelectedIndexChanged
        Try
            If radBank.SelectedItem.Value.ToUpper = "OTHERS" Then
                txtBankAcct.Visible = True
                txtBankAcct.Focus()
            Else
                txtBankAcct.Visible = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnSave0_Click(sender As Object, e As EventArgs) Handles btnNoAcct.Click
        Try
            MultiView1.ActiveViewIndex = 1
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub radPenMgr_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.DropDownListEventArgs) Handles radPenMgr.SelectedIndexChanged
        Try
            If radPenMgr.SelectedItem.Value.ToUpper = "OTHERS" Then
                txtPenMgr.Visible = True
                txtPenMgr.Focus()
            Else
                txtPenMgr.Visible = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles btnSavePFA.Click
        Try

            If (radPenMgr.SelectedText.Contains("Select")) Then
                lblpfastatus.Text = "Your Pension Administrator is required!"
                radPenMgr.Focus()
                Exit Sub
            End If

            If txtPenMgr.Visible = True Then
                If txtPenMgr.Text.Trim = "" Then
                    lblpfastatus.Text = "Enter your RSA!"
                    txtPenMgr.Focus()
                    Exit Sub
                End If
            End If

            Dim pfa As String = ""

            If txtPenMgr.Visible = True Then
                pfa = txtPenMgr.Text
            Else
                pfa = radBank.SelectedItem.Value
            End If

            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Pension_Update", lblApplicantID.Text, txtPenApplicant.Text, txtRSA.Text.ToUpper, pfa.Trim.ToUpper)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Your Pension Account Detail have been accepted" + "')", True)
            MultiView1.ActiveViewIndex = 2
        Catch ex As Exception
            lblpfastatus.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnSave1_Click(sender As Object, e As EventArgs) Handles btnNoPFA.Click
        MultiView1.ActiveViewIndex = 2
    End Sub

    Protected Sub rdoQuestion1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rdoQuestion1.SelectedIndexChanged
        Try
            If rdoQuestion1.SelectedItem.Value.ToUpper = "NO" Then
                txtQuestionNote1.Enabled = False
            Else
                txtQuestionNote1.Enabled = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnMedSaveSection1_Click(sender As Object, e As EventArgs) Handles btnMedSaveSection1.Click
        Try
            If rdoQuestion1.SelectedValue.Trim = "" Then
                lblMedStatusSection1.Text = "Pick Yes / No answer for Question 1"
                rdoQuestion1.Focus()
                Exit Sub
            End If

            If rdoQuestion1.SelectedValue.ToUpper = "YES" And txtQuestionNote1.Text.Trim = "" Then
                lblMedStatusSection1.Text = "Please details for Question 1"
                txtQuestionNote1.Focus()
                Exit Sub
            End If

            If rdoQuestion2.SelectedValue.Trim = "" Then
                lblMedStatusSection1.Text = "Pick Yes / No answer for Question 2"
                rdoQuestion2.Focus()
                Exit Sub
            End If

            If rdoQuestion2.SelectedValue.ToUpper = "YES" And txtQuestionNote2.Text.Trim = "" Then
                lblMedStatusSection1.Text = "Please details for Question 2"
                txtQuestionNote2.Focus()
                Exit Sub
            End If

            If rdoQuestion3.SelectedValue.Trim = "" Then
                lblMedStatusSection1.Text = "Pick Yes / No answer for Question 3"
                rdoQuestion3.Focus()
                Exit Sub
            End If

            If rdoQuestion3.SelectedValue.ToUpper = "YES" And txtQuestNote3.Text.Trim = "" Then
                lblMedStatusSection1.Text = "Please details for Question 3"
                txtQuestNote3.Focus()
                Exit Sub
            End If

            If rdoQuestion4.SelectedValue.Trim = "" Then
                lblMedStatusSection1.Text = "Pick Yes / No answer for Question 4"
                rdoQuestion4.Focus()
                Exit Sub
            End If

            If rdoQuestion4.SelectedValue.ToUpper = "YES" And txtQuestNote4.Text.Trim = "" Then
                lblMedStatusSection1.Text = "Please details for Question 4"
                txtQuestNote4.Focus()
                Exit Sub
            End If

            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Medical_Data_SectionI_Update", 0, lblApplicantID.Text, rdoQuestion1.SelectedItem.Value, txtQuestionNote1.Text, _
                                       rdoQuestion2.SelectedItem.Value, txtQuestionNote2.Text, rdoQuestion3.SelectedItem.Value, txtQuestNote3.Text, rdoQuestion4.SelectedItem.Value, txtQuestNote4.Text)

            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Section I: Medical Information accepted" + "')", True)

            MultiView1.ActiveViewIndex = 4
        Catch ex As Exception
            lblMedStatusSection1.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnMedNextSection_Click(sender As Object, e As EventArgs) Handles btnMedNextSection.Click

    End Sub

    Protected Sub Button1_Click1(sender As Object, e As EventArgs) Handles btnSaveSectionII.Click
        Try
            Dim confirmValue As String = Request.Form("confirm_value")
            If confirmValue = "Yes" Then
                If rdoheartcomplaint.SelectedItem.Value = "--" Or rdoheartcomplaint.SelectedItem.Value = "" Then
                    lblmedsectionII.Text = "reponse on heart complaint required!"
                    rdoheartcomplaint.Focus()
                    Exit Sub

                End If

                If rdorheumatic.SelectedItem.Value = "--" Or rdorheumatic.SelectedItem.Value = "" Then
                    lblmedsectionII.Text = "response on rheumatic fever required!"
                    rdorheumatic.Focus()
                    Exit Sub

                End If

                If rdochestpain.SelectedItem.Value = "--" Or rdochestpain.SelectedItem.Value = "" Then
                    lblmedsectionII.Text = "response on chest pain required!"
                    rdochestpain.Focus()
                    Exit Sub

                End If

                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Medical_Data_SectionII_Update", 0, lblApplicantID.Text, rdoheartcomplaint.SelectedItem.Value, _
                                          rdorheumatic.SelectedItem.Value, rdochestpain.SelectedItem.Value, rdohbp.SelectedItem.Value, rdoshortbreathe.SelectedItem.Value, rdopacemaker.SelectedItem.Value, _
                                          rdohepatitis.SelectedItem.Value, rdohivaids.SelectedItem.Value, rdodiabetes.SelectedItem.Value, rdothyroid.SelectedItem.Value, rdotuberculosis.SelectedItem.Value, _
                                          rdoepilepsy.SelectedItem.Value, rdofullyfit.SelectedItem.Value, rdofainting.SelectedItem.Value, rdoblackouts.SelectedItem.Value, rdoemphysema.SelectedItem.Value, _
                                          rdowheeziness.SelectedItem.Value, rdopneumonia.SelectedItem.Value, rdoasthma.SelectedItem.Value, rdorespiratorydisease.SelectedItem.Value, rdonervousdisorder.SelectedItem.Value, rdoheadache.SelectedItem.Value, _
                                          rdoeyedisease.SelectedItem.Value, rdodermatitis.SelectedItem.Value, rdoanaesthetic.SelectedItem.Value, rdoblooddisease.SelectedItem.Value, rdoallergymedication.SelectedItem.Value, _
                                          rdogastriculcer.SelectedItem.Value, rdogout.SelectedItem.Value, rdoarthritis.SelectedItem.Value, rdobowel.SelectedItem.Value, rdogallbladder.SelectedItem.Value, rdohipknee.SelectedItem.Value, _
                                          rdobackinjury.SelectedItem.Value, rdospinalinjury.SelectedItem.Value, rdoneckinjury.SelectedItem.Value, rdobonefracture.SelectedItem.Value, rdomentalillness.SelectedItem.Value, _
                                          rdoparalysis.SelectedItem.Value, rdosmoke.SelectedItem.Value, rdohospitalised.SelectedItem.Value, rdoworkercompensation.SelectedItem.Value, txtbodyinjured.Text, datInjurydate.SelectedDate, _
                                          rdolumpsettlement.SelectedItem.Value, rdomedicalclearance.SelectedItem.Value, txtAdditions.Text)

                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Section II: Medical Information accepted" + "')", True)

                MultiView1.ActiveViewIndex = 5
                datDate.SelectedDate = Date.Now
            End If
            
        Catch ex As Exception
            lblmedsectionII.Text = ex.Message
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            Session.Clear()
            Session.Abandon()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnSaveSectionII0_Click(sender As Object, e As EventArgs) Handles btnSaveSectionII0.Click
        Try
            If txtName.Text.Trim = "" Then
                Label66.Text = "Name required!"
                txtName.Focus()
                Exit Sub
            End If

            If txtAddress.Text.Trim = "" Then
                Label66.Text = "Address required!"
                txtAddress.Focus()
                Exit Sub
            End If

            If txtSign.Text.Trim = "" Then
                Label66.Text = "Signature required!"
                txtSign.Focus()
                Exit Sub
            End If

            If txtSign.Text.Trim = "" Then
                Label66.Text = "Signature required!"
                txtSign.Focus()
                Exit Sub
            End If

            If datDate.SelectedDate Is Nothing Then
                Label66.Text = "Date required!"
                datDate.Focus()
                Exit Sub
            End If
            Process.Candidate_Medical_Account_Complete("Medical", lblcompany.Text, lblname.Text, lbljobpost.Text, Process.GetMailLink(AuthenCode, 4))
            MultiView1.ActiveViewIndex = 6
            lbldeclare.Text = "Medical Declaration has been successfully submitted"
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub rdoQuestion2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rdoQuestion2.SelectedIndexChanged
        Try
            If rdoQuestion2.SelectedItem.Value.ToUpper = "NO" Then
                txtQuestionNote2.Enabled = False
            Else
                txtQuestionNote2.Enabled = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub rdoQuestion3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rdoQuestion3.SelectedIndexChanged
        Try
            If rdoQuestion3.SelectedItem.Value.ToUpper = "NO" Then
                txtQuestNote3.Enabled = False
            Else
                txtQuestNote3.Enabled = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub rdoQuestion4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rdoQuestion4.SelectedIndexChanged
        Try
            If rdoQuestion4.SelectedItem.Value.ToUpper = "NO" Then
                txtQuestNote4.Enabled = False
            Else
                txtQuestNote4.Enabled = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub rdoworkercompensation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rdoworkercompensation.SelectedIndexChanged
        Try
            If rdoworkercompensation.SelectedItem.Value.ToUpper = "YES" Then
                txtAdditions.Enabled = True
                datInjurydate.Enabled = True
                txtbodyinjured.Enabled = True
                rdomedicalclearance.Enabled = True
                rdolumpsettlement.Enabled = True
            Else
                txtAdditions.Enabled = False
                datInjurydate.Enabled = False
                txtbodyinjured.Enabled = False
                rdomedicalclearance.Enabled = False
                rdolumpsettlement.Enabled = False

                Process.RadioListCheck(rdomedicalclearance, "No")
                Process.RadioListCheck(rdolumpsettlement, "No")
            End If
        Catch ex As Exception
            lblmedsectionII.Text = ex.Message
        End Try
    End Sub

    Protected Sub Button1_Click2(sender As Object, e As EventArgs) Handles Button1.Click
        Try

            If txtnhfnumber.Text.Trim = "" Then
                lblnhfstatus.Text = "NHF Number required!"
                lblnhfstatus.Focus()
                Exit Sub
            End If

            If txtnhfname.Text.Trim = "" Then
                lblnhfstatus.Text = "NHF Account Name required!"
                txtnhfname.Focus()
                Exit Sub
            End If

            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Payroll_NHF_Update", lblApplicantID.Text, txtNHFApplicant.Text, txtnhfnumber.Text.ToUpper, txtnhfname.Text)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Your NHF Account Detail have been accepted" + "')", True)
            MultiView1.ActiveViewIndex = 6

            Process.Candidate_Medical_Account_Complete("Account", lblcompany.Text, lblname.Text, lbljobpost.Text, Process.ApplicationURL + "/" + Process.GetMailLink(AuthenCode, 4))
            lbldeclare.Text = "Account Details has been successfully submitted"
        Catch ex As Exception
            lblnhfstatus.Text = ex.Message
        End Try
    End Sub

    Protected Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        MultiView1.ActiveViewIndex = 6
        lbldeclare.Text = "Information supplied have been submitted"
    End Sub
End Class