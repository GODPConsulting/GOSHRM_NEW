<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="EmployeeTrainingsUpdate.aspx.vb"
    Inherits="GOSHRM.EmployeeTrainingsUpdate" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title></title>


    </head>

    <body>
        <form id="form1" action="">
            <div class="container col-md-8">
                <div class="row">
                    <div class=" col-md-12">
                        <div id="divalert" runat="server" visible="false" class="alert alert-info">
                            <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <strong id="msgalert" runat="server"></strong>
                            <asp:TextBox ID="txtid" runat="server" Width="1px" Height="1px" Visible="False"></asp:TextBox>
                            <asp:Label ID="lblsessionid" runat="server" Font-Size="1px" Visible="False"></asp:Label>
                            <asp:Label ID="lblassessment" runat="server" Visible="False" Font-Size="1px"></asp:Label>
                            <asp:Label ID="lblappassessment" runat="server" Visible="False" Font-Size="1px"></asp:Label>
                             <asp:Label ID="lblassessmentdate" runat="server" Visible="False" Font-Size="1px"></asp:Label>
                            <asp:Label ID="lbldateassessment" runat="server" Visible="False" Font-Size="1px"></asp:Label>
                            <asp:Label ID="lblappdateassessment" runat="server" Visible="False" Font-Size="1px"></asp:Label>
                            <asp:Label ID="lblEmpID" runat="server" Visible="False" Font-Size="1px"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="panel panel-success">
                        <div class="panel-body">
                            <div class="col-md-8 col-md-offset-0">
                                <h5 id="pagetitle" runat="server" class="page-title">Employee Training Session
                                </h5>
                            </div>
                            <div id="divemplink" runat="server" class="row">
                                <div id="divjoboffer" runat="server" class="col-sm-3 col-md-12 pull-left">
                                    <p>
                                        <a href="Trainings"><u>Training & Development</u></a>
                                        <label>
                                            >
                                        </label>
                                        <a id="A1" href="#"><u>Employee Training Session</u></a>
                                    </p>
                                </div>
                            </div>

                            <div id="collapse_acc" runat="server" class=" card-box">
                                <div class="card-header">
                                    <h6><a class="collapsed card-link" data-toggle="collapse" href="#collapseTwo" title="Click to view" >Skills to Acquire and Training Accomplishment
                                    </a></h6>
                                </div>
                                <div id="collapseTwo" class="collapse" data-parent="#accordion">
                                    <div class="card-body">
                                        <div class="panel panel-success">
                                            <div class="panel-heading">
                                                <b id="B1" runat="server">Skills Accomplishment</b>
                                            </div>
                                            <div class="panel-body">
                                                <asp:DataList ID="gridAccomplishment" runat="server" Width="100%" RepeatColumns="1" CellSpacing="1"
                                                    RepeatLayout="Table" Font-Names="Arial" Font-Size="15px" GridLines="Both" DataKeyField="id"
                                                    BorderColor="#CCCCCC" ForeColor="#666666" BorderStyle="Solid"
                                                    BorderWidth="1px">
                                                    <ItemTemplate>
                                                        <table class="table" width="100%">
                                                            <tr>
                                                                <td valign="top" style="width: 100%">

                                                                    <p class="m-b-5"><%# Eval("kpiobjectives")%> <span id="datscore" runat="server" class="text-success pull-right"><%# Eval("achievement")%>%</span></p>

                                                                    <div class="progress progress-xs  m-b-0">
                                                                        <div id="datprogress" runat="server" class="progress-bar progress-bar-success" role="progressbar" data-toggle="tooltip" title="40%" style="width: 40%"></div>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                            </div>
                                        </div>
                                        
                                        <div class="panel panel-primary">
                                            <div class="panel-heading">
                                                <b id="B2" runat="server">Training Skills To Acquire</b>
                                            </div>
                                            <div class="panel-body">
                                                <asp:DataList ID="gridAcquire" runat="server" Width="100%" RepeatColumns="1" CellSpacing="1" CellPadding="1"
                                                    RepeatLayout="Table" Font-Names="Arial" Font-Size="5px" GridLines="Both" DataKeyField="id"
                                                    BorderColor="#CCCCCC" ForeColor="#666666" BorderStyle="Solid"
                                                    BorderWidth="1px">
                                                    <ItemTemplate>
                                                        <table class="table" width="100%">
                                                            <tr>
                                                                <td valign="top" style="width: 100%">

                                                                    <p class="m-b-5"><%# Eval("kpiobjectives")%> <span id="datscore2" runat="server" class="text-success pull-right"><%# Eval("rating")%>%</span></p>

                                                                    <div class="progress progress-xs m-b-0">
                                                                        <div id="datprogress2" runat="server" class="progress-bar progress-bar-success" role="progressbar" data-toggle="tooltip" title="40%" style="width: 40%"></div>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <br />



                            <div class="row">
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            NAME</label>
                                        <input id="aname" runat="server" class="form-control" type="text" />
                                    </div>
                                </div>
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            TRAINING SESSION</label>
                                        <input id="atrainingsession" runat="server" class="form-control" type="text" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            DATE</label>
                                        <input id="adate" runat="server" class="form-control" type="text" />
                                    </div>
                                </div>
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            TIME</label>
                                        <input id="atime" runat="server" class="form-control" type="text" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <label>
                                            VENUE</label>
                                        <textarea id="avenue" runat="server" class="form-control" rows="3" cols="1" readonly="readonly"></textarea>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            TRAINING STATUS</label>
                                        <telerik:RadComboBox ID="cbotrainingstat" runat="server" Width="100%" ForeColor="#666666"
                                            Skin="Bootstrap" RenderMode="Lightweight"
                                            EmptyMessage="Select">
                                        </telerik:RadComboBox>
                                    </div>
                                </div>
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            TRAINING RATING</label>
                                        <telerik:RadRating ID="trainingrate" runat="server" AutoPostBack="True" ToolTip="How you rate training"
                                            RenderMode="Lightweight" Skin="Bootstrap">
                                        </telerik:RadRating>
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel4" UpdateMode="Always">
                                            <ContentTemplate>
                                                <label id="lbrating" runat="server"></label>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="trainingrate" EventName="Rate" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <label>
                                            OBJECTIVES</label>
                                        <textarea id="aobjective" runat="server" class="form-control" rows="3" cols="1"></textarea>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <label>
                                            COMMENT</label>
                                        <textarea id="acomment" runat="server" class="form-control" rows="3" cols="1"></textarea>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12 m-t-20 text-center">
                                    <button id="lnktrainassessment" runat="server" onserverclick="lnkTrainingAssessment_Click" type="submit"
                                        style="width: 200px" class="btn btn-link " title="Assess and rate the training attended">
                                        Training Assessment</button>
                                    <button id="lnklearnassessment" runat="server" onserverclick="lnkLearning_Click" type="submit"
                                        style="width: 200px" class="btn btn-link " title="Perform self assessment on things learned at the training">
                                        Learning Assessment</button>
                                    <button id="lnkapplicationassessment" runat="server" onserverclick="lnkApplication_Click" type="submit"
                                        style="width: 200px" class="btn btn-link " title="Self evaluation on how training has impacted your work">
                                        Application Assessment</button>
                                     <button id="Button1" runat="server" onserverclick="lnkmaterials_Click" type="submit"
                                        style="width: 200px" class="btn btn-link " title="Self evaluation on how training has impacted your work">
                                        Materials</button>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12 m-t-20 text-center">
                                    <button id="btnupdate" runat="server" onserverclick="btnAdd_Click" type="submit"
                                        style="width: 150px" class="btn btn-primary btn-success">
                                        Save &amp; Update</button>
                                    <button id="btcancel" runat="server" onserverclick="btnCancel_Click" type="submit"
                                        style="width: 150px" class="btn btn-primary btn-info">
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
