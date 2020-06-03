<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/smartHR.Master" CodeBehind="managers.aspx.vb" Inherits="GOSHRM.managers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<%--<div class="tcontainer">
<iframe src="manager1.aspx"  scrolling="no" style="width: 100%; height:2500px;overflow: hidden;  border:0;" ></iframe></div>--%>
					<div class="row">
						<div class="col-sm-8">
                        <h4 class="page-title">My Team</h4>
						</div>
					</div>
					<div class="card-box m-b-0">
						<div class="row">
							<div class="col-md-12">
								<div class="">

									<div class="">
										<div class="row">
											<div class="col-md-6">
												<div class="profile-info-left">											
													<div class="pro-deadline m-b-15">
													<div class="sub-title">
														My Team Count: <span id="Gcount" runat="server" class="text-muted"></span>
													</div>
												</div>
												<div class="project-members m-b-15">
													<div>Sex :</div>
													<ul class="team-members">
														<li>
															<a href="#" data-toggle="tooltip" title="Male"><img src="images/user.jpg" alt="Male"></a>
														</li>
														<li>
															<a href="#" data-toggle="tooltip" title="Female"><img src="images/user.jpg" alt="Female"></a>
														</li>
													</ul>
												</div>
												
												<div class="project-members m-b-15">
													<div>Distribution of Team :</div>
											<div class="card-box">
                                            <div class="experience-box">
                                                <ul id="distribute" runat="server" class="experience-list">
                                                    <li>
                                                        <div class="experience-user">
                                                            <div class="before-circle"></div>
                                                        </div>
                                                        <div class="experience-content">
                                                            <div class="timeline-content">
                                                                <a href="#/" class="name">International College of Arts and Science (UG)</a>
                                                                <div>Bsc Computer Science</div>
                                                            </div>
                                                        </div>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
												</div>
												
													<div class="project-members m-b-15">
													<div>Team Birthday :</div>
											<div class="card-box">
                                            <div class="experience-box">
                                                <ul class="experience-list">
                                                    <li>
                                                        <div class="experience-user">
                                                            <div class="before-circle"></div>
                                                        </div>
                                                        <div class="experience-content">
                                                            <div class="timeline-content">
                                                                <a href="#/" class="name">Ade</a>
                                                                <div>2nd August 1980</div>

                                                            </div>
                                                        </div>
                                                    </li>
                                                    <li>
                                                        <div class="experience-user">
                                                            <div class="before-circle"></div>
                                                        </div>
                                                        <div class="experience-content">
                                                            <div class="timeline-content">
                                                                <a href="#/" class="name">Yomi</a>
                                                                <div>20th June 1990</div>

                                                            </div>
                                                        </div>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
												</div>
											</div>
											</div>
											<div class="col-md-6">
													<div>Top Performance :</div>
									<div>
                                        <div class="activity">
                                            <div class="activity-box">
                                                <ul class="activity-list">
                                                    <li>
                                                        <div class="activity-user">
                                                            <a href="profile.html" title="Lesley Grauer" data-toggle="tooltip" class="avatar">
                                                                <img alt="Lesley Grauer" src="images/user.jpg" class="img-responsive img-circle">
                                                            </a>
                                                        </div>
                                                        <div class="activity-content">
                                                            <div class="timeline-content">
                                                                <a href="profile.html" class="name">Lesley Grauer
                                                                <span class="time">96%</span>
                                                            </div>
                                                        </div>
                                                    </li>
                                                    <li>
                                                        <div class="activity-user">
															<a href="profile.html" title="Jeffery Lalor" data-toggle="tooltip" class="avatar">
                                                                <img alt="Lesley Grauer" src="images/user.jpg" class="img-responsive img-circle">
                                                            </a>
                                                        </div>
                                                        <div class="activity-content">
                                                            <div class="timeline-content">
                                                                <a href="profile.html" class="name">Jeffery Lalor
                                                                <span class="time">88%</span>
                                                            </div>
                                                        </div>
                                                    </li>
                                                    <li>
                                                        <div class="activity-user">
                                                            <a href="profile.html" title="Catherine Manseau" data-toggle="tooltip" class="avatar">
                                                                <img alt="Catherine Manseau" src="images/user.jpg" class="img-responsive img-circle">
                                                            </a>
                                                        </div>
                                                        <div class="activity-content">
                                                            <div class="timeline-content">
                                                                <a href="profile.html" class="name">Catherine Manseau
                                                                <span class="time">76%</span>
                                                            </div>
                                                        </div>
                                                    </li>
                                       
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
													<div>Team Leave :</div>
									  <div class="table-responsive">
                                        <table class="table table-striped custom-table m-b-0">
                                            <thead>
                                                <tr>
                                                    <th class="col-md-4">Name</th>
                                                    <th class="col-md-4">StartDate</th>
                                                    <th class="col-md-4">EndDate</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>                                                   
                                                    <td>
                                                        <small class="block text-ellipsis">
                                                            <span class="text-xs">Mike</span>
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
												<tr>                                                   
                                                    <td>
                                                        <small class="block text-ellipsis">
                                                            <span class="text-xs">John</span>
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
												<tr>                                                   
                                                    <td>
                                                        <small class="block text-ellipsis">
                                                            <span class="text-xs">Jeffery</span>
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
                                    </div>
								</div>

							</div>
					</div>
								</div>
							</div>
							</div>
						</div>
					<div class="card-box tab-box">
						<div class="row user-tabs">
							<div class="col-lg-12 col-md-12 col-sm-12 line-tabs">
								<ul class="nav nav-tabs tabs nav-tabs-bottom">
									<li class="active col-sm-3"><a data-toggle="tab" href="#myprojects">My Projects</a></li>
									<li class="col-sm-3"><a data-toggle="tab" href="#tasks">Tasks</a></li>
									<li class="col-sm-3"><a data-toggle="tab" href="#analytics">Analytics</a></li>
								</ul>
							</div>
						</div>
					</div>
                    <div class="row">
                        <div class="col-lg-12">
							<div class="tab-content  profile-tab-content">
								<div id="myprojects" class="tab-pane fade in active">
								<div class="col-lg-3 col-sm-4">

											<div class="card-box project-box">
										<div class="profile-img">
											<a href="#"><img class="avatar" src="images/user.jpg" alt=""></a>
											<h5 class="user-name m-t-0">Global Technologies</h5>
										</div>
										<br>
										<br>
										
												<div class="project-members m-b-15">
												<ul class="personal-info">
													<li>
														<span class="title">Phone:</span>
														<span class="text"><a href="#">9876543210</a></span>
													</li>
													<li>
														<span class="title">Email:</span>
														<span class="text"><a href="#">barrycuda@example.com</a></span>
													</li>
													<li>
														<span class="title">Birthday:</span>
														<span class="text">2nd August</span>
													</li>
													<li>
														<span class="title">Gender:</span>
														<span class="text">Male</span>
													</li>
												</ul>

												</div>
											
                                    <div class="table-responsive">
                                        <table class="table table-striped custom-table m-b-0">
                                            <thead>
                                                <tr>
                                                    <th class="col-md-4">My Training</th>
                                                    <th class="col-md-4">My Competencies</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>                                                   
                                                    <td class="text">
                                                        <small class="block text-ellipsis">
                                                            <span class="text-muted">open tasks</span>
                                                        </small>
                                                    </td>
                                                    <td style="width:20px">
                                                     	<ul class="team-members">
														<li>
															<a href="#" data-toggle="tooltip" title="John Doe"><img src="images/user.jpg" alt="John Doe"></a>
														</li>
														<li>
															<a href="#" data-toggle="tooltip" title="Richard Miles"><img src="images/user.jpg" alt="Richard Miles"></a>
														</li>
														<li>
															<a href="#" data-toggle="tooltip" title="John Smith"><img src="images/user.jpg" alt="John Smith"></a>
														</li>
														
														<li>
															<a href="#" class="all-users">+15</a>
														</li>
													</ul>
                                                    </td>
                                                </tr>
                                                <tr>                                                   
                                                    <td class="text">
                                                        <small class="block text-ellipsis">
                                                            <span class="text-muted">open tasks</span>
                                                        </small>
                                                    </td>
                                                    <td style="width:20px">
                                                       	<ul class="team-members">
														<li>
															<a href="#" data-toggle="tooltip" title="John Doe"><img src="images/user.jpg" alt="John Doe"></a>
														</li>
														<li>
															<a href="#" data-toggle="tooltip" title="Richard Miles"><img src="images/user.jpg" alt="Richard Miles"></a>
														</li>
														<li>
															<a href="#" data-toggle="tooltip" title="John Smith"><img src="images/user.jpg" alt="John Smith"></a>
														</li>
														<li>
															<a href="#" class="all-users">+15</a>
														</li>
													</ul>
                                                    </td>
                                                </tr>

                                            </tbody>
                                        </table>
                                    </div>
											
											</div>
										</div>
								<div class="col-lg-3 col-sm-4">
								<div class="card-box project-box">
										<div class="profile-img">
											<a href="#"><img class="avatar" src="images/user.jpg" alt=""></a>
											<h5 class="user-name m-t-0">Global Technologies</h5>
										</div>
										<br>
										<br>
										
												<div class="project-members m-b-15">
												<ul class="personal-info">
													<li>
														<span class="title">Phone:</span>
														<span class="text"><a href="#">9876543210</a></span>
													</li>
													<li>
														<span class="title">Email:</span>
														<span class="text"><a href="#">barrycuda@example.com</a></span>
													</li>
													<li>
														<span class="title">Birthday:</span>
														<span class="text">2nd August</span>
													</li>
													<li>
														<span class="title">Gender:</span>
														<span class="text">Male</span>
													</li>
												</ul>

												</div>
											
                                    <div class="table-responsive">
                                        <table class="table table-striped custom-table m-b-0">
                                            <thead>
                                                <tr>
                                                    <th class="col-md-4">My Training</th>
                                                    <th class="col-md-4">My Competencies</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>                                                   
                                                    <td class="text">
                                                        <small class="block text-ellipsis">
                                                            <span class="text-muted">open tasks</span>
                                                        </small>
                                                    </td>
                                                    <td style="width:20px">
                                                     	<ul class="team-members">
														<li>
															<a href="#" data-toggle="tooltip" title="John Doe"><img src="images/user.jpg" alt="John Doe"></a>
														</li>
														<li>
															<a href="#" data-toggle="tooltip" title="Richard Miles"><img src="images/user.jpg" alt="Richard Miles"></a>
														</li>
														<li>
															<a href="#" data-toggle="tooltip" title="John Smith"><img src="images/user.jpg" alt="John Smith"></a>
														</li>
														
														<li>
															<a href="#" class="all-users">+15</a>
														</li>
													</ul>
                                                    </td>
                                                </tr>
                                                <tr>                                                   
                                                    <td class="text">
                                                        <small class="block text-ellipsis">
                                                            <span class="text-muted">open tasks</span>
                                                        </small>
                                                    </td>
                                                    <td style="width:20px">
                                                       	<ul class="team-members">
														<li>
															<a href="#" data-toggle="tooltip" title="John Doe"><img src="images/user.jpg" alt="John Doe"></a>
														</li>
														<li>
															<a href="#" data-toggle="tooltip" title="Richard Miles"><img src="images/user.jpg" alt="Richard Miles"></a>
														</li>
														<li>
															<a href="#" data-toggle="tooltip" title="John Smith"><img src="images/user.jpg" alt="John Smith"></a>
														</li>
														<li>
															<a href="#" class="all-users">+15</a>
														</li>
													</ul>
                                                    </td>
                                                </tr>

                                            </tbody>
                                        </table>
                                    </div>
											
											</div>
										</div>																
								<div class="col-lg-3 col-sm-4">

											<div class="card-box project-box">
										<div class="profile-img">
											<a href="#"><img class="avatar" src="images/user.jpg" alt=""></a>
											<h5 class="user-name m-t-0">Global Technologies</h5>
										</div>
										<br>
										<br>
										
												<div class="project-members m-b-15">
												<ul class="personal-info">
													<li>
														<span class="title">Phone:</span>
														<span class="text"><a href="#">9876543210</a></span>
													</li>
													<li>
														<span class="title">Email:</span>
														<span class="text"><a href="#">barrycuda@example.com</a></span>
													</li>
													<li>
														<span class="title">Birthday:</span>
														<span class="text">2nd August</span>
													</li>
													<li>
														<span class="title">Gender:</span>
														<span class="text">Male</span>
													</li>
												</ul>

												</div>
											
                                    <div class="table-responsive">
                                        <table class="table table-striped custom-table m-b-0">
                                            <thead>
                                                <tr>
                                                    <th class="col-md-4">My Training</th>
                                                    <th class="col-md-4">My Competencies</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>                                                   
                                                    <td class="text">
                                                        <small class="block text-ellipsis">
                                                            <span class="text-muted">open tasks</span>
                                                        </small>
                                                    </td>
                                                    <td style="width:20px">
                                                     	<ul class="team-members">
														<li>
															<a href="#" data-toggle="tooltip" title="John Doe"><img src="images/user.jpg" alt="John Doe"></a>
														</li>
														<li>
															<a href="#" data-toggle="tooltip" title="Richard Miles"><img src="images/user.jpg" alt="Richard Miles"></a>
														</li>
														<li>
															<a href="#" data-toggle="tooltip" title="John Smith"><img src="images/user.jpg" alt="John Smith"></a>
														</li>
														
														<li>
															<a href="#" class="all-users">+15</a>
														</li>
													</ul>
                                                    </td>
                                                </tr>
                                                <tr>                                                   
                                                    <td class="text">
                                                        <small class="block text-ellipsis">
                                                            <span class="text-muted">open tasks</span>
                                                        </small>
                                                    </td>
                                                    <td style="width:20px">
                                                       	<ul class="team-members">
														<li>
															<a href="#" data-toggle="tooltip" title="John Doe"><img src="images/user.jpg" alt="John Doe"></a>
														</li>
														<li>
															<a href="#" data-toggle="tooltip" title="Richard Miles"><img src="images/user.jpg" alt="Richard Miles"></a>
														</li>
														<li>
															<a href="#" data-toggle="tooltip" title="John Smith"><img src="images/user.jpg" alt="John Smith"></a>
														</li>
														<li>
															<a href="#" class="all-users">+15</a>
														</li>
													</ul>
                                                    </td>
                                                </tr>

                                            </tbody>
                                        </table>
                                    </div>
											
											</div>
										</div>																				
								<div class="col-lg-3 col-sm-4">

											<div class="card-box project-box">
										<div class="profile-img">
											<a href="#"><img class="avatar" src="images/user.jpg" alt=""></a>
											<h5 class="user-name m-t-0">Global Technologies</h5>
										</div>
										<br>
										<br>
										
												<div class="project-members m-b-15">
												<ul class="personal-info">
													<li>
														<span class="title">Phone:</span>
														<span class="text"><a href="#">9876543210</a></span>
													</li>
													<li>
														<span class="title">Email:</span>
														<span class="text"><a href="#">barrycuda@example.com</a></span>
													</li>
													<li>
														<span class="title">Birthday:</span>
														<span class="text">2nd August</span>
													</li>
													<li>
														<span class="title">Gender:</span>
														<span class="text">Male</span>
													</li>
												</ul>

												</div>
											
                                    <div class="table-responsive">
                                        <table class="table table-striped custom-table m-b-0">
                                            <thead>
                                                <tr>
                                                    <th class="col-md-4">My Training</th>
                                                    <th class="col-md-4">My Competencies</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>                                                   
                                                    <td class="text">
                                                        <small class="block text-ellipsis">
                                                            <span class="text-muted">open tasks</span>
                                                        </small>
                                                    </td>
                                                    <td style="width:20px">
                                                     	<ul class="team-members">
														<li>
															<a href="#" data-toggle="tooltip" title="John Doe"><img src="images/user.jpg" alt="John Doe"></a>
														</li>
														<li>
															<a href="#" data-toggle="tooltip" title="Richard Miles"><img src="images/user.jpg" alt="Richard Miles"></a>
														</li>
														<li>
															<a href="#" data-toggle="tooltip" title="John Smith"><img src="images/user.jpg" alt="John Smith"></a>
														</li>
														
														<li>
															<a href="#" class="all-users">+15</a>
														</li>
													</ul>
                                                    </td>
                                                </tr>
                                                <tr>                                                   
                                                    <td class="text">
                                                        <small class="block text-ellipsis">
                                                            <span class="text-muted">open tasks</span>
                                                        </small>
                                                    </td>
                                                    <td style="width:20px">
                                                       	<ul class="team-members">
														<li>
															<a href="#" data-toggle="tooltip" title="John Doe"><img src="images/user.jpg" alt="John Doe"></a>
														</li>
														<li>
															<a href="#" data-toggle="tooltip" title="Richard Miles"><img src="images/user.jpg" alt="Richard Miles"></a>
														</li>
														<li>
															<a href="#" data-toggle="tooltip" title="John Smith"><img src="images/user.jpg" alt="John Smith"></a>
														</li>
														<li>
															<a href="#" class="all-users">+15</a>
														</li>
													</ul>
                                                    </td>
                                                </tr>

                                            </tbody>
                                        </table>
                                    </div>
											
											</div>
										</div>


							
								</div>																								
															
								<div id="tasks" class="tab-pane fade">
								
									<div class="project-task">
										<div class="tabbable">
											<ul class="nav nav-tabs nav-tabs-top nav-justified m-b-0">
												<li class="active"><a href="#all_tasks" data-toggle="tab" aria-expanded="true">Planing and Request</a></li>
												<li><a href="#pending_tasks" data-toggle="tab" aria-expanded="false">Approval</a></li>
											</ul>
											<div class="tab-content">
												<div class="tab-pane active" id="all_tasks">
													<div class="task-wrapper">


                    <div class="row">
                        <div class="col-md-3 col-sm-3 col-md-2">
                            <div class="dash-widget clearfix card-box">
                                <span class="dash-widget-icon"><i class="fa fa-folder" aria-hidden="true"></i></span>
                                <div class="dash-widget-info">
                                    <h3>2</h3>
                                    <span>My Loan</span>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3 col-sm-3 col-md-2">
                            <div class="dash-widget clearfix card-box">
                                <span class="dash-widget-icon"><i class="fa fa-bars" aria-hidden="true"></i></span>
                                <div class="dash-widget-info">
                                    <h3>3</h3>
                                    <span>My Leave</span>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3 col-sm-3 col-md-2">
                            <div class="dash-widget clearfix card-box">
                                <span class="dash-widget-icon"><i class="fa fa-trash-o" aria-hidden="true"></i></span>
                                <div class="dash-widget-info">
                                    <h3>1</h3>
                                    <span>Development</span>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3 col-sm-3 col-md-2">
                            <div class="dash-widget clearfix card-box">
                                <span class="dash-widget-icon"><i class="fa fa-cog" aria-hidden="true"></i></span>
                                <div class="dash-widget-info">
                                    <h3>6</h3>
                                    <span>My Training</span>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3 col-sm-3 col-md-2">
                            <div class="dash-widget clearfix card-box">
                                <span class="dash-widget-icon"><i class="fa fa-comment-o" aria-hidden="true"></i></span>
                                <div class="dash-widget-info">
                                    <h3>10</h3>
                                    <span>My Performance</span>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3 col-sm-3 col-md-2">
                            <div class="dash-widget clearfix card-box">
                                <span class="dash-widget-icon"><i class="fa fa-cubes" aria-hidden="true"></i></span>
                                <div class="dash-widget-info">
                                    <h3>4</h3>
                                    <span>My Discipline</span>
                                </div>
                            </div>
                        </div>
						  <div class="col-md-3 col-sm-3 col-md-2">
                            <div class="dash-widget clearfix card-box">
                                <span class="dash-widget-icon"><i class="fa fa-cubes" aria-hidden="true"></i></span>
                                <div class="dash-widget-info">
                                    <h3>4</h3>
                                    <span>My Discipline</span>
                                </div>
                            </div>
                        </div>
									  <div class="col-md-3 col-sm-3 col-md-2">
                            <div class="dash-widget clearfix card-box">
                                <span class="dash-widget-icon"><i class="fa fa-cubes" aria-hidden="true"></i></span>
                                <div class="dash-widget-info">
                                    <h3>4</h3>
                                    <span>My Discipline</span>
                                </div>
                            </div>
                        </div>
									  <div class="col-md-3 col-sm-3 col-md-2">
                            <div class="dash-widget clearfix card-box">
                                <span class="dash-widget-icon"><i class="fa fa-cubes" aria-hidden="true"></i></span>
                                <div class="dash-widget-info">
                                    <h3>4</h3>
                                    <span>My Discipline</span>
                                </div>
                            </div>
                        </div>
									  <div class="col-md-3 col-sm-3 col-md-2">
                            <div class="dash-widget clearfix card-box">
                                <span class="dash-widget-icon"><i class="fa fa-cubes" aria-hidden="true"></i></span>
                                <div class="dash-widget-info">
                                    <h3>4</h3>
                                    <span>My Discipline</span>
                                </div>
                            </div>
                        </div>
									  <div class="col-md-3 col-sm-3 col-md-2">
                            <div class="dash-widget clearfix card-box">
                                <span class="dash-widget-icon"><i class="fa fa-cubes" aria-hidden="true"></i></span>
                                <div class="dash-widget-info">
                                    <h3>4</h3>
                                    <span>My Discipline</span>
                                </div>
                            </div>
                        </div>
									  <div class="col-md-3 col-sm-3 col-md-2">
                            <div class="dash-widget clearfix card-box">
                                <span class="dash-widget-icon"><i class="fa fa-cubes" aria-hidden="true"></i></span>
                                <div class="dash-widget-info">
                                    <h3>4</h3>
                                    <span>My Discipline</span>
                                </div>
                            </div>
                        </div>
									  <div class="col-md-3 col-sm-3 col-md-2">
                            <div class="dash-widget clearfix card-box">
                                <span class="dash-widget-icon"><i class="fa fa-cubes" aria-hidden="true"></i></span>
                                <div class="dash-widget-info">
                                    <h3>4</h3>
                                    <span>My Discipline</span>
                                </div>
                            </div>
                        </div>
									  <div class="col-md-3 col-sm-3 col-md-2">
                            <div class="dash-widget clearfix card-box">
                                <span class="dash-widget-icon"><i class="fa fa-cubes" aria-hidden="true"></i></span>
                                <div class="dash-widget-info">
                                    <h3>4</h3>
                                    <span>My Discipline</span>
                                </div>
                            </div>
                        </div>
                    </div>

													
