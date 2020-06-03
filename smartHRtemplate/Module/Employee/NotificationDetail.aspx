
<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="NotificationDetail.aspx.vb" Inherits="GOSHRM.NotificationDetail" EnableEventValidation="false" %>


<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
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
        .style5
        {
            font-family: Candara;
            font-size: medium;
            width: 89px;
        }
        .style6
        {
            width: 89px;
        }
        .style7
        {
            width: 519px;
        }
        </style>
</head>

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
    
        Message</td>
    </tr>

      <tr>

    <td class="style6">
    
        <asp:Label ID="lblid" runat="server" Font-Names="Verdana" 
            Font-Size="12px" Visible="False"></asp:Label>
    
    </td>
    <td class="style7">
       
        <asp:Label ID="lblstatus" runat="server" Font-Bold="True" Font-Names="Candara" 
            Font-Size="12px" ForeColor="#FF3300"></asp:Label>
       
          </td>
    </tr>
    

    <tr>

    <td class="style5">
    
        <asp:Label ID="Label1" runat="server" Font-Names="Verdana" Text="From:" 
            Font-Size="12px"></asp:Label>
        </td>
    <td class="style7">
       
        <asp:Label ID="lblfrom" runat="server" Font-Names="Verdana" 
            Font-Size="12px" BorderColor="#CCCCCC" BorderWidth="1px" Width="500px"></asp:Label>
       
    </td>
    </tr>
     <tr>

    <td class="style5">
    
        <asp:Label ID="Label9" runat="server" Font-Names="Verdana" Text="To:" 
            Font-Size="12px"></asp:Label>
        </td>
    <td class="style7">
       
        <asp:Label ID="lblto" runat="server" Font-Names="Verdana" 
            Font-Size="12px" BorderColor="#CCCCCC" BorderWidth="1px" Width="500px"></asp:Label>
       
    </td>
    </tr>
     <tr>

    <td class="style5">
    
        <asp:Label ID="Label12" runat="server" Font-Names="Verdana" Text="CC:" 
            Font-Size="12px"></asp:Label>
        </td>
    <td class="style7">
       
        <asp:Label ID="lblcc" runat="server" Font-Names="Verdana" 
            Font-Size="12px" BorderColor="#CCCCCC" BorderWidth="1px" Width="500px"></asp:Label>
       
    </td>
    </tr>

    <tr>

    <td class="style6">
    
        <asp:Label ID="Label2" runat="server" Font-Names="Verdana" Text="Date:" 
            Font-Size="12px"></asp:Label>
    
    </td>
    <td class="style7">
       
        <asp:Label ID="lbldate" runat="server" Font-Names="Verdana" 
            Font-Size="12px" BorderColor="#CCCCCC" BorderWidth="1px" Width="500px"></asp:Label>
       
    </td>
    </tr>
   
    <tr>

    <td valign=top class="style6">
    
        <asp:Label ID="Label13" runat="server" Font-Names="Verdana" Text="Subject:" 
            Font-Size="12px"></asp:Label>
        </td>
    <td class="style7">
       
        <asp:Label ID="lblsubject" runat="server" Font-Names="Verdana" 
            Font-Size="12px" BorderColor="#CCCCCC" BorderWidth="1px" Width="500px"></asp:Label>
       
    </td>
    </tr>
   
    <tr>

    <td valign=top class="style6">
    
        &nbsp;</td>
    <td class="style7">
       
        <asp:TextBox ID="txtbody" runat="server" BorderColor="#CCCCCC" 
            BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="12px" 
            Height="450px" ReadOnly="True" TextMode="MultiLine" Width="500px"></asp:TextBox>
       
    </td>
    </tr>
 <tr>
    <td class="style6">
    
        &nbsp;</td>
    <td class="style7">
       
                     <asp:Button ID="btnCancel" runat="server" Text="Close" BackColor="#FF3300" 
                         ForeColor="White" Width="120px" Height = "25px" BorderStyle="None" 
                         Font-Names="Verdana" Font-Size="12px"/>
       
                     <asp:Button ID="btnOO" runat="server" BackColor="White" 
                         ForeColor="Black" Width="120px" Height = "25px" BorderStyle="None" 
                         Font-Names="Verdana" Font-Size="12px"/>
       
    </td>
    </tr>

     </table>
 
    </form>
</body>
</html>
