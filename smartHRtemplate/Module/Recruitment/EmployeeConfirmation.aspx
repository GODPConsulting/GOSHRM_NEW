<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="EmployeeConfirmation.aspx.vb"
    Inherits="GOSHRM.EmployeeConfirmation" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml"/>
    <head >
        <title></title>
         
    </head>
    
    
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
    
    <body>
        
        <form id="form1" action="">
        <div class="container col-md-12">
        <div class="row">
        <div id="divalert" runat="server" visible="false" class="alert alert-info">
            <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <strong
                id="msgalert" runat="server">Danger!</strong>
        </div>
        </div>
        <div class="panel panel-success">
                <div class="panel-heading">
                     <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Always">
                    <ContentTemplate>
                        <h5 id="pagetitle" runat="server" class="page-title" style="color: #1BA691">
                            Head</h5>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cboCompany" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="cboStatus" EventName="SelectedIndexChanged" />
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
                    <input id="search" style="width:100%" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                    <button onserverclick="btnFind_Click" id="btsearch" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                </div>           
            <div class="col-sm-3 col-md-3 col-xs-12 pull-right">
                <telerik:RadComboBox ID="cboStatus" runat="server" Width="100%" ForeColor="#666666" AutoPostBack="True"
                    Skin="Bootstrap">
                     <Items>
                            <telerik:RadComboBoxItem runat="server" Text="Pending" Value="Pending" />
                               <telerik:RadComboBoxItem runat="server" Text="Due" Value="Due" />
                                <telerik:RadComboBoxItem runat="server" Text="Not Due" Value="Not Due" />
                                <telerik:RadComboBoxItem runat="server" Text="Confirmed" Value="Confirmed" />
                                <telerik:RadComboBoxItem runat="server" Text="Terminate Employment" Value="Terminate Employment" />
                                <telerik:RadComboBoxItem runat="server" Text="Extend Probation" Value="Extend Probation" />                                
                            </Items>
                </telerik:RadComboBox>
            </div> 
             <div class="col-sm-3 col-md-3 col-xs-12 pull-right">
                <telerik:RadComboBox runat="server" RenderMode="Lightweight"
                    ResolvedRenderMode="Classic" Width="100%" ID="cboCompany" AutoPostBack="True"
                    Filter="Contains" ForeColor="#666666" Skin="Bootstrap">
                </telerik:RadComboBox>
            </div>           
            <%--<div class="col-sm-3 col-md-3 col-xs-6">
                <div class="form-group form-focus">
                    <input id="search" runat="server" type="text" class="form-control floating" style="height: 30px"
                        placeholder="Search..." />
                    <button id="btnsearch" type="button" runat="server" class="glyphicon glyphicon-search"
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
            </div>--%>
        </div>
        
       <div class="row">
            <div class="table-responsive">
                <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Always">
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
                                        <asp:CheckBox ID="chkEmp" runat="server" AutoPostBack="false"></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Rows" ItemStyle-Width="5%" HeaderText="Row" SortExpression="rows" />
                                <asp:TemplateField HeaderText="Employee" ItemStyle-Font-Bold="true" SortExpression="recruitname">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/Recruitment/EmpConfirmationUpdate?id={0}&empid={1}",
                     HttpUtility.UrlEncode(Eval("id").ToString()),HttpUtility.UrlEncode(Eval("recruitid").ToString())) %>' Text='<%# Eval("recruitname")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:BoundField DataField="office" HeaderText="Office" SortExpression="office" />
                                <asp:BoundField DataField="datejoin" HeaderText="Date Joined"  SortExpression="datejoin" DataFormatString="{0:dd, MMM yyyy}"  />
                                <asp:BoundField DataField="ExpectedConfirmDate" HeaderText="Expected Confirmation" SortExpression="ExpectedConfirmDate" DataFormatString="{0:dd, MMM yyyy}"   />
                                <asp:BoundField DataField="probation" HeaderText="Probation (Mth)" SortExpression="probation" ItemStyle-HorizontalAlign="Right"/>                                
                                <asp:BoundField DataField="completestat" HeaderText="Completion Stat" SortExpression="completestat"  />                                           
                            </Columns>
                            <HeaderStyle BackColor="White" ForeColor="#1BA691" HorizontalAlign="center" />
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cboCompany" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="cboStatus" EventName="SelectedIndexChanged" />
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
        </div>
        </div> 
        </div>
        </form>
    </body>
    </html>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    
</asp:Content>
