﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="EmployeeShiftHistory.aspx.vb" Inherits="GOSHRM.EmployeeShiftHistory"
    EnableEventValidation="false" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<html xmlns="http://www.w3.org/1999/xhtml">

<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

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

    <script type="text/javascript" language="javascript">
    //    Grid View Check box
    function CheckAllEmp(Checkbox) {
        var GridVwHeaderChckbox = document.getElementById("<%=gridTrainers.ClientID %>");
        for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
            GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
        }
    }
    </script>

 <script type="text/javascript">
     function ConfirmSend() {
         var send_value = document.createElement("INPUT");
         send_value.type = "hidden";
         send_value.name = "send_value";
         if (confirm("Send mail to shortlisted candidates")) {
             send_value.value = "Yes";
         } else {
             send_value.value = "No";
         }
         document.forms[0].appendChild(send_value);
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
    <body onunload="window.opener.location=window.opener.location;" style="height: 317px">
        <form id="form1" runat="server">
        <div>
            <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
            </telerik:RadScriptManager>
        </div>
        <div style="border: thin solid #C0C0C0">
            <asp:Label ID="lblHeader" runat="server" BackColor="#6699FF" Font-Size="Medium" Style="color: #FFFFFF;
                font-weight: 700; font-family: Candara" Width="100%"></asp:Label>
        </div>
        <div style="border: thin solid #C0C0C0">
         <asp:Label ID="lblStatus" runat="server" Font-Bold="True" Font-Size="12px" 
                    ForeColor="Red" Width="100%" Font-Names="Verdana"></asp:Label>
        </div>
          
        <div>
            <asp:GridView ID="gridTrainers" runat="server" AllowPaging="True" AllowSorting="True"
                BorderStyle="Solid" Font-Names="Verdana" Font-Size="12px" Height="50px" Width="100%"
                ShowHeaderWhenEmpty="True" EmptyDataText="No data to display" AutoGenerateColumns="False" GridLines="Vertical" ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC" PageSize="100" 
                DataKeyNames="id">
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
                    <asp:BoundField DataField="Row" ItemStyle-Width="5px" HeaderText="Row" />                  
                     <asp:BoundField DataField="shiftname" ItemStyle-Width="100px" HeaderText="Shift" />
                    <asp:BoundField DataField="datefrom" ItemStyle-Width="70px" HeaderText="Date From" ItemStyle-HorizontalAlign="Center" />  
                    <asp:BoundField DataField="dateto" ItemStyle-Width="70px" HeaderText="Date To" ItemStyle-HorizontalAlign="Center" />
                    <asp:TemplateField HeaderText="" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Bold="true" >
                        <ItemTemplate>
                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/Employee/TimeManagement/EmployeeAttendance.aspx?empid={0}&start={1}&to={2}",
                     HttpUtility.UrlEncode(Eval("empid").ToString()), HttpUtility.UrlEncode(Eval("datefrom").ToString()),HttpUtility.UrlEncode(Eval("dateto").ToString())) %>'
                                Text='Attendance' />
                        </ItemTemplate>
                    </asp:TemplateField>
                                                    
                </Columns>
                <HeaderStyle BackColor="#1BA691" ForeColor="White" HorizontalAlign="center" />
                <RowStyle HorizontalAlign="Left" />
            </asp:GridView>

        </div>
        <div style="height: 163px">
         <div style="height: 10px">
         </div>
            <div style="height: 50px">
                <asp:Button runat="server" Text="Delete" BackColor="#FF3300" BorderStyle="None" 
                    Font-Names="Verdana" Font-Size="11px" ForeColor="White" Height="20px" 
                    Width="100px" ID="btnDelete" onclientclick="Confirm()"></asp:Button>
            </div>
            <div style="border: thin solid #C0C0C0">
                <asp:Label ID="Label3" runat="server" Font-Size="Smaller"></asp:Label>
            </div>
            <div style="height: 20px">
                
            </div>
            <div>
            </div>
         
        </div>
        </form>
    </body>
</html>
