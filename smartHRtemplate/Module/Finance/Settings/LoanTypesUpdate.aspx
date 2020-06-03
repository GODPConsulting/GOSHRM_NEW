
<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="false" CodeBehind="LoanTypesUpdate.aspx.vb" Inherits="GOSHRM.LoanTypesUpdate" EnableEventValidation="false" %>


<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagPrefix="asp" %>

<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<html xmlns="http://www.w3.org/1999/xhtml">
<head >
    <title></title>
    <script type="text/javascript">

    function closeWin() {
        popup.close();   // Closes the new window
    }
   

    </script>
  
</head>

<body>
    <form id="form1" >

    <div class="content container-fluid col-md-8">
        <div class="row">
            <div class="col-md-12">
                <div id="divalert" runat="server" visible="false" class="alert alert-info">
                    <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <strong
                        id="msgalert" runat="server">Danger!</strong>
                    <asp:TextBox ID="txtid" runat="server" Font-Names="Verdana" Font-Size="1px" Visible="False"></asp:TextBox>
                </div>
            </div>
        </div>
         <div class="panel panel-success">
                <div class="panel-heading">
                    <h5><b id="pagetitle" runat="server">Loan Type</b></h5>
                </div>
             <div class="panel-body">
        <div class="row">
            <div class=" col-md-12">
                <div class="form-group">
                    <label>
                        NAME *</label>
                    <input id="aname" runat="server" class="form-control" type="text" placeholder="Year" />
                </div>
            </div>
            <div class=" col-md-12">
                <div class="form-group">
                    <label>
                        DESCRIPTION</label>
                    <textarea id="adesc" runat="server" class="form-control" rows="4" cols="1"></textarea>
                </div>
            </div>
            
        </div>
        <div class="row">
            <div class="col-md-8 m-t-20">
                <button id="btnsave" runat="server" onserverclick="btnAdd_Click" type="submit"
                    style="width: 150px" class="btn btn-primary btn-success">
                    Save &amp; Update</button>
                <button id="btnback" runat="server" onserverclick="btnCancel_Click" type="submit"
                    style="width: 150px" class="btn btn-primary btn-danger">
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