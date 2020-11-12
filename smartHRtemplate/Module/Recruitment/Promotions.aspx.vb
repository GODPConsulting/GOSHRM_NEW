Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Imports Telerik.Web.UI
Imports System.Web.UI
Imports System.Threading


Public Class Promotions
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "ADMPROMOTION"
    Dim Pages As String = "Promotions"
    Dim PhotoURL As String = ConfigurationManager.AppSettings("PhotoURL")
    Dim FileURL As String = ConfigurationManager.AppSettings("FileURL")
    Dim sampleCSV As String = ConfigurationManager.AppSettings("sampleCSV")
    Public Shared Separator() As Char = {"."c}
    Private Function LoadEmployeeGrid() As DataTable
        Dim datatables As New DataTable
        search.Value = Session("promosearch")
        If search.Value.Trim = "" Then
            datatables = Process.SearchData("Recruitment_Promotion_Get_All", cboCompany.SelectedValue)
        Else
            datatables = Process.SearchDataP2("Recruitment_Promotion_Search", cboCompany.SelectedValue, search.Value.Trim)
        End If

        pagetitle.InnerText = cboCompany.SelectedValue & ": " & " Promotions (" & datatables.Rows.Count & ")"

        Return datatables
    End Function

    Private Sub LoadGrid()
        Try

            GridVwHeaderChckbox.PageIndex = CInt(Session("promoindex"))
            GridVwHeaderChckbox.DataSource = LoadEmployeeGrid()
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
                Dim ismulti As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select isnull(ismulticompany,'No')  from general_info")
                If ismulti.ToLower = "no" Then
                    Process.LoadRadComboTextAndValueP2(cboCompany, "Company_Structure_Get_ByLevel", "1", Session("Access"), "name", "name", False)
                    divcompany.Visible = False
                Else

                    Process.LoadRadComboTextAndValueP2(cboCompany, "Company_Structure_Get_ByLevel", "2", Session("Access"), "name", "name", False)
                End If

                If Session("promocompany") Is Nothing Then
                    Session("promocompany") = Session("Organisation")
                End If

                If Session("promosearch") Is Nothing Then
                    Session("promosearch") = ""
                End If

                If Session("promoindex") Is Nothing Then
                    Session("promoindex") = "0"
                End If
                Process.AssignRadComboValue(cboCompany, Session("promocompany"))

                LoadGrid()
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Protected Sub SortRecords(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Try
            Dim sortExpression As String = e.SortExpression
            Session("promosort") = sortExpression
            Dim direction As String = String.Empty
            If SortsDirection = SortDirection.Ascending Then
                SortsDirection = SortDirection.Descending
                direction = " DESC"
            Else
                SortsDirection = SortDirection.Ascending
                direction = " ASC"
            End If
            Dim table As New DataTable
            table = LoadEmployeeGrid()
            table.DefaultView.Sort = sortExpression & direction
            GridVwHeaderChckbox.PageIndex = CInt(Session("promoindex"))
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
            Session("promoindex") = e.NewPageIndex
            LoadGrid()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub GridVwHeaderChckbox_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridVwHeaderChckbox.RowCreated
        Try
            Process.SortArrow(e, SortsDirection, Session("promosort"))
        Catch ex As Exception
        End Try

    End Sub

    Protected Sub OnRowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridVwHeaderChckbox.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GridVwHeaderChckbox, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub

    Protected Sub btnFind_Click(sender As Object, e As EventArgs)
        Try
            Session("promosearch") = search.Value.Trim
            LoadGrid()
            'End If
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
            Response.Redirect("promotionsupdate?hr=1", True)
            
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Private Class DeleteObj
        Public Property Id As Integer
        Public Property EmpId As String
        Public Property Name As String
        Public Property Jobtitle As String
        Public Property JobGrade As String
        Public Property ApprovalStatus As String
        Public Property EffectiveDate As String
        Public Property CreatedBy As String
    End Class
    Protected Sub Delete(sender As Object, e As EventArgs) Handles btnDelete.Click
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
                        Dim promotionDeleted As New DeleteObj()
                        Dim strUser As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruitment_Promotion_Get", ID)
                        promotionDeleted.EmpId = strUser.Tables(0).Rows(0).Item("EmpID").ToString
                        promotionDeleted.Jobtitle = strUser.Tables(0).Rows(0).Item("jobtitle")
                        promotionDeleted.JobGrade = strUser.Tables(0).Rows(0).Item("jobgrade")
                        promotionDeleted.Name = strUser.Tables(0).Rows(0).Item("employee").ToString
                        promotionDeleted.ApprovalStatus = strUser.Tables(0).Rows(0).Item("finalstatus").ToString
                        promotionDeleted.EffectiveDate = strUser.Tables(0).Rows(0).Item("effectivedate").ToString
                        promotionDeleted.CreatedBy = Session("LoginID")
                        Dim OldValue As String = ""
                        Dim NewValue As String = ""

                        Dim j As Integer = 0

                        For Each a In GetType(DeleteObj).GetProperties() 'New Entries
                            If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
                                If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                                    If a.GetValue(promotionDeleted, Nothing) = Nothing Then
                                        NewValue += a.Name + ":" + " " & vbCrLf
                                    Else
                                        NewValue += a.Name + ": " + a.GetValue(promotionDeleted, Nothing).ToString & vbCrLf
                                    End If
                                End If
                            End If
                        Next
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruitment_Promotion_delete", ID)
                        Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(NewValue, OldValue, "Deleted", "Promotion Page")
                    End If
                Next
                Process.loadalert(divalert, msgalert, count.ToString & " records successfully deleted", "success")
                LoadGrid()
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub
    Private Sub cboCompany_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboCompany.SelectedIndexChanged
        Session("promocompany") = cboCompany.SelectedValue
        LoadGrid()
    End Sub
End Class