<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.vb"
    Inherits="GOSHRM.ErrorPage" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Charting" TagPrefix="telerik" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <title>Error Page</title>
    <meta content='width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no'
        name='viewport'>
    <body>
        <form id="form1">
        <div>
            <asp:Label ID="Label1" runat="server" Font-Names="Verdana" Font-Size="16px" Style="text-align: center;
                font-size: large; font-weight: 700;" Text="Ooops! There seems to be an error, if error continues please contact support"
                Width="100%"></asp:Label>
                <div>
                </div> 
                <asp:Label ID="lblErr" runat="server" Font-Names="Verdana" Font-Size="10px" Style="text-align: center;
                font-size: large; "
                Width="100%"></asp:Label>
        </div>
        <div>
            <table width="100%">
                <tr style="width: 100%">
                    <td style="width: 33%">
                    </td>
                    <td style="width: 34%">
                        <asp:Image ID="Image2" runat="server" Height="398px" ImageUrl="~/images/maintenance.png"
                            Width="99%" />
                    </td>
                    <td style="width: 33%">
                    </td>
                </tr>
            </table>
        </div>
        </form>
    </body>
    </html>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
</asp:Content>
