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


Public Class DummyMisc
    Inherits System.Web.UI.Page
    Public queryno, genderm, genderf, sid, svar, out1, out2, out3, devid, devdetail, scheduledno, attendedno, jhistory, qn1, qn2 As String
    Public trainall, didfinal, dsave As List(Of String)


    Public Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ConnetionString = WebConfigName.ConnectionString  ' "Data Source=DESKTOP-IIG2VIO\GODP_IT;Initial Catalog=GOSHRM;Persist Security Info=True;User ID=sa;Password=godp"





        If Request.QueryString("getmngrpfmnc") Is Nothing Then


            'work history

            Dim sdhistory As SqlDataReader = SqlHelper.ExecuteReader(ConnetionString, CommandType.Text, "select * from Emp_Work_History  where EmpID = '" & Request.QueryString("eid") & "'")

            Dim dlist As New ArrayList
            'get workhistory
            While sdhistory.Read()
                Dim list As New List(Of String)(New String() {sdhistory.Item("JobTitle").ToString(), sdhistory.Item("GradeLevel").ToString(), sdhistory.Item("Office").ToString(), sdhistory.Item("StartDate").ToString(), sdhistory.Item("StartYear").ToString(), sdhistory.Item("EndDate").ToString()})
                dlist.Add(list)

            End While

            Dim jh As List(Of String)
            jh = dlist(0)

            jhistory = jh(0) & "," & jh(1) & "," & jh(2) & "," & jh(3) & "," & jh(4) & "," & jh(5)






            'devplan
            
            Dim sdr3 As SqlDataReader = SqlHelper.ExecuteReader(ConnetionString, CommandType.Text, "select * from Performance_Development_Plan  where EmpID = '" & Request.QueryString("eid") & "'")



            'get successionid
            While sdr3.Read
                devid = sdr3.Item("id")
                ' dsave.Add(sdr3.Item("id"))
            End While





            Dim sdr4 As SqlDataReader = SqlHelper.ExecuteReader(ConnetionString, CommandType.Text, "select * from Performance_Development_Plan_Detail  where DevPlanID = '" & devid & "'")


            'get successionid
            While sdr4.Read
                devdetail = sdr4.Item("interventiontype")
            End While




             








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


                If String.Equals(TDR.Item("Employee"), Request.QueryString("name") & " [" & Session("Organisation") & "]") Then
                    trlist.Add(TDR.Item("Training Session") & "-" & TDR.Item("Status") & "/" & TDR.Item("id"))
                End If


            Next

            trainall = trlist










            'get succession


            Dim sdrq1 As SqlDataReader = SqlHelper.ExecuteReader(ConnetionString, CommandType.Text, "select Count(*) as rc from Performance_Employee_Query  where EmpID = '" & Request.QueryString("eid") & "' and ROStatus='Complete'")

            Dim sdrq2 As SqlDataReader = SqlHelper.ExecuteReader(ConnetionString, CommandType.Text, "select Count(*) as rc from Performance_Employee_Query  where EmpID = '" & Request.QueryString("eid") & "' and ROStatus='In-progress'")

            Dim sdr1 As SqlDataReader = SqlHelper.ExecuteReader(ConnetionString, CommandType.Text, "select * from Recruitment_Succession_Detail_Responsibility  where empid = '" & Request.QueryString("eid") & "'")

            Dim sdrs As SqlDataReader = SqlHelper.ExecuteReader(ConnetionString, CommandType.Text, "select * from Recruitment_Succession where empid = '" & Request.QueryString("eid") & "'")


            'number of queries
            While sdrq1.Read
                'queryno.Add("Completed : " & sdrq1.Item("rc"))
                qn1 = "Completed : " & sdrq1.Item("rc")
            End While



            While sdrq2.Read
                'queryno.Add("In- Progress : " & sdrq2.Item("rc"))
                qn2 = "In Progress : " & sdrq2.Item("rc")
            End While

            queryno = qn1 & "," & qn2

            'succession title etc
            While sdrs.Read

                'Dim list1 As New List(Of String)(New String() {sdrs.Item("plannedjobtitle").ToString, sdrs.Item("plannedjobgrade").ToString, sdrs.Item("performancerating").ToString})
                out1 = sdrs.Item("plannedjobtitle").ToString & "," & sdrs.Item("finalstatus").ToString
                sid = sdrs.Item("id")
            End While



            'Dim serializer As New Script.Serialization.JavaScriptSerializer()
            ''get successionid
            'While sdr1.Read
            '    sid = sdr1.Item("successiondetailid")
            'End While


            If String.IsNullOrEmpty(sid) Then
                sid = 0
            End If


            Dim CMD As New SqlCommand("Recruitment_Succession_Detail_Get_All")

            CMD.Parameters.AddWithValue("@sid", sid)

            Dim connection As New SqlConnection(ConnetionString)
            CMD.Connection = connection
            CMD.CommandType = CommandType.StoredProcedure

            Dim adapter As New SqlDataAdapter(CMD)
            adapter.SelectCommand.CommandTimeout = 300

            'Fill the dataset
            Dim DS As New DataSet
            adapter.Fill(DS)
            connection.Close()

            Dim nlist, tlist As New List(Of String)()

            For Each DR As DataRow In DS.Tables(0).Rows
                'Dim list2 As New List(Of String)(New String() {DR.Item("action").ToString, DR.Item("status").ToString})
                out2 = DR.Item("action").ToString & "," & sid
            Next





            'top performance

            Dim sdrpfm1 As SqlDataReader = SqlHelper.ExecuteReader(ConnetionString, CommandType.Text, "select * from Performance_Appraisal_Summary  inner join Performance_Appraisal_Cycle on  Performance_Appraisal_Summary.AppraisalCycleID =Performance_Appraisal_Cycle.id where Performance_Appraisal_Summary.EmpID = '" & Request.QueryString("eid") & "'")

            Dim pfm1list As New List(Of String)()
            While sdrpfm1.Read()
                pfm1list.Add(sdrpfm1.Item("Period") & ":" & sdrpfm1.Item("Score"))

            End While






            'out3 = serializer.Serialize(out2)
            out3 = out1 & "," & out2 & "-" & devdetail & "," & scheduledno & "," & attendedno & "*" & queryno & "-" & jhistory & "*" & String.Join(",", trainall) & "*" & String.Join(",", pfm1list)












        ElseIf Request.QueryString("getmngrpfmnc") = "yes" Then





            'graph performance, I'm doing oroju with the records ;)

            Dim sdrpfm1 As SqlDataReader = SqlHelper.ExecuteReader(ConnetionString, CommandType.Text, "select * from Performance_Appraisal_Summary  where CoachID = '" & Session("UserEmpID") & "'")

            Dim pfm1list As New List(Of String)()
            While sdrpfm1.Read()


                Dim sdrpfm2 As SqlDataReader = SqlHelper.ExecuteReader(ConnetionString, CommandType.Text, "select * from Employees_All  where EmpID = '" & sdrpfm1.Item("EmpID") & "'")

                While sdrpfm2.Read()



                    Dim sdrpfm3 As SqlDataReader = SqlHelper.ExecuteReader(ConnetionString, CommandType.Text, "select * from Performance_Appraisal_Cycle  where id = '" & sdrpfm1.Item("AppraisalCycleID") & "'")

                    While sdrpfm3.Read()
                        pfm1list.Add(sdrpfm2.Item("Name") & "*" & sdrpfm1.Item("Score") & "*" & sdrpfm3.Item("StartPeriod") & ">" & sdrpfm3.Item("EndPeriod"))
                    End While



                End While

            End While


            out3 = String.Join(",", pfm1list)




        ElseIf Request.QueryString("getmngrpfmnc") = "gender" Then

            Dim sdrpfm4 As SqlDataReader = SqlHelper.ExecuteReader(ConnetionString, CommandType.Text, "select Count(*) as hm from Employees_all  where CoachID = '" & Session("UserEmpID") & "' and Gender ='Male'")
            Dim sdrpfm5 As SqlDataReader = SqlHelper.ExecuteReader(ConnetionString, CommandType.Text, "select Count(*) as hf from Employees_all  where CoachID = '" & Session("UserEmpID") & "' and Gender ='Female'")

            While sdrpfm4.Read()
                genderm = sdrpfm4.Item("hm")
            End While

            While sdrpfm5.Read()
                genderf = sdrpfm5.Item("hf")
            End While


            out3 = genderm & "," & genderf


        Else


            out3 = "ll"
        End If









    End Sub


End Class






