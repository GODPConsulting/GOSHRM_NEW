Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports GOSHRM.GOSHRM.GOSHRM.BO
Imports System.IO


Public Class TrainingMaterialUpdate
    Inherits System.Web.UI.Page
    Dim emailFile As String = ConfigurationManager.AppSettings("FileURL")
    Dim education As New clsEducation
    Dim AuthenCode As String = "TRAINSESSION"
    Dim olddata(4) As String
    Dim options(4) As String
    Dim Separators() As Char = {","c}
    Dim answer As String = ""
    Dim lblstatus As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'SingleLineTextBox, // will render a textbox 
            'MultiLineTextBox, // will render a text area
            'YesOrNo, //will render a checkbox
            'SingleSelect, //will render a dropdownlist
            'MultiSelect //will render a listbo

            If Not Me.IsPostBack Then
                Dim sss As New DataSet
                Dim title As String = ""
                sss = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Training_Sessions_get", Request.QueryString("sessionid"))
                If sss.Tables(0).Rows.Count > 0 Then
                    title = sss.Tables(0).Rows(0).Item("name").ToString
                End If
                pagetitle.InnerText = title

                If Request.QueryString("id") IsNot Nothing Then
                    Dim strUser As New DataSet

                    strUser = SqlHelper.ExecuteDataset(WebConfig.ConnectionString, "Training_Session_Attachement_Get", Request.QueryString("id"))
                    txtid.Text = strUser.Tables(0).Rows(0).Item("id").ToString
                    txtname.Value = strUser.Tables(0).Rows(0).Item("name").ToString
                    txtDesc.Value = strUser.Tables(0).Rows(0).Item("filedescription").ToString
                    lblfiletype.Value = strUser.Tables(0).Rows(0).Item("filetype").ToString
                    lblfilename.Value = strUser.Tables(0).Rows(0).Item("filename").ToString
                    lblfilesize.Value = FormatNumber(CDbl(strUser.Tables(0).Rows(0).Item("filesize").ToString) / 1024, 0)
                    lbldatecreated.Value = strUser.Tables(0).Rows(0).Item("createdon").ToString
                    lbldateupdated.Value = strUser.Tables(0).Rows(0).Item("updatedon").ToString
                Else
                    txtid.Text = "0"
                    txtname.Focus()
                End If

            End If
            If lblfilename.Value = "" Then
                lnkDownload.Visible = False
            Else
                lnkDownload.Visible = True
            End If
        Catch ex As Exception
            Process.loadalert(divalert, msgalert, ex.Message, "warning")
        End Try
    End Sub
    Private Function GetImageIdentity(ByVal sessionid As String, ByVal fileimage As Byte(), ByVal filename As String, ByVal filetype As String, ByVal filesize As String, ByVal suser As String) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure

            If filetype.Contains("video") Or filetype.Contains("audio") Then
                cmd.CommandText = "Training_Session_Attachement_File_VA"
            Else
                cmd.CommandText = "Training_Session_Attachement_File_Flat"
            End If

            cmd.Parameters.Add("@id", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@sessionid", SqlDbType.Int).Value = sessionid

            If filetype.Contains("video") Or filetype.Contains("audio") Then
                'cmd.Parameters.Add("@fileplay", SqlDbType.Binary).Value = fileimage
            Else
                'cmd.Parameters.Add("@fileimage", SqlDbType.Image).Value = fileimage
            End If
            cmd.Parameters.Add("@filename", SqlDbType.VarChar).Value = filename
            cmd.Parameters.Add("@filetype", SqlDbType.VarChar).Value = filetype
            cmd.Parameters.Add("@filesize", SqlDbType.VarChar).Value = filesize
            cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = suser
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception
            Process.strExp = ex.Message
            Return "0"
        End Try
    End Function
    Private Function GetIdentity(ByVal sessionid As String, mname As String, filedesc As String, suser As String) As String
        Try
            Dim strConnString As String = WebConfig.ConnectionString   ' ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Training_Session_Attachement_Update"
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@sessionid", SqlDbType.Int).Value = sessionid
            cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = mname
            cmd.Parameters.Add("@filedescription", SqlDbType.VarChar).Value = filedesc
            cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = suser
            cmd.Connection = con
            con.Open()
            Dim obj As Object = cmd.ExecuteScalar()
            Return obj.ToString()
        Catch ex As Exception
            Process.strExp = ex.Message
            Return 0
        End Try
    End Function

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            If txtid.Text <> "0" Then
                If Process.AuthenAction(Session("role"), AuthenCode, "Update") = False Then
                    ClientScript.RegisterClientScriptBlock(Me.[GetType](), "alert", Convert.ToString("alert('") & "You don't have privilege to perform this action" + "')", True)
                    Exit Sub
                End If
            End If
            If txtname.Value.Trim = "" Then
                lblstatus = "training material name required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                txtname.Focus()
                Exit Sub
            End If

            If txtDesc.Value.Trim = "" Then
                lblstatus = "brief training material description required!"
                Process.loadalert(divalert, msgalert, lblstatus, "warning")
                txtDesc.Focus()
                Exit Sub
            End If


            answer = ""

            If txtid.Text.Trim = "" Then
                txtid.Text = "0"
            End If


            If txtid.Text = "0" Then
                txtid.Text = GetIdentity(Request.QueryString("sessionid"), txtname.Value.Trim, txtDesc.Value, Session("LoginID"))
                If txtid.Text = "0" Then

                    lblstatus = Process.strExp
                    Process.loadalert(divalert, msgalert, lblstatus, "warning")
                    Exit Sub
                End If
            Else
                SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Training_Session_Attachement_Update", txtid.Text, Request.QueryString("sessionid"), txtname.Value.Trim, txtDesc.Value, Session("LoginID"))
            End If

            lblstatus = "Record saved"
            Process.loadalert(divalert, msgalert, lblstatus, "success")
            Dim id As String = Request.QueryString("sessionid")
            Response.Redirect("~/Module/Trainings/Settings/TrainingMaterials.aspx?sessionid=" + id + "")
        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "warning")
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            Dim id As String = Request.QueryString("sessionid")
            If ViewState("PreviousPage") IsNot Nothing Then
                Response.Redirect("~/Module/Trainings/Settings/TrainingMaterials.aspx?sessionid=" + id + "")
            End If
            Response.Redirect("~/Module/Trainings/Settings/TrainingMaterials.aspx?sessionid=" + id + "")
        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "warning")
        End Try
    End Sub

    Protected Sub btnImage_Click(sender As Object, e As EventArgs) Handles btnImage.Click
        Try
            If imgUpload.HasFile AndAlso Not imgUpload.PostedFile Is Nothing Then

                Dim strtype As String = ""
                Dim strname As String = ""
                Dim imgdata As Byte() = Nothing
                Dim strsize As Integer = 0

                Dim img_strm As Stream = imgUpload.PostedFile.InputStream
                Dim img_len As Integer = imgUpload.PostedFile.ContentLength
                strtype = imgUpload.PostedFile.ContentType.ToString()
                strname = Path.GetFileName(imgUpload.PostedFile.FileName)
                strsize = imgUpload.PostedFile.ContentLength
                Dim imageName As String = (Server.MapPath(emailFile) + "EmployeeTrainingMaterial_" & strname)
                If File.Exists(imageName) Then
                    File.Delete(imageName)
                End If

                If strtype.Contains("video") Or strtype.Contains("audio") Then
                    Using br As New BinaryReader(imgUpload.PostedFile.InputStream)
                        imgdata = br.ReadBytes(CInt(imgUpload.PostedFile.InputStream.Length))
                    End Using
                Else
                    imgdata = New Byte(img_len - 1) {}
                    Dim n As Integer = img_strm.Read(imgdata, 0, img_len)
                End If

                Using Stream As New FileStream(imageName, FileMode.Create)
                    Stream.Write(imgdata, 0, imgdata.Length)
                End Using
                imageName = "EmployeeTrainingMaterial_" & strname
                ' Process.SaveFiles(imgUpload, strtype, strname, imgdata, strsize)

                If txtid.Text = "0" Then
                    txtid.Text = GetImageIdentity(Request.QueryString("sessionid"), Nothing, imageName, strtype, strsize, Session("LoginID"))
                    If txtid.Text = "0" Then
                        lblstatus = Process.strExp
                        Process.loadalert(divalert, msgalert, lblstatus, "warning")
                        Exit Sub
                    End If
                Else
                    If strtype.ToLower.Contains("video") Or strtype.ToLower.Contains("audio") Then
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Training_Session_Attachement_File_VA", txtid.Text, Request.QueryString("sessionid"), Nothing, imageName, strtype, strsize, Session("LoginID"))
                    Else
                        SqlHelper.ExecuteNonQuery(WebConfig.ConnectionString, "Training_Session_Attachement_File_VA", txtid.Text, Request.QueryString("sessionid"), Nothing, imageName, strtype, strsize, Session("LoginID"))
                    End If

                End If


                lblstatus = "File uploaded"
                Process.loadalert(divalert, msgalert, lblstatus, "success")
                lblfilename.Value = imageName
                lblfilesize.Value = FormatNumber(strsize / 1024, 0)
                lblfiletype.Value = strtype
            Else
                lblstatus = "No file selected for upload"
                Process.loadalert(divalert, msgalert, lblstatus, "info")
                imgUpload.Focus()
            End If

        Catch ex As Exception
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "danger")
        End Try
    End Sub

    Protected Sub lnkDownload_Click(sender As Object, e As EventArgs) Handles lnkDownload.Click
        Try
            Dim dt As DataTable = Process.SearchData("Training_Session_Attachement_Get", txtid.Text)
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
            lblstatus = ex.Message
            Process.loadalert(divalert, msgalert, lblstatus, "warning")
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
End Class