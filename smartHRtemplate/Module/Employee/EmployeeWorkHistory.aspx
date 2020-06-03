<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="false"
    CodeBehind="EmployeeWorkHistory.aspx.vb" Inherits="GOSHRM.EmployeeWorkHistory"
    EnableEventValidation="true" %>

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
                <div class="col-md-12">
                    <div id="divalert" runat="server" visible="false" class="alert alert-info">
                        <strong id="msgalert" runat="server">Danger!</strong>
                        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                            <span aria-hidden="true">&times;</span></button>
                        <asp:TextBox ID="txtid" runat="server" Width="1px" Font-Size="1px" Visible="False"></asp:TextBox>
                        <asp:TextBox ID="txtempid" runat="server" Width="1px" Font-Size="1px" Visible="False"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class=" col-md-10">
                    <div class="panel panel-success">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-sm-8 col-md-12">
                                    <h5 id="pagetitle" runat="server" class="page-title" style="color: #1BA691">
                                        Employee Career Setup</h5>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <label>
                                            EMPLOYEE</label>
                                        <input id="aname" runat="server" class="form-control" type="text" disabled="disabled" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            JOB GRADE *</label>
                                        <telerik:radcombobox id="cboJobGrade" runat="server" rendermode="Lightweight" font-names="Verdana"
                                            forecolor="#666666" width="100%" skin="Bootstrap" filter="Contains" autopostback="True">
                                        </telerik:radcombobox>
                                    </div>
                                </div>
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            JOB TITLE *</label>
                                        <telerik:radcombobox id="cboJobTitle" runat="server" rendermode="Lightweight" font-names="Verdana"
                                            forecolor="#666666" width="100%" filter="Contains" skin="Bootstrap">
                                        </telerik:radcombobox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            JOB TYPE *</label>
                                        <telerik:radcombobox id="cbojobtype" runat="server" rendermode="Lightweight" font-names="Verdana"
                                            forecolor="#666666" width="100%" filter="Contains" skin="Bootstrap">
                                        </telerik:radcombobox>
                                    </div>
                                </div>
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            COUNTRY *</label>
                                        <telerik:radcombobox id="cboCountry" runat="server" rendermode="Lightweight" font-names="Verdana"
                                            forecolor="#666666" width="100%" autopostback="True" skin="Bootstrap" filter="Contains">
                                        </telerik:radcombobox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <label>
                                            LOCATION *</label>
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel" UpdateMode="Always">
                                            <ContentTemplate>
                                                <telerik:radcombobox id="cboLocation" runat="server" filter="Contains" forecolor="#666666"
                                                    rendermode="Lightweight" skin="Bootstrap" width="100%">
                                                </telerik:radcombobox>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cboCountry" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            DEPARTMENT / OFFICE *</label>
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Always">
                                            <ContentTemplate>
                                                <telerik:radcombobox id="cboOffice" runat="server" filter="Contains" forecolor="#666666"
                                                    rendermode="Lightweight" skin="Bootstrap" width="100%">
                                                </telerik:radcombobox>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cboCountry" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            LINE MANAGER *</label>
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Always">
                                            <ContentTemplate>
                                                <telerik:radcombobox id="cboSupervisor" runat="server" rendermode="Lightweight" forecolor="#666666"
                                                    width="100%" skin="Bootstrap" filter="Contains">
                                                </telerik:radcombobox>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cboJobGrade" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            REVIEWER I *</label>
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Always">
                                            <ContentTemplate>
                                                <telerik:radcombobox id="cboreviewerI" runat="server" rendermode="Lightweight" forecolor="#666666"
                                                    width="100%" skin="Bootstrap" filter="Contains">
                                                </telerik:radcombobox>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cboJobGrade" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            REVIEWER II *</label>
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel4" UpdateMode="Always">
                                            <ContentTemplate>
                                                <telerik:radcombobox id="cboReviewerII" runat="server" rendermode="Lightweight" forecolor="#666666"
                                                    width="100%" skin="Bootstrap" filter="Contains">
                                                </telerik:radcombobox>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cboJobGrade" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            START MONTH *</label>
                                        <telerik:radcombobox id="cbostartmonth" runat="server" rendermode="Lightweight" forecolor="#666666"
                                            width="100%" skin="Bootstrap">
                                        </telerik:radcombobox>
                                    </div>
                                </div>
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            START YEAR *</label>
                                        <telerik:radcombobox id="cbostartyear" runat="server" rendermode="Lightweight" forecolor="#666666"
                                            width="100%" skin="Bootstrap">
                                        </telerik:radcombobox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            END MONTH *</label>
                                        <telerik:radcombobox id="cboendmonth" runat="server" rendermode="Lightweight" forecolor="#666666"
                                            width="100%" skin="Bootstrap" autopostback="True">
                                        </telerik:radcombobox>
                                    </div>
                                </div>
                                <asp:UpdatePanel runat="server" ID="UpdatePanel5" UpdateMode="Always">
                                    <ContentTemplate>
                                        <div id="divendyear" runat="server" class=" col-md-6">
                                            <div class="form-group">
                                                <label>
                                                    END YEAR *</label>
                                                <telerik:radcombobox id="cboendyear" runat="server" rendermode="Lightweight" forecolor="#666666"
                                                    width="100%" skin="Bootstrap">
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
                                <div class="col-md-12 m-t-20">
                                    <button id="btnsave" runat="server" onserverclick="btnAdd_Click" type="submit" style="width: 150px"
                                        class="btn btn-success rounded">
                                        Save & Update</button>
                                    <button id="btnback" runat="server" onserverclick="btnCancel_Click" type="submit"
                                        style="width: 150px" class="btn btn-info rounded">
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
