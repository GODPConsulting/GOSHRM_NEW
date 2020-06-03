<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="QueryEmployeePage.aspx.vb" Inherits="GOSHRM.QueryEmployeePage"
    EnableEventValidation="false" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
        }
        .lbl
        {
            font-family: Candara;
            font-size: medium;
        }
        .style6
        {
            width: 189px;
        }
        .style7
        {
            width: 423px;
        }
    .RadComboBox_Default{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.RadComboBox{text-align:left;display:inline-block;vertical-align:middle;white-space:nowrap;*display:inline;*zoom:1}.RadComboBox_Default{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.RadComboBox{text-align:left;display:inline-block;vertical-align:middle;white-space:nowrap;*display:inline;*zoom:1}.RadComboBox_Default{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.RadComboBox{text-align:left;display:inline-block;vertical-align:middle;white-space:nowrap;*display:inline;*zoom:1}.RadComboBox_Default{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.RadComboBox{text-align:left;display:inline-block;vertical-align:middle;white-space:nowrap;*display:inline;*zoom:1}.RadComboBox .rcbReadOnly .rcbInputCellLeft{background-position:0 -88px}.RadComboBox .rcbReadOnly .rcbInputCellLeft{background-position:0 -88px}.RadComboBox .rcbReadOnly .rcbInputCellLeft{background-position:0 -88px}.RadComboBox .rcbReadOnly .rcbInputCellLeft{background-position:0 -88px}.RadComboBox_Default .rcbInputCell{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')}.RadComboBox .rcbInputCellLeft{background-position:0 0}.RadComboBox .rcbInputCell{padding-right:4px;padding-left:5px;width:100%;height:20px;line-height:20px;text-align:left;vertical-align:middle}.RadComboBox .rcbInputCell{padding:0;border-width:0;border-style:solid;background-color:transparent;background-repeat:no-repeat}.RadComboBox_Default .rcbInputCell{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')}.RadComboBox .rcbInputCellLeft{background-position:0 0}.RadComboBox .rcbInputCell{padding-right:4px;padding-left:5px;width:100%;height:20px;line-height:20px;text-align:left;vertical-align:middle}.RadComboBox .rcbInputCell{padding:0;border-width:0;border-style:solid;background-color:transparent;background-repeat:no-repeat}.RadComboBox_Default .rcbInputCell{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')}.RadComboBox .rcbInputCellLeft{background-position:0 0}.RadComboBox .rcbInputCell{padding-right:4px;padding-left:5px;width:100%;height:20px;line-height:20px;text-align:left;vertical-align:middle}.RadComboBox .rcbInputCell{padding:0;border-width:0;border-style:solid;background-color:transparent;background-repeat:no-repeat}.RadComboBox_Default .rcbInputCell{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')}.RadComboBox .rcbInputCellLeft{background-position:0 0}.RadComboBox .rcbInputCell{padding-right:4px;padding-left:5px;width:100%;height:20px;line-height:20px;text-align:left;vertical-align:middle}.RadComboBox .rcbInputCell{padding:0;border-width:0;border-style:solid;background-color:transparent;background-repeat:no-repeat}.RadComboBox_Default .rcbReadOnly .rcbInput{color:#333}.RadComboBox .rcbReadOnly .rcbInput{cursor:default}.RadComboBox_Default .rcbReadOnly .rcbInput{color:#333}.RadComboBox .rcbReadOnly .rcbInput{cursor:default}.RadComboBox_Default .rcbReadOnly .rcbInput{color:#333}.RadComboBox .rcbReadOnly .rcbInput{cursor:default}.RadComboBox_Default .rcbReadOnly .rcbInput{color:#333}.RadComboBox .rcbReadOnly .rcbInput{cursor:default}.RadComboBox_Default .rcbInput{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif;line-height:16px}.RadComboBox .rcbInput{margin:0;padding:2px 0 1px;height:auto;width:100%;border-width:0;outline:0;color:inherit;background-color:transparent;vertical-align:top}.RadComboBox_Default .rcbInput{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif;line-height:16px}.RadComboBox .rcbInput{margin:0;padding:2px 0 1px;height:auto;width:100%;border-width:0;outline:0;color:inherit;background-color:transparent;vertical-align:top}.RadComboBox_Default .rcbInput{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif;line-height:16px}.RadComboBox .rcbInput{margin:0;padding:2px 0 1px;height:auto;width:100%;border-width:0;outline:0;color:inherit;background-color:transparent;vertical-align:top}.RadComboBox_Default .rcbInput{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif;line-height:16px}.RadComboBox .rcbInput{margin:0;padding:2px 0 1px;height:auto;width:100%;border-width:0;outline:0;color:inherit;background-color:transparent;vertical-align:top}.RadComboBox .rcbReadOnly .rcbArrowCellRight{background-position:-162px -176px}.RadComboBox .rcbReadOnly .rcbArrowCellRight{background-position:-162px -176px}.RadComboBox .rcbReadOnly .rcbArrowCellRight{background-position:-162px -176px}.RadComboBox .rcbReadOnly .rcbArrowCellRight{background-position:-162px -176px}.RadComboBox_Default .rcbArrowCell{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')}.RadComboBox .rcbArrowCellRight{background-position:-18px -176px}.RadComboBox .rcbArrowCell{width:18px}.RadComboBox .rcbArrowCell{padding:0;border-width:0;border-style:solid;background-color:transparent;background-repeat:no-repeat}.RadComboBox_Default .rcbArrowCell{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')}.RadComboBox .rcbArrowCellRight{background-position:-18px -176px}.RadComboBox .rcbArrowCell{width:18px}.RadComboBox .rcbArrowCell{padding:0;border-width:0;border-style:solid;background-color:transparent;background-repeat:no-repeat}.RadComboBox_Default .rcbArrowCell{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')}.RadComboBox .rcbArrowCellRight{background-position:-18px -176px}.RadComboBox .rcbArrowCell{width:18px}.RadComboBox .rcbArrowCell{padding:0;border-width:0;border-style:solid;background-color:transparent;background-repeat:no-repeat}.RadComboBox_Default .rcbArrowCell{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')}.RadComboBox .rcbArrowCellRight{background-position:-18px -176px}.RadComboBox .rcbArrowCell{width:18px}.RadComboBox .rcbArrowCell{padding:0;border-width:0;border-style:solid;background-color:transparent;background-repeat:no-repeat}
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
<body onunload="window.opener.location=window.opener.location;" style="height: 317px">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
        rel="Stylesheet" type="text/css" />
        <div>
                        <asp:Label ID="lblstatus" runat="server" Font-Bold="True" 
                Font-Names="Candara" Font-Size="Small"
                            ForeColor="#FF3300"></asp:Label>
        </div>
        <table>
                <tr>
                    <td class="style1" colspan="2" style="background-color: #1BA691">
                        &nbsp;<asp:Label ID="lblHeader" runat="server" Font-Bold="True" 
                            Font-Names="Verdana" Font-Size="15px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style6">
                        <asp:Label ID="Label17" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                            Text="Query Message"></asp:Label>
                    </td>
                    <td class="style7">
                        <asp:Label ID="lblQuery" runat="server" Font-Names="Verdana" ForeColor="#666666" Font-Size="12px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style6">
                        <asp:Label ID="Label11" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                            Text="Date"></asp:Label>
                    </td>
                    <td class="style7">
                        <asp:Label ID="lblQueryDate" runat="server" Font-Names="Verdana" ForeColor="#666666"
                            Font-Size="12px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style6">
                        <asp:Label ID="Label10" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                            Text="Expected Response Date"></asp:Label>
                    </td>
                    <td class="style7">
                        <asp:Label ID="lblexpecteddate" runat="server" Font-Names="Verdana" ForeColor="#666666"
                            Font-Size="12px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style6">
                        <asp:Label ID="Label12" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                            Text="Expected Response Time"></asp:Label>
                    </td>
                    <td class="style7">
                        <asp:Label ID="lblexpectedtime" runat="server" Font-Names="Verdana" ForeColor="#666666"
                            Font-Size="12px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style6" valign="top">
                        
                        <asp:Label ID="Label16" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                            Text="My Response"></asp:Label>
                    </td>
                    <td class="style7">
                        <asp:TextBox ID="txtComment" runat="server" BorderColor="#CCCCCC" ForeColor="#666666"
                            BorderWidth="1px" Font-Names="Verdana" Font-Size="12px" Height="140px" TextMode="MultiLine" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style6" valign="top">
                        <asp:Label ID="Label3" runat="server" Font-Names="Verdana" Text="My Response Date" Font-Bold="True" ForeColor="#666666"
                            Font-Size="12px"></asp:Label>
                    </td>
                    <td class="style7">
                        <asp:Label ID="lblEmpDate" runat="server" Font-Names="Verdana" ForeColor="#666666" Font-Size="12px"></asp:Label>
                    </td>
                </tr>
              
                <tr>
                    <td class="style6">
                        <asp:Label ID="Label14" runat="server" Font-Names="Verdana" Text="Query Status" Font-Bold="True" ForeColor="#666666"
                            Font-Size="12px"></asp:Label>
                    </td>
                    <td class="style7">
                        <telerik:RadComboBox ID="cboApproval" runat="server" AutoPostBack="True" 
                            Font-Names="Verdana" Font-Size="12px" Width="150px" RenderMode="Lightweight">
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td class="style6" valign="top">
                        <asp:Label ID="Label5" runat="server" Font-Names="Verdana" Text="HR Comment" Font-Bold="True" ForeColor="#666666"
                            Font-Size="12px"></asp:Label>
                    </td>
                    <td class="style7">
                        <asp:Label ID="lblhrcomment" runat="server" Font-Names="Verdana" ForeColor="#666666"
                            Font-Size="12px"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td class="style6" valign="top">
                        <asp:Label ID="Label7" runat="server" Font-Names="Verdana" Text="HR Recommendation" Font-Bold="True" ForeColor="#666666"
                            Font-Size="12px"></asp:Label>
                    </td>
                    <td class="style7">
                        <asp:Label ID="lblrecomm" runat="server" Font-Names="Verdana" ForeColor="#666666"
                            Font-Size="12px"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td class="style6">
                        <asp:Label ID="lblinitiator" runat="server" Font-Size="10px" Text="Label" 
                            Visible="False"></asp:Label>
                        <asp:Label ID="lbldate" runat="server" Font-Size="10px" Text="Label" 
                            Visible="False"></asp:Label>
                        <asp:Label ID="lblinitiatorid" runat="server" Font-Size="10px" Text="Label" 
                            Visible="False"></asp:Label>
                            <asp:TextBox ID="txtid" runat="server" BorderColor="#CCCCCC" 
                            BorderWidth="1px" Font-Names="Verdana" Font-Size="12px" Height="10px" Style="font-size: medium;
                            font-family: Candara" Width="1%" Visible="False"></asp:TextBox>
                    </td>
                    <td class="style7">
                       
                       </td>
                </tr>
                <tr>
                    <td class="style6">
                        <asp:Button ID="btnUpdateStatus" runat="server" Text="Save" BackColor="#1BA691" ForeColor="White"
                            Width="120px" Height="25px" BorderStyle="None" Font-Names="Verdana" Font-Size="12px"
                            ToolTip="Save Comment" />
                    </td>
                    <td class="style7">
                        <table width="100%">
                            <tr>
                                <td>
                                     <asp:Button ID="btnBack" runat="server" Text="Close" BackColor="#FF3300" ForeColor="White"
                            Width="120px" Height="25px" BorderStyle="None" Font-Names="Verdana" 
                            Font-Size="12px" />
                                </td>
                                   <td>
                                    <asp:Button ID="btnNotifyHR" runat="server" Text="Send Notification" 
                            BackColor="#0099FF" ForeColor="White"
                            Width="120px" Height="25px" BorderStyle="None" Font-Names="Verdana" Font-Size="12px"
                            ToolTip="Notification will be sent to the Reporting Officer" />
                                </td>
                            </tr>
                        </table>
                       
                    </td>
                </tr>

               
            </table> 
    
    </form>
</body>
</html>
