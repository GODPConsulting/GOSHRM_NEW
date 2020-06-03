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
Public Class ExpenseMasterPage
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "EMPNONPAYROLL"
    Dim Pages As String = "Employee Non Payroll Expense"

   
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
    Private Function DataData(LoadType As String) As DataSet
        Dim StGrade As String = ""
        Dim active As Integer = 0

        If chkActive.Checked = True Then
            active = 1
        End If

        Dim SP As String = ""
        Dim SP1 As String = ""
        Dim SP2 As String = ""
        Dim SP3 As String = ""

        Dim strDataSet As New DataSet
        'strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, script)
        If Session("LoadType") = "All" Then
            If chkActive.Checked = True Then
                strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Finance_Non_Salary_Active_Get_All", radOffice.SelectedValue)
            Else
                strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Finance_Non_Salary_InActive_Get_All", radOffice.SelectedValue)
            End If
        Else
            If chkActive.Checked = True Then
                strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Finance_Non_Salary_Active_Search", radOffice.SelectedValue, txtsearch.Text)

            Else
                strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Finance_Non_Salary_InActive_Search", radOffice.SelectedValue, txtsearch.Text)
            End If
        End If
        lblView.Text = radOffice.SelectedValue & ": " & txtsearch.Text & " Employee Salary Master (" & strDataSet.Tables(0).Rows.Count.ToString & ")"
        Return strDataSet
    End Function
    'Private Function DataData(LoadType As String) As DataSet
    '    Dim StGrade As String = ""
    '    'Dim collection As IList(Of RadComboBoxItem) = radGrade.CheckedItems
    '    'If (collection.Count <> 0) Then
    '    '    For Each item As RadComboBoxItem In collection
    '    '        StGrade = StGrade & item.Text & "','"
    '    '    Next
    '    'End If
    '    'StGrade = StGrade.Substring(0, StGrade.Length - 3)
    '    Dim active As Integer = 0

    '    If chkActive.Checked = True Then
    '        active = 1
    '    End If


    '    Dim SP As String = ""
    '    Dim SP1 As String = ""
    '    Dim SP2 As String = ""
    '    Dim SP3 As String

    '    If chkActive.Checked = True Then
    '        SP = "Select ROW_NUMBER() OVER(ORDER BY a.EmpID) AS [Rows],b.id,a.EmpID [Employee No], a.Name,a.Grade,a.JobTitle [Job Title],b.SalaryType [Salary Type],dbo.My_Company(a.Office) Company, a.Office,a.Location,a.[State], a.jobstatus [Job Status],dbo.dd_mm_yyyy(DateGenerated) [Created On],case when b.EmpID is null then 'Salary Not Generated' Else 'Salary Generated' End [Is Salary Generated],(Select sum(x.amount) [PBT] from Finance_Non_Payslip x inner join Finance_Non_Payroll_Items ii on x.SalaryItem = ii.item  where b.id = x.salaryprimaryid and ii.active = 1 group by x.salaryprimaryid) PBT from "
    '        SP1 = " dbo.Employees_All a left outer join (SELECT Distinct d.id, d.EmpID, d.DateGenerated, d.SalaryType,d.grade FROM dbo.Finance_Salary_Primary d where d.active = 1) b on b.empid = a.empid and b.grade = a.grade"
    '        SP2 = " Where dbo.My_Company(a.Office) = '@Company'"
    '        SP3 = " and (b.empid like '%@text%' or a.name like '%@text%' or a.jobtitle like '%@text%' or a.Office like '%@text%' or a.location like '%@text%' or a.grade like '%@text%' or a.jobstatus like '%@text%' or a.[state] like '%@text%' )"

    '    Else
    '        SP = "SELECT ROW_NUMBER() OVER(ORDER BY d.EmpID,d.DateGenerated desc) AS [Rows],d.id, d.EmpID [Employee No],a.Name,d.grade, work.JobTitle,dbo.My_Company(work.Office) Company,work.Office,work.Location,loc.[state],work.JobType [Job Status], d.DateGenerated [Created On], d.SalaryType,d.active, "
    '        SP1 = " '' [Is Salary Generated], (Select sum(x.amount) [PBT] from Finance_Non_Payslip x where d.id = x.salaryprimaryid group by x.salaryprimaryid) PBT FROM dbo.Finance_Salary_Primary d inner join dbo.Employees_All a"
    '        SP2 = " on d.EmpID = a.EmpID inner join Emp_Work_History work on d.EmpID = work.EmpID and d.grade = work.GradeLevel inner join Location loc on loc.name = work.Location where d.active = 0 and dbo.My_Company(a.Office) = '@Company'"
    '        SP3 = "  and (d.empid like '%@text%' or a.name like '%@text%' or work.jobtitle like '%@text%' or work.Office like '%@text%' or work.location like '%@text%' or a.grade like '%@text%' or work.JobType like '%@text%' or loc.[state] like '%@text%' )"
    '    End If

    '    Dim script As String = ""

    '    If LoadType = "All" Then
    '        script = SP & SP1 & SP2
    '        script = script.Replace("@Grade", StGrade).Replace("@active", active).Replace("@Company", radOffice.SelectedValue)
    '    Else
    '        script = SP & SP1 & SP2 & SP3
    '        script = script.Replace("@Grade", StGrade).Replace("@text", txtsearch.Text.Trim).Replace("@active", active).Replace("@Company", radOffice.SelectedValue)
    '    End If

    '    Dim strDataSet As New DataSet
    '    strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, script)
    '    lblView.Text = txtsearch.Text & " Employee Non Payroll Expense (" & strDataSet.Tables(0).Rows.Count.ToString & ")"
    '    Return strDataSet
    'End Function


    Private Sub LoadLoans(LoadType As String, pageindex As Integer)
        Try
            Dim strDataSet As New DataSet
            strDataSet = DataData(LoadType) 'SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, script)
            dtTable = strDataSet.Tables(0)
            GridVwHeaderChckbox.PageIndex = pageindex
            GridVwHeaderChckbox.DataSource = dtTable 'Process.SearchDataP3("Finance_Salary_Get_All", radLocation.SelectedText, radLocation.SelectedValue, StGrade)

            GridVwHeaderChckbox.AllowSorting = True
            GridVwHeaderChckbox.AllowPaging = True
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try


            If Not Me.IsPostBack Then

                If Process.AuthenAction(Session("role"), AuthenCode, "Read") = False Then
                    Response.Write("You don't have privilege to perform this action")
                    ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
                    Exit Sub
                End If

                Dim ismulti As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select isnull(ismulticompany,'No')  from general_info")
                If ismulti.ToLower = "no" Then
                    Process.LoadRadComboTextAndValueP2(radOffice, "Company_Structure_Get_ByLevel", "1", Session("Access"), "name", "name", False)
                Else
                    Process.LoadRadComboTextAndValueP2(radOffice, "Company_Structure_Get_ByLevel", "2", Session("Access"), "name", "name", False)
                End If
                If Session("company") Is Nothing Then
                    Session("company") = Session("Organisation")
                End If
                Process.AssignRadComboValue(radOffice, Session("company"))

                chkActive.Checked = 1

                Session("pageIndex1") = 0
                Session("LoadType") = "All"
                LoadLoans(Session("LoadType"), Session("pageIndex1"))

            End If
        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    Protected Sub SortRecords(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Try
            Dim table As New DataTable

            Dim strDataSet As New DataSet
            strDataSet = DataData(Session("LoadType")) 'SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, script)
            table = strDataSet.Tables(0)

            Dim sortExpression As String = e.SortExpression
            Dim direction As String = String.Empty
            If SortDirection = SortDirection.Ascending Then
                SortDirection = SortDirection.Descending
                direction = " DESC"
            Else
                SortDirection = SortDirection.Ascending
                direction = " ASC"
            End If

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


    Protected Sub GridVwHeaderChckbox_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridVwHeaderChckbox.PageIndexChanging
        Try
            Dim table As New DataTable

            'Dim StGrade As String = ""
            'Dim collection As IList(Of RadComboBoxItem) = radGrade.CheckedItems
            'If (collection.Count <> 0) Then
            '    For Each item As RadComboBoxItem In collection
            '        StGrade = StGrade & item.Text & "','"
            '    Next
            'End If
            'StGrade = StGrade.Substring(0, StGrade.Length - 3)
            Dim strDataSet As New DataSet
            strDataSet = DataData(Session("LoadType")) 'SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, script)
            table = strDataSet.Tables(0)

            GridVwHeaderChckbox.PageIndex = e.NewPageIndex
            Session("pageIndex1") = e.NewPageIndex
            GridVwHeaderChckbox.DataSource = table
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub OnRowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GridVwHeaderChckbox, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub


    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles btnFind.Click
        Try

            'If Not Me.IsPostBack Then
            If txtsearch.Text.Trim = "" Then
                Session("LoadType") = "All"
                'LoadLoans("All")
            Else
                Session("LoadType") = "Find"
                'LoadLoans("Find")
            End If
            Session("pageIndex1") = 0
            LoadLoans(Session("LoadType"), Session("pageIndex1"))
            'End If
        Catch ex As Exception
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    'Protected Sub Delete(sender As Object, e As EventArgs) Handles btnDelete.Click
    '    Try
    '        lblstatus.Text = ""
    '        If Process.AuthenAction(Session("role"), AuthenCode, "Delete") = False Then
    '            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
    '            Exit Sub
    '        End If
    '        Dim confirmValue As String = Request.Form("confirm_value")
    '        If confirmValue = "Yes" Then
    '            Dim atLeastOneRowDeleted As Boolean = False
    '            ' Iterate through the Products.Rows property
    '            For Each row As GridViewRow In GridVwHeaderChckbox.Rows
    '                ' Access the CheckBox
    '                Dim cb As CheckBox = row.FindControl("chkEmp")
    '                If cb IsNot Nothing AndAlso cb.Checked Then
    '                    ' Delete row! (Well, not really...)
    '                    atLeastOneRowDeleted = True
    '                    ' First, get the ProductID for the selected row
    '                    Dim ID As String = _
    '                        Convert.ToString(GridVwHeaderChckbox.DataKeys(row.RowIndex).Value)
    '                    ' "Delete" the row
    '                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Finance_Salary_Primary_Delete", ID)
    '                End If
    '            Next
    '            LoadLoans(Session("LoadType"), Session("pageIndex1"))
    '        Else
    '            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Delete has been cancelled" + "')", True)
    '        End If
    '    Catch ex As Exception
    '        ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
    '    End Try
    'End Sub

    'Protected Sub btnApply_Click(sender As Object, e As EventArgs) Handles btnApply.Click
    '    Try
    '        lblstatus.Text = ""
    '        If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
    '            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
    '            Exit Sub
    '        End If
    '        Process.loadtype = "Add"
    '        Response.Write("<script language='javascript'> { popup = window.open(""SalaryUpdate.aspx"" , ""Stone Details"", ""height=800,width=800,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no"");  popup.focus(); }</script>")
    '    Catch ex As Exception
    '        ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
    '    End Try
    'End Sub



    Protected Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
        Try
            Dim confirmGen As String = Request.Form("confirm_ref")
            If confirmGen = "Yes" Then
                If Process.ResetEmployeeExpenseItem(radOffice.SelectedValue) = True Then
                    ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "All Items successfully refreshed to " & radOffice.SelectedValue & " Employees PaySlips" + "')", True)
                Else
                    ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & Process.strExp + "')", True)
                End If
                LoadLoans(Session("LoadType"), Session("pageIndex1"))
            Else
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Item refresh per Employee cancelled" + "')", True)
            End If
            'Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri)
        Catch ex As Exception
            response.write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try

    End Sub

    Protected Sub btnImport0_Click(sender As Object, e As EventArgs) Handles btnRegenerate.Click
        Try

            Dim confirmGen As String = Request.Form("confirm_gen")
            If confirmGen = "Yes" Then
                If Process.ResetEmployeeSalaryItem(radOffice.SelectedValue) = True Then
                    ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "New Items successfully included to Employees PaySlips" + "')", True)
                Else
                    ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & Process.strExp + "')", True)
                End If
                LoadLoans(Session("LoadType"), Session("pageIndex1"))
            Else
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Item refresh per Employee cancelled" + "')", True)
            End If
            'Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri)
        Catch ex As Exception
            response.write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                Response.Write("You don't have privilege to perform this action")
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
                Exit Sub
            End If
            'lblstatus.Text = "Uploading, please wait ..."
            If FileUpload1.HasFile AndAlso Not FileUpload1.PostedFile Is Nothing Then
                'To create a PostedFile
                Dim csvPath As String = Server.MapPath(Process.FileURL) + Path.GetFileName(FileUpload1.PostedFile.FileName)
                If File.Exists(csvPath) = True Then
                    File.Delete(csvPath)
                End If
                FileUpload1.SaveAs(csvPath)
                If chkIsTransposed.Checked = True Then
                    If Process.ImportTransposed(csvPath, "Finance_Non_Salary_Primary_Update", Pages) = True Then

                        Response.Write("Uploaded " & Session("uploadcnt") & " record(s)")
                        ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Uploaded " & Session("uploadcnt") & " record(s)" + "')", True)
                    Else
                        Response.Write(Process.strExp)
                        ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & Process.strExp + "')", True)
                    End If
                Else
                    If Process.ImportWithUsers(csvPath, "Finance_Non_Salary_Primary_Update", Pages) = True Then
                        Response.Write("Uploaded " & Session("uploadcnt") & " record(s)")
                        ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Uploaded " & Session("uploadcnt") & " record(s)" + "')", True)
                    Else
                        Response.Write(Process.strExp)
                        ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & Process.strExp + "')", True)
                    End If
                End If
                Process.GetAuditTrailInsertandUpdate("", "Uploaded " & Session("uploadcnt") & " record(s) from " & csvPath, "File Upload", Pages)
            Else
                Response.Write("No files selected to upload")
                FileUpload1.Focus()
                Exit Sub
            End If
            LoadLoans(Session("LoadType"), Session("pageIndex1"))
            'Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri)
            'Response.Write("<script language='javascript'> { self.close() }</script>")
        Catch ex As Exception
            response.write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    Protected Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            Process.Export(GridVwHeaderChckbox, "SalaryMaster", 1, 2)
        Catch ex As Exception
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    Protected Sub chkActive_CheckedChanged(sender As Object, e As EventArgs) Handles chkActive.CheckedChanged
        'Try
        '    Button1_Click(sender, e)
        'Catch ex As Exception

        'End Try
    End Sub



    Protected Sub chkActive0_CheckedChanged(sender As Object, e As EventArgs) Handles chkIsTransposed.CheckedChanged
        Try
            If chkIsTransposed.Checked = True Then
                btnUpload.ToolTip = "EMPID, Item 1,Item 2,Item 3,...."
            Else
                btnUpload.ToolTip = "EMPID, Item , Amount"
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub radOffice_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles radOffice.SelectedIndexChanged
        Try
            Session("company") = radOffice.SelectedValue
            Session("pageIndex1") = 0
            Session("LoadType") = "All"
            LoadLoans(Session("LoadType"), Session("pageIndex1"))
        Catch ex As Exception

        End Try
    End Sub
End Class