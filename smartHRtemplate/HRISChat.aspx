<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="HRISChat.aspx.vb" Inherits="GOSHRM.HRISChat" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>


    <html xmlns="http://www.w3.org/1999/xhtml">
    <title>Chat</title>
    <link rel="icon" type="image/png" href="images/Messages-512.png" />
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
        body
        {
            font-family: Arial;
            font-size: 10pt;
        }
        td
        {
            cursor: pointer;
        }
        .hover_row
        {
            background-color: #A1DCF2;
        }
        .lbl
        {
            font-family: Candara;
            color: #000000;
        }
        
        .notes
{
margin-bottom:5px;
font:Arial;
font-size: 13.5px; 
color: #000000;
width: 365px;
height :200px;
padding: 5px;
background-color: #fff;
background-repeat: repeat;
display: block;
overflow:auto;
}
.roundedcorners
{
-webkit-border-radius: 15px;
-khtml-border-radius: 15px;	
-moz-border-radius: 15px;
border-radius: 15px;
}

        .RadDropDownList { display:inline-block !important; 
                                               width: 225px !important; }
                            .RadDropDownList_Default{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.RadDropDownList{width:160px;line-height:1.3333em;text-align:left;display:inline-block;vertical-align:middle;white-space:nowrap;cursor:default;*zoom:1;*display:inline} .RadDropDownList { display:inline-block !important; 
                                               width: 225px !important; }
                            .RadDropDownList_Default{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.RadDropDownList{width:160px;line-height:1.3333em;text-align:left;display:inline-block;vertical-align:middle;white-space:nowrap;cursor:default;*zoom:1;*display:inline}.RadDropDownList_Default .rddlInner{border-radius:3px;border-color:#aaa;color:#333;background-color:#c1c1c1;background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radGradientButtonSprite.png');background-image:linear-gradient(#fafafa,#e6e6e6)}.RadDropDownList .rddlInner{vertical-align:top}.RadDropDownList .rddlInner{padding:2px 22px 2px 5px;border-width:1px;border-style:solid;display:block;position:relative;overflow:hidden}.RadDropDownList_Default .rddlInner{border-radius:3px;border-color:#aaa;color:#333;background-color:#c1c1c1;background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radGradientButtonSprite.png');background-image:linear-gradient(#fafafa,#e6e6e6)}.RadDropDownList .rddlInner{vertical-align:top}.RadDropDownList .rddlInner{padding:2px 22px 2px 5px;border-width:1px;border-style:solid;display:block;position:relative;overflow:hidden}.RadDropDownList_Default .rddlDefaultMessage{color:#a5a5a5}.RadDropDownList .rddlDefaultMessage{font-style:italic}.RadDropDownList .rddlFakeInput{margin:0;padding:0;width:100%;min-height:16px;display:block;overflow:hidden}.RadDropDownList_Default .rddlDefaultMessage{color:#a5a5a5}.RadDropDownList .rddlDefaultMessage{font-style:italic}.RadDropDownList .rddlFakeInput{margin:0;padding:0;width:100%;min-height:16px;display:block;overflow:hidden}
                            .rddlFakeInput {
                                    height: 16px !important; 
                                    width: 80% !important;}
                            .rddlFakeInput {
                                    height: 16px !important; 
                                    width: 80% !important;}.RadDropDownList_Default .rddlIcon{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radActionsSprite.png');background-position:-22px -20px}.RadDropDownList .rddlIcon{width:16px;height:100%;border:0;background-repeat:no-repeat;position:absolute;top:0;right:0}.RadDropDownList_Default .rddlIcon{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radActionsSprite.png');background-position:-22px -20px}.RadDropDownList .rddlIcon{width:16px;height:100%;border:0;background-repeat:no-repeat;position:absolute;top:0;right:0}.rddlSlide{float:left;display:none;position:absolute;overflow:hidden;z-index:7000}.rddlSlide{float:left;display:none;position:absolute;overflow:hidden;z-index:7000}.rddlPopup_Default{border-color:#a0a0a0;color:#333;background-color:#fff;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.rddlPopup{*zoom:1;border-width:1px;border-style:solid;text-align:left;position:relative;cursor:default;width:160px;*width:158px;box-sizing:border-box}.rddlPopup_Default{border-color:#a0a0a0;color:#333;background-color:#fff;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.rddlPopup{*zoom:1;border-width:1px;border-style:solid;text-align:left;position:relative;cursor:default;width:160px;*width:158px;box-sizing:border-box}
    .RadComboBox{text-align:left;display:inline-block;vertical-align:middle;white-space:nowrap;*display:inline;*zoom:1}.RadComboBox_Default{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.RadComboBox .rcbReadOnly .rcbInput{cursor:default}.RadComboBox_Default .rcbReadOnly .rcbInput{color:#333}.RadComboBox .rcbInput{margin:0;padding:2px 0 1px;height:auto;width:100%;border-width:0;outline:0;color:inherit;background-color:transparent;vertical-align:top}.RadComboBox_Default .rcbInput{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif;line-height:16px}
    </style>
    <script type="text/javascript" id="telerikClientEvents1">
//<![CDATA[

	function cboCompany_DropDownClosed(sender,args)
	{
	    //Add JavaScript handler code here
	    document.getElementById("Button3").click();
	}
//]]>
</script>

    <body>
    
        <form id="form1" runat ="server">
        
        <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <asp:Label ID="lblView" runat="server" Font-Names="Candara" Font-Size="20px" Width="100%"
                            Style="font-weight: 700; color: #FF6600"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div style="width: 100%">
            <table width="100%">
                <tr>
                    <td>
                        <asp:Label ID="lblcompany" runat="server" CssClass="lbl" Text="Company" Font-Names="Verdana"
                            Font-Size="11px" Font-Bold="True" ForeColor="#666666"></asp:Label>
                        <telerik:RadComboBox runat="server" DropDownAutoWidth="Enabled" RenderMode="Lightweight"
                            ResolvedRenderMode="Classic" Width="300px" ID="cboCompany"
                            Filter="Contains" Font-Names="Verdana" Font-Size="11px" 
                            ForeColor="#666666" CheckBoxes="True" 
                            onclientdropdownclosed="cboCompany_DropDownClosed">
                        </telerik:RadComboBox>
                        <asp:Button ID="Button2" runat="server" Text="Create Chat" BackColor="White" Font-Names="Verdana"
                            Font-Size="11px" ForeColor="White" Width="5px" Height="20px" BorderStyle="None" />
                        <asp:TextBox ID="txtsearch" runat="server" Width="200px" Height="20px" BorderColor="#CCCCCC"
                            BorderWidth="1px" TextMode="Search" Font-Names="Verdana" Font-Size="11px" ForeColor="#666666"
                            AutoPostBack="True" ToolTip="search users"></asp:TextBox>
                        <asp:Button ID="Button1" runat="server" Text="Create Chat" BackColor="White" Font-Names="Verdana"
                            Font-Size="11px" ForeColor="White" Width="5px" Height="20px" 
                            BorderStyle="None" />
                        <asp:Button ID="btnAdd" runat="server" Text="Open Chat" BackColor="#1BA691" Font-Names="Verdana"
                            Font-Size="11px" ForeColor="White" Width="80px" Height="20px" 
                            BorderStyle="None"  />
                    </td>
                    <td>
                        &nbsp;</td>
                    <td class="style22">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td align="right">
                        &nbsp;
                    </td>
                    <td align="right">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
        <%--'<%# "imgChat.ashx?empid=" + System.Convert.ToString(Eval("empid")) %>'--%>
        <table width="100%">
            <tr style="width :100%">
                <td style="width :100%">
                    <div style="border: 0.5px solid #C0C0C0; height: 600px; width: 100%; overflow-y: scroll; overflow-x: hidden;">
            <asp:UpdatePanel runat="server" ID="UpdatePanel5" UpdateMode="Always">
                <ContentTemplate>
                    <asp:DataList ID="dlBlogs" runat="server" Width="100%" RepeatColumns="1" CellSpacing="2"
                        RepeatLayout="Table" Font-Names="Arial" Font-Size="13px" GridLines="Both" DataKeyField="empid"
                        BorderColor="#CCCCCC" ForeColor="#666666" BorderStyle="Solid"  
                        BorderWidth="1px">
                        <ItemTemplate>
                            <table class="table" width="100%">
                                <tr>
                                    <td valign="top" style="width: 1%">
                                        <asp:CheckBox ID="chkEmp" runat="server"></asp:CheckBox>
                                    </td>
                                    <td valign="top" style="width: 4%">
                                        <asp:ImageButton ID="Image1" runat="server" Height="40px" Width="40px" onerror="this.onerror=null; this.src='/images/user-icon.png';" ImageUrl="~/images/user-icon.png" AlternateText="." CssClass ="roundedcorners"  />
                                    </td>
                                    <td valign="top" style="width: 95%; font-size: 12px">
                                        <b> <%# Eval("name")%> </b> <br />
                                        <asp:Label ID="suserid" runat="server" Height="0px" Text='<%# Eval("userid")%>' Font-Italic="true"   />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="lblchatcount" runat="server"  Text='0' Font-Italic="true" BackColor = "#1BA691" ForeColor="White" Visible ="false"  CssClass="roundedcorners" Width="20px" Height ="20px" style="vertical-align:middle; text-align: center"  />
                                        <br />
                                        <br />
                                        <%# Eval("office")%>
                                        
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:DataList>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="txtsearch" EventName="TextChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cboCompany" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Timer ID="Timer1" runat="server" OnTick="ChatRefresh" Interval="45000" 
                        Enabled="False"  />
                    
                </ContentTemplate>
                <Triggers >
                    <asp:AsyncPostBackTrigger ControlID = "Timer1" EventName = "Tick" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
                </td>
             
            </tr>
        </table>
        
        <div>
            <asp:Button ID="Button3" runat="server" BackColor="White" BorderColor="White" 
                        BorderStyle="Solid" BorderWidth="1px" />
        </div>
        <div>
            <table width="100%">
                <tr>
                    <td>
                    </td>
                </tr>
            </table>
        </div>
     
        <div>
            <div>
                <table class="style21">
                </table>
                <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
                <script type="text/javascript">
                    $(function () {
                        $("[id*=GridVwHeaderChckbox] td").hover(function () {
                            $("td", $(this).closest("tr")).addClass("hover_row");
                        }, function () {
                            $("td", $(this).closest("tr")).removeClass("hover_row");
                        })
                    })
                </script>

            </div>
        </div>
        </form>
    </body>
    </html>

