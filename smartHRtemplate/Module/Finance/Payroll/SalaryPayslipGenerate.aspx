<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="SalaryPayslipGenerate.aspx.vb"
    Inherits="GOSHRM.SalaryPayslipGenerate" EnableEventValidation="false" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Run Payslips</title>

    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600,700,300italic,400italic,600italic" />
    <link href="~/css/bootstrap.min.css" rel="stylesheet" type="text/css">
    <link href="~/css/font-awesome.min.css" rel="stylesheet" type="text/css">
    <link href="~/css/fullcalendar.min.css" rel="stylesheet" />
    <link href="~/css/dataTables.bootstrap.min.css" rel="stylesheet" type="text/css">
    <link rel="stylesheet" href="~/css/select2.min.css" type="text/css">
    <link rel="stylesheet" href="~/css/bootstrap-datetimepicker.min.css" type="text/css">
    <link rel="stylesheet" href="~/plugins/morris/morris.css">
    <link href="~/css/style.css" rel="stylesheet" type="text/css">
    <link href="~/css/gridview.css" rel="stylesheet" type="text/css">

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

</head>

<body onunload="window.opener.location=window.opener.location;" style="height: 317px">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="container">
        <div class="row">
            <div class=" col-md-8">
                <div id="divalert" runat="server" visible="false" class="alert alert-info">
                    <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <strong
                        id="msgalert" runat="server">Danger!</strong>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-8">
                <div class="panel panel-success">
                    <div class="panel-heading">
                        <b>Payroll Generation</b>
                    </div>
                    <div class="panel-body">
                        <div class=" col-md-12">
                            <div class="row">
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <label>
                                            Company</label>
                                        <telerik:radcombobox id="cbocompany" runat="server" forecolor="#666666" width="100%"
                                            filter="Contains" rendermode="Lightweight" skin="Bootstrap">
                                        </telerik:radcombobox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            Start</label>
                                            <telerik:RadDatePicker Skin="Bootstrap" ID="dateFrom" runat="server" Font-Names="Verdana" 
                                        Height="30px" Width="100%" Culture="en-US" ResolvedRenderMode="Classic" ForeColor="#666666">
                                        <calendar Height="30px" enableweekends="True" fastnavigationnexttext="&amp;lt;&amp;lt;" 
                                            usecolumnheadersasselectors="False" userowheadersasselectors="False">
                                        </calendar>
                                        <dateinput dateformat="dd/MM/yyyy" displaydateformat="dd/MM/yyyy" height="30px" 
                                            labelwidth="40%" width="">
                                        <emptymessagestyle resize="None" />
                                        <readonlystyle resize="None" />
                                        <focusedstyle resize="None" />
                                        <disabledstyle resize="None" />
                                        <invalidstyle resize="None" />
                                        <hoveredstyle resize="None" />
                                        <enabledstyle resize="None" />
                                        </dateinput>
                                        <DatePopupButton HoverImageUrl="" ImageUrl="" CssClass="" />
                                    </telerik:RadDatePicker>
                                    </div>
                                </div>
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            End</label>
                                        <telerik:RadDatePicker Skin="Bootstrap" ID="datEnd" runat="server" Font-Names="Verdana" 
                                        Height="30px" Width="100%" Culture="en-US" ResolvedRenderMode="Classic" ForeColor="#666666">
                                        <calendar Height="30px" enableweekends="True" fastnavigationnexttext="&amp;lt;&amp;lt;" 
                                            usecolumnheadersasselectors="False" userowheadersasselectors="False">
                                        </calendar>
                                        <dateinput dateformat="dd/MM/yyyy" displaydateformat="dd/MM/yyyy" height="30px" 
                                            labelwidth="40%" width="">
                                        <emptymessagestyle resize="None" />
                                        <readonlystyle resize="None" />
                                        <focusedstyle resize="None" />
                                        <disabledstyle resize="None" />
                                        <invalidstyle resize="None" />
                                        <hoveredstyle resize="None" />
                                        <enabledstyle resize="None" />
                                        </dateinput>
                                        <DatePopupButton HoverImageUrl="" ImageUrl="" CssClass="" />
                                    </telerik:RadDatePicker>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12 m-t-20 text-center">
                                    <button id="btnupdate" runat="server" onserverclick="btnAdd_Click" type="submit"
                                        style="width: 150px" class="btn btn-primary btn-success">
                                        Run</button>
                                    <button id="Button1" runat="server" onserverclick="btnCancel_Click" type="submit"
                                        style="width: 150px" class="btn btn-primary btn-info">
                                        Back</button>
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
        <img src="../../../images/loaders.gif" alt="" />
    </div>
</body>
</html>
