﻿<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="StaffRequisitionApprove.aspx.vb"
    Inherits="GOSHRM.StaffRequisitionApprove" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
  
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
    <body>
        <form id="form1">
        <div class="row">
            <div>
                <div id="divalert" runat="server" visible="false" class="alert alert-info">
                    <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <strong
                        id="msgalert" runat="server">Danger!</strong>
                </div>
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
                            <asp:AsyncPostBackTrigger ControlID="cboStatus" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
             <div class="panel-body">
        <%--<div class="row">
            <div class="col-xs-8">
               <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Always">
                    <ContentTemplate>
                        <h5 id="pagetitle" runat="server" class="page-title" style="color: #1BA691">
                            Head</h5>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cboStatus" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>--%>
         
<div class="row">
        <div class="search-box-wrapper col-sm-6 col-md-3 col-xs-12 form-group pull-right">
                    <input id="search" style="width:100%" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                    <button onserverclick="btnFind_Click" id="btsearch" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                </div>
        <div class="col-sm-6 col-md-3 col-xs-12 pull-right">
            <telerik:radcombobox id="cboStatus" runat="server" width="100%" forecolor="#666666"
                skin="Bootstrap" autopostback="true" sort="Ascending">
                <items>
                                    <telerik:RadComboBoxItem runat="server" Text="Pending" Value="Pending"
                                                            Owner="cboStatus" />
                                    <telerik:RadComboBoxItem runat="server" Text="Cancelled" Value="Cancelled" 
                                        Owner="cboStatus" /><telerik:RadComboBoxItem runat="server" Text="Rejected" 
                                        Value="Rejected" Owner="cboStatus" />
                                    <telerik:RadComboBoxItem runat="server" Text="Approved" Value="Approved" 
                                        Owner="cboStatus" />
                             </items>
            </telerik:radcombobox>
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
                                <asp:TemplateField HeaderText="Job Title" ItemStyle-Font-Bold="true" SortExpression="code">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/Recruitment/StaffRequisitionsUpdate?id1={0}",
                     HttpUtility.UrlEncode(Eval("id").ToString())) %>' Text='<%# Eval("Job Title")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Job Type" HeaderText="Job Type" SortExpression="job type" />
                                <asp:BoundField DataField="RecruitMode" HeaderText="Recruit Mode" SortExpression="RecruitMode" />
                                <asp:BoundField DataField="Specialization" HeaderText="Specialization" />
                                <asp:BoundField DataField="Hiring Manager" HeaderText="Hiring Manager" SortExpression="Hiring Manager" />
                                <asp:BoundField DataField="Positions" HeaderText="Slots" SortExpression="Positions"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:d}" />
                                <asp:BoundField DataField="AddedOn" HeaderText="Date Posted" SortExpression="AddedOn"
                                    DataFormatString="{0:dd, MMM yyyy}" />
                            </Columns>
                            <HeaderStyle BackColor="White" ForeColor="#1BA691" HorizontalAlign="center" />
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
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
      </div></div>
        </form>
    </body>
    </html>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    
</asp:Content>
