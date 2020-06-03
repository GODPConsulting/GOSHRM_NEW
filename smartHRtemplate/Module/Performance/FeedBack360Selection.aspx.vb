Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Telerik.Web.UI

Public Class FeedBack360Selection
    Inherits System.Web.UI.Page
    Dim skills As New clsSkills
    Dim AuthenCode As String = "APP360FEEDBACK"
    Dim olddata(3) As String
    Dim lblempid As String
    Private Sub LoadGrid(id As String)
        Try
            GridVwHeaderChckbox.DataSource = Process.SearchData("Performance_Appraisal_360_Stat", id)
            GridVwHeaderChckbox.AllowSorting = True
            GridVwHeaderChckbox.AllowPaging = True
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                Session("PreviousPage") = Request.UrlReferrer.ToString
                Process.LoadRadComboTextAndValueP2(cboReviewer, "Emp_PersonalDetail_get_all", "", Process.GetCompanyName, "Employee2", "Employee No", False)
                If Request.QueryString("id") IsNot Nothing Then
                    lblid.Text = Request.QueryString("id")
                    lblempid = Request.QueryString("empid")

                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_PersonalDetail_get", lblempid)
                    If strUser.Tables(0).Rows.Count > 0 Then
                        pagetitle.InnerText = strUser.Tables(0).Rows(0).Item("last name").ToString & " " & strUser.Tables(0).Rows(0).Item("first name").ToString & "'s 360 Degree Feedback Reviewers"
                    End If
                    Process.LoadListAndComboxFromDataset(lstReviewers, cboReviewer, "Performance_Appraisal_360_Get_Reviewers", "employee2", "empid", Request.QueryString("id"))
                    LoadGrid(Request.QueryString("id"))
                End If
                'Company_Structure_get_parent
            Else
                If Request.QueryString("id") IsNot Nothing Then
                    lblid.Text = Request.QueryString("id")
                    lblempid = Request.QueryString("empid")
                End If
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub



    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Dim url As String = Session("PreviousPage")
            Response.Redirect(url)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


    'Protected Sub OnRowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridVwHeaderChckbox.RowDataBound
    '    Try
    '        If e.Row.RowType = DataControlRowType.DataRow Then
    '            Dim quantity As Double = Double.Parse(e.Row.Cells(2).Text)

    '            For Each cell As TableCell In e.Row.Cells
    '                If quantity = 0 Then
    '                    'cell.BackColor = Color.Red
    '                    e.Row.Cells(2).BackColor = Color.Red
    '                ElseIf quantity > 0 AndAlso quantity <= 99.9 Then
    '                    'cell.BackColor = Color.Yellow
    '                    e.Row.Cells(2).BackColor = Color.Yellow
    '                ElseIf quantity > 99.9 AndAlso quantity <= 100 Then
    '                    'cell.BackColor = Color.LawnGreen
    '                    e.Row.Cells(2).BackColor = Color.LawnGreen
    '                Else
    '                    'cell.BackColor = Color.Red
    '                    e.Row.Cells(2).BackColor = Color.Red
    '                End If

    '            Next
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Protected Sub cboReviewer_ItemChecked(sender As Object, e As Telerik.Web.UI.RadComboBoxItemEventArgs) Handles cboReviewer.ItemChecked
    '    Try
    '        Process.LoadListBoxFromCombo(lstReviewers, cboReviewer)
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Process.LoadListBoxFromCombo(lstReviewers, cboReviewer)
            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_360_Delete_Stat", lblid.Text)
            Dim app_id As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "Performance_Appraisal_Get_period", lblid.Text)
            If lstReviewers.Items.Count > 0 Then
                Dim collection As IList(Of RadComboBoxItem) = cboReviewer.CheckedItems
                If (collection.Count <> 0) Then
                    For Each item As RadComboBoxItem In collection
                        Process.Appraisal_360_Notification(app_id, lblempid, item.Value, Process.ApplicationURL + "/" + Process.GetMailLink(AuthenCode, 3))
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_360_Update_Reviewer", lblid.Text, lblempid, item.Value, Session("UserEmpID"), Session("LoginID"))
                    Next
                End If
            End If
            LoadGrid(lblid.Text)
            Process.loadalert(divalert, msgalert, "Record saved!", "success")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnselect_Click(sender As Object, e As EventArgs) Handles btnselect.Click
        Try
            Process.LoadListBoxFromCombo(lstReviewers, cboReviewer)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
End Class