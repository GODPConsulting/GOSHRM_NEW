  <%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="AppraisalFeedback.aspx.vb"
   Inherits="GOSHRM.AppraisalFeedback" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <html xmlns="http://www.w3.org/1999/xhtml">
    
<head>
    <title></title>
    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to submit for review?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
    

    <script type="text/javascript" src="<%= Page.ResolveClientUrl("~/js/jquery-3.2.1.min.js") %>"></script>

      <script type="text/javascript">
          function ConfirmDisagree() {
              var confirmplan_value = document.createElement("INPUT");
              confirmplan_value.type = "hidden";
              confirmplan_value.name = "confirmplan_value";
              if (confirm("Do you want to disagree with Appraisal Objective?")) {
                  confirmplan_value.value = "Yes";
              } else {
                  confirmplan_value.value = "No";
              }
              document.forms[0].appendChild(confirmplan_value);
          }
    </script>
    <script>
        function Editcorevalues(lol) {
            var pid = lol;
            this.kpi_id = lol;
            
           // $("#btnsubmit").attr("disabled", false);
          $('#performanceid').val(lol);
          
        }
        function commentsubmit() {
            $("#btnsubmit").attr("disabled", false);
            var checkBox = document.getElementById("self");
            var kpimetric = {};
            kpimetric.empid = "<%=Session("UserEmpID") %>";
            kpimetric.performanceid = $('#performanceid').val();
            kpimetric.kpiid =  <%=lblid.Text %>;
            kpimetric.radEndDate = $('#birthday').val();
            kpimetric.obj = $('#comment').val();
            
            console.log("am here", kpimetric.empid);
              if ((kpimetric.radEndDate == "") || (kpimetric.obj == "") ) {
                  $('#msgbox2').css('display', 'block');
                  $('#msgbox1').css('display', 'none');
              } else {
                  $('#msgbox2').css('display', 'none');
                  console.log("submit", JSON.stringify(kpimetric));
                  $.ajax({
                      url: "<%= Page.ResolveUrl("~/res_new/gos.asmx/addcomment") %>",
                         method: 'post',
                         data: '{emp: ' + JSON.stringify(kpimetric) + '}',
                         contentType: "application/json; charset=utf-8",
                         success: function (data) {
                             $('#msgbox1').css('display', 'block');
                             $('#comment').val("");              
                             $('#radEndDate').val(""); 
                             location.reload()
                         },
                         error: function (err) {
                             //alert(JSON.stringify(err));
                             $(err).each(function (index, prog) {
                                 $('#msgbox2').css('display', 'block');
                                 $("#pmsg").text(prog.responseText);
                             });
                             //$('#pmsg').text("The Programme Name '" + programme.Name + "' already exist");
                         }
                     });
              }
        }
        function pointsubmit() {
            $("#btnsubmit").attr("disabled", false);
            var checkBox = document.getElementById("self");
            var kpimetric = {};
            kpimetric.userid = "<%=Session("UserEmpID") %>";
            kpimetric.empid = "<%=txtEmpID.Text%>";
            kpimetric.rev1id = "<%=lblreviewer.Text%>";
            kpimetric.rev2id = "<%=lblreviewer2.Text%>";
            kpimetric.pid = $('#performanceid1').val();
            kpimetric.kpiobjectives= $('#performanceid2').val();
            kpimetric.jobgrade =  "<%=ajobgrade.Value %>";
            kpimetric.points = $('#Select10 :selected').val();
            var check = $('#performanceid3').val();
            //alert(kpimetric.points)
            console.log("am here", kpimetric.empid);
            if ((check == "Yes") ) {
                $('#msgbox22').css('display', 'block');
                $('#msgbox1').css('display', 'none');
            }

            else {
                $('#msgbox22').css('display', 'none');
                $.ajax({
                    url: "<%= Page.ResolveUrl("~/res_new/gos.asmx/PerformanceSubmit")%>",
                    method: 'post',
                    data: '{Performance: ' + JSON.stringify(kpimetric) + '}',
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {


                        document.getElementById('<%=Button19.ClientID%>').click();
                    },
                    error: function (err) {
                        //alert(JSON.stringify(err));
                        $(err).each(function (index, prog) {
                            $('#msgbox22').css('display', 'block');
                            $("#pmsg1").text(prog.responseText);
                        });
                        //$('#pmsg').text("The Programme Name '" + programme.Name + "' already exist");
                    }
                });
            }
        }
       
        function corevalues(id) {
            
            var pid = id;
            var empid = "<%=txtEmpID.Text%>";
             $.ajax({
                 url: "<%= Page.ResolveClientUrl("~/res_new/gos.asmx/getcomment") %>",
                method: 'post',
                 data: {
                     pid: pid,
                 empid:empid},
                 dataType: 'json',

                success: function (data) {

                    //var selsubmod = $('#tableComment');
                    var selsubmod = document.getElementById("tableses");
                    //selsubmod.append('<thead> <tr>< th > Comment</th ><th>Date</th></tr ></thead><tbody><tr><td>' +  '</td><td>' +  '</td></tr><tbody>');
                  

                    //selsubmod.empty();
                    selsubmod.innerHTML = '';
                    $(data).each(function (index, prog) {
                        console.log(prog)
                        selsubmod.innerHTML += '<tr><td>' + prog.name + '</td><td>' + prog.description.split(' ')[0] + '</td></tr>';
                    });
                },
                error: function (err) {
                    //alert(JSON.stringify(err));
                    $(err).each(function (index, prog) {
                        $('#msgbox2').css('display', 'block');
                        $("#pmsg").text(prog.responseText);
                    });
                }
            });
        }
        function corevalues1(id) {

            var pid = id;
            var empid = "<%=lblreviewer.Text%>";
            $.ajax({
                
                url: "<%= Page.ResolveClientUrl("~/res_new/gos.asmx/getcommentmngr") %>",
                 method: 'post',
                 data: {
                     pid: pid,
                     empid: empid
                 },
                 dataType: 'json',

                 success: function (data) {

                     //var selsubmod = $('#tableComment');
                     var selsubmod = document.getElementById("tableses1");
                     //selsubmod.append('<thead> <tr>< th > Comment</th ><th>Date</th></tr ></thead><tbody><tr><td>' +  '</td><td>' +  '</td></tr><tbody>');
                     selsubmod.innerHTML = ''

                     //selsubmod.empty();

                     $(data).each(function (index, prog) {
                         console.log(prog)
                         selsubmod.innerHTML += '<tr><td>' + prog.name + '</td><td>' + prog.description.split(' ')[0] + '</td></tr>';
                     });
                 },
                 error: function (err) {
                     //alert(JSON.stringify(err));
                     $(err).each(function (index, prog) {
                         $('#msgbox2').css('display', 'block');
                         $("#pmsg").text(prog.responseText);
                     });
                 }
             });
        }
        function corevalues2(id) {

            var pid = id;
            var empid = "<%=lblreviewer2.Text%>";
            $.ajax({
                url: "<%= Page.ResolveClientUrl("~/res_new/gos.asmx/getcommensupervisor") %>",
                 method: 'post',
                 data: {
                     pid: pid,
                     empid: empid
                 },
                 dataType: 'json',

                 success: function (data) {

                     //var selsubmod = $('#tableComment');
                     var selsubmod = document.getElementById("tableses2");
                     //selsubmod.append('<thead> <tr>< th > Comment</th ><th>Date</th></tr ></thead><tbody><tr><td>' +  '</td><td>' +  '</td></tr><tbody>');


                     //selsubmod.empty();
                     selsubmod.innerHTML = ''
                     $(data).each(function (index, prog) {
                         console.log(prog)
                         selsubmod.innerHTML += '<tr><td>' + prog.name + '</td><td>' + prog.description.split(' ')[0] + '</td></tr>';
                     });
                 },
                 error: function (err) {
                     //alert(JSON.stringify(err));
                     $(err).each(function (index, prog) {
                         $('#msgbox2').css('display', 'block');
                         $("#pmsg").text(prog.responseText);
                     });
                 }
             });
        }
        function corevalues3() {

            var pid = <%=lblid.Text%>;
            var empid = "<%=txtEmpID.Text%>";
            var empid1 = "<%=lblreviewer.Text%>";
            var empid2 = "<%=lblreviewer2.Text%>";
           
           
                 $.ajax({
                     url: "<%= Page.ResolveClientUrl("~/res_new/gos.asmx/getAllComments") %>",
                method: 'post',
                data: {
                    pid: pid,
                    empid: empid,
                    empid1: empid1,
                    empid2: empid2
                },
                dataType: 'json',

                     success: function (data) {
                         var b6 = document.getElementById("B6")
                         
                         b6.innerHTML = '';
                         b6.innerHTML = 'Review Comments'

                    //var selsubmod = $('#tableComment');
                    var selsubmod = document.getElementById("tableses3");
                    //selsubmod.append('<thead> <tr>< th > Comment</th ><th>Date</th></tr ></thead><tbody><tr><td>' +  '</td><td>' +  '</td></tr><tbody>');


                    //selsubmod.empty();
                         selsubmod.innerHTML = '';
                    $(data).each(function (index, prog) {
                        const tableList = () => {
                            const types = prog.type.split(';');
                            let string = '';
                            types.forEach(type => {
                                string =string + type + '<br /><br/>';
                               
                            });
                            console.log(string)
                            return string
                        }
                       console.log(tableList())
                       
                        selsubmod.innerHTML += '<tr><td>' + prog.name + '</td><td>' + prog.description+'</td><td>' + tableList() + '</td></tr>';
                    });
                },
                error: function (err) {
                    //alert(JSON.stringify(err));
                    $(err).each(function (index, prog) {
                        $('#msgbox2').css('display', 'block');
                        $("#pmsg").text(prog.responseText);
                    });
                }
            });
        }
        <%--function corevalues4() {

            var pid = <%=lblid.Text%>;
            var empid = "<%=lblreviewer.Text%>";
            var pid = <%=lblid.Text%>;
            var empid = "<%=lblreviewer2.Text%>";
            var b6 = document.getElementById("B6")
            b6.innerHTML = '';
            var selsubmod = document.getElementById("tableses3");
            //selsubmod.append('<thead> <tr>< th > Comment</th ><th>Date</th></tr ></thead><tbody><tr><td>' +  '</td><td>' +  '</td></tr><tbody>');


            //selsubmod.empty();
            selsubmod.innerHTML = '';
            $.ajax({
                url: "<%= Page.ResolveClientUrl("~/res_new/gos.asmx/getAllComments") %>",
                     method: 'post',
                     data: {
                         pid: pid,
                         empid: empid
                     },
                     dataType: 'json',

                success: function (data) {
                    var b6 = document.getElementById("B6")
                    b6.innerHTML = ''
                    b6.innerHTML = 'All Reviewer I Comments'

                         //var selsubmod = $('#tableComment');
                         var selsubmod = document.getElementById("tableses3");
                         //selsubmod.append('<thead> <tr>< th > Comment</th ><th>Date</th></tr ></thead><tbody><tr><td>' +  '</td><td>' +  '</td></tr><tbody>');


                         //selsubmod.empty();
                    selsubmod.innerHTML = '';
                         $(data).each(function (index, prog) {
                             const tableList = () => {
                                 const types = prog.type.split(';');
                                 let string = '';
                                 types.forEach(type => {
                                     string = string + type + '<br /><br/>';

                                 });
                                 console.log(string)
                                 return string
                             }
                             console.log(tableList())
                             selsubmod.innerHTML += '<tr><td>' + prog.name + '</td><td>' + prog.description + '</td><td>' + tableList() + '</td></tr>';

                             });
                     },
                     error: function (err) {
                         //alert(JSON.stringify(err));
                         $(err).each(function (index, prog) {
                             $('#msgbox2').css('display', 'block');
                             $("#pmsg").text(prog.responseText);
                         });
                     }
                 });
        }
        function corevalues5() {

            var pid = <%=lblid.Text%>;
            var empid = "<%=lblreviewer2.Text%>";
            var b6 = document.getElementById("B6")
            b6.innerHTML = '';
            var selsubmod = document.getElementById("tableses3");
            //selsubmod.append('<thead> <tr>< th > Comment</th ><th>Date</th></tr ></thead><tbody><tr><td>' +  '</td><td>' +  '</td></tr><tbody>');


            //selsubmod.empty();
            selsubmod.innerHTML = '';
            //alert(empid)
            $.ajax({
                url: "<%= Page.ResolveClientUrl("~/res_new/gos.asmx/getAllComments") %>",
                     method: 'post',
                     data: {
                         pid: pid,
                         empid: empid
                     },
                     dataType: 'json',

                success: function (data) {
                    var b6 = document.getElementById("B6")
                    b6.innerHTML = ''
                    b6.innerHTML = 'All Reviewer II Comments'

                         //var selsubmod = $('#tableComment');
                         var selsubmod = document.getElementById("tableses3");
                         //selsubmod.append('<thead> <tr>< th > Comment</th ><th>Date</th></tr ></thead><tbody><tr><td>' +  '</td><td>' +  '</td></tr><tbody>');


                         //selsubmod.empty();
                    selsubmod.innerHTML = '';
                         $(data).each(function (index, prog) {
                             const tableList = () => {
                                 const types = prog.type.split(';');
                                 let string = '';
                                 types.forEach(type => {
                                     string = string + type + '<br /><br/>';

                                 });
                                 console.log(string)
                                 return string
                             }
                             console.log(tableList())

                             selsubmod.innerHTML += '<tr><td>' + prog.name + '</td><td>' + prog.description + '</td><td>' + tableList() + '</td></tr>';
                       });
                     },
                     error: function (err) {
                         //alert(JSON.stringify(err));
                         $(err).each(function (index, prog) {
                             $('#msgbox2').css('display', 'block');
                             $("#pmsg").text(prog.responseText);
                         });
                     }
                 });
        }--%>
        function loading() {
            $('#coachdate').datetimepicker({
                defaultDate: new Date(),
                format: 'YYYY/MM/DD'
                //changeMonth: true,
                //changeYear: true,
                //minDate: new Date(2017, 0, 1),
                //maxDate: new Date(2018, 0, 1),
                //showOn: "both",
                //buttonText: "<i class='fa fa-calender'></i>"
                //autoclose: true
            })
        }
        function performpoints(id,kpiobjectives,uploadstatus) {
            //alert(uploadstatus)
            var pid = id;
            $('#msgbox22').css('display', 'none');
            $('#performanceid1').val(pid);
            $('#performanceid2').val(kpiobjectives);
            $('#performanceid3').val(uploadstatus);
            $.ajax({
                url: "<%= Page.ResolveClientUrl("~/res_new/gos.asmx/PerformPoints") %>",
                 method: 'post',
                 data: {
                     pid: pid},
                 dataType: 'json',

                 success: function (data) {

                     //var selsubmod = $('#tableComment');
                     var selsubmod = document.getElementById("Select10");
                     

                    // selsubmod.empty();
// selsubmod.append('<option value=-1>--Select KPI Metric--</option>');
                     selsubmod.innerHTML = ''
                     $(data).each(function (index, prog) {
                         console.log(prog)
                         selsubmod.innerHTML +='<option value=' + prog.points + '>' + prog.desc + '</option>'; 
                     });
                     
                     
                 },
                 error: function (err) {
                     //alert(JSON.stringify(err));
                     $(err).each(function (index, prog) {
                         $('#msgbox2').css('display', 'block');
                         $("#pmsg").text(prog.responseText);
                     });
                 }
             });
        }
        function objectivesload() {

            var pid = <%=Request.QueryString("id")%>;
           //alert(pid)
            $.ajax({
                url: "<%= Page.ResolveClientUrl("~/res_new/gos.asmx/PerformObjectives") %>",
                method: 'post',
                data: {
                    pid: pid
                },
                dataType: 'json',

                success: function (data) {
                    
                    //var selsubmod = $('#tableComment');
                    var selsubmod = document.getElementById("grateful");
                    
                    selsubmod.innerHTML =''
                    // selsubmod.empty();
                    // selsubmod.append('<option value=-1>--Select KPI Metric--</option>');
                    $(document).ready(function () {
                        $(selsubmod).multiselect();
                    });

                    $(data).each(function (index, prog) {
                        console.log(prog)
                        selsubmod.innerHTML += '<option value=' + prog.name + '>' + prog.desc + '</option>';
                    });
                    
                },
                error: function (err) {
                    //alert(JSON.stringify(err));
                    $(err).each(function (index, prog) {
                        $('#msgbox2').css('display', 'block');
                        $("#pmsg").text(prog.responseText);
                    });
                }
            });
        }
    </script>
    

    <script type="text/javascript">

        function closeWin() {
            popup.close();   // Closes the new window
        }
   

    </script>
   
