<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="EmployeeSkills.aspx.vb" Inherits="GOSHRM.EmployeeSkills" EnableEventValidation="true" %>

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
            color: #666666;
            font-family: Candara;
        }
        .lbl
        {
            font-family: Candara;
            font-size: small;
        }
        .style2
        {
            font-family: Candara;
            font-size: small;
            color: #FF3300;
        }
        .style3
        {
            font-family: Candara;
            font-size: medium;
            height: 44px;
        }
        .style4
        {
            height: 44px;
        }
        .RadPicker{vertical-align:middle}.RadPicker{vertical-align:middle}.RadPicker{vertical-align:middle}.RadPicker{vertical-align:middle;
            font-family: Candara;
            font-size: medium;
        }.rdfd_{position:absolute}.rdfd_{position:absolute}.rdfd_{position:absolute}.rdfd_{position:absolute}.RadPicker .rcTable{table-layout:auto}.RadPicker .rcTable{table-layout:auto}.RadPicker .rcTable{table-layout:auto}.RadPicker .rcTable{table-layout:auto}.RadPicker .RadInput{vertical-align:baseline}.RadPicker .RadInput{vertical-align:baseline}.RadPicker .RadInput{vertical-align:baseline}.RadPicker .RadInput{vertical-align:baseline}.RadInput_Default{font:12px "segoe ui",arial,sans-serif}.RadInput{vertical-align:middle}.RadInput_Default{font:12px "segoe ui",arial,sans-serif}.RadInput{vertical-align:middle}.RadInput_Default{font:12px "segoe ui",arial,sans-serif}.RadInput{vertical-align:middle}.RadInput_Default{font:12px "segoe ui",arial,sans-serif}.RadInput{vertical-align:middle}.RadInput .riTextBox{height:17px}.RadInput .riTextBox{height:17px}.RadInput .riTextBox{height:17px}.RadInput .riTextBox{height:17px}.RadPicker_Default .rcCalPopup{background-position:0 0}.RadPicker_Default .rcCalPopup{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Calendar.sprite.gif')}.RadPicker .rcCalPopup{display:block;overflow:hidden;width:22px;height:22px;background-color:transparent;background-repeat:no-repeat;text-indent:-2222px;text-align:center;-webkit-box-sizing:content-box;-moz-box-sizing:content-box;box-sizing:content-box}.RadPicker_Default .rcCalPopup{background-position:0 0}.RadPicker_Default .rcCalPopup{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Calendar.sprite.gif')}.RadPicker .rcCalPopup{display:block;overflow:hidden;width:22px;height:22px;background-color:transparent;background-repeat:no-repeat;text-indent:-2222px;text-align:center;-webkit-box-sizing:content-box;-moz-box-sizing:content-box;box-sizing:content-box}.RadPicker_Default .rcCalPopup{background-position:0 0}.RadPicker_Default .rcCalPopup{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Calendar.sprite.gif')}.RadPicker .rcCalPopup{display:block;overflow:hidden;width:22px;height:22px;background-color:transparent;background-repeat:no-repeat;text-indent:-2222px;text-align:center;-webkit-box-sizing:content-box;-moz-box-sizing:content-box;box-sizing:content-box}.RadPicker_Default .rcCalPopup{background-position:0 0}.RadPicker_Default .rcCalPopup{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Calendar.sprite.gif')}.RadPicker .rcCalPopup{display:block;overflow:hidden;width:22px;height:22px;background-color:transparent;background-repeat:no-repeat;text-indent:-2222px;text-align:center;-webkit-box-sizing:content-box;-moz-box-sizing:content-box;box-sizing:content-box}.RadPicker td a{position:relative;outline:0;z-index:2;margin:0 2px;text-decoration:none}.RadPicker td a{position:relative;outline:0;z-index:2;margin:0 2px;text-decoration:none}.RadPicker td a{position:relative;outline:0;z-index:2;margin:0 2px;text-decoration:none}.RadPicker td a{position:relative;outline:0;z-index:2;margin:0 2px;text-decoration:none}
    </style>
</head>

<body onunload="window.opener.location=window.opener.location;" style="height: 317px">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    
    <table>
        <tr>
            <td class="style1" colspan="2" style="background-color: #C0C0C0">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:TextBox ID="txtid" runat="server" Width="13px" Style="font-size: medium; font-family: Candara"
                    Font-Names="Candara" Height="20px" Visible="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style3">
                &nbsp;<asp:Label ID="Label1" runat="server" CssClass="lbl" 
                    Text="Employee Number:" Font-Names="Verdana"
                        Font-Size="12px" Font-Bold="True" ForeColor="#666666"></asp:Label>
            </td>
            <td class="style4">
                <asp:TextBox ID="txtEmpID" runat="server" Width="634px"  BorderColor="#999999" BorderStyle="Solid" 
                    Font-Names="Verdana" Font-Size="12px" BorderWidth="1px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                    <asp:Label ID="Label2" runat="server" CssClass="lbl" Text="Skill:" Font-Names="Verdana"
                        Font-Size="12px" Font-Bold="True" ForeColor="#666666"></asp:Label>
                </td>
            <td>
                <telerik:RadDropDownList ID="radSkill" runat="server" 
                    DefaultMessage="-- Select --" Font-Names="Verdana" Font-Size="12px" 
            Height="16px" Width="443px">
                </telerik:RadDropDownList>

     
            </td>
        </tr>
          
        
                     
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
    
        
        
        <tr>
            <td>
            </td>
            <td>
                <asp:Label ID="lblstatus" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="12px"
                    ForeColor="#FF3300"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnAdd" runat="server" Text="Save" BackColor="#1BA691" ForeColor="White"
                    Width="120px" Height="30px" BorderStyle="None" Font-Bold="True" 
                    Font-Names="Verdana" Font-Size="11px" />
            </td>
            <td>
                <asp:Button ID="btnCancel" runat="server" Text="Close" BackColor="#FF3300" ForeColor="White"
                    Width="120px" Height="30px" BorderStyle="None" Font-Bold="True" 
                    Font-Names="Verdana" Font-Size="11px" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
