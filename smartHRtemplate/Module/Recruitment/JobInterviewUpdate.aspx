<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="JobInterviewUpdate.aspx.vb"
    Inherits="GOSHRM.JobInterviewUpdate" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title></title>
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        <script type="text/javascript">
            function ShowProgress() {
                setTimeout(function () {
                    var modal = $('<div />');
                    modal.addClass("modal");
                    $('body').append(modal);
                    var loading = $(".loading");
                    loading.show();
                    var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                    var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                    loading.css({ top: top, left: left });
                }, 200);
            }
            $('form').live("submit", function () {
                ShowProgress();
            });
        </script>
        <style type="text/css">
            .modal
            {
                position: fixed;
                top: 0;
                left: 0;
                background-color: black;
                z-index: 99;
                opacity: 0.8;
                filter: alpha(opacity=80);
                -moz-opacity: 0.8;
                min-height: 100%;
                width: 100%;
            }
            .loading
            {
                font-family: Arial;
                font-size: 10pt;
                border: 5px solid #67CFF5;
                width: 200px;
                height: 100px;
                display: none;
                position: fixed;
                background-color: White;
                z-index: 999;
            }
        </style>
    </head>
    <%--<script type="text/javascript">
    function setHeight() {
        var tt = document.getElementById("<%=txtCandidates.ClientID%>");
        tt.style.height = tt.scrollHeight + "px";
    }
