Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Telerik.Web.UI


Public Class LeaveRuleUpdate
    Inherits System.Web.UI.Page
    Dim leaverule As New clsLeaveRule
    Dim AuthenCode As String = "LEAVERULE"
    Dim olddata(7) As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then

                Process.LoadRadDropDownTextAndValue(radLeaveType, "Leave_Type_get_all", "Name", "Name", False)
                Process.LoadRadComboTextAndValue(cboJobGrade, "Job_Grade_get_all", "Name", "Name", False)
                Process.LoadRadDropDownTextAndValue(radEmpStatus, "employment_status_get_all", "Name", "Name", False)

                radLeaveAccruedEnabled.Items.Clear()
                radLeaveAccruedEnabled.Items.Add("No")
                radLeaveAccruedEnabled.Items.Add("Yes")

                radLeaveCarriedEnabled.Items.Clear()
                radLeaveCarriedEnabled.Items.Add("No")
                radLeaveCarriedEnabled.Items.Add("Yes")

                radAvailabilityPeriod.Items.Clear()
                radAvailabilityPeriod.Items.Add("0")
                radAvailabilityPeriod.Items.Add("12")
                radAvailabilityPeriod.Items.Add("24")
                radAvailabilityPeriod.Items.Add("36")

                'Holidays_get_parent
                If Request.QueryString("id") IsNot Nothing Then
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Leave_Rule_get", Request.QueryString("id"))
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    aname.Value = strUser.Tables(0).Rows(0).Item("rulename").ToString
                    Process.AssignRadDropDownValue(radLeaveType, strUser.Tables(0).Rows(0).Item("LeaveType").ToString)
                    'Process.AssignRadComboValue(cboJobGrade, strUser.Tables(0).Rows(0).Item("Grade").ToString)
                    Process.AssignRadDropDownValue(radEmpStatus, strUser.Tables(0).Rows(0).Item("EmploymentStatus").ToString)
                    leavePerYear.Value = strUser.Tables(0).Rows(0).Item("LeavePerYear").ToString
                    minservice.Value = strUser.Tables(0).Rows(0).Item("minmthservice").ToString
                    maxservice.Value = strUser.Tables(0).Rows(0).Item("maxmthservice").ToString
                    Process.LoadListAndComboxFromDataset(lstMakeup, cboJobGrade, "leave_Rule_JobGrade_Get_All", "jobgrade", "jobgrade", txtid.Text)
                    Process.AssignRadDropDownValue(radLeaveCarriedEnabled, strUser.Tables(0).Rows(0).Item("LeaveCarriedForward").ToString)
                    txtPercent.Value = strUser.Tables(0).Rows(0).Item("PercentageCarriedForward").ToString
                    Process.AssignRadDropDownValue(radAvailabilityPeriod, strUser.Tables(0).Rows(0).Item("CarriedForwardLeaveAvailabilityPeriod").ToString)
                    Process.AssignRadDropDownValue(radLeaveAccruedEnabled, strUser.Tables(0).Rows(0).Item("LeaveAccruedEnabled").ToString)
                Else
                    txtid.Text = "0"
                    minservice.Value = "0"
                    maxservice.Value = "0"
                    txtPercent.Value = "0"
                End If

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Function GetIdentity(ByVal leavetype As String, ByVal empstatus As String, ByVal leaveperyear As Integer) As String
        Try
            If radAvailabilityPeriod.SelectedText = "" Then
                radAvailabilityPeriod.SelectedText = "0"
            End If
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Leave_Rule_update"
            cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = txtid.Text
            cmd.Parameters.Add("@leavetype", SqlDbType.VarChar).Value = leavetype
            cmd.Parameters.Add("@EmpStatus", SqlDbType.VarChar).Value = empstatus
            cmd.Parameters.Add("@leavesperyear", SqlDbType.Int).Value = leaveperyear
            cmd.Parameters.Add("@minmthservice", SqlDbType.Int).Value = minservice.Value
            cmd.Parameters.Add("@maxmthservice", SqlDbType.Int).Value = maxservice.Value
            cmd.Parameters.Add("@rulename", SqlDbType.VarChar).Value = aname.Value.Trim
            cmd.Parameters.Add("@LeaveCarriedForward", SqlDbType.VarChar).Value = radLeaveCarriedEnabled.SelectedText
            cmd.Parameters.Add("@PercentageCarriedForward", SqlDbType.Int).Value = txtPercent.Value
            cmd.Parameters.Add("@CarriedForwardLeaveAvailabilityPeriod", SqlDbType.Int).Value = radAvailabilityPeriod.SelectedText 'LeaveAccruedEnabled
            cmd.Parameters.Add("@LeaveAccruedEnabled", SqlDbType.VarChar).Value = radLeaveAccruedEnabled.SelectedText
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
            Return 0
        End Try
    End Function

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try

            Dim sid As Integer = txtid.Text
            If txtid.Text <> "0" Then
                If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False Then
                    ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
                    Exit Sub
                End If
            End If


            Dim lblstatus As String = ""
            If (aname.Value.Trim = "") Then
                lblstatus = "Name required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                aname.Focus()
                Exit Sub
            End If

            Dim collection As IList(Of RadComboBoxItem) = cboJobGrade.CheckedItems
            If (collection.Count <= 0) Then
                lblstatus = "job grades required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                cboJobGrade.Focus()
                Exit Sub
            End If

            If (IsNumeric(leavePerYear.Value) = False) Then
                lblstatus = "Leave entitlement required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                leavePerYear.Focus()
                Exit Sub
            End If

            If (IsNumeric(txtPercent.Value) = False) Then
                lblstatus = "Percentage of leave days carried forward required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                txtPercent.Focus()
                Exit Sub
            End If

            If (radEmpStatus.SelectedText.Trim = "" Or radEmpStatus.SelectedText.Trim = "-- Select --") Then
                lblstatus = "Employment Status required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                radEmpStatus.Focus()
                Exit Sub
            End If

            If (radLeaveType.SelectedText.Trim = "" Or radLeaveType.SelectedText.Trim = "-- Select --") Then
                lblstatus = "Leave Type required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                radLeaveType.Focus()
                Exit Sub
            End If

            If IsNumeric(minservice.Value) = False Then
                lblstatus = "Minimum eligible month of service required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                minservice.Focus()
                Exit Sub
            End If

            If IsNumeric(maxservice.Value) = False Then
                lblstatus = "Maximum eligible month of service required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                maxservice.Focus()
                Exit Sub
            End If

            If CInt(minservice.Value) > CInt(maxservice.Value) Then
                lblstatus = "invalid minimum or maximum eligible month of service!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                maxservice.Focus()
                Exit Sub
            End If




            If txtid.Text.Trim = "" Then
                leaverule.id = 0
            Else
                leaverule.id = txtid.Text
            End If

            leaverule.EmploymentStatus = radEmpStatus.SelectedText
            leaverule.LeaveType = radLeaveType.SelectedText
            leaverule.LeavePerYear = leavePerYear.Value
            leaverule.MinMthService = minservice.Value
            leaverule.MaxMthService = maxservice.Value

            Dim OldValue As String = ""
            Dim NewValue As String = ""


            If txtid.Text = "0" Then
                txtid.Text = GetIdentity(leaverule.LeaveType, leaverule.EmploymentStatus, leaverule.LeavePerYear)
                If txtid.Text = "0" Then
                    Exit Sub
                End If
            Else
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Leave_Rule_update", txtid.Text, leaverule.LeaveType, leaverule.EmploymentStatus, leaverule.LeavePerYear, minservice.Value, maxservice.Value, aname.Value.Trim, radLeaveCarriedEnabled.SelectedText, txtPercent.Value, radAvailabilityPeriod.SelectedText, radLeaveAccruedEnabled.SelectedText)
            End If


            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "leave_Rule_JobGrade_Delete", txtid.Text)
            For d As Integer = 0 To lstMakeup.Items.Count - 1
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "leave_Rule_JobGrade_Update", txtid.Text, lstMakeup.Items(d).Text)
            Next

            If NewValue.Trim = "" And OldValue.Trim = "" Then
            Else
                If sid <> 0 Then
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Updated " & aname.Value, "Leave Rules")
                Else
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Inserted", "Leave Rules")
                End If
            End If
            lblstatus = "Record saved"
            Process.loadalert(divalert, msgalert, lblstatus, "success")

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect("~/Module/Admin/leaverules", True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Private Sub cboJobGrade_CheckAllCheck(sender As Object, e As Telerik.Web.UI.RadComboBoxCheckAllCheckEventArgs) Handles cboJobGrade.CheckAllCheck
        Try
            Process.LoadListBoxFromCombo(lstMakeup, cboJobGrade)
        Catch ex As Exception

        End Try
    End Sub



    Private Sub cboJobGrade_ItemChecked(sender As Object, e As Telerik.Web.UI.RadComboBoxItemEventArgs) Handles cboJobGrade.ItemChecked
        Try
            Process.LoadListBoxFromCombo(lstMakeup, cboJobGrade)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub radLeaveCarriedEnabled_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.DropDownListEventArgs) Handles radLeaveCarriedEnabled.SelectedIndexChanged
        Try
            If radLeaveCarriedEnabled.SelectedItem.Text.ToUpper = "NO" Then
                If IsNumeric(txtPercent.Value) = False Then
                    txtPercent.Value = "0"
                End If
                radAvailabilityPeriod.Enabled = False
                txtPercent.Disabled = True
            Else
                radAvailabilityPeriod.Enabled = True
                txtPercent.Disabled = False
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
End Class