Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Telerik.Web.UI


Public Class MyDevObjectives
    Inherits System.Web.UI.Page
    Dim comp As New clsCompetence
    Dim AuthenCode As String = "DEVPLAN"
    Dim AuthenCode2 As String = "APPDEV"
    Dim olddata(3) As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                Session("PreviousPage") = Request.UrlReferrer.ToString
                Process.LoadRadComboTextAndValueP1(cboResponsibity, "Emp_PersonalDetail_Get_Employees", Session("Access"), "name", "EmpID", False)
                Process.LoadRadComboTextAndValue(cbotraining, "Courses_get_all", "Course Title", "Code", True)
                Process.LoadRadComboTextAndValueP2(cboKPIType, "Job_Title_Skills_Emp_Get", Request.QueryString("empid"), Session("ReviewDate"), "skills", "id", False)

                If Request.QueryString("id") IsNot Nothing Then
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Development_Plan_Detail_Get", Request.QueryString("id"))
                    adevobjective.Value = strUser.Tables(0).Rows(0).Item("MyObjectives").ToString
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    'txtplanid.Text = strUser.Tables(0).Rows(0).Item("DevPlanID").ToString
                    txtplanid.Text = Request.QueryString("planid")
                    ainterventiondetail.Value = strUser.Tables(0).Rows(0).Item("intervention").ToString

                    Process.AssignRadComboValue(cboKPIType, strUser.Tables(0).Rows(0).Item("CompetencyType").ToString)
                    cboKPIType.Text = strUser.Tables(0).Rows(0).Item("CompetencyType").ToString
                    If IsDBNull(strUser.Tables(0).Rows(0).Item("targetdate")) = False Then
                        datDate.SelectedDate = strUser.Tables(0).Rows(0).Item("targetdate").ToString
                    End If
                    ainterventiontype.Value = strUser.Tables(0).Rows(0).Item("interventiontype").ToString
                    Process.LoadListAndComboxFromDataset(lstResponsibity, cboResponsibity, "Performance_Development_Plan_Responsibility_Get", "name", "empid", txtid.Text)
                    Process.LoadListAndComboxFromDataset(lsttraining, cbotraining, "Performance_Development_Plan_Training_Get_All", "training", "item", txtid.Text)
                Else
                    txtid.Text = "0"
                    txtplanid.Text = Request.QueryString("planid")
                    Process.LoadListAndComboxFromDataset(lstResponsibity, cboResponsibity, "Performance_Development_Plan_Responsibility_Get", "name", "empid", 0)
                    Process.LoadListAndComboxFromDataset(lsttraining, cbotraining, "Performance_Development_Plan_Training_Get_All", "training", "item", 0)
                    ainterventiondetail.Value = ""
                    ainterventiontype.Value = ""
                    Process.LoadRadComboTextAndValueP2(cboKPIType, "Job_Title_Skills_Emp_Get", Request.QueryString("empid"), Session("ReviewDate"), "skills", "id", False)
                End If
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Function GetIdentity(ByVal devplanid As Integer, ByVal kpiobj As String, ByVal myobjective As String, ByVal kpitypes As String, _
                                ByVal interventiontype As String, ByVal intervention As String, ByVal targetdate As Date, competencyType As String) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Performance_Development_Plan_Detail_Update"
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@DevPlanID", SqlDbType.Int).Value = devplanid
            cmd.Parameters.Add("@KPIObjectives", SqlDbType.VarChar).Value = kpiobj
            cmd.Parameters.Add("@MyObjectives", SqlDbType.VarChar).Value = myobjective
            cmd.Parameters.Add("@KPIType", SqlDbType.VarChar).Value = kpitypes
            cmd.Parameters.Add("@interventiontype", SqlDbType.VarChar).Value = interventiontype
            cmd.Parameters.Add("@intervention", SqlDbType.VarChar).Value = intervention
            cmd.Parameters.Add("@targetdate", SqlDbType.Date).Value = Process.DDMONYYYY(targetdate)
            cmd.Parameters.Add("@competencyType", SqlDbType.VarChar).Value = competencyType
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
            Return "0"
        End Try
    End Function
    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            Dim lblstatus As String = ""
            If txtid.Text <> "0" Then
                If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False And Process.AuthenAction(Session("role"), AuthenCode2, "Update") = False Then
                    Process.loadalert(divalert, msgalert, Process.privilegemsg, "warning")                    
                    Exit Sub
                End If
            End If

            If adevobjective.Value.Trim = "" Then
                lblstatus = "Your objective is required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                adevobjective.Focus()
                Exit Sub
            End If

            If ainterventiontype.Value.Trim = "" Then
                lblstatus = "Intervention Type is required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                ainterventiontype.Focus()
                Exit Sub
            End If

            If ainterventiondetail.Value.Trim = "" Then
                lblstatus = "Intervention detail is required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                ainterventiondetail.Focus()
                Exit Sub
            End If

            If datDate.SelectedDate Is Nothing Then
                lblstatus = "Target date is required!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                datDate.Focus()
                Exit Sub
            End If

            If txtid.Text = "" Or txtid.Text = "0" Then
                txtid.Text = GetIdentity(txtplanid.Text, "", adevobjective.Value, "", ainterventiontype.Value, ainterventiondetail.Value.Trim, datDate.SelectedDate, cboKPIType.SelectedValue)
                If txtid.Text = "0" Then
                    Exit Sub
                End If
            Else
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Development_Plan_Detail_Update", txtid.Text, txtplanid.Text, "", adevobjective.Value, "", ainterventiontype.Value, ainterventiondetail.Value.Trim, datDate.SelectedDate, cboKPIType.SelectedValue)
            End If

            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Development_Plan_Responsibility_Update_Stat", txtid.Text, "N")
            Dim collection As IList(Of RadComboBoxItem) = cboResponsibity.CheckedItems
            If collection.Count > 0 Then
                If (collection.Count <> 0) Then
                    For Each item As RadComboBoxItem In collection
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Development_Plan_Responsibility_Update", txtid.Text, item.Value)
                    Next
                End If
            End If
            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Development_Plan_Responsibility_delete", txtid.Text)

            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Development_Plan_Training_Delete", txtid.Text)
            Dim coll As IList(Of RadComboBoxItem) = cbotraining.CheckedItems
            If coll.Count > 0 Then
                If (coll.Count <> 0) Then
                    For Each item As RadComboBoxItem In coll
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Development_Plan_Training_Update", txtid.Text, item.Value)
                    Next
                End If
            End If
            Session("devplan") = "True"
            lblstatus = "Record saved"
            Process.loadalert(divalert, msgalert, lblstatus, "success")
            Response.Redirect("DevPlanUpdate?id=" & txtplanid.Text, True)

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            'If Session("PreviousPage").ToString = "" Then
            '    Response.Write("<script language='javascript'> { self.close() }</script>")
            'Else
            '    Response.Redirect(Session("PreviousPage").ToString)
            'End If
            'Response.Write("<script language='javascript'> { self.close() }</script>")
            Response.Redirect("DevPlanUpdate?id=" & txtplanid.Text, True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub



    'Private Sub Button3_Click(sender As Object, e As System.EventArgs) Handles Button3.Click
    '    Process.LoadListBoxFromCombo(lstResponsibity, cboResponsibity)
    'End Sub

    Protected Sub cboResponsibity_ItemChecked(sender As Object, e As Telerik.Web.UI.RadComboBoxItemEventArgs) Handles cboResponsibity.ItemChecked
        'Process.LoadListBoxFromCombo(lstResponsibity, cboResponsibity)
        Try
            lstResponsibity.Items.Clear()
            Dim collection As IList(Of RadComboBoxItem) = cboResponsibity.CheckedItems
            If (collection.Count <> 0) Then
                For Each item As RadComboBoxItem In collection
                    Dim listitem As New RadListBoxItem()
                    listitem.Text = item.Text
                    listitem.Value = item.Value
                    lstResponsibity.Items.Add(listitem)
                    listitem.DataBind()
                Next
            Else
                lstResponsibity.Items.Clear()
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub cbotraining_ItemChecked(sender As Object, e As Telerik.Web.UI.RadComboBoxItemEventArgs) Handles cbotraining.ItemChecked
        'Process.LoadListBoxFromCombo(lsttraining, cbotraining)
        Try
            lsttraining.Items.Clear()
            Dim collection As IList(Of RadComboBoxItem) = cbotraining.CheckedItems
            If (collection.Count <> 0) Then
                For Each item As RadComboBoxItem In collection
                    Dim listitem As New RadListBoxItem()
                    listitem.Text = item.Text
                    listitem.Value = item.Value
                    lsttraining.Items.Add(listitem)
                    listitem.DataBind()
                Next
            Else
                lsttraining.Items.Clear()
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
End Class