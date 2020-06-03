Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Telerik.Web.UI

Public Class GratuityUpdate
    Inherits System.Web.UI.Page
    Dim paygrade As New clsPayGrade1
    Dim AuthenCode As String = "GRATUITY"
    Dim olddata(3) As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                cboYear.Items.Clear()
                cboMonth.Items.Clear()
                For z As Integer = 2015 To 2050
                    Dim itemTemp As New RadComboBoxItem()
                    itemTemp.Text = z.ToString
                    itemTemp.Value = z.ToString
                    cboYear.Items.Add(itemTemp)
                    itemTemp.DataBind()
                    If (z = Now.Year) Then
                        Exit For
                    End If
                Next

                Dim strmonth As New DataSet
                strmonth = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "select * from dbo.Calendar('finance',2016) order by id")
                If strmonth.Tables(0).Rows.Count > 0 Then
                    For z As Integer = 0 To strmonth.Tables(0).Rows.Count - 1
                        Dim itemTemp As New RadComboBoxItem()
                        itemTemp.Text = strmonth.Tables(0).Rows(z).Item("calmonths").ToString
                        itemTemp.Value = strmonth.Tables(0).Rows(z).Item("id").ToString
                        cboMonth.Items.Add(itemTemp)
                        itemTemp.DataBind()
                    Next
                End If
                txtAmount.Value = 0
                Process.LoadRadComboTextAndValueP1(cboemp, "Emp_PersonalDetail_Get_Employees", Session("Access"), "Employee3", "empid", False)
                'Holidays_get_parent
                If Request.QueryString("id") IsNot Nothing Then
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Finance_Gratuity_Get", Request.QueryString("id"))
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    txtAmount.Value = strUser.Tables(0).Rows(0).Item("amount").ToString
                    Process.AssignRadComboValue(cboMonth, strUser.Tables(0).Rows(0).Item("monthno").ToString)
                    Process.AssignRadComboValue(cboYear, strUser.Tables(0).Rows(0).Item("year").ToString)
                    Process.AssignRadComboValue(cboemp, strUser.Tables(0).Rows(0).Item("empid").ToString)
                    lblentrystatus.Text = strUser.Tables(0).Rows(0).Item("entrystatus").ToString

                    If IsDBNull(strUser.Tables(0).Rows(0).Item("createdby")) = False Then
                        lblcreatedby.Value = strUser.Tables(0).Rows(0).Item("createdby").ToString
                    End If
                    If IsDBNull(strUser.Tables(0).Rows(0).Item("createdon")) = False Then
                        lblcreatedon.Value = strUser.Tables(0).Rows(0).Item("createdon").ToString
                    End If
                    If IsDBNull(strUser.Tables(0).Rows(0).Item("updatedby")) = False Then
                        lblupdatedby.Value = strUser.Tables(0).Rows(0).Item("updatedby").ToString
                    End If
                    If IsDBNull(strUser.Tables(0).Rows(0).Item("updatedon")) = False Then
                        lblupdatedon.Value = strUser.Tables(0).Rows(0).Item("updatedon").ToString
                    End If
                    cboemp.Enabled = False
                    cboYear.Enabled = False
                    cboMonth.Enabled = False
                Else
                    txtid.Text = "0"
                    txtAmount.Value = "0"
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
            cmd.CommandText = "Finance_Gratuity_update"
            cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = txtid.Text
            cmd.Parameters.Add("@empid", SqlDbType.VarChar).Value = cboemp.SelectedValue
            cmd.Parameters.Add("@month", SqlDbType.VarChar).Value = cboMonth.SelectedItem.Text
            cmd.Parameters.Add("@year", SqlDbType.VarChar).Value = cboYear.SelectedValue
            cmd.Parameters.Add("@amount", SqlDbType.VarChar).Value = txtAmount.Value
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

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            Dim lblstatus As String
            If (IsNumeric(txtAmount.Value) = False) Then
                lblstatus = "Amount required!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                txtAmount.Focus()
                Exit Sub
            End If


            txtid.Text = GetIdentity()
            If txtid.Text = "0" Then
                Exit Sub
            End If
            lblstatus = "Record saved"
            Process.loadalert(divalert, msgalert, lblstatus, "success")
            'Response.Write("<script language='javascript'> { self.close() }</script>")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect("gratuity.aspx")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

End Class