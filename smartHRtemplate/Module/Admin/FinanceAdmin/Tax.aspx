<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="Tax.aspx.vb"
    Inherits="GOSHRM.Tax" EnableEventValidation="false" Debug="true" %>

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
        
        
        
        .style35
        {
            width: 517px;
        }
        .style37
        {
        }
        .style39
        {
            width: 170px;
        }
    .RadComboBox_Default{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}
    .RadComboBox{text-align:left;display:inline-block;vertical-align:middle;white-space:nowrap;*display:inline;*zoom:1}.RadComboBox_Default .rcbReadOnly .rcbInput{color:#333}.RadComboBox .rcbReadOnly .rcbInput{cursor:default}.RadComboBox_Default .rcbInput{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif;line-height:16px}
    .RadComboBox .rcbInput{margin:0;padding:2px 0 1px;height:auto;width:100%;border-width:0;outline:0;color:inherit;background-color:transparent;vertical-align:top}
    </style>
    <body>
        <form id="form1" action="">
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
           <div class="search-box-wrapper col-sm-6 col-md-4 col-xs-12 form-group pull-right">
                        <asp:LinkButton ID="btnDelete" data-toggle="tooltip" data-original-title="Delete" Height="35px" runat="server" CssClass="btn btn-default btn-sm" OnClick="btnDelete_Click" OnClientClick="Confirm()">
                            <span style="margin-top:5px" aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                        </asp:LinkButton>
                         <button id="btAdd" type="button" data-toggle="tooltip" data-original-title="Add" runat="server" class="glyphicon glyphicon-plus btn btn-default btn-sm" onserverclick="btnAdd_Click"
                        style="margin-left:10px;margin-right:10px;height: 35px;"></button> 
                    <input id="search" style="width:100%" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                    <button onserverclick="btnFind_Click" id="btnSearch" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                </div>
            </div>            

        <div class="row">
            <div class="table-responsive">
                <asp:GridView ID="GridVwHeaderChckbox" runat="server" OnSorting="SortRecords" AllowSorting="True"
                    BorderStyle="Solid" Font-Names="Verdana" AllowPaging="True" PageSize="200" DataKeyNames="id"
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
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            <ItemTemplate>
                                <asp:CheckBox ID="chkEmp" runat="server"></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Rows" ItemStyle-Width="5%" HeaderText="Rows" ItemStyle-VerticalAlign="Top" SortExpression="rows" />
                        <asp:TemplateField HeaderText="Company" ItemStyle-VerticalAlign="Top"
                            ItemStyle-HorizontalAlign="Left" ItemStyle-Font-Bold="true" SortExpression="company">
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/Admin/FinanceAdmin/Taxsetup.aspx?id={0}",
                     HttpUtility.UrlEncode(Eval("id").ToString())) %>' Text='<%# Eval("company")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="TaxDescription" HeaderText="Tax Description"
                            SortExpression="TaxDescription" />
                        <asp:BoundField DataField="ExemptedTaxAmount" HeaderText="Tax Exemption"
                            DataFormatString="{0:n}" ItemStyle-HorizontalAlign="Right" SortExpression="ExemptedTaxAmount" />
                        <asp:BoundField DataField="incomerelief" HeaderText="Gross Income Relief (%)"
                            DataFormatString="{0:n}" ItemStyle-HorizontalAlign="Right" SortExpression="incomerelief" />
                        <asp:BoundField DataField="TaxRangeSet" HeaderText="Tax Range Set"
                            SortExpression="TaxRangeSet" />
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
       </div>
        <%--<table width="100%">
            <tr>
                <td style="width: 20%">
                </td>
                <td style="width: 60%">
                    <table width="100%">
                        <tr>
                            <td class="style37" colspan="2">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblView" runat="server" Font-Names="Candara" Font-Size="Medium" Width="100%"
                                                Style="font-weight: 700; color: #FF6600"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <div>
                                    <div>
                                        <table width="50%">
                                            <tr>
                                                <td class="style22">
                                                    <asp:TextBox ID="txtsearch" runat="server" Width="251px" Height="20px" BorderColor="#CCCCCC"
                                                        BorderWidth="1px" Font-Names="Verdana" Font-Size="11px" ForeColor="#666666"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnFind" runat="server" Text="Search" BackColor="#1BA691" ForeColor="White"
                                                        Width="80px" Height="20px" BorderStyle="None" Font-Names="Verdana" Font-Size="11px" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnAdd" runat="server" Text="Add" BackColor="#1BA691" ForeColor="White"
                                                        Width="80px" Height="20px" BorderStyle="None" Font-Names="Verdana" Font-Size="11px" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="Delete" OnClientClick="Confirm()"
                                                        BackColor="#FF3300" ForeColor="White" Width="80px" Height="20px" BorderStyle="None"
                                                        Font-Names="Verdana" Font-Size="11px" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnExport" runat="server" Text="Export" BackColor="#FF9933" ForeColor="White"
                                                        Width="100px" Height="20px" BorderStyle="None" Font-Names="Verdana" Font-Size="11px"
                                                        Visible="False" />
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:FileUpload ID="FileUpload1" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                        Visible="False" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnUpload" runat="server" BackColor="#00CC99" BorderStyle="None"
                                                        ForeColor="White" Height="20px" Text="Upload File" ToolTip="CSV File: Job Title, Job Description, Skills, Skill Description"
                                                        Width="100px" Font-Names="Verdana" Font-Size="11px" Visible="False" />
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                                <asp:GridView ID="GridVwHeaderChckbox" runat="server" OnSorting="SortRecords" AllowSorting="True"
                                    BorderStyle="Solid" Font-Names="Verdana" AllowPaging="True" PageSize="30" DataKeyNames="id"
                                    OnRowDataBound="OnRowDataBound" Width="100%" Height="50px" ToolTip="click row to select record"
                                    Font-Size="12px" AutoGenerateColumns="False" GridLines="Vertical" ShowHeaderWhenEmpty="True"
                                    EmptyDataText="No data to display" ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC">
                                    <AlternatingRowStyle BackColor="#CCCCCC" />
                                    <Columns>
                                        <asp:TemplateField ItemStyle-Width="1px">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="CheckAllEmp(this);" />
                                            </HeaderTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkEmp" runat="server"></asp:CheckBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Rows" ItemStyle-Width="2px" HeaderText="Rows" ItemStyle-VerticalAlign="Top" />
                                        <asp:TemplateField HeaderText="Company" ItemStyle-Width="150px" ItemStyle-VerticalAlign="Top"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Font-Bold="true" SortExpression="company">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/Admin/FinanceAdmin/Taxsetup.aspx?id={0}",
                     HttpUtility.UrlEncode(Eval("id").ToString())) %>' Text='<%# Eval("company")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="TaxDescription" ItemStyle-Width="200px" HeaderText="Tax Description"
                                            SortExpression="TaxDescription" />
                                        <asp:BoundField DataField="ExemptedTaxAmount" ItemStyle-Width="20px" HeaderText="Tax Exemption"
                                            DataFormatString="{0:n}" ItemStyle-HorizontalAlign="Right" SortExpression="ExemptedTaxAmount" />
                                        <asp:BoundField DataField="incomerelief" ItemStyle-Width="20px" HeaderText="Gross Income Relief (%)"
                                            DataFormatString="{0:n}" ItemStyle-HorizontalAlign="Right" SortExpression="incomerelief" />
                                        <asp:BoundField DataField="TaxRangeSet" ItemStyle-Width="50px" HeaderText="Tax Range Set"
                                            SortExpression="TaxRangeSet" />
                                    </Columns>
                                    <HeaderStyle BackColor="#1BA691" ForeColor="White" HorizontalAlign="center" />
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
                                        window.open("TaxSetupUpdate.aspx?id=" + code, "open_window", "width=600,height=400");
                                    }
                                </script>
                            </td>
                        </tr>
                        <tr>
                            <td class="style39">
                                <asp:Button ID="Button1" runat="server" Text="Add" BackColor="White" ForeColor="White"
                                    Width="10px" Height="20px" BorderStyle="None" Font-Names="Verdana" Font-Size="12px" />
                            </td>
                            <td class="style35">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 20%">
                </td>
            </tr>
        </table>--%>
        </form>
    </body>
    </html>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
</asp:Content>
