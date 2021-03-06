﻿<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="KPIJobGrade.aspx.vb"
    Inherits="GOSHRM.KPIJobGrade" EnableEventValidation="false" Debug="true" %>

<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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

    <script type="text/javascript">
        function ShoeHide(img, div) {
            var current = $('#' + div).css('display');
            if (current == 'none') {
                $('#' + div).show('slow');
                $(img).attr('src', '/images/open-arrow.png');
            }
            else {
                $('#' + div).hide('slow');
                $(img).attr('src', '/images/close-arrow.png');
            }
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
          .style55
        {
            width: 100px;
        }
        .collapsed-row
        {
            display:none;
            padding:0px;
            margin:0px;
        }
        </style>
    <body style="">
        <form id="form1">
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
                         <button id="btAdd" type="button" data-toggle="tooltip" data-original-title="Add" runat="server" class="glyphicon glyphicon-plus btn btn-default btn-sm" onserverclick="btnAdd_Click"
                        style="margin-left:10px;margin-right:10px;height: 35px;"></button> 
                    <input id="txtsearch" style="width:100%" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                    <button onserverclick="Button1_Click" id="btsearch" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                </div>
                <div class="col-sm-6 col-md-4 col-xs-12 pull-right">
                    <asp:LinkButton ID="LinkButton2" runat="server" Font-Names="Verdana" 
                                Font-Size="12px" ToolTip="validate weight total per job grade">&nbsp;&nbsp;&nbsp;Summary Weight per Grade</asp:LinkButton>
                </div>         
        </div>
        
            <div class="row">
                <asp:GridView ID="GridVwHeaderChckbox" runat="server" OnSorting="SortRecords" AllowSorting="True"
                    BorderStyle="Solid" Font-Names="Verdana" AllowPaging="True" PageSize="100"
                    OnRowDataBound="OnRowDataBound" Width="100%" Height="50px" ToolTip="click row to select record"
                    Font-Size="12px" ShowHeaderWhenEmpty="True" EmptyDataText="No data to display"
                    AutoGenerateColumns="False" GridLines="Both" ForeColor="#666666" BorderWidth="1px"
                    BorderColor="#CCCCCC" CssClass="table table-condensed">
                    <RowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField ItemStyle-Width="5px">
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="CheckAllEmp(this);" />
                            </HeaderTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemTemplate>
                                <asp:CheckBox ID="chkEmp" runat="server"></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Rows" HeaderText="Rows" SortExpression="Rows" />
                        <asp:BoundField DataField="JobGrade" HeaderText="Job Grade" SortExpression ="JobGrade" />                     
                        <asp:BoundField DataField="CompetencyType" HeaderText="KPI Type" SortExpression ="CompetencyType" />
                        <asp:BoundField DataField="Weight" HeaderText="Weight (%)" ItemStyle-HorizontalAlign="Right" SortExpression="Weight" />

                        <asp:TemplateField HeaderText="KPIs" 
                            ItemStyle-Font-Bold="true">
                            <ItemTemplate>
                                <a href='KPIJobGradeUpdate.aspx?jobgrade=<%# Eval("JobGrade") %>&type=<%# Eval("CompetencyType") %>'>
                                  <%# Eval("Competencies")%></a>                                  
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle BackColor="White" ForeColor="#1BA691" HorizontalAlign="center" />
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
                    function openWindow(code,code1) {
                        window.open("KPIJobGradeUpdate.aspx?jobgrade=" + code + "&type=" + code1, "open_window", "width=700,height=500");
                    }
                </script>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
        </div>
        </div>
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
            width: 100%;
        }
        .style22
        {
        }
    </style>
</asp:Content>
