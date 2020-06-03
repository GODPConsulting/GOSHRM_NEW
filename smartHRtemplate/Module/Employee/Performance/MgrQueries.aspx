<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="MgrQueries.aspx.vb"
    Inherits="GOSHRM.MgrQueries" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
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
        <div class="search-box-wrapper col-sm-6 col-md-4 col-xs-12 form-group pull-right">
                        <asp:LinkButton ID="btDelete" data-toggle="tooltip" data-original-title="Delete" Height="35px" runat="server" CssClass="btn btn-default btn-sm" OnClick="Delete" OnClientClick="Confirm()">
                            <span style="margin-top:5px" aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                        </asp:LinkButton>
                         <button id="btAdd" type="button" data-toggle="tooltip" data-original-title="Add Query" runat="server" class="glyphicon glyphicon-plus btn btn-default btn-sm" onserverclick="btnApply_Click"
                        style="margin-left:10px;margin-right:10px;height: 35px;"></button> 
                    <input id="search" style="width:100%" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                    <button onserverclick="btnFind_Click" id="btsearch" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                </div>          
        </div>

        <div class="row">
            <div class="table-responsive">
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
                                <asp:TemplateField HeaderText="Employee" ItemStyle-Font-Bold="true" SortExpression="Employee">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/employee/performance/QueryROPage.aspx?id={0}",
                     HttpUtility.UrlEncode(Eval("id").ToString()),HttpUtility.UrlEncode(Eval("Employee").ToString())) %>' Text='<%# Eval("Employee")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:BoundField DataField="ROStatus" HeaderText="Query Status" SortExpression="ROStatus" />                                
                                <asp:BoundField DataField="QueryDate" HeaderText="Query Date" SortExpression="QueryDate" DataFormatString="{0:dd, MMM yyyy}"/>
                                <asp:BoundField DataField="EmpStatus" HeaderText="Employee Reply"  SortExpression="EmpStatus"  />
                                <asp:BoundField DataField="EmpResponseDate" HeaderText="Reply Date" SortExpression="EmpResponseDate" DataFormatString="{0:dd, MMM yyyy}"/>                                
                                <asp:BoundField DataField="HRAction" HeaderText="Action" SortExpression="HRAction"  />                                           
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
            </div>
        </div> 
        </div>
        </div> 

<%--



    
        <div style="border: thin solid #C0C0C0">
            <table width="100%">
                <tr>
                    <td >
                        <asp:Label ID="lblView" runat="server" Font-Names="Candara" Font-Size="Medium" Width="100%"
                            Style="font-weight: 700; color: #FF6600"></asp:Label>
                    </td>
                </tr>
            </table>
            <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        </div>
        <div style="height: 163px">
            <div>
            
                           
                            <div>
                                <table class="style21">
                                    <tr>
                                        <td class="style22">
                                            <asp:TextBox ID="txtsearch" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Height="20px" Style="margin-left: 0px" TextMode="Search" 
                                                Width="251px" Font-Names="Verdana" Font-Size="11px" ForeColor="#666666"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnFind" runat="server" BackColor="#1BA691" BorderStyle="None" ForeColor="White"
                                                Height="20px" Text="View" Width="100px" Font-Names="Verdana" Font-Size="11px">
                                            </asp:Button>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnApply" runat="server" BackColor="#1BA691" BorderStyle="None" ForeColor="White"
                                                Height="20px" Text="Issue a Query" Width="100px" Font-Names="Verdana" Font-Size="11px">
                                            </asp:Button>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnDelete" runat="server" BackColor="#FF3300" BorderStyle="None"
                                                ForeColor="White" Height="20px" OnClick="Delete" OnClientClick="Confirm()" Text="Delete Query"
                                                Width="100px" Font-Names="Verdana" Font-Size="11px"></asp:Button>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                           
                            <div>
                                <asp:GridView ID="GridVwHeaderChckbox" runat="server" AllowPaging="True" AllowSorting="True"
                                    AutoGenerateColumns="False" GridLines="Vertical" ForeColor="#666666" 
                                    BorderWidth="1px" BorderColor="#CCCCCC" BorderStyle="Solid" DataKeyNames="id" Font-Names="Verdana"
                                    Font-Size="11px" Height="50px" OnSorting="SortSurbodinateRecords"
                                    PageSize="100" ShowHeaderWhenEmpty="True" EmptyDataText="No data to display" ToolTip="click row to select record"
                                    Width="100%">
                                    <AlternatingRowStyle BackColor="#CCCCCC"></AlternatingRowStyle>
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="CheckAllEmp(this);" /></HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkEmp" runat="server" /></ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3px"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Rows" HeaderText="Rows">
                                            <ItemStyle Width="5px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Employee" SortExpression="Employee">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/employee/performance/QueryROPage.aspx?id={0}",
                     HttpUtility.UrlEncode(Eval("id").ToString())) %>' onclick="window.open (this.href, 'popupwindow',  'width=700,height=500,scrollbars,resizable'); return false;"
                                                    Text='<%# Eval("Employee")%>' /></ItemTemplate>
                                            <ItemStyle Font-Bold="True" Width="150px" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="jobtitle" HeaderText="Job Title">
                                            <ItemStyle Width="150px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="jobgrade" HeaderText="Job Grade">
                                            <ItemStyle Width="120px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ROStatus" HeaderText="Query Status">
                                            <ItemStyle HorizontalAlign="Center" Width="70px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="EmpStatus" HeaderText="Employee Reply Status">
                                            <ItemStyle HorizontalAlign="Center" Width="70px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="QueryDate" HeaderText="Query Date">
                                            <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="EmpResponseDate" HeaderText="Employee Reply Date">
                                            <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="HRAction" HeaderText="Disciplnary Action">
                                            <ItemStyle Width="100px"></ItemStyle>
                                        </asp:BoundField>
                                    </Columns>
                                    <HeaderStyle BackColor="#1BA691" ForeColor="White" HorizontalAlign="Center"></HeaderStyle>
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
                            </div>
            </div>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
        </div>--%>
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
