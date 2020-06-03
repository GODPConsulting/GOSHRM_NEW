Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO


Public Class NonPayGradesUpdate
    Inherits System.Web.UI.Page
    Dim paygrade As New clsNonPayGrade1
    Dim AuthenCode As String = "NONPAYROLLPAY"
    Dim olddata(3) As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                Process.LoadRadDropDownTextAndValue(cboGrade, "Job_Grade_get_all", "name", "name", False)
                Process.LoadRadDropDownTextAndValue(cboItem, "Finance_Non_Payroll_Items_Get_All", "item", "item", False)
                txtAmount.Text = 0
                'Holidays_get_parent
                If Request.QueryString("id") IsNot Nothing Then
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Finance_Non_Payslip_Grade_Get", Request.QueryString("id"))
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    txtAmount.Text = strUser.Tables(0).Rows(0).Item("amount").ToString
                    Process.AssignRadDropDownValue(cboGrade, strUser.Tables(0).Rows(0).Item("grade").ToString)
                    Process.AssignRadDropDownValue(cboItem, strUser.Tables(0).Rows(0).Item("salaryitem").ToString)
                    lblItemType.Text = strUser.Tables(0).Rows(0).Item("itemtype").ToString
                    cboItem.Enabled = False
                    cboGrade.Enabled = False
                Else
                    txtid.Text = "0"
                    cboItem.Enabled = True
                    cboGrade.Enabled = True
                End If

            End If
        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub
    Private Function GetIdentity() As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Finance_Non_Payslip_Grade_Update"
            cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = 0
            cmd.Parameters.Add("@grade", SqlDbType.VarChar).Value = cboGrade.SelectedValue
            cmd.Parameters.Add("@salary", SqlDbType.Int).Value = cboItem.SelectedValue
            cmd.Parameters.Add("@amount", SqlDbType.Decimal).Value = txtAmount.Text
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception
            lblstatus.Text = ex.Message
            Return 0
        End Try
    End Function

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            lblstatus.Text = ""
            If (IsNumeric(txtAmount.Text) = False) Then
                lblstatus.Text = "Amount required!"
                txtAmount.Focus()
                Exit Sub
            End If

            If (cboGrade.SelectedText = Nothing Or cboGrade.SelectedText.Contains("Select") = True) Then
                lblstatus.Text = "Job Grade required!"
                cboGrade.Focus()
                Exit Sub
            End If

            If (cboItem.SelectedText = Nothing Or cboItem.SelectedText.Contains("Select") = True) Then
                lblstatus.Text = "Item required!"
                cboItem.Focus()
                Exit Sub
            End If

            'Old Data
            If txtid.Text <> "0" Then
                If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False Then
                    lblstatus.Text = "You don't have privilege to perform this action"
                    Exit Sub
                End If
                Dim strUser As New DataSet
                strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Finance_Non_Payslip_Grade_Get", txtid.Text)
                olddata(0) = strUser.Tables(0).Rows(0).Item("grade").ToString
                olddata(1) = strUser.Tables(0).Rows(0).Item("salaryitem").ToString
                olddata(2) = strUser.Tables(0).Rows(0).Item("amount").ToString
            End If




            paygrade.Amount = txtAmount.Text
            paygrade.Grade = cboGrade.SelectedText
            paygrade.Item = cboItem.SelectedText

            Dim OldValue As String = ""
            Dim NewValue As String = ""

            Dim j As Integer = 0

            If txtid.Text <> "0" Then 'Updates
                For Each a In GetType(clsNonPayGrade1).GetProperties()
                    If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If IsNumeric(a.GetValue(paygrade, Nothing)) = True And IsNumeric(olddata(j)) = True Then
                                If CDbl(a.GetValue(paygrade, Nothing)) <> CDbl(olddata(j)) Then
                                    NewValue += a.Name + ": " + a.GetValue(paygrade, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            Else
                                If a.GetValue(paygrade, Nothing).ToString <> olddata(j).ToString Then
                                    NewValue += a.Name + ": " + a.GetValue(paygrade, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            End If
                        End If
                    End If
                    j = j + 1
                Next
            Else
                For Each a In GetType(clsNonPayGrade1).GetProperties() 'New Entries
                    If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If a.GetValue(paygrade, Nothing) = Nothing Then
                                NewValue += a.Name + ":" + " " & vbCrLf
                            Else
                                NewValue += a.Name + ": " + a.GetValue(paygrade, Nothing).ToString & vbCrLf
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
                If NewValue.Trim = "" And OldValue.Trim = "" Then
                Else
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Inserted", "Non-Pay-Items Grade")
                End If
            Else
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Finance_Non_Payslip_Grade_Update", txtid.Text, paygrade.Grade, paygrade.Item, paygrade.Amount)
                If NewValue.Trim = "" And OldValue.Trim = "" Then
                Else
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Updated " & paygrade.Grade & " with " & paygrade.Item, "Non-Pay-Items Grade")
                End If
            End If


            
            lblstatus.Text = "Record saved"
            'Response.Write("<script language='javascript'> { self.close() }</script>")
        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Response.Write("<script language='javascript'> { self.close() }</script>")
        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub


    Protected Sub cboItem_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.DropDownListEventArgs) Handles cboItem.SelectedIndexChanged
        Try
            Dim strUser As New DataSet
            strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "select itemtype from Finance_Non_Payroll_Items where item = '" & cboItem.SelectedText & "'")
            lblItemType.Text = strUser.Tables(0).Rows(0).Item("itemtype").ToString            
        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub
End Class