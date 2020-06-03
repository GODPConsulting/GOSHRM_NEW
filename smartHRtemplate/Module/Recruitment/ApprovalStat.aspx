<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ApprovalStat.aspx.vb"
    Inherits="GOSHRM.ApprovalStat" EnableEventValidation="false" %>

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
</head>
<body onunload="window.opener.location=window.opener.location;">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <div class="container">
    <div class="row">
        <div class=" col-md-8">
            <div id="divalert" runat="server" visible="false" class="alert alert-info">
                <strong id="msgalert" runat="server"></strong>
                <asp:Label ID="lblempid" runat="server" Font-Size="1px" Visible="False"></asp:Label>
                <asp:Label ID="lblid" runat="server" Font-Size="1px" Visible="False"></asp:Label>
                <asp:Label ID="lblinitiator" runat="server" Font-Size="1px" Visible="False"></asp:Label>
                <asp:Label ID="lblfinalapprovalstat" runat="server" Font-Size="1px" Visible="False"></asp:Label>
                <asp:Label ID="lbljobgrade" runat="server" Font-Size="1px" Visible="False"></asp:Label>
                    <asp:Label ID="lbljobgradeold" runat="server" Font-Size="1px" visible="False"></asp:Label>
                     <asp:Label ID="lbljobtitle" runat="server"  Font-Size="1px" 
                    Visible="False"></asp:Label>
                    <asp:Label ID="lbljobtitleold" runat="server"  Font-Size="1px" 
                    Visible="False"></asp:Label> 
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-8 col-md-offset-0">
            <h5 id="pagetitle" runat="server" class="page-title">
                Approval Status</h5>
        </div>
    </div>
    <div class="row">
        <div class=" col-md-8">
            <div class="panel panel-success">
                <div class="panel-heading">
                    <b>1. APPROVER</b>
                </div>
                <div class="panel-body">
                        <div class="row">
                            <div class=" col-md-6">
                                <div class="form-group">
                                    <label>
                                        NAME</label>
                                    <telerik:RadComboBox ID="cboapprover1" runat="server" ForeColor="#666666" Width="100%"
                                        Filter="Contains" RenderMode="Lightweight" Skin="Bootstrap">
                                    </telerik:RadComboBox>
                                </div>
                            </div>
                            <div class=" col-md-6">
                                <div class="form-group">
                                    <label>
                                        APPROVAL</label>
                                    <input id="approver1stat" runat="server" class="form-control" type="text" disabled="disabled" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class=" col-md-12">
                                <div class="form-group">
                                    <label>
                                        COMMENT</label>
                                    <textarea id="approver1comment" runat="server" class="form-control" rows="3" cols="1"
                                        readonly="readonly"></textarea>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class=" col-md-12">
                                <div class="form-group">
                                    <label>
                                        DATE</label>
                                    <input id="approver1date" runat="server" class="form-control" type="text" disabled="disabled" />
                                </div>
                            </div>
                        </div>
                    </div>
            </div>
        </div>
    </div>
    <div id="divapprover2" runat="server" class="row">
        <div class=" col-md-8">
            <div class="panel panel-success">
                <div class="panel-heading">
                    <b>II. APPROVER</b>
                </div>
                <div class="panel-body">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>
                                        NAME</label>
                                    <telerik:RadComboBox ID="cboapprover2" runat="server" ForeColor="#666666" Width="100%"
                                        Filter="Contains" RenderMode="Lightweight" Skin="Bootstrap">
                                    </telerik:RadComboBox>
                                </div>
                            </div>
                            <div class=" col-md-6">
                                <div class="form-group">
                                    <label>
                                        APPROVAL</label>
                                    <input id="approver2stat" runat="server" class="form-control" type="text" disabled="disabled" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label>
                                        COMMENT</label>
                                    <textarea id="approver2comment" runat="server" class="form-control" rows="3" cols="1"
                                        readonly="readonly"></textarea>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class=" col-md-12">
                                <div class="form-group">
                                    <label>
                                        DATE</label>
                                    <input id="approver2date" runat="server" class="form-control" type="text" disabled="disabled" />
                                </div>
                            </div>
                        </div>
                    </div>
            </div>
        </div>
    </div>
    <div id="divapprover3" runat="server" class="row">
        <div class=" col-md-8">
            <div class="panel panel-success">
                <div class="panel-heading">
                    <b>III. APPROVER</b>
                </div>
                <div class="panel-body">
                        <div class="row">
                            <div class=" col-md-6">
                                <div class="form-group">
                                    <label>
                                        NAME</label>
                                    <telerik:RadComboBox ID="cboapprover3" runat="server" ForeColor="#666666" Width="100%"
                                        Filter="Contains" RenderMode="Lightweight" Skin="Bootstrap">
                                    </telerik:RadComboBox>
                                </div>
                            </div>
                            <div class=" col-md-6">
                                <div class="form-group">
                                    <label>
                                        APPROVAL</label>
                                    <input id="approver3stat" runat="server" class="form-control" type="text" disabled="disabled" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class=" col-md-12">
                                <div class="form-group">
                                    <label>
                                        COMMENT</label>
                                    <textarea id="approver3comment"  readonly="readonly" runat="server" class="form-control" rows="3" cols="1"></textarea>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class=" col-md-12">
                                <div class="form-group">
                                    <label>
                                        DATE</label>
                                    <input id="approver3date" runat="server" class="form-control" type="text" disabled="disabled" />
                                </div>
                            </div>
                        </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-8 m-t-20">
            <button id="btnupdate" runat="server" onserverclick="btnSave_Click" type="submit"
                style="width: 150px" class="btn btn-primary btn-success">
                Save &amp; Update</button>
            <button id="Button1" runat="server" onserverclick="btnCancel_Click" type="submit"
                style="width: 150px" class="btn btn-primary btn-danger">
                Close</button>
        </div>
    </div>
   </div>
    </form>
    <div class="loading" align="center">
        Processing, please wait...<br />
        <br />
        <img src="~/images/loaders.gif" alt="" />
    </div>
</body>
</html>
