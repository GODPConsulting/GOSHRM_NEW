<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup ="true" CodeBehind="Gratuity.aspx.vb" Inherits="GOSHRM.Gratuity" EnableEventValidation="false" Debug="true"%>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID = "content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


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

    <script type = "text/javascript">
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

      <script type = "text/javascript">
          function BulkUpload() {
              var confirm_value = document.createElement("INPUT");
              confirm_value.type = "hidden";
              confirm_value.name = "confirm_value";
              if (confirm("Perform bulk picture upload?")) {
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
        .style25
        {
            width: 134px;
        }
         .style30
        {
            width: 550px;
            text-align: right;
        }
        .lbl
        {
            font-family: Candara;
            color: #000000;
        }
        .RadDropDownList { display:inline-block !important; 
                                               width: 225px !important; }
                            .RadDropDownList_Default{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.RadDropDownList{width:160px;line-height:1.3333em;text-align:left;display:inline-block;vertical-align:middle;white-space:nowrap;cursor:default;*zoom:1;*display:inline} .RadDropDownList { display:inline-block !important; 
                                               width: 225px !important; }
                            .RadDropDownList_Default{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.RadDropDownList{width:160px;line-height:1.3333em;text-align:left;display:inline-block;vertical-align:middle;white-space:nowrap;cursor:default;*zoom:1;*display:inline}.RadDropDownList_Default .rddlInner{border-radius:3px;border-color:#aaa;color:#333;background-color:#c1c1c1;background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radGradientButtonSprite.png');background-image:linear-gradient(#fafafa,#e6e6e6)}.RadDropDownList .rddlInner{vertical-align:top}.RadDropDownList .rddlInner{padding:2px 22px 2px 5px;border-width:1px;border-style:solid;display:block;position:relative;overflow:hidden}.RadDropDownList_Default .rddlInner{border-radius:3px;border-color:#aaa;color:#333;background-color:#c1c1c1;background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radGradientButtonSprite.png');background-image:linear-gradient(#fafafa,#e6e6e6)}.RadDropDownList .rddlInner{vertical-align:top}.RadDropDownList .rddlInner{padding:2px 22px 2px 5px;border-width:1px;border-style:solid;display:block;position:relative;overflow:hidden}.RadDropDownList_Default .rddlDefaultMessage{color:#a5a5a5}.RadDropDownList .rddlDefaultMessage{font-style:italic}.RadDropDownList .rddlFakeInput{margin:0;padding:0;width:100%;min-height:16px;display:block;overflow:hidden}.RadDropDownList_Default .rddlDefaultMessage{color:#a5a5a5}.RadDropDownList .rddlDefaultMessage{font-style:italic}.RadDropDownList .rddlFakeInput{margin:0;padding:0;width:100%;min-height:16px;display:block;overflow:hidden}
                            .rddlFakeInput {
                                    height: 16px !important; 
                                    width: 80% !important;}
                            .rddlFakeInput {
                                    height: 16px !important; 
                                    width: 80% !important;}.RadDropDownList_Default .rddlIcon{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radActionsSprite.png');background-position:-22px -20px}.RadDropDownList .rddlIcon{width:16px;height:100%;border:0;background-repeat:no-repeat;position:absolute;top:0;right:0}.RadDropDownList_Default .rddlIcon{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radActionsSprite.png');background-position:-22px -20px}.RadDropDownList .rddlIcon{width:16px;height:100%;border:0;background-repeat:no-repeat;position:absolute;top:0;right:0}.rddlSlide{float:left;display:none;position:absolute;overflow:hidden;z-index:7000}.rddlSlide{float:left;display:none;position:absolute;overflow:hidden;z-index:7000}.rddlPopup_Default{border-color:#a0a0a0;color:#333;background-color:#fff;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.rddlPopup{*zoom:1;border-width:1px;border-style:solid;text-align:left;position:relative;cursor:default;width:160px;*width:158px;box-sizing:border-box}.rddlPopup_Default{border-color:#a0a0a0;color:#333;background-color:#fff;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.rddlPopup{*zoom:1;border-width:1px;border-style:solid;text-align:left;position:relative;cursor:default;width:160px;*width:158px;box-sizing:border-box}
    </style>

<body>
   


    <form id="form1" >
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
                         <div class="col-md-3 col-sm-6 col-xs-12 form-group pull-right">                                  
                            <button id="btnUploadFile" type="button" runat="server" class="fa fa-upload btn btn-default btn-sm"
                                onserverclick="btnUpload_Click" data-toggle="tooltip" data-original-title="CSV File (Comma Delimited): EMPID, MONTH e.g APR, YEAR e.g 2017, ADJUSTED AMOUNT" style="margin-right:10px;margin-left:10px;height:35px"></button>
                                <button id="btExport" data-toggle="tooltip" data-original-title="Export" type="button" runat="server" class="fa fa-download btn btn-default btn-sm" onserverclick="btnExport_Click"
                            style="margin-right:10px;height: 35px"></button>
                                <asp:LinkButton ID="btnDelete" data-toggle="tooltip" data-original-title="Delete" Height="35px" runat="server" CssClass="btn btn-default btn-sm" OnClick="Delete" OnClientClick="Confirm()">
                                    <span style="margin-top:5px" aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                                </asp:LinkButton>
                                 <button id="btAdd" type="button" data-toggle="tooltip" data-original-title="Add" runat="server" class="glyphicon glyphicon-plus btn btn-default btn-sm" onserverclick="btnAdd_Click"
                                style="height: 35px;margin-right:10px;margin-left:10px;"></button> 
                        </div>
                        <div class="col-md-3 col-sm-6 col-xs-12 pull-right">
                            <input style="height:35px;" class="form-control" type="file" id="FileUpload1" runat="server" />
                        </div>
                        
                       <div class="col-md-2 col-sm-6 col-xs-12 pull-right">
                            <telerik:RadComboBox runat="server" RenderMode="Lightweight" 
                    ResolvedRenderMode="Classic" Width="100%" ID="cboYear" Skin ="Bootstrap" 
                        Filter="Contains" Font-Names="Verdana" Font-Size="12px" AutoPostBack="True" 
                            ForeColor="#666666"></telerik:RadComboBox>
                        </div>   
                        <div class="col-md-2 col-sm-6 col-xs-12 pull-right">
                            <telerik:RadComboBox runat="server" RenderMode="Lightweight" 
                    ResolvedRenderMode="Classic" Skin ="Bootstrap" Width="100%" ID="cboMonth" Font-Names="Verdana" 
                            Font-Size="12px" AutoPostBack="True" ForeColor="#666666">
                            </telerik:RadComboBox>
                        </div>
                         <div class="col-md-2 col-sm-6 col-xs-12 pull-right">
                            <telerik:RadComboBox runat="server" 
                     Skin ="Bootstrap" RenderMode="Lightweight" 
                    ResolvedRenderMode="Classic" Width="100%" ID="cboCompany" 
                        Filter="Contains" AutoPostBack="True" Font-Names="Verdana" Font-Size="12px" 
                            ForeColor="#666666"></telerik:RadComboBox>
                        </div>  
                </div>
                <div class="row">
                     <div class="search-box-wrapper col-sm-6 col-md-3 col-xs-12 form-group pull-right">                        
                            <input id="txtsearch" style="width:100%" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                            <button onserverclick="Button1_Click" id="btnFind" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                        </div>
                          <div class="col-md-3 col-sm-6 col-xs-12 pull-right">
                            <asp:Label ID="lblgratuity" runat="server" Font-Names="Verdana" Font-Size="12px" Width="100%" Font-Bold="True" 
                        ForeColor="#666666"></asp:Label>
                        </div>
                        <div class="col-md-4 col-sm-6 col-xs-12 pull-right">
                        <asp:Label ID="lblytdgratuity" runat="server" Font-Names="Verdana" Font-Size="12px" Width="100%" Font-Bold="True" 
                        ForeColor="#666666"></asp:Label>
                            
                        </div>
                       
                </div>

    
    <div class="row">
    
        <div>
            <asp:GridView ID="GridVwHeaderChckbox" runat="server" OnSorting="SortRecords" 
                AllowSorting="True" BorderStyle="Solid" 
                Font-Names="Verdana" PageSize="2000" DataKeyNames="id" 
                OnRowDataBound = "OnRowDataBound" CssClass="table table-condensed" 
                 Width="100%" Height="30px" ToolTip="click row to select record"  
                Font-Size="12px" AutoGenerateColumns="False" GridLines="Vertical" 
                ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC" 
                ShowHeaderWhenEmpty="True" EmptyDataText="No data to display" >
                <RowStyle BackColor="white" />
                <Columns>
                     <asp:TemplateField ItemStyle-Width="5px">
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="CheckAllEmp(this);" />
                        </HeaderTemplate>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemTemplate>
                            <asp:CheckBox ID="chkEmp" runat="server"></asp:CheckBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Rows" ItemStyle-Width="5px" HeaderText="Row" />                                                    
                         <asp:TemplateField HeaderText="Emp ID" ItemStyle-Width="50px" 
                            ItemStyle-Font-Bold="true" SortExpression="empid">
                            <ItemTemplate>                             
                                     <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/Finance/Payroll/gratuityupdate.aspx?id={0}",
                     HttpUtility.UrlEncode(Eval("id").ToString())) %>'
                                            Text='<%# Eval("empid")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />  
                    <asp:BoundField DataField="company" HeaderText="Company" SortExpression="Company" />   
                    <asp:BoundField DataField="office" HeaderText="Office/Dept" SortExpression= "office" />  
                    <asp:BoundField DataField="yearrange" HeaderText="Years in Service" SortExpression= "yearrange"  DataFormatString="{0:n}" ItemStyle-HorizontalAlign="Right"/> 
                    <asp:BoundField DataField="amount" HeaderText="Gratuity" SortExpression= "amount"  DataFormatString="{0:n}" ItemStyle-HorizontalAlign="Right"/>  
                    <asp:BoundField DataField="YTD" HeaderText="YTD Gratuity" SortExpression= "YTD"  DataFormatString="{0:n}" ItemStyle-HorizontalAlign="Right"/>  
                    <asp:BoundField DataField="entrystatus" HeaderText="Entry Mode" SortExpression="entrystatus" />                       
                </Columns>
                <HeaderStyle BackColor="white" ForeColor="#1BA691" HorizontalAlign="center" />
                
            </asp:GridView>
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

            <script type="text/javascript">
                function openWindow(code) {
                    window.open("EmployeeData.aspx?id=" + code, "open_window", "width=1500,height=800");
                }
            </script>

   
           
        </div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
    
    </div>
    </div></div>
    
    </form>
</body>
</html>
</asp:Content>

<asp:Content ID="Content2" runat="server" contentplaceholderid="head">
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


