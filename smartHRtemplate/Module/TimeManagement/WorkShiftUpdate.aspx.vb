Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Microsoft.VisualBasic


Public Class WorkShiftUpdate
    Inherits System.Web.UI.Page
    Dim TimeSheet As New clsTimeSheet
    Dim ApproveLeave As New clsApproveLeave
    Dim AuthenCode As String = "WORKSHIFT"
    Dim olddata(11) As String
    Dim emp_emailaddr As String
    Dim approver1_emailaddr As String
    Dim approver2_emailaddr As String
    Dim LeaveBalance As Integer = 0
    Dim NoDays As Integer = 0
    Dim Level1(2) As String
    Dim Level2(2) As String
    Dim Separators() As Char = {":"c}
    Dim EmpID_1 As String = ""
    Dim EmpID_2 As String = ""
    Dim EmpID_1_Name As String = ""
    Dim EmpID_2_Name As String = ""
    Dim isEligible As String = "Yes"
    Dim lblstatus As String = ""



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            '

            If Not Me.IsPostBack Then
                If Request.QueryString("id") IsNot Nothing Then

                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Job_Work_Shift_Get", Request.QueryString("id"))

                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    lblDays.Value = strUser.Tables(0).Rows(0).Item("duration").ToString
                    Process.LoadTimeToRadCombo(radHourStart, radMinStart, radTimeStart, strUser.Tables(0).Rows(0).Item("timeFrom"))
                    Process.LoadTimeToRadCombo(radHourEnd, radMinEnd, radTimeEnd, strUser.Tables(0).Rows(0).Item("timeTo"))
                    txtShiftName.Value = strUser.Tables(0).Rows(0).Item("shiftname").ToString
                Else
                    txtid.Text = "0"
                    Process.AssignRadComboValue(radHourStart, "8")
                    Process.AssignRadComboValue(radHourEnd, "5")
                    Process.AssignRadComboValue(radTimeEnd, "PM")
                End If

                lblDays.Value = GetDuration()

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Function GetIdentity(ByVal shiftname As String, ByVal timefrom As String, ByVal timeto As String, ByVal timeduration As Integer, ByVal tstart As String, ByVal tend As String) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Job_Work_Shift_Update"
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@shiftname", SqlDbType.VarChar).Value = shiftname
            cmd.Parameters.Add("@timefrom", SqlDbType.VarChar).Value = timefrom
            cmd.Parameters.Add("@timeto", SqlDbType.VarChar).Value = timeto
            cmd.Parameters.Add("@duration", SqlDbType.Int).Value = timeduration
            cmd.Parameters.Add("@tstart", SqlDbType.VarChar).Value = tstart
            cmd.Parameters.Add("@tend", SqlDbType.VarChar).Value = tend
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception
            Process.strExp = ex.Message
            Return 0
        End Try
    End Function

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            If Request.QueryString("id") IsNot Nothing Then
                If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False Then
                    Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "danger")
                    Exit Sub
                End If
            Else
                If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                    Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "danger")
                    Exit Sub
                End If
            End If


            lblstatus = "Record saving, please wait ..."
            Process.loadalert(divalert, msgalert, lblstatus, "danger")

            If (txtShiftName.Value Is Nothing) Then
                lblstatus = "Shift Name required!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                txtShiftName.Focus()
                Exit Sub
            End If

            'If CDate(Process.ConvertCtrlToTime(radHourEnd, radMinEnd, radTimeEnd)).TimeOfDay < CDate(Process.ConvertCtrlToTime(radHourStart, radMinStart, radTimeStart)).TimeOfDay Then
            '    lblstatus.Text = "End Time can not be time ahead of Start Time!"
            '    Exit Sub
            'End If

            If lblDays.Value.Trim = "0" Then
                lblstatus = "Error in time range set!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                radHourStart.Focus()
                Exit Sub
            End If

            btnupdate.EnableViewState = False


            If txtid.Text <> "0" And txtid.Text.Trim <> "" Then
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Job_Work_Shift_Update", txtid.Text, txtShiftName.Value, Process.ConvertCtrlToTime(radHourStart.Text, radMinStart.Text, radTimeStart.Text), Process.ConvertCtrlToTime(radHourEnd.Text, radMinEnd.Text, radTimeEnd.Text), lblDays.Value, radTimeStart.SelectedItem.Value, radTimeEnd.SelectedItem.Value)

            Else
                txtid.Text = GetIdentity(txtShiftName.Value, Process.ConvertCtrlToTime(radHourStart.Text, radMinStart.Text, radTimeStart.Text), Process.ConvertCtrlToTime(radHourEnd.Text, radMinEnd.Text, radTimeEnd.Text), lblDays.Value, radTimeStart.SelectedItem.Value, radTimeEnd.SelectedItem.Value)
                If txtid.Text = "0" Then
                    lblstatus = Process.strExp
                    Process.loadalert(divalert, msgalert, lblstatus, "danger")
                    Exit Sub
                End If
            End If


            lblstatus = "Record saved"
            Process.loadalert(divalert, msgalert, lblstatus, "success")

        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "danger")
        Finally
            btnupdate.EnableViewState = True
        End Try
    End Sub


    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            Response.Write("<script language='javascript'> { self.close() }</script>")
            Response.Redirect("~/Module/TimeManagement/WorkShifts", True)
        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "success")
        End Try
    End Sub

    Private Function GetDuration() As Integer
        Try
            Dim Duration As Integer = 0

            Duration = (CDate(Process.ConvertCtrlToTime(radHourEnd.Text, radMinEnd.Text, radTimeEnd.Text)).TimeOfDay.TotalHours - CDate(Process.ConvertCtrlToTime(radHourStart.Text, radMinStart.Text, radTimeStart.Text)).TimeOfDay.TotalHours)
            If Duration < 0 Then
                Duration = DateDiff(DateInterval.Hour, CDate(Process.ConvertCtrlToTime(radHourEnd.Text, radMinEnd.Text, radTimeEnd.Text)).AddDays(1), CDate(Process.ConvertCtrlToTime(radHourStart.Text, radMinStart.Text, radTimeStart.Text)))
            Else
                Duration = DateDiff(DateInterval.Hour, CDate(Process.ConvertCtrlToTime(radHourEnd.Text, radMinEnd.Text, radTimeEnd.Text)), CDate(Process.ConvertCtrlToTime(radHourStart.Text, radMinStart.Text, radTimeStart.Text)))
            End If

            If radTimeEnd.SelectedValue = radTimeStart.SelectedValue Then
                If CInt(radHourStart.SelectedValue) > CInt(radHourEnd.SelectedValue) Then
                    lblDays1.Value = "Next Day"
                ElseIf CInt(radHourStart.SelectedValue) = CInt(radHourEnd.SelectedValue) Then
                    If CInt(radMinStart.SelectedValue) > CInt(radMinEnd.SelectedValue) Then
                        lblDays1.Value = "Next Day"
                    End If
                End If
            ElseIf radTimeEnd.SelectedValue < radTimeStart.SelectedValue Then
                lblDays1.Value = "Next Day"
            Else
                lblDays1.Value = "Same Day"
            End If

            Return Math.Abs(Duration)
        Catch ex As Exception
            Return 0
        End Try
    End Function
    Protected Sub radHourStart_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles radHourStart.SelectedIndexChanged
        Try

            lblDays.Value = GetDuration()

        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "success")
        End Try
    End Sub

    Protected Sub radMinStart_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles radMinStart.SelectedIndexChanged
        Try
            lblDays.Value = GetDuration()

        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "success")
        End Try
    End Sub

    Protected Sub radTimeStart_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles radTimeStart.SelectedIndexChanged
        Try
            lblDays.Value = GetDuration()

        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "success")
        End Try
    End Sub

    Protected Sub radHourStart0_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles radHourEnd.SelectedIndexChanged
        Try
            lblDays.Value = GetDuration()

        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "success")
        End Try
    End Sub

    Protected Sub radMinStart0_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles radMinEnd.SelectedIndexChanged
        Try
            lblDays.Value = GetDuration()

        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "success")
        End Try
    End Sub

    Protected Sub radTimeStart0_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles radTimeEnd.SelectedIndexChanged
        Try
            lblDays.Value = GetDuration()

        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "success")
        End Try
    End Sub
End Class