Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Telerik.Web.UI


Public Class GratuityGradeUpdate
    Inherits System.Web.UI.Page
    Dim monthlyearning As New clsMonthlyStructure
    Dim AuthenCode As String = "GRAUTITYSETUP"
    Dim olddata(7) As String
    Dim lblstatus As String = ""
   
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not Me.IsPostBack Then
                'Process.LoadListBox(lstMakeup, "Finance_Monthly_Earning_Items_Get_All", 1)

                Process.LoadRadComboTextAndValue(radJobGrade, "Job_Grade_get_all", "name", "name", False)

                Process.LoadRadComboTextAndValue(radComponents, "Finance_Monthly_Earning_Items_Get_All", "payslip item", "payslip item")
                'Company_Structure_get_parent
                If Request.QueryString("id") IsNot Nothing Then
                    Dim strUser As New DataSet
                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Finance_Gratuity_Grade_Get", Request.QueryString("id"))
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    Process.AssignRadComboValue(radJobGrade, strUser.Tables(0).Rows(0).Item("grade").ToString)
           
                    Process.LoadListAndComboxFromDataset(lstMakeup, radComponents, "Finance_Gratuity_Grade_Item_Get", "makeup", "makeup", txtid.Text)
                Else
                    txtid.Text = "0"

                End If

            End If
        Catch ex As Exception
            lblstatus = ex.Message
        End Try
    End Sub
    Private Function GetIdentity() As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Finance_Gratuity_Grade_Update"
            cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = txtid.Text
            cmd.Parameters.Add("@grade", SqlDbType.VarChar).Value = radjobgrade.SelectedValue          
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
            lblstatus = ""
            If txtid.Text <> "0" Then
                If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False Then
                    Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "danger")
                    Exit Sub
                End If
            End If



            'Old Data

            If txtid.Text = "0" Then
                txtid.Text = GetIdentity()
                If txtid.Text = "0" Then
                    Exit Sub
                End If
            Else
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Finance_Gratuity_Grade_Update", txtid.Text, radJobGrade.SelectedValue)
            End If



            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Finance_Gratuity_Grade_Item_Delete", txtid.Text)

            For d As Integer = 0 To lstMakeup.Items.Count - 1
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Finance_Gratuity_Grade_Item_Update", txtid.Text, lstMakeup.Items(d).Text)
            Next
            Process.LoadListAndComboxFromDataset(lstMakeup, radComponents, "Finance_Gratuity_Grade_Item_Get", "makeup", "makeup", txtid.Text)
            lblstatus = "Record saved"
            Process.loadalert(divalert, msgalert, lblstatus, "success")
            'Response.Write("<script language='javascript'> { self.close() }</script>")
        Catch ex As Exception
            lblstatus = ex.Message

        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect("gratuitygradesetup")
        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "danger")
        End Try
    End Sub


    Private Sub ReloadComponentList()
        Try
            lstMakeup.Items.Clear()
            Dim collection As IList(Of RadComboBoxItem) = radComponents.CheckedItems
            If (collection.Count > 0) Then
                For Each item As RadComboBoxItem In collection
                    lstMakeup.Items.Add(item.Text)

                Next
            Else
                lstMakeup.Items.Clear()
            End If
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub radComponents_ItemChecked(sender As Object, e As Telerik.Web.UI.RadComboBoxItemEventArgs) Handles radComponents.ItemChecked
        ReloadComponentList()
    End Sub
End Class