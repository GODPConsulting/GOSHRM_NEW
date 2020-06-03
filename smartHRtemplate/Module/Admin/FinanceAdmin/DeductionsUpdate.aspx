<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="DeductionsUpdate.aspx.vb"
    Inherits="GOSHRM.DeductionsUpdate" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title></title>
    </head>
    <body>
        <form action="" id="form1">
        <div class="container">
            <div>
                <div class="row">
                    <div class="col-md-8">
                        <div id="divalert" runat="server" visible="false" class="alert alert-info">
                            <strong id="msgalert" runat="server"></strong>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class=" col-md-8">
                        <div class="panel panel-success">
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-md-12 col-md-offset-0">
                                        <h4 class="page-title" style="color: #1BA691">
                                            Payslip Deduction Item</h4>
                                        <form action="">
                                        <div class="row">
                                            <div class=" col-md-12">
                                                <div class="form-group">
                                                    <label>
                                                        Payslip Item</label>
                                                    <input id="payslipitem" runat="server" class="form-control" type="text" />
                                                </div>
                                            </div>
                                            <div class=" col-md-12">
                                                <div class="form-group">
                                                    <label>
                                                        Type</label>
                                                    <telerik:raddropdownlist id="radInputType" font-names="Verdana" forecolor="#666666"
                                                        font-size="12px" runat="server" defaultmessage="--Select--" autopostback="True"
                                                        skin="Bootstrap" width="100%">
                                                    </telerik:raddropdownlist>
                                                </div>
                                            </div>
                                            <div class=" col-md-12">
                                                <div class="form-group">
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel5" UpdateMode="Always">
                                                        <ContentTemplate>
                                                            <label id="lblComponents" runat="server">
                                                                Components</label>
                                                            <telerik:radcombobox id="radComponents" runat="server" checkboxes="True" filter="Contains"
                                                                autopostback="True" enablecheckallitemscheckbox="True" font-names="Verdana" forecolor="#666666"
                                                                font-size="12px" rendermode="Lightweight" width="100%" skin="Bootstrap">
                                                            </telerik:radcombobox>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="radInputType" EventName="SelectedIndexChanged" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Always">
                                                        <ContentTemplate>
                                                            <telerik:radlistbox id="lstComponents" runat="server" enabled="False" font-names="Verdana"
                                                                forecolor="#666666" font-size="12px" width="100%" skin="Bootstrap">
                                                            </telerik:radlistbox>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="radComponents" EventName="ItemChecked" />
                                                            <asp:AsyncPostBackTrigger ControlID="radComponents" EventName="CheckAllCheck" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <div class=" col-md-12">
                                                <div class="form-group">
                                                    <label>
                                                        Amount</label>
                                                    <input id="amount" runat="server" class="form-control" type="text" />
                                                </div>
                                            </div>
                                            <div class=" col-md-12">
                                                <div class="form-group">
                                                    <label>
                                                        Position</label>
                                                    <input id="position" runat="server" class="form-control" type="text" />
                                                </div>
                                            </div>
                                            <div class=" col-md-12">
                                                <div class="form-group">
                                                    <label>
                                                        Expiration Date</label>
                                                    <telerik:raddatepicker id="radenddate" runat="server" font-names="Verdana" forecolor="#666666"
                                                        font-size="12px" mindate="" tooltip="last pay date deduction will be effective"
                                                        maxdate="2100-12-31" skin="Bootstrap" width="100%">
                                                        <calendar userowheadersasselectors="False" usecolumnheadersasselectors="False" enableweekends="True"
                                                            fastnavigationnexttext="&amp;lt;&amp;lt;" skin="Bootstrap">
                                    </calendar>
                                                        <dateinput displaydateformat="dd/MM/yyyy" dateformat="dd/MM/yyyy" labelwidth="100%">
                                        <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                        <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                                        <FocusedStyle Resize="None"></FocusedStyle>
                                        <DisabledStyle Resize="None"></DisabledStyle>
                                        <InvalidStyle Resize="None"></InvalidStyle>
                                        <HoveredStyle Resize="None"></HoveredStyle>
                                        <EnabledStyle Resize="None"></EnabledStyle>
                                    </dateinput>
                                                        <datepopupbutton imageurl="" hoverimageurl=""></datepopupbutton>
                                                    </telerik:raddatepicker>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label>
                                                        Active</label>
                                                    <select id="isactive" runat="server" class="select form-control">
                                                        <option>No</option>
                                                        <option>Yes</option>
                                                    </select>
                                                </div>
                                            </div>
                                            <div class=" col-md-12">
                                                <div class="form-group">
                                                    <label>
                                                        Exempted Employees</label>
                                                    <telerik:radcombobox id="drpExemptions" runat="server" checkboxes="True" font-names="Verdana"
                                                        forecolor="#666666" font-size="12px" filter="Contains" autopostback="True" enablecheckallitemscheckbox="True"
                                                        rendermode="Lightweight" width="100%" skin="Bootstrap">
                                                    </telerik:radcombobox>
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel" UpdateMode="Always">
                                                        <ContentTemplate>
                                                            <telerik:radlistbox id="lstExemptions" runat="server" enabled="False" font-names="Verdana"
                                                                forecolor="#666666" font-size="12px" width="100%" skin="Bootstrap" emptymessage="No exempted employee">
                                                                <buttonsettings transferbuttons="All" />
                                                                <emptymessagetemplate>
                                                No exempted employee
                                            </emptymessagetemplate>
                                                            </telerik:radlistbox>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="drpExemptions" EventName="ItemChecked" />
                                                            <asp:AsyncPostBackTrigger ControlID="drpExemptions" EventName="CheckAllCheck" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <div class=" col-md-12">
                                                <div class="form-group">
                                                    <label>
                                                        Note</label>
                                                    <textarea id="note" runat="server" class="form-control" rows="5"></textarea>
                                                </div>
                                            </div>
                                            <div class="col-md-12 m-t-20 text-center">
                                                <button id="btnupdate" runat="server" onserverclick="btnAdd_Click" type="submit"
                                                    style="width: 150px" class="btn btn-primary btn-success">
                                                    Save &amp; Update</button>
                                                <button id="Button1" runat="server" onserverclick="btnCancel_Click" type="submit"
                                                    style="width: 150px" class="btn btn-primary btn-danger">
                                                    << Back</button>
                                            </div>
                                        </div>
                                        </form>
                                    </div>
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
