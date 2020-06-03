Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO


Public Class LeaveTypeUpdate
    Inherits System.Web.UI.Page
    Dim leavetypes As New clsLeaveType
    Dim AuthenCode As String = "LEAVETYPE"
    Dim olddata(12) As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then

                radEmpCanApply.Items.Clear()
                radEmpCanApply.Items.Add("No")
                radEmpCanApply.Items.Add("Yes")

                radProbation.Items.Clear()
                radProbation.Items.Add("No")
                radProbation.Items.Add("Yes")

              

                radLeaveAccruedEnabled.Items.Clear()
                radLeaveAccruedEnabled.Items.Add("No")
                radLeaveAccruedEnabled.Items.Add("Yes")

                radPayable.Items.Clear()
                radPayable.Items.Add("No")
                radPayable.Items.Add("Yes")

                radSpecific.Items.Clear()
                radSpecific.Items.Add("No")
                radSpecific.Items.Add("Yes")

                radLeaveCarriedEnabled.Items.Clear()
                radLeaveCarriedEnabled.Items.Add("No")
                radLeaveCarriedEnabled.Items.Add("Yes")

                radAvailabilityPeriod.Items.Clear()
                radAvailabilityPeriod.Items.Add("0")
                radAvailabilityPeriod.Items.Add("12")
                radAvailabilityPeriod.Items.Add("24")

                Process.LoadRadDropDownTextAndValue(radGender, "Gender_get_all", "sex", "sex", False)


                'Holidays_get_parent
                If Request.QueryString("id") IsNot Nothing Then
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Leave_Type_get", Request.QueryString("id"))
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    aname.Value = strUser.Tables(0).Rows(0).Item("Name").ToString
                    leavePerYear.Value = strUser.Tables(0).Rows(0).Item("LeavePerYear").ToString
                    Process.AssignRadDropDownValue(radSpecific, strUser.Tables(0).Rows(0).Item("specificduration").ToString)
                    Process.AssignRadDropDownValue(radEmpCanApply, strUser.Tables(0).Rows(0).Item("EmployeeCanApply").ToString)                    
                    Process.AssignRadDropDownValue(radLeaveAccruedEnabled, strUser.Tables(0).Rows(0).Item("LeaveAccruedEnabled").ToString)
                    Process.AssignRadDropDownValue(radLeaveCarriedEnabled, strUser.Tables(0).Rows(0).Item("LeaveCarriedForward").ToString)
                    txtPercent.Value = strUser.Tables(0).Rows(0).Item("PercentageCarriedForward").ToString
                    Process.AssignRadDropDownValue(radAvailabilityPeriod, strUser.Tables(0).Rows(0).Item("CarriedForwardLeaveAvailabilityPeriod").ToString)
                    Process.AssignRadDropDownValue(radGender, strUser.Tables(0).Rows(0).Item("gender").ToString)
                    Process.AssignRadDropDownValue(radProbation, strUser.Tables(0).Rows(0).Item("EligibleAtProbation").ToString)
                    Process.AssignRadDropDownValue(radPayable, strUser.Tables(0).Rows(0).Item("payable").ToString)
                   
                    If aname.Value = "Annual Leave" Or aname.Value = "Casual Leave" Then
                        aname.Disabled = True
                    Else
                        aname.Disabled = False
                    End If
                Else
                    txtid.Text = "0"
                End If

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Function GetIdentity(ByVal sname As String, ByVal leave As Integer, ByVal EmpCanApply As String, _
                               ByVal accrueable As String, ByVal carriedforward As String, ByVal percentagecarriedforward As Integer, _
                                ByVal availabilityperiod As Integer, ByVal gender As String, ByVal EligibleAtProbation As String, ByVal Payable As String, ByVal SpecificDays As String) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Leave_Type_update"
            cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = 0
            cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = sname
            cmd.Parameters.Add("@leave", SqlDbType.Int).Value = leave
            cmd.Parameters.Add("@empcanapply", SqlDbType.VarChar).Value = EmpCanApply
            cmd.Parameters.Add("@accrueable", SqlDbType.VarChar).Value = accrueable
            cmd.Parameters.Add("@carriedforward", SqlDbType.VarChar).Value = carriedforward
            cmd.Parameters.Add("@percentagecarriedforward", SqlDbType.Int).Value = percentagecarriedforward
            cmd.Parameters.Add("@availabilityperiod", SqlDbType.Int).Value = availabilityperiod
            cmd.Parameters.Add("@gender", SqlDbType.VarChar).Value = gender
            cmd.Parameters.Add("@EligibleAtProbation", SqlDbType.VarChar).Value = EligibleAtProbation
            cmd.Parameters.Add("@Payable", SqlDbType.VarChar).Value = Payable
            cmd.Parameters.Add("@specificduration", SqlDbType.VarChar).Value = SpecificDays
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

            'Old Data
            If txtid.Text <> "0" Then
                Dim strUser As New DataSet
                strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Leave_Type_get", txtid.Text)
                olddata(0) = strUser.Tables(0).Rows(0).Item("id").ToString
                olddata(1) = strUser.Tables(0).Rows(0).Item("Name").ToString
                olddata(2) = strUser.Tables(0).Rows(0).Item("LeavePerYear").ToString
                olddata(3) = strUser.Tables(0).Rows(0).Item("EmployeeCanApply").ToString
                olddata(4) = strUser.Tables(0).Rows(0).Item("SendNotificationEmail").ToString
                olddata(5) = strUser.Tables(0).Rows(0).Item("LeaveAccruedEnabled").ToString
                olddata(6) = strUser.Tables(0).Rows(0).Item("LeaveCarriedForward").ToString
                olddata(7) = strUser.Tables(0).Rows(0).Item("PercentageCarriedForward").ToString
                olddata(8) = strUser.Tables(0).Rows(0).Item("CarriedForwardLeaveAvailabilityPeriod").ToString
                olddata(9) = strUser.Tables(0).Rows(0).Item("gender").ToString
                olddata(10) = strUser.Tables(0).Rows(0).Item("EligibleAtProbation").ToString
                olddata(11) = strUser.Tables(0).Rows(0).Item("payable").ToString
            End If

            If txtid.Text.Trim = "" Then
                leavetypes.id = 0
            Else
                leavetypes.id = txtid.Text
            End If

            leavetypes.Name = aname.Value.Trim
            leavetypes.LeavePerYear = leavePerYear.Value
            leavetypes.EmployeeCanApplyForLeave = radEmpCanApply.SelectedText
            leavetypes.SendMailNotification = "Yes"
            leavetypes.LeaveAccrued = radLeaveAccruedEnabled.SelectedText
            leavetypes.LeaveCarriedForward = radLeaveCarriedEnabled.SelectedText
            leavetypes.PercentageOfLeaveCarriedForward = txtPercent.Value
            leavetypes.CarriedForwardLeaveAvailabilityPeriod = radAvailabilityPeriod.SelectedText
            leavetypes.Gender = radGender.SelectedText
            leavetypes.EligibleAtProbation = radProbation.SelectedText
            leavetypes.AllowancePayable = radPayable.SelectedText
            Dim OldValue As String = ""
            Dim NewValue As String = ""

            Dim j As Integer = 0

            If txtid.Text <> "0" Then 'Updates
                For Each a In GetType(clsLeaveType).GetProperties()
                    If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If IsNumeric(a.GetValue(leavetypes, Nothing)) = True And IsNumeric(olddata(j)) = True Then
                                If CDbl(a.GetValue(leavetypes, Nothing)) <> CDbl(olddata(j)) Then
                                    NewValue += a.Name + ": " + a.GetValue(leavetypes, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            Else
                                If a.GetValue(leavetypes, Nothing).ToString <> olddata(j).ToString Then
                                    NewValue += a.Name + ": " + a.GetValue(leavetypes, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            End If
                        End If
                    End If
                    j = j + 1
                Next
            Else
                For Each a In GetType(clsLeaveType).GetProperties() 'New Entries
                    If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If a.GetValue(leavetypes, Nothing) = Nothing Then
                                NewValue += a.Name + ":" + " " & vbCrLf
                            Else
                                NewValue += a.Name + ": " + a.GetValue(leavetypes, Nothing).ToString & vbCrLf
                            End If
                        End If
                    End If
                Next
            End If

            If txtid.Text = "0" Then
                txtid.Text = GetIdentity(leavetypes.Name, leavetypes.LeavePerYear, leavetypes.EmployeeCanApplyForLeave, leavetypes.LeaveAccrued, leavetypes.LeaveCarriedForward, leavetypes.PercentageOfLeaveCarriedForward, leavetypes.CarriedForwardLeaveAvailabilityPeriod, leavetypes.Gender, leavetypes.EligibleAtProbation, leavetypes.AllowancePayable, radSpecific.SelectedText)
                If txtid.Text = "0" Then
                    Exit Sub
                End If
            Else
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Leave_Type_update", leavetypes.id, leavetypes.Name, leavetypes.LeavePerYear, leavetypes.EmployeeCanApplyForLeave, leavetypes.LeaveAccrued, leavetypes.LeaveCarriedForward, leavetypes.PercentageOfLeaveCarriedForward, leavetypes.CarriedForwardLeaveAvailabilityPeriod, leavetypes.Gender, leavetypes.EligibleAtProbation, leavetypes.AllowancePayable, radSpecific.SelectedText)
            End If


            If NewValue.Trim = "" And OldValue.Trim = "" Then
            Else
                If txtid.Text <> "0" Then
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Updated " & leavetypes.Name, "Leave Types")
                Else
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Inserted", "Leave Types")
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
            Response.Redirect("~/Module/Admin/leavetype", True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


    Protected Sub radLeaveCarriedEnabled_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.DropDownListEventArgs) Handles radLeaveCarriedEnabled.SelectedIndexChanged
        Try
            If radLeaveCarriedEnabled.SelectedItem.Text.ToUpper = "NO" Then
                Process.AssignRadDropDownValue(radAvailabilityPeriod, "0")
                txtPercent.Value = "0"

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


    Protected Sub radSpecific_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.DropDownListEventArgs) Handles radSpecific.SelectedIndexChanged
        Try
            If radSpecific.SelectedText = "No" Then
                leavePerYear.Value = "0"
                leavePerYear.Disabled = True
                Process.AssignRadDropDownValue(radLeaveAccruedEnabled, "No")
                Process.AssignRadDropDownValue(radLeaveCarriedEnabled, "No")
                Process.AssignRadDropDownValue(radAvailabilityPeriod, "0")
                txtPercent.Value = "0"
                radLeaveAccruedEnabled.Enabled = False
                radLeaveCarriedEnabled.Enabled = False
                radAvailabilityPeriod.Enabled = False
                txtPercent.Disabled = True
            Else
                leavePerYear.Disabled = False
                radLeaveAccruedEnabled.Enabled = True
                radLeaveCarriedEnabled.Enabled = True
                radAvailabilityPeriod.Enabled = True
                txtPercent.Disabled = False
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class