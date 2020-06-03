Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Telerik.Web.UI


Public Class MonthlyStructureUpdate
    Inherits System.Web.UI.Page
    Dim monthlyearning As New clsMonthlyStructure
    Dim AuthenCode As String = "MONTHPAY"
    Dim olddata(8) As String
    Private Sub MakeUpVisible()
        Try
            If radItemType.SelectedText.ToLower.Contains("percent") Then
                lstMakeup.Visible = True
                lblMakeups.Visible = True
                radComponents.Visible = True
                lblattendances.Visible = False
                radAttendance.Visible = False
            Else
                lstMakeup.Visible = False
                lblMakeups.Visible = False
                radComponents.Visible = False
                lblattendances.Visible = True
                radAttendance.Visible = True
            End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
          
            If Not Me.IsPostBack Then
                'Process.LoadListBox(lstMakeup, "Finance_Monthly_Earning_Items_Get_All", 1)

                radIsTaxable.Items.Clear()
                radIsTaxable.Items.Add("No")
                radIsTaxable.Items.Add("Yes")

                radAttendance.Items.Clear()
                radAttendance.Items.Add("No")
                radAttendance.Items.Add("Yes")

                radItemType.Items.Clear()
                radItemType.Items.Add("Fixed Amount")
                radItemType.Items.Add("Percentage")

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
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Finance_Monthly_Earning_Items_get", Request.QueryString("id"))
                    payslipitem.Value = strUser.Tables(0).Rows(0).Item("item").ToString
                    Process.AssignRadDropDownValue(radIsTaxable, strUser.Tables(0).Rows(0).Item("taxable").ToString)
                    Process.AssignRadDropDownValue(radItemType, strUser.Tables(0).Rows(0).Item("itemtype").ToString)
                    Process.AssignRadDropDownValue(radAttendance, strUser.Tables(0).Rows(0).Item("attendanceapplied").ToString)
                    amount.Value = strUser.Tables(0).Rows(0).Item("figure").ToString
                    position.Value = strUser.Tables(0).Rows(0).Item("ordering").ToString
                    If CBool(strUser.Tables(0).Rows(0).Item("Active").ToString) = True Then
                        isactive.Value = "Yes"
                    Else
                        isactive.Value = "No"
                    End If

                    MakeUpVisible()
                    Process.LoadListAndComboxFromDataset(lstMakeup, radComponents, "Finance_Earning_Makeup_Get", "makeup", "makeup", Request.QueryString("id"))


                Else
                    isactive.Value = "Yes"
                    Process.AssignRadDropDownValue(radAttendance, "No")
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
            cmd.CommandText = "Finance_Monthly_Earning_Items_Update"
            cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = "0"
            cmd.Parameters.Add("@Item", SqlDbType.VarChar).Value = payslipitem.Value
            cmd.Parameters.Add("@taxable", SqlDbType.VarChar).Value = radIsTaxable.SelectedText
            cmd.Parameters.Add("@itemtype", SqlDbType.VarChar).Value = radItemType.SelectedText
            cmd.Parameters.Add("@figure", SqlDbType.Decimal).Value = amount.Value
            Dim boolActive As Boolean = False
            If isactive.Value = "Yes" Then
                boolActive = True
            Else
                boolActive = False
            End If
            cmd.Parameters.Add("@active", SqlDbType.Bit).Value = boolActive
            cmd.Parameters.Add("@ordering", SqlDbType.Int).Value = position.Value
            cmd.Parameters.Add("@attendanceapplied", SqlDbType.VarChar).Value = radAttendance.SelectedText
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
                Process.loadalert(divalert, msgalert, "Payslip Item required!", "warning")
                payslipitem.Focus()
                Exit Sub
            End If

            If (radIsTaxable.SelectedText = Nothing) Then
                Process.loadalert(divalert, msgalert, "State if Item is taxable or not!", "warning")
                radIsTaxable.Focus()
                Exit Sub
            End If

            If (radItemType.SelectedText = Nothing) Then
                Process.loadalert(divalert, msgalert, "Item Type required!", "warning")
                radItemType.Focus()
                Exit Sub
            End If

            If (IsNumeric(amount.Value) = False) Then
                Process.loadalert(divalert, msgalert, "Amount/Percentage required!", "warning")
                amount.Focus()
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
                strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Finance_Monthly_Earning_Items_get", Request.QueryString("id"))
                olddata(0) = strUser.Tables(0).Rows(0).Item("id").ToString
                olddata(1) = strUser.Tables(0).Rows(0).Item("item").ToString
                olddata(2) = strUser.Tables(0).Rows(0).Item("taxable").ToString
                olddata(3) = strUser.Tables(0).Rows(0).Item("itemtype").ToString
                olddata(4) = strUser.Tables(0).Rows(0).Item("figure").ToString
                olddata(5) = strUser.Tables(0).Rows(0).Item("ordering").ToString
                olddata(6) = strUser.Tables(0).Rows(0).Item("active").ToString
                olddata(7) = strUser.Tables(0).Rows(0).Item("attendanceapplied").ToString
            End If

            If Request.QueryString("id") Is Nothing Then
                monthlyearning.id = 0
            Else
                monthlyearning.id = Request.QueryString("id")
            End If
            monthlyearning.Figure = amount.Value
            monthlyearning.IsTaxable = radIsTaxable.SelectedText
            monthlyearning.ItemType = radItemType.SelectedText
            monthlyearning.PaySlipItem = payslipitem.Value

            Dim boolActive As Boolean = False
            If isactive.Value = "Yes" Then
                boolActive = True
            Else
                boolActive = False
            End If
            monthlyearning.Active = boolActive
            monthlyearning.Order = position.Value
            monthlyearning.AttendanceBased = radAttendance.SelectedText
            Dim OldValue As String = ""
            Dim NewValue As String = ""

            Dim j As Integer = 0

            If Request.QueryString("id") IsNot Nothing Then
                For Each a In GetType(clsMonthlyStructure).GetProperties()
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
                For Each a In GetType(clsMonthlyStructure).GetProperties() 'New Entries
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
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Updated ID: " & monthlyearning.id, "Pay Slip Earning Items")
                End If
            Else
                sid = monthlyearning.id
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Finance_Monthly_Earning_Items_update", monthlyearning.id, monthlyearning.PaySlipItem, monthlyearning.IsTaxable, monthlyearning.ItemType, monthlyearning.Figure, monthlyearning.Active, monthlyearning.Order, monthlyearning.AttendanceBased)
                If NewValue.Trim = "" And OldValue.Trim = "" Then
                Else
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Inserted", "Pay Slip Earning Items")
                End If
            End If

            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Finance_Earning_Makeup_truncate", monthlyearning.PaySlipItem)

            If radComponents.Visible = True Then
                If radComponents.CheckedItems.Count > 0 Then
                    For d As Integer = 0 To radComponents.CheckedItems.Count - 1
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Finance_Earning_Makeup_Update", monthlyearning.PaySlipItem, radComponents.CheckedItems.Item(d).Text)
                    Next
                End If              
            End If
            
            Process.LoadListAndComboxFromDataset(lstMakeup, radComponents, "Finance_Earning_Makeup_Get", "makeup", "makeup", sid)

            If Request.QueryString("id") IsNot Nothing Then
                Process.loadalert(divalert, msgalert, "Record saved", "success")
            Else
                Response.Redirect("~/Module/Admin/FinanceAdmin/monthlystructureupdate.aspx?id=" & sid, True)
            End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            Session("isnew") = "0"
            Response.Redirect("~/Module/Admin/FinanceAdmin/monthlypaystructure.aspx", True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


    Protected Sub radItemType_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.DropDownListEventArgs) Handles radItemType.SelectedIndexChanged
        MakeUpVisible()
    End Sub
    Private Sub ReloadComponentList()
        Try
            lstMakeup.Items.Clear()
            Dim collection As IList(Of RadComboBoxItem) = radComponents.CheckedItems
            If (collection.Count > 0) Then
                For Each item As RadComboBoxItem In collection
                    lstMakeup.Items.Add(item.Text)

                Next
            Else
                lstMakeup.Items.Clear()
            End If
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub radComponents_ItemChecked(sender As Object, e As Telerik.Web.UI.RadComboBoxItemEventArgs) Handles radComponents.ItemChecked
        ReloadComponentList()
    End Sub
End Class