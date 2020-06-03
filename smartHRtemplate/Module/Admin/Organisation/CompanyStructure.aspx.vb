Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports Telerik.Web.UI
Imports System.IO

Public Class CompanyStructure
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "COMPSTRUCT"
    Dim Levels(10) As String
    Dim DefnLevels(10) As String
    Dim Pages As String = "Company Structure"
    Private Sub LoadChart()
        Try

            Dim NodeDataSet As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "StructureDefinition_get_all")
            For i As Integer = 0 To NodeDataSet.Tables(0).Rows.Count - 1
                DefnLevels(i) = NodeDataSet.Tables(0).Rows(i).Item("Definition").ToString()
                Levels(i) = NodeDataSet.Tables(0).Rows(i).Item("level").ToString()
            Next

            Dim Node1DataSet As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Company_Structure_get_struct", DefnLevels(0), "")
            For i As Integer = 0 To Node1DataSet.Tables(0).Rows.Count - 1
                Dim LevelNode1 As New OrgChartNode
                Dim LevelNode1RenderedField As New OrgChartRenderedField
                Dim LevelNode1GrpItem As New OrgChartGroupItem
                Dim LevelNode1GrpItemHead As New OrgChartRenderedField

                LevelNode1RenderedField.Text = Node1DataSet.Tables(0).Rows(i).Item("Structure Type").ToString() & ": " & Node1DataSet.Tables(0).Rows(i).Item("Name").ToString()
                Dim orgname As String = Node1DataSet.Tables(0).Rows(i).Item("Name").ToString()
                LevelNode1GrpItemHead.Text = "Head: " & Node1DataSet.Tables(0).Rows(i).Item("HeadName").ToString()
                LevelNode1GrpItem.ImageUrl = "C:\Photo.jpg"
                LevelNode1GrpItem.Text = "HHH"
                '"~/Module/Employee/ImgHandler.ashx?imgid=" & Node1DataSet.Tables(0).Rows(i).Item("head").ToString()
                '2nd Level
                Dim Node2DataSet As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Company_Structure_get_child", orgname)
                For a As Integer = 0 To Node2DataSet.Tables(0).Rows.Count - 1
                    Dim LevelNode2 As New OrgChartNode
                    Dim LevelNode2RenderedField As New OrgChartRenderedField
                    Dim LevelNode2GrpItem As New OrgChartGroupItem
                    Dim LevelNode2GrpItemHead As New OrgChartRenderedField

                    Dim level2org As String = Node2DataSet.Tables(0).Rows(a).Item("Name").ToString()
                    LevelNode2GrpItemHead.Text = "Head: " & Node2DataSet.Tables(0).Rows(a).Item("HeadName").ToString()
                    LevelNode2RenderedField.Text = Node2DataSet.Tables(0).Rows(a).Item("Structure Type").ToString() & ": " & Node2DataSet.Tables(0).Rows(a).Item("Name").ToString()
                    '3rd Level
                    Dim Node3DataSet As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Company_Structure_get_child", level2org)
                    For b As Integer = 0 To Node3DataSet.Tables(0).Rows.Count - 1
                        Dim LevelNode3 As New OrgChartNode
                        Dim LevelNode3RenderedField As New OrgChartRenderedField
                        Dim LevelNode3GrpItem As New OrgChartGroupItem
                        Dim LevelNode3GrpItemRenderField As New OrgChartRenderedField
                        LevelNode3GrpItemRenderField.Text = Node3DataSet.Tables(0).Rows(b).Item("Name").ToString()
                        LevelNode3RenderedField.Text = Node3DataSet.Tables(0).Rows(b).Item("Structure Type").ToString()
                        '4th Level
                        Dim Node4DataSet As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Company_Structure_get_child", LevelNode3GrpItemRenderField.Text)
                        For c As Integer = 0 To Node4DataSet.Tables(0).Rows.Count - 1
                            Dim LevelNode4 As New OrgChartNode
                            Dim LevelNode4RenderedField As New OrgChartRenderedField
                            Dim LevelNode4GrpItem As New OrgChartGroupItem
                            Dim LevelNode4GrpItemRenderField As New OrgChartRenderedField
                            LevelNode4GrpItemRenderField.Text = Node4DataSet.Tables(0).Rows(c).Item("Name").ToString()
                            LevelNode4RenderedField.Text = Node4DataSet.Tables(0).Rows(c).Item("Structure Type").ToString()
                            '5th Level
                            Dim Node5DataSet As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Company_Structure_get_child", LevelNode4GrpItemRenderField.Text)
                            For d As Integer = 0 To Node5DataSet.Tables(0).Rows.Count - 1
                                Dim LevelNode5 As New OrgChartNode
                                Dim LevelNode5RenderedField As New OrgChartRenderedField
                                Dim LevelNode5GrpItem As New OrgChartGroupItem
                                Dim LevelNode5GrpItemRenderField As New OrgChartRenderedField
                                LevelNode5GrpItemRenderField.Text = Node5DataSet.Tables(0).Rows(c).Item("Name").ToString()
                                LevelNode5RenderedField.Text = Node5DataSet.Tables(0).Rows(c).Item("Structure Type").ToString()
                                '6th Level
                                Dim Node6DataSet As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Company_Structure_get_child", LevelNode5GrpItemRenderField.Text)
                                For e As Integer = 0 To Node5DataSet.Tables(0).Rows.Count - 1
                                    Dim LevelNode6 As New OrgChartNode
                                    Dim LevelNode6RenderedField As New OrgChartRenderedField
                                    Dim LevelNode6GrpItem As New OrgChartGroupItem
                                    Dim LevelNode6GrpItemRenderField As New OrgChartRenderedField
                                    LevelNode6GrpItemRenderField.Text = Node6DataSet.Tables(0).Rows(c).Item("Name").ToString()
                                    LevelNode6RenderedField.Text = Node6DataSet.Tables(0).Rows(c).Item("Structure Type").ToString()
                                    '7th Level


                                    LevelNode6GrpItem.RenderedFields.Add(LevelNode6GrpItemRenderField)
                                    LevelNode6.RenderedFields.Add(LevelNode6RenderedField)
                                    LevelNode6.GroupItems.Add(LevelNode6GrpItem)
                                    LevelNode5.Nodes.Add(LevelNode6)
                                Next
                                LevelNode5GrpItem.RenderedFields.Add(LevelNode5GrpItemRenderField)
                                LevelNode5.RenderedFields.Add(LevelNode5RenderedField)
                                LevelNode5.GroupItems.Add(LevelNode5GrpItem)
                                LevelNode4.Nodes.Add(LevelNode5)
                            Next
                            LevelNode4GrpItem.RenderedFields.Add(LevelNode4GrpItemRenderField)
                            LevelNode4.RenderedFields.Add(LevelNode4RenderedField)
                            LevelNode4.GroupItems.Add(LevelNode4GrpItem)
                            LevelNode3.Nodes.Add(LevelNode4)
                        Next
                        LevelNode3GrpItem.RenderedFields.Add(LevelNode3GrpItemRenderField)
                        LevelNode3.RenderedFields.Add(LevelNode3RenderedField)
                        LevelNode3.GroupItems.Add(LevelNode3GrpItem)
                        LevelNode2.Nodes.Add(LevelNode3)
                    Next
                    LevelNode2GrpItem.RenderedFields.Add(LevelNode2GrpItemHead)
                    LevelNode2.RenderedFields.Add(LevelNode2RenderedField)
                    LevelNode2.GroupItems.Add(LevelNode2GrpItem)
                    LevelNode1.Nodes.Add(LevelNode2)
                Next

                LevelNode1GrpItem.RenderedFields.Add(LevelNode1GrpItemHead)
                LevelNode1.RenderedFields.Add(LevelNode1RenderedField)
                LevelNode1.GroupItems.Add(LevelNode1GrpItem)
                RadOrgChart1.Nodes.Add(LevelNode1)
            Next


            'For k As Integer = 1 To 5
            '    Dim NodeLevel2 As New OrgChartNode
            '    Dim NodeLevel1_2 As New OrgChartRenderedField

            '    Dim ssLevel2 As New OrgChartGroupItem
            '    Dim ssLevel1_2 As New OrgChartRenderedField

            '    ssLevel1_2.Text = "Fintrak Sub " & k
            '    NodeLevel1_2.Text = "Dept " & k

            '    ssLevel2.RenderedFields.Add(ssLevel1_2)
            '    NodeLevel2.RenderedFields.Add(NodeLevel1_2)
            '    NodeLevel2.GroupItems.Add(ssLevel2)

            '    LevelNode1.Nodes.Add(NodeLevel2)
            'Next



        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub
    Private Function LoadDatatable() As DataTable
        Dim dt As New DataTable
        If Session("LoadType") = "All" Then
            dt = Process.GetData("Company_Structure_get_all")
        ElseIf Session("LoadType") = "Find" Then
            dt = Process.SearchData("Company_Structure_search", search.Value.Trim)
        End If
        pagetitle.InnerText = radView.SelectedItem.Text & " (" & dt.Rows.Count.ToString & ")"
        Return dt
    End Function
    Private Sub LoadGrid(LoadType As String)
        Try
            '
            GridVwHeaderChckbox.DataSource = LoadDatatable()
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
                Process.loadalert(divalert, msgalert, Process.privilegemsg, "warning")

                Exit Sub
            End If

            If Not Me.IsPostBack Then
                MultiView1.ActiveViewIndex = 0
                Session("LoadType") = "All"
                LoadGrid("All")
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
            Dim table As DataTable = LoadDatatable()
            table.DefaultView.Sort = sortExpression & direction
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
            GridVwHeaderChckbox.DataSource = LoadDatatable()
            GridVwHeaderChckbox.DataBind()
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

            'If Not Me.IsPostBack Then
            If search.Value.Trim = "" Then
                Session("LoadType") = "All"
            Else
                Session("LoadType") = "Find"
            End If
            LoadGrid(Session("LoadType"))
            'End If
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Create") = False Then
                Process.loadalert(divalert, msgalert, Process.privilegemsg, "warning")

                Exit Sub
            End If
            Session("isnew") = "1"
            Response.Redirect("~/Module/Admin/Organisation/CompanyStructureUpdate", True)
            'Response.Write("<script language='javascript'> { popup = window.open(""CompanyStructureUpdate.aspx"" , ""Stone Details"", ""height=500,width=600,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no"");  popup.focus(); }</script>")
        Catch ex As Exception

        End Try
    End Sub



    Protected Sub radView_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.DropDownListEventArgs) Handles radView.SelectedIndexChanged
        Try

            MultiView1.ActiveViewIndex = CInt(radView.SelectedValue)
            If CInt(radView.SelectedValue) = 1 Then
                LoadChart()
            End If
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
                        ' Delete row! (Well, not really...)
                        atLeastOneRowDeleted = True
                        count = count + 1
                        ' First, get the ProductID for the selected row
                        Dim ID As String = _
                            Convert.ToString(GridVwHeaderChckbox.DataKeys(row.RowIndex).Value)
                        ' "Delete" the row
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Company_Structure_delete", ID)
                    End If
                Next
                Process.loadalert(divalert, msgalert, count.ToString & " records successfully deleted", "success")
                LoadGrid(Session("LoadType"))

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub



    Protected Sub RadOrgChart1_DrillDown(sender As Object, e As Telerik.Web.UI.OrgChartDrillDownEventArguments) Handles RadOrgChart1.DrillDown
        Try
            'Dim test As String = RadOrgChart1.
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnExport_Click(sender As Object, e As EventArgs)
        Try

            If Process.ExportExcel(LoadDatatable(), "organisation") = False Then
                Process.loadalert(divalert, msgalert, Session("exception"), "danger")
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

            If Not file1.PostedFile Is Nothing Then
                'To create a PostedFile
                Dim csvPath As String = Server.MapPath(Process.FileURL) + Path.GetFileName(file1.PostedFile.FileName)
                If File.Exists(csvPath) = True Then
                    File.Delete(csvPath)
                End If
                file1.PostedFile.SaveAs(csvPath)
                'Create byte Array with file len
                'File.ContentLength
                If Process.Import(csvPath, "Company_Structure_Upload", Pages) = True Then
                    Process.loadalert(divalert, msgalert, "Uploaded " & Session("uploadcnt") & " record(s)", "success")
                    Process.GetAuditTrailInsertandUpdate("", "Uploaded " & Session("uploadcnt") & " record(s) from " & csvPath, "File Upload", Pages)
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Company_Structure_Update_Company")
                Else
                    Process.loadalert(divalert, msgalert, Session("exception"), "danger")
                End If
                If File.Exists(csvPath) = True Then
                    File.Delete(csvPath)
                End If
                Session("LoadType") = "All"
                LoadGrid(Session("LoadType"))
            Else
                Process.loadalert(divalert, msgalert, "No file selected!", "warning")
            End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Private Sub GridVwHeaderChckbox_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridVwHeaderChckbox.RowCreated
        Try
            Process.SortArrow(e, SortsDirection, Session("sortExpression"))
        Catch ex As Exception

        End Try
    End Sub
End Class