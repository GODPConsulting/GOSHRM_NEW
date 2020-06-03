Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO


Public Class EmployeeTrainersUpdate
    Inherits System.Web.UI.Page
    Dim empTrainSession As New clsEmpTraining
    Dim AuthenCode As String = "EMPTRAINING"
    Dim olddata(6) As String
    Dim Level1(2) As String
    Dim EmpID As String
    Dim Separators() As Char = {":"c}

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then

                'Company_Structure_get_parent
                If Request.QueryString("id") IsNot Nothing Then
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Training_Session_Trainers_Get", Request.QueryString("id"))
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    txtEmployee.Text = Session("EmpName")
                    lblEmpID.Text = strUser.Tables(0).Rows(0).Item("empid").ToString
                    lblsession.Text = strUser.Tables(0).Rows(0).Item("name").ToString
                    
                    txtObjective.Text = strUser.Tables(0).Rows(0).Item("objectives").ToString
                    
                    txtComment.Text = strUser.Tables(0).Rows(0).Item("Comment").ToString
                    lbltime.Text = Process.AMPM_Time(strUser.Tables(0).Rows(0).Item("trainingtime").ToString)
                    lblvenue.Text = strUser.Tables(0).Rows(0).Item("DeliveryLocation").ToString.Replace(vbCrLf, "<br/>")
                    lbldate.Text = Process.DDMONYYYY(strUser.Tables(0).Rows(0).Item("ScheduledTime")) & " : " & Process.DDMONYYYY(strUser.Tables(0).Rows(0).Item("duedate"))
              
                End If
            End If
        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try

            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Training_Session_Trainers_Update", txtid.Text, txtComment.Text, Session("LoginID"))

            lblstatus.Text = "Record updated"
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