<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/smartHR.Master" CodeBehind="EmpDashboard.aspx.vb" Inherits="GOSHRM.EmpDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--    <div class="tcontainer">
<iframe src="employee1.aspx"  scrolling="no" style="width: 100%; height:1200px;overflow: hidden; border:0;" ></iframe></div>--%>


    <div class="content container-fluid">

        <div class="row" style="">
            <div class="tabs_wrapper" style="background: #fff; padding: 1rem; margin: 0 0.6rem">
                <ul class="link_wrapper">
                    <li>
                        <a href="<%= Page.ResolveClientUrl("~/empdashboard")%>" style="border-bottom: 2px solid teal; padding-bottom: 5px">Employee Dashboard</a>
                    </li>
                    <li style="display:none" class="link_manager">
                        <a href="<%= Page.ResolveClientUrl("~/manager")%>" class="links">Manager Dashboard</a>
                    </li>
                    <li style="display:none" class="link_hr">
                        <a href="<%= Page.ResolveClientUrl("~/hrdashboard")%>">HR Dashboard</a>
                    </li>
                </ul>
            </div>
            <div class="col-md-4">
                <div class="dash-widget clearfix" style="background-color: aliceblue; padding: 5px; border: 1px solid #ddd; border-radius: 4px; margin: 5px">
                     <div style="margin-top: 5px; margin-left: 5px;">
                    <div class="" style="background-color: aliceblue;">
                        <h4 style="font-weight: 600">My Request</h4>
                        <div class="dash-widget clearfix card-box">
                            <div style="margin-top: 10px;"><a href="<%= Page.ResolveClientUrl("~/Module/Finance/Loans/LoansAndAdvances")%>" data-toggle="tooltip" title="" data-original-title="My Loan"><span class="dash-widget-icons"><i class="fa fa-usd" aria-hidden="true" style="color: #ff6d00; margin-top: 12px"><span id="Span1" runat="server" style="position: absolute" class="badge bg-purple pull-right">8</span></i></span></a></div>
                            <div><a href="<%= Page.ResolveClientUrl("~/Module/Employee/LeaveManagement/LeaveRoster")%>" data-toggle="tooltip" title="" data-original-title="My Leave"><span class="dash-widget-icons"><i class="fa fa-calendar" aria-hidden="true" style="color: #ff6d00; margin-top: 12px"><span id="Span5" runat="server" style="position: absolute" class="badge bg-purple pull-right">8</span></i></span></a></div>
                            <div><a href="<%= Page.ResolveClientUrl("~/Module/Employee/Performance/DevelopmentPlans")%>" data-toggle="tooltip" title="" data-original-title="My Development"><span class="dash-widget-icons"><i class="fa fa-edit" aria-hidden="true" style="color: #ff6d00; margin-top: 12px"><span id="Span6" runat="server" style="position: absolute" class="badge bg-purple pull-right">8</span></i></span></a></div>
                            <div><a href="<%= Page.ResolveClientUrl("~/Module/Employee/TrainingPortal/Trainings")%>" data-toggle="tooltip" title="" data-original-title="My Trainings"><span class="dash-widget-icons"><i class="fa fa-graduation-cap" aria-hidden="true" style="color: #ff6d00; margin-top: 12px"><span id="Span7" runat="server" style="position: absolute" class="badge bg-purple pull-right">0</span></i></span></a></div>
                            <div><a href="<%= Page.ResolveClientUrl("~/Module/Employee/Performance/AppraisalFeedbackList")%>" data-toggle="tooltip" title="" data-original-title="My Performance"><span class="dash-widget-icons"><i class="fa fa-tachometer" aria-hidden="true" style="color: #ff6d00; margin-top: 12px"><span id="Span8" runat="server" style="position: absolute" class="badge bg-purple pull-right">8</span></i></span></a></div>
                            <div><a href="<%= Page.ResolveClientUrl("~/Module/Employee/Performance/Query")%>" data-toggle="tooltip" title="" data-original-title="My Queries"><span class="dash-widget-icons"><i class="fa fa-user-times" aria-hidden="true" style="color: #ff6d00; margin-top: 12px"><span id="Span9" runat="server" style="position: absolute" class="badge bg-purple pull-right">8</span></i></span></a></div>
                        </div>
                    </div>
                    <div class="" style="background-color: aliceblue; display: block; ">
                        <h4 style="font-weight: 600">My Analytics</h4>
                        <div class="dash-widget clearfix card-box">
                            <div style="margin-top: 10px;"><a href="#" data-toggle="tooltip" title="" data-original-title="Average Length of stay on position(months)"><span class="dash-widget-iconz"><i class="fa fa-hourglass" aria-hidden="true" style="color: #ff6d00; margin-top: 12px"><span id="Span10" runat="server" style="position: absolute" class="badge bg-purple pull-right">8</span></i></span></a></div>
                            <div><a href="#" data-toggle="tooltip" title="" data-original-title="Current length of stay on position(months)"><span class="dash-widget-iconz"><i class="fa fa-hourglass-1" aria-hidden="true" style="color: #ff6d00; margin-top: 12px"><span id="Span11" runat="server" style="position: absolute" class="badge bg-purple pull-right"><%=cur_lenght%></span></i></span></a></div>
                            <div><a href="#" data-toggle="tooltip" title="" data-original-title="Average performance score in the company"><span class="dash-widget-iconz"><i class="fa fa-edit" aria-hidden="true" style="color: #ff6d00; margin-top: 12px"><span id="Span12" runat="server" style="position: absolute" class="badge bg-purple pull-right"><%=cur_per%>%</span></i></span></a></div>
                            <div><a href="#" data-toggle="tooltip" title="" data-original-title="My performance Forecast for Next Review Period"><span class="dash-widget-iconz"><i class="fa fa-tachometer" aria-hidden="true" style="color: #ff6d00; margin-top: 12px"><span id="Span13" runat="server" style="position: absolute" class="badge bg-purple pull-right"><%=cur_per_forcast%>%</span></i></span></a></div>
                        </div>

                    </div>
                </div>
                    <h4 style="font-weight: 600">My Career</h4>
                    <div class="row">
                        <div class="col-md-12">
                            <%--<div class="card-box">
                                <div class="experience-box">
                                                <ul class="experience-list">
                                                    <li>
                                                        <div style="width:20px; height:20px" class="experience-user">
                                                            <div class="fa fa-battery-three-quarters"></div>
                                                        </div>
                                                        <div class="experience-content">
                                                            <div class="timeline-content">
                                                                <a href="#" class="name">My Competencies</a>
                                                                <div id="jobskill" runat="server">
                                                                <div><span class="glyphicon glyphicon-ok"></span> Bsc Computer Science</div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </li>
                                         </ul> 
                                  </div>
                                  </div>--%>
                            <div>
                                <div class="activity">

                                    <div class="activity-box" >
                                        <ul class="activity-list">
                                            <li style="display: none">
                                                <div class="activity-user" >
                                                    <span style="width: 0px; height: 0px;" class="dash-widget-icon"><i style="color: #ff6d00;" class="fa fa-battery-three-quarters" aria-hidden="true"></i></span>
                                                </div>
                                                <div class="activity-content">
                                                    <div class="timeline-content">
                                                        <a href="#" class="name">My Competencies</a>
                                                        <div id="jobskill" runat="server">
                                                            <div>No Competency</div>
                                                        </div>
                                                       
                                                    </div>
                                                </div>
                                            </li>
                                            <li>
                                                <div class="activity-user">
                                                    <span style="width: 0px; height: 0px;" class="dash-widget-icon"><i style="color: #ff6d00;" class="fa fa-book" aria-hidden="true"></i></span>
                                                </div>
                                                <div class="dropdown profile-action">
                                                    <a href="#" class="action-icon dropdown-toggle" data-toggle="dropdown" aria-expanded="false"><i class="fa fa-ellipsis-v"></i></a>
                                                    <ul class="dropdown-menu pull-right">

                                                        <li><a href="<%= Page.ResolveClientUrl("~/Module/Employee/TrainingPortal/Trainings")%>"><i class="glyphicon glyphicon-eye-open m-r-5"></i>View All</a></li>
                                                    </ul>
                                                </div>
                                                <div class="activity-content">
                                                    <div class="timeline-content">
                                                        <a href="#" class="name">My Trainings</a>
                                                        <div id="train" runat="server">
                                                            <div>No Training</div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </li>
                                            <li>
                                                <div class="activity-user">
                                                    <span style="width: 0px; height: 0px;" class="dash-widget-icon"><i style="color: #ff6d00;" class="fa fa-tachometer" aria-hidden="true"></i></span>
                                                </div>
                                                <div class="activity-content">
                                                    <div class="timeline-content">
                                                        <a href="#" class="name">My Performance <%=obj%></a>
                                                        <div id="metrics" runat="server">
                                                            <div>No Performance Objective</div>
                                                        </div>
                                                       
                                                    </div>
                                                </div>
                                            </li>
                                            <li  style="display:none">
                                                <div class="activity-user">
                                                    <span style="width: 0px; height: 0px;" class="dash-widget-icon"><i style="color: #ff6d00;" class="fa fa-tachometer" aria-hidden="true"></i></span>
                                                </div>
                                                <div class="dropdown profile-action">
                                                    <a href="#" class="action-icon dropdown-toggle" data-toggle="dropdown" aria-expanded="false"><i class="fa fa-ellipsis-v"></i></a>
                                                    <ul class="dropdown-menu pull-right">
                                                        <li><a href="<%= Page.ResolveClientUrl("~/Module/Employee/Performance/AppraisalFeedbackList")%>"><i class="glyphicon glyphicon-eye-open m-r-5"></i>View All</a></li>
                                                    </ul>
                                                </div>
                                                <div class="activity-content">
                                                    <div class="timeline-content">
                                                        <a href="#" class="name">Performance Rating</a>
                                                        <div id="rates" runat="server">
                                                            <div>No Performance Rating</div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </li>
                                            <li>
                                                <div class="activity-user">
                                                    <span style="width: 0px; height: 0px;" class="dash-widget-icon"><i style="color: #ff6d00;" class="fa fa-users" aria-hidden="true"></i></span>
                                                </div>
                                                <div class="dropdown profile-action">
                                                    <a href="#" class="action-icon dropdown-toggle" data-toggle="dropdown" aria-expanded="false"><i class="fa fa-ellipsis-v"></i></a>
                                                    <ul class="dropdown-menu pull-right">
                                                        <li><a href="<%= Page.ResolveClientUrl("~/Module/Employee/EmployeeJobHistory")%>"><i class="glyphicon glyphicon-eye-open m-r-5"></i>View All</a></li>
                                                    </ul>
                                                </div>
                                                <div class="activity-content">
                                                    <div class="timeline-content">
                                                        <a href="#" class="name">Job History</a>
                                                        <div id="JobH" runat="server">
                                                            <div>No Job History</div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </li>
                                            <li style="display: none;">
                                                <div class="activity-user">
                                                    <span style="width: 0px; height: 0px;" class="dash-widget-icon"><i style="color: #ff6d00;" class="fa fa-tachometer" aria-hidden="true"></i></span>
                                                </div>
                                                <div class="activity-content">
                                                    <div class="timeline-content">
                                                        <a href="#" class="name">Performance Metrics for Next Level</a>
                                                        <div id="nxtP" runat="server">
                                                            <div>No Performance Metrics</div>
                                                        </div>
                                                    </div>
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

            <div class="col-md-8">
                <div style="margin-top: 5px; margin-left: 5px; display:none">
                    <div class="col-md-6" style="background-color: aliceblue; border: 1px solid #ddd; border-radius: 4px;">
                        <h4 style="font-weight: 600">My Request</h4>
                        <div class="dash-widget clearfix card-box">
                            <div style="margin-top: 10px;"><a href="<%= Page.ResolveClientUrl("~/Module/Finance/Loans/LoansAndAdvances")%>" data-toggle="tooltip" title="" data-original-title="My Loan"><span class="dash-widget-icons"><i class="fa fa-usd" aria-hidden="true" style="color: #ff6d00; margin-top: 12px"><span id="loans" runat="server" style="position: absolute" class="badge bg-purple pull-right">8</span></i></span></a></div>
                            <div><a href="<%= Page.ResolveClientUrl("~/Module/Employee/LeaveManagement/LeaveRoster")%>" data-toggle="tooltip" title="" data-original-title="My Leave"><span class="dash-widget-icons"><i class="fa fa-calendar" aria-hidden="true" style="color: #ff6d00; margin-top: 12px"><span id="leaves" runat="server" style="position: absolute" class="badge bg-purple pull-right">8</span></i></span></a></div>
                            <div><a href="<%= Page.ResolveClientUrl("~/Module/Employee/Performance/DevelopmentPlans")%>" data-toggle="tooltip" title="" data-original-title="My Development"><span class="dash-widget-icons"><i class="fa fa-edit" aria-hidden="true" style="color: #ff6d00; margin-top: 12px"><span id="dev" runat="server" style="position: absolute" class="badge bg-purple pull-right">8</span></i></span></a></div>
                            <div><a href="<%= Page.ResolveClientUrl("~/Module/Employee/TrainingPortal/Trainings")%>" data-toggle="tooltip" title="" data-original-title="My Trainings"><span class="dash-widget-icons"><i class="fa fa-graduation-cap" aria-hidden="true" style="color: #ff6d00; margin-top: 12px"><span id="trainer" runat="server" style="position: absolute" class="badge bg-purple pull-right">0</span></i></span></a></div>
                            <div><a href="<%= Page.ResolveClientUrl("~/Module/Employee/Performance/AppraisalFeedbackList")%>" data-toggle="tooltip" title="" data-original-title="My Performance"><span class="dash-widget-icons"><i class="fa fa-tachometer" aria-hidden="true" style="color: #ff6d00; margin-top: 12px"><span id="per" runat="server" style="position: absolute" class="badge bg-purple pull-right">8</span></i></span></a></div>
                            <div><a href="<%= Page.ResolveClientUrl("~/Module/Employee/Performance/Query")%>" data-toggle="tooltip" title="" data-original-title="My Queries"><span class="dash-widget-icons"><i class="fa fa-user-times" aria-hidden="true" style="color: #ff6d00; margin-top: 12px"><span id="query" runat="server" style="position: absolute" class="badge bg-purple pull-right">8</span></i></span></a></div>
                        </div>
                    </div>
                    <div class="col-md-6" style="background-color: aliceblue; display: block; border: 1px solid #ddd; border-radius: 4px;">
                        <h4 style="font-weight: 600">My Analytics</h4>
                        <div class="dash-widget clearfix card-box">
                            <div style="margin-top: 10px;"><a href="#" data-toggle="tooltip" title="" data-original-title="Average Length of stay on position(months)"><span class="dash-widget-iconz"><i class="fa fa-hourglass" aria-hidden="true" style="color: #ff6d00; margin-top: 12px"><span id="avgLength" runat="server" style="position: absolute" class="badge bg-purple pull-right">8</span></i></span></a></div>
                            <div><a href="#" data-toggle="tooltip" title="" data-original-title="Current length of stay on position(months)"><span class="dash-widget-iconz"><i class="fa fa-hourglass-1" aria-hidden="true" style="color: #ff6d00; margin-top: 12px"><span id="Span2" runat="server" style="position: absolute" class="badge bg-purple pull-right"><%=cur_lenght%></span></i></span></a></div>
                            <div><a href="#" data-toggle="tooltip" title="" data-original-title="Average performance score in the company"><span class="dash-widget-iconz"><i class="fa fa-edit" aria-hidden="true" style="color: #ff6d00; margin-top: 12px"><span id="Span3" runat="server" style="position: absolute" class="badge bg-purple pull-right"><%=cur_per%>%</span></i></span></a></div>
                            <div><a href="#" data-toggle="tooltip" title="" data-original-title="My performance Forecast for Next Review Period"><span class="dash-widget-iconz"><i class="fa fa-tachometer" aria-hidden="true" style="color: #ff6d00; margin-top: 12px"><span id="Span4" runat="server" style="position: absolute" class="badge bg-purple pull-right"><%=cur_per_forcast%>%</span></i></span></a></div>
                        </div>

                    </div>
                </div>

                <div class="col-md-6" id="width_1" style="background-color: aliceblue; margin-top: 5px; width: 346px; margin-left: 5px; border: 1px solid #ddd; border-radius: 4px">
                    <h4 style="font-weight: 600">Performance Rating</h4>
                    <div>
                        <canvas id="line-chart" height="300" width="300" style="display: block; background: #fff; "></canvas>
                    </div>
                </div>
               <div class="col-md-6" id="width_2" style="background-color: aliceblue; margin-top: 5px; margin-left: 5px; border: 1px solid #ddd; border-radius:4px">
                    <h4 style="font-weight: 600">Competence Rating</h4>
                    <div>
                        <canvas id="bar-chart-grouped" height="300" width="300" style="display: block; background: #fff;"></canvas>
                    </div>
                </div>
                 <div class=" col-md-12 dash-widget clearfix" id="special_width" style="background-color: aliceblue; padding: 5px; border: 1px solid #ddd; border-radius: 4px; margin: 5px;
}">
                    <h4 style="font-weight: 600">My Development</h4>
                    <div style="display: none;" class="dropdown profile-action">
                        <a href="#" class="action-icon dropdown-toggle" data-toggle="dropdown" aria-expanded="false"><i style="margin-right: 10px" class="fa fa-ellipsis-v"></i></a>
                        <ul class="dropdown-menu pull-right">
                            <li><a href="<%= Page.ResolveClientUrl("~/Module/Employee/Performance/DevelopmentPlans")%>"><i class="glyphicon glyphicon-eye-open m-r-5"></i>View All</a></li>
                        </ul>
                    </div>
                    <div id="dev_panel" runat="server" class="panel-body">
                        <div class="table-responsive">
                            <table class="table table-striped custom-table m-b-0">
                                <thead>
                                    <tr>
                                        <th class="col-md-4">Development Plan Activity</th>
                                        <th class="col-md-4">Development Plan Training</th>
                                        <th class="col-md-4">Target Date</th>
                                    </tr>
                                </thead>
                                <tbody id="dev_plan" runat="server">
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="dash-widget clearfix" style="background-color: aliceblue; padding: 5px; border: 1px solid #ddd; border-radius: 4px; margin: 5px">
                        <h4 style="font-weight: 600">My Task </h4>
                        <div class="task-wrapper" style="padding: 5px">
                            <div class="task-list-container">
                                <div class="task-list-body">
                                    <ul id="task-list">
                                        <%--<li class="task">
                                                    <div class="task-container">
                                                        <span class="task-action-btn task-check">
                                                            <span class="action-circle large complete-btn" title="Mark Complete">
                                                                <i class="material-icons">check</i>
                                                            </span>
                                                        </span>
                                                        <span class="task-label" contenteditable="true">Patient appointment booking</span>
                                                        <span class="task-action-btn task-btn-right">
                                                            <span class="action-circle large" title="Assign">
                                                                <i class="material-icons">person_add</i>
                                                            </span>
                                                            <span class="action-circle large delete-btn" title="Delete Task">
                                                                <i class="material-icons">delete</i>
                                                            </span>
                                                        </span>
                                                    </div>
                                                </li>--%>
                                        <div class="col-md-12">
                                            <div class="card-box">
                                                <div class="experience-box">
                                                    <ul id="taskss" runat="server" class="experience-list">
                                                        <span class='text-muted'>No Tasks</span>
                                                    </ul>
                                                </div>
                                            </div>
                                        </div>

                                    </ul>
                                </div>
                                <div class="task-list-footer">
                                    <div class="new-task-wrapper">
                                        <textarea id="new-task" placeholder="Enter new task here. . ."></textarea>
                                        <span class="error-message hidden">You need to enter a task first</span>
                                        <span class="add-new-task-btn btn" id="add-task">Add Task</span>
                                        <span class="cancel-btn btn" id="close-task-panel">Close</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="dash-widget clearfix" style="background-color: aliceblue; padding: 5px; border: 1px solid #ddd; border-radius: 4px; margin: 5px">
                        <h4 style="font-weight: 600">Work Anniversaries</h4>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="card-box">
                                    <div class="experience-box">
                                        <ul id="work_history" runat="server" class="experience-list">
                                            <span class='text-muted'>No Work History</span>
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




    <div class="themes">
        <div class="themes-icon"><i class="fa fa-cog"></i></div>
        <div class="themes-body">
            <ul id="theme-change" class="theme-colors">
                <li><span class="theme-orange"></span></li>
                <li><span class="theme-purple"></span></li>
                <li><span class="theme-blue"></span></li>
                <li><span class="theme-maroon"></span></li>
                <li><span class="theme-light"></span></li>
                <li><span class="theme-dark"></span></li>
            </ul>
        </div>
    </div>
    </div>
    <script type="text/javascript" src="js/Chart.min.js"></script>
    <script>
        var canvas = document.getElementById("bar-chart-grouped");
        var special_width = document.getElementById('special_width');
        const width_1 = document.getElementById('width_1');
        const width_2 = document.getElementById('width_2');
        console.log(screen.height);
        if (screen.width <= 1366) {
            console.log(screen.height);
            canvas.height = 289;
            special_width.style.maxWidth = '47.4rem'
        }
        if (screen.width == 1600) {
            console.log(screen.width)
            canvas.height = 260;
            special_width.style.maxWidth = '865px';
            width_1.style.width = '400px';
            width_2.style.width = '462px'
        }
        new Chart(document.getElementById("bar-chart-grouped"), {
           type: 'radar',
                 data: {
                     labels: [<%=expectedSkills %>],
                     datasets: [
                         { data: [<%=expectedWeight %>], borderColor: "#3e95cd", label: 'Expected Scores' },
                         { data: [<%=actualWeight %>],borderColor: "#FF6D00", label: 'Actual Scores' }
                     ]
                 },
                 options: {
                     legend: { display: true },
                     title: {
                         display: false,
                         text: 'Employee by Male to Female Ratio'
                     },
                     responsive: true
                 }
        });
       new Chart(document.getElementById("line-chart"), {
                 type: 'line',
                 data: {
                     labels: [<%=year %>],

                      datasets: [
                 {
                     label: "Performance",
                     data: [<%=score %>],
                     fill: false,
                     borderColor: "#4bc0c0"
                 },
                
             ]
                 },
                 options: {
                     legend: { display: true },
                     title: {
                         display: false,
                         text: 'Performance Rating'


                     }
                 }
             });

    </script>

</asp:Content>
