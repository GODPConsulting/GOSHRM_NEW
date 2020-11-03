Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO


Public Class KPIUpdate
    Inherits System.Web.UI.Page
    Dim comp As New clsCompetence
    Dim AuthenCode As String = "COMPET"
    Dim olddata(3) As String
    Dim lblstatus As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        RadDropDownList1.Items.Clear()
        RadDropDownList1.Items.Add("Yes")
        RadDropDownList1.Items.Add("No")
        Try
            If Not Me.IsPostBack Then
                'Company_Structure_get_parent
                Process.LoadRadComboTextAndValue(cboKPIType, "Competency_Group_Get_All", "name", "id", False)
                txtid.Text = "0"
                If Request.QueryString("id") IsNot Nothing Then
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Competency_get", Request.QueryString("id"))
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    txtname.Value = strUser.Tables(0).Rows(0).Item("name").ToString
                    txtDesc.Value = strUser.Tables(0).Rows(0).Item("Description").ToString
                    RadDropDownList1.SelectedText = strUser.Tables(0).Rows(0).Item("UploadStatus").ToString
                    Process.AssignRadComboValue(cboKPIType, strUser.Tables(0).Rows(0).Item("CompetencyType").ToString)
                End If
            End If
        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "danger")
        End Try
    End Sub
    Private Function GetIdentity(ByVal kpitype As String, kpidesc As String, kpigroup As Integer) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Competency_update"
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = kpitype
            cmd.Parameters.Add("@Desc", SqlDbType.VarChar).Value = kpidesc
            cmd.Parameters.Add("@groupid", SqlDbType.Int).Value = kpigroup
            cmd.Parameters.Add("@UploadStatus", SqlDbType.VarChar).Value = RadDropDownList1.SelectedText
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            If txtid.Text <> "0" Then
                If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False Then
                    Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "danger")
                    Exit Sub
                End If
            End If


            If (txtname.Value.Trim = "") Then
                lblstatus = "KPI Name required!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                txtname.Focus()
                Exit Sub
            End If
            If RadDropDownList1.SelectedText = "" Then
                lblstatus = "please select upload status"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                RadDropDownList1.Focus()
            End If


            ''Old Data
            'If Request.QueryString("id") IsNot Nothing Then
            '    Dim strUser As New DataSet
            '    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Competency_get", Request.QueryString("id"))
            '    olddata(0) = strUser.Tables(0).Rows(0).Item("id").ToString
            '    olddata(1) = strUser.Tables(0).Rows(0).Item("name").ToString
            '    olddata(2) = strUser.Tables(0).Rows(0).Item("Description").ToString
            'End If

            'If txtid.Text.Trim = "" Then
            '    comp.id = 0
            'Else
            '    comp.id = txtid.Text
            'End If
            'comp.Name = txtname.Text.Trim
            'comp.Description = txtDesc.Text

            'Dim OldValue As String = ""
            'Dim NewValue As String = ""

            'Dim j As Integer = 0

            'If Request.QueryString("id") IsNot Nothing Then 'Updates
            '    For Each a In GetType(clsCompetence).GetProperties()
            '        If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
            '            If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
            '                If IsNumeric(a.GetValue(comp, Nothing)) = True And IsNumeric(olddata(j)) = True Then
            '                    If CDbl(a.GetValue(comp, Nothing)) <> CDbl(olddata(j)) Then
            '                        NewValue += a.Name + ": " + a.GetValue(comp, Nothing).ToString & vbCrLf
            '                        OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
            '                    End If
            '                Else
            '                    If a.GetValue(comp, Nothing).ToString <> olddata(j).ToString Then
            '                        NewValue += a.Name + ": " + a.GetValue(comp, Nothing).ToString & vbCrLf
            '                        OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
            '                    End If
            '                End If
            '            End If
            '        End If
            '        j = j + 1
            '    Next
            'Else
            '    For Each a In GetType(clsCompetence).GetProperties() 'New Entries
            '        If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
            '            If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
            '                If a.GetValue(comp, Nothing) = Nothing Then
            '                    NewValue += a.Name + ":" + " " & vbCrLf
            '                Else
            '                    NewValue += a.Name + ": " + a.GetValue(comp, Nothing).ToString & vbCrLf
            '                End If
            '            End If
            '        End If
            '    Next
            'End If
            If txtid.Text = "0" Then
                txtid.Text = GetIdentity(txtname.Value, txtDesc.Value, cboKPIType.SelectedValue)
            Else
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Competency_update", txtid.Text, txtname.Value.Trim, txtDesc.Value, cboKPIType.SelectedValue, RadDropDownList1.SelectedText)
            End If

            lblstatus = "Record saved"
            Process.loadalert(divalert, msgalert, lblstatus, "success")
            'Response.Write("<script language='javascript'> { self.close() }</script>")
        Catch ex As Exception
            lblstatus = ex.Message
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect("~/Module/Performance/Settings/KPI.aspx", True)
        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "danger")
        End Try
    End Sub


End Class