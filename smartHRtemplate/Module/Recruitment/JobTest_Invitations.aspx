<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="JobTest_Invitations.aspx.vb"
    Inherits="GOSHRM.JobTest_Invitations" EnableEventValidation="false" Debug="true" %>

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
            width: 134px;
        }
        .style26
        {
            width: 22px;
        }
        .style27
        {
            width: 35px;
        }
        .style28
        {            width: 177px;
        }
    </style>
    <body>
        <form id="form1">
         <div class="container col-md-12">
            <div class="row">
                    <div id="divalert" runat="server" visible="false" class="alert alert-info">
                        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <strong
                            id="msgalert" runat="server">Danger!</strong>
                         <asp:Label ID="lblid" runat="server" Font-Size="5px" Visible="False"></asp:Label>
                        <asp:Label ID="lbltitle" runat="server" Font-Size="5px" Visible="False"></asp:Label>
                    </div>
            </div>
            <div class="panel panel-success">
                <div class="panel-heading">
                     <h5 class="page-title" id="pagetitle" runat="server" style="color:#1BA691">JOB TEST INVITATION</h5>
                </div>
             <div class="panel-body">  
            <div class="row">
          <div class="search-box-wrapper col-sm-6 col-md-3 col-xs-12 form-group pull-right">                        
                    <input id="txtfind" style="width:100%" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                    <button onserverclick="Button1_Click" id="btnFind" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                </div>
                 <div class="col-md-3 col-sm-6 col-xs-12 form-group pull-right">                                  
                    <button id="btnBack" type="button" runat="server" class="fa fa-backward btn btn-default btn-sm"
                        onserverclick="btnBack_Click" data-toggle="tooltip" data-original-title="Back" style="margin-right:10px;margin-left:10px;height:35px"></button>
                        <asp:LinkButton ID="btnDelete" data-toggle="tooltip" data-original-title="Delete" Height="35px" runat="server" CssClass="btn btn-default btn-sm" OnClick="Delete" OnClientClick="Confirm()">
                            <span style="margin-top:5px" aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                        </asp:LinkButton>
                         <button id="btnAdd" type="button" data-toggle="tooltip" data-original-title="Add" runat="server" class="glyphicon glyphicon-plus btn btn-default btn-sm" onserverclick="btnAdd_Click"
                        style="height: 35px;margin-right:10px;margin-left:10px;"></button> 
                </div>
<%--            <table class="style21">
                <tr>
                    <td class="style28">
                        <telerik:RadTextBox ID="txtfind" Runat="server" DisplayText="Search" 
                            Width="200px" Font-Names="Verdana" Font-Size="11px" ForeColor="#666666">
                        </telerik:RadTextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnFind" runat="server" Text="Search" BackColor="#1BA691" ForeColor="White"
                            Width="80px" Height="20px" BorderStyle="None" Font-Names="Verdana" 
                            Font-Size="11px" />
                    </td>
                    <td>
                        
                    </td>
                    <td>
                        <asp:Button ID="btnAdd" runat="server" Text="Add Test Invitation" 
                            BackColor="#1BA691" ForeColor="White"
                            Width="150px" Height="20px" BorderStyle="None" Font-Names="Verdana" 
                            Font-Size="11px" />
                    </td>
                    <td>
                        <asp:Button ID="btnDelete" runat="server" Text="Delete Invitation" 
                            OnClick="Delete" OnClientClick="Confirm()"
                            BackColor="#FF3300" ForeColor="White" Width="150px" Height="20px" 
                            BorderStyle="None" Font-Names="Verdana" Font-Size="11px" />
                    </td>
                    <td>
                        <asp:Button ID="btnBack" runat="server" Text="&lt;&lt; Back"
                            BackColor="#CC9900" ForeColor="White" Width="150px" Height="20px" 
                            BorderStyle="None" Font-Names="Verdana" Font-Size="11px" />                        
                    </td>
                </tr>               
            </table>--%>
            </div>
            
        <div class="row">
            <div>
                <asp:GridView ID="GridVwHeaderChckbox" runat="server" OnSorting="SortRecords" AllowSorting="True"
                    BorderStyle="Solid" Font-Names="Verdana" AllowPaging="True" PageSize="40" DataKeyNames="id" EmptyDataText="No data to display"
                    OnRowDataBound="OnRowDataBound" Width="100%" Height="50px" ToolTip="click row to view detail" CssClass="table table-condensed"
                    Font-Size="12px" AutoGenerateColumns="False" GridLines="Vertical" ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC" ShowHeaderWhenEmpty="True">
                    <RowStyle BackColor="white" />
                    <Columns>
                        <asp:TemplateField ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Center">
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="CheckAllEmp(this);" />
                            </HeaderTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemTemplate>
                                <asp:CheckBox ID="chkEmp" runat="server"></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Rows" ItemStyle-Width="5%" HeaderText="Rows" />                                                
                        <asp:TemplateField HeaderText="Job Test" 
                            ItemStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Left" SortExpression="TestTitle">
                            <ItemTemplate>
                                <a href="JobTestInviteUpdate.aspx?id=<%# Eval("id") %>">
                                    <%# Eval("TestTitle")%></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="testdate" HeaderText="Test Date" ItemStyle-HorizontalAlign="Center" SortExpression="testdate" />
                         <asp:BoundField DataField="testtime" HeaderText="Time Time" ItemStyle-HorizontalAlign="Center" SortExpression="testtime" />
                      
                                                                                                 
                        <asp:TemplateField HeaderText="Shortlisted"
                             ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Bold="true">
                            <ItemTemplate>
                                <a href="#" onclick='openInterview("<%# Eval("id") %>");'>
                                  Invites: <%# Eval("invites")%></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                                                                                                                        
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
                    function openDetail(code) {
                        window.location = "JobTestInviteUpdate.aspx?id=" + code;
                    }
                </script>  
                <script type="text/javascript">
                    function openInterview(code) {
                        window.open("PaperResult.aspx?id=" + code, "open_window", "width=1050,height=700");
                    }
                </script> 
            </div>
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
