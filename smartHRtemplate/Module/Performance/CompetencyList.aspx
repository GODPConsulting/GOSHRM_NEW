<%--<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CompetencyList.aspx.vb"
    Inherits="GOSHRM.CompetencyList" EnableEventValidation="false" %>--%>
<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="CompetencyList.aspx.vb"
    Inherits="GOSHRM.CompetencyList" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<html xmlns="http://www.w3.org/1999/xhtml">
<%--<script type="text/javascript" language="javascript">
    //    Grid View Check box
//    function CheckAllEmp(Checkbox) {
//        var GridVwHeaderChckbox = document.getElementById("<%=GridVwHeaderChckbox.ClientID %>");
//        for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
//            GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
//        }
//    }
</script>--%>
<title></title>
<link rel="icon" type="image/png" href="../../../images/goshrm.png">
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
    .RadPicker
    {
        vertical-align: middle;
    }
    .rdfd_
    {
        position: absolute;
    }
    .RadPicker .rcTable
    {
        table-layout: auto;
    }
    .RadPicker .RadInput
    {
        vertical-align: baseline;
    }
    .RadInput_Default
    {
        font: 12px "segoe ui" ,arial,sans-serif;
    }
    .RadInput
    {
        vertical-align: middle;
    }
    .RadInput .riTextBox
    {
        height: 17px;
    }
    .RadPicker_Default .rcCalPopup
    {
        background-position: 0 0;
    }
    .RadPicker_Default .rcCalPopup
    {
        background-image: url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Calendar.sprite.gif');
    }
    .RadPicker .rcCalPopup
    {
        display: block;
        overflow: hidden;
        width: 22px;
        height: 22px;
        background-color: transparent;
        background-repeat: no-repeat;
        text-indent: -2222px;
        text-align: center;
        -webkit-box-sizing: content-box;
        -moz-box-sizing: content-box;
        box-sizing: content-box;
    }
</style>
<body style="background: White">
   <%-- <body style="height: 317px">--%>
        <form id="form1" >
      
        <div>
        </div>
          <div>
            <table width="100%">
                <tr>
                    <td >
                        <asp:Label ID="lblView" runat="server" Font-Names="Candara" Font-Size="Medium" Width="100%"
                            Style="font-weight: 700; color: #FF6600"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>

        <div>
            <asp:Label ID="lblHeader" runat="server" Font-Names="Candara" Font-Size="Medium"
                Style="color: #333333; font-weight: 700; text-align: center;" Width="100%"></asp:Label>
        </div>
        <div>
            <asp:Button ID="btnAdd" runat="server" Text="Add" BackColor="#1BA691" ForeColor="White"
                Width="80px" Height="20px" BorderStyle="None" Font-Names="Verdana" 
                Font-Size="11px" />
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="Delete" OnClientClick="Confirm()"
                BackColor="Red" ForeColor="White" Width="80px" Height="20px" 
                BorderStyle="None" Font-Names="Verdana" Font-Size="11px" />
            &nbsp;&nbsp;&nbsp;
            <telerik:RadTextBox ID="txtsearch" runat="server" DisplayText="Search" Width="20%">
            </telerik:RadTextBox>
            &nbsp;<asp:Button ID="btnFind" runat="server" Text="Search" BackColor="#1BA691" ForeColor="White"
                Width="80px" Height="20px" BorderStyle="None" Font-Names="Verdana" 
                Font-Size="11px" />
        </div>
        <div style="height: 10px">
            <asp:Label ID="lblHang" runat="server" Style="color: #FFFFFF"></asp:Label>
        </div>
        <div>
            <telerik:RadGrid RenderMode="Lightweight" ID="gridCompetency" runat="server" PageSize="100"
                AllowSorting="True" AllowMultiRowSelection="True" AllowPaging="True" ShowGroupPanel="True"
                AutoGenerateColumns="False" GridLines="Vertical" ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC" GroupPanelPosition="Top" ResolvedRenderMode="Classic"
                DataKeyNames="ID" >
                <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
                <MasterTableView Width="100%">
                    <GroupByExpressions>
                        <telerik:GridGroupByExpression>
                            <SelectFields>
                                <telerik:GridGroupByField FieldAlias="" FieldName="GroupName"></telerik:GridGroupByField>
                            </SelectFields>
                            <GroupByFields>
                                <telerik:GridGroupByField FieldName="GroupName" SortOrder="Ascending"></telerik:GridGroupByField>
                            </GroupByFields>
                        </telerik:GridGroupByExpression>
                    </GroupByExpressions>
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" ItemStyle-Width="5px" >
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" OnCheckedChanged="ToggleRowSelection"
                                    AutoPostBack="True" />
                            </ItemTemplate>
                            <HeaderTemplate>
                                <asp:CheckBox ID="headerChkbox" runat="server" OnCheckedChanged="ToggleSelectedState"
                                    AutoPostBack="True" />
                            </HeaderTemplate>
                        </telerik:GridTemplateColumn>
                         <telerik:GridBoundColumn SortExpression="ID" HeaderText="ID" HeaderButtonType="TextButton"
                            DataField="ID" ItemStyle-Width="5px" >
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn UniqueName="strName" HeaderText="Name" DataField="Name" ItemStyle-Width="150px" >
                            <ItemTemplate>
                            <a href="#" onclick='openWindow("<%# Eval("Name") %>");'>
                                <%# DataBinder.Eval(Container.DataItem, "Name")%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                       <%-- <telerik:GridBoundColumn SortExpression="Name" HeaderText="Name" HeaderButtonType="TextButton"
                            DataField="Name">
                        </telerik:GridBoundColumn>--%>
                        <telerik:GridBoundColumn SortExpression="Description" HeaderText="Description" HeaderButtonType="TextButton"
                            DataField="Description" ItemStyle-Width="400px" >
                        </telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
                <ClientSettings ReorderColumnsOnClient="True" AllowDragToGroup="True" AllowColumnsReorder="True">
                    <Selecting AllowRowSelect="True"></Selecting>
                    <Resizing AllowRowResize="True" AllowColumnResize="True" EnableRealTimeResize="True"
                        ResizeGridOnColumnResize="False"></Resizing>
                </ClientSettings>
                <GroupingSettings ShowUnGroupButton="true"></GroupingSettings>
                <FilterMenu RenderMode="Lightweight">
                </FilterMenu>
                <HeaderContextMenu RenderMode="Lightweight">
                </HeaderContextMenu>
            </telerik:RadGrid>
             <script type="text/javascript">
                 function openWindow(code) {
                     window.open("CompetencyUpdates.aspx?id=" + code, "open_window", "width=800,height=800");
                 }
                </script>
        </div>
        <div style="height: 163px">
            <div>
                <asp:Label ID="lblHang0" runat="server" Style="color: #FFFFFF"></asp:Label>
            </div>
            <div style="height: 6px">
                <table>
                    <tr>
                        <td class="style29">
                            <asp:Button ID="btnClose" runat="server" BackColor="#0066FF" BorderStyle="None" ForeColor="White"
                                Height="20px" Text="Close" Width="100px" Font-Names="Verdana" 
                                Font-Size="11px" Visible="False" />
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
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
        </style>
</asp:Content>