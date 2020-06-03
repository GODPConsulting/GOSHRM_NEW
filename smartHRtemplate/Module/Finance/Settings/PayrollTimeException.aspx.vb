Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Imports Telerik.Web.UI
Imports System.Web.UI
Imports System.Threading


Public Class PayrollTimeException
    Inherits System.Web.UI.Page

    Dim AuthenCode As String = "PAYROLLOPT"
    Dim olddata(7) As String
    Dim Pages As String = "Payroll Attendance Exception"
    Dim FileURL As String = ConfigurationManager.AppSettings("FileURL")
    Private Function LoadData() As DataTable
        Dim datatables As New DataTable
        datatables = Process.SearchData("Payroll_Attendance_Grade_Exception_Get_All", Request.QueryString("id"))
        Return datatables
    End Function
    Private Sub LoadGrid()
        Try
            GridVwHeaderChckbox.DataSource = LoadData()
            GridVwHeaderChckbox.AllowSorting = True
            GridVwHeaderChckbox.AllowPaging = True
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception
            lblstatus.Text = ex.Message
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not Me.IsPostBack Then
                Process.LoadRadComboTextAndValueP1(radJobGrade, "Job_Grade_Get_all_Payroll_Attendance", Request.QueryString("id"), "name", "name", False)
                'Process.LoadListBox(lstMakeup, "Finance_Monthly_Earning_Items_Get_All", 1)
                txtid.Text = Request.QueryString("id")
                Dim strUser As New DataSet
                strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Payroll_Options_Get", Request.QueryString("id"))
                lblCompany.Text = strUser.Tables(0).Rows(0).Item("company").ToString
                LoadGrid()

            End If
        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub
   
    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAddGrade.Click
        Try
            lblstatus.Text = ""
            If txtid.Text <> "0" Then
                If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False Or Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                    lblstatus.Text = "You don't have privilege to perform this action"
                    ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
                    Exit Sub
                End If
            End If



            'Old Data

          

            Dim ccc As Integer = radJobGrade.CheckedItems.Count
            If radJobGrade.CheckedItems.Count > 0 Then
                Dim collection As IList(Of RadComboBoxItem) = radJobGrade.CheckedItems
                If (collection.Count <> 0) Then
                    For Each item As RadComboBoxItem In collection
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Payroll_Attendance_Grade_Exception_Update", item.Value, Request.QueryString("id"))
                    Next
                End If
            Else
                lblstatus.Text = "no selection made!"
                Exit Sub
            End If

            'SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Payroll_Attendance_Grade_Exception_Update", radJobGrade.SelectedValue, Request.QueryString("id"))

            Process.LoadRadComboTextAndValueP1(radJobGrade, "Job_Grade_Get_all_Payroll_Attendance", Request.QueryString("id"), "name", "name", False)
            LoadGrid()
            lblstatus.Text = ccc.ToString & " job grades saved"
            'Response.Write("<script language='javascript'> { self.close() }</script>")
        Catch ex As Exception
            lblstatus.Text = ex.Message
        End Try
    End Sub

  


    Protected Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            lblstatus.Text = ""
            If Process.AuthenAction(Session("role"), AuthenCode, "Delete") = False Then
                lblstatus.Text = "You don't have privilege to perform this action"
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
                Exit Sub
            End If
            Dim confirmValue As String = Request.Form("confirm_value")
            If confirmValue = "Yes" Then
                Dim atLeastOneRowDeleted As Boolean = False
                ' Iterate through the Products.Rows property
                For Each row As GridViewRow In GridVwHeaderChckbox.Rows
                    ' Access the CheckBox
                    Dim cb As CheckBox = row.FindControl("chkEmp")
                    If cb IsNot Nothing AndAlso cb.Checked Then
                        ' Delete row! (Well, not really...)
                        atLeastOneRowDeleted = True
                        ' First, get the ProductID for the selected row
                        Dim ID As String = _
                            Convert.ToString(GridVwHeaderChckbox.DataKeys(row.RowIndex).Value)
                        ' "Delete" the row
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Payroll_Attendance_Grade_Exception_Delete", ID)
                    End If
                Next
                Process.LoadRadComboTextAndValueP1(radJobGrade, "Job_Grade_Get_all_Payroll_Attendance", Request.QueryString("id"), "name", "name", False)
                LoadGrid()
            Else
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Delete has been cancelled" + "')", True)
            End If
        Catch ex As Exception
            lblstatus.Text = ex.Message
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    Protected Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            lblstatus.Text = ""
            If Process.Export(GridVwHeaderChckbox, "payroll_attendance_grade_exception", 1, 4) = False Then
                lblstatus.Text = Process.strExp
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & lblstatus.Text + "')", True)
            End If
        Catch ex As Exception
            lblstatus.Text = ex.Message
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False Then
                Response.Write("You don't have privilege to perform this action")
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
                Exit Sub
            End If


            If FileUpload1.HasFile AndAlso Not FileUpload1.PostedFile Is Nothing Then

                'To create a PostedFile
                Dim csvPath As String = Server.MapPath(FileURL) + Path.GetFileName(FileUpload1.PostedFile.FileName)
                If File.Exists(csvPath) = True Then
                    File.Delete(csvPath)
                End If
                FileUpload1.SaveAs(csvPath)
                Dim proc As String = ""


                If Process.ImportWithUsersAndP1(csvPath, "Payroll_Attendance_Grade_Exception_Update", txtid.Text, Pages) = True Then
                    ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Uploaded " & Session("uploadcnt") & " record(s)" + "')", True)
                    Response.Write("Uploaded " & Session("uploadcnt") & " record(s)")
                Else
                    Response.Write(Process.strExp)
                End If
                Process.GetAuditTrailInsertandUpdate("", "Uploaded " & Session("uploadcnt") & " record(s) from " & FileUpload1.PostedFile.FileName, "File Upload", Pages)
                LoadGrid()
            Else
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Choose file to upload" + "')", True)
                FileUpload1.Focus()
            End If

            'Response.Write("<script language='javascript'> { self.close() }</script>")
        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub
End Class