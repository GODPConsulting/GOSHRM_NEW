﻿<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="LocationUpdate.aspx.vb"
    Inherits="GOSHRM.LocationUpdate" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title></title>
    </head>
    <body>
        <form id="form1" action="">
        <div class="container">
            <div>
                <div class="row">
                    <div id="divalert" runat="server" visible="false" class="alert alert-info">
                        <strong id="msgalert" runat="server"></strong>
                        <asp:TextBox ID="txtid" runat="server" Width="5px" Font-Size="5px" Font-Names="Candara"
                            Height="5px" Visible="False"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-10 col-md-offset-0">
                        <div class="row">
                            <div class=" col-md-10">
                                <div class="panel panel-success">
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class=" col-md-12">
                                                <h5 class="page-title" style="color: #1BA691">
                                                    Location</h5>
                                            </div>
                                        </div>
                                        <form action="">
                                        <div class="row">
                                            <div class=" col-md-12">
                                                <div class="form-group">
                                                    <label>
                                                        Name*</label>
                                                    <input id="locationname" runat="server" class="form-control" type="text" />
                                                </div>
                                            </div>
                                            <div class=" col-md-12">
                                                <div class="form-group">
                                                    <label>
                                                        Address*</label>
                                                    <textarea id="txtaddress" runat="server" class="form-control" rows="5"></textarea>
                                                </div>
                                            </div>
                                            <div class=" col-md-12">
                                                <div class="form-group">
                                                    <label>
                                                        City</label>
                                                    <input id="city" runat="server" class="form-control" type="text" />
                                                </div>
                                            </div>
                                            <div class=" col-md-12">
                                                <div class="form-group">
                                                    <label>
                                                        State/Province</label>
                                                    <input id="province" runat="server" class="form-control" type="text" />
                                                </div>
                                            </div>
                                            <div class=" col-md-12">
                                                <div class="form-group">
                                                    <label>
                                                        Country*</label>
                                                    <telerik:raddropdownlist id="radCountry" runat="server" forecolor="#666666" defaultmessage="-- Select --"
                                                        font-names="Verdana" width="100%" rendermode="Lightweight" skin="Bootstrap">
                                                    </telerik:raddropdownlist>
                                                </div>
                                            </div>
                                            <div class=" col-md-12">
                                                <div class="form-group">
                                                    <label>
                                                        Contact Number</label>
                                                    <input id="telephone" runat="server" class="form-control" type="text" />
                                                </div>
                                            </div>
                                            <div class=" col-md-12">
                                                <div class="form-group">
                                                    <label>
                                                        Additional Information</label>
                                                    <textarea id="note" runat="server" class="form-control" rows="5"></textarea>
                                                </div>
                                            </div>
                                            <div class="col-md-12 m-t-20 text-center">
                                                <button id="btnupdate" runat="server" onserverclick="btnAdd_Click" type="submit"
                                                    style="width: 150px" class="btn btn-primary btn-success">
                                                    Save &amp; Update</button>
                                                <button id="Button1" runat="server" onserverclick="btnCancel_Click" type="submit"
                                                    style="width: 150px" class="btn btn-primary btn-danger">
                                                    << Back</button>
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
        </form>
    </body>
    </html>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
</asp:Content>
