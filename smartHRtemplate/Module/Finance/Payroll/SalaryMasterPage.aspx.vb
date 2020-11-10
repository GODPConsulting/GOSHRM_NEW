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
Public Class Salary
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "SALARY"
    Dim Pages As String = "Employee Salary"
    Public Function GetUnitData() As DataTable
        Dim table As New DataTable()
        table.Columns.Add("ID")
        table.Columns.Add("ParentID")
        table.Columns.Add("Value")
        table.Columns.Add("Text")

        Dim strDataSet As New DataSet
        strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Company_Structure_dropdwon")
        If strDataSet.Tables(0).Rows.Count > 0 Then
            For i As Integer = 0 To strDataSet.Tables(0).Rows.Count - 1
                Dim id As String = ""
                Dim Parent As String = ""
                Dim Value As String = ""
                Dim TextField As String = ""

                id = strDataSet.Tables(0).Rows(i).Item("ID").ToString
                If strDataSet.Tables(0).Rows(i).Item("Parent").ToString = "" Then
                    Parent = Nothing
                ElseIf IsDBNull(strDataSet.Tables(0).Rows(i).Item("Parent")) Then
                    Parent = Nothing
                Else
                    Parent = strDataSet.Tables(0).Rows(i).Item("Parent").ToString
                End If

                Value = strDataSet.Tables(0).Rows(i).Item("Name").ToString
                TextField = strDataSet.Tables(0).Rows(i).Item("Name").ToString

                table.Rows.Add(New [String]() {id, Parent, Value, TextField})
            Next
        End If

        Return table
    End Function
    Private Function DataData() As DataSet
        Dim StGrade As String = ""
        Dim active As Integer = 0

        If chkActive.Checked = True Then
            active = 1
        End If

        Dim SP As String = ""
        Dim SP1 As String = ""
        Dim SP2 As String = ""
        Dim SP3 As String = ""

        search.Value = Session("salarysearch")

        Dim strDataSet As New DataSet
        'strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, script)
        If search.Value.Trim = "" Then
            If chkActive.Checked = True Then
                strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Finance_Salary_Active_Get_All", radOffice.SelectedValue)
            Else
                strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Finance_Salary_InActive_Get_All", radOffice.SelectedValue)
            End If

        Else
            If chkActive.Checked = True Then
                strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Finance_Salary_Active_Search", radOffice.SelectedValue, search.Value.Trim)
            Else
                strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Finance_Salary_InActive_Search", radOffice.SelectedValue, search.Value.Trim)
            End If

        End If
        If chkActive.Checked = True Then
            pagetitle.InnerText = radOffice.SelectedValue & ": Active Employee Salary (" & strDataSet.Tables(0).Rows.Count.ToString & ")"
        Else
            pagetitle.InnerText = radOffice.SelectedValue & ": Inactive Employee Salary (" & strDataSet.Tables(0).Rows.Count.ToString & ")"
        End If
        Return strDataSet
    End Function


    Private Sub LoadLoans()
        Try
 
            GridVwHeaderChckbox.PageIndex = CInt(Session("salaryindex"))
            GridVwHeaderChckbox.DataSource = DataData()
            GridVwHeaderChckbox.AllowSorting = True
            GridVwHeaderChckbox.AllowPaging = True
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Read") = False Then
                content.Style.Add("display", "none")
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform view this page", "info")
                Exit Sub
            End If
            If Not Me.IsPostBack Then
                Dim ismulti As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select isnull(ismulticompany,'No')  from general_info")
                If ismulti.ToLower = "no" Then
                    Process.LoadRadComboTextAndValueP2(radOffice, "Company_Structure_Get_ByLevel", "1", Session("Access"), "name", "name", False)
                    divcompany.Visible = False
                Else
                    Process.LoadRadComboTextAndValueP2(radOffice, "Company_Structure_Get_ByLevel", "2", Session("Access"), "name", "name", False)
                End If
                If Session("salarycompany") Is Nothing Then
                    Session("salarycompany") = Session("Organisation")
                End If
                Process.AssignRadComboValue(radOffice, Session("salarycompany"))

                If Session("salaryindex") Is Nothing Then
                    Session("salaryindex") = "0"
                End If

                If Session("salarysearch") Is Nothing Then
                    Session("salarysearch") = ""
                End If

                If Session("salaryactive") Is Nothing Then
                    Session("salaryactive") = "1"
                End If

                chkActive.Checked = CInt(Session("salaryactive"))
                LoadLoans()

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Protected Sub SortRecords(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Try
            Dim table As New DataTable

            Dim strDataSet As New DataSet
            

            Dim sortExpression As String = e.SortExpression
            Session("salarysort") = sortExpression
            Dim direction As String = String.Empty
            If SortsDirection = SortDirection.Ascending Then
                SortsDirection = SortDirection.Descending
                direction = " DESC"
            Else
                SortsDirection = SortDirection.Ascending
                direction = " ASC"
            End If

            strDataSet = DataData() 'SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, script)
            table = strDataSet.Tables(0)
            table.DefaultView.Sort = sortExpression & direction
            GridVwHeaderChckbox.PageIndex = CInt(Session("salaryindex"))
            GridVwHeaderChckbox.DataSource = table
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception
        End Try
    End Sub


    Public Property SortsDirection() As SortDirection
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


    Protected Sub GridVwHeaderChckbox_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridVwHeaderChckbox.PageIndexChanging
        Try

            GridVwHeaderChckbox.PageIndex = e.NewPageIndex
            Session("salaryindex") = e.NewPageIndex
            LoadLoans()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub OnRowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GridVwHeaderChckbox, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub


    Protected Sub btnFind_Click(sender As Object, e As EventArgs)
        Try
            Session("salarysearch") = search.Value.Trim
            LoadLoans()
            'End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Protected Sub Delete(sender As Object, e As EventArgs) Handles btDelete.Click
        Try
            Dim lblstatus As String = ""
            If Process.AuthenAction(Session("role"), AuthenCode, "Delete") = False Then
                Process.loadalert(divalert, msgalert, Process.privilegemsg, "warning")
                Exit Sub
            End If
            Dim count As Integer = 0
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
                        If ID <> "0" Then
                            count = count + 1
                        End If
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Finance_Salary_Primary_Delete", ID)
                    End If
                Next
                Process.loadalert(divalert, msgalert, count.ToString & " records successfully deleted", "success")
                LoadLoans()
           
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
                Exit Sub
            End If
            Process.loadtype = "Add"
            Response.Redirect("SalaryMasterUpdate")
        Catch ex As Exception

        End Try
    End Sub



    Protected Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
        Try
            Dim lblstatus As String = ""
            Dim confirmGen As String = Request.Form("confirm_ref")
            If confirmGen = "Yes" Then
                If Process.ResetEmployeeAllSalaryItem(radOffice.SelectedValue) = True Then                    
                    lblstatus = "All Salary Items successfully refreshed to " & radOffice.SelectedValue & " Employees PaySlips"
                    Process.loadalert(divalert, msgalert, lblstatus, "success")
                Else
                    Process.loadalert(divalert, msgalert, Session("exception"), "danger")
                End If
                LoadLoans()            
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnRegenerate_Click(sender As Object, e As EventArgs) Handles btnRegenerate.Click
        Try
            Dim lblstatus As String = ""
            Dim confirmGen As String = Request.Form("confirm_gen")
            If confirmGen = "Yes" Then
                If Process.ResetEmployeeSalaryItem(radOffice.SelectedValue) = True Then
                    lblstatus = "New Salary Items successfully included to Employees PaySlips"
                    Process.loadalert(divalert, msgalert, lblstatus, "success")
                Else
                    Process.loadalert(divalert, msgalert, Session("exception"), "danger")
                End If
                LoadLoans()

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs)
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                Process.loadalert(divalert, msgalert, Process.privilegemsg, "warning")                
                Exit Sub
            End If
            'lblstatus.Text = "Uploading, please wait ..."
            If Not file1.PostedFile Is Nothing Then
                'To create a PostedFile
                'System.Threading.Thread.Sleep(300)
                Dim csvPath As String = Server.MapPath(Process.FileURL) + Path.GetFileName(file1.PostedFile.FileName)
                If File.Exists(csvPath) = True Then
                    File.Delete(csvPath)
                End If
                file1.PostedFile.SaveAs(csvPath)

                If chkIsTransposed.Checked = True Then
                    If Process.ImportTransposed(csvPath, "Finance_Salary_Primary_Update", Pages) = True Then
                        Process.loadalert(divalert, msgalert, "Uploaded " & Session("uploadcnt") & " record(s)", "success")
                    Else
                        Process.loadalert(divalert, msgalert, Session("exception"), "danger")
                    End If
                Else
                    If Process.ImportWithUsers(csvPath, "Finance_Salary_Primary_Update", Pages) = True Then
                        Process.loadalert(divalert, msgalert, "Uploaded " & Session("uploadcnt") & " record(s)", "success")
                    Else
                        Process.loadalert(divalert, msgalert, Session("exception"), "danger")
                    End If
                End If
                LoadLoans()
                If File.Exists(csvPath) = True Then
                    File.Delete(csvPath)
                End If

                Process.GetAuditTrailInsertandUpdate("", "Uploaded " & Session("uploadcnt") & " record(s) from " & csvPath, "File Upload", Pages)
            Else
                Process.loadalert(divalert, msgalert, "No file selected to upload!", "warning")
                Exit Sub
            End If


        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Protected Sub btnExport_Click(sender As Object, e As EventArgs)
        Try
            'Process.Export(GridVwHeaderChckbox, "SalaryMaster", 1, 2)
            'System.Threading.Thread.Sleep(300)
            Dim filename As String = ""
            Dim empIDList As String = ""

            Dim TitleHeader As String()
            Dim TitleData As String()

            Dim scripts As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Scripts_get", "payroll")
            For i As Integer = 0 To GridVwHeaderChckbox.PageCount - 1
                GridVwHeaderChckbox.PageIndex = i
                For j As Integer = 0 To GridVwHeaderChckbox.Rows.Count - 1
                    Dim controls As HyperLink = DirectCast(GridVwHeaderChckbox.Rows(j).Cells(2).FindControl("HyperLink1"), HyperLink)
                    If i = 0 And j = 0 Then
                        empIDList = "'" & controls.Text.Replace(",", "") & "'"
                    Else
                        empIDList = empIDList & "," & "'" & controls.Text.Replace(",", "") & "'"
                    End If
                Next
            Next
            If empIDList = "" Then
                empIDList = "''"
            End If
            scripts = scripts.Replace("@empid", empIDList)
            Dim strDataSet As New DataSet
            strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, scripts)
            Dim dataTables As DataTable = strDataSet.Tables(0)

            filename = radOffice.SelectedValue & "payroll"

            If Process.ExportExcel(dataTables, filename) = False Then
                Process.loadalert(divalert, msgalert, Session("exception"), "danger")
            Else
                Process.loadalert(divalert, msgalert, "File saved as " & filename & ".xls", "success")
            End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Protected Sub chkActive_CheckedChanged(sender As Object, e As EventArgs) Handles chkActive.CheckedChanged
        Try
            Session("salaryactive") = chkActive.Checked
            Session("salarysearch") = ""
            Session("salaryindex") = "0"
            LoadLoans()
        Catch ex As Exception

        End Try
    End Sub



    Protected Sub chkIsTransposed_CheckedChanged(sender As Object, e As EventArgs) Handles chkIsTransposed.CheckedChanged
        Try
            If chkIsTransposed.Checked = True Then
                btnUploadFile.Attributes.Add("title", "EMPID, Salary Item 1,Salary Item 2,Salary Item 3,Salary Item N....")
            Else
                btnUploadFile.Attributes.Add("title", "EMPID, Salary Item , Amount")
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Private Sub radOffice_SelectedIndexChanged1(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles radOffice.SelectedIndexChanged
        Try
            Session("salarycompany") = radOffice.SelectedValue
            Session("salarysearch") = ""
            Session("salaryindex") = "0"
            LoadLoans()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
End Class