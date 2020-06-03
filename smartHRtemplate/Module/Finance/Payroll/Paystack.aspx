<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="Paystack.aspx.vb"
    Inherits="GOSHRM.Paystack" EnableEventValidation="false" Debug="true" %>

<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%@ register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
    <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
    <html xmlns="http://www.w3.org/1999/xhtml">

    <script type="text/javascript">             
             
             function getRecipient() {
                 //$('#loader').css('display', 'block');
                 var pid = <%=txtid.Text %>;
                     var recipient = {};
                     $.ajax({
                         url: "<%= Page.ResolveClientUrl("~/res_new/gos.asmx/getRecipient") %>",                        
                         method: 'post',
                         dataType: 'json',
                         data: { pid: pid },
                         success: function (data) {
                            $(data).each(function (index, prog) {
                             recipient.name = prog.name;
                             recipient.type = prog.type;
                             recipient.description = prog.description;
                             recipient.account_number = prog.account_number;
                             recipient.bank_code = prog.bank_code;
                             recipient.currency = prog.currency;
                             createRecipient(recipient);                                            
                         });       
                         },
                         error: function (err) {
                                 $(err).each(function (index, prog) {
                                 $("#diva").css("display", "block");
                                 $("#msga").text(prog.responseJSON.message);
                                 });
                         }
                     });
                 }
                 function createRecipient(list){
                 $.ajax({
                         url: "https://api.paystack.co/transferrecipient",                        
                         method: 'post',
                         beforeSend : function(xhr) {
                            xhr.setRequestHeader("Authorization", "<%=PKeys.Text %>");
                         },
                         contentType: "application/json",
                         data:  JSON.stringify(list),
                         dataType: 'json',
                         success: function (msg) {
                         updateRecipient(msg.data);
                         },
                         error: function (err) {
                                 $(err).each(function (index, prog) {
                                 $("#diva").css("display", "block");
                                 $("#msga").text(prog.responseJSON.message);
                                 });
                         }
                     });

                     }
                 function getRecipientFromPaystack(){
                 $.ajax({
                         url: "https://api.paystack.co/transferrecipient",                        
                         method: 'get',
                         beforeSend : function(xhr) {
                            xhr.setRequestHeader("Authorization", "<%=PKeys.Text %>");
                         },
                         contentType: "application/json",
                         dataType: 'json',
                         success: function (data) {
                         //console.log(data);
                         },
                         error: function (err) {
                               //console.log(JSON.stringify(err));
                             //$('#pmsg').text("The Programme Name '" + programme.Name + "' already exist");
                         }
                     });
                     }

                   function getBalanceFromPaystack(){
                 $.ajax({
                         url: "https://api.paystack.co/balance",                        
                         method: 'get',
                         beforeSend : function(xhr) {
                            xhr.setRequestHeader("Authorization", "<%=PKeys.Text %>");
                         },
                         contentType: "application/json",
                         dataType: 'json',
                         success: function (mm) {
                            $(mm.data).each(function (index, prog) {
                                var bal = (prog.balance)/100;
                                var bal = bal.toLocaleString()
                                var curr = prog.currency;
                                $('#fig').text(curr + " " + bal +".00");
                            });
                         },
                         error: function (err) {
                              $(err).each(function (index, prog) {
                                 $("#diva").css("display", "block");
                                 $("#msga").text(prog.responseJSON.message);
                                 });
                             //$('#pmsg').text("The Programme Name '" + programme.Name + "' already exist");
                         }
                     });
                     }

                  function payRecipientWithPaystack(list){                                 
                         $.ajax({
                         url: "https://api.paystack.co/transfer/bulk",                        
                         method: 'post',
                         beforeSend : function(xhr) {
                            xhr.setRequestHeader("Authorization", "<%=PKeys.Text %>");
                         },
                         contentType: "application/json",
                         data: JSON.stringify(list),
                         dataType: 'json',
                         success: function (msg) {
                            console.log(msg.status); 
                            updatePaymentStatus();
                           document.getElementById('<%=btnSample.ClientID%>').click();                     
                         },
                         error: function (err) {
                         var error = JSON.stringify(err)
                                 $(err).each(function (index, prog) {
                                 $("#diva").css("display", "block");
                                 $("#msga").text(prog.responseJSON.message);
                                 });
                         }
                     });
                     }
                     function updateRecipient(users){
                     var user = {};
                     user.description = users.description;
                     user.recipient_code = users.recipient_code;
                     //console.log((user));
                      $.ajax({
                         url: "<%= Page.ResolveClientUrl("~/res_new/gos.asmx/updateRecipient") %>",                        
                         method: 'post',
                         data: '{emp: ' + JSON.stringify(user) + '}',
                         contentType: "application/json; charset=utf-8",
                         success: function (data) {
                            //console.log(data);
                         },
                         error: function (err) {
                                 $(err).each(function (index, prog) {
                                 $("#diva").css("display", "block");
                                 $("#msga").text(prog.responseJSON.message);
                                 });
                         }
                     });
                     }

                     function payRecipient(){
                     var payment = "<%=paystatus %>";
                     if (payment == "True"){
                       $("#diva").css("display", "block");
                       $("#msga").text("Payment has already been made");
                     }else{
                         var pid = <%=txtid.Text %>;                    
                          $.ajax({
                             url: "<%= Page.ResolveClientUrl("~/res_new/gos.asmx/payRecipient") %>",                        
                             method: 'post',
                             dataType: 'json',
                             data: { pid: pid },
                             success: function (data) {
                                //console.log(JSON.stringify(data));                                                   
                                payRecipientWithPaystack(data);
                             },
                             error: function (err) {
                                 $(err).each(function (index, prog) {
                                 $("#diva").css("display", "block");
                                 $("#msga").text(prog.responseJSON.message);
                                 });
                             }
                         });
                     }
                    
                     }                  
                     function updatePaymentStatus(){
                     var pid = <%=txtid.Text %>;
                      $.ajax({
                         url: "<%= Page.ResolveClientUrl("~/res_new/gos.asmx/updatePaymentStatus") %>",                        
                         method: 'post',
                         dataType: 'json',
                         data: { pid: pid },
                         success: function (data) {                        
                            //console.log("Am successful",data);
                            document.getElementById('<%=btnSample.ClientID%>').click();
                         },
                         error: function (err) {
                                 $(err).each(function (index, prog) {
                                 $("#diva").css("display", "block");
                                 $("#msga").text(prog.responseJSON.message);
                                 });
                         }
                     });
                     }
         window.onload = getRecipient;
         window.onload = getBalanceFromPaystack;
    </script>
    <script type="text/javascript" language="javascript">
        //    Grid View Check box
        function CheckAllEmp(Checkbox) {
            var GridVwHeaderChckbox = document.getElementById("<%=GridVwHeaderChckbox.ClientID %>");
            for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }
    </script>
    <title>Employee Payroll</title>
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
        function ConfirmPayslip() {
            var confirm_payslip = document.createElement("INPUT");
            confirm_payslip.type = "hidden";
            confirm_payslip.name = "confirm_payslip";
            if (confirm("Send Payslip to Employees?")) {
                confirm_payslip.value = "Yes";
            } else {
                confirm_payslip.value = "No";
            }
            document.forms[0].appendChild(confirm_payslip);
        }
    </script>

     <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
         <script type="text/javascript">
             function ShowProgress() {
                 setTimeout(function () {
                     var modal = $('<div />');
                     modal.addClass("modal");
                     $('body').append(modal);
                     var loading = $(".loading");
                     loading.show();
                     var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                     var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                     loading.css({ top: top, left: left });
                 }, 200);
             }
             $('form').live("submit", function () {
                 ShowProgress();
             });
    </script>
     <style type="text/css">
        .modal
        {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }
           .loading
        {
            font-family: Arial;
            font-size: 10pt;
            border: 5px solid #67CFF5;
            width: 200px;
            height: 100px;
            display: none;
            position: fixed;
            background-color: White;
            z-index: 999;
        }
    </style>

    <body>
        <form id="form1">
        <div class="row">
            <div class="col-xs-8 col-md-12">
                <div id="diva" class="alert alert-info" style="display:none;">
                    <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <strong
                        id="msga"></strong>
                </div>
                <div id="divalert" runat="server" visible="false" class="alert alert-info">
                    <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <strong
                        id="msgalert" runat="server">Danger!</strong>
                        <asp:TextBox ID="txtid" runat="server" Height="1px" Width="1px" Visible="false"></asp:TextBox>
                        <asp:TextBox ID="Pstatus" runat="server" Height="1px" Width="1px" Visible="false"></asp:TextBox>
                        <asp:TextBox ID="PKeys" runat="server" Height="1px" Width="1px" Visible="false"></asp:TextBox>
                </div>
            </div>
        </div>
         <div class="panel panel-success">
                <div class="panel-heading">
                    <h5><b id="pagetitle" runat="server"></b></h5>
                </div>
             <div class="panel-body">
        <div style="display:none;" class="row">
            <div class="col-xs-8 col-md-12">
                <div id="divapproval" runat="server" class="w3-panel w3-pale-red w3-bottombar w3-border-red w3-border">
                    <p id="lbapproval" runat="server"></p>
                </div>
            </div>
        </div>
         <div class="row">
            <div class="search-box-wrapper col-sm-6 col-md-3 col-xs-12 form-group pull-right">                        
                    <input id="search" style="width:100%" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                    <button onserverclick="btnFind_Click" id="btsearch" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                </div>
                 <div class="col-md-3 col-sm-6 col-xs-12 form-group pull-right"> 
                 <button id="btback" type="button" data-toggle="tooltip" data-original-title="Back" runat="server" class="fa fa-backward btn btn-default btn-sm" onserverclick="btnCancel_Click"
                        style="height: 35px; width:35px"></button>
                       <button id="btnrefresh" data-toggle="tooltip" data-original-title="Refresh" runat="server" onserverclick="btnRefresh_Click" type="submit"
                                            style="height:35px" class="btn btn-default glyphicon glyphicon-refresh"></button>
                   <asp:LinkButton ID="btsendslips" Visible="false" data-toggle="tooltip" data-original-title="Send payslips to employees" Height="35px" CssClass="btn btn-default btn-sm" runat="server" OnClick="btnDelete0_Click"  OnClientClick="ConfirmPayslip()">
                    <span style="margin-top:5px" aria-hidden="true" class="glyphicon glyphicon-send"></span>
                    </asp:LinkButton>
                    <asp:Button runat="server" ID="btnSample" Text="" style="display:none;" OnClick="btnRefresh_Click" />                             
                    <%--<button id="btnUploadFile" type="button" runat="server" class="fa fa-upload btn btn-default btn-sm"
                        onserverclick="btnUpload_Click" data-toggle="tooltip" data-original-title="CSV File: location,address,country,state/province,city, contactno" style="margin-right:10px;margin-left:10px;height:35px"></button>--%>
                        <button id="btExport" data-toggle="tooltip" data-original-title="Export" type="button" runat="server" class="fa fa-download btn btn-default btn-sm" onserverclick="btnExport_Click"
                    style="height: 35px"></button>
                        <asp:LinkButton ID="btDelete" data-toggle="tooltip" data-original-title="Delete" Height="35px" runat="server" CssClass="btn btn-default btn-sm" OnClick="Delete" OnClientClick="Confirm()">
                            <span style="margin-top:5px" aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                        </asp:LinkButton>
                         <button id="btAdd" type="button" data-toggle="tooltip" data-original-title="Add Recipient" runat="server" class="glyphicon glyphicon-plus btn btn-default btn-sm"
                        style="height: 35px; display:none;"></button>                          
                </div>
                <div class="col-sm-6 col-md-4 col-xs-6 pull-right">
                    <label style="font-size:16px; margin-top:6px; color:Green; font-weight:bold;">Paystack Balance: <span id="fig"></span></label>
                </div>
                <div class="col-sm-3 col-md-2 col-xs-6 pull-right">
                    <button id="btprocess" type="button" runat="server" onclick="payRecipient()" class="btn btn-success" title="Pay with paystack"
                        style="height: 35px; width: 100%">
                        Pay with Paystack</button>
                </div>
        </div>

        <div class="row">
            <div class="table-responsive">
                        <asp:GridView ID="GridVwHeaderChckbox" runat="server" OnSorting="SortRecords" AllowSorting="True"
                            BorderStyle="Solid" Font-Names="Verdana" AllowPaging="True" PageSize="100" DataKeyNames="EmpID"
                            Width="100%" Height="50px" ToolTip="click row to select record"
                            Font-Size="12px" ShowHeaderWhenEmpty="True" EmptyDataText="No data to display"
                            AutoGenerateColumns="False" GridLines="Both" ForeColor="#666666" BorderWidth="1px"
                            BorderColor="#CCCCCC" CssClass="table table-condensed">
                            <RowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField ItemStyle-Width="1%" ItemStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="CheckAllEmp(this);" />
                                    </HeaderTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkEmp" runat="server" AutoPostBack="false"></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Rows" ItemStyle-Width="5%" HeaderText="Row" SortExpression="rows" />
                                <asp:TemplateField HeaderText="Employee" ItemStyle-Font-Bold="true" SortExpression="name">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/Finance/Payroll/EmployeePayroll?id={0}",
                     HttpUtility.UrlEncode(Eval("id").ToString())) %>' onclick="window.open (this.href, 'popupwindow',  'width=700,height=700,scrollbars,resizable'); return false;" 
                                        Text='<%# Eval("name")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>                              
                                <asp:BoundField DataField="bank" HeaderText="Bank" SortExpression="bank"   />
                                <asp:BoundField DataField="RecipientCode" HeaderText="RecipientCode" SortExpression="RecipientCode"  />                           
                                <asp:BoundField DataField="Net Pay" HeaderText="Net Pay" ItemStyle-HorizontalAlign="Right" SortExpression="Net Pay" DataFormatString="{0:n}"/>                             
                                <asp:BoundField DataField="PaymentStatus" HeaderText="Payment Status" SortExpression="PaymentStatus"   />
                            </Columns>
                            <HeaderStyle BackColor="White" ForeColor="#1BA691" HorizontalAlign="center" />
                        </asp:GridView>
                    
                <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
                <script type="text/javascript">
                    $(function () {
                        $("[id*=GridVwHeaderChckbox] td").hover(function () {
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
             
                
        </form>
           <div class="loading" align="center">
        Processing, please wait...<br />
        <br />
        <img src="../../../images/loaders.gif" alt="" />
    </div>
    </body>
    </html>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    
</asp:Content>
