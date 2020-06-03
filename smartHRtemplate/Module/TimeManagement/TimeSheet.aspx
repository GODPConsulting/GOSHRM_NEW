<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="TimeSheet.aspx.vb"
    Inherits="GOSHRM.TimeSheet" EnableEventValidation="false" Debug="true" %>

<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%@ register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
    <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
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
        .style29
        {
            width: 178px;
        }
        .style30
        {
            width: 93px;
        }
        .button
        {
            background-color: #008CBA; /* Green */
            border: none;
            color: white;
            padding: 15px 32px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 16px;
            margin: 4px 2px;
            cursor: pointer;
        }
        .style31
        {
            width: 533px;
        }
        .style32
        {
            width: 233px;
        }
        .style33
        {
            width: 129px;
        }
        .style34
        {
            width: 354px;
        }
    </style>
    <body style="">
        <form id="form1">
        <div style="width: 100%">
            <table width="100%">
                <tr style="width: 100%">
                    <td class="style22" style="width: 100%">
                       <%-- <asp:Label ID="lblView" runat="server" Font-Names="Candara" Font-Size="Medium" Width="100%"
                            Style="font-weight: 700; color: #FF6600"></asp:Label>--%>

                            <div class="row">
                                <div id="divalert" runat="server" visible="false" class="alert alert-info">
                                    <strong id="msgalert" runat="server">Danger!</strong>
                                </div>
                            </div>
                    </td>
                </tr>
            </table>
            <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        </div>
      
        <div>
        <div class="panel panel-success">
                <div class="panel-heading">
                    <h5><b id="pagetitle" runat="server"></b></h5>
                </div>
             <div class="panel-body">
            <div>
                <div>
                    <table>
                        <tr>
                            <td>
                            </td>
                            <td class="style32">
                                <asp:RadioButtonList ID="rdoViewList" runat="server" RepeatDirection="Horizontal"
                                    Width="330px" AutoPostBack="True" Font-Names="Verdana" Font-Size="11px" 
                                    ForeColor="#666666">
                                    <asp:ListItem Value="1">Calendar View</asp:ListItem>
                                    <asp:ListItem Value="0">Grid View</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                    <asp:View ID="View1" runat="server">
                        <div>
                            <div>
                                <%--<table>
                                    <tr>
                                        <td class="style30">
                                            <asp:Label ID="Label1" runat="server" Text="Project" Font-Names="Verdana" 
                                                Font-Size="11px" ForeColor="#666666" Font-Bold="True"></asp:Label>
                                        </td>
                                        <td class="style31">
                                            <telerik:RadComboBox ID="cboProject" runat="server" Font-Names="Verdana" 
                                                Font-Size="11px" ForeColor="#666666" ResolvedRenderMode="Classic" Width="100%">
                                            </telerik:RadComboBox>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtsearch" runat="server" BorderColor="Silver" 
                                                BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="11px" 
                                                ForeColor="#666666" Height="20px" Style="margin-left: 0px" TextMode="Search" 
                                                Width="251px"></asp:TextBox>
                                        </td>
                                        <td>
                                        <asp:Button ID="btnFind" runat="server" BackColor="#1BA691" BorderStyle="None" ForeColor="White"
                                            Height="20px" Text="View" Width="100px" Font-Names="Verdana" Font-Size="11px">
                                        </asp:Button>
                                    </td>

                                    <td>
                                        <asp:Button ID="btnApply" runat="server" BackColor="#1BA691" BorderStyle="None" ForeColor="White"
                                            Height="20px" Text="Add Time Sheet" Width="100px" Font-Names="Verdana" 
                                            Font-Size="11px">
                                        </asp:Button>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnDelete" runat="server" BackColor="#FF3300" BorderStyle="None"
                                            ForeColor="White" Height="20px" OnClick="Delete" OnClientClick="Confirm()" Text="Delete"
                                            Width="100px" Font-Names="Verdana" Font-Size="11px"></asp:Button>
                                    </td>
                                    </tr>
                                </table>--%>
                                <div class="row">                               
                                    <div class="search-box-wrapper col-sm-6 col-md-4 col-xs-12 form-group pull-right">
                                        <asp:LinkButton ID="btnDelete" data-toggle="tooltip" data-original-title="Delete" Height="35px" runat="server" CssClass="btn btn-default btn-sm" OnClick="Delete" OnClientClick="Confirm()">
                                            <span style="margin-top:5px" aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                                        </asp:LinkButton>
                                         <button id="btAdd" type="button" data-toggle="tooltip" data-original-title="Add TimeSheet" runat="server" class="glyphicon glyphicon-plus btn btn-default btn-sm" onserverclick="btnApply_Click"
                                        style="margin-left:10px;margin-right:10px;height: 35px;"></button> 
                                    <input id="txtsearch" style="width:100%" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                                    <button onserverclick="Button1_Click" id="btsearch" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                                </div>
                                <div class="col-sm-6 col-md-3 col-xs-12 pull-right">
                                    <telerik:RadComboBox ID="cboProject" runat="server" Skin="Bootstrap" Font-Names="Verdana" 
                                                Font-Size="12px" ForeColor="#666666" ResolvedRenderMode="Classic" Width="100%">
                                            </telerik:RadComboBox>
                                </div>   
                                </div>                                        
                            </div>
                         
                        </div>
                  
                        <div>
                            <asp:GridView ID="GridVwHeaderChckbox" runat="server" AllowPaging="True" AllowSorting="True"
                                AutoGenerateColumns="False" GridLines="Vertical" ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC" BorderStyle="Solid" DataKeyNames="id" Font-Names="Verdana"
                                Font-Size="12px" Height="50px" OnRowDataBound="OnRowDataBound" OnSorting="SortRecords"
                                PageSize="500" ShowHeaderWhenEmpty="True" EmptyDataText="No data to display" ToolTip="click row to select record"
                                Width="100%" CssClass="table table-condensed">
                                <RowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="CheckAllEmp(this);" /></HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkEmp" runat="server" /></ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="1%"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Rows" HeaderText="Row">
                                        <ItemStyle Width="5%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Project" HeaderText="Project">
                                        
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Activity">
                                        <ItemTemplate>
                                            <a href="#" onclick='openWindow("<%# Eval("id") %>");'>
                                                <%# Eval("Activity")%></a></ItemTemplate>
                                        
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Activity Date" HeaderText="Activity Date">
                                        
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Activity End Date" HeaderText="Activity End Date">
                                        
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Start Time" HeaderText="Start Time">
                                        
                                    </asp:BoundField>
                                    <asp:BoundField DataField="End Time" HeaderText="End Time">
                                        
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PM Status" HeaderText="PM Approval Status">
                                        
                                    </asp:BoundField>
                                    <asp:BoundField DataField="HRStatus" HeaderText="HR Approval Status">
                                        
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Status" HeaderText="Approval Status">
                                        
                                    </asp:BoundField>
                                </Columns>
                               <HeaderStyle BackColor="White" ForeColor="#1BA691" HorizontalAlign="center" />
                            </asp:GridView>
                            <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js" type="text/javascript">

                            </script>
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

                                function openWindow(code) {
                                    window.open("TimeSheetUpdate.aspx?id=" + code, "open_window", "width=750,height=650");
                                }
                            </script>
                        </div>
                    </asp:View>
                    <asp:View ID="View2" runat="server">
                        <div>
                            <asp:Button ID="btnApply0" runat="server" BackColor="#1BA691" BorderStyle="None"
                                ForeColor="White" Height="20px" Text="Add Time Sheet" Width="100px" Font-Names="Verdana"
                                Font-Size="11px" />
                            <br />
                        </div>
                        <div style="border: thin solid #C0C0C0">
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        <telerik:RadScheduler ID="schTmeSheet" runat="server" AllowDelete="False" 
                                            AllowEdit="False" AllowInsert="False" AppointmentStyleMode="Simple" 
                                            BackColor="#66CCFF" CustomAttributeNames="Activity" 
                                            EditFormDateFormat="dd/MM/yyyy" EditFormTimeFormat="HH:mm" 
                                            EnableDescriptionField="True" Height="561px" RenderMode="Lightweight" 
                                            ResolvedRenderMode="Classic" SelectedDate="2016-09-08" SelectedView="MonthView" 
                                            Width="100%">
                                            <exportsettings>
                                                <pdf pagebottommargin="1in" pageleftmargin="1in" pagerightmargin="1in" 
                                                    pagetopmargin="1in" />
                                            </exportsettings>
                                            <AdvancedForm Modal="True" />
                                            <TimelineView UserSelectable="False" />
                                            <WeekView UserSelectable="False" />
                                            <DayView UserSelectable="False" />
                                            <MonthView VisibleAppointmentsPerDay="20" />
                                            <appointmenttemplate>
                                                <asp:Label ID="lblSubject" runat="server" Text='<%# Eval("Subject") %>' />
                                                <br />
                                            </appointmenttemplate>
                                        </telerik:RadScheduler>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </asp:View>
                </asp:MultiView>
            </div>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
        </div></div></div>
        </form>
    </body>
    </html>
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
