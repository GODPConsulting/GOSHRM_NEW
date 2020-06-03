Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Public Class EmergencyContacts
    Inherits System.Web.UI.Page
    Dim olddata(5) As String
    Dim AuthenCode As String = "EMPLIST"
    Dim EmergencyDetail As New clsEmpEmergency



    Private Sub LoadEmergencyContact(ByVal EmpID As String)
        Try
            Dim strEmergency As New DataSet
            strEmergency = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Emp_Emergency_Contact_get", EmpID)
            'Emergency Contacts
            txtEmpID.Text = EmpID
            If strEmergency.Tables(0).Rows.Count > 0 Then
                txtid.Text = strEmergency.Tables(0).Rows(0).Item("id").ToString
                txtEmpID.Text = strEmergency.Tables(0).Rows(0).Item("empid").ToString
                aemername1.Value = strEmergency.Tables(0).Rows(0).Item("Name1").ToString
                aemeraddress1.Value = strEmergency.Tables(0).Rows(0).Item("Address1").ToString
                aemercontactnumber1.Value = strEmergency.Tables(0).Rows(0).Item("Phone1").ToString

                Process.AssignHTMLSelectValue(drprelation1, strEmergency.Tables(0).Rows(0).Item("Relationship1").ToString)

                aemername2.Value = strEmergency.Tables(0).Rows(0).Item("Name2").ToString
                aemeraddress2.Value = strEmergency.Tables(0).Rows(0).Item("Address2").ToString
                aemercontactnumber2.Value = strEmergency.Tables(0).Rows(0).Item("Phone2").ToString
                Process.AssignHTMLSelectValue(drprelation2, strEmergency.Tables(0).Rows(0).Item("Relationship2").ToString)



            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, "Emergency Contact: " & ex.Message, "danger")
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                Process.LoadHTMLSelectTextAndValue(drprelation1, "emp_relationship_get_all", "name", "name", True)
                Process.LoadHTMLSelectTextAndValue(drprelation2, "emp_relationship_get_all", "name", "name", True)


                If Request.QueryString("id") IsNot Nothing Then
                    LoadEmergencyContact(Request.QueryString("id"))
                End If
            End If

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            Dim lblstatus As String = ""


            If (aemername1.Value.Trim = "") Then
                lblstatus = "emergency contact name required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                aemername1.Focus()
                Exit Sub
            End If

            If aemeraddress1.Value.Trim = "" Then
                lblstatus = "emergency contact address required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                aemeraddress1.Focus()
                Exit Sub
            End If

            If aemercontactnumber1.Value.Trim = "" Then
                lblstatus = "emergency contact number required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                aemercontactnumber1.Focus()
                Exit Sub
            End If

            If drprelation1.Value.Trim = "" Then
                lblstatus = "relationship with employee required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                drprelation1.Focus()
                Exit Sub
            End If

            If aemeraddress2.Value.Trim <> "" Then
                If aemercontactnumber2.Value.Trim = "" Then
                    lblstatus = "Emergency Contact 2 phone number required!"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    aemercontactnumber2.Focus()
                    Exit Sub
                End If

                If aemeraddress2.Value.Trim Is Nothing Then
                    lblstatus = "Contact 2 Address required!"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    aemeraddress2.Focus()
                    Exit Sub
                End If

                If drprelation2.Value.Trim = "" Then
                    lblstatus = "contact 2 relationship required!"
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    drprelation2.Focus()
                    Exit Sub
                End If

            End If

            If aemeraddress2.Value Is Nothing Then
                aemeraddress2.Value = ""
            End If

            If aemeraddress1.Value Is Nothing Then
                aemeraddress1.Value = ""
            End If

            If aemercontactnumber2.Value Is Nothing Then
                aemercontactnumber2.Value = ""
            End If

            EmergencyDetail.EmpID = txtEmpID.Text
            EmergencyDetail.Name1 = aemername1.Value
            EmergencyDetail.Address1 = aemeraddress1.Value
            EmergencyDetail.Phone1 = aemercontactnumber1.Value
            EmergencyDetail.RelationShip1 = drprelation1.Value
            EmergencyDetail.Name2 = aemeraddress1.Value
            EmergencyDetail.Address2 = aemeraddress2.Value
            EmergencyDetail.Phone2 = aemercontactnumber2.Value
            EmergencyDetail.RelationShip2 = drprelation2.Value


            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Emp_Emergency_Contact_Changes_Update", EmergencyDetail.ID, EmergencyDetail.EmpID, EmergencyDetail.Name1, EmergencyDetail.Address1,
                                      EmergencyDetail.Phone1, EmergencyDetail.RelationShip1, EmergencyDetail.Name2, EmergencyDetail.Address2,
                                      EmergencyDetail.Phone2, EmergencyDetail.RelationShip2)

            Process.Mail_HR(Process.GetMailList("hr"), Process.GetEmployeeData(txtEmpID.Text, "fullname"), "Emergency Contact Info update", "", txtEmpID.Text, "", Process.MailSuccessionPlan, Process.ApplicationURL + "/Module/Employee/Employeedata?id=" + txtEmpID.Text)


            lblstatus = "Update successfully sent to HR for approval"
            Process.loadalert(divalert, msgalert, lblstatus, "success")

        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect("EmployeeProfile?empid=" & txtEmpID.Text, True)
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

End Class