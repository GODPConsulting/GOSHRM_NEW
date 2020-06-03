<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="AppraisalPreference.aspx.vb"
    Inherits="GOSHRM.AppraisalPreference" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <title></title>
    <style type="text/css">
        body
        {
            font-family: Arial;
            font-size: 10pt;
        }
        td
        {
            cursor: pointer;
        }
        .hover_row
        {
            background-color: #A1DCF2;
        }
        
        
        
        .style27
        {
            height: 12px;
            width: 204px;
        }
        .style31
        {
            width: 377px;
            font-size: small;
        }
        .style34
        {
            color: #FFFFFF;
        }
        .style35
        {
        }
        .style37
        {
            width: 377px;
        }
    
        </style>



    <body style="">
        <form id="form1">
        <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
        <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
        <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
            rel="Stylesheet" type="text/css" />
         <div class="container col-md-10">
            <div class="row">
                <div id="divalert" runat="server" visible="false" class="alert alert-info">
                    <strong id="msgalert" runat="server"></strong>
                    <asp:TextBox ID="txtid" runat="server" Width="3px"  
                    Font-Names="Candara" Height="2px" Visible="False"></asp:TextBox>
                </div>
                </div>
            <div class="panel panel-success">
                    <div class="panel-heading">
                        <h5><b id="pagetitle" runat="server"></b></h5>
                    </div>
                 <div class="panel-body">
                    <div class="row">
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>Company:*</label>
                               <telerik:RadComboBox ID="cboCompany" Skin="Bootstrap" Runat="server" ForeColor="#666666"
                                        EmptyMessage="--Select Appraisal Cycle-- " Width="100%" 
                                        Font-Names="Verdana" Font-Size="12px" AutoPostBack="True">
                                    </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>Current Appraisal Cycle:*</label>
                                <telerik:RadComboBox ID="cboPeriod" Skin="Bootstrap" Runat="server" ForeColor="#666666"
                                        EmptyMessage="--Select Appraisal Cycle-- " Width="100%" 
                                        Font-Names="Verdana" Font-Size="12px">
                                    </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>Reviewer I Comment Visible to Reviewee:*</label>
                                <telerik:RadComboBox ID="cboReviewerVisibility" Skin="Bootstrap" Runat="server" ForeColor="#666666"
                                        EmptyMessage="--Select Appraisal Cycle-- " Width="100%" 
                                        Font-Names="Verdana" Font-Size="12px">
                                    </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>
                                    Reviewer II Comment Visible to Reviewee:*</label>
                                <telerik:RadComboBox ID="cboReviewIIVisibility" Skin="Bootstrap" Runat="server" ForeColor="#666666"
                                        EmptyMessage="--Select Appraisal Cycle-- " Width="100%" 
                                        Font-Names="Verdana" Font-Size="12px">
                                    </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>Status of Current Appraisal Cycle:*</label>
                                <telerik:RadComboBox ID="cboStat" Skin="Bootstrap" Runat="server" ForeColor="#666666"
                                        EmptyMessage="--Select Appraisal Cycle-- " Width="100%" 
                                        Font-Names="Verdana" Font-Size="12px">
                                    </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>
                                    Employee can view Coach's Performance Objectives:*</label>
                                <telerik:RadComboBox ID="cboViewCoach" Skin="Bootstrap" Runat="server" ForeColor="#666666"
                                        EmptyMessage="--Select Appraisal Cycle-- " Width="100%" 
                                        Font-Names="Verdana" Font-Size="12px">
                                    </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class="col-md-12 m-t-20 text-center">
                            <button id="btnupdate" runat="server" onserverclick="btnAdd_Click" type="submit"
                                style="width: 150px" class="btn btn-primary btn-success">
                                Save &amp; Update</button>
                        </div>
                    </div>
        </div></div></div>
        </form>
    </body>
    </html>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">

  

</asp:Content>
