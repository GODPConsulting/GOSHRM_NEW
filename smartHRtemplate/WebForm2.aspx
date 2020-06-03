<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="WebForm2.aspx.vb" Inherits="GOSHRM.WebForm2" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title></title>
     <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600,700,300italic,400italic,600italic" />

     <link href="css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="css/fullcalendar.min.css" rel="stylesheet" />
    <link href="css/dataTables.bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="css/select2.min.css" type="text/css" />
    <link rel="stylesheet" href="css/bootstrap-datetimepicker.min.css" type="text/css" />
    <link rel="stylesheet" href="plugins/morris/morris.css" />
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <link href="css/gridview.css" rel="stylesheet" type="text/css" />
    <%--<link href="css/slider-goke.css" rel="stylesheet" type="text/css" />--%>
    <script type="text/javascript" src="https://gitcdn.github.io/bootstrap-toggle/2.2.2/js/bootstrap-toggle.min.js"></script>
    <link type="text/css" rel="stylesheet" href="https://gitcdn.github.io/bootstrap-toggle/2.2.2/css/bootstrap-toggle.min.css" />

    <style type="text/css">
        .toggle.ios, .toggle-on.ios, .toggle-off.ios
        {
            border-radius: 20px;
            width: 110px;
        }
        .toggle.ios .toggle-handle
        {
            border-radius: 20px;
            width: 40px;
        }
    </style>

   <style type="text/css">
/* The Modal (background) */
.modal {
    display: none; /* Hidden by default */
    position: fixed; /* Stay in place */
    z-index: 1; /* Sit on top */
    padding-top: 100px; /* Location of the box */
    left: 0;
    top: 0;
    width: 100%; /* Full width */
    height: 100%; /* Full height */
    overflow: auto; /* Enable scroll if needed */
    /*background-color: rgb(0,0,0); /* Fallback color */
   /* background-color: rgba(0,0,0,0.4); /* Black w/ opacity */
}

/* Modal Content */
.modal-content {
    
    margin: auto;
    padding: 20px;
    border: 1px solid #888;
    width: 80%;
}

/* The Close Button */
.close {
    color: #aaaaaa;
    float: right;
    font-size: 28px;
    font-weight: bold;
}

.close:hover,
.close:focus {
    color: #000;
    text-decoration: none;
    cursor: pointer;
}
</style>
     
     function CmbChange(obj) {
         var cmbValue = document.getElementById("cmbID").value;
         __doPostBack('cmbID', cmbValue);
     }

</head>
<body>
    <form id="form1" runat="server">
        


                    <div class="list-group-item">          <p>Test</p>            <div class="material-switch">
											<input id="holidays_module" type="checkbox" checked="checked" onchange="CmbChange"/>
											<label for="holidays_module" class="label-success"></label>
										</div>          </div>  

    <h2>Toggle Switch</h2>



<label class="switch">
  <input type="checkbox">
  <span class="slider round"></span>
</label>

<label class="switch">
  <input id="stest" runat="server" type="checkbox"/>
  <span class="slider round"></span>
</label>
                               

<div id="piechart">
    <telerik:RadScriptManager ID="RadScriptManager1" Runat="server">
    </telerik:RadScriptManager>
          </div>

<script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>

<script type="text/javascript">
    // Load google charts
    google.charts.load('current', { 'packages': ['corechart'] });
    google.charts.setOnLoadCallback(drawChart);

    // Draw the chart and set the chart values
    function drawChart() {
        var data = google.visualization.arrayToDataTable([
  ['Task', 'Hours per Day'],
  ['Reviewee', 10],
  ['Review 1', 40],
  ['Reviewer 2', 50]
]);

        // Optional; add a title and set the width and height of the chart
        var options = { 'title': 'My Average Day', 'width': 350, 'height': 250 };

        // Display the chart inside the <div> element with id="piechart"
        var chart = new google.visualization.PieChart(document.getElementById('piechart'));
        chart.draw(data, options);
    }
</script>
    
          <telerik:RadTextBox ID="RadTextBox1" Runat="server" Skin="Bootstrap">
          </telerik:RadTextBox>


                  
												<div class="form-group">
													<label class="control-label">Gendar</label>
													<select class="select form-control">
														<option value="">Select Gendar</option>
														<option value="">Male</option>
														<option value="">Female</option>
													</select>
												</div>
											
    
   </form>

</body>

</html>
