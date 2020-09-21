Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO


Public Class ExamResultUpdate
    Inherits System.Web.UI.Page
    Dim cert As New clsCertification
    Dim olddata(3) As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                If Session("clicked") Is Nothing Then
                    Session("clicked") = "0"
                End If

                Process.LoadRadComboTextAndValue(cbograde, "Recruit_OLevel_Grade_Get_All", "name", "name", False)
                'Company_Structure_get_parent
                If Request.QueryString("id") IsNot Nothing Then
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Applicant_School_Leaving_Result_Get", Request.QueryString("id"))
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    Process.LoadRadComboTextAndValueInitiateP1(cboSubject, "Recruit_Applicant_School_Leaving_Result_Add", strUser.Tables(0).Rows(0).Item("applicantid").ToString, strUser.Tables(0).Rows(0).Item("subject").ToString, "name", "name")
                    Process.AssignRadComboValue(cboSubject, strUser.Tables(0).Rows(0).Item("subject").ToString)
                    Process.AssignRadComboValue(cbograde, strUser.Tables(0).Rows(0).Item("grade").ToString)
                    txtappid.Text = strUser.Tables(0).Rows(0).Item("applicantid").ToString
                Else
                    txtid.Text = "0"
                    txtappid.Text = Request.QueryString("appid")
                    Process.LoadRadComboTextAndValueP1(cboSubject, "Recruit_Applicant_School_Leaving_Result_Add", Request.QueryString("appid"), "name", "name", False)
                End If

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try

            'If Session("AppID") = "0" Then
            '    Session("AppID") = GetAppIdentity()
            'End If
            Dim lblstatus As String = ""
            Process.loadalert(divalert, msgalert, lblstatus, "success")

            If txtid.Text = "0" Or txtid.Text = "" Then
                txtid.Text = GetIdentity()
                If txtid.Text = "0" Then
                    Exit Sub
                End If
            Else
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Applicant_School_Leaving_Result_Update", txtid.Text, txtappid.Text, cboSubject.SelectedValue, cbograde.SelectedValue)
            End If
            Session("clicked") = "1"
            lblstatus = "Record saved"
            Process.loadalert(divalert, msgalert, lblstatus, "success")
            Response.Write("<script language='javascript'> { self.close() }</script>")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Function GetAppIdentity() As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Recruit_Applicant_Generate"
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
            Return 0
        End Try
    End Function
    Private Function GetIdentity() As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Recruit_Applicant_School_Leaving_Result_Update"
            cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = txtid.Text
            cmd.Parameters.Add("@appid", SqlDbType.VarChar).Value = txtappid.Text
            cmd.Parameters.Add("@subject", SqlDbType.VarChar).Value = cboSubject.SelectedValue
            cmd.Parameters.Add("@grade", SqlDbType.VarChar).Value = cbograde.SelectedValue
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
            Return 0
        End Try
    End Function
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            Response.Write("<script language='javascript'> { self.close() }</script>")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


End Class