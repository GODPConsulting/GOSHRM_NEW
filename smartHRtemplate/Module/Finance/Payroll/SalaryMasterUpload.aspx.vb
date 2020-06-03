Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports System.IO


Public Class SalaryMasterUpload
    Inherits System.Web.UI.Page
    Dim workweek As New clsWorkWeek
    Dim AuthenCode As String = "SALARY"
    Dim olddata(4) As String
    Dim Pages As String = "Employee Salary Master"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then

            End If
        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            lblstatus.Text = "Uploading, please wait ..."
            If FileUpload1.HasFile AndAlso Not FileUpload1.PostedFile Is Nothing Then
                'To create a PostedFile
                Dim csvPath As String = Server.MapPath(Process.FileURL) + Path.GetFileName(FileUpload1.PostedFile.FileName)
                If File.Exists(csvPath) = True Then
                    File.Delete(csvPath)
                End If
                FileUpload1.SaveAs(csvPath)
                'Create byte Array with file len
                'File.ContentLength

                If Process.Import(csvPath, "Finance_Salary_Primary_Update", Pages) = True Then
                    lblstatus.Text = "Uploaded " & Session("uploadcnt") & " record(s)"
                Else
                    lblstatus.Text = Process.strExp
                End If
                Process.GetAuditTrailInsertandUpdate("", "Uploaded " & Session("uploadcnt") & " record(s) from " & csvPath, "File Upload", Pages)
            Else
                lblstatus.Text = ""
            End If

            'Response.Write("<script language='javascript'> { self.close() }</script>")
        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub


    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click

        Try
            Dim sPath As String = Server.MapPath(Process.sampleCSV)

            Response.AppendHeader("Content-Disposition", "attachment; filename=SalaryMaster.csv")
            Response.TransmitFile(sPath & Convert.ToString("SalaryMaster.csv"))
            Response.[End]()

        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try

    End Sub
End Class