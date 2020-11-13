<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/smartHR.Master" CodeBehind="hrdashboard2.aspx.vb" Inherits="GOSHRM.hrdashboard2" %>
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
                    <a href="<%= Page.ResolveClientUrl("~/manager")%>">Manager Dashboard</a>
                </li>
               <li style="display:none" class="link_hr">
                    <a href="<%= Page.ResolveClientUrl("~/hrdashboard2")%>" style="border-bottom: 2px solid teal; padding-bottom: 5px">HR Dashboard</a>
                </li>
                <li style="display:none" class="link_hr">
                    <a href="<%= Page.ResolveClientUrl("~/hrdashboard")%>">HR Analytics</a>
                </li>
            </ul>
        </div>
         <div class="panel panel-success">
                                <div class="panel-heading">
                                    <h4><b>HR Dashboard</b></h4>
                                </div>
                                <div class="panel-body">

					<div class="card-box m-b-0">
                            <div class="hr-wrapper">
                                
        <table class="table table-striped custom-table datatable dataTable no-footer" id="DataTables_Table_0" role="grid" aria-describedby="DataTables_Table_0_info">
									<thead>
										<tr role="row"><th style="width: 206px;" class="sorting_asc" tabindex="0" aria-controls="DataTables_Table_0" rowspan="1" colspan="1" aria-sort="ascending" aria-label="Name: activate to sort column descending"><b>Office</b></th>
                                            <th class="sorting" tabindex="0" aria-controls="DataTables_Table_0" rowspan="1" colspan="1" aria-label="Employee ID: activate to sort column ascending" style="width: 206px;"><b>Number of Employees</b></th>
                                            <th class="sorting" tabindex="0" aria-controls="DataTables_Table_0" rowspan="1" colspan="1" aria-label="Email: activate to sort column ascending" style="width: 206px;"><b>Location</b></th>
                                            <th class="sorting" tabindex="0" aria-controls="DataTables_Table_0" rowspan="1" colspan="1" aria-label="Mobile: activate to sort column ascending" style="width: 206px;"><b>Performance Rating</b></th>
                                            <th class="sorting" tabindex="0" aria-controls="DataTables_Table_0" rowspan="1" colspan="1" aria-label="Role: activate to sort column ascending" style="width: 124.4px;"><b>Action</b></th>
										</tr>
									</thead>
									<tbody id ="companytb" runat="server">

                                    </tbody>
								</table>
