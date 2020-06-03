<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="WorkForceBudget.aspx.vb"
    Inherits="GOSHRM.WorkForceBudget" EnableEventValidation="false" Debug="true" %>

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

     <script type="text/javascript" language="javascript">
         //    Grid View Check box
         function CheckAllEmp0(Checkbox) {
             var gridWorkPlan = document.getElementById("<%=gridWorkPlan.ClientID %>");
             for (i = 1; i < gridWorkPlan.rows.length; i++) {
                 gridWorkPlan.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
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
            width: 189px;
        }
    </style>
    <body>
        <form id="form1">
        <div class="row">
            <div id="divalert" runat="server" visible="false" class="alert alert-info">
                <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <strong
                    id="msgalert" runat="server">Danger!</strong>
            </div>
        </div>
        
        <div class="row">
          <div class="m-t-20 text-center">
                <button id="btbudget" runat="server" onserverclick="lnkworkbudget_Click" type="submit"
                    class="btn btn-default" title="Work Force Budget requiring your approval">
                    Work Force Budget</button>
                <button id="btplan" runat="server" onserverclick="lnkworkplanapp_Click" type="submit"
                    class="btn btn-default" title="Deprtment's WorkForce Plan requiring your approval" >
                    WorkForce Plan for Approvals</button>
            </div>
        </div>
        

                <div>
             
             <div id="details" runat="server">
                <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">   
                <asp:View ID="WorkForceBudgets" runat="server">
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Always">
                                <ContentTemplate>
                                    <h5 id="pagebudget" runat="server" class="page-title" style="color: #1BA691">
                                        Head</h5>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="cbobudgetCompany" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="radStatus" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="cboBudgetYear" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                     <div class="panel-body">
                    <div class="row">
                    <div class="search-box-wrapper col-sm-6 col-md-4 col-xs-12 form-group pull-right">
                        <asp:LinkButton ID="btDelete" data-toggle="tooltip" data-original-title="Delete" Height="35px" runat="server" CssClass="btn btn-default btn-sm" OnClick="Delete" OnClientClick="Confirm()">
                            <span style="margin-top:5px" aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                        </asp:LinkButton>
                         <button id="btAdd" type="button" data-toggle="tooltip" data-original-title="Add" runat="server" class="glyphicon glyphicon-plus btn btn-default btn-sm" onserverclick="btnAdd_Click"
                        style="margin-left:10px;margin-right:10px;height: 35px;"></button> 
                    <input id="searchbudget" style="width:100%" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                    <button onserverclick="btnFindBudget_Click" id="btsearch" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                </div>
                        <div class="col-sm-3 col-md-2 col-xs-6 pull-right ">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel4" UpdateMode="Always">
                                <ContentTemplate>
                                    <a href="#"><b id="lbtotaltitle" runat="server">Budget Total: </b><b id="lbbudgetcurrency" runat="server"></b><b id="lbbudgettotal" runat="server"></b></a>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="cbobudgetCompany" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="radStatus" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="cboBudgetYear" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <div class="col-sm-3 col-md-2 col-xs-6 pull-right">
                            <telerik:radcombobox id="radStatus" runat="server" width="100%" forecolor="#666666"
                                autopostback="True" skin="Bootstrap">
                            </telerik:radcombobox>
                        </div>
                        <div class="col-sm-3 col-md-2 col-xs-6 pull-right">
                            <telerik:radcombobox id="cboBudgetYear" runat="server" width="100%" forecolor="#666666"
                                autopostback="True" skin="Bootstrap">
                            </telerik:radcombobox>
                        </div>

                        <div class="col-sm-3 col-md-2 col-xs-6 pull-right">
                          <telerik:radcombobox runat="server" rendermode="Lightweight"
                    resolvedrendermode="Classic" width="100%" id="cbobudgetCompany" autopostback="True"
                    filter="Contains" forecolor="#666666" skin="Bootstrap">
                </telerik:radcombobox>
                </div>
                    </div>
                    <div class="row">
                        <div class="table-responsive">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Always">
                                <ContentTemplate>
                                    <asp:GridView ID="GridVwHeaderChckbox" runat="server"  AllowSorting="True"
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
                                            <asp:TemplateField HeaderText="Office" ItemStyle-Font-Bold="true" SortExpression="office">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/Recruitment/WorkForceBudgetUpdate?id={0}",
                     HttpUtility.UrlEncode(Eval("id").ToString())) %>' Text='<%# Eval("office")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="budget" HeaderText="Budget" SortExpression="budget" ItemStyle-HorizontalAlign="right"
                                                DataFormatString="{0:n}" />
                                            <asp:BoundField DataField="budgetstat" HeaderText="Completion Status" SortExpression="budgetstat" />
                                            <asp:BoundField DataField="finalstatus" HeaderText="Approval Status" SortExpression="finalstatus" />
                                            <asp:BoundField DataField="createdon" HeaderText="Date Created" SortExpression="createdon"
                                                DataFormatString="{0:dd, MMM yyyy}" />
                                        </Columns>
                                        <HeaderStyle BackColor="White" ForeColor="#1BA691" HorizontalAlign="center" />
                                    </asp:GridView>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="cbobudgetCompany" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="radStatus" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="cboBudgetYear" EventName="SelectedIndexChanged" />
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
                    </div></div></div>

                </asp:View>
                <asp:View ID="WorkPlanning" runat="server">
                     <div class="panel panel-success">
                        <div class="panel-heading">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel6" UpdateMode="Always">
                            <ContentTemplate>
                                <h5 id="pageplan" runat="server" class="page-title" style="color: #1BA691">
                                    Head</h5>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="cboplanCompany" EventName="SelectedIndexChanged" />
                                 <asp:AsyncPostBackTrigger ControlID="cboPlanStatus" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="cboPlanYear" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                        </div>
                     <div class="panel-body">
                    <div class="row">
                    <div class="search-box-wrapper col-sm-6 col-md-3 col-xs-12 form-group pull-right">
                         <button id="btnsearchplan" type="button" data-toggle="tooltip" data-original-title="Add" runat="server" class="glyphicon glyphicon-plus btn btn-default btn-sm" onserverclick="btnAdd_Click"
                        style="margin-left:10px;margin-right:10px;height: 35px;"></button> 
                    <input id="searchplan" style="width:100%" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                    <button onserverclick="btnFindPlan_Click" id="Button2" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                </div>
                        <div class="col-sm-3 col-md-3 col-xs-6 pull-right ">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Always">
                                <ContentTemplate>
                                    <a href="#"><b id="B1" runat="server">Budget Total: </b><b id="lbplancurrency" runat="server"></b><b id="lbplantotal" runat="server"></b></a>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="cboplanCompany" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="cboPlanStatus" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="cboPlanYear" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <div class="col-sm-3 col-md-2 col-xs-6 pull-right">
                            <telerik:radcombobox id="cboPlanStatus" runat="server" width="100%" forecolor="#666666"
                                autopostback="True" skin="Bootstrap">
                            </telerik:radcombobox>
                        </div>
                        <div class="col-sm-3 col-md-2 col-xs-6 pull-right">
                            <telerik:radcombobox id="cboPlanYear" runat="server" width="100%" forecolor="#666666"
                                autopostback="True" skin="Bootstrap">
                            </telerik:radcombobox>
                        </div>
                        <div class="col-sm-3 col-md-2 col-xs-6 pull-right">
                          <telerik:radcombobox runat="server" dropdownautowidth="Enabled" rendermode="Lightweight"
                    resolvedrendermode="Classic" width="100%" id="cboplanCompany" autopostback="True"
                    filter="Contains" forecolor="#666666" skin="Bootstrap">
                </telerik:radcombobox>
                </div>
                    </div>

                     <div class="row">
                        <div class="table-responsive">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel5" UpdateMode="Always">
                                <ContentTemplate>
                                    <asp:GridView ID="gridWorkPlan" runat="server"  AllowSorting="True"
                                        BorderStyle="Solid" Font-Names="Verdana" AllowPaging="True" PageSize="100" DataKeyNames="id"
                                        Width="100%" Height="50px" ToolTip="click row to select record" Font-Size="12px"
                                        ShowHeaderWhenEmpty="True" EmptyDataText="No data to display" AutoGenerateColumns="False"
                                        GridLines="Both" ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC"
                                        CssClass="table table-condensed">
                                        <RowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Width="1%" ItemStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkboxSelectAll0" runat="server" onclick="CheckAllEmp0(this);" />
                                                </HeaderTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkEmp" runat="server" AutoPostBack="false"></asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Rows" ItemStyle-Width="5%" HeaderText="Row" SortExpression="rows" />
                                            <asp:BoundField DataField="company" HeaderText="Company" SortExpression="company" />
                                            <asp:TemplateField HeaderText="Office" ItemStyle-Font-Bold="true" SortExpression="office">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/Recruitment/WorkForceBudgetUpdate?id={0}",
                     HttpUtility.UrlEncode(Eval("id").ToString())) %>' Text='<%# Eval("office")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="budget" HeaderText="Budget" SortExpression="budget" ItemStyle-HorizontalAlign="right"
                                                DataFormatString="{0:n}" />
                                            <asp:BoundField DataField="budgetstat" HeaderText="Completion Status" SortExpression="budgetstat" />
                                            <asp:BoundField DataField="finalstatus" HeaderText="Approval Status" SortExpression="finalstatus" />
                                            <asp:BoundField DataField="createdon" HeaderText="Date Created" SortExpression="createdon"
                                                DataFormatString="{0:dd, MMM yyyy}" />
                                        </Columns>
                                        <HeaderStyle BackColor="White" ForeColor="#1BA691" HorizontalAlign="center" />
                                    </asp:GridView>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="cboplanCompany" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="cboPlanStatus" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="cboPlanYear" EventName="SelectedIndexChanged" />
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
                    </div></div>
               
                </asp:View>
            </asp:MultiView>  
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
            width: 330px;
        }
        </style>
</asp:Content>
