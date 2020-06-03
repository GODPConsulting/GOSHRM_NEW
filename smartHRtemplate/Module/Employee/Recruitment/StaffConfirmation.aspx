<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="StaffConfirmation.aspx.vb"
    Inherits="GOSHRM.StaffConfirmation" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
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
        {
            width: 275px;
            font-size: x-large;
            font-family: Candara;
        }
    </style>
    <body>
        <form id="form1">
        <%--<div>
            <table width="100%">
                <tr>
                    
                    <td >
                        <asp:Label ID="lblView" runat="server" Font-Names="Candara" Font-Size="Medium" 
                            Font-Bold="True" ForeColor="#FF9900"></asp:Label>
                    </td>                    
                </tr>
            </table>
            <table>
                <tr>
                    <td>
                        <telerik:RadComboBox ID="cboStatus" runat="server" Font-Names="Verdana" Font-Size="12px"
                                                    ResolvedRenderMode="Classic" Width="200px" 
                            AutoPostBack="True">
                            <Items>
                                <telerik:RadComboBoxItem runat="server" Text="Due" Value="Due" />
                                <telerik:RadComboBoxItem runat="server" Text="Not Due" Value="Not Due" />
                                <telerik:RadComboBoxItem runat="server" Text="Confirmed" Value="Confirmed" />
                                <telerik:RadComboBoxItem runat="server" Text="Terminate Employment" 
                                    Value="Terminate Employment" />
                                <telerik:RadComboBoxItem runat="server" Text="Extend Probation" 
                                    Value="Extend Probation" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                    <td >
                        <asp:TextBox ID="txtsearch" runat="server" Width="150px" Height="15px" BorderColor="#CCCCCC"
                            BorderWidth="1px" Font-Names="Verdana" Font-Size="11px" 
                            ForeColor="#666666"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnFind" runat="server" Text="View" BackColor="#1BA691" ForeColor="White"
                            Width="80px" Height="15px" BorderStyle="None" Font-Names="Verdana" 
                            Font-Size="11px" />
                    </td>
                    <td>
                    </td>
                     <td>
                        <asp:Button ID="btnAdd" runat="server" Text="Add" BackColor="#1BA691" ForeColor="White"
                            Width="80px" Height="15px" BorderStyle="None" 
                            Font-Names="Verdana" Font-Size="11px" />
                    </td>
                    <td class="style26">
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="Delete" OnClientClick="Confirm()"
                            BackColor="#FF3300" ForeColor="White" Width="80px" Height="15px" 
                            BorderStyle="None" Visible="False" Font-Names="Verdana" Font-Size="11px" />
                    </td>
                </tr>
            
            </table>
        </div>--%>
        <div class="container col-md-12">
            <div id="divalert" runat="server" visible="false" class="alert alert-info">
                <strong id="msgalert" runat="server">Danger!</strong>
            </div>
        <div class="panel panel-success">
                <div class="panel-heading">
                    <h5><b id="pagetitle" runat="server"></b></h5>
                </div>
             <div class="panel-body">
        
         <div class="row">
             <div class="col-sm-3 col-md-3 col-xs-6">
                <telerik:RadComboBox ID="cboStatus" Skin="Bootstrap" runat="server" Font-Names="Verdana" Font-Size="12px"
                      ResolvedRenderMode="Classic" Width="100%" AutoPostBack="True">
                            <Items>
                                <telerik:RadComboBoxItem runat="server" Text="Due" Value="Due" />
                                <telerik:RadComboBoxItem runat="server" Text="Not Due" Value="Not Due" />
                                <telerik:RadComboBoxItem runat="server" Text="Confirmed" Value="Confirmed" />
                                <telerik:RadComboBoxItem runat="server" Text="Terminate Employment" 
                                    Value="Terminate Employment" />
                                <telerik:RadComboBoxItem runat="server" Text="Extend Probation" 
                                    Value="Extend Probation" />
                            </Items>
                        </telerik:RadComboBox>
             </div>        
             <div class="search-box-wrapper col-sm-6 col-md-4 col-xs-12 form-group pull-right">
                         <button id="btAdd" type="button" data-toggle="tooltip" data-original-title="Add Employee Confirmation" runat="server" class="glyphicon glyphicon-plus btn btn-default btn-sm" onserverclick="btnAdd_Click"
                        style="margin-left:10px;margin-right:10px;height: 35px;"></button> 
                    <input id="txtsearch" style="width:100%" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                    <button onserverclick="Button1_Click" id="btsearch" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                </div>
                <div class="col-sm-6 col-md-3 col-xs-6">
                 <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="Delete" OnClientClick="Confirm()"
                             CssClass="btn btn-primary btn-danger" ForeColor="White" Width="100px" Height="15px" 
                            BorderStyle="None" Visible="False" Font-Names="Verdana" Font-Size="11px" />
                </div>      
            </div> 
                        
  
     
        <div style="height: 163px">
            <div>
                <asp:GridView ID="GridVwHeaderChckbox" runat="server" OnSorting="SortRecords" AllowSorting="True"
                    BorderStyle="Solid" Font-Names="Verdana" AllowPaging="True" PageSize="1000" DataKeyNames="id"
                    OnRowDataBound="OnRowDataBound" Width="100%" Height="30px" ToolTip="click row to view detail" CssClass="table table-condensed"
                    Font-Size="11px" AutoGenerateColumns="False" GridLines="Vertical" ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC" ShowHeaderWhenEmpty="True" EmptyDataText="No data to display" >
                    <RowStyle BackColor="WHITE" />
                    <Columns>
                         <asp:TemplateField ItemStyle-Width="1px" ItemStyle-HorizontalAlign="Center">
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkboxSelectAll" runat="server" Enabled="false" 
                                    onclick="CheckAllEmp(this);" />
                            </HeaderTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"  />
                            <ItemTemplate>
                                <asp:CheckBox ID="chkEmp" runat="server"></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="Row" ItemStyle-Width="5px" HeaderText="Row" />
                         <asp:TemplateField HeaderText="Employee" SortExpression="RecruitName" 
                            ItemStyle-Font-Bold="true">
                            <ItemTemplate>          
                                     <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/Employee/Recruitment/StaffConfirmationUpdate.aspx?id={0}&empid={1}",
                     HttpUtility.UrlEncode(Eval("id").ToString()),HttpUtility.UrlEncode(Eval("recruitid").ToString())) %>'
                                            Text='<%# Eval("RecruitName")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>                     
                        <asp:BoundField DataField="office" HeaderText="Office" SortExpression="office" />
                        <asp:BoundField DataField="datejoin" HeaderText="Date Join" SortExpression="datejoin" />
                        <asp:BoundField DataField="ExpectedConfirmDate" HeaderText="Expected Confirmation Date" SortExpression="ExpectedConfirmDate"  />
                        <asp:BoundField DataField="probation" HeaderText="Probation (Mth)" SortExpression="probation" ItemStyle-HorizontalAlign="Right"/>
                        <asp:BoundField DataField="recommendation" HeaderText="My Recommendation" SortExpression="recommendation"  />
                        <asp:BoundField DataField="hrrecommendation" HeaderText="HR Recommendation" SortExpression="hrrecommendation"  />
                        <asp:BoundField DataField="completestat" HeaderText="Completion Stat" SortExpression="completestat"  />
                        <asp:BoundField DataField="hrnotified" HeaderText="HR Notified" SortExpression="hrnotified" />                       
                         <asp:BoundField DataField="createdon" HeaderText="Date Posted" SortExpression="createdon" />
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

            </div>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
        </div>
        </div></div></div>
        </form>
    </body>
    </html>
</asp:Content>

