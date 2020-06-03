<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="Authenticationmode.aspx.vb"
    Inherits="GOSHRM.Authenticationmode" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title></title>
    </head>
    <body>
        <form action="" id="form1">
            <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
            <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
            <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
                rel="Stylesheet" type="text/css" />
            <div class="container">
                <div class="row">
                    <div class="col-md-10">
                        <div id="divalert" runat="server" visible="false" class="alert alert-info">
                            <strong id="msgalert" runat="server">Danger!</strong>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class=" col-md-10">
                        <div class="panel panel-success">
                            <div class="panel-heading">
                                <h5><b id="pagetitle" runat="server">Authentication Mode</b></h5>
                            </div>
                            <div class="panel-body">

                                <div class="row">
                                    <div class="col-md-12 col-md-offset-0">
                                        <form action="">
                                            <div class="row">
                                                <div class=" col-md-3">
                                                    <div class="form-group">
                                                        <label>Active Directory</label>
                                                        <telerik:RadComboBox ID="cboAD" ForeColor="#666666"
                                                            runat="server"
                                                            RenderMode="Lightweight"
                                                            ResolvedRenderMode="Classic" Width="100%" Skin="Bootstrap" AutoPostBack="True" ToolTip="Use Active Directory for user authentication">
                                                            <Items>
                                                                <telerik:RadComboBoxItem runat="server" Text="Yes" Value="True"
                                                                    Owner="radMonth" />
                                                                <telerik:RadComboBoxItem runat="server" Text="No" Value="False"
                                                                    Owner="radMonth" />
                                                            </Items>
                                                        </telerik:RadComboBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Always">
                                                            <ContentTemplate>
                                                                <label>
                                                                    LDAP Url</label>
                                                                <input id="ldap" runat="server" class="form-control" type="text" placeholder="LDAP://example.com/dc=example,dc=com" />
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="cboAD" EventName="SelectedIndexChanged" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class=" col-md-3">
                                                    <div class="form-group">
                                                        <label>Dual Authentication</label>
                                                        <telerik:RadComboBox ID="cboDualAuthen" ForeColor="#666666"
                                                            runat="server"
                                                            RenderMode="Lightweight"
                                                            ResolvedRenderMode="Classic" Width="100%" Skin="Bootstrap" AutoPostBack="True" ToolTip="Use dual authentication">
                                                            <Items>
                                                                <telerik:RadComboBoxItem runat="server" Text="Yes" Value="True"
                                                                    Owner="radMonth" />
                                                                <telerik:RadComboBoxItem runat="server" Text="No" Value="False"
                                                                    Owner="radMonth" />
                                                            </Items>
                                                        </telerik:RadComboBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class=" col-md-3">
                                                    <div class="form-group">
                                                        <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Always">
                                                            <ContentTemplate>
                                                                <label>Authentication</label>
                                                                <telerik:RadComboBox ID="cboAuthenMode" ForeColor="#666666"
                                                                    runat="server"
                                                                    RenderMode="Lightweight"
                                                                    ResolvedRenderMode="Classic" Width="100%" Skin="Bootstrap" ToolTip="Dual authentication by">
                                                                    <Items>
                                                                        <telerik:RadComboBoxItem runat="server" Text="EMAIL" Value="email"
                                                                            Owner="radMonth" />
                                                                        <telerik:RadComboBoxItem runat="server" Text="SMS" Value="sms"
                                                                            Owner="radMonth" />
                                                                    </Items>
                                                                </telerik:RadComboBox>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="cboDualAuthen" EventName="SelectedIndexChanged" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class=" col-sm-1 m-t-20 text-center">
                                                    <button id="btnupdate" runat="server" onserverclick="btnAdd_Click" type="submit"
                                                        class="btn btn-primary btn-success">
                                                        Save &amp; Update</button>
                                                </div>
                                            </div>
                                        </form>
                                    </div>
                                </div>
                            </div>
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
