<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="StaffLoans.aspx.vb"
    Inherits="GOSHRM.StaffLoans" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
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
        <div class="container col-md-12">
            <div class="row">
                <div id="divalert" runat="server" visible="false" class="alert alert-info">
                    <strong id="msgalert" runat="server"></strong>
                    <asp:TextBox ID="txtid" runat="server" Width="3px"  
                    Font-Names="Candara" Height="2px" Visible="False"></asp:TextBox>
                </div>
                </div> 
                   <div class="panel panel-success">
                        <div class="panel-heading">
                            <h5><b id="pagetitle" runat="server"></b></h5>
                        </div>
                     <div class="panel-body">
                              <div class="row">
                               <div class="col-md-3 col-sm-6 col-xs-12 form-group">          
                              <telerik:RadComboBox Skin="Bootstrap" runat="server"
                            ResolvedRenderMode="Classic" Width="100%" ID="cboCompany" AutoPostBack="True" 
                                Filter="Contains" Font-Names="Verdana" Font-Size="11px" ForeColor="#666666">
                                </telerik:RadComboBox>
                        </div>
                              <div class="col-md-3 col-sm-6 col-xs-12 form-group">
                                    <telerik:RadComboBox ID="radStatus" Skin="Bootstrap" runat="server" AutoPostBack="True" Font-Names="Verdana"
                                                    Font-Size="11px" ResolvedRenderMode="Classic" ForeColor="#666666">
                                                </telerik:RadComboBox>
                              </div>
                              <div class="col-md-3 col-sm-6 col-xs-12 form-group">          
                              <telerik:RadDatePicker ID="dateFrom" Skin="Bootstrap" runat="server" Culture="en-US" Font-Names="Verdana"
                                                    Font-Size="11px" ResolvedRenderMode="Classic" Width="100%" 
                                                    ForeColor="#666666">
                                                    <Calendar EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;" UseColumnHeadersAsSelectors="False"
                                                        UseRowHeadersAsSelectors="False">
                                                    </Calendar>
                                                    <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" Height="18px" LabelWidth="40%"
                                                        Width="">
                                                        <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                                        <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                                                        <FocusedStyle Resize="None"></FocusedStyle>
                                                        <DisabledStyle Resize="None"></DisabledStyle>
                                                        <InvalidStyle Resize="None"></InvalidStyle>
                                                        <HoveredStyle Resize="None"></HoveredStyle>
                                                        <EnabledStyle Resize="None"></EnabledStyle>
                                                    </DateInput>
                                                    <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl=""></DatePopupButton>
                                                </telerik:RadDatePicker>
                        </div>
                              <div class="col-md-3 col-sm-6 col-xs-12 form-group">
                                    <telerik:RadDatePicker Skin="Bootstrap" ID="dateTo" runat="server" Culture="en-US" Font-Names="Verdana"
                                                    Font-Size="11px" ResolvedRenderMode="Classic" Width="100%" 
                                                    ForeColor="#666666">
                                                    <Calendar EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;" UseColumnHeadersAsSelectors="False"
                                                        UseRowHeadersAsSelectors="False">
                                                    </Calendar>
                                                    <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" Height="18px" LabelWidth="40%"
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
                                                </telerik:RadDatePicker>
                              </div>                          
                
                        </div>
       <div class="row">
                       <div class="search-box-wrapper col-sm-6 col-md-4 col-xs-12 form-group pull-right">
                                <asp:LinkButton ID="btnDelete" data-toggle="tooltip" data-original-title="Delete" Height="35px" runat="server" CssClass="btn btn-default btn-sm" OnClick="Delete" OnClientClick="Confirm()">
                                    <span style="margin-top:5px" aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                                </asp:LinkButton>
                                 <button id="btAdd" type="button" data-toggle="tooltip" data-original-title="Add" runat="server" class="glyphicon glyphicon-plus btn btn-default btn-sm" onserverclick="btnApply_Click"
                                style="margin-left:10px;margin-right:10px;height: 35px;"></button> 
                            <input id="txtsearch" style="width:100%" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                            <button onserverclick="Button1_Click" id="btnApply" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                        </div>
                        <div class="col-md-3 col-sm-6 col-xs-12 form-group pull-right">          
                             <asp:RadioButtonList ID="rdoStatusType" runat="server" AutoPostBack="True" Font-Names="Verdana"
                              RepeatDirection="Horizontal" TabIndex="7" Width="100%" Font-Size="11px" 
                              ForeColor="#666666" Font-Bold="True"> <asp:ListItem>Approval</asp:ListItem>
                              <asp:ListItem>Payment</asp:ListItem></asp:RadioButtonList>
                        </div>
                <div class="col-md-2 col-sm-3 col-xs-12 form-group pull-right">          
                    <asp:Button ID="btnApprove" CssClass="btn btn-primary btn-success" runat="server" BorderStyle="None"
                    ForeColor="White" Height="35px" Text="Approve" Width="100%" Font-Names="Verdana"
                    Font-Size="11px" OnClientClick="ConfirmApprove()" />
                </div>
                
            </div>
                            <div class="row">
                                <asp:GridView 
                                    ID="GridVwHeaderChckbox" runat="server" AllowPaging="True" AllowSorting="True"
                            BorderStyle="Solid" DataKeyNames="id" Font-Names="Verdana" Font-Size="11px" CssClass="table table-condensed"
                            Height="50px" OnRowDataBound="OnRowDataBound" OnSorting="SortRecords" PageSize="2000"
                            ToolTip="click row to select record" Width="100%" AutoGenerateColumns="False" GridLines="Vertical" ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC" ShowHeaderWhenEmpty="True" EmptyDataText="No data to display">
                            <RowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="CheckAllEmp(this);" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkEmp" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="1px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="rows" HeaderText="Row">
                                    <ItemStyle Width="5px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Loan RefNo">
                                    <ItemTemplate>
                                        <a href="LoanDetail.aspx?id=<%# Eval("id") %>">
                                            <%# Eval("Loan RefNo")%></a>
                                    </ItemTemplate>
                                    <ItemStyle Font-Bold="True" Width="20px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                 <asp:BoundField DataField="Employee" HeaderText="Employee" SortExpression= "Employee">
                                </asp:BoundField>
                                <asp:BoundField DataField="Loan" HeaderText="Loan Type" SortExpression ="loan">
                                </asp:BoundField>
                                <asp:BoundField DataField="Loan Date" HeaderText="Loan date" ItemStyle-HorizontalAlign="Center" SortExpression="loan date">
                                </asp:BoundField>
                                <asp:BoundField DataField="amount" HeaderText="Loan Amount" ItemStyle-HorizontalAlign="Right" SortExpression = "amount" DataFormatString="{0:n}" >
                                </asp:BoundField>
                                  <asp:BoundField DataField="Monthly Payment" HeaderText="Monthly Payment" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n}">
                                </asp:BoundField>
                                <asp:BoundField DataField="ExpectedPay" HeaderText="Expected Payment" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n}">
                                </asp:BoundField>
                                <asp:BoundField DataField="Payments" HeaderText="Payments" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n}">
                                </asp:BoundField>
                                <asp:BoundField DataField="Balance" HeaderText="Outstanding Balance" ItemStyle-HorizontalAlign="Right" SortExpression="balance" DataFormatString="{0:n}">
                                </asp:BoundField>                                                   
                                <asp:BoundField DataField="Status" HeaderText="Final Approval">
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate >
                                        <a href="LoansSchedule.aspx?id=<%# Eval("id") %>">
                                            Schedule</a>
                                    </ItemTemplate >
                                    <ItemStyle Font-Bold="True" Width="20px" HorizontalAlign="Center" />
                                </asp:TemplateField>

                       
                            </Columns>
                            <HeaderStyle BackColor="white" ForeColor="#1BA691" HorizontalAlign="Center"></HeaderStyle>
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
                                        window.open("LoanDetail.aspx?id=" + code, "open_window", "width=800,height=650");
                                    }
                                </script>
                                <script type="text/javascript">

                                    function openSchedule(code) {
                                        window.open("LoansSchedule.aspx?id=" + code, "open_window", "width=800,height=700");
                                    }
                                </script>
                            </div>
                            
            </div>
        </div>
        <div class="loading" align="center">
            Please wait...<br />
            <br />
            <img src="../../../images/loader.gif" alt="" />
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
