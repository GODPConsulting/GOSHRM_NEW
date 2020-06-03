<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="CoacheeAppraisalObjectives.aspx.vb"
    Inherits="GOSHRM.CoacheeAppraisalObjectives" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
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
        .style28
        {
            width: 200px;
        }
        .style40
        {
            width: 400px;
        }
        .style29
        {
            width: 363px;
        }
    </style>
    <body>
        <form id="form1" action="">
        <div class="container col-md-12">
        <div class="row">
           <div id="divalert" runat="server" visible="false" class="alert alert-info">
            <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <strong
                id="msgalert" runat="server">Danger!</strong>
        </div>
        </div>
       <div class="panel panel-success">
                <div class="panel-heading">
                    <h5><b id="pagetitle" runat="server"></b></h5>
                </div>
             <div class="panel-body">
        <div class="row">
             <div class="search-box-wrapper col-sm-6 col-md-3 col-xs-12 form-group pull-right">
                    <input id="search" style="width:100%" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                    <button onserverclick="btnFind_Click" id="btsearch" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                </div>
             <div class="col-sm-6 col-md-3 col-xs-12 pull-right">
             <telerik:RadComboBox ID="cboYear" runat="server" Skin="Bootstrap"
                                    AutoPostBack="True" Width="100%" ForeColor="#666666" Sort="Descending">
                                </telerik:RadComboBox>
             </div>
        </div>
        <div class="row">
        <div class="table-responsive">
                <asp:GridView ID="gridCoach" runat="server" OnSorting="SortRecords" AllowSorting="True"
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
                        <asp:BoundField DataField="ReviewYear" HeaderText="Review Year" SortExpression="ReviewYear"  />
                        <asp:TemplateField HeaderText="Review Period"  ItemStyle-Font-Bold="true"
                            SortExpression="period">
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/Employee/Performance/AppObjectiveUpdate.aspx?id={0}",
                     HttpUtility.UrlEncode(Eval("id").ToString())) %>' Text='<%# Eval("period")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:BoundField DataField="EmpName" HeaderText="Employee" SortExpression="EmpName"  />
                        <asp:BoundField DataField="JobGrade" HeaderText="Job Grade" SortExpression="JobGrade"/>
                        <asp:BoundField DataField="dept" HeaderText="Office" SortExpression="dept"/>
                        <asp:BoundField DataField="AppraisalStatus" HeaderText="Review Stat" SortExpression="AppraisalStatus"/> 
                        <asp:BoundField DataField="CoachApprovalStatus" HeaderText="Approval Stat" SortExpression="CoachApprovalStatus"/> 
                    </Columns>
                    <HeaderStyle BackColor="White" ForeColor="#1BA691" HorizontalAlign="center" />
                </asp:GridView>
                <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
                <script type="text/javascript">
                    $(function () {
                        $("[id*=gridCoach] td").hover(function () {
                            $("td", $(this).closest("tr")).addClass("hover_row");
                        }, function () {
                            $("td", $(this).closest("tr")).removeClass("hover_row");
                        })
                    })
                </script>
            </div>
       
       </div>
       </div>
        </div></div>
        </form>
    </body>
    </html>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .style21
        {
            width: 435px;
        }
        .style22
        {
        }
    </style>
</asp:Content>
