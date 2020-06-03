Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO


Public Class CompetencyJobGradeUpdate
    Inherits System.Web.UI.Page
    Dim comp As New clsCompetence
    Dim AuthenCode As String = "COMPETJOBGRADE"
    Dim olddata(3) As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                'Company_Structure_get_parent
                Process.LoadRadComboTextAndValue(radJobTitle, "Job_Grade_get_all", "Name", "Name", False)
                Process.LoadRadComboTextAndValue(cboKPIType, "Competency_Group_Get_All", "Name", "id", False)
                If Request.QueryString("jobgrade") IsNot Nothing Then
                    Dim strGrade As New DataSet
                    strGrade = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Competency_JobGrade_Get", Request.QueryString("jobgrade"), Request.QueryString("type"))
                    Process.AssignRadComboValue(radJobTitle, strGrade.Tables(0).Rows(0).Item("JobGrade").ToString)
                    Process.AssignRadComboValue(cboKPIType, strGrade.Tables(0).Rows(0).Item("CompetencyType").ToString)
                    txtWeight.Value = strGrade.Tables(0).Rows(0).Item("Weight").ToString
                    radJobTitle.Enabled = False
                    cboKPIType.Enabled = False
                    Process.LoadListBoxP2(lstSource, "Competency_JobGrade_GetSource", radJobTitle.SelectedItem.Text, cboKPIType.SelectedValue, "Name")
                    Process.LoadListBoxP2(lstDestination, "Competency_JobGrade_Get_Mapping", radJobTitle.SelectedItem.Text, cboKPIType.SelectedValue, "Name")
                    'Else
                    '    Process.LoadListBox(lstSource, "Competency_get_all", 1)
                    '    Process.LoadListBoxP1(lstDestination, "Competency_JobGrade_Get_Mapping", "", "Name")
                Else
                    Process.LoadListBoxP1(lstSource, "Competency_JobGrade_GetSource1", cboKPIType.SelectedValue, "Name")
                    lstDestination.Items.Clear()
                End If
                lstSource.Sort = Telerik.Web.UI.RadListBoxSort.Ascending
                lstDestination.Sort = Telerik.Web.UI.RadListBoxSort.Ascending
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False Then
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
                Exit Sub
            End If

            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Competency_JobGrade_Delete", radJobTitle.SelectedItem.Text, cboKPIType.SelectedItem.Text)
            For i As Integer = 0 To lstDestination.Items.Count - 1
                Dim competencies As String = lstDestination.Items.Item(i).Text
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Competency_JobGrade_Update", radJobTitle.SelectedItem.Text, competencies, txtWeight.Value)
            Next

            'lblstatus.Text = "Record saved"
            Process.loadalert(divalert, msgalert, "Record saved", "success")
            'Response.Write("<script language='javascript'> { self.close() }</script>")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect("KPIJobGrade.aspx", True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


    Protected Sub cboKPIType_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboKPIType.SelectedIndexChanged
        Try
            Process.LoadListBoxP1(lstSource, "Competency_JobGrade_GetSource1", cboKPIType.SelectedValue, "Name")
            lstDestination.Items.Clear()
        Catch ex As Exception
        End Try
    End Sub
End Class