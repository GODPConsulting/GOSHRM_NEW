<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="PayrollPeriod.aspx.vb"
    Inherits="GOSHRM.PayrollPeriod" EnableEventValidation="false" Debug="true" %>

<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%@ register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
    <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
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
    <title>Employee Salary</title>
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
        function ConfirmPayslip() {
            var confirm_payslip = document.createElement("INPUT");
            confirm_payslip.type = "hidden";
            confirm_payslip.name = "confirm_payslip";
            if (confirm("Generate Payslips for the selected Location, Start Date and End Date?")) {
                confirm_payslip.value = "Yes";
            } else {
                confirm_payslip.value = "No";
            }
            document.forms[0].appendChild(confirm_payslip);
        }
    </script>
    
    <body>
        <form id="form1">
        <div class="container col-md-12">
         <div class="row">
                <div id="divalert" runat="server" visible="false" class="alert alert-info">
                    <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <strong
                        id="msgalert" runat="server">Danger!</strong>
                </div>
        </div>
         <div class="panel panel-success">
                <div class="panel-heading">
                    <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Always">
                    <ContentTemplate>
                         <h5 id="pagetitle" runat="server" class="page-title" style="color: #1BA691">
                    Head</h5>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cbocompany" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="cboyear" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
                </div>
             <div class="panel-body">
        <div class="row">
            <div class="col-sm-3 col-md-3 col-xs-6">
                <telerik:radcombobox ID="cbocompany" runat="server" RenderMode="Lightweight"
                                                ResolvedRenderMode="Classic" Width="100%"
                    Skin="Bootstrap" AutoPostBack="True" EmptyMessage="Year">
                                            </telerik:radcombobox>
            </div>
           
          <div class="search-box-wrapper col-sm-6 col-md-4 col-xs-12 form-group pull-right">
                        <asp:LinkButton ID="btDelete" data-toggle="tooltip" data-original-title="Delete" Height="35px" runat="server" CssClass="btn btn-default btn-sm" OnClick="Delete" OnClientClick="Confirm()">
                            <span style="margin-top:5px" aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                        </asp:LinkButton>
                         <button id="btAdd" type="button" onserverclick="btnAdd_Click" data-toggle="tooltip" data-original-title="Add" runat="server" class="glyphicon glyphicon-plus btn btn-default btn-sm"
                        style="margin-left:10px;margin-right:10px;height: 35px;"></button> 
                    <input id="search" style="width:100%" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                    <button onserverclick="btnFind_Click" id="btsearch" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                </div>
                 <div class="col-sm-3 col-md-2 col-xs-6 pull-right">
                <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Always">
                    <ContentTemplate>
                         <telerik:radcombobox ID="cboyear" runat="server" RenderMode="Lightweight"
                                                ResolvedRenderMode="Classic" Width="100%"
                    Skin="Bootstrap" AutoPostBack="True" EmptyMessage="Year">
                                            </telerik:RadComboBox>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cbocompany" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>

            </div>
        </div>
        <div class="row">
            <div class="table-responsive">
            <asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Always">
                    <ContentTemplate>
                <asp:GridView ID="GridVwHeaderChckbox" runat="server" OnSorting="SortRecords" AllowSorting="True"
                    BorderStyle="Solid" Font-Names="Verdana" AllowPaging="True" PageSize="100" DataKeyNames="id"
                    Width="100%" Height="50px" ToolTip="click row to select record" Font-Size="12px"
                    ShowHeaderWhenEmpty="True" EmptyDataText="No data to display" AutoGenerateColumns="False"
                    GridLines="Both" ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC"
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
                        <asp:BoundField DataField="Rows" ItemStyle-Width="5%" HeaderText="Row" SortExpression="rows" />
                        <asp:TemplateField HeaderText="Period" ItemStyle-Font-Bold="true" SortExpression="period">
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/Finance/Payroll/PayrollPeriodData?id={0}",
                     HttpUtility.UrlEncode(Eval("id").ToString())) %>' Text='<%# Eval("period")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="netpay" HeaderText="Net Pay" SortExpression="netpay"  ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n}"/>
                                <asp:BoundField DataField="approveddate" HeaderText="Approval Date" SortExpression="approveddate"  />
                                <asp:BoundField DataField="ApprovalStatus" HeaderText="Approval" SortExpression="ApprovalStatus"   />                               
                                <asp:BoundField DataField="datecreated" HeaderText="Date Generated" SortExpression="datecreated" DataFormatString="{0:dd, MMM yyyy}"   />
                                <asp:BoundField DataField="status" HeaderText="Payroll Status" SortExpression="status"   />
                                <asp:TemplateField HeaderText="" ItemStyle-Font-Bold="true" SortExpression="Period" >
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%# string.Format("~/Module/Finance/Payroll/Payroll.aspx?id={0}&company={1}",
                     HttpUtility.UrlEncode(Eval("id").ToString()),HttpUtility.UrlEncode(Eval("company").ToString())) %>' Text='Employee Payroll' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                    </Columns>
                    <HeaderStyle BackColor="White" ForeColor="#1BA691" HorizontalAlign="center" />
                </asp:GridView>
                </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cbocompany" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="cboyear" EventName="SelectedIndexChanged" />
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
        </div> </div>
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
