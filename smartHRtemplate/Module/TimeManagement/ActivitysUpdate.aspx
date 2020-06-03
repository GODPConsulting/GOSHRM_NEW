<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ActivitysUpdate.aspx.vb"
    Inherits="GOSHRM.ActivitysUpdate" EnableEventValidation="false" %>

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
        .lbl
        {
            font-family: Candara;
            font-size: medium;
        }
        .style6
        {
        }
        .style7
        {
            width: 511px;
        }
        .style8
        {
            width: 266px;
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
<body onunload="window.opener.location=window.opener.location;" style="height: 317px">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
        rel="Stylesheet" type="text/css" />
    <table>
        <tr>
            <td class="style6" valign="top" colspan="2">
                <asp:TextBox ID="txtid" runat="server" Visible="False" Width="7px" 
                    Font-Size="5px"></asp:TextBox>
                <asp:TextBox ID="txtprojid" runat="server" Visible="False" Width="7px" 
                    Font-Size="5px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style8" valign="top">
                <asp:Label ID="Label10" runat="server" Text="Project" Font-Names="Verdana" 
                    Font-Size="12px" Font-Bold="True" ForeColor="#666666"></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="lblProject" runat="server" Font-Names="Verdana" Font-Size="12px"
                    Style="text-align: left" Width="100%" ForeColor="#666666"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style8" valign="top">
                <asp:Label ID="Label11" runat="server" Text="Client" Font-Names="Verdana" 
                    Font-Size="12px" Font-Bold="True" ForeColor="#666666"></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="lblClient" runat="server" Font-Names="Verdana" Font-Size="12px"
                    Style="text-align: left" Width="100%" ForeColor="#666666"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style8" valign="top">
                <asp:Label ID="Label9" runat="server" Text="Activity" Font-Names="Verdana" 
                    Font-Size="12px" Font-Bold="True" ForeColor="#666666"></asp:Label>
            </td>
            <td class="style7">
                <asp:TextBox ID="txtDetail" runat="server" Width="100%" Style="font-size: medium;
                    font-family: Candara" Font-Names="Verdana" Height="150px" TextMode="MultiLine"
                    BorderColor="#CCCCCC" BorderWidth="1px" Font-Size="12px" 
                    ForeColor="#666666"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style8" valign="top">
                <asp:Label ID="Label1" runat="server" 
                    Text="Duration to Complete Activity (Hours)" Font-Names="Verdana" 
                    Font-Size="12px" Font-Bold="True" ForeColor="#666666"></asp:Label>
            </td>
            <td class="style7">
                <asp:TextBox ID="txtEstimation" runat="server" Width="74px" 
                    BorderColor="#CCCCCC" BorderWidth="1px" Font-Names="Verdana" Font-Size="12px" 
                    ForeColor="#666666"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style8">
            </td>
            <td class="style7">
                <asp:Label ID="lblstatus" runat="server" Font-Bold="True" Font-Names="Candara" Font-Size="12px"
                    ForeColor="#FF3300"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style8">
                <asp:Button ID="btnAdd" runat="server" Text="Save" BackColor="#1BA691" ForeColor="White"
                    Width="120px" Height="25px" BorderStyle="None" Font-Names="Verdana" 
                    Font-Size="11px" />
            </td>
            <td class="style7">
                <asp:Button ID="btnCancel" runat="server" Text="Close" BackColor="#FF3300" ForeColor="White"
                    Width="120px" Height="25px" BorderStyle="None" Font-Names="Verdana" 
                    Font-Size="11px" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
