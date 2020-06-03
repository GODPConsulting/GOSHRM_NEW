<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup ="true" CodeBehind="calendar.aspx.vb" Inherits="GOSHRM.calendar" EnableEventValidation="false" Debug="true"%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

                        
                                 <div class="row">
						            <div class="col-sm-8 col-xs-6">
							            <h4 class="page-title">Events</h4>
						            </div>
						            <div class="col-sm-4 col-xs-6 text-right m-b-30">
							            <a href="#" class="btn btn-primary rounded" data-toggle="modal" data-target="#add_event"><i class="fa fa-plus"></i> Add Event</a>
						            </div>
					            </div>
                                <div class="row">
                                     <div class="card-box m-b-0">
                                        <div class="row">
									        <div class="col-md-12">
										        <div id="calender"></div>
									        </div>
								        </div>
                                    </div>
                                </div>                                                             
        <div id="add_event" class="modal custom-modal fade" role="dialog">
				<div class="modal-dialog">
					<button type="button" class="close" data-dismiss="modal">&times;</button>
					<div class="modal-content modal-md">
						<div class="modal-header">
							<%--<h4 class="modal-title">Add Event</h4>--%>
						</div>
						<div class="modal-body">
							<form>
									<div class="container">
        <div>
            <div class="row">
                <div id="divalert" runat="server" visible="false" class="alert alert-info">
                    <strong id="msgalert" runat="server"></strong>
                    <asp:TextBox ID="txtid" runat="server" Width="3px"  
                    Font-Names="Candara" Height="2px" Visible="False"></asp:TextBox>
                </div>
                <div class="col-md-6 col-md-offset-0">
                    <h5 class="page-title" style="color:#1BA691">My Events</h5>
                    <form action="">
                    <div class="">
                        <div class="row">
                            <div class="form-group">
                                <label>Event Title *</label>
                                <input id="txtname" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <label>Event Description*</label>
                                <textarea id="txtDesc" runat="server" class="form-control" rows="5"></textarea>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <label>Start *</label>
                                 <telerik:RadDatePicker Skin="Bootstrap" ID="radScheduleTime" runat="server" Width="100%" RenderMode="Lightweight"
                                    Font-Names="Verdana" ForeColor="#666666">
                                    <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True"
                                        FastNavigationNextText="&amp;lt;&amp;lt;" RenderMode="Lightweight">
                                    </Calendar>
                                    <DateInput DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" LabelWidth="40%"
                                        RenderMode="Lightweight" Height="22px">
                                        <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                        <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                                        <FocusedStyle Resize="None"></FocusedStyle>
                                        <DisabledStyle Resize="None"></DisabledStyle>
                                        <InvalidStyle Resize="None"></InvalidStyle>
                                        <HoveredStyle Resize="None"></HoveredStyle>
                                        <EnabledStyle Resize="None"></EnabledStyle>
                                    </DateInput>
                                    <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                </telerik:RadDatePicker>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <label>End *</label>
                                <telerik:RadDatePicker Skin="Bootstrap" ID="radEndDate" runat="server" Width="100%" RenderMode="Lightweight"
                                    Font-Names="Verdana" ForeColor="#666666">
                                    <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True"
                                        FastNavigationNextText="&amp;lt;&amp;lt;" RenderMode="Lightweight">
                                    </Calendar>
                                    <DateInput DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" LabelWidth="40%"
                                        RenderMode="Lightweight" Height="22px">
                                        <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                        <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                                        <FocusedStyle Resize="None"></FocusedStyle>
                                        <DisabledStyle Resize="None"></DisabledStyle>
                                        <InvalidStyle Resize="None"></InvalidStyle>
                                        <HoveredStyle Resize="None"></HoveredStyle>
                                        <EnabledStyle Resize="None"></EnabledStyle>
                                    </DateInput>
                                    <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                </telerik:RadDatePicker>
                            </div>
                        </div>
                                        <div class="row">
                                            <label class="control-label">Event Time</label>
											<div class="form-group col-md-12">												
												<input type="text" class="form-control datetimepicker">
                                                <div class="col-md-4 col-sm-4 col-xs-12">
                                                     <telerik:RadComboBox ID="radHourStart" runat="server" ResolvedRenderMode="Classic"
                                                   Width="100%" Skin="Bootstrap" Font-Names="Verdana" ForeColor="#666666">
                                                    <Items>
                                                        <telerik:RadComboBoxItem runat="server" Text="1" Value="1" Owner="radHourStart" />
                                                        <telerik:RadComboBoxItem runat="server" Text="2" Value="2" Owner="radHourStart" />
                                                        <telerik:RadComboBoxItem runat="server" Text="3" Value="3" Owner="radHourStart" />
                                                        <telerik:RadComboBoxItem runat="server" Text="4" Value="4" Owner="radHourStart" />
                                                        <telerik:RadComboBoxItem runat="server" Text="5" Value="5" Owner="radHourStart" />
                                                        <telerik:RadComboBoxItem runat="server" Text="6" Value="6" Owner="radHourStart" />
                                                        <telerik:RadComboBoxItem runat="server" Text="7" Value="7" Owner="radHourStart" />
                                                        <telerik:RadComboBoxItem runat="server" Text="8" Value="8" Owner="radHourStart" />
                                                        <telerik:RadComboBoxItem runat="server" Text="9" Value="9" Owner="radHourStart" />
                                                        <telerik:RadComboBoxItem runat="server" Text="10" Value="10" Owner="radHourStart" />
                                                        <telerik:RadComboBoxItem runat="server" Text="11" Value="11" Owner="radHourStart" />
                                                        <telerik:RadComboBoxItem runat="server" Text="12" Value="12" Owner="radHourStart" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                                </div>                                              
                                                <div class="col-md-4 col-sm-4 col-xs-12">
                                                    <telerik:RadComboBox ID="radMinStart" runat="server" ResolvedRenderMode="Classic"
                                                   Width="100%" Skin="Bootstrap" Font-Names="Verdana" ForeColor="#666666">
                                                    <Items>
                                                        <telerik:RadComboBoxItem runat="server" Text="00" Value="00" Owner="radMinStart" />
                                                        <telerik:RadComboBoxItem runat="server" Text="15" Value="15" Owner="radMinStart" />
                                                        <telerik:RadComboBoxItem runat="server" Text="30" Value="30" Owner="radMinStart" />
                                                        <telerik:RadComboBoxItem runat="server" Text="45" Value="45" Owner="radMinStart" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                                </div>                                                
                                                 <div class="col-md-4 col-sm-4 col-xs-12">
                                                    <telerik:RadComboBox ID="radTimeStart" runat="server" ResolvedRenderMode="Classic"
                                                   Width="100%" Skin="Bootstrap" Font-Names="Verdana" ForeColor="#666666">
                                                    <Items>
                                                        <telerik:RadComboBoxItem runat="server" Text="AM" Value="AM" Owner="radTimeStart" />
                                                        <telerik:RadComboBoxItem runat="server" Text="PM" Value="PM" Owner="radTimeStart" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                                </div>                                               
											</div>
										</div>
                        <div class="row">
                            <div class="form-group">
                                <label>Event Status</label>
                               <telerik:RadComboBox ID="radStat" Skin="Bootstrap" runat="server" ResolvedRenderMode="Classic" Width="100%"
                                    AutoPostBack="True" Font-Names="Verdana" Font-Size="11px" 
                                    ForeColor="#666666">
                                    <Items>
                                        <telerik:RadComboBoxItem runat="server" Text="Not Started" Value="Not Started" Owner="radStat" />
                                        <telerik:RadComboBoxItem runat="server" Text="Started" Value="Started" Owner="radStat" />
                                        <telerik:RadComboBoxItem runat="server" Text="Cancelled" Value="Cancelled" Owner="radStat" />
                                    </Items>
                                </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <label>Invitees</label>
                                <telerik:RadComboBox ID="cboInvitees" Skin="Bootstrap" runat="server" AutoPostBack="True" CheckBoxes="True"
                                    Width="100%" 
                                    Font-Names="Verdana" Font-Size="11px" ForeColor="#666666">
                                </telerik:RadComboBox>
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Always">
                                                    <ContentTemplate>
                                                            <telerik:RadListBox ID="lstInvitees" runat="server" ResolvedRenderMode="Classic"
                                                                BorderStyle="None" Enabled="False" Width="100%" EmptyMessage="No Interviewer"
                                                                RenderMode="Lightweight" Sort="Ascending" Font-Names="Verdana" 
                                                                Font-Size="11px" ForeColor="#666666">
                                                                <ButtonSettings TransferButtons="All"></ButtonSettings>
                                                                <EmptyMessageTemplate>
                                                                    No Invites
                                                                </EmptyMessageTemplate>
                                                            </telerik:RadListBox>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="cboInvitees" EventName="ItemChecked" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="row form-group">
                            <asp:CheckBox ID="chkMail" runat="server" Font-Names="Verdana" Font-Size="11px" 
                            Text="Send Mail Invite to Invitees After clicking SAVE" 
                            ForeColor="#666666" Font-Bold="True" />
                        </div>
                        <div class="col-md-10 m-t-20 text-center">
                            <button id="btnupdate" runat="server" onserverclick="btnAdd_Click" type="submit"
                                style="width: 150px" class="btn btn-primary btn-success">
                                Save &amp; Update</button>
                            <button id="Button1" runat="server" onserverclick="btnCancel_Click" type="submit" style="width: 150px"
                                class="btn btn-primary btn-info">
                                << Back</button>
                        </div>
                    </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
														</form>
													</div>
												</div>
											</div>
										</div>
                                <div class="modal custom-modal fade none-border" id="event-modal">
								<div class="modal-dialog">
									<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
									<div class="modal-content modal-md">
										<div class="modal-header">
											<h4 class="modal-title">Add New Event</h4>
										</div>
										<div class="modal-body"></div>
										<div class="modal-footer text-center">
											<button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
											<button type="button" class="btn btn-success save-event">Create event</button>
											<button type="button" class="btn btn-danger delete-event" data-dismiss="modal">Delete</button>
										</div>
									</div>
								</div>
							</div>
                                <div class="modal fade none-border" id="add-category">
								<div class="modal-dialog">
									<div class="modal-content">
										<div class="modal-header">
											<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
											<h4 class="modal-title">Add a category</h4>
										</div>
										<div class="modal-body p-20">
												<div class="row">
													<div class="col-md-6">
														<label class="control-label">Category Name</label>
														<input class="form-control form-white" placeholder="Enter name" type="text" name="category-name"/>
													</div>
													<div class="col-md-6">
														<label class="control-label">Choose Category Color</label>
														<select class="form-control form-white" data-placeholder="Choose a color..." name="category-color">
															<option value="success">Success</option>
															<option value="danger">Danger</option>
															<option value="info">Info</option>
															<option value="pink">Pink</option>
															<option value="primary">Primary</option>
															<option value="warning">Warning</option>
															<option value="orange">Orange</option>
															<option value="brown">Brown</option>
															<option value="teal">Teal</option>
															<option value="inverse">Inverse</option>
														</select>
													</div>
												</div>			
							</form>
						</div>
					</div>
				</div>
			</div>
        <script type="text/javascript" src="js/jquery-3.2.1.min.js"></script>
        <script type="text/javascript" src="js/bootstrap.min.js"></script>
		<script type="text/javascript" src="js/jquery.dataTables.min.js"></script>
		<script type="text/javascript" src="js/dataTables.bootstrap.min.js"></script>
		<script type="text/javascript" src="js/jquery.slimscroll.js"></script>
		<script type="text/javascript" src="js/select2.min.js"></script>
		<script type="text/javascript" src="js/moment.min.js"></script>
		<script type="text/javascript" src="js/jquery-ui.min.html"></script>
       <script type="text/javascript" src="js/fullcalendar.min.js"></script>
        <script type="text/javascript" src="js/jquery.fullcalendar.js"></script>
		<script type="text/javascript" src="js/bootstrap-datetimepicker.min.js"></script>
		<script type="text/javascript" src="js/app.js"></script>
        <%--<script>
            $(document).ready(function () {              
                var events = [];
                $.ajax({
                    method: "get",
                    dataType: "json",
                    url: "Calender_handler.ashx",
                    success: function (data) {
                        $.each(data, function (index, v) {
                            events.push({
                                title: v.eventtitle,
                                description: v.eventdes,
                                start: moment(v.eventdate),
                                end: v.eventenddate != null ? moment(v.eventenddate) : null,
                                color: v.eventcolor,
                                allDay: v.isfullday
                            });
                        })
                        GenerateCalender(events);
                    },
                    error: function (error) {
                        //alert(JSON.stringify(error));
                    }
                })

                function GenerateCalender(events) {
                    $('#calender').fullCalendar('destroy');
                    $('#calender').fullCalendar({
                        contentHeight: 400,
                        defaultDate: new Date(),
                        timeFormat: 'h(:mm)a',
                        header: {
                            left: 'prev,next today',
                            center: 'title',
                            right: 'month,basicWeek,basicDay,agenda'
                        },
                        eventLimit: true,
                        eventColor: '#378006',
                        events: events,
                        eventClick: function (calEvent, jsEvent, view) {
                            $('#myModal #eventTitle').text(calEvent.title);
                            var $description = $('<div/>');
                            $description.append($('<p/>').html('<b>Start:</b>' + calEvent.start.format("DD-MMM-YYYY HH:mm a")));
                            if (calEvent.end != null) {
                                $description.append($('<p/>').html('<b>End:</b>' + calEvent.end.format("DD-MMM-YYYY HH:mm a")));
                            }
                            $description.append($('<p/>').html('<b>Description:</b>' + calEvent.description));
                            $('#myModal #pDetails').empty().html($description);

                            $('#myModal').modal();
                        }
                    })
                }
            })
        </script>   --%>
</asp:Content>

