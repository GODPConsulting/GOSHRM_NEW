Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports Telerik.Web.UI
Public Class MgrTrainings
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "EMPTRAINING"


    Private Function LoadDataTable() As DataTable
        Dim strDataSet As New DataTable

        search.Value = Session("trainingsearch")
        If Convert.ToString(Session("trainingyear")) = "" Then
            Session("trainingyear") = Date.Now.Year
        End If
        If Session("trainingview").ToString.ToLower = "as trainee" Then
            If Session("trainingsearch") = "" Then
                strDataSet = Process.SearchDataP3("Employee_Training_Sessions2_get_all", Session("Emp"), Process.DDMONYYYY(Process.FirstDay(Session("trainingyear"), 1)), Process.DDMONYYYY(Process.LastDay(Session("trainingyear"), 1)))
            Else
                strDataSet = Process.SearchDataP4("Employee_Training_Sessions2_Search", Session("Emp"), Process.DDMONYYYY(Process.FirstDay(Session("trainingyear"), 1)), Process.DDMONYYYY(Process.LastDay(Session("trainingyear"), 1)), search.Value)
            End If
        Else
            If Session("trainingsearch") = "" Then
                strDataSet = Process.SearchDataP3("Employee_Training_Trainer_get_all", Session("Emp"), Process.DDMONYYYY(Process.FirstDay(Session("trainingyear"), 1)), Process.DDMONYYYY(Process.LastDay(Session("trainingyear"), 1)))
            Else
                strDataSet = Process.SearchDataP4("Employee_Training_Trainer_Search", Session("Emp"), Process.DDMONYYYY(Process.FirstDay(Session("trainingyear"), 1)), Process.DDMONYYYY(Process.LastDay(Session("trainingyear"), 1)), search.Value)
            End If
        End If
        If cboyear.Items.Count > 0 Then
            'pagetitle.InnerText = "Training & Development " & cboview.SelectedItem.Text & " : " & cboyear.SelectedValue & "(" & FormatNumber(strDataSet.Rows.Count, 0) & ")"
            divdetailheader.InnerText = "Training & Development " & cboview.SelectedItem.Text & " : " & cboyear.SelectedValue & "(" & FormatNumber(strDataSet.Rows.Count, 0) & ")"
        Else
            'pagetitle.InnerText = "Training & Development " & cboview.SelectedItem.Text & " : (" & FormatNumber(strDataSet.Rows.Count, 0) & ")"
            divdetailheader.InnerText = "Training & Development " & cboview.SelectedItem.Text & " : " & cboyear.SelectedValue & "(" & FormatNumber(strDataSet.Rows.Count, 0) & ")"
        End If

        Return strDataSet
    End Function
    Private Sub LoadMyTrainings()
        Try
            'SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, script)

            dtTable = LoadDataTable()
            GridVwHeaderChckbox.PageIndex = CInt(Session("trainingindex"))
            GridVwHeaderChckbox.DataSource = dtTable 'Process.SearchDataP3("Finance_Salary_Get_All", radLocation.SelectedText, radLocation.SelectedValue, StGrade)
            GridVwHeaderChckbox.AllowSorting = True
            GridVwHeaderChckbox.AllowPaging = True
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub DrillDetail(id As String)
        Try
            Dim s As String = ""
            If Session("trainingview").ToString.ToLower = "as trainee" Then
                'Dim url As String = "EmployeeTrainingsUpdate.aspx?id=" & id
                's = "window.open('" & url + "', 'popup_window', 'width=700,height=800,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no');"
                'ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)
                Response.Redirect("~/Module/Employee/TrainingPortal/EmployeeTrainingsUpdate?id=" & id, True)
            Else
                'Dim url As String = "EmployeeTrainersUpdate.aspx?id=" & id
                's = "window.open('" & url + "', 'popup_window', 'width=700,height=600,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no');"
                'ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)
                Response.Redirect("~/Module/Employee/TrainingPortal/EmployeeTrainersUpdate?id=" & id, True)
            End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Read") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "danger")
                Exit Sub
            End If
            If Not Me.IsPostBack Then

                If Request.QueryString("id") IsNot Nothing Then
                    Session("Emp") = Request.QueryString("id")
                    cboview.Items.Clear()
                    cboview.Items.Add("As Trainee")
                    cboview.Items.Add("As Instructor")

                    If Session("trainingsearch") Is Nothing Then
                        Session("trainingsearch") = ""
                    End If

                    If Session("trainingindex") Is Nothing Then
                        Session("trainingindex") = "0"
                    End If

                    If Session("trainingview") Is Nothing Then
                        Session("trainingview") = cboview.SelectedItem.Text
                    Else
                        Process.AssignRadComboValue(cboview, Session("trainingview"))
                    End If
                    Dim years As String = ""

                    Dim DefaultYear As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Employee_Training_Sessions2_Years", Session("userempid"))
                    Dim k As Integer
                    If DefaultYear.Tables(0).Rows.Count > 0 Then
                        k = DefaultYear.Tables(0).Rows.Count
                        If k > 0 Then
                            For i As Integer = 0 To k - 1
                                years = Convert.ToString(DefaultYear.Tables(0).Rows(i)("Year"))
                            Next
                        End If

                    End If


                    If cboview.SelectedItem.Text = "As Trainee" Then
                        Process.LoadRadComboTextAndValueP1(cboyear, "Employee_Training_Sessions2_Years", Session("userempid"), "year", "year", False)
                        Session("trainingyear") = years
                    Else
                        Process.LoadRadComboTextAndValueP1(cboyear, "Employee_Training_Trainer_Years", Session("userempid"), "year", "year", False)

                    End If

                    If cboyear.Items.Count > 0 Then
                        If Session("trainingyear") Is Nothing Then
                            Session("trainingyear") = cboyear.SelectedValue
                        Else
                            Process.AssignRadComboValue(cboyear, Session("trainingyear"))
                        End If
                    Else
                        Session("trainingyear") = Date.Now.Year
                    End If
                    Process.AssignRadComboValue(cboyear, Session("trainingyear"))
                    LoadMyTrainings()

                Else
                    Process.loadalert(divalert, msgalert, "No employee was selected", "danger")
                End If

                
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub SortRecords(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Try
            Dim sortExpression As String = e.SortExpression
            Session("trainingsort") = sortExpression
            Dim direction As String = String.Empty
            If SortsDirection = SortDirection.Ascending Then
                SortsDirection = SortDirection.Descending
                direction = " DESC"
            Else
                SortsDirection = SortDirection.Ascending
                direction = " ASC"
            End If
            Dim table As New DataTable
            table = LoadDataTable()
            table.DefaultView.Sort = sortExpression & direction
            GridVwHeaderChckbox.DataSource = table
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
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
            Dim table As New DataTable
            'SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, script)
            table = LoadDataTable()
            Session("trainingindex") = e.NewPageIndex
            GridVwHeaderChckbox.PageIndex = e.NewPageIndex
            GridVwHeaderChckbox.DataSource = table
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Private Sub GridVwHeaderChckbox_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridVwHeaderChckbox.RowCommand
        Try
            If (e.CommandName = "DrillDown") Then
                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                DrillDetail(index)
            End If
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub btnFind_Click(sender As Object, e As EventArgs)
        Try
            'If Not Me.IsPostBack Then

            If search.Value.Trim = "" Then
                Session("trainingsearch") = ""
            Else
                Session("trainingsearch") = search.Value
            End If

            LoadMyTrainings()
            'End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Private Sub cboyear_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboyear.SelectedIndexChanged
        Try

            Session("trainingsearch") = ""

            Session("trainingindex") = "0"
            Session("trainingyear") = cboyear.SelectedValue
            LoadMyTrainings()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub cboView_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboview.SelectedIndexChanged
        Try
            Session("trainingsearch") = ""
            Session("trainingindex") = "0"
            Session("trainingview") = cboview.SelectedItem.Text
            If cboview.SelectedItem.Text = "As Trainee" Then
                Process.LoadRadComboTextAndValueP1(cboyear, "Employee_Training_Sessions2_Years", Session("userempid"), "year", "year", False)
            Else
                Process.LoadRadComboTextAndValueP1(cboyear, "Employee_Training_Trainer_Years", Session("userempid"), "year", "year", False)
            End If
            LoadMyTrainings()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Private Sub GridVwHeaderChckbox_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridVwHeaderChckbox.RowCreated
        Try
            Process.SortArrow(e, SortsDirection, Session("trainingsort"))
        Catch ex As Exception

        End Try
    End Sub
End Class