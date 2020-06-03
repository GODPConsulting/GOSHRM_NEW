Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO


Public Class WorkApprovalStatView
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
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_WorkForce_Budget_Get", Request.QueryString("id"))

                    approver1.Value = strUser.Tables(0).Rows(0).Item("approver1name").ToString
                    approver1comment.Value = strUser.Tables(0).Rows(0).Item("approver1comment").ToString
                    If IsDate(strUser.Tables(0).Rows(0).Item("approver1date")) Then
                        approver1date.Value = CDate(strUser.Tables(0).Rows(0).Item("approver1date")).ToLongDateString
                    End If

                    If approver1.Value.Trim = "" Then
                        divapprover1.Visible = False
                    End If


                    approver1stat.Value = strUser.Tables(0).Rows(0).Item("approver1status").ToString
                    lblfinalapprovalstat.Text = strUser.Tables(0).Rows(0).Item("finalstatus").ToString
                    If strUser.Tables(0).Rows(0).Item("approver2name").ToString.ToLower <> "n/a" And strUser.Tables(0).Rows(0).Item("approver2name").ToString.Trim <> "" Then
                        approver2.Value = strUser.Tables(0).Rows(0).Item("approver2name").ToString
                        approver2comment.Value = strUser.Tables(0).Rows(0).Item("approver2comment").ToString

                        If IsDate(strUser.Tables(0).Rows(0).Item("approver2date")) Then
                            approver2date.Value = strUser.Tables(0).Rows(0).Item("approver2date").ToString
                        End If
                        approver2stat.Value = strUser.Tables(0).Rows(0).Item("approver2status").ToString
                    Else
                        divapprover2.Visible = False
                    End If

                    If strUser.Tables(0).Rows(0).Item("approver3name").ToString.ToLower <> "n/a" And strUser.Tables(0).Rows(0).Item("approver3name").ToString.Trim <> "" Then
                        approver3.Value = strUser.Tables(0).Rows(0).Item("approver3name").ToString
                        approver3comment.Value = strUser.Tables(0).Rows(0).Item("approver3comment").ToString
                        If IsDate(strUser.Tables(0).Rows(0).Item("approver3date")) Then
                            approver3date.Value = CDate(strUser.Tables(0).Rows(0).Item("approver3date")).ToLongDateString
                        End If

                        approver3stat.Value = strUser.Tables(0).Rows(0).Item("approver3status").ToString
                    Else
                        divapprover3.Visible = False
                    End If

                    If strUser.Tables(0).Rows(0).Item("approver4name").ToString.ToLower <> "n/a" And strUser.Tables(0).Rows(0).Item("approver4name").ToString.Trim <> "" Then
                        approver4.Value = strUser.Tables(0).Rows(0).Item("approver4name").ToString
                        approver4comment.Value = strUser.Tables(0).Rows(0).Item("approver4comment").ToString
                        If IsDate(strUser.Tables(0).Rows(0).Item("approver4date")) Then
                            approver4date.Value = CDate(strUser.Tables(0).Rows(0).Item("approver4date")).ToLongDateString
                        End If
                        approver4stat.Value = strUser.Tables(0).Rows(0).Item("approver4status").ToString
                    Else
                        divapprover4.Visible = False
                    End If

                    If divapprover1.Visible = False And divapprover2.Visible = False Then
                        Process.loadalert(divalert, msgalert, "No approver(s) has been selected by Human Resource Unit!", "info")
                    End If
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

        End Try
    End Sub



End Class