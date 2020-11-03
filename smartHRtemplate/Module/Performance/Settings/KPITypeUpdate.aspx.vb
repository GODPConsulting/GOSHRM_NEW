Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO


Public Class KPITypeUpdate
    Inherits System.Web.UI.Page
    Dim clsappcycle As New clsAppraisalCycle
    Dim AuthenCode As String = "COMPETTYPE"
    Dim olddata(8) As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then

                cbobyHR.Items.Clear()
                cbobyHR.Items.Add("No")
                cbobyHR.Items.Add("Yes")

                cboEmpSetObj.Items.Clear()
                cboEmpSetObj.Items.Add("No")
                cboEmpSetObj.Items.Add("Yes")

                txtid.Text = "0"
                If Request.QueryString("id") IsNot Nothing Then
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Competency_Group_Get", Request.QueryString("id"))
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    Process.AssignRadComboValue(cboEmpSetObj, strUser.Tables(0).Rows(0).Item("EmpSetObjective").ToString)
                    aname.Value = strUser.Tables(0).Rows(0).Item("CompetencyType").ToString
                    adesc.Value = strUser.Tables(0).Rows(0).Item("Description").ToString

                    If cboEmpSetObj.SelectedItem.Text.ToLower = "no" Then
                        cboWeightModel.Items.Clear()
                        cboWeightModel.Items.Add("even distribution")
                        cboWeightModel.Items.Add("360 degree feedback")
                    Else
                        cboWeightModel.Items.Clear()
                        cboWeightModel.Items.Add("even distribution")
                        cboWeightModel.Items.Add("independent allocation")
                    End If
                    Process.AssignRadComboValue(cboWeightModel, strUser.Tables(0).Rows(0).Item("weightmodel").ToString)
                    If cboWeightModel.SelectedItem.Text.ToLower.Contains("360 degree") = True Then
                        div360review.Visible = True
                    Else
                        div360review.Visible = False
                        cbobyHR.Items.Add("n/a")
                    End If
                    Process.AssignRadComboValue(cbobyHR, strUser.Tables(0).Rows(0).Item("ReviewerSelectionByHR").ToString)


                Else
                    Process.AssignRadComboValue(cboEmpSetObj, "Yes")
                    cboWeightModel.Items.Clear()
                    cboWeightModel.Items.Add("even distribution")
                    cboWeightModel.Items.Add("independent allocation")
                End If

                If cboWeightModel.SelectedItem.Text.ToLower.Contains("360 degree") = True Then
                    div360review.Visible= True 
                Else
                    div360review.Visible = False
                End If
                If aname.Value = "Others" Then
                    aname.Disabled = True
                    adesc.Disabled = True
                    cboWeightModel.Visible = False
                    cboEmpSetObj.Visible = False
                End If
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.message, "danger")
        End Try
    End Sub
    Private Function GetIdentity(ByVal kpitype As String, kpidesc As String, byhr As String) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Competency_Group_Update"
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@Type", SqlDbType.VarChar).Value = kpitype
            cmd.Parameters.Add("@Desc", SqlDbType.VarChar).Value = kpidesc
            cmd.Parameters.Add("@empsetobj", SqlDbType.VarChar).Value = cboEmpSetObj.SelectedItem.Text
            cmd.Parameters.Add("@weightmodel", SqlDbType.VarChar).Value = cboWeightModel.SelectedItem.Text
            cmd.Parameters.Add("@hr", SqlDbType.VarChar).Value = byhr
            cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = Session("LoginID")
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.message, "danger")
            Return 0
        End Try
    End Function

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            Dim lblstatus As String = ""
            If txtid.Text <> "0" Then
                If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False Then
                    Process.loadalert(divalert, msgalert, Process.privilegemsg, "warning")
                    Exit Sub
                End If
            End If

            Dim byhr As String = "n/a"
            If cboWeightModel.SelectedItem.Text.ToLower.Contains("360 degree") = True Then
                byhr = cbobyHR.SelectedItem.Text
            End If


            If (aname.Value.Trim = "") Then
                lblstatus = "KPI Type name required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                aname.Focus()
                Exit Sub
            End If

            If txtid.Text = "0" Then
                txtid.Text = GetIdentity(aname.Value.Trim, adesc.Value.Trim, byhr)
                If txtid.Text = "0" Then
                    Exit Sub
                End If
            Else
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Competency_Group_update", txtid.Text, aname.Value.Trim, adesc.Value.Trim, cboEmpSetObj.SelectedItem.Text, cboWeightModel.SelectedItem.Text, byhr, Session("LoginID"))
            End If

            lblstatus = "Record saved"
            Process.loadalert(divalert, msgalert, lblstatus, "success")
            Response.Redirect("kpitype", True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect("kpitype", True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub cboWeightModel_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboWeightModel.SelectedIndexChanged
        Try
            If cboWeightModel.SelectedItem.Text.ToLower.Contains("360 degree") = True Then
                div360review.Visible = True
                If cbobyHR.Items.Count > 2 Then
                    cbobyHR.Items.Remove("n/a")
                End If
            Else
                div360review.Visible = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub cboEmpSetObj_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboEmpSetObj.SelectedIndexChanged
        Try
            If cboEmpSetObj.SelectedItem.Text.ToLower = "no" Then
                    cboWeightModel.Items.Clear()
                    cboWeightModel.Items.Add("even distribution")
                cboWeightModel.Items.Add("360 degree feedback")

                If cbobyHR.Items.Count > 2 Then
                    cbobyHR.Items.Remove("n/a")
                End If

            Else
                cboWeightModel.Items.Clear()
                cboWeightModel.Items.Add("even distribution")
                cboWeightModel.Items.Add("independent allocation")
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class