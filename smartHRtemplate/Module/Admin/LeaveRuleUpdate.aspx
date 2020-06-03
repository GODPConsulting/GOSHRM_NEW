<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="LeaveRuleUpdate.aspx.vb"
    Inherits="GOSHRM.LeaveRuleUpdate" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title></title>
    </head>
    <body>
        <form id="form1">
        <div class="container">
            <div class="row">
                <div class="col-md-10">
                    <div id="divalert" runat="server" visible="false" class="alert alert-info">
                        <strong id="msgalert" runat="server"></strong>
                        <asp:TextBox ID="txtid" runat="server" Width="3px" Font-Names="Candara" Height="2px"
                            Visible="False"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class=" col-md-10">
                    <div class="panel panel-success">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-8 col-md-offset-0">
                                    <h4 class="page-title">
                                        Leave Rule</h4>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>
                                            Name*</label>
                                        <input id="aname" runat="server" class="form-control" type="text" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>
                                            Leave Type*</label>
                                        <telerik:raddropdownlist id="radLeaveType" runat="server" forecolor="#666666" defaultmessage="-- Select --"
                                            width="100%" rendermode="Lightweight" skin="Bootstrap">
                                        </telerik:raddropdownlist>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>
                                            Job Type*</label>
                                        <telerik:raddropdownlist id="radEmpStatus" runat="server" forecolor="#666666" defaultmessage="-- Select --"
                                            height="16px" width="100%" tabindex="2" rendermode="Lightweight" skin="Bootstrap">
                                        </telerik:raddropdownlist>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>
                                            Leave Days Entitled*</label>
                                        <input id="leavePerYear" runat="server" class="form-control" type="text" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <label>
                                            Job Grade*</label>
                                        <telerik:radcombobox id="cboJobGrade" runat="server" resolvedrendermode="Classic"
                                            autopostback="True" checkboxes="True" filter="Contains" rendermode="Lightweight"
                                            width="100%" forecolor="#666666" enablecheckallitemscheckbox="True" skin="Bootstrap">
                                        </telerik:radcombobox>
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel" UpdateMode="Always">
                                            <ContentTemplate>
                                                <telerik:radlistbox id="lstMakeup" runat="server" forecolor="#666666" width="100%"
                                                    rendermode="Lightweight" skin="Bootstrap">
                                                </telerik:radlistbox>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cboJobGrade" EventName="ItemChecked" />
                                                <asp:AsyncPostBackTrigger ControlID="cboJobGrade" EventName="CheckAllCheck" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>
                                            Eligible Months of Service<span class="text-danger">*</span></label>
                                    </div>
                                </div>
                                <div class="col-md-5">
                                    <div class="form-group">
                                        <input id="minservice" runat="server" class="form-control" type="text" />
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label style="text-align: justify">
                                            To</label>
                                    </div>
                                </div>
                                <div class="col-md-5">
                                    <div class="form-group">
                                        <input id="maxservice" runat="server" class="form-control" type="text" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>
                                            Leave Accrued<span class="text-danger">*</span></label>
                                        <telerik:raddropdownlist id="radLeaveAccruedEnabled" runat="server" defaultmessage="-- Select --"
                                            height="16px" width="100%" forecolor="#666666" rendermode="Lightweight" skin="Bootstrap">
                                        </telerik:raddropdownlist>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>
                                            Leave Carried Forward<span class="text-danger">*</span></label>
                                        <telerik:raddropdownlist id="radLeaveCarriedEnabled" runat="server" defaultmessage="-- Select --"
                                            height="16px" width="100%" autopostback="True" forecolor="#666666" rendermode="Lightweight"
                                            skin="Bootstrap">
                                        </telerik:raddropdownlist>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Always">
                                    <ContentTemplate>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>
                                                    Percentage of Leave Days Carried Forward<span class="text-danger">*</span></label>
                                                <input id="txtPercent" runat="server" class="form-control" type="text" />
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>
                                                    Carried Forward Leave Period (Mths)<span class="text-danger">*</span></label>
                                                <telerik:raddropdownlist id="radAvailabilityPeriod" runat="server" defaultmessage="-- Select --"
                                                    height="16px" width="100%" forecolor="#666666" skin="Bootstrap" rendermode="Lightweight">
                                                </telerik:raddropdownlist>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="radLeaveCarriedEnabled" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                            <div class="row">
                                <div class="col-md-10 m-t-20">
                                    <button id="btnupdate" runat="server" onserverclick="btnAdd_Click" type="submit"
                                        style="width: 150px" class="btn btn-primary btn-success">
                                        Save &amp; Update</button>
                                    <button id="Button1" runat="server" onserverclick="btnCancel_Click" type="submit"
                                        style="width: 150px" class="btn btn-primary btn-danger">
                                        << Back</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        </form>
    </body>
    </html>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
</asp:Content>
