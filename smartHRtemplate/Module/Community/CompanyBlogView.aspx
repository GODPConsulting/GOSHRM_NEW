<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="CompanyBlogView.aspx.vb"
    Inherits="GOSHRM.CompanyBlogView" EnableEventValidation="false" Debug="true" %>

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

    <body>
        <form>
        <div class="container col-md-12">
        <div class="row">
           <div id="divalert" runat="server" visible="false" class="alert alert-info">
                <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                <strong id="msgalert" runat="server">Danger!</strong>
                <asp:Label ID="lblid" runat="server" CssClass="lbl" Font-Bold="False" 
                Font-Names="Consolas" Font-Size="1px" ForeColor="#666666" Visible="False"></asp:Label>
            </div>
        </div>
        <div class="panel panel-success">
                <div class="panel-heading">
                    <h5><b id="pagetitle" runat="server">Blog View</b></h5>
                </div>
             <div class="panel-body">    
        
        <div class="row">
            <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                <asp:View ID="BlogEdit" runat="server">
 
                    <div class="col-md-12 m-t-20 text-center">
                        <button id="btnupdate" runat="server" onserverclick="btnAdd_Click" type="submit"
                            style="width: 150px" class="btn btn-primary btn-success">
                            Save &amp; Update</button>
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" OnClientClick="Confirm()"
                            ForeColor="White" Width="150px" Height="35px" cssclass="btn btn-danger"
                            BorderStyle="None" Font-Names="Verdana" Font-Size="12px" />
                            <button id="linkBack" type="button" runat="server" class="btn btn-info" onserverclick="lnkBack_Click"
                            style="height: 35px; width: 150px">
                            << Back</button>
                    </div>

                    <div class="row">
                        
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>
                                    Approval Status</label>
                                <telerik:RadDropDownList ID="radStatus" runat="server" DefaultMessage="-- Select --"
                                    Font-Names="Verdana" ForeColor="#666666" Skin="Bootstrap" RenderMode="Lightweight"
                                    Width="100%" ToolTip="approval to have blog published for public view">
                                </telerik:RadDropDownList>
                            </div>
                        </div>
                        <div id="divapproval" runat="server" class="col-md-10">
                            <div class="form-group">
                                <label>
                                    Approval Comment</label>
                                <textarea id="Textarea1" runat="server" class="form-control" rows="5"></textarea>
                            </div>
                        </div>
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>
                                    Heading</label>
                                <input id="heading" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>Blog Type</label>
                                 <telerik:RadComboBox runat="server" DropDownAutoWidth="Enabled" RenderMode="Lightweight"
                                    ResolvedRenderMode="Classic" ID="cboBlogType" Filter="Contains" Width="100%" 
                                    Font-Names="Verdana" Skin="Bootstrap" ForeColor="#666666">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>Date Posted</label>
                                 <input id="dateposted" runat="server" class="form-control" type="text" disabled="disabled" />
                            </div>
                        </div>
                         <div class=" col-md-12">
                            <div class="form-group">
                                 <label>Retire Date</label>
                                <telerik:RadDatePicker ID="datRetireDate" runat="server" Font-Names="Verdana" ForeColor="#666666"
                                    Font-Size="12px" MinDate="" ToolTip="last pay date deduction will be effective"
                                    MaxDate="2100-12-31" Skin="Bootstrap" Width="100%">
                                    <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True"
                                        FastNavigationNextText="&amp;lt;&amp;lt;" Skin="Bootstrap">
                                    </Calendar>
                                    <DateInput DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" LabelWidth="100%">
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
                         <div class=" col-md-12">
                            <div class="form-group">
                                <label>Unit/Department</label>
                                 <telerik:RadComboBox ID="radoffice" runat="server" Filter="Contains" Font-Names="Verdana"
                                    ForeColor="#666666" ResolvedRenderMode="Classic" Width="100%"
                                    CheckBoxes="True" ToolTip="select units/offices that is to see blog" Skin="Bootstrap" 
                                    AutoPostBack="False" EnableCheckAllItemsCheckBox="True">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>
                                    Attachment</label>
                                <div class="row">
                                    <div class="col-md-5">
                                        <div class="form-group">
                                            <span class="control-fileupload">
                                                <input type="file" id="file1" runat="server" style="width: 200px" />
                                            </span>
                                        </div>
                                    </div>
                                    <div class="col-md-5">
                                        <div class="form-group">
                                            <label id="fileattached" runat="server"></label>
                                            <asp:LinkButton ID="lnkclear" runat="server" Font-Names="Verdana" Font-Size="12px" ToolTip="delete attached file"
                                                Visible="False" OnClientClick="Confirm()">Clear</asp:LinkButton>
                                            <asp:LinkButton ID="lnkDownloadAttach" runat="server" Font-Names="Verdana" Font-Size="12px"
                                                Visible="False" ToolTip="download attached file">Download</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                                            
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>Body</label>
                                <textarea id="blogbody" runat="server" class="form-control" rows="10"></textarea>
                            </div>
                        </div>
                     
                    </div>

                </asp:View>
            </asp:MultiView>
            <%--<table width="100%">
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
                    <td style="width: 55%;">
                        <div id="divcomments" runat ="server" style="height: 150px; width: 100%; overflow-y: scroll; overflow-x: hidden;">
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
        </div></div></div></div>
        </form>
    </body>
    </html>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    <script type="text/javascript" id="telerikClientEvents1">
//<![CDATA[

        function radoffice_DropDownClosing(sender, args) {
            //Add JavaScript handler code here
            document.getElementById("btnRefresh").click();
        }
//]]>
    </script>
</asp:Content>
