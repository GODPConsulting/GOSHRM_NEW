Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Telerik.Web.UI


Public Class NonPayrollItemUpdate
    Inherits System.Web.UI.Page
    Dim monthlyearning As New clsNonPayroll
    Dim AuthenCode As String = "NONPAYROLLPAY"
    Dim olddata(5) As String
    Dim Pages As String = "Non Payroll Items"
    Private Sub MakeUpVisible()
        Try
            If radInputType.SelectedText.ToLower.Contains("percent") Then
                lstComponents.Visible = True
                lblComponents.Visible = True
                radComponents.Visible = True
            Else
                lstComponents.Visible = False
                lblComponents.Visible = False
                radComponents.Visible = False
            End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not Me.IsPostBack Then
                radInputType.Items.Clear()
                radInputType.Items.Add("Fixed Amount")
                radInputType.Items.Add("Percentage")

                Process.LoadRadComboTextAndValue(radComponents, "Finance_Monthly_Earning_Items_Get_All", "payslip item", "payslip item")
                'Company_Structure_get_parent
                If Request.QueryString("id") IsNot Nothing Then
                    If Session("isnew") IsNot Nothing Then
                        If Session("isnew") = "1" Then
                            Process.loadalert(divalert, msgalert, "Record saved", "success")
                            Session("isnew") = "0"
                        End If
                    End If
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Finance_Non_Payroll_Items_get", Request.QueryString("id"))
                    payslipitem.Value = strUser.Tables(0).Rows(0).Item("item").ToString
                    Process.AssignRadDropDownValue(radInputType, strUser.Tables(0).Rows(0).Item("itemtype").ToString)
                    position.Value = strUser.Tables(0).Rows(0).Item("ordering").ToString
                    If CBool(strUser.Tables(0).Rows(0).Item("Active").ToString) = True Then
                        isactive.Value = "Yes"
                    Else
                        isactive.Value = "No"
                    End If

                    MakeUpVisible()
                    Process.LoadListAndComboxFromDataset(lstComponents, radComponents, "Finance_Non_Payroll_Makeup_Get", "makeup", "makeup", Request.QueryString("id"))

                Else
                    isactive.Value = "Yes"
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
            cmd.CommandText = "Finance_Non_Payroll_Items_Update"
            cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = "0"
            cmd.Parameters.Add("@Item", SqlDbType.VarChar).Value = payslipitem.Value
            cmd.Parameters.Add("@itemtype", SqlDbType.VarChar).Value = radInputType.SelectedText
            Dim boolActive As Boolean = False
            If isactive.Value = "Yes" Then
                boolActive = True
            Else
                boolActive = False
            End If
            cmd.Parameters.Add("@active", SqlDbType.Bit).Value = boolActive
            cmd.Parameters.Add("@ordering", SqlDbType.Int).Value = position.Value
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
            If Request.QueryString("id") IsNot Nothing Then
                If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False Then
                    Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "warning")
                    Exit Sub
                End If
            End If

            If (payslipitem.Value.Trim = "") Then
                Process.loadalert(divalert, msgalert, "Item name required!", "warning")
                payslipitem.Focus()
                Exit Sub
            End If

            If (radInputType.SelectedText = Nothing) Then
                Process.loadalert(divalert, msgalert, "Item Type required!", "warning")
                radInputType.Focus()
                Exit Sub
            End If

            If (IsNumeric(position.Value) = False) Then
                Process.loadalert(divalert, msgalert, "Position required!", "warning")
                position.Focus()
                Exit Sub
            End If
            'Old Data
            If Request.QueryString("id") IsNot Nothing Then
                Dim strUser As New DataSet
                strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Finance_Non_Payroll_Items_get", Request.QueryString("id"))
                olddata(0) = strUser.Tables(0).Rows(0).Item("id").ToString
                olddata(1) = strUser.Tables(0).Rows(0).Item("item").ToString
                olddata(2) = strUser.Tables(0).Rows(0).Item("itemtype").ToString
                olddata(3) = strUser.Tables(0).Rows(0).Item("ordering").ToString
                olddata(4) = strUser.Tables(0).Rows(0).Item("active").ToString
            End If

            If Request.QueryString("id") Is Nothing Then
                monthlyearning.id = 0
            Else
                monthlyearning.id = Request.QueryString("id")
            End If
            monthlyearning.ItemType = radInputType.SelectedText
            monthlyearning.Item = payslipitem.Value
            Dim boolActive As Boolean = False
            If isactive.Value = "Yes" Then
                boolActive = True
            Else
                boolActive = False
            End If
            monthlyearning.Active = boolActive
            monthlyearning.Order = position.Value
            Dim OldValue As String = ""
            Dim NewValue As String = ""

            Dim j As Integer = 0

            If Request.QueryString("id") IsNot Nothing Then 'Updates
                For Each a In GetType(clsNonPayroll).GetProperties()
                    If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If IsNumeric(a.GetValue(monthlyearning, Nothing)) = True And IsNumeric(olddata(j)) = True Then
                                If CDbl(a.GetValue(monthlyearning, Nothing)) <> CDbl(olddata(j)) Then
                                    NewValue += a.Name + ": " + a.GetValue(monthlyearning, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            Else
                                If a.GetValue(monthlyearning, Nothing).ToString <> olddata(j).ToString Then
                                    NewValue += a.Name + ": " + a.GetValue(monthlyearning, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            End If
                        End If
                    End If
                    j = j + 1
                Next
            Else
                For Each a In GetType(clsNonPayroll).GetProperties() 'New Entries
                    If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If a.GetValue(monthlyearning, Nothing) = Nothing Then
                                NewValue += a.Name + ":" + " " & vbCrLf
                            Else
                                NewValue += a.Name + ": " + a.GetValue(monthlyearning, Nothing).ToString & vbCrLf
                            End If
                        End If
                    End If
                Next
            End If

            Dim sid As String = "0"
            If Request.QueryString("id") Is Nothing Then
                sid = GetIdentity()
                If sid = "0" Then
                    Exit Sub
                End If
                If NewValue.Trim = "" And OldValue.Trim = "" Then
                Else
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Updated ID: " & monthlyearning.id, Pages)
                End If
            Else
                sid = monthlyearning.id
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Finance_Non_Payroll_Items_Update", monthlyearning.id, monthlyearning.Item, monthlyearning.ItemType, monthlyearning.Active, monthlyearning.Order)
                If NewValue.Trim = "" And OldValue.Trim = "" Then
                Else
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Inserted", Pages)
                End If
            End If


            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Finance_Non_Payroll_Makeup_truncate", monthlyearning.Item)

            If radComponents.Visible = True Then
                If radComponents.CheckedItems.Count > 0 Then
                    For d As Integer = 0 To radComponents.CheckedItems.Count - 1
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Finance_Non_Payroll_Makeup_Update", monthlyearning.Item, radComponents.CheckedItems.Item(d).Text)
                    Next
                End If              
            End If
            
            Process.LoadListAndComboxFromDataset(lstComponents, radComponents, "Finance_Non_Payroll_Makeup_Get", "makeup", "makeup", sid)

            If Request.QueryString("id") IsNot Nothing Then
                Process.loadalert(divalert, msgalert, "Record saved", "success")
            Else
                Response.Redirect("~/Module/Admin/FinanceAdmin/NonPayrollItemUpdate.aspx?id=" & sid, True)
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            Session("isnew") = "0"
            Response.Redirect("~/Module/Admin/FinanceAdmin/NonPayrollItems.aspx", True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


    Protected Sub radInputType_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.DropDownListEventArgs) Handles radInputType.SelectedIndexChanged
        MakeUpVisible()
    End Sub
    Private Sub ReloadComponentList()
        Try
            lstComponents.Items.Clear()
            Dim collection As IList(Of RadComboBoxItem) = radComponents.CheckedItems
            If (collection.Count > 0) Then
                For Each item As RadComboBoxItem In collection
                    lstComponents.Items.Add(item.Text)

                Next
            Else
                lstComponents.Items.Clear()
            End If
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub radComponents_ItemChecked(sender As Object, e As Telerik.Web.UI.RadComboBoxItemEventArgs) Handles radComponents.ItemChecked
        ReloadComponentList()
    End Sub
End Class