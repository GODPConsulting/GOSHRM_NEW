Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO


Public Class CompanyStructureUpdate
    Inherits System.Web.UI.Page
    Dim compstructure As New clsCompanyStructure
    Dim AuthenCode As String = "COMPSTRUCT"
    Dim olddata(7) As String
    Dim EmpID As String = ""
    Dim Level(2) As String
    Dim Separators() As Char = {":"c}
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                Process.LoadRadDropDownTextAndValue(radCountry, "CountryTable_get", "Country", "Country", False)
                Process.LoadRadDropDownTextAndValue(radStructure, "StructureDefinition_get_all", "Definition", "Definition", False)
                Process.LoadRadComboTextAndValueP1(radHead, "Emp_PersonalDetail_Get_Employees", "", "name", "EmpID", True)

                'Company_Structure_get_parent
                If Request.QueryString("id") IsNot Nothing Then
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Company_Structure_get", Request.QueryString("id"))
                    radStructure.SelectedText = strUser.Tables(0).Rows(0).Item("structuretype").ToString
                    Process.LoadRadComboTextAndValueP1(radParent, "Company_Structure_get_parent", radStructure.SelectedValue, "Name", "Name", True)
                    Process.AssignRadDropDownValue(radCountry, strUser.Tables(0).Rows(0).Item("country").ToString)
                    Process.AssignRadComboValue(radHead, strUser.Tables(0).Rows(0).Item("Head").ToString)
                    Process.AssignRadComboValue(radParent, strUser.Tables(0).Rows(0).Item("Parent").ToString)
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    txtoffice.Value = strUser.Tables(0).Rows(0).Item("name").ToString
                    txtaddress.Value = strUser.Tables(0).Rows(0).Item("address").ToString
                Else
                    txtid.Text = "0"
                End If

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try

            If txtid.Text <> "0" Then
                If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False Then
                    Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "warning")
                    Exit Sub
                End If
            End If

            Dim status As String = ""
            If (txtoffice.Value.Trim = "") Then
                status = "Office name required!"
                Process.loadalert(divalert, msgalert, status, "warning")
                txtoffice.Focus()
                Exit Sub
            End If

            If (radStructure.SelectedText.Trim = "" Or radStructure.SelectedText.Trim = "-- Select --") Then
                status = "Structure Type required!"
                Process.loadalert(divalert, msgalert, status, "warning")
                radStructure.Focus()
                Exit Sub
            End If

            If (radCountry.SelectedText.Trim = "" Or radCountry.SelectedText.Trim = "-- Select --") Then
                status = "Country required!"
                Process.loadalert(divalert, msgalert, status, "warning")
                radCountry.Focus()
                Exit Sub
            End If

            'Old Data
            If txtid.Text <> "0" Then
                Dim strUser As New DataSet
                strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Company_Structure_get", Request.QueryString("id"))
                olddata(0) = strUser.Tables(0).Rows(0).Item("id").ToString
                olddata(1) = strUser.Tables(0).Rows(0).Item("name").ToString
                olddata(2) = strUser.Tables(0).Rows(0).Item("structuretype").ToString
                olddata(3) = strUser.Tables(0).Rows(0).Item("head").ToString
                olddata(4) = strUser.Tables(0).Rows(0).Item("country").ToString
                olddata(5) = strUser.Tables(0).Rows(0).Item("parent").ToString
                olddata(6) = strUser.Tables(0).Rows(0).Item("address").ToString
            End If

            If txtid.Text.Trim = "" Then
                compstructure.id = 0
            Else
                compstructure.id = txtid.Text
            End If
            compstructure.Name = txtoffice.Value.Trim
            compstructure.Country = radCountry.SelectedItem.Value
            compstructure.Parent = radParent.SelectedItem.Value
            compstructure.Type = radStructure.SelectedItem.Value
            compstructure.Address = txtaddress.Value
            compstructure.Head = radHead.SelectedItem.Value

            Dim OldValue As String = ""
            Dim NewValue As String = ""

            Dim j As Integer = 0



            If txtid.Text <> "0" Then 'Updates
                For Each a In GetType(clsCompanyStructure).GetProperties()
                    If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If IsNumeric(a.GetValue(compstructure, Nothing)) = True And IsNumeric(olddata(j)) = True Then
                                If CDbl(a.GetValue(compstructure, Nothing)) <> CDbl(olddata(j)) Then
                                    NewValue += a.Name + ": " + a.GetValue(compstructure, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            Else
                                If a.GetValue(compstructure, Nothing).ToString <> olddata(j).ToString Then
                                    NewValue += a.Name + ": " + a.GetValue(compstructure, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            End If
                        End If
                    End If
                    j = j + 1
                Next
            Else
                For Each a In GetType(clsCompanyStructure).GetProperties() 'New Entries
                    If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If a.GetValue(compstructure, Nothing) = Nothing Then
                                NewValue += a.Name + ":" + " " & vbCrLf
                            Else
                                NewValue += a.Name + ": " + a.GetValue(compstructure, Nothing).ToString & vbCrLf
                            End If
                        End If
                    End If
                Next
            End If

            If txtid.Text = "0" Then
                txtid.Text = GetIdentity(compstructure.id, compstructure.Name, compstructure.Type, compstructure.Head, compstructure.Country, compstructure.Parent, compstructure.Address)
                If txtid.Text = "0" Then
                    Exit Sub
                End If
            Else
                GetIdentity(compstructure.id, compstructure.Name, compstructure.Type, compstructure.Head, compstructure.Country, compstructure.Parent, compstructure.Address)
            End If
            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Company_Structure_Update_Company")

            If NewValue.Trim = "" And OldValue.Trim = "" Then
            Else
                If txtid.Text <> "0" Then
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Updated", "Company Structure")
                Else
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Inserted", "Company Structure")
                End If

            End If
            Process.loadalert(divalert, msgalert, "Record saved", "success")
           
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Function GetIdentity(sid As Integer, sName As String, sType As String, sHead As String, Country As String, sParent As String, sAddress As String) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Company_Structure_update"
            cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = sid
            cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = sName
            cmd.Parameters.Add("@type", SqlDbType.VarChar).Value = sType
            cmd.Parameters.Add("@Head", SqlDbType.VarChar).Value = sHead
            cmd.Parameters.Add("@country", SqlDbType.VarChar).Value = Country
            cmd.Parameters.Add("@Parent", SqlDbType.VarChar).Value = sParent
            cmd.Parameters.Add("@Address", SqlDbType.VarChar).Value = sAddress
            cmd.CommandTimeout = 157200
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
            Return 0
        End Try
    End Function
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            'Response.Write("<script language='javascript'> { self.close() }</script>")
            Response.Redirect("~/Module/Admin/Organisation/CompanyStructure.aspx", True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub radStructure_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.DropDownListEventArgs) Handles radStructure.SelectedIndexChanged
        Try
            Process.LoadRadComboTextAndValueP1(radParent, "Company_Structure_get_parent", radStructure.SelectedValue, "Name", "Name", True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
End Class