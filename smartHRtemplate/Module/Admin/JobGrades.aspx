<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="JobGrades.aspx.vb"
    Inherits="GOSHRM.JobGrades" EnableEventValidation="false" Debug="true" %>

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
    <title>Roles</title>
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
        .style25
        {
            width: 134px;
        }
        .style26
        {
            width: 22px;
        }
        .style27
        {
            width: 35px;
        }
        .style29
        {
            font-size: large;
            font-weight: bold;
            color: #FFFFFF;
        }
    </style>
    <body>
        <form id="form1">
        <div class="content">
            <div id="divalert" runat="server" visible="false" class="alert alert-info">
                <strong id="msgalert" runat="server">Danger!</strong>
            </div>
            <div class="row">
                <div class="col-xs-8">
                    <label>View</label>
                    <telerik:RadDropDownList ID="radView" runat="server" 
                        Width="50%" AutoPostBack="True" RenderMode="Lightweight" Skin="Bootstrap">
                        <Items>
                            <telerik:DropDownListItem runat="server" DropDownList="radView" Text="Job Grade"
                                Value="0" />
                            <telerik:DropDownListItem runat="server" DropDownList="radView" Text="Job Grade Chart"
                                Value="1" />
                        </Items>
                    </telerik:RadDropDownList>
                </div>
            </div>
            <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                <asp:View ID="vwList" runat="server">
                       <div class="row">
                            <div>
                                <div id="div1" runat="server" visible="false" class="alert alert-info">
                                    <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <strong
                                        id="Strong1" runat="server">Danger!</strong>
                                </div>
                            </div>
                        </div>
          <div class="panel panel-success">
                <div class="panel-heading">
                    <h5><b id="pagetitle" runat="server"></b></h5>
                </div>
             <div class="panel-body">
         
<div class="row">
                <div class="search-box-wrapper col-sm-6 col-md-3 col-xs-12 form-group pull-right">                        
                    <input id="search" style="width:100%" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                    <button onserverclick="btnFind_Click" id="btsearch" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                </div>
                 <div class="col-md-3 col-sm-6 col-xs-12 form-group pull-right">                                  
                    <button id="btnUploadFile" type="button" runat="server" class="fa fa-upload btn btn-default btn-sm"
                        onserverclick="btnUpload_Click" data-toggle="tooltip" data-original-title="CSV File: Grade, ReportToGrade, GradeDescription, Rank Number, Probation" style="margin-right:10px;margin-left:10px;height:35px"></button>
                        <button id="btExport" data-toggle="tooltip" data-original-title="Export" type="button" runat="server" class="fa fa-download btn btn-default btn-sm" onserverclick="btnExport_Click"
                    style="height: 35px"></button>
                        <asp:LinkButton ID="btnDelete" data-toggle="tooltip" data-original-title="Delete" Height="35px" runat="server" CssClass="btn btn-default btn-sm" OnClick="Delete" OnClientClick="Confirm()">
                            <span style="margin-top:5px" aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                        </asp:LinkButton>
                         <button id="btAdd" type="button" data-toggle="tooltip" data-original-title="Add" runat="server" class="glyphicon glyphicon-plus btn btn-default btn-sm" onserverclick="btnAdd_Click"
                        style="height: 35px;margin-right:10px;margin-left:10px;"></button> 
                </div>
                <div class="col-md-3 col-sm-6 col-xs-12 pull-right">
                    <input style="height:35px;" class="form-control" type="file" id="file1" runat="server" />
                </div>
      
        <%--<div class="col-sm-3 col-md-3 col-xs-6">
            <div class="form-group form-focus">
                <input id="search" runat="server" type="text" class="form-control floating" style="height: 30px"
                    placeholder="Search..." />
                <button id="btFind" type="button" runat="server" class="glyphicon glyphicon-search"
                    onserverclick="btnFind_Click" style="height: 29px; width: 40px">
                </button>
            </div>
        </div>

        <div class="col-sm-3 col-md-1 col-xs-6">
            <button id="btAdd" type="button" runat="server" class="btn-success" onserverclick="btnAdd_Click"
                style="height: 30px; width: 100px">
                Add
            </button>
        </div>

        <div class="col-sm-3 col-md-1 col-xs-6">
            <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="Delete" OnClientClick="Confirm()"
                BackColor="#FF3300" ForeColor="White" Width="100px" Height="30px" CssClass="btn-danger"
                BorderStyle="None" Font-Names="Verdana" Font-Size="12px" />
        </div>

           <div class="col-sm-3 col-md-1 col-xs-6">
            <button id="btExport" type="button" runat="server" class="btn-warning" onserverclick="btnExport_Click"
                style="height: 30px; width: 100px">
                Export</button>
        </div>

        <div class="col-sm-3 col-md-2 col-xs-6">
            <input class="form-control" type="file" id="file1" runat="server" />
        </div>

        <div class="col-sm-3 col-md-1 col-xs-6">
            <button id="btnUploadFile" type="button" runat="server" class=" btn-info btn-block"
                onserverclick="btnUpload_Click"  title="CSV File: Grade, ReportToGrade, GradeDescription, Rank Number, Probation"
                style="height: 30px; width: 100px">
                Upload</button>
        </div>--%>
        
    </div>

                    <div class="row">
                        <div class="table-responsive">
                            <asp:GridView ID="GridVwHeaderChckbox" runat="server" OnSorting="SortRecords" AllowSorting="True"
                                BorderStyle="Solid" Font-Names="Verdana" AllowPaging="True" PageSize="200" DataKeyNames="id"
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
                                            <asp:CheckBox ID="chkEmp" runat="server"></asp:CheckBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Rows" HeaderText="Rows" SortExpression="rows" />
                                    <asp:TemplateField HeaderText="Name" ItemStyle-Font-Bold="true" SortExpression="name">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/Admin/JobGradesUpdate?id={0}",
                     HttpUtility.UrlEncode(Eval("id").ToString())) %>' 
                                            Text='<%# Eval("name")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:BoundField DataField="ReportsTo"  HeaderText="Reports To" SortExpression="ReportsTo"/>
                                <asp:BoundField DataField="Level Rank"  HeaderText="Rank" SortExpression="Level Rank"/>
                                <asp:BoundField DataField="Probation (Mth)"  HeaderText="Probation" SortExpression="Probation (Mth)" DataFormatString="{0:d}" ItemStyle-HorizontalAlign="Right"  />
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
                    </div></div>
                    </div>

                </asp:View>
                <asp:View ID="vwTree" runat="server">
                <asp:Panel ID="Panel1" runat="server" BorderColor="#CCCCCC" Width="100%">
                    <telerik:RadTreeView ID="RadTreeView1" runat="server" RenderMode="Lightweight" Width="100%"
                        ResolvedRenderMode="Classic" Font-Bold="True" Skin="Bootstrap" 
                         ForeColor="#666666"></telerik:RadTreeView>
                </asp:Panel>
            </asp:View>
            </asp:MultiView>


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
        .style23
        {
            width: 275px;
        }
    </style>
</asp:Content>
