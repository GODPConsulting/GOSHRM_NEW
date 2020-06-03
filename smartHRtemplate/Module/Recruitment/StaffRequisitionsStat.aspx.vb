Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
'HR View on Approval Requistion Detail

Public Class StaffRequisitionsStat
    Inherits System.Web.UI.Page
    Dim exits As New clsExit
    Dim AuthenCode As String = "STAFFREQUISITEFORM"
    Dim olddata(3) As String
    Private Sub LoadData(id As String)
        Dim strUser As New DataSet
        strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Job_Requisition_get", id)
        If strUser.Tables(0).Rows.Count > 0 Then
            lblid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
            ahodname.Value = strUser.Tables(0).Rows(0).Item("HODName").ToString
            ahodcomment.Value = strUser.Tables(0).Rows(0).Item("hodcomment").ToString
            ahoddate.Value = strUser.Tables(0).Rows(0).Item("HODApprovalDate").ToString
            ahodapproval.Value = strUser.Tables(0).Rows(0).Item("HODApproval").ToString
            lblfinalapprovalstat.Text = strUser.Tables(0).Rows(0).Item("finalstatus").ToString
            Session("ManagerID") = strUser.Tables(0).Rows(0).Item("HOD").ToString
            Session("HiringMgrID") = strUser.Tables(0).Rows(0).Item("HiringManager").ToString
            Session("Approver1ID") = strUser.Tables(0).Rows(0).Item("gmdid").ToString
            If CBool(strUser.Tables(0).Rows(0).Item("RequiresGMD").ToString.ToLower) = True Then
                Process.AssignRadComboValue(cboHigherEmployee, strUser.Tables(0).Rows(0).Item("gmdid").ToString)
                approvercomment.Value = strUser.Tables(0).Rows(0).Item("highercomment").ToString
                approverdate.Value = strUser.Tables(0).Rows(0).Item("GMDApprovalDate").ToString
                approval.Value = strUser.Tables(0).Rows(0).Item("GMDApproval").ToString
            End If
            chkHigherApproval.Checked = CBool(strUser.Tables(0).Rows(0).Item("RequiresGMD"))

            ahrname.Value = strUser.Tables(0).Rows(0).Item("HRMgrName").ToString
            ahrcomment.Value = strUser.Tables(0).Rows(0).Item("hrcomment").ToString
            ahrdate.Value = strUser.Tables(0).Rows(0).Item("HRApprovalDate").ToString
            Process.AssignRadComboValue(cbohrapproval, strUser.Tables(0).Rows(0).Item("HRApproval").ToString)
        End If
      
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                cbohrapproval.Items.Clear()
                cbohrapproval.Items.Add("Pending")
                cbohrapproval.Items.Add("Approved")
                cbohrapproval.Items.Add("Rejected")
                cbohrapproval.Items.Add("Cancelled")

                Process.LoadRadComboTextAndValueInitiate(cboHigherEmployee, "Emp_PersonalDetail_Get_Employees", "--Select Employee--", "name", "EmpID")
 
                If Request.QueryString("id") IsNot Nothing Then
                    LoadData(Request.QueryString("id"))
                End If
                If chkHigherApproval.Checked = True Then
                    divapprover.Visible = True
                Else
                    divapprover.Visible = False
                End If
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            Response.Write("<script language='javascript'> { self.close() }</script>")
        Catch ex As Exception

        End Try
    End Sub



    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            Dim lblstatus As String = ""
            Process.loadalert(divalert, msgalert, lblstatus, "warning")
            Dim strData As New DataSet
            strData = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Job_Requisition_get", lblid.Text)
            Dim jobtitle As String = ""
            Dim dept As String = ""
            Dim positions As Integer = 0
            Dim dateresume As Date
            Dim hodapproval As String = ""
            Dim hod As String = ""
            Dim initiatorid As String = ""

            If strData.Tables(0).Rows.Count > 0 Then
                jobtitle = strData.Tables(0).Rows(0).Item("Title").ToString
                dept = strData.Tables(0).Rows(0).Item("Dept").ToString
                positions = strData.Tables(0).Rows(0).Item("NoOfPositions").ToString
                dateresume = CDate(strData.Tables(0).Rows(0).Item("LastestResumption"))
                hodapproval = strData.Tables(0).Rows(0).Item("hodapproval")
                hod = strData.Tables(0).Rows(0).Item("hod")
                initiatorid = strData.Tables(0).Rows(0).Item("addedbyempid")
            End If

            If hodapproval <> "Approved" Then
                lblstatus = " Head Of Department yet to approve requisition!" & vbNewLine & "Save cancelled"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                Exit Sub
            End If

            Dim strHOD As New DataSet
            Dim hodemail As String = ""
            Dim hodname As String = ""
            Dim hiringmgrname As String = ""

            strHOD = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "select name, email from dbo.Employees_All where EmpID = '" & cboHigherEmployee.SelectedValue & "'")
            If strHOD.Tables(0).Rows.Count > 0 Then
                hodname = strHOD.Tables(0).Rows(0).Item("name").ToString
                hodemail = strHOD.Tables(0).Rows(0).Item("email").ToString
            End If

            If chkHigherApproval.Checked = True Then
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Job_Requisition_HR_Approval", lblid.Text, Session("UserEmpID"), cbohrapproval.SelectedItem.Text, ahrcomment.Value, chkHigherApproval.Checked, cboHigherEmployee.SelectedValue)
            Else
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Job_Requisition_HR_Approval", lblid.Text, Session("UserEmpID"), cbohrapproval.SelectedItem.Text, ahrcomment.Value, chkHigherApproval.Checked, "n/a")
            End If
            LoadData(Request.QueryString("id"))


            If chkHigherApproval.Checked = True And approval.Value.ToLower <> "approved" Then
                Process.requestedURL = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "menu_get_link", AuthenCode, 1)
                Process.Staff_Requisition_Alert_Approver(hiringmgrname, jobtitle, dept, positions, dateresume, "", Process.HRTeam, cboHigherEmployee.SelectedValue, Process.ApplicationURL & "/" & Process.requestedURL)
            End If
            lblstatus = "Status successfully updated!"
            Process.loadalert(divalert, msgalert, lblstatus, "success")
            'send mail to HR Members
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub chkHigherApproval_CheckedChanged(sender As Object, e As EventArgs) Handles chkHigherApproval.CheckedChanged

        Try
            If chkHigherApproval.Checked = True Then
                divapprover.Visible = True
                Process.AssignRadComboValue(cbohrapproval, "Pending")
                cbohrapproval.Enabled = False
            Else
                divapprover.Visible = False
                cbohrapproval.Enabled = True
            End If
        Catch ex As Exception

        End Try

    End Sub
End Class