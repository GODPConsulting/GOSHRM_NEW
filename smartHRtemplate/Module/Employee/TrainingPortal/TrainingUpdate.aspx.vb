Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Telerik.Web.UI


Public Class TrainingUpdate
    Inherits System.Web.UI.Page
    Dim trainsession As New clsTrainSession
    Dim AuthenCode As String = "TRAINSESSION"
    Dim olddata(13) As String
    Dim oFileAttached1 As Byte
    Dim oFileAttached As Byte()
    Dim EmpID As String = ""
    Dim Level(2) As String
    Dim HREmpID As String = ""
    Dim HRLevel(2) As String
    Dim Separators() As Char = {":"c}
    Dim TrainersList As New StringBuilder()


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                If Request.QueryString("id") IsNot Nothing Then
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Training_Sessions_get", Request.QueryString("id"))

                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    txtname.Text = strUser.Tables(0).Rows(0).Item("name").ToString
                    txtCourse.Text = strUser.Tables(0).Rows(0).Item("course").ToString
                    txtScheduleDate.Text = strUser.Tables(0).Rows(0).Item("scheduledtime").ToString
                    txtDueDate.Text = strUser.Tables(0).Rows(0).Item("duedate").ToString
                    txtDeliveryMethod.Text = strUser.Tables(0).Rows(0).Item("deliverymethod").ToString
                    txtLocation.Text = strUser.Tables(0).Rows(0).Item("deliverylocation").ToString
                    txtTime.Text = strUser.Tables(0).Rows(0).Item("trainingtime").ToString
                    txtCurrency.Text = strUser.Tables(0).Rows(0).Item("Currency").ToString
                    txtCost.Text = strUser.Tables(0).Rows(0).Item("Cost").ToString
                    txtSessionType.Text = strUser.Tables(0).Rows(0).Item("TrainingType").ToString
                    txtCoordinator.Text = strUser.Tables(0).Rows(0).Item("coordinator").ToString
                    Process.LoadListBoxP1(lstTrainer, "Training_Session_Get_Trainers", txtid.Text, "trainers")
                    Process.LoadListBoxP1(lstTrainee, "Training_Session_Get_Trainees", txtid.Text, "Employee")
                End If

                If lstTrainee.Items.Count < 1 Then
                    lstTrainee.Visible = False
                Else
                    lstTrainee.Visible = True
                End If
            End If
        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub
    Private Function GetIdentity(ByVal empid As String, session As String, hod As String) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Employee_Training_Sessions_Apply"
            cmd.Parameters.Add("@empid", SqlDbType.VarChar).Value = empid
            cmd.Parameters.Add("@Session", SqlDbType.VarChar).Value = session
            cmd.Parameters.Add("@HOD", SqlDbType.VarChar).Value = hod
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception
            lblstatus.Text = ex.Message
            Return 0
        End Try
    End Function

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            lblstatus.Text = "Saving record, please wait ...."
            'Old Data
            Dim strUser As New DataSet
            strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "select SupervisorID, email, name,office,grade,jobtitle from dbo.Employees_All a where a.EmpID = '" & Session("UserEmpID") & "'")
            Dim supervisor As String = strUser.Tables(0).Rows(0).Item("SupervisorID").ToString
            Dim empemail As String = strUser.Tables(0).Rows(0).Item("email").ToString
            Dim empname As String = strUser.Tables(0).Rows(0).Item("name").ToString
            Dim office As String = strUser.Tables(0).Rows(0).Item("office").ToString

            ' SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Employee_Training_Sessions_Apply", Session("UserEmpID"), txtid.Text, supervisor)

            If GetIdentity(Session("UserEmpID"), txtid.Text, supervisor) <> "-1" Then
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Training request successfully sent" + "')", True)
                lblstatus.Text = "Training request successfully sent"

                Process.Training_Request_By_Trainee(empemail, empname, txtname.Text, txtScheduleDate.Text, Session("UserEmpID"), "")

                If supervisor <> "N/A" And supervisor.Trim <> "" Then
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "select email, name from dbo.Employees_All a where a.EmpID = '" & supervisor & "'")
                    empemail = strUser.Tables(0).Rows(0).Item("email").ToString
                    Dim supname As String = strUser.Tables(0).Rows(0).Item("name").ToString
                    Process.Training_For_Approval_Supervisors(empemail, empname, office, txtname.Text, txtLocation.Text, txtScheduleDate.Text, txtTime.Text, txtDueDate.Text, Session("UserEmpID"), supervisor, Process.GetMailList("hr"))
                End If
            Else
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You have either applied for this training before or you are already a participant" + "')", True)
            End If

            Process.DisableButton(btnAdd)

            'Response.Write("<script language='javascript'> { self.close() }</script>")
        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Response.Write("<script language='javascript'> { self.close() }</script>")
        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub
    Protected Sub drpTrainee_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs)

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

    End Sub
End Class