</head>
<body>
    <form id="form1" action="">
        <div class="modal fade" tabindex="-1" id="loginModal1"
        data-keyboard="false" data-backdrop="static">
    <div class="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" onclick="hide_msg()" class="close" data-dismiss="modal">
                    &times;
                </button>
                <h4 class="modal-title"><b id="modal_title" runat="server">Schedule Coaching Session</b></h4>
            </div>
            <div class="modal-body">
                <form>
                   
                    
                   
                    <div class="form-group">
                        <div class="col-md-12">
                            <label>Reviewer Name</label>
                             <input id="Text1"  class="form-control" type="text" runat="server"   disabled="disabled" />
                                    <input id="Text3"  class="form-control" type="text" runat="server" Visible="false"  />
                                  
                        </div>
                    </div>
                    
                     
                    <div class="form-group">
                        <div class="col-md-12">
                            <label> Date</label>
                            <br />
                             <input id="coachdate" type="date" name="coachdate" class="form-control"  style="width:100%"   />
                          
                          
                        </div>
                         
                    </div>
                     <div class="form-group">
                        <div class="col-md-12">
                            <label>Time</label>
                           
                              <input style="width:100%;height:40px" type="time" id="appt" name="appt"/>
                                   
                        </div>
                    </div>
                     <div class="form-group">
                        <div class="col-md-12">
                            <label>Performance Objective</label><br />
                             
                            <select style="width:100%"  id="grateful" name="grateful" class="mdb-select colorful-select dropdown-primary md-form" multiple searchable="Search here..">
      
    </select>
                 
                                   
                        </div>
                    </div>
                    
                     <div class="form-group">
                    <div class="col-md-12">
                        <label>Comment</label>
                        <textarea id="comment1" runat="server" class="form-control" type="text" cols="5"></textarea>
                       
                    </div>
                   
                    </div>
                     
                    <div style="display:none;" class="row">
                        <label><input id="self" style="margin-top:10px;" onclick="onchecked()" type="checkbox"/> Self</label>
                    </div>
                    <div id="msgbox20" style="display:none;" class="row text-center">
                      <label class=" m-t-10" id="pmsg" style="color:Red;">Please Complete Every Field !!!</label>
                    </div>
                    <div id="msgbox100" style="display:none;" class="row text-center">
                      <label class=" m-t-10" id="Label11" style="color:Green;"><b>Objective Saved !!!</b></label>
                    </div>  
                </form>
            </div>
            <div class="modal-footer">
                <button id="btnsubmit1" type="button"  runat="server"   onserverclick="submitschedule" class="btn btn-success m-t-10">Save</button>
                <button type="button" onclick="hide_msg()" class="btn btn-danger m-t-10" data-dismiss="modal">Close</button>
            </div>
        </div>
    </div>
