<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="false"
    CodeBehind="PromotionsUpdate.aspx.vb" Inherits="GOSHRM.PromotionsUpdate" EnableEventValidation="false" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title></title>
       
    </head>
    <body>
        <form id="form1">
        <div class="container col-md-12">
            <div class="row">
                <div class="col-md-10">
                    <div id="divalert" runat="server" visible="false" class="alert alert-info">
                        <strong id="msgalert" runat="server">Danger!</strong>
                        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                            <span aria-hidden="true">&times;</span></button>
                        <asp:Label ID="lblID" runat="server" Visible="False" Font-Size="1px"></asp:Label>
                        <asp:Label ID="lblpath" runat="server" Font-Size="1px" Visible="False"></asp:Label>
                        <asp:TextBox ID="txtmthservice" runat="server" BorderColor="#CCCCCC" BorderWidth="1px"
                            Font-Size="1px" Enabled="False" Visible="False"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row col-md-10">
                <div class="col-md-8 col-md-offset-0" style="width: 100%">
                    <h5 id="pagetitle" runat="server" class="page-title" style="color: #1BA691">
                        Promotion
                    </h5>
                </div>
                <div class="col-xs-4 text-right m-b-30 pull-right">
                    <button id="approvallink" type="button" runat="server" class="btn btn-primary btn-success"
                        title="Manage approval selection" onserverclick="lnkApprovalStat_Click">
                        Approval Status</button>
                </div>
            </div>
            <div class="row">
                <div class=" col-md-10">
                    <div class="panel panel-success">
                        <div class="panel-body">
                            <div class="row">
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <label>
                                            Employee</label>
                                        <telerik:radcombobox id="cboEmployee" runat="server" forecolor="#666666" resolvedrendermode="Classic"
                                            filter="Contains" width="100%" autopostback="True" 
                                            rendermode="Lightweight" skin="Bootstrap" EmptyMessage="-- Select --">
                                        </telerik:radcombobox>
                                    </div>
                                </div>
                            </div>
                            <div id="divempcompany" runat="server" class="row">
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <label>
                                            Company</label>
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel12" UpdateMode="Always">
                                            <ContentTemplate>
                                                <input id="aempcompany" runat="server" class="form-control" type="text" readonly="readonly" />
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cboEmployee" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-12">
                                    <label>
                                        Office</label>
                                    <asp:UpdatePanel runat="server" ID="UpdatePanel13" UpdateMode="Always">
                                        <ContentTemplate>
                                            <input id="aempoffice" runat="server" class="form-control" type="text" readonly="readonly" />
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="cboEmployee" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-12">
                                    <label>
                                        Job Grade</label>
                                    <asp:UpdatePanel runat="server" ID="UpdatePanel10" UpdateMode="Always">
                                        <ContentTemplate>
                                            <input id="aempgrade" runat="server" class="form-control" type="text" readonly="readonly" />
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="cboEmployee" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-12">
                                    <label>
                                        Job Title</label>
                                    <asp:UpdatePanel runat="server" ID="UpdatePanel11" UpdateMode="Always">
                                        <ContentTemplate>
                                            <input id="aemptitle" runat="server" class="form-control" type="text" readonly="readonly" />
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="cboEmployee" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-12">
                                    <label>
                                        Months in Position</label>
                                    <asp:UpdatePanel runat="server" ID="UpdatePanel14" UpdateMode="Always">
                                        <ContentTemplate>
                                            <input id="aempmthposition" runat="server" class="form-control" type="text" readonly="readonly" />
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="cboEmployee" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-12">
                                    <label>
                                        Performance Rating</label>
                                    <asp:UpdatePanel runat="server" ID="UpdatePanel15" UpdateMode="Always">
                                        <ContentTemplate>
                                            <input id="aemprating" runat="server" class="form-control" type="text" readonly="readonly" />
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="cboEmployee" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <label>
                                            Initiated By
                                        </label>
                                        <telerik:radcombobox id="cboinitiator" runat="server" forecolor="#666666" resolvedrendermode="Classic"
                                            filter="Contains" width="100%" autopostback="True" rendermode="Lightweight" skin="Bootstrap">
                                        </telerik:radcombobox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-8 col-md-10" style="width: 100%">
                                    <h5 id="H1" runat="server" class="page-title" style="color: #1BA691">
                                        Promotion Detail
                                    </h5>
                                </div>
                            </div>
                            <div id="divcompany" runat="server" class="row">
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <label>
                                            Company</label>
                                        <telerik:radcombobox id="cboCompany" runat="server" forecolor="#666666" resolvedrendermode="Classic"
                                            filter="Contains" width="100%" autopostback="True" rendermode="Lightweight" skin="Bootstrap"
                                            emptymessage="-- Select Company --">
                                        </telerik:radcombobox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <label>
                                            Office</label>
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Always">
                                            <ContentTemplate>
                                                <telerik:radcombobox id="radoffice" runat="server" forecolor="#666666" resolvedrendermode="Classic"
                                                    filter="Contains" width="100%" autopostback="True" 
                                                    rendermode="Lightweight" skin="Bootstrap" EmptyMessage="-- Select Office --">
                                                </telerik:radcombobox>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cboCompany" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <label>
                                            Location</label>
                                        <telerik:radcombobox id="radlocation" runat="server" forecolor="#666666" resolvedrendermode="Classic"
                                            filter="Contains" width="100%" tooltip="location promoted employee will be located"
                                            rendermode="Lightweight" skin="Bootstrap" 
                                            EmptyMessage="-- Select location --">
                                        </telerik:radcombobox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <label>
                                            Head</label>
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel4" UpdateMode="Always">
                                            <ContentTemplate>
                                                <telerik:radcombobox id="radhod" runat="server" forecolor="#666666" resolvedrendermode="Classic"
                                                    filter="Contains" width="100%" skin="Bootstrap">
                                                </telerik:radcombobox>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="radOffice" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <label>
                                            Job Grade</label>
                                        <telerik:radcombobox id="radjobgrade" runat="server" forecolor="#666666" resolvedrendermode="Classic"
                                            filter="Contains" width="100%" tooltip="grade employee is promoted to" autopostback="True"
                                            rendermode="Lightweight" skin="Bootstrap">
                                        </telerik:radcombobox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <label>
                                            Job Title</label>
                                        <telerik:radcombobox id="radjobtitle" runat="server" forecolor="#666666" resolvedrendermode="Classic"
                                            filter="Contains" width="100%" tooltip="promotion job title" rendermode="Lightweight"
                                            skin="Bootstrap">
                                        </telerik:radcombobox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <label>
                                            Supervisor</label>
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel5" UpdateMode="Always">
                                            <ContentTemplate>
                                                <telerik:radcombobox id="radsup" runat="server" forecolor="#666666" resolvedrendermode="Classic"
                                                    filter="Contains" width="100%" tooltip="supervisor employee will report to" rendermode="Lightweight"
                                                    skin="Bootstrap">
                                                </telerik:radcombobox>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="radjobgrade" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <label>
                                            Date of Effect</label>
                                        <%--<input id="aeffdate" runat="server" class="form-control datetimepicker" type="text" /></div>--%>
                                        <telerik:raddatepicker runat="server" mindate="1900-01-01" forecolor="#666666" culture="en-US"
                                            rendermode="Lightweight" width="100%" resolvedrendermode="Classic" id="aeffdate"
                                            skin="Bootstrap">
                                            <calendar enableweekends="True" fastnavigationnexttext="&amp;lt;&amp;lt;" rendermode="Lightweight"
                                                skin="Bootstrap" usecolumnheadersasselectors="False" userowheadersasselectors="False">
                                            </calendar>
                                                        <dateinput dateformat="dd/MM/yyyy" displaydateformat="dd/MM/yyyy" labelwidth="40%"
                                                            rendermode="Lightweight">
                                            <emptymessagestyle resize="None">
                                            </emptymessagestyle>
                                            <readonlystyle resize="None">
                                            </readonlystyle>
                                            <focusedstyle resize="None">
                                            </focusedstyle>
                                            <disabledstyle resize="None">
                                            </disabledstyle>
                                            <invalidstyle resize="None">
                                            </invalidstyle>
                                            <hoveredstyle resize="None">
                                            </hoveredstyle>
                                            <enabledstyle resize="None">
                                            </enabledstyle>
                                            </dateinput>
                                                        <datepopupbutton cssclass="" imageurl="" hoverimageurl=""></datepopupbutton>
                                                    </telerik:raddatepicker>
                                </div>
                            </div>
                            <div id="divpromoletter" runat="server" class="row">
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <button id="btndownload" runat="server" onserverclick="lnkletter_Click" type="submit"
                                            class="btn btn-default btn-block ">
                                            <i class="fa fa-download"></i>Download Promotion Letter
                                        </button>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>
                                            Comment</label>
                                        <textarea id="acomment" runat="server" class="form-control" rows="4" cols="1"></textarea>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12 m-t-20">
                                    <button id="btnupdate" runat="server" onserverclick="btnsave_Click" type="submit"
                                        style="width: 150px" class="btn btn-primary btn-success ">
                                        Save &amp; Update</button>
                                    <button id="btletter" runat="server" onserverclick="btnNotify_Click" type="submit"
                                        style="width: 150px" class="btn btn-default " title="generate promotion letter">
                                        Generate Letter
                                    </button>
                                    <button id="btclose" runat="server" onserverclick="btnClose_Click" type="submit"
                                        style="width: 150px" class="btn btn-primary btn-danger ">
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
