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
        If Not Me.IsPostBack Then
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
                hobbiesrate.Value = CDbl(strUser.Tables(0).Rows(0).Item("Stars"))
                Select Case hobbiesrate.Value
                    Case 0
                        lbrating.InnerText = "Very Poor"
                    Case 1
                        lbrating.InnerText = "Poor"
                    Case 2
                        lbrating.InnerText = "Average"
                    Case 3
                        lbrating.InnerText = "Good"
                    Case 4
                        lbrating.InnerText = "Very Good"
                    Case 5
                        lbrating.InnerText = "Outstanding"
                End Select
            Else
                txtempid.Text = Session("EmpID")
                aname.Value = Process.GetEmployeeName(txtempid.Text)


            End If
        End If
    End Sub
    Private Function GetIdentity(ByVal empid As String, ByVal hobbyname As String, ByVal hobbyDesciption As String,
                                 ByVal hobbystar As String) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()


            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Emp_Hobbies_Update"
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@EmpID", SqlDbType.VarChar).Value = empid
            cmd.Parameters.Add("@hobbyname", SqlDbType.VarChar).Value = hobbyname
            cmd.Parameters.Add("@hobbydescription", SqlDbType.VarChar).Value = hobbyDesciption
            cmd.Parameters.Add("@star", SqlDbType.Int).Value = hobbystar


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
            Dim lblstatus As String = ""

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
            If txtid.Text = "" Then
                txtid.Text = GetIdentity(txtempid.Text, hobbiesname.Value, hobbyDescriptions.Value, hobbiesrate.Value)
            Else
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Hobbies_Update", txtid.Text, txtempid.Text, hobbiesname.Value, hobbyDescriptions.Value, hobbiesrate.Value)
            End If
            lblstatus = "Employee Asset has been Updated"
            Process.loadalert(divalert, msgalert, lblstatus, "success")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
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
            If Session("PreviousHobbiesPage") IsNot Nothing Then
                Response.Redirect(Session("PreviousHobbiesPage"), True)
                Exit Sub
            End If
            Response.Redirect("~/Module/Employee/EmployeeData.aspx?Id=" & txtempid.Text, True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

End Class