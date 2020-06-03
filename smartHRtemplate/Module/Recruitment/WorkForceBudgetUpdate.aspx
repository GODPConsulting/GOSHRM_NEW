<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="WorkForceBudgetUpdate.aspx.vb"
    Inherits="GOSHRM.WorkForceBudgetUpdate" EnableEventValidation="false" Debug="true" %>

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
    <script type="text/javascript">
        function Complete() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Are you sure you want to continue with this action, process cannot be reversed?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
    <body>
        <form id="form1" action="">
        <div class="main-wrapper">
            <div class="row">
                <div class=" col-md-8">
                    <div id="divalert" runat="server" visible="false" class="alert alert-info">
                        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <strong
                            id="msgalert" runat="server"></strong>
                        <asp:TextBox ID="txtid" runat="server" Width="1px" Font-Size="1px" Visible="False"></asp:TextBox>
                        <asp:Label ID="lblDate" runat="server" CssClass="lbl" Font-Size="1px" Visible="False"></asp:Label>
                        <asp:Label ID="lblentry" runat="server" CssClass="lbl" Font-Size="1px" Visible="False"></asp:Label>
                        <asp:Label ID="lbloriginalentry" runat="server" Font-Size="1px" Visible="False"></asp:Label>
                        <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Always">
                            <ContentTemplate>
                                <asp:Label ID="lblcompany" runat="server" CssClass="lbl" Font-Size="1px"></asp:Label>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="cboDept" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-8 col-md-offset-0">
                    <h5 id="pagetitle" runat="server" class="page-title">
                        Workforce Plan</h5>
                </div>
            </div>
            <div class="row">
                <div id="divjoboffer" runat="server" class="col-sm-3 col-md-12 col-xs-6 pull-left">
                    <p>
                        <a href="WorkforceBudget"><u>Workforce Budget & Planning</u></a>
                        <label>
                            >
                        </label>
                        <a id="A1" href="#">Workforce Budget & Planning Breakdown</a>
                    </p>
                </div>
            </div>
            <div class="row">
                <div class=" col-md-10">
                    <div class="panel panel-success">
                        <div class="panel-body">
                            <div class="row">
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <label>
                                            YEAR</label>
                                        <telerik:radtextbox id="txtyear" runat="server" skin="Bootstrap" autopostback="True"
                                            width="100%" rendermode="Lightweight" emptymessage="Year">
                                        </telerik:radtextbox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <label>
                                            OFFICE</label>
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel5" UpdateMode="Always">
                                            <ContentTemplate>
                                                <telerik:radcombobox runat="server" forecolor="#666666" dropdownautowidth="Enabled"
                                                    rendermode="Lightweight" resolvedrendermode="Classic" width="100%" id="cboDept"
                                                    filter="Contains" autopostback="True" skin="Bootstrap">
                                                </telerik:radcombobox>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txtyear" EventName="TextChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <label>
                                            BUDGET</label>
                                        <input id="abudget" runat="server" class="form-control" type="text" disabled="disabled" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-4">
                                    <div class="form-group">
                                        <label>
                                            UPDATE STATUS</label>
                                        <input id="lbstat" runat="server" class="form-control" type="text" disabled="disabled"
                                            value="On-going" />
                                    </div>
                                </div>
                                <div id="divappstat" runat="server" class=" col-md-4">
                                    <div class="form-group">
                                        <label>
                                            APPROVAL STATUS</label>
                                        <input id="afinalstatus" runat="server" class="form-control" type="text" disabled="disabled" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <label style="font-size: 12px">
                                            <i id="lbprogressstat" runat="server"></i>
                                        </label>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12 text-right">
                                    <div class="form-group">
                                        <label style="font-size: 12px">
                                            <label>
                                                <i id="lbprogressstat0" runat="server"></i>
                                            </label>
                                        </label>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12 m-t-20 text-center">
                                    <button id="btnapprovallink" runat="server" onserverclick="lnkApprovalStat_Click"
                                        type="submit" class="btn btn-default " title="manage approvers for plan / budget">
                                        Approval Management</button>
                                    <button id="btsave" runat="server" onserverclick="btnSave_Click" type="submit" style="width: 150px"
                                        class="btn btn-primary btn-success ">
                                        Save &amp; Update</button>
                                    <asp:Button ID="btncomplete" runat="server" Text="Complete" OnClientClick="Complete()"
                                        Width="150px" Height="34px" CssClass="btn btn-default "
                                        Font-Names="Verdana" Font-Size="13px" />
                                    <asp:Button ID="btnMove" runat="server" Text="Adopt as Budget" OnClientClick="Complete()"
                                        ForeColor="White" Width="150px" Height="34px" CssClass="btn btn-info "
                                        BorderStyle="None" Font-Names="Verdana" Font-Size="13px" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div id="divsummary" runat="server" class="panel panel-success">
                <div class="panel-heading">
                    <b>BUDGET SUMMARY</b>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="table-responsive">
                            <asp:GridView ID="gridsummary" runat="server" OnSorting="SortSummaryRecords" AllowSorting="True"
                                BorderStyle="Solid" Font-Names="Verdana" AllowPaging="False" PageSize="12" DataKeyNames="rows"
                                Width="100%" Height="50px" ToolTip="click to breakdown budget" Font-Size="12px"
                                ShowHeaderWhenEmpty="True" EmptyDataText="No data to display" AutoGenerateColumns="False"
                                GridLines="Both" ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC"
                                CssClass="table table-condensed">
                                <RowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField ItemStyle-Width="1%" ItemStyle-HorizontalAlign="Center">
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkboxSelectAll0" runat="server" onclick="CheckAllEmp(this);" />
                                        </HeaderTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkEmp0" runat="server" AutoPostBack="false"></asp:CheckBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Rows" ItemStyle-Width="5%" HeaderText="Row" SortExpression="rows" />
                                    <asp:TemplateField HeaderText="Month" ItemStyle-Font-Bold="true">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDownload" Text='<%# Eval("endmonth")%>' CommandArgument='<%# Eval("endmonth") %>'
                                                runat="server" OnClick="DrillDown"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="payrollbudget" HeaderText="Payroll Budget" ItemStyle-HorizontalAlign="right"
                                        DataFormatString="{0:n}" />
                                    <asp:BoundField DataField="gratuity" HeaderText="Gratuity" ItemStyle-HorizontalAlign="right"
                                        DataFormatString="{0:n}" />
                                    <asp:BoundField DataField="trainingbudget" HeaderText="Training Budget" ItemStyle-HorizontalAlign="right"
                                        DataFormatString="{0:n}" />
                                    <asp:BoundField DataField="otherexpense" HeaderText="Other Expense" ItemStyle-HorizontalAlign="right"
                                        DataFormatString="{0:n}" />
                                    <asp:BoundField DataField="budget" HeaderText="Total Budget" ItemStyle-HorizontalAlign="right"
                                        DataFormatString="{0:n}" />
                                </Columns>
                                <HeaderStyle BackColor="White" ForeColor="#1BA691" HorizontalAlign="center" />
                            </asp:GridView>
                            <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
                            <script type="text/javascript">
                                $(function () {
                                    $("[id*=gridsummary] td").hover(function () {
                                        $("td", $(this).closest("tr")).addClass("hover_row");
                                    }, function () {
                                        $("td", $(this).closest("tr")).removeClass("hover_row");
                                    })
                                })
                            </script>
                        </div>
                    </div>
                    <div class="row">
                        <div class="m-t-20">
                            <button id="btmonthcopy" runat="server" onserverclick="btnNewMonth_Click" type="submit"
                                class="btn btn-default " title="Adopt budget for new month">
                                Copy to New Month</button>
                            <button id="btyearcopy" runat="server" onserverclick="btnNewYear_Click" type="submit"
                                class="btn btn-default " title="Adopt budget for new financial year">
                                Copy to Next Year</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="divdetail" runat="server" class="row">
            <div class="panel panel-success">
                <div class="panel-heading">
                    <b id="divdetailheader" runat="server"></b>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="search-box-wrapper col-sm-6 col-md-4 col-xs-12 form-group pull-right">
                              <asp:LinkButton ID="btdeletedetail" data-toggle="tooltip" data-original-title="Delete Details" Height="35px" runat="server" CssClass="btn btn-default btn-sm" OnClick="Delete" OnClientClick="Confirm()">
                            <span style="margin-top:5px" aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                                </asp:LinkButton>
                         <button id="btadddetail" type="button" data-toggle="tooltip" data-original-title="Add Details" runat="server" class="glyphicon glyphicon-plus btn btn-default btn-sm" onserverclick="btnAdd_Click"
                        style="margin-left:10px;margin-right:10px;height: 35px;"></button> 
                             <input id="search" style="width:100%" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                    <button onserverclick="btnFind_Click" id="btnsearch" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                            </div>
                    </div>
                    <div class="row">
                        <div class="table-responsive">
                            <asp:GridView ID="GridVwHeaderChckbox" runat="server" OnSorting="SortRecords" AllowSorting="True"
                                BorderStyle="Solid" Font-Names="Verdana" AllowPaging="False" PageSize="15" DataKeyNames="rows"
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
                                    <asp:TemplateField HeaderText="Job Title" ItemStyle-Font-Bold="true" SortExpression="jobtitle">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/Employee/Recruitment/WorkForcePlanDetailUpdate?id={0}&year={1}&mode=hr",
                     HttpUtility.UrlEncode(Eval("id").ToString()),HttpUtility.UrlEncode(Eval("budgetyear").ToString())) %>' Text='<%# Eval("jobtitle")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="jobgrade" HeaderText="Job Grade" SortExpression="jobgrade" />
                                    <asp:BoundField DataField="amountrequired" HeaderText="Requirement" ItemStyle-HorizontalAlign="right"
                                        SortExpression="amountrequired" />
                                    <asp:BoundField DataField="payrollbudget" HeaderText="Payroll Budget" ItemStyle-HorizontalAlign="right"
                                        DataFormatString="{0:n}" SortExpression="payrollbudget" />
                                    <asp:BoundField DataField="gratuity" HeaderText="Gratuity" ItemStyle-HorizontalAlign="right"
                                        DataFormatString="{0:n}" SortExpression="gratuity" />
                                    <asp:BoundField DataField="trainingbudget" HeaderText="Training Budget" ItemStyle-HorizontalAlign="right"
                                        DataFormatString="{0:n}" SortExpression="trainingbudget" />
                                    <asp:BoundField DataField="otherexpense" HeaderText="Other Expense" ItemStyle-HorizontalAlign="right"
                                        DataFormatString="{0:n}" SortExpression="otherexpense" />
                                    <asp:BoundField DataField="budget" HeaderText="Budget" ItemStyle-HorizontalAlign="right"
                                        DataFormatString="{0:n}" SortExpression="budget" />
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
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 text-right">
                <div class="form-group">
                    <label style="font-size: 11px">
                        <i id="createdon" runat="server"></i>
                    </label>
                </div>
                <div class="form-group">
                    <label style="font-size: 11px">
                        <i id="updatedon" runat="server"></i>
                    </label>
                </div>
            </div>
        </div>
        <%--<div>
            <table >
                <tr>
                   
                    <td >
                        <asp:Label ID="lblView" runat="server" Font-Names="Candara" Font-Size="20px" Width="100%"
                            Style="font-weight: 400; color: #FF6600" Font-Bold="True"></asp:Label>
                    </td>
                 
                </tr>
        </table>
        </div>
         
    <div >
        <table style ="width:100%">
            <tr>
                <td class="style32" >
                    <asp:Label ID="Label5" runat="server" CssClass="lbl" Text="Company:" Font-Names="Verdana" ForeColor="#666666"
                        Font-Size="12px" Font-Bold="True"></asp:Label>
                </td>
                <td class="style29">
                    <asp:Label ID="lblcompany" runat="server" CssClass="lbl" Font-Names="Verdana" ForeColor="#666666"
                        Font-Size="12px" Font-Bold="True"></asp:Label>
                </td>
                <td class="style30">
                </td>
                <td class="style28" >
                    <asp:Label ID="Label1" runat="server" CssClass="lbl" Text="Budget Year:" Font-Names="Verdana" ForeColor="#666666"
                        Font-Size="12px" Font-Bold="True"></asp:Label>
                </td>
                <td class="style31">

                <telerik:RadDropDownList ID="radBudgetYear" runat="server"  ForeColor="#666666"
                    DefaultMessage="-- Select --" Font-Names="Verdana" Font-Size="12px"
            Height="23px" Width="150px" AutoPostBack="True" RenderMode="Lightweight" >
                </telerik:RadDropDownList>
                                            
                </td>
                <td>
                </td>
                <td>
        <asp:Label ID="lbloriginalentry" runat="server" CssClass="lbl" Font-Names="Verdana" ForeColor="#666666"
                        Font-Size="12px" Visible="False"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style32" >
                    <asp:Label ID="Label23" runat="server" CssClass="lbl" Text="Office/Dept:" Font-Names="Verdana" ForeColor="#666666"
                        Font-Size="11px" Font-Bold="True"></asp:Label>
                </td>
                <td class="style29">

                               <asp:UpdatePanel runat="server" ID="UpdatePanel13" UpdateMode="Always">
                        <ContentTemplate>
                           <telerik:RadComboBox runat="server" ForeColor="#666666"
                    DropDownAutoWidth="Enabled" RenderMode="Lightweight" 
                    ResolvedRenderMode="Classic" Width="400px" ID="cboDept" 
                    Font-Names="Verdana" Font-Size="12px" Filter="Contains">
