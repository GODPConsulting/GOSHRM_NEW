<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="WorkWeekGradeUpdate.aspx.vb"
    Inherits="GOSHRM.WorkWeekGradeUpdate" EnableEventValidation="false" Debug="true" %>

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
            <div class="row">
                <div id="divalert" runat="server" visible="false" class="alert alert-info">
                    <strong id="msgalert" runat="server"></strong>
                </div>
            </div>
            <div class="row">
                <div class=" col-md-10">
                    <div class="panel panel-success">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-8 col-md-offset-0">
                                    <h4 class="page-title">
                                        Work Week</h4>
                                    <asp:TextBox ID="txtid" runat="server" Width="4px" Font-Names="Candara" Height="2px"
                                        Visible="False"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <label>
                                            Job Grade*</label>
                                        <telerik:radcombobox id="cboGrade" runat="server" filter="Contains" forecolor="#666666"
                                            font-names="Verdana" width="100%" skin="Bootstrap">
                                        </telerik:radcombobox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <label>
                                            Day*</label>
                                        <telerik:raddropdownlist id="radDay" font-names="Verdana" forecolor="#666666" runat="server"
                                            defaultmessage="--Select--" skin="Bootstrap" width="100%">
                                        </telerik:raddropdownlist>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <label>
                                            Status*</label>
                                        <telerik:raddropdownlist id="radStatus" font-names="Verdana" forecolor="#666666"
                                            runat="server" defaultmessage="--Select--" skin="Bootstrap" width="100%">
                                        </telerik:raddropdownlist>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <label>
                                            Country*</label>
                                        <telerik:raddropdownlist id="radCountry" font-names="Verdana" forecolor="#666666"
                                            runat="server" defaultmessage="--Select--" skin="Bootstrap" width="100%">
                                        </telerik:raddropdownlist>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12 m-t-20">
                                    <button id="btnupdate" runat="server" onserverclick="btnAdd_Click" type="submit"
                                        style="width: 150px" class="btn btn-primary btn-success rounded">
                                        Save &amp; Update</button>
                                    <button id="Button1" runat="server" onserverclick="btnCancel_Click" type="submit"
                                        style="width: 150px" class="btn btn-primary btn-info rounded">
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
