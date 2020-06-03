<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="PayScenario.aspx.vb"
    Inherits="GOSHRM.PayScenario" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <title></title>
    <body>
        <form id="form1">
         <div class="container col-md-12">
         <div class="panel panel-success">
                        <div class="panel-heading">
                            <h5><b id="pagetitle" runat="server">STAFF LOAN REPORT</b></h5>
                        </div>
                     <div class="panel-body">
                      <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>COMPANY</label>
                                <telerik:RadComboBox ID="cboCompany" Skin="Bootstrap" runat="server" Width="100%" 
                                    Font-Size="12px" ForeColor="#666666" AutoPostBack="True" Filter="Contains">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>DEPT/OFFICE</label>
                               <telerik:RadComboBox ID="cboDept" Skin="Bootstrap" runat="server" Width="100%" 
                                    AutoPostBack="True" Filter="Contains" Font-Names="Verdana" 
                                    Font-Size="11px" ForeColor="#666666">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                         <div class="col-md-4">
                            <div class="form-group">
                                <label>JOB GRADE</label>
                                <telerik:RadComboBox ID="cboGrade" Skin="Bootstrap" runat="server" Width="100%" 
                                    Height="200px" Font-Names="Verdana" Font-Size="11px" ForeColor="#666666">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class="col-md-12 m-t-5 text-left">
                            <asp:Button ID="btnSend" CssClass="btn btn-success" runat="server" Text="Display" ForeColor="White"
                            Width="150px" Height="35px" BorderStyle="None" Font-Names="Verdana" Font-Size="11px" />
                            </div>
              </div>

        
        <div class="row">
                    <asp:GridView ID="GridRepay" runat="server" BorderStyle="Solid" Font-Names="Verdana" CssClass="table table-condensed"
                        Font-Size="12px" Height="50px" Width="600px" PageSize="30" AutoGenerateColumns="False" GridLines="Vertical" ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC"
                        ShowHeaderWhenEmpty="True" EmptyDataText="No data to display" Font-Bold="False">
                        <RowStyle BackColor="white" />
                        <Columns>
                            <asp:BoundField DataField="rows" ItemStyle-Width="5px" HeaderText="SNo" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="item" ItemStyle-Width="200px" HeaderText="Salary Item" />
                            <asp:TemplateField  HeaderText="Amount" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate >
                                    <asp:TextBox ID="txtAmount" Width="50px" Font-Names="Verdana"  Font-Size="12px" AutoPostBack="False" ForeColor="Gray"
                                        runat="server" Text='<%# Eval("Amount") %>' ></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>                           
                        </Columns>
                        <HeaderStyle BackColor="white" ForeColor="#1BA691" HorizontalAlign="center" />
                        <RowStyle HorizontalAlign="Left" />
                    </asp:GridView>
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
