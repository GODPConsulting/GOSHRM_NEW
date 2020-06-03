<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup ="true" CodeBehind="LoanRules.aspx.vb" Inherits="GOSHRM.LoanRules" EnableEventValidation="false" Debug="true"%>
<asp:Content ID = "content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


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

    

    

    <title>Roles</title>

    <script type = "text/javascript">
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

 
<body>
   


    <form id="form1" >
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
                    <h5><b id="pagetitle" runat="server"></b></h5>
                </div>
             <div class="panel-body">
    <%--<div class="row">
        <div class="col-sm-3 col-md-3 col-xs-6">
            <div class="form-group form-focus">
                <input id="search" runat="server" type="text" class="form-control floating" style="height: 30px"
                    placeholder="Search..." />
                <button id="btFind" type="button" runat="server" class="glyphicon glyphicon-search"
                    onserverclick="btnFind_Click" style="height: 29px; width: 40px">
                </button>
            </div>
        </div>

        <div class="col-sm-3 col-md-1 col-xs-6">
            <button id="btAdd" type="button" runat="server" class="btn-success" onserverclick="btnAdd_Click"
                style="height: 30px; width: 100px">
                Add
            </button>
        </div>

        <div class="col-sm-3 col-md-1 col-xs-6">
            <asp:Button ID="btDelete" runat="server" Text="Delete" OnClick="Delete" OnClientClick="Confirm()"
                BackColor="#FF3300" ForeColor="White" Width="100px" Height="30px" CssClass="btn-danger"
                BorderStyle="None" Font-Names="Verdana" Font-Size="12px" />
        </div>

           <div class="col-sm-3 col-md-1 col-xs-6">
            <button id="btExport" type="button" runat="server" class="btn-warning" onserverclick="btnExport_Click"
                style="height: 30px; width: 100px">
                Export</button>
        </div>

        <div class="col-sm-3 col-md-2 col-xs-6">
            <input class="form-control" type="file" id="file1" runat="server" />
        </div>

        <div class="col-sm-3 col-md-1 col-xs-6">
            <button id="btnUploadFile" type="button" runat="server" class=" btn-info btn-block"
                onserverclick="btnUpload_Click"  title="CSV File: LoanType, JobGrade,EmploymentStatus,InterestRate,MarketRate, Loan Amount Limit,Min. Months In Service,Confirmed Staff Only {Yes/No}, Repay Factor, Repayment Factor(%), Use Gratuity Rule {Yes/No}, Amount Limit Type {Percentage/Fixed Amount}"
                style="height: 30px; width: 100px">
                Upload</button>
        </div>
        
    </div>--%>
     <div class="row">
            <div class="search-box-wrapper col-sm-6 col-md-3 col-xs-12 form-group pull-right">                        
                    <input id="search" style="width:100%" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                    <button onserverclick="btnFind_Click" id="btsearch" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                </div>
                 <div class="col-md-3 col-sm-6 col-xs-12 form-group pull-right">                                  
                    <button id="btnUploadFile" type="button" runat="server" class="fa fa-upload btn btn-default btn-sm"
                        onserverclick="btnUpload_Click" data-toggle="tooltip" data-original-title="CSV File: LoanType, JobGrade,EmploymentStatus,InterestRate,MarketRate, Loan Amount Limit,Min. Months In Service,Confirmed Staff Only {Yes/No}, Repay Factor, Repayment Factor(%), Use Gratuity Rule {Yes/No}, Amount Limit Type {Percentage/Fixed Amount}" style="margin-right:10px;margin-left:10px;height:35px"></button>
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
        </div>
    <div class="row">
            <div class="table-responsive">
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
                                <asp:CheckBox ID="chkEmp" runat="server" AutoPostBack="false"></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Rows" ItemStyle-Width="5%" HeaderText="Row" SortExpression="rows" />
                        <asp:TemplateField HeaderText="Rule" ItemStyle-Font-Bold="true" SortExpression="rulename">
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/Finance/Settings/LoanRulesUpdate?id={0}",
                     HttpUtility.UrlEncode(Eval("id").ToString())) %>' Text='<%# Eval("rulename")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>   
                        <asp:BoundField DataField="Loan Type" HeaderText="Loan Type" SortExpression="Loan Type"/> 
                        <asp:BoundField DataField="Employment Status" HeaderText="Employment Status" SortExpression="Employment Status"/> 
                        <asp:BoundField DataField="Interest Rate(%)" HeaderText="Int. Rate(%)" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n}" SortExpression ="Interest Rate(%)"/>         
                         <asp:BoundField DataField="Market Rate" HeaderText="Market Rate(%)" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n}" SortExpression ="Market Rate"/> 
                         <asp:BoundField DataField="Maximum Amount" HeaderText="Amount Limit" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n}" SortExpression="maximum amount" /> 
                         <asp:BoundField DataField="servicerange" HeaderText="Months In Service" SortExpression="servicerange" ItemStyle-HorizontalAlign="Right"/>                   
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









    
    
   
    
    </form>
</body>
</html>
</asp:Content>

<asp:Content ID="Content2" runat="server" contentplaceholderid="head">
 
</asp:Content>


