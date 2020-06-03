

    <%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="LibraryDetail.aspx.vb"
    Inherits="GOSHRM.LibraryDetail" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Notify Trainees to commence Learning Assessment?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>

    <script type="text/javascript">
        function ConfirmApp() {
            var confirm_value1 = document.createElement("INPUT");
            confirm_value1.type = "hidden";
            confirm_value1.name = "confirm_value1";
            if (confirm("Notify Trainees to commence Application Assessment?")) {
                confirm_value1.value = "Yes";
            } else {
                confirm_value1.value = "No";
            }
            document.forms[0].appendChild(confirm_value1);
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
         .center
        {
            z-index: 1000;
            margin: 300px auto;
            padding: 10px;
            width: 130px;
            background-color: White;
            border-radius: 10px;
            filter: alpha(opacity=100);
            opacity: 1;
            -moz-opacity: 1;
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
    <script type="text/javascript" id="telerikClientEvents1">
//<![CDATA[

        function drpTrainee_DropDownClosing(sender, args) {
            //Add JavaScript handler code here
            document.getElementById("Button1").click();
        }
//]]>
    </script>
    <script type="text/javascript" id="telerikClientEvents2">
//<![CDATA[

        function cboTrainer_DropDownClosing(sender, args) {
            //Add JavaScript handler code here
            document.getElementById("Button2").click();
        }
//]]>
    </script>
</head>

<body >
    <form id="form1">
    <div class="container col-md-10">
        <div class="row">
            <div id="divalert" runat="server" visible="false" class="alert alert-info">
                <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <strong
                    id="msgalert" runat="server">Danger!</strong>
            </div>
        </div>
        <div class="panel panel-success">
         <div class="panel-heading">
        <div class="row">
            <div class="col-md-8">
                <h5 id="pagetitle" runat="server" class="page-title" style="color: #1BA691">
                    Training Detail</h5><asp:TextBox ID="txtid" runat="server" Font-Size="1px"  Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtsessiontype" runat="server" Font-Size="1px" Visible="False"></asp:TextBox>
            </div>
        </div>
        </div>
         <div class="panel-body">
         <div class="row">
            <div class="">
                <div class="form-group">
                    <label>
                        COURSE TITLE</label>
                    <input id="acourse" runat="server" class="form-control" type="text" disabled="disabled" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="">
                <div class="form-group">
                    <label>
                        SESSION</label>
                    <input id="atrainingsession" runat="server" class="form-control" type="text" disabled="disabled" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="">
                <div class="form-group">
                    <label>
                        TRAINING COORDINATOR</label>
                    <input id="acoordinator" runat="server" class="form-control" type="text" disabled="disabled" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="">
                <div class="form-group">
                    <label>
                       TRAINING DATE</label>
                    <input id="adate" runat="server" class="form-control" type="text" disabled="disabled" />
                </div>
            </div>
            <div class="">
                <div class="form-group">
                    <label>
                       TIME</label>
                    <input id="atime" runat="server" class="form-control" type="text" disabled="disabled" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="">
                <div class="form-group">
                    <label>
                        TRAINER(S)</label>
                    <div style="overflow: auto">
                        <asp:DataList ID="dlBlogs" runat="server" Width="100%" RepeatColumns="1" CellSpacing="0"
                            RepeatLayout="Table" Font-Names="Arial" Font-Size="14px" GridLines="Both"
                            BorderColor="#CCCCCC" ForeColor="#666666" BorderStyle="Solid" CssClass=" w3-ul w3-border"
                            BorderWidth="1px">
                            <ItemTemplate>
                                <li><%# Eval("Lists")%></a></li>                                
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                </div>
            </div>
        </div>
         <div class="row">
            <div class="">
                <div class="form-group">
                    <label>
                        DELIVERY METHOD</label>
                    <input id="adeliverymethod" runat="server" class="form-control" type="text" disabled="disabled" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="">
                <div class="form-group">
                    <label>
                        OBJECTIVE</label>
                    <textarea id="aobjective" runat="server" class="form-control" rows="3" cols="1" readonly="readonly"></textarea>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="">
                <div class="form-group">
                    <label>
                        VENUE</label>
                    <textarea id="alocation" runat="server" class="form-control" rows="3" cols="1" readonly="readonly"></textarea>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="">
                <div class="form-group">
                    <label>
                        FORWARD TO FOR APPROVAL</label>
                    <telerik:RadComboBox ID="cboapprover" runat="server" ForeColor="#666666" Width="100%"
                         Filter="Contains" RenderMode="Lightweight" Skin="Bootstrap">
                    </telerik:RadComboBox>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="">
                <div class="form-group">
                    <label>
                        REASON</label>
                    <textarea id="areason" runat="server" class="form-control" rows="3" cols="1" placeholder="Put reason for training to speed approval process"></textarea>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 m-t-20 text-center">
                <button id="btnupdate" runat="server" onserverclick="btnAdd_Click" type="submit"
                    style="width: 150px" class="btn btn-primary btn-success" title="Apply to attend training" >
                    APPLY</button>
                <button id="Button3" runat="server" onserverclick="btnCancel_Click" type="submit"
                    style="width: 150px" class="btn btn-primary btn-info">
                    << BACK</button>
            </div>
        </div>
        </div></div>
    </div>
    </form>
    <div class="loading" align="center">
        Processing, please wait...<br />
        <br />
        <img src="~/images/loaders.gif" alt="" />
    </div>
</body>
</html>
</asp:Content>