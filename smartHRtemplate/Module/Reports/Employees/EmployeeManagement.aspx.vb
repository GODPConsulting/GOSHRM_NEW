Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Configuration
Imports Microsoft.Reporting.WebForms
Imports System.IO
Imports Telerik.Web.UI
Public Class EmployeeManagement
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "EMPLOYEEREPORT"
    Private Sub LoadEmployees()
        Dim i As Integer = 0
        Dim gradeList As String = ""
        Dim locList As String = ""
        Dim deptList As String = ""

        'job grades
        Dim coljobgrade As IList(Of RadComboBoxItem) = cboJobGrade.CheckedItems
        If (coljobgrade.Count <> 0) Then
            For Each item As RadComboBoxItem In coljobgrade
                Dim listitem As New RadListBoxItem()
                If i = 0 Then
                    gradeList = "'" & item.Value.Replace(",", "") & "'"
                Else
                    gradeList = gradeList & "," & "'" & item.Value.Replace(",", "") & "'"
                End If
                i = i + 1
            Next
        Else
            gradeList = "''"
        End If
        'Locations
        i = 0
        Dim colLoc As IList(Of RadComboBoxItem) = cboLocation.CheckedItems
        If (colLoc.Count <> 0) Then
            For Each item As RadComboBoxItem In colLoc
                Dim listitem As New RadListBoxItem()
                If i = 0 Then
                    locList = "'" & item.Value.Replace(",", "") & "'"
                Else
                    locList = locList & "," & "'" & item.Value.Replace(",", "") & "'"
                End If
                i = i + 1
            Next
        Else
            locList = "''"
        End If

        i = 0
        Dim colDept As IList(Of RadComboBoxItem) = cboDept.CheckedItems
        If (colDept.Count <> 0) Then
            For Each item As RadComboBoxItem In colDept
                Dim listitem As New RadListBoxItem()
                If i = 0 Then
                    deptList = "'" & item.Value.Replace(",", "") & "'"
                Else
                    deptList = deptList & "," & "'" & item.Value.Replace(",", "") & "'"
                End If
                i = i + 1
            Next
        Else
            deptList = "''"
        End If

        Dim exited As String = "no"
        If chkIncude.Checked = True Then
            exited = ""
        End If

        Dim script As String = "select a.Employee3,a.EmpID   from dbo.Employees_All a where isnull(a.Office,'') in (@dept) and isnull(a.grade,'') in (@grade) and isnull(a.location,'') in (@location) and Terminated like '%" & exited & "%'"
        script = script.Replace("@dept", deptList).Replace("@grade", gradeList).Replace("@location", locList)
        Dim strDataSet As New DataSet
        cboEmployee.Items.Clear()
        strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, script)
        For i = 0 To strDataSet.Tables(0).Rows.Count - 1
            'strDataSet.Tables(0).Rows(i).Item(Index).ToString()
            Dim item As New RadComboBoxItem()
            item.Text = strDataSet.Tables(0).Rows(i).Item("employee3").ToString()
            item.Value = strDataSet.Tables(0).Rows(i).Item("empid").ToString()
            cboEmployee.Items.Add(item)
            item.DataBind()
        Next
    End Sub


    Private Sub LodaDataTable()
        Dim i As Integer = 0
        Dim IDList As String = ""
        Dim criterias As String = ""
        Dim Datas As New DataTable
        Dim strDataSet As New DataSet
        Dim scripts As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "scripts_get", "employees_all_report")
        Dim collection As IList(Of RadComboBoxItem) = cboEmployee.CheckedItems
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

        scripts = scripts.Replace("@empid", IDList)


        strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, scripts)
        Datas = strDataSet.Tables(0)

        'Datas = Process.SearchDataP5("Time_Employee_Attendance_Report", dept, datstart, datend, views, includeabsent)
        Generatereport(Datas)
    End Sub
    Private Sub Generatereport(dt As DataTable)
        ReportViewer1.ProcessingMode = ProcessingMode.Local
        ReportViewer1.SizeToReportContent = True
        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Module/Reports/employees/employeemanagement.rdlc")

        Dim _rsource As New ReportDataSource("employees", dt)
        Dim reportParameter As ReportParameter() = New ReportParameter(cboColumns.Items.Count) {}
        reportParameter(0) = New ReportParameter("column0", cboCompany.SelectedValue)
        For i As Integer = 0 To cboColumns.Items.Count - 1
            If cboColumns.Items(i).Checked = True Then
                reportParameter(i + 1) = New ReportParameter("column" & (i + 1).ToString, "true")
            Else
                reportParameter(i + 1) = New ReportParameter("column" & (i + 1).ToString, "false")
            End If
        Next

        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.LocalReport.SetParameters(reportParameter)
        ReportViewer1.LocalReport.DataSources.Add(_rsource)
        ReportViewer1.LocalReport.Refresh()
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

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

                Process.LoadRadComboTextAndValue(cboJobGrade, "Job_Grade_get_all", "name", "name", False)
                Process.LoadRadComboTextAndValueP1(cboLocation, "location_get_country", cboCompany.SelectedValue, "name", "name", False)
                Process.LoadRadComboTextAndValue(cboColumns, "Employees_All_Columns", "name", "name", False)
 

                pagetitle.InnerText = "EMPLOYEE REPORT"

            End If
        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub


    Protected Sub btnSend_Click(sender As Object, e As EventArgs) Handles btnSend.Click

        Try

            LodaDataTable()
        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub

    Private Sub cboLocation_CheckAllCheck(sender As Object, e As Telerik.Web.UI.RadComboBoxCheckAllCheckEventArgs) Handles cboLocation.CheckAllCheck
        Try
            LoadEmployees()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub cboLocation_ItemChecked(sender As Object, e As Telerik.Web.UI.RadComboBoxItemEventArgs) Handles cboLocation.ItemChecked
        Try
            LoadEmployees()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

   

    Private Sub cboDept_CheckAllCheck(sender As Object, e As Telerik.Web.UI.RadComboBoxCheckAllCheckEventArgs) Handles cboDept.CheckAllCheck
        Try
            LoadEmployees()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

   

    Private Sub cboDept_ItemChecked(sender As Object, e As Telerik.Web.UI.RadComboBoxItemEventArgs) Handles cboDept.ItemChecked
        Try
            LoadEmployees()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub cboJobGrade_CheckAllCheck(sender As Object, e As Telerik.Web.UI.RadComboBoxCheckAllCheckEventArgs) Handles cboJobGrade.CheckAllCheck
        Try
            LoadEmployees()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

   

    Private Sub cboJobGrade_ItemChecked(sender As Object, e As Telerik.Web.UI.RadComboBoxItemEventArgs) Handles cboJobGrade.ItemChecked
        Try
            LoadEmployees()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

  
    Protected Sub cboCompany_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboCompany.SelectedIndexChanged
        If Process.IsHRManager(Session("UserEmpID")) = True Or Process.IsFinance(Session("UserEmpID")) = True Or Process.IsAdmin(Session("LoginID")) = True Then
            Process.LoadRadComboTextAndValueP1(cboDept, "Company_Parent_Breakdown", cboCompany.SelectedValue, "companys", "companys", False)
        Else
            cboCompany.Enabled = False
            Process.LoadRadComboTextAndValueP1(cboDept, "Company_Parent_Breakdown", Session("Dept"), "companys", "companys", False)
        End If
        Process.LoadRadComboTextAndValueP1(cboLocation, "location_get_country", cboCompany.SelectedValue, "name", "name", False)
        LoadEmployees()
    End Sub
End Class