<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/smartHR.Master" CodeBehind="hrdashboard.aspx.vb" Inherits="GOSHRM.hrdashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
    <div class="row">
        <div class="tabs_wrapper" style="background: #fff; padding: 1rem; margin: 0 0.6rem 5px 0.6rem">
            <ul class="link_wrapper">
                <li>
                    <a href="<%= Page.ResolveClientUrl("~/empdashboard")%>">Employee Dashboard</a>
                </li>
                <li style="display:none" class="link_manager">
                    <a href="<%= Page.ResolveClientUrl("~/manager")%>" class="links">Manager Dashboard</a>
                </li>
                <li style="display:none" class="link_hr">
                    <a href="<%= Page.ResolveClientUrl("~/hrdashboard2.aspx")%>">HR Dashboard</a>
                </li>
                <li style="display:none" class="link_hr">
                    <a href="<%= Page.ResolveClientUrl("~/hrdashboard")%>" style="border-bottom: 2px solid teal; padding-bottom: 5px">HR Analytics</a>
                </li>
            </ul>
        </div>
        <div class="row user-tabs">
                      <div class="row col-sm-3 col-md-6 col-xs-6 pull-right">
                       <div class="col-lg-12">
                        <telerik:RadComboBox runat="server" Skin="Bootstrap"
                            RenderMode="Lightweight" Width="100%" ResolvedRenderMode="Classic" ID="cboCompany"
                            AutoPostBack="True" Filter="Contains" Font-Names="Verdana"  ForeColor="#666666" >
                        </telerik:RadComboBox>
                       </div>
                        </div>
                         </div>
                      <div class="card-box tab-box">
						<div class="row user-tabs">
							<div class="col-lg-12 col-md-12 col-sm-12 line-tabs">
								<ul class="nav nav-tabs tabs nav-tabs-bottom">
									<li class="active col-sm-2"><a data-toggle="tab" href="#myprojects">Employee Anaytics</a></li>
									<li class="col-sm-2"><a data-toggle="tab" href="#tasks">L&D Analytics</a></li>
                                    <li class="col-sm-2"><a data-toggle="tab" href="#recruitments">Recruitment Anaytics</a></li>
                                    <li class="col-sm-2"><a data-toggle="tab" href="#compensations">Compensation and Benefit Anaytics </a></li>
                                     <li class="col-sm-2"><a data-toggle="tab" href="#times">Time and Leave Analytics</a></li>
                                    <li class="col-sm-2"><a data-toggle="tab" href="#pms">PMS Analytics </a></li>

								</ul>
							</div>
						</div>
					</div>
                      <div class="row">
                        <div class="col-lg-12">
							<div class="tab-content  profile-tab-content">
								<div id="myprojects" class="tab-pane fade in active">
						                <div class="row">
						                        <div class="col-md-12">
							                        <div class="row">
                                                        <div class="col-md-6 col-sm-12 text-center">
									                        <div class="card-box">
                                                            <h3>Employee by Male to Female Ratio</h3>
                                                           <canvas id="bar-chart-grouped" width="800" height="450"></canvas>
									                        </div>
								                        </div>
                                                        <div class="col-md-6 col-sm-12 text-center">
									                        <div class="card-box">
                                                            <h3>Employee by Job Title Ratio</h3>
                                                            <canvas id="doughnut-chart1a" width="800" height="450"></canvas>
									                        </div>
								                        </div>
                                                        <div  class="col-md-6 col-sm-12 text-center">
									                        <div class="card-box">
                                                             <h3>Employee by Office</h3>
                                                               <canvas id="radar-chart1a" width="800" height="450"></canvas>
									                        </div>
								                        </div>
								                        <div  class="col-md-6 col-sm-12 text-center">
									                        <div class="card-box">
                                                            <h3>Employee by Job Grade Ratio</h3>
                                                                <canvas id="doughnut-chart" width="800" height="450"></canvas>
									                        </div>
								                        </div>  
                                                        <div class="col-md-6 col-sm-12 text-center">
									                        <div class="card-box">
                                                             <h3>Employee by Nationality Ratio</h3>
                                                              <canvas id="pie-charta1" width="800" height="450"></canvas>
									                        </div>
								                        </div>
                                                        <div class="col-md-6 col-sm-12 text-center">
									                        <div class="card-box">
                                                            <h3>Employee Population Per Performance Score</h3>
                                                          <canvas id="bar-chart"" width="800" height="450"></canvas>
									                        </div>
								                        </div>

							                        </div>
						                        </div>
					                    </div>
								</div>
								<div id="tasks" class="tab-pane fade">
							            <div class="row">
						                        <div class="col-md-12">
							                        <div class="row">
                                                         <div class="col-md-6 col-sm-12 text-center">
									                        <div class="card-box">
                                                             <h3>Average Training Investment</h3>
                                                              <canvas id="line-chart2" width="800" height="450"></canvas>
									                        </div>
								                        </div>
                                                         <div class="col-md-6 col-sm-12 text-center">
									                        <div class="card-box">
                                                             <h3>Training cost year on year</h3>
                                                              <canvas id="bar-chart2a" width="800" height="450"></canvas>
									                        </div>
								                        </div>
                                                         <div class="col-md-12 col-sm-12 text-center">
									                        <div class="card-box">
                                                             <h3>Global Training Hours per Training Type</h3>
                                                              <canvas id="line-chart2b" width="800" height="250"></canvas>
									                        </div>
								                        </div>
                                                         <div  class="col-md-6 col-sm-12 text-center">
									                        <div class="card-box">
                                                            <h3>Global Hours of Training</h3>
                                                               <canvas id="bar-chart2" width="800" height="450"></canvas>
									                        </div>
								                        </div>
                                                         <div  class="col-md-6 col-sm-12 text-center">
									                        <div class="card-box">
                                                            <h3>Completion Status Analysis</h3>
                                                               <canvas id="pie-chart2" width="800" height="450"></canvas>
									                        </div>
								                        </div>
							                        </div>
						                        </div>
					                    </div>
								</div>
                                <div id="recruitments" class="tab-pane fade">
							        <div class="row">
						                        <div class="col-md-12">
							                        <div class="row">
                                                          <div class="col-md-6 col-sm-12 text-center">
									                        <div class="card-box">
                                                             <h3>Workforce Growth Rate</h3>
                                                              <canvas id="line-chart3" width="800" height="450"></canvas>
									                        </div>
								                        </div>
                                                          <div class="col-md-6 col-sm-12 text-center">
									                        <div class="card-box">
                                                             <h3>Turnover Rate</h3>
                                                              <canvas id="line-chart3b" width="800" height="450"></canvas>
									                        </div>
								                        </div>
                                                        <div class="col-md-6 col-sm-12 text-center">
									                        <div class="card-box">
                                                             <h3>Joiners/Leavers Yearly</h3>
                                                              <canvas id="bar-chart-grouped3" width="800" height="450"></canvas>
									                        </div>
								                        </div>
                                                          <div class="col-md-6 col-sm-12 text-center">
									                        <div class="card-box">
                                                             <h3>Leavers Analysis by Reason</h3>
                                                              <canvas id="pie-chart3a" width="800" height="450"></canvas>
									                        </div>
								                        </div>
                                                          <div class="col-md-6 col-sm-12 text-center">
									                        <div class="card-box">
                                                             <h3>Leavers Analysis by Length of Service</h3>
                                                              <canvas id="bar-chart3" width="800" height="450"></canvas>
									                        </div>
								                        </div>
                                                          <div class="col-md-6 col-sm-12 text-center">
									                        <div class="card-box">
                                                             <h3>Leavers Analysis by Performance Score</h3>
                                                              <canvas id="pie-chart3" width="800" height="450"></canvas>
									                        </div>
								                        </div>
							                        </div>
						                        </div>
					                    </div>
								</div>
                                <div id="compensations" class="tab-pane fade">
	                                     <div class="row">
						                        <div class="col-md-12">
							                        <div class="row">
                                                        <div  class="col-md-6 col-sm-12 text-center">
									                        <div class="card-box">
                                                            <h3>Annual Compensation and Benefit</h3>
                                                                <canvas id="bar-chart4" width="800" height="450"></canvas>
									                        </div>
								                        </div>
                                                        <div  class="col-md-6 col-sm-12 text-center">
									                        <div class="card-box">
                                                            <h3>Compensation and Benefit Analysis</h3>
                                                               <canvas id="pie-chart4" width="800" height="450"></canvas>
									                        </div>
								                        </div>
                                                            <div  class="col-md-6 col-sm-12 text-center">
									                        <div class="card-box">
                                                            <h3>Monthly Compensation and Benefit</h3>
                                                                <canvas id="bar-charta4" width="800" height="450"></canvas>
									                        </div>
								                        </div>
                                                        <div  class="col-md-6 col-sm-12 text-center">
									                        <div class="card-box">
                                                            <h3>Monthly Compensation and Benefit Analysis</h3>
                                                               <canvas id="pie-charta4" width="800" height="450"></canvas>
									                        </div>
								                        </div>
							                        </div>
						                        </div>
					                    </div>
								</div>
                                <div id="times" class="tab-pane fade">
	                                     <div class="row">
						                        <div class="col-md-12">
							                        <div class="row">
                                                        <div  class="col-md-6 col-sm-12 text-center">
									                        <div class="card-box">
                                                            <h3>Global Leave days</h3>
                                                                <canvas id="bar-chart5b" width="1000" height="450"></canvas>
									                        </div>
								                        </div>
                                                        <div  class="col-md-6 col-sm-12 text-center">
									                        <div class="card-box">
                                                            <h3>Global Leave Cost(Millions)</h3>
                                                                <canvas id="bar-chart5c" width="1000" height="450"></canvas>
									                        </div>
								                        </div>
							                        </div>
						                        </div>
					                    </div>
								</div>
                                <div id="pms" class="tab-pane fade">
	                                     <div class="row">
						                        <div class="col-md-12">
							                        <div class="row">
                                                    <div  class="col-md-6 col-sm-12 text-center">
									                        <div class="card-box">
                                                            <h3>Objective Analysis</h3>
                                                               <canvas id="pie-chart6" width="800" height="450"></canvas>
									                        </div>
								                        </div>
                                                    <div  class="col-md-6 col-sm-12 text-center">
									                        <div class="card-box">
                                                            <h3>Feedback Analysis</h3>
                                                               <canvas id="pie-chart6b" width="800" height="450"></canvas>
									                        </div>
								                        </div>
                                                    <div  class="col-md-12 col-sm-12 text-center">
									                        <div class="card-box">
                                                            <h3>Performance Analysis</h3>
                                                               <canvas id="pie-chart6c" width="800" height="250"></canvas>
									                        </div>
								                        </div>
                                                         <%--<div  class="col-md-12 col-sm-12 text-center">
									                        <div class="card-box">
                                                            <h3>Performance Metrics</h3>
                                                               <canvas id="line-chart" width="800" height="250"></canvas>
									                        </div>
								                        </div>
                                                       <div  class="col-md-12 col-sm-12 text-center">
									                        <div class="card-box">
                                                            <h3>Staff Turnover</h3>
                                                               <canvas id="line-chartq" width="800" height="250"></canvas>
									                        </div>
								                        </div>--%>
							                        </div>
						                        </div>
					                    </div>
								</div>
							 </div>
						 </div>
					  </div>
	            </div>

        <div class="sidebar-overlay" data-reff="#sidebar"></div>
