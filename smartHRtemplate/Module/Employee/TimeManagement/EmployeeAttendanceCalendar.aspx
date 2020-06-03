<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="EmployeeAttendanceCalendar.aspx.vb"
    Inherits="GOSHRM.EmployeeAttendanceCalendar" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
  
  
    <title></title>
    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you wish to continue with this action?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
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
    <body>
        <form id="form1">
        <div class="row">
            <div class="col-md-12">
                <div id="divalert" runat="server" visible="false" class="alert alert-info">
                    <strong id="msgalert" runat="server">Danger!</strong>
                    <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-8 col-md-12">
                <h5 id="pagetitle" runat="server" class="page-title" style="color: #1BA691">
                    Head</h5>
                     <asp:Label ID="lblid" runat="server" Font-Names="Verdana" Font-Size="1px" 
                                        Visible="False"></asp:Label>
                                    <asp:Label ID="lblholidayid" runat="server" Font-Names="Verdana" Font-Size="1px" 
                                        Visible="False"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <div id="divapproval" runat="server" class="w3-panel w3-pale-green w3-bottombar w3-border-green w3-border">
                    <label style="font-size: 12px">
                        Present (including weekends and holidays)</label>
                </div>
            </div>
            <div class="col-md-6">
                <div id="div1" runat="server" class="w3-panel  w3-pale-yellow w3-bottombar w3-border-yellow w3-border">
                    <label style="font-size: 12px">
                        Absent (including weekends and holidays)</label>
                </div>
            </div>
        </div>
        <div class="row">
            <div class=" col-md-6">
                <div class="form-group">
                    <asp:Calendar ID="MyPCalendar" runat="server" BackColor="White" BorderColor="#FFCC66"
                        BorderWidth="1px" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt"
                        ForeColor="#663399" Height="300px" ShowGridLines="True" Width="500px">
                        <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Height="1px" HorizontalAlign="Center" />
                        <DayStyle HorizontalAlign="Center" Font-Bold="True" />
                        <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
                        <OtherMonthDayStyle ForeColor="#CC9966" />
                        <SelectedDayStyle BackColor="Black" Font-Bold="True" />
                        <SelectorStyle BackColor="#FFCC66" />
                        <TitleStyle BackColor="#999999" Font-Bold="True" Font-Size="9pt" ForeColor="White" />
                        <TodayDayStyle BackColor="Silver" ForeColor="Black" />
                        <WeekendDayStyle ForeColor="Red" />
                    </asp:Calendar>
                </div>
            </div>
            <div class=" col-md-6">
                <div class="form-group">
                    <div class="row">
                        <div class="col-md-12">
                            <label id="lbdateheader" runat="server"></label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <label>Assigned Shift</label>
                            <input id="ashift" runat="server" class="form-control" type="text" disabled="disabled"/>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <label>Expected Duration (Hours)</label>
                            <input id="ashiftduration" runat="server" class="form-control" type="text" disabled="disabled"/>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <label>Clock In</label>
                            <input id="aclockin" runat="server" class="form-control" type="text" disabled="disabled" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <label>Date Clock Out</label>
                            <input id="aDateOut" runat="server" class="form-control" type="text" disabled="disabled" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <label>Clock Out</label>
                            <input id="aclockout" runat="server" class="form-control" type="text" disabled="disabled" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                             <label>Hours Logged</label>
                            <input id="ahourlogged" runat="server" class="form-control" type="text" disabled="disabled" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <label>System Overtime</label>
                            <input id="aovertime" runat="server" class="form-control" type="text" disabled="disabled" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <label>Approved Overtime</label>
                            <input id="aapprovedovertime" runat="server" class="form-control" type="text" disabled="disabled" />
                        </div>
                    </div>
                    <div id="divovertimestatid" runat="server" class="row">
                        <div class="col-md-12">
                            <label>Overtime Pay Request</label>
                            <input id="aovertimestat" runat="server" class="form-control" type="text" disabled="disabled" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <button id="btrequest" type="button" runat="server" class="btn btn-block btn-success" onserverclick="btnRequest_Click" title="request pay for overtime" 
                                >
                                Request Overtime Pay
                            </button>
                        </div>
                        <div class="col-md-6">
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel Request"  OnClientClick="Confirm()"
                                ForeColor="White" CssClass="btn btn-block btn-danger"
                        BorderStyle="None" Font-Names="Verdana" Font-Size="13px" />
                               
                        </div>
                    </div>

                </div>
            </div>
        </div>
      

                
        </form>
    </body>
    </html>
    <div class="loading" align="center">
        Please wait...<br />
        <br />
        <img src="../../../images/loaders.gif" alt="" />
    </div>
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
    </style>
</asp:Content>
