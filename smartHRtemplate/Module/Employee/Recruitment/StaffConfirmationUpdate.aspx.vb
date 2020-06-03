Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO


Public Class StaffConfirmationUpdate
    Inherits System.Web.UI.Page
    Dim exits As New clsExit
    Dim AuthenCode As String = "EMPCONFIRMATION"
    Dim olddata(3) As String
    Dim lblstatus As String
    Private Sub FreshConfirmation()
        Try
            If Process.view = "confirmed" Then
                lblstatus = "Confirmation detail not available, process done out of system."
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
            Else
                Process.LoadRadComboTextAndValueInitiateP1(cboEmployee, "Emp_PersonalDetail_Get_MyTeam_NotConfirmed", Session("UserEmpID"), "--Select--", "Employee2", "EmpID")
                txtid.Text = "0"
                lblhrcomment.Visible = False
                lblHRCommentID.Visible = False
                lblHRManager.Visible = False
                lblHRManagerID.Visible = False
                lblHRRecommendation.Visible = False
                lblHRRecommendationID.Visible = False
                Label6.Visible = False
                Process.AssignRadDropDownValue(radRecommendation, "Pending")
                btnComplete.Visible = False
                If Request.QueryString("empid") IsNot Nothing Then
                    Process.AssignRadComboValue(cboEmployee, Request.QueryString("empid"))
                    cboEmployee.Enabled = False
                    Dim strEmp As New DataSet
                    strEmp = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_PersonalDetail_get", cboEmployee.SelectedValue)
                    If strEmp.Tables(0).Rows.Count > 0 Then
                        lblDateJoin.Text = CDate(strEmp.Tables(0).Rows(0).Item("datejoin")).ToLongDateString
                        txtProbation.Text = strEmp.Tables(0).Rows(0).Item("ProbationPeriod").ToString
                        lbloffice.Text = strEmp.Tables(0).Rows(0).Item("Office").ToString
                    Else
                        lblDateJoin.Text = ""
                        txtProbation.Text = "0"
                    End If
                End If
            End If

            
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then

                'Company_Structure_get_parent
                If Request.QueryString("id") IsNot Nothing And Request.QueryString("id") <> "0" Then
                    Process.LoadRadComboTextAndValueInitiateP1(cboEmployee, "Emp_PersonalDetail_Get_Employees", Session("Access"), "--Select--", "Employee2", "EmpID")
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Confirmation_Get", Request.QueryString("id"))
                    If strUser.Tables(0).Rows.Count > 0 Then
                        txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                        Process.AssignRadComboValue(cboEmployee, strUser.Tables(0).Rows(0).Item("recruitid").ToString)
                        lbloffice.Text = strUser.Tables(0).Rows(0).Item("office").ToString
                        txtProbation.Text = strUser.Tables(0).Rows(0).Item("probation").ToString
                        lblDateJoin.Text = CDate(strUser.Tables(0).Rows(0).Item("datejoin")).ToLongDateString
                        txtTargetAchieved.Value = strUser.Tables(0).Rows(0).Item("targetsachieved").ToString
                        txtAreaOfDev.Value = strUser.Tables(0).Rows(0).Item("areaofdevelopment").ToString
                        txtComment.Value = strUser.Tables(0).Rows(0).Item("targetcomments").ToString

                        Process.AssignRadDropDownValue(radRecommendation, strUser.Tables(0).Rows(0).Item("recommendation").ToString)
                        txtProbationExtension.Text = strUser.Tables(0).Rows(0).Item("probationextension").ToString
                        lblCompleteStat.Text = strUser.Tables(0).Rows(0).Item("completestat").ToString
                        lblhrcomment.Text = strUser.Tables(0).Rows(0).Item("hrcomment").ToString.Replace(vbCrLf, "<br/>")
                        lblHRManager.Text = strUser.Tables(0).Rows(0).Item("HRManagerName").ToString
                        lblHRRecommendation.Text = strUser.Tables(0).Rows(0).Item("hrrecommendation").ToString
                        If radRecommendation.SelectedValue.ToUpper.Contains("EXTEND") = True Then
                            lblExtendProbationID.Visible = True
                            txtProbationExtension.Visible = True
                        Else
                            lblExtendProbationID.Visible = False
                            txtProbationExtension.Visible = False
                            txtProbationExtension.Text = "0"
                        End If
                        If IsDBNull(strUser.Tables(0).Rows(0).Item("hrempid")) = True Or strUser.Tables(0).Rows(0).Item("hrempid").ToString = "" Then
                            lblhrcomment.Visible = False
                            lblHRCommentID.Visible = False
                            lblHRManager.Visible = False
                            lblHRManagerID.Visible = False
                            lblHRRecommendation.Visible = False
                            lblHRRecommendationID.Visible = False
                        End If
                        cboEmployee.Enabled = False

                        RadRating1.Value = CDbl(strUser.Tables(0).Rows(0).Item("rating"))
                        Dim strPoint As New DataSet
                        strPoint = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Points_Get_Point", RadRating1.Value)
                        If strPoint.Tables(0).Rows.Count > 0 Then
                            lblRating.InnerText = strPoint.Tables(0).Rows(0).Item("PointName").ToString
                        End If
                    Else
                        FreshConfirmation()
                    End If

                Else
                    FreshConfirmation()
                End If

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            If cboEmployee.SelectedValue Is Nothing Then
                lblstatus = "Select Employee to confirm!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                cboEmployee.Focus()
                Exit Sub
            End If

            If txtTargetAchieved.Value.Trim = "" Then
                lblstatus = "Provide target achieved to make employee confirmation easy!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                txtTargetAchieved.Focus()
                Exit Sub
            End If

            If txtid.Text = "0" Then
                txtid.Text = GetIdentity(Session("UserEmpID"), cboEmployee.SelectedValue, txtProbation.Text.Trim, lbloffice.Text, RadRating1.Value, radRecommendation.SelectedValue, txtComment.Value, txtTargetAchieved.Value, txtAreaOfDev.Value, txtProbationExtension.Text, Session("LoginID"))
                If txtid.Text = "0" Then
                    lblstatus = Process.strExp
                    Process.loadalert(divalert, msgalert, lblstatus, "danger")
                    Exit Sub
                End If
            Else
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Confirmation_Update", txtid.Text, Session("UserEmpID"), cboEmployee.SelectedValue, txtProbation.Text.Trim, lbloffice.Text, RadRating1.Value, radRecommendation.SelectedValue, txtComment.Value, txtTargetAchieved.Value, txtAreaOfDev.Value, txtProbationExtension.Text, Session("LoginID"), "")
            End If
            lblstatus = "Record saved!"
            Process.loadalert(divalert, msgalert, lblstatus, "success")
            If radRecommendation.SelectedValue = "Pending" Then
                btnComplete.Visible = False
            Else
                btnComplete.Visible = True
            End If

        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "danger")
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect("StaffConfirmation")
        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "danger")
        End Try
    End Sub


    Protected Sub cboEmployee_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboEmployee.SelectedIndexChanged
        Try
            Dim strEmp As New DataSet
            strEmp = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_PersonalDetail_get", cboEmployee.SelectedValue)
            If strEmp.Tables(0).Rows.Count > 0 Then
                lblDateJoin.Text = CDate(strEmp.Tables(0).Rows(0).Item("datejoin")).ToLongDateString
                txtProbation.Text = strEmp.Tables(0).Rows(0).Item("ProbationPeriod").ToString
                lbloffice.Text = strEmp.Tables(0).Rows(0).Item("Office").ToString
            Else
                lblDateJoin.Text = ""
                txtProbation.Text = "0"
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub radRecommendation_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.DropDownListEventArgs) Handles radRecommendation.SelectedIndexChanged
        Try
            If radRecommendation.SelectedValue.ToUpper.Contains("EXTEND") = True Then
                lblExtendProbationID.Visible = True
                txtProbationExtension.Visible = True
            Else
                lblExtendProbationID.Visible = False
                txtProbationExtension.Visible = False
                txtProbationExtension.Text = "0"
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Function GetIdentity(ByVal empid As String, ByVal recruitid As String, ByVal probation As Integer, _
                                ByVal dept As String, ByVal ratings As Double, ByVal recommendation As String, _
                                 ByVal comments As String, ByVal targetachieved As String, _
                                 ByVal areaofdevelopment As String, ByVal probationext As Integer, ByVal strusers As String) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Recruit_Confirmation_Update"
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@empid", SqlDbType.VarChar).Value = empid
            cmd.Parameters.Add("@recruitid", SqlDbType.VarChar).Value = recruitid
            cmd.Parameters.Add("@probation", SqlDbType.Int).Value = probation
            cmd.Parameters.Add("@office", SqlDbType.VarChar).Value = dept
            cmd.Parameters.Add("@rating", SqlDbType.Decimal).Value = ratings
            cmd.Parameters.Add("@recommendation", SqlDbType.VarChar).Value = recommendation
            cmd.Parameters.Add("@targetcomments", SqlDbType.VarChar).Value = comments
            cmd.Parameters.Add("@targetsachieved", SqlDbType.VarChar).Value = targetachieved
            cmd.Parameters.Add("@areaofdevelopment", SqlDbType.VarChar).Value = areaofdevelopment
            cmd.Parameters.Add("@probationextension", SqlDbType.Int).Value = probationext
            cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = strusers
            cmd.Parameters.Add("@hr", SqlDbType.VarChar).Value = ""
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception
            Process.strExp = "Personal Info: " & ex.Message
            Return 0
        End Try
    End Function

    Protected Sub btnComplete_Click(sender As Object, e As EventArgs) Handles btnComplete.Click
        Try
            lblstatus = ""
            Dim msg As String = ""
            Dim confirmValue As String = "Yes"
            If confirmValue = "Yes" Then
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Confirmation_Mark", txtid.Text)
                Process.Staff_Confirmation_Notification(cboEmployee.SelectedValue, Session("UserEmpID"))
                Process.Staff_Confirmation_HR_Notification(cboEmployee.SelectedValue, Session("UserEmpID"), Process.ApplicationURL + "/" + Process.GetMailLink(AuthenCode, 1))
            End If
            lblCompleteStat.Text = "Complete"
            Label6.Visible = True
            lblstatus = "Marked as complete and notification sent to HR for final processing!"
            Process.loadalert(divalert, msgalert, lblstatus, "success")
        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "danger")
        End Try
    End Sub

    Protected Sub RadRating1_Rate(sender As Object, e As EventArgs) Handles RadRating1.Rate
        Try
            Dim strPoint As New DataSet
                 strPoint = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Points_Get_Point", RadRating1.Value)
            If strPoint.Tables(0).Rows.Count > 0 Then
                lblRating.InnerText = strPoint.Tables(0).Rows(0).Item("PointName").ToString
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
End Class