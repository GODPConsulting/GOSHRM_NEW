<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="PayAdjustment.aspx.vb"
    Inherits="GOSHRM.PayAdjustment" EnableEventValidation="false" Debug="true" %>

<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%@ register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
    <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
    <html xmlns="http://www.w3.org/1999/xhtml">
    <script type="text/javascript" language="javascript">
        //    Grid View Check box
        function CheckAllEmp(Checkbox) {
            var GridVwHeaderChckbox = document.getElementById("<%=GridVwHeaderChckbox.ClientID %>");
            for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }
    </script>
    <title>Staff Loans and Advances</title>
    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to delete data?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
    <style type="text/css">
        body
        {
            font-family: Arial;
            font-size: 10pt;
        }
        td
        {
            cursor: pointer;
        }
        .hover_row
        {
            background-color: #A1DCF2;
        }
        .style29
        {
            width: 178px;
        }
        .button
        {
            background-color: #008CBA; /* Green */
            border: none;
            color: white;
            padding: 15px 32px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 16px;
            margin: 4px 2px;
            cursor: pointer;
        }
        .RadComboBox_Default{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.RadComboBox{text-align:left;display:inline-block;vertical-align:middle;white-space:nowrap;*display:inline;*zoom:1}.RadComboBox_Default .rcbReadOnly .rcbInput{color:#333}.RadComboBox .rcbReadOnly .rcbInput{cursor:default}.RadComboBox_Default .rcbInput{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif;line-height:16px}.RadComboBox .rcbInput{margin:0;padding:2px 0 1px;height:auto;width:100%;border-width:0;outline:0;color:inherit;background-color:transparent;vertical-align:top}
        .style106
        {
            width: 373px;
        }
    </style>
    <body>
        <form id="form1">
       
        <div >
            <table width="100%">
                <tr>
                    <td class="style22">
                        <asp:Label ID="lblView" runat="server" Font-Names="Candara" Font-Size="Medium" Width="100%"
                            Style="font-weight: 700; color: #FF6600"></asp:Label>
                    </td>
                </tr>
            </table>
            <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        </div>
        <div>
            <div>
<%--                <ajaxToolkit:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%"
                    Font-Names="Candara" Font-Size="Small" Height="100%" ScrollBars="Auto">
                    <ajaxToolkit:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel1">
                        <HeaderTemplate>
                            Staff Loans and Advances
                        </HeaderTemplate>
                        <ContentTemplate>--%>
                            <div>
                                <div>
                                    <table class="style21">
                                        <tr>
                                            <td class="style106">
                                                <asp:Label ID="Label1" runat="server" Text="Date" Font-Names="Verdana" 
                                                    Font-Size="11px" Font-Bold="True" ForeColor="#666666"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:raddatepicker ID="dateFrom" runat="server" Font-Names="Verdana" 
                                                    Font-Size="11px" Width="160px" Culture="en-US" ResolvedRenderMode="Classic" 
                                                    AutoPostBack="True" forecolor="#666666">
                                                    <Calendar EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;" UseColumnHeadersAsSelectors="False"
                                                        UseRowHeadersAsSelectors="False">
                                                    </Calendar>
                                                    <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelWidth="40%"
                                                        Width="" AutoPostBack="True">
                                                        <EmptyMessageStyle Resize="None" />
                                                        <ReadOnlyStyle Resize="None" />
                                                        <FocusedStyle Resize="None" />
                                                        <DisabledStyle Resize="None" />
                                                        <InvalidStyle Resize="None" />
                                                        <HoveredStyle Resize="None" />
                                                        <EnabledStyle Resize="None" />
                                                    </DateInput>
                                                    <DatePopupButton HoverImageUrl="" ImageUrl="" CssClass="" />
                                                </telerik:raddatepicker>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDate" runat="server" Width="100px" Font-Names="Verdana" 
                                                    Font-Size="11px" Font-Bold="True" ForeColor="#666666"></asp:Label>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                               <asp:TextBox ID="txtsearch" runat="server" Height="20px" Width="251px" BorderColor="#CCCCCC"
                                                BorderWidth="1px" TextMode="Search" Font-Names="Verdana" Font-Size="11px" 
                                                    ForeColor="#666666"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnFind" runat="server" BackColor="#1BA691" BorderStyle="None" ForeColor="White"
                                                Height="20px" Text="View" Width="100px" Font-Names="Verdana" 
                                                Font-Size="11px" />
                                            </td>
                                            <td>
                                            </td>
                                             <td class="style29">
                                            <asp:Button ID="btnApply" runat="server" BackColor="#1BA691" BorderStyle="None" ForeColor="White"
                                                Height="20px" Text="Add Payment" Width="100px" Font-Names="Verdana" 
                                                Font-Size="11px" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnDelete" runat="server" BackColor="#FF3300" BorderStyle="None"
                                                ForeColor="White" Height="20px" OnClick="Delete" OnClientClick="Confirm()" Text="Delete"
                                                Width="100px" Font-Names="Verdana" Font-Size="11px" />
                                        </td>
                                        </tr>
                                    </table>
                                </div>                               
                            </div>
                            
                            <div>
                                <asp:GridView ID="GridVwHeaderChckbox" runat="server" AllowPaging="True" AllowSorting="True"
                                    BorderStyle="Solid" DataKeyNames="id" Font-Names="Verdana" Font-Size="12px"
                                    Height="50px" OnRowDataBound="OnRowDataBound" OnSorting="SortRecords" PageSize="1000"
                                    ToolTip="click row to select record" Width="90%" ShowHeaderWhenEmpty="True"
                                    AutoGenerateColumns="False" GridLines="Vertical" ForeColor="#666666" 
                                    BorderWidth="1px" BorderColor="#CCCCCC">
                                    <AlternatingRowStyle BackColor="#CCCCCC" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="CheckAllEmp(this);" />
                                            </HeaderTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkEmp" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Rows" HeaderText="Rows" >
                                        <ItemStyle Width="5%" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Employee">
                                            <ItemTemplate>
                                                <a href="#" onclick='openWindow("<%# Eval("id") %>");'>
                                                    <%# Eval("Employee")%></a>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="True" HorizontalAlign="Left" Width="20%" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Title" HeaderText="Adjustment Title" >
                                        <ItemStyle Width="25%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="AdjType" HeaderText="Adjustment Type" >
                                        <ItemStyle Width="15%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Amount" HeaderText="Amount" >
                                        <ItemStyle HorizontalAlign="Right" Width="20%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Paydate" HeaderText="Payment Date" >

                                 
                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                        </asp:BoundField>

                                 
                                    </Columns>
                                    <HeaderStyle BackColor="#1BA691" ForeColor="White" HorizontalAlign="Center" />
                                </asp:GridView>
                                <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js" type="text/javascript">

                                </script>
                                <script type="text/javascript">

                                    $(function () {
                                        $("[id*=GridVwHeaderChckbox] td").hover(function () {
                                            $("td", $(this).closest("tr")).addClass("hover_row");
                                        }, function () {
                                            $("td", $(this).closest("tr")).removeClass("hover_row");
                                        })
                                    })
                                </script>
                                <script type="text/javascript">
                                    function openWindow(code) {
                                        window.open("LoanRequest.aspx?id=" + code, "open_window", "width=600,height=700");
                                    }
                                </script>         
                            </div>
                            
<%--                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                </ajaxToolkit:TabContainer>--%>
            </div>
        </div>
        </form>
    </body>
    </html>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .style21
        {
            width: 275px;
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
