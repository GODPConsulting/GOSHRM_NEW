Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Microsoft.Reporting.WebForms
Imports System.IO


Public Class TerminationUpdate
    Inherits System.Web.UI.Page
    Dim exits As New clsExit
    Dim AuthenCode As String = "EMPTERMINATE"
    Dim olddata(3) As String
    Dim emailFile As String = ConfigurationManager.AppSettings("EmailURL")
    Private Sub ResignTable(id As String)
        Dim dtEarning As New DataTable
        lnkappletter.InnerText = ""

        If cboExitType.SelectedValue.ToLower.Contains("resign") = True Then
            lnkappletter.InnerText = "ResignationAcceptance" & id & ".pdf"
        ElseIf cboExitType.SelectedValue.ToLower.Contains("death") = False Or cboExitType.SelectedValue.ToLower.Contains("abandon") = False Then
            lnkappletter.InnerText = "TerminationLetter" & id & ".pdf"
        End If

        If lnkappletter.InnerText <> "" Then
            dtEarning = Process.SearchData("Emp_Termination_Get", id)
            divappletter.Visible = True
            lblpath.Text = Server.MapPath(emailFile & lnkappletter.InnerText)
            GenerateResignLetter(dtEarning, Process.GetData("general_info_get"), Server.MapPath(emailFile & lnkappletter.InnerText))
        End If
       
       
    End Sub
    Private Sub GenerateResignLetter(dtearn As DataTable, logos As DataTable, ByVal savePath As String)
        Dim ReportViewer1 As New ReportViewer
        ReportViewer1.ProcessingMode = ProcessingMode.Local
        ReportViewer1.SizeToReportContent = True

        If cboExitType.SelectedValue.ToLower.Contains("resign") = True Then
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Module/Reports/Recruitment/ResignationAcceptance.rdlc")
        ElseIf cboExitType.SelectedValue.ToLower.Contains("death") = False Or cboExitType.SelectedValue.ToLower.Contains("abandon") = False Then
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Module/Reports/Recruitment/TerminationLetter.rdlc")
        End If

        Dim _rsource As New ReportDataSource("resign", dtearn)
        Dim _rsource2 As New ReportDataSource("logo", logos)
        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.LocalReport.DataSources.Add(_rsource)
        ReportViewer1.LocalReport.DataSources.Add(_rsource2)
        ReportViewer1.LocalReport.Refresh()
        Dim Bytes() As Byte = ReportViewer1.LocalReport.Render("PDF", "", Nothing, Nothing, Nothing, Nothing, Nothing)

        If File.Exists(savePath) Then
            File.Delete(savePath)
        End If

        Using Stream As New FileStream(savePath, FileMode.Create)
            Stream.Write(Bytes, 0, Bytes.Length)
        End Using
        Session("rptAttachment") = savePath
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            divhigherapproval2.Visible = False
            divhigherapproval.Visible = False
            divhighercomment.Visible = False

            If Not Me.IsPostBack Then
                cboHRApproval.Items.Clear()
                cboHRApproval.Items.Add("Pending")
                cboHRApproval.Items.Add("Approved")
                cboHRApproval.Items.Add("Cancelled")
                cboHRApproval.Items.Add("Rejected")

                lblcompany.Text = Session("company")
                Process.LoadRadComboTextAndValueInitiateP1(cboEmployee, "Emp_PersonalDetail_Get_Employees", Session("company"), "--select--", "Employee2", "EmpID")
                Process.LoadRadComboTextAndValue(cboExitType, "Emp_Exit_Type_Get_All", "Name", "Name", False)

                'Company_Structure_get_parent
                If Request.QueryString("id") IsNot Nothing Then
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Termination_Get", Request.QueryString("id"))
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    anoticedate.SelectedDate = CDate(strUser.Tables(0).Rows(0).Item("NoticeDate"))
                    aexitdate.SelectedDate = CDate(strUser.Tables(0).Rows(0).Item("TerminationDate"))
                    Process.LoadRadComboTextAndValueInitiateP2(cboManager, "Emp_PersonalDetail_get_Superiors", strUser.Tables(0).Rows(0).Item("grade").ToString, Process.GetCompanyName, "-- Forward To --", "Employee2", "EmpID")
                    Process.LoadRadComboTextAndValueP2(cboApproverII, "Emp_PersonalDetail_get_Superiors", strUser.Tables(0).Rows(0).Item("grade").ToString, Process.GetCompanyName, "Employee2", "EmpID", True)

                    areason.Value = strUser.Tables(0).Rows(0).Item("Reason").ToString
                    acomment.Value = strUser.Tables(0).Rows(0).Item("hrcomment").ToString
                    Process.AssignRadComboValue(cboEmployee, strUser.Tables(0).Rows(0).Item("empid").ToString)
                    Process.AssignRadComboValue(cboExitType, strUser.Tables(0).Rows(0).Item("ExitType").ToString)
                    Process.AssignRadComboValue(cboHRApproval, strUser.Tables(0).Rows(0).Item("HRApproval").ToString)
                    Process.AssignRadComboValue(cboApproverII, strUser.Tables(0).Rows(0).Item("approver2").ToString)
                    Process.AssignRadComboValue(cboManager, strUser.Tables(0).Rows(0).Item("MgrID").ToString)
                    aseniorapproval.Value = strUser.Tables(0).Rows(0).Item("SupervisorApproval").ToString
                    cboEmployee.Enabled = False
                    aseniorapproval.Value = strUser.Tables(0).Rows(0).Item("SupervisorApproval").ToString
                    aseniorcomment.Value = strUser.Tables(0).Rows(0).Item("supervisorcomment").ToString.Replace(vbCrLf, "<br />")
                    ahigherapproval.Value = strUser.Tables(0).Rows(0).Item("Approval2").ToString
                    ahighercomment.Value = strUser.Tables(0).Rows(0).Item("ApproverComment2").ToString.Replace(vbCrLf, "<br />")

                    If cboHRApproval.SelectedItem.Text.ToLower <> "pending" Then
                        btnotify.Visible = True
                    Else
                        btnotify.Visible = False
                    End If

                    If cboHRApproval.SelectedItem.Text.ToLower = "approved" Then
                        If cboExitType.SelectedValue.ToLower.Contains("resign") = True Then
                            ResignTable(txtid.Text)
                        ElseIf cboExitType.SelectedValue.ToLower.Contains("death") = False Or cboExitType.SelectedValue.ToLower.Contains("abandon") = False Then
                            ResignTable(txtid.Text)
                        End If

                    End If
                Else
                    btnotify.Visible = False
                    txtid.Text = "0"
                    divterminalbenefit.Visible = False
                    ahigherapproval.Value = "Pending"
                    divhigherapproval.Visible = False
                    divhighercomment.Visible = False
                    divseniorapproval.Visible = False
                    divseniorcomment.Visible = False
                End If

            End If

            'If ahigherapproval.Value <> "" Then
            '    divhigherapproval2.Visible = True
            '    divhigherapproval.Visible = True
            '    divhighercomment.Visible = True

            'End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            Dim msgbuild As New StringBuilder()
            Dim Separators() As Char = {";"c}
            Dim lblstatus As String = ""
            If txtid.Text <> "0" Then
                If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False Then
                    Process.loadalert(divalert, msgalert, Process.privilegemsg, "warning")
                    Exit Sub
                End If
            End If

            If aseniorapproval.Value.ToUpper <> "APPROVED" And cboHRApproval.SelectedItem.Text.ToUpper = "APPROVED" Then
                lblstatus = aseniorapproval.Value & " 's Approval required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                Exit Sub
            End If
            If ahigherapproval.Value.ToUpper <> "APPROVED" And cboHRApproval.SelectedItem.Text.ToUpper = "APPROVED" Then
                lblstatus = cboApproverII.SelectedValue & " 's Approval required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                Exit Sub
            End If
            If (anoticedate.SelectedDate Is Nothing) Then
                lblstatus = "Date of Notice required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                anoticedate.Focus()
                Exit Sub
            End If

            If (aexitdate.SelectedDate Is Nothing) Then
                lblstatus = "Date of Exit required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                aexitdate.Focus()
                Exit Sub
            End If

            If CDate(Process.DDMONYYYY(anoticedate.SelectedDate)) > CDate(Process.DDMONYYYY(aexitdate.SelectedDate)) Then
                lblstatus = "Exit Date cannot be earlier than Notice Date!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                anoticedate.Focus()
            End If

            Dim OldValue As String = ""
            Dim NewValue As String = ""

            Dim j As Integer = 0

            Dim forwardemail As String = ""
            Dim forwardname As String = ""

            Dim initatorname As String = ""
            Dim initatoremail As String = ""
            Dim initatorposition As String = ""
            Dim initatordept As String = ""

            Dim strEmp As New DataSet
            strEmp = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_PersonalDetail_get", cboManager.SelectedValue)
            If strEmp.Tables(0).Rows.Count > 0 Then
                forwardemail = strEmp.Tables(0).Rows(0).Item("Office Email").ToString
                forwardname = strEmp.Tables(0).Rows(0).Item("fullname").ToString
            End If

            strEmp = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_PersonalDetail_get", cboEmployee.SelectedValue)
            If strEmp.Tables(0).Rows.Count > 0 Then
                initatoremail = strEmp.Tables(0).Rows(0).Item("Office Email").ToString
                initatorname = strEmp.Tables(0).Rows(0).Item("fullname").ToString
                initatorposition = strEmp.Tables(0).Rows(0).Item("Jobtitle").ToString
                initatordept = strEmp.Tables(0).Rows(0).Item("Office").ToString
            End If

            If cboApproverII.SelectedValue.ToLower = "n/a" Then
                ahigherapproval.Value = "Approved"
            End If



            If txtid.Text <> "0" And txtid.Text <> "" Then
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Termination_Update", txtid.Text, cboEmployee.SelectedValue, cboManager.SelectedValue, Process.DDMONYYYY(anoticedate.SelectedDate), Process.DDMONYYYY(aexitdate.SelectedDate), areason.Value, cboExitType.SelectedItem.Value, acomment.Value, cboHRApproval.SelectedItem.Text, Session("UserEmpID"), cboApproverII.SelectedValue)

            Else
                txtid.Text = GetIdentity(txtid.Text, cboEmployee.SelectedValue, cboManager.SelectedValue, Process.DDMONYYYY(anoticedate.SelectedDate), Process.DDMONYYYY(aexitdate.SelectedDate), areason.Value, cboExitType.SelectedItem.Value, acomment.Value, cboHRApproval.SelectedItem.Text, Session("UserEmpID"))
                If txtid.Text = "0" Then
                    lblstatus = Process.strExp
                    Exit Sub
                End If
                Process.Exit_From_HR(Process.DDMONYYYY(aexitdate.SelectedDate), cboExitType.SelectedItem.Text, areason.Value, Process.GetMailList("hr"), cboEmployee.SelectedValue, cboManager.SelectedValue, Process.ApplicationURL() + "/" + Process.GetMailLink(AuthenCode, 1))
            End If

            lblstatus = "Record updated"
            Process.loadalert(divalert, msgalert, lblstatus, "success")
            If cboHRApproval.SelectedItem.Text.ToLower <> "pending" Then
                btnotify.Visible = True
            Else
                btnotify.Visible = False
            End If

            If cboHRApproval.SelectedItem.Text.ToLower = "approved" And cboExitType.SelectedValue.ToLower.Contains("resign") = True Then
                ResignTable(txtid.Text)
            End If
            'Response.Write("<script language='javascript'> { self.close() }</script>")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Function GetIdentity(ByVal id As Integer, ByVal empid As String, ByVal mgrid As String, ByVal noticedate As Date, ByVal exitdate As Date, ByVal reason As String, ByVal exittype As String, ByVal comment As String, ByVal status As String, ByVal userempid As String) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Emp_Termination_Update"
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@EmpID", SqlDbType.VarChar).Value = empid
            cmd.Parameters.Add("@MgrID", SqlDbType.VarChar).Value = mgrid
            cmd.Parameters.Add("@Noticedate", SqlDbType.Date).Value = noticedate
            cmd.Parameters.Add("@Terminationdate", SqlDbType.Date).Value = exitdate
            cmd.Parameters.Add("@Reason", SqlDbType.VarChar).Value = reason
            cmd.Parameters.Add("@ExitType", SqlDbType.VarChar).Value = exittype
            cmd.Parameters.Add("@Comment", SqlDbType.VarChar).Value = comment
            cmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = status
            cmd.Parameters.Add("@UserEmpID", SqlDbType.VarChar).Value = userempid
            cmd.Parameters.Add("@approver2", SqlDbType.VarChar).Value = cboApproverII.SelectedValue
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception
            Process.strExp = ex.Message
            Return 0
        End Try
    End Function
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            'Response.Redirect("terminations", True)
            Response.Redirect("~/Module/Recruitment/terminations.aspx?", True)

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub



    Protected Sub lnkTerminalBenefit_Click(sender As Object, e As EventArgs)
        Try
            Dim lblstatus As String = ""
            Dim strUser As New DataSet
            strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Termination_Get", txtid.Text)
            Dim hrapproval As String = strUser.Tables(0).Rows(0).Item("supervisorapproval").ToString
            'Dim supapproval As String = ""
            If hrapproval.ToLower <> "approved" Then
                lblstatus = "Termination has not be formally approved, please approve for Benefits to be generated"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                Exit Sub
            End If

            Dim url As String = "EmployeeTerminalBenefit.aspx?empid=" & cboEmployee.SelectedValue
            'lblstatus = "Termination has not be formally approved, please approve for Benefits to be generated"
            'Process.loadalert(divalert, msgalert, lblstatus, "success")
            'Dim s As String = "window.open('" & url + "', 'popup_window', 'width=900,height=600,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no');"
            'ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)

            Response.Redirect("~/Module/Recruitment/EmployeeTerminalBenefit.aspx?empid=" & cboEmployee.SelectedValue, True)

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Protected Sub cboEmployee_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboEmployee.SelectedIndexChanged
        Try
            Dim strEmp As New DataSet
            strEmp = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_PersonalDetail_get", cboEmployee.SelectedValue)
            If strEmp.Tables(0).Rows.Count > 0 Then
                Process.LoadRadComboTextAndValueP2(cboManager, "Emp_PersonalDetail_get_Superiors", strEmp.Tables(0).Rows(0).Item("grade").ToString, Process.GetCompanyName, "name", "EmpID", True)
                Process.LoadRadComboTextAndValueP2(cboApproverII, "Emp_PersonalDetail_get_Superiors", strEmp.Tables(0).Rows(0).Item("grade").ToString, Process.GetCompanyName, "name", "EmpID", True)


                Process.AssignRadComboValue(cboManager, strEmp.Tables(0).Rows(0).Item("supervisorid").ToString)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnNotify_Click(sender As Object, e As EventArgs)
        Try
            Dim forwardemail As String = ""
            Dim forwardname As String = ""

            Dim initatorname As String = ""
            Dim initatoremail As String = ""
            Dim initatorposition As String = ""
            Dim initatordept As String = ""

            Dim strEmp As New DataSet
            strEmp = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_PersonalDetail_get", cboManager.SelectedValue)
            If strEmp.Tables(0).Rows.Count > 0 Then
                forwardemail = strEmp.Tables(0).Rows(0).Item("Office Email").ToString
                forwardname = strEmp.Tables(0).Rows(0).Item("fullname").ToString
            End If

            strEmp = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_PersonalDetail_get", cboEmployee.SelectedValue)
            If strEmp.Tables(0).Rows.Count > 0 Then
                initatoremail = strEmp.Tables(0).Rows(0).Item("Office Email").ToString
                initatorname = strEmp.Tables(0).Rows(0).Item("fullname").ToString
                initatorposition = strEmp.Tables(0).Rows(0).Item("Jobtitle").ToString
                initatordept = strEmp.Tables(0).Rows(0).Item("Office").ToString
            End If

            If cboApproverII.SelectedValue.ToLower = "n/a" Then
                ahigherapproval.Value = "Approved"
            End If
            If aseniorapproval.Value.ToLower <> "pending" Then
                Process.Exit_Approval_By_HR(Process.DDMONYYYY(aexitdate.SelectedDate), cboHRApproval.SelectedItem.Text, Date.Now, acomment.Value, Process.GetMailList("hr"), cboEmployee.SelectedValue, cboManager.SelectedValue, Process.ApplicationURL() + "/" + Process.GetMailLink(AuthenCode, 3), Process.ApplicationURL() + "/" + Process.GetMailLink(AuthenCode, 1), lblpath.Text)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkLetter_Click(sender As Object, e As EventArgs)
        Try

            Process.downloadFile(lblpath.Text)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnNotifyApprover_Click(sender As Object, e As EventArgs)
        Try
            Dim lblstatus As String = ""
            Process.loadalert(divalert, msgalert, lblstatus, "warning")
            If cboManager.SelectedItem.Text = "" Or cboManager.SelectedItem.Text.ToLower = "n/a" Then
                lblstatus = "No Higher Approver selected!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")

                Exit Sub
            Else
                If cboApproverII.SelectedValue = cboManager.SelectedValue Then
                    lblstatus = "Higher Approver already approved exit as first approver, select another Approver!"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    Exit Sub
                End If

                If cboManager.SelectedValue <> "" Then
                    Process.Exit_For_HOD_Approval(Process.DDMONYYYY(aexitdate.SelectedDate), cboExitType.SelectedValue, areason.Value, cboEmployee.SelectedValue, cboManager.SelectedValue, Process.ApplicationURL() + "/" + Process.GetMailLink(AuthenCode, 1))
                End If

                If cboApproverII.SelectedValue <> "" Then
                    Process.Exit_For_HOD_Approval(Process.DDMONYYYY(aexitdate.SelectedDate), cboExitType.SelectedValue, areason.Value, cboEmployee.SelectedValue, cboApproverII.SelectedValue, Process.ApplicationURL() + "/" + Process.GetMailLink(AuthenCode, 1))
                End If
                'Process.Exit_For_HOD_Approval(Process.DDMONYYYY(aexitdate.Value), cboExitType.SelectedValue, areason.Value, cboEmployee.SelectedValue, cboApproverII.SelectedValue, Process.ApplicationURL() + "/" + Process.GetMailLink(AuthenCode, 1))cboManager
                'Process.Exit_For_HOD_Approval(Process.DDMONYYYY(aexitdate.Value), cboExitType.SelectedValue, areason.Value, cboEmployee.SelectedValue, cboManager.SelectedValue, Process.ApplicationURL() + "/" + Process.GetMailLink(AuthenCode, 1))
                lblstatus = "Approver Notification sent!"
                Process.loadalert(divalert, msgalert, lblstatus, "success")
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Protected Sub chkHigherApproval_CheckedChanged(sender As Object, e As EventArgs) Handles chkHigherApproval2.CheckedChanged
        Try
            If chkHigherApproval2.Checked = True Then
                divhigherapproval2.Visible = True
                divhigherapproval.Visible = True
                divhighercomment.Visible = True
            Else
                divhigherapproval2.Visible = False
                divhigherapproval.Visible = False
                divhighercomment.Visible = False
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class