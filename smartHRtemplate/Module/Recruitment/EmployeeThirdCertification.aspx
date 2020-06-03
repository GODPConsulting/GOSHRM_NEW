<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="EmployeeThirdCertification.aspx.vb" Inherits="GOSHRM.EmployeeThirdCertification" EnableEventValidation="true" %>

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
            width: 308px;
        }
        .style3
        {
            font-family: Candara;
            font-size: medium;
            height: 44px;
            width: 308px;
        }
        .style4
        {
            height: 44px;
        }
        .style7
        {
            color: #FF3300;
        }
        .style8
        {
            font-family: Candara;
            font-size: medium;
            border-left-color: #A0A0A0;
            border-right-color: #C0C0C0;
            border-top-color: #A0A0A0;
            border-bottom-color: #C0C0C0;
            padding: 1px;
        }
        .style9
        {
            font-family: Candara;
            border-left-color: #A0A0A0;
            border-right-color: #C0C0C0;
            border-top-color: #A0A0A0;
            border-bottom-color: #C0C0C0;
            padding: 1px;
        }
.RadPicker{vertical-align:middle}.RadPicker{vertical-align:middle}.RadPicker{vertical-align:middle}.RadPicker{vertical-align:middle;
            font-family: Candara;
            font-size: medium;
        }.rdfd_{position:absolute}.rdfd_{position:absolute}.rdfd_{position:absolute}.rdfd_{position:absolute}.RadPicker .rcTable{table-layout:auto}.RadPicker .rcTable{table-layout:auto}.RadPicker .rcTable{table-layout:auto}.RadPicker .rcTable{table-layout:auto}.RadPicker .RadInput{vertical-align:baseline}.RadPicker .RadInput{vertical-align:baseline}.RadPicker .RadInput{vertical-align:baseline}.RadPicker .RadInput{vertical-align:baseline}.RadInput_Default{font:12px "segoe ui",arial,sans-serif}.RadInput{vertical-align:middle}.RadInput_Default{font:12px "segoe ui",arial,sans-serif}.RadInput{vertical-align:middle}.RadInput_Default{font:12px "segoe ui",arial,sans-serif}.RadInput{vertical-align:middle}.RadInput_Default{font:12px "segoe ui",arial,sans-serif}.RadInput{vertical-align:middle}.RadInput .riTextBox{height:17px}.RadInput .riTextBox{height:17px}.RadInput .riTextBox{height:17px}.RadInput .riTextBox{height:17px}.RadPicker_Default .rcCalPopup{background-position:0 0}.RadPicker_Default .rcCalPopup{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Calendar.sprite.gif')}.RadPicker .rcCalPopup{display:block;overflow:hidden;width:22px;height:22px;background-color:transparent;background-repeat:no-repeat;text-indent:-2222px;text-align:center;-webkit-box-sizing:content-box;-moz-box-sizing:content-box;box-sizing:content-box}.RadPicker_Default .rcCalPopup{background-position:0 0}.RadPicker_Default .rcCalPopup{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Calendar.sprite.gif')}.RadPicker .rcCalPopup{display:block;overflow:hidden;width:22px;height:22px;background-color:transparent;background-repeat:no-repeat;text-indent:-2222px;text-align:center;-webkit-box-sizing:content-box;-moz-box-sizing:content-box;box-sizing:content-box}.RadPicker_Default .rcCalPopup{background-position:0 0}.RadPicker_Default .rcCalPopup{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Calendar.sprite.gif')}.RadPicker .rcCalPopup{display:block;overflow:hidden;width:22px;height:22px;background-color:transparent;background-repeat:no-repeat;text-indent:-2222px;text-align:center;-webkit-box-sizing:content-box;-moz-box-sizing:content-box;box-sizing:content-box}.RadPicker_Default .rcCalPopup{background-position:0 0}.RadPicker_Default .rcCalPopup{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Calendar.sprite.gif')}.RadPicker .rcCalPopup{display:block;overflow:hidden;width:22px;height:22px;background-color:transparent;background-repeat:no-repeat;text-indent:-2222px;text-align:center;-webkit-box-sizing:content-box;-moz-box-sizing:content-box;box-sizing:content-box}.RadPicker td a{position:relative;outline:0;z-index:2;margin:0 2px;text-decoration:none}.RadPicker td a{position:relative;outline:0;z-index:2;margin:0 2px;text-decoration:none}.RadPicker td a{position:relative;outline:0;z-index:2;margin:0 2px;text-decoration:none}.RadPicker td a{position:relative;outline:0;z-index:2;margin:0 2px;text-decoration:none}
        .style10
        {
            width: 308px;
        }
        .style99
        {
            width: 100px;
        }
    </style>
