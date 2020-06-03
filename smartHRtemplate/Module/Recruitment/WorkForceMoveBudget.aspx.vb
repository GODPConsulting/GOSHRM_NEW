Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Telerik.Web.UI


Public Class WorkForceMoveBudget
    Inherits System.Web.UI.Page
    Dim leaveperiod As New clsLeavePeriod
    Dim olddata(4) As String
    Private Sub LoadDrops()
        Try
            cboNewBudget.Items.Clear()
            For z As Integer = 2016 To 2100
                Dim itemTemp As New RadComboBoxItem()
                itemTemp.Text = z.ToString
                itemTemp.Value = z.ToString

                Dim strExists As New DataSet
                strExists = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_WorkForce_Budget_Get_Months", z.ToString, lblCompany.Text)
                If strExists.Tables(0).Rows.Count <= 0 Then
                    cboNewBudget.Items.Add(itemTemp)
                    itemTemp.DataBind()
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                'Company_Structure_get_parent
                

                If Request.QueryString("id") IsNot Nothing Then
                    lblid.Text = Request.QueryString("id").ToString
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_WorkForce_Budget_Get", Request.QueryString("id"))
                    lblBudget.Text = strUser.Tables(0).Rows(0).Item("budgetyear").ToString
                    lblCompany.Text = strUser.Tables(0).Rows(0).Item("office").ToString.ToUpper

                    LoadDrops()

                    If IsNumeric(lblBudget.Text) = True Then
                        Process.AssignRadComboValue(cboNewBudget, CInt(lblBudget.Text) + 1)
                    Else
                        Process.AssignRadComboValue(cboNewBudget, lblBudget.Text)
                    End If
                End If

            End If
        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            Dim confirmValue As String = Request.Form("confirm_value")
            If confirmValue = "Yes" Then
                System.Threading.Thread.Sleep(300)
                If IsNumeric(lblBudget.Text) = True And IsNumeric(cboNewBudget.SelectedValue) = True Then
                    If CInt(lblBudget.Text) >= CInt(cboNewBudget.SelectedValue) Then
                        lblstatus.Text = "Move Budget to a forward year only!"
                        cboNewBudget.Focus()
                        Exit Sub
                    Else
                        Dim strExists As New DataSet
                        strExists = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_WorkForce_Budget_Get_Months", cboNewBudget.SelectedValue, lblCompany.Text)

                        If strExists.Tables(0).Rows.Count > 0 Then
                            lblstatus.Text = lblCompany.Text & " already exist for " & cboNewBudget.SelectedValue
                            Exit Sub
                        Else
                            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_WorkForce_Budget_Move_Newyear", lblid.Text, Session("UserEmpID"), lblBudget.Text, cboNewBudget.Text, Session("LoginID"), "budget")
                            lblstatus.Text = lblCompany.Text & " moved to " & cboNewBudget.SelectedValue
                            LoadDrops()
                        End If
                    End If
                End If

            End If

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