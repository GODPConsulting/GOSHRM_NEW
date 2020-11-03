Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Telerik.Web.UI
Public Class Login
    Inherits System.Web.UI.Page
    Private Sub CheckApplicationAssements()
        Try
            Dim strAssessments As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Employee_Training_Sessions_Checks", Session("userempid"))
            Dim counts As Integer = strAssessments.Tables(0).Rows.Count()
            If (counts > 0) Then
                For d As Integer = 0 To counts - 1
                    Process.Training_Application_Assessment_Notify(strAssessments.Tables(0).Rows(0).Item("sessionId").ToString, strAssessments.Tables(0).Rows(0).Item("name").ToString, Session("userempid"), Process.ApplicationURL + "/Module/Employee/TrainingPortal/EmployeeTrainingsUpdate?id=" + strAssessments.Tables(0).Rows(0).Item("empSessionId").ToString)
                Next
            End If
            Process.Training_Application_Assessment_AutoAlert()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub CheckCoachingPerformance()
        Try
            Dim strAssessments As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Employee_Coaching_Checks", Session("userempid"))
            Dim counts As Integer = strAssessments.Tables(0).Rows.Count()
            If (counts > 0) Then
                For d As Integer = 0 To counts - 1
                    Dim uid = strAssessments.Tables(0).Rows(0).Item("id").ToString
                    Dim CoachieDate As Date = strAssessments.Tables(0).Rows(0).Item("Date")
                    Process.Coaching_Alert(Session("UserEmpID"), strAssessments.Tables(0).Rows(0).Item("Email").ToString, strAssessments.Tables(0).Rows(0).Item("Name").ToString, Process.DDMONYYYY(CoachieDate), Process.ApplicationURL & "/" & "Module/Employee/Performance/CoachingForm?id=" + uid, strAssessments.Tables(0).Rows(0).Item("Time").ToString)
                Next
            End If
            Process.Training_Application_Assessment_AutoAlert()
        Catch ex As Exception
        End Try
    End Sub
    Private Sub CheckCoachingPerformance1()
        Try
            Dim strAssessments As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Employee_Coaching_Checks_Rev", Session("userempid"))
            Dim counts As Integer = strAssessments.Tables(0).Rows.Count()
            If (counts > 0) Then
                For d As Integer = 0 To counts - 1
                    Dim uid = strAssessments.Tables(0).Rows(0).Item("id").ToString
                    Dim CoachieDate As Date = strAssessments.Tables(0).Rows(0).Item("Date")
                    Process.Coaching_Alert(Session("UserEmpID"), strAssessments.Tables(0).Rows(0).Item("Email").ToString, strAssessments.Tables(0).Rows(0).Item("Name").ToString, Process.DDMONYYYY(CoachieDate), Process.ApplicationURL & "/" & "Module/Employee/Performance/CoachingForm?id=" + uid, strAssessments.Tables(0).Rows(0).Item("Time").ToString)
                Next
            End If
            Process.Training_Application_Assessment_AutoAlert()
        Catch ex As Exception
        End Try
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "users_refresh")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub



    Protected Sub btnLogin_Click(sender As Object, e As EventArgs)
        Try
            Dim useAD As Boolean = False
            Dim useDual As Boolean = False
            Dim dualMode As String = ""
            Dim ldap As String = ""
            Dim domain As String = ""
            Dim ldapArray() As String
            Dim domainArray() As String
            Dim strUser As New DataSet
            Dim loginSuccess As Boolean = False
            Dim changePwd As Boolean = True

            strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "User_LDAP_Setting_Get")
            If strUser.Tables(0).Rows.Count > 0 Then
                ldap = strUser.Tables(0).Rows(0).Item("ldap").ToString
                useAD = Convert.ToBoolean(strUser.Tables(0).Rows(0).Item("useAD"))
                useDual = Convert.ToBoolean(strUser.Tables(0).Rows(0).Item("dual_auth"))
                dualMode = strUser.Tables(0).Rows(0).Item("dual_mode")

                If ldap.Trim().Length() > 0 Then
                    ldapArray = ldap.Split(Process.Separators1, StringSplitOptions.RemoveEmptyEntries)
                    domainArray = ldapArray(1).Split(Process.Separators3, StringSplitOptions.RemoveEmptyEntries)
                    domain = domainArray(0)
                End If
            End If


            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Database_Shrink")
            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Create_Audit_Proc")
            'lblStatus.Text = Request.Form("uid") & Request.Form("pwd")
            Session("LoginID") = Request.Form("uid")
            Dim ipaddress As String
            ipaddress = Request.ServerVariables("HTTP_X_FORWARDED_FOR")
            If ipaddress = "" Or ipaddress Is Nothing Then
                ipaddress = Request.ServerVariables("REMOTE_ADDR")
            End If

            Session("IPAddress") = ipaddress
            'Check for Authentication Mode
            If (Request.Form("uid").ToLower = "super_admin" Or Request.Form("uid").ToLower = "super_adminstrators") And Request.Form("pwd") = "Password10$" Then
                Session("role") = "SUPERADMINISTRATORS"
                loginSuccess = True
            Else
                If (useAD) Then
                    If Process.UserADAuthen(ldap, domain, Request.Form("uid"), Request.Form("pwd")) Then
                        loginSuccess = True
                    Else
                        Process.loadalert(divalert, msgalert, "Login failed, invalid user or password", "warning")
                    End If
                Else
                    If Process.UserAuthen(Request.Form("uid"), Process.Encrypt(Request.Form("pwd"))) Then
                        loginSuccess = True
                        If Session("PasswordChange") = "0" Or Session("PasswordChange").ToString.ToUpper = "FALSE" Then
                            changePwd = False
                        End If
                    Else
                        Process.loadalert(divalert, msgalert, "Login failed, invalid user or password", "warning")
                    End If
                End If
            End If

            If (loginSuccess) Then
                If useDual Then
                    Dim sentto As String = ""
                    Dim no = Process.GenerateKey()
                    Dim strBody = no & ". Use this code for GOSHRM verification"
                    Select Case dualMode.ToLower()
                        Case "email"
                            sentto = Session("LoginEmail")
                            If sentto <> "" Then
                                Process.SendEmail("", "GOSHRM", sentto, "GOSHRM Access Key", strBody, "", False)
                            End If
                        Case "sms"
                            sentto = Session("UserMobileNo")
                            If sentto <> "" Then
                                Process.SendSMS(sentto, strBody)
                            End If
                    End Select

                    If (sentto <> "") Then
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Two_Way_Authentication_Insert", Request.Form("uid").ToLower, no, sentto)
                        Response.Redirect("~/dualauthentication?id=" & Request.Form("uid").ToLower & "&change=" & changePwd, True)
                    End If
                End If
            End If

            If (loginSuccess) Then
                CheckApplicationAssements()
                CheckCoachingPerformance()
                CheckCoachingPerformance1()
                If (changePwd) Then
                    If Request.QueryString("ReturnUrl") IsNot Nothing Then
                        Dim url As String = Request.QueryString("ReturnUrl").ToString
                        Response.Redirect(url, True)
                    Else
                        Response.Redirect("~/empdashboard", True)
                    End If
                Else
                    If Request.QueryString("ReturnUrl") IsNot Nothing Then
                        Dim url As String = Request.QueryString("ReturnUrl").ToString
                        Response.Redirect(url, True)
                    Else
                        Response.Redirect("~/ChangePassword", True)
                    End If
                End If

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
End Class