<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="SignUp.aspx.vb" Inherits="GOSHRM.SignUp" %>

<!DOCTYPE html>

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
</head>
<body>
    <form id="form1" runat="server">
        <div>
           <div class="container">
        <div id="divalert" runat="server"  visible="false" class="alert alert-danger">
            <strong id="msgalert" runat="server">Danger!</strong>
        </div>
    </div>

        <div class="account-page">
            <div class="container"><br />
                <h3 class="account-title">Applicants Sign Up</h3>
                <div class="account-box">
                    <div class="account-wrapper" style="width:180%;margin-left:-40%">
                        <div class="account-logo">
                             <span class="style8">
                    <asp:Image ID="imgProfile" runat="server" Height="100px" Width="100px" onerror="this.onerror=null; this.src='/images/file2.jpg';" ImageUrl="~/images/file2.jpg" />
                        </span>
                        </div>
                        

                        <%--<p class="login-box-msg">
                            Sign in to start your job application session</p>--%>
                        <div class="form-group has-feedback col-md-6">
                            <label>Email</label>
                            <input id="aemailadd" runat="server" name="uid" type="text" class="form-control" placeholder="Username" />
                            <span class="glyphicon glyphicon-user form-control-feedback"></span>
                        </div>
                           <div class="form-group has-feedback col-md-6">
                            <label>Firstname</label>
                            <input id="afirstnameadd" runat="server" name="uid" type="text" class="form-control" placeholder="First Name" />
                            <span class="glyphicon glyphicon-user form-control-feedback"></span>
                        </div>
                        <div class="form-group has-feedback col-md-6">
                            <label>Password</label>
                            <input id="apwd" name="pwd" type="password" runat="server" class="form-control" placeholder="Password" />
                            <span class="glyphicon glyphicon-lock form-control-feedback"></span>
                        </div>
                           <div class="form-group has-feedback col-md-6">
                            <label>Middle Name</label>
                            <input id="amiddlenameadd" runat="server" name="uid" type="text" class="form-control" placeholder="Middle Name" />
                            <span class="glyphicon glyphicon-user form-control-feedback"></span>
                        </div>
                         <div class="form-group has-feedback col-md-6">
                            <label>Comfirm Password</label>
                            <input id="aconfirmpwd" name="pwd" type="password" runat="server" class="form-control" placeholder="Password" />
                            <span class="glyphicon glyphicon-lock form-control-feedback"></span>
                        </div>
                           <div class="form-group has-feedback col-md-6">
                            <label>Lastname</label>
                            <input id="alastnameadd" runat="server" name="uid" type="text" class="form-control" placeholder="LastName" />
                            <span class="glyphicon glyphicon-user form-control-feedback"></span>
                        </div>
                        <div class="text-center" style="height:20px">
                          
                        </div>
                        <div class="form-group text-center">
                            <button id="btnLogin" runat="server" onserverclick="Btn_sendClick"   class="btn btn-primary btn-block account-btn"
                                type="submit">
                                Sign Up</button>
                        </div>
                        <div class="text-center">
                            <button id="Button1" runat="server" onserverclick="lnkOldUser"  class="btn btn-link btn-flat"
                                type="submit">
                                Already have an account, Login</button>                           
                        </div>
                        <div class="text-center">
                            <button id="Button2" runat="server" onserverclick="lnkforgetpwd"  class="btn btn-link btn-flat"
                                type="submit">
                                Forgot your password?</button>                           
                        </div>
                     
                    </div>
                </div>
            </div>
        </div>
    </div>
       
    </form>
</body>
</html>
