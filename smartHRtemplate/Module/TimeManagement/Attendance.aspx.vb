Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Public Class Attendance
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "ATTENDANCE"
    Dim Pages As String = "Employee Attendance"
    Dim apptype As String = ConfigurationManager.AppSettings("apptype")
    Private Sub LinkClink(ByVal ViewIndex As String)
        Try
            Session("clicked") = ViewIndex
            MultiView1.ActiveViewIndex = ViewIndex
            Process.DeactivateButton(btnLog)
            Process.DeactivateButton(btnRegister)

            Select Case ViewIndex
                Case "0"
                    Process.ActivateButton(btnLog)
                    LoadLogGrid(Session("LoadType"))
                Case "1"
                    Process.ActivateButton(btnRegister)
                    LoadAttendanceGrid(Session("LoadType"))

                Case Else
                    Process.ActivateButton(btnLog)
                    LoadLogGrid(Session("LoadType"))
            End Select
        Catch ex As Exception

        End Try
    End Sub
    Private Function LoadAttendance(LoadType As String) As DataTable
        Dim datatables As New DataTable
        Session("company") = cboCompany.SelectedValue

        If LoadType = "All" Then
            datatables = Process.SearchDataP2("Time_Employee_Attendance_Get_All_Admin", Session("company"), MyCalendar.SelectedDate)
        ElseIf LoadType = "Find" Then
            datatables = Process.SearchDataP3("Time_Employee_Attendance_Search_Admin", Session("company"), MyCalendar.SelectedDate, txtsearch.Value.Trim)
        End If
        If MyCalendar.SelectedDate.ToString IsNot Nothing Then
            pagetitle.InnerHtml = Process.DDMONYYYY(MyCalendar.SelectedDate) & ": " & txtsearch.Value & " Employee Attendance"
        Else
            pagetitle.InnerHtml = Session("company") & ": " & txtsearch.Value & " Employee Attendance"
        End If

        Return datatables
    End Function
    Private Function LoadLog(LoadType As String) As DataTable
        Dim datatables As New DataTable
        Session("company") = cboCompany.SelectedValue

        If LoadType = "All" Then
            datatables = Process.SearchDataP3("Time_Attendance_get_all", Process.DDMONYYYY(dateFrom.SelectedDate), Process.DDMONYYYY(dateTo.SelectedDate), Session("company"))
        ElseIf LoadType = "Find" Then
            datatables = Process.SearchDataP4("Time_Attendance_Search", Process.DDMONYYYY(dateFrom.SelectedDate), Process.DDMONYYYY(dateTo.SelectedDate), Session("company"), txtsearch.Value.Trim)
        End If
        pagetitle.InnerHtml = Session("company") & ": " & txtsearch.Value & " " & Process.DDMONYYYY(dateFrom.SelectedDate) & " to " & Process.DDMONYYYY(dateTo.SelectedDate) & " Employee Attendance"

        Return datatables
    End Function
    Private Sub LoadLogGrid(LoadType As String)
        Try
            gridLog.DataSource = LoadLog(LoadType)
            gridLog.AllowSorting = True
            gridLog.AllowPaging = True
            gridLog.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub
    Private Sub LoadAttendanceGrid(LoadType As String)
        Try
            GridVwHeaderChckbox.DataSource = LoadAttendance(LoadType)
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

            If apptype.ToLower = "cloud" Then
                btnZKteco.Visible = False
            End If

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
                    Session("company") = Session("Organisation")
                End If
                Process.AssignRadComboValue(cboCompany, Session("company"))

                If Session("Date1") Is Nothing Then
                    Session("Date1") = Date.Now
                End If

                If Session("Date2") Is Nothing Then
                    Session("Date2") = Date.Now
                End If

                dateFrom.SelectedDate = Session("Date1")  'Process.FirstDay(Date.Now.Year, Date.Now.Month) 'Date.Now.AddDays(1 - Date.Now.Day).AddMonths(1 - Date.Now.Month)
                dateTo.SelectedDate = Session("Date2")  'Date.Now.Date
                MyCalendar.SelectedDate = Date.Now
                Session("LoadType") = "All"
                If Session("clicked") Is Nothing Then
                    Session("clicked") = "0"
                End If

                LinkClink(Session("clicked"))
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
            Dim table As DataTable = LoadAttendance(Session("LoadType"))
            table.DefaultView.Sort = sortExpression & direction
            GridVwHeaderChckbox.DataSource = table
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub SortLogRecords(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
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
            Dim table As DataTable = LoadLog(Session("LoadType"))
            table.DefaultView.Sort = sortExpression & direction
            gridLog.DataSource = table
            gridLog.DataBind()
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
    Protected Sub gridLog_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gridLog.PageIndexChanging
        Try
            gridLog.PageIndex = e.NewPageIndex
            gridLog.DataSource = LoadLog(Session("LoadType"))
            gridLog.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub GridVwHeaderChckbox_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridVwHeaderChckbox.PageIndexChanging
        Try
            GridVwHeaderChckbox.PageIndex = e.NewPageIndex
            GridVwHeaderChckbox.DataSource = LoadAttendance(Session("LoadType"))
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub OnRowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridVwHeaderChckbox.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                For Each cell As TableCell In e.Row.Cells
                    Dim read As String = e.Row.Cells(13).Text.Trim
                    Dim imgProd As HyperLink = DirectCast(e.Row.FindControl("HyperLink1"), HyperLink)
                    If read = "" Then
                        imgProd.NavigateUrl = ""
                        imgProd.Enabled = False
                    End If

                Next
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs)
        Try

            If txtsearch.Value = "" Then
                Session("LoadType") = "All"
            Else
                Session("LoadType") = "Find"
            End If
            LoadLogGrid(Session("LoadType"))

        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub


    Protected Sub btnFind1_Click(sender As Object, e As EventArgs)

    End Sub
    Protected Sub Delete(sender As Object, e As EventArgs)
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
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Time_Attendance_delete", ID)
                    End If
                Next
                LoadLogGrid(Session("LoadType"))
            Else
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Delete has been cancelled" + "')", True)
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub
    Public Shared Function AttendanaceCreate(sStartDate As Date, sEndDate As Date, ByVal scompany As String) As Boolean
        Try
            Dim conn2 As New SqlConnection(WebConfig.ConnectionString)
            Dim comm2 As New SqlCommand("Time_Employee_Attendance_Create", conn2)
            comm2.CommandType = CommandType.StoredProcedure
            comm2.Parameters.AddWithValue("@StartDate", sStartDate)
            comm2.Parameters.AddWithValue("@EndDate", sEndDate)
            comm2.Parameters.AddWithValue("@company", scompany)
            comm2.CommandTimeout = 157200
            Dim checkDS As New DataSet
            Dim sdat2 As New SqlDataAdapter(comm2)
            sdat2.Fill(checkDS)
            Return True
        Catch ex As Exception
            Process.strExp = ex.Message
            Return False
        End Try
    End Function
    Protected Sub btnUpload_Click(sender As Object, e As EventArgs)
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                Response.Write("You don't have privilege to perform this action")
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
                Exit Sub
            End If

            'Dim confirmValue As String = Request.Form("confirm_value")
            Dim confirmValue As String = "Yes"
            If confirmValue = "Yes" Then
                'System.Threading.Thread.Sleep(2000)
                If FileUpload1.PostedFile IsNot Nothing Then
                    'To create a PostedFile
                    Dim csvPath As String = Server.MapPath(Process.FileURL) + Path.GetFileName(FileUpload1.PostedFile.FileName)
                    If File.Exists(csvPath) = True Then
                        File.Delete(csvPath)
                    End If
                    FileUpload1.PostedFile.SaveAs(csvPath)
                    'Create byte Array with file len
                    'File.ContentLength
                    If chkDelete.Checked = True Then
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Time_Attendance_Delete_Date", dateFrom.SelectedDate, dateTo.SelectedDate.Value.AddDays(1), cboCompany.SelectedValue)
                    End If

                    If Process.Import(csvPath, "Time_Attendance_upload", Pages) = True Then

                        If AttendanaceCreate(dateFrom.SelectedDate.Value, dateTo.SelectedDate.Value.AddDays(1), cboCompany.SelectedValue) = True Then
                            Process.loadalert(divalert, msgalert, "Uploaded " & Session("uploadcnt") & " record(s)", "success")
                        Else
                            Response.Write(Process.strExp)
                        End If

                    Else
                        Response.Write(Process.strExp)
                    End If
                    Button1_Click(sender, e)
                    ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & cboCompany.SelectedValue & ": Uploaded " & Session("uploadcnt") & " record(s) from " & csvPath + "')", True)
                    Process.GetAuditTrailInsertandUpdate("", cboCompany.SelectedValue & ": Uploaded " & Session("uploadcnt") & " record(s) from " & csvPath, "File Upload", Pages)
                Else

                End If

                LoadLogGrid(Session("LoadType"))
            Else
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Import cancelled!" + "')", True)

            End If




        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    Protected Sub Button1_Click1(sender As Object, e As EventArgs)
        Try
            If Process.Export(gridLog, "LogSheet", 1, 100) = False Then
                Response.Write(Process.strExp)
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & Process.strExp + "')", True)
            End If



        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    Protected Sub btnZKteco_Click(sender As Object, e As EventArgs)
        Try
            Dim counts As Integer = 0
            Dim adatasource As String = ""
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Import Attendance of " & dateFrom.SelectedDate.ToString & " to " & dateTo.SelectedDate.ToString & " from ZKTeco Database" + "')", True)
            Dim confirmValue As String = Request.Form("confirm_value")
            If confirmValue = "Yes" Then
                'System.Threading.Thread.Sleep(2000)
                If chkDelete.Checked = True Then
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Time_Attendance_Delete_Date", dateFrom.SelectedDate, dateTo.SelectedDate.Value.AddDays(1))
                End If

                Dim strUser As New DataSet
                strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "zkteco_configuration_get")
                If strUser.Tables(0).Rows.Count > 0 Then
                    adatasource = strUser.Tables(0).Rows(0).Item("datasource").ToString
                End If

                Dim conn As New ADODB.Connection, rec As New ADODB.Recordset
                Dim esql As String = "", esql2 As String = "", searchvar As String = ""
                Dim test As Boolean = False
                conn = New ADODB.Connection
                rec = New ADODB.Recordset
                Dim enddate As String = dateTo.SelectedDate.Value.AddDays(1)
                Dim startdate As String = dateFrom.SelectedDate.Value
                conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & adatasource & ";Persist Security Info=False"
                conn.Open()
                esql = "SELECT [USERID], [CHECKTIME], [CHECKTYPE], [VERIFYCODE] FROM [CHECKINOUT] where [CHECKTIME] >= #" & Process.DDMONYYYY(startdate) & "# AND [CHECKTIME] <= #" & Process.DDMONYYYY(enddate) & "#"
                rec.Open(esql, conn, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
                Dim userid As String = ""
                Dim checktime As String = ""
                Dim checktype As String = ""
                While Not rec.EOF
                    counts = counts + 1
                    userid = rec.Fields(0).Value
                    checktime = rec.Fields(1).Value
                    checktype = rec.Fields(2).Value
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Time_Attendance_Upload", userid, checktime, checktype)
                    rec.MoveNext()
                End While
                Button1_Click(sender, e)
                Response.Write(counts & " records uploaded")
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & counts & " records uploaded" + "')", True)

                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Time_Employee_Attendance_Create", dateFrom.SelectedDate.Value, dateTo.SelectedDate.Value.AddDays(1))
                Response.Write(Process.strExp)
                LoadLogGrid(Session("LoadType"))
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub



    Protected Sub MyCalendar_SelectionChanged(sender As Object, e As EventArgs) Handles MyCalendar.SelectionChanged
        Try
            LoadAttendanceGrid(Session("LoadType"))
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cboCompany_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboCompany.SelectedIndexChanged
        Try
            Session("company") = cboCompany.SelectedValue
            Session("LoadType") = "All"
            LoadAttendanceGrid(Session("LoadType"))
            LoadLogGrid(Session("LoadType"))
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnExport_Click(sender As Object, e As EventArgs)
        Try
            If Process.Export(GridVwHeaderChckbox, "EmployeeAttendance", 0, 100) = False Then
                Response.Write(Process.strExp)
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & Process.strExp + "')", True)
            End If

            '    'Process.Export(GridVwHeaderChckbox, "JobGrades", 1, 2)
            '    Response.Clear()
            '    Response.Buffer = True
            '    Response.AddHeader("content-disposition", "attachment;filename=EmployeeAttendance.csv")
            '    Response.Charset = ""
            '    Response.ContentType = "application/text"
            '    Dim sBuilder As StringBuilder = New System.Text.StringBuilder()
            '    For index As Integer = 1 To GridVwHeaderChckbox.Columns.Count - 1
            '        sBuilder.Append(GridVwHeaderChckbox.Columns(index).HeaderText + ","c)

            '    Next
            '    sBuilder.Append(vbCr & vbLf)
            '    For i As Integer = 0 To GridVwHeaderChckbox.Rows.Count - 1
            '        For k As Integer = 1 To GridVwHeaderChckbox.HeaderRow.Cells.Count - 1
            '            If k = 2 Then
            '                Dim controls As HyperLink = DirectCast(GridVwHeaderChckbox.Rows(i).Cells(k).FindControl("HyperLink1"), HyperLink)
            '                sBuilder.Append(controls.Text.Replace(",", "") + ",")
            '            Else
            '                sBuilder.Append(GridVwHeaderChckbox.Rows(i).Cells(k).Text.Replace(",", "") + ",")
            '            End If
            '        Next
            '        sBuilder.Append(vbCr & vbLf)
            '    Next
            '    Response.Output.Write(sBuilder.ToString())
            '    Response.Flush()
            '    Response.[End]()

        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    Protected Sub btnLog_Click(sender As Object, e As EventArgs) Handles btnLog.Click
        LinkClink(0)
        'LoadLogGrid(Session("LoadType"))
    End Sub

    Protected Sub btnRegister_Click(sender As Object, e As EventArgs) Handles btnRegister.Click
        LinkClink(1)
        'LoadAttendanceGrid(Session("LoadType"))
    End Sub


    Protected Sub btnRefresh_Click(sender As Object, e As EventArgs)
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                Response.Write("You don't have privilege to perform this action")
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
                Exit Sub
            End If
            Dim confirmValue As String = Request.Form("confirm_value")
            If confirmValue = "Yes" Then
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Time_Employee_Attendance_Create", dateFrom.SelectedDate.Value, dateTo.SelectedDate.Value.AddDays(1), cboCompany.SelectedValue)
                If AttendanaceCreate(dateFrom.SelectedDate.Value, dateTo.SelectedDate.Value.AddDays(1), cboCompany.SelectedValue) = True Then
                    Response.Write(cboCompany.SelectedValue & ": " & Process.DDMONYYYY(dateFrom.SelectedDate) & " to " & Process.DDMONYYYY(dateTo.SelectedDate) & " successfully refreshed")
                    ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & cboCompany.SelectedValue & ": " & Process.DDMONYYYY(dateFrom.SelectedDate) & " to " & Process.DDMONYYYY(dateTo.SelectedDate) & " successfully refreshed" + "')", True)
                Else
                    Response.Write(Process.strExp)
                End If

                LoadAttendanceGrid(Session("LoadType"))
            Else
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Cancelled" + "')", True)
            End If


        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    Protected Sub Button1_Click2(sender As Object, e As EventArgs)
        Try

            If txtsearch.Value = "" Then
                Session("LoadType") = "All"
            Else
                Session("LoadType") = "Find"
            End If
            LoadAttendanceGrid(Session("LoadType"))
        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    Private Sub dateFrom_SelectedDateChanged(sender As Object, e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles dateFrom.SelectedDateChanged
        Try
            Session("Date1") = dateFrom.SelectedDate
        Catch ex As Exception

        End Try
    End Sub

    Private Sub dateTo_SelectedDateChanged(sender As Object, e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles dateTo.SelectedDateChanged
        Try
            Session("Date2") = dateTo.SelectedDate
        Catch ex As Exception

        End Try
    End Sub
End Class