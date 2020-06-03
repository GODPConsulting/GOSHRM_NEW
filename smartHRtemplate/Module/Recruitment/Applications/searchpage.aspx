<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="searchpage.aspx.vb" Inherits="GOSHRM.searchpage" %>

<!DOCTYPE html>
<html lang="en">
  <head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">    
    <meta http-equiv="content-type" content="text/html; charset=utf-8">
    <meta name="author" content="Jobboard">
    
    <title>GOSHRM Recruitment Portal</title>    

    <!-- Favicon -->
    <link rel="shortcut icon" href="assets/img/favicon.png">
    <!-- Bootstrap CSS -->
    <link rel="stylesheet" href="assets/css/bootstrap.min.css" type="text/css">    
    <link rel="stylesheet" href="assets/css/jasny-bootstrap.min.css" type="text/css">  
    <link rel="stylesheet" href="assets/css/bootstrap-select.min.css" type="text/css">  
    <!-- Material CSS -->
    <link rel="stylesheet" href="assets/css/material-kit.css" type="text/css">
    <!-- Font Awesome CSS -->
    <link rel="stylesheet" href="assets/fonts/font-awesome.min.css" type="text/css"> 
    <link rel="stylesheet" href="assets/fonts/themify-icons.css"> 

    <!-- Animate CSS -->
    <link rel="stylesheet" href="assets/extras/animate.css" type="text/css">
    <!-- Owl Carousel -->
    <link rel="stylesheet" href="assets/extras/owl.carousel.css" type="text/css">
    <link rel="stylesheet" href="assets/extras/owl.theme.css" type="text/css">
    <!-- Rev Slider CSS -->
    <link rel="stylesheet" href="assets/extras/settings.css" type="text/css"> 
    <!-- Slicknav js -->
    <link rel="stylesheet" href="assets/css/slicknav.css" type="text/css">
    <!-- Main Styles -->
    <link rel="stylesheet" href="assets/css/main.css" type="text/css">
    <!-- Responsive CSS Styles -->
    <link rel="stylesheet" href="assets/css/responsive.css" type="text/css">

    <!-- Color CSS Styles  -->
    <link rel="stylesheet" type="text/css" href="assets/css/colors/red.css" media="screen" />
    
  </head>

  <body>  
  <form runat="server">
      <!-- Header Section Start -->
      <div class="header">    
        <!-- Start intro section -->
        <section id="intro" class="section-intro">
          <div class="logo-menu">
            <nav class="navbar navbar-default" role="navigation" data-spy="affix" data-offset-top="50">
              <div class="container">
                <!-- Brand and toggle get grouped for better mobile display -->
                <div class="navbar-header">
                  <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#navbar">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                  </button>
                  <%-- <a class="navbar-brand logo" href="index.html"><img src="assets/img/logo.png" alt=""></a>--%>
                 <asp:Image ID="imgProfile" runat="server" CssClass=" img-rounded" ImageUrl="~/img/logo.png" Height="80px" Width="120px" />

                 <asp:Image ID="imgClear" runat="server" ImageUrl="~/images/logo.png" Height="150px" Width="150px" Visible="False" />
                </div>

                <div class="collapse navbar-collapse" id="navbar">              
                <!-- Start Navigation List -->
                <ul class="nav navbar-nav navbar-right float-right">
                  <%--<li class="left"><a href="post-job.html"><i class="ti-pencil-alt"></i> Post A Job</a></li>--%>
                  <li class="right"><a href="applicantlogin"><i class="ti-lock"></i>  Log In</a></li>
                </ul>
              </div>                           
            </div>
            <!-- Mobile Menu Start -->
            <ul class="wpb-mobile-menu">
              <li class="btn-m"><a href="applicantlogin"><i class="ti-lock"></i>  Log In</a></li>          
            </ul>
            <!-- Mobile Menu End --> 
          </nav>
   

            
    </section>
    <!-- end intro section -->
    </div>
    
    <!-- Find Job Section Start -->
    <section class="find-job section">
    <div class="search-containers">
        <div class="container">
          <div class="row">
            <div class="col-md-12">
              <h1>Find the job that fits your life</h1><br><br /><%--<h2>More than <strong>12,000</strong> jobs are waiting to Kickstart your career!</h2>--%>
              <div class="content">
                <form method="" action="">
                  <div class="row">
                    <div class="col-md-4 col-sm-6">
                      <div class="form-group">
                        <input class="form-control" id="search" runat="server" type="text" placeholder="job title / keywords / company name">
                        <i class="ti-time"></i>
                      </div>
                    </div>
                    <div class="col-md-4 col-sm-6">
                      <div class="form-group">
                      <%--<i class="ti-location-pin"></i>--%>
                        <%--<input class="form-control" type="email" placeholder="city / province / zip code">--%>
                        <select id="locations" runat="server" class="dropdown-product selectpicker">
                            <option value= "All">All Locations</option>                            
                          </select>
                        
                      </div>
                    </div>
                    <div class="col-md-3 col-sm-6">
                      <div class="search-category-container">
                        <label class="styled-select">
                          <select runat="server" id="Specializations" class="dropdown-product selectpicker">
                            <option>All Specializations</option>
                            <option>Administration  &amp;  Office Support</option>
                            <option>Agriculture/Farming</option>
                            <option>Banking / Finance / Insurance</option>
                            <option>Building Design/Architecture</option>
                            <option>Construction</option>
                            <option>Consulting/Business Strategy  &amp;  Planning</option>
                            <option>Creatives(Arts, Design, Fashion)</option>
                            <option>Customer Service</option>
                            <option>Education/Teaching/Training</option>
                            <option>Engineering</option>
                            <option>Executive / Top Management</option>
                            <option>Healthcare / Pharmaceutical</option>
                            <option>Hospitality / Leisure / Travels</option>
                            <option>Human Resources</option>
                            <option>Information Technology</option>
                            <option>Legal</option>
                            <option>Logistics / Transportation</option>
                            <option>Manufacturing / Production</option>
                            <option>Marketing / Advertising / Communications</option>
                            <option>NGO/Community Services &amp; Dev</option>
                            <option>Oil &amp; Gas / Mining / Energy</option>
                            <option>Project / Programme Management</option>
                            <option>QA &amp; QC / HSE</option>
                            <option>Real Estate / Property</option>
                            <option>Research</option>
                            <option>Sales/Business Development</option>
                            <option>Supply Chain / Procurement</option>
                            <option>Telecommunications</option>
                            <option>Vocational Trade and Services</option>
                          </select>
                        </label>
                      </div>
                    </div>
                    <div class="col-md-1 col-sm-6">
                      <button type="submit" onserverclick="btn_search_Click" runat="server" class="btn btn-search-icon"><i class="ti-search"></i></button>
                      <%--<asp:Button ID="btn_search" name="search"  CssClass="btn btn-search-icon ti-search"  runat="server" Text=""></asp:Button>--%>
                    </div>
                  </div>
                </form>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div style="margin-bottom:85px" class="container">
       <%-- <h2 class="section-title">Hot Jobs</h2>--%>
       <div id="divalert" runat="server" visible="false" class="alert alert-info">
                <strong id="msgalert" runat="server">Danger!</strong>
            </div>
        <div class="row">
          <div id="job_list" runat="server" class="col-md-12">
            <div class="job-list">
              <div class="job-list-content">
                <h4>No Jobs Available !!!</h4>
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>
    <!-- Find Job Section End -->

    	<div id="copyright">
    		<div class="container">
    			<div class="row">
    				<div class="col-md-12">
              <div class="site-info text-center">
                <p>All Rights reserved &copy; <%=yaer%> - Designed & Developed by <a rel="nofollow" href="http://godp.co.uk">GODP Consulting</a></p>
              </div>   
    				</div>
    			</div>
    		</div>
    	</div>
    	<!-- Copyright End -->

    </footer>
    <!-- Footer Section End -->  
    
    <!-- Go To Top Link -->
    <a href="#" class="back-to-top">
      <i class="ti-arrow-up"></i>
    </a>

    <div id="loading">
      <div id="loading-center">
        <div id="loading-center-absolute">
          <div class="object" id="object_one"></div>
          <div class="object" id="object_two"></div>
          <div class="object" id="object_three"></div>
          <div class="object" id="object_four"></div>
          <div class="object" id="object_five"></div>
          <div class="object" id="object_six"></div>
          <div class="object" id="object_seven"></div>
          <div class="object" id="object_eight"></div>
        </div>
      </div>
    </div>
        
    <!-- Main JS  -->
    <script type="text/javascript" src="assets/js/jquery-min.js"></script>      
    <script type="text/javascript" src="assets/js/bootstrap.min.js"></script>    
    <script type="text/javascript" src="assets/js/material.min.js"></script>
    <script type="text/javascript" src="assets/js/material-kit.js"></script>
    <script type="text/javascript" src="assets/js/jquery.parallax.js"></script>
    <script type="text/javascript" src="assets/js/owl.carousel.min.js"></script>
    <script type="text/javascript" src="assets/js/jquery.slicknav.js"></script>
    <script type="text/javascript" src="assets/js/main.js"></script>
    <script type="text/javascript" src="assets/js/jquery.counterup.min.js"></script>
    <script type="text/javascript" src="assets/js/waypoints.min.js"></script>
    <script type="text/javascript" src="assets/js/jasny-bootstrap.min.js"></script>
    <script type="text/javascript" src="assets/js/bootstrap-select.min.js"></script>
    <script type="text/javascript" src="assets/js/form-validator.min.js"></script>
    <script type="text/javascript" src="assets/js/contact-form-script.js"></script>    
    <script type="text/javascript" src="assets/js/jquery.themepunch.revolution.min.js"></script>
    <script type="text/javascript" src="assets/js/jquery.themepunch.tools.min.js"></script>
    
    </form>
  </body>
</html>