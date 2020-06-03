<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="StaffLoan.aspx.vb"
    Inherits="GOSHRM.StaffLoan" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <title></title>
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
        .style24
        {
            width: 115px;
        }
        .style25
        {
            width: 390px;
            height: 12px;
        }
    </style>
    <body>
        <form id="form1">
          <div class="container col-md-12">
         <div class="panel panel-success">
                        <div class="panel-heading">
                            <h5><b id="pagetitle" runat="server">STAFF LOAN REPORT</b></h5>
                        </div>
                     <div class="panel-body">
            <div class="row">
                        <asp:RadioButtonList ID="rdoreport" Width="30%" runat="server" Font-Names="Verdana" Font-Size="11px"
                            RepeatDirection="Horizontal" Font-Bold="True" ForeColor="#666666">
                            <asp:ListItem Value="ifrs">Amortised Cost</asp:ListItem>
                            <asp:ListItem Value="gaap">Loan Report</asp:ListItem>
                        </asp:RadioButtonList>
            </div>
             <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>COMPANY</label>
                                 <telerik:RadComboBox ID="cboCompany" Skin="Bootstrap" runat="server" Width="100%" 
                                    Font-Size="11px" ForeColor="#666666" AutoPostBack="True" Filter="Contains">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>REPORT DATE</label>
                                <telerik:RadDatePicker ID="radReportDate" Skin="Bootstrap" Height="30px" runat="server" RenderMode="Lightweight"
                                    Width="100%" Font-Names="Verdana" Font-Size="11px" ForeColor="#666666">
                                    <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True"
                                        FastNavigationNextText="&amp;lt;&amp;lt;" RenderMode="Lightweight">
                                    </Calendar>
                                    <DateInput DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" LabelWidth="40%"
                                        RenderMode="Lightweight">
                                        <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                        <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                                        <FocusedStyle Resize="None"></FocusedStyle>
                                        <DisabledStyle Resize="None"></DisabledStyle>
                                        <InvalidStyle Resize="None"></InvalidStyle>
                                        <HoveredStyle Resize="None"></HoveredStyle>
                                        <EnabledStyle Resize="None"></EnabledStyle>
                                    </DateInput>
                                    <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                </telerik:RadDatePicker>
                             </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>QUERY BY</label>
                                <telerik:RadComboBox ID="cboCriteria" runat="server" Width="100%" Skin="Bootstrap" 
                                     AutoPostBack="True" Font-Names="Verdana" Font-Size="11px" 
                                    ForeColor="#666666">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>EMPLOYEE</label>
                               <telerik:RadComboBox ID="cboDept" runat="server" Width="100%" Skin="Bootstrap" 
                                    Font-Names="Verdana" Font-Size="11px" Filter="Contains" 
                                    ForeColor="#666666">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>LOAN</label>
                                 <telerik:RadComboBox ID="cboLoanType" runat="server" Width="100%" Skin="Bootstrap" 
                                    Font-Names="Verdana" Font-Size="11px" Filter="Contains" 
                                    ForeColor="#666666">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                        <div style="margin-top:30px;">
                         <asp:RadioButtonList ID="rdoStatus" runat="server" Width="100%" Font-Names="Verdana" Font-Size="11px"
                            RepeatDirection="Horizontal" Font-Bold="True" ForeColor="#666666">
                            <asp:ListItem>All</asp:ListItem>
                            <asp:ListItem>Active</asp:ListItem>
                            <asp:ListItem Value="LIQ">Liquidated</asp:ListItem>
                        </asp:RadioButtonList>
                        </div>                           
                        </div>
                        <div class="col-md-12 m-t-5 text-left">
                             <asp:Button ID="btnSend" CssClass="btn btn-success" runat="server" Text="Display" ForeColor="White"
                            Width="100px" Height="35px" BorderStyle="None" Font-Names="Verdana" Font-Size="11px" />                                               
                        </div>
                   </div>  
            <%--<table>
                <tr>
                    <td class="style24">
                        <asp:Label ID="Label5" runat="server" Font-Names="Verdana" 
                            Text="Company" Font-Bold="True" Font-Size="11px" ForeColor="#666666"></asp:Label>
                    </td>
                    <td class="style23">
                        <telerik:RadComboBox ID="cboCompany" runat="server" Width="400px" 
                            Font-Size="11px" ForeColor="#666666" AutoPostBack="True" Filter="Contains">
                        </telerik:RadComboBox>
                    </td>
                    <td>
                        
                        <asp:Label ID="Label1" runat="server" Font-Names="Verdana" Text="Report Date" 
                            Font-Size="11px" Font-Bold="True" ForeColor="#666666"></asp:Label>
                        
                    </td>
                    <td>
                        
                        <telerik:RadDatePicker ID="radReportDate" runat="server" RenderMode="Lightweight"
                            Width="150px" Font-Names="Verdana" Font-Size="11px" ForeColor="#666666">
                            <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True"
                                FastNavigationNextText="&amp;lt;&amp;lt;" RenderMode="Lightweight">
                            </Calendar>
                            <DateInput DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" LabelWidth="40%"
                                RenderMode="Lightweight">
                                <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                                <FocusedStyle Resize="None"></FocusedStyle>
                                <DisabledStyle Resize="None"></DisabledStyle>
                                <InvalidStyle Resize="None"></InvalidStyle>
                                <HoveredStyle Resize="None"></HoveredStyle>
                                <EnabledStyle Resize="None"></EnabledStyle>
                            </DateInput>
                            <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                        </telerik:RadDatePicker>
                        
                        <asp:Button ID="btnSend" runat="server" Text="Display" BackColor="#1BA691" ForeColor="White"
                            Width="100px" Height="20px" BorderStyle="None" Font-Names="Verdana" Font-Size="11px" />
                       
                    </td>
                </tr>
                <tr>
                    <td class="style24">
                        <asp:Label ID="Label4" runat="server" Font-Names="Verdana" Text="Query By" 
                            Font-Size="11px" Font-Bold="True" ForeColor="#666666"></asp:Label>
                    </td>
                    <td class="style23">
                        <telerik:RadComboBox ID="cboCriteria" runat="server" Width="300px" 
                             AutoPostBack="True" Font-Names="Verdana" Font-Size="11px" 
                            ForeColor="#666666">
                        </telerik:RadComboBox>
                    </td>
                    <td>
                        <asp:Label ID="lblQuery" runat="server" Font-Names="Verdana" Font-Size="11px" 
                            Font-Bold="True" ForeColor="#666666"></asp:Label>
                    </td>
                    <td>
                        
                        <telerik:RadComboBox ID="cboDept" runat="server" Width="400px" 
                            Font-Names="Verdana" Font-Size="11px" Filter="Contains" 
                            ForeColor="#666666">
                        </telerik:RadComboBox>
                        
                    </td>
                </tr>
                <tr>
                    <td class="style24">
                        <asp:Label ID="Label3" runat="server" Font-Names="Verdana" Text="Loans" 
                            Font-Size="11px" Font-Bold="True" ForeColor="#666666"></asp:Label>
                    </td>
                    <td class="style25">
                        <telerik:RadComboBox ID="cboLoanType" runat="server" Width="400px" 
                            Font-Names="Verdana" Font-Size="11px" Filter="Contains" 
                            ForeColor="#666666">
                        </telerik:RadComboBox>
                    </td>
                    <td>
                       
                        &nbsp;</td>
                    <td>
                        
                        <asp:RadioButtonList ID="rdoStatus" runat="server" Font-Names="Verdana" Font-Size="11px"
                            RepeatDirection="Horizontal" Font-Bold="True" ForeColor="#666666">
                            <asp:ListItem>All</asp:ListItem>
                            <asp:ListItem>Active</asp:ListItem>
                            <asp:ListItem Value="LIQ">Liquidated</asp:ListItem>
                        </asp:RadioButtonList>
                        
                    </td>
                </tr>
                </table>--%>

        <div style="height: 10px">
        </div>
        <div class="row">
            <div>
                <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>             
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" Font-Names="Verdana"
                    Font-Size="8pt" InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana"
                    WaitMessageFont-Size="14pt" ProcessingMode="Remote">
                    <ServerReport ReportServerUrl="" />
                </rsweb:ReportViewer>
            </div>
         </div></div></div></div>
        </form>
    </body>
    </html>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .style22
        {
        }
        .style23
        {
            width: 275px;
        }
    </style>
</asp:Content>
