<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/smartHR.Master" CodeBehind="HomeOld.aspx.vb" Inherits="GOSHRM.HomeOld" %>
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
									<h3>112</h3>
									<span>Training</span>
								</div>
							</div>
						</div>
						<div class="col-md-3 col-sm-3 col-md-2">
							<div class="dash-widget clearfix card-box">
							
								<span class="dash-widget-icon"><i class="fa fa-bed" aria-hidden="true"></i></span>
								<div class="dash-widget-info">
									<h3>112</h3>
									<span>Leave</span>
								</div>
							</div>
						</div>
						<div class="col-md-3 col-sm-3 col-md-2">
							<div class="dash-widget clearfix card-box">
								<span class="dash-widget-icon"><i class="fa fa-line-chart" aria-hidden="true"></i></span>
								<div class="dash-widget-info">
									<h3>112</h3>
									<span>Performance</span>
								</div>
							</div>
						</div>
						<div class="col-md-3 col-sm-3 col-md-2">
							<div class="dash-widget clearfix card-box">
								<span class="dash-widget-icon"><i class="fa fa-book" aria-hidden="true"></i></span>
								<div class="dash-widget-info">
									<h3>112</h3>
									<span>Library</span>
								</div>
							</div>
						</div>
						<div class="col-md-3 col-sm-3 col-md-2">
							<div class="dash-widget clearfix card-box">
								<span class="dash-widget-icon"><i class="fa fa-check" aria-hidden="true"></i></span>
								<div class="dash-widget-info">
									<h3>112</h3>
									<span>Confirmations</span>
								</div>
							</div>
						</div>
						<div class="col-md-3 col-sm-3 col-md-2">
							<div class="dash-widget clearfix card-box">
								<span class="dash-widget-icon"><i class="fa fa-file" aria-hidden="true"></i></span>
								<div class="dash-widget-info">
									<h3>112</h3>
									<span>Reviews</span>
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
													<h2 class="panel-title"><b>24th May 2018</b></h2>
												</div>												
													<div class="table-responsive">
														<table class="table table-striped custom-table m-b-0">
															<thead>
																<tr>
																	<th>Description</th>
																	<th>Date</th>
																	<th>Time</th>
																	<th>Participant</th>
																</tr>
															</thead>
															<tbody>
																<tr>
																	<td><a href="invoice-view.html">Project Meeting</a></td>
																	<td>
																		<h2><a href="#">Hazel Nutt</a></h2>
																	</td>
																	<td>8 Aug 2017</td>
																	<td>OluwaSeun Samuel</td>
																</tr>
																<tr>
																	<td><a href="invoice-view.html">Dept Meeting</a></td>
																	<td>
																		<h2><a href="#">Ben Dover</a></h2>
																	</td>
																	<td>30 Nov 2017</td>
																	<td>OluwaSeun Samuel</td>
																</tr>
															</tbody>
														</table>													
												</div>
												<div class="panel-footer">
													<a href="#" class="btn btn-primary rounded" data-toggle="modal" data-target="#add_event"><i class="fa fa-plus"></i> Add Event</a>
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
