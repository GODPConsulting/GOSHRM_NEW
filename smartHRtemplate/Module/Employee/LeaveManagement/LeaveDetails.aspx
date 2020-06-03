<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="LeaveDetails.aspx.vb"
    Inherits="GOSHRM.LeaveDetails" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI.Gantt" TagPrefix="cc1" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title></title>
        <link rel="icon" type="image/png" href="../../../images/goshrm.png">
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
        <script type="text/javascript">
            function ConfirmCancellation() {
                var confirm_value = document.createElement("INPUT");
                confirm_value.type = "hidden";
                confirm_value.name = "confirm_value";
                if (confirm("Do you want to cancel the leave request?")) {
                    confirm_value.value = "Yes";
                } else {
                    confirm_value.value = "No";
                }
                document.forms[0].appendChild(confirm_value);
            }
        </script>
        <style type="text/css">
            .modal {
                position: fixed;
                top: 0;
                left: 0;
                background-color: black;
                z-index: 99;
                opacity: 0.8;
                filter: alpha(opacity=80);
                -moz-opacity: 0.8;
                min-height: 100%;
                width: 100%;
            }

            .loading {
                font-family: Arial;
                font-size: 10pt;
                border: 5px solid #67CFF5;
                width: 200px;
                height: 100px;
                display: none;
                position: fixed;
                background-color: White;
                z-index: 999;
            }
        </style>
    </head>
    <body>
        <form id="form1" action="">
            <div class="container">
                <div class="row">
                    <div class="col-md-10">
                        <div id="divalert" runat="server" visible="false" class="alert alert-info">
                            <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <strong
                                id="msgalert" runat="server">Danger!</strong>

                            <asp:TextBox ID="txtid" runat="server" Width="1px" Height="1px" Visible="False"></asp:TextBox>
                            <asp:Label ID="lblleavetype" runat="server" Font-Bold="True" Width="1px" Height="1px"
                                Visible="False"></asp:Label>
                            <asp:Label ID="lblLoanRefNo" runat="server" Font-Bold="True" Width="1px" Height="1px"
                                Visible="False"></asp:Label>
                            <asp:Label ID="lbleligible" runat="server" Width="1px" Height="1px" Visible="False"></asp:Label>
                            <asp:Label ID="lblBalance" runat="server" Visible="False" Width="1px" Height="1px"></asp:Label>
                            <asp:Label ID="lblGradeLevel" runat="server" Visible="False" Width="1px" Height="1px"></asp:Label>
                            <asp:Label ID="lblEmpID" runat="server" Visible="False" Width="1px" Height="1px"></asp:Label>
                        </div>
                    </div>
                </div>
                <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Always">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-md-10">
                                <div id="divalert1" runat="server" visible="false" class="alert alert-info">
                                    <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <strong
                                        id="msgalert1" runat="server">Danger!</strong>

                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="radStartDate" EventName="SelectedDateChanged" />
                        <asp:AsyncPostBackTrigger ControlID="radEndDate" EventName="SelectedDateChanged" />
                    </Triggers>
                </asp:UpdatePanel>
                <div class="row">
                    <div class=" col-md-10">
                        <div class="panel panel-success">
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-sm-8 col-md-12">
                                        <h5 id="pagetitle" runat="server" class="page-title" style="color: #1BA691">Leave</h5>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class=" col-md-6">
                                        <div class="form-group">
                                            <label>
                                                NAME</label>
                                            <input id="aempname" runat="server" class="form-control" type="text" disabled="disabled" />
                                        </div>
                                    </div>
                                    <div class=" col-md-6">
                                        <div class="form-group">
                                            <label>LEAVE LENGTH</label>
                                            <telerik:RadComboBox ID="radlength" ForeColor="#666666"
                                                runat="server"
                                                RenderMode="Lightweight"
                                                ResolvedRenderMode="Classic" Width="100%" Skin="Bootstrap" AutoPostBack="True">
                                                <items>
                                        <telerik:RadComboBoxItem runat="server" Text="Full Day" Value="Full" 
                                            Owner="radMonth" />
                                        <telerik:RadComboBoxItem runat="server" Text="Half Day" Value="Half" 
                                            Owner="radMonth" />                                     
                                    </items>
                                            </telerik:RadComboBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class=" col-md-6">
                                        <div class="form-group">
                                            <label>
                                                START DATE</label>
                                            <telerik:RadDatePicker ID="radStartDate" runat="server" AutoPostBack="True" ForeColor="#666666"
                                                RenderMode="Lightweight" Skin="Bootstrap" Width="100%">
                                                <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True"
                                                    FastNavigationNextText="&amp;lt;&amp;lt;" RenderMode="Lightweight" Skin="Bootstrap">
                                                </Calendar>
                                                <DateInput DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" LabelWidth="40%"
                                                    AutoPostBack="True" RenderMode="Lightweight">
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
                                    <div class=" col-md-6">
                                        <div class="form-group">
                                            <label>
                                                END DATE</label>
                                            <telerik:RadDatePicker ID="radEndDate" runat="server" AutoPostBack="True" ForeColor="#666666"
                                                RenderMode="Lightweight" Skin="Bootstrap" Width="100%">
                                                <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True"
                                                    FastNavigationNextText="&amp;lt;&amp;lt;" RenderMode="Lightweight" Skin="Bootstrap">
                                                </Calendar>
                                                <DateInput DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" LabelWidth="40%"
                                                    AutoPostBack="True" RenderMode="Lightweight">
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
                                <div class="row">
                                    <asp:UpdatePanel runat="server" ID="UpdatePanel5" UpdateMode="Always">
                                        <ContentTemplate>
                                            <div class=" col-md-6">
                                                <div class="form-group">
                                                    <label>
                                                        NUMBER OF DAYS</label>
                                                    <input id="aDays" runat="server" class="form-control" type="text" disabled="disabled" />
                                                </div>
                                            </div>
                                            <div id="divpaydate" runat="server" class=" col-md-6">
                                                <div class="form-group">
                                                    <label>
                                                        ALLOWANCE PAY DATE</label>
                                                    <telerik:RadDatePicker ID="radPayDate" runat="server" MinDate="" ForeColor="#666666"
                                                        RenderMode="Lightweight" ToolTip="Select Salary Month you want allowance paid"
                                                        Skin="Bootstrap" Width="100%">
                                                        <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True"
                                                            FastNavigationNextText="&amp;lt;&amp;lt;" RenderMode="Lightweight" Skin="Bootstrap">
                                                        </Calendar>
                                                        <DateInput DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" LabelWidth="40%"
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
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="radStartDate" EventName="SelectedDateChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="radEndDate" EventName="SelectedDateChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="row">
                                    <div class=" col-md-12">
                                        <div class="form-group">
                                            <label>
                                                ATTACHMENT</label>
                                            <input class="form-control" type="file" id="file1" runat="server" />
                                            <button id="lnkclr" runat="server" type="submit" onserverclick="lnkClear_Click" class="btn btn-link">
                                                Clear Attachment</button>
                                            <button id="lnkdownload" runat="server" type="submit" onserverclick="lnkDownloadAttach_Click"
                                                class="btn btn-link">
                                                Download Attachment</button>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class=" col-md-12">
                                        <div class="form-group">
                                            <label>
                                                REASON</label>
                                            <textarea id="areason" runat="server" class="form-control" rows="5"></textarea>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class=" col-md-12">
                                        <div class="form-group">
                                            <label>
                                                MANAGER</label>
                                            <input id="amgrname" runat="server" class="form-control" type="text" disabled="disabled" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class=" col-md-6">
                                        <div class="form-group">
                                            <label>
                                                APPROVAL STATUS</label>
                                            <input id="amgrstatus" runat="server" class="form-control" type="text" disabled="disabled" />
                                        </div>
                                    </div>
                                    <div class=" col-md-6">
                                        <div class="form-group">
                                            <label>
                                                HUMAN RESOURCE DEPARTMENT</label>
                                            <input id="ahrstatus" runat="server" class="form-control" type="text" disabled="disabled" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div id="divmgrcomment" runat="server" class=" col-md-12">
                                        <div class="form-group">
                                            <label>
                                                MANAGER COMMENT</label>
                                            <textarea id="amgrcomment" runat="server" class="form-control" rows="5" readonly="readonly"></textarea>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div id="divhrcomment" runat="server" class=" col-md-12">
                                        <div class="form-group">
                                            <label>
                                                HUMAN RESOURCE COMMENT</label>
                                            <textarea id="ahrcomment" runat="server" class="form-control" rows="5" readonly="readonly"></textarea>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12 m-t-20">
                                        <button id="btnupdate" runat="server" onserverclick="btnAdd_Click" type="submit"
                                            style="width: 150px" class="btn btn-primary btn-success">
                                            Save &amp; Update</button>
                                        <button id="Button1" runat="server" onserverclick="btnCancel_Click" type="submit"
                                            style="width: 150px" class="btn btn-primary btn-info">
                                            << Back</button>
                                        <asp:Button ID="btnApprove" runat="server" Text="Approve" Visible="false"
                                            ForeColor="White" Width="100%" Height="30px" CssClass="btn btn-danger btn-success pull-right" OnClientClick="ConfirmCancellation()"
                                            ToolTip="Cancel leave request"
                                            BorderStyle="None" Font-Names="Verdana" Font-Size="12px" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </form>
        <div class="loading" align="center">
            Processing, please wait...<br />
            <br />
            <img src="../../../images/loaders.gif" alt="" />
        </div>
    </body>
    </html>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
</asp:Content>
