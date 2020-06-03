<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="false" CodeBehind="KPITypeUpdate.aspx.vb"
    Inherits="GOSHRM.KPITypeUpdate" EnableEventValidation="false" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<html xmlns="http://www.w3.org/1999/xhtml">
<%--<meta name="viewport" content="width=device-width, initial-scale=1">
<link rel="stylesheet" href="http://www.w3schools.com/lib/w3.css">--%>
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
    <div class="content container-fluid col-md-10">
        <div class="row">
            <div class="col-md-8">
                <div id="divalert" runat="server" visible="false" class="alert alert-info">
                    <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <strong
                        id="msgalert" runat="server">Danger!</strong>
                    <asp:TextBox ID="txtid" runat="server" Font-Names="Verdana" Font-Size="1px" Visible="False"></asp:TextBox>
                </div>
            </div>
        </div>
          <div class="panel panel-success">
                <div class="panel-heading">
                    <h5><b id="pagetitle" runat="server">KPI Category</b></h5>
                </div>
             <div class="panel-body">
        <div class="row">            
            <div class=" col-md-6">
                <div class="form-group">
                    <label>
                        NAME *</label>
                    <input id="aname" runat="server" class="form-control" type="text" placeholder="" />
                </div>
            </div>
            <div class=" col-md-6">
                <div class="form-group">
                    <label>
                        EMPLOYEE PERMITTED TO SET OBJECTIVES</label>
                       <telerik:RadComboBox runat="server"
                    RenderMode="Lightweight"  ForeColor="#666666"
                    ResolvedRenderMode="Classic" Width="100%" ID="cboEmpSetObj" 
                    AutoPostBack="True" Skin="Bootstrap">
                </telerik:RadComboBox>
                </div>
            </div>
        </div>
        <div class="row">
        <div class=" col-md-12">
                <div class="form-group">
                    <label>
                        DESCRIPTION</label>
                    <textarea id="adesc" runat="server" class="form-control" rows="4" cols="1"></textarea>
                </div>
            </div>
        </div>
        <div class="row">            
            <div class=" col-md-12">
                <div class="form-group">
                    <label>
                        WEIGHT MODEL *</label>
                         <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Always">
                        <ContentTemplate>
                              <telerik:RadComboBox runat="server"
                                  RenderMode="Lightweight"  ForeColor="#666666"
                                    ResolvedRenderMode="Classic" Width="100%" ID="cboWeightModel" 
                                  AutoPostBack="True" Skin="Bootstrap">
                                </telerik:RadComboBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cboEmpSetObj" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
            <asp:UpdatePanel runat="server" ID="UpdatePanel" UpdateMode="Always">
                <ContentTemplate>
                    <div id="div360review" runat="server" class=" col-md-4">
                        <div class="form-group">
                            <label>
                                REVIEWERS SELECTION ONLY BY HR UNIT</label>
                            <telerik:radcombobox runat="server" dropdownautowidth="Enabled" rendermode="Lightweight"
                                forecolor="#666666" resolvedrendermode="Classic" width="100%" id="cbobyHR" autopostback="True"
                                skin="Bootstrap">
                            </telerik:radcombobox>
                        </div>
                    </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cboWeightModel" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
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
   </div>
    </form>
</body>
</html>
</asp:Content>