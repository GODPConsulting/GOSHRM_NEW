<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="EmployeeAttendance.aspx.vb" Inherits="GOSHRM.EmployeeAttendance"
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
   <%-- <body onunload="window.opener.location=window.opener.location;" style="height: 317px">--%>
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
         <asp:Label ID="lblStatus" runat="server" Font-Bold="True" Font-Size="11px" 
                    ForeColor="Red" Width="100%" Font-Names="Verdana"></asp:Label>
        </div>
       <div>
        <table>
            <tr>
                <td>
                     <asp:CheckBox ID="chkAbsent" runat="server" AutoPostBack="True" 
               Font-Names="Verdana" Font-Size="11px" Text="Include Days Employee was absent" 
                         ForeColor="#666666" />
                </td>
                 <td>
                    <asp:Button runat="server" Text="&lt;&lt; Back" BackColor="#999966" BorderStyle="None" 
                    Font-Names="Verdana" Font-Size="11px" ForeColor="White" Height="20px" 
                    Width="100px" ID="btnDelete"></asp:Button>
                </td>
                <td>
                    <asp:Button ID="btnRequest" runat="server" BackColor="#1BA691" BorderStyle="None" 
                                                    Font-Names="Verdana" Font-Size="11px" ForeColor="White" Height="20px" 
                                                    Text=" Request Overtime Pay" Width="150px" />
                </td>
            </tr>
        </table>
          
       </div>
        <div>
            <asp:GridView ID="gridTrainers" runat="server" AllowPaging="True" AllowSorting="True"
                BorderStyle="Solid" Font-Names="Verdana" Font-Size="11px" Height="50px" Width="100%"
                ShowHeaderWhenEmpty="True" EmptyDataText="No data to display" AutoGenerateColumns="False" GridLines="Vertical" ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC" PageSize="100" 
                DataKeyNames="id">
                <AlternatingRowStyle BackColor="#CCCCCC" />
                <Columns>                                
                    <asp:BoundField DataField="Rows" ItemStyle-Width="5px" HeaderText="Row" />     
                     <asp:BoundField DataField="DateNames" ItemStyle-Width="100px" HeaderText="Date" />             
                     <asp:BoundField DataField="shiftname" ItemStyle-Width="50px" HeaderText="Shift" />
                     <asp:BoundField DataField="shifts" ItemStyle-Width="100px" HeaderText="Shift Time" ItemStyle-HorizontalAlign="Center" />
                     <asp:BoundField DataField="duration" ItemStyle-Width="30px" HeaderText="Duration (Hr)" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="checkindate" ItemStyle-Width="50px" HeaderText="Date Clockin" ItemStyle-HorizontalAlign="Center" />  
                    <asp:BoundField DataField="checkoutdate" ItemStyle-Width="50px" HeaderText="Date Clockout" ItemStyle-HorizontalAlign="Center" /> 
                    <asp:BoundField DataField="checkintime" ItemStyle-Width="50px" HeaderText="Time Clockin" ItemStyle-HorizontalAlign="Center" />  
                    <asp:BoundField DataField="checkouttime" ItemStyle-Width="50px" HeaderText="Time Clockout" ItemStyle-HorizontalAlign="Center" /> 
                    <asp:BoundField DataField="actualduration" ItemStyle-Width="30px" HeaderText="Actual Duration (Hr)" ItemStyle-HorizontalAlign="Right" />                                                       
                </Columns>
                <HeaderStyle BackColor="#1BA691" ForeColor="White" HorizontalAlign="center" />
                <RowStyle HorizontalAlign="Left" />
            </asp:GridView>
        </div>
        </form>
    </body>
</html>
