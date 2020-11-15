Imports System.Web.SessionState
Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Routing

Public Class Global_asax
    Inherits System.Web.HttpApplication

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the application is started
        RegisterRoutes(RouteTable.Routes)
    End Sub

    Private Shared Sub RegisterRoutes(ByVal routes As RouteCollection)
        routes.MapPageRoute("getcorevalues", "getcorevalues", "~/res_new/gos.asmx/getcorevalues")
        routes.MapPageRoute("empdashboard", "empdashboard", "~/empdashboard.aspx")
        routes.MapPageRoute("hrdashboard2", "hrdashboard2", "~/hrdashboard2.aspx")
        routes.MapPageRoute("default", "default", "~/default.aspx")
        routes.MapPageRoute("changepassword", "changepassword", "~/changepassword.aspx")
        routes.MapPageRoute("Module/Employee/Performance/appObjectiveUpdate", "Module/Employee/Performance/appObjectiveUpdate", "~/Module/Employee/Performance/appObjectiveUpdate.aspx")
        Dim url As DataSet
        url = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "SELECT distinct Viewpath, REPLACE(ViewPath, '.aspx', '') as Viewpaths from Menu where viewpath not like '%' + '?id' + '%'")
        If url.Tables(0).Rows.Count > 0 Then
            Dim c As Integer = url.Tables(0).Rows.Count
            If c > 0 Then
                For i As Integer = 0 To c - 1
                    Dim oldurl As String = Convert.ToString(url.Tables(0).Rows(i)("Viewpath"))
                    Dim newurl As String = Convert.ToString(url.Tables(0).Rows(i)("Viewpaths"))
                    Dim oldurls As String = "~/" + oldurl
                    routes.MapPageRoute(newurl, newurl, oldurls)
                Next
            End If

        End If

    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session is started
    End Sub

    Private Shared Sub HandleAjax(ByVal context As HttpContext)
        Dim dotasmx As Integer = context.Request.Path.IndexOf(".asmx")
        Dim path As String = context.Request.Path.Substring(0, dotasmx + 5)
        Dim pathInfo As String = context.Request.Path.Substring(dotasmx + 5)
        context.RewritePath(path, pathInfo, context.Request.Url.Query)
    End Sub

    Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)

        'If Request.Path.IndexOf(".asmx") <> -1 Then
        '    HandleAjax(Context)
        '    Exit Sub
        'End If


        Dim aaa As String = ""

        ' Fires at the beginning of each request
        Dim WebsiteURL As String = Request.Url.ToString()
        Dim SplitedURL As [String]() = WebsiteURL.Split("/"c)
        Dim Temp As [String]() = SplitedURL(SplitedURL.Length - 1).Split("."c)
        Dim Temp2 As [String]() = SplitedURL(SplitedURL.Length - 2).Split("."c)

        ' This is for aspx page
        'If Not WebsiteURL.ToLower.Trim.Contains(".png") Then
        If Not WebsiteURL.Contains(".aspx") AndAlso Temp.Length = 1 Then
            If Not String.IsNullOrEmpty(Temp(0).Trim()) Then
                If Not Temp(0).Contains("?") Then
                    If Temp2(0).Contains("gos") Then
                        'Context.RewritePath(Temp(0) + "")
                        'Context.RewritePath("/res_new/gos.asmx/", Temp(0), Context.Request.Url.Query)
                        Exit Sub
                    End If
                    Context.RewritePath(Temp(0) + ".aspx")
                Else
                    Dim SplitedPage As [String]() = Temp(0).Split("?"c)
                    Context.RewritePath(SplitedPage(0) + ".aspx?" + SplitedPage(1))
                End If

            End If
        End If
        'End If
    End Sub

    Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires upon attempting to authenticate the use
    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when an error occurs
        Dim lastErrorWrapper As HttpException = TryCast(Server.GetLastError(), HttpException)
        Dim aaa As String = ""
        Dim lastError As Exception = lastErrorWrapper
        If lastErrorWrapper.InnerException IsNot Nothing Then
            lastError = lastErrorWrapper.InnerException
        End If
        Dim lastErrorTypeName As String = lastError.[GetType]().ToString()
        Dim lastErrorMessage As String = lastError.Message
        Dim lastErrorStackTrace As String = lastError.StackTrace

        aaa = HttpContext.Current.Request.FilePath
        Process.strExp = lastErrorMessage

        If aaa.Contains("asp") = True Then
            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Error_Log_Update", Process.strExp, "", "", Request.ServerVariables("HTTP_X_FORWARDED_FOR"), aaa)
        End If

        Response.Redirect("~/ErrorPage.aspx", True)
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session ends
    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the application ends
    End Sub

End Class