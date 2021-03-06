﻿<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="LeaveAllowanceGrade.aspx.vb"
    Inherits="GOSHRM.LeaveAllowanceGrade" EnableEventValidation="false" Debug="true" %>
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
                <div class="container col-md-6">
            <div id="divalert" runat="server" visible="false" class="alert alert-info">
                <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                <strong id="msgalert" runat="server">Danger!</strong>
                <asp:TextBox ID="txtid" runat="server" Width="13px" Style="font-size: medium; font-family: Candara"
                    Font-Names="Candara" Height="10px" Visible="False"></asp:TextBox>
            </div>
                   <div class="panel panel-success">
                        <div class="panel-heading">
                            <h5><b id="pagetitle" runat="server">LEAVE ALLOWANCE BY EMPLOYEE</b></h5>
                        </div>
                     <div class="panel-body">
                    <div class="row">
                        <div class="">
                            <div class="form-group">
                                <label>JOBGRADE*</label>
                                <telerik:RadDropDownList ID="radJobGrade" Skin="Bootstrap" runat="server" Width="100%" Font-Names="Verdana" Font-Size="12px" ForeColor="#666666"
                                    DefaultMessage="--Select--">
                                </telerik:RadDropDownList>
                            </div>
                        </div> 
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>ALLOWANCE(%)</label>
                               <input id="txtContribution" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class="col-md-12 m-t-20 text-center">
                            <button id="btnupdate" runat="server" onserverclick="btnAdd_Click" type="submit"
                                style="width: 150px" class="btn btn-primary btn-success">
                                Save &amp; Update</button>
                            <button id="Button1" runat="server" onserverclick="btnCancel_Click" type="submit" style="width: 150px"
                                class="btn btn-primary btn-info">
                                << Back</button>
                        </div>
                    </div>
                </div>
             </div>
            </div>
    </form>

   
</body>
</html>
</asp:Content>