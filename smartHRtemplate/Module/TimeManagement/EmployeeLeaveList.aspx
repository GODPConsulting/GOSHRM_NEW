<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="EmployeeLeaveList.aspx.vb"
    Inherits="GOSHRM.EmployeeLeaveList" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerikcontrol" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
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
        function CheckAllEmp(Checkbox) {
            var GridVwHeaderChckbox = document.getElementById("<%=GridView1.ClientID %>");
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
    <body>
        <form>
    <div class="container col-md-12">
     <div class="row">
             <div id="divalert" runat="server" visible="false" class="alert alert-info">
                <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                <strong id="msgalert" runat="server">Danger!</strong>
            </div>
        </div>
     <div class="panel panel-success">
                <div class="panel-heading">
                    <h5><b id="pagetitle" runat="server"></b></h5>
                </div>
             <div class="panel-body">
         <div class="row">   
        <div>
            <div class="row form-group col-md-2 pull-right">                        
                          <button id="btclose" runat="server" onserverclick="Button2_Click" type="submit"
                                        style="width: 150px" class="btn btn-primary btn-info ">
                                        << Back</button>
            </div>
            <div class="row form-group col-md-2 pull-right">                        
                            <asp:Button ID="btnDelete" runat="server" 
                                BorderStyle="None" CssClass="btn btn-primary btn-danger" Font-Names="Verdana" Font-Size="12px" ForeColor="White" 
                                OnClick="Delete" OnClientClick="Confirm()" Text="Delete" 
                                Width="100%" ToolTip="Delete leave pending approval" />
            </div>
            <div>
                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
                    BorderStyle="Solid" DataKeyNames="refno" Font-Names="Verdana" Font-Size="11px"
                    Height="50px" OnSorting="SortSurbodinateRecords" EmptyDataText="No data to display"
                    PageSize="500" ToolTip="click row to select record" Width="100%" AutoGenerateColumns="False" GridLines="Vertical" ForeColor="#666666" BorderWidth="1px" 
                     BorderColor="#CCCCCC" ShowHeaderWhenEmpty="True" CssClass="table table-condensed">
                    <RowStyle BackColor="white" />
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="CheckAllEmp(this);" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkEmp" runat="server" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5px" />
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
                        <asp:BoundField DataField="Leave Type" HeaderText="Leave Type" SortExpression="Leave Type">
                        </asp:BoundField>
                        <asp:BoundField DataField="Employee No" HeaderText="Emp ID" >
                        </asp:BoundField>
                        <asp:BoundField DataField="Employee Name" HeaderText="Employee" >
                        </asp:BoundField>
                        <asp:BoundField DataField="MgrApproval" HeaderText="Manager Approval" SortExpression="MgrApproval">
                        </asp:BoundField>
                        <asp:BoundField DataField="HRApproval" HeaderText="HR Approval" SortExpression="HRApproval">
                        </asp:BoundField>
                        <asp:BoundField DataField="From" HeaderText="Leave From" SortExpression="From">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="To" HeaderText="Leave To" SortExpression="To">
                            <ItemStyle HorizontalAlign="Center"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="No of Days" HeaderText="Days" SortExpression="No of Days">
                            <ItemStyle HorizontalAlign="Right"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="Status" HeaderText="Final Status" SortExpression="Status">
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle BackColor="white" ForeColor="#1BA691" HorizontalAlign="Center" />
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
        </div>
        </div></div></div>
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