</div>
        <div class="modal fade" tabindex="-1" id="loginModal2"
        data-keyboard="false" data-backdrop="static">
    <div class="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" onclick="hide_msg()" class="close" data-dismiss="modal">
                    &times;
                </button>
                <h4 class="modal-title"><b id="B5" runat="server"></b></h4>
            </div>
            <div class="modal-body">
                <form>
                   
                    <h3 class="text-center">Are You sure you want to submit</h3>
                   
                    
                </form>
            </div>
            <div class="modal-footer">
                <button id="Button16" type="button"  runat="server"   onserverclick="btnSubmitReview_Click" class="btn btn-success m-t-10">Yes</button>
                <button type="button" onclick="hide_msg()" class="btn btn-danger m-t-10" data-dismiss="modal">No</button>
            </div>
        </div>
    </div>
</div>
          
                                                                   <div class="modal fade" tabindex="-1" id="loginModal"
        data-keyboard="false" data-backdrop="static">
    <div class="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" onclick="hide_msg()" class="close" data-dismiss="modal">
                    &times;
                </button>
                <h4 class="modal-title"><b id="B1" runat="server">Reviewee Comment</b></h4>
            </div>
            <div class="modal-body">
                <form>
                   
                    
                    <div class="form-group">
                    <div class="col-md-12">
                        <label>Comment</label>
                        <textarea id="comment"  class="form-control" type="text" cols="5"></textarea>
                       
                    </div>
                   
                    </div>
                     
                    <div class="form-group">
                        <div class="col-md-12">
                            <label for="birthday">Achievement Date</label><br />
                             <input type="date" style="width:100%" id="birthday" name="birthday">
                            <textarea id="performanceid" style="display:none"  ></textarea>
                          <%--   <telerik:RadDatePicker ID="RadDatePicker1" runat="server" AutoPostBack="False" ForeColor="#666666"
                                     RenderMode="Lightweight"  Skin="Bootstrap" Width="100%">
                                    <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True"
                                        FastNavigationNextText="&amp;lt;&amp;lt;" RenderMode="Lightweight" Skin="Bootstrap">
                                    </Calendar>
                                    <DateInput DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" LabelWidth="40%"
                                        AutoPostBack="True" RenderMode="Lightweight">
                                        <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                        <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                                        <FocusedStyle Resize="None"></FocusedStyle>
                                        <DisabledStyle Resize="None"></DisabledStyle>
                                        <InvalidStyle Resize="None"></InvalidStyle>
                                        <HoveredStyle Resize="None"></HoveredStyle>
                                        <EnabledStyle Resize="None"></EnabledStyle>
                                    </DateInput>
                                    <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                </telerik:RadDatePicker>--%>
                            
                        </div>
                         
                    </div>
                  
                    <div style="display:none;" class="row">
                        <label><input id="self" style="margin-top:10px;" onclick="onchecked()" type="checkbox"/> Self</label>
                    </div>
                    <div id="msgbox2" style="display:none;" class="row text-center">
                      <label class=" m-t-10" id="pmsg" style="color:Red;">Please Complete Every Field !!!</label>
                    </div>
                    <div id="msgbox1" style="display:none;" class="row text-center">
                      <label class=" m-t-10" id="Label1" style="color:Green;"><b>Objective Saved !!!</b></label>
                    </div>  
                </form>
            </div>
            <div class="modal-footer">
                <button id="btnsubmit2" type="button" onclick="commentsubmit()" class="btn btn-success m-t-10">Save</button>
                <button type="button" onclick="hide_msg()" class="btn btn-danger m-t-10" data-dismiss="modal">Close</button>
            </div>
        </div>
    </div>
