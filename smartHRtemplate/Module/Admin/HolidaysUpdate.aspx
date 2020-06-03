<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="HolidaysUpdate.aspx.vb"
    Inherits="GOSHRM.HolidaysUpdate" EnableEventValidation="false" Debug="true" %>

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
                    <strong id="msgalert" runat="server"></strong>
                    <asp:TextBox ID="txtid" runat="server" Width="5px" Font-Names="Candara" Height="5px"
                        Visible="False"></asp:TextBox>
                </div>
            </div>

             <div class="row">
                <div class=" col-md-10">
                    <div class="panel panel-success">
                        <div class="panel-body">

            <div class="row">
                <div class="col-md-8 col-md-offset-0">
                    <h5 class="page-title">
                        Holiday</h5>
                </div>
            </div>
            <div class="row">
                <div class=" col-md-12">
                    <div class="form-group">
                        <label>
                            Name*</label>
                        <input id="aname" runat="server" class="form-control" type="text" />
                    </div>
                </div>
            </div>
            <div class="row">
                <div class=" col-md-12">
                    <label>
                        Date*</label>
                    <telerik:raddatepicker id="radDate" runat="server" forecolor="#666666" font-names="Verdana"
                        skin="Bootstrap" width="100%">
                        <calendar userowheadersasselectors="False" usecolumnheadersasselectors="False" enableweekends="True"
                            fastnavigationnexttext="&amp;lt;&amp;lt;" skin="Bootstrap">
                                    </calendar>
                        <dateinput displaydateformat="dd/MM/yyyy" dateformat="dd/MM/yyyy" labelwidth="60%">
                                        <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                        <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                                        <FocusedStyle Resize="None"></FocusedStyle>
                                        <DisabledStyle Resize="None"></DisabledStyle>
                                        <InvalidStyle Resize="None"></InvalidStyle>
                                        <HoveredStyle Resize="None"></HoveredStyle>
                                        <EnabledStyle Resize="None"></EnabledStyle>
                                    </dateinput>
                        <datepopupbutton imageurl="" hoverimageurl=""></datepopupbutton>
                    </telerik:raddatepicker>
                </div>
            </div>
            <div class="row">
                <div class=" col-md-12">
                    <div class="form-group">
                        <label>
                            Status*</label>
                        <telerik:raddropdownlist id="radStatus" runat="server" defaultmessage="-- Select --"
                            forecolor="#666666" font-names="Verdana" width="100%" rendermode="Lightweight"
                            skin="Bootstrap">
                        </telerik:raddropdownlist>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class=" col-md-12">
                    <div class="form-group">
                        <label>
                            Country*</label>
                        <telerik:raddropdownlist id="radCountry" runat="server" defaultmessage="-- Select --"
                            forecolor="#666666" font-names="Verdana" width="100%" style="margin-left: 0px"
                            rendermode="Lightweight" skin="Bootstrap">
                        </telerik:raddropdownlist>
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
    <style type="text/css">
        .style21
        {
            width: 100%;
        }
        .style22
        {
        }
        .style23
        {
            width: 275px;
        }
    </style>
</asp:Content>
