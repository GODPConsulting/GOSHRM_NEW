Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Configuration
Imports Microsoft.Reporting.WebForms
Imports System.IO
Imports Telerik.Web.UI
Public Class AttendanceReport
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "ATTENDANCEREPORT"

    Private Sub LodaDataTable(dept As String, datstart As String, datend As String, views As String, includeabsent As String)
        Dim i As Integer = 0
        Dim IDList As String = ""
        Dim criterias As String = ""
        Dim Datas As New DataTable
        Dim strDataSet As New DataSet
        Dim scripts As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "scripts_get", "attendance_report")
        scripts = scripts.Replace("@startdate", datstart).Replace("@enddate", datend).Replace("@office", dept)
        Dim collection As IList(Of RadComboBoxItem) = cboValue.CheckedItems
        If (collection.Count <> 0) Then
            For Each item As RadComboBoxItem In collection
                Dim listitem As New RadListBoxItem()
                If i = 0 Then
                    IDList = "'" & item.Value.Replace(",", "") & "'"
                Else
                    IDList = IDList & "," & "'" & item.Value.Replace(",", "") & "'"
                End If
                i = i + 1
            Next
        End If
        If IDList = "" Then
            IDList = "''"
        End If

        If views.ToLower = "employee" Then
            criterias = " and emp.EmpID in (@empid)"
            scripts = scripts & criterias.Replace("@empid", IDList)
        Else
            criterias = " and emp.grade in (@grade)"
            scripts = scripts & criterias.Replace("@grade", IDList)
        End If


        strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, scripts)
        Datas = strDataSet.Tables(0)

        'Datas = Process.SearchDataP5("Time_Employee_Attendance_Report", dept, datstart, datend, views, includeabsent)
        Generatereport(Datas)
    End Sub
    Private Sub Generatereport(dt As DataTable)
        ReportViewer1.ProcessingMode = ProcessingMode.Local
        ReportViewer1.SizeToReportContent = True
        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Module/Reports/TimeManagement/AttendanceReport.rdlc")
        Dim _rsource As New ReportDataSource("Attendance", dt)
        Dim reportParameter As ReportParameter() = New ReportParameter(3) {}
        reportParameter(0) = New ReportParameter("company", cboCompany.SelectedValue)
        reportParameter(1) = New ReportParameter("dept", cboDept.SelectedValue)
        reportParameter(2) = New ReportParameter("daterange", Process.DDMONYYYY(datStart.SelectedDate) & " - " & Process.DDMONYYYY(datEnd.SelectedDate))
        reportParameter(3) = New ReportParameter("person", cboValue.SelectedValue)
        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.LocalReport.SetParameters(reportParameter)
        ReportViewer1.LocalReport.DataSources.Add(_rsource)
        ReportViewer1.LocalReport.Refresh()
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                cboCriteria.Items.Clear()
                cboCriteria.Items.Add("Employee")
                cboCriteria.Items.Add("Job Grade")

                Dim ismulti As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select isnull(ismulticompany,'No')  from general_info")
                If ismulti.ToLower = "no" Then
                    Process.LoadRadComboTextAndValueP2(cboCompany, "Company_Structure_Get_ByLevel", "1", Session("Access"), "name", "name", False)
                Else
                    Process.LoadRadComboTextAndValueP2(cboCompany, "Company_Structure_Get_ByLevel", "2", Session("Access"), "name", "name", False)
                End If

                Process.AssignRadComboValue(cboCompany, Session("Organisation"))
                If Process.IsHRManager(Session("UserEmpID")) = True Or Process.IsFinance(Session("UserEmpID")) = True Or Process.IsAdmin(Session("LoginID")) = True Then
                    Process.LoadRadComboTextAndValueP1(cboDept, "Company_Parent_Breakdown", cboCompany.SelectedValue, "companys", "companys", False)
                Else
                    cboCompany.Enabled = False
                    Process.LoadRadComboTextAndValueP1(cboDept, "Company_Parent_Breakdown", Session("Dept"), "companys", "companys", False)
                End If

                'lblQuery.Text = cboCriteria.SelectedItem.Text
                If cboCriteria.SelectedItem.Text.ToUpper.Contains("GRADE") = True Then
                    Process.LoadRadComboTextAndValue(cboValue, "Job_Grade_get_all", "name", "name", False)
                ElseIf cboCriteria.SelectedItem.Text.ToUpper.Contains("EMPLOYEE") = True Then
                    If chkIncudeSub.Checked Then
                        Process.LoadRadComboTextAndValueP1(cboValue, "Emp_PersonalDetail_Get_Employees", cboDept.SelectedValue, "Employee3", "empid", False)
                    Else
                        Process.LoadRadComboTextAndValueP1(cboValue, "Emp_PersonalDetail_get_all_Dept", cboDept.SelectedValue, "Employee3", "empid", False)
                    End If
                    'Process.LoadRadComboTextAndValueP1(cboValue, "Emp_PersonalDetail_Get_Employees", cboDept.SelectedValue, "Employee3", "empid", False)
                End If


                pagetitle.InnerText = "ATTENDANCE REPORT"

            End If
        Catch ex As Exception
            response.write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub


    Protected Sub btnSend_Click(sender As Object, e As EventArgs) Handles btnSend.Click

        Try

            Dim includes As String = "No"
            'If chkIncude.Checked = True Then
            '    includes = "Yes"
            'End If
            LodaDataTable(cboValue.SelectedValue, Process.DDMONYYYY(datStart.SelectedDate), Process.DDMONYYYY(datEnd.SelectedDate), cboCriteria.SelectedItem.Text, includes)
        Catch ex As Exception
            response.write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub



    Protected Sub cboDateCriteria_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboCriteria.SelectedIndexChanged
        Try
            'lblQuery.Text = cboCriteria.SelectedItem.Text
     
            If cboCriteria.SelectedItem.Text.ToUpper.Contains("GRADE") = True Then
                Process.LoadRadComboTextAndValue(cboValue, "Job_Grade_get_all", "name", "name", False)
            ElseIf cboCriteria.SelectedItem.Text.ToUpper.Contains("EMPLOYEE") = True Then
                If chkIncudeSub.Checked Then
                    Process.LoadRadComboTextAndValueP1(cboValue, "Emp_PersonalDetail_Get_Employees", cboDept.SelectedValue, "Employee3", "empid", False)
                Else
                    Process.LoadRadComboTextAndValueP1(cboValue, "Emp_PersonalDetail_get_all_Dept", cboDept.SelectedValue, "Employee3", "empid", False)
                End If
                'Process.LoadRadComboTextAndValueP1(cboValue, "Emp_PersonalDetail_Get_Employees", cboDept.SelectedValue, "Employee3", "empid", False)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub cboCompany_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboCompany.SelectedIndexChanged
        If Process.IsHRManager(Session("UserEmpID")) = True Or Process.IsFinance(Session("UserEmpID")) = True Or Process.IsAdmin(Session("LoginID")) = True Then
            Process.LoadRadComboTextAndValueP1(cboDept, "Company_Parent_Breakdown", cboCompany.SelectedValue, "companys", "companys", False)
        Else
            cboCompany.Enabled = False
            Process.LoadRadComboTextAndValueP1(cboDept, "Company_Parent_Breakdown", Session("Dept"), "companys", "companys", False)
        End If

        If cboCriteria.SelectedItem.Text.ToUpper.Contains("GRADE") = True Then
            Process.LoadRadComboTextAndValue(cboValue, "Job_Grade_get_all", "name", "name", False)
        ElseIf cboCriteria.SelectedItem.Text.ToUpper.Contains("EMPLOYEE") = True Then
            If chkIncudeSub.Checked Then
                Process.LoadRadComboTextAndValueP1(cboValue, "Emp_PersonalDetail_Get_Employees", cboDept.SelectedValue, "Employee3", "empid", False)
            Else
                Process.LoadRadComboTextAndValueP1(cboValue, "Emp_PersonalDetail_get_all_Dept", cboDept.SelectedValue, "Employee3", "empid", False)
            End If
            ' Process.LoadRadComboTextAndValueP1(cboValue, "Emp_PersonalDetail_Get_Employees", cboDept.SelectedValue, "Employee3", "empid", False)
        End If
    End Sub

    Protected Sub cboDept_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboDept.SelectedIndexChanged
        If cboCriteria.SelectedItem.Text.ToUpper.Contains("GRADE") = True Then
            Process.LoadRadComboTextAndValue(cboValue, "Job_Grade_get_all", "name", "name", False)
        ElseIf cboCriteria.SelectedItem.Text.ToUpper.Contains("EMPLOYEE") = True Then
            If chkIncudeSub.Checked Then
                Process.LoadRadComboTextAndValueP1(cboValue, "Emp_PersonalDetail_Get_Employees", cboDept.SelectedValue, "Employee3", "empid", False)
            Else
                Process.LoadRadComboTextAndValueP1(cboValue, "Emp_PersonalDetail_get_all_Dept", cboDept.SelectedValue, "Employee3", "empid", False)
            End If
            'Process.LoadRadComboTextAndValueP1(cboValue, "Emp_PersonalDetail_Get_Employees", cboDept.SelectedValue, "Employee3", "empid", False)
        End If
    End Sub

    Protected Sub chkIncude0_CheckedChanged(sender As Object, e As EventArgs) Handles chkIncudeSub.CheckedChanged
        Try
            If cboCriteria.SelectedItem.Text.ToUpper.Contains("GRADE") = False Then
                If chkIncudeSub.Checked Then
                    Process.LoadRadComboTextAndValueP1(cboValue, "Emp_PersonalDetail_Get_Employees", cboDept.SelectedValue, "Employee3", "empid", False)
                Else
                    Process.LoadRadComboTextAndValueP1(cboValue, "Emp_PersonalDetail_get_all_Dept", cboDept.SelectedValue, "Employee3", "empid", False)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class