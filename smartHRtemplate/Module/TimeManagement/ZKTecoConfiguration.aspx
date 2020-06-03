
<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="ZKTecoConfiguration.aspx.vb"
    Inherits="GOSHRM.ZKTecoConfiguration" EnableEventValidation="false" Debug="true" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI.Gantt" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>ZKTeco Attendance Configuration</title>
    <link rel="icon" type="image/png" href="../../../images/goshrm.png">
    <script type="text/javascript">

        function closeWin() {
            popup.close();   // Closes the new window
        }
   

    </script>
    <style type="text/css">
        .style1
        {
            color: #FDFDFD;
            font-family: Candara;
        }
        .lbl
        {
            font-family: Candara;
            font-size: medium;
        }
        .style2
        {
            font-family: Candara;
            font-size: small;
            width: 186px;
            color: #FF0000;
        }
        .style5
        {
            font-family: Candara;
            font-size: medium;
            width: 186px;
        }
        .style6
        {
            width: 186px;
        }
        .RadDropDownList { display:inline-block !important; 
                                               width: 443px !important; }
                            .RadDropDownList_Default{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.RadDropDownList{width:160px;line-height:1.3333em;text-align:left;display:inline-block;vertical-align:middle;white-space:nowrap;cursor:default;*zoom:1;*display:inline}.RadDropDownList_Default .rddlInner{border-radius:3px;border-color:#aaa;color:#333;background-color:#c1c1c1;background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radGradientButtonSprite.png');background-image:linear-gradient(#fafafa,#e6e6e6)}.RadDropDownList .rddlInner{vertical-align:top}.RadDropDownList .rddlInner{padding:2px 22px 2px 5px;border-width:1px;border-style:solid;display:block;position:relative;overflow:hidden}.RadDropDownList_Default .rddlDefaultMessage{color:#a5a5a5}.RadDropDownList .rddlDefaultMessage{font-style:italic}.RadDropDownList .rddlFakeInput{margin:0;padding:0;width:100%;min-height:16px;display:block;overflow:hidden}
                            .rddlFakeInput {
                                    height: 16px !important; 
                                    width: 80% !important;}.RadDropDownList_Default .rddlIcon{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radActionsSprite.png');background-position:-22px -20px}.RadDropDownList .rddlIcon{width:16px;height:100%;border:0;background-repeat:no-repeat;position:absolute;top:0;right:0}.rddlSlide{float:left;display:none;position:absolute;overflow:hidden;z-index:7000}.rddlPopup_Default{border-color:#a0a0a0;color:#333;background-color:#fff;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.rddlPopup{*zoom:1;border-width:1px;border-style:solid;text-align:left;position:relative;cursor:default;width:160px;*width:158px;box-sizing:border-box}
        .style7
        {
            width: 502px;
        }
        </style>

     
</head>



<body>
    <form>
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>

    
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
        rel="Stylesheet" type="text/css" />
    <table>
        <tr>
            <td class="style1" colspan="2" style="background-color: #1BA691" bgcolor="#0066FF">
                ZKTeco Attendance Configuration
            </td>
        </tr>
        <tr>
            <td class="style6">
            </td>
            <td class="style7">
                            <asp:Label ID="Label14" runat="server" Font-Size="11px" 
                                Text="Network Location sample e.g. \\serverName\foldername\att2000.mdb" 
                                Font-Names="Verdana" ForeColor="#666666"></asp:Label>
                                <br />
                                <asp:Label ID="Label1" runat="server" Font-Size="11px" 
                                Text="Local Location sample e.g. C:\foldername\att2000.mdb" 
                                Font-Names="Verdana" ForeColor="#666666"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style5" valign="top">
                <asp:Label ID="Label3" runat="server" Font-Names="Verdana" Text="Data Source" 
                    Font-Bold="True" Font-Size="12px" ForeColor="#666666"></asp:Label>
                <asp:Label ID="Label9" runat="server" ForeColor="#FF3300" Text=" *"></asp:Label>
            </td>
            <td class="style7">
                <asp:TextBox ID="txtdatasource" runat="server" Font-Names="Verdana" Font-Size="12px"
                    Width="100%" TextMode="MultiLine" 
                    BorderColor="#CCCCCC" BorderWidth="1px" 
                    Height="69px" ForeColor="#666666"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style6">
                <asp:Label ID="lblHR" runat="server" Font-Names="Verdana" Text="User ID" 
                    Font-Italic="False" Font-Bold="True" Font-Size="12px" ForeColor="#666666"></asp:Label>
            </td>
            <td class="style7">
                <asp:TextBox ID="txtuserid" runat="server" Width="100%"  Font-Names="Verdana"
                    Font-Size="12px" 
                    BorderColor="#CCCCCC" BorderWidth="1px" ForeColor="#666666">Admin</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style6" valign="top">
                <asp:Label ID="Label13" runat="server" Font-Names="Verdana" Text="Password" 
                    Font-Bold="True" Font-Size="12px" ForeColor="#666666"></asp:Label>
            </td>
            <td class="style7">
                <asp:TextBox ID="txtpwd" runat="server" Width="100%" Font-Names="Verdana"
                    Font-Size="12px" 
                    BorderColor="#CCCCCC" BorderWidth="1px" ForeColor="#666666"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style6" valign="top">
                <asp:Label ID="Label2" runat="server" Font-Names="Verdana" Text="Check-In Indicator" 
                    Font-Bold="True" Font-Size="12px" ForeColor="#666666"></asp:Label>
            </td>
            <td class="style7">
                <asp:TextBox ID="txtin" runat="server" Width="100px" Font-Names="Verdana"
                    Font-Size="12px" 
                    BorderColor="#CCCCCC" BorderWidth="1px" ForeColor="#666666" 
                    ToolTip="check-in description/ indicator from biometric device"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style6" valign="top">
                <asp:Label ID="Label4" runat="server" Font-Names="Verdana" Text="Check-Out Indicator" 
                    Font-Bold="True" Font-Size="12px" ForeColor="#666666"></asp:Label>
            </td>
            <td class="style7">
                <asp:TextBox ID="txtout" runat="server" Width="100px" Font-Names="Verdana"
                    Font-Size="12px" 
                    BorderColor="#CCCCCC" BorderWidth="1px" ForeColor="#666666" 
                    ToolTip="check-in description/ indicator from biometric device"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style2" style="border-bottom-style: solid; border-bottom-width: thin;
                border-bottom-color: #C0C0C0">
                * Required Field
            </td>
            <td style="border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #C0C0C0"
                class="style7">
            </td>
        </tr>
        <tr>
            <td class="style6">
            </td>
            <td class="style7">
                <asp:Label ID="lblstatus" runat="server" Font-Bold="True" Font-Names="Candara" Font-Size="12px"
                    ForeColor="#FF3300"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style6">
                <asp:Button ID="btnsave" runat="server" Text="Save" BackColor="#1BA691" ForeColor="White"
                    Width="100px" Height="20px" BorderStyle="None" Font-Names="Verdana" 
                    Font-Size="11px" />
            </td>
            <td class="style7">
                <asp:Button ID="btnCancel" runat="server" Text="Back" BackColor="#FF3300" ForeColor="White"
                    Width="100px" Height="20px" BorderStyle="None" Font-Names="Verdana" 
                    Font-Size="11px" />

                &nbsp;&nbsp;

                <asp:Button ID="Button1" runat="server" Text="Test Connection" 
                    BackColor="#1BA691" ForeColor="White"
                    Width="120px" Height="20px" BorderStyle="None" Font-Names="Verdana" 
                    Font-Size="11px" />
            </td>
        </tr>
    </table>
    </form>

   
</body>
</html>
</asp:Content>