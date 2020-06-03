Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Telerik.Web.UI


Public Class LibraryToInitiate
    Inherits System.Web.UI.Page
    Dim trainsession As New clsTrainSession
    Dim AuthenCode As String = "MGRTRAININGS"
    Dim olddata(13) As String
    Dim oFileAttached1 As Byte
    Dim oFileAttached As Byte()
    Dim EmpID As String = ""
    Dim Level(2) As String
    Dim HREmpID As String = ""
    Dim HRLevel(2) As String
    Dim Separators() As Char = {":"c}
    Dim TrainersList As New StringBuilder()
    Private Sub LoadAccomplishment(sessionid As Integer)
        Try
            gridAcquire.DataSource = Process.SearchData("Training_Sessions_Skills_To_Acquire", sessionid)
            gridAcquire.DataBind()
        Catch ex As Exception
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then

                Process.LoadRadComboTextAndValueP2(cbopropose, "Emp_PersonalDetail_TrainingRequest", Session("UserEmpID"), Request.QueryString("id"), "Employee2", "EmpID", False)

                Dim strDataSet As New DataSet

                If Request.QueryString("id") IsNot Nothing Then
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Training_Sessions_get", Request.QueryString("id"))
                    LoadAccomplishment(Request.QueryString("id"))
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    aname.Value = strUser.Tables(0).Rows(0).Item("name").ToString
                    acourse.Value = strUser.Tables(0).Rows(0).Item("course").ToString
                    adate.Value = CDate(strUser.Tables(0).Rows(0).Item("scheduledtime")).ToLongDateString & " to " & CDate(strUser.Tables(0).Rows(0).Item("duedate")).ToLongDateString
                    adeliverymethod.Value = strUser.Tables(0).Rows(0).Item("deliverymethod").ToString
                    alocation.Value = strUser.Tables(0).Rows(0).Item("deliverylocation").ToString
                    atime.Value = Process.AMPM_Time(strUser.Tables(0).Rows(0).Item("trainingtime"))
                    anote.Value = strUser.Tables(0).Rows(0).Item("comment").ToString

                    asessiontype.Value = strUser.Tables(0).Rows(0).Item("TrainingType").ToString

                    If asessiontype.Value.ToLower = "internal" Then
                        'Get Trainees
                        atrainer.Visible = False
                        'Process.LoadListAndComboxFromDataset(lstTrainer, cboTrainer, "Training_Session_Get_Trainers", "Trainers", "EmpiD", txtid.Text)

                        Process.LoadListBoxP1(lsttrainers, "Training_Session_Get_Trainers", txtid.Text, "Trainers")
                        If lsttrainers.Items.Count < 1 Then
                            lsttrainers.Visible = False
                        Else
                            lsttrainers.Visible = True
                        End If
                    Else
                        atrainer.Visible = True

                        lsttrainers.Visible = False

                        Dim strTrainer As New DataSet
                        strTrainer = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Training_Session_Get_Trainers", txtid.Text)
                        If strTrainer.Tables(0).Rows.Count > 0 Then
                            atrainer.Value = strTrainer.Tables(0).Rows(0).Item("Trainers").ToString
                        End If
                    End If

                End If

                Process.LoadListAndComboxFromDatasetP2(lstpropose, cbopropose, "Emp_PersonalDetail_TrainingRequest_Get", "Employee2", "EmpiD", Session("UserEmpID"), txtid.Text)

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            Dim lblstatus As String = ""
            If acomment.Value.Trim = "" Then
                lblstatus = "include comment to aid approval process!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                acomment.Focus()
                Exit Sub

            End If
            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Training_Sessions_Request_Mgr_Flg_Update", txtid.Text, Session("UserEmpID"))
            Dim strUser As New DataSet
            If lstpropose.Items.Count > 0 Then
                For d As Integer = 0 To lstpropose.Items.Count - 1
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Training_Sessions_Request_Mgr_Update", txtid.Text, lstpropose.Items(d).Value, Session("UserEmpID"), "Approved", acomment.Value)

                    Process.Training_Manager_Request(aname.Value, adate.Value, atime.Value, alocation.Value, "", lstpropose.Items(d).Value, Session("UserEmpID"), Process.GetMailLink(AuthenCode, 1))
                Next
            End If
            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Training_Sessions_Request_Mgr_Delete", txtid.Text, Session("UserEmpID"))


            lblstatus = "Request sent for HR Approval"
            Process.loadalert(divalert, msgalert, lblstatus, "success")

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect("~/Module/Employee/TrainingPortal/Trainings")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
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


    Protected Sub cbopropose_ItemChecked(sender As Object, e As Telerik.Web.UI.RadComboBoxItemEventArgs) Handles cbopropose.ItemChecked
        Process.LoadListBoxFromCombo(lstpropose, cbopropose)
    End Sub
End Class