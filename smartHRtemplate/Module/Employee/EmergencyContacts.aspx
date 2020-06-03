<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="false" CodeBehind="EmergencyContacts.aspx.vb" Inherits="GOSHRM.EmergencyContacts" EnableEventValidation="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title></title>

    </head>

    <body>
        <form id="form1">
            <div class="container">
                <div class="row">
                    <div id="divalert" runat="server" visible="false" class="alert alert-info">
                        <strong id="msgalert" runat="server">Danger!</strong>
                        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                            <span aria-hidden="true">&times;</span></button>
                        <asp:TextBox ID="txtEmpID" runat="server" Width="1px" Visible="False"></asp:TextBox>
                        <asp:TextBox ID="txtid" runat="server" Width="1px" Visible="False"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div class="panel panel-success">
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-sm-8 col-md-12">
                                        <h5 id="pagetitle" runat="server" class="page-title" style="color: #1BA691">Emergency Contact 1</h5>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class=" col-md-12">
                                        <div class="form-group">
                                            <label>
                                                Name</label>
                                            <input id="aemername1" runat="server" class="form-control" type="text" placeholder="Name" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class=" col-md-12">
                                        <div class="form-group">
                                            <label>
                                                    CONTACT NUMBER</label>
                                                <input id="aemercontactnumber1" runat="server" class="form-control" type="text" placeholder="Phone number" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">                                            
                                            <label>
                                                    RELATIONSHIP*</label>
                                                <select id="drprelation1" runat="server" class="select form-control">
                                                </select>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class=" col-md-12">
                                        <div class="form-group">
                                           <label>
                                                    HOME ADDRESS*</label>
                                                <textarea id="aemeraddress1" runat="server" class="form-control" rows="4" cols="1" placeholder="Home Address"></textarea>
                                        </div>
                                    </div>
                                </div>                                
                            </div>
                        </div>
                    </div>

                    <div class="col-md-6">
                        <div class="panel panel-success">
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-sm-8 col-md-12">
                                        <h5 id="H1" runat="server" class="page-title" style="color: #1BA691">Emergency Contact 2</h5>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class=" col-md-12">
                                        <div class="form-group">
                                            <label>
                                                Name</label>
                                            <input id="aemername2" runat="server" class="form-control" type="text" placeholder="Name" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class=" col-md-12">
                                        <div class="form-group">
                                            <label>
                                                    CONTACT NUMBER</label>
                                                <input id="aemercontactnumber2" runat="server" class="form-control" type="text" placeholder="Phone number" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">                                            
                                            <label>
                                                    RELATIONSHIP*</label>
                                                <select id="drprelation2" runat="server" class="select form-control">
                                                </select>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class=" col-md-12">
                                        <div class="form-group">
                                           <label>
                                                    HOME ADDRESS*</label>
                                                <textarea id="aemeraddress2" runat="server" class="form-control" rows="4" cols="1" placeholder="Home Address"></textarea>
                                        </div>
                                    </div>
                                </div>                                
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12 m-t-20">
                                    <button id="btnsaveobj" runat="server" onserverclick="btnAdd_Click" type="submit"
                                        style="width: 150px" class="btn btn-primary btn-success">
                                        Save & Update</button>
                                    <button id="btncloseobj" runat="server" onserverclick="btnCancel_Click" type="submit"
                                        style="width: 150px" class="btn btn-primary btn-danger">
                                        << Back</button>
                                </div>
                    </div>

            </div>
        </form>
    </body>
    </html>
</asp:Content>
