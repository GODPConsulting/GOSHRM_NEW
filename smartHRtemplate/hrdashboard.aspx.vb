Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Text
Imports System.Data
Imports Microsoft.ApplicationBlocks.Data
Imports System.Configuration
Imports System.Web.Script.Serialization
Imports System.Data.SqlClient
Imports System.Threading

Public Class hrdashboard
    Inherits System.Web.UI.Page
    Public Jtitle, Jobtitletotal As String
    Public nationalitytitle, nationalitytotal As String
    Public departmenttitle, departmenttotal As String
    Public Jgrade, Jobgradetotal As String
    Public Egender, gendertotal As String
    Public mss, fss, yss, lss, yyss As String
    Public Epopulation, avergarpopulationtotal As String
    Public office, leavedaystotal, leavecosttotal As String
    Public amountYearly, amountMonthly, monthly, yearly As String
    Public dept, deptAmountMonthly, deptAmountYearly As String
    Public objective, objectiveCount As String
    Public feedback, feedbackCount As String
    Public PerformanceName, PerformanceNameScore As String
    Public ReasonName, ReasonNameTotal As String
    Public jobtype_data As String
    Public joiners, leavers, years As String
    Public LeaversPerformanceName, LeaversPerformanceNameScore As String
    Public LeaversLengthOfServiceName, LeaversLengthOfServiceNameScore As String
    Public AverageTraningInvestmentName, AverageTraningInvestmentNameScore As String
    Public WorkForceGrowthName, WorkForceGrowthNameScore As String
    Public TurnoverRateName, TurnoverRateNameScore As String
    Public TraningCostName, TraningCostNameScore As String
    Public TraningHourName, TraningHourNameScore As String
    Public TraningTypeHourName, TraningTypeHourNameScore As String
    Public CompletionStatusName, CompletionStatusNameScore As String
    Private count As Integer = 0
    Private curr_year As Integer = DateTime.Now.Year
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If cboCompany.Text = "" Then
            Process.LoadRadComboTextAndValueP2(cboCompany, "Company_Structure_Get_ByLevel", "2", Session("Access"), "name", "name", False)
            'Process.AssignRadComboValue(cboCompany, Session("company"))
        End If

    End Sub


    Private Sub loadDashBoard(company As String)

        Dim cultureInfo As Globalization.CultureInfo = New Globalization.CultureInfo("en-US", False)
        Thread.CurrentThread.CurrentCulture = cultureInfo
        cultureInfo.NumberFormat.NumberGroupSeparator = ","
        'JobTitle
        Dim strJobTitle As New DataSet
        '= SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "JobTitle_Hr_dashboard", Session("UserEmpID"))

        Using conn2 As New SqlConnection(WebConfig.ConnectionString)
            Dim comm2 As New SqlCommand("JobTitle_Hr_dashboard", conn2)
            comm2.CommandType = CommandType.StoredProcedure
            'comm2.Parameters.AddWithValue("@Company", Session("UserEmpID"))
            comm2.Parameters.AddWithValue("@Company", company)
            comm2.CommandTimeout = 157200
            Dim sdat2 As New SqlDataAdapter(comm2)
            sdat2.Fill(strJobTitle)
            conn2.Close()
        End Using


        If strJobTitle.Tables(0).Rows.Count > 0 Then
            Dim c As Integer = strJobTitle.Tables(0).Rows.Count
            Dim jobtitle(c) As String
            Dim total(c) As String
            If c > 0 Then
                For i As Integer = 0 To c - 1
                    jobtitle(i) = Convert.ToString(strJobTitle.Tables(0).Rows(i)("JobTitle"))
                    total(i) = Convert.ToString(strJobTitle.Tables(0).Rows(i)("Total"))
                Next
            End If
            Jtitle = String.Format("'{0}'", String.Join("','", jobtitle)).Replace("''", "")
            Jobtitletotal = String.Join(",", (total))
        End If



        'EmployeeByNationality
        'Dim strEmployeeByCompany As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "sp_Get EmployeeByCompany_Hr_dashboard", Session("UserEmpID"))
        Dim strEmployeeByCompany As New DataSet
        Using conn2 As New SqlConnection(WebConfig.ConnectionString)
            Dim comm2 As New SqlCommand("sp_Get EmployeeByCompany_Hr_dashboard", conn2)
            comm2.CommandType = CommandType.StoredProcedure
            'comm2.Parameters.AddWithValue("@Company", Session("UserEmpID"))
            comm2.Parameters.AddWithValue("@Company", company)
            comm2.CommandTimeout = 157200
            Dim sdat2 As New SqlDataAdapter(comm2)
            sdat2.Fill(strEmployeeByCompany)
            conn2.Close()
        End Using

        If strEmployeeByCompany.Tables(0).Rows.Count > 0 Then
            Dim c As Integer = strEmployeeByCompany.Tables(0).Rows.Count
            Dim nationality(c) As String
            Dim total(c) As String
            If c > 0 Then
                For i As Integer = 0 To c - 1
                    nationality(i) = Convert.ToString(strEmployeeByCompany.Tables(0).Rows(i)("Nationality"))
                    total(i) = Convert.ToString(strEmployeeByCompany.Tables(0).Rows(i)("Total"))
                Next
            End If
            nationalitytitle = String.Format("'{0}'", String.Join("','", nationality)).Replace("''", "")
            nationalitytotal = String.Join(",", (total))
        End If


        'EmployeeByDepartment
        'Dim strEmployeeByDepartment As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "sp_Get EmployeeByDepartment_Hr_dashboard", Session("UserEmpID"))
        Dim strEmployeeByDepartment As New DataSet
        Using conn2 As New SqlConnection(WebConfig.ConnectionString)
            Dim comm2 As New SqlCommand("sp_Get EmployeeByDepartment_Hr_dashboard", conn2)
            comm2.CommandType = CommandType.StoredProcedure
            'comm2.Parameters.AddWithValue("@Company", Session("UserEmpID"))
            comm2.Parameters.AddWithValue("@Company", company)
            comm2.CommandTimeout = 157200
            Dim sdat2 As New SqlDataAdapter(comm2)
            sdat2.Fill(strEmployeeByDepartment)
            conn2.Close()
        End Using
        If strEmployeeByDepartment.Tables(0).Rows.Count > 0 Then
            Dim c As Integer = strEmployeeByDepartment.Tables(0).Rows.Count
            Dim department(c) As String
            Dim total(c) As String
            If c > 0 Then
                For i As Integer = 0 To c - 1
                    department(i) = Convert.ToString(strEmployeeByDepartment.Tables(0).Rows(i)("Office"))
                    total(i) = Convert.ToString(strEmployeeByDepartment.Tables(0).Rows(i)("Total"))
                Next
            End If
            departmenttitle = String.Format("'{0}'", String.Join("','", department)).Replace("''", "")
            departmenttotal = String.Join(",", (total))
        End If




        'JobGrade
        'Dim strJobGrade As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "sp_Get EmployeeByJobGrade_Hr_dashboard", Session("UserEmpID"))
        Dim strJobGrade As New DataSet
        Using conn2 As New SqlConnection(WebConfig.ConnectionString)
            Dim comm2 As New SqlCommand("sp_Get EmployeeByJobGrade_Hr_dashboard", conn2)
            comm2.CommandType = CommandType.StoredProcedure
            'comm2.Parameters.AddWithValue("@Company", Session("UserEmpID"))
            comm2.Parameters.AddWithValue("@Company", company)
            comm2.CommandTimeout = 157200
            Dim sdat2 As New SqlDataAdapter(comm2)
            sdat2.Fill(strJobGrade)
            conn2.Close()
        End Using
        If strJobGrade.Tables(0).Rows.Count > 0 Then
            Dim c As Integer = strJobGrade.Tables(0).Rows.Count
            Dim jobgrade(c) As String
            Dim total(c) As String
            If c > 0 Then
                For i As Integer = 0 To c - 1
                    jobgrade(i) = Convert.ToString(strJobGrade.Tables(0).Rows(i)("Grade"))
                    total(i) = Convert.ToString(strJobGrade.Tables(0).Rows(i)("Total"))
                Next
            End If
            Jgrade = String.Format("'{0}'", String.Join("','", jobgrade)).Replace("''", "")
            Jobgradetotal = String.Join(",", (total))
        End If

        'Gender Analysis
        Dim strGender As New DataSet
        Using conn2 As New SqlConnection(WebConfig.ConnectionString)
            Dim comm2 As New SqlCommand("sp_Get EmployeeByGender_Hr_dashboard", conn2)
            comm2.CommandType = CommandType.StoredProcedure
            'comm2.Parameters.AddWithValue("@Company", Session("UserEmpID"))
            comm2.Parameters.AddWithValue("@Company", company)
            comm2.CommandTimeout = 157200
            Dim sdat2 As New SqlDataAdapter(comm2)
            sdat2.Fill(strGender)
            conn2.Close()
        End Using
        If strGender.Tables(0).Rows.Count > 0 Then
            Dim c As Integer = strGender.Tables(0).Rows.Count
            Dim male(c) As String
            Dim female(c) As String
            Dim myear(c) As String
            If c > 0 Then
                For i As Integer = 0 To c - 1
                    male(i) = Convert.ToString(strGender.Tables(0).Rows(i)("Male"))
                    female(i) = Convert.ToString(strGender.Tables(0).Rows(i)("Female"))
                    myear(i) = Convert.ToString(strGender.Tables(0).Rows(i)("Years"))
                Next
            End If
            mss = String.Join(",", (male)).Trim(",")
            fss = String.Join(",", (female)).Trim(",")
            yss = String.Format("'{0}'", String.Join("','", myear)).Replace("''", "").Trim(",")
        End If

        ''Dim strtest2 As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "sp_Get EmployeeByGender_Hr_dashboard", Session("UserEmpID"))
        'Dim strtest2 As New DataSet
        'Using conn2 As New SqlConnection(WebConfig.ConnectionString)
        '    Dim comm2 As New SqlCommand("sp_Get EmployeeByGender_Hr_dashboard", conn2)
        '    comm2.CommandType = CommandType.StoredProcedure
        '    comm2.Parameters.AddWithValue("@Company", Session("UserEmpID"))
        '    comm2.CommandTimeout = 99999999
        '    Dim sdat2 As New SqlDataAdapter(comm2)
        '    sdat2.Fill(strtest2)
        '    conn2.Close()
        'End Using
        'If strtest2.Tables(0).Rows.Count > 0 Then
        '    Dim c As Integer = strtest2.Tables(0).Rows.Count
        '    If c > 0 Then
        '        While count < 5
        '            For i As Integer = 0 To 5 - 1
        '                'Dim str As String = "select count(gender) from emp_personaldetail where year(DateJoin) < " & curr_year & " and  and gender = 'Male'"
        '                'Dim str2 As String = "select count(gender) from emp_personaldetail where year(DateJoin) < " & curr_year & " and gender = 'Female'"
        '                'Dim str3 As String = "select count(gender) from emp_personaldetail where year(TerminationDate) < " & curr_year & " and  and gender = 'Male'"
        '                'Dim str4 As String = "select count(gender) from emp_personaldetail where year(TerminationDate) < " & curr_year & " and gender = 'Female'"

        '                'male(i) = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "sp_Get EmployeeByGenderDiffByMale", curr_year, Session("UserEmpID"))
        '                'Dim male1 As New DataSet
        '                'Using conn2 As New SqlConnection(WebConfig.ConnectionString)
        '                '    Dim comm2 As New SqlCommand("sp_Get EmployeeByGender_Hr_dashboard", conn2)
        '                '    comm2.CommandType = CommandType.StoredProcedure
        '                '    comm2.Parameters.AddWithValue("@Company", Session("UserEmpID"))
        '                '    comm2.CommandTimeout = 99999999
        '                '    Dim sdat2 As New SqlDataAdapter(comm2)
        '                '    sdat2.Fill(male1)
        '                '    conn2.Close()
        '                'End Using
        '                male(i) = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "sp_Get EmployeeByGenderDiffByMale", curr_year, Session("UserEmpID"))
        '                female(i) = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, "sp_Get EmployeeByGenderDiffByFemale", curr_year, Session("UserEmpID"))
        '                myear(i) = curr_year
        '                curr_year = curr_year - 1
        '                count = count + 1
        '            Next
        '            Array.Reverse(male)
        '            Array.Reverse(female)
        '            Array.Reverse(myear)
        '        End While
        '    End If
        'End If
        'mss = String.Join(",", (male)).Trim(",")
        'fss = String.Join(",", (female)).Trim(",")
        'yss = String.Format("'{0}'", String.Join("','", myear)).Replace("''", "").Trim(",")



        'PopulationByPerformance
        'Dim strPopulation As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "sp_Get EmployeePopulationByPerformanceScore", Session("UserEmpID"))
        Dim strPopulation As New DataSet
        Using conn2 As New SqlConnection(WebConfig.ConnectionString)
            Dim comm2 As New SqlCommand("sp_Get EmployeePopulationByPerformanceScore", conn2)
            comm2.CommandType = CommandType.StoredProcedure
            'comm2.Parameters.AddWithValue("@Company", Session("UserEmpID"))
            comm2.Parameters.AddWithValue("@Company", company)
            comm2.CommandTimeout = 157200
            Dim sdat2 As New SqlDataAdapter(comm2)
            sdat2.Fill(strPopulation)
            conn2.Close()
        End Using
        If strPopulation.Tables(0).Rows.Count > 0 Then
            Dim c As Integer = strPopulation.Tables(0).Rows.Count
            Dim population(c) As String
            Dim avergarpopulation(c) As String
            If c > 0 Then
                For i As Integer = 0 To c - 1
                    population(i) = Convert.ToString(strPopulation.Tables(0).Rows(i)("Office"))
                    avergarpopulation(i) = Convert.ToString(strPopulation.Tables(0).Rows(i)("AverageScore"))
                Next
            End If
            Epopulation = String.Format("'{0}'", String.Join("','", population)).Replace("''", "")
            avergarpopulationtotal = String.Join(",", (avergarpopulation))
        End If



        'GlobalLeaveByDays
        'Dim strLeaveDays As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "sp_Get_GlobalLeaveDays_Hr_dashboard", Session("UserEmpID"))
        Dim strLeaveDays As New DataSet
        Using conn2 As New SqlConnection(WebConfig.ConnectionString)
            Dim comm2 As New SqlCommand("sp_Get_GlobalLeaveDays_Hr_dashboard", conn2)
            comm2.CommandType = CommandType.StoredProcedure
            'comm2.Parameters.AddWithValue("@EmpID", Session("UserEmpID"))
            comm2.Parameters.AddWithValue("@Company", company)
            comm2.CommandTimeout = 157200
            Dim sdat2 As New SqlDataAdapter(comm2)
            sdat2.Fill(strLeaveDays)
            conn2.Close()
        End Using
        If strLeaveDays.Tables(0).Rows.Count > 0 Then
            Dim c As Integer = strLeaveDays.Tables(0).Rows.Count
            Dim leave(c) As String
            Dim leaveDays(c) As String
            If c > 0 Then
                For i As Integer = 0 To c - 1
                    leave(i) = Convert.ToString(strLeaveDays.Tables(0).Rows(i)("Office"))
                    leaveDays(i) = Convert.ToString(strLeaveDays.Tables(0).Rows(i)("Days"))
                Next
            End If
            office = String.Format("'{0}'", String.Join("','", leave)).Replace("''", "")
            leavedaystotal = String.Join(",", (leaveDays))


        End If



        'GlobalLeaveByCost
        'Dim strLeaveCost As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "sp_Get_GlobalLeaveCost_Hr_dashboard", Session("UserEmpID"))
        Dim strLeaveCost As New DataSet
        Using conn2 As New SqlConnection(WebConfig.ConnectionString)
            Dim comm2 As New SqlCommand("sp_Get_GlobalLeaveCost_Hr_dashboard", conn2)
            comm2.CommandType = CommandType.StoredProcedure
            'comm2.Parameters.AddWithValue("@EmpID", Session("UserEmpID"))
            comm2.Parameters.AddWithValue("@Company", company)
            comm2.CommandTimeout = 157200
            Dim sdat2 As New SqlDataAdapter(comm2)
            sdat2.Fill(strLeaveCost)
            conn2.Close()
        End Using
        If strLeaveCost.Tables(0).Rows.Count > 0 Then
            Dim c As Integer = strLeaveCost.Tables(0).Rows.Count
            Dim leave(c) As String
            Dim leaveCost(c) As String
            If c > 0 Then
                For i As Integer = 0 To c - 1
                    leave(i) = Convert.ToString(strLeaveCost.Tables(0).Rows(i)("Office"))
                    leaveCost(i) = Convert.ToString(strLeaveCost.Tables(0).Rows(i)("Cost"))
                Next
            End If
            office = String.Format("'{0}'", String.Join("','", leave)).Replace("''", "")
            leavecosttotal = String.Join(",", (leaveCost))


        End If




        'AnnualCompensationBenefit
        'Dim strAnnualCompensation As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "sp_Get_AnnualCompensationBenefit_Hr_dashboard", Session("UserEmpID"))
        Dim strAnnualCompensation As New DataSet
        Using conn2 As New SqlConnection(WebConfig.ConnectionString)
            Dim comm2 As New SqlCommand("sp_Get_AnnualCompensationBenefit_Hr_dashboard", conn2)
            comm2.CommandType = CommandType.StoredProcedure
            'comm2.Parameters.AddWithValue("@Company", Session("UserEmpID"))
            comm2.Parameters.AddWithValue("@Company", company)
            comm2.CommandTimeout = 157200
            Dim sdat2 As New SqlDataAdapter(comm2)
            sdat2.Fill(strAnnualCompensation)
            conn2.Close()
        End Using
        If strAnnualCompensation.Tables(0).Rows.Count > 0 Then
            Dim c As Integer = strAnnualCompensation.Tables(0).Rows.Count
            Dim AnnualCompensation(c) As String
            Dim AnnualCompensationYear(c) As String
            If c > 0 Then
                For i As Integer = 0 To c - 1
                    AnnualCompensation(i) = Convert.ToString(strAnnualCompensation.Tables(0).Rows(i)("Amount"))
                    AnnualCompensationYear(i) = Convert.ToString(strAnnualCompensation.Tables(0).Rows(i)("Year"))
                Next
            End If
            amountYearly = String.Format("'{0}'", String.Join("','", AnnualCompensation)).Replace("''", "")
            yearly = String.Format("'{0}'", String.Join("','", AnnualCompensationYear)).Replace("''", "")
            'yearly = String.Join(",", (AnnualCompensationYear))


        End If




        'AnnualCompensationBenefitMonthly
        'Dim strAnnualCompensationMonthly As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "sp_Get_AnnualCompensationBenefit_Monthly_Hr_dashboard", Session("UserEmpID"))
        Dim strAnnualCompensationMonthly As New DataSet
        Using conn2 As New SqlConnection(WebConfig.ConnectionString)
            Dim comm2 As New SqlCommand("sp_Get_AnnualCompensationBenefit_Monthly_Hr_dashboard", conn2)
            comm2.CommandType = CommandType.StoredProcedure
            'comm2.Parameters.AddWithValue("@Company", Session("UserEmpID"))
            comm2.Parameters.AddWithValue("@Company", company)
            comm2.CommandTimeout = 157200
            Dim sdat2 As New SqlDataAdapter(comm2)
            sdat2.Fill(strAnnualCompensationMonthly)
            conn2.Close()
        End Using
        If strAnnualCompensationMonthly.Tables(0).Rows.Count > 0 Then
            Dim c As Integer = strAnnualCompensationMonthly.Tables(0).Rows.Count
            Dim AnnualCompensation(c) As String
            Dim AnnualCompensationMonthly(c) As String
            If c > 0 Then
                For i As Integer = 0 To c - 1
                    AnnualCompensation(i) = Convert.ToString(strAnnualCompensationMonthly.Tables(0).Rows(i)("Month"))
                    AnnualCompensationMonthly(i) = Convert.ToString(strAnnualCompensationMonthly.Tables(0).Rows(i)("Amount"))
                Next
            End If
            'Dim ssss As String = ""
            amountMonthly = String.Format("'{0}'", String.Join("','", AnnualCompensation)).Replace("''", "")
            'monthly = String.Join(",", (AnnualCompensationMonthly))
            monthly = String.Format("'{0}'", String.Join("','", AnnualCompensationMonthly)).Replace("''", "")


        End If




        'AnnualCompensationBenefitByDept
        'Dim strAnnualCompensationBenefitByDept As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "sp_Get_AnnualCompensationBenefitByDept_Hr_dashboard", Session("UserEmpID"))
        Dim strAnnualCompensationBenefitByDept As New DataSet
        Using conn2 As New SqlConnection(WebConfig.ConnectionString)
            Dim comm2 As New SqlCommand("sp_Get_AnnualCompensationBenefitByDept_Hr_dashboard", conn2)
            comm2.CommandType = CommandType.StoredProcedure
            'comm2.Parameters.AddWithValue("@Company", Session("UserEmpID"))
            comm2.Parameters.AddWithValue("@Company", company)
            comm2.CommandTimeout = 157200
            Dim sdat2 As New SqlDataAdapter(comm2)
            sdat2.Fill(strAnnualCompensationBenefitByDept)
            conn2.Close()
        End Using
        If strAnnualCompensationBenefitByDept.Tables(0).Rows.Count > 0 Then
            Dim c As Integer = strAnnualCompensationBenefitByDept.Tables(0).Rows.Count
            Dim AnnualCompDept(c) As String
            Dim AnnualCompensationDept(c) As String
            If c > 0 Then
                For i As Integer = 0 To c - 1
                    AnnualCompDept(i) = Convert.ToString(strAnnualCompensationBenefitByDept.Tables(0).Rows(i)("Office"))
                    AnnualCompensationDept(i) = Convert.ToString(strAnnualCompensationBenefitByDept.Tables(0).Rows(i)("Amount"))
                Next
            End If
            dept = String.Format("'{0}'", String.Join("','", AnnualCompDept)).Replace("''", "")
            deptAmountYearly = String.Join(",", (AnnualCompensationDept))


        End If

        'AnnualCompensationBenefitByDeptMonthly
        'Dim strAnnualCompensationBenefitByDeptMonthly As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "sp_Get_AnnualCompensationBenefitByDeptByMonthly_Hr_dashboard", Session("UserEmpID"))
        Dim strAnnualCompensationBenefitByDeptMonthly As New DataSet
        Using conn2 As New SqlConnection(WebConfig.ConnectionString)
            Dim comm2 As New SqlCommand("sp_Get_AnnualCompensationBenefitByDeptByMonthly_Hr_dashboard", conn2)
            comm2.CommandType = CommandType.StoredProcedure
            'comm2.Parameters.AddWithValue("@Company", Session("UserEmpID"))
            comm2.Parameters.AddWithValue("@Company", company)
            comm2.CommandTimeout = 157200
            Dim sdat2 As New SqlDataAdapter(comm2)
            sdat2.Fill(strAnnualCompensationBenefitByDeptMonthly)
            conn2.Close()
        End Using
        If strAnnualCompensationBenefitByDeptMonthly.Tables(0).Rows.Count > 0 Then
            Dim c As Integer = strAnnualCompensationBenefitByDeptMonthly.Tables(0).Rows.Count
            Dim AnnualCompensation(c) As String
            Dim AnnualCompensationDept(c) As String
            If c > 0 Then
                For i As Integer = 0 To c - 1
                    AnnualCompensation(i) = Convert.ToString(strAnnualCompensationBenefitByDeptMonthly.Tables(0).Rows(i)("Office"))
                    AnnualCompensationDept(i) = Convert.ToString(strAnnualCompensationBenefitByDeptMonthly.Tables(0).Rows(i)("Amount"))
                Next
            End If
            dept = String.Format("'{0}'", String.Join("','", AnnualCompensation)).Replace("''", "")
            deptAmountMonthly = String.Join(",", (AnnualCompensationDept))


        End If


        'ObjectiveAnalysis
        'Dim strObjectiveAnalysis As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "sp_Get_ObjectiveAnalysis_Hr_dashboard", Session("UserEmpID"))
        Dim strObjectiveAnalysis As New DataSet
        Using conn2 As New SqlConnection(WebConfig.ConnectionString)
            Dim comm2 As New SqlCommand("sp_Get_ObjectiveAnalysis_Hr_dashboard", conn2)
            comm2.CommandType = CommandType.StoredProcedure
            'comm2.Parameters.AddWithValue("@Company", Session("UserEmpID"))
            comm2.Parameters.AddWithValue("@Company", company)
            comm2.CommandTimeout = 157200
            Dim sdat2 As New SqlDataAdapter(comm2)
            sdat2.Fill(strObjectiveAnalysis)
            conn2.Close()
        End Using
        If strObjectiveAnalysis.Tables(0).Rows.Count > 0 Then
            Dim c As Integer = strObjectiveAnalysis.Tables(0).Rows.Count
            Dim ObjectiveAnalysis(c) As String
            Dim ObjectiveAnalysisCount(c) As String
            If c > 0 Then
                For i As Integer = 0 To c - 1
                    ObjectiveAnalysis(i) = Convert.ToString(strObjectiveAnalysis.Tables(0).Rows(i)("CoachApprovalStatus"))
                    ObjectiveAnalysisCount(i) = Convert.ToString(strObjectiveAnalysis.Tables(0).Rows(i)("Count"))
                Next
            End If
            objective = String.Format("'{0}'", String.Join("','", ObjectiveAnalysis)).Replace("''", "")
            objectiveCount = String.Join(",", (ObjectiveAnalysisCount))


        End If



        'FeedbackAnalysis
        'Dim strFeedbackAnalysis As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "sp_Get_FeedBackAnalysis_Hr_dashboard", Session("UserEmpID"))
        Dim strFeedbackAnalysis As New DataSet
        Using conn2 As New SqlConnection(WebConfig.ConnectionString)
            Dim comm2 As New SqlCommand("sp_Get_FeedBackAnalysis_Hr_dashboard", conn2)
            comm2.CommandType = CommandType.StoredProcedure
            'comm2.Parameters.AddWithValue("@Company", Session("UserEmpID"))
            comm2.Parameters.AddWithValue("@Company", company)
            comm2.CommandTimeout = 157200
            Dim sdat2 As New SqlDataAdapter(comm2)
            sdat2.Fill(strFeedbackAnalysis)
            conn2.Close()
        End Using
        If strFeedbackAnalysis.Tables(0).Rows.Count > 0 Then
            Dim c As Integer = strFeedbackAnalysis.Tables(0).Rows.Count
            Dim FeedbackAnalysis(c) As String
            Dim FeedbackAnalysisCount(c) As String
            If c > 0 Then
                For i As Integer = 0 To c - 1
                    FeedbackAnalysis(i) = Convert.ToString(strFeedbackAnalysis.Tables(0).Rows(i)("CompletedStatus"))
                    FeedbackAnalysisCount(i) = Convert.ToString(strFeedbackAnalysis.Tables(0).Rows(i)("Count"))
                Next
            End If
            feedback = String.Format("'{0}'", String.Join("','", FeedbackAnalysis)).Replace("''", "")
            feedbackCount = String.Join(",", (FeedbackAnalysisCount))


        End If



        'Performance
        'Dim strPerformance As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "sp_Get_Performance_Hr_dashboard", Session("UserEmpID"))
        Dim strPerformance As New DataSet
        Using conn2 As New SqlConnection(WebConfig.ConnectionString)
            Dim comm2 As New SqlCommand("sp_Get_Performance_Hr_dashboard", conn2)
            comm2.CommandType = CommandType.StoredProcedure
            'comm2.Parameters.AddWithValue("@Company", Session("UserEmpID"))
            comm2.Parameters.AddWithValue("@Company", company)
            comm2.CommandTimeout = 157200
            Dim sdat2 As New SqlDataAdapter(comm2)
            sdat2.Fill(strPerformance)
            conn2.Close()
        End Using
        If strPerformance.Tables(0).Rows.Count > 0 Then
            Dim c As Integer = strPerformance.Tables(0).Rows.Count
            Dim Performance(c) As String
            Dim PerformanceScore(c) As String
            If c > 0 Then
                For i As Integer = 0 To c - 1
                    Performance(i) = Convert.ToString(strPerformance.Tables(0).Rows(i)("Name"))
                    PerformanceScore(i) = Convert.ToString(strPerformance.Tables(0).Rows(i)("Score"))
                Next
            End If
            PerformanceName = String.Format("'{0}'", String.Join("','", Performance)).Replace("''", "")
            PerformanceNameScore = String.Join(",", (PerformanceScore))


        End If




        'Joiners/Leavers Analysis



        'Dim strJoinersLeavers As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "sp_Get_JoinersLeavers", Session("UserEmpID"))
        Dim strJoinersLeavers As New DataSet
        Using conn2 As New SqlConnection(WebConfig.ConnectionString)
            Dim comm2 As New SqlCommand("sp_Get_JoinersLeavers", conn2)
            comm2.CommandType = CommandType.StoredProcedure
            'comm2.Parameters.AddWithValue("@Company", Session("UserEmpID"))
            comm2.Parameters.AddWithValue("@Company", company)
            comm2.CommandTimeout = 157200
            Dim sdat2 As New SqlDataAdapter(comm2)
            sdat2.Fill(strJoinersLeavers)
            conn2.Close()
        End Using
        If strJoinersLeavers.Tables(0).Rows.Count > 0 Then
            Dim c As Integer = strJoinersLeavers.Tables(0).Rows.Count
            Dim joiner(c) As String
            Dim leaver(c) As String
            Dim year(c) As String
            If c > 0 Then
                For i As Integer = 0 To c - 1
                    joiner(i) = Convert.ToString(strJoinersLeavers.Tables(0).Rows(i)("Joiner"))
                    leaver(i) = Convert.ToString(strJoinersLeavers.Tables(0).Rows(i)("Leaver"))
                    year(i) = Convert.ToString(strJoinersLeavers.Tables(0).Rows(i)("Years"))
                Next
            End If

            joiners = String.Join(",", (joiner)).Trim(",")
            leavers = String.Join(",", (leaver)).Trim(",")
            years = String.Format("'{0}'", String.Join("','", year)).Replace("''", "").Trim(",")
        End If


        'LeaversAnalysisByReasons
        'Dim strReason As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "sp_Get_LeaversAnalysisByReasons_Hr_dashboard", Session("UserEmpID"))
        Dim strReason As New DataSet
        Using conn2 As New SqlConnection(WebConfig.ConnectionString)
            Dim comm2 As New SqlCommand("sp_Get_LeaversAnalysisByReasons_Hr_dashboard", conn2)
            comm2.CommandType = CommandType.StoredProcedure
            'comm2.Parameters.AddWithValue("@Company", Session("UserEmpID"))
            comm2.Parameters.AddWithValue("@Company", company)
            comm2.CommandTimeout = 157200
            Dim sdat2 As New SqlDataAdapter(comm2)
            sdat2.Fill(strReason)
            conn2.Close()
        End Using
        If strReason.Tables(0).Rows.Count > 0 Then
            Dim c As Integer = strReason.Tables(0).Rows.Count
            Dim Reason(c) As String
            Dim ReasonTotal(c) As String
            If c > 0 Then
                For i As Integer = 0 To c - 1
                    Reason(i) = Convert.ToString(strReason.Tables(0).Rows(i)("Reason"))
                    ReasonTotal(i) = Convert.ToString(strReason.Tables(0).Rows(i)("Total"))
                Next
            End If
            ReasonName = String.Format("'{0}'", String.Join("','", Reason)).Replace("''", "")
            ReasonNameTotal = String.Join(",", (ReasonTotal))


        End If



        'LeaversPerformance
        'Dim strLeaversPerformance As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "sp_Get_LeaversPerformance_Hr_dashboard", Session("UserEmpID"))
        Dim strLeaversPerformance As New DataSet
        Using conn2 As New SqlConnection(WebConfig.ConnectionString)
            Dim comm2 As New SqlCommand("sp_Get_LeaversPerformance_Hr_dashboard", conn2)
            comm2.CommandType = CommandType.StoredProcedure
            'comm2.Parameters.AddWithValue("@Company", Session("UserEmpID"))
            comm2.Parameters.AddWithValue("@Company", company)
            comm2.CommandTimeout = 157200
            Dim sdat2 As New SqlDataAdapter(comm2)
            sdat2.Fill(strLeaversPerformance)
            conn2.Close()
        End Using
        If strLeaversPerformance.Tables(0).Rows.Count > 0 Then
            Dim c As Integer = strLeaversPerformance.Tables(0).Rows.Count
            Dim LeaversPerformance(c) As String
            Dim LeaversPerformanceScore(c) As String
            If c > 0 Then
                For i As Integer = 0 To c - 1
                    LeaversPerformance(i) = Convert.ToString(strLeaversPerformance.Tables(0).Rows(i)("Name"))
                    LeaversPerformanceScore(i) = Convert.ToString(strLeaversPerformance.Tables(0).Rows(i)("Score"))
                Next
            End If
            LeaversPerformanceName = String.Format("'{0}'", String.Join("','", LeaversPerformance)).Replace("''", "")
            LeaversPerformanceNameScore = String.Join(",", (LeaversPerformanceScore))


        End If



        'LeaversLengthOfService
        'Dim strLeaversLengthOfService As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "sp_Get_LeaversAnalysisLengthOfService_Hr_dashboard", Session("UserEmpID"))
        Dim strLeaversLengthOfService As New DataSet
        Using conn2 As New SqlConnection(WebConfig.ConnectionString)
            Dim comm2 As New SqlCommand("sp_Get_LeaversAnalysisLengthOfService_Hr_dashboard", conn2)
            comm2.CommandType = CommandType.StoredProcedure
            'comm2.Parameters.AddWithValue("@Company", Session("UserEmpID"))
            comm2.Parameters.AddWithValue("@Company", company)
            comm2.CommandTimeout = 157200
            Dim sdat2 As New SqlDataAdapter(comm2)
            sdat2.Fill(strLeaversLengthOfService)
            conn2.Close()
        End Using
        If strLeaversLengthOfService.Tables(0).Rows.Count > 0 Then
            Dim c As Integer = strLeaversLengthOfService.Tables(0).Rows.Count
            Dim LeaversLengthOfService(c) As String
            Dim LeaversLengthOfServiceScore(c) As String
            If c > 0 Then
                For i As Integer = 0 To c - 1
                    LeaversLengthOfService(i) = Convert.ToString(strLeaversLengthOfService.Tables(0).Rows(i)("Office"))
                    LeaversLengthOfServiceScore(i) = Convert.ToString(strLeaversLengthOfService.Tables(0).Rows(i)("Score"))
                Next
            End If
            LeaversLengthOfServiceName = String.Format("'{0}'", String.Join("','", LeaversLengthOfService)).Replace("''", "")
            LeaversLengthOfServiceNameScore = String.Join(",", (LeaversLengthOfServiceScore))


        End If



        'AverageTraningInvestment
        'Dim strAverageTraningInvestment As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "sp_Get_AverageTraningInvestment_Hr_dashboard", Session("UserEmpID"))
        Dim strAverageTraningInvestment As New DataSet
        Using conn2 As New SqlConnection(WebConfig.ConnectionString)
            Dim comm2 As New SqlCommand("sp_Get_AverageTraningInvestment_Hr_dashboard", conn2)
            comm2.CommandType = CommandType.StoredProcedure
            'comm2.Parameters.AddWithValue("@Company", Session("UserEmpID"))
            comm2.Parameters.AddWithValue("@Company", company)
            comm2.CommandTimeout = 157200
            Dim sdat2 As New SqlDataAdapter(comm2)
            sdat2.Fill(strAverageTraningInvestment)
            conn2.Close()
        End Using
        If strAverageTraningInvestment.Tables(0).Rows.Count > 0 Then
            Dim c As Integer = strAverageTraningInvestment.Tables(0).Rows.Count
            Dim AverageTraningInvestment(c) As String
            Dim AverageTraningInvestmentScore(c) As String
            If c > 0 Then
                For i As Integer = 0 To c - 1
                    AverageTraningInvestment(i) = Convert.ToString(strAverageTraningInvestment.Tables(0).Rows(i)("Name"))
                    AverageTraningInvestmentScore(i) = Convert.ToString(strAverageTraningInvestment.Tables(0).Rows(i)("AverageCost"))
                Next
            End If
            AverageTraningInvestmentName = String.Format("'{0}'", String.Join("','", AverageTraningInvestment)).Replace("''", "")
            AverageTraningInvestmentNameScore = String.Join(",", (AverageTraningInvestmentScore))


        End If



        'WorkForceGrowth
        'Dim strWorkForceGrowth As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "sp_Get_WorkForceGrowth_Hr_dashboard", Session("UserEmpID"))
        Dim strWorkForceGrowth As New DataSet
        Using conn2 As New SqlConnection(WebConfig.ConnectionString)
            Dim comm2 As New SqlCommand("sp_Get_WorkForceGrowth_Hr_dashboard", conn2)
            comm2.CommandType = CommandType.StoredProcedure
            'comm2.Parameters.AddWithValue("@Company", Session("UserEmpID"))
            comm2.Parameters.AddWithValue("@Company", company)
            comm2.CommandTimeout = 157200
            Dim sdat2 As New SqlDataAdapter(comm2)
            sdat2.Fill(strWorkForceGrowth)
            conn2.Close()
        End Using
        If strWorkForceGrowth.Tables(0).Rows.Count > 0 Then
            Dim c As Integer = strWorkForceGrowth.Tables(0).Rows.Count
            Dim WorkForceGrowth(c) As String
            Dim WorkForceGrowthScore(c) As String
            If c > 0 Then
                For i As Integer = 0 To c - 1
                    WorkForceGrowth(i) = Convert.ToString(strWorkForceGrowth.Tables(0).Rows(i)("Name"))
                    WorkForceGrowthScore(i) = Convert.ToString(strWorkForceGrowth.Tables(0).Rows(i)("Count"))
                Next
            End If
            WorkForceGrowthName = String.Format("'{0}'", String.Join("','", WorkForceGrowth)).Replace("''", "")
            WorkForceGrowthNameScore = String.Join(",", (WorkForceGrowthScore))


        End If





        'TurnoverRate
        'Dim strTurnoverRate As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "sp_Get_TurnoverRate_Hr_dashboard", Session("UserEmpID"))
        Dim strTurnoverRate As New DataSet
        Using conn2 As New SqlConnection(WebConfig.ConnectionString)
            Dim comm2 As New SqlCommand("sp_Get_TurnoverRate_Hr_dashboard", conn2)
            comm2.CommandType = CommandType.StoredProcedure
            'comm2.Parameters.AddWithValue("@Company", Session("UserEmpID"))
            comm2.Parameters.AddWithValue("@Company", company)
            comm2.CommandTimeout = 157200
            Dim sdat2 As New SqlDataAdapter(comm2)
            sdat2.Fill(strTurnoverRate)
            conn2.Close()
        End Using
        If strTurnoverRate.Tables(0).Rows.Count > 0 Then
            Dim c As Integer = strTurnoverRate.Tables(0).Rows.Count
            Dim TurnoverRate(c) As String
            Dim TurnoverRateScore(c) As String
            If c > 0 Then
                For i As Integer = 0 To c - 1
                    TurnoverRate(i) = Convert.ToString(strTurnoverRate.Tables(0).Rows(i)("Name"))
                    TurnoverRateScore(i) = Convert.ToString(strTurnoverRate.Tables(0).Rows(i)("Count"))
                Next
            End If
            TurnoverRateName = String.Format("'{0}'", String.Join("','", TurnoverRate)).Replace("''", "")
            TurnoverRateNameScore = String.Join(",", (TurnoverRateScore))


        End If


        'TraningCost
        'Dim strTraningCost As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "sp_Get_TraningCost_Hr_dashboard", Session("UserEmpID"))
        Dim strTraningCost As New DataSet
        Using conn2 As New SqlConnection(WebConfig.ConnectionString)
            Dim comm2 As New SqlCommand("sp_Get_TraningCost_Hr_dashboard", conn2)
            comm2.CommandType = CommandType.StoredProcedure
            'comm2.Parameters.AddWithValue("@Company", Session("UserEmpID"))
            comm2.Parameters.AddWithValue("@Company", company)
            comm2.CommandTimeout = 157200
            Dim sdat2 As New SqlDataAdapter(comm2)
            sdat2.Fill(strTraningCost)
            conn2.Close()
        End Using
        If strTraningCost.Tables(0).Rows.Count > 0 Then
            Dim c As Integer = strTraningCost.Tables(0).Rows.Count
            Dim TraningCost(c) As String
            Dim TraningCostScore(c) As String
            If c > 0 Then
                For i As Integer = 0 To c - 1
                    TraningCost(i) = Convert.ToString(strTraningCost.Tables(0).Rows(i)("Office"))
                    TraningCostScore(i) = Convert.ToString(strTraningCost.Tables(0).Rows(i)("Cost"))
                Next
            End If
            TraningCostName = String.Format("'{0}'", String.Join("','", TraningCost)).Replace("''", "")
            TraningCostNameScore = String.Join(",", (TraningCostScore))


        End If


        'TraningHour
        'Dim strTraningHour As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "sp_Get_TraningHours_Hr_dashboard", Session("UserEmpID"))
        Dim strTraningHour As New DataSet
        Using conn2 As New SqlConnection(WebConfig.ConnectionString)
            Dim comm2 As New SqlCommand("sp_Get_TraningHours_Hr_dashboard", conn2)
            comm2.CommandType = CommandType.StoredProcedure
            'comm2.Parameters.AddWithValue("@Company", Session("UserEmpID"))
            comm2.Parameters.AddWithValue("@Company", company)
            comm2.CommandTimeout = 157200
            Dim sdat2 As New SqlDataAdapter(comm2)
            sdat2.Fill(strTraningHour)
            conn2.Close()
        End Using
        If strTraningHour.Tables(0).Rows.Count > 0 Then
            Dim c As Integer = strTraningHour.Tables(0).Rows.Count
            Dim TraningHour(c) As String
            Dim TraningHourScore(c) As String
            If c > 0 Then
                For i As Integer = 0 To c - 1
                    TraningHour(i) = Convert.ToString(strTraningHour.Tables(0).Rows(i)("Office"))
                    TraningHourScore(i) = Convert.ToString(strTraningHour.Tables(0).Rows(i)("Hour"))
                Next
            End If
            TraningHourName = String.Format("'{0}'", String.Join("','", TraningHour)).Replace("''", "")
            TraningHourNameScore = String.Join(",", (TraningHourScore))


        End If



        'TraningTypeHour
        'Dim strTraningTypeHour As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "sp_Get_TraningTypesHour_Hr_dashboard", Session("UserEmpID"))
        Dim strTraningTypeHour As New DataSet
        Using conn2 As New SqlConnection(WebConfig.ConnectionString)
            Dim comm2 As New SqlCommand("sp_Get_TraningTypesHour_Hr_dashboard", conn2)
            comm2.CommandType = CommandType.StoredProcedure
            'comm2.Parameters.AddWithValue("@Company", Session("UserEmpID"))
            comm2.Parameters.AddWithValue("@Company", company)
            comm2.CommandTimeout = 157200
            Dim sdat2 As New SqlDataAdapter(comm2)
            sdat2.Fill(strTraningTypeHour)
            conn2.Close()
        End Using
        If strTraningTypeHour.Tables(0).Rows.Count > 0 Then
            Dim c As Integer = strTraningTypeHour.Tables(0).Rows.Count
            Dim TraningTypeHour(c) As String
            Dim TraningTypeHourScore(c) As String
            If c > 0 Then
                For i As Integer = 0 To c - 1
                    TraningTypeHour(i) = Convert.ToString(strTraningTypeHour.Tables(0).Rows(i)("TrainingType"))
                    TraningTypeHourScore(i) = Convert.ToString(strTraningTypeHour.Tables(0).Rows(i)("Hour"))
                Next
            End If
            TraningTypeHourName = String.Format("'{0}'", String.Join("','", TraningTypeHour)).Replace("''", "")
            TraningTypeHourNameScore = String.Join(",", (TraningTypeHourScore))


        End If


        'CompletionStatus
        'Dim strCompletionStatus As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "sp_Get_CompletionStatus_Hr_dashboard", Session("UserEmpID"))
        Dim strCompletionStatus As New DataSet
        Using conn2 As New SqlConnection(WebConfig.ConnectionString)
            Dim comm2 As New SqlCommand("sp_Get_CompletionStatus_Hr_dashboard", conn2)
            comm2.CommandType = CommandType.StoredProcedure
            'comm2.Parameters.AddWithValue("@Company", Session("UserEmpID"))
            comm2.Parameters.AddWithValue("@Company", company)
            comm2.CommandTimeout = 157200
            Dim sdat2 As New SqlDataAdapter(comm2)
            sdat2.Fill(strCompletionStatus)
            conn2.Close()
        End Using
        If strCompletionStatus.Tables(0).Rows.Count > 0 Then
            Dim c As Integer = strCompletionStatus.Tables(0).Rows.Count
            Dim CompletionStatus(c) As String
            Dim CompletionStatusScore(c) As String
            If c > 0 Then
                For i As Integer = 0 To c - 1
                    CompletionStatus(i) = Convert.ToString(strCompletionStatus.Tables(0).Rows(i)("Office"))
                    CompletionStatusScore(i) = Convert.ToString(strCompletionStatus.Tables(0).Rows(i)("Status"))
                Next
            End If
            CompletionStatusName = String.Format("'{0}'", String.Join("','", CompletionStatus)).Replace("''", "")
            CompletionStatusNameScore = String.Join(",", (CompletionStatusScore))


        End If
    End Sub

    Protected Sub cboCompany_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboCompany.SelectedIndexChanged
        Try
            'search.Value = ""
            'Session("LoadType") = "All"
            'LoadGrid()
            Dim oComapany As String = ""
            oComapany = cboCompany.Text 'Or oComapany = cboCompany.SelectedValue
            loadDashBoard(oComapany)
        Catch ex As Exception
            'Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
End Class