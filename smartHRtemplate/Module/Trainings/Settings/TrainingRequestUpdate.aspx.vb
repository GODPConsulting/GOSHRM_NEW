Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO


Public Class TrainingRequestUpdate
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

                    rdoApprovalStatus.Items.Clear()
                    rdoApprovalStatus.Items.Add("Pending")
                    rdoApprovalStatus.Items.Add("Approved")
                    rdoApprovalStatus.Items.Add("Rejected")

                    Process.RadioListCheck(rdoApprovalStatus, "Pending")
                    'Company_Structure_get_parent
                    If Request.QueryString("id") IsNot Nothing Then
                        Dim strUser As New DataSet
                        strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Training_Sessions_Request_Get", Request.QueryString("id"))
                        txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                        lblemp.Value = strUser.Tables(0).Rows(0).Item("Employee").ToString
                        lblempid.Text = strUser.Tables(0).Rows(0).Item("empid").ToString
                        lblapprovalby.Value = strUser.Tables(0).Rows(0).Item("ForwardedTo").ToString
                        lblapprovaldate.Value = strUser.Tables(0).Rows(0).Item("HRApprovalDate").ToString
                        lblapprovalstatus.Value = strUser.Tables(0).Rows(0).Item("ForwardApproval").ToString
                        lblrequestdate.Value = strUser.Tables(0).Rows(0).Item("DateRequest").ToString
                        lblsession.Value = strUser.Tables(0).Rows(0).Item("Name").ToString
                        lblhod.Text = strUser.Tables(0).Rows(0).Item("forwardtoid").ToString
                        lbltrainingsessionid.Text = strUser.Tables(0).Rows(0).Item("TrainingSessionID").ToString
                        Process.RadioListCheck(rdoApprovalStatus, strUser.Tables(0).Rows(0).Item("HRApproval").ToString)
                    Else
                        txtid.Text = "0"
                    End If
                End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "warning")
        End Try
    End Sub
    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Training_Sessions_Request_Update_Status_HR", txtid.Text, Session("UserEmpID"), rdoApprovalStatus.SelectedValue)

            Dim strUser As New DataSet
            strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Training_Sessions_get", lbltrainingsessionid.Text)
            If strUser.Tables(0).Rows.Count > 0 Then
                Dim tsession As String = strUser.Tables(0).Rows(0).Item("name").ToString
                Dim trainingdate As Date = CDate(strUser.Tables(0).Rows(0).Item("scheduledtime"))
                Dim duedate As Date = CDate(strUser.Tables(0).Rows(0).Item("duedate"))
                Dim deliverymethod As String = strUser.Tables(0).Rows(0).Item("deliverymethod").ToString
                Dim location As String = strUser.Tables(0).Rows(0).Item("deliverylocation").ToString
                Dim trainer As String = ""
                Dim coordinator = strUser.Tables(0).Rows(0).Item("coordinator").ToString
                Dim trainingtime As String = strUser.Tables(0).Rows(0).Item("trainingtime").ToString

                Dim stremail As New DataSet

                stremail = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "select email from Employees_All where EmpID = '" & lblhod.Text & "'")
                Dim approver As String = ""
                If stremail.Tables(0).Rows.Count > 0 Then
                    approver = stremail.Tables(0).Rows(0).Item("email").ToString
                End If

                Dim empmail As String = ""
                Dim empname As String = ""
                stremail = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_PersonalDetail_get", lblempid.Text)
                If stremail.Tables(0).Rows.Count > 0 Then
                    empmail = stremail.Tables(0).Rows(0).Item("Office Email").ToString
                    empname = stremail.Tables(0).Rows(0).Item("First Name").ToString & " " & stremail.Tables(0).Rows(0).Item("Last Name").ToString

                End If

                If rdoApprovalStatus.SelectedItem.Text = "Approved" Then
                    Process.Training_Notification_Trainees(tsession, trainer, coordinator, location, trainingdate, Process.AMPM_Time(trainingtime), duedate, lblempid.Text, Process.ApplicationURL & "/" & Process.GetMailLink(AuthenCode, 2))
                End If
                lblstatus = "Record Saved"
                Process.loadalert(divalert, msgalert, lblstatus, "success")
                Response.Redirect("~/Module/Trainings/Settings/TrainingSessions", True)
            Else
                lblstatus = "Training Session don't exist!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
            End If

            'Response.Write("<script language='javascript'> { self.close() }</script>")
        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "warning")
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            'Response.Redirect(Session("PreviousPage"))
            Response.Redirect("~/Module/Trainings/Settings/TrainingSessions", True)
        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "warning")
        End Try
    End Sub



End Class