<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="false"
    CodeBehind="successionplanupdate.aspx.vb" Inherits="GOSHRM.successionplanupdate"
    EnableEventValidation="false" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title></title>
        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
            <script type="text/javascript">

                function closeWin() {
                    popup.close();   // Closes the new window
                }
            </script>
        </telerik:RadCodeBlock>
        <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
            <script type="text/javascript" language="javascript">
                //    Grid View Check box
                function CheckAllEmp(Checkbox) {
                    var GridVwHeaderChckbox = document.getElementById("<%=GridVwHeaderChckbox.ClientID %>");
                    for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                        GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
                    }
                }
            </script>
        </telerik:RadCodeBlock>
        <telerik:RadCodeBlock ID="RadCodeBlock3" runat="server">
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
        </telerik:RadCodeBlock>
    </head>
    <body>
        <form id="form1">
            <div class="container">
                <div class="row">
                    <div class="col-md-8">
                        <div id="divalert" runat="server" visible="false" class="alert alert-info">
                            <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <strong
                                id="msgalert" runat="server">Danger!</strong>
                            <asp:Label ID="lblID" runat="server" Visible="False" Font-Size="1px"></asp:Label>
                            <asp:UpdatePanel runat="server" ID="UpdatePanel4" UpdateMode="Always">
                                <ContentTemplate>
                                    <asp:Label ID="lblhodid" runat="server" Visible="False" Font-Size="1px"></asp:Label>
                                    <asp:Label ID="lblmanagerid" runat="server" Visible="False" Font-Size="1px"></asp:Label>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="cboEmployee" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-8">
                        <h5 id="pagetitle" runat="server" class="page-title" style="color: #1BA691">Succession Plan</h5>
                    </div>
                </div>
                <div class="row col-md-8">
                    <div id="divapprovalview" runat="server" class="col-xs-4 text-right m-b-30 pull-right" visible="false">
                        <button id="approvallink" runat="server" onserverclick="lnkApprovalStat_Click" type="submit"
                            class="btn btn-primary btn-success " title="view approvals status">
                            View Approval Status
                        </button>
                    </div>
                    <div id="divapprovalupdate" runat="server" class="col-xs-4 text-right m-b-30 pull-right" visible="false">
                        <button id="Button1" runat="server" onserverclick="lnkupdatestatus_Click" type="submit"
                            class="btn btn-primary btn-success " title="update approvals status">
                            Update Approval Status
                        </button>
                    </div>

                </div>
                <div class="row">
                    <div class="col-md-8">
                        <div class="form-group">
                            <label>
                                EMPLOYEE *</label>
                            <telerik:RadComboBox ID="cboEmployee" runat="server" ForeColor="#666666" resolvedrendermode="Classic"
                                Filter="Contains" Width="100%" AutoPostBack="True" RenderMode="Lightweight" Skin="Bootstrap"
                                EmptyMessage="-- Select Employee --">
                            </telerik:RadComboBox>
                        </div>
                    </div>
                </div>

                <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Always">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-md-8">
                                <div class="panel panel-success">
                                    <div class="panel-heading">
                                        <b></b>
                                    </div>
                                    <div class="panel-body">
                                        <div class="col-md-12">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <label>
                                                           RECENT PERFORMANCE RATING</label>
                                                        <input id="aperformancerating" runat="server" class="form-control" type="text" disabled="disabled" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>
                                                            JOB TITLE</label>

                                                        <input id="ajobtitle" runat="server" class="form-control" type="text" disabled="disabled" />

                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>
                                                            JOB GRADE</label>
                                                        <input id="ajobgrade" runat="server" class="form-control" type="text" disabled="disabled" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="divcompany" runat="server" class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <label>
                                                            COMPANY</label>
                                                        <input id="acompany" runat="server" class="form-control" type="text" disabled="disabled" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <label>
                                                            OFFICE</label>
                                                        <input id="aoffice" runat="server" class="form-control" type="text" disabled="disabled" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <textarea id="alocation" runat="server" class="form-control" rows="2" cols="1" readonly="readonly" visible="false"></textarea>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cboEmployee" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
                <%--End of Panel--%>
                <div class="row">
                    <div class=" col-md-8">
                        <div class="panel panel-success">
                            <div class="panel-heading">
                                <b>PLANNED POSITION</b>
                            </div>
                            <div class="panel-body">
                                <div class=" col-md-12">
                                    <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Always">
                                        <ContentTemplate>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <label>
                                                            JOB TITLE *</label>
                                                        <telerik:RadComboBox ID="radplanjobtitle" runat="server" ForeColor="#666666" resolvedrendermode="Classic"
                                                            Filter="Contains" Width="100%" RenderMode="Lightweight" Skin="Bootstrap" AutoPostBack="true"
                                                            EmptyMessage="-- Select Job Title --">
                                                        </telerik:RadComboBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Always">
                                                <ContentTemplate>
                                                    <div id="collapse_acc" runat="server" class=" card-box">
                                                        <div class="card-header">
                                                            <h6><a class="collapsed card-link" data-toggle="collapse" href="#collapseTwo" title="Click to view">Job Skills
                                                            </a></h6>
                                                        </div>
                                                        <div id="collapseTwo" class="collapse" data-parent="#accordion">
                                                            <div class="card-body">
                                                                <div class="panel panel-success">
                                                                    <div class="panel-heading">
                                                                        <b id="B1" runat="server"></b>
                                                                    </div>
                                                                    <div class="panel-body">
                                                                        <asp:DataList ID="gridAcquire" runat="server" Width="100%" RepeatColumns="1" CellSpacing="1"
                                                                            RepeatLayout="Table" Font-Names="Arial" Font-Size="15px" GridLines="Both" DataKeyField="id"
                                                                            BorderColor="#CCCCCC" ForeColor="#666666" BorderStyle="Solid"
                                                                            BorderWidth="1px">
                                                                            <ItemTemplate>
                                                                                <table class="table" width="100%">
                                                                                    <tr>
                                                                                        <td valign="top" style="width: 100%">
                                                                                            <p class="m-b-5"><%# Eval("skills")%> <span id="datscore" runat="server" class="text-success pull-right"><%# Eval("rating")%>%</span></p>
                                                                                            <div class="progress progress-xs  m-b-0">
                                                                                                <div id="datprogress" runat="server" class="progress-bar progress-bar-success" role="progressbar" data-toggle="tooltip" title="40%" style="width: 40%"></div>
                                                                                            </div>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </ItemTemplate>
                                                                        </asp:DataList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="radplanjobtitle" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>


                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <label>
                                                            JOB GRADE *</label>
                                                        <telerik:RadComboBox ID="radplanjobgrade" runat="server" ForeColor="#666666" resolvedrendermode="Classic"
                                                            Filter="Contains" Width="100%" RenderMode="Lightweight" Skin="Bootstrap" AutoPostBack="true"
                                                            EmptyMessage="-- Select Job Grade --">
                                                        </telerik:RadComboBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <asp:UpdatePanel runat="server" ID="UpdatePanel5" UpdateMode="Always">
                                                <ContentTemplate>
                                                    <div id="collapse_acc2" runat="server" class=" card-box">
                                                        <div class="card-header">
                                                            <h6><a class="collapsed card-link" data-toggle="collapse" href="#collapseT" title="Click to view">Performance Metrics
                                                            </a></h6>
                                                        </div>
                                                        <div id="collapseT" class="collapse" data-parent="#accordion">
                                                            <div class="card-body">
                                                                <div class="panel panel-success">
                                                                    <div class="panel-heading">
                                                                        <b id="B2" runat="server"></b>
                                                                    </div>
                                                                    <div class="panel-body">
                                                                        <asp:DataList ID="gridPMS" runat="server" Width="100%" RepeatColumns="1" CellSpacing="1"
                                                                            RepeatLayout="Table" Font-Names="Arial" Font-Size="15px" GridLines="Both" DataKeyField="id"
                                                                            BorderColor="#CCCCCC" ForeColor="#666666" BorderStyle="Solid"
                                                                            BorderWidth="1px">
                                                                            <ItemTemplate>
                                                                                <table class="table" width="100%">
                                                                                    <tr>
                                                                                        <td valign="top" style="width: 100%">
                                                                                            <p class="m-b-5"><b><%# Eval("CompetencyType")%> </b><span id="datscore2" runat="server" class="text-success pull-right"><%# Eval("weight")%>%</span></p>
                                                                                            <p class="m-b-5"><%# Eval("Competencies")%> </p>
                                                                                            <div class="progress progress-xs  m-b-0">
                                                                                                <div id="datprogress2" runat="server" class="progress-bar progress-bar-success" role="progressbar" data-toggle="tooltip" title="40%" style="width: 40%"></div>
                                                                                            </div>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </ItemTemplate>
                                                                        </asp:DataList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="radplanjobgrade" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>

                                            <div id="divplancompany" runat="server" class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <label>
                                                            COMPANY *</label>
                                                        <telerik:RadComboBox ID="cboplancompany" runat="server" ForeColor="#666666" resolvedrendermode="Classic"
                                                            Filter="Contains" Width="100%" RenderMode="Lightweight" Skin="Bootstrap"
                                                            AutoPostBack="True" EmptyMessage="-- Select Company --">
                                                        </telerik:RadComboBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <label>
                                                            OFFICE *</label>
                                                        <asp:UpdatePanel runat="server" ID="UpdatePanel6" UpdateMode="Always">
                                                            <ContentTemplate>
                                                                <telerik:RadComboBox ID="radplanoffice" runat="server" ForeColor="#666666" resolvedrendermode="Classic"
                                                                    Filter="Contains" Width="100%" RenderMode="Lightweight" Skin="Bootstrap"
                                                                    EmptyMessage="-- Select Office --">
                                                                </telerik:RadComboBox>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="cboplancompany" EventName="SelectedIndexChanged" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <label>
                                                            LOCATION *</label>
                                                        <telerik:RadComboBox ID="radplanlocation" runat="server" ForeColor="#666666" resolvedrendermode="Classic"
                                                            Filter="Contains" Width="100%" RenderMode="Lightweight" Skin="Bootstrap"
                                                            EmptyMessage="-- Select Location --">
                                                        </telerik:RadComboBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="cboEmployee" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label>
                                                    SUCCESSION STATUS</label>
                                                <telerik:RadComboBox ID="radstatus" runat="server" ForeColor="#666666" resolvedrendermode="Classic"
                                                    Width="100%" RenderMode="Lightweight" Skin="Bootstrap">
                                                </telerik:RadComboBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label>
                                                    COMMENT</label>
                                                <textarea id="acomment" runat="server" class="form-control" rows="4" cols="1" placeholder="Comment"></textarea>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="m-t-20">
                                            <button id="btsave" runat="server" onserverclick="btnSend_Click" type="submit"
                                                class="btn btn-primary btn-success">
                                                Save & Update</button>
                                            <button id="btclose" runat="server" onserverclick="btCancel_Click" type="submit"
                                                class="btn btn-primary btn-info ">
                                                << Back</button>
                                            <button id="btcomplete" runat="server" onserverclick="btnNotify_Click" type="submit"
                                                class="btn btn-primary btn-success ">
                                                Send Notification</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="divdetail" runat="server" class="row">
                    <div class=" col-md-10">
                        <div class="panel panel-success">
                            <div class="panel-heading">
                                <b>Succession Plan Breakdown</b>
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="search-box-wrapper col-sm-6 col-md-3 col-xs-12 form-group pull-right">
                                        <input id="search" style="width: 100%" runat="server" type="text" placeholder="Search..." class="search-box-input" />
                                        <button id="btnsearch" runat="server" class="search-box-button"><i style="color: Black;" class="fa fa-search"></i></button>
                                    </div>
                                    <div id="divbtndetail" runat="server" class="col-sm-6 col-md-3 col-xs-12 pull-right">
                                        <button id="btadddetail" runat="server" onserverclick="btnAdd_Click" type="submit"
                                            style="width: 100px" class="btn btn-primary btn-success " title="add detail to budget">
                                            Add Detail</button>
                                        <asp:Button ID="btdeletedetail" runat="server" Text="Delete Detail"
                                            OnClientClick="Confirm()" BackColor="#FF3300" ForeColor="White" Width="100px"
                                            Height="34px" CssClass="btn btn-primary btn-danger " BorderStyle="None" Font-Names="Verdana"
                                            Font-Size="13px" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div>

                                        <div class="table-responsive">
                                            <asp:GridView ID="GridVwHeaderChckbox" runat="server" AllowSorting="True"
                                                BorderStyle="Solid" Font-Names="Verdana" AllowPaging="True" PageSize="5" DataKeyNames="id"
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
                                                    <asp:TemplateField HeaderText="Action" ItemStyle-Font-Bold="true" ItemStyle-Font-Underline="true" SortExpression="action">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDownload" Text='<%# Eval("action")%>' CommandArgument='<%# Eval("id") %>'
                                                                runat="server" OnClick="viewdetail"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="expectedstartdate" HeaderText="Target Start Date" SortExpression="expectedstartdate" DataFormatString="{0:dd, MMM yyyy}" />
                                                    <asp:BoundField DataField="expectedenddate" HeaderText="Target Due Date" SortExpression="expectedenddate" DataFormatString="{0:dd, MMM yyyy}" />
                                                    <asp:BoundField DataField="status" HeaderText="Status" SortExpression="status" />
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
                        </div>
                    </div>
                </div>
            </div>

        </form>
    </body>
    </html>
</asp:Content>
