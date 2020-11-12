<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="TerminalBenefits.aspx.vb"
    Inherits="GOSHRM.TerminalBenefits" EnableEventValidation="false" Debug="true" %>

       <%@ register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
    <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

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
    <title>Employee Payroll</title>
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
            if (confirm("Send Payslip to Employees?")) {
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
                <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                <strong id="msgalert" runat="server">Danger!</strong>
                <asp:Label ID="lblAppoval" runat="server" Font-Names="Verdana" 
                            Font-Size="Smaller" Width="30%" Style="font-weight: 700; color: #FF6600"></asp:Label>
                            <asp:TextBox ID="txtid" runat="server" Height="1px" Width="1px" Visible="false"></asp:TextBox>
            </div>
        </div>
            <div id="content" runat="server">
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
                            <input id="txtsearch" style="margin-left:10px;width:100%" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                            <button onserverclick="Button1_Click" id="btnFind" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                        </div>
                        <div class="col-sm-3 col-md-3 col-xs-12 form-group pull-right">
                            <telerik:RadComboBox Skin="Bootstrap" runat="server" 
                             RenderMode="Lightweight" 
                            ResolvedRenderMode="Classic" Width="100%" ID="cboCompany" AutoPostBack="True" 
                                Filter="Contains" Font-Names="Verdana" Height="500px" Font-Size="12px" ForeColor="#666666">
                                </telerik:RadComboBox>
                        </div>
                    </div>
                <div class="row">
                    <asp:GridView ID="GridVwHeaderChckbox" runat="server" AllowPaging="True" AllowSorting="True"
                        BorderStyle="Solid" DataKeyNames="id" Font-Names="Verdana"
                        Font-Size="11px" Height="50px" OnRowDataBound="OnRowDataBound" OnSorting="SortRecords"
                        PageSize="500" ShowHeaderWhenEmpty="True" EmptyDataText="No data to display" ToolTip="click row to select record"
                        Width="100%" AutoGenerateColumns="False" GridLines="Vertical" ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC">
                        <RowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="2%">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="CheckAllEmp(this);" />
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkEmp" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Rows" ItemStyle-Width="5%" HeaderText="Rows" SortExpression="rows" />                          
                            <asp:BoundField DataField="EmpID" HeaderText="EMP ID" SortExpression="EMPID" ItemStyle-HorizontalAlign="Center"  />
                                <asp:TemplateField HeaderText="Name" ItemStyle-Font-Bold="true" SortExpression="name">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/Recruitment/EmployeeTerminalBenefit.aspx?empid={0}",
                     HttpUtility.UrlEncode(Eval("EmpID").ToString())) %>'
                                            Text='<%# Eval("name")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="jobtitle" HeaderText="Job Title" SortExpression="jobtitle"   />
                                <asp:BoundField DataField="grade" HeaderText="Grade" SortExpression="grade"   />
                                <asp:BoundField DataField="location" HeaderText="Location" SortExpression="location"  />
                                <asp:BoundField DataField="AccountNumber" HeaderText="Account" SortExpression="AccountNumber"  ItemStyle-HorizontalAlign="Center"/>
                                <asp:BoundField DataField="Bank" HeaderText="Bank" SortExpression="bank"   />                                
                                <asp:BoundField DataField="NetPay" HeaderText="Net Pay" ItemStyle-HorizontalAlign="Right" SortExpression="Net Pay" DataFormatString="{0:n}"/>
                            <%--<asp:TemplateField HeaderText="" ItemStyle-Width="60px">
                                <ItemTemplate>
                                    <a href="#" onclick='openWindow("<%# Eval("Employee No") %>","<%# Eval("Start Date") %>","<%# Eval("End Date") %>");'>
                                        Details</a>
                                </ItemTemplate>
                                <ItemStyle Width="60px" />
                            </asp:TemplateField>--%>
                        </Columns>
                        <HeaderStyle BackColor="White" ForeColor="#1BA691" HorizontalAlign="center" />
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
                     function openWindow(code, code2, code3) {
                            window.open("SalaryMasterUpdate.aspx?empid=" + code + "&startdate=" + code2 + "&enddate=" + code3, "open_window", "width=600,height=700");
                        }
                    </script>
                </div>
            </div>
            <div>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
        </div>
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
