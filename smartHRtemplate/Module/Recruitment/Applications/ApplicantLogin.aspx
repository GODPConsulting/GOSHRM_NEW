<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ApplicantLogin.aspx.vb" Inherits="GOSHRM.ApplicantLogin" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
</head>
<body>
    <div class="container">
        <div id="divalert" runat="server" visible="false" class="alert alert-danger">
            <strong id="msgalert" runat="server">Danger!</strong>
        </div>
    </div>
    <div class="main-wrapper">
        <div class="account-page">
            <div class="container"><br />
                <h3 class="account-title">Applicant Login</h3>
                <div class="account-box">
                    <div class="account-wrapper">
                        <div class="account-logo">
                             <span class="style8">
                    <asp:Image ID="imgProfile" runat="server" Height="100px" Width="100px" onerror="this.onerror=null; this.src='/images/file2.jpg';" ImageUrl="~/images/file2.jpg" />
                        </span>
                        </div>
                        <form id="frmlogin" runat="server">
                        <%--<p class="login-box-msg">
                            Sign in to start your job application session</p>--%>
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
                            <button id="btnLogin" runat="server" onserverclick="btnLogin_Click" class="btn btn-primary btn-block account-btn"
                                type="submit">
                                Login</button>
                        </div>
                        <div class="text-center">
                            <button id="Button1" runat="server" onserverclick="lnkNewUser" class="btn btn-link btn-flat"
                                type="submit">
                                Don&#39;t have an account, Sign Up</button>                           
                        </div>
                        <div class="text-center">
                            <button id="Button2" runat="server" onserverclick="lnkforgetpwd" class="btn btn-link btn-flat"
                                type="submit">
                                Forgot your password?</button>                           
                        </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
    
</body>
</html>
