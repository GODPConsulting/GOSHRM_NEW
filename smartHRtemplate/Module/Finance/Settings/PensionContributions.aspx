<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="PensionContributions.aspx.vb"
    Inherits="GOSHRM.PensionContributions" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
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
            var GridVwHeaderChckbox = document.getElementById("<%=GridVwHeaderChckbox.ClientID %>");
            for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }
    </script>
    <title>Pension Contribution Setting</title>
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
        function Confirm2() {
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
    </style>
    <body>
        <form id="form1">
        <div class="container col-md-12">
        <div class="row">
            <div id="divalert" runat="server" visible="false" class="alert alert-info">
                <strong id="msgalert" runat="server">Danger!</strong>
                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                    <span aria-hidden="true">&times;</span></button>
            </div>
        </div>
        <div class="panel panel-success">
                <div class="panel-heading">
                    <h5 class="page-title"><b  id="pagetitle" runat="server"></b></h5>
                </div>
             <div class="panel-body">
        <div class="row text-center">
                        <asp:Button ID="btnregular" runat="server" BorderStyle="None"
                            ForeColor="White" Height="35px" Text="Pension Contribution" Width="150px" Font-Names="Arial"
                            Font-Size="12px" Font-Bold="True" />

                        <asp:Button ID="btnAdditional" runat="server" BorderStyle="None"
                            ForeColor="White" Height="35px" Text="Additional Contribution" Width="150px"
                            Font-Names="Arial" Font-Size="12px" Font-Bold="True" />
        </div>
        <div class="row">
            <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                <asp:View ID="vwPension" runat="server">
                <div class="row">
                <div class="search-box-wrapper col-sm-6 col-md-3 col-xs-12 form-group pull-right">                        
                    <input id="txtsearch" style="width:100%" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                    <button onserverclick="Button1_Click" id="btnFind" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                </div>
                 <div class="col-md-3 col-sm-6 col-xs-12 form-group pull-right">                                  
                    <button id="btnEmployerUpload" type="button" runat="server" class="fa fa-upload btn btn-default btn-sm"
                        onserverclick="btnEmployerUpload_Click" data-toggle="tooltip" data-original-title="CSV File: Job Grade, Employee Contribution(%),Employer Contribution(Fixed Amount)"
                         style="margin-right:10px;margin-left:10px;height:35px"></button>
                        <button id="btnExportEmployer" data-toggle="tooltip" data-original-title="Export" type="button" runat="server" class="fa fa-download btn btn-default btn-sm" onserverclick="btnExportEmployer_Click"
                    style="margin-right:10px;height: 35px"></button>
                        <asp:LinkButton ID="btnDelete" data-toggle="tooltip" data-original-title="Delete" Height="35px" runat="server" CssClass="btn btn-default btn-sm" OnClick="Delete" OnClientClick="Confirm()">
                            <span style="margin-top:5px" aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                        </asp:LinkButton>
                         <button id="btnAdd" type="button" data-toggle="tooltip" data-original-title="Add" runat="server" class="glyphicon glyphicon-plus btn btn-default btn-sm" onserverclick="btnApply_Click"
                        style="height: 35px;margin-right:10px;margin-left:10px;"></button> 
                </div>
                <div class="col-md-3 col-sm-6 col-xs-12 pull-right">
                    <input style="height:35px;" class="form-control" type="file" id="FileUpload1" runat="server" />
                </div>
           </div>
                    <div class="row">
                        <asp:GridView ID="GridVwHeaderChckbox" runat="server" AllowPaging="True" AllowSorting="True"
                            BorderStyle="Solid" DataKeyNames="id" Font-Names="Verdana" Font-Size="12px" Height="50px"
                            OnRowDataBound="OnRowDataBound" OnSorting="SortRecords" PageSize="1000" ToolTip="click row to select record"
                            Width="100%" AutoGenerateColumns="False" GridLines="Vertical" ForeColor="#666666" EmptyDataText="No data to display"
                            BorderWidth="1px" BorderColor="#CCCCCC" CssClass="table table-condensed" ShowHeaderWhenEmpty="True">
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
                                <asp:TemplateField HeaderText="Job Grade" SortExpression="jobgrade">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/Finance/Settings/EmployerPenConUpdate.aspx?id={0}",
            HttpUtility.UrlEncode(Eval("id").ToString())) %>' Text='<%# Eval("jobgrade")%>' />
                                    </ItemTemplate>
                                    <ItemStyle Font-Bold="True" HorizontalAlign="Left" VerticalAlign="Top" Width="100px">
                                    </ItemStyle>
                                </asp:TemplateField>
                                <asp:BoundField DataField="contribution" HeaderText="Employee (%)" DataFormatString="{0:n}"
                                    SortExpression="contribution">
                                    <ItemStyle HorizontalAlign="Right" Width="10px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="employercontribution" HeaderText="Employer (%)" DataFormatString="{0:n}"
                                    SortExpression="employercontribution">
                                    <ItemStyle HorizontalAlign="Right" Width="10px" />
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
                                window.open("EmployerPenConUpdate.aspx?id=" + code, "open_window", "width=600,height=400");
                            }
                        </script>
                    </div>
                </asp:View>
                <asp:View ID="optional" runat="server">
                    <div>
                     <div class="row">
                <div class="search-box-wrapper col-sm-6 col-md-3 col-xs-12 form-group pull-right">                        
                    <input id="txtSubSearch" style="width:100%" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                    <button onserverclick="btnSubFind_Click" id="btnSubFind" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                </div>
                 <div class="col-md-3 col-sm-6 col-xs-12 form-group pull-right">                                  
                    <button id="btnUploadEmployee" type="button" runat="server" class="fa fa-upload btn btn-default btn-sm"
                        onserverclick="btnUploadEmployee_Click" data-toggle="tooltip" data-original-title="CSV File: Emp ID, Employee Contribution(%)"
                         style="margin-right:10px;margin-left:10px;height:35px"></button>
                        <button id="btnExportEmployee" data-toggle="tooltip" data-original-title="Export" type="button" runat="server" class="fa fa-download btn btn-default btn-sm" onserverclick="btnExportEmployee_Click"
                    style="margin-right:10px;height: 35px"></button>
                        <asp:LinkButton ID="btnDelete2" data-toggle="tooltip" data-original-title="Delete" Height="35px" runat="server" CssClass="btn btn-default btn-sm" OnClick="btnDelete2_Click" OnClientClick="Confirm2()">
                            <span style="margin-top:5px" aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                        </asp:LinkButton>
                         <button id="Button1" type="button" data-toggle="tooltip" data-original-title="Add" runat="server" class="glyphicon glyphicon-plus btn btn-default btn-sm" onserverclick="Button1_Click1"
                        style="height: 35px;margin-right:10px;margin-left:10px;"></button> 
                </div>
                <div class="col-md-3 col-sm-6 col-xs-12 pull-right">
                    <input style="height:35px;" class="form-control" type="File" id="FileUpload2" runat="server" />
                </div>
                <div class="col-md-3 col-sm-6 col-xs-12 pull-right">
                    <telerik:RadComboBox runat="server" Skin="Bootstrap" DropDownAutoWidth="Enabled" RenderMode="Lightweight"
                                        ResolvedRenderMode="Classic" Width="100%" ID="cboCompany" AutoPostBack="True"
                                        Filter="Contains" Font-Names="Verdana" Font-Size="11px" ForeColor="#666666">                                        
                                    </telerik:RadComboBox>
                </div>
           </div>
                    </div>
                    <div class="row">
                        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
                            BorderStyle="Solid" DataKeyNames="id" Font-Names="Verdana" Font-Size="12px" Height="50px"
                            OnRowDataBound="OnRowSurbodinateDataBound" OnSorting="SortSurbodinateRecords" CssClass="table table-condensed"
                            PageSize="1000" ToolTip="click row to select record" Width="100%" AutoGenerateColumns="False"
                            GridLines="Vertical" ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC" ShowHeaderWhenEmpty="true" EmptyDataText="No data to display" > 
                            <RowStyle BackColor="White" />
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
                                <asp:TemplateField HeaderText="Emp ID" SortExpression="empid">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/Finance/Settings/EmployeePenConUpdate.aspx?id={0}",
                     HttpUtility.UrlEncode(Eval("id").ToString())) %>'  Text='<%# Eval("empid")%>' />
                                    </ItemTemplate>
                                    <ItemStyle Font-Bold="True" HorizontalAlign="Left" VerticalAlign="Top" Width="50px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="Name" HeaderText="Name">
                                    <ItemStyle HorizontalAlign="Left" Width="150px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Office" HeaderText="Unit/Dept">
                                    <ItemStyle HorizontalAlign="Left" Width="200px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="location" HeaderText="Location">
                                    <ItemStyle HorizontalAlign="Left" Width="100px"></ItemStyle>
                                </asp:BoundField>
                                <%--<asp:BoundField DataField="company" HeaderText="Company">
                                    <ItemStyle HorizontalAlign="Left" Width="100px"></ItemStyle>
                                </asp:BoundField>--%>
                                <asp:BoundField DataField="contribution" HeaderText="Contribution">
                                    <ItemStyle HorizontalAlign="Right" Width="15px"></ItemStyle>
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle BackColor="White" ForeColor="#1BA691" HorizontalAlign="Center" />
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

                            function openWindows(code) {
                                window.open("EmployeePenConUpdate.aspx?id=" + code, "open_window", "width=600,height=400");
                            }
                        </script>
                    </div>
                </asp:View>
            </asp:MultiView>
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
