Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Telerik.Web.UI
Imports System.IO

Public Class LeaveAllowanceSetup
    Inherits System.Web.UI.Page
    Dim payrolloption As New clsPayrollOption
    Dim AuthenCode As String = "LEAVEPAY"
    Dim olddata(5) As String
    Dim oFileAttached1 As Byte
    Dim oFileAttached As Byte()
    Dim Pages As String = "Leave Allowance Setting"
    Private Function LoadData() As DataTable
        Dim sdata As DataTable
        If txtsearch.Value.Trim = "" Then
            sdata = Process.GetData("Job_Grade_Leave_Allowance_Get_All")
        Else
            sdata = Process.SearchData("Job_Grade_Leave_Allowance_Search", txtsearch.Value)
        End If
        Return sdata
    End Function
    Private Sub LoadGrid()
        Try
            GridVwHeaderChckbox.PageIndex = Session("pageIndex1")
            GridVwHeaderChckbox.DataSource = LoadData()
            GridVwHeaderChckbox.AllowSorting = True
            GridVwHeaderChckbox.AllowPaging = True
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                cboyear.Items.Clear()
                For z As Integer = 2015 To 2050
                    Dim itemTemp As New RadComboBoxItem()
                    itemTemp.Text = z.ToString
                    itemTemp.Value = z.ToString
                    cboyear.Items.Add(itemTemp)
                    itemTemp.DataBind()
                    If (z = Now.Year) Then
                        Exit For
                    End If
                Next

                Session("pageIndex1") = 0
                Process.LoadRadComboTextAndValue(cboMakeup, "Finance_Monthly_Earning_Items_Get_All", "PAYSLIP ITEM", "PAYSLIP ITEM", False)

                Dim strUser As New DataSet
                strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Finance_Leave_Allowance_Setup_Get")
                If strUser.Tables(0).Rows.Count > 0 Then
                    Process.LoadListAndComboxFromDataset(lstMakeup, cboMakeup, "Finance_Leave_Items_Get_All", "item", "item", "23")
                    txtLeaveDesc.Value = strUser.Tables(0).Rows(0).Item("leavedesc").ToString
                    txtContribution.Value = strUser.Tables(0).Rows(0).Item("contribution").ToString
                    Process.AssignRadComboValue(cboyear, strUser.Tables(0).Rows(0).Item("startyear").ToString)
                Else
                    txtContribution.Value = "0"
                    Process.AssignRadComboValue(cboyear, Date.Now.Year)
                End If
                LoadGrid()
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub



    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False Then
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
                Exit Sub
            End If

            If (txtLeaveDesc.Value.Trim = "") Then
                Process.loadalert(divalert, msgalert, "Allowance Payslip description required!", "danger")
                txtLeaveDesc.Focus()
                Exit Sub
            End If

            If IsNumeric(txtContribution.Value) = False Then
                Process.loadalert(divalert, msgalert, "Leave Allowance required!", "danger")
                txtContribution.Focus()
                Exit Sub
            End If

            If CDbl(txtContribution.Value) < 100 And CDbl(txtContribution.Value) > 100 Then
                Process.loadalert(divalert, msgalert, "Leave Allowance invalid, only range 0 - 100 allowed!", "danger")
                txtContribution.Focus()
                Exit Sub
            End If

            Dim NewValue As String = ""
            Dim OldValue As String = ""


            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Finance_Leave_Allowance_Setup_Update", txtLeaveDesc.Value.Trim, txtContribution.Value.Trim, "No", cboyear.SelectedValue)
            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Finance_Leave_Items_delete")

            If lstMakeup.Items.Count > 0 Then
                Dim collection As IList(Of RadComboBoxItem) = cboMakeup.CheckedItems
                If (collection.Count <> 0) Then
                    For Each item As RadComboBoxItem In collection
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Finance_Leave_Items_Update", item.Value)
                    Next
                End If
            End If

            Process.loadalert(divalert, msgalert, "Record saved", "success")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub SortRecords(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Try
            Dim sortExpression As String = e.SortExpression
            Dim direction As String = String.Empty
            If SortDirection = SortDirection.Ascending Then
                SortDirection = SortDirection.Descending
                direction = " DESC"
            Else
                SortDirection = SortDirection.Ascending
                direction = " ASC"
            End If
            Dim table As DataTable = LoadData()
            table.DefaultView.Sort = sortExpression & direction
            GridVwHeaderChckbox.PageIndex = Session("pageIndex1")
            GridVwHeaderChckbox.DataSource = table
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Public Property SortDirection() As SortDirection
        Get
            If ViewState("SortDirection") Is Nothing Then
                ViewState("SortDirection") = SortDirection.Ascending
            End If
            Return DirectCast(ViewState("SortDirection"), SortDirection)
        End Get
        Set(ByVal value As SortDirection)
            ViewState("SortDirection") = value
        End Set
    End Property



    Protected Sub cboMakeup_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboMakeup.SelectedIndexChanged
        Try
            Process.LoadListBoxFromCombo(lstMakeup, cboMakeup)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub txtLeaveDesc_TextChanged(sender As Object, e As EventArgs)
        Try
            If txtLeaveDesc.Value.Trim = "" Then
                btnAddGrade.Disabled = True
            Else
                btnAddGrade.Disabled = False
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnAddGrade_Click(sender As Object, e As EventArgs)
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "danger")
                Exit Sub
            End If


            If (txtLeaveDesc.Value.Trim = "") Then
                Process.loadalert(divalert, msgalert, "Allowance Payslip description required!", "danger")
                txtLeaveDesc.Focus()
                Exit Sub
            End If
            Process.loadtype = "Add"
            Response.Redirect("Leaveallowancegrade")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub GridVwHeaderChckbox_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridVwHeaderChckbox.PageIndexChanging
        Try
            GridVwHeaderChckbox.PageIndex = e.NewPageIndex
            Session("pageIndex1") = e.NewPageIndex
            GridVwHeaderChckbox.DataSource = LoadData()
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Delete") = False Then
                Response.Write("You don't have privilege to perform this action")
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
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Job_Grade_Leave_Allowance_Delete", ID)
                    End If
                Next
                LoadGrid()
            Else
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Delete has been cancelled" + "')", True)
            End If
        Catch ex As Exception
             Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnExport_Click(sender As Object, e As EventArgs)
        Try

            'Process.Export(GridVwHeaderChckbox, "JobGrades", 1, 2)
            Response.Clear()
            Response.Buffer = True
            Response.AddHeader("content-disposition", "attachment;filename=LeaveAllowanceSetup.csv")
            Response.Charset = ""
            Response.ContentType = "application/text"
            Dim sBuilder As StringBuilder = New System.Text.StringBuilder()
            For index As Integer = 1 To GridVwHeaderChckbox.Columns.Count - 1
                sBuilder.Append(GridVwHeaderChckbox.Columns(index).HeaderText + ","c)

            Next
            sBuilder.Append(vbCr & vbLf)
            For i As Integer = 0 To GridVwHeaderChckbox.Rows.Count - 1
                For k As Integer = 1 To GridVwHeaderChckbox.HeaderRow.Cells.Count - 1
                    If k = 2 Then
                        Dim controls As HyperLink = DirectCast(GridVwHeaderChckbox.Rows(i).Cells(k).FindControl("HyperLink1"), HyperLink)
                        sBuilder.Append(controls.Text.Replace(",", "") + ",")
                    Else
                        sBuilder.Append(GridVwHeaderChckbox.Rows(i).Cells(k).Text.Replace(",", "") + ",")
                    End If
                Next
                sBuilder.Append(vbCr & vbLf)
            Next
            Response.Output.Write(sBuilder.ToString())
            Response.Flush()
            Response.[End]()

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs)
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                Response.Write("You don't have privilege to perform this action")
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
                Exit Sub
            End If

            If FileUpload1.PostedFile IsNot Nothing Then
                'To create a PostedFile
                Dim csvPath As String = Server.MapPath(Process.FileURL) + Path.GetFileName(FileUpload1.PostedFile.FileName)
                If File.Exists(csvPath) = True Then
                    File.Delete(csvPath)
                End If
                FileUpload1.PostedFile.SaveAs(csvPath)
                'Create byte Array with file len
                'File.ContentLength

                If Process.Import(csvPath, "Job_Grade_Leave_Allowance_Upload", Pages) = True Then
                    Process.loadalert(divalert, msgalert, "Uploaded " & Session("uploadcnt") & " record(s)", "success")
                Else
                    Process.loadalert(divalert, msgalert, Process.strExp, "success")
                End If
                Process.GetAuditTrailInsertandUpdate("", "Uploaded " & Session("uploadcnt") & " record(s) from " & csvPath, "File Upload", Pages)

            End If
            LoadGrid()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnFind_Click(sender As Object, e As EventArgs)
        Try
            LoadGrid()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub LinkButton2_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect("~/Module/Finance/Settings/EmpLeaveAllowanceSetup")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
End Class