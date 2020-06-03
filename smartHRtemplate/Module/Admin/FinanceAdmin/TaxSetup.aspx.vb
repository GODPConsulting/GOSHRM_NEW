Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Telerik.Web.UI

Public Class TaxSetup
    Inherits System.Web.UI.Page
    Dim mailconfig As New clsMailConfig
    Dim AuthenCode As String = "TAX"
    Dim olddata(14) As String
    Dim oFileAttached1 As Byte
    Dim oFileAttached As Byte()
    Private Sub LoadGrid(sid As Integer)
        Try

            GridVwHeaderChckbox.DataSource = Process.SearchData("Finance_Tax_Range_Get_All", sid)
            GridVwHeaderChckbox.AllowSorting = False
            GridVwHeaderChckbox.AllowPaging = True
            GridVwHeaderChckbox.DataBind()

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not Me.IsPostBack Then
                Dim ismulti As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select isnull(ismulticompany,'No')  from general_info")
                If Request.QueryString("id") IsNot Nothing Then
                    If ismulti.ToLower = "no" Then
                        Process.LoadRadComboTextAndValueP2(cboCompany, "Company_Structure_Get_ByLevel", "1", Session("Access"), "name", "name", False)
                    Else
                        Process.LoadRadComboTextAndValueP2(cboCompany, "Company_Structure_Get_ByLevel", "2", Session("Access"), "name", "name", False)
                    End If

                    Dim strTax As New DataSet
                    strTax = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Finance_Tax_Get", Request.QueryString("id"))
                    If strTax.Tables(0).Rows.Count > 0 Then
                        lblid.Text = strTax.Tables(0).Rows(0).Item("id").ToString
                        txtTaxID.Text = strTax.Tables(0).Rows(0).Item("TaxID").ToString
                        taxdesc.Value = strTax.Tables(0).Rows(0).Item("TaxDescription").ToString
                        taxrelief.Value = strTax.Tables(0).Rows(0).Item("ExemptedTaxAmount").ToString
                        incomerelief.Value = strTax.Tables(0).Rows(0).Item("incomerelief").ToString
                        fixedrelief.Value = strTax.Tables(0).Rows(0).Item("FixedIncomeRelief").ToString

                        If CBool(strTax.Tables(0).Rows(0).Item("ApplyToPayroll")) = True Then
                            isactive.Value = "Yes"
                        Else
                            isactive.Value = "No"
                        End If

                        Process.AssignRadComboValue(cboCompany, strTax.Tables(0).Rows(0).Item("company").ToString)
                        cboCompany.Enabled = False
                    End If
                    divdetails.Visible = True
                Else
                    If ismulti.ToLower = "no" Then
                        Process.LoadRadComboTextAndValueP2(cboCompany, "Company_Structure_Get_ByLevel_Tax", "1", Session("Access"), "name", "name", False)
                    Else
                        Process.LoadRadComboTextAndValueP2(cboCompany, "Company_Structure_Get_ByLevel_Tax", "2", Session("Access"), "name", "name", False)
                    End If

                    txtTaxID.Text = "PAYE"
                    taxdesc.Focus()
                    lblid.Text = 0
                    divdetails.Visible = False
                End If
                LoadGrid(lblid.Text)
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            If Request.QueryString("id") IsNot Nothing Then
                If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False Then
                    Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "warning")
                    Exit Sub
                End If
            End If


            If (txtTaxID.Text.Trim = "") Then
                Process.loadalert(divalert, msgalert, "Tax ID required!", "warning")
                txtTaxID.Focus()
                Exit Sub
            End If

            If (taxdesc.Value.Trim = "") Then
                Process.loadalert(divalert, msgalert, "Payslip Description required!", "warning")
                taxdesc.Focus()
                Exit Sub
            End If

            If IsNumeric(incomerelief.Value) = False Then
                Process.loadalert(divalert, msgalert, "Monthly Tax Relief required!", "warning")
                incomerelief.Focus()
                Exit Sub
            End If

            If IsNumeric(incomerelief.Value) = False Then
                Process.loadalert(divalert, msgalert, "Gross Income Relief required!", "warning")
                incomerelief.Focus()
                Exit Sub
            End If

            Dim boolActive As Boolean = False
            If isactive.Value = "Yes" Then
                boolActive = True
            Else
                boolActive = False
            End If

            If lblid.Text = "0" Then
                lblid.Text = GetIdentity()
            Else
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Finance_Tax_Update", lblid.Text, cboCompany.SelectedValue, txtTaxID.Text.Trim, taxdesc.Value.Trim, taxrelief.Value, incomerelief.Value, boolActive, fixedrelief.Value)
            End If
            If lblid.Text = "0" Then
                Exit Sub
            End If

            divdetails.Visible = True

            Process.loadalert(divalert, msgalert, "Record saved!", "success")
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
            cmd.CommandText = "Finance_Tax_Update"
            cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = lblid.Text
            cmd.Parameters.Add("@company", SqlDbType.VarChar).Value = cboCompany.SelectedValue
            cmd.Parameters.Add("@TaxID", SqlDbType.VarChar).Value = txtTaxID.Text.Trim
            cmd.Parameters.Add("@Desc", SqlDbType.VarChar).Value = taxdesc.Value.Trim
            cmd.Parameters.Add("@Amount", SqlDbType.Decimal).Value = taxrelief.Value
            cmd.Parameters.Add("@Rate", SqlDbType.Decimal).Value = incomerelief.Value
            cmd.Parameters.Add("@fixedrelief", SqlDbType.Decimal).Value = fixedrelief.Value

            Dim boolActive As Boolean = False
            If isactive.Value = "Yes" Then
                boolActive = True
            Else
                boolActive = False
            End If
            cmd.Parameters.Add("@Apply", SqlDbType.Bit).Value = boolActive
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
            Return 0
        End Try
    End Function
    Protected Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDeleteTax.Click
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Delete") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "warning")
                Exit Sub
            End If
            Dim count As Integer = 0
            Dim confirmValue As String = Request.Form("confirm_value")
            If confirmValue = "Yes" Then
                Dim atLeastOneRowDeleted As Boolean = False
                ' Iterate through the Products.Rows property
                For Each row As GridViewRow In GridVwHeaderChckbox.Rows
                    ' Access the CheckBox
                    Dim cb As CheckBox = row.FindControl("chkEmp")
                    If cb IsNot Nothing AndAlso cb.Checked Then
                        ' Delete row! (Well, not really...)
                        count = count + 1
                        atLeastOneRowDeleted = True
                        ' First, get the ProductID for the selected row
                        Dim ID As String = _
                            Convert.ToString(GridVwHeaderChckbox.DataKeys(row.RowIndex).Value)
                        ' "Delete" the row
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Finance_Deduction_delete", ID)
                    End If
                Next
                Process.loadalert(divalert, msgalert, count.ToString & " records successfully deleted", "success")
                LoadGrid(lblid.Text)

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                Process.loadalert(divalert, msgalert, Process.privilegemsg, "warning")
                Exit Sub
            End If
            Response.Redirect("~/Module/Admin/FinanceAdmin/TaxSetupUpdate?taxid=" + lblid.Text, True)

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnValidate_Click(sender As Object, e As EventArgs)
        Try
            Process.loadalert(divalert, msgalert, SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Finance_Tax_Validate", lblid.Text), "info")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect("~/Module/Admin/FinanceAdmin/Tax", True)
        Catch ex As Exception

        End Try
    End Sub
End Class