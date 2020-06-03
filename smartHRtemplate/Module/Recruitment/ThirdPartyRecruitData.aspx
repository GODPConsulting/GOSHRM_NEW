<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="ThirdPartyRecruitData.aspx.vb"
    Inherits="GOSHRM.ThirdPartyRecruitData" EnableEventValidation="false" Debug="true" %>
    <asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Employee Detail</title>
    <script type="text/javascript">

        function closeWin() {
            popup.close();   // Closes the new window
        }

    </script>
    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to delete data?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
    <style type="text/css">
        td
        {
            cursor: pointer;
        }
        .style29
        {
        }
        .style28
        {
            width: 812px;
            font-size: small;
            font-family: Candara;
        }
        .style30
        {
            width: 810px;
        }
        .style33
        {
            width: 639px;
        }
        .style31
        {
            font-size: small;
            border-left-color: #A0A0A0;
            border-right-color: #C0C0C0;
            border-top-color: #A0A0A0;
            border-bottom-color: #C0C0C0;
            padding: 1px;
        }
        .style34
        {
        }
        .style44
        {
            color: #FF3300;
            font-size: small;
        }
        .style45
        {
            width: 291px;
        }
        .style40
        {
            width: 42px;
        }
        .style37
        {
            width: 240px;
        }
        .style41
        {
            width: 44px;
        }
        
        .style43
        {
            color: #FF3300;
        }
        .style46
        {
            width: 300px;
        }
        .style50
        {
            width: 720px;
        }
        .style53
        {
            width: 32px;
        }
        .style54
        {
            width: 303px;
        }
        .style55
        {
            width: 103px;
        }
        .style56
        {
            width: 104px;
        }
        .style57
        {
            width: 10px;
        }
        .style58
        {
            width: 11px;
        }
        .style63
        {
            width: 1008px;
        }
        .style65
        {
            color: #FFFFFF;
            font-weight: bold;
            border-left-color: #A0A0A0;
            border-right-color: #C0C0C0;
            border-top-color: #A0A0A0;
            border-bottom-color: #C0C0C0;
            padding: 1px;
            background-color: #1BA691;
        }
        .modal
        {
            position: fixed;
            z-index: 999;
            height: 100%;
            width: 100%;
            top: 0;
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
            -moz-opacity: 0.8;
        }
        .center
        {
            z-index: 1000;
            margin: 300px auto;
            padding: 10px;
            width: 130px;
            background-color: White;
            border-radius: 10px;
            filter: alpha(opacity=100);
            opacity: 1;
            -moz-opacity: 1;
        }
        .style69
        {
            width: 290px;
        }
        .style70
        {
            width: 266px;
        }
        .style72
        {
            width: 236px;
        }
        .style75
        {
            width: 302px;
        }
        .style76
        {
            width: 23px;
        }
        .style78
        {
            width: 153px;
        }
        .style79
        {
            width: 139px;
        }
        .style80
        {
            width: 161px;
        }
        .style82
        {
            width: 155px;
        }
        .style83
        {
            width: 316px;
        }
        .style84
        {
            color: #FFFFFF;
        }
    </style>
