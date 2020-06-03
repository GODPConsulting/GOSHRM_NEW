<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="HostServer.aspx.vb"
    Inherits="GOSHRM.HostServer" EnableEventValidation="false" Debug="true" %>

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
                        <h5><b id="pagetitle" runat="server"> Host Server</b></h5>
                    </div>
                        <div class="panel-body">
                            <div>
                                <div>
                                    <div class="row">
                                        <div class="col-md-12 col-md-offset-0">
                                            <form action="">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <label>
                                                            Host Domain / Server</label>
                                                        <input id="hostname" runat="server" class="form-control" type="text" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class ="row">
                                             <div class="col-md-12">
                                             <div class="form-group">
                                              <label> Allow HTTPS</label>
                                             <asp:CheckBox ID="allowhttps" runat="server" />
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
            </div>
        </div>
        </form>
    </body>
    </html>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
</asp:Content>
