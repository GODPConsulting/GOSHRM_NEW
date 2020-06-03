<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="false"
    CodeBehind="InterviewDetail.aspx.vb" Inherits="GOSHRM.InterviewDetail" EnableEventValidation="false" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title></title>
                <script type="text/javascript">
            function Complete() {
                var confirm_value = document.createElement("INPUT");
                confirm_value.type = "hidden";
                confirm_value.name = "confirm_value";
                if (confirm("Mark as complete and send notification to HR?")) {
                    confirm_value.value = "Yes";
                } else {
                    confirm_value.value = "No";
                }
                document.forms[0].appendChild(confirm_value);
            }         
        </script>
    </head>
    <body>
        <form id="form1">
        <div class="container col-md-12">
            <div class="row">
                <div class="">
                    <div id="divalert" runat="server" visible="false" class="alert alert-info">
                        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <strong
                            id="msgalert" runat="server">Danger!</strong>
                         <asp:Label ID="lblID" runat="server" Visible="False" Font-Size="1px"></asp:Label>
                         <asp:Label ID="lbljobid" runat="server" Visible="False" Font-Size="1px"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="panel panel-success">
                <div class="panel-heading">
                     <h5 class="page-title" style="color:#1BA691">Interview</h5>
                </div>
             <div class="panel-body">      
            <div class="row">
                <div class=" col-md-6">
                    <div class="form-group">
                        <label>
                            CANDIDATE</label>
                        <input id="acandidate" runat="server" class="form-control" type="text" disabled="disabled" />
                    </div>
                </div>
                <div class=" col-md-6">
                    <div class="form-group">
                        <label>
                            EMAIL ADDRESS</label>
                        <input id="aemailaddress" runat="server" class="form-control" type="text" disabled="disabled" />
                    </div>
                </div>
            </div>
            <div class="row">
                <div class=" col-md-6">
                    <div class="form-group">
                        <label>
                            GENDER
                        </label>
                        <input id="agender" runat="server" class="form-control" type="text" disabled="disabled" />
                    </div>
                </div>
                <div class=" col-md-6">
                    <div class="form-group">
                        <label>
                            DATE OF BIRTH
                        </label>
                        <input id="adob" runat="server" class="form-control" type="text" disabled="disabled" />
                    </div>
                </div>
            </div>
            <div class="row">
                <div class=" col-md-6">
                    <div class="form-group">
                        <label>
                            SPECIALISATION
                        </label>
                        <input id="aspecialisation" runat="server" class="form-control" type="text" disabled="disabled" />
                    </div>
                </div>
                <div class=" col-md-6">
                    <div class="form-group">
                        <label>
                            EDUCATION
                        </label>
                        <input id="aeducation" runat="server" class="form-control" type="text" disabled="disabled" />
                    </div>
                </div>
            </div>
            <div style="margin-top:20px; margin-bottom:20px;" class="row col-md-12 form-group">
                <div id="divcoverletter" runat="server" class="col-md-3 text-center ">
                    <button id="btncoverletter" runat="server" type="submit" onserverclick="lnkCoverLetter_Click"
                        style="width: 100%" class="btn btn-default">
                        <i class="fa fa-download"></i>COVER LETTER</button>
                </div>
                <div id="divresume" runat="server" class="col-md-3 text-center ">
                    <button id="btnresume" runat="server" type="submit" onserverclick="lnkResume_Click"
                        style="width: 100%" class="btn btn-default">
                        <i class="fa fa-download"></i>RESUME</button>
                </div>
                <div id="divcert" runat="server" class="col-md-3 text-center ">
                    <button id="btncertificate" runat="server" type="submit" onserverclick="lnkCert_Click"
                        style="width: 100%" class="btn btn-default">
                        <i class="fa fa-download"></i>CERTIFICATE</button>
                </div>
                <div id="divevaluateform" runat="server" class="col-md-3 text-center ">
                    <button id="btevaluationform" runat="server" type="submit" onserverclick="lnkEvaluation_Click"
                        style="width: 100%" class="btn btn-default" title="evaluation forms are to be completed by the interviewer to rank the candidates overall
qualifications for the position">
                        <i class="fa fa-download"></i>EVALUATION FORM</button>
                </div>
            </div>
            <div class="row">
                <div class=" col-md-6">
                    <div class="form-group">
                        <label>
                            RECOMMENDATION</label>
                        <telerik:radcombobox runat="server" forecolor="#666666"
                            rendermode="Lightweight" resolvedrendermode="Classic" width="100%" id="cborecommendation"
                            skin="Bootstrap">
                        </telerik:radcombobox>
                    </div>
                </div>
                <div id="divhr" runat="server" class=" col-md-6">
                    <div class="form-group">
                        <label>
                            HR DEPT. RECOMMENDATION</label>
                        <input style="height:35px;" id="ahrrecommendation" runat="server" class="form-control" type="text" disabled="disabled" />
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <label>
                            COMMENT</label>
                        <textarea id="acomment" runat="server" class="form-control" rows="4"></textarea>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <b>UPLOAD EVALUATION PAPER FORM</b>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="form-group">
                                    <label>
                                        EVALUATION FORM</label>
                                    <button id="lnkattach" type="button" runat="server" class="btn btn-link" onserverclick="downloadevaluation_Click">
                                        <i class="fa fa-upload"></i></button>
                                        <button id="Button1" data-toggle="tooltip" data-original-title="Upload" type="submit" runat="server" class="fa fa-upload btn btn-default" onserverclick="btnUpload_Click"></button>
                                    <input class="form-control" type="file" id="file1" runat="server" />                                    
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="divperformance" runat="server" class="row">
                <div class="col-md-12">
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <b>TEST PERFORMANCE</b>
                        </div>
                        <div class="panel-body">
                            <div class=" col-md-7">
                                <div class="table-responsive">
                                    <asp:GridView ID="GridVwHeaderChckbox" runat="server" BorderStyle="Solid" Font-Names="Verdana"
                                        AllowPaging="True" PageSize="10" DataKeyNames="ID" Width="100%" Height="50px"
                                        ToolTip="click row to select record" Font-Size="12px" ShowHeaderWhenEmpty="True"
                                        EmptyDataText="No data to display" AutoGenerateColumns="False" GridLines="Both"
                                        ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC" CssClass="table table-condensed">
                                        <RowStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="Mode" HeaderText="Exam Mode" />
                                            <asp:BoundField DataField="TestStage" HeaderText="Stage" />
                                            <asp:BoundField DataField="TestTitle" HeaderText="Test" />
                                            <asp:BoundField DataField="questions" HeaderText="Correct Ans" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="score" HeaderText="Score" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="passmark" HeaderText="Pass Score" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="DateTaken" HeaderText="Date" DataFormatString="{0:dd, MMM yyyy}" />
                                        </Columns>
                                        <HeaderStyle BackColor="White" ForeColor="#1BA691" HorizontalAlign="center" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
            <div class="col-md-8 m-t-20">
                <button id="btsave" runat="server" onserverclick="btnSend_Click" type="submit"
                    style="width: 150px" class="btn btn-success">
                    Save & Update</button>
                     <asp:Button ID="btComplete" runat="server" Text="Complete" OnClientClick="Complete()"
                     Width="150px" Height="34px" CssClass="btn btn-success " ToolTip="Mark requisition as complete to forward to Department Head for approval" />
                <button id="btnclose" runat="server" onserverclick="btnCancel_Click" type="submit"
                    style="width: 150px" class="btn btn-primary btn-info">
                    Close</button>
            </div>
        </div>
        </div></div>
        </div>
        
        </form>
    </body>
    </html>
</asp:Content>
