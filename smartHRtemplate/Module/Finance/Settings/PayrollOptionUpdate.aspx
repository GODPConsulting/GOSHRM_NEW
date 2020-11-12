<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="PayrollOptionUpdate.aspx.vb"
    Inherits="GOSHRM.PayrollOptionUpdate" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <title></title>


    <body>
        <form id="form1">
            <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
            <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
            <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
                rel="Stylesheet" type="text/css" />
            <script type="text/javascript">
                function corevalues(id) {
                    var id = id
                    corevalues1(id)
                    corevalues2(id)
                    
                }

                function corevalues1(id) {
                    alert(id)
                    var pid = <%=Request.QueryString("id")%>;
                    //alert(pid)
                    $.ajax({
                        url: "<%= Page.ResolveClientUrl("~/res_new/gos.asmx/FinanceComponents") %>",
                method: 'post',
                data: {
                    pid: pid
                },
                dataType: 'json',

                success: function (data) {
                    $('#performanceid').val(id);
                    //var selsubmod = $('#tableComment');
                    var selsubmod = document.getElementById("grateful");
                  
                    selsubmod.innerHTML = ''
                    // selsubmod.empty();
                    // selsubmod.append('<option value=-1>--Select KPI Metric--</option>');
                    $(document).ready(function () {
                        $(selsubmod).multiselect();
                    });

                    $(data).each(function (index, prog) {
                        console.log(prog)
                        selsubmod.innerHTML += '<option value=' + prog.points + '>' + prog.desc + '</option>';
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
                    alert(id)
                    var pid = id;
                    //alert(pid)
                    $.ajax({
                        url: "<%= Page.ResolveClientUrl("~/res_new/gos.asmx/DaysComponents") %>",
                        method: 'post',
                        data: {
                            pid: pid
                        },
                        dataType: 'json',

                        success: function (data) {
                            $('#performanceid').val(id);
                            //var selsubmod = $('#tableComment');
                            
                            var selsubmod1 = document.getElementById("grateful1");
                            selsubmod1.innerHTML = ''
                            // selsubmod.empty();
                            // selsubmod.append('<option value=-1>--Select KPI Metric--</option>');
                            $(document).ready(function () {
                                $(selsubmod1).multiselect();
                            });

                            $(data).each(function (index, progs) {
                                console.log(progs)
                                selsubmod1.innerHTML += '<option value=' + progs.day + '>' + progs.day + '</option>';
                            });


                        },
                        error: function (err) {
                            //alert(JSON.stringify(err));
                            $(err).each(function (index, prog) {
                                $(document).ready(function () {
                                    $(selsubmod1).multiselect();
                                });
                                $('#msgbox2').css('display', 'block');
                                $("#pmsg").text(prog.responseText);
                            });
                        }
                    });
                }
            </script>




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
                   
                    
                   <textarea id="performanceid" name="performanceid" style="display:none"  ></textarea>
                    
                     
                    
                     <div class="form-group">
                        <div class="col-md-12">
                            <label>Salary Components</label><br />
                             
                            <select style="width:100%"  id="grateful" name="grateful" class="mdb-select colorful-select dropdown-primary md-form" multiple searchable="Search here..">
      
    </select>
                 
                                   
                        </div>
                    </div>
                       <div class="form-group">
                        <div class="col-md-12">
                            <label>Days Applied</label><br />
                             
                            <select style="width:100%"  id="grateful1" name="grateful1" class="mdb-select colorful-select dropdown-primary md-form" multiple searchable="Search here..">
      
    </select>
                 
                                   
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
                <button id="btnsubmit1" type="button"  runat="server" onserverclick="Add_Details"   class="btn btn-success m-t-10">Save</button>
                <button type="button" onclick="hide_msg()" class="btn btn-danger m-t-10" data-dismiss="modal">Close</button>
            </div>
        </div>
    </div>
</div>
            <div class="container col-md-10">
                <div class="row">
                    <div id="divalert" runat="server" visible="false" class="alert alert-info">
                        <strong id="msgalert" runat="server"></strong>
                        <asp:TextBox ID="txtid" runat="server" Width="3px"
                            Font-Names="Candara" Height="2px" Visible="False"></asp:TextBox>
                        <asp:Label ID="lblid" runat="server" Font-Names="Verdana" Font-Size="12px"
                            Font-Bold="True" ForeColor="#666666"
                            Text="0" Visible="False"></asp:Label>
                        <asp:Label ID="lblauto" runat="server" Text="Label" Visible="False" Width="1px"
                            Font-Size="1px"></asp:Label>
                        <asp:Label ID="lblemail" runat="server" Text="Label" Visible="False" Width="1px"
                            Font-Size="1px"></asp:Label>
                        <asp:Label ID="lblovertimeenabled" runat="server" Text="Label" Visible="False" Width="1px"
                            Font-Size="1px"></asp:Label>
                        <asp:Label ID="lblattendance" runat="server" Text="Label" Visible="False" Width="1px"
                            Font-Size="1px"></asp:Label>
                         <asp:TextBox ID="txtskillid" runat="server" Font-Size="1px" Height="1px" Width="1px"
                            Visible="False"></asp:TextBox>
                    </div>
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <h5><b id="pagetitle" runat="server">Payroll Option</b></h5>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>COMPANY</label>
                                        <telerik:RadComboBox Skin="Bootstrap" ID="cboCompany" runat="server" EnableCheckAllItemsCheckBox="True"
                                            RenderMode="Lightweight" Width="100%"
                                            AutoPostBack="True" ForeColor="#666666"
                                            Filter="Contains"
                                            Font-Names="Verdana" Font-Size="12px">
                                        </telerik:RadComboBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>PAYROLL CURRENCY</label>
                                        <telerik:RadDropDownList Skin="Bootstrap" ID="drpCurrency" runat="server" DefaultMessage="--Select--" ForeColor="#666666"
                                            Font-Names="Verdana" Font-Size="12px" Width="100%"
                                            ToolTip="currency payroll and other amounts are automatically based on">
                                        </telerik:RadDropDownList>
                                    </div>
                                </div>
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>AUTO APPROVE PAYSLIP:</label>
                                        <asp:RadioButtonList Skin="Bootstrap" ID="rdoAutoApprove" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" ForeColor="#666666"
                                            Font-Names="Verdana" Font-Size="12px" Width="100%">
                                            <asp:ListItem Selected="True">Yes</asp:ListItem>
                                            <asp:ListItem Value="No">No, Payroll must go through an approval process</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                 </div>
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            MINIMUM ADJUSTMENT AMOUNT REQUIRING APPROVALS</label>
                                        <input id="txtAmount" runat="server" class="form-control" type="text" />
                                    </div>
                                </div>
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <label>
                                            PAYSLIP CAN BE APPROVED BY</label>
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel4" UpdateMode="Always">
                                            <ContentTemplate>
                                                <telerik:RadComboBox ID="cboApprove" Skin="Bootstrap" runat="server"
                                                    CheckBoxes="True"
                                                    RenderMode="Lightweight" Width="100%" AutoPostBack="True" ForeColor="#666666"
                                                    Filter="Contains"
                                                    Font-Names="Verdana" Font-Size="12px">
                                                </telerik:RadComboBox>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cboCompany" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="form-group">
                                        <telerik:RadListBox ID="lstApprover" runat="server"
                                            ResolvedRenderMode="Classic" BorderStyle="None" ForeColor="#666666"
                                            Enabled="False" Width="100%"
                                            RenderMode="Lightweight" Sort="Ascending" Font-Names="Verdana"
                                            Font-Size="12px">
                                            <ButtonSettings TransferButtons="All"></ButtonSettings>
                                            <EmptyMessageTemplate>
                                                None
                                            </EmptyMessageTemplate>
                                        </telerik:RadListBox>
                                    </div>
                                </div>
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>AUTO EMAIL APPROVED PAYSLIP TO EMPLOYEES</label>
                                        <telerik:RadDropDownList Skin="Bootstrap" ID="radAutoEmailSlips" runat="server" DefaultMessage="--Select--"
                                            Font-Names="Verdana" Font-Size="12px" Width="100%" Height="31px" ForeColor="#666666"
                                            ResolvedRenderMode="Classic"
                                            ToolTip="Automatically send approved payslips to employees immediately">
                                            <Items>
                                                <telerik:DropDownListItem runat="server" DropDownList="radAutoEmailSlips"
                                                    Text="No" Value="No" />
                                                <telerik:DropDownListItem runat="server" DropDownList="radAutoEmailSlips"
                                                    Text="Yes" Value="Yes" />
                                            </Items>
                                        </telerik:RadDropDownList>
                                    </div>
                                </div>
                                
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            PER DAY SALARY ADJUSTMENT ON RECRUITS</label>
                                        <telerik:RadDropDownList Skin="Bootstrap" ID="drpAdjustment" runat="server" DefaultMessage="--Select--" ForeColor="#666666"
                                            Font-Names="Verdana" Font-Size="12px" Width="100%" Height="16px"
                                            ToolTip="generate salary pay for new recruits based on resumption date">
                                        </telerik:RadDropDownList>
                                    </div>
                                </div>
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            MONTHLY SALARIES CALCULATED BASED ON ATTENDANCE</label>
                                        <telerik:RadDropDownList Skin="Bootstrap" ID="radPayOnAttendance" runat="server" DefaultMessage="--Select--" ForeColor="#666666"
                                            Font-Names="Verdana" Font-Size="12px" Width="100%" Height="31px"
                                            ResolvedRenderMode="Classic"
                                            ToolTip="base salary pay on monthly attendance">
                                            <Items>
                                                <telerik:DropDownListItem runat="server" Text="No" Value="No" />
                                                <telerik:DropDownListItem runat="server" Text="Yes" Value="Yes" />
                                            </Items>
                                        </telerik:RadDropDownList>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>
                                            OVERTIME PAYMENT ENABLED</label>
                                        <telerik:RadDropDownList ID="radPayOverTime" Skin="Bootstrap" runat="server" DefaultMessage="--Select--" ForeColor="#666666"
                                            Font-Names="Verdana" Font-Size="12px" Width="100%" Height="31px"
                                            ResolvedRenderMode="Classic" ToolTip="enabled overtime payment"
                                            AutoPostBack="True">
                                            <Items>
                                                <telerik:DropDownListItem runat="server" DropDownList="radPayOverTime"
                                                    Text="No" Value="No" />
                                                <telerik:DropDownListItem runat="server" DropDownList="radPayOverTime"
                                                    Text="Yes" Value="Yes" />
                                            </Items>
                                        </telerik:RadDropDownList>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Always">
                                        <ContentTemplate>
                                            <asp:Label ID="lblOvertimePaymentID" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                                                Text="Overtime Payment Index:"></asp:Label>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="radPayOverTime" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>

                                    <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Always">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtOvertimeIndex" runat="server" Width="70px"
                                                Font-Names="Verdana" Font-Size="12px" ForeColor="#666666"
                                                BorderColor="#CCCCCC" BorderWidth="1px"></asp:TextBox>
                                            &nbsp;<asp:Label ID="lblpaydesc" runat="server" Font-Names="Verdana" Font-Size="12px" ForeColor="#666666"
                                                Text="Overtime Payment = (Basic * (Overtime/WorkShift)) * OverTimeIndex"
                                                Font-Italic="True"></asp:Label>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="radPayOverTime" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>

                                    <asp:LinkButton ID="lnkexception" runat="server" Font-Names="Verdana" Font-Size="12px"
                                        ToolTip="Grades exempted from Attendance and Overtime">Job Grades Excluded from Overtime and Attendance</asp:LinkButton>
                                </div>
                                <div class="col-md-12 m-t-20 text-center">
                                    <button id="btnupdate" runat="server" onserverclick="btnAdd_Click" type="submit"
                                        style="width: 150px" class="btn btn-primary btn-success">
                                        Save &amp; Update</button>
                                    <button id="Button1" runat="server" onserverclick="btnBack_Click" type="submit" style="width: 150px"
                                        class="btn btn-primary btn-danger">
                                        << Back</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                   <div id="pnskill" runat="server" class="panel panel-info">
                <div class="panel-heading">
                   <h6><b>Overtime Set Up</b></h6>  
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="search-box-wrapper col-sm-6 col-md-3 col-xs-12 form-group pull-right">
                            <input id="search" style="width: 100%" runat="server" type="text" placeholder="Search..." class="search-box-input" />
                            <button onserverclick="btnFind_Click" id="btsearch" runat="server" class="search-box-button"><i style="color: Black;" class="fa fa-search"></i></button>
                        </div>
                        <div class="col-md-3 col-sm-6 col-xs-12 form-group pull-right">
                            <button id="btExport" data-toggle="tooltip" data-original-title="Export" type="button" runat="server" class="fa fa-download btn btn-default btn-sm" onserverclick="btnExport_Click"
                                style="height: 35px">
                            </button>
                            <button id="btnUploadFile" type="button" runat="server" class="fa fa-upload btn btn-default btn-sm"
                                onserverclick="btnUpload_Click" data-toggle="tooltip" data-original-title="Upload(Skill, CourseCode, Rating)" style="margin-right: 10px; margin-left: 10px; height: 35px">
                            </button>
                            <asp:LinkButton ID="btDelete" data-toggle="tooltip" data-original-title="Delete" Height="35px" runat="server" CssClass="btn btn-default btn-sm" OnClick="Delete" OnClientClick="Confirm()">
                            <span style="margin-top:5px" aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                            </asp:LinkButton>
                            <button id="btAdd" type="button" data-toggle="tooltip" data-original-title="Add Skills" runat="server" class="glyphicon glyphicon-plus btn btn-default btn-sm" onserverclick="btnAddSkill_Click"
                                style="height: 35px; margin-right: 10px; margin-left: 10px;">
                            </button>
                        </div>
                        <div class="col-md-3 col-sm-6 col-xs-12 pull-right">
                            <input style="height: 35px;" class="form-control" type="file" id="file1" runat="server" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="table-responsive">
                            <asp:GridView ID="gridskills" runat="server" OnSorting="SortRecords" AllowSorting="True"
                                BorderStyle="Solid" Font-Names="Verdana" AllowPaging="True" PageSize="10" DataKeyNames="id"
                                Width="100%" Height="50px" ToolTip="click row to select record" Font-Size="12px"
                                ShowHeaderWhenEmpty="True" EmptyDataText="No data to display" AutoGenerateColumns="False"
                                GridLines="Both" ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC"
                                CssClass="table table-condensed">
                                <RowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top">
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="CheckAllEmp(this);" />
                                        </HeaderTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkEmp" runat="server"></asp:CheckBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Rows" HeaderText="Rows" SortExpression="rows"
                                        ItemStyle-VerticalAlign="Top" />
                                    <asp:TemplateField HeaderText="Jobgrade" SortExpression="skills"
                                        ItemStyle-VerticalAlign="Top">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDownload" Text='<%# Eval("jobgrade")%>' CommandArgument='<%# Eval("id") %>'
                                                runat="server" OnClick="DrillDown"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:BoundField DataField="rate" HeaderText="Rate" SortExpression="rating"
                                        ItemStyle-VerticalAlign="Top" />
                                     <asp:BoundField DataField="Active" HeaderText="Status" SortExpression="rating"
                                        ItemStyle-VerticalAlign="Top" />
                                      <asp:TemplateField HeaderText="ADD" ItemStyle-Font-Bold="true" SortExpression="name">
                                    <ItemTemplate>
                                       
                                        <a href="#" data-toggle="modal" data-target="#loginModal1" onclick='corevalues("<%# Eval("id") %>");' >
                                        ADD </a>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                </Columns>
                                <HeaderStyle BackColor="White" ForeColor="#1BA691" HorizontalAlign="center" />
                            </asp:GridView>
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
            <%-- <table width="100%">
            <tr>
                <td class="style34" colspan="4" style="background-color: #1BA691">
                    <strong>Payroll Option</strong>
                </td>
            </tr>
            <tr>
                <td class="style37">
                </td>
                <td class="style35">                  
                    <asp:Label ID="lblstatus" runat="server" Font-Bold="True" Font-Names="Candara" Font-Size="12px"
                        ForeColor="#FF3300"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style31">                    
                    <asp:Label ID="lblauto1" runat="server" Font-Names="Verdana" Font-Size="12px" 
                        Font-Bold="True" ForeColor="#666666"
                        Text="Company:"></asp:Label>
                </td>
                <td class="style35">
                              <telerik:RadComboBox ID="cboCompany" runat="server" EnableCheckAllItemsCheckBox="True"
                                                RenderMode="Lightweight" Width="500px" 
                        AutoPostBack="True" ForeColor="#666666"
                                Filter="Contains" 
                                                Font-Names="Verdana" Font-Size="12px">
                              </telerik:RadComboBox>
                </td>
                <td class="style27">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style37">
                    &nbsp;</td>
                <td class="style35">
                    &nbsp;</td>
                <td class="style27">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style37">
                      <asp:Label ID="Label5" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                        Text="Payroll Currency:" Visible="True"></asp:Label>
                   
                </td>
                <td class="style35">
                    <telerik:RadDropDownList ID="drpCurrency" runat="server" DefaultMessage="--Select--" ForeColor="#666666"
                        Font-Names="Verdana" Font-Size="12px" Width="500px" Height="31px" 
                        ToolTip="currency payroll and other amounts are automatically based on">
                    </telerik:RadDropDownList>
                </td>
                <td class="style27">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style31">                    
                    &nbsp;</td>
                <td class="style35">
                    &nbsp;</td>
                <td class="style27">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style31">                    
                    <asp:Label ID="lblauto0" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                        Text="Auto Approve Payslip:" Visible="True"></asp:Label>
                </td>
                <td class="style35">
                    <asp:RadioButtonList ID="rdoAutoApprove" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" ForeColor="#666666"
                        Font-Names="Verdana" Font-Size="12px" Width="500px">
                        <asp:ListItem Selected="True">Yes</asp:ListItem>
                        <asp:ListItem Value="No">No, Payroll must be go through an approval process</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td class="style27">
                                 <asp:Label ID="lblauto" runat="server" Text="Label" Visible="False" Width="1px" 
                        Font-Size="1px"></asp:Label>                                  
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style37">
                    &nbsp;</td>
                <td class="style35">
                    &nbsp;
                </td>
                <td class="style27">
                    &nbsp;
                    <asp:Label ID="lblid" runat="server" Font-Names="Verdana" Font-Size="12px" 
                        Font-Bold="True" ForeColor="#666666"
                        Text="0" Visible="False"></asp:Label>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style37">                 
                        <asp:Label ID="Label1" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                        Text="Payslip can be approved by:" Visible="True"></asp:Label>
                </td>
                <td class="style35">
                       <asp:UpdatePanel runat="server" ID="UpdatePanel4" UpdateMode="Always">
                        <ContentTemplate>
                              <telerik:RadComboBox ID="cboApprove" runat="server" 
                                CheckBoxes="True"
                                                RenderMode="Lightweight" Width="500px" AutoPostBack="True" ForeColor="#666666"
                                Filter="Contains" 
                                                Font-Names="Verdana" Font-Size="12px">
                              </telerik:RadComboBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cboCompany" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td class="style27">
                   
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style37">
                    
                </td>
                <td class="style35">
                            <telerik:RadListBox ID="lstApprover" runat="server" 
                                ResolvedRenderMode="Classic" BorderStyle="None" ForeColor="#666666"
                                Enabled="False" Width="500px" 
                                RenderMode="Lightweight" Sort="Ascending" Font-Names="Verdana" 
                                    Font-Size="12px">
                                <ButtonSettings TransferButtons="All"></ButtonSettings>
                                <EmptyMessageTemplate>
                                   None
                                </EmptyMessageTemplate>
                            </telerik:RadListBox>
                       
                </td>
                <td class="style30">
                </td>
                <td class="style24">
                </td>
            </tr>
            <tr>
                <td class="style37">
                    &nbsp;</td>
                <td class="style35">
                    &nbsp;</td>
                <td class="style30">
                </td>
                <td class="style24">
                </td>
            </tr>
            <tr>
                <td class="style37">
                 <asp:Label ID="Label2" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                        Text="Auto Email Approved Payslip to Employees:"></asp:Label>
                    
                </td>
                <td class="style35">
                    <telerik:RadDropDownList ID="radAutoEmailSlips" runat="server" DefaultMessage="--Select--"
                        Font-Names="Verdana" Font-Size="12px" Width="500px" Height="31px" ForeColor="#666666"
                        ResolvedRenderMode="Classic" 
                        ToolTip="Automatically send approved payslips to employees immediately">
                        <Items>
                            <telerik:DropDownListItem runat="server" DropDownList="radAutoEmailSlips" 
                                Text="No" Value="No" />
                            <telerik:DropDownListItem runat="server" DropDownList="radAutoEmailSlips" 
                                Text="Yes" Value="Yes" />
                        </Items>
                    </telerik:RadDropDownList>
                </td>
                <td valign="top" class="style27">
                    
                               <asp:Label ID="lblemail" runat="server" Text="Label" Visible="False" Width="1px" 
                        Font-Size="1px"></asp:Label></td>
                    
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style37">
                    </td>
                <td class="style35">
                    
                </td>
                <td valign="top" class="style27">
                    
                </td>
                <td>
                    
                </td>
            </tr>
            <tr>
                <td class="style37">                    
                       <asp:Label ID="Label3" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                        Text="Minimum Adjustment Amount requiring Approvals:" Visible="True"></asp:Label>
                 </td>
                <td class="style35">
                    
        <asp:TextBox ID="txtAmount" runat="server" Width="195px" ForeColor="#666666"
            Font-Names="Verdana" Font-Size="12px"
            BorderColor="#CCCCCC" BorderWidth="1px" 
                        ToolTip="Minimal Adjustment amount requiring approval before passed"></asp:TextBox>
       
                </td>
                <td valign="top" class="style27">
                    
                </td>
                <td>
                    
                </td>
            </tr>
            <tr>
                <td class="style37">
                    </td>
                <td class="style35">
                    
                </td>
                <td valign="top" class="style27">
                    
                </td>
                <td>
                    
                </td>
            </tr>
            <tr>
                <td class="style37">
                    <asp:Label ID="Label4" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                        Text="Per Day Salary Adjustment on Recruits:" Visible="True"></asp:Label>
                    
                </td>
                <td class="style35">
                    <telerik:RadDropDownList ID="drpAdjustment" runat="server" DefaultMessage="--Select--" ForeColor="#666666"
                        Font-Names="Verdana" Font-Size="12px" Width="500px" Height="16px" 
                        ToolTip="generate salary pay for new recruits based on resumption date">
                    </telerik:RadDropDownList>
                </td>
                <td class="style27">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            
            <tr>
                <td class="style37">
                      &nbsp;</td>
                <td class="style35">
                    &nbsp;</td>
                <td class="style27">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style37">
                      <asp:Label ID="Label7" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                        Text="Monthly Salaries calculated based on Attendance:"></asp:Label>
                   
                </td>
                <td class="style35">
                    <telerik:RadDropDownList ID="radPayOnAttendance" runat="server" DefaultMessage="--Select--" ForeColor="#666666"
                        Font-Names="Verdana" Font-Size="12px" Width="500px" Height="31px" 
                        ResolvedRenderMode="Classic" 
                        ToolTip="base salary pay on monthly attendance">
                        <Items>
                            <telerik:DropDownListItem runat="server" Text="No" Value="No" />
                            <telerik:DropDownListItem runat="server" Text="Yes" Value="Yes" />
                        </Items>
                    </telerik:RadDropDownList>
                </td>
                <td class="style27">
                     <asp:Label ID="lblattendance" runat="server" Text="Label" Visible="False" Width="1px" 
                        Font-Size="1px"></asp:Label>                   
                </td>
                <td>
                   
                </td>
            </tr>
            <tr>
                <td class="style37">
                      </td>
                <td class="style35">
                    </td>
                <td class="style27">
                  
                </td>
                <td>
                
                </td>
            </tr>
            <tr>
                <td class="style37">
                      <asp:Label ID="Label8" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                        Text="Overtime Payment enabled:"></asp:Label>
                   
                </td>
                <td class="style35">
                    <telerik:RadDropDownList ID="radPayOverTime" runat="server" DefaultMessage="--Select--" ForeColor="#666666"
                        Font-Names="Verdana" Font-Size="12px" Width="500px" Height="31px" 
                        ResolvedRenderMode="Classic" ToolTip="enabled overtime payment" 
                        AutoPostBack="True">
                        <Items>
                            <telerik:DropDownListItem runat="server" DropDownList="radPayOverTime" 
                                Text="No" Value="No" />
                            <telerik:DropDownListItem runat="server" DropDownList="radPayOverTime" 
                                Text="Yes" Value="Yes" />
                        </Items>
                    </telerik:RadDropDownList>
                </td>
                <td class="style27">
                                 <asp:Label ID="lblovertimeenabled" runat="server" Text="Label" Visible="False" Width="1px" 
                        Font-Size="1px"></asp:Label>
                </td>
                <td>
                    
                </td>
            </tr>
            <tr>
                <td class="style37">  
                    <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Always">
                        <ContentTemplate>
                             <asp:Label ID="lblOvertimePaymentID" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                             Text="Overtime Payment Index:"></asp:Label> 
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="radPayOverTime" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>                 
                                           
                </td>
                <td class="style35">
                   <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Always">
                        <ContentTemplate>
                                  <asp:TextBox ID="txtOvertimeIndex" runat="server" Width="70px" 
                            Font-Names="Verdana" Font-Size="12px" ForeColor="#666666"
                            BorderColor="#CCCCCC" BorderWidth="1px"></asp:TextBox>
                                  &nbsp;<asp:Label ID="lblpaydesc" runat="server" Font-Names="Verdana" Font-Size="12px"  ForeColor="#666666"
                             Text="Overtime Payment = (Basic * (Overtime/WorkShift)) * OverTimeIndex" 
                                      Font-Italic="True"></asp:Label>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="radPayOverTime" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel> 
                </td>
                <td class="style27">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style37">
                      &nbsp;</td>
                <td class="style35">
                    <asp:LinkButton ID="lnkexception" runat="server" 
            Font-Names="Verdana"  Font-Size="12px" 
                    ToolTip="Grades exempted from Attendance and Overtime">Job Grades Excluded from Overtime and Attendance</asp:LinkButton>
                </td>
                <td class="style27">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style37">
                    <asp:Button ID="btnAdd" runat="server" Text="Save" BackColor="#1BA691" ForeColor="White"
                        Width="120px" Height="25px" BorderStyle="None" Font-Names="Verdana" 
                        Font-Size="12px" />
                   
                    <asp:Button ID="btnreloadapprover" runat="server" BackColor="White" ForeColor="#666666"
                        Width="120px" Height="25px" BorderStyle="None" Font-Names="Verdana" 
                        Font-Size="12px" />
                   
                </td>
                <td class="style35">
                    <asp:Button ID="btnBack" runat="server" Text="Back" BackColor="#999966" ForeColor="White"
                        Width="120px" Height="25px" BorderStyle="None" Font-Names="Verdana" 
                        Font-Size="12px" />
                   
                </td>
                <td class="style27">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            </table>--%>
        </form>
    </body>
    </html>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
</asp:Content>
