Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports Microsoft.ApplicationBlocks.Data
Imports System.Web.Script.Serialization
Imports GOSHRM.GOSHRM.GOSHRM.BO

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="GOSHRM")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class gos
    Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function HelloWorld() As String
        Return "Hello World"
    End Function

    Public Class Corevalues
        Public Property ID As Integer
        Public Property AppID As Integer
        Public Property count As Integer
        Public Property Name As String
        Public Property cat As String
        Public Property kpi As String
        Public Property key As String
        Public Property suc As String
        Public Property tdate As String
        Public Property tdates As DateTime
        Public Property aweight As String
        Public Property w_model As String
        Public Property obj As String
        Public Property agree As String
        Public Property EmpSetObj As String
        Public Property UploadStatus As String
    End Class
    Public Class PerfomancePoints
        Public Property points As Integer
        Public Property name As String
        Public Property desc As String
    End Class

    Public Class companyObj
        Public Property workforceplan As Integer
        Public Property staffRequest As Integer
        Public Property jobPortal As Integer
        Public Property recruitmentTest As Integer
        Public Property interview As Integer
        Public Property employeeDataset As Integer
        Public Property employeeConfirmation As Integer
        Public Property successionPlan As Integer
        Public Property promotion As Integer
        Public Property employeeExit As Integer
        Public Property hmo As Integer
        Public Property workforceCount As Integer
        Public Property turnoverCount As Decimal
        Public Property performanceRating As Decimal
        Public Property compentenceRating As Integer
        Public Property queries As Integer
        Public Property payroll As Integer
        Public Property terminalBenefit As Integer
        Public Property staffLoan As Integer
        Public Property leaveAllowance As Integer
        Public Property overTimeRequest As Integer
    End Class
    Public Class EmpComment
        Public Property performanceid As Integer
        Public Property radEnddate As Date
        Public Property obj As String
        Public Property Kpiid As String
        Public Property empid As String
    End Class
    Public Class EmpComment1
        Public Property performanceid As Integer
        Public Property radEnddate As Date
        Public Property obj As String
        Public Property Kpiid As String
        Public Property empid As String
        Public Property obj2 As String
    End Class
    Public Class EmpGetComment
        Public Property pid As Integer
        Public Property empid As String
    End Class
    Public Class transfers
        Public Property recipient As String
        Public Property amount As String
    End Class

    Public Class recipient
        Public Property type As String
        Public Property name As String
        Public Property description As String
        Public Property account_number As String
        Public Property bank_code As String
        Public Property currency As String
        Public Property recipient_code As String
        'Public Property recipient As String
        'Public Property amount As String
        Public Property source As String

        Public transfers As List(Of transfers)
    End Class
    Public Class EmployeePerformanceGrade
        Public Property Pid As Integer
        Public Property Points As String
        Public Property userid As String
        Public Property empid As String
        Public Property rev1id As String
        Public Property rev2id As String
        Public Property kpiobjectives As String
        Public Property jobgrade As String

    End Class


    Public Class recipient2
        Public Property currency As String
        Public Property source As String
        Public transfers As List(Of transfers)
    End Class
    Public Class DayList
        Public Property day As String
    End Class
    Public Class EmployeesData
        Public Property Presentday As String
        Public Property AbsentDay As String
        Public Property Leaveday As String
        Public Property Overtime As String
        Public Property AttendanceRate As String
        Public Property LeaveTaken As String
        Public Property LeaveRequest As String
        Public Property Performance As String
    End Class


    <WebMethod()> _
    Public Sub getRecipient(ByVal pid As Integer)
        Dim listRecipients As List(Of recipient) = New List(Of recipient)()
        Dim strTest As DataSet
        strTest = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Finance_Payslip_Get_All", pid, "")
        Dim i As Integer = 0
        Dim count As Integer = 1

        If strTest.Tables(0).Rows.Count > 0 Then
            For Each dr As DataRow In strTest.Tables(0).Rows
                Try
                    Dim prog As recipient = New recipient()
                    prog.name = Convert.ToString(strTest.Tables(0).Rows(i)("Name"))
                    prog.description = Convert.ToString(strTest.Tables(0).Rows(i)("EmpID"))
                    prog.account_number = Convert.ToString(strTest.Tables(0).Rows(i)("accountnumber"))
                    prog.bank_code = Convert.ToString(strTest.Tables(0).Rows(i)("BankCode"))
                    prog.currency = "NGN"
                    prog.type = "nuban"

                    listRecipients.Add(prog)
                    i += 1
                    count += 1
                Catch Ex As Exception
                    Context.Response.Write(Ex.Message)
                End Try
            Next
        End If

        Dim js As JavaScriptSerializer = New JavaScriptSerializer()
        Context.Response.Write(js.Serialize(listRecipients))
    End Sub

    <WebMethod()> _
    Public Sub payRecipient(ByVal pid As Integer)
        Dim listTransfers As List(Of transfers) = New List(Of transfers)()
        Dim strTest As DataSet
        strTest = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Finance_Payslip_Get_All", pid, "")
        Dim i As Integer = 0
        Dim count As Integer = 1

        If strTest.Tables(0).Rows.Count > 0 Then
            For Each dr As DataRow In strTest.Tables(0).Rows
                Try
                    Dim prog As transfers = New transfers()
                    prog.recipient = Convert.ToString(strTest.Tables(0).Rows(i)("RecipientCode"))
                    Dim amount As Decimal = (Convert.ToDecimal(strTest.Tables(0).Rows(i)("Net Pay")) * 100)
                    amount = Convert.ToInt32(amount)
                    prog.amount = Convert.ToString(amount)

                    listTransfers.Add(prog)
                    i += 1
                    count += 1
                Catch Ex As Exception
                    Context.Response.Write(Ex.Message)
                End Try
            Next
        End If

        Dim recipients As recipient2 = New recipient2()
        recipients.currency = "NGN"
        recipients.source = "balance"
        recipients.transfers = listTransfers
        Dim js As JavaScriptSerializer = New JavaScriptSerializer()
        Context.Response.Write(js.Serialize(recipients))
    End Sub

    <WebMethod()> _
    Public Sub updatePaymentStatus(ByVal pid As Integer)
        Dim listRecipients As List(Of transfers) = New List(Of transfers)()
        Dim strTest As Integer
        strTest = SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Finance_Payslip_Update_payment", pid)
    End Sub


    <WebMethod()> _
    Public Sub updateRecipient(emp As recipient)
        Try
            Dim empid As String = emp.description
            Dim Rcode As String = emp.recipient_code
            Dim strTest As Integer
            strTest = SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Finance_Payslip_Recipient_code_update", empid, Rcode)
        Catch Ex As Exception
            Context.Response.Write(Ex.Message)
        End Try
    End Sub

    <WebMethod()> _
    Public Sub getcorevaluesbyID(ByVal pid As Integer)
        Dim listProgrammes As List(Of Corevalues) = New List(Of Corevalues)()
        Dim canSetObj As String = "yes"
       
        Dim strTest As DataSet
        strTest = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Appraisal_Get", pid)
        Dim i As Integer = 0
        Dim count As Integer = 1

        If strTest.Tables(0).Rows.Count > 0 Then
            For Each dr As DataRow In strTest.Tables(0).Rows
                Try
                    Dim prog As Corevalues = New Corevalues()
                    prog.ID = Convert.ToInt32(strTest.Tables(0).Rows(i)("id"))
                    prog.AppID = Convert.ToInt32(strTest.Tables(0).Rows(i)("AppraisalSummaryID"))
                    prog.cat = Convert.ToString(strTest.Tables(0).Rows(i)("KPIType"))
                    prog.kpi = Convert.ToString(strTest.Tables(0).Rows(i)("KPIObjectives"))
                    prog.aweight = Convert.ToString(strTest.Tables(0).Rows(i)("customweight"))
                    prog.suc = Convert.ToString(strTest.Tables(0).Rows(i)("comment"))
                    prog.obj = Convert.ToString(strTest.Tables(0).Rows(i)("objectives"))
                    prog.UploadStatus = Convert.ToString(strTest.Tables(0).Rows(i)("Upload_Status"))
                    If IsDBNull(strTest.Tables(0).Rows(i)("targetdate")) = False Then
                        prog.tdates = Convert.ToDateTime(strTest.Tables(0).Rows(i)("targetdate"))
                        prog.tdate = String.Format("{0: dd-MM-yyyy}", prog.tdates)
                    End If
                    prog.key = Convert.ToString(strTest.Tables(0).Rows(i)("AppraisalItem"))
                    prog.agree = Convert.ToString(strTest.Tables(0).Rows(i)("CoachApprovalStatus"))
                    Dim strWeight As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Competency_Group_Get", prog.cat)
                    If strWeight.Tables(0).Rows.Count > 0 Then
                        prog.EmpSetObj = strWeight.Tables(0).Rows(0).Item("EmpSetObjective").ToString.ToLower
                    End If
                    listProgrammes.Add(prog)
                    i += 1
                    count += 1
                Catch Ex As Exception
                    Context.Response.Write(Ex.Message)
                End Try
            Next
        End If

        Dim js As JavaScriptSerializer = New JavaScriptSerializer()
        Context.Response.Write(js.Serialize(listProgrammes))
    End Sub

    <WebMethod(EnableSession:=True)>
    Public Sub getcorevalues(ByVal pid As String)
        Dim listProgrammes As List(Of Corevalues) = New List(Of Corevalues)()
        Dim query As String = "select cj.Competency Name, cj.id from Competency a inner join Competency_Group b on a.CompetencyGroupID = b.id inner join Competency_JobGrade cj on a.Name = cj.Competency  where b.CompetencyType ='" & pid & "' and cj.JobGrade = '" & Session("UserJobgrade").ToString() & "'"
        Dim strTest As DataSet
        strTest = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, query)
        Dim i As Integer = 0
        Dim count As Integer = 1

        If strTest.Tables(0).Rows.Count > 0 Then
            For Each dr As DataRow In strTest.Tables(0).Rows
                Try
                    Dim prog As Corevalues = New Corevalues()
                    prog.ID = Convert.ToInt32(strTest.Tables(0).Rows(i)("id"))
                    prog.Name = Convert.ToString(strTest.Tables(0).Rows(i)("Name"))
                    listProgrammes.Add(prog)
                    i += 1
                    count += 1
                Catch Ex As Exception
                    Context.Response.Write(Ex.Message)
                End Try
            Next
        End If

        Dim js As JavaScriptSerializer = New JavaScriptSerializer()
        Context.Response.Write(js.Serialize(listProgrammes))
    End Sub


    <WebMethod()>
    Public Sub addCorevalues(ByVal emp As Corevalues)

        Dim canSetObj As String = "yes"
        Dim strWeight As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Competency_Group_Get", emp.cat)

        If strWeight.Tables(0).Rows.Count > 0 Then
            emp.w_model = strWeight.Tables(0).Rows(0).Item("weightmodel").ToString.ToLower
            canSetObj = strWeight.Tables(0).Rows(0).Item("EmpSetObjective").ToString.ToLower
        End If
        Dim appObj As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Appraisal_Summary_Get", emp.AppID)
        Dim discussed, empname, reviewyear, manager, managerID As String
        Dim complete As String = "No"
        If appObj.Tables(0).Rows.Count > 0 Then
            empname = appObj.Tables(0).Rows(0).Item("EmpName").ToString
            reviewyear = appObj.Tables(0).Rows(0).Item("period").ToString
            manager = appObj.Tables(0).Rows(0).Item("CoachName").ToString
            complete = appObj.Tables(0).Rows(0).Item("Completed").ToString
            managerID = appObj.Tables(0).Rows(0).Item("CoachID").ToString
            discussed = appObj.Tables(0).Rows(0).Item("CoachApprovalStatus").ToString
        End If

        If emp.ID = 0 Then
            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_Update", emp.ID, emp.AppID, emp.cat, emp.kpi, emp.key, emp.tdate, emp.aweight, emp.suc, emp.obj, emp.w_model, emp.UploadStatus)
        Else
            If complete = "Yes" Then
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_Update", emp.ID, emp.AppID, emp.cat, emp.kpi, emp.key, emp.tdate, emp.aweight, emp.suc, emp.obj, emp.w_model, emp.UploadStatus)
                Process.Appraisal_Obj_UpdateKPI(manager, empname, reviewyear, emp.ID, managerID, Process.ApplicationURL + "/" + "Module/Employee/Performance/CoacheeAppraisalObjectives")
            Else
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_Update", emp.ID, emp.AppID, emp.cat, emp.kpi, emp.key, emp.tdate, emp.aweight, emp.suc, emp.obj, emp.w_model, emp.UploadStatus)
            End If
        End If
    End Sub

    <WebMethod(EnableSession:=True)>
    Public Sub addcomment(ByVal emp As EmpComment)
        Dim id = 0
        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_comment_add", id, emp.performanceid, emp.Kpiid, emp.obj, emp.radEnddate, emp.empid)

    End Sub

    <WebMethod()>
    Public Sub getcomment(ByVal pid As String, empid As String)
        Dim listRecipients As List(Of recipient) = New List(Of recipient)()
        Dim strTest As DataSet
        strTest = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_comment_get", pid, empid)
        Dim i As Integer = 0
        Dim count As Integer = 1

        If strTest.Tables(0).Rows.Count > 0 Then
            For Each dr As DataRow In strTest.Tables(0).Rows
                Try
                    Dim prog As recipient = New recipient()
                    prog.name = Convert.ToString(strTest.Tables(0).Rows(i)("Comment"))
                    prog.description = Convert.ToString(strTest.Tables(0).Rows(i)("Date"))


                    listRecipients.Add(prog)
                    i += 1
                    count += 1
                Catch Ex As Exception
                    Context.Response.Write(Ex.Message)
                End Try
            Next
            Dim js As JavaScriptSerializer = New JavaScriptSerializer()
            Context.Response.Write(js.Serialize(listRecipients))
        End If
    End Sub
    <WebMethod()>
    Public Sub getcommentmngr(ByVal pid As String, empid As String)
        Dim listRecipients As List(Of recipient) = New List(Of recipient)()
        Dim strTest As DataSet
        strTest = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_comment_get", pid, empid)
        Dim i As Integer = 0
        Dim count As Integer = 1

        If strTest.Tables(0).Rows.Count > 0 Then
            For Each dr As DataRow In strTest.Tables(0).Rows
                Try
                    Dim prog As recipient = New recipient()
                    prog.name = Convert.ToString(strTest.Tables(0).Rows(i)("Comment"))
                    prog.description = Convert.ToString(strTest.Tables(0).Rows(i)("Date"))


                    listRecipients.Add(prog)
                    i += 1
                    count += 1
                Catch Ex As Exception
                    Context.Response.Write(Ex.Message)
                End Try
            Next
            Dim js As JavaScriptSerializer = New JavaScriptSerializer()
            Context.Response.Write(js.Serialize(listRecipients))
        End If
    End Sub
    <WebMethod()>
    Public Sub getcommensupervisor(ByVal pid As String, empid As String)
        Dim listRecipients As List(Of recipient) = New List(Of recipient)()
        Dim strTest As DataSet
        strTest = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_comment_get", pid, empid)
        Dim i As Integer = 0
        Dim count As Integer = 1

        If strTest.Tables(0).Rows.Count > 0 Then
            For Each dr As DataRow In strTest.Tables(0).Rows
                Try
                    Dim prog As recipient = New recipient()
                    prog.name = Convert.ToString(strTest.Tables(0).Rows(i)("Comment"))
                    prog.description = Convert.ToString(strTest.Tables(0).Rows(i)("Date"))


                    listRecipients.Add(prog)
                    i += 1
                    count += 1
                Catch Ex As Exception
                    Context.Response.Write(Ex.Message)
                End Try
            Next
            Dim js As JavaScriptSerializer = New JavaScriptSerializer()
            Context.Response.Write(js.Serialize(listRecipients))
        End If
    End Sub
    <WebMethod()>
    Public Sub PerformPoints(ByVal pid As String)
        Dim listRecipients As List(Of PerfomancePoints) = New List(Of PerfomancePoints)()
        Dim strTest As DataSet
        strTest = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Points_Get_All")
        Dim i As Integer = 0
        Dim count As Integer = 1

        If strTest.Tables(0).Rows.Count > 0 Then
            For Each dr As DataRow In strTest.Tables(0).Rows
                Try
                    Dim prog As PerfomancePoints = New PerfomancePoints()
                    prog.points = Convert.ToString(strTest.Tables(0).Rows(i)("Point"))
                    prog.name = Convert.ToString(strTest.Tables(0).Rows(i)("PointName"))
                    prog.desc = Convert.ToString(strTest.Tables(0).Rows(i)("PointDesc"))


                    listRecipients.Add(prog)
                    i += 1
                    count += 1
                Catch Ex As Exception
                    Context.Response.Write(Ex.Message)
                End Try
            Next
            Dim js As JavaScriptSerializer = New JavaScriptSerializer()
            Context.Response.Write(js.Serialize(listRecipients))
        End If
    End Sub
    <WebMethod(EnableSession:=True)>
    Public Sub PerformanceSubmit(ByVal Performance As EmployeePerformanceGrade)
        Try

            Dim point = Decimal.Parse(Performance.Points)
            If Performance.userid = Performance.empid Then
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_Questionaire_Update_reviewee", Performance.Pid, point, Performance.jobgrade, Performance.kpiobjectives)
            ElseIf Performance.userid = Performance.rev1id Then
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_Questionaire_Update_reviewer", Performance.Pid, point, Performance.jobgrade, Performance.kpiobjectives)
            Else
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_Questionaire_Update_reviewer2", Performance.Pid, point, Performance.jobgrade, Performance.kpiobjectives)


            End If
        Catch ex As Exception
            Context.Response.Write(ex.Message)
        End Try

    End Sub
    <WebMethod()>
    Public Sub PerformObjectives(ByVal pid As String)
        Dim listRecipients As List(Of PerfomancePoints) = New List(Of PerfomancePoints)()
        Dim strTest As DataSet
        strTest = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Objectives_Get_All", pid)
        Dim i As Integer = 0
        Dim count As Integer = 1

        If strTest.Tables(0).Rows.Count > 0 Then
            For Each dr As DataRow In strTest.Tables(0).Rows
                Try
                    Dim prog As PerfomancePoints = New PerfomancePoints()
                    prog.name = Convert.ToString(strTest.Tables(0).Rows(i)("KPIObjectives"))

                    prog.desc = Convert.ToString(strTest.Tables(0).Rows(i)("KPIObjectives"))


                    listRecipients.Add(prog)
                    i += 1
                    count += 1
                Catch Ex As Exception
                    Context.Response.Write(Ex.Message)
                End Try
            Next
            Dim js As JavaScriptSerializer = New JavaScriptSerializer()
            Context.Response.Write(js.Serialize(listRecipients))
        End If
    End Sub
    <WebMethod(EnableSession:=True)>
    Public Sub addcomment1(ByVal emp As EmpComment1)
        Dim id = 0
        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Coaching_comment_add", id, emp.performanceid, emp.Kpiid, emp.obj, emp.radEnddate, emp.empid, emp.obj2)

    End Sub
    <WebMethod()>
    Public Sub getcomment1(ByVal pid As String, empid As String)
        Dim listRecipients As List(Of recipient) = New List(Of recipient)()
        Dim strTest As DataSet
        strTest = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Coaching_comment_get", pid, empid)
        Dim i As Integer = 0
        Dim count As Integer = 1

        If strTest.Tables(0).Rows.Count > 0 Then
            For Each dr As DataRow In strTest.Tables(0).Rows
                Try
                    Dim prog As recipient = New recipient()
                    prog.name = Convert.ToString(strTest.Tables(0).Rows(i)("Comment"))
                    prog.description = Convert.ToString(strTest.Tables(0).Rows(i)("Deadline_Date"))
                    prog.type = Convert.ToString(strTest.Tables(0).Rows(i)("Key_Takeaways"))

                    listRecipients.Add(prog)
                    i += 1
                    count += 1
                Catch Ex As Exception
                    Context.Response.Write(Ex.Message)
                End Try
            Next
            Dim js As JavaScriptSerializer = New JavaScriptSerializer()
            Context.Response.Write(js.Serialize(listRecipients))
        End If
    End Sub
    <WebMethod()>
    Public Sub getcommentmngr1(ByVal pid As String, empid As String)
        Dim listRecipients As List(Of recipient) = New List(Of recipient)()
        Dim strTest As DataSet
        strTest = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Coaching_comment_get", pid, empid)
        Dim i As Integer = 0
        Dim count As Integer = 1

        If strTest.Tables(0).Rows.Count > 0 Then
            For Each dr As DataRow In strTest.Tables(0).Rows
                Try
                    Dim prog As recipient = New recipient()
                    prog.name = Convert.ToString(strTest.Tables(0).Rows(i)("Comment"))
                    prog.description = Convert.ToString(strTest.Tables(0).Rows(i)("Deadline_Date"))
                    prog.type = Convert.ToString(strTest.Tables(0).Rows(i)("Key_Takeaways"))

                    listRecipients.Add(prog)
                    i += 1
                    count += 1
                Catch Ex As Exception
                    Context.Response.Write(Ex.Message)
                End Try
            Next
            Dim js As JavaScriptSerializer = New JavaScriptSerializer()
            Context.Response.Write(js.Serialize(listRecipients))
        End If
    End Sub
    <WebMethod()>
    Public Sub getAllComments(ByVal pid As String, empid As String, ByVal empid1 As String, ByVal empid2 As String)
        Dim listRecipients As List(Of recipient) = New List(Of recipient)()
        Dim strTest As DataSet
        strTest = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Comment_get_All", pid, empid, empid1, empid2)
        Dim i As Integer = 0
        Dim count As Integer = 1

        If strTest.Tables(0).Rows.Count > 0 Then
            For Each dr As DataRow In strTest.Tables(0).Rows
                Try
                    Dim prog As recipient = New recipient()
                    prog.name = Convert.ToString(strTest.Tables(0).Rows(i)("Objectives"))
                    prog.type = Convert.ToString(strTest.Tables(0).Rows(i)("CommentDate"))
                    prog.description = Convert.ToString(strTest.Tables(0).Rows(i)("successtarget"))
                    If prog.type <> "" Then
                        listRecipients.Add(prog)

                    End If
                    i += 1
                    count += 1
                Catch Ex As Exception
                    Context.Response.Write(Ex.Message)
                End Try
            Next
            Dim js As JavaScriptSerializer = New JavaScriptSerializer()
            Context.Response.Write(js.Serialize(listRecipients))
        End If
    End Sub
    <WebMethod(EnableSession:=True)>
    Public Sub Send_Return_Request(ByVal ID As String)
        Try
            Dim returnid = ID
            Dim lblstatus As String = ""
            Dim initiator As String = ""
            Dim initiatorname As String = ""
            Dim office As String = ""
            Dim strUser As New DataSet
            strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Asset_get", returnid)
            Dim AssetName = strUser.Tables(0).Rows(0).Item("AssetName").ToString
            Dim EmpID = strUser.Tables(0).Rows(0).Item("EmpID").ToString
            Dim AssetNumber = strUser.Tables(0).Rows(0).Item("AssetNumber").ToString
            Dim strEmp As New DataSet
            strEmp = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "select a.Name, a.Employee2 Employee,Email, Office     from dbo.Employees_All a where a.EmpID = '" & EmpID & "'")
            If strEmp.Tables(0).Rows.Count > 0 Then
                initiator = strEmp.Tables(0).Rows(0).Item("Email").ToString
                initiatorname = strEmp.Tables(0).Rows(0).Item("Name").ToString
                office = strEmp.Tables(0).Rows(0).Item("Office").ToString
            End If
            Dim url As String = ""
            url = Process.ApplicationURL & "/" & "Module/Employee/EmployeeData"
            If Process.Asset_Return_Request(AssetName, Process.GetMailList("hr"), initiatorname & " of " & office, AssetNumber, initiator, EmpID, "", url & "?id=" & EmpID) = True Then
                lblstatus = "Changes has been forwarded to HR for acceptance"

            Else
                lblstatus = Session("exception")
            End If


        Catch ex As Exception

        End Try
    End Sub
    <WebMethod()>
    Public Sub FinanceComponents(ByVal pid As String)
        Dim listRecipients As List(Of PerfomancePoints) = New List(Of PerfomancePoints)()

        Dim strTest As DataSet
        strTest = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Finance_Monthly_Earning_Items_Get_All")

        Dim i As Integer = 0
        Dim count As Integer = 1

        If strTest.Tables(0).Rows.Count > 0 Then
            For Each dr As DataRow In strTest.Tables(0).Rows
                Try
                    Dim prog As PerfomancePoints = New PerfomancePoints()
                    prog.points = Convert.ToString(strTest.Tables(0).Rows(i)("id"))

                    prog.desc = Convert.ToString(strTest.Tables(0).Rows(i)("Payslip Item"))


                    listRecipients.Add(prog)
                    i += 1
                    count += 1
                Catch Ex As Exception
                    Context.Response.Write(Ex.Message)
                End Try
            Next
            Dim js As JavaScriptSerializer = New JavaScriptSerializer()
            Context.Response.Write(js.Serialize(listRecipients))

        End If

    End Sub
    <WebMethod()>
    Public Sub DaysComponents(ByVal pid As String)
        Dim listRecipients1 As List(Of DayList) = New List(Of DayList)()
        Dim strTest2 As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Work_Week_get_all_Overtime", pid)
        Dim i As Integer = 0
        Dim count As Integer = 1

        If strTest2.Tables(0).Rows.Count > 0 Then
            For Each dr As DataRow In strTest2.Tables(0).Rows
                Try
                    Dim progs As DayList = New DayList()
                    progs.day = Convert.ToString(strTest2.Tables(0).Rows(i)("Day"))


                    listRecipients1.Add(progs)
                    i += 1
                    count += 1
                Catch Ex As Exception
                    Context.Response.Write(Ex.Message)
                End Try
            Next
            Dim js As JavaScriptSerializer = New JavaScriptSerializer()
            Context.Response.Write(js.Serialize(listRecipients1))

        End If

    End Sub
    <WebMethod()>
    Public Sub Employedata(ByVal pid As String, ByVal empid As String)
        Dim listRecipients1 As List(Of EmployeesData) = New List(Of EmployeesData)()

        Dim strPresentDays As DataTable = Process.SearchDataP3("Time_Employee_Attendance_Get_All_Working", pid, DateSerial(Now.Year, Now.Month, 1), Date.Today)
        'Dim strPresentDays As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Time_Employee_Attendance_Get_All", empid, DateSerial(Now.Year, Now.Month, 1), Date.Today)
        Dim foundRow As DataRow() = strPresentDays.[Select]("isworkday = 1")
        Dim foundRow1 As DataRow() = strPresentDays.[Select]("leaveid <> '0' and isworkday = 1")
        Dim foundRow2 As DataRow() = strPresentDays.[Select]("checkindate ='' and isworkday = 1 and leaveid = '0'")
        Dim strLeave As DataTable
        strLeave = Process.SearchData("Emp_Leave_Chart", pid)

        Dim sum = IIf(IsDBNull(strLeave.Compute("SUM(totalbalance)", "")), "0", strLeave.Compute("SUM(totalbalance)", ""))
        Dim sum2 As Integer = Convert.ToInt32(strLeave.Compute("SUM(ApprovedDays)", String.Empty))
        'Dim sum2 = IIf(IsDBNull(strLeave.Compute("SUM(ApprovedDays)", "")), "0", strLeave.Compute("SUM(ApprovedDays)", ""))
        Dim sums As String = sum.ToString()
        Dim sums2 As String = sum2.ToString()
        Dim strEmployee As DataSet = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_PersonalDetail_DirectReports_My_Team", empid)
        Dim newDataRow As DataRow() = strEmployee.Tables(0).[Select]("empid ='" & pid & "'")




        Dim progs As EmployeesData = New EmployeesData()
        progs.Presentday = (foundRow.Length - (foundRow1.Length + foundRow2.Length)).ToString()
        progs.AbsentDay = foundRow2.Length.ToString()
        progs.Leaveday = foundRow1.Length.ToString
        If foundRow.Length > 0 Then
            progs.AttendanceRate = ((Integer.Parse(progs.Presentday) / foundRow.Length) * (100)).ToString()
        Else
            progs.AttendanceRate = "0"
        End If
        progs.LeaveTaken = sums2.Split(".")(0)
        progs.Performance = newDataRow(0).Item("Score")
        listRecipients1.Add(progs)



        Dim js As JavaScriptSerializer = New JavaScriptSerializer()
        Context.Response.Write(js.Serialize(listRecipients1))


    End Sub


    <WebMethod()>
    Public Sub HRDashboardData(ByVal companyName As String)
        Dim listRecipients As List(Of companyObj) = New List(Of companyObj)()

        Dim prog As companyObj = New companyObj()
        Dim strTest As DataSet
        strTest = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_WorkForce_Budget_get_all", companyName, "Pending", "Plan", Date.Now.Year)
        prog.workforceplan = FormatNumber(strTest.Tables(0).Rows.Count.ToString(), 0)
        strTest.Clear()

        strTest = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Job_Post_get_all", "Opened", companyName)
        prog.jobPortal = FormatNumber(strTest.Tables(0).Rows.Count.ToString(), 0)
        strTest.Clear()

        strTest = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Job_Test_Get_All", companyName)
        prog.recruitmentTest = FormatNumber(strTest.Tables(0).Rows.Count.ToString(), 0)
        strTest.Clear()

        strTest = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Job_Interview_Get_All", companyName)
        prog.interview = FormatNumber(strTest.Tables(0).Rows.Count.ToString(), 0)
        strTest.Clear()

        strTest = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Job_Requisition_Get_All_HR", companyName, "Pending")
        prog.staffRequest = FormatNumber(strTest.Tables(0).Rows.Count.ToString(), 0)
        strTest.Clear()

        strTest = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_PersonalDetail_get_all_Specific", "", companyName, 1, 1000000)
        prog.employeeDataset = FormatNumber(strTest.Tables(0).Rows.Count.ToString(), 0)
        strTest.Clear()

        strTest = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Confirmation_Get_HR", companyName, "Pending")
        prog.employeeConfirmation = FormatNumber(strTest.Tables(0).Rows.Count.ToString(), 0)
        strTest.Clear()

        strTest = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruitment_Succession_Get_All_HR", companyName)
        prog.successionPlan = FormatNumber(strTest.Tables(0).Rows.Count.ToString(), 0)
        strTest.Clear()

        strTest = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruitment_Promotion_Get_All", companyName)
        prog.promotion = FormatNumber(strTest.Tables(0).Rows.Count.ToString(), 0)
        strTest.Clear()

        strTest = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Termination_Get_All", companyName, "Pending", DateSerial(Date.Now.Year, 1, 1), Date.Now)
        prog.employeeExit = FormatNumber(strTest.Tables(0).Rows.Count.ToString(), 0)
        strTest.Clear()

        strTest = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_HMO_Get_All")
        prog.hmo = FormatNumber(strTest.Tables(0).Rows.Count.ToString(), 0)
        strTest.Clear()

        strTest = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "sp_Get_WorkForceGrowth_Hr_dashboard", companyName)
        Dim k As Integer = strTest.Tables(0).Rows.Count
        If k > 0 Then
            For i As Integer = 0 To k - 1
                Dim name As Integer = Convert.ToInt32(strTest.Tables(0).Rows(i)("name"))
                If name = Date.Now.Year Then
                    prog.workforceCount = Convert.ToInt32(strTest.Tables(0).Rows(i)("count"))
                Else
                    prog.workforceCount = 0
                End If
            Next
        End If
        strTest.Clear()

        strTest = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "sp_Get_TurnoverRate_Hr_dashboard", companyName)
        Dim n As Integer = strTest.Tables(0).Rows.Count
        If n > 0 Then
            For o As Integer = 0 To n - 1
                Dim name As Integer = Convert.ToInt32(strTest.Tables(0).Rows(o)("name"))
                If name = Date.Now.Year Then
                    prog.turnoverCount = Convert.ToDecimal(strTest.Tables(0).Rows(o)("count"))
                Else
                    prog.turnoverCount = 0
                End If
            Next
        End If
        strTest.Clear()

        strTest = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "sp_Get_Performance_Hr_dashboard", companyName)
        prog.performanceRating = FormatNumber(strTest.Tables(0).Rows.Count.ToString(), 0)
        Dim y As Integer = strTest.Tables(0).Rows.Count
        If y > 0 Then
            For a As Integer = 0 To y - 1
                prog.performanceRating = Convert.ToDecimal(strTest.Tables(0).Rows(a)("score"))
            Next
        End If
        strTest.Clear()

        strTest = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "sp_Get_WorkForceGrowth_Hr_dashboard", companyName)
        prog.compentenceRating = FormatNumber(strTest.Tables(0).Rows.Count.ToString(), 0)
        strTest.Clear()

        strTest = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Employee_Query_Get_All", companyName)
        prog.queries = FormatNumber(strTest.Tables(0).Rows.Count.ToString(), 0)
        strTest.Clear()

        strTest = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Finance_Payslip_Primary_Get_All", Date.Now.Year, companyName)
        prog.payroll = FormatNumber(strTest.Tables(0).Rows.Count.ToString(), 0)
        strTest.Clear()

        strTest = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Finance_Payslip_Terminal_Get_All", companyName)
        prog.terminalBenefit = FormatNumber(strTest.Tables(0).Rows.Count.ToString(), 0)
        strTest.Clear()

        strTest = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Finance_Emp_Loan_Get_all", DateSerial(Date.Now.Year, 1, 1), Date.Now, "Pending", companyName)
        prog.staffLoan = FormatNumber(strTest.Tables(0).Rows.Count.ToString(), 0)
        strTest.Clear()

        strTest = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Employee_Leavelist_get_all", "", "Pending", DateSerial(Date.Now.Year, 1, 1), Date.Now, companyName, "")
        prog.leaveAllowance = FormatNumber(strTest.Tables(0).Rows.Count.ToString(), 0)
        strTest.Clear()

        strTest = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Employee_Query_Get_All", companyName)
        prog.overTimeRequest = FormatNumber(strTest.Tables(0).Rows.Count.ToString(), 0)
        strTest.Clear()

        listRecipients.Add(prog)
        Dim js As JavaScriptSerializer = New JavaScriptSerializer()
        Context.Response.Write(js.Serialize(listRecipients))

    End Sub

End Class