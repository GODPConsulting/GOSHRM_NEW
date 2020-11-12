Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Imports Telerik.Web.UI
Imports System.Web.UI
Imports System.Threading


Public Class Gratuity
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "GRATUITY"
    Dim Pages As String = "Gratuity"
    Dim PhotoURL As String = ConfigurationManager.AppSettings("PhotoURL")
    Dim FileURL As String = ConfigurationManager.AppSettings("FileURL")
    Dim sampleCSV As String = ConfigurationManager.AppSettings("sampleCSV")
    Public Shared Separator() As Char = {"."c}
    Private Function LoadEmployeeGrid(LoadType As String) As DataTable
        Session("company") = cboCompany.SelectedValue
        Dim datatables As New DataTable
        If LoadType = "All" Then
            datatables = Process.SearchDataP3("Finance_Gratuity_Get_All", Session("company"), cboMonth.SelectedValue, cboYear.SelectedValue)
        ElseIf LoadType = "Find" Then
            datatables = Process.SearchDataP4("Finance_Gratuity_Search", Session("company"), cboMonth.SelectedValue, cboYear.SelectedValue, txtsearch.Value.Trim)
        End If
        Dim gratuity As Double = 0
        Dim YTDgratuity As Double = 0

        Dim strgrauity As New DataSet
        strgrauity = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Company_Finance_Gratuity", Session("company"), cboYear.SelectedValue, cboMonth.SelectedValue)
        If strgrauity.Tables(0).Rows.Count > 0 Then
            gratuity = strgrauity.Tables(0).Rows(0).Item("amount").ToString
        End If

        Dim strytdgrauity As New DataSet
        strytdgrauity = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Company_Finance_Gratuity_YTD", Session("company"), cboYear.SelectedValue, cboMonth.SelectedValue)
        If strytdgrauity.Tables(0).Rows.Count > 0 Then
            YTDgratuity = strytdgrauity.Tables(0).Rows(0).Item("amount").ToString
        End If
        lblytdgratuity.Text = "Year to Date Gratuity: " & FormatNumber(YTDgratuity, 2)
        lblgratuity.Text = Process.DDMONYYYY(Process.LastDay(cboYear.SelectedValue, cboMonth.SelectedValue)) & " Gratuity: " & FormatNumber(gratuity, 2)

        pagetitle.InnerText = Session("company") & ": " & txtsearch.Value & " " & cboMonth.SelectedItem.Text & ", " & cboYear.SelectedValue & " Gratuity (" & datatables.Rows.Count & ")"

        Return datatables
    End Function

    Private Sub LoadGrid(LoadType As String, pageno As Integer)
        Try

            GridVwHeaderChckbox.PageIndex = pageno
            GridVwHeaderChckbox.DataSource = LoadEmployeeGrid(LoadType)
            GridVwHeaderChckbox.AllowSorting = True
            ' GridVwHeaderChckbox.AllowPaging = True

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
                cboYear.Items.Clear()
                cboMonth.Items.Clear()
                For z As Integer = 2015 To 2050
                    Dim itemTemp As New RadComboBoxItem()
                    itemTemp.Text = z.ToString
                    itemTemp.Value = z.ToString
                    cboYear.Items.Add(itemTemp)
                    itemTemp.DataBind()
                    If (z = Now.Year) Then
                        Exit For
                    End If
                Next

                Dim strmonth As New DataSet
                strmonth = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "select * from dbo.Calendar('finance',2016) order by sno")
                If strmonth.Tables(0).Rows.Count > 0 Then
                    For z As Integer = 0 To strmonth.Tables(0).Rows.Count - 1
                        Dim itemTemp As New RadComboBoxItem()
                        itemTemp.Text = strmonth.Tables(0).Rows(z).Item("calmonths").ToString
                        itemTemp.Value = strmonth.Tables(0).Rows(z).Item("id").ToString
                        cboMonth.Items.Add(itemTemp)
                        itemTemp.DataBind()
                    Next
                End If

                Process.AssignRadComboValue(cboMonth, Date.Now.Month)
                Process.AssignRadComboValue(cboYear, Date.Now.Year)
                Dim ismulti As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select isnull(ismulticompany,'No')  from general_info")
                If ismulti.ToLower = "no" Then
                    Process.LoadRadComboTextAndValueP2(cboCompany, "Company_Structure_Get_ByLevel", "1", Session("Access"), "name", "name", False)
                Else
                    Process.LoadRadComboTextAndValueP2(cboCompany, "Company_Structure_Get_ByLevel", "2", Session("Access"), "name", "name", False)
                End If
                Process.AssignRadComboValue(cboCompany, Session("company"))

                Session("pageIndex1") = 0
                Session("LoadType") = "All"
                LoadGrid(Session("LoadType"), Session("pageIndex1"))
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
            Dim table As New DataTable
            table = LoadEmployeeGrid(Session("LoadType"))
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
            GridVwHeaderChckbox.PageIndex = e.NewPageIndex
            Session("pageIndex1") = e.NewPageIndex
            GridVwHeaderChckbox.DataSource = LoadEmployeeGrid(Session("LoadType"))
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub OnRowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridVwHeaderChckbox.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GridVwHeaderChckbox, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs)
        Try
            'If Not Me.IsPostBack Then

            If txtsearch.Value.Trim Is Nothing Then
                Session("LoadType") = "All"
            Else
                Session("LoadType") = "Find"
            End If
            Session("pageIndex1") = 0
            LoadGrid(Session("LoadType"), Session("pageIndex1"))
            'End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "danger")
                Exit Sub
            End If

            Process.loadtype = "Add"
            Response.Redirect("gratuityupdate")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
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
                For Each row As GridViewRow In GridVwHeaderChckbox.Rows
                    ' Access the CheckBox
                    Dim cb As CheckBox = row.FindControl("chkEmp")
                    If cb IsNot Nothing AndAlso cb.Checked Then
                        ' Delete row! (Well, not really...)
                        atLeastOneRowDeleted = True
                        ' First, get the ProductID for the selected row
                        Dim ID As String = Convert.ToString(GridVwHeaderChckbox.DataKeys(row.RowIndex).Value)
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Finance_Gratuity_delete", ID)
                    End If
                Next

                LoadGrid(Session("LoadType"), Session("pageIndex1"))
            Else
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Delete has been cancelled" + "')", True)
            End If
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
                Process.loadalert(divalert, msgalert, "Uploading, please wait ...", "success")
                'To create a PostedFile
                Dim csvPath As String = Server.MapPath(FileURL) + Path.GetFileName(FileUpload1.PostedFile.FileName)
                If File.Exists(csvPath) = True Then
                    File.Delete(csvPath)
                End If
                FileUpload1.PostedFile.SaveAs(csvPath)
                Dim proc As String = ""
                proc = "Finance_Gratuity_Upload"

                If Process.ImportWithUsers(csvPath, proc, Pages) = True Then
                    Process.loadalert(divalert, msgalert, "Uploaded " & Session("uploadcnt") & " record(s)", "success")
                Else
                    Response.Write(Process.strExp)
                    Process.loadalert(divalert, msgalert, Process.strExp, "danger")
                End If
                Process.GetAuditTrailInsertandUpdate("", "Uploaded " & Session("uploadcnt") & " record(s) from " & FileUpload1.PostedFile.FileName, "File Upload", Pages)
            Else
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Choose file to upload" + "')", True)
                FileUpload1.Focus()
            End If

        Catch ex As Exception
             Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnExport_Click(sender As Object, e As EventArgs)
        Try

            Dim filename As String = ""
            Dim empIDList As String = ""

            Dim TitleHeader As String()
            Dim TitleData As String()

            Dim title As String = cboCompany.SelectedValue & cboMonth.SelectedItem.Text & cboYear.SelectedItem.Text & "_Gratuity"
            Dim scripts As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Scripts_get", "gratuity")
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
            scripts = scripts.Replace("@month", cboMonth.SelectedValue)
            scripts = scripts.Replace("@year", cboYear.SelectedValue)
            Dim strDataSet As New DataSet
            strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, scripts)
            Dim dataTables As DataTable = strDataSet.Tables(0)
            'Dim Columns(dataTables.Columns.Count - 1) As String
            'Dim ColumnHeaders(dataTables.Columns.Count - 1) As String

            'For i As Integer = 0 To dataTables.Columns.Count - 1
            '    ColumnHeaders(i) = dataTables.Columns(i).ColumnName
            '    Columns(i) = dataTables.Columns(i).ColumnName
            'Next
            filename = cboCompany.SelectedValue & cboMonth.SelectedItem.Text & cboYear.SelectedItem.Text & "_Gratuity"
            If Process.ExportExcel(dataTables, filename) = False Then
                Response.Write(Process.strExp)
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & Process.strExp + "')", True)
            Else
                Response.Write("File saved as " & filename & ".xls")
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "File saved as " & filename & ".xls" + "')", True)
            End If

            'Process.ExportDataSet(TitleHeader, TitleData, dataTables, ColumnHeaders, Columns, filename)
            'Response.Write("File saved as " & filename & ".csv")
            'ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "File saved as " & filename & ".csv" + "')", True)


        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


    Protected Sub cboCompany_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboCompany.SelectedIndexChanged
        Try

            txtsearch.Value = ""
            Session("LoadType") = "All"
            LoadGrid(Session("LoadType"), 0)
        Catch ex As Exception
            response.write(ex.Message)
        End Try
    End Sub

    Protected Sub cboYear_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboYear.SelectedIndexChanged
        Try

            txtsearch.Value = ""
            Session("LoadType") = "All"
            LoadGrid(Session("LoadType"), 0)
        Catch ex As Exception
            response.write(ex.Message)
        End Try
    End Sub

    Private Sub cboMonth_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboMonth.SelectedIndexChanged
        Try

            txtsearch.Value = ""
            Session("LoadType") = "All"
            LoadGrid(Session("LoadType"), 0)
        Catch ex As Exception
            response.write(ex.Message)
        End Try
    End Sub
End Class