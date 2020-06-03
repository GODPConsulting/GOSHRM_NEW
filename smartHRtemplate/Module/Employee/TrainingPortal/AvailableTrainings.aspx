<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="AvailableTrainings.aspx.vb"
    Inherits="GOSHRM.AvailableTrainings" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title></title>
    </head>
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
        <div class= "container col-md-12">
            <div id="divalert" runat="server" visible="false" class="alert alert-info">
                <strong id="msgalert" runat="server">Danger!</strong>
            </div>        
       <div class="panel panel-success">
                <div class="panel-heading">
                    <h5><b id="pagetitle" runat="server"></b></h5>
                </div>
             <div class="panel-body">
        
         <div class="row filter-row col-md-12">
            <div class="search-box-wrapper col-sm-6 col-md-3 col-xs-12 form-group pull-right">
                    <input id="search" style="width:100%" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                    <button onserverclick="btnFind_Click" id="btsearch" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                </div>             
        </div>
            <asp:GridView ID="GridVwHeaderChckbox" runat="server" AllowSorting="True" BorderStyle="Solid"
                Font-Names="Verdana" AllowPaging="True" PageSize="300" DataKeyNames="id" Width="100%" 
                Height="50px" ToolTip="click row to select record" Font-Size="12px" ShowHeaderWhenEmpty="True" EmptyDataText="No data to display"
                AutoGenerateColumns="False" GridLines="Vertical" ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC" CssClass="table">
                <AlternatingRowStyle BackColor="white" />
                <Columns>
                    <asp:BoundField DataField="Rows" HeaderText="Rows">
                        <ItemStyle Width="5px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="company" HeaderText="Company" SortExpression="company">
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Name" ItemStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkopen" runat="server" Text = '<%# Eval("name")%>' CommandName = "AddToCart" CausesValidation="false"  CommandArgument = '<%# Eval("id") %>' ></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:BoundField DataField="Course" HeaderText="Course" SortExpression="course">
                    </asp:BoundField>
                    <asp:BoundField DataField="Scheduled Time" HeaderText="Scheduled Date" SortExpression="Scheduled Time">
                    </asp:BoundField>
                    <asp:BoundField DataField="availabletrainness" HeaderText="Total Slots" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:d}">
                    </asp:BoundField>
                       <asp:BoundField DataField="availableslots" HeaderText="Remaining Slots" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:d}">
                    </asp:BoundField>
                    <asp:BoundField DataField="ForwardApproval" HeaderText="Request Status" SortExpression="ForwardApproval"/>
                </Columns>
                <HeaderStyle BackColor="White" ForeColor="#1BA691" HorizontalAlign="Center" />
            </asp:GridView>
            <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
            <script type="text/javascript">
                $(function () {
                    $("[id*=gridTrainings] td").hover(function () {
                        $("td", $(this).closest("tr")).addClass("hover_row");
                    }, function () {
                        $("td", $(this).closest("tr")).removeClass("hover_row");
                    })
                })
            </script>
            </div>
        </div>
        </div>
        </form>
    </body>
    </html>
</asp:Content>
