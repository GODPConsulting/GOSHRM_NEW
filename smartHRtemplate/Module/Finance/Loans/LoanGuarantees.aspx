<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="LoanGuarantees.aspx.vb"
    Inherits="GOSHRM.LoanGuarantees" EnableEventValidation="false" Debug="true" %>
     <%@ register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
    <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <html xmlns="http://www.w3.org/1999/xhtml">

    <title></title>
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
    <body>
        <form id="form1">
        <div></div>
           <div id="divalert" runat="server" visible="false" class="alert alert-info">
                <strong id="msgalert" runat="server">Danger!</strong>
            </div>
           <div class="panel panel-success">
                <div class="panel-heading">
                    <h5><b id="pagetitle" runat="server"></b></h5>
                </div>
             <div class="panel-body">
        <%--<div>
            <table class="style21">
                                        <tr>
                                            <td class="style30">
                                                <asp:Label ID="Label3" runat="server" Font-Size="Small" Text="Status"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:radcombobox ID="radStatus" runat="server" Font-Names="Candara" Font-Size="Small"
                                                    ResolvedRenderMode="Classic">
                                                    <Items>
                                                        <telerik:radcomboboxitem runat="server" Text="Pending Approval" Value="Pending Approval"
                                                            Owner="" />
                                                        <telerik:radcomboboxitem runat="server" Text="Cancelled" Value="Cancelled" 
                                                            Owner="" />
                                                        <telerik:radcomboboxitem runat="server" Text="Rejected" Value="Rejected" 
                                                            Owner="" />
                                                        <telerik:radcomboboxitem runat="server" Text="Taken" Value="Taken" Owner="" />
                                                    </Items>
                                                </telerik:radcombobox>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                <table class="style21">
                                    <tr>
                                        <td class="style23">
                                            <asp:TextBox ID="txtsearch" runat="server" Height="20px" Width="251px" BorderColor="#CCCCCC"
                                                BorderWidth="1px" TextMode="Search"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnFind" runat="server" BackColor="#1BA691" BorderStyle="None" ForeColor="White"
                                                Height="20px" Text="View" Width="120px" Font-Names="Verdana" 
                                                Font-Size="11px" />
                                        </td>
                                        <td>
                                            
                                <asp:Button ID="btnDelete" runat="server" Text="Delete" 
                                    OnClick="Delete" OnClientClick="Confirm()"
                                    BackColor="#1BA691" ForeColor="White" Width="100px" Height="20px" 
                                    BorderStyle="None" Font-Names="Verdana" 
                                    Font-Size="11px" />
                                
                                        </td>
                                    </tr>
                                </table>
                            </div>--%>
                            <div class="row">
                               <div class="search-box-wrapper col-sm-6 col-md-3 col-xs-12 form-group pull-right">
                                        <asp:LinkButton ID="btnDelete" data-toggle="tooltip" data-original-title="Delete" Height="35px" runat="server" CssClass="btn btn-default btn-sm" OnClick="Delete" OnClientClick="Confirm()">
                                            <span style="margin-top:5px" aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                                        </asp:LinkButton>
                                        <div style="width:10px"></div>
                                    <input id="txtsearch" style="width:100%" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                                    <button onserverclick="Button1_Click" id="btsearch" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                                </div>
                             <div class="col-sm-6 col-md-3 col-xs-12 pull-right">
                                <telerik:radcombobox ID="radStatus" Skin="Bootstrap" Width="100%" runat="server" Font-Names="Candara" Font-Size="12px"
                                                    ResolvedRenderMode="Classic">
                                                    <Items>
                                                        <telerik:radcomboboxitem runat="server" Text="Pending Approval" Value="Pending Approval"
                                                            Owner="" />
                                                        <telerik:radcomboboxitem runat="server" Text="Cancelled" Value="Cancelled" 
                                                            Owner="" />
                                                        <telerik:radcomboboxitem runat="server" Text="Rejected" Value="Rejected" 
                                                            Owner="" />
                                                        <telerik:radcomboboxitem runat="server" Text="Taken" Value="Taken" Owner="" />
                                                    </Items>
                                                </telerik:radcombobox>
                                </div>
                            </div>
                            <div>
                                <asp:GridView ID="GridVwHeaderChckbox" runat="server" AllowPaging="True" AllowSorting="True"
                                    BorderStyle="Solid" DataKeyNames="id" Font-Names="Verdana" Font-Size="11px"
                                     OnRowDataBound="OnRowDataBound" OnSorting="SortRecords" PageSize="500" EmptyDataText="No data to display"
                                    ToolTip="click row to select record" Width="100%" AutoGenerateColumns="False" GridLines="Vertical" ForeColor="#666666" BorderWidth="1px"
                                     BorderColor="#CCCCCC" ShowHeaderWhenEmpty="True" CssClass="table table-condensed">
                                    <RowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="CheckAllEmp(this);" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkEmp" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle VerticalAlign="Middle"/>
                                </asp:TemplateField>
                                        <asp:BoundField DataField="Rows" HeaderText="Rows">
                                            <ItemStyle Width="5px" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Ref No">
                                            <ItemTemplate>
                                                <a href="#" onclick='openWindow("<%# Eval("id") %>");'><%# Eval("LoanRefNo")%></a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="LoanType" HeaderText="Loan Type" SortExpression="LoanType" >
                                        </asp:BoundField>
                                         <asp:BoundField DataField="LoanDate" HeaderText="Loan Date" SortExpression="LoanDate">
                                        </asp:BoundField>
                                         <asp:BoundField DataField="Amount" HeaderText="Loan Amount">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Name" HeaderText="Borrower" SortExpression="Name">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Office" HeaderText="Office/Dept" SortExpression="Office">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="datecreated" HeaderText="Date Created" SortExpression="datecreated">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="datesigned" HeaderText="Date Signed" SortExpression="datesigned">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="approvalstatus" HeaderText="Approval Status" SortExpression="approvalstatus">
                                        </asp:BoundField>
                                    </Columns>
                                    <HeaderStyle BackColor="White" ForeColor="#1BA691" HorizontalAlign="Center" />
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
                                <script type="text/javascript">

                                    function openWindow(code) {
                                        window.open("LoanGuarantorSign.aspx?id=" + code, "open_window", "width=750,height=700");
                                    }
                                </script>
                            </div>
                            </div></div>
      
        </form>
    </body>
    </html>
</asp:Content>
