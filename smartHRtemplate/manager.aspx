<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/smartHR.Master" CodeBehind="manager.aspx.vb" Inherits="GOSHRM.test1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<%--<div class="tcontainer">
<iframe src="manager1.aspx"  scrolling="no" style="width: 100%; height:2500px;overflow: hidden;  border:0;" ></iframe></div>--%>
<div class="container col-md-12">
    <div class="tabs_wrapper" style="background: #fff; padding: 1rem; margin: 0 0.6rem 5px 0.6rem">
            <ul class="link_wrapper">
                <li>
                    <a href="<%= Page.ResolveClientUrl("~/empdashboard")%>">Employee Dashboard</a>
                </li>
                <li style="display:none" class="link_manager">
                    <a href="<%= Page.ResolveClientUrl("~/manager")%>" class="links" style="border-bottom: 2px solid teal; padding-bottom: 5px">Manager Dashboard</a>
                </li>
               <li style="display:none" class="link_hr">
                    <a href="<%= Page.ResolveClientUrl("~/hrdashboard2")%>">HR Dashboard</a>
                </li>
                <li style="display:none" class="link_hr">
                    <a href="<%= Page.ResolveClientUrl("~/hrdashboard")%>">HR Analytics</a>
                </li>
            </ul>
        </div>
         <div class="panel panel-success">
                                <div class="panel-heading">
                                    <h4><b>Manager Dashboard</b></h4>
                                </div>
                                <div class="panel-body">

					<div class="card-box m-b-0">
						<div class="row">
							<div class="col-md-12">
											<div class="col-md-6">                                           
                                            <div class="card-box"> 
                                            <div>Team Information</div>                                          
												<div class="profile-info-left">											
													<div class="pro-deadline m-b-15">
													<div class="sub-title card-box">
														Team Count <span id="Gcount" runat="server" class="text-muted"></span>
													</div>
												</div>
												<div class="project-members m-b-15 card-box">
													<div>Sex :</div>
													<ul class="team-members">
														<li>
															<a href="#" data-toggle="tooltip" title="Male <%=male %>"><img src="images/male-avatar.png" alt="Male"></a>
														</li>
														<li>
															<a href="#" data-toggle="tooltip" title="Female <%=female %>"><img src="images/female-avatar.png" alt="Female"></a>
														</li>
													</ul>
												</div>
												
												<div class="project-members m-b-15 card-box">
													<div>Distribution of Team</div>
 
                                            <div class="experience-box">
                                                <ul id="distribute" runat="server" class="experience-list">
                                                    <li>
                                                        <div class="experience-user">
                                                            <div class="before-circle"></div>
                                                        </div>
                                                        <div class="experience-content">
                                                            <div class="timeline-content">
                                                                <a href="#/" class="name">No Direct Report</a>
                                                                <%--<div>Bsc Computer Science</div>--%>
                                                            </div>
                                                        </div>
                                                    </li>
                                                </ul>
                                            </div>
 
												</div>
												
													<div class="project-members m-b-15">
													<div>Team Birthday</div>
											<div class="card-box">
                                            <div class="experience-box">
                                                <ul id="birthday" runat="server" class="experience-list">
                                                    <li>
                                                        <div class="experience-user">
                                                            <div class="before-circle"></div>
                                                        </div>
                                                        <div class="experience-content">
                                                            <div class="timeline-content">
                                                                <a href="#/" class="name">No Birthdays Today</a>
                                                                <%--<div>2nd August 1980</div>--%>
                                                            </div>
                                                        </div>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
												</div>
											</div></div>
											</div>
											<div class="col-md-6">
                                            <div class="card-box">
													<div>Top Performance</div>
									<div>
                                        <div class="activity">
                                            <div class="activity-box">
                                                <ul id="top_perform" runat="server" class="activity-list">
                                                    <li>
                                                       <%-- <div class="activity-user">
                                                            <a href="profile.html" title="Lesley Grauer" data-toggle="tooltip" class="avatar">
                                                                <img alt="Lesley Grauer" src="images/user.jpg" class="img-responsive img-circle">
                                                            </a>
                                                        </div>--%>
                                                        <div class="activity-content">
                                                            <div class="timeline-content">
                                                                <a href="profile.html" class="name">No top performer
                                                                <%--<span class="time">96%</span>--%>
                                                            </div>
                                                        </div>
                                                    </li>                                                   
                                                </ul>
                                            </div>
                                        </div>
                                    </div> </div>
                                      <div class="card-box">
									  <div>Team Leave</div>                                     
									  <div class="table-responsive">
                                        <table class="table table-striped custom-table m-b-0">
                                            <thead>
                                                <tr>
                                                    <th class="col-md-3">Name</th>
                                                    <th class="col-md-3">Leave Type</th>
                                                    <th class="col-md-3">Start Date</th>
                                                    <th class="col-md-3">End Date</th>
                                                </tr>
                                            </thead>
                                            <tbody id="leavetable" runat="server">
                                                <tr>                                                   
                                                    <td>
                                                        <small class="block text-ellipsis">
                                                            <span class="text-xs">Mike</span>
                                                        </small>
                                                    </td>
                                                     <td class="text">
                                                        <small class="block text-ellipsis">
                                                            <span class="text-xs">Tiredness</span>
                                                        </small>
                                                    </td>
                                                    <td class="text">
                                                        <small class="block text-ellipsis">
                                                            <span class="text-xs">22th Angust 2018</span>
                                                        </small>
                                                    </td>
                                                      <td class="text">
                                                        <small class="block text-ellipsis">
                                                            <span class="text-xs">31st Angust 2018</span>
                                                        </small>
                                                    </td>
                                                </tr>				
                                            </tbody>
                                        </table>
                                    </div></div>
								</div>
							</div>
							</div>
						</div>
                    <div class="row">
			<%--		<div style="display: none" class="card-box tab-box">
                        <div class="row user-tabs">
                            <div class="col-md-12 col-sm-12 col-xs-12 line-tabs">
                                <ul class="nav nav-tabs tabs nav-tabs-bottom">
                                    <li class="active col-sm-4"><a data-toggle="tab" href="#myprojects">My Projects</a></li>
                                    <li class="col-sm-4"><a data-toggle="tab" href="#tasks">Tasks</a></li>
                                    <li class="col-sm-4"><a data-toggle="tab" href="#analytics">Analytics</a></li>
                                </ul>
                            </div>
                        </div>
                    </div>--%>
                    
                    <div class="card-box tab-box" style="">
                        <div class="row user-tabs" >
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 line-tabs">
                                <ul class="nav nav-tabs tabs nav-tabs-bottom">
                                    <li class="active col-md-4"><a data-toggle="tab" href="#myprojects">My Team</a></li>
                                    <li class="col-md-4"><a data-toggle="tab" href="#tasks">Requests and Approvals</a></li>
                                    <li class="col-sm-4"><a data-toggle="tab" href="#analytics">Analytics</a></li>
                                </ul>
                            </div>
                        </div>
                    </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="tab-content  profile-tab-content">
                        <div id="myprojects" class="tab-pane fade in active">
                        <div class="project-task">
                        <div class="row" style="margin: 0 5px">
                            <div class="col-md-12">
                         <div id="mTeam" runat="server" class="row">
                          
                            </div>
                             </div>
                              </div>
                             </div>
                             </div>
                        <div id="tasks" class="tab-pane fade">
                            <div class="project-task">
                                <div class="tabbable">
                                    <ul class="nav nav-tabs nav-tabs-top nav-justified m-b-0">
                                        <li class="active"><a href="#all_tasks" data-toggle="tab" aria-expanded="true">Approval</a></li>
                                        <li><a href="#pending_tasks" data-toggle="tab" aria-expanded="false">Planing and Request</a></li>
                                    </ul>
                                    <div class="tab-content">
                                        <div class="tab-pane active" id="all_tasks">
                                            <div class="task-wrapper">
                                                    <a href="<%= Page.ResolveClientUrl("~/Module/Finance/Loans/LoansApproval")%>"><div class="col-md-3 col-sm-3 col-md-2">
                                                        <div class="dash-widget clearfix card-box">
                                                            <span class="dash-widget-icon"><i class="fa fa-money" aria-hidden="true" style="color: #ff6d00; margin-top: 12px"></i></span>
                                                            <div class="dash-widget-info">
                                                                <h3 id="Aloan" runat="server"></h3>
                                                                <span>Loan</span>
                                                            </div>
                                                        </div>
                                                    </div></a>
                                                    <a href="<%= Page.ResolveClientUrl("~/Module/Employee/LeaveManagement/LeaveRoster")%>"><div class="col-md-3 col-sm-3 col-md-2">
                                                        <div class="dash-widget clearfix card-box">
                                                            <span class="dash-widget-icon"><i class="fa fa-calendar" aria-hidden="true" style="color: #ff6d00; margin-top: 12px"></i></span>
                                                            <div class="dash-widget-info">
                                                                <h3  id="ALeave" runat="server"></h3>
                                                                <span>Leave</span>
                                                            </div>
                                                        </div>
                                                    </div></a> 
                                                    <a href="<%= Page.ResolveClientUrl("~/Module/Employee/Performance/CoacheeDevelopmentPlan")%>"><div class="col-md-3 col-sm-3 col-md-2">
                                                        <div class="dash-widget clearfix card-box">
                                                            <span class="dash-widget-icon"><i class="fa fa-edit" aria-hidden="true" style="color: #ff6d00; margin-top: 12px"></i></span>
                                                            <div class="dash-widget-info">
                                                                <h3 id="ADev" runat="server"></h3>
                                                                <span>Development Plan</span>
                                                            </div>
                                                        </div>
                                                    </div></a> 
                                                    <a href="<%= Page.ResolveClientUrl("~/Module/Employee/TrainingPortal/ApprovalTrainings")%>"><div class="col-md-3 col-sm-3 col-md-2">
                                                        <div class="dash-widget clearfix card-box">
                                                            <span class="dash-widget-icon"><i class="fa fa-graduation-cap" aria-hidden="true" style="color: #ff6d00; margin-top: 12px"></i></span>
                                                            <div class="dash-widget-info">
                                                                <h3 id="ATrain" runat="server">6</h3>
                                                                <span>Training</span>
                                                            </div>
                                                        </div>
                                                    </div></a> 
                                                    <a href="<%= Page.ResolveClientUrl("~/Module/Employee/Performance/CoacheeAppraisalObjectives")%>"><div class="col-md-3 col-sm-3 col-md-2">
                                                        <div class="dash-widget clearfix card-box">
                                                            <span class="dash-widget-icon"><i class="fa fa-tachometer" aria-hidden="true" style="color: #ff6d00; margin-top: 12px"></i></span>
                                                            <div class="dash-widget-info">
                                                                <h3 id="APer" runat="server">10</h3>
                                                                <span>Performance Objective</span>
                                                            </div>
                                                        </div>
                                                    </div></a> 
                                                    <a href="<%= Page.ResolveClientUrl("~/Module/Employee/Performance/MgrQueries")%>"><div class="col-md-3 col-sm-3 col-md-2">
                                                        <div class="dash-widget clearfix card-box">
                                                            <span class="dash-widget-icon"><i class="fa fa-tachometer" aria-hidden="true" style="color: #ff6d00; margin-top: 12px"></i></span>
                                                            <div class="dash-widget-info">
                                                                <h3 id="APerFeed" runat="server"></h3>
                                                                <span>Performance Feedback</span>
                                                            </div>
                                                        </div>
                                                    </div></a>
                                                    <a href="<%= Page.ResolveClientUrl("~/Module/Employee/Recruitment/PromotionsApproval")%>"><div class="col-md-3 col-sm-3 col-md-2">
                                                        <div class="dash-widget clearfix card-box">
                                                            <span class="dash-widget-icon"><i class="fa fa-check-square-o" aria-hidden="true" style="color: #ff6d00; margin-top: 12px"></i></span>
                                                            <div class="dash-widget-info">
                                                                <h3 id="APro" runat="server"></h3>
                                                                <span>Promotion</span>
                                                            </div>
                                                        </div>
                                                    </div></a> 
                                                    <a href="<%= Page.ResolveClientUrl("~/Module/Employee/Recruitment/ExitApprovals")%>"><div class="col-md-3 col-sm-3 col-md-2">
                                                        <div class="dash-widget clearfix card-box">
                                                            <span class="dash-widget-icon"><i class="fa fa-external-link" aria-hidden="true" style="color: #ff6d00; margin-top: 12px"></i></span>
                                                            <div class="dash-widget-info">
                                                                <h3 id="AJob" runat="server"></h3>
                                                                <span>Job Exit</span>
                                                            </div>
                                                        </div>
                                                    </div></a>
                                                    <a href="<%= Page.ResolveClientUrl("~/Module/Employee/Recruitment/WorkForcePlanning")%>"><div class="col-md-3 col-sm-3 col-md-2">
                                                        <div class="dash-widget clearfix card-box">
                                                            <span class="dash-widget-icon"><i class="fa fa-bar-chart-o" aria-hidden="true" style="color: #ff6d00; margin-top: 12px"></i></span>
                                                            <div class="dash-widget-info">
                                                                <h3 id="AWork" runat="server"></h3>
                                                                <span>WorkForce Planning</span>
                                                            </div>
                                                        </div>
                                                    </div></a> 
                                                    <a href="<%= Page.ResolveClientUrl("~/Module/Employee/Recruitment/SuccessionPlansApprove")%>"><div class="col-md-3 col-sm-3 col-md-2">
                                                        <div class="dash-widget clearfix card-box">
                                                            <span class="dash-widget-icon"><i class="fa fa-sign-out" aria-hidden="true" style="color: #ff6d00; margin-top: 12px"></i></span>
                                                            <div class="dash-widget-info">
                                                                <h3 id="ASucc" runat="server"></h3>
                                                                <span>Succession Plan</span>
                                                            </div>
                                                        </div>
                                                    </div></a>
                                                    <a href="<%= Page.ResolveClientUrl("~/Module/Employee/Recruitment/StaffConfirmation")%>"><div class="col-md-3 col-sm-3 col-md-2">
                                                        <div class="dash-widget clearfix card-box">
                                                            <span class="dash-widget-icon"><i class="fa fa-thumbs-o-up" aria-hidden="true" style="color: #ff6d00; margin-top: 12px"></i></span>
                                                            <div class="dash-widget-info">
                                                                <h3 id="EmpCon" runat="server"></h3>
                                                                <span>Employee Confirmation</span>
                                                            </div>
                                                        </div>
                                                    </div></a> 
                                                    <a href="<%= Page.ResolveClientUrl("~/Module/Employee/TimeManagement/TeamAttendanceCalendar")%>"><div class="col-md-3 col-sm-3 col-md-2">
                                                        <div class="dash-widget clearfix card-box">
                                                            <span class="dash-widget-icon"><i class="fa fa-newspaper-o" aria-hidden="true" style="color: #ff6d00; margin-top: 12px"></i></span>
                                                            <div class="dash-widget-info">
                                                                <h3 id="EmpAttend" runat="server"></h3>
                                                                <span>Attendance</span>
                                                            </div>
                                                        </div>
                                                    </div></a> 
                                                    <a href="<%= Page.ResolveClientUrl("~/Module/Employee/TimeManagement/TimeSheetManager")%>"><div class="col-md-3 col-sm-3 col-md-2">
                                                        <div class="dash-widget clearfix card-box">
                                                            <span class="dash-widget-icon"><i class="fa fa-history" aria-hidden="true" style="color: #ff6d00; margin-top: 12px"></i></span>
                                                            <div class="dash-widget-info">
                                                                <h3 id="Emptime" runat="server"></h3>
                                                                <span>TimeSheet</span>
                                                            </div>
                                                        </div>
                                                    </div></a> 
                                            </div>
                                        </div>
                                        <div class="tab-pane" id="pending_tasks">
                                            <div class="task-wrapper">
                                                    <a href="<%= Page.ResolveClientUrl("~/Module/Employee/Recruitment/PromotionsInitiated")%>"><div class="col-md-3 col-sm-3 col-md-2">
                                                        <div class="dash-widget clearfix card-box">
                                                            <span class="dash-widget-icon"><i class="fa fa-check-square-o" aria-hidden="true" style="color: #ff6d00; margin-top: 12px"></i></span>
                                                            <div class="dash-widget-info">
                                                                <h3 id="RPro" runat="server"></h3>
                                                                <span>Promotion</span>
                                                            </div>
                                                        </div>
                                                    </div></a>
                                                    <a href="<%= Page.ResolveClientUrl("~/Module/Employee/Recruitment/StaffRequisition")%>"><div class="col-md-3 col-sm-3 col-md-2">
                                                        <div class="dash-widget clearfix card-box">
                                                            <span class="dash-widget-icon"><i class="fa fa-users" aria-hidden="true" style="color: #ff6d00; margin-top: 12px"></i></span>
                                                            <div class="dash-widget-info">
                                                                <h3 id="RStaff" runat="server"></h3>
                                                                <span>Staff Requisition</span>
                                                            </div>
                                                        </div>
                                                    </div></a> 
                                                    <a href="<%= Page.ResolveClientUrl("~/Module/Employee/Recruitment/WorkForce")%>"><div class="col-md-3 col-sm-3 col-md-2">
                                                        <div class="dash-widget clearfix card-box">
                                                            <span class="dash-widget-icon"><i class="fa fa-bar-chart-o" aria-hidden="true" style="color: #ff6d00; margin-top: 12px"></i></span>
                                                            <div class="dash-widget-info">
                                                                <h3 id="RWork" runat="server"></h3>
                                                                <span>WorkForce Planning</span>
                                                            </div>
                                                        </div>
                                                    </div></a> 
                                                    <a href="<%= Page.ResolveClientUrl("~/Module/Employee/Recruitment/SuccessionPlans")%>"><div class="col-md-3 col-sm-3 col-md-2">
                                                        <div class="dash-widget clearfix card-box">
                                                            <span class="dash-widget-icon"><i class="fa fa-sign-out" aria-hidden="true" style="color: #ff6d00; margin-top: 12px"></i></span>
                                                            <div class="dash-widget-info">
                                                                <h3 id="RSucc" runat="server"></h3>
                                                                <span>Succession Plan</span>
                                                            </div>
                                                        </div>
                                                    </div></a> 
                                                     <a href="<%= Page.ResolveClientUrl("~/Module/Employee/Performance/MgrQueries")%>"><div class="col-md-3 col-sm-3 col-md-2">
                                                        <div class="dash-widget clearfix card-box">
                                                            <span class="dash-widget-icon"><i class="fa fa-user-times" aria-hidden="true" style="color: #ff6d00; margin-top: 12px"></i></span>
                                                            <div class="dash-widget-info">
                                                                <h3 id="ADis" runat="server"></h3>
                                                                <span>Discipline</span>
                                                            </div>
                                                        </div>
                                                    </div></a>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <div id="analytics" class="tab-pane fade in active">
                        <div class="project-task">
                        <div class="row" style="margin: 0 5px">
                            <div class="col-md-12">
                                <h4>Analytics</h4>
                                <div class="row">
                                    <div class="col-sm-6 text-center">
                                        <div class="card-box">
                                         <h3>Completion Status Analysis</h3>
                                         <canvas id="pie-chart" width="800" height="450"></canvas>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 text-center">
                                        <div class="card-box">
                                        <h3>Performance Analysis</h3>
                                         <canvas id="doughnut-chart" width="800" height="450"></canvas>
                                        </div>
                                    </div>
                                </div>
                           
                            </div>
                        </div>
                        </div>
                       </div>
                        </div>

                    </div>
                </div>
            </div>
            </div>
            </div>  
            </div>
            <script type="text/javascript" src="js/Chart.min.js"></script>
            <script>

  
               new Chart(document.getElementById("doughnut-chart"), {
                type: 'doughnut',
                data: {
                    labels: [<%=PerformanceName%>],
                    datasets: [{
                        label: "Score for current year",
                        backgroundColor: ["#0250f7", "#f7020e", "#f7c202", "#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850", "#0250f7", "#f7020e"],
                        data: [<%=PerformanceNameScore%>],
                    }]
                },
                options: {
                    legend: { display: false },
                    title: {
                        display: false,
                        text: 'Performance Analysis'
                    }
                }
            });




               new Chart(document.getElementById("pie-chart"), {
                type: 'pie',
                data: {
                    labels: [<%=CompletionStatusName%>],
                    datasets: [{
                        label: "Score for current year",
                        backgroundColor: ["#0250f7", "#f7020e", "#f7c202", "#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850", "#0250f7", "#f7020e"],
                        data: [<%=CompletionStatusNameScore%>],
                    }]
                },
                options: {
                    legend: { display: false },
                    title: {
                        display: false,
                        text: 'Completion Status Analysis'
                    }
                }
            });



             </script>

</asp:Content>
