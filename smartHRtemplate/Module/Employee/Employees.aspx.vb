Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Imports Telerik.Web.UI
Imports System.Web.UI
Imports System.Threading


Public Class Employees
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "EMPLIST"
    Dim Pages As String = "Employee Page"
    Dim lblstatus As String
    Dim PhotoURL As String = ConfigurationManager.AppSettings("PhotoURL")
    Dim emailFile As String = ConfigurationManager.AppSettings("FileURL")
    Dim sampleCSV As String = ConfigurationManager.AppSettings("sampleCSV")
    Public Shared Separator() As Char = {"."c}
    Private Sub UserDataBound()
        Try
            For Each row As DataListItem In dlBlogs.Items
                Dim lblempid As Label = row.FindControl("lblgender")

                Dim imagebtn As ImageButton = row.FindControl("imgavatar")

                'imagebtn.ImageUrl = "ImgHandler.ashx?imgid=" & lblempid.Text
                imagebtn.ImageUrl = lblempid.Text
            Next
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Private Sub BlockPageIndex(pagelow As Integer, pagehigh As Integer)
        Try
            LoadBlock(pagelow, pagehigh)
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub ListClick(sender As Object, e As EventArgs)
        Try
            LoadGrid()
            LinkClink(0)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub BlockClick(sender As Object, e As EventArgs)
        Try
            pagetotal.InnerText = Session("emppagetotal")
            pageno.Disabled = True
            pageof.Disabled = True
            pagetotal.Disabled = True
            LoadBlock(Session("emppagelow"), Session("emppagehigh"))
            LinkClink(1)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Private Sub LinkClink(ByVal ViewIndex As Integer)
        Try
            Session("empclicked") = ViewIndex
            MultiView1.ActiveViewIndex = ViewIndex
        Catch ex As Exception
        End Try
    End Sub
    Private Function GetPageCount(ByVal totalcount As Integer) As Integer
        Try
            Dim pgcount As Integer = 0
            Dim rems As Integer = 0
            If totalcount <= 100 Then
                pgcount = 1
            Else
                pgcount = totalcount \ 100
                rems = totalcount Mod 100
                If rems > 0 Then
                    pgcount = pgcount + 1
                End If
            End If
            Return pgcount
        Catch ex As Exception
            Return 0
        End Try
    End Function
    Protected Sub MoveFirst(sender As Object, e As EventArgs)
        Try
            Session("emppagelow") = "1"
            Session("emppagehigh") = "100"
            Session("emppageno") = "1"

            pageno.InnerText = Session("emppageno")

            BlockPageIndex(Session("emppagelow"), Session("emppagehigh"))
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub MoveLast(sender As Object, e As EventArgs)
        Try
            Session("emppagelow") = (((CInt(Session("emppagetotal")) * 100) - 100) + 1).ToString
            Session("emppagehigh") = (CInt(Session("emppagetotal")) * 100).ToString
            Session("emppageno") = Session("emppagetotal")
            'Session("emppagetotal") = "1"

            pageno.InnerText = Session("emppageno")
            BlockPageIndex(Session("emppagelow"), Session("emppagehigh"))
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub MoveNext(sender As Object, e As EventArgs)
        Try
            Session("emppageno") = CInt(Session("emppageno")) + 1
            If CInt(Session("emppageno")) > CInt(Session("emppagetotal")) Then
                Session("emppageno") = CInt(Session("emppageno")) - 1
            End If

            Session("emppagelow") = (((CInt(Session("emppageno")) * 100) - 100) + 1).ToString
            Session("emppagehigh") = (CInt(Session("emppageno")) * 100).ToString

            pageno.InnerText = Session("emppageno")

            BlockPageIndex(Session("emppagelow"), Session("emppagehigh"))
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub MovePrevious(sender As Object, e As EventArgs)
        Try
            Session("emppageno") = CInt(Session("emppageno")) - 1
            If CInt(Session("emppageno")) <= 0 Then
                Session("emppageno") = 1
            End If

            Session("emppagelow") = (((CInt(Session("emppageno")) * 100) - 100) + 1).ToString
            Session("emppagehigh") = (CInt(Session("emppageno")) * 100).ToString

            pageno.InnerText = Session("emppageno")
            BlockPageIndex(Session("emppagelow"), Session("emppagehigh"))
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Function LoadEmployeeGrid(pagelow As Integer, pagehigh As Integer) As DataTable
        Dim datatables As New DataTable
        If Request.QueryString("empid") IsNot Nothing Then
            datatables = Process.SearchData("Emp_PersonalDetail_Get_1", Request.QueryString("empid"))
            btBack.Visible = True
            cboCompany.Visible = False
            A2.Visible = False
            A1.Visible = False
        Else
            Session("company") = cboCompany.SelectedValue
            If Session("emploadtype") = "All" Then
                datatables = Process.SearchDataP4("Emp_PersonalDetail_get_all_specific", "", Session("company"), pagelow, pagehigh)
            ElseIf Session("emploadtype") = "Find" Then
                search.Value = Session("empsearch")
                datatables = Process.SearchDataP5("Emp_PersonalDetail_search_specific", search.Value.Trim, "", Session("company"), pagelow, pagehigh)
            End If
            btBack.Visible = False
        End If

        If pagehigh > 999900 Then
            Session("emppagetotal") = GetPageCount(datatables.Rows.Count)
            If cboCompany.Visible = False Then
                pagetitle.InnerText = "Employee (" & FormatNumber(datatables.Rows.Count, 0) & ")"
            Else
                pagetitle.InnerText = Session("company") & ": Employee (" & FormatNumber(datatables.Rows.Count, 0) & ")"
            End If
        End If
        Return datatables
    End Function

    Private Sub LoadGrid()
        Try
            GridVwHeaderChckbox.PageIndex = CInt(Session("emppageindex"))
            GridVwHeaderChckbox.DataSource = LoadEmployeeGrid(1, 2000000)
            GridVwHeaderChckbox.AllowSorting = True
            GridVwHeaderChckbox.AllowPaging = True

            GridVwHeaderChckbox.DataBind()

            LoadBlock(Session("emppagelow"), Session("emppagehigh"))
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Sub LoadBlock(pagelow As Integer, pagehigh As Integer)
        Try
            Dim sdatatable As New DataTable
            sdatatable = LoadEmployeeGrid(pagelow, pagehigh)
            dlBlogs.DataSource = sdatatable
            dlBlogs.DataBind()
            UserDataBound()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Read") = False Then
                holder.Style.Add("display", "none")
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform view this page", "info")
                Exit Sub
            End If

            If Not Me.IsPostBack Then
                Dim ismulti As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select isnull(ismulticompany,'No')  from general_info")
                If ismulti.ToLower = "no" Then
                    Process.LoadRadComboTextAndValueP2(cboCompany, "Company_Structure_Get_ByLevel", "1", Session("Access"), "name", "name", False)
                    dvcompany.Visible = False
                Else
                    Process.LoadRadComboTextAndValueP2(cboCompany, "Company_Structure_Get_ByLevel", "2", Session("Access"), "name", "name", False)
                End If
                If Session("company") Is Nothing Then
                    Session("company") = Session("Organisation")
                End If

                If Session("empclicked") Is Nothing Then
                    Session("empclicked") = "1"
                End If

                If Session("emploadtype") Is Nothing Then
                    Session("emploadtype") = "All"
                End If

                If Session("emppageindex") Is Nothing Then
                    Session("emppageindex") = "0"
                End If

                If Session("emppageno") Is Nothing Then
                    Session("emppageno") = "1"
                End If

                If Session("emppagetotal") Is Nothing Then
                    Session("emppagetotal") = "1"
                End If

                If Session("emppagelow") Is Nothing Then
                    Session("emppagelow") = "1"
                End If

                If Session("emppagehigh") Is Nothing Then
                    Session("emppagehigh") = "100"
                End If

                cboUploadType.Items.Clear()
                cboUploadType.Items.Add("Biodata")
                cboUploadType.Items.Add("Work History")
                cboUploadType.Items.Add("Contact Detail")
                cboUploadType.Items.Add("Dependants")
                cboUploadType.Items.Add("Emergency Contact")
                cboUploadType.Items.Add("Education")
                cboUploadType.Items.Add("Professional Qualification")
                cboUploadType.Items.Add("Employess Hobbies")
                cboUploadType.Items.Add("Employee Asset")

                Dim tooltips As String = ""
                If cboUploadType.SelectedItem.Text = "Biodata" Then
                    tooltips = "CSV File (Comma Delimited): empid,firstname,MIDDLENAME,LASTNAME,GENDER,MARITALSTATUS,NATIONALITY,DATE_OF_BIRTH,BLOODGROUP,STATE,IDENTIFICATION,IDNUMBER,IDEXPIRYDATE,IDD_ISSUER,COUNTRY_OF_BIRTH,PLACE_OF_BIRTH,ADDRESS,POSTAL_ADDRESS,CITY,COUNTRY,MOBILE,OFFICEPHONE,PERSONALEMAIL,WORKEMAIL,DATEJOINED, CONFIRMATION_DATE,USERID,ROLE,ISSUPERUSER {Yes/No},ISHRADMIN {Yes/No}, ISFINADMIN {Yes/No}"
                    btnUploadFile.Attributes.Add("title", tooltips.ToLower)
                ElseIf cboUploadType.SelectedItem.Text = "Work History" Then
                    tooltips = "CSV File (Comma Delimited): EMPID,GRADE,JOBTITLE,JOBTYPE,SUPERVISOREMPIDID,COACHEMPID,OFFICE/DEPT,LOCATION,STARTMONTH e.g JAN, STARTYEAR, ENDMONTH e.g JAN or Present, ENDYEAR : 0 if ENDDATE is Present"
                    'btnUpload.ToolTip = tooltips.ToLower
                    btnUploadFile.Attributes.Add("title", tooltips.ToLower)
                ElseIf cboUploadType.SelectedItem.Text = "Contact Detail" Then
                    tooltips = "CSV File (Comma Delimited): empid,address,city,country,phonenumber,personal emailadress,official emailaddress, official phonenumber"
                    btnUploadFile.Attributes.Add("title", tooltips.ToLower)
                ElseIf cboUploadType.SelectedItem.Text = "Dependants" Then
                    tooltips = "CSV File (Comma Delimited): empid,fullname,relationship, date of birth e.g 12-Jan-1998"
                    btnUploadFile.Attributes.Add("title", tooltips.ToLower)
                ElseIf cboUploadType.SelectedItem.Text = "Emergency Contact" Then
                    tooltips = "CSV File (Comma Delimited): empid,name1,address1, phonenumber1,relationship1,name2,address2, phonenumber2,relationship2, referee1name, referee1address, referee1phone,refereee1mail,yearsknown,referee2name, referee2address, referee2phone,refereee2mail,yearsknown"
                    btnUploadFile.Attributes.Add("title", tooltips.ToLower)
                ElseIf cboUploadType.SelectedItem.Text = "Education" Then
                    tooltips = "CSV File (Comma Delimited): empid,degree,institution, startdate e.g DEC, startyear, enddate e.g DEC, endyear"
                    btnUploadFile.Attributes.Add("title", tooltips.ToLower)
                ElseIf cboUploadType.SelectedItem.Text = "Professional Qualification" Then
                    tooltips = "CSV File (Comma Delimited): empid,qualification,institution, startdate e.g DEC, startyear, completedate e.g DEC, completeyear"
                    btnUploadFile.Attributes.Add("title", tooltips.ToLower)
                ElseIf cboUploadType.SelectedItem.Text = "Employess Hobbies" Then
                    tooltips = "CSV File (Comma Delimited): empid,hobby name,hobby description, hobby rating(numeric and less than 5)"
                    btnUploadFile.Attributes.Add("title", tooltips.ToLower)
                ElseIf cboUploadType.SelectedItem.Text = "Employee Asset" Then
                    tooltips = "CSV File (Comma Delimited): empid,asset name,asset number,asset description, asset classification,Physical Condition of asset, location,Status(Active,Inactive,Returned), Comments"
                    btnUploadFile.Attributes.Add("title", tooltips.ToLower)
                End If

                Process.AssignRadComboValue(cboCompany, Session("company"))
                If Session("empsearch") Is Nothing Then
                    Session("empsearch") = ""
                End If
                search.Value = Session("empsearch")

                LoadGrid()
                LinkClink(Session("empclicked"))
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub SortRecords(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Try
            Dim sortExpression As String = e.SortExpression
            Session("empsortExpression") = sortExpression
            Dim direction As String = String.Empty
            If SortsDirection = SortDirection.Ascending Then
                SortsDirection = SortDirection.Descending
                direction = " DESC"
            Else
                SortsDirection = SortDirection.Ascending
                direction = " ASC"
            End If
            Dim table As New DataTable
            table = LoadEmployeeGrid(1, 2000000)
            table.DefaultView.Sort = sortExpression & direction
            GridVwHeaderChckbox.PageIndex = Session("emppageindex")
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
            Session("emppageindex") = e.NewPageIndex
            GridVwHeaderChckbox.DataSource = LoadEmployeeGrid(1, 2000000)
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub GridVwHeaderChckbox_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridVwHeaderChckbox.RowCreated
        Try
            Process.SortArrow(e, SortsDirection, Session("empsortExpression"))
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub OnRowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridVwHeaderChckbox.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GridVwHeaderChckbox, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub

    Protected Sub btnFind_Click(sender As Object, e As EventArgs)  'Handles btnFind.Click
        Try
            'If Not Me.IsPostBack Then

            If search.Value.Trim Is Nothing Then
                Session("emploadtype") = "All"
            Else
                Session("emploadtype") = "Find"
            End If
            Session("empsearch") = search.Value.Trim
            Session("employeepageIndex") = "0"
            Session("emppagelow") = "1"
            Session("emppagehigh") = "100"
            LoadGrid()

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                Process.loadalert(divalert, msgalert, Process.privilegemsg, "warning")
                Exit Sub
            End If
            Response.Redirect("~/Module/Employee/EmployeeData", True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


    Private Class DeleteObj
        Public Property Id As Integer
        Public Property EmpId As String
        Public Property FirstName As String
        Public Property LastName As String
        Public Property JobGrade As String
        Public Property JobTitle As String
        Public Property Office As String
        Public Property CreatedBy As String
    End Class
    'Protected Sub Delete(sender As Object, e As EventArgs)
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
                        Dim ID As String = Convert.ToString(GridVwHeaderChckbox.DataKeys(row.RowIndex).Value)
                        Dim employeeDeleted As New DeleteObj()
                        Dim strUser As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_PersonalDetail_get", ID)
                        employeeDeleted.EmpId = strUser.Tables(0).Rows(0).Item("EmpID").ToString
                        employeeDeleted.FirstName = strUser.Tables(0).Rows(0).Item("FirstName")
                        employeeDeleted.LastName = strUser.Tables(0).Rows(0).Item("LastName")
                        employeeDeleted.JobGrade = strUser.Tables(0).Rows(0).Item("GradeLevel").ToString
                        employeeDeleted.JobTitle = strUser.Tables(0).Rows(0).Item("JobTitle").ToString
                        employeeDeleted.Office = strUser.Tables(0).Rows(0).Item("Office").ToString
                        employeeDeleted.CreatedBy = Session("LoginID")
                        Dim OldValue As String = ""
                        Dim NewValue As String = ""

                        Dim j As Integer = 0

                        For Each a In GetType(DeleteObj).GetProperties() 'New Entries
                            If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
                                If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                                    If a.GetValue(employeeDeleted, Nothing) = Nothing Then
                                        NewValue += a.Name + ":" + " " & vbCrLf
                                    Else
                                        NewValue += a.Name + ": " + a.GetValue(employeeDeleted, Nothing).ToString & vbCrLf
                                    End If
                                End If
                            End If
                        Next
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_PersonalDetail_delete", ID)
                        Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(NewValue, OldValue, "Deleted", "Employee Data Page")
                    End If
                Next
                Process.loadalert(divalert, msgalert, count.ToString & " records successfully deleted", "success")
                LoadGrid()

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Protected Sub DownloadTemplate(sender As Object, e As EventArgs)
        Try
            Dim sPath As String = Server.MapPath(sampleCSV)
            If cboUploadType.SelectedItem.Text = "Biodata" Then
                Response.AppendHeader("Content-Disposition", "attachment; filename=EmployeeMaster.csv")
                Response.TransmitFile(sPath & Convert.ToString("EmployeeMaster.csv"))
            ElseIf cboUploadType.SelectedItem.Text = "Work History" Then
                Response.AppendHeader("Content-Disposition", "attachment; filename=EmployeeWorkHistory.csv")
                Response.TransmitFile(sPath & Convert.ToString("EmployeeWorkHistory.csv"))
            ElseIf cboUploadType.SelectedItem.Text = "Contact Detail" Then
                Response.AppendHeader("Content-Disposition", "attachment; filename=ContactDetail.csv")
                Response.TransmitFile(sPath & Convert.ToString("ContactDetail.csv"))
            ElseIf cboUploadType.SelectedItem.Text = "Dependants" Then
                Response.AppendHeader("Content-Disposition", "attachment; filename=Dependants.csv")
                Response.TransmitFile(sPath & Convert.ToString("Dependants.csv"))
            ElseIf cboUploadType.SelectedItem.Text = "Emergency Contact" Then
                Response.AppendHeader("Content-Disposition", "attachment; filename=EmergencyContact.csv")
                Response.TransmitFile(sPath & Convert.ToString("EmergencyContact.csv"))
            ElseIf cboUploadType.SelectedItem.Text = "Education" Then
                Response.AppendHeader("Content-Disposition", "attachment; filename=Education.csv")
                Response.TransmitFile(sPath & Convert.ToString("Education.csv"))
            ElseIf cboUploadType.SelectedItem.Text = "Professional Qualification" Then
                Response.AppendHeader("Content-Disposition", "attachment; filename=ProfessionalQualification.csv")
                Response.TransmitFile(sPath & Convert.ToString("ProfessionalQualification.csv"))
            ElseIf cboUploadType.SelectedItem.Text = "Employess Hobbies" Then
                Response.AppendHeader("Content-Disposition", "attachment; filename=EmployeeHobbies.csv")
                Response.TransmitFile(sPath & Convert.ToString("EmployeeHobbies.csv"))
            ElseIf cboUploadType.SelectedItem.Text = "Employee Asset" Then
                Response.AppendHeader("Content-Disposition", "attachment; filename=Employeeasset.csv")
                Response.TransmitFile(sPath & Convert.ToString("EmployeeAsset.csv"))
            End If
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            HttpContext.Current.Response.Flush()
            HttpContext.Current.Response.SuppressContent = True
            HttpContext.Current.ApplicationInstance.CompleteRequest()
            Response.End()
            'Response.Flush()
            'Response.ContentType = "text/csv"
            'Response.End()
            ''Response.[End]()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) 'Handles btnUpload.Click
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                Process.loadalert(divalert, msgalert, Process.privilegemsg, "warning")

                Exit Sub
            End If


            If Not file1.PostedFile Is Nothing Then
                'System.Threading.Thread.Sleep(300)
                'To create a PostedFile
                Dim csvPath As String = Server.MapPath(emailFile) + Path.GetFileName(file1.PostedFile.FileName)
                If File.Exists(csvPath) = True Then
                    File.Delete(csvPath)
                End If
                file1.PostedFile.SaveAs(csvPath)
                Dim proc As String = ""
                If cboUploadType.SelectedItem.Text = "Biodata" Then
                    proc = "Emp_PersonalDetail_Upload"
                ElseIf cboUploadType.SelectedItem.Text = "Work History" Then
                    proc = "Emp_Work_History_Upload"
                ElseIf cboUploadType.SelectedItem.Text = "Contact Detail" Then
                    proc = "Emp_Contact_Info_Upload"
                ElseIf cboUploadType.SelectedItem.Text = "Dependants" Then
                    proc = "Emp_Dependents_Upload"
                ElseIf cboUploadType.SelectedItem.Text = "Emergency Contact" Then
                    proc = "Emp_Emergency_Contact_Upload"
                ElseIf cboUploadType.SelectedItem.Text = "Education" Then
                    proc = "Emp_Education_upload"
                ElseIf cboUploadType.SelectedItem.Text = "Professional Qualification" Then
                    proc = "Emp_Certifications_Upload"
                ElseIf cboUploadType.SelectedItem.Text = "Employess Hobbies" Then
                    proc = "Emp_Hobbies_Upload"
                ElseIf cboUploadType.SelectedItem.Text = "Employee Asset" Then
                    proc = "Emp_Asset_Upload"
                End If

                If cboUploadType.SelectedItem.Text = "Biodata" Then
                    If Process.Import1(csvPath, proc, Pages) = True Then
                        Process.loadalert(divalert, msgalert, "Uploaded " & Session("uploadcnt") & " record(s)", "success")
                        LoadGrid()
                    Else
                        Process.loadalert(divalert, msgalert, Session("exception"), "danger")
                    End If
                Else
                    If Process.Import(csvPath, proc, Pages) = True Then
                        Process.loadalert(divalert, msgalert, "Uploaded " & Session("uploadcnt") & " record(s)", "success")
                        LoadGrid()
                    Else
                        Process.loadalert(divalert, msgalert, Session("exception"), "danger")
                    End If
                End If
                Process.GetAuditTrailInsertandUpdate("", "Uploaded " & Session("uploadcnt") & " record(s) from " & file1.PostedFile.FileName, "File Upload", Pages)

            End If

            'Response.Write("<script language='javascript'> { self.close() }</script>")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Protected Sub btnExport_Click(sender As Object, e As EventArgs)
        Try
            'System.Threading.Thread.Sleep(300)
            Dim filename As String = ""
            Dim empIDList As String = ""

            Dim TitleHeader As String()
            Dim TitleData As String()

            Dim scripts As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Scripts_get", "employeeexport")
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
            Dim script As String

            Dim ismulti As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select isnull(ismulticompany,'No')  from general_info")
            If ismulti.ToLower = "no" Then
                script = "Select emp.EmpID, emp.FirstName,emp.MiddleName,emp.LastName, emp.Gender, emp.MaritalStatus,emp.Nationality,emp.DateOfBirth,emp.BloodGroup,emp.[State], emp.IDMethod,emp.IDNo,emp.IDExpiryDate,emp.IDIssuer,emp.CountryOfBirth,emp.PlaceOfBirth,emp.DateJoin,emp.HomeAddr,emp.PostalAddr,emp.CountryofResidence ,emp.MobileNo,emp.WorkPhone,emp.PersonalEmail,emp.Email,emp.ConfirmationDate from dbo.Employees_All emp"
            Else
                script = "Select emp.EmpID, emp.FirstName,emp.MiddleName,emp.LastName, emp.Gender, emp.MaritalStatus,emp.Nationality,emp.DateOfBirth,emp.BloodGroup,emp.[State], emp.IDMethod,emp.IDNo,emp.IDExpiryDate,emp.IDIssuer,emp.CountryOfBirth,emp.PlaceOfBirth,emp.DateJoin,emp.HomeAddr,emp.PostalAddr,emp.CountryofResidence ,emp.MobileNo,emp.WorkPhone,emp.PersonalEmail,emp.Email,emp.ConfirmationDate from dbo.Employees_All emp where emp.Office in (select companys from Fn_Company_Filter('" + cboCompany.SelectedValue + "'))"
            End If

            Dim strDataSet As New DataSet
            strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, script)
            Dim dataTables As DataTable = strDataSet.Tables(0)


            filename = search.Value.Trim & "_employee"
            If Process.ExportPayroll(dataTables, filename) = False Then
                Process.loadalert(divalert, msgalert, Session("exception"), "danger")
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & Process.strExp + "')", True)
            Else
                Response.Write("File saved as " & filename & ".xls")
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "File saved as " & filename & ".xls" + "')", True)
            End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Protected Sub cboUploadType_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboUploadType.SelectedIndexChanged
        Try

            Dim tooltips As String = ""
            If cboUploadType.SelectedItem.Text = "Biodata" Then
                tooltips = "CSV File (Comma Delimited): EMPID,FIRSTNAME,MIDDLENAME,LASTNAME,GENDER,MARITALSTATUS,NATIONALITY,DATE_OF_BIRTH,BLOODGROUP,STATE,IDENTIFICATION,IDNUMBER,IDEXPIRYDATE,IDD_ISSUER,COUNTRY_OF_BIRTH,PLACE_OF_BIRTH,ADDRESS,HOME ADDRESS,CITY,COUNTRY,MOBILE,OFFICEPHONE,PERSONALEMAIL,WORKEMAIL,DATEJOINED, CONFIRMATION_DATE,USERID,ROLE,ISSUPERUSER {Yes/No},ISHRADMIN {Yes/No}, ISFINADMIN {Yes/No}"
                btnUploadFile.Attributes.Add("title", tooltips.ToLower)
            ElseIf cboUploadType.SelectedItem.Text = "Work History" Then
                tooltips = "CSV File (Comma Delimited): EMPID,GRADE,JOBTITLE,JOBTYPE,SUPERVISOREMPIDID,COACHEMPID,OFFICE/DEPT,LOCATION,STARTMONTH e.g JAN, STARTYEAR, ENDMONTH e.g JAN or Present, ENDYEAR : 0 if ENDDATE is Present"
                'btnUpload.ToolTip = tooltips.ToLower
                btnUploadFile.Attributes.Add("title", tooltips.ToLower)
            ElseIf cboUploadType.SelectedItem.Text = "Contact Detail" Then
                tooltips = "CSV File (Comma Delimited): empid,address,city,country,phonenumber,personal emailadress,official emailaddress, official phonenumber"
                btnUploadFile.Attributes.Add("title", tooltips.ToLower)
            ElseIf cboUploadType.SelectedItem.Text = "Dependants" Then
                tooltips = "CSV File (Comma Delimited): empid,fullname,relationship, date of birth e.g 12-Jan-1998"
                btnUploadFile.Attributes.Add("title", tooltips.ToLower)
            ElseIf cboUploadType.SelectedItem.Text = "Emergency Contact" Then
                tooltips = "CSV File (Comma Delimited): empid,name1,address1, phonenumber1,relationship1,name2,address2, phonenumber2,relationship2, referee1name, referee1address, referee1phone,refereee1mail,yearsknown,referee2name, referee2address, referee2phone,refereee2mail,yearsknown"
                btnUploadFile.Attributes.Add("title", tooltips.ToLower)
            ElseIf cboUploadType.SelectedItem.Text = "Education" Then
                tooltips = "CSV File (Comma Delimited): empid,degree,institution, startdate e.g DEC, startyear, enddate e.g DEC, endyear"
                btnUploadFile.Attributes.Add("title", tooltips.ToLower)
            ElseIf cboUploadType.SelectedItem.Text = "Professional Qualification" Then
                tooltips = "CSV File (Comma Delimited): empid,qualification,institution, startdate e.g DEC, startyear, completedate e.g DEC, completeyear"
                btnUploadFile.Attributes.Add("title", tooltips.ToLower)
            ElseIf cboUploadType.SelectedItem.Text = "Employess Hobbies" Then
                tooltips = "CSV File (Comma Delimited): empid,hobby name,hobby description, hobby rating(numeric and less than 5)"
                btnUploadFile.Attributes.Add("title", tooltips.ToLower)
            ElseIf cboUploadType.SelectedItem.Text = "Employee Asset" Then
                tooltips = "CSV File (Comma Delimited): empid,asset name,asset number,asset description, asset classification,Physical Condition of asset, location,Status(Active,Inactive,Returned), Comments"
                btnUploadFile.Attributes.Add("title", tooltips.ToLower)
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect("~/Module/Recruitment/Terminations", True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub


    Protected Sub btnPicUpload_Click(sender As Object, e As EventArgs)
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False Then
                Process.loadalert(divalert, msgalert, Process.privilegemsg, "warning")

                Exit Sub
            End If

            'Dim confirmValue As String = Request.Form("confirm_value")
            Dim confirmValue As String = "Yes"
            If confirmValue = "Yes" Then
                'System.Threading.Thread.Sleep(300)
                Dim selectedfolder As String = "c:\Photo"
                Dim Arrays() As String
                ' fileName  is the file name
                Dim files As String() = Directory.GetFiles(selectedfolder)
                For i = 0 To files.Count - 1
                    Dim filePath As String = files(i).ToString
                    Dim filename As String = Path.GetFileName(filePath)

                    Dim fs As FileStream = New FileStream(filePath, FileMode.Open, FileAccess.Read)
                    Dim br As BinaryReader = New BinaryReader(fs)
                    Dim bytes As Byte() = br.ReadBytes(Convert.ToInt32(fs.Length))

                    br.Close()
                    fs.Close()

                    Arrays = filename.Split(Separator, StringSplitOptions.RemoveEmptyEntries)
                    Dim imageName As String = (Server.MapPath(emailFile) + "EmployeePhoto_" & Arrays(0).ToString & ".jpg")
                    If File.Exists(imageName) Then
                        File.Delete(imageName)
                    End If
                    Using Stream As New FileStream(imageName, FileMode.Create)
                        Stream.Write(bytes, 0, bytes.Length)
                    End Using
                    imageName = emailFile + "EmployeePhoto_" & Arrays(0).ToString & ".jpg"
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_PersonalDetail_update_Bulk", Arrays(0).ToString, imageName)
                    'SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_PersonalDetail_update_Bulk", Arrays(0).ToString, bytes)
                Next
                lblstatus = "Employee Photo successfully uploaded"
                Process.loadalert(divalert, msgalert, lblstatus, "success")
            Else
                lblstatus = "Bulk picture upload cancelled"
                Process.loadalert(divalert, msgalert, lblstatus, "info")
            End If



        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub cboCompany_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboCompany.SelectedIndexChanged
        Try
            search.Value = ""
            Session("LoadType") = "All"
            LoadGrid()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
End Class