<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="Queries.aspx.vb"
    Inherits="GOSHRM.Queries" EnableEventValidation="false" Debug="true" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

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
        .RadComboBox_Default{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.RadComboBox{text-align:left;display:inline-block;vertical-align:middle;white-space:nowrap;*display:inline;*zoom:1}.RadComboBox_Default .rcbReadOnly .rcbInput{color:#333}.RadComboBox .rcbReadOnly .rcbInput{cursor:default}.RadComboBox_Default .rcbInput{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif;line-height:16px}.RadComboBox .rcbInput{margin:0;padding:2px 0 1px;height:auto;width:100%;border-width:0;outline:0;color:inherit;background-color:transparent;vertical-align:top}
        .lbl
        {
            font-family: Candara;
            color: #000000;
        }
        </style>
    <body>
        <form id="form1">
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
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        <div class="row">
             <div class="search-box-wrapper col-sm-6 col-md-4 col-xs-12 form-group pull-right">
                        <asp:LinkButton ID="btnDelete" data-toggle="tooltip" data-original-title="Delete" Height="35px" runat="server" CssClass="btn btn-default btn-sm" OnClick="Delete" OnClientClick="Confirm()">
                            <span style="margin-top:5px" aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                        </asp:LinkButton>
                    <input id="txtsearch" style="width:100%; margin-left:10px;" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                    <button onserverclick="btnFind_Click" id="btsearch" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                </div>
                <div class="col-md-3 col-sm-6 col-xs-12 pull-right"> 
                    <telerik:RadComboBox runat="server" Skin="Bootstrap" RenderMode="Lightweight" 
                    ResolvedRenderMode="Classic" Width="100%" ID="cboCompany" AutoPostBack="True" 
                        Filter="Contains" Font-Names="Verdana" ForeColor="#666666">
                        </telerik:RadComboBox>
                </div>
        </div>
        <%--<div>
            <table>
                <tr>
                    <td>
                    <asp:Label ID="lblcompany" runat="server" CssClass="lbl" Text="Company" Font-Names="Verdana"
                        Font-Size="11px" Font-Bold="True" ForeColor="#666666"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadComboBox runat="server" 
                    DropDownAutoWidth="Enabled" RenderMode="Lightweight" 
                    ResolvedRenderMode="Classic" Width="300px" ID="cboCompany" AutoPostBack="True" 
                        Filter="Contains" Font-Names="Verdana" Font-Size="11px" ForeColor="#666666">
                        </telerik:RadComboBox>
                    </td>
                    <td class="style22">
                        <asp:TextBox ID="txtsearch" runat="server" Width="251px" Height="20px" BorderColor="#CCCCCC"
                            BorderWidth="1px" TextMode="Search"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnFind" runat="server" Text="Search" BackColor="#1BA691" ForeColor="White"
                            Width="80px" Height="20px" BorderStyle="None" Font-Names="Verdana" 
                            Font-Size="11px" />
                    </td>
                    <td>
                    </td>
                     <td>
                                    

                                    <asp:Button runat="server" OnClientClick="Confirm()" Text="Delete" 
                            BackColor="#FF3300" BorderStyle="None" ForeColor="White" Height="20px" 
                            Width="80px" ID="btnDelete" OnClick="Delete" Font-Names="Verdana" 
                             Font-Size="11px"></asp:Button>

                                    


                    </td>
                </tr>
            </table>
        </div>--%>
     
        <div class="row">
            <div>
                <asp:GridView ID="GridVwHeaderChckbox" runat="server" OnSorting="SortRecords" AllowSorting="True"
                    BorderStyle="Solid" Font-Names="Verdana" AllowPaging="True" PageSize="500" DataKeyNames="id" CssClass="table table-condensed"
                    OnRowDataBound="OnRowDataBound" Width="100%" Height="50px" ToolTip="click row to select record"
                    Font-Size="11px" AutoGenerateColumns="False" GridLines="Vertical" ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC" ShowHeaderWhenEmpty="True" EmptyDataText="No data to display">
                    <RowStyle BackColor="white" />
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="CheckAllEmp(this);" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkEmp" runat="server"></asp:CheckBox>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="2px"></ItemStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Rows" HeaderText="Rows" />
                        <asp:BoundField DataField="EmpID" HeaderText="Emp No" ItemStyle-HorizontalAlign="Center" />
                        <asp:TemplateField HeaderText="Employee" ItemStyle-Font-Bold="true" SortExpression="Employee" >
                            <ItemTemplate>
                                <a href="QueriesUpdate.aspx?id=<%# Eval("id") %>">
                                    <%# Eval("Employee")%></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="jobgrade" HeaderText="Job Grade" />
                        <asp:BoundField DataField="jobtitle" HeaderText="Job Title" />
                        <asp:BoundField DataField="ReportingOfficer" HeaderText="Issuer" SortExpression="ReportingOfficer" />
                        <asp:BoundField DataField="QueryDate" HeaderText="Query Date" ItemStyle-HorizontalAlign="Center" SortExpression="QueryDate" />
                        <asp:BoundField DataField="EmpResponseDate" HeaderText="Expected Response Date"
                            ItemStyle-HorizontalAlign="Center" SortExpression="EmpResponseDate" />
                        <asp:BoundField DataField="ExpectedResponseTime" HeaderText="Expected Response Time"
                            ItemStyle-HorizontalAlign="Center" />
                             <asp:BoundField DataField="EmpResponseDate" HeaderText="Employee Response Date"  ItemStyle-HorizontalAlign="Center"/>
                        <asp:BoundField DataField="HRAction" HeaderText="Action"/>

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
                        window.open("QueriesUpdate.aspx?id=" + code, "open_window", "width=700,height=700");
                    }
                </script>
            </div>
            <div>
                <table>
                    <tr>
                        <td>
                            
                        </td>
                    </tr>
                </table>
            </div>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
        </div>
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
