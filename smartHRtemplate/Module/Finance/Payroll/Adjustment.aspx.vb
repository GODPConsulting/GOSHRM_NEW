Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports Telerik.Web.UI
Imports System.IO
Imports GOSHRM.GOSHRM.GOSHRM.BO

Public Class Adjustment
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim payrolladj As New clsPayrollAdjustment
    Dim dtTable As DataTable
    Dim AuthenCode As String = "PAYGRADE"
    Dim Pages As String = "Pay Grade"

    Private Function LoadDataTables(loadtype As String) As DataTable
        Dim sData As New DataTable
        If Session("LoadType") = "All" Then
            sData = Process.SearchDataP3("Finance_Payslip_Adjustment_Get_All", cboCompany.SelectedValue, radYear.SelectedItem.Value, radMonth.SelectedItem.Value)
        ElseIf Session("LoadType") = "Find" Then
            sData = Process.SearchDataP4("Finance_Payslip_Adjustment_Search", cboCompany.SelectedValue, radYear.SelectedItem.Value, radMonth.SelectedItem.Value, txtsearch.Value.Trim)
        End If
        pagetitle.InnerText = cboCompany.SelectedValue & ": " & "Payroll Adjustment (" & FormatNumber(sData.Rows.Count, 0) & ")"
        Return sData
    End Function
    Private Sub LoadGrid(LoadType As String)
        Try
            GridView1.DataSource = LoadDataTables(LoadType)

            GridView1.AllowSorting = True
            GridView1.AllowPaging = True
            GridView1.DataBind()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not Me.IsPostBack Then
                If Process.AuthenAction(Session("role"), AuthenCode, "Read") = False Then
                    Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "danger")
                    Exit Sub
                End If

                radYear.Items.Clear()
                For z As Integer = 2015 To 2050
                    Dim itemTemp As New RadComboBoxItem()
                    itemTemp.Text = z.ToString
                    itemTemp.Value = z.ToString
                    radYear.Items.Add(itemTemp)
                    itemTemp.DataBind()
                    If (z = Now.Year) Then
                        Exit For
                    End If
                Next

                If Session("varMonth") Is Nothing Then
                    Session("varMonth") = Date.Now.Month
                End If
                Process.AssignRadComboValue(radMonth, Session("varMonth"))

                If Session("varYear") Is Nothing Then
                    Session("varYear") = Date.Now.Year
                End If
                Process.AssignRadComboValue(radYear, Session("varYear"))
                Dim ismulti As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select isnull(ismulticompany,'No')  from general_info")
                If ismulti.ToLower = "no" Then
                    Process.LoadRadComboTextAndValueP2(cboCompany, "Company_Structure_Get_ByLevel", "1", Session("Access"), "name", "name", False)
                Else
                    Process.LoadRadComboTextAndValueP2(cboCompany, "Company_Structure_Get_ByLevel", "2", Session("Access"), "name", "name", False)
                End If
                If Session("company") Is Nothing Then
                    Session("company") = Session("Organisation")
                End If
                Process.AssignRadComboValue(cboCompany, Session("company"))

                Session("LoadType") = "All"
                LoadGrid(Session("LoadType"))
            End If
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
            Dim table As DataTable = LoadDataTables(Session("LoadType"))
            table.DefaultView.Sort = sortExpression & direction
            GridView1.DataSource = table
            GridView1.DataBind()
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


    Protected Sub GridVwHeaderChckbox_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        Try
            GridView1.PageIndex = e.NewPageIndex
            GridView1.DataSource = LoadDataTables(Session("LoadType"))
            GridView1.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub OnRowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GridView1, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs)
        Try
            'If Not Me.IsPostBack Then
            If txtsearch.Value.Trim = "" Then
                Session("LoadType") = "All"
            Else
                Session("LoadType") = "Find"
            End If

            LoadGrid(Session("LoadType"))
            'End If
        Catch ex As Exception
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub


    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "danger")
                Exit Sub
            End If
            Process.loadtype = "Add"
            Response.Redirect("AdjustmentUpdate")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnFind1_Click(sender As Object, e As EventArgs)

    End Sub
    Protected Sub Delete(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Delete") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "danger")
                Exit Sub
            End If
            Dim confirmValue As String = Request.Form("confirm_value")
            If confirmValue = "Yes" Then
                Dim atLeastOneRowDeleted As Boolean = False
                ' Iterate through the Products.Rows property
                For Each row As GridViewRow In GridView1.Rows
                    ' Access the CheckBox
                    Dim cb As CheckBox = row.FindControl("chkEmp")
                    If cb IsNot Nothing AndAlso cb.Checked Then
                        ' Delete row! (Well, not really...)
                        atLeastOneRowDeleted = True
                        ' First, get the ProductID for the selected row
                        Dim ID As String = _
                            Convert.ToString(GridView1.DataKeys(row.RowIndex).Value)
                        ' "Delete" the row
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Finance_Payslip_Adjustment_Delete", ID)
                    End If
                Next
                LoadGrid(Session("LoadType"))
            Else
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Delete has been cancelled" + "')", True)
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub



    Protected Sub Button1_Click1(sender As Object, e As EventArgs)
        Try
            If Process.ExportExcel(LoadDataTables(""), "Adjustments") = False Then
                Response.Write(Process.strExp)
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & Process.strExp + "')", True)
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    Protected Sub Button1_Click2(sender As Object, e As EventArgs)
        Try
            For Each row As GridViewRow In GridView1.Rows
                ' Access the CheckBox
                Dim cb As CheckBox = row.FindControl("chkEmp")
                If cb IsNot Nothing AndAlso cb.Checked Then

                    Dim ID As String = Convert.ToString(GridView1.DataKeys(row.RowIndex).Value)
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Finance_Payslip_Adjustment_Get", ID)
                    If strUser.Tables(0).Rows.Count > 0 Then
                        Dim pid As Integer = strUser.Tables(0).Rows(0).Item("id").ToString
                        Dim lblref As String = strUser.Tables(0).Rows(0).Item("transref").ToString
                        'Process.AssignRadComboValue(cboemployee, strUser.Tables(0).Rows(0).Item("Employee").ToString)
                        Dim empname As String = strUser.Tables(0).Rows(0).Item("Employee").ToString
                        Dim adj As String = strUser.Tables(0).Rows(0).Item("AdjType").ToString
                        Dim txtAmount As String = strUser.Tables(0).Rows(0).Item("amount").ToString
                        Dim datPayDate As String = strUser.Tables(0).Rows(0).Item("Paydate").ToString
                        Dim cboTaxable As String = strUser.Tables(0).Rows(0).Item("taxable").ToString
                        Dim txtnote As String = strUser.Tables(0).Rows(0).Item("note").ToString
                        Dim ApprovalStatus As String = strUser.Tables(0).Rows(0).Item("approvalstatus").ToString

                        Dim ApprovedBy As String = strUser.Tables(0).Rows(0).Item("approver").ToString
                        Dim txtDesc As String = strUser.Tables(0).Rows(0).Item("Title").ToString
                        Dim lblID As String = strUser.Tables(0).Rows(0).Item("transref").ToString
                        If IsDBNull(strUser.Tables(0).Rows(0).Item("UpdatedOn")) = False Then
                            Dim lblupdatedon As String = strUser.Tables(0).Rows(0).Item("UpdatedOn")
                        End If
                        If IsDBNull(strUser.Tables(0).Rows(0).Item("approvaldate")) = False Then
                            Dim lblApprovedOn As String = strUser.Tables(0).Rows(0).Item("approvaldate")
                        End If

                        Dim lblupdatedby As String = strUser.Tables(0).Rows(0).Item("updatedby").ToString

                        If pid = "0" Then
                            'pid = GetIdentity(empname, txtDesc, adj, txtAmount, datPayDate, txtnote, cboTaxable, ApprovalStatus, Session("LoginID"))
                            If pid = "0" Then
                                Exit Sub
                            End If
                        Else
                            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Finance_Payslip_Adjustment_Update_Status", pid, Session("UserEmpID"), "Approved")
                            'SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Finance_Payslip_Adjustment_Update", pid, empname, txtDesc, adj, txtAmount, datPayDate, txtnote, cboTaxable, ApprovalStatus, Session("LoginID"))
                        End If

                        Process.loadalert(divalert, msgalert, "Approval Successful", "success")
                        Session("LoadType") = "All"
                        LoadGrid(Session("LoadType"))
                    End If
                End If
            Next
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


    Protected Sub btnUpload_Click(sender As Object, e As EventArgs)
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "danger")
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

                If Process.Import(csvPath, "Finance_Payslip_Adjustment_Upload", Pages) = True Then
                    Process.loadalert(divalert, msgalert, "Uploaded " & Session("uploadcnt") & " record(s)", "success")
                Else
                    Process.loadalert(divalert, msgalert, Process.strExp, "danger")
                End If
                Process.GetAuditTrailInsertandUpdate("", "Uploaded " & Session("uploadcnt") & " record(s) from " & csvPath, "File Upload", Pages)
            End If
            LoadGrid(Session("LoadType"))
            'Response.Write("<script language='javascript'> { self.close() }</script>")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Private Sub cboCompany_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboCompany.SelectedIndexChanged
        Try
            Session("company") = cboCompany.SelectedValue
            txtsearch.Value = ""
            Session("LoadType") = "All"
            LoadGrid(Session("LoadType"))
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub radYear_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles radYear.SelectedIndexChanged
        Try
            Session("varYear") = radYear.SelectedValue
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub radMonth_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles radMonth.SelectedIndexChanged
        Try
            Session("varMonth") = radMonth.SelectedValue
        Catch ex As Exception

        End Try

    End Sub
End Class