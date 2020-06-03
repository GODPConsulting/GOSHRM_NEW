Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO


Public Class EmpConfirmationUpdate
    Inherits System.Web.UI.Page
    Dim exits As New clsExit
    Dim mmail As String
    Dim AuthenCode As String = "STAFFCONFIRMATION"
    Dim olddata(3) As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then

                Process.LoadRadComboTextAndValueInitiateP1(cboManager, "Emp_PersonalDetail_Get_Employees", Session("Access"), "--Select--", "name", "EmpID")
                'Company_Structure_get_parent
                If Request.QueryString("id") IsNot Nothing Then
                    Process.LoadRadComboTextAndValueInitiateP1(cboEmployee, "Emp_PersonalDetail_Get_Employees", Session("Access"), "--Select--", "name", "EmpID")
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruit_Confirmation_Get", Request.QueryString("id"))
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    Process.AssignRadComboValue(cboEmployee, strUser.Tables(0).Rows(0).Item("recruitid").ToString)
                    Process.AssignRadComboValue(cboManager, strUser.Tables(0).Rows(0).Item("empid").ToString)

                    aoffice.Value = strUser.Tables(0).Rows(0).Item("office").ToString
                    mmail = strUser.Tables(0).Rows(0).Item("office").ToString
                    aprobation.Value = strUser.Tables(0).Rows(0).Item("probation").ToString
                    adatejoined.Value = CDate(strUser.Tables(0).Rows(0).Item("datejoin")).ToLongDateString
                    lbtargetachived.Value = strUser.Tables(0).Rows(0).Item("targetsachieved").ToString
                    aareadevelopment.Value = strUser.Tables(0).Rows(0).Item("areaofdevelopment").ToString
                    acomment.Value = strUser.Tables(0).Rows(0).Item("targetcomments").ToString
                    RadRating1.Value = CDbl(strUser.Tables(0).Rows(0).Item("rating"))
                    arecommendation.Value = strUser.Tables(0).Rows(0).Item("recommendation").ToString()
                    Process.AssignRadComboValue(cborecommendation, strUser.Tables(0).Rows(0).Item("hrrecommendation").ToString)
                    aProbationExtension.Value = strUser.Tables(0).Rows(0).Item("probationextension").ToString
                    lbcompletestat.Value = strUser.Tables(0).Rows(0).Item("completestat").ToString
                    lblconfirmation.Text = strUser.Tables(0).Rows(0).Item("letter").ToString



                    ahrcomment.Value = strUser.Tables(0).Rows(0).Item("hrcomment").ToString
                    ahrmanager.Value = strUser.Tables(0).Rows(0).Item("HRManagerName").ToString
                    If IsDBNull(strUser.Tables(0).Rows(0).Item("confirmationdate")) = False Then
                        radConfirm.SelectedDate = CDate(strUser.Tables(0).Rows(0).Item("confirmationdate"))
                    End If


                    If cborecommendation.SelectedValue.ToUpper.Contains("EXTEND") = True Then
                        divhrresponse.Visible = True
                        radConfirm.Visible = False
                        lbExtendProbationID.InnerText = "Extend Probation By:"
                    ElseIf cborecommendation.SelectedValue.ToUpper.Contains("CONFIRM") Then
                        divhrresponse.Visible = True
                        aProbationExtension.Visible = False
                        lbExtendProbationID.InnerText = "Confirmation Date:"
                        radConfirm.Visible = True
                    Else
                        divhrresponse.Visible = False
                        aProbationExtension.Value = "0"

                    End If

                    cboEmployee.Enabled = False
                    Select Case RadRating1.Value
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
                    lblpath.Text = strUser.Tables(0).Rows(0).Item("filepath").ToString
                    If lblpath.Text.Trim = "" Then
                        divconfirmation.Visible = False
                    Else
                        divconfirmation.Visible = True
                    End If
                Else
                    Process.LoadRadComboTextAndValueInitiateP1(cboEmployee, "Emp_PersonalDetail_Get_NotCofirmed", Session("Access"), "--Select--", "name", "EmpID")
                    txtid.Text = "0"
                    Process.AssignRadComboValue(cborecommendation, "Pending")
                    divconfirmation.Visible = False
                End If

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            Dim lblstatus As String = ""
            If cboEmployee.SelectedValue Is Nothing Then
                lblstatus = "Select Employee to confirm!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                cboEmployee.Focus()
                Exit Sub
            End If

            Dim confirmdate As Date = Date.Now
            If radConfirm.Visible = True Then
                If radConfirm.SelectedDate Is Nothing Then
                    lblstatus = "Select a confirmdation date for recruited employee!"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    radConfirm.Focus()
                    Exit Sub
                End If
            Else
                If radConfirm.SelectedDate Is Nothing Then
                    confirmdate = Nothing
                Else
                    confirmdate = radConfirm.SelectedDate
                End If
            End If



            If txtid.Text = "0" Then
                txtid.Text = GetIdentity(cboManager.SelectedValue, cboEmployee.SelectedValue, aprobation.Value.Trim, aoffice.Value, RadRating1.Value, cborecommendation.SelectedValue, ahrcomment.Value, aProbationExtension.Value, Session("LoginID"), confirmdate)
                If txtid.Text = "0" Then                    
                    Exit Sub
                End If
            Else
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruit_Confirmation_hr_Update", txtid.Text, cboManager.SelectedValue, cboEmployee.SelectedValue, aprobation.Value.Trim, aoffice.Value, RadRating1.Value, cborecommendation.SelectedValue, ahrcomment.Value, aProbationExtension.Value, Session("LoginID"), Session("UserEmpID"), confirmdate)
            End If
            lblstatus = "Record saved!"
            Process.loadalert(divalert, msgalert, lblstatus, "success")

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect("~/Module/Recruitment/employeeconfirmation", True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


    Protected Sub cboEmployee_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboEmployee.SelectedIndexChanged
        Try
            Dim strEmp As New DataSet
            strEmp = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_PersonalDetail_get", cboEmployee.SelectedValue)
            If strEmp.Tables(0).Rows.Count > 0 Then
                adatejoined.Value = CDate(strEmp.Tables(0).Rows(0).Item("datejoin")).ToLongDateString
                aprobation.Value = strEmp.Tables(0).Rows(0).Item("ProbationPeriod").ToString
                aoffice.Value = strEmp.Tables(0).Rows(0).Item("Office").ToString
                Process.AssignRadComboValue(cboManager, strEmp.Tables(0).Rows(0).Item("SupervisorID").ToString)
            Else
                adatejoined.Value = ""
                aprobation.Value = "0"
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub cborecommendation_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cborecommendation.SelectedIndexChanged
        Try

            If cborecommendation.SelectedValue.ToUpper.Contains("EXTEND") = True Then
                divhrresponse.Visible = True
                radConfirm.Visible = False
                lbExtendProbationID.InnerText = "Extend Probation By:"
            ElseIf cborecommendation.SelectedValue.ToUpper.Contains("CONFIRM") Then
                divhrresponse.Visible = True
                aProbationExtension.Visible = False
                lbExtendProbationID.InnerText = "Confirmation Date:"
                radConfirm.Visible = True
            Else
                divhrresponse.Visible = False
                aProbationExtension.Value = "0"

            End If

          
        Catch ex As Exception

        End Try
    End Sub
    Private Function GetIdentity(ByVal empid As String, ByVal recruitid As String, ByVal probation As Integer, _
                                ByVal dept As String, ByVal ratings As Double, ByVal recommendation As String, _
                                 ByVal comments As String, ByVal probationext As Integer, ByVal strusers As String, ByVal confirmdate As Date) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Recruit_Confirmation_hr_Update"
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@empid", SqlDbType.VarChar).Value = empid
            cmd.Parameters.Add("@recruitid", SqlDbType.VarChar).Value = recruitid
            cmd.Parameters.Add("@probation", SqlDbType.Int).Value = probation
            cmd.Parameters.Add("@office", SqlDbType.VarChar).Value = dept
            cmd.Parameters.Add("@rating", SqlDbType.Decimal).Value = ratings
            cmd.Parameters.Add("@recommendation", SqlDbType.VarChar).Value = recommendation
            cmd.Parameters.Add("@comment", SqlDbType.VarChar).Value = comments
            cmd.Parameters.Add("@probationextension", SqlDbType.Int).Value = probationext
            cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = strusers
            cmd.Parameters.Add("@hr", SqlDbType.VarChar).Value = Session("UserEmpID")
            cmd.Parameters.Add("@confirm", SqlDbType.VarChar).Value = confirmdate
            'Session("UserEmpID")
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
            Return 0
        End Try
    End Function
    Protected Sub RadRating1_Rate(sender As Object, e As EventArgs) Handles RadRating1.Rate
        Try
            Select Case RadRating1.Value
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

    Protected Sub btnConfirmation_Click(sender As Object, e As EventArgs)
        Try
            Dim lblstatus As String = ""
            Process.loadalert(divalert, msgalert, lblstatus, "warning")
            If txtid.Text = "0" Then
                lblstatus = "Save confirmation detail before generating confirmation letter!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                Exit Sub
            Else
                If radConfirm.SelectedDate Is Nothing Then
                    lblstatus = "Confirmation date required!"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    Exit Sub
                Else
                    'Dim url As String = "MailTemplate.aspx?empid=" & cboEmployee.SelectedValue & "&template=confirmation&id=" & txtid.Text & "&date=" & Process.DDMONYYYY(radConfirm.SelectedDate)
                    'Dim s As String = "window.open('" & url + "', 'popup_window', 'width=700,height=700,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no');"
                    'ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)

                    Response.Redirect("~/Module/Recruitment/MailTemplate?empid=" & cboEmployee.SelectedValue & "&template=confirmation&id=" & txtid.Text & "&date=" & Process.DDMONYYYY(radConfirm.SelectedDate), True)
                End If

            End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub lnkletter_Click(sender As Object, e As EventArgs)
        Try
            Process.loadalert(divalert, msgalert, "", "warning")
            Process.downloadFile(lblpath.Text)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
End Class