<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="false"
    CodeBehind="TrainingParticipants.aspx.vb" Inherits="GOSHRM.TrainingParticipants"
    EnableEventValidation="false" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <head>
        <title></title>
    </head>
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
    <body>
        <form id="form1">
        <div class="row">
            <div>
                <div id="divalert" runat="server" visible="false" class="alert alert-info">
                    <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <strong
                        id="msgalert" runat="server">Danger!</strong>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-8">
                <h5 id="pagetitle" runat="server" class="page-title" style="color: #1BA691">
                </h5>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-3 col-md-1 col-xs-6">
                <button id="btBack" type="button" runat="server" class="btn btn-primary btn-danger" onserverclick="btnClose_Click"
                    style="height: 30px; width: 100px">
                    Back
                </button>
            </div>
        </div>
        <div class="row">
            <div>
                <div class="panel panel-success">
                    <div class="panel-heading">
                        <b>Trainers</b>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="table-responsive">
                                <asp:GridView ID="gridTrainers" runat="server" BorderStyle="Solid" Font-Names="Verdana"
                                    AllowPaging="True" PageSize="10" DataKeyNames="id" Width="100%" Height="50px"
                                    ToolTip="click row to select record" Font-Size="12px" ShowHeaderWhenEmpty="True"
                                    EmptyDataText="No data to display" AutoGenerateColumns="False" GridLines="Both"
                                    ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC" CssClass="table table-condensed">
                                    <RowStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundField DataField="ID" ItemStyle-Width="5%" HeaderText="Row" />
                                        <asp:BoundField DataField="Trainers" HeaderText="Trainer" />
                                    </Columns>
                                    <HeaderStyle BackColor="White" ForeColor="#1BA691" HorizontalAlign="center" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div>
                <div class="panel panel-success">
                    <div class="panel-heading">
                        <b>Trainees</b>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="search-box-wrapper col-sm-6 col-md-3 col-xs-12 form-group pull-right">
                                <input id="search" style="width:100%" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                                <button onserverclick="btnFind_Click" id="btsearch" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                            </div>
                        </div>
                        <div class="row">
                            <div class="table-responsive">
                                <asp:GridView ID="gridTrainees" runat="server" OnSorting="SortRecords" AllowSorting="True"
                                    BorderStyle="Solid" Font-Names="Verdana" AllowPaging="True" PageSize="50" DataKeyNames="id"
                                    Width="100%" Height="50px" ToolTip="click row to select record" Font-Size="12px"
                                    ShowHeaderWhenEmpty="True" EmptyDataText="No data to display" AutoGenerateColumns="False"
                                    GridLines="Both" ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC"
                                    CssClass="table table-condensed">
                                    <RowStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundField DataField="Rows" HeaderText="Row" SortExpression="rows" />
                                        <asp:BoundField DataField="Trainees" HeaderText="Trainees" SortExpression="Trainees" />
                                        <asp:BoundField DataField="Status" HeaderText="Attendance" SortExpression="Status" />
                                        <asp:TemplateField HeaderText="" ItemStyle-Font-Bold="true">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#String.Format("~/Module/Employee/TrainingPortal/EmployeeTrainingAssessment?assessid={0}",
                                 HttpUtility.UrlEncode(Eval("id").ToString())) %>' Text='Training Assessment' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-Font-Bold="true">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl='<%#String.Format("~/Module/Trainings/Portal/ApplicationAssessment?id={0}",
                                 HttpUtility.UrlEncode(Eval("id").ToString())) %>' Text='<%# Eval("reviewstat")%>'/>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-Width="100px">
                                        <ItemTemplate>
                                            <p class="m-b-5"><span id="datscore" runat="server" class="text-success pull-right"><%# Eval("score")%>%</span></p>
                                            <div class="progress progress-xs  m-b-0">
                                                            <div id="datprogress" runat="server" class="progress-bar progress-bar-success" role="progressbar" data-toggle="tooltip" title="40%" style="width: 40%"></div>
                                                        </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle BackColor="White" ForeColor="#1BA691" HorizontalAlign="center" />
                                </asp:GridView>
                                <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
                                <script type="text/javascript">
                                    $(function () {
                                        $("[id*=gridTrainees] td").hover(function () {
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
        </div>
        </form>
    </body>
    </html>
</asp:Content>
