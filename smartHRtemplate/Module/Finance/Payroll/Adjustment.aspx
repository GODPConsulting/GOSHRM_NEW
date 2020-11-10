<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="Adjustment.aspx.vb"
    Inherits="GOSHRM.Adjustment" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <script type="text/javascript" language="javascript">
        //    Grid View Check box
        function CheckAllEmp(Checkbox) {
            var GridView1 = document.getElementById("<%=GridView1.ClientID %>");
            for (i = 1; i < GridView1.rows.length; i++) {
                GridView1.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }
    </script>
    <title></title>
        <script type="text/javascript">
            function ConfirmApprove() {
                var confirm_app = document.createElement("INPUT");
                confirm_app.type = "hidden";
                confirm_app.name = "confirm_app";
                if (confirm("Approve the selected items?")) {
                    confirm_app.value = "Yes";
                    ShowProgress();
                } else {
                    confirm_app.value = "No";
                }
                document.forms[0].appendChild(confirm_app);
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
            font-size: large;
            font-weight: bold;
            color: #FFFFFF;
        }
        .style30
        {
            width: 6%;
            
        }
    </style>
    <body>
        <form id="form1" >
        <div class="container col-md-12">
      <div class="row">
             <div id="divalert" runat="server" visible="false" class="alert alert-info">
                <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                <strong id="msgalert" runat="server">Danger!</strong>
            </div>
        </div>
            <div id="content" runat="server">
        <div class="panel panel-success">
                <div class="panel-heading">
                    <h5><b id="pagetitle" runat="server">Payroll Adjustment</b></h5>
                </div>
             <div class="panel-body">
             <div class="row">
                   <div class="col-md-3 col-sm-6 col-xs-12 pull-right">
                        <telerik:RadComboBox Skin="Bootstrap" ID="radYear" Runat="server" 
                                    ResolvedRenderMode="Classic" Width="100%" Font-Names="Verdana" 
                                    Font-Size="12px" AutoPostBack="True">
                                    <Items>
                                        <telerik:RadComboBoxItem runat="server" Text="January" Value="1" 
                                            Owner="radYear" />
                                        <telerik:RadComboBoxItem runat="server" Text="February" Value="2" 
                                            Owner="radYear" />
                                        <telerik:RadComboBoxItem runat="server" Text="March" Value="3" 
                                            Owner="radYear" />
                                        <telerik:RadComboBoxItem runat="server" Text="April" Value="4" 
                                            Owner="radYear" />
                                        <telerik:RadComboBoxItem runat="server" Text="May" Value="5" Owner="radYear" />
                                        <telerik:RadComboBoxItem runat="server" Text="June" Value="6" Owner="radYear" />
                                        <telerik:RadComboBoxItem runat="server" Text="July" Value="7" Owner="radYear" />
                                        <telerik:RadComboBoxItem runat="server" Text="August" Value="8" 
                                            Owner="radYear" />
                                        <telerik:RadComboBoxItem runat="server" Text="September" Value="9" 
                                            Owner="radYear" />
                                        <telerik:RadComboBoxItem runat="server" Text="October" Value="10" 
                                            Owner="radYear" />
                                        <telerik:RadComboBoxItem runat="server" Text="November" Value="11" 
                                            Owner="radYear" />
                                        <telerik:RadComboBoxItem runat="server" Text="December" Value="12" 
                                            Owner="radYear" />
                                    </Items>
                                </telerik:RadComboBox>
                    </div>    
                   <div class="col-md-3 col-sm-6 col-xs-12 pull-right">
                     <telerik:RadComboBox Skin="Bootstrap" ID="radMonth" Runat="server" 
                                    ResolvedRenderMode="Classic" Width="100%" Font-Names="Verdana" 
                                    Font-Size="12px" AutoPostBack="True">
                                    <Items>
                                        <telerik:RadComboBoxItem runat="server" Text="January" Value="1" 
                                            Owner="radMonth" />
                                        <telerik:RadComboBoxItem runat="server" Text="February" Value="2" 
                                            Owner="radMonth" />
                                        <telerik:RadComboBoxItem runat="server" Text="March" Value="3" 
                                            Owner="radMonth" />
                                        <telerik:RadComboBoxItem runat="server" Text="April" Value="4" 
                                            Owner="radMonth" />
                                        <telerik:RadComboBoxItem runat="server" Text="May" Value="5" Owner="radMonth" />
                                        <telerik:RadComboBoxItem runat="server" Text="June" Value="6" 
                                            Owner="radMonth" />
                                        <telerik:RadComboBoxItem runat="server" Text="July" Value="7" 
                                            Owner="radMonth" />
                                        <telerik:RadComboBoxItem runat="server" Text="August" Value="8" 
                                            Owner="radMonth" />
                                        <telerik:RadComboBoxItem runat="server" Text="September" Value="9" 
                                            Owner="radMonth" />
                                        <telerik:RadComboBoxItem runat="server" Text="October" Value="10" 
                                            Owner="radMonth" />
                                        <telerik:RadComboBoxItem runat="server" Text="November" Value="11" 
                                            Owner="radMonth" />
                                        <telerik:RadComboBoxItem runat="server" Text="December" Value="12" 
                                            Owner="radMonth" />
                                    </Items>
                                </telerik:RadComboBox>
                   </div>
                   <div class="col-md-3 col-sm-6 col-xs-12 pull-right">
                    <telerik:RadComboBox runat="server" Skin="Bootstrap" RenderMode="Lightweight"
                                        ResolvedRenderMode="Classic" Width="100%" ID="cboCompany" Filter="Contains"
                                        AutoPostBack="True" Font-Names="Verdana" Font-Size="12px" 
                                        ForeColor="#666666">
                                    </telerik:RadComboBox>
                   </div>
             </div>
            <div class="row">
                    <div class="search-box-wrapper col-sm-6 col-md-3 col-xs-12 form-group pull-right">                        
                        <input id="txtsearch" style="width:100%" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                        <button onserverclick="Button1_Click" id="btnFind" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                    </div>
                     <div class="col-md-3 col-sm-6 col-xs-12 form-group pull-right">                                  
                        <button id="btnUpload" type="button" runat="server" class="fa fa-upload btn btn-default btn-sm"
                            onserverclick="btnUpload_Click" data-toggle="tooltip" data-original-title="CSV File: EMPID, AdjustmentTitle, Type: Earning/Deduction,Amount, Pay Date,Description, IsTaxable: Yes/No" style="margin-right:10px;margin-left:10px;height:35px"></button>
                            <button id="btnExport" data-toggle="tooltip" data-original-title="Export" type="button" runat="server" class="fa fa-download btn btn-default btn-sm" onserverclick="Button1_Click1"
                        style="margin-right:10px;height: 35px"></button>
                            <asp:LinkButton ID="btnDelete" data-toggle="tooltip" data-original-title="Delete" Height="35px" runat="server" CssClass="btn btn-default btn-sm" OnClick="Delete" OnClientClick="Confirm()">
                                <span style="margin-top:5px" aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                            </asp:LinkButton>
                             <button id="btAdd" type="button" data-toggle="tooltip" data-original-title="Add" runat="server" class="glyphicon glyphicon-plus btn btn-default btn-sm" onserverclick="btnAdd_Click"
                            style="height: 35px;margin-left:10px;"></button> 
                    </div>
                    <div class="col-md-3 col-sm-6 col-xs-12 pull-right">
                        <input style="height:35px;" class="form-control" type="file" id="FileUpload1" runat="server" />
                    </div>
                     <div class="col-md-2 col-sm-6 col-xs-12 pull-right">
                    <button data-toggle="tooltip" data-original-title="Approve for Everyone" type="button" runat="server" OnClientClick="ConfirmApprove()" class="btn btn-success btn-lg" onserverclick="Button1_Click2"
                        style="width:100%; font-size:12px; height:35px">Approve</button>
                        </div>                   
                </div>
     
            
             
                
                <div class="row">
                    <div>
                        <asp:GridView ID="GridView1" runat="server" OnSorting="SortRecords" AllowSorting="True" CssClass="table table-condensed"
                            BorderStyle="Solid" Font-Names="Verdana" AllowPaging="True" PageSize="500" DataKeyNames="id"
                            OnRowDataBound="OnRowDataBound" Width="100%" Height="50px" ToolTip="click row to select record"
                            Font-Size="11px" ShowHeaderWhenEmpty="True" EmptyDataText="No data to display" AutoGenerateColumns="False" GridLines="Vertical" ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC" >
                            <RowStyle BackColor="white" />
                            <Columns>
                                <asp:TemplateField ItemStyle-Width="1px">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="CheckAllEmp(this);" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkEmp" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkEmp" runat="server"></asp:CheckBox></ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Rows" ItemStyle-Width="5px" HeaderText="Rows" SortExpression="rows" />
                                
                                <asp:TemplateField HeaderText="Reference" ItemStyle-Width="20px" ItemStyle-Font-Bold="true" SortExpression="transref">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/Finance/Payroll/AdjustmentUpdate.aspx?id={0}",
                     HttpUtility.UrlEncode(Eval("id").ToString())) %>'
                                            Text='<%# Eval("transref")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="EmpID" ItemStyle-Width="30px" HeaderText="EMP ID" SortExpression="EMPID" />                              
                                <asp:BoundField DataField="name" HeaderText="Name" SortExpression="name" />
                                <asp:BoundField DataField="title" HeaderText="Description" SortExpression="title"/>
                                <asp:BoundField DataField="adjtype" HeaderText="Adjustment Type" SortExpression="adjtype"/>
                                 <asp:BoundField DataField="amount" HeaderText="Amount" ItemStyle-HorizontalAlign="Right" SortExpression="amount" DataFormatString="{0:n}"/>
                                <asp:BoundField DataField="paydate" HeaderText="Pay Date" SortExpression="paydate"/>
                                <asp:BoundField DataField="approvalstatus" HeaderText="Approval" SortExpression="approvalstatus"/>
                                <asp:BoundField DataField="taxable" HeaderText="Taxable" SortExpression="taxable" ItemStyle-HorizontalAlign="Center"  />
                             
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
                                window.open("AdjustmentUpdate.aspx?id=" + code, "open_window", "width=400,height=400");
                            }
                        </script>
                    </div>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:GOSHRMConnectionString %>"
                        SelectCommand="Job_Grade_TreeView" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                </div>  
                </div></div></div></div>  
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
