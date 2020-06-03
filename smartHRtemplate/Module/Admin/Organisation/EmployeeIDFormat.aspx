<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="EmployeeIDFormat.aspx.vb"
    Inherits="GOSHRM.EmployeeIDFormat" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <title></title>
    <body>
        <form id="form1" action="">
        <div class="container">
            <div class="row">
            <div class="col-md-10">
                <div id="divalert" runat="server" visible="false" class="alert alert-info">
                    <strong id="msgalert" runat="server">Danger!</strong>
                    <asp:TextBox ID="txtid" runat="server" Style="font-size: 5px; font-family: Candara"
                        Font-Names="Candara" Visible="False"></asp:TextBox>
                </div>
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
                                                Employee Identity Format</h5>
                                        </div>
                                    </div>
                                    <form action="">
                                    <div class="row">
                                        <div id="divcompany" runat="server" class=" col-md-12">
                                            <div class="form-group">
                                                <label>
                                                    Company</label>
                                                <telerik:radcombobox runat="server" rendermode="Lightweight"
                                                    resolvedrendermode="Classic" width="100%" id="cboCompany" filter="Contains" font-names="Verdana"
                                                     forecolor="#666666" skin="Bootstrap">
                                                </telerik:radcombobox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class=" col-md-12">
                                            <div class="form-group">
                                                <label>
                                                    Employee ID Prefix</label>
                                                <asp:TextBox ID="txtPrefix" runat="server" Width="100%" Font-Names="Verdana" BorderColor="#CCCCCC"
                                                    ForeColor="#666666" BorderStyle="Solid" BorderWidth="1px" AutoPostBack="true"
                                                    CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class=" col-md-12">
                                            <div class="form-group">
                                                <label>
                                                    Employee ID (Number of Digits)</label>
                                                <asp:TextBox ID="txtDigits" runat="server" Width="100%" Font-Names="Verdana" BorderColor="#CCCCCC"
                                                    ForeColor="#666666" BorderStyle="Solid" BorderWidth="1px" Font-Size="12px" TextMode="Number"
                                                    AutoPostBack="true" CssClass="form-control">1</asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class=" col-md-12">
                                            <div class="form-group">
                                                <label>
                                                    Format</label>
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Always">
                                                    <ContentTemplate>
                                                        <input id="formatid" runat="server" class="form-control" type="text" readonly="readonly" />
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="txtPrefix" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtDigits" EventName="TextChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class=" col-md-12">
                                            <div class="form-group">
                                                <label>
                                                    Updated By</label>
                                                <input id="updatedby" runat="server" class="form-control" type="text" readonly="readonly" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class=" col-md-12">
                                            <div class="form-group">
                                                <label>
                                                    Updated On</label>
                                                <input id="updatedon" runat="server" class="form-control" type="text" readonly="readonly" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12 m-t-20 text-center">
                                        <button id="btnupdate" runat="server" type="submit" onserverclick="btnAdd_Click"
                                            class="btn btn-primary btn-success">
                                            Save &amp; Update</button>
                                        <button id="btncancel" runat="server" onserverclick="btnBack_Click" type="submit"
                                            style="width: 150px" class="btn btn-primary btn-info">
                                            << Back</button>
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
