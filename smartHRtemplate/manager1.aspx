<%@ Page Language="vb"    CodeBehind="manager1.aspx.vb" Inherits="GOSHRM.DummyGetter"%>

<!DOCTYPE html>
<!--
* CoreUI - Free Bootstrap Admin Template
* @version v2.0.0
* @link https://coreui.io
* Copyright (c) 2018 GOS HRM Łukasz Holeczek
* Licensed under MIT (https://coreui.io/license)
-->

<html lang="en">
  <head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, shrink-to-fit=no">
    <meta name="description" content="CoreUI - Open Source Bootstrap Admin Template">
    <meta name="author" content="Łukasz Holeczek">
    <meta name="keyword" content="Bootstrap,Admin,Template,Open,Source,jQuery,CSS,HTML,RWD,Dashboard">
    <title>Manager 1</title>
    <!-- Icons-->
    <link href="<%= Page.ResolveClientUrl("~/res_new/vendors/@coreui/icons/css/coreui-icons.min.css") %>" rel="stylesheet">
    <link href="res_new/vendors/flag-icon-css/css/flag-icon.min.css" rel="stylesheet">
    <link href="res_new/vendors/font-awesome/css/font-awesome.min.css" rel="stylesheet">
    <link href="res_new/vendors/simple-line-icons/css/simple-line-icons.css" rel="stylesheet">
    <!-- Main styles for this application-->
    <link href="res_new/css/style.css" rel="stylesheet">
    <link href="res_new/vendors/pace-progress/css/pace.min.css" rel="stylesheet">

        
  </head>
  <body class="app header-fixed sidebar-fixed aside-menu-fixed sidebar-lg-show">
  <%-- <%="" %>
    <div class="app-bodys">
   
      <main class="mains" style="width:100%;>
        <!-- Breadcrumb-->
 
        <div class="container-fluid" ">
          <div class="animated fadeIn">
            
            <!--/.row-->
 
            <!--/.card-->

           
            <!--/.row-->

            <div class="row">
              <div class="col-md-12">
                <div class="card">
                  <div class="card-header">
                   My Team
                  </div>
                  <div class="card-body" style="padding-top:0px;">
                    <table class="table table-responsive-sm table-hover table-outline mb-0">
                      <thead class="thead-light">
                        <tr>
                          <th>
                            <i class="icon-people"></i>
                          </th>
                          <th>Name</th>
                          <th>Company</th>
                          <th>Department</th>
                          <th>Job Title</th>
                          <th>Job Grade</th><th>Actions</th>
                        </tr>
                      </thead>
                      <tbody>
                   
                         
                 
                   
                       
                       
                   
                         <% For Each x In Session("alldetail")%>
					    
                               <tr><td class="text-center">
                            <div class="avatar">
                              <img src="res_new/img/avatars/7.jpg" class="img-avatar" alt="picture">
                              <span class="avatar-status badge-success"></span>
                            </div>
                          </td>

<td>
                             <%=x(0) %> <%=x(1) %> 
                            
                               
                           
                          </td>


  
                          <td>
                             <%=x(2)%>
                          </td>
                          <td>
                         <%=x(3) %>
                          </td>
                          <td>
                         <%=x(4) %>
                          </td>

                            <td>
                         <%=x(5)%>
                          </td>

                          <td>
                          <button id="<%=x(0) %><%=x(1) %>1" type="button" class="btn btn-primary" data-toggle="collapse" data-target="#<%=x(0) %><%=x(1) %>" aria-expanded="false" aria-controls="<%=x(0) %><%=x(1) %>" 
                          
                          
                          value="<% 
                          
                          Dim nam as String = x(0) & " " & x(1)

                          Dim inam="eid="& x(6) & "&name=" &nam
                          response.write(inam)
                          
                          %>">
  Details
</button>

                          
                          </td>


                  
  

  
                        </tr>
                        <br />
                      

                      <tr><td colspan="8">
    <div class="collapse" id="<%=x(0) %><%=x(1) %>">
                     
   
    <table class="table table-responsive-sm table-hover table-outline mb-0">
                         <thead class="thead-light">
                           <tr>
                             
                            
                             
                             <th>Succession Plan</th>
                             <th>Succession Activities</th>
                             <th>Succession Status</th>
                             <th>Development Plan Activities</th>
                             <th>Training</th><th>Queries</th> 
   
                               <th>Performance</th><th>Job History</th>
                           </tr>
                         </thead>
                         <tbody>
                         <tr>
                         <td id="sp<%=x(0) %><%=x(1) %>"></td>
                         <td id="sa<%=x(0) %><%=x(1) %>"></td>
                         <td id="ss<%=x(0) %><%=x(1) %>"></td>
                         <td id="dp<%=x(0) %><%=x(1) %>"></td>
                         <td id="trr<%=x(0) %><%=x(1) %>"></td>
                         <td id="qu<%=x(0) %><%=x(1) %>"></td>
                         <td id="pe<%=x(0) %><%=x(1) %>"></td>
                         <td id="jh<%=x(0) %><%=x(1) %>"></td></tr>
                         </tbody>
   
                         </table>
   
     <span id='details'>..</span>
   
      
   
   
     <script>


    (function () {
    //http requests and some mazy string splittting
 
        function getDetails() {
            var bid = document.getElementById("<%=x(0) %><%=x(1) %>1").value;



            if (window.XMLHttpRequest) {// code for IE7+, Firefox, Chrome, Opera, Safari
                xmlhttp = new XMLHttpRequest();
            }
            else {// code for IE6, IE5
                xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
            }
            xmlhttp.onreadystatechange = function () {
                if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
                    document.getElementById('details').innerHTML = ''
                    var als= xmlhttp.responseText
                    var alls = als.replace(/<(?:.|\n)*?>/gm, '')
                    var allst = alls.split('-')

                    var allstr = allst[0].split(",");
                    console.log(allst[0])
                    var allstr2 = allst[1].split(",");
                    var allstr3 = allst[2].split(",");
                    var allt = alls.split('*')
                    
                    //var allperfi = alls.split('>')

                    var allperf = allt[3].split(',')

                    allperfglobal= allperf
 
                    var allq1 = allt[1].split('-')
                    
                    var allqu = allq1[0].split(',')
                    //var allqu = allq2[0]


                    var alltr = allt[2].split(',')
                    
//training split
                    var dumtrain = ''
                    function myFunction(item) {
                       var itemr = item.split('/')
                        
                      var  item2=itemr[0]


                      item = item2.replace("-Not-Attended", ' <i class="fa fa-minus-square fa-lg mt-4" style="color:yellow; font-size:17px"></i>');
                        console.log(item)
                        item = item.replace("-Attended", ' <i class="fa fa-check-square fa-lg mt-4" style="color:green;font-size:17px"></i>');
                      
                        
                 
                        dumtrain = dumtrain + "<a href='/Module/Trainings/Portal/TrainingParticipants?id="+itemr[1]+"'> "+ item + "</b><br/>";
                    }

                    alltr.forEach(myFunction)
                    document.getElementById('trr<%=x(0) %><%=x(1) %>').innerHTML = dumtrain





                    //query split


                    var itemq1= ''
                     var itemq2 = ''
                     
                    
                    itemq1 = allqu[0].replace("Inprogress  :", ' <i class="fa fa-minus-square fa-lg mt-4" style="color:yellow; font-size:17px"></i>');
                    itemq2 = allqu[1].replace("Completed : ", ' <i class="fa fa-check-square fa-lg mt-4" style="color:green;font-size:17px"></i>');




                    document.getElementById('qu<%=x(0) %><%=x(1) %>').innerHTML = "<a href='/Module/Employee/Performance/Query.aspx'>" + itemq1 + "<br/> " + itemq2 + "<br/></a>";


                    console.log(allstr[2])

                    document.getElementById('sp<%=x(0) %><%=x(1) %>').innerHTML = "<a href='/Module/Employee/Recruitment/successionplanupdate.aspx?appid=" +allstr[3] +"'>" + (allstr[0] == null ? '' : allstr[0]) + "</a>";
                    document.getElementById('sa<%=x(0) %><%=x(1) %>').innerHTML = "<a href='/Module/Employee/Recruitment/successionplanupdate.aspx?appid=" + allstr[3] + "'>" + (allstr[2] == null ? '' : allstr[2]) + "</a>";
                    document.getElementById('ss<%=x(0) %><%=x(1) %>').innerHTML = "<a href='/Module/Employee/Recruitment/successionplanupdate.aspx?appid=" + allstr[3] + "'>" + (allstr[0] == null ? '' : allstr[1]) + "</a>";
                    document.getElementById('dp<%=x(0) %><%=x(1) %>').innerHTML = " <a class='btn btn-success' href='#' >" + allstr2[0] + "</a>"


                    document.getElementById('pe<%=x(0) %><%=x(1) %>').innerHTML = "" + allperf[0] + " <br/>" + allperf[1];






                   // document.getElementById('qu<%=x(0) %><%=x(1) %>').innerHTML = " <a class='btn btn-success' href='' > Queries " + allstr2[3] + "</a>"
                    document.getElementById('jh<%=x(0) %><%=x(1) %>').innerHTML = "<div style='border-left: 3px solid green;height: auto; margin-right:5px;'> " + ' - <b> ' + allstr3[0] + '</b><br/>' + ' - ' + allstr3[1] + '<br/>' + ' - ' + allstr3[2] + '<br/>' + ' - ' + allstr3[3] + ' ' + allstr3[4] + ' - ' + +allstr3[5] + '</div>';


                }

                else {
                    document.getElementById('details').innerHTML = 'not available'
                }
            }
            xmlhttp.open("GET", 'getempdetails.aspx?' + bid, false);
            xmlhttp.send();
           
        }
        getDetails()
    })();

     
    </script>
      
 
  </div></td></tr>

        


                        <% Next %>
                        
                      
                       
                       


 
                      </tbody>
                    </table>
                  </div>
                </div>
              </div>
              <!--/.col-->
            </div>



