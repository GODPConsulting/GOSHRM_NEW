Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports System.IO


Public Class EmployeeTrainingAssessment
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
                Dim strEmp As New DataSet
                strEmp = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Training_Sessions_Assessment_EmpID", Request.QueryString("assessid"))
                If strEmp.Tables(0).Rows.Count > 0 Then
                    EmpID = strEmp.Tables(0).Rows(0).Item("empid").ToString
                    lblTraining.Text = strEmp.Tables(0).Rows(0).Item("session").ToString
                End If

                Session("PreviousPage") = Request.UrlReferrer

                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Training_Sessions_Assessment_Create", Request.QueryString("assessid"))
                Process.saved = False
                Dim strUser As New DataSet
                strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Training_Sessions_Assessment_Get", Request.QueryString("assessid"), 1)
                Process.RadioListCheck(rdo1, strUser.Tables(0).Rows(0).Item("answer").ToString)
                strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Training_Sessions_Assessment_Get", Request.QueryString("assessid"), 2)
                Process.RadioListCheck(rdo2, strUser.Tables(0).Rows(0).Item("answer").ToString)
                strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Training_Sessions_Assessment_Get", Request.QueryString("assessid"), 3)
                Process.RadioListCheck(rdo3, strUser.Tables(0).Rows(0).Item("answer").ToString)
                strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Training_Sessions_Assessment_Get", Request.QueryString("assessid"), 4)
                Process.RadioListCheck(rdo4, strUser.Tables(0).Rows(0).Item("answer").ToString)
                strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Training_Sessions_Assessment_Get", Request.QueryString("assessid"), 5)
                Process.RadioListCheck(rdo5, strUser.Tables(0).Rows(0).Item("answer").ToString)
                strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Training_Sessions_Assessment_Get", Request.QueryString("assessid"), 6)
                txt6.Text = strUser.Tables(0).Rows(0).Item("answer").ToString
                strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Training_Sessions_Assessment_Get", Request.QueryString("assessid"), 7)
                txt7.Text = strUser.Tables(0).Rows(0).Item("answer").ToString
                strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Training_Sessions_Assessment_Get", Request.QueryString("assessid"), 8)
                txt8.Text = strUser.Tables(0).Rows(0).Item("answer").ToString
                If EmpID <> Session("UserEmpID") Then
                    rdo1.Enabled = False
                    rdo2.Enabled = False
                    rdo3.Enabled = False
                    rdo4.Enabled = False
                    rdo5.Enabled = False
                    txt6.Enabled = False
                    txt7.Enabled = False
                    txt8.Enabled = False
                    btnAdd.Visible = False
                    btnComplete.Visible = False
                End If

            End If
        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            Dim cvtype As String = ""
            Dim cvname As String = ""
            Dim cvfile As Byte() = Nothing
            If Not FileUpload1.PostedFile Is Nothing Then
                ''To create a PostedFile
                Dim img_strm As Stream = FileUpload1.PostedFile.InputStream
                Dim img_len As Integer = FileUpload1.PostedFile.ContentLength
                cvtype = FileUpload1.PostedFile.ContentType.ToString()
                cvname = Path.GetFileName(FileUpload1.PostedFile.FileName)
                cvfile = New Byte(img_len - 1) {}
                Dim n As Integer = img_strm.Read(cvfile, 0, img_len)
            Else
                lblstatus.ForeColor = Color.Blue
                lblstatus.Text = "No document available for upload, please select a document"
                FileUpload1.Focus()
                Exit Sub
            End If

            If Session("EmpID") = Session("UserEmpID") Then
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Training_Sessions_Assessment_Update", Request.QueryString("assessid"), 1, rdo1.SelectedValue, cvfile, cvtype, cvname)
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Training_Sessions_Assessment_Update", Request.QueryString("assessid"), 2, rdo2.SelectedValue, cvfile, cvtype, cvname)
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Training_Sessions_Assessment_Update", Request.QueryString("assessid"), 3, rdo3.SelectedValue, cvfile, cvtype, cvname)
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Training_Sessions_Assessment_Update", Request.QueryString("assessid"), 4, rdo4.SelectedValue, cvfile, cvtype, cvname)
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Training_Sessions_Assessment_Update", Request.QueryString("assessid"), 5, rdo5.SelectedValue, cvfile, cvtype, cvname)
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Training_Sessions_Assessment_Update", Request.QueryString("assessid"), 6, txt6.Text, cvfile, cvtype, cvname)
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Training_Sessions_Assessment_Update", Request.QueryString("assessid"), 7, txt7.Text, cvfile, cvtype, cvname)
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Training_Sessions_Assessment_Update", Request.QueryString("assessid"), 8, txt8.Text, cvfile, cvtype, cvname)
                lblstatus.Text = "Assessment Saved"
                Process.saved = True
            Else
                lblstatus.Text = "Only appropriate trainee can update assessment"
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & lblstatus.Text + "')", True)
            End If

        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Response.Redirect(Session("PreviousPage").ToString())
        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub



    Protected Sub btnComplete_Click(sender As Object, e As EventArgs) Handles btnComplete.Click
        Try
            lblstatus.Text = ""
            Dim confirmValue As String = Request.Form("confirm_value1")
            If confirmValue = "No" Then
                lblstatus.Text = "Cancelled"
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & lblstatus.Text + "')", True)
            Else
                If (txt6.Text.Trim = "" Or txt7.Text.Trim = "" Or txt8.Text.Trim = "" Or rdo1.SelectedValue Is Nothing Or rdo2.SelectedValue Is Nothing _
                    Or rdo3.SelectedValue Is Nothing Or rdo4.SelectedValue Is Nothing Or rdo5.SelectedValue Is Nothing) Then
                    lblstatus.ForeColor = Color.Red
                    lblstatus.Text = "All questions requires a respond, completion cancelled!"
                    ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & lblstatus.Text + "')", True)
                    Exit Sub
                End If

                Dim cvtype As String = ""
                Dim cvname As String = ""
                Dim cvfile As Byte() = Nothing
                If Not FileUpload1.PostedFile Is Nothing Then
                    ''To create a PostedFile
                    Dim img_strm As Stream = FileUpload1.PostedFile.InputStream
                    Dim img_len As Integer = FileUpload1.PostedFile.ContentLength
                    cvtype = FileUpload1.PostedFile.ContentType.ToString()
                    cvname = Path.GetFileName(FileUpload1.PostedFile.FileName)
                    cvfile = New Byte(img_len - 1) {}
                    Dim n As Integer = img_strm.Read(cvfile, 0, img_len)
                Else
                        lblstatus.ForeColor = Color.Blue
                        lblstatus.Text = "No document available for upload, please select a document"
                        FileUpload1.Focus()
                        Exit Sub
                End If


                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Training_Sessions_Assessment_Update", Request.QueryString("assessid"), 1, rdo1.SelectedValue, cvfile, cvtype, cvname)
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Training_Sessions_Assessment_Update", Request.QueryString("assessid"), 2, rdo2.SelectedValue, cvfile, cvtype, cvname)
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Training_Sessions_Assessment_Update", Request.QueryString("assessid"), 3, rdo3.SelectedValue, cvfile, cvtype, cvname)
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Training_Sessions_Assessment_Update", Request.QueryString("assessid"), 4, rdo4.SelectedValue, cvfile, cvtype, cvname)
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Training_Sessions_Assessment_Update", Request.QueryString("assessid"), 5, rdo5.SelectedValue, cvfile, cvtype, cvname)
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Training_Sessions_Assessment_Update", Request.QueryString("assessid"), 6, txt6.Text, cvfile, cvtype, cvname)
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Training_Sessions_Assessment_Update", Request.QueryString("assessid"), 7, txt7.Text, cvfile, cvtype, cvname)
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Training_Sessions_Assessment_Update", Request.QueryString("assessid"), 8, txt8.Text, cvfile, cvtype, cvname)
                Process.Training_Assessment_Complete("Training", lblTraining.Text, Session("userempid"), Process.GetEmployeeData(Session("userempid"), "linemanagerid"), Process.GetMailLink(AuthenCode, 2), Process.GetMailLink(AuthenCode, 1))

                lblstatus.ForeColor = Color.DarkGreen
                lblstatus.Text = "Application Assessment is complete!"
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & lblstatus.Text + "')", True)
            End If

        Catch ex As Exception
            lblstatus.Text = ex.Message
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & lblstatus.Text + "')", True)
        End Try
    End Sub
End Class