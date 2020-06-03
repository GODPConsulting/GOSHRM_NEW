Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Telerik.Web.UI


Public Class WorkForceCopyMonthBudget
    Inherits System.Web.UI.Page
    Dim leaveperiod As New clsLeavePeriod
    Dim olddata(4) As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                'Company_Structure_get_parent
                lblCompany.Text = Session("Dept")
                lblBudget.Text = Request.QueryString("budgetyear")
                Process.LoadRadComboTextAndValueInitiateP2(cboSource, "Recruit_WorkForce_Budget_Get_Months", Request.QueryString("budgetyear"), Session("Dept"), "--select--", "endperiod", "endperiod")
                Process.LoadRadComboTextAndValueInitiateP2(cboDestination, "Recruit_WorkForce_Budget_Get_Missing_Months", Request.QueryString("budgetyear"), Session("Dept"), "--select--", "enddate", "enddate")
            End If
        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            lblstatus.Text = ""
            Dim confirmValue As String = Request.Form("confirm_value")
            Dim collection As IList(Of RadComboBoxItem) = cboDestination.CheckedItems
            If confirmValue = "Yes" Then
                System.Threading.Thread.Sleep(300)
                If cboSource.SelectedValue = "" Then
                    lblstatus.Text = "source month is required!"
                    Exit Sub
                Else
                    If (collection.Count <> 0) Then
                        For Each item As RadComboBoxItem In collection
                            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_WorkForce_Budget_Move_Month", lblCompany.Text, cboSource.SelectedValue, item.Value, Session("LoginID"))
                        Next
                        lblstatus.Text = cboSource.SelectedValue & " copied "
                        'Process.DisableButton(btnAdd)
                    Else
                        lblstatus.Text = "destination month is required!"
                        Exit Sub
                    End If
                End If
                Process.LoadRadComboTextAndValueInitiateP2(cboSource, "Recruit_WorkForce_Budget_Get_Months", Request.QueryString("budgetyear"), Session("Dept"), "--select--", "endperiod", "endperiod")
                Process.LoadRadComboTextAndValueInitiateP2(cboDestination, "Recruit_WorkForce_Budget_Get_Missing_Months", Request.QueryString("budgetyear"), Session("Dept"), "--select--", "enddate", "enddate")
            End If
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