<!-- approvals widgets-->
<div class="row">

<div class="col-sm-12">
	<div class="card">
                  <div class="card-header">
                   Approvals
                  </div>
                  
                  <div class="card-body">
                      <div class="row">    
          
	<div class="col-6 col-lg-3">
                <div class="card">
                  <div class="card-body p-3 d-flex align-items-center">
                    <i class="fa fa-cogs bg-primary p-3 font-2xl mr-3"></i>
                    <div>
                      <div class="text-value-sm text-primary"><%=requisition %></div>
                      <div class="text-muted text-uppercase font-weight-bold small">Staff Requisition</div>
                    </div>
                  </div>
                  <div class="card-footer px-3 py-2">
                    <a class="btn-block text-muted d-flex justify-content-between align-items-center" href="/Module/Employee/Recruitment/StaffRequisitionApprove.aspx">
                      <span class="small font-weight-bold">View More</span>
                      <i class="fa fa-angle-right"></i>
                    </a>
                  </div>
                </div>
              </div>
              
              <div class="col-6 col-lg-3">
                <div class="card">
                  <div class="card-body p-3 d-flex align-items-center">
                    <i class="fa fa-cogs bg-primary p-3 font-2xl mr-3"></i>
                    <div>
                      <div class="text-value-sm text-primary"><%=wfplanning %></div>
                      <div class="text-muted text-uppercase font-weight-bold small">Workforce Planning</div>
                    </div>
                  </div>
                  <div class="card-footer px-3 py-2">
                    <a class="btn-block text-muted d-flex justify-content-between align-items-center" href="/Module/Employee/Recruitment/WorkForcePlanning.aspx">
                      <span class="small font-weight-bold">View More</span>
                      <i class="fa fa-angle-right"></i>
                    </a>
                  </div>
                </div>
              </div>
              
              <div class="col-6 col-lg-3">
                <div class="card">
                  <div class="card-body p-3 d-flex align-items-center">
                    <i class="fa fa-cogs bg-primary p-3 font-2xl mr-3"></i>
                    <div>
                      <div class="text-value-sm text-primary"><%=performancea%></div>
                      <div class="text-muted text-uppercase font-weight-bold small">Appraisal Objectives</div>
                    </div>
                  </div>
                  <div class="card-footer px-3 py-2">
                    <a class="btn-block text-muted d-flex justify-content-between align-items-center" href="/Module/Employee/Performance/CoacheeAppraisalObjectives.aspx">
                      <span class="small font-weight-bold">View More</span>
                      <i class="fa fa-angle-right"></i>
                    </a>
                  </div>
                </div>
              </div>
              
              
              <div class="col-6 col-lg-3">
                <div class="card">
                  <div class="card-body p-3 d-flex align-items-center">
                    <i class="fa fa-cogs bg-primary p-3 font-2xl mr-3"></i>
                    <div>
                      <div class="text-value-sm text-primary"><%=performanceb%></div>
                      <div class="text-muted text-uppercase font-weight-bold small">Appraisal Feedback</div>
                    </div>
                  </div>
                  <div class="card-footer px-3 py-2">
                    <a class="btn-block text-muted d-flex justify-content-between align-items-center" href="/Module/Employee/Performance/SecondRevewAppraisalObjectivesForm.aspx">
                      <span class="small font-weight-bold">View More</span>
                      <i class="fa fa-angle-right"></i>
                    </a>
                  </div>
                </div>
              </div>
               
              
              <div class="col-6 col-lg-3">
                <div class="card">
                  <div class="card-body p-3 d-flex align-items-center">
                    <i class="fa fa-cogs bg-primary p-3 font-2xl mr-3"></i>
                    <div>
                      <div class="text-value-sm text-primary"><%=devplan %></div>
                      <div class="text-muted text-uppercase font-weight-bold small">Development Plan</div>
                    </div>
                  </div>
                  <div class="card-footer px-3 py-2">
                    <a class="btn-block text-muted d-flex justify-content-between align-items-center" href="/Module/Employee/Performance/CoacheeDevelopmentPlan.aspx">
                      <span class="small font-weight-bold">View More</span>
                      <i class="fa fa-angle-right"></i>
                    </a>
                  </div>
                </div>
              </div>
              
              <div class="col-6 col-lg-3">
                <div class="card">
                  <div class="card-body p-3 d-flex align-items-center">
                    <i class="fa fa-cogs bg-primary p-3 font-2xl mr-3"></i>
                    <div>
                      <div class="text-value-sm text-primary"><%=training%></div>
                      <div class="text-muted text-uppercase font-weight-bold small">Training</div>
                    </div>
                  </div>
                  <div class="card-footer px-3 py-2">
                    <a class="btn-block text-muted d-flex justify-content-between align-items-center" href="/Module/Employee/TrainingPortal/ApprovalTrainings.aspx">
                      <span class="small font-weight-bold">View More</span>
                      <i class="fa fa-angle-right"></i>
                    </a>
                  </div>
                </div>
              </div>
              
              <div class="col-6 col-lg-3">
                <div class="card">
                  <div class="card-body p-3 d-flex align-items-center">
                    <i class="fa fa-cogs bg-primary p-3 font-2xl mr-3"></i>
                    <div>
                      <div class="text-value-sm text-primary"><%=jobexit%></div>
                      <div class="text-muted text-uppercase font-weight-bold small">Job Exits</div>
                    </div>
                  </div>
                  <div class="card-footer px-3 py-2">
                    <a class="btn-block text-muted d-flex justify-content-between align-items-center" href="/Module/Employee/Recruitment/ExitApprovals.aspx">
                      <span class="small font-weight-bold">View More</span>
                      <i class="fa fa-angle-right"></i>
                    </a>
                  </div>
                </div>
              </div>
              
              <div class="col-6 col-lg-3">
                <div class="card">
                  <div class="card-body p-3 d-flex align-items-center">
                    <i class="fa fa-cogs bg-primary p-3 font-2xl mr-3"></i>
                    <div>
                      <div class="text-value-sm text-primary"><%=promotion%></div>
                      <div class="text-muted text-uppercase font-weight-bold small">Promotion</div>
                    </div>
                  </div>
                  <div class="card-footer px-3 py-2">
                    <a class="btn-block text-muted d-flex justify-content-between align-items-center" href="/Module/Employee/Recruitment/PromotionsApproval.aspx">
                      <span class="small font-weight-bold">View More</span>
                      <i class="fa fa-angle-right"></i>
                    </a>
                  </div>
                </div>
              </div>
              
              <div class="col-6 col-lg-3">
                <div class="card">
                  <div class="card-body p-3 d-flex align-items-center">
                    <i class="fa fa-cogs bg-primary p-3 font-2xl mr-3"></i>
                    <div>
                      <div class="text-value-sm text-primary"><%=loan%></div>
                      <div class="text-muted text-uppercase font-weight-bold small">Loans</div>
                    </div>
                  </div>
                  <div class="card-footer px-3 py-2">
                    <a class="btn-block text-muted d-flex justify-content-between align-items-center" href="/Module/Finance/Loans/LoansApproval.aspx">
                      <span class="small font-weight-bold">View More</span>
                      <i class="fa fa-angle-right"></i>
                    </a>
                  </div>
                </div>
              </div>





                 <div class="col-6 col-lg-3">
                <div class="card">
                  <div class="card-body p-3 d-flex align-items-center">
                    <i class="fa fa-cogs bg-primary p-3 font-2xl mr-3"></i>
                    <div>
                      <div class="text-value-sm text-primary"><%=leave%></div>
                      <div class="text-muted text-uppercase font-weight-bold small">Leave</div>
                    </div>
                  </div>
                  <div class="card-footer px-3 py-2">
                    <a class="btn-block text-muted d-flex justify-content-between align-items-center" href="/Module/Employee/LeaveManagement/LeaveRoster.aspx">
                      <span class="small font-weight-bold">View More</span>
                      <i class="fa fa-angle-right"></i>
                    </a>
                  </div>
                </div>
              </div>





	
	</div>
	
	</div>
	
	</div>

	</div>
