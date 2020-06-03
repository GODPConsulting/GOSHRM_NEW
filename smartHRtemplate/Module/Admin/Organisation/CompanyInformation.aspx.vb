Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports Telerik.Web.UI
Imports System.IO


Public Class CompanyInformation
    Inherits System.Web.UI.Page
    Dim geninfo As New clsGeneralInfo
    Dim AuthenCode As String = "GENINFO"
    Dim olddata(14) As String
    Dim oFileAttached1 As Byte
    Dim oFileAttached As Byte()
    Dim Pages As String = "General Information"
   
    
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                'Company_Structure_get_parent
                radMultiCompany.Items.Clear()
                radMultiCompany.Items.Add("No")
                radMultiCompany.Items.Add("Yes")

                cboLevel.Items.Clear()
                For i As Integer = 1 To 10
                    'strDataSet.Tables(0).Rows(i).Item(Index).ToString()
                    Dim item As New RadComboBoxItem()
                    item.Text = i
                    item.Value = i
                    cboLevel.Items.Add(item)
                    item.DataBind()
                Next

                Process.LoadRadDropDownTextAndValue(radCountry, "CountryTable_get", "Country", "Country", False)
                Process.LoadRadDropDownTextAndValue(radCurrency, "Currency_Load_1", "Currency", "Code", False)
                Dim strUser As New DataSet
                strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "general_info_get")
                If strUser.Tables(0).Rows.Count > 0 Then
                    companyname.Value = strUser.Tables(0).Rows(0).Item("organisationname").ToString
                    companytaxid.Value = strUser.Tables(0).Rows(0).Item("taxid").ToString
                    companyregnumber.Value = strUser.Tables(0).Rows(0).Item("regno").ToString
                    companyphone.Value = strUser.Tables(0).Rows(0).Item("phone").ToString
                    companyemail.Value = strUser.Tables(0).Rows(0).Item("email").ToString
                    companyfax.Value = strUser.Tables(0).Rows(0).Item("fax").ToString
                    companyaddr1.Value = strUser.Tables(0).Rows(0).Item("address1").ToString
                    companyaddr2.Value = strUser.Tables(0).Rows(0).Item("address2").ToString
                    companycity.Value = strUser.Tables(0).Rows(0).Item("city").ToString
                    companystate.Value = strUser.Tables(0).Rows(0).Item("state").ToString
                    companynote.Value = strUser.Tables(0).Rows(0).Item("note").ToString
                    companyzipcode.Value = strUser.Tables(0).Rows(0).Item("zipcode").ToString
                    companyemptotal.Value = strUser.Tables(0).Rows(0).Item("TotalWorkers").ToString

                    Process.AssignRadDropDownValue(radCountry, strUser.Tables(0).Rows(0).Item("country").ToString)
                    Process.AssignRadDropDownValue(radCurrency, strUser.Tables(0).Rows(0).Item("currency").ToString)
                    Process.AssignRadDropDownValue(radMultiCompany, strUser.Tables(0).Rows(0).Item("ismulticompany").ToString)

                    If IsDBNull(strUser.Tables(0).Rows(0).Item("imgLogo")) Or strUser.Tables(0).Rows(0).Item("imgLogo").ToString.Trim = "" Then
                        imgProfile.ImageUrl = imgClear.ImageUrl
                    Else
                        imgProfile.ImageUrl = "CompanyLogo.ashx"
                    End If

                    If radMultiCompany.SelectedText.ToUpper = "NO" Then
                        lblsubLevel.Visible = False
                        cboLevel.Visible = False
                    Else
                        lblsubLevel.Visible = True
                        cboLevel.Visible = True
                    End If
                    Process.AssignRadComboValue(cboLevel, strUser.Tables(0).Rows(0).Item("subsidiary_level").ToString)
                End If
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub


    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False Then
                Process.loadalert(divalert, msgalert, "You don't have privilege to perform this action", "warning")
                Exit Sub
            End If

            Dim lblstatus As String = ""
            If (companyname.Value.Trim = "") Then
                lblstatus = "Name of Organisation required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                companyname.Focus()
                Exit Sub
            End If

            If (radCountry.SelectedText.Trim = "") Or (radCountry.SelectedText.Trim = "--Select--") Then
                lblstatus = "Country required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                radCountry.Focus()
                Exit Sub
            End If

            'Old Data

            Dim NewValue As String = ""
            Dim OldValue As String = ""

            If Not imguploads.PostedFile Is Nothing Then
                Dim img_strm As Stream = imguploads.PostedFile.InputStream
                'Retrieving the length of the file to upload
                Dim img_len As Integer = imguploads.PostedFile.ContentLength
                'retrieving the type of the file to upload
                Dim strtype As String = imguploads.PostedFile.ContentType.ToString()
                Dim strname As String = Path.GetFileName(imguploads.PostedFile.FileName)
                Dim imgdata As Byte() = New Byte(img_len - 1) {}
                Dim n As Integer = img_strm.Read(imgdata, 0, img_len)
                If imgdata.Length <> 0 Then
                    SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "general_info_logo", imgdata, strtype)
                End If

            End If

            Dim j As Integer = 0

            geninfo.Address1 = companyaddr1.Value.Trim
            geninfo.Address2 = companyaddr2.Value.Trim
            geninfo.City = companycity.Value.Trim
            geninfo.Country = radCountry.SelectedValue
            geninfo.Currency = radCurrency.SelectedValue
            geninfo.Email = companyemail.Value.Trim
            geninfo.Fax = companyfax.Value.Trim
            geninfo.Note = companynote.Value
            geninfo.Organisation = companyname.Value.Trim
            geninfo.Phone = companyphone.Value.Trim
            geninfo.RegistrationNo = companyregnumber.Value.Trim
            geninfo.State = companystate.Value.Trim
            geninfo.TaxID = companytaxid.Value
            geninfo.ZipPostCode = companyzipcode.Value.Trim
            If radMultiCompany.SelectedText.ToUpper = "NO" Then
                geninfo.SubsidiaryLevel = 1
            Else
                geninfo.SubsidiaryLevel = cboLevel.SelectedValue
            End If

            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "general_info_update", geninfo.Organisation, geninfo.TaxID, geninfo.RegistrationNo, geninfo.Phone, geninfo.Email, geninfo.Fax, geninfo.ZipPostCode, geninfo.Address1, geninfo.Address2, geninfo.City, geninfo.State, geninfo.Country, geninfo.Note, geninfo.Currency, radMultiCompany.SelectedText, geninfo.SubsidiaryLevel)

            'Process.GetAuditTrailInsertandUpdate("", generalinfo, "Updated " + geninfo.Organisation, Pages)
            Process.loadalert(divalert, msgalert, "Record saved", "success")
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnImage_Click(sender As Object, e As EventArgs)
        Try

            If Not imguploads.PostedFile Is Nothing Then
                Dim img_strm As Stream = imguploads.PostedFile.InputStream
                'Retrieving the length of the file to upload
                Dim img_len As Integer = imguploads.PostedFile.ContentLength
                'retrieving the type of the file to upload
                Dim strtype As String = imguploads.PostedFile.ContentType.ToString()
                Dim strname As String = Path.GetFileName(imguploads.PostedFile.FileName)
                Dim imgdata As Byte() = New Byte(img_len - 1) {}
                Dim n As Integer = img_strm.Read(imgdata, 0, img_len)


                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "general_info_logo", imgdata, strtype)
            End If
           

            imgProfile.ImageUrl = "CompanyLogo.ashx"

            'lblstatus.Text = "Logo uploaded"
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "danger")
        End Try
    End Sub

    Protected Sub btnImage0_Click(sender As Object, e As EventArgs) 'Handles btnImage0.Click
        Try
            imgProfile.ImageUrl = imgClear.ImageUrl
            SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "general_info_logo_clear")
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub radMultiCompany_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.DropDownListEventArgs) Handles radMultiCompany.SelectedIndexChanged
        Try
            If radMultiCompany.SelectedText.ToUpper = "NO" Then
                lblsubLevel.Visible = False
                cboLevel.Visible = False
            Else
                lblsubLevel.Visible = True
                cboLevel.Visible = True
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class