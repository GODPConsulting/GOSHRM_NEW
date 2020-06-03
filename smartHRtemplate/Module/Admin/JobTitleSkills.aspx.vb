Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Telerik.Web.UI

Public Class JobTitleSkills
    Inherits System.Web.UI.Page
    Dim jobtitle As New clsJobTitle
    Dim AuthenCode As String = "Job"
    Dim olddata(3) As String
 
 
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                If Request.QueryString("id") IsNot Nothing Then
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Job_Title_Skills_Get", Request.QueryString("id"))
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    aname.Value = strUser.Tables(0).Rows(0).Item("skills").ToString
                    adesc.Value = strUser.Tables(0).Rows(0).Item("description").ToString
                    txtjobid.Text = strUser.Tables(0).Rows(0).Item("jobtitleid").ToString
                    aweight.Value = strUser.Tables(0).Rows(0).Item("rating").ToString
                Else
                    aweight.Value = "0"
                    txtid.Text = "0"
                    txtjobid.Text = Request.QueryString("jobid")
                End If

                Dim strjob As New DataSet
                strjob = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Job_Titles_get", txtjobid.Text)
                If strjob.Tables(0).Rows.Count > 0 Then
                    pagetitle.InnerText = strjob.Tables(0).Rows(0).Item("name").ToString & " Skills"
                End If
            End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Private Function GetIdentity() As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Job_Title_Skills_Update"
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = txtid.Text
            cmd.Parameters.Add("@jobtitleid", SqlDbType.Int).Value = txtjobid.Text
            cmd.Parameters.Add("@value", SqlDbType.VarChar).Value = aname.Value
            cmd.Parameters.Add("@description", SqlDbType.VarChar).Value = adesc.Value
            cmd.Parameters.Add("@rating", SqlDbType.Decimal).Value = aweight.Value
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
            Dim url As String = "JobTitleUpdate?id=" & txtjobid.Text
            Response.Redirect(url, True)
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            Dim lblstatus As String = ""
            If aname.Value.Trim = "" Then
                lblstatus = "Skill required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                aname.Focus()
                Exit Sub
            End If

            If IsNumeric(aweight.Value) = False Then
                lblstatus = "Weight required or value is invalid!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                aweight.Focus()
                Exit Sub
            End If

            If txtid.Text = "0" Then
                txtid.Text = GetIdentity()
                If txtid.Text = "0" Then
                    Exit Sub
                End If
            Else
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Job_Title_Skills_Update", txtid.Text, txtjobid.Text, aname.Value.Trim, adesc.Value, aweight.Value)
            End If

            lblstatus = aname.Value & " saved!"
            Process.loadalert(divalert, msgalert, lblstatus, "success")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
End Class