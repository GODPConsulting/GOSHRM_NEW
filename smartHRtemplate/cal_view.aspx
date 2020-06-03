<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/smartHR.Master" CodeBehind="cal_view.aspx.vb" Inherits="GOSHRM.cal_view" %>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
                <div class="row">
						<div class="col-sm-8 col-xs-6">
							<h4 class="page-title">Events</h4>
						</div>
						<div class="col-sm-4 col-xs-6 text-right m-b-30">
							<a href="#" class="btn btn-success" data-toggle="modal" data-target="#add_event"><i class="fa fa-plus"></i> Add Event</a>
						</div>
					</div>
					<div class="row">
						<div class="col-lg-12">
							<div class="card-box m-b-0">
								<div class="row">
									<div class="col-md-12">
										<div id="calendar"></div>
									</div>
								</div>
							</div>
							<!-- BEGIN MODAL -->
							<div id="add_event" class="modal custom-modal fade" role="dialog">
											<div class="modal-dialog">
												<button type="button" class="close" data-dismiss="modal">&times;</button>
												<div class="modal-content modal-md">
													<div class="modal-header">
														<h4 class="modal-title">Add Event</h4>
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
                                            <%--<h5 class="page-title" style="color:#1BA691">My Events</h5>--%>
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
                                                         <telerik:raddatepicker Skin="Bootstrap" ID="radScheduleTime" runat="server" 
                                                            Width="100%" RenderMode="Lightweight"
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
                                                        </telerik:raddatepicker>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group">
                                                        <label>End *</label>
                                                        <telerik:raddatepicker Skin="Bootstrap" ID="radEndDate" runat="server" 
                                                            Width="100%" RenderMode="Lightweight"
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
                                                        </telerik:raddatepicker>
                                                    </div>
                                                </div>
                                        <div class="row">
                                            <label class="control-label">Event Time</label>
											<div class="form-group col-md-12">												
												<%--<input type="text" class="form-control datetimepicker">--%>
                                                <div class="col-md-4 col-sm-4 col-xs-12">
                                                     <telerik:radcombobox ID="radHourStart" runat="server" ResolvedRenderMode="Classic"
                                                   Width="100%" Skin="Bootstrap" Font-Names="Verdana" ForeColor="#666666">
                                                    <Items>
                                                        <telerik:radcomboboxitem runat="server" Text="1" Value="1" 
                                                            Owner="radHourStart" />
                                                        <telerik:radcomboboxitem runat="server" Text="2" Value="2" 
                                                            Owner="radHourStart" />
                                                        <telerik:radcomboboxitem runat="server" Text="3" Value="3" 
                                                            Owner="radHourStart" />
                                                        <telerik:radcomboboxitem runat="server" Text="4" Value="4" 
                                                            Owner="radHourStart" />
                                                        <telerik:radcomboboxitem runat="server" Text="5" Value="5" 
                                                            Owner="radHourStart" />
                                                        <telerik:radcomboboxitem runat="server" Text="6" Value="6" 
                                                            Owner="radHourStart" />
                                                        <telerik:radcomboboxitem runat="server" Text="7" Value="7" 
                                                            Owner="radHourStart" />
                                                        <telerik:radcomboboxitem runat="server" Text="8" Value="8" 
                                                            Owner="radHourStart" />
                                                        <telerik:radcomboboxitem runat="server" Text="9" Value="9" 
                                                            Owner="radHourStart" />
                                                        <telerik:radcomboboxitem runat="server" Text="10" Value="10" 
                                                            Owner="radHourStart" />
                                                        <telerik:radcomboboxitem runat="server" Text="11" Value="11" 
                                                            Owner="radHourStart" />
                                                        <telerik:radcomboboxitem runat="server" Text="12" Value="12" 
                                                            Owner="radHourStart" />
                                                    </Items>
                                                </telerik:radcombobox>
                                                </div>                                              
                                                <div class="col-md-4 col-sm-4 col-xs-12">
                                                    <telerik:radcombobox ID="radMinStart" runat="server" ResolvedRenderMode="Classic"
                                                   Width="100%" Skin="Bootstrap" Font-Names="Verdana" ForeColor="#666666">
                                                    <Items>
                                                        <telerik:radcomboboxitem runat="server" Text="00" Value="00" 
                                                            Owner="radMinStart" />
                                                        <telerik:radcomboboxitem runat="server" Text="15" Value="15" 
                                                            Owner="radMinStart" />
                                                        <telerik:radcomboboxitem runat="server" Text="30" Value="30" 
                                                            Owner="radMinStart" />
                                                        <telerik:radcomboboxitem runat="server" Text="45" Value="45" 
                                                            Owner="radMinStart" />
                                                    </Items>
                                                </telerik:radcombobox>
                                                </div>                                                
                                                 <div class="col-md-4 col-sm-4 col-xs-12">
                                                    <telerik:radcombobox ID="radTimeStart" runat="server" ResolvedRenderMode="Classic"
                                                   Width="100%" Skin="Bootstrap" Font-Names="Verdana" ForeColor="#666666">
                                                    <Items>
                                                        <telerik:radcomboboxitem runat="server" Text="AM" Value="AM" 
                                                            Owner="radTimeStart" />
                                                        <telerik:radcomboboxitem runat="server" Text="PM" Value="PM" 
                                                            Owner="radTimeStart" />
                                                    </Items>
                                                </telerik:radcombobox>
                                                </div>                                               
											</div>
										</div>
                                        <div class="row">
                                            <div class="form-group">
                                                <label>Event Status</label>
                                               <telerik:radcombobox ID="radStat" Skin="Bootstrap" runat="server" 
                                                    ResolvedRenderMode="Classic" Width="100%"
                                                    Font-Names="Verdana" Font-Size="11px" ForeColor="#666666" enabled="true">
                                                    <Items>
                                                        <telerik:radcomboboxitem runat="server" Text="Scheduled" Value="Not Started" 
                                                            Owner="radStat" />
                                                        <telerik:radcomboboxitem runat="server" Text="Started" Value="Started" 
                                                            Owner="radStat" />
                                                        <telerik:radcomboboxitem runat="server" Text="Cancelled" Value="Cancelled" 
                                                            Owner="radStat" />
                                                    </Items>
                                                </telerik:radcombobox>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
                                                <label>Invitees</label>
                                                <telerik:radcombobox ID="cboInvitees" Skin="Bootstrap" runat="server" 
                                                    AutoPostBack="True" CheckBoxes="True"
                                                    Width="100%" 
                                                    Font-Names="Verdana" Font-Size="11px" ForeColor="#666666" 
                                                    filter="Contains">
                                                </telerik:radcombobox>
                                                            <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Always">
                                                                    <ContentTemplate>
                                                                            <telerik:radlistbox ID="lstInvitees" runat="server" ResolvedRenderMode="Classic"
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
                                            <%--<button id="Button1" runat="server" onserverclick="btnCancel_Click" type="submit" style="width: 150px"
                                                class="btn btn-primary btn-info">
                                                << Back</button>--%>
                                         
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
							<!-- Modal Add Category -->
							<div class="modal fade none-border" id="add-category">
								<div class="modal-dialog">
									<div class="modal-content">
										<div class="modal-header">
											<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
											<h4 class="modal-title">Add a category</h4>
										</div>
										<div class="modal-body p-20">
											<form role="form">
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
										<div class="modal-footer">
											<button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
											<button type="button" class="btn btn-danger save-category" data-dismiss="modal">Save</button>
										</div>
									</div>
								</div>
							</div>
							<!-- END MODAL -->
						</div>
					</div>
        </div>
		<div class="sidebar-overlay" data-reff="#sidebar"></div>
        <script type="text/javascript" src="js/jquery-3.2.1.min.js"></script>
        <%--<script type="text/javascript" src="js/bootstrap.min.js"></script>--%>
		<%--<script type="text/javascript" src="js/jquery.dataTables.min.js"></script>--%>
		<%--<script type="text/javascript" src="js/dataTables.bootstrap.min.js"></script>--%>
		<%--<script type="text/javascript" src="js/jquery.slimscroll.js"></script>--%>
		<%--<script type="text/javascript" src="js/select2.min.js"></script>--%>
		<script type="text/javascript" src="js/moment.min.js"></script>
		<%--<script type="text/javascript" src="js/jquery-ui.min.html"></script>--%>
        <script type="text/javascript" src="js/fullcalendar.min.js"></script>
		<%--<script type="text/javascript" src="js/bootstrap-datetimepicker.min.js"></script>--%>
		<%--<script type="text/javascript" src="js/app.js"></script>--%>
        <script type="text/javascript">

            !function ($) {
                "use strict";

                var CalendarApp = function () {
                    this.$body = $("body")
                    this.$modal = $('#event-modal'),
                    this.$event = ('#external-events div.external-event'),
                    this.$calendar = $('#calendar'),
                    this.$saveCategoryBtn = $('.save-category'),
                    this.$categoryForm = $('#add-category form'),
                    this.$extEvents = $('#external-events'),
                    this.$calendarObj = null
                   };


                /* on drop */
                CalendarApp.prototype.onDrop = function (eventObj, date) {
                    var $this = this;
                    // retrieve the dropped element's stored Event Object
                    var originalEventObject = eventObj.data('eventObject');
                    var $categoryClass = eventObj.attr('data-class');
                    // we need to copy it, so that multiple events don't have a reference to the same object
                    var copiedEventObject = $.extend({}, originalEventObject);
                    // assign it the date that was reported
                    copiedEventObject.start = date;
                    if ($categoryClass)
                        copiedEventObject['className'] = [$categoryClass];
                    // render the event on the calendar
                    $this.$calendar.fullCalendar('renderEvent', copiedEventObject, true);
                    // is the "remove after drop" checkbox checked?
                    if ($('#drop-remove').is(':checked')) {
                        // if so, remove the element from the "Draggable Events" list
                        eventObj.remove();
                    }
                },
                /* on click on event */
    CalendarApp.prototype.onEventClick = function (calEvent, jsEvent, view) {
        var $this = this;
        var form = $("<form></form>");
        form.append("<label>Change event name</label>");
        form.append("<div class='input-group'><input class='form-control' type=text value='" + calEvent.title + "' /><span class='input-group-btn'><button type='submit' class='btn btn-success btn-md'>Save</button></span></div>");
        $this.$modal.modal({
            backdrop: 'static'
        });
        $this.$modal.find('.delete-event').show().end().find('.save-event').hide().end().find('.modal-body').empty().prepend(form).end().find('.delete-event').unbind('click').click(function () {
            $this.$calendarObj.fullCalendar('removeEvents', function (ev) {
                return (ev._id == calEvent._id);
            });
            $this.$modal.modal('hide');
        });
        $this.$modal.find('form').on('submit', function () {
            calEvent.title = form.find("input[type=text]").val();
            $this.$calendarObj.fullCalendar('updateEvent', calEvent);
            $this.$modal.modal('hide');
            return false;
        });
    },
                /* on select */
    CalendarApp.prototype.onSelect = function (start, end, allDay) {
        var $this = this;
        $this.$modal.modal({
            backdrop: 'static'
        });
        var form = $("<form></form>");
        form.append("<div class='row'></div>");
        form.find(".row")
                .append("<div class='col-md-6'><div class='form-group'><label class='control-label'>Event Name</label><input class='form-control' type='text' name='title'/></div></div>")
                .append("<div class='col-md-6'><div class='form-group'><label class='control-label'>Category</label><select class='select form-control' name='category'></select></div></div>")
                .find("select[name='category']")
                .append("<option value='bg-danger'>Danger</option>")
                .append("<option value='bg-success'>Success</option>")
                .append("<option value='bg-purple'>Purple</option>")
                .append("<option value='bg-primary'>Primary</option>")
                .append("<option value='bg-pink'>Pink</option>")
                .append("<option value='bg-info'>Info</option>")
                .append("<option value='bg-inverse'>Inverse</option>")
                .append("<option value='bg-orange'>Orange</option>")
                .append("<option value='bg-brown'>Brown</option>")
                .append("<option value='bg-teal'>Teal</option>")
                .append("<option value='bg-warning'>Warning</option></div></div>");
        $this.$modal.find('.delete-event').hide().end().find('.save-event').show().end().find('.modal-body').empty().prepend(form).end().find('.save-event').unbind('click').click(function () {
            form.submit();
        });
        $this.$modal.find('form').on('submit', function () {
            var title = form.find("input[name='title']").val();
            var beginning = form.find("input[name='beginning']").val();
            var ending = form.find("input[name='ending']").val();
            var categoryClass = form.find("select[name='category'] option:checked").val();
            if (title !== null && title.length != 0) {
                $this.$calendarObj.fullCalendar('renderEvent', {
                    title: title,
                    start: start,
                    end: end,
                    allDay: false,
                    className: categoryClass
                }, true);
                $this.$modal.modal('hide');
            }
            else {
                alert('You have to give a title to your event');
            }
            return false;

        });
        $this.$calendarObj.fullCalendar('unselect');
    },
    CalendarApp.prototype.enableDrag = function () {
        //init events
        $(this.$event).each(function () {
            // create an Event Object (http://arshaw.com/fullcalendar/docs/event_data/Event_Object/)
            // it doesn't need to have a start or end
            var eventObject = {
                title: $.trim($(this).text()) // use the element's text as the event title
            };
            // store the Event Object in the DOM element so we can get to it later
            $(this).data('eventObject', eventObject);
            // make the event draggable using jQuery UI
            $(this).draggable({
                zIndex: 999,
                revert: true,      // will cause the event to go back to its
                revertDuration: 0  //  original position after the drag
            });
        });
    }
                /* Initializing */
                CalendarApp.prototype.init = function () {
                    this.enableDrag();
                    /*  Initialize the calendar  */
                    var date = new Date();
                    var d = date.getDate();
                    var m = date.getMonth();
                    var y = date.getFullYear();
                    var form = '';
                    var today = new Date($.now());

                    var defaultEvents = <%=data %>

                    var $this = this;
                    $this.$calendarObj = $this.$calendar.fullCalendar({
                        slotDuration: '00:15:00', /* If we want to split day time each 15minutes */
                        minTime: '08:00:00',
                        maxTime: '19:00:00',
                        defaultView: 'month',
                        handleWindowResize: true,
                        height: $(window).height() - 200,
                        header: {
                            left: 'prev,next today',
                            center: 'title',
                            right: 'month,agendaWeek,agendaDay'
                        },
                        events: defaultEvents,
                        editable: true,
                        droppable: true, // this allows things to be dropped onto the calendar !!!
                        eventLimit: true, // allow "more" link when too many events
                        selectable: true,
                        drop: function (date) { $this.onDrop($(this), date); },
                        select: function (start, end, allDay) { $this.onSelect(start, end, allDay); },
                        eventClick: function (calEvent, jsEvent, view) { $this.onEventClick(calEvent, jsEvent, view); }

                    });

                    //on new event
                    this.$saveCategoryBtn.on('click', function () {
                        var categoryName = $this.$categoryForm.find("input[name='category-name']").val();
                        var categoryColor = $this.$categoryForm.find("select[name='category-color']").val();
                        if (categoryName !== null && categoryName.length != 0) {
                            $this.$extEvents.append('<div class="external-event bg-' + categoryColor + '" data-class="bg-' + categoryColor + '" style="position: relative;"><i class="mdi mdi-checkbox-blank-circle m-r-10 vertical-middle"></i>' + categoryName + '</div>')
                            $this.enableDrag();
                        }

                    });
                },

                //init CalendarApp
    $.CalendarApp = new CalendarApp, $.CalendarApp.Constructor = CalendarApp

            } (window.jQuery),

            //initializing CalendarApp
function ($) {
    "use strict";
    $.CalendarApp.init()
} (window.jQuery);

        </script>
</asp:Content>
