<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="CompanyInformation.aspx.vb"
    Inherits="GOSHRM.CompanyInformation" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <title></title>
 
    <body>
        <form id="form1" action="">
        <%--        <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
        <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
        <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css" rel="Stylesheet" type="text/css" />--%>
        <div class="container col-md-12">
            <div class="row">
                <div class="col-md-12">
                    <div id="divalert" runat="server" visible="false" class="alert alert-info">
                        <strong id="msgalert" runat="server">Danger!</strong>
                        <asp:TextBox ID="txtid" runat="server" Font-Size="1px" Height="16px" Width="6px"
                            Visible="False"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                  <div class="row">
                <div class=" col-md-12">
                    <div class="panel panel-success">
                        <div class="panel-body">
  
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Image ID="imgProfile" runat="server" CssClass=" img-rounded" ImageUrl="~/images/logo.png"
                                    Height="80px" Width="120px" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:Image ID="imgClear" runat="server" ImageUrl="~/images/logo.png" Height="150px"
                                        Width="150px" Visible="False" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-4">
                            <div class="form-group">
                                <label>
                                    Company Logo</label>
                                <input id="imguploads" runat="server" class="form-control" type="file" />
                                <%--<span class="help-block">Recommended image size is 40px x 40px</span>--%>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>
                                    Company Name <span class="text-danger">*</span></label>
                                <input id="companyname" runat="server" class="form-control" type="text" value="Focus Technologies">
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>
                                    Email Address</label>
                                <input id="companyemail" runat="server" class="form-control " value="" type="text" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>
                                    Tax ID</label>
                                <input id="companytaxid" runat="server" class="form-control " value="" type="text" />
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>
                                    Registration Number</label>
                                <input id="companyregnumber" runat="server" class="form-control " value="" type="text" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label>
                                    Phone Number</label>
                                <input id="companyphone" runat="server" class="form-control " value="" type="text" />
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label>
                                    Fax</label>
                                <input id="companyfax" runat="server" class="form-control " value="" type="text" />
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label>
                                    Employees</label>
                                <input id="companyemptotal" runat="server" class="form-control " value="" type="text"
                                    readonly="readonly" />
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label>
                                    Standard Currency</label>
                                <telerik:raddropdownlist id="radCurrency" runat="server" defaultmessage="-- Select --"
                                    forecolor="#666666" font-names="Verdana" width="100%" rendermode="Lightweight"
                                    skin="Bootstrap">
                                </telerik:raddropdownlist>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>
                                    Address 1</label>
                                <textarea id="companyaddr1" runat="server" rows="4" class="form-control"></textarea>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>
                                    Address 2</label>
                                <textarea id="companyaddr2" runat="server" rows="4" class="form-control"></textarea>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>
                                    City</label>
                                <input id="companycity" runat="server" class="form-control" value="" type="text" />
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>
                                    State/Province</label>
                                <input id="companystate" runat="server" class="form-control" value="" type="text" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>
                                    ZIP/Postal Code</label>
                                <input id="companyzipcode" runat="server" class="form-control" value="818-635-5579"
                                    type="text" />
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>
                                    Country</label>
                                <telerik:raddropdownlist id="radCountry" runat="server" defaultmessage="-- Select --"
                                    forecolor="#666666" font-names="Verdana" width="100%" rendermode="Lightweight"
                                    skin="Bootstrap">
                                </telerik:raddropdownlist>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>
                                    Note</label>
                                <textarea id="companynote" runat="server" rows="4" class="form-control"></textarea>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>
                                    Is Multi-Company Setup</label>
                                <telerik:raddropdownlist id="radMultiCompany" runat="server" defaultmessage="-- Select --"
                                    forecolor="#666666" font-names="Verdana" width="100%" autopostback="True" rendermode="Lightweight"
                                    skin="Bootstrap">
                                </telerik:raddropdownlist>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:UpdatePanel runat="server" ID="UpdatePanel5" UpdateMode="Always">
                                    <ContentTemplate>
                                        <label id="lblsubLevel" runat="server">
                                            No of Subsidiary Levels</label>
                                        <telerik:radcombobox runat="server" dropdownautowidth="Enabled" rendermode="Lightweight"
                                            resolvedrendermode="Classic" width="100%" id="cboLevel" autopostback="True" font-names="Verdana"
                                            forecolor="#666666" tooltip="applied only to multi-company, last level set for subsidiaries"
                                            skin="Bootstrap">
                                        </telerik:radcombobox>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="radMultiCompany" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 m-t-20 text-center">
                            <button id="btnupdate" runat="server" onserverclick="btnAdd_Click" type="submit"
                                style="width: 150px" class="btn btn-primary btn-success">
                                Save &amp; Update</button>
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
