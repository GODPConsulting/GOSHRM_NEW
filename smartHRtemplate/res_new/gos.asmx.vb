﻿Imports System.Web.Services
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

    Public Class recipient2
        Public Property currency As String
        Public Property source As String
        Public transfers As List(Of transfers)
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
            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_Update", emp.ID, emp.AppID, emp.cat, emp.kpi, emp.key, emp.tdate, emp.aweight, emp.suc, emp.obj, emp.w_model)
        Else
            If complete = "Yes" Then
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_Update", emp.ID, emp.AppID, emp.cat, emp.kpi, emp.key, emp.tdate, emp.aweight, emp.suc, emp.obj, emp.w_model)
                Process.Appraisal_Obj_UpdateKPI(manager, empname, reviewyear, emp.ID, managerID, Process.ApplicationURL + "/" + "Module/Employee/Performance/CoacheeAppraisalObjectives")
            Else
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_Update", emp.ID, emp.AppID, emp.cat, emp.kpi, emp.key, emp.tdate, emp.aweight, emp.suc, emp.obj, emp.w_model)
            End If
        End If
    End Sub

End Class