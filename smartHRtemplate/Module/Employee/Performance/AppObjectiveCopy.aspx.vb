Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Telerik.Web.UI


Public Class AppObjectiveCopy
    Inherits System.Web.UI.Page
    Dim leaveperiod As New clsLeavePeriod
    Dim olddata(4) As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                Process.LoadRadComboTextAndValueInitiateP1(cboSource, "Performance_Appraisal_Summary_Get_Period", Session("userempid"), "--select--", "period", "id")
                Process.LoadRadComboTextAndValueInitiateP2(cboDestination, "Performance_Appraisal_Summary_UNcreated_Month", Session("userempid"), Session("Organisation"), "--select--", "period", "id")
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            Dim lblstatus As String = ""
            Dim confirmValue As String = Request.Form("confirm_value")
            If confirmValue = "Yes" Then
                System.Threading.Thread.Sleep(300)
                If cboSource.SelectedValue = "" Or cboDestination.SelectedValue = "" Then
                    lblstatus = "source and target period is required!"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    Exit Sub
                Else
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_Copy", cboSource.SelectedValue, cboDestination.SelectedValue, Session("LoginID"))
                    lblstatus = "Appraisal Objectives: " & cboSource.SelectedItem.Text & " copied to " & cboDestination.SelectedItem.Text
                    Process.loadalert(divalert, msgalert, lblstatus, "success")
                    Process.DisableButton(btnAdd)
                End If

            End If            
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