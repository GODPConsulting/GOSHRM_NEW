<%@ Page Language="vb" CodeBehind="employee1.aspx.vb" Inherits="GOSHRM.EmpDash"%>

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
    <title>Employee Dashboard</title>
    <!-- Icons-->
    <link href="res_new/vendors/@coreui/icons/css/coreui-icons.min.css" rel="stylesheet">
    <link href="res_new/vendors/flag-icon-css/css/flag-icon.min.css" rel="stylesheet">
    <link href="res_new/vendors/font-awesome/css/font-awesome.min.css" rel="stylesheet">
    <link href="res_new/vendors/simple-line-icons/css/simple-line-icons.css" rel="stylesheet">
    <!-- Main styles for this application-->
    <link href="res_new/css/style.css" rel="stylesheet">
    <link href="res_new/vendors/pace-progress/css/pace.min.css" rel="stylesheet">
  </head>
  <body class="app header-fixed sidebar-fixed aside-menu-fixed sidebar-lg-show">
  <%="" %>
    <div class="app-bodys">
 
      <main class="mains">
        <!-- Breadcrumb-->
            <div class="row">
        <div class="col-xs-8 col-md-12">
            <h3 id="pagetitle" runat="server" class="page-title" style="color: #1BA691">
                My Dashboard</h3>
        </div>
    </div>
        <div class="container-fluid">
          <div class="animated fadeIn">
            
      
            <div class="row">
              <div class="col-md-12">
                <div class="card">
                  <div class="card-header">
                   My Career
                  </div>
                  <div class="card-body">
                   
                    <!--/.row-->
                    <br/>
                    <table class="table table-responsive-sm table-hover table-outline mb-0">
                      <thead class="thead-light">
                          <tr>
                       
                          <th>Activities for Next Level Performance</th>
                          <th>Skills for Current Level Performance</th>
                          <th>Training</th>
                          <th>Performance</th>
                          <th>Job History</th>
                        </tr>
                      </thead>
                      <tbody>
                        <tr>
                           
                          <td>
                          <%=anlp %>
                          </td>


                              <td>
                           <% For Each skls In sclp%>
                               
                                    
                             <%=skls%> <br /> 
                               
                           <%   Next  %>
                          </td>
                        

                              <td>
                           

                              <% For Each trn In trainall%>
                               
                         <%
                           
                           
                           
                                                                                                    Dim trainrpl = trn.Replace("-Not-Attended", " <i class='fa fa-minus-square fa-lg mt-4' style='color:yellow; font-size:17px'></i>")
                                                                                                    
                                                                                                    trainrpl = trainrpl.Replace("-Attended", " <i class='fa fa-check-square fa-lg mt-4' style='color:green; font-size:17px'></i>")

                             Response.Write("<a href='/Module/Employee/TrainingPortal/Trainings.aspx'>" & trainrpl & "</a>")
                                                                                                    
                                                                                                    %>    <br />
                               
                           <%   Next  %>
                          </td>


                              <td>


                      <%
    
    
                                        If perf.Count = 0 Then
                                            Response.Write("")
                             
                                        Else
                                            Response.Write(perf(0))
                             
                             
                             
                                        End If
                             
                         
                         
                                    %>
                                    <br />
                                    <%
                            
                            
                                        If perf.Count = 0 Then
                                            Response.Write("")
                             
                                        Else
                                            If perf.Count > 1 Then
                                                Response.Write(perf(1))
                                            End If
                                        End If
                            
                                %>
                            
                          </td>


                              <td>
                        <div style='border-left: 3px solid green;height: auto; margin-right:5px;'> 
                          - <%=jhis(0)%> <br />
                         - <%=jhis(1)%> <br />
                          - <%=jhis(2)%></b><br />
                          - <%=jhis(3)%> <%=jhis(4)%>   - <%=jhis(5)%>  
                        
                        </div>

                          </td>
                           
                        </tr>
                    
                      </tbody>
                    </table>
                  </div>
                </div>
              </div>
              <!--/.col-->
            </div>












<!-- planning and requests widgets-->
<div class="row">

