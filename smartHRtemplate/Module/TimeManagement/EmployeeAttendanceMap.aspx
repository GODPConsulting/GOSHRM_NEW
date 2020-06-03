﻿<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="EmployeeAttendanceMap.aspx.vb"
    Inherits="GOSHRM.EmployeeAttendanceMap" EnableEventValidation="false" Debug="true" %>
    <%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
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
        .style28
        {
            width: 696px;
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
          </div>
     <div class="panel panel-success">
                <div class="panel-heading">
                    <h5><b id="pagetitle" runat="server"></b></h5>
                </div>
             <div class="panel-body">
        
         <div class="row">
         <div class="search-box-wrapper col-sm-6 col-md-3 col-xs-12 form-group pull-right">                        
                    <input id="txtsearch" style="width:100%" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                    <button onserverclick="Button1_Click" id="btsearch" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                </div>
                 <div class="col-md-3 col-sm-6 col-xs-12 form-group pull-right">                                  
                    <button id="btnUploadFile" type="button" runat="server" class="fa fa-upload btn btn-default btn-sm"
                        onserverclick="btnUpload_Click" data-toggle="tooltip" data-original-title="CSV File: AttendanceID, EmployeeID" style="margin-right:10px;margin-left:10px;height:35px"></button>
                        <button id="btExport" data-toggle="tooltip" data-original-title="Export" type="button" runat="server" class="fa fa-download btn btn-default btn-sm" onserverclick="Button1_Click1"
                    style="margin-right:10px;height: 35px"></button>
                        <asp:LinkButton ID="btnDelete" data-toggle="tooltip" data-original-title="Delete" Height="35px" runat="server" CssClass="btn btn-default btn-sm" OnClick="Delete" OnClientClick="Confirm()">
                            <span style="margin-top:5px" aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                        </asp:LinkButton>
                         <button id="btnConfig" type="button" data-toggle="tooltip" data-original-title="Config ZKTeco Data Source" runat="server" class="glyphicon glyphicon-cog btn btn-default btn-sm" onserverclick="btnAdd_Click"
                        style="height: 35px;margin-right:10px;margin-left:10px;"></button> 
                </div>
                <div class="col-md-3 col-sm-6 col-xs-12 pull-right">
                    <input style="height:35px;" class="form-control" type="file" id="FileUpload1" runat="server" />
                </div> 
         <div class="col-sm-6 col-md-3 col-xs-12 pull-right">
            <telerik:RadComboBox runat="server" Skin="Bootstrap"
                RenderMode="Lightweight" ResolvedRenderMode="Classic" Width="100%" ID="cboCompany"
                AutoPostBack="True" Font-Names="Verdana" ForeColor="#666666">
            </telerik:RadComboBox>
        </div> 
        </div>        
        <div style="border: thin solid #C0C0C0">
            <table style="width: 1573px">
                <tr>
                    
                </tr>
            </table>
            <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        </div>
       
        <div class="row">
            <div>
                <asp:GridView ID="GridVwHeaderChckbox" runat="server" OnSorting="SortRecords" AllowSorting="True"
                    BorderStyle="Solid" Font-Names="Verdana" AllowPaging="True" PageSize="1000" DataKeyNames="id" CssClass="table table-condensed" 
                    OnRowDataBound="OnRowDataBound" Width="100%" ToolTip="click row to select record"
                    Font-Size="12px" AutoGenerateColumns="False" GridLines="Vertical" ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC" ShowHeaderWhenEmpty="True">
                    <AlternatingRowStyle BackColor="white" />
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
                        <asp:BoundField DataField="Rows" ItemStyle-Width="2px" HeaderText="Rows" />
                        <asp:BoundField DataField="AttnID" ItemStyle-Width="50px" HeaderText="Attendance ID" />
                        <asp:BoundField DataField="EmpID" ItemStyle-Width="50px" HeaderText="Employee No" />
                        <asp:TemplateField HeaderText="Employee Name" ItemStyle-Width="150px" ItemStyle-Font-Bold="true">
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/Admin/EmploymentStatusUpdate.aspx?id={0}",
                     HttpUtility.UrlEncode(Eval("id").ToString())) %>' onclick="window.open (this.href, 'popupwindow',  'width=500,height=400,scrollbars,resizable'); return false;"
                                    Text='<%# Eval("Employee")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Office" ItemStyle-Width="200px" HeaderText="Department / Office" />
                        <asp:BoundField DataField="WorkShift" ItemStyle-Width="80px" HeaderText="Work Shift" />
                        <asp:BoundField DataField="timefrom" ItemStyle-Width="50px" HeaderText="Time From" />
                        <asp:BoundField DataField="timeto" ItemStyle-Width="50px" HeaderText="Time To"  />
                        <asp:BoundField DataField="CreatedOn" ItemStyle-Width="50px" HeaderText="Date Created" />
                        <asp:BoundField DataField="UpdatedOn" ItemStyle-Width="50px" HeaderText="Date Updated" />
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
                        window.open("EmploymentStatusUpdate.aspx?id=" + code, "open_window", "width=500,height=400");
                    }
                </script>
            </div>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
        </div></div>
        </div></div>
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
