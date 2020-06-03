<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="MenuSetup.aspx.vb"
    Inherits="GOSHRM.MenuSetup" EnableEventValidation="false" Debug="true" %>

<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%@ register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
    <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
    <html xmlns="http://www.w3.org/1999/xhtml">
  
    <title>Menu Setup</title>
    <body>
        <form id="form1">
         <div class="container col-md-12">
          <div class=" col-md-12">
                        <div id="divalert" runat="server" visible="false" class="alert alert-info">
                            <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <strong
                        id="msgalert" runat="server">Danger!</strong>
                            </div>
         <div class="panel panel-success">
                        <div class="panel-heading">
                            <h5><b id="pagetitle" runat="server">MENU SETUP</b></h5>
                        </div>
                     <div class="panel-body">

       <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>MODULES</label>
                                <telerik:RadComboBox ID="modules" Skin="Bootstrap" runat="server" Width="100%" 
                                    Font-Size="12px" ForeColor="#666666" AutoPostBack="False" Filter="Contains">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                         <div class="col-md-2">
                            <div class="form-group">
                            <label>&nbsp</label>
                                <asp:Button ID="Button1" CssClass="btn btn-success" runat="server" Text="Display" ForeColor="White"
                            Width="100%" Height="35px" BorderStyle="None" Font-Names="Verdana" Font-Size="11px" />
                            </div>
                        </div>
                       
              </div>

                <div class="row">
                    <asp:GridView ID="GridVwHeaderChckbox" runat="server" AllowPaging="True" AllowSorting="True"
                        BorderStyle="Solid" Font-Names="Verdana" CssClass="table table-condensed" AutoGenerateColumns="False"
                        Font-Size="12px" Height="50px" OnRowDataBound="OnRowDataBound" OnSorting="SortRecords"
                        PageSize="50" ShowHeaderWhenEmpty="True" EmptyDataText="No data to display" ToolTip="click row to select record"
                        Width="100%" ForeColor="#666666">
                        <RowStyle BackColor="white" />
                        <Columns>
                            <asp:TemplateField HeaderText="SubModule"  ItemStyle-Font-Bold="true"
                            SortExpression="SubModule">
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/Reports/Finance/MenuSetupUpdate?id={0}",
                     HttpUtility.UrlEncode(Eval("id").ToString())) %>' Text='<%# Eval("SubModule")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="SubModule1"  ItemStyle-Font-Bold="true"
                            SortExpression="SubModule1">
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%# string.Format("~/Module/Reports/Finance/MenuSetupUpdate?id1={0}",
                     HttpUtility.UrlEncode(Eval("id").ToString())) %>' Text='<%# Eval("SubModule1")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="submodule2"  ItemStyle-Font-Bold="true"
                            SortExpression="submodule2">
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl='<%# string.Format("~/Module/Reports/Finance/MenuSetupUpdate?id2={0}",
                     HttpUtility.UrlEncode(Eval("id").ToString())) %>' Text='<%# Eval("submodule2")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        </Columns>
                        <HeaderStyle BackColor="white" ForeColor="#1BA691" HorizontalAlign="center" />
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

            <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
        </div></div></div>
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
