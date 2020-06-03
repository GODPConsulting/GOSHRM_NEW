<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="vacancyview.aspx.vb" Inherits="GOSHRM.vacancyviews" %>

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
    <!-- Main Styles -->
    <link rel="stylesheet" href="assets/css/main.css" type="text/css">
    <!-- Slicknav js -->
    <link rel="stylesheet" href="assets/css/slicknav.css" type="text/css">
    <!-- Responsive CSS Styles -->
    <link rel="stylesheet" href="assets/css/responsive.css" type="text/css">

    <!-- Color CSS Styles  -->
    <link rel="stylesheet" type="text/css" href="assets/css/colors/red.css" media="screen" />
    
  </head>

  <body>
   <form id="Form1" runat="server">  
      <!-- Header Section Start -->
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
                <ul class="nav navbar-nav">

                </ul>
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

          <!-- Off Canvas Navigation -->
          <div class="navmenu navmenu-default navmenu-fixed-left offcanvas"> 
          <!--- Off Canvas Side Menu -->
            <div class="close" data-toggle="offcanvas" data-target=".navmenu">
                <i class="ti-close"></i>
            </div>
              <h3 class="title-menu">All Pages</h3>
              <ul class="nav navmenu-nav">
                <li><a href="index.html">Home</a></li>
                <li><a href="index-02.html">Home Page 2</a></li>
                <li><a href="index-03.html">Home Page 3</a></li>
                <li><a href="index-04.html">Home Page 4</a></li>
                <li><a href="about.html">About us</a></li>            
                <li><a href="job-page.html">Job Page</a></li>             
                <li><a href="job-details.html">Job Details</a></li>    
                <li><a href="resume.html">Resume Page</a></li> 
                <li><a href="privacy-policy.html">Privacy Policy</a></li>
                <li><a href="pricing.html">Pricing Tables</a></li>
                <li><a href="browse-jobs.html">Browse Jobs</a></li>
                <li><a href="browse-categories.html">Browse Categories</a></li>
                <li><a href="add-resume.html">Add Resume</a></li>
                <li><a href="manage-resumes.html">Manage Resumes</a></li> 
                <li><a href="job-alerts.html">Job Alerts</a></li> 
                <li><a href="post-job.html">Add Job</a></li>  
                <li><a href="manage-jobs.html">Manage Jobs</a></li>
                <li><a href="manage-applications.html">Manage Applications</a></li>
                <li><a href="browse-resumes.html">Browse Resumes</a></li>
                <li><a href="contact.html">Contact</a></li>
                <li><a href="faq.html">Faq</a></li>
                <li><a href="my-account.html">Login</a></li>
            </ul><!--- End Menu -->
          </div> <!--- End Off Canvas Side Menu -->
          <%--<div class="tbtn wow pulse" id="menu" data-wow-iteration="infinite" data-wow-duration="500ms" data-toggle="offcanvas" data-target=".navmenu">
            <p><i class="ti-files"></i> All Pages</p>
          </div>--%>
      </div>
      <!-- Header Section End -->  

      <!-- Page Header Start -->
      <div class="page-header" style="background: url(assets/img/banner1.jpg);">
        <div class="container">
          <div class="row">         
            <div class="col-md-12">
              <div class="breadcrumb-wrapper">
                <h2 id="ajobtitle" runat="server" class="product-title">Head of Operations</h2>
                <ol class="breadcrumb">
                  <li><a href="#"><i class="ti-home"></i><span id="acompany" runat="server">GODP CONSULTING</span></a></li>
                  <li id="ajobtype" runat ="server" class="current">Permanent</li>
                </ol>
              </div>
            </div>
          </div>
        </div>
      </div>
      <!-- Page Header End -->        

      <!-- Main container Start -->  
      <div class="about section">
        <div style="min-height:215px;" class="container">
                            <div class="row">
                                <div id="divalert" runat="server" visible="false" class="alert alert-info">
                                    <strong id="msgalert" runat="server">Danger!</strong>
                                    <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                        <span aria-hidden="true">&times;</span></button>
                                </div>
                            </div>
                            <div class="row text-center">
                                    <asp:Button ID="Button1" name="search" Visible ="false" CssClass="btn btn-common" BackColor="#55ce63"  runat="server" Text="OK"></asp:Button>
                                    </div>
          <div id="testmode" runat="server" style="" class="row">          
             
            <asp:Label ID="lblID" runat="server" Visible="False" Font-Size="10px"></asp:Label>
            <asp:TextBox ID="txtHasAptitude" runat="server" BorderColor="#CCCCCC" BorderWidth="1px"
                                    Height="1px" Visible="False"></asp:TextBox>
                                    
            <div class="row text-right">
                    <asp:Button ID="btnAdd" name="search"  CssClass="btn btn-common" BackColor="#55ce63"  runat="server" Text="Apply Now"></asp:Button> 
                    <asp:Button ID="Button2" name="search"  CssClass="btn btn-common"  runat="server" Text="Back"></asp:Button>                   
                    <%--<button type="submit" id="btnAdd" runat="server" class="btn btn-common" style="background-color:#55ce63 !important" onserverclick="btnAdd_Click">Apply Now</button>--%>
                    <%--<a href="<%= Page.ResolveClientUrl("~/Module/Recruitment/Applications/Welcome")%>" runat="server" class="btn btn-common">Back</a>--%>
                  </div>
            <div class="col-md-12 col-sm-12">
                <h3 class="">Job Description</h3>
                <p id="ajobdesc" runat="server">Lorem ipsum dolor sit amet, consectetur adipisicing elit. Magni delectus soluta adipisci beatae ullam quisquam, quia recusandae rem assumenda, praesentium porro sequi eaque doloremque tenetur incidunt officiis explicabo optio perferendis.
                 Lorem ipsum dolor sit amet, consectetur adipisicing elit. Magni delectus soluta adipisci beatae ullam quisquam, quia recusandae rem assumenda, praesentium porro sequi eaque doloremque tenetur incidunt officiis explicabo optio perferendis.
                 Lorem ipsum dolor sit amet, consectetur adipisicing elit. Magni delectus soluta adipisci beatae ullam quisquam, quia recusandae rem assumenda, praesentium porro sequi eaque doloremque tenetur incidunt officiis explicabo optio perferendis.</p>   
            </div>
            <div class="col-md-6 col-sm-12">
                 <h3>Area of Specialization</h3>
                <p id="aspecialisation" runat="server">Permanent </p>
                 
                 <h3>Skills</h3>
                 <ul id="askill" runat="server">
                 <li style="list-style-type:disc;margin-left:30px;">Permanent</li>   
                  <li style="list-style-type:disc;margin-left:30px;">Permanent</li> 
                   <li style="list-style-type:disc;margin-left:30px;">Permanent</li>
                   <li style="list-style-type:disc;margin-left:30px;">Permanent</li>   
                  <li style="list-style-type:disc;margin-left:30px;">Permanent</li> 
                   <li style="list-style-type:disc;margin-left:30px;">Permanent</li>                    
                 </ul>
                
                 <h3>Minimum Educational Level</h3>
                <p id="amineducation" runat="server">Permanent </p>
                 <h3 id="olevelsubjectlabel" runat="server">O'Level Subjects</h3>
                 <div id="olevelsubject" runat="server">
                    <p>No OLevel Subjects </p>         
                 </div>
                   
            </div>
             <div class="col-md-6 col-sm-12">
             <h3>Experience Level</h3>
                <p id="aexplevel" runat="server">Permanent </p>
                 <h3>Years of Experience</h3>
                <p id="aexpyears" runat="server">Permanent </p>
                 <h3>Location</h3>
                <p id="alocation" runat="server">Country </p>
                 <h3>Country</h3>
                <p id="acountry" runat="server">Permanent </p>
                 <h3>Age</h3>
                <p id="aage" runat="server">Permanent </p>
                 <h3>Closing Date</h3>
                <p id="aclosingdate" runat="server">Permanent </p>
             </div>
             </div>        
        <%--  <div class="row text-center">
            <a href="#" class="btn btn-common">Apply Now</a>
          </div>--%>
        </div>
      </div>

        <!-- Copyright Start  -->
        <div id="copyright">
    		<div class="container">
    			<div class="row">
    				<div class="col-md-12">
              <div class="site-info text-center">
                <p>All Rights reserved &copy; 2018 - Designed & Developed by <a rel="nofollow" href="http://godp.co.uk">GODP Consulting</a></p>
              </div>   
    				</div>
    			</div>
    		</div>
    	</div>
        <!-- Copyright End -->

      </footer>
      <!-- Footer Section End -->  
      </form>
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
      
  </body>
</html>