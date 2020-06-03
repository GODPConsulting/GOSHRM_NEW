<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="TrainingSession.aspx.vb"
    Inherits="GOSHRM.TrainingSession" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
   
    <title></title>
    <body>
        <form id="form1">
             <div class="container col-md-12">
          <div class="panel panel-success">
                        <div class="panel-heading">
                            <h5><b id="pagetitle" runat="server">TRAINING SESSION DETAIL REPORT</b></h5>
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
                                <label>COURSE</label>
                               <telerik:RadComboBox ID="cboCourse" Skin="Bootstrap" runat="server" Width="100%" 
                                    AutoPostBack="True" Filter="Contains" Font-Names="Verdana" 
                                    Font-Size="11px" ForeColor="#666666">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                         <div class=" col-md-4">
                            <div class="form-group">
                                <label>SESSION</label>
                                <telerik:RadComboBox ID="cboSession" Skin="Bootstrap" runat="server" Width="100%" 
                                    Height="200px" Font-Names="Verdana" Font-Size="11px" ForeColor="#666666">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class="col-md-12 m-t-5 text-left">
                            <asp:Button ID="btnSend" CssClass="btn btn-success" runat="server" Text="Display" ForeColor="White"
                            Width="150px" Height="35px" BorderStyle="None" Font-Names="Verdana" Font-Size="11px" />

                            <asp:CheckBox ID="chkTrainAssessment" runat="server" Font-Names="Verdana" 
                            Text="Include Training Assessment" Width="220px" Font-Bold="True" 
                            Font-Size="11px" ForeColor="#666666" />
         
                            <asp:CheckBox ID="chkLearnAssessment" runat="server" Font-Names="Verdana" 
                                Text="Include Learning Assessment" Width="250px" Font-Bold="True" 
                                Font-Size="11px" ForeColor="#666666" />
                            </div>
              </div>
        
        <div style="height: 10px">
        </div>
        <div>
            <div>
                <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>                
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" 
                    Font-Names="Verdana" Font-Size="8pt" InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana" 
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
