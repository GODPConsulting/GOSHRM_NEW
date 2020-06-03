Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Telerik.Web.UI


Public Class TrainingApproval
    Inherits System.Web.UI.Page
    Dim trainsession As New clsTrainSession
    Dim AuthenCode As String = "APPTRAINING"
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
                'Process.LoadRadCombo(radHR, "Emp_PersonalDetail_Get_HR_Staff", 0)

                'Company_Structure_get_parent
                radDeliveryMethod.Items.Clear()
                radDeliveryMethod.Items.Add("Classroom")
                radDeliveryMethod.Items.Add("Online")
                radDeliveryMethod.Items.Add("Self Study")



                radSessionType.Items.Clear()
                radSessionType.Items.Add("External")
                radSessionType.Items.Add("Internal")

                cboApproval.Items.Clear()
                cboApproval.Items.Add("Pending")
                cboApproval.Items.Add("Approved")
                cboApproval.Items.Add("Rejected")

                'cboHRRequired.Items.Clear()
                'cboHRRequired.Items.Add("No")
                'cboHRRequired.Items.Add("Yes")

                Process.LoadRadDropDownTextAndValue(radCoordinator, "Emp_PersonalDetail_Get_Employees", "Employee2", "EmpID")
                Process.LoadRadComboTextAndValue(cboTrainer, "Emp_PersonalDetail_Get_Employees", "Employee", "EmpID", False)


                Dim strDataSet As New DataSet

                If Request.QueryString("id") IsNot Nothing Then
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Training_Sessions_Request_Get", Request.QueryString("id"))

                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    txtname.Text = strUser.Tables(0).Rows(0).Item("name").ToString
                    lblemployee.Text = strUser.Tables(0).Rows(0).Item("employee").ToString
                    'txtDesc.Text = strUser.Tables(0).Rows(0).Item("details").ToString
                    radScheduleTime.SelectedDate = CDate(strUser.Tables(0).Rows(0).Item("scheduledtime"))
                    radDateDue.SelectedDate = strUser.Tables(0).Rows(0).Item("duedate").ToString
                    radDeliveryMethod.SelectedText = strUser.Tables(0).Rows(0).Item("deliverymethod").ToString
                    txtLocation.Text = strUser.Tables(0).Rows(0).Item("deliverylocation").ToString
                    lblempid.Text = strUser.Tables(0).Rows(0).Item("empid").ToString
                    radSessionType.SelectedText = strUser.Tables(0).Rows(0).Item("TrainingType").ToString
                    radCoordinator.SelectedText = strUser.Tables(0).Rows(0).Item("coordinator").ToString
                    Process.AssignRadComboValue(cboApproval, strUser.Tables(0).Rows(0).Item("forwardapproval").ToString)
                    txtComment.Text = strUser.Tables(0).Rows(0).Item("managercomment").ToString
                    lbltime.Value = Process.AMPM_Time(strUser.Tables(0).Rows(0).Item("trainingtime").ToString)
                    If radSessionType.SelectedText.ToLower = "internal" Then
                        'Get Trainees
                        txtTrainer.Visible = False
                        Process.LoadListAndComboxFromDataset(lstTrainer, cboTrainer, "Training_Session_Get_Trainers", "Trainers", "EmpiD", strUser.Tables(0).Rows(0).Item("trainingsessionid").ToString)
                        If lstTrainer.Items.Count < 1 Then
                            lstTrainer.Visible = False
                        Else
                            lstTrainer.Visible = True
                        End If
                    Else
                        txtTrainer.Visible = True
                        cboTrainer.Visible = False
                        lstTrainer.Visible = False

                        Dim strTrainer As New DataSet
                        strTrainer = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Training_Session_Get_Trainers", txtid.Text)
                        If strTrainer.Tables(0).Rows.Count > 0 Then
                            txtTrainer.Text = strTrainer.Tables(0).Rows(0).Item("Trainers").ToString
                        End If
                    End If

                End If

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try

            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Training_Sessions_Request_Update_Status", txtid.Text, cboApproval.SelectedItem.Text, txtComment.Text)

            'Dim forwardto As String = "", empmail As String = "", empname As String = "", empoffice As String = ""
            'Dim stremail As New DataSet
            'stremail = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "select employee2 from Employees_All where EmpID = '" & Session("UserEmpID") & "'")
            'Dim approver As String = ""
            'If stremail.Tables(0).Rows.Count > 0 Then
            '    approver = stremail.Tables(0).Rows(0).Item("employee2").ToString
            'End If


            'stremail = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_PersonalDetail_get", lblempid.Text)
            'If stremail.Tables(0).Rows.Count > 0 Then
            '    empmail = stremail.Tables(0).Rows(0).Item("Office Email").ToString
            '    empname = stremail.Tables(0).Rows(0).Item("First Name").ToString & " " & stremail.Tables(0).Rows(0).Item("Last Name").ToString
            '    empoffice = stremail.Tables(0).Rows(0).Item("Office").ToString
            'End If

            If cboApproval.SelectedItem.Text.ToUpper = "APPROVED" Then
                Process.Training_Emp_Request_To_HR(Date.Now, txtname.Text, radCoordinator.SelectedItem.Text, radScheduleTime.SelectedDate, lbltime.Value, txtLocation.Text, lblempid.Text, Session("UserEmpID"), Process.ApplicationURL + "/" + Process.GetMailLink(AuthenCode, 1))
            Else
                Process.Training_Emp_UnApproved(cboApproval.SelectedItem.Text, txtname.Text, lblempid.Text, Session("UserEmpID"))
            End If
            Process.loadalert(divalert, msgalert, "Approval Status updated", "success")
            Response.Redirect("Approvaltrainings", True)

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "success")
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect("Approvaltrainings", True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "success")
        End Try
    End Sub




    Protected Sub drpTrainee_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs)

    End Sub



    Protected Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs)
        'Try
        '    If CheckBox3.Checked = True Then
        '        lblHR.Visible = True
        '        txtHR.Visible = True
        '        radHR.Visible = True
        '    Else
        '        lblHR.Visible = False
        '        txtHR.Visible = False
        '        radHR.Visible = False
        '    End If
        'Catch ex As Exception

        'End Try
    End Sub

    Protected Sub radSessionType_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.DropDownListEventArgs) Handles radSessionType.SelectedIndexChanged
        Try
            If radSessionType.SelectedText.ToLower = "internal" Then
                lstTrainer.Visible = True
                cboTrainer.Visible = True
                txtTrainer.Visible = False
            Else
                lstTrainer.Visible = False
                cboTrainer.Visible = False
                txtTrainer.Visible = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub cboTrainer_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs)

    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs)
        Try
            Process.LoadListBoxFromCombo(lstTrainer, cboTrainer)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub


End Class