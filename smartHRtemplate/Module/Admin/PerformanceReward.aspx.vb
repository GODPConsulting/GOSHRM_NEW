Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Telerik.Web.UI

Public Class PerformanceReward
    Inherits System.Web.UI.Page
    Dim jobtitle As New clsJobTitle
    Dim AuthenCode As String = "cboskill"
    Dim olddata(3) As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then


                If Request.QueryString("id") IsNot Nothing Then
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "emp_Reward_Performance_get", Request.QueryString("id"))
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    alowerscorerange.Value = strUser.Tables(0).Rows(0).Item("LowerScore")
                    aupperscorerange.Value = strUser.Tables(0).Rows(0).Item("HigherScore")
                    txtcourseid.Text = strUser.Tables(0).Rows(0).Item("rewardid").ToString
                    aweight.Value = strUser.Tables(0).Rows(0).Item("Percentage").ToString
                Else
                    txtid.Text = "0"
                    txtcourseid.Text = Request.QueryString("rewardid")
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
            cmd.CommandText = "Performance_reward_update"
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = txtid.Text
            cmd.Parameters.Add("@rewardid", SqlDbType.Int).Value = txtcourseid.Text
            cmd.Parameters.Add("@lowerscore", SqlDbType.VarChar).Value = alowerscorerange.Value
            cmd.Parameters.Add("@higherscore", SqlDbType.VarChar).Value = aupperscorerange.Value
            cmd.Parameters.Add("@percentage", SqlDbType.Decimal).Value = aweight.Value
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
            Try
                Dim url As String = "reward?id=" & txtcourseid.Text
                Response.Redirect(url, True)
            Catch ex As Exception

            End Try
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
            Return 0
        End Try
    End Function


    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            Dim url As String = "reward?id=" & txtcourseid.Text
            Response.Redirect(url, True)
        Catch ex As Exception

        End Try
    End Sub





    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            Dim lblstatus As String = ""

            If (alowerscorerange.Value Is Nothing) Then
                lblstatus = "please input lower score range!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                alowerscorerange.Focus()
                Exit Sub
            End If
            If (IsNumeric(alowerscorerange.Value) = False) Then
                lblstatus = "range should be in numbers"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                alowerscorerange.Focus()
            End If
            If (aupperscorerange.Value Is Nothing) Then
                lblstatus = "please input lower score range!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                aupperscorerange.Focus()
                Exit Sub
            End If
            If (IsNumeric(aupperscorerange.Value) = False) Then
                lblstatus = "range should be in numbers"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                aupperscorerange.Focus()
            End If

            If (aweight.Value Is Nothing) Then
                lblstatus = "Skill weight required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                aweight.Focus()
                Exit Sub
            End If

            If (IsNumeric(aweight.Value) = False) Then
                lblstatus = "Skill weight invalid!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                aweight.Focus()
                Exit Sub
            End If

            If txtid.Text = "0" Then
                txtid.Text = GetIdentity()
                If txtid.Text = "0" Then
                    Exit Sub
                End If
            Else
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_reward_update", txtid.Text, txtcourseid.Text, alowerscorerange.Value.Trim, aupperscorerange.Value.Trim, aweight.Value)
            End If

            lblstatus = "Record saved!"
            Process.loadalert(divalert, msgalert, lblstatus, "success")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
End Class