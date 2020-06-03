<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="InterviewEvaluationForm.aspx.vb"
    Inherits="GOSHRM.InterviewEvaluationForm" EnableEventValidation="false" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript">

        function closeWin() {
            popup.close();   // Closes the new window
        }
   

    </script>

    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600,700,300italic,400italic,600italic" />
    <link href="~/css/bootstrap.min.css" rel="stylesheet" type="text/css">
    <link href="~/css/font-awesome.min.css" rel="stylesheet" type="text/css">
    <link href="~/css/fullcalendar.min.css" rel="stylesheet" />
    <link href="~/css/dataTables.bootstrap.min.css" rel="stylesheet" type="text/css">
    <link rel="stylesheet" href="~/css/select2.min.css" type="text/css">
    <link rel="stylesheet" href="~/css/bootstrap-datetimepicker.min.css" type="text/css">
    <link rel="stylesheet" href="~/plugins/morris/morris.css">
    <link href="~/css/style.css" rel="stylesheet" type="text/css">
    <link href="~/css/gridview.css" rel="stylesheet" type="text/css">

</head>
<body style="height: 317px">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="container">
        <div class="row">
            <div id="divalert" runat="server" visible="false" class="alert alert-info">
                <strong id="msgalert" runat="server"></strong>
                      <asp:Label ID="lblid" runat="server" Font-Names="Verdana" Font-Size="1px"
                                Visible="False"></asp:Label>
                                <asp:Label ID="lblIntid" runat="server" Font-Names="Verdana" Font-Size="1px"
                                Visible="False"></asp:Label>
                                <asp:Label ID="lblInterviewerID" runat="server" Font-Names="Verdana" Font-Size="1px"
                               Visible="False"></asp:Label>
                               <asp:Label ID="lblApplicantID" runat="server" Font-Names="Verdana" Font-Size="1px"
                                Visible="False"></asp:Label>
            </div>
        </div>
        <div class="row">
            <h5 id="pagetitle" runat="server" class="page-title">
                CANDIDATE EVALUATION FORM</h5>
        </div>
        <div class="row">
            <div class=" col-md-12">
                <div class="form-group">
                    <label>
                        CANDIDATE</label>
                    <input id="acandidate" runat="server" class="form-control" type="text" disabled="disabled" />
                </div>
            </div>
        </div>
         <div class="row">
            <div class=" col-md-12">
                <div class="form-group">
                    <label>
                        INTERVIEWER</label>
                    <input id="ainterviewer" runat="server" class="form-control" type="text" disabled="disabled" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class=" col-md-12">
                <div class="form-group">
                    <label>
                        INTERVIEW DATE</label>
                    <div class="cal-icon">
                        <input id="ainterviewdate" runat="server" class="form-control datetimepicker" type="text" /></div>
                </div>
            </div>
        </div>
        <div class="panel panel-success">
            <div class="panel-heading">
                <b>Scoring</b>
            </div>
            <div class="panel-body">
                <div>
                    <div class="row">
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label style="font-size:12px" >Candidate evaluation forms are to be completed by the interviewer to rank the candidates overall qualifications for the position. Under each heading the interviewer should give the candidate a numerical rating and write specific job related comments in the space provided. The numerical rating system is based on the following:</label>                                
                                <label style="font-size:12px" id="lbrating" runat="server" ></label> 
                            </div>
                        </div>
                    </div>
                    <div class="card-box">
                        <h5 class="card-title" style="color: #1BA691">
                            Education Backgroud</h5>
                        <div class="row">
                            <div class=" col-md-12">
                                <div class="form-group">
                                    <label>
                                        Does the candidate have the appropriate educational qualifications or training for
                                        this position?</label>
                                </div>
                            </div>
                            <div class=" col-md-12">
                                <div class="form-group">
                                    <label>
                                        Rating</label>
                                    <asp:RadioButtonList ID="rdoeducation" runat="server" Font-Names="Verdana" Font-Size="13px"
                                        ForeColor="#666666" RepeatDirection="Horizontal" Width="100%">
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class=" col-md-12">
                                <div class="form-group">
                                    <label>
                                        Comment</label>
                                    <textarea id="aeducation" runat="server" class="form-control" rows="4"></textarea>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="card-box">
                        <h5 class="card-title" style="color: #1BA691">
                            Prior Work Experience</h5>
                        <div class="row">
                            <div class=" col-md-12">
                                <div class="form-group">
                                    <label>
                                        Has the candidate acquired necessary skills or qualifications through past work experiences?</label>
                                </div>
                            </div>
                            <div class=" col-md-12">
                                <div class="form-group">
                                    <label>
                                        Rating</label>
                                        <asp:RadioButtonList ID="rdoworkexp" runat="server" Font-Names="Verdana" Font-Size="13px" ForeColor="#666666"
                                RepeatDirection="Horizontal" Width="100%">
                            </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class=" col-md-12">
                                <div class="form-group">
                                    <label>
                                        Comment</label>
                                        <textarea id="aworkexperience" runat="server" class="form-control" rows="4"></textarea>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="card-box">
                        <h5 class="card-title" style="color: #1BA691">
                            Technical Qualifications/Experience</h5>
                        <div class="row">
                            <div class=" col-md-12">
                                <div class="form-group">
                                    <label>
                                        Does the candidate have the technical skills necessary for this position?</label>
                                </div>
                            </div>
                            <div class=" col-md-12">
                                <div class="form-group">
                                    <label>
                                        Rating</label>
                                        <asp:RadioButtonList ID="rdotechnical" runat="server" Font-Names="Verdana" Font-Size="13px" ForeColor="#666666"
                                RepeatDirection="Horizontal" Width="100%">
                            </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class=" col-md-12">
                                <div class="form-group">
                                    <label>
                                        Comment</label>
                                        <textarea id="atechnical" runat="server" class="form-control" rows="4"></textarea>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="card-box">
                        <h5 class="card-title" style="color: #1BA691">
                            Administrative and budgetary experience: financial planning, staff supervision, management of resources</h5>
                        <div class="row">
                            <div class=" col-md-12">
                                <div class="form-group">
                                    <label>
                                        Does the candidate demonstrate the knowledge of these areas necessary for this position?</label>
                                </div>
                            </div>
                            <div class=" col-md-12">
                                <div class="form-group">
                                    <label>
                                        Rating</label>
                                        <asp:RadioButtonList ID="rdoadmin" runat="server" ForeColor="#666666" Font-Names="Verdana" Font-Size="13px"
                                RepeatDirection="Horizontal" Width="100%">
                            </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class=" col-md-12">
                                <div class="form-group">
                                    <label>
                                        Comment</label>
                                        <textarea id="aadmin" runat="server" class="form-control" rows="4"></textarea>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="card-box">
                        <h5 class="card-title" style="color: #1BA691">
                            Leadership Ability</h5>
                        <div class="row">
                            <div class=" col-md-12">
                                <div class="form-group">
                                    <label>
                                        Did the candidate demonstrate the leadership skills necessary for this position?</label>
                                </div>
                            </div>
                            <div class=" col-md-12">
                                <div class="form-group">
                                    <label>
                                        Rating</label>
                                        <asp:RadioButtonList ID="rdoleadership" runat="server" Font-Names="Verdana" Font-Size="13px" ForeColor="#666666"
                                RepeatDirection="Horizontal" Width="100%">
                            </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class=" col-md-12">
                                <div class="form-group">
                                    <label>
                                        Comment</label>
                                        <textarea id="aleadership" runat="server" class="form-control" rows="4"></textarea>
                                </div>
                            </div>
                        </div>
                    </div>
                      <div class="card-box">
                        <h5 class="card-title" style="color: #1BA691">
                            Customer Service Skills</h5>
                        <div class="row">
                            <div class=" col-md-12">
                                <div class="form-group">
                                    <label>
                                        Did the candidate demonstrate the knowledge and skills to create a positive customer experience/interaction necessary for this position?</label>
                                </div>
                            </div>
                            <div class=" col-md-12">
                                <div class="form-group">
                                    <label>
                                        Rating</label>
                                        <asp:RadioButtonList ID="rdocustomer" runat="server" ForeColor="#666666" Font-Names="Verdana" Font-Size="13px"
                                RepeatDirection="Horizontal" Width="100%">
                            </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class=" col-md-12">
                                <div class="form-group">
                                    <label>
                                        Comment</label>
                                        <textarea id="acustomer" runat="server" class="form-control" rows="4"></textarea>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="card-box">
                        <h5 class="card-title" style="color: #1BA691">
                            Communication Skills</h5>
                        <div class="row">
                            <div class=" col-md-12">
                                <div class="form-group">
                                    <label>
                                        How were the candidate’s communication skills during the interview?</label>
                                </div>
                            </div>
                            <div class=" col-md-12">
                                <div class="form-group">
                                    <label>
                                        Rating</label>
                                        <asp:RadioButtonList ID="rdocommunication" runat="server" ForeColor="#666666" Font-Names="Verdana" Font-Size="13px"
                                RepeatDirection="Horizontal" Width="100%">
                            </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class=" col-md-12">
                                <div class="form-group">
                                    <label>
                                        Comment</label>
                                        <textarea id="acommunication" runat="server" class="form-control" rows="4"></textarea>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="card-box">
                        <h5 class="card-title" style="color: #1BA691">
                            Candidate Enthusiasm</h5>
                        <div class="row">
                            <div class=" col-md-12">
                                <div class="form-group">
                                    <label>
                                        How much interest did the candidate show in the position?</label>
                                </div>
                            </div>
                            <div class=" col-md-12">
                                <div class="form-group">
                                    <label>
                                        Rating</label>
                                        <asp:RadioButtonList ID="rdoenthusiam" runat="server" Font-Names="Verdana" Font-Size="13px" ForeColor="#666666"
                                RepeatDirection="Horizontal" Width="100%">
                            </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class=" col-md-12">
                                <div class="form-group">
                                    <label>
                                        Comment</label>
                                        <textarea id="aenthisiam" runat="server" class="form-control" rows="4"></textarea>
                                </div>
                            </div>
                        </div>
                    </div>
                     <div class="card-box">
                        <h5 class="card-title" style="color: #1BA691">
                            Overall Impression about Candidate</h5>
                        <div class="row">
                            <div class=" col-md-12">
                                <div class="form-group">
                                    <label>
                                        Final comments and recommendations for proceeding with this candidate.</label>
                                </div>
                            </div>
                            <div class=" col-md-12">
                                <div class="form-group">
                                    <label>
                                        Rating</label>
                                        <asp:RadioButtonList ID="rdoimpression" runat="server" Font-Names="Verdana" Font-Size="12px" ForeColor="#666666"
                                RepeatDirection="Horizontal" Width="100%">
                            </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class=" col-md-12">
                                <div class="form-group">
                                    <label>
                                        Comment</label>
                                        <textarea id="aimpression" runat="server" class="form-control" rows="4"></textarea>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-8 m-t-20">
                            <button id="btsave" runat="server" onserverclick="btnMedSaveSection_Click" type="submit" style="width: 150px"
                                class="btn btn-success">
                                Save & Update</button>
                            <button id="btnclose" runat="server" onserverclick="btnExit_Click" type="submit"
                                style="width: 150px" class="btn btn-primary btn-info">
                                Close</button>
                        </div>
                    </div>



                </div>
            </div>
        </div>

    </div>


    
    </form>
</body>
</html>
