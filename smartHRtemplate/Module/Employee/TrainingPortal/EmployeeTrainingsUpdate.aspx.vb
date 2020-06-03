Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO


Public Class EmployeeTrainingsUpdate
    Inherits System.Web.UI.Page
    Dim empTrainSession As New clsEmpTraining
    Dim AuthenCode As String = "EMPTRAINING"
    Dim olddata(6) As String
    Dim Level1(2) As String
    Dim EmpID As String
    Dim Separators() As Char = {":"c}
    Private Sub MsgDataBound()
        Try
            For Each row As DataListItem In gridAccomplishment.Items
                '' Access the CheckBox
                Dim chcount As HtmlGenericControl = row.FindControl("datscore")
                Dim datprogress As HtmlGenericControl = row.FindControl("datprogress")
                Dim htmlclass As String = ""
                Dim htmlstyle As String = ""
                Dim htmltitle As String = ""
                If (CInt(chcount.InnerText.Replace("%", "")) > 69) Then
                    htmlclass = "progress-bar progress-bar-success"
                ElseIf (CInt(chcount.InnerText.Replace("%", "")) > 49) And (CInt(chcount.InnerText.Replace("%", "")) <= 69) Then
                    htmlclass = "progress-bar progress-bar-info"
                ElseIf (CInt(chcount.InnerText.Replace("%", "")) > 39) And (CInt(chcount.InnerText.Replace("%", "")) <= 49) Then
                    htmlclass = "progress-bar progress-bar-warning"
                Else
                    htmlclass = "progress-bar progress-bar-danger"
                End If
                htmlstyle = "width:" + chcount.InnerText
                htmltitle = chcount.InnerText
                datprogress.Attributes.Add("class", htmlclass)
                datprogress.Attributes.Add("style", htmlstyle)
                datprogress.Attributes.Add("title", htmltitle)
            Next

            For Each row As DataListItem In gridAcquire.Items
                '' Access the CheckBox
                Dim chcount As HtmlGenericControl = row.FindControl("datscore2")
                Dim datprogress As HtmlGenericControl = row.FindControl("datprogress2")
                Dim htmlclass As String = ""
                Dim htmlstyle As String = ""
                Dim htmltitle As String = ""

                htmlstyle = "width:" + chcount.InnerText
                htmltitle = chcount.InnerText
                datprogress.Attributes.Add("style", htmlstyle)
                datprogress.Attributes.Add("title", htmltitle)
            Next
        Catch ex As Exception
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub
    Private Sub LoadAccomplishment(empsessionid As Integer, sessionid As Integer)
        Try
            gridAccomplishment.DataSource = Process.SearchData("Emp_Training_Application_Assessment_Scores", empsessionid)
            gridAccomplishment.DataBind()

            gridAcquire.DataSource = Process.SearchData("Emp_Training_Application_Assessment_Scores", empsessionid)
            gridAcquire.DataBind()

            If (gridAcquire.Items.Count() <= 0) Then
                gridAcquire.DataSource = Process.SearchData("Training_Sessions_Skills_To_Acquire", sessionid)
                gridAcquire.DataBind()
            End If
        Catch ex As Exception
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
               
                cbotrainingstat.Items.Clear()
                cbotrainingstat.Items.Add("Scheduled")
                cbotrainingstat.Items.Add("Attended")
                cbotrainingstat.Items.Add("Not-Attended")
                cbotrainingstat.Items.Add("Cancelled")
                'cbotrainingstat.Enabled = False

                'Company_Structure_get_parent
                If Request.QueryString("id") IsNot Nothing Then
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Employee_Training_Sessions_get", Request.QueryString("id"))

                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    aname.Value = strUser.Tables(0).Rows(0).Item("Employee2").ToString
                    lblEmpID.Text = strUser.Tables(0).Rows(0).Item("empid").ToString
                    atrainingsession.Value = strUser.Tables(0).Rows(0).Item("Sessions").ToString
                    lblsessionid.Text = strUser.Tables(0).Rows(0).Item("trainingsessionid").ToString
                    Process.AssignRadComboValue(cbotrainingstat, strUser.Tables(0).Rows(0).Item("Status").ToString)
                    trainingrate.Value = CDbl(strUser.Tables(0).Rows(0).Item("rating"))
                    aobjective.Value = strUser.Tables(0).Rows(0).Item("objectives").ToString
                    Select Case trainingrate.Value
                        Case 0
                            lbrating.InnerText = "Very Poor"
                        Case 1
                            lbrating.InnerText = "Poor"
                        Case 2
                            lbrating.InnerText = "Average"
                        Case 3
                            lbrating.InnerText = "Good"
                        Case 4
                            lbrating.InnerText = "Very Good"
                        Case 5
                            lbrating.InnerText = "Outstanding"
                    End Select
                    acomment.Value = strUser.Tables(0).Rows(0).Item("Comment").ToString

                    LoadAccomplishment(Request.QueryString("id"), lblsessionid.Text)
                    MsgDataBound()

                    If IsDBNull(strUser.Tables(0).Rows(0).Item("assessment")) = False Then
                        lblassessment.Text = strUser.Tables(0).Rows(0).Item("assessment").ToString
                    Else
                        lblassessment.Text = ""
                    End If

                    'received
                    If IsDBNull(strUser.Tables(0).Rows(0).Item("dateassessment")) = False Then
                        lbldateassessment.Text = strUser.Tables(0).Rows(0).Item("dateassessment").ToString
                    Else
                        lbldateassessment.Text = ""
                    End If


                    If IsDBNull(strUser.Tables(0).Rows(0).Item("appassessstat")) = False Then
                        lblappassessment.Text = strUser.Tables(0).Rows(0).Item("appassessstat").ToString
                    Else
                        lblappassessment.Text = ""
                    End If

                    'received
                    If IsDBNull(strUser.Tables(0).Rows(0).Item("applicationassessmentdate")) = False Then
                        lblappdateassessment.Text = strUser.Tables(0).Rows(0).Item("applicationassessmentdate").ToString
                    Else
                        lblappdateassessment.Text = ""
                    End If

                    'Learning Assement
                    If cbotrainingstat.SelectedItem.Text.ToLower <> "attended" Then
                        lnktrainassessment.Disabled = True
                    Else
                        lnktrainassessment.Disabled = False
                    End If

                    If Session("UserEmpID") = lblEmpID.Text Then
                        cbotrainingstat.Enabled = False
                    End If


                    If lbldateassessment.Text.Trim = "" Or lbldateassessment.Text.Contains("1900") = True Then
                        lnklearnassessment.Disabled = False
                        'lnklearnassessment.Disabled = True
                        lnklearnassessment.InnerText = "Learning Assessment"
                    Else
                        lnklearnassessment.Disabled = True
                        lnklearnassessment.InnerText = "Learning Assessment Taken"
                    End If

                    'Application Asseement
                    If lblappassessment.Text.Trim = "" Or lblappassessment.Text.ToLower <> "received" Then
                        lnkapplicationassessment.Visible = False
                    Else
                        lnkapplicationassessment.Visible = True
                    End If

                    If lblappdateassessment.Text.Trim = "" Or lblappdateassessment.Text.Contains("1900") = True Then
                        lnkapplicationassessment.Disabled = False
                    Else
                        lnkapplicationassessment.Disabled = False
                        lnkapplicationassessment.InnerText = "Application Assessment Submitted"
                    End If
                    Session("EmpID") = lblEmpID.Text
                    atime.Value = Process.AMPM_Time(strUser.Tables(0).Rows(0).Item("trainingtime").ToString)
                    avenue.Value = strUser.Tables(0).Rows(0).Item("DeliveryLocation").ToString  'Replace(vbCrLf, "<br/>")
                    adate.Value = Process.DDMONYYYY(strUser.Tables(0).Rows(0).Item("ScheduledTime")) & " to " & Process.DDMONYYYY(strUser.Tables(0).Rows(0).Item("duedate"))
                Else
                    txtid.Text = "0"
                    lnklearnassessment.Visible = False
                    lnktrainassessment.Visible = False
                End If
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            Dim lblstatus As String = ""
            If (cbotrainingstat.SelectedItem.Text.Trim = "") Or (cbotrainingstat.SelectedItem.Text.Trim.ToLower.Contains("select")) Then
                lblstatus = "Training Status required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                cbotrainingstat.Focus()
                Exit Sub
            End If
            EmpID = lblEmpID.Text


            If txtid.Text.Trim = "" Then
                empTrainSession.id = 0
            Else
                empTrainSession.id = txtid.Text
            End If



            empTrainSession.EmpID = EmpID
            empTrainSession.Status = cbotrainingstat.SelectedItem.Text
            empTrainSession.Training = lblsessionid.Text
            empTrainSession.Comment = acomment.Value
            empTrainSession.Rating = trainingrate.Value
            Dim OldValue As String = ""
            Dim NewValue As String = ""

            Dim j As Integer = 0
            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Surbodinate_Training_Sessions_update", empTrainSession.id, empTrainSession.EmpID, empTrainSession.Training, empTrainSession.Status, empTrainSession.Rating, empTrainSession.Comment)

            If cbotrainingstat.SelectedItem.Text.ToLower <> "attended" Then
                lnktrainassessment.Disabled = True                
            Else
                lnktrainassessment.Disabled = False
            End If
            lblstatus = "Record updated"
            Process.loadalert(divalert, msgalert, lblstatus, "success")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            'Response.Write("<script language='javascript'> { self.close() }</script>")
            Response.Redirect("~/Module/Employee/TrainingPortal/Trainings", True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub



    Protected Sub lnkTrainingAssessment_Click(sender As Object, e As EventArgs)
        Try
            Dim lblstatus As String = ""
            Dim s As String = ""
            If txtid.Text = "0" Then
                lblstatus = "Employee Training Data hasnt been saved!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
            Else
                Dim url As String = "EmployeeTrainingAssessment?assessid=" & txtid.Text & "&session=" & atrainingsession.Value
                s = "window.open('" & url + "', 'popup_window', 'width=1000,height=700,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no');"
                ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)
            End If
        Catch ex As Exception

        End Try
    End Sub



    Protected Sub lnkLearning_Click(sender As Object, e As EventArgs)

        Try
            Dim lblstatus As String = ""
            Dim s As String = ""
            If txtid.Text = "0" Then
                lblstatus = "Employee Training Data hasnt been saved!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
            Else
                'Response.Redirect("~/Module/Trainings/Portal/LearningAssessment.aspx?id=" & txtid.Text, True)
                Dim url As String = "LearningAssessment?id=" & txtid.Text
                s = "window.open('" & url + "', 'popup_window', 'width=800,height=600,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no');"
                ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try

    End Sub

    Protected Sub lnkApplication_Click(sender As Object, e As EventArgs)
        Try
            Dim lblstatus As String = ""
            Dim s As String = ""

            If txtid.Text = "0" Then
                lblstatus = "Employee Training Data hasnt been saved!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
            Else
                Response.Redirect("~/Module/Trainings/Portal/ApplicationAssessment?id=" & txtid.Text, True)
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub trainingrate_Rate(sender As Object, e As EventArgs) Handles trainingrate.Rate
        Try
            Select Case trainingrate.Value
                Case 0
                    lbrating.InnerText = "Very Poor"
                Case 1
                    lbrating.InnerText = "Poor"
                Case 2
                    lbrating.InnerText = "Average"
                Case 3
                    lbrating.InnerText = "Good"
                Case 4
                    lbrating.InnerText = "Very Good"
                Case 5
                    lbrating.InnerText = "Outstanding"
            End Select
        Catch ex As Exception

        End Try
    End Sub

    
End Class