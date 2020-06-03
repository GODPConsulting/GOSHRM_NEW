Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO


Public Class StaffRequisitionStat
    Inherits System.Web.UI.Page
    Dim exits As New clsExit
    Dim AuthenCode As String = "APPSTAFFREQUISITE"
    Dim olddata(3) As String
    'Approval Page for Non-HR Staff
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                cboaehodapproval.Items.Clear()
                cboaehodapproval.Items.Add("Pending")
                cboaehodapproval.Items.Add("Approved")
                cboaehodapproval.Items.Add("Rejected")
                cboaehodapproval.Items.Add("Cancelled")

                cboapproverstat.Items.Clear()
                cboapproverstat.Items.Add("Pending")
                cboapproverstat.Items.Add("Approved")
                cboapproverstat.Items.Add("Rejected")
                cboapproverstat.Items.Add("Cancelled")


                'Company_Structure_get_parent
                If Request.QueryString("id") IsNot Nothing Then
                    MultiView1.ActiveViewIndex = 0
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Job_Requisition_get", Request.QueryString("id"))
                    lblid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    avhodname.Value = strUser.Tables(0).Rows(0).Item("HODName").ToString
                    avhodcomment.Value = strUser.Tables(0).Rows(0).Item("hodcomment").ToString
                    avhoddate.Value = strUser.Tables(0).Rows(0).Item("HODApprovalDate").ToString
                    avhodapproval.Value = strUser.Tables(0).Rows(0).Item("HiringManager").ToString
                    lblmgrid.Text = strUser.Tables(0).Rows(0).Item("gmdid").ToString

                    If CBool(strUser.Tables(0).Rows(0).Item("RequiresGMD").ToString.ToLower) = True Then
                        vapprovername.Value = strUser.Tables(0).Rows(0).Item("GMD").ToString
                        vapprovercomment.Value = strUser.Tables(0).Rows(0).Item("highercomment").ToString
                        If IsDate(strUser.Tables(0).Rows(0).Item("GMDApprovalDate")) Then
                            vapproverdate.Value = CDate(strUser.Tables(0).Rows(0).Item("GMDApprovalDate")).ToLongDateString
                        End If

                        vapproval.Value = strUser.Tables(0).Rows(0).Item("GMDApproval").ToString
                        divapprover.Visible = True
                    Else
                        divapprover.Visible = False

                    End If


                    'If strUser.Tables(0).Rows(0).Item("HRManager").ToString.ToLower = "n/a" Or strUser.Tables(0).Rows(0).Item("HRManager").ToString.ToLower = "" Then
                    '    divhrview.Visible = False

                    'Else
                    '    vapprovername.Value = strUser.Tables(0).Rows(0).Item("HRMgrName").ToString
                    '    vapprovercomment.Value = strUser.Tables(0).Rows(0).Item("hrcomment").ToString
                    '    If IsDate(strUser.Tables(0).Rows(0).Item("HRApprovalDate")) Then
                    '        vapproverdate.Value = CDate(strUser.Tables(0).Rows(0).Item("HRApprovalDate")).ToLongDateString
                    '    End If
                    '    vapproval.Value = strUser.Tables(0).Rows(0).Item("HRApproval").ToString
                    'End If

                    If strUser.Tables(0).Rows(0).Item("HRManager").ToString.ToLower = "n/a" Or strUser.Tables(0).Rows(0).Item("HRManager").ToString.ToLower = "" Then
                        divhrview.Visible = False

                    Else
                        avhrname.Value = strUser.Tables(0).Rows(0).Item("HRMgrName").ToString
                        avhrcomment.Value = strUser.Tables(0).Rows(0).Item("hrcomment").ToString
                        If IsDate(strUser.Tables(0).Rows(0).Item("HRApprovalDate")) Then
                            avhrdate.Value = CDate(strUser.Tables(0).Rows(0).Item("HRApprovalDate")).ToLongDateString
                        End If              
                        avhrapproval.Value = strUser.Tables(0).Rows(0).Item("HRApproval").ToString
                    End If


                    'index 2
                    aehodname.Value = strUser.Tables(0).Rows(0).Item("HODName").ToString
                    aehodcomment.Value = strUser.Tables(0).Rows(0).Item("hodcomment").ToString
                    If IsDate(strUser.Tables(0).Rows(0).Item("HODApprovalDate")) Then
                        aehoddate.Value = CDate(strUser.Tables(0).Rows(0).Item("HODApprovalDate")).ToLongDateString
                    End If

                    Process.AssignRadComboValue(cboaehodapproval, strUser.Tables(0).Rows(0).Item("HODApproval").ToString)

                    
                    If CBool(strUser.Tables(0).Rows(0).Item("RequiresGMD").ToString.ToLower) = True Then
                        aeapprovername.Value = strUser.Tables(0).Rows(0).Item("GMD").ToString
                        aeappcomment.Value = strUser.Tables(0).Rows(0).Item("highercomment").ToString
                        If IsDate(strUser.Tables(0).Rows(0).Item("GMDApprovalDate")) Then
                            aeapproverdate.Value = CDate(strUser.Tables(0).Rows(0).Item("GMDApprovalDate")).ToLongDateString
                        End If

                        Process.AssignRadComboValue(cboapproverstat, strUser.Tables(0).Rows(0).Item("GMDApproval").ToString)
                        diveapprover2.Visible = True
                    Else
                        'diveapprover2.Visible = False
                    End If

                    If strUser.Tables(0).Rows(0).Item("HRManager").ToString.ToLower = "n/a" Or strUser.Tables(0).Rows(0).Item("HRManager").ToString.ToLower = "" Then

                        divehr.Visible = False
                    Else
                        aehrname.Value = strUser.Tables(0).Rows(0).Item("HRMgrName").ToString
                        aehrcomment.Value = strUser.Tables(0).Rows(0).Item("hrcomment").ToString
                        aehrdate.Value = strUser.Tables(0).Rows(0).Item("HRApprovalDate").ToString
                        aehrapproval.Value = strUser.Tables(0).Rows(0).Item("HRApproval").ToString
                        If IsDate(strUser.Tables(0).Rows(0).Item("HRApprovalDate")) Then
                            aehrdate.Value = CDate(strUser.Tables(0).Rows(0).Item("HRApprovalDate")).ToLongDateString
                        End If
                    End If
                    lblhodid.Text = strUser.Tables(0).Rows(0).Item("hod").ToString
                    lblmgrid.Text = strUser.Tables(0).Rows(0).Item("GMDID").ToString

                    If strUser.Tables(0).Rows(0).Item("GMDID").ToString = Session("UserEmpID") Or strUser.Tables(0).Rows(0).Item("hod").ToString = Session("UserEmpID") Then
                        MultiView1.ActiveViewIndex = 1
                        If strUser.Tables(0).Rows(0).Item("GMDID").ToString = Session("UserEmpID") Then
                            aehodcomment.Attributes.Add("readonly", "readonly")
                            cboaehodapproval.Enabled = False
                        End If

                        If strUser.Tables(0).Rows(0).Item("hod").ToString = Session("UserEmpID") Then
                            aeappcomment.Attributes.Add("readonly", "readonly")
                            cboapproverstat.Enabled = False
                        End If
                    Else
                        btnupdate.Visible = False
                    End If

                    If strUser.Tables(0).Rows(0).Item("finalstatus").ToString.ToLower = "approved" Then
                        cboapproverstat.Enabled = False
                        cboaehodapproval.Enabled = False
                    End If
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
            Dim strData As New DataSet
            strData = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Job_Requisition_get", lblid.Text)
            Dim jobtitle As String = ""
            Dim dept As String = ""
            Dim positions As Integer = 0
            Dim hiringmgrname As String = ""
            Dim hiringmgrid As String = ""
            Dim initiatorid As String = ""
            Dim dateresume As Date
            If strData.Tables(0).Rows.Count > 0 Then
                jobtitle = strData.Tables(0).Rows(0).Item("Title").ToString
                dept = strData.Tables(0).Rows(0).Item("Dept").ToString
                positions = strData.Tables(0).Rows(0).Item("NoOfPositions").ToString
                dateresume = strData.Tables(0).Rows(0).Item("LastestResumption")
                hiringmgrid = strData.Tables(0).Rows(0).Item("hiringmanager")
                initiatorid = strData.Tables(0).Rows(0).Item("addedbyempid")
            End If

            Dim strHOD As New DataSet
            strHOD = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "select name from dbo.Employees_All where EmpID = '" & Session("HiringMgrID") & "'")
            If strHOD.Tables(0).Rows.Count > 0 Then
                hiringmgrname = strHOD.Tables(0).Rows(0).Item("name").ToString
            End If

            If Session("UserEmpID") = lblhodid.Text Then
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Job_Requisition_HOD_Approval", lblid.Text, cboaehodapproval.SelectedItem.Text, aehodcomment.Value)
                lblstatus = "Approval Status updated"
                Process.loadalert(divalert, msgalert, lblstatus, "success")
                If cboaehodapproval.SelectedItem.Text = "Approved" Then
                    Dim hodemail As String = ""
                    Dim hodname As String = ""


                    Process.requestedURL = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "menu_get_link", AuthenCode, 1)
                    Process.Staff_Requisition_Alert_Approvals("hod", cboaehodapproval.SelectedItem.Text, aehodcomment.Value, hiringmgrname, Session("UserEmpID"), jobtitle, dept, positions, dateresume, "", Process.ApplicationURL & "/" & Process.requestedURL)
                End If
            End If

            If Session("UserEmpID") = lblmgrid.Text Then
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Job_Requisition_GMD_Approval", lblid.Text, cboapproverstat.SelectedItem.Text, aeappcomment.Value)
                lblstatus = "Approval Status updated"
                Process.loadalert(divalert, msgalert, lblstatus, "success")
                Dim higherapprover As String = ""

                strHOD = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "select name from dbo.Employees_All where EmpID = '" & lblmgrid.Text & "'")
                If strHOD.Tables(0).Rows.Count > 0 Then
                    higherapprover = strHOD.Tables(0).Rows(0).Item("name").ToString
                End If

                'send mail to HR Members
                'Process.Top_Staff_Requisition_Alert_Reply(Process.GetMailList("hr"), lblhodname.Text, jobtitle, cboapproverstat.SelectedText, "", Session("HiringMgrID"), lblmgrid.Text, lblhodid.Text)
                Process.requestedURL = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "menu_get_link", AuthenCode, 1)
                Process.Staff_Requisition_Alert_Approvals("", cboapproverstat.SelectedItem.Text, aehodcomment.Value, hiringmgrname, higherapprover, jobtitle, dept, positions, dateresume, "", Process.ApplicationURL & "/" & Process.requestedURL)
                If cboapproverstat.SelectedItem.Text = "approved" Then
                    Process.Staff_Requisition_Approvals_Complete(hiringmgrname, jobtitle, dept, positions, dateresume, initiatorid, hiringmgrid, Process.GetMailLink(AuthenCode, 1), Process.GetMailLink(AuthenCode, 2))
                End If

            End If

            'send mail to HR Members
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
End Class