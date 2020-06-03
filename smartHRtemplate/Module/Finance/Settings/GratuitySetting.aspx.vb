Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Microsoft.VisualBasic


Public Class GratuitySetting
    Inherits System.Web.UI.Page
    Dim TimeSheet As New clsTimeSheet
    Dim ApproveLeave As New clsApproveLeave
    Dim AuthenCode As String = "GRAUTITYSETUP"
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
    Dim lblstatus As String = ""



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                If Request.QueryString("id") IsNot Nothing Then
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Finance_Grautity_Range_Setup_Get", Request.QueryString("id"))
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString                  
                    txtContribution.Value = strUser.Tables(0).Rows(0).Item("rate").ToString
                    txtmin.Value = strUser.Tables(0).Rows(0).Item("minrange").ToString
                    txtmax.Value = strUser.Tables(0).Rows(0).Item("maxrange").ToString
                Else
                    txtid.Text = "0"
                End If
            End If
        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "danger")
        End Try
    End Sub
    Private Function GetIdentity() As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Finance_Grautity_Range_Setup_Update"
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = txtid.Text
            cmd.Parameters.Add("@minrange", SqlDbType.VarChar).Value = txtmin.Value
            cmd.Parameters.Add("@maxrange", SqlDbType.Decimal).Value = txtmax.Value
            cmd.Parameters.Add("@rate", SqlDbType.Decimal).Value = txtContribution.Value
            cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = Session("UserEmpID")
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
            If (txtid.Text <> "0" And txtid.Text.Trim <> "") Then
                If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False And Process.AuthenAction(Session("role"), AuthenCode, "Update") = False Then
                    lblstatus = "You don't have privilege to perform this action"
                    Process.loadalert(divalert, msgalert, lblstatus, "danger")
                    Exit Sub
                End If
            Else
                If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False And Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                    lblstatus = "You don't have privilege to perform this action"
                    Process.loadalert(divalert, msgalert, lblstatus, "danger")
                    Exit Sub
                End If
            End If

            lblstatus = "Record saving, please wait ..."
            Process.loadalert(divalert, msgalert, lblstatus, "success")


            If IsNumeric(txtmin.Value) = False Then
                lblstatus = "Min. Years of Service required!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                txtmin.Focus()
                Exit Sub
            End If

            If IsNumeric(txtmax.Value) = False Then
                lblstatus = "Max. Years of Service required!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                txtmax.Focus()
                Exit Sub
            End If

            If IsNumeric(txtContribution.Value) = False Then
                lblstatus = "Percentage required!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                txtContribution.Focus()
                Exit Sub
            End If


            txtid.Text = GetIdentity()
            If txtid.Text = "0" Then
                lblstatus = Process.strExp
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                Exit Sub
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
            Response.Redirect("gratuitysetup")
        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "danger")
        End Try
    End Sub
End Class