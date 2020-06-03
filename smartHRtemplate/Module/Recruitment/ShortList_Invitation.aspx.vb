Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Collections.Generic
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Telerik.Web.UI

Public Class ShortList_Invitation
    Inherits System.Web.UI.Page
    Dim jobpost As New clsJobPost
    Dim olddata(22) As String
    Dim AuthenCode As String = "JOBPOST"
    Private sMsg As New StringBuilder
    Dim emailList As String

    Private Sub LoadDynamic()
        Try
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim JobTitle As String = ""
            If Not Me.IsPostBack Then
                cboInviteType.Items.Clear()
                cboInviteType.Items.Add("Interview")
                cboInviteType.Items.Add("Test")

                'If Request.QueryString("id") IsNot Nothing Then
                Dim strUser As New DataSet
                strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Job_Post_get", Session("JobID"))
                'txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                txtJob.Text = strUser.Tables(0).Rows(0).Item("Title").ToString

                Dim strMsg As New DataSet
                strMsg = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Job_Mailbox_Get_DefaultMessage", Session("JobID"))
                If strMsg.Tables(0).Rows.Count > 0 Then
                    sMsg.AppendLine(strMsg.Tables(0).Rows(0).Item("Message").ToString)
                Else
                    sMsg.AppendLine("For the role of " & txtJob.Text & " you have been invited for")
                    sMsg.AppendLine()
                    sMsg.AppendLine()
                    sMsg.AppendLine()
                    sMsg.AppendLine()
                    sMsg.AppendLine()
                    sMsg.AppendLine()
                    sMsg.AppendLine()
                    sMsg.AppendLine()
                    sMsg.AppendLine("HR Department,")
                    sMsg.AppendLine(Process.GetCompanyName())
                End If

               
                'End If
            End If
        Catch ex As Exception
            'lblstatus.Text = ex.Message
        End Try
    End Sub

    Protected Sub cboInviteType_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboInviteType.SelectedIndexChanged
        Try
            If cboInviteType.SelectedItem.Text.Contains("Interview") Then

            End If
        Catch ex As Exception

        End Try
    End Sub
End Class