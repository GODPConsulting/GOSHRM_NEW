Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports Microsoft.Reporting.WebForms
Imports System.IO
Public Class EmployeeLeaves
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "TIMLEAVE"
    Dim emailFile As String = ConfigurationManager.AppSettings("EmailURL")
    Dim PayrollFile As String = ""
   
    Private Function LoadGroupDataTable(loadtype As String) As DataTable
        Dim datatables As New DataTable
        If loadtype = "All" Then
            datatables = Process.SearchDataP5("Employee_Leavelist_Summary_Get_All", "", dateGroupFrom.SelectedDate, dateGroupTo.SelectedDate, cboCompany.SelectedValue, loadtype)
        ElseIf loadtype = "Find" Then
            datatables = Process.SearchDataP6("Employee_Leavelist_Summary_Search", "", dateGroupFrom.SelectedDate, dateGroupTo.SelectedDate, cboCompany.SelectedValue, "", txtSearchGroup.Value)
        End If
        pagetitle.InnerText = cboCompany.SelectedValue & ": " & txtSearchGroup.Value & " Employee Leaves (" & datatables.Rows.Count.ToString & ")"

        Return datatables
    End Function
    Private Sub LoadGroupLeaves(LoadType As String, pageindex As Integer)
        Try
            gridGroupView.PageIndex = pageindex
            gridGroupView.DataSource = LoadGroupDataTable(LoadType)
            gridGroupView.AllowSorting = True
            gridGroupView.AllowPaging = True
            gridGroupView.DataBind()

            pagetitle.InnerText = txtSearchGroup.Value & " LEAVE: " & Process.DDMONYYYY(dateGroupFrom.SelectedDate) & " : " & Process.DDMONYYYY(dateGroupTo.SelectedDate)
            'lnkMyLeave.Text = "My Leaves(" & GridVwHeaderChckbox.Rows.Count & ")"
        Catch ex As Exception
            Response.Write(ex.Message)
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


    Private Function LoadLeavesDataTable(loadtype As String) As DataTable
        Dim datatables As New DataTable
        If loadtype = "All" Then
            datatables = Process.SearchDataP6("Employee_Leavelist_get_all", "", radStatus.SelectedItem.Text, dateFrom.SelectedDate, dateTo.SelectedDate, cboCompany.SelectedValue, "")
        ElseIf loadtype = "Find" Then
            datatables = Process.SearchDataP7("Employee_Leavelist_search", "", radStatus.SelectedItem.Text, dateFrom.SelectedDate, dateTo.SelectedDate, txtsearch.Value, cboCompany.SelectedValue, "")
        End If
        pagetitle.InnerText = cboCompany.SelectedValue & ": " & txtsearch.Value & " Employee Leaves (" & datatables.Rows.Count.ToString & ")"
        Return datatables
    End Function
    Private Sub LoadLeaves(LoadType As String, PageIndex As Integer)
        Try
            GridVwHeaderChckbox.PageIndex = PageIndex
            GridVwHeaderChckbox.DataSource = LoadLeavesDataTable(LoadType)
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
                If Session("clicked") Is Nothing Then
                    Session("clicked") = "0"
                End If

                Dim ismulti As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select isnull(ismulticompany,'No')  from general_info")
                If ismulti.ToLower = "no" Then
                    Process.LoadRadComboTextAndValueP2(cboCompany, "Company_Structure_Get_ByLevel", "1", Session("Access"), "name", "name", False)
                Else
                    Process.LoadRadComboTextAndValueP2(cboCompany, "Company_Structure_Get_ByLevel", "2", Session("Access"), "name", "name", False)
                End If
                If Session("company") Is Nothing Then
                    Session("company") = Session("organisation")
                End If
                Process.AssignRadComboValue(cboCompany, Session("company"))
                'Process.LoadRadComboTextAndValueP1(cboDept, "Company_Parent_Breakdown", cboCompany.SelectedValue, "Companys", "Companys", False)
                'If Session("Dept") <> "" Then
                '    Process.AssignRadComboValue(cboDept, Session("Dept"))
                'End If

                Dim script As String = "$(document).ready(function () { $('[id*=btnApprove]').click(); });"
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "load", script, True)

                cboView.Items.Clear()
                cboView.Items.Add("LeaveList")
                cboView.Items.Add("Grouping")

                radStatus.Items.Clear()
                radStatus.Items.Add("Pending")
                radStatus.Items.Add("Approved")
                radStatus.Items.Add("Cancelled")
                radStatus.Items.Add("Rejected")

                Session("pageIndex1") = 0
                Session("pageIndex2") = 0

                'Leave
                dateFrom.SelectedDate = Process.FirstDateofYear
                dateTo.SelectedDate = Process.LastDateofYear

                dateGroupFrom.SelectedDate = Process.FirstDateofYear
                dateGroupTo.SelectedDate = Process.LastDateofYear

                Session("LoadType") = "All"
                If Session("clicked") = 2 Then
                    Process.AssignRadComboValue(cboView, "Grouping")
                    MultiView1.ActiveViewIndex = 1
                    LoadGroupLeaves(Session("LoadType"), 0)
                Else
                    Process.AssignRadComboValue(cboView, "LeaveList")
                    MultiView1.ActiveViewIndex = 0
                    LoadLeaves(Session("LoadType"), 0)
                End If

            End If
        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
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
            table = LoadLeavesDataTable(Session("LoadType"))
            table.DefaultView.Sort = sortExpression & direction
            GridVwHeaderChckbox.DataSource = table
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub
    Protected Sub SortGroupRecords(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
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
            table = LoadGroupDataTable(Session("LoadType"))
            table.DefaultView.Sort = sortExpression & direction
            gridGroupView.DataSource = table
            gridGroupView.DataBind()
        Catch ex As Exception
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
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
            GridVwHeaderChckbox.DataSource = LoadLeavesDataTable(Session("LoadType"))
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    Protected Sub OnRowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GridVwHeaderChckbox, "Select$" & e.Row.RowIndex)
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

            LoadLeaves(Session("LoadType"), 0)
        Catch ex As Exception
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    Protected Sub Delete(sender As Object, e As EventArgs) Handles btnDelete.Click
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
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Employee_Leavelist_HR_Delete", ID)
                    End If
                Next
                LoadLeaves(Session("LoadType"), Session("pageIndex1"))
            Else
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Delete has been cancelled" + "')", True)
            End If
        Catch ex As Exception
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub




    Protected Sub radStatus_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles radStatus.SelectedIndexChanged
        Try
            If radStatus.SelectedItem.Text <> "Approved" Then
                btnApprove.EnableViewState = True
            Else
                btnApprove.EnableViewState = False
            End If
            If Session("clicked") = 2 Then
                Process.AssignRadComboValue(cboView, "Grouping")
                MultiView1.ActiveViewIndex = 1
                LoadGroupLeaves(Session("LoadType"), 0)
            Else
                Process.AssignRadComboValue(cboView, "LeaveList")
                MultiView1.ActiveViewIndex = 0
                LoadLeaves(Session("LoadType"), 0)
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub LodaDataTable(id As String)
        Dim dtEarning As New DataTable
        dtEarning = Process.SearchData("Employee_Leavelist_get", id)
        GenerateLeaveLetter(dtEarning, Process.GetData("general_info_get"), Server.MapPath(emailFile & "LeaveLetter" & id & ".PDF"))
    End Sub
    Private Sub GenerateLeaveLetter(dtearn As DataTable, logos As DataTable, ByVal savePath As String)
        Dim ReportViewer1 As New ReportViewer
        ReportViewer1.ProcessingMode = ProcessingMode.Local
        ReportViewer1.SizeToReportContent = True
        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Module/Reports/TimeManagement/LeaveLetter.rdlc")
        Dim _rsource As New ReportDataSource("Leave", dtearn)
        Dim _rsource2 As New ReportDataSource("logo", logos)
        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.LocalReport.DataSources.Add(_rsource)
        ReportViewer1.LocalReport.DataSources.Add(_rsource2)
        ReportViewer1.LocalReport.Refresh()
        Dim Bytes() As Byte = ReportViewer1.LocalReport.Render("PDF", "", Nothing, Nothing, Nothing, Nothing, Nothing)

        If File.Exists(savePath) Then
            File.Delete(savePath)
        End If

        Using Stream As New FileStream(savePath, FileMode.Create)
            Stream.Write(Bytes, 0, Bytes.Length)
        End Using
        Session("rptAttachment") = savePath
    End Sub
    Protected Sub btnApprove_Click(sender As Object, e As EventArgs)
        Try
            Dim leaveref As String = ""
            Dim finalstatus As String = ""
            Dim RequesterName As String = ""
            Dim RequesterMail As String = ""
            Dim supervisorStat As String = ""
            Dim RequesterID As String = ""
            Dim noOfDays As Integer
            Dim startdate As Date, enddate As Date, approver1name As String = "", approver2name As String = ""
            Dim leavetype As String = "", status_approver1name As String = "", status_approver2name As String = ""

            'Dim confirmValue As String = Request.Form("confirm_app")
            Dim confirmValue As String = "Yes"
            If confirmValue = "Yes" Then
                System.Threading.Thread.Sleep(300)
                Dim atLeastOneRowDeleted As Boolean = False
                ' Iterate through the Products.Rows property
                For Each row As GridViewRow In GridVwHeaderChckbox.Rows
                    ' Access the CheckBox
                    Dim cb As CheckBox = row.FindControl("chkEmp")
                    If cb IsNot Nothing AndAlso cb.Checked Then
                        atLeastOneRowDeleted = True

                        Dim ID As String = Convert.ToString(GridVwHeaderChckbox.DataKeys(row.RowIndex).Value)

                        Dim strLoan As New DataSet
                        strLoan = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Employee_Leavelist_get", ID)
                        If strLoan.Tables(0).Rows.Count > 0 Then
                            finalstatus = strLoan.Tables(0).Rows(0).Item("FinalStatus").ToString
                            status_approver1name = strLoan.Tables(0).Rows(0).Item("status").ToString
                            status_approver2name = strLoan.Tables(0).Rows(0).Item("status2").ToString
                            approver1name = strLoan.Tables(0).Rows(0).Item("Approver1Name").ToString
                            approver2name = strLoan.Tables(0).Rows(0).Item("Approver2Name").ToString
                            leaveref = strLoan.Tables(0).Rows(0).Item("refno").ToString
                            RequesterID = strLoan.Tables(0).Rows(0).Item("EmpID").ToString
                            noOfDays = strLoan.Tables(0).Rows(0).Item("NoOfDays").ToString
                            startdate = strLoan.Tables(0).Rows(0).Item("LeaveFrom").ToString
                            enddate = strLoan.Tables(0).Rows(0).Item("LeaveTo").ToString

                            Dim strGrade As New DataSet
                            Dim approvername As String = ""

                            'Get Admin Approver
                            strGrade = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "select name, email from dbo.Employees_All where empid = '" & Session("UserEmpID") & "'")
                            If strGrade.Tables(0).Rows.Count > 0 Then
                                approvername = strGrade.Tables(0).Rows(0).Item("name").ToString
                            End If

                            'Get Employee
                            strGrade = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "select name, email from dbo.Employees_All where empid = '" & RequesterID & "'")

                            If strGrade.Tables(0).Rows.Count > 0 Then
                                RequesterMail = strGrade.Tables(0).Rows(0).Item("email").ToString
                                RequesterName = strGrade.Tables(0).Rows(0).Item("name").ToString
                            End If

                            'Send Mails to Employee
                            LodaDataTable(ID)
                            'Process.Leave_Notification_Final(RequesterMail, leaveref, finalstatus, RequesterName, leavetype, startdate, enddate, "", status_approver1name, approver1name, status_approver2name, approver2name, RequesterID, Session("UserEmpID"), Session("rptAttachment"))
                            'Process.Leave_Notification_Final(leaveref, RequesterName, leavetype, startdate, enddate, "", status_approver1name, approver1name, status_approver2name, approver2name, RequesterID, Session("UserEmpID"), Session("rptAttachment"), Process.ApplicationURL + "/" + Process.GetMailLink(AuthenCode, 1), Process.ApplicationURL + "/" + Process.GetMailLink(AuthenCode, 2))
                        End If

                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Employee_Leavelist_Update_HR_Status", ID, "Approved", "2", Session("UserEmpID"), Session("LoginID"), startdate, enddate, noOfDays)
                        Process.Leave_Notification_Final(leaveref, RequesterName, leavetype, startdate, enddate, "", status_approver1name, approver1name, status_approver2name, approver2name, RequesterID, Session("UserEmpID"), Session("rptAttachment"), Process.ApplicationURL + "/" + Process.GetMailLink(AuthenCode, 1), Process.ApplicationURL + "/" + Process.GetMailLink(AuthenCode, 2))
                    End If
                Next
                If atLeastOneRowDeleted = True Then
                    Process.loadalert(divalert, msgalert, "Multiple Leaves Approved successful", "success")
                    'Response.Write("Multiple Leaves Approved successful")
                    'ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Multiple Leaves Approved successful" + "')", True)
                Else
                    Process.loadalert(divalert, msgalert, "Multiple Approval cancelled, no selection made", "success")
                    'ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Multiple Approval cancelled, no selection made" + "')", True)
                End If
                LoadLeaves(Session("LoadType"), Session("pageIndex1"))
            Else
                Process.loadalert(divalert, msgalert, "Multiple Approval cancelled, no selection made", "success")
                'ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Multiple Approval cancelled" + "')", True)
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub gridGroupView_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gridGroupView.PageIndexChanging
        Try
            gridGroupView.PageIndex = e.NewPageIndex
            Session("pageIndex2") = e.NewPageIndex
            gridGroupView.DataSource = LoadGroupDataTable(Session("LoadType"))
            gridGroupView.DataBind()
        Catch ex As Exception
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    Protected Sub drpView_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboView.SelectedIndexChanged
        Try
            If cboView.SelectedItem.Text = "LeaveList" Then
                MultiView1.ActiveViewIndex = 0
                Session("clicked") = 1
                LoadLeaves(Session("LoadType"), Session("pageIndex1"))
            Else
                MultiView1.ActiveViewIndex = 1
                Session("clicked") = 2
                LoadGroupLeaves(Session("LoadType"), Session("pageIndex2"))

            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnGroupApprove_Click(sender As Object, e As EventArgs)
        Try
            Dim msgbuild As New StringBuilder()
            Dim Separators() As Char = {";"c}

            Dim leaveref As String = ""
            Dim finalstatus As String = ""
            Dim RequesterName As String = ""
            Dim RequesterMail As String = ""
            Dim supervisorStat As String = ""
            Dim RequesterID As String = ""
            Dim startdate As Date, enddate As Date, approver1name As String = "", approver2name As String = ""
            Dim leavetype As String = "", status_approver1name As String = "", status_approver2name As String = ""
            Dim strLoan As New DataSet

            Dim confirmValue As String = Request.Form("confirm_app")
            If confirmValue = "No" Then
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Multiple Approval cancelled" + "')", True)
            Else

                System.Threading.Thread.Sleep(1000)
                Dim atLeastOneRowDeleted As Boolean = False
                ' Iterate through the Products.Rows property
                For Each row As GridViewRow In gridGroupView.Rows
                    ' Access the CheckBox
                    Dim cb As CheckBox = row.FindControl("chkEmp")
                    If cb IsNot Nothing AndAlso cb.Checked Then
                        atLeastOneRowDeleted = True

                        Dim ID As String = Convert.ToString(gridGroupView.DataKeys(row.RowIndex).Values(0))
                        Dim datefrom As String = Process.DDMONYYYY(Convert.ToDateTime(gridGroupView.DataKeys(row.RowIndex).Values(1)).Date)
                        Dim dateto As String = Process.DDMONYYYY(Convert.ToDateTime(gridGroupView.DataKeys(row.RowIndex).Values(2)).Date)


                        strLoan = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Employee_Leavelist_get_HR_pending", ID, datefrom, dateto)

                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Employee_Leavelist_HR_Update_Multiple_Status", ID, datefrom, dateto, "Approved")


                        If strLoan.Tables(0).Rows.Count > 0 Then
                            For i = 0 To strLoan.Tables(0).Rows.Count - 1
                                finalstatus = strLoan.Tables(0).Rows(i).Item("FinalStatus").ToString
                                status_approver1name = strLoan.Tables(0).Rows(i).Item("status").ToString
                                status_approver2name = strLoan.Tables(0).Rows(i).Item("status2").ToString
                                approver1name = strLoan.Tables(0).Rows(i).Item("Approver1Name").ToString
                                approver2name = strLoan.Tables(0).Rows(i).Item("Approver2Name").ToString
                                leaveref = strLoan.Tables(0).Rows(i).Item("refno").ToString
                                RequesterID = strLoan.Tables(0).Rows(i).Item("EmpID").ToString
                                leavetype = strLoan.Tables(0).Rows(i).Item("Leave Type").ToString
                                startdate = CDate(strLoan.Tables(0).Rows(i).Item("from"))
                                enddate = CDate(strLoan.Tables(0).Rows(i).Item("to"))
                                Dim strGrade As New DataSet
                                Dim approvername As String = ""

                            Next
                        End If
                    End If
                Next
                If atLeastOneRowDeleted = True Then
                    Process.loadalert(divalert, msgalert, "Multiple Pending Leaves Approved successful", "success")

                Else
                    Process.loadalert(divalert, msgalert, "Multiple Approval cancelled, no selection made", "info")
                End If
                LoadLeaves(Session("LoadType"), Session("pageIndex1"))

            End If
        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub



    Protected Sub cboCompany_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboCompany.SelectedIndexChanged
        Try
            Session("company") = cboCompany.SelectedValue
            Session("LoadType") = "All"
            If Session("clicked") = 2 Then
                Process.AssignRadComboValue(cboView, "Grouping")
                MultiView1.ActiveViewIndex = 1
                LoadGroupLeaves(Session("LoadType"), 0)
            Else
                Process.AssignRadComboValue(cboView, "LeaveList")
                MultiView1.ActiveViewIndex = 0
                LoadLeaves(Session("LoadType"), 0)
            End If

        Catch ex As Exception

        End Try
    End Sub


End Class