<%--      <table class="details-table">
        <thead>
          <tr>
            <th>Office</th>
            <th>Number of Employee</th>
            <th>Location</th>
            <th>Performance Rating</th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <td class="table-data"><a onclick='viewdashboardInfo()' href="#">Finance</a></td>
            <td class="table-data">200</td>
            <td class="table-data">Ikeja</td>
            <td class="table-data">98%</td>
          </tr>
        </tbody>
      </table>--%>

      <div style="margin-left:13%; width:88%" class="manager-modal" id="myModal">
        <div class="manager-modal-content">
          <span class="manager-close">&times;</span>
          <div class="hr-card-container">
            <div class="hr-card">
              <h3 class="content-card-header">Recruitment & Onboarding</h3>
              <div class="rating-container">
                <span id="workforceCount" class="rating-percent">80%</span>
                <span>Workforce</span>
              </div>
              <div>
                <div class="hr-item-container">
                  <a href="<%= Page.ResolveClientUrl("~/Module/Recruitment/WorkForceBudget.aspx")%>"><div class="hr-item">
                    <span id="workforceplan">0</span><span>Workforce Plan</span>
                  </div></a>
                 <a href="<%= Page.ResolveClientUrl("~/Module/Recruitment/WorkForceBudgetUpdate")%>"><div class="hr-item">Initiate Workforce Plan</div></a>
                </div>
                <div class="hr-item-container">
                  <a href="<%= Page.ResolveClientUrl("~/Module/Recruitment/StaffRequisitionForm.aspx")%>"><div class="hr-item">
                    <span id="staffRequest">0</span><span>Staff Request</span>
                  </div></a>
                  <a href="<%= Page.ResolveClientUrl("~/Module/Recruitment/JobPostings.aspx")%>"><div class="hr-item">
                    <span id="jobPortal">0</span><span>Job Posting</span>
                  </div></a>
                </div>
                <div class="hr-item-container">
                  <a href="<%= Page.ResolveClientUrl("~/Module/Recruitment/JobTests.aspx")%>"><div class="hr-item">
                    <span id="recruitmentTest">0</span><span>Recruitment Test</span>
                  </div></a>
                  <a href="<%= Page.ResolveClientUrl("~/Module/Recruitment/JobInterviews.aspx")%>"><div class="hr-item">
                    <span id="interview">0</span><span>Interviews</span>
                  </div></a>
                </div>
              </div>
            </div>
            <div class="hr-card">
              <h3 class="content-card-header">Employee</h3>
              <div class="rating-container">
                <span id="turnoverCount" class="rating-percent">80%</span>
                <span>Turnover</span>
              </div>
              <div>
                <div class="hr-item-container">
                   <a href="<%= Page.ResolveClientUrl("~/Module/Employee/Employees.aspx")%>"><div class="hr-item">
                    <span id="employeeDataset">0</span><span>Employee Data</span>
                  </div></a>
                  <a href="<%= Page.ResolveClientUrl("~/Module/Recruitment/EmployeeConfirmation.aspx")%>"><div class="hr-item">
                    <span id="employeeConfirmation">0</span><span>Employee Confirmation</span>
                  </div></a>
                </div>
                <div class="hr-item-container">
                  <a href="<%= Page.ResolveClientUrl("~/Module/Recruitment/SuccessionPlan.aspx")%>"><div class="hr-item">
                    <span id="successionPlan">0</span><span>Successor Plan</span>
                  </div></a>
                  <a href="<%= Page.ResolveClientUrl("~/Module/Recruitment/Promotions.aspx")%>"><div class="hr-item">
                    <span id="promotion">0</span><span>Promotion</span>
                  </div></a>
                </div>
                <div class="hr-item-container">
                  <a href="<%= Page.ResolveClientUrl("~/Module/Recruitment/Terminations.aspx")%>"><div class="hr-item">
                    <span id="employeeExit">0</span><span>Employee Exit</span>
                  </div></a>
                  <a href="<%= Page.ResolveClientUrl("~/Module/Recruitment/HMOs.aspx")%>"><div class="hr-item"><span id="hmo">0</span><span>HMO</span></div></a>
                </div>
              </div>
            </div>
            <div class="hr-card">
              <h3 class="content-card-header">Performance & Development</h3>
              <div style="display: flex">
                <div class="hr-item">
                  <span id="performanceRating">80%</span><span>Perf Rating</span>
                </div>
                <div class="manager-pipe"></div>
                <div class="hr-item">
                  <span id="compentenceRating">0%</span>
                  <span>Competence Rating</span>
                </div>
              </div>
              <div>
                <div class="hr-item-container">
                  <a href="<%= Page.ResolveClientUrl("~/Module/Trainings/Settings/Courses.aspx")%>"><div class="hr-item">
                    <span></span><span>Development Activities</span>
                  </div></a>
                  <a href="<%= Page.ResolveClientUrl("~/Module/Trainings/Settings/TrainingSessions.aspx")%>"><div class="hr-item">
                    <span>Initiate</span><span>Development Session</span>
                  </div></a>
                </div>
                <div class="hr-item-container">
                  <a href="<%= Page.ResolveClientUrl("~/Module/Performance/Settings/AppraisalPeriodUpdate")%>"><div class="hr-item">
                    <span>Create</span><span>Perf Cycle</span>
                  </div></a>
                  <a href="<%= Page.ResolveClientUrl("~/Module/Performance/Settings/AppraisalPeriodList.aspx")%>"><div class="hr-item">
                    <span>View</span><span>Perf Cycle</span>
                  </div></a>
                </div>
                <div class="hr-item-container">
                   <a href="<%= Page.ResolveClientUrl("~/Module/Performance/Queries.aspx")%>"><div class="hr-item"><span id="queries">0</span><span>Queries</span></div></a>
                  <div class="hr-item"><span></span><span></span></div>
                </div>
              </div>
            </div>
            <div class="hr-card">
              <h3 class="content-card-header">Compensation & Benefit</h3>
              <div style="display: flex">
                <a href="<%= Page.ResolveClientUrl("~/Module/Finance/Payroll/PayrollPeriod.aspx")%>"><div class="hr-item"><span id="payroll">0</span><span>Payroll</span></div></a>
                <div class="manager-pipe"></div>
                <a href="<%= Page.ResolveClientUrl("~/Module/Finance/Payroll/SalaryPayslipGenerate")%>"><div class="hr-item">
                  <span>Initiate</span>
                  <span>Payroll</span>
                </div></a>
              </div>
              <div>
                <div class="hr-item-container">
                    <a href="<%= Page.ResolveClientUrl("~/Module/Finance/Payroll/TerminalBenefits.aspx")%>"><div class="hr-item">
                        <span id="terminalBenefit">0</span><span>Terminal Benefits</span>
                    </div></a>
                  <a href="<%= Page.ResolveClientUrl("~/Module/Finance/Loans/StaffLoans.aspx")%>"><div class="hr-item">
                    <span id="staffLoan">0</span><span>Staff Loans</span>
                  </div></a>
                </div>
                <div class="hr-item-container">
                  <a href="<%= Page.ResolveClientUrl("~/Module/TimeManagement/EmployeeLeaves.aspx")%>"><div class="hr-item">
                    <span id="leaveAllowance">0</span><span>Leave Allow.</span>
                  </div></a>
                  <a href="<%= Page.ResolveClientUrl("~/Module/TimeManagement/Attendance.aspx")%>"><div class="hr-item">
                    <span id="overTimeRequest">0</span><span>Overtime Requests</span>
                  </div></a>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <script type="text/javascript">
        var modal = document.getElementById("myModal");

        // Get the button that opens the modal
        var btn = document.querySelectorAll(".table-data.td");

        // Get the <span> element that closes the modal
        var span = document.getElementsByClassName("manager-close")[0];

        btn.forEach(el => {
            el.addEventListener('click', (e) => {
                let id = e.target.id
                modal.style.display = "block";
            })
        })

        // When the user clicks on <span> (x), close the modal
        span.onclick = function () {
            modal.style.display = "none";
        };

        // When the user clicks anywhere outside of the modal, close it
        window.onclick = function (event) {
            if (event.target == modal) {
                modal.style.display = "none";
            }
        };
    </script>
                        <script type="text/javascript">
                            function viewdashboardInfo(company) {
                                var workforce = document.getElementById("workforceplan");
                                var staffRequest = document.getElementById("staffRequest");
                                var jobPortal = document.getElementById("jobPortal");
                                var recruitmentTest = document.getElementById("recruitmentTest");
                                var interview = document.getElementById("interview");
                                var employeeDataset = document.getElementById("employeeDataset");
                                var employeeConfirmation = document.getElementById("employeeConfirmation");
                                var successionPlan = document.getElementById("successionPlan");
                                var promotion = document.getElementById("promotion");
                                var employeeExit = document.getElementById("employeeExit");
                                var performanceRating = document.getElementById("performanceRating");
                                var compentenceRating = document.getElementById("compentenceRating");
                                var queries = document.getElementById("queries");
                                var payroll = document.getElementById("payroll");
                                var terminalBenefit = document.getElementById("terminalBenefit");
                                var staffLoan = document.getElementById("staffLoan");
                                var leaveAllowance = document.getElementById("leaveAllowance");
                                var overTimeRequest = document.getElementById("overTimeRequest");
                                var hmo = document.getElementById("hmo");
                                var turnoverCount = document.getElementById("turnoverCount");
                                var workforceCount = document.getElementById("workforceCount");
                                workforce.innerText = "";                               
                                staffRequest.innerText = "";                               
                                jobPortal.innerText = "";                               
                                recruitmentTest.innerText = "";                               
                                interview.innerText = "";                               
                                queries.innerText = "";                               
                                employeeConfirmation.innerText = "";                               
                                successionPlan.innerText = "";                               
                                promotion.innerText = "";                               
                                employeeExit.innerText = "";                               
                                performanceRating.innerText = "";                               
                                compentenceRating.innerText = "";                               
                                payroll.innerText = "";                               
                                terminalBenefit.innerText = "";                               
                                staffLoan.innerText = "";                               
                                leaveAllowance.innerText = "";                               
                                overTimeRequest.innerText = "";
                                hmo.innerText = "";
                                turnoverCount.innerText = "";
                                workforceCount.innerText = "";
                                $.ajax({
                                    url: 'res_new/gos.asmx/HRDashboardData',
                                    method: 'post',
                                    dataType: 'json',
                                    data: { companyName: company },
                                    success: function (data) {
                                        $(data).each(function (index, sup) {
                                            workforce.innerText = sup.workforceplan;
                                            staffRequest.innerText = sup.staffRequest;
                                            jobPortal.innerText = sup.jobPortal;
                                            recruitmentTest.innerText = sup.recruitmentTest;
                                            interview.innerText = sup.interview;
                                            employeeDataset.innerText = sup.employeeDataset;
                                            employeeConfirmation.innerText = sup.employeeConfirmation;
                                            successionPlan.innerText = sup.successionPlan;
                                            promotion.innerText = sup.promotion;
                                            employeeExit.innerText = sup.employeeExit;
                                            performanceRating.innerText = sup.performanceRating;
                                            compentenceRating.innerText = sup.compentenceRating;
                                            queries.innerText = sup.queries;
                                            payroll.innerText = sup.payroll;
                                            terminalBenefit.innerText = sup.terminalBenefit;
                                            staffLoan.innerText = sup.staffLoan;
                                            leaveAllowance.innerText = sup.leaveAllowance;
                                            overTimeRequest.innerText = sup.overTimeRequest;
                                            turnoverCount.innerText = sup.turnoverCount;
                                            workforceCount.innerText = sup.workforceCount;
                                            hmo.innerText = sup.hmo;
                                        });
                                    },
                                    error: function (err) {
                                        console.log(JSON.stringify(err));
                                    }
                                });
                            }
                        </script>
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
