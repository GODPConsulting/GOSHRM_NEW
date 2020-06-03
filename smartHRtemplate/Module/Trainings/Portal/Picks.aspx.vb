Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports GOSHRM.GOSHRM.GOSHRM.BO

Public Class Picks
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "JOBPOST"
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
    Dim Pages As String = ""

    Private Sub LoadData(ByVal grade As String, ByVal sessions As String)
        Try
            'sessions = Session("JobID")
            Dim sstable As DataTable = Process.SearchDataP2("Emp_Training_Application_Assessment_Pick", sessions, grade)
            gridTrainers.DataSource = sstable
            gridTrainers.AllowSorting = True
            gridTrainers.AllowPaging = True
            gridTrainers.DataBind()

            lblHeader.Text = grade & " Skills"


        Catch ex As Exception
            lblStatus.Text = ex.Message
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                LoadData(Session("varJobTitle"), Request.QueryString("session"))
            End If
        Catch ex As Exception
            lblStatus.Text = ex.Message
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub




    Protected Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Try
            Response.Write("<script language='javascript'> { self.close() }</script>")
        Catch ex As Exception
        End Try
    End Sub


    Protected Sub btnRepay0_Click(sender As Object, e As EventArgs)

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
    Protected Sub SortRecords(ByVal sender As Object, ByVal e As GridViewSortEventArgs) Handles gridTrainers.Sorting
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

            table = Process.SearchDataP2("Emp_Training_Application_Assessment_Pick", Request.QueryString("session"), Session("varJobTitle"))
            table.DefaultView.Sort = sortExpression & direction
            gridTrainers.DataSource = table
            gridTrainers.DataBind()
        Catch ex As Exception
            lblStatus.Text = ex.Message
        End Try
    End Sub

   

    Protected Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            'If Process.AuthenAction(Session("role"), AuthenCode, "Delete") = False Then
            '    ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
            '    Exit Sub
            'End If
            lblStatus.Text = ""
            Dim loops As Integer = 0
            Dim confirmValue As String = Request.Form("confirm_value")
            If confirmValue = "Yes" Then
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Training_Application_Assessment_Prepare", Request.QueryString("session"))
                ' Iterate through the Products.Rows property
                For Each row As GridViewRow In gridTrainers.Rows
                    ' Access the CheckBox
                    Dim cb As CheckBox = row.FindControl("chkEmp")
                    If cb IsNot Nothing AndAlso cb.Checked Then
                        loops = loops + 1
                        Dim ID As String = Convert.ToString(gridTrainers.DataKeys(row.RowIndex).Value)
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Training_Application_Assessment_Create", Request.QueryString("session"), ID)
                    End If
                Next
                If loops = 0 Then
                    lblStatus.Text = "all skills have been removed from your assessment"
                ElseIf loops = 1 Then
                    lblStatus.Text = loops & " skill successfully added "
                Else
                    lblStatus.Text = loops & " skills successfully added "
                End If
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Training_Application_Assessment_Delete_Unselect", Request.QueryString("session"))
            Else
                lblStatus.Text = "saved cancelled"
                Exit Sub
            End If
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & lblStatus.Text + "')", True)
            Response.Write("<script language='javascript'> { self.close() }</script>")
        Catch ex As Exception
            lblStatus.Text = ex.Message
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & lblStatus.Text + "')", True)
        End Try
    End Sub
End Class