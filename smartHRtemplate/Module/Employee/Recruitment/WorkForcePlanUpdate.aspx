<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="WorkForcePlanUpdate.aspx.vb"
    Inherits="GOSHRM.WorkForcePlanUpdate" EnableEventValidation="false" Debug="true" %>

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
            var GridVwHeaderChckbox = document.getElementById("<%=gridSummary.ClientID %>");
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
            if (confirm("Mark plan as complete and send notification to HR?")) {
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
                <div class=" col-md-10">
                    <div id="divalert" runat="server" visible="false" class="alert alert-info">
                        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <strong
                            id="msgalert" runat="server"></strong>
                        <asp:TextBox ID="txtid" runat="server" Width="1px" Font-Size="1px" Visible="False"></asp:TextBox>
                        <asp:Label ID="lblDate" runat="server" CssClass="lbl" Font-Size="1px" Visible="False"></asp:Label>
                        <asp:Label ID="lblentry" runat="server" CssClass="lbl" Font-Size="1px" Visible="False"></asp:Label>
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
                <div id="divjoboffer" runat="server" class="col-sm-3 col-md-6 col-xs-6 pull-left">
                    <p>
                        <a href="Workforce"><u>Workforce</u></a>
                        <label>
                            >
                        </label>
                        <a id="A1" href="#">Workforce Plan Breakdown</a>
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
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <label>
                                            UPDATE STATUS</label>
                                        <input id="lbstat" runat="server" class="form-control" type="text" disabled="disabled"
                                            value="On-going" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div id="divappstat" runat="server" class=" col-md-12">
                                    <div class="form-group">
                                        <label>
                                            APPROVAL STATUS</label>
                                        <input id="afinalstatus" runat="server" class="form-control" type="text" disabled="disabled" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12 m-t-20 text-center">
                                    <button id="btnapprovallink" runat="server" onserverclick="lnkApproval_Click" type="submit"
                                        class="btn btn-default ">
                                        View Approval Status</button>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <label>
                                            <i id="lbprogressstat" runat="server"></i>
                                        </label>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12 m-t-20">
                                    <button id="btsave" runat="server" onserverclick="btnSave_Click" type="submit" style="width: 150px"
                                        class="btn btn-primary btn-success ">
                                        Save &amp; Update</button>
                                    <asp:Button ID="btncomplete" runat="server" Text="Complete" OnClientClick="Complete()"
                                        Width="150px" Height="34px" CssClass="btn btn-default " BorderStyle="Solid"
                                        Font-Names="Verdana" Font-Size="13px" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="divsummary" runat="server" class="row">
            <div class="panel panel-success">
                <div class="panel-heading">
                    <b>BUDGET SUMMARY</b>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="table-responsive">
                            <asp:GridView ID="gridsummary" runat="server" OnSorting="SortSummaryRecords" AllowSorting="True"
                                BorderStyle="Solid" Font-Names="Verdana" AllowPaging="False" PageSize="12" DataKeyNames="rows"
                                Width="100%" Height="50px" ToolTip="click row to select record" Font-Size="12px"
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
                                class="btn btn-primary btn-success " title="Adopt budget for new month">
                                Copy to New Month</button>
                            <button id="btyearcopy" runat="server" onserverclick="btnNewYear_Click" type="submit"
                                class="btn btn-primary btn-info " title="Adopt budget for new financial year">
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
                        <div class="col-sm-3 col-md-4 col-xs-6">
                            <div class="form-group form-focus">
                                <input id="search" runat="server" type="text" class="form-control floating" style="height: 30px"
                                    placeholder="Search..." />
                                <button id="btnsearch" type="button" runat="server" class="glyphicon glyphicon-search"
                                    onserverclick="btnFind_Click" style="height: 29px; width: 40px">
                                </button>
                            </div>
                        </div>
                        <div id="divbtndetail" runat="server" class="col-sm-3 col-md-6 col-xs-6">
                            <button id="btadddetail" runat="server" onserverclick="btnAdd_Click" type="submit"
                                style="width: 150px" class="btn btn-primary btn-success " title="add detail to budget">
                                Add Detail</button>
                            <asp:Button ID="btdeletedetail" runat="server" Text="Delete Detail" OnClick="Delete"
                                OnClientClick="Confirm()" BackColor="#FF3300" ForeColor="White" Width="150px"
                                Height="34px" CssClass="btn btn-danger " BorderStyle="None" Font-Names="Verdana"
                                Font-Size="13px" />
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
                                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/Employee/Recruitment/WorkForcePlanDetailUpdate?id={0}&year={1}",
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
