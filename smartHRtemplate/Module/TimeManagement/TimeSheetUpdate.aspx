<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="TimeSheetUpdate.aspx.vb"
    Inherits="GOSHRM.TimeSheetUpdate" EnableEventValidation="false" Debug="true" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI.Gantt" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
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
        .style2
        {
            font-family: Candara;
            font-size: small;
            width: 258px;
            color: #FF0000;
        }
        .style5
        {
            font-family: Candara;
            font-size: medium;
            width: 258px;
        }
        .style6
        {
            width: 258px;
        }
        .RadDropDownList { display:inline-block !important; 
                                               width: 443px !important; }
                            .RadDropDownList_Default{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.RadDropDownList{width:160px;line-height:1.3333em;text-align:left;display:inline-block;vertical-align:middle;white-space:nowrap;cursor:default;*zoom:1;*display:inline}.RadDropDownList_Default .rddlInner{border-radius:3px;border-color:#aaa;color:#333;background-color:#c1c1c1;background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radGradientButtonSprite.png');background-image:linear-gradient(#fafafa,#e6e6e6)}.RadDropDownList .rddlInner{vertical-align:top}.RadDropDownList .rddlInner{padding:2px 22px 2px 5px;border-width:1px;border-style:solid;display:block;position:relative;overflow:hidden}.RadDropDownList_Default .rddlDefaultMessage{color:#a5a5a5}.RadDropDownList .rddlDefaultMessage{font-style:italic}.RadDropDownList .rddlFakeInput{margin:0;padding:0;width:100%;min-height:16px;display:block;overflow:hidden}
                            .rddlFakeInput {
                                    height: 16px !important; 
                                    width: 80% !important;}.RadDropDownList_Default .rddlIcon{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radActionsSprite.png');background-position:-22px -20px}.RadDropDownList .rddlIcon{width:16px;height:100%;border:0;background-repeat:no-repeat;position:absolute;top:0;right:0}.rddlSlide{float:left;display:none;position:absolute;overflow:hidden;z-index:7000}.rddlPopup_Default{border-color:#a0a0a0;color:#333;background-color:#fff;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.rddlPopup{*zoom:1;border-width:1px;border-style:solid;text-align:left;position:relative;cursor:default;width:160px;*width:158px;box-sizing:border-box}
        .style7
        {
            width: 502px;
        }
        </style>

     
</head>

<script type="text/javascript">
    function setTextBoxHeight() {
        var act = document.getElementById("<%=txtActivity.ClientID%>");
        act.style.height = act.scrollHeight + "px";

        var note = document.getElementById("<%=txtNote.ClientID%>");
        note.style.height = note.scrollHeight + "px";

        var comment = document.getElementById("<%=txtComment.ClientID%>");
        comment.style.height = comment.scrollHeight + "px";

        var hrcomment = document.getElementById("<%=txtHRComment.ClientID%>");
        hrcomment.style.height = hrcomment.scrollHeight + "px";
    }
    </script>

<body onload="setTextBoxHeight();" onunload="window.opener.location=window.opener.location;" style="">
    <form>
    <%--<textarea cols = onkeyup="setTextBoxHeight();" onkeydown="setTextBoxHeight();" ></textarea>--%>
    <%--<script type="text/javascript">
        function closeme() {
            window.close();
        }
        window.onblur = closeme;
    </script>--%>

    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>

    
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
        rel="Stylesheet" type="text/css" />
    <table>
        <tr>
            <td class="style1" colspan="2" style="background-color: #1BA691" bgcolor="#0066FF">
                Time Sheet&nbsp;
                <asp:Label ID="lblDays0" runat="server" Font-Names="Verdana" Font-Size="Small"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style6">
            </td>
            <td class="style7">
                <asp:TextBox ID="txtid" runat="server" Width="183px" Style="font-size: medium; font-family: Candara"
                    Font-Names="Candara" Height="20px" Visible="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style6" valign="top">
                <asp:Label ID="Label14" runat="server" Font-Names="Verdana" Font-Size="12px" 
                    Text="Project" Font-Bold="True" ForeColor="#666666"></asp:Label>
                <asp:Label ID="Label18" runat="server" Font-Names="Verdana" Font-Size="12px" ForeColor="#FF3300" Text=" *"></asp:Label>
            </td>
            <td class="style7">
                <telerik:RadDropDownList ID="radProject" runat="server" DefaultMessage="-- Select --"
                    Font-Names="Verdana" Font-Size="12px" Height="16px" Width="100%" 
                    AutoPostBack="True" DataTextField="Project" DataValueField="id" ResolvedRenderMode="Classic"
                    RenderMode="Lightweight" ForeColor="#666666">
                </telerik:RadDropDownList>
            </td>
        </tr>
        <tr>
            <td class="style5" valign="top">
                <asp:Label ID="Label3" runat="server" Font-Names="Verdana" Font-Size="12px" 
                    Text="Activity" Font-Bold="True" ForeColor="#666666"></asp:Label>
                <asp:Label ID="Label9" runat="server" Font-Names="Verdana" Font-Size="12px" ForeColor="#FF3300" Text=" *"></asp:Label>
            </td>
            <td class="style7">
                <telerik:RadDropDownList ID="radActivity" runat="server" DefaultMessage="-- Select --"
                    Font-Names="Verdana" Font-Size="12px" Height="16px" Width="100%" 
                    ResolvedRenderMode="Classic" DataTextField="Activity" DataValueField="Activity"
                    AutoPostBack="True" RenderMode="Lightweight" ForeColor="#666666">
                </telerik:RadDropDownList>
                <asp:TextBox ID="txtActivity" runat="server" Font-Names="Verdana" Font-Size="12px"
                    Width="100%" Enabled="False" TextMode="MultiLine" 
                    BorderColor="#CCCCCC" BorderWidth="1px" 
                    Height="69px" ForeColor="#666666"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style5" valign="top">
                <asp:Label ID="Label1" runat="server" Font-Names="Verdana" Font-Size="12px" 
                    Text="Employee" Font-Bold="True" ForeColor="#666666"></asp:Label>
                <asp:Label ID="Label2" runat="server" Font-Names="Verdana" Font-Size="12px" ForeColor="#FF3300" Text=" *"></asp:Label>
            </td>
            <td class="style7">
                <telerik:RadDropDownList ID="radEmployee" runat="server" DefaultMessage="-- Select --"
                    Font-Names="Verdana" Font-Size="12px" Height="16px" Width="100%" 
                    ResolvedRenderMode="Classic" RenderMode="Lightweight" ForeColor="#666666">
                </telerik:RadDropDownList>
            </td>
        </tr>
        <tr>
            <td class="style6">
                <asp:Label ID="Label6" runat="server" Font-Names="Verdana" Font-Size="12px" 
                    Text="Activity Date" Font-Bold="True" ForeColor="#666666"></asp:Label>
                <asp:Label ID="Label11" runat="server" Font-Names="Verdana" Font-Size="12px" ForeColor="#FF3300" Text=" *"></asp:Label>
            </td>
            <td class="style7">
                <telerik:RadDatePicker ID="radStartDate" runat="server" RenderMode="Lightweight"
                    AutoPostBack="True" Font-Names="Verdana" Font-Size="11px" 
                    ForeColor="#666666">
                    <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True"
                        FastNavigationNextText="&amp;lt;&amp;lt;" RenderMode="Lightweight">
                    </Calendar>
                    <DateInput DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" LabelWidth="40%"
                        RenderMode="Lightweight" AutoPostBack="True">
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
            </td>
        </tr>
        <tr>
            <td class="style6">
                <asp:Label ID="Label4" runat="server" Font-Names="Verdana" Font-Size="12px" 
                    Text="Activity End Date" Font-Bold="True" ForeColor="#666666"></asp:Label>
                <asp:Label ID="Label5" runat="server" Font-Names="Verdana" Font-Size="12px" ForeColor="#FF3300" Text=" *"></asp:Label>
            </td>
            <td class="style7">
                <telerik:RadDatePicker ID="radEndDate" runat="server" RenderMode="Lightweight" 
                    AutoPostBack="True" Font-Names="Verdana" Font-Size="11px" 
                    ForeColor="#666666">
                    <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True"
                        FastNavigationNextText="&amp;lt;&amp;lt;" RenderMode="Lightweight">
                    </Calendar>
                    <DateInput DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" LabelWidth="40%"
                        RenderMode="Lightweight" AutoPostBack="True">
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
            </td>
        </tr>
        <tr>
            <td class="style6">
                <asp:Label ID="Label7" runat="server" Font-Names="Verdana" Font-Size="12px" 
                    Text="Start Time" Font-Bold="True" ForeColor="#666666"></asp:Label>
                <asp:Label ID="Label12" runat="server" Font-Names="Verdana" Font-Size="12px" ForeColor="#FF3300" Text=" *"></asp:Label>
            </td>
            <td class="style7">
                <telerik:RadComboBox ID="radHourStart" Runat="server" 
                    ResolvedRenderMode="Classic" Width="50px" AutoPostBack="True" 
                    Font-Names="Verdana" Font-Size="11px" ForeColor="#666666">
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
                <asp:Label ID="lblHRName0" runat="server" Font-Names="Candara" Visible="False" 
                    Font-Italic="True">:</asp:Label>
                <telerik:RadComboBox ID="radMinStart" Runat="server" 
                    ResolvedRenderMode="Classic" Width="50px" AutoPostBack="True"  
                    Font-Names="Verdana" Font-Size="11px" ForeColor="#666666">
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
                <telerik:RadComboBox ID="radTimeStart" Runat="server" 
                    ResolvedRenderMode="Classic" Width="50px" AutoPostBack="True"  
                    Font-Names="Verdana" Font-Size="11px" ForeColor="#666666">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="AM" Value="AM" 
                            Owner="radTimeStart" />
                        <telerik:RadComboBoxItem runat="server" Text="PM" Value="PM" 
                            Owner="radTimeStart" />                 
                    </Items>
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td class="style6">
                <asp:Label ID="Label15" runat="server" Font-Names="Verdana" Font-Size="12px" 
                    Text="End Time" Font-Bold="True" ForeColor="#666666"></asp:Label>
                <asp:Label ID="Label19" runat="server" Font-Names="Verdana" Font-Size="12px" ForeColor="#FF3300" Text=" *"></asp:Label>
            </td>
            <td class="style7">
                <telerik:RadComboBox ID="radHourStart0" Runat="server" 
                    ResolvedRenderMode="Classic" Width="50px" AutoPostBack="True"  
                    Font-Names="Verdana" Font-Size="11px" ForeColor="#666666">
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
                <asp:Label ID="Label20" runat="server" Font-Names="Verdana" Font-Size="12px" Visible="False" 
                    Font-Italic="True">:</asp:Label>
                <telerik:RadComboBox ID="radMinStart0" Runat="server" 
                    ResolvedRenderMode="Classic" Width="50px" AutoPostBack="True"  
                    Font-Names="Verdana" Font-Size="11px" ForeColor="#666666">
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
                <telerik:RadComboBox ID="radTimeStart0" Runat="server" 
                    ResolvedRenderMode="Classic" Width="50px" AutoPostBack="True"  
                    Font-Names="Verdana" Font-Size="11px" ForeColor="#666666">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="AM" Value="AM" 
                            Owner="radTimeStart" />
                        <telerik:RadComboBoxItem runat="server" Text="PM" Value="PM" 
                            Owner="radTimeStart" />                 
                    </Items>
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td class="style6">
                <asp:Label ID="Label16" runat="server" Font-Names="Verdana" Font-Size="12px" 
                    Text="Duration (Hours)" Font-Bold="True" ForeColor="#666666"></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="lblDays" runat="server" Font-Names="Verdana" Font-Size="12px" 
                    ForeColor="#666666"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style6" valign="top">
                <asp:Label ID="Label8" runat="server" Font-Names="Verdana" Font-Size="12px" 
                    Text="Note" Font-Bold="True" ForeColor="#666666"></asp:Label>
            </td>
            <td class="style7">
                <asp:TextBox ID="txtNote" runat="server" Width="100%" 
                    Font-Names="Verdana" Font-Size="12px" TextMode="MultiLine" 
                    BorderColor="#CCCCCC" BorderWidth="1px" Height="100px" 
                    ForeColor="#666666" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style6">
                <asp:Label ID="Label17" runat="server" Font-Names="Verdana" Font-Size="12px"  
                    Text="Time Sheet Status" Font-Bold="True" ForeColor="#666666"></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="lblFinalStatus" runat="server"  Font-Names="Verdana" 
                    Font-Size="12px" Width="100%" ForeColor="#666666"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style6">
                <asp:Label ID="Label10" runat="server" Font-Names="Verdana" Font-Size="12px" Text="Project Manager"
                    Visible="False" Font-Bold="True" ForeColor="#666666"></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="lblPMName" runat="server" Font-Names="Verdana" Font-Size="12px" 
                    Visible="False" Width="100%" ForeColor="#666666"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style6">
                <asp:Label ID="lblActStatus" runat="server" Font-Names="Verdana" 
                    Font-Size="12px" Text="PM Approval Status" Font-Bold="True" ForeColor="#666666"></asp:Label>
            </td>
            <td class="style7">
                <asp:RadioButtonList ID="rdoStatus" runat="server" TabIndex="7" Width="100%" 
                    Font-Names="Verdana" Font-Size="12px"
                    RepeatDirection="Horizontal" ForeColor="#666666">
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td class="style6" valign="top">
                <asp:Label ID="lblComment" runat="server" Font-Names="Verdana" Font-Size="12px" 
                    Text="PM's Comment" Font-Bold="True" ForeColor="#666666"></asp:Label>
            </td>
            <td class="style7">
                <asp:TextBox ID="txtComment" runat="server" Width="100%" Font-Names="Verdana" 
                    Font-Size="12px" TextMode="MultiLine" 
                    BorderColor="#CCCCCC" BorderWidth="1px" ForeColor="#666666" Height="100px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style6">
                <asp:Label ID="lblHRStatus" runat="server" Font-Names="Verdana" 
                    Font-Size="12px" Text="HR Approval Status" Font-Bold="True" ForeColor="#666666"></asp:Label>
            </td>
            <td class="style7">
                <asp:RadioButtonList ID="rdoHRStatus" runat="server" TabIndex="7" Width="100%" 
                    Font-Names="Verdana" Font-Size="12px"
                    RepeatDirection="Horizontal" ForeColor="#666666">
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td class="style6">
                <asp:Label ID="lblHR" runat="server" Font-Names="Verdana" Font-Size="12px" 
                    Text="HR Approval By" Font-Italic="True" Font-Bold="True" ForeColor="#666666"></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="lblHRName" runat="server" Font-Names="Verdana" Font-Size="12px" 
                    Visible="False" Font-Italic="True" ForeColor="#666666"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style6" valign="top">
                <asp:Label ID="Label13" runat="server" Font-Names="Verdana" Font-Size="12px" 
                    Text="HR's Comment" Font-Bold="True" ForeColor="#666666"></asp:Label>
            </td>
            <td class="style7">
                <asp:TextBox ID="txtHRComment" runat="server" Width="100%" Font-Names="Verdana" 
                    Font-Size="12px" TextMode="MultiLine" 
                    BorderColor="#CCCCCC" BorderWidth="1px" ForeColor="#666666" Height="100px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style2" style="border-bottom-style: solid; border-bottom-width: thin;
                border-bottom-color: #C0C0C0">
                * Required Field
            </td>
            <td style="border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #C0C0C0"
                class="style7">
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
                <asp:Button ID="btnAdd" runat="server" Text="Save &amp; Submit" BackColor="#1BA691"
                    ForeColor="White" Width="120px" Height="20px" BorderStyle="None" 
                    Font-Names="Verdana" Font-Size="11px" />
                <asp:Button ID="btnStatus" runat="server" Text="Submit PM Approval" BackColor="#1BA691"
                    ForeColor="White" Width="120px" Height="20px" BorderStyle="None" 
                    Visible="False" Font-Names="Verdana" Font-Size="11px" />
                <asp:Button ID="btnHR" runat="server" Text="Submit HR Approval" BackColor="#1BA691"
                    ForeColor="White" Width="120px" Height="20px" BorderStyle="None" 
                    Visible="False" Font-Names="Verdana" Font-Size="11px" />
            </td>
            <td class="style7">
                <asp:Button ID="btnCancel" runat="server" Text="Close" BackColor="#FF3300" ForeColor="White"
                    Width="120px" Height="20px" BorderStyle="None" Font-Names="Verdana" 
                    Font-Size="11px" />
            </td>
        </tr>
    </table>
    </form>

   
</body>
</html>
</asp:Content>