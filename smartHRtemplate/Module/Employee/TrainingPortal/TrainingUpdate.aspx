<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="TrainingUpdate.aspx.vb"
    Inherits="GOSHRM.TrainingUpdate" EnableEventValidation="false" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
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
        .style2
        {
            font-family: Candara;
            font-size: small;
            width: 240px;
            color: #FF3300;
        }
        .style5
        {
            font-family: Candara;
            font-size: medium;
            width: 240px;
        }
        .style7
        {
            width: 240px;
        }
        .style8
        {
            width: 468px;
        }
    </style>
    <script type="text/javascript" id="telerikClientEvents1">
//<![CDATA[

        function drpTrainee_DropDownClosing(sender, args) {
            //Add JavaScript handler code here
            document.getElementById("Button1").click();
        }
//]]>
    </script>
    <script type="text/javascript" id="telerikClientEvents2">
//<![CDATA[

        function cboTrainer_DropDownClosing(sender, args) {
            //Add JavaScript handler code here
            document.getElementById("Button2").click();
        }
//]]>
    </script>
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
<body onunload="window.opener.location=window.opener.location;" style="height: 317px">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
        rel="Stylesheet" type="text/css" />
    <table>
        <tr>
            <td class="style1" colspan="2" style="background-color: #1BA691">
                Training Sessions
                <asp:TextBox ID="txtid" runat="server" Width="13px" Style="font-size: medium; font-family: Candara"
                    Font-Names="Candara" Height="16px" Visible="False"></asp:TextBox>
            </td>
        </tr>
        <%-- <tr>
            <td class="style6">
            </td>
            <td>
                &nbsp;</td>
        </tr>--%>
        <tr>
            <td class="style5">
                <asp:Label ID="Label1" runat="server" Text="Session Name" Font-Names="Verdana" 
                    Font-Size="12px" ForeColor="#666666" Font-Bold="True"></asp:Label>
                <asp:Label ID="Label2" runat="server" Style="color: #FF3300" Text="*"></asp:Label>
            </td>
            <td class="style8">
                <asp:TextBox ID="txtname" runat="server" Width="100%"
                    Font-Names="Verdana" BorderColor="#CCCCCC" BorderWidth="1px" ForeColor="#666666"
                    Font-Size="12px" Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style5">
                <asp:Label ID="Label3" runat="server" Text="Course" Font-Names="Verdana" 
                    Font-Size="12px" ForeColor="#666666" Font-Bold="True"></asp:Label>
                <asp:Label ID="Label4" runat="server" Style="color: #FF3300" Text="*"></asp:Label>
            </td>
            <td class="style8">
                <asp:TextBox ID="txtCourse" runat="server" Width="100%" 
                    Font-Names="Verdana" BorderColor="#CCCCCC" BorderWidth="1px"  ForeColor="#666666"
                    Font-Size="12px" Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style5">
                <asp:Label ID="Label25" runat="server" Text="Coordinator" Font-Names="Verdana" 
                    Font-Size="12px" ForeColor="#666666" Font-Bold="True"></asp:Label>
                <asp:Label ID="Label26" runat="server" Style="color: #FF3300" Text="*"></asp:Label>
            </td>
            <td class="style8">
                <asp:TextBox ID="txtCoordinator" runat="server" Width="100%" 
                    Font-Names="Verdana" BorderColor="#CCCCCC" BorderWidth="1px"  ForeColor="#666666"
                    Font-Size="12px" Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <%-- <tr>
            <td valign="top" class="style5">
                <asp:Label ID="Label5" runat="server" Text="Training Detail"></asp:Label>
            </td>
            <td class="style8">
                <asp:TextBox ID="txtDesc" runat="server" Width="100%" Style="font-size: medium; font-family: Candara"
                    Font-Names="Candara" Height="59px" TextMode="MultiLine" BorderColor="#CCCCCC"
                    BorderWidth="1px"></asp:TextBox>
            </td>
        </tr>--%>
        <tr>
            <td class="style5">
                <asp:Label ID="Label6" runat="server" Text="Scheduled Date" Font-Names="Verdana" 
                    Font-Size="12px" ForeColor="#666666" Font-Bold="True"></asp:Label>
                <asp:Label ID="Label7" runat="server" Style="color: #FF3300" Text="*"></asp:Label>
            </td>
            <td class="style8">
                <asp:TextBox ID="txtScheduleDate" runat="server" Width="100%" 
                    Font-Names="Verdana" BorderColor="#CCCCCC" BorderWidth="1px"  ForeColor="#666666"
                    Font-Size="12px" Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style5">
                <asp:Label ID="Label13" runat="server" Text="Time" Font-Names="Verdana" 
                    Font-Size="12px" ForeColor="#666666" Font-Bold="True"></asp:Label>
                <asp:Label ID="Label14" runat="server" Style="color: #FF3300" Text="*"></asp:Label>
            </td>
            <td class="style8">
                <asp:TextBox ID="txtTime" runat="server" Width="100%" 
                    Font-Names="Verdana" BorderColor="#CCCCCC" BorderWidth="1px" ForeColor="#666666"
                    Font-Size="12px" Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style5">
                <asp:Label ID="Label8" runat="server" Text="Due Date" Font-Names="Verdana" 
                    Font-Size="12px" ForeColor="#666666" Font-Bold="True"></asp:Label>
                <asp:Label ID="Label9" runat="server" Style="color: #FF3300" Text="*"></asp:Label>
            </td>
            <td class="style8">
                <asp:TextBox ID="txtDueDate" runat="server" Width="100%" 
                    Font-Names="Verdana" BorderColor="#CCCCCC" BorderWidth="1px" ForeColor="#666666"
                    Font-Size="12px" Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style5">
                <asp:Label ID="Label21" runat="server" Text="Session Type" Font-Names="Verdana" 
                    Font-Size="12px" ForeColor="#666666" Font-Bold="True"></asp:Label>
                <asp:Label ID="Label22" runat="server" Style="color: #FF3300" Text="*"></asp:Label>
            </td>
            <td class="style8">
                <asp:TextBox ID="txtSessionType" runat="server" Width="100%" 
                    Font-Names="Verdana" BorderColor="#CCCCCC" BorderWidth="1px" ForeColor="#666666"
                    Font-Size="12px" Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style5" valign="top">
                <asp:Label ID="Label23" runat="server" Text="Trainer" Font-Names="Verdana" 
                    Font-Size="12px" ForeColor="#666666" Font-Bold="True"></asp:Label>
                <asp:Label ID="Label24" runat="server" Style="color: #FF3300" Text="*"></asp:Label>
            </td>
            <td class="style8">
               
                <telerik:RadListBox ID="lstTrainer" runat="server" ResolvedRenderMode="Classic" BorderStyle="None" Font-Names="Verdana" 
                    Font-Size="12px" ForeColor="#666666"
                    Enabled="False" Width="100%" RenderMode="Lightweight">
                    <ButtonSettings TransferButtons="All"></ButtonSettings>
                </telerik:RadListBox>
            </td>
        </tr>
        <tr>
            <td class="style5">
                <asp:Label ID="Label10" runat="server" Text="Delivery Method" Font-Names="Verdana" 
                    Font-Size="12px" ForeColor="#666666" Font-Bold="True"></asp:Label>
                <asp:Label ID="Label11" runat="server" Style="color: #FF3300" Text="*"></asp:Label>
            </td>
            <td class="style8">
                <asp:TextBox ID="txtDeliveryMethod" runat="server" Width="100%" ForeColor="#666666"
                    Font-Names="Verdana" BorderColor="#CCCCCC" BorderWidth="1px" 
                    Font-Size="12px" Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style5" valign="top">
                <asp:Label ID="Label12" runat="server" Text="Training Location" Font-Names="Verdana" 
                    Font-Size="12px" ForeColor="#666666" Font-Bold="True"></asp:Label>
            </td>
            <td class="style8">
                <asp:TextBox ID="txtLocation" runat="server" Width="100%" Font-Names="Verdana" 
                    Font-Size="12px" ForeColor="#666666" BorderColor="#CCCCCC" BorderWidth="1px"
                    Height="98px" TextMode="MultiLine" Enabled="False" ></asp:TextBox>
            </td>
        </tr>
        
        <tr>
            <td class="style5">
                <asp:Label ID="Label16" runat="server" Text="Currency" Font-Names="Verdana" 
                    Font-Size="12px" ForeColor="#666666" Font-Bold="True"></asp:Label>
            </td>
            <td class="style8">
                <asp:TextBox ID="txtCurrency" runat="server" Width="100%" 
                    Font-Names="Verdana" BorderColor="#CCCCCC" BorderWidth="1px"  ForeColor="#666666"
                    Font-Size="12px" Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style5">
                <asp:Label ID="Label27" runat="server" Text="Cost" Font-Names="Verdana" 
                    Font-Size="12px" ForeColor="#666666" Font-Bold="True"></asp:Label>
            </td>
            <td class="style8">
                <asp:TextBox ID="txtCost" runat="server" Font-Names="Verdana" Font-Size="12px" ForeColor="#666666"
                    Width="100%" BorderColor="#CCCCCC" BorderWidth="1px"></asp:TextBox>
            </td>
        </tr>
        <%--   <tr>
            <td class="style20" valign="top" style="border-bottom-style: solid; border-bottom-width: thin;
                border-bottom-color: #C0C0C0">
                <asp:Label ID="lblHR0" runat="server" Font-Names="Candara" 
                    Text="Requires HR Approval"></asp:Label>
                </td>
            <td style="border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #C0C0C0">
                <telerik:RadComboBox ID="cboHRRequired" runat="server" Filter="Contains"
                    AutoPostBack="True" EnableCheckAllItemsCheckBox="True" RenderMode="Lightweight"
                    Width="100%">
                </telerik:RadComboBox>
       
                        
       
            </td>
        </tr>--%><%--  <tr>
            <td class="style20" valign="top" style="border-bottom-style: solid; border-bottom-width: thin;
                border-bottom-color: #C0C0C0">
                <asp:Label ID="lblHR" runat="server" Font-Names="Candara" 
                    Text="HR Personnel to approve Session"></asp:Label>
                </td>
            <td style="border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #C0C0C0">
       <asp:TextBox ID="txtHR" runat="server" 
            Font-Names="Candara" Font-Size="Small"
                            Width="100%" Enabled="False" AutoPostBack="True" 
                    BorderColor="#CCCCCC" BorderWidth="1px"></asp:TextBox>
        <telerik:RadDropDownList ID="radHR" runat="server" 
            DefaultMessage="-- Select --" Font-Names="Candara" Font-Size="Medium" 
            Height="16px" Width="100%" 
            DataTextField="Employee" DataValueField="EmpID" 
            ResolvedRenderMode="Classic" AutoPostBack="True">
        </telerik:RadDropDownList>
       
                        
       
            </td>
        </tr>--%>
        <tr>
            <td class="style5" valign="top">
                <asp:Label ID="Label20" runat="server" Text="Trainees" Font-Names="Verdana" 
                    Font-Size="12px" ForeColor="#666666" Font-Bold="True"></asp:Label>
            </td>
            <td class="style8">
               
                <telerik:RadListBox ID="lstTrainee" runat="server" ResolvedRenderMode="Classic" BorderStyle="None" Font-Names="Verdana" 
                    Font-Size="12px" ForeColor="#666666"
                    Enabled="False" Width="100%" RenderMode="Lightweight">
                    <ButtonSettings TransferButtons="All"></ButtonSettings>
                </telerik:RadListBox>
            </td>
        </tr>
        <tr>
            <td class="style7" style="border-bottom-style: solid; border-bottom-width: thin;
                border-bottom-color: #C0C0C0">
                &nbsp;
            </td>
            <td style="border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #C0C0C0"
                class="style8">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="style2">
                * Required Field
            </td>
            <td class="style8">
                <asp:Label ID="lblstatus" runat="server" Font-Bold="True" Font-Names="Candara" Font-Size="Small"
                    ForeColor="#FF3300"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style7">
                <asp:Button ID="btnAdd" runat="server" Text="Apply" BackColor="#1BA691" ForeColor="White"
                    Width="120px" Height="20px" BorderStyle="None" Font-Names="Verdana" 
                    Font-Size="11px" />
            </td>
            <td class="style8">
                <asp:Button ID="btnCancel" runat="server" Text="Close" BackColor="#FF3300" ForeColor="White"
                    Width="120px" Height="20px" BorderStyle="None" Font-Names="Verdana" 
                    Font-Size="11px" />
        <asp:Button ID="Button2" runat="server" BackColor="White" BorderStyle="None" 
            Enabled="False" Font-Size="9px" />
        <asp:Button ID="Button1" runat="server" BackColor="White" BorderStyle="None" 
            Enabled="False" Font-Size="9px" />
            </td>
        </tr>
    </table>
    
    </form>
</body>
</html>
