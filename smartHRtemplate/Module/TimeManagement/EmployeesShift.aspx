<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="EmployeesShift.aspx.vb"
    Inherits="GOSHRM.EmployeesShift" EnableEventValidation="false" Debug="true" %>

    <%@ register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    

    <html xmlns="http://www.w3.org/1999/xhtml">
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

    <script type="text/javascript" language="javascript">
        //    Grid View Check box
        function CheckAllEmp(Checkbox) {
            var GridVwHeaderChckbox = document.getElementById("<%=GridVwHeaderChckbox.ClientID %>");
            for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }
    </script>
    <title></title>
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
         function ConfirmImport() {
             var confirm_value = document.createElement("INPUT");
             confirm_value.type = "hidden";
             confirm_value.name = "confirm_value";
             if (confirm("Import Attendance from ZKTeco Database?")) {
                 confirm_value.value = "Yes";
             } else {
                 confirm_value.value = "No";
             }
             document.forms[0].appendChild(confirm_value);
         }
    </script>
    
    <body style="">
        <form id="form1">
        <div class="container col-md-12">
            <div class="row">
                 <div id="divalert" runat="server" visible="false" class="alert alert-info">
                    <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                    <strong id="msgalert" runat="server">Danger!</strong>
                </div>
            </div>
       <div class="panel panel-success">
                <div class="panel-heading">
                    <h5><b id="pagetitle" runat="server"></b></h5>
                </div>
             <div class="panel-body">
        
         <div class="row">                                           
            <div class="search-box-wrapper col-sm-6 col-md-3 col-xs-12 form-group pull-right">                        
                    <input id="txtsearch" style="width:100%" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                    <button onserverclick="btnFind_Click" id="btsearch" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                </div>
                 <div class="col-md-3 col-sm-6 col-xs-12 form-group pull-right">                                  
                    <button id="btnUploadFile" type="button" runat="server" class="fa fa-upload btn btn-default btn-sm"
                        onserverclick="btnUpload_Click" data-toggle="tooltip" data-original-title="CSV File (Comma Delimited): ShiftName,Empid,DateFrom,DateTo(e.g Present)" style="margin-right:10px;margin-left:10px;height:35px"></button>
                        <button id="btExport" data-toggle="tooltip" data-original-title="Export" type="button" runat="server" class="fa fa-download btn btn-default btn-sm" onserverclick="btnExport_Click"
                    style="margin-right:10px;height: 35px"></button>
                        <asp:LinkButton ID="btnDelete" data-toggle="tooltip" data-original-title="Delete" Height="35px" runat="server" CssClass="btn btn-default btn-sm" OnClick="Delete" OnClientClick="Confirm()">
                            <span style="margin-top:5px" aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                        </asp:LinkButton>
                         <button id="btAdd" type="button" data-toggle="tooltip" data-original-title="Add" runat="server" class="glyphicon glyphicon-plus btn btn-default btn-sm" onserverclick="btnAdd_Click"
                        style="height: 35px;margin-right:10px;margin-left:10px;"></button> 
                </div>
                <div class="col-md-3 col-sm-6 col-xs-12 pull-right">
                    <input style="height:35px;" class="form-control" type="file" id="FileUpload1" runat="server" />
                </div>
                <div class="col-sm-6 col-md-3 col-xs-12 pull-right">
                                                <telerik:RadComboBox AutoPostBack="True" ID="cboCompany" runat="server" Filter="Contains" EnableCheckAllItemsCheckBox="False"
                                                    RenderMode="Lightweight" Width="100%" ForeColor="#666666" Skin="Bootstrap">
                                                </telerik:RadComboBox>
                                            </div>     
        </div>
            
        <div class="row">
                 <asp:GridView ID="GridVwHeaderChckbox" runat="server" OnSorting="SortRecords" AllowSorting="True"
                    BorderStyle="Solid" Font-Names="Verdana" AllowPaging="True" PageSize="100" DataKeyNames="id"
                    OnRowDataBound="OnRowDataBound" Width="100%" Height="50px" ToolTip="click row to select record"
                    Font-Size="12px" ShowHeaderWhenEmpty="True" EmptyDataText="No data to display"
                    AutoGenerateColumns="False" GridLines="Both" ForeColor="#666666" BorderWidth="1px"
                    BorderColor="#CCCCCC" CssClass="table table-condensed">
                    <RowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField ItemStyle-Width="1px">
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="CheckAllEmp(this);" />
                            </HeaderTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemTemplate>
                                <asp:CheckBox ID="chkEmp" runat="server"></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Row" ItemStyle-Width="5px" HeaderText="Row" />
                        <asp:BoundField DataField="shiftname" ItemStyle-Width="60px" HeaderText="Shift"  SortExpression="shiftname"/>
                        <asp:TemplateField HeaderText="Emp ID" SortExpression="empid" >
                                    <ItemTemplate>
                                        <a href="" onclick='openShift("<%# Eval("id") %>");'><%# Eval("empid") %></a>
                                    </ItemTemplate>
                                    <ItemStyle Font-Bold="True" Width="50px"/>
                         </asp:TemplateField>  
                        <asp:BoundField DataField="Name" ItemStyle-Width="120px" HeaderText="EMP Name"  SortExpression="Name"/>                  
                        <asp:BoundField DataField="Office" ItemStyle-Width="200px" HeaderText="Unit/Dept."  SortExpression="Office" />
                        <asp:BoundField DataField="location" ItemStyle-Width="100px" HeaderText="Location"  SortExpression="location" />
                        <asp:BoundField DataField="datefrom" ItemStyle-Width="50px" HeaderText="From"  SortExpression="datefrom" />
                        <asp:BoundField DataField="dateto" ItemStyle-Width="50px" HeaderText="To"  SortExpression="dateto" />  
                   

                    <asp:TemplateField HeaderText="" ItemStyle-Width="50px" ItemStyle-Font-Bold="true">
                            <ItemTemplate>                             
                                     <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/Employee/TimeManagement/EmployeeShiftHistory.aspx?empid={0}",
                     HttpUtility.UrlEncode(Eval("empid").ToString())) %>' onclick="window.open (this.href, 'popupwindow',  'width=1000,height=700,scrollbars,resizable'); return false;"
                                            Text='History' />
                            </ItemTemplate>
                        </asp:TemplateField>                   
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
         
                <script type="text/javascript">
                    function openShift(code) {
                        window.open("EmployeesShiftUpdate.aspx?id=" + code, "open_window", "width=600,height=400");
                    }
                </script>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
        </div>
        </div>
         </div>
        </div>
        </form>
    </body>
    </html>
    
   <%-- <div class="loading" align="center">
        Please wait...<br />
        <br />
        <img src="../../images/loader.gif" alt="" />
    </div>--%>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .style21
        {
            width: 100%;
        }
        .style22
        {
        }
    </style>
</asp:Content>
