<%--<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="License.aspx.vb" Inherits="GOSHRM.License"
    EnableEventValidation="false" %>--%>

<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="License.aspx.vb"
    Inherits="GOSHRM.License" EnableEventValidation="false" Debug="true" %>

<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title></title>
        <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
            rel="Stylesheet" type="text/css" />
    </head>
    <body>
        <form id="form1" action="">
        <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
        <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
        <div class="container col-md-10">
            <div class="row">
                <div class="">
                    <div id="divalert" runat="server" visible="false" class="alert alert-info">
                        <strong id="msgalert" runat="server">Danger!</strong>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="">
                    <div class="panel panel-success">
                     <div class="panel-heading">
                     <h4><b>Host</b></h4>
                     </div>
                        <div class="panel-body">
                            <div>
                                <div class="row">
                                    <div class="">
                                        
                                       
                                        <div class="row">
                                            <div class="">
                                                <div class="form-group">
                                                    <label>
                                                        Host IP/Name</label>
                                                    <input id="hostip" runat="server" class="form-control" type="text" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="">
                                                <div class="form-group">
                                                    <label>
                                                        License Key</label>
                                                    <input id="license" runat="server" class="form-control" type="text" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="">
                                                <div class="form-group">
                                                    <label>
                                                        Created On</label>
                                                    <input id="createdon" runat="server" class="form-control" type="text" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="">
                                                <div class="form-group">
                                                    <label>
                                                        Updated On</label>
                                                    <input id="updatedon" runat="server" class="form-control" type="text" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class=" col-sm-1 m-t-20 text-center">
                                                <button id="btnupdate" runat="server" type="submit" onserverclick="btnSave_Click"
                                                    class="btn btn-primary btn-success">
                                                    Save &amp; Update</button>
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
    <style type="text/css">
        .style24
        {
            font-family: Candara;
            text-transform: uppercase;
            border-left-color: #A0A0A0;
            border-right-color: #C0C0C0;
            border-top-color: #A0A0A0;
            border-bottom-color: #C0C0C0;
            padding: 1px;
            background-color: #FFFFFF;
            font-size: small;
            width: 228px;
        }
    </style>
</asp:Content>
