<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="JobPostings.aspx.vb"
    Inherits="GOSHRM.JobPostings" EnableEventValidation="false" Debug="true" %>

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
    <script type="text/javascript">
        function OpenMaxWindow(file) {
            var xMax = screen.width, yMax = screen.height;
            window.open(file, 'open_window', 'scrollbars=yes,width=' + xMax + ',height=' + yMax + ',top=0,left=0,resizable=yes');
        } 
    </script>
    <body>
        <form id="form1" action="">
        <div class="row">
            <div id="divalert" runat="server" visible="false" class="alert alert-info">
                <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <strong
                    id="msgalert" runat="server">Danger!</strong>
            </div>
        </div>
        <div class="panel panel-success">
                <div class="panel-heading">
                   <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Always">
                    <ContentTemplate>
                        <h5 id="pagetitle" runat="server" class="page-title" style="color: #1BA691">
                            Head</h5>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cboCompany" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="radStatus" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
                </div>
             <div class="panel-body">
        <div class="row">
                    <div class="search-box-wrapper col-sm-6 col-md-4 col-xs-12 form-group pull-right">
                    <button id="Button1" type="button" data-toggle="tooltip" data-original-title="Job Portal" runat="server" class="glyphicon glyphicon-asterisk btn btn-default btn-sm" onserverclick="btnAdd_jobportal"
                        style="margin-left:10px;margin-right:10px;height: 35px;"></button> 
                        <asp:LinkButton ID="btnDelete" data-toggle="tooltip" data-original-title="Delete" Height="35px" runat="server" CssClass="btn btn-default btn-sm" OnClick="Delete" OnClientClick="Confirm()">
                            <span style="margin-top:5px" aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                        </asp:LinkButton> 
                         <button id="btAdd" type="button" data-toggle="tooltip" data-original-title="Add" runat="server" class="glyphicon glyphicon-plus btn btn-default btn-sm" onserverclick="btnAdd_Click"
                        style="margin-left:10px;margin-right:10px;height: 35px;"></button> 
                        
                    <input id="search" style="width:100%" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                    <button onserverclick="btnFind_Click" id="btsearch" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                </div>
            <div class="col-sm-6 col-md-3 col-xs-6 pull-right ">
                <button id="btCopy" type="button" runat="server" class="btn btn-primary btn-success" onserverclick="btnSystem_Click"
                    title="perform shortlisting of candidate based on requirement criteria" style="width:100%;height: 30px">
                    Shortlist Candidates
                </button>
            </div>
            <div class="col-sm-3 col-md-2 col-xs-6 pull-right">
                <telerik:RadComboBox ID="radStatus" runat="server" Width="100%" ForeColor="#666666" AutoPostBack="True"
                    Skin="Bootstrap">
                </telerik:RadComboBox>
            </div>
            <div class="col-sm-3 col-md-3 col-xs-6 pull-right">
                <telerik:RadComboBox runat="server" RenderMode="Lightweight"
                    ResolvedRenderMode="Classic" Width="100%" ID="cboCompany" AutoPostBack="True"
                    Filter="Contains" ForeColor="#666666" Skin="Bootstrap">
                </telerik:RadComboBox>
            </div>
        </div>
        <div class="row">
            <div class="table-responsive">
                <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Always">
                    <ContentTemplate>
                        <asp:GridView ID="GridVwHeaderChckbox" runat="server" OnSorting="SortRecords" AllowSorting="True"
                            BorderStyle="Solid" Font-Names="Verdana" AllowPaging="True" PageSize="100" DataKeyNames="id"
                            OnRowDataBound="OnRowDataBound" Width="100%" Height="50px" ToolTip="click row to select record"
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
                                <asp:TemplateField HeaderText="Code" ItemStyle-Font-Bold="true" SortExpression="code">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/Recruitment/JobPostingUpdate?id={0}",
                     HttpUtility.UrlEncode(Eval("code").ToString())) %>' Text='<%# Eval("code")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Job Title" HeaderText="Job Title" SortExpression="Job Title" />
                                <asp:BoundField DataField="Job Type" HeaderText="Job Type" SortExpression="job type" />
                                <asp:BoundField DataField="Specialization" HeaderText="Specialization" SortExpression="specialization" />
                                <asp:BoundField DataField="Date Posted" HeaderText="Date Posted" SortExpression="Date Posted"
                                    DataFormatString="{0:dd, MMM yyyy}" />
                                <asp:BoundField DataField="Closing Date" HeaderText="Closing Date" SortExpression=" Closing Date"
                                    DataFormatString="{0:dd, MMM yyyy}" />
                                <asp:TemplateField HeaderText="Applicants" ItemStyle-Font-Bold="true" SortExpression="Applicants"
                                    ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink11" runat="server" NavigateUrl='<%# string.Format("~/Module/Recruitment/Applicants?Jobid={0}&type=applications",
                     HttpUtility.UrlEncode(Eval("code").ToString())) %>' Text='<%# Eval("Applicants")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Shortlists" ItemStyle-Font-Bold="true" SortExpression="ShortLists"
                                    ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%# string.Format("~/Module/Recruitment/Applicants?Jobid={0}&type=shortlists",
                     HttpUtility.UrlEncode(Eval("code").ToString())) %>' Text='<%# Eval("ShortLists")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Recruits" ItemStyle-Font-Bold="true" SortExpression="Recruits"
                                    ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl='<%# string.Format("~/Module/Recruitment/Applicants?Jobid={0}&type=recruits",
                     HttpUtility.UrlEncode(Eval("code").ToString())) %>' Text='<%# Eval("Recruits")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="TestOnline" HeaderText="Aptitude Test" SortExpression="TestOnline" />
                            </Columns>
                            <HeaderStyle BackColor="White" ForeColor="#1BA691" HorizontalAlign="center" />
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cboCompany" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="radStatus" EventName="SelectedIndexChanged" />
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
        </div>
        </div>
        </form>
    </body>
    </html>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
</asp:Content>
