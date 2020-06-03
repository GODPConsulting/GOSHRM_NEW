Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Public Class LocationUpdate
    Inherits System.Web.UI.Page
    Dim location As New clsLocation
    Dim olddata(10) As String
    Dim AuthenCode As String = "LOC"

    

    Private Sub LoadDynamic()
        Try
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                Process.LoadRadDropDownTextAndValue(radCountry, "CountryTable_get_all", "country", "country", False)

                If Request.QueryString("id") IsNot Nothing Then
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "location_get", Request.QueryString("id"))
                    locationname.Value = strUser.Tables(0).Rows(0).Item("name").ToString
                    Process.AssignRadDropDownValue(radCountry, strUser.Tables(0).Rows(0).Item("country").ToString)
                    province.Value = strUser.Tables(0).Rows(0).Item("state").ToString
                    city.Value = strUser.Tables(0).Rows(0).Item("city").ToString
                    txtaddress.Value = strUser.Tables(0).Rows(0).Item("address").ToString
                    telephone.Value = strUser.Tables(0).Rows(0).Item("phone").ToString
                    note.Value = strUser.Tables(0).Rows(0).Item("note").ToString
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
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
            
            Dim lblstatus As String = ""
            If locationname.Value.Trim = "" Then
                lblstatus = "Name is required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                locationname.Focus()
                Exit Sub
            End If

            If (radCountry.SelectedText.Trim = "" Or radCountry.SelectedText.Trim = "-- Select --") Then
                lblstatus = "Country required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                radCountry.Focus()
                Exit Sub
            End If



            If city.Value.Trim = "" Then
                lblstatus = "City is required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                city.Focus()
                Exit Sub
            End If

            If province.Value.Trim = "" Then
                lblstatus = "State is required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                province.Focus()
                Exit Sub
            End If

            If locationname.Value.Trim.ToLower = province.Value.Trim.ToLower Then
                lblstatus = "Location name must differ from State!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                province.Focus()
                Exit Sub
            End If

            If Request.QueryString("id") IsNot Nothing Then
                Dim strUser As New DataSet
                strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "location_get", Request.QueryString("id"))
                olddata(0) = strUser.Tables(0).Rows(0).Item("id").ToString
                olddata(1) = strUser.Tables(0).Rows(0).Item("name").ToString
                olddata(2) = strUser.Tables(0).Rows(0).Item("country").ToString
                olddata(3) = strUser.Tables(0).Rows(0).Item("state").ToString
                olddata(4) = strUser.Tables(0).Rows(0).Item("city").ToString
                olddata(5) = strUser.Tables(0).Rows(0).Item("address").ToString
                olddata(6) = strUser.Tables(0).Rows(0).Item("zipcode").ToString
                olddata(7) = strUser.Tables(0).Rows(0).Item("phone").ToString
                olddata(8) = strUser.Tables(0).Rows(0).Item("fax").ToString
                olddata(9) = strUser.Tables(0).Rows(0).Item("note").ToString
            End If


            If txtid.Text.Trim = "" Then
                location.id = 0
            Else
                location.id = txtid.Text
            End If
            location.Name = locationname.Value.Trim
            location.Country = radCountry.SelectedValue
            location.City = city.Value.Trim
            location.State = province.Value.Trim
            location.Address = txtaddress.Value
            location.ZipCode = ""
            location.Phone = telephone.Value
            location.Fax = ""
            location.Note = note.Value

            Dim OldValue As String = ""
            Dim NewValue As String = ""

            Dim j As Integer = 0

            If Request.QueryString("id") IsNot Nothing Then 'Updates
                For Each a In GetType(clsLocation).GetProperties()
                    If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If IsNumeric(a.GetValue(location, Nothing)) = True And IsNumeric(olddata(j)) = True Then
                                If CDbl(a.GetValue(location, Nothing)) <> CDbl(olddata(j)) Then
                                    NewValue += a.Name + ": " + a.GetValue(location, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            Else
                                If a.GetValue(location, Nothing).ToString <> olddata(j).ToString Then
                                    NewValue += a.Name + ": " + a.GetValue(location, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            End If
                        End If
                    End If
                    j = j + 1
                Next
            Else
                For Each a In GetType(clsLocation).GetProperties() 'New Entries
                    If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If a.GetValue(location, Nothing) = Nothing Then
                                NewValue += a.Name + ":" + " " & vbCrLf
                            Else
                                NewValue += a.Name + ": " + a.GetValue(location, Nothing).ToString & vbCrLf
                            End If
                        End If
                    End If
                Next
            End If
            If txtid.Text = "0" Then
                txtid.Text = GetIdentity(location.Name, location.Country, location.State, location.City, location.Address, location.ZipCode, location.Phone)
                If txtid.Text = "0" Then
                    Exit Sub

                End If
            Else
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "location_update", location.id, location.Name, location.Country, location.State, location.City, location.Address, location.ZipCode, location.Phone, location.Fax, location.Note)
            End If

            Process.loadalert(divalert, msgalert, "Record saved", "success")

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Function GetIdentity(ByVal sname As String, ByVal country As String, ByVal state As String, ByVal city As String, ByVal address As String, ByVal phone As String, ByVal note As String) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "location_update"
            cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = 0
            cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = sname
            cmd.Parameters.Add("@country", SqlDbType.VarChar).Value = country
            cmd.Parameters.Add("@state", SqlDbType.VarChar).Value = state
            cmd.Parameters.Add("@city", SqlDbType.VarChar).Value = city
            cmd.Parameters.Add("@address", SqlDbType.VarChar).Value = address
            cmd.Parameters.Add("@zipcode", SqlDbType.VarChar).Value = ""
            cmd.Parameters.Add("@phone", SqlDbType.VarChar).Value = phone
            cmd.Parameters.Add("@fax", SqlDbType.VarChar).Value = ""
            cmd.Parameters.Add("@note", SqlDbType.VarChar).Value = note

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
            Response.Redirect("~/Module/Admin/Organisation/Location.aspx", True)
        Catch ex As Exception

        End Try
    End Sub
End Class