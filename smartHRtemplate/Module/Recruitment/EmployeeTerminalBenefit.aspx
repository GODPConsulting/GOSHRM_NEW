<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="EmployeeTerminalBenefit.aspx.vb"
    Inherits="GOSHRM.EmployeeTerminalBenefit" EnableEventValidation="false" %>

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

<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
         <script type="text/javascript">
             function ShowProgress() {
                 setTimeout(function () {
                     var modal = $('<div />');
                     modal.addClass("modal");
                     $('body').append(modal);
                     var loading = $(".loading");
                     loading.show();
                     var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                     var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                     loading.css({ top: top, left: left });
                 }, 200);
             }
             $('form').live("submit", function () {
                 ShowProgress();
             });
    </script>
     <style type="text/css">
        .modal
        {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }
           .loading
        {
            font-family: Arial;
            font-size: 10pt;
            border: 5px solid #67CFF5;
            width: 200px;
            height: 100px;
            display: none;
            position: fixed;
            background-color: White;
            z-index: 999;
        }
    </style>

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
        width: 153px;
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
        width: 123px;
    }
    .style31
    {
        width: 346px;
    }
    .AlgRgh
    {
      text-align:right;
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
                    <td class="style22" colspan="3">
                        <asp:Label ID="Label2" runat="server" Font-Names="Candara" Font-Size="X-Large" Style="font-size: large;
                            text-align: center;" Width="100%" Font-Bold="True" ForeColor="#666666">Employee Terminal Benefits</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style30">
                      
        <asp:LinkButton ID="lnkPrint" runat="server" Font-Names="Verdana" 
            Font-Size="12px" Font-Bold="True">Print Payment</asp:LinkButton>
    
                    </td>
                    <td class="style31">
                        
                                <asp:Label ID="lblPayStatus" runat="server" Font-Names="Verdana" Font-Size="X-Small"
                                    Style="color: #FF0000; font-size: xx-small;"></asp:Label>
                        
                    </td>
                     <td>
                         &nbsp;</td>
                </tr>
                <tr>
                    <td class="style30">
                        <asp:Label ID="lblEmpID0" runat="server" Font-Names="Verdana" Font-Size="12px"
                            Width="100%" Font-Bold="True" ForeColor="#666666">Employee ID:</asp:Label>
                    </td>
                    <td class="style31">
                        <asp:Label ID="lblEmpID" runat="server" Font-Names="Verdana" Font-Size="12px" ForeColor="#666666"
                            Width="100%"></asp:Label>
                    </td>
                    <td>
                        <asp:CheckBox ID="chkNetPay" runat="server" Font-Names="Verdana" Font-Bold="True" ForeColor="#666666"
                            Font-Size="12px" Text="Apply Monthly Net Pay" 
                            ToolTip="Apply Employee's Monthly Net pay to generate Terminal Benefit" />
                    </td>
                </tr>
                <tr>
                    <td class="style30">
                        <asp:Label ID="lblEmpID1" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                            Width="100%">Employee Name:</asp:Label>
                    </td>
                    <td class="style31">
                        <asp:Label ID="lblName" runat="server" Font-Names="Verdana" Font-Size="12px" ForeColor="#666666"
                            Width="100%"></asp:Label>
                    </td>
                     <td>
                         <asp:CheckBox ID="chkLoan" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                             Text="Apply Outstanding Loan" 
                             ToolTip="Deduct Outstanding Loans of Employee from Terminal Benefits" />
                    </td>
                </tr>
                <tr>
                    <td class="style30">
                        <asp:Label ID="Label1" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                            Width="100%">Grade:</asp:Label>
                    </td>
                    <td class="style31">
                        <asp:Label ID="lblGrade" runat="server" Font-Names="Verdana" Font-Size="12px" ForeColor="#666666"
                            Width="100%"></asp:Label>
                    </td>
                     <td>
                         <asp:CheckBox ID="chkLeaveAllowance" runat="server" Font-Names="Verdana" Font-Bold="True" ForeColor="#666666"
                             Font-Size="12px" Text="Apply Leave Allowance" 
                             ToolTip="Compute outstanding leave allowance and apply to benefits" />
                    </td>
                </tr>
                <tr>
                    <td class="style30">
                        <asp:Label ID="Label4" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                            Width="100%">Job Title:</asp:Label>
                    </td>
                    <td class="style31">
                        <asp:Label ID="lbljobtitle" runat="server" Font-Names="Verdana" Font-Size="12px" ForeColor="#666666"
                            Width="100%"></asp:Label>
                    </td>
                     <td>
                         <asp:CheckBox ID="chkLeaveDays" runat="server" Font-Names="Verdana" Font-Bold="True" ForeColor="#666666"
                             Font-Size="12px" Text="Apply Leave Days" 
                             ToolTip="Compute outstanding leave allowance and apply to benefits" />
                    </td>
                </tr>
                <tr>
                    <td class="style30">
                        <asp:Label ID="Label3" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                            Width="100%">Office / Unit:</asp:Label>
                    </td>
                    <td class="style31">
                        <asp:Label ID="lblOffice" runat="server" Font-Names="Verdana" Font-Size="12px" ForeColor="#666666"
                            Width="100%"></asp:Label>
                    </td>
                     <td>
                         <asp:CheckBox ID="chkGratuity" runat="server" Font-Names="Verdana" 
                             Font-Bold="True" ForeColor="#666666"
                             Font-Size="12px" Text="Apply Gratuity" 
                             ToolTip="Compute outstanding leave allowance and apply to benefits" />
                    </td>
                </tr>
                <tr>
                    <td class="style30">
                        <asp:Label ID="Label5" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                            Width="100%">Location:</asp:Label>
                    </td>
                    <td class="style31">
                        <asp:Label ID="lblLocation" runat="server" Font-Names="Verdana" ForeColor="#666666"
                            Font-Size="12px"
                            Width="100%"></asp:Label>
                    </td>
                     <td>
                    </td>
                </tr>
                <tr>
                    <td class="style30">
                        <asp:Label ID="Label6" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                            Width="100%">Exit Date</asp:Label>
                    </td>
                    <td class="style31">
                        <telerik:RadDatePicker ID="datExitDate" Runat="server" Font-Names="Verdana" ForeColor="#666666"
                            Font-Size="12px" Enabled="False">
                        </telerik:RadDatePicker>
                    </td>
                     <td>
                    </td>
                </tr>
                <tr>
                    <td class="style30">
                        <asp:Label ID="Label7" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                            Width="100%">Total Earnings</asp:Label>
                    </td>
                    <td class="style31">
                        <asp:Label ID="lblearning" runat="server" Font-Names="Verdana" Font-Size="12px" ForeColor="#666666"
                            Width="30%" style="text-align: right">0</asp:Label>
                    </td>
                     <td>
                    </td>
                </tr>
                <tr>
                    <td class="style30">
                        <asp:Label ID="Label8" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                            Width="100%">Total Deductions</asp:Label>
                    </td>
                    <td class="style31">
                        <asp:Label ID="lbldeduction" runat="server" Font-Names="Verdana" Font-Size="12px" ForeColor="#666666"
                            Width="30%" style="text-align: right">0</asp:Label>
                    </td>
                     <td>
                    </td>
                </tr>
                <tr>
                    <td class="style30">
                        <asp:Label ID="Label9" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                            Width="100%">Net Pay</asp:Label>
                    </td>
                    <td class="style31">
                        <asp:Label ID="lblnet" runat="server" Font-Names="Verdana" Font-Size="12px" ForeColor="#666666"
                            Width="30%" style="text-align: right">0</asp:Label>
                    </td>
                     <td>
                    </td>
                </tr>
            </table>
            <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        </div>
        <div style="height: 163px">
            <div>
                <%--<telerik:RadTabStrip RenderMode="Lightweight" ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1"
                    SelectedIndex="0" ResolvedRenderMode="Classic" Width="100%" Font-Names="Arial"
                    Font-Size="X-Small">
                    <Tabs>
                        <telerik:RadTab Text="Repayments" runat="server" PageViewID="Repay" PostBack="False">
                        </telerik:RadTab>
                       
                    </Tabs>
                </telerik:RadTabStrip>--%><%--       <telerik:RadMultiPage runat="server" ID="RadMultiPage1" SelectedIndex="2" Width="100%">
                    
                    <telerik:RadPageView ID="Repay" runat="server" CssClass="pageView4 pageView">--%>
               
                <div>
                    <asp:GridView ID="GridRepay" runat="server" BorderStyle="Solid" Font-Names="Verdana"
                        Font-Size="12px" Height="50px" Width="100%" PageSize="50" 
                        AutoGenerateColumns="False" GridLines="Vertical" ForeColor="#666666" 
                        BorderWidth="1px" BorderColor="#CCCCCC"
                        ShowHeaderWhenEmpty="True" EmptyDataText="No data to display" Font-Bold="False">
                        <%--<AlternatingRowStyle BackColor="#CCCCCC" />--%>
                        <Columns>                     
                            <asp:BoundField DataField="rows" ItemStyle-Width="5%" HeaderText="SNo" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="SalaryItem" ItemStyle-Width="45%" HeaderText="Pay Item" />
                            <asp:TemplateField  HeaderText="Amount" ItemStyle-Width="25%" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate >
                                    <asp:TextBox ID="txtAmount" Width="100px"   Font-Names="Verdana"  Font-Size="12px" AutoPostBack="False" CssClass="AlgRgh" ForeColor="#666666" 
                                        runat="server" Text='<%# Eval("Amount", "{0:#,0.00}") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:BoundField DataField="itemclass" ItemStyle-Width="15%" HeaderText="Item Category" />
                            <asp:BoundField DataField="IsTaxable" ItemStyle-Width="10%" HeaderText="Taxable" />
                        </Columns>
                        <HeaderStyle BackColor="#1BA691" ForeColor="White" HorizontalAlign="center" />
                        <RowStyle HorizontalAlign="Left" />
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
                    <script type="text/javascript">




                        function openSchedule(code) {
                            window.open("LoanSchedule.aspx?id=" + code, "open_window", "width=600,height=700");
                        }
                    </script>
                </div>
                <div>
                    <table class="style21">
                        <tr>
                            <td class="style29">
                                &nbsp;</td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="style29">
                                <asp:Button ID="btnRepay" runat="server" BackColor="#1BA691" BorderStyle="None" ForeColor="White"
                                    Height="20px" Text="Save" Width="140px" Font-Names="Verdana" 
                                    Font-Size="11px" Font-Bold="True" />
                            </td>
                            <td>
                                <asp:Button ID="btnClose" runat="server" BackColor="#FF3300" BorderStyle="None" ForeColor="White"
                                    Height="20px" Text="Back" Width="140px" Font-Names="Verdana" 
                                    Font-Size="11px" Font-Bold="True" />
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:Button ID="btnReset" runat="server" BackColor="#0099FF" BorderStyle="None"
                                    ForeColor="White" Height="20px" Text="Regenerate Benefits" 
                                    Width="150px" ToolTip="Click refresh Salary Item for Employee Terminal Benefits"
                                    OnClientClick="ConfirmGenerate()" Font-Names="Verdana" Font-Size="11px" 
                                    Font-Bold="True" />
                            </td>
                        </tr>
                        <tr>
                            <td class="style29">
                                &nbsp; &nbsp;
                            <asp:Label ID="lblactive" runat="server" Font-Names="Verdana" 
                            Font-Size="13px" Visible="False"
                            Width="1%"></asp:Label>
                            </td>
                            <td>
                            <asp:Label ID="lblid" runat="server" Font-Names="Verdana" 
                            Font-Size="13px" Visible="False"
                            Width="1%"></asp:Label>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="style29">
                                &nbsp;</td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </div>
                <%--<AlternatingRowStyle BackColor="#CCCCCC" />--%>
            </div>
            <div>
                <tr>
                    <td class="style29">
                        &nbsp;
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
            </div>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
        </div>
        </form>
          <div class="loading" align="center">
        Processing, please wait...<br />
        <br />
        <img src="../../images/loaders.gif" alt="" />
    </div>
    </body>
</html>
