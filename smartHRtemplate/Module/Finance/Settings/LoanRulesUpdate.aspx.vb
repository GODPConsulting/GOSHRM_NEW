Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Telerik.Web.UI


Public Class LoanRulesUpdate
    Inherits System.Web.UI.Page
    Dim loanrule As New clsLoanRule
    Dim AuthenCode As String = "LOANRULE"
    Dim olddata(7) As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                Process.LoadRadComboTextAndValue(cboLoanType, "Loan_Type_get_all", "name", "name", False)
                Process.LoadRadComboTextAndValue(cbojobstatus, "employment_status_get_all", "name", "name", False)
                Process.LoadRadComboTextAndValue(cboJobGrade, "Job_Grade_get_all", "name", "name", False)

                cboruletype.Items.Clear()
                cboruletype.Items.Add("Fixed Amount")
                cboruletype.Items.Add("Percentage")

                cboapplygratuity.Items.Clear()
                cboapplygratuity.Items.Add("No")
                cboapplygratuity.Items.Add("Yes")

                cboconfirmedstaff.Items.Clear()
                cboconfirmedstaff.Items.Add("No")
                cboconfirmedstaff.Items.Add("Yes")


                'Company_Structure_get_parent
                If Request.QueryString("id") IsNot Nothing Then
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Loan_Rules_get", Request.QueryString("id"))
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    arulename.Value = strUser.Tables(0).Rows(0).Item("rulename").ToString
                    Process.AssignRadComboValue(cboLoanType, strUser.Tables(0).Rows(0).Item("LoanType").ToString)
                    Process.LoadListAndComboxFromDataset(lstMakeup, cboJobGrade, "Loan_Rule_JobGrade_Get_All", "jobgrade", "jobgrade", txtid.Text)
                    Process.AssignRadComboValue(cbojobstatus, strUser.Tables(0).Rows(0).Item("EmploymentStatus").ToString)
                    Process.AssignRadComboValue(cboruletype, strUser.Tables(0).Rows(0).Item("itemtype").ToString)
                    Process.AssignRadComboValue(cboapplygratuity, strUser.Tables(0).Rows(0).Item("usegratuityrule").ToString)
                    Process.AssignRadComboValue(cboconfirmedstaff, strUser.Tables(0).Rows(0).Item("confirmedonly").ToString)
                    aloantenor.Value = strUser.Tables(0).Rows(0).Item("maxrepayperiod").ToString
                    arepayfactor.Value = strUser.Tables(0).Rows(0).Item("netfactor").ToString
                    If cboruletype.SelectedItem.Text.ToLower = "percentage" Then
                        lbruletype.Visible = True
                    Else
                        lbruletype.Visible = False
                    End If

                    aminservicemth.Value = strUser.Tables(0).Rows(0).Item("minmthsofservice").ToString
                    amaxservicemth.Value = strUser.Tables(0).Rows(0).Item("maxmthservice").ToString
                    aloanamount.Value = strUser.Tables(0).Rows(0).Item("Limit").ToString
                    aloanintrate.Value = strUser.Tables(0).Rows(0).Item("interestrate").ToString
                    aloanmarketrate.Value = strUser.Tables(0).Rows(0).Item("marketrate").ToString
              

                    If IsDate(strUser.Tables(0).Rows(0).Item("AddedOn")) = True Then
                        createdon.InnerText = "Created on " & CDate(strUser.Tables(0).Rows(0).Item("AddedOn")).ToLongDateString & " by " & strUser.Tables(0).Rows(0).Item("addedby").ToString
                    End If
                    If IsDate(strUser.Tables(0).Rows(0).Item("UpdatedOn")) = True Then
                        updatedon.InnerText = "Last modified on " & CDate(strUser.Tables(0).Rows(0).Item("UpdatedOn")).ToLongDateString & " by " & strUser.Tables(0).Rows(0).Item("updatedby").ToString
                    End If
                Else
                    txtid.Text = "0"
                    aloanamount.Value = "0"
                    aminservicemth.Value = "0"
                    aloanintrate.Value = "0"
                    aloanmarketrate.Value = "0"
                    arulename.Value = ""
                    Process.AssignRadComboValue(cboruletype, "Fixed Amount")
                    Process.AssignRadComboValue(cboapplygratuity, "No")
                    Process.AssignRadComboValue(cboconfirmedstaff, "Yes")
                    If cboruletype.SelectedItem.Text = "Percentage" Then
                        lbruletype.Visible = True
                    Else
                        lbruletype.Visible = False
                    End If
                End If

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.message, "danger")
        End Try
    End Sub
    Private Function GetIdentity(ByVal loanname As String, ByVal emptype As String, ByVal intrate As Decimal, ByVal marketrate As Decimal, ByVal eirate As Decimal, ByVal limit As Decimal, ByVal empuserid As String, ByVal ruletype As String) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Loan_Rules_update"
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = txtid.Text
            cmd.Parameters.Add("@LoanType", SqlDbType.VarChar).Value = loanname
            cmd.Parameters.Add("@EmploymentStatus", SqlDbType.VarChar).Value = emptype
            cmd.Parameters.Add("@Rate", SqlDbType.Decimal).Value = intrate
            cmd.Parameters.Add("@MarketRate", SqlDbType.Decimal).Value = marketrate
            cmd.Parameters.Add("@EIRate", SqlDbType.Decimal).Value = eirate
            cmd.Parameters.Add("@Limit", SqlDbType.Decimal).Value = limit
            cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = empuserid
            cmd.Parameters.Add("@itemtype", SqlDbType.VarChar).Value = ruletype
            cmd.Parameters.Add("@netfactor", SqlDbType.VarChar).Value = arepayfactor.Value
            cmd.Parameters.Add("@usegratuityrule", SqlDbType.VarChar).Value = cboapplygratuity.SelectedItem.Text
            cmd.Parameters.Add("@confirmedonly", SqlDbType.VarChar).Value = cboconfirmedstaff.SelectedItem.Text
            cmd.Parameters.Add("@minmthsofservice", SqlDbType.Int).Value = aminservicemth.Value
            cmd.Parameters.Add("@maxmthservice", SqlDbType.Int).Value = amaxservicemth.Value
            cmd.Parameters.Add("@maxrepayperiod", SqlDbType.Int).Value = aloantenor.Value
            cmd.Parameters.Add("@rulename", SqlDbType.VarChar).Value = arulename.Value
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
            If txtid.Text <> "0" Then
                If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False Then
                    Process.loadalert(divalert, msgalert, Process.privilegemsg, "warning")
                    Exit Sub
                End If
            End If
            Dim lblstatus As String = ""

            Dim collection As IList(Of RadComboBoxItem) = cboJobGrade.CheckedItems
            If (collection.Count <= 0) Then
                lblstatus = "job grades required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                cboJobGrade.Focus()
                Exit Sub
            End If

            If arulename.Value.Trim = "" Then
                lblstatus = "loan rule name required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                arulename.Focus()
                Exit Sub
            End If

            If (cbojobstatus.SelectedValue Is Nothing) Then
                lblstatus = "Employment Status required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                cbojobstatus.Focus()
                Exit Sub
            End If

            If IsNumeric(aloanintrate.Value) = False Then
                aloanintrate.Value = "0"
            End If
            If IsNumeric(aloanmarketrate.Value) = False Then
                aloanmarketrate.Value = "0"
            End If

            If IsNumeric(aloanamount.Value) = False Then
                lblstatus = "Maximum Loan Amount required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                aloanamount.Focus()
                Exit Sub
            End If

            If IsNumeric(arepayfactor.Value) = False Then
                lblstatus = "Repayment Factor required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                arepayfactor.Focus()
                Exit Sub
            Else
                If arepayfactor.Value.Trim = "" Then
                    arepayfactor.Value = "0"
                End If
            End If

            If IsNumeric(aminservicemth.Value) = False Then
                lblstatus = "Minimum eligible month of service required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                aminservicemth.Focus()
                Exit Sub
            End If

            If IsNumeric(amaxservicemth.Value) = False Then
                lblstatus = "Maximum eligible month of service required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                amaxservicemth.Focus()
                Exit Sub
            End If

            If CInt(aminservicemth.Value) > CInt(amaxservicemth.Value) Then
                lblstatus = "invalid minimum or maximum eligible month of service!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                amaxservicemth.Focus()
                Exit Sub
            End If


            'Old Data
            If txtid.Text <> "0" And txtid.Text.Trim <> "" Then
                Dim strUser As New DataSet
                strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Loan_Rules_get", txtid.Text)
                olddata(0) = strUser.Tables(0).Rows(0).Item("id").ToString
                olddata(1) = strUser.Tables(0).Rows(0).Item("rulename").ToString
                olddata(2) = strUser.Tables(0).Rows(0).Item("LoanType").ToString
                olddata(3) = strUser.Tables(0).Rows(0).Item("EmploymentStatus").ToString
                olddata(4) = strUser.Tables(0).Rows(0).Item("Interestrate").ToString
                olddata(5) = strUser.Tables(0).Rows(0).Item("Marketrate").ToString
                olddata(6) = strUser.Tables(0).Rows(0).Item("Limit").ToString
            End If

            If txtid.Text.Trim = "" Or txtid.Text.Trim = "0" Then
                loanrule.id = 0
            Else
                loanrule.id = txtid.Text
            End If


            loanrule.LoanType = cboLoanType.SelectedValue
            loanrule.EmploymentStatus = cbojobstatus.SelectedValue
            loanrule.Amount = aloanamount.Value
            loanrule.InterestRate = aloanintrate.Value
            loanrule.MarketRate = aloanmarketrate.Value
            loanrule.RuleName = arulename.Value

            Dim OldValue As String = ""
            Dim NewValue As String = ""

            Dim j As Integer = 0

            If txtid.Text <> "0" And txtid.Text.Trim <> "" Then 'Updates
                For Each a In GetType(clsLoanRule).GetProperties()
                    If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If IsNumeric(a.GetValue(loanrule, Nothing)) = True And IsNumeric(olddata(j)) = True Then
                                If CDbl(a.GetValue(loanrule, Nothing)) <> CDbl(olddata(j)) Then
                                    NewValue += a.Name + ": " + a.GetValue(loanrule, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            Else
                                If a.GetValue(loanrule, Nothing).ToString <> olddata(j).ToString Then
                                    NewValue += a.Name + ": " + a.GetValue(loanrule, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            End If
                        End If
                    End If
                    j = j + 1
                Next
            Else
                For Each a In GetType(clsLoanRule).GetProperties() 'New Entries
                    If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If a.GetValue(loanrule, Nothing) = Nothing Then
                                NewValue += a.Name + ":" + " " & vbCrLf
                            Else
                                NewValue += a.Name + ": " + a.GetValue(loanrule, Nothing).ToString & vbCrLf
                            End If
                        End If
                    End If
                Next
            End If

            'If txtid.Text <> "0" And txtid.Text.Trim <> "" Then
            '    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Loan_Rules_update", loanrule.id, loanrule.LoanType, loanrule.JobGrade, loanrule.EmploymentStatus, loanrule.InterestRate, loanrule.MarketRate, 0, loanrule.Amount, Session("LoginID"), drpType.SelectedItem.Text, radConfirmedStaff.SelectedItem.Text, txtmthofservice.Text)
            'Else
            '    txtid.Text = GetIdentity(loanrule.LoanType, loanrule.JobGrade, loanrule.EmploymentStatus, loanrule.InterestRate, loanrule.MarketRate, 0, loanrule.Amount, Session("LoginID"), drpType.SelectedItem.Text)
            '    If txtid.Text = "0" Then
            '        lblstatus.Text = Process.strExp
            '        Exit Sub
            '    End If
            'End If


            If txtid.Text = "0" Then
                txtid.Text = GetIdentity(loanrule.LoanType, loanrule.EmploymentStatus, loanrule.InterestRate, loanrule.MarketRate, 0, loanrule.Amount, Session("LoginID"), cboruletype.SelectedItem.Text)
                If txtid.Text = "0" Then
                    Exit Sub
                End If
            Else
                If GetIdentity(loanrule.LoanType, loanrule.EmploymentStatus, loanrule.InterestRate, loanrule.MarketRate, 0, loanrule.Amount, Session("LoginID"), cboruletype.SelectedItem.Text) = "0" Then
                    Exit Sub
                End If
            End If



            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Loan_Rule_JobGrade_Delete", txtid.Text)
            For d As Integer = 0 To lstMakeup.Items.Count - 1
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Loan_Rule_JobGrade_Update", txtid.Text, lstMakeup.Items(d).Text)
            Next

            If NewValue.Trim = "" And OldValue.Trim = "" Then
            Else
                If txtid.Text <> "0" And txtid.Text.Trim <> "" Then
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Updated ID: " & loanrule.id, "Loan Rules")
                Else
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Inserted", "Loan Rules")
                End If
            End If
            Process.LoadListAndComboxFromDataset(lstMakeup, cboJobGrade, "Loan_Rule_JobGrade_Get_All", "JOBGRADE", "JOBGRADE", txtid.Text)

            lblstatus = "Record saved"
            Process.loadalert(divalert, msgalert, lblstatus, "success")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect("LoanRules", True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


    Protected Sub drpType_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboruletype.SelectedIndexChanged
        Try
            If cboruletype.SelectedItem.Text = "Percentage" Then
                lbruletype.Visible = True
            Else
                lbruletype.Visible = False
            End If
        Catch ex As Exception

        End Try
    End Sub

   

    Protected Sub cboJobGrade_ItemChecked(sender As Object, e As Telerik.Web.UI.RadComboBoxItemEventArgs) Handles cboJobGrade.ItemChecked
        Try
            Process.LoadListBoxFromCombo(lstMakeup, cboJobGrade)

            lstMakeup.Items.Clear()
            Dim collection As IList(Of RadComboBoxItem) = cboJobGrade.CheckedItems
            If (collection.Count <> 0) Then
                For Each item As RadComboBoxItem In collection
                    Dim listitem As New RadListBoxItem()
                    listitem.Text = item.Text
                    listitem.Value = item.Value
                    lstMakeup.Items.Add(listitem)
                    listitem.DataBind()
                Next
            Else
                lstMakeup.Items.Clear()
            End If
            lstMakeup.Visible = True
        Catch ex As Exception

        End Try
    End Sub
End Class