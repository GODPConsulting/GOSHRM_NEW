<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="false" CodeBehind="LoanRulesUpdate.aspx.vb"
    Inherits="GOSHRM.LoanRulesUpdate" EnableEventValidation="false" %>

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
    </script>
</head>

<body >
    <form id="form1" action="">
    <div class="content container-fluid col-md-10">
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
                    <h5><b id="pagetitle" runat="server">Loan Rules</b></h5>
                </div>
             <div class="panel-body">
        <div class="row">            
            <div class=" col-md-6">
                <div class="form-group">
                    <label>
                        RULE NAME *</label>
                    <input id="arulename" runat="server" class="form-control" type="text" placeholder="Loan rule name" />
                </div>
            </div>
            <div class=" col-md-6">
                <div class="form-group">
                    <label>
                        LOAN TYPE *</label>
                        <telerik:RadComboBox ID="cboLoanType" runat="server"
                                    ResolvedRenderMode="Classic" Filter="Contains" 
                            RenderMode="Lightweight" Width="100%" ForeColor="#666666" Skin="Bootstrap">
                                </telerik:RadComboBox>
                </div>
            </div>
        </div>
        <div class="row">
            <div class=" col-md-12">
                <div class="form-group">
                    <label>
                        JOB GRADE *</label>
                    <telerik:radcombobox id="cboJobGrade" runat="server"
                        resolvedrendermode="Classic" autopostback="True" checkboxes="True" filter="Contains"
                        rendermode="Lightweight" width="100%" forecolor="#666666" Skin="Bootstrap">
                    </telerik:radcombobox>
                    <asp:UpdatePanel runat="server" ID="UpdatePanel" UpdateMode="Always">
                        <ContentTemplate>
                            <telerik:radlistbox id="lstMakeup" runat="server" font-names="Verdana" forecolor="#666666"
                                font-size="12px" width="100%" rendermode="Lightweight" Skin="Bootstrap">
                            </telerik:radlistbox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cboJobGrade" EventName="ItemChecked" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div class="row">
            <div class=" col-md-6">
                <div class="form-group">
                    <label>
                        EMPLOYMENT STATUS *</label>
                        <telerik:RadComboBox ID="cbojobstatus" runat="server"
                                    ResolvedRenderMode="Classic" Filter="Contains" 
                            RenderMode="Lightweight" Width="100%" ForeColor="#666666" Skin="Bootstrap">
                                </telerik:RadComboBox>
                </div>
            </div>
            <div class=" col-md-6">
                <div class="form-group">
                    <label>
                        APPLIES TO CONFIRMED STAFF ONLY *</label>
                        <telerik:RadComboBox ID="cboconfirmedstaff" runat="server"
                                    ResolvedRenderMode="Classic" 
                            RenderMode="Lightweight" Width="100%" ForeColor="#666666" Skin="Bootstrap">
                                </telerik:RadComboBox>
                </div>
            </div>
            </div>
            <div class="row">
            <div class=" col-md-6">
                <div class="form-group">
                    <label>
                        RULE TYPE *</label>
                        <telerik:RadComboBox ID="cboruletype" runat="server"
                                    ResolvedRenderMode="Classic" 
                            RenderMode="Lightweight" Width="100%" ForeColor="#666666" 
                        Skin="Bootstrap" AutoPostBack="True">
                                </telerik:RadComboBox>
                        <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Always">
                        <ContentTemplate>
                            <label id="lbruletype" runat="server" style="font-size:13px" >On Net Pay</label>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cboruletype" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                        
                </div>
            </div>
            <div class=" col-md-6">
                <div class="form-group">
                    <label>
                        APPLY GRATUITY *</label>
                        <telerik:RadComboBox ID="cboapplygratuity" runat="server"
                                    ResolvedRenderMode="Classic"  
                            RenderMode="Lightweight" Width="100%" ForeColor="#666666" Skin="Bootstrap">
                                </telerik:RadComboBox>
                </div>
            </div>
            </div>
        <div class="row">
            <div class=" col-md-6">
                <div class="form-group">
                    <label>
                        MAXIMUM LOAN AMOUNT *</label>
                    <input id="aloanamount" runat="server" class="form-control" type="text" placeholder="Maximum loan eligible to" />
                </div>
            </div>
            <div class=" col-md-6">
                <div class="form-group">
                    <label>
                        MAXIMUM TENOR (MONTH) *</label>
                    <input id="aloantenor" runat="server" class="form-control" type="text" placeholder="Maximum tenor" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class=" col-md-6">
                <div class="form-group">
                    <label>
                        ANNUAL INTEREST RATE (%) *</label>
                    <input id="aloanintrate" runat="server" class="form-control" type="text" placeholder="Interest rate" />
                </div>
            </div>
            <div class=" col-md-6">
                <div class="form-group">
                    <label>
                        MARKET RATE (%) *</label>
                    <input id="aloanmarketrate" runat="server" class="form-control" type="text" placeholder="Market rate" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class=" col-md-6">
                <div class="form-group">
                    <label>
                        MINIMUM ELIGIBLE SERVICE MONTH *</label>
                    <input id="aminservicemth" runat="server" class="form-control" type="text" placeholder="Minimum eligible month of service" />
                </div>
            </div>
            <div class=" col-md-6">
                <div class="form-group">
                    <label>
                        MAXIMUM ELIGIBLE SERVICE MONTH *</label>
                    <input id="amaxservicemth" runat="server" class="form-control" type="text" placeholder="Maximum eligible month of service" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class=" col-md-6">
                <div class="form-group">
                    <label>
                        MAXIMUM REPAYMENT FACTOR</label>
                    <input id="arepayfactor" runat="server" class="form-control" type="text" placeholder="Maximum repayment factor" />
                </div>
            </div>
        </div>
        
        <div class="row">
            <div class="col-md-12 text-right">
                <div class="form-group">
                    <label style="font-size:12px" ><i id="createdon" runat="server"></i></label>                    
                </div>
                <div class="form-group">
                    <label style="font-size:12px"><i id="updatedon" runat="server"></i></label>                    
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 m-t-20 text-center">
                <button id="btupdate" runat="server" onserverclick="btnAdd_Click" type="submit"
                    style="width: 150px" class="btn btn-primary btn-success">
                    Save &amp; Update</button>
                <button id="btcancel" runat="server" onserverclick="btnCancel_Click" type="submit"
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