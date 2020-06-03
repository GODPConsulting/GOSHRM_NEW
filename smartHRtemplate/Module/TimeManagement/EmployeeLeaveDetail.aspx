<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="EmployeeLeaveDetail.aspx.vb"
    Inherits="GOSHRM.EmployeeLeaveDetail" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI.Gantt" TagPrefix="cc1" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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

      <style type="text/css">
        .modal
        {
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
           .loading
        {
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
            width: 201px;
            color: #FF0000;
        }
        .style5
        {
            font-family: Candara;
            font-size: medium;
            width: 201px;
        }
        .style6
        {
            width: 201px;
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
        .RadComboBox{text-align:left;display:inline-block;vertical-align:middle;white-space:nowrap;*display:inline;*zoom:1}
        .RadComboBox_Default{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.RadComboBox .rcbReadOnly .rcbInputCellLeft{background-position:0 -88px}.RadComboBox .rcbInputCell{padding:0;border-width:0;border-style:solid;background-color:transparent;background-repeat:no-repeat}.RadComboBox .rcbInputCell{padding-right:4px;padding-left:5px;width:100%;height:20px;line-height:20px;text-align:left;vertical-align:middle}.RadComboBox .rcbInputCellLeft{background-position:0 0}.RadComboBox_Default .rcbInputCell{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')}.RadComboBox .rcbReadOnly .rcbInput{cursor:default}.RadComboBox_Default .rcbReadOnly .rcbInput{color:#333}.RadComboBox .rcbInput{margin:0;padding:2px 0 1px;height:auto;width:100%;border-width:0;outline:0;color:inherit;background-color:transparent;vertical-align:top}.RadComboBox_Default .rcbInput{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif;line-height:16px}.RadComboBox .rcbReadOnly .rcbArrowCellRight{background-position:-162px -176px}.RadComboBox .rcbArrowCell{padding:0;border-width:0;border-style:solid;background-color:transparent;background-repeat:no-repeat}
        .RadComboBox .rcbArrowCell{width:18px}.RadComboBox .rcbArrowCellRight{background-position:-18px -176px}.RadComboBox_Default .rcbArrowCell{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')}
        .RadComboBox .rcbArrowCell a{width:18px;height:22px;position:relative;outline:0;font-size:0;line-height:1px;text-decoration:none;text-indent:9999px;display:block;overflow:hidden;cursor:default;*zoom:1}
        .RadPicker{vertical-align:middle}.RadPicker{vertical-align:middle}.rdfd_{position:absolute}.rdfd_{position:absolute}.RadPicker .rcTable{table-layout:auto}.RadPicker .rcTable{table-layout:auto}.RadPicker .RadInput{vertical-align:baseline}.RadPicker .RadInput{vertical-align:baseline}.RadInput_Default{font:12px "segoe ui",arial,sans-serif}.RadInput{vertical-align:middle}.RadInput_Default{font:12px "segoe ui",arial,sans-serif}.RadInput{vertical-align:middle}.RadInput .riTextBox{height:17px}.RadInput .riTextBox{height:17px}.RadPicker_Default .rcCalPopup{background-position:0 0}.RadPicker_Default .rcCalPopup{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Calendar.sprite.gif')}.RadPicker .rcCalPopup{display:block;overflow:hidden;width:22px;height:22px;background-color:transparent;background-repeat:no-repeat;text-indent:-2222px;text-align:center;-webkit-box-sizing:content-box;-moz-box-sizing:content-box;box-sizing:content-box}.RadPicker_Default .rcCalPopup{background-position:0 0}.RadPicker_Default .rcCalPopup{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Calendar.sprite.gif')}.RadPicker .rcCalPopup{display:block;overflow:hidden;width:22px;height:22px;background-color:transparent;background-repeat:no-repeat;text-indent:-2222px;text-align:center;-webkit-box-sizing:content-box;-moz-box-sizing:content-box;box-sizing:content-box}
        </style>
</head>
<body>
    <form>
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
        rel="Stylesheet" type="text/css" />
          <div class="container col-md-12">
             <div class="row">
                 <div id="divalert" runat="server" visible="false" class="alert alert-info">
                    <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                    <strong id="msgalert" runat="server">Danger!</strong>
                </div>
            </div>
            <div class="panel panel-success">
                <div class="panel-heading col-md-12">
               <h5><b><asp:Label ID="lblLeaveType" runat="server" Font-Bold="True"></asp:Label>
                      <asp:Label ID="Label9" runat="server" Font-Bold="True" Text=": " ></asp:Label>
                      <asp:Label ID="lblLoanRefNo" runat="server" Font-Bold="True"></asp:Label></b></h5>
                </div>
             <div class="panel-body">
        
    <div>
        <table width="100%" >
            <tr>
                <td>               
                                        <asp:GridView ID="gridLeaveChart" runat="server" 
                        AllowPaging="True" AllowSorting="True"
                                            BorderStyle="Solid" Font-Names="Verdana" Font-Size="12px"
                                            Height="50px" PageSize="2"
                                            ToolTip="click row to select record" Width="100%" AutoGenerateColumns="False" GridLines="Vertical" ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC"
                                            ShowHeaderWhenEmpty="True">
                                            <AlternatingRowStyle BackColor="#CCCCCC" />
                                            <Columns> 
                                                <asp:BoundField DataField="LeaveYear" HeaderText="Current Year">
                                                </asp:BoundField>                                      
                                                <asp:BoundField DataField="LeaveType" HeaderText="Leave Type">
                                                </asp:BoundField>
                                                <asp:BoundField DataField="LeavesPerYear" HeaderText="Entitlement (Days)" 
                                                    ItemStyle-HorizontalAlign="Right">
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ApprovedDays" HeaderText="Approved Days" 
                                                    ItemStyle-HorizontalAlign="Right">
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PendingDays" HeaderText="Pending Days" 
                                                    ItemStyle-HorizontalAlign="Right">
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Balance" HeaderText="Current Year Balance" 
                                                    ItemStyle-HorizontalAlign="Right">
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PreviousBalance" HeaderText="Previous Years Balance" 
                                                    ItemStyle-HorizontalAlign="Right">
                                                </asp:BoundField>
                                                <asp:BoundField DataField="totalbalance" HeaderText="Total Balance" 
                                                    ItemStyle-HorizontalAlign="Right">
                                                </asp:BoundField>                                                
                                            </Columns>
                                            <HeaderStyle BackColor="#999966" ForeColor="White" HorizontalAlign="Center" />
                                        </asp:GridView>
                
                </td>
            </tr>
        </table>
    </div>
    <table width="100%">
        <tr>
            <td class="style1" colspan="2" style="background-color: #1BA691" bgcolor="#0066FF">
               
            </td>
        </tr>
    </table>
    
    <table >
        <tr>
            <td class="style6">
                <asp:TextBox ID="txtapproverlevel" runat="server" Width="12px" Style="font-size: medium;
                    font-family: Candara" Font-Names="Candara" Height="10px" Visible="False"></asp:TextBox>
            </td>
            <td class="style7">
                <asp:TextBox ID="txtid" runat="server" Width="12px" Style="font-size: medium; font-family: Candara"
                    Font-Names="Candara" Height="10px" Visible="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style5">
                <asp:Label ID="Label1" runat="server" Font-Names="Verdana" Text="Employee" 
                    Font-Size="12px" Font-Bold="True" ForeColor="#666666"></asp:Label>
            </td>
            <td class="style7">
                
                <asp:Label ID="lblEmpName" runat="server" Font-Names="Verdana" Font-Size="12px" 
                    ForeColor="#666666"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style6">
                <asp:Label ID="Label14" runat="server" Font-Names="Verdana" Text="Location" Font-Bold="True" ForeColor="#666666"
                    Font-Size="12px"></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="lblLocation" runat="server" Font-Names="Verdana" Font-Size="12px" ForeColor="#666666"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style5" valign="top">
                <asp:Label ID="Label4" runat="server" Font-Names="Verdana" Text="Leave Year" Font-Bold="True" ForeColor="#666666"
                    Font-Size="12px"></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="lblLeave" runat="server" Font-Names="Verdana" Font-Size="12px" ForeColor="#666666"></asp:Label>
            </td>
        </tr>
        
        <tr>
            <td class="style6">
                <asp:Label ID="Label6" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666" Text="Leave Start Date"></asp:Label>
            </td>
            <td class="style7">
                <telerik:RadDatePicker ID="radStartDate" runat="server" AutoPostBack="True" ForeColor="#666666"
                    Width="30%" RenderMode="Lightweight" Font-Names="Verdana" Font-Size="12px">
                    <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True"
                        FastNavigationNextText="&amp;lt;&amp;lt;" RenderMode="Lightweight">
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
            </td>
        </tr>
        <tr>
            <td class="style6">
                <asp:Label ID="Label7" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666" Text="Leave End Date"></asp:Label>
            </td>
            <td class="style7">
                <telerik:RadDatePicker ID="radEndDate" runat="server" AutoPostBack="True" ForeColor="#666666"
                    Width="30%" RenderMode="Lightweight" Font-Names="Verdana" Font-Size="12px">
                    <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True"
                        FastNavigationNextText="&amp;lt;&amp;lt;" RenderMode="Lightweight">
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
            </td>
        </tr>
        <tr>
            <td class="style6"> 
                <asp:Label ID="Label10" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666" Text="Length"></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="lbllength" runat="server" Font-Names="Verdana" ForeColor="#666666" Font-Size="12px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style6"> 
                <asp:Label ID="Label16" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666" Text="Number of Days"></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="lblDays" runat="server" Font-Names="Verdana" ForeColor="#666666" Font-Size="12px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style6" valign="top">
                <asp:Label ID="Label8" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666" Text="Reason"></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="lblreason" runat="server" Font-Names="Verdana" ForeColor="#666666" Font-Size="12px"></asp:Label>
            </td>
        </tr>
        <tr id="payline" runat="server">
            <td class="style6">
                <asp:Label ID="lblAllowance" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                    Text="Allowance Pay Date"></asp:Label>
            </td>
            <td class="style7">
                
                <asp:Label ID="lblPayDate" runat="server" Font-Names="Verdana" ForeColor="#666666" Font-Size="12px"></asp:Label>
                
            </td>
        </tr>
        <tr>
            <td class="style5" valign="top">
                <asp:Label ID="lblfilelabel" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                    Text="File Name"></asp:Label>
            </td>
            <td class="style8">
                <asp:Label ID="lblfilename" runat="server" Font-Names="Verdana" ForeColor="#666666"
                    Font-Size="12px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td >
                </td>
            <td >
                <asp:LinkButton ID="lnkDownloadAttach" runat="server" Font-Names="Verdana" 
                    Font-Size="11px" Visible="False">Download Attachment</asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td class="style5" valign="top">
                <asp:Label ID="Label3" runat="server" Font-Names="Verdana" Font-Bold="True" ForeColor="#666666"
                    Text="Manager" Font-Size="12px"></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="lblSupervisor" runat="server" Font-Names="Verdana" ForeColor="#666666"
                    Font-Size="12px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style6">
                <asp:Label ID="lblStatus1" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                    Text="Manager Approval Status"></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="lblSupervisorApproval" runat="server" Font-Names="Verdana" ForeColor="#666666"
                    Font-Size="12px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style6">
                <asp:Label ID="Label5" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                    Text="Manager Comment"></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="lblMgrComment" runat="server" Font-Names="Verdana" ForeColor="#666666"
                    Font-Size="12px" BorderColor="#CCCCCC" BorderStyle="Solid" 
                    BorderWidth="1px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style6">
            </td>
            <td class="style7">
            </td>
        </tr>
        <tr>
            <td class="style6">
                <asp:Label ID="lblStatus2" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666" Text="Approval Status HR"></asp:Label>
            </td>
            <td class="style7">
                                                <telerik:RadComboBox ID="radApproval" ForeColor="#666666"
                    runat="server" 
                                                    Font-Names="Verdana" Font-Size="12px" RenderMode="Lightweight" 
                                                    ResolvedRenderMode="Classic" Width="150px">
                                                    <Items>
                                                        <telerik:RadComboBoxItem runat="server" Owner="radApproval" Text="Pending Approval" 
                                                            Value="Pending Approval" />
                                                        <telerik:RadComboBoxItem runat="server" Owner="radApproval" Text="Cancelled" 
                                                            Value="Cancelled" />
                                                        <telerik:RadComboBoxItem runat="server" Owner="radApproval" Text="Rejected" 
                                                            Value="Rejected" />
                                                        <telerik:RadComboBoxItem runat="server" Owner="radApproval" Text="Taken" 
                                                            Value="Taken" />
                                                    </Items>
                                                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td class="style6" valign="top">
                <asp:Label ID="Label2" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                    Text="HR Comment"></asp:Label>
            </td>
            <td class="style7">
                <asp:TextBox ID="txtComment" runat="server" Width="100%" Font-Names="Verdana" ForeColor="#666666"
                    Font-Size="12px" Height="70px" TextMode="MultiLine"
                    BorderColor="#CCCCCC" BorderWidth="1px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style6">
                <asp:Label ID="lblStatus3" runat="server" Font-Names="Verdana" Font-Size="12px" Text="Created By" Font-Bold="True" ForeColor="#666666"
                    Visible="False" Font-Italic="True" ></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="lblcreatedby" runat="server" Font-Names="Verdana" Font-Size="12px" Visible="False" ForeColor="#666666"
                    Font-Italic="True" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style6">
                <asp:Label ID="lblStatus4" runat="server" Font-Names="Verdana" Font-Size="12px" Text="Created On" Font-Bold="True" ForeColor="#666666"
                    Visible="False" Font-Italic="True" ></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="lblcreatedon" runat="server" Font-Names="Verdana" Font-Size="12px" Visible="False" ForeColor="#666666"
                    Font-Italic="True" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td >
                <asp:Label ID="lblMgrID" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                    Visible="False"></asp:Label>
                </td>
            <td style="border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #C0C0C0"
                class="style7">
                <asp:Label ID="lblGradeApprover1" runat="server" Font-Names="Verdana" Font-Size="12px" ForeColor="#666666" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style6">
                <asp:Label ID="lbleligible" runat="server" Font-Names="Verdana" 
                    Font-Size="12px" Visible="False"
                    Font-Italic="True" ></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="lblEmpID" runat="server" Font-Names="Verdana" Font-Size="12px" 
                    Visible="False"></asp:Label>
                <asp:Label ID="lblGradeLevel" runat="server" Font-Names="Verdana" Font-Size="12px" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style6">
                <asp:Label ID="lblstatustemp" runat="server" Font-Size="X-Small" Text="Label" 
                    Visible="False"></asp:Label>
                <asp:Label ID="lblstatustemp2" runat="server" Font-Size="X-Small" Text="Label" 
                    Visible="False"></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="lblstatus" runat="server" Font-Bold="True" Font-Names="Candara" Font-Size="12px"
                    ForeColor="#FF3300"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style6">
                <asp:Button ID="btnStatus" runat="server" Text="Save" BackColor="#1BA691"
                    ForeColor="White" Width="120px" Height="20px" BorderStyle="None" 
                    Visible="False" Font-Names="Verdana" Font-Size="12px" />
            </td>
            <td class="style7">
                <asp:Button ID="btnCancel" runat="server" Text="Close" BackColor="#FF3300" ForeColor="White"
                    Width="120px" Height="20px" BorderStyle="None" Font-Names="Verdana" 
                    Font-Size="12px" />
                <asp:Label ID="lblpaysuspendstatus" runat="server" Font-Size="X-Small" 
                    Visible="False"></asp:Label>
            </td>
        </tr>
    </table>
    </div></div></div>
    </form>

     <div class="loading" align="center">
        Processing, please wait...<br />
        <br />
        <img src="../../images/loaders.gif" alt="" />
    </div>
</body>
</html>
</asp:Content>