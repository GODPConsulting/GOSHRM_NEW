<%--<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="StaffRequisitionsUpdate.aspx.vb"
    Inherits="GOSHRM.StaffRequisitionsUpdate" EnableEventValidation="false" %>--%>

<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="StaffRequisitionsUpdate.aspx.vb"
    Inherits="GOSHRM.StaffRequisitionsUpdate" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title></title>
        <script type="text/javascript">
            function Complete() {
                var bugdet = <%=req %>;
                var reqpos = <%=bug %>;
                alert(bugdet);
                alert(reqpos);
                var confirm_value = document.createElement("INPUT");
                confirm_value.type = "hidden";
                confirm_value.name = "confirm_value";
                if (confirm("Mark as complete and send notification to HR?")) {
                    confirm_value.value = "Yes";
                } else {
                    confirm_value.value = "No";
                }
                document.forms[0].appendChild(confirm_value);
            }         
        </script>
    </head>
    <body>
        <form id="form1" action="">
        <div class="container col-md-12">
            <div class="row">
                <div class=" col-md-12">
                    <div id="divalert" runat="server" visible="false" class="alert alert-info">
                        <strong id="msgalert" runat="server"></strong>
                        <asp:TextBox ID="txtid" runat="server" Width="1px" Height="1px" Visible="False"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class=" col-md-12">
                    <div class="panel panel-success">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-8 col-md-offset-0">
                                    <h5 id="pagetitle" runat="server" class="page-title" style="color: #1BA691">
                                        Staff Requisition</h5>
                                </div>
                                <div class="col-xs-4 text-right m-b-30">
                                    <button id="approvallink" type="button" runat="server" class="btn-success "
                                        title="Manage approval selection" onserverclick="lnkApprovalStat_Click" style="height: 30px;
                                        width: 150px">
                                        Approval Status</button>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            COMPANY*</label>
                                        <telerik:radcombobox id="cbocompany" runat="server" forecolor="#666666" width="100%"
                                            autopostback="True" filter="Contains" rendermode="Lightweight" skin="Bootstrap">
                                        </telerik:radcombobox>
                                    </div>
                                </div>
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            OFFICE*</label>
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel6" UpdateMode="Always">
                                            <ContentTemplate>
                                                <telerik:radcombobox id="cbooffice" runat="server" forecolor="#666666" width="100%"
                                                    autopostback="True" filter="Contains" rendermode="Lightweight" skin="Bootstrap"
                                                    emptymessage="--Select Office --">
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
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            HEAD OF DEPARTMENT</label>
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Always">
                                            <ContentTemplate>
                                                <input id="ahod" runat="server" class="form-control" type="text" disabled="disabled" />
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cbooffice" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            HIRING MANAGER*</label>
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Always">
                                            <ContentTemplate>
                                                <telerik:radcombobox id="cboHiringManager" runat="server" forecolor="#666666" width="100%"
                                                    filter="Contains" rendermode="Lightweight" skin="Bootstrap" emptymessage="-- Select Hiring Manager --">
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
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>
                                            REQUISITION TYPE</label>
                                        <telerik:radcombobox id="cborequisition" runat="server" forecolor="#666666" width="100%"
                                            rendermode="Lightweight" skin="Bootstrap" autopostback="True" emptymessage="-- Select Requisition Type --">
                                        </telerik:radcombobox>
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel19" UpdateMode="Always">
                                            <ContentTemplate>
                                                <label id="lbrequisition" runat="server">
                                                </label>
                                                <textarea id="areason" runat="server" class="form-control" rows="5" cols="1" placeholder="Brief explanation for job requisition"></textarea>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cborequisition" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            JOB TITLE*</label>
                                        <telerik:radcombobox id="cbojobtitle" runat="server" forecolor="#666666" width="100%"
                                            filter="Contains" skin="Bootstrap" autopostback="true">
                                        </telerik:radcombobox>
                                    </div>
                                </div>
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            JOB GRADE*</label>
                                        <telerik:radcombobox id="cbograde" runat="server" filter="Contains" forecolor="#666666"
                                            rendermode="Lightweight" skin="Bootstrap" width="100%" autopostback="true">
                                        </telerik:radcombobox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            JOB TYPE*</label>
                                        <telerik:radcombobox id="cbojobtype" runat="server" forecolor="#666666" width="100%"
                                            filter="Contains" skin="Bootstrap" autopostback="true">
                                        </telerik:radcombobox>
                                    </div>
                                </div>
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            LASTEST RESUMPTION DATE*</label>
                                        <telerik:raddatepicker id="datLastResumption" runat="server" mindate="" width="100%"
                                            autopostback="True" forecolor="#666666" skin="Bootstrap">
                                            <calendar userowheadersasselectors="False" usecolumnheadersasselectors="False" enableweekends="True"
                                                fastnavigationnexttext="&amp;lt;&amp;lt;" skin="Bootstrap">
                        </calendar>
                                            <dateinput displaydateformat="dd/MM/yyyy" dateformat="dd/MM/yyyy" labelwidth="40%"
                                                autopostback="True">
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
                            </div>
                            <div class="row">
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            MINIMUM EDUCATION REQUIRED*</label>
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel7" UpdateMode="Always">
                                            <ContentTemplate>
                                                <telerik:radcombobox id="cboeducation" runat="server" forecolor="#666666" width="100%"
                                                    filter="Contains" rendermode="Lightweight" skin="Bootstrap">
                                                </telerik:radcombobox>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cbograde" EventName="SelectedIndexChanged" />
                                                <asp:AsyncPostBackTrigger ControlID="cbojobtype" EventName="SelectedIndexChanged" />
                                                <asp:AsyncPostBackTrigger ControlID="datLastResumption" EventName="SelectedDateChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            EXPERIENCE LEVEL*</label>
                                        <telerik:radcombobox id="cboexperience" runat="server" forecolor="#666666" width="100%"
                                            filter="Contains" rendermode="Lightweight" skin="Bootstrap">
                                        </telerik:radcombobox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>
                                            MINIMUM YEARS OF EXPERIENCE</label>
                                        <input id="aminexpyr" runat="server" class="form-control" type="text" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>
                                            MAXIMUM YEARS OF EXPERIENCE</label>
                                        <input id="amaxexpyr" runat="server" class="form-control" type="text" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>
                                            MINIMUM AGE</label>
                                        <input id="aminage" runat="server" class="form-control" type="text" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>
                                            MAXIMUM AGE</label>
                                        <input id="amaxage" runat="server" class="form-control" type="text" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            AREA OF SPECILISATION*</label>
                                        <telerik:radcombobox id="cbospecialisation" runat="server" forecolor="#666666" width="100%"
                                            filter="Contains" rendermode="Lightweight" skin="Bootstrap">
                                        </telerik:radcombobox>
                                    </div>
                                </div>
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            BUDGETED POSITION*</label>
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel9" UpdateMode="Always">
                                            <ContentTemplate>
                                                <input id="abudgetposition" runat="server" readonly="" class="form-control" type="text" />
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cbooffice" EventName="SelectedIndexChanged" />
                                                <asp:AsyncPostBackTrigger ControlID="cbograde" EventName="SelectedIndexChanged" />
                                                <asp:AsyncPostBackTrigger ControlID="cbojobtype" EventName="SelectedIndexChanged" />
                                                <asp:AsyncPostBackTrigger ControlID="datLastResumption" EventName="SelectedDateChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            REQUIRED NUMBER OF POSITION*</label>
                                        <input id="areqposition" runat="server" class="form-control" type="text" />
                                        <b><span id="reqbug" style="color: Red;" runat="server"></span></b>
                                    </div>
                                </div>
                                <script type="text/javascript">
  
                                </script>
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            FILLED POSITION*</label>
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel8" UpdateMode="Always">
                                            <ContentTemplate>
                                                <input id="afilledposition" readonly="" runat="server" class="form-control" type="text" />
                                                
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cbooffice" EventName="SelectedIndexChanged" />
                                                <asp:AsyncPostBackTrigger ControlID="cbograde" EventName="SelectedIndexChanged" />
                                                <asp:AsyncPostBackTrigger ControlID="cbojobtype" EventName="SelectedIndexChanged" />
                                                <asp:AsyncPostBackTrigger ControlID="datLastResumption" EventName="SelectedDateChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>
                                            JOB DESCRIPTION*</label>
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel31" UpdateMode="Always">
                                            <ContentTemplate>
                                                <textarea id="ajobdesc" runat="server" class="form-control" rows="5" cols="1"></textarea>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cbojobtitle" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>
                                            SKILLS*</label>
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel10" UpdateMode="Always">
                                            <ContentTemplate>
                                                <textarea id="askills" runat="server" class="form-control" rows="5" cols="1"></textarea>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cbojobtitle" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>
                                            LOCATION</label>
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel17" UpdateMode="Always">
                                            <ContentTemplate>
                                                <telerik:radcombobox id="cbolocation" runat="server" forecolor="#666666" width="100%"
                                                    filter="Contains" rendermode="Lightweight" skin="Bootstrap" autopostback="True"
                                                    checkboxes="True">
                                                </telerik:radcombobox>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cbooffice" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel23" UpdateMode="Always">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtLocation" runat="server" Width="100%" Height="75px" ForeColor="#666666"
                                                    TextMode="MultiLine" BorderColor="#CCCCCC" BorderWidth="1px" Font-Names="Verdana"
                                                    Font-Size="12px" ReadOnly="True"></asp:TextBox>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cbolocation" EventName="ItemChecked" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12 m-t-20">
                                    <button id="btnupdate" runat="server" onserverclick="btnAdd_Click" type="submit"
                                        style="width: 150px" class="btn btn-primary btn-success ">
                                        Save &amp; Update</button>
                                    <asp:Button ID="btComplete" runat="server" Text="Complete" OnClientClick="Complete()"
                                        Width="150px" Height="34px" CssClass="btn btn-default " ToolTip="mark requisition as complete to forward to Department Head for approval" />
                                    <button id="Button1" runat="server" onserverclick="btnCancel_Click" type="submit"
                                        style="width: 150px" class="btn btn-primary btn-danger ">
                                        << Back</button>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12 text-right">
                                    <div class="form-group">
                                        <label>
                                            <i id="createdon" runat="server" style="font-size: 12px"></i>
                                        </label>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            <i id="updatedon" runat="server" style="font-size: 12px"></i>
                                        </label>
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
