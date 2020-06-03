<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="InterviewHRDetail.aspx.vb"
    Inherits="GOSHRM.InterviewHRDetail" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title></title>
    </head>
    <body>
        <form id="form1" action="">
        <div class="container col-md-12">
        <div class="row">
            <div id="divalert" runat="server" visible="false" class="alert alert-info">
                <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <strong
                    id="msgalert" runat="server">Danger!</strong>
            </div>
        </div>
        <div class="panel panel-success">
                            <div class="panel-heading">
                                 <h5 id="pagetitle" runat="server" class="page-title" style="color: #1BA691">Head</h5>
                            </div>
                            
                            <div class="panel-body">

        <div class="row">
            <div class="col-md-8 pull-left">
                <p>
                    <a href="JobInterviews"><u>Interviews</u></a>
                    <label>
                        >
                    </label>
                    <a href="Interviewees"><u>Interview Shortlists</u></a>
                    <label>
                        >
                    </label>
                    <a href="#">Candidate</a>
                </p>
                <asp:Label ID="lblapplicant" runat="server" Font-Size="1px" Visible="False"></asp:Label>
                <asp:Label ID="lblrecruit" runat="server" Font-Size="1px" Visible="False"></asp:Label>
                <asp:Label ID="lblApplicantID" runat="server" Visible="False" Font-Size="1px"></asp:Label>
                <asp:Label ID="lbljobid" runat="server" Visible="False" Font-Size="1px"></asp:Label>
                <asp:Label ID="lblID" runat="server" Visible="False" Font-Size="1px"></asp:Label>
                <asp:TextBox ID="txthrrecomm" runat="server" Font-Size="1px" Visible="False"></asp:TextBox>
                <asp:Label ID="lblCertificate" runat="server" Font-Size="1px" Visible="False"></asp:Label>
                <asp:Label ID="lblresume" runat="server" Font-Size="1px" Visible="False"></asp:Label>
                <asp:Label ID="lblcoverletter" runat="server" Font-Size="1px" Visible="False"></asp:Label>
            </div>
            <div class="col-md-3 pull-right">
                                <button id="btnbiodata" type="button" runat="server" class="btn btn-success" onserverclick="lnkBioData_Click" title="Applicant's account and medical information"
                                    style="height: 30px; width: 150px">
                                    More Info</button>
                            </div>
            
        </div>
        <div class="row">
            <div class="">
                <div class="form-group">
                    <label>
                        NAME</label>
                    <input id="aname" runat="server" class="form-control" type="text" disabled="disabled" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <div class="form-group">
                    <label>
                        EMAIL ADDRESS</label>
                    <input id="aemailaddr" runat="server" class="form-control" type="text" disabled="disabled" />
                </div>
            </div>
             <div class="col-md-6">
                <div class="form-group">
                    <label>
                        SPECIALISATION</label>
                    <input id="aspecialisation" runat="server" class="form-control" type="text" disabled="disabled" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <div class="form-group">
                    <label>
                        GENDER</label>
                    <input id="agender" runat="server" class="form-control" type="text" disabled="disabled" />
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-group">
                    <label>
                        DATE OF BIRTH</label>
                    <input id="adob" runat="server" class="form-control" type="text" disabled="disabled" />
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-6">
                <div class="form-group">
                    <label>
                        EDUCATION</label>
                    <input id="aeducation" runat="server" class="form-control" type="text" disabled="disabled" />
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-group">
                    <label>
                        YEARS OF EXPERIENCE</label>
                    <input id="aexpyear" runat="server" class="form-control" type="text" disabled="disabled" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="">
                <div class="form-group">
                    <label>
                        RECOMMENDATION</label>
                         <telerik:RadDropDownList ID="radRecommendation" runat="server" Width="100%"  ForeColor="#666666" Skin="Bootstrap"
                    DefaultMessage="-- Select --">
                </telerik:RadDropDownList>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <div class="form-group">
                    <label>
                        MEDICAL COMMENT</label>
                    <textarea id="amedicalcoment" runat="server" class="form-control" rows="4" cols="1" placeholder="Comment on medical status"></textarea>
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-group">
                    <label>
                        COMMENT</label>
                    <textarea id="acomment" runat="server" class="form-control" rows="4" cols="1" placeholder="General comment"></textarea>
                </div>
            </div>
        </div>
        <div id="divrecruit" runat="server" class="row">
            <div class="col-md-6">
                <div class="form-group">
                    <label>
                        OFFER SENT</label>
                    <input id="aoffer" runat="server" class="form-control" type="text" disabled="disabled" />
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-group">
                    <label>
                        RECRUITED</label>
                    <input id="arecruited" runat="server" class="form-control" type="text" disabled="disabled" />
                </div>
            </div>
        </div>
        <div id="div1" runat="server" class="row">
            <div class="col-md-6">
                <div class="form-group">
                    <label>
                        MEDICALS REQUESTED</label>
                    <input id="amedicalreq" runat="server" class="form-control" type="text" disabled="disabled" />
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-group">
                    <label>
                        BANK DETAILS REQUESTED</label>
                    <input id="aacctreq" runat="server" class="form-control" type="text" disabled="disabled" />
                </div>
            </div>
        </div>
        <div class="">
            <div class="panel panel-success">
                <div class="panel-heading">
                    <b>ATTACHEMENTS</b>
                </div>
                <div class="panel-body text-left text-uppercase">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <button id="lnkccletter" type="button" runat="server" class="btn-link rounded" onserverclick="lnkCoverLetter_Click">
                                    <i class="fa fa-download"></i><b>Cover Letter</b></button>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <button id="lnkcv" type="button" runat="server" class="btn-link rounded" onserverclick="lnkResume_Click">
                                    <i class="fa fa-download"></i><b>Resume</b></button>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <button id="lnkcert" type="button" runat="server" class="btn-link rounded" onserverclick="lnkCert_Click">
                                    <i class="fa fa-download"></i><b>Certificate</b></button>
                            </div>
                        </div>
                        <div class=" col-md-3">
                            <div class="form-group">
                                <button id="lnkoffletter" type="button" runat="server" class="btn-link rounded" onserverclick="lnkofferletter_Click">
                                    <i class="fa fa-download"></i><b>Offer Letter</b></button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="divgrid" runat="server" class="row">
            <div class=" col-md-12">
            <div class="table-responsive">
                        <asp:GridView ID="GridVwHeaderChckbox" runat="server" 
                            BorderStyle="Solid" Font-Names="Verdana" AllowPaging="True" PageSize="10"
                            Width="100%" Height="50px" 
                            Font-Size="12px" ShowHeaderWhenEmpty="True" EmptyDataText="No data to display"
                            AutoGenerateColumns="False" GridLines="Both" ForeColor="#666666" BorderWidth="1px"
                            BorderColor="#CCCCCC" CssClass="table table-condensed">
                            <RowStyle BackColor="White" />
                            <Columns>                                
                                <asp:BoundField DataField="TestStage" HeaderText="Stage" />        
                                <asp:BoundField DataField="TestTitle" HeaderText="Test" /> 
                                <asp:BoundField DataField="questions" HeaderText="Correct" ItemStyle-HorizontalAlign="Right"/>  
                                <asp:BoundField DataField="score" HeaderText="Score" ItemStyle-HorizontalAlign="Right"/> 
                                <asp:BoundField DataField="passmark" HeaderText="Pass Score" ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="DateTaken" HeaderText="Date" />                                     
                            </Columns>
                            <HeaderStyle BackColor="White" ForeColor="#1BA691" HorizontalAlign="center" />
                        </asp:GridView>
       
                <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
                <script type="text/javascript">
                    $(function () {
                        $("[id*=GridVwHeaderChckbox] td").hover(function () {
                            $("td", $(this).closest("tr")).addClass("hover_row");
                        }, function () {
                            $("td", $(this).closest("tr")).removeClass("hover_row");
                        })
                    })
                </script>
            </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 m-t-20 text-center">
                <button id="btupdate" runat="server" onserverclick="btnAdd_Click" type="submit"
                    class="btn btn-success">
                    Save &amp; Update</button>
                <button id="btoff" runat="server" onserverclick="btnJobOffer_Click" type="submit"
                    class="btn btn-info" title="generate offer letter" >
                    Offer Letter</button>
                <button id="btacctreq" runat="server" onserverclick="btnAccountInfo_Click" type="submit"
                    class="btn btn-info" title="request bank detail" >
                    Bank Account Request</button>
                <button id="btmedreq" runat="server" onserverclick="btnMedicalReq_Click" type="submit"
                    class="btn btn-info" title="request medical evaluation" >
                    Medical Request</button>
                <button id="btrecruit" runat="server" onserverclick="btnRecruit_Click" type="submit"
                    class="btn btn-info" title="initiate recruitment process" style="width:150px" >
                    Hire</button>
            </div>
        </div>
        </div>
        </div> </div>
        </form>
    </body>
    </html>
</asp:Content>
