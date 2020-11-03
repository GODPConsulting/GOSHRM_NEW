<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="PerformanceReport.aspx.vb"
    Inherits="GOSHRM.PerformanceReport" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <title></title>
    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
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
        .style24
        {
            width: 131px;
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
                            <h5><b id="pagetitle" runat="server">PERFORMANCE REPORT</b></h5>
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
                               <asp:UpdatePanel runat="server" ID="UpdatePanel" UpdateMode="Always">
                                    <ContentTemplate>
                                         <telerik:RadComboBox ID="cboDepts" runat="server" Width="100%" Skin="Bootstrap" 
                                        Font-Names="Verdana" Font-Size="11px" ForeColor="#666666" 
                                        AutoPostBack="True" Filter="Contains" CheckBoxes="False" 
                                        EnableCheckAllItemsCheckBox="False">
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
                                <label>REVIEW PERIOD</label>
                                <telerik:RadComboBox ID="cboReviewCycle" Skin="Bootstrap" runat="server" Width="100%"   Font-Size="12px" ForeColor="#666666"
                                     AutoPostBack="True">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class=" col-md-4">
                            <div class="form-group">
                                <label>JOB GRADE</label>
                                 <telerik:RadComboBox ID="cboJobGrade" runat="server" Width="100%" Skin="Bootstrap" 
                                    Font-Names="Verdana" Font-Size="11px" CheckBoxes="True" 
                                    EnableCheckAllItemsCheckBox="True" ForeColor="#666666" Filter="Contains" 
                                    AutoPostBack="True">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class=" col-md-4">
                            <div class="form-group">
                                <label>EMPLOYEES</label>
                                 <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Always">
                                    <ContentTemplate>
                                            <telerik:RadComboBox ID="cboEmployee" runat="server" Width="100%" Skin="Bootstrap" 
                                        Font-Names="Verdana" Font-Size="11px" 
                                        ForeColor="#666666" CheckBoxes="True" EnableCheckAllItemsCheckBox="True">
                                    </telerik:RadComboBox>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="cboCompany" EventName="SelectedIndexChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="cboDepts" EventName="ItemChecked" />
                                        <asp:AsyncPostBackTrigger ControlID="cboJobGrade" EventName="ItemChecked" />
                                       <%-- <asp:AsyncPostBackTrigger ControlID="cboDepts" EventName="CheckAllCheck" />--%>
                                        <asp:AsyncPostBackTrigger ControlID="cboJobGrade" EventName="CheckAllCheck" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>REPORT COLUMNS</label>
                                 <telerik:RadComboBox ID="cboColumns" runat="server" Width="100%" Skin="Bootstrap"
                                    Font-Names="Verdana" Font-Size="11px" CheckBoxes="True" 
                                    ForeColor="#666666" Filter="Contains">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class="col-md-12 m-t-5 text-left">
                            <asp:Button ID="btnSend" CssClass="btn btn-success" runat="server" Text="Display" ForeColor="White"
                            Width="150px" Height="35px" BorderStyle="None" Font-Names="Verdana" Font-Size="11px" />
                        </div>
              </div>
       
           <div style="height: 10px">
        </div>
        <div class="row col-md-12">
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
