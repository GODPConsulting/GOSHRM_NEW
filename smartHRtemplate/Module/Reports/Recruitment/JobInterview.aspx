<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="JobInterview.aspx.vb"
    Inherits="GOSHRM.JobInterview" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
   
    <title></title>
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
        </style>
    <body>
        <form id="form1">
 <div class="container col-md-12">    
       <div class="panel panel-success">
                        <div class="panel-heading">
                            <h5><b id="pagetitle" runat="server">INTERVIEWED CANDIDATES REPORT</b></h5>
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
                                <label>JOB POST</label>
                                <telerik:RadComboBox ID="cboJobPost" Skin="Bootstrap" runat="server" Width="100%" 
                                    Font-Names="Verdana" Font-Size="11px" ForeColor="#666666">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                         <div class=" col-md-4">
                            <div class="form-group">
                                <label>INTERVIEW DATE</label>
                                <div class="row">
                                <div class="col-md-6 col-sm-6 col-xs-6">
                                    <telerik:RadDatePicker Skin="Bootstrap" runat="server"
                                     Width="100%" ID="datStartDate" AutoPostBack="True" 
                                     Font-Names="Verdana" Font-Size="11px">
                                </telerik:RadDatePicker>
                                <asp:Label ID="lblDateRange" runat="server" Font-Names="Verdana" 
                            Font-Size="12px" Font-Bold="True" ForeColor="#666666"></asp:Label>
                                </div>
                                <div class="col-md-6 col-sm-6 col-xs-6">
                                     <telerik:RadDatePicker Skin="Bootstrap" runat="server"
                                       Width="100%" ID="datEndDate" 
                                         Font-Names="Verdana" Font-Size="11px">
                                    </telerik:RadDatePicker>      
                                </div>
                                </div>                 
                            </div>
                        </div>
                        <div class="col-md-12 m-t-5 text-left">
                            <asp:Button ID="btnSend" CssClass="btn btn-success" runat="server" Text="Display" ForeColor="White"
                            Width="150px" Height="35px" BorderStyle="None" Font-Names="Verdana" Font-Size="11px" />
                        </div>
              </div>

        <div style="height: 10px">
        </div>
        <div style="height: 163px">
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
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" 
                    Font-Names="Verdana" Font-Size="8pt" InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana" 
                    WaitMessageFont-Size="14pt">
                    <ServerReport ReportServerUrl="" />
                    <LocalReport ReportPath="Module\Recruitment\Reports\JobInterview.rdlc">
                        <DataSources>
                            <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="JobInterview" />
                        </DataSources>
                    </LocalReport>
                </rsweb:ReportViewer>
            </div>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:GOSHRMConnectionString %>" 
                SelectCommand="Recruit_Job_Interview_Report" 
                SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:ControlParameter ControlID="cboJobPost" Name="JobID" 
                        PropertyName="SelectedValue" Type="Int32" />                    
                    <asp:ControlParameter ControlID="datStartDate" DbType="Date" Name="startdate" 
                        PropertyName="SelectedDate" />
                    <asp:ControlParameter ControlID="datEndDate" DbType="Date" Name="enddate" 
                        PropertyName="SelectedDate" />
                </SelectParameters>
            </asp:SqlDataSource>
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
