<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="EmployeeLeaves.aspx.vb"
    Inherits="GOSHRM.EmployeeLeaves" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerikcontrol" %>
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
    <script type="text/javascript" language="javascript">
        //    Grid View Check box
        function CheckGroupEmp(Checkbox) {
            var GridVwHeaderChckbox = document.getElementById("<%=gridGroupView.ClientID %>");
            for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }
    </script>
    <title>Leave</title>
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
            if (confirm("Approve the selected items?")) {
                confirm_app.value = "Yes";
                ShowProgress();
            } else {
                confirm_app.value = "No";
            }
            document.forms[0].appendChild(confirm_app);
        }
    </script>
    <body style="">
        <form id="form1">
        <div class="container col-md-12">
             <div class="row">
                 <div id="divalert" runat="server" visible="false" class="alert alert-info">
                    <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                    <strong id="msgalert" runat="server">Danger!</strong>
                </div>
            </div>
            <div id="content" runat="server">
            <div class="panel panel-success">
                <div class="panel-heading">
                    <h5><b id="pagetitle" runat="server"></b></h5>
                </div>
             <div class="panel-body">
        
         <div class="row">
         <div class="col-sm-3 col-md-3 col-xs-6">  
                        <telerik:RadComboBox Width="100%" ID="cboView" Skin="Bootstrap" runat="server" Font-Names="Verdana" Font-Size="12px"
                            ResolvedRenderMode="Classic" RenderMode="Lightweight" AutoPostBack="True" ForeColor="#666666">
                        </telerik:RadComboBox>
               </div>
             <div class="col-sm-3 col-md-3 col-xs-6">  
               <telerik:RadComboBox runat="server" Skin="Bootstrap" ForeColor="#666666"
                            RenderMode="Lightweight" ResolvedRenderMode="Classic" Width="100%" ID="cboCompany"
                            Font-Names="Verdana" Font-Size="12px" Filter="Contains" AutoPostBack="True">
                        </telerik:RadComboBox>
               </div>

               </div>
             
        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
            <asp:View ID="AllListView" runat="server">
                <div class="row">
                <div class="col-sm-3 col-md-3 col-xs-6">
                <telerik:RadComboBox ID="radStatus" Skin="Bootstrap" runat="server" Font-Names="Verdana" Font-Size="12px"
                                        ResolvedRenderMode="Classic" Width="100%" AutoPostBack="True" ForeColor="#666666">
                                        <Items>
                                            <telerikcontrol:RadComboBoxItem runat="server" Owner="" Text="Pending Approval" 
                                                Value="Pending Approval" />
                                            <telerikcontrol:RadComboBoxItem runat="server" Owner="" Text="Cancelled" 
                                                Value="Cancelled" />
                                            <telerikcontrol:RadComboBoxItem runat="server" Owner="" Text="Rejected" 
                                                Value="Rejected" />
                                            <telerikcontrol:RadComboBoxItem runat="server" Owner="" Text="Taken" 
                                                Value="Taken" />
                                        </Items>
                                    </telerik:RadComboBox>
                </div>
                <div class="col-sm-3 col-md-2 col-xs-6">     
                    <telerik:RadDatePicker ID="dateFrom" Skin="Bootstrap" runat="server" Font-Names="Verdana" Font-Size="12px"
                                        Height="30px" Width="100%" Culture="en-US" ResolvedRenderMode="Classic" ForeColor="#666666">
                                        <calendar enableweekends="True" Height="30px" fastnavigationnexttext="&amp;lt;&amp;lt;" 
                                            usecolumnheadersasselectors="False" userowheadersasselectors="False">
                                        </calendar>
                                        <dateinput dateformat="dd/MM/yyyy" displaydateformat="dd/MM/yyyy" height="30px" 
                                            labelwidth="40%" width="">
                                        <emptymessagestyle resize="None" />
                                        <readonlystyle resize="None" />
                                        <focusedstyle resize="None" />
                                        <disabledstyle resize="None" />
                                        <invalidstyle resize="None" />
                                        <hoveredstyle resize="None" />
                                        <enabledstyle resize="None" />
                                        </dateinput>
                                        <DatePopupButton HoverImageUrl="" ImageUrl="" CssClass="" />
                                    </telerik:RadDatePicker>
                </div>
                <div class="col-sm-3 col-md-2 col-xs-6">     
                    <telerik:RadDatePicker Skin="Bootstrap" ID="dateTo" runat="server" Font-Names="Verdana" Font-Size="11px"
                                        Height="30px" Width="100%" Culture="en-US" ResolvedRenderMode="Classic" ForeColor="#666666">
                                        <calendar Height="30px" enableweekends="True" fastnavigationnexttext="&amp;lt;&amp;lt;" 
                                            usecolumnheadersasselectors="False" userowheadersasselectors="False">
                                        </calendar>
                                        <dateinput dateformat="dd/MM/yyyy" displaydateformat="dd/MM/yyyy" height="30px" 
                                            labelwidth="40%" width="">
                                        <emptymessagestyle resize="None" />
                                        <readonlystyle resize="None" />
                                        <focusedstyle resize="None" />
                                        <disabledstyle resize="None" />
                                        <invalidstyle resize="None" />
                                        <hoveredstyle resize="None" />
                                        <enabledstyle resize="None" />
                                        </dateinput>
                                        <DatePopupButton HoverImageUrl="" ImageUrl="" CssClass="" />
                                    </telerik:RadDatePicker>
                </div>
                <div class="search-box-wrapper col-sm-6 col-md-4 col-xs-12 form-group pull-right">
                         <button id="btnApprove" type="button" data-toggle="tooltip" data-original-title="Approve" runat="server" class="glyphicon glyphicon-ok btn btn-default btn-sm" onserverclick="btnApprove_Click"
                        style="margin-left:10px;margin-right:10px;height: 35px;"></button> 
                        <asp:LinkButton ID="btnDelete" data-toggle="tooltip" data-original-title="Delete" Height="35px" runat="server" CssClass="btn btn-default btn-sm" OnClick="Delete" OnClientClick="Confirm()">
                            <span style="margin-top:5px" aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                        </asp:LinkButton>
                    <input id="txtsearch" style="margin-left:10px;width:100%" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                    <button onserverclick="Button1_Click" id="btnFind" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                </div>
            </div>
                <div class="row col-md-12">
                    <asp:GridView ID="GridVwHeaderChckbox" runat="server" AllowPaging="True" AllowSorting="True"
                        BorderStyle="Solid" DataKeyNames="refno" Font-Names="Verdana" Font-Size="11px"
                        OnRowDataBound="OnRowDataBound" OnSorting="SortRecords" PageSize="50"
                        ToolTip="click row to select record" Width="100%" AutoGenerateColumns="False" CssClass="table table-condensed"
                        GridLines="Vertical" ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC"
                        ShowHeaderWhenEmpty="True">
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
                            <asp:BoundField DataField="Rows" HeaderText="Rows">
                                <ItemStyle Width="5px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Ref No">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/TimeManagement/EmployeeLeaveDetail.aspx?id={0}",
                                    HttpUtility.UrlEncode(Eval("refno").ToString())) %>'
                                        Text='<%# Eval("refno")%>' />
                                </ItemTemplate>
                                <ItemStyle Font-Bold="True" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="Leave Type" HeaderText="Leave Type">
                            </asp:BoundField>
                            <asp:BoundField DataField="Employee Name" HeaderText="Employee">
                            </asp:BoundField>
                            <asp:BoundField DataField="MgrApproval" HeaderText="Manager Approval">
                            </asp:BoundField>
                            <asp:BoundField DataField="HRApproval" HeaderText="HR Approval">
                            </asp:BoundField>
                            <asp:BoundField DataField="From" HeaderText="Leave From">
                            </asp:BoundField>
                            <asp:BoundField DataField="To" HeaderText="Leave To">
                            </asp:BoundField>
                            <asp:BoundField DataField="No of Days" HeaderText="Days">
                            </asp:BoundField>
                            <asp:BoundField DataField="Status" HeaderText="Status">
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle BackColor="White" ForeColor="#1BA691" HorizontalAlign="Center" />
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
                            window.open("LeaveRequest.aspx?id=" + code, "open_window", "width=750,height=700");
                        }
                    </script>
                </div>
            </asp:View>
            <asp:View ID="GroupByEmployee" runat="server">
                <div class="row">
                <div class="search-box-wrapper col-sm-6 col-md-4 col-xs-12 form-group pull-right">
                                         <button id="btnGroupApprove" type="button" data-toggle="tooltip" data-original-title="Approve" runat="server" class="glyphicon glyphicon-ok btn btn-default btn-sm" onserverclick="btnGroupApprove_Click"
                                        style="margin-left:10px;margin-right:10px;height: 35px;"></button> 
                                    <input id="txtSearchGroup" style="margin-left:10px;width:100%" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                                    <button onserverclick="Button1_Click" id="btnGroupView" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                                </div>
                <div class="col-sm-3 col-md-2 col-xs-6 pull-right">     
                                <telerik:RadDatePicker Skin="Bootstrap" ID="dateGroupFrom" runat="server" Font-Names="Verdana"
                                    Font-Size="12px" Width="100%" Culture="en-US" ResolvedRenderMode="Classic"
                                    ForeColor="#666666">
                                    <calendar enableweekends="True" fastnavigationnexttext="&amp;lt;&amp;lt;" 
                                        usecolumnheadersasselectors="False" userowheadersasselectors="False">
                                    </calendar>
                                    <dateinput dateformat="dd/MM/yyyy" displaydateformat="dd/MM/yyyy" height="22px" 
                                        labelwidth="40%" width="">
                                    <emptymessagestyle resize="None" />
                                    <readonlystyle resize="None" />
                                    <focusedstyle resize="None" />
                                    <disabledstyle resize="None" />
                                    <invalidstyle resize="None" />
                                    <hoveredstyle resize="None" />
                                    <enabledstyle resize="None" />
                                    </dateinput>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" CssClass="" />
                                </telerik:RadDatePicker>
                                </div>
                          <div class="col-sm-3 col-md-2 col-xs-6 pull-right">     
                                <telerik:RadDatePicker Skin="Bootstrap" ID="dateGroupTo" runat="server" Culture="en-US" Font-Names="Verdana"
                                    Font-Size="12px" ResolvedRenderMode="Classic" Width="100%" ForeColor="#666666">
                                    <calendar enableweekends="True" fastnavigationnexttext="&amp;lt;&amp;lt;" 
                                        usecolumnheadersasselectors="False" userowheadersasselectors="False">
                                    </calendar>
                                    <dateinput dateformat="dd/MM/yyyy" displaydateformat="dd/MM/yyyy" height="22px" 
                                        labelwidth="40%" width="">
                                    <emptymessagestyle resize="None" />
                                    <readonlystyle resize="None" />
                                    <focusedstyle resize="None" />
                                    <disabledstyle resize="None" />
                                    <invalidstyle resize="None" />
                                    <hoveredstyle resize="None" />
                                    <enabledstyle resize="None" />
                                    </dateinput>
                                    <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                </telerik:RadDatePicker>
                                </div>
                </div>
                <div class="row col-md-12">
                    <asp:GridView ID="gridGroupView" runat="server" AllowPaging="True" AllowSorting="True"
                        BorderStyle="Solid" DataKeyNames="empid, DateFrom, DateTo" Font-Names="Verdana" CssClass="table table-condensed"
                        Font-Size="12px" Height="50px" OnSorting="SortGroupRecords" PageSize="100" ToolTip="click row to select record"
                        Width="100%" AutoGenerateColumns="False" GridLines="Vertical" ForeColor="#666666"
                        BorderWidth="1px" BorderColor="#CCCCCC" ShowHeaderWhenEmpty="True">
                        <RowStyle BackColor="white" />
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="CheckGroupEmp(this);" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkEmp" runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="1px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="Rows" HeaderText="Rows">
                                <ItemStyle Width="5px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Emp ID" SortExpression="EmpID">
                                <ItemTemplate>
                                    <a href="#" onclick='openSubWindow("<%# Eval("EmpID") %>","<%# Eval("DateFrom") %>","<%# Eval("DateTo") %>");'>
                                        <%# Eval("EmpID")%></a>
                                </ItemTemplate>
                                <ItemStyle Font-Bold="True" Width="20px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="Employee" HeaderText="Name" SortExpression="Employee">
                            </asp:BoundField>
                            <asp:BoundField DataField="JobTitle" HeaderText="Job Title" SortExpression="JobTitle">
                            </asp:BoundField>
                            <asp:BoundField DataField="Company" HeaderText="Company" SortExpression="Company">
                            </asp:BoundField>
                            <asp:BoundField DataField="Office" HeaderText="Dept/Unit" SortExpression="Office">
                            </asp:BoundField>
                            <asp:BoundField DataField="pending" HeaderText="Pending" ItemStyle-HorizontalAlign="Right"
                                SortExpression="pending" DataFormatString="{0:n}">
                                <ItemStyle Width="30px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="approved" HeaderText="Approved Days" ItemStyle-HorizontalAlign="Right"
                                SortExpression="approved" DataFormatString="{0:n}">
                            </asp:BoundField>
                            <asp:BoundField DataField="taken" HeaderText="Taken Days" ItemStyle-HorizontalAlign="Right"
                                SortExpression="taken" DataFormatString="{0:n}">
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle BackColor="white" ForeColor="#1BA691" HorizontalAlign="Center" />
                    </asp:GridView>
                    <script type="text/javascript">
                        function openSubWindow(code, code2, code3) {
                            window.location = "EmployeeLeaveList.aspx?empid=" + code + "&datefrom=" + code2 + "&dateto=" + code3
                            //window.open("EmployeeLeaveList.aspx?empid=" + code + "&datefrom=" + code2 + "&dateto=" + code3, "open_window", "width=1400,height=700");
                        }
                    </script>
                </div>
            </asp:View>
        </asp:MultiView>
        </div></div></div></div>
        <div class="loading" align="center">
            Please wait...<br />
            <br />
            <img src="../../../images/loader.gif" alt="" />
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
        .style23
        {
            width: 275px;
        }
    </style>
</asp:Content>
