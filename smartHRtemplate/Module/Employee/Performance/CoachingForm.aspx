<%@ Page Title="" Language="vb" AutoEventWireup="true" MasterPageFile="~/smartHR.Master" CodeBehind="CoachingForm.aspx.vb" Inherits="GOSHRM.CoachingForm" EnableEventValidation="false" Debug="true" %>


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
            //alert(pid)
            this.kpi_id = lol;
            
           // $("#btnsubmit").attr("disabled", false);
          $('#performanceid').val(lol);
          
        }
        function commentsubmit() {
            $("#btnsubmit").attr("disabled", false);
            var checkBox = document.getElementById("self");
            var kpimetric = {};
            kpimetric.empid = "<%=Session("UserEmpID") %>";
           // alert(kpimetric.empid)
             kpimetric.performanceid = $('#performanceid').val();
             kpimetric.kpiid =  <%=Request.QueryString("id") %>;
             kpimetric.radEndDate = $('#birthday').val();
             kpimetric.obj = $('#comment').val();
            kpimetric.obj2 = $('#takeaways').val();

             console.log("am here", kpimetric.empid);
             if ((kpimetric.radEndDate == "") || (kpimetric.obj == "")) {
                 $('#msgbox2').css('display', 'block');
                 $('#msgbox1').css('display', 'none');
             } else {
                 $('#msgbox2').css('display', 'none');
                 console.log("submit", JSON.stringify(kpimetric));
                 $.ajax({
                     url: "<%= Page.ResolveUrl("~/res_new/gos.asmx/addcomment1") %>",
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
        function corevalues(id) {

            var pid = id;
            var empid = "<%=txtEmpID.Text%>";
            $.ajax({
                url: "<%= Page.ResolveClientUrl("~/res_new/gos.asmx/getcomment1") %>",
                 method: 'post',
                 data: {
                     pid: pid,
                     empid: empid
                 },
                 dataType: 'json',

                 success: function (data) {

                     //var selsubmod = $('#tableComment');
                     var selsubmod = document.getElementById("tableses");
                     //selsubmod.append('<thead> <tr>< th > Comment</th ><th>Date</th></tr ></thead><tbody><tr><td>' +  '</td><td>' +  '</td></tr><tbody>');


                     //selsubmod.empty();
                     selsubmod.innerHTML = '';
                     $(data).each(function (index, prog) {
                         console.log(prog)
                         
                         selsubmod.innerHTML += '<tr><td>' + prog.name + '</td><td>' + prog.type +'</td><td>' + prog.description.split(' ')[0] + '</td></tr>';
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
                url: "<%= Page.ResolveClientUrl("~/res_new/gos.asmx/getcommentmngr1") %>",
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


                     //selsubmod.empty();
                     selsubmod.innerHTML = '';
                     $(data).each(function (index, prog) {
                         console.log(prog)
                         

                         selsubmod.innerHTML += '<tr><td>' + prog.name + '</td><td>'+prog.type+'</td><td>' + prog.description.split(' ')[0] + '</td></tr>';
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
                <h4 class="modal-title"><b id="modal_title" runat="server">Schedule Training</b></h4>
            </div>
            <div class="modal-body">
                <form>
                   
                    
                   
                    <div class="form-group">
                        <div class="col-md-12">
                            <label>Reviewer Name</label>
                             <input id="Text1"  class="form-control" type="text" runat="server"   disabled="disabled" />
                                   
                        </div>
                    </div>
                    
                     
                    <div class="form-group">
                        <div class="col-md-12">
                            <label> Date</label>
                          
                             <telerik:RadDatePicker ID="radEndDate1" runat="server" AutoPostBack="False" ForeColor="#666666"
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
                                </telerik:RadDatePicker>
                        </div>
                         
                    </div>
                     <div class="form-group">
                        <div class="col-md-12">
                            <label>Time</label>
                             <input id="Text2"  class="form-control" type="text" runat="server"    />
                                   
                        </div>
                    </div>
                     <div class="form-group">
                        <div class="col-md-12">
                            <label>Performance Objective</label>
                             <telerik:RadComboBox ID="radObjectives" runat="server" Filter="Contains" checkboxes="True" EnableCheckAllItemsCheckBox="True"
                                           autopostback="True" RenderMode="Lightweight" Width="100%" ForeColor="#666666" Skin="Bootstrap">
                                        </telerik:RadComboBox>
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
                    <div id="msgbox2" style="display:none;" class="row text-center">
                      <label class=" m-t-10" id="pmsg" style="color:Red;">Please Complete Every Field !!!</label>
                    </div>
                    <div id="msgbox1" style="display:none;" class="row text-center">
                      <label class=" m-t-10" id="Label1" style="color:Green;"><b>Objective Saved !!!</b></label>
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
                        <label>Coaching Notes</label>
                        <textarea id="comment"  class="form-control" type="text" cols="12" ></textarea>
                       
                    </div>
                   
                    </div>
                    <div class="form-group">
                     <div class=" col-md-12">
                           <label>Key Takeaways</label>
                         <textarea id="takeaways"  class="form-control" type="text" cols="5" ></textarea>
                        </div>
                        </div>
                     
                    <div class="form-group">
                        <div class="col-md-12">
                            <label for="birthday">Deadline</label><br />
                             <input type="date" style="width:100%" id="birthday" name="birthday">
                            <textarea id="performanceid" style="display:none"  ></textarea>
                        
                            
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
         <div class="modal fade" tabindex="-1" id="commentModal"
        data-keyboard="false" data-backdrop="static">
    <div class="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" onclick="hide_msg()" class="close" data-dismiss="modal">
                    &times;
                </button>
                <h4 class="modal-title"><b id="B3" runat="server">Reviewee Coaching Notes</b></h4>
            </div>
            <div class="modal-body">
                
                   
                <table class="table">
    <thead>
      <tr style="font-size:20px;font-weight:bold">
        <th>Coaching Notes</th>
          <th>Key TakeAways</th>
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
                <h4 class="modal-title"><b id="B2" runat="server">Reviewer Coaching Notes</b></h4>
            </div>
            <div class="modal-body">
                
                   
                <table class="table">
    <thead>
      <tr style="font-size:20px;font-weight:bold">
        <th>Coaching Notes</th>
          <th>Key TakeAways</th>
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
                    <asp:Label ID="Label2" runat="server" Font-Names="Verdana" Font-Size="1px" Visible="False"></asp:Label>
                    <asp:Label ID="lblend" runat="server" Font-Names="Verdana" Font-Size="1px" Visible="False"></asp:Label>
                    <asp:Label ID="lblvisibleI" runat="server" Font-Names="Verdana" Font-Size="1px" Visible="False"></asp:Label>
                    <asp:Label ID="lblreviewer" runat="server" Font-Names="Verdana" Font-Size="1px" Visible="False"></asp:Label>
                    <asp:Label ID="Label15" runat="server" Font-Names="Verdana" Font-Size="1px" Visible="False"></asp:Label>
                    <asp:Label ID="lblQuestID" runat="server" Font-Names="Verdana" Font-Size="1px" Visible="False"></asp:Label>
                     <asp:Label ID="lblrevieweremail" runat="server" Font-Names="Verdana" Font-Size="1px" Visible="False"></asp:Label>
                    <asp:Label ID="lblQuestCount" runat="server" Font-Names="Verdana" Font-Size="1px" Visible="False"></asp:Label>
                    <asp:Label ID="lblreviewer2" runat="server" Font-Names="Verdana" Font-Size="1px" Visible="False"></asp:Label>
                    <asp:Label ID="lblvisibleII" runat="server" Font-Names="Verdana" Font-Size="1px" Visible="False"></asp:Label>
                    <asp:TextBox ID="txtEmpID" runat="server" Font-Size="1px" Visible="False"></asp:TextBox>
                     <asp:TextBox ID="txtdept" runat="server" Font-Size="1px"  Visible="false"></asp:TextBox>
                    <asp:TextBox ID="txtlocation" runat="server" Font-Size="1px" Visible="false"></asp:TextBox>
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
                        <h5 id="pagetitle" runat="server" class="page-title">Performance Appraisal  Form</h5>
                </div>
         <div class="col-md-2 pull-right">
                                <button id="Button2" runat="server" onserverclick="btnClose_Click1" type="submit"
                                                style="width: 150px" class="btn btn-primary btn-danger">
                                                Back</button>
                            </div>
               
                <div style="display:none" id="divemplink" runat="server" class="row">
                <div id="divjoboffer" runat="server" class="col-sm-3 col-md-12 col-xs-6 pull-left">
                    <p>
                        <a href="AppraisalFeedbackList"><u>Appraisal Feedback</u></a>
                        <label>
                            >
                        </label>
                        <a href="#"><u>Coaching Form</u></a>
                    </p>
                </div>
            </div>
                <div id="reviewerdetails" runat="server" class="row col-md-12 m-t-10">
                    <div class=" col-md-3">
                        <div class="form-group">
                            <label>
                                REVIEW YEAR</label>
                            <input id="reviewyear" runat="server" class="form-control" type="text" disabled="disabled" />
                        </div>
                    </div>
                    <div class=" col-md-3">
                        <div class="form-group">
                            <label>
                                REVIEW PERIOD START</label>
                            <input id="reviewstart" runat="server" class="form-control" type="text" disabled="disabled" />
                        </div>
                    </div>
                    <div class=" col-md-3">
                        <div class="form-group">
                            <label>
                                REVIEW PERIOD END</label>
                            <input id="reviewend" runat="server" class="form-control" type="text" disabled="disabled" />
                        </div>
                    </div>
                    <div class=" col-md-3">
                        <div class="form-group">
                            <label>
                                EMPLOYEE</label>
                            <input id="aname" runat="server" class="form-control" type="text" disabled="disabled" />
                        </div>
                    </div>
                    <div class=" col-md-3">
                        <div class="form-group">
                            <label>
                                JOB GRADE</label>
                            <input id="ajobgrade" runat="server" class="form-control" type="text" disabled="disabled" />
                        </div>
                    </div>
                    <div class=" col-md-3">
                        <div class="form-group">
                            <label>
                                JOB TITLE</label>
                            <input id="ajobtitle" runat="server" class="form-control" type="text" disabled="disabled" />
                        </div>
                    </div>

                    <div class=" col-md-3">
                        <div class="form-group">
                            <label>
                                LENGTH OF SERVICE</label>
                            <input id="alenofservice" runat="server" class="form-control" type="text" disabled="disabled" />
                        </div>
                    </div>
                    <div class=" col-md-3">
                        <div class="form-group">
                            <label>
                                TIME IN PRESENT POSITION</label>
                            <input id="apresentposition" runat="server" class="form-control" type="text" disabled="disabled" />
                        </div>
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
                           
                             <div class="row">

                        <div class="table-responsive">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel4" UpdateMode="Always">
                    <ContentTemplate>
                            <asp:GridView ID="gridskills" runat="server" OnSorting="SortRecords" AllowSorting="True"
                            BorderStyle="Solid" Font-Names="Verdana" AllowPaging="True" PageSize="100" DataKeyNames="id"
                            OnRowDataBound="OnRowDataBound" Width="100%" Height="50px" ToolTip="click row to select record"
                            Font-Size="12px" ShowHeaderWhenEmpty="True" EmptyDataText="No data to display"
                            AutoGenerateColumns="False" GridLines="Both" ForeColor="#666666" BorderWidth="1px"
                            BorderColor="#CCCCCC" CssClass="table table-condensed">
                                <RowStyle BackColor="White" />
                                <Columns>
                                    
                                  
                                  
                                      <asp:BoundField DataField="objectives" HeaderText="KPI Objectives" SortExpression="objectives"
                                        ItemStyle-VerticalAlign="Top" />
                                     <asp:BoundField DataField="successtarget" HeaderText="Success Target" SortExpression="successtarget"
                                        ItemStyle-VerticalAlign="Top" />
                                    <asp:TemplateField HeaderText="Reviewee Coaching Diary" ItemStyle-Font-Bold="true" SortExpression="name">
                                    <ItemTemplate>
                                        <a href="#" data-toggle="modal" data-target="#loginModal" onclick='Editcorevalues("<%# Eval("id") %>");'>
                                    <button id="btAdd" type="button" data-toggle="tooltip" data-original-title="Add Coaching Note"   class="glyphicon glyphicon-plus btn btn-default btn-sm" 
                        style="margin-left:10px;margin-right:10px;height: 35px;"></button> </a>
                                        <a href="#" data-toggle="modal" data-target="#commentModal" onclick='corevalues("<%# Eval("id") %>");' >
                                         <button id="Button8" type="button" data-toggle="tooltip" data-original-title="View Coaching Note" runat="server" class="glyphicon glyphicon-eye-open btn btn-default btn-sm" 
                        style="margin-left:10px;margin-right:10px;height: 35px;"></button> </a>
                                    </ItemTemplate>

                                </asp:TemplateField> 
                                     <asp:TemplateField HeaderText="Reviewee Coaching Diary" ItemStyle-Font-Bold="true" SortExpression="name">
                                    <ItemTemplate>
                                      
                                        <a href="#" data-toggle="modal" data-target="#commentModal" onclick='corevalues("<%# Eval("id") %>");' >
                                         <button id="Button8" type="button" data-toggle="tooltip" data-original-title="View Coaching Note" runat="server" class="glyphicon glyphicon-eye-open btn btn-default btn-sm" 
                        style="margin-left:10px;margin-right:10px;height: 35px;"></button> </a>
                                    </ItemTemplate>

                                </asp:TemplateField> 
                                    
                                     <asp:TemplateField HeaderText="Reviewer Coaching Diary" ItemStyle-Font-Bold="true" SortExpression="name">
                                    <ItemTemplate>
                             <a href="#" data-toggle="modal" data-target="#loginModal" onclick='Editcorevalues("<%# Eval("id") %>");'>
                                    <button id="btAdd" type="button" data-toggle="tooltip" data-original-title="Add Coaching Note"   runat="server" class="glyphicon glyphicon-plus btn btn-default btn-sm" 
                        style="margin-left:10px;margin-right:10px;height: 35px;"></button> </a>
                                        <a href="#" data-toggle="modal" data-target="#commentModal1" onclick='corevalues1("<%# Eval("id") %>");' >
                                         <button id="Button8" type="button" data-toggle="tooltip" data-original-title="View Coaching Note" runat="server" class="glyphicon glyphicon-eye-open btn btn-default btn-sm" 
                        style="margin-left:10px;margin-right:10px;height: 35px;"></button></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                    
                                     <asp:TemplateField HeaderText="Reviewer Coaching Diary" ItemStyle-Font-Bold="true" SortExpression="name">
                                    <ItemTemplate>
                            
                                        <a href="#" data-toggle="modal" data-target="#commentModal1" onclick='corevalues1("<%# Eval("id") %>");' >
                                         <button id="Button8" type="button" data-toggle="tooltip" data-original-title="View Coaching Note" runat="server" class="glyphicon glyphicon-eye-open btn btn-default btn-sm" 
                        style="margin-left:10px;margin-right:10px;height: 35px;"></button></a>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                    
                               
                                    
                                </Columns>
                                <HeaderStyle BackColor="White" ForeColor="#1BA691" HorizontalAlign="center" />
                            </asp:GridView>
                        </ContentTemplate>
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
               <%--     <div class="col-md-12 text-center">
                                                <button id="Button3" runat="server"  type="submit"
                                                style="width: 150px" onserverclick="btnContinue_Click" class="btn btn-primary btn-success">
                                                Feedback</button>
                                                <button id="Button4" runat="server"  type="submit"
                                                style="width: 150px" onserverclick="btnContinue_Click" class="btn btn-primary btn-info">
                                                Feedback</button>
                                            <button id="Button2" runat="server" onserverclick="btnClose_Click" type="submit"
                                                style="width: 150px" class="btn btn-primary btn-danger">
                                                Back</button>
                                 </div>--%>
                       
                           
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
                                <div id="gridsss" runat="server" class=" col-md-12">
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
                                         <%--   <div class="row">
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
                                                    --%>
                                                                       
         
                                            
                                        </div>
                                    </div>
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
                      </div>
                    </div>
                </div>
    </div>


    </form>
</body>
</html>
</asp:Content>