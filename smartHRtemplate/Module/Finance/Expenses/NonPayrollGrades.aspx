<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="NonPayrollGrades.aspx.vb"
    Inherits="GOSHRM.NonPayrollGrades" EnableEventValidation="false" Debug="true" %>

<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
    <title>Roles</title>
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
        .style25
        {
            width: 134px;
        }
        .style26
        {
            width: 22px;
        }
        .style27
        {
            width: 35px;
        }
        .style28
        {
        }
    </style>
    <body>
        <div>
            <table width="100%">
                <tr>
                    <td class="style22">
                        <asp:Label ID="lblView" runat="server" Font-Names="Candara" Font-Size="Medium" Width="100%"
                            Style="font-weight: 700; color: #FF6600"></asp:Label>
                    </td>
                </tr>
           </table>
           <table class="style21">
                <tr>
                    <td class="style28">
                        <asp:TextBox ID="txtsearch" runat="server" Width="251px" Height="20px" BorderColor="#CCCCCC"
                            BorderWidth="1px" TextMode="Search"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnFind" runat="server" Text="Search" BackColor="#1BA691" ForeColor="White"
                            Width="80px" Height="20px" BorderStyle="None" Font-Names="Verdana" 
                            Font-Size="11px"/>
                    </td>
                         <td class="style25" >
                        <asp:Button ID="btnAdd" runat="server" Text="Add" BackColor="#1BA691" ForeColor="White"
                            Width="100px" Height="20px" BorderStyle="None" Font-Names="Verdana" 
                            Font-Size="11px" />
                    </td>
                    <td></td>
                    <td class="style26">
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="Delete" OnClientClick="Confirm()"
                            BackColor="#FF3300" ForeColor="White" Width="100px" Height="20px" 
                            BorderStyle="None" Font-Names="Verdana" 
                            Font-Size="11px"/>
                    </td>
                    <td></td>
                    <td>
                        
                        <asp:Button ID="Button1" runat="server" Text="Export" 
                            BackColor="#FF9933" ForeColor="White"
                            Width="100px" Height="20px" BorderStyle="None" Font-Names="Verdana" 
                            Font-Size="11px"/>
                        
                    </td>
                    <td>
                    </td>
                    <td>
                                    <asp:FileUpload ID="FileUpload1" runat="server" Font-Names="Verdana" 
                                        Font-Size="11px" Width="300px" />
                        
                    </td>
                    <td>
                                    <asp:Button ID="btnUpload" runat="server" BackColor="#00CC99" BorderStyle="None"
                                        ForeColor="White" Height="20px" Text="Upload File" 
                             Width="100px" ToolTip="CSV File: Job Grade, Expense Item, Amount" 
                                        Font-Names="Verdana" Font-Size="11px" />
                    </td>
                </tr>
            </table>
        </div>
           
 
     
        <form id="form1" >
        <div>
            <div>
                <asp:GridView ID="GridVwHeaderChckbox" runat="server" OnSorting="SortRecords" AllowSorting="True"
                    BorderStyle="Solid" Font-Names="Verdana" AllowPaging="True" PageSize="100" DataKeyNames="id"
                    OnRowDataBound="OnRowDataBound" Width="80%" Height="50px" ToolTip="click row to select record"
                    Font-Size="11px" ShowHeaderWhenEmpty="True" EmptyDataText="No data to display" AutoGenerateColumns="False" GridLines="Vertical" ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC">
                    <AlternatingRowStyle BackColor="#CCCCCC" />
                    <Columns>
                        <asp:TemplateField ItemStyle-Width="5px">
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="CheckAllEmp(this);" />
                            </HeaderTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemTemplate>
                                <asp:CheckBox ID="chkEmp" runat="server"></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Rows" ItemStyle-Width="5px" HeaderText="Rows" />
                          <asp:TemplateField HeaderText="Job Grade" ItemStyle-Width="150px" ItemStyle-Font-Bold="true" >
                        <ItemTemplate>
                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/Finance/Expenses/NonPayGradesUpdate.aspx?id={0}",
                     HttpUtility.UrlEncode(Eval("id").ToString())) %>'
                        onclick="window.open (this.href, 'popupwindow',  'width=600,height=00,scrollbars,resizable'); return false;"  Text='<%# Eval("grade")%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                        <asp:TemplateField HeaderText="Expense Item" ItemStyle-Width="150px" ItemStyle-Font-Bold="true" >
                        <ItemTemplate>
                            <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%# string.Format("~/Module/Finance/Expenses/NonPayGradesUpdate.aspx?id={0}",
                     HttpUtility.UrlEncode(Eval("id").ToString())) %>'
                        onclick="window.open (this.href, 'popupwindow',  'width=600,height=00,scrollbars,resizable'); return false;"  Text='<%# Eval("salaryitem")%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                        <asp:BoundField DataField="itemtype" ItemStyle-Width="100px" HeaderText="Item Type" />
                        <asp:BoundField DataField="amount" ItemStyle-Width="80px" HeaderText="Amount" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n}"  />                                        
                    </Columns>
                    <HeaderStyle BackColor="#1BA691" ForeColor="White" HorizontalAlign="center" />
                </asp:GridView>
                <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
                <script type="text/javascript">
                    $(function () {
                        $("[id*=GridVwHeaderChckbox] td").hover(function () {
                            $("td", $(this).closest("tr")).addClass("hover_row");
                        }, function () {
                            $("td", $(this).closest("tr")).removeClass("hover_row");
                        })
                    })
                </script>
            </div>
             <div >
        </div>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
        </div>
        </form>
    </body>
    </html>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .style21
        {
            width: 100%;
        }
        .style22
        {
        }
        </style>
</asp:Content>
