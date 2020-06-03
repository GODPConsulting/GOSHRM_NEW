Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Configuration
Imports Microsoft.Reporting.WebForms
Imports System.IO
Imports Telerik.Web.UI
Public Class LeaveReport
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "LEAVEREPORT"
    Dim emailFile As String = ConfigurationManager.AppSettings("EmailURL")
    Dim months(11) As String
   
    Private Sub LodaDataTable(view As String, querytype As String, dept As String, startdate As String, enddate As String)
        Dim i As Integer = 0
        Dim IDList As String = ""
        Dim criterias As String = ""
        Dim Datas As New DataTable
        Dim strDataSet As New DataSet
        Dim scripts As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "scripts_get", "time_leave_report")
        scripts = scripts.Replace("@startdate", startdate).Replace("@enddate", enddate).Replace("@dept", dept).Replace("@view", view)

        Dim groupsby As String = " group by summary.EmpID,summary.LeaveType,summary.PreviousBalance,summary.Balance,summary.Entitled, summary.MonthType1,summary.MonthType2,summary.MonthType3,summary.MonthType4,summary.MonthType5,  summary.MonthType6,summary.MonthType7,summary.MonthType8,summary.MonthType9,summary.MonthType10,summary.MonthType11,summary.MonthType12,empdetail.Name  ,empdetail.Office,summary.Ordering"

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

        If querytype.ToLower = "employee" Then
            criterias = " where empdetail.EmpID in (@empid) and summary.Entitled is not null "
            scripts = scripts & criterias.Replace("@empid", IDList)
        Else
            criterias = " where  empdetail.Office in (Select m.companys from dbo.Fn_Company_Smart_Filter('@dept', case when '@view' = 'yes' then 1 else 0 end) m)  and empdetail.grade in (@grade) and summary.Entitled is not null "
            scripts = scripts & criterias.Replace("@grade", IDList)
        End If
        scripts = scripts & groupsby

        'Dim Datas As New DataTable
        'Dim strDataSet As New DataSet
        'strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Time_Leave_Report", view, dept, startdate, enddate)

        strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, scripts)

        Datas = strDataSet.Tables(0)
        Generatereport(Datas)
    End Sub
    Private Sub Generatereport(dt As DataTable)
        Dim strDataSet As New DataSet
        strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Calendar_Get_all", "leave", cboYear.SelectedValue)
        For i As Integer = 0 To strDataSet.Tables(0).Rows.Count - 1
            months(i) = strDataSet.Tables(0).Rows(i).Item("calmonths").ToString()
        Next

        ReportViewer1.ProcessingMode = ProcessingMode.Local
        ReportViewer1.SizeToReportContent = True
        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Module/Reports/TimeManagement/LeaveReport.rdlc")
        Dim _rsource As New ReportDataSource("leaves", dt)
        Dim reportParameter As ReportParameter() = New ReportParameter(15) {}
        reportParameter(0) = New ReportParameter("company", cboCompany.SelectedValue)
        reportParameter(1) = New ReportParameter("dept", cboDept.SelectedValue)
        reportParameter(2) = New ReportParameter("daterange", Process.DDMONYYYY(radStart.SelectedDate) & " : " & Process.DDMONYYYY(radEnd.SelectedDate))
        reportParameter(3) = New ReportParameter("month1", months(0))
        reportParameter(4) = New ReportParameter("month2", months(1))
        reportParameter(5) = New ReportParameter("month3", months(2))
        reportParameter(6) = New ReportParameter("month4", months(3))
        reportParameter(7) = New ReportParameter("month5", months(4))
        reportParameter(8) = New ReportParameter("month6", months(5))
        reportParameter(9) = New ReportParameter("month7", months(6))
        reportParameter(10) = New ReportParameter("month8", months(7))
        reportParameter(11) = New ReportParameter("month9", months(8))
        reportParameter(12) = New ReportParameter("month10", months(9))
        reportParameter(13) = New ReportParameter("month11", months(10))
        reportParameter(14) = New ReportParameter("month12", months(11))
        reportParameter(15) = New ReportParameter("monthno", DateDiff(DateInterval.Month, radStart.SelectedDate.Value, radEnd.SelectedDate.Value) + 1)
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

                cboYear.Items.Clear()
                For z As Integer = 2016 To 2100
                    Dim itemTemp As New RadComboBoxItem()
                    itemTemp.Text = z
                    itemTemp.Value = z
                    cboYear.Items.Add(itemTemp)
                    itemTemp.DataBind()
                Next

                Process.AssignRadComboValue(cboYear, Date.Now.Year)
                Dim strDataSet As New DataSet
                strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Leave_Period_get", cboYear.SelectedValue)
                If strDataSet.Tables(0).Rows.Count > 0 Then
                    lblCalendar.Text = Process.DDMONYYYY(strDataSet.Tables(0).Rows(0).Item("PeriodStart")) & " : " & Process.DDMONYYYY(strDataSet.Tables(0).Rows(0).Item("PeriodEnd"))
                    radStart.SelectedDate = CDate(strDataSet.Tables(0).Rows(0).Item("PeriodStart"))
                    radEnd.SelectedDate = CDate(strDataSet.Tables(0).Rows(0).Item("PeriodEnd"))
                Else
                    lblCalendar.Text = ""
                End If


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
                    If chkIncude.Checked Then
                        Process.LoadRadComboTextAndValueP1(cboValue, "Emp_PersonalDetail_Get_Employees", cboDept.SelectedValue, "Employee3", "empid", False)
                    Else
                        Process.LoadRadComboTextAndValueP1(cboValue, "Emp_PersonalDetail_get_all_Dept", cboDept.SelectedValue, "Employee3", "empid", False)
                    End If
                End If
               
            End If
        Catch ex As Exception
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub


    Protected Sub btnSend_Click(sender As Object, e As EventArgs) Handles btnSend.Click

        Try
            pagetitle.InnerText = "Leave Report"
            Dim rates(32) As String
            If Process.ValidateCalendar("leave", cboYear.SelectedValue, radStart.SelectedDate).ToUpper = "NO" Then
                Dim ss As String = radStart.SelectedDate.ToString & " is not a valid date within " & cboYear.SelectedValue & " leave calendar"
                Response.Write(ss)
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ss + "')", True)
                Exit Sub
            End If

            If Process.ValidateCalendar("leave", cboYear.SelectedValue, radEnd.SelectedDate).ToUpper = "NO" Then
                Dim ss As String = radEnd.SelectedDate.ToString & " is not a valid date within " & cboYear.SelectedValue & " leave calendar"
                Response.Write(ss)
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ss + "')", True)
                Exit Sub
            End If
            Dim sview As String = ""
            If chkIncude.Checked = True Then
                sview = "yes"
            Else
                sview = "no"
            End If
            
            LodaDataTable(sview, cboCriteria.SelectedItem.Text, cboDept.SelectedValue, Process.DDMONYYYY(radStart.SelectedDate), Process.DDMONYYYY(radEnd.SelectedDate))
        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub


    Protected Sub cboCriteria_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboCompany.SelectedIndexChanged
        Try
            Process.LoadRadComboTextAndValueP1(cboDept, "Company_Parent_Breakdown", cboCompany.SelectedValue, "Companys", "Companys", False)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub cboYear_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboYear.SelectedIndexChanged
        Try
            Dim strDataSet As New DataSet
            strDataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Leave_Period_get", cboYear.SelectedValue)
            If strDataSet.Tables(0).Rows.Count > 0 Then
                lblCalendar.Text = Process.DDMONYYYY(strDataSet.Tables(0).Rows(0).Item("PeriodStart")) & " : " & Process.DDMONYYYY(strDataSet.Tables(0).Rows(0).Item("PeriodEnd"))
                radStart.SelectedDate = CDate(strDataSet.Tables(0).Rows(0).Item("PeriodStart"))
                radEnd.SelectedDate = CDate(strDataSet.Tables(0).Rows(0).Item("PeriodEnd"))
            Else
                lblCalendar.Text = ""
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub cboCriteria_SelectedIndexChanged1(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboCriteria.SelectedIndexChanged
        Try
            'lblQuery.Text = cboCriteria.SelectedItem.Text
       
            If cboCriteria.SelectedItem.Text.ToUpper.Contains("GRADE") = True Then
                Process.LoadRadComboTextAndValue(cboValue, "Job_Grade_get_all", "name", "name", False)
            ElseIf cboCriteria.SelectedItem.Text.ToUpper.Contains("EMPLOYEE") = True Then
                If chkIncude.Checked Then
                    Process.LoadRadComboTextAndValueP1(cboValue, "Emp_PersonalDetail_Get_Employees", cboDept.SelectedValue, "Employee3", "empid", False)
                Else
                    Process.LoadRadComboTextAndValueP1(cboValue, "Emp_PersonalDetail_get_all_Dept", cboDept.SelectedValue, "Employee3", "empid", False)
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub chkIncude_CheckedChanged(sender As Object, e As EventArgs) Handles chkIncude.CheckedChanged
        Try
            If cboCriteria.SelectedItem.Text.ToUpper.Contains("GRADE") = False Then               
                If chkIncude.Checked Then
                    Process.LoadRadComboTextAndValueP1(cboValue, "Emp_PersonalDetail_Get_Employees", cboDept.SelectedValue, "Employee3", "empid", False)
                Else
                    Process.LoadRadComboTextAndValueP1(cboValue, "Emp_PersonalDetail_get_all_Dept", cboDept.SelectedValue, "Employee3", "empid", False)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub cboDept_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboDept.SelectedIndexChanged
        Try
            If cboCriteria.SelectedItem.Text.ToUpper.Contains("GRADE") = False Then
                If chkIncude.Checked Then
                    Process.LoadRadComboTextAndValueP1(cboValue, "Emp_PersonalDetail_Get_Employees", cboDept.SelectedValue, "Employee3", "empid", False)
                Else
                    Process.LoadRadComboTextAndValueP1(cboValue, "Emp_PersonalDetail_get_all_Dept", cboDept.SelectedValue, "Employee3", "empid", False)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class