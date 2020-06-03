<%--<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CompanyStructureUpdate.aspx.vb"
    Inherits="GOSHRM.CompanyStructureUpdate" EnableEventValidation="false" %>--%>

<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="CompanyStructureUpdate.aspx.vb"
    Inherits="GOSHRM.CompanyStructureUpdate" EnableEventValidation="false" Debug="true" %>

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
                    <div class="col-md-8">
                        <div id="divalert" runat="server" visible="false" class="alert alert-info">
                            <strong id="msgalert" runat="server"></strong>
                            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                <span aria-hidden="true">&times;</span></button>
                            <asp:TextBox ID="txtid" runat="server" Font-Size="1px" Visible="False"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-8">
               
                                <div class="panel panel-success">
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class=" col-md-12">
                                            <h5 class="page-title" style="color: #1BA691">
                                                Company Structure</h5>
                                            </div>
                                        </div>
                                        <form action="">
                                        <div class="row">
                                            <div class=" col-md-12">
                                                <div class="form-group">
                                                    <label>
                                                        Structure*</label>
                                                    <telerik:raddropdownlist id="radStructure" runat="server" onselectedindexchanged="radStructure_SelectedIndexChanged"
                                                        forecolor="#666666" defaultmessage="-- Select --" font-names="Verdana" font-size="12px"
                                                        width="100%" autopostback="True" rendermode="Lightweight" skin="Bootstrap">
                                                    </telerik:raddropdownlist>
                                                </div>
                                            </div>
                                            <div class=" col-md-12">
                                                <div class="form-group">
                                                    <label>
                                                        Office*</label>
                                                    <input id="txtoffice" runat="server" class="form-control" type="text" />
                                                </div>
                                            </div>
                                            <div class=" col-md-12">
                                                <div class="form-group">
                                                    <label>
                                                        Head</label>
                                                    <telerik:radcombobox id="radHead" runat="server" font-names="Verdana" font-size="12px"
                                                        resolvedrendermode="Classic" forecolor="#666666" filter="Contains" width="100%"
                                                        rendermode="Lightweight" skin="Bootstrap">
                                                    </telerik:radcombobox>
                                                </div>
                                            </div>
                                            <div class=" col-md-12">
                                                <div class="form-group">
                                                    <label>
                                                        Parent Office</label>
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Always">
                                                        <ContentTemplate>
                                                            <telerik:radcombobox id="radParent" runat="server" font-names="Verdana" font-size="12px"
                                                                resolvedrendermode="Classic" forecolor="#666666" filter="Contains" width="100%"
                                                                rendermode="Lightweight" skin="Bootstrap">
                                                            </telerik:radcombobox>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="radStructure" EventName="SelectedIndexChanged" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <div class=" col-md-12">
                                                <div class="form-group">
                                                    <label>
                                                        Address</label>
                                                    <textarea id="txtaddress" runat="server" class="form-control" rows="5" cols="1"></textarea>
                                                </div>
                                            </div>
                                            <div class=" col-md-12">
                                                <div class="form-group">
                                                    <label>
                                                        Country*</label>
                                                    <telerik:raddropdownlist id="radCountry" runat="server" forecolor="#666666" defaultmessage="-- Select --"
                                                        font-names="Verdana" font-size="12px" width="100%" rendermode="Lightweight" skin="Bootstrap">
                                                    </telerik:raddropdownlist>
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
        </form>
    </body>
    </html>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
</asp:Content>
