<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="AuditTrailManagement.aspx.vb"
    Inherits="GOSHRM.AuditTrailManagement" EnableEventValidation="false" Debug="true" %>

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
                            <h5><b id="pagetitle" runat="server"></b></h5>
                        </div>
                     <div class="panel-body">
                     <div class="row">
                        <div class="col-md-4 form-group">
                                <label>ACTION DATE</label>
                                <div class="row">
                                <div class="col-md-6 col-sm-6 col-xs-6">
                                     <telerik:RadDatePicker ID="datStart" Skin="Bootstrap" runat="server" Width="100%"  Font-Names="Verdana" 
                                         Font-Size="11px" ForeColor="#666666">
                                     </telerik:RadDatePicker>
                                     <asp:Label ID="lblDateRange" runat="server" Font-Names="Verdana" Font-Bold="True" Font-Size="12px" ForeColor="#666666"></asp:Label>
                                </div>
                                <div class="col-md-6 col-sm-6 col-xs-6">
                                    <telerik:RadDatePicker ID="datEnd" Font-Names="Verdana" 
                                     Font-Size="11px" Skin="Bootstrap" runat="server" Width="100%"  ForeColor="#666666">
                                 </telerik:RadDatePicker>
                                </div>
                                </div>                                        
                        </div>
                        <div class=" col-md-4">
                            <div class="form-group">
                                <label>ACTION TYPE</label>
                                 <telerik:RadComboBox ID="RadComboBox1" runat="server" Width="100%" Skin="Bootstrap"
                                    Font-Names="Verdana" Font-Size="11px" 
                                    ForeColor="#666666" Filter="Contains">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class=" col-md-4">
                            <div class="form-group">
                                <label>MODULES(Pages)</label>
                                 <telerik:RadComboBox ID="RadComboBox2" runat="server" Width="100%" Skin="Bootstrap"
                                    Font-Names="Verdana" Font-Size="11px" 
                                    ForeColor="#666666" Filter="Contains">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class="col-md-12 m-t-5 text-left">
                             <asp:Button ID="btnSend" CssClass="btn btn-success" runat="server" Text="Display" ForeColor="White"
                            Width="100px" Height="35px" BorderStyle="None" Font-Names="Verdana" Font-Size="11px" />
                        
                        <asp:CheckBox ID="chkIncude" runat="server" Font-Names="Verdana" 
                            Font-Size="11px" Text="Include Ex-Staff" ForeColor="#666666" 
                            Font-Bold="True"/>
                        </div>
                   </div>               
           <div style="height: 10px">
        </div>
        <div class="row">
            <div class="col-md-12">
                <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" Font-Names="Verdana"
                    Font-Size="8pt" InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana"
                    WaitMessageFont-Size="14pt" ProcessingMode="Remote">
                    <ServerReport ReportServerUrl="" />
                </rsweb:ReportViewer>
            </div>
        </div>
        </div></div></div>
        </form>
    </body>
    </html>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .style23
        {
            width: 275px;
        }
    </style>
</asp:Content>
