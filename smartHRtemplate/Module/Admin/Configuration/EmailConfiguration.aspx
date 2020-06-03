<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="EmailConfiguration.aspx.vb"
    Inherits="GOSHRM.EmailConfiguration" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title></title>
        <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
            rel="Stylesheet" type="text/css" />
    </head>
    <body>
        <form id="form1" action="">
        <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
        <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
        <div class="container">
            <div class="row">
                <div class="col-md-10">
                    <div id="divalert" runat="server" visible="false" class="alert alert-info">
                        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <strong
                            id="msgalert" runat="server">Danger!</strong>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class=" col-md-10">
                    <div class="panel panel-success">
                     <div class="panel-heading">
                        <h5><b id="pagetitle" runat="server">Email Configuration</b></h5>
                    </div>
                        <div class="panel-body">
                            <div>
                                <div>
                                    <div class="row">
                                        <div class="col-md-12 col-md-offset-0">
                                            <form action="">
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>
                                                            Email From Address</label>
                                                        <input id="smtpemail" runat="server" class="form-control" type="text" />
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>
                                                            Emails From Name</label>
                                                        <input id="sendername" runat="server" class="form-control" type="text" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>
                                                            SMTP HOST</label>
                                                        <input id="smtphost" runat="server" class="form-control" type="text" />
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>
                                                            SMTP USER</label>
                                                        <input id="smtpuser" runat="server" class="form-control" type="text" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>
                                                            SMTP PASSWORD</label>
                                                        <input id="smtppwd" runat="server" class="form-control" type="password" />
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>
                                                            SMTP PORT</label>
                                                        <input id="smtpport" runat="server" class="form-control" type="text" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>
                                                            SMTP Security</label>
                                                        <select id="smtpauthentication" runat="server" class="select form-control">
                                                            <option>None</option>
                                                            <option>SSL</option>
                                                            <option>TLS</option>
                                                        </select>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>
                                                            <span class="tooltip">Tooltip text</span> Send Email Notifications</label>
                                                        <select id="sendemail" runat="server" class="select form-control">
                                                            <option>No</option>
                                                            <option>Yes</option>
                                                        </select>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class=" col-sm-1 col-md-12 m-t-20">
                                                    <button id="btnupdate" runat="server" onserverclick="btnAdd_Click" type="submit"
                                                        class="btn btn-primary btn-info">
                                                        Save &amp; Update</button>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>
                                                            Email Address</label>
                                                        <input id="txttestemail" name="testemail" runat="server" type="text" class="form-control"
                                                            placeholder="email to send test message to" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-1 col-md-6 m-t-20">
                                                    <button id="Button1" type="button" runat="server" class="btn btn-primary btn-success"
                                                        onserverclick="btnTest_Click">
                                                        Send Test Message</button>
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
