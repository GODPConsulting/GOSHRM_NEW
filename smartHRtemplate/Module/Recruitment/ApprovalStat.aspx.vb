Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO


Public Class ApprovalStat
    Inherits System.Web.UI.Page
    Dim exits As New clsExit
    Dim AuthenCode As String = "ADMPROMOTION"
    Dim AuthenCode2 As String = "ADMSUCCESSION"
    Dim olddata(3) As String
    Private Sub LoadControls(id As String)
        Dim strUser As New DataSet

        If Request.QueryString("stat") = "promotion" Then
            strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruitment_Promotion_Get", id)
        ElseIf Request.QueryString("stat") = "succession" Then
            strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruitment_Succession_Get", id)
        End If
        'empName
        pagetitle.InnerText = strUser.Tables(0).Rows(0).Item("empName").ToString & " (" & Process.loadtype.ToUpper & ")"
        lblempid.Text = strUser.Tables(0).Rows(0).Item("empid").ToString

        lblid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
        lblfinalapprovalstat.Text = strUser.Tables(0).Rows(0).Item("finalstatus").ToString
        Process.AssignRadComboValue(cboapprover1, strUser.Tables(0).Rows(0).Item("approver1").ToString)
        'approver1date.Value  
        'approver1stat.value
        'approver1comment.value
        'cboapprover1
        approver1comment.Value = strUser.Tables(0).Rows(0).Item("approvercomment1").ToString
        If IsDate(strUser.Tables(0).Rows(0).Item("approverdate1")) Then
            approver1date.Value = CDate(strUser.Tables(0).Rows(0).Item("approverdate1")).ToLongDateString
        End If

        approver1stat.Value = strUser.Tables(0).Rows(0).Item("approverstatus1").ToString

        Process.AssignRadComboValue(cboApprover2, strUser.Tables(0).Rows(0).Item("approver2").ToString)
        approver2comment.Value = strUser.Tables(0).Rows(0).Item("approvercomment2").ToString

        If IsDate(strUser.Tables(0).Rows(0).Item("approverdate2")) Then
            approver2date.Value = CDate(strUser.Tables(0).Rows(0).Item("approverdate2")).ToLongDateString
        End If
        approver2stat.Value = strUser.Tables(0).Rows(0).Item("approverstatus2").ToString

        Process.AssignRadComboValue(cboapprover3, strUser.Tables(0).Rows(0).Item("approver3").ToString)
        approver3comment.Value = strUser.Tables(0).Rows(0).Item("approvercomment3").ToString

        If IsDate(strUser.Tables(0).Rows(0).Item("approverdate3")) Then
            approver3date.Value = CDate(strUser.Tables(0).Rows(0).Item("approverdate3")).ToLongDateString
        End If
        approver3stat.Value = strUser.Tables(0).Rows(0).Item("approverstatus3").ToString

        If Request.QueryString("stat") = "promotion" Then
            lblinitiator.Text = strUser.Tables(0).Rows(0).Item("initiatorid").ToString
            lbljobgrade.Text = strUser.Tables(0).Rows(0).Item("jobgrade").ToString
            lbljobgradeold.Text = strUser.Tables(0).Rows(0).Item("oldjobgrade").ToString
            lbljobtitle.Text = strUser.Tables(0).Rows(0).Item("Jobtitle").ToString
            lbljobtitleold.Text = strUser.Tables(0).Rows(0).Item("oldJobtitle").ToString
        Else
            lbljobgrade.Text = strUser.Tables(0).Rows(0).Item("plannedjobgrade").ToString
            lbljobgradeold.Text = strUser.Tables(0).Rows(0).Item("jobgrade").ToString
            lbljobtitle.Text = strUser.Tables(0).Rows(0).Item("plannedJobtitle").ToString
            lbljobtitleold.Text = strUser.Tables(0).Rows(0).Item("Jobtitle").ToString
        End If


        If approver1stat.Value.ToUpper = "APPROVED" And cboapprover1.SelectedValue.ToUpper <> "N/A" Then
            cboapprover1.Enabled = False
        End If
        If approver2stat.Value.ToUpper = "APPROVED" And cboapprover2.SelectedValue.ToUpper <> "N/A" Then
            cboapprover2.Enabled = False
        End If
        If approver3stat.Value.ToUpper = "APPROVED" And cboapprover3.SelectedValue.ToUpper <> "N/A" Then
            cboapprover3.Enabled = False
        End If


        If lblfinalapprovalstat.Text = "Approved" Then
            btnupdate.Disabled = True
        End If

        If cboApprover2.SelectedValue = "n/a" Then
            divapprover2.Visible = False
        End If

        If cboapprover3.SelectedValue = "n/a" Then
            divapprover3.Visible = False

        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                'Company_Structure_get_parent
                Process.LoadRadComboTextAndValueP1(cboapprover1, "Emp_PersonalDetail_Get_Employees", Session("Access"), "name", "EmpID", True)
                Process.LoadRadComboTextAndValueP1(cboapprover2, "Emp_PersonalDetail_Get_Employees", Session("Access"), "name", "EmpID", True)
                Process.LoadRadComboTextAndValueP1(cboapprover3, "Emp_PersonalDetail_Get_Employees", Session("Access"), "name", "EmpID", True)

                If Request.QueryString("id") IsNot Nothing Then
                    LoadControls(Request.QueryString("id"))
                End If

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            Dim id As String = ""
            'If Request.QueryString("id") IsNot Nothing Then
            '    id = Request.QueryString("id")
            'End If
            'If Request.QueryString("stat") = "promotion" Then
            '    Dim url As String = "promotionsupdate.aspx?id=" + id + ""
            '    Response.Redirect(url, True)
            'ElseIf Request.QueryString("stat") = "succession" Then
            '    Dim url As String = "successionupdate.aspx?id=" + id + ""
            '    Response.Redirect(url, True)
            'End If
            Response.Write("<script language='javascript'> { self.close() }</script>")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub



    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            Dim lblstatus As String = ""
            System.Threading.Thread.Sleep(300)
            If cboapprover1.SelectedValue = lblempid.Text Then
                lblstatus = "affected employee cannot be included as an approver!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                cboapprover1.Focus()
                Exit Sub
            End If

            If cboapprover2.SelectedValue = lblempid.Text Then
                lblstatus = "affected employee cannot be included as an approver!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                cboapprover2.Focus()
                Exit Sub
            End If

            If cboapprover3.SelectedValue = lblempid.Text Then
                lblstatus = "affected employee cannot be included as an approver!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                cboapprover3.Focus()
                Exit Sub
            End If


            If cboapprover2.SelectedValue Is Nothing Then
                Process.AssignRadComboValue(cboapprover2, "N/A")
            End If

            If cboapprover3.SelectedValue Is Nothing Then
                Process.AssignRadComboValue(cboapprover3, "N/A")
            End If



            If cboapprover1.SelectedValue = cboapprover2.SelectedValue And cboapprover1.SelectedValue.ToUpper <> "N/A" Then
                lblstatus = "An employee cannot be selected more than once as Approver!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                cboapprover2.Focus()
                Exit Sub
            End If

            If cboapprover1.SelectedValue = cboapprover3.SelectedValue And cboapprover1.SelectedValue.ToUpper <> "N/A" Then
                lblstatus = "An employee cannot be selected more than once as Approver!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                cboapprover3.Focus()
                Exit Sub
            End If


            If cboapprover2.SelectedValue = cboapprover3.SelectedValue And cboapprover2.SelectedValue.ToUpper <> "N/A" Then
                lblstatus = "An employee cannot be selected more than once as Approver!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                cboapprover3.Focus()
                Exit Sub
            End If


            If Request.QueryString("stat") = "promotion" Then
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruitment_Promotion_Approvers_Update", lblid.Text, cboapprover1.SelectedValue, cboapprover2.SelectedValue, cboapprover3.SelectedValue, Session("LoginID"))
                If cboapprover1.SelectedValue.ToLower <> "n/a" Then
                    Process.Staff_Promotion_Approver_Notification(lbljobtitleold.Text, lbljobgradeold.Text, lbljobtitle.Text, lbljobgrade.Text, lblempid.Text, lblinitiator.Text, cboapprover1.SelectedValue, Process.ApplicationURL + "/" + Process.GetMailLink(AuthenCode, 1))
                ElseIf cboapprover2.SelectedValue.ToLower <> "n/a" Then
                    Process.Staff_Promotion_Approver_Notification(lbljobtitleold.Text, lbljobgradeold.Text, lbljobtitle.Text, lbljobgrade.Text, lblempid.Text, lblinitiator.Text, cboapprover2.SelectedValue, Process.ApplicationURL + "/" + Process.GetMailLink(AuthenCode, 1))
                End If
            ElseIf Request.QueryString("stat") = "succession" Then
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruitment_Succession_Approvers_Update", lblid.Text, cboapprover1.SelectedValue, cboapprover2.SelectedValue, cboapprover3.SelectedValue, Session("LoginID"))
                If cboapprover1.SelectedValue.ToLower <> "n/a" Then
                    Process.Staff_Succession_Approver_Notification(lbljobtitleold.Text, lbljobgradeold.Text, lbljobtitle.Text, lbljobgrade.Text, lblempid.Text, lblinitiator.Text, cboapprover1.SelectedValue, Process.ApplicationURL + "/" + Process.GetMailLink(AuthenCode2, 1))
                ElseIf cboapprover2.SelectedValue.ToLower <> "n/a" Then
                    Process.Staff_Succession_Approver_Notification(lbljobtitleold.Text, lbljobgradeold.Text, lbljobtitle.Text, lbljobgrade.Text, lblempid.Text, lblinitiator.Text, cboapprover2.SelectedValue, Process.ApplicationURL + "/" + Process.GetMailLink(AuthenCode2, 1))
                End If
            End If

            LoadControls(Request.QueryString("id"))

            lblstatus = "approvers data updated!"
            Process.loadalert(divalert, msgalert, lblstatus, "success")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
End Class