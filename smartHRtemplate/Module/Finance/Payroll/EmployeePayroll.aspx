<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="EmployeePayroll.aspx.vb"
    Inherits="GOSHRM.EmployeePayroll" EnableEventValidation="false" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<title>Employee Salary</title>
<link rel="icon" type="image/png" href="../../../images/goshrm.png">
<script type="text/javascript">
    function ConfirmGenerate() {
        var confirm_gen = document.createElement("INPUT");
        confirm_gen.type = "hidden";
        confirm_gen.name = "confirm_gen";

        if (confirm("Refresh Salary Items for Employee, existing items won't be reset?")) {
            confirm_gen.value = "Yes";
        } else {
            confirm_gen.value = "No";
        }
        document.forms[0].appendChild(confirm_gen);
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
        font-size: xx-small;
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
.RadComboBox{text-align:left;display:inline-block;vertical-align:middle;white-space:nowrap;*display:inline;*zoom:1}
        .RadComboBox_Default{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.RadComboBox .rcbReadOnly .rcbInput{cursor:default}.RadComboBox_Default .rcbReadOnly .rcbInput{color:#333}.RadComboBox .rcbInput{margin:0;padding:2px 0 1px;height:auto;width:100%;border-width:0;outline:0;color:inherit;background-color:transparent;vertical-align:top}
        .RadComboBox_Default .rcbInput{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif;line-height:16px}
    .style30
    {
        width: 172px;
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
            <table width="100%">
                <tr>
                    <td class="style22" colspan="2">
                        <asp:Label ID="Label2" runat="server" Font-Names="Verdana" Font-Size="14px" Style="font-size: medium;
                            text-align: center;" Width="100%" ForeColor="#666666">Employee Payroll</asp:Label>
                    </td>
                </tr>
               <tr>
                    <td class="style22" colspan="2">
                        <asp:Label ID="lbldate" runat="server" Font-Names="Verdana" Font-Size="12px" Style="font-size: medium;
                            text-align: center;" Width="100%" ForeColor="#666666"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style30">
                        <asp:Label ID="lblEmpID1" runat="server" Font-Names="Verdana" Font-Size="12px" Width="100%" Font-Bold="True" ForeColor="#666666">Employee Name:</asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblName" runat="server" Font-Names="Verdana" Font-Size="12px" Width="100%" ForeColor="#666666"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style30">
                        <asp:Label ID="Label1" runat="server" Font-Names="Verdana" Font-Size="12px" Width="100%" Font-Bold="True" ForeColor="#666666">Grade:</asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblGrade" runat="server" Font-Names="Verdana" Font-Size="12px" Width="100%" ForeColor="#666666"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style30">
                        <asp:Label ID="Label4" runat="server" Font-Names="Verdana" Font-Size="12px" Width="100%" Font-Bold="True" ForeColor="#666666">Job Title:</asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lbljobtitle" runat="server" Font-Names="Verdana" Font-Size="12px" ForeColor="#666666"
                            Width="100%"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style30">
                        <asp:Label ID="Label3" runat="server" Font-Names="Verdana" Font-Size="12px" Width="100%" Font-Bold="True" ForeColor="#666666">Office / Department:</asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblOffice" runat="server" Font-Names="Verdana" Font-Size="12px" Width="100%" ForeColor="#666666"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style30">
                        <asp:Label ID="Label5" runat="server" Font-Names="Verdana" Font-Size="12px" Width="100%" Font-Bold="True" ForeColor="#666666">Location:</asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblLocation" runat="server" Font-Names="Verdana" Font-Size="12px" ForeColor="#666666"
                            Visible="True" Width="100%"></asp:Label>
                    </td>
                </tr>  
                <tr>
                    <td class="style30">
                        <asp:Label ID="Label11" runat="server" Font-Names="Verdana" Font-Size="12px" Width="100%" Font-Bold="True" ForeColor="#666666">Work Days:</asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lbldays" runat="server" Font-Names="Verdana" Font-Size="12px" ForeColor="#666666"
                            Width="100%"></asp:Label>
                    </td>
                </tr>          
                <tr>
                    <td class="style30">
                        <asp:Label ID="Label8" runat="server" Font-Names="Verdana" Font-Size="12px" Width="100%" Font-Bold="True" ForeColor="#666666">Attendance:</asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblactualdays" runat="server" Font-Names="Verdana" 
                            Font-Size="12px" ForeColor="#666666"
                            Width="100%"></asp:Label>
                    </td>
                </tr>
                
            </table>
            <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        </div>
        <div style="height: 163px">
            <div>
                <div>
                    <div>
                        <asp:Label ID="lblEmpID2" runat="server" Font-Names="Verdana" Font-Size="14px" Width="100%"
                            Style="font-weight: 700" Font-Bold="True" ForeColor="#666666">Earnings</asp:Label>
                    </div>
                </div>
                <div>
                    <asp:GridView ID="gridEarning" runat="server" BorderStyle="Solid" Font-Names="Verdana"
                        Font-Size="13px" Height="30px" Width="100%" PageSize="30" AutoGenerateColumns="False" GridLines="Vertical" ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC"
                        ShowHeaderWhenEmpty="True" EmptyDataText="No data to display"  Font-Bold="False">
                        <%--<AlternatingRowStyle BackColor="#CCCCCC" />--%>
                        <Columns>
                            <asp:BoundField DataField="rows" ItemStyle-Width="5%" HeaderText="SNo" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="SalaryItem" ItemStyle-Width="45%" HeaderText="Salary Item" />
                            <asp:BoundField DataField="total" ItemStyle-Width="15%" HeaderText="Amount" DataFormatString="{0:n}" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="IsTaxable" ItemStyle-Width="10%" HeaderText="Taxable" />
                        </Columns>
                        <HeaderStyle BackColor="#1BA691" ForeColor="White" HorizontalAlign="center" />
                        <RowStyle HorizontalAlign="Left" />
                    </asp:GridView>
                    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js" type="text/javascript">

                    </script>
                </div>
              
            </div>
            <div>
            <div>
                <div>
                    <div>
                        <asp:Label ID="Label6" runat="server" Font-Names="Verdana" Font-Size="14px" Width="100%"
                            Style="font-weight: 700" Font-Bold="True" ForeColor="#666666">Deductions</asp:Label>
                    </div>
                </div>
                <div>
                    <asp:GridView ID="gridDeduction" runat="server" BorderStyle="Solid" Font-Names="Verdana"
                        Font-Size="13px" Height="30px" Width="100%" PageSize="30" AutoGenerateColumns="False" GridLines="Vertical" ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC"
                        ShowHeaderWhenEmpty="True" EmptyDataText="No data to display" Font-Bold="False">
                        <%--<AlternatingRowStyle BackColor="#CCCCCC" />--%>
                        <Columns>
                            <asp:BoundField DataField="rows" ItemStyle-Width="5%" HeaderText="SNo" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="SalaryItem" ItemStyle-Width="45%" HeaderText="Salary Item" />
                            <asp:BoundField DataField="total" ItemStyle-Width="15%" HeaderText="Amount" DataFormatString="{0:n}" ItemStyle-HorizontalAlign="Right"/>
                            <asp:BoundField DataField="IsTaxable" ItemStyle-Width="10%" HeaderText="Taxable" />
                        </Columns>
                        <HeaderStyle BackColor="#1BA691" ForeColor="White" HorizontalAlign="center" />
                        <RowStyle HorizontalAlign="Left" />
                    </asp:GridView>
                    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js" type="text/javascript">

                    </script>
                </div>
                <div>
                    <table class="style21">
                        <tr>
                            <td class="style29">
                                <asp:Label ID="lblPayStatus" runat="server" Font-Names="Verdana" Font-Size="X-Small"
                                    Style="color: #FF0000; font-size: xx-small;"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style29">
                                <asp:Button ID="btnClose" runat="server" BackColor="#1BA691" BorderStyle="None" ForeColor="White"
                                    Height="20px" Text="Close" Width="140px" Font-Names="Verdana" 
                                    Font-Size="11px" />
                            </td>
                        </tr>
                        </table>
                </div>
            </div>
            </div>
           
            <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
        </div>
        </form>
    </body>
</html>
