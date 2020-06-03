<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="WorkForceBudgit.aspx.vb"
    Inherits="GOSHRM.WorkForceBudgit" EnableEventValidation="false" Debug="true" %>

  <%@ register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
   <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
    
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">



    <script type="text/javascript" language="javascript">
        //    Grid View Check box
        function CheckAllEmp2(Checkbox) {
            var grdWFBudget = document.getElementById("<%=GridVwHeaderChckbox.ClientID %>");
            for (i = 1; i < grdWFBudget.rows.length; i++) {
                grdWFBudget.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
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
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
         <script type="text/javascript">
             function ShowProgress() {
                 setTimeout(function () {
                     var modal = $('<div />');
                     modal.addClass("modal");
                     $('body').append(modal);
                     var loading = $(".loading");
                     loading.show();
                     var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                     var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                     loading.css({ top: top, left: left });
                 }, 200);
             }
             $('form').live("submit", function () {
                 ShowProgress();
             });
    </script>
     <style type="text/css">
        .modal
        {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }
           .loading
        {
            font-family: Arial;
            font-size: 10pt;
            border: 5px solid #67CFF5;
            width: 200px;
            height: 100px;
            display: none;
            position: fixed;
            background-color: White;
            z-index: 999;
        }
    </style>

   

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
                     <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Always">
                    <ContentTemplate>
                        <h5 id="pagetitle" runat="server" class="page-title" style="color: #1BA691">
                            Head</h5>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cboCompany" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="cboWFBudgetStatus" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
                </div>
             <div class="panel-body">
    <div class="row">
        <div class="col-sm-3 col-md-3 col-xs-6 pull-right ">
            <asp:UpdatePanel runat="server" ID="UpdatePanel4" UpdateMode="Always">
            <ContentTemplate>
            <a href="#"><b>Budget Total: </b><b id="lbcurreny" runat="server"></b><b id="lbbudget"
                runat="server"></b></a>
             </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="cboCompany" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="cboWFBudgetStatus" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
        </div>
        <asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Always">
            <ContentTemplate>
                <div id="divApprove" runat="server" class="col-sm-3 col-md-2 col-xs-6 pull-right ">
                    <asp:Button ID="btnApprove" runat="server" CssClass="btn btn-info" BorderStyle="None"
                        ForeColor="White" Text="Approve" Font-Names="Verdana" Font-Size="14px" OnClientClick="ConfirmApprove()" />
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="cboWFBudgetStatus" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
       <div class="search-box-wrapper col-sm-6 col-md-2 col-xs-12 form-group pull-right">
                    <input id="search" style="width:100%" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                    <button onserverclick="btnFind_Click" id="btnsearch" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                </div>
        <div class="col-sm-3 col-md-2 col-xs-6 pull-right">
            <telerik:radcombobox id="cboWFBudgetStatus" runat="server" width="100%" forecolor="#666666"
                autopostback="True" skin="Bootstrap">
                <items>
                                    <telerik:RadComboBoxItem runat="server" Text="Pending" Value="Pending"
                                                            Owner="cboWFBudgetStatus" />
                                    <telerik:RadComboBoxItem runat="server" Text="Cancelled" Value="Cancelled" 
                                        Owner="cboWFBudgetStatus" /><telerik:RadComboBoxItem runat="server" 
                                        Text="Rejected" Value="Rejected" Owner="cboWFBudgetStatus" />
                                    <telerik:RadComboBoxItem runat="server" Text="Approved" Value="Approved" 
                                        Owner="cboWFBudgetStatus" /></items>
            </telerik:radcombobox>
        </div>
        <div class="col-sm-3 col-md-3 col-xs-6 pull-right">
            <telerik:radcombobox runat="server" rendermode="Lightweight"
                resolvedrendermode="Classic" width="100%" id="cboCompany" autopostback="True"
                filter="Contains" forecolor="#666666" skin="Bootstrap">
            </telerik:radcombobox>
        </div>
    </div>
    <div class="row">
            <div class="table-responsive">
                <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Always">
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
                                <asp:TemplateField HeaderText="Office" ItemStyle-Font-Bold="true" SortExpression="office">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/Employee/Recruitment/WorkForceApproval.aspx?id={0}",
                     HttpUtility.UrlEncode(Eval("id").ToString())) %>' Text='<%# Eval("office")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="budgetyear" HeaderText="Year" SortExpression="budgetyear" />
                                <asp:BoundField DataField="budget" HeaderText="Budget" SortExpression="budget" ItemStyle-HorizontalAlign="right" DataFormatString="{0:n}"  />
                                <asp:BoundField DataField="budgetstat" HeaderText="Completion Status" SortExpression="budgetstat" />
                                <asp:BoundField DataField="myapprovalStat" HeaderText="Approval Status" SortExpression="myapprovalStat"/>
                                <asp:BoundField DataField="createdon" HeaderText="Date Created" SortExpression="createdon"
                                    DataFormatString="{0:dd, MMM yyyy}" />
                               
                            </Columns>
                            <HeaderStyle BackColor="White" ForeColor="#1BA691" HorizontalAlign="center" />
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cboCompany" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="cboWFBudgetStatus" EventName="SelectedIndexChanged" />
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
       
    </form>
     <div class="loading" align="center">
        Processing, please wait...<br />
        <br />
        <img src="~/images/loaders.gif" alt="" />
    </div>
</body>
</html>
</asp:Content>

<asp:Content ID="Content2" runat="server" contentplaceholderid="head">
    </asp:Content>