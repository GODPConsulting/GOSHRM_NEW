﻿<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="false"
    CodeBehind="EmployeeEducation.aspx.vb" Inherits="GOSHRM.EmployeeEducation" EnableEventValidation="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title></title>
        <script type="text/javascript">
            function closeWin() {
                popup.close();   // Closes the new window
            }
        </script>
    </head>
    <body>
        <form id="form1" action="">
        <div class="container">
            <div class="row">
                <div class=" col-md-8">
                    <div id="divalert" runat="server" visible="false" class="alert alert-info">
                        <strong id="msgalert" runat="server"></strong>
                        <asp:TextBox ID="txtEmpID" runat="server" Font-Size="1px" Visible="false"></asp:TextBox>
                        <asp:Label ID="lblidnew" runat="server" Font-Size="1px" Visible="False"></asp:Label>
                        <asp:TextBox ID="txtid" runat="server" Font-Size="1px" Visible="False"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-3 col-md-12 col-xs-6 pull-left">
                    <p>
                        <a href="#" runat="server" onserverclick="Back"><u>Employee Profile</u></a>
                        <label>
                            >
                        </label>
                        <a id="A2" href="#">Education Qualification</a>
                    </p>
                </div>
            </div>
            <div class="card-box">
                <div class="row">
                    <div class="profile-basic">
                        <div class="row">
                            <div class=" col-md-8">
                                <h5 class="card-title" style="color: #1BA691">
                                    Education</h5>
                            </div>
                        </div>
                        <div class="row">
                            <div class=" col-md-8">
                                <div class="form-group">
                                    <label>
                                        NAME</label>
                                    <input id="aname" runat="server" class="form-control" type="text" disabled="disabled" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class=" col-md-8">
                                <div class="form-group">
                                    <label>
                                        CERTIFICATE</label>
                                    <telerik:radcombobox id="cboqualification" runat="server" width="100%" forecolor="#666666"
                                        rendermode="Lightweight" skin="Bootstrap" filter="Contains">
                                    </telerik:radcombobox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class=" col-md-8">
                                <div class="form-group">
                                    <label>
                                        INSTITUTE</label>
                                    <input id="aschool" runat="server" class="form-control" type="text" placeholder="Institute / School" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class=" col-md-4">
                                <div class="form-group">
                                    <label>
                                        START DATE</label>
                                    <telerik:radcombobox id="cbostartmonth" runat="server" autopostback="True" forecolor="#666666"
                                        rendermode="Lightweight" skin="Bootstrap" width="100%">
                                    </telerik:radcombobox>
                                </div>
                            </div>
                            <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Always">
                                <ContentTemplate>
                                    <div id="divstartyear" runat="server" class="col-md-4">
                                        <div class="form-group">
                                            <label>
                                                YEAR</label>
                                            <telerik:radcombobox id="cbostartyear" runat="server" forecolor="#666666" skin="Bootstrap"
                                                width="100%">
                                            </telerik:radcombobox>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="cbostartmonth" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <div class="row">
                            <div class=" col-md-4">
                                <div class="form-group">
                                    <label>
                                        END DATE</label>
                                    <telerik:radcombobox id="cboendmonth" runat="server" autopostback="True" width="100%"
                                        forecolor="#666666" rendermode="Lightweight" skin="Bootstrap">
                                    </telerik:radcombobox>
                                </div>
                            </div>
                            <asp:UpdatePanel runat="server" ID="UpdatePanel5" UpdateMode="Always">
                                <ContentTemplate>
                                    <div id="divexpyear" runat="server" class=" col-md-4">
                                        <div class="form-group">
                                            <label>
                                                YEAR</label>
                                            <telerik:radcombobox id="cboendyear" runat="server" style="margin-left: 0px" forecolor="#666666"
                                                rendermode="Lightweight" width="100%" skin="Bootstrap">
                                            </telerik:radcombobox>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="cboendmonth" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <div class="row">
                            <div class=" col-md-8">
                                <div class="form-group">
                                    <label>
                                        ATTACHMENT</label>
                                    <input id="afilename" runat="server" class="form-control" type="text" disabled="disabled" />
                                    <input class="form-control" type="file" id="file1" runat="server" />
                                    <button id="btndownload" runat="server" onserverclick="Download_Click" type="submit"
                                        class="btn btn-default">
                                        <i class="fa fa-download"></i>Download</button>
                                    <button id="btnremove" runat="server" onserverclick="remove_Click" type="submit"
                                        class="btn btn-default">
                                        <i class="fa fa-remove"></i>Remove Attachment</button>
                                </div>
                            </div>
                        </div>
                        <div id="divapproval" runat="server" class="row">
                            <div class=" col-md-8">
                                <div class="form-group">
                                    <label>
                                        ACCEPTANCE STATUS</label>
                                    <telerik:radcombobox id="radstatus" runat="server" forecolor="#666666" rendermode="Lightweight"
                                        skin="Bootstrap" width="100%">
                                    </telerik:radcombobox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-8 m-t-20 text-center">
                                <button id="btupdate" runat="server" style="width:150px;" onserverclick="btnAdd_Click" type="submit" class="btn btn-success">
                                    Save</button>
                                <button id="btnclose" runat="server" style="width:150px;" onserverclick="btnClose_Click" type="submit" class="btn btn-info">
                                    << Back</button>
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
