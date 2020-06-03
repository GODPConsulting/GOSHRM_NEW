<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PasswordRecovery.aspx.vb" Inherits="GOSHRM.PasswordRecovery" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>

   <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600,700,300italic,400italic,600italic" />
    <link href="../../../css/bootstrap.min.css" rel="stylesheet" type="text/css">
    <link href="../../../css/font-awesome.min.css" rel="stylesheet" type="text/css">
    <link href="../../../css/fullcalendar.min.css" rel="stylesheet" />
    <link href="../../../css/dataTables.bootstrap.min.css" rel="stylesheet" type="text/css">
    <link rel="stylesheet" href="../../../css/select2.min.css" type="text/css">
    <link rel="stylesheet" href="../../../css/bootstrap-datetimepicker.min.css" type="text/css">
    <link rel="stylesheet" href="../../../plugins/morris/morris.css">
    <link href="../../../css/style.css" rel="stylesheet" type="text/css">
    <link href="../../../css/gridview.css" rel="stylesheet" type="text/css">
    <link href="../../../css/w3.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/slider-goke.css" rel="stylesheet" type="text/css" />

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
    <form id="form1" runat="server">
    <div class="container">
        <div id="divalert" runat="server" visible="false" class="alert alert-danger">
            <strong id="msgalert" runat="server">Danger!</strong>
        </div>
    </div>


    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">             
        <asp:View ID="Recovery" runat="server">
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
                             <span class="style8">
                    <asp:Image ID="imgProfile" runat="server" Height="100px" Width="150px" />
                        </span>
                        </div>
                        
                        <p class="login-box-msg text-center">
                            Let's get you into your account</p>
                        <div class="form-group has-feedback">
                            <input id="txtuid" name="uid" type="text" class="form-control" placeholder="enter sign-in email address" />
                            <span class="glyphicon form-control-feedback"></span>
                        </div>
                        
                        <div class="text-center" style="height:20px">
                          
                        </div>
                        <div class="form-group text-center">
                            <button id="btnlog" runat="server" onserverclick="btnLogin_Click" class="btn btn-success btn-block btn-flat btn-lg"
                                type="submit">
                                Recover Password</button>
                        </div>
                        
                        
                    </div>
                </div>
            </div>
        </div>
    </div>

        </asp:View>
         <asp:View ID="result" runat="server">
             <div class="main-wrapper">
        <div class="account-page">
            <div class="container text-center">
                <h3 id="fstatus" runat="server" class="account-title">
                    Test
                </h3>
            </div>
            </div>
            </div>
        </asp:View>
    </asp:MultiView>
    
    </form>
</body>
</html>
