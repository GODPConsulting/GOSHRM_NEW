Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports System.IO


Public Class ApplicantBioDataView
    Inherits System.Web.UI.Page
    Dim education As New clsEducation
    Dim Separators() As Char = {","c}
    Dim section As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'SingleLineTextBox, // will render a textbox 
            'MultiLineTextBox, // will render a text area
            'YesOrNo, //will render a checkbox
            'SingleSelect, //will render a dropdownlist
            'MultiSelect //will render a listbo

            If Not Me.IsPostBack Then
                MultiView1.ActiveViewIndex = 0
                If Request.QueryString("ApplicantID") IsNot Nothing Then
                    lblApplicantID.Text = Request.QueryString("ApplicantID")

                    Dim strApp As DataSet
                    strApp = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "select b.applicant from Recruit_Applications a inner join Recruit_Applicants b on a.applicantid = b.id where a.id =" & Request.QueryString("ApplicantID"))
                    lblApplicantName.Text = strApp.Tables(0).Rows(0).Item("applicant").ToString

                    strApp = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Payroll_Accounts_Get", Request.QueryString("ApplicantID"))
                    If strApp.Tables(0).Rows.Count > 0 Then
                        lblBank.Text = strApp.Tables(0).Rows(0).Item("Bank").ToString
                        lblAccountNumber.Text = strApp.Tables(0).Rows(0).Item("AccountNumber").ToString
                    End If
                  

                    strApp = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Pension_Get", Request.QueryString("ApplicantID"))
                    If strApp.Tables(0).Rows.Count > 0 Then
                        lblPFA.Text = strApp.Tables(0).Rows(0).Item("PensionManager").ToString
                        lblRSA.Text = strApp.Tables(0).Rows(0).Item("RSACode").ToString
                    End If

                    strApp = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Payroll_NHF_Get", Request.QueryString("ApplicantID"))
                    If strApp.Tables(0).Rows.Count > 0 Then
                        lblNHFNo.Text = strApp.Tables(0).Rows(0).Item("NHSNumber").ToString
                        lblNHFName.Text = strApp.Tables(0).Rows(0).Item("Accountname").ToString
                    End If

                    strApp = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Medical_Data_SectionI_Get", Request.QueryString("ApplicantID"))
                    If strApp.Tables(0).Rows.Count > 0 Then
                        Process.RadioListCheck(rdoQuestion1, strApp.Tables(0).Rows(0).Item("Q1Option1").ToString)
                        lblQuestNote1.Text = strApp.Tables(0).Rows(0).Item("Q1Text").ToString
                        Process.RadioListCheck(rdoQuestion2, strApp.Tables(0).Rows(0).Item("Q2Option1").ToString)
                        lblQuestNote2.Text = strApp.Tables(0).Rows(0).Item("Q2Text").ToString
                        Process.RadioListCheck(rdoQuestion3, strApp.Tables(0).Rows(0).Item("Q3Option1").ToString)
                        lblQuestNote3.Text = strApp.Tables(0).Rows(0).Item("Q3Text").ToString
                        Process.RadioListCheck(rdoQuestion4, strApp.Tables(0).Rows(0).Item("Q4Option1").ToString)
                        lblQuestNote4.Text = strApp.Tables(0).Rows(0).Item("Q4Text").ToString
                    End If

                    strApp = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Medical_Data_SectionII_Get", Request.QueryString("ApplicantID"))
                    If strApp.Tables(0).Rows.Count > 0 Then
                        rdoheartcomplaint.SelectedValue = strApp.Tables(0).Rows(0).Item("heart").ToString
                        rdorheumatic.SelectedValue = strApp.Tables(0).Rows(0).Item("rheumatic").ToString
                        rdochestpain.SelectedValue = strApp.Tables(0).Rows(0).Item("chestpain").ToString
                        rdohbp.SelectedValue = strApp.Tables(0).Rows(0).Item("hbp").ToString
                        rdoshortbreathe.SelectedValue = strApp.Tables(0).Rows(0).Item("shortbreathe").ToString
                        rdopacemaker.SelectedValue = strApp.Tables(0).Rows(0).Item("artificialvalve").ToString
                        rdohepatitis.SelectedValue = strApp.Tables(0).Rows(0).Item("hepatitis").ToString
                        rdohivaids.SelectedValue = strApp.Tables(0).Rows(0).Item("hiv").ToString

                        rdodiabetes.SelectedValue = strApp.Tables(0).Rows(0).Item("diabetes").ToString
                        rdothyroid.SelectedValue = strApp.Tables(0).Rows(0).Item("thyroid").ToString
                        rdotuberculosis.SelectedValue = strApp.Tables(0).Rows(0).Item("tb").ToString
                        rdoepilepsy.SelectedValue = strApp.Tables(0).Rows(0).Item("epilepsy").ToString
                        rdofullyfit.SelectedValue = strApp.Tables(0).Rows(0).Item("fullyfit").ToString
                        rdofainting.SelectedValue = strApp.Tables(0).Rows(0).Item("fainting").ToString
                        rdoblackouts.SelectedValue = strApp.Tables(0).Rows(0).Item("blackout").ToString
                        rdoemphysema.SelectedValue = strApp.Tables(0).Rows(0).Item("emphysema").ToString

                        rdowheeziness.SelectedValue = strApp.Tables(0).Rows(0).Item("diabetes").ToString
                        rdopneumonia.SelectedValue = strApp.Tables(0).Rows(0).Item("pneumonia").ToString
                        rdoasthma.SelectedValue = strApp.Tables(0).Rows(0).Item("asthma").ToString
                        rdorespiratorydisease.SelectedValue = strApp.Tables(0).Rows(0).Item("otherrespiratorydiseases").ToString
                        rdonervousdisorder.SelectedValue = strApp.Tables(0).Rows(0).Item("nervousdisorder").ToString
                        rdoheadache.SelectedValue = strApp.Tables(0).Rows(0).Item("persistentheadaches").ToString
                        rdoeyedisease.SelectedValue = strApp.Tables(0).Rows(0).Item("eyediseases").ToString
                        rdodermatitis.SelectedValue = strApp.Tables(0).Rows(0).Item("skindiseases").ToString

                        rdoanaesthetic.SelectedValue = strApp.Tables(0).Rows(0).Item("allergytopenicillin").ToString
                        rdoblooddisease.SelectedValue = strApp.Tables(0).Rows(0).Item("bleeding").ToString
                        rdoallergymedication.SelectedValue = strApp.Tables(0).Rows(0).Item("allergytomedic").ToString
                        rdogastriculcer.SelectedValue = strApp.Tables(0).Rows(0).Item("gastriculcer").ToString
                        rdogout.SelectedValue = strApp.Tables(0).Rows(0).Item("gout").ToString
                        rdoarthritis.SelectedValue = strApp.Tables(0).Rows(0).Item("arthritis").ToString
                        rdobowel.SelectedValue = strApp.Tables(0).Rows(0).Item("boweldisease").ToString
                        rdogallbladder.SelectedValue = strApp.Tables(0).Rows(0).Item("gallbladder").ToString

                        rdohipknee.SelectedValue = strApp.Tables(0).Rows(0).Item("replacements").ToString
                        rdobackinjury.SelectedValue = strApp.Tables(0).Rows(0).Item("backinjury").ToString
                        rdospinalinjury.SelectedValue = strApp.Tables(0).Rows(0).Item("spinalinjury").ToString
                        rdoneckinjury.SelectedValue = strApp.Tables(0).Rows(0).Item("neckinjury").ToString
                        rdobonefracture.SelectedValue = strApp.Tables(0).Rows(0).Item("bonefractures").ToString
                        rdomentalillness.SelectedValue = strApp.Tables(0).Rows(0).Item("mentalillness").ToString
                        rdoparalysis.SelectedValue = strApp.Tables(0).Rows(0).Item("paralysis").ToString
                        rdosmoke.SelectedValue = strApp.Tables(0).Rows(0).Item("smoker").ToString
                        rdohospitalised.SelectedValue = strApp.Tables(0).Rows(0).Item("hosiptalisedbefore").ToString

                        Process.RadioListCheck(rdoworkercompensation, strApp.Tables(0).Rows(0).Item("workcompensation").ToString)
                        txtbodyinjured.Text = strApp.Tables(0).Rows(0).Item("partofbodyinjury").ToString
                        txtinjurydate.Text = strApp.Tables(0).Rows(0).Item("dateofinjury").ToString
                        Process.RadioListCheck(rdolumpsettlement, strApp.Tables(0).Rows(0).Item("lumpsumsettlement").ToString)
                        Process.RadioListCheck(rdomedicalclearance, strApp.Tables(0).Rows(0).Item("finalmedicalclearance").ToString)
                        txtAdditions.Text = strApp.Tables(0).Rows(0).Item("AdditionalInfo").ToString
                    End If

                End If
            End If
        Catch ex As Exception
            lblbankstatus.Text = ex.Message
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & lblbankstatus.Text + "')", True)
        End Try
    End Sub

   

    

    Protected Sub btnMedSaveSection1_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        Try

            MultiView1.ActiveViewIndex = 1
        Catch ex As Exception
            lblbankstatus.Text = ex.Message
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & lblbankstatus.Text + "')", True)
        End Try
    End Sub

   

    Protected Sub Button1_Click1(sender As Object, e As EventArgs) Handles btnPrevious.Click
        Try

            MultiView1.ActiveViewIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles btnClose2.Click
        Try
           Response.Write("<script language='javascript'> { self.close() }</script>")
        Catch ex As Exception

        End Try
    End Sub

   
    Protected Sub btnClose1_Click(sender As Object, e As EventArgs) Handles btnClose1.Click
        Try
            Response.Write("<script language='javascript'> { self.close() }</script>")
        Catch ex As Exception

        End Try
    End Sub
End Class