<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="AppraisalGradeUpdate.aspx.vb"
    Inherits="GOSHRM.AppraisalGradeUpdate" EnableEventValidation="false" Debug="true" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<%--<meta name="viewport" content="width=device-width, initial-scale=1">
<link rel="stylesheet" href="http://www.w3schools.com/lib/w3.css">--%>
<head>
    <title></title>
    <script type="text/javascript">

        function closeWin() {
            popup.close();   // Closes the new window
        }
   

    </script>
</head>
<body>
    <form>
    <script type="text/javascript">
        function closeme() {
            window.close();
        }
        window.onblur = closeme;
    </script>
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
        rel="Stylesheet" type="text/css" />
         <div class="container col-md-12">
        <div>
            <div class="row">
                <div id="divalert" runat="server" visible="false" class="alert alert-info">
                    <strong id="msgalert" runat="server"></strong>
                    <asp:TextBox ID="txtid" runat="server" Width="3px"  
                    Font-Names="Candara" Height="2px" Visible="False"></asp:TextBox>
                </div>
                </div>
                <div class="panel panel-success">
                <div class="panel-heading">
                    <h5><b id="pagetitle" runat="server">Points Definition</b></h5>
                </div>
             <div class="panel-body">
                    <div class="row">
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>Name*</label>
                                <input id="txtName" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>Minimum (%)*</label>
                                <input id="txtMin" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>Maximum (%)*</label>
                                <input id="txtMax" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>Description</label>
                                <textarea id="txtDesc" runat="server" class="form-control" rows="5"></textarea>
                            </div>
                        </div>
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>
                                    Created By</label>
                                <input id="lblcreatedby" runat="server" class="form-control" readonly="" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>
                                    Created On</label>
                                <input id="lblcreatedon" runat="server" class="form-control" readonly="" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>
                                    Updated By</label>
                                <input id="lblupdatedby" runat="server" class="form-control" readonly="" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>
                                    Update On</label>
                                <input id="lblupdatedon" runat="server" class="form-control" readonly="" type="text" />
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
    </form>
</body>
</html>
</asp:Content>