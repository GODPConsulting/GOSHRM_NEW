<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="TrainingMaterialUpdate.aspx.vb"
    Inherits="GOSHRM.TrainingMaterialUpdate" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>Add New</title>
    <script type="text/javascript">

        function closeWin() {
            popup.close();   // Closes the new window
        }
   

    </script>
    <style type="text/css">
        .style1
        {
            color: #FFFFFF;
            font-family: Candara;
            font-weight: bold;
        }
        .lbl
        {
            font-family: Candara;
            font-size: medium;
        }
        .style5
        {
            font-family: Candara;
            font-size: medium;
            width: 176px;
        }
        .style6
        {
            width: 176px;
        }
        .RadDropDownList { display:inline-block !important; 
                                               width: 443px !important; }
                            .RadDropDownList_Default{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.RadDropDownList{width:160px;line-height:1.3333em;text-align:left;display:inline-block;vertical-align:middle;white-space:nowrap;cursor:default;*zoom:1;*display:inline}.RadDropDownList_Default .rddlInner{border-radius:3px;border-color:#aaa;color:#333;background-color:#c1c1c1;background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radGradientButtonSprite.png');background-image:linear-gradient(#fafafa,#e6e6e6)}.RadDropDownList .rddlInner{vertical-align:top}.RadDropDownList .rddlInner{padding:2px 22px 2px 5px;border-width:1px;border-style:solid;display:block;position:relative;overflow:hidden}.RadDropDownList_Default .rddlDefaultMessage{color:#a5a5a5}.RadDropDownList .rddlDefaultMessage{font-style:italic}.RadDropDownList .rddlFakeInput{margin:0;padding:0;width:100%;min-height:16px;display:block;overflow:hidden}
                            .rddlFakeInput {
                                    height: 16px !important; 
                                    width: 80% !important;}.RadDropDownList_Default .rddlIcon{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radActionsSprite.png');background-position:-22px -20px}.RadDropDownList .rddlIcon{width:16px;height:100%;border:0;background-repeat:no-repeat;position:absolute;top:0;right:0}.rddlSlide{float:left;display:none;position:absolute;overflow:hidden;z-index:7000}.rddlPopup_Default{border-color:#a0a0a0;color:#333;background-color:#fff;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.rddlPopup{*zoom:1;border-width:1px;border-style:solid;text-align:left;position:relative;cursor:default;width:160px;*width:158px;box-sizing:border-box}
        .style8
        {
            width: 561px;
        }
        </style>
</head>
<%-- <script type="text/javascript" language="javascript">
    function LoadList()
    {        
        var ds=null;
        ds = <%=listFilter %>;
            $( "#txtName" ).autocomplete({
              source: ds
            });
    }
    </script>--%>

 

<body>
    <form>
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
        rel="Stylesheet" type="text/css" />

<%--    <table>
        <tr>

            <div class="row">
                                <div class="col-xs-8">
                                    <h5 id="pagetitle" runat="server" class="page-title" style="color:#1BA691">
                                       Training Material</h5>
                                </div>
                            </div>
        </tr>
        <tr>
            <td class="style6">
                <asp:TextBox ID="txtid" runat="server" Visible="False" Height="16px" 
                    Width="5px"></asp:TextBox>
            </td>
            <td class="style8">
                <asp:Label ID="lblHeader" runat="server" Font-Names="Candara" 
                    Font-Size="Medium" Font-Bold="True" ForeColor="#CC9900"></asp:Label>
            </td>
        </tr>        
       
        <tr>
            <td class="style6">
                <asp:Label ID="Label4" runat="server" Font-Names="Verdana" Font-Size="12px" 
                    Text="Material Name" Font-Bold="True" ForeColor="#666666"></asp:Label>
            </td>
            <td class="style8">
                <asp:TextBox ID="txtname" runat="server" BorderColor="#CCCCCC" 
                    BorderWidth="1px" Font-Names="Verdana" Font-Size="12px"
                    Width="500px" TabIndex="2" ForeColor="#666666"></asp:TextBox>
            </td>
        </tr>
         <tr>
            <td class="style6" valign="top">
                <asp:Label ID="Label1" runat="server" Font-Names="Verdana" Font-Size="12px" 
                    Text="Description" Font-Bold="True" ForeColor="#666666"></asp:Label>
            </td>
            <td class="style8">
                <asp:TextBox ID="txtDesc" runat="server"  Width="500px" TextMode="MultiLine" 
                    Font-Names="Verdana" Font-Size="12px"
                    BorderColor="#CCCCCC" BorderWidth="1px" TabIndex="1" Height="76px" 
                    ForeColor="#666666"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style5" valign="top">
                <asp:Label ID="opt1" runat="server" Font-Names="Verdana" Font-Size="12px" 
                    Text="File Name" Font-Bold="True" ForeColor="#666666"></asp:Label>
            </td>
            <td class="style8">
                <asp:Label ID="lblfilename" runat="server" Font-Names="Verdana" 
                    Font-Size="12px" ForeColor="#666666"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style6">
                <asp:Label ID="opt2" runat="server" Font-Names="Verdana" Font-Size="12px" 
                    Text="File Type" Font-Bold="True" ForeColor="#666666"></asp:Label>
            </td>
            <td class="style8">
                <asp:Label ID="lblfiletype" runat="server" Font-Names="Verdana" 
                    Font-Size="12px" ForeColor="#666666"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style5" valign="top">
                <asp:Label ID="opt3" runat="server" Font-Names="Verdana" Font-Size="12px" 
                    Text="File Size (KB)" Font-Bold="True" ForeColor="#666666"></asp:Label>
            </td>
            <td class="style8">
                <asp:Label ID="lblfilesize" runat="server" Font-Names="Verdana" 
                    Font-Size="12px" ForeColor="#666666"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style5" valign="top">
                </td>
            <td class="style8">
                <asp:LinkButton ID="lnkDownload" runat="server" Font-Names="Verdana" 
                    Font-Size="11px">Download File</asp:LinkButton>
                </td>
        </tr>
        <tr>
            <td class="style6" valign="top">
                &nbsp;</td>
            <td class="style8">
                    <asp:FileUpload ID="imgUpload" runat="server" ToolTip="Browse Photo" Font-Names="Verdana" Font-Size="12px"/>
                    <br />
                    <asp:Button ID="btnImage" runat="server" Text="Upload File" BackColor="#1BA691"
                        ForeColor="White" Width="20%" Height="20px" BorderStyle="None" 
                        Font-Names="Verdana" Font-Size="12px"
                        Style="margin-top: 0px" />
                    
            </td>
        </tr>
        <tr>
            <td class="style5" valign="top">
                
            </td>
            <td class="style8">
               
            </td>
        </tr>
        <tr>
            <td class="style5" valign="top">
                <asp:Label ID="Label2" runat="server" Font-Names="Verdana" Font-Size="12px" 
                    Text="Date Created" Font-Bold="True" ForeColor="#666666"></asp:Label>
            </td>
            <td class="style8">
                <asp:Label ID="lbldatecreated" runat="server" Font-Names="Verdana" 
                    Font-Size="12px" ForeColor="#666666"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style5" valign="top">
                <asp:Label ID="Label6" runat="server" Font-Names="Verdana" Font-Size="12px" 
                    Text="Date Updated" Font-Bold="True" ForeColor="#666666"></asp:Label>
            </td>
            <td class="style8">
                <asp:Label ID="lbldateupdated" runat="server" Font-Names="Verdana" 
                    Font-Size="12px" ForeColor="#666666"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style6">
                <asp:Label ID="lblimage" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="12px"
                    ForeColor="#FF3300" Visible="False"></asp:Label>
                <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="12px"
                    ForeColor="#FF3300"></asp:Label>
            </td>
            <td class="style8">
                <asp:Label ID="lblstatus" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="12px"
                    ForeColor="#FF3300"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style6">
                <asp:Button ID="btnAdd" runat="server" Text="Save" BackColor="#1BA691" ForeColor="White" Font-Names="Verdana" Font-Size="12px"
                    Width="120px" Height="20px" BorderStyle="None" />
            </td>
            <td class="style8">
                <asp:Button ID="btnCancel" runat="server" Text="Back" BackColor="#999966" ForeColor="White" Font-Names="Verdana" Font-Size="12px"
                    Width="120px" Height="20px" BorderStyle="None" />
            </td>
        </tr>
    </table>--%>
    <div class="container col-md-10">
            <div class="row">
                <div id="divalert" runat="server" visible="false" class="alert alert-info">
                    <strong id="msgalert" runat="server"></strong>
                    <asp:TextBox ID="txtid" runat="server" Width="3px"  
                    Font-Names="Candara" Height="2px" Visible="False"></asp:TextBox>
                    <asp:Label ID="lblimage" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="12px"
                    ForeColor="#FF3300" Visible="False"></asp:Label>
                <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="12px"
                    ForeColor="#FF3300"></asp:Label>
                </div>
                </div>
                   <div class="panel panel-success">
                        <div class="panel-heading">
                            <h5><b id="pagetitle" runat="server">TRAINING MATERIAL</b></h5>
                        </div>
                     <div class="panel-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>MATERIAL NAME</label>
                                <input id="txtname" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>DESCRIPTION*</label>
                                <textarea id="txtDesc" runat="server" class="form-control" rows="5"></textarea>
                            </div>
                        </div>
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>FILE NAME</label>
                                <input id="lblfilename" runat="server" readonly="" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>
                                    FILE TYPE</label>
                                <input id="lblfiletype" runat="server" readonly="" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class="col-md-12">
                        <div class="col-md-3 col-sm-3 col-xs-3">
                            <asp:LinkButton ID="lnkDownload" Width="100%" CssClass="btn btn-primary btn-success" runat="server" Font-Names="Verdana" 
                                Font-Size="11px">Download File</asp:LinkButton>
                                </div>
                             <div class="col-md-3 col-sm-3 col-xs-3">
                                     <asp:FileUpload ID="imgUpload" runat="server" ToolTip="Browse Photo" Font-Names="Verdana" Font-Size="12px"/>
                            </div>
                            <div class="col-md-2 col-sm-6 col-xs-3">
                                <asp:Button ID="btnImage" runat="server" Text="Upload File"
                                    ForeColor="White" CssClass="btn btn-primary btn-success" Width="100%" BorderStyle="None" 
                                    Font-Names="Verdana" Font-Size="12px"
                                    Style="margin-top: 0px" />
                                </div>
                        </div>
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>FILE SIZE(KB)</label>
                                <input id="lblfilesize" runat="server" readonly="" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>
                                    DATE CREATED</label>
                                <input id="lbldatecreated" runat="server" readonly="" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>
                                    DATE UPDATED</label>
                                <input id="lbldateupdated" runat="server" readonly="" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class="col-md-12 m-t-20 text-center">
                            <button id="btnAdd" runat="server" onserverclick="btnAdd_Click" type="submit"
                                style="width: 150px" class="btn btn-primary btn-success">
                                Save &amp; Update</button>
                            <button id="Button1" runat="server" onserverclick="btnCancel_Click" type="submit" style="width: 150px"
                                class="btn btn-primary btn-danger">
                                << Back</button>
                        </div>
                    </div>
                </div>
            </div>
            </div>
    </form>
</body>
</html>
</asp:Content>