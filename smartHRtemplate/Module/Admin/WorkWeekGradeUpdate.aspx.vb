Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO


Public Class WorkWeekGradeUpdate
    Inherits System.Web.UI.Page
    Dim workweek As New clsWorkWeek
    Dim AuthenCode As String = "WORKWEEK"
    Dim olddata(4) As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                'Process.LoadRadCombo(radCountry, "CountryTable_get_all", 0)

                Process.LoadRadDropDownTextAndValue(radCountry, "CountryTable_get_all", "country", "country", False)
                Process.LoadRadComboTextAndValueInitiate(cboGrade, "Job_Grade_get_all", "--select--", "name", "name")
                radDay.Items.Clear()
                radDay.Items.Add("Monday")
                radDay.Items.Add("Tuesday")
                radDay.Items.Add("Wednesday")
                radDay.Items.Add("Thursday")
                radDay.Items.Add("Friday")
                radDay.Items.Add("Saturday")
                radDay.Items.Add("Sunday")

                radStatus.Items.Clear()
                radStatus.Items.Add("Full Day")
                radStatus.Items.Add("Non-working Day")

                'JobGrade_Work_Week_get_parent
                If Request.QueryString("id") IsNot Nothing Then
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "JobGrade_Work_Week_get", Request.QueryString("id"))
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    Process.AssignRadDropDownValue(radDay, strUser.Tables(0).Rows(0).Item("Day").ToString)
                    Process.AssignRadDropDownValue(radCountry, strUser.Tables(0).Rows(0).Item("Country").ToString)
                    Process.AssignRadDropDownValue(radStatus, strUser.Tables(0).Rows(0).Item("Status").ToString)
                    Process.AssignRadComboValue(cboGrade, strUser.Tables(0).Rows(0).Item("jobgrade").ToString)
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
                    Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "warning")
                    Exit Sub
                End If
            End If
          

            Dim lblstatus As String = ""
            If (cboGrade.SelectedItem.Text = "" Or cboGrade.SelectedItem.Text.ToLower = "--select--") Then
                lblstatus = "Grade required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                radDay.Focus()
                Exit Sub
            End If

            If (radDay.SelectedText.Trim = "" Or radDay.SelectedText.Trim = "-- Select --") Then
                lblstatus = "Day required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                radDay.Focus()
                Exit Sub
            End If

            If (radCountry.SelectedText.Trim = "" Or radCountry.SelectedText.Trim = "-- Select --") Then
                lblstatus = "Country required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                radCountry.Focus()
                Exit Sub
            End If

            If (radStatus.SelectedText.Trim = "" Or radStatus.SelectedText.Trim = "-- Select --") Then
                lblstatus = "Work Week Status required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                radStatus.Focus()
                Exit Sub
            End If

            'Old Data


            'SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "JobGrade_Work_Week_update", workweek.id, workweek.Day, workweek.Status, workweek.Country)


            If txtid.Text = "0" Then
                txtid.Text = GetIdentity()
                If txtid.Text = "0" Then
                    Exit Sub
                End If
            Else
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "JobGrade_Work_Week_update", txtid.Text, cboGrade.SelectedValue, radDay.SelectedText, radStatus.SelectedText, radCountry.SelectedText)
            End If

            lblstatus = "Record saved"
            Process.loadalert(divalert, msgalert, lblstatus, "success")
            ' Response.Write("<script language='javascript'> { self.close() }</script>")
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
            cmd.CommandText = "JobGrade_Work_Week_update"
            cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = txtid.Text
            cmd.Parameters.Add("@grade", SqlDbType.VarChar).Value = cboGrade.SelectedValue
            cmd.Parameters.Add("@day", SqlDbType.VarChar).Value = radDay.SelectedText
            cmd.Parameters.Add("@status", SqlDbType.VarChar).Value = radStatus.SelectedText
            cmd.Parameters.Add("@country", SqlDbType.VarChar).Value = radCountry.SelectedText
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
            Response.Redirect("~/Module/Admin/WorkWeekGrade", True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    'Protected Sub radStructure_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.DropDownListEventArgs) Handles radDay.SelectedIndexChanged
    '    Try
    '        Process.LoadRadComboP1(radStatus, "JobGrade_Work_Week_get_parent", radDay.SelectedText, 0)
    '    Catch ex As Exception
    '        Process.loadalert(divalert, msgalert, ex.Message, "danger")
    '    End Try
    'End Sub
End Class