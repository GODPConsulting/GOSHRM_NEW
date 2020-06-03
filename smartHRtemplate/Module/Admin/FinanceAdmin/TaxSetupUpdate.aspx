<%--<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="TaxSetupUpdate.aspx.vb"
    Inherits="GOSHRM.TaxSetupUpdate" EnableEventValidation="false" %>--%>
<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="TaxSetupUpdate.aspx.vb"
    Inherits="GOSHRM.TaxSetupUpdate" EnableEventValidation="false" Debug="true" %>


<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1">
    <title></title>
    <script type="text/javascript">

        function closeWin() {
            popup.close();   // Closes the new window
        }


        function btnupdate_onclick() {

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
        .style5
        {
            font-family: Candara;
            font-size: medium;
            width: 117px;
        }
        .style6
        {
            width: 117px;
        }
        .style7
        {
            font-family: Candara;
            font-size: x-small;
            width: 117px;
            color: #FF0000;
        }
        </style>
</head>
<body>
    <form id="form1" action="">
    
    <div class="container col-md-10">
            <div class="row">
                <div id="divalert" runat="server" visible="false" class="alert alert-info">
                    <strong id="msgalert" runat="server"></strong>
                </div>
                </div>
                  <div class="panel panel-success">
                <div class="panel-heading">
                    <h5><b id="pagetitle" runat="server">Tax Rate</b></h5>
                </div>
             <div class="panel-body">          
                    <div class="row">                      
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>Range *</label>
                                <telerik:RadDropDownList ID="radTaxRange" runat="server" DefaultMessage="--Select--"
                                    Font-Names="Verdana" Font-Size="12px" ForeColor="#666666" Width="100%" RenderMode="Lightweight"
                                    Skin="Bootstrap">
                                </telerik:RadDropDownList>
                            </div>
                        </div>                       
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>Upper Value *</label>
                                <input id="uppervalue" runat="server" class="form-control" type="text" />
                            </div>
                        </div>  
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>Tax Rate (%) *</label>
                                <input id="rate" runat="server" class="form-control" type="text" />
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
                    <div class="row">
                             <asp:TextBox ID="txtid" runat="server" Width="5px" 
                    Font-Names="Candara" Height="20px" Visible="False" Font-Size="5px">0</asp:TextBox>
                <asp:TextBox ID="txttaxid" runat="server" Width="5px" 
                    Font-Names="Candara" Height="20px" Visible="False" Font-Size="5px">0</asp:TextBox>
                        </div>
                        </div>
            </div>
    </div>

    </form>
</body>
</html>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
</asp:Content>