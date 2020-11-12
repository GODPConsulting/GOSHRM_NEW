﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/smartHR.Master" CodeBehind="hrdashboard2.aspx.vb" Inherits="GOSHRM.hrdashboard2" %>
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
      <table class="details-table">
        <thead>
          <tr>
            <th>Office</th>
            <th>Number of Employee</th>
            <th>Location</th>
            <th>Performance Rating</th>
          </tr>
        </thead>
        <tbody id ="companytb" runat="server">
          <tr>
            <td class="table-data"><a onclick='viewdashboardInfo()' href="#">Finance</a></td>
            <td class="table-data">200</td>
            <td class="table-data">Ikeja</td>
            <td class="table-data">98%</td>
          </tr>
        </tbody>
      </table>
      <div style="margin-left:13%; width:88%" class="manager-modal" id="myModal">
        <div class="manager-modal-content">
          <span class="manager-close">&times;</span>
          <div class="hr-card-container">
            <div class="hr-card">
              <h3 class="content-card-header">Recruitment & Onboarding</h3>
              <div class="rating-container">
                <span class="rating-percent">80%</span>
                <span>Workforce</span>
              </div>
              <div>
                <div class="hr-item-container">
                  <div class="hr-item">
                    <span id="workforceplan">0</span><span>Workforce Plan</span>
                  </div>
                  <div class="hr-item">Initiate Workforce Plan</div>
                </div>
                <div class="hr-item-container">
                  <div class="hr-item">
                    <span id="staffRequest">0</span><span>Staff Request</span>
                  </div>
                  <div class="hr-item">
                    <span id="jobPortal">0</span><span>Job Portal</span>
                  </div>
                </div>
                <div class="hr-item-container">
                  <div class="hr-item">
                    <span id="recruitmentTest">0</span><span>Recruitment Test</span>
                  </div>
                  <div class="hr-item">
                    <span id="interview">0</span><span>Interviews</span>
                  </div>
                </div>
              </div>
            </div>
            <div class="hr-card">
              <h3 class="content-card-header">Employee</h3>
              <div class="rating-container">
                <span id="rating" class="rating-percent">80</span>%
                <span>Turnover</span>
              </div>
              <div>
                <div class="hr-item-container">
                  <div class="hr-item">
                    <span id="employeeDataset">0</span><span>Employee Dataset</span>
                  </div>
                  <div class="hr-item">
                    <span id="employeeConfirmation">0</span><span>Employee Confirmation</span>
                  </div>
                </div>
                <div class="hr-item-container">
                  <div class="hr-item">
                    <span id="successionPlan">0</span><span>Successor Plan</span>
                  </div>
                  <div class="hr-item">
                    <span id="promotion">0</span><span>Promotion</span>
                  </div>
                </div>
                <div class="hr-item-container">
                  <div class="hr-item">
                    <span id="employeeExit">0</span><span>Employee Exit</span>
                  </div>
                  <div class="hr-item"><span id="hmo">0</span><span>HMO</span></div>
                </div>
              </div>
            </div>
            <div class="hr-card">
              <h3 class="content-card-header">Performance & Development</h3>
              <div style="display: flex">
                <div class="hr-item">
                  <span id="performanceRating">80</span>%<span>Perf Rating</span>
                </div>
                <div class="manager-pipe"></div>
                <div class="hr-item">
                  <span id="compentenceRating">0</span>%
                  <span>Competence Rating</span>
                </div>
              </div>
              <div>
                <div class="hr-item-container">
                  <div class="hr-item">
                    <span></span><span>Development Activities</span>
                  </div>
                  <div class="hr-item">
                    <span>Initiate</span><span>Development Session</span>
                  </div>
                </div>
                <div class="hr-item-container">
                  <div class="hr-item">
                    <span>Create</span><span>App Cycle</span>
                  </div>
                  <div class="hr-item">
                    <span>View</span><span>App Cycle</span>
                  </div>
                </div>
                <div class="hr-item-container">
                  <div class="hr-item"><span id="queries">0</span><span>Querries</span></div>
                  <div class="hr-item"><span></span><span></span></div>
                </div>
              </div>
            </div>
            <div class="hr-card">
              <h3 class="content-card-header">Compensation & Benefit</h3>
              <div style="display: flex">
                <div class="hr-item"><span id="payroll">0</span><span>Payroll</span></div>
                <div class="manager-pipe"></div>
                <div class="hr-item">
                  <span>Initiate</span>
                  <span>Payroll</span>
                </div>
              </div>
              <div>
                <div class="hr-item-container">
                  <div class="hr-item">
                    <span id="terminalBenefit">0</span><span>Terminal Benefits</span>
                  </div>
                  <div class="hr-item">
                    <span id="staffLoan">0</span><span>Staff Loans</span>
                  </div>
                </div>
                <div class="hr-item-container">
                  <div class="hr-item">
                    <span id="leaveAllowance">0</span><span>Leave Allow.</span>
                  </div>
                  <div class="hr-item">
                    <span id="overTimeRequest">0</span><span>Overtime Requests</span>
                  </div>
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
                                console.log(company)
                                $.ajax({
                                    url: '../gos.asmx/HRDashboardData',
                                    method: 'post',
                                    dataType: 'json',
                                    data: { companyName: company },
                                    success: function (data) {
                                        $(data).each(function (index, sup) {
                                            $('#workforceplan').val(sup.workforceplan);
                                            $('#staffRequest').val(sup.staffRequest);
                                            $('#jobPortal').val(sup.jobPortal);
                                            $('#recruitmentTest').val(sup.recruitmentTest);
                                            $('#interview').val(sup.interview);
                                            $('#employeeDataset').val(sup.employeeDataset);
                                            $('#employeeConfirmation').val(sup.employeeConfirmation);
                                            $('#successionPlan').val(sup.successionPlan);
                                            $('#promotion').val(sup.promotion);
                                            $('#employeeExit').val(sup.employeeExit);
                                            $('#performanceRating').val(sup.performanceRating);
                                            $('#compentenceRating').val(sup.compentenceRating);
                                            $('#queries').val(sup.queries);
                                            $('#payroll').val(sup.payroll);
                                            $('#terminalBenefit').val(sup.terminalBenefit);
                                            $('#staffLoan').val(sup.staffLoan);
                                            $('#leaveAllowance').val(sup.leaveAllowance);
                                            $('#overTimeRequest').val(sup.overTimeRequest);
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