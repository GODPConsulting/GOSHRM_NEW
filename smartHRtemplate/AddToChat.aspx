<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AddToChat.aspx.vb" Inherits="GOSHRM.AddToChat" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

    <html xmlns="http://www.w3.org/1999/xhtml">

    <head runat ="server">
    <title>New Conversation</title>
    <link rel="icon" type="image/png" href="images/Messages-512.png" />
  

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
</head>
    <body>
        <form id="form1" runat ="server">

        <script type="text/javascript">
            function closeme() {
                window.close();
            }
            window.onblur = closeme;
        </script>

        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
         <div style="width: 100%">
        <asp:Label ID="lblroom" runat="server" Font-Names="Consolas" Font-Size="18px" 
               Text="New Conversion" Font-Bold="True" style="text-align: center" 
                 Width="100%"></asp:Label>
    </div>
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <asp:Label ID="lblView" runat="server" Font-Names="Candara" Font-Size="15px" Width="100%"
                            Style="font-weight: 700; color: #FF6600"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div style="width: 100%">
            <table width="100%">
                <tr>
                    <td>
                        <telerik:RadComboBox runat="server" DropDownAutoWidth="Enabled" RenderMode="Lightweight"
                            ResolvedRenderMode="Classic" Width="150px" ID="cboCompany"
                            Filter="Contains" Font-Names="Verdana" Font-Size="10px" 
                            ForeColor="#666666" CheckBoxes="True" 
                            onclientdropdownclosed="cboCompany_DropDownClosed">
                        </telerik:RadComboBox>
                        <asp:Button ID="Button2" runat="server" Text="Create Chat" BackColor="White" Font-Names="Verdana"
                            Font-Size="11px" ForeColor="White" Width="5px" Height="20px" BorderStyle="None" />
                        <asp:TextBox ID="txtsearch" runat="server" Width="100px" Height="20px" BorderColor="#CCCCCC"
                            BorderWidth="1px" TextMode="Search" Font-Names="Verdana" Font-Size="10px" ForeColor="#666666"
                            AutoPostBack="True" ToolTip="search users"></asp:TextBox>
                        <asp:Button ID="Button1" runat="server" Text="Create Chat" BackColor="White" Font-Names="Verdana"
                            Font-Size="11px" ForeColor="White" Width="5px" Height="20px" BorderStyle="None" />
                        <asp:Button ID="btnAdd" runat="server" Text="Start Conversion" 
                            BackColor="#1BA691" Font-Names="Verdana"
                            Font-Size="10px" ForeColor="White" Width="100px" Height="20px" 
                            BorderStyle="None" />
                    </td>
                    <td>
                        
                    </td>
                </tr>
            </table>
        </div>
        <%--'<%# "imgChat.ashx?empid=" + System.Convert.ToString(Eval("empid")) %>'--%>
        <div style="border: 0.5px solid #C0C0C0; height: 550px; width: 100%; overflow-y: scroll; overflow-x: hidden;">
            <asp:UpdatePanel runat="server" ID="UpdatePanel5" UpdateMode="Always">
                <ContentTemplate>
                    <asp:DataList ID="dlBlogs" runat="server" Width="100%" RepeatColumns="1" CellSpacing="2"
                        RepeatLayout="Table" Font-Names="Arial" Font-Size="12px" GridLines="Both" DataKeyField="empid"
                        BorderColor="#CCCCCC" ForeColor="#666666" BorderStyle="Solid" 
                        BorderWidth="1px">
                        <ItemTemplate>
                            <table class="table" width="100%">
                                <tr>
                                    <td valign="top" style="width: 1%">
                                        <asp:CheckBox ID="chkEmp" runat="server"></asp:CheckBox>
                                    </td>
                                    <td valign="top" style="width: 4%">
                                        <asp:ImageButton ID="Image1" runat="server" Height="40px" Width="40px" 
                                        onerror="this.onerror=null; this.src='/images/user-icon.png';" ImageUrl="~/images/user-icon.png"  AlternateText="." CssClass="roundedcorners"/>
                                    </td>
                                    <td valign="top" style="width: 95%; font-size: 12px">
                                        <%# Eval("name")%><br />
                                        <asp:Label ID="suserid" runat="server" Height="0px" Text='<%# Eval("userid")%>' />
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
            
        </div>
        <div>
            <asp:Button ID="Button3" runat="server" BackColor="White" BorderColor="White" 
                        BorderStyle="Solid" BorderWidth="1px" Font-Size="10px" />
        </div>
        <div>
            <table width="100%">
                <tr>
                    <td>
                    </td>
                </tr>
            </table>
        </div>
        <div style="width: 100%">
            <table width="100%">
                <tr>
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

         <div class="loading" align="center">
        Processing, please wait...<br />
        <br />
        <img src="images/loaders.gif" alt="" />
        </div>
        </form>

       

    </body>
    </html>

