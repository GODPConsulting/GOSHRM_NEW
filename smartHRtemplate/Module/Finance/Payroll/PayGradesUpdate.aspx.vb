Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO


Public Class PayGradesUpdate
    Inherits System.Web.UI.Page
    Dim paygrade As New clsPayGrade1
    Dim AuthenCode As String = "PAYGRADE"
    Dim olddata(3) As String
    Dim lblstatus As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                Process.LoadRadDropDownTextAndValue(cboGrade, "Job_Grade_get_all", "name", "name", False)
                Process.LoadRadDropDownTextAndValue(cboItem, "vw_Finance_Paylip_Item_Get_All", "item", "item", False)
                txtAmount.Value = 0
                'Holidays_get_parent
                If Request.QueryString("id") IsNot Nothing Then
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Finance_Payslip_Grade_Breakdown_Get", Request.QueryString("id"))
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    txtAmount.Value = strUser.Tables(0).Rows(0).Item("amount").ToString
                    Process.AssignRadDropDownValue(cboGrade, strUser.Tables(0).Rows(0).Item("grade").ToString)
                    Process.AssignRadDropDownValue(cboItem, strUser.Tables(0).Rows(0).Item("salaryitem").ToString)
                    lblItemCategory.Value = strUser.Tables(0).Rows(0).Item("itemclass").ToString
                    lblItemType.Value = strUser.Tables(0).Rows(0).Item("itemtype").ToString
                    cboItem.Enabled = False
                    cboGrade.Enabled = False
                Else
                    txtid.Text = "0"
                    cboItem.Enabled = True
                    cboGrade.Enabled = True
                End If

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Function GetIdentity(ByVal grade As String, salaryitem As String, amount As Double) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Finance_Payslip_Grade_Breakdown_Update"
            cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = 0
            cmd.Parameters.Add("@Grade", SqlDbType.VarChar).Value = grade
            cmd.Parameters.Add("@SalaryItem", SqlDbType.VarChar).Value = salaryitem
            cmd.Parameters.Add("@amount", SqlDbType.Decimal).Value = amount
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
            Return 0
        End Try
    End Function

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            lblstatus = ""
            If (txtAmount.Value.Trim = "") Then
                lblstatus = "Amount required!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                txtAmount.Focus()
                Exit Sub
            End If

            If (cboGrade.SelectedText = Nothing Or cboGrade.SelectedText.Contains("Select") = True) Then
                lblstatus = "Job Grade required!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                cboGrade.Focus()
                Exit Sub
            End If

            If (cboItem.SelectedText = Nothing Or cboItem.SelectedText.Contains("Select") = True) Then
                lblstatus = "Payslip Item required!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                cboItem.Focus()
                Exit Sub
            End If

            'Old Data
            If txtid.Text <> "0" Then
                If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False Then
                    lblstatus = "You don't have privilege to perform this action"
                    Process.loadalert(divalert, msgalert, lblstatus, "danger")
                    Exit Sub
                End If
                Dim strUser As New DataSet
                strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Finance_Payslip_Grade_Breakdown_Get", txtid.Text)
                olddata(0) = strUser.Tables(0).Rows(0).Item("grade").ToString
                olddata(1) = strUser.Tables(0).Rows(0).Item("salaryitem").ToString
                olddata(2) = strUser.Tables(0).Rows(0).Item("amount").ToString
            End If




            paygrade.Amount = txtAmount.Value
            paygrade.Grade = cboGrade.SelectedValue
            paygrade.SalaryItem = cboItem.SelectedValue

            Dim OldValue As String = ""
            Dim NewValue As String = ""

            Dim j As Integer = 0

            If txtid.Text <> "0" Then 'Updates
                For Each a In GetType(clsPayGrade1).GetProperties()
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
                For Each a In GetType(clsPayGrade1).GetProperties() 'New Entries
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
                txtid.Text = GetIdentity(paygrade.Grade, paygrade.SalaryItem, paygrade.Amount)
                If txtid.Text = "0" Then
                    Exit Sub
                End If
            Else
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Finance_Payslip_Grade_Breakdown_Update", txtid.Text, paygrade.Grade, paygrade.SalaryItem, paygrade.Amount)
            End If


            If NewValue.Trim = "" And OldValue.Trim = "" Then
            Else
                If txtid.Text <> "0" Then
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Updated " & paygrade.Grade & " with " & paygrade.SalaryItem, "Pay Grade")
                Else
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Inserted", "Pay Grade")
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
            Response.Redirect("paygrade")
        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "danger")
        End Try
    End Sub


    Protected Sub cboItem_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.DropDownListEventArgs) Handles cboItem.SelectedIndexChanged
        Try
            Dim strUser As New DataSet
            strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "SELECT [item],[taxable],[itemtype],[figure],[ordering],[itemclass],[active] FROM [dbo].[vw_Finance_Paylip_Item] where [item] = '" & cboItem.SelectedText & "'")
            lblItemType.Value = strUser.Tables(0).Rows(0).Item("itemtype").ToString
            lblItemCategory.Value = strUser.Tables(0).Rows(0).Item("itemclass").ToString

        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "danger")
        End Try
    End Sub
End Class