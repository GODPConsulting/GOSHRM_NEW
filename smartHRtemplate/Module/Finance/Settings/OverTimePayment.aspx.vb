Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Telerik.Web.UI
Public Class MyList1
    Public Property id As Integer
    Public Property skills As String
End Class
Public Class OverTimePayment
    Inherits System.Web.UI.Page
    Dim jobtitle As New clsJobTitle
    Dim AuthenCode As String = "Job"
    Dim olddata(3) As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                radItemActive.Items.Clear()
                radItemActive.Items.Add("Yes")
                radItemActive.Items.Add("No")
                Process.LoadRadComboTextAndValueInitiate(cboskill, "Job_Grade_get_all", "--Select--", "Name", "Name")

                If Request.QueryString("id") IsNot Nothing Then
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Overtime_Category_get", Request.QueryString("id"))
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    Process.AssignRadComboValue(cboskill, strUser.Tables(0).Rows(0).Item("Jobgrade").ToString)
                    txtcourseid.Text = strUser.Tables(0).Rows(0).Item("PAYROLLID").ToString
                    aweight.Value = strUser.Tables(0).Rows(0).Item("rate").ToString
                    If CBool(strUser.Tables(0).Rows(0).Item("Active")) = True Then
                        radItemActive.SelectedText = "Yes"
                    Else
                        radItemActive.SelectedText = "No"
                    End If
                Else
                    txtid.Text = "0"
                    txtcourseid.Text = Request.QueryString("courseid")
                End If

                'Dim strjob As New DataSet
                'strjob = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Courses_get", txtcourseid.Text)
                'If strjob.Tables(0).Rows.Count > 0 Then
                '    pagetitle.InnerText = strjob.Tables(0).Rows(0).Item("name").ToString & " Skills"
                'End If

            End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Private Function GetIdentity() As String
        Try
            Dim active As Boolean
            If radItemActive.SelectedText = "Yes" Then
                active = True
            Else
                active = False
            End If

            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Overtime_Jobgrade_update"
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = txtid.Text
            cmd.Parameters.Add("@payrollid", SqlDbType.Int).Value = txtcourseid.Text
            cmd.Parameters.Add("@jobgrade", SqlDbType.VarChar).Value = cboskill.SelectedItem.Value
            cmd.Parameters.Add("@rate", SqlDbType.Decimal).Value = aweight.Value
            cmd.Parameters.Add("@Active", SqlDbType.Bit).Value = active
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
            Dim url As String = "PayrollOptionUpdate?id=" & txtcourseid.Text
            Response.Redirect(url, True)
        Catch ex As Exception

        End Try
    End Sub





    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            Dim lblstatus As String = ""

            If (cboskill.SelectedValue Is Nothing) Then
                lblstatus = "Please select a jobgrade"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                cboskill.Focus()
                Exit Sub
            End If

            If (aweight.Value Is Nothing) Then
                lblstatus = "rate is required"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                aweight.Focus()
                Exit Sub
            End If

            If (IsNumeric(aweight.Value) = False) Then
                lblstatus = "rate is invalid! It must me numeric"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                aweight.Focus()
                Exit Sub
            End If

            Dim SkillList As List(Of MyList1) = New List(Of MyList1)
            Dim strUser As DataSet
            strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Overtime_Category_get_all", txtcourseid.Text)
            Dim i As Integer = 0
            Dim count1 As Integer = 1

            If strUser.Tables(0).Rows.Count > 0 Then
                For Each dr As DataRow In strUser.Tables(0).Rows
                    Try
                        Dim prog As MyList1 = New MyList1()
                        prog.skills = Convert.ToString(strUser.Tables(0).Rows(i)("jobgrade"))
                        prog.id = Convert.ToString(strUser.Tables(0).Rows(i)("id"))
                        If prog.skills = cboskill.SelectedValue And prog.id <> txtid.Text Then
                            lblstatus = "Jobgrade cannot be inputed twice "
                            Process.loadalert(divalert, msgalert, lblstatus, "danger")
                            Exit Sub
                        End If

                        SkillList.Add(prog)


                        i += 1
                        count1 += 1
                    Catch Ex As Exception
                        Process.loadalert(divalert, msgalert, Ex.Message, "danger")
                        Exit Sub
                    End Try
                Next


            End If

            If txtid.Text = "0" Then

                txtid.Text = GetIdentity()
                If txtid.Text = "0" Then
                    Exit Sub
                End If

            Else
                Dim active As Boolean
                If radItemActive.SelectedText = "Yes" Then
                    active = True
                Else
                    active = False
                End If
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Overtime_Jobgrade_update", txtid.Text, txtcourseid.Text, cboskill.SelectedItem.Value.Trim, aweight.Value, active)
            End If
            Dim url As String = "PayrollOptionUpdate?id=" & txtcourseid.Text
            Response.Redirect(url, True)
            lblstatus = "Record saved!"
            Process.loadalert(divalert, msgalert, lblstatus, "success")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
End Class