<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="Trainings.aspx.vb"
    Inherits="GOSHRM.Trainings" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
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
    <body>
        <form id="form1">
        <div id="divalert" runat="server" visible="false" class="alert alert-info">
            <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <strong
                id="msgalert" runat="server">Danger!</strong>
        </div>
        <div class="row">
        </div>
        <div class="panel panel-success">
                <div class="panel-heading">
                    <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Always">
                    <ContentTemplate>
                       <h5><b id="divdetailheader" runat="server"></b></h5>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cboview" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="cboyear" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
                </div>
                <div class="panel-body">
        <div class="row">
            <div class="search-box-wrapper col-sm-6 col-md-3 col-xs-12 form-group pull-right">
                    <input id="search" style="width:100%" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                    <button onserverclick="btnFind_Click" id="btnsearch" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                </div>
            <div class="col-sm-6 col-md-4 col-xs-12 pull-right">
                <telerik:RadComboBox runat="server" RenderMode="Lightweight"
                    ResolvedRenderMode="Classic" Width="100%" ID="cboview" AutoPostBack="True" Filter="Contains"
                    ForeColor="#666666" Skin="Bootstrap">
                </telerik:RadComboBox>
            </div>
            <div class="col-sm-6 col-md-4 col-xs-12 pull-right">
            <asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Always">
                    <ContentTemplate>
                <telerik:RadComboBox ID="cboyear" runat="server" Width="100%" ForeColor="#666666"
                    AutoPostBack="True" Filter="Contains" Skin="Bootstrap" EmptyMessage="Select Year">
                </telerik:RadComboBox>
                </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cboview" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>           
        </div>
        <div class="row">
            <div class="table-responsive">
                <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Always">
                    <ContentTemplate>
                        <asp:GridView ID="GridVwHeaderChckbox" runat="server" OnSorting="SortRecords" AllowSorting="True"
                            BorderStyle="Solid" Font-Names="Verdana" AllowPaging="True" PageSize="100" DataKeyNames="id"
                             Width="100%" Height="50px" ToolTip="click row to select record"
                            Font-Size="12px" ShowHeaderWhenEmpty="True" EmptyDataText="No data to display"
                            AutoGenerateColumns="False" GridLines="Both" ForeColor="#666666" BorderWidth="1px"
                            BorderColor="#CCCCCC" CssClass="table table-condensed">
                            <RowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="Rows" HeaderText="Row" SortExpression="rows" />
                                <asp:BoundField DataField="course" HeaderText="Course" SortExpression="course" />
                                <asp:TemplateField HeaderText="Training Session" ItemStyle-Font-Bold="true" ItemStyle-Font-Underline="true"   SortExpression="Session Name">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDownload" Text='<%# Eval("Session Name")%>' CommandArgument='<%# Eval("id") %>'
                                            runat="server" CommandName="DrillDown" CausesValidation="false"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ScheduledTime" HeaderText="Date" SortExpression="ScheduledTime" DataFormatString="{0:dd, MMM yyyy}" >
                                </asp:BoundField>
                                <asp:BoundField DataField="trainingtime" HeaderText="Time" SortExpression="trainingtime">
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Materials" ItemStyle-Font-Bold="true" ItemStyle-Font-Underline="true" SortExpression="Materials">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#String.Format("~/Module/Employee/TrainingPortal/TrainingMaterial?sessionid={0}",
                                 HttpUtility.UrlEncode(Eval("trainingsessionid").ToString())) %>' Text='<%# Eval("Materials")%>' />
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
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cboview" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="cboyear" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
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
                </div></div>
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
        .style23
        {
            width: 275px;
        }
    </style>
</asp:Content>
