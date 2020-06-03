Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Telerik.Web.UI


Public Class LibraryDetail
    Inherits System.Web.UI.Page
    Dim trainsession As New clsTrainSession
    Dim AuthenCode As String = "EMPTRAININGS"
    Dim olddata(13) As String
    Dim oFileAttached1 As Byte
    Dim oFileAttached As Byte()
    Dim EmpID As String = ""
    Dim Level(2) As String
    Dim HREmpID As String = ""
    Dim HRLevel(2) As String
    Dim Separators() As Char = {":"c}
    Dim TrainersList As New StringBuilder()
 
    Private Sub LoadBlock(sessionid As Integer)
        Try
            Dim sdatatable As New DataTable
            sdatatable = Process.SearchData("Training_Session_Get_Trainers", sessionid)
            dlBlogs.DataSource = sdatatable
            dlBlogs.DataBind()

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then

                Process.LoadRadComboTextAndValueP2(cboapprover, "Emp_PersonalDetail_get_Superiors", Session("UserJobgrade"), Process.GetCompanyName, "name", "empid", False)

                Dim strDataSet As New DataSet

                If Request.QueryString("id") IsNot Nothing Then
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Training_Sessions_get", Request.QueryString("id"))

                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    atrainingsession.Value = strUser.Tables(0).Rows(0).Item("name").ToString
                    acourse.Value = strUser.Tables(0).Rows(0).Item("course").ToString
                    adate.Value = CDate(strUser.Tables(0).Rows(0).Item("scheduledtime")).ToLongDateString & " to " & CDate(strUser.Tables(0).Rows(0).Item("duedate")).ToLongDateString

                    adeliverymethod.Value = strUser.Tables(0).Rows(0).Item("deliverymethod").ToString
                    acoordinator.Value = strUser.Tables(0).Rows(0).Item("Coordinator").ToString
                    alocation.Value = strUser.Tables(0).Rows(0).Item("deliverylocation").ToString
                    atime.Value = Process.AMPM_Time(strUser.Tables(0).Rows(0).Item("trainingtime"))
                    aobjective.Value = strUser.Tables(0).Rows(0).Item("objectives").ToString

                    Dim strEmp As New DataSet
                    strEmp = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_PersonalDetail_get", Session("UserEmpID"))
                    If strEmp.Tables(0).Rows.Count > 0 Then
                        Process.AssignRadComboValue(cboapprover, strEmp.Tables(0).Rows(0).Item("SupervisorID").ToString)
                    End If
                    txtsessiontype.Text = strUser.Tables(0).Rows(0).Item("TrainingType").ToString
                    LoadBlock(txtid.Text)

                End If

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            Dim lblstatus As String = ""
            If (cboapprover.SelectedValue Is Nothing) Then
                lblstatus = "Select your HOD / Senior Colleague for Training Session Approval!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                cboapprover.Focus()
                Exit Sub
            End If

            If (areason.Value.Trim = "") Then
                lblstatus = "Reason for requesting training is required to aid approval process!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                areason.Focus()
                Exit Sub
            End If

            System.Threading.Thread.Sleep(300)

            Dim strUser As New DataSet
            strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "Select * from Employee_Training_Sessions where empid = '" & Session("UserEmpID") & "' and [sessions] = '" & atrainingsession.Value & "'")
            If strUser.Tables(0).Rows.Count > 0 Then
                lblstatus = "You are already scheduled to attend " & atrainingsession.Value & ", request is being truncated"
                Process.loadalert(divalert, msgalert, lblstatus, "info")
                Exit Sub
            End If

            strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "Select * from Training_Sessions_Request where empid = '" & Session("UserEmpID") & "' and TrainingSessionID = '" & txtid.Text & "'")
            If strUser.Tables(0).Rows.Count > 0 Then
                lblstatus = "You already have a pending request related to " & atrainingsession.Value & " that is awaiting approval"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                Exit Sub
            End If

            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Training_Sessions_Request_Update", txtid.Text, Session("UserEmpID"), cboapprover.SelectedValue, areason.Value)

            Process.Training_Emp_Request(atrainingsession.Value, acoordinator.Value, adate.Value, atime.Value, alocation.Value, "", Session("UserEmpID"), cboapprover.SelectedValue, Process.ApplicationURL + "/" + Process.GetMailLink(AuthenCode, 2))
            lblstatus = "Request sent for approval to " & cboapprover.SelectedItem.Text
            Process.loadalert(divalert, msgalert, lblstatus, "success")

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect("~/Module/Employee/TrainingPortal/AvailableTrainings", True)
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

 

    Protected Sub cboTrainer_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs)

    End Sub



   
End Class