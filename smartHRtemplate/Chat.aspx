<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="false" CodeBehind="Chat.aspx.vb" Inherits="GOSHRM.Chat" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID = "content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Chat</title>
    
    <script type="text/javascript">
        function Alerts() {
            alert("Hello!");
        }
    </script>
    <script type="text/javascript">
        function UpdateMsgScroll() {
            var objDiv = document.getElementById("dvScroll");
            objDiv.scrollTop = objDiv.scrollHeight;
            window.setTimeout("UpdateScroll()", 1);
        }
    </script>
    <script type="text/javascript">
        function UpdateChatScroll() {
            var div = document.getElementById("div_chats");
            var div_position = document.getElementById("div_chats_position");
            var position = parseInt('<%=Request.Form("div_chats_position") %>');
            if (isNaN(position)) {
                position = 0;
            }
            div.scrollTop = position;
            div.onscroll = function () {
                div_position.value = div.scrollTop;
            }
        }

    </script>
    <script type="text/javascript">
        function start() {
            UpdateChatScroll();


        }
    </script>
    <script type="text/javascript">
        window.onload = start;
    </script>
    <script type="text/javascript">
        window.onunload = start;
    </script>
    <script language="javascript" type="text/javascript">
        function OpenPopupCenter(pageURL, title, w, h) {
            var left = (screen.width - w) / 2;
            var top = (screen.height - h) / 4;  // for 25% - devide by 4  |  for 33% - devide by 3
            var targetWin = window.open(pageURL, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
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
            margin-bottom: 5px;
            font: Arial;
            font-size: 13.5px;
            color: #000000;
            width: 365px;
            height: 200px;
            padding: 5px;
            background-color: #fff;
            background-repeat: repeat;
            display: block;
            overflow: auto;
        }
        .roundedcorners
        {
            -webkit-border-radius: 15px;
            -khtml-border-radius: 15px;
            -moz-border-radius: 15px;
            border-radius: 15px;
        }
        
    </style>
</head>
<body>
    <form id="form1">
    <div class="content">
    <div>
        <table width="100%">
            <tr>
                <td>
                    <asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Always">
                        <ContentTemplate>
                            <asp:Label ID="lblView" runat="server" Font-Names="Verdana" Font-Size="20px" Width="100%"
                                Font-Bold="False"></asp:Label>
                            <asp:LinkButton ID="lnkAdd" runat="server" Font-Names="Arial" Font-Size="12px" Style="text-align: center"
                                ToolTip="add participants to create group chat" Font-Bold="True" ForeColor="#009900">Add Participants</asp:LinkButton>&nbsp;
                            <asp:LinkButton ID="lnkExit" runat="server" Font-Names="Arial" Font-Size="12px" Style="text-align: center"
                                ToolTip="delete chat" Font-Bold="True" ForeColor="#CC0000">Leave Chat</asp:LinkButton>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="dlchats" EventName="ItemCommand" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
    <div style="width: 100%">
        <table width="100%">
            <tr>
                <td style="width: 25%">
                    <asp:TextBox ID="txtsearch" runat="server" Width="200px" Height="20px" BorderColor="#CCCCCC"
                        BorderWidth="1px" TextMode="Search" Font-Names="Verdana" Font-Size="11px" ForeColor="#666666"
                        AutoPostBack="True" ToolTip="search chats"></asp:TextBox>
                    <asp:Button ID="btnAdd" runat="server" Text="+" BackColor="White" Font-Names="Verdana"
                        Font-Size="18px" ForeColor="#009933" Width="30px" Height="20px" BorderStyle="None"
                        Font-Bold="True" ToolTip="New Conversion" />
                    <%--<button title ="New Chat" style="background-color:White; border-style:none; color:#009933; font-size:15px; font-weight:bold"  onclick="OpenPopupCenter('AddToChat.aspx'"  ", 'New!?', 400, 600);">+</button>--%>
                </td>
                <td style="width: 75%">
                    <asp:UpdatePanel runat="server" ID="UpdatePanel4" UpdateMode="Always">
                        <ContentTemplate>
                            <asp:Label ID="lblstatus" runat="server" Font-Italic="True" Font-Size="12px" Width="100%"
                                ForeColor="Gray"></asp:Label>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="refresh" EventName="Tick" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
    <%--'<%# "imgChat.ashx?empid=" + System.Convert.ToString(Eval("empid")) %>'--%>
    <table width="100%">
        <tr style="width: 100%">
            <td valign="top" style="width: 25%">
                <div id="div_chats" style="border: 0.5px solid #C0C0C0; height: 600px; width: 100%; overflow-y: scroll; overflow-x: hidden;">                  
                    <asp:UpdatePanel runat="server" ID="UpdatePanel5" UpdateMode="Always">
                        <ContentTemplate>
                            <asp:DataList ID="dlchats" runat="server" Width="100%" RepeatColumns="1" CellSpacing="4"
                                RepeatLayout="Table" Font-Names="Arial" Font-Size="13px" GridLines="Both" DataKeyField="roomid"
                                BorderColor="#CCCCCC" ForeColor="#666666" BackColor="White" BorderStyle="Solid" BorderWidth="1px"
                                ToolTip="Chats">
                                <ItemTemplate>
                                   <table width="100%">
									<tr>
                                            <td valign="top" style="width: 4%">
                                                <asp:ImageButton ID="Image1" runat="server" ToolTip="chats" Height="40px" Width="40px"
                                                    onerror="this.onerror=null; this.src='/images/user-icon.png';" ImageUrl="~/images/user-icon.png"
                                                    AlternateText="." CssClass="roundedcorners" CommandName="chatdetail" CausesValidation="false"
                                                    CommandArgument='<%# Eval("roomid") %>' />
                                            </td>
                                            <td valign="top" style="width: 96%; font-size: 12px">
                                                <asp:LinkButton ID="lnkmembers" runat="server" Font-Underline="false" ForeColor="#666666" Font-Bold="true"
                                                    ToolTip='<%# Eval("members")%>' Text='<%# Eval("members")%>' CommandName="chatdetail"
                                                    CausesValidation="false" CommandArgument='<%# Eval("roomid") %>'></asp:LinkButton>
                                                <br />
                                                <br />
                                                <asp:LinkButton ID="lnkopen" runat="server" ToolTip='<%# Eval("tooltip")%>' Width="70%"
                                                    Font-Italic="true" Font-Underline="false" ForeColor="#666666" Text='<%# Eval("lastmessage") %>'
                                                    CommandName="chatdetail" CausesValidation="false" CommandArgument='<%# Eval("roomid") %>'></asp:LinkButton>
                                                <span id="spchatcount" runat="server" class="badge bg-success pull-right"><%# Eval("isnewmessage")%></span>
                                            
                                                <asp:Label ID="lblmemcount" runat="server" Height="0px" Text='<%# Eval("counts")%>'
                                                    Visible="false" />
                                                <asp:Label ID="lblmember" runat="server" Height="0px" Text='<%# Eval("usermembers")%>'
                                                    Visible="false" />
                                                    <br />
                                            </td>
                                        </tr>
										</table>                             
                                </ItemTemplate>
                            </asp:DataList>
                            <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
                            <script type="text/javascript">
                                $(function () {
                                    $("[id*=dlchats] td").hover(function () {
                                        $("td", $(this).closest("tr")).addClass("hover_row");
                                    }, function () {
                                        $("td", $(this).closest("tr")).removeClass("hover_row");
                                    })
                                })
                            </script>
                            <input type="hidden" id="div_chats_position" name="div_chats_position" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="txtsearch" EventName="TextChanged" />
                            <asp:AsyncPostBackTrigger ControlID="refresh" EventName="Tick" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:Timer ID="refresh" runat="server" OnTick="ChatRefresh" Interval="1000" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="refresh" EventName="Tick" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </td>
            <td valign="top" style="width: 75%">
                <div id="dvScroll" style="border: 0.5px solid #C0C0C0; overflow-y: scroll; overflow-x: hidden;
                    height: 570px; width: 100%">
                    <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Always">
                        <ContentTemplate>
                            <asp:DataList ID="dlcomments" runat="server" Width="100%" RepeatColumns="1" CellSpacing="0"
                                RepeatLayout="Table" Font-Names="Consolas" Font-Size="14px" GridLines="None"
                                DataKeyField="id" BorderColor="#CCCCCC" ForeColor="#666666">
                                <ItemTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td valign="top" style="width: 100%; font-size: 14px"><b>
                                            <asp:Label ID="lbluser" Text='<%# Eval("userid").ToString() %>' runat="server"
                                                     BorderColor="#CCCCCC" Visible="True"
                                                    Font-Size="13px" /></b><asp:Label ID="lblid" Text='<%# Eval("id").ToString() %>' runat="server"
                                                     BorderColor="#CCCCCC" Visible="false"
                                                    Font-Size="1px" />
                                            </td>
                                        </tr>                                    
                                        <tr>
                                            <td style="width: 100%">
                                             
                                                <div id="othersmsg" class="chat chat-left" runat="server" >
                                                    <div class="chat-avatar">
															<a href="#" title="John Doe" data-placement="right" data-toggle="tooltip" class="avatar">
																<img alt="John Doe" src="imgChat.ashx?imgid=<%# Eval("empid").ToString() %>" class="img-responsive img-circle">
															</a>
												    &nbsp;&nbsp;&nbsp;&nbsp;</div>
                                                    <div class="chat-body">
                                                        <div class="chat-bubble">
                                                            <div class="chat-content">
                                                                <p style="font-size:14px"  ><%# Eval("message").ToString() %></p>
                                                                <span class="chat-time"><%# Eval("timestamps")%></span>
                                                            </div>
                                                        </div>                                                        
                                                    </div>
                                                </div>

                                                <div id="selfmsg" runat="server" class="chat chat-right">
                                                    <div class="chat-body">
                                                        <div class="chat-bubble">
                                                            <div class="chat-content">
                                                                <p style="font-size:14px"  ><%# Eval("message").ToString() %></p>
                                                                <span class="chat-time"><%# Eval("timestamps")%></span>
                                                            </div>
                                                            <div class="chat-action-btns">
                                                                <ul>
                                                                    <li><a  runat="server"  title = '<%# Eval("id")%>'  onserverclick = "DeleteMessage" class="del-msg" ><i class="fa fa-trash-o"></i></a>
                                                                    </li>
                                                                </ul>
                                                            </div>
                                                        </div>                                                        
                                                    </div>
                                                </div>
                                              
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:DataList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="refresh" EventName="Tick" />
                            <asp:AsyncPostBackTrigger ControlID="dlchats" EventName="ItemCommand" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div class="chat-footer">
                    <div class="message-bar">
                        <div class="message-inner">
                            <div >
                                <div class="input-group">
                                    <textarea id="txmsg" runat="server" class="form-control" placeholder="Type message..."></textarea>
                                    <span class="input-group-btn">
                                        <button class="btn btn-success" type="button" runat="server" onserverclick="btnPost_Click">
                                            <i class="fa fa-send"></i>
                                        </button>
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:Button ID="btnPost" runat="server" BackColor="transparent" BorderStyle="None" Font-Names="Verdana"
                                    Font-Size="11px" ForeColor="White" Height="25px" Text="" Width="95%" />
                <%--<div style="height: 30px; width: 100%">
                    <table width="100%">
                        <tr>
                            <td style="width: 92%">
                                <asp:TextBox ID="txtmsg" runat="server" BorderColor="#CCCCCC" Font-Names="Consolas"
                                    Font-Size="14px" BorderStyle="Solid" BorderWidth="1px" Width="100%"></asp:TextBox>
                            </td>
                            <td align="right" style="width: 8%">                                
                                    <button id="btnupdate" runat="server" onserverclick="btnPost_Click" type="submit"
                                        style="width: 95%; height: 25px" class="btn btn-primary btn-success"><i class="fa fa-send"></i> 
                                    Save</button>
                            </td>
                        </tr>
                    </table>
                </div>--%>
            </td>
        </tr>
    </table>
    <div>
        <asp:Button ID="Button3" runat="server" BackColor="White" BorderColor="White" BorderStyle="Solid"
            BorderWidth="1px" Font-Size="2px" Height="10px" />
            
    </div>
    <div>
        <table width="100%">
            <tr>
                <td>
                    <asp:Timer ID="reload" runat="server" Enabled="False" />
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
    </div>
    </form>
</body>
</html>
</asp:Content>