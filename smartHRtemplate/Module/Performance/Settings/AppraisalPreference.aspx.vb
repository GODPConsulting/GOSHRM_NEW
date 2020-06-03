Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Telerik.Web.UI

Public Class AppraisalPreference
    Inherits System.Web.UI.Page
    Dim appoption As New clsAppraisalOption
    Dim AuthenCode As String = "APPRAISALSETUP"
    Dim olddata(6) As String
    Dim oFileAttached1 As Byte
    Dim oFileAttached As Byte()
    Dim lblstatus As String = ""
    Private Sub LoadControls(cc As String)
        Try
            lblstatus = ""
            Dim strUser As New DataSet
           

            strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Appraisal_Preference_Get", cc)
            Process.LoadRadComboTextAndValueInitiateP1(cboPeriod, "Performance_Appraisal_Cycle_Get_All", cc, "--select--", "period", "period")
            If strUser.Tables(0).Rows.Count > 0 Then
                Process.AssignRadComboValue(cboPeriod, strUser.Tables(0).Rows(0).Item("AppraisalCycle").ToString)
                Process.AssignRadComboValue(cboCompany, strUser.Tables(0).Rows(0).Item("company").ToString)
                Process.AssignRadComboValue(cboReviewerVisibility, strUser.Tables(0).Rows(0).Item("reviewervisible").ToString)
                Process.AssignRadComboValue(cboReviewIIVisibility, strUser.Tables(0).Rows(0).Item("RevieweriiVisible").ToString)
                Process.AssignRadComboValue(cboStat, strUser.Tables(0).Rows(0).Item("locked").ToString)
                Process.AssignRadComboValue(cboViewCoach, strUser.Tables(0).Rows(0).Item("ReviewerObjVisible").ToString)
            Else
                Process.AssignRadComboValue(cboPeriod, "")
                Process.AssignRadComboValue(cboCompany, cc)
                lblstatus = "No appraisal preference setting for " & cc
                Process.loadalert(divalert, msgalert, lblstatus, "danger")

            End If
            pagetitle.innertext = cc & " Appraisal Perference"
        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "danger")
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                cboReviewerVisibility.Items.Clear()
                cboReviewerVisibility.Items.Add("No")
                cboReviewerVisibility.Items.Add("Yes")

                cboReviewIIVisibility.Items.Clear()
                cboReviewIIVisibility.Items.Add("No")
                cboReviewIIVisibility.Items.Add("Yes")

                cboStat.Items.Clear()
                cboStat.Items.Add("Locked")
                cboStat.Items.Add("Open")

                cboViewCoach.Items.Clear()
                cboViewCoach.Items.Add("No")
                cboViewCoach.Items.Add("Yes")
                Dim ismulti As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select isnull(ismulticompany,'No')  from general_info")

                If ismulti.ToLower = "no" Then
                    Process.LoadRadComboTextAndValueP2(cboCompany, "Company_Structure_Get_ByLevel", "1", Session("Access"), "name", "name", False)
                Else
                    Process.LoadRadComboTextAndValueP2(cboCompany, "Company_Structure_Get_ByLevel", "2", Session("Access"), "name", "name", False)
                End If

                LoadControls(Session("Organisation"))
            End If
        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "danger")
        End Try
    End Sub



    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            lblstatus = ""
            If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "danger")
                Exit Sub
            End If

            If cboPeriod.SelectedItem.Text.Contains("--select--") Then
                lblstatus = cboCompany.SelectedValue & " Current Appraisal Cycle required!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                Exit Sub
            End If


            'Old Data
            appoption.AppraisalCycle = cboPeriod.SelectedValue
            appoption.LockCurrentCycle = cboStat.SelectedItem.Text
            appoption.ReviewerCommentVisible = cboReviewerVisibility.SelectedItem.Text
            appoption.Company = cboCompany.SelectedValue
            Dim strUser As New DataSet
            strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Appraisal_Preference_Get", cboCompany.SelectedValue)
            Dim ccount As Integer = strUser.Tables(0).Rows.Count
            If strUser.Tables(0).Rows.Count > 0 Then
                olddata(0) = strUser.Tables(0).Rows(0).Item("AppraisalCycle").ToString
                olddata(1) = strUser.Tables(0).Rows(0).Item("reviewervisible").ToString 'approvers 
                olddata(2) = strUser.Tables(0).Rows(0).Item("locked").ToString
                olddata(3) = strUser.Tables(0).Rows(0).Item("company").ToString
            End If

            Dim NewValue As String = ""
            Dim OldValue As String = ""

            Dim j As Integer = 0

            If olddata(0) IsNot Nothing Then
                For Each a In GetType(clsAppraisalOption).GetProperties()
                    If a.Name.ToLower <> "id" And a.Name.ToLower.Contains("password") = False Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If IsNumeric(a.GetValue(appoption, Nothing)) = True And IsNumeric(olddata(j)) = True Then
                                If CDbl(a.GetValue(appoption, Nothing)) <> CDbl(olddata(j)) Then
                                    NewValue += a.Name + ": " + a.GetValue(appoption, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            Else
                                If a.GetValue(appoption, Nothing).ToString <> olddata(j).ToString Then
                                    NewValue += a.Name + ": " + a.GetValue(appoption, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            End If
                        End If
                    End If
                    j = j + 1
                Next
            Else
                For Each a In GetType(clsAppraisalOption).GetProperties() 'New Entries
                    If a.Name.ToLower <> "id" And a.Name.ToLower.Contains("password") Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If a.GetValue(appoption, Nothing) = Nothing Then
                                NewValue += a.Name + ":" + " " & vbCrLf
                            Else
                                NewValue += a.Name + ": " + a.GetValue(appoption, Nothing).ToString & vbCrLf
                            End If
                        End If
                    End If
                Next
            End If

            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_Preference_Update", appoption.AppraisalCycle, 0, 0, appoption.LockCurrentCycle, appoption.ReviewerCommentVisible, cboReviewIIVisibility.SelectedItem.Text, cboViewCoach.SelectedItem.Text, Session("LoginID"), appoption.Company)

            If NewValue.Trim = "" And OldValue.Trim = "" Then
            Else
                If olddata(0) IsNot Nothing Then
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Updated", "Appraisal Preference")
                Else
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Inserted", "Appraisal Preference")
                End If
            End If

            'Response.Redirect("~/Module/Performance/Settings/AppraisalPreference.aspx", False)
            lblstatus = cboCompany.SelectedValue & " appraisal preference record saved"
            Process.loadalert(divalert, msgalert, lblstatus, "success")
            LoadControls(cboCompany.SelectedValue)
        Catch ex As Exception
            lblstatus = ex.Message
        End Try
    End Sub

  


    Protected Sub cboCompany_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboCompany.SelectedIndexChanged
        Try
            LoadControls(cboCompany.SelectedValue)
            
        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "danger")
        End Try
    End Sub
End Class