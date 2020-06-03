Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO


Public Class StructureDefinitionUpdate
    Inherits System.Web.UI.Page
    Dim structuredefn As New clsStructureDefn
    Dim AuthenCode As String = "COMPDEFN"
    Dim olddata(4) As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                Dim tooltips As String = "Hierarchy Number of Organisation Level. Highest Level begins from 1"
                structlevel.Attributes.Add("title", tooltips.ToLower)

                If Request.QueryString("id") IsNot Nothing Then
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "StructureDefinition_get", Request.QueryString("id"))
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    structname.Value = strUser.Tables(0).Rows(0).Item("definition").ToString
                    structdesc.Value = strUser.Tables(0).Rows(0).Item("description").ToString
                    structlevel.Value = strUser.Tables(0).Rows(0).Item("level").ToString
                Else
                    txtid.Text = "0"
                    Dim strLevel As New DataSet
                    strLevel = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "StructureDefinition_get_all")
                    If strLevel.Tables(0).Rows.Count = 0 Then
                        structlevel.Disabled = True
                        structlevel.Value = "1"
                    End If
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
            If (structname.Value.Trim = "") Then
                lblstatus = "Definition required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                structname.Focus()
                Exit Sub
            End If


            If structdesc.Value.Trim = "" Then
                lblstatus = "Description required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                structdesc.Focus()
                Exit Sub
            End If

            If IsNumeric(structlevel.Value) = False Then
                lblstatus = "Level required and must be number!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                structlevel.Focus()
                Exit Sub
            End If



            If txtid.Text.Trim = "" Then
                structuredefn.id = 0
            Else
                structuredefn.id = txtid.Text
            End If
            structuredefn.Definition = structname.Value.Trim
            structuredefn.Desc = structdesc.Value
            structuredefn.Level = structlevel.Value

            If txtid.Text <> "0" Then
                Dim strUser As New DataSet
                strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "StructureDefinition_get", txtid.Text)
                olddata(0) = strUser.Tables(0).Rows(0).Item("id").ToString
                olddata(1) = strUser.Tables(0).Rows(0).Item("definition").ToString
                olddata(2) = strUser.Tables(0).Rows(0).Item("description").ToString
                olddata(3) = strUser.Tables(0).Rows(0).Item("level").ToString
            End If


            'Check Level Existence
            'If txtLevel.Text.Trim = "1" Then
            Dim strLevel As New DataSet
            strLevel = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "StructureDefinition_Check_Level1", txtid.Text, structlevel.Value)
            If strLevel.Tables(0).Rows.Count > 0 Then
                lblstatus = "A Level " & structlevel.Value & " structure already exists, only one Level " & structlevel.Value & " structure can be created!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                Exit Sub
            End If
            'End If


            Dim OldValue As String = ""
            Dim NewValue As String = ""


            Dim j As Integer = 0
            If txtid.Text <> "0" Then 'Updates
                For Each a In GetType(clsStructureDefn).GetProperties()
                    If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If IsNumeric(a.GetValue(structuredefn, Nothing)) = True And IsNumeric(olddata(j)) = True Then
                                If CDbl(a.GetValue(structuredefn, Nothing)) <> CDbl(olddata(j)) Then
                                    NewValue += a.Name + ": " + a.GetValue(structuredefn, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            Else
                                If a.GetValue(structuredefn, Nothing).ToString <> olddata(j).ToString Then
                                    NewValue += a.Name + ": " + a.GetValue(structuredefn, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            End If
                        End If
                    End If
                    j = j + 1
                Next
            Else
                For Each a In GetType(clsStructureDefn).GetProperties() 'New Entries
                    If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If a.GetValue(structuredefn, Nothing) = Nothing Then
                                NewValue += a.Name + ":" + " " & vbCrLf
                            Else
                                NewValue += a.Name + ": " + a.GetValue(structuredefn, Nothing).ToString & vbCrLf
                            End If
                        End If
                    End If
                Next
            End If

            If txtid.Text = "0" Then
                txtid.Text = GetIdentity()
                If txtid.Text = "0" Then
                    Exit Sub
                End If
            Else
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "StructureDefinition_update", structuredefn.id, structuredefn.Definition, structuredefn.Desc, structuredefn.Level)
            End If



            If NewValue.Trim = "" And OldValue.Trim = "" Then
            Else
                If txtid.Text <> "0" Then
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Updated", "Structure Definition")
                Else
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Inserted", "Structure Definition")
                End If
            End If
            lblstatus = "Record saved!"
            Process.loadalert(divalert, msgalert, lblstatus, "warning")

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
            cmd.CommandText = "StructureDefinition_update"
            cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = 0
            cmd.Parameters.Add("@defn", SqlDbType.VarChar).Value = structname.Value.Trim
            cmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = structdesc.Value.Trim
            cmd.Parameters.Add("@Level", SqlDbType.Int).Value = structlevel.Value

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
            Response.Redirect("~/Module/Admin/Organisation/StructureDefinition.aspx", True)
        Catch ex As Exception

        End Try
    End Sub
End Class