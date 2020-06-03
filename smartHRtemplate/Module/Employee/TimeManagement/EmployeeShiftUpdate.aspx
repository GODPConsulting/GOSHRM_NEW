<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup ="true" CodeBehind="EmployeeShiftUpdate.aspx.vb" Inherits="GOSHRM.EmployeeShiftUpdate" EnableEventValidation="false" Debug="true"%>
<asp:Content ID = "content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

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
</head>



<body  onunload="window.opener.location=window.opener.location;">
    <form>
    <%--<textarea cols = onkeyup="setTextBoxHeight();" onkeydown="setTextBoxHeight();" ></textarea>--%><%--<script type="text/javascript">
        function closeme() {
            window.close();
        }
        window.onblur = closeme;
    </script>--%>

    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>

    
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
        rel="Stylesheet" type="text/css" />
    <%--<table>
        <tr>
            <td class="style1" colspan="2" style="background-color: #1BA691" bgcolor="#0066FF">
                Assign Shift
            </td>
        </tr>
        <tr>
            <td class="style6">
                <asp:TextBox ID="txtid" runat="server" Width="13px" Style="font-size: medium; font-family: Candara"
                    Font-Names="Candara" Height="16px" Visible="False"></asp:TextBox>
            </td>
            <td class="style7">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style6">
                <asp:Label ID="Label7" runat="server" Font-Names="Verdana" Text="Employee" 
                    Font-Size="12px" Font-Bold="True" ForeColor="#666666"></asp:Label>
                <asp:Label ID="Label12" runat="server" ForeColor="#FF3300" Text=" *"></asp:Label>
            </td>
            <td class="style7">
                <telerik:RadComboBox runat="server" ResolvedRenderMode="Classic" 
                    Font-Names="Verdana" Font-Size="12px" ID="cboEmployee" Filter="Contains" 
                    Width="100%" ForeColor="#666666">
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td class="style6">
                <asp:Label ID="Label4" runat="server" Font-Names="Verdana" Text="Shift Name" 
                    Font-Size="12px" Font-Bold="True" ForeColor="#666666"></asp:Label>
                <asp:Label ID="Label5" runat="server" ForeColor="#FF3300" Text=" *"></asp:Label>
            </td>
            <td class="style7">
                <telerik:RadDropDownList ID="radShift" runat="server" DefaultMessage="-- Select --"
                    Font-Names="Verdana" Font-Size="12px" Height="16px" Width="100%" 
                    ResolvedRenderMode="Classic" ForeColor="#666666">
                </telerik:RadDropDownList>
            </td>
        </tr>
        
        <tr>
            <td class="style6">
                <asp:Label ID="Label15" runat="server" Font-Names="Verdana" Text="From" 
                    Font-Size="12px" Font-Bold="True" ForeColor="#666666"></asp:Label>
                <asp:Label ID="Label19" runat="server" ForeColor="#FF3300" Text=" *"></asp:Label>
            </td>
            <td class="style7">
                <telerik:RadDatePicker ID="radStartDate" runat="server" 
                    Width="120px" RenderMode="Lightweight" Font-Names="Verdana" 
                    Font-Size="12px" ForeColor="#666666">
                    <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True"
                        FastNavigationNextText="&amp;lt;&amp;lt;" RenderMode="Lightweight">
                    </Calendar>
                    <DateInput DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" 
                        LabelWidth="40%" RenderMode="Lightweight">
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
                <asp:Label ID="lblDateTo" runat="server" Font-Names="Verdana" Text="To" Font-Bold="True" ForeColor="#666666"
                    Font-Size="12px"></asp:Label>
            </td>
            <td class="style7">
                <telerik:RadDatePicker ID="radEndDate" runat="server" 
                    Width="120px" RenderMode="Lightweight" Font-Names="Verdana" 
                    Font-Size="12px" ForeColor="#666666">
                    <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True"
                        FastNavigationNextText="&amp;lt;&amp;lt;" RenderMode="Lightweight">
                    </Calendar>
                    <DateInput DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" 
                        LabelWidth="40%" RenderMode="Lightweight">
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
            <td class="style2" style="border-bottom-style: solid; border-bottom-width: thin;
                border-bottom-color: #C0C0C0">
                &nbsp;</td>
            <td style="border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #C0C0C0"
                class="style7">
                <asp:CheckBox ID="chkCurrent" runat="server" AutoPostBack="True" Font-Bold="True" ForeColor="#666666"
                    Font-Names="Verdana" Font-Size="12px" Text="Is Current Shift" />
            </td>
        </tr>
        <tr>
            <td class="style6">
            </td>
            <td class="style7">
                <asp:Label ID="lblstatus" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="11px"
                    ForeColor="#FF3300"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style6">
                <asp:Button ID="btnAdd" runat="server" Text="Save" BackColor="#1BA691"
                    ForeColor="White" Width="120px" Height="20px" BorderStyle="None" 
                    Font-Names="Verdana" Font-Size="11px" />
            </td>
            <td class="style7">
                <asp:Button ID="btnCancel" runat="server" Text="Close" BackColor="#FF3300" ForeColor="White"
                    Width="120px" Height="20px" BorderStyle="None" Font-Names="Verdana" 
                    Font-Size="11px" />
            </td>
        </tr>
    </table>--%>
    <div class="container col-md-12">
        <div>
            <div class="row">
                <div id="divalert" runat="server" visible="false" class="alert alert-info">
                    <strong id="msgalert" runat="server"></strong>
                    <asp:TextBox ID="txtid" runat="server" Width="3px"  
                    Font-Names="Candara" Height="2px" Visible="False"></asp:TextBox>
                </div>
                <div class="col-md-8 col-md-offset-0">
                    <h5 class="page-title" style="color:#1BA691">Assign Shift </h5>
                    <form action="">
                    <div class="row">                       
                         <div class=" col-md-10">
                            <div class="form-group">
                                <label>
                                    Employee </label>
                                <telerik:RadComboBox Skin="Bootstrap" runat="server" ResolvedRenderMode="Classic" 
                                    Font-Names="Verdana" Font-Size="12px" ID="cboEmployee" Filter="Contains" 
                                    Width="100%" ForeColor="#666666">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class=" col-md-10">
                            <div class="form-group">
                                <label>
                                    Shift Name</label>
                                <telerik:RadDropDownList Skin="Bootstrap" ID="radShift" runat="server" DefaultMessage="-- Select --"
                                    Font-Names="Verdana" Font-Size="12px" Width="100%" 
                                    ResolvedRenderMode="Classic" ForeColor="#666666">
                                </telerik:RadDropDownList>
                            </div>
                        </div>
                        <div class=" col-md-10">
                            <div class="form-group">
                                <label>
                                    From</label>
                               <telerik:RadDatePicker Skin="Bootstrap" ID="radStartDate" runat="server" 
                                    Width="100%" RenderMode="Lightweight" Font-Names="Verdana" 
                                    Font-Size="12px" ForeColor="#666666">
                                    <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True"
                                        FastNavigationNextText="&amp;lt;&amp;lt;" RenderMode="Lightweight">
                                    </Calendar>
                                    <DateInput DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" 
                                        LabelWidth="40%" RenderMode="Lightweight">
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
                        <div class=" col-md-10">
                            <div class="form-group">
                                <label id="lblDateTo" runat="server">
                                    To</label>
                               <telerik:RadDatePicker Skin="Bootstrap" ID="radEndDate" runat="server" 
                                    Width="100%" RenderMode="Lightweight" Font-Names="Verdana" 
                                    Font-Size="12px" ForeColor="#666666">
                                    <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True"
                                        FastNavigationNextText="&amp;lt;&amp;lt;" RenderMode="Lightweight">
                                    </Calendar>
                                    <DateInput DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" 
                                        LabelWidth="40%" RenderMode="Lightweight">
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
                       <div class=" col-md-10">
                            <asp:CheckBox ID="chkCurrent" runat="server" AutoPostBack="True" Font-Bold="True" ForeColor="#666666"
                            Font-Names="Verdana" Font-Size="12px" Text="Is Current Shift" />
                        </div>
                        <div class="col-md-10 m-t-20 text-center">
                            <button id="btnAdd" runat="server" onserverclick="btnAdd_Click" type="submit"
                                style="width: 150px" class="btn btn-primary btn-success">
                                Save &amp; Update</button>
                            <button id="Button1" runat="server" onserverclick="btnCancel_Click" type="submit" style="width: 150px"
                                class="btn btn-primary btn-info">
                                << Back</button>
                        </div>
                    </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
    </form>

   
</body>
</html>
</asp:Content>