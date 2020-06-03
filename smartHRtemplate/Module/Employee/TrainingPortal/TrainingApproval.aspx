<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="TrainingApproval.aspx.vb"
    Inherits="GOSHRM.TrainingApproval" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title></title>
        <script type="text/javascript">
            function Confirm() {
                var confirm_value = document.createElement("INPUT");
                confirm_value.type = "hidden";
                confirm_value.name = "confirm_value";
                if (confirm("Notify Trainees to commence Learning Assessment?")) {
                    confirm_value.value = "Yes";
                } else {
                    confirm_value.value = "No";
                }
                document.forms[0].appendChild(confirm_value);
            }
        </script>

        <script type="text/javascript">
            function ConfirmApp() {
                var confirm_value1 = document.createElement("INPUT");
                confirm_value1.type = "hidden";
                confirm_value1.name = "confirm_value1";
                if (confirm("Notify Trainees to commence Application Assessment?")) {
                    confirm_value1.value = "Yes";
                } else {
                    confirm_value1.value = "No";
                }
                document.forms[0].appendChild(confirm_value1);
            }
        </script>


        <script type="text/javascript">

            function closeWin() {
                popup.close();   // Closes the new window
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
                font-size: medium;
            }

            .style2 {
                font-family: Candara;
                font-size: small;
                color: #FF3300;
            }

            .style5 {
                font-family: Candara;
                font-size: medium;
                width: 183px;
            }

            .style7 {
                width: 183px;
            }

            .style8 {
                width: 468px;
            }
        </style>
        <script type="text/javascript" id="telerikClientEvents1">
            //<![CDATA[

            function drpTrainee_DropDownClosing(sender, args) {
                //Add JavaScript handler code here
                document.getElementById("Button1").click();
            }
//]]>
        </script>
        <script type="text/javascript" id="telerikClientEvents2">
            //<![CDATA[

            function cboTrainer_DropDownClosing(sender, args) {
                //Add JavaScript handler code here
                document.getElementById("Button2").click();
            }
