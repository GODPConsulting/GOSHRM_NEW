<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="WorkForceMovePlan.aspx.vb"
    Inherits="GOSHRM.WorkForceMovePlan" EnableEventValidation="false" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

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
    <link href="~/css/w3.css" rel="stylesheet" type="text/css" />
    <link href="~/css/slider-goke.css" rel="stylesheet" type="text/css" />

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
              if (confirm("Move Budget to selected New Year?")) {
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

  
</head>

<body onunload="window.opener.location=window.opener.location;">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
        rel="Stylesheet" type="text/css" />

<div class="container">
    <div class="row">
        <div id="divalert" runat="server" visible="false" class="alert alert-info">
            <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <strong
                id="msgalert" runat="server">Danger!</strong>
            <asp:Label ID="lblid" runat="server" Font-Size="1px" Visible="False"></asp:Label>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-8">
            <h5 id="pagetitle" runat="server" class="page-title" style="color: #1BA691">
                Adopt for New Financial Year</h5>
        </div>
    </div>
    <div class="row">
        <div class="col-md-8">
            <div class="form-group">
                <label>
                    COMPANY</label>
                <input id="acompany" runat="server" class="form-control" type="text" disabled="disabled" />
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-8">
            <div class="form-group">
                <label>
                    CURRENT BUDGET</label>
                <input id="abudget" runat="server" class="form-control" type="text" disabled="disabled" />
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-8">
            <div class="form-group">
                <label>
                    NEXT BUDGET</label>
                <telerik:radcombobox runat="server" forecolor="#666666" dropdownautowidth="Enabled"
                    rendermode="Lightweight" resolvedrendermode="Classic" width="100%" id="cboNewBudget"
                    skin="Bootstrap">
                    <items>
<telerik:RadComboBoxItem runat="server" Text="January" Value="1" Owner="cboNewBudget"></telerik:RadComboBoxItem>
<telerik:RadComboBoxItem runat="server" Text="February" Value="2" Owner="cboNewBudget"></telerik:RadComboBoxItem>
<telerik:RadComboBoxItem runat="server" Text="March" Value="3" Owner="cboNewBudget"></telerik:RadComboBoxItem>
<telerik:RadComboBoxItem runat="server" Text="April" Value="4" Owner="cboNewBudget"></telerik:RadComboBoxItem>
<telerik:RadComboBoxItem runat="server" Text="May" Value="5" Owner="cboNewBudget"></telerik:RadComboBoxItem>
<telerik:RadComboBoxItem runat="server" Text="June" Value="6" Owner="cboNewBudget"></telerik:RadComboBoxItem>
<telerik:RadComboBoxItem runat="server" Text="July" Value="7" Owner="cboNewBudget"></telerik:RadComboBoxItem>
<telerik:RadComboBoxItem runat="server" Text="August" Value="8" Owner="cboNewBudget"></telerik:RadComboBoxItem>
<telerik:RadComboBoxItem runat="server" Text="September" Value="9" Owner="cboNewBudget"></telerik:RadComboBoxItem>
<telerik:RadComboBoxItem runat="server" Text="October" Value="10" Owner="cboNewBudget"></telerik:RadComboBoxItem>
<telerik:RadComboBoxItem runat="server" Text="November" Value="11" Owner="cboNewBudget"></telerik:RadComboBoxItem>
<telerik:RadComboBoxItem runat="server" Text="December" Value="12" Owner="cboNewBudget"></telerik:RadComboBoxItem>
</items>
                </telerik:radcombobox>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-8 m-t-20 text-center">
            <asp:Button ID="btnupdate" runat="server" Text="Copy" OnClientClick="Confirm()"
                     ForeColor="White" Width="80px" Height="34px" CssClass="btn btn-success"
                    BorderStyle="None" Font-Names="Verdana" Font-Size="14px" />
            <button id="btcancel" runat="server" onserverclick="btnCancel_Click" type="submit"
                class="btn btn-primary btn-info">
                Close</button>
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
