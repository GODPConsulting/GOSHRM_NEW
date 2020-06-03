Imports Microsoft.ApplicationBlocks.Data
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Collections.Generic
Imports System.Web.UI.WebControls
Imports Telerik.Web.UI
Imports Telerik.Charting
Imports Telerik.Web.UI.HtmlChart.PlotArea
Imports Telerik.Web.UI.HtmlChart
Imports Telerik.Web.UI.HtmlChart.Enums
Imports GOSHRM.GOSHRM.GOSHRM.BO

Imports GOSHRM.GOSHRM.GOSHRM.DAT

Public Class EmpDash
    Inherits System.Web.UI.Page
    Public cjtitle, anlp, dpa As String
    Public sclp, trainall, jhis, perf, avgpfm As List(Of String)
    Protected lv, ln, dpln, perf2, avg2, lot, performanceb, pfm360 As String
    Protected ttime As Long




    Public Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ConnetionString = WebConfigName.ConnectionString  ' "Data Source=DESKTOP-IIG2VIO\GODP_IT;Initial Catalog=GOSHRM;Persist Security Info=True;User ID=sa;Password=godp"
        Dim output1 As New List(Of String)()




        Dim sdr1 As SqlDataReader = SqlHelper.ExecuteReader(ConnetionString, CommandType.Text, "select * from Recruitment_Succession where empid = '" & Session("UserEmpID") & "'")


        While sdr1.Read
            anlp = sdr1.Item("plannedjobtitle").ToString
            cjtitle = sdr1.Item("jobtitle").ToString
        End While

        Dim sdr2 As SqlDataReader = SqlHelper.ExecuteReader(ConnetionString, CommandType.Text, "select * from Job_Title_Skills where jobtitleid in (select id from Job_Titles where name= '" & cjtitle & "')")


        While sdr2.Read
            output1.Add(sdr2.Item("skills").ToString)

        End While

        'sclp = String.Join(",", output1)
        sclp = output1




        'training

        Dim trdat As New SqlCommand("Employee_Training_Sessions_get_all")

        trdat.Parameters.AddWithValue("@company", Session("Organisation"))

        Dim connman As New SqlConnection(ConnetionString)
        trdat.Connection = connman
        trdat.CommandType = CommandType.StoredProcedure

        Dim trainadapt As New SqlDataAdapter(trdat)


        'Fill the dataset
        Dim trdat1 As New DataSet

        trainadapt.Fill(trdat1)


        Dim trlist As New List(Of String)()

        For Each TDR As DataRow In trdat1.Tables(0).Rows


            If String.Equals(TDR.Item("Employee"), Session("EmpName") & " [" & Session("Organisation") & "]") Then
                trlist.Add(TDR.Item("Training Session") & "-" & TDR.Item("Status"))
            End If


        Next

        trainall = trlist



        'dev plan

        Dim sdr3 As SqlDataReader = SqlHelper.ExecuteReader(ConnetionString, CommandType.Text, "select * from Performance_Development_Plan_Detail  where DevPlanID in (select id from Performance_Development_Plan  where empid = '" & Session("UserEmpID") & "')")
        Dim dlist As New List(Of String)()

        While sdr3.Read
            dlist.Add(sdr3.Item("interventiontype"))
        End While


        dpa = String.Join(",", dlist)





        'work history

        Dim sdhistory As SqlDataReader = SqlHelper.ExecuteReader(ConnetionString, CommandType.Text, "select * from Emp_Work_History  where EmpID = '" & Session("UserEmpID") & "'")

        Dim hlist As New ArrayList







        While sdhistory.Read()
            Dim list As New List(Of String)(New String() {sdhistory.Item("JobTitle").ToString(), sdhistory.Item("GradeLevel").ToString(), sdhistory.Item("Office").ToString(), sdhistory.Item("StartDate").ToString(), sdhistory.Item("StartYear").ToString(), sdhistory.Item("EndDate").ToString()})
            hlist.Add(list)

            ttime = DateDiff(DateInterval.Month, DateTime.Parse(sdhistory.Item("StartWork")), DateTime.Parse(sdhistory.Item("EndWork")))


        End While

 
        jhis = hlist(0)
        ' lot = ttime.ToString()



        'The requests


        'loan

        Dim sdrloan As SqlDataReader = SqlHelper.ExecuteReader(ConnetionString, CommandType.Text, "select Count(*) as rc from Emp_Loan  where EmpID = '" & Session("UserEmpID") & "' and Status='Pending'")


        While sdrloan.Read()
            ln = sdrloan.Item("rc")

        End While



        'leave

        Dim sdrleave As SqlDataReader = SqlHelper.ExecuteReader(ConnetionString, CommandType.Text, "select Count(*) as rc from Employee_Leavelist  where EmpID = '" & Session("UserEmpID") & "' and Status='Pending'")


        While sdrleave.Read()
            lv = sdrleave.Item("rc")

        End While



        'devplan

        Dim sdrdevplan As SqlDataReader = SqlHelper.ExecuteReader(ConnetionString, CommandType.Text, "select Count(*) as rc from Performance_Development_Plan  where EmpID = '" & Session("UserEmpID") & "' and ApprovalStatus='Pending'")


        While sdrdevplan.Read()
            dpln = sdrdevplan.Item("rc")

        End While


        '360 appraisal

        Dim sdr360 As SqlDataReader = SqlHelper.ExecuteReader(ConnetionString, CommandType.Text, "select Count(*) as rc from Performance_Appraisal_360  where empid = '" & Session("UserEmpID") & "' and stat='pending'")



        While sdr360.Read()
            pfm360 = sdr360.Item("rc")

        End While





        'performance count

        Dim sdrpfm As SqlDataReader = SqlHelper.ExecuteReader(ConnetionString, CommandType.Text, "select Count(*) as rc from Performance_Appraisal_Summary  where EmpID = '" & Session("UserEmpID") & "' and CoachApprovalStatus='Pending'")


        While sdrpfm.Read()
            perf2 = sdrpfm.Item("rc")

        End While






        'feedback performance

        Dim dats10 As New SqlCommand("Performance_Appraisal_Summary_SecondReview_Get_All")

        dats10.Parameters.AddWithValue("@EmpID", Session("UserEmpID"))


        dats10.Parameters.AddWithValue("@year", Now.ToString("yyyy"))

        Dim connection1 As New SqlConnection(ConnetionString)

        dats10.Connection = connection1
        dats10.CommandType = CommandType.StoredProcedure

        Dim adapters10 As New SqlDataAdapter(dats10)


        'Fill the dataset
        Dim dds10 As New DataTable

        adapters10.Fill(dds10)

        Dim dstr As String

        performanceb = dds10.Rows.Count.ToString()



      







        'top performance

        Dim sdrpfm1 As SqlDataReader = SqlHelper.ExecuteReader(ConnetionString, CommandType.Text, "select * from Performance_Appraisal_Summary  inner join Performance_Appraisal_Cycle on  Performance_Appraisal_Summary.AppraisalCycleID =Performance_Appraisal_Cycle.id where Performance_Appraisal_Summary.EmpID = '" & Session("UserEmpID") & "'")

        Dim pfm1list As New List(Of String)()
        While sdrpfm1.Read()
            pfm1list.Add(sdrpfm1.Item("Period") & " : " & sdrpfm1.Item("Score"))

        End While


        perf = pfm1list




        'avg performance

        Dim sdrpfm2 As SqlDataReader = SqlHelper.ExecuteReader(ConnetionString, CommandType.Text, "select Score from Performance_Appraisal_Summary where EmpID = '" & Session("UserEmpID") & "'")

        Dim pfm1list2 As New List(Of String)()
        While sdrpfm2.Read()
            pfm1list2.Add(sdrpfm2.Item("Score"))

        End While

        avgpfm = pfm1list2

        For Each x In pfm1list2

            avg2 = Convert.ToDouble(avg2) + Convert.ToDouble(x)

        Next










    End Sub


End Class






