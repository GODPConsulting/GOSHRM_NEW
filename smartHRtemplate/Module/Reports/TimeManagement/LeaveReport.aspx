<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="LeaveReport.aspx.vb"
    Inherits="GOSHRM.LeaveReport" EnableEventValidation="false" Debug="true" %>

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
                            <h5><b id="pagetitle" runat="server">LEAVE REPORT</b></h5>
                        </div>
                     <div class="panel-body">
                     <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>COMPANY</label>
                                 <telerik:RadComboBox ID="cboCompany" Skin="Bootstrap" runat="server" Width="100%" 
                                    Font-Size="11px" ForeColor="#666666" AutoPostBack="True" Filter="Contains">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class=" col-md-4">
                            <div class="form-group">
                                <label>DEPT/OFFICE</label>
                                    <telerik:RadComboBox ID="cboDept" runat="server" Width="100%" Skin="Bootstrap" 
                                        Font-Names="Verdana" Font-Size="11px" ForeColor="#666666" 
                                        AutoPostBack="True" Filter="Contains" CheckBoxes="False" 
                                        EnableCheckAllItemsCheckBox="False">
                                    </telerik:RadComboBox>
                                    <asp:Label ID="lblCalendar" runat="server" Font-Names="Verdana" 
                            Font-Size="11px" ForeColor="#666666" Font-Bold="True"></asp:Label>
                            </div>
                        </div>
                        <div class=" col-md-4">
                            <div class="form-group">
                                <label>LEAVE YEAR</label>
                                 <telerik:RadComboBox ID="cboYear" runat="server" Width="100%" Skin="Bootstrap" 
                                    Font-Names="Verdana" Font-Size="11px" AutoPostBack="True" 
                                    ForeColor="#666666">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class=" col-md-4">
                            <div class="form-group">
                                <label>DATES</label>
                                  <div class="row">
                                <div class="col-md-6 col-sm-6 col-xs-6">
                                    <telerik:RadDatePicker Skin="Bootstrap" runat="server"
                                         Width="100%" ID="radStart" AutoPostBack="True" 
                                         Font-Names="Verdana" Font-Size="11px">
                                    </telerik:RadDatePicker>
                                </div>
                                <div class="col-md-6 col-sm-6 col-xs-6">
                                     <telerik:RadDatePicker Skin="Bootstrap" runat="server"
                                       Width="100%" ID="radEnd" 
                                         Font-Names="Verdana" Font-Size="11px">
                                    </telerik:RadDatePicker>      
                                </div>
                                </div> 
                            </div>
                        </div>
                        <div class=" col-md-4">
                            <div class="form-group">
                                <label>QUERY BY</label>
                                 <telerik:RadComboBox ID="cboCriteria" runat="server" Width="100%" Skin="Bootstrap" 
                                     AutoPostBack="True" Font-Names="Verdana" Font-Size="11px" 
                                    ForeColor="#666666">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class=" col-md-4">
                            <div class="form-group">
                                <label>EMPLOYEE</label>
                                 <telerik:RadComboBox ID="cboValue" runat="server" Width="100%" Skin="Bootstrap" 
                                    Font-Names="Verdana" Font-Size="11px" CheckBoxes="True" 
                                    EnableCheckAllItemsCheckBox="True" ForeColor="#666666" Filter="Contains">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class="col-md-12 m-t-5 text-left">
                             <asp:Button ID="btnSend" CssClass="btn btn-success" runat="server" Text="Display" ForeColor="White"
                            Width="100px" Height="35px" BorderStyle="None" Font-Names="Verdana" Font-Size="11px" />
                        
                      <asp:CheckBox ID="chkIncude" runat="server" Font-Names="Verdana" 
                            Font-Size="11px" Text="Include Sub-Units" ForeColor="#666666" 
                            Font-Bold="True" AutoPostBack="True"/>
                        </div>
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
        .style21
        {
            width: 375px;
        }
        .style22
        {
        }
        .style23
        {
            width: 275px;
        }
    </style>
</asp:Content>
