<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="AppraisalPeriodList.aspx.vb"
    Inherits="GOSHRM.AppraisalPeriodList" EnableEventValidation="false" Debug="true" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title></title>
        <link rel="stylesheet" href="../../../AdminLTE/bootstrap/css/bootstrap.min.css"/>
        <link rel="stylesheet" href="../../../AdminLTE/dist/css/Admin-lte.min.css"/>
        <link rel="stylesheet" href="../../../AdminLTE/plugins/ionicons/css/ionicons.min.css"/>
        <link rel="stylesheet" href="../../../Skins/_all-skins.min.css"/>
        <link rel="stylesheet" href="../../../css/font-awesome.min.css"/>
    </head>
    <script type="text/javascript" language="javascript">
        //    Grid View Check box
        function CheckAllEmp(Checkbox) {
            var GridVwHeaderChckbox = document.getElementById("<%=GridVwHeaderChckbox.ClientID %>");
            for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
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
        .style25
        {
            width: 203px;
        }
        .style26
        {
            width: 22px;
        }
        .style27
        {
            width: 35px;
        }
        .RadComboBox_Default{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.RadComboBox{text-align:left;display:inline-block;vertical-align:middle;white-space:nowrap;*display:inline;*zoom:1}.RadComboBox_Default{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.RadComboBox{text-align:left;display:inline-block;vertical-align:middle;white-space:nowrap;*display:inline;*zoom:1}.RadComboBox .rcbReadOnly .rcbInputCellLeft{background-position:0 -88px}.RadComboBox .rcbReadOnly .rcbInputCellLeft{background-position:0 -88px}.RadComboBox_Default .rcbInputCell{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')}.RadComboBox .rcbInputCellLeft{background-position:0 0}.RadComboBox .rcbInputCell{padding-right:4px;padding-left:5px;width:100%;height:20px;line-height:20px;text-align:left;vertical-align:middle}.RadComboBox .rcbInputCell{padding:0;border-width:0;border-style:solid;background-color:transparent;background-repeat:no-repeat}.RadComboBox_Default .rcbInputCell{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')}.RadComboBox .rcbInputCellLeft{background-position:0 0}.RadComboBox .rcbInputCell{padding-right:4px;padding-left:5px;width:100%;height:20px;line-height:20px;text-align:left;vertical-align:middle}.RadComboBox .rcbInputCell{padding:0;border-width:0;border-style:solid;background-color:transparent;background-repeat:no-repeat}.RadComboBox_Default .rcbReadOnly .rcbInput{color:#333}.RadComboBox .rcbReadOnly .rcbInput{cursor:default}.RadComboBox_Default .rcbReadOnly .rcbInput{color:#333}.RadComboBox .rcbReadOnly .rcbInput{cursor:default}.RadComboBox_Default .rcbInput{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif;line-height:16px}.RadComboBox .rcbInput{margin:0;padding:2px 0 1px;height:auto;width:100%;border-width:0;outline:0;color:inherit;background-color:transparent;vertical-align:top}.RadComboBox_Default .rcbInput{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif;line-height:16px}.RadComboBox .rcbInput{margin:0;padding:2px 0 1px;height:auto;width:100%;border-width:0;outline:0;color:inherit;background-color:transparent;vertical-align:top}.RadComboBox .rcbReadOnly .rcbArrowCellRight{background-position:-162px -176px}.RadComboBox .rcbReadOnly .rcbArrowCellRight{background-position:-162px -176px}.RadComboBox_Default .rcbArrowCell{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')}.RadComboBox .rcbArrowCellRight{background-position:-18px -176px}.RadComboBox .rcbArrowCell{width:18px}.RadComboBox .rcbArrowCell{padding:0;border-width:0;border-style:solid;background-color:transparent;background-repeat:no-repeat}.RadComboBox_Default .rcbArrowCell{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')}.RadComboBox .rcbArrowCellRight{background-position:-18px -176px}.RadComboBox .rcbArrowCell{width:18px}.RadComboBox .rcbArrowCell{padding:0;border-width:0;border-style:solid;background-color:transparent;background-repeat:no-repeat}
        .style28
        {
            border-width: 0;
            padding-left: 5px;
            padding-right: 4px;
            padding-top: 0;
            padding-bottom: 0;
        }
        .style29
        {
            border-width: 0;
            padding: 0;
        }
        </style>
    <body style="">
        <form id="form1">
      <div class="container col-md-12">
              <div class="row">
                     <div id="divalert" runat="server" visible="false" class="alert alert-info">
                        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                        <strong id="msgalert" runat="server">Danger!</strong>
                    </div>
                </div>
          <div id="content" runat="server">
         <div class="panel panel-success">
                <div class="panel-heading">
                    <h5><b id="pagetitle" runat="server"></b></h5>
                </div>
             <div class="panel-body">

         <div class="row">
        <div class="col-sm-3 col-md-4 col-xs-12 form-group">
            <div class="col-md-6">
                <input style="height:35px;width:200px" class="form-control" type="file" id="file1" runat="server" />
            </div>
            <div class="col-md-6">
                <button id="btnUploadFile" type="button" data-toggle="tooltip" data-original-title="360 Feedback Upload(EmpID of Reviewee, EmpID of Reviewer, Company)" runat="server" class="fa fa-upload btn btn-default btn-sm"
                onserverclick="btnUpload_Click" style="height:35px;width:40px"></button>
                <button id="Button1" type="button" data-toggle="tooltip" data-original-title="Upload Of Kpi Score(EmpID of Reviewee,Company,KPI Name, Reviewee Score, Reviewer1 Score,Reviewer2 Score)"  class="fa fa-upload btn btn-default btn-sm" runat="server"
                onserverclick="btnUpload_Click2" style="height:35px;width:40px "></button>
            </div>
           
        </div>
          <div class="search-box-wrapper col-sm-6 col-md-4 col-xs-12 form-group pull-right">             
                        <asp:LinkButton ID="btnDelete" data-toggle="tooltip" data-original-title="Delete" Height="35px" runat="server" CssClass="btn btn-default btn-sm" OnClick="Delete" OnClientClick="Confirm()">
                            <span style="margin-top:5px" aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                        </asp:LinkButton>
                         <button id="btAdd" type="button" data-toggle="tooltip" data-original-title="Add" runat="server" class="glyphicon glyphicon-plus btn btn-default btn-sm" onserverclick="btnAdd_Click"
                        style="margin-left:10px;margin-right:10px;height: 35px;"></button> 
                    <input id="search" style="width:100%" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                    <button onserverclick="Button1_Click" id="btsearch" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                </div>
            <div class="col-sm-6 col-md-3 col-xs-12 pull-right">          
                <telerik:RadComboBox runat="server" Skin="Bootstrap"
                RenderMode="Lightweight" ResolvedRenderMode="Classic" Width="100%" ID="cboCompany"
                AutoPostBack="True" Font-Names="Verdana" ForeColor="#666666">
            </telerik:RadComboBox>
            </div>
       </div>
                 <div class="row"></div>
                </div>
        <div class="row">
                <asp:GridView ID="GridVwHeaderChckbox" runat="server" OnSorting="SortRecords" AllowSorting="True"
                    BorderStyle="Solid" Font-Names="Verdana" AllowPaging="True" PageSize="100" DataKeyNames="id"
                    OnRowDataBound="OnRowDataBound" Width="100%" Height="50px" ToolTip="click row to select record"
                    Font-Size="12px" ShowHeaderWhenEmpty="True" EmptyDataText="No data to display"
                    AutoGenerateColumns="False" GridLines="Both" ForeColor="#666666" BorderWidth="1px"
                    BorderColor="#CCCCCC" CssClass="table table-condensed">
                    <RowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField ItemStyle-Width="1%">
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="CheckAllEmp(this);" />
                            </HeaderTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemTemplate>
                                <asp:CheckBox ID="chkEmp" runat="server"></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Rows" ItemStyle-Width="3%" HeaderText="Rows" />
                        <%--<asp:BoundField DataField="company" ItemStyle-Width="10%" HeaderText="Company" SortExpression="company"  />--%>
                         <asp:BoundField DataField="ReviewYear" ItemStyle-Width="10%" HeaderText="Review Year" ItemStyle-HorizontalAlign="Center" SortExpression="ReviewYear" />
                        <asp:TemplateField HeaderText="Period" ItemStyle-Width="25%" 
                            ItemStyle-Font-Bold="true">
                            <ItemTemplate>
                                <a href='AppraisalPeriodUpdate.aspx?id=<%# Eval("id") %>'><%# Eval("Period")%></a>
                            </ItemTemplate>
                        </asp:TemplateField>                     
                        <asp:BoundField DataField="Due" ItemStyle-Width="12%" HeaderText="Due" ItemStyle-HorizontalAlign="Center" SortExpression="due" />                     
                        <asp:BoundField DataField="Status" ItemStyle-Width="12%" HeaderText="Status" ItemStyle-HorizontalAlign="Left" SortExpression="status" />
                        <asp:BoundField DataField="MgrGradeRatio" ItemStyle-Width="9%" HeaderText="Reviewer 1 Weight" ItemStyle-HorizontalAlign="Right" SortExpression="MgrGradeRatio"/>
                        <asp:BoundField DataField="MgrGradeRatio2" ItemStyle-Width="9%" HeaderText="Reviewer 2 Weight" ItemStyle-HorizontalAlign="Right" SortExpression="MgrGradeRatio2"/>
                        <asp:BoundField DataField="EmpGradeRatio" ItemStyle-Width="9%" HeaderText="Reviewee Weight" ItemStyle-HorizontalAlign="Right" SortExpression="EmpGradeRatio"/>
                         <asp:TemplateField HeaderText="" ItemStyle-Width="10%" 
                            ItemStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                 <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/Performance/Appraisals.aspx?cycleid={0}",
                     HttpUtility.UrlEncode(Eval("id").ToString())) %>'
                        Text='Appraisals' />

                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle BackColor="White" ForeColor="#1BA691" HorizontalAlign="center" />
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
                        window.open("AppraisalPeriodUpdate.aspx?id=" + code, "open_window", "width=700,height=600");
                    }
                </script>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
        </div></div></div></div>
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
