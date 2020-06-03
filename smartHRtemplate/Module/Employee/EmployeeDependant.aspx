<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="false" CodeBehind="EmployeeDependant.aspx.vb" Inherits="GOSHRM.EmployeeDependantUpdate" EnableEventValidation="true" %>

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

        <script type="text/javascript">
            function ConfirmApprove() {
                var confirmapprove_value = document.createElement("INPUT");
                confirmapprove_value.type = "hidden";
                confirmapprove_value.name = "confirmapprove_value";
                if (confirm("Approve the update?")) {
                    confirmapprove_value.value = "Yes";
                } else {
                    confirmapprove_value.value = "No";
                }
                document.forms[0].appendChild(confirmapprove_value);
            }
        </script>
        <script type="text/javascript">
            function ConfirmCancel() {
                var confirmcancel_value = document.createElement("INPUT");
                confirmcancel_value.type = "hidden";
                confirmcancel_value.name = "confirmcancel_value";
                if (confirm("Cancel the update, cancellation cannot be reverted?")) {
                    confirmcancel_value.value = "Yes";
                } else {
                    confirmcancel_value.value = "No";
                }
                document.forms[0].appendChild(confirmcancel_value);
            }
        </script>

        <style type="text/css">
            .style1 {
                color: #FFFFFF;
                font-family: Candara;
                font-weight: bold;
            }

            .lbl {
                font-family: Candara;
                font-size: small;
            }

            .style2 {
                font-family: Candara;
                font-size: small;
                color: #FF3300;
                width: 202px;
            }

            .style3 {
                font-family: Candara;
                font-size: medium;
                height: 44px;
                width: 202px;
            }

            .style4 {
                height: 44px;
                width: 408px;
            }

            .RadPicker {
                vertical-align: middle
            }

            .RadPicker {
                vertical-align: middle
            }

            .RadPicker {
                vertical-align: middle
            }

            .RadPicker {
                vertical-align: middle;
                font-family: Candara;
                font-size: medium;
            }

            .rdfd_ {
                position: absolute
            }

            .rdfd_ {
                position: absolute
            }

            .rdfd_ {
                position: absolute
            }

            .rdfd_ {
                position: absolute
            }

            .RadPicker .rcTable {
                table-layout: auto
            }

            .RadPicker .rcTable {
                table-layout: auto
            }

            .RadPicker .rcTable {
                table-layout: auto
            }

            .RadPicker .rcTable {
                table-layout: auto
            }

            .RadPicker .RadInput {
                vertical-align: baseline
            }

            .RadPicker .RadInput {
                vertical-align: baseline
            }

            .RadPicker .RadInput {
                vertical-align: baseline
            }

            .RadPicker .RadInput {
                vertical-align: baseline
            }

            .RadInput_Default {
                font: 12px "segoe ui",arial,sans-serif
            }

            .RadInput {
                vertical-align: middle
            }

            .RadInput_Default {
                font: 12px "segoe ui",arial,sans-serif
            }

            .RadInput {
                vertical-align: middle
            }

            .RadInput_Default {
                font: 12px "segoe ui",arial,sans-serif
            }

            .RadInput {
                vertical-align: middle
            }

            .RadInput_Default {
                font: 12px "segoe ui",arial,sans-serif
            }

            .RadInput {
                vertical-align: middle
            }

                .RadInput .riTextBox {
                    height: 17px
                }

                .RadInput .riTextBox {
                    height: 17px
                }

                .RadInput .riTextBox {
                    height: 17px
                }

                .RadInput .riTextBox {
                    height: 17px
                }

            .RadPicker_Default .rcCalPopup {
                background-position: 0 0
            }

            .RadPicker_Default .rcCalPopup {
                background-image: url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Calendar.sprite.gif')
            }

            .RadPicker .rcCalPopup {
                display: block;
                overflow: hidden;
                width: 22px;
                height: 22px;
                background-color: transparent;
                background-repeat: no-repeat;
                text-indent: -2222px;
                text-align: center;
                -webkit-box-sizing: content-box;
                -moz-box-sizing: content-box;
                box-sizing: content-box
            }

            .RadPicker_Default .rcCalPopup {
                background-position: 0 0
            }

            .RadPicker_Default .rcCalPopup {
                background-image: url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Calendar.sprite.gif')
            }

            .RadPicker .rcCalPopup {
                display: block;
                overflow: hidden;
                width: 22px;
                height: 22px;
                background-color: transparent;
                background-repeat: no-repeat;
                text-indent: -2222px;
                text-align: center;
                -webkit-box-sizing: content-box;
                -moz-box-sizing: content-box;
                box-sizing: content-box
            }

            .RadPicker_Default .rcCalPopup {
                background-position: 0 0
            }

            .RadPicker_Default .rcCalPopup {
                background-image: url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Calendar.sprite.gif')
            }

            .RadPicker .rcCalPopup {
                display: block;
                overflow: hidden;
                width: 22px;
                height: 22px;
                background-color: transparent;
                background-repeat: no-repeat;
                text-indent: -2222px;
                text-align: center;
                -webkit-box-sizing: content-box;
                -moz-box-sizing: content-box;
                box-sizing: content-box
            }

            .RadPicker_Default .rcCalPopup {
                background-position: 0 0
            }

            .RadPicker_Default .rcCalPopup {
                background-image: url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Calendar.sprite.gif')
            }

            .RadPicker .rcCalPopup {
                display: block;
                overflow: hidden;
                width: 22px;
                height: 22px;
                background-color: transparent;
                background-repeat: no-repeat;
                text-indent: -2222px;
                text-align: center;
                -webkit-box-sizing: content-box;
                -moz-box-sizing: content-box;
                box-sizing: content-box
            }

            .RadPicker td a {
                position: relative;
                outline: 0;
                z-index: 2;
                margin: 0 2px;
                text-decoration: none
            }

            .RadPicker td a {
                position: relative;
                outline: 0;
                z-index: 2;
                margin: 0 2px;
                text-decoration: none
            }

            .RadPicker td a {
                position: relative;
                outline: 0;
                z-index: 2;
                margin: 0 2px;
                text-decoration: none
            }

            .RadPicker td a {
                position: relative;
                outline: 0;
                z-index: 2;
                margin: 0 2px;
                text-decoration: none
            }

            .style10 {
                width: 202px;
            }

            .style11 {
                width: 408px;
            }
        </style>
    </head>

    <body>
        <form id="form1">
            <div class="container">
                <div class="row">
                    <div id="divalert" runat="server" visible="false" class="alert alert-info">
                        <strong id="msgalert" runat="server">Danger!</strong>
                        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                            <span aria-hidden="true">&times;</span></button>
                        <asp:TextBox ID="txtEmpID" runat="server" Width="1px" Visible="False"></asp:TextBox>
                        <asp:TextBox ID="txtid" runat="server" Width="1px" Visible="False"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div class="panel panel-success">
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-sm-8 col-md-12">
                                        <h5 id="pagetitle" runat="server" class="page-title" style="color: #1BA691">Dependant</h5>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class=" col-md-12">
                                        <div class="form-group">
                                            <label>
                                                Name</label>
                                            <input id="aname" runat="server" class="form-control" type="text" placeholder="Dependant Name" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class=" col-md-12">
                                        <div class="form-group">
                                            <label>
                                                Relationship</label>
                                            <telerik:RadDropDownList ID="radRelationship" runat="server"
                                                DefaultMessage="-- Select --" Font-Names="Verdana" Skin="Bootstrap" Height="20px" Width="100%">
                                            </telerik:RadDropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>
                                                DATE OF BIRTH</label>
                                            <div class="cal-icon">
                                                <telerik:RadDatePicker runat="server" MinDate="1900-01-01" ForeColor="#666666" Culture="en-US"
                                                    RenderMode="Lightweight" Width="100%" resolvedrendermode="Classic" ID="radDOB"
                                                    Skin="Bootstrap">
                                                    <Calendar EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;" RenderMode="Lightweight"
                                                        Skin="Bootstrap" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False">
                                                    </Calendar>
                                                    <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelWidth="100%"
                                                        RenderMode="Lightweight">
                                                        <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                                        <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                                                        <FocusedStyle Resize="None"></FocusedStyle>
                                                        <DisabledStyle Resize="None"></DisabledStyle>
                                                        <InvalidStyle Resize="None"></InvalidStyle>
                                                        <HoveredStyle Resize="None"></HoveredStyle>
                                                        <EnabledStyle Resize="None"></EnabledStyle>
                                                    </DateInput>
                                                    <DatePopupButton CssClass="" ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                                </telerik:RadDatePicker>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" runat="server" id="divapprovalstat">
                                    <div class=" col-md-12">
                                        <div class="form-group">
                                            <label>
                                                APPROVAL
                                            </label>
                                            <input id="aapprovalstat" runat="server" class="form-control" type="text" disabled="disabled" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12 m-t-20">
                                    <button id="btnsaveobj" runat="server" onserverclick="btnAdd_Click" type="submit"
                                        style="width: 150px" class="btn btn-primary btn-success">
                                        Save & Update</button>
                                    <button id="btncloseobj" runat="server" onserverclick="btnCancel_Click" type="submit"
                                        style="width: 150px" class="btn btn-primary btn-danger">
                                        << Back</button>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-6" runat="server" id="divchanges">
                        <div class="panel panel-success">
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-sm-8 col-md-12">
                                        <h6 id="H1" runat="server" class="page-title" style="color: #1BA691">Updates</h6>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class=" col-md-12">
                                        <div class="form-group">
                                            <label>
                                                Name</label>
                                            <input id="atempname" runat="server" class="form-control" type="text" disabled="disabled" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class=" col-md-12">
                                        <div class="form-group">
                                            <label>
                                                Relationship</label>
                                            <input id="atemprelationship" runat="server" class="form-control" type="text" disabled="disabled" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>
                                                DATE OF BIRTH</label>
                                            <input id="atempdateofbirth" runat="server" class="form-control" type="text" disabled="disabled" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row" id="divbtnapprove" runat="server">
                                        <div class="col-md-12 m-t-20 text-right">
                                            <asp:LinkButton ID="lnkApprove" data-toggle="tooltip" data-original-title="Approve updates" Width="150px" runat="server" CssClass="btn btn-success" OnClientClick="ConfirmApprove()">
                    Approve <span style="margin-top:5px" aria-hidden="true" class="glyphicon glyphicon-ok"></span>
                                            </asp:LinkButton>

                                            <asp:LinkButton ID="lnkCancel" data-toggle="tooltip" data-original-title="Cancel updates" Width="150px" runat="server" CssClass="btn btn-danger" OnClientClick="ConfirmCancel()">
                    Cancel <span style="margin-top:5px" aria-hidden="true" class="glyphicon glyphicon-minus-sign"></span>
                                            </asp:LinkButton>
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
