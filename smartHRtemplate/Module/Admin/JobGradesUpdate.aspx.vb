Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO


Public Class JobGradesUpdate
    Inherits System.Web.UI.Page
    Dim jobgrade As New clsJobGrade
    Dim AuthenCode As String = "JobGrade"
    Dim olddata(6) As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                Process.LoadRadComboTextAndValue(cboReportsTo, "Job_Grade_get_all", "Name", "Name", True)
                If Request.QueryString("id") IsNot Nothing Then
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Job_Grade_get", Request.QueryString("id"))
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    gradename.Value = strUser.Tables(0).Rows(0).Item("gradename").ToString
                    probation.Value = strUser.Tables(0).Rows(0).Item("ProbationPeriod").ToString
                    txtdescription.Value = strUser.Tables(0).Rows(0).Item("description").ToString
                    rankno.Value = strUser.Tables(0).Rows(0).Item("ranks").ToString

                    Process.AssignRadComboValue(cboReportsTo, strUser.Tables(0).Rows(0).Item("reportsto").ToString)
                Else
                    txtid.Text = "0"
                End If
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try

            If txtid.Text <> "0" Then
                If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False Then
                    ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
                    Exit Sub
                End If
            End If

            Dim lblstatus As String = ""
            If gradename.Value.Trim = "" Then
                lblstatus = "Grade Name required"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                gradename.Focus()
                Exit Sub
            End If

            If IsNumeric(probation.Value) = False Then
                lblstatus = "Probation required and must be a number!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                probation.Focus()
                Exit Sub
            End If

            If IsNumeric(rankno.Value) = False Then
                lblstatus = "Probation required and must be a number!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                rankno.Focus()
                Exit Sub
            End If



            

            'Old Data
            If txtid.Text <> "0" Then
                Dim strUser As New DataSet
                strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Job_Grade_get", txtid.Text)
                olddata(0) = strUser.Tables(0).Rows(0).Item("id").ToString
                olddata(1) = strUser.Tables(0).Rows(0).Item("GradeName").ToString
                olddata(2) = strUser.Tables(0).Rows(0).Item("ProbationPeriod").ToString
                olddata(3) = strUser.Tables(0).Rows(0).Item("description").ToString
                olddata(4) = strUser.Tables(0).Rows(0).Item("ranks").ToString
                olddata(5) = strUser.Tables(0).Rows(0).Item("reportsto").ToString
            End If


            If txtid.Text.Trim = "" Then
                jobgrade.id = 0
            Else
                jobgrade.id = txtid.Text
            End If
            jobgrade.Description = txtdescription.Value
            jobgrade.Grade = gradename.Value.Trim
            jobgrade.Probation = probation.Value
            jobgrade.Ranks = rankno.Value
            jobgrade.ReportsTo = cboReportsTo.SelectedValue
            Dim OldValue As String = ""
            Dim NewValue As String = ""

            Dim j As Integer = 0

            If txtid.Text <> "0" Then 'Updates
                For Each a In GetType(clsJobGrade).GetProperties()
                    If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If IsNumeric(a.GetValue(jobgrade, Nothing)) = True And IsNumeric(olddata(j)) = True Then
                                If CDbl(a.GetValue(jobgrade, Nothing)) <> CDbl(olddata(j)) Then
                                    NewValue += a.Name + ": " + a.GetValue(jobgrade, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            Else
                                If a.GetValue(jobgrade, Nothing).ToString <> olddata(j).ToString Then
                                    NewValue += a.Name + ": " + a.GetValue(jobgrade, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            End If
                        End If
                    End If
                    j = j + 1
                Next
            Else
                For Each a In GetType(clsJobGrade).GetProperties() 'New Entries
                    If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If a.GetValue(jobgrade, Nothing) = Nothing Then
                                NewValue += a.Name + ":" + " " & vbCrLf
                            Else
                                NewValue += a.Name + ": " + a.GetValue(jobgrade, Nothing).ToString & vbCrLf
                            End If
                        End If
                    End If
                Next
            End If

            If txtid.Text = "0" Then
                txtid.Text = GetIdentity()
                If txtid.Text = "0" Then
                    Exit Sub
                End If
            Else
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Job_Grade_update", jobgrade.id, jobgrade.Grade, jobgrade.Probation, jobgrade.Description, jobgrade.Ranks, jobgrade.ReportsTo)
            End If


            If NewValue.Trim = "" And OldValue.Trim = "" Then
            Else
                If txtid.Text <> "0" Then
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Updated " & jobgrade.Grade, "Job Grade")
                Else
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Inserted", "Job Grade")
                End If

            End If

            lblstatus = "Record saved!"
            Process.loadalert(divalert, msgalert, lblstatus, "success")

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
            cmd.CommandText = "Job_Grade_update"
            cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = 0
            cmd.Parameters.Add("@grade", SqlDbType.VarChar).Value = gradename.Value.Trim
            cmd.Parameters.Add("@probation", SqlDbType.Int).Value = probation.Value
            cmd.Parameters.Add("@desc", SqlDbType.VarChar).Value = txtdescription.Value
            cmd.Parameters.Add("@rank", SqlDbType.Int).Value = rankno.Value
            cmd.Parameters.Add("@reportto", SqlDbType.VarChar).Value = cboReportsTo.SelectedValue
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
            Response.Redirect("~/Module/Admin/jobgrades", True)
        Catch ex As Exception

        End Try
    End Sub
End Class