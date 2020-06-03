    <%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="QueryROPage.aspx.vb"
    Inherits="GOSHRM.QueryROPage" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Add New</title>
    <script type="text/javascript">

        function closeWin() {
            popup.close();   // Closes the new window
        }
   

    </script>
    <style type="text/css">
        .style1
        {
            color: #FFFFFF;
            font-family: Candara;
        }
        .lbl
        {
            font-family: Candara;
            font-size: medium;
        }
        .style5
        {
            font-family: Candara;
            font-size: medium;
            width: 180px;
        }
        .style10
        {
            width: 486px;
        }
        .style11
        {
            width: 501px;
        }
        .style12
        {
            width: 172px;
        }
        .style13
        {
            width: 180px;
        }
        .style14
        {
            width: 167px;
        }
    </style>
</head>
<%-- <script type="text/javascript" language="javascript">
    function LoadList()
    {        
        var ds=null;
        ds = <%=listFilter %>;
            $( "#txtName" ).autocomplete({
              source: ds
            });
    }
    </script>--%>
<body onunload="window.opener.location=window.opener.location;">
    <form>
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
        rel="Stylesheet" type="text/css" />
        <div>
        </div>
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server">
            <%--<table>
                <tr>
                    <td class="style1" colspan="2" style="background-color: #1BA691">
                        <strong>Raised Query</strong>
                    </td>
                </tr>
                <tr>
                    <td class="style13">
                        &nbsp;
                    </td>
                    <td class="style10">
                        <asp:TextBox ID="txtid" runat="server" Width="13px" Style="font-size: medium; font-family: Candara"
                            Font-Names="Candara" Height="20px" Visible="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style5">
                        <asp:Label ID="Label1" runat="server" Font-Names="Verdana" Text="Employee*" 
                            Font-Size="12px" Font-Bold="True" ForeColor="#666666"></asp:Label>
                    </td>
                    <td class="style10">
                        <telerik:RadComboBox ID="cboEmployee" runat="server" Width="100%" Filter="Contains" ForeColor="#666666"
                            Font-Names="Verdana" Font-Size="12px">
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td class="style13">
                        <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="#666666" Font-Names="Verdana" Text="Query Date*" Font-Size="12px"></asp:Label>
                    </td>
                    <td class="style10">
                        <telerik:RadDatePicker ID="datNotice" runat="server" Culture="en-US" MinDate="" RenderMode="Lightweight" ForeColor="#666666"
                            ResolvedRenderMode="Classic" Width="35%" Font-Names="Verdana" 
                            Font-Size="12px" Enabled="False">
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
                    </td>
                </tr>
                <tr>
                    <td class="style13">
                        <asp:Label ID="Label8" runat="server" Font-Bold="True" ForeColor="#666666" Font-Names="Verdana" 
                            Text="Expected Reply Date*" Font-Size="12px"></asp:Label>
                    </td>
                    <td class="style10">
                        <telerik:RadDatePicker ID="datExpectedDate" runat="server" Culture="en-US" ForeColor="#666666"
                            MinDate="" RenderMode="Lightweight"
                            ResolvedRenderMode="Classic" Width="35%" Font-Names="Verdana" 
                            Font-Size="12px">
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
                    </td>
                </tr>
                <tr>
                    <td class="style13">
                        <asp:Label ID="Label9" runat="server" Font-Bold="True" ForeColor="#666666" 
                            Font-Names="Verdana" Font-Size="12px" 
                            Text="Expected Reply Time*"></asp:Label>
                    </td>
                    <td class="style10">
                        <telerik:RadComboBox ID="radHourStart" Runat="server" ForeColor="#666666"
                            Font-Names="Verdana" Font-Size="11px" ResolvedRenderMode="Classic" 
                            Width="70px">
                            <Items>
                                <telerik:RadComboBoxItem runat="server" Owner="radHourStart" Text="1" 
                                    Value="1" />
                                <telerik:RadComboBoxItem runat="server" Owner="radHourStart" Text="2" 
                                    Value="2" />
                                <telerik:RadComboBoxItem runat="server" Owner="radHourStart" Text="3" 
                                    Value="3" />
                                <telerik:RadComboBoxItem runat="server" Owner="radHourStart" Text="4" 
                                    Value="4" />
                                <telerik:RadComboBoxItem runat="server" Owner="radHourStart" Text="5" 
                                    Value="5" />
                                <telerik:RadComboBoxItem runat="server" Owner="radHourStart" Text="6" 
                                    Value="6" />
                                <telerik:RadComboBoxItem runat="server" Owner="radHourStart" Text="7" 
                                    Value="7" />
                                <telerik:RadComboBoxItem runat="server" Owner="radHourStart" Text="8" 
                                    Value="8" />
                                <telerik:RadComboBoxItem runat="server" Owner="radHourStart" Text="9" 
                                    Value="9" />
                                <telerik:RadComboBoxItem runat="server" Owner="radHourStart" Text="10" 
                                    Value="10" />
                                <telerik:RadComboBoxItem runat="server" Owner="radHourStart" Text="11" 
                                    Value="11" />
                                <telerik:RadComboBoxItem runat="server" Owner="radHourStart" Text="12" 
                                    Value="12" />
                            </Items>
                        </telerik:RadComboBox>
                        <asp:Label ID="lblHRName0" runat="server" Font-Italic="True" 
                            Font-Names="Candara" Visible="False">:</asp:Label>
                        <telerik:RadComboBox ID="radMinStart" Runat="server" ForeColor="#666666"
                            Font-Names="Verdana" Font-Size="11px" ResolvedRenderMode="Classic" 
                            Width="70px">
                            <Items>
                                <telerik:RadComboBoxItem runat="server" Owner="radMinStart" Text="00" 
                                    Value="00" />
                                <telerik:RadComboBoxItem runat="server" Owner="radMinStart" Text="15" 
                                    Value="15" />
                                <telerik:RadComboBoxItem runat="server" Owner="radMinStart" Text="30" 
                                    Value="30" />
                                <telerik:RadComboBoxItem runat="server" Owner="radMinStart" Text="45" 
                                    Value="45" />
                            </Items>
                        </telerik:RadComboBox>
                        <telerik:RadComboBox ID="radTimeStart" Runat="server" ForeColor="#666666"
                            Font-Names="Verdana" Font-Size="11px" ResolvedRenderMode="Classic" 
                            Width="70px">
                            <Items>
                                <telerik:RadComboBoxItem runat="server" Owner="radTimeStart" Text="AM" 
                                    Value="AM" />
                                <telerik:RadComboBoxItem runat="server" Owner="radTimeStart" Text="PM" 
                                    Value="PM" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td class="style13" valign="top">
                        <asp:Label ID="Label6" runat="server" Font-Names="Verdana" Font-Bold="True" ForeColor="#666666" Text="Query Message" Font-Size="12px"></asp:Label>
                    </td>
                    <td class="style10">
                        <asp:TextBox ID="txtQuery" runat="server" Width="100%" ForeColor="#666666" Font-Names="Verdana" Height="140px" TextMode="MultiLine"
                            BorderColor="#CCCCCC" BorderWidth="1px" Font-Size="12px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style13" valign="top">
                        <asp:Label ID="Label13" runat="server" Font-Names="Verdana" 
                            Text="Employee Reply" Font-Bold="True" ForeColor="#666666"
                            Font-Size="12px"></asp:Label>
                    </td>
                    <td class="style10">
                        <asp:Label ID="lblEmployeeResponse" runat="server" Font-Names="Verdana" ForeColor="#666666" Font-Size="12px"></asp:Label>
                        <br />
                        <asp:LinkButton ID="lnkResponse" runat="server" Font-Names="Verdana" Font-Size="12px">Comment on Employee Response for HR Disciplinary Action</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td class="style13" valign="top">
                        <asp:Label ID="Label3" runat="server" Font-Names="Verdana" Font-Bold="True" 
                            ForeColor="#666666" Text="Employee Reply Date"
                            Font-Size="12px"></asp:Label>
                    </td>
                    <td class="style10">
                        <asp:Label ID="lblEmpDate" runat="server" ForeColor="#666666" Font-Names="Verdana" Font-Size="12px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style13" valign="top">
                        <asp:Label ID="Label4" runat="server" Font-Names="Verdana" 
                            Text="Employee Reply Status" Font-Bold="True" ForeColor="#666666"
                            Font-Size="12px"></asp:Label>
                    </td>
                    <td class="style10">
                        <asp:Label ID="lblEmpStatus" runat="server" ForeColor="#666666" Font-Names="Verdana" Font-Size="12px"></asp:Label>
                    </td>
                </tr>
                
                <tr>
                    <td class="style13">
                        &nbsp;</td>
                    <td class="style10">
                        <asp:Label ID="lblinitiator" runat="server" Font-Names="Verdana" 
                            Font-Size="13px" Text="Label" Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style13">
                        <asp:Button ID="btnAdd" runat="server" Text="Save" BackColor="#1BA691" ForeColor="White"
                            Width="120px" Height="25px" BorderStyle="None" Font-Names="Verdana" Font-Size="11px"
                            
                            ToolTip="Save Query and an automatic email will be sent to the queried employee" 
                            Font-Bold="True" />
                    </td>
                    <td class="style10">
                        <table width="100%">
                            <tr>
                                <td class="style14">
                                    <asp:Button ID="btnCancel" runat="server" BackColor="#FF3300" 
                                        BorderStyle="None" Font-Names="Verdana" Font-Size="11px" ForeColor="White" 
                                        Height="25px" Text="Close" Width="120px" Font-Bold="True" />
                                </td>
                                <td>
                                    <asp:Button ID="btnNotification" runat="server" BackColor="#3399FF" 
                                        BorderStyle="None" Font-Bold="True" Font-Names="Verdana" Font-Size="11px" 
                                        ForeColor="White" Height="25px" Text="Notify Employee" 
                                        ToolTip="Automatic email will be sent to the queried employee" Width="120px" />
                                </td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="style13">
                        &nbsp;</td>
                    <td class="style10">
                        &nbsp;</td>
                </tr>
            </table>--%>
            <div class="container col-md-12">
        <div>
            <div class="row">
                <div id="divalert" runat="server" visible="false" class="alert alert-info">
                    <strong id="msgalert" runat="server"></strong>
                    <asp:TextBox ID="txtid" runat="server" Width="3px"  
                    Font-Names="Candara" Height="2px" Visible="False"></asp:TextBox>
                     <asp:Label ID="lblinitiator" runat="server" Font-Names="Verdana" Font-Size="13px" Text="Label" Visible="False"></asp:Label>
                    <asp:Label ID="Label1" runat="server" Font-Names="Verdana" ForeColor="#666666" Font-Size="12px"></asp:Label>
                        <br />                  
                </div>
                </div>
                <div class="row col-md-10">
                 <div class="panel panel-success">
                <div class="panel-heading">
                     <h5 class="page-title" style="color:#1BA691">Raised Query</h5>
                </div>
             <div class="panel-body">                  
                    <form action="">
                        <div class="">
                            <div class="form-group">
                                <label>EMPLOYEE*</label>
                                <telerik:RadComboBox ID="cboEmployee" runat="server" Width="100%" Filter="Contains" ForeColor="#666666"
                                    Font-Names="Verdana" Skin="Bootstrap">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class="">
                            <div class="form-group">
                                <label>QUERY DATE*</label>
                                <telerik:RadDatePicker ID="datNotice" runat="server" Culture="en-US" MinDate="" RenderMode="Lightweight" ForeColor="#666666"
                                    ResolvedRenderMode="Classic" Width="100%" Font-Names="Verdana" 
                                    Skin="Bootstrap" Enabled="False">
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
                        <div class="">
                            <div class="form-group">
                                <label>EXPECTED REPLY DATE</label>
                                <telerik:RadDatePicker ID="datExpectedDate" runat="server" Culture="en-US" ForeColor="#666666"
                                    MinDate="" RenderMode="Lightweight"
                                    ResolvedRenderMode="Classic" Width="100%" Font-Names="Verdana" 
                                   Skin="Bootstrap">
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
                        <div class="">
                            <div class="form-group">
                                <label>
                                    EXPECTED REPLY TIME</label><br />
                               <telerik:RadComboBox Skin="Bootstrap" ID="radHourStart" Runat="server" ForeColor="#666666"
                                Font-Names="Verdana" ResolvedRenderMode="Classic" 
                                Width="100px">
                                <Items>
                                    <telerik:RadComboBoxItem runat="server" Owner="radHourStart" Text="1" 
                                        Value="1" />
                                    <telerik:RadComboBoxItem runat="server" Owner="radHourStart" Text="2" 
                                        Value="2" />
                                    <telerik:RadComboBoxItem runat="server" Owner="radHourStart" Text="3" 
                                        Value="3" />
                                    <telerik:RadComboBoxItem runat="server" Owner="radHourStart" Text="4" 
                                        Value="4" />
                                    <telerik:RadComboBoxItem runat="server" Owner="radHourStart" Text="5" 
                                        Value="5" />
                                    <telerik:RadComboBoxItem runat="server" Owner="radHourStart" Text="6" 
                                        Value="6" />
                                    <telerik:RadComboBoxItem runat="server" Owner="radHourStart" Text="7" 
                                        Value="7" />
                                    <telerik:RadComboBoxItem runat="server" Owner="radHourStart" Text="8" 
                                        Value="8" />
                                    <telerik:RadComboBoxItem runat="server" Owner="radHourStart" Text="9" 
                                        Value="9" />
                                    <telerik:RadComboBoxItem runat="server" Owner="radHourStart" Text="10" 
                                        Value="10" />
                                    <telerik:RadComboBoxItem runat="server" Owner="radHourStart" Text="11" 
                                        Value="11" />
                                    <telerik:RadComboBoxItem runat="server" Owner="radHourStart" Text="12" 
                                        Value="12" />
                                </Items>
                            </telerik:RadComboBox>
                            <telerik:RadComboBox Skin="Bootstrap" ID="radMinStart" Runat="server" ForeColor="#666666"
                                Font-Names="Verdana" ResolvedRenderMode="Classic" 
                                Width="100px">
                                <Items>
                                    <telerik:RadComboBoxItem runat="server" Owner="radMinStart" Text="00" 
                                        Value="00" />
                                    <telerik:RadComboBoxItem runat="server" Owner="radMinStart" Text="15" 
                                        Value="15" />
                                    <telerik:RadComboBoxItem runat="server" Owner="radMinStart" Text="30" 
                                        Value="30" />
                                    <telerik:RadComboBoxItem runat="server" Owner="radMinStart" Text="45" 
                                        Value="45" />
                                </Items>
                            </telerik:RadComboBox>
                            <telerik:RadComboBox ID="radTimeStart" Runat="server" ForeColor="#666666"
                                Font-Names="Verdana" Skin="Bootstrap" ResolvedRenderMode="Classic" 
                                Width="100px">
                                <Items>
                                    <telerik:RadComboBoxItem runat="server" Owner="radTimeStart" Text="AM" 
                                        Value="AM" />
                                    <telerik:RadComboBoxItem runat="server" Owner="radTimeStart" Text="PM" 
                                        Value="PM" />
                                </Items>
                            </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class="">
                            <div class="form-group">
                                <label>QUERY MESSAGE*</label>
                               <textarea id="txtQuery" runat="server" class="form-control" rows="5"></textarea><br />
                               <asp:LinkButton ID="lnkResponse" runat="server" Font-Names="Verdana" Font-Size="12px">Comment on Employee Response for HR Disciplinary Action</asp:LinkButton>
                            </div>
                        </div>
                        <div class="">
                            <div class="form-group">
                                <label>
                                    EMPLOYEE REPLY</label>  
                                <input type="text" id="lblEmployeeResponse" runat="server" class="form-control" readonly="" />
                            </div>
                        </div>
                         <div class="">
                            <div class="form-group">
                                <label>
                                    EMPLOYEE REPLY DATE</label>
                                 <input type="text" id="lblEmpDate" runat="server" class="form-control" readonly="" />
                            </div>
                        </div>
                         <div class="">
                            <div class="form-group">
                                <label>
                                    EMPLOYEE REPLY STATUS</label>
                                 <input type="text" id="lblEmpStatus" runat="server" class="form-control" readonly="" />
                            </div>
                        </div>
                        <div class="col-md-10 m-t-20 text-center">
                            <%--<button id="btnupdate" runat="server" onserverclick="btnAdd_Click" type="submit"
                                style="width: 150px" class="btn btn-primary btn-success">
                                Save &amp; Update</button>--%>
                                <asp:Button ID="btnAdd" CssClass="btn btn-primary btn-success" runat="server" Text="Save" ForeColor="White"
                            Width="150px" BorderStyle="None" Font-Names="Verdana"
                            ToolTip="Save Query and an automatic email will be sent to the queried employee" 
                            Font-Bold="True" />
                                <asp:Button ID="btnCancel" runat="server" 
                                        BorderStyle="None" CssClass="btn btn-primary btn-danger" Font-Names="Verdana" ForeColor="White" 
                                        Text="Back" Width="150px" Font-Bold="True" />
                                    <asp:Button ID="btnNotification" runat="server"
                                        BorderStyle="None" Font-Bold="True" Font-Names="Verdana" 
                                        ForeColor="White" CssClass="btn btn-primary btn-info"  Text="Notify Employee" 
                                        ToolTip="Automatic email will be sent to the queried employee" Width="150px" />
                               <%-- <button id="Button3" runat="server" onserverclick="btnCancel_Click" type="submit"
                                style="width: 150px" class="btn btn-primary btn-danger">
                                << Back</button>
                            <button id="Button2" runat="server" onserverclick="Button2_Click" type="submit" style="width: 150px"
                                class="btn btn-primary btn-info">
                                Notify Employee</button>--%>

                        </div>
                    </form>
                    </div></div>
                </div>
        </div>
    </div>
        </asp:View>
         <asp:View ID="View2" runat="server">
         <div class="container col-md-10">
            <div class="row">
                   <div class="panel panel-success">
                        <div class="panel-heading">
                            <h5><b id="pagetitle" runat="server">RAISED QUERY</b></h5>
                        </div>
                     <div class="panel-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                            <label id="lblEmpName" runat="server"></label><br />
                                <label>COMMENT</label>
                                <textarea id="txtComment" runat="server" class="form-control" rows="5"></textarea>
                            </div>
                        </div>
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>QUERY STATUS</label>
                                <telerik:RadComboBox ID="cboApproval" Skin="Bootstrap" runat="server" AutoPostBack="True" 
                                    Font-Names="Verdana" Font-Size="12px" Width="100%" RenderMode="Lightweight">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>HR COMMENT</label>
                                <textarea id="lblhrcomment" readonly runat="server" class="form-control" rows="5"></textarea>
                            </div>
                        </div>
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>
                                    HR RECOMMENDATION</label>
                                    <input type="text" id="lblrecomm" readonly runat="server" class="form-control" />
                                <%--<textarea id="lblrecomm" runat="server" readonly class="form-control" rows="5"></textarea>--%>
                            </div>
                        </div>
                       <%-- <div class=" col-md-12">
                            <div class="form-group">
                                <label>COST PER PARTICIPANTS*</label>
                                <input id="acost" runat="server" class="form-control" type="text" />
                            </div>
                        </div>--%>
                     <%--   <div class=" col-md-12">
                            <div class="form-group">
                                <label>
                                    HR STATUS</label>
                                <telerik:RadDropDownList ID="radStatus" runat="server" DefaultMessage="-- Select --"
                                     Width="100%" ForeColor="#666666" Skin="Bootstrap">
                                </telerik:RadDropDownList>
                            </div>
                        </div>--%>
                        <div class="col-md-12 m-t-20 text-center">
