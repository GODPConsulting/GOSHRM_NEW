<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="BlogType.aspx.vb"
    Inherits="GOSHRM.BlogType" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            function closeWin() {
                popup.close();   // Closes the new window
            }
   

    </script>

    <script type="text/javascript" language="javascript">
        //    Grid View Check box
        function CheckAllEmp(Checkbox) {
            var GridVwHeaderChckbox = document.getElementById("<%=GridVwHeaderChckbox.ClientID %>");
            for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }
    </script>
    </telerik:RadCodeBlock>
</head>
<body>
    <form>
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
        rel="Stylesheet" type="text/css" />
    <div class="container col-md-12">
           <div class="row">
             <div id="divalert" runat="server" visible="false" class="alert alert-info">
                <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                <strong id="msgalert" runat="server">Danger!</strong>
            </div>
         </div>
       <div class="panel panel-success">
                <div class="panel-heading">
                    <h5><b id="pagetitle" runat="server"></b></h5>
                </div>
             <div class="panel-body">       
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="List" runat="server">
        <div class="row">
          <div class="search-box-wrapper col-sm-6 col-md-3 col-xs-12 form-group pull-right">                        
                    <input id="txtsearch" style="width:100%" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                    <button onserverclick="btnFind_Click" id="btsearch" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                </div>
                 <div class="col-md-3 col-sm-6 col-xs-12 form-group pull-right">                                  
                    <button id="btnUploadFile" type="button" runat="server" class="fa fa-upload btn btn-default btn-sm"
                        onserverclick="btnUpload_Click" data-toggle="tooltip" data-original-title="CSV File: Name, Comment Viewable {Yes/No}"  style="margin-right:10px;margin-left:10px;height:35px"></button>
                        <button id="btExport" data-toggle="tooltip" data-original-title="Export" type="button" runat="server" class="fa fa-download btn btn-default btn-sm" onserverclick="btnExport_Click"
                    style="margin-right:10px;height: 35px"></button>
                        <asp:LinkButton ID="btnDelete" data-toggle="tooltip" data-original-title="Delete" Height="35px" runat="server" CssClass="btn btn-default btn-sm" OnClick="Delete" OnClientClick="Confirm()">
                            <span style="margin-top:5px" aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                        </asp:LinkButton>
                         <button id="btnAdd" type="button" data-toggle="tooltip" data-original-title="Add" runat="server" class="glyphicon glyphicon-plus btn btn-default btn-sm" onserverclick="btnAdd_Click"
                        style="height: 35px;margin-right:10px;margin-left:10px;"></button> 
                </div>
                <div class="col-md-3 col-sm-6 col-xs-12 pull-right">
                    <input style="height:35px;" class="form-control" type="file" id="FileUpload1" runat="server" />
                </div>     
        </div>
        <div class="row">
             <asp:GridView ID="GridVwHeaderChckbox" runat="server" AllowSorting="True" OnSorting="SortRecords"
                    BorderStyle="Solid" Font-Names="Verdana" AllowPaging="True" PageSize="50" DataKeyNames="id"
                    Width="100%" Height="50px" ToolTip="click row to select record" CssClass="table table-condensed" 
                    Font-Size="12px" AutoGenerateColumns="False" GridLines="Vertical" ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC" ShowHeaderWhenEmpty="True" EmptyDataText="No data to display">
                    <RowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField ItemStyle-Width="1px">
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="CheckAllEmp(this);" />
                        </HeaderTemplate>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemTemplate>
                            <asp:CheckBox ID="chkEmp" runat="server"></asp:CheckBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                        <asp:BoundField DataField="Rows" ItemStyle-Width="2px" HeaderText="Rows">
                            <ItemStyle Width="2px"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Name" ItemStyle-Width="100px" 
                        ItemStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Left" SortExpression="name">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkName" Text = '<%# Eval("name")%>' CommandArgument = '<%# Eval("id") %>' runat="server" OnClick = "LinkDetail"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="view_other_comments" ItemStyle-Width="5px" HeaderText="Comments Visible"  SortExpression="view_other_comments" >
                            <ItemStyle Width="10px"></ItemStyle>
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle BackColor="White" ForeColor="#1BA691" HorizontalAlign="center" />
                </asp:GridView>
        </div>
        </asp:View> 
        <asp:View ID="editcontrols" runat="server">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>NAME*</label>
                                <input id="txtname" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>COMMENT VIEWABLE*</label>
                                <telerik:RadComboBox ID="cboView" Skin="Bootstrap" runat="server" AutoPostBack="True" 
                                 Filter="Contains" Font-Names="Verdana" 
                                Font-Size="12px" ForeColor="#666666" RenderMode="Lightweight" 
                                ResolvedRenderMode="Classic" Width="100%" 
                                ToolTip="if users should other users' comments">                                
                            </telerik:RadComboBox>
                            <asp:Label ID="lblid" runat="server" CssClass="lbl" Font-Bold="True" 
                                Font-Names="Verdana" Font-Size="12px" ForeColor="#666666" Visible="False"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-12 m-t-20 text-center">
                                <asp:Button ID="Button1" runat="server" Text="Save" Cssclass="btn btn-success" ForeColor="White"
                            Width="150px" Height="30px" BorderStyle="None" 
                            Font-Names="Verdana" Font-Size="12px" ToolTip="save" />
                            <asp:Button ID="Button2" runat="server" Text="Add" BackColor="White" ForeColor="White"
                            Width="10px" Height="30px" BorderStyle="None" 
                            Font-Names="Verdana" Font-Size="11px" ToolTip="Add new Shift" />
                            <asp:Button ID="btnCancel" runat="server" Cssclass="btn btn-danger"
                                BorderStyle="None" Font-Names="Verdana" Font-Size="12px" ForeColor="White" 
                                Height="30px" Text="Back" Width="150px" 
                                ToolTip="return to blog type list" />
                        </div>
                    </div>
        </asp:View>
    </asp:MultiView>
    </div></div></div>
    </form>
</body>
</html>
</asp:Content>