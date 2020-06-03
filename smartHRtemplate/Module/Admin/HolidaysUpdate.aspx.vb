Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO


Public Class HolidaysUpdate
    Inherits System.Web.UI.Page
    Dim holiday As New clsHolidays
    Dim AuthenCode As String = "HOLIDAY"
    Dim olddata(6) As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                Process.LoadRadDropDownTextAndValue(radCountry, "CountryTable_get_all", "country", "country", False)

                radStatus.Items.Clear()
                radStatus.Items.Add("Full Day")
                radStatus.Items.Add("Half Day")

                'Holidays_get_parent
                If Request.QueryString("id") IsNot Nothing Then
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Holidays_get", Request.QueryString("id"))
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    aname.Value = strUser.Tables(0).Rows(0).Item("Name").ToString
                    radDate.SelectedDate = strUser.Tables(0).Rows(0).Item("Date").ToString
                    radStatus.SelectedText = strUser.Tables(0).Rows(0).Item("Status").ToString
                    Process.AssignRadDropDownValue(radCountry, strUser.Tables(0).Rows(0).Item("Country").ToString)
                Else
                    txtid.Text = "0"
                End If

            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            If txtid.Text <> "0" Then
                If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False Then
                    Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "warning")
                    Exit Sub
                End If
            End If
            
            Dim lblstatus As String = ""
            If (aname.Value.Trim = "") Then
                lblstatus = "Name required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                aname.Focus()
                Exit Sub
            End If

            If (radCountry.SelectedText.Trim = "" Or radCountry.SelectedText.Trim = "-- Select --") Then
                lblstatus = "Country required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                radCountry.Focus()
                Exit Sub
            End If

            If (radStatus.SelectedText.Trim = "" Or radStatus.SelectedText.Trim = "-- Select --") Then
                lblstatus = "Status required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                radStatus.Focus()
                Exit Sub
            End If

            If (radDate.SelectedDate Is Nothing) Then
                lblstatus = "Date required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                radDate.Focus()
                Exit Sub
            End If

            If (IsDate(radDate.SelectedDate) = False) Then
                lblstatus = "Date format invalid!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                radDate.Focus()
                Exit Sub
            End If


            'Old Data
            If txtid.Text <> "0" Then
                Dim strUser As New DataSet
                strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Holidays_get", txtid.Text)
                olddata(0) = strUser.Tables(0).Rows(0).Item("id").ToString
                olddata(1) = strUser.Tables(0).Rows(0).Item("Name").ToString
                olddata(2) = strUser.Tables(0).Rows(0).Item("leaveperiod").ToString
                olddata(3) = strUser.Tables(0).Rows(0).Item("Date").ToString
                olddata(4) = strUser.Tables(0).Rows(0).Item("Status").ToString
                olddata(5) = strUser.Tables(0).Rows(0).Item("Country").ToString
            End If

            If txtid.Text.Trim = "" Then
                holiday.id = 0
            Else
                holiday.id = txtid.Text
            End If
            holiday.Country = radCountry.SelectedText
            holiday.HDate = radDate.SelectedDate
            holiday.Name = aname.Value.Trim
            holiday.Period = radDate.SelectedDate.Value.Year
            holiday.Status = radStatus.SelectedText

            Dim OldValue As String = ""
            Dim NewValue As String = ""

            Dim j As Integer = 0

            If txtid.Text <> "0" Then 'Updates
                For Each a In GetType(clsHolidays).GetProperties()
                    If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If IsNumeric(a.GetValue(holiday, Nothing)) = True And IsNumeric(olddata(j)) = True Then
                                If CDbl(a.GetValue(holiday, Nothing)) <> CDbl(olddata(j)) Then
                                    NewValue += a.Name + ": " + a.GetValue(holiday, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            Else
                                If a.GetValue(holiday, Nothing).ToString <> olddata(j).ToString Then
                                    NewValue += a.Name + ": " + a.GetValue(holiday, Nothing).ToString & vbCrLf
                                    OldValue += a.Name + ": " + olddata(j).ToString & vbCrLf
                                End If
                            End If
                        End If
                    End If
                    j = j + 1
                Next
            Else
                For Each a In GetType(clsHolidays).GetProperties() 'New Entries
                    If a.Name.ToLower <> "id" And a.Name.ToLower <> "password" Then
                        If a.PropertyType.IsValueType = True Or a.PropertyType.Equals(GetType(String)) Then
                            If a.GetValue(holiday, Nothing) = Nothing Then
                                NewValue += a.Name + ":" + " " & vbCrLf
                            Else
                                NewValue += a.Name + ": " + a.GetValue(holiday, Nothing).ToString & vbCrLf
                            End If
                        End If
                    End If
                Next
            End If

            If txtid.Text = "0" Then
                txtid.Text = GetIdentity(holiday.Period, holiday.Name, holiday.HDate, holiday.Status, holiday.Country)
                If txtid.Text = "0" Then
                    Exit Sub
                End If
            Else
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Holidays_update", holiday.id, holiday.Period, holiday.Name, holiday.HDate, holiday.Status, holiday.Country)
            End If


            If NewValue.Trim = "" And OldValue.Trim = "" Then
            Else
                If olddata(0) IsNot Nothing Then
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Updated " & holiday.Name, "Holiday")
                Else
                    Dim saveAudit As Boolean = Process.GetAuditTrailInsertandUpdate(OldValue, NewValue, "Inserted", "Holiday")
                End If

            End If
            lblstatus = "Record saved"
            Process.loadalert(divalert, msgalert, lblstatus, "success")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub
    Private Function GetIdentity(period As String, sname As String, sdate As Date, sstatus As String, country As String) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Holidays_update"
            cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = 0
            cmd.Parameters.Add("@period", SqlDbType.VarChar).Value = period
            cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = sname
            cmd.Parameters.Add("@date", SqlDbType.Date).Value = sdate
            cmd.Parameters.Add("@status", SqlDbType.VarChar).Value = sstatus
            cmd.Parameters.Add("@country", SqlDbType.VarChar).Value = country
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
            Return 0
        End Try
    End Function
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect("~/Module/Admin/holidays.aspx", True)
        Catch ex As Exception

        End Try
    End Sub

    'Protected Sub radStructure_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.DropDownListEventArgs) Handles radPeriod.SelectedIndexChanged
    '    Try
    '        Process.LoadRadComboP1(radStatus, "Holidays_get_parent", radPeriod.SelectedText, 0)
    '    Catch ex As Exception
    '        Process.loadalert(divalert, msgalert, ex.Message, "danger")
    '    End Try
    'End Sub
End Class