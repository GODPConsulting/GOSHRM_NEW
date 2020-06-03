Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO


Public Class LoanTypesUpdate
    Inherits System.Web.UI.Page
    Dim loantype As New clsLoanType
    Dim AuthenCode As String = "LOANTYPE"
    Dim olddata(3) As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                'Company_Structure_get_parent
                If Request.QueryString("id") IsNot Nothing Then
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Loan_Type_get", Request.QueryString("id"))
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    aname.value = strUser.Tables(0).Rows(0).Item("name").ToString
                    adesc.value = strUser.Tables(0).Rows(0).Item("Description").ToString
                Else
                    txtid.Text = "0"
                    aname.Value = ""
                    adesc.Value = ""
                End If

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.message, "danger")
        End Try
    End Sub
    Private Function GetIdentity(ByVal loanname As String, ByVal desc As String) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Loan_Type_update"
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = loanname
            cmd.Parameters.Add("@desc", SqlDbType.VarChar).Value = desc
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.message, "danger")
            Return 0
        End Try
    End Function

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            Dim lblstatus As String = ""
            If txtid.Text <> "0" Then
                If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False Then
                    Process.loadalert(divalert, msgalert, Process.privilegemsg, "warning")
                    Exit Sub
                End If
            End If
            

            If (aname.Value.Trim = "") Then
                lblstatus = "Loan Name required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                aname.Focus()
                Exit Sub
            End If


            'Old Data
            If txtid.Text <> "0" And txtid.Text.Trim <> "" Then
                Dim strUser As New DataSet
                strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Loan_Type_get", txtid.Text)
                olddata(0) = strUser.Tables(0).Rows(0).Item("id").ToString
                olddata(1) = strUser.Tables(0).Rows(0).Item("name").ToString
                olddata(2) = strUser.Tables(0).Rows(0).Item("Description").ToString
            End If

            If txtid.Text.Trim = "" Then
                loantype.id = 0
            Else
                loantype.id = txtid.Text
            End If
            loantype.Name = aname.Value.Trim
            loantype.Description = adesc.Value

            Dim OldValue As String = ""
            Dim NewValue As String = ""

            Dim j As Integer = 0

            If txtid.Text <> "0" And txtid.Text.Trim <> "" Then 'Updates
                For Each a In GetType(clsLoanType).GetProperties()
                    If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If IsNumeric(a.GetValue(loantype, Nothing)) = True And IsNumeric(olddata(j)) = True Then
                                If CDbl(a.GetValue(loantype, Nothing)) <> CDbl(olddata(j)) Then
                                    NewValue += a.Name + ": " + a.GetValue(loantype, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            Else
                                If a.GetValue(loantype, Nothing).ToString <> olddata(j).ToString Then
                                    NewValue += a.Name + ": " + a.GetValue(loantype, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            End If
                        End If
                    End If
                    j = j + 1
                Next
            Else
                For Each a In GetType(clsLoanType).GetProperties() 'New Entries
                    If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If a.GetValue(loantype, Nothing) = Nothing Then
                                NewValue += a.Name + ":" + " " & vbCrLf
                            Else
                                NewValue += a.Name + ": " + a.GetValue(loantype, Nothing).ToString & vbCrLf
                            End If
                        End If
                    End If
                Next
            End If

            If txtid.Text <> "0" And txtid.Text.Trim <> "" Then
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Loan_Type_update", loantype.id, loantype.Name, loantype.Description)
            Else
                txtid.Text = GetIdentity(loantype.Name, loantype.Description)
                If txtid.Text = "0" Then                    
                    Exit Sub
                End If
            End If



            If NewValue.Trim = "" And OldValue.Trim = "" Then
            Else
                If txtid.Text <> "0" And txtid.Text.Trim <> "" Then
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Updated " & loantype.Name, "Loan Types")
                Else
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Inserted", "Loan Types")
                End If

            End If
            lblstatus = "Record saved"
            Process.loadalert(divalert, msgalert, lblstatus, "success")            
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect("loantypes", True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


End Class