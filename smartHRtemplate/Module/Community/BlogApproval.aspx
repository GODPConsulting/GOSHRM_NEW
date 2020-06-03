

    <%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup ="true" CodeBehind="BlogApproval.aspx.vb" Inherits="GOSHRM.BlogApproval" EnableEventValidation="false" Debug="true"%>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID = "content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


  <html xmlns="http://www.w3.org/1999/xhtml">
<%--<meta name="viewport" content="width=device-width, initial-scale=1">
<link rel="stylesheet" href="http://www.w3schools.com/lib/w3.css">--%>
<head id="Head1" >
    <title></title>

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
            width: 124px;
        }
        .style6
        {
        }
        .style8
        {
            width: 509px;
        }
        .style9
        {
            width: 124px;
        }
    </style>
</head>

<body style="height: 317px;
    margin-bottom: 15px;">
    <form id="form1"  action="">
        
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
        rel="Stylesheet" type="text/css" />
    <table>
        <tr>
            <td class="style1" colspan="2" style="background-color: #1BA691">
                Blog Approval Comment
            </td>
        </tr>
     
        <tr>
            <td class="style5" valign="top">
                <asp:Label ID="Label2" runat="server" Font-Names="Verdana" Font-Size="12px"
                    Text="Comment" Font-Bold="True" ForeColor="#666666"></asp:Label>
            </td>
            <td class="style8">
                <asp:TextBox ID="txtGoalDesc" runat="server" BorderColor="#CCCCCC" ForeColor="#666666"
                    BorderWidth="1px" Height="150px" TextMode="MultiLine" Width="100%" 
                    Font-Names="Verdana" Font-Size="12px"></asp:TextBox>
            </td>
        </tr>
   
        <tr>
            <td class="style6" colspan="2">
                <asp:Label ID="lblstatus" runat="server" Font-Bold="True" Font-Names="Candara" Font-Size="12px"
                    ForeColor="#FF3300"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style9">
                <asp:Button ID="btnAdd" runat="server" Text="Save" BackColor="#1BA691" ForeColor="White"
                    Width="100px" Height="20px" BorderStyle="None" Font-Names="Verdana"  Font-Bold ="True"
                    Font-Size="11px" />
            </td>
            <td class="style8">
                <asp:Button ID="btnCancel" runat="server" Text="Close" Font-Bold ="True"
                    BackColor="#FF3300" ForeColor="White"
                    Width="100px" Height="20px" BorderStyle="None" Font-Names="Verdana" 
                    Font-Size="11px" />
                <asp:TextBox ID="txtid" runat="server" Width="56px" Style="font-size: medium; font-family: Candara"
                    Font-Names="Candara" Height="16px" Visible="False"></asp:TextBox>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
</asp:Content>

<asp:Content ID="Content2" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .style21
        {
            width: 100%;
        }
        .style22
        {
        }
        .style23
        {
            width: 275px;
        }
    </style>
</asp:Content>