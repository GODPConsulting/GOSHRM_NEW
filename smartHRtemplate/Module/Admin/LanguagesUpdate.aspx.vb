﻿Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO


Public Class LanguagesUpdate
    Inherits System.Web.UI.Page
    Dim lang As New clsLanguage
    Dim AuthenCode As String = "LANG"
    Dim olddata(3) As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                'Company_Structure_get_parent
                If Request.QueryString("id") IsNot Nothing Then
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Languages_get", Request.QueryString("id"))
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    aname.Value = strUser.Tables(0).Rows(0).Item("name").ToString
                    adesc.Value = strUser.Tables(0).Rows(0).Item("Description").ToString
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
            If (aname.Value.Trim = "") Then
                lblstatus = "Name required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                aname.Focus()
                Exit Sub
            End If


            'Old Data
            If txtid.Text <> "0" Then
                Dim strUser As New DataSet
                strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Languages_get", Request.QueryString("id"))
                olddata(0) = strUser.Tables(0).Rows(0).Item("id").ToString
                olddata(1) = strUser.Tables(0).Rows(0).Item("name").ToString
                olddata(2) = strUser.Tables(0).Rows(0).Item("Description").ToString

            End If

            If txtid.Text.Trim = "" Then
                lang.id = 0
            Else
                lang.id = txtid.Text
            End If
            lang.Name = aname.Value.Trim
            lang.Description = adesc.Value.Trim

            Dim OldValue As String = ""
            Dim NewValue As String = ""

            Dim j As Integer = 0

            If txtid.Text <> "0" Then 'Updates
                For Each a In GetType(clsLanguage).GetProperties()
                    If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If IsNumeric(a.GetValue(lang, Nothing)) = True And IsNumeric(olddata(j)) = True Then
                                If CDbl(a.GetValue(lang, Nothing)) <> CDbl(olddata(j)) Then
                                    NewValue += a.Name + ": " + a.GetValue(lang, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            Else
                                If a.GetValue(lang, Nothing).ToString <> olddata(j).ToString Then
                                    NewValue += a.Name + ": " + a.GetValue(lang, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            End If
                        End If
                    End If
                    j = j + 1
                Next
            Else
                For Each a In GetType(clsLanguage).GetProperties() 'New Entries
                    If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If a.GetValue(lang, Nothing) = Nothing Then
                                NewValue += a.Name + ":" + " " & vbCrLf
                            Else
                                NewValue += a.Name + ": " + a.GetValue(lang, Nothing).ToString & vbCrLf
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
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Languages_update", lang.id, lang.Name, lang.Description)
            End If



            If NewValue.Trim = "" And OldValue.Trim = "" Then
            Else
                If OldValue.Trim <> "" Then
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Updated", "Languages")
                Else
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Inserted", "Languages")
                End If

            End If
            Process.loadalert(divalert, msgalert, "Record saved", "success")
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
            cmd.CommandText = "Languages_update"
            cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = txtid.Text
            cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = aname.Value
            cmd.Parameters.Add("@desc", SqlDbType.VarChar).Value = adesc.Value
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
            Response.Redirect("~/Module/Admin/languages", True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


End Class