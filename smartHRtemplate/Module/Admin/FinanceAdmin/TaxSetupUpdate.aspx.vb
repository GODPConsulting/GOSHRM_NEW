Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Telerik.Web.UI


Public Class TaxSetupUpdate
    Inherits System.Web.UI.Page
    Dim saldeduction As New clsDeductions
    Dim AuthenCode As String = "TAX"
    Dim olddata(8) As String
    Dim Level(2) As String
    Dim Separators() As Char = {":"c}
    Dim EmpID As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                radTaxRange.Items.Clear()
                radTaxRange.Items.Add("FIRST")
                radTaxRange.Items.Add("NEXT")
                radTaxRange.Items.Add("OVER")
                If Request.QueryString("id") IsNot Nothing Then
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Finance_Tax_Range_Get", Request.QueryString("id"))
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    Process.AssignRadDropDownValue(radTaxRange, strUser.Tables(0).Rows(0).Item("Range").ToString)
                    uppervalue.Value = strUser.Tables(0).Rows(0).Item("UpperValue").ToString
                    rate.Value = strUser.Tables(0).Rows(0).Item("Rate").ToString
                    txttaxid.Text = strUser.Tables(0).Rows(0).Item("taxid").ToString
                Else
                    txtid.Text = "0"
                    txttaxid.Text = Request.QueryString("taxid")
                End If

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            If txtid.Text <> "0" Then
                If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False Then
                    Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "warning")
                    Exit Sub
                End If
            End If
           


            If (radTaxRange.SelectedText = Nothing) Then
                Process.loadalert(divalert, msgalert, "Range Type required!", "warning")
                radTaxRange.Focus()
                Exit Sub
            End If

            If (IsNumeric(rate.Value) = False) Then
                Process.loadalert(divalert, msgalert, "Rate required!", "warning")
                rate.Focus()
                Exit Sub
            End If

            If (IsNumeric(uppervalue.Value) = False) Then
                Process.loadalert(divalert, msgalert, "Upper Value required!", "warning")
                uppervalue.Focus()
                Exit Sub
            End If

            If txtid.Text = "0" Then
                txtid.Text = GetIdentity()
            Else
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Finance_Tax_Range_Update", txtid.Text, radTaxRange.SelectedText, uppervalue.Value, rate.Value, txttaxid.Text, Session("LoginID").ToString)
            End If

            If txtid.Text = "0" Then
                Exit Sub
            End If

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
            cmd.CommandText = "Finance_Tax_Range_Update"
            cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = txtid.Text
            cmd.Parameters.Add("@Range", SqlDbType.VarChar).Value = radTaxRange.SelectedText
            cmd.Parameters.Add("@UpperValue", SqlDbType.Decimal).Value = uppervalue.Value
            cmd.Parameters.Add("@Rate", SqlDbType.Decimal).Value = rate.Value
            cmd.Parameters.Add("@taxid", SqlDbType.VarChar).Value = txttaxid.Text
            cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = Session("LoginID")

            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
            Return 0
        End Try
    End Function
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect("~/Module/Admin/FinanceAdmin/TaxSetup.aspx?id=" & txttaxid.Text, True)
        Catch ex As Exception

        End Try
    End Sub


End Class