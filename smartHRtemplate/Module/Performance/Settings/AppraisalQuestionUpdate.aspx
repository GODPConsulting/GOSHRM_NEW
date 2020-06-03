<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AppraisalQuestionUpdate.aspx.vb"
    Inherits="GOSHRM.AppraisalQuestionUpdate" EnableEventValidation="false" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<%--<meta name="viewport" content="width=device-width, initial-scale=1">
<link rel="stylesheet" href="http://www.w3schools.com/lib/w3.css">--%>
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
            width: 234px;
            color: #FF0000;
        }
        .style5
        {
            font-family: Candara;
            font-size: medium;
            width: 234px;
        }
        .style6
        {
            width: 234px;
        }
        .style8
        {
            width: 496px;
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
<body onunload="window.opener.location=window.opener.location;" style="height: 317px;
    margin-bottom: 15px;">
    <form id="form1" runat="server">
    <script type="text/javascript">
        function closeme() {
            window.close();
        }
        window.onblur = closeme;
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
        rel="Stylesheet" type="text/css" />
    <table>
        <tr>
            <td class="style1" colspan="2" style="background-color: #1BA691">
                Appraisal Question
            </td>
        </tr>
        <tr>
            <td class="style6">
                <asp:TextBox ID="txtactive" runat="server" style="text-align: right" 
                    Width="20%" Height="16px" Visible="False" Font-Names="Verdana" Font-Size="12px">33.33</asp:TextBox>
            </td>
            <td class="style8">
                <asp:TextBox ID="txtid" runat="server" Width="56px" Style="font-size: medium; font-family: Candara"
                    Font-Names="Candara" Height="16px" Visible="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style5" valign="top">
                <asp:Label ID="Label3" runat="server" Font-Names="Verdana" Font-Bold="True" ForeColor="#666666"
                    Text="Question Category" Font-Size="12px"></asp:Label>
            </td>
            <td class="style8">
                <telerik:RadComboBox ID="cboCategory" Runat="server" Width="100%"  ForeColor="#666666"
                    Font-Names="Verdana" Font-Size="12px">
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td class="style5" valign="top">
                <asp:Label ID="Label1" runat="server" Font-Names="Verdana" Text="Question" Font-Bold="True" ForeColor="#666666"
                    Font-Size="12px"></asp:Label>
            </td>
            <td class="style8">
                <asp:TextBox ID="txtQuestion" runat="server" BorderColor="#CCCCCC"  ForeColor="#666666"
                    BorderWidth="1px" Height="100px" TextMode="MultiLine" Width="100%" 
                    Font-Names="Verdana" Font-Size="12px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style5" valign="top">
                <asp:Label ID="Label2" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                    Text="Apply to Appraisal (Active)"></asp:Label>
            </td>
            <td class="style8">
                <asp:RadioButtonList ID="rdoActive" runat="server" Font-Names="Verdana" Font-Size="12px" AutoPostBack="True"  ForeColor="#666666"
                    RepeatDirection="Horizontal">
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td class="style6">
                &nbsp;</td>
            <td class="style8">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style6">
                <asp:Label ID="Label5" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666" Text="Created By" Font-Italic="True"
                    ></asp:Label>
            </td>
            <td class="style8">
                <asp:Label ID="lblcreatedby" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Italic="True"  ForeColor="#666666"
                    Width="100%"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style6">
                <asp:Label ID="Label7" runat="server" Font-Names="Verdana" Font-Size="12px" Text="Created On" Font-Italic="True" Font-Bold="True" ForeColor="#666666"
                    ></asp:Label>
            </td>
            <td class="style8">
                <asp:Label ID="lblcreatedon" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Italic="True"  ForeColor="#666666"
                     Width="100%"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style6">
                <asp:Label ID="Label6" runat="server" Font-Names="Verdana" Font-Size="12px" Text="Updated By" Font-Italic="True" Font-Bold="True" ForeColor="#666666"
                    ></asp:Label>
            </td>
            <td class="style8">
                <asp:Label ID="lblupdatedby" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Italic="True" ForeColor="#666666"
                     Width="100%"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style6">
                <asp:Label ID="Label8" runat="server" Font-Names="Verdana" Font-Size="12px" Text="Updated On" Font-Italic="True" Font-Bold="True" ForeColor="#666666"
                    ></asp:Label>
            </td>
            <td class="style8">
                <asp:Label ID="lblupdatedon" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Italic="True"  ForeColor="#666666"
                    Width="100%"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style2" style="border-bottom-style: solid; border-bottom-width: thin;
                border-bottom-color: #C0C0C0">
                * Required Field
            </td>
            <td style="border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #C0C0C0"
                class="style8">
            </td>
        </tr>
        <tr>
            <td class="style6">
                &nbsp;</td>
            <td class="style8">
            </td>
        </tr>
        <tr>
            <td class="style6">
            </td>
            <td class="style8">
                <asp:Label ID="lblstatus" runat="server" Font-Bold="True" Font-Names="Candara" Font-Size="12px"
                    ForeColor="#FF3300"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style6">
                <asp:Button ID="btnAdd" runat="server" Text="Save" BackColor="#1BA691" ForeColor="White"
                    Width="120px" Height="20px" BorderStyle="None" Font-Names="Verdana" Font-Size="11px"/>
            </td>
            <td class="style8">
                <asp:Button ID="btnCancel" runat="server" Text="Close" BackColor="#FF3300" ForeColor="White"
                    Width="120px" Height="20px" BorderStyle="None" Font-Names="Verdana" Font-Size="11px"/>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
