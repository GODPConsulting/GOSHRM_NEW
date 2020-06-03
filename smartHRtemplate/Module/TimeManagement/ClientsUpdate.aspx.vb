Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO


Public Class ClientsUpdate
    Inherits System.Web.UI.Page
    Dim client As New clsClient
    Dim AuthenCode As String = "CLIENT"
    Dim olddata(8) As String
    Dim lblstatus As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                'Company_Structure_get_parent
                If Request.QueryString("id") IsNot Nothing Then
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Time_Clients_get", Request.QueryString("id"))
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    txtname.Value = strUser.Tables(0).Rows(0).Item("name").ToString
                    txtDetail.Value = strUser.Tables(0).Rows(0).Item("Detail").ToString
                    txtEmail.Value = strUser.Tables(0).Rows(0).Item("ContactEmail").ToString
                    txtNumber.Value = strUser.Tables(0).Rows(0).Item("ContactNo").ToString
                    txtAddress.Value = strUser.Tables(0).Rows(0).Item("Address").ToString
                    txtURL.Value = strUser.Tables(0).Rows(0).Item("CompanyURL").ToString
                    radContactDate.SelectedDate = strUser.Tables(0).Rows(0).Item("FirstContactDate").ToString
                End If

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "danger")
                Exit Sub
            End If

            If (txtname.Value.Trim = "") Then
                lblstatus = "Client Name required!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                txtname.Focus()
                Exit Sub
            End If


            'Old Data
            If Request.QueryString("id") IsNot Nothing Then
                Dim strUser As New DataSet
                strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Time_Clients_get", Request.QueryString("id"))
                olddata(0) = strUser.Tables(0).Rows(0).Item("id").ToString
                olddata(1) = strUser.Tables(0).Rows(0).Item("name").ToString
                olddata(2) = strUser.Tables(0).Rows(0).Item("ContactNo").ToString
                olddata(3) = strUser.Tables(0).Rows(0).Item("ContactEmail").ToString
                olddata(4) = strUser.Tables(0).Rows(0).Item("CompanyURL").ToString
                olddata(5) = strUser.Tables(0).Rows(0).Item("Address").ToString
                olddata(6) = strUser.Tables(0).Rows(0).Item("Detail").ToString
                olddata(7) = strUser.Tables(0).Rows(0).Item("FirstContactDate").ToString
            End If

            If txtid.Text.Trim = "" Then
                client.id = 0
            Else
                client.id = txtid.Text
            End If
            client.Name = txtname.Value.Trim.Trim
            client.ContactEmail = txtEmail.Value.Trim
            client.CompanyURL = txtURL.Value.Trim
            client.Address = txtAddress.Value
            client.Detail = txtDetail.Value
            client.FirstContactDate = radContactDate.SelectedDate
            client.ContactNo = txtNumber.Value.Trim

            Dim OldValue As String = ""
            Dim NewValue As String = ""

            Dim j As Integer = 0

            If Request.QueryString("id") IsNot Nothing Then 'Updates
                For Each a In GetType(clsClient).GetProperties()
                    If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If IsNumeric(a.GetValue(client, Nothing)) = True And IsNumeric(olddata(j)) = True Then
                                If CDbl(a.GetValue(client, Nothing)) <> CDbl(olddata(j)) Then
                                    NewValue += a.Name + ": " + a.GetValue(client, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            Else
                                If a.GetValue(client, Nothing).ToString <> olddata(j).ToString Then
                                    NewValue += a.Name + ": " + a.GetValue(client, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            End If
                        End If
                    End If
                    j = j + 1
                Next
            Else
                For Each a In GetType(clsClient).GetProperties() 'New Entries
                    If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If a.GetValue(client, Nothing) = Nothing Then
                                NewValue += a.Name + ":" + " " & vbCrLf
                            Else
                                NewValue += a.Name + ": " + a.GetValue(client, Nothing).ToString & vbCrLf
                            End If
                        End If
                    End If
                Next
            End If

            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Time_Clients_update", client.id, client.Name, client.ContactNo, client.ContactEmail, client.CompanyURL, client.Address, client.Detail, client.FirstContactDate)

            If NewValue.Trim = "" And OldValue.Trim = "" Then
            Else
                If Request.QueryString("id") IsNot Nothing Then
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Updated " & client.Name, "Clients")
                Else
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Inserted", "Clients")
                End If

            End If
            lblstatus = "Record saved"
            Process.loadalert(divalert, msgalert, lblstatus, "success")
        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "danger")
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect("~/Module/TimeManagement/Clients.aspx", True)
        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "danger")
        End Try
    End Sub


End Class