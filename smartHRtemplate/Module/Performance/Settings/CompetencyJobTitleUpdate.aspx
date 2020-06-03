<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CompetencyJobTitleUpdate.aspx.vb"
    Inherits="GOSHRM.CompetencyJobTitleUpdate" EnableEventValidation="false" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<%--<meta name="viewport" content="width=device-width, initial-scale=1">
<link rel="stylesheet" href="http://www.w3schools.com/lib/w3.css">--%>
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
            width: 307px;
            color: #FF0000;
        }
        .style5
        {
            font-family: Candara;
            font-size: medium;
            width: 307px;
        }
        .style6
        {
            width: 307px;
        }
        .style9
        {
        }
        .style11
        {
            width: 302px;
        }
        .style12
        {
            width: 328px;
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
            <td class="style1" colspan="2" style="background-color: #1BA691">
                Competency Mapping For Job Title</td>
        </tr>
        <tr>
            <td class="style6">
            </td>
            <td class="style12">
                <asp:TextBox ID="txtid" runat="server" Width="183px" Style="font-size: medium; font-family: Candara"
                    Font-Names="Candara" Height="20px" Visible="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style5">
                <asp:Label ID="Label1" runat="server" Font-Names="Verdana" Font-Size="12px"  Font-Bold="True" ForeColor="#666666" Text="Job Title"></asp:Label>
            </td>
            <td class="style9" colspan="2">
                <telerik:RadComboBox ID="radJobTitle" Runat="server" Font-Size="12px"  ForeColor="#666666" Width="80%">
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td class="style5" valign="top">
                <asp:Label ID="Label2" runat="server" Font-Names="Verdana" Font-Size="12px"  Font-Bold="True" ForeColor="#666666" Text="Competencies"></asp:Label>
            </td>
            <td class="style12">
                <asp:Label ID="Label3" runat="server" Font-Names="Verdana" Font-Bold="True" ForeColor="#666666" Font-Size="12px" 
                    Text="Mapped Competencies"></asp:Label>
            </td>
              <td class="style11">
               
                  &nbsp;</td>
        </tr>
        <tr>
            <td class="style6" valign="top">
                <telerik:RadListBox ID="lstSource" runat="server" AllowTransfer="True" 
                    AllowTransferOnDoubleClick="True" AutoPostBackOnTransfer="True" 
                    BorderColor="#CCCCCC" BorderWidth="1px" RenderMode="Lightweight" 
                    Sort="Ascending" TransferToID="lstDestination" Width="100%" Height="200px" 
                    SelectionMode="Multiple" Font-Names="Verdana" Font-Size="11px" 
                    ForeColor="#666666">
                </telerik:RadListBox>
            </td>
            <td class="style12" valign="top">
                <telerik:RadListBox ID="lstDestination" runat="server" AllowReorder="True" 
                    BorderColor="#CCCCCC" BorderWidth="1px" Width="100%" Height="200px" 
                    SelectionMode="Multiple" Font-Names="Verdana" Font-Size="11px" 
                    ForeColor="#666666">
                </telerik:RadListBox>
            </td>
              <td class="style11">
               
            </td>
        </tr>
        <tr>
            <td class="style2" style="border-bottom-style: solid; border-bottom-width: thin;
                border-bottom-color: #C0C0C0">
                &nbsp;</td>
            <td style="border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #C0C0C0"
                class="style12">
            </td>
        </tr>
        <tr>
            <td class="style6">
            </td>
            <td class="style12">
                <asp:Label ID="lblstatus" runat="server" Font-Bold="True" Font-Names="Candara" Font-Size="12px"
                    ForeColor="#FF3300"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style6">
                <asp:Button ID="btnAdd" runat="server" Text="Save" BackColor="#1BA691" ForeColor="White"
                    Width="120px" Height="20px" BorderStyle="None" Font-Names="Verdana" 
                    Font-Size="11px" />
            </td>
            <td class="style12">
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" BackColor="#FF3300" ForeColor="White"
                    Width="120px" Height="20px" BorderStyle="None" Font-Names="Verdana" 
                    Font-Size="11px" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
