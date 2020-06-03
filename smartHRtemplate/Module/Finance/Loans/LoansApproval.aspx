<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="LoansApproval.aspx.vb"
    Inherits="GOSHRM.LoansApproval" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="mytelerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="myajax" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
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
 
    <script type="text/javascript" language="javascript">
        //    Grid View Check box
        function CheckAllEmp1(Checkbox) {
            var GridView1 = document.getElementById("<%=GridView1.ClientID %>");
            for (i = 1; i < GridView1.rows.length; i++) {
                GridView1.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
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
    <script type="text/javascript">
        function ConfirmApprove() {
            var confirm_app = document.createElement("INPUT");
            confirm_app.type = "hidden";
            confirm_app.name = "confirm_app";
            if (confirm("Approve the selected loans?")) {
                confirm_app.value = "Yes";
                ShowProgress();
            } else {
                confirm_app.value = "No";
            }
            document.forms[0].appendChild(confirm_app);
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
        .style30
        {
            width: 93px;
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
    </style>
    <body>
        <form id="form1">
        
        <%--<div style="border: thin solid #C0C0C0">
            <table width="100%">
                <tr>
                    <td class="style22">
                        <asp:Label ID="lblView" runat="server" Font-Names="Candara" Font-Size="Medium" Width="100%"
                            Style="font-weight: 700; color: #FF6600"></asp:Label>
                    </td>
                </tr>
            </table>
            <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        </div>--%>
        <div class="container col-md-12">
            <div id="divalert" runat="server" visible="false" class="alert alert-info">
                <strong id="msgalert" runat="server">Danger!</strong>
            </div>
        <div class="panel panel-success">
                <div class="panel-heading">
                    <h5><b id="pagetitle" runat="server"></b></h5>
                </div>
             <div class="panel-body">
    
            <div>
                <div>
                    <%--<table>
                        <tr>
                            <td class="style30">
                                <asp:Label ID="Label4" runat="server" Text="From" Font-Names="Verdana" 
                                    Font-Size="11px" Font-Bold="True" ForeColor="#666666"></asp:Label>
                            </td>
                            <td>
                                <mytelerik:RadDatePicker ID="radSubDateFrom" runat="server" Font-Names="Verdana" ForeColor="#666666"
                                    Font-Size="11px" Height="22px" Width="120px" Culture="en-US" ResolvedRenderMode="Classic">
                                    <Calendar EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;" UseColumnHeadersAsSelectors="False"
                                        UseRowHeadersAsSelectors="False">
                                    </Calendar>
                                    <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" Height="22px" LabelWidth="40%"
                                        Width="">
                                        <EmptyMessageStyle Resize="None" />
                                        <ReadOnlyStyle Resize="None" />
                                        <FocusedStyle Resize="None" />
                                        <DisabledStyle Resize="None" />
                                        <InvalidStyle Resize="None" />
                                        <HoveredStyle Resize="None" />
                                        <EnabledStyle Resize="None" />
                                    </DateInput>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" CssClass="" />
                                </mytelerik:RadDatePicker>
                            </td>
                            <td>
                                <asp:Label ID="Label5" runat="server" Font-Names="Verdana" Font-Size="11px" ForeColor="#666666" Text="To"></asp:Label>
                            </td>
                            <td>
                                <mytelerik:RadDatePicker ID="radSubDateTo" runat="server" Culture="en-US" Font-Names="Verdana" ForeColor="#666666"
                                    Font-Size="11px" Height="22px" ResolvedRenderMode="Classic" Width="120px">
                                    <Calendar EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;" UseColumnHeadersAsSelectors="False"
                                        UseRowHeadersAsSelectors="False">
                                    </Calendar>
                                    <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" Height="22px" LabelWidth="40%"
                                        Width="">
                                        <EmptyMessageStyle Resize="None" />
                                        <ReadOnlyStyle Resize="None" />
                                        <FocusedStyle Resize="None" />
                                        <DisabledStyle Resize="None" />
                                        <InvalidStyle Resize="None" />
                                        <HoveredStyle Resize="None" />
                                        <EnabledStyle Resize="None" />
                                    </DateInput>
                                    <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                </mytelerik:RadDatePicker>
                            </td>
                            <td>
                            </td>
                             <td class="style30">
                                <asp:Label ID="Label6" runat="server" Font-Names="Verdana" Font-Size="11px" 
                                    ForeColor="#666666" Text="Status" Font-Bold="True"></asp:Label>
                            </td>
                            <td>
                                <mytelerik:RadComboBox ID="cboSubStatus" runat="server" AutoPostBack="True" Font-Names="Verdana" ForeColor="#666666"
                                    Font-Size="11px" ResolvedRenderMode="Classic" Width="150px">
                                </mytelerik:RadComboBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtSubSearch" runat="server" BorderColor="#CCCCCC" BorderWidth="1px"
                                    Height="20px" TextMode="Search" Width="200px" Font-Names="Verdana" 
                                    Font-Size="11px" ForeColor="#666666"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnSubFind" runat="server" BackColor="#1BA691" BorderStyle="None"
                                    Font-Names="Verdana" Font-Size="11px" ForeColor="White" Height="20px" Text="View"
                                    Width="100px" />
                            </td>
                            <td>
                                <asp:Button ID="btnApprove" runat="server" BackColor="#6699FF" BorderStyle="None"
                                    Font-Names="Verdana" Font-Size="11px" ForeColor="White" Height="20px" OnClientClick="ConfirmApprove()"
                                    Text="Approve" Width="100px" />
                            </td>
                        </tr>
                    </table>--%>
                    <div class="row">
                    <div class="col-sm-3 col-md-2 col-xs-6">
                            <mytelerik:RadComboBox ID="cboSubStatus" Skin="Bootstrap" runat="server" AutoPostBack="True" Font-Names="Verdana" ForeColor="#666666"
                                    ResolvedRenderMode="Classic" Width="150px">
                                </mytelerik:RadComboBox>
                        </div>
                         <div class="col-sm-3 col-md-2 col-xs-6">
                           <mytelerik:RadDatePicker Skin="Bootstrap" ID="radSubDateFrom" runat="server" Font-Names="Verdana" ForeColor="#666666"
                                    Font-Size="11px" Width="100%" Culture="en-US" ResolvedRenderMode="Classic">
                                    <Calendar EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;" UseColumnHeadersAsSelectors="False"
                                        UseRowHeadersAsSelectors="False">
                                    </Calendar>
                                    <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" Height="22px" LabelWidth="40%"
                                        Width="">
                                        <EmptyMessageStyle Resize="None" />
                                        <ReadOnlyStyle Resize="None" />
                                        <FocusedStyle Resize="None" />
                                        <DisabledStyle Resize="None" />
                                        <InvalidStyle Resize="None" />
                                        <HoveredStyle Resize="None" />
                                        <EnabledStyle Resize="None" />
                                    </DateInput>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" CssClass="" />
                                </mytelerik:RadDatePicker>
                        </div>                        
                        <div class="col-sm-3 col-md-2 col-xs-6">
                           <mytelerik:RadDatePicker Skin="Bootstrap" ID="radSubDateTo" runat="server" Culture="en-US" Font-Names="Verdana" ForeColor="#666666"
                                    Font-Size="11px" ResolvedRenderMode="Classic" Width="100%">
                                    <Calendar EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;" UseColumnHeadersAsSelectors="False"
                                        UseRowHeadersAsSelectors="False">
                                    </Calendar>
                                    <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" Height="22px" LabelWidth="40%"
                                        Width="">
                                        <EmptyMessageStyle Resize="None" />
                                        <ReadOnlyStyle Resize="None" />
                                        <FocusedStyle Resize="None" />
                                        <DisabledStyle Resize="None" />
                                        <InvalidStyle Resize="None" />
                                        <HoveredStyle Resize="None" />
                                        <EnabledStyle Resize="None" />
                                    </DateInput>
                                    <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                </mytelerik:RadDatePicker>
                        </div>
                         <div class="search-box-wrapper col-sm-6 col-md-3 col-xs-12 form-group pull-right">
                            <input id="txtSubSearch" style="width:100%" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                            <button onserverclick="btnSubFind_Click" id="btnsearch" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                        </div>                     
                        <div style="margin-left:20px;" class="col-sm-3 col-md-2 col-xs-6">
                            <asp:Button ID="btnApprove" CssClass="btn btn-primary btn-info" runat="server" BackColor="#6699FF" BorderStyle="None"
                                    Font-Names="Verdana" ForeColor="White" Height="30px" OnClientClick="ConfirmApprove()"
                                    Text="Approve" Width="100%" />
                        </div>
                    </div>
                </div>
            </div>
            <div>
                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
                    BorderStyle="Solid" DataKeyNames="id" Font-Names="Verdana" Font-Size="11px" EmptyDataText="No data to display"
                    OnRowDataBound="OnRowSurbodinateDataBound" OnSorting="SortSurbodinateRecords" CssClass="table table-condensed"
                    PageSize="100" ToolTip="click row to select record" Width="100%" ShowHeaderWhenEmpty="True"
                    AutoGenerateColumns="False" GridLines="Vertical" ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC">
                    <RowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="CheckAllEmp1(this);" /></HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkEmp" runat="server" /></ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px"></ItemStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="rows" HeaderText="Row">
                            <ItemStyle Width="5px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Loan RefNo" SortExpression="Loan RefNo">
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/Finance/Loans/ManagerLoanApprove.aspx?id={0}",
                HttpUtility.UrlEncode(Eval("id").ToString())) %>'
                                    Text='<%# Eval("Loan RefNo")%>' /></ItemTemplate>
                            <ItemStyle Font-Bold="True" Width="20px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Employee" HeaderText="Employee">
                        </asp:BoundField>
                        <asp:BoundField DataField="Loan" HeaderText="Loan Type">                          
                        </asp:BoundField>
                        <asp:BoundField DataField="Loan Date" HeaderText="Loan date" ItemStyle-HorizontalAlign="Center">
                        </asp:BoundField>
                        <asp:BoundField DataField="Repayment Start Date" HeaderText="Repayment Start Date"
                            ItemStyle-HorizontalAlign="Center">
                        </asp:BoundField>
                        <asp:BoundField DataField="amount" HeaderText="Loan Amount" ItemStyle-HorizontalAlign="Right"
                            DataFormatString="{0:n}">
                        </asp:BoundField>
                        <asp:BoundField DataField="Monthly Payment" HeaderText="Monthly Payment" ItemStyle-HorizontalAlign="Right"
                            DataFormatString="{0:n}">
                        </asp:BoundField>
                        <asp:BoundField DataField="ApproverStatus" HeaderText="Status">
                        </asp:BoundField>
                        <asp:BoundField DataField="Status" HeaderText="Final Status">
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle BackColor="White" ForeColor="#1BA691" HorizontalAlign="Center"></HeaderStyle>
                </asp:GridView>
                <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js" type="text/javascript">
                </script>
                <script type="text/javascript">
                    $(function () {
                        $("[id*=GridView1] td").hover(function () {
                            $("td", $(this).closest("tr")).addClass("hover_row");
                        }, function () {
                            $("td", $(this).closest("tr")).removeClass("hover_row");
                        })
                    })
                </script>
            </div>
            <div class="loading" align="center">
                Please wait...<br />
                <br />
                <img src="../../../images/loader.gif" alt="" />
            </div>
            </div></div>
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
        .style23
        {
            width: 275px;
        }
    </style>
</asp:Content>
