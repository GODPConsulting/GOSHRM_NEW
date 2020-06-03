<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="Notifications.aspx.vb"
    Inherits="GOSHRM.Notifications" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <title></title>
    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to delete notifications?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
    <script type="text/javascript">
        window.onload = function () {
            var div = document.getElementById("dvScroll");
            var div_position = document.getElementById("div_position");
            var position = parseInt('<%=Request.Form("div_position") %>');
            if (isNaN(position)) {
                position = 0;
            }
            div.scrollTop = position;
            div.onscroll = function () {
                div_position.value = div.scrollTop;
            };
        };
    </script>
    <body>
        <form id="form1">
        <div class="container col-md-12">
        <div class="row">
            <div class="col-md-12">
                <div id="divalert" runat="server" visible="false" class="alert alert-info">
                    <strong id="msgalert" runat="server">Danger!</strong>
                    <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                </div>
            </div>
        </div>
         <div class="panel panel-success">
                <div class="panel-heading">
                    <h5><b id="pagetitle" runat="server">Mail</b></h5>
                </div>
             <div class="panel-body">
        <div id="divemplink" runat="server" class="row">
                <div id="divjoboffer" runat="server" class="col-sm-3 col-md-12 col-xs-6 pull-left">
                    <p>
                        <a href="notifications"><u>Notification</u></a>
                        <label>
                            >
                        </label>
                        <a href="#">Mail</a>
                    </p>
                </div>
            </div>
         
    <div class="row">
         <div style="margin-right:20px;" class="search-box-wrapper col-sm-6 col-md-3 col-xs-12 form-group pull-right">
                 <asp:LinkButton ID="btDelete" data-toggle="tooltip" data-original-title="Delete" Height="35px" runat="server" CssClass="btn btn-default btn-sm" OnClick="btnDelete_Click" OnClientClick="Confirm()">
                            <span style="margin-top:5px" aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                        </asp:LinkButton>                        
                    <input id="search" style="width:100%; margin-left:10px;" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                    <button onserverclick="btnFind_Click" id="btsearch" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>                   
           </div>
              <div class="col-sm-3 col-md-3 col-xs-6">
            <asp:LinkButton ID="lnkWorkHistory" runat="server" Font-Names="Verdana" Font-Size="19px">Inbox(0)</asp:LinkButton>
            <asp:LinkButton ID="lnkoutbox" runat="server" Font-Names="Verdana" Font-Size="11px"
                                        Visible="False">Outbox(0)</asp:LinkButton>
                </div>

    </div>
 

        <div>
            <table width="100%">
                <tr>
                    <td valign="top" align="center" style="width: 0%">
                        <table width="100%">
                            <tr>
                                <td>
                                    
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblpath0" runat="server" Font-Names="Arial" Font-Size="1px" Visible="false"
                                        ForeColor="#666666" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblpath1" runat="server" Font-Names="Arial" Font-Size="1px" Visible="false"
                                        ForeColor="#666666" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblpath2" runat="server" Font-Names="Arial" Font-Size="1px" Visible="false"
                                        ForeColor="#666666" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblpath3" runat="server" Font-Names="Arial" Font-Size="1px" Visible="false"
                                        ForeColor="#666666" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 100%">
                        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                            <asp:View ID="listmail" runat="server">
                                <div id="dvScroll" style="overflow: scroll; height: 900px">
                                    <telerik:radgrid rendermode="Lightweight" id="gridMailList" runat="server" pagesize="100"
                                        allowsorting="True" allowmultirowselection="True" allowpaging="True" showgrouppanel="True"
                                        autogeneratecolumns="False" borderwidth="1px" bordercolor="#CCCCCC" grouppanelposition="Top"
                                        resolvedrendermode="Classic" datakeynames="ID" gridlines="Both" enablegroupsexpandall="True"
                                        showfooter="True" showstatusbar="True" width="100%" font-names="Verdana" font-size="12px"
                                        skin="Bootstrap">
                                        <pagerstyle mode="NextPrevNumericAndAdvanced" showpagertext="False"></pagerstyle>
                                        <mastertableview width="100%" enablegroupsexpandall="True">
                                    <GroupByExpressions>
                                        <telerik:GridGroupByExpression>
                                            <SelectFields>
                                                <telerik:GridGroupByField HeaderText="Date" FieldAlias="" FieldName="daterecevied"></telerik:GridGroupByField>
                                            </SelectFields>
                                            <GroupByFields>
                                                <telerik:GridGroupByField FieldName="daterecevied3" SortOrder="Descending"></telerik:GridGroupByField>
                                            </GroupByFields>
                                        </telerik:GridGroupByExpression>
                                    </GroupByExpressions>
                                    <Columns>
                                        <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" ItemStyle-Width="30px" ItemStyle-VerticalAlign="Top"
                                            HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBox1" runat="server" ToolTip= "Check to delete" 
                                                    AutoPostBack="False" />
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="headerChkbox" runat="server" OnCheckedChanged="ToggleSelectedState"
                                                    AutoPostBack="True" />
                                            </HeaderTemplate>
                                            <HeaderStyle Width="30px"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn SortExpression="ID" HeaderText="ID" HeaderButtonType="TextButton" ItemStyle-VerticalAlign="Top"
                                            HeaderStyle-Width="1px" DataField="ID" ItemStyle-Width="1px">
                                            <HeaderStyle Width="1px"></HeaderStyle>
                                            <ItemStyle Width="1px"></ItemStyle>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn UniqueName="imagess" ItemStyle-Width="30px" ItemStyle-VerticalAlign="Top"
                                            HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                 <asp:imagebutton id="imgBetsmen1" runat="server" Height="20px" Width="25px" />                                                 
                                            </ItemTemplate>
                          
                                            <HeaderStyle Width="30px" />
                          
                                            <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn SortExpression="ID" HeaderText="ID" HeaderButtonType="TextButton" ItemStyle-VerticalAlign="Top"
                                            HeaderStyle-Width="1px" DataField="ID" ItemStyle-Width="1px">
                                            <HeaderStyle Width="1px"></HeaderStyle>
                                            <ItemStyle Width="1px"></ItemStyle>
                                        </telerik:GridBoundColumn>
                                         <telerik:GridTemplateColumn UniqueName="refnumber" HeaderText="Reference" DataField="refnumber" ItemStyle-VerticalAlign="Top"
                                            ItemStyle-Width="100px" HeaderStyle-Width="100px" ItemStyle-Font-Size="13px" ItemStyle-Font-Bold="true"  >
                                            <ItemTemplate>                                                    
                                                   <asp:LinkButton ID="lnkDownload" Text = '<%# Eval("refnumber")%>' CommandArgument = '<%# Eval("id") %>' runat="server" OnClick = "LinkDownLoad" ForeColor="Blue" ></asp:LinkButton>
                                            </ItemTemplate>
                                             <HeaderStyle Width="100px" Font-Size="13px"  />
                                            <ItemStyle Width="100px"></ItemStyle>
                                        </telerik:GridTemplateColumn>
                                          <telerik:GridBoundColumn SortExpression="mailtype" HeaderText="Mail Type" ItemStyle-VerticalAlign="Top" UniqueName="mailtype"
                                            HeaderButtonType="TextButton" HeaderStyle-Width="100px" DataField="mailtype"
                                            ItemStyle-Width="100px" ItemStyle-Font-Size="13px">
                                            <HeaderStyle Width="90px" Font-Size="13px" ></HeaderStyle>
                                            <ItemStyle Width="90px"></ItemStyle>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn SortExpression="subject" HeaderText="Subject" ItemStyle-VerticalAlign="Top" UniqueName="subject"
                                            HeaderButtonType="TextButton" HeaderStyle-Width="300px" DataField="subject"
                                            ItemStyle-Width="400px" ItemStyle-Font-Size="13px">
                                            <HeaderStyle Width="400px" Font-Size="13px" ></HeaderStyle>
                                            <ItemStyle Width="400px"></ItemStyle>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn SortExpression="sendername" HeaderText="From" ItemStyle-VerticalAlign="Top" UniqueName="sendername"
                                            HeaderButtonType="TextButton" HeaderStyle-Width="120px" DataField="sendername"
                                            ItemStyle-Width="120px" ItemStyle-Font-Size="13px">
                                            <HeaderStyle Width="100px" Font-Size="13px" ></HeaderStyle>
                                            <ItemStyle Width="100px"></ItemStyle>
                                        </telerik:GridBoundColumn>                                        
                                        <telerik:GridBoundColumn SortExpression="timereceived" HeaderText="Target Date" ItemStyle-VerticalAlign="Top"
                                            HeaderButtonType="TextButton" HeaderStyle-Width="100px" DataField="timereceived"
                                            ItemStyle-Width="100px" ItemStyle-Font-Size="13px">
                                            <HeaderStyle Width="90px" Font-Size="13px" ></HeaderStyle>
                                            <ItemStyle Width="90px"></ItemStyle>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn  HeaderStyle-Width="0.1px" DataField="markread" UniqueName="markread"
                                            ItemStyle-Width="0.1px">
                                            <HeaderStyle Width="0.1px"></HeaderStyle>
                                            <ItemStyle Width="0.1px"></ItemStyle>
                                        </telerik:GridBoundColumn>

                                    </Columns>
                                </mastertableview>
                                        <clientsettings reordercolumnsonclient="True" allowdragtogroup="True" allowcolumnsreorder="True">
                                    <Selecting AllowRowSelect="True"></Selecting>
                                    <Resizing AllowRowResize="True" AllowColumnResize="True" EnableRealTimeResize="True"
                                        ResizeGridOnColumnResize="False"></Resizing>
                                </clientsettings>
                                        <groupingsettings showungroupbutton="true"></groupingsettings>
                                        <filtermenu rendermode="Lightweight">
                                </filtermenu>
                                        <headercontextmenu rendermode="Lightweight">
                                </headercontextmenu>
                                    </telerik:radgrid>
                                </div>
                            </asp:View>
                            <asp:View ID="maildetail" runat="server">
                                <div class="content container-fluid">
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <h4 class="page-title">
                                                View Message</h4>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <div class="card-box">
                                                <div class="mailview-content">
                                                    <div class="mailview-header">
                                                        <h4 id="mailsubject" runat="server" class="text-ellipsis">
                                                            HRMS Bootstrap Admin Template</h4>
                                                        <div class="pull-right">
                                                            <div class="btn-group">
                                                                <button type="button" class="btn btn-default btn-sm" data-toggle="tooltip" title="Delete">
                                                                    <i class="fa fa-trash-o"></i>
                                                                </button>
                                                            </div>
                                                            <button type="button" class="btn btn-default btn-sm" data-toggle="tooltip" title="Print">
                                                                <i class="fa fa-print"></i>
                                                            </button>
                                                        </div>
                                                        <h3>
                                                        </h3>
                                                        <div class="sender-info">
                                                            <div class="sender-img">
                                                                <img alt="" class="img-circle" src="images/user.jpg" width="40"> </img>
                                                            </div>
                                                            <div class="receiver-details pull-left">
                                                                <span id="mailsender" runat="server" class="sender-name">John Doe</span> <span class="receiver-name">
                                                                    <span id="mailreceiver" runat="server">me, James, Paul</span> </span>
                                                            </div>
                                                            <div class="pull-right">
                                                                <span id="mailtime" runat="server" class="mail-time"></span>
                                                            </div>
                                                            <div class="clearfix">
                                                            </div>
                                                            <div pull-left>
                                                                <asp:LinkButton ID="lnknavigate" runat="server" Font-Names="Arial" Font-Size="12px">.</asp:LinkButton>
                                                            </div>
                                                        </div>
                                                        <h3>
                                                        </h3>
                                                    </div>
                                                    <div class="mailview-inner">
                                                        <p style="white-space: pre;" id="mailcontent" runat="server">
                                                        </p>
                                                    </div>
                                                </div>
                                                <div class="mail-attachments">
                                                    <p>
                                                        <i id="mailcountattach" runat="server" class="fa fa-paperclip"></i><a href="#">View
                                                            all</a> | <a href="#">Download all</a></p>
                                                    <ul class="attachments clearfix">
                                                        <li id="mailliattach1" runat="server" onserverclick="attachment0_Click">
                                                            <div class="attach-file">
                                                                <i class="fa fa-file-pdf-o"></i>
                                                            </div>
                                                            <div class="attach-info">
                                                                <%--<a id="mailattach1" runat="server" href="#" class="attach-filename" ></a>--%>
                                                                <button id="lnkattach" type="button" runat="server" class="btn btn-link" onserverclick="lnkattachment_Click">LinkButton</button>
                                                                <div class="attach-fileize">
                                                                </div>
                                                            </div>
                                                        </li>
                                                        <li id="mailliattach2" runat="server" onserverclick="attachment1_Click">
                                                            <div class="attach-file">
                                                                <i class="fa fa-file-word-o"></i>
                                                            </div>
                                                            <div class="attach-info">
                                                                <%--<a id="mailattach2" runat="server" href="#" class="attach-filename" ></a>--%>
                                                                <button id="Button1" type="button" runat="server" class="btn btn-link" onserverclick="lnkattachment_Click">LinkButton</button>
                                                                <div class="attach-fileize">
                                                                </div>
                                                            </div>
                                                        </li>
                                                        <li id="mailliattach3" runat="server" onserverclick="attachment2_Click">
                                                            <div class="attach-file">
                                                                <i class="fa fa-file-picture-o"></i>
                                                            </div>
                                                            <div class="attach-info">
                                                                <%--<a id="mailattach3" runat="server" href="#" class="attach-filename"></a>--%>
                                                                <button id="Button2" type="button" runat="server" class="btn btn-link" onserverclick="lnkattachment_Click">LinkButton</button>
                                                                <div class="attach-fileize">
                                                                </div> 
                                                            </div>
                                                        </li>
                                                        <li id="mailliattach4" runat="server" onserverclick="attachment3_Click">
                                                            <div class="attach-file">
                                                                <i class="fa fa-file-picture-o"></i>
                                                            </div>
                                                            <div class="attach-info">
                                                                <%--<a id="mailattach4" runat="server" href="#" class="attach-filename" ></a>--%>
                                                                <button id="Button3" type="button" runat="server" class="btn btn-link" onserverclick="lnkattachment_Click">LinkButton</button>
                                                                <div class="attach-fileize">
                                                                </div>
                                                            </div>
                                                        </li>
                                                    </ul>
                                                </div>
                                                <div class="mailview-footer">
                                                    <div class="row">
                                                        <div class="col-sm-6 right-action">
                                                            <button type="button" class="btn btn-default">
                                                                <i class="fa fa-print" runat="server"></i>Print</button>
                                                            <button type="button" class="btn btn-default">
                                                                <i class="fa fa-trash-o" runat="server"></i>Delete</button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:View>
                        </asp:MultiView>
                    </td>
                </tr>
            </table>
        </div>
        </div>
        </div>
        </div>
        </form>
    </body>
    </html>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
</asp:Content>
