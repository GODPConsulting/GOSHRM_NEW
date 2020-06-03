﻿
    <%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="false" CodeBehind="AppraisalFeedback.aspx.vb"
    Inherits="GOSHRM.AppraisalFeedback" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to submit for review?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>

      <script type="text/javascript">
          function ConfirmDisagree() {
              var confirmplan_value = document.createElement("INPUT");
              confirmplan_value.type = "hidden";
              confirmplan_value.name = "confirmplan_value";
              if (confirm("Do you want to disagree with Appraisal Objective?")) {
                  confirmplan_value.value = "Yes";
              } else {
                  confirmplan_value.value = "No";
              }
              document.forms[0].appendChild(confirmplan_value);
          }
    </script>

    <script type="text/javascript">

        function closeWin() {
            popup.close();   // Closes the new window
        }
   

    </script>
   
</head>
<body>
    <form id="form1" action="">
    <div class="container col-md-12">
     <div id="divalert" runat="server" visible="false" class="alert alert-info">
                    <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <strong
                id="msgalert" runat="server">Danger!</strong>
                
                    <asp:Label ID="lblid" runat="server" Font-Names="Verdana" Font-Size="1px" Visible="False"></asp:Label>
                    <asp:Label ID="lblend" runat="server" Font-Names="Verdana" Font-Size="1px" Visible="False"></asp:Label>
                    <asp:Label ID="lblvisibleI" runat="server" Font-Names="Verdana" Font-Size="1px" Visible="False"></asp:Label>
                    <asp:Label ID="lblreviewer" runat="server" Font-Names="Verdana" Font-Size="1px" Visible="False"></asp:Label>
                    <asp:Label ID="Label15" runat="server" Font-Names="Verdana" Font-Size="1px" Visible="False"></asp:Label>
                    <asp:Label ID="lblQuestID" runat="server" Font-Names="Verdana" Font-Size="1px" Visible="False"></asp:Label>
                     <asp:Label ID="lblrevieweremail" runat="server" Font-Names="Verdana" Font-Size="1px" Visible="False"></asp:Label>
                    <asp:Label ID="lblQuestCount" runat="server" Font-Names="Verdana" Font-Size="1px" Visible="False"></asp:Label>
                    <asp:Label ID="lblreviewer2" runat="server" Font-Names="Verdana" Font-Size="1px" Visible="False"></asp:Label>
                    <asp:Label ID="lblvisibleII" runat="server" Font-Names="Verdana" Font-Size="1px" Visible="False"></asp:Label>
                    <asp:TextBox ID="txtEmpID" runat="server" Font-Size="1px" Visible="False"></asp:TextBox>
                     <asp:TextBox ID="txtdept" runat="server" Font-Size="1px"  Visible="false"></asp:TextBox>
                    <asp:TextBox ID="txtlocation" runat="server" Font-Size="1px" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="MrgEndcycle" runat="server" Font-Size="1px" Visible="false"></asp:TextBox>
                    
                            <asp:UpdatePanel runat="server" ID="UpdatePanel" UpdateMode="Always">
                                <ContentTemplate>
                                    <asp:Label ID="lblMyRating" runat="server" Font-Names="Verdana" Font-Size="1px" Visible="False"></asp:Label>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="rdoMyRatings" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>

                            <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Always">
                                <ContentTemplate>
                                    <asp:Label ID="lblMgrRating" runat="server" Font-Names="Verdana" Font-Size="1px" Visible="False"></asp:Label>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="rdoMgrRatings" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>

                            <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                <ContentTemplate>
                                    <asp:Label ID="lblMgrRating2" runat="server" Font-Names="Verdana" Font-Size="1px" Visible="False"></asp:Label>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="rdoMgrRatings2" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                </div>
    <div class="panel panel-success">
                <div class="panel-heading">
                        <h5 id="pagetitle" runat="server" class="page-title">Performance Appraisal Feedback Form</h5>
                </div>
               
                <div style="display:none" id="divemplink" runat="server" class="row">
                <div id="divjoboffer" runat="server" class="col-sm-3 col-md-12 col-xs-6 pull-left">
                    <p>
                        <a href="AppraisalFeedbackList"><u>Appraisal Feedback</u></a>
                        <label>
                            >
                        </label>
                        <a href="#"><u>Performance Appraisal Feedback Form</u></a>
                    </p>
                </div>
            </div>
                <div id="reviewerdetails" runat="server" class="row col-md-12 m-t-10">
                    <div class=" col-md-4">
                        <div class="form-group">
                            <label>
                                REVIEW YEAR</label>
                            <input id="reviewyear" runat="server" class="form-control" type="text" disabled="disabled" />
                        </div>
                    </div>
                    <div class=" col-md-4">
                        <div class="form-group">
                            <label>
                                REVIEW PERIOD START</label>
                            <input id="reviewstart" runat="server" class="form-control" type="text" disabled="disabled" />
                        </div>
                    </div>
                    <div class=" col-md-4">
                        <div class="form-group">
                            <label>
                                REVIEW PERIOD END</label>
                            <input id="reviewend" runat="server" class="form-control" type="text" disabled="disabled" />
                        </div>
                    </div>
                    <div class=" col-md-4">
                        <div class="form-group">
                            <label>
                                EMPLOYEE</label>
                            <input id="aname" runat="server" class="form-control" type="text" disabled="disabled" />
                        </div>
                    </div>
                    <div class=" col-md-4">
                        <div class="form-group">
                            <label>
                                JOB GRADE</label>
                            <input id="ajobgrade" runat="server" class="form-control" type="text" disabled="disabled" />
                        </div>
                    </div>
                    <div class=" col-md-4">
                        <div class="form-group">
                            <label>
                                JOB TITLE</label>
                            <input id="ajobtitle" runat="server" class="form-control" type="text" disabled="disabled" />
                        </div>
                    </div>

                    <div class=" col-md-4">
                        <div class="form-group">
                            <label>
                                LENGTH OF SERVICE</label>
                            <input id="alenofservice" runat="server" class="form-control" type="text" disabled="disabled" />
                        </div>
                    </div>
                    <div class=" col-md-4">
                        <div class="form-group">
                            <label>
                                TIME IN PRESENT POSITION</label>
                            <input id="apresentposition" runat="server" class="form-control" type="text" disabled="disabled" />
                        </div>
                    </div>
                    <div class="col-md-4 m-t-20 form-group pull-right">
                     <label>&nbsp;&nbsp;&nbsp; </label>
                        <button id="btnsubmit" onserverclick="btnEndcycle" visible="false" style="width:150px;" runat="server" type="submit" class="btn btn-success">End Cycle</button>
                        <button id="btnrefresh" data-toggle="tooltip" data-original-title="Link to Query" runat="server" onserverclick="btnDev_Click3" type="submit"
                        style="height:35px" class="btn btn-default fa fa-user-times"></button>
                        <button id="Button5" data-toggle="tooltip" data-original-title="Link to Training" runat="server" onserverclick="btnDev_Click1" type="submit"
                        style="height:35px" class="btn btn-default fa fa-graduation-cap"></button>
                        <button id="Button6" data-toggle="tooltip" data-original-title="Link to Feedback Nugget" runat="server" onserverclick="btnDev_Click2" type="submit"
                        style="height:35px;" class="btn btn-default fa fa-american-sign-language-interpreting"></button>
                        <button id="Button1" data-toggle="tooltip" data-original-title="Link to Development Plan" runat="server" onserverclick="btnDev_Click" type="submit"
                        style="height:35px" class="btn btn-default fa fa-id-card"></button>

                    </div>
                    <div id="divoverdesc" runat="server" class=" col-md-12">
                        <div class="form-group">
                            <label>
                                OVERALL REMARK</label>
                            <input id="aoverdesc" runat="server" class="form-control" type="text" disabled="disabled" />
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="panel panel-success">
                            <div class="panel-heading">
                                <b>PERFORMANCE REVIEWER</b>
                            </div>
                            <div class="panel-body">
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            1. FIRST REVIEWER</label>
                                        <input id="areviewer1" runat="server" class="form-control" type="text" disabled="disabled" />
                                    </div>
                                </div>
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            2. SECOND REVIEWER</label>
                                        <input id="areviewer2" runat="server" class="form-control" type="text" disabled="disabled" />
                                    </div>
                                </div>
                            </div>
                        </div>    
                    </div>
                    <div class="col-md-12 text-center">
                                                <button id="Button3" runat="server"  type="submit"
                                                style="width: 150px" onserverclick="btnContinue_Click" class="btn btn-primary btn-success">
                                                Feedback</button>
                                                <button id="Button4" runat="server"  type="submit"
                                                style="width: 150px" onserverclick="btnContinue_Click" class="btn btn-primary btn-info">
                                                Feedback</button>
                                            <button id="Button2" runat="server" onserverclick="btnClose_Click" type="submit"
                                                style="width: 150px" class="btn btn-primary btn-danger">
                                                Back</button>
                                 </div>
                    </div>
                    <div class="row">
                        <div class=" col-md-12">
                        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                            <asp:View ID="View1" runat="server">
                                <div id="nuggetsquestion" runat="server" style="display:none;" class="row">
                                    <div id="questionaira" runat="server"  class=" col-md-12">
                                    <label id="aratingdesc" runat ="server"> </label>
                                        <div class="">
                                            <div style="display:none;" class="panel-heading">
                                                <b>QUESTIONAIRE</b>
                                                <h6 id="akpitype" runat="server" class="page-title" visible="false">
                                                    Performance Appraisal Objective:</h6>
                                            </div>
                                            <div class="panel-body">
                                                <div class=" col-md-12">
                                                    <div class="row">
                                                        <div class=" col-md-12">
                                                            <div class="form-group">
                                                                <label id="aQuestNo" runat="server" visible="false">
                                                                    1.</label>
                                                                <label id="apageview" runat="server">
                                                                </label>
                                                                <input id="aObjective" runat="server" class="form-control" type="text" readonly="readonly" />
                                                                <label>
                                                                    <i id="aObjDesc" runat="server">1.</i></label>
                                                            </div>
                                                        </div>
                                                       
                                                        <div class=" col-md-12">
                                                            <div class="form-group">
                                                                <label id="lll" runat="server">
                                                                    SUCCESS TARGET:</label><br />
                                                                <label id="aMyObjective" style="text-indent: 20px; white-space: pre; display:inline" runat="server">
                                                                </label>
                                                            </div>
                                                        </div>
                                                         <div id="emp_app" runat="server">
                                                        <div class=" col-md-12">
                                                            <div class="form-group">
                                                                <label>
                                                                    COMMENT</label>
                                                                <textarea id="aMyPerformance" runat="server" class="form-control" rows="3"></textarea>
                                                                <div class="row">
                                                                <asp:RadioButtonList ID="rdoMyRatings" runat="server" Font-Names="Verdana" Font-Size="13px"
                                                                    ForeColor="#666666" AutoPostBack="True" 
                                                                        Width="100%">
                                                                </asp:RadioButtonList>
                                                                </div>
                                                                
                                                            </div>
                                                        </div>
                                                        </div>
                                                        <div id="divreviewer1" runat="server" class=" col-md-12">
                                                            <div class="form-group">
                                                                <label>
                                                                    FIRST REVIEWER COMMENT</label>
                                                                <textarea id="amanager1" runat="server" class="form-control" rows="5"></textarea>
                                                                <asp:RadioButtonList ID="rdoMgrRatings" runat="server" Font-Names="Verdana" Font-Size="13px"
                                                                    ForeColor="#666666" AutoPostBack="True" Width="70%">
                                                                </asp:RadioButtonList>
                                                            </div>
                                                        </div>
                                                        <div id="divreviewer2" runat="server" class=" col-md-12">
                                                            <div class="form-group">
                                                                <label>
                                                                    SECOND REVIEWER COMMENT</label>
                                                                <textarea id="amanager2" runat="server" class="form-control" rows="5"></textarea>
                                                                <asp:RadioButtonList ID="rdoMgrRatings2" runat="server" Font-Names="Verdana" Font-Size="13px"
                                                                    ForeColor="#666666" AutoPostBack="True" Width="70%">
                                                                </asp:RadioButtonList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12 text-center">
                                            <button id="btprevious" runat="server" onserverclick="btnPrevious_Click" type="button"
                                                style="width: 150px" class="btn btn-primary btn-success">
                                                Previous</button>
                                            <button id="btnext" runat="server" onserverclick="btnNext_Click" type="button" style="width: 150px"
                                                class="btn btn-success">
                                                Next</button>                                            
                                            <button id="btSubmit" runat="server" onserverclick="btnSubmit_Click" type="button"
                                                style="width: 150px" class="btn btn-info">
                                                Finish</button>
                                            <button runat="server" onserverclick="btnClose_Click" type="button" style="width: 150px"
                                                class="btn btn-danger">
                                                Back</button>
                                        </div>
                                    </div>
                                </div>
                            </asp:View>
                            <asp:View ID="View2" runat="server">
                                <div class="row">
                                <div id="gridsss" runat="server" class=" col-md-12">
                                        <div class="panel-heading">
                                            <b style="color:Green;">FEEDBACK REVIEW</b>
                                        </div>
                                        <div class="m-b-20">
                                            <div class="col-md-12 text-left">
                                                <button id="btback" runat="server" onserverclick="btnBack_Click" type="submit" style="width: 150px"
                                                    class="btn btn-primary btn-info">
                                                    << Back</button>
                                                <asp:Button ID="btSubmitReview" runat="server" Text="Submit For Review" ForeColor="White"
                                                    Width="170px" Height="35px" BorderStyle="None" Font-Size="14px" 
                                                    OnClientClick="Confirm()" ToolTip="Complete" CssClass="btn btn-success" />
                                                <button id="bt360degree" runat="server" onserverclick="btn360degree_Click" type="submit"
                                                    style="width: 150px" class="btn btn-purple">
                                                    360 Feedback</button>
                                                <asp:Button ID="btnDisagree" runat="server" Text="Disagree First Review" ForeColor="White"
                                                    Width="200px" Height="35px" BorderStyle="None" Font-Size="14px" Visible="False"
                                                    OnClientClick="ConfirmDisagree()" ToolTip="click to disagree" CssClass="btn btn-danger" />
                                            </div>
                                            <div id="divrecommendation" runat="server" class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <label>
                                                            RECOMMENDATION</label>
                                                        <telerik:RadComboBox ID="cborecommendation" runat="server" AutoPostBack="True" Skin="Bootstrap"
                                                            ForeColor="#666666" RenderMode="Lightweight" Width="100%" ResolvedRenderMode="Classic">
                                                            <Items>
                                                                <telerik:RadComboBoxItem runat="server" Text="--select--" Value="--select--" />
                                                                <telerik:RadComboBoxItem runat="server" Text="Development Required" Value="Development Required" />
                                                                <telerik:RadComboBoxItem runat="server" Text="Merit Increase" Value="Merit Increase" />
                                                                <telerik:RadComboBoxItem runat="server" Text="No Recommendation" Value="No Recommendation" />
                                                                <telerik:RadComboBoxItem runat="server" Text="Promote" Value="Promote" />
                                                                <telerik:RadComboBoxItem runat="server" Text="Terminate Job" Value="Terminate Job" />
                                                                <telerik:RadComboBoxItem runat="server" Text="Transfer" Value="Transfer" />
                                                            </Items>
                                                        </telerik:RadComboBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <telerik:RadGrid ID="gridReviewee" runat="server" AllowMultiRowSelection="True" AllowPaging="True"
                                                        AllowSorting="True" AutoGenerateColumns="False" GridLines="Both" BorderWidth="1px"
                                                        BorderColor="#CCCCCC" DataKeyNames="ID" EnableGroupsExpandAll="True" Font-Names="Verdana"
                                                        Font-Size="12px" GroupPanelPosition="Top" PageSize="50" RenderMode="Lightweight"
                                                        ResolvedRenderMode="Classic" ShowFooter="True" ShowGroupPanel="True" ShowStatusBar="True"
                                                        Width="100%" Visible="False">
                                                        <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                                        <MasterTableView EnableGroupsExpandAll="True" Width="100%">
                                                            <GroupByExpressions>
                                                                <telerik:GridGroupByExpression>
                                                                    <SelectFields>
                                                                        <telerik:GridGroupByField FieldAlias="" FieldName="KPIType" />
                                                                    </SelectFields>
                                                                    <GroupByFields>
                                                                        <telerik:GridGroupByField FieldName="KPIType" SortOrder="Ascending" />
                                                                    </GroupByFields>
                                                                </telerik:GridGroupByExpression>
                                                            </GroupByExpressions>
                                                            <Columns>
                                                                <telerik:GridBoundColumn DataField="KPIObjectives" HeaderButtonType="TextButton"
                                                                    ItemStyle-VerticalAlign="Top" HeaderText="KPI Type">
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="objectives" HeaderButtonType="TextButton" 
                                                                    ItemStyle-VerticalAlign="Top" UniqueName="objectives" HeaderText="Objectives"
                                                                    >
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="successtarget" HeaderButtonType="TextButton"
                                                                    ItemStyle-VerticalAlign="Top" UniqueName="successtarget" 
                                                                    HeaderText="Success Target" >
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="EMpIDComment" HeaderButtonType="TextButton" 
                                                                    ItemStyle-VerticalAlign="Top" UniqueName="empidcomment" HeaderText="Reviewee Comment"
                                                                    >
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="EMpIDRating" HeaderButtonType="TextButton" 
                                                                    ItemStyle-VerticalAlign="Top" HeaderText="Points" 
                                                                    >
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="supervisorcomment" HeaderButtonType="TextButton"
                                                                    ItemStyle-VerticalAlign="Top" UniqueName="supervisorcomment" 
                                                                    HeaderText="Reviewer I Comment" >
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="mgrIDRating" HeaderButtonType="TextButton" 
                                                                    ItemStyle-VerticalAlign="Top" HeaderText="Points"
                                                                    >
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="supervisorcomment2" HeaderButtonType="TextButton"
                                                                    ItemStyle-VerticalAlign="Top" UniqueName="supervisorcomment2" 
                                                                    HeaderText="Reviewer II Comment" >
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="mgrIDRating2" HeaderButtonType="TextButton" 
                                                                    ItemStyle-VerticalAlign="Top" HeaderText="Points"
                                                                    >
                                                                </telerik:GridBoundColumn>
                                                            </Columns>
                                                        </MasterTableView>
                                                        <ClientSettings AllowColumnsReorder="True" AllowDragToGroup="True" ReorderColumnsOnClient="True">
                                                            <Resizing AllowColumnResize="True" AllowRowResize="True" EnableRealTimeResize="True"
                                                                ResizeGridOnColumnResize="False" />
                                                        </ClientSettings>
                                                        <GroupingSettings ShowUnGroupButton="true" />
                                                        <FilterMenu RenderMode="Lightweight">
                                                        </FilterMenu>
                                                        <HeaderContextMenu RenderMode="Lightweight">
                                                        </HeaderContextMenu>
                                                    </telerik:RadGrid>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                
                            </asp:View>
                             <asp:View ID="View3" runat="server">
                                <br />
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                            <div class="m-b-20">
                                                <div class="form-group">
                                                    <h3 id="H1" runat="server" class="page-title text-center">
                                                        <b>Appraisal Successfully Submitted</b></h3>
                                                </div>
                                                <div class="col-md-12 m-t-20 m-b-20 text-center">
                                                    <button id="btnend" runat="server" onserverclick="btnClose_Click" type="submit" style="width: 150px"
                                                        class="btn btn-primary btn-success">
                                                        OK</button>
                                                </div>
                                            </div>
                                    </div>
                                </div>
                            </asp:View>
                        </asp:MultiView>
                      </div>
                    </div>
                </div>
    </div>


    </form>
</body>
</html>
</asp:Content>