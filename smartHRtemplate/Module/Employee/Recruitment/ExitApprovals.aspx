<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="ExitApprovals.aspx.vb"
    Inherits="GOSHRM.ExitApprovals" EnableEventValidation="false" Debug="true" %>
     <%@ register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
    <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
    
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   

    
    <html xmlns="http://www.w3.org/1999/xhtml">
  
  <head>
    <title></title>
     <link rel="stylesheet" href="../../../AdminLTE/bootstrap/css/bootstrap.min.css"/>
     <%--<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">--%>
    <link rel="stylesheet" href="../../../AdminLTE/dist/css/Admin-lte.min.css"/>
    <link rel="stylesheet" href="../../../AdminLTE/plugins/ionicons/css/ionicons.min.css"/>
    <link rel="stylesheet" href="../../../Skins/_all-skins.min.css"/>
    <link rel="stylesheet" href="../../../css/font-awesome.min.css"/>
  </head>
    
    <body>
        <form id="form1">
        <div class="container col-md-12">
        <div id="divalert" runat="server" visible="false" class="alert alert-info">
                <strong id="msgalert" runat="server">Danger!</strong>
            </div>
        <div class="panel panel-success">
                <div class="panel-heading">
                    <h5><b id="pagetitle" runat="server"></b></h5>
                </div>
             <div class="panel-body">
               
                            <div>
                                <div>
                                    <%--<table >
                                        <tr>
                                            <td class="style30">
                                                <asp:Label ID="Label1" runat="server" Text="Exit From" Font-Size="12px" 
                                                    Font-Bold="True" Font-Names="Verdana" ForeColor="#666666"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadDatePicker ID="dateFrom" runat="server" Font-Names="Verdana" Font-Size="12px"
                                                    Height="22px" Width="100%" RenderMode="Lightweight" Culture="en-US" 
                                                    ResolvedRenderMode="Classic" ForeColor="#666666">
                                                    <Calendar EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;" UseColumnHeadersAsSelectors="False"
                                                        UseRowHeadersAsSelectors="False" RenderMode="Lightweight">
                                                    </Calendar>
                                                    <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" Height="22px" LabelWidth="40%"
                                                        RenderMode="Lightweight" Width="">
                                                        <EmptyMessageStyle Resize="None" />
                                                        <ReadOnlyStyle Resize="None" />
                                                        <FocusedStyle Resize="None" />
                                                        <DisabledStyle Resize="None" />
                                                        <InvalidStyle Resize="None" />
                                                        <HoveredStyle Resize="None" />
                                                        <EnabledStyle Resize="None" />
                                                    </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" CssClass="" />
                                                </telerik:RadDatePicker>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label2" runat="server" Font-Size="12px" Text="To" 
                                                    Font-Bold="True" Font-Names="Verdana" ForeColor="#666666"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadDatePicker ID="dateTo" runat="server" Font-Names="Verdana" Font-Size="12px"
                                                    Height="22px" Width="100%" Culture="en-US" ResolvedRenderMode="Classic" 
                                                    ForeColor="#666666">
                                                    <Calendar EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;" UseColumnHeadersAsSelectors="False"
                                                        UseRowHeadersAsSelectors="False">
                                                    </Calendar>
                                                    <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" Height="22px" LabelWidth="40%"
                                                        Width="">
                                                        <EmptyMessageStyle Resize="None" />
                                                        <ReadOnlyStyle Resize="None" />
                                                        <FocusedStyle Resize="None" />
                                                        <DisabledStyle Resize="None" />
                                                        <InvalidStyle Resize="None" />
                                                        <HoveredStyle Resize="None" />
                                                        <EnabledStyle Resize="None" />
                                                    </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" CssClass="" />
                                                </telerik:RadDatePicker>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label3" runat="server" Font-Size="12px" Text="Approval Stat" 
                                                    Font-Bold="True" Font-Names="Verdana" ForeColor="#666666"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="radStatus" runat="server" Font-Names="Verdana" Font-Size="12px"
                                                    ResolvedRenderMode="Classic" ForeColor="#666666">
                                                </telerik:RadComboBox>
                                            </td>
                                            <td>
                                            </td>
                                            <td class="style23">
                                            <asp:TextBox ID="txtsearch" runat="server" Height="20px" Width="251px" BorderColor="#CCCCCC"
                                                BorderWidth="1px" TextMode="Search" Font-Names="Verdana" Font-Size="11px" 
                                                ForeColor="#666666"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnFind" runat="server" BackColor="#1BA691" BorderStyle="None" ForeColor="White"
                                                Height="20px" Text="View" Width="100px" Font-Names="Verdana" 
                                                Font-Size="11px" />
                                        </td>
                                        </tr>
                                        </table>--%>
                                        
                         <div class="row col-md-12">                         
                             <div class="col-sm-6 col-md-3 col-xs-12">
                                 <telerik:RadDatePicker ID="dateFrom" runat="server" Skin="Bootstrap" Font-Names="Verdana" Font-Size="12px"
                                                     Width="100%" Culture="en-US" 
                                                    ResolvedRenderMode="Classic" ForeColor="#666666">
                                                    <Calendar EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;" UseColumnHeadersAsSelectors="False"
                                                        UseRowHeadersAsSelectors="False">
                                                    </Calendar>
                                                    <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" Height="22px" LabelWidth="40%"
                                                        Width="">
                                                        <EmptyMessageStyle Resize="None" />
                                                        <ReadOnlyStyle Resize="None" />
                                                        <FocusedStyle Resize="None" />
                                                        <DisabledStyle Resize="None" />
                                                        <InvalidStyle Resize="None" />
                                                        <HoveredStyle Resize="None" />
                                                        <EnabledStyle Resize="None" />
                                                    </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" CssClass="" />
                                                </telerik:RadDatePicker>
                            </div>
                             <div class="col-sm-6 col-md-3 col-xs-12">
                                 <telerik:RadDatePicker ID="dateTo" runat="server" Font-Names="Verdana" Font-Size="12px"
                                                    Skin="Bootstrap" Width="100%" Culture="en-US" ResolvedRenderMode="Classic" 
                                                    ForeColor="#666666">
                                                    <Calendar EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;" UseColumnHeadersAsSelectors="False"
                                                        UseRowHeadersAsSelectors="False">
                                                    </Calendar>
                                                    <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" Height="22px" LabelWidth="40%"
                                                        Width="">
                                                        <EmptyMessageStyle Resize="None" />
                                                        <ReadOnlyStyle Resize="None" />
                                                        <FocusedStyle Resize="None" />
                                                        <DisabledStyle Resize="None" />
                                                        <InvalidStyle Resize="None" />
                                                        <HoveredStyle Resize="None" />
                                                        <EnabledStyle Resize="None" />
                                                    </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" CssClass="" />
                                                </telerik:RadDatePicker>
                            </div>
                             <div class="col-sm-6 col-md-3 col-xs-12">  
                                <telerik:RadComboBox Skin="Bootstrap" ID="radStatus" runat="server" Font-Names="Verdana" Font-Size="12px"
                                                    ResolvedRenderMode="Classic" Width="100%" ForeColor="#666666">
                                                </telerik:RadComboBox>
                             </div>            
                             <div class="search-box-wrapper col-sm-6 col-md-3 col-xs-12 form-group">
                                <input id="txtsearch" style="width:100%" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                                <button onserverclick="Button1_Click" id="btnsearch" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                            </div>
                        </div>
                                </div>
         
                            </div>
                            <div>
                                <asp:GridView ID="GridVwHeaderChckbox" runat="server" AllowPaging="True" AllowSorting="True"
                                    BorderStyle="Solid" DataKeyNames="id" Font-Names="Verdana" Font-Size="12px"
                                    Height="50px" OnRowDataBound="OnRowDataBound" OnSorting="SortRecords" PageSize="40"
                                    ToolTip="click row to select record" Width="100%" AutoGenerateColumns="False" GridLines="Vertical" ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC"
                                    ShowHeaderWhenEmpty="True"  CssClass="table table-condensed" EmptyDataText="No data to display" >
                                    <RowStyle BackColor="white" />
                                    <Columns>
                                        
                                        <asp:BoundField DataField="Rows" HeaderText="Rows">
                                            <ItemStyle Width="5px" />
                                        </asp:BoundField>
                                        

                                         <asp:TemplateField HeaderText="Employee">
                                            <ItemTemplate>
                                                <a href='ExitApprovalUpdate.aspx?id=<%# Eval("id") %>' ><%# Eval("Employee")%></a>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="True" />
                                        </asp:TemplateField>
                                     
                                        <asp:BoundField DataField="NoticeDate" HeaderText="Notice Date">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TerminationDate" HeaderText="Exit Date">
                                        </asp:BoundField>                                      
                                        <asp:BoundField DataField="ExitType" HeaderText="Exit Type">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SupervisorApproval" HeaderText="My Approval">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="HRApproval" HeaderText="HR Approval">
                                        </asp:BoundField>  
                                        <asp:BoundField DataField="Approval2" HeaderText="Higher Approval">
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
                                        window.open("ExitApprovalUpdate.aspx?id=" + code, "open_window", "width=700,height=600");
                                    }
                                </script>
                            </div>
 </div></div> 
                    </div>
                    
                    </asp:Content>

