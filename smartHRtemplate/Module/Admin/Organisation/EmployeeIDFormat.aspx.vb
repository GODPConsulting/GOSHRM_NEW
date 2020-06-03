Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Telerik.Web.UI

Public Class EmployeeIDFormat
    Inherits System.Web.UI.Page
    Dim mailconfig As New clsMailConfig
    Dim AuthenCode As String = "EMPIDFORMAT"
    Dim olddata(14) As String
    Dim oFileAttached1 As Byte
    Dim oFileAttached As Byte()
    

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                Dim ismulti As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select isnull(ismulticompany,'No')  from general_info")
                If Request.QueryString("id") IsNot Nothing Then
                    If ismulti.ToLower = "no" Then
                        Process.LoadRadComboTextAndValueP2(cboCompany, "Company_Structure_Get_ByLevel", "1", Session("Access"), "name", "name", False)
                        divcompany.Visible = False
                    Else
                        Process.LoadRadComboTextAndValueP2(cboCompany, "Company_Structure_Get_ByLevel", "2", Session("Access"), "name", "name", False)
                    End If
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_EmployeeID_Format_Get", Request.QueryString("id"))
                    If strUser.Tables(0).Rows.Count > 0 Then
                        txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                        Process.AssignRadComboValue(cboCompany, strUser.Tables(0).Rows(0).Item("company").ToString)
                        cboCompany.Enabled = False
                        txtPrefix.Text = strUser.Tables(0).Rows(0).Item("Prefix").ToString
                        txtDigits.Text = strUser.Tables(0).Rows(0).Item("Digits").ToString
                        If IsDBNull(strUser.Tables(0).Rows(0).Item("updatedby")) = False Then
                            updatedby.Value = strUser.Tables(0).Rows(0).Item("updatedby").ToString
                        End If
                        If IsDBNull(strUser.Tables(0).Rows(0).Item("updatedon")) = False Then
                            updatedon.Value = strUser.Tables(0).Rows(0).Item("updatedon").ToString
                        End If
                        formatid.Value = txtPrefix.Text & "" & "2".PadLeft(CInt(txtDigits.Text), "0")
                    End If
                Else
                    txtid.Text = "0"
                    If ismulti.ToLower = "no" Then
                        divcompany.Visible = False
                        Process.LoadRadComboTextAndValueP2(cboCompany, "Recruit_EmployeeID_Format_ByLevel", "1", Session("Access"), "name", "name", False)
                    Else
                        Process.LoadRadComboTextAndValueP2(cboCompany, "Recruit_EmployeeID_Format_ByLevel", "2", Session("Access"), "name", "name", False)
                    End If
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
            cmd.CommandText = "Recruit_EmployeeID_Format_Update"
            cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = txtid.Text
            cmd.Parameters.Add("@company", SqlDbType.VarChar).Value = cboCompany.SelectedValue
            cmd.Parameters.Add("@prefix", SqlDbType.VarChar).Value = txtPrefix.Text.Trim
            cmd.Parameters.Add("@digits", SqlDbType.VarChar).Value = txtDigits.Text
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
            If txtid.Text <> "0" Then
                If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False Then
                    Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "warning")
                    Exit Sub
                End If
            End If
           

            If txtDigits.Text.Trim <> "" Then
                If IsNumeric(txtDigits.Text) = False Then
                    Process.loadalert(divalert, msgalert, "Number of Digits is required in numeric!", "warning")
                    txtDigits.Focus()
                    Exit Sub
                End If
            End If

            If txtid.Text = "0" Then
                txtid.Text = GetIdentity()
                If txtid.Text = "0" Then
                    Exit Sub
                End If
            Else
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_EmployeeID_Format_Update", cboCompany.SelectedValue, txtPrefix.Text.Trim, txtDigits.Text, Session("LoginID"))
            End If



            Process.loadalert(divalert, msgalert, "Record saved", "success")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub



    Protected Sub btnBack_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect("~/Module/Admin/Organisation/EmployeeID.aspx", True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub txtPrefix_TextChanged(sender As Object, e As EventArgs) Handles txtPrefix.TextChanged
        Try

            formatid.Value = txtPrefix.Text & "" & "2".PadLeft(CInt(txtDigits.Text), "0")
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub txtDigits_TextChanged(sender As Object, e As EventArgs) Handles txtDigits.TextChanged
        Try
            If txtDigits.Text.Trim = "" Or IsNumeric(txtDigits.Text) = False Then
                txtDigits.Text = "1"
            End If
  
            formatid.Value = txtPrefix.Text & "" & "2".PadLeft(CInt(txtDigits.Text), "0")
        Catch ex As Exception

        End Try
    End Sub
End Class