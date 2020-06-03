Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO


Public Class NotificationDetail
    Inherits System.Web.UI.Page
    Dim exits As New clsExit
    Dim olddata(3) As String
    Public Shared copies() As String
    Public Shared mailsto() As String
    Public Shared Separators() As Char = {";"c}
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                'Company_Structure_get_parent
                If Request.QueryString("id") IsNot Nothing Then
                    Dim strUser As New DataSet                   
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_MailBox_Received_Get", Request.QueryString("id"))
                    lblid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    lblfrom.Text = strUser.Tables(0).Rows(0).Item("SenderName").ToString
                    lblsubject.Text = strUser.Tables(0).Rows(0).Item("subject").ToString
                    lbldate.Text = strUser.Tables(0).Rows(0).Item("daterecevied2").ToString
                    txtbody.Text = strUser.Tables(0).Rows(0).Item("body").ToString
                    mailsto = strUser.Tables(0).Rows(0).Item("receiver").ToString.Split(Separators, StringSplitOptions.RemoveEmptyEntries)
                    Dim copied As String = ""
                    Dim mailto As String = ""
                    For i = 0 To mailsto.Count - 1
                        Dim copiedemp As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select Employee2  from dbo.Employees_All where empid = '" & mailsto(i).ToString.Trim & "'")
                        If i = 0 Then
                            mailto = copiedemp
                        Else
                            mailto = mailto & "; " & copiedemp
                        End If
                    Next
                    lblto.Text = mailto

                    copies = strUser.Tables(0).Rows(0).Item("copied").ToString.Split(Separators, StringSplitOptions.RemoveEmptyEntries)
                    For i = 0 To copies.Count - 1
                        Dim copiedemp As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select Employee2  from dbo.Employees_All where empid = '" & copies(i).ToString.Trim & "'")
                        If i = 0 Then
                            copied = copiedemp
                        Else
                            copied = copied & "; " & copiedemp
                        End If
                    Next
                    lblcc.Text = copied
                End If

            End If
        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Response.Write("<script language='javascript'> { self.close() }</script>")
        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub



    Protected Sub btnOO_Click(sender As Object, e As EventArgs) Handles btnOO.Click
        Try
            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_MailBox_Mark_As_Read", Request.QueryString("id"))
        Catch ex As Exception

        End Try
    End Sub
End Class