<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="EmployeeManagement.aspx.vb"
    Inherits="GOSHRM.EmployeeManagement" EnableEventValidation="false" Debug="true" %>

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
                        <div class=" col-md-4">
                            <div class="form-group">
                                <label>LOCATION</label>
                                 <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Always">
                                    <ContentTemplate>
                                         <telerik:RadComboBox ID="cboLocation" runat="server" Width="100%" Skin="Bootstrap" 
                                         AutoPostBack="True" Font-Names="Verdana" Font-Size="11px" 
                                        ForeColor="#666666" CheckBoxes="True" EnableCheckAllItemsCheckBox="True">
                                    </telerik:RadComboBox>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="cboCompany" EventName="SelectedIndexChanged" />

                                    </Triggers>
                                </asp:UpdatePanel>
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
                                        <asp:AsyncPostBackTrigger ControlID="cboDept" EventName="ItemChecked" />
                                        <asp:AsyncPostBackTrigger ControlID="cboJobGrade" EventName="ItemChecked" />
                                        <asp:AsyncPostBackTrigger ControlID="cboLocation" EventName="ItemChecked" />
                                        <asp:AsyncPostBackTrigger ControlID="cboDept" EventName="CheckAllCheck" />
                                        <asp:AsyncPostBackTrigger ControlID="cboJobGrade" EventName="CheckAllCheck" />
                                        <asp:AsyncPostBackTrigger ControlID="cboLocation" EventName="CheckAllCheck" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class=" col-md-4">
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
