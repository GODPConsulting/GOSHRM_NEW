Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Telerik.Web.UI


Public Class DeductionsUpdate
    Inherits System.Web.UI.Page
    Dim saldeduction As New clsDeductions
    Dim AuthenCode As String = "DEDUCTION"
    Dim olddata(10) As String
    Dim Level(2) As String
    Dim Separators() As Char = {":"c}
    Dim EmpID As String = ""
    Private Sub ComponentsCheck()
        Try
            If radInputType.SelectedText.Contains("Percentage") Then
                lblComponents.Visible = True
                radComponents.Visible = True
                lstComponents.Visible = True
            Else
                lblComponents.Visible = False
                radComponents.Visible = False
                lstComponents.Visible = False
            End If
        Catch ex As Exception

        End Try
    End Sub
    'Private Sub ExemptionsCheck()
    '    Try
    '        If chkEmpExemption.Checked = True Then
    '            'lstExemptions.Visible = True
    '            drpExemptions.Enabled = True
    '            'lblExemptions.Visible = True
    '        Else
    '            'lstExemptions.Visible = False
    '            drpExemptions.Enabled = False
    '            'lblExemptions.Visible = False
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub
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

    Private Sub ReloadList()
        Try
            lstExemptions.Items.Clear()
            Dim collection As IList(Of RadComboBoxItem) = drpExemptions.CheckedItems
            If (collection.Count > 0) Then
                For Each item As RadComboBoxItem In collection
                    'lstExemptions.Items.Add(item.Text)

                    Dim listitem As New RadListBoxItem()
                    listitem.Text = item.Text
                    listitem.Value = item.Value
                    lstExemptions.Items.Add(listitem)
                    listitem.DataBind()
                Next
            Else
                lstExemptions.Items.Clear()
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

                Process.LoadRadComboTextAndValueP1(drpExemptions, "Emp_PersonalDetail_Get_Employees", "", "Employee2", "empid", False)
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
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Finance_Deduction_get", Request.QueryString("id"))
                    payslipitem.Value = strUser.Tables(0).Rows(0).Item("title").ToString
                    amount.Value = strUser.Tables(0).Rows(0).Item("amount").ToString
                    radInputType.SelectedText = strUser.Tables(0).Rows(0).Item("inputtype").ToString
                    If IsDBNull(strUser.Tables(0).Rows(0).Item("deductiondate")) = False Then
                        radenddate.SelectedDate = strUser.Tables(0).Rows(0).Item("deductiondate").ToString
                    End If
                    note.Value = strUser.Tables(0).Rows(0).Item("note").ToString
                    position.Value = strUser.Tables(0).Rows(0).Item("ordering").ToString

                    If CBool(strUser.Tables(0).Rows(0).Item("Active").ToString) = True Then
                        isactive.Value = "Yes"
                    Else
                        isactive.Value = "No"
                    End If

                Else
                    radenddate.SelectedDate = Process.LastDay(2100, 12)
                    isactive.Value = "Yes"
                End If


                'ExemptionsCheck()

                ComponentsCheck()

                'Get Trainees
                Process.LoadListAndComboxFromDataset(lstExemptions, drpExemptions, "Finance_Deduction_Employee_Exemption_Get_Employee", "Employee", "empid", payslipitem.Value)
                Process.LoadListAndComboxFromDataset(lstComponents, radComponents, "Finance_Deduction_Component_Get", "Components", "Components", payslipitem.Value)

                'lstExemptions.Items.Clear()
                'Dim strTrainee As New DataSet
                'strTrainee = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Finance_Deduction_Employee_Exemption_Get_Employee", txtItem.Text)
                'If strTrainee.Tables(0).Rows.Count > 0 Then
                '    For z As Integer = 0 To strTrainee.Tables(0).Rows.Count - 1
                '        lstExemptions.Items.Add(strTrainee.Tables(0).Rows(z).Item("Employee").ToString)
                '        For l As Integer = 0 To drpExemptions.Items.Count - 1
                '            If drpExemptions.Items(l).Text.Contains(strTrainee.Tables(0).Rows(z).Item("EmpiD").ToString) Then
                '                drpExemptions.Items(l).Checked = True
                '                Exit For
                '            End If
                '        Next
                '    Next
                'End If

                ''Get Trainees
                'lstComponents.Items.Clear()
                'Dim strComponents As New DataSet
                'strComponents = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Finance_Deduction_Component_Get", txtItem.Text)
                'If strComponents.Tables(0).Rows.Count > 0 Then
                '    For z As Integer = 0 To strComponents.Tables(0).Rows.Count - 1
                '        lstComponents.Items.Add(strComponents.Tables(0).Rows(z).Item("Components").ToString)
                '        For l As Integer = 0 To radComponents.Items.Count - 1
                '            If radComponents.Items(l).Text.Contains(strComponents.Tables(0).Rows(z).Item("Components").ToString) Then
                '                radComponents.Items(l).Checked = True
                '                Exit For
                '            End If
                '        Next
                '    Next
                'End If


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
            cmd.CommandText = "Finance_Deduction_Update"
            cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = 0
            cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = payslipitem.Value
            cmd.Parameters.Add("@deductiontype", SqlDbType.VarChar).Value = "Recurring"
            cmd.Parameters.Add("@amount", SqlDbType.Decimal).Value = amount.Value
            cmd.Parameters.Add("@deductiondate", SqlDbType.Date).Value = radenddate.SelectedDate
            cmd.Parameters.Add("@note", SqlDbType.VarChar).Value = note.Value
            cmd.Parameters.Add("@empspecific", SqlDbType.Bit).Value = True
            cmd.Parameters.Add("@inputtype", SqlDbType.VarChar).Value = radInputType.SelectedText
            cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = Session("LoginID")
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
            Process.loadalert(divalert, msgalert, "")
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
            If radenddate.SelectedDate Is Nothing Then
                radenddate.SelectedDate = Process.LastDay(2100, 12)
            End If

            'Old Data
            If Request.QueryString("id") IsNot Nothing Then
                Dim strUser As New DataSet
                strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Finance_Deduction_get", Request.QueryString("id"))
                olddata(0) = strUser.Tables(0).Rows(0).Item("id").ToString
                olddata(1) = strUser.Tables(0).Rows(0).Item("title").ToString
                olddata(2) = strUser.Tables(0).Rows(0).Item("deductiontype").ToString
                olddata(3) = strUser.Tables(0).Rows(0).Item("amount").ToString
                olddata(4) = strUser.Tables(0).Rows(0).Item("deductiondate").ToString
                olddata(5) = strUser.Tables(0).Rows(0).Item("EmployeeSpecfic").ToString
                olddata(6) = strUser.Tables(0).Rows(0).Item("inputtype").ToString
                olddata(7) = strUser.Tables(0).Rows(0).Item("note").ToString
                olddata(8) = strUser.Tables(0).Rows(0).Item("ordering").ToString
                olddata(9) = strUser.Tables(0).Rows(0).Item("active").ToString
            End If

            If Request.QueryString("id") Is Nothing Then
                saldeduction.id = 0
            Else
                saldeduction.id = Request.QueryString("id")
            End If
            saldeduction.Amount = amount.Value
            If radenddate.SelectedDate Is Nothing Then
                saldeduction.DateOfDeduction = Process.LastDay(2100, 12)
                saldeduction.DateOfDeduction = Process.LastDay(2100, 12)
            Else
                saldeduction.DateOfDeduction = radenddate.SelectedDate
            End If

            saldeduction.DeductionType = "Recurring"
            saldeduction.ExemptSomeEmployees = True
            saldeduction.Note = note.Value
            saldeduction.Title = payslipitem.Value.Trim
            saldeduction.InputType = radInputType.SelectedText
            Dim boolActive As Boolean = False
            If isactive.Value = "Yes" Then
                boolActive = True
            Else
                boolActive = False
            End If
            saldeduction.Active = boolActive
            saldeduction.Order = position.Value
            Dim OldValue As String = ""
            Dim NewValue As String = ""

            Dim j As Integer = 0

            If Request.QueryString("id") IsNot Nothing Then
                For Each a In GetType(clsDeductions).GetProperties()
                    If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If IsNumeric(a.GetValue(saldeduction, Nothing)) = True And IsNumeric(olddata(j)) = True Then
                                If CDbl(a.GetValue(saldeduction, Nothing)) <> CDbl(olddata(j)) Then
                                    NewValue += a.Name + ": " + a.GetValue(saldeduction, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            Else
                                If a.GetValue(saldeduction, Nothing).ToString <> olddata(j).ToString Then
                                    NewValue += a.Name + ": " + a.GetValue(saldeduction, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            End If
                        End If
                    End If
                    j = j + 1
                Next
            Else
                For Each a In GetType(clsDeductions).GetProperties() 'New Entries
                    If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If a.GetValue(saldeduction, Nothing) = Nothing Then
                                NewValue += a.Name + ":" + " " & vbCrLf
                            Else
                                NewValue += a.Name + ": " + a.GetValue(saldeduction, Nothing).ToString & vbCrLf
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
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Inserted", "Pay Slip Deduction Items")
                End If

            Else
                sid = saldeduction.id
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Finance_Deduction_update", saldeduction.id, saldeduction.Title, saldeduction.DeductionType, saldeduction.Amount, saldeduction.DateOfDeduction, saldeduction.Note, saldeduction.ExemptSomeEmployees, saldeduction.InputType, saldeduction.Active, saldeduction.Order, Session("LoginID"))
                If NewValue.Trim = "" And OldValue.Trim = "" Then
                Else
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Updated ID: " & saldeduction.id, "Pay Slip Deduction Items")
                End If
            End If


            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Finance_Deduction_Component_Delete", saldeduction.Title)
            'Components
            If radInputType.SelectedText.Contains("Percentage") = True Then
                If radComponents.CheckedItems.Count > 0 Then
                    For d As Integer = 0 To radComponents.CheckedItems.Count - 1
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Finance_Deduction_Component_Add", saldeduction.Title, radComponents.CheckedItems.Item(d).Text)
                    Next
                End If
            Else
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Finance_Deduction_Component_Delete", saldeduction.Title)
            End If

            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Finance_Deduction_Employee_Delete", saldeduction.Title)
            'If saldeduction.id <> 0 Then

            If drpExemptions.CheckedItems.Count > 0 Then
                For d As Integer = 0 To drpExemptions.CheckedItems.Count - 1
                    EmpID = drpExemptions.CheckedItems.Item(d).Value
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Finance_Deduction_Employee_Add", EmpID, saldeduction.Title)
                Next

            Else
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Finance_Deduction_Employee_Delete", saldeduction.Title)
            End If
            Process.LoadListAndComboxFromDataset(lstExemptions, drpExemptions, "Finance_Deduction_Employee_Exemption_Get_Employee", "Employee", "empid", payslipitem.Value)
            Process.LoadListAndComboxFromDataset(lstComponents, radComponents, "Finance_Deduction_Component_Get", "Components", "Components", payslipitem.Value)

            If Request.QueryString("id") IsNot Nothing Then
                Process.loadalert(divalert, msgalert, "Record saved", "success")
            Else
                Response.Redirect("~/Module/Admin/FinanceAdmin/DeductionsUpdate?id=" & sid, True)
            End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            Session("isnew") = "0"
            Response.Redirect("~/Module/Admin/FinanceAdmin/Deductions", True)

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    'Protected Sub chkEmpExemption_CheckedChanged(sender As Object, e As EventArgs) Handles chkEmpExemption.CheckedChanged

    '    ExemptionsCheck()
    'End Sub

    Private Sub drpExemptions_ItemChecked(sender As Object, e As Telerik.Web.UI.RadComboBoxItemEventArgs) Handles drpExemptions.ItemChecked
        ReloadList()
    End Sub



    Private Sub radComponents_ItemChecked(sender As Object, e As Telerik.Web.UI.RadComboBoxItemEventArgs) Handles radComponents.ItemChecked
        ReloadComponentList()
    End Sub


    Protected Sub radInputType_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.DropDownListEventArgs) Handles radInputType.SelectedIndexChanged
        ComponentsCheck()
    End Sub
End Class