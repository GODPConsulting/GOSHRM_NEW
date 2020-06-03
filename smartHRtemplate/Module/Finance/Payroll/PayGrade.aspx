﻿<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="PayGrade.aspx.vb"
    Inherits="GOSHRM.PayGrade" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <script type="text/javascript" language="javascript">
        //    Grid View Check box
        function CheckAllEmp(Checkbox) {
            var GridView1 = document.getElementById("<%=GridView1.ClientID %>");
            for (i = 1; i < GridView1.rows.length; i++) {
                GridView1.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }
    </script>
    <title></title>
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
        .style27
        {
            width: 35px;
        }
        .style29
        {
            font-size: large;
            font-weight: bold;
            color: #FFFFFF;
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
            <div class="search-box-wrapper col-sm-6 col-md-3 col-xs-12 form-group pull-right">                        
                    <input id="txtsearch" style="width:100%" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                    <button onserverclick="Button1_Click" id="btnFind" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                </div>
                 <div class="col-md-3 col-sm-6 col-xs-12 form-group pull-right">                                  
                    <button id="btnUploadFile" type="button" runat="server" class="fa fa-upload btn btn-default btn-sm"
                        onserverclick="btnUpload_Click" data-toggle="tooltip" data-original-title="CSV File: Grade, Payslip Item, Amount" style="margin-right:10px;margin-left:10px;height:35px"></button>
                        <button id="btExport" data-toggle="tooltip" data-original-title="Export" type="button" runat="server" class="fa fa-download btn btn-default btn-sm" onserverclick="Button1_Click1"
                    style="margin-right:10px;height: 35px"></button>
                        <asp:LinkButton ID="btnDelete" data-toggle="tooltip" data-original-title="Delete" Height="35px" runat="server" CssClass="btn btn-default btn-sm" OnClick="Delete" OnClientClick="Confirm()">
                            <span style="margin-top:5px" aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                        </asp:LinkButton>
                         <button id="btnAdd" type="button" data-toggle="tooltip" data-original-title="Add" runat="server" class="glyphicon glyphicon-plus btn btn-default btn-sm" onserverclick="btnAdd_Click"
                        style="height: 35px;margin-right:10px;margin-left:10px;"></button> 
                </div>
                <div class="col-md-3 col-sm-6 col-xs-12 pull-right">
                    <input style="height:35px;" class="form-control" type="file" id="FileUpload1" runat="server" />
                </div>
                <div class="col-md-3 col-sm-6 col-xs-12 pull-right">
                     <asp:CheckBox ID="chkIsTransposed" runat="server" AutoPostBack="True" 
                                        Font-Names="Verdana" Text="Pay Roll Items are transposed for Upload" Font-Size="11px" 
                                        Font-Bold="True" 
                                        ToolTip="ie the payroll items are the headers in the upload file" 
                                        ForeColor="#666666" />
                </div>     
        </div>

                
               
                  <div class="row">
                    <div>
                        <asp:GridView ID="GridView1" runat="server" OnSorting="SortRecords" AllowSorting="True" CssClass="table table-condensed"
                            BorderStyle="Solid" Font-Names="Verdana" AllowPaging="True" PageSize="100" DataKeyNames="id"
                            OnRowDataBound="OnRowDataBound" Width="100%" Height="50px" ToolTip="click row to select record"
                            Font-Size="12px" ShowHeaderWhenEmpty="True" EmptyDataText="No data to display" AutoGenerateColumns="False" GridLines="Vertical" ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC" >
                            <RowStyle BackColor="white" />
                            <Columns>
                                <asp:TemplateField ItemStyle-Width="1px">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="CheckAllEmp(this);" /></HeaderTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkEmp" runat="server" AutoPostBack="false"></asp:CheckBox></ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Rows" ItemStyle-Width="5px" HeaderText="Rows" SortExpression="rows" />
                                <asp:TemplateField HeaderText="Grade" ItemStyle-Font-Bold="true" SortExpression="grade">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/Finance/Payroll/PayGradesUpdate.aspx?id={0}",
                     HttpUtility.UrlEncode(Eval("id").ToString())) %>'
                                            Text='<%# Eval("grade")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="salaryitem" HeaderText="Payslip Item" SortExpression="salaryitem"/>
                                 <asp:BoundField DataField="amount" HeaderText="Monthly Amount" ItemStyle-HorizontalAlign="Right" SortExpression="amount" DataFormatString="{0:n}"/>
                                <asp:BoundField DataField="itemclass" HeaderText="Class" SortExpression="itemclass"/>
                                <asp:BoundField DataField="itemtype" HeaderText="Input Type" SortExpression="itemtype"/>
                                <asp:BoundField DataField="taxable" HeaderText="Taxable" SortExpression="taxable" ItemStyle-HorizontalAlign="Center"  />
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
                    </div>
                   <%-- <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:GOSHRMConnectionString %>"
                        SelectCommand="Job_Grade_TreeView" SelectCommandType="StoredProcedure"></asp:SqlDataSource>--%>
                </div>
</div></div></div>
        
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
