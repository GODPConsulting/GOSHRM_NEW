<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="AppraisalsFeedBack.aspx.vb"
    Inherits="GOSHRM.AppraisalsFeedBack" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<html xmlns="http://www.w3.org/1999/xhtml">

<title></title>
<link rel="icon" type="image/png" href="../../../images/goshrm.png">
<script type="text/javascript">
    function Confirm() {
        var confirm_value = document.createElement("INPUT");
        confirm_value.type = "hidden";
        confirm_value.name = "confirm_value";
        if (confirm("Do you want to delete data?")) {
            confirm_value.value = "Yes";
        } else {
            confirm_value.value = "No";
        }
        document.forms[0].appendChild(confirm_value);
    }
</script>
<body>
  
        <form>
       
        <div class="container col-md-12">
          <div class="row">
             <div id="divalert" runat="server" visible="false" class="alert alert-info">
                <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                <strong id="msgalert" runat="server">Danger!</strong>
                <asp:Label ID="lblid" runat="server" Font-Names="Verdana" Font-Size="12px" Visible="False"></asp:Label>
                <asp:Label ID="lblQuestCount" runat="server" Font-Names="Verdana" Font-Size="12px"
                    Visible="False"></asp:Label>
                <asp:Label ID="lblQuestID" runat="server" Font-Names="Verdana" Font-Size="12px" Visible="False"></asp:Label>
                <asp:Label ID="lblend" runat="server" Font-Names="Verdana" Font-Size="12px" Visible="False"></asp:Label>
                <asp:Label ID="lblreviewerII" runat="server" Font-Names="Verdana" Font-Size="12px"
                    Visible="False"></asp:Label>
                <asp:Label ID="lblreviewer" runat="server" Font-Names="Verdana" Font-Size="12px"
                    Visible="False"></asp:Label>
            </div>
        </div>
         <div class="panel panel-success">
                <div class="panel-heading">
                    <h5><b id="pagetitle" runat="server">Performance Appraisal Feedback Form</b></h5>
                </div>
             <div class="panel-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>REVIEW YEAR</label>  
                                <input id="txtYear" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>GRADE</label>
                                <input id="txtJobGrade" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>REVIEW PERIOD START</label>
                                <div class="row">
                                <div class="col-md-6">
                                     <telerik:RadDatePicker Skin="Bootstrap" Width="100%" ID="datStartPeriod" runat="server" Enabled="False" ForeColor="#666666">
                                     </telerik:RadDatePicker>
                                </div>
                                <div class="col-md-6">
                                     <telerik:RadDatePicker Skin="Bootstrap" Width="100%" ID="datEndReview" runat="server" Enabled="False" ForeColor="#666666">
                                    </telerik:RadDatePicker>
                                </div>
                                </div> 
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>
                                    DEPARTMENT</label>
                                <input id="txtDept" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>EMPLOYEE NUMBER*</label>
                                <input id="txtEmpID" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>
                                    LOCATION</label>
                                <input id="txtLocation" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>EMPLOYEE NAME</label>
                                <input id="txtName" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>
                                    TIME IN PRESENT POSITION</label>
                                <input id="txtPresentPos" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>JOB TITLE</label>
                                <input id="txtJobTitle" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>
                                    LENGTH OF SERVICE</label>
                                <input id="txtLengthOfService" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>REVIEWER</label>
                                <input id="txtreviewer" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>
                                    REVIEWER II</label>
                                <input id="txtreviewerII" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>REVIEWER POINT</label>
                                <input id="lblReviewerPoint" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>
                                    REVIEWER II POINT AVERAGE</label>
                                <input id="lblReviewerIIPoint" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>REVIEWER RECOMMENDATION</label>
                                <input id="lblrecommendationI" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>
                                    REVIEWER II RECOMMENDATION</label>
                                <input id="lblrecommendationII" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>OVERALL REMARK</label> 
                                <input id="lbloverdesc" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>
                                    REVIEWEE POINT AVERAGE</label>
                                <input id="lblRevieweePoint" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-4"> 
                            <div class="form-group">
                                <label>OVERALL POINT</label>
                                <input id="lbloverallpoint" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-4">
                            <div class="form-group">
                                <label>
                                    OVERALL SCORE (%)</label>
                                <input id="lblscore" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                         <div class=" col-md-4">
                            <div class="form-group">
                                <label>
                                    ADJUSTED OVERALL SCORE (%)</label>
                                <input id="txtadjustedscore" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>HR RECOMMENDATION</label>
                                <textarea id="txtHRRecommendation" runat="server" class="form-control" rows="5"></textarea>
                            </div>
                        </div>
                          <div class=" col-md-6">
                            <div class="form-group">
                                <label>Employee Final Decision</label>
                                <input id="decision"  readonly runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>
                                    Employee Comment</label>
                                <input id="comment" readonly runat="server" class="form-control" type="text" />
                            </div>
                        </div>                      
                        <div class="col-md-12 m-t-20 text-center">
                         <button id="Button1" runat="server" onserverclick="btnCancel_Click" type="submit" style="width: 150px"
                                class="btn btn-primary btn-danger">
                                << Back</button>  
                            <button id="btnClose" runat="server" onserverclick="btnClose_Click" type="submit"
                                style="width: 150px" class="btn btn-primary btn-success">
                                Save Record</button>
                            <button id="btnPromote" runat="server" onserverclick="btnPromote_Click" type="submit" style="width: 150px"
                                class="btn btn-primary btn-info">Promote Staff</button>
                                 <button id="btn360degree" runat="server" onserverclick="btn360degree_Click" type="submit" style="width: 165px"
                                class="btn btn-primary btn-success">360 Degree Feedback</button>
                        </div>
                    </div>
             <%--<table width="100%">
        <tr>
            <td class="style22">txtYear txtJobGrade 
                <asp:Label ID="Label10" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666" Text="Review Year"></asp:Label>
            </td>
            <td class="style22">
                <asp:TextBox ID="txtYear" runat="server" Width="150px" Font-Names="Verdana" Font-Size="12px" ForeColor="#666666"
                    BorderColor="#CCCCCC" BorderWidth="1px" Enabled="False" ReadOnly="True"></asp:TextBox>
            </td>
            <td class="style22">
                <asp:Label ID="Label8" runat="server" Font-Names="Verdana" Font-Bold="True" ForeColor="#666666" Font-Size="12px" Text="Grade"></asp:Label>
            </td>
            <td class="style23">
                <asp:TextBox runat="server" BorderColor="#CCCCCC" BorderWidth="1px" Font-Names="Verdana" ForeColor="#666666"
                    Font-Size="12px" Width="400px" ID="txtJobGrade" ReadOnly="True"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style22">
                <asp:Label ID="Label11" runat="server" Font-Names="Verdana" Font-Bold="True" ForeColor="#666666" Font-Size="12px" Text="Review Period Start"></asp:Label>
            </td>
            <td class="style22">
                <telerik:RadDatePicker ID="datStartPeriod" runat="server" Enabled="False" ForeColor="#666666">
                </telerik:RadDatePicker>
                <asp:Label ID="lblReviewerPoint0" runat="server" Font-Names="Verdana" 
                    Font-Size="12px"> : </asp:Label>
                <telerik:RadDatePicker ID="datEndReview" runat="server" Enabled="False" ForeColor="#666666">
                </telerik:RadDatePicker>
            </td>
            <td class="style22">
                <asp:Label ID="Label5" runat="server" Font-Names="Verdana" Font-Bold="True" ForeColor="#666666" Font-Size="12px" Text="Department"></asp:Label>
            </td>
            <td class="style23">
                <asp:TextBox runat="server" BorderColor="#CCCCCC" BorderWidth="1px" Font-Names="Verdana"
                    Font-Size="12px" Width="400px" ID="txtDept" ReadOnly="True"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style24">
                <asp:Label ID="Label1" runat="server" Font-Names="Verdana" Font-Bold="True" ForeColor="#666666" Font-Size="12px" Text="Employee Number"></asp:Label>
            </td>
            <td class="style24">
                <asp:TextBox ID="txtEmpID" runat="server" Width="150px" Font-Names="Verdana" Font-Size="12px" ForeColor="#666666"
                    BorderColor="#CCCCCC" BorderWidth="1px" Enabled="False" ReadOnly="True"></asp:TextBox>
            </td>
            <td class="style24">
                <asp:Label ID="Label6" runat="server" Font-Names="Verdana" Font-Bold="True" ForeColor="#666666" Font-Size="12px" Text="Location"></asp:Label>
            </td>
            <td class="style25">
                <asp:TextBox runat="server" BorderColor="#CCCCCC" BorderWidth="1px" Font-Names="Verdana" ForeColor="#666666"
                    Font-Size="12px" Width="400px" ID="txtLocation" ReadOnly="True"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style26">
                <asp:Label ID="Label2" runat="server" Font-Names="Verdana" Font-Bold="True" ForeColor="#666666" Font-Size="12px" Text="Employee Name"></asp:Label>
            </td>
            <td class="style26">
                <asp:TextBox runat="server" BorderColor="#CCCCCC" BorderWidth="1px" Font-Names="Verdana" ForeColor="#666666"
                    Font-Size="12px" Width="400px" ID="txtName" Enabled="False" 
                    ReadOnly="True"></asp:TextBox>
            </td>
            <td class="style26">
                <asp:Label ID="Label7" runat="server" Font-Names="Verdana" Font-Bold="True" 
                    ForeColor="#666666" Font-Size="12px" Text="Time in Present Position"></asp:Label>
            </td>
            <td class="style27">
                <asp:TextBox runat="server" BorderColor="#CCCCCC" BorderWidth="1px" Font-Names="Verdana" ForeColor="#666666"
                    Font-Size="12px" Width="400px" ID="txtPresentPos" ReadOnly="True"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style22">
                <asp:Label ID="Label4" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666" Text="Job Title"></asp:Label>
            </td>
            <td class="style22">
                <asp:TextBox runat="server" BorderColor="#CCCCCC" BorderWidth="1px" Font-Names="Verdana" ForeColor="#666666"
                    Font-Size="12px" Width="400px" ID="txtJobTitle" ReadOnly="True" 
                    Enabled="False"></asp:TextBox>
            </td>
            <td class="style22">
                <asp:Label ID="Label3" runat="server" Font-Names="Verdana" Font-Size="12px" 
                    Font-Bold="True" ForeColor="#666666" Text="Length of Service"></asp:Label>
            </td>
            <td class="style23">
                <asp:TextBox runat="server" BorderColor="#CCCCCC" BorderWidth="1px" Font-Names="Verdana" ForeColor="#666666"
                    Font-Size="12px" Width="400px" ID="txtLengthOfService" ReadOnly="True"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style30">
                <asp:Label ID="Label12" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666" Text="Reviewer"></asp:Label>
            </td>
            <td class="style30">
                <asp:TextBox runat="server" BorderColor="#CCCCCC" BorderWidth="1px" Font-Names="Verdana" ForeColor="#666666"
                    Font-Size="12px" Width="400px" ID="txtreviewer" ReadOnly="True" 
                    Enabled="False"></asp:TextBox>
            </td>
            <td class="style30">
                <asp:Label ID="Label21" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                    Text="Reviewer II"></asp:Label>
            </td>
            <td class="style31">
                <asp:TextBox runat="server" BorderColor="#CCCCCC" BorderWidth="1px" Font-Names="Verdana" ForeColor="#666666"
                    Font-Size="12px" Width="400px" ID="txtreviewerII" ReadOnly="True" 
                    Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style24">
                <asp:Label ID="Label9" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                    Text="Reviewer Point Average"></asp:Label>
            </td>
            <td class="style24">
                <asp:Label ID="lblReviewerPoint" runat="server" Font-Names="Verdana"  ForeColor="#666666"
                    Font-Size="12px"></asp:Label>
            </td>
            <td class="style24">
                <asp:Label ID="Label22" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                    Text="Reviewer II Point Average"></asp:Label>
            </td>
            <td class="style25">
                <asp:Label ID="lblReviewerIIPoint" runat="server" Font-Names="Verdana" ForeColor="#666666"
                    Font-Size="12px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style24">
                <asp:Label ID="Label13" runat="server" Font-Names="Verdana" Font-Size="12px" 
                    Font-Bold="True" ForeColor="#666666"
                    Text="Reviewer Recommendation"></asp:Label>
            </td>
            <td class="style24">
                <asp:Label ID="lblrecommendationI" runat="server" Font-Names="Verdana" ForeColor="#666666"
                    Font-Size="12px"></asp:Label>
            </td>
            <td class="style24">
                <asp:Label ID="Label18" runat="server" Font-Names="Verdana" Font-Bold="True" ForeColor="#666666" Font-Size="12px">Reviewer II Recommendation</asp:Label>
            </td>
            <td class="style25">
                <asp:Label ID="lblrecommendationII" runat="server" Font-Names="Verdana" ForeColor="#666666"
                    Font-Size="12px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style24">
                <asp:Label ID="Label23" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                    Text="Over all Remark"></asp:Label>
            </td>
            <td class="style24"> lbloverdesc lblRevieweePoint
                <asp:Label ID="lbloverdesc" runat="server" Font-Names="Verdana" ForeColor="#666666"
                    Font-Size="12px"></asp:Label>
            </td>
            <td class="style24">
                <asp:Label ID="Label20" runat="server" Font-Names="Verdana" Font-Bold="True" ForeColor="#666666" Font-Size="12px">Reviewee Point Average</asp:Label>
            </td>
            <td class="style25">
                <asp:Label ID="lblRevieweePoint" runat="server" Font-Names="Verdana" ForeColor="#666666"
                    Font-Size="12px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style24">
                <asp:Label ID="Label15" runat="server" Font-Names="Verdana" Font-Size="12px" 
                    Font-Bold="True" ForeColor="#666666"
                    Text="Overall Point"></asp:Label>
            </td>
            <td class="style24">lbloverallpoint lblscore
                <asp:Label ID="lbloverallpoint" runat="server" Font-Names="Verdana" ForeColor="#666666"
                    Font-Size="12px"></asp:Label>
            </td>
            <td class="style24">
                <asp:Label ID="Label16" runat="server" Font-Names="Verdana" Font-Bold="True" ForeColor="#666666" Font-Size="12px">Overall Score (%)</asp:Label>
            </td>
            <td class="style25">
                <asp:Label ID="lblscore" runat="server" Font-Names="Verdana" ForeColor="#666666" Font-Size="12px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style24" valign="top">
                <asp:Label ID="Label14" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                    Text="HR Recommendation"></asp:Label>
            </td>
            <td class="style24">
                <asp:TextBox runat="server" BorderColor="#CCCCCC" BorderWidth="1px" Font-Names="Verdana" ForeColor="#666666"
                    Font-Size="12px" Width="400px" ID="txtHRRecommendation" Height="100px" 
                    TextMode="MultiLine"></asp:TextBox>
            </td>
            <td class="style24" valign="top" >
                <asp:Label ID="Label24" runat="server" Font-Names="Verdana" Font-Bold="True" 
                    ForeColor="#666666" Font-Size="12px">Adjusted Overall Score (%)</asp:Label>
            </td>
            <td class="style25" valign="top">
                <asp:TextBox runat="server" BorderColor="#CCCCCC" BorderWidth="1px" 
                    Font-Names="Verdana" ForeColor="#666666"
                    Font-Size="12px" Width="80px" ID="txtadjustedscore" ReadOnly="True" 
                    Enabled="False" ToolTip="adjust the final overall score"></asp:TextBox>
                <asp:Label ID="lblid" runat="server" Font-Names="Verdana" Font-Size="12px" Visible="False"></asp:Label>
                <asp:Label ID="lblQuestCount" runat="server" Font-Names="Verdana" Font-Size="12px"
                    Visible="False"></asp:Label>
                <asp:Label ID="lblQuestID" runat="server" Font-Names="Verdana" Font-Size="12px" Visible="False"></asp:Label>
                <asp:Label ID="lblend" runat="server" Font-Names="Verdana" Font-Size="12px" Visible="False"></asp:Label>
                <asp:Label ID="lblreviewerII" runat="server" Font-Names="Verdana" Font-Size="12px"
                    Visible="False"></asp:Label>
                <asp:Label ID="lblreviewer" runat="server" Font-Names="Verdana" Font-Size="12px"
                    Visible="False"></asp:Label>
            </td>
        </tr>
        </table>--%>
       
            <%--table >
                <tr>btnClose btnPromote btn360degree
                    <td>
                        <asp:Button ID="btnClose" runat="server" BackColor="#1BA691" 
                BorderStyle="None" ForeColor="White"
                                Height="20px" Text="Save Record" Width="150px" 
                            Font-Names="Verdana" Font-Size="11px" Font-Bold="True" />
                    </td>
                    <td>
                        <asp:Button ID="btnPromote" runat="server" BackColor="#3399FF" 
                BorderStyle="None" ForeColor="White"
                                Height="20px" Text="Promote Staff" Width="150px" Font-Names="Verdana" 
                            Font-Size="11px" Font-Bold="True" />
                    </td>
                    <td>
                            <asp:Button ID="btn360degree" runat="server" Text="360 Degree Feedback" BackColor="#FF9933"
                                ForeColor="White" Width="150px" Height="20px" BorderStyle="None" Style="margin-top: 0px"
                                Font-Bold="True" Font-Names="Verdana" Font-Size="11px" 
                                ToolTip="select reviewers for your 360 degree feedback"
                              />
                    </td>
                </tr>
            </table>--%>
                            
        <div class="row">
                <telerik:RadGrid ID="gridFeedback" runat="server" 
                AllowMultiRowSelection="True" AllowPaging="True" CssClass="table table-condensed"
                    AllowSorting="True" AutoGenerateColumns="False" BorderWidth="1px" BorderColor="#CCCCCC" DataKeyNames="ID" EnableGroupsExpandAll="True"
                    Font-Names="Verdana" Font-Size="10px" GridLines="Both" GroupPanelPosition="Top"
                    PageSize="20" RenderMode="Lightweight" ResolvedRenderMode="Classic" ShowFooter="True"
                    ShowGroupPanel="True" ShowStatusBar="True" Width="100%">
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
                            <telerik:GridBoundColumn DataField="KPIObjectives" HeaderButtonType="TextButton" UniqueName="kpiobjectives" HeaderText="KPI Objectives">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="EMpIDComment" HeaderButtonType="TextButton" UniqueName="empidcomment"
                                HeaderText="Reviewee Comment" >
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="EMpRatingDesc" HeaderButtonType="TextButton"
                                 HeaderText="Rating">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="EMpIDRating" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                HeaderText="Points" ItemStyle-HorizontalAlign="Right" 
                                ItemStyle-Width="50px">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="supervisorcomment" HeaderButtonType="TextButton" UniqueName="supervisorcomment"
                                 HeaderText="Reviewer Comment">
                            </telerik:GridBoundColumn>
                            
                            <telerik:GridBoundColumn DataField="mgrRatingDesc" HeaderButtonType="TextButton" HeaderText="Rating">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="mgrIDRating" HeaderButtonType="TextButton"
                                HeaderText="Points" ItemStyle-HorizontalAlign="Right">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="supervisorcomment2" HeaderButtonType="TextButton" UniqueName="supervisorcomment2" HeaderText="Reviewer II Comment">
                            </telerik:GridBoundColumn>
                            
                            <telerik:GridBoundColumn DataField="mgrRatingDesc2" HeaderButtonType="TextButton"
                               HeaderText="Rating">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="mgrIDRating2" HeaderButtonType="TextButton"
                                HeaderText="Points" ItemStyle-HorizontalAlign="Right">
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings AllowColumnsReorder="True" AllowDragToGroup="True" 
                        ReorderColumnsOnClient="True">
                        <Resizing AllowColumnResize="True" AllowRowResize="True" EnableRealTimeResize="True"
                            ResizeGridOnColumnResize="False" />
                    </ClientSettings>
                    <GroupingSettings ShowUnGroupButton="true" />
                    <FilterMenu RenderMode="Lightweight">
                    </FilterMenu>
                    <HeaderContextMenu RenderMode="Lightweight">
                    </HeaderContextMenu>
                </telerik:RadGrid>
             <script type="text/javascript">
                 function openWindow(code) {
                     window.open("CompetencyUpdates.aspx?id=" + code, "open_window", "width=800,height=800");
                 }
                </script>
        </div>
        <div style="height: 163px">
            <div>
                <asp:Label ID="lblHang0" runat="server" Style="color: #FFFFFF"></asp:Label>
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
    <style type="text/css">
        .style21
        {
            width: 100%;
        }
        </style>
</asp:Content>