</div>
													</div>
												<div class="tab-pane" id="pending_tasks">
												<div class="task-wrapper">
					<div class="row">
                        <div class="col-md-3 col-sm-3 col-md-2">
                            <div class="dash-widget clearfix card-box">
                                <span class="dash-widget-icon"><i class="fa fa-folder" aria-hidden="true"></i></span>
                                <div class="dash-widget-info">
                                    <span>Average Length of stay on position - 2 months</span>
                                </div>
                            </div>
                        </div>
                    <div class="col-md-3 col-sm-3 col-md-2">
                            <div class="dash-widget clearfix card-box">
                                <span class="dash-widget-icon"><i class="fa fa-folder" aria-hidden="true"></i></span>
                                <div class="dash-widget-info">
                                    <span>Average Length of stay on position - 2 months</span>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3 col-sm-3 col-md-2">
                            <div class="dash-widget clearfix card-box">
                                <span class="dash-widget-icon"><i class="fa fa-trash-o" aria-hidden="true"></i></span>
                                <div class="dash-widget-info">
                                    <span>Average performance score in the company - 90%</span>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3 col-sm-3 col-md-2">
                            <div class="dash-widget clearfix card-box">
                                <span class="dash-widget-icon"><i class="fa fa-cog" aria-hidden="true"></i></span>
                                <div class="dash-widget-info">
                                    <span>My performance Forecast for Next Review Period- 98%</span>
                                </div>
                            </div>
                        </div>
						      <div class="col-md-3 col-sm-3 col-md-2">
                            <div class="dash-widget clearfix card-box">
                                <span class="dash-widget-icon"><i class="fa fa-cog" aria-hidden="true"></i></span>
                                <div class="dash-widget-info">
                                    <span>My performance Forecast for Next Review Period- 98%</span>
                                </div>
                            </div>
                        </div>
						      <div class="col-md-3 col-sm-3 col-md-2">
                            <div class="dash-widget clearfix card-box">
                                <span class="dash-widget-icon"><i class="fa fa-cog" aria-hidden="true"></i></span>
                                <div class="dash-widget-info">
                                    <span>My performance Forecast for Next Review Period- 98%</span>
                                </div>
                            </div>
                        </div>
						      <div class="col-md-3 col-sm-3 col-md-2">
                            <div class="dash-widget clearfix card-box">
                                <span class="dash-widget-icon"><i class="fa fa-cog" aria-hidden="true"></i></span>
                                <div class="dash-widget-info">
                                    <span>My performance Forecast for Next Review Period- 98%</span>
                                </div>
                            </div>
                        </div>
						      <div class="col-md-3 col-sm-3 col-md-2">
                            <div class="dash-widget clearfix card-box">
                                <span class="dash-widget-icon"><i class="fa fa-cog" aria-hidden="true"></i></span>
                                <div class="dash-widget-info">
                                    <span>My performance Forecast for Next Review Period- 98%</span>
                                </div>
                            </div>
                        </div>
						      <div class="col-md-3 col-sm-3 col-md-2">
                            <div class="dash-widget clearfix card-box">
                                <span class="dash-widget-icon"><i class="fa fa-cog" aria-hidden="true"></i></span>
                                <div class="dash-widget-info">
                                    <span>My performance Forecast for Next Review Period- 98%</span>
                                </div>
                            </div>
                        </div>
						      <div class="col-md-3 col-sm-3 col-md-2">
                            <div class="dash-widget clearfix card-box">
                                <span class="dash-widget-icon"><i class="fa fa-cog" aria-hidden="true"></i></span>
                                <div class="dash-widget-info">
                                    <span>My performance Forecast for Next Review Period- 98%</span>
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
								<div id="analytics" class="tab-pane fade">
									<div class="row">
						<div class="col-md-12">						
							<div class="row">					
								<div class="col-md-3 col-sm-12 text-center">
									<div class="card-box">
										<div id="pie-chart" ></div>
									</div>
								</div>
								<div class="col-sm-3 text-center">
									<div class="card-box">
										<div id="area-chart" ></div>
									</div>
								</div>
								<div class="col-sm-3 text-center">
									<div class="card-box">
										<div id="line-chart"></div>
									</div>
								</div>
								<div class="col-md-3 col-sm-12 text-center">
									<div class="card-box">
										<div id="pie-chart" ></div>
									</div>
								</div>
							</div>
						</div>
					</div>
								</div>
								</div>
							</div>
							</div>
</asp:Content>
