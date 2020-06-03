Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO


Public Class CompaniesUpdate
    Inherits System.Web.UI.Page
    Dim education As New clsEducation
    Dim AuthenCode As String = "THIRDPARTY"
    Dim olddata(4) As String
    Dim options(4) As String
    Dim Separators() As Char = {","c}
    Dim answer As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                If Request.QueryString("id") IsNot Nothing Then
                    Dim strUser As New DataSet
                    txtid.Text = Request.QueryString("id")
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_ThirdParty_Company_Get", Request.QueryString("id"))

                    aname.Value = strUser.Tables(0).Rows(0).Item("companyname").ToString
                    aaddress.Value = strUser.Tables(0).Rows(0).Item("contactaddress").ToString
                    acontactperson.Value = strUser.Tables(0).Rows(0).Item("contactperson").ToString
                    aphonenumber.Value = strUser.Tables(0).Rows(0).Item("contactnumber").ToString
                    aemailaddr.Value = strUser.Tables(0).Rows(0).Item("contactemail").ToString
                    Process.AssignRadDropDownValue(drcompany, strUser.Tables(0).Rows(0).Item("industry").ToString)
                Else
                    txtid.Text = "0"
                End If
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Function GetIdentity(ByVal hname As String, ByVal address As String, _
                                ByVal contactperson As String, ByVal contactnumber As String, ByVal contactemail As String, ByVal industry As String) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Recruit_ThirdParty_Company_Update"
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@companyname", SqlDbType.VarChar).Value = hname
            cmd.Parameters.Add("@contactaddress", SqlDbType.VarChar).Value = address
            cmd.Parameters.Add("@contactperson", SqlDbType.VarChar).Value = contactperson
            cmd.Parameters.Add("@contactnumber", SqlDbType.VarChar).Value = contactnumber
            cmd.Parameters.Add("@contactemail", SqlDbType.VarChar).Value = contactemail
            cmd.Parameters.Add("@industry", SqlDbType.VarChar).Value = industry
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
                    Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "danger")                    
                    Exit Sub
                End If
            End If


            Dim lblstatus As String = ""

            If aname.Value.Trim = "" Then
                lblstatus = "Name required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                aname.Focus()
                Exit Sub
            End If

            If aaddress.Value.Trim = "" Then
                lblstatus = "Address required!"
                aaddress.Focus()
                Exit Sub
            End If

            If acontactperson.Value.Trim = "" Then
                lblstatus = "Contact Person required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                acontactperson.Focus()
                Exit Sub
            End If

            If aphonenumber.Value.Trim = "" Then
                lblstatus = "Contact Number required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                aphonenumber.Focus()
                Exit Sub
            End If

            If drcompany.SelectedItem.Value Is Nothing Then
                lblstatus = "Industry required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                drcompany.Focus()
                Exit Sub
            End If



            If txtid.Text.Trim = "" Then
                txtid.Text = "0"
            End If

            If txtid.Text = "0" Then
                txtid.Text = GetIdentity(aname.Value.Trim, aaddress.Value.Trim, acontactperson.Value.Trim, aphonenumber.Value.Trim, aemailaddr.Value, drcompany.SelectedValue)
                If txtid.Text = "0" Then
                    Exit Sub
                End If
            Else
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_ThirdParty_Company_Update", txtid.Text, aname.Value.Trim, aaddress.Value.Trim, acontactperson.Value.Trim, aphonenumber.Value.Trim, aemailaddr.Value, drcompany.SelectedValue, Session("LoginID"))
            End If

            lblstatus = "Record saved"
            Process.loadalert(divalert, msgalert, lblstatus, "success")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try

            Response.Redirect("~/Module/Recruitment/Companies", True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

End Class