<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="ConfirmationReport.aspx.vb"
    Inherits="GOSHRM.ConfirmationReport" EnableEventValidation="false" Debug="true" %>

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
           width: 164px;
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
                            <h5><b id="pagetitle" runat="server">CONFIRMATION REPORT</b></h5>
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
                        <div class=" col-md-4">
                            <div class="form-group">
                                <label>DEPT/OFFICE</label>
                                <telerik:RadComboBox ID="cboDept" Skin="Bootstrap" runat="server" Width="100%" 
                                    Font-Names="Verdana" Font-Size="11px" ForeColor="#666666">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                         <div class=" col-md-4">
                            <div class="form-group">
                                <label>DATE CRITERIA</label>
                                <telerik:RadComboBox ID="cboDateCriteria" Skin="Bootstrap" runat="server" Width="100%"   Font-Size="12px" ForeColor="#666666"
                                     AutoPostBack="True">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                         <div class="col-md-4 form-group">
                                <label>CONFIRMATION DATE</label>
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
                         <div class="col-md-4 m-t-20">
                            <asp:Button ID="btnSend" CssClass="btn btn-success" runat="server" Text="Display" ForeColor="White"
                            Width="150px" Height="35px" BorderStyle="None" Font-Names="Verdana" Font-Size="11px" />
                             <asp:CheckBox runat="server" Text="Include Sub-Units" 
                            Font-Names="Verdana" ID="chkInclude" Font-Size="12px" Font-Bold="True" 
                            ForeColor="#666666"></asp:CheckBox>
                        </div>
                        </div>

        <div style="height: 10px">
        </div>
        <div class="row">
            <div>
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
                <script type="text/javascript">
                    function openWindow(code) {
                        window.open("JobPostingsUpdate.aspx?id=" + code, "open_window", "width=800,height=800");
                    }
                </script>
                <script type="text/javascript">
                    function openApplicants(code) {
                        window.open("Applicants.aspx?Jobid=" + code, "open_window", "width=1000,height=800");
                    }
                </script>
                <script type="text/javascript">
                    function openShortlists(code) {
                        window.open("ShortLists.aspx?Jobid=" + code, "open_window", "width=1000,height=800");
                    }
                </script>
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
        .style23
        {
            width: 275px;
        }
    </style>
</asp:Content>
