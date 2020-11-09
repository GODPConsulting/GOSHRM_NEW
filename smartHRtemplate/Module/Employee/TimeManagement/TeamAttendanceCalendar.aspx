<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="TeamAttendanceCalendar.aspx.vb"
    Inherits="GOSHRM.TeamAttendanceCalendar" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
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
        function Confirm1() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to Approve?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
    <title></title>
    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you wish to continue with this action?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
     <style type="text/css">
        body
        {
            font-family: Arial;
            font-size: 10pt;
        }
        td
        {
            cursor: pointer;
        }
        .hover_row
        {
            background-color: #A1DCF2;
        }
        .style31
        {
            width: 245px;
        }
    </style>
    <body>
        <form id="form1">
        <div class ="container col-md-12">
        <div class="row">
            <div id="divalert" runat="server" visible="false" class="alert alert-info">
                <strong id="msgalert" runat="server">Danger!</strong>
            </div>             
        </div>
        <div class="panel panel-success">
                <div class="panel-heading">
                    <h5><b id="pagetitle" runat="server"></b></h5>
                </div>
             <div class="panel-body">
       
                <div class="row">
                    <table width="100%">
                        <tr>
                            <td>
                            </td>
                            <td align="center">
                                <asp:Calendar ID="MyCalendar" runat="server" BackColor="White" BorderColor="#FFCC66"
                                    BorderWidth="1px" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt"
                                    ForeColor="#663399" Height="300px" ShowGridLines="True" Width="400px">
                                    <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Height="1px" 
                                        HorizontalAlign="Center" />
                                    <DayStyle HorizontalAlign="Center" Font-Bold="True" />
                                    <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
                                    <OtherMonthDayStyle ForeColor="#CC9966" />
                                    <SelectedDayStyle BackColor="Black" Font-Bold="True" />
                                    <SelectorStyle BackColor="#FFCC66" />
                                    <TitleStyle BackColor="#999999" Font-Bold="True" Font-Size="9pt" 
                                        ForeColor="White" />
                                    <TodayDayStyle BackColor="#999999" ForeColor="White" />
                                    <WeekendDayStyle ForeColor="Red" />
                                </asp:Calendar>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="row">
                    <div>
                        <div class="search-box-wrapper col-sm-6 col-md-5 col-xs-12 form-group pull-right"> 
                            <asp:button class="btn btn-success glyphicon glyphicon-ok "  Text="Approve"  runat="server" id="btapprove" data-toggle="tooltip" data-original-title="Approve Overtime" OnClick="Approve" OnClientClick="Confirm1()"></asp:button>
                            <input id="txtsearch" style="width:100%" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                            <button onserverclick="Button1_Click" id="btnsearch" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                        </div>
                     </div>
                    <div>
                        <asp:GridView ID="GridVwHeaderChckbox" runat="server" OnSorting="SortRecords" AllowSorting="True"
                            BorderStyle="Solid" Font-Names="Verdana" AllowPaging="True" PageSize="500" DataKeyNames="id"
                            OnRowDataBound="OnRowDataBound" Width="100%" EmptyDataText="No data to display" ToolTip="click row to select record"
                            Font-Size="11px" AutoGenerateColumns="False" GridLines="Vertical" ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC"
                             ShowHeaderWhenEmpty="True" CssClass="table table-condensed">
                            <RowStyle BackColor="White" />
                            <Columns>
                                 <asp:TemplateField ItemStyle-HorizontalAlign="left" ItemStyle-VerticalAlign="Top" >
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="CheckAllEmp(this);" />
                                        </HeaderTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5px" />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkEmp" runat="server"></asp:CheckBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                <asp:BoundField DataField="Rows" ItemStyle-Width="5px" HeaderText="Row" />
                                <asp:BoundField DataField="empid" ItemStyle-Width="50px" HeaderText="Emp ID" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="Name" ItemStyle-Width="100px" HeaderText="Name" />
                                <asp:BoundField DataField="shiftname" ItemStyle-Width="100px" HeaderText="Shift" />
                                <asp:BoundField DataField="duration" ItemStyle-Width="20px" HeaderText="Duration (Hr)"
                                    ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="checkindate" ItemStyle-Width="100px" HeaderText="Date In"
                                    ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="checkoutdate" ItemStyle-Width="100px" HeaderText="Date Out"
                                    ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="checkintime" ItemStyle-Width="50px" HeaderText="Time In"
                                    ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="checkouttime" ItemStyle-Width="50px" HeaderText="Time Out"
                                    ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="actualduration" ItemStyle-Width="20px" HeaderText="Actual Duration (Hr)"
                                    ItemStyle-HorizontalAlign="Right" />
                               
                               
                                <asp:TemplateField HeaderText="Request Status" ItemStyle-Width="30px" ItemStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>                             
                                         <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/Employee/TimeManagement/OvertimeApproval.aspx?id={0}",
                             HttpUtility.UrlEncode(Eval("id").ToString())) %>' onclick="window.open (this.href, 'popupwindow',  'width=700,height=600,scrollbars,resizable'); return false;"
                                                    Text='<%#Eval("overtimepayrequest")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="" ItemStyle-Width="30px" ItemStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>                             
                                             <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%# string.Format("~/Module/Employee/TimeManagement/EmployeeShiftHistory.aspx?empid={0}",
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
                    </div>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
                </div>
                </div></div>
            </div>
        </form>
    </body>
    </html>
    <div style="display:none" class="loading" align="center">
        Please wait...<br />
        <br />
        <img src="../../images/loader.gif" alt="" />
    </div>
</asp:Content>
