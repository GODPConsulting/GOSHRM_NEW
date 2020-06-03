Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Telerik.Web.UI


Public Class WorkForceMovePlan
    Inherits System.Web.UI.Page
    Dim leaveperiod As New clsLeavePeriod
    Dim olddata(4) As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                'Company_Structure_get_parent
                cboNewBudget.Items.Clear()
                For z As Integer = 2016 To 2100
                    Dim itemTemp As New RadComboBoxItem()
                    itemTemp.Text = z.ToString
                    itemTemp.Value = z.ToString
                    cboNewBudget.Items.Add(itemTemp)
                    itemTemp.DataBind()
                Next

                If Request.QueryString("id") IsNot Nothing Then
                    lblid.Text = Request.QueryString("id").ToString
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_WorkForce_Budget_Get", Request.QueryString("id"))
                    abudget.Value = strUser.Tables(0).Rows(0).Item("budgetyear").ToString
                    acompany.Value = strUser.Tables(0).Rows(0).Item("office").ToString.ToUpper
                    If IsNumeric(abudget.Value) = True Then
                        Process.AssignRadComboValue(cboNewBudget, CInt(abudget.Value) + 1)
                    Else
                        Process.AssignRadComboValue(cboNewBudget, abudget.Value)
                    End If
                End If

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnupdate.Click
        Try
            Dim lblstatus As String = ""
            Dim confirmValue As String = Request.Form("confirm_value")
            If confirmValue = "Yes" Then
                If IsNumeric(abudget.Value) = True And IsNumeric(cboNewBudget.SelectedValue) = True Then
                    If CInt(abudget.Value) >= CInt(cboNewBudget.SelectedValue) Then
                        lblstatus = "Move Budget to a forward year only!"
                        cboNewBudget.Focus()
                        Exit Sub
                    Else
                        System.Threading.Thread.Sleep(300)
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_WorkForce_Budget_Move_Newyear", lblid.Text, Session("UserEmpID"), abudget.Value, cboNewBudget.Text, Session("LoginID"), "plan")
                        lblstatus = acompany.Value & " moved to " & cboNewBudget.SelectedValue
                        btnupdate.Enabled = False
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