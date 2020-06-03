Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports Telerik.Web.UI
Public Class EmployeeCalendar
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "JOBPOST"

    Private Sub GtCalendarView()
        Try
            schTmeSheet.DataStartField = "Activity Date"
            schTmeSheet.DataSubjectField = "Activity"
            schTmeSheet.DataEndField = "Activity End Date"
            schTmeSheet.DataDescriptionField = "Project"
            schTmeSheet.DataKeyField = "id"

            If Request.QueryString("PID") Is Nothing Then
                schTmeSheet.DataSource = Process.SearchDataP2("Time_Sheet_Get_All_Calendar", Request.QueryString("EMPID"), "All Projects")
            Else
                'Time_Sheet_Get_All
                schTmeSheet.DataSource = Process.SearchDataP2("Time_Sheet_Get_All_Calendar", Request.QueryString("EMPID"), Request.QueryString("PID"))
            End If

            schTmeSheet.DataBind()
            schTmeSheet.SelectedView = SchedulerViewType.MonthView
            schTmeSheet.Visible = True
        Catch ex As Exception
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try


            If Not Me.IsPostBack Then
                If Session("PreviousPage2") Is Nothing Then
                    Session("PreviousPage2") = Request.UrlReferrer
                End If

                Dim strDataSet As New DataSet
                strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "select name from Employees_All where EmpID = '" & Request.QueryString("EMPID") & "'")
                If strDataSet.Tables(0).Rows.Count > 0 Then
                    If Request.QueryString("PID") Is Nothing Then
                        lblView.Text = "Timesheet Calendar for " & strDataSet.Tables(0).Rows(0).Item("Name").ToString
                    Else
                        Dim strProject As New DataSet
                        Dim projectname As String = ""
                        strProject = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Time_Projects_Get", Request.QueryString("PID"))
                        If strProject.Tables(0).Rows.Count > 0 Then
                            projectname = strProject.Tables(0).Rows(0).Item("Name").ToString
                        End If

                        lblView.Text = projectname & " Calendar for " & strDataSet.Tables(0).Rows(0).Item("Name").ToString
                    End If

                End If

                GtCalendarView()
                schTmeSheet.SelectedDate = Date.Now
            End If
        Catch ex As Exception

            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub



    'Protected Sub btnSystem_Click(sender As Object, e As EventArgs) Handles btnSystem.Click
    '    Try
    '        Response.Redirect("~/Module/Recruitment/MatchCandidates.aspx", True)
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            'Response.Write("<script language='javascript'> { self.close() }</script>")
            If Session("PreviousPage2") IsNot Nothing Then
                Response.Redirect(Session("PreviousPage2").ToString())
            Else
                Response.Write("<script language='javascript'> { self.close() }</script>")
            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub schTmeSheet_AppointmentClick(sender As Object, e As Telerik.Web.UI.SchedulerEventArgs) Handles schTmeSheet.AppointmentClick
        Try
            ScriptManager.RegisterStartupScript(Page, GetType(Page), "OpenWindow", "window.open('TimeSheetUpdate.aspx?id= " & e.Appointment.ID.ToString & "','mywindow','width=700,height=800');", True)
        Catch ex As Exception
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub
End Class