</div>
	
	<!-- end approvals -->







<!-- planning and requests widgets-->
<div class="row">

<div class="col-sm-12">
	<div class="card">
                  <div class="card-header">
                Planning and requests
                  </div>
                  
                  <div class="card-body">
                      <div class="row">    
          
	  <div class="col-6 col-lg-3">
       <a href="/Module/Employee/Recruitment/WorkForce.aspx">
                <div class="card border-warning">
                  <div class="card-header" align="center">
                   Workforce Plan 
                  </div>
                  <div class="card-body" align="center">
                   <b>(<%=wfplanning2%>)</b>
                  </div>
                </div></a>
              </div>
                
                
                
          <div class="col-6 col-lg-3">
          <a href="/Module/Employee/Recruitment/StaffRequisition.aspx">
                <div class="card border-warning">
                  <div class="card-header" align="center">
                   Staff Requisition
                  </div>
                  <div class="card-body" align="center">
                    <b>(<%=requisition2%>)</b>
                  </div>
                </div></a>
              </div>
                
              <div class="col-6 col-lg-3">
              <a href="/Module/Employee/Recruitment/SuccessionPlans.aspx">
                <div class="card border-warning">
                  <div class="card-header" align="center">
              Succession Planning
                  </div>
                  <div class="card-body" align="center">
                    <b>(<%=successplan%>)</b>
                  </div>
                </div></a>
              </div>
                
               <div class="col-6 col-lg-3">
              <a href="/Module/Employee/Recruitment/PromotionsInitiated.aspx">
                <div class="card border-warning">
                  <div class="card-header" align="center">
              Promotion
                  </div>
                  <div class="card-body" align="center">
                    <b>(<%=promotion2%>)</b>
                  </div>
                </div></a>
              </div>
              
              
                            <div class="col-6 col-lg-3">
              <a href="/Module/Employee/Performance/MgrQueries.aspx">
                <div class="card border-warning">
                  <div class="card-header" align="center">
              Queries
                  </div>
                  <div class="card-body" align="center">
                    <b>(<%=queries%>)</b>
                  </div>
                </div></a>
              </div>
                
                
                     <div class="col-6 col-lg-3">
              <a href="/Module/Employee/Recruitment/Interviews.aspx">
                <div class="card border-warning">
                  <div class="card-header" align="center">
             Interviews
                  </div>
                  <div class="card-body" align="center">
                    <b>(<%=interviews%>)</b>
                  </div>
                </div></a>
              </div>
                
                
                
                
                
                
	
	</div>
	
	</div>
	
	</div>

	</div>
