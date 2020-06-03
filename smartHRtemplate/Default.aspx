<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Default.aspx.vb" Inherits="GOSHRM.Login" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GOS HRM</title>
    <link href="https://fonts.googleapis.com/css?family=Montserrat:300,400,500,600,700"
        rel="stylesheet" />
    <link rel="icon" type="image/png" href="images/Goshrm.png" />
    <%--<link rel="stylesheet" href="AdminLTE/bootstrap/css/bootstrap.min.css" />
    <link rel="stylesheet" href="AdminLTE/dist/css/Admin-lte.min.css" />
    <link rel="stylesheet" href="AdminLTE/plugins/font-awesome/css/font-awesome.min.css" />
    <link rel="stylesheet" href="AdminLTE/plugins/ionicons/css/ionicons.min.css" />
    <link rel="stylesheet" href="AdminLTE/plugins/icheck/css/sqare/blue.css" />
    <link href="styles/css/style.css" rel="stylesheet" type="text/css" />--%>

      <link href="css/bootstrap.min.css" rel="stylesheet" type="text/css">
        <link href="css/font-awesome.min.css" rel="stylesheet" type="text/css">
		<link href="css/fullcalendar.min.css" rel="stylesheet" />
		<link href="css/dataTables.bootstrap.min.css" rel="stylesheet" type="text/css">
		<link rel="stylesheet" href="css/select2.min.css" type="text/css" >
		<link rel="stylesheet" href="css/bootstrap-datetimepicker.min.css" type="text/css" >
		<link rel="stylesheet" href="plugins/morris/morris.css" >
        <link href="css/style.css" rel="stylesheet" type="text/css" >
    <meta content='width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no'
        name='viewport' />
    <script type="text/javascript">
        window.onload = function () {
            var popup = window.open("Popup.aspx");
            if (popup == null) {
                alert("Please disable popup blocker.");
            } else {
                popup.close();
            }
        };
    </script>
    <script>
        window.onload = function () {
            var d = new Date();
            var n = d.getFullYear();
            document.getElementById("demo").innerHTML = n;
        }
</script>
    <style type="text/css">
        .style1
        {
            width: 100%;
            height: 600px;
        }
        .style4
        {
            height: 200px;
            width: 497px;
        }
        .style8
        {
            font-family: Candara;
            font-size: 40pt;
            text-decoration: blink;
        }
        .style22
        {
            text-align: center;
        }
        .style23
        {
            font-size: small;
        }
        .style26
        {
            height: 235px;
            width: 135px;
        }
        .style27
        {
            width: 497px;
            height: 235px;
        }
        .style31
        {
            height: 235px;
            width: 175px;
        }
        .style34
        {
            width: 497px;
            height: 115px;
        }
        .style35
        {
            height: 115px;
        }
        .style36
        {
            height: 115px;
            width: 135px;
        }
        .style37
        {
            height: 115px;
            width: 175px;
        }
        .style42
        {
            height: 200px;
            width: 135px;
        }
        .style43
        {
            height: 200px;
            width: 175px;
        }
        .style44
        {
            height: 200px;
        }
        .style45
        {
            font-size: x-small;
            font-family: Arial;
        }
        .style46
        {
            height: 115px;
        }
        .style47
        {
            height: 200px;
        }
        .style48
        {
            height: 235px;
        }
    </style>
</head>
<body>
    <div class="container">
        <div id="divalert" runat="server" visible="false" class="alert alert-danger">
            <strong id="msgalert" runat="server">Danger!</strong>
        </div>
    </div>
    <div class="main-wrapper">
        <div class="account-page">
            <div class="container">
                <h3 class="account-title">
                </h3>
                <div class="text-center" style="height:80px">
                          
                 </div>
                <div class="account-box">
                    <div class="account-wrapper">
                        <div class="account-logo">
                            <%--<a><img src="images/goshrm_logo.png" alt="GOS HRM" style="Height:43px; width:202px"/></a>--%>
                             <span class="style8">
                    <asp:Image ID="Image1" runat="server" Height="54px" Width="118px" 
                                ImageUrl="~/images/GOSHRM.png" />
                </span>
                        </div>
                        <form id="frmlogin" runat="server">
                        <div class="form-group has-feedback">
                            <input id="txtuid" name="uid" type="text" class="form-control" placeholder="Username" />
                            <span class="glyphicon glyphicon-user form-control-feedback"></span>
                        </div>
                        <div class="form-group has-feedback">
                            <input id="txtpwd" name="pwd" type="password" class="form-control" placeholder="Password" />
                            <span class="glyphicon glyphicon-lock form-control-feedback"></span>
                        </div>
                        <div class="text-center" style="height:20px">
                          
                        </div>
                        <div class="form-group text-center">
                            <button id="btnLogin" runat="server" onserverclick="btnLogin_Click" class="btn btn-success btn-block btn-flat btn-lg"
                                type="submit">
                                Login</button>
                        </div>
                        <div class="text-center">
									<a href="Module/Recruitment/Applications/PasswordRecovery?recovery=staff">Forgot your password?</a>
						</div>
                        <div></div>
                       
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="footer">
        <div class="style22">
            <span class="style45"><strong>GOSHRM 1.00</strong></span><br class="style23" />
            <span class="style45"><strong>© <span id="demo">2017</span> GODP. All rights reserved.</strong></span><span
                class="style23"> </span>
        </div>
    </div>
</body>
</html>
