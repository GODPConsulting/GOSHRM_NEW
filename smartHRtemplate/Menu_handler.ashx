<%@ WebHandler Language="VB" Class="Menu_handler" %>

Imports System.Web
Imports System.Web.Services
Imports System.Web.UI
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.Web.Script.Serialization
Imports System.Data

Public Class Menu_handler
    Implements IHttpHandler
    Implements IReadOnlySessionState
    Implements IRequiresSessionState
    
    Private cs As String = System.Configuration.ConfigurationManager.ConnectionStrings("GOSHRMConnectionString").ConnectionString

    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Try

            Dim sess_role As String = HttpContext.Current.Session.Item("role").ToString()
            Dim sess_user As String = HttpContext.Current.Session.Item("loginid").ToString()
             
            'Dim sess_user As String = HttpContext.Current.Session.Item("LoginID")
            Dim Modules As DataSet = SqlHelper.ExecuteDataset(cs, "Modules_Get_All")
            Dim LMenu As List(Of Menu) = New List(Of Menu)()
            For i As Integer = 0 To Modules.Tables(0).Rows.Count - 1
                Dim NodeLevel1 As New Menu
                NodeLevel1.MenuText = Modules.Tables(0).Rows(i).Item("Module").ToString()
                NodeLevel1.value = Modules.Tables(0).Rows(i).Item("ModuleCode").ToString()
                Dim listMenu As List(Of Menu) = New List(Of Menu)()
                Dim menus As DataSet = SqlHelper.ExecuteDataset(cs, "Menu_Get_Module", NodeLevel1.value, sess_role)
                For k As Integer = 0 To menus.Tables(0).Rows.Count - 1
                    Dim NodeLevel2 As New Menu
                    NodeLevel2.MenuText = menus.Tables(0).Rows(k).Item("SubModule").ToString()
                    If menus.Tables(0).Rows(k).Item("ViewPath").ToString().ToLower = "n/a" Then
                        Dim listsubMenu As List(Of Menu) = New List(Of Menu)()
                        Dim submenus As DataSet = SqlHelper.ExecuteDataset(cs, "Menu_Get_Sub_Module", sess_user, NodeLevel1.value, NodeLevel2.MenuText, sess_role)
                        For x As Integer = 0 To submenus.Tables(0).Rows.Count - 1
                            Dim NodeLevel3 As New Menu
                            NodeLevel3.MenuText = submenus.Tables(0).Rows(x).Item("SubModule1").ToString()
                            If submenus.Tables(0).Rows(x).Item("ViewPath").ToString().ToLower = "n/a" Then
                                Dim listsubsubMenu As List(Of Menu) = New List(Of Menu)()
                                Dim subsubmenus As DataSet = SqlHelper.ExecuteDataset(cs, "Menu_Get_Sub_Sub_Module", sess_user, NodeLevel1.value, NodeLevel2.MenuText, NodeLevel3.MenuText, sess_role)
                                For z As Integer = 0 To subsubmenus.Tables(0).Rows.Count - 1
                                    Dim NodeLevel4 As New Menu
                                    NodeLevel4.MenuText = subsubmenus.Tables(0).Rows(z).Item("SubModule2").ToString()
                                    NodeLevel4.NavURL = subsubmenus.Tables(0).Rows(z).Item("ViewPath").ToString()
                                    NodeLevel4.value = subsubmenus.Tables(0).Rows(z).Item("SubModuleCode").ToString()
                                    listsubsubMenu.Add(NodeLevel4)
                                Next
                                NodeLevel3.List = listsubsubMenu
                            Else
                                NodeLevel3.value = submenus.Tables(0).Rows(x).Item("SubModuleCode").ToString()
                                NodeLevel3.NavURL = submenus.Tables(0).Rows(x).Item("ViewPath").ToString()
                            End If
                            listsubMenu.Add(NodeLevel3)
                        Next
                        NodeLevel2.List = listsubMenu
                    Else
                        NodeLevel2.value = menus.Tables(0).Rows(k).Item("SubModuleCode").ToString()
                        NodeLevel2.NavURL = menus.Tables(0).Rows(k).Item("ViewPath").ToString()
                    End If
                    listMenu.Add(NodeLevel2)
                Next
                NodeLevel1.List = listMenu
                If listMenu.Count > 0 Then
                    LMenu.Add(NodeLevel1)
                End If
               
            Next
            Dim jsonSerializer = New JavaScriptSerializer()
            Dim data As String = jsonSerializer.Serialize(LMenu)
            context.Response.Write(data)
        Catch Ex As System.Exception
            context.Response.Write(Ex.Message)
        End Try
    End Sub

    Public Class Menu
        Public Property MenuText As String
        Public Property NavURL As String
        Public Property value As String
        Public Property List As List(Of Menu)
    End Class

    Public ReadOnly Property IsReusable() As Boolean _
        Implements IHttpHandler.IsReusable
        Get
            Return True
        End Get
    End Property
    
    Public Shared Function ResolveUrl(ByVal originalUrl As String) As String
        If originalUrl Is Nothing Then Return Nothing
        If originalUrl.IndexOf("://") <> -1 Then Return originalUrl
        Dim newUrl As String = ""
        If originalUrl.StartsWith("~") Then
            

            If HttpContext.Current IsNot Nothing Then
                newUrl = HttpContext.Current.Request.ApplicationPath & originalUrl.Substring(1).Replace("//", "/")
            Else
            End If

            
        End If
        Return newUrl
    End Function
    
    Public Function ResolveServerUrl(ByVal serverUrl As String, ByVal forceHttps As Boolean) As String
        If serverUrl.IndexOf("://") > -1 Then Return serverUrl
        Dim newUrl As String = ResolveUrl(serverUrl)
        Dim originalUri As Uri = HttpContext.Current.Request.Url
        newUrl = (If(forceHttps, "https", originalUri.Scheme)) & "://" + originalUri.Authority & newUrl.Replace("//", "/")
        Return newUrl
    End Function
End Class