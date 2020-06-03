<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="StaffRequisitionUpdate.aspx.vb"
    Inherits="GOSHRM.StaffRequisitionUpdate" EnableEventValidation="false" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
            font-weight: bold;
        }
        .lbl
        {
            font-family: Candara;
            font-size: 12px;
        }
        .style12
        {
            font-size: 12px;
        }
        .style19
        {
            width: 229px;
        }
        .style20
        {
            font-family: Candara;
            font-size: 12px;
            width: 229px;
        }
        .style21
        {
            width: 400px;
        }
        .style22
        {
            font-family: Candara;
            font-size: 12px;
            width: 400px;
        }
        .style24
        {
            font-family: Candara;
            font-size: 12px;
            width: 153px;
            height: 17px;
        }
        .style25
        {
            width: 153px;
        }
        .style26
        {
            font-family: Candara;
            font-size: 12px;
            width: 153px;
        }
        </style>
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

     <script type="text/javascript">
         function Complete() {
             var confirm_value = document.createElement("INPUT");
             confirm_value.type = "hidden";
             confirm_value.name = "confirm_value";
             if (confirm("Mark as complete and send notification to HR?")) {
                 confirm_value.value = "Yes";
             } else {
                 confirm_value.value = "No";
             }
             document.forms[0].appendChild(confirm_value);
         }
    </script>
