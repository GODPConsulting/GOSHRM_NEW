Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Microsoft.VisualBasic

Imports System.IO


Public Class ZKTecoConfiguration
    Inherits System.Web.UI.Page




    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            '

            If Not Me.IsPostBack Then
                Dim strUser As New DataSet
                strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "zkteco_configuration_get")
                If strUser.Tables(0).Rows.Count > 0 Then
                    txtdatasource.Text = strUser.Tables(0).Rows(0).Item("datasource").ToString
                    txtuserid.Text = strUser.Tables(0).Rows(0).Item("userid").ToString
                    txtpwd.Text = strUser.Tables(0).Rows(0).Item("password").ToString
                    txtin.Text = strUser.Tables(0).Rows(0).Item("checkinindicator").ToString
                    txtout.Text = strUser.Tables(0).Rows(0).Item("checkoutindicator").ToString
                Else
                    txtin.Text = "I"
                    txtout.Text = "O"
                End If
           
            End If
        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub
    



    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Response.Redirect("~/Module/TimeManagement/EmployeeAttendanceMap.aspx")
        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub



    Protected Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        Try
            If txtdatasource.Text.Trim = "" Then
                lblstatus.Text = "Data Source required!"
                txtdatasource.Focus()
                Exit Sub
            End If

            If txtin.Text.Trim = "" Then
                lblstatus.Text = "Indicator that describes check-in event from the biometric device is required!"
                txtin.Focus()
                Exit Sub
            End If

            If txtout.Text.Trim = "" Then
                lblstatus.Text = "Indicator that describes check-out event from the biometric device is required!"
                txtout.Focus()
                Exit Sub
            End If

            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "zkteco_configuration_update", txtdatasource.Text.Trim, txtuserid.Text.Trim, txtpwd.Text, txtin.Text.Trim, txtout.Text.Trim)
            lblstatus.Text = "update saved"
            'If Process.UpdateAccessConnString(txtdatasource.Text, txtuserid.Text, txtpwd.Text) = True Then
            '    lblstatus.Text = "update saved"
            'Else
            '    lblstatus.Text = Process.strExp
            'End If
        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            If Process.TestAccessConnection(txtdatasource.Text) = True Then
                lblstatus.Text = "Connection successful"
            Else
                lblstatus.Text = "Connection failed, " & Process.strExp
            End If
        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub
End Class