</div>
	
	<!-- end planning and requests -->



    <!-- begin graphs -->


    
<div class="card-columns cols-2">

<div class="row">
              <div class="card">
                <div class="card-header">
                 Performance Analytics
                
                </div>
                <div class="card-body">
                <canvas id="scatterChart" ></canvas>
                
               
                </div>
                
                </div>
                
                
               
                  <div class="card">
                <div class="card-header">
               Head Count
                
                </div>
                <div class="card-body">
     <div class="card">
                  <div class="card-body p-0 d-flex align-items-center">
                    <i class="fa fa-male bg-warning p-4 font-2xl mr-3"></i>
                    <div>
                      <div class="text-value-sm text-warning" id='gmale'>9995</div>
                      <div class="text-muted text-uppercase font-weight-bold small">Male</div>
                    </div>
                  </div>
                </div>
              
       
                <div class="card">
                  <div class="card-body p-0 d-flex align-items-center">
                    <i class="fa fa-female bg-warning p-4 font-2xl mr-3"></i>
                    <div>
                      <div class="text-value-sm text-warning"  id='gfemale'>9500</div>
                      <div class="text-muted text-uppercase font-weight-bold small">Female</div>
                    </div>
                  </div>
                </div>
                <br/> <br/> <br/> <br/> 
       
                </div>
                
                </div></div>


                
                  <div class="card">
                <div class="card-header">
               Training Cost
                
                </div>
                <div class="card-body">
                <canvas id="myBarChart" ></canvas>
                
               
                </div>
                
                </div>
                
                
                
                  <div class="card">
                <div class="card-header">
               Training Hours
                
                </div>
                <div class="card-body">
                <canvas id="myBarChart2" ></canvas>
                
               
                </div>
                
                </div>
                
                 
                
                
                
                
                
                
                
                
                
                
                
                
              
                
                </div>
                
               
 


