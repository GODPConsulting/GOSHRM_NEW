Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Telerik.Web.UI


Public Class WorkForceCopyMonthPlan
    Inherits System.Web.UI.Page
    Dim leaveperiod As New clsLeavePeriod
    Dim olddata(4) As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                'Company_Structure_get_parent

                acompany.Value = Session("Dept")
                abudget.Value = Request.QueryString("budgetyear")
                Process.LoadRadComboTextAndValueInitiateP2(cboSource, "Recruit_WorkForce_Budget_Get_Months", Request.QueryString("budgetyear"), acompany.Value, "--select--", "endperiod", "endperiod")
                Process.LoadRadComboTextAndValueInitiateP2(cboDestination, "Recruit_WorkForce_Budget_Get_Missing_Months", Request.QueryString("budgetyear"), acompany.Value, "--select--", "enddate", "enddate")
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnupdate.Click
        Try
            Dim lblstatus As String = ""
            Dim confirmValue As String = Request.Form("confirm_value")
            Dim collection As IList(Of RadComboBoxItem) = cboDestination.CheckedItems
            If confirmValue = "Yes" Then
                If cboSource.SelectedValue = "" Then
                    lblstatus = "source month is required!"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    Exit Sub
                Else
                    System.Threading.Thread.Sleep(300)
                    If (collection.Count <> 0) Then
                        For Each item As RadComboBoxItem In collection
                            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_WorkForce_Budget_Move_Month", acompany.Value, cboSource.SelectedValue, item.Value, Session("LoginID"))
                        Next
                        lblstatus = Process.DDMONYYYY(CDate(cboSource.SelectedValue)) & " copied "
                        Process.loadalert(divalert, msgalert, lblstatus, "success")
                        Process.LoadRadComboTextAndValueInitiateP2(cboSource, "Recruit_WorkForce_Budget_Get_Months", Request.QueryString("budgetyear"), acompany.Value, "--select--", "endperiod", "endperiod")
                        Process.LoadRadComboTextAndValueInitiateP2(cboDestination, "Recruit_WorkForce_Budget_Get_Missing_Months", Request.QueryString("budgetyear"), acompany.Value, "--select--", "enddate", "enddate")
                    Else
                        lblstatus = "destination month is required!"
                        Process.loadalert(divalert, msgalert, lblstatus, "warning")
                        Exit Sub
                    End If

                End If

            End If
            'Response.Write("<script language='javascript'> { self.close() }</script>")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            Response.Write("<script language='javascript'> { self.close() }</script>")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


End Class