Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO


Public Class PensionManagerUpdate
    Inherits System.Web.UI.Page
    Dim loanrule As New clsLoanRule
    Dim AuthenCode As String = "PENMGR"
    Dim olddata(8) As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                Process.LoadRadComboTextAndValueP2(cboEmployee, "Emp_PersonalDetail_get_all_Specific", "", Session("company"), "Employee2", "EmpID")
                Process.LoadRadComboTextAndValueInitiate(cboPenMgr, "Finance_Pension_Get_PenManagers", "--select--", "PensionManager", "PensionManager")
                'Company_Structure_get_parent
                If Request.QueryString("id") IsNot Nothing Then
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Finance_Pension_Get", Request.QueryString("id"))
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    Process.AssignRadComboValue(cboEmployee, strUser.Tables(0).Rows(0).Item("empid").ToString)
                    Process.AssignRadComboValue(cboPenMgr, strUser.Tables(0).Rows(0).Item("pensionmanager").ToString)
                    txtPensionMgr.Visible = False
                    txtRSA.Value = strUser.Tables(0).Rows(0).Item("RSACode").ToString
                Else
                    txtid.Text = "0"
                End If

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Function GetIdentity(ByVal empid As String, ByVal rsacode As String, ByVal penmanager As String) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Finance_Pension_Update"
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@empid", SqlDbType.VarChar).Value = empid
            cmd.Parameters.Add("@rsacode", SqlDbType.VarChar).Value = rsacode
            cmd.Parameters.Add("@penmanager", SqlDbType.VarChar).Value = penmanager
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception
            Process.strExp = ex.Message
            Return 0
        End Try
    End Function


    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "danger")
                Exit Sub
            End If
            Dim lblstatus As String
            If cboEmployee.SelectedItem.Text.ToLower.Contains("--select") = True Then
                lblstatus = "Employee required!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                cboEmployee.Focus()
                Exit Sub
            End If


            If txtRSA.Value.Trim = "" Then
                lblstatus = "Employee RSA required!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                txtRSA.Focus()
                Exit Sub
            End If

            If (cboPenMgr.SelectedItem.Text.ToLower.Contains("--select")) = True Then
                lblstatus = "Pension Manager required!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                cboPenMgr.Focus()
                Exit Sub
            End If

            If txtPensionMgr.Visible = True Then
                If txtPensionMgr.Text.Trim = "" Then
                    lblstatus = "Pension Manager required!"
                    Process.loadalert(divalert, msgalert, lblstatus, "danger")
                    txtPensionMgr.Focus()
                    Exit Sub
                End If
            End If

            Dim penmgr As String = ""

            If txtPensionMgr.Visible = True Then
                penmgr = txtPensionMgr.Text
            Else
                penmgr = cboPenMgr.SelectedValue
            End If





            If txtid.Text <> "0" And txtid.Text.Trim <> "" Then
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Finance_Pension_Update", txtid.Text, cboEmployee.SelectedValue, txtRSA.Value.Trim.ToUpper, penmgr)
            Else
                txtid.Text = GetIdentity(cboEmployee.SelectedValue, txtRSA.Value.Trim.ToUpper, penmgr)

                If txtid.Text = "0" Then
                    lblstatus = Process.strExp
                    Process.loadalert(divalert, msgalert, lblstatus, "danger")
                    Exit Sub
                End If
            End If

            lblstatus = "Record saved"
            Process.loadalert(divalert, msgalert, lblstatus, "success")
            'RadWindowManager1.RadAlert("An <br /><b>html</b> string.<br />", 200, 100, "Record saved", "callBackFn", "myAlertImage.png")
            'Response.Write("<script language='javascript'> { self.close() }</script>")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            'Response.Write("<script language='javascript'> { self.close() }</script>")
            Response.Redirect("PensionManager")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


   

    Protected Sub cboPenMgr_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboPenMgr.SelectedIndexChanged
        Try
            If cboPenMgr.SelectedValue.ToUpper = "OTHERS" Then
                txtPensionMgr.Visible = True
                txtPensionMgr.Focus()
            Else
                txtPensionMgr.Visible = False
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class