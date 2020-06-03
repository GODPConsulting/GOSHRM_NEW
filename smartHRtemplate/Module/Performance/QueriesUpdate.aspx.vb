Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO


Public Class QueriesUpdate
    Inherits System.Web.UI.Page
    Dim exits As New clsExit
    Dim AuthenCode As String = "QUERIES"
    Dim olddata(3) As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                cborecommendation.Items.Clear()
                cborecommendation.Items.Add("--Select Recommendation--")
                cborecommendation.Items.Add("Suspension")
                cborecommendation.Items.Add("Dismissal")
                cborecommendation.Items.Add("Warning")
                cborecommendation.Items.Add("Justified")
                cborecommendation.Items.Add("Settled")

                'Company_Structure_get_parent
                If Request.QueryString("id") IsNot Nothing Then
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Employee_Query_Get", Request.QueryString("id"))
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    lblQueryDate.Value = strUser.Tables(0).Rows(0).Item("QueryDate")
                    lblexpecteddate.Value = strUser.Tables(0).Rows(0).Item("ExpectedResponseDate")
                    lblquery.Value = strUser.Tables(0).Rows(0).Item("ROQuery").ToString

                    lblEmpName.Value = strUser.Tables(0).Rows(0).Item("employee").ToString
                    lblissuer.Value = strUser.Tables(0).Rows(0).Item("ReportingOfficer").ToString

                    lblEmployeeResponse.Value = strUser.Tables(0).Rows(0).Item("EmpComment").ToString
                    lblEmpDate.Value = strUser.Tables(0).Rows(0).Item("EmpResponseDate").ToString
                    lblEmpStatus.Value = strUser.Tables(0).Rows(0).Item("EmpStatus").ToString
                    lblquerystatus.Value = strUser.Tables(0).Rows(0).Item("ROStatus").ToString

                    lblrorersponse.Value = strUser.Tables(0).Rows(0).Item("ROComment").ToString

                    txtHRComment.Value = strUser.Tables(0).Rows(0).Item("HRComment").ToString
                    Process.AssignRadComboValue(cborecommendation, strUser.Tables(0).Rows(0).Item("HRAction").ToString)
                    lblexpectedtime.Value = strUser.Tables(0).Rows(0).Item("ExpectedResponseTime").ToString
                    lblupdatedon.Value = strUser.Tables(0).Rows(0).Item("HRDate").ToString
                End If

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try

            Dim OldValue As String = ""
            Dim NewValue As String = ""

            Dim j As Integer = 0

            Dim forwardemail As String = ""
            Dim forwardname As String = ""
            Dim jobtitle As String = ""
            Dim jobgrade As String = ""
            Dim office As String = ""
            Dim strEmp As New DataSet
            strEmp = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "select a.JobTitle,a.Grade,a.Office,a.Email ,a.Name     from dbo.Employees_All a where a.EmpID = '" & cborecommendation.SelectedItem.Value & "'")
            If strEmp.Tables(0).Rows.Count > 0 Then
                forwardemail = strEmp.Tables(0).Rows(0).Item("Email").ToString
                forwardname = strEmp.Tables(0).Rows(0).Item("Name").ToString
                jobtitle = strEmp.Tables(0).Rows(0).Item("JobTitle").ToString
                jobgrade = strEmp.Tables(0).Rows(0).Item("Grade").ToString
                office = strEmp.Tables(0).Rows(0).Item("Office").ToString
            End If

            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Employee_Query_UpdateStatus_HR", txtid.Text, txtHRComment.Value, cborecommendation.SelectedItem.Text, Session("UserEmpID"))

            Process.loadalert(divalert, msgalert, "Record updated", "success")
            'Response.Write("<script language='javascript'> { self.close() }</script>")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Function GetIdentity(ByVal id As Integer, ByVal empid As String, ByVal jobtitle As String, ByVal jobgrade As String, ByVal office As String, ByVal querydate As Date, ByVal strquery As String, ByVal userempid As String, ByVal expecteddate As Date, ByVal expectedtime As String) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Performance_Employee_Query_Update_RO"
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@EmpID", SqlDbType.VarChar).Value = empid
            cmd.Parameters.Add("@JobTilte", SqlDbType.VarChar).Value = jobtitle
            cmd.Parameters.Add("@JobGrade", SqlDbType.VarChar).Value = jobgrade
            cmd.Parameters.Add("@Office", SqlDbType.VarChar).Value = office
            cmd.Parameters.Add("@QueryDate", SqlDbType.Date).Value = querydate
            cmd.Parameters.Add("@Query", SqlDbType.VarChar).Value = strquery
            cmd.Parameters.Add("@UserID", SqlDbType.VarChar).Value = userempid
            cmd.Parameters.Add("@expecteddate", SqlDbType.VarChar).Value = expecteddate
            cmd.Parameters.Add("@expecttime", SqlDbType.VarChar).Value = expectedtime

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
            Response.Redirect("queries")

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub





    'Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles btnNotification.Click
    '    Try
    '        Dim forwardemail As String = ""
    '        Dim forwardname As String = ""
    '        Dim jobtitle As String = ""
    '        Dim jobgrade As String = ""
    '        Dim office As String = ""

    '        Dim initiator As String = ""
    '        Dim initiator2 As String = ""

    '        Dim strEmp As New DataSet
    '        strEmp = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "select a.JobTitle,a.Grade,a.Office,a.Email ,a.Name     from dbo.Employees_All a where a.EmpID = '" & cborecommendation.SelectedItem.Value & "'")
    '        If strEmp.Tables(0).Rows.Count > 0 Then
    '            forwardemail = strEmp.Tables(0).Rows(0).Item("Email").ToString
    '            forwardname = strEmp.Tables(0).Rows(0).Item("Name").ToString
    '            jobtitle = strEmp.Tables(0).Rows(0).Item("JobTitle").ToString
    '            jobgrade = strEmp.Tables(0).Rows(0).Item("Grade").ToString
    '            office = strEmp.Tables(0).Rows(0).Item("Office").ToString
    '        End If


    '        strEmp = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "select a.Name, a.Employee2 Employee     from dbo.Employees_All a where a.EmpID = '" & Session("UserEmpID") & "'")
    '        If strEmp.Tables(0).Rows.Count > 0 Then
    '            initiator = strEmp.Tables(0).Rows(0).Item("Employee").ToString
    '            initiator2 = strEmp.Tables(0).Rows(0).Item("Name").ToString
    '        End If

    '        If Process.Query_Notification(forwardemail, forwardname, initiator, datNotice.SelectedDate, datExpectedDate.SelectedDate, Process.ConvertCtrlToTime(radHourStart.SelectedValue, radMinStart.SelectedValue, radTimeStart.SelectedValue), Process.GetMailList("hr")) = True Then
    '            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Query notification successfully sent to " & forwardname + "')", True)
    '            lblstatus.Text = "Query notification successfully sent to " & forwardname
    '        Else
    '            lblstatus.Text = Process.strExp
    '            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & lblstatus.Text + "')", True)
    '        End If


    '    Catch ex As Exception
    '        lblstatus.Text = ex.Message
    '        ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & lblstatus.Text + "')", True)
    '    End Try
    'End Sub

   

    Protected Sub btnNotify_Click(sender As Object, e As EventArgs) Handles btnNotify.Click
        Try
            Process.Query_Recommendation_HR(txtid.Text, Process.ApplicationURL + "/" + Process.GetMailLink(AuthenCode, 4))
            Process.Query_Recommendation(txtid.Text, Process.ApplicationURL + "/" + Process.GetMailLink(AuthenCode, 1), Process.ApplicationURL + "/" + Process.GetMailLink(AuthenCode, 2))
            Process.loadalert(divalert, msgalert, "Notification sent", "success")
        Catch ex As Exception

        End Try
    End Sub
End Class