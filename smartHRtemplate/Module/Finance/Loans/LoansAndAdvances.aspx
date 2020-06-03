<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="LoansAndAdvances.aspx.vb"
    Inherits="GOSHRM.LoansAndAdvances" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="mytelerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="myajax" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
    <title>Staff Loans and Advances</title>
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
        function openWindow(code) {
            window.open("LoanRequest.aspx?id=" + code, "open_window", "width=800,height=700");
        }
                </script>
                <script type="text/javascript">
                    function openSchedule(code) {
                        window.open("LoansSchedule?id=" + code, "open_window", "width=800,height=600");
                    }
                </script>

   
    
    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to delete data, only unapproved will be deleted?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
   </head>
    <body>
        <form id="form1">
        <div class="container col-md-12">
        <div class="row">
            <div>
                <div id="divalert" runat="server" visible="false" class="alert alert-info">
                    <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <strong
                        id="msgalert" runat="server"></strong>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-8">
                <h5 id="pagetitle" runat="server" class="page-title" style="color: #1BA691">
                </h5>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-8">
                <asp:CheckBox ID="chkDisable" runat="server" AutoPostBack="True" Font-Names="Verdana" Text="Exclude me from Loan Guarantor list" Font-Size="13px" Font-Bold="True" 
                                        ToolTip="disable employees from selecting you to guarantor their loan requests" 
                                        ForeColor="#666666" />
            </div>
        </div>
        <div class="row">
                <div class="panel panel-success">
                    <div class="panel-heading">
                        <b>Loans Types</b>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="table-responsive">
                                <asp:GridView ID="gridLoanChart" runat="server" BorderStyle="Solid" Font-Names="Verdana"
                                    PageSize="50" Width="100%" Height="50px"
                                    ToolTip="click row to select record" Font-Size="12px" ShowHeaderWhenEmpty="True"
                                    EmptyDataText="No data to display" AutoGenerateColumns="False" GridLines="Both"
                                    ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC" CssClass="table table-condensed">
                                    <RowStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundField DataField="LoanType" HeaderText="Loan Type"/>                                       
                                      <asp:BoundField DataField="limittype" HeaderText="Limit Type"/>                                  
                                    <asp:BoundField DataField="limit" HeaderText="Limit" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n}"/>
                                    <asp:BoundField DataField="LoanAmount" HeaderText="Loan Amount" ItemStyle-HorizontalAlign="Right"
                                        DataFormatString="{0:n}"/>
                                    <asp:BoundField DataField="ExpectedPay" HeaderText="Expected Payment" ItemStyle-HorizontalAlign="Right"
                                        DataFormatString="{0:n}"/>
                                    <asp:BoundField DataField="Payments" HeaderText="Payments" ItemStyle-HorizontalAlign="Right"
                                        DataFormatString="{0:n}"/>
                                    <asp:BoundField DataField="OutstandingBalance" HeaderText="Outstanding" ItemStyle-HorizontalAlign="Right"
                                        DataFormatString="{0:n}"/>                                    
                                    <asp:TemplateField HeaderText="" ItemStyle-Font-Bold="true">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/Finance/Loans/LoanRequest?loantype={0}",
                     HttpUtility.UrlEncode(Eval("loantype").ToString())) %>' Text='<%# Eval("applyloan")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                    </Columns>
                                    <HeaderStyle BackColor="White" ForeColor="#1BA691" HorizontalAlign="center" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
        </div>
        <div class="row">
         <div class="panel panel-success">
                <div class="panel-heading">
                    <h5><b id="divdetailheader" runat="server"></b></h5>
                </div>
                <div class="panel-body">
        <div class="row">
        <div class="search-box-wrapper col-sm-6 col-md-3 col-xs-12 form-group pull-right">
                        <asp:LinkButton ID="btDelete" data-toggle="tooltip" data-original-title="Delete" Height="35px" runat="server" CssClass="btn btn-default btn-sm" OnClick="Delete" OnClientClick="Confirm()">
                            <span style="margin-top:5px" aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                        </asp:LinkButton>
                         <div style="width:10px; height:10px;"></div> 
                    <input id="search" style="width:100%" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                    <button onserverclick="btnFind_Click" id="btsearch" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                </div>
            <div class="col-sm-6 col-md-3 col-xs-12 pull-right">
                <input id="adatefrom" style="height:35px" runat="server" class="form-control datetimepicker" type="text"
                    placeholder="From" />
            </div>
            <div class="col-sm-6 col-md-3 col-xs-12 pull-right">
                <input id="adateto" style="height:35px" runat="server" class="form-control datetimepicker" type="text"
                    placeholder="To" />
            </div>
            <div class="col-sm-6 col-md-3 col-xs-12 pull-right">
                <mytelerik:radcombobox id="radStatus" runat="server" resolvedrendermode="Classic"
                    forecolor="#666666" rendermode="Lightweight" skin="Bootstrap" width="100%" AutoPostBack="True"
                    EmptyMessage="-- Select Status --">
                </mytelerik:radcombobox>
            </div>           
        </div>

         <div class="row">
            <div class="table-responsive">
                <asp:UpdatePanel runat="server" ID="UpdatePanel5" UpdateMode="Always">
               <ContentTemplate>
                        <asp:GridView ID="GridVwHeaderChckbox" runat="server" OnSorting="SortRecords" AllowSorting="True"
                            BorderStyle="Solid" Font-Names="Verdana" AllowPaging="True" PageSize="100" DataKeyNames="id"
                             Width="100%" Height="50px" ToolTip="click row to select record"
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
                                        <asp:CheckBox ID="chkEmp" runat="server" AutoPostBack="false"></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Rows" ItemStyle-Width="5%" HeaderText="Row" SortExpression="rows" />
                                <asp:TemplateField HeaderText="Loan Ref No"  ItemStyle-Font-Bold="true"
                            SortExpression="Loan RefNo"><ItemTemplate><asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/Finance/Loans/LoanRequest?id={0}",
                     HttpUtility.UrlEncode(Eval("id").ToString())) %>' Text='<%# Eval("Loan RefNo")%>' /></ItemTemplate></asp:TemplateField>
                                 <asp:BoundField DataField="Loan" HeaderText="Loan Type" SortExpression="loan"/>                           
                                <asp:BoundField DataField="Loan Date" HeaderText="Date" SortExpression="Loan Date" DataFormatString="{0:dd, MMM yyyy}" />
                                <asp:BoundField DataField="Repayment Start Date" HeaderText="Start Date" SortExpression="Repayment Start Date" DataFormatString="{0:dd, MMM yyyy}" />
                                <asp:BoundField DataField="amount" HeaderText="Amount" DataFormatString="{0:n}" SortExpression="amount" ItemStyle-HorizontalAlign ="Right"/>                           
                                <asp:BoundField DataField="Monthly Payment" HeaderText="Monthly Payment" DataFormatString="{0:n}" ItemStyle-HorizontalAlign ="Right" SortExpression="Monthly Payment"/>                         
                                <asp:BoundField DataField="ExpectedPay" HeaderText="Expected Payment" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n}" SortExpression="ExpectedPay"/>
                                <asp:BoundField DataField="Payments" HeaderText="Payments" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n}" SortExpression="Payments"/>                          
                                <asp:BoundField DataField="Balance" HeaderText="Balance" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n}" SortExpression="Balance"/>
                       
                                <asp:BoundField DataField="Status" HeaderText="Loan Status" SortExpression="Status"/>
                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <a href="#" onclick='openSchedule("<%# Eval("id") %>");'>Schedule</a></ItemTemplate>
                            <ItemStyle Font-Bold="True"/>
                        </asp:TemplateField>
                            </Columns>
                            <HeaderStyle BackColor="White" ForeColor="#1BA691" HorizontalAlign="center" />
                        </asp:GridView>
                   </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="radStatus" EventName="SelectedIndexChanged" />
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
        </div></div></div>
      


        



         
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
