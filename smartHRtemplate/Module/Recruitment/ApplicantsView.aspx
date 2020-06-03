<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="ApplicantsView.aspx.vb"
    Inherits="GOSHRM.ApplicantsView" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title></title>
    </head>
    <body>
        <form id="form1" action="">
        <div class="container col-md-10">
                    <div class="row">
                        <div id="divalert" runat="server" visible="false" class="alert alert-info">
                            <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <strong
                                id="msgalert" runat="server">Danger!</strong>
                        </div>
                    </div>
                     <div class="panel panel-success">
                        <div class="panel-heading">
                            <h5 id="pagetitle" runat="server" class="page-title" style="color: #1BA691">
                                Head</h5>
                                <asp:Label ID="lblID" runat="server" Visible="False" Font-Size="1px"></asp:Label>
                                <asp:Label ID="lblrecruited" runat="server" Visible="False" Font-Size="1px"></asp:Label>
                                <asp:Label ID="lblofferpath" runat="server" Visible="False" Font-Size="1px"></asp:Label>
                                <asp:Label ID="lblResume" runat="server" Font-Size="1px" ></asp:Label>
                                <asp:Label ID="lblCoverLetter" runat="server" Font-Size="1px"></asp:Label>
                                <asp:Label ID="lblCertificate" runat="server" Font-Size="1px"></asp:Label>
                                   <asp:Label ID="labeloffer" runat="server" Font-Size="1px"></asp:Label>
                        </div>
                     <div class="panel-body">
                    <div class="row">
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>
                                    APPLICANT</label>
                                <input id="aapplicant" runat="server" class="form-control" type="text" disabled="disabled" />
                            </div>
                        </div>                        
                    </div>
                    <div class="row">
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>
                                    EMAIL ADDRESS</label>
                                <input id="aemailaddr" runat="server" class="form-control" type="text" disabled="disabled" />
                            </div>
                        </div>                        
                    </div>
                    <div class="row">
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>
                                    GENDER</label>
                                <input id="agender" runat="server" class="form-control" type="text" disabled="disabled" />
                            </div>
                        </div>                        
                    </div>
                    <div class="row">
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>
                                    DATE OF BIRTH</label>
                                <input id="adob" runat="server" class="form-control" type="text" disabled="disabled" />
                            </div>
                        </div>                        
                    </div>
                    <div class="row">
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>
                                    CONTACT NUMBER</label>
                                <input id="aphonenumber" runat="server" class="form-control" type="text" disabled="disabled" />
                            </div>
                        </div>                        
                    </div>
                    <div class="row">
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>
                                    MARITAL STATUS</label>
                                <input id="amaritalstat" runat="server" class="form-control" type="text" disabled="disabled" />
                            </div>
                        </div>                        
                    </div>
                    <div class="row">
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>
                                    NATIONALITY</label>
                                <input id="anationality" runat="server" class="form-control" type="text" disabled="disabled" />
                            </div>
                        </div>                        
                    </div>
                     <div class="row">
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>
                                    RESIDENTIAL ADDRESS</label>
                                <textarea id="aaddress" runat="server" class="form-control" rows="5" cols="1" readonly="readonly"></textarea>
                            </div>
                        </div>                        
                    </div>
                    <div class="row">
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>
                                    SPECIALISATION</label>
                                <input id="aspecialisation" runat="server" class="form-control" type="text" disabled="disabled" />
                            </div>
                        </div>                        
                    </div>
                    <div class="row">
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>
                                    DISCIPLINE</label>
                                <input id="adiscipline" runat="server" class="form-control" type="text" disabled="disabled" />
                            </div>
                        </div>                        
                    </div>
                    <div class="row">
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>
                                    YEARS OF EXPERIENCE</label>
                                <input id="aexpyear" runat="server" class="form-control" type="text" disabled="disabled" />
                            </div>
                        </div>                        
                    </div>
                    <div class="row">
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>
                                    EDUCATIONAL LEVEL</label>
                                <input id="aeducation" runat="server" class="form-control" type="text" disabled="disabled" />
                            </div>
                        </div>                        
                    </div>
                    
                    <div class="row">
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>
                                    GRADE</label>
                                <input id="agrade" runat="server" class="form-control" type="text" disabled="disabled" />
                            </div>
                        </div>                        
                    </div>
                   
                    <div class="row">
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>
                                    SKILLS</label>
                                <textarea id="askill" runat="server" class="form-control" rows="5" cols="1" readonly="readonly"></textarea>
                            </div>
                        </div>                        
                    </div>
                    <div class="row col-md-12">
                        <div class="col-md-2 text-center ">
                            <button id="btncoverletter" runat="server" type="submit" onserverclick="lnkCoverLetter_Click"
                                style="width: 100px" class="btn btn-link">
                                <i class="fa fa-download"></i>COVER LETTER</button>
                        </div>
                         <div class="col-md-2 text-center ">
                            <button id="btnresume" runat="server" type="submit" onserverclick="lnkResume_Click"
                                style="width: 100px" class="btn btn-link">
                                <i class="fa fa-download"></i>RESUME</button>
                        </div>
                        <div class="col-md-2 text-center ">
                            <button id="btncertificate" runat="server" type="submit" onserverclick="lnkCert_Click"
                                style="width: 100px" class="btn btn-link">
                                <i class="fa fa-download"></i>CERTIFICATE</button>
                        </div>
                        <div class="col-md-2 text-center ">
                            <button id="btnofferletter" runat="server" type="submit" onserverclick="lnkofferletter_Click"
                                style="width: 100px" class="btn btn-link">
                                <i class="fa fa-download"></i>OFFER LETTER</button>
                        </div>
                        
                    </div>
                    <div class="row col-md-12">
                        <div class="col-md-8 m-t-20 text-center">
                            <button id="btnupdate" runat="server" onserverclick="btnSend_Click" type="submit"
                                style="width: 150px" class="btn btn-primary btn-success">
                                Shortlist Applicant</button>
                            <button id="btcancel" runat="server" onserverclick="btnclose_Click" type="submit"
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
