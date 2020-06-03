<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="false"
    CodeBehind="Chats.aspx.vb" Inherits="GOSHRM.Chats" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
           <div class="container col-md-12">
                <div class="col-md-12">
                    <div id="divalert" runat="server" visible="false" class="alert alert-info">
                        <strong id="msgalert" runat="server">Danger!</strong>
                        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                            <span aria-hidden="true">&times;</span></button>
                        <asp:Timer ID="reload" runat="server" Enabled="False" />
                    </div>
                </div>
           <div class="panel panel-success">
                        <div class="panel-heading">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel6" UpdateMode="Always">
                        <ContentTemplate>
                            <h4 id="pagetitle" runat="server" class="page-title" style="color: #1BA691">
                                Chats</h4>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="dlchats" EventName="ItemCommand" />
                        </Triggers>
                    </asp:UpdatePanel>
                        </div>
                     <div class="panel-body">  

            <div class="row">
                <div class="col-md-2">
                      <asp:Button ID="lnkAdd" CssClass="btn btn-success" runat="server" Text="Add Participants" ForeColor="White"
                            Width="150px" Height="30px" BorderStyle="None" Font-Names="Verdana" Font-Size="12px" />
                </div>
                <div class="col-md-2">
                        <asp:Button ID="lnkExit" CssClass="btn btn-info" runat="server" Text="Leave Chat" ForeColor="White"
                            Width="100px" Height="30px" BorderStyle="None" Font-Names="Verdana" Font-Size="12px" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">
                    <div class="form-group">
                        <div class="row">
                            <div class="chat-header">
                                    <telerik:RadTextBox ID="txtfind" Runat="server" EmptyMessage="Search..." 
                                                    Skin="Bootstrap" Width="100%" AutoPostBack="True">
                                                </telerik:RadTextBox>
                            </div>
                        </div>
                        <div id="div_chats" style="border: 0.5px solid #C0C0C0; height: 300px; width: 100%;
                            overflow-y: scroll; overflow-x: hidden;">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel5" UpdateMode="Always">
                                <ContentTemplate>
                                    <div class="sidebar-menu">
                                        <ul>
                                            <li class="menu-title">Chats <a href="#" data-toggle="modal" data-target="#add_chat_user">
                                                </a></li>
                                            <asp:DataList ID="dlchats" runat="server" Width="100%" RepeatColumns="1" CellSpacing="1"
                                                RepeatLayout="Table" Font-Names="Arial" Font-Size="12px" GridLines="Both" DataKeyField="roomid"
                                                BorderColor="#CCCCCC" BackColor="White" BorderStyle="Solid" CssClass="table table-condensed"
                                                BorderWidth="1px" ToolTip="Chats">
                                                <ItemTemplate>
                                                    <li><a href="Chats.aspx?id=<%# Eval("roomid")%>" title='<%# Eval("members")%>'><span
                                                        class="status <%# Eval("status")%>"></span>
                                                        <%# Eval("members")%><span class="badge bg-danger pull-right">
                                                            <%# Eval("isnewmessage")%></span></a> </li>
                                                    <asp:Label ID="lblmemcount" runat="server" Font-Size="0px" Text='<%# Eval("counts")%>'
                                                        Visible="false" />
                                                    <asp:Label ID="lblmember" runat="server" Font-Size="1px" Text='<%# Eval("usermembers")%>'
                                                        Visible="false" />
                                                </ItemTemplate>
                                            </asp:DataList>
                                        </ul>
                                    </div>
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
                                    <asp:AsyncPostBackTrigger ControlID="txtfind" EventName="TextChanged" />
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
                    </div>
                </div>
                <div class=" col-md-8">
                    <div class="form-group">                       
                        <div class="row">
                            <div  class="chat-header">
                                <div  class="navbar">
                                    <div class="user-details">
                                        <div class="user-info pull-left">
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Always">
                                                <ContentTemplate>
                                                    <a href="#"><span id="typingstat" runat="server" class="font-bold"></span>
                                                        <i id="typingv" runat="server" class="typing-text"></i></a> <span class="last-seen">
                                                        </span>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="refresh" EventName="Tick" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row" >
                            <div id="dvScroll" style="border: 0.5px solid #C0C0C0; overflow-y: scroll; overflow-x: hidden;
                                height: 250px; width: 100%">
                                <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Always">
                                    <ContentTemplate>
                                        <div class="chat-box">
                                            <div id="chats_list" runat="server" class="chats">
                                                
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="refresh" EventName="Tick" />
                                        <asp:AsyncPostBackTrigger ControlID="dlchats" EventName="ItemCommand" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                       <div class="row">
                            <div class="chat-footer">
                                <div  class="message-bar">
                                    <div class="message-inner">
                                        <div >
                                        <asp:Panel DefaultButton="btnsend">
                                            <div class="input-group">
                                                <%--<a class="link attach-icon" href="#" data-toggle="modal">
                                                    <img src="images/attachment.png" alt="" /></a>--%>
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel4" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                         <textarea id="txmsg" runat="server" class="form-control" cols="1" placeholder="Type message..."></textarea>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnsend" EventName="Click" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                               
                                                 <span class="input-group-btn">
                                                 <asp:LinkButton ID="btnsend" data-toggle="tooltip" data-original-title="Send" Width="60px" Height="54px" runat="server" CssClass="btn btn-success">
                                                    <span style="margin-top:10px" aria-hidden="true" class="fa fa-send"></span>
                                                </asp:LinkButton>                                       
                                                </span>
                                            </div>
                                            </asp:Panel> 
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>                       
                    </div>                                  
                </div>
            </div>
           </div>
           </div></div></div>
        </form>
    </body>
    </html>
</asp:Content>
