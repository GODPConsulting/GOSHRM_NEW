<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="SalaryMasterPage.aspx.vb"
    Inherits="GOSHRM.Salary" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title>Employee Salary</title>
        <script type="text/javascript" language="javascript">
            //    Grid View Check box
            function CheckAllEmp(Checkbox) {
                var GridVwHeaderChckbox = document.getElementById("<%=GridVwHeaderChckbox.ClientID %>");
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
            function ConfirmGenerate() {
                var confirm_gen = document.createElement("INPUT");
                confirm_gen.type = "hidden";
                confirm_gen.name = "confirm_gen";
                if (confirm("Refresh to include new Salary Items?")) {
                    confirm_gen.value = "Yes";
                } else {
                    confirm_gen.value = "No";
                }
                document.forms[0].appendChild(confirm_gen);
            }
        </script>
        <script type="text/javascript">
            function ConfirmRefresh() {

                var confirm_ref = document.createElement("INPUT");
                confirm_ref.type = "hidden";
                confirm_ref.name = "confirm_ref";

                if (confirm("refresh entire selected company 's Employee Pay from Grade Pay Structure?")) {
                    confirm_ref.value = "Yes";
                } else {
                    confirm_ref.value = "No";
                }
                document.forms[0].appendChild(confirm_ref);
            }
        </script>
    </head>
    <body>
        <form id="form1">
        <div class="container col-md-12">
        <div class="row">
            <div class="col-xs-8 col-md-12">
                <div id="divalert" runat="server" visible="false" class="alert alert-info">
                    <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <strong
                        id="msgalert" runat="server">Danger!</strong>
                </div>
            </div>
        </div>
            <div id="content" runat="server">
         <div class="panel panel-success">
                <div class="panel-heading">
                   <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Always">
                    <ContentTemplate>
                        <h5 id="pagetitle" runat="server" class="page-title" style="color: #1BA691">
                            Head</h5>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="radOffice" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="chkActive" EventName="CheckedChanged" />
                    </Triggers>
                </asp:UpdatePanel>
                </div>
             <div class="panel-body">
        <div class="row">
             <div class="col-md-3 col-sm-6 col-xs-12 pull-right">
                     <asp:CheckBox ID="chkActive" runat="server" AutoPostBack="True" Font-Names="Verdana"
                    Text="Active" Font-Size="13px" Font-Bold="True" ForeColor="#666666" ToolTip="Active pay only"  />
                </div> 
                <div class="col-md-3 col-sm-6 col-xs-12 pull-right">
                    <asp:CheckBox ID="chkIsTransposed" runat="server" AutoPostBack="True" Font-Names="Verdana"
                    Text="Transposed Item" Font-Size="13px" Font-Bold="True" ToolTip="Check to transpose salary items if items are headers in upload file"
                    ForeColor="#666666" />
                </div>
                <div class="col-md-3 col-sm-6 col-xs-12 pull-right">
                     <asp:Button ID="btnRegenerate" runat="server" BorderStyle="None" ForeColor="White"
                    Height="30px" Text="Reset for New Items" Width="150px" ToolTip="Click to  generate Salary Schedule on first start or to add new pay item to employee payslip"
                    OnClientClick="ConfirmGenerate()" CssClass="btn btn-primary btn-success" Font-Size="12px"  />
                </div> 
                <div class="col-md-3 col-sm-6 col-xs-12 pull-right">
                     <asp:Button ID="btnImport" runat="server" BorderStyle="None" ForeColor="White" Height="30px"
                    Text="Reset All" Width="150px" ToolTip="Reset entire Employee salary structures"
                    CssClass="btn btn-primary btn-success" OnClientClick="ConfirmRefresh()" Font-Size="12px"/>
                </div>
                </div>   

         <div class="row">
            <div class="search-box-wrapper col-sm-6 col-md-3 col-xs-12 form-group pull-right">                        
                    <input id="search" style="width:100%" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                    <button onserverclick="btnFind_Click" id="btsearch" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                </div>
                 <div class="col-md-3 col-sm-6 col-xs-12 form-group pull-right">                                  
                    <button id="btnUploadFile" type="button" runat="server" class="fa fa-upload btn btn-default btn-sm"
                        onserverclick="btnUpload_Click" data-toggle="tooltip" data-original-title="CSV File: EMPID, Salary Item, Amount" style="margin-right:10px;margin-left:10px;height:35px"></button>
                        <button id="btExport" data-toggle="tooltip" data-original-title="Export" type="button" runat="server" class="fa fa-download btn btn-default btn-sm" onserverclick="btnExport_Click"
                    style="margin-right:10px;height: 35px"></button>
                        <asp:LinkButton ID="btDelete" data-toggle="tooltip" data-original-title="Delete" Height="35px" runat="server" CssClass="btn btn-default btn-sm" OnClick="Delete" OnClientClick="Confirm()">
                            <span style="margin-top:5px" aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                        </asp:LinkButton>
                         <button id="btAdd" type="button" data-toggle="tooltip" data-original-title="Add" runat="server" class="glyphicon glyphicon-plus btn btn-default btn-sm" onserverclick="btnAdd_Click"
                        style="height: 35px;margin-right:10px;margin-left:10px;"></button> 
                </div>
                <div class="col-md-3 col-sm-6 col-xs-12 pull-right">
                    <input style="height:35px;" class="form-control" type="file" id="file1" runat="server" />
                </div>
                <div class="col-md-3 col-sm-6 col-xs-12 pull-right">
                    <div id="divcompany" runat="server">
                        <telerik:radcombobox runat="server" rendermode="Lightweight"
                            resolvedrendermode="Classic" width="100%" id="radOffice" autopostback="True"
                            filter="Contains" forecolor="#666666" skin="Bootstrap">
                        </telerik:radcombobox>
                    </div>
                </div>              
        </div>
        <div class="row">
            <div class="table-responsive">
                <asp:UpdatePanel runat="server" ID="UpdatePanel5" UpdateMode="Always">
                    <ContentTemplate>
                        <asp:GridView ID="GridVwHeaderChckbox" runat="server" OnSorting="SortRecords" AllowSorting="True"
                            BorderStyle="Solid" Font-Names="Verdana" AllowPaging="True" PageSize="100" DataKeyNames="id"
                            OnRowDataBound="OnRowDataBound" Width="100%" Height="50px" ToolTip="click row to select record"
                            Font-Size="12px" ShowHeaderWhenEmpty="True" EmptyDataText="No data to display"
                            AutoGenerateColumns="False" GridLines="Both" ForeColor="#666666" BorderWidth="1px"
                            BorderColor="#CCCCCC" CssClass="table table-condensed">
                            <RowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField ItemStyle-Width="1%" ItemStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="CheckAllEmp(this);" />
                                    </HeaderTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkEmp" runat="server" AutoPostBack="false"></asp:CheckBox></ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Rows" ItemStyle-Width="5%" HeaderText="Row" SortExpression="rows" />
                                <asp:TemplateField HeaderText="Employee" ItemStyle-Font-Bold="true" SortExpression="Name">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#String.Format("~/Module/Finance/Payroll/SalaryMasterUpdate?id={0}&empid={1}",
                                             HttpUtility.UrlEncode(Eval("id").ToString()), HttpUtility.UrlEncode(Eval("Employee No").ToString())) %>' Text='<%# Eval("Name")%>' /></ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Grade" HeaderText="Grade" SortExpression="grade" />
                                <asp:BoundField DataField="PBT" HeaderText="Pay Before Tax" SortExpression="pbt"
                                    DataFormatString="{0:n}" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="Office" HeaderText="Office" SortExpression="office" />
                            </Columns>
                            <HeaderStyle BackColor="White" ForeColor="#1BA691" HorizontalAlign="center" />
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="radOffice" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="chkActive" EventName="CheckedChanged" />
                    </Triggers>
                </asp:UpdatePanel>
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
        </div>
        </div>  </div>  </div> </div>
        </form>
    </body>
    </html>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
</asp:Content>
