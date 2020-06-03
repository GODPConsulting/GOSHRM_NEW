<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="WorkForce.aspx.vb"
    Inherits="GOSHRM.WorkForce" EnableEventValidation="false" Debug="true" %>

  <%@ register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
   <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
    
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">

  <script type="text/javascript" language="javascript">
      //    Grid View Check box
      function CheckAllEmp(Checkbox) {
          var grdWFPlan = document.getElementById("<%=GridVwHeaderChckbox.ClientID %>");
          for (i = 1; i < grdWFPlan.rows.length; i++) {
              grdWFPlan.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
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
  
   

<body >
    <form id="form1" action="">
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
                    <asp:AsyncPostBackTrigger ControlID="cboyear" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cbostatus" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
                </div>
             <div class="panel-body">
    <div class="row">
    <div class="search-box-wrapper col-sm-6 col-md-4 col-xs-12 form-group pull-right">
                        <asp:LinkButton ID="btDelete" data-toggle="tooltip" data-original-title="Delete" Height="35px" runat="server" CssClass="btn btn-default btn-sm" OnClick="Delete" OnClientClick="Confirm()">
                            <span style="margin-top:5px" aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                        </asp:LinkButton>
                         <button id="btAdd" type="button" data-toggle="tooltip" data-original-title="Add WorkForce Plan" runat="server" class="glyphicon glyphicon-plus btn btn-default btn-sm" onserverclick="btnAdd_Click"
                        style="margin-left:10px;margin-right:10px;height: 35px;"></button> 
                    <input id="search" style="width:100%" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                    <button onserverclick="btnFind_Click" id="btnsearch" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                </div>
        <div class="col-sm-6 col-md-3 col-xs-12 pull-right">
            <telerik:radcombobox runat="server" rendermode="Lightweight"
                resolvedrendermode="Classic" width="100%" id="cboyear" autopostback="True" filter="Contains"
                forecolor="#666666" skin="Bootstrap">
            </telerik:radcombobox>
        </div>
        <div class="col-sm-6 col-md-3 col-xs-12 pull-right">
            <telerik:radcombobox id="cbostatus" runat="server" width="100%" forecolor="#666666"
                autopostback="True" skin="Bootstrap">
                <items>
                                    <telerik:RadComboBoxItem runat="server" Text="Pending" Value="Pending"
                                                            Owner="cboStatus" />
                                    <telerik:RadComboBoxItem runat="server" Text="Cancelled" Value="Cancelled" 
                                        Owner="cboStatus" /><telerik:RadComboBoxItem runat="server" Text="Rejected" 
                                        Value="Rejected" Owner="cboStatus" />
                                    <telerik:RadComboBoxItem runat="server" Text="Approved" Value="Approved" 
                                        Owner="cboStatus" /></items>
            </telerik:radcombobox>
        </div>

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
                                <asp:TemplateField HeaderText="Year" ItemStyle-Font-Bold="true" SortExpression="budgetyear">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/Employee/Recruitment/WorkForcePlanUpdate?id={0}",
                     HttpUtility.UrlEncode(Eval("id").ToString())) %>' Text='<%# Eval("budgetyear")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:BoundField DataField="budget" HeaderText="Budget" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n}" SortExpression="budget" />
                                <asp:BoundField DataField="budgetstat"  HeaderText="Complete Stat" SortExpression ="budgetstat" />
                                 <asp:BoundField DataField="finalstatus"  HeaderText="Approval Stat" SortExpression="finalstatus" />                                   
                                 <asp:BoundField DataField="createdby"  HeaderText="Created By" SortExpression="createdby" />
                                 <asp:BoundField DataField="createdon"  HeaderText="Created On" SortExpression ="createdon"  DataFormatString="{0:dd, MMM yyyy}"/>                                            
                            </Columns>
                            <HeaderStyle BackColor="White" ForeColor="#1BA691" HorizontalAlign="center" />
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cboyear" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="cbostatus" EventName="SelectedIndexChanged" />
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
         
    </form>
</body>
</html>
</asp:Content>

<asp:Content ID="Content2" runat="server" contentplaceholderid="head">
    </asp:Content>