<div class="col-sm-12">
	<div class="card">
                  <div class="card-header">
             My Requests
                  </div>
                  
                  <div class="card-body">
                      <div class="row">    
          
 <div class="col-6 col-lg-3">
               <a href='/Module/Employee/LeaveManagement/LeaveRoster.aspx'> <div class="card border-warning">
                  <div class="card-header" align="center">
                Leave
                  </div>
                  <div class="card-body" align="center">
                  <%=lv %>
                  </div>
                </div></a>
              </div>
                
                
                
          <div class="col-6 col-lg-3">
               <a href='/Module/Finance/Loans/LoansAndAdvances.aspx'> <div class="card border-warning">
                  <div class="card-header" align="center">
                Loan
                  </div>
                  <div class="card-body" align="center">
                  <%=ln %>
                  </div>
                </div></a>
              </div>
                
           
                    <div class="col-6 col-lg-3">
               <a href='/Module/Employee/Performance/DevelopmentPlans.aspx'> <div class="card border-warning">
                  <div class="card-header" align="center">
         Development Plans
                  </div>
                  <div class="card-body" align="center">
                  <%=dpln %>
                  </div>
                </div></a>
              </div>
                
           
                <div class="col-6 col-lg-3">
               <a href='/Module/Employee/Performance/AppraisalObjectivesForm.aspx'> <div class="card border-warning">
                  <div class="card-header" align="center">
        Appraisal Objective
                  </div>
                  <div class="card-body" align="center">
                  <%=perf2%>
                  </div>
                </div></a>
              </div>
                
                      <div class="col-6 col-lg-3">
               <a href='/Module/Employee/Performance/AppraisalFeedbackList.aspx'> <div class="card border-warning">
                  <div class="card-header" align="center">
        Appraisal Feedback
                  </div>
                  <div class="card-body" align="center">
                  <%=performanceb%>
                  </div>
                </div></a>
              </div>
                
              
                        <div class="col-6 col-lg-3">
               <a href='/Module/Employee/Performance/FeedBack360Request.aspx'> <div class="card border-warning">
                  <div class="card-header" align="center">
        Performance 360
                  </div>
                  <div class="card-body" align="center">
                  <%=pfm360%>
                  </div>
                </div></a>
              </div>
                
                
                
                
                
	
	</div>
	
	</div>
	
	</div>

	</div>
</div>
	
	<!-- end planning and requests -->






















<!-- approvals widgets-->
<div class="row">

<div class="col-sm-12">
	<div class="card">
                  <div class="card-header">
                 Analytics
                  </div>
                  
                  <div class="card-body">
                      <div class="row">    
    <div class="col-6 col-lg-3">
               <a href='/Module/Employee/Performance/AppraisalObjectivesForm.aspx'> <div class="card border-warning">
                  <div class="card-header" align="center">
       Average Performance Score
                  </div>
                  <div class="card-body" align="center">
                  <%=avg2/3%> %
                  </div>
                </div></a>
              </div>


                 <div class="col-6 col-lg-3">
               <a href='/Module/Employee/Performance/AppraisalObjectivesForm.aspx'> <div class="card border-warning">
                  <div class="card-header" align="center">
     Current   Performance Score
                  </div>
                  <div class="card-body" align="center">
                  <%
                      If avgpfm.Count = 0 Then
                          Response.Write("")
                      Else
                          Response.Write(avgpfm(0))
                      End If
                                   
                      
                      
                      
                      
                      %> %
                  </div>
                </div></a>
              </div>



                   <div class="col-6 col-lg-3">
               <a href='/Module/Employee/Performance/AppraisalObjectivesForm.aspx'> <div class="card border-warning">
                  <div class="card-header" align="center">
     Previous Performance Score
                  </div>
                  <div class="card-body" align="center">
                  <%
                      If avgpfm.Count = 0 Then
                          Response.Write("")
                      Else
                          If avgpfm.Count > 1 Then
                              Response.Write(avgpfm(1))
                          End If
                          
                      End If
                              
                      
                      %> %
                  </div>
                </div></a>
              </div>




              
         
              
              
              
            
              
               <div class="col-6 col-lg-3">
               <a href='/Module/Employee/Performance/AppraisalObjectivesForm.aspx'> <div class="card border-warning">
                  <div class="card-header" align="center">
     Previous Performance Score
                  </div>
                  <div class="card-body" align="center">
                  <%=ttime%> Months
                  </div>
                </div></a>
              </div>


	
	</div>
	
	</div>
	
	</div>

	</div>
</div>
	
	<!-- end approvals -->













            <!--/.row-->
          </div>

        </div>
      </main>
    
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



     <script>



         var makel = document.querySelectorAll('a');
         var i;

         for (i = 0; i <= makel.length; i++) {
             makel[i].target = "_top";
             // alert(makel[i])
             var att = makel[i].getAttribute('href')


             makel[i].setAttribute('href', "/goshrm/" + att);
           
         }



    
    </script> 
  </body>
</html>
