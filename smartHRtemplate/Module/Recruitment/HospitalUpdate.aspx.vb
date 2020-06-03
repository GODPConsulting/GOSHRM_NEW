Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO


Public Class HospitalUpdate
    Inherits System.Web.UI.Page
    Dim education As New clsEducation
    Dim AuthenCode As String = "HOSPITAL"
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
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Hospital_Get", Request.QueryString("id"))
                    aname.Value = strUser.Tables(0).Rows(0).Item("Name").ToString
                    aaddress.Value = strUser.Tables(0).Rows(0).Item("Address").ToString
                    acontactperson.Value = strUser.Tables(0).Rows(0).Item("ContactPerson").ToString
                    acontactnumber.Value = strUser.Tables(0).Rows(0).Item("ContactNumber").ToString
                Else
                    txtid.Text = "0"
                    aname.Value = ""
                    aaddress.Value = ""
                    acontactnumber.Value = ""
                    acontactperson.Value = ""
                End If
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Function GetIdentity(ByVal hname As String, ByVal address As String, _
                                ByVal contactperson As String, ByVal contactnumber As String) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Recruit_Hospital_Update"
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = hname
            cmd.Parameters.Add("@address", SqlDbType.VarChar).Value = address
            cmd.Parameters.Add("@contactperson", SqlDbType.VarChar).Value = contactperson
            cmd.Parameters.Add("@contactnumber", SqlDbType.VarChar).Value = contactnumber


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

            If aname.Value.Trim = "" Then
                lblstatus = "Name required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                aname.Focus()
                Exit Sub
            End If

            If aaddress.Value.Trim = "" Then
                lblstatus = "Address required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                aaddress.Focus()
                Exit Sub
            End If



            If txtid.Text.Trim = "" Then
                txtid.Text = "0"
            End If

            If txtid.Text = "0" Then
                txtid.Text = GetIdentity(aname.Value.Trim, aaddress.Value.Trim, acontactperson.Value.Trim, acontactnumber.Value.Trim)
                If txtid.Text = "0" Then
                    Exit Sub
                End If
            Else
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Hospital_Update", txtid.Text, aname.Value.Trim, aaddress.Value.Trim, acontactperson.Value.Trim, acontactnumber.Value.Trim)
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

            Response.Redirect("hospitals", True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

End Class