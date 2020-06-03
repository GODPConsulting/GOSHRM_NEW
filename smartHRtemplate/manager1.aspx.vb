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


Public Class DummyGetter    
    Inherits System.Web.UI.Page
    Public train, per, job, inter, emp, tod, cid, edept As String
    Public ename, ecompany, edepartment, ejob, egrade, eimg, mlist As New List(Of String)()
    Public requisition, wfplanning, performancea, performanceb, devplan, training, jobexit, promotion, loan, leave As String
    Public wfplanning2, requisition2, successplan, promotion2, queries, interviews As String
    Dim cs As String = ConfigurationManager.ConnectionStrings("GOSHRMConnectionString").ConnectionString

    

    Public Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ConnetionString = WebConfigName.ConnectionString '"Data Source=DESKTOP-IIG2VIO\GODP_IT;Initial Catalog=GOSHRM;Persist Security Info=True;User ID=sa;Password=godp"
        Dim output As New List(Of String)()



        'first compute the numbers below the page


        Dim dat1 As New SqlCommand("Recruit_Job_Requisition_Approval_all")
        'CMD.Parameters("@EmpID").Value = "ABC0001"
        dat1.Parameters.AddWithValue("@EmpID", Session("UserEmpID"))
        dat1.Parameters.AddWithValue("@Status", "Pending")


        Dim connection1 As New SqlConnection(ConnetionString)
        dat1.Connection = connection1
        dat1.CommandType = CommandType.StoredProcedure

        Dim adapter1 As New SqlDataAdapter(dat1)


        'Fill the dataset
        Dim dd1 As New DataTable

        adapter1.Fill(dd1)


        requisition = dd1.Rows.Count.ToString()









        'starting the planning and request part

        Dim dat2 As New SqlCommand("Recruit_WorkForce_Plan_get_all")

        dat2.Parameters.AddWithValue("@EmpID", Session("UserEmpID"))
        dat2.Parameters.AddWithValue("@Status", "Pending")
        dat2.Parameters.AddWithValue("@year", Now.ToString("yyyy"))



        dat2.Connection = connection1
        dat2.CommandType = CommandType.StoredProcedure

        Dim adapter2 As New SqlDataAdapter(dat2)


        'Fill the dataset
        Dim dd2 As New DataTable

        adapter2.Fill(dd2)


        wfplanning2 = dd2.Rows.Count.ToString()






        Dim dat3 As New SqlCommand("Recruit_Job_Requisition_get_all")

        dat3.Parameters.AddWithValue("@EmpID", Session("UserEmpID"))
        dat3.Parameters.AddWithValue("@Status", "Pending")



        dat3.Connection = connection1
        dat3.CommandType = CommandType.StoredProcedure

        Dim adapter3 As New SqlDataAdapter(dat3)


        'Fill the dataset
        Dim dd3 As New DataTable

        adapter3.Fill(dd3)


        requisition2 = dd3.Rows.Count.ToString()




        Dim dat4 As New SqlCommand("Recruitment_Succession_Get_All_Manager")

        dat4.Parameters.AddWithValue("@EmpID", Session("UserEmpID"))




        dat4.Connection = connection1
        dat4.CommandType = CommandType.StoredProcedure

        Dim adapter4 As New SqlDataAdapter(dat4)


        'Fill the dataset
        Dim dd4 As New DataTable

        adapter4.Fill(dd4)


        successplan = dd4.Rows.Count.ToString()








        Dim dat5 As New SqlCommand("Recruitment_Promotion_Manager_Get_All")

        dat5.Parameters.AddWithValue("@empid", Session("UserEmpID"))




        dat5.Connection = connection1
        dat5.CommandType = CommandType.StoredProcedure

        Dim adapter5 As New SqlDataAdapter(dat5)


        'Fill the dataset
        Dim dd5 As New DataTable

        adapter5.Fill(dd5)


        promotion2 = dd5.Rows.Count.ToString()







        Dim dat6 As New SqlCommand("Performance_Employee_Query_Get_RO")

        dat6.Parameters.AddWithValue("@empid", Session("UserEmpID"))




        dat6.Connection = connection1
        dat6.CommandType = CommandType.StoredProcedure

        Dim adapter6 As New SqlDataAdapter(dat6)


        'Fill the dataset
        Dim dd6 As New DataTable

        adapter6.Fill(dd6)


        queries = dd6.Rows.Count.ToString()






        Dim dat7 As New SqlCommand("Recruit_Job_Interview_Comment_get_all")

        dat7.Parameters.AddWithValue("@empid", Session("UserEmpID"))




        dat7.Connection = connection1
        dat7.CommandType = CommandType.StoredProcedure

        Dim adapter7 As New SqlDataAdapter(dat7)


        'Fill the dataset
        Dim dd7 As New DataTable

        adapter6.Fill(dd7)


        interviews = dd7.Rows.Count.ToString()



        'begin approvals again

        Dim dats8 As New SqlCommand("Recruit_WorkForce_Approval_Get_All")

        dats8.Parameters.AddWithValue("@EmpID", Session("UserEmpID"))

        dats8.Parameters.AddWithValue("@company", Session("Organisation"))
        dats8.Parameters.AddWithValue("@Status", "Pending")
        dats8.Parameters.AddWithValue("@entry", "Plan")




        dats8.Connection = connection1
        dats8.CommandType = CommandType.StoredProcedure

        Dim adapters8 As New SqlDataAdapter(dats8)


        'Fill the dataset
        Dim dds8 As New DataTable

        adapters8.Fill(dds8)


        wfplanning = dds8.Rows.Count.ToString()








        Dim dats9 As New SqlCommand("Performance_Appraisal_Summary_Coach_Get_All")

        dats9.Parameters.AddWithValue("@empid", Session("UserEmpID"))


        dats9.Parameters.AddWithValue("@year", Now.ToString("yyyy"))





        dats9.Connection = connection1
        dats9.CommandType = CommandType.StoredProcedure

        Dim adapters9 As New SqlDataAdapter(dats9)


        'Fill the dataset
        Dim dds9 As New DataTable

        adapters9.Fill(dds9)


        performancea = dds9.Rows.Count.ToString()




        Dim dats10 As New SqlCommand("Performance_Appraisal_Summary_SecondReview_Get_All")

        dats10.Parameters.AddWithValue("@EmpID", Session("UserEmpID"))


        dats10.Parameters.AddWithValue("@year", Now.ToString("yyyy"))

        dats10.Connection = connection1
        dats10.CommandType = CommandType.StoredProcedure

        Dim adapters10 As New SqlDataAdapter(dats10)


        'Fill the dataset
        Dim dds10 As New DataTable

        adapters10.Fill(dds10)


        performanceb = dds10.Rows.Count.ToString()





        Dim dats11 As New SqlCommand("Performance_Development_Plan_Get_Coach")

        dats11.Parameters.AddWithValue("@EmpID", Session("UserEmpID"))
        dats11.Parameters.AddWithValue("@Status", "Pending")



        dats11.Connection = connection1
        dats11.CommandType = CommandType.StoredProcedure

        Dim adapters11 As New SqlDataAdapter(dats11)


        'Fill the dataset
        Dim dds11 As New DataTable

        adapters11.Fill(dds11)


        devplan = dds11.Rows.Count.ToString()






        Dim dats12 As New SqlCommand("Training_Sessions_Request_Get_Surbodinate")

        dats12.Parameters.AddWithValue("@empid", Session("UserEmpID"))




        dats12.Connection = connection1
        dats12.CommandType = CommandType.StoredProcedure

        Dim adapters12 As New SqlDataAdapter(dats12)


        'Fill the dataset
        Dim dds12 As New DataTable

        adapters12.Fill(dds12)


        training = dds12.Rows.Count.ToString()



        Dim dats13 As New SqlCommand("Emp_Termination_Get_Surbodinate_Employee")

        dats13.Parameters.AddWithValue("@empid", Session("UserEmpID"))
        dats13.Parameters.AddWithValue("@status", "Pending")
        dats13.Parameters.AddWithValue("@startdate", DateTime.Now.AddYears(-1).ToShortDateString())
        dats13.Parameters.AddWithValue("@enddate", FormatDateTime(Now, DateFormat.ShortDate))




        dats13.Connection = connection1
        dats13.CommandType = CommandType.StoredProcedure

        Dim adapters13 As New SqlDataAdapter(dats13)


        'Fill the dataset
        Dim dds13 As New DataTable

        adapters13.Fill(dds13)


        jobexit = dds13.Rows.Count.ToString()










        Dim dats14 As New SqlCommand("Recruitment_Promotion_Approver_Get_All")

        dats14.Parameters.AddWithValue("@empid", Session("UserEmpID"))

        dats14.Connection = connection1
        dats14.CommandType = CommandType.StoredProcedure

        Dim adapters14 As New SqlDataAdapter(dats14)


        'Fill the dataset
        Dim dds14 As New DataTable

        adapters14.Fill(dds14)
        promotion = dds13.Rows.Count.ToString()






        Dim dats15 As New SqlCommand("Emp_Loan_Approver_get_all")

        dats15.Parameters.AddWithValue("@empid", Session("UserEmpID"))
        dats15.Parameters.AddWithValue("@status", "Pending")
        dats15.Parameters.AddWithValue("@DateFrom", DateTime.Now.AddYears(-1).ToShortDateString())
        dats15.Parameters.AddWithValue("@DateTo", FormatDateTime(Now, DateFormat.ShortDate))




        dats15.Connection = connection1
        dats15.CommandType = CommandType.StoredProcedure

        Dim adapters15 As New SqlDataAdapter(dats15)


        'Fill the dataset
        Dim dds15 As New DataTable

        adapters15.Fill(dds15)


        loan = dds15.Rows.Count.ToString()







        Dim dats16 As New SqlCommand("Employee_Leavelist_Approver_Summary_get_all")

        dats16.Parameters.AddWithValue("@empid", Session("UserEmpID"))

        dats16.Parameters.AddWithValue("@DateFrom", DateTime.Now.AddYears(-1).ToShortDateString())
        dats16.Parameters.AddWithValue("@DateTo", FormatDateTime(Now, DateFormat.ShortDate))




        dats16.Connection = connection1
        dats16.CommandType = CommandType.StoredProcedure

        Dim adapters16 As New SqlDataAdapter(dats16)


        'Fill the dataset
        Dim dds16 As New DataTable

        adapters16.Fill(dds16)


        leave = dds16.Rows.Count.ToString()







        'finally close conn1
        connection1.Close()



        'Dim sdr As SqlDataReader = SqlHelper.ExecuteReader(ConnetionString, CommandType.Text, "Select CoachID from dbo.Employees_All a where EmpID = '" & Session("UserEmpID") & "'")



        Dim CMD As New SqlCommand("Emp_PersonalDetail_DirectReports")
        'CMD.Parameters("@EmpID").Value = "ABC0001"
        CMD.Parameters.AddWithValue("@EmpID", Session("UserEmpID"))

        Dim connection As New SqlConnection(ConnetionString)
        CMD.Connection = connection
        CMD.CommandType = CommandType.StoredProcedure

        Dim adapter As New SqlDataAdapter(CMD)
        adapter.SelectCommand.CommandTimeout = 300

        'Fill the dataset
        Dim DS As New DataSet
        adapter.Fill(DS)
        connection.Close()

        Dim serializer As New Script.Serialization.JavaScriptSerializer()
        Dim nlist, tlist As New List(Of String)()
        Dim dlist As New ArrayList
        For Each DR As DataRow In DS.Tables(0).Rows
            Dim list As New List(Of String)(New String() {DR.Item("First Name").ToString(), DR.Item("Last Name").ToString(), DR.Item("Company").ToString(), DR.Item("Office").ToString(), DR.Item("JobTitle").ToString(), DR.Item("GradeLevel").ToString(), DR.Item("EmpID").ToString()})
            dlist.Add(list)
        Next



        'per = serializer.Serialize(nlist)
        Session("alldetail") = dlist




        'per = FormatNumber(train.Tables(0).Rows.Count.ToString(), 0)

        'tod = Session("UserEmpID")


    End Sub


End Class






