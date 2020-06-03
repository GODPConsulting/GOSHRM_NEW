Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO


Public Class CustomNamingUpdate
    Inherits System.Web.UI.Page
    Dim workweek As New clsWorkWeek
    Dim AuthenCode As String = "CUSTOMNAMINGUPDATE"
    Dim olddata(4) As String
    Dim RID As String = "1"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                Dim sDataset As DataSet
                sDataset = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "SELECT * FROM Performance_Custom_Naming where id = '" + RID + "'")
                txtid.Text = sDataset.Tables(0).Rows(0).Item("id").ToString
                kpicategory.Value = sDataset.Tables(0).Rows(0).Item("KPICategory").ToString
                keyperformanceindicator.Value = sDataset.Tables(0).Rows(0).Item("KeyPerformanceIndicator").ToString
                kpi.Value = sDataset.Tables(0).Rows(0).Item("KPI").ToString
                kpitojobgrade.Value = sDataset.Tables(0).Rows(0).Item("KPIToJobGrade").ToString
                kpitype.Value = sDataset.Tables(0).Rows(0).Item("KPIType").ToString
                objectives.Value = sDataset.Tables(0).Rows(0).Item("Objectives").ToString
                successtarget.Value = sDataset.Tables(0).Rows(0).Item("SuccessTarget").ToString
                successmeasure.Value = sDataset.Tables(0).Rows(0).Item("SuccessMeasure").ToString
                keyaction.Value = sDataset.Tables(0).Rows(0).Item("KeyAction").ToString
                targetdate.Value = sDataset.Tables(0).Rows(0).Item("TargetDate").ToString
                performanceobjective.Value = sDataset.Tables(0).Rows(0).Item("PerformanceObjective").ToString
                appraisalfbk.Value = sDataset.Tables(0).Rows(0).Item("AppraisalFeedback").ToString
                appraisalfbkNugget.Value = sDataset.Tables(0).Rows(0).Item("AppraisalFeedbackNugget").ToString

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
            Dim i As Integer = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "update Performance_Custom_Naming set AppraisalFeedbackNugget='" & appraisalfbkNugget.Value & "', AppraisalFeedback ='" & appraisalfbk.Value & "', KPICategory = '" & kpicategory.Value & "', KeyPerformanceIndicator='" + keyperformanceindicator.Value + "', KPI='" + kpi.Value + "', KPIToJobGrade='" + kpitojobgrade.Value + "', KPIType='" + kpitype.Value + "', Objectives='" + objectives.Value + "', SuccessTarget='" + successtarget.Value + "', SuccessMeasure='" + successmeasure.Value + "', KeyAction='" + keyaction.Value + "', TargetDate='" + targetdate.Value + "', PerformanceObjective='" + performanceobjective.Value + "' where id = '" & RID & "'")

            Process.loadalert(divalert, msgalert, "Record Saved", "success")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect("~/Module/Reports/Finance/MenuSetup", True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    'Protected Sub radStructure_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.DropDownListEventArgs) Handles radDay.SelectedIndexChanged
    '    Try
    '        Process.LoadRadComboP1(radStatus, "Work_Week_get_parent", radDay.SelectedText, 0)
    '    Catch ex As Exception
    '        Process.loadalert(divalert, msgalert, ex.Message, "danger")
    '    End Try
    'End Sub
End Class