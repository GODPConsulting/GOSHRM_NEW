<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="false" CodeBehind="EmployeeLanguage.aspx.vb" Inherits="GOSHRM.EmployeeLanguage" EnableEventValidation="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<html xmlns="http://www.w3.org/1999/xhtml">
<head >
    <title></title>
    <script type="text/javascript">
        function closeWin() {
            popup.close();   // Closes the new window
        }
        function Button1_onclick() {

        }

    </script>


</head>

<body>
    <form id="form1" >
    <div class="container">
            <div class="row">
                <div class=" col-md-8">
                    <div id="divalert" runat="server" visible="false" class="alert alert-info">
                         <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <strong
                            id="msgalert" runat="server">Danger!</strong>
                            <asp:TextBox ID="txtEmpID" runat="server" Font-Size="1px" Visible="false"></asp:TextBox>
                            <asp:TextBox ID="txtid" runat="server"  Font-Size="1px" Visible="False"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div id="divemp" runat="server" class="col-sm-3 col-md-8 col-xs-6 pull-left">
                    <p>
                        <a id="A1" href="#" runat="server" onserverclick="btnCancel_Click"><u>Employee Profile</u></a>
                        <label>
                            >
                        </label>
                        <a id="A2" href="#">Language</a>
                    </p>
                </div>
            </div>
            <div class="card-box">
                <div class="row">
                    <div class="profile-basic">
                        <div class="row">
                            <div class=" col-md-8">
                                <h5 class="card-title" style="color: #1BA691">
                                    Language</h5>
                            </div>
                        </div>
                        <div class="row">
                            <div class=" col-md-8">
                                <div class="form-group">
                                    <label>
                                        NAME</label>
                                    <input id="aname" runat="server" class="form-control" type="text" disabled="disabled" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class=" col-md-8">
                                <div class="form-group">
                                    <label>
                                        LANGUAGE</label>
                                    <telerik:radcombobox id="cbolang" runat="server" width="100%" forecolor="#666666"
                                        rendermode="Lightweight" skin="Bootstrap" filter="Contains">
                                    </telerik:radcombobox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class=" col-md-8">
                                <div class="form-group">
                                    <label>
                                        READING</label>
                                    <telerik:radcombobox id="cboreading" runat="server" width="100%" forecolor="#666666"
                                        rendermode="Lightweight" skin="Bootstrap" filter="Contains">
                                    </telerik:radcombobox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class=" col-md-8">
                                <div class="form-group">
                                    <label>
                                        WRITING</label>
                                    <telerik:radcombobox id="cbowriting" runat="server" width="100%" forecolor="#666666"
                                        rendermode="Lightweight" skin="Bootstrap" filter="Contains">
                                    </telerik:radcombobox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class=" col-md-8">
                                <div class="form-group">
                                    <label>
                                        SPEAKING</label>
                                    <telerik:radcombobox id="cbospeak" runat="server" width="100%" forecolor="#666666"
                                        rendermode="Lightweight" skin="Bootstrap" filter="Contains">
                                    </telerik:radcombobox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 m-t-20">
                                <button id="btupdate" runat="server" onserverclick="btnAdd_Click" type="submit" class="btn btn-success rounded">
                                    Save</button>
                                <button id="Button1" runat="server" onserverclick="btnClose_Click" type="submit" class="btn btn-info rounded">
                                    << Back</button>
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