//]]>
        </script>
    </head>

    <body>
        <form>
            <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
            <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
            <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
                rel="Stylesheet" type="text/css" />

            <div class="container col-md-8">
                <div class="panel panel-success">
                    <div class="panel-body">
                        <div id="divalert" runat="server" visible="false" class="row col-md-8 alert alert-info">
                            <asp:TextBox ID="txtid" runat="server" Width="13px" Style="font-size: medium; font-family: Candara"
                                Font-Names="Candara" Height="10px" Visible="False"></asp:TextBox>
                            <strong id="msgalert" runat="server">Danger!</strong>
                        </div>
                        <div class="row col-md-8 col-md-offset-0">
                            <h5 id="pagetitle" runat="server" class="page-title">Training Request
                            </h5>
                        </div>
                        <div id="divemplink" runat="server" class="row">
                            <div id="divjoboffer" runat="server" class="col-sm-3 col-md-12 col-xs-6 pull-left">
                                <p>
                                    <a href="Approvaltrainings"><u>Trainings to Approve</u></a>
                                    <label>
                                        >
                                    </label>
                                    <a id="A1" href="#"><u>Training Request</u></a>
                                </p>
                            </div>
                        </div>
                        <div class="row">
                            <div class=" col-md-12">
                                <div class="form-group">
                                    <label>
                                        SESSION NAME</label>
                                    <asp:TextBox ID="txtname" runat="server" Width="100%"
                                        Font-Names="Verdana" CssClass="form-control" BorderColor="#CCCCCC" BorderWidth="1px"
                                        Enabled="False"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class=" col-md-12">
                                <div class="form-group">
                                    <label>
                                        COORDINATOR</label>
                                    <telerik:RadDropDownList ID="radCoordinator" Skin="Bootstrap" runat="server" DefaultMessage="-- Select --"
                                        Font-Names="Verdana" Height="16px" Width="100%"
                                        Enabled="False">
                                    </telerik:RadDropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class=" col-md-6">
                                <div class="form-group">
                                    <label>
                                        SCHEDULED DATE</label>
                                    <telerik:RadDatePicker ID="radScheduleTime" Skin="Bootstrap" runat="server" Width="100%"
                                        RenderMode="Lightweight" Font-Names="Verdana" 
                                        Enabled="False">
                                        <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                                            EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;"
                                            RenderMode="Lightweight">
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
                            <div class=" col-md-6">
                                <div class="form-group">
                                    <label runat="server">
                                        DUE DATE</label>
                                    <telerik:RadDatePicker ID="radDateDue" Skin="Bootstrap" runat="server" Width="100%"
                                        RenderMode="Lightweight" Enabled="False">
                                        <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                                            EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;"
                                            RenderMode="Lightweight">
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
                        </div>
                        <div class="row">
                            <div class=" col-md-6">
                                <div class="form-group">
                                    <label>
                                        TIME</label>
                                    <input type="text" class="form-control" runat="server" id="lbltime" />
                                </div>
                            </div>
                        
                            <div class=" col-md-6">
                                <div class="form-group">
                                    <label>
                                        SESSION TYPE</label>
                                    <telerik:RadDropDownList ID="radSessionType" Skin="Bootstrap" runat="server" DefaultMessage="-- Select --"
                                        Font-Names="Verdana" Height="16px" Width="100%"
                                        AutoPostBack="True" Enabled="False">
                                    </telerik:RadDropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class=" col-md-12">
                                <div class="form-group">
                                    <label>
                                        TRAINER</label>
                                    <asp:TextBox ID="txtTrainer" runat="server" Width="100%" Font-Names="Verdana"
                                        CssClass="form-control" BorderColor="#CCCCCC" BorderWidth="1px" Enabled="False"></asp:TextBox>
                                    <telerik:RadComboBox ID="cboTrainer" runat="server" CheckBoxes="True" Filter="Contains"
                                        AutoPostBack="True" EnableCheckAllItemsCheckBox="True" RenderMode="Lightweight"
                                        Width="100%" OnClientDropDownClosing="cboTrainer_DropDownClosing"
                                        Enabled="False" Visible="False">
                                    </telerik:RadComboBox>
                                    <telerik:RadListBox ID="lstTrainer" Skin="Bootstrap" runat="server" ResolvedRenderMode="Classic" BorderStyle="None"
                                        Enabled="False" Width="100%" RenderMode="Lightweight" Font-Names="Verdana"
                                        Font-Size="12px">
                                        <ButtonSettings TransferButtons="All"></ButtonSettings>
                                    </telerik:RadListBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class=" col-md-12">
                                <div class="form-group">
                                    <label>
                                        DELIVERY METHOD</label>
                                    <telerik:RadDropDownList Skin="Bootstrap" ID="radDeliveryMethod" runat="server" DefaultMessage="-- Select --"
                                        Font-Names="Verdana" Font-Size="12px" Width="100%"
                                        Enabled="False">
                                    </telerik:RadDropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class=" col-md-12">
                                <div class="form-group">
                                    <label>
                                        TRAINING LOCATION</label>
                                    <asp:TextBox ID="txtLocation" runat="server" Width="100%" Font-Names="Verdana"
                                        BorderColor="#CCCCCC" BorderWidth="1px" CssClass="form-control"
                                        Height="100px" TextMode="MultiLine" ReadOnly="True" Enabled="False"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class=" col-md-12">
                                <div class="form-group">
                                    <label>
                                        REQUEST BY</label>
                                    <asp:Label ID="lblemployee" runat="server" Text="Request By" Font-Bold="False" ForeColor="#666666"
                                        Font-Names="Verdana" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class=" col-md-12">
                                <div class="form-group">
                                    <label>
                                        APPROVAL STATUS</label>
                                    <telerik:RadComboBox ID="cboApproval" Skin="Bootstrap" runat="server" RenderMode="Lightweight"
                                        Width="100%"
                                        Font-Names="Verdana" ForeColor="#666666" CssClass="form-control">
                                    </telerik:RadComboBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class=" col-md-12">
                                <div class="form-group">
                                    <label>
                                        COMMENT</label>
                                    <asp:TextBox ID="txtComment" runat="server" Width="100%" Font-Names="Verdana"
                                        CssClass="form-control" BorderColor="#CCCCCC" BorderWidth="1px"
                                        Height="100px" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 m-t-20 text-center">
                                <button id="btnupdate" runat="server" onserverclick="btnAdd_Click" type="submit"
                                    style="width: 150px" class="btn btn-primary btn-success">
                                    Save &amp; Update</button>
                                <button id="btcancel" runat="server" onserverclick="btnCancel_Click" type="submit"
                                    style="width: 150px" class="btn btn-primary btn-info">
                                    << Back</button>
                            </div>
                        </div>
                        <div class="row">
                            <div>
                                <asp:Label ID="lblempid" runat="server"
                                    Font-Names="Verdana" Font-Size="12px" Visible="False"></asp:Label>
                            </div>
                            <div class="col-md-12 m-t-20 text-center">

                                <asp:Button ID="Button2" runat="server" BackColor="White" BorderStyle="None"
                                    Font-Size="10px" />
                                <asp:Button ID="Button1" runat="server" BackColor="White" BorderStyle="None"
                                    Font-Size="10px" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <p>
                &nbsp;
            </p>
        </form>
    </body>
    </html>
</asp:Content>
