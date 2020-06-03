<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/smartHR.Master" CodeBehind="Home.aspx.vb" Inherits="GOSHRM.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
						<div class="col-md-3 col-sm-3 col-md-2">
							<div class="dash-widget clearfix card-box">
								<span class="dash-widget-icon"><i class="fa fa-graduation-cap" aria-hidden="true"></i></span>								
								<div class="dash-widget-info">
									<h3><%=train%></h3>
									<span><a style="color:Black;" href="Module/Employee/TrainingPortal/Trainings.aspx">Training</a></span>
								</div>
							</div>
						</div>
						<div class="col-md-3 col-sm-3 col-md-2">
							<div class="dash-widget clearfix card-box">							
								<span class="dash-widget-icon"><i class="fa fa-bed" aria-hidden="true"></i></span>
								<div class="dash-widget-info">
									<h3><%=leave%></h3>
									<span><a style="color:Black;" href="Module/Employee/LeaveManagement/LeaveRoster.aspx">Leave</a></span>
								</div>
							</div>
						</div>
						<div class="col-md-3 cols-sm-3 col-md-2">
							<div class="dash-widget clearfix card-box">
								<span class="dash-widget-icon"><i class="fa fa-line-chart" aria-hidden="true"></i></span>
								<div class="dash-widget-info">
									<h3><%=per%></h3>
									<span><a style="color:Black;" href="Module/Employee/Performance/AppraisalFeedbackList.aspx">Performance</a></span>
								</div>
							</div>
						</div>
						<div class="col-md-3 col-sm-3 col-md-2">
							<div class="dash-widget clearfix card-box">
								<span class="dash-widget-icon"><i class="fa fa-book" aria-hidden="true"></i></span>
								<div class="dash-widget-info">
									<h3><%=job%></h3>
									<span><a style="color:Black;" href="Module/Recruitment/JobPostings.aspx">Active Jobs</a></span>
								</div>
							</div>
						</div>
						<div class="col-md-3 col-sm-3 col-md-2">
							<div class="dash-widget clearfix card-box">
								<span class="dash-widget-icon"><i class="fa fa-check" aria-hidden="true"></i></span>
								<div class="dash-widget-info">
									<h3><%=emp%></h3>
									<span><a style="color:Black;" href="Module/Employee/Employees.aspx">Employees</a></span>
								</div>
							</div>
						</div>
						<div class="col-md-3 col-sm-3 col-md-2">
							<div class="dash-widget clearfix card-box">
								<span class="dash-widget-icon"><i class="fa fa-file" aria-hidden="true"></i></span>
								<div class="dash-widget-info">
									<h3><%=inter%></h3>
									<span><a style="color:Black;" href="Module/Recruitment/JobInterviews.aspx">Interviews</a></span>
								</div>
							</div>
						</div>
					</div>
					<div class="row">
						<div class="col-md-12">
							<div class="row">
							<div class="col-md-4 col-sm-12 text-center">
									<div class="card-box">
										<div id="pie-chart" ></div>
									</div>
								</div>
								<div  class="col-md-8 col-sm-12 text-center card-box">
									<div class="">
										<div class="col-md-8">										
											<div class="panel panel-table">
												<div class="panel-heading">
													<h2 class="panel-title"><b><%=today%></b></h2>
												</div>												
													<div class="table-responsive">
														<table class="table table-striped custom-table m-b-0">
															<thead>
																<tr>
																	<th>Description</th>
																	<th>Date</th>
																	<th>Time</th>
																</tr>
															</thead>
															<tbody id="caldatas" runat="server">
																<tr>
																	<td><a href="invoice-view.html">Project Meeting</a></td>
																	<td>
																		<h2><a href="#">Hazel Nutt</a></h2>
																	</td>
																	<td>8 Aug 2017</td>
																</tr>
																<tr>
																	<td><a href="invoice-view.html">Dept Meeting</a></td>
																	<td>
																		<h2><a href="#">Ben Dover</a></h2>
																	</td>
																	<td>30 Nov 2017</td>
																</tr>
															</tbody>
														</table>													
												</div>
												<div class="panel-footer">
													<a href="events.aspx" class="btn btn-primary rounded"><i class="fa fa-plus"></i>Add Event</a>
												</div>
											</div>
										</div>
										<div class="col-md-4">
											<div class="panel panel-table">
												<div class="panel-heading">
													<h3 class="panel-title">Birthdays for Today</h3>
												</div>												
													<div class="table-responsive">
														<table class="table table-striped custom-table m-b-0">
															<tbody>
																<tr>
																	<th>1</th>
																	<th>Hassan Oludare</th>
																</tr>
																<tr>
																	<th>2</th>
																	<th>adfafafaaSeun Akinyelu</th>
																</tr>
																<tr>
																	<th>3</th>
																	<th>adfasdfafaSeun Akinyelu</th>
																</tr>
															</tbody>
														</table>													
												</div>
											</div>
										</div>
									</div>
								</div>																															
							</div>
						</div>
					</div>
					<%--<div class="row">
						<div class="col-md-12">
						<div class="row">
							<div  class="col-md-3 col-sm-12 text-center">
									<div class="card-box">
										<div id="bar-chart" ></div>
									</div>
								</div>
								<div class="col-md-3 col-sm-12 text-center">
									<div class="card-box">
										<div id="stacked" ></div>
									</div>
								</div>
								<div class="col-md-6 col-sm-12 text-center">
									<div class="card-box">
										<div id="line-chart" ></div>
									</div>
								</div>
						</div>
						</div>
					</div>--%>
</asp:Content>