</head>

<body onunload="window.opener.location=window.opener.location;" style="height: 317px">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    
    <table>
        <tr>
            <td class="style1" colspan="2" style="background-color: #C0C0C0">
                Certification</td>
        </tr>
        <tr>
            <td class="style10">
            </td>
            <td>
                <asp:TextBox ID="txtid" runat="server" Width="183px" Style="font-size: medium; font-family: Candara"
                    Font-Names="Candara" Height="20px" Visible="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style3">
                ID
            </td>
            <td class="style4">
                <asp:TextBox ID="txtEmpID" runat="server" Width="10%" Style="font-size: small;
                    font-family: Candara" BorderStyle="None" Enabled="False"></asp:TextBox>
            </td>
        </tr>
       
        <tr>
            <td class="style10">
                <span class="style9">Professional Certificate</span><span class="style8"> </span> <span class="style7">*</span></td>
            <td >
                <telerik:RadDropDownList ID="radCertification" runat="server" 
                    DefaultMessage="-- Select --" Font-Names="Candara" Font-Size="Medium" 
            Height="23px" Width="100%" style="font-size: small">
                </telerik:RadDropDownList>

     
            </td>
        </tr>
      
        
        
        
        
        <tr>
            <td class="style10">
                <span class="style9">Institute / School </span> <span class="style7">*</span></td>
            <td>
                <asp:TextBox ID="txtInstitute" runat="server" Width="634px" Style="font-size: small;
                    font-family: Candara"></asp:TextBox>

     
            </td>
        </tr>
       
        <tr>
            <td class="style10">
                <span class="style8">Date Granted </span> <span class="style7">*</span></td>
            <td>
               <table width="100%">
               <tr>
               <td class="style99">
                   <telerik:RadComboBox ID="radGrantDate" Runat="server" AutoPostBack="True">
                   </telerik:RadComboBox>
               </td>
               <td>
                   <telerik:RadComboBox ID="radGrantYear" Runat="server" Visible="False">
                   </telerik:RadComboBox>
               </td>
               <td>
                   &nbsp;</td>
               </tr>
               
               </table>

     
            </td>
           
        </tr>
       
        <tr>
            <td class="style10">
                <span class="style8">Expiry Date </span></td>
            <td>
               <table width="100%">
               <tr>
               <td class="style99">
                   <telerik:RadComboBox ID="radExpiryDate" Runat="server" AutoPostBack="True">
                   </telerik:RadComboBox>
               </td>
               <td>
                   <telerik:RadComboBox ID="radExpiryYear" Runat="server" style="margin-left: 0px" 
                       Visible="False">
                   </telerik:RadComboBox>
               </td>
               <td>
                   &nbsp;</td>
               </tr>
               
               </table>

     
            </td>
           
        </tr>
       
        <tr>
            <td class="style2" style="border-bottom-style: solid; border-bottom-width: thin;
                border-bottom-color: #C0C0C0">
                * Required Field
            </td>
            <td style="border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #C0C0C0">
            </td>
        </tr>
        <tr>
            <td class="style10">
            </td>
            <td>
                <asp:Label ID="lblstatus" runat="server" Font-Bold="True" Font-Names="Candara" Font-Size="Small"
                    ForeColor="#FF3300"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style10">
                <asp:Button ID="btnAdd" runat="server" Text="Save" BackColor="#1BA691" ForeColor="White"
                    Width="120px" Height="30px" BorderStyle="None" />
            </td>
            <td>
                <asp:Button ID="btnCancel" runat="server" Text="Close" BackColor="#FF3300" ForeColor="White"
                    Width="120px" Height="30px" BorderStyle="None" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
