<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/smartHR.Master" CodeBehind="AppFeedbackComment.aspx.vb" Inherits="GOSHRM.AppFeedbackComment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
    <form id="form1">
 
    <div class="container col-md-8">
            <div class="row">
            <div id="divalert" runat="server" visible="false" class="alert alert-info">
                <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                <strong id="msgalert" runat="server">Danger!</strong>
                <asp:TextBox ID="txtid" runat="server" Width="3px"  
                    Font-Names="Candara" Height="2px" Visible="False"></asp:TextBox>
            </div>
                   <div class="panel panel-success">
                        <div class="panel-heading">
                            <h5><b id="pagetitle" runat="server">Performance Feedback Comments</b></h5>
                        </div>
                     <div class="panel-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>Decision (I Accept Feedback)*</label>
                               <telerik:radcombobox ID="cboDecisions" runat="server" Filter="Contains" EnableCheckAllItemsCheckBox="False"
                                    RenderMode="Lightweight" Width="100%" ForeColor="#666666" Skin="Bootstrap" EmptyMessage="--Select--">
                                            <items>
                                            <telerik:RadComboBoxItem runat="server" Text="No" Value="No" />
                                            <telerik:RadComboBoxItem runat="server" Text="Yes" Value="Yes" />
                                        </items>
                                </telerik:radcombobox>
                            </div>
                        </div>
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>Comments*</label>
                                <textarea id="comments" runat="server" class="form-control" rows="5"></textarea>
                            </div>
                        </div>
                       
                        <div class="col-md-12 m-t-20 text-center">
                            <button id="btnupdate" runat="server"  type="submit"
                                style="width: 150px" onserverclick="btnAdd_Click" class="btn btn-primary btn-success">
                                Save &amp; Update</button>
                            <button id="Button1" runat="server" onserverclick="btnCancel_Click"  type="submit" style="width: 150px"
                                class="btn btn-primary btn-danger">
                                << Back</button>
                        </div>
                    </div>
                </div>
            </div>
             </div>
            </div>
    </form>
</asp:Content>
