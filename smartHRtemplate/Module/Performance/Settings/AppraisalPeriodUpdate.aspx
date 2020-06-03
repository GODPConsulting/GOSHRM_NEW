<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="AppraisalPeriodUpdate.aspx.vb"
    Inherits="GOSHRM.AppraisalPeriodUpdate" EnableEventValidation="false" Debug="true" %>

<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<%--<meta name="viewport" content="width=device-width, initial-scale=1">
<link rel="stylesheet" href="http://www.w3schools.com/lib/w3.css">--%>
<head>
    <title></title>
    <script type="text/javascript">

        function closeWin() {
            popup.close();   // Closes the new window
        }
   

    </script>
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
       <script type="text/javascript">
           function Confirm() {
               var confirm_value = document.createElement("INPUT");
               confirm_value.type = "hidden";
               confirm_value.name = "confirm_value";
               if (confirm("Copy last appraisal objectives of employee to this current appraisal cycle?")) {
                   confirm_value.value = "Yes";
               } else {
                   confirm_value.value = "No";
               }
               document.forms[0].appendChild(confirm_value);
           }
    </script>
</head>
<body>
    <form>    
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
        rel="Stylesheet" type="text/css" />
    <div class="container col-md-12">
        <div class="row">
        <div id="divalert" runat="server" visible="false" class="alert alert-info">
                    <strong id="msgalert" runat="server"></strong>
                    <asp:TextBox ID="txtid" runat="server" Width="3px"  
                    Font-Names="Candara" Height="2px" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtQuestScore" CssClass="from-control" runat="server" Style="text-align: right" Width="100%"
                     BorderColor="#CCCCCC" BorderWidth="1px" Height="1px" Visible="False">0</asp:TextBox>
                </div>
                </div>
                  <div class="panel panel-success">
                        <div class="panel-heading">
                            <h5><b id="pagetitle" runat="server">Appraisal Cycle</b></h5>
                        </div>
                     <div class="panel-body">
                    <div class="row">
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>Calendar Range:*</label>
                                <input id="lblCalendar" runat="server" style="height:28px;" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>Company*</label>
                                <telerik:RadComboBox ID="cboCompany" Skin="Bootstrap" runat="server" Width="100%"  ForeColor="#666666"
                                    Filter="Contains" RenderMode="Lightweight" Font-Names="Verdana" 
                                    Font-Size="12px">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>Review Year*</label>
                                <telerik:RadComboBox ID="cboYear" Skin="Bootstrap" runat="server" Width="100%"  ForeColor="#666666"
                                    Filter="Contains" RenderMode="Lightweight" Font-Names="Verdana" 
                                    Font-Size="12px" AutoPostBack="True">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>Start Period*</label>
                                <telerik:RadDatePicker ID="dateStart" Skin="Bootstrap" runat="server" MinDate="1910-01-01" Width="100%"  ForeColor="#666666"
                                RenderMode="Lightweight">
                                <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True"
                                    FastNavigationNextText="&amp;lt;&amp;lt;" RenderMode="Lightweight">
                                </Calendar>
                                <DateInput DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" LabelWidth="40%"
                                    RenderMode="Lightweight">
                                    <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                    <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                                    <FocusedStyle Resize="None"></FocusedStyle>
                                    <DisabledStyle Resize="None"></DisabledStyle>
                                    <InvalidStyle Resize="None"></InvalidStyle>
                                    <HoveredStyle Resize="None"></HoveredStyle>
                                    <EnabledStyle Resize="None"></EnabledStyle>
                                </DateInput>
                                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                            </telerik:RadDatePicker>
                            </div>
                        </div>
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label> End Period*</label>
                                <telerik:RadDatePicker ID="dateEnd" Skin="Bootstrap" runat="server" MinDate="1910-01-01" Width="100%"  ForeColor="#666666"
                                    RenderMode="Lightweight" Font-Names="Verdana">
                                    <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True"
                                        FastNavigationNextText="&amp;lt;&amp;lt;" RenderMode="Lightweight">
                                    </Calendar>
                                    <DateInput DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" LabelWidth="40%"
                                        RenderMode="Lightweight">
                                        <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                        <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                                        <FocusedStyle Resize="None"></FocusedStyle>
                                        <DisabledStyle Resize="None"></DisabledStyle>
                                        <InvalidStyle Resize="None"></InvalidStyle>
                                        <HoveredStyle Resize="None"></HoveredStyle>
                                        <EnabledStyle Resize="None"></EnabledStyle>
                                    </DateInput>
                                    <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                </telerik:RadDatePicker>
                            </div>
                        </div>
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>Due Date*</label>
                                <telerik:RadDatePicker ID="dateDue" Skin="Bootstrap" runat="server" MinDate="1910-01-01" Width="100%"  ForeColor="#666666"
                                    RenderMode="Lightweight" Font-Names="Verdana">
                                    <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True"
                                        FastNavigationNextText="&amp;lt;&amp;lt;" RenderMode="Lightweight">
                                    </Calendar>
                                    <DateInput DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" LabelWidth="40%"
                                        RenderMode="Lightweight">
                                        <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                        <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                                        <FocusedStyle Resize="None"></FocusedStyle>
                                        <DisabledStyle Resize="None"></DisabledStyle>
                                        <InvalidStyle Resize="None"></InvalidStyle>
                                        <HoveredStyle Resize="None"></HoveredStyle>
                                        <EnabledStyle Resize="None"></EnabledStyle>
                                    </DateInput>
                                    <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                </telerik:RadDatePicker>
                            </div>
                        </div>
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>Reviewer's 1 Weight(%)*</label>
                                <input id="txtmgrweight" runat="server" style="height:28px;" class="form-control"  type="text" />
                            </div>
                        </div>
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>Reviewer's 2 Weight(%)*</label>
                                <input id="txtmgrweight2" runat="server" style="height:28px;" class="form-control"  type="text" />
                            </div>
                        </div>
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>Reviewee's Weight(%)*</label>
                                <input id="txtEmpweight" runat="server" style="height:28px;" class="form-control"  type="text" />
                            </div>
                        </div>
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>Status</label>
                                <telerik:RadComboBox ID="cboStatus" Skin="Bootstrap" runat="server" Width="100%"  ForeColor="#666666"
                                    RenderMode="Lightweight">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>Form Generated</label>
                                <input id="lblForm" runat="server" style="height:28px;" class="form-control" readonly="" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>Notification Sent</label>
                                <input id="lblnotification" runat="server" style="height:28px;" class="form-control" readonly="" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-3">
                            <div class="form-group">
                                <label>
                                    Created By</label>
                                <input id="lblcreatedby" runat="server" style="height:28px;" class="form-control" readonly="" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-3">
                            <div class="form-group">
                                <label>
                                    Created On</label>
                                <input id="lblcreatedon" runat="server" style="height:28px;" class="form-control" readonly="" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-3">
                            <div class="form-group">
                                <label>
                                    Updated By</label>
                                <input id="lblupdatedby" runat="server" style="height:28px;" class="form-control" readonly="" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-3">
                            <div class="form-group">
                                <label>
                                    Update On</label>
                                <input id="lblupdatedon" runat="server" style="height:28px;" class="form-control" readonly="" type="text" />
                            </div>
                        </div>
                        <div class="col-md-12 m-t-20 text-center">
                            <button id="btnupdate" runat="server" onserverclick="btnAdd_Click" type="submit"
                                style="width: 150px" class="btn btn-primary btn-success">
                                Save &amp; Update</button>
                            <button id="Button1" runat="server" onserverclick="btnCancel_Click" type="submit" style="width: 150px"
                                class="btn btn-primary btn-danger">
                                << Back</button>
                                <%--<button id="btngenerate0" runat="server" onserverclick="btnCancel_Click" type="submit" style="width: 150px"
                                class="btn btn-primary btn-info">
                                Copy Previous Objectives</button>--%>
                                <asp:Button ID="btngenerate0" runat="server" Text="Copy Previous Objectives" BackColor="#0066FF"
                                ForeColor="White" CssClass="btn btn-primary btn-info" BorderStyle="None" 
                                Font-Names="Verdana" Font-Size="14px" ToolTip="Copy previous appraisal objective of employees to this appraisal cycle" 
                                onclientclick="Confirm()" />
                                <button id="btngenerate" runat="server" onserverclick="btngenerate_Click" type="submit" style="width: 150px"
                                class="btn btn-primary btn-warning">
                                Send Objective Alert</button>
                                <button id="btnfeedbackalert" runat="server" onserverclick="btngenerate1_Click" type="submit" style="width: 150px"
                                class="btn btn-primary btn-info">
                                Send Feedback Alert</button>
                        </div>
        </div></div></div>
    </div>
    </form>
    <div class="loading" align="center">
        Processing, please wait...<br />
        <br />
        <img src="../../../images/loaders.gif" alt="" />
    </div>
</body>
</html>
</asp:Content>
