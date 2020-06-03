<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="MenuSetupUpdate.aspx.vb"
    Inherits="GOSHRM.MenuSetupUpdate" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title></title>
        <body>
            <form id="form1">
            <div class="container">
                <div class="row">
                    <div class=" col-md-10">
                        <div id="divalert" runat="server" visible="false" class="alert alert-info">
                            <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <strong
                        id="msgalert" runat="server">Danger!</strong>
                            </div>
                </div>
                <div class="row">
                    <div class=" col-md-10">
                        <div class="panel panel-success">
                        <div class="panel-heading">
                                             <h5><b id="pagetitle" runat="server">MENU SETUP UPDATE</b></h5>
                                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-md-8 col-md-offset-0">
                                        <asp:TextBox ID="txtid" runat="server" Width="4px" style="font-size: medium; font-family: Candara"
                                            Font-Names="Candara" Height="2px" Visible="False">
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class=" col-md-6">
                                        <div class="form-group">
                                            <label>
                                                OLD MENU NAME*</label>
                                                <input id="oldname" runat="server" class="form-control" readonly />
                                        </div>
                                    </div>
                                    <div class=" col-md-6">
                                        <div class="form-group">
                                            <label>
                                                NEW MENU NAME*</label>
                                            <input id="newname" runat="server" class="form-control" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12 m-t-20">
                                        <button id="btnupdate" runat="server" onserverclick="btnAdd_Click" type="submit"
                                            style="width: 150px" class="btn btn-primary btn-success">
                                            Save &amp; Update</button>
                                        <button id="Button1" runat="server" onserverclick="btnCancel_Click" type="submit"
                                            style="width: 150px" class="btn btn-primary btn-danger">
                                            << Back</button>
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
