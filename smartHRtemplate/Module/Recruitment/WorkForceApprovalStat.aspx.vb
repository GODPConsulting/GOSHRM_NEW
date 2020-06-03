Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO


Public Class WorkForceApprovalStat
    Inherits System.Web.UI.Page
    Dim exits As New clsExit
    Dim olddata(3) As String
    Dim AuthenCode As String = "WFBUDGET"
    Private Sub LoadDatas(sid As String)
        Try
            Dim strUser As New DataSet
            strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_WorkForce_Budget_Get", sid)
            lblempid.Text = strUser.Tables(0).Rows(0).Item("empid").ToString
            lblid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
            lbldept.Text = strUser.Tables(0).Rows(0).Item("office").ToString
            lblbudget.Text = strUser.Tables(0).Rows(0).Item("budgetyear").ToString
            lblfinalapprovalstat.Text = strUser.Tables(0).Rows(0).Item("finalstatus").ToString
            Process.AssignRadComboValue(cboApprover1, strUser.Tables(0).Rows(0).Item("approver1").ToString)

            approver1comment.Value = strUser.Tables(0).Rows(0).Item("approver1comment").ToString
            approver1date.Value = strUser.Tables(0).Rows(0).Item("approver1date").ToString
            approver1stat.Value = strUser.Tables(0).Rows(0).Item("approver1status").ToString

            Process.AssignRadComboValue(cboApprover2, strUser.Tables(0).Rows(0).Item("approver2").ToString)
            approver2comment.Value = strUser.Tables(0).Rows(0).Item("approver2comment").ToString
            approver2date.Value = strUser.Tables(0).Rows(0).Item("approver2date").ToString
            approver2stat.Value = strUser.Tables(0).Rows(0).Item("approver2status").ToString

            Process.AssignRadComboValue(cboApprover3, strUser.Tables(0).Rows(0).Item("approver3").ToString)
            approver3comment.Value = strUser.Tables(0).Rows(0).Item("approver3comment").ToString
            approver3date.Value = strUser.Tables(0).Rows(0).Item("approver3date").ToString
            approver3stat.Value = strUser.Tables(0).Rows(0).Item("approver3status").ToString

            Process.AssignRadComboValue(cboApprover4, strUser.Tables(0).Rows(0).Item("approver4").ToString)
            approver4comment.Value = strUser.Tables(0).Rows(0).Item("approver4comment").ToString
            approver4date.Value = strUser.Tables(0).Rows(0).Item("approver4date").ToString
            approver4stat.Value = strUser.Tables(0).Rows(0).Item("approver4status").ToString

            If approver1stat.Value.ToUpper = "APPROVED" And cboApprover1.SelectedValue.ToUpper <> "N/A" Then
                cboApprover1.Enabled = False
            End If
            If approver2stat.Value.ToUpper = "APPROVED" And cboApprover2.SelectedValue.ToUpper <> "N/A" Then
                cboApprover2.Enabled = False
            End If
            If approver3stat.Value.ToUpper = "APPROVED" And cboApprover3.SelectedValue.ToUpper <> "N/A" Then
                cboApprover3.Enabled = False
            End If
            If approver4stat.Value.ToUpper = "APPROVED" And cboApprover4.SelectedValue.ToUpper <> "N/A" Then
                cboApprover4.Enabled = False
            End If

            If lblfinalapprovalstat.Text = "Approved" Then
                btupdate.Disabled = True
            End If

            If cboApprover2.SelectedValue = "n/a" Then
                divapprover2.Visible = False
            End If

            If cboApprover3.SelectedValue = "n/a" Then
                divapprover3.Visible = False
            End If

            If cboApprover4.SelectedValue = "n/a" Then
                divapprover4.Visible = False
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                If Session("clicked") Is Nothing Then
                    Session("clicked") = "0"
                End If

                'Company_Structure_get_parent
                Process.LoadRadComboTextAndValueP1(cboApprover1, "Emp_PersonalDetail_Get_Employees", Session("Access"), "name", "EmpID", True)
                Process.LoadRadComboTextAndValueP1(cboApprover2, "Emp_PersonalDetail_Get_Employees", Session("Access"), "name", "EmpID", True)
                Process.LoadRadComboTextAndValueP1(cboApprover3, "Emp_PersonalDetail_Get_Employees", Session("Access"), "name", "EmpID", True)
                Process.LoadRadComboTextAndValueP1(cboApprover4, "Emp_PersonalDetail_Get_Employees", Session("Access"), "name", "EmpID", True)
                If Request.QueryString("id") IsNot Nothing Then
                    LoadDatas(Request.QueryString("id"))
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
            If cboApprover1.SelectedValue = lblempid.Text Then
                lblstatus = "Sorry you cannot approve Work-Force Budget you initiated yourself!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                cboApprover1.Focus()
                Exit Sub
            End If

            If cboApprover2.SelectedValue = lblempid.Text Then
                lblstatus = "Sorry you cannot approve Work-Force Budget you initiated yourself!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                cboApprover2.Focus()
                Exit Sub
            End If

            If cboApprover3.SelectedValue = lblempid.Text Then
                lblstatus = "Sorry you cannot approve Work-Force Budget you initiated yourself!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                cboApprover3.Focus()
                Exit Sub
            End If

            If cboApprover4.SelectedValue = lblempid.Text Then
                lblstatus = "Sorry you cannot approve Work-Force Budget you initiated yourself!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                cboApprover4.Focus()
                Exit Sub
            End If
            If cboApprover2.SelectedValue Is Nothing Then
                Process.AssignRadComboValue(cboApprover2, "N/A")
            End If

            If cboApprover3.SelectedValue Is Nothing Then
                Process.AssignRadComboValue(cboApprover3, "N/A")
            End If

            If cboApprover4.SelectedValue Is Nothing Then
                Process.AssignRadComboValue(cboApprover4, "N/A")
            End If

            If cboApprover1.SelectedValue = cboApprover2.SelectedValue And cboApprover1.SelectedValue.ToUpper <> "N/A" Then
                lblstatus = "An employee cannot be selected more than once as Approver!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                cboApprover2.Focus()
                Exit Sub
            End If

            If cboApprover1.SelectedValue = cboApprover3.SelectedValue And cboApprover1.SelectedValue.ToUpper <> "N/A" Then
                lblstatus = "An employee cannot be selected more than once as Approver!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                cboApprover3.Focus()
                Exit Sub
            End If

            If cboApprover1.SelectedValue = cboApprover4.SelectedValue And cboApprover1.SelectedValue.ToUpper <> "N/A" Then
                lblstatus = "An employee cannot be selected more than once as Approver!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                cboApprover4.Focus()
                Exit Sub
            End If

            If cboApprover2.SelectedValue = cboApprover3.SelectedValue And cboApprover2.SelectedValue.ToUpper <> "N/A" Then
                lblstatus = "An employee cannot be selected more than once as Approver!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                cboApprover3.Focus()
                Exit Sub
            End If

            If cboApprover2.SelectedValue = cboApprover4.SelectedValue And cboApprover2.SelectedValue.ToUpper <> "N/A" Then
                lblstatus = "An employee cannot be selected more than once as Approver!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                cboApprover4.Focus()
                Exit Sub
            End If

            If cboApprover3.SelectedValue = cboApprover4.SelectedValue And cboApprover3.SelectedValue.ToUpper <> "N/A" Then
                lblstatus = "An employee cannot be selected more than once as Approver!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                cboApprover4.Focus()
                Exit Sub
            End If

            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_WorkForce_Budget_Approvers_Update", lblid.Text, cboApprover1.SelectedValue, cboApprover2.SelectedValue, cboApprover3.SelectedValue, cboApprover4.SelectedValue, Session("LoginID"))

            Dim strUser As New DataSet
            strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_WorkForce_Budget_Get", Request.QueryString("id"))

            LoadDatas(Request.QueryString("id"))
            Dim sForceType As String = ""
            If Session("clicked") = 1 Then
                sForceType = "Budget"
            Else
                sForceType = "Plan"
            End If

            If approver1stat.Value.ToLower <> "approved" Then
                strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_WorkForce_Budget_Get", Request.QueryString("id"))
                If strUser.Tables(0).Rows.Count > 0 Then
                    Process.requestedURL = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "menu_get_link", AuthenCode, Session("clicked"))
                    If cboApprover1.SelectedValue.Trim.ToLower <> "n/a" Then
                        Dim new_url As String = Process.ApplicationURL & "/" & Process.requestedURL
                        Process.Work_Force_To_Approvers(sForceType, lblbudget.Text, lbldept.Text, cboApprover1.SelectedValue, new_url)
                    End If
                End If
            ElseIf approver2stat.Value.ToLower <> "approved" And approver1stat.Value.ToLower = "approved" Then
                strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_WorkForce_Budget_Get", Request.QueryString("id"))
                If strUser.Tables(0).Rows.Count > 0 Then
                    Process.requestedURL = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "menu_get_link", AuthenCode, Session("clicked"))
                    If cboApprover2.SelectedValue.Trim.ToLower <> "n/a" Then
                        Dim new_url As String = Process.ApplicationURL & "/" & Process.requestedURL
                        Process.Work_Force_To_Approvers(sForceType, lblbudget.Text, lbldept.Text, cboApprover2.SelectedValue, new_url)
                    End If
                End If
            ElseIf approver3stat.Value.ToLower <> "approved" And approver2stat.Value.ToLower = "approved" Then
                strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_WorkForce_Budget_Get", Request.QueryString("id"))
                If strUser.Tables(0).Rows.Count > 0 Then
                    Process.requestedURL = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "menu_get_link", AuthenCode, Session("clicked"))
                    If cboApprover3.SelectedValue.Trim.ToLower <> "n/a" Then
                        Dim new_url As String = Process.ApplicationURL & "/" & Process.requestedURL
                        Process.Work_Force_To_Approvers(sForceType, lblbudget.Text, lbldept.Text, cboApprover3.SelectedValue, new_url)
                    End If
                End If
            ElseIf approver4stat.Value.ToLower <> "approved" And approver3stat.Value.ToLower = "approved" Then
                strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_WorkForce_Budget_Get", Request.QueryString("id"))
                If strUser.Tables(0).Rows.Count > 0 Then
                    Process.requestedURL = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "menu_get_link", AuthenCode, Session("clicked"))
                    If cboApprover4.SelectedValue.Trim.ToLower <> "n/a" Then
                        Dim new_url As String = Process.ApplicationURL & "/" & Process.requestedURL
                        Process.Work_Force_To_Approvers(sForceType, lblbudget.Text, lbldept.Text, cboApprover4.SelectedValue, new_url)
                    End If
                End If
            End If
            lblstatus = "Record saved!"
            Process.loadalert(divalert, msgalert, lblstatus, "success")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
End Class