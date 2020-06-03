<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="false"
    CodeBehind="LoanRequest.aspx.vb" Inherits="GOSHRM.LoanRequest" EnableEventValidation="false" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI.Gantt" TagPrefix="cc1" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title></title>
        <script type="text/javascript">

            function closeWin() {
                popup.close();   // Closes the new window
            }
   

        </script>
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        <script type="text/javascript">
            function ShowProgress() {
                setTimeout(function () {
                    var modal = $('<div />');
                    modal.addClass("modal");
                    $('body').append(modal);
                    var loading = $(".loading");
                    loading.show();
                    var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                    var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                    loading.css({ top: top, left: left });
                }, 200);
            }
            $('form').live("submit", function () {
                ShowProgress();
            });
        </script>
    </head>
    <body>
        <form id="form1">
        <div class="container">
            <div class="row">
                <div class="col-md-8">
                    <div id="divalert" runat="server" visible="false" class="alert alert-info">
                        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <strong
                            id="msgalert" runat="server"></strong>
                        <asp:Label ID="lblLocation" runat="server" Font-Size="1px" Visible="false"></asp:Label>
                        <asp:TextBox ID="txtapproverlevel" runat="server" Font-Size="1px" Visible="False"></asp:TextBox>
                        <asp:Label ID="lblGradeApprover1" runat="server" Font-Size="1px" Visible="False"></asp:Label>
                        <asp:TextBox ID="txtid" runat="server" Font-Size="1px" Visible="False"></asp:TextBox>
                        <asp:Label ID="lblEmpID" runat="server" Font-Size="1px" Visible="False"></asp:Label>
                        <asp:Label ID="lblGradeLevel" runat="server" Font-Size="1px" Visible="False"></asp:Label>
                        <asp:Label ID="lblMarketRate" runat="server" Font-Size="1px" Visible="False"></asp:Label>
                        <asp:Label ID="lblstatustemp" runat="server" Font-Size="X-Small" Text="Label" ForeColor="White"
                            Visible="False"></asp:Label>
                        <asp:Label ID="lblstatustemp2" runat="server" Font-Size="X-Small" Text="Label" ForeColor="White"
                            Visible="False"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class=" col-md-8">
                    <div class="panel panel-success">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-xs-8">
                                    <h5 id="pagetitle" runat="server" class="page-title" style="color: #1BA691">
                                        My Loan
                                    </h5>
                                </div>
                            </div>
                            <div id="divloanref" runat="server" class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>
                                            LOAN REFERENCE</label>
                                        <input id="aloanref" runat="server" class="form-control" type="text" disabled="disabled" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>
                                            LOAN</label>
                                        <input id="aloantype" runat="server" class="form-control" type="text" disabled="disabled" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>
                                            LOAN DATE</label>
                                        <telerik:raddatepicker runat="server" mindate="1900-01-01" forecolor="#666666" culture="en-US"
                                            rendermode="Lightweight" width="100%" resolvedrendermode="Classic" id="datDate" Skin="Bootstrap">
                                            <calendar enableweekends="True" fastnavigationnexttext="&amp;lt;&amp;lt;" rendermode="Lightweight" Skin="Bootstrap"
                                                 usecolumnheadersasselectors="False" userowheadersasselectors="False">
                                </calendar>
                                            <dateinput Skin="Bootstrap" dateformat="dd/MM/yyyy" displaydateformat="dd/MM/yyyy" labelwidth="40%"
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
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>
                                            LOAN REQUISITION METHOD
                                        </label>
                                        <telerik:radcombobox runat="server" resolvedrendermode="Classic" forecolor="#666666"
                                            id="cborequisition" width="100%" tooltip="repayment computation method" rendermode="Lightweight"
                                            skin="Bootstrap" emptymessage="-- Select --" autopostback="True">
                                        </telerik:radcombobox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>
                                            LOAN AMOUNT</label>
                                        <telerik:radtextbox id="aloanamt" runat="server" emptymessage="Loan amount" skin="Bootstrap"
                                            width="100%" Height="35px" autopostback="True">
                                        </telerik:radtextbox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>
                                            REPAYMENT START DATE</label>
                                        <telerik:raddatepicker runat="server" mindate="1900-01-01" forecolor="#666666" culture="en-US"
                                            rendermode="Auto" width="100%" resolvedrendermode="Auto" 
                                            id="arepaystart" Skin="Bootstrap">
                                            <calendar enableweekends="True" fastnavigationnexttext="&amp;lt;&amp;lt;" rendermode="Auto"
                                                usecolumnheadersasselectors="False" userowheadersasselectors="False" 
                                                Skin="Bootstrap">
                                            </calendar>
                                            <dateinput dateformat="dd/MM/yyyy" displaydateformat="dd/MM/yyyy" labelwidth="40%"
                                                rendermode="Auto">
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
                            </div>
                            <asp:UpdatePanel runat="server" ID="UpdatePanel10" UpdateMode="Always">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label>
                                                    MONTHLY REPAYMENT</label>
                                                <telerik:radtextbox id="aloanrepayamt" runat="server" emptymessage="Monthly repayment amount"
                                                    skin="Bootstrap" width="100%" autopostback="True">
                                                </telerik:radtextbox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label>
                                                    TENOR</label>
                                                <telerik:radtextbox id="aloantenor" runat="server" emptymessage="Tenor" skin="Bootstrap"
                                                    width="100%" autopostback="True">
                                                </telerik:radtextbox>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="cborequisition" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="aloanamt" EventName="TextChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="aloantenor" EventName="TextChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="aloanrepayamt" EventName="TextChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>
                                            ANNUAL INTEREST RATE (%)</label>
                                        <input id="aloanintrate" runat="server" class="form-control" type="text" placeholder="Annual interest rate"
                                            disabled="disabled" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>
                                            PURPOSE FOR REQUEST</label>
                                        <textarea id="areason" runat="server" class="form-control" rows="4" cols="1" placeholder="Purpose for loan"></textarea>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>
                                            FORWARD TO</label>
                                        <telerik:radcombobox runat="server" resolvedrendermode="Classic" forecolor="#666666"
                                            id="cboApprover1" filter="Contains" width="100%" tooltip="Manager / Supervisor to approve"
                                            rendermode="Lightweight" skin="Bootstrap" emptymessage="-- Select --">
                                        </telerik:radcombobox>
                                    </div>
                                </div>
                            </div>
                            <div id="divguarantor" runat="server" class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>
                                            GUARANTOR</label>
                                        <telerik:radcombobox runat="server" resolvedrendermode="Classic" forecolor="#666666"
                                            id="cboGuarantor" filter="Contains" width="100%" rendermode="Lightweight" skin="Bootstrap"
                                            emptymessage="-- Select --">
                                        </telerik:radcombobox>
                                    </div>
                                </div>
                            </div>
                            <div id="divsupapproval" runat="server" class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>
                                            APPROVAL</label>
                                        <input id="aapproval" runat="server" class="form-control" type="text" disabled="disabled" />
                                    </div>
                                </div>
                            </div>
                            <div id="divguarantorstat" runat="server" class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>
                                            GUARANTOR APPROVAL</label>
                                        <input id="aguarantorstat" runat="server" class="form-control" type="text" disabled="disabled" />
                                    </div>
                                </div>
                            </div>
                            <div id="divguarantorcomment" runat="server" class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>
                                            GUARANTOR COMMENT</label>
                                        <textarea id="aguarantorcomment" runat="server" class="form-control" rows="4" cols="1"
                                            readonly="readonly"></textarea>
                                    </div>
                                </div>
                            </div>
                            <div id="divfinapproval" runat="server" class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>
                                            FINANCE UNIT APPROVAL</label>
                                        <input id="afinapproval" runat="server" class="form-control" type="text" disabled="disabled" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12 m-t-20">
                                    <button id="btnupdate" runat="server" onserverclick="btnAdd_Click" type="submit"
                                        style="width: 150px" class="btn btn-primary btn-success">
                                        Save &amp; Update</button>
                                    <button id="btclose" runat="server" onserverclick="btnCancel_Click" type="submit"
                                        style="width: 150px" class="btn btn-primary btn-info">
                                        << Back</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        </form>
<%--        <div class="loading" align="center">
            Processing, please wait...<br />
            <br />
            <img src="../../../images/loaders.gif" alt="" />
        </div>--%>
    </body>
    </html>
</asp:Content>
