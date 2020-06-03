<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="AddUser.aspx.vb"
    Inherits="GOSHRM.AddUser" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title></title>
        <script type="text/javascript" id="telerikClientEvents1">
//<![CDATA[

            function cboaccess_DropDownClosing(sender, args) {
                //Add JavaScript handler code here
                document.getElementById("btnaccess").click();
            }
//]]>
        </script>
    </head>
    <body>
        <form id="form1" action="">
        <div class="container">
            <div>
                <div class="row">
                    <div id="divalert" runat="server" visible="false" class="alert alert-info">
                        <strong id="msgalert" runat="server"></strong>
                        <asp:TextBox ID="txtid" runat="server" Font-Size="1px" Visible="False"></asp:TextBox>
                        <asp:Button ID="btnaccess" runat="server" BackColor="White" ForeColor="Black" Width="5px"
                            Height="5px" BorderStyle="None" Font-Names="Verdana" Font-Size="11px" />
                    </div>
                </div>
                <div class="row">
                    <div class=" col-md-10">
                        <div class="panel panel-success">
                         <div class="panel-heading">
                         <h4><b>User</b></h4>
                         </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        
                                        <form action="">                                        
                                            <div class="row">
                                                <div class=" col-md-12">
                                                    <div class="form-group">
                                                        <label>
                                                            Username*</label>
                                                        <input id="username" runat="server" class="form-control" type="text" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class=" col-md-6">
                                                    <div class="form-group">
                                                        <label>
                                                            Is Employee*</label>
                                                        <telerik:radcombobox id="cboIsEmp" runat="server" width="100%" font-names="Verdana"
                                                            autopostback="True" forecolor="#666666" rendermode="Lightweight" skin="Bootstrap">
                                                        </telerik:radcombobox>
                                                    </div>
                                                </div>
                                                <div class=" col-md-6">
                                                    <div class="form-group">
                                                        <label>
                                                            Name*</label>
                                                        <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Always">
                                                            <ContentTemplate>
                                                                <telerik:radcombobox id="cboEmployee" runat="server" filter="Contains" visible="False"
                                                                    width="100%" autopostback="True" font-names="Verdana" forecolor="#666666" rendermode="Lightweight"
                                                                    skin="Bootstrap">
                                                                </telerik:radcombobox>
                                                                <input id="fullname" runat="server" class="form-control" type="text" />
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="cboIsEmp" EventName="SelectedIndexChanged" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class=" col-md-6">
                                                    <div class="form-group">
                                                        <label>
                                                            Email</label>
                                                        <asp:UpdatePanel runat="server" ID="UpdatePanel5" UpdateMode="Always">
                                                            <ContentTemplate>
                                                                <input id="usermail" runat="server" class="form-control" type="text" />
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="cboEmployee" EventName="SelectedIndexChanged" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>
                                                            Status*</label>
                                                        <telerik:radcombobox id="cbostatus" runat="server" width="100%" font-names="Verdana"
                                                            forecolor="#666666" rendermode="Lightweight" skin="Bootstrap">
                                                        </telerik:radcombobox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>
                                                            Role*</label>
                                                        <telerik:radcombobox id="radroletypes" runat="server" filter="Contains" skin="Bootstrap"
                                                            width="100%" font-names="Verdana" forecolor="#666666">
                                                        </telerik:radcombobox>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>
                                                            Is System Administrator</label>
                                                        <select id="isadminsystem" runat="server" class="select form-control">
                                                            <option>No</option>
                                                            <option>Yes</option>
                                                        </select>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>
                                                            Is HR Administrator</label>
                                                        <select id="isadminhr" runat="server" class="select form-control">
                                                            <option>No</option>
                                                            <option>Yes</option>
                                                        </select>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>
                                                            Is Finance Administrator</label>
                                                        <select id="isadminfinance" runat="server" class="select form-control">
                                                            <option>No</option>
                                                            <option>Yes</option>
                                                        </select>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div id="divaccesslevel" runat="server" class="col-md-6">
                                                    <div class="form-group">
                                                        <label>
                                                            Access Level*</label>
                                                        <telerik:radcombobox id="cbolevel" runat="server" checkboxes="False" filter="Contains"
                                                            autopostback="True" enablecheckallitemscheckbox="False" font-names="Verdana"
                                                            forecolor="#666666" skin="Bootstrap" rendermode="Lightweight" width="100%">
                                                        </telerik:radcombobox>
                                                    </div>
                                                </div>
                                                <div id="divaccess" runat="server" class="col-md-6">
                                                    <div class="form-group">
                                                        <label>
                                                            Access*</label>
                                                        <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Always">
                                                            <ContentTemplate>
                                                                <telerik:radcombobox id="cboaccess" runat="server" checkboxes="True" filter="Contains"
                                                                    autopostback="False" enablecheckallitemscheckbox="True" font-names="Verdana"
                                                                    forecolor="#666666" skin="Bootstrap" rendermode="Lightweight" width="100%" onclientdropdownclosing="cboaccess_DropDownClosing">
                                                                </telerik:radcombobox>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="cbolevel" EventName="SelectedIndexChanged" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                        <telerik:radlistbox id="lstaccess" runat="server" enabled="False" font-names="Verdana"
                                                            forecolor="#666666" font-size="13px" width="100%" rendermode="Lightweight" skin="Bootstrap">
                                                        </telerik:radlistbox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="divpwd" runat="server" class="row">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>
                                                            Password*</label>
                                                        <input id="pwd" runat="server" class="form-control" type="password" />
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>
                                                            Confirm Password*</label>
                                                        <input id="pwdconfirm" runat="server" class="form-control" type="password" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-12 m-t-20 text-center">
                                                <button id="btnupdate" runat="server" onserverclick="btnAdd_Click" type="submit"
                                                    style="width: 150px" class="btn btn-primary btn-success">
                                                    Save &amp; Update</button>
                                                <button id="Button1" runat="server" onserverclick="btnCancel_Click" type="submit"
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
        </div>
        </form>
    </body>
    </html>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
</asp:Content>