<!-- end graphs -->





            <!--/.row-->
          </div>

        </div>
      </main>
  
    </div>


    <!-- Button trigger modal -->


<!-- Modal -->
<div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="exampleModalLabel">Employee Details</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
        ...
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
       
      </div>
    </div>
  </div>
</div>


 
    <!-- Bootstrap and necessary plugins-->
    <script src="res_new/vendors/jquery/js/jquery.min.js"></script>
    <script src="res_new/vendors/popper.js/js/popper.min.js"></script>
    <script src="res_new/vendors/bootstrap/js/bootstrap.min.js"></script>
    <script src="res_new/vendors/pace-progress/js/pace.min.js"></script>
    <script src="res_new/vendors/perfect-scrollbar/js/perfect-scrollbar.min.js"></script>
    <script src="res_new/vendors/@coreui/coreui/js/coreui.min.js"></script>
    <!-- Plugins and scripts required by this view-->
    <script src="res_new/vendors/chart.js/js/Chart.min.js"></script>
    <script src="res_new/vendors/@coreui/coreui-plugin-chartjs-custom-tooltips/js/custom-tooltips.min.js"></script>
    <script src="res_new/js/main.js"></script>
 

 //gendercount


 <script type="text/javascript">
 
  (function () {


      if (window.XMLHttpRequest) {// code for IE7+, Firefox, Chrome, Opera, Safari
          xmlhttp = new XMLHttpRequest();
      }
      else {// code for IE6, IE5
          xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
      }
      xmlhttp.onreadystatechange = function () {
          if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {

              var everyg = xmlhttp.responseText
              var gcount = everyg.split(",");
              document.getElementById("gmale").innerHTML = gcount[0];
              document.getElementById("gfemale").innerHTML = gcount[1]
          }

          else {

          }
      }
      xmlhttp.open("GET", 'getempdetails.aspx?getmngrpfmnc=gender', false);
      xmlhttp.send();

  })();
 
 
 
 </script>


 //build the analytics array1 

 

 
    
    
     <script type="text/javascript">  
     (function () {

var pdat = [];
         if (window.XMLHttpRequest) {// code for IE7+, Firefox, Chrome, Opera, Safari
             xmlhttp = new XMLHttpRequest();
         }
         else {// code for IE6, IE5
             xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
         }
         xmlhttp.onreadystatechange = function () {
             if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
                 document.getElementById('details').innerHTML = ''
                 var every0 = xmlhttp.responseText
                 var every00 = every0.replace(/<(?:.|\n)*?>/gm, '')
                 var every1 = every00.split(",")
                 //var every2 = every00.split(",")
 
                 for (var i = 0; i < every1.length;  i++ ) {

                     pp = every1[i]

                     var pd = pp.split("*");


                     //pdat.push("{x:" + pd[0] + ",y:" + pd[1] + "}");
                     pdat.push({x: pd[0] ,y : pd[1] });
                   
                 }
                 
             }

             else {

             }
         }



         xmlhttp.open("GET", 'getempdetails.aspx?getmngrpfmnc=yes', false);
         xmlhttp.send();


         console.log(pdat)



         var ctx = document.getElementById("scatterChart");
         var scatterChart = new Chart(ctx, {
             type: 'scatter',
             data: {
                 datasets: [{
                     label: 'Scatter Dataset',
                     data: pdat
                 }]
             },
             options: {
                 scales: {
                     xAxes: [{
                         type: 'linear',
                         position: 'bottom'
                     }]
                 }
             }
         });




     })();	
                </script>
    
    
    
    
    
    
    
    <script>


        var ctx2 = document.getElementById("myBarChart");
        var myChart = new Chart(ctx2, {
            type: 'bar',
            data: {
                labels: ["Year 1", "Year 2", "Year 3", "Year 4", "Year 5"],
                datasets: [{
                    label: '# of Years',
                    data: [12, 19, 3, 5, 2],
                    backgroundColor: [
                'rgba(255, 99, 132, 0.2)',
                'rgba(54, 162, 235, 0.2)',
                'rgba(255, 206, 86, 0.2)',
                'rgba(75, 192, 192, 0.2)',
                'rgba(153, 102, 255, 0.2)'
            ],
                    borderColor: [
                'rgba(255,99,132,1)',
                'rgba(54, 162, 235, 1)',
                'rgba(255, 206, 86, 1)',
                'rgba(75, 192, 192, 1)',
                'rgba(153, 102, 255, 1)',

            ],
                    borderWidth: 1
                }]
            },
            options: {
                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero: true
                        }
                    }]
                }
            }
        });
    	
    </script>
    
    
    
    
    
    
    
    <script>


        var ctx3 = document.getElementById("myBarChart2");
        var myChart = new Chart(ctx3, {
            type: 'bar',
            data: {
                labels: ["Year 1", "Year 2", "Year 3", "Year 4", "Year 5"],
                datasets: [{
                    label: '# of Years',
                    data: [12, 19, 3, 5, 2],
                    backgroundColor: [
                'rgba(255, 99, 132, 0.2)',
                'rgba(54, 162, 235, 0.2)',
                'rgba(255, 206, 86, 0.2)',
                'rgba(75, 192, 192, 0.2)',
                'rgba(153, 102, 255, 0.2)'
            ],
                    borderColor: [
                'rgba(255,99,132,1)',
                'rgba(54, 162, 235, 1)',
                'rgba(255, 206, 86, 1)',
                'rgba(75, 192, 192, 1)',
                'rgba(153, 102, 255, 1)',

            ],
                    borderWidth: 1
                }]
            },
            options: {
                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero: true
                        }
                    }]
                }
            }
        });
    	
    </script>
    
    
       <script>



           var makel = document.querySelectorAll('a');
           var i;

           for (i = 0; i <= makel.length; i++) {
               makel[i].target = "_top";
               var att = makel[i].getAttribute('href')


               makel[i].setAttribute('href', "/goshrm/" + att);
           
               // alert(makel[i])

           }



    
    </script> 

--%>



  </body>
</html>

