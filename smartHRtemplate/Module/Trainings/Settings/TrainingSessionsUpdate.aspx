<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="TrainingSessionsUpdate.aspx.vb"
    Inherits="GOSHRM.TrainingSessionsUpdate" EnableEventValidation="false" Debug="true" %>

<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
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
            function ApplyClicks() {
                var click_value = document.createElement("INPUT");
                click_value.type = "hidden";
                click_value.name = "click_value";
                if (confirm("Do you want to apply selected employees to the Trainee List?")) {
                    click_value.value = "Yes";
                } else {
                    click_value.value = "No";
                }
                document.forms[0].appendChild(click_value);
            }
        </script>
        <script type="text/javascript">
            function ClearConfirm() {
                var clear_value = document.createElement("INPUT");
                clear_value.type = "hidden";
                clear_value.name = "clear_value";
                if (confirm("Do you want to clear Trainee List?")) {
                    clear_value.value = "Yes";
                } else {
                    clear_value.value = "No";
                }
                document.forms[0].appendChild(clear_value);
            }
        </script>
        <script type="text/javascript">

            function closeWin() {
                popup.close();   // Closes the new window
            }


        </script>
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
        <script type="text/javascript" id="telerikClientEvents3">
            //<![CDATA[

            function drpGrade_DropDownClosing(sender, args) {
                //Add JavaScript handler code here
                document.getElementById("Button6").click();
            }
//]]>
        </script>
        <script type="text/javascript" id="Script1">
            //<![CDATA[

            function lstTrainer_DropDownClosing(sender, args) {
                //Add JavaScript handler code here
                document.getElementById("Button6").click();
            }
//]]>
        </script>


        <script type="text/javascript" id="Script2">
            //<![CDATA[

            function lstTrainee_DropDownClosing(sender, args) {
                //Add JavaScript handler code here
                document.getElementById("Button6").click();
            }
