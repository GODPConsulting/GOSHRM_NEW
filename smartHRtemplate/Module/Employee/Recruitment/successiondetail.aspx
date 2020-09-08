<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="false"
    CodeBehind="successiondetail.aspx.vb" Inherits="GOSHRM.successiondetail" EnableEventValidation="false" %>

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
                <div class=" col-md-8">
                    <div id="divalert" runat="server" visible="false" class="alert alert-info">
                        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <strong
                            id="msgalert" runat="server"></strong>
                        <asp:Label ID="lblid" runat="server" Font-Size="1px" Visible="False"></asp:Label>
                        <asp:Label ID="lblsuccessionid" runat="server" Font-Size="1px" Visible="False"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-8 col-md-offset-0">
                    <h5 id="pagetitle" runat="server" class="page-title" style="color: #1BA691">
                        Succession Plan Breakdown</h5>
                </div>
            </div>
            <div class="row">
                <div id="divuser" runat="server" class="col-sm-3 col-md-10 col-xs-6 pull-left">
                    <p>
                        <a href="#" runat="server" onserverclick="btnlist_Click"><u>Succession Plan</u></a>
                        <label>
                            >
                        </label>
                        <a id="A1" href="#" runat="server" onserverclick="btnCancel_Click"><u>Detail</u></a>
                        <label>
                            >
                        </label>
                        <a id="A2" href="#">Breakdown</a>
                    </p>
                </div>
            </div>
            <div class="row">
                <div class="col-md-8">
                    <div class="form-group">
                        <label>
                            KEY ACTION *</label>
                        <textarea id="akeyaction" runat="server" class="form-control" rows="4" cols="1" placeholder="Key Action"></textarea>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-8">
                    <div class="form-group">
                        <label>
                            RESPONSIBILITY BY *</label>
                        <telerik:radcombobox id="cboemployee" runat="server" checkboxes="True" forecolor="#666666"
                            filter="Contains" autopostback="True" rendermode="Lightweight"
                            width="100%" skin="Bootstrap" EmptyMessage="-- Select --">
                        </telerik:radcombobox>
                        <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Always">
                            <ContentTemplate>
                                <telerik:radlistbox id="lstemployees" runat="server" font-names="Verdana" forecolor="#666666"
                                    font-size="13px" width="100%" visible="True" rendermode="Lightweight" skin="Bootstrap">
                                </telerik:radlistbox>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="cboemployee" EventName="ItemChecked" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-8">
                    <div class="form-group">
                        <label>
                            STATUS *</label>
                        <telerik:radcombobox id="cbostatus" runat="server" forecolor="#666666" enablecheckallitemscheckbox="True"
                            rendermode="Lightweight" width="100%" skin="Bootstrap" emptymessage="-- Select Status --">
                        </telerik:radcombobox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class=" col-md-8">
                    <%--<div class="form-group">
                        <label>
                            START DATE *</label>
                        <div class="cal-icon">
                            <input id="astartdate" runat="server" class="form-control datetimepicker" type="text" /></div>
                    </div>--%>
                    <div class="form-group">
                                        <label>
                                            TARGET START DATE</label>
                                        <telerik:raddatepicker runat="server" mindate="1900-01-01" forecolor="#666666" culture="en-US"
                                            rendermode="Lightweight" width="100%" resolvedrendermode="Classic" id="astartdate"
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
            </div>
            <div class="row">
                <div class=" col-md-8">
                    <div class="form-group">
                                        <label>
                                           TARGET DUE DATE</label>
                                        <telerik:raddatepicker runat="server" mindate="1900-01-01" forecolor="#666666" culture="en-US"
                                            rendermode="Lightweight" width="100%" resolvedrendermode="Classic" id="aduedate"
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
            </div>
            <div class="row">
                <div class="col-md-8 m-t-20">
                    <button id="btnupdate" runat="server" onserverclick="btnAdd_Click" type="submit"
                        style="width: 150px" class="btn btn-primary btn-success">
                        Save &amp; Update</button>
                    <button id="Button1" runat="server" onserverclick="btnCancel_Click" type="submit"
                        style="width: 150px" class="btn btn-primary btn-info">
                        Close</button>
                </div>
            </div>
        </div>
        </form>
    </body>
    </html>
</asp:Content>
