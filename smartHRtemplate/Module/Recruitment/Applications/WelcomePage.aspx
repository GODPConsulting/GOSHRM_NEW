<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="WelcomePage.aspx.vb" Inherits="GOSHRM.WelcomePage"
    EnableEventValidation="false" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        .style2
        {
            width: 167px;
        }
        .style4
        {
            font-family: Arial;
            color: #666666;
            font-size: 13px;
            line-height: 100%;
        }
        .style6
        {
            font-family: Arial, Helvetica, sans-serif;
            color: #666666;
            font-weight: normal;
            font-size: 13px;
        }
        .style7
        {
            width: 889px;
        }
        .style8
        {
            font-size: large;
        }
        .style10
        {
            width: 782px;
            font-family: Arial;
            color: #666666;
            font-size: 13px;
            line-height: 100%;
        }
        .style11
        {
            width: 782px;
            font-family: Arial;
            color: #666666;
            font-size: 13px;
            line-height: 150%;
        }
        .style12
        {
            width: 891px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%">
        <tr>
            <td class="style12">
            </td>
            <td valign="bottom">
                <table width="100%">
                    <tr>
                        <td class="style2">
                            <asp:Button ID="btnBrowseJob" runat="server" Text="Browse Jobs" BackColor="#3399FF"
                                ForeColor="White" Font-Names="Verdana" Font-Size="11px" Width="150px" Height="30px"
                                BorderStyle="None" Font-Bold="True" />
                        </td>
                        <td>
                            <asp:Button ID="btnSignIn" runat="server" Text="Sign In" BackColor="#FF6666" ForeColor="White"
                                Font-Names="Verdana" Font-Size="11px" Width="150px" Height="30px" BorderStyle="None"
                                Font-Bold="True" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
            </td>
            <td>
                <div style="text-align: right">
                    <asp:Image ID="imgProfile" runat="server" Height="90px" Width="130px" />
                </div>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td>
                <asp:Image ID="imgbgwork" runat="server" Height="120px" ImageUrl="~/images/subheader-image-jobline-1.jpg"
                    Width="100%" />
            </td>
        </tr>
    </table>
    <table width="100%" style="height: 350px">
        <tr>
            <td class="style7">
                <h2 class="style6">
                    <strong><span class="style8">WELCOME</span></strong>
                </h2>
                <p class="style4">
                    Welcome to our Recruitment Campaign. Thank you for thinking about us.
                </p>
                <p class="style11">
                    Our recruitment process is summarized below:
                </p>
                <ol>
                    <li class="style10">
                        <p class="style10">
                            Register and obtain your login details (ID and password).</p>
                    </li>
                    <li class="style10">
                        <p class="style10">
                            Please remember your login details, it will be required to apply for a particular
                            vacancy.</p>
                    </li>
                    <li class="style10">
                        <p class="style10">
                            If you forget your password, use the service provided to retrieve it. An email will
                            be sent to your e mail address.</p>
                    </li>
                    <li class="style10">
                        <p class="style10">
                            Ensure your CV has the following information before you apply for any of the jobs:
                        </p>
                        <ul>
                            <li class="style10">
                                <p class="style10">
                                    Personal Information</p>
                            </li>
                            <li class="style10">
                                <p class="style10">
                                    Computer Appreciation</p>
                            </li>
                            <li class="style10">
                                <p class="style10">
                                    Education Qualification and Certification</p>
                            </li>
                            <li class="style10">
                                <p class="style10">
                                    Work Experience</p>
                            </li>
                            <li class="style10">
                                <p class="style10">
                                    Referee</p>
                            </li>
                        </ul>
                    </li>
                    <li class="style10">
                        <p class="style10">
                            To apply for any of the vacancies, follow the link provided. You will be prompted
                            to input your ID and password to apply for your chosen vacancy..</p>
                    </li>
                    <li class="style10">
                        <p class="style10">
                            Application process also allows you to update and amend your CV information at any
                            time.</p>
                    </li>
                    <li class="style10">
                        <p class="style10">
                            To update and amend your CV, follow the link provided. You require your ID and password.</p>
                    </li>
                </ol>
            </td>
            <td>
                <asp:Image ID="imgProfile0" runat="server" Height="100%" Width="100%" ImageUrl="~/images/public-domain-world-country-outline-map.gif" />
            </td>
        </tr>
    </table>
    <br />
    <table width="100%" style="background-color:#1BA691; height: 109px;">
        <tr>
            <td valign="top">
                <asp:Label ID="lblHeader" runat="server" Text="DISCLAIMER!!!" Font-Bold="True" 
                    Font-Names="Arial" Font-Size="16px" BackColor="#1BA691" ForeColor="White" Width="100%"></asp:Label>
                    <asp:Label ID="Label1" runat="server" Text=" " Font-Bold="True" 
                    Font-Names="Arial" Font-Size="16px" BackColor="#1BA691" ForeColor="White" Width="100%"></asp:Label>
                    <asp:Label ID="Label2" runat="server" Text=" " Font-Bold="True" 
                    Font-Names="Arial" Font-Size="16px" BackColor="#1BA691" ForeColor="White" Width="100%"></asp:Label>
                    <asp:Label ID="Label3" runat="server" Text=" " Font-Bold="True" 
                    Font-Names="Arial" Font-Size="16px" BackColor="#1BA691" ForeColor="White" Width="100%"></asp:Label>
                    <asp:Label ID="Label4" runat="server" Text=" " Font-Bold="True" 
                    Font-Names="Arial" Font-Size="16px" BackColor="#1BA691" ForeColor="White" Width="100%"></asp:Label>
                    <asp:Label ID="lblDisclaimer" runat="server" BackColor="#1BA691" ForeColor="White"
                    Text="The recruitment process gives equal employment opportunity and will under no circumstance request applicants to pay money or give any personal items of monetary value to our company or any agency. " 
                    Font-Names="Verdana" Font-Size="12px" Width="100%"></asp:Label>
                    <asp:Label ID="Label5" runat="server" Text=" " Font-Bold="True" 
                    Font-Names="Arial" Font-Size="16px" BackColor="#1BA691" ForeColor="White" Width="100%"></asp:Label>
                    <asp:Label ID="Label6" runat="server" Text=" " Font-Bold="True" 
                    Font-Names="Arial" Font-Size="16px" BackColor="#1BA691" ForeColor="White" Width="100%"></asp:Label>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
