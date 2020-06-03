<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="LeaveRoster.aspx.vb"
    Inherits="GOSHRM.LeaveRoster" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="DayPilot" Namespace="DayPilot.Web.Ui" TagPrefix="DayPilot" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerikcontrol" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" src="../../../js/modal.js"></script>
    <script type="text/javascript" language="javascript">
        //    Grid View Check box
        function CheckAllEmp(Checkbox) {
            var GridVwHeaderChckbox = document.getElementById("<%=GridVwHeaderChckbox.ClientID %>");
            for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }
    </script>

    <script type="text/javascript">
        var modal = new DayPilot.Modal();
        modal.border = "10px solid #ccc";
        modal.closed = function () {
            if (this.result == "OK") {
                dps.commandCallBack('refresh');
            }
        };

        function createEvent(start, end, resource) {

        }

        function edit(id) {
            window.location = "leavetoapprove?id=" + id;
        }

        function afterRender(data) {
        };

    </script>

    <script type="text/javascript">
        function ConfirmApprove() {
            var confirm_app = document.createElement("INPUT");
            confirm_app.type = "hidden";
            confirm_app.name = "confirm_app";
            if (confirm("Approve the selected leaves with pending approval?")) {
                confirm_app.value = "Yes";
                ShowProgress();
            } else {
                confirm_app.value = "No";
            }
            document.forms[0].appendChild(confirm_app);
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
        .modal {
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

        .loading {
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

    <html xmlns="http://www.w3.org/1999/xhtml">
    <title></title>
    <link href="../../../css/main.css" type="text/css" rel="stylesheet" />
    <link href="../../../GridTheme/scheduler_white.css" type="text/css" rel="stylesheet" />



    <style type="text/css">
        .style1 {
            width: 99px;
        }

        .style2 {
            width: 163px;
        }

        .style3 {
            width: 83px;
        }

        .style22 {
            cursor: pointer;
        }
    </style>
    <body>
        <form id="form1">
            <div class="container col-md-12">
                <div class="content">
                    <div id="divalert" runat="server" visible="false" class="alert alert-info">
                        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                        <strong id="msgalert" runat="server">Danger!</strong>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-8">
                        <h4 id="pagetitle" runat="server" class="page-title">Leave Roaster</h4>
                    </div>
                </div>
                <div id="divsummary" runat="server" class="row">
                    <div class="panel panel-success">
                        <div class="panel-body">
                            <div>
                                <div class="panel-heading">
                                    <asp:Label ID="lblLeaveSummary" runat="server" Font-Names="Verdana" Font-Size="15px" Width="100%" Style="text-align: center;" Font-Bold="True">Leave Summary</asp:Label>
                                    <br />
                                </div>
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gridLeaveChart" runat="server"
                                            BorderStyle="Solid" Font-Names="sans-serif" AllowPaging="True" PageSize="20"
                                            Width="100%" Height="50px" Font-Size="12px"
                                            ShowHeaderWhenEmpty="True" EmptyDataText="No data to display" AutoGenerateColumns="False"
                                            GridLines="Both" ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC"
                                            CssClass="table table-condensed">
                                            <RowStyle BackColor="White" />
                                            <Columns>
                                                <asp:BoundField DataField="Rows" ItemStyle-Width="5%" HeaderText="Rows" SortExpression="rows" />
                                                <asp:BoundField DataField="LeaveType" HeaderText="Leave Type" />
                                                <asp:BoundField DataField="LeavesPerYear" HeaderText="Entitled Leave Days" ItemStyle-HorizontalAlign="Right" />
                                                <asp:BoundField DataField="ApprovedDays" HeaderText="Approved Leave" ItemStyle-HorizontalAlign="Right" />
                                                <asp:BoundField DataField="casual" HeaderText="Casual (Annual) Approved" ItemStyle-HorizontalAlign="Right" />
                                                <asp:BoundField DataField="Balance" HeaderText="Current Year Balance" ItemStyle-HorizontalAlign="Right" />
                                                <asp:BoundField DataField="PreviousBalance" HeaderText="Previous Years Balance" ItemStyle-HorizontalAlign="Right" />
                                                <asp:BoundField DataField="totalbalance" HeaderText="Total Balance" ItemStyle-HorizontalAlign="Right" />
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <span class="label label-warning-border"><a href="LeaveDetails.aspx?leaveid=<%# Eval("leaveid") %>&balances=<%# Eval("totalbalance") %>">
                                                            <%# Eval("ApplyLeave")%></a></span>

                                                    </ItemTemplate>
                                                    <ItemStyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>



                                            </Columns>
                                            <HeaderStyle BackColor="White" ForeColor="#1BA691" HorizontalAlign="center" />
                                        </asp:GridView>
                                        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
                                        <script type="text/javascript">
                                            $(function () {
                                                $("[id*=gridLeaveChart] td").hover(function () {
                                                    $("td", $(this).closest("tr")).addClass("hover_row");
                                                }, function () {
                                                    $("td", $(this).closest("tr")).removeClass("hover_row");
                                                })
                                            })
                                        </script>
                                        <script type="text/javascript">
                                            function openLeaveRequest(code, code2) {
                                                window.open("LeaveDetails?leavetype=" + code + "&balances=" + code2, "open_window", "width=700,height=600");
                                            }
                                        </script>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <button id="btAdd" type="button" runat="server" class="btn-info" onserverclick="btnApplyCasual_Click"
                        style="height: 30px">
                        Apply For Casual Leave
                    </button>
                </div>

                <div class="row">
                    <div class="row">
                        <table>
                            <tr>
                                <td>
                                    <telerikcontrol:RadComboBox ID="cboView" runat="server"
                                        ResolvedRenderMode="Classic" AutoPostBack="True"
                                        ForeColor="#666666" RenderMode="Lightweight" Skin="Bootstrap">
                                        <Items>
                                            <telerikcontrol:RadComboBoxItem runat="server" Text="Pending" Value="Pending"
                                                Owner="cboView" />
                                            <telerikcontrol:RadComboBoxItem runat="server" Text="Cancelled" Value="Cancelled"
                                                Owner="cboView" />
                                            <telerikcontrol:RadComboBoxItem runat="server" Text="Rejected"
                                                Value="Rejected" Owner="cboView" />
                                            <telerikcontrol:RadComboBoxItem runat="server" Text="Approved" Value="Approved"
                                                Owner="cboView" />
                                        </Items>
                                    </telerikcontrol:RadComboBox>
                                </td>
                                <td></td>
                                <td></td>
                            </tr>
                        </table>
                    </div>
                    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                        <asp:View ID="Roasters" runat="server">
                            <div class="panel panel-success col-md-12">
                                <div class="panel-body">
                                    <div>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="TextBox1" runat="server" BackColor="#00CC00" BorderStyle="None"
                                                        Height="10px" Width="10px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Verdana"
                                                        Font-Size="11px" Text="Approved/Taken" ForeColor="#666666"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBox2" runat="server" BackColor="Yellow" BorderStyle="None"
                                                        Height="10px" Width="10px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="Verdana"
                                                        Font-Size="11px" Text="Approval Pending" ForeColor="#666666"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBox3" runat="server" BackColor="#FF3300" BorderStyle="None"
                                                        Height="10px" Width="10px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Names="Verdana"
                                                        Font-Size="11px" Text="Rejected/Cancelled" ForeColor="#666666"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <table width="100%">
                                        <tr>
                                            <td class="style2">
                                                <telerikcontrol:RadComboBox ID="cboMonth" runat="server" ResolvedRenderMode="Classic"
                                                    AutoPostBack="True" ForeColor="#666666" RenderMode="Lightweight" Skin="Bootstrap">
                                                    <Items>
                                                        <telerikcontrol:RadComboBoxItem runat="server" Text="January" Value="1" />
                                                        <telerikcontrol:RadComboBoxItem runat="server" Text="February" Value="2" />
                                                        <telerikcontrol:RadComboBoxItem runat="server" Text="March" Value="3" />
                                                        <telerikcontrol:RadComboBoxItem runat="server" Text="April" Value="4" />
                                                        <telerikcontrol:RadComboBoxItem runat="server" Text="May" Value="5" />
                                                        <telerikcontrol:RadComboBoxItem runat="server" Text="June" Value="6" />
                                                        <telerikcontrol:RadComboBoxItem runat="server" Text="July" Value="7" />
                                                        <telerikcontrol:RadComboBoxItem runat="server" Text="August" Value="8" />
                                                        <telerikcontrol:RadComboBoxItem runat="server" Text="September" Value="9" />
                                                        <telerikcontrol:RadComboBoxItem runat="server" Text="October" Value="10" />
                                                        <telerikcontrol:RadComboBoxItem runat="server" Text="November" Value="11" />
                                                        <telerikcontrol:RadComboBoxItem runat="server" Text="December" Value="12" />
                                                    </Items>
                                                </telerikcontrol:RadComboBox>

                                            </td>
                                            <td>
                                                <telerikcontrol:RadComboBox ID="cboYear" runat="server" ResolvedRenderMode="Classic"
                                                    AutoPostBack="True" ForeColor="#666666" RenderMode="Lightweight" Skin="Bootstrap">
                                                </telerikcontrol:RadComboBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <div style="overflow-y: scroll; overflow-x: scroll;">
                                        <DayPilot:DayPilotScheduler ID="DayPilotScheduler1" runat="server" HeaderFontSize="10px"
                                            HeaderHeight="20" DataStartField="LeaveFrom" DataEndField="LeaveTo" DataTextField="Employee"
                                            DataValueField="refno" DataResourceField="Employee" EventHeight="25" EventFontSize="10px"
                                            CellDuration="1440" CellWidth="30" Days="31" BorderColor="#aaaaaa" EventBorderColor="#aaaaaa"
                                            TimeRangeSelectedHandling="JavaScript" TimeRangeSelectedJavaScript="create('{0}', null, '{1}');"
                                            EventClickHandling="JavaScript" EventClickJavaScript="edit('{0}');" OnHeaderColumnWidthChanged="DayPilotScheduler1_HeaderColumnWidthChanged"
                                            OnBeforeResHeaderRender="DayPilotScheduler1_BeforeResHeaderRender" OnBeforeEventRender="DayPilotScheduler1_BeforeEventRender"
                                            CssOnly="true" CssClassPrefix="scheduler_white" RowHeaderWidthAutoFit="true"
                                            DurationBarVisible="False" Font-Names="sans-serif" Font-Size="10px"
                                            EventFontFamily="sans-serif" HeaderFontFamily="sans-serif" HourFontFamily="Verdana"
                                            HourFontSize="10px">
                                            <HeaderColumns>
                                                <DayPilot:RowHeaderColumn Title="Employee" Width="200" />
                                            </HeaderColumns>
                                        </DayPilot:DayPilotScheduler>
                                    </div>
                                </div>
                            </div>
                        </asp:View>
                        <asp:View ID="Grid" runat="server">
                            <div class="panel panel-success col-md-12">
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-sm-6 col-md-3 col-xs-12">
                                            <div class="form-group form-focus select-focus">

                                                <telerikcontrol:RadDatePicker Width="100%" ID="dateFrom" runat="server" Culture="en-US" ResolvedRenderMode="Classic"
                                                    ForeColor="#666666" RenderMode="Lightweight" Skin="Bootstrap" AutoPostBack="True">
                                                    <Calendar EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;" UseColumnHeadersAsSelectors="False"
                                                        UseRowHeadersAsSelectors="False" Skin="Bootstrap">
                                                    </Calendar>
                                                    <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" Height="22px" LabelWidth="40%"
                                                        Width="" AutoPostBack="True">
                                                        <EmptyMessageStyle Resize="None" />
                                                        <ReadOnlyStyle Resize="None" />
                                                        <FocusedStyle Resize="None" />
                                                        <DisabledStyle Resize="None" />
                                                        <InvalidStyle Resize="None" />
                                                        <HoveredStyle Resize="None" />
                                                        <EnabledStyle Resize="None" />
                                                    </DateInput>
                                                    <DatePopupButton HoverImageUrl="" ImageUrl="" CssClass="" />
                                                </telerikcontrol:RadDatePicker>
                                            </div>
                                        </div>

                                        <div class="col-sm-6 col-md-3 col-xs-12">
                                            <div class="form-group form-focus select-focus">
                                                <telerikcontrol:RadDatePicker Width="100%" ID="dateTo" runat="server" Culture="en-US" ResolvedRenderMode="Classic"
                                                    ForeColor="#666666" RenderMode="Lightweight" Skin="Bootstrap" AutoPostBack="True">
                                                    <Calendar EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;" UseColumnHeadersAsSelectors="False"
                                                        UseRowHeadersAsSelectors="False" RenderMode="Lightweight" Skin="Bootstrap">
                                                    </Calendar>
                                                    <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" Height="22px" LabelWidth="40%"
                                                        Width="" RenderMode="Lightweight" AutoPostBack="True">
                                                        <EmptyMessageStyle Resize="None" />
                                                        <ReadOnlyStyle Resize="None" />
                                                        <FocusedStyle Resize="None" />
                                                        <DisabledStyle Resize="None" />
                                                        <InvalidStyle Resize="None" />
                                                        <HoveredStyle Resize="None" />
                                                        <EnabledStyle Resize="None" />
                                                    </DateInput>
                                                    <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                                </telerikcontrol:RadDatePicker>
                                            </div>
                                        </div>
                                        <div class="search-box-wrapper col-sm-6 col-md-3 col-xs-12 form-group pull-right">
                                            <input id="search" style="width: 100%" runat="server" type="text" placeholder="Search..." class="search-box-input" />
                                            <button onserverclick="btnFind_Click" id="btsearch" runat="server" class="search-box-button"><i style="color: Black;" class="fa fa-search"></i></button>
                                        </div>
                                        <div class="col-sm-3 col-md-2 col-xs-6">
                                            <div class="form-group">
                                                <asp:Button ID="btnApprove" runat="server" Text="Approve"
                                                    ForeColor="White" Width="100%" Height="30px" CssClass="btn btn-primary btn-success" OnClientClick="ConfirmApprove()"
                                                    ToolTip="Approve multiple leave applications"
                                                    BorderStyle="None" Font-Names="Verdana" Font-Size="12px" />

                                            </div>
                                        </div>
                                    </div>

                                    <div id="mgrview" runat="server"  class="row">
                                        <div class="table-responsive">
                                            <asp:GridView ID="GridVwHeaderChckbox" runat="server" OnSorting="SortRecords" AllowSorting="True"
                                                BorderStyle="Solid" Font-Names="sans-serif" AllowPaging="True" PageSize="200" DataKeyNames="empid, DateFrom, DateTo"
                                                Width="100%" Height="50px" ToolTip="click row to select record"
                                                Font-Size="11px" ShowHeaderWhenEmpty="True" EmptyDataText="No data to display"
                                                AutoGenerateColumns="False" GridLines="Both" ForeColor="#666666" BorderWidth="1px"
                                                BorderColor="#CCCCCC" CssClass="table table-condensed">
                                                <RowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="CheckAllEmp(this);" />
                                                        </HeaderTemplate>
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkEmp" runat="server"></asp:CheckBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Rows" HeaderText="Row" SortExpression="rows" />
                                                    <asp:TemplateField HeaderText="Name" ItemStyle-VerticalAlign="Top" SortExpression="Employee"
                                                        ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/Employee/LeaveManagement/LeaveEmployeeList?empid={0}&datefrom={1}&dateto={2}",
                     HttpUtility.UrlEncode(Eval("empid").ToString()), HttpUtility.UrlEncode(Eval("datefrom").ToString()), HttpUtility.UrlEncode(Eval("dateto").ToString()) ) %>'
                                                                Text='<%# Eval("Employee")%>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="pending" HeaderText="Pending Approval" ItemStyle-HorizontalAlign="Right" SortExpression="pending" DataFormatString="{0:n}" />
                                                    <asp:BoundField DataField="approved" HeaderText="Approved Days" ItemStyle-HorizontalAlign="Right" SortExpression="approved" DataFormatString="{0:n}" />
                                                    <asp:BoundField DataField="taken" HeaderText="Taken Days" ItemStyle-HorizontalAlign="Right" SortExpression="taken" DataFormatString="{0:n}" />
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
                                    <div id="empview" runat="server" class="row">
                                        <div class="table-responsive">
                                            <asp:GridView ID="grdEmp" runat="server" OnSorting="SortRecords" AllowSorting="True"
                                                BorderStyle="Solid" Font-Names="sans-serif" AllowPaging="True" PageSize="200" DataKeyNames="id"
                                                Width="100%" Height="50px" ToolTip="click row to select record"
                                                Font-Size="11px" ShowHeaderWhenEmpty="True" EmptyDataText="No data to display"
                                                AutoGenerateColumns="False" GridLines="Both" ForeColor="#666666" BorderWidth="1px"
                                                BorderColor="#CCCCCC" CssClass="table table-condensed">
                                                <RowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:BoundField DataField="Rows" HeaderText="Row"  />
                                                    <asp:TemplateField HeaderText="Type" ItemStyle-VerticalAlign="Top" 
                                                        ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#String.Format("~/Module/Employee/LeaveManagement/leavedetails?id={0}",
                                                         HttpUtility.UrlEncode(Eval("id").ToString())) %>'
                                                                Text='<%# Eval("LeaveType")%>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="leavefrom" HeaderText="From" ItemStyle-HorizontalAlign="Right"  DataFormatString="{0:dd, MMM yyyy}" />
                                                    <asp:BoundField DataField="leaveto" HeaderText="To" ItemStyle-HorizontalAlign="Right"  DataFormatString="{0:dd, MMM yyyy}" />
                                                    <asp:BoundField DataField="noofdays" HeaderText="Days" ItemStyle-HorizontalAlign="Right"  DataFormatString="{0:n}" />
                                                    <asp:BoundField DataField="status" HeaderText="Status"  />
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

                        </asp:View>
                    </asp:MultiView>
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
