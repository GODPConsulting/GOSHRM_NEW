Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Telerik.Web.UI

Public Class PensionSetup
    Inherits System.Web.UI.Page
    Dim payrolloption As New clsPayrollOption
    Dim AuthenCode As String = "EMPRPENSION"
    Dim olddata(5) As String
    Dim oFileAttached1 As Byte
    Dim oFileAttached As Byte()

    Private Sub Lodadata()
        Process.LoadRadComboTextAndValue(cboMakeup, "Finance_Monthly_Earning_Items_Get_All", "PAYSLIP ITEM", "PAYSLIP ITEM", False)

        Dim strUser As New DataSet
        strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Finance_Pension_Setup_Get")
        If strUser.Tables(0).Rows.Count > 0 Then
            Process.LoadListAndComboxFromDataset(lstApprover, cboMakeup, "Finance_Pension_Items_Get_All", "item", "item", "23")
            chkApply.Checked = CBool(strUser.Tables(0).Rows(0).Item("chkapply"))
            txtPensionDesc.Value = strUser.Tables(0).Rows(0).Item("Pensiondesc").ToString
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                Lodadata()
                
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub



    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "danger")
                Exit Sub
            End If

            If (txtPensionDesc.Value.Trim Is Nothing) Then
                Process.loadalert(divalert, msgalert, "Payslip description required!", "danger")
                txtPensionDesc.Focus()
                Exit Sub
            End If

            Dim NewValue As String = ""
            Dim OldValue As String = ""


            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Finance_Pension_Setup_Update", txtPensionDesc.Value.Trim, chkApply.Checked)
            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Finance_Pension_Items_delete")

            If cboMakeup.CheckedItems.Count > 0 Then
                Dim collection As IList(Of RadComboBoxItem) = cboMakeup.CheckedItems
                If (collection.Count <> 0) Then
                    For i As Integer = 0 To collection.Count - 1
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Finance_Pension_Items_Update", collection(i).Value)
                    Next

                    'For Each item As RadComboBoxItem In collection
                    '    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Finance_Pension_Items_Update", item.Value)
                    'Next
                End If
            End If
            Lodadata()
            Process.loadalert(divalert, msgalert, "Record saved", "success")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    'Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
    '    Process.LoadListBoxFromCombo(lstApprover, cboMakeup)
    'End Sub

    
    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Process.LoadListBoxFromCombo(lstApprover, cboMakeup)
    End Sub
End Class