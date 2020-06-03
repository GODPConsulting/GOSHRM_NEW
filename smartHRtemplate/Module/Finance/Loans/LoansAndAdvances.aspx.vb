Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Public Class LoansAndAdvances
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "EMPLOAN"
    Private Sub LoadChart()
        Try
            gridLoanChart.DataSource = Process.SearchData("Emp_Loan_Chart", Session("UserEmpID"))
            gridLoanChart.AllowSorting = False
            gridLoanChart.AllowPaging = False
            gridLoanChart.DataBind()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

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
    Private Function MyLoans() As DataTable
        Dim DataTables As New DataTable
        search.Value = Session("myloansearch")
        If search.Value.Trim = "" Then
            DataTables = Process.SearchDataP4("Emp_Loan_get_all", Session("UserEmpID"), radStatus.SelectedItem.Text, Process.DDMONYYYY(adatefrom.Value), Process.DDMONYYYY(adateto.Value))
        Else
            DataTables = Process.SearchDataP5("Emp_Loan_search", Session("UserEmpID"), radStatus.SelectedItem.Text, Process.DDMONYYYY(adatefrom.Value), Process.DDMONYYYY(adateto.Value), search.Value.Trim)
        End If
        pagetitle.InnerText = "My Loans: " & radStatus.SelectedItem.Text & ", as at " & Process.DDMONYYYY(adatefrom.Value) & " to " & Process.DDMONYYYY(adateto.Value)
        Return DataTables
    End Function

    Private Sub LoadLoans()
        Try
            GridVwHeaderChckbox.PageIndex = CInt(Session("myloanindex"))
            GridVwHeaderChckbox.DataSource = MyLoans()
            GridVwHeaderChckbox.AllowSorting = True
            GridVwHeaderChckbox.AllowPaging = True
            GridVwHeaderChckbox.DataBind()

            LoadChart()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Read") = False Then
                Process.loadalert(divalert, msgalert, Process.privilegemsg, "warning")                
                Exit Sub
            End If

            If Not Me.IsPostBack Then
                radStatus.Items.Clear()
                radStatus.Items.Add("Approved")
                radStatus.Items.Add("Pending")
                radStatus.Items.Add("Cancelled")
                radStatus.Items.Add("Rejected")


                If Session("myloandatefrom") Is Nothing Then
                    Session("myloandatefrom") = Date.Now.AddDays(1 - Date.Now.Day).AddMonths(1 - Date.Now.Month)
                End If

                If Session("myloandateto") Is Nothing Then
                    Session("myloandateto") = Date.Now.AddMonths(12 - Date.Now.Month).AddDays(31 - Date.Now.Day)
                End If

                If Session("myloansearch") Is Nothing Then
                    Session("myloansearch") = ""
                End If

                If Session("myloanindex") Is Nothing Then
                    Session("myloanindex") = "0"
                End If

                If Session("myloanstatus") Is Nothing Then
                    Session("myloanstatus") = "Pending"
                End If

                'Loans and Advances
                adatefrom.Value = Session("myloandatefrom")
                adateto.Value = Session("myloandateto")
                Process.AssignRadComboValue(radStatus, Session("myloanstatus"))
                LoadLoans()
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Protected Sub SortRecords(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Try
            Dim sortExpression As String = e.SortExpression
            Session("myloansort") = sortExpression
            Dim direction As String = String.Empty
            If SortsDirection = SortDirection.Ascending Then
                SortsDirection = SortDirection.Descending
                direction = " DESC"
            Else
                SortsDirection = SortDirection.Ascending
                direction = " ASC"
            End If
            Dim table As New DataTable
            table = MyLoans()


            table.DefaultView.Sort = sortExpression & direction
            GridVwHeaderChckbox.PageIndex = CInt(Session("myloanindex"))
            GridVwHeaderChckbox.DataSource = table
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
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
            Session("myloanindex") = e.NewPageIndex
            LoadLoans()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
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
            Session("myloansearch") = search.Value.Trim
            LoadLoans()

            'End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
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
                    ' Access the CheckBox
                    Dim cb As CheckBox = row.FindControl("chkEmp")
                    If cb IsNot Nothing AndAlso cb.Checked Then
                        count = count + 1
                        ' Delete row! (Well, not really...)
                        atLeastOneRowDeleted = True
                        ' First, get the ProductID for the selected row
                        Dim ID As String = _
                            Convert.ToString(GridVwHeaderChckbox.DataKeys(row.RowIndex).Value)
                        ' "Delete" the row
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Loan_Staff_Delete", ID)
                    End If
                Next
                Process.loadalert(divalert, msgalert, count.ToString & " records successfully deleted", "success")
                LoadLoans()
           
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub


    Protected Sub gridLoanChart_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridLoanChart.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim applycaption As String = e.Row.Cells(6).Text


            For Each cell As TableCell In e.Row.Cells
                If applycaption.ToUpper = "APPLY" Then
                    e.Row.Cells(6).Enabled = True
                Else
                    e.Row.Cells(6).Enabled = False
                End If
 
            Next
        End If
    End Sub

    Protected Sub chkIsTransposed_CheckedChanged(sender As Object, e As EventArgs) Handles chkDisable.CheckedChanged
        Try
            If chkDisable.Checked = True Then
                Dim ss As String = "Successfully excluded from Guarantor List to other Employees"
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Finance_Loan_Guarantor_Exclude_Delete", Session("UserEmpID"))
                Process.loadalert(divalert, msgalert, ss, "success")
            Else
                Dim ss As String = "Successfully included into Guarantor List for other Employees"
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Finance_Loan_Guarantor_Exclude_Update", Session("UserEmpID"))
                Process.loadalert(divalert, msgalert, ss, "success")
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class