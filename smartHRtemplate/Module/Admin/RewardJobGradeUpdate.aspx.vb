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
Public Class RewardJobGradeUpdate
    Inherits System.Web.UI.Page
    Dim course As New clsCourse
    Dim olddata(7) As String
    Dim AuthenCode As String = "rewards"
    Dim Pages As String = "Reward Bonuses"

    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    <WebMethod()>
    Private Sub ComponentsCheck()
        Try
            If abonustype.SelectedText.Contains("Calculated") Then
                lblcomponents.Visible = True
                radComponents.Visible = True
                lblstatus.Visible = True
                radStatus.Visible = True
                lblDropDownList2.Visible = True
                RadDropDownList2.Visible = True
            Else
                lblcomponents.Visible = False
                radComponents.Visible = False
                lblstatus.Visible = False
                radStatus.Visible = False
                lblDropDownList2.Visible = False
                RadDropDownList2.Visible = False
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub ComponentsCheck2()
        Try
            If RadDropDownList2.SelectedText.Contains("Fixed") Then
                lblflatpercentage.Visible = True
                aflatpercentage.Visible = True
            Else
                lblflatpercentage.Visible = False
                aflatpercentage.Visible = False

            End If
        Catch ex As Exception

        End Try

    End Sub
    Public Shared Function GetName(prefix As String) As String()
        Dim customers As New List(Of String)()
        Using conn As New SqlConnection()
            Dim terms As List(Of String) = prefix.Split(","c).ToList()
            terms = terms.Select(Function(s) s.Trim()).ToList()

            'Extract the term to be searched from the list
            Dim searchTerm As String = terms.LastOrDefault().ToString().Trim()

            'Return if Search Term is empty
            If String.IsNullOrEmpty(searchTerm) Then
                Return New String(-1) {}
            End If

            'Populate the terms that need to be filtered out
            Dim excludeTerms As New List(Of String)()
            If terms.Count > 1 Then
                terms.RemoveAt(terms.Count - 1)
                excludeTerms = terms
            End If

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("GOSHRMConnectionString").ConnectionString
            Using cmd As New SqlCommand()
                Dim query As String = "select FirstName, LastName from Emp_PersonalDetail where FirstName like @SearchText + '%'"

                'Filter out the existing searched items
                If excludeTerms.Count > 0 Then
                    query += String.Format(" and FirstName not in ({0})", String.Join(",", excludeTerms.[Select](Function(s) "'" + s + "'").ToArray()))
                End If
                cmd.CommandText = query
                cmd.Parameters.AddWithValue("@SearchText", searchTerm)
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        customers.Add(String.Format("{0} {1}", sdr("FirstName"), sdr("LastName")))
                    End While
                End Using
                conn.Close()
            End Using
        End Using
        Return customers.ToArray()
    End Function
    Protected Sub DrillDown(ByVal sender As Object, ByVal e As EventArgs)
        Try
            txtskillid.Text = CType(sender, LinkButton).CommandArgument
            Dim url As String = "courseskills?id=" & txtskillid.Text
            Response.Redirect(url, True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Sub LoadDynamic()
        Try
        Catch ex As Exception

        End Try
    End Sub
    Private Sub PanelVisibility()
        Try
            If RadDropDownList2.SelectedText.Contains("Performace Based") Then
                pnskill.Visible = True
            Else
                pnskill.Visible = False
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Function LoadDatatable() As DataTable
        Dim dt As New DataTable
        search.Value = Session("courseskillLoadsearch")

        dt = Process.SearchData("reward_performance_get_all", txtid.Text)

        Return dt
    End Function
    Private Sub LoadGrid(id As String)
        Try
            gridskills.PageIndex = CInt(Session("courseskillLoadindex"))
            gridskills.DataSource = LoadDatatable()
            gridskills.AllowSorting = True
            gridskills.DataBind()
            PanelVisibility()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                If Session("courseskillLoadindex") Is Nothing Then
                    Session("courseskillLoadindex") = "0"
                End If

                If Session("courseskillLoadsearch") Is Nothing Then
                    Session("courseskillLoadsearch") = ""
                End If

                radStatus.Items.Clear()
                radStatus.Items.Add("Yes")
                radStatus.Items.Add("No")

                abonustype.Items.Clear()
                abonustype.Items.Add("Calculated")
                abonustype.Items.Add("Uploaded")

                RadDropDownList2.Items.Clear()
                RadDropDownList2.Items.Add("Fixed")
                RadDropDownList2.Items.Add("Performace Based")

                RadDropDownList1.Items.Clear()
                RadDropDownList1.Items.Add("Yes")
                RadDropDownList1.Items.Add("No")


                Process.LoadRadComboTextAndValue(radComponents, "Finance_Monthly_Earning_Items_Get_All", "payslip item", "payslip item")
                Process.LoadRadDropDownTextAndValue(RadDropDownList3, "Job_Grade_get_all", "name", "name", False)
                If Request.QueryString("id") IsNot Nothing Then
                    Dim strUser As New DataSet
                    Dim strUser2 As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Reward_get", Request.QueryString("id"))
                    abonusname.Value = strUser.Tables(0).Rows(0).Item("BonusReward").ToString
                    Process.AssignRadDropDownValue(abonustype, strUser.Tables(0).Rows(0).Item("Type").ToString)
                    Process.AssignRadDropDownValue(RadDropDownList3, strUser.Tables(0).Rows(0).Item("jobgrade").ToString)

                    Dim Active = strUser.Tables(0).Rows(0).Item("Active")
                    If Active = True Then
                        Active = "Yes"
                    Else
                        Active = "No"
                    End If
                    RadDropDownList1.SelectedText = Active

                    aflatpercentage.Value = strUser.Tables(0).Rows(0).Item("FixedPercentage").ToString
                    Dim Taxable = strUser.Tables(0).Rows(0).Item("Taxable")
                    If Taxable = True Then
                        Taxable = "Yes"
                    Else
                        Taxable = "No"
                    End If
                    radStatus.SelectedText = Taxable

                    Process.AssignRadDropDownValue(RadDropDownList2, strUser.Tables(0).Rows(0).Item("BonusModel").ToString)
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    strUser2 = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Reward_Components_get", Request.QueryString("id"))
                    Process.AssignRadComboValue(radComponents, strUser2.Tables(0).Rows(0).Item("Components").ToString)
                    'aobjective.Value = strUser.Tables(0).Rows(0).Item("objectives").ToString
                    LoadGrid(txtid.Text)
                    PanelVisibility()


                Else
                    txtid.Text = "0"

                    PanelVisibility()
                End If
            End If
            ComponentsCheck()
            ComponentsCheck2()

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            Process.loadalert(divalert, msgalert, "", "danger")
            Dim lblstatus As String = ""
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                lblstatus = "You don't have privilege to perform this action"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                Exit Sub
            End If

            If abonusname.Value.Trim = "" Then
                lblstatus = "Bonus name is required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                abonusname.Focus()
                Exit Sub
            End If

            If (RadDropDownList1.SelectedText.Trim = "" Or RadDropDownList1.SelectedText.Trim.ToLower = "-- select --") Then
                lblstatus = "Active Status required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                radStatus.Focus()
                Exit Sub
            End If
            If (abonustype.SelectedText.Trim = "" Or abonustype.SelectedText.Trim.ToLower = "-- select --") Then
                lblstatus = "Choose bonus type!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                radStatus.Focus()
                Exit Sub
            End If


            If Request.QueryString("id") IsNot Nothing Then
                Dim strUser As New DataSet
                strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Courses_get", Request.QueryString("id"))
                olddata(0) = strUser.Tables(0).Rows(0).Item("id").ToString
                olddata(1) = strUser.Tables(0).Rows(0).Item("Code").ToString
                olddata(2) = strUser.Tables(0).Rows(0).Item("Name").ToString
                olddata(3) = strUser.Tables(0).Rows(0).Item("objectives").ToString
                olddata(4) = strUser.Tables(0).Rows(0).Item("currency").ToString
                olddata(5) = strUser.Tables(0).Rows(0).Item("cost").ToString
                olddata(6) = strUser.Tables(0).Rows(0).Item("Status").ToString
            End If


            If txtid.Text.Trim = "" Then
                course.id = 0
            Else
                course.id = txtid.Text
            End If

            'course.Code = acode.Value.Trim
            'course.Status = radStatus.SelectedText
            'course.Name = acoursetitle.Value.Trim
            ''course.Objective = aobjective.Value.Trim
            'course.Currency = radComponents .SelectedValue
            'course.Cost = acost.Value

            Dim OldValue As String = ""
            Dim NewValue As String = ""

            Dim j As Integer = 0

            If Request.QueryString("id") IsNot Nothing Then 'Updates
                For Each a In GetType(clsCourse).GetProperties()
                    If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If IsNumeric(a.GetValue(course, Nothing)) = True And IsNumeric(olddata(j)) = True Then
                                If CDbl(a.GetValue(course, Nothing)) <> CDbl(olddata(j)) Then
                                    NewValue += a.Name + ": " + a.GetValue(course, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            Else
                                If a.GetValue(course, Nothing).ToString <> olddata(j).ToString Then
                                    NewValue += a.Name + ": " + a.GetValue(course, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            End If
                        End If
                    End If
                    j = j + 1
                Next
            Else
                For Each a In GetType(clsCourse).GetProperties() 'New Entries
                    If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If a.GetValue(course, Nothing) = Nothing Then
                                NewValue += a.Name + ":" + " " & vbCrLf
                            Else
                                NewValue += a.Name + ": " + a.GetValue(course, Nothing).ToString & vbCrLf
                            End If
                        End If
                    End If
                Next
            End If
            Dim str = Guid.NewGuid.ToString()
            txtskillid.Text = str
            txtid.Text = GetIdentity()
            If RadDropDownList2 IsNot Nothing Then
                For d As Integer = 0 To radComponents.CheckedItems.Count - 1
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Reward_Component_Add", txtskillid.Text, radComponents.CheckedItems.Item(d).Text, txtid.Text)
                Next
            End If

            If NewValue.Trim = "" And OldValue.Trim = "" Then
            Else
                If Request.QueryString("id") IsNot Nothing Then
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Updated", "rewards")
                Else
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Inserted", "rewards")
                End If
            End If
            Process.loadalert(divalert, msgalert, "Record saved!", "success")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Function GetIdentity() As String
        Dim Active = RadDropDownList1.SelectedText
        If Active = "Yes" Then
            Active = True
        Else
            Active = False
        End If
        Dim Taxable = radStatus.SelectedText
        If Taxable = "Yes" Then
            Taxable = True
        Else
            Taxable = False
        End If

        Dim aPercentage As Decimal
        If RadDropDownList2.SelectedText.Contains("Performace Based") Then
            aPercentage = 0
        Else
            aPercentage = Decimal.Parse(aflatpercentage.Value.Trim)
        End If
        Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
        Dim con As New SqlConnection(strConnString)
        Dim cmd As New SqlCommand()
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "Job_grade_reward_update"
        cmd.Parameters.Add("@id", SqlDbType.Int).Value = txtid.Text
        cmd.Parameters.Add("@BonusReward", SqlDbType.VarChar).Value = abonusname.Value.ToUpper.Trim
        cmd.Parameters.Add("@Active", SqlDbType.Bit).Value = Active
        cmd.Parameters.Add("@Type", SqlDbType.VarChar).Value = abonustype.SelectedText
        cmd.Parameters.Add("@FixedPercentage", SqlDbType.Decimal).Value = aPercentage
        cmd.Parameters.Add("@BonusModel", SqlDbType.VarChar).Value = RadDropDownList2.SelectedText
        cmd.Parameters.Add("jobgrade", SqlDbType.VarChar).Value = RadDropDownList3.SelectedText
        cmd.Parameters.Add("@GUID", SqlDbType.VarChar).Value = txtskillid.Text
        cmd.Parameters.Add("@Taxable", SqlDbType.Bit).Value = Taxable
        cmd.Connection = con
        con.Open()
        Dim obj As Object = cmd.ExecuteScalar()
        Return obj.ToString()
    End Function
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            Session("courseskillLoadsearch") = ""
            Session("courseskillLoadindex") = "0"
            Response.Redirect("~/Module/Trainings/Settings/Courses", True)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Delete(sender As Object, e As EventArgs) Handles btDelete.Click
        Try
            Process.loadalert(divalert, msgalert, "", "warning")
            If Process.AuthenAction(Session("role"), AuthenCode, "Delete") = False Then
                Process.loadalert(divalert, msgalert, Process.privilegemsg, "warning")

                Exit Sub
            End If
            Dim count As Integer = 0
            Dim confirmValue As String = Request.Form("confirm_value")
            'If confirmValue = "Yes" Then
            Dim atLeastOneRowDeleted As Boolean = False
            ' Iterate through the Products.Rows property
            For Each row As GridViewRow In gridskills.Rows
                ' Access the CheckBox
                Dim cb As CheckBox = row.FindControl("chkEmp")
                If cb IsNot Nothing AndAlso cb.Checked Then
                    count = count + 1
                    ' Delete row! (Well, not really...)
                    atLeastOneRowDeleted = True
                    ' First, get the ProductID for the selected row
                    Dim ID As String =
                        Convert.ToString(gridskills.DataKeys(row.RowIndex).Value)
                    ' "Delete" the row
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Course_Skills_Delete", ID)
                End If
            Next
            Process.loadalert(divalert, msgalert, count.ToString & " records successfully deleted", "success")

            LoadGrid(txtid.Text)

            'End If
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

            If Not file1.PostedFile Is Nothing Then
                'To create a PostedFile
                Dim csvPath As String = Server.MapPath(Process.FileURL) + Path.GetFileName(file1.PostedFile.FileName)
                If File.Exists(csvPath) = True Then
                    File.Delete(csvPath)
                End If
                file1.PostedFile.SaveAs(csvPath)
                'Create byte Array with file len
                'File.ContentLength
                If Process.Import(csvPath, "CourseSkills_upload", Pages) = True Then
                    Process.loadalert(divalert, msgalert, "Uploaded " & Session("uploadcnt") & " record(s)", "success")
                    Process.GetAuditTrailInsertandUpdate("", "Uploaded " & Session("uploadcnt") & " record(s) from " & csvPath, "File Upload", Pages)
                    LoadGrid(txtid.Text)
                Else
                    Process.loadalert(divalert, msgalert, Session("exception"), "danger")
                End If
                If File.Exists(csvPath) = True Then
                    File.Delete(csvPath)
                End If
            Else
                Process.loadalert(divalert, msgalert, "No files selected to upload", "warning")
                file1.Focus()
                Exit Sub
            End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub
    Protected Sub btnExport_Click(sender As Object, e As EventArgs)
        Try

            If Process.ExportExcel(LoadDatatable(), "CourseSkills") = False Then
                Response.Output.Write(Process.strExp)
            End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub
    Protected Sub btnAddSkill_Click(sender As Object, e As EventArgs)
        Try
            Dim url As String = "PerformanceReward?rewardid=" & txtid.Text
            Response.Redirect(url, True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnFind_Click(sender As Object, e As EventArgs)
        Try
            'If Not Me.IsPostBack Then
            Session("courseskillLoadsearch") = search.Value.Trim
            LoadGrid(txtid.Text)
            'End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub
    Protected Sub SortRecords(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Try
            Dim sortExpression As String = e.SortExpression
            Session("courseskillsortExpression") = sortExpression
            Dim direction As String = String.Empty
            If SortsDirection = SortDirection.Ascending Then
                SortsDirection = SortDirection.Descending
                direction = " DESC"
            Else
                SortsDirection = SortDirection.Ascending
                direction = " ASC"
            End If
            gridskills.PageIndex = CInt(Session("courseskillLoadindex"))
            Dim table As DataTable = LoadDatatable()
            table.DefaultView.Sort = sortExpression & direction
            gridskills.DataSource = table
            gridskills.DataBind()
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

    Private Sub gridskills_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gridskills.PageIndexChanging
        Try
            gridskills.PageIndex = e.NewPageIndex
            Session("courseskillLoadindex") = e.NewPageIndex
            LoadGrid(txtid.Text)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gridskills_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridskills.RowCreated
        Try
            Process.SortArrow(e, SortsDirection, Session("courseskillsortExpression"))
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub abonustype_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.DropDownListEventArgs) Handles abonustype.SelectedIndexChanged
        ComponentsCheck()

    End Sub
    Protected Sub RadDropdownList2_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.DropDownListEventArgs) Handles RadDropDownList2.SelectedIndexChanged
        ComponentsCheck2()
        PanelVisibility()

    End Sub
End Class