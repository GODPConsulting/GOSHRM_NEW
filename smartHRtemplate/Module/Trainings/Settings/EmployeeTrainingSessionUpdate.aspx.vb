Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO


Public Class EmployeeTrainingSessionUpdate
    Inherits System.Web.UI.Page
    Dim empTrainSession As New clsEmpTrainSession
    Dim AuthenCode As String = "EMPTRAINSESSION"
    Dim olddata(4) As String
    Dim Level1(2) As String
    Dim EmpID As String
    Dim Separators() As Char = {":"c}
    Dim lblstatus As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                Session("PreviousPage") = Request.UrlReferrer.ToString
                Process.LoadRadDropDownTextAndValueP1(radSession, "Training_Sessions_get_name", Process.GetCompanyName, "name", "id", False)

                radStatus.Items.Clear()
                radStatus.Items.Add("Scheduled")
                radStatus.Items.Add("Attended")
                radStatus.Items.Add("Not-Attended")
                radStatus.Items.Add("Cancelled")

                'Company_Structure_get_parent
                If Request.QueryString("id") IsNot Nothing Then
                    Process.LoadRadComboTextAndValueP1(cboEmployee, "Emp_PersonalDetail_Get_Employees", Session("Access"), "Employee2", "empid", False)
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Employee_Training_Sessions_get", Request.QueryString("id"))
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    Process.AssignRadComboValue(cboEmployee, strUser.Tables(0).Rows(0).Item("empid").ToString)
                    Process.AssignRadDropDownValue(radSession, strUser.Tables(0).Rows(0).Item("trainingsessionid").ToString)
                    Process.AssignRadDropDownValue(radStatus, strUser.Tables(0).Rows(0).Item("Status").ToString)
                    cboEmployee.Enabled = False
                Else

                    txtid.Text = "0"
                    If Request.QueryString("sessionid") IsNot Nothing Then
                        Process.LoadRadComboTextAndValueP2(cboEmployee, "Emp_PersonalDetail_Get_TrainingSession", Session("Access"), Request.QueryString("sessionid"), "Employee2", "empid", False)
                        Process.AssignRadDropDownValue(radSession, Request.QueryString("sessionid"))
                    End If

                End If
                radSession.Enabled = False
            End If
        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "danger")
        End Try
    End Sub
    Private Function GetIdentity(ByVal sEmpID As String, ByVal Training As String, ByVal training_status As String) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Employee_Training_Sessions_update"
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@empid", SqlDbType.VarChar).Value = sEmpID
            cmd.Parameters.Add("@Session", SqlDbType.VarChar).Value = Training
            cmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = training_status
            
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception
            Process.strExp = ex.Message
            Return 0
        End Try
    End Function
    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            If txtid.Text.Trim <> "0" Then
                If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False Then
                    Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "danger")
                    Exit Sub
                End If
            End If
          


            If (radSession.SelectedText.Trim = "") Or (radSession.SelectedText.Trim = "--Select--") Then
                lblstatus = "Training Session required!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                radSession.Focus()
                Exit Sub
            End If

            If (radStatus.SelectedText.Trim = "") Or (radStatus.SelectedText.Trim = "--Select--") Then
                lblstatus = "Training Status required!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                radStatus.Focus()
                Exit Sub
            End If


            'Old Data

            If txtid.Text.Trim = "" Or txtid.Text.Trim = "0" Then
                empTrainSession.id = 0
            Else
                empTrainSession.id = txtid.Text
            End If
            empTrainSession.EmpID = cboEmployee.SelectedValue
            empTrainSession.Status = radStatus.SelectedText
            empTrainSession.Training = radSession.SelectedValue

            Dim OldValue As String = ""
            Dim NewValue As String = ""

            If txtid.Text.Trim = "" Or txtid.Text.Trim = "0" Then
                txtid.Text = GetIdentity(empTrainSession.EmpID, empTrainSession.Training, empTrainSession.Status)

                
                If txtid.Text = "0" Then
                    lblstatus = Process.strExp
                    Exit Sub
                End If

                Dim strSession As New DataSet
                strSession = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Training_Sessions_get", radSession.SelectedValue)
                If strSession.Tables(0).Rows.Count > 0 Then
                    Process.Training_Notification_Trainees(strSession.Tables(0).Rows(0).Item("name").ToString, "", "", strSession.Tables(0).Rows(0).Item("deliverylocation").ToString, strSession.Tables(0).Rows(0).Item("scheduledtime").ToString, Process.AMPM_Time(strSession.Tables(0).Rows(0).Item("trainingtime").ToString), strSession.Tables(0).Rows(0).Item("duedate").ToString, cboEmployee.SelectedValue, Process.GetMailLink(AuthenCode, 2))
                End If
            Else
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Employee_Training_Sessions_update", empTrainSession.id, empTrainSession.EmpID, empTrainSession.Training, empTrainSession.Status)
            End If


            lblstatus = "Record saved"
            Process.loadalert(divalert, msgalert, lblstatus, "success")
            Response.Write("<script language='javascript'> { self.close() }</script>")
        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "danger")
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Response.Redirect(Session("PreviousPage"), True)
        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "danger")
        End Try
    End Sub


    
End Class