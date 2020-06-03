<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="BlackBoard.aspx.vb"
    Inherits="GOSHRM.BlackBoard" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <title></title>
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
height :30px;
padding: 5px;
background-color: #fff;
background-repeat: repeat;
display: block;
overflow:auto;
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

    <body>
        <form id="form1">
         <div class="container col-md-12">
           <div class="row">
             <div id="divalert" runat="server" visible="false" class="alert alert-info">
                <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                <strong id="msgalert" runat="server">Danger!</strong>
            </div>
         </div>
       <div class="panel panel-success">
                <div class="panel-heading">
                    <h5><b id="pagetitle" runat="server">Blogs</b></h5>
                </div>
             <div class="panel-body">
           <div class="row">
              <div class="search-box-wrapper col-sm-6 col-md-4 col-xs-12 form-group pull-right">
                        <button id="btAdd" type="button" data-toggle="tooltip" data-original-title="Create Post" runat="server" class="glyphicon glyphicon-plus btn btn-default btn-sm" onserverclick="btnAdd_Click"
                            style="height: 35px;margin-right:10px;margin-left:10px;"></button>                         
                        <input id="txtsearch" style="width:100%" runat="server" type="text" placeholder="Search Post..." class="search-box-input"/>
                        <button onserverclick="btnGo_Click" id="btsearch" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                    </div>
                    <div class="col-md-3 col-sm-6 col-xs-12 pull-right">
                        <telerik:RadComboBox runat="server" Skin="Bootstrap" RenderMode="Lightweight"
                                ResolvedRenderMode="Classic" Width="100%" ID="cboBlogType" Filter="Contains"
                                Font-Names="Verdana" Font-Size="14px" ForeColor="#666666">
                            </telerik:RadComboBox>
                    </div>     
            </div>       
        <%--<div style="width: 100%">
            <table width="100%">
                <tr>
                    <td>
                        <asp:Button ID="btnAdd" runat="server" Text="Create Post" BackColor="#1BA691" Font-Names="Verdana"
                            Font-Size="11px" ForeColor="White" Width="80px" Height="20px" 
                            BorderStyle="None" />
                    </td>
                    <td>                     
                    </td>
                    <td class="style22">                     
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td align="right">                        
                    </td>
                    <td align="right">
                        <telerik:RadComboBox runat="server" DropDownAutoWidth="Enabled" RenderMode="Lightweight"
                            ResolvedRenderMode="Classic" Width="300px" ID="cboBlogType" Filter="Contains"
                            Font-Names="Verdana" Font-Size="11px" ForeColor="#666666">
                        </telerik:RadComboBox>
                        <asp:Button ID="Button1" runat="server" Text="Create Post" 
                            BackColor="White" Font-Names="Verdana"
                            Font-Size="11px" ForeColor="White" Width="10px" Height="20px" 
                            BorderStyle="None" />
                        <asp:TextBox ID="txtsearch" runat="server" Width="251px" Height="20px" BorderColor="#CCCCCC"
                            BorderWidth="1px" TextMode="Search" Font-Names="Verdana" Font-Size="11px" 
                            ForeColor="#666666"></asp:TextBox>
                            <asp:Button ID="Button2" runat="server" Text="Create Post" 
                            BackColor="White" Font-Names="Verdana"
                            Font-Size="11px" ForeColor="White" Width="10px" Height="20px" 
                            BorderStyle="None" />
                        <asp:Button ID="btnGo" runat="server" Text="Go" BackColor="#0099CC" Font-Names="Verdana"
                            Font-Size="11px" ForeColor="White" Width="50px" Height="20px" 
                            BorderStyle="None" />
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </div>--%>
         
        <%--<div class="row" style="border: 1px solid #C0C0C0; height: 550px; width: 100%; overflow-y: scroll; overflow-x: hidden;">
            <asp:DataList ID="dlBlogs" runat="server" Width="100%" RepeatColumns="1" CellSpacing="2"
                RepeatLayout="Table" Font-Names="Arial" Font-Size="13px" GridLines="Both" DataKeyField="id"
                BorderColor="#CCCCCC" CssClass="table table-condensed" ForeColor="#666666" BorderStyle="Solid" 
                BorderWidth="1px" >
                <ItemTemplate>
                    <table class="table" width="100%" >
                        <tr>
                            <td colspan="2">
                                <b> <%# Eval("heading")%></b>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 90%">                              
                                <asp:TextBox ID="Label2" Text='<%# Eval("message").ToString() %>' runat="server" ForeColor="#666666" 
                                    ReadOnly="true" Width="95%" BorderColor="#CCCCCC" BorderStyle="None" 
                BorderWidth="1px" CssClass="notes"   /><br />
                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/Employee/BlackBoardView.aspx?id={0}",
                     HttpUtility.UrlEncode(Eval("id").ToString())) %>' Text='Read More...' ToolTip="click to view full blog detail"  />                                 
                            </td>
                            <td valign="top" style="width: 10%; font-size:12px">
                               Posted By  <%# Eval("postedby")%>
                                <br />
                                <%# Eval("createdon")%><br />
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:DataList>
        </div>--%>  
        <div class="row">
                                <div class="activity">
									<div class="activity-box">
										<ul id="blogger" runat="server" class="activity-list">
											<li>
												<div class="activity-content">
													<div class="timeline-content">
														<a href="#" class="name">No Blog Post</a>
													</div>
												</div>
											</li>										
										</ul>
									</div>
								</div>
        </div>
                                
        </div></div></div>
        </form>
    </body>
    </html>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .style21
        {
            width: 100%;
        }
        .style22
        {
        }
    </style>
</asp:Content>
