Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO


Public Class StatView
    Inherits System.Web.UI.Page
    Dim exits As New clsExit
    Dim AuthenCode As String = ""
    Dim olddata(3) As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                'Company_Structure_get_parent
                If Request.QueryString("id") IsNot Nothing Then
                    Dim strUser As New DataSet

                    If Request.QueryString("type") = "promotion" Then
                        strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruitment_Promotion_Get", Request.QueryString("id"))
                    ElseIf Request.QueryString("type") = "succession" Then
                        strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruitment_Succession_Get", Request.QueryString("id"))
                    End If

                    approvername1.Value = strUser.Tables(0).Rows(0).Item("approvername1").ToString
                    approvercomment1.Value = strUser.Tables(0).Rows(0).Item("approvercomment1").ToString
                    If IsDate(strUser.Tables(0).Rows(0).Item("approverdate1")) Then
                        approverdate1.Value = CDate(strUser.Tables(0).Rows(0).Item("approverdate1")).ToLongDateString
                    End If

                    approval1.Value = strUser.Tables(0).Rows(0).Item("approverstatus1").ToString
                    lblfinalapprovalstat.Text = strUser.Tables(0).Rows(0).Item("finalstatus").ToString
                    If strUser.Tables(0).Rows(0).Item("approver2").ToString.ToLower <> "n/a" Then
                        approvername2.Value = strUser.Tables(0).Rows(0).Item("approvername2").ToString
                        approvercomment2.Value = strUser.Tables(0).Rows(0).Item("approvercomment2").ToString
                        If IsDate(strUser.Tables(0).Rows(0).Item("approverdate2")) Then
                            approverdate2.Value = CDate(strUser.Tables(0).Rows(0).Item("approverdate2")).ToLongDateString
                        End If
                        approval2.Value = strUser.Tables(0).Rows(0).Item("approverstatus2").ToString
                    Else
                        div2.Visible = False
                    End If

                    If strUser.Tables(0).Rows(0).Item("approver3").ToString.ToLower <> "n/a" Then
                        approvername3.Value = strUser.Tables(0).Rows(0).Item("approvername3").ToString
                        approvalcomment3.Value = strUser.Tables(0).Rows(0).Item("approvercomment3").ToString
                        If IsDate(strUser.Tables(0).Rows(0).Item("approverdate3")) Then
                            approvaldate3.Value = CDate(strUser.Tables(0).Rows(0).Item("approverdate3")).ToLongDateString
                        End If
                        approval3.Value = strUser.Tables(0).Rows(0).Item("approverstatus3").ToString
                    Else
                        div3.Visible = False
                    End If


                End If

            End If
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            Response.Write("<script language='javascript'> { self.close() }</script>")
        Catch ex As Exception

        End Try
    End Sub



End Class