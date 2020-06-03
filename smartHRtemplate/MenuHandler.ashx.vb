Imports System.Web
Imports System.Web.Services
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.Web.Script.Serialization

Public Class MenuHandler
    Implements System.Web.IHttpHandler
    Implements IReadOnlySessionState
    Implements IRequiresSessionState
    Dim cs As String = ConfigurationManager.ConnectionStrings("GOSHRMConnectionString").ConnectionString
    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Try
            Dim sess_role As String = HttpContext.Current.Session.Item("role").ToString()
            Dim loginid As String = HttpContext.Current.Session.Item("loginid").ToString()
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
                        Dim submenus As DataSet = SqlHelper.ExecuteDataset(cs, "Menu_Get_Sub_Module", loginid, NodeLevel1.value, NodeLevel2.MenuText, sess_role)
                        For x As Integer = 0 To submenus.Tables(0).Rows.Count - 1
                            Dim NodeLevel3 As New Menu
                            NodeLevel3.MenuText = submenus.Tables(0).Rows(x).Item("SubModule1").ToString()
                            If submenus.Tables(0).Rows(x).Item("ViewPath").ToString().ToLower = "n/a" Then
                                Dim listsubsubMenu As List(Of Menu) = New List(Of Menu)()
                                Dim subsubmenus As DataSet = SqlHelper.ExecuteDataset(cs, "Menu_Get_Sub_Sub_Module", loginid, NodeLevel1.value, NodeLevel2.MenuText, NodeLevel3.MenuText, sess_role)
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
                LMenu.Add(NodeLevel1)
            Next
            Dim jsonSerializer = New JavaScriptSerializer()
            Dim data As String = jsonSerializer.Serialize(LMenu)
            context.Response.Write(data)
        Catch ex As Exception
        End Try
    End Sub

    'Public Function GetMenuTree(ByVal list As List(Of Menu), ByVal parent As Integer?) As List(Of Menu)
    '    Return list.Where(Function(x) x.ParentId = parent).[Select](Function(x) New Menu With {
    '        .Id = x.Id,
    '        .MenuText = x.MenuText,
    '        .ParentId = x.ParentId,
    '        .Active = x.Active,
    '        .List = GetMenuTree(list, x.Id)
    '    }).ToList()
    'End Function

    Public Class Menu
        'Public Property Id As Integer
        Public Property MenuText As String
        Public Property NavURL As String
        Public Property value As String
        'Public Property ParentId As Integer?
        'Public Property Active As Boolean
        Public Property List As List(Of Menu)
    End Class

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class