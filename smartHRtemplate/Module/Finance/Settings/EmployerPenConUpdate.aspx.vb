Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Microsoft.VisualBasic


Public Class EmployerPenConUpdate
    Inherits System.Web.UI.Page
    Dim TimeSheet As New clsTimeSheet
    Dim ApproveLeave As New clsApproveLeave
    Dim AuthenCode As String = "EMPPENSION"
    Dim olddata(11) As String
    Dim emp_emailaddr As String
    Dim approver1_emailaddr As String
    Dim approver2_emailaddr As String
    Dim LeaveBalance As Integer = 0
    Dim NoDays As Integer = 0
    Dim Level1(2) As String
    Dim Level2(2) As String
    Dim Separators() As Char = {":"c}
    Dim EmpID_1 As String = ""
    Dim EmpID_2 As String = ""
    Dim EmpID_1_Name As String = ""
    Dim EmpID_2_Name As String = ""
    Dim isEligible As String = "Yes"



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            '

            If Not Me.IsPostBack Then

                Process.LoadRadDropDownTextAndValue(cboJobGrade, "Job_Grade_get_all", "name", "name", False)
                If Request.QueryString("id") IsNot Nothing Then

                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Finance_Pension_Employer_Setup_Get", Request.QueryString("id"))

                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString                    
                    Process.AssignRadDropDownValue(cboJobGrade, strUser.Tables(0).Rows(0).Item("jobgrade").ToString)
                    txtContribution.Value = strUser.Tables(0).Rows(0).Item("contribution").ToString
                    txtecontribution.Value = strUser.Tables(0).Rows(0).Item("employercontribution").ToString
                Else
                    txtid.Text = "0"
                    txtContribution.Value = "0"
                    txtecontribution.Value = "0"
                End If

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Function GetIdentity(ByVal jobgrade As String, ByVal contribution As Double, ByVal econtribution As Double) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Finance_Pension_Employer_Setup_Update"
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@grade", SqlDbType.VarChar).Value = jobgrade
            cmd.Parameters.Add("@contribution", SqlDbType.Decimal).Value = contribution
            cmd.Parameters.Add("@econtribution", SqlDbType.Decimal).Value = econtribution
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
            If Request.QueryString("id") IsNot Nothing Then
                If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False And Process.AuthenAction(Session("role"), AuthenCode, "Update") = False Then
                    Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "danger")
                    Exit Sub
                End If
            Else
                If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False And Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                   Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "danger")
                    Exit Sub
                End If
            End If

            Dim lblstatus As String
            lblstatus = "Record saving, please wait ..."

            If (cboJobGrade.SelectedValue Is Nothing) Then
                lblstatus = "Job Grade required!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                cboJobGrade.Focus()
                Exit Sub
            End If

            If IsNumeric(txtecontribution.Value) = False Then
                lblstatus = "Contribution required!"               
                txtecontribution.Focus()
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                Exit Sub
            End If

            If IsNumeric(txtContribution.Value) = False Then
                lblstatus = "Contribution required!"
                txtContribution.Focus()
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                Exit Sub
            End If

            If (CDbl(txtContribution.Value) < 0 Or CDbl(txtContribution.Value) > 100) Then
                lblstatus = "Employee Contribution figure invalid!"
                txtContribution.Focus()
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                Exit Sub
            End If

            If (CDbl(txtecontribution.Value) < 0 Or CDbl(txtecontribution.Value) > 100) Then
                lblstatus = "Employer Contribution figure invalid!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                txtecontribution.Focus()
                Exit Sub
            End If

            'btnAdd.Enabled = False

            If txtid.Text <> "0" And txtid.Text.Trim <> "" Then
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Finance_Pension_Employer_Setup_Update", txtid.Text, cboJobGrade.SelectedValue, txtContribution.Value, txtecontribution.Value)
            Else
                txtid.Text = GetIdentity(cboJobGrade.SelectedValue, txtContribution.Value, txtecontribution.Value)
                If txtid.Text = "0" Then
                    lblstatus = Process.strExp
                    Process.loadalert(divalert, msgalert, lblstatus, "danger")
                    Exit Sub
                End If
            End If
            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Finance_Pension_Reset")
            lblstatus = "Record saved"
            Process.loadalert(divalert, msgalert, lblstatus, "success")

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        Finally
            'btnAdd.Enabled = True
        End Try
    End Sub


    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            'Response.Write("<script language='javascript'> { self.close() }</script>")
            Response.Redirect("PensionContributions")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
End Class