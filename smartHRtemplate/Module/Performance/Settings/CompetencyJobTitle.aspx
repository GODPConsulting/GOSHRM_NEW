﻿<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="CompetencyJobTitle.aspx.vb"
    Inherits="GOSHRM.CompetencyJobTitle" EnableEventValidation="false" Debug="true" %>

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
            width: 275px;
            font-size: x-large;
            font-family: Candara;
        }
    </style>
    <body style="background: #C9C9C9">
        <form id="form1">
        <div>
            <table width="100%">
                <tr>
                    <td >
                        <asp:Label ID="lblView" runat="server" Font-Names="Candara" Font-Size="Medium" Width="100%"
                            Style="font-weight: 700; color: #FF6600"></asp:Label>
                    </td>
                </tr>
            </table>
            <table class="style21">
                <tr>
                    <td class="style22">
                        <asp:TextBox ID="txtsearch" runat="server" Width="251px" Height="20px" BorderColor="#CCCCCC"
                            BorderWidth="1px" TextMode="Search"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnFind" runat="server" Text="Search" BackColor="#1BA691" ForeColor="White"
                            Width="80px" Height="20px" BorderStyle="None" Font-Names="Verdana" Font-Size="11px"/>
                    </td>
                </tr>
            </table>
        </div>
     
        <div style="border: thin solid #C0C0C0">
            <table class="style21">
                <tr>
                    
                    <td class="style25">
                        <asp:Button ID="btnAdd" runat="server" Text="Add" BackColor="#1BA691" ForeColor="White"
                            Width="80px" Height="20px" BorderStyle="None" Font-Names="Verdana" Font-Size="11px"/>
                    </td>
                    <td class="style26">
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="Delete" OnClientClick="Confirm()"
                            BackColor="#1BA691" ForeColor="White" Width="80px" Height="20px" BorderStyle="None" Font-Names="Verdana" Font-Size="11px"/>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
            <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        </div>
       
        <div style="height: 163px">
            <div>
                <asp:GridView ID="GridVwHeaderChckbox" runat="server" OnSorting="SortRecords" AllowSorting="True"
                    BorderStyle="Solid" Font-Names="Arial" AllowPaging="True" PageSize="100" DataKeyNames="JobTitle"
                    OnRowDataBound="OnRowDataBound" Width="80%" Height="50px" ToolTip="click row to select record"
                    Font-Size="11px" AutoGenerateColumns="False" GridLines="Vertical" ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC" ShowHeaderWhenEmpty="True">
                    <AlternatingRowStyle BackColor="#CCCCCC" />
                    <Columns>
                        <asp:TemplateField ItemStyle-Width="1%">
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="CheckAllEmp(this);" />
                            </HeaderTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemTemplate>
                                <asp:CheckBox ID="chkEmp" runat="server"></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Rows" ItemStyle-Width="5%" HeaderText="Rows" />
                        <asp:TemplateField HeaderText="Job Title" ItemStyle-Width="34%" 
                            ItemStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <a href="#" onclick='openWindow("<%# Eval("JobTitle") %>");'>
                                    <%# Eval("JobTitle")%></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Competencies" ItemStyle-Width="60%" HeaderText="Competencies" />        
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
                <script type="text/javascript">
                    function openWindow(code) {
                        window.open("CompetencyJobTitleUpdate.aspx?id=" + code, "open_window", "width=600,height=500");
                    }
                </script>
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
