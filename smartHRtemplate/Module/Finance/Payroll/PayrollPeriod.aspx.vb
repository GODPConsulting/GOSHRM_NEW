Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Telerik.Web.UI
Public Class PayrollPeriod
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "PAYROLLPERIOD"

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
    Private Function LoadDataTables() As DataTable
        Dim sData As New DataTable
        search.Value = Session("payperiodsearch")
        If search.Value.Trim = "" Then
            sData = Process.SearchDataP2("Finance_Payslip_Primary_Get_All", cboyear.SelectedValue, cbocompany.SelectedValue)
        ElseIf Session("LoadType") = "Find" Then
            sData = Process.SearchDataP3("Finance_Payslip_Primary_Search", cboyear.SelectedValue, cbocompany.SelectedValue, search.Value)
        End If
        pagetitle.InnerText = cbocompany.SelectedValue & ": " & cboyear.SelectedValue & " Payroll"
        Return sData
    End Function

    Private Sub LoadData()
        Try
            ''SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, script)
            'If Session("PayrollYear") IsNot Nothing Then
            '    Process.AssignRadComboValue(radYear, Session("PayrollYear"))
            'End If


            dtTable = LoadDataTables()
            GridVwHeaderChckbox.PageIndex = CInt(Session("payperiodindex"))
            GridVwHeaderChckbox.DataSource = dtTable '
            GridVwHeaderChckbox.AllowSorting = True
            GridVwHeaderChckbox.AllowPaging = True
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.message, "danger")
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Read") = False Then
                content.Style.Add("display", "none")
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform view this page", "info")
                Exit Sub
            End If
            Session("processid") = 0
            If Not Me.IsPostBack Then
                Dim ismulti As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select isnull(ismulticompany,'No')  from general_info")
                If ismulti.ToLower = "no" Then
                    Process.LoadRadComboTextAndValueP2(cbocompany, "Company_Structure_Get_ByLevel", "1", Session("Access"), "name", "name", False)
                    cbocompany.Visible = False
                Else
                    Process.LoadRadComboTextAndValueP2(cbocompany, "Company_Structure_Get_ByLevel", "2", Session("Access"), "name", "name", False)
                End If



                If Session("payperiodcompany") Is Nothing Then
                    Session("payperiodcompany") = Session("Organisation")
                End If
                Process.AssignRadComboValue(cbocompany, Session("payperiodcompany"))

                If cbocompany.SelectedValue IsNot Nothing Then
                    Process.LoadRadComboTextAndValueP1(cboyear, "Finance_Payslip_Years", cbocompany.SelectedValue, "year", "year", False)
                End If

                If Session("payperiodyear") Is Nothing Then
                    Session("payperiodyear") = Date.Now.Year
                End If

                If cboyear.Items.Count < 1 Then
                    Dim itemTemp As New RadComboBoxItem()
                    itemTemp.Text = Session("payperiodyear")
                    itemTemp.Value = Session("payperiodyear")
                    cboyear.Items.Add(itemTemp)
                    itemTemp.DataBind()
                End If
                Process.AssignRadComboValue(cboyear, Session("payperiodyear"))

                If Session("payperiodsearch") Is Nothing Then
                    Session("payperiodsearch") = ""
                End If

                If Session("payperiodindex") Is Nothing Then
                    Session("payperiodindex") = "0"
                End If

                LoadData()

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.message, "danger")
        End Try
    End Sub

    Protected Sub SortRecords(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Try
            Dim table As New DataTable
            'SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, script)
            table = LoadDataTables()

            Dim sortExpression As String = e.SortExpression
            Session("payperiodsort") = sortExpression
            Dim direction As String = String.Empty
            If SortsDirection = SortDirection.Ascending Then
                SortsDirection = SortDirection.Descending
                direction = " DESC"
            Else
                SortsDirection = SortDirection.Ascending
                direction = " ASC"
            End If

            table.DefaultView.Sort = sortExpression & direction
            GridVwHeaderChckbox.PageIndex = CInt(Session("payperiodindex"))
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

    Protected Sub OnRowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GridVwHeaderChckbox, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub




    Protected Sub Delete(sender As Object, e As EventArgs) Handles btDelete.Click
        Try
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
                    Dim cb As CheckBox = row.FindControl("chkEmp")
                    If cb IsNot Nothing AndAlso cb.Checked Then
                        count = count + 1
                        ' Delete row! (Well, not really...)
                        atLeastOneRowDeleted = True
                        ' First, get the ProductID for the selected row
                        Dim ID As String = Convert.ToString(GridVwHeaderChckbox.DataKeys(row.RowIndex).Value)
                        'Dim startdate As Date = CDate(GridVwHeaderChckbox.DataKeys(row.RowIndex).Values(1))
                        'Dim enddate As Date = CDate(GridVwHeaderChckbox.DataKeys(row.RowIndex).Values(2))
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Finance_Payslip_Primary_Delete", ID)
                    End If
                Next
                Process.loadalert(divalert, msgalert, count.ToString & " records successfully deleted", "success")
                LoadData()            
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnImport_Click(sender As Object, e As EventArgs)
        Try
            Session("View") = "multiple"
            Response.Write("<script language='javascript'> { popup = window.open(""SalaryPayslipGenerate.aspx"" , ""Stone Details"", ""height=500,width=600,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no"");  popup.focus(); }</script>")

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


    Protected Sub radYear_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboyear.SelectedIndexChanged
        Try
            Session("payperiodyear") = cboyear.SelectedValue
            LoadData()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnFind_Click(sender As Object, e As EventArgs)
        Try
            Session("payperiodsearch") = search.Value.Trim
            LoadData()
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
                Exit Sub
            End If
            Process.loadtype = "Add"
            Response.Redirect("SalaryPayslipGenerate")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridVwHeaderChckbox_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridVwHeaderChckbox.PageIndexChanging
        Try
            Session("payperiodindex") = e.NewPageIndex
            GridVwHeaderChckbox.PageIndex = e.NewPageIndex
            GridVwHeaderChckbox.DataSource = LoadDataTables()
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub cbocompany_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cbocompany.SelectedIndexChanged
        Try
            If cbocompany.SelectedValue IsNot Nothing Then
                Session("payperiodcompany") = cbocompany.SelectedValue
                Process.LoadRadComboTextAndValueP1(cboyear, "Finance_Payslip_Years", cbocompany.SelectedValue, "year", "year", False)
            End If
            LoadData()
        Catch ex As Exception

        End Try
    End Sub
End Class