//]]>
        </script>


        <script type="text/javascript" id="telerikClientEvents4">
            //<![CDATA[

            function radDept_DropDownClosing(sender, args) {
                //Add JavaScript handler code here
                document.getElementById("Button6").click();
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
            <div class="content container-fluid col-md-8">
                <div class="row">
                    <div id="divalert" runat="server" visible="false" class="alert alert-info">
                        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                        <strong id="msgalert" runat="server">Danger!</strong>
                        <asp:TextBox ID="txtid" runat="server" Width="13px" Style="font-size: 13px; font-family: Verdana"
                            Font-Names="Verdana" Height="10px" Visible="False"></asp:TextBox>
                    </div>
                </div>
                <div class="panel panel-success">
                    <div class="panel-heading">
                        <h5><b id="pagetitle" runat="server">Training &amp; Development Sessions</b></h5>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="control-label">
                                        Company</label>
                                    <telerik:RadComboBox AutoPostBack="True" ID="cboCompany" runat="server" Filter="Contains"
                                        EnableCheckAllItemsCheckBox="False" RenderMode="Lightweight" Width="100%" ForeColor="#666666"
                                        Skin="Bootstrap">
                                    </telerik:RadComboBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="control-label">
                                        Course</label>
                                    <telerik:RadComboBox runat="server" RenderMode="Lightweight" ResolvedRenderMode="Classic"
                                        ID="cboCourse" Width="100%" Skin="Bootstrap" Filter="Contains" Font-Names="Verdana"
                                        AutoPostBack="True" ForeColor="#666666">
                                    </telerik:RadComboBox>
                                </div>
                            </div>
                                </div>
                            <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="control-label">
                                        Session Name
                                    </label>
                                    <asp:TextBox runat="server" ID="txtname" Height="33px" Width="100%" Skin="Bootstrap"
                                        Font-Names="Verdana" BorderColor="#CCCCCC" BorderWidth="1px" ForeColor="#666666"></asp:TextBox>

                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="control-label">
                                        Category</label>
                                    <telerik:RadDropDownList ID="drpCategory" runat="server" DefaultMessage="-- Select --"
                                        Width="100%" ForeColor="#666666" Skin="Bootstrap">
                                    </telerik:RadDropDownList>
                                </div>
                            </div>
                                </div>
                            <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="control-label">
                                        Coordinator
                                    </label>
                                    <telerik:RadComboBox ID="cbocoordinator" runat="server" Filter="Contains" EnableCheckAllItemsCheckBox="True"
                                        RenderMode="Lightweight" Font-Names="Verdana" Width="100%"
                                        Skin="Bootstrap" ForeColor="#666666">
                                    </telerik:RadComboBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="control-label">
                                        Scheduled Date</label>
                                    <%--<input type="text" class="form-control datetimepicker">--%>
                                    <telerik:RadDatePicker ID="radScheduleTime" runat="server" Width="100%" Skin="Bootstrap"
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
                            <div class="row">
                            <div class="col-md-6">
                                <label class="control-label">
                                    Time</label>
                                <div class="form-group col-md-12">
                                    <%--<input type="text" class="form-control datetimepicker">--%>
                                    <div class="col-md-4 col-sm-4 col-xs-12">
                                        <telerik:RadComboBox ID="radHourStart" runat="server" ResolvedRenderMode="Classic"
                                            Width="100%" Skin="Bootstrap" Font-Names="Verdana" ForeColor="#666666">
                                            <Items>
                                                <telerik:RadComboBoxItem runat="server" Text="1" Value="1"
                                                    Owner="radHourStart" />
                                                <telerik:RadComboBoxItem runat="server" Text="2" Value="2"
                                                    Owner="radHourStart" />
                                                <telerik:RadComboBoxItem runat="server" Text="3" Value="3"
                                                    Owner="radHourStart" />
                                                <telerik:RadComboBoxItem runat="server" Text="4" Value="4"
                                                    Owner="radHourStart" />
                                                <telerik:RadComboBoxItem runat="server" Text="5" Value="5"
                                                    Owner="radHourStart" />
                                                <telerik:RadComboBoxItem runat="server" Text="6" Value="6"
                                                    Owner="radHourStart" />
                                                <telerik:RadComboBoxItem runat="server" Text="7" Value="7"
                                                    Owner="radHourStart" />
                                                <telerik:RadComboBoxItem runat="server" Text="8" Value="8"
                                                    Owner="radHourStart" />
                                                <telerik:RadComboBoxItem runat="server" Text="9" Value="9"
                                                    Owner="radHourStart" />
                                                <telerik:RadComboBoxItem runat="server" Text="10" Value="10"
                                                    Owner="radHourStart" />
                                                <telerik:RadComboBoxItem runat="server" Text="11" Value="11"
                                                    Owner="radHourStart" />
                                                <telerik:RadComboBoxItem runat="server" Text="12" Value="12"
                                                    Owner="radHourStart" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </div>
                                    <div class="col-md-4 col-sm-4 col-xs-12">
                                        <telerik:RadComboBox ID="radMinStart" runat="server" ResolvedRenderMode="Classic"
                                            Width="100%" Skin="Bootstrap" Font-Names="Verdana" ForeColor="#666666">
                                            <Items>
                                                <telerik:RadComboBoxItem runat="server" Text="00" Value="00"
                                                    Owner="radMinStart" />
                                                <telerik:RadComboBoxItem runat="server" Text="15" Value="15"
                                                    Owner="radMinStart" />
                                                <telerik:RadComboBoxItem runat="server" Text="30" Value="30"
                                                    Owner="radMinStart" />
                                                <telerik:RadComboBoxItem runat="server" Text="45" Value="45"
                                                    Owner="radMinStart" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </div>
                                    <div class="col-md-4 col-sm-4 col-xs-12">
                                        <telerik:RadComboBox ID="radTimeStart" runat="server" ResolvedRenderMode="Classic"
                                            Width="100%" Skin="Bootstrap" Font-Names="Verdana" ForeColor="#666666">
                                            <Items>
                                                <telerik:RadComboBoxItem runat="server" Text="AM" Value="AM"
                                                    Owner="radTimeStart" />
                                                <telerik:RadComboBoxItem runat="server" Text="PM" Value="PM"
                                                    Owner="radTimeStart" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </div>
                                </div>
                            </div>
                                
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="control-label">
                                        Due Date</label>
                                    <telerik:RadDatePicker ID="radDateDue" runat="server" Width="100%" Skin="Bootstrap"
                                        RenderMode="Lightweight" Font-Names="Verdana" ForeColor="#666666">
                                        <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True"
                                            FastNavigationNextText="&amp;lt;&amp;lt;" RenderMode="Lightweight">
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
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="control-label">
                                        Session Type</label>
                                    <telerik:RadDropDownList ID="radSessionType" runat="server" DefaultMessage="-- Select --"
                                        Font-Names="Verdana" Width="100%" Skin="Bootstrap" AutoPostBack="True"
                                        ForeColor="#666666">
                                    </telerik:RadDropDownList>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="control-label">
                                        Activity/Delivery Method *</label>
                                    <telerik:RadDropDownList ID="radDeliveryMethod" runat="server" DefaultMessage="-- Select --"
                                        Font-Names="Verdana" Width="100%" Skin="Bootstrap" ResolvedRenderMode="Classic"
                                        ForeColor="#666666">
                                        <Items>
                                            <telerik:DropDownListItem runat="server" Text="Classroom" Value="Classroom" />
                                            <telerik:DropDownListItem runat="server" Text="Online" Value="Online" />
                                            <telerik:DropDownListItem runat="server" Text="Self Study" Value="Self Study" />
                                            <telerik:DropDownListItem runat="server" Text="Mentoring" Value="Mentoring" />
                                            <telerik:DropDownListItem runat="server" Text="E-Learning" Value="E-Learning" />
                                            <telerik:DropDownListItem runat="server" Text="Job Rotation"
                                                Value="Job Rotation" />
                                        </Items>
                                    </telerik:RadDropDownList>
                                </div>
                            </div>



                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="control-label">
                                        Slots</label>
                                    <asp:TextBox ID="txtAttendee" runat="server" Font-Names="Verdana" Width="100%" Height="33px"
                                        Skin="Bootstrap" AutoPostBack="True" BorderColor="#CCCCCC" BorderWidth="1px"
                                        ForeColor="#666666"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="control-label">
                                        Currency</label>
                                    <telerik:RadDropDownList ID="radCurrency" runat="server" DefaultMessage="-- Select --"
                                        Font-Names="Verdana" Width="100%" Skin="Bootstrap" ForeColor="#666666">
                                    </telerik:RadDropDownList>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="control-label">
                                        Cost</label>
                                    <asp:TextBox ID="txtCost" runat="server" Font-Names="Verdana" Width="100%" Height="33px"
                                        Skin="Bootstrap" BorderColor="#CCCCCC" BorderWidth="1px" ForeColor="#666666"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="control-label">
                                        Application Assessment Date</label>
                                    <telerik:RadDatePicker ID="datApplicationAssessment" runat="server" Width="100%" Skin="Bootstrap"
                                        RenderMode="Lightweight" Font-Names="Verdana" ForeColor="#666666" ToolTip="Select date for Application Assessment">
                                        <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True"
                                            FastNavigationNextText="&amp;lt;&amp;lt;" RenderMode="Lightweight">
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
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="control-label">
                                        Training Location</label>
                                    <textarea id="txtLocation" runat="server" rows="4" class="form-control"></textarea>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="control-label">
                                        Note</label>
                                    <textarea id="txtnote" runat="server" rows="4" class="form-control" cols="1"></textarea>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <div id="collapse_acc" runat="server" class=" card-box">
                                    <div class="card-header">
                                        <h6><b><a class="collapsed card-link" data-toggle="collapse" href="#collapseTwo" title="Click to select trainer">Trainer
                                        </a></b></h6>
                                    </div>
                                    <div id="collapseTwo" class="collapse" data-parent="#accordion">
                                        <div class="card-body">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <div class="panel panel-default">
                                                        <div class="panel-heading">
                                                            <b id="B1" runat="server">Trainer</b>
                                                        </div>
                                                        <div class="panel-body">
                                                            <input id="txtTrainer" runat="server" type="text" class="form-control floating" />
                                                            <telerik:RadComboBox ID="cboTrainer" runat="server" CheckBoxes="True"
                                                                Filter="Contains" RenderMode="Lightweight" Width="100%" Skin="Bootstrap"
                                                                Font-Names="Verdana" ForeColor="#666666" AutoPostBack="True" EnableCheckAllItemsCheckBox="True">
                                                            </telerik:RadComboBox>
                                                            <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Always">
                                                                <ContentTemplate>
                                                                    <telerik:RadListBox ID="lstTrainer" runat="server" ResolvedRenderMode="Classic" BorderStyle="None"
                                                                        Enabled="False" Width="100%" Skin="Bootstrap" RenderMode="Lightweight" Font-Names="Verdana"
                                                                        ForeColor="#666666">
                                                                        <ButtonSettings TransferButtons="All"></ButtonSettings>
                                                                    </telerik:RadListBox>
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="cboTrainer" EventName="ItemChecked" />
                                                                    <asp:AsyncPostBackTrigger ControlID="cboTrainer" EventName="CheckAllCheck" />
                                                                </Triggers>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <div id="Div1" runat="server" class=" card-box">
                                    <div class="card-header">
                                        <h6><b><a class="collapsed card-link" data-toggle="collapse" href="#collapseThree" title="Click to select trainees by grade, department or specific employees">Trainees
                                        </a></b></h6>
                                    </div>
                                    <div id="collapseThree" class="collapse" data-parent="#accordion">
                                        <div class="card-body">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <div class="panel panel-default">
                                                        <div class="panel-heading">
                                                            <b id="B2" runat="server">Trainees</b>
                                                        </div>
                                                        <div class="panel-body">
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label class="control-label">
                                                                        Grade</label>
                                                                    <telerik:RadComboBox ID="drpGrade" runat="server" CheckBoxes="True" Filter="Contains"
                                                                        EnableCheckAllItemsCheckBox="True" RenderMode="Lightweight"
                                                                        Width="100%" Skin="Bootstrap"
                                                                        Font-Names="Verdana" ForeColor="#666666"
                                                                        AutoPostBack="True">
                                                                    </telerik:RadComboBox>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label class="control-label">
                                                                        Department</label>
                                                                    <telerik:RadDropDownTree ID="radDept" runat="server" Width="100%" Skin="Bootstrap"
                                                                        RenderMode="Lightweight" Font-Names="Verdana"
                                                                        CheckBoxes="CheckChildNodes"
                                                                        ForeColor="#666666" AutoPostBack="True">
                                                                    </telerik:RadDropDownTree>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label class="control-label">
                                                                        Names</label>
                                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel5" UpdateMode="Always">
                                                                        <ContentTemplate>
                                                                            <telerik:RadComboBox ID="drpTrainee" runat="server" CheckBoxes="True" Filter="Contains"
                                                                                EnableCheckAllItemsCheckBox="True" RenderMode="Lightweight" Width="100%"
                                                                                Skin="Bootstrap" Font-Names="Verdana"
                                                                                ForeColor="#666666" AutoPostBack="True">
                                                                            </telerik:RadComboBox>
                                                                        </ContentTemplate>
                                                                        <Triggers>
                                                                            <asp:AsyncPostBackTrigger ControlID="drpGrade" EventName="ItemChecked" />
                                                                        </Triggers>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-12">
                                                                <div class="form-group">
                                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Always">
                                                                        <ContentTemplate>
                                                                            <telerik:RadListBox ID="lstTrainee" runat="server" ResolvedRenderMode="Classic" BorderStyle="None"
                                                                                Enabled="False" Width="100%" Skin="Bootstrap" RenderMode="Lightweight" Font-Names="Verdana"
                                                                                ForeColor="#666666">
                                                                                <ButtonSettings TransferButtons="All"></ButtonSettings>
                                                                            </telerik:RadListBox>
                                                                        </ContentTemplate>
                                                                        <Triggers>
                                                                            <asp:AsyncPostBackTrigger ControlID="drpTrainee" EventName="ItemChecked" />
                                                                            <asp:AsyncPostBackTrigger ControlID="drpTrainee" EventName="CheckAllCheck" />
                                                                        </Triggers>
                                                                    </asp:UpdatePanel>

                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="m-t-20 text-center">
                            <asp:Button ID="btnMail0" runat="server" Text="Learning Assessment Notification"
                                ForeColor="White" Width="210px" Skin="Bootstrap" BorderStyle="None"
                                ToolTip="Send Learning Assessment Notification to Trainees to answer test questions"
                                Font-Names="Verdana" OnClientClick="Confirm()" CssClass="btn btn-success" Visible="False"></asp:Button>
                            <asp:Button ID="btnAdd" runat="server" Text="Save Session" ForeColor="White"
                                Width="210px" Skin="Bootstrap" BorderStyle="None" Font-Names="Verdana" CssClass="btn btn-success"
                                Font-Bold="True" />
                            <asp:Button ID="btnCancel" runat="server" Text="Back" ForeColor="White"
                                Width="210px" Skin="Bootstrap" BorderStyle="None" Font-Names="Verdana" CssClass="btn btn-danger"
                                Font-Bold="True" />
                            <asp:Button ID="btnMail" runat="server" Text="Send Notification"
                                CssClass="btn btn-success" ForeColor="White" Width="210px" Skin="Bootstrap" BorderStyle="None"
                                ToolTip="Sending Training Notification to Participants" Font-Names="Verdana"
                                Font-Bold="True" />
                            <asp:Button ID="btnAssessmentNotify" runat="server" Text="Application Assessment"
                                ForeColor="White" Width="210px" Skin="Bootstrap" BorderStyle="None"
                                CssClass="btn btn-success" ToolTip="Send Application Assessment Notification to Trainees"
                                Font-Names="Verdana" OnClientClick="ConfirmApp()" Font-Bold="True" />
                        </div>
                        <div class="row">
                            <asp:Button ID="Button2" runat="server" BackColor="White" BorderStyle="None" Font-Size="1px" />
                            <asp:Button ID="Button1" runat="server" BackColor="White" BorderStyle="None" Font-Size="1px" />
                            <asp:Button ID="Button5" runat="server" BackColor="White" BorderStyle="None" Font-Size="1px" />
                            <asp:Button ID="Button6" runat="server" BackColor="White" BorderStyle="None" Font-Size="1px" />
                        </div>
                    </div>
                </div>
            </div>
            <p></p>
        </form>
    </body>
    </html>
</asp:Content>
