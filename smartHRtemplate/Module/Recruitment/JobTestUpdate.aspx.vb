Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO


Public Class JobTestUpdate
    Inherits System.Web.UI.Page
    'Dim education As New clsEducation
    Dim AuthenCode As String = "JOBTEST"
    Dim olddata(4) As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then

                Dim ismulti As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select isnull(ismulticompany,'No')  from general_info")
                If ismulti.ToLower = "no" Then
                    Process.LoadRadComboTextAndValueP2(cbocompany, "Company_Structure_Get_ByLevel", "1", Session("Access"), "name", "name", False)
                Else
                    Process.LoadRadComboTextAndValueP2(cbocompany, "Company_Structure_Get_ByLevel", "2", Session("Access"), "name", "name", False)
                End If


                cboOnline.Items.Clear()
                cboOnline.Items.Add("No")
                cboOnline.Items.Add("Yes")

                Process.AssignRadComboValue(cboonline, "No")
                aactive.Checked = True
                apassmark.Value = "0"

                If Request.QueryString("id") IsNot Nothing Then
                    ViewState("PreviousPage") = Request.UrlReferrer
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Job_Test_Get", Request.QueryString("id"))

                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    atesttitle.Value = strUser.Tables(0).Rows(0).Item("TestTitle").ToString
                    atestdesc.Value = strUser.Tables(0).Rows(0).Item("TestDescription").ToString
                    apassmark.Value = strUser.Tables(0).Rows(0).Item("Passmark").ToString
                    atestduration.Value = strUser.Tables(0).Rows(0).Item("TestDuration").ToString
                    'cbojobpost.Text = strUser.Tables(0).Rows(0).Item("Title").ToString
                    'cbostageno.Text = strUser.Tables(0).Rows(0).Item("StageNo").ToString

                    Process.AssignRadComboValue(cboonline, strUser.Tables(0).Rows(0).Item("Online").ToString)

                    If strUser.Tables(0).Rows(0).Item("Active").ToString.ToLower = "yes" Then
                        aactive.Checked = True
                    Else
                        aactive.Checked = False
                    End If


                    Process.AssignRadComboValue(cbocompany, strUser.Tables(0).Rows(0).Item("company").ToString)
                    Process.LoadRadComboTextAndValueInitiateP1(cbojobpost, "Recruit_Job_Post_Get_All_Has_Test", cbocompany.SelectedValue, "--Select Job Post--", "Title", "Code")
                    Process.AssignRadComboValue(cbojobpost, strUser.Tables(0).Rows(0).Item("Title").ToString)
                    Process.LoadRadComboTextAndValueP1(cbostageno, "Recruit_Test_Stages_Filter_All_Stages", CInt(cbojobpost.SelectedValue), "stageno", "stageno", False)
                    Process.AssignRadComboValue(cbostageno, strUser.Tables(0).Rows(0).Item("StageNo").ToString)

                    cbojobpost.Enabled = False
                    cbocompany.Enabled = False
                    cbostageno.Enabled = False
                Else
                    txtid.Text = "0"
                    apassmark.Value = "0"

                    Process.AssignRadComboValue(cbocompany, Session("company"))
                    Process.LoadRadComboTextAndValueInitiateP1(cbojobpost, "Recruit_Job_Post_Get_All_Has_Test", cbocompany.SelectedValue, "--Select Job Post--", "Title", "Code")
                    'Process.LoadRadComboTextAndValueP1(cbostageno, "Recruit_Test_Stages_Filter_Stages", CInt(cbojobpost.SelectedValue), "stageno", "stageno", False)
                End If
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            Dim lblstatus As String = ""
            Process.loadalert(divalert, msgalert, lblstatus, "warning")
            If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "warning")
                Exit Sub
            End If
            Dim stageno As Integer = 0
            stageno = cbostageno.SelectedItem.Value

            'If stageno > 1 Then
            '    For h As Integer = 0 To stageno - 1
            '        For l As Integer = 0 To cbostageno.Items.Count - 1
            '            If cbostageno.Items(l).Value.Contains(h) Then
            '                lblstatus = "Create Test Stage " & h.ToString & " before creating Stage " & stageno
            '                Process.loadalert(divalert, msgalert, lblstatus, "warning")
            '                Exit Sub
            '                Exit For
            '            End If
            '        Next
            '    Next
            'End If

            If cboonline.SelectedItem.Text = "Yes" And cbostageno.SelectedItem.Value <> "1" Then
                Dim countMoreOnline As Integer = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select Count(*) from Recruit_Job_Test a where a.[Online] = 'Yes' and a.Active = 'Yes' and  a.jobid = " & CInt(cbojobpost.SelectedValue) & " and stageno < " & cbostageno.SelectedItem.Value)
                If countMoreOnline < 1 Then
                    lblstatus = "Online Tests cannot be after Paper Test in stages"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    Exit Sub
                End If
            End If

            'Confirm
            'If cboOnline.SelectedItem.Text = "Yes" Then
            '    Dim counts As Integer = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Recruit_Job_Test_Confirm_Activation", txtid.Text, CInt(radJobPost.SelectedValue), cboStageNo.SelectedItem.Text)
            '    If counts > 0 Then
            '        lblstatus.Text = "An active test with " & radJobPost.SelectedItem.Text & " already has this Stage No, only a maximum of 2 test stages are available"
            '        Exit Sub
            '    End If
            'End If

            If (atesttitle.Value.Trim = "") Then
                lblstatus = "Test Title required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                atesttitle.Focus()
                Exit Sub
            End If

            If IsNumeric(apassmark.Value) = False Then
                lblstatus = "Passmark required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                apassmark.Focus()
                Exit Sub
            End If

            If IsNumeric(atestduration.Value) = False Then
                lblstatus = "Test Duration required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                atestduration.Focus()
                Exit Sub
            End If

            If CDbl(apassmark.Value) > 100 Then
                lblstatus = "Passmark cannot be more than 100!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                apassmark.Focus()
                Exit Sub
            End If

            Dim OldValue As String = ""
            Dim NewValue As String = ""

            Dim j As Integer = 0
            If txtid.Text.Trim = "" Then
                txtid.Text = "0"
            End If

            Dim isactive As String = "No"
            If aactive.Checked = True Then
                isactive = "Yes"
            Else
                isactive = "No"
            End If

            If txtid.Text = "0" Then
                txtid.Text = GetIdentity()
                If txtid.Text = "0" Then
                    Exit Sub
                End If
            Else
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Job_Test_Update", txtid.Text, CInt(cbojobpost.SelectedValue), _
                                          atesttitle.Value.Trim, atestdesc.Value, CDbl(apassmark.Value), cboonline.SelectedItem.Text, atestduration.Value, _
                                          cbostageno.SelectedItem.Text, isactive, Session("LoginID"))
            End If

            

            lblstatus = "Record saved"
            'ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Record saved" + "')", True)

            cbojobpost.Enabled = False
            cbocompany.Enabled = False

            Process.loadalert(divalert, msgalert, lblstatus, "success")
            Response.Redirect("~/Module/Recruitment/JobTests", True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub
    Private Function GetIdentity() As String
        Try
            Dim isactive As String = "No"
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Recruit_Job_Test_Update"
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = txtid.Text
            cmd.Parameters.Add("@jobid", SqlDbType.Int).Value = cbojobpost.SelectedValue
            cmd.Parameters.Add("@TestTitle", SqlDbType.VarChar).Value = atesttitle.Value.Trim
            cmd.Parameters.Add("@TestDesc", SqlDbType.VarChar).Value = atestdesc.Value
            cmd.Parameters.Add("@PassMark", SqlDbType.VarChar).Value = apassmark.Value
            cmd.Parameters.Add("@IsOnline", SqlDbType.VarChar).Value = cboonline.SelectedItem.Text
            cmd.Parameters.Add("@Duration", SqlDbType.VarChar).Value = atestduration.Value
            cmd.Parameters.Add("@StageNo", SqlDbType.VarChar).Value = cbostageno.SelectedItem.Text
            If aactive.Checked = True Then
                isactive = "Yes"
            Else
                isactive = "No"
            End If
            cmd.Parameters.Add("@Active", SqlDbType.VarChar).Value = isactive
            cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = Session("LoginID")
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

            Response.Redirect("~/Module/Recruitment/JobTests", True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


    Protected Sub cboOnline_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboonline.SelectedIndexChanged
        Try
            If cboOnline.SelectedItem.Text = "Yes" Then
                divstage.Visible = True
            Else
                divstage.Visible = False
                Process.AssignRadComboValue(cboStageNo, "1")
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub cbojobpost_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cbojobpost.SelectedIndexChanged
        Try
            Process.LoadRadComboTextAndValueP1(cbostageno, "Recruit_Test_Stages_Filter_Stages", CInt(cbojobpost.SelectedValue), "stageno", "stageno", False)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub radOffice_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cbocompany.SelectedIndexChanged
        Process.LoadRadComboTextAndValueInitiateP1(cbojobpost, "Recruit_Job_Post_Get_All_Has_Test", cbocompany.SelectedValue, "--Select Job Post--", "Title", "Code")
    End Sub
End Class