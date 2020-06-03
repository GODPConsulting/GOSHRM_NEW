<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="AppObjectiveUpdate.aspx.vb"
    Inherits="GOSHRM.AppObjectiveUpdate" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title></title>
        <script>
            function loading() {
                $('#tdate').datetimepicker({
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
			 
    </script>
         <script>
             var kpi_id = 0;
             function hide_msg() {
                 $('#msgbox1').css('display', 'none');
                 $('#msgbox2').css('display', 'none');
             }
             function coresubmit() {
                 //$('#loader').css('display', 'block');
                 $("#btnsubmit").attr("disabled", false); 
                 var checkBox = document.getElementById("self");
                 var kpimetric = {};
                 if ($('#Select1 :selected').text() == "--Others--") {
                     kpimetric.kpi = $('#selfinput').val();
                 } else {
                     kpimetric.kpi = $('#Select1 :selected').text();
                 }

                 kpimetric.cat = $('#cat').val();
                 kpimetric.obj = $('#obj').val();
                 kpimetric.suc = $('#suc').val();
                 kpimetric.key = $('#key').val();
                 if (this.kpi_id == 0) {
                     kpimetric.ID = 0;
                 } else {
                     kpimetric.ID = this.kpi_id;
                 }
                  if ($('#AppID').val() == "") {
                     kpimetric.AppID = <%=AppID %>;
                 } else {
                     kpimetric.AppID = $('#AppID').val();
                 }

                 kpimetric.tdate = $('#tdate').val();
                 kpimetric.aweight = $('#weight').val();
                 if ((kpimetric.kpi == "--Select KPI Metric--") || (kpimetric.weight == "") || (kpimetric.tdate == "") || (kpimetric.key == "") || (kpimetric.suc == "") || (kpimetric.obj == "")) {
                     $('#msgbox2').css('display', 'block');
                      $('#msgbox1').css('display', 'none');
                 } else {
                 $('#msgbox2').css('display', 'none');
                     //console.log("submit", kpimetric);
                     $.ajax({
                         url: "<%= Page.ResolveClientUrl("~/res_new/gos.asmx/addcorevalues") %>",                        
                         method: 'post',
                         data: '{emp: ' + JSON.stringify(kpimetric) + '}',
                         contentType: "application/json; charset=utf-8",
                         success: function (data) {
                             $('#msgbox1').css('display', 'block');
                             $('#obj').val("");
                             $('#suc').val("");
                             $('#Pobj').val("");
                             $('#key').val("");
                             $('#tdate').val("");
                             $('#weight').val("0");
                             document.getElementById('<%=btnSample.ClientID%>').click();
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
                 $('#cat').val(id);
                 $.ajax({
                     url: "<%= Page.ResolveClientUrl("~/res_new/gos.asmx/getcorevalues") %>",              
                     method: 'post',
                     dataType: 'json',
                     data: { pid: pid },
                     success: function (data) {
                         var selsubmod = $('#Select1');
                         selsubmod.prop('disabled', false);
                         selsubmod.empty();
                         selsubmod.append('<option value=-1>--Select KPI Metric--</option>');
                         $(data).each(function (index, prog) {
                             selsubmod.append('<option value=' + prog.ID + '>' + prog.Name + '</option>');                           
                             $('#obj').val("");
                             $('#suc').val("");
                             $('#Pobj').val("");
                             $('#key').val("");
                             $('#tdate').val("");
                             $('#weight').val("0");
                         });
                         selsubmod.append('<option value=1>--Others--</option>');
                         $("#btnsubmit").attr("disabled", false); 
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
             function Editcorevalues(lol) {
                 var pid = lol;
                 this.kpi_id = lol;
                 $("#btnsubmit").attr("disabled", false); 
                 $.ajax({
                     url: "<%= Page.ResolveClientUrl("~/res_new/gos.asmx/getcorevaluesbyID") %>",              
                     method: 'post',
                     dataType: 'json',
                     data: { pid: pid },
                     success: function (data) {
                         var selsubmod = $('#Select1');
                         selsubmod.prop('disabled', true);
                         selsubmod.empty();
                         $(data).each(function (index, prog) {
                             $('#obj').val(prog.obj);
                             $('#suc').val(prog.suc);
                             $('#Pobj').val(prog.obj);
                             $('#key').val(prog.key);
                             $('#tdate').datetimepicker({ defaultDate: prog.tdates, format: 'YYYY/MM/DD'})
                             $('#weight').val(prog.aweight);
                             $('#cat').val(prog.cat);
                             $('#AppID').val(prog.AppID);
                             $('#agree').val(prog.agree);
                             if ((prog.agree == "Discussed & Agreed") || (prog.EmpSetObj == "no")){
                                $("#btnsubmit").attr("disabled", true); 
                             }
                             selsubmod.append('<option value=' + prog.kpi + '>' + prog.kpi + '</option>');
                             //console.log("get", prog);
                         });
                     },
                     error: function (err) {
                         //alert(JSON.stringify(err));
                         $(err).each(function (index, prog) {
                         //console.log(prog.responseText);
                                 $('#msgbox2').css('display', 'block');
                                 $("#pmsg").text(prog.responseText);
                                 });
                     }
                 });
             }
//             function onchecked() {
//                 var checkBox = document.getElementById("self");
//                 var selfinput = document.getElementById("selfinput");
//                 kpi_input = $('#Select1 :selected').text();
//                 if (checkBox.checked == true) {
//                     selfinput.style.display = "block";
//                     kpi_input.style.display = "none";
//                 } else {
//                     selfinput.style.display = "none";
//                     kpi_input.style.display = "block";
//                 }
//             }
    </script>
    <script type="text/javascript">
        document.addEventListener('DOMContentLoaded', function () {
            document.querySelector('select[name="Select1"]').onchange = changeEventHandler;
        }, false);

        function changeEventHandler(event) {
            // You can use “this” to refer to the selected element.
            var kpi_input = $('#Select1 :selected').text();
            if (kpi_input == "--Others--") {
                selfinput.style.display = "block";
                kpi_input.style.display = "none";
            } else {
                selfinput.style.display = "none";
                kpi_input.style.display = "block";
            }

        }
    </script>

        <script type="text/javascript">
            function OnRowDblClick(sender, eventArgs) {
                var grid = $find("<%=gridCompetency.ClientID %>");
                var master = grid.get_masterTableView();
                editedRow = eventArgs.get_itemIndexHierarchical();
                master.fireCommand("DoubleClickEdit", editedRow);
            }
        </script>

        <script type="text/javascript">
            function Confirm() {
                var confirm_value = document.createElement("INPUT");
                confirm_value.type = "hidden";
                confirm_value.name = "confirm_value";
                if (confirm("Do you want to delete data?")) {
                    confirm_value.value = "Yes";
                } else {
                    confirm_value.value = "No";
                }
                document.forms[0].appendChild(confirm_value);
            }
        </script>
        <script type="text/javascript">
            function ConfirmPlan() {
                var confirmplan_value = document.createElement("INPUT");
                confirmplan_value.type = "hidden";
                confirmplan_value.name = "confirmplan_value";
                if (confirm("Do you want to sign Appraisal Objective as Agreed & Discussed?")) {
                    confirmplan_value.value = "Yes";
                } else {
                    confirmplan_value.value = "No";
                }
                document.forms[0].appendChild(confirmplan_value);
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
           <script type="text/javascript">
               function ConfirmUpdate() {
                   var confirmplan_value = document.createElement("INPUT");
                   confirmplan_value.type = "hidden";
                   confirmplan_value.name = "confirmplan_value";
                   if (confirm("Do you want to return Appraisal Objective?")) {
                       confirmplan_value.value = "Yes";
                   } else {
                       confirmplan_value.value = "No";
                   }
                   document.forms[0].appendChild(confirmplan_value);
               }
        </script>
        <script type="text/javascript">
            function ConfirmComplete() {
                var confirm_complete = document.createElement("INPUT");
                confirm_complete.type = "hidden";
                confirm_complete.name = "confirm_complete";
                if (confirm("Sign appraisal objective as COMPLETE and send notification for approval?")) {
                    confirm_complete.value = "Yes";
                } else {
                    confirm_complete.value = "No";
                }
                document.forms[0].appendChild(confirm_complete);
            }
        </script>
        <script type="text/javascript">

            function closeWin() {
                popup.close();   // Closes the new window
            }
   

        </script>
        <style type="text/css">
        .style1
        {
            color: #FFFFFF;
            font-family: Candara;
            font-weight: bold;
        }
        .lbl
        {
            font-family: Candara;
            font-size: medium;
        }
        .RadDropDownList { display:inline-block !important; 
                                               width: 443px !important; }
                            .RadDropDownList_Default{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.RadDropDownList{width:160px;line-height:1.3333em;text-align:left;display:inline-block;vertical-align:middle;white-space:nowrap;cursor:default;*zoom:1;*display:inline}.RadDropDownList_Default .rddlInner{border-radius:3px;border-color:#aaa;color:#333;background-color:#c1c1c1;background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radGradientButtonSprite.png');background-image:linear-gradient(#fafafa,#e6e6e6)}.RadDropDownList .rddlInner{vertical-align:top}.RadDropDownList .rddlInner{padding:2px 22px 2px 5px;border-width:1px;border-style:solid;display:block;position:relative;overflow:hidden}.RadDropDownList_Default .rddlDefaultMessage{color:#a5a5a5}.RadDropDownList .rddlDefaultMessage{font-style:italic}.RadDropDownList .rddlFakeInput{margin:0;padding:0;width:100%;min-height:16px;display:block;overflow:hidden}
                            .rddlFakeInput {
                                    height: 16px !important; 
                                    width: 80% !important;}.RadDropDownList_Default .rddlIcon{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radActionsSprite.png');background-position:-22px -20px}.RadDropDownList .rddlIcon{width:16px;height:100%;border:0;background-repeat:no-repeat;position:absolute;top:0;right:0}.rddlSlide{float:left;display:none;position:absolute;overflow:hidden;z-index:7000}.rddlPopup_Default{border-color:#a0a0a0;color:#333;background-color:#fff;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.rddlPopup{*zoom:1;border-width:1px;border-style:solid;text-align:left;position:relative;cursor:default;width:160px;*width:158px;box-sizing:border-box}
        #Text1
        {
            height: 22px;
        }
        
        </style>
    </head>
    <body>
        <form action="" id="form1">
        <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
        <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
        <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
            rel="Stylesheet" type="text/css" />
        <div class="container col-md-12">
            <div class="">
                <div class="">
                    <div id="divalert" runat="server" visible="false" class="alert alert-info">
                        <strong id="msgalert" runat="server"></strong>
                        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                            <span aria-hidden="true">&times;</span></button>
                        <asp:TextBox ID="txtEmpID" runat="server" Font-Size="1px" Visible="false"></asp:TextBox>
                        <asp:TextBox ID="txtdept" runat="server" Font-Size="1px" Visible="false"></asp:TextBox>
                        <asp:TextBox ID="txtlocation" runat="server" Font-Size="1px" Visible="false"></asp:TextBox>
                        <asp:TextBox runat="server" Font-Size="1px" Visible="false" ID="txtManagerMail"></asp:TextBox>
                        <asp:TextBox runat="server" Font-Size="1px" Visible="false" ID="txtemailreviewer2"></asp:TextBox>
                        <asp:TextBox runat="server" Font-Size="1px" Visible="false" ID="txtemailreviewer"></asp:TextBox>
                        <asp:Label ID="lblTotal" runat="server" Font-Size="1px" Visible="false"></asp:Label>
                        <%--objective detail--%>
                        <asp:TextBox ID="txtobjid" runat="server" Font-Size="1px" Height="1px" Width="1px"
                            Visible="false"></asp:TextBox>
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
                <h4 class="modal-title"><b id="modal_title" runat="server">PERFORMANCE METRIC OBJECTIVES</b></h4>
            </div>
            <div class="modal-body">
                <form>
                    <div class="form-group">
                  <label>Choose Performance Metric</label>
                  <select id="Select1" name="Select1" class="form-control">
                    <option>--Select KPI Metric--</option>
                  </select>
                  <input onclick="hide_msg()" id="selfinput" placeholder="Type KPI Metric" style="display:none; margin-top:10px;" class="form-control" type="text" />
                  </div>
                    
                    <div class="form-group">
                    <div class="col-md-6">
                        <label>Objective</label>
                        <textarea id="obj" onclick="hide_msg()" class="form-control" type="text" rows="2"cols="1"></textarea>
                        <textarea id="cat" onclick="hide_msg()" class="form-control" style="display:none;" type="text"></textarea>
                        <textarea id="AppID" onclick="hide_msg()" class="form-control" style="display:none;" type="text"></textarea>
                        <textarea id="agree" onclick="hide_msg()" class="form-control" style="display:none;" type="text"></textarea>
                    </div>
                    <div class="col-md-6">
                        <label>Success Measure</label>
                        <textarea id="suc" onclick="hide_msg()" class="form-control" type="text" rows="2"
                                            cols="1"></textarea>
                    </div>
                    </div>
                     <div class="form-group">
                        <label>Key Actions</label>
                        <textarea id="key" onclick="hide_msg()" class="form-control" type="text" rows="3"
                                            cols="1"></textarea>
                    </div>
                    
                    <div class="form-group">
                        <div class="col-md-6">
                            <label>Target Date</label>
                            <input id="tdate" onclick="hide_msg()" onfocus="loading()" class="form-control floating" type="text" />
                        </div>
                         <div class="col-md-6">
                            <label>WEIGHT (%)</label>
                                           <input onclick="hide_msg()" id="weight" value="0" class="form-control" type="text" />
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
                <button id="btnsubmit" type="button" onclick="coresubmit()" class="btn btn-success m-t-10">Save</button>
                <button type="button" onclick="hide_msg()" class="btn btn-danger m-t-10" data-dismiss="modal">Close</button>
            </div>
        </div>
    </div>
</div>
                <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                    <asp:View ID="View1" runat="server">
                        <div class="row">
                           
                        </div>
                         <div class="row">
                        </div>
                        <div id="ob" runat="server" class="row">
                            <div class="">
                                <div class="panel panel-success">
                                 <div class="panel-heading">
                                  <h5 id="page_title" runat="server" style="color: #1BA691" class="page-title">
                                        Performance Objective:</h5>
                                    <asp:Label ID="lblid" runat="server" Font-Size="1px" Visible="False">0</asp:Label>
                                    <asp:Label ID="lblapproval" runat="server" Font-Size="1px" Visible="False"></asp:Label>
                                 </div>
                                    <div class="panel-body">
                                      <div class="row col-md-12 m-t-20 form-group">                                                                                      
                                               
                                                <asp:LinkButton ID="btnAgreed" style="margin-left:10px;font-size:20px;" data-toggle="tooltip" data-original-title="Discussed and Agreed" Height="35px" Width="40px" runat="server" Visible="False" CssClass="btn btn-default btn-sm pull-right" OnClientClick="ConfirmPlan()">
                                                    <span style="margin-top:0px" aria-hidden="true" class="glyphicon glyphicon-thumbs-up"></span>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="Update" style="margin-left:10px;font-size:20px;" data-toggle="tooltip" data-original-title="Revoke" Height="35px" Width="40px" runat="server" Visible="False" CssClass="btn btn-default btn-sm pull-right" OnClientClick="ConfirmUpdate()">
                                                    <span style="margin-top:0px" aria-hidden="true" class="glyphicon glyphicon-remove"></span>
                                                </asp:LinkButton>
                                                 <asp:LinkButton ID="btnDisagree" style="margin-left:10px; font-size:20px;" data-toggle="tooltip" data-original-title="Disagreed" Height="35px" Width="40px" runat="server" Visible="False" CssClass="btn btn-default btn-sm pull-right" OnClientClick="ConfirmDisagree()">
                                                    <span style="margin-top:0px" aria-hidden="true" class="glyphicon glyphicon-thumbs-down"></span>
                                                </asp:LinkButton>                                           
                                            <button id="btnback" runat="server" onserverclick="btnClose_Click" type="submit" data-toggle="tooltip" data-original-title="Back"
                                                style="margin-left:10px;" class="btn btn-default glyphicon glyphicon-backward pull-right"></button>
                                                <asp:LinkButton style="margin-left:10px;" ID="btnComplete" data-toggle="tooltip" data-original-title="Complete and Send to Line Manager" Height="35px" Width="35px" runat="server" Visible="False" CssClass="btn btn-default btn-sm pull-right" OnClientClick="ConfirmComplete()">
                                                    <span style="margin-top:5px" aria-hidden="true" class="fa fa-send-o"></span>
                                                </asp:LinkButton>   
                                                <button id="btnupdate" runat="server" onserverclick="btnAdd_Click" type="submit" data-toggle="tooltip" data-original-title="Start Objective"
                                                style="height:35px; Width:35px; margin-left:10px;" class="glyphicon glyphicon-play btn btn-default btn-sm pull-right"></button>
                                                <button id="mgrSavebtn" runat="server" onserverclick="btnAdd_Click" type="submit" data-toggle="tooltip" data-original-title="Save Comment"
                                                style="height:35px;display:none;Width:40px;margin-left:10px;font-size:20px;" class="glyphicon glyphicon-floppy-save btn btn-default btn-sm pull-right"></button>
                                                <button id="continueBTN" runat="server" onserverclick="btnAdd_Click1" type="submit" data-toggle="tooltip" data-original-title="Continue Objective"
                                                style="display:none;margin-left:10px;Width:40px;" class="glyphicon glyphicon-play btn btn-default pull-right"></button>
    
                                        </div>
                                        <div id="Ryear" runat="server" class="row">
                                            <div class=" col-md-6">
                                                <div class="form-group">
                                                    <label>
                                                        REVIEW YEAR*</label>
                                                    <telerik:radcombobox id="cboDevPlan" runat="server" skin="Bootstrap" defaultmessage="--Select--"
                                                        autopostback="True" width="100%" forecolor="#666666">
                                                    </telerik:radcombobox>
                                                </div>
                                            </div>
                                            <div class=" col-md-6">
                                                <div class="form-group">
                                                    <label>
                                                        REVIEW PERIOD*</label>
                                                    <telerik:radcombobox id="cboStartReview" runat="server" skin="Bootstrap" forecolor="#666666"
                                                        width="100%" rendermode="Lightweight" autopostback="True">
                                                    </telerik:radcombobox>
                                                </div>
                                            </div>
                                           
                                        </div>
                                        <div class="row">
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
                                             
                                             <div style="display:none" class=" col-md-4">
                                                <div class="form-group">
                                                    <label>
                                                        COMMENT</label>
                                                    <textarea id="aempcomment" runat="server" class="form-control" rows="1"></textarea>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            
                                        </div>
                                        <div class="row">
                                            <div class=" col-md-6">
                                                <div class="panel panel-success">
                                                    <div class="panel-heading">
                                                        <b>LINE MANAGER</b>
                                                    </div>
                                                    <div class="panel-body">
                                                        <label>
                                                                NAME</label>
                                                        <input id="alinemanager" runat="server" class="form-control" type="text" disabled="disabled" />
                                                        <div style="margin-bottom:32px;" class="form-group">
                                                            <label>
                                                                COMMENT</label>
                                                            <textarea id="amgrcomment" runat="server" class="form-control" rows="2"></textarea>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class=" col-md-6">
                                                <div class="panel panel-success">
                                                    <div class="panel-heading">
                                                        <b>PERFORMANCE REVIEWER</b>
                                                    </div>
                                                    <div class="panel-body">
                                                        <div class=" col-md-12">
                                                            <div class="form-group">
                                                                <label>
                                                                    1. FIRST REVIEWER</label>
                                                                <input id="areviewer1" runat="server" class="form-control" type="text" disabled="disabled" />
                                                            </div>
                                                        </div>
                                                        <div class=" col-md-12">
                                                            <div class="form-group">
                                                                <label>
                                                                    2. SECOND REVIEWER</label>
                                                                <input id="areviewer2" runat="server" class="form-control" type="text" disabled="disabled" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                      
                                        <div class="row">
                                            <div class=" col-md-6">
                                                <div class="form-group">
                                                    <label id="asign" runat="server" style="display:none" visible="false">
                                                        Discussed and Agreed By</label>
                                                </div>
                                            </div>
                                            <div class=" col-md-6">
                                                <div class="form-group">
                                                    <button id="btnlink" runat="server" onserverclick="lnkCoachObj_Click" type="submit"
                                                        class="btn btn-link" title="View your Line Manager's Performance Objective">
                                                        View Line Manager&#39;s Objectives</button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="p" runat="server" style="display:none;" class="row">
                            <div class="panel panel-success">
                                <div class="panel-heading">
                                    <b>PERFORMANCE OBJECTIVES</b></div>
                                <div class="panel-body">
                                    <div class="">
                                        <div class="form-group">
                                            <label id="lbltotalweight" runat="server">
                                            </label>
                                        </div>
                                    </div>
                                        <div class="row" id="kpigroup" runat="server">
                                                            
                                                        </div>
                                    <div class="row m-t-20 text-right">
                                        <button id="btnadd" runat="server" onserverclick="btnAddGrid_Click" type="submit"
                                            style="width: 150px; display:none;" class="btn btn-default">
                                            Add Objective</button>
                                        <button id="Button1" data-toggle="tooltip" data-original-title="Link to Development Plan" runat="server" onserverclick="btnDev_Click" type="submit"
                                            style="height:35px" class="btn btn-default fa fa-id-card"></button>
<%--                                        <asp:Button ID="btnDeleteGrid" runat="server" Text="Delete Objective" ForeColor="White"
                                            Width="150px" Height="35px" BorderStyle="None" Font-Size="14px" Visible="False"
                                            OnClientClick="Confirm()" ToolTip="delete" CssClass="btn btn-primary btn-danger" />--%>
                                        <asp:LinkButton ID="btnDeleteGrid" data-toggle="tooltip" data-original-title="Delete Objective" Height="35px" Width="35px" runat="server" CssClass="btn btn-default btn-sm" OnClientClick="Confirm()">
                                            <span style="margin-top:5px" aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                                        </asp:LinkButton>
                                        <button id="btnrefresh" data-toggle="tooltip" data-original-title="Refresh" runat="server" onserverclick="btnRefresh_Click" type="submit"
                                            style="height:35px" class="btn btn-default glyphicon glyphicon-refresh"></button>
                                        <asp:LinkButton ID="SaveButton" data-toggle="tooltip" data-original-title="Save" Height="35px" Width="35px" runat="server" CssClass="btn btn-default btn-sm">
                                            <span style="margin-top:5px" aria-hidden="true" class="glyphicon glyphicon-floppy-save"></span>
                                        </asp:LinkButton>
                                            <asp:Button runat="server" ID="btnSample" Text="" style="display:none;" OnClick="btnRefresh_Click" />
                                         <button id="Btngridback" runat="server" data-toggle="tooltip" data-original-title="Go back" onserverclick="btnAdd_Click2" type="submit"
                                            style="height:35px" class="btn btn-default glyphicon glyphicon-backward"></button>
                                    </div>
                         </div>
                         <div class="row">
                            <div class="table-responsive">
                                <telerik:radgrid rendermode="Lightweight" id="gridCompetency" runat="server" pagesize="20"
                                    allowsorting="True" Skin="Sunset" allowmultirowselection="True" allowpaging="True" showgrouppanel="True"
                                    autogeneratecolumns="False" borderwidth="1px" bordercolor="#CCCCCC" grouppanelposition="Top"
                                    resolvedrendermode="Classic" datakeynames="ID" gridlines="Both" enablegroupsexpandall="True"
                                    showfooter="True" showstatusbar="True" width="100%" font-names="Verdana" 
                                    font-size="11px">
                                    <pagerstyle mode="NextPrevNumericAndAdvanced"></pagerstyle>
                                    <mastertableview enablegroupsexpandall="True" width="100%">
                                    <groupbyexpressions>
                                    <telerik:GridGroupByExpression>
                                                <selectfields>
                                                    <telerik:GridGroupByField FieldAlias="" FieldName="KPIType" 
                                        HeaderText="KPI Category">
                                                    </telerik:GridGroupByField>
                                                </selectfields>
                                                <groupbyfields>
                                                    <telerik:GridGroupByField FieldName="KPIType" 
                                        SortOrder="Ascending"></telerik:GridGroupByField>
                                                </groupbyfields>
                                            </telerik:GridGroupByExpression>
                                    </groupbyexpressions>
                                    <Columns>
                                        <telerik:GridTemplateColumn HeaderStyle-Width="30%" 
                                            ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top" 
                                            ItemStyle-Width="30%" UniqueName="CheckBoxTemplateColumn">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="False" 
                                                        ToolTip="Check to delete" />
                                                </ItemTemplate>
                                        
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="headerChkbox" runat="server" AutoPostBack="False" 
                                                        OnCheckedChanged="ToggleSelectedState" />
                                                </HeaderTemplate>
                                        
                                                <HeaderStyle Width="30px"></HeaderStyle>                                        
                                                <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>                                        
                                            </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="ID" HeaderButtonType="TextButton" 
                                            HeaderStyle-Width="1px" HeaderText="ID" ItemStyle-VerticalAlign="Top" 
                                            ItemStyle-Width="1px" SortExpression="ID">
                                                <HeaderStyle Width="1px"></HeaderStyle>                                        
                                                <ItemStyle Width="1px"></ItemStyle>                                        
                                            </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn DataField="KPIObjectives" HeaderStyle-Width="150px" 
                                            HeaderText="KPI Type" ItemStyle-VerticalAlign="Top" ItemStyle-Width="150px" 
                                            UniqueName="strName">
                                                <%--<ItemTemplate>
                                                    <asp:LinkButton ID="lnkDownload" runat="server" 
                                                        CommandArgument='<%# Eval("id") %>' OnClick="LinkDownLoad" 
                                                        Text='<%# Eval("KPIObjectives")%>'></asp:LinkButton>
                                                </ItemTemplate>--%>
                                                <ItemTemplate>
                                                    <b><a href="#" data-toggle="modal" data-target="#loginModal" onclick='Editcorevalues("<%# Eval("id") %>");'>
                                                        <%# Eval("KPIObjectives")%></a></b>
                                                </ItemTemplate>
                                                <ItemStyle Width="150px"></ItemStyle>
                                        
                                            </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="objectives" HeaderButtonType="TextButton" 
                                            HeaderStyle-Width="300px" HeaderText="Objectives" ItemStyle-VerticalAlign="Top" 
                                            ItemStyle-Width="300px" SortExpression="objectives" UniqueName="objectives">
                                                <HeaderStyle Width="200px"></HeaderStyle>                                        
                                                <ItemStyle Width="200px"></ItemStyle>                                        
                                            </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="successtarget" 
                                            HeaderButtonType="TextButton" HeaderStyle-Width="300px" 
                                            HeaderText="Success Target" ItemStyle-VerticalAlign="Top" 
                                            ItemStyle-Width="300px" SortExpression="successtarget" 
                                            UniqueName="successtarget">
                                                <HeaderStyle Width="200px"></HeaderStyle>                                        
                                                <ItemStyle Width="200px"></ItemStyle>                                        
                                            </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="keyactions" HeaderButtonType="TextButton" 
                                            HeaderStyle-Width="300px" HeaderText="Key Action" ItemStyle-VerticalAlign="Top" 
                                            ItemStyle-Width="300px" SortExpression="keyactions" UniqueName="keyactions">
                                                <HeaderStyle Width="200px"></HeaderStyle>
                                        
                                                <ItemStyle Width="200px"></ItemStyle>
                                        
                                            </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="targetdate" HeaderButtonType="TextButton" 
                                            HeaderStyle-Width="150px" HeaderText="Target Date" 
                                            ItemStyle-VerticalAlign="Top" ItemStyle-Width="150px" 
                                            SortExpression="targetdate">
                                                <HeaderStyle Width="100px"></HeaderStyle>
                                        
                                                <ItemStyle Width="100px"></ItemStyle>
                                        
                                            </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="customweight" HeaderButtonType="TextButton" 
                                            HeaderStyle-Width="80px" HeaderText="Weight(%)" 
                                            ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top" 
                                            ItemStyle-Width="80px" SortExpression="customweight">
                                                <HeaderStyle Width="80px"></HeaderStyle>                                        
                                                <ItemStyle Width="80px"></ItemStyle>                                        
                                            </telerik:GridBoundColumn>
                                    </Columns>
                                    </mastertableview>
                                    <clientsettings allowcolumnsreorder="True" allowdragtogroup="True" reordercolumnsonclient="True">
                                    <selecting allowrowselect="True">
                                    </selecting>
                                    <resizing allowcolumnresize="True" allowrowresize="True" 
                                        enablerealtimeresize="True" resizegridoncolumnresize="False">
                                    </resizing>
                                    </clientsettings>
                                    <groupingsettings showungroupbutton="true"></groupingsettings>
                                    <filtermenu rendermode="Lightweight">
                                    </filtermenu>
                                    <headercontextmenu rendermode="Lightweight">
                                    </headercontextmenu>
                                </telerik:radgrid>
                                <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
                                <script type="text/javascript">
                                    function openWindow(code) {
                                        window.open("MyAppObjectives.aspx?id=" + code, "open_window", "width=500,height=400");
                                    }
                                </script>
                            </div>
                            </div></div>
                        </div>
                    </asp:View>
                    <asp:View ID="AppObjectives" runat="server">
                        <div class="row">
                <div class="col-md-10">
                    <div class="panel panel-success">
                        <div class="panel-body">

                            <div class="row">
                                <div class="col-xs-8">
                                    <h5 id="pagetitle1" runat="server" class="page-title" style="color: #1BA691">
                                        Appraisal Objectives</h5>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            PERFORMANCE METRIC CATEGORY</label>
                                        <telerik:radcombobox id="cboKPICategory" runat="server" width="100%" autopostback="True"
                                            forecolor="#666666" rendermode="Lightweight" skin="Bootstrap" emptymessage="--Select--"
                                            filter="Contains">
                                        </telerik:radcombobox>
                                    </div>
                                </div>
                                <asp:UpdatePanel runat="server" ID="UpdatePanel" UpdateMode="Always">
                                    <ContentTemplate>
                                        <div id="divkpitype" runat="server" class=" col-md-6">
                                            <div class="form-group">
                                                <label>
                                                    PERFORMANCE METRIC</label>
                                                <telerik:radcombobox id="cboKPI" runat="server" width="100%" forecolor="#666666"
                                                    rendermode="Lightweight" skin="Bootstrap">
                                                </telerik:radcombobox>
                                                <asp:Label ID="lblmodel" runat="server" Font-Size="1px" Height="1px" Width="1px"
                                                    Visible="false"></asp:Label>
                                                <asp:Label ID="lblWeightView" runat="server" Font-Names="Verdana" Text="" Font-Size="1px"
                                                    Visible="False"></asp:Label>
                                            </div>
                                        </div>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="cboKPICategory" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                            <div class="row">
                                <div id="divobjective" runat="server" class=" col-md-12">
                                    <div class="form-group">
                                        <label>
                                            OBJECTIVES</label>
                                        <textarea id="a_objective" runat="server" class="form-control" type="text" rows="4"
                                            cols="1"></textarea>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <label>
                                            SUCCESS MEASURES</label>
                                        <textarea id="a_measure" runat="server" class="form-control" type="text" rows="4"
                                            cols="1"></textarea>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <label>
                                            KEY ACTIONS</label>
                                        <textarea id="a_keyactions" runat="server" class="form-control" type="text" rows="4"
                                            cols="1"></textarea>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            TARGET DATE</label>
                                        <telerik:raddatepicker runat="server" mindate="1900-01-01" forecolor="#666666" culture="en-US"
                                            rendermode="Lightweight" width="100%" resolvedrendermode="Classic" id="datDate"
                                            skin="Bootstrap">
                                            <calendar enableweekends="True" fastnavigationnexttext="&amp;lt;&amp;lt;" rendermode="Lightweight"
                                                skin="Bootstrap" usecolumnheadersasselectors="False" userowheadersasselectors="False">
                                </calendar>
                                            <dateinput dateformat="dd/MM/yyyy" displaydateformat="dd/MM/yyyy" labelwidth="40%"
                                                rendermode="Lightweight">
                                <emptymessagestyle resize="None">
                                </emptymessagestyle>
                                <readonlystyle resize="None">
                                </readonlystyle>
                                <focusedstyle resize="None">
                                </focusedstyle>
                                <disabledstyle resize="None">
                                </disabledstyle>
                                <invalidstyle resize="None">
                                </invalidstyle>
                                <hoveredstyle resize="None">
                                </hoveredstyle>
                                <enabledstyle resize="None">
                                </enabledstyle>
                                </dateinput>
                                            <datepopupbutton cssclass="" imageurl="" hoverimageurl=""></datepopupbutton>
                                        </telerik:raddatepicker>
                                    </div>
                                </div>
                                <div id="divweight" runat="server" class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            WEIGHT (%)</label>
                                        <input id="aweight" runat="server" class="form-control" type="text" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12 m-t-20">
                                <button id="btnsaveobj" runat="server" onserverclick="btnAddObj_Click" type="submit"
                                    style="width: 150px" class="btn btn-primary btn-success">
                                    Save & Update</button>
                                <button id="btncloseobj" runat="server" onserverclick="btnCancelObj_Click" type="submit"
                                    style="width: 150px" class="btn btn-primary btn-danger">
                                    << Back</button>
                            </div>
                               </div>
                    </div>
                </div>
        </div>

                    </asp:View>
                </asp:MultiView>
            </div>
        </div>
        </form>
    </body>
    </html>
</asp:Content>
