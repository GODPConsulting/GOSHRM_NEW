Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports System.IO
Imports Telerik.Web.UI


Public Class CoachObjectives
    Inherits System.Web.UI.Page
    Dim education As New clsEducation
    Dim Separators() As Char = {","c}
    Dim AuthenCode As String = "EMPMYGOAL"
    Dim rowCounts As Integer = 0
    Protected Sub ToggleRowSelection(ByVal sender As Object, ByVal e As EventArgs)

        TryCast(TryCast(sender, CheckBox).NamingContainer, GridItem).Selected = TryCast(sender, CheckBox).Checked
        Dim checkHeader As Boolean = True
        For Each dataItem As GridDataItem In gridCompetency.MasterTableView.Items
            If Not TryCast(dataItem.FindControl("CheckBox1"), CheckBox).Checked Then
                checkHeader = False
                Exit For
            End If
        Next
        Dim headerItem As GridHeaderItem = TryCast(gridCompetency.MasterTableView.GetItems(GridItemType.Header)(0), GridHeaderItem)
        TryCast(headerItem.FindControl("headerChkbox"), CheckBox).Checked = checkHeader
    End Sub
    Protected Sub ToggleSelectedState(ByVal sender As Object, ByVal e As EventArgs)
        Dim headerCheckBox As CheckBox = TryCast(sender, CheckBox)
        For Each dataItem As GridDataItem In gridCompetency.MasterTableView.Items
            TryCast(dataItem.FindControl("CheckBox1"), CheckBox).Checked = headerCheckBox.Checked
            dataItem.Selected = headerCheckBox.Checked
        Next
    End Sub


    Private Sub LoadData(dataid As Integer)
        Try
            gridCompetency.DataSource = Process.SearchData("Performance_Appraisal_Get_All", dataid)
            gridCompetency.DataBind()

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not Me.IsPostBack Then
                Dim endDaTE As Date = Date.Now
                Session("coachPreviousPage") = Request.UrlReferrer.ToString
                If Request.QueryString("id") IsNot Nothing Then
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Appraisal_Summary_Get", Request.QueryString("id").ToString)
                    lblid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    areviewperiod.Value = strUser.Tables(0).Rows(0).Item("period").ToString
                    areviewyear.Value = strUser.Tables(0).Rows(0).Item("reviewyear").ToString
                    aname.Value = strUser.Tables(0).Rows(0).Item("empname").ToString
                    ajobtitle.Value = strUser.Tables(0).Rows(0).Item("jobtitle").ToString                    
                    LoadData(lblid.Text)
                End If
            End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Protected Sub btnClose_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect(Session("coachPreviousPage"), True)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub gridCompetency_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles gridCompetency.ItemDataBound
        Try
            If TypeOf e.Item Is GridDataItem Then
                Dim objectives As TableCell = TryCast(e.Item, GridDataItem)("objectives")
                objectives.Text = objectives.Text.Replace(vbCr & vbLf, "<br/>")

                Dim kpiobjectives As TableCell = TryCast(e.Item, GridDataItem)("kpiobjectives")
                kpiobjectives.Text = kpiobjectives.Text.Replace(vbCr & vbLf, "<br/>")

                Dim keyactions As TableCell = TryCast(e.Item, GridDataItem)("keyactions")
                keyactions.Text = keyactions.Text.Replace(vbCr & vbLf, "<br/>")

                Dim successtarget As TableCell = TryCast(e.Item, GridDataItem)("successtarget")
                successtarget.Text = successtarget.Text.Replace(vbCr & vbLf, "<br/>")
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class