<%--                            <button id="btnupdate" runat="server" onserverclick="btnAdd_Click" type="submit"
                                style="width: 150px" class="btn btn-primary btn-success">
                                Save &amp; Update</button>
                            <button id="Button1" runat="server" onserverclick="btnCancel_Click" type="submit" style="width: 150px"
                                class="btn btn-primary btn-danger">
                                << Back</button>--%>
                                <asp:Button ID="btnUpdateStatus" runat="server" class="btn btn-primary btn-success" Text="Save" ForeColor="White"
                                Width="150px" BorderStyle="None" Font-Names="Verdana" Font-Size="11px"
                                ToolTip="Save Comment" Font-Bold="True" />
    
                                    <asp:Button ID="btnBack" class="btn btn-primary btn-danger" runat="server" BorderStyle="None" 
                                        Font-Names="Verdana" Font-Size="11px" ForeColor="White" 
                                        Text="&lt;&lt; Back" Width="150px" Font-Bold="True" />

                                    <asp:Button ID="btnNotifyHR" class="btn btn-primary btn-info" runat="server" BackColor="#0099FF" 
                                        BorderStyle="None" Font-Bold="True" Font-Names="Verdana" Font-Size="11px" 
                                        ForeColor="White" Text="Notify HR" 
                                        ToolTip="send notification to HR to implement displinary action" 
                                        Width="150px" />
                        </div>
                    </div>
                </div>
            </div>
             </div>
            </div>
            <%--<table>
                <tr>
                    <td class="style1" colspan="2" style="background-color: #1BA691">
                        <strong>Raised Query</strong>
                    </td>
                </tr>
                <tr>
                    <td class="style12">
                        &nbsp;
                    </td>
                    <td class="style11">
                        <asp:Label ID="lblEmpName" runat="server" Font-Names="Verdana" Font-Size="13px" 
                            Text="Label" Font-Bold="True" ForeColor="#666666"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style12" valign="top">
                        
                        <asp:Label ID="Label16" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                            Text="Comment"></asp:Label>
                    </td>
                    <td class="style11">
                        <asp:TextBox ID="txtComment" runat="server" BorderColor="#CCCCCC" ForeColor="#666666"
                            BorderWidth="1px" Font-Names="Verdana" Font-Size="12px" Height="140px" TextMode="MultiLine" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style12">
                        <asp:Label ID="Label14" runat="server" Font-Names="Verdana" Text="Query Status" Font-Bold="True" ForeColor="#666666"
                            Font-Size="12px"></asp:Label>
                    </td>
                    <td class="style11">
                        <telerik:RadComboBox ID="cboApproval" runat="server" AutoPostBack="True" 
                            Font-Names="Verdana" Font-Size="12px" Width="150px" RenderMode="Lightweight">
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td class="style12" valign="top">
                        <asp:Label ID="Label5" runat="server" Font-Names="Verdana" Text="HR Comment" Font-Bold="True" ForeColor="#666666"
                            Font-Size="12px"></asp:Label>
                    </td>
                    <td class="style11">
                        <asp:Label ID="lblhrcomment" runat="server" Font-Names="Verdana"  ForeColor="#666666"
                            Font-Size="12px"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td class="style12" valign="top">
                        <asp:Label ID="Label7" runat="server" Font-Names="Verdana" Text="HR Recommendation" Font-Bold="True" ForeColor="#666666"
                            Font-Size="12px"></asp:Label>
                    </td>
                    <td class="style11">
                        <asp:Label ID="lblrecomm" runat="server" Font-Names="Verdana" ForeColor="#666666"
                            Font-Size="12px"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td class="style12">
                    </td>
                    <td class="style11">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style12">
                        <asp:Button ID="btnUpdateStatus" runat="server" Text="Save" BackColor="#1BA691" ForeColor="White"
                            Width="120px" Height="25px" BorderStyle="None" Font-Names="Verdana" Font-Size="11px"
                            ToolTip="Save Comment" Font-Bold="True" />
    
                                    <asp:Button ID="btnBack" runat="server" BackColor="#999966" BorderStyle="None" 
                                        Font-Names="Verdana" Font-Size="11px" ForeColor="White" Height="25px" 
                                        Text="&lt;&lt; Back" Width="120px" Font-Bold="True" />

                                    <asp:Button ID="btnNotifyHR" runat="server" BackColor="#0099FF" 
                                        BorderStyle="None" Font-Bold="True" Font-Names="Verdana" Font-Size="11px" 
                                        ForeColor="White" Height="25px" Text="Notify HR" 
                                        ToolTip="send notification to HR to implement displinary action" 
                                        Width="120px" />

                                    <asp:Button ID="Button1" runat="server" BackColor="#FF3300" 
                                        BorderStyle="None" Font-Names="Verdana" Font-Size="11px" ForeColor="White" 
                                        Height="25px" Text="Close" Width="120px" Font-Bold="True" />
                                </td>
                            </tr>
                        </table> 
                    </td>
                </tr>

                <tr>
                    <td class="style12">
                        &nbsp;</td>
                    <td class="style11">
                        &nbsp;</td>
                </tr>
            </table> --%>
            </asp:View> 
    </asp:MultiView>
    </form>
</body>
</html>
</asp:Content>
