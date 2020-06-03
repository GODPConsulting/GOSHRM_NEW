<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="RecruitCandidate.aspx.vb"
    Inherits="GOSHRM.RecruitCandidate" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
        <form id="form1" action="">
        <table width="100%">
            <tr>
                <td style="width: 15%">
                    <input type="hidden" id="lefttime" value="60:00" runat="server" />
                    <input type="hidden" id="statusid" value="0" runat="server" />
                </td>
                <td style="width: 70%">
                    <div class="row">
                        <div id="divalert" runat="server" visible="false" class="alert alert-info">
                            <strong id="msgalert" runat="server">Danger!</strong>
                            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                <span aria-hidden="true">&times;</span></button>
                        </div>
                    </div>
                    <div class="row">
                        <h5 id="pagetitle" runat="server" class="page-title" style="color: #1BA691">
                            Recruitment</h5>
                    </div>
                    <div class="row">
                        <div id="divrecruitment" runat="server" class="pull-left" style="width: 100%">
                            <p>
                                <a href="JobInterviews"><u>Interviews</u></a>
                                <label>
                                    >
                                </label>
                                <a href="Interviewees"><u>Interview Shortlists</u></a>
                                <label>
                                    >
                                </label>
                                <a href="InterviewHRDetail"><u>Candidate</u></a>
                                <label>
                                    >
                                </label>
                                <a href="#">Recruitment</a>
                            </p>
                        </div>
                    </div>
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <b>PERSONAL INFORMATION</b>
                        </div>
                        <div class="panel-body">
                            <%--<div id="photo" runat="server" class="row">
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <asp:Image ID="imgProfile" runat="server" ImageUrl="~/images/blank-avatar.jpg" Height="120px"
                                            CssClass="avatar" Width="120px" />
                                        <input class="form-control" type="file" id="imgUpload" runat="server" />
                                        <asp:Button ID="btnupload" runat="server" Text="Upload Photo" BackColor="#33CC33"
                                            ForeColor="White" BorderStyle="None" Font-Names="Verdana" Font-Size="12px" 
                                            Font-Bold="True" />
                                    </div>
                                </div>
                            </div>--%>
                            <div class="row">
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <label>
                                            LASTNAME</label>
                                        <input id="alastname" runat="server" class="form-control" type="text" placeholder="Surname" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <label>
                                            OTHER NAMES</label>
                                        <input id="aothernames" runat="server" class="form-control" type="text" placeholder="Firstname Middlename" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            GENDER</label>
                                        <telerik:RadComboBox ID="cbogender" runat="server" CheckBoxes="False" RenderMode="Lightweight"
                                            Width="100%" Sort="Ascending" ForeColor="#666666" Skin="Bootstrap">
                                        </telerik:RadComboBox>
                                    </div>
                                </div>
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            MARITAL STATUS</label>
                                        <telerik:RadComboBox ID="cbomaritalstat" runat="server" CheckBoxes="False" RenderMode="Lightweight"
                                            Width="100%" Sort="Ascending" ForeColor="#666666" Skin="Bootstrap">
                                        </telerik:RadComboBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            NATIONALITY</label>
                                        <telerik:RadComboBox ID="cbonationality" runat="server" CheckBoxes="False" Filter="Contains"
                                            RenderMode="Lightweight" Width="100%" Sort="Ascending" ForeColor="#666666" Skin="Bootstrap">
                                        </telerik:RadComboBox>
                                    </div>
                                </div>
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            COUNTRY OF BIRTH</label>
                                        <telerik:RadComboBox ID="cbocountrybirth" runat="server" CheckBoxes="False" Filter="Contains"
                                            RenderMode="Lightweight" Width="100%" Sort="Ascending" ForeColor="#666666" Skin="Bootstrap">
                                        </telerik:RadComboBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            DATE OF BIRTH</label>
                                        <telerik:RadDatePicker Skin="Bootstrap" runat="server" MinDate="1900-01-01" Culture="en-US" RenderMode="Lightweight"
                                            ForeColor="#666666" Width="100%" ResolvedRenderMode="Classic" ID="datDOB">
                                            <calendar userowheadersasselectors="False" usecolumnheadersasselectors="False" enableweekends="True"
                                                fastnavigationnexttext="&amp;lt;&amp;lt;" rendermode="Lightweight" skin="Bootstrap">
                                        </calendar>
                                            <dateinput width="" displaydateformat="dd/MM/yyyy" dateformat="dd/MM/yyyy" labelwidth="40%"
                                                rendermode="Lightweight">
                                            <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                            <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                                            <FocusedStyle Resize="None"></FocusedStyle>
                                            <DisabledStyle Resize="None"></DisabledStyle>
                                            <InvalidStyle Resize="None"></InvalidStyle>
                                            <HoveredStyle Resize="None"></HoveredStyle>
                                            <EnabledStyle Resize="None"></EnabledStyle>
                                        </dateinput>
                                            <datepopupbutton cssclass="" imageurl="" hoverimageurl=""></datepopupbutton>
                                        </telerik:RadDatePicker>
                                    </div>
                                </div>
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            RESUMPTION DATE</label>
                                        <telerik:RadDatePicker runat="server" Skin="Bootstrap" MinDate="1900-01-01" Culture="en-US" RenderMode="Lightweight"
                                            ForeColor="#666666" Width="100%" ResolvedRenderMode="Classic" ID="datDateJoined">
                                            <calendar userowheadersasselectors="False" usecolumnheadersasselectors="False" enableweekends="True"
                                                fastnavigationnexttext="&amp;lt;&amp;lt;" Skin="Bootstrap" rendermode="Lightweight">
                                        </calendar>
                                            <dateinput width="" displaydateformat="dd/MM/yyyy" dateformat="dd/MM/yyyy" labelwidth="40%"
                                                rendermode="Lightweight">
                                            <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                            <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                                            <FocusedStyle Resize="None"></FocusedStyle>
                                            <DisabledStyle Resize="None"></DisabledStyle>
                                            <InvalidStyle Resize="None"></InvalidStyle>
                                            <HoveredStyle Resize="None"></HoveredStyle>
                                            <EnabledStyle Resize="None"></EnabledStyle>
                                        </dateinput>
                                            <datepopupbutton cssclass="" imageurl="" hoverimageurl=""></datepopupbutton>
                                        </telerik:RadDatePicker>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <b>CONTACT INFORMATION</b>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>
                                            RESIDENT ADDRESS</label>
                                        <textarea id="aaddress" runat="server" class="form-control" rows="4" cols="1" placeholder="Residential Address"></textarea>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            CITY</label>
                                        <input id="acity" runat="server" class="form-control" type="text" placeholder="City of Residence" />
                                    </div>
                                </div>
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            COUNTRY</label>
                                        <telerik:RadComboBox ID="cbocountry" runat="server" CheckBoxes="False" Filter="Contains"
                                            RenderMode="Lightweight" Width="100%" Sort="Ascending" ForeColor="#666666" Skin="Bootstrap">
                                        </telerik:RadComboBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            EMAIL ADDRESS</label>
                                        <input id="aemailaddress" runat="server" class="form-control" type="text" placeholder="Personal Email Address" />
                                    </div>
                                </div>
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            PHONE NUMBER</label>
                                        <input id="aphonenumber" runat="server" class="form-control" type="text" placeholder="Contact Number" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <b>EMERGENCY CONTACT</b>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <label>
                                            NAME</label>
                                        <input id="aemername" runat="server" class="form-control" type="text" placeholder="Full Name" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>
                                            RESIDENT ADDRESS</label>
                                        <textarea id="aemeraddress" runat="server" class="form-control" rows="4" cols="1"
                                            placeholder="Residential Address"></textarea>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            PHONE NUMBER</label>
                                        <input id="aemernumber" runat="server" class="form-control" type="text" placeholder="Contact Number" />
                                    </div>
                                </div>
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            RELATIONSHIP</label>
                                        <telerik:RadComboBox ID="cboemerrelationship" runat="server" CheckBoxes="False" RenderMode="Lightweight"
                                            Width="100%" Sort="Ascending" ForeColor="#666666" Skin="Bootstrap">
                                        </telerik:RadComboBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <b>REFEREES</b>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <label>
                                            1. NAME</label>
                                        <input id="arefname1" runat="server" class="form-control" type="text" placeholder="Full Name" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>
                                            ORGANISATION</label>
                                        <textarea id="arefaddr1" runat="server" class="form-control" rows="4" cols="1" placeholder="Place Work and Address"></textarea>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            POSITION
                                        </label>
                                        <input id="arefposition1" runat="server" class="form-control" type="text" placeholder="Position at work" />
                                    </div>
                                </div>
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            PHONE NUMBER</label>
                                        <input id="arefphone1" runat="server" class="form-control" type="text" placeholder="Contact Number" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            EMAIL ADDRESS
                                        </label>
                                        <input id="arefemail1" runat="server" class="form-control" type="text" placeholder="Personal or Official Email Address" />
                                    </div>
                                </div>
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            YEARS KNOWN</label>
                                        <input id="arefyears1" runat="server" class="form-control" type="text" placeholder="Number of years known to the applicant" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <label>
                                            CONFIRMED
                                        </label>
                                        <telerik:RadComboBox ID="cborefconfirmed1" runat="server" CheckBoxes="False" RenderMode="Lightweight"
                                            Width="100%" Sort="Ascending" ForeColor="#666666" Skin="Bootstrap">
                                        </telerik:RadComboBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <label>
                                            2. NAME</label>
                                        <input id="arefname2" runat="server" class="form-control" type="text" placeholder="Full Name" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>
                                            ORGANISATION</label>
                                        <textarea id="arefaddr2" runat="server" class="form-control" rows="4" cols="1" placeholder="Place Work and Address"></textarea>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            POSITION
                                        </label>
                                        <input id="arefposition2" runat="server" class="form-control" type="text" placeholder="Position at work" />
                                    </div>
                                </div>
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            PHONE NUMBER</label>
                                        <input id="arefphone2" runat="server" class="form-control" type="text" placeholder="Contact Number" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            EMAIL ADDRESS
                                        </label>
                                        <input id="arefemail2" runat="server" class="form-control" type="text" placeholder="Personal or Official Email Address" />
                                    </div>
                                </div>
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            YEARS KNOWN</label>
                                        <input id="arefyears2" runat="server" class="form-control" type="text" placeholder="Number of years known to the applicant" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <label>
                                            CONFIRMED
                                        </label>
                                        <telerik:RadComboBox ID="cborefconfirmed2" runat="server" CheckBoxes="False" RenderMode="Lightweight"
                                            Width="100%" Sort="Ascending" ForeColor="#666666" Skin="Bootstrap">
                                        </telerik:RadComboBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <b>JOB INFORMATION</b>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <label>
                                            CLIENT COMPANY RECRUIMENT</label>
                                        <telerik:RadComboBox runat="server" RenderMode="Lightweight"
                                            ResolvedRenderMode="Classic" Width="100%" ID="cborecruit" AutoPostBack="True"
                                            ForeColor="#666666" Skin="Bootstrap" ToolTip="is recruitment for a third party client"
                                            EmptyMessage="--Select--">
                                            <items>
                                            <telerik:RadComboBoxItem runat="server" Text="No" Value="No" />
                                            <telerik:RadComboBoxItem runat="server" Text="Yes" Value="Yes" />
                                        </items>
                                        </telerik:RadComboBox>
                                    </div>
                                </div>
                            </div>
                            <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Always">
                                <ContentTemplate>
                                    <div id="divworkclient" runat="server" class="row">
                                        <div class=" col-md-12">
                                            <div class="form-group">
                                                <label id="lbcompany" runat="server">
                                                    COMPANY</label>
                                                <telerik:RadComboBox runat="server" RenderMode="Lightweight"
                                                    Filter="Contains" ResolvedRenderMode="Classic" Width="100%" ID="cbocompany" AutoPostBack="True"
                                                    ForeColor="#666666" Skin="Bootstrap" EmptyMessage="--Select--">
                                                </telerik:RadComboBox>
                                            </div>
                                        </div>
                                        <div class=" col-md-12">
                                            <div class="form-group">
                                                <label>
                                                    EMPLOYEE NUMBER*</label>
                                                <input id="aempid" runat="server" class="form-control" type="text" placeholder="Employee Number" />
                                                <button id="btngenerate" runat="server" type="submit" onserverclick="btngenerate_Click"
                                                    class="btn btn-success">
                                                    <i class="fa fa-spinner"></i>Generate Number</button>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="cborecruit" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Always">
                                <ContentTemplate>
                                    <div id="divwork" runat="server">
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel4" UpdateMode="Always">
                                            <ContentTemplate>
                                                <div class="row">
                                                    <div class=" col-md-12">
                                                        <div class="form-group">
                                                            <label>
                                                                OFFICE*</label>
                                                            <telerik:RadComboBox ID="cbojoboffice" runat="server" CheckBoxes="False" Filter="Contains"
                                                                RenderMode="Lightweight" Width="100%" Sort="Ascending" AutoPostBack="true" ForeColor="#666666"
                                                                Skin="Bootstrap">
                                                            </telerik:RadComboBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cboworkcountry" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                        <div class="row">
                                            <div class=" col-md-6">
                                                <div class="form-group">
                                                    <label>
                                                        JOB TITLE*</label>
                                                    <telerik:RadComboBox runat="server" DropDownAutoWidth="Enabled" RenderMode="Lightweight"
                                                        ResolvedRenderMode="Classic" Width="100%" ID="cbojobtitle" Filter="Contains"
                                                        ForeColor="#666666" Skin="Bootstrap" EmptyMessage="--Select--">
                                                    </telerik:RadComboBox>
                                                </div>
                                            </div>
                                            <div class=" col-md-6">
                                                <div class="form-group">
                                                    <label>
                                                        JOB GRADE*</label>
                                                    <telerik:RadComboBox runat="server" DropDownAutoWidth="Enabled" RenderMode="Lightweight"
                                                        ResolvedRenderMode="Classic" Width="100%" ID="cbojobgrade" Filter="Contains"
                                                        AutoPostBack="true" ForeColor="#666666" Skin="Bootstrap" EmptyMessage="--Select--">
                                                    </telerik:RadComboBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class=" col-md-6">
                                                <div class="form-group">
                                                    <label>
                                                        JOB STATUS*</label>
                                                    <telerik:RadComboBox runat="server" DropDownAutoWidth="Enabled" RenderMode="Lightweight"
                                                        ResolvedRenderMode="Classic" Width="100%" ID="cbojobstatus" Filter="Contains"
                                                        ForeColor="#666666" Skin="Bootstrap" EmptyMessage="--Select--">
                                                    </telerik:RadComboBox>
                                                </div>
                                            </div>
                                            <div class=" col-md-6">
                                                <div class="form-group">
                                                    <label>
                                                        SHIFT*</label>
                                                    <telerik:RadComboBox runat="server" DropDownAutoWidth="Enabled" RenderMode="Lightweight"
                                                        ResolvedRenderMode="Classic" Width="100%" ID="cboshift" Filter="Contains" AutoPostBack="true"
                                                        ForeColor="#666666" Skin="Bootstrap" EmptyMessage="--Select--">
                                                    </telerik:RadComboBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class=" col-md-6">
                                                <div class="form-group">
                                                    <label>
                                                        WORK EMAIL ADDRESS</label>
                                                    <input id="aworkemail" runat="server" class="form-control" type="text" placeholder="Official Email Address" />
                                                </div>
                                            </div>
                                            <div class=" col-md-6">
                                                <div class="form-group">
                                                    <label>
                                                        COUNTRY*</label>
                                                    <telerik:RadComboBox ID="cboworkcountry" runat="server" CheckBoxes="False" Filter="Contains"
                                                        RenderMode="Lightweight" Width="100%" Sort="Ascending" AutoPostBack="true" ForeColor="#666666"
                                                        Skin="Bootstrap">
                                                    </telerik:RadComboBox>
                                                </div>
                                            </div>
                                        </div>
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Always">
                                            <ContentTemplate>
                                                <div class="row">
                                                    <div class=" col-md-12">
                                                        <div class="form-group">
                                                            <label>
                                                                LOCATION*</label>
                                                            <telerik:RadComboBox ID="cboworklocation" runat="server" CheckBoxes="False" Filter="Contains"
                                                                RenderMode="Lightweight" Width="100%" Sort="Ascending" AutoPostBack="true" ForeColor="#666666"
                                                                Skin="Bootstrap">
                                                            </telerik:RadComboBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cboworkcountry" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel7" UpdateMode="Always">
                                            <ContentTemplate>
                                                <div class="row">
                                                    <div class=" col-md-12">
                                                        <div class="form-group">
                                                            <label>
                                                                REPORTS TO*</label>
                                                            <telerik:RadComboBox ID="cboreportsto" runat="server" CheckBoxes="False" Filter="Contains"
                                                                RenderMode="Lightweight" Width="100%" Sort="Ascending" ForeColor="#666666" Skin="Bootstrap">
                                                            </telerik:RadComboBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class=" col-md-12">
                                                        <div class="form-group">
                                                            <label>
                                                                FIRST PERFORMANCE REVIEWER*</label>
                                                            <telerik:RadComboBox ID="cboreviewer1" runat="server" CheckBoxes="False" Filter="Contains"
                                                                RenderMode="Lightweight" Width="100%" Sort="Ascending" ForeColor="#666666" Skin="Bootstrap">
                                                            </telerik:RadComboBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class=" col-md-12">
                                                        <div class="form-group">
                                                            <label>
                                                                SECOND PERFORMANCE REVIEWER*</label>
                                                            <telerik:RadComboBox ID="cboreviewer2" runat="server" CheckBoxes="False" Filter="Contains"
                                                                RenderMode="Lightweight" Width="100%" Sort="Ascending" ForeColor="#666666" Skin="Bootstrap">
                                                            </telerik:RadComboBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cbojobgrade" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                        <div class="row">
                                            <div class=" col-md-12">
                                                <div class="form-group">
                                                    <asp:CheckBox runat="server" AutoPostBack="True" Text="Create GOSHRM Login Profile"
                                                        ForeColor="#666666" Font-Size="13px" ID="chkLogin"></asp:CheckBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class=" col-md-12">
                                                <div class="form-group">
                                                    <asp:CheckBox runat="server" AutoPostBack="True" Text="Upload Profile Photo (Save Personal Details Before Uploading Picture)"
                                                        ForeColor="#666666" Font-Size="13px" ID="Chkphoto"></asp:CheckBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="cborecruit" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <asp:UpdatePanel runat="server" ID="UpdatePanel5" UpdateMode="Always">
                        <ContentTemplate>
                            <div id="divlogin" runat="server" class="panel panel-success">
                                <div class="panel-heading">
                                    <b>GOSHRM LOGIN PROFILE</b>
                                </div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class=" col-md-6">
                                            <div class="form-group">
                                                <label>
                                                    USERNAME*
                                                </label>
                                                <input id="ausername" runat="server" class="form-control" type="text" placeholder="Username" />
                                            </div>
                                        </div>
                                        <div class=" col-md-6">
                                            <div class="form-group">
                                                <label>
                                                    PASSWORD*</label>
                                                <input id="apassword" runat="server" class="form-control" type="password" placeholder="Temporary password" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class=" col-md-6">
                                            <div class="form-group">
                                                <label>
                                                    USER ROLE*
                                                </label>
                                                <telerik:RadComboBox ID="cbouserrole" runat="server" CheckBoxes="False" Filter="Contains"
                                                    RenderMode="Lightweight" Width="100%" Sort="Ascending" ForeColor="#666666" Skin="Bootstrap">
                                                </telerik:RadComboBox>
                                            </div>
                                        </div>
                                        <div class=" col-md-6">
                                            <div class="form-group">
                                                <label>
                                                    STATUS*</label>
                                                <telerik:RadComboBox ID="cbouserstat" runat="server" CheckBoxes="False" Filter="Contains"
                                                    RenderMode="Lightweight" Width="100%" Sort="Ascending" ForeColor="#666666" Skin="Bootstrap">
                                                </telerik:RadComboBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                     <div class=" col-md-6">
                                            <div class="form-group">
                                                <label>
                                                    ACCESS LEVEL*</label>
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel6" UpdateMode="Always">
                                                    <ContentTemplate>
                                                        <telerik:RadComboBox ID="cbouseraccesslevel" runat="server"  CheckBoxes="False" Filter="Contains"
                                                            RenderMode="Lightweight" Width="100%" Sort="Ascending" AutoPostBack="true" ForeColor="#666666" 
                                                            Skin="Bootstrap">
                                                        </telerik:RadComboBox>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="cbouseraccess" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class=" col-md-6">
                                            <div class="form-group">
                                                <label>
                                                    ACCESS*
                                                </label>
                                                <telerik:RadComboBox ID="cbouseraccess" runat="server" CheckBoxes="true" Filter="Contains"
                                                    RenderMode="Lightweight" Width="100%" Sort="Ascending" ForeColor="#666666" AutoPostBack="true"
                                                    Skin="Bootstrap">
                                                </telerik:RadComboBox>
                                            </div>
                                        </div>
                                       
                                    </div>
                                    <div class="row">
                                        <div class=" col-md-4">
                                            <div class="form-group">
                                                <input type="checkbox" id="chksuperadmin" runat="server"/>Is Super Administrator
                                            </div>
                                        </div>
                                        <div class=" col-md-4">
                                            <div class="form-group">
                                                <input type="checkbox" id="chkhradmin" runat="server"/>Is HR Administrator
                                            </div>
                                        </div>
                                        <div class=" col-md-4">
                                            <div class="form-group">
                                                <input type="checkbox" id="chkfinadmin" runat="server"/>Is Finance Administrator
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="chkLogin" EventName="CheckedChanged" />
                        </Triggers>
                    </asp:UpdatePanel>

                     <asp:UpdatePanel runat="server" ID="UpdatePanel8" UpdateMode="Always">
                        <ContentTemplate>
                    <div id="photo" runat="server" class="panel panel-success">
                        <div class="panel-heading">
                            <b>UPLOAD PROFILE PICTURE</b>
                        </div>
                        <div class="panel-body">
                            <div runat="server" class="row">
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <asp:Image ID="imgProfile" runat="server" ImageUrl="~/images/blank-avatar.jpg" Height="120px"
                                            CssClass="avatar" Width="120px" />
                                        <input class="form-control" type="file" id="imgUpload" runat="server" />
                                        <asp:Button ID="btnupload" runat="server" Text="Upload Photo" BackColor="#33CC33"
                                            ForeColor="White" BorderStyle="None" Font-Names="Verdana" Font-Size="12px" 
                                            Font-Bold="True" />
                                    </div>
                                </div>
                            </div>
                            </div>
                            </div>
                             </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Chkphoto" EventName="CheckedChanged" />
                        </Triggers>
                    </asp:UpdatePanel>

                    <div class="row">
                        <div class="col-md-12 m-t-20">
                            <button id="btnupdate" runat="server" type="submit" onserverclick="btnSave_Click"
                                style="width: 150px" class="btn btn-success">
                                <i class="fa fa-save"></i>Save</button>
                        </div>
                        <asp:Label ID="lblIDContact" runat="server" Font-Names="Verdana" Font-Size="1px"
                            Visible="False" Width="0px"></asp:Label>
                        <asp:Label ID="lblIDEmp" runat="server" Font-Names="Verdana" Font-Size="1px" Visible="False"
                            Width="0px"></asp:Label>
                        <asp:Label ID="lblIDEmer" runat="server" Font-Names="Verdana" Font-Size="1px" Visible="False"
                            Width="0px"></asp:Label>
                        <asp:Label ID="lblIDJob" runat="server" Font-Names="Verdana" Font-Size="1px" Visible="False"></asp:Label>
                        <asp:TextBox runat="server" BorderColor="#CCCCCC" Font-Size="1px" ID="txtFirstName"></asp:TextBox>
                        <asp:TextBox runat="server" BorderColor="#CCCCCC" Font-Size="1px" ID="txtmiddlename"></asp:TextBox>
                        <asp:TextBox runat="server" Font-Size="1px" Visible="false" ID="txtimage"></asp:TextBox>
                        <telerik:RadListBox ID="lstLang" runat="server" ResolvedRenderMode="Classic" BorderStyle="None"
                            ForeColor="#666666" Enabled="False" Width="1%" EmptyMessage="No data" RenderMode="Lightweight"
                            Sort="Ascending" Font-Size="1px" Visible="False">
                            <buttonsettings transferbuttons="All"></buttonsettings>
                            <emptymessagetemplate>
                                    No Languages
                                </emptymessagetemplate>
                        </telerik:RadListBox>
                        <telerik:RadListBox ID="lstSkills" runat="server" ResolvedRenderMode="Classic" BorderStyle="None"
                            Enabled="False" Width="1%" EmptyMessage="No data" RenderMode="Lightweight" Sort="Ascending"
                            Font-Names="Verdana" Font-Size="1px" Visible="False">
                            <buttonsettings transferbuttons="All"></buttonsettings>
                            <emptymessagetemplate>
                                    No Languages
                                </emptymessagetemplate>
                        </telerik:RadListBox>
                    </div>
                </td>
                <td style="width: 15%">
                </td>
            </tr>
        </table>
        </form>
    </body>
    </html>
</asp:Content>
