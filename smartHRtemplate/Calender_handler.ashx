<%@ WebHandler Language="VB" Class="Calender_handler" %>

Imports System.Web
Imports System.Web.Services
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.Web.Script.Serialization
Imports System.Data

Public Class Calender_handler
    Implements IHttpHandler

    Private cs As String = System.Configuration.ConfigurationManager.ConnectionStrings("GOSHRMConnectionString").ConnectionString

    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Try
             Dim listevents As List(Of cal_events) = New List(Of cal_events)()
            Dim strtest As DataSet = SqlHelper.ExecuteDataset(cs, "Calendar_Event_Get_All")
            If strtest.Tables(0).Rows.Count > 0 Then
                Dim c As Integer = strtest.Tables(0).Rows.Count
                If c > 0 Then
                    For i As Integer = 0 To c - 1
                        Dim ce As cal_events = New cal_events()
                        ce.eventdate = Convert.ToString(strtest.Tables(0).Rows(i)("EventDate"))
                        ce.eventdes = Convert.ToString(strtest.Tables(0).Rows(i)("EventDescription"))
                        ce.eventenddate = Convert.ToString(strtest.Tables(0).Rows(i)("EventEndDate"))
                        ce.eventstat = Convert.ToString(strtest.Tables(0).Rows(i)("EventStat"))
                        ce.eventtime = Convert.ToString(strtest.Tables(0).Rows(i)("EventTime"))
                        ce.eventtitle = Convert.ToString(strtest.Tables(0).Rows(i)("EventTitle"))
                        ce.eventcolor = "#378006"
                        ce.isfullday = True
                        listevents.Add(ce)
                    Next
                End If
            End If
            
            Dim jsonSerializer = New JavaScriptSerializer()
            Dim data As String = jsonSerializer.Serialize(listevents)
            context.Response.Write(data)
        Catch Ex As System.Exception
            context.Response.Write(Ex.Message)
        End Try
    End Sub
    Public Class cal_events
        Public Property eventdate As String
        Public Property eventtime As String
        Public Property eventdes As String
        Public Property eventenddate As String
        Public Property eventstat As String
        Public Property eventtitle As String
        Public Property eventcolor As String
        Public Property isfullday As Boolean
    End Class

    Public ReadOnly Property IsReusable() As Boolean _
        Implements IHttpHandler.IsReusable
        Get
            Return True
        End Get
    End Property
End Class