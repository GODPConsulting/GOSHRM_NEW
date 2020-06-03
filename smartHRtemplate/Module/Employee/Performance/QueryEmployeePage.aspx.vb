Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO


Public Class QueryEmployeePage
    Inherits System.Web.UI.Page
    Dim exits As New clsExit
    Dim AuthenCode As String = "EMPQUERIES"
    Dim olddata(3) As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                cboApproval.Items.Clear()
                cboApproval.Items.Add("In-progress")
                cboApproval.Items.Add("Complete")


                'Company_Structure_get_parent
                If Request.QueryString("id") IsNot Nothing Then
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Employee_Query_Get", Request.QueryString("id"))
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    lbldate.Text = strUser.Tables(0).Rows(0).Item("QueryDate")
  

                    lblinitiator.Text = strUser.Tables(0).Rows(0).Item("ReportingOfficer").ToString

                    lblHeader.Text = "Query from " & lblinitiator.Text
                    lblQuery.Text = strUser.Tables(0).Rows(0).Item("ROQuery").ToString.Replace(vbCrLf, "<br />")
                    lblQueryDate.Text = strUser.Tables(0).Rows(0).Item("QueryDate").ToString
                    lblexpecteddate.Text = strUser.Tables(0).Rows(0).Item("ExpectedResponseDate").ToString
                    lblexpectedtime.Text = strUser.Tables(0).Rows(0).Item("ExpectedResponseTime").ToString
                    txtComment.Text = strUser.Tables(0).Rows(0).Item("EmpComment").ToString
                    lblEmpDate.Text = strUser.Tables(0).Rows(0).Item("EmpResponseDate").ToString
                    Process.AssignRadComboValue(cboApproval, strUser.Tables(0).Rows(0).Item("EmpStatus").ToString)
                    lblinitiatorid.Text = strUser.Tables(0).Rows(0).Item("ReportingOfficerID").ToString
                    lblhrcomment.Text = strUser.Tables(0).Rows(0).Item("hrcomment").ToString.Replace(vbCrLf, "<br />")
                    lblrecomm.Text = strUser.Tables(0).Rows(0).Item("HRAction").ToString
                    If cboApproval.SelectedValue.ToUpper = "COMPLETE" Then
                        Process.DisableButton(btnUpdateStatus)
                        Process.DisableButton(btnNotifyHR)
                    End If


                    If lblrecomm.Text <> "--Select Recommendation--" Then
                        'Process.DisableButton(btnUpdateStatus)
                        'Process.DisableButton(btnNotifyHR)

                        btnUpdateStatus.Visible = False
                        btnNotifyHR.Visible = False

                    End If
               
                End If

            End If
        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub


    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles btnUpdateStatus.Click
        Try
            If txtComment.Text.Trim = "" Then
                lblstatus.Text = "Response is required!"
                txtComment.Focus()
                Exit Sub
            End If

            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Employee_Query_UpdateStatus_Employee", txtid.Text, txtComment.Text, cboApproval.SelectedItem.Text)
            lblstatus.Text = "Record updated"
        Catch ex As Exception
            lblstatus.Text = ex.Message
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & lblstatus.Text + "')", True)
        End Try
    End Sub

    Protected Sub btnNotifyHR_Click(sender As Object, e As EventArgs) Handles btnNotifyHR.Click
        Try
            Dim forwardemail As String = ""
            Dim forwardname As String = ""
            Dim jobtitle As String = ""
            Dim jobgrade As String = ""
            Dim office As String = ""

            Dim initiator As String = ""
            Dim initiatorname As String = ""

            Dim strEmp As New DataSet
            strEmp = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "select a.JobTitle,a.Grade,a.Office,a.Email ,a.Name     from dbo.Employees_All a where a.EmpID = '" & Session("UserEmpID") & "'")
            If strEmp.Tables(0).Rows.Count > 0 Then
                forwardname = strEmp.Tables(0).Rows(0).Item("Name").ToString
            End If


            strEmp = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "select a.Name, a.Employee2 Employee,Email     from dbo.Employees_All a where a.EmpID = '" & lblinitiatorid.Text & "'")
            If strEmp.Tables(0).Rows.Count > 0 Then
                initiator = strEmp.Tables(0).Rows(0).Item("Email").ToString
                initiatorname = strEmp.Tables(0).Rows(0).Item("Name").ToString
            End If

            If cboApproval.SelectedItem.Text.ToUpper <> "COMPLETE" Then
                lblstatus.Text = "Complete Query before notifying " & initiatorname
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & lblstatus.Text & "')", True)
            Else

                If Process.Query_Notification_Reply(lbldate.Text, "", Session("UserEmpID"), lblinitiatorid.Text, Process.ApplicationURL + "/" + Process.GetMailLink(AuthenCode, 2)) = True Then
                    ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Query notification successfully sent to " & initiatorname & "')", True)
                    lblstatus.Text = "Query notification successfully sent to " & initiatorname
                Else
                    lblstatus.Text = Process.strExp
                    ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & lblstatus.Text + "')", True)
                End If
            End If

        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Try
            Response.Write("<script language='javascript'> { self.close() }</script>")

        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub
End Class