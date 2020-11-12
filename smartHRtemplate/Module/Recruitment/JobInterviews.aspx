<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="JobInterviews.aspx.vb"
    Inherits="GOSHRM.JobInterviews" EnableEventValidation="false" Debug="true" %>

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
        .style28
        {            width: 177px;
        }
    .RadComboBox_Default{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.RadComboBox{text-align:left;display:inline-block;vertical-align:middle;white-space:nowrap;*display:inline;*zoom:1}.RadComboBox_Default .rcbReadOnly .rcbInput{color:#333}.RadComboBox .rcbReadOnly .rcbInput{cursor:default}.RadComboBox_Default .rcbInput{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif;line-height:16px}.RadComboBox .rcbInput{margin:0;padding:2px 0 1px;height:auto;width:100%;border-width:0;outline:0;color:inherit;background-color:transparent;vertical-align:top}
    </style>
    <body>
        <form id="form1">
        <div class="container col-md-12">
        <div class="row">
        <div id="divalert" runat="server" visible="false" class="alert alert-info">
            <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <strong
                id="msgalert" runat="server">Danger!</strong>
        </div>
        </div>
            <div id="content" runat="server">
           <div class="panel panel-success">
                <div class="panel-heading">
                   <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Always">
                        <ContentTemplate>
                            <h5 id="pagetitle" runat="server" class="page-title" style="color: #1BA691">
                                Head</h5>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cbocompany" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
             <div class="panel-body">
        <div class="row">
        <div class="search-box-wrapper col-sm-6 col-md-4 col-xs-12 form-group pull-right">
                        <asp:LinkButton ID="btnDelete" data-toggle="tooltip" data-original-title="Delete" Height="35px" runat="server" CssClass="btn btn-default btn-sm" OnClick="Delete" OnClientClick="Confirm()">
                            <span style="margin-top:5px" aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                        </asp:LinkButton>
                         <button id="btAdd" type="button" data-toggle="tooltip" data-original-title="Add" runat="server" class="glyphicon glyphicon-plus btn btn-default btn-sm" onserverclick="btnAdd_Click"
                        style="margin-left:10px;margin-right:10px;height: 35px;"></button> 
                    <input id="search" style="width:100%" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                    <button onserverclick="btnFind_Click" id="btnsearch" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                </div>
            <div class="col-sm-3 col-md-3 col-xs-6 pull-right">
                <telerik:RadComboBox runat="server" DropDownAutoWidth="Enabled" RenderMode="Lightweight"
                    ResolvedRenderMode="Classic" Width="100%" ID="cbocompany" AutoPostBack="True"
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
                                <asp:TemplateField HeaderText="Code" ItemStyle-Font-Bold="true" SortExpression="jobcode">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/Recruitment/JobinterviewUpdate.aspx?id={0}",
                     HttpUtility.UrlEncode(Eval("id").ToString())) %>' Text='<%# Eval("jobcode")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Job Post" HeaderText="Job Title" SortExpression="Job Post" />                               
                                <asp:BoundField DataField="Interview Date" HeaderText="Date Posted" SortExpression="Interview Date" DataFormatString="{0:dd, MMM yyyy}" />
                                <asp:BoundField DataField="Interview Time" HeaderText="Interview Time" SortExpression="Interview Time"/>
                                <asp:BoundField DataField="Interviewers" HeaderText="Interviewers" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:d}" SortExpression="Interviewers"  />
                                <asp:TemplateField HeaderText="Shortlisted" ItemStyle-Font-Bold="true" SortExpression="Interviewees" ItemStyle-HorizontalAlign="Right" >
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl='<%# string.Format("~/Module/Recruitment/Interviewees.aspx?id={0}",
                     HttpUtility.UrlEncode(Eval("id").ToString())) %>' Text='<%# Eval("Interviewees")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Recruited" ItemStyle-Font-Bold="true">
                                    <ItemTemplate>
                                        <a>Candidates:
                                            <%# Eval("Recruited")%></a>
                                    </ItemTemplate>
                                </asp:TemplateField>  
                            </Columns>
                            <HeaderStyle BackColor="White" ForeColor="#1BA691" HorizontalAlign="center" />
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cbocompany" EventName="SelectedIndexChanged" />
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
     </div></div></div>
     </div>
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
