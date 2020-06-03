<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="EmpLeaveAllowanceSetup.aspx.vb"
    Inherits="GOSHRM.EmpLeaveAllowanceSetup" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
    <title></title>

    <script type="text/javascript">
        function openWindow(code) {
            window.open("Leaveallowancegrade.aspx?id=" + code, "open_window", "width=500,height=400");
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
    <style type="text/css">
        body
        {
            font-family: Arial;
            font-size: 10pt;
        }
        td
        {
            cursor: pointer;
        }
        .hover_row
        {
            background-color: #A1DCF2;
        }
        
        
        
        .style35
        {
        }
        .style37
        {
            width: 213px;
        }
    .RadComboBox_Default{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.RadComboBox{text-align:left;display:inline-block;vertical-align:middle;white-space:nowrap;*display:inline;*zoom:1}.RadComboBox_Default{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.RadComboBox{text-align:left;display:inline-block;vertical-align:middle;white-space:nowrap;*display:inline;*zoom:1}.RadComboBox_Default{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.RadComboBox{text-align:left;display:inline-block;vertical-align:middle;white-space:nowrap;*display:inline;*zoom:1}.RadComboBox_Default{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.RadComboBox{text-align:left;display:inline-block;vertical-align:middle;white-space:nowrap;*display:inline;*zoom:1}.RadComboBox_Default{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.RadComboBox{text-align:left;display:inline-block;vertical-align:middle;white-space:nowrap;*display:inline;*zoom:1}.RadComboBox_Default{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.RadComboBox{text-align:left;display:inline-block;vertical-align:middle;white-space:nowrap;*display:inline;*zoom:1}.RadComboBox_Default{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.RadComboBox{text-align:left;display:inline-block;vertical-align:middle;white-space:nowrap;*display:inline;*zoom:1}.RadComboBox .rcbReadOnly .rcbInputCellLeft{background-position:0 -88px}.RadComboBox .rcbReadOnly .rcbInputCellLeft{background-position:0 -88px}.RadComboBox .rcbReadOnly .rcbInputCellLeft{background-position:0 -88px}.RadComboBox .rcbReadOnly .rcbInputCellLeft{background-position:0 -88px}.RadComboBox .rcbReadOnly .rcbInputCellLeft{background-position:0 -88px}.RadComboBox .rcbReadOnly .rcbInputCellLeft{background-position:0 -88px}.RadComboBox .rcbReadOnly .rcbInputCellLeft{background-position:0 -88px}.RadComboBox_Default .rcbInputCell{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')}.RadComboBox .rcbInputCellLeft{background-position:0 0}.RadComboBox .rcbInputCell{padding-right:4px;padding-left:5px;width:100%;height:20px;line-height:20px;text-align:left;vertical-align:middle}.RadComboBox .rcbInputCell{padding:0;border-width:0;border-style:solid;background-color:transparent;background-repeat:no-repeat}.RadComboBox_Default .rcbInputCell{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')}.RadComboBox .rcbInputCellLeft{background-position:0 0}.RadComboBox .rcbInputCell{padding-right:4px;padding-left:5px;width:100%;height:20px;line-height:20px;text-align:left;vertical-align:middle}.RadComboBox .rcbInputCell{padding:0;border-width:0;border-style:solid;background-color:transparent;background-repeat:no-repeat}.RadComboBox_Default .rcbInputCell{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')}.RadComboBox .rcbInputCellLeft{background-position:0 0}.RadComboBox .rcbInputCell{padding-right:4px;padding-left:5px;width:100%;height:20px;line-height:20px;text-align:left;vertical-align:middle}.RadComboBox .rcbInputCell{padding:0;border-width:0;border-style:solid;background-color:transparent;background-repeat:no-repeat}.RadComboBox_Default .rcbInputCell{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')}.RadComboBox .rcbInputCellLeft{background-position:0 0}.RadComboBox .rcbInputCell{padding-right:4px;padding-left:5px;width:100%;height:20px;line-height:20px;text-align:left;vertical-align:middle}.RadComboBox .rcbInputCell{padding:0;border-width:0;border-style:solid;background-color:transparent;background-repeat:no-repeat}.RadComboBox_Default .rcbInputCell{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')}.RadComboBox .rcbInputCellLeft{background-position:0 0}.RadComboBox .rcbInputCell{padding-right:4px;padding-left:5px;width:100%;height:20px;line-height:20px;text-align:left;vertical-align:middle}.RadComboBox .rcbInputCell{padding:0;border-width:0;border-style:solid;background-color:transparent;background-repeat:no-repeat}.RadComboBox_Default .rcbInputCell{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')}.RadComboBox .rcbInputCellLeft{background-position:0 0}.RadComboBox .rcbInputCell{padding-right:4px;padding-left:5px;width:100%;height:20px;line-height:20px;text-align:left;vertical-align:middle}.RadComboBox .rcbInputCell{padding:0;border-width:0;border-style:solid;background-color:transparent;background-repeat:no-repeat}.RadComboBox_Default .rcbInputCell{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')}.RadComboBox .rcbInputCellLeft{background-position:0 0}.RadComboBox .rcbInputCell{padding-right:4px;padding-left:5px;width:100%;height:20px;line-height:20px;text-align:left;vertical-align:middle}.RadComboBox .rcbInputCell{padding:0;border-width:0;border-style:solid;background-color:transparent;background-repeat:no-repeat}.RadComboBox_Default .rcbReadOnly .rcbInput{color:#333}.RadComboBox .rcbReadOnly .rcbInput{cursor:default}.RadComboBox_Default .rcbReadOnly .rcbInput{color:#333}.RadComboBox .rcbReadOnly .rcbInput{cursor:default}.RadComboBox_Default .rcbReadOnly .rcbInput{color:#333}.RadComboBox .rcbReadOnly .rcbInput{cursor:default}.RadComboBox_Default .rcbReadOnly .rcbInput{color:#333}.RadComboBox .rcbReadOnly .rcbInput{cursor:default}.RadComboBox_Default .rcbReadOnly .rcbInput{color:#333}.RadComboBox .rcbReadOnly .rcbInput{cursor:default}.RadComboBox_Default .rcbReadOnly .rcbInput{color:#333}.RadComboBox .rcbReadOnly .rcbInput{cursor:default}.RadComboBox_Default .rcbReadOnly .rcbInput{color:#333}.RadComboBox .rcbReadOnly .rcbInput{cursor:default}.RadComboBox_Default .rcbInput{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif;line-height:16px}.RadComboBox .rcbInput{margin:0;padding:2px 0 1px;height:auto;width:100%;border-width:0;outline:0;color:inherit;background-color:transparent;vertical-align:top}.RadComboBox_Default .rcbInput{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif;line-height:16px}.RadComboBox .rcbInput{margin:0;padding:2px 0 1px;height:auto;width:100%;border-width:0;outline:0;color:inherit;background-color:transparent;vertical-align:top}.RadComboBox_Default .rcbInput{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif;line-height:16px}.RadComboBox .rcbInput{margin:0;padding:2px 0 1px;height:auto;width:100%;border-width:0;outline:0;color:inherit;background-color:transparent;vertical-align:top}.RadComboBox_Default .rcbInput{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif;line-height:16px}.RadComboBox .rcbInput{margin:0;padding:2px 0 1px;height:auto;width:100%;border-width:0;outline:0;color:inherit;background-color:transparent;vertical-align:top}.RadComboBox_Default .rcbInput{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif;line-height:16px}.RadComboBox .rcbInput{margin:0;padding:2px 0 1px;height:auto;width:100%;border-width:0;outline:0;color:inherit;background-color:transparent;vertical-align:top}.RadComboBox_Default .rcbInput{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif;line-height:16px}.RadComboBox .rcbInput{margin:0;padding:2px 0 1px;height:auto;width:100%;border-width:0;outline:0;color:inherit;background-color:transparent;vertical-align:top}.RadComboBox_Default .rcbInput{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif;line-height:16px}.RadComboBox .rcbInput{margin:0;padding:2px 0 1px;height:auto;width:100%;border-width:0;outline:0;color:inherit;background-color:transparent;vertical-align:top}.RadComboBox .rcbReadOnly .rcbArrowCellRight{background-position:-162px -176px}.RadComboBox .rcbReadOnly .rcbArrowCellRight{background-position:-162px -176px}.RadComboBox .rcbReadOnly .rcbArrowCellRight{background-position:-162px -176px}.RadComboBox .rcbReadOnly .rcbArrowCellRight{background-position:-162px -176px}.RadComboBox .rcbReadOnly .rcbArrowCellRight{background-position:-162px -176px}.RadComboBox .rcbReadOnly .rcbArrowCellRight{background-position:-162px -176px}.RadComboBox .rcbReadOnly .rcbArrowCellRight{background-position:-162px -176px}.RadComboBox_Default .rcbArrowCell{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')}.RadComboBox .rcbArrowCellRight{background-position:-18px -176px}.RadComboBox .rcbArrowCell{width:18px}.RadComboBox .rcbArrowCell{padding:0;border-width:0;border-style:solid;background-color:transparent;background-repeat:no-repeat}.RadComboBox_Default .rcbArrowCell{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')}.RadComboBox .rcbArrowCellRight{background-position:-18px -176px}.RadComboBox .rcbArrowCell{width:18px}.RadComboBox .rcbArrowCell{padding:0;border-width:0;border-style:solid;background-color:transparent;background-repeat:no-repeat}.RadComboBox_Default .rcbArrowCell{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')}.RadComboBox .rcbArrowCellRight{background-position:-18px -176px}.RadComboBox .rcbArrowCell{width:18px}.RadComboBox .rcbArrowCell{padding:0;border-width:0;border-style:solid;background-color:transparent;background-repeat:no-repeat}.RadComboBox_Default .rcbArrowCell{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')}.RadComboBox .rcbArrowCellRight{background-position:-18px -176px}.RadComboBox .rcbArrowCell{width:18px}.RadComboBox .rcbArrowCell{padding:0;border-width:0;border-style:solid;background-color:transparent;background-repeat:no-repeat}.RadComboBox_Default .rcbArrowCell{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')}.RadComboBox .rcbArrowCellRight{background-position:-18px -176px}.RadComboBox .rcbArrowCell{width:18px}.RadComboBox .rcbArrowCell{padding:0;border-width:0;border-style:solid;background-color:transparent;background-repeat:no-repeat}.RadComboBox_Default .rcbArrowCell{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')}.RadComboBox .rcbArrowCellRight{background-position:-18px -176px}.RadComboBox .rcbArrowCell{width:18px}.RadComboBox .rcbArrowCell{padding:0;border-width:0;border-style:solid;background-color:transparent;background-repeat:no-repeat}.RadComboBox_Default .rcbArrowCell{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')}.RadComboBox .rcbArrowCellRight{background-position:-18px -176px}.RadComboBox .rcbArrowCell{width:18px}.RadComboBox .rcbArrowCell{padding:0;border-width:0;border-style:solid;background-color:transparent;background-repeat:no-repeat}
        </style>



    <body>
        <form id="form1">
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
             <div class="row">
          <div class="search-box-wrapper col-sm-6 col-md-3 col-xs-12 form-group pull-right">                        
                                    <input id="txtsearch" style="width:100%" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                                    <button onserverclick="btnFind_Click" id="btsearch" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                                </div>
                                 <div class="col-md-3 col-sm-6 col-xs-12 form-group pull-right">
                                 <button id="Button1" type="button" data-toggle="tooltip" data-original-title="Back" runat="server" class="fa fa-backward btn btn-default btn-sm" onserverclick="btnBack_Click"
                                        style="height: 35px;"></button>                                   
                                    <button id="btnUpload" type="button" runat="server" class="fa fa-upload btn btn-default btn-sm"
                                        onserverclick="btnUpload_Click" data-toggle="tooltip" data-original-title="CSV File: EmpID, allowance(%)" style="margin-right:10px;margin-left:10px;height:35px"></button>
                                        <button id="btnExport" data-toggle="tooltip" data-original-title="Export" type="button" runat="server" class="fa fa-download btn btn-default btn-sm" onserverclick="btnExport_Click"
                                    style="margin-right:10px;height: 35px"></button>
                                        <asp:LinkButton ID="btnDelete" data-toggle="tooltip" data-original-title="Delete" Height="35px" runat="server" CssClass="btn btn-default btn-sm" OnClick="btnDelete_Click" OnClientClick="Confirm()">
                                            <span style="margin-top:5px" aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                                        </asp:LinkButton>
                                         <button id="btnAddGrade" type="button" data-toggle="tooltip" data-original-title="Add" runat="server" class="glyphicon glyphicon-plus btn btn-default btn-sm" onserverclick="btnAddGrade_Click"
                                        style="height: 35px;margin-right:10px;margin-left:10px;"></button> 
                                </div>
                                <div class="col-md-3 col-sm-6 col-xs-12 pull-right">
                                    <input style="height:35px;" class="form-control" type="file" id="FileUpload1" runat="server" />
                                </div>     
                <div class="col-md-3 col-sm-6 col-xs-12 pull-right">
                     <telerik:RadComboBox ID="cboCompany" Runat="server" Skin="Bootstrap" 
                                        EmptyMessage="--Select--" Width="100%" 
                                        Font-Names="Verdana" Font-Size="12px" AutoPostBack="True" 
                                        BorderColor="#CCCCCC" ForeColor="#666666">
                                    </telerik:RadComboBox>
                </div>     
        </div>
        <div class="row">
            <asp:GridView runat="server" AllowPaging="True" AllowSorting="True" 
                    AutoGenerateColumns="False" GridLines="Vertical" ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC" DataKeyNames="id" PageSize="200" 
                    ShowHeaderWhenEmpty="True" EmptyDataText="No data to display" BorderStyle="Solid" Font-Names="Verdana" 
                    Font-Size="12px" Height="50px" ToolTip="click row to select record" Width="100%" 
                    ID="GridVwHeaderChckbox" CssClass="table table-condensed" 
                    OnSorting="SortRecords">
                   <RowStyle BackColor="White" />
                    <Columns>
                    <asp:TemplateField ItemStyle-Width="1px"><HeaderTemplate>
                            <asp:CheckBox ID="chkboxSelectAll" runat="server" 
                                onclick="CheckAllEmp(this);" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkEmp" runat="server" />                                            
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5px"></ItemStyle>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Rows" HeaderText="Rows">
                    <ItemStyle Width="5px"></ItemStyle>
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Employee ID" ItemStyle-Width="50px" ItemStyle-Font-Bold="true">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/Finance/Settings/EmpLeaveAllowanceUpdate.aspx?id={0}",
                     HttpUtility.UrlEncode(Eval("id").ToString())) %>'
                                            Text='<%# Eval("empid")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>   
                    <asp:BoundField DataField="name" HeaderText="Name">
                    <ItemStyle Width="100px"></ItemStyle>
                    </asp:BoundField>                
                    <asp:BoundField DataField="allowance" DataFormatString="{0:n}" HeaderText="Leave Allowance(%)" SortExpression="allowance" ItemStyle-Width="15px" ItemStyle-HorizontalAlign="Right">                    
                    </asp:BoundField>                    
                    </Columns>
                    <HeaderStyle HorizontalAlign="Center" BackColor="white" ForeColor="#1BA691"></HeaderStyle>
                    </asp:GridView>
        </div>
        <%--<table width="100%">
            <tr>
                <td class="style37">
                    &nbsp;</td>
                <td class="style35">
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Font-Names="Verdana" Font-Size="11px" Width="70px" 
                                    Text="Company" ForeColor="#666666" Font-Bold="True"></asp:Label>
                            </td>
                            <td>
                                    <telerik:RadComboBox ID="cboCompany" Runat="server" 
                                        EmptyMessage="--Select--" Width="400px" 
                                        Font-Names="Verdana" Font-Size="11px" AutoPostBack="True" 
                                        BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" 
                                        RenderMode="Lightweight" ForeColor="#666666">
                                    </telerik:RadComboBox>
                               </td>
                            <td>
                            </td>
                            <td>
                                <asp:Button ID="btnAddGrade" runat="server" Text="Add" BackColor="#1BA691" ForeColor="White"
                                Width="100px" Height="20px" BorderStyle="None" Font-Names="Verdana" 
                                Font-Size="11px" ToolTip="Add new leave allowance grade" />                 
                            </td>
                            <td>
                                <asp:Button ID="btnDelete" runat="server" Text="Delete" BackColor="#FF3300" ForeColor="White"
                                Width="100px" Height="20px" BorderStyle="None" Font-Names="Verdana" 
                                Font-Size="11px" 
                                    onclientclick="Confirm()" />
                            </td>
                            <td>
                               <asp:Button ID="btnExport" runat="server" Text="Export" BackColor="#FF9933" ForeColor="White"
                                    Width="100px" Height="20px" BorderStyle="None" Font-Names="Verdana" 
                                    Font-Size="11px" />
                            </td>
                            <td>
                            </td>
                             <td>
                                <asp:FileUpload ID="FileUpload1" runat="server" Font-Names="Verdana" 
                                    Font-Size="11px" />
                            </td>
                             <td>
                                <asp:Button ID="btnUpload" runat="server" BackColor="#00CC99" 
                                    BorderStyle="None" ForeColor="White" Height="20px" Text="Upload File" 
                                    ToolTip="CSV File: EmpID, allowance(%)" 
                                    Width="100px" Font-Names="Verdana" Font-Size="11px" />
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:TextBox ID="txtsearch" runat="server" BorderColor="#CCCCCC" BorderWidth="1px"
                                    Height="20px" Width="251px" TextMode="Search" Font-Names="Verdana" 
                                    Font-Size="11px" ForeColor="#666666"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnFind" runat="server" Text="Search" BackColor="#1BA691" ForeColor="White"
                                    Width="80px" Height="20px" BorderStyle="None" Font-Names="Verdana" 
                                    Font-Size="11px" />
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:Button ID="btnBack" runat="server" Text="&lt;&lt; Back" 
                                    BackColor="#999966" ForeColor="White"
                                    Width="80px" Height="20px" BorderStyle="None" Font-Names="Verdana" 
                                    Font-Size="11px" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
             
            <tr>
                <td class="style37">
                </td>
                <td class="style35">
                   <asp:GridView runat="server" AllowPaging="True" AllowSorting="True" 
                    AutoGenerateColumns="False" GridLines="Vertical" ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC" DataKeyNames="id" PageSize="200" 
                    ShowHeaderWhenEmpty="True" EmptyDataText="No data to display" BorderStyle="Solid" Font-Names="Verdana" 
                    Font-Size="11px" Height="10px" ToolTip="click row to select record" Width="100%" 
                    ID="GridVwHeaderChckbox" 
                    OnSorting="SortRecords">
                    <AlternatingRowStyle BackColor="#CCCCCC"></AlternatingRowStyle>
                    <Columns>
                    <asp:TemplateField ItemStyle-Width="1px"><HeaderTemplate>
                            <asp:CheckBox ID="chkboxSelectAll" runat="server" 
                                onclick="CheckAllEmp(this);" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkEmp" runat="server" />                                            
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5px"></ItemStyle>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Rows" HeaderText="Rows">
                    <ItemStyle Width="5px"></ItemStyle>
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Employee ID" ItemStyle-Width="50px" ItemStyle-Font-Bold="true">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/Finance/Settings/EmpLeaveAllowanceUpdate.aspx?id={0}",
                     HttpUtility.UrlEncode(Eval("id").ToString())) %>' onclick="window.open (this.href, 'popupwindow',  'width=700,height=400,scrollbars,resizable'); return false;"
                                            Text='<%# Eval("empid")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>   
                    <asp:BoundField DataField="name" HeaderText="Name">
                    <ItemStyle Width="100px"></ItemStyle>
                    </asp:BoundField>                
                    <asp:BoundField DataField="allowance" DataFormatString="{0:n}" HeaderText="Leave Allowance(%)" SortExpression="allowance" ItemStyle-Width="15px" ItemStyle-HorizontalAlign="Right">                    
                    </asp:BoundField>                    
                    </Columns>
                    <HeaderStyle HorizontalAlign="Center" BackColor="#1BA691" ForeColor="White"></HeaderStyle>
                    </asp:GridView>
                 </td>
                  <td class="style37">
                </td>
            </tr>
          
        </table>--%>
        </form>
    </body>
    </html>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">

    <script type="text/javascript" id="telerikClientEvents2">
//<![CDATA[

	function cboApprove_DropDownClosing(sender,args)
	{
	    //Add JavaScript handler code here
	    document.getElementById("Button1").click();
	}
//]]>
</script>

</asp:Content>
