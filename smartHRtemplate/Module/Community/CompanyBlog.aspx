<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="CompanyBlog.aspx.vb"
    Inherits="GOSHRM.CompanyBlog" EnableEventValidation="false" Debug="true" %>

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
        <div class="container col-md-12">
        <div class="row">
            <div id="divalert" runat="server" visible="false" class="alert alert-info">
                <strong id="msgalert" runat="server">Danger!</strong>
            </div>
        </div>
         <div class="panel panel-success">
                        <div class="panel-heading">
                            <h5><b id="pagetitle" runat="server">Blogs</b></h5>
                        </div>
                     <div class="panel-body">
        <div class="row">
        <div class="search-box-wrapper col-sm-6 col-md-3 col-xs-12 form-group pull-right">                        
                    <input id="txtsearch" style="width:100%" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                    <button onserverclick="btnGo_Click" id="btnGo" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                </div>
                 <div class="col-md-3 col-sm-6 col-xs-12 form-group pull-right">                                  
                        <asp:LinkButton ID="btnAdd0" data-toggle="tooltip" data-original-title="Delete Post" Height="35px" runat="server" CssClass="btn btn-default btn-sm" OnClick="btnAdd0_Click" OnClientClick="Confirm()">
                            <span style="margin-top:5px" aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                        </asp:LinkButton>
                         <button id="btnAdd" type="button" data-toggle="tooltip" data-original-title="Create Post" runat="server" class="glyphicon glyphicon-plus btn btn-default btn-sm" onserverclick="btnAdd_Click"
                        style="height: 35px;margin-right:10px;margin-left:10px;"></button> 
                        <asp:LinkButton ID="lnkBlogType" data-toggle="tooltip" data-original-title="Manage Blog Types" Height="35px" runat="server" CssClass="btn btn-default btn-sm" Font-Bold="True" 
                            Font-Names="Verdana" Font-Size="12px" ToolTip="manage blog types list"><span style="margin-top:5px" aria-hidden="true" class="glyphicon glyphicon-cog"></span>
                        </asp:LinkButton>
                </div>
                <div class="col-md-3 col-sm-6 col-xs-12 pull-right">
                    <telerik:RadComboBox runat="server" Skin="Bootstrap" 
                     RenderMode="Lightweight" ResolvedRenderMode="Classic" Width="100%" ID="cboCompany" AutoPostBack="True" 
                        Filter="Contains" Font-Names="Verdana" Font-Size="14px" ForeColor="#666666">
                        </telerik:RadComboBox>
                </div>
                <div class="col-md-3 col-sm-6 col-xs-12 pull-right">
                    <telerik:RadComboBox runat="server" Skin="Bootstrap" RenderMode="Lightweight"
                            ResolvedRenderMode="Classic" Width="100%" ID="cboBlogType" Filter="Contains"
                            Font-Names="Verdana" Font-Size="14px" ForeColor="#666666">
                        </telerik:RadComboBox>
                </div>
        </div>
         
        <div style="height: 500px; width: 100%; overflow-y: scroll; overflow-x: hidden;">
            <asp:DataList ID="dlBlogs" runat="server" Width="100%" RepeatColumns="1" CellSpacing="2"
                RepeatLayout="Table" Font-Names="Arial" Font-Size="13px" GridLines="Both" DataKeyField="id"
                BorderColor="#CCCCCC" ForeColor="#666666" BorderStyle="Solid" 
                BorderWidth="1px" >
                <ItemTemplate>
                    <table class="table" width="100%" >
                        <tr>
                            
                            <td colspan="2">
                                <asp:CheckBox ID="chkEmp" runat="server"></asp:CheckBox>  
                                <b>
                                    <%# Eval("heading")%></b>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" style="width: 90%">                                                          
                                <asp:TextBox ID="Label2" Text='<%# Eval("message").ToString() %>' runat="server" ForeColor="#666666"
                                    ReadOnly="true" Width="95%" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CssClass="notes"   /><br />
                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/Community/CompanyBlogView.aspx?id={0}",
                     HttpUtility.UrlEncode(Eval("id").ToString())) %>' Text='Read More...' ToolTip="click to view full blog detail"  />                                  
                            </td>
                            <td valign="top" style="width: 10%; font-size:12px">
                                <%# Eval("postedby")%>
                                <br />
                                <%# Eval("createdon")%><br />
                                <br />
                                <%# Eval("approval")%>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:DataList>
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
