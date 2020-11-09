Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Public Class EmployeeAssetReturn
    Inherits System.Web.UI.Page
    Dim AuthenCode As String = "EMPLIST"
    Dim olddata(8) As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        RadComboBox2.Enabled = False
        Session("PreviousAssetPage") = Request.UrlReferrer.ToString
        If Request.QueryString("Id1") IsNot Nothing Then
            Dim strUser As New DataSet
            strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Asset_get", Request.QueryString("id1"))
            txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
            txtempid.Text = strUser.Tables(0).Rows(0).Item("EmpID").ToString
            aname.Value = strUser.Tables(0).Rows(0).Item("Name").ToString
            assetsname.Value = strUser.Tables(0).Rows(0).Item("AssetName").ToString
            assetsdescription.Value = strUser.Tables(0).Rows(0).Item("Descrption").ToString
            assetsnumber.Value = strUser.Tables(0).Rows(0).Item("AssetNumber").ToString
            locations.Value = strUser.Tables(0).Rows(0).Item("Location").ToString
            classifications.Value = strUser.Tables(0).Rows(0).Item("Classification").ToString
            physicalconditions.Value = strUser.Tables(0).Rows(0).Item("PhysicalCondition").ToString
            Process.AssignRadComboValue(RadComboBox2, strUser.Tables(0).Rows(0).Item("Status").ToString)
            comments.Value = strUser.Tables(0).Rows(0).Item("comments").ToString
        Else
            txtempid.Text = Session("EmpID")
            aname.Value = Process.GetEmployeeName(txtempid.Text)


        End If

    End Sub
    Private Function GetIdentity(ByVal empid As String, ByVal assetname1 As String, ByVal assetnumber1 As String,
                                 ByVal location1 As String, ByVal classification1 As String, ByVal Description As String,
                                  ByVal PhysicalCondtion1 As String, ByVal Status As String, ByVal Comments As String) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()


            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Emp_Asset_Update"
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = txtid.Text
            cmd.Parameters.Add("@EmpID", SqlDbType.VarChar).Value = empid
            cmd.Parameters.Add("@AssetName", SqlDbType.VarChar).Value = assetname1
            cmd.Parameters.Add("@AssetNumber", SqlDbType.VarChar).Value = assetnumber1
            cmd.Parameters.Add("@Location", SqlDbType.VarChar).Value = location1
            cmd.Parameters.Add("@Classification", SqlDbType.VarChar).Value = classification1
            cmd.Parameters.Add("@AssetDescription", SqlDbType.VarChar).Value = Description
            cmd.Parameters.Add("@PhysicalCondition", SqlDbType.VarChar).Value = PhysicalCondtion1
            cmd.Parameters.Add("@status", SqlDbType.VarChar).Value = Status
            cmd.Parameters.Add("@comments", SqlDbType.VarChar).Value = Comments

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
        Dim lblstatus As String = ""
        Dim initiator As String = ""
        Dim initiatorname As String = ""
        Dim office As String = ""

        Dim strEmp As New DataSet
        strEmp = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "select a.Name, a.Employee2 Employee,Email, Office     from dbo.Employees_All a where a.EmpID = '" & txtempid.Text & "'")
        If strEmp.Tables(0).Rows.Count > 0 Then
            initiator = strEmp.Tables(0).Rows(0).Item("Email").ToString
            initiatorname = strEmp.Tables(0).Rows(0).Item("Name").ToString
            office = strEmp.Tables(0).Rows(0).Item("Office").ToString
        End If
        Dim url As String = ""
        url = Process.ApplicationURL & "/" & "Module/Employee/EmployeeData"
        If Process.Asset_Return_Request(assetsname.Value, Process.GetMailList("hr"), initiatorname & " of " & office, assetsnumber.Value, initiator, txtempid.Text, "", url & "?id=" & txtempid.Text) = True Then
            lblstatus = "Changes has been forwarded to HR for acceptance"
            Process.loadalert(divalert, msgalert, lblstatus, "success")
        Else
            Process.loadalert(divalert, msgalert, Session("exception"), "danger")
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
            Response.Redirect("~/Module/Employee/EmployeeProfile.aspx", True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


End Class