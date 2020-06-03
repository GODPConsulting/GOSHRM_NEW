Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Telerik.Web.UI


Public Class successiondetail
    Inherits System.Web.UI.Page
    Dim monthlyearning As New clsNonPayroll

    Dim olddata(5) As String
    Dim Pages As String = "Non Payroll Items"
   
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not Me.IsPostBack Then
                cbostatus.Items.Clear()
                cbostatus.Items.Add("Not Started")
                cbostatus.Items.Add("In progress")
                cbostatus.Items.Add("Completed")
                Process.LoadRadComboTextAndValueP1(cboemployee, "Emp_PersonalDetail_Get_Employees", Session("Access"), "name", "empid")
                'Company_Structure_get_parent
                If Request.QueryString("id") IsNot Nothing Then
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Recruitment_Succession_Detail_Get", Request.QueryString("id"))
                    lblid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    lblsuccessionid.Text = strUser.Tables(0).Rows(0).Item("successionid").ToString
                    akeyaction.Value = strUser.Tables(0).Rows(0).Item("action").ToString
                    Process.AssignRadComboValue(cbostatus, strUser.Tables(0).Rows(0).Item("status").ToString)
                    astartdate.SelectedDate = CDate(strUser.Tables(0).Rows(0).Item("expectedstartdate"))
                    aduedate.SelectedDate = CDate(strUser.Tables(0).Rows(0).Item("expectedenddate"))
                    Process.LoadListAndComboxFromDataset(lstemployees, cboemployee, "Recruitment_Succession_Detail_Responsibility_Get", "name", "empid", lblid.Text)

                Else
                    lblid.Text = "0"
                    lblsuccessionid.Text = Request.QueryString("successsionid")
                    Process.AssignRadComboValue(cbostatus, "Not Started")
                End If

                If Request.QueryString("empid") = Session("UserEmpID") Then
                    cboemployee.Enabled = False
                    akeyaction.Attributes.Add("readonly", "readonly")
                    astartdate.Enabled = False
                    aduedate.Enabled = False
                    btnupdate.Disabled = True
                End If

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Function GetIdentity() As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Recruitment_Succession_Detail_Update"
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = lblid.Text
            cmd.Parameters.Add("@successionid", SqlDbType.Int).Value = lblsuccessionid.Text
            cmd.Parameters.Add("@action", SqlDbType.VarChar).Value = akeyaction.Value
            cmd.Parameters.Add("@expectedstartdate", SqlDbType.Date).Value = Process.DDMONYYYY(astartdate.SelectedDate)
            cmd.Parameters.Add("@expectedenddate", SqlDbType.Date).Value = Process.DDMONYYYY(aduedate.SelectedDate)
            cmd.Parameters.Add("@status", SqlDbType.VarChar).Value = cbostatus.SelectedItem.Text
            cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = Session("LoginID")
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
            Return 0
        End Try
    End Function
    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try

            Dim lblstatus As String = ""

            If (akeyaction.Value.Trim = "") Then
                lblstatus = "action statement required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                akeyaction.Focus()
                Exit Sub
            End If

            If astartdate.SelectedDate Is Nothing Then
                lblstatus = "Expected start date required!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                astartdate.Focus()
                Exit Sub
            End If

            If aduedate.SelectedDate Is Nothing Then
                lblstatus = "Expected due date required!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                aduedate.Focus()
                Exit Sub
            End If

            If CDate(Process.DDMONYYYY(astartdate.SelectedDate)) > CDate(Process.DDMONYYYY(aduedate.SelectedDate)) Then
                lblstatus = "Start date cannot be after due date!"
                Process.loadalert(divalert, msgalert, lblstatus, "danger")
                astartdate.Focus()
                Exit Sub
            End If


            'Old Data
            If lblid.Text = "0" Then
                lblid.Text = GetIdentity()
                If lblid.Text = "0" Then
                    Exit Sub
                End If
            Else
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruitment_Succession_Detail_Update", lblid.Text, lblsuccessionid.Text, akeyaction.Value, astartdate.SelectedDate, aduedate.SelectedDate, cbostatus.SelectedItem.Text, Session("LoginID"))
            End If


            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruitment_Succession_Detail_Responsibility_Delete", lblid.Text)
            If lstemployees.Items.Count <= 0 Then
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruitment_Succession_Detail_Responsibility_Update", lblid.Text, Request.QueryString("empid"))
            Else
                For d As Integer = 0 To lstemployees.Items.Count - 1
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Recruitment_Succession_Detail_Responsibility_Update", lblid.Text, lstemployees.Items(d).Value)
                Next
            End If

            Process.LoadListAndComboxFromDataset(lstemployees, cboemployee, "Recruitment_Succession_Detail_Responsibility_Get", "name", "empid", lblid.Text)
            lblstatus = "Record saved"
            Process.loadalert(divalert, msgalert, lblstatus, "success")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub btnlist_Click(sender As Object, e As EventArgs)
        Try
            If Request.QueryString("appid") IsNot Nothing Then
                Response.Redirect("SuccessionPlans", True)
            ElseIf Request.QueryString("type") = "user" Then
                Response.Redirect("SuccessionPlans", True)
            ElseIf Request.QueryString("type") = "hr" Then
                Response.Redirect("~/module/recruitment/successionplan", True)
            End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            If Request.QueryString("appid") IsNot Nothing Then
                Response.Redirect("successionplanupdate?appid=" & lblsuccessionid.Text, True)
            ElseIf Request.QueryString("type") = "user" Then
                Response.Redirect("successionplanupdate?id=" & lblsuccessionid.Text, True)
            ElseIf Request.QueryString("type") = "hr" Then
                Response.Redirect("~/module/recruitment/successionupdate?id=" & lblsuccessionid.Text, True)
            End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


    Private Sub ReloadComponentList()
        Try
            lstemployees.Items.Clear()
            Dim collection As IList(Of RadComboBoxItem) = cboemployee.CheckedItems
            If (collection.Count > 0) Then
                For Each item As RadComboBoxItem In collection
                    Dim listitem As New RadListBoxItem()
                    listitem.Text = item.Text
                    listitem.Value = item.Value
                    lstemployees.Items.Add(listitem)
                    listitem.DataBind()
                Next
            Else
                lstemployees.Items.Clear()
            End If
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub radComponents_ItemChecked(sender As Object, e As Telerik.Web.UI.RadComboBoxItemEventArgs) Handles cboemployee.ItemChecked
        ReloadComponentList()
    End Sub
End Class