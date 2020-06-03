Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Public Class Education
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "EDUCARE"
    Dim Pages As String = "Academic Qualification"
    Dim Pages2 As String = "Academic Discipline"
    Dim Pages3 As String = "Academic grade"
    Dim Pages4 As String = "O Level Subject"
    Dim Pages5 As String = "O Level Grade"
    Private Sub LinkClink(ByVal ViewIndex As String)
        Try
            MultiView1.ActiveViewIndex = ViewIndex
            Process.deactivatehtmlmenu(btnAQualification)
            Process.deactivatehtmlmenu(btnADiscipline)
            Process.deactivatehtmlmenu(btnAGrade)
            Process.deactivatehtmlmenu(btnLevelGrade)
            Process.deactivatehtmlmenu(btnLevelSubject)

            Select Case ViewIndex
                Case "0"
                    Process.activatehtmlmenu(btnAQualification)
                Case "1"
                    Process.activatehtmlmenu(btnADiscipline)
                Case "2"
                    Process.activatehtmlmenu(btnAGrade)
                Case "3"
                    Process.activatehtmlmenu(btnLevelSubject)
                Case "4"
                    Process.activatehtmlmenu(btnLevelGrade)
                Case Else
                    MultiView1.ActiveViewIndex = 0
                    Process.activatehtmlmenu(btnAQualification)
            End Select
        Catch ex As Exception

        End Try
    End Sub
    Private Function LoadDataQualification() As DataTable
        Dim datas As New DataTable
        If Session("LoadType") = "All" Then
            datas = Process.GetData("education_get_all")
        ElseIf Session("LoadType") = "Find" Then
            datas = Process.SearchData("education_search", searchmenu1.Value.Trim)
        End If
        pagetitlemenu1.InnerText = "Academic Qualifications (" & datas.Rows.Count.ToString & ")"
        Return datas
    End Function
    Private Function LoadDataDiscipline() As DataTable
        Dim datas As New DataTable
        If Session("LoadType") = "All" Then
            datas = Process.GetData("Recruit_Academic_Discipline_get_all")
        ElseIf Session("LoadType") = "Find" Then
            datas = Process.SearchData("Recruit_Academic_Discipline_search", searchmenu2.Value.Trim)
        End If
        pagetitlemenu2.InnerText = "Academic Disciplines (" & datas.Rows.Count.ToString & ")"
        Return datas
    End Function
    Private Function LoadDataAGrade() As DataTable
        Dim datas As New DataTable
        If Session("LoadType") = "All" Then
            datas = Process.GetData("Recruit_Academic_Grade_Get_All")
        ElseIf Session("LoadType") = "Find" Then
            datas = Process.SearchData("Recruit_Academic_Grade_Search", searchmenu3.Value.Trim)
        End If
        pagetitlemenu3.InnerText = "Academic Grades (" & datas.Rows.Count.ToString & ")"
        Return datas
    End Function
    Private Function LoadDataOLSubject() As DataTable
        Dim datas As New DataTable
        If Session("LoadType") = "All" Then
            datas = Process.GetData("Recruit_OLevel_Subject_Get_All")
        ElseIf Session("LoadType") = "Find" Then
            datas = Process.SearchData("Recruit_OLevel_Subject_Search", searchmenu4.Value.Trim)
        End If
        pagetitlemenu4.InnerText = "Secondary School Subjects (" & datas.Rows.Count.ToString & ")"
        Return datas
    End Function
    Private Function LoadDataOLGrade() As DataTable
        Dim datas As New DataTable
        If Session("LoadType") = "All" Then
            datas = Process.GetData("Recruit_OLevel_Grade_Get_All")
        ElseIf Session("LoadType") = "Find" Then
            datas = Process.SearchData("Recruit_OLevel_Grade_Search", searchmenu5.Value.Trim)
        End If
        pagetitlemenu5.InnerText = "Secondary School Grade (" & datas.Rows.Count.ToString & ")"
        Return datas
    End Function
    Private Sub LoadGridQualification(LoadType As String)
        Try

            GridVwHeaderChckbox.PageIndex = Session("pageIndex1")
            GridVwHeaderChckbox.DataSource = LoadDataQualification()
            GridVwHeaderChckbox.AllowSorting = True
            GridVwHeaderChckbox.AllowPaging = True
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception

            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Sub LoadGridDiscipline()
        Try
            gridDiscipline.PageIndex = Session("pageIndex2")
            gridDiscipline.DataSource = LoadDataDiscipline()
            gridDiscipline.AllowSorting = True
            gridDiscipline.AllowPaging = True
            gridDiscipline.DataBind()
        Catch ex As Exception

            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Sub LoadGridAgrade()
        Try
            gridAcademicGrade.PageIndex = Session("pageIndex3")
            gridAcademicGrade.DataSource = LoadDataAGrade()
            gridAcademicGrade.AllowSorting = True
            gridAcademicGrade.AllowPaging = True
            gridAcademicGrade.DataBind()
        Catch ex As Exception

            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Sub LoadGridOLSubject()
        Try
            gridOLSubject.PageIndex = Session("pageIndex4")
            gridOLSubject.DataSource = LoadDataOLSubject()
            gridOLSubject.AllowSorting = True
            gridOLSubject.AllowPaging = True
            gridOLSubject.DataBind()
        Catch ex As Exception

            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Sub LoadGridOLGrade()
        Try
            gridOLGrade.PageIndex = Session("pageIndex5")
            gridOLGrade.DataSource = LoadDataOLGrade()
            gridOLGrade.AllowSorting = True
            gridOLGrade.AllowPaging = True
            gridOLGrade.DataBind()
        Catch ex As Exception

            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not Me.IsPostBack Then
                'qualification
                Dim tooltips As String = "CSV File: Name, Description, Rank"
                btnUploadMenu1.Attributes.Add("title", tooltips.ToLower)

                'discipline
                tooltips = "CSV File: Name"
                btnUploadMenu2.Attributes.Add("title", tooltips.ToLower)

                'academic grades
                tooltips = "CSV File: Name, Description, Rank"
                btnUploadMenu3.Attributes.Add("title", tooltips.ToLower)

                'high school subjects
                tooltips = "CSV File: Name"
                btnUploadMenu4.Attributes.Add("title", tooltips.ToLower)

                'high school grade
                tooltips = "CSV File: Name, Description, Rank"
                btnUploadMenu5.Attributes.Add("title", tooltips.ToLower)

                Session("pageIndex1") = 0
                Session("pageIndex2") = 0
                Session("pageIndex3") = 0
                Session("pageIndex4") = 0
                Session("pageIndex5") = 0
                If Session("LoadType") Is Nothing Then
                    Session("LoadType") = "All"
                End If

                If Session("clicked") Is Nothing Then
                    Session("clicked") = "0"
                End If
                LinkClink(Session("clicked"))
                LoadGridQualification(Session("LoadType"))
                LoadGridDiscipline()
                LoadGridAgrade()
                LoadGridOLSubject()
                LoadGridOLGrade()

            End If

        Catch ex As Exception

            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub SortRecords(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Try           
            Dim sortExpression As String = e.SortExpression
            Session("sortExpression") = sortExpression
            Dim direction As String = String.Empty
            If SortsDirection = SortDirection.Ascending Then
                SortsDirection = SortDirection.Descending
                direction = " DESC"
            Else
                SortsDirection = SortDirection.Ascending
                direction = " ASC"
            End If

            Session("clicked") = "0"
            LinkClink(Session("clicked"))
            Dim table As DataTable = LoadDataQualification()
            GridVwHeaderChckbox.PageIndex = Session("pageIndex1")
            table.DefaultView.Sort = sortExpression & direction
            GridVwHeaderChckbox.DataSource = table
            GridVwHeaderChckbox.DataBind()
            
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gridDiscipline_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridDiscipline.RowCreated
        Try
            Process.SortArrow(e, SortsDirectionDiscipline, Session("sortExpression2"))
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub SortRecordsDiscipline(ByVal sender As Object, ByVal e As GridViewSortEventArgs) Handles gridDiscipline.Sorting
        Try
            'Session("clicked") = "1"
            Dim sortExpression As String = e.SortExpression
            Session("sortExpression2") = sortExpression
            Dim direction As String = String.Empty
            If SortsDirectionDiscipline = SortDirection.Ascending Then
                SortsDirectionDiscipline = SortDirection.Descending
                direction = " DESC"
            Else
                SortsDirectionDiscipline = SortDirection.Ascending
                direction = " ASC"
            End If
            Session("clicked") = "1"
            LinkClink(Session("clicked"))
            Dim table As DataTable = LoadDataDiscipline()
            table.DefaultView.Sort = sortExpression & direction
            gridDiscipline.PageIndex = Session("pageIndex2")
            gridDiscipline.DataSource = table
            gridDiscipline.DataBind()
            
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gridAcademicGrade_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridAcademicGrade.RowCreated
        Try
            Process.SortArrow(e, SortsDirectionAGrade, Session("sortExpression3"))
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub SortRecordsAGrade(ByVal sender As Object, ByVal e As GridViewSortEventArgs) Handles gridAcademicGrade.Sorting
        Try
            Dim sortExpression As String = e.SortExpression
            Session("sortExpression3") = sortExpression
            Dim direction As String = String.Empty
            If SortsDirectionAGrade = SortDirection.Ascending Then
                SortsDirectionAGrade = SortDirection.Descending
                direction = " DESC"
            Else
                SortsDirectionAGrade = SortDirection.Ascending
                direction = " ASC"
            End If
            Session("clicked") = "2"
            LinkClink(Session("clicked"))
            Dim table As DataTable = LoadDataAGrade()
            table.DefaultView.Sort = sortExpression & direction
            gridAcademicGrade.PageIndex = Session("pageIndex3")
            gridAcademicGrade.DataSource = table
            gridAcademicGrade.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gridOLSubject_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridOLSubject.RowCreated
        Try
            Process.SortArrow(e, SortsDirectionSSubject, Session("sortExpression4"))
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub SortRecordsOLSubject(ByVal sender As Object, ByVal e As GridViewSortEventArgs) Handles gridOLSubject.Sorting
        Try
            Dim sortExpression As String = e.SortExpression
            Session("sortExpression4") = sortExpression
            Dim direction As String = String.Empty
            If SortsDirectionSSubject = SortDirection.Ascending Then
                SortsDirectionSSubject = SortDirection.Descending
                direction = " DESC"
            Else
                SortsDirectionSSubject = SortDirection.Ascending
                direction = " ASC"
            End If
            Session("clicked") = "3"
            LinkClink(Session("clicked"))
            Dim table As DataTable = LoadDataOLSubject()
            table.DefaultView.Sort = sortExpression & direction
            gridOLSubject.PageIndex = Session("pageIndex4")
            gridOLSubject.DataSource = table
            gridOLSubject.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gridOLGrade_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridOLGrade.RowCreated
        Try
            Process.SortArrow(e, SortsDirectionSGrade, Session("sortexpression5"))
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub SortRecordsOLGrade(ByVal sender As Object, ByVal e As GridViewSortEventArgs) Handles gridOLGrade.Sorting
        Try
            Dim sortExpression As String = e.SortExpression
            Session("sortExpression5") = sortExpression
            Dim direction As String = String.Empty
            If SortsDirectionSGrade = SortDirection.Ascending Then
                SortsDirectionSGrade = SortDirection.Descending
                direction = " DESC"
            Else
                SortsDirectionSGrade = SortDirection.Ascending
                direction = " ASC"
            End If
            Session("clicked") = "4"
            LinkClink(Session("clicked"))
            Dim table As DataTable = LoadDataOLGrade()
            table.DefaultView.Sort = sortExpression & direction
            gridOLGrade.PageIndex = Session("pageIndex5")
            gridOLGrade.DataSource = table
            gridOLGrade.DataBind()
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
    Public Property SortsDirectionDiscipline() As SortDirection
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
    Public Property SortsDirectionAGrade() As SortDirection
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
    Public Property SortsDirectionSSubject() As SortDirection
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
    Public Property SortsDirectionSGrade() As SortDirection
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
            GridVwHeaderChckbox.DataSource = LoadDataQualification()
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub gridDiscipline_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gridDiscipline.PageIndexChanging
        Try
            gridDiscipline.PageIndex = e.NewPageIndex
            Session("pageIndex2") = e.NewPageIndex
            gridDiscipline.DataSource = LoadDataDiscipline()
            gridDiscipline.DataBind()
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub gridAcademicGrade_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gridAcademicGrade.PageIndexChanging
        Try
            gridAcademicGrade.PageIndex = e.NewPageIndex
            Session("pageIndex3") = e.NewPageIndex
            gridAcademicGrade.DataSource = LoadDataAGrade()
            gridAcademicGrade.DataBind()
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub gridOLSubject_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gridOLSubject.PageIndexChanging
        Try
            gridOLSubject.PageIndex = e.NewPageIndex
            Session("pageIndex4") = e.NewPageIndex
            gridOLSubject.DataSource = LoadDataOLSubject()
            gridOLSubject.DataBind()
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub gridOLGrade_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gridOLGrade.PageIndexChanging
        Try
            gridOLGrade.PageIndex = e.NewPageIndex
            Session("pageIndex5") = e.NewPageIndex
            gridOLGrade.DataSource = LoadDataOLSubject()
            gridOLGrade.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub GridVwHeaderChckbox_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridVwHeaderChckbox.RowCreated
        Try
            Process.SortArrow(e, SortsDirection, Session("sortExpression"))
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub OnRowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridVwHeaderChckbox.RowDataBound
        Dim row As GridViewRow = e.Row
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GridVwHeaderChckbox, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
            row.Cells(3).Text = row.Cells(3).Text.Replace(vbCrLf, "<br />")
        End If
    End Sub
    Protected Sub gridDiscipline_OnRowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gridDiscipline.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gridDiscipline, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub
    Protected Sub gridAcademicGrade_OnRowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gridAcademicGrade.RowDataBound
        Dim row As GridViewRow = e.Row
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gridAcademicGrade, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
            row.Cells(3).Text = row.Cells(3).Text.Replace(vbCrLf, "<br />")
        End If
    End Sub
    Protected Sub gridOLSubject_OnRowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gridOLSubject.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gridOLSubject, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub
    Protected Sub gridOLGrade_OnRowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gridOLGrade.RowDataBound
        Dim row As GridViewRow = e.Row
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gridOLGrade, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
            row.Cells(3).Text = row.Cells(3).Text.Replace(vbCrLf, "<br />")
        End If
    End Sub

    Protected Sub btnFindMenu1_Click(sender As Object, e As EventArgs)
        Try
            If searchmenu1.Value.Trim = "" Then
                Session("LoadType") = "All"
            Else
                Session("LoadType") = "Find"
            End If
            LoadGridQualification("Find")
            'End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub btnSearchAcademicGrade_Click(sender As Object, e As EventArgs)
        Try
            If searchmenu3.Value.Trim = "" Then
                Session("LoadType") = "All"
            Else
                Session("LoadType") = "Find"
            End If
            LoadGridAgrade()
            'End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub btnSearchOLSubject_Click(sender As Object, e As EventArgs)
        Try
            If searchmenu4.Value.Trim = "" Then
                Session("LoadType") = "All"
            Else
                Session("LoadType") = "Find"
            End If
            LoadGridOLSubject()
            'End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub btnsearchOLGrade_Click(sender As Object, e As EventArgs)
        Try
            If searchmenu5.Value.Trim = "" Then
                Session("LoadType") = "All"
            Else
                Session("LoadType") = "Find"
            End If
            LoadGridOLGrade()
            'End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub btnAddMenu1_Click(sender As Object, e As EventArgs)
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "warning")
                Exit Sub
            End If
            Response.Redirect("~/Module/Admin/EducationUpdate.aspx", True)
            'Response.Write("<script language='javascript'> { popup = window.open(""EducationUpdate.aspx"" , ""Stone Details"", ""height=400,width=600,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no"");  popup.focus(); }</script>")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub btnAddDiscipline_Click(sender As Object, e As EventArgs)
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "warning")

                Exit Sub
            End If
            Response.Redirect("~/Module/Admin/DisciplineUpdate.aspx", True)

        Catch ex As Exception

            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub btnAddAcademicGrade_Click(sender As Object, e As EventArgs)
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "warning")
                Exit Sub
            End If
            Response.Redirect("~/Module/Admin/AcademicGradeUpdate.aspx", True)

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub btnAddOLSubject_Click(sender As Object, e As EventArgs)
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "warning")

                Exit Sub
            End If
            Response.Redirect("~/Module/Admin/OLSubjectUpdate.aspx", True)

        Catch ex As Exception

            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub btnAddOLGrade_Click(sender As Object, e As EventArgs)
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "warning")

                Exit Sub
            End If
            Response.Redirect("~/Module/Admin/OLgradeUpdate.aspx", True)

        Catch ex As Exception

            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub Delete_Education(sender As Object, e As EventArgs) Handles btnDeletemenu1.Click
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Delete") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "warning")
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
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "education_delete", ID)
                    End If
                Next
                Process.loadalert(divalert, msgalert, count.ToString & " records successfully deleted", "success")
                LoadGridQualification(Session("LoadType"))

            End If
        Catch ex As Exception

            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub btnDelAcademicGrade_Click(sender As Object, e As EventArgs) Handles btnDeleteMenu3.Click
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Delete") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "warning")
                Exit Sub
            End If
            Dim count As Integer = 0
            Dim confirmValue As String = Request.Form("confirm_value")
            If confirmValue = "Yes" Then
                Dim atLeastOneRowDeleted As Boolean = False
                ' Iterate through the Products.Rows property
                For Each row As GridViewRow In gridAcademicGrade.Rows
                    ' Access the CheckBox
                    Dim cb As CheckBox = row.FindControl("chkEmp")
                    If cb IsNot Nothing AndAlso cb.Checked Then
                        count = count + 1
                        ' Delete row! (Well, not really...)
                        atLeastOneRowDeleted = True
                        ' First, get the ProductID for the selected row
                        Dim ID As String = Convert.ToString(gridAcademicGrade.DataKeys(row.RowIndex).Value)
                        ' "Delete" the row
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Academic_Grade_Delete", ID)
                    End If
                Next
                Process.loadalert(divalert, msgalert, count.ToString & " records successfully deleted", "success")
                LoadDataAGrade()
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub btnDelDiscipline_Click(sender As Object, e As EventArgs) Handles btnDeleteMenu2.Click
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Delete") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "warning")
                Exit Sub
            End If
            Dim count As Integer = 0
            Dim confirmValue As String = Request.Form("confirm_value")
            If confirmValue = "Yes" Then
                Dim atLeastOneRowDeleted As Boolean = False
                ' Iterate through the Products.Rows property
                For Each row As GridViewRow In gridDiscipline.Rows
                    ' Access the CheckBox
                    Dim cb As CheckBox = row.FindControl("chkEmp")
                    If cb IsNot Nothing AndAlso cb.Checked Then
                        count = count + 1
                        ' Delete row! (Well, not really...)
                        atLeastOneRowDeleted = True
                        ' First, get the ProductID for the selected row
                        Dim ID As String = _
                            Convert.ToString(gridDiscipline.DataKeys(row.RowIndex).Value)
                        ' "Delete" the row
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Academic_Discipline_delete", ID)
                    End If
                Next
                Process.loadalert(divalert, msgalert, count.ToString & " records successfully deleted", "success")
                LoadGridDiscipline()

            End If
        Catch ex As Exception

            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub btnDelOLSubject_Click(sender As Object, e As EventArgs) Handles btnDeleteMenu4.Click
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Delete") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "warning")

                Exit Sub
            End If
            Dim count As Integer = 0
            Dim confirmValue As String = Request.Form("confirm_value")
            If confirmValue = "Yes" Then
                Dim atLeastOneRowDeleted As Boolean = False
                ' Iterate through the Products.Rows property
                For Each row As GridViewRow In gridOLSubject.Rows
                    ' Access the CheckBox
                    Dim cb As CheckBox = row.FindControl("chkEmp")
                    If cb IsNot Nothing AndAlso cb.Checked Then
                        count = count + 1
                        ' Delete row! (Well, not really...)
                        atLeastOneRowDeleted = True
                        ' First, get the ProductID for the selected row
                        Dim ID As String = _
                            Convert.ToString(gridOLSubject.DataKeys(row.RowIndex).Value)
                        ' "Delete" the row
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_OLevel_Subject_delete", ID)
                    End If
                Next
                Process.loadalert(divalert, msgalert, count.ToString & " records successfully deleted", "success")
                LoadGridOLSubject()

            End If
        Catch ex As Exception

            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub btnDelOLGrade_Click(sender As Object, e As EventArgs) Handles btnDeleteMenu5.Click
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Delete") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "warning")
                Exit Sub
            End If
            Dim count As Integer = 0
            Dim confirmValue As String = Request.Form("confirm_value")
            If confirmValue = "Yes" Then
                Dim atLeastOneRowDeleted As Boolean = False
                ' Iterate through the Products.Rows property
                For Each row As GridViewRow In gridOLGrade.Rows
                    ' Access the CheckBox
                    Dim cb As CheckBox = row.FindControl("chkEmp")
                    If cb IsNot Nothing AndAlso cb.Checked Then
                        count = count + 1
                        ' Delete row! (Well, not really...)
                        atLeastOneRowDeleted = True
                        ' First, get the ProductID for the selected row
                        Dim ID As String = _
                            Convert.ToString(gridOLGrade.DataKeys(row.RowIndex).Value)
                        ' "Delete" the row
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_OLevel_grade_delete", ID)
                    End If
                Next
                Process.loadalert(divalert, msgalert, count.ToString & " records successfully deleted", "success")
                LoadGridOLGrade()

            End If
        Catch ex As Exception

            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


    Protected Sub btnExportMenu1_Click(sender As Object, e As EventArgs)
        Try

            If Process.Export(GridVwHeaderChckbox, "Qualification", 1, 2) = False Then
                Process.loadalert(divalert, msgalert, Session("exception"), "danger")
            End If


        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub btnExportDiscipline_Click(sender As Object, e As EventArgs)
        Try
            If Process.Export(gridDiscipline, "Discipline", 1, 2) = False Then
                Process.loadalert(divalert, msgalert, Session("exception"), "danger")
            End If

        Catch ex As Exception

            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub btnExpAcademicGrade_Click(sender As Object, e As EventArgs)
        Try
            If Process.Export(gridAcademicGrade, "AcademicGrade", 1, 2) = False Then
                Process.loadalert(divalert, msgalert, Session("exception"), "danger")
            End If

        Catch ex As Exception

            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub btnExpOLSubject_Click(sender As Object, e As EventArgs)
        Try
            If Process.Export(gridOLSubject, "OLSubject", 1, 2) = False Then
                Process.loadalert(divalert, msgalert, Session("exception"), "danger")
            End If

        Catch ex As Exception

            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub btnExpOLGrade_Click(sender As Object, e As EventArgs)
        Try
            If Process.Export(gridOLGrade, "OLGrade", 1, 2) = False Then
                Process.loadalert(divalert, msgalert, Session("exception"), "danger")
            End If

        Catch ex As Exception

            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub btnUploadMenu1_Click(sender As Object, e As EventArgs)
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "warning")

                Exit Sub
            End If


            If Not filemenu1.PostedFile Is Nothing Then
                'To create a PostedFile
                Dim csvPath As String = Server.MapPath(Process.FileURL) + Path.GetFileName(filemenu1.PostedFile.FileName)
                If File.Exists(csvPath) = True Then
                    File.Delete(csvPath)
                End If
                filemenu1.PostedFile.SaveAs(csvPath)


                If Process.Import(csvPath, "education_upload", Pages) = True Then
                    Process.loadalert(divalert, msgalert, "Uploaded " & Session("uploadcnt") & " record(s)", "success")
                    Process.GetAuditTrailInsertandUpdate("", "Uploaded " & Session("uploadcnt") & " record(s) from " & csvPath, "File Upload", Pages)
                Else
                    Process.loadalert(divalert, msgalert, Session("exception"), "danger")
                End If
                If File.Exists(csvPath) = True Then
                    File.Delete(csvPath)
                End If
            Else
                Process.loadalert(divalert, msgalert, "No files selected to upload", "warning")
                filemenu1.Focus()
            End If
            LoadGridQualification(Session("LoadType"))
        Catch ex As Exception

            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub btnUploadDiscipline_Click(sender As Object, e As EventArgs)
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "warning")

                Exit Sub
            End If

            If filemenu2.PostedFile Is Nothing Then
                'To create a PostedFile
                Dim csvPath As String = Server.MapPath(Process.FileURL) + Path.GetFileName(filemenu2.PostedFile.FileName)
                If File.Exists(csvPath) = True Then
                    File.Delete(csvPath)
                End If
                filemenu2.PostedFile.SaveAs(csvPath)

                If Process.Import(csvPath, "Recruit_Academic_Discipline_Upload", Pages2) = True Then
                    Process.loadalert(divalert, msgalert, "Uploaded " & Session("uploadcnt") & " record(s)", "success")
                    Process.GetAuditTrailInsertandUpdate("", "Uploaded " & Session("uploadcnt") & " record(s) from " & csvPath, "File Upload", Pages2)
                Else
                    Process.loadalert(divalert, msgalert, Session("exception"), "danger")
                End If
                If File.Exists(csvPath) = True Then
                    File.Delete(csvPath)
                End If
            Else
                Process.loadalert(divalert, msgalert, "No file selected!", "warning")
            End If
            LoadGridDiscipline()
        Catch ex As Exception

            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub btnUploadAcademicGrade_Click(sender As Object, e As EventArgs)
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "warning")
                Exit Sub
            End If

            If Not filemenu3.PostedFile Is Nothing Then
                'To create a PostedFile
                Dim csvPath As String = Server.MapPath(Process.FileURL) + Path.GetFileName(filemenu3.PostedFile.FileName)
                If File.Exists(csvPath) = True Then
                    File.Delete(csvPath)
                End If
                filemenu3.PostedFile.SaveAs(csvPath)

                If Process.Import(csvPath, "Recruit_Academic_Grade_Upload", Pages3) = True Then
                    Process.loadalert(divalert, msgalert, "Uploaded " & Session("uploadcnt") & " record(s)", "success")
                    LoadGridAgrade()
                Else
                    Process.loadalert(divalert, msgalert, Session("exception"), "danger")
                    Exit Sub
                End If
                If File.Exists(csvPath) = True Then
                    File.Delete(csvPath)
                End If
                Process.GetAuditTrailInsertandUpdate("", "Uploaded " & Session("uploadcnt") & " record(s) from " & csvPath, "File Upload", Pages3)
            Else
                Process.loadalert(divalert, msgalert, "No file selected!", "warning")
            End If

        Catch ex As Exception

            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub btnUploadOLSubject_Click(sender As Object, e As EventArgs)
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "warning")

                Exit Sub
            End If

            If Not filemenu4.PostedFile Is Nothing Then
                'To create a PostedFile
                Dim csvPath As String = Server.MapPath(Process.FileURL) + Path.GetFileName(filemenu4.PostedFile.FileName)
                If File.Exists(csvPath) = True Then
                    File.Delete(csvPath)
                End If
                filemenu4.PostedFile.SaveAs(csvPath)

                If Process.Import(csvPath, "Recruit_OLevel_Subject_Upload", Pages4) = True Then
                    Process.loadalert(divalert, msgalert, "Uploaded " & Session("uploadcnt") & " record(s)", "success")
                    Process.GetAuditTrailInsertandUpdate("", "Uploaded " & Session("uploadcnt") & " record(s) from " & csvPath, "File Upload", Pages4)
                    LoadGridOLSubject()
                Else
                    Process.loadalert(divalert, msgalert, Session("exception"), "danger")
                End If
                If File.Exists(csvPath) = True Then
                    File.Delete(csvPath)
                End If
            Else
                Process.loadalert(divalert, msgalert, "No file selected!", "warning")
            End If

        Catch ex As Exception

            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub btnUploadOLgrade_Click(sender As Object, e As EventArgs)
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "warning")
                Exit Sub
            End If

            If Not filemenu5.PostedFile Is Nothing Then
                'To create a PostedFile
                Dim csvPath As String = Server.MapPath(Process.FileURL) + Path.GetFileName(filemenu5.PostedFile.FileName)
                If File.Exists(csvPath) = True Then
                    File.Delete(csvPath)
                End If
                filemenu5.PostedFile.SaveAs(csvPath)

                If Process.Import(csvPath, "Recruit_OLevel_grade_Upload", Pages5) = True Then
                    Process.loadalert(divalert, msgalert, "Uploaded " & Session("uploadcnt") & " record(s)", "success")
                    LoadGridOLSubject()
                    Process.GetAuditTrailInsertandUpdate("", "Uploaded " & Session("uploadcnt") & " record(s) from " & csvPath, "File Upload", Pages5)
                Else
                    Process.loadalert(divalert, msgalert, Session("exception"), "danger")
                End If
                If File.Exists(csvPath) = True Then
                    File.Delete(csvPath)
                End If
            Else
                Process.loadalert(divalert, msgalert, "No file selected!", "warning")
            End If

        Catch ex As Exception

            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub btnsearchdiscipline_Click(sender As Object, e As EventArgs)
        Try
            If searchmenu2.Value.Trim = "" Then
                Session("LoadType") = "All"
            Else
                Session("LoadType") = "Find"
            End If
            LoadGridDiscipline()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnAcademicQualification_Click(sender As Object, e As EventArgs)
        LinkClink(0)
    End Sub

    Protected Sub btnAcademicDiscipline_Click(sender As Object, e As EventArgs)
        LinkClink(1)
    End Sub

    Protected Sub btnAcademicGrade_Click(sender As Object, e As EventArgs)
        LinkClink(2)
    End Sub

    Protected Sub btnOLevelSubject_Click(sender As Object, e As EventArgs)
        LinkClink(3)
    End Sub

    Protected Sub btnOLevelGrade_Click(sender As Object, e As EventArgs)
        LinkClink(4)
    End Sub
End Class