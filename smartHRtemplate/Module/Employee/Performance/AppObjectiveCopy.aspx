<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AppObjectiveCopy.aspx.vb"
    Inherits="GOSHRM.AppObjectiveCopy" EnableEventValidation="false" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript">
        function closeWin() {
            popup.close();   // Closes the new window
        }
    </script>
    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Copy objectives?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
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
            <div id="divalert" runat="server" visible="false" class="alert alert-info">
                <strong id="msgalert" runat="server"></strong>
                <asp:Label ID="lblid" runat="server" Font-Size="1px" Visible="False"></asp:Label>
            </div>
            <div class="panel panel-success">
                <div class="panel-heading">
                    <b>Copy Objectives Forward</b>
                </div>
                <div class="panel-body">
                    <div class="col-md-10">                                               
                        <form action="">
                        <div class="row">
                            <div class=" col-md-10">
                                <div class="form-group">
                                    <label>
                                        SOURCE*</label>
                                    <telerik:radcombobox runat="server" forecolor="#666666" dropdownautowidth="Enabled"
                                        rendermode="Lightweight" resolvedrendermode="Classic" width="100%" id="cboSource"
                                        skin="Bootstrap">
                                    </telerik:radcombobox>
                                </div>
                            </div>
                            <div class=" col-md-10">
                                <div class="form-group">
                                    <label>
                                        TARGET*</label>
                                    <telerik:radcombobox runat="server" forecolor="#666666" dropdownautowidth="Enabled"
                                        rendermode="Lightweight" resolvedrendermode="Classic" width="100%" id="cboDestination"
                                        skin="Bootstrap">
                                    </telerik:radcombobox>
                                </div>
                            </div>
                            <div class="col-md-10 m-t-20 text-center">
                                <asp:Button ID="btnAdd" runat="server" Text="Copy" ForeColor="White" Width="170px"
                                    Height="35px" BorderStyle="None" Font-Names="Verdana" CssClass="btn btn-primary btn-success"
                                    Font-Size="14px" OnClientClick="Confirm()" />
                                <button id="Button1" runat="server" onserverclick="btnCancel_Click" type="submit"
                                    style="width: 150px" class="btn btn-primary btn-info">
                                    Close</button>
                            </div>
                        </div>
                        </form>
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
