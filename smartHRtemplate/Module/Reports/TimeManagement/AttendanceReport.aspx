<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="AttendanceReport.aspx.vb"
    Inherits="GOSHRM.AttendanceReport" EnableEventValidation="false" Debug="true" %>

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
            width: 138px;
        }
         .style23
        {
            width: 300px;
        }
    </style>
    <body>
        <form id="form1">
          <div class="container col-md-12">
          <div class="panel panel-success">
                        <div class="panel-heading">
                            <h5><b id="pagetitle" runat="server">Car Loan</b></h5>
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
                                <asp:UpdatePanel runat="server" ID="UpdatePanel" UpdateMode="Always">
                                    <ContentTemplate>
                                         <telerik:RadComboBox ID="cboDept" runat="server" Width="100%" Skin="Bootstrap" 
                                        Font-Names="Verdana" Font-Size="11px" ForeColor="#666666" 
                                        AutoPostBack="True" Filter="Contains" CheckBoxes="True" 
                                        EnableCheckAllItemsCheckBox="True">
                                    </telerik:RadComboBox>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="cboCompany" EventName="SelectedIndexChanged" />

                                    </Triggers>
                                </asp:UpdatePanel>
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
                                <label>EMPLOYEES</label>
                                 <telerik:RadComboBox ID="cboValue" runat="server" Width="100%" Skin="Bootstrap" 
                                    Font-Names="Verdana" Font-Size="11px" CheckBoxes="True" 
                                    EnableCheckAllItemsCheckBox="True" ForeColor="#666666" Filter="Contains">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>DATE RANGE</label>
                                 <div class="row">
                                <div class="col-md-6 col-sm-6 col-xs-6">
                                    <telerik:RadDatePicker Skin="Bootstrap" runat="server"
                                         Width="100%" ID="datStart" AutoPostBack="True" 
                                         Font-Names="Verdana" Font-Size="11px">
                                    </telerik:RadDatePicker>
                                </div>
                                <div class="col-md-6 col-sm-6 col-xs-6">
                                     <telerik:RadDatePicker Skin="Bootstrap" runat="server"
                                       Width="100%" ID="datEnd" 
                                         Font-Names="Verdana" Font-Size="11px">
                                    </telerik:RadDatePicker>      
                                </div>
                                </div> 
                            </div>
                        </div>
                        <div class="col-md-12 m-t-5 text-left">
                             <asp:Button ID="btnSend" CssClass="btn btn-success" runat="server" Text="Display" ForeColor="White"
                            Width="100px" Height="35px" BorderStyle="None" Font-Names="Verdana" Font-Size="11px" />                        
                        <asp:CheckBox ID="chkIncudeSub" runat="server" Font-Names="Verdana" 
                            Font-Size="11px" Text="Include Sub-Units" ForeColor="#666666" 
                            Font-Bold="True" AutoPostBack="True"/>
                        </div>
                   </div>  
                  

       
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
        </div> </div> </div> </div>
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
