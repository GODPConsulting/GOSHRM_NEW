Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports System.IO
Imports System.Configuration
Imports Microsoft.Reporting.WebForms
Imports Telerik.Web.UI


Public Class EmployeeProfile
    Inherits System.Web.UI.Page
    Dim education As New clsEducation
    Dim Separators() As Char = {","c}
    Dim section As String = ""
    Dim AuthenCode As String = "MYEMPPROFILE"
   
    Private Sub LoadDependants(ByVal EmpID As String)
        Try
            Dim datatables As New DataTable
            datatables = Process.SearchData("Emp_Dependents_get_all", EmpID)
            dlDependents.DataSource = datatables
            dlDependents.DataBind()

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
   
    Private Sub LoadPersonalDetail(ByVal EmpID As String)
        Try
            lblEmpID.Text = EmpID

            Dim strPersonal As New DataSet
            strPersonal = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_PersonalDetail_get", EmpID)
            If strPersonal.Tables(0).Rows.Count > 0 Then
                'imgphoto.Src = "ImgHandler.ashx?imgid=" & strPersonal.Tables(0).Rows(0).Item("id").ToString
                imgphoto.Src = strPersonal.Tables(0).Rows(0).Item("imgtype").ToString
                pempid.InnerText = "Employee ID: " & strPersonal.Tables(0).Rows(0).Item("empid").ToString
                pempname.InnerText = strPersonal.Tables(0).Rows(0).Item("FirstName").ToString & " " & strPersonal.Tables(0).Rows(0).Item("MiddleName").ToString & " " & strPersonal.Tables(0).Rows(0).Item("LastName").ToString
                pmaritalstatus.InnerText = "Marital Status: " & strPersonal.Tables(0).Rows(0).Item("MaritalStatus").ToString
                If IsDBNull(strPersonal.Tables(0).Rows(0).Item("DateOfBirth")) = False Then
                    If IsDate(strPersonal.Tables(0).Rows(0).Item("DateOfBirth")) = True Then
                        pdob.InnerText = "Date of Birth: " & CDate(strPersonal.Tables(0).Rows(0).Item("DateOfBirth")).ToLongDateString
                    Else
                        pdob.InnerText = "Date of Birth:"
                    End If
                Else
                    pdob.InnerText = "Date of Birth:"
                End If
                pgender.InnerText = "Gender: " & strPersonal.Tables(0).Rows(0).Item("Gender").ToString
                pbloodgroup.InnerText = "Blood Group: " & strPersonal.Tables(0).Rows(0).Item("BloodGroup").ToString
                pcountry.InnerText = "Country of Birth: " & strPersonal.Tables(0).Rows(0).Item("CountryOfBirth").ToString
                pnationality.InnerText = strPersonal.Tables(0).Rows(0).Item("Nationality").ToString
                pidtype.InnerText = strPersonal.Tables(0).Rows(0).Item("IDMethod").ToString
                If IsDBNull(strPersonal.Tables(0).Rows(0).Item("IDExpiryDate")) = False Then
                    If IsDate(strPersonal.Tables(0).Rows(0).Item("IDExpiryDate")) = True Then
                        'pidexpirydate.InnerText = CDate(strPersonal.Tables(0).Rows(0).Item("IDExpiryDate")).ToLongDateString
                        pidexpirydate.InnerText = CDate(strPersonal.Tables(0).Rows(0).Item("IDExpiryDate")).ToShortDateString
                    End If
                Else
                    pidexpirydate.InnerText = "Unknown"
                End If
                pidissuer.InnerText = strPersonal.Tables(0).Rows(0).Item("IDIssuer").ToString
                pidnumber.InnerText = strPersonal.Tables(0).Rows(0).Item("IDNo").ToString
                pmobileno.InnerText = strPersonal.Tables(0).Rows(0).Item("MobileNo").ToString
                If IsDBNull(strPersonal.Tables(0).Rows(0).Item("DateJoin")) = False Then
                    If IsDate(strPersonal.Tables(0).Rows(0).Item("DateJoin")) = True Then
                        presumptiondate.InnerText = CDate(strPersonal.Tables(0).Rows(0).Item("DateJoin")).ToLongDateString
                    End If
                Else
                    presumptiondate.InnerText = "Unknown"
                End If


                If IsDBNull(strPersonal.Tables(0).Rows(0).Item("Confirmationdate")) = False Then
                    If IsDate(strPersonal.Tables(0).Rows(0).Item("Confirmationdate")) = True Then
                        pconfirmationdate.InnerText = CDate(strPersonal.Tables(0).Rows(0).Item("Confirmationdate")).ToLongDateString
                    End If
                Else
                    pconfirmationdate.InnerText = "Yet to be Confirmed"
                End If
                'personalmail.InnerText = strPersonal.Tables(0).Rows(0).Item("MyMail").ToString
                pofficemail.InnerText = strPersonal.Tables(0).Rows(0).Item("WorkEmail").ToString
                phomeaddress.InnerText = strPersonal.Tables(0).Rows(0).Item("HomeAddr").ToString

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Sub LoadEmergencyContact(ByVal EmpID As String)
        Try
            Dim strEmergency As New DataSet
            strEmergency = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Emergency_Contact_get", EmpID)
            'Emergency Contacts
            If strEmergency.Tables(0).Rows.Count > 0 Then
                emername1.InnerText = strEmergency.Tables(0).Rows(0).Item("Name1").ToString
                emeraddr1.InnerText = strEmergency.Tables(0).Rows(0).Item("Address1").ToString
                emerphone1.InnerText = strEmergency.Tables(0).Rows(0).Item("Phone1").ToString
                emerrelationship1.InnerText = strEmergency.Tables(0).Rows(0).Item("Relationship1").ToString

                emername2.InnerText = strEmergency.Tables(0).Rows(0).Item("Name2").ToString
                emeraddr2.InnerText = strEmergency.Tables(0).Rows(0).Item("Address2").ToString
                emerphone2.InnerText = strEmergency.Tables(0).Rows(0).Item("Phone2").ToString
                emerrelationship2.InnerText = strEmergency.Tables(0).Rows(0).Item("Relationship2").ToString
            Else
                emername1.InnerText = "Unknown"
                emeraddr1.InnerText = "Unknown"
                emerphone1.InnerText = "Unknown"
                emerrelationship1.InnerText = "Unknown"

                emername2.InnerText = "Unknown"
                emeraddr2.InnerText = "Unknown"
                emerphone2.InnerText = "Unknown"
                emerrelationship2.InnerText = "Unknown"
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Private Sub LoadSkills(ByVal EmpID As String)
        Try
            Dim datatables As New DataTable
            datatables = Process.SearchData("Emp_Skills_get_all", EmpID)
            dlskills.DataSource = datatables
            dlskills.DataBind()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Sub LoadCertification(ByVal EmpID As String)
        Try
            Dim datatables As New DataTable
            datatables = Process.SearchData("Emp_Certifications_get_all", EmpID)
            dlcertification.DataSource = datatables
            dlcertification.DataBind()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Sub LoadAssets(ByVal EmpID As String)
        Try
            Dim datatables As New DataTable
            datatables = Process.SearchData("Emp_Asset_get_all", EmpID)
            DataList1.DataSource = datatables
            DataList1.DataBind()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Sub LoadHobbies(ByVal EmpID As String)
        Try
            Dim datatables As New DataTable
            datatables = Process.SearchData("Emp_Hobbies_get_all", EmpID)
            DataList2.DataSource = datatables
            DataList2.DataBind()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Private Sub LoadEducation(ByVal EmpID As String)
        Try
            Dim datatables As New DataTable
            datatables = Process.SearchData("Emp_Education_get_all", EmpID)
            dlEducation.DataSource = datatables
            dlEducation.DataBind()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                If Request.QueryString("empid") IsNot Nothing Then
                    LoadPersonalDetail(Request.QueryString("empid"))
                    LoadCertification(Request.QueryString("empid"))
                    LoadEducation(Request.QueryString("empid"))
                    LoadEmergencyContact(Request.QueryString("empid"))
                    LoadSkills(Request.QueryString("empid"))
                    LoadAssets(Request.QueryString("empid"))
                    'LoadDirectReport(Request.QueryString("empid"))
                    LoadDependants(Request.QueryString("empid"))
                    LoadHobbies(Request.QueryString("empid"))
                    ' LoadContacts(Request.QueryString("empid"))
                Else
                    LoadPersonalDetail(Session("UserEmpID"))
                    LoadCertification(Session("UserEmpID"))
                    LoadEducation(Session("UserEmpID"))
                    LoadEmergencyContact(Session("UserEmpID"))
                    LoadSkills(Session("UserEmpID"))
                    'LoadDirectReport(Session("UserEmpID"))
                    LoadDependants(Session("UserEmpID"))
                    LoadAssets(Session("UserEmpID"))
                    LoadHobbies(Session("UserEmpID"))
                    'LoadContacts(Session("UserEmpID"))
                    'btnWorkHistory.Visible = False
                End If
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    'Private Sub LinkClink(ByVal ViewIndex As String)
    '    Try
    '        Session("clicked") = ViewIndex
    '        MultiView1.ActiveViewIndex = ViewIndex
    '        Process.DeactivateButton(btnPersonaldetail)
    '        Process.DeactivateButton(btnEmerContact)
    '        Process.DeactivateButton(btnDependants)
    '        Process.DeactivateButton(btnQualification)
    '        Process.DeactivateButton(btnWorkHistory)

    '        Select Case ViewIndex
    '            Case "0"
    '                Process.ActivateButton(btnPersonaldetail)
    '            Case "1"
    '                Process.ActivateButton(btnEmerContact)
    '            Case "2"
    '                Process.ActivateButton(btnDependants)
    '            Case "3"
    '                Process.ActivateButton(btnQualification)
    '            Case "4"
    '                Process.ActivateButton(btnWorkHistory)
    '            Case Else
    '                Process.ActivateButton(btnPersonaldetail)
    '        End Select
    '    Catch ex As Exception

    '    End Try
    'End Sub



    'Protected Sub gridDirectReports_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gridDirectReports.PageIndexChanging
    '    Try
    '        gridDirectReports.PageIndex = e.NewPageIndex
    '        gridDirectReports.DataSource = Process.SearchData("Emp_PersonalDetail_DirectReports", lblEmpID.Text)
    '        gridDirectReports.AllowSorting = True
    '        gridDirectReports.AllowPaging = True
    '        gridDirectReports.DataBind()
    '    Catch ex As Exception

    '    End Try
    'End Sub

    ''Private Sub LoadPaySlip(empid As String, startdate As Date, enddate As Date)
    ''    Dim dtEarning As New DataTable
    ''    dtEarning = Process.SearchDataP4("Finance_Employee_Payslip_Detail_Get", empid, "earning", startdate, enddate)

    ''    Dim dtDeduction As New DataTable
    ''    dtDeduction = Process.SearchDataP4("Finance_Employee_Payslip_Detail_Get", empid, "deduction", startdate, enddate)
    ''    GenerateSlip(dtEarning, dtDeduction, empid, MonthName(enddate.Month, True).ToUpper & " " & enddate.Year.ToString)
    ''End Sub
    ''Private Sub GenerateSlip(dtearn As DataTable, dtdeduct As DataTable, empid As String, reportdate As String)
    ''    rptPayslip.ProcessingMode = ProcessingMode.Local
    ''    rptPayslip.SizeToReportContent = True
    ''    rptPayslip.LocalReport.ReportPath = Server.MapPath("~/Module/Finance/Report/Payslip.rdlc")
    ''    Dim _rsource As New ReportDataSource("Payslip_Earning", dtearn)
    ''    Dim _rsource1 As New ReportDataSource("Payslip_Deduction", dtdeduct)
    ''    Dim reportParameter As ReportParameter() = New ReportParameter(1) {}
    ''    reportParameter(0) = New ReportParameter("company", Process.GetCompanyByEmpID(empid))
    ''    reportParameter(1) = New ReportParameter("reportdate", reportdate)
    ''    rptPayslip.LocalReport.DataSources.Clear()
    ''    rptPayslip.LocalReport.DataSources.Add(_rsource)
    ''    rptPayslip.LocalReport.DataSources.Add(_rsource1)
    ''    rptPayslip.LocalReport.Refresh()
    ''End Sub



    'Protected Sub DownloadCertificate(ByVal sender As Object, ByVal e As EventArgs)
    '    Try

    '        Dim sid As String = CType(sender, LinkButton).CommandArgument
    '        Dim dt As DataTable = Process.SearchData("Emp_Certifications_get", sid)
    '        If dt IsNot Nothing Then
    '            downloadFile(CType(dt.Rows(0)("fileimage"), Byte()), dt.Rows(0)("filetype").ToString(), dt.Rows(0)("filename").ToString())
    '        End If
    '    Catch ex As Exception
    '        Process.loadalert(divalert, msgalert, ex.Message, "danger")
    '    End Try
    'End Sub
    'Protected Sub DownloadEducation(ByVal sender As Object, ByVal e As EventArgs)
    '    Try

    '        Dim sid As String = CType(sender, LinkButton).CommandArgument
    '        Dim dt As DataTable = Process.SearchData("Emp_Education_get", sid)
    '        If dt IsNot Nothing Then
    '            downloadFile(CType(dt.Rows(0)("fileimage"), Byte()), dt.Rows(0)("filetype").ToString(), dt.Rows(0)("filename").ToString())
    '        End If
    '    Catch ex As Exception
    '        Process.loadalert(divalert, msgalert, ex.Message, "danger")
    '    End Try
    'End Sub
    'Protected Sub downloadFile(ByVal bytefile As Byte(), ByVal filetype As String, ByVal filename As String)
    '    Dim bytes() As Byte = bytefile
    '    Response.Buffer = True
    '    Response.Charset = ""
    '    Response.Cache.SetCacheability(HttpCacheability.NoCache)
    '    Response.ContentType = filetype
    '    Response.AddHeader("content-disposition", "attachment;filename=" & filename)
    '    Response.BinaryWrite(bytes)
    '    Response.Flush()
    '    Response.End()
    'End Sub

    Protected Sub btnAddCert_Click(sender As Object, e As EventArgs)
        Try
            Session("EmpID") = lblEmpID.Text
            Response.Redirect("EmployeeCertification?self=emp&empid=" & lblEmpID.Text, True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try

    End Sub

    'Protected Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
    '    Try
    '        If Process.AuthenAction(Session("role"), AuthenCode, "Delete") = False Then
    '            Response.Write("You don't have privilege to perform this action!")
    '            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
    '            Exit Sub
    '        End If
    '        Dim confirmValue As String = Request.Form("confirm_value")
    '        If confirmValue = "Yes" Then
    '            Dim atLeastOneRowDeleted As Boolean = False
    '            ' Iterate through the Products.Rows property
    '            For Each row As GridViewRow In GridVwHeaderChckbox.Rows
    '                ' Access the CheckBox
    '                Dim cb As CheckBox = row.FindControl("chkEmp")
    '                If cb IsNot Nothing AndAlso cb.Checked Then
    '                    ' Delete row! (Well, not really...)
    '                    atLeastOneRowDeleted = True
    '                    ' First, get the ProductID for the selected row
    '                    Dim ID As String = _
    '                        Convert.ToString(GridVwHeaderChckbox.DataKeys(row.RowIndex).Value)
    '                    ' "Delete" the row
    '                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Certifications_delete_emp", ID)
    '                End If
    '            Next
    '            LoadCertification(lblEmpID.Text)
    '        Else
    '            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Delete has been cancelled" + "')", True)
    '        End If
    '    Catch ex As Exception
    '        Process.loadalert(divalert, msgalert, ex.Message, "danger")
    '    End Try
    'End Sub

    'Protected Sub btnPersonaldetail_Click(sender As Object, e As EventArgs) Handles btnPersonaldetail.Click
    '    Try
    '        LinkClink(0)
    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Protected Sub btnEmerContact_Click(sender As Object, e As EventArgs) Handles btnEmerContact.Click
    '    LinkClink(1)
    'End Sub

    'Protected Sub btnDependants_Click(sender As Object, e As EventArgs) Handles btnDependants.Click
    '    LinkClink(2)
    'End Sub

    'Protected Sub btnQualification_Click(sender As Object, e As EventArgs) Handles btnQualification.Click
    '    LinkClink(3)
    'End Sub

    'Protected Sub btnWorkHistory_Click(sender As Object, e As EventArgs) Handles btnWorkHistory.Click
    '    LinkClink(4)
    'End Sub

    'Protected Sub btnDelEducation_Click(sender As Object, e As EventArgs) Handles btnDelEducation.Click
    '    Try
    '        If Process.AuthenAction(Session("role"), AuthenCode, "Delete") = False Then
    '            Response.Write("You don't have privilege to perform this action!")
    '            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
    '            Exit Sub
    '        End If
    '        Dim confirmValue As String = Request.Form("confirm_value")
    '        If confirmValue = "Yes" Then
    '            Dim atLeastOneRowDeleted As Boolean = False
    '            ' Iterate through the Products.Rows property
    '            For Each row As GridViewRow In GridVwEducation.Rows
    '                ' Access the CheckBox
    '                Dim cb As CheckBox = row.FindControl("chkEmpEdu")
    '                If cb IsNot Nothing AndAlso cb.Checked Then
    '                    ' Delete row! (Well, not really...)
    '                    atLeastOneRowDeleted = True
    '                    ' First, get the ProductID for the selected row
    '                    Dim ID As String = _
    '                        Convert.ToString(GridVwEducation.DataKeys(row.RowIndex).Value)
    '                    ' "Delete" the row
    '                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Education_delete_emp", ID)
    '                End If
    '            Next
    '            LoadEducation(lblEmpID.Text)
    '        Else
    '            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "Delete has been cancelled" + "')", True)
    '        End If
    '    Catch ex As Exception
    '        Process.loadalert(divalert, msgalert, ex.Message, "danger")
    '    End Try
    'End Sub

    Protected Sub btnAddEducation_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect("EmployeeEducation?self=emp&empid=" & lblEmpID.Text, True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub btnAddDependant_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect("EmployeeDependant?self=emp&empid=" & lblEmpID.Text, True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
End Class