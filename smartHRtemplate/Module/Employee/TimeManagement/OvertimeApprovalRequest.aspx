<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="OvertimeApprovalRequest.aspx.vb"
    Inherits="GOSHRM.OvertimeApprovalRequest" EnableEventValidation="false" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI.Gantt" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Add New</title>
    <link rel="icon" type="image/png" href="../../../images/goshrm.png">
    <script type="text/javascript">

        function closeWin() {
            popup.close();   // Closes the new window
        }
   

    </script>
    <style type="text/css">
        .style1
        {
            color: #FDFDFD;
            font-family: Candara;
        }
        .lbl
        {
            font-family: Candara;
            font-size: medium;
        }
        .style6
        {
            width: 181px;
        }
        .RadDropDownList { display:inline-block !important; 
                                               width: 443px !important; }
                            .RadDropDownList_Default{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.RadDropDownList{width:160px;line-height:1.3333em;text-align:left;display:inline-block;vertical-align:middle;white-space:nowrap;cursor:default;*zoom:1;*display:inline}.RadDropDownList_Default .rddlInner{border-radius:3px;border-color:#aaa;color:#333;background-color:#c1c1c1;background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radGradientButtonSprite.png');background-image:linear-gradient(#fafafa,#e6e6e6)}.RadDropDownList .rddlInner{vertical-align:top}.RadDropDownList .rddlInner{padding:2px 22px 2px 5px;border-width:1px;border-style:solid;display:block;position:relative;overflow:hidden}.RadDropDownList_Default .rddlDefaultMessage{color:#a5a5a5}.RadDropDownList .rddlDefaultMessage{font-style:italic}.RadDropDownList .rddlFakeInput{margin:0;padding:0;width:100%;min-height:16px;display:block;overflow:hidden}
                            .rddlFakeInput {
                                    height: 16px !important; 
                                    width: 80% !important;}.RadDropDownList_Default .rddlIcon{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radActionsSprite.png');background-position:-22px -20px}.RadDropDownList .rddlIcon{width:16px;height:100%;border:0;background-repeat:no-repeat;position:absolute;top:0;right:0}.rddlSlide{float:left;display:none;position:absolute;overflow:hidden;z-index:7000}.rddlPopup_Default{border-color:#a0a0a0;color:#333;background-color:#fff;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.rddlPopup{*zoom:1;border-width:1px;border-style:solid;text-align:left;position:relative;cursor:default;width:160px;*width:158px;box-sizing:border-box}
        .style7
        {
            width: 511px;
        }
        .RadPicker{vertical-align:middle}.RadPicker{vertical-align:middle}.rdfd_{position:absolute}.rdfd_{position:absolute}.RadPicker .rcTable{table-layout:auto}.RadPicker .rcTable{table-layout:auto}.RadPicker .RadInput{vertical-align:baseline}.RadPicker .RadInput{vertical-align:baseline}.RadInput_Default{font:12px "segoe ui",arial,sans-serif}.RadInput{vertical-align:middle}.RadInput_Default{font:12px "segoe ui",arial,sans-serif}.RadInput{vertical-align:middle}.RadInput .riTextBox{height:17px}.RadInput .riTextBox{height:17px}.RadPicker_Default .rcCalPopup{background-position:0 0}.RadPicker_Default .rcCalPopup{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Calendar.sprite.gif')}.RadPicker .rcCalPopup{display:block;overflow:hidden;width:22px;height:22px;background-color:transparent;background-repeat:no-repeat;text-indent:-2222px;text-align:center;-webkit-box-sizing:content-box;-moz-box-sizing:content-box;box-sizing:content-box}.RadPicker_Default .rcCalPopup{background-position:0 0}.RadPicker_Default .rcCalPopup{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Calendar.sprite.gif')}.RadPicker .rcCalPopup{display:block;overflow:hidden;width:22px;height:22px;background-color:transparent;background-repeat:no-repeat;text-indent:-2222px;text-align:center;-webkit-box-sizing:content-box;-moz-box-sizing:content-box;box-sizing:content-box}
        .style8
        {
            width: 100%;
            cursor: pointer;
        }
        </style>
</head>
<body style="height: 317px">
    <form id="form1" runat="server">
    <%--<textarea cols = onkeyup="setTextBoxHeight();" onkeydown="setTextBoxHeight();" ></textarea>--%><%--<script type="text/javascript">
        function closeme() {
            window.close();
        }
        window.onblur = closeme;
    </script>--%>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
        rel="Stylesheet" type="text/css" />
    <table>
        <tr>
            <td class="style1" colspan="2" style="background-color: #1BA691" bgcolor="#0066FF">
                <asp:Label ID="lblHeADER" runat="server" Font-Names="Verdana" Font-Size="16px" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style6">
                <asp:TextBox ID="txtid" runat="server" Width="3px" Style="font-size: medium; font-family: Candara"
                    Font-Names="Candara" Height="10px" Visible="False"></asp:TextBox>
            </td>
            <td class="style7">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="style6">
                <asp:Label ID="Label5" runat="server" Font-Names="Verdana" Text="Start Date:" 
                    Font-Size="12px" Font-Bold="True" ForeColor="#666666"></asp:Label>
            </td>
            <td class="style7">
                <telerik:RadDatePicker runat="server" AutoPostBack="True" Culture="en-US" 
                    Width="160px" ForeColor="#666666"
                    ResolvedRenderMode="Classic" Font-Names="Verdana" Font-Size="11px"
                    ID="dateFrom">
                    <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True"
                        FastNavigationNextText="&amp;lt;&amp;lt;">
                    </Calendar>
                    <DateInput Width="" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy"
                        AutoPostBack="True" LabelWidth="40%">
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
            </td>
        </tr>
        <tr>
            <td class="style6">
                <asp:Label ID="Label9" runat="server" Font-Names="Verdana" Text="Close Date:" Font-Size="12px" Font-Bold="True" ForeColor="#666666"></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="lblEndDate" runat="server" Font-Names="Verdana" Font-Size="12px" ForeColor="#666666"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style6">
                <asp:Label ID="Label4" runat="server" Font-Names="Verdana" Text="Shift" Font-Size="12px" Font-Bold="True" ForeColor="#666666"></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="lblShift" runat="server" Font-Names="Verdana" Font-Size="12px" ForeColor="#666666"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style6">
                <asp:Label ID="Label7" runat="server" Font-Names="Verdana" Text="Work Duration" Font-Size="12px" Font-Bold="True" ForeColor="#666666"></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="lblDuration" runat="server" Font-Names="Verdana" Font-Size="12px" ForeColor="#666666"></asp:Label>
            </td>
        </tr>
        
        <tr>
            <td class="style6">
                <asp:Label ID="Label15" runat="server" Font-Names="Verdana" Text="Start Time:" Font-Size="12px" Font-Bold="True" ForeColor="#666666"></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="lblStartTime" runat="server" Font-Names="Verdana" Font-Size="12px" ForeColor="#666666"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style6">
                <asp:Label ID="lblDateTo" runat="server" Font-Names="Verdana" Text="End Time:" Font-Size="12px" Font-Bold="True" ForeColor="#666666"></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="lblEndTime" runat="server" Font-Names="Verdana" Font-Size="12px" ForeColor="#666666"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style6">
                <asp:Label ID="Label1" runat="server" Font-Names="Verdana" Text="Hours Logged:" Font-Size="12px" Font-Bold="True" ForeColor="#666666"></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="lblHoursLogged" runat="server" Font-Names="Verdana" Font-Size="12px" ForeColor="#666666"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style6">
                <asp:Label ID="Label3" runat="server" Font-Names="Verdana" Text="Overtime (Hr):" Font-Bold="True" ForeColor="#666666"
                    Font-Size="12px"></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="lblOverTime" runat="server" Font-Names="Verdana" Font-Size="12px" ForeColor="#666666"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style6">
                <asp:Label ID="Label8" runat="server" Font-Names="Verdana" Text="Approved Overtime (Hr):" Font-Bold="True" ForeColor="#666666"
                    Font-Size="12px"></asp:Label>
            </td>
            <td class="style7">
                <asp:TextBox ID="txtagreedOvertime" runat="server" BorderColor="#CCCCCC" BorderStyle="Solid" ForeColor="#666666"
                    BorderWidth="1px" Width="50px" ToolTip="Set Overtime to approve" Font-Names="Verdana"
                    Font-Size="12px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style6">
                <asp:Label ID="Label10" runat="server" Font-Names="Verdana" Text="Overtime Payment Index:" Font-Bold="True" ForeColor="#666666"
                    Font-Size="12px"></asp:Label>
            </td>
            <td class="style7">
                <asp:TextBox ID="txtovertimeindex" runat="server" BorderColor="#CCCCCC" BorderStyle="Solid" ForeColor="#666666"
                    BorderWidth="1px" Width="50px" ToolTip="Set Overtime to approve" Font-Names="Verdana"
                    Font-Size="12px"></asp:TextBox>
                <asp:Label ID="lblpaydesc" runat="server" Font-Names="Verdana" Font-Size="12px" Text="Overtime Payment = (Basic * (Overtime/WorkShift)) * OverTimeIndex" ForeColor="#666666"
                    Font-Italic="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style6">
                <asp:Label ID="Label6" runat="server" Font-Names="Verdana" Text="Approval Status:" Font-Bold="True" ForeColor="#666666"
                    Font-Size="12px"></asp:Label>
            </td>
            <td class="style7">
                <telerik:RadComboBox runat="server" ResolvedRenderMode="Classic" Font-Names="Verdana" ForeColor="#666666"
                    Font-Size="12px" ID="cboApproval" Filter="Contains" Width="100%">
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td class="style6" valign="top">
                <asp:Label ID="Label2" runat="server" Font-Names="Verdana" Text="Approval Comment:" Font-Bold="True" ForeColor="#666666"
                    Font-Size="12px"></asp:Label>
            </td>
            <td class="style7">
                <asp:TextBox ID="txtComment" runat="server" BorderColor="#CCCCCC" 
                    BorderStyle="Solid" ForeColor="#666666"
                    BorderWidth="1px" TextMode="MultiLine" Width="100%" Height="80px" Font-Names="Verdana"
                    Font-Size="12px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style6" valign="top">
                <asp:Label ID="lblDate" runat="server" Font-Names="Verdana" Text="Date Approval Updated:" Font-Bold="True" ForeColor="#666666"
                    Font-Size="12px"></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="lblApprovalDate" runat="server" Font-Names="Verdana" Font-Size="12px" ForeColor="#666666"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style6">
            </td>
            <td class="style7">
                <asp:Label ID="lblstatus" runat="server" Font-Bold="True" Font-Names="Candara" Font-Size="12px"
                    ForeColor="#FF3300"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style6">
                <asp:Button ID="btnAdd" runat="server" Text="Save" BackColor="#1BA691" ForeColor="White"
                    Width="100px" Height="20px" BorderStyle="None" Font-Names="Verdana" Font-Size="11px" />
            </td>
            <td class="style7">
                <asp:Button ID="btnCancel" runat="server" Text="Close" BackColor="#FF3300" ForeColor="White"
                    Width="100px" Height="20px" BorderStyle="None" Font-Names="Verdana" Font-Size="11px" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
