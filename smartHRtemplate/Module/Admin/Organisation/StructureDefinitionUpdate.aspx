
<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="StructureDefinitionUpdate.aspx.vb"
    Inherits="GOSHRM.StructureDefinitionUpdate" EnableEventValidation="false" Debug="true" %>


<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
 
  
</head>

<body >
    <form id="form1" action-"">
    <div class="container">
        <div>
            <div class="row">
                <div id="divalert" runat="server" visible="false" class="alert alert-info">
                    <strong id="msgalert" runat="server"></strong>
                    <asp:TextBox ID="txtid" runat="server" Width="5px" Font-Size="1px" 
                     Visible="False"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-10 col-md-offset-0">
                 <div class="row">
                <div class=" col-md-10">
                    <div class="panel panel-success">
                        <div class="panel-body">
                        <div class="row">
                        <div class=" col-md-12">
                    <h5 class="page-title" style="color:#1BA691">Organisation Structure Definition
                        </h5>
                   </div>
                   </div>
                    <div class="row">
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>
                                    Name*</label>
                                <input id="structname" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                         <div class=" col-md-12">
                            <div class="form-group">
                                <label>Description*</label>
                                <textarea id="structdesc" runat="server" class="form-control" rows="5"></textarea>
                            </div>
                        </div>
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>
                                    Level*</label>
                                <input id="structlevel" runat="server" class="form-control" type="text" />
                            </div>
                        </div>

                        <div class="col-md-12 m-t-20 text-center">
                            <button id="btnupdate" runat="server" onserverclick="btnAdd_Click" type="submit"
                                style="width: 150px" class="btn btn-primary btn-success">
                                Save &amp; Update</button>
                            <button id="Button1" runat="server" onserverclick="btnCancel_Click" type="submit"
                                style="width: 150px" class="btn btn-primary btn-danger">
                                << Back</button>
                        </div>
                    </div>
                    
                </div>
            </div>
        </div>
    </div>
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