<%--        <script type="text/javascript" src="js/jquery-3.2.1.min.js"></script>
        <script type="text/javascript" src="js/bootstrap.min.js"></script>
		<script type="text/javascript" src="js/jquery.slimscroll.js"></script>
		<script type="text/javascript" src="js/select2.min.js"></script>
		<script type="text/javascript" src="js/moment.min.js"></script>
		<script type="text/javascript" src="js/bootstrap-datetimepicker.min.js"></script>--%>
        <!-- =================Chart JS ===========================-->
          <script type="text/javascript" src="js/Chart.min.js"></script>
<%--
        <script type="text/javascript" src="js/moment.min.js"></script>
		<script type="text/javascript" src="plugins/morris/morris.min.js"></script>
		<script type="text/javascript" src="plugins/raphael/raphael-min.js"></script>
		<script type="text/javascript" src="js/app.js"></script>--%>


<%--<script>
    function addCommas(nStr) {
        nStr += '';
        x = nStr.split('.');
        x1 = x[0];
        x2 = x.length > 1 ? '.' + x[1] : '';
        var rgx = /(\d+)(\d{3})/;
        while (rgx.test(x1)) {
            x1 = x1.replace(rgx, '$1' + ',' + '$2');
        }
        return x1 + x2;
    }
</script>
--%>
        ------------------<!--Employee-->---------------------------

        
         <script>


             new Chart(document.getElementById("pie-charta1"), {
                 type: 'pie',
                 data: {
                     labels: [<%=nationalitytitle %>],
                     datasets: [{
                         label: "Amount (millions)",
                         backgroundColor: ["#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850", "#0250f7", "#f7020e"],
                         data: [<%=nationalitytotal %>]
                     }]
                 },
                 options: {
                     legend: { display: true },
                     title: {
                         display: false,
                         text: 'Employee by Nationality Ratio'
                     }
                 }
             });


             new Chart(document.getElementById("radar-chart1a"), {
                 type: 'radar',
                 data: {
                     labels: [<%=departmenttitle %>],
                     datasets: [
                         {
                             label: "",
                             fill: true,
                             backgroundColor: "rgba(179,181,198,0.2)",
                             borderColor: "rgba(179,181,198,1)",
                             pointBorderColor: ["#fff", "#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850"],
                             pointBackgroundColor: "rgba(179,181,198,1)",
                             data: [<%=departmenttotal %>]
                         }
                     ]
                 },
                 options: {
                     legend: { display: true },
                     title: {
                         display: false,
                         text: 'Employee by Department'
                     }
                 }
             });


             new Chart(document.getElementById("doughnut-chart"), {
                 type: 'doughnut',
                 data: {
                     labels: [<%=Jgrade %>],
                     datasets: [
                         {
                             label: "Employee by Job Grade Ratio",
                             backgroundColor: ["#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850", "#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850", "#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850", "#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850", "#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850", "#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850"],
                             data: [<%=Jobgradetotal %>]
                         }
                     ]
                 },
                 options: {
                     legend: { display: true },
                     title: {
                         display: false,
                         text: 'Employee by Job Grade Ratio'
                     }
                 }
             });



             new Chart(document.getElementById("doughnut-chart1a"), {
                 type: 'doughnut',
                 data: {
                     labels: [<%=Jtitle %>],
                     datasets: [
                         {
                             label: "Employee by Job Title Ratio",
                             backgroundColor: ["#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850", "#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850", "#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850", "#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850", "#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850", "#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850"],
                             data: [<%=Jobtitletotal %>]
                         }
                     ]
                 },
                 options: {
                     legend: { display: true },
                     title: {
                         display: false,
                         text: 'Employee by Job Title Ratio'
                     }
                 }
             });



             new Chart(document.getElementById("bar-chart-grouped"), {
                 type: 'bar',
                 data: {
                     labels: [<%=yss %>],
                     datasets: [
                         {
                             label: "Male",
                             backgroundColor: "#3e95cd",
                             data: [<%=mss %>]
                         }, {
                             label: "Female",
                             backgroundColor: "#8e5ea2",
                             data: [<%=fss %>]
                         }
                     ]
                 },
                 options: {
                     legend: { display: true },
                     title: {
                         display: false,
                         text: 'Employee by Male to Female Ratio'
                     }
                 }
             });

             new Chart(document.getElementById("bar-chart"), {
                 type: 'bar',
                 data: {
                     labels: [<%=Epopulation %>],

                     datasets: [
                         {
                             label: "",
                             backgroundColor: ["#0250f7", "#f7020e", "#f7c202", "#37f702", "#f702eb", "#0250f7", "#f7020e",],
                             //             data: [20,40,60,80,100]
                             data: [<%=avergarpopulationtotal %>]
                         }
                     ]
                 },
                 options: {
                     legend: { display: true },
                     title: {
                         display: false,
                         text: 'Employee Population Per Performance Score'


                     }
                 }
             });

             new Chart(document.getElementById("line-chart"), {
                 type: 'scatter',
                 data: {
                     //labels: [10, 15, 20, 3, 5],
                     datasets: [
                         {
                             label: "Level of skills",
                             borderColor: "#565656",

                             data: [{ x: 4, y: 10 }, { x: 10, y: 20 }, { x: 20, y: 30 }, { x: 70, y: 70 }]
                         },
                         {
                             label: "No of queries",
                             borderColor: "#00ffbf",

                             data: [{ x: 4, y: 10 }, { x: 32, y: 20 }, { x: 44, y: 30 }, { x: 58, y: 40 }]
                         },
                          {
                             label: "Attendance",
                             borderColor: "#4bc0c0",

                             data: [{ x: 12, y: 10 }, { x: 42, y: 20 }, { x: 54, y: 30 }, { x: 68, y: 40 }]
                         },
                         {
                             label: "Leave",
                             borderColor: "teal",

                             data: [{ x: 14, y: 30 }, { x: 22, y: 40 }, { x: 34, y: 50 }, { x: 68, y: 20 }]
                         },
                         {
                             label: "Demography of employment levels",
                             borderColor: "#bf00ff",

                             data: [{ x: 34, y: 50 }, { x: 32, y: 80 }, { x: 44, y: 60 }, { x: 18, y: 40 }]
                         }
                     ]
                 },
                 options: {
                     legend: { display: true },
                     title: {
                         display: true,
                         text: 'Performance Metrics'
                     },
                     scales: {
                         xAxes: [{
                             type: 'linear',

                         }]
                     }
                 }
             });
             new Chart(document.getElementById("line-chartq"), {
                  type: 'scatter',
                 data: {
                     //labels: [10, 15, 20, 3, 5],
                     datasets: [
                         {
                             label: "Invesment in training ",
                             borderColor: "#565656",

                             data: [{ x: 4, y: 10 }, { x: 10, y: 20 }, { x: 20, y: 30 }, { x: 70, y: 70 }]
                         },
                         {
                             label: "Salary increase",
                             borderColor: "#00ffbf",

                             data: [{ x: 4, y: 10 }, { x: 32, y: 20 }, { x: 44, y: 30 }, { x: 58, y: 40 }]
                         },
                          {
                             label: "Leave",
                             borderColor: "#4bc0c0",

                             data: [{ x: 12, y: 10 }, { x: 42, y: 20 }, { x: 54, y: 30 }, { x: 68, y: 40 }]
                         },
                         {
                             label: "Leave",
                             borderColor: "teal",

                             data: [{ x: 14, y: 30 }, { x: 22, y: 40 }, { x: 34, y: 50 }, { x: 68, y: 20 }]
                         },
                         {
                             label: "No of training scheduled and conducted",
                             borderColor: "#bf00ff",

                             data: [{ x: 34, y: 50 }, { x: 32, y: 80 }, { x: 44, y: 60 }, { x: 18, y: 40 }]
                         }
                     ]
                 },
                 options: {
                     legend: { display: true },
                     title: {
                         display: true,
                         text: 'Staff Turnover'
                     },
                     scales: {
                         xAxes: [{
                             type: 'linear',

                         }]
                     }
                 }
             });



         </script>

        ------------------<!--Compensation and Benefit-->---------------------------

        <script>


            new Chart(document.getElementById("bar-chart4"), {
                type: 'bar',
                data: {
                    labels: [<%=yearly %>],
                    datasets: [
                        {
                            label: "",
                            Format: "N0",
                            backgroundColor: ["#0250f7", "#f7020e", "#f7c202", "#0250f7", "#f7020e", "#f7c202", "#37f702", "#f702eb"],
                            data: [<%=amountYearly %>]
                        }
                    ]
                },
                options: {
                    legend: { display: true },
                    title: {
                        display: false,
                        text: 'Compensation and Benefit Analysis'


                    }
                }
            });

            new Chart(document.getElementById("pie-chart4"), {
                type: 'pie',
                data: {
                    labels: [<%=dept%>],
                    datasets: [{
                        label: "",
                        Format: "N0",
                        backgroundColor: ["#0250f7", "#f7020e", "#f7c202", "#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850", "#0250f7", "#f7020e"],
                        data: [<%=deptAmountYearly%>]
                    }]
                },
                options: {
                    legend: { display: true },
                    title: {
                        display: false,
                        text: 'Annual Compensation and Benefit'
                    }
                }
            });





            new Chart(document.getElementById("bar-charta4"), {
                type: 'bar',
                data: {
                    labels: [<%=amountMonthly %>],
                    datasets: [
                        {
                            label: "",
                            backgroundColor: ["#0250f7", "#f7020e", "#f7c202", "#0250f7", "#f7020e", "#f7c202", "#37f702", "#f702eb"],
                            data: [<%=monthly %>]
                        }
                    ]
                },
                options: {
                    legend: { display: true },
                    title: {
                        display: false,
                        text: 'Compensation and Benefit Analysis'


                    }
                }
            });

            new Chart(document.getElementById("pie-charta4"), {
                type: 'pie',
                data: {
                    labels: [<%=dept%>],
                    datasets: [{
                        label: "",
                        backgroundColor: ["#0250f7", "#f7020e", "#f7c202", "#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850", "#0250f7", "#f7020e"],
                        data: [<%=deptAmountMonthly%>]
                    }]
                },
                options: {
                    legend: { display: true },
                    title: {
                        display: false,
                        text: 'Annual Compensation and Benefit'
                    }
                }
            });

        </script>

        ------------------<!--Leaning and Development-->---------------------------
        <script>


            new Chart(document.getElementById("line-chart2"), {
                type: 'line',
                data: {
                    labels: [<%=AverageTraningInvestmentName %>],
                    datasets: [{
                        data: [<%=AverageTraningInvestmentNameScore %>],
                        label: "",
                        borderColor: "#3e95cd",
                        fill: true
                    }
                    ]
                },
                options: {
                    legend: { display: true },
                    title: {
                        display: false,
                        text: 'Average Training Investment'
                    }
                }
            });


            new Chart(document.getElementById("line-chart2b"), {
                type: 'line',
                data: {
                    labels: [<%=TraningTypeHourName %>],
                    datasets: [{
                        data: [<%=TraningTypeHourNameScore %>],
                        label: "Hours",
                        borderColor: "#3e95cd",
                        fill: true
                    }

                    ]
                },
                options: {
                    legend: { display: true },
                    title: {
                        display: false,
                        text: 'Global Training Hours per Training Type'
                    }
                }
            });

            new Chart(document.getElementById("bar-chart2a"), {
                type: 'bar',
                data: {
                    labels: [<%=TraningCostName %>],
                    datasets: [
                        {
                            label: "Cost",
                            backgroundColor: ["#0250f7", "#f7020e", "#f7c202", "#37f702", "#f702eb", "#0250f7", "#f7020e", "#f7c202", "#0250f7", "#f7020e", "#f7c202"],
                            data: [<%=TraningCostNameScore %>]
                        }
                    ]
                },
                options: {
                    legend: { display: true },
                    title: {
                        display: false,
                        text: 'Training cost year on year'


                    }
                }
            });

            new Chart(document.getElementById("pie-chart2"), {
                type: 'pie',
                data: {
                    labels: [<%=CompletionStatusName %>],
                    datasets: [{
                        label: "Status",
                        backgroundColor: ["#3e95cd", "#8e5ea2", "#c45850", "#0250f7", "#f7020e", "#0250f7", "#f7020e", "#f7c202", "#0250f7", "#f7020e", "#f7c202"],
                        data: [<%=CompletionStatusNameScore %>]
                    }]
                },
                options: {
                    legend: { display: true },
                    title: {
                        display: false,
                        text: 'Completion Status Analysis'
                    }
                }
            });


            new Chart(document.getElementById("bar-chart2"), {
                type: 'bar',
                data: {
                    labels: [<%=TraningHourName %>],
                    datasets: [
                        {
                            label: "Global Hours",
                            backgroundColor: ["#0250f7", "#f7020e", "#f7c202", "#0250f7", "#f7020e", "#f7c202", "#37f702", "#f702eb", "#0250f7", "#f7020e"],
                            data: [<%=TraningHourNameScore %>]
                        }
                    ]
                },
                options: {
                    legend: { display: true },
                    title: {
                        display: false,
                        text: 'Global Hours of Training'


                    }
                }
            });

             </script>

        ------------------<!--PMS Analytics-->---------------------------

        <script>

            new Chart(document.getElementById("pie-chart6"), {
                type: 'pie',
                data: {
                    labels: [<%=objective %>],
                    datasets: [{
                        label: "",
                        backgroundColor: ["#0250f7", "#8e5ea2", "#f7c202", "#f7020e", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850", "#0250f7", "#f7020e"],
                        data: [<%=objectiveCount %>]
                    }]
                },
                options: {
                    legend: { display: true },
                    title: {
                        display: false,
                        text: 'Objective Analysis'
                    }
                }
            });


            new Chart(document.getElementById("pie-chart6b"), {
                type: 'pie',
                data: {
                    labels: [<%=feedback %>],
                    datasets: [{
                        label: "",
                        backgroundColor: ["#0250f7", "#f7020e", "#f7c202", "#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850", "#0250f7", "#f7020e"],
                        data: [<%=feedbackCount %>]
                    }]
                },
                options: {
                    legend: { display: true },
                    title: {
                        display: false,
                        text: 'Feedback Analysis'
                    }
                }
            });



            new Chart(document.getElementById("pie-chart6c"), {
                type: 'pie',
                data: {
                    labels: [<%=performanceName %>],
                    datasets: [{
                        label: "",
                        backgroundColor: ["#0250f7", "#f7020e", "#f7c202", "#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850", "#0250f7", "#f7020e"],
                        data: [<%=PerformanceNameScore %>]
                    }]
                },
                options: {
                    legend: { display: true },
                    title: {
                        display: false,
                        text: 'Performance Analysis'
                    }
                }
            });


        </script>

        ------------------<!--Time and Leave Analytics-->---------------------------

        <script>

            new Chart(document.getElementById("bar-chart5b"), {
                type: 'bar',
                data: {
                    labels: [<%=office%>],

                    datasets: [
                        {
                            label: "",
                            backgroundColor: ["#0250f7", "#f7020e", "#f7c202", "#37f702", "#f702eb", "#0250f7", "#f7020e",],
                            data: [<%=leavedaystotal %>]
                        }
                    ]
                },
                options: {
                    legend: { display: true },
                    title: {
                        display: false,
                        text: 'Global Leave days'


                    }
                }
            });

            new Chart(document.getElementById("bar-chart5c"), {
                type: 'bar',
                data: {
                    labels: [<%=office %>],
                    datasets: [
                        {
                            label: "",
                            backgroundColor: ["#0250f7", "#f7020e", "#f7c202", "#37f702", "#f702eb", "#0250f7", "#f7020e",],
                            data: [<%=leavecosttotal %>]
                        }
                    ]
                },
                options: {
                    legend: { display: true },
                    title: {
                        display: false,
                        text: 'Global Leave Cost'


                    }
                }
            });

        </script>


        ------------------<!--Recruitment Analytics-->---------------------------

        <script>

            new Chart(document.getElementById("line-chart3"), {
                type: 'line',
                data: {
                    labels: [<%=WorkForceGrowthName %>],
                    datasets: [{
                        data: [<%=WorkForceGrowthNameScore %>],
                        label: "",
                        borderColor: "#3e95cd",
                        fill: false
                    }
                    ]
                },
                options: {
                    legend: { display: true },
                    title: {
                        display: false,
                        text: 'Workforce Growth Rate'
                    }
                }
            });


            //            ["Finance", "Stores", "Human Resource", "Procurement", "ICT", "Business Development", "Research and Development"],

            new Chart(document.getElementById("line-chart3b"), {
                type: 'line',
                data: {
                    labels: [<%=TurnoverRateName %>],
                    datasets: [{
                        data: [<%=TurnoverRateNameScore %>],
                        label: "",
                        borderColor: "#3e95cd",
                        fill: false
                    }
                    ]
                },
                options: {
                    legend: { display: true },
                    title: {
                        display: false,
                        text: 'Turnover rate'
                    }
                }
            });



            new Chart(document.getElementById("pie-chart3a"), {
                type: 'pie',
                data: {
                    labels: [<%=ReasonName %>],
                    datasets: [{
                        label: "Score for current year",
                        backgroundColor: ["#0250f7", "#f7020e", "#f7c202", "#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850", "#0250f7", "#f7020e"],
                        data: [<%=ReasonNameTotal %>]
                    }]
                },
                options: {
                    legend: { display: true },
                    title: {
                        display: false,
                        text: 'Leavers Analysis by Reason'
                    }
                }
            });

//               new Chart(document.getElementById("bar-chart-grouped3a"), {
//                     type: 'bar',
//                     data: {
//                         labels: ["2016", "2017", "2018"],
//                         datasets: [
//        {
//            label: "Death",
//            backgroundColor: "#3e95cd",
//            data: [2, 5, 9]
//        }, {
//            label: "Dismissal",
//            backgroundColor: "#8e5ea2",
//            data: [5, 11, 3]
//        }
//        , {
//            label: "Corporate Restructuring",
//            backgroundColor: "#3cba9f",
//            data: [20, 2, 5]
//        }
//        , {
//            label: "Others",
//            backgroundColor: "#c45850",
//            data: [6, 1, 9]
//        }
//      ]
//                     },
//                     options: {
//                         legend: { display: false },
//                         title: {
//                             display: false,
//                             text: 'Leavers Analysis by Reason'
//                         }
//                     }
//                 });

//new Chart(document.getElementById("mixed-chart3"), {
//    type: 'bar',
//    data: {
//      labels: [<%=years %>],
//      datasets: [{
//          label: "Joiners",
//          type: "line",
//          borderColor: "#8e5ea2",
//          data: [<%=joiners %>],
//          fill: false
//        }, {
//          label: "Leavers",
//          type: "line",
//          borderColor: "#3e95cd",
//          data: [<%=leavers %>],
            //          fill: false
            //        }

            //      ]
            //    },
            //    options: {
            //      title: {
            //        display: false,
            //        text: 'Joiners/Leavers (year on year)'
            //      },
            //      legend: { display: false }
            //    }
            //});

            new Chart(document.getElementById("bar-chart-grouped3"), {
                type: 'bar',
                data: {
                    labels: [<%=years %>],
                    datasets: [
                        {
                            label: "Joiners",
                            backgroundColor: ["#0250f7"],
                            data: [<%=joiners %>]
                         }, {
                             label: "Leavers",
                             backgroundColor: ["#f7020e"],
                             data: [<%=leavers %>]
                        }
                    ]
                },
                options: {
                    legend: { display: true },
                    title: {
                        display: false,
                        text: 'Joiners/Leavers (year on year)'
                    }
                }
            });


            new Chart(document.getElementById("bar-chart3"), {
                type: 'bar',
                data: {
                    labels: [<%=LeaversLengthOfServiceName %>],

                    datasets: [
                        {
                            label: "Length of Service",
                            backgroundColor: ["#0250f7", "#f7020e", "#f7c202", "#37f702", "#f702eb", "#0250f7", "#f7020e", "#0250f7", "#f7020e", "#f7c202"],
                            data: [<%=LeaversLengthOfServiceNameScore %>]
                        }
                    ]
                },
                options: {
                    legend: { display: true },
                    title: {
                        display: false,
                        text: 'Leavers Analysis by Length of Service (Year on Year)'


                    }
                }
            });

            new Chart(document.getElementById("pie-chart3"), {
                type: 'pie',
                data: {
                    labels: [<%=LeaversPerformanceName %>],
                    datasets: [{
                        label: "Score for current year",
                        backgroundColor: ["#0250f7", "#f7020e", "#f7c202", "#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850", "#0250f7", "#f7020e",],
                        data: [<%=LeaversPerformanceNameScore %>]
                    }]
                },
                options: {
                    legend: { display: true },
                    title: {
                        display: false,
                        text: 'Leavers Analysis by Performance Score'
                    }
                }
            });

        </script>



</asp:Content>
