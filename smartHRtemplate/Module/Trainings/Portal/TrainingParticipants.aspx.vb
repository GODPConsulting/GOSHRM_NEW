Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports GOSHRM.GOSHRM.GOSHRM.BO

Public Class TrainingParticipants
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "TRAINING"
    Dim marketrate As Double = 0
    Dim interestrate As Double = 0
    Dim monthlypay As Double
    Dim loanamount As Double = 0
    Dim tenor As Integer = 0
    Dim fairvalue As Double = 0
    Dim EIR As Double = 0
    Dim AmortEIR As Double = 0
    Dim AmortFairValue As Double = 0
    Dim repaystartdate As Date
    Dim EMPID As String = ""
    Dim LoanType As String = ""
    Dim trainingapproval As New clsEmpTraningApproval
    Dim olddata(3) As String
    Dim Pages As String = "Employee Training Sessions"

    Private Function GetTable(ByVal sessions As String) As DataTable
        Dim DD As New DataTable
        search.Value = Session("trainingparticipartsearch")
        If search.Value.Trim = "" Then
            DD = Process.SearchDataP2("Trainee_Training_Sessions_get_all", sessions, Session("UserEmpID"))
        Else
            DD = Process.SearchDataP3("Trainee_Training_Sessions_Search", sessions, Session("UserEmpID"), search.Value.Trim)
        End If

        Return DD
    End Function
    Private Sub LoadData(ByVal sessions As String)
        Try
            gridTrainers.DataSource = Process.SearchData("Trainer_Training_Sessions_get_all", sessions)
            gridTrainers.AllowSorting = False
            gridTrainers.AllowPaging = False
            gridTrainers.DataBind()
            pagetitle.InnerText = sessions

            gridTrainees.PageIndex = CInt(Session("trainingparticipartindex"))
            gridTrainees.DataSource = GetTable(sessions)
            gridTrainees.AllowSorting = True
            gridTrainees.AllowPaging = True
            gridTrainees.DataBind()

            Dim strUser As New DataSet
            strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Training_Sessions_get", sessions)
            If strUser.Tables(0).Rows.Count > 0 Then
                pagetitle.InnerText = strUser.Tables(0).Rows(0).Item("Name").ToString
            End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                If Session("trainingparticipartsearch") Is Nothing Then
                    Session("trainingparticipartsearch") = ""
                End If

                If Session("trainingparticipartindex") Is Nothing Then
                    Session("trainingparticipartindex") = "0"
                End If
                LoadData(Request.QueryString("id"))


            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub SortRecords(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Try
            Dim sortExpression As String = e.SortExpression
            Session("trainingparticipartsort") = sortExpression
            Dim direction As String = String.Empty
            If SortsDirection = SortDirection.Ascending Then
                SortsDirection = SortDirection.Descending
                direction = " DESC"
            Else
                SortsDirection = SortDirection.Ascending
                direction = " ASC"
            End If
            Dim table As DataTable = GetTable(Request.QueryString("id"))
            table.DefaultView.Sort = sortExpression & direction
            gridTrainees.PageIndex = CInt(Session("trainingparticipartindex"))
            gridTrainees.DataSource = table
            gridTrainees.DataBind()
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


    Protected Sub btnClose_Click(sender As Object, e As EventArgs)
        Try
            ' Response.Write("<script language='javascript'> { self.close() }</script>")
            Response.Redirect("~/Module/Employee/TrainingPortal/DirectReportTrainings.aspx", True)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gridTrainees_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gridTrainees.PageIndexChanging
        Try
            Session("trainingparticipartindex") = e.NewPageIndex
            LoadData(Request.QueryString("id"))
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btnFind_Click(sender As Object, e As EventArgs)
        Try
            Session("trainingparticipartsearch") = search.Value.Trim
            LoadData(Request.QueryString("id"))
            'End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gridTrainees_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridTrainees.RowCreated
        Try
            Process.SortArrow(e, SortsDirection, Session("trainingparticipartsort"))
        Catch ex As Exception
        End Try

    End Sub

    Private Sub gridTrainees_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gridTrainees.RowDataBound
        Try
            Dim chcount As HtmlGenericControl = e.Row.FindControl("datscore")
            Dim datprogress As HtmlGenericControl = e.Row.FindControl("datprogress")
            Dim htmlclass As String = ""
            Dim htmlstyle As String = ""
            Dim htmltitle As String = ""
            If IsNumeric(chcount.InnerText.Replace("%", "")) Then
                If (CInt(chcount.InnerText.Replace("%", "")) > 69) Then
                    htmlclass = "progress-bar progress-bar-success"
                ElseIf (CInt(chcount.InnerText.Replace("%", "")) > 49) And (CInt(chcount.InnerText.Replace("%", "")) <= 69) Then
                    htmlclass = "progress-bar progress-bar-info"
                ElseIf (CInt(chcount.InnerText.Replace("%", "")) > 39) And (CInt(chcount.InnerText.Replace("%", "")) <= 49) Then
                    htmlclass = "progress-bar progress-bar-warning"
                Else
                    htmlclass = "progress-bar progress-bar-danger"
                End If


                htmlstyle = "width:" + chcount.InnerText
                htmltitle = chcount.InnerText + " training accomplishment"
                datprogress.Attributes.Add("class", htmlclass)
                datprogress.Attributes.Add("style", htmlstyle)
                datprogress.Attributes.Add("title", htmltitle)
            Else
                datprogress.Visible = False
                chcount.Visible = False
            End If


        Catch ex As Exception

        End Try
    End Sub
End Class