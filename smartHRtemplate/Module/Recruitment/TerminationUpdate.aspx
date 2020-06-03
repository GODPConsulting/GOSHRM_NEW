<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="false"
    CodeBehind="TerminationUpdate.aspx.vb" Inherits="GOSHRM.TerminationUpdate" EnableEventValidation="false" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title></title>
        <script type="text/javascript">

            function closeWin() {
                popup.close();   // Closes the new window
            }
   

        </script>
    </head>
    <body>
        <form id="form1">
        <div class="container">
            <div class="row">
                <div class="col-md-10">
                    <div id="divalert" runat="server" visible="false" class="alert alert-info">
                        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <strong
                            id="msgalert" runat="server">Danger!</strong>
                        <asp:TextBox ID="txtid" runat="server" Width="1px" Visible="False"></asp:TextBox>
                        <asp:Label ID="lblcompany" runat="server" Font-Size="1px"></asp:Label>
                        <asp:Label ID="lblpath" runat="server" Text="Subject" Font-Bold="True" Font-Size="1px" Visible="False"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class=" col-md-10">
                    <div class="panel panel-success">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <h5 id="pagetitle" runat="server" class="page-title" style="color: #1BA691">
                                        Exit
                                    </h5>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <label>
                                            Employee</label>
                                        <telerik:radcombobox id="cboEmployee" runat="server" width="100%" forecolor="#666666"
                                            filter="Contains" autopostback="True" rendermode="Lightweight" skin="Bootstrap">
                                        </telerik:radcombobox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <label>
                                            Exit Type</label>
                                        <telerik:radcombobox id="cboExitType" runat="server" width="100%" forecolor="#666666"
                                            emptymessage="-- Select --" rendermode="Lightweight" skin="Bootstrap">
                                        </telerik:radcombobox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="control-label">
                                        Notice Date</label>
                                    <telerik:RadDatePicker ID="anoticedate" runat="server" Width="100%" Skin="Bootstrap"
                                        RenderMode="Lightweight" Font-Names="Verdana" ForeColor="#666666">
                                        <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True"
                                            FastNavigationNextText="&amp;lt;&amp;lt;" RenderMode="Lightweight">
                                        </Calendar>
                                        <DateInput DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" LabelWidth="100%"
                                            RenderMode="Lightweight">
                                            <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                            <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                                            <FocusedStyle Resize="None"></FocusedStyle>
                                            <DisabledStyle Resize="None"></DisabledStyle>
                                            <InvalidStyle Resize="None"></InvalidStyle>
                                            <HoveredStyle Resize="None"></HoveredStyle>
                                            <EnabledStyle Resize="None"></EnabledStyle>
                                        </DateInput>
                                        <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                    </telerik:RadDatePicker>
                                </div>
                            </div>
                                <div class="col-md-6">
                                <div class="form-group">
                                    <label class="control-label">
                                        Exit Date</label>
                                    <telerik:RadDatePicker ID="aexitdate" runat="server" Width="100%" Skin="Bootstrap"
                                        RenderMode="Lightweight" Font-Names="Verdana" ForeColor="#666666">
                                        <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True"
                                            FastNavigationNextText="&amp;lt;&amp;lt;" RenderMode="Lightweight">
                                        </Calendar>
                                        <DateInput DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" LabelWidth="100%"
                                            RenderMode="Lightweight">
                                            <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                            <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                                            <FocusedStyle Resize="None"></FocusedStyle>
                                            <DisabledStyle Resize="None"></DisabledStyle>
                                            <InvalidStyle Resize="None"></InvalidStyle>
                                            <HoveredStyle Resize="None"></HoveredStyle>
                                            <EnabledStyle Resize="None"></EnabledStyle>
                                        </DateInput>
                                        <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                    </telerik:RadDatePicker>
                                </div>
                            </div>
                            </div>
                            <%--<div class="row">
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <label>
                                            Notice Date</label>
                                        <input id="anoticedate" runat="server" readonly="" class="form-control datetimepicker" type="text"
                                            placeholder="Notice Date" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <label>
                                            Exit Date</label>
                                        <input id="aexitdate" runat="server" readonly="" class="form-control datetimepicker" type="text"
                                            placeholder="Notice Date" />
                                    </div>
                                </div>
                            </div>--%>
                             <div id="div1" runat ="server" class="row">
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <label>
                                            Exit Reason </label>
                                            <textarea id="areason" runat="server" class="form-control" rows="4" cols="1" ></textarea>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <label>
                                            Line Manager</label>
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Always">
                                            <ContentTemplate>
                                                <telerik:radcombobox id="cboManager" runat="server" width="100%" forecolor="#666666"
                                                    emptymessage="-- Select --" rendermode="Lightweight" skin="Bootstrap">
                                                </telerik:radcombobox>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cboEmployee" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                            <div id="divseniorapproval" runat ="server" class="row">
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <label>
                                            Approval </label> 
                                            <input id="aseniorapproval" runat="server" class="form-control" type="text" readonly="readonly" /></div>
                                    </div>
                                </div>
                            <div id="divseniorcomment" runat ="server" class="row">
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <label>
                                            Comment </label>
                                            <textarea id="aseniorcomment" runat="server" class="form-control" rows="4" cols="1" readonly="readonly"></textarea>
                                    </div>
                                </div>
                            </div>
                                <div class="row">
                                    <div class="col-md-8">
                                    <asp:CheckBox ID="chkHigherApproval2" runat="server" AutoPostBack="true" Text="Requires higher level approval (Higher Approval)"
                                         ForeColor="#666666" />
                                    </div>
                                </div>
                            <div id="divhigherapproval2" runat ="server" class="row">
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <label>
                                            Higher Approval</label>
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Always">
                                            <ContentTemplate>
                                                <telerik:radcombobox id="cboApproverII" runat="server" width="100%" forecolor="#666666"
                                                    emptymessage="-- Select --" rendermode="Lightweight" skin="Bootstrap" 
                                                    CheckBoxes="True" EnableCheckAllItemsCheckBox="True" Filter="Contains">
                                                </telerik:radcombobox>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cboEmployee" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                             <div id="divhigherapproval" runat ="server" class="row">
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <label>
                                            Approval </label> 
                                            <input id="ahigherapproval" runat="server" class="form-control" type="text" readonly="readonly" /></div>
                                    </div>
                                </div>
                            <div id="divhighercomment" runat ="server" class="row">
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <label>
                                            Comment </label>
                                            <textarea id="ahighercomment" runat="server" class="form-control" rows="4" cols="1" readonly="readonly"></textarea>
                                    </div>
                                </div>
                            </div>
                             <div class="row">
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <label>
                                            Human Resource Approval</label>
                                            <telerik:radcombobox id="cboHRApproval" runat="server" width="100%" forecolor="#666666"
                        emptymessage="-- Select --" rendermode="Lightweight" skin="Bootstrap">
                    </telerik:radcombobox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <label>
                                            Comment </label>
                                            <textarea id="acomment" runat="server" class="form-control" rows="4" cols="1"></textarea>
                                    </div>
                                </div>
                            </div>
                    <div class="row">
                        <div class=" col-md-12">
                            <div id="divterminalbenefit" runat ="server" class="pull-left">
                                <button id="Button1" type="button" runat="server" class="btn btn-default" 
                                    onserverclick="lnkTerminalBenefit_Click">
                                    Terminal Benefits</button>
                            </div>
                            <div id="divappletter" runat ="server" class="pull-right" visible="false">
                                <button id="lnkappletter" type="button" runat="server" class="btn btn-default"
                                     onserverclick="lnkLetter_Click">
                                    Acceptance of Resignation</button>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                                <div class="col-md-12 m-t-20">
                                    <button id="btnupdate" runat="server" onserverclick="btnAdd_Click" type="submit"
                                        style="width: 150px" class="btn btn-primary btn-success">
                                        Save &amp; Update</button>
                                    <button id="btnotifyapprover" runat="server" onserverclick="btnNotifyApprover_Click" type="submit"
                                        style="width: 150px" class="btn btn-default">
                                        Notify Approver
                                    </button>
                                    <button id="btnotify" runat="server" onserverclick="btnNotify_Click" type="submit"
                                        style="width: 150px" class="btn btn-default" title="generate promotion letter">
                                        Notify Employee
                                    </button>
                                    <button id="btclose" runat="server" onserverclick="btnCancel_Click" type="submit"
                                        style="width: 150px" class="btn btn-primary btn-danger">
                                        << Back</button>
                                </div>
                            </div>
                            </div></div>

                            
                        </div>
                    </div>
                </div>
           



        
        </form>
    </body>
    </html>
</asp:Content>