</div>

        <div class="modal fade" tabindex="-1" id="scoreModal"
        data-keyboard="false" data-backdrop="static">
    <div class="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" onclick="hide_msg()" class="close" data-dismiss="modal">
                    &times;
                </button>
                <h4 class="modal-title"><b id="pointsheader" runat="server">Schedule Training</b></h4>
            </div>
            <div class="modal-body">
                
             <form>
                 <textarea id="performanceid1" style="display:none"  ></textarea>
                 <textarea id="performanceid2" style="display:none"  ></textarea>
                 <textarea id="performanceid3" style="display:none"  ></textarea>
                 <label for="cars">Choose Your Score:</label>
  <select style="width:100%;height:40PX" name="cars" id="Select10">
    <option>--Select KPI Metric--</option>
    
  </select>
 
             </form>
                <div id="msgbox22" style="display:none;" class="row text-center">
                      <label class=" m-t-10" id="pmsg1" style="color:Red;">Cannot update actual performance result. It's uploaded centrally by admin</label>
                    </div>
            </div>
            <div class="modal-footer">
                <button id="Button11" type="button"  runat="server"   onclick="pointsubmit()" class="btn btn-success m-t-10">Save</button>
                <button type="button" onclick="hide_msg()" class="btn btn-danger m-t-10" data-dismiss="modal">Close</button>
            </div>
        </div>
    </div>
</div>
         <div class="modal fade" tabindex="-1" id="commentModal"
        data-keyboard="false" data-backdrop="static">
    <div class="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" onclick="hide_msg()" class="close" data-dismiss="modal">
                    &times;
                </button>
                <h4 class="modal-title"><b id="B3" runat="server">Reviewee Comments</b></h4>
            </div>
            <div class="modal-body">
                
                   
                <table class="table">
    <thead>
      <tr style="font-size:20px;font-weight:bold">
        <th>Comment</th>
        <th>Date</th>
        
      </tr>
    </thead>
    <tbody id ="tableses">
    <%--  <tr>
          <td>john</td>
          <td>doe</td>

      </tr>--%>
                                </tbody>
        </table>
                
            </div>
            <div class="modal-footer">
                <button id="Button12" type="button"  runat="server"   onserverclick="submitschedule" class="btn btn-success m-t-10">Save</button>
                <button type="button" onclick="hide_msg()" class="btn btn-danger m-t-10" data-dismiss="modal">Close</button>
            </div>
        </div>
    </div>
</div>
        <div class="modal fade" tabindex="-1" id="commentModal1"
        data-keyboard="false" data-backdrop="static">
    <div class="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" onclick="hide_msg()" class="close" data-dismiss="modal">
                    &times;
                </button>
                <h4 class="modal-title"><b id="B2" runat="server">Reviewer I comments</b></h4>
            </div>
            <div class="modal-body">
                
                   
                <table class="table">
    <thead>
      <tr style="font-size:20px;font-weight:bold">
        <th>Comment</th>
        <th>Date</th>
        
      </tr>
    </thead>
    <tbody id ="tableses1">
    <%--  <tr>
          <td>john</td>
          <td>doe</td>

      </tr>--%>
                                </tbody>
        </table>
                
            </div>
            <div class="modal-footer">
                <button id="Button13" type="button"  runat="server"   onserverclick="submitschedule" class="btn btn-success m-t-10">Save</button>
                <button type="button" onclick="hide_msg()" class="btn btn-danger m-t-10" data-dismiss="modal">Close</button>
            </div>
        </div>
    </div>
</div>
        <div class="modal fade" tabindex="-1" id="commentModal2"
        data-keyboard="false" data-backdrop="static">
    <div class="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" onclick="hide_msg()" class="close" data-dismiss="modal">
                    &times;
                </button>
                <h4 class="modal-title"><b id="B4" runat="server">Reviewer II Comments</b></h4>
            </div>
            <div class="modal-body">
                
                   
                <table class="table">
    <thead>
      <tr style="font-size:20px;font-weight:bold">
        <th>Comment</th>
        <th>Date</th>
        
      </tr>
    </thead>
    <tbody id ="tableses2">
    <%--  <tr>
          <td>john</td>
          <td>doe</td>

      </tr>--%>
                                </tbody>
        </table>
                
            </div>
            <div class="modal-footer">
                <button id="Button14" type="button"  runat="server"   onserverclick="submitschedule" class="btn btn-success m-t-10">Save</button>
                <button type="button" onclick="hide_msg()" class="btn btn-danger m-t-10" data-dismiss="modal">Close</button>
            </div>
        </div>
    </div>
</div>

         <div class="modal fade" tabindex="-1" id="commentModal3"
        data-keyboard="false" data-backdrop="static">
    <div class="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" onclick="hide_msg()" class="close" data-dismiss="modal">
                    &times;
                </button>
                <h4 class="modal-title"><b id="B6" >All Comments</b></h4>
            </div>
            <div class="modal-body">
                
                   
                <table class="table">
    <thead>
      <tr style="font-size:20px;font-weight:bold">
        <th>Kpi Objectives</th>
          <th>Success Targets</th>
        <th>Comments</th>
        
      </tr>
    </thead>
    <tbody id ="tableses3">
    <%--  <tr>
          <td>john</td>
          <td>doe</td>

      </tr>--%>
                                </tbody>
        </table>
                
            </div>
            <div class="modal-footer">
                <button id="Button17" type="button"  runat="server"   onserverclick="download_click" class="btn btn-success m-t-10 glyphicon glyphicon-circle-arrow-down"></button>
                <button type="button" onclick="hide_msg()" class="btn btn-danger m-t-10" data-dismiss="modal">Close</button>
            </div>
        </div>
    </div>
