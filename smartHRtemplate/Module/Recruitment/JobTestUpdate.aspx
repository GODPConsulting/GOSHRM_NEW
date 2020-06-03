<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="JobTestUpdate.aspx.vb"
    Inherits="GOSHRM.JobTestUpdate" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title></title>
    </head>
    <body>
        <form id="form1" action="">
        <div class="container">
            <div class="row">
                <div class=" col-md-10">
                    <div id="divalert" runat="server" visible="false" class="alert alert-info">
                        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <strong
                            id="msgalert" runat="server"></strong>
                        <asp:TextBox ID="txtid" runat="server" Width="1px" Height="1px" Visible="False"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class=" col-md-10">
                    <div class="panel panel-success">
                        <div class="panel-body">

            <div class="row">
                <div class="col-md-8 col-md-offset-0">
                    <h5 id="pagetitle" runat="server" class="page-title">
                        Recruitment Test</h5>
                </div>
            </div>
            <div class="row">
                <div class=" col-md-12">
                    <div class="form-group">
                        <label>
                            COMPANY*</label>
                        <telerik:radcombobox id="cbocompany" runat="server" forecolor="#666666" width="100%"
                            autopostback="True" filter="Contains" rendermode="Lightweight" skin="Bootstrap">
                        </telerik:radcombobox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class=" col-md-12">
                    <div class="form-group">
                        <label>
                            JOB POST*</label>
                        <asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Always">
                            <ContentTemplate>
                                <telerik:radcombobox id="cbojobpost" runat="server" width="100%" forecolor="#666666"
                                    autopostback="True" filter="Contains" rendermode="Lightweight" skin="Bootstrap">
                                </telerik:radcombobox>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="cbocompany" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <label>
                            TEST TITLE</label>
                        <input id="atesttitle" runat="server" class="form-control" type="text" />
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <label>
                            TEST DESCRIPTION</label>
                        <textarea id="atestdesc" runat="server" class="form-control" rows="4" cols="1"></textarea>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <div class="form-group">
                        <label>
                            PASS MARK (%)</label>
                        <input id="apassmark" runat="server" class="form-control" type="text" />
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <label>
                            TEST DURATION (MINUTES)</label>
                        <input id="atestduration" runat="server" class="form-control" type="text" />
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <div class="form-group">
                        <label>
                            HAS ONLINE TEST</label>
                        <telerik:radcombobox id="cboonline" runat="server" width="100%" forecolor="#666666"
                            tooltip="Determines if Test will be available Online for Remote Access by Candidates"
                            autopostback="True" rendermode="Lightweight" skin="Bootstrap">
                        </telerik:radcombobox>
                    </div>
                </div>
                <div id="divstage" runat="server" class="col-md-6">
                    <div class="form-group">
                        <label>
                            TEST STAGE</label>
                        <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Always">
                            <ContentTemplate>
                                <telerik:radcombobox id="cbostageno" runat="server" width="100%" forecolor="#666666"
                                    resolvedrendermode="Classic" tooltip="Stage Level of Test for Job Post" rendermode="Lightweight"
                                    skin="Bootstrap">
                                </telerik:radcombobox>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="cbojobpost" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="cboonline" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-1">
                    <div class="form-group">
                        <label>
                            ACTIVE</label>
                    </div>
                    <div class="form-group">
                        <label class="switch">
                            <input id="aactive" runat="server" type="checkbox" />
                            <span class="slider round"></span>
                        </label>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 m-t-20">
                    <button id="btnupdate" runat="server" onserverclick="btnSave_Click" type="submit"
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
