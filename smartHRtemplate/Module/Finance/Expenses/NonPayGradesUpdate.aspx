
<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="NonPayGradesUpdate.aspx.vb" Inherits="GOSHRM.NonPayGradesUpdate" EnableEventValidation="false" %>


<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagPrefix="asp" %>

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
            font-weight: bold;
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
            width: 175px;
            color: #FF3300;
        }
        .style5
        {
            font-family: Candara;
            font-size: medium;
            width: 175px;
        }
        .style6
        {
        }
        .RadPicker{vertical-align:middle}
        .RadPicker{vertical-align:middle}.rdfd_{position:absolute}.rdfd_{position:absolute}.RadPicker .rcTable{table-layout:auto}.RadPicker .rcTable{table-layout:auto}.RadPicker .RadInput{vertical-align:baseline}.RadPicker .RadInput{vertical-align:baseline}.RadInput_Default{font:12px "segoe ui",arial,sans-serif}.RadInput{vertical-align:middle}.RadInput{vertical-align:middle}.RadInput_Default{font:12px "segoe ui",arial,sans-serif}.RadInput .riTextBox{height:17px}.RadInput .riTextBox{height:17px}.RadPicker_Default .rcCalPopup{background-position:0 0}.RadPicker_Default .rcCalPopup{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Calendar.sprite.gif')}.RadPicker .rcCalPopup{display:block;overflow:hidden;width:22px;height:22px;background-color:transparent;background-repeat:no-repeat;text-indent:-2222px;text-align:center;-webkit-box-sizing:content-box;-moz-box-sizing:content-box;box-sizing:content-box}.RadPicker .rcCalPopup{display:block;overflow:hidden;width:22px;height:22px;background-color:transparent;background-repeat:no-repeat;text-indent:-2222px;text-align:center;-webkit-box-sizing:content-box;-moz-box-sizing:content-box;box-sizing:content-box}.RadPicker_Default .rcCalPopup{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Calendar.sprite.gif')}.RadPicker_Default .rcCalPopup{background-position:0 0}.RadPicker td a{position:relative;outline:0;z-index:2;margin:0 2px;text-decoration:none}.RadPicker td a{position:relative;outline:0;z-index:2;margin:0 2px;text-decoration:none}
        .style9
        {
            width: 586px;
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
<body onunload = "window.opener.location=window.opener.location;" style="height: 317px">
    <form id="form1" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
        rel="Stylesheet" type="text/css" />


    <table>
    <tr>
    <td class="style1" colspan="2" style="background-color: #1BA691">
    
        NON PAYROLL EXPENSE - GRADE</td>
    </tr>

      <tr>

    <td class="style6">
    
    </td>
    <td class="style9">
       
        <asp:TextBox ID="txtid" runat="server" Width="13px" 
            style="font-size: medium; font-family: Candara" Font-Names="Candara" 
            Height="20px" Visible="False"></asp:TextBox>
       
    </td>
    </tr>
    <tr>

    <td class="style6">
    
        <asp:Label ID="Label1" runat="server" Font-Names="Verdana" Font-Size="12px" 
            Text="Job Grade" Font-Bold="True" ForeColor="#666666"></asp:Label>
        </td>
    <td class="style9">
       
        <telerik:RadDropDownList ID="cboGrade" runat="server" ForeColor="#666666"
            DefaultMessage="-- Select --" Font-Names="Verdana" Font-Size="12px" 
            Height="16px" Width="100%">
        </telerik:RadDropDownList>
       
    </td>
    </tr>
   
    <tr>

    <td class="style6">
    
        <asp:Label ID="Label2" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
            Text="Item"></asp:Label>
        </td>
    <td class="style9">
       
        <telerik:RadDropDownList ID="cboItem" runat="server"  ForeColor="#666666"
            DefaultMessage="-- Select --" Font-Names="Verdana" Font-Size="12px" 
            Height="16px" Width="100%" AutoPostBack="True">
        </telerik:RadDropDownList>
       
    </td>
    </tr>
    
    <tr>

    <td class="style5">
    
        <asp:Label ID="Label3" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
            Text="Amount/Percentage"></asp:Label>
        </td>
    <td class="style9">
       
        <asp:TextBox ID="txtAmount" runat="server" Width="30%" ForeColor="#666666"
            style="text-align: right;" Font-Names="Verdana" 
            BorderColor="#CCCCCC" BorderWidth="1px" Font-Size="12px"></asp:TextBox>
       
    </td>
    </tr>

    
     <tr>

    <td class="style6">
    
        <asp:Label ID="Label4" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
            Text="Item Type"></asp:Label>
         </td>
    <td class="style9">
       
        <asp:Label ID="lblItemType" runat="server" Font-Names="Verdana" ForeColor="#666666"
            Font-Size="12px"></asp:Label>
         </td>
    </tr>
     
    
    
 
 <tr>
    <td valign ="top" class="style6">
        &nbsp;</td>
    <td class="style9">
       
        &nbsp;</td>
    </tr>
    
    
    <tr>
    <td class="style2" 
            style="border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #C0C0C0">
    
        * Required Field</td>
    <td style="border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #C0C0C0" 
            class="style9">
       
    </td>
    </tr>
 <tr>
    <td class="style6" colspan="2">
    
        <asp:Label ID="lblstatus" runat="server" Font-Bold="True" Font-Names="Candara" 
            Font-Size="12px" ForeColor="#FF3300"></asp:Label>
       
    </td>
    </tr>

     <tr>

    <td class="style6">
    
                     <asp:Button ID="btnAdd" runat="server" Text="Save" BackColor="#1BA691" 
                         ForeColor="White" Width="120px" Height = "25px" BorderStyle="None" 
                         Font-Names="Verdana" Font-Size="11px"/>
                 </td>
    <td class="style9">
       
                     <asp:Button ID="btnCancel" runat="server" Text="Close" BackColor="#FF3300" 
                         ForeColor="White" Width="120px" Height = "25px" BorderStyle="None" 
                         Font-Names="Verdana" Font-Size="11px"/>
                 </td>
    </tr>
    </table>
 
    </form>
</body>
</html>
