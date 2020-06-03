<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/smartHR.Master" CodeBehind="chatters.aspx.vb" Inherits="GOSHRM.chatters" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="chat-main-wrapper">
						<div class="col-xs-9 message-view task-view">
							<div class="chat-window">
								<div class="chat-header">
									<div class="navbar">
										<div class="user-details">
											<div class="pull-left user-img m-r-10">
												<a href="profile.html" title="Mike Litorus"><img src="images/user.jpg" alt="" class="w-40 img-circle"><span class="status online"></span></a>
											</div>
											<div class="user-info pull-left">
												<a href="profile.html" title="Mike Litorus"><span class="font-bold">Mike Litorus</span> <i class="typing-text">Typing...</i></a>
												<span class="last-seen">Last seen today at 7:50 AM</span>
											</div>
										</div>
										<ul class="nav navbar-nav pull-right chat-menu">
											<li class="dropdown">
												<a href="#" class="dropdown-toggle" data-toggle="dropdown" aria-expanded="false"><i class="fa fa-cog"></i></a>
												<ul class="dropdown-menu">
													<li><a href="javascript:void(0)">Delete Conversations</a></li>
													<li><a href="javascript:void(0)">Settings</a></li>
												</ul>
											</li>
										</ul>
										<a href="#task_window" class="task-chat profile-rightbar pull-right"><i class="fa fa-user" aria-hidden="true"></i></a>
										<div class="message-search pull-right">
											<div class="input-group input-group-sm">
												<input type="text" class="form-control" placeholder="Search" required="">
												<span class="input-group-btn">
													<button class="btn" type="button"><i class="fa fa-search"></i></button>
												</span>
											</div>
										</div>
									</div>
								</div>
								<div class="chat-contents">
									<div class="chat-content-wrap">
										<div class="chat-wrap-inner">
											<div class="chat-box">
												<div class="chats">
													<div class="chat chat-right">
														<div class="chat-body">
															<div class="chat-bubble">
																<div class="chat-content">
																	<p>Hello. What can I do for you?</p>
																	<span class="chat-time">8:30 am</span>
																</div>
																<div class="chat-action-btns">
																	<ul>
																		<li><a href="#" class="share-msg" title="Share"><i class="fa fa-share-alt"></i></a></li>
																		<li><a href="#" class="edit-msg" title="Edit"><i class="fa fa-pencil"></i></a></li>
																		<li><a href="#" class="del-msg" title="Delete"><i class="fa fa-trash-o"></i></a></li>
																	</ul>
																</div>
															</div>
														</div>
													</div>
													<div class="chat-line">
														<span class="chat-date">October 8th, 2015</span>
													</div>
													<div class="chat chat-left">
														<div class="chat-avatar">
															<a href="profile.html" title="" data-placement="right" data-toggle="tooltip" class="avatar" data-original-title="John Doe">
																<img alt="John Doe" src="images/user.jpg" class="img-responsive img-circle">
															</a>
														</div>
														<div class="chat-body">
															<div class="chat-bubble">
																<div class="chat-content">
																	<p>I'm just looking around.</p>
																	<p>Will you tell me something about yourself? </p>
																	<span class="chat-time">8:35 am</span>
																</div>
																<div class="chat-action-btns">
																	<ul>
																		<li><a href="#" class="share-msg" title="Share"><i class="fa fa-share-alt"></i></a></li>
																		<li><a href="#" class="edit-msg" title="Edit"><i class="fa fa-pencil"></i></a></li>
																		<li><a href="#" class="del-msg" title="Delete"><i class="fa fa-trash-o"></i></a></li>
																	</ul>
																</div>
															</div>
															<div class="chat-bubble">
																<div class="chat-content">
																	<p>Are you there? That time!</p>
																	<span class="chat-time">8:40 am</span>
																</div>
																<div class="chat-action-btns">
																	<ul>
																		<li><a href="#" class="share-msg" title="Share"><i class="fa fa-share-alt"></i></a></li>
																		<li><a href="#" class="edit-msg" title="Edit"><i class="fa fa-pencil"></i></a></li>
																		<li><a href="#" class="del-msg" title="Delete"><i class="fa fa-trash-o"></i></a></li>
																	</ul>
																</div>
															</div>
														</div>
													</div>
													<div class="chat chat-right">
														<div class="chat-body">
															<div class="chat-bubble">
																<div class="chat-content">
																	<p>Where?</p>
																	<span class="chat-time">8:35 am</span>
																</div>
																<div class="chat-action-btns">
																	<ul>
																		<li><a href="#" class="share-msg" title="Share"><i class="fa fa-share-alt"></i></a></li>
																		<li><a href="#" class="edit-msg" title="Edit"><i class="fa fa-pencil"></i></a></li>
																		<li><a href="#" class="del-msg" title="Delete"><i class="fa fa-trash-o"></i></a></li>
																	</ul>
																</div>
															</div>
															<div class="chat-bubble">
																<div class="chat-content">
																	<p>OK, my name is Limingqiang. I like singing, playing basketballand so on.</p>
																	<span class="chat-time">8:42 am</span>
																</div>
																<div class="chat-action-btns">
																	<ul>
																		<li><a href="#" class="share-msg" title="Share"><i class="fa fa-share-alt"></i></a></li>
																		<li><a href="#" class="edit-msg" title="Edit"><i class="fa fa-pencil"></i></a></li>
																		<li><a href="#" class="del-msg" title="Delete"><i class="fa fa-trash-o"></i></a></li>
																	</ul>
																</div>
															</div>
														</div>
													</div>
													<div class="chat chat-left">
														<div class="chat-avatar">
															<a title="" data-placement="right" href="#" data-toggle="tooltip" class="avatar" data-original-title="profile.html">
															<img alt="John Doe" src="images/user.jpg" class="img-responsive img-circle">
															</a>
														</div>
														<div class="chat-body">
															<div class="chat-bubble">
																<div class="chat-content">
																	<p>You wait for notice.</p>
																	<span class="chat-time">8:30 am</span>
																</div>
																<div class="chat-action-btns">
																	<ul>
																		<li><a href="#" class="share-msg" title="Share"><i class="fa fa-share-alt"></i></a></li>
																		<li><a href="#" class="edit-msg" title="Edit"><i class="fa fa-pencil"></i></a></li>
																		<li><a href="#" class="del-msg" title="Delete"><i class="fa fa-trash-o"></i></a></li>
																	</ul>
																</div>
															</div>
															<div class="chat-bubble">
																<div class="chat-content">
																	<p>Consectetuorem ipsum dolor sit?</p>
																	<span class="chat-time">8:50 am</span>
																</div>
																<div class="chat-action-btns">
																	<ul>
																		<li><a href="#" class="share-msg" title="Share"><i class="fa fa-share-alt"></i></a></li>
																		<li><a href="#" class="edit-msg" title="Edit"><i class="fa fa-pencil"></i></a></li>
																		<li><a href="#" class="del-msg" title="Delete"><i class="fa fa-trash-o"></i></a></li>
																	</ul>
																</div>
															</div>
															<div class="chat-bubble">
																<div class="chat-content">
																	<p>OK?</p>
																	<span class="chat-time">8:55 am</span>
																</div>
																<div class="chat-action-btns">
																	<ul>
																		<li><a href="#" class="share-msg" title="Share"><i class="fa fa-share-alt"></i></a></li>
																		<li><a href="#" class="edit-msg" title="Edit"><i class="fa fa-pencil"></i></a></li>
																		<li><a href="#" class="del-msg" title="Delete"><i class="fa fa-trash-o"></i></a></li>
																	</ul>
																</div>
															</div>
															<div class="chat-bubble">
																<div class="chat-content img-content">
																	<div class="chat-img-group clearfix">
																		<p>Uploaded 3 Images</p>
																		<a class="chat-img-attach" href="#">
																			<img width="182" height="137" alt="" src="images/placeholder.png">
																			<div class="chat-placeholder">
																				<div class="chat-img-name">placeholder.png</div>
																				<div class="chat-file-desc">842 KB</div>
																			</div>
																		</a>
																		<a class="chat-img-attach" href="#">
																			<img width="182" height="137" alt="" src="images/placeholder.png">
																			<div class="chat-placeholder">
																				<div class="chat-img-name">842 KB</div>
																			</div>
																		</a>
																		<a class="chat-img-attach" href="#">
																			<img width="182" height="137" alt="" src="images/placeholder.png">
																			<div class="chat-placeholder">
																				<div class="chat-img-name">placeholder.png</div>
																				<div class="chat-file-desc">842 KB</div>
																			</div>
																		</a>
																	</div>
																	<span class="chat-time">9:00 am</span>
																</div>
																<div class="chat-action-btns">
																	<ul>
																		<li><a href="#" class="share-msg" title="Share"><i class="fa fa-share-alt"></i></a></li>
																		<li><a href="#" class="edit-msg" title="Edit"><i class="fa fa-pencil"></i></a></li>
																		<li><a href="#" class="del-msg" title="Delete"><i class="fa fa-trash-o"></i></a></li>
																	</ul>
																</div>
															</div>
														</div>
													</div>
													<div class="chat chat-right">
														<div class="chat-body">
															<div class="chat-bubble">
																<div class="chat-content">
																	<p>OK!</p>
																	<span class="chat-time">9:00 am</span>
																</div>
																<div class="chat-action-btns">
																	<ul>
																		<li><a href="#" class="share-msg" title="Share"><i class="fa fa-share-alt"></i></a></li>
																		<li><a href="#" class="edit-msg" title="Edit"><i class="fa fa-pencil"></i></a></li>
																		<li><a href="#" class="del-msg" title="Delete"><i class="fa fa-trash-o"></i></a></li>
																	</ul>
																</div>
															</div>
														</div>
													</div>
													<div class="chat chat-left">
														<div class="chat-avatar">
															<a title="" data-placement="right" href="#" data-toggle="tooltip" class="avatar" data-original-title="profile.html">
																<img alt="John Doe" src="images/user.jpg" class="img-responsive img-circle">
															</a>
														</div>
														<div class="chat-body">
															<div class="chat-bubble">
																<div class="chat-content">
																	<p>Uploaded 3 files</p>
																	<ul class="attach-list">
																		<li><i class="fa fa-file"></i> <a href="#">example.avi</a></li>
																		<li><i class="fa fa-file"></i> <a href="#">activity.psd</a></li>
																		<li><i class="fa fa-file"></i> <a href="#">example.psd</a></li>
																	</ul>
																</div>
																<div class="chat-action-btns">
																	<ul>
																		<li><a href="#" class="share-msg" title="Share"><i class="fa fa-share-alt"></i></a></li>
																		<li><a href="#" class="edit-msg" title="Edit"><i class="fa fa-pencil"></i></a></li>
																		<li><a href="#" class="del-msg" title="Delete"><i class="fa fa-trash-o"></i></a></li>
																	</ul>
																</div>
															</div>
															<div class="chat-bubble">
																<div class="chat-content">
																	<p>Consectetuorem ipsum dolor sit?</p>
																	<span class="chat-time">8:50 am</span>
																</div>
																<div class="chat-action-btns">
																	<ul>
																		<li><a href="#" class="share-msg" title="Share"><i class="fa fa-share-alt"></i></a></li>
																		<li><a href="#" class="edit-msg" title="Edit"><i class="fa fa-pencil"></i></a></li>
																		<li><a href="#" class="del-msg" title="Delete"><i class="fa fa-trash-o"></i></a></li>
																	</ul>
																</div>
															</div>
															<div class="chat-bubble">
																<div class="chat-content">
																	<p>OK?</p>
																	<span class="chat-time">8:55 am</span>
																</div>
																<div class="chat-action-btns">
																	<ul>
																		<li><a href="#" class="share-msg" title="Share"><i class="fa fa-share-alt"></i></a></li>
																		<li><a href="#" class="edit-msg" title="Edit"><i class="fa fa-pencil"></i></a></li>
																		<li><a href="#" class="del-msg" title="Delete"><i class="fa fa-trash-o"></i></a></li>
																	</ul>
																</div>
															</div>
														</div>
													</div>
													<div class="chat chat-right">
														<div class="chat-body">
															<div class="chat-bubble">
																<div class="chat-content img-content">
																	<div class="chat-img-group clearfix">
																		<p>Uploaded 6 Images</p>
																		<a class="chat-img-attach" href="#">
																			<img width="182" height="137" alt="" src="images/placeholder.png">
																			<div class="chat-placeholder">
																				<div class="chat-img-name">placeholder.png</div>
																				<div class="chat-file-desc">842 KB</div>
																			</div>
																		</a>
																		<a class="chat-img-attach" href="#">
																			<img width="182" height="137" alt="" src="images/placeholder.png">
																			<div class="chat-placeholder">
																				<div class="chat-img-name">842 KB</div>
																			</div>
																		</a>
																		<a class="chat-img-attach" href="#">
																			<img width="182" height="137" alt="" src="images/placeholder.png">
																			<div class="chat-placeholder">
																				<div class="chat-img-name">placeholder.png</div>
																				<div class="chat-file-desc">842 KB</div>
																			</div>
																		</a>
																		<a class="chat-img-attach" href="#">
																			<img width="182" height="137" alt="" src="images/placeholder.png">
																			<div class="chat-placeholder">
																				<div class="chat-img-name">placeholder.png</div>
																				<div class="chat-file-desc">842 KB</div>
																			</div>
																		</a>
																		<a class="chat-img-attach" href="#">
																			<img width="182" height="137" alt="" src="images/placeholder.png">
																			<div class="chat-placeholder">
																				<div class="chat-img-name">placeholder.png</div>
																				<div class="chat-file-desc">842 KB</div>
																			</div>
																		</a>
																		<a class="chat-img-attach" href="#">
																			<img width="182" height="137" alt="" src="images/placeholder.png">
																			<div class="chat-placeholder">
																				<div class="chat-img-name">placeholder.png</div>
																				<div class="chat-file-desc">842 KB</div>
																			</div>
																		</a>
																	</div>
																	<span class="chat-time">9:00 am</span>
																</div>
																<div class="chat-action-btns">
																	<ul>
																		<li><a href="#" class="share-msg" title="Share"><i class="fa fa-share-alt"></i></a></li>
																		<li><a href="#" class="edit-msg" title="Edit"><i class="fa fa-pencil"></i></a></li>
																		<li><a href="#" class="del-msg" title="Delete"><i class="fa fa-trash-o"></i></a></li>
																	</ul>
																</div>
															</div>
														</div>
													</div>
													<div class="chat chat-left">
														<div class="chat-avatar">
															<a title="" data-placement="right" href="#" data-toggle="tooltip" class="avatar" data-original-title="profile.html">
																<img alt="John Doe" src="images/user.jpg" class="img-responsive img-circle">
															</a>
														</div>
														<div class="chat-body">
															<div class="chat-bubble">
																<div class="chat-content">
																	<ul class="attach-list">
																		<li class="pdf-file"><i class="fa fa-file-pdf-o"></i> <a href="#">Document_2016.pdf</a></li>
																	</ul>
																	<span class="chat-time">9:00 am</span>
																</div>
																<div class="chat-action-btns">
																	<ul>
																		<li><a href="#" class="share-msg" title="Share"><i class="fa fa-share-alt"></i></a></li>
																		<li><a href="#" class="edit-msg" title="Edit"><i class="fa fa-pencil"></i></a></li>
																		<li><a href="#" class="del-msg" title="Delete"><i class="fa fa-trash-o"></i></a></li>
																	</ul>
																</div>
															</div>
														</div>
													</div>
													<div class="chat chat-right">
														<div class="chat-body">
															<div class="chat-bubble">
																<div class="chat-content">
																	<ul class="attach-list">
																		<li class="pdf-file"><i class="fa fa-file-pdf-o"></i> <a href="#">Document_2016.pdf</a></li>
																	</ul>
																	<span class="chat-time">9:00 am</span>
																</div>
																<div class="chat-action-btns">
																	<ul>
																		<li><a href="#" class="share-msg" title="Share"><i class="fa fa-share-alt"></i></a></li>
																		<li><a href="#" class="edit-msg" title="Edit"><i class="fa fa-pencil"></i></a></li>
																		<li><a href="#" class="del-msg" title="Delete"><i class="fa fa-trash-o"></i></a></li>
																	</ul>
																</div>
															</div>
														</div>
													</div>
													<div class="chat chat-left">
														<div class="chat-avatar">
															<a title="" data-placement="right" href="#" data-toggle="tooltip" class="avatar" data-original-title="profile.html">
																<img alt="John Doe" src="images/user.jpg" class="img-responsive img-circle">
															</a>
														</div>
														<div class="chat-body">
															<div class="chat-bubble">
																<div class="chat-content">
																	<p>Typing ...</p>
																</div>
															</div>
														</div>
													</div>
												</div>
											</div>
										</div>
									</div>
								</div>
								<div class="chat-footer">
									<div class="message-bar">
										<div class="message-inner">
											<a class="link attach-icon" href="#" data-toggle="modal" data-target="#drag_files"><img src="images/attachment.png" alt=""></a>
											<div class="message-area"><div class="input-group">
												<textarea class="form-control" placeholder="Type message..."></textarea>
												<span class="input-group-btn">
													<button class="btn btn-custom" type="button"><i class="fa fa-send"></i></button>
												</span>
												</div>
											</div>
										</div>
									</div>
								</div>
							</div>
						</div>
						<div class="col-xs-3 profile-right fixed-sidebar-right chat-profile-view" id="task_window">
							<div class="display-table profile-right-inner">
								<div class="table-row">
									<div class="table-body scrollable">
										<div class="table-content">
											<div class="chat-profile-img">
												<div class="edit-profile-img">
													<img class="avatar" src="images/user.jpg" alt="">
													<span class="change-img">Change Image</span>
												</div>
												<h3 class="user-name m-t-10 m-b-0">John Doe</h3>
												<small class="text-muted">Web Designer</small>
												<a href="edit-profile.html" class="btn btn-primary edit-btn"><i class="fa fa-pencil"></i></a>
											</div>
											<div class="chat-profile-info">
												<ul class="user-det-list">
													<li>
														<span>Username:</span>
														<span class="pull-right text-muted">johndoe</span>
													</li>
													<li>
														<span>DOB:</span>
														<span class="pull-right text-muted">24 July</span>
													</li>
													<li>
														<span>Email:</span>
														<span class="pull-right text-muted">johndoe@example.com</span>
													</li>
													<li>
														<span>Phone:</span>
														<span class="pull-right text-muted">9876543210</span>
													</li>
												</ul>
											</div>
											<div class="tabbable">
												<ul class="nav nav-tabs nav-tabs-solid nav-justified m-b-0">
													<li class="active"><a href="#all_files" data-toggle="tab">All Files</a></li>
													<li><a href="#my_files" data-toggle="tab">My Files</a></li>
												</ul>
												<div class="tab-content">
													<div class="tab-pane active" id="all_files">
														<ul class="files-list">
															<li>
																<div class="files-cont">
																	<div class="file-type">
																		<span class="files-icon"><i class="fa fa-file-pdf-o"></i></span>
																	</div>
																	<div class="files-info">
																		<span class="file-name text-ellipsis">AHA Selfcare Mobile Application Test-Cases.xls</span>
																		<span class="file-author"><a href="#">Loren Gatlin</a></span> <span class="file-date">May 31st at 6:53 PM</span>
																	</div>
																	<ul class="files-action">
																		<li class="dropdown">
																			<a href="#" class="dropdown-toggle btn btn-default btn-xs" data-toggle="dropdown" aria-expanded="false"><i class="fa fa-ellipsis-h"></i></a>
																			<ul class="dropdown-menu">
																				<li><a href="javascript:void(0)">Download</a></li>
																				<li><a href="#" data-toggle="modal" data-target="#share_files">Share</a></li>
																			</ul>
																		</li>
																	</ul>
																</div>
															</li>
														</ul>
													</div>
													<div class="tab-pane" id="my_files">
														<ul class="files-list">
															<li>
																<div class="files-cont">
																	<div class="file-type">
																		<span class="files-icon"><i class="fa fa-file-pdf-o"></i></span>
																	</div>
																	<div class="files-info">
																		<span class="file-name text-ellipsis">AHA Selfcare Mobile Application Test-Cases.xls</span>
																		<span class="file-author"><a href="#">John Doe</a></span> <span class="file-date">May 31st at 6:53 PM</span>
																	</div>
																	<ul class="files-action">
																		<li class="dropdown">
																			<a href="#" class="dropdown-toggle btn btn-default btn-xs" data-toggle="dropdown" aria-expanded="false"><i class="fa fa-ellipsis-h"></i></a>
																			<ul class="dropdown-menu">
																				<li><a href="javascript:void(0)">Download</a></li>
																				<li><a href="#" data-toggle="modal" data-target="#share_files">Share</a></li>
																			</ul>
																		</li>
																	</ul>
																</div>
															</li>
														</ul>
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
