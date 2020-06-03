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


Public Class MyExit
    Inherits System.Web.UI.Page
    Dim education As New clsEducation
    Dim Separators() As Char = {","c}
    Dim authencode As String = "EMPMYEXIT"
    Private Sub LoadData()
        Try

            GridVwHeaderChckbox.DataSource = Process.SearchData("Emp_Termination_Get_Employee", Session("UserEmpID"))
            GridVwHeaderChckbox.AllowSorting = True
            GridVwHeaderChckbox.AllowPaging = True
            GridVwHeaderChckbox.DataBind()

            If GridVwHeaderChckbox.Rows.Count > 0 Then
                MultiView1.ActiveViewIndex = 2
            Else
                MultiView1.ActiveViewIndex = 0
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.message, "danger")
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & ex.Message + "')", True)
        End Try
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'SingleLineTextBox, // will render a textbox 
            'MultiLineTextBox, // will render a text area
            'YesOrNo, //will render a checkbox
            'SingleSelect, //will render a dropdownlist
            'MultiSelect //will render a listbo

            If Not Me.IsPostBack Then
                pagetitle.InnerText = "JOB EXIT REQUEST"
                Process.LoadRadComboTextAndValueInitiateP2(radForwardTo, "Emp_PersonalDetail_get_Superiors", Session("UserJobgrade"), Process.GetCompanyName, "-- Forward To --", "name", "EmpID")
                Process.LoadRadComboTextAndValue(cboexittype, "Emp_Exit_Type_Self_Get_All", "Name", "Name", False)
                txtEmpID.Text = Session("UserEmpID")
                aname.Value = Session("EmpName")

                anoticedate.SelectedDate = Date.Now
                anoticedate.Enabled = True
                Process.AssignRadComboValue(radForwardTo, Process.GetEmployeeData(txtEmpID.Text, "linemanagerid"))
                LoadData()

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.message, "danger")
        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btsend.Click
        Try
            Dim lblstatus As String = ""
            Dim confirmValue As String = Request.Form("confirmsave_value")
            If confirmValue = "Yes" Then
                If radForwardTo.SelectedItem.Value = "" Then
                    lblstatus = "Select your line manager to approval your exit!"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    radForwardTo.Focus()
                    Exit Sub
                End If

                If cboexittype.SelectedValue Is Nothing Then
                    lblstatus = "Exit Type required!"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    cboexittype.Focus()
                    Exit Sub
                End If

                If aexitdate.SelectedDate Is Nothing Then
                    lblstatus = "Exit Date is required!"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    aexitdate.Focus()
                    Exit Sub
                End If

                If CDate(anoticedate.SelectedDate) > CDate(aexitdate.SelectedDate) Then
                    lblstatus = "Exit Date cannot be earlier than Notice Date!"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    aexitdate.Focus()
                    Exit Sub
                End If

                If areason.Value.Trim = "" Then
                    lblstatus = "Exit Reason is required!"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    areason.Focus()
                    Exit Sub
                End If


                Dim forwardemail As String = ""
                Dim forwardname As String = ""

                Dim initatorname As String = ""
                Dim initatoremail As String = ""
                Dim initatorposition As String = ""
                Dim initatordept As String = ""

                Dim strEmp As New DataSet
                strEmp = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_PersonalDetail_get", radForwardTo.SelectedItem.Value)
                If strEmp.Tables(0).Rows.Count > 0 Then
                    forwardemail = strEmp.Tables(0).Rows(0).Item("Office Email").ToString
                    forwardname = strEmp.Tables(0).Rows(0).Item("fullname").ToString
                End If

                strEmp = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_PersonalDetail_get", txtEmpID.Text)
                If strEmp.Tables(0).Rows.Count > 0 Then
                    initatoremail = strEmp.Tables(0).Rows(0).Item("Office Email").ToString
                    initatorname = strEmp.Tables(0).Rows(0).Item("fullname").ToString
                    initatorposition = strEmp.Tables(0).Rows(0).Item("Jobtitle").ToString
                    initatordept = strEmp.Tables(0).Rows(0).Item("Office").ToString
                End If

                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Termination_Update_Employee", 0, txtEmpID.Text, radForwardTo.SelectedItem.Value, anoticedate.SelectedDate, aexitdate.SelectedDate, areason.Value, cboexittype.SelectedValue)

                Process.Exit_For_HOD_Approval(aexitdate.SelectedDate, cboexittype.SelectedValue, areason.Value, txtEmpID.Text, radForwardTo.SelectedItem.Value, Process.ApplicationURL() + "/" + Process.GetMailLink(authencode, 2))

                MultiView1.ActiveViewIndex = 1
                lbnotify.InnerText = "Your Exit request has been forwarded to " & Process.GetEmployeeData(radForwardTo.SelectedValue, "fullname") & " for approval"
            End If



        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try

    End Sub


    Protected Sub btnClose_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/empdashboard")
        'MultiView1.ActiveViewIndex = 2
        'LoadData()
    End Sub


    Protected Sub btnOK_Click(sender As Object, e As EventArgs)
        Try
            MultiView1.ActiveViewIndex = 2
            LoadData()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            If Process.AuthenAction(Session("role"), authencode, "Create") = False Then
                Process.loadalert(divalert, msgalert, Process.privilegemsg, "warning")
                Exit Sub
            End If
            Dim lblstatus As String = ""
            Dim strEmp As New DataSet
            strEmp = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Termination_Get_Employee_Check", txtEmpID.Text)
            If strEmp.Tables(0).Rows.Count > 0 Then
                lblstatus = "You have pending exit request, have them deleted or rejected by your HR Admin before you can initiate another"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
            Else
                MultiView1.ActiveViewIndex = 0
            End If


        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub OnRowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GridVwHeaderChckbox, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub

    Protected Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btDelete.Click
        Try
            If Process.AuthenAction(Session("role"), authencode, "Delete") = False Then
                Process.loadalert(divalert, msgalert, Process.privilegemsg, "warning")
                Exit Sub
            End If
            Dim confirmValue As String = Request.Form("confirm_value")
            If confirmValue = "Yes" Then
                Dim atLeastOneRowDeleted As Boolean = False
                ' Iterate through the Products.Rows property
                For Each row As GridViewRow In GridVwHeaderChckbox.Rows
                    ' Access the CheckBox
                    Dim cb As CheckBox = row.FindControl("chkEmp")
                    If cb IsNot Nothing AndAlso cb.Checked Then
                        ' Delete row! (Well, not really...)
                        atLeastOneRowDeleted = True
                        ' First, get the ProductID for the selected row
                        Dim ID As String = _
                            Convert.ToString(GridVwHeaderChckbox.DataKeys(row.RowIndex).Value)
                        ' "Delete" the row
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Job_Grade_delete", ID)
                    End If
                Next
                LoadData()
           
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Protected Sub DrillDown(id As String)
        Try
            ' lblDate.Text = CType(sender, LinkButton).CommandArgument

            Dim strUser As New DataSet
            strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Termination_Get", id)
            txtEmpID.Text = Session("UserEmpID")
            aname.Value = Session("EmpName")
            Process.AssignRadComboValue(radForwardTo, strUser.Tables(0).Rows(0).Item("mgrid").ToString)
            anoticedate.SelectedDate = strUser.Tables(0).Rows(0).Item("mgrid").ToString
            aexitdate.SelectedDate = CDate(strUser.Tables(0).Rows(0).Item("NoticeDate").ToString)
            anoticedate.SelectedDate = CDate(strUser.Tables(0).Rows(0).Item("TerminationDate").ToString)
            Process.AssignRadComboValue(cboexittype, strUser.Tables(0).Rows(0).Item("ExitType").ToString)
            areason.Value = strUser.Tables(0).Rows(0).Item("Reason").ToString
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.message, "danger")
        End Try
    End Sub

    Protected Sub GridVwHeaderChckbox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridVwHeaderChckbox.SelectedIndexChanged
        Try
            DrillDown(GridVwHeaderChckbox.SelectedDataKey.Value)
        Catch ex As Exception

        End Try
    End Sub
End Class