Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO


Public Class CompetencyJobTitleUpdate
    Inherits System.Web.UI.Page
    Dim comp As New clsCompetence
    Dim AuthenCode As String = "COMPETJOBTITLE"
    Dim olddata(3) As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                'Company_Structure_get_parent
                Process.LoadRadComboTextAndValue(radJobTitle, "Job_Titles_get_all", "Name", "Name")
                If Request.QueryString("id") IsNot Nothing Then
                    Process.AssignRadComboValue(radJobTitle, Request.QueryString("id"))
                    radJobTitle.Enabled = False
                    Process.LoadListBoxP1(lstSource, "Competency_JobTitle_GetSource", radJobTitle.SelectedItem.Text, "Name")
                    Process.LoadListBoxP1(lstDestination, "Competency_JobTitle_Get_Mapping", radJobTitle.SelectedItem.Text, "Name")
                Else
                    Process.LoadListBox(lstSource, "Competency_get_all", 1)
                    Process.LoadListBoxP1(lstDestination, "Competency_JobTitle_Get_Mapping", "", "Name")
                End If
                lstSource.Sort = Telerik.Web.UI.RadListBoxSort.Ascending
                lstDestination.Sort = Telerik.Web.UI.RadListBoxSort.Ascending
            End If
        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False Then
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
                Exit Sub
            End If

            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Competency_JobTitle_Delete", radJobTitle.SelectedItem.Text)
            For i As Integer = 0 To lstDestination.Items.Count - 1
                Dim competencies As String = lstDestination.Items.Item(i).Text
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Competency_JobTitle_Update", radJobTitle.SelectedItem.Text, competencies)
            Next

            lblstatus.Text = "Record saved"
            'Response.Write("<script language='javascript'> { self.close() }</script>")
        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Response.Write("<script language='javascript'> { self.close() }</script>")
        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub


End Class