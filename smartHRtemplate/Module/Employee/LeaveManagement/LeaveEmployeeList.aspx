<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="LeaveEmployeeList.aspx.vb"
    Inherits="GOSHRM.LeaveEmployeeList" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerikcontrol" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
    <title>Leave</title>
     <link rel="stylesheet" href="../../../AdminLTE/bootstrap/css/bootstrap.min.css"/>     
    <link rel="stylesheet" href="../../../AdminLTE/dist/css/Admin-lte.min.css"/>
    <link rel="stylesheet" href="../../../AdminLTE/plugins/ionicons/css/ionicons.min.css"/>
    <link rel="stylesheet" href="../../../Skins/_all-skins.min.css"/>
    <link rel="stylesheet" href="../../../css/font-awesome.min.css"/>
    </head>
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
            var GridVwHeaderChckbox = document.getElementById("<%=GridView1.ClientID %>");
            for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }
    </script>
    
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
        </style>
    <body >
        <form id="form1" action ="">
        <div class="container">
            <div id="divalert" runat="server" visible="false" class="alert alert-info">
                <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                <strong id="msgalert" runat="server">Danger!</strong>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-8">
                <h4 id="pagetitle" runat="server" class="page-title" style="color:#1BA691">
                    Head</h4>
            </div>
        </div>
        <div class="row filter-row">
            
                 <button id="backlink" type="button" runat="server" class="btn-info" onserverclick="btnBack_Click"
                style="height: 30px; width: 150px">
                << Back</button>
          
                <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="Delete" OnClientClick="Confirm()"
                    BackColor="#FF3300" ForeColor="White" Width="150px" Height="30px" CssClass="btn-danger"
                    BorderStyle="None" Font-Names="Verdana" Font-Size="12px" />                           
        </div>
        <br />
        <div class="row">
            <div class="table-responsive">
                <asp:GridView ID="GridView1" runat="server" OnSorting="SortRecords" AllowSorting="True"
                    BorderStyle="Solid" Font-Names="Verdana" AllowPaging="True" PageSize="50" DataKeyNames="refno"
                    Width="100%" Height="50px" ToolTip="click row to select record"
                    Font-Size="12px" ShowHeaderWhenEmpty="True" EmptyDataText="No data to display"
                    AutoGenerateColumns="False" GridLines="Both" ForeColor="#666666" BorderWidth="1px"
                    BorderColor="#CCCCCC" 
                    CssClass="table table-condensed">
                    <RowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField ItemStyle-Width="1%" ItemStyle-HorizontalAlign="Center">
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="CheckAllEmp(this);" />
                            </HeaderTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemTemplate>
                                <asp:CheckBox ID="chkEmp" runat="server" AutoPostBack="false"></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Rows" HeaderText="Rows" SortExpression="rows" />   
                        <asp:TemplateField HeaderText="Leave" SortExpression="Leave Type">
                            <ItemTemplate>                                             
                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/Employee/LeaveManagement/LeaveToApprove.aspx?id={0}",
                                HttpUtility.UrlEncode(Eval("refno").ToString())) %>' Text='<%# Eval("Leave Type")%>' />
                            </ItemTemplate>
                            <ItemStyle Font-Bold="True"/>
                        </asp:TemplateField>                                                       
                        <asp:BoundField DataField="employee name" HeaderText="Employee" SortExpression="employee name" /> 
                        <asp:BoundField DataField="mgrapproval" HeaderText="Approval Status" SortExpression="mgrapproval" /> 
                        <asp:BoundField DataField="hrapproval" HeaderText="Human Resource Status" SortExpression="hrapproval" />
                        <asp:BoundField DataField="from" HeaderText="From" SortExpression="from" DataFormatString="{0:dd, MMM yyyy}"  /> 
                        <asp:BoundField DataField="to" HeaderText="To" SortExpression="to" DataFormatString="{0:dd, MMM yyyy}"  /> 
                        <asp:BoundField DataField="No of Days" HeaderText="Leave Days" SortExpression="No of Days" ItemStyle-HorizontalAlign="Right"   /> 
                    </Columns>
                    <HeaderStyle BackColor="White" ForeColor="#1BA691" HorizontalAlign="center" />
                </asp:GridView>
                <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
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
        </div>

        <%--<div>
            <asp:Label ID="lblstatus" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="12px"
                ForeColor="Red" Width="100%"></asp:Label>
        </div>
        <div style="border: thin solid #C0C0C0">
            <table width="100%">
                <tr>
                    <td>
                        <asp:Label ID="lblView" runat="server" Font-Names="Candara" Width="100%"
                            Style="font-weight: 700; color: #FF6600"></asp:Label>
                    </td>
                </tr>
            </table>
            <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        </div>      
        <div>
            <div>                        
                <table class="style21">
                    <tr>
                        <td class="style23">
                            <asp:Button ID="btnDelete" runat="server" BackColor="#FF3300" 
                                BorderStyle="None" Font-Names="Verdana" Font-Size="11px" ForeColor="White" 
                                Height="20px" OnClick="Delete" OnClientClick="Confirm()" Text="Delete" 
                                Width="100px" ToolTip="Delete leave pending approval" />
                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
                    BorderStyle="Solid" DataKeyNames="refno" Font-Names="Verdana" Font-Size="12px"
                    Height="50px" OnRowDataBound="OnRowSurbodinateDataBound" OnSorting="SortSurbodinateRecords"
                    PageSize="200" ToolTip="click row to select record" Width="100%" AutoGenerateColumns="False" GridLines="Vertical" ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC"
                    ShowHeaderWhenEmpty="True" CssClass="table">
                    <AlternatingRowStyle BackColor="#CCCCCC" />
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="CheckAllEmp(this);" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkEmp" runat="server" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="2px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Rows" HeaderText="Rows">
                            <ItemStyle Width="5px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Ref No">
                            <ItemTemplate>                                             
                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/Employee/LeaveManagement/LeaveToApprove.aspx?id={0}",
                                HttpUtility.UrlEncode(Eval("refno").ToString())) %>'
                                onclick="window.open (this.href, 'popupwindow',  'width=700,height=600,scrollbars,resizable'); return false;"  Text='<%# Eval("refno")%>' />
                                                    </ItemTemplate>
                            <ItemStyle Font-Bold="True" Width="20px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Leave Type" HeaderText="Leave Type" SortExpression="Leave Type">
                            <ItemStyle Width="50px" />
                        </asp:BoundField>
                      
                        <asp:BoundField DataField="Employee Name" HeaderText="Employee" >
                            <ItemStyle Width="200px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="MgrApproval" HeaderText="Manager Approval" SortExpression="MgrApproval">
                            <ItemStyle Width="50px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="HRApproval" HeaderText="HR Approval" SortExpression="HRApproval">
                            <ItemStyle Width="50px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="From" HeaderText="Leave From" SortExpression="From">
                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="To" HeaderText="Leave To" SortExpression="To">
                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="No of Days" HeaderText="Days" SortExpression="No of Days">
                            <ItemStyle HorizontalAlign="Right" Width="15px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Status" HeaderText="Final Status" SortExpression="Status">
                            <ItemStyle Width="40px" />
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle BackColor="#1BA691" ForeColor="White" HorizontalAlign="Center" />
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
                <script type="text/javascript">
                    function openSubWindow(code) {
                        window.open("LeaveToApprove.aspx?id=" + code, "open_window", "width=600,height=500");
                    }
                </script>
            </div>
        </div>--%>

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
    
</asp:Content>
