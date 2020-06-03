<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="CustomNamingUpdate.aspx.vb"
    Inherits="GOSHRM.CustomNamingUpdate" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title></title>
        <body>
            <form id="form1">
            <div class="container">
                <div class="row">
                    <div class=" col-md-10">
                        <div id="divalert" runat="server" visible="false" class="alert alert-info">
                            <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <strong
                        id="msgalert" runat="server">Danger!</strong>
                            </div>
                </div>
                <div class="row">
                    <div class=" col-md-10">
                        <div class="panel panel-success">
                          <div class="panel-heading">
                                             <h5><b id="pagetitle" runat="server">PERFORMANCE CUSTOM NAMING</b></h5>
                                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-md-8 col-md-offset-0">                                       
                                        <asp:TextBox ID="txtid" runat="server" Width="4px" style="font-size: medium; font-family: Candara"
                                            Font-Names="Candara" Height="2px" Visible="False">
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class=" col-md-6">
                                        <div class="form-group">
                                            <label>
                                                KPI Category*</label>
                                                <input id="kpicategory" runat="server" class="form-control" />
                                        </div>
                                    </div>
                                    <div class=" col-md-6">
                                        <div class="form-group">
                                            <label>
                                                Key Performance Indicator*</label>
                                            <input id="keyperformanceindicator" runat="server" class="form-control" />
                                        </div>
                                    </div>
                                     <div class=" col-md-6">
                                        <div class="form-group">
                                            <label>
                                                KPI*</label>
                                            <input id="kpi" runat="server" class="form-control" />
                                        </div>
                                    </div>
                                    <div class=" col-md-6">
                                        <div class="form-group">
                                            <label>
                                                KPI To JobGrade*</label>
                                            <input id="kpitojobgrade" runat="server" class="form-control" />
                                        </div>
                                    </div>
                                    <div class=" col-md-6">
                                        <div class="form-group">
                                            <label>
                                                KPI Type*</label>
                                            <input id="kpitype" runat="server" class="form-control" />
                                        </div>
                                    </div>
                                     <div class=" col-md-6">
                                        <div class="form-group">
                                            <label>
                                                Objectives*</label>
                                            <input id="objectives" runat="server" class="form-control" />
                                        </div>
                                    </div>
                                     <div class=" col-md-6">
                                        <div class="form-group">
                                            <label>
                                                Success Target*</label>
                                            <input id="successtarget" runat="server" class="form-control" />
                                        </div>
                                    </div>
                                     <div class=" col-md-6">
                                        <div class="form-group">
                                            <label>
                                                Success Measure*</label>
                                            <input id="successmeasure" runat="server" class="form-control" />
                                        </div>
                                    </div>
                                     <div class=" col-md-6">
                                        <div class="form-group">
                                            <label>
                                                Key Action*</label>
                                            <input id="keyaction" runat="server" class="form-control" />
                                        </div>
                                    </div>
                                    <div class=" col-md-6">
                                        <div class="form-group">
                                            <label>
                                                Target Date*</label>
                                            <input id="targetdate" runat="server" class="form-control" />
                                        </div>
                                    </div>
                                    <div class=" col-md-6">
                                        <div class="form-group">
                                            <label>
                                                Performance Objective*</label>
                                            <input id="performanceobjective" runat="server" class="form-control" />
                                        </div>
                                    </div>
                                     <div class=" col-md-6">
                                        <div class="form-group">
                                            <label>
                                                Appraisal Feedback*</label>
                                            <input id="appraisalfbk" runat="server" class="form-control" />
                                        </div>
                                    </div>
                                    <div class=" col-md-6">
                                        <div class="form-group">
                                            <label>
                                                Appraisal Feedback Nugget*</label>
                                            <input id="appraisalfbkNugget" runat="server" class="form-control" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12 m-t-20 text-center">
                                        <button id="btnupdate" runat="server" onserverclick="btnAdd_Click" type="submit"
                                            style="width: 150px" class="btn btn-primary btn-success">
                                            Save &amp; Update</button>
                                        <button id="Button1" runat="server" onserverclick="btnCancel_Click" type="submit"
                                            style="width: 150px; display:none;" class="btn btn-primary btn-danger">
                                            << Back</button>
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
