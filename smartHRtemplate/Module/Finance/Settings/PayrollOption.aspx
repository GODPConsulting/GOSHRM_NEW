﻿<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup ="true" CodeBehind="PayrollOption.aspx.vb" Inherits="GOSHRM.PayrollOption" EnableEventValidation="false" Debug="true"%>
<asp:Content ID = "content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <html xmlns="http://www.w3.org/1999/xhtml">
    <script type="text/javascript" language="javascript">
    //    Grid View Check box
        function CheckAllEmp(Checkbox) {
            var GridVwHeaderChckbox = document.getElementById("<%=GridVwHeaderChckbox.ClientID %>");
            for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }
    </script>

    

    

    <title>Roles</title>

    <script type = "text/javascript">
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
        .style25
        {
            width: 134px;
        }
        .style26
        {
            width: 22px;
        }
        </style>

<body>
   


    <form id="form1" >
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
                <div class="search-box-wrapper col-sm-6 col-md-4 col-xs-12 form-group pull-right">
                        <asp:LinkButton ID="btnDelete" data-toggle="tooltip" data-original-title="Delete" Height="35px" runat="server" CssClass="btn btn-default btn-sm" OnClick="Delete" OnClientClick="Confirm()">
                            <span style="margin-top:5px" aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                        </asp:LinkButton>
                         <button id="btnAdd" type="button" data-toggle="tooltip" data-original-title="Add Objective" runat="server" class="glyphicon glyphicon-plus btn btn-default btn-sm" onserverclick="btnAdd_Click"
                        style="margin-left:10px;margin-right:10px;height: 35px;"></button> 
                    <input id="txtsearch" style="width:100%" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                    <button onserverclick="Button1_Click" id="btsearch" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                </div>
             </div>
     
     
    <div class="row">
    
        <div>
            <asp:GridView ID="GridVwHeaderChckbox" runat="server" OnSorting="SortRecords" 
                AllowSorting="True" BorderStyle="Solid" 
                Font-Names="Verdana" AllowPaging="True" PageSize="500" DataKeyNames="id" 
                OnRowDataBound = "OnRowDataBound" CssClass="table table-condensed"
                 Width="100%" ToolTip="click row to select record"  
                Font-Size="11px" AutoGenerateColumns="False" GridLines="Vertical" ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC" 
                ShowHeaderWhenEmpty="True" EmptyDataText="No data to display" >
                <RowStyle BackColor="white" />
                <Columns>
                     <asp:TemplateField ItemStyle-Width="1px">
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="CheckAllEmp(this);" />
                        </HeaderTemplate>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemTemplate>
                            <asp:CheckBox ID="chkEmp" runat="server"></asp:CheckBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:BoundField DataField="Rows" HeaderText="Rows" />                        
                        <asp:TemplateField HeaderText="Company">
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/Finance/Settings/PayrollOptionUpdate.aspx?id={0}",
                                                     HttpUtility.UrlEncode(Eval("id").ToString())) %>'
                                    Text='<%# Eval("company") %>' />
                            </ItemTemplate>
                            <ItemStyle Font-Bold="True"></ItemStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="currency" HeaderText="Currency"/>
                        <asp:BoundField DataField="autoemailpayslip" HeaderText="Auto-Email Payslip"/>
                        <asp:BoundField DataField="SalaryBasedOnAttendance" HeaderText="Pay Based on Attendance"/>
                        <asp:BoundField DataField="perdaysalaryadjustment" HeaderText="New Recruits Pay"/>
                        <asp:BoundField DataField="PayOvertime" HeaderText="Pay Overtime"/>
                </Columns>
                <HeaderStyle BackColor="white" ForeColor="#1BA691" HorizontalAlign="center" />
                
            </asp:GridView>
            <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
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
                    window.open("LoanTypesUpdate.aspx?id=" + code, "open_window", "width=500,height=400");
                }
            </script>

   
           
        </div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
    
    </div></div></div></div>
    
    </form>
</body>
</html>
</asp:Content>

<asp:Content ID="Content2" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .style21
        {
            width: 100%;
        }
        .style22
        {
        }
        </style>
</asp:Content>


