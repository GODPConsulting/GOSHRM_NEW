﻿<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup ="true" CodeBehind="EmployeePenConUpdate.aspx.vb" Inherits="GOSHRM.EmployeePenConUpdate" EnableEventValidation="false" Debug="true"%>
<asp:Content ID = "content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI.Gantt" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Add New</title>
    <link rel="icon" type="image/png" href="../../../images/goshrm.png">
    <script type="text/javascript">

        function closeWin() {
            popup.close();   // Closes the new window
        }
   

    </script>

     
</head>



<body>
    <form>
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>

    
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
        rel="Stylesheet" type="text/css" />

         <div class="container col-md-8">
            <div class="row">
            <div id="divalert" runat="server" visible="false" class="alert alert-info">
                <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                <strong id="msgalert" runat="server">Danger!</strong>
                <asp:TextBox ID="txtid" runat="server" Width="3px"  
                    Font-Names="Candara" Height="2px" Visible="False"></asp:TextBox>
            </div>
                   <div class="panel panel-success">
                        <div class="panel-heading">
                            <h5><b id="pagetitle" runat="server">EMPLOYEE PENSION CONTRIBUTION</b></h5>
                        </div>
                     <div class="panel-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>EMPLOYEE*</label>
                                 <telerik:RadComboBox runat="server" ResolvedRenderMode="Classic" ForeColor="#666666"
                                    Font-Names="Verdana" Font-Size="12px" Skin="Bootstrap" ID="cboEmployee" Filter="Contains" 
                                    Width="100%">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>CONTRIBUTION*</label>
                                <input id="txtContribution" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class="col-md-12 m-t-20 text-center">
                            <button id="btnupdate" runat="server" onserverclick="btnAdd_Click" type="submit"
                                style="width: 150px" class="btn btn-primary btn-success">
                                Save &amp; Update</button>
                            <button id="Button1" runat="server" onserverclick="btnCancel_Click" type="submit" style="width: 150px"
                                class="btn btn-primary btn-danger">
                                << Back</button>
                        </div>
                    </div>
                </div>
            </div>
             </div>
            </div>
<%--    <table>
        <tr>
            <td class="style1" colspan="2" style="background-color: #1BA691" bgcolor="#0066FF">
                Employee Pension Contribution</td>
        </tr>
        <tr>
            <td class="style6">
                <asp:TextBox ID="txtid" runat="server" Width="13px" Style="font-size: medium; font-family: Candara"
                    Font-Names="Candara" Height="10px" Visible="False"></asp:TextBox>
            </td>
            <td class="style7">
                <asp:Label ID="lblcompany" runat="server" Font-Names="Verdana" Font-Size="12px" 
                    Font-Bold="True" ForeColor="#666666"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style6">
                <asp:Label ID="Label1" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                    Text="Employee"></asp:Label>
                <asp:Label ID="Label2" runat="server" ForeColor="#FF3300" Text=" *"></asp:Label>
            </td>
            <td class="style7">
                <telerik:RadComboBox runat="server" ResolvedRenderMode="Classic" ForeColor="#666666"
                    Font-Names="Verdana" Font-Size="12px" ID="cboEmployee" Filter="Contains" 
                    Width="100%">
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td class="style6">
                <asp:Label ID="Label7" runat="server" Font-Names="Verdana" Font-Size="12px" 
                    Font-Bold="True" ForeColor="#666666"
                    Text="Contribution"></asp:Label>
                <asp:Label ID="Label12" runat="server" ForeColor="#FF3300" Text=" *"></asp:Label>
            </td>
            <td class="style7">
                                            <asp:TextBox runat="server" ForeColor="#666666"
                    BorderColor="#CCCCCC" BorderWidth="1px" Height="20px" Width="100px" 
                    ID="txtContribution"></asp:TextBox>

            </td>
        </tr>
        <tr>
            <td class="style2" style="border-bottom-style: solid; border-bottom-width: thin;
                border-bottom-color: #C0C0C0">
                * Required Field
            </td>
            <td style="border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #C0C0C0"
                class="style7">
            </td>
        </tr>
        <tr>
            <td class="style6">
            </td>
            <td class="style7">
                <asp:Label ID="lblstatus" runat="server" Font-Bold="True" Font-Names="Candara" Font-Size="Small"
                    ForeColor="#FF3300"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style6">
                <asp:Button ID="btnAdd" runat="server" Text="Save" BackColor="#1BA691"
                    ForeColor="White" Width="120px" Height="20px" BorderStyle="None" 
                    Font-Names="Verdana" Font-Size="11px" />
            </td>
            <td class="style7">
                <asp:Button ID="btnCancel" runat="server" Text="Close" BackColor="#FF3300" ForeColor="White"
                    Width="120px" Height="20px" BorderStyle="None" Font-Names="Verdana" 
                    Font-Size="11px" />
            </td>
        </tr>
    </table>--%>
    </form>

   
</body>
</html>
</asp:Content>