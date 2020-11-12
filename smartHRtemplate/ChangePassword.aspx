<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ChangePassword.aspx.vb"
    Inherits="GOSHRM.ChangePassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GOS HRM</title>
    <link rel="icon" type="image/png" href="images/Goshrm.png" />
    <%--  <link rel="stylesheet" href="AdminLTE/bootstrap/css/bootstrap.min.css"/>
    <link rel="stylesheet" href="AdminLTE/dist/css/Admin-lte.min.css"/>
    <link rel="stylesheet" href="AdminLTE/plugins/font-awesome/css/font-awesome.min.css"/>
    <link rel="stylesheet" href="AdminLTE/plugins/ionicons/css/ionicons.min.css"/>
    <link rel="stylesheet" href="AdminLTE/plugins/icheck/css/sqare/blue.css"/>--%>
    <link href="css/bootstrap.min.css" rel="stylesheet" type="text/css">
    <link href="css/font-awesome.min.css" rel="stylesheet" type="text/css">
    <link href="css/fullcalendar.min.css" rel="stylesheet" />
    <link href="css/dataTables.bootstrap.min.css" rel="stylesheet" type="text/css">
    <link rel="stylesheet" href="css/select2.min.css" type="text/css">
    <link rel="stylesheet" href="css/bootstrap-datetimepicker.min.css" type="text/css">
    <link rel="stylesheet" href="plugins/morris/morris.css">
    <link href="css/style.css" rel="stylesheet" type="text/css">
    <meta content='width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no'
        name='viewport' />
    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to cancel password update?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
        function btncancel_onclick() {

        }

    </script>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            height: 211px;
        }
        .style4
        {
            height: 211px;
            width: 497px;
            background-color: #F0F0F0;
        }
        .style5
        {
            height: 116px;
        }
        .style9
        {
            font-family: Candara;
            text-transform: uppercase;
            border-left-color: #A0A0A0;
            border-right-color: #C0C0C0;
            border-top-color: #A0A0A0;
            border-bottom-color: #C0C0C0;
            padding: 1px;
        }
        .style15
        {
            height: 116px;
        }
        .style16
        {
            height: 211px;
            width: 135px;
        }
        .style18
        {
            width: 204px;
            height: 36px;
        }
        .style20
        {
            width: 204px;
            height: 35px;
        }
        .style21
        {
            height: 35px;
        }
        .style25
        {
            height: 293px;
        }
        .style26
        {
            height: 293px;
            width: 135px;
        }
        .style27
        {
            width: 497px;
            height: 293px;
        }
        .style30
        {
            height: 211px;
            width: 175px;
        }
        .style31
        {
            height: 293px;
            width: 175px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" style="background-color: White">
    <div class="container">
        <div id="divalert" runat="server" visible="false" class="alert alert-info">
            <strong id="msgalert" runat="server"></strong>
        </div>
    </div>

    <div class="container">
        <div class="row">
            <div class="col-md-offset-3  col-md-6">
                <div class="row text-center">
                            <h1 class="account-title">
                            </h1>
                            <h1 class="account-title">
                            </h1>
                            <h2 class="page-title">
                                </h2>
                        </div>
                <div class="panel panel-success">
                    <div class="panel-body">
                        <div class="row text-center">
                            <h1 class="account-title">
                            </h1>
                            <h1 class="account-title">
                            </h1>
                            <h2 class="page-title">
                                CHANGE PASSWORD</h2>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label>
                                        CURRENT PASSWORD</label>
                                    <input id="currentpwd" runat="server" class="form-control" type="password" />
                                </div>
                            </div>
                        </div>
                         
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label>
                                        NEW PASSWORD</label>
                                    <input id="newpwd" runat="server" class="form-control" type="password" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label>
                                        CONFIRM NEW PASSWORD</label>
                                    <input id="confirmpwd" runat="server" class="form-control" type="password" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 m-t-20">
                                <button id="btnupdate" runat="server" onserverclick="btnLogin_Click" type="submit"
                                    class="btn btn-success btn-block btn-lg">
                                    Save</button>
                                <label style="color: White">Current Password</label>
                                <button id="btncancel" runat="server" onserverclick="btnHome_Click" style="display:none" type="submit"
                                    class="btn btn-danger btn-block btn-lg">
                                    Not Now</button>
                            </div>
                        </div>
                        <div class="row text-center">
                            <h1 class="account-title">
                            </h1>
                            <h1 class="account-title">
                            </h1>
                            <h2 class="page-title">
                            </h2>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
