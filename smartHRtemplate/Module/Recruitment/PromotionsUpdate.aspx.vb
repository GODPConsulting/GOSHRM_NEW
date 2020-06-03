Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports System.IO


Public Class PromotionsUpdate
    Inherits System.Web.UI.Page
    Dim AuthenCode As String = "ADMPROMOTION"
    Dim AuthenCode2 As String = "ADMPROMOTION"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                Dim ismulti As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select isnull(ismulticompany,'No')  from general_info")
                If ismulti.ToLower = "no" Then
                    Process.LoadRadComboTextAndValueP2(cboCompany, "Company_Structure_Get_ByLevel", "1", Process.GetCompanyName(""), "name", "name", False)
                    divcompany.Visible = False
                    divempcompany.Visible = False
                    Process.AssignRadComboValue(cboCompany, Process.GetCompanyName(""))
                Else
                    Process.LoadRadComboTextAndValueP2(cboCompany, "Company_Structure_Get_ByLevel", "2", Process.GetCompanyName(""), "name", "name", False)
                End If

                Process.LoadRadComboTextAndValueInitiate(radjobgrade, "Job_Grade_get_all", "--select--", "Name", "Name")
                Process.LoadRadComboTextAndValueInitiate(radjobtitle, "Job_Titles_get_all", "--select--", "name", "name")

                Process.LoadRadComboTextAndValueP1(radhod, "Emp_PersonalDetail_Get_Employees", Process.GetCompanyName(""), "name", "empid", False)
                Process.LoadRadComboTextAndValueP1(radsup, "Emp_PersonalDetail_Get_Employees", Process.GetCompanyName(""), "name", "empid", False)
                Process.LoadRadComboTextAndValueP1(cboinitiator, "Emp_PersonalDetail_Get_Employees", Process.GetCompanyName(""), "name", "empid", False)

                Process.LoadRadComboTextAndValue(radoffice, "Company_Structure_get_all", "name", "name", False)
                Process.LoadRadComboTextAndValue(radlocation, "location_get_all", "name", "name", False)

                If Request.QueryString("id") IsNot Nothing Then
                    Dim locked As Boolean = False
                    Process.LoadRadComboTextAndValueP1(cboEmployee, "Emp_PersonalDetail_Get_Employees", Process.GetCompanyName(""), "name", "empid", False)

                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruitment_Promotion_Get", Request.QueryString("id"))
                    If strUser.Tables(0).Rows.Count > 0 Then
                        cboEmployee.Enabled = False
                        'radCompany.Enabled = False
                        'radoffice.Enabled = False
                        'radlocation.Enabled = False
                        'radjobgrade.Enabled = False
                        'radjobtitle.Enabled = False
                        'txtrating.Enabled = False
                        'radhod.Enabled = False
                        'radsup.Enabled = False

                        lblID.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                        Process.AssignRadComboValue(cboEmployee, strUser.Tables(0).Rows(0).Item("empid").ToString)
                        Process.AssignRadComboValue(cboCompany, strUser.Tables(0).Rows(0).Item("company").ToString)
                        Process.AssignRadComboValue(radoffice, strUser.Tables(0).Rows(0).Item("dept").ToString)
                        Process.AssignRadComboValue(radjobgrade, strUser.Tables(0).Rows(0).Item("jobgrade").ToString)
                        Process.AssignRadComboValue(radjobtitle, strUser.Tables(0).Rows(0).Item("jobtitle").ToString)
                        Process.AssignRadComboValue(radlocation, strUser.Tables(0).Rows(0).Item("location").ToString)
                        aeffdate.SelectedDate = CDate(strUser.Tables(0).Rows(0).Item("effectivedate").ToString)

                        aemprating.Value = strUser.Tables(0).Rows(0).Item("performancerating").ToString
                        acomment.Value = strUser.Tables(0).Rows(0).Item("reasons").ToString
                        txtmthservice.Text = strUser.Tables(0).Rows(0).Item("monthsincurrentposition").ToString
                        aempmthposition.Value = strUser.Tables(0).Rows(0).Item("duration").ToString
                        Process.AssignRadComboValue(radhod, strUser.Tables(0).Rows(0).Item("hod").ToString)
                        Process.AssignRadComboValue(radsup, strUser.Tables(0).Rows(0).Item("supervisor").ToString)

                        Process.AssignRadComboValue(cboinitiator, strUser.Tables(0).Rows(0).Item("initiatorid").ToString)
                        approvallink.Visible = True

                        lblpath.Text = strUser.Tables(0).Rows(0).Item("filepath").ToString
                        If lblpath.Text.Trim = "" Then
                            divpromoletter.Visible = False
                        Else
                            divpromoletter.Visible = True
                        End If

                        Dim strHistory As New DataSet
                        strHistory = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Work_History_Get_Actual", cboEmployee.SelectedValue, Process.LastDay(CDate(Process.DDMONYYYY(aeffdate.SelectedDate)).AddMonths(-1).Year, CDate(Process.DDMONYYYY(aeffdate.SelectedDate)).AddMonths(-1).Month))
                        If strHistory.Tables(0).Rows.Count > 0 Then
                            aempcompany.Value = strHistory.Tables(0).Rows(0).Item("company").ToString
                            aempoffice.Value = strHistory.Tables(0).Rows(0).Item("office").ToString
                            aempgrade.Value = strHistory.Tables(0).Rows(0).Item("grade").ToString
                            aemptitle.Value = strHistory.Tables(0).Rows(0).Item("jobtitle").ToString
                        End If

                    End If

                Else
                    'Session("UserEmpID")
                    lblID.Text = "0"
                    divpromoletter.Visible = False

                    Process.LoadRadComboTextAndValueP1(cboEmployee, "Emp_PersonalDetail_Get_Employees", Session("company"), "name", "empid", False)
                    approvallink.Visible = False
                End If
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnClose_Click(sender As Object, e As EventArgs)
        Try
            If Request.QueryString("hr") IsNot Nothing Then
                Response.Redirect("promotions", True)
            End If

        Catch ex As Exception
        End Try
    End Sub
 
    Private Function GetIdentity() As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Recruitment_Promotion_Update"
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = lblID.Text
            cmd.Parameters.Add("@empid", SqlDbType.VarChar).Value = cboEmployee.SelectedValue
            cmd.Parameters.Add("@company", SqlDbType.VarChar).Value = cboCompany.SelectedValue
            cmd.Parameters.Add("@dept", SqlDbType.VarChar).Value = radoffice.SelectedValue
            cmd.Parameters.Add("@location", SqlDbType.VarChar).Value = radlocation.SelectedValue
            cmd.Parameters.Add("@jobtitle", SqlDbType.VarChar).Value = radjobtitle.SelectedValue
            cmd.Parameters.Add("@jobgrade", SqlDbType.VarChar).Value = radjobgrade.SelectedValue
            cmd.Parameters.Add("@performancerating", SqlDbType.Decimal).Value = aemprating.Value
            cmd.Parameters.Add("@reasons", SqlDbType.VarChar).Value = acomment.Value
            cmd.Parameters.Add("@hod", SqlDbType.VarChar).Value = radhod.SelectedValue
            cmd.Parameters.Add("@supervisor", SqlDbType.VarChar).Value = radsup.SelectedValue
            cmd.Parameters.Add("@effectivedate", SqlDbType.DateTime).Value = Process.DDMONYYYY(aeffdate.SelectedDate)
            cmd.Parameters.Add("@monthsincurrentposition", SqlDbType.Int).Value = txtmthservice.Text
            cmd.Parameters.Add("@initiators", SqlDbType.VarChar).Value = cboinitiator.SelectedValue
            cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = Session("LoginID")
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
            Return "0"
            'Session("UserEmpID")
        End Try
    End Function

    Protected Sub btnsave_Click(sender As Object, e As EventArgs)
        Try
            If lblID.Text <> "0" Then
                If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False And Process.AuthenAction(Session("role"), AuthenCode2, "Update") = False Then
                    Process.loadalert(divalert, msgalert, Process.privilegemsg, "warning")
                    Exit Sub
                End If
            End If


            'Session("UserEmpID")

            Dim lblstatus As String = ""
            If cboEmployee.SelectedValue.ToString = "" Then
                lblstatus = "Employee required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                cboEmployee.Focus()
                Exit Sub
            End If

            If cboEmployee.SelectedValue.ToString = Session("UserEmpID") Then
                lblstatus = "you cannot enlist yourself for promotion, save will be aborted!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                cboEmployee.Focus()
                Exit Sub
            End If

            If cboCompany.SelectedValue.ToString = "" Then
                lblstatus = "Company required!"
                cboCompany.Focus()
                Exit Sub
            End If

            If radoffice.SelectedValue.ToString = "" Then
                lblstatus = "Office required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                radoffice.Focus()
                Exit Sub
            End If

            If radjobgrade.SelectedValue.ToString = "" Then
                lblstatus = "Promotion job grade required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                radjobgrade.Focus()
                Exit Sub
            End If

            If radjobtitle.SelectedValue.ToString = "" Then
                lblstatus = "Promotion job title required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                radjobtitle.Focus()
                Exit Sub
            End If

            If radlocation.SelectedValue.ToString = "" Then
                lblstatus = "Location required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                radlocation.Focus()
                Exit Sub
            End If

            If acomment.Value.Trim = "" Then
                lblstatus = "provide reasons for promotion"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                acomment.Focus()
                Exit Sub
            End If

            If aeffdate.SelectedDate Is Nothing Then
                lblstatus = "Effective date required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                aeffdate.Focus()
                Exit Sub
            End If

            'If CDate(Process.DDMONYYYY(aeffdate.SelectedDate)) > Date.Now Then
            '    lblstatus = "Effective date cannot be future date!"
            '    Process.loadalert(divalert, msgalert, lblstatus, "warning")
            '    aeffdate.Focus()
            '    Exit Sub
            'End If

            If lblID.Text = "0" Then
                lblID.Text = GetIdentity()
                If lblID.Text = "0" Then
                    Exit Sub
                End If
            Else
                If GetIdentity() = "0" Then
                    Exit Sub
                End If
            End If


            lblstatus = "Record saved!"
            Process.loadalert(divalert, msgalert, lblstatus, "success")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")            
        End Try
    End Sub

    Protected Sub cboEmployee_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboEmployee.SelectedIndexChanged
        Try
            Dim strUser As New DataSet
            strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_PersonalDetail_get", cboEmployee.SelectedValue)
            If strUser.Tables(0).Rows.Count > 0 Then

                aempcompany.Value = strUser.Tables(0).Rows(0).Item("company").ToString
                aempoffice.Value = strUser.Tables(0).Rows(0).Item("office").ToString
                aempgrade.Value = strUser.Tables(0).Rows(0).Item("grade").ToString
                aemptitle.Value = strUser.Tables(0).Rows(0).Item("jobtitle").ToString
                aemprating.Value = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select top 1 isnull(grade,0) from Performance_Appraisal_Summary where empid = '" & cboEmployee.SelectedValue & "' order by AppraisalCycleID desc")
                If aemprating.Value.Trim = "" Then
                    aemprating.Value = "0"
                End If
                txtmthservice.Text = strUser.Tables(0).Rows(0).Item("MthInService").ToString
                aempmthposition.Value = strUser.Tables(0).Rows(0).Item("duration").ToString
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkApprovalStat_Click(sender As Object, e As EventArgs)

        Try
            Process.loadtype = "promotion"
            Dim url As String = "ApprovalStat.aspx?id=" & lblID.Text & "&stat=promotion"
            'Response.Redirect(url, True)
            Dim s As String = "window.open('" & url + "', 'popup_window', 'width=600,height=600,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no');"
            ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


    Protected Sub btnNotify_Click(sender As Object, e As EventArgs)
        Try
            Dim lblstatus As String = ""
            If lblID.Text = "0" Then
                lblstatus = "Save before generating letter!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                Exit Sub
            Else
                Response.Redirect("~/Module/Recruitment/MailTemplate?empid=" & cboEmployee.SelectedValue & "&template=promotion&id=" & lblID.Text, True)
            End If

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub radCompany_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboCompany.SelectedIndexChanged
        Process.LoadRadComboTextAndValueP1(radoffice, "Company_Parent_Breakdown", cboCompany.SelectedValue, "Companys", "Companys", False)
    End Sub

    Protected Sub radoffice_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles radoffice.SelectedIndexChanged
        Try
            Dim strUser As New DataSet
            strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Company_Structure_get", radoffice.SelectedValue)
            If strUser.Tables(0).Rows.Count > 0 Then
                Process.AssignRadComboValue(radhod, strUser.Tables(0).Rows(0).Item("empid").ToString)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub radjobgrade_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles radjobgrade.SelectedIndexChanged
        Try
            Process.LoadRadComboTextAndValueP2(radsup, "Emp_PersonalDetail_get_Superiors", radjobgrade.SelectedValue, Process.GetCompanyName, "name", "EmpiD", True)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkletter_Click(sender As Object, e As EventArgs)
        Try
            Process.loadalert(divalert, msgalert, "", "danger")
            Process.downloadFile(lblpath.Text)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
End Class