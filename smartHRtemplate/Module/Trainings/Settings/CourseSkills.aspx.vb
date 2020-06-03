Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Telerik.Web.UI

Public Class CourseSkills
    Inherits System.Web.UI.Page
    Dim jobtitle As New clsJobTitle
    Dim AuthenCode As String = "Job"
    Dim olddata(3) As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                Process.LoadRadComboTextAndValueInitiate(cboskill, "Job_Title_Skills_Get_All_Distinct", "--Select--", "skills", "skills")

                If Request.QueryString("id") IsNot Nothing Then
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Course_Skills_get", Request.QueryString("id"))
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    Process.AssignRadComboValue(cboskill, strUser.Tables(0).Rows(0).Item("skill").ToString)
                    txtcourseid.Text = strUser.Tables(0).Rows(0).Item("courseid").ToString
                    aweight.Value = strUser.Tables(0).Rows(0).Item("rating").ToString
                Else
                    txtid.Text = "0"
                    txtcourseid.Text = Request.QueryString("courseid")
                End If

                Dim strjob As New DataSet
                strjob = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Courses_get", txtcourseid.Text)
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
            cmd.CommandText = "Course_Skills_update"
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = txtid.Text
            cmd.Parameters.Add("@courseid", SqlDbType.Int).Value = txtcourseid.Text
            cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = cboskill.SelectedItem.Value
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
            Dim url As String = "CoursesUpdate?id=" & txtcourseid.Text
            Response.Redirect(url, True)
        Catch ex As Exception

        End Try
    End Sub





    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            Dim lblstatus As String = ""

            If (cboskill.SelectedValue Is Nothing) Then
                lblstatus = "Skill required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                cboskill.Focus()
                Exit Sub
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
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Course_Skills_update", txtid.Text, txtcourseid.Text, cboskill.SelectedItem.Value.Trim, aweight.Value)
            End If

            lblstatus = "Record saved!"
            Process.loadalert(divalert, msgalert, lblstatus, "success")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
End Class