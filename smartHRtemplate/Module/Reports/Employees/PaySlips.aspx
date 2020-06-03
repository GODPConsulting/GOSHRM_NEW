<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="PaySlips.aspx.vb"
    Inherits="GOSHRM.PaySlips" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <title></title>
    <body>
        <form id="form1">
       <div class="content container-fluid">
               <div class="row col-md-8">
                    <div id="divalert" runat="server" visible="false" class="alert alert-info">
                    <strong id="msgalert" runat="server">Danger!</strong>
                    </div>
                </div>     
           <div class="panel panel-success">
                <div class="panel-heading">
                    <h5><b id="pagetitle" runat="server"></b></h5>
                </div>
             <div class="panel-body">
                <div class="row">
                    <div class="col-sm-3 col-md-3 col-xs-6">
                        <div class="form-group form-focus">
                            <telerik:RadComboBox runat="server" 
                                                 RenderMode="Lightweight" 
                                                ResolvedRenderMode="Classic" Width="100%" 
                            ID="cboMonth" skin="Bootstrap" Font-Names="Verdana" ForeColor="#666666">
                            </telerik:RadComboBox>
                        </div>
                    </div>

                    <div class="col-sm-3 col-md-3 col-xs-6">
                        <telerik:RadComboBox runat="server" 
                                                 RenderMode="Lightweight" 
                                                ResolvedRenderMode="Classic" Width="100%" 
                            ID="cboYear" skin="Bootstrap" Font-Names="Verdana" ForeColor="#666666">
                        </telerik:RadComboBox>
                    </div>

                    <div class="col-sm-3 col-md-2 col-xs-6">
                    <asp:Button ID="btnSend" runat="server" Text="Display" ForeColor="White"
                            Width="100%" Height="30px" BorderStyle="None" CssClass="btn btn-prmary btn-success" Font-Names="Verdana" Font-Size="11px" />
                    </div>        
                </div>
        </div>
       
        <div class="row">
            <div style="margin-left:20px;" class="col-md-12 col-sm-12 col-xs-12">
                <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>             
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" Font-Names="Verdana"
                    Font-Size="8pt" InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana"
                    WaitMessageFont-Size="14pt" ProcessingMode="Remote">
                    <ServerReport ReportServerUrl="" />
                </rsweb:ReportViewer>
            </div>
         </div>
         </div></div>
        </form>
    </body>
    </html>
</asp:Content>
