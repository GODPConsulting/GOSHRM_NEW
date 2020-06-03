<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="Attendance.aspx.vb"
    Inherits="GOSHRM.Attendance" EnableEventValidation="false" Debug="true" %>

    <%@ register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    

    <html xmlns="http://www.w3.org/1999/xhtml">
    

    <script type="text/javascript" language="javascript">
        //    Grid View Check box
        function CheckAllEmp(Checkbox) {
            var GridVwHeaderChckbox = document.getElementById("<%=GridVwHeaderChckbox.ClientID %>");
            for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }
    </script>
    <script type="text/javascript" language="javascript">
        //    Grid View Check box
        function CheckAlllog(Checkbox) {
            var gridlog = document.getElementById("<%=gridlog.ClientID %>");
            for (i = 1; i < gridlog.rows.length; i++) {
                gridlog.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
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
         function RefreshLog() {
             var confirm_value = document.createElement("INPUT");
             confirm_value.type = "hidden";
             confirm_value.name = "confirm_value";
             if (confirm("Refresh Attendance?")) {
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
             if (confirm("Import Attendance from Attendance Database based on date range selected?")) {
                 confirm_value.value = "Yes";
             } else {
                 confirm_value.value = "No";
             }
             document.forms[0].appendChild(confirm_value);
         }
    </script>

     <script type="text/javascript">
         function ConfirmUpload() {
             var confirm_value = document.createElement("INPUT");
             confirm_value.type = "hidden";
             confirm_value.name = "confirm_value";
             if (confirm("Import Attendance from File based on date range selected?")) {
                 confirm_value.value = "Yes";
             } else {
                 confirm_value.value = "No";
             }
             document.forms[0].appendChild(confirm_value);
         }
    </script>
    <body style="">
        <form id="form1">
                    <div class="content container-fluid">
					    <div class="row">
						    <div class="col-sm-8">
							   <div id="divalert" runat="server" visible="false" class="alert alert-info">
                                    <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                                    <strong id="msgalert" runat="server">Danger!</strong>
                                </div>
						    </div>
					    </div>

                        <div class="panel panel-success">
                            <div class="panel-heading">
                                <h5><b id="pagetitle" runat="server"></b></h5>
                            </div>
                         <div class="panel-body">
					    <div class="row">
						    <div class="col-md-3 col-sm-3 col-xs-6">
							    <div class="form-group form-focus select-focus">							    
                                    <telerik:RadComboBox Skin="Bootstrap" runat="server" DropDownAutoWidth="Enabled" RenderMode="Lightweight"
                                        ResolvedRenderMode="Classic" Width="100%" ID="cboCompany" Filter="Contains"
                                        AutoPostBack="True" Font-Names="Verdana" Font-Size="12px" 
                                        ForeColor="#666666">
                                    </telerik:RadComboBox>

							    </div>
						    </div>
						    <div class="col-md-2 col-sm-3 col-xs-6">
							    <div class="form-group form-focus select-focus">
                                    <telerik:RadDatePicker Skin="Bootstrap" ID="dateFrom" runat="server" Font-Names="Verdana" Font-Size="12px"
                                        Height="30px" Width="100%" RenderMode="Lightweight" Culture="en-US" ResolvedRenderMode="Classic">
                                        <Calendar EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;" UseColumnHeadersAsSelectors="False"
                                            UseRowHeadersAsSelectors="False" RenderMode="Lightweight">
                                        </Calendar>
                                        <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" Height="20px" LabelWidth="40%"
                                            RenderMode="Lightweight" Width="">
                                            <EmptyMessageStyle Resize="None" />
                                            <ReadOnlyStyle Resize="None" />
                                            <FocusedStyle Resize="None" />
                                            <DisabledStyle Resize="None" />
                                            <InvalidStyle Resize="None" />
                                            <HoveredStyle Resize="None" />
                                            <EnabledStyle Resize="None" />
                                        </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" CssClass="" />
                                    </telerik:RadDatePicker>
							    </div>
						    </div>

						    <div class="col-md-2 col-sm-3 col-xs-6">
							    <div class="form-group form-focus select-focus">
                                    <telerik:RadDatePicker ID="dateTo" runat="server" Font-Names="Verdana" Font-Size="12px"
                                        Height="30px" Width="100%" Skin="Bootstrap" Culture="en-US" ResolvedRenderMode="Classic" RenderMode="Lightweight">
                                        <Calendar EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;" UseColumnHeadersAsSelectors="False"
                                            UseRowHeadersAsSelectors="False" RenderMode="Lightweight">
                                        </Calendar>
                                        <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" Height="20px" LabelWidth="40%"
                                            Width="" RenderMode="Lightweight">
                                            <EmptyMessageStyle Resize="None" />
                                            <ReadOnlyStyle Resize="None" />
                                            <FocusedStyle Resize="None" />
                                            <DisabledStyle Resize="None" />
                                            <InvalidStyle Resize="None" />
                                            <HoveredStyle Resize="None" />
                                            <EnabledStyle Resize="None" />
                                        </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" CssClass="" />
                                    </telerik:RadDatePicker>
							    </div>
						    </div>
						
						    <div class="col-md-3 col-sm-3 col-xs-6">
                               <asp:Button ID="btnLog" runat="server" BorderStyle="None" ForeColor="White"
                                Height="30px" CssClass="btn btn-primary btn-success" Text="Attendance Log Sheet" Width="100%" Font-Names="Arial" Font-Size="12px"
                                Font-Bold="True" />
						    </div>
						
						    <div class="col-md-2 col-xs-6">
                                <asp:Button ID="btnRegister" runat="server" BorderStyle="None"
                                ForeColor="White" CssClass="btn btn-primary btn-success" Height="30px" Text="Attendance " Width="100%" Font-Names="Arial"
                                Font-Size="12px" Font-Bold="True" />
						    </div>
                        </div>
                <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                    <asp:View ID="vwlogsheet" runat="server">
                    <div>
                    <div class="row">
                    <div class="search-box-wrapper col-sm-6 col-md-3 col-xs-12 form-group pull-right">                        
                        <input id="txtsearch" style="width:100%" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                        <button onserverclick="Button1_Click2" id="btnfind" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                    </div>
                     <div class="col-md-3 col-sm-6 col-xs-12 form-group pull-right">                                  
                        <button id="btnUpload" type="button" runat="server" class="fa fa-upload btn btn-default btn-sm"
                            onserverclick="btnUpload_Click" data-toggle="tooltip" data-original-title="CSV File (Comma Delimited): EmpID,DateTime,Check Indicator" style="margin-right:10px;margin-left:10px;height:35px"></button>
                            <button id="btnExportLog" data-toggle="tooltip" data-original-title="Export" type="button" runat="server" class="fa fa-download btn btn-default btn-sm" onserverclick="btnExport_Click"
                        style="margin-right:10px;height: 35px"></button>
                            <asp:LinkButton ID="btnDelete" data-toggle="tooltip" data-original-title="Delete" Height="35px" runat="server" CssClass="btn btn-default btn-sm" OnClick="Delete" OnClientClick="Confirm()">
                                <span style="margin-top:5px" aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                            </asp:LinkButton>
                             <%--<button id="btAdd" type="button" data-toggle="tooltip" data-original-title="Add" runat="server" class="glyphicon glyphicon-plus btn btn-default btn-sm"
                            style="height: 35px;margin-right:10px;margin-left:10px;"></button>--%>
                            <button id="btnZKteco" type="button" data-toggle="tooltip" data-original-title="Upload from ZTECO Database" runat="server" class="fa fa-upload btn btn-default btn-sm" onserverclick="btnZKteco_Click"
                            style="height: 35px;margin-right:10px;margin-left:10px;"></button>  
                    </div>
                    <div class="col-md-3 col-sm-6 col-xs-12 pull-right">
                        <input style="height:35px;" class="form-control" type="file" id="FileUpload1" runat="server" />
                    </div>
                    <div class="col-md-3 col-sm-6 col-xs-12 pull-right">
                        <input type="checkbox" ID="chkDelete" runat="server"/>Delete Date before Upload
                    </div>     
                </div>
                <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

                <asp:GridView ID="gridLog" runat="server" OnSorting="SortLogRecords" AllowSorting="True"
                    BorderStyle="Solid" Font-Names="Verdana" AllowPaging="True" 
                    PageSize="5000" DataKeyNames="id"
                    Width="100%" Height="50px" ToolTip="click row to select record"
                    Font-Size="11px" AutoGenerateColumns="False" GridLines="Vertical" 
                    ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC" 
                    ShowHeaderWhenEmpty="True">
                    <RowStyle BackColor="White" />
                    <Columns>
                       <asp:TemplateField ItemStyle-Width="5px">
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="CheckAlllog(this);" />
                            </HeaderTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemTemplate>
                                <asp:CheckBox ID="chkEmp" runat="server"></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                    <asp:BoundField DataField="Rows" ItemStyle-Width="5px" HeaderText="Row" />     
                    <asp:BoundField DataField="empid" ItemStyle-Width="50px" HeaderText="Emp ID"  SortExpression ="empid"/> 
                    <asp:BoundField DataField="Name" ItemStyle-Width="120px" HeaderText="Name" SortExpression ="empid"/> 
                    <asp:BoundField DataField="Office" ItemStyle-Width="200px" HeaderText="Office/Dept" SortExpression ="Office"/>
                    <asp:BoundField DataField="location" ItemStyle-Width="150px" HeaderText="Location" SortExpression ="location"/>           
                    <asp:BoundField DataField="checktime" ItemStyle-Width="100px" HeaderText="Check Time" SortExpression="checktime" />
                    <asp:BoundField DataField="punchtype" ItemStyle-Width="20px" HeaderText="Punch Type" SortExpression ="punchtype"/>
                    </Columns>
                    <HeaderStyle BackColor="white" ForeColor="#1BA691" HorizontalAlign="center" />
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
            </asp:View>
            <asp:View ID="vwattendance" runat="server">
                <div>
            <table width="100%" >
                <tr>
                    <td>
                    </td>
                    <td align="center">
                        <asp:Calendar ID="MyCalendar" runat="server" BackColor="White" 
                                            BorderColor="#FFCC66" 
        BorderWidth="1px" DayNameFormat="Shortest" 
                                            Font-Names="Verdana" Font-Size="8pt" 
        ForeColor="#663399" Height="250px" 
                                            ShowGridLines="True" Width="400px">
                                            <DayHeaderStyle BackColor="#FFCC66" Font-Bold="True" Height="1px" 
                                                HorizontalAlign="Center" />
                                            <DayStyle HorizontalAlign="Center" />
                                            <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
                                            <OtherMonthDayStyle ForeColor="#CC9966" />
                                            <SelectedDayStyle BackColor="Black" Font-Bold="True" />
                                            <SelectorStyle BackColor="#FFCC66" />
                                            <TitleStyle BackColor="#33CCCC" Font-Bold="True" Font-Size="9pt" 
                                                ForeColor="#FFFFCC" />
                                            <TodayDayStyle BackColor="#FFCC66" ForeColor="White" />
                                            <WeekendDayStyle ForeColor="Red" />
                                        </asp:Calendar>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </div>
        <div style="margin-top:20px;" class="row">
        <div class="search-box-wrapper col-sm-6 col-md-4 col-xs-12 form-group pull-right">
                        <button id="btnExport" type="button" data-toggle="tooltip" data-original-title="Export" runat="server" onclick="RefreshLog()" class="fa fa-download btn btn-default btn-sm" onserverclick="btnExport_Click"
                        style="margin-left:10px;margin-right:10px;height: 35px;"></button>
                         <button id="btnRefresh" type="button" data-toggle="tooltip" data-original-title="Refresh after upload of attendance log" runat="server" class="glyphicon glyphicon-refresh btn btn-default btn-sm" onserverclick="btnRefresh_Click"
                        style="margin-right:10px;height: 35px;"></button> 
                    <input id="TextBox1" style="width:100%" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                    <button id="Button1" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                </div>
                  
        </div>

        <div class="row">
            <div>
                <asp:GridView ID="GridVwHeaderChckbox" runat="server" OnSorting="SortRecords" AllowSorting="True"
                    BorderStyle="Solid" Font-Names="Verdana" AllowPaging="True" CssClass="table table-condensed" 
                    PageSize="2000" DataKeyNames="id" EmptyDataText="No data to display"
                    OnRowDataBound="OnRowDataBound" Width="100%" Height="50px" ToolTip="click row to select record"
                    Font-Size="11px" AutoGenerateColumns="False" GridLines="Vertical" 
                    ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC" 
                    ShowHeaderWhenEmpty="True">
                    <RowStyle BackColor="White" />
                    <Columns>             
                    <asp:BoundField DataField="Rows" ItemStyle-Width="1%" HeaderText="Row" />     
                    <asp:BoundField DataField="empid"  HeaderText="Emp ID" SortExpression="empid"/> 
                    <asp:BoundField DataField="Name"  HeaderText="Name" SortExpression="Name"/> 
                    <asp:BoundField DataField="Office"  HeaderText="Office/Dept" SortExpression="Office"/>                  
                    <asp:BoundField DataField="shifts"  HeaderText="Shift Time"  />
                    <asp:BoundField DataField="duration"  HeaderText="Duration (Hr)" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="checkindate"  HeaderText="Date In" />  
                    <asp:BoundField DataField="checkoutdate"  HeaderText="Date Out" /> 
                    <asp:BoundField DataField="checkintime"  HeaderText="Time In" SortExpression="checkintime" />  
                    <asp:BoundField DataField="checkouttime" HeaderText="Time Out" SortExpression="checkouttime"  /> 
                    <asp:BoundField DataField="actualduration" HeaderText="Actual Duration (Hr)" ItemStyle-HorizontalAlign="Right" SortExpression="actualduration" />
                     <asp:BoundField DataField="overtimepayrequest" HeaderText="" ItemStyle-HorizontalAlign="Right" />
                    <asp:TemplateField HeaderText="" ItemStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>                             
                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/TimeManagement/OvertimeApprovals.aspx?id={0}",
                    HttpUtility.UrlEncode(Eval("id").ToString())) %>' onclick="window.open (this.href, 'popupwindow',  'width=650,height=550,scrollbars,resizable'); return false;"
                                        Text='Overtime' />
                        </ItemTemplate>
                    </asp:TemplateField>
                 
                    </Columns>
                    <HeaderStyle BackColor="white" ForeColor="#1BA691" HorizontalAlign="center" />
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
            <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
             <asp:Button ID="btnSubmit" runat="server" BackColor="White" 
                BorderStyle="None" />
        </div>
            </asp:View>
        </asp:MultiView>
       
    
      </div></div>
       </div>
        </form>
    </body>
    </html>
      
</asp:Content>