</telerik:RadComboBox>
                        </ContentTemplate>
                        <Triggers>                            
                            <asp:AsyncPostBackTrigger ControlID="radBudgetYear" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                                            
                </td>
                <td class="style30">
                </td>
                <td class="style28" >
                    
                    <asp:Label ID="Label2" runat="server" CssClass="lbl" Text="Budget Sum:" Font-Names="Verdana" ForeColor="#666666"
                        Font-Size="12px" Font-Bold="True"></asp:Label>
                    
                </td>
                <td class="style31">
                    
                    <asp:Label ID="lblBudget" runat="server" CssClass="lbl" Font-Names="Verdana" ForeColor="#666666"
                        Font-Size="12px"></asp:Label>
                    
                </td>
                <td>
                </td>
                <td>
        <asp:Label ID="lblentry" runat="server" CssClass="lbl" Font-Names="Verdana" ForeColor="#666666"
                        Font-Size="12px" Visible="False"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style32" >
                    
                    <asp:Label ID="lblfinalappid" runat="server" CssClass="lbl" ForeColor="#666666"
                        Text="Final Approval Status:" Font-Names="Verdana"
                        Font-Size="11px" Font-Bold="True"></asp:Label>
                    
                </td>
                <td class="style29">
                    
        <asp:Label ID="lblfinalapprove" runat="server" CssClass="lbl" Font-Names="Verdana" ForeColor="#666666"
                        Font-Size="12px" Font-Bold="True"></asp:Label>
                    
                </td>
                <td class="style30">
                </td>
                <td class="style28" >
                    
                </td>
                <td class="style31">
                  
                    <asp:LinkButton ID="lnkApprovalStat" runat="server" Font-Bold="True" 
                        Font-Names="Verdana" Font-Size="11px">Approval Status View</asp:LinkButton>
                  
                </td>
                <td>
                </td>
                <td>
        <asp:Label ID="lblid" runat="server" CssClass="lbl" Font-Names="Verdana"
                        Font-Size="12px" Visible="False"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style32" >
                    
                </td>
                <td class="style29">
                    
                </td>
                <td class="style30">
                </td>
                <td class="style28" >
                    
                    &nbsp;</td>
                <td class="style31">
                  
                    &nbsp;</td>
                <td>
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style32" >
                    <asp:Label ID="Label18" runat="server" CssClass="lbl" Text="Created By:" Font-Names="Verdana" ForeColor="#666666"
                        Font-Size="11px" Font-Italic="True" Font-Bold="True"></asp:Label>
                </td>
                <td class="style29">
                    <asp:Label ID="lblcreatedby" runat="server" CssClass="lbl" Font-Names="Verdana" ForeColor="#666666"
                        Font-Size="11px" Font-Italic="True"></asp:Label>
                </td>
                <td class="style30">
                </td>
                <td class="style28" >
                    <asp:Label ID="Label21" runat="server" CssClass="lbl" Text="Updated By:" Font-Names="Verdana" ForeColor="#666666"
                        Font-Size="11px" Font-Italic="True" Font-Bold="True"></asp:Label>
                </td>
                <td class="style31">
                    <asp:Label ID="lblupdatedby" runat="server" CssClass="lbl" Font-Names="Verdana" ForeColor="#666666"
                        Font-Size="11px" Font-Italic="True"></asp:Label>
                </td>
                <td>
                </td>
                <td>
        <asp:Label ID="lblDate" runat="server" CssClass="lbl" Font-Names="Verdana" ForeColor="#666666"
                        Font-Size="12px" Visible="False" Width="20px"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style32" >
                    <asp:Label ID="Label20" runat="server" CssClass="lbl" Text="Created On:" Font-Names="Verdana" ForeColor="#666666"
                        Font-Size="11px" Font-Italic="True" Font-Bold="True"></asp:Label>
                </td>
                <td class="style29">
                    <asp:Label ID="lblcreatedon" runat="server" CssClass="lbl" Font-Names="Verdana" ForeColor="#666666"
                        Font-Size="11px" Font-Italic="True"></asp:Label>
                </td>
                <td class="style30">
                </td>
                <td class="style28" >
                    <asp:Label ID="Label22" runat="server" CssClass="lbl" Text="Updated On:" Font-Names="Verdana" ForeColor="#666666"
                        Font-Size="11px" Font-Italic="True" Font-Bold="True"></asp:Label>
                </td>
                <td class="style31">
                    <asp:Label ID="lblupdatedon" runat="server" CssClass="lbl" Font-Names="Verdana" ForeColor="#666666"
                        Font-Size="11px" Font-Italic="True"></asp:Label>
                </td>
                <td>
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style32" >
                    <asp:Label ID="lblApproverCommentTag5" runat="server" CssClass="lbl"  ForeColor="#666666"
                        Text="Update Status:" Font-Names="Verdana"
                        Font-Size="12px" Font-Bold="True"></asp:Label>
                </td>
                <td class="style29">
                    <asp:Label ID="lblprogressstat" runat="server" CssClass="lbl" Font-Names="Verdana" ForeColor="#666666"
                        Font-Size="12px" Font-Bold="True"></asp:Label>
                </td>
                <td class="style30">
                </td>
                <td class="style28" >
                </td>
                <td class="style31" colspan="3">
                    <asp:Label ID="lblprogressstat0" runat="server" CssClass="lbl" 
                        Font-Names="Verdana" ForeColor="#666666"
                        Font-Size="12px" Font-Bold="True" Font-Italic="True" Width="100%"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            </table>
     </div>
      <div style="border: thin solid #C0C0C0">
            <table class="style21">
                <tr>
                                       <td class="style25">
                        <asp:Button ID="btnSave" runat="server" Text="Save" BackColor="#1BA691" ForeColor="White"
                            Width="90px" Height="20px" BorderStyle="None" Font-Names="Verdana" 
                            Font-Size="11px" />
                    </td>
                    <td class="style26">
                        <asp:Button ID="btnBack" runat="server" Text="Back"
                            BackColor="#999966" ForeColor="White" Width="90px" Height="20px" 
                            BorderStyle="None" Font-Names="Verdana" Font-Size="11px" />
                    </td>
                    <td>                       
                       <asp:Button ID="btnComplete" runat="server" Text="Complete" OnClientClick="Complete()"
                            BackColor="#1BA691" ForeColor="White" Width="90px" Height="20px" 
                            BorderStyle="None" Font-Names="Verdana" Font-Size="11px" 
                            ToolTip="Mark Plan as complete and send Approval Notifications" />
                    </td>
                    <td>                       
                       <asp:Button ID="btnMove" runat="server" Text="Adopt as Budget" OnClientClick="Complete()"
                            BackColor="#0099CC" ForeColor="White" Width="120px" Height="20px" 
                            BorderStyle="None" Font-Names="Verdana" Font-Size="11px" 
                            ToolTip="Move Dept/Unit Plan to Budget" Font-Bold="True" />
                    </td>
                </tr>
            </table>
            <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        </div>
    
        <div>
        <table width="100%">
            <tr>
                <td style ="width:20%">
                </td>
                <td style ="width:60%">
                    <asp:GridView ID="gridSummary" runat="server"              
                    BorderStyle="Solid" Font-Names="Verdana" AllowPaging="True" DataKeyNames="rows"
                    Width="100%" Height="50px" ToolTip="click row to view detail" 
                    Font-Size="11.5px" AutoGenerateColumns="False" GridLines="Vertical" ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC" ShowHeaderWhenEmpty="True" EmptyDataText="No data to display" 
                    PageSize="12">
                    <AlternatingRowStyle BackColor="#CCCCCC" />
                    <Columns>
                        <asp:TemplateField ItemStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkboxSelectAll0" runat="server" Enabled="false" 
                                    onclick="CheckAllEmp0(this);" />
                            </HeaderTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"   />
                            <ItemTemplate>
                                <asp:CheckBox ID="chkEmp0" runat="server" Enabled="false"></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>                    
                        <asp:BoundField DataField="Rows" ItemStyle-Width="5px" HeaderText="Row" /> 
                        <asp:TemplateField HeaderText="Month" ItemStyle-Width="60px" ItemStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkDownload" Text = '<%# Eval("endmonth")%>' CommandArgument = '<%# Eval("endmonth") %>' runat="server" OnClick="DrillDown"></asp:LinkButton>
                        </ItemTemplate>
                        </asp:TemplateField>  
                        <asp:BoundField DataField="payrollbudget" ItemStyle-Width="20px" HeaderText="Payroll Budget" 
                            ItemStyle-HorizontalAlign="right" DataFormatString="{0:n}"/>
                         <asp:BoundField DataField="gratuity" ItemStyle-Width="20px" HeaderText="Gratuity Budget" 
                            ItemStyle-HorizontalAlign="right" DataFormatString="{0:n}"/>
                            <asp:BoundField DataField="trainingbudget" ItemStyle-Width="20px" HeaderText="Training Budget" 
                            ItemStyle-HorizontalAlign="right" DataFormatString="{0:n}"/>
                            <asp:BoundField DataField="otherexpense" ItemStyle-Width="20px" HeaderText="Other Expense" 
                            ItemStyle-HorizontalAlign="right" DataFormatString="{0:n}"/>                                        
                        <asp:BoundField DataField="budget" ItemStyle-Width="20px" HeaderText="Budget" 
                            ItemStyle-HorizontalAlign="right" DataFormatString="{0:n}"/>              
                    </Columns>
                    <HeaderStyle BackColor="#1BA691" ForeColor="White" HorizontalAlign="center" />
                </asp:GridView>
                </td>
                <td style ="width:20%">
                </td>
            </tr>
        </table> 
        <div style="border: thin solid #C0C0C0">
            <table width="100%">
                <tr> 
                      <td align="right" style="width:50%">                       
                       <asp:Button ID="btnNewMonth" runat="server" Text="Copy to New Month"
                            BackColor="#0099FF" ForeColor="White" Width="150px" Height="20px" 
                            BorderStyle="None" Font-Names="Verdana" Font-Size="11px" 
                            ToolTip="Adopt budget for new month" Font-Bold="True" />
                    </td>                        
                    <td align="left" style="width:50%">                       
                       <asp:Button ID="btnNewYear" runat="server" Text="Move to New Year"
                            BackColor="#FF6600" ForeColor="White" Width="150px" Height="20px" 
                            BorderStyle="None" Font-Names="Verdana" Font-Size="11px" 
                            ToolTip="Adopt budget for new financial year" Font-Bold="True" />
                    </td>
                </tr>
            </table>            
        </div>
        
        
                       
     </div>     
     <div>
            <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                <asp:View ID="BudgetDetail" runat="server">
                     <div>
                        <table >
                            <tr>                   
                                <td >
                                    <asp:Label ID="lblBudgetDetail" runat="server" Font-Names="Candara" Font-Size="16px" Width="100%"
                                        Style="font-weight: 300; color: #FF6600" Font-Bold="True"></asp:Label>                    
                                </td>                 
                            </tr>
                        </table>
                    </div>
                   <div >
                   <table class="style21">
                <tr>
                                       <td class="style25">
                        <asp:Button ID="btnAdd" runat="server" Text="Add Detail" BackColor="#1BA691" ForeColor="White"
                            Width="90px" Height="20px" BorderStyle="None" Font-Names="Verdana" 
                            Font-Size="11px" />
                    </td>
                    <td class="style26">
                        <asp:Button ID="btnDelete" runat="server" Text="Delete Detail" OnClick="Delete" OnClientClick="Confirm()"
                            BackColor="#FF3300" ForeColor="White" Width="90px" Height="20px" 
                            BorderStyle="None" Font-Names="Verdana" Font-Size="11px" />
                    </td>
                    <td>
                       
                        &nbsp;</td>
                </tr>
                    </table>
            <div>
                <asp:GridView ID="GridVwHeaderChckbox" runat="server" OnSorting="SortRecords" AllowSorting="True"
                    BorderStyle="Solid" Font-Names="Verdana" AllowPaging="True" DataKeyNames="id"
                    OnRowDataBound="OnRowDataBound" Width="100%" Height="50px" ToolTip="click row to view detail"
                    Font-Size="11px" AutoGenerateColumns="False" GridLines="Vertical" 
                    ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC" ShowHeaderWhenEmpty="True" EmptyDataText="No data to display" 
                    PageSize="50">
                    <AlternatingRowStyle BackColor="#CCCCCC" />
                    <Columns>
                        <asp:TemplateField ItemStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="CheckAllEmp(this);" />
                            </HeaderTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemTemplate>
                                <asp:CheckBox ID="chkEmp" runat="server"></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>                    
                        <asp:BoundField DataField="Rows" ItemStyle-Width="5px" HeaderText="Row" />                        
                       <asp:TemplateField HeaderText="Job Title" ItemStyle-Width="100px" 
                            ItemStyle-Font-Bold="true">
                            <ItemTemplate>
                                <a href="#" onclick='openWindow("<%# Eval("id") %>");'>
                                    <%# Eval("jobtitle")%></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="jobgrade" ItemStyle-Width="100px" HeaderText="Job Grade" />
                        <asp:BoundField DataField="min_education" ItemStyle-Width="100px" HeaderText="Min Education" />                                                
                         <asp:BoundField DataField="amountrequired" ItemStyle-Width="20px" HeaderText="Requirement" ItemStyle-HorizontalAlign="right"/> 
                         <asp:BoundField DataField="payrollbudget" ItemStyle-Width="20px" HeaderText="Payroll Budget" ItemStyle-HorizontalAlign="right" DataFormatString="{0:n}"/>  
                          <asp:BoundField DataField="gratuity" ItemStyle-Width="20px" HeaderText="Gratuity" ItemStyle-HorizontalAlign="right" DataFormatString="{0:n}"/> 
                          <asp:BoundField DataField="trainingbudget" ItemStyle-Width="20px" HeaderText="Training Budget" ItemStyle-HorizontalAlign="right" DataFormatString="{0:n}"/>   
                             
                            <asp:BoundField DataField="otherexpense" ItemStyle-Width="20px" HeaderText="Other Expense" ItemStyle-HorizontalAlign="right" DataFormatString="{0:n}"/>    
                        <asp:BoundField DataField="budget" ItemStyle-Width="20px" HeaderText="Budget" ItemStyle-HorizontalAlign="right" DataFormatString="{0:n}"/>            
                    </Columns>
                    <HeaderStyle BackColor="#1BA691" ForeColor="White" HorizontalAlign="center" />
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
                        window.open("WorkForceBudgetDetailUpdate.aspx?id=" + code, "open_window", "width=800,height=800");
                    }
                </script>
               
            </div>
            <div style="border: thin solid #C0C0C0">
            
                    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
                </div>

       
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
                </div>

            </asp:View>
            </asp:MultiView>
        </div>--%>
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
