<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="false"
    CodeBehind="WorkForcePlanDetailUpdate.aspx.vb" Inherits="GOSHRM.WorkForcePlanDetailUpdate"
    EnableEventValidation="false" %>

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
        <script type="text/javascript" id="telerikClientEvents1">
//<![CDATA[

            function cboCompetency_DropDownClosing(sender, args) {
                //Add JavaScript handler code here
                document.getElementById("Button1").click();
            }
//]]>
        </script>
        <script type="text/javascript" id="telerikClientEvents2">
//<![CDATA[

            function cboLocation_DropDownClosing(sender, args) {
                //Add JavaScript handler code here
                document.getElementById("Button2").click();
            }
//]]>
        </script>
    </head>
    <body>
        <form id="form1" action="">
        <div class="container">
            <div class="row">
                 <div class="col-md-10">
                <div id="divalert" runat="server" visible="false" class="alert alert-info">
                    <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <strong
                        id="msgalert" runat="server">Danger!</strong>
                    <asp:Label ID="lblentry" runat="server" Font-Names="Verdana" Font-Size="5px" Visible="False"></asp:Label>
                    <asp:Label ID="lblid" runat="server" Font-Names="Verdana" Font-Size="1px" Visible="False"></asp:Label>
                    <asp:Label ID="varyear" runat="server" Font-Names="Verdana" Font-Size="1px" Visible="False"></asp:Label>
                    <asp:Label ID="lblprimaryid" runat="server" Font-Names="Verdana" Font-Size="1px"
                        Visible="False"></asp:Label>
                </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-8 col-xs-8">
                    <h5 id="pagetitle" runat="server" class="page-title" style="color: #1BA691">
                        Head</h5>
                </div>
            </div>
            <div id="divemplink" runat="server" class="row">
                <div id="divjoboffer" runat="server" class="col-sm-3 col-md-12 col-xs-6 pull-left">
                    <p>
                        <a href="Workforce"><u>Workforce</u></a>
                        <label>
                            >
                        </label>
                        <a href="#" runat="server" onserverclick="btnCancel_Click"><u>Workforce Plan Breakdown</u></a>
                        <label>
                            >
                        </label>
                        <a href="#">Workforce Plan Detail</a>
                    </p>
                </div>
            </div>
            <div id="divhrlink" runat="server" class="row">
                <div id="div1" runat="server" class="col-sm-3 col-md-12 col-xs-6 pull-left">
                    <p>
                        <a href="~/Recruitment/WorkForceBudget"><u>Workforce Budget & Planning</u></a>
                        <label>
                            >
                        </label>
                        <a id="A1" href="#" runat="server" onserverclick="btnCancel1_Click"><u>Workforce Budget
                            & Planning Breakdown</u></a>
                        <label>
                            >
                        </label>
                        <a href="#">Workforce Detail</a>
                    </p>
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
                                            AS AT</label>
                                        <telerik:radcombobox runat="server" dropdownautowidth="Enabled" rendermode="Lightweight"
                                            resolvedrendermode="Classic" width="50%" id="radMonthStart" autopostback="True"
                                            forecolor="#666666" skin="Bootstrap">
                                            <items>
                                    <telerik:RadComboBoxItem runat="server" Text="January" Value="1" Owner="radMonthStart">
                                    </telerik:RadComboBoxItem>
                                    <telerik:RadComboBoxItem runat="server" Text="February" Value="2" Owner="radMonthStart">
                                    </telerik:RadComboBoxItem>
                                    <telerik:RadComboBoxItem runat="server" Text="March" Value="3" Owner="radMonthStart">
                                    </telerik:RadComboBoxItem>
                                    <telerik:RadComboBoxItem runat="server" Text="April" Value="4" Owner="radMonthStart">
                                    </telerik:RadComboBoxItem>
                                    <telerik:RadComboBoxItem runat="server" Text="May" Value="5" Owner="radMonthStart">
                                    </telerik:RadComboBoxItem>
                                    <telerik:RadComboBoxItem runat="server" Text="June" Value="6" Owner="radMonthStart">
                                    </telerik:RadComboBoxItem>
                                    <telerik:RadComboBoxItem runat="server" Text="July" Value="7" Owner="radMonthStart">
                                    </telerik:RadComboBoxItem>
                                    <telerik:RadComboBoxItem runat="server" Text="August" Value="8" Owner="radMonthStart">
                                    </telerik:RadComboBoxItem>
                                    <telerik:RadComboBoxItem runat="server" Text="September" Value="9" Owner="radMonthStart">
                                    </telerik:RadComboBoxItem>
                                    <telerik:RadComboBoxItem runat="server" Text="October" Value="10" Owner="radMonthStart">
                                    </telerik:RadComboBoxItem>
                                    <telerik:RadComboBoxItem runat="server" Text="November" Value="11" Owner="radMonthStart">
                                    </telerik:RadComboBoxItem>
                                    <telerik:RadComboBoxItem runat="server" Text="December" Value="12" Owner="radMonthStart">
                                    </telerik:RadComboBoxItem>
                                </items>
                                        </telerik:radcombobox>
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel" UpdateMode="Always">
                                            <ContentTemplate>
                                                <label id="lbyear" runat="server">
                                                </label>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="radMonthStart" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            JOB TITLE</label>
                                        <telerik:radcombobox runat="server" forecolor="#666666"
                                            rendermode="Lightweight" resolvedrendermode="Classic" width="100%" id="cboJobTitle"
                                            autopostback="True" filter="Contains" skin="Bootstrap">
                                        </telerik:radcombobox>
                                    </div>
                                </div>
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            JOB GRADE</label>
                                        <telerik:radcombobox runat="server" forecolor="#666666"
                                            rendermode="Lightweight" resolvedrendermode="Classic" width="100%" id="radJobGrade"
                                            autopostback="True" filter="Contains" skin="Bootstrap">
                                        </telerik:radcombobox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            MINIMUM QUALIFICATION</label>
                                        <telerik:radcombobox runat="server" forecolor="#666666"
                                            rendermode="Lightweight" resolvedrendermode="Classic" width="100%" id="radeducation"
                                            filter="Contains" skin="Bootstrap">
                                        </telerik:radcombobox>
                                    </div>
                                </div>
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            EXPECTED RESUMPTION DATE</label>
                                        <telerik:raddatepicker id="radRecruitDate" runat="server" forecolor="#666666" width="100%"
                                            skin="Bootstrap">
                                            <calendar userowheadersasselectors="False" usecolumnheadersasselectors="False" enableweekends="True"
                                                fastnavigationnexttext="&amp;lt;&amp;lt;" skin="Bootstrap">
                    </calendar>
                                            <dateinput displaydateformat="dd/MM/yyyy" dateformat="dd/MM/yyyy" labelwidth="40%">
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
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>
                                            CURRENT NUMBER OF STAFF</label>
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel13" UpdateMode="Always">
                                            <ContentTemplate>
                                                <input id="acurrentstaffno" runat="server" class="form-control" type="text" disabled="disabled" />
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="radJobgrade" EventName="SelectedIndexChanged" />
                                                <asp:AsyncPostBackTrigger ControlID="cboJobTitle" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>
                                            EXPECTED NUMBER OF STAFF</label>
                                        <telerik:radtextbox id="aexpectedstaffno" runat="server" skin="Bootstrap" autopostback="True"
                                            width="100%" rendermode="Lightweight">
                                        </telerik:radtextbox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>
                                            MINIMUM SALARY</label>
                                        <input id="aminsalary" runat="server" class="form-control" type="text" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>
                                            MAXIMUM SALARY</label>
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel14" UpdateMode="Always">
                                            <ContentTemplate>
                                                <telerik:radtextbox id="amaxsalary" runat="server" skin="Bootstrap" autopostback="True"
                                                    width="100%" rendermode="Lightweight">
                                                </telerik:radtextbox>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="radJobgrade" EventName="SelectedIndexChanged" />
                                                <asp:AsyncPostBackTrigger ControlID="aexpectedstaffno" EventName="TextChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                            <asp:UpdatePanel runat="server" ID="UpdatePanel15" UpdateMode="Always">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label>
                                                    JOB DESCRIPTION*</label>
                                                <textarea id="ajobdesc" runat="server" class="form-control" rows="4" cols="1"></textarea>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label>
                                                    EXPECTED SKILLS*</label>
                                                <textarea id="acompetency" runat="server" class="form-control" rows="4" cols="1"></textarea>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="cboJobTitle" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>
                                            PAYROLL BUDGET</label>
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel12" UpdateMode="Always">
                                            <ContentTemplate>
                                                <telerik:radtextbox id="abudgetpayroll" runat="server" skin="Bootstrap" autopostback="True"
                                                    width="100%" rendermode="Lightweight">
                                                </telerik:radtextbox>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cboJobTitle" EventName="SelectedIndexChanged" />
                                                <asp:AsyncPostBackTrigger ControlID="aexpectedstaffno" EventName="TextChanged" />
                                                <asp:AsyncPostBackTrigger ControlID="amaxsalary" EventName="TextChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                            <asp:UpdatePanel runat="server" ID="UpdatePanel16" UpdateMode="Always">
                                <ContentTemplate>
                                    <div id="divpayrolldesc" runat="server" class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label>
                                                    PAYROLL BUDGET DESCRIPTION</label>
                                                <textarea id="apayrolldesc" runat="server" class="form-control" rows="4" cols="1"
                                                    placeholder="Budget description"></textarea>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="abudgetpayroll" EventName="TextChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>
                                            GRATUITY BUDGET</label>
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel17" UpdateMode="Always">
                                            <ContentTemplate>
                                                <telerik:radtextbox id="abudgetgratuity" runat="server" skin="Bootstrap" autopostback="True"
                                                    width="100%" rendermode="Lightweight">
                                                </telerik:radtextbox>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cboJobTitle" EventName="SelectedIndexChanged" />
                                                <asp:AsyncPostBackTrigger ControlID="aexpectedstaffno" EventName="TextChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                            <asp:UpdatePanel runat="server" ID="UpdatePanel18" UpdateMode="Always">
                                <ContentTemplate>
                                    <div id="divgratuitydesc" runat="server" class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label>
                                                    GRATUITY BUDGET DESCRIPTION</label>
                                                <textarea id="agratuitydesc" runat="server" class="form-control" rows="4" cols="1"
                                                    placeholder="Budget description"></textarea>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="abudgetgratuity" EventName="TextChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>
                                            OTHER EXPENSE BUDGET</label>
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel19" UpdateMode="Always">
                                            <ContentTemplate>
                                                <telerik:radtextbox id="abudgetotherexp" runat="server" skin="Bootstrap" autopostback="True"
                                                    width="100%" rendermode="Lightweight">
                                                </telerik:radtextbox>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cboJobTitle" EventName="SelectedIndexChanged" />
                                                <asp:AsyncPostBackTrigger ControlID="aexpectedstaffno" EventName="TextChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                            <asp:UpdatePanel runat="server" ID="UpdatePanel20" UpdateMode="Always">
                                <ContentTemplate>
                                    <div id="divotherexpdesc" runat="server" class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label>
                                                    OTHER EXPENSE BUDGET DESCRIPTION</label>
                                                <textarea id="aotherexpdesc" runat="server" class="form-control" rows="4" cols="1"
                                                    placeholder="Budget description"></textarea>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="abudgetotherexp" EventName="TextChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>
                                            TRAINING BUDGET</label>
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel21" UpdateMode="Always">
                                            <ContentTemplate>
                                                <telerik:radtextbox id="abudgettraining" runat="server" skin="Bootstrap" autopostback="True"
                                                    width="100%" rendermode="Lightweight">
                                                </telerik:radtextbox>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="radJobgrade" EventName="SelectedIndexChanged" />
                                                <asp:AsyncPostBackTrigger ControlID="cboJobTitle" EventName="SelectedIndexChanged" />
                                                <asp:AsyncPostBackTrigger ControlID="radMonthStart" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                            <asp:UpdatePanel runat="server" ID="UpdatePanel22" UpdateMode="Always">
                                <ContentTemplate>
                                    <div id="divtrainingdesc" runat="server" class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label>
                                                    TRAINING BUDGET DESCRIPTION</label>
                                                <textarea id="atrainingdesc" runat="server" class="form-control" rows="4" cols="1"
                                                    placeholder="Budget description"></textarea>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="abudgettraining" EventName="TextChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <div class="row">
                                <div class="col-md-12 m-t-20 text-center">
                                    <button id="btnupdate" runat="server" onserverclick="btnAdd_Click" type="submit"
                                        style="width: 150px" class="btn btn-primary btn-success ">
                                        Save &amp; Update</button>
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
