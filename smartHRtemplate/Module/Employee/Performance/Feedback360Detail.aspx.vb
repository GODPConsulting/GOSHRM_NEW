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


Public Class Feedback360Detail
    Inherits System.Web.UI.Page
    Dim education As New clsEducation
    Dim Separators() As Char = {","c}
    Dim AuthenCode As String = "APP360FEEDBACK"
    Dim rowCounts As Integer = 0

    Private Sub LoadReviewee(dataid As Integer)
        Try

            GridVwHeaderChckbox.DataSource = Process.SearchData("Performance_Appraisal_360_Detail_Get_All", dataid)
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


    Private Sub LoadQuestionaire(ByVal question As Integer, ByVal summaryID As Integer)
        Try

            Dim strUser As New DataSet
            strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Appraisal_360_Questionaire_Get", question, summaryID)
            If strUser.Tables(0).Rows.Count > 0 Then
                Process.LoadRadioButtonsDb(rdoMyRatings, "Performance_Points_Get_All", "pointname", "point", "pointdescription")

                lbkpitype.InnerText = "."
                lbquestno.InnerText = strUser.Tables(0).Rows(0).Item("rows").ToString
                lblQuestID.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                acomment.Value = strUser.Tables(0).Rows(0).Item("comments").ToString
                lbobjective.Value = strUser.Tables(0).Rows(0).Item("appraisalitem").ToString
                Process.RadioListCheck(rdoMyRatings, strUser.Tables(0).Rows(0).Item("rating").ToString)
                lblMyRating.Text = strUser.Tables(0).Rows(0).Item("rating").ToString
                lbobjectivedesc.InnerText = strUser.Tables(0).Rows(0).Item("description").ToString
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim empSubmit As String = "", MgrSubmit As String = "", MgrSubmit2 As String = "", cyclestat As String = ""
            Session("PreviousPage") = ""
            If Not Me.IsPostBack Then
                Dim strRates As New DataSet

                lbrates.InnerText = ""
                strRates = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Points_Get_All")
                For h As Integer = 0 To strRates.Tables(0).Rows.Count - 1
                    lbrates.InnerText = lbrates.InnerText & " " & strRates.Tables(0).Rows(h).Item("pointdesc").ToString
                Next


                'Process.DisableButton(btnSubmit)
                lblend.Text = "False"
                Dim summaryid As Integer = 0
                If Request.QueryString("id") IsNot Nothing Then
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_360_Detail_Create", Request.QueryString("id").ToString)
                    Dim strSummary As New DataSet
                    strSummary = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Appraisal_360_Get", Request.QueryString("id").ToString)
                    If strSummary.Tables(0).Rows.Count > 0 Then
                        summaryid = strSummary.Tables(0).Rows(0).Item("appraisalsummaryid").ToString
                        areviewer.Value = strSummary.Tables(0).Rows(0).Item("Name").ToString
                        apointmax.Value = strSummary.Tables(0).Rows(0).Item("maxgrade").ToString
                        apointawarded.Value = strSummary.Tables(0).Rows(0).Item("grade").ToString
                        ascore.Value = strSummary.Tables(0).Rows(0).Item("score").ToString
                        astat.Value = strSummary.Tables(0).Rows(0).Item("stat").ToString
                        lblreviewerid.Text = strSummary.Tables(0).Rows(0).Item("reviewer").ToString
                    End If
                    'do actaul count

                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Appraisal_Summary_Get", summaryid)
                    lblid.Text = Request.QueryString("id").ToString
                    areviewcycle.Value = strUser.Tables(0).Rows(0).Item("period").ToString
                    txtEmpID.Text = strUser.Tables(0).Rows(0).Item("empid").ToString
                    aemployee.Value = strUser.Tables(0).Rows(0).Item("empname").ToString
                    ajobtitle.Value = strUser.Tables(0).Rows(0).Item("jobtitle").ToString
                    aoofice.Value = strUser.Tables(0).Rows(0).Item("dept").ToString

                    Dim sscount As New DataSet
                    sscount = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Performance_Appraisal_360_Detail_Create", lblid.Text)

                    lblQuestCount.Text = sscount.Tables(0).Rows.Count

                    If astat.Value.ToLower.Contains("complete") = True Then
                        Process.DisableButton(btnNext)
                        Process.DisableButton(btnPrevious)
                        Process.DisableButton(btnSubmit)
                        MultiView1.ActiveViewIndex = 1
                        LoadReviewee(lblid.Text)
                        btnback.Visible = False
                        btnSend2.Visible = False
                    End If

                    MultiView1.ActiveViewIndex = 0
                    Session("QuestionNo") = 1

                    If CInt(lblQuestCount.Text) > 0 Then
                        LoadQuestionaire(Session("QuestionNo"), lblid.Text)
                        apageno.InnerText = "1 of " & lblQuestCount.Text
                    Else
                        lbquestno.InnerText = "0."
                        Process.loadalert(divalert, msgalert, "No 360 Degree Feedback Questions", "warning")

                    End If

                    If lblreviewerid.Text <> Session("UserEmpID") Then
                        Process.DisableButton(btnNext)
                        Process.DisableButton(btnPrevious)
                        Process.DisableButton(btnSubmit)
                    End If
                End If
            End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")            
        End Try
    End Sub

    Protected Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Try
            Response.Redirect("~/Module/Employee/Performance/FeedBack360Request", True)
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub rdoMyRatings_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rdoMyRatings.SelectedIndexChanged
        Try
            lblMyRating.Text = rdoMyRatings.SelectedValue
            If lblend.Text = "True" Then
                Process.EnableButton(btnSubmit)
            End If
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub btnPrevious_Click(sender As Object, e As EventArgs) Handles btnPrevious.Click
        Try


            Session("QuestionNo") = CInt(Session("QuestionNo")) - 1

            If CInt(Session("QuestionNo")) < CInt(lblQuestCount.Text) Then
                Process.EnableButton(btnNext)
            End If

            If CInt(Session("QuestionNo")) <= 0 Then
                Session("QuestionNo") = CInt(Session("QuestionNo")) + 1
                Process.DisableButton(btnPrevious)
                Process.EnableButton(btnNext)
            Else
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_360_Questionaire_Update", lblQuestID.Text, acomment.Value, lblMyRating.Text)
                If Session("QuestionNo") = 1 Then
                    Process.DisableButton(btnPrevious)
                    Process.EnableButton(btnNext)
                Else
                    Process.EnableButton(btnPrevious)
                End If
                LoadQuestionaire(Session("QuestionNo"), lblid.Text)

            End If
            apageno.InnerText = lbquestno.InnerText & " of " & lblQuestCount.Text
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        Try
            Process.EnableButton(btnSubmit)
            Session("QuestionNo") = CInt(Session("QuestionNo")) + 1
            If CInt(Session("QuestionNo")) > CInt(lblQuestCount.Text) Then
                Session("QuestionNo") = CInt(Session("QuestionNo")) - 1

                Process.DisableButton(btnNext)
                Process.EnableButton(btnPrevious)
            Else
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_360_Questionaire_Update", lblQuestID.Text, acomment.Value, lblMyRating.Text)
                If CInt(Session("QuestionNo")) = CInt(lblQuestCount.Text) Then
                    Process.DisableButton(btnNext)
                    Process.EnableButton(btnPrevious)
                    Process.EnableButton(btnSubmit)
                    lblend.Text = "True"
                Else
                    Process.EnableButton(btnNext)
                    Process.EnableButton(btnPrevious)


                End If
                LoadQuestionaire(Session("QuestionNo"), lblid.Text)
            End If
            apageno.InnerText = lbquestno.InnerText & " of " & lblQuestCount.Text
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Try
            'Performance_Appraisal_Reviewer_Submit
            Dim confirmValue As String = Request.Form("confirm_value")
            If confirmValue = "Yes" Then
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_360_Questionaire_Update", lblQuestID.Text, acomment.Value, lblMyRating.Text)
                LoadReviewee(lblid.Text)
                MultiView1.ActiveViewIndex = 1
            End If
            

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles btnSend2.Click
        Try
            Dim confirmValue As String = Request.Form("confirm_value")
            If confirmValue = "Yes" Then
                If lblreviewerid.Text = Session("UserEmpID") Then
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Performance_Appraisal_360_Submit", lblid.Text)
                    Process.Appraisal_360_Complete(areviewcycle.Value, txtEmpID.Text, lblreviewerid.Text, Process.GetMailLink(AuthenCode, 1))
                End If

                Process.DisableButton(btnSend2)
                MultiView1.ActiveViewIndex = 2

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles btnback.Click
        Try
            MultiView1.ActiveViewIndex = 0
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

End Class