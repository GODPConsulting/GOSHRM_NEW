Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports GOSHRM.GOSHRM.GOSHRM.BO
Public Class AddRole
    Inherits System.Web.UI.Page
    Dim dtTable As DataTable
    Dim cRole As New clsRole
    Dim cPrivilege As New clsPrivilege
    Dim olddata(3) As String
    Dim olddata1(4) As String
    Dim AuthenCode As String = "UserRole"
    Private Function LoadData() As DataTable
        Dim dt As New DataTable
        Dim strDataSet1 As DataSet
        strDataSet1 = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "roles_get", txtid.Text)
        If strDataSet1.Tables(0).Rows.Count > 0 Then
            rolename.Value = strDataSet1.Tables(0).Rows(0).Item("role").ToString()
            radRoleType.SelectedText = strDataSet1.Tables(0).Rows(0).Item("Role Type").ToString()
        End If


            If txtid.Text = "0" Then ' Request.QueryString("id") Is Nothing Then
                dt = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "roles_privilege_all_add_1", radRoleType.SelectedText, radMenus.SelectedValue).Tables(0)
            Else
            dt = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "roles_privilege_all_update_1", radRoleType.SelectedText, rolename.Value, radMenus.SelectedValue).Tables(0)
            End If
        pagetitle.InnerText = "User Role(" & dt.Rows.Count.ToString & ")"
        Return dt
    End Function
    Private Sub LoadDataGrid()
        Try
            gridrole.DataSource = LoadData()
            gridrole.AllowSorting = False
            gridrole.AllowPaging = False
            gridrole.DataBind()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                If Request.QueryString("id") Is Nothing Then
                    txtid.Text = "0"
                Else
                    txtid.Text = Request.QueryString("id")
                End If

                Process.LoadRadDropDownTextAndValue(radMenus, "Modules_Get_All", "Module", "ModuleCode", False)
                Process.AssignRadDropDownValue(radMenus, "EMP")

                radRoleType.Items.Clear()
                radRoleType.Items.Add("Admin")
                radRoleType.Items.Add("ESS")
                LoadDataGrid()

            End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub



    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            '
            If txtid.Text = "0" Then
                If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False Then
                    Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "warning")
                    Exit Sub
                End If
            End If
            
            Dim lblstatus As String = ""
            If (radRoleType.SelectedText.Trim = "") Then
                lblstatus = "Role Type required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                radRoleType.Focus()
                Exit Sub
            End If

            Dim strDataSet1 As DataSet
            If txtid.Text <> "0" Then
                strDataSet1 = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "roles_get", txtid.Text)
                olddata(0) = strDataSet1.Tables(0).Rows(0).Item("role").ToString()
                olddata(1) = strDataSet1.Tables(0).Rows(0).Item("Role Type").ToString()
                olddata(2) = strDataSet1.Tables(0).Rows(0).Item("Description").ToString()
            End If

            cRole.Role = rolename.Value.Trim
            cRole.RoleType = radRoleType.SelectedText
            cRole.Description = ""
            Dim ModuleCode As String = ""
            Dim ModuleCodetest As String = ""
            Dim read As Boolean = False
            Dim create As Boolean = False
            Dim update As Boolean = False
            Dim delete As Boolean = False
            Dim id As Integer = 0
            If txtid.Text.Trim = "" Then
                id = 0
            Else
                id = txtid.Text
            End If

            Dim strprivilege As New DataSet

            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "roles_get_add", id, cRole.Role, cRole.RoleType, "", Session("LoginID"))
            'Reset Privileges first
            ''SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "roles_privilege_reset_1", cRole.Role, radMenus.SelectedValue, read, create, update, delete)
            Dim pagecount As Integer = gridrole.PageCount
            'For h As Integer = 0 To pagecount - 1
            '    GridVwHeaderChckbox.SetPageIndex(h)
            For Each row As GridViewRow In gridrole.Rows
                ModuleCode = gridrole.Rows(row.RowIndex).Cells(1).Text
                Dim readchk As CheckBox = row.FindControl("reads")
                Dim createchk As CheckBox = row.FindControl("creates")
                Dim updatechk As CheckBox = row.FindControl("updates")
                Dim deletechk As CheckBox = row.FindControl("deletes")
                If readchk IsNot Nothing AndAlso readchk.Checked Then
                    read = True
                Else
                    read = False
                End If

                If createchk IsNot Nothing AndAlso createchk.Checked Then
                    create = True
                Else
                    create = False
                End If

                If updatechk IsNot Nothing AndAlso updatechk.Checked Then
                    update = True
                Else
                    update = False
                End If

                If deletechk IsNot Nothing AndAlso deletechk.Checked Then
                    delete = True
                Else
                    delete = False
                End If

                Dim oldexists As Boolean = False
                cPrivilege.Create = create.ToString
                cPrivilege.Delete = delete.ToString
                cPrivilege.Read = read.ToString
                cPrivilege.Update = update.ToString
                strprivilege = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "roles_privilege_get", cRole.Role, ModuleCode)
                If strprivilege.Tables(0).Rows.Count > 0 Then
                    olddata1(0) = strprivilege.Tables(0).Rows(0).Item("reads").ToString()
                    olddata1(1) = strprivilege.Tables(0).Rows(0).Item("creates").ToString()
                    olddata1(2) = strprivilege.Tables(0).Rows(0).Item("updates").ToString()
                    olddata1(3) = strprivilege.Tables(0).Rows(0).Item("deletes").ToString()
                    oldexists = True
                End If

                Dim k As Integer = 0
                Dim OldValue1 As String = ""
                Dim NewValue1 As String = ""
                For Each a In GetType(clsPrivilege).GetProperties()
                    If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If a.GetValue(cPrivilege, Nothing).ToString <> olddata1(k).ToString Then
                                NewValue1 += a.Name + ": " + a.GetValue(cPrivilege, Nothing).ToString & vbCrLf
                                OldValue1 += a.Name + ": " + olddata1(k).ToString & vbCrLf
                            End If
                        End If
                    End If
                    k = k + 1
                Next

                If OldValue1 <> NewValue1 Then
                    Process.GetAuditTrailInsertandUpdate(OldValue1, NewValue1, "Updated " + ModuleCode + " on " + cRole.Role, "Role Privilege")
                End If


                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "roles_privilege_update", cRole.Role, ModuleCode, read, create, update, delete)
            Next

            Dim OldValue As String = ""
            Dim NewValue As String = ""
            Dim j As Integer = 0


            If txtid.Text <> "0" Then 'Updates
                For Each a In GetType(clsRole).GetProperties()
                    If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If IsNumeric(a.GetValue(cRole, Nothing)) = True And IsNumeric(olddata(j)) = True Then
                                If CDbl(a.GetValue(cRole, Nothing)) <> CDbl(olddata(j)) Then
                                    NewValue += a.Name + ": " + a.GetValue(cRole, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            Else
                                If a.GetValue(cRole, Nothing).ToString <> olddata(j).ToString Then
                                    NewValue += a.Name + ": " + a.GetValue(cRole, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            End If
                        End If
                    End If
                    j = j + 1
                Next
            Else
                For Each a In GetType(clsRole).GetProperties() 'New Entries
                    If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If a.GetValue(cRole, Nothing) = Nothing Then
                                NewValue += a.Name + ":" + " " & vbCrLf
                            Else
                                NewValue += a.Name + ": " + a.GetValue(cRole, Nothing).ToString & vbCrLf
                            End If
                        End If
                    End If
                Next
            End If


            If NewValue.Trim = "" And OldValue.Trim = "" Then
            Else
                If txtid.Text <> "0" Then
                    Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Updated " + cRole.Role, "Role")
                Else
                    Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Inserted", "Role")
                End If
            End If

            If txtid.Text = "0" Then
                If Request.QueryString("id") IsNot Nothing Then
                    txtid.Text = Request.QueryString("id")
                Else
                    Dim strDataSet11 As DataSet
                    strDataSet11 = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "roles_get_name", cRole.Role)
                    txtid.Text = strDataSet11.Tables(0).Rows(0).Item("id").ToString()
                End If
            End If

            Process.loadalert(divalert, msgalert, radMenus.SelectedText & " Privileges updated for " & rolename.Value, "success")

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect("~/Module/Admin/Roles.aspx", True)            
        Catch ex As Exception

        End Try
    End Sub

  
    Protected Sub radRoleType_SelectedIndexChanged1(sender As Object, e As Telerik.Web.UI.DropDownListEventArgs) Handles radRoleType.SelectedIndexChanged
        LoadDataGrid()
    End Sub

  

    
    Protected Sub radMenus_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.DropDownListEventArgs) Handles radMenus.SelectedIndexChanged
        Try
            LoadDataGrid()
        Catch ex As Exception

        End Try
    End Sub


End Class