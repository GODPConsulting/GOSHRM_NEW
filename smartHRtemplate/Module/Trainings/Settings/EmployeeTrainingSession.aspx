<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="EmployeeTrainingSession.aspx.vb" Inherits="GOSHRM.EmployeeTrainingSession" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
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
        body {
            font-family: Arial;
            font-size: 10pt;
        }

        td {
            cursor: pointer;
        }

        .hover_row {
            background-color: #A1DCF2;
        }

        .style25 {
            width: 134px;
        }

        .style26 {
            width: 22px;
        }

        .style27 {
            width: 35px;
        }
    </style>

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
                            <div class="col-sm-3 col-md-3 col-xs-6">
                                <telerik:RadComboBox runat="server" Skin="Bootstrap"
                                    RenderMode="Lightweight" ResolvedRenderMode="Classic" Width="100%" ID="cboCompany"
                                    AutoPostBack="True" Font-Names="Verdana" ForeColor="#666666">
                                </telerik:RadComboBox>
                            </div>
                            <div class="search-box-wrapper col-sm-6 col-md-4 col-xs-12 form-group pull-right">
                                <button id="btnUploadFile" type="button" runat="server" class="fa fa-upload btn btn-default btn-sm"
                                    onserverclick="btnUpload_Click" data-toggle="tooltip" data-original-title="CSV File: EMPID, Status(Attended, Not-Attended, Cancelled, Scheduled)" style="margin-right: 10px; margin-left: 10px; height: 35px">
                                </button>
                                <asp:LinkButton ID="btnDelete" data-toggle="tooltip" data-original-title="Delete" Height="35px" runat="server" CssClass="btn btn-default btn-sm" OnClick="Delete" OnClientClick="Confirm()">
                            <span style="margin-top:5px" aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                                </asp:LinkButton>
                                <button id="btAdd" type="button" data-toggle="tooltip" data-original-title="Add" runat="server" class="glyphicon glyphicon-plus btn btn-default btn-sm" onserverclick="btnAdd_Click"
                                    style="margin-left: 10px; margin-right: 10px; height: 35px;">
                                </button>
                                <input id="search" style="width: 100%" runat="server" type="text" placeholder="Search..." class="search-box-input" />
                                <button onserverclick="Button1_Click" id="btsearch" runat="server" class="search-box-button"><i style="color: Black;" class="fa fa-search"></i></button>
                            </div>
                            <div class="col-md-3 col-sm-6 col-xs-12 pull-right">
                                <input style="height: 35px;" class="form-control" type="file" id="FileUpload1" runat="server" />
                            </div>
                            <div class="col-sm-3 col-md-1 col-xs-6">
                                <asp:Button ID="btnBack" runat="server" Text="&lt;&lt; Back"
                                    ForeColor="White" Width="100px" Height="35px" CssClass="btn btn-primary btn-danger" BorderStyle="None" onserverclick="btnBack_Click"
                                    Font-Names="Verdana" Font-Size="11px" />
                            </div>
                        </div>

                        <div>
                            <asp:GridView ID="GridVwHeaderChckbox" runat="server" OnSorting="SortRecords"
                                AllowSorting="True" BorderStyle="Solid"
                                Font-Names="Verdana" AllowPaging="True" PageSize="100" DataKeyNames="id"
                                OnRowDataBound="OnRowDataBound" CssClass="table table-condensed"
                                Width="100%" Height="50px" ToolTip="click row to select record"
                                Font-Size="12px" AutoGenerateColumns="False" GridLines="Vertical" ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC" ShowHeaderWhenEmpty="True" EmptyDataText="No data to display">
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

                                    <asp:BoundField DataField="Rows" HeaderText="Rows" />
                                    <asp:TemplateField HeaderText="Trainee" SortExpression="employee"
                                        ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <a href="EmployeeTrainingSessionUpdate.aspx?id=<%# Eval("id") %>"><%# Eval("Employee")%></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Training Session" HeaderText="Training & Development Session" SortExpression="Training Session" />
                                    <asp:BoundField DataField="Scheduled Time" HeaderText="Scheduled Date" ItemStyle-HorizontalAlign="Center" SortExpression="Scheduled Time" />
                                    <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="status" />

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/Employee/TrainingPortal/EmployeeTrainingAssessment?assessid={0}",
                     HttpUtility.UrlEncode(Eval("id").ToString())) %>'
                                                Text='Training Assessment' />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%# string.Format("~/Module/Trainings/Portal/AssessmentMarkings.aspx?id={0}",
                     HttpUtility.UrlEncode(Eval("id").ToString())) %>'
                                                Text='<%# Eval("Comment") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl='<%# string.Format("~/Module/Trainings/Portal/ApplicationAssessment.aspx?id={0}",
                     HttpUtility.UrlEncode(Eval("id").ToString())) %>'
                                                Text='Application Assessment' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" ItemStyle-Width="100px">
                                        <ItemTemplate>
                                           <p class="m-b-5"><span id="datscore" runat="server" class="text-success pull-right"><%# Eval("score")%>%</span></p>
                                           <div class="progress progress-xs  m-b-0">
                                                            <div id="datprogress" runat="server" class="progress-bar progress-bar-success" role="progressbar" data-toggle="tooltip" title="50%" style="width: 40%"></div>
                                                        </div>
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
                                function openWindow(code) {
                                    window.open("EmployeeTrainingSessionUpdate.aspx?id=" + code, "open_window", "width=800,height=400");
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
</asp:Content>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .style21 {
            width: 100%;
        }

        .style22 {
        }
    </style>
</asp:Content>