</head>
<body onunload="window.opener.location=window.opener.location;" style="height: 317px">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.4/jquery.min.js" type="text/javascript"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js"
        type="text/javascript"></script>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css"
        rel="Stylesheet" type="text/css" />
    <%--    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=txtSkills.ClientID %>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "JobPostingsUpdate.aspx/GetSkills",
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('-')[0],
                                    val: item.split('-')[1]
                                }
                            }))
                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },
                select: function (e, i) {
                    var text = this.value.split(/,\s*/);
                    text.pop();
                    text.push(i.item.value);
                    text.push("");
                    this.value = text.join(", ");
                    var value = $("[id*=hfCustomerId]").val().split(/,\s*/);
                    value.pop();
                    value.push(i.item.val);
                    value.push("");
                    $("#[id*=hfCustomerId]")[0].value = value.join(", ");
                    return false;
                },
                minLength: 1
            });
        });   
    </script>--%>
    <table width="100%">
        <tr>
            <td class="style1" colspan="2" style="background-color: #1BA691">
                Staff Requisition<asp:TextBox ID="txtid" runat="server" Width="10px" Style="font-size: medium;
                    font-family: Candara" Font-Names="Candara" Height="10px" Visible="False"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <table>
                    <tr>
                        <td class="style19">
                          
                            <asp:LinkButton ID="lnkApprovalStat" runat="server" Font-Bold="True" Font-Names="Verdana"
                                Font-Size="13px" ToolTip="click here to view / update approval status">Approval Status</asp:LinkButton>
                        </td>
                        <td class="style21">
                            <asp:Label ID="lblstatus" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="11px"
                                ForeColor="#FF3300" Width="100%"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style19">
                            <asp:Label ID="lblcreatedby0" runat="server" Font-Bold="True" 
                                Font-Names="Verdana" Font-Size="12px" ForeColor="#666666">Company*</asp:Label>
                        </td>
                        <td class="style21">
                                    <telerik:RadComboBox ID="radCompany" runat="server" Filter="Contains" AutoPostBack="true" 
                                        Font-Names="Verdana" Font-Size="12px" Width="340px" 
                                ForeColor="#666666">
                                    </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style19">
                            <asp:Label ID="Label2" runat="server" Font-Bold="True" 
                                Font-Names="Verdana" Font-Size="12px" ForeColor="#666666">Dept/Unit*</asp:Label>
                        </td>
                        <td class="style21">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel11" UpdateMode="Always">
                                <ContentTemplate>
                                    <telerik:RadComboBox ID="cbooffice" runat="server" Filter="Contains" AutoPostBack="true" 
                                        Font-Names="Verdana" Font-Size="12px" Width="340px" ForeColor="#666666">
                                    </telerik:RadComboBox>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="radCompany" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                            
                        </td>
                    </tr>
                    <tr>
                        <td class="style19">
                            <asp:Label ID="Label4" runat="server" Font-Bold="True" 
                                Font-Names="Verdana" Font-Size="12px" ForeColor="#666666">Head*</asp:Label>
                        </td>
                        <td class="style21">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel6" UpdateMode="Always">
                                <ContentTemplate>
                                    <asp:Label ID="lblHOD" runat="server" Font-Names="Verdana" Font-Size="12px" 
                                        ForeColor="#666666"></asp:Label>
                                    <asp:Label ID="lblHODID" runat="server" Font-Names="Verdana" Font-Size="1px"></asp:Label>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="cbooffice" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td class="style19">
                            <asp:Label ID="Label5" runat="server" Font-Bold="True" 
                                Font-Names="Verdana" Font-Size="12px" ForeColor="#666666">Hiring Manager*</asp:Label>
                        </td>
                        <td class="style21">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel12" UpdateMode="Always">
                                <ContentTemplate>
                                     <telerik:RadComboBox ID="cboHiringManager" runat="server" Font-Names="Verdana" Font-Size="12px"
                                Width="340px" Filter="Contains" ForeColor="#666666">
                            </telerik:RadComboBox>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="radCompany" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                           
                        </td>
                    </tr>
                       <tr>
                        <td class="style20" valign="top">
                            <asp:Label ID="Label8" runat="server" Font-Bold="True" 
                                Font-Names="Verdana" Font-Size="12px" ForeColor="#666666">Job Title*</asp:Label>
                        </td>
                        <td class="style21">
                            <telerik:RadComboBox ID="cboJobTitle" runat="server" Width="340px" Filter="Contains"
                                        AutoPostBack="True" Font-Names="Verdana" Font-Size="12px" 
                                ForeColor="#666666">
                                    </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style20" valign="top">
                           <asp:Label ID="Label6" runat="server" Font-Bold="True" 
                                Font-Names="Verdana" Font-Size="12px" ForeColor="#666666">Job Grade*</asp:Label>
                        </td>
                        <td class="style21">
                            <telerik:RadComboBox ID="cboGrade" runat="server" Width="340px" 
                                Filter="Contains" Font-Names="Verdana" Font-Size="12px" 
                                ForeColor="#666666">
                                    </telerik:RadComboBox>
                        </td>
                    </tr>
                   
                    <tr>
                        <td class="style20" valign="top">
                            <asp:Label ID="Label7" runat="server" Font-Bold="True" 
                                Font-Names="Verdana" Font-Size="12px" ForeColor="#666666">Job Type*</asp:Label>
                        </td>
                        <td class="style21">
                            <telerik:RadDropDownList ID="radJobType" runat="server" DefaultMessage="-- Select --"
                                Font-Names="Verdana" Font-Size="12px" Height="16px" Width="340px" 
                                RenderMode="Lightweight" ForeColor="#666666">
                            </telerik:RadDropDownList>
                        </td>
                    </tr>
                  
                    <tr>
                        <td class="style20" valign="top">
                            <asp:Label ID="Label9" runat="server" Font-Bold="True" 
                                Font-Names="Verdana" Font-Size="12px" ForeColor="#666666">Lastest Resumption Date*</asp:Label>
                        </td>
                        <td class="style21" valign="top">
                            <telerik:RadDatePicker ID="datLastResumption" runat="server" MinDate="" Font-Names="Verdana"
                                Font-Size="12px" Width="150px" ForeColor="#666666">
                                <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True"
                                    FastNavigationNextText="&amp;lt;&amp;lt;">
                                </Calendar>
                                <DateInput DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" 
                                    LabelWidth="40%">
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
                        <td class="style20" valign="top">
                            <asp:Label ID="Label10" runat="server" Font-Bold="True" 
                                Font-Names="Verdana" Font-Size="12px" ForeColor="#666666">Min. Education*</asp:Label>
                        </td>
                        <td class="style21">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Always">
                                <ContentTemplate>
                                    <telerik:RadComboBox ID="cboEducation" runat="server" AutoPostBack="True" 
                                        Width="340px" Font-Names="Verdana" Font-Size="12px" ForeColor="#666666" 
                                        RenderMode="Lightweight">
                                    </telerik:RadComboBox>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="cboGrade" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="cboJobTitle" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="radJobType" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="datLastResumption" EventName="SelectedDateChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td class="style20" valign="top">
                            <asp:Label ID="Label11" runat="server" Font-Bold="True" 
                                Font-Names="Verdana" Font-Size="12px" ForeColor="#666666">Experience*</asp:Label>
                        </td>
                        <td class="style21" valign="top">
                            <telerik:RadComboBox ID="cboExperience" runat="server" Width="340px" 
                                Filter="Contains" Font-Names="Verdana" Font-Size="12px" 
                                ForeColor="#666666">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style20" valign="top">
                            <asp:Label ID="Label12" runat="server" Font-Bold="True" 
                                Font-Names="Verdana" Font-Size="12px" ForeColor="#666666">Years of Experience*</asp:Label>
                        </td>
                        <td class="style21" valign="top">
                            <asp:TextBox ID="txtYrStart" runat="server" Width="50px" Font-Names="Verdana" Font-Size="12px"
                                BorderColor="#CCCCCC" BorderWidth="1px" TextMode="Number" 
                                ForeColor="#666666">0</asp:TextBox>
                            &nbsp;<asp:Label ID="Label3" runat="server" Font-Names="Candara" Font-Size="Small"
                                Text="-"></asp:Label>
                            &nbsp;<asp:TextBox ID="txtYrEnd" runat="server" Width="50px" Style="font-size: 12px;
                                font-family: Verdana" BorderColor="#CCCCCC" BorderWidth="1px" 
                                TextMode="Number" Font-Names="Verdana" Font-Size="12px" ForeColor="#666666">0</asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style20" valign="top">
                            <asp:Label ID="Label13" runat="server" Font-Bold="True" 
                                Font-Names="Verdana" Font-Size="12px" ForeColor="#666666">Age Range*</asp:Label>
                        </td>
                        <td class="style21" valign="top">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel7" UpdateMode="Always">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtAgeMin" runat="server" Width="50px" Font-Names="Verdana" Font-Size="12px"
                                        BorderColor="#CCCCCC" BorderWidth="1px" TextMode="Number" 
                                        ForeColor="#666666">0</asp:TextBox>
                                    &nbsp;<asp:Label ID="Label1" runat="server" Font-Names="Candara" Font-Size="Small"
                                        Text="-"></asp:Label>
                                    &nbsp;<asp:TextBox ID="txtAgeMax" runat="server" Width="50px" Font-Names="Verdana"
                                        Font-Size="12px" BorderColor="#CCCCCC" BorderWidth="1px" TextMode="Number" 
                                        ForeColor="#666666">0</asp:TextBox>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="cboGrade" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="cboJobTitle" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="radJobType" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="datLastResumption" EventName="SelectedDateChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td class="style20" valign="top">
                            <asp:Label ID="Label14" runat="server" Font-Bold="True" 
                                Font-Names="Verdana" Font-Size="12px" ForeColor="#666666">Specialisation*</asp:Label>
                        </td>
                        <td class="style21" valign="top">
                            <telerik:RadComboBox ID="cboSpecialisation" runat="server" Font-Names="Verdana" Font-Size="12px"
                                Width="340px" Filter="Contains" ForeColor="#666666">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style20" valign="top">
                            <asp:Label ID="Label15" runat="server" Font-Bold="True" 
                                Font-Names="Verdana" Font-Size="12px" ForeColor="#666666">Budgeted Position</asp:Label>
                        </td>
                        <td class="style21" valign="top">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Always">
                                <ContentTemplate>
                                    <asp:Label ID="lblBNo" runat="server" Font-Names="Verdana" Font-Size="12px" 
                                        ForeColor="#666666">0</asp:Label>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="cbooffice" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="cboGrade" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="cboJobTitle" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="radJobType" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="datLastResumption" EventName="SelectedDateChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td class="style20" valign="top">
                            <asp:Label ID="Label16" runat="server" Font-Bold="True" 
                                Font-Names="Verdana" Font-Size="12px" ForeColor="#666666">Filled Position</asp:Label>
                        </td>
                        <td class="style21" valign="top">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel8" UpdateMode="Always">
                                <ContentTemplate>
                                    <asp:Label ID="lblCurrentPositions" runat="server" Font-Names="Verdana"
                                        Font-Size="12px" ForeColor="#666666">0</asp:Label>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="cbooffice" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="cboGrade" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="cboJobTitle" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="radJobType" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="datLastResumption" EventName="SelectedDateChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td class="style20" valign="top">
                            <asp:Label ID="Label18" runat="server" Font-Bold="True" 
                                Font-Names="Verdana" Font-Size="12px" ForeColor="#666666">Numbers Required*</asp:Label>
                        </td>
                        <td class="style21" valign="top">
                            <asp:TextBox ID="txtPositions" runat="server" Width="50px" 
                                CssClass="style12" BorderColor="#CCCCCC" BorderWidth="1px" 
                                Font-Names="Verdana" Font-Size="12px" ForeColor="#666666">1</asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style20" valign="top">
                            <asp:Label ID="Label17" runat="server" Font-Bold="True" 
                                Font-Names="Verdana" Font-Size="12px" ForeColor="#666666">Job Description*</asp:Label>
                        </td>
                        <td class="style21">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtJobDesc" runat="server" BorderColor="#CCCCCC" BorderWidth="1px"
                                        Font-Names="Verdana" Font-Size="12px" Height="162px" TextMode="MultiLine" 
                                        Width="340px" ForeColor="#666666"></asp:TextBox>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="cboJobTitle" EventName="SelectedIndexChanged" />
                          
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td class="style20" valign="top">
                            <asp:Label ID="Label19" runat="server" Font-Bold="True" 
                                Font-Names="Verdana" Font-Size="11px" ForeColor="#666666">Created By</asp:Label>
                        </td>
                        <td class="style22">
                            <asp:Label ID="lblcreatedby" runat="server" Font-Names="Verdana" 
                                Font-Size="11px" ForeColor="#666666"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style20" valign="top">
                            <asp:Label ID="Label20" runat="server" Font-Bold="True" 
                                Font-Names="Verdana" Font-Size="11px" ForeColor="#666666">Created On</asp:Label>
                        </td>
                        <td class="style22">
                            <asp:Label ID="lblcreatedon" runat="server" Font-Names="Verdana" 
                                Font-Size="11px" ForeColor="#666666"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style20" valign="top">
                           <asp:Label ID="Label21" runat="server" Font-Bold="True" 
                                Font-Names="Verdana" Font-Size="11px" ForeColor="#666666">Updated By</asp:Label>
                        </td>
                        <td class="style22">
                            <asp:Label ID="lblupdatedby" runat="server" Font-Names="Verdana" 
                                Font-Size="11px" ForeColor="#666666"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style20" valign="top">
                            <asp:Label ID="Label22" runat="server" Font-Bold="True" 
                                Font-Names="Verdana" Font-Size="11px" ForeColor="#666666">Updated On</asp:Label>
                        </td>
                        <td class="style22">
                            <asp:Label ID="lblupdatedon" runat="server" Font-Names="Verdana" 
                                Font-Size="11px" ForeColor="#666666"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style19">
                            <asp:Button ID="btnAdd" runat="server" Text="Save Detail" BackColor="#1BA691" ForeColor="White"
                                Width="120px" Height="25px" BorderStyle="None" Font-Size="11px" 
                                Font-Names="Verdana" Font-Bold="True" />
                        </td>
                        <td class="style21" >
                            <table width="100%" >
                                <tr>
                                    <td style="width:50%" >
                                        <asp:Button ID="btnComplete" runat="server" Text="Complete" BackColor="#3399FF" ForeColor="White"
                                Width="120px" Height="25px" BorderStyle="None" Font-Size="11px" 
                                Font-Names="Verdana" Font-Bold="True" onclientclick="Complete()" />
                                    </td>
                                      <td style="width:50%" >
                                        <asp:Button ID="btnCancel" runat="server" Text="Close" BackColor="#FF3300" ForeColor="White"
                                Width="120px" Height="25px" BorderStyle="None" Font-Size="11px" 
                                Font-Names="Verdana" Font-Bold="True" />
                                    </td>
                                </tr>
                            </table>
                            
                        </td>
                    </tr>
                </table>
            </td>
            <td valign="top">
                <table>
                    <tr>
                        <td class="style25">
                            &nbsp;
                            </td>
                        <td class="style21">
                            
                        </td>
                    </tr>
                    <tr>
                        <td class="style26" valign="top">
                            <asp:Label ID="Label23" runat="server" Font-Bold="True" 
                                Font-Names="Verdana" Font-Size="12px" ForeColor="#666666">Skills*</asp:Label>
                        </td>
                        <td class="style21">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel5" UpdateMode="Always">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtCompetency" runat="server" Width="340px" Style="margin-left: 0px;"
                                        Height="135px" TextMode="MultiLine" BorderColor="#CCCCCC" Font-Names="Verdana"
                                        Font-Size="12px" BorderWidth="1px" Enabled="True" ForeColor="#666666"></asp:TextBox>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="cboJobTitle" EventName="SelectedIndexChanged" />
                            
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                       <td class="style26" valign="top">
                            <asp:Label ID="Label24" runat="server" Font-Bold="True" 
                                Font-Names="Verdana" Font-Size="12px" ForeColor="#666666">Location*</asp:Label>
                        </td>
                        <td class="style21">
                               <asp:UpdatePanel runat="server" ID="UpdatePanel9" UpdateMode="Always">
                                <ContentTemplate>
                                    <telerik:RadComboBox ID="cboLocation" runat="server" CheckBoxes="True" 
                                        Width="340px" ForeColor="#666666"
                                 Filter="Contains" AutoPostBack="True" RenderMode="Lightweight" 
                                        Font-Names="Verdana" Font-Size="12px">
                            </telerik:RadComboBox>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="cbooffice" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                              <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Always">
                                <ContentTemplate>
                                     <asp:TextBox ID="txtLocation" runat="server" Width="340px" Font-Names="Verdana" Font-Size="12px" ForeColor="#666666"
                                Height="150px" TextMode="MultiLine" BorderColor="#CCCCCC" BorderWidth="1px" Enabled="True"></asp:TextBox>
                          
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="cboLocation" EventName="ItemChecked" />
                                     <asp:AsyncPostBackTrigger ControlID="cboLocation" EventName="CheckAllCheck" />
                                </Triggers>
                            </asp:UpdatePanel>
                          
                        </td>
                    </tr>
                    <tr>
                        <td class="style26" valign="top">
                            <asp:Label ID="Label25" runat="server" Font-Bold="True" 
                                Font-Names="Verdana" Font-Size="12px" ForeColor="#666666">Requisition Type*</asp:Label>
                            
                        </td>
                        <td class="style21">
                            
                            <telerik:RadDropDownList ID="radHiringType" runat="server" DefaultMessage="-- Select --" ForeColor="#666666"
                                Font-Names="Verdana" Font-Size="12px" Height="16px" Width="340px" RenderMode="Lightweight"
                                AutoPostBack="True">
                            </telerik:RadDropDownList>
                            
                        </td>
                    </tr>
                    <tr>
                     <td class="style24" valign="top">  
                            <asp:UpdatePanel runat="server" ID="UpdatePanel10" UpdateMode="Always">
                                <ContentTemplate>
                                   <asp:Label ID="lblrequisition" runat="server" Font-Names="Verdana" 
                                        Font-Size="12px" ForeColor="#666666" Font-Bold="True"></asp:Label>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="radHiringType" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>                                                                                 
                        </td>
                        <td class="style21">
                            
                            <asp:TextBox ID="txtreason" runat="server" BorderColor="#CCCCCC" BorderWidth="1px" ForeColor="#666666"
                                Font-Names="Verdana" Font-Size="12px" Height="60px" TextMode="MultiLine" Width="340px"></asp:TextBox>
                            
                        </td>
                    </tr>
                    <tr>
                     <td class="style24" valign="top">  
                            <asp:Button ID="Button2" runat="server" BackColor="White" BorderStyle="None" Font-Size="10px"
                                Height="15px" />
                        </td>
                        <td class="style21">
                            
                            <asp:Button ID="Button1" runat="server" BackColor="White" BorderStyle="None" Font-Size="10px"
                                Height="15px" />
                            
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
