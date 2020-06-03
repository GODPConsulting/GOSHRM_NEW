<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ShortList_Invitation.aspx.vb"
    Inherits="GOSHRM.ShortList_Invitation" EnableEventValidation="false" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Send Invites</title>
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
            font-size: medium;
        }
        .style5
        {
            font-family: Candara;
            font-size: medium;
            width: 168px;
        }
        .style12
        {
            font-size: medium;
        }
        .style16
        {
            width: 782px;
        }
        .style19
        {
            width: 168px;
        }
    </style>
</head>
<body onunload="window.opener.location=window.opener.location;" style="height: 317px">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
  
    <script type="text/javascript">
        function closeme() {
            window.close();
        }
        window.onblur = closeme;
    </script>

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
    <table>
        <tr>
            <td class="style1" colspan="2" style="background-color: #1BA691">
                Job Invite</td>
        </tr>
     
         <tr>
            <td class="style5" valign="top">
                <asp:Label ID="Label4" runat="server" Font-Names="Verdana" Font-Size="12px" 
                    Text="Invitation Purpose" Font-Bold="True" ForeColor="#666666"></asp:Label>
             </td>
            <td class="style16">
                <telerik:RadComboBox ID="cboInviteType" runat="server"
                    Width="100%" Font-Names="Verdana" Font-Size="12px" ForeColor="#666666">
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td class="style5" valign="top">
                <asp:Label ID="Label2" runat="server" Font-Names="Verdana" Font-Size="12px" ForeColor="#666666" 
                    Text="Message Title" Font-Bold="True"></asp:Label>
             </td>
            <td class="style16">
                <asp:TextBox ID="txtMsgTitle" runat="server" Width="100%" Font-Names="Verdana" Font-Size="12px" ForeColor="#666666"
                    CssClass="style12" BorderColor="#CCCCCC" BorderWidth="1px" 
                    ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style5" valign="top">
                <asp:Label ID="Label1" runat="server" Font-Names="Verdana" Font-Size="12px" ForeColor="#666666" 
                    Text="Job Title" Font-Bold="True"></asp:Label>
             </td>
            <td class="style16">
                <asp:TextBox ID="txtJob" runat="server" Width="100%"  Font-Names="Verdana" Font-Size="12px" ForeColor="#666666"
                    CssClass="style12" BorderColor="#CCCCCC" BorderWidth="1px" ReadOnly="True" 
                    ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style5" valign="top">
                &nbsp;<asp:Label ID="Label5" runat="server" Font-Names="Verdana" 
                    Font-Size="12px" ForeColor="#666666" 
                    Text="Message Body" Font-Bold="True"></asp:Label>
            </td>
            <td class="style16">
                <asp:TextBox ID="txtPositions" runat="server" Width="100%" Font-Names="Verdana" Font-Size="12px" ForeColor="#666666"
                    CssClass="style12" BorderColor="#CCCCCC" BorderWidth="1px" ReadOnly="True" 
                    Height="56px" Font-Italic="True">Dear Applicant,</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style19" valign="top">
                &nbsp;</td>
            <td class="style16">
                
                <telerik:RadTextBox ID="RadTextBox1" Runat="server" 
                    DisplayText="Type Message Here..." EmptyMessage="Type Message Here..." 
                    Font-Names="Verdana" Font-Size="12px" ForeColor="#666666" Height="465px" TextMode="MultiLine" 
                    Width="100%">
                </telerik:RadTextBox>
                
            </td>
        </tr>
        <tr>
            <td class="style19" valign="top">
                        <asp:Button ID="btnAdd" runat="server" Text="Send" BackColor="#1BA691" ForeColor="White"
                            Width="80px" Height="20px" BorderStyle="None" Font-Names="Verdana" 
                            Font-Size="11px" />
               </td>
            <td class="style16">
                
                
                
                        <asp:Button ID="btnClose" runat="server" Text="Cancel" 
                    BackColor="#1BA691" ForeColor="White"
                            Width="80px" Height="20px" BorderStyle="None" Font-Names="Verdana" 
                            Font-Size="11px" />
                
                
                
            </td>
        </tr>
        
    </table>
    </form>
</body>
</html>
