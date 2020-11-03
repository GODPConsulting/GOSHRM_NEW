<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="KPIUpdate.aspx.vb"
    Inherits="GOSHRM.KPIUpdate" EnableEventValidation="false" Debug="true" %>

<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<%--<meta name="viewport" content="width=device-width, initial-scale=1">
<link rel="stylesheet" href="http://www.w3schools.com/lib/w3.css">--%>
<head>
    <title>Add New</title>
    <script type="text/javascript">

        function closeWin() {
            popup.close();   // Closes the new window
        }
   

    </script>
    <style type="text/css">
        .style1
        {
            color: #FFFFFF;
            font-family: Candara;
            font-weight: bold;
        }
        .lbl
        {
            font-family: Candara;
            font-size: medium;
        }
        .style2
        {
            font-family: Candara;
            font-size: small;
            width: 176px;
            color: #FF0000;
        }
        .style5
        {
            font-family: Candara;
            font-size: medium;
            width: 176px;
        }
        .style6
        {
            width: 176px;
        }
        .style7
        {
            color: #FF0000;
        }
        .style8
        {
            width: 522px;
        }
    </style>
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
                    <strong id="msgalert" runat="server"></strong>
                    <asp:TextBox ID="txtid" runat="server" Width="3px"  
                    Font-Names="Candara" Height="2px" Visible="False"></asp:TextBox>
                </div>
                </div>
                 <div class="panel panel-success">
                <div class="panel-heading">
                    <h5><b id="pagetitle" runat="server">Key Performance Indicator</b></h5>
                </div>
             <div class="panel-body">       
                    <div class="row">
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>KPI Category*</label>
                                <telerik:RadComboBox ID="cboKPIType" Skin="Bootstrap" runat="server" Width="100%"  ForeColor="#666666"
                                    Font-Names="Verdana" Font-Size="12px">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>KPI Name*</label>
                                <input id="txtname" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>Upload of actual Result</label>
                                  <telerik:RadDropDownList ID="RadDropDownList1" runat="server" DefaultMessage="-- Select --"
                                            Width="100%" ForeColor="#666666" Skin="Bootstrap"/>
                            </div>
                        </div>
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>Description </label>
                                <textarea id="txtDesc" runat="server" class="form-control" rows="5"></textarea>
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