</div>
    <div class="container col-md-12">
     <div id="divalert" runat="server" visible="false" class="alert alert-info">
                    <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <strong
                id="msgalert" runat="server">Danger!</strong>
                
                    <asp:Label ID="lblid" runat="server" Font-Names="Verdana" Font-Size="1px" Visible="False"></asp:Label>
                    <asp:Label ID="lblend" runat="server" Font-Names="Verdana" Font-Size="1px" Visible="False"></asp:Label>
                    <asp:Label ID="lblvisibleI" runat="server" Font-Names="Verdana" Font-Size="1px" Visible="False"></asp:Label>
                    <asp:Label ID="lblreviewer" runat="server" Font-Names="Verdana" Font-Size="1px" Visible="False"></asp:Label>
                    <asp:Label ID="Label15" runat="server" Font-Names="Verdana" Font-Size="1px" Visible="False"></asp:Label>
                    <asp:Label ID="lblQuestID" runat="server" Font-Names="Verdana" Font-Size="1px" Visible="False"></asp:Label>
                     <asp:Label ID="lblrevieweremail" runat="server" Font-Names="Verdana" Font-Size="1px" Visible="False"></asp:Label>
                     <asp:Label ID="empemail" runat="server" Font-Names="Verdana" Font-Size="1px" Visible="False"></asp:Label>
                     
                    <asp:Label ID="lblQuestCount" runat="server" Font-Names="Verdana" Font-Size="1px" Visible="False"></asp:Label>
                    <asp:Label ID="lblreviewer2" runat="server" Font-Names="Verdana" Font-Size="1px" Visible="False"></asp:Label>
                    <asp:Label ID="lblvisibleII" runat="server" Font-Names="Verdana" Font-Size="1px" Visible="False"></asp:Label>
                    <asp:TextBox ID="txtEmpID" runat="server" Font-Size="1px" Visible="False"></asp:TextBox>
                     <asp:TextBox ID="txtdept" runat="server" Font-Size="1px"  Visible="false"></asp:TextBox>
                    <asp:TextBox ID="txtlocation" runat="server" Font-Size="1px" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="Period" runat="server" Font-Size="1px" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="MrgEndcycle" runat="server" Font-Size="1px" Visible="false"></asp:TextBox>
                    
                            <asp:UpdatePanel runat="server" ID="UpdatePanel" UpdateMode="Always">
                                <ContentTemplate>
                <asp:Label ID="lblMyRating" runat="server" Font-Names="Verdana" Font-Size="1px" Visible="False"></asp:Label>
                                </ContentTemplate>
                                <Triggers>
                <asp:AsyncPostBackTrigger ControlID="rdoMyRatings" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>

                            <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Always">
                                <ContentTemplate>
                <asp:Label ID="lblMgrRating" runat="server" Font-Names="Verdana" Font-Size="1px" Visible="False"></asp:Label>
                                </ContentTemplate>
                                <Triggers>
                <asp:AsyncPostBackTrigger ControlID="rdoMgrRatings" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>

                            <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                <ContentTemplate>
                <asp:Label ID="lblMgrRating2" runat="server" Font-Names="Verdana" Font-Size="1px" Visible="False"></asp:Label>
                                </ContentTemplate>
                                <Triggers>
                <asp:AsyncPostBackTrigger ControlID="rdoMgrRatings2" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                </div>
    <div class="panel panel-success">
                <div class="panel-heading">
                        <h5 id="pagetitle" runat="server" class="page-title">Performance Appraisal Feedback Form</h5>
                </div>
               
                <div style="display:none" id="divemplink" runat="server" class="row">
                <div id="divjoboffer" runat="server" class="col-sm-3 col-md-12 col-xs-6 pull-left">
                    <p>
                        <a href="AppraisalFeedbackList"><u>Appraisal Feedback</u></a>
                        <label>
                            >
                        </label>
                        <a href="#"><u>Performance Appraisal Feedback Form</u></a>
                    </p>
                </div>
            </div>
                <div id="reviewerdetails" runat="server" class="row col-md-12 m-t-10">
                    <div class=" col-md-4">
                        <div class="form-group">
                            <label>
                                REVIEW YEAR</label>
                            <input id="reviewyear" runat="server" class="form-control" type="text" disabled="disabled" />
                        </div>
                    </div>
                    <div class=" col-md-4">
                        <div class="form-group">
                            <label>
                                        REVIEW PERIOD START</label>
                            <input id="reviewstart" runat="server" class="form-control" type="text" disabled="disabled" />
                        </div>
                    </div>
                    <div class=" col-md-4">
                        <div class="form-group">
                            <label>
                                                REVIEW PERIOD END</label>
                            <input id="reviewend" runat="server" class="form-control" type="text" disabled="disabled" />
                        </div>
                    </div>
                    <div class=" col-md-4">
                        <div class="form-group">
                            <label>
                                                        EMPLOYEE</label>
                            <input id="aname" runat="server" class="form-control" type="text" disabled="disabled" />
                        </div>
                    </div>
                    <div class=" col-md-4">
                        <div class="form-group">
                            <label>
                                                                JOB GRADE</label>
                            <input id="ajobgrade" runat="server" class="form-control" type="text" disabled="disabled" />
                        </div>
                    </div>
                    <div class=" col-md-4">
                        <div class="form-group">
                            <label>
                                                                        JOB TITLE</label>
                            <input id="ajobtitle" runat="server" class="form-control" type="text" disabled="disabled" />
                        </div>
                    </div>

                    <div class=" col-md-4">
                        <div class="form-group">
                            <label>
                                                                                LENGTH OF SERVICE</label>
                            <input id="alenofservice" runat="server" class="form-control" type="text" disabled="disabled" />
                        </div>
                    </div>
                    <div class=" col-md-4">
                        <div class="form-group">
                            <label>
                                                                                        TIME IN PRESENT POSITION</label>
                            <input id="apresentposition" runat="server" class="form-control" type="text" disabled="disabled" />
                        </div>
                    </div>
                    <div class="col-md-4 m-t-20 form-group pull-right">
                     <label>&nbsp;&nbsp;&nbsp; </label>
                        <button id="btnsubmit" onserverclick="btnEndcycle" visible="false" style="width:150px;" runat="server" type="submit" class="btn btn-success">End Cycle</button>
                        <button id="btnrefresh" data-toggle="tooltip" data-original-title="Link to Query" runat="server" onserverclick="btnDev_Click3" type="submit"
                        style="height:35px" class="btn btn-default glyphicon glyphicon-hand-up"></button>
                        <button id="Button5" data-toggle="tooltip" data-original-title="Link to Training" runat="server" onserverclick="btnDev_Click1" type="submit"
                        style="height:35px" class="btn btn-default glyphicon glyphicon-education"></button>
                        <button id="Button6" data-toggle="tooltip" data-original-title="Link to Feedback Nugget" runat="server" onserverclick="btnDev_Click2" type="submit"
                        style="height:35px;" class="btn btn-default glyphicon glyphicon-bullhorn"></button>
                        <button id="Button1" data-toggle="tooltip" data-original-title="Link to Development Plan" runat="server" onserverclick="btnDev_Click" type="submit"
                        style="height:35px;" class="btn btn-default glyphicon glyphicon-arrow-up"></button>
                        <a href="#" data-toggle="modal" data-target="#loginModal1" onclick='objectivesload()'>
                        <button id="Button7" data-toggle="tooltip" data-original-title="Schedule coaching session" runat="server"  type="submit"
                        style="height:35px" class="btn btn-default glyphicon glyphicon-calendar"></button></a> 
                        <button id="Button10" data-toggle="tooltip" data-original-title="View  coaching session" runat="server"  type="submit"
                        style="height:35px" onserverclick="Coaching_Sessions"  class="btn btn-default glyphicon glyphicon-list-alt"></button>
                    </div>
                    <div id="divoverdesc" runat="server" class=" col-md-12">
                        <div class="form-group">
                            <label>
                                OVERALL REMARK</label>
                            <input id="aoverdesc" runat="server" class="form-control" type="text" disabled="disabled" />
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="panel panel-success">
                            <div class="panel-heading">
                                <b>PERFORMANCE REVIEWER</b>
                            </div>
                            <div class="panel-body">
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            1. FIRST REVIEWER</label>
                                        <input id="areviewer1" runat="server" class="form-control" type="text" disabled="disabled" />
                                    </div>
                                </div>
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            2. SECOND REVIEWER</label>
                                        <input id="areviewer2" runat="server" class="form-control" type="text" disabled="disabled" />
                                    </div>
                                </div>
                            </div>
                        </div>    
                    </div>
                    <div class="col-md-12 text-center">
                                                <button id="Button3" runat="server"  type="submit"
                                                style="width: 150px" onserverclick="btnContinue_Click" class="btn btn-primary btn-success">
                                                Feedback</button>
                                                <button id="Button4" runat="server"  type="submit"
                                                style="width: 150px" onserverclick="btnContinue_Click" class="btn btn-primary btn-info">
                                                Feedback</button>
                                            <button id="Button2" runat="server" onserverclick="btnClose_Click" type="submit"
                                                style="width: 150px" class="btn btn-primary btn-danger">
                                                Back</button>
                          <asp:button ID="Button19" runat="server"  type="submit"
                                                style="display:none;" OnClick="btnContinue_Click"  Text=""/>
                                                
                        <a href="#" data-toggle="modal" data-target="#loginModal2">
                         <button id="Button9" runat="server"  type="submit"
                                                style="width: 180px"  class="btn btn-primary btn-info">
                                               Complete Feedback</button></a>
                      
                         <button id="Button15" runat="server" onserverclick="btnSubmitRecommend_Click"  type="submit"
                                                style="width: 180px"  class="btn btn-primary btn-info">
                                               Complete Feedback</button>
                                 </div>
                    </div>
                    <div class="row">
                        <div class=" col-md-12">
                        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                            <asp:View ID="View1" runat="server">
                                <div id="nuggetsquestion" runat="server" style="display:none;" class="row">
                                    <div id="questionaira" runat="server"  class=" col-md-12">
                                    <label id="aratingdesc" runat ="server"> </label>
                                        <div class="">
                                            <div style="display:none;" class="panel-heading">
                                                <b>QUESTIONAIRE</b>
                                                <h6 id="akpitype" runat="server" class="page-title" visible="false">
                                                    Performance Appraisal Objective:</h6>
                                            </div>
                                            <div class="panel-body">
                                                <div class=" col-md-12">
                                                    <div class="row">
                                                        <div class=" col-md-12">
                                                            <div class="form-group">
                                                                <label id="aQuestNo" runat="server" visible="false">
                                                                    1.</label>
                                                                <label id="apageview" runat="server">
                                                                </label>
                                                                <input id="aObjective" runat="server" class="form-control" type="text" readonly="readonly" />
                                                                <label>
                                                                    <i id="aObjDesc" runat="server">1.</i></label>
                                                            </div>
                                                        </div>
                                                       
                                                        <div class=" col-md-12">
                                                            <div class="form-group">
                                                                <label id="lll" runat="server">
                                                                    SUCCESS TARGET:</label><br />
                                                                <label id="aMyObjective" style="text-indent: 20px; white-space: pre; display:inline" runat="server">
                                                                </label>
                                                            </div>
                                                        </div>
                                                         <div id="emp_app" runat="server">
                                                        <div class=" col-md-12">
                                                            <div class="form-group">
                                                                <label>
                                                                    COMMENT</label>
                                                                <textarea id="aMyPerformance" runat="server" class="form-control" rows="3"></textarea>
                                                                <div class="row">
                                                                <asp:RadioButtonList ID="rdoMyRatings" runat="server" Font-Names="Verdana" Font-Size="13px"
                                                                    ForeColor="#666666" AutoPostBack="True" 
                                                                        Width="100%">
                                                                </asp:RadioButtonList>
                                                                </div>
                                                                
                                                            </div>
                                                        </div>
                                                        </div>
                                                        <div id="divreviewer1" runat="server" class=" col-md-12">
                                                            <div class="form-group">
                                                                <label>
                                                                    FIRST REVIEWER COMMENT</label>
                                                                <textarea id="amanager1" runat="server" class="form-control" rows="5"></textarea>
                                                                <asp:RadioButtonList ID="rdoMgrRatings" runat="server" Font-Names="Verdana" Font-Size="13px"
                                                                    ForeColor="#666666" AutoPostBack="True" Width="70%">
                                                                </asp:RadioButtonList>
                                                            </div>
                                                        </div>
                                                        <div id="divreviewer2" runat="server" class=" col-md-12">
                                                            <div class="form-group">
                                                                <label>
                                                                    SECOND REVIEWER COMMENT</label>
                                                                <textarea id="amanager2" runat="server" class="form-control" rows="5"></textarea>
                                                                <asp:RadioButtonList ID="rdoMgrRatings2" runat="server" Font-Names="Verdana" Font-Size="13px"
                                                                    ForeColor="#666666" AutoPostBack="True" Width="70%">
                                                                </asp:RadioButtonList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12 text-center">
                                            <button id="btprevious" runat="server" onserverclick="btnPrevious_Click" type="button"
                                                style="width: 150px" class="btn btn-primary btn-success">
                                                Previous</button>
                                            <button id="btnext" runat="server" onserverclick="btnNext_Click" type="button" style="width: 150px"
                                                class="btn btn-success">
                                                Next</button>                                            
                                            <button id="btSubmit" runat="server" onserverclick="btnSubmit_Click" type="button"
                                                style="width: 150px" class="btn btn-info">
                                                Finish</button>

                                            <button runat="server" onserverclick="btnClose_Click" type="button" style="width: 150px"
                                                class="btn btn-danger">
                                                Back</button>
                                        </div>
                                    </div>
                                </div>
                            </asp:View>
                            
                            <asp:View ID="View2" runat="server">
                                <div class="row">
                              <%--  <div id="gridsss" runat="server" class=" col-md-12">
                                        <div class="panel-heading">
                                            <b style="color:Green;">FEEDBACK REVIEW</b>
                                        </div>
                                        <div class="m-b-20">
                                            <div class="col-md-12 text-left">
                                                <button id="btback" runat="server" onserverclick="btnBack_Click" type="submit" style="width: 150px"
                                                    class="btn btn-primary btn-info">
                                                    << Back</button>
                                                <asp:Button ID="btSubmitReview" runat="server" Text="Submit For Review" ForeColor="White"
                                                    Width="170px" Height="35px" BorderStyle="None" Font-Size="14px" 
                                                    OnClientClick="Confirm()" ToolTip="Complete" CssClass="btn btn-success" />
                                                <button id="bt360degree" runat="server" onserverclick="btn360degree_Click" type="submit"
                                                    style="width: 150px" class="btn btn-purple">
                                                    360 Feedback</button>
                                                <asp:Button ID="btnDisagree" runat="server" Text="Disagree First Review" ForeColor="White"
                                                    Width="200px" Height="35px" BorderStyle="None" Font-Size="14px" Visible="False"
                                                    OnClientClick="ConfirmDisagree()" ToolTip="click to disagree" CssClass="btn btn-danger" />
                                            </div>
                                            <div id="divrecommendation" runat="server" class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <label>
                                                            RECOMMENDATION</label>
                                                        <telerik:RadComboBox ID="cborecommendation" runat="server" AutoPostBack="True" Skin="Bootstrap"
                                                            ForeColor="#666666" RenderMode="Lightweight" Width="100%" ResolvedRenderMode="Classic">
                                                            <Items>
                                                                <telerik:RadComboBoxItem runat="server" Text="--select--" Value="--select--" />
                                                                <telerik:RadComboBoxItem runat="server" Text="Development Required" Value="Development Required" />
                                                                <telerik:RadComboBoxItem runat="server" Text="Merit Increase" Value="Merit Increase" />
                                                                <telerik:RadComboBoxItem runat="server" Text="No Recommendation" Value="No Recommendation" />
                                                                <telerik:RadComboBoxItem runat="server" Text="Promote" Value="Promote" />
                                                                <telerik:RadComboBoxItem runat="server" Text="Terminate Job" Value="Terminate Job" />
                                                                <telerik:RadComboBoxItem runat="server" Text="Transfer" Value="Transfer" />
                                                            </Items>
                                                        </telerik:RadComboBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <telerik:RadGrid ID="gridReviewee" runat="server" AllowMultiRowSelection="True" AllowPaging="True"
                                                        AllowSorting="True" AutoGenerateColumns="False" GridLines="Both" BorderWidth="1px"
                                                        BorderColor="#CCCCCC" DataKeyNames="ID" EnableGroupsExpandAll="True" Font-Names="Verdana"
                                                        Font-Size="12px" GroupPanelPosition="Top" PageSize="50" RenderMode="Lightweight"
                                                        ResolvedRenderMode="Classic" ShowFooter="True" ShowGroupPanel="True" ShowStatusBar="True"
                                                        Width="100%" Visible="False">
                                                        <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                                        <MasterTableView EnableGroupsExpandAll="True" Width="100%">
                                                            <GroupByExpressions>
                                                                <telerik:GridGroupByExpression>
                                                                    <SelectFields>
                                                                        <telerik:GridGroupByField FieldAlias="" FieldName="KPIType" />
                                                                    </SelectFields>
                                                                    <GroupByFields>
                                                                        <telerik:GridGroupByField FieldName="KPIType" SortOrder="Ascending" />
                                                                    </GroupByFields>
                                                                </telerik:GridGroupByExpression>
                                                            </GroupByExpressions>
                                                            <Columns>
                                                                <telerik:GridBoundColumn DataField="KPIObjectives" HeaderButtonType="TextButton"
                                                                    ItemStyle-VerticalAlign="Top" HeaderText="KPI Type">
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="objectives" HeaderButtonType="TextButton" 
                                                                    ItemStyle-VerticalAlign="Top" UniqueName="objectives" HeaderText="Objectives"
                                                                    >
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="successtarget" HeaderButtonType="TextButton"
                                                                    ItemStyle-VerticalAlign="Top" UniqueName="successtarget" 
                                                                    HeaderText="Success Target" >
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="EMpIDComment" HeaderButtonType="TextButton" 
                                                                    ItemStyle-VerticalAlign="Top" UniqueName="empidcomment" HeaderText="Reviewee Comment"
                                                                    >
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="EMpIDRating" HeaderButtonType="TextButton" 
                                                                    ItemStyle-VerticalAlign="Top" HeaderText="Points" 
                                                                    >
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="supervisorcomment" HeaderButtonType="TextButton"
                                                                    ItemStyle-VerticalAlign="Top" UniqueName="supervisorcomment" 
                                                                    HeaderText="Reviewer I Comment" >
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="mgrIDRating" HeaderButtonType="TextButton" 
                                                                    ItemStyle-VerticalAlign="Top" HeaderText="Points"
                                                                    >
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="supervisorcomment2" HeaderButtonType="TextButton"
                                                                    ItemStyle-VerticalAlign="Top" UniqueName="supervisorcomment2" 
                                                                    HeaderText="Reviewer II Comment" >
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="mgrIDRating2" HeaderButtonType="TextButton" 
                                                                    ItemStyle-VerticalAlign="Top" HeaderText="Points"
                                                                    >
                                                                </telerik:GridBoundColumn>
                                                            </Columns>
                                                        </MasterTableView>
                                                        <ClientSettings AllowColumnsReorder="True" AllowDragToGroup="True" ReorderColumnsOnClient="True">
                                                            <Resizing AllowColumnResize="True" AllowRowResize="True" EnableRealTimeResize="True"
                                                                ResizeGridOnColumnResize="False" />
                                                        </ClientSettings>
                                                        <GroupingSettings ShowUnGroupButton="true" />
                                                        <FilterMenu RenderMode="Lightweight">
                                                        </FilterMenu>
                                                        <HeaderContextMenu RenderMode="Lightweight">
                                                        </HeaderContextMenu>
                                                    </telerik:RadGrid>
                                                </div>
                                            </div>
                                                    
                    
         
                                              
                                        </div>
                                    </div>--%>
                                </div>
                                
                            </asp:View>
                             <asp:View ID="View3" runat="server">
                                <br />
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                            <div class="m-b-20">
                                                <div class="form-group">
                                                    <h3 id="H1" runat="server" class="page-title text-center">
                                                        <b>Appraisal Successfully Submitted</b></h3>
                                                </div>
                                                <div class="col-md-12 m-t-20 m-b-20 text-center">
                                                    <button id="btnend" runat="server" onserverclick="btnClose_Click" type="submit" style="width: 150px"
                                                        class="btn btn-primary btn-success">
                                                        OK</button>
                                                </div>
                                            </div>
                                    </div>
                                </div>
                            </asp:View>
                            
                        </asp:MultiView>
                                 <div class="row" runat="server" id="Views4">
                                    <div class="col-md-3 pull-right">
                                        <a href="#" data-toggle="modal" data-target="#commentModal3" onclick='corevalues3()' >
                                         <button id="Button18" type="button" data-toggle="tooltip" data-original-title="View Comment" runat="server" class="glyphicon glyphicon-eye-open btn btn-default btn-sm" 
                        style="margin-left:10px;margin-right:10px;height: 35px;"></button> </a>
                                        <%-- <a href="#" data-toggle="modal" data-target="#commentModal3" onclick='corevalues4()' >
                                         <button id="Button19" type="button" data-toggle="tooltip" data-original-title="View All Reviewer I Comments" runat="server" class="glyphicon glyphicon-eye-close btn btn-default btn-sm" 
                        style="margin-left:10px;margin-right:10px;height: 35px;"></button> </a>
                                         <a href="#" data-toggle="modal" data-target="#commentModal3" onclick='corevalues5()' >
                                         <button id="Button20" type="button" data-toggle="tooltip" data-original-title="View All Reviewer II Comments" runat="server" class="glyphicon glyphicon-fire btn btn-default btn-sm" 
                        style="margin-left:10px;margin-right:10px;height: 35px;"></button> </a>--%>
                                    </div>
                                </div>
                            <div class="row" id="mate" runat="server">
                            <div class="col-md-12">
                                     <div class="form-group">
                                                        <label>
                                                            RECOMMENDATION</label>
                                                        <telerik:RadComboBox ID="RadComboBox2" runat="server" AutoPostBack="True" Skin="Bootstrap"
                                                            ForeColor="#666666" RenderMode="Lightweight" Width="100%" ResolvedRenderMode="Classic">
                                                            <Items>
                                                                <telerik:RadComboBoxItem runat="server" Text="--select--" Value="--select--" />
                                                                <telerik:RadComboBoxItem runat="server" Text="Development Required" Value="Development Required" />
                                                                <telerik:RadComboBoxItem runat="server" Text="Merit Increase" Value="Merit Increase" />
                                                                <telerik:RadComboBoxItem runat="server" Text="No Recommendation" Value="No Recommendation" />
                                                                <telerik:RadComboBoxItem runat="server" Text="Promote" Value="Promote" />
                                                                <telerik:RadComboBoxItem runat="server" Text="Terminate Job" Value="Terminate Job" />
                                                                <telerik:RadComboBoxItem runat="server" Text="Transfer" Value="Transfer" />
                                                                <telerik:RadComboBoxItem runat="server" Text="Others" Value="Others" />
                                                            </Items>
                                                        </telerik:RadComboBox>
                                                    </div>
                                </div>
                                
                                <div class="col-md-12">
                                    <div class="form-group">
                                    <label>RECOMMENDATION COMMENTS</label><br />
                                    <input type="text" class="form-control" id="recomm"  runat="server"/>
                                        </div>
                                </div>
                                </div>
                                <div class="row" id="mate2" runat="server">
                                    <div class="col-md-12 text-center">
                                          <asp:Button ID="btSubmitReview" runat="server" Text="Submit For Review" ForeColor="White"
                                                    Width="170px" Height="35px" BorderStyle="None" Font-Size="14px" 
                                                    OnClientClick="Confirm()" ToolTip="Complete" CssClass="btn btn-success" />
                                               <%-- <button id="bt360degree" runat="server" onserverclick="btn360degree_Click" type="submit"
                                                    style="width: 150px" class="btn btn-purple">
                                                    360 Feedback</button>--%>
                                                <asp:Button ID="btnDisagree" runat="server" Text="Disagree First Review" ForeColor="White"
                                                    Width="200px" Height="35px" BorderStyle="None" Font-Size="14px" Visible="False"
                                                    OnClientClick="ConfirmDisagree()" ToolTip="click to disagree" CssClass="btn btn-danger" />
                                    </div>
                                </div>
                           
                                
                        <div class="table-responsive">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Always">
                    <ContentTemplate>
                            <asp:GridView ID="gridskills" runat="server" OnSorting="SortRecords" AllowSorting="True"
                            BorderStyle="Solid" Font-Names="Verdana" AllowPaging="True" PageSize="100" DataKeyNames="id"
                            OnRowDataBound="OnRowDataBound" Width="100%" Height="50px" ToolTip="click row to select record"
                            Font-Size="12px" ShowHeaderWhenEmpty="True" EmptyDataText="No data to display"
                            AutoGenerateColumns="False" GridLines="Both" ForeColor="#666666" BorderWidth="1px"
                            BorderColor="#CCCCCC" CssClass="table table-condensed">
                                <RowStyle BackColor="White" />
                                <Columns>
                                    <asp:BoundField DataField="KPIType" HeaderText="KPI Type" SortExpression="kpitype" ItemStyle-VerticalAlign="Top" /> 
                                  
                                  
                                      <asp:BoundField DataField="objectives" HeaderText="KPI Objectives" SortExpression="objectives"
                                        ItemStyle-VerticalAlign="Top" />
                                     <asp:BoundField DataField="successtarget" HeaderText="Success Target" SortExpression="successtarget"
                                        ItemStyle-VerticalAlign="Top" />
                                    <asp:TemplateField HeaderText="Reviewee comment" ItemStyle-Font-Bold="true" SortExpression="name">
                                    <ItemTemplate>
                                        <a href="#" data-toggle="modal" data-target="#loginModal" onclick='Editcorevalues("<%# Eval("id") %>");'>
                                    <button id="btAdd" type="button" data-toggle="tooltip" data-original-title="Add Comment" runat="server" class="glyphicon glyphicon-plus btn btn-default btn-sm" 
                        style="margin-left:10px;margin-right:10px;height: 35px;"></button> </a>
                                        <a href="#" data-toggle="modal" data-target="#commentModal" onclick='corevalues("<%# Eval("id") %>");' >
                                         <button id="Button8" type="button" data-toggle="tooltip" data-original-title="View Comment" runat="server" class="glyphicon glyphicon-eye-open btn btn-default btn-sm" 
                        style="margin-left:10px;margin-right:10px;height: 35px;"></button> </a>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                       <asp:TemplateField HeaderText="Reviewee comment" ItemStyle-Font-Bold="true" SortExpression="name">
                                    <ItemTemplate>
                                       
                                        <a href="#" data-toggle="modal" data-target="#commentModal" onclick='corevalues("<%# Eval("id") %>");' >
                                         <button id="Button8" type="button" data-toggle="tooltip" data-original-title="View Comment" runat="server" class="glyphicon glyphicon-eye-open btn btn-default btn-sm" 
                        style="margin-left:10px;margin-right:10px;height: 35px;"></button> </a>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                    <asp:BoundField DataField="EMpIDRating" HeaderText="Reviewee Score" SortExpression="empidrating" />
                                      <asp:TemplateField HeaderText="Reviewee Score" ItemStyle-Width="150px" ItemStyle-Font-Bold="true" SortExpression="period">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="HyperLink1" runat="server" 
                                                Text='<%# Eval("EMpIDRating")%>' />
                                            <a href="#" data-toggle="modal" data-target="#scoreModal" onclick='performpoints("<%# Eval("id") %>","<%# Eval("objectives") %>","<%# Eval("Upload_Status") %>");'>
                                            <button id="Button8" type="button" data-toggle="tooltip" data-original-title="Input Your Score" runat="server" class="glyphicon glyphicon-pencil btn btn-default btn-sm" 
                        style="margin-left:10px;margin-right:10px;height: 35px;"></button> </a>
                                        </ItemTemplate>
                                    </asp:TemplateField>  
                                     <asp:TemplateField HeaderText="Reviewer I comment" ItemStyle-Font-Bold="true" SortExpression="name">
                                    <ItemTemplate>
                                        <a href="#" data-toggle="modal" data-target="#loginModal" onclick='Editcorevalues("<%# Eval("id") %>");'>
                                    <button id="btAdd" type="button" data-toggle="tooltip" data-original-title="Add Comment" runat="server" class="glyphicon glyphicon-plus btn btn-default btn-sm" 
                        style="margin-left:10px;margin-right:10px;height: 35px;"></button> </a>
                                        <a href="#" data-toggle="modal" data-target="#commentModal1" onclick='corevalues1("<%# Eval("id") %>");' >
                                         <button id="Button8" type="button" data-toggle="tooltip" data-original-title="View Comment" runat="server" class="glyphicon glyphicon-eye-open btn btn-default btn-sm" 
                        style="margin-left:10px;margin-right:10px;height: 35px;"></button> </a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Reviewer I comment" ItemStyle-Font-Bold="true" SortExpression="name">
                                    <ItemTemplate>
                                        <a href="#" data-toggle="modal" data-target="#commentModal1" onclick='corevalues1("<%# Eval("id") %>");' >
                                         <button id="Button8" type="button" data-toggle="tooltip" data-original-title="View Comment" runat="server" class="glyphicon glyphicon-eye-open btn btn-default btn-sm" 
                        style="margin-left:10px;margin-right:10px;height: 35px;"></button> </a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                    <asp:BoundField DataField="mgrIDRating" HeaderText="Reviewe I Points" SortExpression="empidrating" />
                                     <asp:TemplateField HeaderText="Reviewer I Point" ItemStyle-Width="150px" ItemStyle-Font-Bold="true" SortExpression="period">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="HyperLink2" runat="server" 
                                                Text='<%# Eval("mgrIDRating")%>' />
                                            <a href="#" data-toggle="modal" data-target="#scoreModal" onclick='performpoints("<%# Eval("id") %>","<%# Eval("objectives") %>","<%# Eval("Upload_Status") %>");'>
                                             <button id="Button8" type="button" data-toggle="tooltip" data-original-title="Input Your Score" runat="server" class="glyphicon glyphicon-pencil btn btn-default btn-sm" 
                        style="margin-left:10px;margin-right:10px;height: 35px;"></button> </a>
                                        </ItemTemplate>
                                    </asp:TemplateField>  
                                    <asp:TemplateField HeaderText="Reviewer II comment" ItemStyle-Font-Bold="true" SortExpression="name">
                                    <ItemTemplate>
                                        <a href="#" data-toggle="modal" data-target="#loginModal" onclick='Editcorevalues("<%# Eval("id") %>");'>
                                    <button id="btAdd" type="button" data-toggle="tooltip" data-original-title="Add Comment" runat="server" class="glyphicon glyphicon-plus btn btn-default btn-sm" 
                        style="margin-left:10px;margin-right:10px;height: 35px;"></button> </a>
                                        <a href="#" data-toggle="modal" data-target="#commentModal2" onclick='corevalues2("<%# Eval("id") %>");' >
                                         <button id="Button8" type="button" data-toggle="tooltip" data-original-title="View Comment" runat="server" class="glyphicon glyphicon-eye-open btn btn-default btn-sm" 
                        style="margin-left:10px;margin-right:10px;height: 35px;"></button> </a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Reviewer II comment" ItemStyle-Font-Bold="true" SortExpression="name">
                                    <ItemTemplate>

                                    <a href="#" data-toggle="modal" data-target="#commentModal2" onclick='corevalues2("<%# Eval("id") %>");' >
                                         <button id="Button8" type="button" data-toggle="tooltip" data-original-title="View Comment" runat="server" class="glyphicon glyphicon-eye-open btn btn-default btn-sm" 
                        style="margin-left:10px;margin-right:10px;height: 35px;"></button> </a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                    <asp:BoundField DataField="mgrIDRating2" HeaderText="Reviewer II Points" SortExpression="empidrating" />
                                     <asp:TemplateField HeaderText="Reviewer II Points" ItemStyle-Width="150px" ItemStyle-Font-Bold="true" SortExpression="period">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="HyperLink1" runat="server" 
                                                Text='<%# Eval("mgrIDRating2")%>' />
                                            <a href="#" data-toggle="modal" data-target="#scoreModal" onclick='performpoints("<%# Eval("id") %>","<%# Eval("objectives") %>","<%# Eval("Upload_Status") %>");'>
                                            <button id="Button8" type="button" data-toggle="tooltip" data-original-title="Input Your Score" runat="server" class="glyphicon glyphicon-pencil btn btn-default btn-sm" 
                        style="margin-left:10px;margin-right:10px;height: 35px;"></button> </a>
                                        </ItemTemplate>
                                    </asp:TemplateField>  
                                </Columns>
                                <HeaderStyle BackColor="White" ForeColor="#1BA691" HorizontalAlign="center" />
                            </asp:GridView>
                        </ContentTemplate>
                               <%-- <Triggers >
                                         <asp:AsyncPostBackTrigger ControlID="Button4" EventName="SelectedIndexChanged" />
                                    </Triggers>--%>
                                </asp:UpdatePanel>
                            <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
                            <script type="text/javascript">
                                $(function () {
                                    $("[id*=gridskills] td").hover(function () {
                                        $("td", $(this).closest("tr")).addClass("hover_row");
                                    }, function () {
                                        $("td", $(this).closest("tr")).removeClass("hover_row");
                                    })
                                })
                            </script>
                        </div>
                    </div>
                      </div>
                    </div>
                </div>
    


    </form>
</body>
</html>
</asp:Content>