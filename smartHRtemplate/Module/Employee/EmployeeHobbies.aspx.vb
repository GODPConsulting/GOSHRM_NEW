Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO



Public Class EmployeeHobbies
    Inherits System.Web.UI.Page

    Dim AuthenCode As String = "EMPLIST"


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("PreviousHobbiesPage") = Request.UrlReferrer.ToString
        If Request.QueryString("Id1") IsNot Nothing Then
            Dim strUser As New DataSet
            strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Hobbies_get", Request.QueryString("id1"))
            txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
            txtempid.Text = strUser.Tables(0).Rows(0).Item("EmpID").ToString
            aname.Value = strUser.Tables(0).Rows(0).Item("Name").ToString
            hobbiesname.Value = strUser.Tables(0).Rows(0).Item("HobbyName").ToString
            hobbyDescriptions.Value = strUser.Tables(0).Rows(0).Item("HobbyDescription").ToString
            TextBox1.Text = strUser.Tables(0).Rows(0).Item("Stars").ToString
        Else
            txtempid.Text = Session("EmpID")
            aname.Value = Process.GetEmployeeName(txtempid.Text)


        End If
    End Sub
    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Dim lblstatus As String = ""
        If txtid.Text <> "0" Then
            If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False Then
                Process.loadalert(divalert, msgalert, Process.privilegemsg, "danger")
                Exit Sub
            End If
        End If
        If (hobbiesname.Value Is Nothing) Then
            lblstatus = "Hobby name is required!"
            hobbiesname.Focus()
            Exit Sub
        End If
        If hobbyDescriptions.Value = "" Then
            lblstatus = "Hobby Description  is required!"
            hobbiesname.Focus()
            Exit Sub
        End If


    End Sub
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            'If Session("PreviousCareerPage") IsNot Nothing Then
            '    If Session("PreviousCareerPage").ToString.ToLower.Contains("employeedata") = True Then
            '        Response.Redirect("~/Module/Employee/EmployeeData.aspx?Id=" & txtempid.Text, True)
            '    Else
            '        'Response.Redirect(Session("PreviousCareerPage"), True)
            '        Response.Write("<script language='javascript'> { self.close() }</script>")
            '    End If
            'End If

            'Response.Redirect("~/empdashboard", True)
            Response.Redirect("~/Module/Employee/EmployeeData.aspx?Id=" & txtempid.Text, True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

End Class