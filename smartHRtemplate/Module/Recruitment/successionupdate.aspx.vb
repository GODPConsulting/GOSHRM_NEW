Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports System.IO


Public Class successionupdate
    Inherits System.Web.UI.Page
    Dim AuthenCode As String = "ADMSUCCESSION"
    Private Sub LoadGrid(jobid As Integer)
        Try
            GridVwHeaderChckbox.DataSource = Process.SearchData("Recruitment_Succession_Detail_Get_All", jobid)
            GridVwHeaderChckbox.AllowSorting = True
            GridVwHeaderChckbox.AllowPaging = True
            GridVwHeaderChckbox.DataBind()
        Catch ex As Exception
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                'ViewState("PreviousPage") = Request.UrlReferrer
                radstatus.Items.Clear()
                radstatus.Items.Add("ready now")
                radstatus.Items.Add("ready in 1 year")
                radstatus.Items.Add("ready in 2-3 years")
                radstatus.Items.Add("ready in 3-5 years")
                radstatus.Items.Add("ready later than 5 years")

                Dim ismulti As String = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select isnull(ismulticompany,'No')  from general_info")

                If ismulti.ToLower = "no" Then
                    Process.LoadRadComboTextAndValueP2(cboplancompany, "Company_Structure_Get_ByLevel", "1", Process.GetCompanyName(""), "name", "name", False)
                    cboplancompany.Visible = False
                    acompany.Visible = False
                    acompany.Value = Session("organisation")
                    Process.AssignRadComboValue(cboplancompany, Session("organisation"))
                    Process.LoadRadComboTextAndValueP1(radplanoffice, "Company_Parent_Breakdown", Process.GetCompanyName(""), "Companys", "Companys", False)
                Else
                    Process.LoadRadComboTextAndValueP2(cboplancompany, "Company_Structure_Get_ByLevel", "2", Process.GetCompanyName(""), "name", "name", False)
                End If

                Process.LoadRadComboTextAndValue(radplanjobgrade, "Job_Grade_get_all", "Name", "Name", False)
                Process.LoadRadComboTextAndValue(radplanjobtitle, "Job_Titles_get_all", "name", "name", False)

                Process.LoadRadComboTextAndValue(radplanlocation, "location_get_all", "name", "name", False)

                Process.LoadRadComboTextAndValueInitiate(radplanjobgrade, "Job_Grade_get_all", "--select--", "Name", "Name")
                Process.LoadRadComboTextAndValueInitiate(radplanjobtitle, "Job_Titles_get_all", "--select--", "name", "name")


                Process.LoadRadComboTextAndValue(radplanoffice, "Company_Structure_get_all", "name", "name", False)
                Process.LoadRadComboTextAndValue(radplanlocation, "location_get_all", "name", "name", False)

                If Request.QueryString("id") IsNot Nothing Then
                    Process.LoadRadComboTextAndValueP1(cboEmployee, "Emp_PersonalDetail_Get_Employees", Process.GetCompanyName(""), "name", "empid", False)

                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruitment_Succession_Get", Request.QueryString("id"))
                    If strUser.Tables(0).Rows.Count > 0 Then
                        cboEmployee.Enabled = False
                        lblID.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                        Process.AssignRadComboValue(cboEmployee, strUser.Tables(0).Rows(0).Item("empid").ToString)
                        acompany.Value = strUser.Tables(0).Rows(0).Item("company").ToString
                        aoffice.Value = strUser.Tables(0).Rows(0).Item("dept").ToString
                        ajobgrade.Value = strUser.Tables(0).Rows(0).Item("jobgrade").ToString
                        ajobtitle.Value = strUser.Tables(0).Rows(0).Item("jobtitle").ToString
                        alocation.Value = strUser.Tables(0).Rows(0).Item("location").ToString
                        aperformancerating.Value = strUser.Tables(0).Rows(0).Item("performancerating").ToString
                        acomment.Value = strUser.Tables(0).Rows(0).Item("reasons").ToString
                        lblhodid.Text = strUser.Tables(0).Rows(0).Item("hod").ToString
                        lblmanagerid.Text = strUser.Tables(0).Rows(0).Item("supervisor").ToString
                        Process.AssignRadComboValue(radstatus, strUser.Tables(0).Rows(0).Item("status").ToString)

                        Process.AssignRadComboValue(cboplancompany, strUser.Tables(0).Rows(0).Item("plannedcompany").ToString)
                        Process.AssignRadComboValue(radplanoffice, strUser.Tables(0).Rows(0).Item("planneddept").ToString)
                        Process.AssignRadComboValue(radplanjobgrade, strUser.Tables(0).Rows(0).Item("plannedjobgrade").ToString)
                        Process.AssignRadComboValue(radplanjobtitle, strUser.Tables(0).Rows(0).Item("plannedjobtitle").ToString)
                        Process.AssignRadComboValue(radplanlocation, strUser.Tables(0).Rows(0).Item("plannedlocation").ToString)

                        If cboEmployee.SelectedValue = Session("UserEmpID") Then
                            cboplancompany.Enabled = False
                            radplanoffice.Enabled = False
                            radplanlocation.Enabled = False
                            radplanjobgrade.Enabled = False
                            radplanjobtitle.Enabled = False
                            acomment.Attributes.Add("readonly", "readonly")
                            btadddetail.Disabled = True
                            btdeletedetail.Enabled = False
                            btsave.Disabled = True

                        End If
                        LoadGrid(lblID.Text)
                    End If

                Else
                    'Session("UserEmpID")
                    lblID.Text = "0"
                    Process.LoadRadComboTextAndValueP1(cboEmployee, "Emp_PersonalDetail_Get_Employees", Session("company"), "name", "empid", False)

                End If
            End If
            If lblID.Text = "0" Then
                btadddetail.Disabled = True
                btdeletedetail.Enabled = False
            Else
                btadddetail.Disabled = False
                btdeletedetail.Enabled = True
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnclose_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect("successionplan", True)
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub viewdetail(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim sid As String = CType(sender, LinkButton).CommandArgument
            Dim url As String = "~/module/employee/recruitment/successiondetail?id=" & sid & "&empid=" & cboEmployee.SelectedValue & "&type=hr"
            If Request.QueryString("appid") IsNot Nothing Then
                url = url & "&appid=" & sid
            End If
            Response.Redirect(url, True)
            'Dim s As String = "window.open('" & url + "', 'popup_window', 'width=650,height=500,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no');"
            'ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
        'Try
        '    Dim sid As String = CType(sender, LinkButton).CommandArgument
        '    Dim url As String = "successiondetails.aspx?id=" & sid & "&empid=" & cboEmployee.SelectedValue
        '    Dim s As String = "window.open('" & url + "', 'popup_window', 'width=650,height=500,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no');"
        '    ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)
        'Catch ex As Exception
        '    Process.loadalert(divalert, msgalert, ex.Message, "danger")
        'End Try
    End Sub
    Private Function GetIdentity() As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "recruitment_succession_update"
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = lblID.Text
            cmd.Parameters.Add("@empid", SqlDbType.VarChar).Value = cboEmployee.SelectedValue
            cmd.Parameters.Add("@company", SqlDbType.VarChar).Value = acompany.Value
            cmd.Parameters.Add("@dept", SqlDbType.VarChar).Value = aoffice.Value
            cmd.Parameters.Add("@location", SqlDbType.VarChar).Value = alocation.Value
            cmd.Parameters.Add("@jobtitle", SqlDbType.VarChar).Value = ajobtitle.Value
            cmd.Parameters.Add("@jobgrade", SqlDbType.VarChar).Value = ajobgrade.Value
            cmd.Parameters.Add("@performancerating", SqlDbType.Decimal).Value = aperformancerating.Value
            cmd.Parameters.Add("@reasons", SqlDbType.VarChar).Value = acomment.Value
            cmd.Parameters.Add("@hod", SqlDbType.VarChar).Value = lblhodid.Text
            cmd.Parameters.Add("@supervisor", SqlDbType.VarChar).Value = lblmanagerid.Text
            cmd.Parameters.Add("@plannedcompany", SqlDbType.VarChar).Value = cboplancompany.SelectedValue
            cmd.Parameters.Add("@planneddept", SqlDbType.VarChar).Value = radplanoffice.SelectedValue
            cmd.Parameters.Add("@plannedlocation", SqlDbType.VarChar).Value = radplanlocation.SelectedValue
            cmd.Parameters.Add("@plannedjobtitle", SqlDbType.VarChar).Value = radplanjobtitle.SelectedValue
            cmd.Parameters.Add("@plannedjobgrade", SqlDbType.VarChar).Value = radplanjobgrade.SelectedValue
            cmd.Parameters.Add("@status", SqlDbType.VarChar).Value = radstatus.SelectedValue
            cmd.Parameters.Add("@createdby", SqlDbType.VarChar).Value = Session("LoginID")
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
            Return "0"
        End Try
    End Function

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            Dim lblstatus As String = ""

            If lblID.Text <> "0" Then
                If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False Then
                    Process.loadalert(divalert, msgalert, Process.privilegemsg, "success")
                    ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
                    Exit Sub
                End If
            End If

            If cboEmployee.SelectedValue = "" Then
                lblstatus = "Employee required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                cboEmployee.Focus()
                Exit Sub
            End If

            If cboplancompany.SelectedValue = "" Then
                lblstatus = "Success Company required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                cboplancompany.Focus()
                Exit Sub
            End If

            If radplanoffice.SelectedValue Is Nothing Then
                lblstatus = "Success Office required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                radplanoffice.Focus()
                Exit Sub
            End If

            If radplanlocation.SelectedValue Is Nothing Then
                lblstatus = "Success location required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                radplanlocation.Focus()
                Exit Sub
            End If

            If radplanjobgrade.SelectedValue Is Nothing Then
                lblstatus = "provide succession job grade!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                radplanjobgrade.Focus()
                Exit Sub
            End If

            If radplanjobtitle.SelectedValue Is Nothing Then
                lblstatus = "succession job title required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                radplanjobtitle.Focus()
                Exit Sub
            End If


            If acomment.Value.Trim = "" Then
                lblstatus = "provide reason for succession plan!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                acomment.Focus()
                Exit Sub
            End If

            If lblID.Text = "0" Then
                lblID.Text = GetIdentity()
                'Process.successionid = lblID.Text
                If lblID.Text = "0" Then
                    Exit Sub
                End If
            Else
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "recruitment_succession_update", CInt(lblID.Text), cboEmployee.SelectedValue, acompany.Value, aoffice.Value, alocation.Value, ajobtitle.Value, ajobgrade.Value, CDbl(aperformancerating.Value), acomment.Value, lblhodid.Text, lblmanagerid.Text, cboplancompany.SelectedValue, radplanoffice.SelectedValue, radplanlocation.SelectedValue, radplanjobtitle.SelectedValue, radplanjobgrade.SelectedValue, radstatus.SelectedValue, Session("LoginID"))
            End If
            divdetail.Visible = True

            lblstatus = "record saved!"
            Process.loadalert(divalert, msgalert, lblstatus, "success")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Protected Sub cboEmployee_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboEmployee.SelectedIndexChanged
        Try
            Dim lblstatus As String = ""
            Dim strUser As New DataSet
            strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_PersonalDetail_get", cboEmployee.SelectedValue)
            If strUser.Tables(0).Rows.Count > 0 Then
                acompany.Value = strUser.Tables(0).Rows(0).Item("company").ToString
                aoffice.Value = strUser.Tables(0).Rows(0).Item("office").ToString
                ajobgrade.Value = strUser.Tables(0).Rows(0).Item("grade").ToString
                ajobtitle.Value = strUser.Tables(0).Rows(0).Item("jobtitle").ToString
                alocation.Value = strUser.Tables(0).Rows(0).Item("location").ToString
                lblmanagerid.Text = strUser.Tables(0).Rows(0).Item("supervisorid").ToString
                lblhodid.Text = strUser.Tables(0).Rows(0).Item("hodid").ToString
                aperformancerating.Value = SqlHelper.ExecuteScalar(WebConfig.ConnectionString, CommandType.Text, "select top 1 case when AdjustGrade is null or AdjustGrade = -1 then isnull(grade,0) else AdjustGrade end from Performance_Appraisal_Summary  where empid = '" & cboEmployee.SelectedValue & "' order by AppraisalCycleID desc")
                If aperformancerating.Value.Trim = "" Then
                    aperformancerating.Value = "0"
                End If

                If cboplancompany.SelectedValue Is Nothing Or cboplancompany.SelectedValue = "" Then
                    Process.AssignRadComboValue(cboplancompany, strUser.Tables(0).Rows(0).Item("company").ToString)
                End If

                Process.LoadRadComboTextAndValueP1(radplanoffice, "Company_Parent_Breakdown", cboplancompany.SelectedValue, "Companys", "Companys", False)
                If radplanoffice.SelectedValue Is Nothing Or radplanoffice.SelectedValue = "" Then
                    Process.AssignRadComboValue(radplanoffice, strUser.Tables(0).Rows(0).Item("office").ToString)
                End If

                If radplanjobgrade.SelectedValue Is Nothing Or radplanjobgrade.SelectedValue = "" Then
                    Process.AssignRadComboValue(radplanjobgrade, strUser.Tables(0).Rows(0).Item("grade").ToString)
                End If

                If radplanjobtitle.SelectedValue Is Nothing Or radplanjobtitle.SelectedValue = "" Then
                    Process.AssignRadComboValue(radplanjobtitle, strUser.Tables(0).Rows(0).Item("jobtitle").ToString)
                End If

                If radplanlocation.SelectedValue Is Nothing Or radplanlocation.SelectedValue = "" Then
                    Process.AssignRadComboValue(radplanlocation, strUser.Tables(0).Rows(0).Item("location").ToString)
                End If
            Else
                acompany.Value = ""
                aoffice.Value = ""
                ajobgrade.Value = ""
                ajobtitle.Value = ""
                alocation.Value = ""
                lblmanagerid.Text = ""
                lblhodid.Text = ""
                aperformancerating.Value = ""
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub radPlancompany_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboplancompany.SelectedIndexChanged
        Try
            Process.LoadRadComboTextAndValueP1(radplanoffice, "Company_Parent_Breakdown", cboplancompany.SelectedValue, "Companys", "Companys", False)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            Dim url As String = "~/module/employee/recruitment/successiondetail?successsionid=" & lblID.Text & "&empid=" & cboEmployee.SelectedValue & "&type=hr"
            If Request.QueryString("appid") IsNot Nothing Then
                url = url & "&appid=" & lblID.Text
            End If
            Response.Redirect(url, True)
            'Dim s As String = "window.open('" & url + "', 'popup_window', 'width=650,height=500,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no');"
            'ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
        'Try
        '    Dim url As String = "successiondetails.aspx?successsionid=" & lblID.Text & "&empid=" & cboEmployee.SelectedValue
        '    Dim s As String = "window.open('" & url + "', 'popup_window', 'width=650,height=500,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no');"
        '    ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)
        'Catch ex As Exception
        '    Process.loadalert(divalert, msgalert, ex.Message, "danger")
        'End Try
    End Sub

    Protected Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btdeletedetail.Click
        Try
            Dim count As Integer = 0
            Dim confirmValue As String = Request.Form("confirm_value")
            If confirmValue = "Yes" Then
                Dim atLeastOneRowDeleted As Boolean = False
                ' Iterate through the Products.Rows property
                For Each row As GridViewRow In GridVwHeaderChckbox.Rows
                    ' Access the CheckBox
                    Dim cb As CheckBox = row.FindControl("chkEmp")
                    If cb IsNot Nothing AndAlso cb.Checked Then
                        count = count + 1
                        ' Delete row! (Well, not really...)
                        atLeastOneRowDeleted = True
                        ' First, get the ProductID for the selected row
                        Dim ID As String = Convert.ToString(GridVwHeaderChckbox.DataKeys(row.RowIndex).Value)
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruitment_Succession_Detail_Delete", ID)
                    End If
                Next
                Process.loadalert(divalert, msgalert, count.ToString & " records successfully deleted", "success")
                LoadGrid(lblID.Text)
            
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub

    Protected Sub lnkApprovalStat_Click(sender As Object, e As EventArgs)
        Try

            Dim url As String = "ApprovalStat.aspx?id=" & lblID.Text & "&stat=succession"
            Dim s As String = "window.open('" & url + "', 'popup_window', 'width=600,height=600,status=yes,resizable=no,toolbar=no,menubar=no,location=center,scrollbars=yes,resizable=no');"
            ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")

        End Try
    End Sub
End Class