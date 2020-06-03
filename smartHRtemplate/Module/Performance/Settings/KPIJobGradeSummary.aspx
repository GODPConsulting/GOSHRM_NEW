<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="KPIJobGradeSummary.aspx.vb"
    Inherits="GOSHRM.KPIJobGradeSummary" EnableEventValidation="false" Debug="true" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
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
            width: 231px;
        }
        .style6
        {
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
    <form>
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
        rel="Stylesheet" type="text/css" />
    <table width="100%">
      
        <tr>
            <td class="style6" >
                <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
                <asp:GridView ID="GridVwHeaderChckbox" runat="server" AllowSorting="True" OnSorting="SortRecords"
                    BorderStyle="Solid" Font-Names="Verdana" AllowPaging="False" PageSize="30" DataKeyNames="JobGrade"
                    OnRowDataBound="OnRowDataBound" Width="100%" Height="50px" ToolTip="click row to select record"
                    Font-Size="12px" AutoGenerateColumns="False" GridLines="Vertical" 
                    ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC" 
                    ShowHeaderWhenEmpty="True" EmptyDataText="No data to display" CssClass="table table-condensed">
                    <RowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="Rows" ItemStyle-Width="5px" HeaderText="Rows" SortExpression="Rows">
                            <ItemStyle Width="5px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="JobGrade" ItemStyle-Width="140px" HeaderText="Job Grade" SortExpression="JobGrade" >
                            <ItemStyle Width="140px"></ItemStyle>
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="Total" ItemStyle-Width="50px" HeaderText="Total (%)" SortExpression ="Total"
                            ItemStyle-HorizontalAlign="Right">
                            <ItemStyle HorizontalAlign="Right" Width="50px"></ItemStyle>
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle BackColor="White" ForeColor="#1BA691" HorizontalAlign="center" />
                </asp:GridView>
            </td>
        </tr>
        </table>
        <br />
        <table>
        <tr>
            <td valign="top" class="style6">
                
                <%--<asp:Button ID="btnCancel" runat="server" Text="Close" BackColor="#FF3300" ForeColor="White"
                    Width="100px" Height="20px" BorderStyle="None" Font-Names="Verdana" 
                    Font-Size="11px" />--%>
                    <button id="Button1" runat="server" onserverclick="btnCancel_Click" type="submit" style="width: 150px"
                                class="btn btn-primary btn-info">
                                << Back</button>
            </td>
            <td>
                &nbsp;
                </td>
        </tr>
        </table>
    </form>
</body>
</html>
</asp:Content>