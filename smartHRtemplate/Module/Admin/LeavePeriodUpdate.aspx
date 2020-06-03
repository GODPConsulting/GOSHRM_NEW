<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="LeavePeriodUpdate.aspx.vb"
    Inherits="GOSHRM.LeavePeriodUpdate" EnableEventValidation="false" Debug="true" %>

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
                <div class=" col-md-10">
                    <div id="divalert" runat="server" visible="false" class="alert alert-info">
                        <strong id="msgalert" runat="server"></strong>
                        <asp:TextBox ID="txtid" runat="server" Width="3px" Font-Names="Candara" Height="2px"
                            Visible="False"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class=" col-md-10">
                    <div class="panel panel-success">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-8 col-md-offset-0">
                                    <h4 class="page-title">
                                        Leave Period</h4>
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
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>
                                            Start Month<span class="text-danger">*</span></label>
                                        <telerik:radcombobox runat="server" dropdownautowidth="Enabled" rendermode="Lightweight"
                                            skin="Bootstrap" resolvedrendermode="Classic" id="radMonthStart" width="100%">
                                            <items>
                                            <telerik:RadComboBoxItem runat="server" Text="January" Value="1" Owner="radMonthStart">
                                            </telerik:RadComboBoxItem>
                                            <telerik:RadComboBoxItem runat="server" Text="February" Value="2" Owner="radMonthStart">
                                            </telerik:RadComboBoxItem>
                                            <telerik:RadComboBoxItem runat="server" Text="March" Value="3" Owner="radMonthStart">
                                            </telerik:RadComboBoxItem>
                                            <telerik:RadComboBoxItem runat="server" Text="April" Value="4" Owner="radMonthStart">
                                            </telerik:RadComboBoxItem>
                                            <telerik:RadComboBoxItem runat="server" Text="May" Value="5" Owner="radMonthStart">
                                            </telerik:RadComboBoxItem>
                                            <telerik:RadComboBoxItem runat="server" Text="June" Value="6" Owner="radMonthStart">
                                            </telerik:RadComboBoxItem>
                                            <telerik:RadComboBoxItem runat="server" Text="July" Value="7" Owner="radMonthStart">
                                            </telerik:RadComboBoxItem>
                                            <telerik:RadComboBoxItem runat="server" Text="August" Value="8" Owner="radMonthStart">
                                            </telerik:RadComboBoxItem>
                                            <telerik:RadComboBoxItem runat="server" Text="September" Value="9" Owner="radMonthStart">
                                            </telerik:RadComboBoxItem>
                                            <telerik:RadComboBoxItem runat="server" Text="October" Value="10" Owner="radMonthStart">
                                            </telerik:RadComboBoxItem>
                                            <telerik:RadComboBoxItem runat="server" Text="November" Value="11" Owner="radMonthStart">
                                            </telerik:RadComboBoxItem>
                                            <telerik:RadComboBoxItem runat="server" Text="December" Value="12" Owner="radMonthStart">
                                            </telerik:RadComboBoxItem>
                                        </items>
                                        </telerik:radcombobox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>
                                            Start Year<span class="text-danger">*</span></label>
                                        <telerik:radcombobox runat="server" dropdownautowidth="Enabled" rendermode="Lightweight"
                                            skin="Bootstrap" resolvedrendermode="Classic" id="radYearStart" width="100%">
                                        </telerik:radcombobox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>
                                            End Month <span class="text-danger">*</span></label>
                                        <telerik:radcombobox runat="server" dropdownautowidth="Enabled" rendermode="Lightweight"
                                            skin="Bootstrap" resolvedrendermode="Classic" id="radMonthEnd" width="100%">
                                            <items>
                                            <telerik:RadComboBoxItem runat="server" Text="January" Value="1" Owner="radMonthStart">
                                            </telerik:RadComboBoxItem>
                                            <telerik:RadComboBoxItem runat="server" Text="February" Value="2" Owner="radMonthStart">
                                            </telerik:RadComboBoxItem>
                                            <telerik:RadComboBoxItem runat="server" Text="March" Value="3" Owner="radMonthStart">
                                            </telerik:RadComboBoxItem>
                                            <telerik:RadComboBoxItem runat="server" Text="April" Value="4" Owner="radMonthStart">
                                            </telerik:RadComboBoxItem>
                                            <telerik:RadComboBoxItem runat="server" Text="May" Value="5" Owner="radMonthStart">
                                            </telerik:RadComboBoxItem>
                                            <telerik:RadComboBoxItem runat="server" Text="June" Value="6" Owner="radMonthStart">
                                            </telerik:RadComboBoxItem>
                                            <telerik:RadComboBoxItem runat="server" Text="July" Value="7" Owner="radMonthStart">
                                            </telerik:RadComboBoxItem>
                                            <telerik:RadComboBoxItem runat="server" Text="August" Value="8" Owner="radMonthStart">
                                            </telerik:RadComboBoxItem>
                                            <telerik:RadComboBoxItem runat="server" Text="September" Value="9" Owner="radMonthStart">
                                            </telerik:RadComboBoxItem>
                                            <telerik:RadComboBoxItem runat="server" Text="October" Value="10" Owner="radMonthStart">
                                            </telerik:RadComboBoxItem>
                                            <telerik:RadComboBoxItem runat="server" Text="November" Value="11" Owner="radMonthStart">
                                            </telerik:RadComboBoxItem>
                                            <telerik:RadComboBoxItem runat="server" Text="December" Value="12" Owner="radMonthStart">
                                            </telerik:RadComboBoxItem>
                                        </items>
                                        </telerik:radcombobox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>
                                            End Year<span class="text-danger">*</span></label>
                                        <telerik:radcombobox runat="server" dropdownautowidth="Enabled" rendermode="Lightweight"
                                            skin="Bootstrap" resolvedrendermode="Classic" id="radYearEnd" width="100%">
                                        </telerik:radcombobox>
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
</asp:Content>