</head>
<body>
    <form>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.4/jquery.min.js" type="text/javascript"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js"
        type="text/javascript"></script>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css"
        rel="Stylesheet" type="text/css" />
    <div>
        <table class="style21">
            <tr>
                <td class="style84" bgcolor="#6699FF" colspan="6">
                    <strong>Third Party Employee</strong></td>
            </tr>
            <tr>
                <td class="style29">
                </td>
                <td class="style28">
                </td>
                <td class="style54">
                </td>
                <td class="style54">
                </td>
                <td class="style54">
                </td>
                <td class="style54">
                </td>
            </tr>
            <tr>
                <td class="style29">
                </td>
                <td class="style28">
                    <asp:Label ID="lblView" runat="server" Font-Names="Candara" Font-Size="Medium"></asp:Label>
                </td>
                <td class="style54">
                </td>
            </tr>
            <tr>
                <td class="style29">
                </td>
                <td class="style28">
                </td>
                <td>
                </td>
            </tr>
        </table>
    </div>
    <div>
        <table class="style21">
            <tr>
                <td>
                </td>
                <td class="style30">
                    <asp:Image ID="imgProfile" runat="server" ImageUrl="~/images/user_male.png" Height="150px"
                        Width="150px" />
                    <asp:Button ID="btnImage" runat="server" Text="Upload Profile Image" BackColor="#1BA691"
                        ForeColor="White" Width="20%" Height="20px" BorderStyle="None" Style="margin-top: 0px" />
                    <asp:Button ID="btnImage0" runat="server" Text="Delete Profile Image" BackColor="#FF9933"
                        ForeColor="White" Width="20%" Height="20px" BorderStyle="None" Style="margin-top: 0px" />
                </td>
                <td class="style33">
                    <asp:Image ID="imgClear" runat="server" ImageUrl="~/images/user_male.png" Height="150px"
                        Width="150px" Visible="False" />
                </td>
            </tr>
            <tr>
                <td class="style29">
                </td>
                <td class="style28">
                    <asp:FileUpload ID="imgUpload" runat="server" ToolTip="Browse Photo" />
                </td>
                <td>
                    <asp:TextBox ID="txtImage" runat="server" Visible="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style29">
                </td>
                <td class="style30" valign="top">
                    &nbsp;</td>
                <td class="style33">
                    <asp:Label runat="server" Font-Bold="True" Font-Names="Candara" Font-Size="Small"
                        ForeColor="#FF3300" ID="lblstatus"></asp:Label>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td class="style29">
                </td>
                <td class="style28">
                    <asp:TextBox ID="txtEmpID" runat="server" Width="186px" Style="font-size: medium;
                        font-family: Candara" Font-Names="Candara" Font-Size="Small" BorderColor="#CCCCCC"
                        BorderWidth="1px" Height="5px" Visible="False"></asp:TextBox>
                </td>
                <td>
                </td>
            </tr>
        </table>
    </div>
    <div style="height: 163px">
        <div>
            <ajaxToolkit:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Height="604px"
                Width="100%" Font-Names="Candara" Font-Size="Small">
                <ajaxToolkit:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel1" OnDemandMode="Always">
                    <HeaderTemplate>
                        Personal Details</HeaderTemplate>
                    <ContentTemplate>
                        <table width="100%">
                            <tr>
                                <td class="style1" colspan="2">
                                    <asp:TextBox ID="txtID" runat="server" Font-Names="Candara" Font-Size="Small" Style="font-size: small;
                                        font-family: Candara" Visible="False" Width="238px" Height="16px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="style72">
                                    <span class="style31">First Name </span><span class="style44">*</span>
                                </td>
                                <td class="style45">
                                    <asp:TextBox ID="txtFirstName" runat="server" Font-Names="Candara" Font-Size="Small"
                                        Style="font-size: small; font-family: Candara" Width="100%" BorderColor="#CCCCCC"
                                        BorderWidth="1px"></asp:TextBox>
                                </td>
                                <td class="style40">
                                </td>
                                <td class="style37">
                                    Middle Name
                                </td>
                                <td class="style37">
                                    <asp:TextBox ID="txtMidName" runat="server" Font-Names="Candara" Font-Size="Medium"
                                        Style="font-size: small; font-family: Candara" Width="100%" BorderColor="#CCCCCC"
                                        BorderWidth="1px"></asp:TextBox>
                                </td>
                                <td class="style41">
                                </td>
                                <td class="style37">
                                    Last Name <span class="style43">*</span>
                                </td>
                                <td class="style37">
                                    <asp:TextBox ID="txtLastName" runat="server" Font-Names="Candara" Font-Size="Medium"
                                        Style="font-size: small; font-family: Candara" Width="100%" BorderColor="#CCCCCC"
                                        BorderWidth="1px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="style72">
                                    Gender <span class="style43">*</span>
                                </td>
                                <td class="style45">
                                    <telerik:RadDropDownList ID="radGender" runat="server" BackColor="White" DefaultMessage="-- Select --"
                                        DropDownHeight="50px" Font-Names="Candara" Font-Size="Medium" Height="73px" RenderMode="Lightweight"
                                        ResolvedRenderMode="Classic" Style="font-size: small; font-family: Candara" Width="100%">
                                    </telerik:RadDropDownList>
                                </td>
                                <td class="style40">
                                </td>
                                <td class="style37">
                                    Marital Status <span class="style43">*</span>
                                </td>
                                <td class="style37">
                                    <telerik:RadDropDownList ID="radMaritalStatus" runat="server" BackColor="White" DefaultMessage="-- Select --"
                                        DropDownHeight="200px" Font-Names="Candara" Font-Size="Medium" Height="73px"
                                        RenderMode="Lightweight" ResolvedRenderMode="Classic" Style="font-size: small;
                                        font-family: Candara" Width="100%">
                                    </telerik:RadDropDownList>
                                </td>
                                <td class="style41">
                                </td>
                                <td class="style37">
                                    Nationality <span class="style43">*</span>
                                </td>
                                <td class="style37">
                                    <telerik:RadDropDownList ID="radNationality" runat="server" BackColor="White" DefaultMessage="-- Select --"
                                        DropDownHeight="200px" Font-Names="Candara" Font-Size="Medium" Height="52px"
                                        RenderMode="Lightweight" ResolvedRenderMode="Classic" Style="font-size: small;
                                        font-family: Candara" Width="238px">
                                    </telerik:RadDropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="style72">
                                    Date of Birth <span class="style43">*</span>
                                </td>
                                <td class="style45">
                                    <telerik:RadDatePicker ID="radDOB" runat="server" Culture="en-US" Height="20px" MinDate="1900-01-01"
                                        ResolvedRenderMode="Classic" Width="55%" RenderMode="Lightweight">
                                        <Calendar EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;" UseColumnHeadersAsSelectors="False"
                                            UseRowHeadersAsSelectors="False" RenderMode="Lightweight">
                                        </Calendar>
                                        <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" Height="20px" LabelWidth="40%"
                                            Width="" RenderMode="Lightweight">
                                            <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                            <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                                            <FocusedStyle Resize="None"></FocusedStyle>
                                            <DisabledStyle Resize="None"></DisabledStyle>
                                            <InvalidStyle Resize="None"></InvalidStyle>
                                            <HoveredStyle Resize="None"></HoveredStyle>
                                            <EnabledStyle Resize="None"></EnabledStyle>
                                        </DateInput><DatePopupButton CssClass="" HoverImageUrl="" ImageUrl=""></DatePopupButton>
                                    </telerik:RadDatePicker>
                                </td>
                                <td class="style40">
                                </td>
                                <td class="style37">
                                    Blood Group <span class="style43">*</span>
                                </td>
                                <td class="style37">
                                    <asp:TextBox ID="txtBloodGrp" runat="server" BorderColor="#CCCCCC" BorderWidth="1px"
                                        Font-Names="Candara" Font-Size="Medium" Style="font-size: small; font-family: Candara"
                                        Width="100%"></asp:TextBox>
                                </td>
                                <td class="style41">
                                </td>
                                <td class="style37">
                                    State of Origin
                                </td>
                                <td class="style37">
                                    <asp:TextBox ID="txtStateOrigin" runat="server" Font-Names="Candara" Font-Size="Medium"
                                        Style="font-size: small; font-family: Candara" Width="100%" BorderColor="#CCCCCC"
                                        BorderWidth="1px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="style72">
                                    Identification Type <span class="style43">*</span>
                                </td>
                                <td class="style45">
                                    <asp:TextBox ID="txtIDType" runat="server" BorderColor="#CCCCCC" BorderWidth="1px"
                                        Font-Names="Candara" Font-Size="Medium" Style="font-size: small; font-family: Candara"
                                        Width="100%"></asp:TextBox>
                                </td>
                                <td class="style40">
                                </td>
                                <td class="style37">
                                    Identification Number <span class="style43">*</span>
                                </td>
                                <td class="style37">
                                    <asp:TextBox ID="txtIDNo" runat="server" Font-Names="Candara" Font-Size="Medium"
                                        Style="font-size: small; font-family: Candara" Width="100%" BorderColor="#CCCCCC"
                                        BorderWidth="1px"></asp:TextBox>
                                </td>
                                <td class="style41">
                                </td>
                                <td class="style37">
                                    ID Expiry Date <span class="style43">*</span>
                                </td>
                                <td class="style37">
                                    <telerik:RadDatePicker ID="radIDExpiry" runat="server" Culture="en-US" Height="16px"
                                        MinDate="" ResolvedRenderMode="Classic" Width="55%" 
                                        RenderMode="Lightweight">
                                        <Calendar EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;" UseColumnHeadersAsSelectors="False"
                                            UseRowHeadersAsSelectors="False" RenderMode="Lightweight">
                                        </Calendar>
                                        <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" Height="16px" LabelWidth="40%"
                                            Width="" RenderMode="Lightweight">
                                            <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                            <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                                            <FocusedStyle Resize="None"></FocusedStyle>
                                            <DisabledStyle Resize="None"></DisabledStyle>
                                            <InvalidStyle Resize="None"></InvalidStyle>
                                            <HoveredStyle Resize="None"></HoveredStyle>
                                            <EnabledStyle Resize="None"></EnabledStyle>
                                        </DateInput><DatePopupButton CssClass="" HoverImageUrl="" ImageUrl=""></DatePopupButton>
                                    </telerik:RadDatePicker>
                                </td>
                            </tr>
                            <tr>
                                <td class="style72">
                                    ID Issuer
                                </td>
                                <td class="style45">
                                    <asp:TextBox ID="txtIDIssuer" runat="server" Font-Names="Candara" Font-Size="Medium"
                                        Style="font-size: small; font-family: Candara" Width="100%" BorderColor="#CCCCCC"
                                        BorderWidth="1px"></asp:TextBox>
                                </td>
                                <td class="style40">
                                </td>
                                <td class="style37">
                                    Country of Birth
                                </td>
                                <td class="style37">
                                    <telerik:RadDropDownList ID="radCountryofBirth" runat="server" BackColor="White"
                                        DefaultMessage="-- Select --" DropDownHeight="200px" Font-Names="Candara" Font-Size="Medium"
                                        Height="52px" RenderMode="Lightweight" ResolvedRenderMode="Classic" Style="font-size: small;
                                        font-family: Candara" Width="100%">
                                    </telerik:RadDropDownList>
                                </td>
                                <td class="style41">
                                </td>
                                <td class="style37">
                                    Place of Birth
                                </td>
                                <td class="style37">
                                    <asp:TextBox ID="txtPlaceOfBirth" runat="server" Font-Names="Candara" Font-Size="Medium"
                                        Style="font-size: small; font-family: Candara" Width="238px" BorderColor="#CCCCCC"
                                        BorderWidth="1px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="style72" valign="top">
                                    Hobbies
                                </td>
                                <td class="style45">
                                    <asp:TextBox ID="txtHobbies" runat="server" Font-Names="Candara" Font-Size="Medium"
                                        Height="84px" Style="font-size: small; font-family: Candara" TextMode="MultiLine"
                                        Width="100%" BorderColor="#CCCCCC" BorderWidth="1px"></asp:TextBox>
                                </td>
                                <td class="style40">
                                </td>
                                <td class="style34" valign="top">
                                    Joined Date <span class="style43">*</span>
                                </td>
                                <td class="style37" valign="top">
                                    <telerik:RadDatePicker ID="radDateJoin" runat="server" Culture="en-US" Height="25px"
                                        MinDate="1900-01-01" ResolvedRenderMode="Classic" Width="100%" 
                                        RenderMode="Lightweight">
                                        <Calendar EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;" UseColumnHeadersAsSelectors="False"
                                            UseRowHeadersAsSelectors="False" RenderMode="Lightweight">
                                        </Calendar>
                                        <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" Height="25px" LabelWidth="40%"
                                            Width="" RenderMode="Lightweight">
                                            <EmptyMessageStyle Resize="None" />
                                            <ReadOnlyStyle Resize="None" />
                                            <FocusedStyle Resize="None" />
                                            <DisabledStyle Resize="None" />
                                            <InvalidStyle Resize="None" />
                                            <HoveredStyle Resize="None" />
                                            <EnabledStyle Resize="None" />
                                        </DateInput><DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                    </telerik:RadDatePicker>
                                </td>
                                <td class="style41">
                                </td>
                                <td class="style37" valign="top">
                                    Company</td>
                                <td class="style37" valign="top">
                                   
                                    <telerik:RadDropDownList ID="radCompany" runat="server" BackColor="White" 
                                        DefaultMessage="-- Select --" DropDownHeight="200px" Font-Names="Candara" 
                                        Font-Size="Medium" Height="52px" RenderMode="Lightweight" 
                                        ResolvedRenderMode="Classic" Style="font-size: small;
                                        font-family: Candara" Width="238px"></telerik:RadDropDownList>
                                    
                                </td>
                            </tr>
                            <tr>
                                <td class="style72">
                                </td>
                                <td class="style45">
                                </td>
                                <td class="style40">
                                </td>
                                <td class="style37">
                                    <telerik:RadDatePicker ID="radConfirmDate" runat="server" Culture="en-US" 
                                        Height="16px" MinDate="" ResolvedRenderMode="Classic" Visible="False" 
                                        Width="55%">
                                        <Calendar EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;" 
                                            UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False">
                                        </Calendar>
                                        <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" Height="16px" 
                                            LabelWidth="40%" Width="">
                                            <EmptyMessageStyle Resize="None" />
                                            <ReadOnlyStyle Resize="None" />
                                            <FocusedStyle Resize="None" />
                                            <DisabledStyle Resize="None" />
                                            <InvalidStyle Resize="None" />
                                            <HoveredStyle Resize="None" />
                                            <EnabledStyle Resize="None" />
                                        </DateInput>
                                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                    </telerik:RadDatePicker>
                                </td>
                                <td class="style37">
                                    <telerik:RadDatePicker ID="radTerminationDate" runat="server" Culture="en-US" 
                                        Height="16px" MinDate="1900-01-01" ResolvedRenderMode="Classic" Visible="False" 
                                        Width="231px">
                                        <Calendar EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;" 
                                            UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False">
                                        </Calendar>
                                        <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" Height="16px" 
                                            LabelWidth="40%" Width="">
                                            <EmptyMessageStyle Resize="None" />
                                            <ReadOnlyStyle Resize="None" />
                                            <FocusedStyle Resize="None" />
                                            <DisabledStyle Resize="None" />
                                            <InvalidStyle Resize="None" />
                                            <HoveredStyle Resize="None" />
                                            <EnabledStyle Resize="None" />
                                        </DateInput>
                                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                    </telerik:RadDatePicker>
                                </td>
                                <td class="style41">
                                </td>
                                <td class="style37">
                                    &nbsp;</td>
                                <td class="style37">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style72">
                                    &nbsp;</td>
                                <td class="style45">
                                </td>
                                <td class="style40">
                                </td>
                                <td class="style37">
                                </td>
                                <td class="style37">
                                </td>
                                <td class="style41">
                                </td>
                                <td class="style37">
                                </td>
                                <td class="style37">
                                </td>
                            </tr>
                            <tr>
                                <td class="style72">
                                    &nbsp;</td>
                                <td class="style45">
                                    &nbsp;</td>
                                <td class="style40">
                                </td>
                                <td class="style37">
                                </td>
                                <td class="style37">
                                </td>
                                <td class="style41">
                                </td>
                                <td class="style37">
                                </td>
                                <td class="style37">
                                </td>
                            </tr>
                            <tr>
                                <td class="style72">
                                    &nbsp;</td>
                                <td class="style45">
                                    &nbsp;</td>
                                <td class="style40">
                                </td>
                                <td class="style37">
                                </td>
                                <td class="style37">
                                </td>
                                <td class="style41">
                                </td>
                                <td class="style37">
                                </td>
                                <td class="style37">
                                </td>
                            </tr>
                            <tr>
                                <td class="style72">
                                    &nbsp;</td>
                                <td class="style45">
                                    &nbsp;</td>
                                <td class="style40">
                                </td>
                                <td class="style37">
                                </td>
                                <td class="style37">
                                </td>
                                <td class="style41">
                                </td>
                                <td class="style37">
                                </td>
                                <td class="style37">
                                </td>
                            </tr>
                            <tr>
                                <td class="style72">
                                    &nbsp;</td>
                                <td class="style45">
                                    &nbsp;</td>
                                <td class="style40">
                                </td>
                                <td class="style37">
                                </td>
                                <td class="style37">
                                </td>
                                <td class="style41">
                                </td>
                                <td class="style37">
                                </td>
                                <td class="style37">
                                </td>
                            </tr>
                            <tr>
                                <td class="style72">
                                    &nbsp;</td>
                                <td class="style45">
                                    <asp:TextBox ID="txtuseremail" runat="server" Font-Names="Candara" Font-Size="Small"
                                        Style="font-size: small; font-family: Candara" Width="100%"></asp:TextBox>
                                </td>
                                <td class="style40">
                                </td>
                                <td class="style37">
                                </td>
                                <td class="style37">
                                </td>
                                <td class="style41">
                                </td>
                                <td class="style37">
                                </td>
                                <td class="style37">
                                </td>
                            </tr>
                            <tr>
                                <td class="style72">
                                    &nbsp;</td>
                                <td class="style45">
                                </td>
                                <td class="style40">
                                </td>
                                <td class="style37">
                                </td>
                                <td class="style37">
                                </td>
                                <td class="style41">
                                </td>
                                <td class="style37">
                                </td>
                                <td class="style37">
                                </td>
                            </tr>
                            <tr>
                                <td class="style72">
                                    &nbsp;</td>
                                <td class="style45">
                                </td>
                                <td class="style40">
                                </td>
                                <td class="style37">
                                </td>
                                <td class="style37">
                                </td>
                                <td class="style41">
                                </td>
                                <td class="style37">
                                </td>
                                <td class="style37">
                                </td>
                            </tr>
                            <tr>
                                <td class="style72">
                                    &nbsp;</td>
                                <td class="style45">
                                </td>
                                <td class="style40">
                                </td>
                                <td class="style37">
                                </td>
                                <td class="style37">
                                </td>
                                <td class="style41">
                                </td>
                                <td class="style37">
                                </td>
                                <td class="style37">
                                </td>
                            </tr>
                            <tr>
                                <td class="style72">
                                    <asp:Button ID="btnAddPDetail" runat="server" Text="Save Personal Detail" BackColor="#1BA691"
                                        ForeColor="White" Width="90%" Height="20px" BorderStyle="None" />
                                </td>
                                <td class="style34">
                                    <asp:Button ID="btnCancel" runat="server" Text="Close Window" BackColor="#1BA691"
                                        ForeColor="White" Width="50%" Height="20px" BorderStyle="None" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
                <ajaxToolkit:TabPanel runat="server" HeaderText="TabPanel2" ID="TabPanel2" OnDemandMode="Always">
                    <HeaderTemplate>
                        Contact Details</HeaderTemplate>
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td class="style1" colspan="2">
                                    <asp:TextBox ID="txtIDContact" runat="server" Font-Names="Candara" Font-Size="Small"
                                        Height="16px" Style="font-size: small; font-family: Candara" Visible="False"
                                        Width="238px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="style70" valign="top">
                                    Address 1 <span class="style43">*</span>
                                </td>
                                <td class="style42">
                                    <asp:TextBox ID="txtcAddr1" runat="server" BorderColor="#CCCCCC" BorderWidth="1px"
                                        Font-Names="Candara" Font-Size="Medium" Height="80px" Style="font-size: small;
                                        font-family: Candara" TextMode="MultiLine" Width="100%"></asp:TextBox>
                                </td>
                                <td class="style40">
                                </td>
                                <td class="style69" valign="top">
                                    Address 2
                                </td>
                                <td class="style42" valign="top">
                                    <asp:TextBox ID="txtAddr2" runat="server" BorderColor="#CCCCCC" BorderWidth="1px"
                                        Font-Names="Candara" Font-Size="Medium" Height="80px" Style="font-size: small;
                                        font-family: Candara; margin-left: 2px;" TextMode="MultiLine" Width="100%"></asp:TextBox>
                                </td>
                                <td class="style40">
                                </td>
                            </tr>
                            <tr>
                                <td class="style70" valign="top">
                                    Postal Address
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPostalAddr" runat="server" BorderColor="#CCCCCC" BorderWidth="1px"
                                        Font-Names="Candara" Font-Size="Medium" Height="80px" Style="font-size: small;
                                        font-family: Candara" TextMode="MultiLine" Width="100%"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                                <td class="style69" valign="top">
                                    Private Email Address
                                </td>
                                <td class="style42" valign="top">
                                    <asp:TextBox ID="txtEmail" runat="server" BorderColor="#CCCCCC" BorderWidth="1px"
                                        Font-Names="Candara" Font-Size="Medium" Height="80px" Style="font-size: small;
                                        font-family: Candara" TextMode="MultiLine" Width="100%"></asp:TextBox>
                                </td>
                                <td class="style40">
                                </td>
                            </tr>
                            <tr>
                                <td class="style70">
                                    City <span class="style43">*</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCity" runat="server" BorderColor="#CCCCCC" BorderWidth="1px"
                                        Font-Names="Candara" Font-Size="Medium" Height="30px" Style="font-size: small;
                                        font-family: Candara" Width="408px"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                                <td class="style69" valign="top">
                                    Country <span class="style43">*</span>
                                </td>
                                <td>
                                    <telerik:RadDropDownList ID="radResidenceCountry" runat="server" BackColor="White"
                                        DefaultMessage="-- Select --" DropDownHeight="200px" Font-Names="Candara" Font-Size="Medium"
                                        Height="81px" RenderMode="Lightweight" ResolvedRenderMode="Classic" Style="font-size: small;
                                        font-family: Candara" Width="391px">
                                    </telerik:RadDropDownList>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="style70">
                                    Home Phone
                                </td>
                                <td>
                                    <asp:TextBox ID="txtHomePhone" runat="server" BorderColor="#CCCCCC" BorderWidth="1px"
                                        Font-Names="Candara" Font-Size="Medium" Height="30px" Style="font-size: small;
                                        font-family: Candara" Width="408px"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                                <td class="style69" valign="top">
                                    Mobile Number <span class="style43">*</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtMobileNo" runat="server" BorderColor="#CCCCCC" BorderWidth="1px"
                                        Font-Names="Candara" Font-Size="Medium" Height="30px" Style="font-size: small;
                                        font-family: Candara" Width="408px"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="style70">
                                    Work Phone
                                </td>
                                <td>
                                    <asp:TextBox ID="txtWorkPhone" runat="server" BorderColor="#CCCCCC" BorderWidth="1px"
                                        Font-Names="Candara" Font-Size="Medium" Height="30px" Style="font-size: small;
                                        font-family: Candara" Width="408px"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                                <td class="style69">
                                    Work Email Address <span class="style43">*</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtWorkEmail" runat="server" BorderColor="#CCCCCC" BorderWidth="1px"
                                        Font-Names="Candara" Font-Size="Medium" Height="30px" Style="font-size: small;
                                        font-family: Candara" Width="408px"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="style70">
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td class="style69">
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="style70">
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td class="style69">
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="style70">
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td class="style69">
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="style70">
                                    <asp:Button ID="btnAddContact" runat="server" BackColor="#1BA691" BorderStyle="None"
                                        ForeColor="White" Height="20px" Text="Save Contact Detail" Width="184px"></asp:Button>
                                </td>
                                <td class="style8">
                                    <asp:Button ID="Button2" runat="server" BackColor="#1BA691" BorderStyle="None" ForeColor="White"
                                        Height="20px" Text="Close Window" Width="120px"></asp:Button>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
                <ajaxToolkit:TabPanel runat="server" HeaderText="TabPanel3" ID="TabPanel3" OnDemandMode="Always">
                    <HeaderTemplate>
                        Emergency Contact/Referees</HeaderTemplate>
                    <ContentTemplate>
                        <table width="100%">
                            <tr valign="top">
                                <td style="width: 50%">
                                    <table>
                                        <tr>
                                            <td class="style34" bgcolor="#1BA691" colspan="5" style="border-right-style: solid;
                                                border-right-width: thin">
                                                <span class="style65">Emergency Contact</span>
                                                <asp:TextBox ID="txtIDEmergency" runat="server" Font-Names="Candara" Font-Size="Small"
                                                    Height="16px" Style="font-size: small; font-family: Candara" Visible="False"
                                                    Width="238px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style78">
                                                <span class="style31">Full Name </span><span class="style44">*</span>
                                            </td>
                                            <td class="style46">
                                                <asp:TextBox ID="txtEmerName1" runat="server" BorderColor="#CCCCCC" BorderWidth="1px"
                                                    Font-Names="Candara" Font-Size="Small" Style="font-size: small; font-family: Candara"
                                                    Width="100%"></asp:TextBox>
                                            </td>
                                            <td class="style76">
                                            </td>
                                            <td class="style79">
                                                <span class="style31">Full Name</span>
                                            </td>
                                            <td class="style75">
                                                <asp:TextBox ID="txtEmerName2" runat="server" BorderColor="#CCCCCC" BorderWidth="1px"
                                                    Font-Names="Candara" Font-Size="Small" Style="font-size: small; font-family: Candara"
                                                    Width="100%"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style78" valign="top">
                                                Address <span class="style43">*</span>
                                            </td>
                                            <td class="style46">
                                                <asp:TextBox ID="txtEmerAddr1" runat="server" BorderColor="#CCCCCC" BorderWidth="1px"
                                                    Font-Names="Candara" Font-Size="Small" Height="105px" Style="font-size: small;
                                                    font-family: Candara" TextMode="MultiLine" Width="100%"></asp:TextBox>
                                            </td>
                                            <td class="style76">
                                            </td>
                                            <td class="style79" valign="top">
                                                Address
                                            </td>
                                            <td class="style75">
                                                <asp:TextBox ID="txtEmerAddr2" runat="server" BorderColor="#CCCCCC" BorderWidth="1px"
                                                    Font-Names="Candara" Font-Size="Small" Height="105px" Style="font-size: small;
                                                    font-family: Candara" TextMode="MultiLine" Width="100%"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style78" valign="top">
                                                Phone Number <span class="style43">*</span>
                                            </td>
                                            <td class="style46">
                                                <asp:TextBox ID="txtEmerNo1" runat="server" BorderColor="#CCCCCC" BorderWidth="1px"
                                                    Font-Names="Candara" Font-Size="Medium" Style="font-size: small; font-family: Candara"
                                                    Width="100%"></asp:TextBox>
                                            </td>
                                            <td class="style76">
                                            </td>
                                            <td class="style79">
                                                Phone Number
                                            </td>
                                            <td class="style75">
                                                <asp:TextBox ID="txtEmerNo2" runat="server" BorderColor="#CCCCCC" BorderWidth="1px"
                                                    Font-Names="Candara" Font-Size="Medium" Style="font-size: small; font-family: Candara"
                                                    Width="100%"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style78" valign="top">
                                                Relationship <span class="style43">*</span>
                                            </td>
                                            <td class="style46">
                                                <telerik:RadDropDownList ID="radEmerRelationship1" runat="server" BackColor="White"
                                                    DefaultMessage="-- Select --" DropDownHeight="200px" Font-Names="Candara" Font-Size="Medium"
                                                    Height="72px" RenderMode="Lightweight" ResolvedRenderMode="Classic" Style="font-size: small;
                                                    font-family: Candara" Width="100%">
                                                </telerik:RadDropDownList>
                                            </td>
                                            <td class="style76">
                                            </td>
                                            <td class="style79">
                                                Relationship
                                            </td>
                                            <td class="style75">
                                                <telerik:RadDropDownList ID="radEmerRelationship2" runat="server" BackColor="White"
                                                    DefaultMessage="-- Select --" DropDownHeight="200px" Font-Names="Candara" Font-Size="Medium"
                                                    Height="62px" RenderMode="Lightweight" ResolvedRenderMode="Classic" Style="font-size: small;
                                                    font-family: Candara" Width="100%">
                                                </telerik:RadDropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style78">
                                            </td>
                                            <td class="style46">
                                            </td>
                                            <td class="style76">
                                            </td>
                                            <td class="style79">
                                            </td>
                                            <td class="style34" valign="top">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style78">
                                                &nbsp;</td>
                                            <td class="style46">
                                                &nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 50%">
                                    <table>
                                        <tr>
                                            <td class="style34" bgcolor="#1BA691" colspan="5">
                                                <span class="style65">Referees </span>
                                                <asp:TextBox ID="TextBox1" runat="server" Font-Names="Candara" Font-Size="Small"
                                                    Height="16px" Style="font-size: small; font-family: Candara" Visible="False"
                                                    Width="238px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style80">
                                                Refere<span class="style31">e Name</span>
                                            </td>
                                            <td class="style46">
                                                <asp:TextBox ID="txtRefName1" runat="server" BorderColor="#CCCCCC" BorderWidth="1px"
                                                    Font-Names="Candara" Font-Size="Small" Style="font-size: small; font-family: Candara"
                                                    Width="100%"></asp:TextBox>
                                            </td>
                                            <td class="style76">
                                            </td>
                                            <td class="style82">
                                                <span class="style31">Referee Name</span>
                                            </td>
                                            <td class="style83">
                                                <asp:TextBox ID="txtRefName2" runat="server" BorderColor="#CCCCCC" BorderWidth="1px"
                                                    Font-Names="Candara" Font-Size="Small" Style="font-size: small; font-family: Candara"
                                                    Width="100%"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style80" valign="top">
                                                Organisation
                                            </td>
                                            <td class="style46">
                                                <asp:TextBox ID="txtRefAddress1" runat="server" BorderColor="#CCCCCC" BorderWidth="1px"
                                                    Font-Names="Candara" Font-Size="Small" Height="105px" Style="font-size: small;
                                                    font-family: Candara" TextMode="MultiLine" Width="100%"></asp:TextBox>
                                            </td>
                                            <td class="style76">
                                            </td>
                                            <td class="style82" valign="top">
                                                Organisation
                                            </td>
                                            <td class="style83">
                                                <asp:TextBox ID="txtRefAddress2" runat="server" BorderColor="#CCCCCC" BorderWidth="1px"
                                                    Font-Names="Candara" Font-Size="Small" Height="105px" Style="font-size: small;
                                                    font-family: Candara" TextMode="MultiLine" Width="100%"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style80" valign="top">
                                                Phone Number
                                            </td>
                                            <td class="style46">
                                                <asp:TextBox ID="txtRefPhone1" runat="server" BorderColor="#CCCCCC" BorderWidth="1px"
                                                    Font-Names="Candara" Font-Size="Medium" Style="font-size: small; font-family: Candara"
                                                    Width="100%"></asp:TextBox>
                                            </td>
                                            <td class="style76">
                                            </td>
                                            <td class="style82">
                                                Phone Number
                                            </td>
                                            <td class="style83">
                                                <asp:TextBox ID="txtRefPhone2" runat="server" BorderColor="#CCCCCC" BorderWidth="1px"
                                                    Font-Names="Candara" Font-Size="Medium" Style="font-size: small; font-family: Candara"
                                                    Width="100%"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style80" valign="top">
                                                Email Address
                                            </td>
                                            <td class="style46">
                                                <asp:TextBox ID="txtRefEmail1" runat="server" BorderColor="#CCCCCC" BorderWidth="1px"
                                                    Font-Names="Candara" Font-Size="Medium" Style="font-size: small; font-family: Candara"
                                                    Width="100%"></asp:TextBox>
                                            </td>
                                            <td class="style76">
                                            </td>
                                            <td class="style82">
                                                Email Address
                                            </td>
                                            <td class="style83">
                                                <asp:TextBox ID="txtRefEmail2" runat="server" BorderColor="#CCCCCC" BorderWidth="1px"
                                                    Font-Names="Candara" Font-Size="Medium" Style="font-size: small; font-family: Candara"
                                                    Width="100%"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style80">
                                                Position
                                            </td>
                                            <td class="style46">
                                                <asp:TextBox ID="txtRefPostion1" runat="server" BorderColor="#CCCCCC" BorderWidth="1px"
                                                    Font-Names="Candara" Font-Size="Medium" Style="font-size: small; font-family: Candara"
                                                    Width="100%"></asp:TextBox>
                                            </td>
                                            <td class="style76">
                                            </td>
                                            <td class="style82">
                                                Position
                                            </td>
                                            <td class="style83" valign="top">
                                                <asp:TextBox ID="txtRefPostion2" runat="server" BorderColor="#CCCCCC" BorderWidth="1px"
                                                    Font-Names="Candara" Font-Size="Medium" Style="font-size: small; font-family: Candara"
                                                    Width="100%"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style80">
                                                Years Known
                                            </td>
                                            <td class="style46">
                                                <asp:TextBox ID="txtRefYear1" runat="server" BorderColor="#CCCCCC" BorderWidth="1px"
                                                    Font-Names="Candara" Font-Size="Medium" Style="font-size: small; font-family: Candara;
                                                    text-align: right;" TextMode="Number" Width="50%"></asp:TextBox>
                                            </td>
                                            <td class="style76">
                                            </td>
                                            <td class="style82">
                                                Years Known
                                            </td>
                                            <td class="style83" valign="top">
                                                <asp:TextBox ID="txtRefYear2" runat="server" BorderColor="#CCCCCC" BorderWidth="1px"
                                                    Font-Names="Candara" Font-Size="Medium" Style="font-size: small; font-family: Candara;
                                                    text-align: right;" Width="50%"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style80">
                                                Confirmed</td>
                                            <td class="style46">
                                                <telerik:RadComboBox ID="cboRefConfirm1" Runat="server">
                                                </telerik:RadComboBox>
                                            </td>
                                            <td class="style76">
                                            </td>
                                            <td class="style82">
                                                Confirmed
                                            </td>
                                            <td class="style83" valign="top">
                                                <telerik:RadComboBox ID="cboRefConfirm2" Runat="server">
                                                </telerik:RadComboBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <table >
                            <tr>
                                <td>
                                    <asp:Button ID="Button1" runat="server" BackColor="#1BA691" BorderStyle="None" 
                                        ForeColor="White" Height="20px" Text="Save" Width="120px" />
                                </td>
                                <td>
                                    <asp:Button ID="Button3" runat="server" BackColor="#1BA691" BorderStyle="None" 
                                        ForeColor="White" Height="20px" Text="Close Window" Width="120px" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
                
                <ajaxToolkit:TabPanel runat="server" HeaderText="TabPanel4" ID="TabPanel7" OnDemandMode="Always">
                    <HeaderTemplate>
                        Qualifications</HeaderTemplate>
                    <ContentTemplate>
                        <div>
                            <table>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td class="style50">
                                    </td>
                                    <td class="style57">
                                    </td>
                                    <td class="style53">
                                    </td>
                                    <td>
                                    </td>
                                    <td class="style63">
                                    </td>
                                    <td class="style58">
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        Lang.
                                    </td>
                                    <td>
                                    </td>
                                    <td class="style50">
                                        <asp:GridView ID="GridVwLang" runat="server" AllowPaging="True" AllowSorting="True"
                                            BorderStyle="Solid" DataKeyNames="id" Font-Names="Verdana" Font-Size="12px" Height="50px"
                                            OnRowDataBound="OnRowDataBoundLang" OnSorting="SortLanguages" PageSize="5" ToolTip="click row to select record"
                                            Width="100%" AutoGenerateColumns="False" GridLines="Vertical" ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC">
                                            <AlternatingRowStyle BackColor="#CCCCCC"></AlternatingRowStyle>
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkboxSelectAll0" runat="server" onclick="CheckAllEmpLang(this);" /></HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkEmp0" runat="server" /></ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Rows" HeaderText="Rows">
                                                    <ItemStyle Width="5%" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Language">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("Id", "~/Module/Recruitment/EmployeeThirdLanguage.aspx?Id1={0}") %>'
                                                            Text='<%# Eval("Language")%>' /></ItemTemplate>
                                                    <ItemStyle Font-Bold="True" HorizontalAlign="Left" Width="54%"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Reading" HeaderText="Reading">
                                                    <ItemStyle Width="12%" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Writing" HeaderText="Writing">
                                                    <ItemStyle Width="12%" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Speaking" HeaderText="Speaking">
                                                    <ItemStyle Width="12%" />
                                                </asp:BoundField>
                                            </Columns>
                                            <HeaderStyle BackColor="#1BA691" ForeColor="White" HorizontalAlign="Center"></HeaderStyle>
                                        </asp:GridView>
                                        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
                                        <script type="text/javascript">
                                            $(function () {
                                                $("[id*=GridVwLang] td").hover(function () {
                                                    $("td", $(this).closest("tr")).addClass("hover_row");
                                                }, function () {
                                                    $("td", $(this).closest("tr")).removeClass("hover_row");
                                                })
                                            })
                                        </script>
                                        <script type="text/javascript">
                                            function openLang(code) {
                                                window.open("EmpLangUpdate.aspx?id=" + code, "open_window", "width=800,height=600");
                                            }
                                        </script>
                                    </td>
                                    <td class="style57">
                                    </td>
                                    <td class="style53">
                                    </td>
                                    <td valign="top">
                                        Educations
                                    </td>
                                    <td class="style63">
                                        <asp:GridView ID="GridVwEducation" runat="server" AllowPaging="True" AllowSorting="True"
                                            BorderStyle="Solid" DataKeyNames="id" Font-Names="Verdana" Font-Size="12px" Height="50px"
                                            OnRowDataBound="OnRowDataBoundEdu" OnSorting="SortEducation" PageSize="5" ToolTip="click row to select record"
                                            Width="100%" Style="margin-right: 36px" AutoGenerateColumns="False" GridLines="Vertical" ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC">
                                            <AlternatingRowStyle BackColor="#CCCCCC"></AlternatingRowStyle>
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkboxSelectAll2" runat="server" onclick="CheckAllEmpEdu(this);" /></HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkEmp2" runat="server" /></ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Rows" HeaderText="Rows">
                                                    <ItemStyle Width="5%" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Education">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("Id", "~/Module/Recruitment/EmployeeThirdEducation.aspx?Id1={0}") %>'
                                                            Text='<%# Eval("Qualification")%>' /></ItemTemplate>
                                                    <ItemStyle Font-Bold="True" HorizontalAlign="Left" Width="20%"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Institute" HeaderText="Institute">
                                                    <ItemStyle Width="40%" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Start Date" HeaderText="Start Date">
                                                    <ItemStyle Width="15%" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Completed On" HeaderText="Completed On">
                                                    <ItemStyle Width="15%" />
                                                </asp:BoundField>
                                            </Columns>
                                            <HeaderStyle BackColor="#1BA691" ForeColor="White" HorizontalAlign="Center"></HeaderStyle>
                                        </asp:GridView>
                                        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
                                        <script type="text/javascript">
                                            $(function () {
                                                $("[id*=GridVwEducation] td").hover(function () {
                                                    $("td", $(this).closest("tr")).addClass("hover_row");
                                                }, function () {
                                                    $("td", $(this).closest("tr")).removeClass("hover_row");
                                                })
                                            })
                                        </script>
                                        <script type="text/javascript">
                                            function openEducation(code) {
                                                window.open("EmpEducationUpdate.aspx?id=" + code, "open_window", "width=800,height=600");
                                            }
                                        </script>
                                    </td>
                                    <td class="style58">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td class="style50">
                                    </td>
                                    <td class="style57">
                                    </td>
                                    <td class="style53">
                                    </td>
                                    <td>
                                    </td>
                                    <td class="style63">
                                    </td>
                                    <td class="style58">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td class="style50">
                                    </td>
                                    <td class="style57">
                                    </td>
                                    <td class="style53">
                                    </td>
                                    <td>
                                    </td>
                                    <td class="style63">
                                    </td>
                                    <td class="style58">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td class="style50">
                                        <table width="100%">
                                            <tr>
                                                <td class="style55">
                                                    <asp:Button ID="btnAddLang" runat="server" BackColor="#1BA691" BorderStyle="None"
                                                        ForeColor="White" Height="20px" Text="Add Language" Width="100px"></asp:Button>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnDelLang" runat="server" BackColor="#1BA691" BorderStyle="None"
                                                        ForeColor="White" Height="20px" Text="Delete Language" Width="100px"></asp:Button>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="style57">
                                    </td>
                                    <td class="style53">
                                    </td>
                                    <td>
                                    </td>
                                    <td class="style63">
                                        <table width="100%">
                                            <tr>
                                                <td class="style55">
                                                    <asp:Button ID="btnAddEducation" runat="server" BackColor="#1BA691" BorderStyle="None"
                                                        ForeColor="White" Height="20px" Text="Add Education" Width="100px"></asp:Button>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnDelEducation" runat="server" BackColor="#1BA691" BorderStyle="None"
                                                        ForeColor="White" Height="20px" Style="margin-left: 0px" Text="Delete Education"
                                                        Width="100px"></asp:Button>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="style58">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td class="style50">
                                    </td>
                                    <td class="style57">
                                    </td>
                                    <td class="style53">
                                    </td>
                                    <td>
                                    </td>
                                    <td class="style63">
                                    </td>
                                    <td class="style58">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td class="style50">
                                    </td>
                                    <td class="style57">
                                    </td>
                                    <td class="style53">
                                    </td>
                                    <td>
                                    </td>
                                    <td class="style63">
                                    </td>
                                    <td class="style58">
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        Skills
                                    </td>
                                    <td>
                                    </td>
                                    <td class="style50">
                                        <asp:GridView ID="GridVwSkills" runat="server" AllowPaging="True" AllowSorting="True"
                                            BorderStyle="Solid" DataKeyNames="id" Font-Names="Verdana" Font-Size="12px" Height="50px"
                                            OnRowDataBound="OnRowDataBoundSkills" OnSorting="SortSkills" PageSize="5" ToolTip="click row to select record"
                                            Width="100%" AutoGenerateColumns="False" GridLines="Vertical" ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC">
                                            <AlternatingRowStyle BackColor="#CCCCCC"></AlternatingRowStyle>
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkboxSelectAll1" runat="server" onclick="CheckAllEmpSkills(this);" /></HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkEmp1" runat="server" /></ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Rows" HeaderText="Rows">
                                                    <ItemStyle Width="5%" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Language">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("Id", "~/Module/Recruitment/EmployeeThirdSkills.aspx?Id1={0}") %>'
                                                            Text='<%# Eval("Skill")%>' /></ItemTemplate>
                                                    <ItemStyle Font-Bold="True" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Description" HeaderText="Description">
                                                    <ItemStyle Width="60%" />
                                                </asp:BoundField>
                                            </Columns>
                                            <HeaderStyle BackColor="#1BA691" ForeColor="White" HorizontalAlign="Center"></HeaderStyle>
                                        </asp:GridView>
                                        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
                                        <script type="text/javascript">
                                            $(function () {
                                                $("[id*=GridVwSkills] td").hover(function () {
                                                    $("td", $(this).closest("tr")).addClass("hover_row");
                                                }, function () {
                                                    $("td", $(this).closest("tr")).removeClass("hover_row");
                                                })
                                            })
                                        </script>
                                        <script type="text/javascript">
                                            function openSkills(code) {
                                                window.open("EmployeeSkills.aspx?id1=" + code, "open_window", "width=800,height=600");
                                            }
                                        </script>
                                    </td>
                                    <td class="style57">
                                    </td>
                                    <td class="style53">
                                    </td>
                                    <td valign="top">
                                        Certifications
                                    </td>
                                    <td class="style63">
                                        <asp:GridView ID="GridVwCertification" runat="server" AllowPaging="True" AllowSorting="True"
                                            BorderStyle="Solid" DataKeyNames="id" Font-Names="Verdana" Font-Size="12px" Height="50px"
                                            OnRowDataBound="OnRowDataBoundCert" OnSorting="SortCertifications" PageSize="5"
                                            ToolTip="click row to select record" Width="100%" AutoGenerateColumns="False" GridLines="Vertical" ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC">
                                            <AlternatingRowStyle BackColor="#CCCCCC"></AlternatingRowStyle>
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkboxSelectAll3" runat="server" onclick="CheckAllEmpCert(this);" /></HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkEmp3" runat="server" /></ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Rows" HeaderText="Rows">
                                                    <ItemStyle Width="5%" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Education">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("Id", "~/Module/Recruitment/EmployeeThirdCertification.aspx?Id1={0}") %>'
                                                            Text='<%# Eval("Certification")%>' /></ItemTemplate>
                                                    <ItemStyle Font-Bold="True" HorizontalAlign="Left" Width="20%"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Institute" HeaderText="Institute">
                                                    <ItemStyle Width="40%" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Date Granted" HeaderText="Date Granted">
                                                    <ItemStyle Width="15%" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Expiry Date" HeaderText="Expiry Date">
                                                    <ItemStyle Width="15%" />
                                                </asp:BoundField>
                                            </Columns>
                                            <HeaderStyle BackColor="#1BA691" ForeColor="White" HorizontalAlign="Center"></HeaderStyle>
                                        </asp:GridView>
                                        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
                                        <script type="text/javascript">
                                            $(function () {
                                                $("[id*=GridVwCertification] td").hover(function () {
                                                    $("td", $(this).closest("tr")).addClass("hover_row");
                                                }, function () {
                                                    $("td", $(this).closest("tr")).removeClass("hover_row");
                                                })
                                            })
                                        </script>
                                        <script type="text/javascript">
                                            function openCertificate(code) {
                                                window.open("EmpCertificateUpdate.aspx?id=" + code, "open_window", "width=800,height=600");
                                            }
                                        </script>
                                    </td>
                                    <td class="style58">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td class="style50">
                                    </td>
                                    <td class="style57">
                                    </td>
                                    <td class="style53">
                                    </td>
                                    <td>
                                    </td>
                                    <td class="style63">
                                    </td>
                                    <td class="style58">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td class="style50">
                                    </td>
                                    <td class="style57">
                                    </td>
                                    <td class="style53">
                                    </td>
                                    <td>
                                    </td>
                                    <td class="style63">
                                    </td>
                                    <td class="style58">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td class="style50">
                                        <table width="100%">
                                            <tr>
                                                <td class="style56">
                                                    <asp:Button ID="btnAddSkill" runat="server" BackColor="#1BA691" BorderStyle="None"
                                                        ForeColor="White" Height="20px" Text="Add Skill" Width="100px"></asp:Button>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnDelSkill" runat="server" BackColor="#1BA691" BorderStyle="None"
                                                        ForeColor="White" Height="20px" Style="margin-left: 0px" Text="Delete Skill"
                                                        Width="100px"></asp:Button>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="style57">
                                    </td>
                                    <td class="style53">
                                    </td>
                                    <td>
                                    </td>
                                    <td class="style63">
                                        <table width="100%">
                                            <tr>
                                                <td class="style55">
                                                    <asp:Button ID="btnAddCert" runat="server" BorderStyle="None"
                                                        ForeColor="White" CssClass="btn btn-primary btn-success" Height="20px" Text="Add Certification" Width="100px"></asp:Button>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnDelCert" CssClass="btn btn-primary btn-danger" runat="server" BorderStyle="None"
                                                        ForeColor="White" Height="20px" Style="margin-left: 0px" Text="Delete Certification"
                                                        Width="100px"></asp:Button>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="style58">
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
                
              
            </ajaxToolkit:TabContainer>
            
            <script type="text/javascript" language="javascript">
                //    Grid View Language
                function CheckAllEmpLang(Checkbox) {
                    var GridVwLang = document.getElementById("<%=GridVwLang.ClientID %>");
                    for (i = 1; i < GridVwLang.rows.length; i++) {
                        GridVwLang.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
                    }
                }
            </script>
            <script type="text/javascript" language="javascript">
                //    Grid View Skills
                function CheckAllEmpSkills(Checkbox) {
                    var GridVwSkills = document.getElementById("<%=GridVwSkills.ClientID %>");
                    for (i = 1; i < GridVwSkills.rows.length; i++) {
                        GridVwSkills.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
                    }
                }
            </script>
            <script type="text/javascript" language="javascript">
                //    Grid View Education
                function CheckAllEmpEdu(Checkbox) {
                    var GridVwEducation = document.getElementById("<%=GridVwEducation.ClientID %>");
                    for (i = 1; i < GridVwEducation.rows.length; i++) {
                        GridVwEducation.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
                    }
                }
            </script>
            <script type="text/javascript" language="javascript">
                //    Grid View Certificate
                function CheckAllEmpCert(Checkbox) {
                    var GridVwCertification = document.getElementById("<%=GridVwCertification.ClientID %>");
                    for (i = 1; i < GridVwCertification.rows.length; i++) {
                        GridVwCertification.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
                    }
                }
            </script>
           
        </div>
    </div>
    </form>
</body>
</html>
</asp:Content>