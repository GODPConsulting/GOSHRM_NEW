Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Telerik.Web.UI
Public Class AppraisalsFeedBack
    Inherits System.Web.UI.Page
    ' Dim gridroles As Grid = New Grid()
    Dim _sortDirection As String
    Dim dtTable As DataTable
    Dim AuthenCode As String = "JOBTEST"
    Dim olddata(5) As String
    
    Private Function LodaDataTable(id As String) As DataTable
        Dim Datas As New DataTable
        Dim serach As String = ""

        Datas = Process.SearchData("Performance_Appraisal_Get_All", id)

        Return Datas
    End Function

    Private Sub LoadData(id As String)
        Try
            gridFeedback.DataSource = LodaDataTable(id)
            gridFeedback.DataBind()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                Session("PreviousPage") = Request.UrlReferrer.ToString
                Dim strUser As New DataSet
                strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Appraisal_Summary_Get", Request.QueryString("id").ToString)
                If strUser.Tables(0).Rows.Count > 0 Then

                    lblid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    txtYear.Value = strUser.Tables(0).Rows(0).Item("reviewyear").ToString
                    txtEmpID.Value = strUser.Tables(0).Rows(0).Item("empid").ToString
                    txtName.Value = strUser.Tables(0).Rows(0).Item("empname").ToString
                    txtJobTitle.Value = strUser.Tables(0).Rows(0).Item("jobtitle").ToString
                    txtJobGrade.Value = strUser.Tables(0).Rows(0).Item("jobgrade").ToString
                    txtDept.Value = strUser.Tables(0).Rows(0).Item("dept").ToString
                    datStartPeriod.SelectedDate = strUser.Tables(0).Rows(0).Item("StartPeriod")
                    datEndReview.SelectedDate = strUser.Tables(0).Rows(0).Item("EndPeriod")
                    txtPresentPos.Value = strUser.Tables(0).Rows(0).Item("MthInPosition").ToString
                    txtLengthOfService.Value = strUser.Tables(0).Rows(0).Item("MthInService")
                    txtLocation.Value = strUser.Tables(0).Rows(0).Item("location").ToString
                    lblreviewer.Text = strUser.Tables(0).Rows(0).Item("supervisorid").ToString
                    txtreviewer.Value = strUser.Tables(0).Rows(0).Item("supervisorname2").ToString
                    lblReviewerPoint.Value = strUser.Tables(0).Rows(0).Item("mgrgrade").ToString
                    lbloverallpoint.Value = strUser.Tables(0).Rows(0).Item("grade").ToString
                    lblRevieweePoint.Value = strUser.Tables(0).Rows(0).Item("empgrade").ToString
                    lbloverdesc.Value = strUser.Tables(0).Rows(0).Item("gradename").ToString
                    lblscore.Value = strUser.Tables(0).Rows(0).Item("score").ToString

                    txtHRRecommendation.Value = strUser.Tables(0).Rows(0).Item("recommendation").ToString
                    lblrecommendationI.Value = strUser.Tables(0).Rows(0).Item("MgrRecommendation").ToString
                    lblrecommendationII.Value = strUser.Tables(0).Rows(0).Item("MgrRecommendation2").ToString

                    lblreviewerII.Text = strUser.Tables(0).Rows(0).Item("SupervisorID2").ToString
                    txtreviewerII.Value = strUser.Tables(0).Rows(0).Item("Supervisor2Name2").ToString
                    lblReviewerIIPoint.Value = strUser.Tables(0).Rows(0).Item("mgrgrade2").ToString

                    decision.Value = strUser.Tables(0).Rows(0).Item("decision").ToString
                    comment.Value = strUser.Tables(0).Rows(0).Item("finalcomment").ToString

                    If CDbl(strUser.Tables(0).Rows(0).Item("AdjustedScore")) < 0 Then
                        txtadjustedscore.Value = ""
                    Else
                        txtadjustedscore.Value = strUser.Tables(0).Rows(0).Item("AdjustedScore")
                    End If

                    LoadData(lblid.Text)
                End If

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub



    Public Property SortDirection() As SortDirection
        Get
            If ViewState("SortDirection") Is Nothing Then
                ViewState("SortDirection") = SortDirection.Ascending
            End If
            Return DirectCast(ViewState("SortDirection"), SortDirection)
        End Get
        Set(ByVal value As SortDirection)
            ViewState("SortDirection") = value
        End Set
    End Property


    Protected Sub btnClose_Click(sender As Object, e As EventArgs)
        Try
            Dim adjustment As Double = -1
            If txtadjustedscore.Value.Trim <> "" Then
                adjustment = txtadjustedscore.Value
            End If
            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_Summary_Recommendation_Update", lblid.Text, txtHRRecommendation.Value, adjustment, Session("LoginID"))
            Process.loadalert(divalert, msgalert, "Recommendation updated", "success")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnPromote_Click(sender As Object, e As EventArgs)
        Try
            'Emp_Work_History_Get_Actual
            Dim workid As Integer = 0
            Dim strUser As New DataSet
            strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Work_History_Get_Actual", txtEmpID.Value.Trim, Now.Date)
            If strUser.Tables(0).Rows.Count > 0 Then
                workid = strUser.Tables(0).Rows(0).Item("id").ToString
            End If
            Session("EmpID") = txtEmpID.Value.Trim
            Response.Redirect("~/Module/Employee/EmployeeWorkHistory.aspx?Id1=" & workid & "&recommendation=Promote", True)

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btn360degree_Click(sender As Object, e As EventArgs)
        Try
            Dim url As String = "feedback360selection.aspx?id=" & lblid.Text & "&empid=" & txtEmpID.Value
            Response.Redirect(url)
            'Dim s As String = "window.open('" & url + "', 'popup_window', 'width=1100,height=600,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no');"
            ClientScript.RegisterStartupScript(Me.GetType(), "script", url, True)

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            Dim url As String = Session("PreviousPage")
            Response.Redirect(url)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Private Sub gridFeedback_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles gridFeedback.ItemDataBound
        Try
            If TypeOf e.Item Is GridDataItem Then
                Dim empidcomment As TableCell = TryCast(e.Item, GridDataItem)("empidcomment")
                empidcomment.Text = empidcomment.Text.Replace(vbCr & vbLf, "<br/>")

                Dim supervisorcomment As TableCell = TryCast(e.Item, GridDataItem)("supervisorcomment")
                supervisorcomment.Text = supervisorcomment.Text.Replace(vbCr & vbLf, "<br/>")

                Dim supervisorcomment2 As TableCell = TryCast(e.Item, GridDataItem)("supervisorcomment2")
                supervisorcomment2.Text = supervisorcomment2.Text.Replace(vbCr & vbLf, "<br/>")
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
End Class