<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="AddRole.aspx.vb"
    Inherits="GOSHRM.AddRole" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Role Privilege</title>  
    <style type="text/css">
        .style1
        {
            color: #FFFFFF;
            font-family: Candara;
            font-weight: bold;
        }
        .lbl
        {
            font-family: Candara;
            color: #000000;
        }
        .style4
        {
        }
        .style5
        {
            width: 611px;
        }
    </style>
    <script type="text/javascript" language="javascript">
        //    Grid View Check box
        function CheckAllRead(Checkbox) {
            var gridrole = document.getElementById("<%=gridrole.ClientID %>");
            for (i = 1; i < gridrole.rows.length; i++) {
                gridrole.rows[i].cells[3].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }
    </script>
    <script type="text/javascript" language="javascript">
        //    Grid View Check box
        function CheckAllCreate(Checkbox) {
            var gridrole = document.getElementById("<%=gridrole.ClientID %>");
            for (i = 1; i < gridrole.rows.length; i++) {
                gridrole.rows[i].cells[4].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }
    </script>
     <script type="text/javascript" language="javascript">
         //    Grid View Check box
         function CheckAllUpdate(Checkbox) {
             var gridrole = document.getElementById("<%=gridrole.ClientID %>");
             for (i = 1; i < gridrole.rows.length; i++) {
                 gridrole.rows[i].cells[5].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
             }
         }
    </script>
    <script type="text/javascript" language="javascript">
        //    Grid View Check box
        function CheckAllDelete(Checkbox) {
            var gridrole = document.getElementById("<%=gridrole.ClientID %>");
            for (i = 1; i < gridrole.rows.length; i++) {
                gridrole.rows[i].cells[6].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }
    </script>
</head>
<body >
    <form id="form1" action="">
        <div class="container col-md-12">
            <div id="divalert" runat="server" visible="false" class="alert alert-info">
                <strong id="msgalert" runat="server">Danger!</strong>
                <asp:TextBox ID="txtid" runat="server" Font-Size="1px" Height="16px" Width="6px"
                    Visible="False"></asp:TextBox>
            </div>
        
    <div class="panel panel-success">
                <div class="panel-heading">
                    <h5><b id="pagetitle" runat="server">User Role</b></h5>
                </div>
             <div class="panel-body">
    <div class="row">
        <div class="col-sm-3 col-xs-6">
            <button id="btnsave" type="button" runat="server" class="btn btn-success" onserverclick="btnAdd_Click"
                style="height: 30px; width: 100px">
                Save</button>
            <button id="Button3" type="button" runat="server" class="btn btn-danger"  onserverclick="btnCancel_Click"
                style="height: 30px; width: 100px">
                << Back</button>
        </div>
    </div>
    <div class="row">
        <div class=" col-md-12">
            <div class="form-group">
                <label>
                    Role*</label>
                <input id="rolename" runat="server" class="form-control" type="text" />
            </div>
        </div>
    </div>
    <div class="row">
        <div class=" col-md-6">
            <div class="form-group">
                <label>Role Type*</label>
                <telerik:RadDropDownList ID="radRoleType" runat="server" AutoPostBack="True" 
                        Width="100%" DefaultMessage="-- Select --" 
                        ForeColor="#666666" RenderMode="Lightweight" Skin="Bootstrap">
                    </telerik:RadDropDownList>
            </div>
        </div>
        <div class=" col-md-6">
            <div class="form-group">
                <label>Module*</label>
                 <telerik:RadDropDownList ID="radMenus" runat="server" AutoPostBack="True" 
                        Width="100%" DefaultMessage="-- Select --" 
                        ToolTip="select a module to set permission" ForeColor="#666666" 
                        RenderMode="Lightweight" Skin="Bootstrap">
                    </telerik:RadDropDownList>
            </div>
        </div>
 
    </div>
    <div class="row">
            <div class="table-responsive">
                <asp:GridView ID="gridrole" runat="server"  AllowSorting="True"
                    BorderStyle="Solid" Font-Names="Verdana" AllowPaging="True" PageSize="200" DataKeyNames="pageid"
                     Width="100%" Height="50px" ToolTip="click row to select record"
                    Font-Size="12px" ShowHeaderWhenEmpty="True" EmptyDataText="No data to display"
                    AutoGenerateColumns="False" GridLines="Both" ForeColor="#666666" BorderWidth="1px"
                    BorderColor="#CCCCCC" CssClass="table table-condensed">
                    <RowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="submodule" HeaderText="Menu" SortExpression="submodule" />
                        <asp:BoundField DataField="pageid" HeaderText="Page ID" SortExpression="pageid" />
                        
                        <asp:BoundField DataField="pages" HeaderText="Pages" SortExpression="pages" />
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkreadSelectAll" runat="server" Text="Read"   onclick="CheckAllRead(this);" />
                            </HeaderTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemTemplate>
                                <asp:CheckBox ID="reads" runat="server" Checked='<%# Eval("reads")%>' ></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>    
                        <asp:TemplateField  ItemStyle-HorizontalAlign="Center">
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkcreateSelectAll" runat="server" Text="Create"   onclick="CheckAllCreate(this);" />
                            </HeaderTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemTemplate>
                                <asp:CheckBox ID="creates" runat="server" Checked='<%# Eval("creates")%>' ></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkupdateSelectAll" runat="server" Text="Update"   onclick="CheckAllUpdate(this);" />
                            </HeaderTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemTemplate>
                                <asp:CheckBox ID="updates" runat="server" Checked='<%# Eval("updates")%>' ></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkdeleteSelectAll" runat="server" Text="Delete"   onclick="CheckAllDelete(this);" />
                            </HeaderTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemTemplate>
                                <asp:CheckBox ID="deletes" runat="server" Checked='<%# Eval("deletes")%>' ></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>                     
                    </Columns>
                    <HeaderStyle BackColor="White" ForeColor="#1BA691" HorizontalAlign="center" />
                </asp:GridView>
                <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
                
            </div>
        </div>
        </div></div>
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
        .style23
        {
            width: 275px;
        }
    </style>
</asp:Content>