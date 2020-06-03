<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="BlackBoardView.aspx.vb"
    Inherits="GOSHRM.BlackBoardView" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagPrefix="asp" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <title></title>
     <script type="text/javascript">
         document.getElementById("txtpost")
        .addEventListener("keyup", function (event) {
            event.preventDefault();
            if (event.keyCode === 13) {
                alert('am here');
                btnPost_Click
            }
        });
    </script>
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
<%--    <script type="text/javascript">
       window.onload = function setHeight() {
            var tt = document.getElementById("<%=txtmessage.ClientID%>");
            tt.style.height = tt.scrollHeight + "px";

            var t = document.getElementById("<%=txtbody.ClientID%>");
            t.style.height = t.scrollHeight + "px";
        }
    </script>--%>

     <script type="text/javascript" id="telerikClientEvents1">
         function OnClientDropDownClosedHandler(sender, eventArgs) {
             document.getElementById("btnRefresh").click();
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
            font-size: 13px;
            color: #000000;
            width: 365px;
            height: 100px;
            padding: 5px;
            background-color: #fff;
            background-repeat: repeat;
            display: block;
            overflow: auto;
        }
    </style>
    <body>
       
        <form>
         <div class="container col-md-10">
           <div class="row">
             <div id="divalert" runat="server" visible="false" class="alert alert-info">
                <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                <strong id="msgalert" runat="server">Danger!</strong>
            </div>
         </div>
       <div class="panel panel-success">
                <div class="panel-heading">
                    <h5><b id="pagetitle" runat="server">Blog Details</b></h5>
                </div>
             <div class="panel-body">
        <div>
            <asp:Button ID="lnkBack" CssClass="btn btn-success" runat="server" Text="Go Back" ForeColor="White"
                            Width="100px" Height="30px" BorderStyle="None" Font-Names="Verdana" Font-Size="12px" />
        </div>
        <div class="row" style="width: 100%">
            <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                <asp:View ID="BlogView" runat="server">
                 <div class="panel panel-success">
                 <div class="panel-body">
                    <div class="row">
                    <div id="blogpost" runat="server" class="activity">
									<%--<div class="activity-box">
										<ul class="activity-list">
											<li>
												<div id="blogimg" runat="server" class="activity-user">
													<a href="#" title="" data-toggle="tooltip" class="avatar" data-original-title="Lesley Grauer">
														<img alt="" src="images/user.jpg" class="img-responsive img-circle">
													</a>
												</div>
												<div class="activity-content">
													<div class="timeline-content">
														<a href="#" class="name"><%=postedby%></a>
														<span class="time"><i class="fa fa-clock-o"></i>&nbsp;<%=postedon%> &nbsp;&nbsp;&nbsp;&nbsp;<i class="fa fa-bank"></i>&nbsp; <%=comp%></span><br />
                                                        <h6><a class="name"><%=topic%></a></h6>
                                                        <span><%=post%></span>
													</div>
												</div>
											</li>
										</ul>
									</div>--%>	
								</div>
                        <asp:Label ID="lblid" runat="server" CssClass="lbl" Font-Bold="False" Font-Names="Consolas"
                                    Font-Size="13px" ForeColor="#666666" Visible="False"></asp:Label>
  <%--                      <div class="col-md-6">
                            <div class="form-group">
                                <label>POSTED ON</label>
                                <input id="" runat="server" readonly class="form-control" type="text" />
                            </div>
                        </div>
                         <div class="col-md-6">
                            <div class="form-group">
                                <label>POSTED BY</label>
                                <input id="" runat="server" readonly class="form-control" type="text" />
                            </div>
                        </div>
                        
                        <div style="display:none" class="col-md-12">
                            <div class="form-group">
                                <label>BLOG POST</label>
                                <textarea id="txtmessage" runat="server" readonly class="form-control" rows="5"></textarea>
                            </div>
                        </div>
                        --%>
                       <div id="chatter" runat="server" class="col-md-12">
                           
                        </div>
                        <div class="chat-footer col-md-12">
									<div class="message-bar">
										<div class="message-inner">
											<a class="link attach-icon" href="#" data-toggle="modal" data-target="#drag_files"><img src="images/attachment.png" alt=""></a>
											<div class=""><div class="input-group">
												<textarea id="txtpost" runat="server" class="form-control" placeholder="Type comment..."></textarea>
												<span class="input-group-btn">
													<button id="btnsend" onserverclick="btnPost_Click" style="width:70px; height:71px;" runat="server" class="btn btn-success" type="submit"><i class="fa fa-send"></i></button>
												</span>
												</div>
											</div>
										</div>
									</div>
								</div>
                    </div>
                 </div>
                 </div>
                    <%--<table width="100%">
                        <tr>
                            <td style="width: 5%">
                                <asp:Label ID="lblid" runat="server" CssClass="lbl" Font-Bold="False" Font-Names="Consolas"
                                    Font-Size="13px" ForeColor="#666666" Visible="False"></asp:Label>
                            </td>
                            <td style="border: 1px solid #CCCCCC; width: 65%;">
                                <asp:Label ID="lblHeader" runat="server" CssClass="lbl" Font-Bold="True" Font-Names="Consolas"
                                    Font-Size="16px" ForeColor="#666666"></asp:Label>
                                <br />
                                <br />
                                <asp:Label ID="Label2" runat="server" CssClass="lbl" Font-Bold="False" Font-Names="Consolas"
                                    Font-Size="13px" ForeColor="#666666">Posted By:</asp:Label>
                                <asp:Label ID="Label3" runat="server" CssClass="lbl" Font-Bold="False" Font-Names="Consolas"
                                    Font-Size="13px" ForeColor="White" Width="5px">tt</asp:Label>
                                <asp:Label ID="lblpostedby" runat="server" CssClass="lbl" Font-Bold="False" Font-Names="Consolas"
                                    Font-Size="13px" ForeColor="#666666"></asp:Label><br />
                                <asp:Label ID="lblHeader0" runat="server" CssClass="lbl" Font-Bold="False" Font-Names="Consolas"
                                    Font-Size="13px" ForeColor="#666666">Posted On:</asp:Label>
                                <asp:Label ID="Label1" runat="server" CssClass="lbl" Font-Bold="False" Font-Names="Consolas"
                                    Font-Size="13px" ForeColor="White" Width="5px">tt</asp:Label>
                                <asp:Label ID="lblpostdate" runat="server" CssClass="lbl" Font-Bold="False" Font-Names="Consolas"
                                    Font-Size="13px" ForeColor="#666666"></asp:Label><br />
                                    <asp:Label ID="lblattachment" runat="server" CssClass="lbl" 
                                    Font-Bold="False" Font-Names="Consolas"
                                    Font-Size="13px" ForeColor="#666666">Attachment:</asp:Label>
                                    <asp:LinkButton ID="lnkattachment" runat="server" Font-Names="Verdana" 
                    Font-Size="13px" Visible="False">Download Attachment</asp:LinkButton>
                                    <br />
                                <br />
                                
                                <asp:TextBox ID="txtmessage" runat="server" Font-Names="Consolas" Font-Size="14px"
                                    ForeColor="#666666" Width="98%" BorderStyle="None" TextMode="MultiLine" 
                                    ReadOnly="True"></asp:TextBox>
                            </td>
                            <td style="width: 30%">
                            </td>
                        </tr>
                    </table>--%>
                </asp:View>
                <asp:View ID="BlogEdit" runat="server">
                <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>HEADING</label>
                                <input id="txtHeading" style="height:30px;" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>BLOG TYPE</label>
                                 <telerik:RadComboBox runat="server" Skin="Bootstrap" RenderMode="Lightweight"
                                    ResolvedRenderMode="Classic" Width="100%" ID="cboBlogType" Filter="Contains" Height="100px"
                                    Font-Names="Verdana" Font-Size="14px" ForeColor="#666666">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>RETIRE DATE</label>
                                <telerik:RadDatePicker ID="datRetireDate" Skin="Bootstrap" runat="server" Width="100%" RenderMode="Lightweight"
                                                Font-Names="Consolas" Font-Size="14px" ForeColor="#666666" ToolTip="date blog will expire">
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
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>
                                    DEPT/OFFICE</label>
                                 <telerik:RadComboBox ID="radoffice" Skin="Bootstrap" runat="server" Filter="Contains" Font-Names="Verdana"
                                    Font-Size="14px" ForeColor="#666666" RenderMode="Lightweight" ResolvedRenderMode="Classic" Width="100%"
                                    CheckBoxes="True" ToolTip="select units/offices that is to see blog"
                                    AutoPostBack="False" EnableCheckAllItemsCheckBox="True">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>ATTACHMENT</label>
                                 <asp:FileUpload ID="imgUpload" runat="server" 
                        Font-Names="Verdana" Font-Size="12px" Width="100%"/>
                            </div>
                        </div>
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>
                                    BODY</label>
                                <textarea id="txtbody" runat="server" class="form-control" rows="5"></textarea>
                                <textarea id="txtapprovalcomment" runat="server" class="form-control" rows="5"></textarea>
                                <asp:LinkButton ID="lnkclear" runat="server" Font-Names="Verdana" 
                    Font-Size="11px" Visible="False" onclientclick="Confirm()">Clear Attachment</asp:LinkButton>
                     <asp:LinkButton ID="LinkButton1" runat="server" Font-Names="Verdana" 
                    Font-Size="11px" Visible="False" onclientclick="Confirm()">Clear Attachment</asp:LinkButton>
                             <asp:LinkButton ID="lnkDownloadAttach" runat="server" Font-Names="Verdana" 
                    Font-Size="11px" Visible="False">Download Attachment</asp:LinkButton>
                    <asp:Label ID="lblfile" runat="server" CssClass="lbl" Font-Bold="True" Font-Names="Consolas"
                                    Font-Size="13px" ForeColor="#666666"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-12 m-t-20 text-center">
                                 <asp:Button ID="btnAdd" runat="server" Text="Save Post" Font-Names="Verdana"
                                    Font-Size="11px" CssClass="btn btn-primary btn-success" ForeColor="White" Width="100px" Height="30px" BorderStyle="None" />
                                
                                <asp:Button ID="Button2" runat="server" Text="Add" BackColor="White" Font-Names="Verdana"
                                    Font-Size="11px" ForeColor="White" Width="10px" Height="20px" BorderStyle="None" />
                                <asp:Button ID="btnDelete" CssClass="btn btn-primary btn-danger" runat="server" Text="Delete" Font-Names="Verdana" Font-Size="11px"
                                    OnClientClick="Confirm()" ForeColor="White" Width="100px"
                                    Height="30px" BorderStyle="None" />
                        </div>
                    </div>
                   <%-- <table width="100%">
                        <tr>
                            <td style="width: 5%">
                            </td>
                            <td style="width: 10%;">
                            </td>
                            <td style="width: 55%;">
                                <asp:Button ID="btnAdd" runat="server" Text="Save Post" BackColor="#1BA691" Font-Names="Verdana"
                                    Font-Size="11px" ForeColor="White" Width="80px" Height="20px" BorderStyle="None" />
                                
                                <asp:Button ID="Button2" runat="server" Text="Add" BackColor="White" Font-Names="Verdana"
                                    Font-Size="11px" ForeColor="White" Width="10px" Height="20px" BorderStyle="None" />
                                <asp:Button ID="btnDelete" runat="server" Text="Delete" Font-Names="Verdana" Font-Size="11px"
                                    OnClientClick="Confirm()" BackColor="#FF3300" ForeColor="White" Width="80px"
                                    Height="20px" BorderStyle="None" />
                            </td>
                            <td style="width: 30%">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 5%">
                            </td>
                            <td style="width: 10%;">
                                <asp:Label ID="Label4" runat="server" CssClass="lbl" Font-Bold="True" Font-Names="Consolas"
                                    Font-Size="13px" ForeColor="#666666">Heading:</asp:Label>
                            </td>
                            <td style="width: 55%;">
                                <asp:TextBox ID="txtHeading" runat="server" BorderColor="#CCCCCC" Font-Names="Consolas"
                                    Font-Size="14px" ForeColor="#666666" BorderStyle="Solid" BorderWidth="1px" ToolTip="Message Title"
                                    Width="99%"></asp:TextBox>
                            </td>
                            <td style="width: 30%">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 5%">
                            </td>
                            <td style="width: 10%;">
                                <asp:Label ID="Label7" runat="server" CssClass="lbl" Font-Bold="True" Font-Names="Consolas"
                                    Font-Size="13px" ForeColor="#666666">Blog Type</asp:Label>
                            </td>
                            <td style="width: 55%;">
                                <telerik:RadComboBox runat="server" DropDownAutoWidth="Enabled" RenderMode="Lightweight"
                                    ResolvedRenderMode="Classic" Width="300px" ID="cboBlogType" Filter="Contains" Height="100px"
                                    Font-Names="Verdana" Font-Size="11px" ForeColor="#666666">
                                </telerik:RadComboBox>
                            </td>
                            <td style="width: 30%">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 5%">
                            </td>
                            <td style="width: 10%;">
                                <asp:Label ID="Label5" runat="server" CssClass="lbl" Font-Bold="True" Font-Names="Consolas"
                                    Font-Size="13px" ForeColor="#666666">Retire Date:</asp:Label>
                            </td>
                            <td style="width: 55%;">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 25%">
                                            <telerik:RadDatePicker ID="datRetireDate" runat="server" Width="110px" RenderMode="Lightweight"
                                                Font-Names="Consolas" Font-Size="12px" ForeColor="#666666" ToolTip="date blog will expire">
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
                                        <td style="width: 10%">
                                            <asp:Button ID="Button1" runat="server" Text="Add" BackColor="White" Font-Names="Verdana"
                                                Font-Size="11px" ForeColor="White" Width="15px" Height="20px" BorderStyle="None" />
                                        </td>
                                        <td align="right" style="width: 65%">
                                            <asp:Label ID="lblstat" runat="server" CssClass="lbl" Font-Bold="True" Font-Names="Consolas"
                                                Font-Size="13px" ForeColor="#666666"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 30%">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 5%">
                            </td>
                            <td valign="top" style="width: 10%;">
                                <asp:Label ID="Label6" runat="server" CssClass="lbl" Font-Bold="True" Font-Names="Consolas"
                                    Font-Size="13px" ForeColor="#666666">Units/Departments:</asp:Label>
                            </td>
                            <td style="width: 55%;">
                                <telerik:RadComboBox ID="radoffice" runat="server" Filter="Contains" Font-Names="Verdana"
                                    Font-Size="12px" ForeColor="#666666" RenderMode="Lightweight" ResolvedRenderMode="Classic" Width="100%"
                                    CheckBoxes="True" ToolTip="select units/offices that is to see blog"
                                    AutoPostBack="False" EnableCheckAllItemsCheckBox="True">
                                </telerik:RadComboBox>

                                <%--<asp:UpdatePanel runat="server" ID="UpdatePanel5" UpdateMode="Always">
                                    <ContentTemplate>
                                       
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="radoffice" EventName="CheckAllCheck" />   
                                        <asp:AsyncPostBackTrigger ControlID="radoffice" EventName="ItemChecked" />                                    
                                    </Triggers>
                                </asp:UpdatePanel>                             
                            </td>
                            <td style="width: 30%">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 5%">
                            </td>
                            <td valign="top" style="width: 10%;">
                                <asp:Label ID="Label9" runat="server" CssClass="lbl" Font-Bold="True" Font-Names="Consolas"
                                    Font-Size="13px" ForeColor="#666666">Attachment:</asp:Label>
                            </td>
                            <td style="width: 55%;">
                                <table width="100%">
                    <tr>
                        <td style="width:40%">
                    <asp:FileUpload ID="imgUpload" runat="server" 
                        Font-Names="Verdana" Font-Size="12px" Width="100%"/>
                        </td>
                        <td align="right"  style="width:30%">
                            <asp:Label ID="lblfile" runat="server" CssClass="lbl" Font-Bold="True" Font-Names="Consolas"
                                    Font-Size="13px" ForeColor="#666666"></asp:Label>
                        </td>
                        <td align="right"  style="width:15%">
                            <asp:LinkButton ID="lnkclear" runat="server" Font-Names="Verdana" 
                    Font-Size="11px" Visible="False" onclientclick="Confirm()">Clear Attachment</asp:LinkButton>
                        </td>
                        <td align="right" style="width:15%">
                             <asp:LinkButton ID="lnkDownloadAttach" runat="server" Font-Names="Verdana" 
                    Font-Size="11px" Visible="False">Download Attachment</asp:LinkButton>
                        </td>
                    </tr>
                </table>
                            </td>
                            <td align="center"  style="width: 30%">
                                <asp:Label ID="lblappcomment" runat="server" CssClass="lbl" Font-Bold="True" 
                                    Font-Names="Consolas" Font-Size="13px" ForeColor="#666666">Approval Comment</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 5%">
                            </td>
                            <td valign="top" style="width: 10%;">
                                <asp:Label ID="Label8" runat="server" CssClass="lbl" Font-Bold="True" Font-Names="Consolas"
                                    Font-Size="13px" ForeColor="#666666">Body:</asp:Label>
                            </td>
                            <td style="width: 55%;">
                                <asp:TextBox ID="txtbody" runat="server" BorderColor="#CCCCCC" Font-Names="Consolas"
                                    Font-Size="15px" ForeColor="#666666" BorderStyle="Solid" BorderWidth="1px" ToolTip="Message Title"
                                    Width="99%" TextMode="MultiLine" Height="300px"></asp:TextBox>
                            </td>
                            <td style="width: 30%">
                                                      <asp:TextBox ID="txtapprovalcomment" runat="server" BorderColor="#CCCCCC" Font-Names="Consolas" ReadOnly="true"
                                    Font-Size="14px" ForeColor="#666666" BorderStyle="Solid" BorderWidth="1px" ToolTip="Approval Comment"
                                    Width="99%" TextMode="MultiLine" Height="300px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>--%>
                </asp:View>
            </asp:MultiView>
           <%-- <table width="100%">
                <tr>
                    <td style="width: 5%">
                    </td>
                    <td valign="top" style="width: 10%;">
                                
                            </td>
                    <td style="width: 55%;">
                        <asp:Label ID="lblcomment" runat="server" CssClass="lbl" Font-Bold="True" Font-Names="Consolas"
                            Font-Size="13px" ForeColor="#666666" Text="Comments"></asp:Label>
                    </td>
                    <td style="width: 30%">
                    </td>
                </tr>
                <tr>
                    <td style="width: 5%">
                    </td>
                    <td valign="top" style="width: 10%;">
                                
                            </td>
                    <td style="border: 1px solid #CCCCCC; width: 55%;">
                        <div id = "divcomment" runat ="server" style="height: 150px; width: 100%; overflow-y: scroll; overflow-x: hidden;">
                            <asp:DataList ID="dlcomments" runat="server" Width="100%" RepeatColumns="1" CellSpacing="2"
                                RepeatLayout="Table" Font-Names="Arial" Font-Size="12px" GridLines="Both" DataKeyField="id"
                                BorderColor="#CCCCCC" ForeColor="#666666">
                                <ItemTemplate>
                                    <table class="table" width="100%">
                                        <tr>
                                            <td valign="top" style="width: 100%">
                                                <b>
                                                    <%# Eval("name")%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" style="width: 100%; font-size: 10px">
                                                <%# Eval("office")%>
                                                <%# Eval("timestamps")%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%">
                                                <asp:Literal ID="ltcomments" runat="server" Text='<%# Eval("comment").ToString() %>' />
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:DataList>
                        </div>
                    </td>
                    <td style="width: 30%">
                    </td>
                </tr>
                <tr>
                    <td style="width: 5%">
                    </td>
                    <td valign="top" style="width: 10%;">
                                
                            </td>
                    
                    <td valign="top" style="border: 1px solid #CCCCCC; width: 55%;">
                        <telerik:RadTextBox ID="txtpost" runat="server" DisplayText="Add a comment" Font-Names="Consolas"
                            Font-Size="13px" TextMode="MultiLine" Width="90%" ForeColor="#666666" BorderColor="#CCCCCC"
                            BorderWidth="1px" Height="50px" BorderStyle="Solid">
                        </telerik:RadTextBox>
                        <asp:Button ID="btnPost" runat="server" BackColor="#3399FF" BorderStyle="None" Font-Names="Verdana"
                            Font-Size="11px" ForeColor="White" Height="40px" Text="Post" Width="80px" />
                    </td>
                    <td style="width: 30%">
                    </td>
                </tr>
            </table>--%>
            <%--<asp:Button ID="btnRefresh" runat="server" Text="Add" BackColor="White" Font-Names="Verdana"
                                    Font-Size="11px" ForeColor="White" Width="10px" Height="20px" BorderStyle="None" />--%>
        </div></div></div></div>
        </form>
    </body>
    </html>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
   
   
   
</asp:Content>
