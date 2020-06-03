Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Microsoft.VisualBasic


Public Class WorkShiftExemptionUpdate
    Inherits System.Web.UI.Page
    Dim TimeSheet As New clsTimeSheet
    Dim ApproveLeave As New clsApproveLeave
    Dim AuthenCode As String = "WORKSHIFTEXE"
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
                Process.LoadRadDropDownTextAndValue(cboShiftname, "Job_Work_Shift_Get_All", "shiftname", "shiftname", False)
                Process.LoadRadDropDownTextAndValue(cboJobGrade, "Job_Grade_get_all", "name", "name", False)
                If Request.QueryString("id") IsNot Nothing Then

                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Job_Work_Shift_Exemptions_Exemptions_Get", Request.QueryString("id"))

                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    lblDays.Value = strUser.Tables(0).Rows(0).Item("duration").ToString
                    Process.LoadTimeToRadCombo(radHourStart, radMinStart, radTimeStart, strUser.Tables(0).Rows(0).Item("timeFrom"))
                    Process.LoadTimeToRadCombo(radHourStart0, radMinStart0, radTimeStart0, strUser.Tables(0).Rows(0).Item("timeTo"))
                    Process.AssignRadDropDownValue(cboJobGrade, strUser.Tables(0).Rows(0).Item("jobgrade").ToString)
                    Process.AssignRadDropDownValue(cboShiftname, strUser.Tables(0).Rows(0).Item("shiftname").ToString)

                Else
                    txtid.Text = "0"
                End If

            End If
        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Function GetIdentity(ByVal shiftname As String, ByVal jobgrade As String, ByVal timefrom As String, ByVal timeto As String, ByVal timeduration As Integer) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Job_Work_Shift_Exemptions_Update"
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@shiftname", SqlDbType.VarChar).Value = shiftname
            cmd.Parameters.Add("@jobgrade", SqlDbType.VarChar).Value = jobgrade
            cmd.Parameters.Add("@timefrom", SqlDbType.VarChar).Value = timefrom
            cmd.Parameters.Add("@timeto", SqlDbType.VarChar).Value = timeto
            cmd.Parameters.Add("@duration", SqlDbType.Int).Value = timeduration
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
                If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False And Process.AuthenAction(Session("role"), AuthenCode, "Update") = False Then
                    Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "danger")
                    Exit Sub
                End If
            Else
                If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False And Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                    Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "danger")
                    Exit Sub
                End If
            End If


            lblstatus = "Record saving, please wait ..."
            Process.loadalert(divalert, msgalert, lblstatus, "success")

            If (cboShiftname.SelectedText Is Nothing) Then
                lblstatus = "Shift Name required!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                cboShiftname.Focus()
                Exit Sub
            End If

            If (cboJobGrade.SelectedText Is Nothing) Then
                lblstatus = "Job Grade required!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                cboJobGrade.Focus()
                Exit Sub
            End If



            If CDate(Process.ConvertCtrlToTime(radHourStart0.Text, radMinStart0.Text, radTimeStart0.Text)).TimeOfDay < CDate(Process.ConvertCtrlToTime(radHourStart.Text, radMinStart.Text, radTimeStart.Text)).TimeOfDay Then
                lblstatus = "End Time can not be time ahead of Start Time!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                Exit Sub
            End If

            If lblDays.Value.Trim = "0" Then
                lblstatus = "Error in time range set!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                radHourStart.Focus()
                Exit Sub
            End If

            btnupdate.EnableViewState = False

            If txtid.Text <> "0" And txtid.Text.Trim <> "" Then
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Job_Work_Shift_Exemptions_Update", txtid.Text, cboShiftname.SelectedValue, cboJobGrade.SelectedValue, Process.ConvertCtrlToTime(radHourStart.Text, radMinStart.Text, radTimeStart.Text), Process.ConvertCtrlToTime(radHourStart0.Text, radMinStart0.Text, radTimeStart0.Text), lblDays.Value)
            Else

            End If
            txtid.Text = GetIdentity(cboShiftname.SelectedValue, cboJobGrade.SelectedValue, Process.ConvertCtrlToTime(radHourStart.Text, radMinStart.Text, radTimeStart.Text), Process.ConvertCtrlToTime(radHourStart0.Text, radMinStart0.Text, radTimeStart0.Text), lblDays.Value)
            If txtid.Text = "0" Then
                lblstatus = Process.strExp
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
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
            Response.Redirect("~/Module/TimeManagement/WorkShiftsExemption.aspx", True)
        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "danger")
        End Try
    End Sub


    Protected Sub radHourStart_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles radHourStart.SelectedIndexChanged
        Try

            Dim Duration As Integer = 0
            Duration = 1 * (CDate(Process.ConvertCtrlToTime(radHourStart0.Text, radMinStart0.Text, radTimeStart0.Text)).TimeOfDay.TotalHours - CDate(Process.ConvertCtrlToTime(radHourStart.Text, radMinStart.Text, radTimeStart.Text)).TimeOfDay.TotalHours)
            If Duration < 0 Then
                lblDays.Value = 0
            Else
                lblDays.Value = Duration 'NoDays * (CDate(Process.ConvertCtrlToTime(radHourStart0, radMinStart0, radTimeStart0)).TimeOfDay.TotalHours - CDate(Process.ConvertCtrlToTime(radHourStart, radMinStart, radTimeStart)).TimeOfDay.TotalHours)
            End If

        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "danger")
        End Try
    End Sub

    Protected Sub radMinStart_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles radMinStart.SelectedIndexChanged
        Try

            Dim Duration As Integer = 0
            Duration = 1 * (CDate(Process.ConvertCtrlToTime(radHourStart0.Text, radMinStart0.Text, radTimeStart0.Text)).TimeOfDay.TotalHours - CDate(Process.ConvertCtrlToTime(radHourStart.Text, radMinStart.Text, radTimeStart.Text)).TimeOfDay.TotalHours)
            If Duration < 0 Then
                lblDays.Value = 0
            Else
                lblDays.Value = Duration
            End If

        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "danger")
        End Try
    End Sub

    Protected Sub radTimeStart_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles radTimeStart.SelectedIndexChanged
        Try

            Dim Duration As Integer = 0
            Duration = 1 * (CDate(Process.ConvertCtrlToTime(radHourStart0.Text, radMinStart0.Text, radTimeStart0.Text)).TimeOfDay.TotalHours - CDate(Process.ConvertCtrlToTime(radHourStart.Text, radMinStart.Text, radTimeStart.Text)).TimeOfDay.TotalHours)
            If Duration < 0 Then
                lblDays.Value = 0
            Else
                lblDays.Value = Duration
            End If

        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "danger")
        End Try
    End Sub

    Protected Sub radHourStart0_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles radHourStart0.SelectedIndexChanged
        Try
            Dim Duration As Integer = 0
            Duration = 1 * (CDate(Process.ConvertCtrlToTime(radHourStart0.Text, radMinStart0.Text, radTimeStart0.Text)).TimeOfDay.TotalHours - CDate(Process.ConvertCtrlToTime(radHourStart.Text, radMinStart.Text, radTimeStart.Text)).TimeOfDay.TotalHours)
            If Duration < 0 Then
                lblDays.Value = 0
            Else
                lblDays.Value = Duration
            End If

        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "danger")
        End Try
    End Sub

    Protected Sub radMinStart0_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles radMinStart0.SelectedIndexChanged
        Try
            Dim Duration As Integer = 0
            Duration = 1 * (CDate(Process.ConvertCtrlToTime(radHourStart0.Text, radMinStart0.Text, radTimeStart0.Text)).TimeOfDay.TotalHours - CDate(Process.ConvertCtrlToTime(radHourStart.Text, radMinStart.Text, radTimeStart.Text)).TimeOfDay.TotalHours)
            If Duration < 0 Then
                lblDays.Value = 0
            Else
                lblDays.Value = Duration
            End If

        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "danger")
        End Try
    End Sub

    Protected Sub radTimeStart0_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles radTimeStart0.SelectedIndexChanged
        Try

            Dim Duration As Integer = 0
            If Duration = 1 * (CDate(Process.ConvertCtrlToTime(radHourStart0.Text, radMinStart0.Text, radTimeStart0.Text)).TimeOfDay.TotalHours - CDate(Process.ConvertCtrlToTime(radHourStart.Text, radMinStart.Text, radTimeStart.Text)).TimeOfDay.TotalHours) Then
                lblDays.Value = Duration
            End If

        Catch ex As Exception
            lblstatus = ex.Message
        End Try
    End Sub
End Class