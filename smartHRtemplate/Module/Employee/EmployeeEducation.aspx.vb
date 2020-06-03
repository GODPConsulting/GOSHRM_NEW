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
Public Class EmployeeEducation
    Inherits System.Web.UI.Page
    Dim emailFile As String = ConfigurationManager.AppSettings("FileURL")
    Dim EmpEducation As New clsEmpEducation
    Dim olddata(8) As String
    Dim AuthenCode As String = "EMPLIST"
    Dim AuthenCode2 As String = "MYEMPPROFILE"
    

    Private Sub LoadDynamic()
        Try
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then

                Process.LoadRadComboTextAndValue(cboqualification, "Education_get_all", "name", "name", False)
                Process.LoadRadComboTextAndValueP2(cbostartmonth, "Data_Period_get_all", "N/A", "Present", "period", "period", False)
                Process.LoadRadComboTextAndValueP2(cboendmonth, "Data_Period_get_all", "N/A", "Present", "period", "period", False)

                radstatus.Items.Clear()
                radstatus.Items.Add("Pending")
                radstatus.Items.Add("Rejected")
                radstatus.Items.Add("Approved")

                cbostartyear.Items.Clear()
                cboendyear.Items.Clear()

                For z As Integer = 1980 To 2099
                    cbostartyear.Items.Add(z.ToString)
                    cboendyear.Items.Add(z.ToString)
                Next
                'aname.Value = Process.GetEmployeeData(Session("UserEmpID"), "fullname")
                Session("PreviousCareerPage") = Request.UrlReferrer.ToString

                If Request.QueryString("self") IsNot Nothing Then
                    radstatus.Enabled = False
                End If

                If Request.QueryString("empid") IsNot Nothing Then
                    txtEmpID.Text = Request.QueryString("empid")
                End If

                If Request.QueryString("Id1") IsNot Nothing Then
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Education_get", Request.QueryString("id1"))
                    If strUser.Tables(0).Rows.Count > 0 Then
                        txtEmpID.Text = strUser.Tables(0).Rows(0).Item("EmpID").ToString
                        Process.AssignRadComboValue(cboqualification, strUser.Tables(0).Rows(0).Item("Qualification").ToString)
                        aschool.Value = strUser.Tables(0).Rows(0).Item("Institute").ToString
                        Process.AssignRadComboValue(cbostartmonth, strUser.Tables(0).Rows(0).Item("StartDate").ToString)
                        Process.AssignRadComboValue(cbostartyear, strUser.Tables(0).Rows(0).Item("StartYear").ToString)
                        Process.AssignRadComboValue(cboendmonth, strUser.Tables(0).Rows(0).Item("CompletedOn").ToString)
                        Process.AssignRadComboValue(cboendyear, strUser.Tables(0).Rows(0).Item("CompletedYear").ToString)
                        txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                        afilename.Value = strUser.Tables(0).Rows(0).Item("filename").ToString

                        txtEmpID.Enabled = False
                    Else
                        txtEmpID.Text = Request.QueryString("id1")
                        aname.Value = Process.GetEmployeeData(txtEmpID.Text, "fullname")
                    End If

                ElseIf Request.QueryString("self") IsNot Nothing Then
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Education_get", Request.QueryString("id1"))
                    If strUser.Tables(0).Rows.Count > 0 Then
                        txtEmpID.Text = strUser.Tables(0).Rows(0).Item("EmpID").ToString
                        Process.AssignRadComboValue(cboqualification, strUser.Tables(0).Rows(0).Item("Qualification").ToString)
                        aschool.Value = strUser.Tables(0).Rows(0).Item("Institute").ToString
                        Process.AssignRadComboValue(cbostartmonth, strUser.Tables(0).Rows(0).Item("StartDate").ToString)
                        Process.AssignRadComboValue(cbostartyear, strUser.Tables(0).Rows(0).Item("StartYear").ToString)
                        Process.AssignRadComboValue(cboendmonth, strUser.Tables(0).Rows(0).Item("CompletedOn").ToString)
                        Process.AssignRadComboValue(cboendyear, strUser.Tables(0).Rows(0).Item("CompletedYear").ToString)
                        txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                        afilename.Value = strUser.Tables(0).Rows(0).Item("filename").ToString
                        txtEmpID.Enabled = False
                    Else
                        txtEmpID.Text = Session("EmpID")
                        txtEmpID.Text = Request.QueryString("empid")
                        txtEmpID.Enabled = False
                        txtid.Text = "0"
                        aschool.Value = ""
                        Process.AssignRadComboValue(radstatus, "Pending")
                        Process.AssignRadComboValue(cbostartmonth, "")
                        Process.AssignRadComboValue(cboendmonth, "")
                        Process.AssignRadComboValue(cboendmonth, "")
                        Process.AssignRadComboValue(cboendyear, "")
                    End If
                Else
                    txtEmpID.Text = Session("EmpID")
                    txtEmpID.Text = Request.QueryString("empid")
                    txtEmpID.Enabled = False
                    txtid.Text = "0"
                    aschool.Value = ""
                    Process.AssignRadComboValue(radstatus, "Pending")
                    Process.AssignRadComboValue(cbostartmonth, "")
                    Process.AssignRadComboValue(cboendmonth, "")
                    Process.AssignRadComboValue(cboendmonth, "")
                    Process.AssignRadComboValue(cboendyear, "")
                End If

                aname.Value = Process.GetEmployeeData(txtEmpID.Text, "fullname")
            End If
            If afilename.Value = "" Then
                btndownload.Visible = False
                btnremove.Visible = False
            Else
                btndownload.Visible = True
                btnremove.Visible = True
            End If
            aschool.Focus()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Function UploadFile() As Boolean
        Try
            Dim result As Boolean = False
            If Not file1.PostedFile Is Nothing Then

                Dim strtype As String = ""
                Dim strname As String = ""
                Dim imgdata As Byte() = Nothing
                Dim strsize As Integer = 0

                Dim img_strm As Stream = file1.PostedFile.InputStream
                Dim img_len As Integer = file1.PostedFile.ContentLength
                strtype = file1.PostedFile.ContentType.ToString()
                strname = Path.GetFileName(file1.PostedFile.FileName)
                strsize = file1.PostedFile.ContentLength
                file1.PostedFile.SaveAs(Server.MapPath(emailFile + "EmployeeEducation_" & txtEmpID.Text & strname))
                strname = "EmployeeEducation_" & txtEmpID.Text & strname

                imgdata = New Byte(img_len - 1) {}
                Dim n As Integer = img_strm.Read(imgdata, 0, img_len)

                If txtid.Text = "0" Or txtid.Text = "" Then
                    txtid.Text = GetCertFileID(imgdata, strname, strtype, strsize)
                    If txtid.Text = "0" Then                        
                        result = False
                    End If
                Else
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Education_Update_File", txtid.Text, txtEmpID.Text, cboqualification.SelectedValue, Nothing, strname, strtype, strsize, Session("loginid"))
                    result = True
                End If
                If result = True Then
                    Process.loadalert(divalert, msgalert, "File uploaded", "success")
                    afilename.Value = strname
                    btnremove.Visible = True
                End If

            Else

                If afilename.Value = "" Then
                    Process.loadalert(divalert, msgalert, "No file selected for upload", "warning")
                    result = False
                    file1.Focus()
                Else
                    result = True
                End If
            End If
            Return result
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
            Return False
        End Try
    End Function
    Private Function GetIdentity(ByVal certification As String, ByVal Institute As String, ByVal dategrant As String, ByVal yeargrant As String, ByVal expirydate As String, ByVal expiryyear As String, ByVal stat As String) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Emp_Education_update"
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@EmpID", SqlDbType.VarChar).Value = txtEmpID.Text
            cmd.Parameters.Add("@Qualification", SqlDbType.VarChar).Value = certification
            cmd.Parameters.Add("@Institute", SqlDbType.VarChar).Value = Institute
            cmd.Parameters.Add("@StartDate", SqlDbType.VarChar).Value = dategrant
            cmd.Parameters.Add("@StartYear", SqlDbType.VarChar).Value = yeargrant
            cmd.Parameters.Add("@CompletedOn", SqlDbType.VarChar).Value = expirydate
            cmd.Parameters.Add("@CompletedYear", SqlDbType.VarChar).Value = expiryyear
            cmd.Parameters.Add("@stat", SqlDbType.VarChar).Value = stat
            cmd.Parameters.Add("@approver", SqlDbType.VarChar).Value = Session("UserEmpID")
            cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = Session("loginid")
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
            Return "0"
        End Try
    End Function
    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            Dim lblstatus As String = ""
            If Process.AuthenAction(Session("role"), AuthenCode2, "Create") = False Then
                Process.loadalert(divalert, msgalert, Process.privilegemsg, "warning")
                Exit Sub
            End If


            If (cboqualification.SelectedValue Is Nothing) Then
                lblstatus = "Educational Qualification required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                cboqualification.Focus()
                Exit Sub
            End If

            If aschool.Value.Trim = "" Then
                lblstatus = "Educational Institute where qualification was obtained is required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                aschool.Focus()
                Exit Sub
            End If

            If cbostartmonth.SelectedValue Is Nothing Then
                lblstatus = "Start Date required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                cbostartmonth.Focus()
                Exit Sub
            End If

            If cboendmonth.SelectedValue Is Nothing Then
                lblstatus = "Date Complete required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                cboendmonth.Focus()
                Exit Sub
            End If

            If IsNumeric(cboendyear.SelectedItem.Text) = True Then
                If CInt(cboendyear.SelectedItem.Text) > 0 Then
                    If CInt(cbostartyear.SelectedItem.Text) > CInt(cboendyear.SelectedItem.Text) Then
                        lblstatus = "Start Year cannot be great Year Completed"
                        Process.loadalert(divalert, msgalert, lblstatus, "warning")
                        cbostartyear.Focus()
                        Exit Sub
                    End If
                End If

            End If


            'If Request.QueryString("Id1") IsNot Nothing Then
            '    Dim strUser As New DataSet
            '    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Education_get", Request.QueryString("id1"))
            '    olddata(0) = strUser.Tables(0).Rows(0).Item("id").ToString
            '    olddata(1) = strUser.Tables(0).Rows(0).Item("EmpID").ToString
            '    olddata(2) = strUser.Tables(0).Rows(0).Item("Qualification").ToString
            '    olddata(3) = strUser.Tables(0).Rows(0).Item("Institute").ToString
            '    olddata(4) = strUser.Tables(0).Rows(0).Item("StartDate").ToString
            '    olddata(5) = strUser.Tables(0).Rows(0).Item("StartYear").ToString
            '    olddata(6) = strUser.Tables(0).Rows(0).Item("CompletedOn").ToString
            '    olddata(7) = strUser.Tables(0).Rows(0).Item("CompletedYear").ToString
            'End If


            If txtid.Text.Trim = "" Then
                EmpEducation.ID = 0
            Else
                EmpEducation.ID = txtid.Text
            End If
            EmpEducation.EmpID = txtEmpID.Text.Trim
            EmpEducation.Institute = aschool.Value.Trim
            EmpEducation.Qualification = cboqualification.SelectedValue
            EmpEducation.StartDate = cbostartmonth.SelectedItem.Text
            EmpEducation.StartYear = cbostartyear.SelectedItem.Text
            EmpEducation.CompletedOn = cboendmonth.SelectedItem.Text
            EmpEducation.YearCompleted = cboendyear.SelectedItem.Text

            'Dim OldValue As String = ""
            'Dim NewValue As String = ""

            'Dim j As Integer = 0

            'If Request.QueryString("Id1") IsNot Nothing Then 'Updates
            '    For Each a In GetType(clsEmpEducation).GetProperties()
            '        If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
            '            If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
            '                If IsNumeric(a.GetValue(EmpEducation, Nothing)) = True And IsNumeric(olddata(j)) = True Then
            '                    If CDbl(a.GetValue(EmpEducation, Nothing)) <> CDbl(olddata(j)) Then
            '                        NewValue += a.Name + ": " + a.GetValue(EmpEducation, Nothing).ToString & vbCrLf
            '                        OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
            '                    End If
            '                Else
            '                    If a.GetValue(EmpEducation, Nothing).ToString <> olddata(j).ToString Then
            '                        NewValue += a.Name + ": " + a.GetValue(EmpEducation, Nothing).ToString & vbCrLf
            '                        OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
            '                    End If
            '                End If
            '            End If
            '        End If
            '        j = j + 1
            '    Next
            'Else
            '    For Each a In GetType(clsEmpEducation).GetProperties() 'New Entries
            '        If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
            '            If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
            '                If a.GetValue(EmpEducation, Nothing) = Nothing Then
            '                    NewValue += a.Name + ":" + " " & vbCrLf
            '                Else
            '                    NewValue += a.Name + ": " + a.GetValue(EmpEducation, Nothing).ToString & vbCrLf
            '                End If
            '            End If
            '        End If
            '    Next
            'End If

            If txtid.Text = "0" Or txtid.Text = "" Then
                txtid.Text = GetIdentity(EmpEducation.Qualification, EmpEducation.Institute, EmpEducation.StartDate, EmpEducation.StartYear, EmpEducation.CompletedOn, EmpEducation.YearCompleted, radstatus.SelectedItem.Text)
                If txtid.Text = "0" Then
                    Exit Sub
                End If

            Else
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Education_update", EmpEducation.ID, EmpEducation.EmpID, EmpEducation.Qualification, EmpEducation.Institute, EmpEducation.StartDate, EmpEducation.StartYear, EmpEducation.CompletedOn, EmpEducation.YearCompleted, radstatus.SelectedItem.Text, Session("UserEmpID"), Session("loginid"))
                Process.Training_Employee(radstatus.SelectedItem.Text, "Your Education Qualification has been " & radstatus.SelectedItem.Text, txtEmpID.Text, Session("UserEmpID"), cboqualification.SelectedValue)
            End If

            If afilename.Value = "" Then
                Dim oresult = UploadFile()
                If oresult = False Then
                    Exit Sub
                End If
            Else

            End If

            If Request.QueryString("self") IsNot Nothing Then
                Dim initiator As String = ""
                Dim initiatorname As String = ""
                Dim office As String = ""

                Dim strEmp As New DataSet
                strEmp = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, CommandType.Text, "select a.Name, a.Employee2 Employee,Email, Office     from dbo.Employees_All a where a.EmpID = '" & txtEmpID.Text & "'")
                If strEmp.Tables(0).Rows.Count > 0 Then
                    initiator = strEmp.Tables(0).Rows(0).Item("Email").ToString
                    initiatorname = strEmp.Tables(0).Rows(0).Item("Name").ToString
                    office = strEmp.Tables(0).Rows(0).Item("Office").ToString
                End If
                Dim url As String = Process.GetMailLink(AuthenCode2, 1)
                url = Process.ApplicationURL & "/" & "Module/Employee/EmployeeData"
                'url = Process.ApplicationURL & "/" & "Module/Employee/EmployeeEducation"
                Process.Credential_Acceptance_Request(cboqualification.SelectedValue, Process.GetMailList("hr"), initiatorname & " of " & office, "Education Certificate", initiator, txtEmpID.Text, "", url & "?id=" & txtEmpID.Text)
                lblstatus = "Changes has been forwarded to HR for acceptance"
                Process.loadalert(divalert, msgalert, lblstatus, "success")
                Exit Sub
            Else
                lblstatus = "Record saved"
                Process.loadalert(divalert, msgalert, lblstatus, "success")
            End If


            'If NewValue.Trim = "" And OldValue.Trim = "" Then
            'Else
            '    If Request.QueryString("Id1") IsNot Nothing Then
            '        Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Updated " + EmpEducation.EmpID, "Employee Education")
            '    Else
            '        Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Inserted", "Employee Education")
            '    End If
            'End If
            lblstatus = "Record saved"
            Process.loadalert(divalert, msgalert, lblstatus, "success")

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


    Private Function GetCertFileID(ByVal fileimage As Byte(), ByVal filename As String, ByVal filetype As String, ByVal filesize As String) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Emp_Education_Update_File"
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@empid", SqlDbType.VarChar).Value = txtEmpID.Text
            cmd.Parameters.Add("@cert", SqlDbType.VarChar).Value = cboqualification.SelectedValue
            cmd.Parameters.Add("@fileimage", SqlDbType.Image).Value = fileimage
            cmd.Parameters.Add("@filename", SqlDbType.VarChar).Value = filename
            cmd.Parameters.Add("@filetype", SqlDbType.VarChar).Value = filetype
            cmd.Parameters.Add("@filesize", SqlDbType.BigInt).Value = filesize
            cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = Session("loginid")
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
            Return "0"
        End Try
    End Function


    Protected Sub remove_Click(sender As Object, e As EventArgs)
        Try

            Dim lblstatus As String = ""
            If radstatus.SelectedItem.Text.ToLower = "approved" Then
                lblstatus = "Attachment is already approved and accepted, clear aborted!"
                Process.loadalert(divalert, msgalert, lblstatus, "info")
            Else
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Education_Update_File_Clear", txtid.Text, Session("loginid"))
                lblstatus = "Attachment cleared"
                Process.loadalert(divalert, msgalert, lblstatus, "success")
                afilename.Value = ""
            End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub cboendmonth_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboendmonth.SelectedIndexChanged
        Try
            If cboendyear.SelectedItem.Text.ToLower = "n/a" Or cboendyear.SelectedItem.Text.ToLower = "present" Then
                cboendyear.Visible = False
                Process.AssignRadComboValue(cboendyear, "0")
            Else
                cboendyear.Visible = True
                Process.AssignRadComboValue(cboendyear, CInt(cbostartyear.SelectedItem.Text) + 1)
            End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub downloadFile(ByVal bytefile As Byte(), ByVal filetype As String, ByVal filename As String)
        Dim bytes() As Byte = bytefile
        Response.Buffer = True
        Response.Charset = ""
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = filetype
        Response.AddHeader("content-disposition", "attachment;filename=" & filename)
        Response.BinaryWrite(bytes)
        Response.Flush()
        Response.End()
    End Sub
    Protected Sub Download_Click(sender As Object, e As EventArgs)
        Try
            Dim dt As DataTable = Process.SearchData("Emp_Education_get", txtid.Text)
            If dt IsNot Nothing Then
                'downloadFile(CType(dt.Rows(0)("fileimage"), Byte()), dt.Rows(0)("filetype").ToString(), dt.Rows(0)("filename").ToString())
            End If
            Dim fileName As String = dt.Rows(0)("filename").ToString()
            Dim filePath As String = Server.MapPath(emailFile & fileName)
            Response.ContentType = ContentType
            Response.AppendHeader("Content-Disposition", ("attachment; filename=" + Path.GetFileName(filePath)))
            Response.WriteFile(filePath)
            Response.End()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub Back(sender As Object, e As EventArgs)
        Try
            If Request.QueryString("self") IsNot Nothing Then
                Response.Redirect("employeeprofile", True)
            Else
                Response.Redirect("EmployeeData?id=" + txtEmpID.Text, True)
            End If


        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub btnClose_Click(sender As Object, e As EventArgs)
        If Request.QueryString("self") IsNot Nothing Then
            Response.Redirect("~/Module/Employee/EmployeeProfile.aspx", True)
        End If
        If Request.QueryString("Id1") IsNot Nothing Then
            Response.Redirect("~/Module/Employee/EmployeeData.aspx?Id=" & Session("EmpID"), True)
        End If
        'Response.Redirect("~/Module/Employee/EmployeeData.aspx?Id=" & Session("EmpID"), True)
    End Sub
End Class