</script>--%>
    <body>
        <form id="form1" action="">
        <div class="container">
            <div class="row">
                <div class=" col-md-12">
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
                                <div class="col-md-12 col-md-offset-0">
                                    <h5 id="pagetitle" runat="server" class="page-title">
                                        Interviews</h5>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <label>
                                            COMPANY</label>
                                        <input id="acompany" runat="server" class="form-control" type="text" disabled="disabled" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <label>
                                            JOB POST*</label>
                                        <telerik:radcombobox id="cbojobpost" runat="server" forecolor="#666666" width="100%"
                                            autopostback="True" filter="Contains" rendermode="Lightweight" skin="Bootstrap">
                                        </telerik:radcombobox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <label>
                                            INTERVIEW DATE*</label>
                                        <telerik:raddatepicker id="dateInterview" runat="server" culture="en-US" mindate=""
                                            resolvedrendermode="Classic" rendermode="Lightweight" forecolor="#666666" skin="Bootstrap"
                                            width="100%">
                                            <calendar userowheadersasselectors="False" usecolumnheadersasselectors="False" enableweekends="True"
                                                fastnavigationnexttext="&amp;lt;&amp;lt;" enablekeyboardnavigation="True" rendermode="Lightweight"
                                                skin="Bootstrap">
                                        </calendar>
                                            <dateinput displaydateformat="dd/MM/yyyy" dateformat="dd/MM/yyyy" labelwidth="40%"
                                                rendermode="Lightweight">
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
                            </div>
                            <div class="row">
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <label>
                                            INTERVIEW TIME*</label>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-4">
                                    <div class="form-group">
                                        <telerik:radcombobox id="radHourStart0" runat="server" resolvedrendermode="Classic"
                                            width="100%" rendermode="Lightweight" forecolor="#666666" skin="Bootstrap">
                                            <items>
                                            <telerik:RadComboBoxItem runat="server" Text="1" Value="1" Owner="radHourStart" />
                                            <telerik:RadComboBoxItem runat="server" Text="2" Value="2" Owner="radHourStart" />
                                            <telerik:RadComboBoxItem runat="server" Text="3" Value="3" Owner="radHourStart" />
                                            <telerik:RadComboBoxItem runat="server" Text="4" Value="4" Owner="radHourStart" />
                                            <telerik:RadComboBoxItem runat="server" Text="5" Value="5" Owner="radHourStart" />
                                            <telerik:RadComboBoxItem runat="server" Text="6" Value="6" Owner="radHourStart" />
                                            <telerik:RadComboBoxItem runat="server" Text="7" Value="7" Owner="radHourStart" />
                                            <telerik:RadComboBoxItem runat="server" Text="8" Value="8" Owner="radHourStart" />
                                            <telerik:RadComboBoxItem runat="server" Text="9" Value="9" Owner="radHourStart" />
                                            <telerik:RadComboBoxItem runat="server" Text="10" Value="10" Owner="radHourStart" />
                                            <telerik:RadComboBoxItem runat="server" Text="11" Value="11" Owner="radHourStart" />
                                            <telerik:RadComboBoxItem runat="server" Text="12" Value="12" Owner="radHourStart" />
                                        </items>
                                        </telerik:radcombobox>
                                    </div>
                                </div>
                                <div class=" col-md-4">
                                    <div class="form-group">
                                        <telerik:radcombobox id="radMinStart0" runat="server" resolvedrendermode="Classic"
                                            width="100%" rendermode="Lightweight" forecolor="#666666" skin="Bootstrap">
                                            <items>
                                            <telerik:RadComboBoxItem runat="server" Text="00" Value="00" Owner="radMinStart" />
                                            <telerik:RadComboBoxItem runat="server" Text="15" Value="15" Owner="radMinStart" />
                                            <telerik:RadComboBoxItem runat="server" Text="30" Value="30" Owner="radMinStart" />
                                            <telerik:RadComboBoxItem runat="server" Text="45" Value="45" Owner="radMinStart" />
                                        </items>
                                        </telerik:radcombobox>
                                    </div>
                                </div>
                                <div class=" col-md-4">
                                    <div class="form-group">
                                        <telerik:radcombobox id="radTimeStart0" runat="server" resolvedrendermode="Classic"
                                            width="100%" rendermode="Lightweight" forecolor="#666666" skin="Bootstrap">
                                            <items>
                                            <telerik:RadComboBoxItem runat="server" Text="AM" Value="AM" Owner="radTimeStart" />
                                            <telerik:RadComboBoxItem runat="server" Text="PM" Value="PM" Owner="radTimeStart" />
                                        </items>
                                        </telerik:radcombobox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>
                                            INTERVIEWERS*</label>
                                        <telerik:radcombobox id="cboInterviewers" runat="server" checkboxes="True" filter="Contains"
                                            autopostback="True" rendermode="Lightweight" width="100%" sort="Ascending" font-names="Verdana"
                                            font-size="12px" forecolor="#666666" skin="Bootstrap">
                                        </telerik:radcombobox>
                                        <br />
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel31" UpdateMode="Always">
                                            <ContentTemplate>
                                                <telerik:radlistbox id="lstInterviewers" runat="server" resolvedrendermode="Classic"
                                                    borderstyle="None" enabled="False" width="100%" emptymessage="No Interviewer"
                                                    rendermode="Lightweight" sort="Ascending" font-size="12px" forecolor="#666666"
                                                    skin="Bootstrap">
                                                    <buttonsettings transferbuttons="All"></buttonsettings>
                                                    <emptymessagetemplate>
                                                    No Interviewer
                                                </emptymessagetemplate>
                                                </telerik:radlistbox>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cboInterviewers" EventName="ItemChecked" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>
                                            CANDIDATES*</label>
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Always">
                                            <ContentTemplate>
                                                <telerik:radcombobox id="cboCandidates" runat="server" checkboxes="True" filter="Contains"
                                                    autopostback="True" enablecheckallitemscheckbox="True" rendermode="Lightweight"
                                                    width="100%" sort="Ascending" forecolor="#666666" skin="Bootstrap">
                                                </telerik:radcombobox>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cbojobpost" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Always">
                                            <ContentTemplate>
                                                <telerik:radlistbox id="lstCandidates" runat="server" resolvedrendermode="Classic"
                                                    borderstyle="None" enabled="False" width="100%" emptymessage="No Candidates"
                                                    rendermode="Lightweight" sort="Ascending" font-size="12px" forecolor="#666666"
                                                    skin="Bootstrap">
                                                    <buttonsettings transferbuttons="All"></buttonsettings>
                                                    <emptymessagetemplate>
                                                    No Candidates
                                                </emptymessagetemplate>
                                                </telerik:radlistbox>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cboCandidates" EventName="ItemChecked" />
                                                <asp:AsyncPostBackTrigger ControlID="cboCandidates" EventName="CheckAllCheck" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>
                                            VENUE*</label>
                                        <textarea id="avenue" runat="server" class="form-control" rows="4" cols="1"></textarea>
                                    </div>
                                </div>
                            </div>
                            <div id="divinterviewer" runat="server" class="row">
                                <div class=" col-md-4">
                                    <div class="form-group">
                                        <label>
                                            INTERVIEWERS NOTIFIED</label>
                                        <input id="albli" runat="server" class="form-control" type="text" disabled="disabled" />
                                    </div>
                                </div>
                                <div class=" col-md-4">
                                    <div class="form-group">
                                        <label>
                                            DATE</label>
                                        <input id="ainterviewerdate" runat="server" class="form-control" type="text" disabled="disabled" />
                                    </div>
                                </div>
                            </div>
                            <div id="divcandidate" runat="server" class="row">
                                <div class=" col-md-4">
                                    <div class="form-group">
                                        <label>
                                            CANDIDATES NOTIFIED</label>
                                        <input id="alblc" runat="server" class="form-control" type="text" disabled="disabled" />
                                    </div>
                                </div>
                                <div class=" col-md-4">
                                    <div class="form-group">
                                        <label>
                                            DATE</label>
                                        <input id="acandidatedate" runat="server" class="form-control" type="text" disabled="disabled" />
                                    </div>
                                </div>
                            </div>
                            <div id="div1" runat="server" class="row">
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtCandidates" runat="server" TextMode="MultiLine" Visible="False"
                                            Width="100%" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12 m-t-20">
                                    <button id="btnupdate" runat="server" onserverclick="btnAdd_Click" type="submit"
                                        style="width: 150px" class="btn btn-success">
                                        Save &amp; Update</button>
                                    <button id="btclose" runat="server" onserverclick="btnCancel_Click" type="submit"
                                        style="width: 150px" class="btn btn-danger">
                                        << Back</button>
                                    <button id="btnotify1" runat="server" onserverclick="btnNotifyInterviewers_Click"
                                        type="submit" class="btn btn-basic">
                                        Send Invite to Interviewers</button>
                                    <button id="btnotify2" runat="server" onserverclick="btnNotifyCandiddate_Click" type="submit"
                                        class="btn btn-basic">
                                        Send Invite to Candidates</button>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12 text-right">
                                    <div class="form-group">
                                        <label>
                                            <i id="createdon" runat="server"></i>
                                        </label>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            <i id="updatedon" runat="server"></i>
                                        </label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        </form>
        <div class="loading" align="center">
            Processing, please wait...<br />
            <br />
            <img src="../../images/loaders.gif" alt="" />
        </div>
    </body>
    </html>
</asp:Content>
